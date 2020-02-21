using iTextSharp.text;
using iTextSharp.text.pdf;
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
    public partial class Reportes : Form
    {
        Conexion cn = new Conexion();

        private string fechaInicial = string.Empty;
        private string fechaFinal = string.Empty;
        public Reportes()
        {
            InitializeComponent();
        }

        private void btnHistorialPrecios_Click(object sender, EventArgs e)
        {
            using (var fechas = new FechasReportes())
            {
                var respuesta = fechas.ShowDialog();

                if (respuesta == DialogResult.OK)
                {
                    fechaInicial = fechas.fechaInicial;
                    fechaFinal = fechas.fechaFinal;

                    if (!string.IsNullOrWhiteSpace(fechaInicial))
                    {
                        if (!string.IsNullOrWhiteSpace(fechaFinal))
                        {
                            GenerarReportePrecios();
                        }
                    }
                }
            }
        }

        private void GenerarReportePrecios()
        {
            var datos = FormPrincipal.datosUsuario;

            var colorFuenteNegrita = new BaseColor(Color.Black);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            int anchoLogo = 110;
            int altoLogo = 60;

            var fechaActual = DateTime.Now;
            var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = $@"C:\Archivos PUDVE\Reportes\Historial\reporte_historial_precios_{fechaActual.ToString("yyyyMMddHHmmss")}.pdf";

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
            Paragraph subTitulo = new Paragraph("REPORTE HISTORIAL PRECIOS\nFecha: " + fechaActual.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);
            //Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;
            //domicilio.Alignment = Element.ALIGN_CENTER;
            //domicilio.SetLeading(10, 0);

            /***************************************
             ** Tabla con los productos ajustados **
             ***************************************/
            float[] anchoColumnas = new float[] { 300f, 80f, 80f, 100f, 80f };

            PdfPTable tabla = new PdfPTable(5);
            tabla.WidthPercentage = 100;
            tabla.SetWidths(anchoColumnas);

            PdfPCell colProducto = new PdfPCell(new Phrase("Producto / Servicio / Combo", fuenteNegrita));
            colProducto.BorderWidth = 1;
            colProducto.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colPrecioAnterior = new PdfPCell(new Phrase("Precio Anterior", fuenteNegrita));
            colPrecioAnterior.BorderWidth = 1;
            colPrecioAnterior.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colPrecioNuevo = new PdfPCell(new Phrase("Precio Nuevo", fuenteNegrita));
            colPrecioNuevo.BorderWidth = 1;
            colPrecioNuevo.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colOrigen = new PdfPCell(new Phrase("Origen", fuenteNegrita));
            colOrigen.BorderWidth = 1;
            colOrigen.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colFechaOperacion = new PdfPCell(new Phrase("Fecha de Operación", fuenteNegrita));
            colFechaOperacion.BorderWidth = 1;
            colFechaOperacion.HorizontalAlignment = Element.ALIGN_CENTER;

            tabla.AddCell(colProducto);
            tabla.AddCell(colPrecioAnterior);
            tabla.AddCell(colPrecioNuevo);
            tabla.AddCell(colOrigen);
            tabla.AddCell(colFechaOperacion);


            //Consulta para obtener los registros del Historial de compras
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                sql_con = new SQLiteConnection("Data source=//" + servidor + @"\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }
            else
            {
                sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }

            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT * FROM HistorialPrecios WHERE IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' ORDER BY FechaOperacion DESC", sql_con);
            dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                var idProducto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDProducto")));
                var datosProducto = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                var nombreProducto = datosProducto[1];

                var precioAnterior = float.Parse(dr.GetValue(dr.GetOrdinal("PrecioAnterior")).ToString());
                var precioNuevo = float.Parse(dr.GetValue(dr.GetOrdinal("PrecioNuevo")).ToString());
                var origen = dr.GetValue(dr.GetOrdinal("Origen")).ToString();

                DateTime fechaOp = (DateTime)dr.GetValue(dr.GetOrdinal("FechaOperacion"));
                var fechaOperacion = fechaOp.ToString("yyyy-MM-dd HH:mm tt");

                PdfPCell colProductoTmp = new PdfPCell(new Phrase(nombreProducto, fuenteNormal));
                colProductoTmp.BorderWidth = 1;
                colProductoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioAnteriorTmp = new PdfPCell(new Phrase("$" + precioAnterior.ToString("N2"), fuenteNormal));
                colPrecioAnteriorTmp.BorderWidth = 1;
                colPrecioAnteriorTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioNuevoTmp = new PdfPCell(new Phrase("$" + precioNuevo.ToString("N2"), fuenteNormal));
                colPrecioNuevoTmp.BorderWidth = 1;
                colPrecioNuevoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colOrigenTmp = new PdfPCell(new Phrase(origen, fuenteNormal));
                colOrigenTmp.BorderWidth = 1;
                colOrigenTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaOperacionTmp = new PdfPCell(new Phrase(fechaOperacion, fuenteNormal));
                colFechaOperacionTmp.BorderWidth = 1;
                colFechaOperacionTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tabla.AddCell(colProductoTmp);
                tabla.AddCell(colPrecioAnteriorTmp);
                tabla.AddCell(colPrecioNuevoTmp);
                tabla.AddCell(colOrigenTmp);
                tabla.AddCell(colFechaOperacionTmp);
            }

            /******************************************
             ** Fin de la tabla                      **
             ******************************************/

            reporte.Add(titulo);
            reporte.Add(subTitulo);
            //reporte.Add(domicilio);
            reporte.Add(tabla);

            reporte.AddTitle("Reporte Historial Precios");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }
    }
}
