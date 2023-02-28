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
        public string SaldoRestante { get; set; }
        public string saldoAnterior;
        bool sincho = false;
        string idC;

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public visualizadorAbonoPrimero(bool primero,string idCliente)
        {
            InitializeComponent();
            sincho = primero;
            idC = idCliente;
        }

        private void visualizadorAbonoPrimero_Load(object sender, EventArgs e)
        {
            if (sincho)
            {
                imprimirTicketAbonoNoPrimero();
            }
            else
            {
                imprimirTicketAbonoPrimero();
            }
        }

        private void imprimirTicketAbonoNoPrimero()
        {
            string pathApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\AbonosNuevo\AbonoNoPrimero.rdlc";
            string queryVenta = cs.visualizadorTicketAbono(Int32.Parse(idVenta), Int32.Parse(idAbono));
            string siguienteAbono = "ULTIMO ABONO";
            DataTable dtDatos = cn.CargarDatos(queryVenta);

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("RazonSocial", $"{dtDatos.Rows[0]["RazonSocial"]}"));
            reportParameters.Add(new ReportParameter("Domicilio", $"{dtDatos.Rows[0]["Domicilio"]}"));
            reportParameters.Add(new ReportParameter("Cp", $"{dtDatos.Rows[0]["ColyCP"]}"));
            reportParameters.Add(new ReportParameter("RFC", $"{dtDatos.Rows[0]["RFC"]}"));
            reportParameters.Add(new ReportParameter("Email", $"{dtDatos.Rows[0]["Correo"]}"));
            reportParameters.Add(new ReportParameter("Fono", $"{dtDatos.Rows[0]["Telefono"]}"));
            reportParameters.Add(new ReportParameter("Cliente", $"{dtDatos.Rows[0]["ClienteNombre"]}"));
            reportParameters.Add(new ReportParameter("NoVenta", $"{dtDatos.Rows[0]["IDVenta"]}"));
            reportParameters.Add(new ReportParameter("TotalOG", $"{dtDatos.Rows[0]["TotalOriginal"]}"));
            reportParameters.Add(new ReportParameter("SaldoAnterior", $"{saldoAnterior}"));
            reportParameters.Add(new ReportParameter("AbonadoCapital", $"{dtDatos.Rows[0]["CantidadAbonada"]}"));
            reportParameters.Add(new ReportParameter("AbonadoInteres", $"{dtDatos.Rows[0]["CantidadAbonadaInteres"]}"));
            reportParameters.Add(new ReportParameter("Restante", $"{SaldoRestante.ToString()}"));
            reportParameters.Add(new ReportParameter("clienteRFC", $"{dtDatos.Rows[0]["ClienteRFC"]}"));
            reportParameters.Add(new ReportParameter("clienteDomicilio", $"{dtDatos.Rows[0]["ClienteDomicilio"]}"));
            reportParameters.Add(new ReportParameter("clienteCp", $"{dtDatos.Rows[0]["ClienteColoniaCodigoPostal"]}"));
            reportParameters.Add(new ReportParameter("clienteEmail", $"{dtDatos.Rows[0]["ClienteCorreo"]}"));
            reportParameters.Add(new ReportParameter("clienteFono", $"{dtDatos.Rows[0]["ClienteTelefono"]}"));

            using (DataTable dtReglasCreditoVenta = cn.CargarDatos($"SELECT * FROM reglasCreditoVenta WHERE IDVenta = {idVenta}"))
            {
                foreach (var fecha in dtReglasCreditoVenta.Rows[0]["FechaInteres"].ToString().Split('%'))
                {
                    if (DateTime.Parse(fecha) > DateTime.Today)
                    {
                        siguienteAbono = $"Siguiente abono el: {DateTime.Parse(fecha).ToString("yyyy-MM-dd")}";
                        break;
                    }

                }
            }

            reportParameters.Add(new ReportParameter("SiguienteAbono", $"{siguienteAbono}"));

            decimal totallOG;

            using (DataTable dtTotalesCredito = cn.CargarDatos($"SELECT SUM(Total) AS TOTAL FROM ventas WHERE `Status`= 4 AND IDCliente = {idC}"))
            {
                totallOG = Decimal.Parse(dtTotalesCredito.Rows[0]["TOTAl"].ToString());
                reportParameters.Add(new ReportParameter("ECTotalOG", $"${dtTotalesCredito.Rows[0]["TOTAl"]}"));
            }
            
            using (DataTable dtTotalesCredito = cn.CargarDatos($"SELECT ID FROM ventas WHERE `Status`= 4 AND IDCliente = {idC}"))
            {
                reportParameters.Add(new ReportParameter("ECCreditos", $"{dtTotalesCredito.Rows.Count}"));
            }

            using (DataTable dtTotalesCredito = cn.CargarDatos($"SELECT SUM(abonos.Total) AS Total FROM Abonos INNER JOIN Ventas ON ( abonos.IDVenta = ventas.id) WHERE ventas.Status = 4 AND ventas.IDCliente = {idC}"))
            {

                reportParameters.Add(new ReportParameter("ECTotalN", $"${totallOG- Decimal.Parse(dtTotalesCredito.Rows[0]["Total"].ToString())}"));
            }
            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);
            EnviarImprimir imp = new EnviarImprimir();
            imp.Imprime(rdlc);
            this.reportViewer1.RefreshReport();
            this.Close();
        }

        private void imprimirTicketAbonoPrimero()
        {
            string pathApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\AbonosNuevo\TickerAbonoPrimero.rdlc";
            string queryVenta = cs.visualizadorTicketAbono(Int32.Parse(idVenta), Int32.Parse(idAbono));
            string siguienteAbono = "ULTIMO ABONO";
            DataTable dtDatos = cn.CargarDatos(queryVenta);

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("RazonSocial", $"{dtDatos.Rows[0]["RazonSocial"]}"));
            reportParameters.Add(new ReportParameter("Domicilio", $"{dtDatos.Rows[0]["Domicilio"]}"));
            reportParameters.Add(new ReportParameter("Cp", $"{dtDatos.Rows[0]["ColyCP"]}"));
            reportParameters.Add(new ReportParameter("RFC", $"{dtDatos.Rows[0]["RFC"]}"));
            reportParameters.Add(new ReportParameter("Email", $"{dtDatos.Rows[0]["Correo"]}"));
            reportParameters.Add(new ReportParameter("Fono", $"{dtDatos.Rows[0]["Telefono"]}"));
            reportParameters.Add(new ReportParameter("Cliente", $"{dtDatos.Rows[0]["ClienteNombre"]}"));
            reportParameters.Add(new ReportParameter("NoVenta", $"{dtDatos.Rows[0]["IDVenta"]}"));
            reportParameters.Add(new ReportParameter("TotalOG", $"{dtDatos.Rows[0]["TotalOriginal"]}"));
            reportParameters.Add(new ReportParameter("SaldoAnterior", $"{saldoAnterior}"));
            reportParameters.Add(new ReportParameter("AbonadoCapital", $"{dtDatos.Rows[0]["CantidadAbonada"]}"));
            reportParameters.Add(new ReportParameter("AbonadoInteres", $"{dtDatos.Rows[0]["CantidadAbonadaInteres"]}"));
            reportParameters.Add(new ReportParameter("Restante", $"{SaldoRestante.ToString()}"));
            reportParameters.Add(new ReportParameter("FechaOperacion", $"{dtDatos.Rows[0]["FechaUltimoAbono"]}"));
            reportParameters.Add(new ReportParameter("clienteRFC", $"{dtDatos.Rows[0]["ClienteRFC"]}"));
            reportParameters.Add(new ReportParameter("clienteDomicilio", $"{dtDatos.Rows[0]["ClienteDomicilio"]}"));
            reportParameters.Add(new ReportParameter("clienteCp", $"{dtDatos.Rows[0]["ClienteColoniaCodigoPostal"]}"));
            reportParameters.Add(new ReportParameter("clienteEmail", $"{dtDatos.Rows[0]["ClienteCorreo"]}"));
            reportParameters.Add(new ReportParameter("clienteFono", $"{dtDatos.Rows[0]["ClienteTelefono"]}"));

            using (DataTable dtReglasCreditoVenta = cn.CargarDatos($"SELECT * FROM reglasCreditoVenta WHERE IDVenta = {idVenta}"))
            {
                foreach (var fecha in dtReglasCreditoVenta.Rows[0]["FechaInteres"].ToString().Split('%'))
                {
                    if (DateTime.Parse(fecha)>DateTime.Today)
                    {
                        siguienteAbono = $"Siguiente abono el: {DateTime.Parse(fecha).ToString("yyyy-MM-dd")}";
                        break;
                    }
                   
                }
            }

            reportParameters.Add(new ReportParameter("SiguienteAbono", $"{siguienteAbono}"));

            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);
            EnviarImprimir imp = new EnviarImprimir();
            imp.Imprime(rdlc);
            this.reportViewer1.RefreshReport();
            this.Close();
        }
    }
}
