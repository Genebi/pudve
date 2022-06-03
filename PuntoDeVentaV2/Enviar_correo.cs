using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class Enviar_correo : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        int i = 1;
        string[][] arr_ids_f_enviar;
        string tipo = "";
        int tipo_factura = 0;
        int id_empleado = FormPrincipal.id_empleado;

        string folio;
        string Serie;
        string nombreUsuario;


        public Enviar_correo(string[][] arr_ids, string titulo, int tp_factura)
        {
            InitializeComponent();

            int tam = arr_ids.Length;
            arr_ids_f_enviar = new string[tam][];
            arr_ids_f_enviar = arr_ids;

            lb_titulo.Text = "Enviar " + titulo;
            cargar_correos_default();

            tipo = titulo;
            tipo_factura = tp_factura;
        }

        private void cargar_correos_default()
        {
            for (int x = 0; x < arr_ids_f_enviar.Length; x++)
            {
                DataTable d_factura = cn.CargarDatos(cs.cargar_datos_venta_xml(1, Convert.ToInt32(arr_ids_f_enviar[x][0]), FormPrincipal.userID));

                if (d_factura.Rows.Count > 0)
                {
                    DataRow r_factura = d_factura.Rows[0];

                    if (r_factura["r_correo"].ToString() != "")
                    {
                        agregar_elementos_correo(r_factura["r_correo"].ToString());
                    }
                }
            }
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            if (txt_correo.Text != "")
            {
                string ex_regular = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

                if (Regex.IsMatch(txt_correo.Text, ex_regular))
                {
                    // Agregar elementos 

                    agregar_elementos_correo(txt_correo.Text);

                    txt_correo.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("El formato del correo no es valido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No hay ningún correo por agregar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void agregar_elementos_correo(string txtcorreo)
        {
            FlowLayoutPanel p_correo = new FlowLayoutPanel();
            p_correo.Name = "pnl_correo" + i;
            p_correo.Size = new Size(365, 25);

            PictureBox img = new PictureBox();
            img.Name = "pctr_borrar" + i;
            img.Location = new Point(1, 1);
            img.Size = new Size(18, 18);
            img.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\close.png");
            img.Cursor = Cursors.Hand;
            img.Click += new EventHandler(img_eliminar_click);

            Label lbl = new Label();
            lbl.Name = "lb_correo" + i;
            lbl.Location = new Point(14, 1);
            lbl.Size = new Size(370, 24);
            lbl.Text = txtcorreo;

            p_correo.Controls.Add(img);
            p_correo.Controls.Add(lbl);
            p_correo.FlowDirection = FlowDirection.TopDown;

            pnl_principal.Controls.Add(p_correo);
            pnl_principal.FlowDirection = FlowDirection.TopDown;

            i++;
        }

        private void img_eliminar_click(object sender, EventArgs e)
        {
            PictureBox img_eliminar = sender as PictureBox;

            string id_img = img_eliminar.Name.Substring(11);
            string pnl_eliminar = "pnl_correo" + id_img;

            foreach (Control pnl_el in pnl_principal.Controls.OfType<FlowLayoutPanel>())
            {
                if (pnl_el.Name == pnl_eliminar)
                {
                    pnl_principal.Controls.Remove(pnl_el);
                }
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            List<string> list_correos = new List<string>();
            string[] arr_obtiene_datos_correo = new string[3];
            var servidor = Properties.Settings.Default.Hosting;


            MessageBox.Show("La envío tardará 8 segundos (aproximadamente) en ser enviado. \n Por favor no cierre la ventana. \n\n Un momento por favor...", "Mensaje del sistema", MessageBoxButtons.OK);

            // Deshabilita los botones 

            btn_enviar.Enabled = false;
            btn_cancelar.Enabled = false;
            btn_enviar.Cursor = Cursors.No;
            btn_cancelar.Cursor = Cursors.No;


            // Guarda los correos 

            foreach (Control pnl in pnl_principal.Controls)
            {
                if (pnl.Name.Contains("pnl_correo"))
                {
                    foreach (Control pnl_sec in pnl.Controls)
                    {
                        if (pnl_sec.Name.Contains("lb_correo"))
                        {
                            list_correos.Add(pnl_sec.Text);
                        }
                    }
                }
            }


            // Obtiene datos ya sea de la factura, o de la nota de venta

            if (tipo == "factura")
            {
                arr_obtiene_datos_correo = datos_factura();
            }
            if (tipo == "nota de venta")
            {
                arr_obtiene_datos_correo = datos_nota_venta();
            }

            string correo_emisor = arr_obtiene_datos_correo[0];
            DateTime fecha_cfdi = Convert.ToDateTime(arr_obtiene_datos_correo[1]);
            string contenido = arr_obtiene_datos_correo[2];



            // Inicia la formación del correo

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("pudve.contacto@gmail.com", "grtpoxrdmngbozwm");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp.TargetName = "STARTTLS/smtp.office365.com";
            //smtp.SSLConfiguration.EnabledSslProtocols = SslProtocols.Tls12;



            MailMessage correo = new MailMessage();
            // De
            correo.From = new MailAddress("pudve.contacto@gmail.com");

            // Para
            foreach (string c in list_correos)
            {
                correo.To.Add(new MailAddress(c));
            }

            // Responder a...
            //correo.ReplyToList.Add(correo_emisor);
            correo.ReplyToList.Add("pudve.contacto@gmail.com");

            // Asunto
            if (tipo == "factura")
            {
                if (tipo_factura == 3)
                {
                    correo.Subject = "PUDVE - Acuse de cancelación de factura";
                }
                else
                {
                    correo.Subject = "PUDVE - Factura de " + fecha_cfdi.ToString("MMMM yyyy");
                }
            }
            else
            {
                correo.Subject = "PUDVE - Nota de venta de " + fecha_cfdi.ToString("MMMM yyyy");
            }


            // Contenido
            correo.Body = contenido;
            correo.IsBodyHtml = true;

            // Archivos a enviar

            
            if (FormPrincipal.userNickName.Contains("@"))
            {
                string[] otro = FormPrincipal.userNickName.Split('@');
                nombreUsuario = otro[0].ToString();
            }
            else
            {
                nombreUsuario = FormPrincipal.userNickName;
            }
            var NumerodeVenta = arr_ids_f_enviar[0][0].ToString();
            using (DataTable consultaFolio = cn.CargarDatos($"SELECT Folio from ventas WHERE ID = {NumerodeVenta}"))
            {
                folio = consultaFolio.Rows[0]["Folio"].ToString();
            }
            using (DataTable ConsultaSerie = cn.CargarDatos($"SELECT Serie from ventas WHERE ID = {NumerodeVenta}"))
            {
                Serie = ConsultaSerie.Rows[0]["Serie"].ToString();
            }

            for (int i=0; i<arr_ids_f_enviar.Length; i++)
            {
                if(tipo == "factura")
                {
                    string sin_con_acuse = "";

                    if(tipo_factura == 3)
                    {
                        sin_con_acuse = "ACUSE_";
                    }
                    string tipo_comprobante = "INGRESOS_";

                    if (arr_ids_f_enviar[i][1] == "P")
                    {
                        tipo_comprobante = "PAGO_";
                    }
                    tipo_comprobante += sin_con_acuse;

                    

                    

                    // Verifica
                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        if (tipo_factura != 3)
                        {
                            correo.Attachments.Add(new Attachment($@"\\{servidor}\Archivos PUDVE\Facturas\XML_" + tipo_comprobante + arr_ids_f_enviar[i][0] + ".pdf"));
                        }

                        correo.Attachments.Add(new Attachment($@"\\{servidor}\Archivos PUDVE\Facturas\XML_" + tipo_comprobante + arr_ids_f_enviar[i][0] + ".xml"));
                    }
                    else
                    {
                        if (tipo_factura != 3)
                        {
                            correo.Attachments.Add(new Attachment(@"C:\Archivos PUDVE\Facturas\XML_" + tipo_comprobante + arr_ids_f_enviar[i][0] + ".pdf"));
                        }

                        correo.Attachments.Add(new Attachment(@"C:\Archivos PUDVE\Facturas\XML_" + tipo_comprobante + arr_ids_f_enviar[i][0] + ".xml"));
                    }
                }

                if (tipo == "nota de venta")
                {
                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        
                        correo.Attachments.Add(new Attachment($@"\\{servidor}\Archivos PUDVE\Ventas\PDF\VENTA_" + arr_ids_f_enviar[i][0] + ".pdf"));
                    }
                    else
                    {
                        correo.Attachments.Add(new Attachment($@"C:\Archivos PUDVE\Ventas\PDF\{nombreUsuario}\VENTA_NoVenta{NumerodeVenta}_Folio{folio}{Serie}.pdf"));
                        //correo.Attachments.Add(new Attachment(@"C:\Archivos PUDVE\Ventas\PDF\VENTA_" + arr_ids_f_enviar[i][0] + ".pdf"));
                        //correo.Attachments.Add(new Attachment(@"C:\Archivos PUDVE\Ventas\PDF\HOUSEDEPOTAUTLAN\VENTA_NoVenta171283_Folio170522A.pdf"));
                    }
                }
            }

            // Prioridad
            correo.Priority = MailPriority.Normal;



            // Envío del correo

            try
            {
                smtp.Send(correo);
                correo.Dispose();

                if (tipo == "factura")
                {
                    for (int x = 0; x < arr_ids_f_enviar.Length; x++)
                    {
                        cn.EjecutarConsulta($"UPDATE Facturas SET id_emp_envia='{id_empleado}', f_enviada=1 WHERE ID='{arr_ids_f_enviar[x][0]}'");
                    }
                }

                MessageBox.Show("La " + tipo + " ha sido enviada con éxito.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Dispose();
            }
            catch (Exception ex)
            {
                btn_enviar.Enabled = true;
                btn_cancelar.Enabled = true;
                btn_enviar.Cursor = Cursors.Hand;
                btn_cancelar.Cursor = Cursors.Hand;

                MessageBox.Show("La " + tipo + " no fue enviada. Revisar que el correo se halla escrito correctamente.  \n\n" + ex.Message + "", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }
        
        private string[] datos_factura()
        {
            string nombre = "";
            string rfc = "";
            string tel = "";
            string correoe = "";
            string[] arr_datos_correo = new string[3];
            DateTime fecha_factura_p = DateTime.Now;
            DateTime[] arr_fechas;
            string cadena_fechas = "";
            var servidor = Properties.Settings.Default.Hosting;

            // Genera los PDF en caso de que no lo esten
            // Solo se generara si la factura no esta cancelada

            if (tipo_factura != 3)
            {
                for (int i = 0; i < arr_ids_f_enviar.Length; i++)
                {
                    string nom = "XML_INGRESOS_" + arr_ids_f_enviar[i][0];

                    if (arr_ids_f_enviar[i][1] == "P")
                    {
                        nom = "XML_PAGO_" + arr_ids_f_enviar[i][0];
                    }

                    // Verifica si el archivo pdf ya esta creado, de no ser así lo crea

                    string ruta_archivo = @"C:\Archivos PUDVE\Facturas\" + nom + ".pdf";

                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        ruta_archivo = $@"\\{servidor}\Archivos PUDVE\Facturas\" + nom + ".pdf";
                    }

                    if (!File.Exists(ruta_archivo))
                    {
                        Facturas f = new Facturas();
                        f.generar_PDF(nom, Convert.ToInt32(arr_ids_f_enviar[i][0]));
                    }
                }
            }


            // Obtiene datos del emisor y factura

            DataTable d_emisor = cn.CargarDatos(cs.cargar_datos_venta_xml(2, 0, FormPrincipal.userID));
            DataRow r_emisor = d_emisor.Rows[0];

            nombre = r_emisor["RazonSocial"].ToString();
            rfc = r_emisor["RFC"].ToString();
            tel = r_emisor["Telefono"].ToString();
            correoe = r_emisor["Email"].ToString();


            // Obtiene fechas de las facturas
            // Las fechas serán obtenidas solo si la factura esta vigente


            arr_fechas = new DateTime[arr_ids_f_enviar.Length];

            for (int i = 0; i < arr_ids_f_enviar.Length; i++)
            {
                int id_f = 0;
                id_f = Convert.ToInt32(arr_ids_f_enviar[i][0]);

                DataTable d_factura = cn.CargarDatos(cs.cargar_datos_venta_xml(1, id_f, FormPrincipal.userID));
                DataRow r_factura = d_factura.Rows[0];

                if (i == 0)
                {
                    fecha_factura_p = Convert.ToDateTime(r_factura["fecha_certificacion"]);
                }

                arr_fechas[i] = Convert.ToDateTime(r_factura["fecha_certificacion"]);
            }

            if (arr_fechas.Length > 1)
            {
                if (tipo_factura == 3)
                {
                    cadena_fechas = "Le envía el acuse de cancelación de su CFDI por la compra del día " + arr_fechas[0].ToString("dd/MMMM/yyyy");
                }
                else
                {
                    cadena_fechas += "Le envía sus CFDI por las compras del día: ";
                }
                cadena_fechas += "<ul>";

                for (int ii = 0; ii < arr_fechas.Length; ii++)
                {
                    cadena_fechas += "<li>" + arr_fechas[ii].ToString("dd/MMMM/yyyy") + "</li>";
                }

                cadena_fechas += "</ul>";
            }
            else
            {
                if (tipo_factura == 3)
                {
                    cadena_fechas = "Le envía el acuse de cancelación de su CFDI por la compra del día " + arr_fechas[0].ToString("dd/MMMM/yyyy");
                }
                else
                {
                    cadena_fechas = "Le envía su CFDI por la compra del día " + arr_fechas[0].ToString("dd/MMMM/yyyy");
                }
            }


            // Contenido del correo

            string contenido = "<div style='padding: 5em 9em; background: #F3F3F3;'>" +
                            "<div style='padding: 1.7em; background: #FFF; font-size: 1.4em;'>" +

                                "<div>Estimado(a) cliente(a).<div>" +

                                "<div style='margin-top: 2.2em;'>" +
                                    nombre + ", con RFC " + rfc + ". " +
                                    cadena_fechas + "." +
                                "</div>" +

                                "<div style='margin-top: 1em;'>" +
                                    "Para cualquier duda o aclaración respecto a su factura, contactar al siguiente número y/o correo. <br> " +
                                    "<br> Gracias por su compra. Excelente día." +
                                "</div>" +

                                "<div>" +
                                    "<ul> " +
                                        "<li><strong>Teléfono: </strong>" + tel + "</li>" +
                                        "<li><strong>Correo: </strong>" + correoe + "</li>" +
                                    "</ul> " +
                                "</div>" +
                            "</div>" +
                        "</div>";



            arr_datos_correo[0] = correoe;
            arr_datos_correo[1] = Convert.ToString(fecha_factura_p);
            arr_datos_correo[2] = contenido;


            return arr_datos_correo;
        }

        private string[] datos_nota_venta()
        {
            string nombre = "";
            string rfc = "";
            string tel = "";
            string correoe = "";
            string[] arr_datos_correo = new string[3];
            DateTime fecha_venta_p = DateTime.Now;
            DateTime[] arr_fechas;
            string cadena_fechas = "";
            var servidor = Properties.Settings.Default.Hosting;


            // Genera los PDF en caso de que no lo esten

            for (int i = 0; i < arr_ids_f_enviar.Length; i++)
            {
                // Verifica si el archivo pdf ya esta creado, de no ser así lo crea
                string ruta_archivo = @"C:\Archivos PUDVE\Ventas\PDF\VENTA_" + arr_ids_f_enviar[i][0] + ".pdf";

                if (!string.IsNullOrWhiteSpace(servidor))
                {
                    ruta_archivo = $@"\\{servidor}\Archivos PUDVE\Ventas\PDF\VENTA_" + arr_ids_f_enviar[i][0] + ".pdf";
                }

                if (!File.Exists(ruta_archivo))
                {
                    ListadoVentas vn = new ListadoVentas();
                    vn.ver_factura(Convert.ToInt32(arr_ids_f_enviar[i][0]));
                }
            }


            // Obtiene datos del emisor y nota

            DataTable d_emisor = cn.CargarDatos(cs.cargar_datos_venta_xml(2, 0, FormPrincipal.userID));
            DataRow r_emisor = d_emisor.Rows[0];

            nombre = r_emisor["RazonSocial"].ToString();
            rfc = r_emisor["RFC"].ToString();
            tel = r_emisor["Telefono"].ToString();
            correoe = r_emisor["Email"].ToString();


            // Obtiene fechas de la nota de venta

            arr_fechas = new DateTime[arr_ids_f_enviar.Length];

            for (int i = 0; i < arr_ids_f_enviar.Length; i++)
            {
                int id_v = Convert.ToInt32(arr_ids_f_enviar[i][0]);

                DataTable d_venta = cn.CargarDatos(cs.consulta_dventa(1, id_v));
                DataRow r_venta = d_venta.Rows[0];

                if (i == 0)
                {
                    fecha_venta_p = Convert.ToDateTime(r_venta["FechaOperacion"]);
                }

                arr_fechas[i] = Convert.ToDateTime(r_venta["FechaOperacion"]);
            }

            if (arr_fechas.Length > 1)
            {
                cadena_fechas += "Le envía sus notas de venta por las compras del día: ";
                cadena_fechas += "<ul>";

                for (int ii = 0; ii < arr_fechas.Length; ii++)
                {
                    cadena_fechas += "<li>" + arr_fechas[ii].ToString("dd/MMMM/yyyy") + "</li>";
                }

                cadena_fechas += "</ul>";
            }
            else
            {
                cadena_fechas = "Le envía su nota de venta por la compra del día " + arr_fechas[0].ToString("dd/MMMM/yyyy");
            }


            // Contenido del correo

            string contenido = "<div style='padding: 5em 9em; background: #F3F3F3;'>" +
                            "<div style='padding: 1.7em; background: #FFF; font-size: 1.4em;'>" +

                                "<div>Estimado(a) cliente(a).<div>" +

                                "<div style='margin-top: 2.2em;'>" +
                                    nombre + ", con RFC " + rfc + ". " +
                                    cadena_fechas + "." +
                                "</div>" +

                                "<div style='margin-top: 1em;'>" +
                                    "Para cualquier duda o aclaración respecto a su nota de venta, contactar al siguiente número y/o correo. <br> " +
                                    "<br> Gracias por su compra. Excelente día." +
                                "</div>" +

                                "<div>" +
                                    "<ul> " +
                                        "<li><strong>Teléfono: </strong>" + tel + "</li>" +
                                        "<li><strong>Correo: </strong>" + correoe + "</li>" +
                                    "</ul> " +
                                "</div>" +
                            "</div>" +
                        "</div>";



            arr_datos_correo[0] = correoe;
            arr_datos_correo[1] = Convert.ToString(fecha_venta_p);
            arr_datos_correo[2] = contenido;


            return arr_datos_correo;
        }

        private void Enviar_correo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Enviar_correo_Load(object sender, EventArgs e)
        {

        }

        private void txt_correo_TextChanged(object sender, EventArgs e)
        {
            var resultado = string.Empty;
            var txtValidarTexto = (TextBox)sender;
            resultado = txtValidarTexto.Text;

            if (!string.IsNullOrWhiteSpace(resultado))
            {
                Regex patronCorerecto = new Regex(@"^[a-zA-Z0-9ÑñÁáÉéÍíÓóÚú]");

                if (patronCorerecto.IsMatch(resultado))
                {
                    resultado = Regex.Replace(resultado, @"[Ñ]", "N");
                    resultado = Regex.Replace(resultado, @"[ñ]", "n");
                    resultado = Regex.Replace(resultado, @"[Á]", "A");
                    resultado = Regex.Replace(resultado, @"[á]", "a");
                    resultado = Regex.Replace(resultado, @"[É]", "E");
                    resultado = Regex.Replace(resultado, @"[é]", "e");
                    resultado = Regex.Replace(resultado, @"[Í]", "I");
                    resultado = Regex.Replace(resultado, @"[í]", "i");
                    resultado = Regex.Replace(resultado, @"[Ó]", "O");
                    resultado = Regex.Replace(resultado, @"[ó]", "o");
                    resultado = Regex.Replace(resultado, @"[Ú]", "U");
                    resultado = Regex.Replace(resultado, @"[ú]", "u");
                    txtValidarTexto.Text = resultado;
                    txtValidarTexto.Select(txtValidarTexto.Text.Length, 0);
                }
                else
                {
                    var resultadoAuxialiar = Regex.Replace(resultado, @"[^a-zA-Z0-9]", string.Empty).Trim();
                    resultado = resultadoAuxialiar;
                    txtValidarTexto.Text = resultado;
                    txtValidarTexto.Focus();
                    txtValidarTexto.Select(txtValidarTexto.Text.Length, 0);
                }
            }
            else
            {
                txtValidarTexto.Focus();
                txtValidarTexto.Select(txtValidarTexto.Text.Length, 0);
            }
        }
    }
}
