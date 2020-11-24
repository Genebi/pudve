using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public static class Utilidades
    {
        public static void CrearMarcaDeAgua(int idVenta, string texto)
        {
            var servidor = Properties.Settings.Default.Hosting;
            var archivoCopia = string.Empty;
            var archivoPDF = string.Empty;
            var nuevoPDF = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                archivoCopia = $@"\\{servidor}\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}_tmp.pdf";
                archivoPDF = $@"\\{servidor}\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}.pdf";
                nuevoPDF = archivoPDF;
                // Renombramos el archivo PDF
                File.Move(archivoPDF, archivoCopia);
            }
            else
            {
                archivoCopia = $@"C:\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}_tmp.pdf";
                archivoPDF = $@"C:\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}.pdf";
                nuevoPDF = archivoPDF;
                // Renombramos el archivo PDF
                File.Move(archivoPDF, archivoCopia);
            }


            using (PdfReader reader = new PdfReader(archivoCopia))
            {
                FileStream fs = new FileStream(nuevoPDF, FileMode.Create, FileAccess.Write, FileShare.None);

                using (PdfStamper stamper = new PdfStamper(reader, fs))
                {
                    int numeroPaginas = reader.NumberOfPages;

                    PdfLayer layer = new PdfLayer("WatermarkLayer", stamper.Writer);

                    for (int i = 1; i <= numeroPaginas; i++)
                    {
                        iTextSharp.text.Rectangle rec = reader.GetPageSize(i);
                        PdfContentByte cb = stamper.GetUnderContent(i);

                        cb.BeginLayer(layer);
                        cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 40);

                        PdfGState gstate = new PdfGState();
                        gstate.FillOpacity = 0.25f;
                        cb.SetGState(gstate);

                        cb.SetColorFill(iTextSharp.text.BaseColor.RED);
                        cb.BeginText();
                        cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, texto, rec.Width / 2, rec.Height / 2, 45f);
                        cb.EndText();
                        cb.EndLayer();
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(archivoCopia))
            {
                if (File.Exists(archivoCopia))
                {
                    File.Delete(archivoCopia);
                }
            }
        }

        public static void GenerarTicketCaja()
        {
            int folioTicket = Properties.Settings.Default.folioAbrirCaja + 1;

            Properties.Settings.Default.folioAbrirCaja = folioTicket;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            var datos = FormPrincipal.datosUsuario;

            // Medidas de ticket de 57 y 80 mm
            // 1 pulgada = 2.54 cm = 72 puntos = 25.4 mm
            // 57mm = 161.28 pt
            // 80mm = 226.08 pt

            var tipoPapel = 80;
            var anchoPapel = Convert.ToInt32(Math.Floor((((tipoPapel * 0.10) * 72) / 2.54)));
            var altoPapel = Convert.ToInt32(anchoPapel + 64); // 54

            string txtDescripcion = string.Empty;
            string txtCantidad = string.Empty;
            string txtImporte = string.Empty;
            string txtPrecio = string.Empty;
            string salto = string.Empty;

            int medidaFuenteMensaje = 0;
            int medidaFuenteNegrita = 0;
            int medidaFuenteNormal = 0;
            int medidaFuenteGrande = 0;

            int separadores = 0;
            int anchoLogo = 0;
            int altoLogo = 0;
            int espacio = 0;

            if (tipoPapel == 80)
            {
                separadores = 81;
                anchoLogo = 110;
                altoLogo = 60;
                espacio = 10;

                medidaFuenteMensaje = 10;
                medidaFuenteGrande = 10;
                medidaFuenteNegrita = 8;
                medidaFuenteNormal = 8;

                salto = "\n";
            }
            else if (tipoPapel == 57)
            {

                separadores = 75;
                anchoLogo = 80;
                altoLogo = 40;
                espacio = 8;

                medidaFuenteMensaje = 6;
                medidaFuenteGrande = 8;
                medidaFuenteNegrita = 6;
                medidaFuenteNormal = 6;

                salto = string.Empty;
            }


            var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Ventas\Tickets\ticket_caja_abierta_" + folioTicket + ".pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Ventas\Tickets\ticket_caja_abierta_" + folioTicket + ".pdf";
            }

            Document ticket = new Document(new iTextSharp.text.Rectangle(anchoPapel, altoPapel), 3, 3, 3, 0);
            PdfWriter writer = PdfWriter.GetInstance(ticket, new FileStream(rutaArchivo, FileMode.Create));

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteNormal);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, medidaFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteGrande);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteMensaje);

            string logotipo = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                logotipo = $@"\\{servidor}\Archivos PUDVE\MisDatos\Usuarios\" + datos[11];
            }
            else
            {
                logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + datos[11];
            }

            string encabezado = $"{salto}{datos[1]} {datos[2]} {datos[3]}, {datos[4]}, {datos[5]}\nCol. {datos[6]} C.P. {datos[7]}\nRFC: {datos[8]}\n{datos[9]}\nTel. {datos[10]}\n\n";

            ticket.Open();

            //Validación para verificar si existe logotipo
            if (logotipo != "")
            {
                if (File.Exists(logotipo))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logotipo);
                    logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    logo.ScaleAbsolute(anchoLogo, altoLogo);
                    ticket.Add(logo);
                }
            }

            Paragraph titulo = new Paragraph(datos[0] + "\n", fuenteGrande);
            Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            domicilio.Alignment = Element.ALIGN_CENTER;
            domicilio.SetLeading(espacio, 0);

            var fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt");

            Paragraph mensaje = new Paragraph("\n*** CAJA ABIERTA ***\n" + fecha, fuenteGrande);
            mensaje.Alignment = Element.ALIGN_CENTER;

            ticket.Add(titulo);
            ticket.Add(domicilio);
            ticket.Add(mensaje);

            ticket.AddTitle("Ticket Caja Abierta");
            ticket.AddAuthor("PUDVE");
            ticket.Close();
            writer.Close();

            ImprimirTicket(folioTicket.ToString(), 1);
        }

        private static void ImprimirTicket(string idVenta, int tipo = 0)
        {
            try
            {
                var servidor = Properties.Settings.Default.Hosting;
                var ruta = string.Empty;

                if (tipo == 0)
                {
                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        ruta = $@"\\{servidor}\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}.pdf";
                    }
                    else
                    {
                        ruta = $@"C:\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}.pdf";
                    }
                }

                if (tipo == 1)
                {
                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        ruta = $@"\\{servidor}\Archivos PUDVE\Ventas\Tickets\ticket_caja_abierta_{idVenta}.pdf";
                    }
                    else
                    {
                        ruta = $@"C:\Archivos PUDVE\Ventas\Tickets\ticket_caja_abierta_{idVenta}.pdf";
                    }
                }

                ProcessStartInfo info = new ProcessStartInfo();
                info.Verb = "print";
                info.FileName = ruta;
                info.CreateNoWindow = true;
                info.WindowStyle = ProcessWindowStyle.Hidden;

                Process p = new Process();
                p.StartInfo = info;
                p.Start();

                p.WaitForInputIdle();
                System.Threading.Thread.Sleep(5000);

                if (false == p.CloseMainWindow())
                {
                    p.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error No: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string cargar_imagen()
        {
            var logo = @"C:\Archivos PUDVE\MisDatos\Usuarios\MIRIIGUGM910427XX1.jpg";

            return logo;
        }

        public static bool EnviarEmail(string html, string asunto, string email)
        {
            var respuesta = false;

            try
            {
                MailMessage mensaje = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                mensaje.From = new MailAddress("pudve.contacto@gmail.com", "PUDVE");
                mensaje.To.Add(new MailAddress(email));
                mensaje.Subject = asunto;
                mensaje.IsBodyHtml = true; // para hacer el cuerpo del mensaje como html 
                mensaje.Body = html;

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; // para host gmail
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("pudve.contacto@gmail.com", "Steroids12");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mensaje);

                respuesta = true;
            }
            catch (Exception)
            {
                // Se comento el mensaje de exception ya que el usuario no sabe que se le enviara correo
                // y que no aparezca el messagebox
                //MessageBox.Show(ex.Message.ToString(), "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return respuesta;
        }

        public static void CambioPrecioProductoEmail(string[] datos, int tipo = 0)
        {
            var correo = FormPrincipal.datosUsuario[9];
            var asunto = "Cambio de precio(s) para producto(s)";
            var html = string.Empty;

            var producto = datos[0];
            var precioAnterior = datos[1];
            var precioNuevo = datos[2];
            var origen = datos[3];

            if (!string.IsNullOrWhiteSpace(correo))
            {
                if (tipo == 0)
                {
                    html = $@"
                    <div>
                        <h4 style='text-align: center;'>PRECIO DE PRODUCTO MODIFICADO</h4><hr>
                        <p>El precio del producto <span style='color: red;'>{producto}</span> ha sido modificado desde
                        {origen}, su precio <b>anterior</b> era de <span style='color: red;'>${precioAnterior}</span> y fue actualizado
                        por el <b>nuevo</b> precio de <span style='color: red;'>${precioNuevo}</span>.</p>
                        <p style='font-size: 0.8em;'>Fecha de Modificación: <b>{DateTime.Now}</b></p>
                    </div>";
                }

                if (tipo == 1)
                {
                    html = $@"
                    <div>
                        <h4 style='text-align: center;'>LISTA DE PRODUCTOS CON PRECIO MODIFICADO</h4><hr>
                        <ul style='font-size: 0.8em;'>
                            {producto}
                        </ul>
                        <p style='font-size: 0.8em;'>
                            <span>NOTA: El precio de los productos fue modificado desde {origen}.</span><br>
                            <span>Fecha de Modificación: <b>{DateTime.Now}</b></span>
                        </p>
                    </div>";
                }

                EnviarEmail(html, asunto, correo);
            }
        }

        public static void CambioStockProductoEmail(string[] datos, int tipo = 0)
        {
            var correo = FormPrincipal.datosUsuario[9];
            var asunto = "Cambio de stock para producto(s)";
            var html = string.Empty;

            var producto = datos[0];
            var stockAnterior = datos[1];
            var stockNuevo = datos[2];
            var stockActual = datos[3];
            var origen = datos[4];
            var operacion = datos[5];

            if (!string.IsNullOrWhiteSpace(correo))
            {
                if (tipo == 0)
                {
                    html = $@"
                    <div>
                        <h4 style='text-align: center;'>STOCK DE PRODUCTO MODIFICADO</h4><hr>
                        <p>El stock del producto <span style='color: red;'>{producto}</span> ha sido modificado desde
                        {origen}, su stock <b>anterior</b> era de <span style='color: red;'>{stockAnterior}</span>, se {operacion} la cantidad
                        de <span style='color: red;'>{stockNuevo}</span> y su stock <b>actual</b> es de <span style='color: red;'>{stockActual}</span>.</p>
                        <p style='font-size: 0.8em;'>Fecha de Modificación: <b>{DateTime.Now}</b></p>
                    </div>";
                }

                if (tipo == 1)
                {
                    html = $@"
                    <div>
                        <h4 style='text-align: center;'>LISTA DE PRODUCTOS CON STOCK MODIFICADO</h4><hr>
                        <ul style='font-size: 0.8em;'>
                            {producto}
                        </ul>
                        <p style='font-size: 0.8em;'>
                            <span>NOTA: El stock de los productos fue modificado desde {origen}.</span><br>
                            <span>Fecha de Modificación: <b>{DateTime.Now}</b></span>
                        </p>
                    </div>";
                }

                EnviarEmail(html, asunto, correo);
            }
        }

        public static bool AdobeReaderInstalado()
        {
            var instalado = false;

            var adobePath = Registry.GetValue(@"HKEY_CLASSES_ROOT\Software\Adobe\Acrobat\Exe", string.Empty, string.Empty);

            if (adobePath != null)
            {
                instalado = true;
            }

            return instalado;
        }

        public static void MensajeAdobeReader()
        {
            MessageBox.Show("Se requiere instalar Adobe Reader", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void MensajePermiso()
        {
            MessageBox.Show("No tiene permiso de realizar esta operación\nConsulte al administrador del sistema", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void CrearMarcaDeAguaNotaVenta(int idVenta, string texto)
        {
            var servidor = Properties.Settings.Default.Hosting;
            var archivoCopia = string.Empty;
            var archivoPDF = string.Empty;
            var nuevoPDF = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                archivoCopia = $@"\\{servidor}\Archivos PUDVE\Ventas\PDF\VENTA_{idVenta}_tmp.pdf";
                archivoPDF = $@"\\{servidor}\Archivos PUDVE\Ventas\PDF\VENTA_{idVenta}.pdf";

                nuevoPDF = archivoPDF;

                // Renombramos el archivo PDF
                File.Move(archivoPDF, archivoCopia);
            }
            else
            {
                archivoCopia = $@"C:\Archivos PUDVE\Ventas\PDF\VENTA_{idVenta}_tmp.pdf";
                archivoPDF = $@"C:\Archivos PUDVE\Ventas\PDF\VENTA_{idVenta}.pdf";

                nuevoPDF = archivoPDF;

                // Renombramos el archivo PDF
                File.Move(archivoPDF, archivoCopia);
            }

            using (PdfReader reader = new PdfReader(archivoCopia))
            {
                FileStream fs = new FileStream(nuevoPDF, FileMode.Create, FileAccess.Write, FileShare.None);

                using (PdfStamper stamper = new PdfStamper(reader, fs))
                {
                    int numeroPaginas = reader.NumberOfPages;

                    PdfLayer layer = new PdfLayer("WatermarkLayer", stamper.Writer);

                    for (int i = 1; i <= numeroPaginas; i++)
                    {
                        iTextSharp.text.Rectangle rec = reader.GetPageSize(i);
                        PdfContentByte cb = stamper.GetUnderContent(i);

                        float posicionX = 0,
                                posicionY = 0,
                                anguloTexto = 0f;

                        cb.BeginLayer(layer);
                        cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 20);

                        PdfGState gstate = new PdfGState();
                        gstate.FillOpacity = 0.35f;
                        cb.SetGState(gstate);

                        cb.SetColorFill(iTextSharp.text.BaseColor.RED);
                        cb.BeginText();
                        posicionX = rec.Width / 2;
                        posicionY = (rec.Height / 3) * (float)1.9;
                        cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, texto, posicionX, posicionY, anguloTexto);
                        cb.EndText();
                        cb.EndLayer();
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(archivoCopia))
            {
                if (File.Exists(archivoCopia))
                {
                    File.Delete(archivoCopia);
                }
            }
        }

        /// <summary>
        /// Metodo para Generar de nuevo el tcket
        /// </summary>
        /// <param name="tbVenta">DataTable que lleva los datos del ticket</param>
        /// <param name="impCodigo">Bool que decide si imprimir codigo de barras o no</param>
        public static void GenerarTicket(DataTable tbVenta, bool impCodigo)
        {
            string[][] productos = new string[tbVenta.Rows.Count][];

            decimal totalTicketVenta = 0;

            string idVenta = string.Empty,
                    IDProducto = string.Empty,
                    Nombre = string.Empty,
                    Cantidad = string.Empty,
                    Precio = string.Empty,
                    DescuentoGeneral = string.Empty,
                    DescuentoIndividual = string.Empty,
                    ImporteIndividual = string.Empty,
                    Descuento = string.Empty,
                    Total = string.Empty,
                    Folio = string.Empty,
                    AnticipoUtilizado = string.Empty,
                    TipoDescuento = string.Empty,
                    formaDePagoDeVenta = string.Empty,
                    referencia = string.Empty,
                    cliente = string.Empty;

            int contador = 0;

            foreach (DataRow item in tbVenta.Rows)
            {
                try
                {
                    idVenta = item["idVenta"].ToString();
                    IDProducto = item["IDProducto"].ToString();
                    Nombre = item["Nombre"].ToString();
                    Cantidad = item["Cantidad"].ToString();
                    Precio = item["Precio"].ToString();
                    DescuentoGeneral = item["DescuentoGeneral"].ToString();
                    DescuentoIndividual = item["DescuentoIndividual"].ToString();
                    ImporteIndividual = item["ImporteIndividual"].ToString();
                    Descuento = item["Descuento"].ToString();
                    totalTicketVenta += Convert.ToDecimal(item["Precio"].ToString());
                    Folio = item["Folio"].ToString();
                    AnticipoUtilizado = item["AnticipoUtilizado"].ToString();
                    TipoDescuento = item["TipoDescuento"].ToString();
                    formaDePagoDeVenta = item["formaDePagoDeVenta"].ToString();
                    referencia = item["Referencia"].ToString();
                    cliente = item["Cliente"].ToString();

                    var guardar = new string[]
                    {
                        idVenta,
                        IDProducto,
                        Nombre,
                        Cantidad,
                        Precio,
                        DescuentoGeneral,
                        DescuentoIndividual,
                        ImporteIndividual,
                        Descuento,
                        totalTicketVenta.ToString("#.##"),
                        Folio,
                        AnticipoUtilizado,
                        TipoDescuento,
                        formaDePagoDeVenta,
                        cliente,
                        referencia
                    };

                    productos[contador] = guardar;

                    contador++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Proceso no esperado en conversion de datos lista Productos para Ticket",
                                    "Advertencias del Sisteman" + ex.Message.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            var datos = FormPrincipal.datosUsuario;

            // Medidas de ticket de 57 y 80 mm
            // 1 pulgada = 2.54 cm = 72 puntos = 25.4 mm
            // 57mm = 161.28 pt
            // 80mm = 226.08 pt

            var tipoPapel = 80;
            var anchoPapel = Convert.ToInt32(Math.Floor((((tipoPapel * 0.10) * 72) / 2.54)));
            var altoPapel = Convert.ToInt32(anchoPapel + 72); // 54 64 68

            if (productos.Length > 3)
            {
                var filas = productos.Length / 2.54;
                filas *= 25.4;
                altoPapel += Convert.ToInt32(filas);

                for (int i = 0; i < productos.Length; i++)
                {
                    if (productos[i][2].Length > 18)
                    {
                        altoPapel += 20;
                    }
                }
            }

            //Variables y arreglos para el contenido de la tabla
            float[] anchoColumnas = new float[] { };

            string txtFormaPago = string.Empty;
            string strFormaPago = string.Empty;
            string txtDescripcion = string.Empty;
            string txtCantidad = string.Empty;
            string txtImporte = string.Empty;
            string txtPrecio = string.Empty;
            // Descuento general
            string txtDesc = string.Empty;
            string salto = string.Empty;

            int medidaFuenteMensaje = 0;
            int medidaFuenteNegrita = 0;
            int medidaFuenteNormal = 0;
            int medidaFuenteGrande = 0;

            int separadores = 0;
            int anchoLogo = 0;
            int altoLogo = 0;
            int espacio = 0;

            if (tipoPapel == 80)
            {
                anchoColumnas = new float[] { 7f, 24f, 10f, 10f, 10f };
                txtFormaPago = "Forma de pago:";
                txtDescripcion = "Descripción";
                txtCantidad = "Cant.";
                txtImporte = "Imp.";
                txtPrecio = "Precio";
                txtDesc = "Desc.";
                separadores = 81;
                anchoLogo = 110;
                altoLogo = 60;
                espacio = 10;

                medidaFuenteMensaje = 10;
                medidaFuenteGrande = 10;
                medidaFuenteNegrita = 8;
                medidaFuenteNormal = 8;

                salto = "\n";
            }
            else if (tipoPapel == 57)
            {
                anchoColumnas = new float[] { 7f, 20f, 11f, 11f, 13f };
                txtFormaPago = "Forma de pago:";
                txtDescripcion = "Descripción";
                txtImporte = "Imp.";
                txtCantidad = "Cant.";
                txtPrecio = "Prec.";
                txtDesc = "Desc.";
                separadores = 75;
                anchoLogo = 80;
                altoLogo = 40;
                espacio = 8;

                medidaFuenteMensaje = 6;
                medidaFuenteGrande = 8;
                medidaFuenteNegrita = 6;
                medidaFuenteNormal = 6;

                salto = string.Empty;
            }

            var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Ventas\Tickets\ticket_venta_" + productos[0][0] + ".pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Ventas\Tickets\ticket_venta_" + productos[0][0] + ".pdf";
            }


            Document ticket = new Document(new iTextSharp.text.Rectangle(anchoPapel, altoPapel), 3, 3, 3, 0);
            PdfWriter writer = PdfWriter.GetInstance(ticket, new FileStream(rutaArchivo, FileMode.Create));

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteNormal);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, medidaFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteGrande);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteMensaje);

            string logotipo = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                logotipo = $@"\\{servidor}\Archivos PUDVE\MisDatos\Usuarios\" + datos[11];
            }
            else
            {
                logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + datos[11];
            }

            string encabezado = $"{salto}{datos[1]} {datos[2]} {datos[3]}, {datos[4]}, {datos[5]}\nCol. {datos[6]} C.P. {datos[7]}\nRFC: {datos[8]}\n{datos[9]}\nTel. {datos[10]}\n\n";

            ticket.Open();

            //Validación para verificar si existe logotipo
            if (logotipo != "")
            {
                if (File.Exists(logotipo))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logotipo);
                    logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    logo.ScaleAbsolute(anchoLogo, altoLogo);
                    ticket.Add(logo);
                }
            }

            Paragraph titulo = new Paragraph(datos[0] + "\n", fuenteGrande);
            Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            domicilio.Alignment = Element.ALIGN_CENTER;
            domicilio.SetLeading(espacio, 0);

            Paragraph clienteP = new Paragraph($"Cliente: {productos[0][14]}", fuenteNormal);
            Paragraph referenciaP = new Paragraph($"Referencia: {productos[0][15]}", fuenteNormal);
            Paragraph FormPago = new Paragraph(txtFormaPago + " " + productos[0][13], fuenteNormal);

            /**************************************
             ** Tabla con los productos vendidos **
             **************************************/

            PdfPTable tabla = new PdfPTable(5);
            tabla.WidthPercentage = 100;
            tabla.SetWidths(anchoColumnas);

            PdfPCell colCantidad = new PdfPCell(new Phrase(txtCantidad, fuenteNegrita));
            colCantidad.BorderWidth = 0;

            PdfPCell colDescripcion = new PdfPCell(new Phrase(txtDescripcion, fuenteNegrita));
            colDescripcion.BorderWidth = 0;

            PdfPCell colPrecio = new PdfPCell(new Phrase(txtPrecio, fuenteNegrita));
            colPrecio.BorderWidth = 0;

            PdfPCell colDesc = new PdfPCell(new Phrase(txtDesc, fuenteNegrita));
            colDesc.BorderWidth = 0;

            PdfPCell colImporte = new PdfPCell(new Phrase(txtImporte, fuenteNegrita));
            colImporte.BorderWidth = 0;

            tabla.AddCell(colCantidad);
            tabla.AddCell(colDescripcion);
            tabla.AddCell(colPrecio);
            tabla.AddCell(colDesc);
            tabla.AddCell(colImporte);

            PdfPCell separadorInicial = new PdfPCell(new Phrase(new string('-', separadores), fuenteNormal));
            separadorInicial.BorderWidth = 0;
            separadorInicial.Colspan = 5;

            tabla.AddCell(separadorInicial);

            float descuentoProductos = 0;
            float descuentoGeneral = 0;
            float totalDescuento = float.Parse(productos[0][8]);
            float totalTicket = float.Parse(totalTicketVenta.ToString("#.##"));
            float totalAnticipo = float.Parse(productos[0][11]);

            var longitud = productos.Length;

            for (int i = 0; i < longitud; i++)
            {
                PdfPCell colCantidadTmp = new PdfPCell(new Phrase(productos[i][3], fuenteNormal));
                colCantidadTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                colCantidadTmp.BorderWidth = 0;

                PdfPCell colDescripcionTmp = new PdfPCell(new Phrase(productos[i][2], fuenteNormal));
                colDescripcionTmp.BorderWidth = 0;

                PdfPCell colPrecioTmp = new PdfPCell(new Phrase("$" + float.Parse(productos[i][4]).ToString("0.00"), fuenteNormal));
                colPrecioTmp.BorderWidth = 0;

                // Convertimos el descuento en array para poder mostrar el porcentaje y sumar
                // el descuento a la variable del total descuentoProductos
                var descuentoAux = productos[i][6].Split('-');

                float descuento = float.Parse(descuentoAux[0].Trim());

                var cadenaDescuento = string.Empty;

                cadenaDescuento += descuento.ToString("0.00");

                if (1 < descuentoAux.Length)
                {
                    cadenaDescuento += $" - {descuentoAux[1].Trim()}";
                }

                float importe = float.Parse(productos[i][7]);

                descuentoProductos += descuento;

                PdfPCell colDescTmp = new PdfPCell(new Phrase("$" + cadenaDescuento, fuenteNormal));
                colDescTmp.BorderWidth = 0;

                PdfPCell colImporteTmp = new PdfPCell(new Phrase("$" + importe.ToString("0.00"), fuenteNormal));
                colImporteTmp.BorderWidth = 0;

                tabla.AddCell(colCantidadTmp);
                tabla.AddCell(colDescripcionTmp);
                tabla.AddCell(colPrecioTmp);
                tabla.AddCell(colDescTmp);
                tabla.AddCell(colImporteTmp);
            }

            PdfPCell separadorFinal = new PdfPCell(new Phrase(new string('-', separadores), fuenteNormal));
            separadorFinal.BorderWidth = 0;
            separadorFinal.Colspan = 5;

            PdfPCell colTotalAnticipo = new PdfPCell(new Phrase("Anticipo: $" + totalAnticipo.ToString("0.00"), fuenteNormal));
            colTotalAnticipo.BorderWidth = 0;
            colTotalAnticipo.HorizontalAlignment = Element.ALIGN_RIGHT;
            colTotalAnticipo.Colspan = 5;

            PdfPCell colTotalDescuento = new PdfPCell(new Phrase("Descuento productos: $" + descuentoProductos.ToString("0.00"), fuenteNormal));
            colTotalDescuento.BorderWidth = 0;
            colTotalDescuento.HorizontalAlignment = Element.ALIGN_RIGHT;
            colTotalDescuento.Colspan = 5;

            var descuentoG = descuentoGeneral;
            descuentoGeneral = totalDescuento - descuentoProductos;

            PdfPCell colDescuentoGeneral = new PdfPCell(new Phrase($"Descuento general ({descuentoG}%): $" + descuentoGeneral.ToString("0.00"), fuenteNormal));
            colDescuentoGeneral.BorderWidth = 0;
            colDescuentoGeneral.HorizontalAlignment = Element.ALIGN_RIGHT;
            colDescuentoGeneral.Colspan = 5;

            PdfPCell totalVenta = new PdfPCell(new Phrase("TOTAL: $" + totalTicket.ToString("0.00"), fuenteNormal));
            totalVenta.BorderWidth = 0;
            totalVenta.HorizontalAlignment = Element.ALIGN_RIGHT;
            totalVenta.Colspan = 5;

            tabla.AddCell(separadorFinal);

            if (totalAnticipo > 0)
            {
                tabla.AddCell(colTotalAnticipo);
            }

            tabla.AddCell(colTotalDescuento);

            if (descuentoGeneral > 0)
            {
                tabla.AddCell(colDescuentoGeneral);
            }

            tabla.AddCell(totalVenta);

            /******************************************
             ** Fin tabla con los productos vendidos **
             ******************************************/

            Paragraph mensaje = new Paragraph("\nCambios y Garantía máximo 7 días después de su compra, presentando el Ticket. Gracias por su preferencia.\n\n", fuenteNormal);
            mensaje.Alignment = Element.ALIGN_CENTER;

            string drFecha = tbVenta.Rows[0]["FechaOperacion"].ToString();

            string[] words;

            words = drFecha.Split(' ');

            var DiaFecha = DateTime.Parse(words[0].ToString());

            var culture = new System.Globalization.CultureInfo("es-MX");
            var dia = culture.DateTimeFormat.GetDayName(DiaFecha.DayOfWeek);
            var fecha = drFecha;

            Conexion cn = new Conexion();

            dia = cn.Capitalizar(dia);

            Paragraph diaVenta = new Paragraph($"{dia} - {fecha} - Folio: {productos[0][10]}", fuenteNormal);
            diaVenta.Alignment = Element.ALIGN_CENTER;

            ticket.Add(titulo);
            ticket.Add(domicilio);

            if (!string.IsNullOrWhiteSpace(productos[0][14]))
            {
                ticket.Add(clienteP);
            }

            if (!string.IsNullOrWhiteSpace(productos[0][15]))
            {
                ticket.Add(referenciaP);
            }

            ticket.Add(FormPago);
            ticket.Add(tabla);
            ticket.Add(mensaje);

            // Imprimir codigo de barras en el ticket
            if (impCodigo)
            {
                // Generar el codigo de barras
                var codigoBarra = GenerarCodigoBarras(productos[0][10], anchoPapel);

                iTextSharp.text.Image imagenCodigo = iTextSharp.text.Image.GetInstance(codigoBarra, System.Drawing.Imaging.ImageFormat.Jpeg);
                imagenCodigo.Alignment = Element.ALIGN_CENTER;

                ticket.Add(imagenCodigo);
            }

            ticket.Add(diaVenta);
            ticket.AddTitle("Ticket Venta");
            ticket.AddAuthor("PUDVE");
            ticket.Close();
            writer.Close();
        }

        /// <summary>
        /// Metodo para Generar de Barras que lleva el ticket de venta
        /// </summary>
        /// <param name="txtCodigo">el codigo de barras</param>
        /// <param name="ancho">el ancho del ticket</param>
        /// <returns></returns>
        private static System.Drawing.Image GenerarCodigoBarras(string txtCodigo, int ancho)
        {
            System.Drawing.Image imagen;

            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();

            try
            {
                var anchoTmp = ancho / 2;
                var auxiliar = anchoTmp;

                anchoTmp = auxiliar / 2;
                ancho = ancho - anchoTmp;

                imagen = codigo.Encode(BarcodeLib.TYPE.CODE128, txtCodigo, Color.Black, Color.White, ancho, 40);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar código de barras para el ticket", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                imagen = null;
            }

            return imagen;
        }

        /// <summary>
        /// Metodo para envio de correo de venta finalizada
        /// </summary>
        /// <param name="productosNoVendidos">Lista de productos en la listas de venta</param>
        /// <param name="fechaSistema">Fecha de operación de venta del sistema</param>
        /// <param name="cantidadTotal">Monto total de l vnta</param>
        /// <param name="datosUsuario">Datos del usuario que realiza la venta</param>
        public static void ventaNotSuccessfulFinalizadaEmail(List<string> productosNoVendidos, string fechaSistema, string cantidadTotal, string[] datosUsuario)
        {
            string[] words;
            string productos = string.Empty;

            foreach (var item in productosNoVendidos)
            {
                words = item.Split('|');
                productos += "<b>Cantidad = </b>" + words[0].ToString() + " <b>Precio = </b>" + words[1].ToString() + " <b>Descripcion = </b>" + words[2].ToString() + " <b>Descuento = </b>" + words[3].ToString() + " <b>Importe = </b>" + words[4].ToString() + "<br>";
            }
        }
    }
}
