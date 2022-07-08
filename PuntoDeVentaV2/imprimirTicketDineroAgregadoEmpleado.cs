using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using PuntoDeVentaV2.ReportesImpresion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class imprimirTicketDineroAgregadoEmpleado : Form
    {
        Consultas cs = new Consultas();
        Conexion cn = new Conexion();

        public int idDineroAgregado { get; set; }

        public imprimirTicketDineroAgregadoEmpleado()
        {
            InitializeComponent();
        }

        private void imprimirTicketDineroAgregadoEmpleado_Load(object sender, EventArgs e)
        {
            CargarDatosTicket();
        }

        private void CargarDatosTicket()
        {
            var servidor = Properties.Settings.Default.Hosting;
            string cadenaConn = string.Empty;
            string queryDineroAgregado = cs.obtenerDatosTicketAgregarDineroEmpleado(idDineroAgregado);
            MySqlConnection conn = new MySqlConnection();
            string pathApplication = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\DineroAgregado\TicketDineroAgregado.rdlc";
            string DirectoryImage = string.Empty;
            string path = string.Empty;
            string pathBarCode = $@"C:\Archivos PUDVE\Ventas\Tickets\BarCode\";
            var folioVentaRealizada = string.Empty;
            var finalLogoTipoPath = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                cadenaConn = $"datasource={servidor};port=6666;username=root;password=;database=pudve;";
            }
            else
            {
                cadenaConn = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve";
            }

            conn.ConnectionString = cadenaConn;

            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            MySqlDataAdapter dineroAgregadoDA = new MySqlDataAdapter(queryDineroAgregado, conn);
            DataTable dineroAgregadoDT = new DataTable();

            dineroAgregadoDA.Fill(dineroAgregadoDT);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            #region Impresion Ticket de 8 cm (80 mm)
            ReportDataSource rp = new ReportDataSource("TicketDineroAgregado", dineroAgregadoDT);

            this.reportViewer1.LocalReport.DataSources.Add(rp);
            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.RefreshReport();

            LocalReport rdlc = new LocalReport();
            rdlc.ReportPath = FullReportPath;
            rdlc.DataSources.Add(rp);
            #endregion

            EnviarImprimir imp = new EnviarImprimir();
            imp.Imprime(rdlc);

            this.Close();
        }
    }
}
