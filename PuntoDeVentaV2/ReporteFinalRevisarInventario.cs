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

        bool IsEmpty;
        int CantidadAlmacen, CantidadFisico, Diferencias;
        string queryFiltroReporteStock, tabla, queryUpdateCalculos, FechaRevision, soloFechaRevision, fechaActual;
        
        public ReporteFinalRevisarInventario()
        {
            InitializeComponent();
        }

        private void ReporteFinalRevisarInventario_Load(object sender, EventArgs e)
        {
            IsEmpty = false;
            tabla = "RevisarInventario";
            cargarTabla();
            checkEmpty(tabla);
            llenarDataGriView();
            OcultarColumnasDGV();
            hacerCalculosDGVRevisionStock();
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

            // Cambiamos el texto de la columbas para mejor visualizacion
            DGVRevisionStock.Columns["IDAlmacen"].HeaderText = "ID Almacen";
            DGVRevisionStock.Columns["ClaveInterna"].HeaderText = "Clave";
            DGVRevisionStock.Columns["CodigoBarras"].HeaderText = "Código";
            DGVRevisionStock.Columns["StockAlmacen"].HeaderText = "Almacen";
            DGVRevisionStock.Columns["StockFisico"].HeaderText = "Física";
            DGVRevisionStock.Columns["Fecha"].HeaderText = "Revisión";
        }

        private void llenarDataGriView()
        {
            if (IsEmpty == true)
            {
                DGVRevisionStock.DataSource = dtFinalReportCheckStockToDay;
            }
            else if (IsEmpty == false)
            {
                MessageBox.Show("La base de datos de Checar Stock verificado no tiene registros", "No tiene registros", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarTabla()
        {
            queryFiltroReporteStock = $"SELECT * FROM '{tabla}' WHERE IDUsuario = '{FormPrincipal.userID}' AND StatusInventariado = '1' ORDER BY Fecha DESC, Nombre ASC";
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
