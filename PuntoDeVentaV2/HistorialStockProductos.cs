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

        public void CargarHistorialStock()
        {
            int idprod = Productos.idProductoHistorialStock;
            var datos = cn.CargarDatos($"SELECT ID, IDProducto AS 'ID de Producto', TipoDeMovimiento AS 'Tipo de Movimiento', StockAnterior AS 'Stock Anterior', Cantidad, StockNuevo 'Nuevo Stock', Fecha, NombreUsuario AS 'Nombre de Usuario' FROM historialstock WHERE IDProducto = {idprod}");

            var nombreproducto = cn.CargarDatos($"SELECT Nombre FROM PRODUCTOS WHERE ID = {idprod}");
            txtNombreProducto.Text = nombreproducto.Rows[0]["Nombre"].ToString();

            DGVHistorialStock.DataSource = datos;
            DGVHistorialStock.Columns["ID"].Visible = false;
            DGVHistorialStock.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
    }
}
