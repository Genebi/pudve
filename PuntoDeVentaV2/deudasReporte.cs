using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
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
    public partial class deudasReporte : Form
    {
        DataTable dt;
        Consultas cs = new Consultas();
        Conexion cn = new Conexion();

        public deudasReporte(DataTable datos)
        {
            InitializeComponent();
            dt = datos;
        }

        private void deudasReporte_Load(object sender, EventArgs e)
        {

            string cadenaConn = string.Empty;

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                cadenaConn = $"datasource={Properties.Settings.Default.Hosting};port=6666;username=root;password=;database=pudve;";
            }
            else
            {
                cadenaConn = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
            }

            MySqlConnection conn = new MySqlConnection();

            conn.ConnectionString = cadenaConn;

            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            string pathApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\Deudas\Deudas.rdlc";

            ReportParameterCollection reportParameters = new ReportParameterCollection();

            using (DataTable dt = cn.CargarDatos($"SELECT RFC,RazonSocial,Email,Telefono FROM usuarios WHERE ID={FormPrincipal.userID}"))
            {
                reportParameters.Add(new ReportParameter("RFC", dt.Rows[0]["RFC"].ToString()));
                reportParameters.Add(new ReportParameter("RazonSocial", dt.Rows[0]["RazonSocial"].ToString()));
                reportParameters.Add(new ReportParameter("Email", dt.Rows[0]["Email"].ToString()));
                reportParameters.Add(new ReportParameter("Telefono", dt.Rows[0]["Telefono"].ToString()));
            }

            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource DTDeudas = new ReportDataSource("Deudas", dt);

            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.LocalReport.DataSources.Add(DTDeudas);
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }
    }
}
