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
    public partial class CantidadRegresada : Form
    {
        public CantidadRegresada()
        {
            InitializeComponent();
        }

        private void CantidadRegresada_Load(object sender, EventArgs e)
        {
            lblNombreProducto.Text = AjustarProducto.nombreDePorducto;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Favor de ingresar una cantidad valida", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Convert.ToDecimal(txtCantidad.Text) <= 0)
            {
                MessageBox.Show("Favor de ingresar una cantidad mayor a 0", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                AjustarProducto.cantidadRegresada = Convert.ToDecimal(txtCantidad.Text);
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            AjustarProducto.cantidadRegresada = 0;
            this.Close();
            return;
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // solo 1 punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                btnAceptar.PerformClick();
            }
        }
    }
}
