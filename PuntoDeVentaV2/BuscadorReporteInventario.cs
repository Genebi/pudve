using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
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
    public partial class BuscadorReporteInventario : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        public BuscadorReporteInventario()
        {
            InitializeComponent();
        }

        private void BuscadorReporteInventario_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void cargarDatos()
        {
            DGVInventario.Rows.Clear();

            var numRevision = string.Empty;
            var nameUser = string.Empty;
            var fecha = string.Empty;
            System.Drawing.Image icono = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-pdf-o.png");


            var query = cn.CargarDatos($"SELECT NoRevision, NameUsr, Fecha FROM RevisarInventarioReportes WHERE IDUsuario = '{FormPrincipal.userID}' GROUP BY NoRevision ORDER BY Fecha DESC");

            if (!query.Rows.Count.Equals(0))
            {
                foreach (DataRow id in query.Rows)
                {
                    numRevision = id["NoRevision"].ToString();
                    nameUser = id["NameUsr"].ToString();
                    fecha = id["Fecha"].ToString();

                    DGVInventario.Rows.Add(numRevision, nameUser, fecha, icono);
                }
            }
        }

        private void DGVInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                var mostrarClave = FormPrincipal.clave;
                int numRev = Convert.ToInt32(DGVInventario.Rows[e.RowIndex].Cells[0].Value.ToString());

                if (mostrarClave == 0)
                {
                    GenerarReporteSinCLaveInterna(numRev);
                }
                else if (mostrarClave == 1)
                {
                    GenerarReporte(numRev);
                }
            }
        }

        private void GenerarReporteSinCLaveInterna(int num)
        {
            var mostrarClave = FormPrincipal.clave;

            // Datos del usuario
            var datos = FormPrincipal.datosUsuario;

            // Fuentes y Colores
            var colorFuenteNegrita = new BaseColor(Color.Black);
            var colorFuenteBlanca = new BaseColor(Color.White);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 8, 1, colorFuenteNegrita);

            var numRow = 0;

            // Ruta donde se creara el archivo PDF
            //var servidor = Properties.Settings.Default.Hosting;
            //var rutaArchivo = string.Empty;
            /*if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\reporte_inventario.pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";
            }*/

            var fechaHoy = DateTime.Now;
            var rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            string UsuarioActivo = string.Empty;

            string tipoReporte = string.Empty,
                    encabezadoTipoReporte = string.Empty;

            float PuntoDeVenta = 0,
                    StockFisico = 0,
                    Diferencia = 0,
                    Precio = 0,
                    CantidadPerdida = 0,
                    CantidadRecuperada = 0;

            tipoReporte = Inventario.filtradoParaRealizar;

            if (!tipoReporte.Equals(string.Empty))
            {
                if (tipoReporte.Equals("Normal"))
                {
                    encabezadoTipoReporte = "Normal";
                }
                if (tipoReporte.Equals("Stock"))
                {
                    encabezadoTipoReporte = "Stock";
                }
                if (tipoReporte.Equals("StockMinimo"))
                {
                    encabezadoTipoReporte = "Stock Minimo";
                }
                if (tipoReporte.Equals("StockNecesario"))
                {
                    encabezadoTipoReporte = "Stock Necesario";
                }
                if (tipoReporte.Equals("NumeroRevision"))
                {
                    encabezadoTipoReporte = "Numero Revision";
                }
                if (tipoReporte.Equals("Filtros"))
                {
                    encabezadoTipoReporte = "Filtros";
                }
            }

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

            Paragraph subTitulo = new Paragraph("REPORTE INVENTARIO\nSECCIÓN ELEGIDA " + encabezadoTipoReporte.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;


            float[] anchoColumnas = new float[] { 30f, 270f, 80f, 80f, 80f, 90f, 70f, 70f, 80f, 100f };

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //============================
            //=== TABLA DE INVENTARIO  ===
            //============================

            PdfPTable tablaInventario = new PdfPTable(10);
            tablaInventario.WidthPercentage = 100;
            tablaInventario.SetWidths(anchoColumnas);

            PdfPCell colNoConcepto = new PdfPCell(new Phrase("No:", fuenteNegrita));
            colNoConcepto.BorderWidth = 1;
            colNoConcepto.BackgroundColor = new BaseColor(Color.SkyBlue);
            colNoConcepto.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colNombre = new PdfPCell(new Phrase("NOMBRE", fuenteTotales));
            colNombre.BorderWidth = 0;
            colNombre.HorizontalAlignment = Element.ALIGN_CENTER;
            colNombre.Padding = 3;
            colNombre.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colCodigo = new PdfPCell(new Phrase("CÓDIGO", fuenteTotales));
            colCodigo.BorderWidth = 0;
            colCodigo.HorizontalAlignment = Element.ALIGN_CENTER;
            colCodigo.Padding = 3;
            colCodigo.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPuntoVenta = new PdfPCell(new Phrase("PUNTO DE VENTA", fuenteTotales));
            colPuntoVenta.BorderWidth = 0;
            colPuntoVenta.HorizontalAlignment = Element.ALIGN_CENTER;
            colPuntoVenta.Padding = 3;
            colPuntoVenta.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colStockFisico = new PdfPCell(new Phrase("STOCK FISICO", fuenteTotales));
            colStockFisico.BorderWidth = 0;
            colStockFisico.HorizontalAlignment = Element.ALIGN_CENTER;
            colStockFisico.Padding = 3;
            colStockFisico.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colFecha = new PdfPCell(new Phrase("FECHA", fuenteTotales));
            colFecha.BorderWidth = 0;
            colFecha.HorizontalAlignment = Element.ALIGN_CENTER;
            colFecha.Padding = 3;
            colFecha.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colDiferencia = new PdfPCell(new Phrase("DIFERENCIA", fuenteTotales));
            colDiferencia.BorderWidth = 0;
            colDiferencia.HorizontalAlignment = Element.ALIGN_CENTER;
            colDiferencia.Padding = 3;
            colDiferencia.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPrecio = new PdfPCell(new Phrase("PRECIO", fuenteTotales));
            colPrecio.BorderWidth = 0;
            colPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
            colPrecio.Padding = 3;
            colPrecio.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPerdida = new PdfPCell(new Phrase("CANTIDAD PERDIDA", fuenteTotales));
            colPerdida.BorderWidth = 0;
            colPerdida.HorizontalAlignment = Element.ALIGN_CENTER;
            colPerdida.Padding = 3;
            colPerdida.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colRecuperada = new PdfPCell(new Phrase("CANTIDAD RECUPERADA", fuenteTotales));
            colRecuperada.BorderWidth = 0;
            colRecuperada.HorizontalAlignment = Element.ALIGN_CENTER;
            colRecuperada.Padding = 3;
            colRecuperada.BackgroundColor = new BaseColor(Color.SkyBlue);

            tablaInventario.AddCell(colNoConcepto);
            tablaInventario.AddCell(colNombre);
            tablaInventario.AddCell(colCodigo);
            tablaInventario.AddCell(colPuntoVenta);
            tablaInventario.AddCell(colStockFisico);
            tablaInventario.AddCell(colFecha);
            tablaInventario.AddCell(colDiferencia);
            tablaInventario.AddCell(colPrecio);
            tablaInventario.AddCell(colPerdida);
            tablaInventario.AddCell(colRecuperada);

            var consulta = cn.CargarDatos($"SELECT * FROM RevisarInventarioReportes WHERE IDUsuario = '{FormPrincipal.userID}' AND NoRevision = '{num}'");

            foreach (DataRow row in consulta.Rows)
            {
                var nombre = row["Nombre"].ToString();
                var clave = row["ClaveInterna"].ToString();
                var codigo = row["CodigoBarras"].ToString();
                var almacen = row["StockAlmacen"].ToString();
                var fisico = row["StockFisico"].ToString();
                var fecha = row["Fecha"].ToString();
                var diferencia = row["Diferencia"].ToString();
                var precio = float.Parse(row["PrecioProducto"].ToString());
                var perdida = string.Empty;
                var recuperada = string.Empty;

                if (float.Parse(diferencia) < 0)
                {
                    perdida = (float.Parse(diferencia) * precio).ToString();
                    recuperada = "0";
                }
                else if (float.Parse(diferencia) > 0)
                {
                    recuperada = (float.Parse(diferencia) * precio).ToString();
                    perdida = "0";
                }
                else
                {
                    recuperada = "0";
                    perdida = "0";
                }

                numRow++;
                PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                colNoConceptoTmp.BorderWidth = 1;
                colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colNombreTmp = new PdfPCell(new Phrase(nombre, fuenteNormal));
                colNombreTmp.BorderWidth = 1;
                colNombreTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoTmp = new PdfPCell(new Phrase(codigo, fuenteNormal));
                colCodigoTmp.BorderWidth = 1;
                colCodigoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPuntoVentaTmp = new PdfPCell(new Phrase(almacen, fuenteNormal));
                PuntoDeVenta += (float)Convert.ToDouble(almacen);
                colPuntoVentaTmp.BorderWidth = 1;
                colPuntoVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockFisicoTmp = new PdfPCell(new Phrase(fisico, fuenteNormal));
                StockFisico += (float)Convert.ToDouble(fisico);
                colStockFisicoTmp.BorderWidth = 1;
                colStockFisicoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmp = new PdfPCell(new Phrase(fecha, fuenteNormal));
                colFechaTmp.BorderWidth = 1;
                colFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colDiferenciaTmp = new PdfPCell(new Phrase(diferencia, fuenteNormal));
                Diferencia += (float)Convert.ToDouble(diferencia);
                colDiferenciaTmp.BorderWidth = 1;
                colDiferenciaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmp = new PdfPCell(new Phrase(precio.ToString("0.00"), fuenteNormal));
                Precio += (float)Convert.ToDouble(precio);
                colPrecioTmp.BorderWidth = 1;
                colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPerdidaTmp = new PdfPCell(new Phrase(perdida, fuenteNormal));
                if (!perdida.Equals("---"))
                {
                    CantidadPerdida += (float)Convert.ToDouble(perdida);
                }
                colPerdidaTmp.BorderWidth = 1;
                colPerdidaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRecuperadaTmp = new PdfPCell(new Phrase(recuperada, fuenteNormal));
                if (!recuperada.Equals("---"))
                {
                    CantidadRecuperada += (float)Convert.ToDouble(recuperada);
                }
                colRecuperadaTmp.BorderWidth = 1;
                colRecuperadaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmp);          // 01
                tablaInventario.AddCell(colNombreTmp);              // 02
                tablaInventario.AddCell(colCodigoTmp);              // 03
                tablaInventario.AddCell(colPuntoVentaTmp);          // 04
                tablaInventario.AddCell(colStockFisicoTmp);         // 05
                tablaInventario.AddCell(colFechaTmp);               // 06
                tablaInventario.AddCell(colDiferenciaTmp);          // 07
                tablaInventario.AddCell(colPrecioTmp);              // 08
                tablaInventario.AddCell(colPerdidaTmp);             // 09
                tablaInventario.AddCell(colRecuperadaTmp);          // 10
            }

            if (PuntoDeVenta > 0 || StockFisico > 0)
            {
                PdfPCell colNoConceptoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNoConceptoTmpExtra.BorderWidth = 0;
                colNoConceptoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colNombreTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNombreTmpExtra.BorderWidth = 0;
                colNombreTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colCodigoTmpExtra.BorderWidth = 0;
                colCodigoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPuntoVentaTmpExtra = new PdfPCell(new Phrase(PuntoDeVenta.ToString("N2"), fuenteNormal));
                colPuntoVentaTmpExtra.BorderWidthTop = 0;
                colPuntoVentaTmpExtra.BorderWidthLeft = 0;
                colPuntoVentaTmpExtra.BorderWidthRight = 0;
                colPuntoVentaTmpExtra.BorderWidthBottom = 1;
                colPuntoVentaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPuntoVentaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockFisicoTmpExtra = new PdfPCell(new Phrase(StockFisico.ToString("N2"), fuenteNormal));
                colStockFisicoTmpExtra.BorderWidthTop = 0;
                colStockFisicoTmpExtra.BorderWidthLeft = 0;
                colStockFisicoTmpExtra.BorderWidthRight = 0;
                colStockFisicoTmpExtra.BorderWidthBottom = 1;
                colStockFisicoTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colStockFisicoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colFechaTmpExtra.BorderWidth = 0;
                colFechaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colDiferenciaTmpExtra = new PdfPCell(new Phrase(Diferencia.ToString("N2"), fuenteNormal));
                colDiferenciaTmpExtra.BorderWidthTop = 0;
                colDiferenciaTmpExtra.BorderWidthLeft = 0;
                colDiferenciaTmpExtra.BorderWidthRight = 0;
                colDiferenciaTmpExtra.BorderWidthBottom = 1;
                colDiferenciaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colDiferenciaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmpExtra = new PdfPCell(new Phrase(Precio.ToString("C"), fuenteNormal));
                colPrecioTmpExtra.BorderWidthTop = 0;
                colPrecioTmpExtra.BorderWidthLeft = 0;
                colPrecioTmpExtra.BorderWidthRight = 0;
                colPrecioTmpExtra.BorderWidthBottom = 1;
                colPrecioTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPrecioTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPerdidaTmpExtra = new PdfPCell(new Phrase(CantidadPerdida.ToString("N2"), fuenteNormal));
                colPerdidaTmpExtra.BorderWidthTop = 0;
                colPerdidaTmpExtra.BorderWidthLeft = 0;
                colPerdidaTmpExtra.BorderWidthRight = 0;
                colPerdidaTmpExtra.BorderWidthBottom = 1;
                colPerdidaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPerdidaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRecuperadaTmpExtra = new PdfPCell(new Phrase(CantidadRecuperada.ToString("N2"), fuenteNormal));
                colRecuperadaTmpExtra.BorderWidthTop = 0;
                colRecuperadaTmpExtra.BorderWidthLeft = 0;
                colRecuperadaTmpExtra.BorderWidthRight = 0;
                colRecuperadaTmpExtra.BorderWidthBottom = 1;
                colRecuperadaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colRecuperadaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmpExtra);
                tablaInventario.AddCell(colNombreTmpExtra);
                tablaInventario.AddCell(colCodigoTmpExtra);
                tablaInventario.AddCell(colPuntoVentaTmpExtra);
                tablaInventario.AddCell(colStockFisicoTmpExtra);
                tablaInventario.AddCell(colFechaTmpExtra);
                tablaInventario.AddCell(colDiferenciaTmpExtra);
                tablaInventario.AddCell(colPrecioTmpExtra);
                tablaInventario.AddCell(colPerdidaTmpExtra);
                tablaInventario.AddCell(colRecuperadaTmpExtra);
            }

            reporte.Add(titulo);
            reporte.Add(Usuario);
            reporte.Add(subTitulo);
            reporte.Add(tablaInventario);

            //================================
            //=== FIN TABLA DE INVENTARIO  ===
            //================================

            reporte.AddTitle("Reporte Inventario");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.Show();

        }

        private void GenerarReporte(int num)
        {
            var mostrarClave = FormPrincipal.clave;

            // Datos del usuario
            var datos = FormPrincipal.datosUsuario;

            // Fuentes y Colores
            var colorFuenteNegrita = new BaseColor(Color.Black);
            var colorFuenteBlanca = new BaseColor(Color.White);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 8, 1, colorFuenteNegrita);

            var numRow = 0;

            // Ruta donde se creara el archivo PDF
            //var servidor = Properties.Settings.Default.Hosting;
            //var rutaArchivo = string.Empty;
            /*if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\reporte_inventario.pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";
            }*/

            var fechaHoy = DateTime.Now;
            var rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            string UsuarioActivo = string.Empty;

            string tipoReporte = string.Empty,
                    encabezadoTipoReporte = string.Empty;

            float PuntoDeVenta = 0,
                    StockFisico = 0,
                    Diferencia = 0,
                    Precio = 0,
                    CantidadPerdida = 0,
                    CantidadRecuperada = 0;

            tipoReporte = Inventario.filtradoParaRealizar;

            if (!tipoReporte.Equals(string.Empty))
            {
                if (tipoReporte.Equals("Normal"))
                {
                    encabezadoTipoReporte = "Normal";
                }
                if (tipoReporte.Equals("Stock"))
                {
                    encabezadoTipoReporte = "Stock";
                }
                if (tipoReporte.Equals("StockMinimo"))
                {
                    encabezadoTipoReporte = "Stock Minimo";
                }
                if (tipoReporte.Equals("StockNecesario"))
                {
                    encabezadoTipoReporte = "Stock Necesario";
                }
                if (tipoReporte.Equals("NumeroRevision"))
                {
                    encabezadoTipoReporte = "Numero Revision";
                }
                if (tipoReporte.Equals("Filtros"))
                {
                    encabezadoTipoReporte = "Filtros";
                }
            }

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

            Paragraph subTitulo = new Paragraph("REPORTE DE REVISAR INVENTARIO\nSECCIÓN ELEGIDA " + encabezadoTipoReporte.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;


            float[] anchoColumnas = new float[] { 30f, 270f, 80f, 80f, 80f, 80f, 90f, 70f, 70f, 80f, 100f };

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //============================
            //=== TABLA DE INVENTARIO  ===
            //============================

            PdfPTable tablaInventario = new PdfPTable(11);
            tablaInventario.WidthPercentage = 100;
            tablaInventario.SetWidths(anchoColumnas);

            PdfPCell colNoConcepto = new PdfPCell(new Phrase("No:", fuenteNegrita));
            colNoConcepto.BorderWidth = 1;
            colNoConcepto.BackgroundColor = new BaseColor(Color.SkyBlue);
            colNoConcepto.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colNombre = new PdfPCell(new Phrase("NOMBRE", fuenteTotales));
            colNombre.BorderWidth = 1;
            colNombre.HorizontalAlignment = Element.ALIGN_CENTER;
            colNombre.Padding = 3;
            colNombre.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colClave = new PdfPCell(new Phrase("CLAVE", fuenteTotales));
            colClave.BorderWidth = 1;
            colClave.HorizontalAlignment = Element.ALIGN_CENTER;
            colClave.Padding = 3;
            colClave.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colCodigo = new PdfPCell(new Phrase("CÓDIGO", fuenteTotales));
            colCodigo.BorderWidth = 1;
            colCodigo.HorizontalAlignment = Element.ALIGN_CENTER;
            colCodigo.Padding = 3;
            colCodigo.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPuntoVenta = new PdfPCell(new Phrase("PUNTO DE VENTA", fuenteTotales));
            colPuntoVenta.BorderWidth = 1;
            colPuntoVenta.HorizontalAlignment = Element.ALIGN_CENTER;
            colPuntoVenta.Padding = 3;
            colPuntoVenta.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colStockFisico = new PdfPCell(new Phrase("STOCK FISICO", fuenteTotales));
            colStockFisico.BorderWidth = 1;
            colStockFisico.HorizontalAlignment = Element.ALIGN_CENTER;
            colStockFisico.Padding = 3;
            colStockFisico.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colFecha = new PdfPCell(new Phrase("FECHA", fuenteTotales));
            colFecha.BorderWidth = 1;
            colFecha.HorizontalAlignment = Element.ALIGN_CENTER;
            colFecha.Padding = 3;
            colFecha.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colDiferencia = new PdfPCell(new Phrase("DIFERENCIA", fuenteTotales));
            colDiferencia.BorderWidth = 1;
            colDiferencia.HorizontalAlignment = Element.ALIGN_CENTER;
            colDiferencia.Padding = 3;
            colDiferencia.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPrecio = new PdfPCell(new Phrase("PRECIO", fuenteTotales));
            colPrecio.BorderWidth = 1;
            colPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
            colPrecio.Padding = 3;
            colPrecio.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPerdida = new PdfPCell(new Phrase("CANTIDAD PERDIDA", fuenteTotales));
            colPerdida.BorderWidth = 1;
            colPerdida.HorizontalAlignment = Element.ALIGN_CENTER;
            colPerdida.Padding = 3;
            colPerdida.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colRecuperada = new PdfPCell(new Phrase("CANTIDAD RECUPERADA", fuenteTotales));
            colRecuperada.BorderWidth = 1;
            colRecuperada.HorizontalAlignment = Element.ALIGN_CENTER;
            colRecuperada.Padding = 3;
            colRecuperada.BackgroundColor = new BaseColor(Color.SkyBlue);

            tablaInventario.AddCell(colNoConcepto);
            tablaInventario.AddCell(colNombre);
            tablaInventario.AddCell(colClave);
            tablaInventario.AddCell(colCodigo);
            tablaInventario.AddCell(colPuntoVenta);
            tablaInventario.AddCell(colStockFisico);
            tablaInventario.AddCell(colFecha);
            tablaInventario.AddCell(colDiferencia);
            tablaInventario.AddCell(colPrecio);
            tablaInventario.AddCell(colPerdida);
            tablaInventario.AddCell(colRecuperada);

            var consulta = cn.CargarDatos($"SELECT * FROM RevisarInventarioReportes WHERE IDUsuario = '{FormPrincipal.userID}' AND NoRevision = '{num}'");

            foreach (DataRow row in consulta.Rows)
            {
                //var nombre = row.Cells["Nombre"].Value.ToString();
                //var clave = row.Cells["ClaveInterna"].Value.ToString();
                //var codigo = row.Cells["CodigoBarras"].Value.ToString();
                ////var almacen = Utilidades.RemoverCeroStock(row.Cells["StockAlmacen"].Value.ToString());
                ////var fisico = Utilidades.RemoverCeroStock(row.Cells["StockFisico"].Value.ToString());
                //var almacen = row.Cells["StockAlmacen"].Value.ToString();
                //var fisico = row.Cells["StockFisico"].Value.ToString();
                //var fecha = row.Cells["Fecha"].Value.ToString();
                //var diferencia = row.Cells["Diferencia"].Value.ToString();
                //var precio = float.Parse(row.Cells["PrecioProducto"].Value.ToString());
                //var perdida = row.Cells["Perdida"].Value.ToString();
                //var recuperada = row.Cells["Recuperada"].Value.ToString();

                var nombre = row["Nombre"].ToString();
                var clave = row["ClaveInterna"].ToString();
                var codigo = row["CodigoBarras"].ToString();
                var almacen = row["StockAlmacen"].ToString();
                var fisico = row["StockFisico"].ToString();
                var fecha = row["Fecha"].ToString();
                var diferencia = row["Diferencia"].ToString();
                var precio = float.Parse(row["PrecioProducto"].ToString());
                var perdida = string.Empty;
                var recuperada = string.Empty;

                if (float.Parse(diferencia) < 0)
                {
                    perdida = (float.Parse(diferencia) * precio).ToString();
                    recuperada = "0";
                }
                else if (float.Parse(diferencia) > 0)
                {
                    recuperada = (float.Parse(diferencia) * precio).ToString();
                    perdida = "0";
                }
                else
                {
                    recuperada = "0";
                    perdida = "0";
                }

                /*var perdida =*/ /*row["Perdida"].ToString();*/
                                  /* var recuperada =*/ /*row["Recuperada"].ToString();*/

                numRow++;
                PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                colNoConceptoTmp.BorderWidth = 1;
                colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colNombreTmp = new PdfPCell(new Phrase(nombre, fuenteNormal));
                colNombreTmp.BorderWidth = 1;
                colNombreTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colClaveTmp = new PdfPCell(new Phrase(clave, fuenteNormal));
                colClaveTmp.BorderWidth = 1;
                colClaveTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoTmp = new PdfPCell(new Phrase(codigo, fuenteNormal));
                colCodigoTmp.BorderWidth = 1;
                colCodigoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPuntoVentaTmp = new PdfPCell(new Phrase(almacen, fuenteNormal));
                PuntoDeVenta += (float)Convert.ToDouble(almacen);
                colPuntoVentaTmp.BorderWidth = 1;
                colPuntoVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockFisicoTmp = new PdfPCell(new Phrase(fisico, fuenteNormal));
                StockFisico += (float)Convert.ToDouble(fisico);
                colStockFisicoTmp.BorderWidth = 1;
                colStockFisicoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmp = new PdfPCell(new Phrase(fecha, fuenteNormal));
                colFechaTmp.BorderWidth = 1;
                colFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colDiferenciaTmp = new PdfPCell(new Phrase(diferencia, fuenteNormal));
                Diferencia += (float)Convert.ToDouble(diferencia);
                colDiferenciaTmp.BorderWidth = 1;
                colDiferenciaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmp = new PdfPCell(new Phrase(precio.ToString("0.00"), fuenteNormal));
                Precio += (float)Convert.ToDouble(precio);
                colPrecioTmp.BorderWidth = 1;
                colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPerdidaTmp = new PdfPCell(new Phrase(perdida, fuenteNormal));
                if (!perdida.Equals("---"))
                {
                    CantidadPerdida += (float)Convert.ToDouble(perdida);
                }
                colPerdidaTmp.BorderWidth = 1;
                colPerdidaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRecuperadaTmp = new PdfPCell(new Phrase(recuperada, fuenteNormal));
                if (!recuperada.Equals("---"))
                {
                    CantidadRecuperada += (float)Convert.ToDouble(recuperada);
                }
                colRecuperadaTmp.BorderWidth = 1;
                colRecuperadaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmp);
                tablaInventario.AddCell(colNombreTmp);
                tablaInventario.AddCell(colClaveTmp);
                tablaInventario.AddCell(colCodigoTmp);
                tablaInventario.AddCell(colPuntoVentaTmp);
                tablaInventario.AddCell(colStockFisicoTmp);
                tablaInventario.AddCell(colFechaTmp);
                tablaInventario.AddCell(colDiferenciaTmp);
                tablaInventario.AddCell(colPrecioTmp);
                tablaInventario.AddCell(colPerdidaTmp);
                tablaInventario.AddCell(colRecuperadaTmp);
            }

            if (PuntoDeVenta > 0 || StockFisico > 0)
            {
                PdfPCell colNoConceptoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNoConceptoTmpExtra.BorderWidth = 0;
                colNoConceptoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colNombreTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNombreTmpExtra.BorderWidth = 0;
                colNombreTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colClaveTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colClaveTmpExtra.BorderWidth = 0;
                colClaveTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colCodigoTmpExtra.BorderWidth = 0;
                colCodigoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPuntoVentaTmpExtra = new PdfPCell(new Phrase(PuntoDeVenta.ToString("N2"), fuenteNormal));
                colPuntoVentaTmpExtra.BorderWidthTop = 0;
                colPuntoVentaTmpExtra.BorderWidthLeft = 0;
                colPuntoVentaTmpExtra.BorderWidthRight = 0;
                colPuntoVentaTmpExtra.BorderWidthBottom = 1;
                colPuntoVentaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPuntoVentaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockFisicoTmpExtra = new PdfPCell(new Phrase(StockFisico.ToString("N2"), fuenteNormal));
                colStockFisicoTmpExtra.BorderWidthTop = 0;
                colStockFisicoTmpExtra.BorderWidthLeft = 0;
                colStockFisicoTmpExtra.BorderWidthRight = 0;
                colStockFisicoTmpExtra.BorderWidthBottom = 1;
                colStockFisicoTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colStockFisicoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colFechaTmpExtra.BorderWidth = 0;
                colFechaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colDiferenciaTmpExtra = new PdfPCell(new Phrase(Diferencia.ToString("N2"), fuenteNormal));
                colDiferenciaTmpExtra.BorderWidthTop = 0;
                colDiferenciaTmpExtra.BorderWidthLeft = 0;
                colDiferenciaTmpExtra.BorderWidthRight = 0;
                colDiferenciaTmpExtra.BorderWidthBottom = 1;
                colDiferenciaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colDiferenciaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmpExtra = new PdfPCell(new Phrase(Precio.ToString("C"), fuenteNormal));
                colPrecioTmpExtra.BorderWidthTop = 0;
                colPrecioTmpExtra.BorderWidthLeft = 0;
                colPrecioTmpExtra.BorderWidthRight = 0;
                colPrecioTmpExtra.BorderWidthBottom = 1;
                colPrecioTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPrecioTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPerdidaTmpExtra = new PdfPCell(new Phrase(CantidadPerdida.ToString("N2"), fuenteNormal));
                colPerdidaTmpExtra.BorderWidthTop = 0;
                colPerdidaTmpExtra.BorderWidthLeft = 0;
                colPerdidaTmpExtra.BorderWidthRight = 0;
                colPerdidaTmpExtra.BorderWidthBottom = 1;
                colPerdidaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPerdidaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRecuperadaTmpExtra = new PdfPCell(new Phrase(CantidadRecuperada.ToString("N2"), fuenteNormal));
                colRecuperadaTmpExtra.BorderWidthTop = 0;
                colRecuperadaTmpExtra.BorderWidthLeft = 0;
                colRecuperadaTmpExtra.BorderWidthRight = 0;
                colRecuperadaTmpExtra.BorderWidthBottom = 1;
                colRecuperadaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colRecuperadaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmpExtra);
                tablaInventario.AddCell(colNombreTmpExtra);
                tablaInventario.AddCell(colClaveTmpExtra);
                tablaInventario.AddCell(colCodigoTmpExtra);
                tablaInventario.AddCell(colPuntoVentaTmpExtra);
                tablaInventario.AddCell(colStockFisicoTmpExtra);
                tablaInventario.AddCell(colFechaTmpExtra);
                tablaInventario.AddCell(colDiferenciaTmpExtra);
                tablaInventario.AddCell(colPrecioTmpExtra);
                tablaInventario.AddCell(colPerdidaTmpExtra);
                tablaInventario.AddCell(colRecuperadaTmpExtra);
            }

            reporte.Add(titulo);
            reporte.Add(Usuario);
            reporte.Add(subTitulo);
            reporte.Add(tablaInventario);

            //================================
            //=== FIN TABLA DE INVENTARIO  ===
            //================================

            reporte.AddTitle("Reporte Inventario");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.Show();
        }
    }
}
