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
        string queryFiltroReporteStock, tabla;

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
        }

        private void OcultarColumnasDGV()
        {
            // Ocultamos las columnas que no seran de utilidad para el usuario
            DGVRevisionStock.Columns["ID"].Visible = false;
            DGVRevisionStock.Columns["IDUsuario"].Visible = false;
            DGVRevisionStock.Columns["Tipo"].Visible = false;
            DGVRevisionStock.Columns["StatusRevision"].Visible = false;
            // Cambiamos el texto de la columbas para mejor visualizacion
            DGVRevisionStock.Columns["ClaveInterna"].HeaderText = "Clave";
            DGVRevisionStock.Columns["CodigoBarras"].HeaderText = "Código";
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
            queryFiltroReporteStock = $"SELECT * FROM '{tabla}' WHERE StatusRevision = '1' ORDER BY DATE(Fecha) DESC";
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
