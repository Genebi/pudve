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
    public partial class DetalleVenta : Form
    {
        public static string cliente = string.Empty;
        public static int idCliente = 0;
        public static float credito = 0f;

        private float total = 0;
        private float totalMetodos = 0;

        public DetalleVenta(float total)
        {
            InitializeComponent();

            this.total = total;
        }

        private void DetalleVenta_Load(object sender, EventArgs e)
        {
            txtTotalVenta.Text = "$" + total.ToString("0.00");

            txtEfectivo.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTarjeta.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtVales.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCheque.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTransferencia.KeyPress += new KeyPressEventHandler(SoloDecimales);

            txtTarjeta.KeyUp += new KeyEventHandler(SumaMetodosPago);
            txtVales.KeyUp += new KeyEventHandler(SumaMetodosPago);
            txtCheque.KeyUp += new KeyEventHandler(SumaMetodosPago);
            txtTransferencia.KeyUp += new KeyEventHandler(SumaMetodosPago);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Ventas.efectivo = CantidadDecimal(txtEfectivo.Text).ToString("0.00");
            Ventas.tarjeta = CantidadDecimal(txtTarjeta.Text).ToString("0.00");
            Ventas.vales = CantidadDecimal(txtVales.Text).ToString("0.00");
            Ventas.cheque = CantidadDecimal(txtCheque.Text).ToString("0.00");
            Ventas.transferencia = CantidadDecimal(txtTransferencia.Text).ToString("0.00");
            Ventas.referencia = txtReferencia.Text;
            
            if (idCliente == 0)
            {
                cliente = string.Empty;
            }

            Ventas.cliente = cliente;
            Ventas.idCliente = idCliente.ToString();
            Ventas.credito = credito.ToString();

            this.Dispose();
        }

        private void lbCliente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ListaClientes clientes = new ListaClientes();

            clientes.FormClosed += delegate
            {
                lbCliente.Text = cliente;

                if (string.IsNullOrWhiteSpace(cliente))
                {
                    lbCliente.Text = "Asignar cliente";
                    lbEliminarCliente.Visible = false;
                    idCliente = 0;
                }
                else
                {
                    lbEliminarCliente.Visible = true;
                }
            };

            clientes.ShowDialog();
        }

        private void btnCredito_Click(object sender, EventArgs e)
        {
            AsignarCreditoVenta agregarCredito = new AsignarCreditoVenta();

            agregarCredito.FormClosed += delegate
            {
                lbTotalCredito.Text = Convert.ToDouble(credito).ToString("0.00");
            };

            agregarCredito.ShowDialog();
        }

        //Obtiene el total de todos los metodos de pago excepto el de efectivo y lo usa para calcular
        //el cambio junto a otras cantidades de otros campos
        private void SumaMetodosPago(object sender, KeyEventArgs e)
        {
            float suma = SumaMetodos();

            //Si es mayor al total a pagar vuelve a calcular las cantidades pero sin tomar en cuenta
            //el campo que hizo que fuera mayor a la cantidad a pagar
            if (suma > total)
            {
                TextBox tb = sender as TextBox;

                tb.Text = string.Empty;

                suma = SumaMetodos();
            }

            totalMetodos = suma;

            CalcularCambio();
        }


        //Este metodo suma todas las cantidades de los campos de metodos de pago excepto el de efectivo
        private float SumaMetodos()
        {
            float tarjeta = CantidadDecimal(txtTarjeta.Text);
            float vales = CantidadDecimal(txtVales.Text);
            float cheque = CantidadDecimal(txtCheque.Text);
            float transferencia = CantidadDecimal(txtTransferencia.Text);

            float suma = tarjeta + vales + cheque + transferencia;

            return suma;
        }

        private float CantidadDecimal(string cantidad)
        {
            float resultado = 0f;

            if (string.IsNullOrWhiteSpace(cantidad))
            {
                resultado = 0;
            }
            else
            {
                resultado = float.Parse(cantidad);
            }

            return resultado;
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

        private void txtEfectivo_KeyUp(object sender, KeyEventArgs e)
        {
            CalcularCambio();
        }

        private void CalcularCambio()
        {
            double cambio = Convert.ToDouble((CantidadDecimal(txtEfectivo.Text) + totalMetodos) - total);

            lbTotalCambio.Text = "$" + cambio.ToString("0.00");
        }

        private void lbEliminarCliente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            idCliente = 0;
            lbCliente.Text = "Asignar cliente";
            lbEliminarCliente.Visible = false;
            credito = 0;
        }
    }
}
