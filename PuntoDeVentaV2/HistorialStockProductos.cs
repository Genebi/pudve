using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{

    public partial class HistorialStockProductos : Form
    {

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        public HistorialStockProductos()
        {
            InitializeComponent();
        }

        private void HistorialStockProductos_Load(object sender, EventArgs e)
        {
            CargarHistorialStock();
        }

        public void dgvHistorialStockDatos(DataTable datos, string stockInicialProductos,string StockInicialH)
        {
            DataTable dtHistorialStock = new DataTable();
            dtHistorialStock.Clear();
            dtHistorialStock.Columns.Add("ID");
            dtHistorialStock.Columns.Add("Tipo de Movimiento");
            dtHistorialStock.Columns.Add("Stock Anterior");
            dtHistorialStock.Columns.Add("Cantidad");
            dtHistorialStock.Columns.Add("Nuevo Stock");
            dtHistorialStock.Columns.Add("Nombre de Usuario");
            //dtHistorialStock.Columns.Add("StockInicial");
            dtHistorialStock.Columns.Add("Fecha");

            if (!datos.Rows.Count.Equals(0))
            {
                DataRow drow = dtHistorialStock.NewRow();
                drow["ID"] = string.Empty;
                drow["Tipo de Movimiento"] = "Stock al Registrarse el Producto";
                drow["Stock Anterior"] = 0;
                drow["Cantidad"] = StockInicialH;
                drow["Nuevo Stock"] = 0;
                drow["Nombre de Usuario"] = FormPrincipal.userNickName;
                //drow["StockInicial"] = string.Empty;
                drow["Fecha"] = DateTime.Now.ToString("dd/MM/yyyy");
                dtHistorialStock.Rows.Add(drow);

                foreach (DataRow item in datos.Rows)
                {
                    drow = dtHistorialStock.NewRow();
                    drow["ID"] = item["ID"].ToString();
                    drow["Tipo de Movimiento"] = item["Tipo de Movimiento"].ToString();
                    drow["Stock Anterior"] = item["Stock Anterior"].ToString();
                    drow["Cantidad"] = item["Cantidad"].ToString();
                    drow["Nuevo Stock"] = item["Nuevo Stock"].ToString();
                    drow["Nombre de Usuario"] = item["Nombre de Usuario"].ToString();
                    //drow["StockInicial"] = item["StockInicial"].ToString();
                    drow["Fecha"] = item["Fecha"].ToString();
                    dtHistorialStock.Rows.Add(drow);
                }
            }
            else
            {
                DataRow drow = dtHistorialStock.NewRow();
                drow["ID"] = string.Empty;
                drow["Tipo de Movimiento"] = "Stock al Registrarse el Producto";
                drow["Stock Anterior"] = stockInicialProductos;
                drow["Cantidad"] = stockInicialProductos;
                drow["Nuevo Stock"] = stockInicialProductos;
                drow["Nombre de Usuario"] = FormPrincipal.userNickName;
                //drow["StockInicial"] = string.Empty;
                drow["Fecha"] = DateTime.Now.ToString("dd/MM/yyyy");
                dtHistorialStock.Rows.Add(drow);
            }
            DGVHistorialStock.DataSource = dtHistorialStock;
            DGVHistorialStock.Columns["ID"].Visible = false;
            DGVHistorialStock.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        public void CargarHistorialStock()
        {
            string StockInicial = "";
            string StockInicialH = "";
            var stockInicialProductos = string.Empty;
            int idprod = Productos.idProductoHistorialStock;
            var datos = cn.CargarDatos($"SELECT ID, TipoDeMovimiento AS 'Tipo de Movimiento', StockAnterior AS 'Stock Anterior', Cantidad, StockNuevo 'Nuevo Stock', Fecha, NombreUsuario AS 'Nombre de Usuario', StockInicial FROM historialstock WHERE IDProducto = {idprod}");

            var datoStock = cn.CargarDatos($"SELECT Cantidad FROM `historialstock` WHERE IDProducto = {idprod} ORDER BY Fecha ASC LIMIT 1");
            if (!datoStock.Rows.Count.Equals(0))
            {
              StockInicial = datos.Rows[0]["Cantidad"].ToString();
            }
           
            if(datos.Rows.Count.Equals(0))
            {
                //Agregar stock inicial desde productos.
                var stockProductos = cn.CargarDatos($"SELECT Stock FROM productos WHERE ID = {idprod}");
                stockInicialProductos = stockProductos.Rows[0]["Stock"].ToString();
                cn.EjecutarConsulta($"UPDATE historialstock SET StockInicial ='{stockInicialProductos}' WHERE IdProducto = {idprod}");
            }
            else if (!datos.Rows.Count.Equals(0) && !string.IsNullOrEmpty(StockInicial))
            {
                //Agregar el stock desde Historial stock tomando el primer stock de la 1° venta.
                var stockHistorial = cn.CargarDatos($"SELECT StockAnterior FROM `historialstock` WHERE IDProducto = {idprod} ORDER BY Fecha ASC LIMIT 1");
                StockInicialH = stockHistorial.Rows[0]["StockAnterior"].ToString();
                cn.EjecutarConsulta($"UPDATE historialstock SET StockInicial ='{StockInicialH}' WHERE IdProducto = {idprod}");
            }

            var nombreproducto = cn.CargarDatos($"SELECT Nombre FROM PRODUCTOS WHERE ID = '{idprod}'");
            txtNombreProducto.Text = nombreproducto.Rows[0]["Nombre"].ToString();

            dgvHistorialStockDatos(datos, stockInicialProductos, StockInicialH);
        }

        private void HistorialStockProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }
    }
}
