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
    public partial class VerTicketCredito8cmListadoVentas : Form
    {
        Consultas cs = new Consultas();
        Conexion cn = new Conexion();

        #region Parametros para el archivo RDLC
        public int Logo { get; set; }
        public int Nombre { get; set; }
        public int NombreComercial { get; set; }
        public int DireccionCiudad { get; set; }
        public int ColoniaCodigoPostal { get; set; }
        public int RFC { get; set; }
        public int Correo { get; set; }
        public int Telefono { get; set; }
        public int NombreCliente { get; set; }
        public int RFCCliente { get; set; }
        public int DomicilioCliente { get; set; }
        public int ColoniaCodigoPostalCliente { get; set; }
        public int CorreoCliente { get; set; }
        public int TelefonoCliente { get; set; }
        public int FormaDePagoCliente { get; set; }
        public int CodigoBarra { get; set; }
        public int Referencia { get; set; }
        #endregion

        public int idVentaRealizada { get; set; }

        public VerTicketCredito8cmListadoVentas()
        {
            InitializeComponent();
        }

        private void VerTicketCredito8cmListadoVentas_Load(object sender, EventArgs e)
        {
            CargarDatosTicket();
        }

        private void CargarDatosTicket()
        {
            var servidor = Properties.Settings.Default.Hosting;
            string cadenaConn = string.Empty;
            string queryCreditoOtorgado = cs.imprimirTicketCredito(idVentaRealizada);
            MySqlConnection conn = new MySqlConnection();
            string pathApplication = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\CreditoRealizado\ReporteTicketCredito80mm.rdlc";
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

            MySqlDataAdapter creditoDA = new MySqlDataAdapter(queryCreditoOtorgado, conn);
            DataTable creditoDT = new DataTable();

            creditoDA.Fill(creditoDT);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            #region Impresion Ticket de 8 cm (80 mm)
            ReportDataSource rp = new ReportDataSource("TicketCredito", creditoDT);

            this.reportViewer1.LocalReport.EnableExternalImages = true;

            ReportParameterCollection reportParameters = new ReportParameterCollection();

            if (!Directory.Exists(pathBarCode))
            {
                Directory.CreateDirectory(pathBarCode);
            }

            using (DataTable dtFolioVenta = cn.CargarDatos(cs.folioDeLaVentaParaElTicket(idVentaRealizada)))
            {
                if (!dtFolioVenta.Rows.Count.Equals(0))
                {
                    DataRow drFolioVenta = dtFolioVenta.Rows[0];
                    folioVentaRealizada = drFolioVenta["Folio"].ToString();
                }
            }

            var codigoBarraTicket = GenerarCodigoBarras(folioVentaRealizada, 170);
            codigoBarraTicket.Save($"{pathBarCode}{folioVentaRealizada}.png");

            var pathBarCodeFull = new Uri($"C:/Archivos PUDVE/Ventas/Tickets/BarCode/{folioVentaRealizada}.png").AbsoluteUri;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                var logoTipo = string.Empty;

                using (DataTable dtLogoTipo = cn.CargarDatos(cs.getLogoTipoUsuario()))
                {
                    if (!dtLogoTipo.Rows.Count.Equals(0))
                    {
                        DataRow drLogoTipoUsuario = dtLogoTipo.Rows[0];
                        logoTipo = drLogoTipoUsuario["LogoTipo"].ToString();
                    }
                }

                if (!string.IsNullOrWhiteSpace(logoTipo))
                {
                    string fileName = logoTipo;
                    string sourcePath = $@"\\{servidor}\Archivos PUDVE\MisDatos\Usuarios\";
                    string targetPath = @"C:\Archivos PUDVE\Ventas\Tickets\BarCode\";

                    // Use Path class to manipulate file and directory paths.
                    string sourceFile = Path.Combine(sourcePath, fileName);
                    string destFile = Path.Combine(targetPath, fileName);
                    finalLogoTipoPath = destFile;

                    // To copy a file to another location and
                    // overwrite the destination file if it already exists.
                    File.Copy(sourceFile, destFile, true);
                }

                path = new Uri($"C:/Archivos PUDVE/Ventas/Tickets/BarCode/").AbsoluteUri;
            }
            else
            {
                path = new Uri("C:/Archivos PUDVE/MisDatos/Usuarios/").AbsoluteUri;
            }

            //01 parametro integer para mostrar / ocultar Logo
            reportParameters.Add(new ReportParameter("Logo", Logo.ToString()));
            //02 parametro integer para mostrar / ocultar Nombre
            reportParameters.Add(new ReportParameter("Nombre", Nombre.ToString()));
            //03 parametro integer para mostrar / ocultar Nombre Comercial
            reportParameters.Add(new ReportParameter("NombreComercial", NombreComercial.ToString()));
            //04 parametro integer para mostrar / ocultar Direccion Ciudad
            reportParameters.Add(new ReportParameter("DireccionCiudad", DireccionCiudad.ToString()));
            //05 parametro integer para mostrar / ocultar Colonia Codigo Postal
            reportParameters.Add(new ReportParameter("ColoniaCodigoPostal", ColoniaCodigoPostal.ToString()));
            //06 parametro integer para mostrar / ocultar RFC
            reportParameters.Add(new ReportParameter("RFC", RFC.ToString()));
            //07 parametro integer para mostrar / ocultar Correo
            reportParameters.Add(new ReportParameter("Correo", Correo.ToString()));
            //08 parametro integer para mostrar / ocultar Telefono
            reportParameters.Add(new ReportParameter("Telefono", Telefono.ToString()));
            //09 parametro integer para mostrar / ocultar Nombre Cliente
            reportParameters.Add(new ReportParameter("NombreCliente", NombreCliente.ToString()));
            //10 parametro integer para mostrar / ocultar RFC Cliente
            reportParameters.Add(new ReportParameter("RFCCliente", RFCCliente.ToString()));
            //11 parametro integer para mostrar / ocultar Domicilio Cliente
            reportParameters.Add(new ReportParameter("DomicilioCliente", DomicilioCliente.ToString()));
            //12 parametro integer para mostrar / ocultar Colonia Codigo Postal Cliente
            reportParameters.Add(new ReportParameter("ColoniaCodigoPostalCliente", ColoniaCodigoPostalCliente.ToString()));
            //13 parametro integer para mostrar / ocultar Correo Ciente
            reportParameters.Add(new ReportParameter("CorreoCliente", CorreoCliente.ToString()));
            //14 parametro integer para mostrar / ocultar Telefono Cliente
            reportParameters.Add(new ReportParameter("TelefonoCliente", TelefonoCliente.ToString()));
            //15 parametro integer para mostrar / ocultar Forma De Pago Cliente
            reportParameters.Add(new ReportParameter("FormaDePagoCliente", FormaDePagoCliente.ToString()));
            //16 parametro integer para mostrar / ocultar Codigo Barra
            reportParameters.Add(new ReportParameter("CodigoBarra", CodigoBarra.ToString()));
            //17 parametro string para mostrar / ocultar Path Logo
            reportParameters.Add(new ReportParameter("PathLogo", path));
            //18 parametro string para mostrar / ocultar Codigo de Barras
            reportParameters.Add(new ReportParameter("PathBarCode", pathBarCodeFull));
            //19 parametro integer para mostrar / ocultar Referencia
            reportParameters.Add(new ReportParameter("Referencia", Referencia.ToString()));

            string UsuarioRealizoVenta = string.Empty;

            using (var DTUsuario = cn.CargarDatos($"SELECT VEN.IDEmpleado, EMP.usuario FROM VENTAS AS VEN INNER JOIN empleados AS EMP ON( EMP.ID = VEN.IDEmpleado) WHERE VEN.ID = {idVentaRealizada} AND VEN.IDUsuario = {FormPrincipal.userID}"))
            {
                if (string.IsNullOrWhiteSpace(DTUsuario.Rows[0][0].ToString()))
                {
                    UsuarioRealizoVenta = FormPrincipal.userNickName;
                }
                else
                {
                    UsuarioRealizoVenta = DTUsuario.Rows[0][1].ToString();
                }
            }
            reportParameters.Add(new ReportParameter("Usuario", UsuarioRealizoVenta));
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.LocalReport.DataSources.Add(rp);
            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.RefreshReport();
            #endregion
        }

        private Image GenerarCodigoBarras(string txtCodigo, int ancho)
        {
            Image imagen;

            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();

            try
            {
                var anchoTmp = ancho / 2;
                var auxiliar = anchoTmp;

                anchoTmp = auxiliar / 2;
                ancho = ancho - anchoTmp;

                imagen = codigo.Encode(BarcodeLib.TYPE.CODE128, txtCodigo, Color.Black, Color.White, ancho, 40);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar código de barras para el ticket", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                imagen = null;
            }

            return imagen;
        }

        private void btnImprimrCredito_Click(object sender, EventArgs e)
        {
            var servidor = Properties.Settings.Default.Hosting;
            string cadenaConn = string.Empty;
            string queryCreditoOtorgado = cs.imprimirTicketCredito(idVentaRealizada);
            MySqlConnection conn = new MySqlConnection();
            string pathApplication = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\CreditoRealizado\ReporteTicketCredito80mm.rdlc";
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

            MySqlDataAdapter creditoDA = new MySqlDataAdapter(queryCreditoOtorgado, conn);
            DataTable creditoDT = new DataTable();

            creditoDA.Fill(creditoDT);

            #region Impresion Ticket de 8 cm (80 mm)
            ReportDataSource rp = new ReportDataSource("TicketCredito", creditoDT);

            ReportParameterCollection reportParameters = new ReportParameterCollection();

            if (!Directory.Exists(pathBarCode))
            {
                Directory.CreateDirectory(pathBarCode);
            }

            using (DataTable dtFolioVenta = cn.CargarDatos(cs.folioDeLaVentaParaElTicket(idVentaRealizada)))
            {
                if (!dtFolioVenta.Rows.Count.Equals(0))
                {
                    DataRow drFolioVenta = dtFolioVenta.Rows[0];
                    folioVentaRealizada = drFolioVenta["Folio"].ToString();
                }
            }

            var codigoBarraTicket = GenerarCodigoBarras(folioVentaRealizada, 170);
            codigoBarraTicket.Save($"{pathBarCode}{folioVentaRealizada}.png");

            var pathBarCodeFull = new Uri($"C:/Archivos PUDVE/Ventas/Tickets/BarCode/{folioVentaRealizada}.png").AbsoluteUri;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                var logoTipo = string.Empty;

                using (DataTable dtLogoTipo = cn.CargarDatos(cs.getLogoTipoUsuario()))
                {
                    if (!dtLogoTipo.Rows.Count.Equals(0))
                    {
                        DataRow drLogoTipoUsuario = dtLogoTipo.Rows[0];
                        logoTipo = drLogoTipoUsuario["LogoTipo"].ToString();
                    }
                }

                if (!string.IsNullOrWhiteSpace(logoTipo))
                {
                    string fileName = logoTipo;
                    string sourcePath = $@"\\{servidor}\Archivos PUDVE\MisDatos\Usuarios\";
                    string targetPath = @"C:\Archivos PUDVE\Ventas\Tickets\BarCode\";

                    // Use Path class to manipulate file and directory paths.
                    string sourceFile = Path.Combine(sourcePath, fileName);
                    string destFile = Path.Combine(targetPath, fileName);
                    finalLogoTipoPath = destFile;

                    // To copy a file to another location and
                    // overwrite the destination file if it already exists.
                    File.Copy(sourceFile, destFile, true);
                }

                path = new Uri($"C:/Archivos PUDVE/Ventas/Tickets/BarCode/").AbsoluteUri;
            }
            else
            {
                path = new Uri("C:/Archivos PUDVE/MisDatos/Usuarios/").AbsoluteUri;
            }

            //01 parametro integer para mostrar / ocultar Logo
            reportParameters.Add(new ReportParameter("Logo", Logo.ToString()));
            //02 parametro integer para mostrar / ocultar Nombre
            reportParameters.Add(new ReportParameter("Nombre", Nombre.ToString()));
            //03 parametro integer para mostrar / ocultar Nombre Comercial
            reportParameters.Add(new ReportParameter("NombreComercial", NombreComercial.ToString()));
            //04 parametro integer para mostrar / ocultar Direccion Ciudad
            reportParameters.Add(new ReportParameter("DireccionCiudad", DireccionCiudad.ToString()));
            //05 parametro integer para mostrar / ocultar Colonia Codigo Postal
            reportParameters.Add(new ReportParameter("ColoniaCodigoPostal", ColoniaCodigoPostal.ToString()));
            //06 parametro integer para mostrar / ocultar RFC
            reportParameters.Add(new ReportParameter("RFC", RFC.ToString()));
            //07 parametro integer para mostrar / ocultar Correo
            reportParameters.Add(new ReportParameter("Correo", Correo.ToString()));
            //08 parametro integer para mostrar / ocultar Telefono
            reportParameters.Add(new ReportParameter("Telefono", Telefono.ToString()));
            //09 parametro integer para mostrar / ocultar Nombre Cliente
            reportParameters.Add(new ReportParameter("NombreCliente", NombreCliente.ToString()));
            //10 parametro integer para mostrar / ocultar RFC Cliente
            reportParameters.Add(new ReportParameter("RFCCliente", RFCCliente.ToString()));
            //11 parametro integer para mostrar / ocultar Domicilio Cliente
            reportParameters.Add(new ReportParameter("DomicilioCliente", DomicilioCliente.ToString()));
            //12 parametro integer para mostrar / ocultar Colonia Codigo Postal Cliente
            reportParameters.Add(new ReportParameter("ColoniaCodigoPostalCliente", ColoniaCodigoPostalCliente.ToString()));
            //13 parametro integer para mostrar / ocultar Correo Ciente
            reportParameters.Add(new ReportParameter("CorreoCliente", CorreoCliente.ToString()));
            //14 parametro integer para mostrar / ocultar Telefono Cliente
            reportParameters.Add(new ReportParameter("TelefonoCliente", TelefonoCliente.ToString()));
            //15 parametro integer para mostrar / ocultar Forma De Pago Cliente
            reportParameters.Add(new ReportParameter("FormaDePagoCliente", FormaDePagoCliente.ToString()));
            //16 parametro integer para mostrar / ocultar Codigo Barra
            reportParameters.Add(new ReportParameter("CodigoBarra", CodigoBarra.ToString()));
            //17 parametro string para mostrar / ocultar Path Logo
            reportParameters.Add(new ReportParameter("PathLogo", path));
            //18 parametro string para mostrar / ocultar Codigo de Barras
            reportParameters.Add(new ReportParameter("PathBarCode", pathBarCodeFull));
            //19 parametro integer para mostrar / ocultar Referencia
            reportParameters.Add(new ReportParameter("Referencia", Referencia.ToString()));


            string UsuarioRealizoVenta = string.Empty;

            using (var DTUsuario = cn.CargarDatos($"SELECT VEN.IDEmpleado, EMP.usuario FROM VENTAS AS VEN INNER JOIN empleados AS EMP ON( EMP.ID = VEN.IDEmpleado) WHERE VEN.ID = {idVentaRealizada} AND VEN.IDUsuario = {FormPrincipal.userID}"))
            {
                if (string.IsNullOrWhiteSpace(DTUsuario.Rows[0][0].ToString()))
                {
                    UsuarioRealizoVenta = FormPrincipal.userNickName;
                }
                else
                {
                    UsuarioRealizoVenta = DTUsuario.Rows[0][1].ToString();
                }
            }
            reportParameters.Add(new ReportParameter("Usuario", UsuarioRealizoVenta));
            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);
            rdlc.DataSources.Add(rp);
            #endregion

            EnviarImprimir imp = new EnviarImprimir();
            imp.Imprime(rdlc);

            File.Delete($"{pathBarCode}{folioVentaRealizada}.png");
            if (File.Exists(finalLogoTipoPath))
            {
                File.Delete(finalLogoTipoPath);
            }

            this.Close();
        }
    }
}
