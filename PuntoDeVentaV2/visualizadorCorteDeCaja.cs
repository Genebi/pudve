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

        #region Variables globales

        #region Tabla de Ventas
        public string conceptoEfectivoDeVentas { get; set; }
        public string conceptoTarjetaDeVentas { get; set; }
        public string conceptoValeDeVentas { get; set; }
        public string conceptoChequeDeVentas { get; set; }
        public string conceptoTransferenciDeVentas { get; set; }
        public string conceptoCreditoDeVentas { get; set; }
        public string conceptoAbonosDeVentas { get; set; }
        public string conceptoAnticiposUtilizados { get; set; }
        #endregion
        #region Tabla de Anticipos
        public string conceptoEfectivoDeAnticipos { get; set; }
        public string conceptoTarjetaDeAnticipos { get; set; }
        public string conceptoValeDeAnticipos { get; set; }
        public string conceptoChequeDeAnticipos { get; set; }
        public string conceptoTransferenciaDeAnticipos { get; set; }
        #endregion
        #region Tabla de Dinero Agregado
        public string conceptoEfectivoDeDineroAgregado { get; set; }
        public string conceptoTarjetaDeDineroAgregado { get; set; }
        public string conceptoValeDeDineroAgregado { get; set; }
        public string conceptoChequeDeDineroAgregado { get; set; }
        public string conceptoTransferenciaDeDineroAgregado { get; set; }
        #endregion
        #region Tabla de Dinero Retirado
        public string conceptoEfectivoDeDineroRetirado { get; set; }
        public string conceptoTarjetaDeDineroRetirado { get; set; }
        public string conceptoValeDeDineroRetirado { get; set; }
        public string conceptoChequeDeDineroRetirado { get; set; }
        public string conceptoTransferenciaDeDineroRetirado { get; set; }
        public string conceptoDevolucionDeDineroRetirado { get; set; }
        #endregion
        #region Tabla de Total de Caja
        public string conceptoEfectivoDeTotalCaja { get; set; }
        public string conceptoTarjetaDeTotalCaja { get; set; }
        public string conceptoValeDeTotalCaja { get; set; }
        public string conceptoChequeDeTotalCaja { get; set; }
        public string conceptoTransferenciaDeTotalCaja { get; set; }
        public string conceptoSaldoInicialDeTotalCaja { get; set; }
        #endregion
        #region Monto antes del corte
        public string conceptoCantidadEnCajaAntesDelCorte { get; set; }
        #endregion
        #region Cantidad retirada en el corte
        public string conceptoCantidadRetiradaAlCorteDeCaja { get; set; }
        #endregion
        #region Total de ventas
        public string conceptoTotalVentas { get; set; }
        #endregion
        #region Total de Anticipos
        public string conceptoTotalAnticipos { get; set; }
        #endregion
        #region Total de Dinero Agregado
        public string conceptoTotalDineroAgregado { get; set; }
        #endregion
        #region Total de Dinero Retirado
        public string conceptoTotalDineroRetirado { get; set; }
        #endregion
        #region Total de Restante al Corte de Caja
        public string conceptoRestanteCorteCaja { get; set; }
        #endregion
        #region Nombre de Usuario
        public string nombreUsuario { get; set; }
        #endregion
        #region Nombre de Empleado
        public string nombreEmpleado { get; set; }
        #endregion
        #region Número de folio
        public string numFolio { get; set; }
        #endregion
        #region Fecha de corte de caja
        public string fechaCorteCaja { get; set; }
        #endregion
        #region id Corte de Caja
        public int idPenultimoCorteDeCaja { get; set; }
        #endregion

        #endregion

        public visualizadorCorteDeCaja()
        {
            InitializeComponent();
        }

        private void visualizadorCorteDeCaja_Load(object sender, EventArgs e)
        {
            reporteCarta();
        }

        private void reporteTicket()
        {
            string cadenaConn = string.Empty;
            
            string pathApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\CorteDeCaja\ReporteTicketCorteDeCaja.rdlc";

            #region Impresion Ticket de 80 mm
            ReportParameterCollection reportParameters = new ReportParameterCollection();

            #region tabla Ventas
            reportParameters.Add(new ReportParameter("conceptoEfectivoDeVentas", conceptoEfectivoDeVentas.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTarjetaDeVentas", conceptoTarjetaDeVentas.ToString()));
            reportParameters.Add(new ReportParameter("conceptoValeDeVentas", conceptoValeDeVentas.ToString()));
            reportParameters.Add(new ReportParameter("conceptoChequeDeVentas", conceptoChequeDeVentas.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTransferenciDeVentas", conceptoTransferenciDeVentas.ToString()));
            reportParameters.Add(new ReportParameter("conceptoCreditoDeVentas", conceptoCreditoDeVentas.ToString()));
            reportParameters.Add(new ReportParameter("conceptoAbonosDeVentas", conceptoAbonosDeVentas.ToString()));
            reportParameters.Add(new ReportParameter("conceptoAnticiposUtilizados", conceptoAnticiposUtilizados.ToString()));
            #endregion
            #region tabla Anticipos
            reportParameters.Add(new ReportParameter("conceptoEfectivoDeAnticipos", conceptoEfectivoDeAnticipos.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTarjetaDeAnticipos", conceptoTarjetaDeAnticipos.ToString()));
            reportParameters.Add(new ReportParameter("conceptoValeDeAnticipos", conceptoValeDeAnticipos.ToString()));
            reportParameters.Add(new ReportParameter("conceptoChequeDeAnticipos", conceptoChequeDeAnticipos.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTransferenciaDeAnticipos", conceptoTransferenciaDeAnticipos.ToString()));
            #endregion
            #region tabla Depositos
            reportParameters.Add(new ReportParameter("conceptoEfectivoDeDineroAgregado", conceptoEfectivoDeDineroAgregado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTarjetaDeDineroAgregado", conceptoTarjetaDeDineroAgregado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoValeDeDineroAgregado", conceptoValeDeDineroAgregado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoChequeDeDineroAgregado", conceptoChequeDeDineroAgregado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTransferenciaDeDineroAgregado", conceptoTransferenciaDeDineroAgregado.ToString()));
            #endregion
            #region tabla Retiros
            reportParameters.Add(new ReportParameter("conceptoEfectivoDeDineroRetirado", conceptoEfectivoDeDineroRetirado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTarjetaDeDineroRetirado", conceptoTarjetaDeDineroRetirado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoValeDeDineroRetirado", conceptoValeDeDineroRetirado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoChequeDeDineroRetirado", conceptoChequeDeDineroRetirado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTransferenciaDeDineroRetirado", conceptoTransferenciaDeDineroRetirado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoDevolucionDeDineroRetirado", conceptoDevolucionDeDineroRetirado.ToString()));
            #endregion
            #region tabla Total de caja
            reportParameters.Add(new ReportParameter("conceptoEfectivoDeTotalCaja", conceptoEfectivoDeTotalCaja.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTarjetaDeTotalCaja", conceptoTarjetaDeTotalCaja.ToString()));
            reportParameters.Add(new ReportParameter("conceptoValeDeTotalCaja", conceptoValeDeTotalCaja.ToString()));
            reportParameters.Add(new ReportParameter("conceptoChequeDeTotalCaja", conceptoChequeDeTotalCaja.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTransferenciaDeTotalCaja", conceptoTransferenciaDeTotalCaja.ToString()));
            reportParameters.Add(new ReportParameter("conceptoSaldoInicialDeTotalCaja", conceptoSaldoInicialDeTotalCaja.ToString()));
            #endregion
            #region Monto antes del corte
            reportParameters.Add(new ReportParameter("conceptoCantidadEnCajaAntesDelCorte", conceptoCantidadEnCajaAntesDelCorte.ToString()));
            #endregion
            #region Cantidad retirada en el corte
            reportParameters.Add(new ReportParameter("conceptoCantidadRetiradaAlCorteDeCaja", conceptoCantidadRetiradaAlCorteDeCaja.ToString()));
            #endregion
            #region Total de ventas
            reportParameters.Add(new ReportParameter("conceptoTotalVentas", conceptoTotalVentas.ToString()));
            #endregion
            #region Total de anticipos
            reportParameters.Add(new ReportParameter("conceptoTotalAnticipos", conceptoTotalAnticipos.ToString()));
            #endregion
            #region Total de depositos
            reportParameters.Add(new ReportParameter("conceptoTotalDineroAgregado", conceptoTotalDineroAgregado.ToString()));
            #endregion
            #region Total de retiros
            reportParameters.Add(new ReportParameter("conceptoTotalDineroRetirado", conceptoTotalDineroRetirado.ToString()));
            #endregion
            #region Restante al corte de caja
            reportParameters.Add(new ReportParameter("conceptoRestanteCorteCaja", conceptoRestanteCorteCaja.ToString()));
            #endregion
            #region Nombre de Usuario
            reportParameters.Add(new ReportParameter("nombreUsuario", nombreUsuario.ToString()));
            #endregion
            #region Nombre de Empleado
            reportParameters.Add(new ReportParameter("nombreEmpleado", nombreEmpleado.ToString()));
            #endregion
            #region Número de folio
            reportParameters.Add(new ReportParameter("numFolio", numFolio.ToString()));
            #endregion
            #region Fecha de corte de caja
            reportParameters.Add(new ReportParameter("fechaCorteCaja", fechaCorteCaja.ToString()));
            #endregion

            LocalReport rdlc = new LocalReport();
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);
            #endregion

            EnviarImprimir imp = new EnviarImprimir();
            imp.Imprime(rdlc);

            this.Close();
        }

        private void reporteCarta()
        {
            string cadenaConn = string.Empty;
            string queryDepositos = string.Empty;
            string querySumaDepositos = string.Empty;
            string queryRetiros = string.Empty;
            string querySumaRetiros = string.Empty;

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                cadenaConn = $"datasource={Properties.Settings.Default.Hosting};port=6666;username=root;password=;database=pudve;";
            }
            else
            {
                cadenaConn = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
            }

            if (!FormPrincipal.userNickName.Contains("@"))
            {
                queryDepositos = cs.HistorialDepositosAdminsitrador(idPenultimoCorteDeCaja);
                querySumaDepositos = cs.cargarHistorialdepositosAdministradorSumaTotal(idPenultimoCorteDeCaja);

                queryRetiros = cs.HistorialRetirosAdminsitrador(idPenultimoCorteDeCaja);
                querySumaRetiros = cs.cargarHistorialRetirosAdministradorSumaTotal(idPenultimoCorteDeCaja);
            }
            else if (FormPrincipal.userNickName.Contains("@"))
            {
                queryDepositos = cs.HistorialDepositosEmpleado(idPenultimoCorteDeCaja, FormPrincipal.id_empleado);
                querySumaDepositos = cs.cargarHistorialdepositosEmpleadoSumaTotal(idPenultimoCorteDeCaja, FormPrincipal.id_empleado);

                queryRetiros = cs.HistorialRetirosEmpleado(idPenultimoCorteDeCaja, FormPrincipal.id_empleado);
                querySumaRetiros = cs.cargarHistorialRetirosEmpleadoSumaTotal(idPenultimoCorteDeCaja, FormPrincipal.id_empleado);
            }

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
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\CorteDeCaja\ReporteCorteDeCaja.rdlc";

            MySqlDataAdapter depositoDA = new MySqlDataAdapter(queryDepositos, conn);
            DataTable depositoDT = new DataTable();
            depositoDA.Fill(depositoDT);

            MySqlDataAdapter sumaDepositosDA = new MySqlDataAdapter(querySumaDepositos, conn);
            DataTable sumaDepositosDT = new DataTable();
            sumaDepositosDA.Fill(sumaDepositosDT);

            MySqlDataAdapter retiroDA = new MySqlDataAdapter(queryRetiros, conn);
            DataTable retiroDT = new DataTable();
            retiroDA.Fill(retiroDT);

            MySqlDataAdapter sumaRetirosDA = new MySqlDataAdapter(querySumaRetiros, conn);
            DataTable sumaRetirosDT = new DataTable();
            sumaRetirosDA.Fill(sumaRetirosDT);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource depositos = new ReportDataSource("DSDepositos", depositoDT);
            ReportDataSource sumaDepositos = new ReportDataSource("DSSumaDepositos", sumaDepositosDT);

            ReportDataSource retiros = new ReportDataSource("DSRetiros", retiroDT);
            ReportDataSource sumaRetiros = new ReportDataSource("DSSumaRetiros", sumaRetirosDT);

            #region Impresion Ticket de 80 mm
            ReportParameterCollection reportParameters = new ReportParameterCollection();

            #region tabla Ventas
            reportParameters.Add(new ReportParameter("conceptoEfectivoDeVentas", conceptoEfectivoDeVentas.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTarjetaDeVentas", conceptoTarjetaDeVentas.ToString()));
            reportParameters.Add(new ReportParameter("conceptoValeDeVentas", conceptoValeDeVentas.ToString()));
            reportParameters.Add(new ReportParameter("conceptoChequeDeVentas", conceptoChequeDeVentas.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTransferenciDeVentas", conceptoTransferenciDeVentas.ToString()));
            reportParameters.Add(new ReportParameter("conceptoCreditoDeVentas", conceptoCreditoDeVentas.ToString()));
            reportParameters.Add(new ReportParameter("conceptoAbonosDeVentas", conceptoAbonosDeVentas.ToString()));
            reportParameters.Add(new ReportParameter("conceptoAnticiposUtilizados", conceptoAnticiposUtilizados.ToString()));
            #endregion
            #region tabla Anticipos
            reportParameters.Add(new ReportParameter("conceptoEfectivoDeAnticipos", conceptoEfectivoDeAnticipos.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTarjetaDeAnticipos", conceptoTarjetaDeAnticipos.ToString()));
            reportParameters.Add(new ReportParameter("conceptoValeDeAnticipos", conceptoValeDeAnticipos.ToString()));
            reportParameters.Add(new ReportParameter("conceptoChequeDeAnticipos", conceptoChequeDeAnticipos.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTransferenciaDeAnticipos", conceptoTransferenciaDeAnticipos.ToString()));
            #endregion
            #region tabla Depositos
            reportParameters.Add(new ReportParameter("conceptoEfectivoDeDineroAgregado", conceptoEfectivoDeDineroAgregado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTarjetaDeDineroAgregado", conceptoTarjetaDeDineroAgregado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoValeDeDineroAgregado", conceptoValeDeDineroAgregado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoChequeDeDineroAgregado", conceptoChequeDeDineroAgregado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTransferenciaDeDineroAgregado", conceptoTransferenciaDeDineroAgregado.ToString()));
            #endregion
            #region tabla Retiros
            reportParameters.Add(new ReportParameter("conceptoEfectivoDeDineroRetirado", conceptoEfectivoDeDineroRetirado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTarjetaDeDineroRetirado", conceptoTarjetaDeDineroRetirado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoValeDeDineroRetirado", conceptoValeDeDineroRetirado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoChequeDeDineroRetirado", conceptoChequeDeDineroRetirado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTransferenciaDeDineroRetirado", conceptoTransferenciaDeDineroRetirado.ToString()));
            reportParameters.Add(new ReportParameter("conceptoDevolucionDeDineroRetirado", conceptoDevolucionDeDineroRetirado.ToString()));
            #endregion
            #region tabla Total de caja
            reportParameters.Add(new ReportParameter("conceptoEfectivoDeTotalCaja", conceptoEfectivoDeTotalCaja.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTarjetaDeTotalCaja", conceptoTarjetaDeTotalCaja.ToString()));
            reportParameters.Add(new ReportParameter("conceptoValeDeTotalCaja", conceptoValeDeTotalCaja.ToString()));
            reportParameters.Add(new ReportParameter("conceptoChequeDeTotalCaja", conceptoChequeDeTotalCaja.ToString()));
            reportParameters.Add(new ReportParameter("conceptoTransferenciaDeTotalCaja", conceptoTransferenciaDeTotalCaja.ToString()));
            reportParameters.Add(new ReportParameter("conceptoSaldoInicialDeTotalCaja", conceptoSaldoInicialDeTotalCaja.ToString()));
            #endregion
            #region Monto antes del corte
            reportParameters.Add(new ReportParameter("conceptoCantidadEnCajaAntesDelCorte", conceptoCantidadEnCajaAntesDelCorte.ToString()));
            #endregion
            #region Cantidad retirada en el corte
            reportParameters.Add(new ReportParameter("conceptoCantidadRetiradaAlCorteDeCaja", conceptoCantidadRetiradaAlCorteDeCaja.ToString()));
            #endregion
            #region Total de ventas
            reportParameters.Add(new ReportParameter("conceptoTotalVentas", conceptoTotalVentas.ToString()));
            #endregion
            #region Total de anticipos
            reportParameters.Add(new ReportParameter("conceptoTotalAnticipos", conceptoTotalAnticipos.ToString()));
            #endregion
            #region Total de depositos
            reportParameters.Add(new ReportParameter("conceptoTotalDineroAgregado", conceptoTotalDineroAgregado.ToString()));
            #endregion
            #region Total de retiros
            reportParameters.Add(new ReportParameter("conceptoTotalDineroRetirado", conceptoTotalDineroRetirado.ToString()));
            #endregion
            #region Restante al corte de caja
            reportParameters.Add(new ReportParameter("conceptoRestanteCorteCaja", conceptoRestanteCorteCaja.ToString()));
            #endregion
            #region Nombre de Usuario
            reportParameters.Add(new ReportParameter("nombreUsuario", nombreUsuario.ToString()));
            #endregion
            #region Nombre de Empleado
            reportParameters.Add(new ReportParameter("nombreEmpleado", nombreEmpleado.ToString()));
            #endregion
            #region Número de folio
            reportParameters.Add(new ReportParameter("numFolio", numFolio.ToString()));
            #endregion
            #region Fecha de corte de caja
            reportParameters.Add(new ReportParameter("fechaCorteCaja", fechaCorteCaja.ToString()));
            #endregion
            #region filas de depositos en la tabla
            reportParameters.Add(new ReportParameter("CantidadDSDepositos", depositoDT.Rows.Count.ToString()));
            #endregion
            #region filas de retiros en la tabla
            reportParameters.Add(new ReportParameter("CantidadDSRetiros", retiroDT.Rows.Count.ToString()));
            #endregion

            
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.LocalReport.DataSources.Add(depositos);
            this.reportViewer1.LocalReport.DataSources.Add(sumaDepositos);
            this.reportViewer1.LocalReport.DataSources.Add(retiros);
            this.reportViewer1.LocalReport.DataSources.Add(sumaRetiros);
            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.RefreshReport();
            #endregion
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            reporteTicket();
        }
    }
}
