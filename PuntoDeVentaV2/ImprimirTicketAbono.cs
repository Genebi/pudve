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
    public partial class ImprimirTicketAbono : Form
    {
        public int idAbono { get; set; }

        Consultas cs = new Consultas();
        public ImprimirTicketAbono()
        {
            InitializeComponent();
        }

        private void ImprimirTicketAbono_Load(object sender, EventArgs e)
        {
            impresionTicketAbono();
        }

        private void impresionTicketAbono()
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

            string queryVenta = cs.ImpresionTicketAbono(idAbono);

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
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\AbonoRealizado\ReporteAbonos.rdlc";

            MySqlDataAdapter ventaDA = new MySqlDataAdapter(queryVenta, conn);
            DataTable ventaDT = new DataTable();

            ventaDA.Fill(ventaDT);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            #region Impresion Ticket de 80 mm
            ReportDataSource rp = new ReportDataSource("TicketAbono", ventaDT);

            string DirectoryImage = string.Empty;

            this.reportViewer1.LocalReport.EnableExternalImages = true;

            this.reportViewer1.LocalReport.DataSources.Add(rp);
            this.reportViewer1.RefreshReport();

            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.DataSources.Add(rp);
            #endregion

            EnviarImprimir imp = new EnviarImprimir();
            imp.Imprime(rdlc);
            this.Close();
        }
    }
}
