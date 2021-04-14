using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
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

        public static void EjecutarAtajoKeyPreviewDown(PreviewKeyDownEventHandler ubicacion, Form form)
        {
            foreach (Control control in form.Controls)
            {
                control.PreviewKeyDown += new PreviewKeyDownEventHandler(ubicacion);
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
            catch (Exception ex)
            {
                // Se comento el mensaje de exception ya que el usuario no sabe que se le enviara correo
                // y que no aparezca el messagebox
                MessageBox.Show(ex.Message.ToString(), "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return respuesta;
        }

        public static bool EnviarEmailConArchivoPDF(string html, string asunto, string email, string rutaPDF )
        {
            //Con este metodo se pueden mandar correos con archivos pdf
            var respuesta = false;

            try
            {
                MailMessage mensaje = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                mensaje.From = new MailAddress("pudve.contacto@gmail.com", "PUDVE");
                mensaje.To.Add(new MailAddress(email));
                mensaje.Subject = asunto;
                mensaje.IsBodyHtml = true; // para hacer el cuerpo del mensaje como html 
                mensaje.Attachments.Add(new Attachment(rutaPDF));
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
            var asunto = "¡ATENCIÓN! Precio(s) de producto(s) modificado(s) (Aumentado o disminuido)";
            var html = string.Empty;

            var producto = datos[0];
            var precioAnterior = datos[1];
            var precioNuevo = datos[2];
            var origen = datos[3];
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (!string.IsNullOrWhiteSpace(correo))
            {
                string footerCorreo = string.Empty;

                if (FormPrincipal.id_empleado > 0)
                {
                    MetodosBusquedas mb = new MetodosBusquedas();

                    var datosEmpleado = mb.obtener_permisos_empleado(FormPrincipal.id_empleado, FormPrincipal.userID);

                    string nombreEmpleado = datosEmpleado[14];
                    string usuarioEmpleado = datosEmpleado[15];

                    var infoEmpleado = usuarioEmpleado.Split('@');

                    footerCorreo = $"<p style='font-size: 0.8em;'>El cambio fue realizado por el empleado <b>{nombreEmpleado} ({infoEmpleado[1]})</b> del usuario <b>{infoEmpleado[0]}</b> desde {origen} con <span style='color: red;'>fecha de {fechaOperacion}</span></p>";
                }
                else
                {
                    footerCorreo = $"<p style='font-size: 0.8em;'>El cambio fue realizado por el <b>ADMIN</b> del usuario <b>{FormPrincipal.userNickName}</b> desde {origen} con <span style='color: red;'>fecha de {fechaOperacion}</span></p>";
                }

                if (tipo == 0)
                {
                    html = $@"
                    <div>
                        <h4 style='text-align: center;'>PRECIO DE PRODUCTO MODIFICADO</h4><hr>
                        <p>El precio del producto <span style='color: red;'>{producto}</span> ha sido modificado desde
                        {origen}, su precio <b>anterior</b> era de <span style='color: red;'>${precioAnterior}</span> y fue actualizado
                        por el <b>nuevo</b> precio de <span style='color: red;'>${precioNuevo}</span>.</p>
                        {footerCorreo}
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
                        {footerCorreo}
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

        public static void GenerarTicketCorteCaja(DataTable tbCorte)
        {

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
            string  productos = string.Empty, 
                    encabezadoHTML = string.Empty, 
                    pieHTML = string.Empty, 
                    correoHTML = string.Empty, 
                    asunto = string.Empty, 
                    correo = string.Empty,
                    correo1 = string.Empty;

            encabezadoHTML = @" <h1 style='text-align: center; color: red;'>VENTA NO FINALIZADA EN EL SISTEMA</h1><br>
                                <p>Registro de venta no realizada en el sistema; la siguiente lista es de productos registrados en la venta no realizada:</p>
                                <table style='width:100%'>
                                    <tr>
                                        <th style = 'text-align: left;'>Cantidad</th>
                                        <th style = 'text-align: left;'>Precio</th>
                                        <th>Descripcion</th>
                                        <th style = 'text-align: left;'>Descuento</th>
                                        <th style = 'text-align: right;'>Importe</th>
                                    </tr>";

            foreach (var item in productosNoVendidos)
            {
                words = item.Split('|');
                productos += $@"    <tr>
                                        <td style = 'text-align: left;'>
                                            <span style='color: blue;'>{words[0].ToString()}</span>
                                        </td>
                                        <td style = 'text-align: left;'>
                                            <span style='color: blue;'>{words[1].ToString()}</span>
                                        </td>
                                        <td style = 'text-align: center;'>
                                            <span style='color: black;'><b>{words[2].ToString()}</b></span>
                                        </td>
                                        <td style = 'text-align: left;'>
                                            <span style='color: blue;'>{words[3].ToString()}</span>
                                        </td>
                                        <td style = 'text-align: right;'>
                                            <span style='color: blue;'><b>{words[4].ToString()}</b></span>
                                        </td>
                                    </tr>";
            }

            string footerCorreo = string.Empty;

            if (FormPrincipal.id_empleado > 0)
            {
                MetodosBusquedas mb = new MetodosBusquedas();

                var datosEmpleado = mb.obtener_permisos_empleado(FormPrincipal.id_empleado, FormPrincipal.userID);

                string nombreEmpleado = datosEmpleado[14];
                string usuarioEmpleado = datosEmpleado[15];

                var infoEmpleado = usuarioEmpleado.Split('@');

                footerCorreo = $"<p style='font-size: 0.8em;'>Está operación fue realizada por el empleado <b>{nombreEmpleado} ({infoEmpleado[1]})</b> del usuario <b>{infoEmpleado[0]}</b> con <span style='color: red;'>fecha de {fechaSistema}</span></p>";
            }
            else
            {
                footerCorreo = $"<p style='font-size: 0.8em;'>Está operación fue realizada por el <b>ADMIN</b> del usuario <b>{FormPrincipal.userNickName}</b> con <span style='color: red;'>fecha de {fechaSistema}</span></p>";
            }

            pieHTML = $@"           <tr>
                                        <td colspan='4' style = 'text-align: right;'>
                                            Total =
                                        </td>
                                        <td style = 'text-align: right;'>
                                            <span style='color: red'><b>{cantidadTotal}</b></span>
                                        </td>
                                    </tr>
                                </table>
                                <p>La ventana fue cerrada cuando contenia productos agregados en la lista.</p>
                                {footerCorreo}";

            correoHTML = encabezadoHTML + productos + pieHTML;

            asunto = "Venta no realizada en el sistema (Al cerrar la ventana de ventas)";
            correo = datosUsuario[9].ToString();
            correo1 = "micorreoeshouse_1@hotmail.com";
            //correo = "genebi@outlook.com";

            if (!correo.Equals(""))
            {
                EnviarEmail(correoHTML, asunto, correo);
            }
        }

        /// <summary>
        /// Metodo para envio de correo de resta del producto
        /// </summary>
        /// <param name="productoRestado">Lista de productos en la listas de venta</param>
        /// <param name="fechaSistema">Fecha de operación de venta del sistema</param>
        /// <param name="datosUsuario">Datos del usuario que realiza la venta</param>
        public static void ventaProductLessEmail(List<string> productoRestado, string fechaSistema, string[] datosUsuario)
        {
            decimal monto = 0;
            string[] words;
            string productos = string.Empty,
                    encabezadoHTML = string.Empty,
                    pieHTML = string.Empty,
                    correoHTML = string.Empty,
                    asunto = string.Empty,
                    correo = string.Empty,
                    correo1 = string.Empty;

            encabezadoHTML = @"<h1 style='text-align: center; color: red;'>PRODUCTO RESTADO (Click simbolo de menos) AL LISTADO DE LA VENTA</h1><br>
                               <p>Registro de disminución del producto; el listado de las cantidades restadas es el siguiente :</p>
                               <table style='width:100%'>
                                    <tr>
                                        <th style = 'text-align: left;'>Cantidad Restada</th>
                                        <th style = 'text-align: left;'>Precio</th>
                                        <th>Descripcion</th>
                                        <th style = 'text-align: left;'>Descuento</th>
                                        <th style = 'text-align: right;'>Importe</th>
                                    </tr>";

            foreach (var item in productoRestado)
            {
                words = item.Split('|');
                productos += $@"    <tr>
                                        <td style = 'text-align: left;'>
                                            <span style='color: blue;'>{words[0].ToString()}</span>
                                        </td>
                                        <td style = 'text-align: left;'>
                                            <span style='color: blue;'>{words[1].ToString()}</span>
                                        </td>
                                        <td style = 'text-align: center;'>
                                            <span style='color: black;'><b>{words[2].ToString()}</b></span>
                                        </td>
                                        <td style = 'text-align: left;'>
                                            <span style='color: blue;'>{words[3].ToString()}</span>
                                        </td>
                                        <td style = 'text-align: right;'>
                                            <span style='color: blue;'><b>{words[4].ToString()}</b></span>
                                        </td>
                                    </tr>";
                monto += Convert.ToDecimal(words[4].ToString());
            }

            pieHTML = $@"           <tr>
                                        <td colspan='4' style = 'text-align: right;'>
                                            Total =
                                        </td>
                                        <td style = 'text-align: right;'>
                                            <span style='color: red'><b>{monto}</b></span>
                                        </td>
                                    </tr>
                               </table>
                               <p>Está operación fue realizada con <span style='color:red;'>fecha de {fechaSistema}</span> por el <span style='color: red'>usuario = {datosUsuario[0].ToString()}</span></p>";

            correoHTML = encabezadoHTML + productos + pieHTML;

            asunto = "Producto Restado (click simbolo de menos) Al Listado De La Venta";
            correo = datosUsuario[9].ToString();

            if (!correo.Equals(""))
            {
                EnviarEmail(correoHTML, asunto, correo);
            }
        }

        /// <summary>
        /// Metodo para envio de correo de Productos Eliminados
        /// </summary>
        /// <param name="productoEliminado">Lista de Productos en la lista de Productos Eliminados</param>
        /// <param name="fechaSistema">Fecha de operacion de eliminacion de los productos del listdo de ventas</param>
        /// <param name="datosUsuario">Datos del usuario que realiza la venta</param>
        public static void ventaProductDeleteEmail(List<string> productoEliminado, string fechaSistema, string[] datosUsuario)
        {
            decimal monto = 0;
            string[] words;
            string productos = string.Empty,
                    encabezadoHTML = string.Empty,
                    pieHTML = string.Empty,
                    correoHTML = string.Empty,
                    asunto = string.Empty,
                    correo = string.Empty,
                    correo1 = string.Empty;

            encabezadoHTML = @"<h1 style='text-align: center; color: red;'>PRODUCTO ELIMINADO (click simbolo de x) DEL LISTADO DE LA VENTA</h1><br>
                               <p>Registro de eliminación del producto de la lista de venta; las cantidades eliminadas son las siguientes :</p>
                               <table style='width:100%'>
                                    <tr>
                                        <th style = 'text-align: left;'>Cantidad Eliminada</th>
                                        <th style = 'text-align: left;'>Precio</th>
                                        <th>Descripcion</th>
                                        <th style = 'text-align: left;'>Descuento</th>
                                        <th style = 'text-align: right;'>Importe</th>
                                    </tr>";

            foreach (var item in productoEliminado)
            {
                words = item.Split('|');
                productos += $@"    <tr>
                                        <td style = 'text-align: left;'>
                                            <span style='color: blue;'>{words[0].ToString()}</span>
                                        </td>
                                        <td style = 'text-align: left;'>
                                            <span style='color: blue;'>{words[1].ToString()}</span>
                                        </td>
                                        <td style = 'text-align: center;'>
                                            <span style='color: black;'><b>{words[2].ToString()}</b></span>
                                        </td>
                                        <td style = 'text-align: left;'>
                                            <span style='color: blue;'>{words[3].ToString()}</span>
                                        </td>
                                        <td style = 'text-align: right;'>
                                            <span style='color: blue;'><b>{words[4].ToString()}</b></span>
                                        </td>
                                    </tr>";
                monto += Convert.ToDecimal(words[4].ToString());
            }

            pieHTML = $@"           <tr>
                                        <td colspan='4' style = 'text-align: right;'>
                                            Total =
                                        </td>
                                        <td style = 'text-align: right;'>
                                            <span style='color: red'><b>{monto}</b></span>
                                        </td>
                                    </tr>
                               </table>
                               <p>Está operación fue realizada con <span style='color:red;'>fecha de {fechaSistema}</span> por el <span style='color: red'>usuario = {datosUsuario[0].ToString()}</span></p>";

            correoHTML = encabezadoHTML + productos + pieHTML;

            asunto = "Producto Eliminado (click simbolo de x) Al Listado De La Venta";
            correo = datosUsuario[9].ToString();

            if (!correo.Equals(""))
            {
                EnviarEmail(correoHTML, asunto, correo);
            }
        }

        public static string RemoverCeroStock(string cantidad)
        {
            string[] tmp = cantidad.Split('.');

            if (tmp.Count() > 1)
            {
                if (tmp[1] == "00")
                {
                    cantidad = tmp[0];
                }
            }

            return cantidad;
        }

        /// <summary>
        /// Metodo para envio de correo del Ultimo Producto Agregado fue Eliminado
        /// </summary>
        /// <param name="productoUltimoAgregadoEliminado">Listado de Productos que se han eliminado</param>
        /// <param name="fechaSistema">Fecha de operacion de eliminacion de los productos del listdo de ventas</param>
        /// <param name="datosUsuario">Datos del usuario que realiza la venta</param>
        public static void ventaBtnUltimoEliminadoEmail(List<string> productoUltimoAgregadoEliminado, string fechaSistema, string[] datosUsuario)
        {
            decimal monto = 0;
            string[] words;
            string productos = string.Empty,
                    encabezadoHTML = string.Empty,
                    pieHTML = string.Empty,
                    correoHTML = string.Empty,
                    asunto = string.Empty,
                    correo = string.Empty,
                    correo1 = string.Empty;

            encabezadoHTML = @"<h1 style='text-align: center; color: red;'>PRODUCTO ELIMINADO (Último en agregarse) DEL LISTADO DE LA VENTA</h1><br>
                               <p>Registro de eliminación del producto de la lista de venta; las cantidades eliminadas son las siguientes :</p>
                               <table style='width:100%'>
                                    <tr>
                                        <th style = 'text-align: left;'>Cantidad Eliminada</th>
                                        <th style = 'text-align: left;'>Precio</th>
                                        <th>Descripcion</th>
                                        <th style = 'text-align: left;'>Descuento</th>
                                        <th style = 'text-align: right;'>Importe</th>
                                    </tr>";

            foreach (var item in productoUltimoAgregadoEliminado)
            {
                words = item.Split('|');
                productos += $@"    <tr>
                                        <td style = 'text-align: left;'>
                                            <span style='color: blue;'>{words[0].ToString()}</span>
                                        </td>
                                        <td style = 'text-align: left;'>
                                            <span style='color: blue;'>{words[1].ToString()}</span>
                                        </td>
                                        <td style = 'text-align: center;'>
                                            <span style='color: black;'><b>{words[2].ToString()}</b></span>
                                        </td>
                                        <td style = 'text-align: left;'>
                                            <span style='color: blue;'>{words[3].ToString()}</span>
                                        </td>
                                        <td style = 'text-align: right;'>
                                            <span style='color: blue;'><b>{words[4].ToString()}</b></span>
                                        </td>
                                    </tr>";
                monto += Convert.ToDecimal(words[4].ToString());
            }

            pieHTML = $@"           <tr>
                                        <td colspan='4' style = 'text-align: right;'>
                                            Total =
                                        </td>
                                        <td style = 'text-align: right;'>
                                            <span style='color: red'><b>{monto}</b></span>
                                        </td>
                                    </tr>
                               </table>
                               <p>Está operación fue realizada con <span style='color:red;'>fecha de {fechaSistema}</span> por el <span style='color: red'>usuario = {datosUsuario[0].ToString()}</span></p>";

            correoHTML = encabezadoHTML + productos + pieHTML;

            asunto = "Producto Eliminado (Último en agregarse) Al Listado De La Venta";
            correo = datosUsuario[9].ToString();

            if (!correo.Equals(""))
            {
                EnviarEmail(correoHTML, asunto, correo);
            }
        }

        /// <summary>
        /// Metodo para envio de correo del Listado de Productos fue eliminado
        /// </summary>
        /// <param name="productosNoVendidos">Lista de Productos en la lista de Productos Eliminados</param>
        /// <param name="fechaSistema">Fecha de operacion de eliminacion de los productos del listdo de ventas</param>
        /// <param name="importeTotal">Importe total de la vent no realizada</param>
        /// <param name="datosUsuario">Datos del usuario que realiza la venta</param>
        public static void ventaBtnClarAllItemSaleEmail(List<string> productosNoVendidos, string fechaSistema, string importeTotal, string[] datosUsuario)
        {
            string[] words;
            string productos = string.Empty,
                    encabezadoHTML = string.Empty,
                    pieHTML = string.Empty,
                    correoHTML = string.Empty,
                    asunto = string.Empty,
                    correo = string.Empty,
                    correo1 = string.Empty;

            encabezadoHTML = @" <h1 style='text-align: center; color: red;'>VENTA NO FINALIZADA (Click en botón borrar todos los productos) EN EL SISTEMA</h1><br>
                                <p>Registro de venta no realizada en el sistema; la siguiente lista es de productos registrados en la venta no realizada:</p>
                                <table style='width:100%'>
                                    <tr>
                                        <th style = 'text-align: left;'>Cantidad</th>
                                        <th style = 'text-align: left;'>Precio</th>
                                        <th>Descripcion</th>
                                        <th style = 'text-align: left;'>Descuento</th>
                                        <th style = 'text-align: right;'>Importe</th>
                                    </tr>";

            foreach (var item in productosNoVendidos)
            {
                words = item.Split('|');
                productos += $@"    <tr>
                                        <td style = 'text-align: left;'>
                                            <span style='color: blue;'>{words[0].ToString()}</span>
                                        </td>
                                        <td style = 'text-align: left;'>
                                            <span style='color: blue;'>{words[1].ToString()}</span>
                                        </td>
                                        <td style = 'text-align: center;'>
                                            <span style='color: black;'><b>{words[2].ToString()}</b></span>
                                        </td>
                                        <td style = 'text-align: left;'>
                                            <span style='color: blue;'>{words[3].ToString()}</span>
                                        </td>
                                        <td style = 'text-align: right;'>
                                            <span style='color: blue;'><b>{words[4].ToString()}</b></span>
                                        </td>
                                    </tr>";
            }

            string footerCorreo = string.Empty;

            if (FormPrincipal.id_empleado > 0)
            {
                MetodosBusquedas mb = new MetodosBusquedas();

                var datosEmpleado = mb.obtener_permisos_empleado(FormPrincipal.id_empleado, FormPrincipal.userID);

                string nombreEmpleado = datosEmpleado[14];
                string usuarioEmpleado = datosEmpleado[15];

                var infoEmpleado = usuarioEmpleado.Split('@');

                footerCorreo = $"<p style='font-size: 0.8em;'>Está operación fue realizada por el empleado <b>{nombreEmpleado} ({infoEmpleado[1]})</b> del usuario <b>{infoEmpleado[0]}</b> con <span style='color: red;'>fecha de {fechaSistema}</span></p>";
            }
            else
            {
                footerCorreo = $"<p style='font-size: 0.8em;'>Está operación fue realizada por el <b>ADMIN</b> del usuario <b>{FormPrincipal.userNickName}</b> con <span style='color: red;'>fecha de {fechaSistema}</span></p>";
            }

            pieHTML = $@"           <tr>
                                        <td colspan='4' style = 'text-align: right;'>
                                            Total =
                                        </td>
                                        <td style = 'text-align: right;'>
                                            <span style='color: red'><b>{importeTotal}</b></span>
                                        </td>
                                    </tr>
                                </table>
                                {footerCorreo}";

            correoHTML = encabezadoHTML + productos + pieHTML;

            asunto = "Venta no realizada en el sistema";
            correo = datosUsuario[9].ToString();

            if (!correo.Equals(""))
            {
                EnviarEmail(correoHTML, asunto, correo);
            }
        }

        /// <summary>
        /// Metodo para envio de correo de Dinero Agregado, Retirado y Corte en Caja
        /// </summary>
        /// <param name="datosOperacion">Listado de datos de la operacion que se realizo</param>
        public static void cajaBtnAgregarRetiroCorteDineroCajaEmail(string[] datosOperacion)
        {
            string[] datosUsuario = FormPrincipal.datosUsuario;
            string[] datosEnvioCorreo = datosOperacion;

            string OperacionRealizada = datosEnvioCorreo[0].ToString(), 
                    CantidadDeOperacion = datosEnvioCorreo[1].ToString(), 
                    SaldoDeOperacion = datosEnvioCorreo[2].ToString(), 
                    ConceptoDeOperacion = datosEnvioCorreo[3].ToString(), 
                    FechaDeOperacion = datosEnvioCorreo[4].ToString(), 
                    IDUsuarioDeOperacion = datosEnvioCorreo[5].ToString(), 
                    MontoEfectivoDeOperacion = datosEnvioCorreo[6].ToString(), 
                    MontoTarjetaDeOperacion = datosEnvioCorreo[7].ToString(), 
                    MontoValesDeOperacion = datosEnvioCorreo[8].ToString(), 
                    MontoChequesDeOperacion = datosEnvioCorreo[9].ToString(), 
                    MontoTransferenciaDeOperacion = datosEnvioCorreo[10].ToString(), 
                    MontoCreditoDeOperacion = datosEnvioCorreo[11].ToString(), 
                    MontoAnticipoDeOperacion = datosEnvioCorreo[12].ToString();

            string NombreUsuario = datosUsuario[0].ToString(), 
                    CalleUsuario = datosUsuario[1].ToString(), 
                    NoExteriorUsuario = datosUsuario[2].ToString(), 
                    NoInteriorUsuario = datosUsuario[3].ToString(), 
                    MunicipioUsuario = datosUsuario[4].ToString(), 
                    EstadoUsuario = datosUsuario[5].ToString(), 
                    ColoniaUsuario = datosUsuario[6].ToString(), 
                    CodigoPostal = datosUsuario[7].ToString(), 
                    RFCUsuario = datosUsuario[8].ToString(), 
                    EmailUsuario = datosUsuario[9].ToString(), 
                    TelefonoUsuario = datosUsuario[10].ToString(), 
                    LogoUsuario = datosUsuario[11].ToString(), 
                    VerificadoUsuario = datosUsuario[12].ToString(), 
                    IDUsuario = datosUsuario[13].ToString(), 
                    PasswordUsuario = datosUsuario[14].ToString(), 
                    FechaHoyUsuario = datosUsuario[15].ToString();

            string cuerpoHTML = string.Empty,
                    encabezadoHTML = string.Empty,
                    pieHTML = string.Empty,
                    correoHTML = string.Empty,
                    asunto = string.Empty,
                    correo = string.Empty;

            if (OperacionRealizada.Equals("deposito"))
            {
                encabezadoHTML = @"<h1 style='text-align: center; color: blue;'>AGREGAR DINERO A CAJA (Click en botón Agregar Dinero) EN EL SISTEMA</h1><br>
                                <p>Registro de deposito de dinero en el sistema; la siguiente información es la registrada en dicha operación:</p>";
                cuerpoHTML = $@"<div style = 'text-align: center;'>
                                    <table style = 'width:50%; margin: 0 auto; text-align: left;'>
                                        <tr>
                                            <th style = 'text-align: center;' colspan = '3'>Cantidad fue Agregada</th>
                                        </tr>";
                asunto = "Agregar Dinero \"Apartado Caja\"";
            }
            else if (OperacionRealizada.Equals("retiro"))
            {
                encabezadoHTML = @"<h1 style='text-align: center; color: red;'>RETIRO DE DINERO CAJA (Click en botón Retirar Dinero) EN EL SISTEMA</h1><br>
                                <p>Registro de retiro de dinero en el sistema; la siguiente información es la registrada en dicha operación:</p>";
                cuerpoHTML = $@"<div style = 'text-align: center;'>
                                    <table style = 'width:50%; margin: 0 auto; text-align: left;'>
                                        <tr>
                                            <th style = 'text-align: center;' colspan = '3'>Cantidad a Retirada</th>
                                        </tr>";
                asunto = "Retiro Dinero \"Apartado Caja\"";
            }

                  cuerpoHTML += $@"     <tr>
                                            <th style = 'text-align: left;'>
                                                Efectivo:
                                            </th>
                                            <th style = 'text-align: center;'>
                                                <span style='color: blue;'>{MontoEfectivoDeOperacion}</span>
                                            </th>
                                            <th style = 'text-align: left;'>
                                                Cheque:
                                            </th>
                                            <th style = 'text-align: center;'>
                                                <span style='color: blue;'>{MontoChequesDeOperacion}</span>
                                            </th>
                                        </tr>
                                        <tr>
                                            <th style = 'text-align: left;'>
                                                Tarjeta:
                                            </th>
                                            <th style = 'text-align: center;'>
                                                <span style='color: blue;'>{MontoTarjetaDeOperacion}</span>
                                            </th>
                                            <th style = 'text-align: left;'>
                                                Transferencia:
                                            </th>
                                            <th style = 'text-align: center;'>
                                                <span style='color: blue;'>{MontoTransferenciaDeOperacion}</span>
                                            </th>
                                        </tr>
                                        <tr>
                                            <th style = 'text-align: left;'>
                                                Vales:
                                            </th>
                                            <th style = 'text-align: center;'>
                                                <span style='color: blue;'>{MontoValesDeOperacion}</span>
                                            </th>
                                            <th style = 'text-align: left;'>
                                                Crédito:
                                            </th>
                                            <th style = 'text-align: center;'>
                                                <span style='color: blue;'>{MontoCreditoDeOperacion}</span>
                                            </th>
                                        </tr>
                                        <tr>
                                            <th style = 'text-align: center;' colspan = '3'>
                                                Concepto del {OperacionRealizada}:
                                            </th>
                                        </tr>

                                        <tr>
                                            <th style = 'text-align: center;' colspan = '3'>
                                                <span style='color: blue;'>{ConceptoDeOperacion}</span>
                                            </th>
                                        </tr>
                                    </table>
                                </div>";

            if (!string.IsNullOrWhiteSpace(datosEnvioCorreo[13]))
            {
                var datosEmpleado = datosEnvioCorreo[14].Split('@');

                pieHTML = $@"<p>Está operación fue realizada por {datosEnvioCorreo[13]} ({datosEmpleado[1]}) del usuario {datosEmpleado[0]} con <span style='color:red;'>fecha de {FechaDeOperacion}</span></p>";
            }
            else
            {
                pieHTML = $@"<p>Está operación fue realizada por el ADMIN del usuario {FormPrincipal.userNickName} con <span style='color:red;'>fecha de {FechaDeOperacion}</span></p>";
            }

            //pieHTML = $@"<p>Está operación fue realizada por con <span style='color:red;'>fecha de {FechaDeOperacion}</span> por el <span style='color: red'>usuario = {NombreUsuario}</span></p>";

            correoHTML = encabezadoHTML + cuerpoHTML + pieHTML;
            
            correo = EmailUsuario;

            if (!correo.Equals(""))
            {
                EnviarEmail(correoHTML, asunto, correo);
            }
        }

        public static void enviarCorreoCorteCaja(string correo, string[] datos, string ruta)
        {
            var asunto = "SE HA REALIZADO CORTE DE CAJA";
            var html = $@"
                <div style='text-align: center;'>
                <h1 style='color: red;'>SE HA REALIZADO CORTE DE CAJA</h1>
                </div>
       
<div style='display: inline-table;'>
			<table border='1'>
				<tr>
					<th colspan='2'>
					<span>VENTAS</span>
					</th>
				</tr>
				<tr>
					<td>Efectivo</td>
					<td><span style='color: black;'>{datos[0]}</span></td>
				</tr>
				<tr>
					<td>Tarjeta</td>
					<td><span style='color: black;'>{datos[1]}</span></td>
				</tr>
				<tr>
					<td>Vales</td>
					<td><span style='color: black;'>{datos[2]}</span></td>
				</tr>
				<tr>
					<td>Cheque</td>
					<td><span style='color: black;'>{datos[3]}</span></td>
				</tr>
				<tr>
					<td>Transferencia</td>
					<td><span style='color: black;'>{datos[4]}</span></td>
				</tr>
				<tr>
					<td>Credito</td>
					<td><span style='color: black;'>{datos[5]}</span></td>
				</tr>
				<tr>
					<td>Abonos</td>
					<td><span style='color: black;'>{datos[6]}</span></td>
				</tr>
				<tr>
					<td>Anticipos Utilizados al Corte</td>
					<td><span style='color: black;'>{datos[7]}</span></td>
				</tr>
				<tr>
					<td style='color: crimson'>Total Ventas</td>
					<td><span style='color: crimson;'>{datos[8]}</span></td>
				</tr>
			</table>
		</div>

		<div style='display: inline-table;'>
			<table border='1'>
				<tr>
					<th colspan='2'>
					<span>ANTICIPOS RECIBIDOS</span>
					</th>
				</tr>
				<tr>
					<td>Efectivo</td>
					<td><span style='color: black;'>{datos[9]}</span></td>
				</tr>
				<tr>
					<td>Tarjeta</td>
					<td><span style='color: black;'>{datos[10]}</span></td>
				</tr>
				<tr>
					<td>Vales</td>
					<td><span style='color: black;'>{datos[11]}</span></td>
				</tr>
				<tr>
					<td>Cheque</td>
					<td><span style='color: black;'>{datos[12]}</span></td>
				</tr>
				<tr>
					<td>Transferencia</td>
					<td><span style='color: black;'>{datos[13]}</span></td>
				</tr>
				<tr>
					<td style='color: crimson'>Total Ventas</td>
					<td><span style='color: crimson;'>{datos[14]}</span></td>
				</tr>
			</table>
			</div>

			<div style='display: inline-table;'>
				<table border='1'>
				<tr>
					<th colspan='2'>
					<span>DINERO AGREGADO</span>
					</th>
				</tr>
				<tr>
					<td>Efectivo</td>
					<td><span style='color: black;'>{datos[15]}</span></td>
				</tr>
				<tr>
					<td>Tarjeta</td>
					<td><span style='color: black;'>{datos[16]}</span></td>
				</tr>
				<tr>
					<td>Vales</td>
					<td><span style='color: black;'>{datos[17]}</span></td>
				</tr>
				<tr>
					<td>Cheque</td>
					<td><span style='color: black;'>{datos[18]}</span></td>
				</tr>
				<tr>
					<td>Transferencia</td>
					<td><span style='color: black;'>{datos[19]}</span></td>
				</tr>
				<tr>
					<td style='color: crimson'>Total Ventas</td>
					<td><span style='color: crimson;'>{datos[20]}</span></td>
				</tr>
			</table>
			</div>
			
		<div style='display: inline-table;'>
			<table border='1'>
				<tr>
					<th colspan='2'>
					<span>DINERO RETIRADO</span>
					</th>
				</tr>
				<tr>
					<td>Efectivo</td>
					<td><span style='color: black;'>{datos[21]}</span></td>
				</tr>
				<tr>
					<td>Tarjeta</td>
					<td><span style='color: black;'>{datos[22]}</span></td>
				</tr>
				<tr>
					<td>Vales</td>
					<td><span style='color: black;'>{datos[23]}</span></td>
				</tr>
				<tr>
					<td>Cheque</td>
					<td><span style='color: black;'>{datos[24]}</span></td>
				</tr>
				<tr>
					<td>Transferencia</td>
					<td><span style='color: black;'>{datos[25]}</span></td>
				</tr>
				<tr>
					<td>Anticipos Utilizados al Corte</td>
					<td><span style='color: black;'>{datos[26]}</span></td>
				</tr>
				<tr>
					<td>Devoluciones</td>
					<td><span style='color: black;'>{datos[27]}</span></td>
				</tr>
				<tr>
					<td style='color: crimson'>Total Ventas</td>
					<td><span style='color: crimson;'>{datos[28]}</span></td>
				</tr>
			</table>
		</div>
			
			<div style='display: inline-table;'>
				<table border='1'>
				<tr>
					<th colspan='2'>
					<span>TOTAL EN CAJA</span>
					</th>
				</tr>
				<tr>
					<td>Efectivo</td>
					<td><span style='color: black;'>{datos[29]}</span></td>
				</tr>
				<tr>
					<td>Tarjeta</td>
					<td><span style='color: black;'>{datos[30]}</span></td>
				</tr>
				<tr>
					<td>Vales</td>
					<td><span style='color: black;'>{datos[31]}</span></td>
				</tr>
				<tr>
					<td>Cheque</td>
					<td><span style='color: black;'>{datos[32]}</span></td>
				</tr>
				<tr>
					<td>Transferencia</td>
					<td><span style='color: black;'>{datos[33]}</span></td>
				</tr>
				<tr>
					<td>Saldo Inicial</td>
					<td><span style='color: black;'>{datos[34]}</span></td>
				</tr>
				<tr>
					<td>Total Credito</td>
					<td><span style='color: black;'>{datos[35]}</span></td>
				</tr>
				<tr>
					<td style='color: crimson'>Total Ventas</td>
					<td><span style='color: crimson;'>{datos[36]}</span></td>
				</tr>
			</table>	
			</div>
                        ";

            EnviarEmailConArchivoPDF(html, asunto, correo, ruta);
        }

        public static bool BuscarDataGridView(string TextoABuscar, string Columna, DataGridView grid)
        {
            bool encontrado = false;

            if (TextoABuscar == string.Empty)
            {
                return false;
            }

            if (grid.RowCount == 0)
            {
                return false;
            }

            grid.ClearSelection();

            if (Columna == string.Empty)
            {
                foreach (DataGridViewRow row in grid.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value.ToString() == TextoABuscar)
                        {
                            row.Selected = true;
                            return true;
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (row.Cells[Columna].Value.ToString() == TextoABuscar)
                    {
                        row.Selected = true;
                        return true;
                    }
                }
            }

            return encontrado;
        }
    }
}
