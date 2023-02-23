using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using PuntoDeVentaV2.ReportesImpresion;
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
    public partial class ImpresionTicketAnticipo : Form
    {
        public int idAnticipo { get; set; }
        public int anticipoSinHistorial { get; set; }
        Consultas cs = new Consultas();

        public ImpresionTicketAnticipo()
        {
            InitializeComponent();
        }

        private void ImpresionTicketAnticipo_Load(object sender, EventArgs e)
        {
            cargarDatosTicket();

        }

        private void cargarDatosTicket()
        {
            string cadenaConn = string.Empty;
            string queryVenta = string.Empty;

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                cadenaConn = $"datasource={Properties.Settings.Default.Hosting};port=6666;username=root;password=;database=pudve;";
            }
            else
            {
                cadenaConn = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
            }

            if (anticipoSinHistorial == 1)
            {
                queryVenta = cs.visualizadorTicketAnticipo(idAnticipo);
            }
            else
            {
                queryVenta = cs.impresionTicketAnticipo(idAnticipo);
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
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\TicketAnticipo\ReporteTicketAnticipo.rdlc";

            MySqlDataAdapter ventaDA = new MySqlDataAdapter(queryVenta, conn);
            DataTable ventaDT = new DataTable();

            ventaDA.Fill(ventaDT);
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            decimal AnticipoAplicado = 0;
            if (!ventaDT.Rows[0]["AnticipoAplicado"].Equals("N/A"))
            {
                AnticipoAplicado = Convert.ToDecimal(ventaDT.Rows[0]["AnticipoAplicado"]);
            }
            decimal algo = Convert.ToDecimal(ventaDT.Rows[0]["TotalRecibido"]) - AnticipoAplicado - Convert.ToDecimal(ventaDT.Rows[0]["SaldoRestante"]);
            reportParameters.Add(new ReportParameter("Anterior", algo.ToString("0.00")));

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.LocalReport.DataSources.Clear();

            #region Impresion Ticket de 80 mm
            ReportDataSource rp = new ReportDataSource("TicketAnticipo", ventaDT);

            string DirectoryImage = string.Empty;

            this.reportViewer1.LocalReport.EnableExternalImages = true;

            this.reportViewer1.LocalReport.DataSources.Add(rp);
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();

            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);
            rdlc.DataSources.Add(rp);
            #endregion

            EnviarImprimir imp = new EnviarImprimir();
            imp.Imprime(rdlc);
            this.Close();
        }
    }
}
