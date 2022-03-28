using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TuesPechkin;

namespace PuntoDeVentaV2
{
    public static class Utilidades
    {
        public static void CrearMarcaDeAgua(int idVenta, string texto)
        {
            Conexion cn = new Conexion();
            Consultas cs = new Consultas();
            MetodosBusquedas mb = new MetodosBusquedas();

            var servidor = Properties.Settings.Default.Hosting;
            var archivoCopia = string.Empty;
            var archivoPDF = string.Empty;
            var nuevoPDF = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                archivoCopia = $@"\\{servidor}\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}_tmp.pdf";
                archivoPDF = $@"\\{servidor}\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}.pdf";

                if (!File.Exists(archivoPDF))
                {
                    var dtImpVenta = cn.CargarDatos(cs.ReimprimirTicket(idVenta));
                    var datosConfig = mb.DatosConfiguracion();
                    bool imprimirCodigo = false;

                    if (Convert.ToInt16(datosConfig[0]) == 1)
                    {
                        imprimirCodigo = true;
                    }

                    if (!dtImpVenta.Rows.Count.Equals(0))
                    {
                        GenerarTicket(dtImpVenta, imprimirCodigo);
                    }
                }

                nuevoPDF = archivoPDF;

                // Renombramos el archivo PDF
                File.Move(archivoPDF, archivoCopia);
            }
            else
            {
                archivoCopia = $@"C:\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}_tmp.pdf";
                archivoPDF = $@"C:\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}.pdf";

                if (!File.Exists(archivoPDF))
                {
                    var dtImpVenta = cn.CargarDatos(cs.ReimprimirTicket(idVenta));
                    var datosConfig = mb.DatosConfiguracion();
                    bool imprimirCodigo = false;

                    if (Convert.ToInt16(datosConfig[0]) == 1)
                    {
                        imprimirCodigo = true;
                    }

                    if (!dtImpVenta.Rows.Count.Equals(0))
                    {
                        GenerarTicket(dtImpVenta, imprimirCodigo);
                    }
                }

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

            var respuesta = MessageBox.Show("¿Desea imprimir el ticket?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                ImprimirTicket(folioTicket.ToString(), 1);
            }
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

                ProcessStartInfo pi = new ProcessStartInfo(ruta);
                pi.UseShellExecute = true;
                pi.Verb = "print";
                Process process = Process.Start(pi);

                //ProcessStartInfo info = new ProcessStartInfo();
                //info.Verb = "print";
                //info.FileName = ruta;
                //info.CreateNoWindow = true;
                //info.WindowStyle = ProcessWindowStyle.Hidden;

                //Process p = new Process();
                //p.StartInfo = info;
                //p.Start();

                //p.WaitForInputIdle();
                //System.Threading.Thread.Sleep(5000);

                //if (false == p.CloseMainWindow())
                //{
                //    p.Kill();
                //}
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
                smtp.Credentials = new NetworkCredential("pudve.contacto@gmail.com", "grtpoxrdmngbozwm");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mensaje);

                respuesta = true;
            }
            catch (Exception ex)
            {
                // Se comento el mensaje de exception ya que el usuario no sabe que se le enviara correo
                // y que no aparezca el messagebox
                //MessageBox.Show(ex.Message.ToString(), "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return respuesta;
        }

        public static bool EnviarEmailConArchivoPDF(string html, string asunto, string email, string rutaPDF)
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
                smtp.Credentials = new NetworkCredential("pudve.contacto@gmail.com", "grtpoxrdmngbozwm");
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

        public static void CambioPrecioProductoEmail(string[] datos, int tipo = 0)
        {
            var correo = FormPrincipal.datosUsuario[9];
            var asunto = "¡ATENCIÓN! PRECIO(S) DE PRODUCTO(S) MODIFICADO(S) (AUMENTADO O DISMINUIDO)";
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

                    string nombreEmpleado = datosEmpleado[15];
                    string usuarioEmpleado = datosEmpleado[16];

                    var infoEmpleado = usuarioEmpleado.Split('@');

                    footerCorreo = $"<p style='font-size: 0.9em;'>El cambio fue realizado por el empleado <b>{nombreEmpleado} ({infoEmpleado[1]})</b> del usuario <b>{infoEmpleado[0]}</b> desde {origen} con <span style='color: red;'>fecha de {fechaOperacion}</span></p>";
                }
                else
                {
                    footerCorreo = $"<p style='font-size: 0.9em;'>El cambio fue realizado por el<FONT SIZE=2> <b>ADMINISTRADOR</b> </FONT> del usuario<FONT SIZE=2> <FONT SIZE=2>  <b>{FormPrincipal.userNickName}</b> </FONT> </FONT> desde {origen} con <span style='color: red;'>fecha de {fechaOperacion}</span></p>";
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
                        <h3 style='text-align: center;'>LISTA DE PRODUCTOS CON PRECIO MODIFICADO</h3><hr>
                        <ul style='font-size: 0.9em;'>
                            {producto}
                        </ul>
                        {footerCorreo}
                    </div>";
                }

