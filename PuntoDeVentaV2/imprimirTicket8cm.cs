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
    public partial class imprimirTicket8cm : Form
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

        public imprimirTicket8cm()
        {
            InitializeComponent();
        }

        private void imprimirTicket8cm_Load(object sender, EventArgs e)
        {
            CargarDatosCaja();
        }

        private void CargarDatosCaja()
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

            string queryVenta = cs.imprimirTicketRealizada(idVentaRealizada);

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

            string FullReportPath = string.Empty;
            int tamanno = 0;
            using (var DTTammanoTicket = cn.CargarDatos($"SELECT tamannoTicket from editarticket WHERE IDUsuario = {FormPrincipal.userID}"))
            {
                tamanno = Convert.ToInt32(DTTammanoTicket.Rows[0][0]);
            }
            if (tamanno == 1)
            {
                FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\VentaRealizada\ReporteTicket80mm.rdlc";
            }
            else if (tamanno == 2)
            {
                FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\VentaRealizada\ReporteTicket80mm2.rdlc";
            }
            else
            {
                FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\VentaRealizada\ReporteTicket80mm3.rdlc";

            }
            MySqlDataAdapter ventaDA = new MySqlDataAdapter(queryVenta, conn);
            DataTable ventaDT = new DataTable();

            

            ventaDA.Fill(ventaDT);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();
            decimal cantidadDesceunto = 0;
            int TieneDescuento = 1;
            foreach (DataRow dataRow in ventaDT.Rows)
            {
                string moneda1 = FormPrincipal.Moneda;
                var moneda2 = moneda1.Split('(');
                moneda2[1] = moneda2[1].Replace(")", "");
                var canditda = dataRow["ProductoDescuento"].ToString().Split(Convert.ToChar(moneda2[1]));
                cantidadDesceunto += Convert.ToDecimal(canditda[1]);
            }
            if (cantidadDesceunto == 0)
            {
                TieneDescuento = 0;
            }
            #region Impresion Ticket de 80 mm
            ReportDataSource rp = new ReportDataSource("TicketVenta", ventaDT);

            string DirectoryImage = string.Empty;

            this.reportViewer1.LocalReport.EnableExternalImages = true;

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            string path = string.Empty;
            reportParameters.Add(new ReportParameter("Usuario", FormPrincipal.userNickName.ToString()));
            reportParameters.Add(new ReportParameter("TieneDescuento", TieneDescuento.ToString()));
            string pathBarCode = $@"C:\Archivos PUDVE\Ventas\Tickets\BarCode\";

            var servidor = Properties.Settings.Default.Hosting;
            var folioVentaRealizada = string.Empty;

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

            var finalLogoTipoPath = string.Empty;

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
            //18 parametro string para mostrar / ocultar Codigo de Barras
            reportParameters.Add(new ReportParameter("Referencia", Referencia.ToString()));
            string UsuarioRealizoVenta =  string.Empty;

            using (var DTUsuario = cn.CargarDatos($"SELECT VEN.IDEmpleado, EMP.usuario FROM VENTAS AS VEN INNER JOIN empleados AS EMP ON( EMP.ID = VEN.IDEmpleado) WHERE VEN.ID = {idVentaRealizada} AND VEN.IDUsuario = {FormPrincipal.userID}"))
            {
                if (!DTUsuario.Rows.Count.Equals(0))
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
                else
                {
                    UsuarioRealizoVenta = FormPrincipal.userNickName;
                }
                
            }
            reportParameters.Add(new ReportParameter("Usuario", UsuarioRealizoVenta));

            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.LocalReport.DataSources.Add(rp);
            this.reportViewer1.RefreshReport();

            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);
            rdlc.DataSources.Add(rp);
            #endregion
            using (var dtTicketOPDF = cn.CargarDatos($"SELECT TicketOPDF FROM `configuracion` WHERE IDUsuario = {FormPrincipal.userID}"))
            {
                if (dtTicketOPDF.Rows[0][0].Equals(1))
                {
                    EnviarImprimir imp = new EnviarImprimir();
                    imp.Imprime(rdlc);
                    File.Delete($"{pathBarCode}{folioVentaRealizada}.png");

                    if (File.Exists(finalLogoTipoPath))
                    {
                        File.Delete(finalLogoTipoPath);
                    }
                    this.Close();
                }
                else
                {
                    FormNotaDeVenta.fuePorVenta = true;
                    FormNotaDeVenta venta = new FormNotaDeVenta(idVentaRealizada);
                    venta.ShowDialog();
                }
            }

            File.Delete($"{pathBarCode}{folioVentaRealizada}.png");

            if (File.Exists(finalLogoTipoPath))
            {
                File.Delete(finalLogoTipoPath);
            }
            this.Close();

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
    }
}
