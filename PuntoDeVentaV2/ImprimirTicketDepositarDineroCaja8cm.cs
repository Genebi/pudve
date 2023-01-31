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
    public partial class ImprimirTicketDepositarDineroCaja8cm : Form
    {
        Consultas cs = new Consultas();
        Conexion cn = new Conexion();

        public int idDineroAgregado { get; set; }
        public static bool SaldoACaja = false; 

        public ImprimirTicketDepositarDineroCaja8cm()
        {
            InitializeComponent();
        }

        private void ImprimirTicketDepositarDineroCaja8cm_Load(object sender, EventArgs e)
        {
            CargarDatosTicket();
        }

        private void CargarDatosTicket()
        {
            var servidor = Properties.Settings.Default.Hosting;
            string cadenaConn = string.Empty;
            string queryDineroAgregado = string.Empty;
            if (SaldoACaja.Equals(true))
            {
                queryDineroAgregado = $"SET lc_time_names = 'es_MX'; SELECT IF ( Usr.RazonSocial = '' OR Usr.RazonSocial IS NULL, '', Usr.RazonSocial ) AS 'RazonSocial', IF ( Box.Operacion = '' OR Box.Operacion IS NULL, '', 'Ticket DEPOSITO' ) AS 'TipoTicket', IF ( Box.FechaOperacion = '' OR Box.FechaOperacion IS NULL, '', CONCAT( 'Fecha: ', ( CONCAT( DATE_FORMAT( Box.FechaOperacion, '%W - %e/%M/%Y' ), '', TIME_FORMAT( Box.FechaOperacion, '%h:%i:%s %p' ) ) ) ) ) AS 'FechaDeposito', IF ( Usr.Usuario = '' OR Usr.Usuario IS NULL, '', CONCAT( 'Empleado: ', Usr.Usuario ) ) AS 'Empleado', IF ( Box.Efectivo = '' OR Box.Efectivo IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Efectivo, 2 ) ) ) AS 'Efectivo', IF ( Box.Tarjeta = '' OR Box.Tarjeta IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Tarjeta, 2 ) ) ) AS 'Tarjeta', IF ( Box.Vales = '' OR Box.Vales IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Vales, 2 ) ) ) AS 'Vales', IF ( Box.Cheque = '' OR Box.Cheque IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Cheque, 2 ) ) ) AS 'Cheque', IF ( Box.Transferencia = '' OR Box.Transferencia IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Transferencia, 2 ) ) ) AS 'Transferencia', IF ( Box.Cantidad = '' OR Box.Cantidad IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Cantidad, 2 ) ) ) AS 'Total', 'SALDO INCIAL'AS 'Concepto', Box.Comentarios FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID = '{idDineroAgregado}'";
            }
            else
            {
                queryDineroAgregado = cs.obtenerDatosTicketAgregarDinero(idDineroAgregado);
            }
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

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            string comentario = dineroAgregadoDT.Rows[0]["Comentarios"].ToString();
            if (string.IsNullOrWhiteSpace(comentario))
            {
                comentario = "";
            }
            reportParameters.Add(new ReportParameter("Comentario", comentario));

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            #region Impresion Ticket de 8 cm (80 mm)
            ReportDataSource rp = new ReportDataSource("TicketDineroAgregado", dineroAgregadoDT);

            this.reportViewer1.LocalReport.DataSources.Add(rp);
            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.RefreshReport();
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            LocalReport rdlc = new LocalReport();
            rdlc.ReportPath = FullReportPath;
            rdlc.DataSources.Add(rp);
            rdlc.SetParameters(reportParameters);

            #endregion

            EnviarImprimir imp = new EnviarImprimir();
            imp.Imprime(rdlc);
            SaldoACaja = false;
            this.Close();
        }
    }
}
