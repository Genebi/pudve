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

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
        }
        private void validarSoloNumeros(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string texto = txt.Text;
            bool esNum = decimal.TryParse(texto, out decimal algo);
            if (esNum.Equals(false))
            {
                txt.Text = "";
            }
        }

        private void cantidadComprada_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4)
            {
                e.Handled = true;
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))

            {
                e.Handled = true;

                return;
            }
        }
    }
}
