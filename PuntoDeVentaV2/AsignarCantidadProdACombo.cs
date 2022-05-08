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
    public partial class AsignarCantidadProdACombo : Form
    {
        decimal cantidadDeProducto = 0;
        public decimal cantidadAsigarAlCombo;
        public int cancelar = 1;
        public AsignarCantidadProdACombo()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            cancelar = 0;
            if (!string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                cantidadDeProducto = Convert.ToDecimal(txtCantidad.Text);
                cantidadAsigarAlCombo = decimal.Round(cantidadDeProducto, 2);
                this.Close();
            }
            else
            {
                MessageBox.Show("Favor de ingresar una cantidad");
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cancelar = 1;
            this.Close();
        }

        private void AsignarCantidadProdACombo_Load(object sender, EventArgs e)
        {
            txtCantidad.SelectAll();

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
        }
    }
}
