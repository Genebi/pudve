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
    public partial class FormNotaDeVenta : Form
    {
        Consultas cs = new Consultas();
        Conexion cn = new Conexion(); 
        Moneda oMoneda = new Moneda();
        int IDVenta;
        public FormNotaDeVenta(int IDDeLaVEnta)
        {
            InitializeComponent();
            this.IDVenta = IDDeLaVEnta;
        }

        private void FormNotaDeVenta_Load(object sender, EventArgs e)
        {
            
            CargarNotaDeVenta();
        }

        private void CargarNotaDeVenta()
        {
            string cadenaConn = string.Empty;
            string queryVentas = string.Empty;
            

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                cadenaConn = $"datasource={Properties.Settings.Default.Hosting};port=6666;username=root;password=;database=pudve;";
            }
            else
            {
                cadenaConn = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
            }

            queryVentas = cs.PDFNotaDeVentas(IDVenta);



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
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\NotasVentas\ReporteVenta.rdlc";

            MySqlDataAdapter retiroDA = new MySqlDataAdapter(queryVentas, conn);
            DataTable DTNotaDeVentas = new DataTable();
            retiroDA.Fill(DTNotaDeVentas);

            decimal Total = Convert.ToDecimal(DTNotaDeVentas.Rows[0]["total"]);
            string resultado = oMoneda.Convertir(Total.ToString(), true, "PESOS");

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("TotalEnTexto", resultado));

            LocalReport rdlc = new LocalReport();
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource NotasVENTAS = new ReportDataSource("DTNotaVenta", DTNotaDeVentas);

            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.LocalReport.DataSources.Add(NotasVENTAS);
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }
    }
}
