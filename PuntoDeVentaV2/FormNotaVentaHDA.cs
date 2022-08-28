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
    public partial class FormNotaVentaHDA : Form
    {

        Consultas cs = new Consultas();
        Conexion cn = new Conexion();
        Moneda oMoneda = new Moneda();
        int IDVenta;
        decimal Total = 0;
        string resultado;
        public static string ruta_archivos_guadados = @"C:\Archivos PUDVE\MisDatos\CSD\";
        string[] usuario;
        string nombreUsuario;
        string DireccionLogo;
        bool SiHayLogo = false;
        string pathLogoImage;
        string StatusVenta;
        public FormNotaVentaHDA(int IDDeLaVEnta)
        {
            InitializeComponent();
            this.IDVenta = IDDeLaVEnta;
        }

        private void FormNotaVentaHDA_Load(object sender, EventArgs e)
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

            queryVentas = cs.PDFNotaDeVentasHDA(IDVenta);

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
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\NotasVentasHDA\ReportNotaVentaHDA.rdlc";

            //imagen

            var servidor = Properties.Settings.Default.Hosting;
            string saveDirectoryImg = @"C:\Archivos PUDVE\MisDatos\Usuarios\";

            using (DataTable ConsultaLogo = cn.CargarDatos(cs.buscarNombreLogoTipo2(FormPrincipal.userID)))
            {
                if (!ConsultaLogo.Rows.Count.Equals(0))
                {
                    string Logo = ConsultaLogo.Rows[0]["Logo"].ToString();
                    if (!Logo.Equals(""))
                    {

                        if (!Directory.Exists(saveDirectoryImg))    // verificamos que si no existe el directorio
                        {
                            Directory.CreateDirectory(saveDirectoryImg);    // lo crea para poder almacenar la imagen
                        }
                        if (!string.IsNullOrWhiteSpace(servidor))
                        {
                            // direccion de la carpeta donde se va poner las imagenes
                            pathLogoImage = new Uri($"C:/Archivos PUDVE/MisDatos/Usuarios/").AbsoluteUri;
                            // ruta donde estan guardados los archivos digitales
                            ruta_archivos_guadados = $@"\\{servidor}\Archivos PUDVE\MisDatos\CSD_{Logo}\";
                        }
                        else
                        {
                            // direccion de la carpeta donde se va poner las imagenes
                            pathLogoImage = new Uri($"C:/Archivos PUDVE/MisDatos/Usuarios/").AbsoluteUri;
                            // ruta donde estan guardados los archivos digitales
                            ruta_archivos_guadados = $@"C:\Archivos PUDVE\MisDatos\CSD_{Logo}\";

                            DireccionLogo = pathLogoImage + Logo;

                        }
                        SiHayLogo = true;
                    }
                }
            }
            //imagen

            MySqlDataAdapter retiroDA = new MySqlDataAdapter(queryVentas, conn);
            DataTable DTNotaDeVentas = new DataTable();
            retiroDA.Fill(DTNotaDeVentas);

            Total = Convert.ToDecimal(DTNotaDeVentas.Rows[0]["total"]);
            resultado = oMoneda.Convertir(Total.ToString(), true, "PESOS");

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("TotalEnTexto", resultado));
            if(SiHayLogo.Equals(true))
            {
                reportParameters.Add(new ReportParameter("Logo", DireccionLogo));
            }
            else
            {
                DireccionLogo = "";
                reportParameters.Add(new ReportParameter("Logo", DireccionLogo));
            }
            string StatusVenta;
            using (DataTable ConsultaEstatus = cn.CargarDatos($"SELECT `Status` FROM ventas WHERE ID = {IDVenta}"))
            {
                string Status = ConsultaEstatus.Rows[0]["Status"].ToString();
                if (Status.Equals("1"))
                {
                    StatusVenta = "Venta Pagada";
                }
                else if (Status.Equals("2"))
                {
                    StatusVenta = "Presupuesto";
                }
                else if (Status.Equals("3"))
                {
                    StatusVenta = "Venta Cancelada";
                }
                else if (Status.Equals("5"))
                {
                    StatusVenta = "Venta Global";
                }
                else
                {
                    StatusVenta = "Venta a Crédito";
                }
            }
            reportParameters.Add(new ReportParameter("StatusVenta", StatusVenta));

            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource NotasVENTAS = new ReportDataSource("DTNotaVentaHDA", DTNotaDeVentas);

            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.LocalReport.DataSources.Add(NotasVENTAS);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
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

            queryVentas = cs.PDFNotaDeVentasHDA(IDVenta);



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
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\NotasVentasHDA\ReportNotaVentaHDA.rdlc";

            //imagen

            var servidor = Properties.Settings.Default.Hosting;
            string saveDirectoryImg = @"C:\Archivos PUDVE\MisDatos\Usuarios\";

            using (DataTable ConsultaLogo = cn.CargarDatos(cs.buscarNombreLogoTipo2(FormPrincipal.userID)))
            {
                if (!ConsultaLogo.Rows.Count.Equals(0))
                {
                    string Logo = ConsultaLogo.Rows[0]["Logo"].ToString();
                    if (!Logo.Equals(""))
                    {

                        if (!Directory.Exists(saveDirectoryImg))    // verificamos que si no existe el directorio
                        {
                            Directory.CreateDirectory(saveDirectoryImg);    // lo crea para poder almacenar la imagen
                        }
                        if (!string.IsNullOrWhiteSpace(servidor))
                        {
                            // direccion de la carpeta donde se va poner las imagenes
                            pathLogoImage = new Uri($"C:/Archivos PUDVE/MisDatos/Usuarios/").AbsoluteUri;
                            // ruta donde estan guardados los archivos digitales
                            ruta_archivos_guadados = $@"\\{servidor}\Archivos PUDVE\MisDatos\CSD_{Logo}\";
                        }
                        else
                        {
                            // direccion de la carpeta donde se va poner las imagenes
                            pathLogoImage = new Uri($"C:/Archivos PUDVE/MisDatos/Usuarios/").AbsoluteUri;
                            // ruta donde estan guardados los archivos digitales
                            ruta_archivos_guadados = $@"C:\Archivos PUDVE\MisDatos\CSD_{Logo}\";

                            DireccionLogo = pathLogoImage + Logo;

                        }
                        SiHayLogo = true;
                    }
                }
            }
            //imagen

            MySqlDataAdapter retiroDA = new MySqlDataAdapter(queryVentas, conn);
            DataTable DTNotaDeVentas = new DataTable();
            retiroDA.Fill(DTNotaDeVentas);

            Total = Convert.ToDecimal(DTNotaDeVentas.Rows[0]["total"]);
            resultado = oMoneda.Convertir(Total.ToString(), true, "PESOS");

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("TotalEnTexto", resultado));
            if (SiHayLogo.Equals(true))
            {
                reportParameters.Add(new ReportParameter("Logo", DireccionLogo));
            }
            else
            {
                DireccionLogo = "";
                reportParameters.Add(new ReportParameter("Logo", DireccionLogo));
            }
            reportParameters.Add(new ReportParameter("StatusVenta", "Venta Pagada"));

            ReportDataSource NotasVENTAS = new ReportDataSource("DTNotaVentaHDA", DTNotaDeVentas);

            LocalReport rdlc = new LocalReport();
            rdlc.ReportPath = FullReportPath;
            rdlc.EnableExternalImages = true;
            rdlc.DataSources.Add(NotasVENTAS);
            rdlc.SetParameters(reportParameters); 

            FormatoCarta imp = new FormatoCarta();
            imp.Imprime(rdlc);

            this.Close();
        }
    }
}
