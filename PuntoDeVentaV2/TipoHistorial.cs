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
            using (var fechas = new FechasReportes())
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
            MySqlCommand sql_cmd;
            MySqlDataReader dr;

            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                sql_con = new MySqlConnection($"datasource={servidor};port=6666;username=root;password=;database=pudve;");
            }
            else
            {
                sql_con = new MySqlConnection($"datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            }

            var consulta = $@"SELECT V.Folio, V.Serie, V.Total, V.FechaOperacion, P.IDVenta, P.Nombre, P.Cantidad, P.Precio FROM Ventas V INNER JOIN ProductosVenta P ON V.ID = P.IDVenta WHERE V.IDUsuario = {FormPrincipal.userID} AND V.Status = 1 AND DATE(V.FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' AND P.IDProducto = {idProducto}";

            sql_con.Open();
            sql_cmd = new MySqlCommand(consulta, sql_con);
            dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                var datos = FormPrincipal.datosUsuario;

                var colorFuenteNegrita = new BaseColor(Color.Black);
                var colorFuenteBlanca = new BaseColor(Color.White);

                var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
                var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
                var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, colorFuenteBlanca);

                int anchoLogo = 110;
                int altoLogo = 60;

                var fechaActual = DateTime.Now;
                var rutaArchivo = $@"C:\Archivos PUDVE\Reportes\Historial\reporte_historial_venta_producto.pdf";

                Document reporte = new Document(PageSize.A3);
                PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

                string logotipo = datos[11];
                //string encabezado = $"\n{datos[1]} {datos[2]} {datos[3]}, {datos[4]}, {datos[5]}\nCol. {datos[6]} C.P. {datos[7]}\nRFC: {datos[8]}\n{datos[9]}\nTel. {datos[10]}\n\n";

                reporte.Open();

                //Validación para verificar si existe logotipo
                if (logotipo != "")
                {
                    logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + logotipo;

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

                using (DataTable dtDataUsr = cn.CargarDatos(cs.UsuarioRazonSocialNombreCompleto(Convert.ToString(FormPrincipal.userID))))
                {
                    if (!dtDataUsr.Rows.Count.Equals(0))
                    {
                        foreach (DataRow drDataUsr in dtDataUsr.Rows)
                        {
                            UsuarioActivo = drDataUsr["Usuario"].ToString();
                        }
                    }
                }

                Usuario = new Paragraph("USUARIO: " + UsuarioActivo, fuenteNegrita);

                subTitulo = new Paragraph("REPORTE HISTORIAL VENTA PRODUCTO\nFecha: " + fechaActual.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);
                //Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

                titulo.Alignment = Element.ALIGN_CENTER;
                Usuario.Alignment = Element.ALIGN_CENTER;
                subTitulo.Alignment = Element.ALIGN_CENTER;
                //domicilio.Alignment = Element.ALIGN_CENTER;
                //domicilio.SetLeading(10, 0);

                /***************************************
		         ** Tabla con los productos ajustados **
		         ***************************************/
                float[] anchoColumnas = new float[] { 300f, 80f, 80f, 80f, 80f, 100f };

                PdfPTable tabla = new PdfPTable(6);
                tabla.WidthPercentage = 100;
                tabla.SetWidths(anchoColumnas);

                PdfPCell colProducto = new PdfPCell(new Phrase("Producto / Servicio / Combo", fuenteNegrita));
                colProducto.BorderWidth = 1;
                colProducto.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCantidad = new PdfPCell(new Phrase("Cantidad", fuenteNegrita));
                colCantidad.BorderWidth = 1;
                colCantidad.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecio = new PdfPCell(new Phrase("Precio", fuenteNegrita));
                colPrecio.BorderWidth = 1;
                colPrecio.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFolioSerie = new PdfPCell(new Phrase("Folio / Serie", fuenteNegrita));
                colFolioSerie.BorderWidth = 1;
                colFolioSerie.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colTotalVenta = new PdfPCell(new Phrase("Total Venta", fuenteNegrita));
                colTotalVenta.BorderWidth = 1;
                colTotalVenta.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaOperacion = new PdfPCell(new Phrase("Fecha de Operación", fuenteNegrita));
                colFechaOperacion.BorderWidth = 1;
                colFechaOperacion.HorizontalAlignment = Element.ALIGN_CENTER;

                tabla.AddCell(colProducto);
                tabla.AddCell(colCantidad);
                tabla.AddCell(colPrecio);
                tabla.AddCell(colFolioSerie);
                tabla.AddCell(colTotalVenta);
                tabla.AddCell(colFechaOperacion);


                float totalCantidad = 0;
                float totalPrecio = 0;

                while (dr.Read())
                {
                    var nombre = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                    var cantidad = dr.GetValue(dr.GetOrdinal("Cantidad")).ToString();
                    var precio = float.Parse(dr.GetValue(dr.GetOrdinal("Precio")).ToString());
                    var folioSerie = dr.GetValue(dr.GetOrdinal("Folio")) + " " + dr.GetValue(dr.GetOrdinal("Serie"));
                    var totalVenta = float.Parse(dr.GetValue(dr.GetOrdinal("Total")).ToString());
                    var fechaOp = (DateTime)dr.GetValue(dr.GetOrdinal("FechaOperacion"));
                    var fechaOperacion = fechaOp.ToString("yyyy-MM-dd HH:mm tt");

                    totalCantidad += float.Parse(cantidad);
                    totalPrecio += precio;

                    //=========================================================================================
                    PdfPCell colProductoTmp = new PdfPCell(new Phrase(nombre, fuenteNormal));
                    colProductoTmp.BorderWidth = 1;
                    colProductoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colCantidadTmp = new PdfPCell(new Phrase(cantidad, fuenteNormal));
                    colCantidadTmp.BorderWidth = 1;
                    colCantidadTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colPrecioTmp = new PdfPCell(new Phrase("$" + precio.ToString("N2"), fuenteNormal));
                    colPrecioTmp.BorderWidth = 1;
                    colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colFolioSerieTmp = new PdfPCell(new Phrase(folioSerie, fuenteNormal));
                    colFolioSerieTmp.BorderWidth = 1;
                    colFolioSerieTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colTotalVentaTmp = new PdfPCell(new Phrase("$" + totalVenta.ToString("N2"), fuenteNormal));
                    colTotalVentaTmp.BorderWidth = 1;
                    colTotalVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colFechaOperacionTmp = new PdfPCell(new Phrase(fechaOperacion, fuenteNormal));
                    colFechaOperacionTmp.BorderWidth = 1;
                    colFechaOperacionTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    tabla.AddCell(colProductoTmp);
                    tabla.AddCell(colCantidadTmp);
                    tabla.AddCell(colPrecioTmp);
                    tabla.AddCell(colFolioSerieTmp);
                    tabla.AddCell(colTotalVentaTmp);
                    tabla.AddCell(colFechaOperacionTmp);
                }

                PdfPCell colAuxiliar1 = new PdfPCell();
                colAuxiliar1.BorderWidth = 0;
                colAuxiliar1.BackgroundColor = new BaseColor(Color.Red);
                colAuxiliar1.Padding = 3;

                PdfPCell colTotalCantidad = new PdfPCell(new Phrase(totalCantidad.ToString(), fuenteTotales));
                colTotalCantidad.BorderWidth = 0;
                colTotalCantidad.HorizontalAlignment = Element.ALIGN_CENTER;
                colTotalCantidad.BackgroundColor = new BaseColor(Color.Red);
                colTotalCantidad.Padding = 3;

                PdfPCell colTotalPrecio = new PdfPCell(new Phrase("$" + totalPrecio.ToString("N2"), fuenteTotales));
                colTotalPrecio.BorderWidth = 0;
                colTotalPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
                colTotalPrecio.BackgroundColor = new BaseColor(Color.Red);
                colTotalPrecio.Padding = 3;

                PdfPCell colAuxiliar2 = new PdfPCell();
                colAuxiliar2.BorderWidth = 0;
                colAuxiliar2.Colspan = 3;
                colAuxiliar2.BackgroundColor = new BaseColor(Color.Red);
                colAuxiliar2.Padding = 3;

                tabla.AddCell(colAuxiliar1);
                tabla.AddCell(colTotalCantidad);
                tabla.AddCell(colTotalPrecio);
                tabla.AddCell(colAuxiliar2);

                /******************************************
                 ** Fin de la tabla                      **
                 ******************************************/

                reporte.Add(titulo);
                reporte.Add(Usuario);
                reporte.Add(subTitulo);
                //reporte.Add(domicilio);
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
    }
}
