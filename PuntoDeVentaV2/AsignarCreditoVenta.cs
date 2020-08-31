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
    public partial class AsignarCreditoVenta : Form
    {
        private float total = 0f;
        private float metodos = 0f;

        public static string cliente = string.Empty;
        public static int idCliente = 0;

        public AsignarCreditoVenta(float total, float metodos)
        {
            InitializeComponent();

            this.total = total;
            this.metodos = metodos;
        }

        private void AsignarCreditoVenta_Load(object sender, EventArgs e)
        {
            txtCantidad.Text = total.ToString("0.00");
            txtCantidad.SelectionStart = txtCantidad.Text.Length;
            txtCantidad.SelectionLength = 0;
            txtCantidad.Focus();

            txtCantidad.KeyPress += new KeyPressEventHandler(SoloDecimales);

            if (!string.IsNullOrWhiteSpace(DetalleVenta.cliente))
            {
                lbCliente.Text = DetalleVenta.cliente;
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DetalleVenta.cliente))
            {
                var respuesta = MessageBox.Show("Para realizar esta operación y agregar crédito\nes necesario asignar un cliente", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.OK)
                {
                    DetalleVenta.credito = 0;

                    this.Dispose();
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtCantidad.Text))
                {
                    return;
                }

                float credito = float.Parse(txtCantidad.Text);

                if ((credito + metodos) > total)
                {
                    var respuesta = MessageBox.Show("La suma de las cantidades de cada forma de pago y \nla cantidad a crédito es mayor al total de la venta", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    if (respuesta == DialogResult.OK)
                    {
                        txtCantidad.Text = string.Empty;
                        txtCantidad.Focus();
                    }

                    return;
                }

                DetalleVenta.credito = credito;

                this.Close();
            }
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void lbCliente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ListaClientes clientes = new ListaClientes(tipo: 2);

            clientes.FormClosed += delegate
            {
                lbCliente.Text = cliente;

                if (string.IsNullOrWhiteSpace(cliente))
                {
                    lbCliente.Text = "Asignar cliente";
                    idCliente = 0;
                }
                else
                {
                    DetalleVenta.idCliente = idCliente;
                    DetalleVenta.cliente = cliente;
                }
            };

            clientes.ShowDialog();
        }
    }
}
