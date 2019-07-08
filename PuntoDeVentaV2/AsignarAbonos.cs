using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                totalEfectivo = totalPendiente - SumaMetodos();
            }
            else
            {
                totalEfectivo = CantidadDecimal(txtEfectivo.Text);
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
    }
}
