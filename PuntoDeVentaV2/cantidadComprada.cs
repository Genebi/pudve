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
    public partial class cantidadComprada : Form
    {
         
        public static decimal nuevaCantidad;
        public static int nuevaCantidadCambio = 0;
        decimal cantidadAnterior = 0;
        public cantidadComprada()
        {
            InitializeComponent();
        }

        private void cantidadComprada_Load(object sender, EventArgs e)
        {
            txtCantidad.Text = Convert.ToString(Ventas.cantidadAnterior);
            cantidadAnterior = Ventas.cantidadAnterior;
            lblNombreProducto.Text = Ventas.nombreprodCantidad;
            nuevaCantidadCambio = 0;
            txtCantidad.SelectAll();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtCantidad.Text = cantidadAnterior.ToString();
            btnAceptar.PerformClick();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Favor de ingresar un valor");
                return;
            }
            Ventas.SeCambioCantidad = true;
            nuevaCantidad = Convert.ToDecimal(txtCantidad.Text);
            nuevaCantidadCambio = 1;
            this.Close();
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
            if (e.KeyData == Keys.Escape)
            {
                btnCancelar.PerformClick();
            }
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
