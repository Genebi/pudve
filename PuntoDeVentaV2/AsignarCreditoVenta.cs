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
        public AsignarCreditoVenta()
        {
            InitializeComponent();
        }

        private void AsignarCreditoVenta_Load(object sender, EventArgs e)
        {
            txtCantidad.KeyPress += new KeyPressEventHandler(SoloDecimales);
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

                DetalleVenta.credito = credito;

                this.Dispose();
            }
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }
    }
}
