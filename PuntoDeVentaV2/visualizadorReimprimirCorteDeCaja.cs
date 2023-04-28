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
    public partial class visualizadorReimprimirCorteDeCaja : Form
    {
        public DataTable dtEncabezado { get; set; }
        public DataTable dtVentasRealizadas { get; set; }
        public DataTable dtAnticiposRecibidos { get; set; }
        public DataTable dtDineroAgregado { get; set; }
        public DataTable dtDineroRetirado { get; set; }
        public DataTable dtTotalCorteDeCaja { get; set; }
        public DataTable dtDineroAgregado2 { get; set; }
        public DataTable dtDineroRetirado2 { get; set; }
        public DataTable dtTotalAgregado { get; set; }
        public DataTable dtTotalRetirado { get; set; }

        public visualizadorReimprimirCorteDeCaja()
        {
            InitializeComponent();
        }

        private void visualizadorReimprimirCorteDeCaja_Load(object sender, EventArgs e)
        {
            cargaDatosReporte();
        }

        private void cargaDatosReporte()
        {
            string pathApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\ReimprimirCorteDeCaja\CorteDeCaja.rdlc";

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource Encabezado = new ReportDataSource("DTEncabezado", dtEncabezado); 
            ReportDataSource VentasRealizadas = new ReportDataSource("DTVentasRealizadas", dtVentasRealizadas);
            ReportDataSource AnticiposRecibidos = new ReportDataSource("DTAnticiposRecibidos", dtAnticiposRecibidos);
            ReportDataSource DepositosRecibidos = new ReportDataSource("DTDineroAgregado", dtDineroAgregado);
            ReportDataSource RetirosRealizados = new ReportDataSource("DTDineroRetirado", dtDineroRetirado);
            ReportDataSource TotalDeCaja = new ReportDataSource("DTTotalDeCaja", dtTotalCorteDeCaja);
            ReportDataSource DepositosRecibidos2 = new ReportDataSource("DTDineroAgregado2", dtDineroAgregado2);
            ReportDataSource RetirosRealizados2 = new ReportDataSource("DTDineroRetirado2", dtDineroRetirado2);
            ReportDataSource totalesAgregados = new ReportDataSource("DTTotalesAgregados", dtTotalAgregado);
            ReportDataSource totalesRetirados = new ReportDataSource("DTTotalesRetirados", dtTotalRetirado);

            this.reportViewer1.LocalReport.DataSources.Add(Encabezado);
            this.reportViewer1.LocalReport.DataSources.Add(VentasRealizadas);
            this.reportViewer1.LocalReport.DataSources.Add(AnticiposRecibidos);
            this.reportViewer1.LocalReport.DataSources.Add(DepositosRecibidos);
            this.reportViewer1.LocalReport.DataSources.Add(RetirosRealizados);
            this.reportViewer1.LocalReport.DataSources.Add(TotalDeCaja);
            this.reportViewer1.LocalReport.DataSources.Add(DepositosRecibidos2);
            this.reportViewer1.LocalReport.DataSources.Add(RetirosRealizados2);
            this.reportViewer1.LocalReport.DataSources.Add(totalesAgregados);
            this.reportViewer1.LocalReport.DataSources.Add(totalesRetirados);

            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.RefreshReport();
        }
    }
}
