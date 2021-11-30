using iTextSharp.text;
using iTextSharp.text.pdf;
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
    public partial class RangosReporteProductosMenosVendidos : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        string fechaHoraInicio = string.Empty;
        string fechaHoraFinal = string.Empty;
        int cantidadLimite = 0;

        public RangosReporteProductosMenosVendidos()
        {
            InitializeComponent();
        }

        private void RangosReporteProductosMenosVendidos_Load(object sender, EventArgs e)
        {
            configurarDateTimePicker();
        }

        private void configurarDateTimePicker()
        {
            var personalizada = "dd / MM / yyyy h:mm:ss tt";

            dtpInicio.Format = DateTimePickerFormat.Custom;
            dtpInicio.CustomFormat = personalizada;
            dtpInicio.Text = DateTime.Parse(dtpInicio.Text).AddMonths(-1).ToString();

            dtpFin.Format = DateTimePickerFormat.Custom;
            dtpFin.CustomFormat = personalizada;
        }

        private void txtCantidadMostar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void botonRedondo1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void botonRedondo2_Click(object sender, EventArgs e)
        {
            if (dtpFin.Value >= dtpInicio.Value)
            {
                fechaHoraInicio = dtpInicio.Value.ToString("yyyy-MM-dd HH:mm:ss");
                fechaHoraFinal = dtpFin.Value.ToString("yyyy-MM-dd HH:mm:ss");

                bool esNumero = false;
                int cantidad = 0;
                esNumero = Int32.TryParse(txtCantidadMostar.Text, out cantidad);

                if (esNumero)
                {
                    if (cantidad > 0)
                    {
                        cantidadLimite = cantidad;
                    }
                    else
                    {
                        MessageBox.Show("La cantidad de productos tiene\nque ser mayor a cero productos", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCantidadMostar.Focus();
                        txtCantidadMostar.SelectAll();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("La cantidad de productos tiene\nque tener un número y ser mayor a cero", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCantidadMostar.Focus();
                    txtCantidadMostar.SelectAll();
                    return;
                }

                using (DataTable dtProductosMenosVendidos = cn.CargarDatos(cs.productosMenosVendidos(fechaHoraInicio, fechaHoraFinal)))
                {
                    if (!dtProductosMenosVendidos.Rows.Count.Equals(0))
                    {
                        MessageBox.Show("Procesando la solicitud de generar reporte,\neste proceso puede tardar un momento en completarse.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        generarReporteMenosVendidos(dtProductosMenosVendidos);
                    }
                }
            }
            else
            {
                MessageBox.Show($"La fecha inicial:\n\t({dtpInicio.Value.ToString("yyyy-MM-dd HH:mm:ss")})\ntiene que ser menor a la fecha final:\n\t({dtpFin.Value.ToString("yyyy-MM-dd HH:mm:ss")})", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void generarReporteMenosVendidos(DataTable dtProductosMenosVendidos)
        {
            var servidor = Properties.Settings.Default.Hosting;

            var rutaArchivo = string.Empty;
            var usuario = string.Empty;

            if (FormPrincipal.userNickName.Contains("@"))
            {
                var palabras = FormPrincipal.userNickName.Split('@');
                usuario = palabras[0].ToString();
            }
            else
            {
                usuario = FormPrincipal.userNickName;
            }

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\Historial\{usuario}\MenosVendidos\";
            }
            else
            {
                rutaArchivo = $@"C:\Archivos PUDVE\Reportes\Historial\{usuario}\MenosVendidos\";
            }

            if (!Directory.Exists(rutaArchivo))
            {
                Directory.CreateDirectory(rutaArchivo);
            }

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo += $"reporte_Menos_Vendidos.pdf";
            }
            else
            {
                rutaArchivo += $"reporte_Menos_Vendidos.pdf";
            }

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

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);

            Paragraph Usuario = new Paragraph("");

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

            Paragraph subTitulo = new Paragraph("REPORTE PRODUCTO MENOS VENDIDO\n\nFechas consultadas\ndesde: " + fechaHoraInicio + "\nhasta: " + fechaHoraFinal + " \nCantidad de productos mostrados: " + cantidadLimite.ToString("N0") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;

            float[] anchoColumnas = new float[] { 40f, 400f, 80f, 80f, 80f, 200f, 80f, 80f };

            PdfPTable tablaInventario = new PdfPTable(8);
            tablaInventario.WidthPercentage = 100;
            tablaInventario.SetWidths(anchoColumnas);

            PdfPCell colNoConcepto = new PdfPCell(new Phrase("No:", fuenteNegrita));
            colNoConcepto.BorderWidth = 1;
            colNoConcepto.BackgroundColor = new BaseColor(Color.SkyBlue);
            colNoConcepto.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell colNombre = new PdfPCell(new Phrase("ARTICULO", fuenteTotales));
            colNombre.BorderWidth = 1;
            colNombre.HorizontalAlignment = Element.ALIGN_LEFT;
            colNombre.Padding = 3;
            colNombre.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colVendidos = new PdfPCell(new Phrase("VENDIDOS", fuenteTotales));
            colVendidos.BorderWidth = 1;
            colVendidos.HorizontalAlignment = Element.ALIGN_CENTER;
            colVendidos.Padding = 3;
            colVendidos.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colCodigoBarra = new PdfPCell(new Phrase("CODIGO DE BARRAS", fuenteTotales));
            colCodigoBarra.BorderWidth = 1;
            colCodigoBarra.HorizontalAlignment = Element.ALIGN_CENTER;
            colCodigoBarra.Padding = 3;
            colCodigoBarra.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colCategoria = new PdfPCell(new Phrase("CATEGORIA", fuenteTotales));
            colCategoria.BorderWidth = 1;
            colCategoria.HorizontalAlignment = Element.ALIGN_CENTER;
            colCategoria.Padding = 3;
            colCategoria.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colUltimaVenta = new PdfPCell(new Phrase("ULTIMA VENTA (fecha de la venta)", fuenteTotales));
            colUltimaVenta.BorderWidth = 1;
            colUltimaVenta.HorizontalAlignment = Element.ALIGN_CENTER;
            colUltimaVenta.Padding = 3;
            colUltimaVenta.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colStock = new PdfPCell(new Phrase("STOCK", fuenteTotales));
            colStock.BorderWidth = 1;
            colStock.HorizontalAlignment = Element.ALIGN_CENTER;
            colStock.Padding = 3;
            colStock.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPrecio = new PdfPCell(new Phrase("PRECIO", fuenteTotales));
            colPrecio.BorderWidth = 1;
            colPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
            colPrecio.Padding = 3;
            colPrecio.BackgroundColor = new BaseColor(Color.SkyBlue);

            tablaInventario.AddCell(colNoConcepto);
            tablaInventario.AddCell(colNombre);
            tablaInventario.AddCell(colVendidos);
            tablaInventario.AddCell(colCodigoBarra);
            tablaInventario.AddCell(colCategoria);
            tablaInventario.AddCell(colUltimaVenta);
            tablaInventario.AddCell(colStock);
            tablaInventario.AddCell(colPrecio);

            for (int i = 0; i < cantidadLimite; i++)
            {
                var articulo = dtProductosMenosVendidos.Rows[i]["ARTICULO"].ToString();
                var vendidos = dtProductosMenosVendidos.Rows[i]["VENDIDOS"].ToString();
                var codigoBarra = string.Empty;
                if (string.IsNullOrWhiteSpace(dtProductosMenosVendidos.Rows[i]["CODIGO DE BARRAS"].ToString()))
                {
                    codigoBarra = "N/A";
                }
                else
                {
                    codigoBarra = dtProductosMenosVendidos.Rows[i]["CODIGO DE BARRAS"].ToString();
                }
                var categoria = dtProductosMenosVendidos.Rows[i]["CATEGORIA"].ToString();
                var ultimaVenta = dtProductosMenosVendidos.Rows[i]["ULTIMA VENTA"].ToString();
                var stock = dtProductosMenosVendidos.Rows[i]["STOCK"].ToString();
                var precio = dtProductosMenosVendidos.Rows[i]["PRECIO"].ToString();

                numRow++;

                PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString("N0"), fuenteNormal));
                colNoConceptoTmp.BorderWidth = 1;
                colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_LEFT;

                PdfPCell colNombreTmp = new PdfPCell(new Phrase(articulo, fuenteNormal));
                colNombreTmp.BorderWidth = 1;
                colNombreTmp.HorizontalAlignment = Element.ALIGN_LEFT;

                PdfPCell colVendidosTmp = new PdfPCell(new Phrase(vendidos, fuenteNormal));
                colVendidosTmp.BorderWidth = 1;
                colVendidosTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoBarraTmp = new PdfPCell(new Phrase(codigoBarra, fuenteNormal));
                colCodigoBarraTmp.BorderWidth = 1;
                colCodigoBarraTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCategoriaTmp = new PdfPCell(new Phrase(categoria, fuenteNormal));
                colCategoriaTmp.BorderWidth = 1;
                colCategoriaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colUltimaVentaTmp = new PdfPCell(new Phrase(ultimaVenta, fuenteNormal));
                colUltimaVentaTmp.BorderWidth = 1;
                colUltimaVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockTmp = new PdfPCell(new Phrase(stock, fuenteNormal));
                colStockTmp.BorderWidth = 1;
                colStockTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmp = new PdfPCell(new Phrase(precio, fuenteNormal));
                colPrecioTmp.BorderWidth = 1;
                colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmp);
                tablaInventario.AddCell(colNombreTmp);
                tablaInventario.AddCell(colVendidosTmp);
                tablaInventario.AddCell(colCodigoBarraTmp);
                tablaInventario.AddCell(colCategoriaTmp);
                tablaInventario.AddCell(colUltimaVentaTmp);
                tablaInventario.AddCell(colStockTmp);
                tablaInventario.AddCell(colPrecioTmp);
            }

            reporte.Add(titulo);
            reporte.Add(Usuario);
            reporte.Add(subTitulo);
            reporte.Add(tablaInventario);

            reporte.AddTitle("Reporte Producto Menos Vendido");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            dtProductosMenosVendidos.Dispose();
            dtProductosMenosVendidos = null;

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }
    }
}
