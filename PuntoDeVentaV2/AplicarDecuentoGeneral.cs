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
    public partial class AplicarDecuentoGeneral : Form
    {
        decimal Total, Porcentaje,cantidad, Resultado;
        int calcu = 0;

        private void txtPorcentaje_Enter(object sender, EventArgs e)
        {
            txtCantidad.Clear();
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                cantidad = 0;
            }
            else
            {
                if (txtCantidad.Text == ".")
                {
                    txtCantidad.Text = "0.";
                    cantidad = Convert.ToDecimal(txtCantidad.Text);
                    txtCantidad.Select(txtCantidad.Text.Length, 0);
                }
                else
                {
                    cantidad = Convert.ToDecimal(txtCantidad.Text);
                }
                
            }

            Porcentaje = (cantidad * 100) / Total;
            lbTotalDescuento.Text = cantidad.ToString();
            lbTotalFinal.Text = (Total - cantidad).ToString();

        }

        private void txtCantidad_Enter(object sender, EventArgs e)
        {
            txtPorcentaje.Clear();
        }

        private void txtPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
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
                            txtPorcentaje.Text = calculadora.lCalculadora.Text;
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

                    //if ()
                    //{
                    //    txtStockMaximo.Text = calculadora.lCalculadora.Text;
                    //}
                }
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
                            txtCantidad.Text = calculadora.lCalculadora.Text;
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

                    //if ()
                    //{
                    //    txtStockMaximo.Text = calculadora.lCalculadora.Text;
                    //}
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPorcentaje.Text) && !string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                Ventas.PorcentajeDescuento = Porcentaje.ToString();
            }
            else
            {
                MessageBox.Show("No se aplicara ningun descuento","Aviso del Sistema",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Ventas.PorcentajeDescuento = "0";
            this.Close();
        }

        private void txtPorcentaje_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtPorcentaje.Text)>99)
            {
                txtPorcentaje.Text = "99";
            }
            if (string.IsNullOrWhiteSpace(txtPorcentaje.Text))
            {
                Porcentaje = 0;
            }
            else
            {
                if (txtPorcentaje.Text == ".")
                {
                    txtPorcentaje.Text = "0.";
                    Porcentaje = Convert.ToDecimal(txtPorcentaje.Text);
                    txtPorcentaje.Select(txtPorcentaje.Text.Length, 0);
                }
                else
                {
                    Porcentaje = Convert.ToDecimal(txtPorcentaje.Text);
                }
            }
            decimal resulta = (Porcentaje * Total) / 100;
            lbTotalDescuento.Text = resulta.ToString();
            lbTotalFinal.Text = (Total - resulta).ToString();
        }

        public AplicarDecuentoGeneral(string Precio)
        {
            InitializeComponent();
            this.Total = Convert.ToDecimal(Precio);
        }

        private void AplicarDecuentoGeneral_Load(object sender, EventArgs e)
        {
            lbPrecio.Text = "Precio Total: $" + Total.ToString();
            lbTotalFinal.Text = Total.ToString();
        }
    }
}
