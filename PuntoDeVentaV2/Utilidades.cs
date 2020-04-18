using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static void CrearMarcaDeAgua(int idVenta)
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
                        cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "CANCELADA", rec.Width / 2, rec.Height / 2, 45f);
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

        public static void EnviarEmail(string html, string asunto, string email)
        {
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
            }
            catch (Exception ex)
            {
                // Se comento el mensaje de exception ya que el usuario no sabe que se le enviara correo
                // y que no aparezca el messagebox
                //MessageBox.Show(ex.Message.ToString(), "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            var asunto = "Cambio de stock para producto";
            var html = string.Empty;

            var producto = datos[0];
            var stockAnterior = datos[1];
            var stockNuevo = datos[2];
            var origen = datos[3];

            if (!string.IsNullOrWhiteSpace(correo))
            {
                if (tipo == 0)
                {
                    html = $@"
                    <div>
                        <h4 style='text-align: center;'>STOCK DE PRODUCTO MODIFICADO</h4><hr>
                        <p>El stock del producto <span style='color: red;'>{producto}</span> ha sido modificado desde
                        {origen}, su stock <b>anterior</b> era de <span style='color: red;'>${stockAnterior}</span> y fue actualizado
                        por el <b>nuevo</b> stock de <span style='color: red;'>${stockNuevo}</span>.</p>
                        <p style='font-size: 0.8em;'>Fecha de Modificación: <b>{DateTime.Now}</b></p>
                    </div>";
                }
                
                if (tipo == 1)
                {

                }

                EnviarEmail(html, asunto, correo);
            }
        }
    }
}
