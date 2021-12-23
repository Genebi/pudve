using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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

        string personalizada = "dd / MM / yyyy h:mm:ss tt";

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

                if (rbMasVendidos.Checked)
                {
                    using (DataTable dtProductosMasVendidos = cn.CargarDatos(cs.productosMasVendidosSinIncluirVentasEnCero(fechaHoraInicio, fechaHoraFinal)))
                    {
                        if (!dtProductosMasVendidos.Rows.Count.Equals(0))
                        {
                            MessageBox.Show("Procesando la solicitud de generar reporte,\neste proceso puede tardar un momento en completarse.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            generarReporteMenosVendidos(dtProductosMasVendidos);
                        }
                    }
                }
                else if (rbMenosVendidos.Checked)
                {
                    if (chkIncluirVentasEnCero.Checked.Equals(true))
                    {
                        using (DataTable dtProductosMenosVendidos = cn.CargarDatos(cs.productosMenosVendidosIncluidoVentasEnCero(fechaHoraInicio, fechaHoraFinal)))
                        {
                            if (!dtProductosMenosVendidos.Rows.Count.Equals(0))
                            {
                                MessageBox.Show("Procesando la solicitud de generar reporte,\neste proceso puede tardar un momento en completarse.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                generarReporteMenosVendidos(dtProductosMenosVendidos);
                            }
                            else
                            {
                                MessageBox.Show($"Usted no ha realizado ventas el dia de ahora {fechaHoraFinal}", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        using (DataTable dtProductosMenosVendidos = cn.CargarDatos(cs.productosMenosVendidosSinIncluidoVentasEnCero(fechaHoraInicio, fechaHoraFinal)))
                        {
                            if (!dtProductosMenosVendidos.Rows.Count.Equals(0))
                            {
                                MessageBox.Show("Procesando la solicitud de generar reporte,\neste proceso puede tardar un momento en completarse.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                generarReporteMenosVendidos(dtProductosMenosVendidos);
                            }
                            else
                            {
                                MessageBox.Show($"Usted no ha realizado ventas el dia de ahora {fechaHoraFinal}", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
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
            long totalVendidos = 0;
            long totalStock = 0;
            long totalPrecio = 0;

            var servidor = Properties.Settings.Default.Hosting;

            var rutaArchivo = string.Empty;
            var usuario = string.Empty;

            var nuevaFechaHoraInicio = new DateTime();
            var nuevaFechaHoraFinal = new DateTime();

            var recorridoFinalProductos = 0;

            if (dtProductosMenosVendidos.Rows.Count < cantidadLimite)
            {
                recorridoFinalProductos = dtProductosMenosVendidos.Rows.Count;
            }
            else
            {
                recorridoFinalProductos = cantidadLimite;
            }

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

            try
            {
                nuevaFechaHoraInicio = DateTime.Parse(fechaHoraInicio, new CultureInfo("en-CA"));
                nuevaFechaHoraFinal = DateTime.Parse(fechaHoraFinal, new CultureInfo("en-CA"));
            }
            catch (FormatException ex)
            {
                //MessageBox.Show($"{fechaHoraInicio} este no es no es un formato correcto.\n\n{ex.Message.ToString()}", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }

            var tipoConcepto = string.Empty;

            if (rbMasVendidos.Checked)
            {
                tipoConcepto = "más";
            }
            else if (rbMenosVendidos.Checked)
            {
                tipoConcepto = "menos";
            }
            
            Paragraph subTitulo = new Paragraph($"REPORTE PRODUCTO {tipoConcepto.ToUpper()} VENDIDO\n\nFechas consultadas\ndesde: {nuevaFechaHoraInicio} \nhasta: {nuevaFechaHoraFinal}\nCantidad de productos mostrados: {recorridoFinalProductos.ToString("N0")}\n\n\n", fuenteNormal);

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

            for (int i = 0; i < recorridoFinalProductos; i++)
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
                var ultimaVenta = string.Empty;
                if (dtProductosMenosVendidos.Rows[i]["ULTIMA VENTA"].ToString().Contains("N/A"))
                {
                    ultimaVenta = dtProductosMenosVendidos.Rows[i]["ULTIMA VENTA"].ToString();
                }
                else
                {
                    var FechaHoraUltimaVenta = new DateTime();
                    try
                    {
                        FechaHoraUltimaVenta = DateTime.Parse(dtProductosMenosVendidos.Rows[i]["ULTIMA VENTA"].ToString(), new CultureInfo("en-CA"));
                        ultimaVenta = FechaHoraUltimaVenta.ToString();
                    }
                    catch (FormatException ex)
                    {
                        //MessageBox.Show($"{fechaHoraInicio} este no es no es un formato correcto.\n\n{ex.Message.ToString()}", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //return;
                    }
                }
                var stock = dtProductosMenosVendidos.Rows[i]["STOCK"].ToString();
                var precio = dtProductosMenosVendidos.Rows[i]["PRECIO"].ToString();

                numRow++;

                PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString("N0"), fuenteNormal));
                colNoConceptoTmp.BorderWidth = 1;
                colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_LEFT;

                PdfPCell colNombreTmp = new PdfPCell(new Phrase(articulo, fuenteNormal));
                colNombreTmp.BorderWidth = 1;
                colNombreTmp.HorizontalAlignment = Element.ALIGN_LEFT;

                PdfPCell colVendidosTmp = new PdfPCell(new Phrase(Convert.ToDecimal(vendidos).ToString("N"), fuenteNormal));
                totalVendidos += (long)Convert.ToDouble(Convert.ToDecimal(vendidos).ToString("N"));
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

                PdfPCell colStockTmp = new PdfPCell(new Phrase(Convert.ToDecimal(stock).ToString("N"), fuenteNormal));
                totalStock += (long)Convert.ToDouble(Convert.ToDecimal(stock).ToString("N"));
                colStockTmp.BorderWidth = 1;
                colStockTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmp = new PdfPCell(new Phrase(precio, fuenteNormal));
                totalPrecio += (long)Convert.ToDouble(precio);
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

            if (totalVendidos > 0 || totalStock > 0)
            {
                PdfPCell colNoConceptoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNoConceptoTmpExtra.BorderWidth = 0;
                colNoConceptoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colNombreTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNombreTmpExtra.BorderWidth = 0;
                colNombreTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colVendidosTmpExtra = new PdfPCell(new Phrase(Convert.ToDouble(totalVendidos).ToString("N"), fuenteNormal));
                colVendidosTmpExtra.BorderWidthTop = 0;
                colVendidosTmpExtra.BorderWidthLeft = 0;
                colVendidosTmpExtra.BorderWidthRight = 0;
                colVendidosTmpExtra.BorderWidthBottom = 1;
                colVendidosTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colVendidosTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoBarraTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colCodigoBarraTmpExtra.BorderWidth = 0;
                colCodigoBarraTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCategoriaTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colCategoriaTmpExtra.BorderWidth = 0;
                colCategoriaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colUltimaVentaTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colUltimaVentaTmpExtra.BorderWidth = 0;
                colUltimaVentaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockTmpExtra = new PdfPCell(new Phrase(Convert.ToDouble(totalStock).ToString("N"), fuenteNormal));
                colStockTmpExtra.BorderWidthTop = 0;
                colStockTmpExtra.BorderWidthLeft = 0;
                colStockTmpExtra.BorderWidthRight = 0;
                colStockTmpExtra.BorderWidthBottom = 1;
                colStockTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colStockTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmpExtra = new PdfPCell(new Phrase(Convert.ToDouble(totalPrecio).ToString("N"), fuenteNormal));
                colPrecioTmpExtra.BorderWidthTop = 0;
                colPrecioTmpExtra.BorderWidthLeft = 0;
                colPrecioTmpExtra.BorderWidthRight = 0;
                colPrecioTmpExtra.BorderWidthBottom = 1;
                colPrecioTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPrecioTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmpExtra);
                tablaInventario.AddCell(colNombreTmpExtra);
                tablaInventario.AddCell(colVendidosTmpExtra);
                tablaInventario.AddCell(colCodigoBarraTmpExtra);
                tablaInventario.AddCell(colCategoriaTmpExtra);
                tablaInventario.AddCell(colUltimaVentaTmpExtra);
                tablaInventario.AddCell(colStockTmpExtra);
                tablaInventario.AddCell(colPrecioTmpExtra);
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

            //vr.FormClosed += delegate
            //{
            //    this.Close();
            //};

            vr.ShowDialog();
        }

        private void rbMenosVendidos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMenosVendidos.Checked)
            {
                rbMenosVendidos.Font = new System.Drawing.Font(rbMenosVendidos.Font, FontStyle.Bold);
                chkIncluirVentasEnCero.Enabled = true;
                reiniciarValoresDateTimeTextBox();
            }
            else
            {
                rbMenosVendidos.Font = new System.Drawing.Font(rbMenosVendidos.Font, FontStyle.Regular);
            }
        }

        private void rbMasVendidos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMasVendidos.Checked)
            {
                rbMasVendidos.Font = new System.Drawing.Font(rbMasVendidos.Font, FontStyle.Bold);
                chkIncluirVentasEnCero.Checked = false;
                chkIncluirVentasEnCero.Enabled = false;
                reiniciarValoresDateTimeTextBox();
            }
            else
            {
                rbMasVendidos.Font = new System.Drawing.Font(rbMasVendidos.Font, FontStyle.Regular);
            }
        }

        private void reiniciarValoresDateTimeTextBox()
        {
            dtpInicio.Text = DateTime.Parse(DateTime.Now.ToString(personalizada)).AddMonths(-1).ToString();
            dtpFin.Text = DateTime.Now.ToString(personalizada);
            txtCantidadMostar.Clear();
            txtCantidadMostar.Text = "0";
        }
    }
}
