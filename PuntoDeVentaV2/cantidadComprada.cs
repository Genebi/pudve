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
        Conexion cn = new Conexion();
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
            Ventas.SeCambioCantidad = true;
            nuevaCantidad = Convert.ToDecimal(txtCantidad.Text);
            nuevaCantidadCambio = 1;
            this.Close();
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
        private void calculadora(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int calcu = 0;
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        if (calculadora.seEnvia.Equals(true))
                        {
                            txt.Text = calculadora.lCalculadora.Text;
                        }
                        calcu = 0;
                    };
                    if (!calculadora.Visible)
                    {
                        calculadora.Show();
                    }
                    else
                    {
                        calculadora.Show();
                    }
                }
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
            var dato = cn.CargarDatos($"SELECT FormatoDeVenta FROM productos WHERE CodigoBarras = '{Ventas.codBarras}' AND IDUSuario = '{FormPrincipal.userID}' AND Status = '1'");
            var estado = dato.Rows[0]["FormatoDeVenta"].ToString();

            if (estado == "1")
            {
                if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) || (e.KeyChar == '.'))
                {
                    MessageBox.Show("Este producto se vende solo por unidades enteras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Handled = true;
                    return;
                }
            }
           
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
