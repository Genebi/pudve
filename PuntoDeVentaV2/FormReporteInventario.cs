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
    public partial class FormReporteInventario : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        DataTable DTFinal = new DataTable();
        string datoscompletos;
        string producto, proveedor, unidadesCompradas, PrecioCompra, TotalComprado, PrecioVenta, StockAnterior, StockNuevo, fechOperacion, comentarios, ubicacion, color;
        bool SiHayLogo = false;
        string pathLogoImage;
        public static string ruta_archivos_guadados = @"C:\Archivos PUDVE\MisDatos\CSD\";
        string DireccionLogo;
        string NombreUsuario, Usuario, Texto;
        decimal UnidadesCompras = 0, PCompra = 0,PTotal = 0, PVenta = 0, SAnterior = 0, SActual= 0;
        string claveP;
        public FormReporteInventario(string clave)
        {
            InitializeComponent();
            if (!clave.Equals(""))
            {
                claveP = $"Clave de traspaso = {clave}";
            }
            else
            {
                claveP = "";
            }
        }

        private void FormReporteInventario_Load(object sender, EventArgs e)
        {
            CargarDatos();
            CargarRDLC();
            this.reportViewer1.RefreshReport();
        }

        private void CargarRDLC()
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
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\Inventario\ReporteInventario.rdlc";

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
                    else
                    {
                        DireccionLogo = "";
                    }
                }
                
            }
            using (var DTDatosUsuarios = cn.CargarDatos($"SELECT Usuario,RazonSocial FROM usuarios WHERE ID = {FormPrincipal.userID}"))
            {
                Usuario = DTDatosUsuarios.Rows[0]["Usuario"].ToString();
                NombreUsuario = DTDatosUsuarios.Rows[0]["RazonSocial"].ToString();
            }

            if (Inventario.esAumentar.Equals(true))
            {
                Texto = "REPORTE DE ACTUALIZAR INVENTARIO SECCIÓN DE AUMENTAR";
            }
            else
            {
                Texto = "REPORTE DE ACTUALIZAR INVENTARIO SECCIÓN DE DISMINUIR";
            }
            int contador = 0;
            foreach (var row in DTFinal.Rows)
            { 
                if (!DTFinal.Rows[contador]["UnidadesCompradasDisminuidas"].Equals(""))
                {
                    string algoxd = DTFinal.Rows[contador]["UnidadesCompradasDisminuidas"].ToString();
                    var esNum = decimal.TryParse(algoxd, out decimal validacion);
                    if (esNum)
                    {
                        UnidadesCompras += Convert.ToDecimal(DTFinal.Rows[contador]["UnidadesCompradasDisminuidas"]);
                    }
                    else
                    {
                        UnidadesCompras += 0;
                    }
                    
                }
                if (!DTFinal.Rows[contador]["PrecioCompra"].Equals(""))
                {
                    string algoxd = DTFinal.Rows[contador]["PrecioCompra"].ToString();
                    var esNum = decimal.TryParse(algoxd, out decimal validacion);
                    if (esNum)
                    {
                        PCompra += Convert.ToDecimal(DTFinal.Rows[contador]["PrecioCompra"]);
                    }
                    else
                    {
                        PCompra += 0;
                    }
                   
                }
                if (!DTFinal.Rows[contador]["TotalCompras"].Equals(""))
                {
                    string algoxd = DTFinal.Rows[contador]["TotalCompras"].ToString();
                    var esNum = decimal.TryParse(algoxd, out decimal validacion);
                    if (esNum)
                    {
                        PTotal += Convert.ToDecimal(DTFinal.Rows[contador]["TotalCompras"]);
                    }
                    else
                    {
                        PTotal += 0;
                    }
                    
                }
                if (!DTFinal.Rows[contador]["PrecioVenta"].Equals(""))
                {
                    string algoxd = DTFinal.Rows[contador]["PrecioVenta"].ToString();
                    var esNum = decimal.TryParse(algoxd, out decimal validacion);
                    if (esNum)
                    {
                        PVenta += Convert.ToDecimal(DTFinal.Rows[contador]["PrecioVenta"]);
                    }
                    else
                    {
                        PVenta += 0;
                    }
                   
                }
                if (!DTFinal.Rows[contador]["StockAnterior"].Equals(""))
                {
                    string algoxd = DTFinal.Rows[contador]["StockAnterior"].ToString();
                    var esNum = decimal.TryParse(algoxd, out decimal validacion);
                    if (esNum)
                    {
                        SAnterior += Convert.ToDecimal(DTFinal.Rows[contador]["StockAnterior"]);
                    }
                    else
                    {
                        SAnterior += 0;
                    }
                    
                }
                if (!DTFinal.Rows[contador]["StockActual"].Equals(""))
                {
                    string algoxd = DTFinal.Rows[contador]["StockActual"].ToString();
                    var esNum = decimal.TryParse(algoxd, out decimal validacion);
                    if (esNum)
                    {
                        SActual += Convert.ToDecimal(DTFinal.Rows[contador]["StockActual"]);
                    }
                    else
                    {
                        SActual += 0;
                    }
                    
                }
                contador++;
            }

            string algo = string.Empty;
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("Logo", DireccionLogo));
            reportParameters.Add(new ReportParameter("Usuario", Usuario));
            reportParameters.Add(new ReportParameter("RazonSocial", NombreUsuario));
            reportParameters.Add(new ReportParameter("Texto", Texto));
            reportParameters.Add(new ReportParameter("UnidadesCompras", UnidadesCompras.ToString()));
            reportParameters.Add(new ReportParameter("PCompra", PCompra.ToString()));
            reportParameters.Add(new ReportParameter("PTotal", PTotal.ToString()));
            reportParameters.Add(new ReportParameter("PVenta", PVenta.ToString()));
            reportParameters.Add(new ReportParameter("SAnterior", SAnterior.ToString()));
            reportParameters.Add(new ReportParameter("SActual", SActual.ToString()));
            reportParameters.Add(new ReportParameter("Clave", claveP));

            UnidadesCompras = 0;
            PCompra = 0;
            PTotal = 0;
            PVenta = 0;
            SAnterior = 0;
            SActual = 0;
            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource ReporteInventario = new ReportDataSource("DataTable1", DTFinal);

            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.LocalReport.DataSources.Add(ReporteInventario);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        private void CargarDatos()
        {
            DTFinal.Columns.Add("No", typeof(String));
            DTFinal.Columns.Add("Producto", typeof(String));
            DTFinal.Columns.Add("Proveedor", typeof(String));
            DTFinal.Columns.Add("UnidadesCompradasDisminuidas", typeof(String));
            DTFinal.Columns.Add("PrecioCompra", typeof(String));
            DTFinal.Columns.Add("TotalCompras", typeof(String));
            DTFinal.Columns.Add("PrecioVenta", typeof(String));
            DTFinal.Columns.Add("StockAnterior", typeof(String));
            DTFinal.Columns.Add("StockActual", typeof(String));
            DTFinal.Columns.Add("FechadeOperacion", typeof(String));
            DTFinal.Columns.Add("Comentarios", typeof(String));
            DTFinal.Columns.Add("Ubicacion", typeof(String));
            DTFinal.Columns.Add("Color", typeof(String));
            int contadorproducos = 1;
            int RowsDatosInventario = 0;
            int contadorROWSTabñaFinal = 0;
            foreach (var dato in Inventario.DTDatos.Rows)
            {
                int ID = Convert.ToInt32(Inventario.DTDatos.Rows[RowsDatosInventario]["No"]);
                DataTable DTConssulta = cn.CargarDatos($"SELECT P.Nombre AS 'Producto', p.PrecioCompra AS 'Precio Compra', p.Precio AS 'Precio Venta', p.Stock AS 'Stock Anterior' FROM productos AS P WHERE P.ID = {ID}");

                DataTable DTProveedor = cn.CargarDatos($"SELECT Proveedor FROM detallesproducto WHERE IDProducto ={ID}");

                int contadorRows = 0;
                foreach (var item in DTFinal.Columns)
                {
                    string columna = item.ToString();
                    if (columna.Equals("No"))
                    {
                        datoscompletos += contadorproducos.ToString() + ",";
                        contadorproducos++;
                    }
                    if (columna.Equals("Producto"))
                    {
                        if (Inventario.listaConceptosSeleccionados.Contains("Producto"))
                        {
                            producto = DTConssulta.Rows[contadorRows]["Producto"].ToString();
                            datoscompletos += producto + ",";
                        }
                        else
                        {
                            producto = "";
                            datoscompletos += producto + ",";
                        }

                    }
                    if (columna.Equals("Proveedor"))
                    {
                        if (Inventario.listaConceptosSeleccionados.Contains("Proveedor"))
                        {
                            if (!DTProveedor.Rows.Count.Equals(0))
                            {
                                proveedor = DTProveedor.Rows[contadorRows]["Proveedor"].ToString();
                            }
                            else
                            {
                                proveedor = "---";
                            }
                            
                            datoscompletos += proveedor + ",";

                        }
                        else
                        {
                            proveedor = "";
                            datoscompletos += proveedor + ",";
                        }

                    }
                    if (columna.Equals("UnidadesCompradasDisminuidas"))
                    {
                        if (Inventario.listaConceptosSeleccionados.Contains("Unidades Compradas/Disminuidas"))
                        {
                            unidadesCompradas = Inventario.DTDatos.Rows[RowsDatosInventario]["Unidades_Compradas"].ToString();
                            if (string.IsNullOrWhiteSpace(unidadesCompradas))
                            {
                                unidadesCompradas = "0";
                            }
                            datoscompletos += unidadesCompradas + ",";
                        }
                        else
                        {
                            unidadesCompradas = "";
                            datoscompletos += unidadesCompradas + ",";
                        }

                    }
                    if (columna.Equals("PrecioCompra"))
                    {
                        if (Inventario.listaConceptosSeleccionados.Contains("Precio Compra"))
                        {
                            PrecioCompra = Inventario.DTDatos.Rows[RowsDatosInventario]["Precio_Compra"].ToString();
                            datoscompletos += PrecioCompra + ",";
                        }
                        else
                        {
                            PrecioCompra = "";
                            datoscompletos += PrecioCompra + ",";
                        }

                    }
                    if (columna.Equals("TotalCompras"))
                    {
                        if (Inventario.listaConceptosSeleccionados.Contains("Total Compras"))
                        {
                            if (unidadesCompradas.Equals(""))
                            {
                                unidadesCompradas = "0";
                            }
                            if (PrecioCompra.Equals(""))
                            {
                                PrecioCompra = "0";
                            }
                            var operacion = Convert.ToDecimal(unidadesCompradas) * Convert.ToDecimal(PrecioCompra);
                            TotalComprado = operacion.ToString();
                            datoscompletos += TotalComprado + ",";
                        }
                        else
                        {
                            TotalComprado = "";
                            datoscompletos += TotalComprado + ",";
                        }

                    }
                    if (columna.Equals("PrecioVenta"))
                    {
                        if (Inventario.listaConceptosSeleccionados.Contains("Precio Venta"))
                        {
                            PrecioVenta = DTConssulta.Rows[contadorRows]["Precio Venta"].ToString();
                            datoscompletos += PrecioVenta + ",";
                        }
                        else
                        {
                            PrecioVenta = "";
                            datoscompletos += PrecioVenta + ",";
                        }

                    }
                    if (columna.Equals("StockAnterior"))
                    {
                        if (Inventario.listaConceptosSeleccionados.Contains("Stock Anterior"))
                        {
                            StockAnterior = DTConssulta.Rows[contadorRows]["Stock Anterior"].ToString();
                            datoscompletos += StockAnterior + ",";
                        }
                        else
                        {
                            StockAnterior = "";
                            datoscompletos += StockAnterior + ",";
                        }

                    }
                    if (columna.Equals("StockActual"))
                    {
                        if (Inventario.listaConceptosSeleccionados.Contains("Stock Actual"))
                        {
                            if (unidadesCompradas.Equals(""))
                            {
                                unidadesCompradas = "0";
                            }
                            if (StockAnterior.Equals(""))
                            {
                                StockAnterior = "0";
                            }
                            var nuevo = Convert.ToDecimal(unidadesCompradas) + Convert.ToDecimal(StockAnterior);
                            StockNuevo = nuevo.ToString();
                            datoscompletos += StockNuevo + ",";
                        }
                        else
                        {
                            StockNuevo = "";
                            datoscompletos += StockNuevo + ",";
                        }

                    }
                    if (columna.Equals("FechadeOperacion"))
                    {
                        if (Inventario.listaConceptosSeleccionados.Contains("Fecha de Operacion"))
                        {
                            DateTime hoy = DateTime.Now;
                            fechOperacion = hoy.ToString("dd-MM-yyyy HH:mm");
                            datoscompletos += fechOperacion + ",";
                        }
                        else
                        {
                            fechOperacion = "";
                            datoscompletos += fechOperacion + ",";
                        }

                    }
                    if (columna.Equals("Comentarios"))
                    {
                        if (Inventario.listaConceptosSeleccionados.Contains("Comentarios"))
                        {
                            comentarios = Inventario.DTDatos.Rows[RowsDatosInventario]["Comentarios"].ToString();
                            if (string.IsNullOrWhiteSpace(comentarios))
                            {
                                comentarios = "---";
                            }
                            datoscompletos += comentarios + ",";
                        }
                        else
                        {
                            comentarios = "";
                            datoscompletos += comentarios + ",";
                        }

                    }
                    if (columna.Equals("Ubicacion"))
                    {
                        var DTDetalles = cn.CargarDatos($"SELECT DPG.IDProducto, DetGral.Descripcion, IF(DetGral.ChckName = '' OR DetGral.ChckName IS NULL,'S/A',DetGral.ChckName)AS 'Detalle' FROM detallesproductogenerales AS DPG INNER JOIN detallegeneral AS DetGral ON ( DetGral.ID = DPG.IDDetalleGral ) WHERE DPG.IDProducto = '{ID}' AND DetGral.ChckName = 'UBICACION'");

                        if (!DTDetalles.Rows.Count.Equals(0))
                        {
                            ubicacion = DTDetalles.Rows[0]["Descripcion"].ToString();
                        }
                        else
                        {
                            ubicacion = "S/A";
                        }
                        datoscompletos += ubicacion + ",";
                    }
                    if (columna.Equals("Color"))
                    {
                        var DTDetalles = cn.CargarDatos($"SELECT DPG.IDProducto, DetGral.Descripcion, IF(DetGral.ChckName = '' OR DetGral.ChckName IS NULL,'S/A',DetGral.ChckName)AS 'Detalle' FROM detallesproductogenerales AS DPG INNER JOIN detallegeneral AS DetGral ON ( DetGral.ID = DPG.IDDetalleGral ) WHERE DPG.IDProducto = '{ID}' AND DetGral.ChckName = 'COLOR'");

                        if (!DTDetalles.Rows.Count.Equals(0))
                        {
                            color = DTDetalles.Rows[0]["Descripcion"].ToString();
                        }
                        else
                        {
                            color = "S/A";
                        }
                        datoscompletos += color + ",";
                    }

                }
                contadorRows++;
                datoscompletos = datoscompletos.TrimEnd(',');
                var jaja = datoscompletos.Split(',');
                int contadorDatos = 0;
                DTFinal.Rows.Add();
                foreach (var item in DTFinal.Columns)
                {
                    string columna = item.ToString();
                    DTFinal.Rows[contadorROWSTabñaFinal][columna] = jaja[contadorDatos];
                    contadorDatos++;
                }
                datoscompletos = string.Empty;
                contadorROWSTabñaFinal++;
                RowsDatosInventario++;
            }
        }
    }
}
