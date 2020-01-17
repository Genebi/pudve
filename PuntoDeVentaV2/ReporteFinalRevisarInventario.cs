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
    public partial class ReporteFinalRevisarInventario : Form
    {
        Conexion cn = new Conexion();
        DataTable dtFinalReportCheckStockToDay;

        MetodosBusquedas mb = new MetodosBusquedas();

        public static int FilterNumActiveRecord;

        public int GetFilterNumActiveRecord { get; set; }
        public bool limpiarTabla { get; set; }

        bool IsEmpty;
        int CantidadAlmacen, CantidadFisico, Diferencias;
        string queryFiltroReporteStock, tabla, queryUpdateCalculos, FechaRevision, soloFechaRevision, fechaActual;

        string consultaStockPaqServ = string.Empty;

        private void FiltroNumRevisionActiva()
        {
            FilterNumActiveRecord = GetFilterNumActiveRecord;
        }

        public ReporteFinalRevisarInventario()
        {
            InitializeComponent();
        }

        private void ReporteFinalRevisarInventario_Load(object sender, EventArgs e)
        {
            IsEmpty = false;
            tabla = "RevisarInventario";
            FiltroNumRevisionActiva();
            cargarTabla();
            checkEmpty(tabla);
            llenarDataGriView();

            if (IsEmpty)
            {
                OcultarColumnasDGV();
                hacerCalculosDGVRevisionStock();
            }
            
            if (limpiarTabla)
            {
                VaciarTabla();
            }
        }

        private void VaciarTabla()
        {
            cn.EjecutarConsulta($"DELETE FROM RevisarInventario WHERE NoRevision = {FilterNumActiveRecord} AND IDUsuario = {FormPrincipal.userID}");
        }

        private void hacerCalculosDGVRevisionStock()
        {
            DGVRevisionStock.Columns["IDAlmacen"].Width = 65;
            DGVRevisionStock.Columns["Nombre"].Width = 190;
            DGVRevisionStock.Columns["ClaveInterna"].Width = 65;
            DGVRevisionStock.Columns["CodigoBarras"].Width = 65;
            DGVRevisionStock.Columns["StockAlmacen"].Width = 65;
            DGVRevisionStock.Columns["StockFisico"].Width = 65;
            DGVRevisionStock.Columns["Fecha"].Width = 90;
            DGVRevisionStock.Columns["Vendido"].Width = 65;
            DGVRevisionStock.Columns["Diferencia"].Width = 65;

            foreach (DataGridViewRow row in DGVRevisionStock.Rows)
            {
                CantidadAlmacen = Convert.ToInt32(row.Cells["StockAlmacen"].Value);
                CantidadFisico = Convert.ToInt32(row.Cells["StockFisico"].Value);
                FechaRevision = row.Cells["Fecha"].Value.ToString();
                soloFechaRevision = FechaRevision.Substring(0, 10);
                fechaActual = DateTime.Now.ToString("dd/MM/yyyy");
                Diferencias = CantidadFisico - CantidadAlmacen;
                row.Cells["Diferencia"].Value = Diferencias;

                var precioProducto = float.Parse(row.Cells["PrecioProducto"].Value.ToString());
                var cantidadPerdida = 0f;
                var cantidadRecuperada = 0f;

                // Si es negativa
                if (Diferencias < 0)
                {
                    var diferenciaTmp = Math.Abs(Diferencias);
                    cantidadPerdida = diferenciaTmp * precioProducto;
                    row.Cells["Perdida"].Value = cantidadPerdida.ToString("0.00");
                    row.Cells["Recuperada"].Value = "---";
                }

                // Si es positiva
                if (Diferencias > 0)
                {
                    cantidadRecuperada = Diferencias * precioProducto;
                    row.Cells["Recuperada"].Value = cantidadRecuperada.ToString("0.00");
                    row.Cells["Perdida"].Value = "---";
                }

                if (soloFechaRevision == fechaActual)
                {
                    queryUpdateCalculos = $"UPDATE '{tabla}' SET Diferencia = '{row.Cells["Diferencia"].Value.ToString()}' WHERE ID = '{row.Cells["ID"].Value.ToString()}'";
                    cn.EjecutarConsulta(queryUpdateCalculos);
                }
            }
        }

        private void OcultarColumnasDGV()
        {
            // Ocultamos las columnas que no seran de utilidad para el usuario
            DGVRevisionStock.Columns["ID"].Visible = false;
            DGVRevisionStock.Columns["NoRevision"].Visible = false;
            DGVRevisionStock.Columns["IDUsuario"].Visible = false;
            DGVRevisionStock.Columns["Tipo"].Visible = false;
            DGVRevisionStock.Columns["StatusRevision"].Visible = false;
            DGVRevisionStock.Columns["StatusInventariado"].Visible = false;
            DGVRevisionStock.Columns["Vendido"].Visible = false;

            // Cambiamos el texto de la columbas para mejor visualizacion
            DGVRevisionStock.Columns["IDAlmacen"].HeaderText = "ID";
            DGVRevisionStock.Columns["ClaveInterna"].HeaderText = "Clave";
            DGVRevisionStock.Columns["CodigoBarras"].HeaderText = "Código";
            DGVRevisionStock.Columns["StockAlmacen"].HeaderText = "Punto de Venta";
            DGVRevisionStock.Columns["StockFisico"].HeaderText = "Stock Físico";
            DGVRevisionStock.Columns["Fecha"].HeaderText = "Revisado el";
        }

        private void llenarDataGriView()
        {
            if (IsEmpty == true)
            {
                DGVRevisionStock.DataSource = dtFinalReportCheckStockToDay;

                if (DGVRevisionStock.Rows.Count > 0)
                {
                    DataGridViewColumn colPerdida = new DataGridViewTextBoxColumn();
                    colPerdida.HeaderText = "Cantidad perdida";
                    colPerdida.Name = "Perdida";
                    DGVRevisionStock.Columns.Add(colPerdida);

                    DataGridViewColumn colRecuperada = new DataGridViewTextBoxColumn();
                    colRecuperada.HeaderText = "Cantidad recuperada";
                    colRecuperada.Name = "Recuperada";
                    DGVRevisionStock.Columns.Add(colRecuperada); 

                    foreach (DataGridViewRow row in DGVRevisionStock.Rows)
                    {
                        var idProducto = Convert.ToInt32(row.Cells["IDAlmacen"].Value);
                        
                        cn.EjecutarConsulta($"UPDATE Productos SET NumeroRevision = {FilterNumActiveRecord} WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID}");

                        var infoGetPaqServ = mb.ObtenerPaqueteServicioAsignado(Convert.ToInt32(idProducto.ToString()), FormPrincipal.userID);

                        var IsEmptyList = infoGetPaqServ.Length;

                        if (IsEmptyList > 0)
                        {
                            consultaStockPaqServ = $"UPDATE Productos SET NumeroRevision = '{infoGetPaqServ[4].ToString()}' WHERE ID = {infoGetPaqServ[6].ToString()} AND IDUsuario = {infoGetPaqServ[0].ToString()}";
                            cn.EjecutarConsulta(consultaStockPaqServ);
                        }
                    }
                }
            }
            else if (IsEmpty == false)
            {
                MessageBox.Show("La base de datos de Checar Stock verificado no tiene registros", "No tiene registros", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cargarTabla()
        {
            queryFiltroReporteStock = $"SELECT * FROM '{tabla}' WHERE IDUsuario = '{FormPrincipal.userID}' AND NoRevision = '{FilterNumActiveRecord}' ORDER BY Fecha DESC, Nombre ASC";
            //queryFiltroReporteStock = $"SELECT * FROM '{tabla}' WHERE IDUsuario = '{FormPrincipal.userID}' AND StatusInventariado = '1' ORDER BY Fecha DESC, Nombre ASC";
            dtFinalReportCheckStockToDay = cn.CargarDatos(queryFiltroReporteStock);
        }

        private bool checkEmpty(string tabla)
        {
            string queryTableCheck = $"SELECT * FROM '{tabla}'";
            IsEmpty = cn.IsEmptyTable(queryTableCheck);
            return IsEmpty;
        }
    }
}
