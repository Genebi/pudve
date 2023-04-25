using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
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
    public partial class FormReporteHistorialPrecio : Form
    {
        string consulta;
        Consultas cs = new Consultas();
        Conexion cn = new Conexion();
        public static string ruta_archivos_guadados = @"C:\Archivos PUDVE\MisDatos\CSD\";
        string DireccionLogo;
        bool SiHayLogo = false;
        string pathLogoImage;
        public static bool fuePorVenta = false;
        public FormReporteHistorialPrecio(string cons)
        {
            InitializeComponent();
            this.consulta = cons;
        }

        private void FormReporteHistorialPrecio_Load(object sender, EventArgs e)
        {
            CargarDatos();
            this.reportViewer1.RefreshReport();
        }

        private void CargarDatos()
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
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\HistorialPrecio\ReporteHIstorialPrecio.rdlc";

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
                            pathLogoImage = new Uri($@"\\{servidor}\Archivos PUDVE\MisDatos\Usuarios\").AbsoluteUri;
                            // ruta donde estan guardados los archivos digitales
                            ruta_archivos_guadados = $@"\\{servidor}\Archivos PUDVE\MisDatos\CSD_{Logo}\";

                            DireccionLogo = pathLogoImage + Logo;
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
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            string Nombre, Usuario, Email, Telefono;
            using (var dt = cn.CargarDatos($"SELECT RazonSocial, Usuario,Email, Telefono FROM usuarios WHERE ID = {FormPrincipal.userID}"))
            {
                Nombre = dt.Rows[0]["RazonSocial"].ToString();
                Usuario = dt.Rows[0]["Usuario"].ToString();
                Email = dt.Rows[0]["Email"].ToString();
                Telefono = dt.Rows[0]["Telefono"].ToString();
            }
            if (string.IsNullOrWhiteSpace(DireccionLogo))
            {
                DireccionLogo = "";
            }
            reportParameters.Add(new ReportParameter("Logo", DireccionLogo));
            reportParameters.Add(new ReportParameter("Nombre", Nombre));
            reportParameters.Add(new ReportParameter("Usuario", Usuario));
            reportParameters.Add(new ReportParameter("Email", Email));
            reportParameters.Add(new ReportParameter("Telefono", Telefono));
            DataTable DTFinal = cn.CargarDatos(consulta);

            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource ReporteHistorialPrecio = new ReportDataSource("DatosHistorialPrecio", DTFinal);

            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.LocalReport.DataSources.Add(ReporteHistorialPrecio);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }
    }
}
