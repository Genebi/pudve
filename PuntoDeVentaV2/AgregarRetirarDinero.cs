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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class AgregarRetirarDinero : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        // 0 = Depositar
        // 1 = Retirar
        // 2 = Corte
        int operacion = 0;

        private float totalEfectivo = 0f;
        private float totalTarjeta = 0f;
        private float totalVales = 0f;
        private float totalCheque = 0f;
        private float totalTransferencia = 0f;
        private float totalCredito = 0f;

        public AgregarRetirarDinero(int operacion = 0)
        {
            InitializeComponent();

            this.operacion = operacion;
        }

        private void AgregarRetirarDinero_Load(object sender, EventArgs e)
        {
            if (operacion == 0)
            {
                lbTitulo.Text = "Cantidad a depositar";
                lbSubtitulo.Text = "Concepto del depósito";
                lbCredito.Visible = false;
                txtCredito.Visible = false;
            }
            else if (operacion == 1)
            {
                lbTitulo.Text = "Cantidad a retirar";
                lbSubtitulo.Text = "Concepto del retiro";
            }
            else if (operacion == 2)
            {
                lbTitulo.Text = "Cantidad a retirar";
                lbSubtitulo.Text = "Concepto del retiro";
                //btnCancelar.Text = "Corte sin retiro";
            }

            txtEfectivo.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCredito.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTarjeta.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCheque.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTrans.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtVales.KeyPress += new KeyPressEventHandler(SoloDecimales);

            // NOTA: Se le suma saldo inicial a efectivo porque se tiene que guardar el valor y anteriormente
            // se tomaba en cuenta el saldo inicial en el apartado de totales del apartado Ventas
            totalEfectivo = CajaN.totalEfectivo + CajaN.saldoInicial;
            totalTarjeta = CajaN.totalTarjeta;
            totalVales = CajaN.totalVales;
            totalCheque = CajaN.totalCheque;
            totalTransferencia = CajaN.totalTransferencia;
            totalCredito = CajaN.totalCredito;
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Solo se ejecuta cuando es Corte de caja
            /*if (operacion == 2)
            {
                string fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                CajaN.fechaUltimoCorte = Convert.ToDateTime(fechaOperacion);

                //totalEfectivo -= CajaN.retiroEfectivo;
                //totalTarjeta -= CajaN.retiroTarjeta;
                //totalVales -= CajaN.retiroVales;
                //totalCheque -= CajaN.retiroCheque;
                //totalTransferencia -= CajaN.retiroTrans;

                if (totalEfectivo <= 0) { totalEfectivo = 0; }
                if (totalTarjeta <= 0) { totalTarjeta = 0; }
                if (totalVales <= 0) { totalVales = 0; }
                if (totalCheque <= 0) { totalCheque = 0; }
                if (totalTransferencia <= 0) { totalTransferencia = 0; }


                var cantidad = totalEfectivo + totalTarjeta + totalVales + totalCheque + totalTransferencia + totalCredito;

                string[] datos = new string[] {
                    "corte", cantidad.ToString("0.00"), "0", "sin retiro", fechaOperacion, FormPrincipal.userID.ToString(),
                    totalEfectivo.ToString("0.00"), totalTarjeta.ToString("0.00"), totalVales.ToString("0.00"), totalCheque.ToString("0.00"),
                    totalTransferencia.ToString("0.00"), totalCredito.ToString("0.00"), "0"
                };

                int resultado = cn.EjecutarConsulta(cs.OperacionCaja(datos));

                if (resultado > 0)
                {
                    // Se pausa por 1 segundo
                    Thread.Sleep(1000);

                    fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    datos = new string[] {
                        "venta", cantidad.ToString("0.00"), "0", "sin retiro", fechaOperacion, FormPrincipal.userID.ToString(),
                        totalEfectivo.ToString("0.00"), totalTarjeta.ToString("0.00"), totalVales.ToString("0.00"), totalCheque.ToString("0.00"),
                        totalTransferencia.ToString("0.00"), totalCredito.ToString("0.00"), "0"
                    };

                    cn.EjecutarConsulta(cs.OperacionCaja(datos));
                }

                CajaN.botones = true;
            }*/

            Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var tipoOperacion = string.Empty;

            // Depositar
            if (operacion == 0)
            {
                tipoOperacion = "deposito";
            }

            // Retirar
            if (operacion == 1)
            {
                tipoOperacion = "retiro";
            }
            
            // Corte de caja
            if (operacion == 2)
            {
                tipoOperacion = "corte";
            }

            var concepto = txtConcepto.Text;
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            CajaN.fechaUltimoCorte = Convert.ToDateTime(fechaOperacion);

            var efectivo = ValidarCampos(txtEfectivo.Text);
            var tarjeta = ValidarCampos(txtTarjeta.Text);
            var cheque = ValidarCampos(txtCheque.Text);
            var vales = ValidarCampos(txtVales.Text);
            var trans = ValidarCampos(txtTrans.Text);
            var credito = ValidarCampos(txtCredito.Text);

            // Se guardan las cantidades que el usuario es lo que va a retirar
            var cantidad = efectivo + tarjeta + cheque + vales + trans + credito;

            // Si es igual a cero no procede la operacion de depositar o retirar
            if (cantidad == 0)
            {
                if (operacion == 0 || operacion == 1)
                {
                    var mensaje = string.Empty;

                    if (operacion == 1)
                    {
                        mensaje = "La cantidad a retirar debe ser mayor a cero";
                    }
                    else if (operacion == 0)
                    {
                        mensaje = "La cantidad a depositar debe ser mayor a cero";
                    }

                    MessageBox.Show(mensaje, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }


            string[] datos = new string[] {
                tipoOperacion, cantidad.ToString("0.00"), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(),
                efectivo.ToString("0.00"), tarjeta.ToString("0.00"), vales.ToString("0.00"), cheque.ToString("0.00"),
                trans.ToString("0.00"), credito.ToString("0.00"), "0"
            };

            int resultado = cn.EjecutarConsulta(cs.OperacionCaja(datos));

            if (resultado > 0)
            {
                // Para generar Ticket al depositar dinero
                if (operacion == 0)
                {
                    GenerarTicket(datos);
                }

                // Para generar Ticket al retirar dinero
                if (operacion == 1)
                {
                    GenerarTicket(datos);
                }

                // Corte
                if (operacion == 2)
                {
                    // Se pausa por 1 segundo
                    Thread.Sleep(1000);

                    // Solo cuando es corte se hace esta resta, al total de cada forma de pago
                    // se le resta lo que el usuario quiere retirar menos el total retirado de cada
                    // forma de pago antes de que se haga el corte de caja

                    efectivo = totalEfectivo - efectivo;// - CajaN.retiroEfectivo;
                    tarjeta = totalTarjeta - tarjeta;// - CajaN.retiroTarjeta;
                    cheque = totalCheque - cheque;// - CajaN.retiroCheque;
                    vales = totalVales - vales;// - CajaN.retiroVales;
                    trans = totalTransferencia - trans;// - CajaN.retiroTrans;
                    credito = totalCredito - credito;

                    cantidad = efectivo + tarjeta + cheque + vales + trans + credito;

                    fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    datos = new string[] {
                        "venta", cantidad.ToString("0.00"), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(),
                        efectivo.ToString("0.00"), tarjeta.ToString("0.00"), vales.ToString("0.00"), cheque.ToString("0.00"),
                        trans.ToString("0.00"), credito.ToString("0.00"), "0"
                    };

                    cn.EjecutarConsulta(cs.OperacionCaja(datos));

                    CajaN.botones = true;
                }
                
                Dispose();
            }
        }

        private void GenerarTicket(string[] info)
        {
            
            var datos = FormPrincipal.datosUsuario;

            // tipo 0 = Agregar dinero
            // tipo 1 = Retirar dinero

            //Medidas de ticket de 57 y 80 mm
            //57mm = 161.28 pt
            //80mm = 226.08 pt

            var tipoPapel = 57;
            var anchoPapel = Convert.ToInt32(Math.Floor((((tipoPapel * 0.10) * 72) / 2.54)));
            var altoPapel = Convert.ToInt32(anchoPapel + 54);

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
                medidaFuenteMensaje = 10;
                medidaFuenteGrande = 10;
                medidaFuenteNegrita = 8;
                medidaFuenteNormal = 8;
            }
            else if (tipoPapel == 57)
            {
                medidaFuenteMensaje = 6;
                medidaFuenteGrande = 8;
                medidaFuenteNegrita = 6;
                medidaFuenteNormal = 6;
            }

            // Ruta donde se creara el archivo PDF
            var rutaArchivo = @"C:\Archivos PUDVE\Reportes\Caja\ticket_caja_" + Convert.ToDateTime(info[4]).ToString("yyyyMMddHHmmss") + ".pdf";

            Document ticket = new Document(new iTextSharp.text.Rectangle(anchoPapel, altoPapel), 3, 3, 5, 0);
            PdfWriter writer = PdfWriter.GetInstance(ticket, new FileStream(rutaArchivo, FileMode.Create));

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteNormal);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, medidaFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteGrande);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteMensaje);

            ticket.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
            Paragraph subTitulo = new Paragraph("Ticket " + info[0].ToUpper() + "\nFecha: " + info[4] + "\nEmpleado: ADMIN", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;

            string contenido = $"Empleado: ADMIN\nEfectivo: ${info[6]}\nTarjeta: ${info[7]}\nVales: ${info[8]}\nCheque: ${info[9]}\nTransferencia: ${info[10]}";
            Paragraph cuerpo = new Paragraph(contenido, fuenteNormal);
            cuerpo.Alignment = Element.ALIGN_CENTER;

            float[] anchoColumnas = new float[] { 20f, 20f };

            PdfPTable tabla = new PdfPTable(2);
            tabla.WidthPercentage = 100;
            tabla.SetWidths(anchoColumnas);

            //======================================================================
            PdfPCell colEfectivo = new PdfPCell(new Phrase("Efectivo", fuenteNormal));
            colEfectivo.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivo.BorderWidth = 0;

            PdfPCell colEfectivoC = new PdfPCell(new Phrase($"${info[6]}", fuenteNormal));
            colEfectivoC.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoC.BorderWidth = 0;

            PdfPCell colTarjeta = new PdfPCell(new Phrase("Tarjeta", fuenteNormal));
            colTarjeta.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjeta.BorderWidth = 0;

            PdfPCell colTarjetaC = new PdfPCell(new Phrase($"${info[7]}", fuenteNormal));
            colTarjetaC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaC.BorderWidth = 0;

            PdfPCell colVales = new PdfPCell(new Phrase("Vales", fuenteNormal));
            colVales.HorizontalAlignment = Element.ALIGN_CENTER;
            colVales.BorderWidth = 0;

            PdfPCell colValesC = new PdfPCell(new Phrase($"${info[8]}", fuenteNormal));
            colValesC.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesC.BorderWidth = 0;

            PdfPCell colCheque = new PdfPCell(new Phrase("Cheque", fuenteNormal));
            colCheque.HorizontalAlignment = Element.ALIGN_CENTER;
            colCheque.BorderWidth = 0;

            PdfPCell colChequeC = new PdfPCell(new Phrase($"${info[9]}", fuenteNormal));
            colChequeC.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeC.BorderWidth = 0;

            PdfPCell colTrans = new PdfPCell(new Phrase("Transferencia", fuenteNormal));
            colTrans.HorizontalAlignment = Element.ALIGN_CENTER;
            colTrans.BorderWidth = 0;

            PdfPCell colTransC = new PdfPCell(new Phrase($"${info[10]}", fuenteNormal));
            colTransC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransC.BorderWidth = 0;

            PdfPCell colCredito = new PdfPCell(new Phrase("Crédito", fuenteNormal));
            colCredito.HorizontalAlignment = Element.ALIGN_CENTER;
            colCredito.BorderWidth = 0;

            PdfPCell colCreditoC = new PdfPCell(new Phrase($"${info[11]}", fuenteNormal));
            colCreditoC.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoC.BorderWidth = 0;

            tabla.AddCell(colEfectivo);
            tabla.AddCell(colEfectivoC);
            tabla.AddCell(colTarjeta);
            tabla.AddCell(colTarjetaC);
            tabla.AddCell(colVales);
            tabla.AddCell(colValesC);
            tabla.AddCell(colCheque);
            tabla.AddCell(colChequeC);
            tabla.AddCell(colTrans);
            tabla.AddCell(colTransC);
            tabla.AddCell(colCredito);
            tabla.AddCell(colCreditoC);

            //======================================================================

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));
            linea.SetLeading(0.5F, 0.5F);

            Paragraph concepto = new Paragraph("CONCEPTO", fuenteNegrita);
            concepto.Alignment = Element.ALIGN_CENTER;

            Paragraph conceptoExtra = new Paragraph(info[3], fuenteNormal);
            conceptoExtra.Alignment = Element.ALIGN_CENTER;

            ticket.Add(titulo);
            ticket.Add(subTitulo);
            ticket.Add(linea);
            ticket.Add(tabla);
            ticket.Add(linea);

            if (!string.IsNullOrWhiteSpace(info[3]))
            {
                ticket.Add(concepto);
                ticket.Add(conceptoExtra);
            }

            ticket.AddTitle("Ticket Corte Caja");
            ticket.AddAuthor("PUDVE");
            ticket.Close();
            writer.Close();

            VisualizadorTickets vt = new VisualizadorTickets(rutaArchivo, rutaArchivo);
            vt.ShowDialog();
        }

        private float ValidarCampos(string cantidad)
        {
            float valor = 0f;

            if (!string.IsNullOrWhiteSpace(cantidad))
            {
                valor = float.Parse(cantidad);
            }

            return valor;
        }

        private void MensajeCantidad(float cantidad, object tb)
        {
            TextBox campo = tb as TextBox;

            MessageBox.Show("La cantidad a retirar no puede ser mayor a $" + cantidad.ToString("N2"), "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            campo.Text = cantidad.ToString();
            campo.SelectionStart = campo.Text.Length;
            campo.SelectionLength = 0;
        }

        private void txtEfectivo_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEfectivo.Text))
            {
                float efectivo = float.Parse(txtEfectivo.Text);

                if (efectivo > totalEfectivo && operacion > 0)
                {
                    MensajeCantidad(totalEfectivo, sender);
                }
            }
        }

        private void txtTarjeta_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTarjeta.Text))
            {
                float tarjeta = float.Parse(txtTarjeta.Text);

                if (tarjeta > totalTarjeta && operacion > 0)
                {
                    MensajeCantidad(totalTarjeta, sender);
                }
            }
        }

        private void txtVales_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtVales.Text))
            {
                float vales = float.Parse(txtVales.Text);

                if (vales > totalVales && operacion > 0)
                {
                    MensajeCantidad(totalVales, sender);
                }
            }
        }

        private void txtCheque_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCheque.Text))
            {
                float cheque = float.Parse(txtCheque.Text);

                if (cheque > totalCheque && operacion > 0)
                {
                    MensajeCantidad(totalCheque, sender);
                }
            }
        }

        private void txtTrans_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTrans.Text))
            {
                float trans = float.Parse(txtTrans.Text);

                if (trans > totalTransferencia && operacion > 0)
                {
                    MensajeCantidad(totalTransferencia, sender);
                }
            }
        }

        private void txtCredito_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCredito.Text))
            {
                float credito = float.Parse(txtCredito.Text);

                if (credito > totalCredito && operacion > 0)
                {
                    MensajeCantidad(totalCredito, sender);
                }
            }
        }
    }
}