                EnviarEmail(html, asunto, correo);
            }
        }

        public static void CambioStockProductoEmail(string[] datos, int tipo = 0, string titulo = "MODIFICADO")
        {
            var producto = datos[0];
            var stockAnterior = datos[1];
            var stockNuevo = datos[2];
            var stockActual = datos[3];
            var origen = datos[4];
            var operacion = datos[5];

            if (operacion.Equals("restó"))
            {
                titulo = "DISMINUIDO";
            }

            var correo = FormPrincipal.datosUsuario[9];
            var asunto = $"STOCK DE PRODUCTO {titulo}";// "Cambio de stock para producto(s)";
            var html = string.Empty;

            asunto += MetodosBusquedas.ObtenerResponsable();

            if (!string.IsNullOrWhiteSpace(correo))
            {
                if (tipo == 0)
                {
                    html = $@"
                    <div>
                        
                        <h4 style='text-align: center;'>STOCK DE PRODUCTO {titulo}</h4><hr>
                        <p>El stock del producto <span style='color: red;'>{producto}</span> ha sido modificado desde
                        {origen}, su stock <b>anterior</b> era de <span style='color: red;'>{stockAnterior}</span>, se {operacion} la cantidad
                        de <span style='color: red;'>{stockNuevo}</span> y su stock <b>actual</b> es de <span style='color: red;'>{stockActual}</span>.</p>
                        <p style='font-size: 0.8em;'>Fecha de Modificación: <b>{DateTime.Now}</b></p>
                    </div>";
                }

                if (tipo == 1)
                {
                    var usuarioEmpleado = FormPrincipal.userNickName.ToString();
                    if (usuarioEmpleado.Contains("@"))
                    {
                        html = $@" 
                        <div>
                        <div style = 'text-align: center;' >
                        <h3>  STOCK DE PRODUCTO MODIFICADO -ASIGNAR </h3>
                        </div>
                        <hr>
                        {producto}
                         <hr>
                        <p style='font-size: 0.9em;'>
                            <span>NOTA:El stock de los productos fue modificado desde {origen} por el <b>EMPLEADO</b> del usuario <b>{FormPrincipal.userNickName}</b> <span style='color: red;'> Fecha de Modificación: {DateTime.Now}</span></span></p></div>";
                    }
                    else
                    {
                        html = $@"
                        <div>
                        <div style = 'text-align: center;' >
                        <h3>  STOCK DE PRODUCTO MODIFICADO -ASIGNAR </h3>
                        </div>
                        <hr>
                        {producto}
                         <hr>
                        <p style='font-size: 0.9em;'>
                            <span>NOTA:El stock de los productos fue modificado desde {origen} por el <b>ADMIN</b> del usuario <b>{FormPrincipal.userNickName}</b> <span style='color: red;'> Fecha de Modificación: {DateTime.Now}</span></span></p></div>";
                    }
                   
                }

                EnviarEmail(html, asunto, correo);
            }
        }

        public static void CambioStockAumentoDecremento(List<string> cadenas, string titulo)
        {
            var correo = FormPrincipal.datosUsuario[9];
            var asunto = $"STOCK DE PRODUCTO {titulo}";// "Cambio de stock para producto(s)";
            var html = string.Empty;

            asunto += MetodosBusquedas.ObtenerResponsable();

            html = $@"
                    <div>
                        <h4 style='text-align: center;'>STOCK DE PRODUCTO {titulo}</h4><hr>
                        <ul>";

            foreach (var cadena in cadenas)
            {
                var producto = cadena.Split('|');

                var nombre = producto[0];
                var stockAnterior = producto[1];
                var stockNuevo = producto[2];
                var stockActual = producto[3];
                var origen = producto[4];
                var operacion = producto[5];

                html += $@"<li>El stock del producto <span style='color: red;'>{nombre}</span> ha sido modificado desde
                        {origen}, su stock <b>anterior</b> era de <span style='color: red;'>{ stockAnterior}</span>, se {operacion}
                        la cantidad de <span style='color: red;'>{stockNuevo}</span> y su stock <b>actual</b> es de <span style='color: red;'>{stockActual}</span>.</li>";
            }

            html += $@"</ul>
                       <p style='font-size: 0.8em;'>Fecha de Modificación: <b>{DateTime.Now}</b></p>
                   </div>";

            Inventario.productosAumentoDecremento.Clear();

            EnviarEmail(html, asunto, correo);
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
            Conexion cn = new Conexion();
            Consultas cs = new Consultas();

            var servidor = Properties.Settings.Default.Hosting;
            var archivoCopia = string.Empty;
            var archivoPDF = string.Empty;
            var nuevoPDF = string.Empty;

            var Usuario = FormPrincipal.userNickName;
            var Folio = string.Empty;
            var Serie = string.Empty;

            using (DataTable dtDatosVentas = cn.CargarDatos(cs.DatosVentaParaLaNota(idVenta)))
            {
                if (!dtDatosVentas.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in dtDatosVentas.Rows)
                    {
                        Folio = item["Folio"].ToString();
                        Serie = item["Serie"].ToString();
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                archivoCopia = $@"\\{servidor}\Archivos PUDVE\Ventas\PDF\{Usuario}\VENTA_NoVenta{idVenta}_Folio{Folio}{Serie}_tmp.pdf";
                archivoPDF = $@"\\{servidor}\Archivos PUDVE\Ventas\PDF\{Usuario}\VENTA_NoVenta{idVenta}_Folio{Folio}{Serie}.pdf";

                nuevoPDF = archivoPDF;

                // Renombramos el archivo PDF
                File.Move(archivoPDF, archivoCopia);
            }
            else
            {
                archivoCopia = $@"C:\Archivos PUDVE\Ventas\PDF\{Usuario}\VENTA_NoVenta{idVenta}_Folio{Folio}{Serie}_tmp.pdf";
                archivoPDF = $@"C:\Archivos PUDVE\Ventas\PDF\{Usuario}\VENTA_NoVenta{idVenta}_Folio{Folio}{Serie}.pdf";

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
            Conexion cn = new Conexion();
            Consultas cs = new Consultas();
            string cargarMensaje = string.Empty;

            var mensaje2 = cn.CargarDatos(cs.MensajeTicket(FormPrincipal.userID));
            foreach (DataRow item in mensaje2.Rows)
            {
                cargarMensaje = item[0].ToString();
            }
            var mensajeTicket = cargarMensaje;

            Paragraph mensaje = new Paragraph(cargarMensaje, fuenteNormal);
            mensaje.Alignment = Element.ALIGN_CENTER;

            string drFecha = tbVenta.Rows[0]["FechaOperacion"].ToString();

            string[] words;

            words = drFecha.Split(' ');

            var DiaFecha = DateTime.Parse(words[0].ToString());

            var culture = new System.Globalization.CultureInfo("es-MX");
            var dia = culture.DateTimeFormat.GetDayName(DiaFecha.DayOfWeek);
            var fecha = drFecha;

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
            string productos = string.Empty,
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

                string nombreEmpleado = datosEmpleado[15];
                string usuarioEmpleado = datosEmpleado[16];

                var infoEmpleado = usuarioEmpleado.Split('@');

                footerCorreo = $"<p style='font-size: 0.8em;'>Está operación fue realizada por el empleado <b>{nombreEmpleado} ({infoEmpleado[1]})</b> del usuario <b>{infoEmpleado[0]}</b> con <span style='color: red;'>fecha de {fechaSistema}</span></p>";
            }
            else
            {
                footerCorreo = $"<p style='font-size: 0.8em;'>Está operación fue realizada por el<FONT SIZE=2> <b>ADMINISTRADOR</b> </FONT> del usuario<FONT SIZE=2> <FONT SIZE=2>  <b>{FormPrincipal.userNickName}</b> </FONT> </FONT> con <span style='color: red;'>fecha de {fechaSistema}</span></p>";
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

                string nombreEmpleado = datosEmpleado[15];
                string usuarioEmpleado = datosEmpleado[16];

                var infoEmpleado = usuarioEmpleado.Split('@');

                footerCorreo = $"<p style='font-size: 0.8em;'>Está operación fue realizada por el empleado <b>{nombreEmpleado} ({infoEmpleado[1]})</b> del usuario <b>{infoEmpleado[0]}</b> con <span style='color: red;'>fecha de {fechaSistema}</span></p>";
            }
            else
            {
                footerCorreo = $"<p style='font-size: 0.8em;'>Está operación fue realizada por el <FONT SIZE=2> <b>ADMINISTRADOR</b>  </FONT> del usuario <FONT SIZE=2> <FONT SIZE=2>  <b>{FormPrincipal.userNickName}</b> </FONT> </FONT> con <span style='color: red;'>fecha de {fechaSistema}</span></p>";
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
                encabezadoHTML = @"<h3 style='text-align: center; color: blue;'>AGREGAR DINERO A CAJA (Click en botón Agregar Dinero) EN EL SISTEMA</h3><br>
                                <p>Registro de deposito de dinero en el sistema; la siguiente información es la registrada en dicha operación:</p>";
                cuerpoHTML = $@"<div style = 'text-align: center;'>
                                    <table style = 'width:50%; margin: 0 auto; text-align: left;'>
                                        <tr>
                                            <th style = 'text-align: center;' colspan = '3'>Cantidad fue Agregada</th>
                                        </tr>";
                asunto = "AGREGAR DINERO \"APARTADO CAJA\"";
            }
            else if (OperacionRealizada.Equals("retiro"))
            {
                encabezadoHTML = @"<h3 style='text-align: center; color: red;'>RETIRO DE DINERO CAJA (Click en botón Retirar Dinero) EN EL SISTEMA</h3><br>
                                <p>Registro de retiro de dinero en el sistema; la siguiente información es la registrada en dicha operación:</p>";
                cuerpoHTML = $@"<div style = 'text-align: center;'>
                                    <table style = 'width:50%; margin: 0 auto; text-align: left;'>
                                        <tr>
                                            <th style = 'text-align: center;' colspan = '3'>Cantidad a Retirada</th>
                                        </tr>";
                asunto = "RETIRO DINERO \"APARTADO CAJA\"";
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

            if (Convert.ToInt32(datosEnvioCorreo[13]) != 0/*!string.IsNullOrWhiteSpace(datosEnvioCorreo[13])*/)//Valida cuando es empleado
            {
                var datosEmpleado = datosEnvioCorreo[14].Split('@');

                pieHTML = $@"<p>Está operación fue realizada por {datosEnvioCorreo[13]} ({datosEmpleado[1]}) del usuario {datosEmpleado[0]} con <span style='color:red;'>fecha de {FechaDeOperacion}</span></p>";
            }
            else
            {
                pieHTML = $@"<p>Está operación fue realizada por el<FONT SIZE=2> <b>ADMINISTRADOR</b> </FONT> del usuario <FONT SIZE=2>   <b>{FormPrincipal.userNickName}</b> </FONT> con <span style='color:red;'>fecha de {FechaDeOperacion}</span></p>";
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

        public static void EnviarCorreoRespaldo(string correo, string ruta)
        {
            DateTime fechaActual = DateTime.Now;
            var usuarioCreador = FormPrincipal.userNickName;
            var tipoUsuario = string.Empty;
            var usuarioFinal = string.Empty;


            if (usuarioCreador.Contains('@'))
            {
                tipoUsuario = "Empleado";
                //string[] datoSeparado = usuarioCreador.Split('@');
            }
            else
            {
                tipoUsuario = "ADMIN";
            }

            var asunto = $"Respaldo de la base de datos - {usuarioCreador}";
            var html = $@"
                <div style='text-align: center;'>
                <h1 style='color: red;'>SE HA REALIZADO UN RESPALDO DE LA BASE DE DATOS</h1>
                </div>
                
                <div style='text-align: center;'>
                <h4> Usuario: {usuarioCreador} </h4>
                <h4> Fecha: {fechaActual.ToString()} </h4>
                </div>

                <div style='text-align: left;'> 
                <h4> El respaldo fue realizado por el {tipoUsuario} del usuario {usuarioCreador} </h4>
                </div>
                
            ";

            EnviarEmailConArchivoPDF(html, asunto, correo, ruta);
        }

        public static bool BuscarDataGridView(string TextoABuscar1, string Columna1, DataGridView grid, string TextoABuscar2, string Columna2)
        {
            bool encontrado = false;
            bool textoBuscarUnoEncontrado = false;
            bool textoBuscarDosEncontrado = false;

            if (TextoABuscar1 == string.Empty ||
                TextoABuscar2 == string.Empty)
            {
                return false;
            }

            if (grid.RowCount == 0)
            {
                return false;
            }

            if (TextoABuscar2.Equals("P"))
            {
                TextoABuscar2 = "PRODUCTO";
            }
            else if (TextoABuscar2.Equals("S"))
            {
                TextoABuscar2 = "SERVICIO";
            }
            else if (TextoABuscar2.Equals("PQ"))
            {
                TextoABuscar2 = "COMBO";
            }

            grid.ClearSelection();

            if (Columna1 == string.Empty || Columna2 == string.Empty)
            {
                foreach (DataGridViewRow row in grid.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value.ToString() == TextoABuscar1)
                        {
                            //row.Selected = true;
                            textoBuscarUnoEncontrado = true;
                            //return true;
                        }
                    }
                }
                foreach (DataGridViewRow row in grid.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value.ToString() == TextoABuscar2)
                        {
                            //row.Selected = true;
                            textoBuscarDosEncontrado = true;
                            //return true;
                        }
                    }
                }
                if (textoBuscarUnoEncontrado.Equals(true) &&
                    textoBuscarDosEncontrado.Equals(true))
                {
                    return true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (row.Cells[Columna1].Value.ToString() == TextoABuscar1)
                    {
                        //row.Selected = true;
                        textoBuscarUnoEncontrado = true;
                        //return true;
                    }
                    if (row.Cells[Columna2].Value.ToString() == TextoABuscar2)
                    {
                        //row.Selected = true;
                        textoBuscarDosEncontrado = true;
                        //return true;
                    }
                }
                if (textoBuscarUnoEncontrado.Equals(true) &&
                    textoBuscarDosEncontrado.Equals(true))
                {
                    return true;
                }
            }

            return encontrado;
        }

        public static void GenerarReporteVentas(string opcionVentas, DataTable tablaResult)
        {
            Consultas cs = new Consultas();

            // Ventas pagadas
            if (opcionVentas == "VP")
            {
                opcionVentas = "Ventas pagadas";
            }
            // Ventas guardadas
            if (opcionVentas == "VG")
            {
                opcionVentas = "Ventas guardadas";
            }
            // Ventas canceladas
            if (opcionVentas == "VC")
            {
                opcionVentas = "Ventas canceladas";
            }
            // Ventas a credito
            if (opcionVentas == "VCC")
            {
                opcionVentas = "Ventas a credito";
            }

            var mostrarClave = FormPrincipal.clave;

            //Datos del usuario
            var datos = FormPrincipal.datosUsuario;

            //Fuentes y Colores
            var colorFuenteNegrita = new BaseColor(Color.Black);
            var colorFuenteBlanca = new BaseColor(Color.White);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 8, 1, colorFuenteNegrita);

            var numRow = 0;
            string numAperturaCaja = string.Empty;

            //Ruta donde se creara el archivo PDF
            var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = string.Empty;
            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\ventas.pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\ventas.pdf";
            }

            var fechaHoy = DateTime.Now;
            //rutaArchivo = @"C:\Archivos PUDVE\Reportes\caja.pdf";

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(opcionVentas, fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            Paragraph Empleado = new Paragraph("");

            Paragraph NumeroFolio = new Paragraph("");

            //string UsuarioActivo = string.Empty;

            string tipoReporte = string.Empty,
            encabezadoTipoReporte = string.Empty;

            //Encabezado del reporte
            encabezadoTipoReporte = opcionVentas;


            var UsuarioActivo = cs.validarEmpleado(FormPrincipal.userNickName, true);
            var obtenerUsuarioPrincipal = cs.validarEmpleadoPorID();

            var sumarTotales = 0.00;

            //var numFolio = obtenerFolio(id);

            Usuario = new Paragraph($"USUARIO: ADMIN ({obtenerUsuarioPrincipal})", fuenteNegrita);
            if (!string.IsNullOrEmpty(UsuarioActivo))
            {
                Empleado = new Paragraph($"EMPLEADO: {UsuarioActivo}", fuenteNegrita);
            }

            //NumeroFolio = new Paragraph("No. Folio: " + numFolio, fuenteNormal);

            Paragraph subTitulo = new Paragraph($"REPORTE DE VENTAS\nSECCIÓN ELEGIDA " + encabezadoTipoReporte.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            if (!string.IsNullOrEmpty(UsuarioActivo))
            {
                Empleado.Alignment = Element.ALIGN_CENTER;
            }
            NumeroFolio.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;


            float[] anchoColumnas = new float[] { 20f, 60f, 40f, 60f, 40f, 60f, 40f, 60f };

            //Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //============================
            //=== TABLA DE INVENTARIO  ===
            //============================

            PdfPTable tablaInventario = new PdfPTable(8);
            tablaInventario.WidthPercentage = 100;
            tablaInventario.SetWidths(anchoColumnas);

            PdfPCell colNum = new PdfPCell(new Phrase("No:", fuenteNegrita));
            colNum.BorderWidth = 1;
            colNum.BackgroundColor = new BaseColor(Color.SkyBlue);
            colNum.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colCliente = new PdfPCell(new Phrase("CLIENTE", fuenteNegrita));
            colCliente.BorderWidth = 1;
            //colCliente.Colspan = 2;
            colCliente.BackgroundColor = new BaseColor(Color.SkyBlue);
            colCliente.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colRFC = new PdfPCell(new Phrase("RFC", fuenteTotales));
            colRFC.BorderWidth = 1;
            //colRFC.Colspan = 2;
            colRFC.HorizontalAlignment = Element.ALIGN_CENTER;
            colRFC.Padding = 3;
            colRFC.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colTotal = new PdfPCell(new Phrase("TOTAL", fuenteTotales));
            colTotal.BorderWidth = 1;
            //colTotal.Colspan = 2;
            colTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotal.Padding = 3;
            colTotal.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colFolio = new PdfPCell(new Phrase("FOLIO", fuenteTotales));
            colFolio.BorderWidth = 1;
            //colFolio.Colspan = 2;
            colFolio.HorizontalAlignment = Element.ALIGN_CENTER;
            colFolio.Padding = 3;
            colFolio.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colSerie = new PdfPCell(new Phrase("SERIE", fuenteTotales));
            colSerie.BorderWidth = 1;
            //colSerie.Colspan = 2;
            colSerie.HorizontalAlignment = Element.ALIGN_CENTER;
            colSerie.Padding = 3;
            colSerie.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colFecha = new PdfPCell(new Phrase("FECHA", fuenteTotales));
            colFecha.BorderWidth = 1;
            //colFecha.Colspan = 2;
            colFecha.HorizontalAlignment = Element.ALIGN_CENTER;
            colFecha.Padding = 3;
            colFecha.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colEmpleado = new PdfPCell(new Phrase("EMPLEADO", fuenteTotales));
            colEmpleado.BorderWidth = 1;
            //colEmpleado.Colspan = 2;
            colEmpleado.HorizontalAlignment = Element.ALIGN_CENTER;
            colEmpleado.Padding = 3;
            colEmpleado.BackgroundColor = new BaseColor(Color.SkyBlue);

            tablaInventario.AddCell(colNum);
            tablaInventario.AddCell(colCliente);
            tablaInventario.AddCell(colRFC);
            tablaInventario.AddCell(colTotal);
            tablaInventario.AddCell(colFolio);
            tablaInventario.AddCell(colSerie);
            tablaInventario.AddCell(colFecha);
            tablaInventario.AddCell(colEmpleado);

            var cliente = string.Empty;
            foreach (DataRow iterador in tablaResult.Rows)
            {

                numRow += 1;
                numAperturaCaja = "--";

                var nombreEmpleado = cs.BuscarEmpleadoCaja(Convert.ToInt32(iterador["IDEmpleado"].ToString()));
                if (string.IsNullOrEmpty(nombreEmpleado))
                {
                    nombreEmpleado = FormPrincipal.userNickName;
                }

                sumarTotales += (float)Convert.ToDecimal(iterador["Total"].ToString());

                PdfPCell colNumFilatemp = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                if (iterador[0].ToString() == "Apertura de Caja")
                {
                    colNumFilatemp = new PdfPCell(new Phrase(numAperturaCaja.ToString(), fuenteNormal));
                    colNumFilatemp.BorderWidth = 1;
                    //colClienteTemp.BorderWidthLeft = 0;
                    //colClienteTemp.BorderWidthTop = 0;
                    //colClienteTemp.BorderWidthBottom = 0;
                    //colClienteTemp.BorderWidthRight = 0;
                    colNumFilatemp.HorizontalAlignment = Element.ALIGN_CENTER;
                    numRow--;
                }
                else
                {
                    colNumFilatemp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                    colNumFilatemp.BorderWidth = 1;
                    //colClienteTemp.BorderWidthLeft = 0;
                    //colClienteTemp.BorderWidthTop = 0;
                    //colClienteTemp.BorderWidthBottom = 0;
                    //colClienteTemp.BorderWidthRight = 0;
                    colNumFilatemp.HorizontalAlignment = Element.ALIGN_CENTER;
                }


                PdfPCell colClienteTemp = new PdfPCell(new Phrase(iterador["Cliente"].ToString(), fuenteNormal));
                colClienteTemp.BorderWidth = 1;
                //colClienteTemp.BorderWidthLeft = 0;
                //colClienteTemp.BorderWidthTop = 0;
                //colClienteTemp.BorderWidthBottom = 0;
                //colClienteTemp.BorderWidthRight = 0;
                colClienteTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRFCTemp = new PdfPCell(new Phrase(iterador["RFC"].ToString(), fuenteNormal));
                colRFCTemp.BorderWidth = 1;
                //colRFCTemp.BorderWidthRight = 0;
                //colRFCTemp.BorderWidthTop = 0;
                //colRFCTemp.BorderWidthBottom = 0;
                //colRFCTemp.BorderWidthLeft = 0;
                colRFCTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colTotalTemp = new PdfPCell(new Phrase("$" + iterador["Total"].ToString(), fuenteNormal));
                colTotalTemp.BorderWidth = 1;
                //colTotalTemp.BorderWidthRight = 0;
                //colTotalTemp.BorderWidthTop = 0;
                //colTotalTemp.BorderWidthBottom = 0;
                //colTotalTemp.BorderWidthLeft = 0;
                colTotalTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFolioTemp = new PdfPCell(new Phrase(iterador["Folio"].ToString(), fuenteNormal));
                colFolioTemp.BorderWidth = 1;
                //colFolioTemp.BorderWidthLeft = 0;
                //colFolioTemp.BorderWidthTop = 0;
                //colFolioTemp.BorderWidthBottom = 0;
                //colFolioTemp.BorderWidthRight = 0;
                colFolioTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colSerieTemp = new PdfPCell(new Phrase(iterador["Serie"].ToString(), fuenteNormal));
                colSerieTemp.BorderWidth = 1;
                //colSerieTemp.BorderWidthRight = 0;
                //colSerieTemp.BorderWidthTop = 0;
                //colSerieTemp.BorderWidthBottom = 0;
                //colSerieTemp.BorderWidthLeft = 0;
                colSerieTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTemp = new PdfPCell(new Phrase(iterador["FechaOperacion"].ToString(), fuenteNormal));
                colFechaTemp.BorderWidth = 1;
                //colFechaTemp.BorderWidthLeft = 0;
                //colFechaTemp.BorderWidthTop = 0;
                //colFechaTemp.BorderWidthBottom = 0;
                //colFechaTemp.BorderWidthRight = 0;
                colFechaTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colEmpleadoTemp = new PdfPCell(new Phrase(nombreEmpleado, fuenteNormal));
                colEmpleadoTemp.BorderWidth = 1;
                //colEmpleadoTemp.BorderWidthLeft = 0;
                //colEmpleadoTemp.BorderWidthTop = 0;
                //colEmpleadoTemp.BorderWidthBottom = 0;
                //colEmpleadoTemp.BorderWidthRight = 0;
                colEmpleadoTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNumFilatemp);
                tablaInventario.AddCell(colClienteTemp);
                tablaInventario.AddCell(colRFCTemp);
                tablaInventario.AddCell(colTotalTemp);
                tablaInventario.AddCell(colFolioTemp);
                tablaInventario.AddCell(colSerieTemp);
                tablaInventario.AddCell(colFechaTemp);
                tablaInventario.AddCell(colEmpleadoTemp);

            }

            //Columna para total de dinero
            PdfPCell colNumFilatempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colNumFilatempTotal.BorderWidth = 0;
            //colClienteTemp.BorderWidthLeft = 0;
            //colClienteTemp.BorderWidthTop = 0;
            //colClienteTemp.BorderWidthBottom = 0;
            //colClienteTemp.BorderWidthRight = 0;
            colNumFilatempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colClienteTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colClienteTempTotal.BorderWidth = 0;
            //colClienteTemp.BorderWidthLeft = 0;
            //colClienteTemp.BorderWidthTop = 0;
            //colClienteTemp.BorderWidthBottom = 0;
            //colClienteTemp.BorderWidthRight = 0;
            colClienteTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colRFCTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colRFCTempTotal.BorderWidth = 0;
            //colRFCTemp.BorderWidthRight = 0;
            //colRFCTemp.BorderWidthTop = 0;
            //colRFCTemp.BorderWidthBottom = 0;
            //colRFCTemp.BorderWidthLeft = 0;
            colRFCTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colTotalTempTotal = new PdfPCell(new Phrase("$" + sumarTotales.ToString("0.00"), fuenteNormal));
            colTotalTempTotal.BorderWidth = 0;
            //colTotalTempTotal.BorderWidthRight = 0;
            //colTotalTempTotal.BorderWidthTop = 0;
            colTotalTempTotal.BorderWidthBottom = 1;
            //colTotalTempTotal.BorderWidthLeft = 0;
            colTotalTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalTempTotal.BackgroundColor = new BaseColor(Color.SkyBlue);


            PdfPCell colFolioTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colFolioTempTotal.BorderWidth = 0;
            //colFolioTemp.BorderWidthLeft = 0;
            //colFolioTemp.BorderWidthTop = 0;
            //colFolioTemp.BorderWidthBottom = 0;
            //colFolioTemp.BorderWidthRight = 0;
            colFolioTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colSerieTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colSerieTempTotal.BorderWidth = 0;
            //colSerieTemp.BorderWidthRight = 0;
            //colSerieTemp.BorderWidthTop = 0;
            //colSerieTemp.BorderWidthBottom = 0;
            //colSerieTemp.BorderWidthLeft = 0;
            colSerieTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colFechaTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colFechaTempTotal.BorderWidth = 0;
            //colFechaTemp.BorderWidthLeft = 0;
            //colFechaTemp.BorderWidthTop = 0;
            //colFechaTemp.BorderWidthBottom = 0;
            //colFechaTemp.BorderWidthRight = 0;
            colFechaTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colEmpleadoTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colEmpleadoTempTotal.BorderWidth = 0;
            //colEmpleadoTemp.BorderWidthLeft = 0;
            //colEmpleadoTemp.BorderWidthTop = 0;
            //colEmpleadoTemp.BorderWidthBottom = 0;
            //colEmpleadoTemp.BorderWidthRight = 0;
            colEmpleadoTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            tablaInventario.AddCell(colNumFilatempTotal);
            tablaInventario.AddCell(colClienteTempTotal);
            tablaInventario.AddCell(colRFCTempTotal);
            tablaInventario.AddCell(colTotalTempTotal);
            tablaInventario.AddCell(colFolioTempTotal);
            tablaInventario.AddCell(colSerieTempTotal);
            tablaInventario.AddCell(colFechaTempTotal);
            tablaInventario.AddCell(colEmpleadoTempTotal);

            reporte.Add(titulo);
            reporte.Add(Usuario);
            if (!string.IsNullOrEmpty(UsuarioActivo))
            {
                reporte.Add(Empleado);
            }
            reporte.Add(NumeroFolio);
            reporte.Add(subTitulo);
            reporte.Add(tablaInventario);

            //================================
            //=== FIN TABLA DE INVENTARIO ===
            //================================

            reporte.AddTitle("Reporte Caja");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.Show();
        }

        public static void GenerarReporteFacturas(string opcionVentas, DataTable tablaResult)
        {
            Consultas cs = new Consultas();

            //// Ventas pagadas
            //if (opcionVentas == "VP") { opcionVentas = "Ventas pagadas"; }
            //// Ventas guardadas
            //if (opcionVentas == "VG") { opcionVentas = "Ventas guardadas"; }
            //// Ventas canceladas
            //if (opcionVentas == "VC") { opcionVentas = "Ventas canceladas"; }
            //// Ventas a credito
            //if (opcionVentas == "VCC") { opcionVentas = "Ventas a credito"; }

            var mostrarClave = FormPrincipal.clave;

            //Datos del usuario
            var datos = FormPrincipal.datosUsuario;

            //Fuentes y Colores
            var colorFuenteNegrita = new BaseColor(Color.Black);
            var colorFuenteBlanca = new BaseColor(Color.White);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 8, 1, colorFuenteNegrita);

            var numRow = 0;
            var totalCantidad = 0.00;

            //Ruta donde se creara el archivo PDF
            var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = string.Empty;
            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\facturas.pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\facturas.pdf";
            }

            var fechaHoy = DateTime.Now;
            //rutaArchivo = @"C:\Archivos PUDVE\Reportes\caja.pdf";

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(opcionVentas, fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            Paragraph Empleado = new Paragraph("");

            Paragraph NumeroFolio = new Paragraph("");

            //string UsuarioActivo = string.Empty;

            string tipoReporte = string.Empty,
                    encabezadoTipoReporte = string.Empty;

            //Encabezado del reporte
            encabezadoTipoReporte = opcionVentas;


            var UsuarioActivo = cs.validarEmpleado(FormPrincipal.userNickName, true);
            var obtenerUsuarioPrincipal = cs.validarEmpleadoPorID();

            //var numFolio = obtenerFolio(id);

            Usuario = new Paragraph($"USUARIO: ADMIN ({obtenerUsuarioPrincipal})", fuenteNegrita);
            if (!string.IsNullOrEmpty(UsuarioActivo))
            {
                Empleado = new Paragraph($"EMPLEADO: {UsuarioActivo}", fuenteNegrita);
            }

            //NumeroFolio = new Paragraph("No. Folio: " + numFolio, fuenteNormal);

            Paragraph subTitulo = new Paragraph($"REPORTE DE Facturas\nSECCIÓN ELEGIDA " + encabezadoTipoReporte.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            if (!string.IsNullOrEmpty(UsuarioActivo)) { Empleado.Alignment = Element.ALIGN_CENTER; }
            NumeroFolio.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;


            float[] anchoColumnas = new float[] { 20f, 30f, 20f, 40f, 130f, 40f, 40f, 60f };

            //Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //============================
            //=== TABLA DE INVENTARIO  ===
            //============================

            PdfPTable tablaInventario = new PdfPTable(8);
            tablaInventario.WidthPercentage = 100;
            tablaInventario.SetWidths(anchoColumnas);

            PdfPCell colNum = new PdfPCell(new Phrase("No:", fuenteNegrita));
            colNum.BorderWidth = 1;
            colNum.BackgroundColor = new BaseColor(Color.SkyBlue);
            colNum.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colFolio = new PdfPCell(new Phrase("FOLIO", fuenteNegrita));
            colFolio.BorderWidth = 1;
            //colCliente.Colspan = 2;
            colFolio.BackgroundColor = new BaseColor(Color.SkyBlue);
            colFolio.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colSerie = new PdfPCell(new Phrase("SERIE", fuenteTotales));
            colSerie.BorderWidth = 1;
            //colRFC.Colspan = 2;
            colSerie.HorizontalAlignment = Element.ALIGN_CENTER;
            colSerie.Padding = 3;
            colSerie.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colRfc = new PdfPCell(new Phrase("RFC", fuenteTotales));
            colRfc.BorderWidth = 1;
            //colTotal.Colspan = 2;
            colRfc.HorizontalAlignment = Element.ALIGN_CENTER;
            colRfc.Padding = 3;
            colRfc.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colRazonSocial = new PdfPCell(new Phrase("RAZON SOCIAL", fuenteTotales));
            colRazonSocial.BorderWidth = 1;
            //colFolio.Colspan = 2;
            colRazonSocial.HorizontalAlignment = Element.ALIGN_CENTER;
            colRazonSocial.Padding = 3;
            colRazonSocial.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colTotal = new PdfPCell(new Phrase("TOTAL", fuenteTotales));
            colTotal.BorderWidth = 1;
            //colSerie.Colspan = 2;
            colTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotal.Padding = 3;
            colTotal.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colFecha = new PdfPCell(new Phrase("FECHA", fuenteTotales));
            colFecha.BorderWidth = 1;
            //colFecha.Colspan = 2;
            colFecha.HorizontalAlignment = Element.ALIGN_CENTER;
            colFecha.Padding = 3;
            colFecha.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colEmpleado = new PdfPCell(new Phrase("EMPLEADO", fuenteTotales));
            colEmpleado.BorderWidth = 1;
            //colEmpleado.Colspan = 2;
            colEmpleado.HorizontalAlignment = Element.ALIGN_CENTER;
            colEmpleado.Padding = 3;
            colEmpleado.BackgroundColor = new BaseColor(Color.SkyBlue);

            tablaInventario.AddCell(colNum);
            tablaInventario.AddCell(colFolio);
            tablaInventario.AddCell(colSerie);
            tablaInventario.AddCell(colRfc);
            tablaInventario.AddCell(colRazonSocial);
            tablaInventario.AddCell(colTotal);
            tablaInventario.AddCell(colFecha);
            tablaInventario.AddCell(colEmpleado);

            var cliente = string.Empty;
            foreach (DataRow iterador in tablaResult.Rows)
            {
                var nombreEmpleado = cs.BuscarEmpleadoCaja(Convert.ToInt32(iterador["id_empleado"].ToString()));
                if (string.IsNullOrEmpty(nombreEmpleado)) { nombreEmpleado = FormPrincipal.userNickName; }
                numRow += 1;
                totalCantidad += float.Parse(iterador["total"].ToString());

                PdfPCell colNumFilatemp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                colNumFilatemp.BorderWidth = 1;
                //colClienteTemp.BorderWidthLeft = 0;
                //colClienteTemp.BorderWidthTop = 0;
                //colClienteTemp.BorderWidthBottom = 0;
                //colClienteTemp.BorderWidthRight = 0;
                colNumFilatemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colClienteTemp = new PdfPCell(new Phrase(iterador["folio"].ToString(), fuenteNormal));
                colClienteTemp.BorderWidth = 1;
                //colClienteTemp.BorderWidthLeft = 0;
                //colClienteTemp.BorderWidthTop = 0;
                //colClienteTemp.BorderWidthBottom = 0;
                //colClienteTemp.BorderWidthRight = 0;
                colClienteTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRFCTemp = new PdfPCell(new Phrase(iterador["serie"].ToString(), fuenteNormal));
                colRFCTemp.BorderWidth = 1;
                //colRFCTemp.BorderWidthRight = 0;
                //colRFCTemp.BorderWidthTop = 0;
                //colRFCTemp.BorderWidthBottom = 0;
                //colRFCTemp.BorderWidthLeft = 0;
                colRFCTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colTotalTemp = new PdfPCell(new Phrase(iterador["r_rfc"].ToString(), fuenteNormal));
                colTotalTemp.BorderWidth = 1;
                //colTotalTemp.BorderWidthRight = 0;
                //colTotalTemp.BorderWidthTop = 0;
                //colTotalTemp.BorderWidthBottom = 0;
                //colTotalTemp.BorderWidthLeft = 0;
                colTotalTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFolioTemp = new PdfPCell(new Phrase(iterador["r_razon_social"].ToString(), fuenteNormal));
                colFolioTemp.BorderWidth = 1;
                //colFolioTemp.BorderWidthLeft = 0;
                //colFolioTemp.BorderWidthTop = 0;
                //colFolioTemp.BorderWidthBottom = 0;
                //colFolioTemp.BorderWidthRight = 0;
                colFolioTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colSerieTemp = new PdfPCell(new Phrase("$" + iterador["total"].ToString(), fuenteNormal));
                colSerieTemp.BorderWidth = 1;
                //colSerieTemp.BorderWidthRight = 0;
                //colSerieTemp.BorderWidthTop = 0;
                //colSerieTemp.BorderWidthBottom = 0;
                //colSerieTemp.BorderWidthLeft = 0;
                colSerieTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTemp = new PdfPCell(new Phrase(iterador["fecha_certificacion"].ToString(), fuenteNormal));
                colFechaTemp.BorderWidth = 1;
                //colFechaTemp.BorderWidthLeft = 0;
                //colFechaTemp.BorderWidthTop = 0;
                //colFechaTemp.BorderWidthBottom = 0;
                //colFechaTemp.BorderWidthRight = 0;
                colFechaTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colEmpleadoTemp = new PdfPCell(new Phrase(nombreEmpleado, fuenteNormal));
                colEmpleadoTemp.BorderWidth = 1;
                //colEmpleadoTemp.BorderWidthLeft = 0;
                //colEmpleadoTemp.BorderWidthTop = 0;
                //colEmpleadoTemp.BorderWidthBottom = 0;
                //colEmpleadoTemp.BorderWidthRight = 0;
                colEmpleadoTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                //tablaInventario.AddCell(colNoConceptoTmp);
                tablaInventario.AddCell(colNumFilatemp);
                tablaInventario.AddCell(colClienteTemp);
                tablaInventario.AddCell(colRFCTemp);
                tablaInventario.AddCell(colTotalTemp);
                tablaInventario.AddCell(colFolioTemp);
                tablaInventario.AddCell(colSerieTemp);
                tablaInventario.AddCell(colFechaTemp);
                tablaInventario.AddCell(colEmpleadoTemp);
            }

            PdfPCell colNumTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colNumTempTotal.BorderWidth = 0;
            //colClienteTemp.BorderWidthLeft = 0;
            //colClienteTemp.BorderWidthTop = 0;
            //colClienteTemp.BorderWidthBottom = 0;
            //colClienteTemp.BorderWidthRight = 0;
            colNumTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colClienteTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colClienteTempTotal.BorderWidth = 0;
            //colClienteTemp.BorderWidthLeft = 0;
            //colClienteTemp.BorderWidthTop = 0;
            //colClienteTemp.BorderWidthBottom = 0;
            //colClienteTemp.BorderWidthRight = 0;
            colClienteTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colRFCTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colRFCTempTotal.BorderWidth = 0;
            //colRFCTemp.BorderWidthRight = 0;
            //colRFCTemp.BorderWidthTop = 0;
            //colRFCTemp.BorderWidthBottom = 0;
            //colRFCTemp.BorderWidthLeft = 0;
            colRFCTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colTotalTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colTotalTempTotal.BorderWidth = 0;
            //colTotalTemp.BorderWidthRight = 0;
            //colTotalTemp.BorderWidthTop = 0;
            //colTotalTemp.BorderWidthBottom = 0;
            //colTotalTemp.BorderWidthLeft = 0;
            colTotalTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colFolioTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colFolioTempTotal.BorderWidth = 0;
            //colFolioTemp.BorderWidthLeft = 0;
            //colFolioTemp.BorderWidthTop = 0;
            //colFolioTemp.BorderWidthBottom = 0;
            //colFolioTemp.BorderWidthRight = 0;
            colFolioTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colSerieTempTotal = new PdfPCell(new Phrase("$" + totalCantidad.ToString("0.00"), fuenteNormal));
            colSerieTempTotal.BorderWidth = 0;
            //colSerieTemp.BorderWidthRight = 0;
            //colSerieTemp.BorderWidthTop = 0;
            colSerieTempTotal.BorderWidthBottom = 1;
            //colSerieTemp.BorderWidthLeft = 0;
            colSerieTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colSerieTempTotal.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colFechaTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colFechaTempTotal.BorderWidth = 0;
            //colFechaTemp.BorderWidthLeft = 0;
            //colFechaTemp.BorderWidthTop = 0;
            //colFechaTemp.BorderWidthBottom = 0;
            //colFechaTemp.BorderWidthRight = 0;
            colFechaTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colEmpleadoTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colEmpleadoTempTotal.BorderWidth = 0;
            //colEmpleadoTemp.BorderWidthLeft = 0;
            //colEmpleadoTemp.BorderWidthTop = 0;
            //colEmpleadoTemp.BorderWidthBottom = 0;
            //colEmpleadoTemp.BorderWidthRight = 0;
            colEmpleadoTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            //tablaInventario.AddCell(colNoConceptoTmp);
            tablaInventario.AddCell(colNumTempTotal);
            tablaInventario.AddCell(colClienteTempTotal);
            tablaInventario.AddCell(colRFCTempTotal);
            tablaInventario.AddCell(colTotalTempTotal);
            tablaInventario.AddCell(colFolioTempTotal);
            tablaInventario.AddCell(colSerieTempTotal);
            tablaInventario.AddCell(colFechaTempTotal);
            tablaInventario.AddCell(colEmpleadoTempTotal);

            reporte.Add(titulo);
            reporte.Add(Usuario);
            if (!string.IsNullOrEmpty(UsuarioActivo)) { reporte.Add(Empleado); }
            reporte.Add(NumeroFolio);
            reporte.Add(subTitulo);
            reporte.Add(tablaInventario);

            //================================
            //=== FIN TABLA DE INVENTARIO ===
            //================================

            reporte.AddTitle("Reporte Caja");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.Show();
        }

        /// <summary>
        /// Método para justificar texto de un label
        /// </summary>
        /// <param name="text">Parametro para pasar el texto que se va justificar</param>
        /// <param name="font">Parametro para pasar el font que se va utilizar</param>
        /// <param name="ControlWidth">Parametro para pasar la medida de lo ancho que se va justificar</param>
        /// <returns>Retorna un String para usarse en el Label</returns>
        public static string JustifyParagraph(string text, System.Drawing.Font font, int ControlWidth)
        {
            string result = string.Empty;
            List<string> ParagraphsList = new List<string>();

            ParagraphsList.AddRange(text.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList());

            foreach (string Paragraph in ParagraphsList)
            {
                string line = string.Empty;
                int ParagraphWidth = TextRenderer.MeasureText(Paragraph, font).Width;
                if (ParagraphWidth > ControlWidth)
                {
                    //Get all paragraph words, add a normal space and calculate when their sum exceeds the constraints 
                    string[] Words = Paragraph.Split(' ');
                    line = Words[0] + (char)32;
                    for (int x = 1; x < Words.Length; x++)
                    {
                        string tmpLine = line + (Words[x] + (char)32);
                        if (TextRenderer.MeasureText(tmpLine, font).Width > ControlWidth)
                        {
                            //Max lenght reached. Justify the line and step back 
                            result += Justify(line.TrimEnd(), font, ControlWidth) + "\r\n";
                            line = string.Empty; --x;
                        }
                        else
                        {
                            //Some capacity still left 
                            line += (Words[x] + (char)32);
                        }
                    }
                    //Adds the remainder if any 
                    if (line.Length > 0)
                    {
                        result += line + "\r\n";
                    }
                }
                else
                {
                    result += Paragraph + "\r\n";
                }
            }

            return result.TrimEnd(new[] { '\r', '\n' });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private static string Justify(string text, System.Drawing.Font font, int width)
        {
            char SpaceChar = (char)0x200A;
            List<string> WordsList = text.Split((char)32).ToList();

            if (WordsList.Capacity < 2)
            {
                return text;
            }

            int NumberOfWords = WordsList.Capacity - 1;
            int WordsWidth = TextRenderer.MeasureText(text.Replace(" ", ""), font).Width;
            int SpaceCharWidth = TextRenderer.MeasureText(WordsList[0] + SpaceChar, font).Width - TextRenderer.MeasureText(WordsList[0], font).Width;

            //Calculate the average spacing between each word minus the last one 
            int AverageSpace = ((width - WordsWidth) / NumberOfWords) / SpaceCharWidth;
            float AdjustSpace = (width - (WordsWidth + (AverageSpace * NumberOfWords * SpaceCharWidth)));

            //Add spaces to all words
            return ((Func<string>)(() =>
            {
                string Spaces = "";
                string AdjustedWords = "";

                for (int h = 0; h < AverageSpace; h++)
                {
                    Spaces += SpaceChar;
                }

                foreach (string Word in WordsList)
                {
                    AdjustedWords += Word + Spaces;
                    //Adjust the spacing if there's a reminder
                    if (AdjustSpace > 0)
                    {
                        AdjustedWords += SpaceChar;
                        AdjustSpace -= SpaceCharWidth;
                    }
                }
                return AdjustedWords.TrimEnd();
            }))();
        }

        public static DateTime UpdateDateTime(/*bool convertToLocalTime*/)
        {
            DateTime dateTime = DateTime.MinValue;
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://www.microsoft.com");
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string todaysDates = response.Headers["date"];

                dateTime = DateTime.ParseExact(todaysDates, "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                    System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat, System.Globalization.DateTimeStyles.AssumeUniversal);
            }

            return dateTime;
            //Random ran = new Random(DateTime.Now.Millisecond);
            //DateTime date = DateTime.Today;
            //string serverResponse = string.Empty;

            //// Represents the list of NIST servers
            ////string[] servers = new string[] {
            ////             "64.90.182.55",
            ////             "206.246.118.250",
            ////             "207.200.81.113",
            ////             "128.138.188.172",
            ////             "64.113.32.5",
            ////             "64.147.116.229",
            ////             "64.125.78.85",
            ////             "128.138.188.172"
            ////              };
            //string[] server = new string[] { "142.251.34.35" };

            //// Try each server in random order to avoid blocked requests due to too frequent request
            //for (int i = 0; i < server.Count(); i++)
            //{
            //    try
            //    {
            //        // Open a StreamReader to a random time server
            //        StreamReader reader = new StreamReader(new System.Net.Sockets.TcpClient(server[ran.Next(0, server.Length)], 13).GetStream());
            //        serverResponse = reader.ReadToEnd();
            //        reader.Close();

            //        // Check to see that the signiture is there
            //        if (serverResponse.Length > 47 && serverResponse.Substring(38, 9).Equals("UTC(NIST)"))
            //        {
            //            // Parse the date
            //            int jd = int.Parse(serverResponse.Substring(1, 5));
            //            int yr = int.Parse(serverResponse.Substring(7, 2));
            //            int mo = int.Parse(serverResponse.Substring(10, 2));
            //            int dy = int.Parse(serverResponse.Substring(13, 2));
            //            int hr = int.Parse(serverResponse.Substring(16, 2));
            //            int mm = int.Parse(serverResponse.Substring(19, 2));
            //            int sc = int.Parse(serverResponse.Substring(22, 2));

            //            if (jd > 51544)
            //                yr += 2000;
            //            else
            //                yr += 1999;

            //            date = new DateTime(yr, mo, dy, hr, mm, sc);

            //            // Convert it to the current timezone if desired
            //            if (convertToLocalTime)
            //                date = date.ToLocalTime();

            //            // Exit the loop
            //            break;
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        /* Do Nothing...try the next server */
            //    }
            //}

            //return date;
        }

        public static void registrarNuevoEmpleadoPermisosConfiguracion(int id_empleado)
        {
            Conexion cn = new Conexion();
            Consultas cs = new Consultas();

            using (var datoEmpleado = cn.CargarDatos(cs.PermisosConfiguracionEmpleados(id_empleado)))
            {
                if (!datoEmpleado.Rows.Count.Equals(0))
                {//Si ya hay registro del empleado no se hace nada
                }
                else
                {//Si no se encuentra registrado se hace un INSERT
                    if (id_empleado != 0)
                    {
                        cn.EjecutarConsulta($"INSERT INTO permisosconfiguracion (IDEmpleado, IDUsuario) VALUES ({id_empleado},{FormPrincipal.userID})");
                    }

                }
            }
        }

        public static void registrarEmpleadosAntiguosPermisosConfiguracion()
        {
            Conexion cn = new Conexion();
            Consultas cs = new Consultas();
            var datos = cn.CargarDatos($"SELECT ID FROM Empleados WHERE IDUsuario = {FormPrincipal.userID} ORDER BY ID");

            foreach (DataRow item in datos.Rows)
            {
                try
                {
                    cn.EjecutarConsulta($"INSERT INTO permisosconfiguracion (IDEmpleado, IDUsuario) VALUES ({item["ID"].ToString()},{FormPrincipal.userID})");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }
            }

        }

        public static void remplazarComillasSimplesEnLaTablaProductos()
        {
            try
            {
                Conexion cn = new Conexion();
                Consultas cs = new Consultas();
                cn.EjecutarConsulta(cs.quitarComillasSimplesDeProductos());
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("error " + ex.Message.ToString(), "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void CrearMarcaDeAguaNota(int idVenta, string texto)
        {
            Conexion cn = new Conexion();
            Consultas cs = new Consultas();

            var servidor = Properties.Settings.Default.Hosting;
            var Usuario = FormPrincipal.userNickName;
            var archivoCopia = string.Empty;
            var archivoPDF = string.Empty;
            var nuevoPDF = string.Empty;
            var numFolio = string.Empty;
            var numSerie = string.Empty;

            using (DataTable dtDatosVentas = cn.CargarDatos(cs.DatosVentaParaLaNota(idVenta)))
            {
                if (!dtDatosVentas.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in dtDatosVentas.Rows)
                    {
                        numFolio = item["Folio"].ToString();
                        numSerie = item["Serie"].ToString();
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                archivoCopia = $@"\\{servidor}\Archivos PUDVE\Ventas\PDF\{Usuario}\VENTA_NoVenta{idVenta}_Folio{numFolio}{numSerie}_tmp.pdf";
                archivoPDF = $@"\\{servidor}\Archivos PUDVE\Ventas\PDF\{Usuario}\VENTA_NoVenta{idVenta}_Folio{numFolio}{numSerie}.pdf";
            }
            else
            {
                archivoCopia = $@"C:\Archivos PUDVE\Ventas\PDF\{Usuario}\VENTA_NoVenta{idVenta}_Folio{numFolio}{numSerie}_tmp.pdf";
                archivoPDF = $@"C:\Archivos PUDVE\Ventas\PDF\{Usuario}\VENTA_NoVenta{idVenta}_Folio{numFolio}{numSerie}.pdf";
            }

            if (!File.Exists(archivoPDF))
            {
                verFactura(idVenta);
            }

            nuevoPDF = archivoPDF;

            // Renombramos el archivo PDF
            File.Move(archivoPDF, archivoCopia);

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
                        cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 80);

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
        }

        public static void verFactura(int idVenta)
        {
            Conexion cn = new Conexion();
            Consultas cs = new Consultas();

            decimal sumaImporteConcepto = 0;
            decimal sumaDescuento = 0;

            List<string> listPorProductoImpuestoTrasladados = new List<string>();

            // Consulta Tabla Venta
            DataTable dVenta = cn.CargarDatos(cs.consulta_dventa(1, idVenta));
            DataRow rVenta = dVenta.Rows[0];

            int idUsuario = Convert.ToInt32(rVenta["IDUsuario"]);
            string folio = rVenta["Folio"].ToString();
            string serie = rVenta["Serie"].ToString();
            DateTime fecha = Convert.ToDateTime(rVenta["FechaOperacion"]);
            decimal anticipo = Convert.ToDecimal(rVenta["Anticipo"]);

            // Consulta Tabla DetallesVenta
            DataTable dDetallesVenta = cn.CargarDatos(cs.consulta_dventa(2, idVenta));

            int idCliente = 0;
            string formaPago = "";

            if (rVenta["Status"].ToString().Equals("2"))
            {
                formaPago += "Presupuesto";
            }

            if (!dDetallesVenta.Rows.Count.Equals(0))
            {
                DataRow rDetallesVenta = dDetallesVenta.Rows[0];

                idCliente = Convert.ToInt32(rDetallesVenta["IDCliente"]);

                if (Convert.ToDecimal(rDetallesVenta["Efectivo"]) > 0)
                {
                    formaPago += "Efectivo";
                }
                if (Convert.ToDecimal(rDetallesVenta["Tarjeta"]) > 0)
                {
                    if (!string.IsNullOrWhiteSpace(formaPago))
                    {
                        formaPago += "/";
                    }
                    formaPago += "Tarjeta";
                }
                if (Convert.ToDecimal(rDetallesVenta["Vales"]) > 0)
                {
                    if (!string.IsNullOrWhiteSpace(formaPago))
                    {
                        formaPago += "/";
                    }
                    formaPago += "Vales";
                }
                if (Convert.ToDecimal(rDetallesVenta["Cheque"]) > 0)
                {
                    if (!string.IsNullOrWhiteSpace(formaPago))
                    {
                        formaPago += "/";
                    }
                    formaPago += "Cheque";
                }
                if (Convert.ToDecimal(rDetallesVenta["Transferencia"]) > 0)
                {
                    if (!string.IsNullOrWhiteSpace(formaPago))
                    {
                        formaPago += "/";
                    }
                    formaPago += "Transferencia";
                }
                if (Convert.ToDecimal(rDetallesVenta["Credito"]) > 0)
                {
                    if (!string.IsNullOrWhiteSpace(formaPago))
                    {
                        formaPago += "/";
                    }
                    formaPago += "Crédito";
                }
            }
            else
            {
                idCliente = Convert.ToInt32(cn.EjecutarSelect($"SELECT IDCliente FROM Ventas WHERE ID='{idVenta}'", 6));
            }

            ComprobanteVenta comprobanteVenta = new ComprobanteVenta();

            // Datos del usuario
            DataTable dUsuario = cn.CargarDatos(cs.cargar_datos_venta_xml(2, 0, idUsuario));
            DataRow rUsuario = dUsuario.Rows[0];

            string lugarExpedicion = rUsuario["Estado"].ToString();

            ComprobanteEmisorVenta emisorV = new ComprobanteEmisorVenta();

            emisorV.Nombre = rUsuario["RazonSocial"].ToString();
            emisorV.Rfc = rUsuario["RFC"].ToString();
            emisorV.RegimenFiscal = rUsuario["Regimen"].ToString();
            emisorV.Correo = rUsuario["Email"].ToString();
            emisorV.Telefono = rUsuario["Telefono"].ToString();

            // Obtiene el nombre comercial del emisor
            if (!string.IsNullOrWhiteSpace(rUsuario["nombre_comercial"].ToString()) & !string.IsNullOrWhiteSpace(rUsuario["nombre_comercial"].ToString()))
            {
                emisorV.NombreComercialEmisor = rUsuario["nombre_comercial"].ToString();
            }

            string domicilioEmisor = string.Empty;

            if (!string.IsNullOrWhiteSpace(rUsuario["Calle"].ToString()))
            {
                domicilioEmisor = rUsuario["Calle"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(rUsuario["NoExterior"].ToString()))
            {
                if (!string.IsNullOrWhiteSpace(domicilioEmisor))
                {
                    domicilioEmisor += ", ";
                }
                domicilioEmisor += rUsuario["NoExterior"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(rUsuario["NoInterior"].ToString()))
            {
                if (!string.IsNullOrWhiteSpace(domicilioEmisor))
                {
                    domicilioEmisor += ", ";
                }
                domicilioEmisor += rUsuario["NoInterior"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(rUsuario["Colonia"].ToString()))
            {
                if (!string.IsNullOrWhiteSpace(domicilioEmisor))
                {
                    domicilioEmisor += ", ";
                }
                domicilioEmisor += rUsuario["Colonia"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(rUsuario["CodigoPostal"].ToString()))
            {
                if (!string.IsNullOrWhiteSpace(domicilioEmisor))
                {
                    domicilioEmisor += ", ";
                }
                domicilioEmisor += rUsuario["CodigoPostal"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(rUsuario["Municipio"].ToString()))
            {
                if (!string.IsNullOrWhiteSpace(domicilioEmisor))
                {
                    domicilioEmisor += ", ";
                }
                domicilioEmisor += rUsuario["Municipio"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(rUsuario["Estado"].ToString()))
            {
                if (!string.IsNullOrWhiteSpace(domicilioEmisor))
                {
                    domicilioEmisor += ", ";
                }
                domicilioEmisor += rUsuario["Estado"].ToString();
            }

            if (!string.IsNullOrWhiteSpace(domicilioEmisor))
            {
                emisorV.DomicilioEmisor = domicilioEmisor;
            }

            comprobanteVenta.Emisor = emisorV;

            // Datos del Cliente
            DataTable dCliente = cn.CargarDatos(cs.cargar_datos_venta_xml(3, idCliente, 0));

            if (!dCliente.Rows.Count.Equals(0))
            {
                DataRow rCliente = dCliente.Rows[0];

                ComprobanteReceptorVenta receptorV = new ComprobanteReceptorVenta();

                receptorV.Nombre = rCliente["RazonSocial"].ToString();
                receptorV.Rfc = rCliente["RFC"].ToString();
                receptorV.Correo = rCliente["Email"].ToString();
                receptorV.Telefono = rCliente["Telefono"].ToString();

                string domicilioReceptor = string.Empty;

                if (!string.IsNullOrWhiteSpace(rCliente["Calle"].ToString()))
                {
                    domicilioReceptor = rCliente["Calle"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(rCliente["NoExterior"].ToString()))
                {
                    if (!string.IsNullOrWhiteSpace(domicilioReceptor))
                    {
                        domicilioReceptor += ", ";
                    }
                    domicilioReceptor += rCliente["NoExterior"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(rCliente["NoInterior"].ToString()))
                {
                    if (!string.IsNullOrWhiteSpace(domicilioReceptor))
                    {
                        domicilioReceptor += ", ";
                    }
                    domicilioReceptor += rCliente["NoInterior"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(rCliente["Colonia"].ToString()))
                {
                    if (!string.IsNullOrWhiteSpace(domicilioReceptor))
                    {
                        domicilioReceptor += ", ";
                    }
                    domicilioReceptor += rCliente["Colonia"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(rCliente["CodigoPostal"].ToString()))
                {
                    if (!string.IsNullOrWhiteSpace(domicilioReceptor))
                    {
                        domicilioReceptor += ", ";
                    }
                    domicilioReceptor += rCliente["CodigoPostal"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(rCliente["Localidad"].ToString()))
                {
                    if (!string.IsNullOrWhiteSpace(domicilioReceptor))
                    {
                        domicilioReceptor += ", ";
                    }
                    domicilioReceptor += rCliente["Localidad"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(rCliente["Municipio"].ToString()))
                {
                    if (!string.IsNullOrWhiteSpace(domicilioReceptor))
                    {
                        domicilioReceptor += ", ";
                    }
                    domicilioReceptor += rCliente["Municipio"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(rCliente["Estado"].ToString()))
                {
                    if (!string.IsNullOrWhiteSpace(domicilioReceptor))
                    {
                        domicilioReceptor += ", ";
                    }
                    domicilioReceptor += rCliente["Estado"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(rCliente["Pais"].ToString()))
                {
                    if (!string.IsNullOrWhiteSpace(domicilioReceptor))
                    {
                        domicilioReceptor += ", ";
                    }
                    domicilioReceptor += rCliente["Pais"].ToString();
                }

                if (!string.IsNullOrWhiteSpace(domicilioReceptor))
                {
                    receptorV.DomicilioReceptor = domicilioReceptor;
                }

                comprobanteVenta.Receptor = receptorV;
            }

            List<ComprobanteConceptoVenta> listaConceptoV = new List<ComprobanteConceptoVenta>();

            DataTable dProdVenta = cn.CargarDatos(cs.cargar_datos_venta_xml(4, idVenta, 0));

            if (!dProdVenta.Rows.Count.Equals(0))
            {
                foreach (DataRow rProdVenta in dProdVenta.Rows)
                {
                    ComprobanteConceptoVenta conceptoV = new ComprobanteConceptoVenta();

                    rProdVenta["Cantidad"] = RemoverCeroStock(rProdVenta["Cantidad"].ToString());

                    conceptoV.Cantidad = Convert.ToDecimal(rProdVenta["Cantidad"]);
                    conceptoV.Descripcion = rProdVenta["Nombre"].ToString();
                    conceptoV.ValorUnitario = Convert.ToDecimal(rProdVenta["Precio"]);

                    decimal importeV = Convert.ToDecimal(rProdVenta["Cantidad"]) * Convert.ToDecimal(rProdVenta["Precio"]);

                    conceptoV.Importe = importeV;

                    sumaImporteConcepto += importeV;

                    // Descuento
                    if (!string.IsNullOrWhiteSpace(rProdVenta["descuento"].ToString()))
                    {
                        var tDesc = (rProdVenta["descuento"].ToString()).IndexOf("-");

                        if (tDesc > -1)
                        {
                            string d = rProdVenta["descuento"].ToString();
                            int tam = rProdVenta["descuento"].ToString().Length;

                            string cDesc = d.Substring(0, tDesc);
                            string procDesc = d.Substring((tDesc + 2), (tam - (tDesc + 2)));

                            conceptoV.Descuento = Convert.ToDecimal(cDesc);
                            conceptoV.PorcentajeDescuento = procDesc;
                            sumaDescuento += Convert.ToDecimal(cDesc);
                        }
                        else
                        {
                            conceptoV.Descuento = Convert.ToDecimal(rProdVenta["descuento"]);
                            sumaDescuento += Convert.ToDecimal(rProdVenta["descuento"]);
                        }
                    }
                    listaConceptoV.Add(conceptoV);
                }
                comprobanteVenta.Conceptos = listaConceptoV.ToArray();
            }

            // Datos generales de la venta
            decimal totalGeneral = sumaImporteConcepto - sumaDescuento;

            if (totalGeneral >= anticipo)
            {
                totalGeneral = totalGeneral - anticipo;
            }
            else
            {
                if (totalGeneral < anticipo)
                {
                    anticipo = totalGeneral;
                    totalGeneral = totalGeneral - anticipo;
                }
            }

            comprobanteVenta.Serie = serie;
            comprobanteVenta.Folio = folio;
            comprobanteVenta.Fecha = fecha.ToString("yyyy-MM-dd HH:mm:ss");
            comprobanteVenta.FormaPago = formaPago;
            comprobanteVenta.SubTotal = sumaImporteConcepto;
            comprobanteVenta.Descuento = sumaDescuento;
            comprobanteVenta.Total = totalGeneral;
            comprobanteVenta.LugarExpedicion = lugarExpedicion;
            comprobanteVenta.Anticipo = anticipo;

            // ....................................................
            // Inicia con la generación de la plantilla 
            // y conversión a PDF    
            // ....................................................
            var servidor = Properties.Settings.Default.Hosting;
            string carpetaVenta = string.Empty;
            // Nombre que tendrá el pdf de la venta
            string nombreVenta = string.Empty;
            var Usuario = FormPrincipal.userNickName;
            var Folio = string.Empty;
            var Serie = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                carpetaVenta = $@"\\{servidor}\Archivos PUDVE\Ventas\PDF\{Usuario}\";
            }
            else
            {
                carpetaVenta = $@"C:\Archivos PUDVE\Ventas\PDF\{Usuario}\";
            }

            // Verifica si tiene creado el directorio
            if (!Directory.Exists(carpetaVenta))
            {
                Directory.CreateDirectory(carpetaVenta);
            }

            using (DataTable dtDatosVentas = cn.CargarDatos(cs.DatosVentaParaLaNota(idVenta)))
            {
                if (!dtDatosVentas.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in dtDatosVentas.Rows)
                    {
                        Folio = item["Folio"].ToString();
                        Serie = item["Serie"].ToString();
                    }
                }
            }

            nombreVenta += $"VENTA_NoVenta{idVenta}_Folio{Folio}{Serie}";

            string origenPDFTemp = nombreVenta + ".pdf";
            string destinoPDF = carpetaVenta + nombreVenta + ".pdf";

            string ruta = AppDomain.CurrentDomain.BaseDirectory + "/";
            // Creación de un archivo html temporal
            string rutaHTMLTemp = ruta + "ventahtml.html";
            // Plantilla que contiene el acomodo del PDF
            string rutaPlantillaHTML = ruta + "Plantilla_notaventa.html";
            string sHTML = GetStringOfFile(rutaPlantillaHTML);
            string resultHTML = "";

            resultHTML = RazorEngine.Razor.Parse(sHTML, comprobanteVenta);

            // Configuración de footer y header
            var footerSettings = new FooterSettings
            {
                ContentSpacing = 10,
                FontSize = 10,
                RightText = "[page] / [topage]"
            };
            var headerSettings = new HeaderSettings
            {
                ContentSpacing = 8,
                FontSize = 9,
                FontName = "Lucida Sans",
                LeftText = "Folio " + comprobanteVenta.Folio + " Serie " + comprobanteVenta.Serie
            };

            var document = new HtmlToPdfDocument
            {
                GlobalSettings =
                {
                    ProduceOutline = true,
                    PaperSize = PaperKind.Letter,
                    Margins =
                    {
                        Top = 2.3,
                        Right = 1.5,
                        Bottom = 2.3,
                        Left = 1.5,
                        Unit = Unit.Centimeters,
                    }
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlText = resultHTML,
                        HeaderSettings = headerSettings,
                        FooterSettings = footerSettings
                    }
                }
            };

            // Convertir el documento
            byte[] result = converter.Convert(document);

            ByteArrayToFile(result, destinoPDF);
        }

        public static IConverter converter = new ThreadSafeConverter(
            new RemotingToolset<PdfToolset>(
                new Win32EmbeddedDeployment(
                    new TempFolderDeployment()
                )
            )
        );

        public static bool ByteArrayToFile(byte[] _byteArray, string _fileName)
        {
            try
            {
                // Abre el archivo
                FileStream fileStream = new FileStream(_fileName, FileMode.Create, FileAccess.Write);
                // Escribe un bloque de bytes para este stream usando datos de una matriz de bytes
                fileStream.Write(_byteArray, 0, _byteArray.Length);
                fileStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex.ToString());
            }

            return false;
        }

        public static string GetStringOfFile(string rutaArchivo)
        {
            string contenido = File.ReadAllText(rutaArchivo);

            return contenido;
        }

        public static void MensajeCuandoSeaCeroEnElListado(string mensaje)
        {
            MessageBox.Show(mensaje, "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
