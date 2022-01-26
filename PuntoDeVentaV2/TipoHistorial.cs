using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class TipoHistorial : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public int tipoRespuesta { get; set; }

        private string fechaInicial;
        private string fechaFinal;
        private int idProducto;

        public TipoHistorial(int idProducto)
        {
            InitializeComponent();

            this.idProducto = idProducto;
        }

        private void btnHistorialCompras_Click(object sender, EventArgs e)
        {
            tipoRespuesta = 1;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnHistorialVentas_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Estamos trabajando en este apartado", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            using (var fechas = new FechasReportes("Productos"))
            {
                var respuesta = fechas.ShowDialog();

                if (respuesta == DialogResult.OK)
                {
                    tipoRespuesta = 2;
                    fechaInicial = fechas.fechaInicial;
                    fechaFinal = fechas.fechaFinal;

                    if (Utilidades.AdobeReaderInstalado())
                    {
                        GenerarReporte();
                    }
                    else
                    {
                        Utilidades.MensajeAdobeReader();
                    }
                }
            }
        }


        private void GenerarReporte()
        {
            //Consulta para obtener los registros del historial de ventas del producto
            MySqlConnection sql_con;
            //MySqlCommand sql_cmd;
            //MySqlDataReader dr;

            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                sql_con = new MySqlConnection($"datasource={servidor};port=6666;username=root;password=;database=pudve;");
            }
            else
            {
                sql_con = new MySqlConnection($"datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            }

            //Validar si es combo
            //var verificarCombo = verificarSiTieneRelacionCombo(idProducto);

            //var consultaProductosRelacionados = string.Empty;

            //if (!string.IsNullOrEmpty(verificarCombo))
            //{
            //    consultaProductosRelacionados = $@"SELECT V.Folio, V.Serie, V.Total, V.FechaOperacion, P.IDVenta, P.Nombre, P.Cantidad, P.Precio, V.IDEmpleado, P.IDProducto FROM Ventas V INNER JOIN ProductosVenta P ON V.ID = P.IDVenta WHERE V.IDUsuario = {FormPrincipal.userID} AND V.Status = 1 AND DATE(V.FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' AND P.IDProducto = '{verificarCombo}'";
            //}


            //var consulta = $@"SELECT V.Folio, V.Serie, V.Total, V.FechaOperacion, P.IDVenta, P.Nombre, P.Cantidad, P.Precio, V.IDEmpleado, P.IDProducto FROM Ventas V INNER JOIN ProductosVenta P ON V.ID = P.IDVenta WHERE V.IDUsuario = {FormPrincipal.userID} AND V.Status = 1 AND DATE(V.FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' AND P.IDProducto = {idProducto}";
            //var consulta = $"";

            //sql_con.Open();
            //sql_cmd = new MySqlCommand(consulta, sql_con
            //dr = sql_cmd.ExecuteReader();

            //var realizarConsulta = cn.CargarDatos(consulta);

            //if (!string.IsNullOrWhiteSpace(consultaProductosRelacionados))
            //{
            //    var consultaCombos = cn.CargarDatos(consultaProductosRelacionados);
            //    //Unir Resultados de las consultas
            //    realizarConsulta.Merge(consultaCombos);
            //    consultaCombos.Dispose();
            //    consultaCombos = null;
            //}

            var realizarConsulta = cn.CargarDatos(cs.CargarHistorialDeVentas(fechaInicial, fechaFinal, idProducto));

            if (/*dr.HasRows*/!realizarConsulta.Rows.Count.Equals(0))
            {
                var datos = FormPrincipal.datosUsuario;

                var colorFuenteNegrita = new BaseColor(Color.Black);
                var colorFuenteBlanca = new BaseColor(Color.White);

                var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
                var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
                var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, colorFuenteNegrita);

                int anchoLogo = 110;
                int altoLogo = 60;

                var numRow = 0;
                var rutaArchivo = string.Empty;
                var fechaActual = DateTime.Now;

                if (!string.IsNullOrWhiteSpace(servidor))
                {
                    rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\Historial\reporte_historial_venta_producto.pdf";
                }
                else
                {
                    rutaArchivo = $@"C:\Archivos PUDVE\Reportes\Historial\reporte_historial_venta_producto.pdf";
                }
                
                Document reporte = new Document(PageSize.A3);
                PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

                string logotipo = datos[11];
                //string encabezado = $"\n{datos[1]} {datos[2]} {datos[3]}, {datos[4]}, {datos[5]}\nCol. {datos[6]} C.P. {datos[7]}\nRFC: {datos[8]}\n{datos[9]}\nTel. {datos[10]}\n\n";

                reporte.Open();

                //Validación para verificar si existe logotipo
                if (logotipo != "")
                {
                    //logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + logotipo;

                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        logotipo = $@"\\{servidor}\Archivos PUDVE\MisDatos\Usuarios\" + logotipo;
                    }
                    else
                    {
                        logotipo = $@"C:\Archivos PUDVE\MisDatos\Usuarios\" + logotipo;
                    }

                    if (File.Exists(logotipo))
                    {
                        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logotipo);
                        logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                        logo.ScaleAbsolute(anchoLogo, altoLogo);
                        reporte.Add(logo);
                    }
                }

                Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
                Paragraph Usuario = new Paragraph("");
                Paragraph subTitulo = new Paragraph("");

                string UsuarioActivo = string.Empty;
                string datoEmpleado = string.Empty;

                //using (DataTable dtDataUsr = cn.CargarDatos(cs.UsuarioRazonSocialNombreCompleto(Convert.ToString(FormPrincipal.userID))))
                //{
                //    if (!dtDataUsr.Rows.Count.Equals(0))
                //    {
                //        foreach (DataRow drDataUsr in dtDataUsr.Rows)
                //        {
                //            UsuarioActivo = drDataUsr["Usuario"].ToString();
                //        }
                //    }
                //}

                var getNombreAdmin = cn.CargarDatos(cs.UsuarioRazonSocialNombreCompleto(FormPrincipal.userID.ToString()));
                var nameAdmin = string.Empty;
                if (!getNombreAdmin.Rows.Count.Equals(0)) { nameAdmin = getNombreAdmin.Rows[0]["Usuario"].ToString(); }

                if (FormPrincipal.userNickName.Contains('@'))
                {
                    datoEmpleado = cs.buscarNombreCliente(FormPrincipal.userNickName);
                }

                Paragraph Empleado = new Paragraph($"Empleado: {datoEmpleado}", fuenteNormal);
                Usuario = new Paragraph($"USUARIO: ADMIN({ nameAdmin })", fuenteNegrita);

                subTitulo = new Paragraph($"REPORTE HISTORIAL VENTA PRODUCTO\nRANGO DE FECHAS\nDEL: {fechaInicial} HASTA: {fechaFinal}\nSECCION DE PRODUCTOS\n\nFecha: {fechaActual.ToString("yyyy-MM-dd HH:mm:ss")}\n\n\n", fuenteNormal);
                //Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

                titulo.Alignment = Element.ALIGN_CENTER;
                Usuario.Alignment = Element.ALIGN_CENTER;
                if (FormPrincipal.userNickName.Contains('@')) { Empleado.Alignment = Element.ALIGN_CENTER; }
                subTitulo.Alignment = Element.ALIGN_CENTER;
                //domicilio.Alignment = Element.ALIGN_CENTER;
                //domicilio.SetLeading(10, 0);

                /***************************************
		         ** Tabla con los productos ajustados **
		         ***************************************/
                float[] anchoColumnas = new float[] { 30f, 300f, 80f, 100f, 100f, 80f, 80f, 80f, 80f, 130f, 100f };

                PdfPTable tabla = new PdfPTable(11);
                tabla.WidthPercentage = 100;
                tabla.SetWidths(anchoColumnas);

                PdfPCell colNumProducto = new PdfPCell(new Phrase("No:", fuenteNegrita));
                colNumProducto.BorderWidth = 1;
                colNumProducto.BackgroundColor = new BaseColor(Color.SkyBlue);
                colNumProducto.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colProducto = new PdfPCell(new Phrase("Producto / Servicio / Combo", fuenteNegrita));
                colProducto.BorderWidth = 1;
                colProducto.BackgroundColor = new BaseColor(Color.SkyBlue);
                colProducto.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colTipo = new PdfPCell(new Phrase("Tipo", fuenteNegrita));
                colTipo.BorderWidth = 1;
                colTipo.BackgroundColor = new BaseColor(Color.SkyBlue);
                colTipo.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoAsociado = new PdfPCell(new Phrase("Código Asociado", fuenteNegrita));
                colCodigoAsociado.BorderWidth = 1;
                colCodigoAsociado.BackgroundColor = new BaseColor(Color.SkyBlue);
                colCodigoAsociado.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colEmpleado = new PdfPCell(new Phrase("Empleado", fuenteNegrita));
                colEmpleado.BorderWidth = 1;
                colEmpleado.BackgroundColor = new BaseColor(Color.SkyBlue);
                colEmpleado.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCantidad = new PdfPCell(new Phrase("Cantidad", fuenteNegrita));
                colCantidad.BorderWidth = 1;
                colCantidad.BackgroundColor = new BaseColor(Color.SkyBlue);
                colCantidad.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecio = new PdfPCell(new Phrase("Precio", fuenteNegrita));
                colPrecio.BorderWidth = 1;
                colPrecio.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPrecio.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFolioSerie = new PdfPCell(new Phrase("Folio / Serie", fuenteNegrita));
                colFolioSerie.BorderWidth = 1;
                colFolioSerie.BackgroundColor = new BaseColor(Color.SkyBlue);
                colFolioSerie.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colTotalVenta = new PdfPCell(new Phrase("Total Venta", fuenteNegrita));
                colTotalVenta.BorderWidth = 1;
                colTotalVenta.BackgroundColor = new BaseColor(Color.SkyBlue);
                colTotalVenta.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaOperacion = new PdfPCell(new Phrase("Fecha de Operación", fuenteNegrita));
                colFechaOperacion.BorderWidth = 1;
                colFechaOperacion.BackgroundColor = new BaseColor(Color.SkyBlue);
                colFechaOperacion.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colVendido = new PdfPCell(new Phrase("Vendido", fuenteNegrita));
                colVendido.BorderWidth = 1;
                colVendido.BackgroundColor = new BaseColor(Color.SkyBlue);
                colVendido.HorizontalAlignment = Element.ALIGN_CENTER;

                tabla.AddCell(colNumProducto);
                tabla.AddCell(colProducto);
                tabla.AddCell(colTipo);
                tabla.AddCell(colCodigoAsociado);
                tabla.AddCell(colEmpleado);
                tabla.AddCell(colCantidad);
                tabla.AddCell(colPrecio);
                tabla.AddCell(colFolioSerie);
                tabla.AddCell(colTotalVenta);
                tabla.AddCell(colFechaOperacion);
                tabla.AddCell(colVendido);

                float totalCantidad = 0;
                float totalPrecio = 0;
                float totalVenta = 0;

                //while (dr.Read())
                //{
                foreach (DataRow dt in realizarConsulta.Rows) {
                    //var nombre = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    //var cantidad = dr.GetValue(dr.GetOrdinal("Cantidad")).ToString();
                    //var precio = float.Parse(dr.GetValue(dr.GetOrdinal("Precio")).ToString());
                    //var folioSerie = dr.GetValue(dr.GetOrdinal("Folio")) + " " + dr.GetValue(dr.GetOrdinal("Serie"));
                    //var venta = float.Parse(dr.GetValue(dr.GetOrdinal("Total")).ToString());
                    //var fechaOp = (DateTime)dr.GetValue(dr.GetOrdinal("FechaOperacion"));
                    //var fechaOperacion = fechaOp.ToString("yyyy-MM-dd HH:mm tt");
                    //var idEmpleadoABuscar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDEmpleado")));
                    //var idProductABuscar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDProducto")));
                    var nombre = dt["Nombre"].ToString();
                    var cantidad = dt["Cantidad"].ToString();
                    var precio = float.Parse(dt["Precio"].ToString());
                    var folioSerie = dt["Folio"] + " " + dt["Serie"].ToString();
                    var venta = float.Parse(dt["Total"].ToString());
                    var fechaOp = (DateTime)dt["FechaOperacion"];
                    var fechaOperacion = fechaOp.ToString("yyyy-MM-dd HH:mm tt");
                    var idEmpleadoABuscar = Convert.ToInt32(dt["IDEmpleado"].ToString());
                    var idProductABuscar = Convert.ToInt32(dt["IDProducto"].ToString());
                    var tipoDeVenta = dt["Vendido"].ToString();

                    var datosProducto = cn.BuscarProducto(Convert.ToInt32(idProductABuscar), FormPrincipal.userID);
                    var nombreProducto = datosProducto[1];
                    var tipoProducto = datosProducto[5];

                    string emp = string.Empty;
                    if (idEmpleadoABuscar != 0)
                    {
                        emp = "empleado";
                    }
                    else
                    {
                        emp = "admin";
                    }

                    var nameEmpleado = cs.consultarUsuarioEmpleado(idEmpleadoABuscar, emp);

                    if (emp.Equals("admin"))
                    {
                        nameEmpleado = $"ADMIN ({nameEmpleado})";
                    }

                    var codigosAsociados = string.Empty;

                    //if (tipoProducto.Equals("PQ"))
                    //{
                    //    codigosAsociados = cs.ObtenerCodigosAsociados(idProducto);
                    //}
                    //else
                    //{
                    //    codigosAsociados = "N/A";
                    //}

                    //if (tipoProducto.Equals("PQ"))
                    //{
                    //    tipoProducto = "Combo";
                    //}
                    //else if (tipoProducto.Equals("P"))
                    //{
                    //    tipoProducto = "Producto";
                    //}
                    //else if (tipoProducto.Equals("S"))
                    //{
                    //    tipoProducto = "Servicio";
                    //}

                    if (tipoProducto.Equals("PQ"))
                    {
                        codigosAsociados = cs.ObtenerCodigosAsociados(idProducto);
                        tipoProducto = "Combo";
                    }
                    else
                    {
                        codigosAsociados = "N/A";
                        if (tipoProducto.Equals("P"))
                        {
                            tipoProducto = "Producto";
                        }
                        else if (tipoProducto.Equals("S"))
                        {
                            tipoProducto = "Servicio";
                        }
                    }

                    //totalCantidad += float.Parse(cantidad);
                    //totalPrecio += precio;
                    //totalVenta += venta;

                    numRow++;
                    
                    if (datosProducto[5].Equals("PQ"))
                    {
                        var buscarProducto = cn.CargarDatos($"SELECT * FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idProducto}'");

                        if (!buscarProducto.Rows.Count.Equals(0))
                        {
                            nombre = buscarProducto.Rows[0]["Nombre"].ToString();
                            if (buscarProducto.Rows[0]["Tipo"].ToString().Equals("P"))
                            {
                                tipoProducto = "Producto";
                            }
                            else if (buscarProducto.Rows[0]["Tipo"].ToString().Equals("PQ"))
                            {
                                tipoProducto = "Combo";
                            }
                            else if (buscarProducto.Rows[0]["Tipo"].ToString().Equals("S"))
                            {
                                tipoProducto = "Servicio";
                            }
                            //codigosAsociados = string.Empty;
                            precio = float.Parse(buscarProducto.Rows[0]["Precio"].ToString());
                        }
                        else
                        {
                            codigosAsociados = "N/A";
                        }
                    }

                    //=========================================================================================
                    PdfPCell colNumProductoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                    colNumProductoTmp.BorderWidth = 1;
                    colNumProductoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colProductoTmp = new PdfPCell(new Phrase(nombre, fuenteNormal));
                    colProductoTmp.BorderWidth = 1;
                    colProductoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colTipoTmp = new PdfPCell(new Phrase(tipoProducto, fuenteNormal));
                    colTipoTmp.BorderWidth = 1;
                    colTipoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    if (string.IsNullOrWhiteSpace(codigosAsociados))
                    {
                        codigosAsociados = "N/A";
                    }

                    PdfPCell colCodigosAsociadosTmp = new PdfPCell(new Phrase(codigosAsociados, fuenteNormal));
                    colCodigosAsociadosTmp.BorderWidth = 1;
                    colCodigosAsociadosTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colEmpleadoTmp = new PdfPCell(new Phrase(nameEmpleado, fuenteNormal));
                    colEmpleadoTmp.BorderWidth = 1;
                    colEmpleadoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colCantidadTmp = new PdfPCell(new Phrase(cantidad, fuenteNormal));
                    colCantidadTmp.BorderWidth = 1;
                    totalCantidad += float.Parse(cantidad);
                    colCantidadTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colPrecioTmp = new PdfPCell(new Phrase("$" + precio.ToString("N2"), fuenteNormal));
                    colPrecioTmp.BorderWidth = 1;
                    totalPrecio += precio;
                    colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colFolioSerieTmp = new PdfPCell(new Phrase(folioSerie, fuenteNormal));
                    colFolioSerieTmp.BorderWidth = 1;
                    colFolioSerieTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colTotalVentaTmp = new PdfPCell(new Phrase("$" + venta.ToString("N2"), fuenteNormal));
                    colTotalVentaTmp.BorderWidth = 1;
                    totalVenta += venta;
                    colTotalVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colFechaOperacionTmp = new PdfPCell(new Phrase(fechaOperacion, fuenteNormal));
                    colFechaOperacionTmp.BorderWidth = 1;
                    colFechaOperacionTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colTipoDeVentaTmp = new PdfPCell(new Phrase(tipoDeVenta, fuenteNormal));
                    colTipoDeVentaTmp.BorderWidth = 1;
                    colTipoDeVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    tabla.AddCell(colNumProductoTmp);
                    tabla.AddCell(colProductoTmp);
                    tabla.AddCell(colTipoTmp);
                    tabla.AddCell(colCodigosAsociadosTmp);
                    tabla.AddCell(colEmpleadoTmp);
                    tabla.AddCell(colCantidadTmp);
                    tabla.AddCell(colPrecioTmp);
                    tabla.AddCell(colFolioSerieTmp);
                    tabla.AddCell(colTotalVentaTmp);
                    tabla.AddCell(colFechaOperacionTmp);
                    tabla.AddCell(colTipoDeVentaTmp);
                }

                if (totalCantidad > 0 || totalPrecio > 0 || totalVenta > 0)
                {
                    PdfPCell colAuxNumProd = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    colAuxNumProd.BorderWidth = 0;
                    colAuxNumProd.Padding = 3;
                    
                    PdfPCell colAuxProd = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    colAuxProd.BorderWidth = 0;
                    colAuxProd.Padding = 3;

                    PdfPCell colAuxTipo = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    colAuxTipo.BorderWidth = 0;
                    colAuxTipo.Padding = 3;

                    PdfPCell colAuxcodigoAsociado = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    colAuxcodigoAsociado.BorderWidth = 0;
                    colAuxcodigoAsociado.Padding = 3;

                    PdfPCell colAuxEmpleado = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    colAuxEmpleado.BorderWidth = 0;
                    colAuxEmpleado.Padding = 3;

                    PdfPCell colTotalCantidad = new PdfPCell(new Phrase(totalCantidad.ToString("N2"), fuenteTotales));
                    colTotalCantidad.BorderWidthLeft = 0;
                    colTotalCantidad.BorderWidthTop = 0;
                    colTotalCantidad.BorderWidthRight = 0;
                    colTotalCantidad.BorderWidthBottom = 1;
                    colTotalCantidad.HorizontalAlignment = Element.ALIGN_CENTER;
                    colTotalCantidad.BackgroundColor = new BaseColor(Color.SkyBlue);
                    colTotalCantidad.Padding = 3;
                    
                    PdfPCell colTotalPrecio = new PdfPCell(new Phrase("$" + totalPrecio.ToString("N2"), fuenteTotales));
                    colTotalPrecio.BorderWidthLeft = 0;
                    colTotalPrecio.BorderWidthTop = 0;
                    colTotalPrecio.BorderWidthRight = 0;
                    colTotalPrecio.BorderWidthBottom = 1;
                    colTotalPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
                    colTotalPrecio.BackgroundColor = new BaseColor(Color.SkyBlue);
                    colTotalPrecio.Padding = 3;
                    
                    PdfPCell colAuxFolioSerie = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    colAuxFolioSerie.BorderWidth = 0;
                    colAuxFolioSerie.Padding = 3;
                    
                    PdfPCell colAuxTotalVenta = new PdfPCell(new Phrase("$" + totalVenta.ToString("N2"), fuenteTotales));
                    colAuxTotalVenta.BorderWidthLeft = 0;
                    colAuxTotalVenta.BorderWidthTop = 0;
                    colAuxTotalVenta.BorderWidthRight = 0;
                    colAuxTotalVenta.BorderWidthBottom = 1;
                    colAuxTotalVenta.HorizontalAlignment = Element.ALIGN_CENTER;
                    colAuxTotalVenta.BackgroundColor = new BaseColor(Color.SkyBlue);
                    colAuxTotalVenta.Padding = 3;
                    
                    PdfPCell colAuxFechaOperacion = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    colAuxFechaOperacion.BorderWidth = 0;
                    colAuxFechaOperacion.Padding = 3;
                    
                    PdfPCell colAuxTipoVenta = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    colAuxTipoVenta.BorderWidth = 0;
                    colAuxTipoVenta.Padding = 3;
                    
                    tabla.AddCell(colAuxNumProd);
                    tabla.AddCell(colAuxProd);
                    tabla.AddCell(colAuxTipo);
                    tabla.AddCell(colAuxcodigoAsociado);
                    tabla.AddCell(colAuxEmpleado);
                    tabla.AddCell(colTotalCantidad);
                    tabla.AddCell(colTotalPrecio);
                    tabla.AddCell(colAuxFolioSerie);
                    tabla.AddCell(colAuxTotalVenta);
                    tabla.AddCell(colAuxFechaOperacion);
                    tabla.AddCell(colAuxTipoVenta);
                }

                /******************************************
                 ** Fin de la tabla                      **
                 ******************************************/

                reporte.Add(titulo);
                reporte.Add(Usuario);
                if (FormPrincipal.userNickName.Contains('@'))
                {
                    reporte.Add(Empleado);
                }
                reporte.Add(subTitulo);
                reporte.Add(tabla);

                reporte.AddTitle("Reporte Historial Ventas Producto");
                reporte.AddAuthor("PUDVE");
                reporte.Close();
                writer.Close();

                VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
                vr.ShowDialog();
            }
            else
            {
                MessageBox.Show("No hay informacion disponible para las fechas seleccionadas", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Close();
        }

        private string verificarSiTieneRelacionCombo(int idCombo)
        {
            var query = cn.CargarDatos($"SELECT IDServicio, NombreProducto FROM ProductosdeServicios WHERE IDProducto = '{idCombo}'");
            var idComboFinal = string.Empty;

            if (!query.Rows.Count.Equals(0))
            {
                idComboFinal = query.Rows[0]["IDServicio"].ToString();
            }

            return idComboFinal;
        }

        private void botonRedondo1_Click(object sender, EventArgs e)
        {
            HistorialStockProductos historial = new HistorialStockProductos();
            historial.Show();
        }

        private void TipoHistorial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }
    }
}
