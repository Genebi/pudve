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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class AgregarAnticipo : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();
        string NombreClienteCorreo;
        string Dinero;
        string formadepagoCorreo;

        public AgregarAnticipo()
        {
            InitializeComponent();
        }

        private void AgregarAnticipo_Load(object sender, EventArgs e)
        {
            CargarClientes();
            cbClientes.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbFormaPago.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            //ComboBox Formas de pago
            Dictionary<string, string> pagos = new Dictionary<string, string>();
            if (Inventario.devolucion == "Devolver Inventario")
            {
                pagos.Add("01", "01 - Efectivo");
                pagos.Add("02", "02 - Cheque nominativo");
                pagos.Add("03", "03 - Transferencia electrónica de fondos");
                pagos.Add("04", "04 - Tarjeta de crédito");
                pagos.Add("05", "05 - Devolucion de Producto");
                pagos.Add("08", "08 - Vales de despensa");
            }
            else
            {
                pagos.Add("01", "01 - Efectivo");
                pagos.Add("02", "02 - Cheque nominativo");
                pagos.Add("03", "03 - Transferencia electrónica de fondos");
                pagos.Add("04", "04 - Tarjeta de crédito");
                pagos.Add("08", "08 - Vales de despensa");
            }

            

            cbFormaPago.DataSource = pagos.ToArray();
            cbFormaPago.DisplayMember = "Value";
            cbFormaPago.ValueMember = "Key";

            txtImporte.KeyPress += new KeyPressEventHandler(SoloDecimales);

            //foreach (Control control in this.Controls)
            //{
            //    control.PreviewKeyDown += new PreviewKeyDownEventHandler(AgregarAnticipo_PreviewKeyDown);
            //}
            AgregarAnticipo form = this;
            Utilidades.EjecutarAtajoKeyPreviewDown(AgregarAnticipo_PreviewKeyDown, form);

            if (Inventario.desdeRegresarProdcuto == 1)
            {
                txtImporte.Text = Inventario.totalFinal.ToString();
                txtConcepto.Text = "Devolucion de Productos";
                cbFormaPago.SelectedIndex = 4;
                cbFormaPago.Enabled = false;
                txtConcepto.Enabled = false;
                txtImporte.Enabled = false;
            }
        }

        private void CargarClientes()
        {
            //ComboBox Clientes
            var clientes = mb.ObtenerClientes(FormPrincipal.userID);

            if (clientes.Length > 0)
            {
                cbClientes.Items.AddRange(clientes);
                cbClientes.SelectedIndex = 0;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (cbClientes.SelectedIndex != 0)
            {
                if (string.IsNullOrWhiteSpace(txtConcepto.Text))
                {
                    txtConcepto.Focus();
                    MessageBox.Show("El concepto es requerido", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtImporte.Text))
                {
                    txtImporte.Focus();
                    MessageBox.Show("Ingresa el importe del anticipo", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var concepto = txtConcepto.Text;
                var importe = Convert.ToDouble(txtImporte.Text);
                Dinero = importe.ToString("C2");

                if (importe <= 0)
                {
                    txtImporte.Focus();
                    MessageBox.Show("La cantidad de importe debe ser mayor a cero", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var cliente = cbClientes.GetItemText(cbClientes.SelectedItem);
                NombreClienteCorreo = cliente;
                if (string.IsNullOrWhiteSpace(cliente))
                {
                    var existenClientes = mb.ObtenerClientes(FormPrincipal.userID);

                    if (existenClientes.Length == 0)
                    {
                        AgregarCliente ac = new AgregarCliente();

                        ac.FormClosed += delegate
                        {
                            CargarClientes();
                        };

                        ac.ShowDialog();
                    }

                    return;
                }

                var formaPago = cbFormaPago.SelectedValue.ToString();
                formadepagoCorreo = formaPago;
                var comentario = txtComentarios.Text;
                var status = "1";
                var FechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string[] datos = new string[] { FormPrincipal.userID.ToString(), concepto, importe.ToString("0.00"), cliente, formaPago, comentario, status, FechaOperacion };

                int respuesta = cn.EjecutarConsulta(cs.GuardarAnticipo(datos, FormPrincipal.id_empleado));

                if (respuesta > 0)
                {
                    var efectivo = "0";
                    var cheque = "0";
                    var transferencia = "0";
                    var tarjeta = "0";
                    var vales = "0";
                    var credito = "0";

                    //Operacion para afectar la Caja
                    if (formaPago == "01") { efectivo = importe.ToString(); }
                    if (formaPago == "02") { cheque = importe.ToString(); }
                    if (formaPago == "03") { transferencia = importe.ToString(); }
                    if (formaPago == "04") { tarjeta = importe.ToString(); }
                    if (formaPago == "08") { vales = importe.ToString(); }

                    if (FormPrincipal.userNickName.Contains("@"))
                    {
                        datos = new string[] { "anticipo", importe.ToString("0.00"), "0", "", FechaOperacion, FormPrincipal.userID.ToString(), efectivo, tarjeta, vales, cheque, transferencia, credito, "0", FormPrincipal.id_empleado.ToString() };

                        cn.EjecutarConsulta(cs.agregarAnticipoCajaEmpleado(datos));
                    }
                    else
                    {
                        datos = new string[] { "anticipo", importe.ToString("0.00"), "0", "", FechaOperacion, FormPrincipal.userID.ToString(), efectivo, tarjeta, vales, cheque, transferencia, credito, "0", FormPrincipal.id_empleado.ToString() };

                        cn.EjecutarConsulta(cs.OperacionCaja(datos));
                    }

                    //Fin operacion caja


                    var idAnticipo = cn.EjecutarSelect($"SELECT ID FROM Anticipos WHERE IDUsuario = {FormPrincipal.userID} ORDER BY ID DESC LIMIT 1", 1).ToString();

                    datos = new string[] { FechaOperacion, cliente, concepto, importe.ToString("0.00"), comentario, idAnticipo };

                    GenerarTicket(datos);
                    using (var dt = cn.CargarDatos($"SELECT TicketAnticipo,PreguntarTicketAnticipo,AbrirCajaAnticipos FROM configuraciondetickets WHERE IDUSuario = {FormPrincipal.userID}"))
                    {
                        if (dt.Rows[0][0].Equals(1))
                        {
                            ImpresionTicketAnticipo imprimirAnt = new ImpresionTicketAnticipo();
                            imprimirAnt.idAnticipo = Convert.ToInt32(idAnticipo);
                            imprimirAnt.anticipoSinHistorial = 1;
                            imprimirAnt.ShowDialog();
                        }
                        else if (dt.Rows[0][1].Equals(1))
                        {
                            DialogResult nose = MessageBox.Show("¿Desea imprimir Ticket?", "Aviso del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (nose.Equals(DialogResult.Yes))
                            {
                                ImpresionTicketAnticipo imprimirAnt = new ImpresionTicketAnticipo();
                                imprimirAnt.idAnticipo = Convert.ToInt32(idAnticipo);
                                imprimirAnt.anticipoSinHistorial = 1;
                                imprimirAnt.ShowDialog();
                            }
                            else if (dt.Rows[0]["AbrirCajaAnticipos"].Equals(1))
                            {
                                AbrirSinTicket abrir = new AbrirSinTicket();
                                abrir.Show();
                            }
                        }
                        else if (dt.Rows[0]["AbrirCajaAnticipos"].Equals(1))
                        {
                            AbrirSinTicket abrir = new AbrirSinTicket();
                            abrir.Show();
                        }
                    }
                    
                   

                    var datosConfig = mb.ComprobarConfiguracion();
                    if (datosConfig.Count > 0)
                    {
                        int inicio = Convert.ToInt32(datosConfig[27]);

                        if (inicio == 1)
                        {
                            EnvioDeCorreoNuevoAnticipo();
                        }
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al intentar guardar el anticipo.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un cliente", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            


        }

        private void EnvioDeCorreoNuevoAnticipo()
        {
            string correo;

            using (DataTable email = cn.CargarDatos(cs.BuscarCorreoDelUsuario(FormPrincipal.userID)))
            {
                correo = email.Rows[0]["Email"].ToString();
            }
            string Usuario;
            if (FormPrincipal.userNickName.Contains("@"))
            {
                var separacion = FormPrincipal.userNickName.Split('@');
                Usuario = separacion[1].ToString();
            }
            else
            {
                Usuario = FormPrincipal.userNickName;
            }
            var FormadePago = cbFormaPago.Text.Split('-');

            var FechaHoy = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
            var asunto = "Nuevo Anticipo Recibido";
            var html = $@"<div>
            <div style = 'text-align: center;' >
            <h3>Anticipo Registrado</h3>
            </div>
            <hr>
            
               Se registro un nuevo Anticipo a nombre del cliente <b>{NombreClienteCorreo}</b> <br>
               <br>    
               fecha: <b>{FechaHoy}</b> <br>
               <br>
               Monto: <b>{Dinero}</b><br>
               <br>
               Metodo de Pago: <b>{FormadePago[1].ToString()}</b><br> 
               <br> 
               Concepto: <b>{txtConcepto.Text}</b> <br>
               <br>
               Recibido por: <b>{Usuario}</b>
              
             <hr>
            </div>";

            Utilidades.EnviarEmail(html, asunto, correo);
        }

        private void SoloDecimales(object sender, KeyPressEventArgs e)
        {
            //permite 0-9, eliminar y decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            //verifica que solo un decimal este permitido
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                {
                    e.Handled = true;
                }
            }
        }


        private void GenerarTicket(string[] info)
        {
            var datos = FormPrincipal.datosUsuario;

            //Medidas de ticket de 57 y 80 mm
            //57mm = 161.28 pt
            //80mm = 226.08 pt

            var tipoPapel = 57;
            var anchoPapel = Convert.ToInt32(Math.Floor((((tipoPapel * 0.10) * 72) / 2.54)));
            var altoPapel = Convert.ToInt32(anchoPapel + 54);

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

            Document ticket = new Document(new iTextSharp.text.Rectangle(anchoPapel, altoPapel), 3, 3, 5, 0);
            PdfWriter writer = PdfWriter.GetInstance(ticket, new FileStream(@"C:\Archivos PUDVE\Anticipos\Tickets\ticket_anticipo_" + info[5] + ".pdf", FileMode.Create));

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteNormal);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, medidaFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteGrande);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteMensaje);

            string logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + datos[11];
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

            Paragraph separadorInicial = new Paragraph(new string('-', separadores), fuenteNormal);

            //Contenido del Ticket
            string contenido = $"{info[0]}\nCliente: {info[1]}\nConcepto: {info[2]}\nImporte: ${info[3]}\n{info[4]}";

            Paragraph cuerpo = new Paragraph(contenido, fuenteNormal);
            cuerpo.Alignment = Element.ALIGN_CENTER;

            Paragraph mensaje = new Paragraph("Comprobante de Anticipo.", fuenteGrande);
            mensaje.Alignment = Element.ALIGN_CENTER;

            Paragraph separadorFinal = new Paragraph(new string('-', separadores), fuenteNormal);

            ticket.Add(titulo);
            ticket.Add(domicilio);
            ticket.Add(separadorInicial);
            ticket.Add(cuerpo);
            ticket.Add(separadorFinal);
            ticket.Add(mensaje);

            ticket.AddTitle("Ticket Anticipo");
            ticket.AddAuthor("PUDVE");
            ticket.Close();
            writer.Close();
        }

        private void txtImporte_KeyUp(object sender, KeyEventArgs e)
        {
            var obtenerTxt = txtImporte.Text;

            if (obtenerTxt.Equals("."))
            {
                txtImporte.Text = "0.";
                txtImporte.Select(txtImporte.Text.Length, 0);
            }
        }

        private void txtComentarios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void AgregarAnticipo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void AgregarAnticipo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            int calcu = 0;
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        if (calculadora.seEnvia.Equals(true))
                        {
                            txtImporte.Text = calculadora.lCalculadora.Text;
                        }
                        calcu = 0;
                    };
                    if (!calculadora.Visible)
                    {
                        calculadora.Show();
                    }
                    else
                    {
                        calculadora.Show();
                    }
                }
            }
        }
    }
}
