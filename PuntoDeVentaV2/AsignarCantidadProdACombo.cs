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
        public string ListProd;
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
                ListProd = ListaProductos.idProdPintar.ToString();
                this.Close();
            }
            else
            {
                cancelar = 1;
                MessageBox.Show("Favor de ingresar una cantidad","Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            txtCantidad.Focus();
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

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (txtCantidad.Text.Equals(""))
            {
                txtCantidad.Text = "1";
                txtCantidad.SelectAll();
                txtCantidad.Focus();
            }
            else if (Convert.ToDecimal(txtCantidad.Text)<1)
            {
                txtCantidad.Text = "1";
                txtCantidad.SelectAll();
                txtCantidad.Focus();
            }
            
        }

        private void AsignarCantidadProdACombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
