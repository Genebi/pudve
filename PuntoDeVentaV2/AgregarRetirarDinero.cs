using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                btnCancelar.Text = "Corte sin retiro";
            }

            txtEfectivo.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCredito.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTarjeta.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCheque.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTrans.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtVales.KeyPress += new KeyPressEventHandler(SoloDecimales);

            totalEfectivo = CajaN.totalEfectivo;
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
            if (operacion == 2)
            {
                string fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                CajaN.fechaCorte = Convert.ToDateTime(fechaOperacion);

                totalEfectivo -= CajaN.retiroEfectivo;
                totalTarjeta -= CajaN.retiroTarjeta;
                totalVales -= CajaN.retiroVales;
                totalCheque -= CajaN.retiroCheque;
                totalTransferencia -= CajaN.retiroTrans;


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
            }

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

            if (operacion == 2)
            {
                tipoOperacion = "corte";
            }

            var concepto = txtConcepto.Text;
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            CajaN.fechaCorte = Convert.ToDateTime(fechaOperacion);

            var efectivo = ValidarCampos(txtEfectivo.Text);
            var tarjeta = ValidarCampos(txtTarjeta.Text);
            var cheque = ValidarCampos(txtCheque.Text);
            var vales = ValidarCampos(txtVales.Text);
            var trans = ValidarCampos(txtTrans.Text);
            var credito = ValidarCampos(txtCredito.Text);

            // Se guardan las cantidades que el usuario es lo que va a retirar
            var cantidad = efectivo + tarjeta + cheque + vales + trans + credito;

            string[] datos = new string[] {
                tipoOperacion, cantidad.ToString("0.00"), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(),
                efectivo.ToString("0.00"), tarjeta.ToString("0.00"), vales.ToString("0.00"), cheque.ToString("0.00"),
                trans.ToString("0.00"), credito.ToString("0.00"), "0"
            };

            int resultado = cn.EjecutarConsulta(cs.OperacionCaja(datos));

            if (resultado > 0)
            {
                // Corte
                if (operacion == 2)
                {
                    // Se pausa por 1 segundo
                    Thread.Sleep(1000);

                    // Solo cuando es corte se hace esta resta, al total de cada forma de pago
                    // se le resta lo que el usuario quiere retirar menos el total retirado de cada
                    // forma de pago antes de que se haga el corte de caja
                    efectivo = totalEfectivo - efectivo - CajaN.retiroEfectivo;
                    tarjeta = totalTarjeta - tarjeta - CajaN.retiroTarjeta;
                    cheque = totalCheque - cheque - CajaN.retiroCheque;
                    vales = totalVales - vales - CajaN.retiroVales;
                    trans = totalTransferencia - trans - CajaN.retiroTrans;

                    cantidad = efectivo + tarjeta + cheque + vales + trans + credito;

                    fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    datos = new string[] {
                        "venta", cantidad.ToString("0.00"), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(),
                        efectivo.ToString("0.00"), tarjeta.ToString("0.00"), vales.ToString("0.00"), cheque.ToString("0.00"),
                        trans.ToString("0.00"), credito.ToString("0.00"), "0"
                    };

                    cn.EjecutarConsulta(cs.OperacionCaja(datos)); 
                }
                
                Dispose();
            }
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

            MessageBox.Show("La cantidad a retirar no puede ser mayor a $" + cantidad, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
