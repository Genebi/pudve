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
    public partial class visualizadorAbonoPrimero : Form
    {
        public string idVenta { get; set; }
        public string idAbono { get; set; }
        public int SaldoRestante { get; set; }

        Consultas cs = new Consultas();

        public visualizadorAbonoPrimero(string[] datos)
        {
            InitializeComponent();
        }

        private void visualizadorAbonoPrimero_Load(object sender, EventArgs e)
        {
            imprimirTicketAbonoPrimero();
        }

        private void imprimirTicketAbonoPrimero()
        {

            string pathApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\AbonosNuevo\TickerAbonoPrimero.rdlc";

         LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;

            EnviarImprimir imp = new EnviarImprimir();
            imp.Imprime(rdlc);
            this.reportViewer1.RefreshReport();
            this.Close();
        }
    }
}
