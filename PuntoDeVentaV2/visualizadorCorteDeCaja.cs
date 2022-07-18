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
    public partial class visualizadorCorteDeCaja : Form
    {
        Consultas cs = new Consultas();
        Conexion cn = new Conexion();

        public string conceptoEfectivoDeVentas { get; set; }
        public string conceptoTarjetaDeVentas { get; set; }
        public string conceptoValeDeVentas { get; set; }
        public string conceptoChequeDeVentas { get; set; }
        public string conceptoTransferenciDeVentas { get; set; }
        public string conceptoCreditoDeVentas { get; set; }
        public string conceptoAbonosDeVentas { get; set; }
        public string conceptoAnticiposUtilizados { get; set; }

        public visualizadorCorteDeCaja()
        {
            InitializeComponent();
        }

        private void visualizadorCorteDeCaja_Load(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
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

            string pathApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\CorteDeCaja\ReporteTicketCorteDeCaja.rdlc";

            #region Impresion Ticket de 80 mm
            ReportParameterCollection reportParameters = new ReportParameterCollection();

            //01 parametro integer para mostrar / ocultar Logo
            reportParameters.Add(new ReportParameter("conceptoEfectivoDeVentas", conceptoEfectivoDeVentas.ToString()));
            //02 parametro integer para mostrar / ocultar Nombre
            reportParameters.Add(new ReportParameter("conceptoTarjetaDeVentas", conceptoTarjetaDeVentas.ToString()));
            //03 parametro integer para mostrar / ocultar Nombre Comercial
            reportParameters.Add(new ReportParameter("conceptoValeDeVentas", conceptoValeDeVentas.ToString()));
            //04 parametro integer para mostrar / ocultar Direccion Ciudad
            reportParameters.Add(new ReportParameter("conceptoChequeDeVentas", conceptoChequeDeVentas.ToString()));
            //05 parametro integer para mostrar / ocultar Colonia Codigo Postal
            reportParameters.Add(new ReportParameter("conceptoTransferenciDeVentas", conceptoTransferenciDeVentas.ToString()));
            //06 parametro integer para mostrar / ocultar RFC
            reportParameters.Add(new ReportParameter("conceptoCreditoDeVentas", conceptoCreditoDeVentas.ToString()));
            //07 parametro integer para mostrar / ocultar Correo
            reportParameters.Add(new ReportParameter("conceptoAbonosDeVentas", conceptoAbonosDeVentas.ToString()));
            //08 parametro integer para mostrar / ocultar Telefono
            reportParameters.Add(new ReportParameter("conceptoAnticiposUtilizados", conceptoAnticiposUtilizados.ToString()));

            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);
            #endregion

            EnviarImprimir imp = new EnviarImprimir();
            imp.Imprime(rdlc);

            this.Close();
        }
    }
}
