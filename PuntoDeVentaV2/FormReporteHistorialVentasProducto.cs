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
    public partial class FormReporteHistorialVentasProducto : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        int IDProd;
        string IDSEmple, FechaI, FechaF;
        string DireccionLogo;
        bool SiHayLogo = false;
        string pathLogoImage;
        public static string ruta_archivos_guadados = @"C:\Archivos PUDVE\MisDatos\CSD\";
        DataTable DTDatos2 = new DataTable();
        public FormReporteHistorialVentasProducto(int IDProducto, string IDEmpleado, string FechaIncio, string FechaFinal)
        {
            InitializeComponent();
            this.IDProd = IDProducto;
            this.IDSEmple = IDEmpleado;
            this.FechaI = FechaIncio;
            this.FechaF = FechaFinal;
        }

        private void FormReporteHistorialVentasProducto_Load(object sender, EventArgs e)
        {
            CargarDatos();
            CargarRdcl();
            this.reportViewer1.RefreshReport();
        }

        private void CargarDatos()
        {
            DataTable DTDatos = new DataTable();
            string IDsVentas = "";
            using (var DTIDVenta1 = cn.CargarDatos($"SELECT IDVenta FROM productosventa WHERE IDProducto = {IDProd}"))
            {
                if (!DTIDVenta1.Rows.Count.Equals(0))
                {
                    int row = 0;
                    foreach (var item in DTIDVenta1.Rows)
                    {
                        IDsVentas += DTIDVenta1.Rows[row]["IDVenta"].ToString() + ",";
                        row++;
                    }
                    IDsVentas = IDsVentas.TrimEnd(',');
                }
                else
                {
                    MessageBox.Show("Este producto no a sido vendido", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            if (!IDSEmple.Equals(""))
            {
                DataTable ventas = new DataTable();
                string IDsVentasporFechaYEmpleado = "";
                using (var DTIDSVentaPorEmpleadoYFecha = cn.CargarDatos($"SELECT ID,Folio,IDEmpleado,FechaOperacion FROM ventas WHERE ID IN ({IDsVentas}) AND DATE(FechaOperacion) BETWEEN '{FechaI}' AND '{FechaF}' AND IDUsuario = {FormPrincipal.userID} AND IDEmpleado IN ({IDSEmple}) AND `Status` = 1"))
                {
                    if (!DTIDSVentaPorEmpleadoYFecha.Rows.Count.Equals(0))
                    {
                        ventas = DTIDSVentaPorEmpleadoYFecha;
                        int row = 0;
                        foreach (var item in DTIDSVentaPorEmpleadoYFecha.Rows)
                        {
                            IDsVentasporFechaYEmpleado += DTIDSVentaPorEmpleadoYFecha.Rows[row]["ID"].ToString() + ",";
                        }
                        IDsVentasporFechaYEmpleado = IDsVentasporFechaYEmpleado.TrimEnd(',');
                    }
                    else
                    {
                        string Usuario;
                        using (var dtusuario = cn.CargarDatos($"SELECT usuario FROM empleados WHERE ID IN ({IDSEmple})"))
                        {
                            Usuario = dtusuario.Rows[0]["usuario"].ToString();
                        }
                        MessageBox.Show($"El usuario {Usuario} no a vendido este producto", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                var IDSVentasCorrectas = cn.CargarDatos($"SELECT * FROM productosventa WHERE IDVenta IN ({IDsVentasporFechaYEmpleado}) AND IDProducto = {IDProd}");

                DTDatos.Columns.Add("Folio", typeof(String));
                DTDatos.Columns.Add("Usuario", typeof(String));
                DTDatos.Columns.Add("Fecha", typeof(String));
                DTDatos.Columns.Add("Cantidad", typeof(String));
                DTDatos.Columns.Add("PrecioUnidad", typeof(String));
                DTDatos.Columns.Add("Total", typeof(String));
                int rows = 0;
                foreach (var item in IDSVentasCorrectas.Rows)
                {
                    DTDatos.Rows.Add();
                    foreach (var DT in DTDatos.Columns)
                    {
                        string Columna = DT.ToString();
                        if (Columna.Equals("Folio"))
                        {
                            DTDatos.Rows[rows]["Folio"] = ventas.Rows[rows]["Folio"].ToString();
                        }
                        else if (Columna.Equals("Usuario"))
                        {
                            string Usuario;
                            string ID = ventas.Rows[rows]["IDEmpleado"].ToString();
                            using (var DTUsuario = cn.CargarDatos($"SELECT Usuario FROM empleados WHERE ID = {ID}"))
                            {
                                if (DTUsuario.Rows.Count.Equals(0))
                                {
                                    var nombre = FormPrincipal.userNickName.Split('@');
                                    Usuario = nombre[0];
                                }
                                else
                                {
                                    Usuario = DTUsuario.Rows[0]["Usuario"].ToString();
                                }
                            }
                            DTDatos.Rows[rows]["Usuario"] = Usuario;
                        }
                        else if (Columna.Equals("Fecha"))
                        {
                            DTDatos.Rows[rows]["Fecha"] = ventas.Rows[rows]["FechaOperacion"].ToString();
                        }
                        else if (Columna.Equals("Cantidad"))
                        {
                            DTDatos.Rows[rows]["Cantidad"] = IDSVentasCorrectas.Rows[rows]["Cantidad"].ToString();
                        }
                        else if (Columna.Equals("PrecioUnidad"))
                        {
                            DTDatos.Rows[rows]["PrecioUnidad"] = IDSVentasCorrectas.Rows[rows]["Precio"].ToString();
                        }
                        else if (Columna.Equals("Total"))
                        {
                            decimal total = Convert.ToDecimal(IDSVentasCorrectas.Rows[rows]["Cantidad"].ToString()) * Convert.ToDecimal(IDSVentasCorrectas.Rows[rows]["Precio"].ToString());
                            DTDatos.Rows[rows]["Total"] = total.ToString("0.00");
                        }
                    }
                    rows++;
                }

            }
            else
            {
                DataTable ventas = new DataTable();
                string IDsVentasporFechaYEmpleado = "";
                using (var DTIDSVentaPorEmpleadoYFecha = cn.CargarDatos($"SELECT ID,Folio,IDEmpleado,FechaOperacion,IDCliente FROM ventas WHERE ID IN ({IDsVentas}) AND DATE(FechaOperacion) BETWEEN '{FechaI}' AND '{FechaF}' AND IDUsuario = {FormPrincipal.userID} AND `Status` = 1"))
                {
                    if (!DTIDSVentaPorEmpleadoYFecha.Rows.Count.Equals(0))
                    {
                        ventas = DTIDSVentaPorEmpleadoYFecha;
                        int row = 0;
                        foreach (var item in DTIDSVentaPorEmpleadoYFecha.Rows)
                        {
                            IDsVentasporFechaYEmpleado += DTIDSVentaPorEmpleadoYFecha.Rows[row]["ID"].ToString() + ",";
                            row++;
                        }
                        IDsVentasporFechaYEmpleado = IDsVentasporFechaYEmpleado.TrimEnd(',');
                    }
                    else
                    {

                        MessageBox.Show($"Este producto no se ha vendido", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                var IDSVentasCorrectas = cn.CargarDatos($"SELECT PV.*, VEN.IDEmpleado, VEN.IDCliente FROM productosventa AS PV INNER JOIN ventas AS VEN ON(PV.IDVenta = VEN.ID) WHERE PV.IDVenta IN ({IDsVentasporFechaYEmpleado}) AND IDProducto = {IDProd}");

                DTDatos.Columns.Add("Folio", typeof(String));
                DTDatos.Columns.Add("Usuario", typeof(String));
                DTDatos.Columns.Add("Fecha", typeof(String));
                DTDatos.Columns.Add("Cantidad", typeof(String));
                DTDatos.Columns.Add("PrecioUnidad", typeof(String));
                DTDatos.Columns.Add("Total", typeof(String));
                DTDatos.Columns.Add("Empleado", typeof(String));
                DTDatos.Columns.Add("Cliente", typeof(String));

                int rows = 0;
                foreach (var item in IDSVentasCorrectas.Rows)
                {
                    DTDatos.Rows.Add();
                    foreach (var DT in DTDatos.Columns)
                    {
                        string Columna = DT.ToString();
                        if (Columna.Equals("Folio"))
                        {
                            DTDatos.Rows[rows]["Folio"] = ventas.Rows[rows]["Folio"].ToString();
                        }
                        else if (Columna.Equals("Usuario"))
                        {
                            string Usuario;
                            string ID = ventas.Rows[rows]["IDEmpleado"].ToString();
                            using (var DTUsuario = cn.CargarDatos($"SELECT Usuario FROM empleados WHERE ID = {ID}"))
                            {
                                if (DTUsuario.Rows.Count.Equals(0))
                                {
                                    var nombre = FormPrincipal.userNickName.Split('@');
                                    Usuario = nombre[0];
                                }
                                else
                                {
                                    Usuario = DTUsuario.Rows[0]["Usuario"].ToString();
                                }
                            }
                            DTDatos.Rows[rows]["Usuario"] = Usuario;
                        }
                        else if (Columna.Equals("Fecha"))
                        {
                            DTDatos.Rows[rows]["Fecha"] = ventas.Rows[rows]["FechaOperacion"].ToString();
                        }
                        else if (Columna.Equals("Cantidad"))
                        {
                            DTDatos.Rows[rows]["Cantidad"] = IDSVentasCorrectas.Rows[rows]["Cantidad"].ToString();
                        }
                        else if (Columna.Equals("PrecioUnidad"))
                        {
                            DTDatos.Rows[rows]["PrecioUnidad"] = IDSVentasCorrectas.Rows[rows]["Precio"].ToString();
                        }
                        else if (Columna.Equals("Total"))
                        {
                            decimal total = Convert.ToDecimal(IDSVentasCorrectas.Rows[rows]["Cantidad"].ToString()) * Convert.ToDecimal(IDSVentasCorrectas.Rows[rows]["Precio"].ToString());
                            DTDatos.Rows[rows]["Total"] = total.ToString("0.00");
                        }
                        else if (Columna.Equals("Empleado"))
                        {
                            if (IDSVentasCorrectas.Rows[rows]["IDEmpleado"].ToString().Equals("0"))
                            {
                                using (var Nombre = cn.CargarDatos($"SELECT Usuario FROM usuarios WHERE ID = {FormPrincipal.userID}"))
                                {
                                    DTDatos.Rows[rows]["Empleado"] = Nombre.Rows[0][0].ToString();
                                }
                            }
                            else
                            {
                                using (var DTUsuario = cn.CargarDatos($"SELECT usuario FROM empleados WHERE ID = {Convert.ToInt32(IDSVentasCorrectas.Rows[rows]["IDEmpleado"])}"))
                                {
                                    DTDatos.Rows[rows]["Empleado"] = DTUsuario.Rows[0][0].ToString();
                                }
                            }
                        }
                        else
                        {
                            using (var DTCliente = cn.CargarDatos($"SELECT RazonSocial FROM clientes WHERE ID = {Convert.ToInt32(IDSVentasCorrectas.Rows[rows]["IDCliente"])}"))
                            {
                                if (DTCliente.Rows.Count.Equals(0))
                                {
                                    DTDatos.Rows[rows]["Cliente"] = "PUBLICO GENERAL";
                                }
                                else
                                {
                                    DTDatos.Rows[rows]["Cliente"] = DTCliente.Rows[0][0].ToString();
                                }
                            }


                        }
                    }
                    rows++;
                }
            }
            DTDatos2 = DTDatos;
        }

        private void CargarRdcl()
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
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\HistorialVenta\ReporteHistorialVenta.rdlc";


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
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            if (SiHayLogo.Equals(true))
            {
                reportParameters.Add(new ReportParameter("Logo", DireccionLogo));
            }
            else
            {
                DireccionLogo = "";
                reportParameters.Add(new ReportParameter("Logo", DireccionLogo));
            }
            string Nombre, Email, Telefono;
            using (var DTusuario = cn.CargarDatos($"SELECT NombreCompleto,Email,Telefono FROM `usuarios` WHERE ID = {FormPrincipal.userID}"))
            {
                Nombre = DTusuario.Rows[0]["NombreCompleto"].ToString();
                Email = DTusuario.Rows[0]["Email"].ToString();
                Telefono = DTusuario.Rows[0]["Telefono"].ToString();
            }
            reportParameters.Add(new ReportParameter("Nombre", Nombre));
            reportParameters.Add(new ReportParameter("Email", Email));
            reportParameters.Add(new ReportParameter("Telefono", Telefono));
            string NombreProducto, CodigoBarras;
            using (var DTProducto = cn.CargarDatos($"SELECT Nombre,CodigoBarras FROM `productos` WHERE ID = {IDProd}"))
            {
                NombreProducto = DTProducto.Rows[0]["Nombre"].ToString();
                CodigoBarras = DTProducto.Rows[0]["CodigoBarras"].ToString();
            }
            reportParameters.Add(new ReportParameter("NombreProducto", NombreProducto));
            reportParameters.Add(new ReportParameter("CodigoBarras", CodigoBarras));

            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource ReporteHistorialVenta = new ReportDataSource("DTHistorialVenta", DTDatos2);

            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.LocalReport.DataSources.Add(ReporteHistorialVenta);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }
    }
}
