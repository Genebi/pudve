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
    public partial class VisualizarTicketAbono : Form
    {
        public int idVenta { get; set; }
        public int idAbono { get; set; }
        public int idSaldoRestante { get; set; }

        Consultas cs = new Consultas();
        public VisualizarTicketAbono()
        {
            InitializeComponent();
        }

        private void VisualizarTicketAbono_Load(object sender, EventArgs e)
        {
            CargarDatosCaja();
        }

        private void CargarDatosCaja()
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

            string queryVenta = cs.visualizadorTicketAbono(idVenta,idAbono);

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


            MySqlDataAdapter ventaDA = new MySqlDataAdapter(queryVenta, conn);
            DataTable ventaDT = new DataTable();

            ventaDA.Fill(ventaDT);

            this.reportViewer1.LocalReport.DataSources.Clear();

            #region Impresion Ticket de 80 mm
            ReportDataSource rp = new ReportDataSource("TicketAbono", ventaDT);

            string DirectoryImage = string.Empty;

            this.reportViewer1.LocalReport.EnableExternalImages = true;

            this.reportViewer1.LocalReport.DataSources.Add(rp);
            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.RefreshReport();

            //LocalReport rdlc = new LocalReport();
            //rdlc.EnableExternalImages = true;
            //rdlc.ReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\AbonoRealizado\ReporteAbonos.rdlc";
            //rdlc.DataSources.Add(rp);
            #endregion

            //EnviarImprimir imp = new EnviarImprimir();
            //imp.Imprime(rdlc);
            //this.reportViewer1.RefreshReport();
        }

        private void btnReImprimirTicket_Click(object sender, EventArgs e)
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

            string queryVenta = cs.visualizadorTicketAbono(idVenta, idAbono);

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

            MySqlDataAdapter ventaDA = new MySqlDataAdapter(queryVenta, conn);
            DataTable ventaDT = new DataTable();

            ventaDA.Fill(ventaDT);

            #region Impresion Ticket de 80 mm
            ReportDataSource rp = new ReportDataSource("TicketAbono", ventaDT);

            string DirectoryImage = string.Empty;

            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\AbonoRealizado\ReporteAbonos.rdlc";
            rdlc.DataSources.Add(rp);
            #endregion

            EnviarImprimir imp = new EnviarImprimir();
            imp.Imprime(rdlc);
            this.reportViewer1.RefreshReport();
            this.Close();
        }
    }
}
