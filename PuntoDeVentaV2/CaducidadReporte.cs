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
    public partial class CaducidadReporte : Form
    {
        DataTable productosCad = new DataTable();
        public CaducidadReporte(DataTable data)
        {
            InitializeComponent();
            productosCad =data;
        }

        private void CaducidadReporte_Load(object sender, EventArgs e)
        {
            Conexion cn = new Conexion();

            string pathApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\Caducidad\ReporteCaducidad.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            using (DataTable datosUser = cn.CargarDatos($"SELECT * FROM Usuarios WHERE ID = {FormPrincipal.userID}"))
            {
                reportParameters.Add(new ReportParameter("RazonSocial", datosUser.Rows[0]["RazonSocial"].ToString()));
                reportParameters.Add(new ReportParameter("RFC", datosUser.Rows[0]["RFC"].ToString()));
                reportParameters.Add(new ReportParameter("Email", datosUser.Rows[0]["Email"].ToString()));
                reportParameters.Add(new ReportParameter("Telefono", datosUser.Rows[0]["Telefono"].ToString()));
            }

            LocalReport rdlc = new LocalReport();
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);
            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.SetParameters(reportParameters);

            ReportDataSource NotasVENTAS = new ReportDataSource("DTCaducidad", productosCad);
            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.LocalReport.DataSources.Add(NotasVENTAS);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.RefreshReport();

        }
    }
}
