﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class AsignarAbonos : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        private int idVenta = 0;
        private float totalOriginal = 0f;
        private float totalPendiente = 0f;
        private float totalMetodos = 0f;
        private bool existenAbonos = false;

        public AsignarAbonos(int idVenta, float totalOriginal)
        {
            InitializeComponent();

            this.idVenta = idVenta;
            this.totalOriginal = totalOriginal;
        }

        private void AsignarAbonos_Load(object sender, EventArgs e)
        {
            //Asignamos el evento para solo aceptar cantidades decimales
            txtEfectivo.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTarjeta.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtVales.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCheque.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTransferencia.KeyPress += new KeyPressEventHandler(SoloDecimales);

            //Terminar abono presionando Enter
            txtEfectivo.KeyDown += new KeyEventHandler(TerminarVenta);
            txtTarjeta.KeyDown += new KeyEventHandler(TerminarVenta);
            txtVales.KeyDown += new KeyEventHandler(TerminarVenta);
            txtCheque.KeyDown += new KeyEventHandler(TerminarVenta);
            txtTransferencia.KeyDown += new KeyEventHandler(TerminarVenta);

            //Suma de los metodos de pago excepto efectivo
            txtTarjeta.KeyUp += new KeyEventHandler(SumaMetodosPago);
            txtVales.KeyUp += new KeyEventHandler(SumaMetodosPago);
            txtCheque.KeyUp += new KeyEventHandler(SumaMetodosPago);
            txtTransferencia.KeyUp += new KeyEventHandler(SumaMetodosPago);

            var detalles = mb.ObtenerDetallesVenta(idVenta, FormPrincipal.userID);
            totalPendiente = float.Parse(detalles[2]);
            txtTotalOriginal.Text = "$" + totalOriginal.ToString("0.00");

            //Comprobamos que no existan abonos
            existenAbonos = (bool)cn.EjecutarSelect($"SELECT * FROM Abonos WHERE IDVenta = {idVenta} AND IDUsuario = {FormPrincipal.userID}");

            if (!existenAbonos)
            {
                txtPendiente.Text = "$" + totalPendiente.ToString("0.00");
            }
            else
            {
                var abonado = mb.ObtenerTotalAbonado(idVenta, FormPrincipal.userID);
                var restante = totalPendiente - abonado;
                txtPendiente.Text = "$" + restante.ToString("0.00");
                totalPendiente = restante;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            float totalEfectivo = 0f;

            var tarjeta = CantidadDecimal(txtTarjeta.Text);
            var vales = CantidadDecimal(txtVales.Text);
            var cheque = CantidadDecimal(txtCheque.Text);
            var transferencia = CantidadDecimal(txtTransferencia.Text);
            var referencia = txtReferencia.Text;
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (SumaMetodos() > 0)
            {
                totalEfectivo = totalPendiente - (SumaMetodos() + CantidadDecimal(txtEfectivo.Text));
            }
            else
            {
                totalEfectivo = CantidadDecimal(txtEfectivo.Text);

                if (totalEfectivo > totalPendiente && SumaMetodos() == 0)
                {
                    totalEfectivo = Math.Abs(CantidadDecimal(txtEfectivo.Text) - totalPendiente);
                }
            }

            var totalAbonado = totalEfectivo + tarjeta + vales + cheque + transferencia;

            //Condicion para saber si se termino de pagar y cambiar el status de la venta
            if (totalAbonado >= totalPendiente)
            {
                cn.EjecutarConsulta(cs.ActualizarVenta(idVenta, 1, FormPrincipal.userID));
            }

            string[] datos = new string[] {
                idVenta.ToString(), FormPrincipal.userID.ToString(), totalAbonado.ToString(), totalEfectivo.ToString(), tarjeta.ToString(),
                vales.ToString(), cheque.ToString(), transferencia.ToString(), referencia, fechaOperacion
            };

            
            int resultado = cn.EjecutarConsulta(cs.GuardarAbonos(datos));

            if (resultado > 0)
            {
                var idAbono = cn.EjecutarSelect($"SELECT * FROM Abonos WHERE IDVenta = {idVenta} AND IDUsuario = {FormPrincipal.userID} ORDER BY FechaOperacion DESC LIMIT 1", 1).ToString();
                var restante = totalPendiente - totalAbonado;

                datos = new string[] { idVenta.ToString(), idAbono, totalOriginal.ToString("0.00"), totalPendiente.ToString("0.00"), totalAbonado.ToString("0.00"), restante.ToString("0.00"), fechaOperacion };

                GenerarTicket(datos);
                ImprimirTicket(idVenta.ToString(), idAbono);

                this.Dispose();
            }
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

        private float CantidadDecimal(string cantidad)
        {
            var valor = 0f;

            if (string.IsNullOrEmpty(cantidad))
            {
                valor = 0;
            }
            else
            {
                valor = float.Parse(cantidad);
            }

            return valor;
        }

        private bool ValidarCantidades()
        {
            var efectivo = CantidadDecimal(txtEfectivo.Text);
            var tarjeta = CantidadDecimal(txtTarjeta.Text);
            var vales = CantidadDecimal(txtVales.Text);
            var cheque = CantidadDecimal(txtCheque.Text);
            var transferencia = CantidadDecimal(txtTransferencia.Text);
            var total = efectivo + tarjeta + vales + cheque + transferencia;

            if (total <= totalPendiente)
            {
                return true;
            }

            return false;
        }

        private void lbVerAbonos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (existenAbonos)
            {
                this.Hide();

                ListaAbonosVenta abonos = new ListaAbonosVenta(idVenta);

                abonos.FormClosed += delegate
                {
                    this.Show();
                };

                abonos.ShowDialog();
            }
            else
            {
                MessageBox.Show("No existen abonos previos para esta venta", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TerminarVenta(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private float SumaMetodos()
        {
            float tarjeta = CantidadDecimal(txtTarjeta.Text);
            float vales = CantidadDecimal(txtVales.Text);
            float cheque = CantidadDecimal(txtCheque.Text);
            float transferencia = CantidadDecimal(txtTransferencia.Text);

            float suma = tarjeta + vales + cheque + transferencia;

            return suma;
        }

        private void CalcularCambio()
        {
            //El total del campo efectivo + la suma de los otros metodos de pago - total de venta
            double cambio = Convert.ToDouble((CantidadDecimal(txtEfectivo.Text) + totalMetodos) - totalPendiente);

            lbTotalCambio.Text = "$" + cambio.ToString("0.00");
        }


        private void SumaMetodosPago(object sender, KeyEventArgs e)
        {
            float suma = SumaMetodos();

            //Si es mayor al total a pagar vuelve a calcular las cantidades pero sin tomar en cuenta
            //el campo que hizo que fuera mayor a la cantidad a pagar
            if (suma > totalPendiente)
            {
                TextBox tb = sender as TextBox;

                tb.Text = string.Empty;

                suma = SumaMetodos();
            }

            totalMetodos = suma;

            CalcularCambio();
        }

        private void txtEfectivo_KeyUp(object sender, KeyEventArgs e)
        {
            CalcularCambio();
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
            PdfWriter writer = PdfWriter.GetInstance(ticket, new FileStream(@"C:\Archivos PUDVE\Ventas\Tickets\ticket_abono_" + info[0] + "_" + info[1] + ".pdf", FileMode.Create));

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
            string contenido = $"ID de venta: {info[0]}\nTotal original: ${info[2]}\nPendiente de pago: ${info[3]}\nCantidad abonada: ${info[4]}\nCantidad restante: ${info[5]}\n{info[6]}";

            Paragraph cuerpo = new Paragraph(contenido, fuenteNormal);
            cuerpo.Alignment = Element.ALIGN_CENTER;

            Paragraph mensaje = new Paragraph("Comprobante de Abono.", fuenteGrande);
            mensaje.Alignment = Element.ALIGN_CENTER;

            Paragraph separadorFinal = new Paragraph(new string('-', separadores), fuenteNormal);

            ticket.Add(titulo);
            ticket.Add(domicilio);
            ticket.Add(separadorInicial);
            ticket.Add(cuerpo);
            ticket.Add(separadorFinal);
            ticket.Add(mensaje);

            ticket.AddTitle("Ticket Abono");
            ticket.AddAuthor("PUDVE");
            ticket.Close();
            writer.Close();
        }

        private void ImprimirTicket(string idVenta, string idAbono)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.Verb = "print";
                info.FileName = @"C:\Archivos PUDVE\Ventas\Tickets\ticket_abono_" + idVenta + "_" + idAbono +".pdf";
                info.CreateNoWindow = true;
                info.WindowStyle = ProcessWindowStyle.Hidden;

                Process p = new Process();
                p.StartInfo = info;
                p.Start();

                p.WaitForInputIdle();
                System.Threading.Thread.Sleep(1000);

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
    }
}
