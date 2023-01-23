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
    public partial class ImprimirTicketRetirarDineroCaja8cm : Form
    {
        Consultas cs = new Consultas();
        Conexion cn = new Conexion();

        public int idDineroRetirado { get; set; }

        public ImprimirTicketRetirarDineroCaja8cm()
        {
            InitializeComponent();
        }

        private void ImprimirTicketRetirarDineroCaja8cm_Load(object sender, EventArgs e)
        {
            CargarDatosTicket();
        }

        private void CargarDatosTicket()
        {
            var servidor = Properties.Settings.Default.Hosting;
            string cadenaConn = string.Empty;
            string queryDineroRetirado = cs.obtenerDatosTicketRetirarDinero(idDineroRetirado);
            MySqlConnection conn = new MySqlConnection();
            string pathApplication = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\DineroRetirado\TicketDineroRetirado.rdlc";
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

            MySqlDataAdapter dineroRetiradoDA = new MySqlDataAdapter(queryDineroRetirado, conn);
            DataTable dineroRetiradoDT = new DataTable();
            string Comentario;
            using (var dt = cn.CargarDatos(queryDineroRetirado))
            {
                if (dt.Rows[0]["Comentarios"].ToString().Equals("COMENTARIOS") || dt.Rows[0]["Comentarios"].ToString().Equals(""))
                {
                    Comentario = "";
                }
                else
                {
                    Comentario = dt.Rows[0]["Comentarios"].ToString();
                }
            }
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("Comentario", Comentario));
            dineroRetiradoDA.Fill(dineroRetiradoDT);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            #region Impresion Ticket de 8 cm (80 mm)
            ReportDataSource rp = new ReportDataSource("TicketDineroRetirado", dineroRetiradoDT);

            this.reportViewer1.LocalReport.DataSources.Add(rp);
            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.RefreshReport();

            LocalReport rdlc = new LocalReport();
            rdlc.ReportPath = FullReportPath;
            rdlc.DataSources.Add(rp);
            rdlc.SetParameters(reportParameters);
            #endregion

            EnviarImprimir imp = new EnviarImprimir();
            imp.Imprime(rdlc);

            this.Close();
        }
    }
}
