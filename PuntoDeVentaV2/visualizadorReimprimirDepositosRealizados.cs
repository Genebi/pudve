using Microsoft.Reporting.WinForms;
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
    public partial class visualizadorReimprimirDepositosRealizados : Form
    {
        public DataTable dtEncabezado { get; set; }
        public DataTable dtDepositosRealizados { get; set; }
        public DataTable dtSumaDepositosRealizados { get; set; }

        public visualizadorReimprimirDepositosRealizados()
        {
            InitializeComponent();
        }

        private void visualizadorReimprimirDepositosRealizados_Load(object sender, EventArgs e)
        {
            cargaDatosReporte();
        }

        private void cargaDatosReporte()
        {
            string pathApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\ReimprimirDepositosRealizados\DepositosRealizados.rdlc";

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource Encabezado = new ReportDataSource("DTEncabezado", dtEncabezado);
            ReportDataSource Depositos = new ReportDataSource("DTDepositos", dtDepositosRealizados);
            ReportDataSource SumaDepositos = new ReportDataSource("DTSumaDepositos", dtSumaDepositosRealizados);

            this.reportViewer1.LocalReport.DataSources.Add(Encabezado);
            this.reportViewer1.LocalReport.DataSources.Add(Depositos);
            this.reportViewer1.LocalReport.DataSources.Add(SumaDepositos);

            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.RefreshReport();
        }
    }
}
