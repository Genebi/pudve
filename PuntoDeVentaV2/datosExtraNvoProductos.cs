using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class datosExtraNvoProductos : Form
    {
        public float importe, descuento;
        public int Cantidad;

        public datosExtraNvoProductos()
        {
            InitializeComponent();
        }

        private void datosExtraNvoProductos_Load(object sender, EventArgs e)
        {
            //limpiarCampos();
            txtImporteProdNvo.Select(txtImporteProdNvo.Text.Length, 0);
        }

        private void limpiarCampos()
        {
            txtImporteProdNvo.Text = "0";
            txtDescuentoProdNvo.Text = "0";
            txtCantidadProdNvo.Text = "0";
        }

        private void txtImporteProdNvo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar que la tecla presionada no sea CTRL u otra tecla no numerica
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // Si deseas, puedes permitir numeros decimales (o float)
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtImporteProdNvo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string texto = txtImporteProdNvo.Text;
                importe = 0;
                if (texto != "" || texto == "0")
                {
                    importe = (float)Convert.ToDouble(texto);
                }
                else
                {
                    importe = -1;
                }

                if (importe >= 0)
                {
                    Productos.ImporteDatoExtraFinal = importe;
                    txtDescuentoProdNvo.Focus();
                    txtDescuentoProdNvo.Select(txtDescuentoProdNvo.Text.Length, 0);
                }
                else
                {
                    MessageBox.Show("Solo se aceptan numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtImporteProdNvo_Leave(object sender, EventArgs e)
        {
            string texto = txtImporteProdNvo.Text;
            importe = 0;
            if (texto != "" || texto == "0")
            {
                importe = (float)Convert.ToDouble(texto);
            }
            else
            {
                importe = -1;
            }

            if (importe >= 0)
            {
                Productos.ImporteDatoExtraFinal = importe;
                txtDescuentoProdNvo.Focus();
                txtDescuentoProdNvo.Select(txtDescuentoProdNvo.Text.Length, 0);
            }
            else
            {
                MessageBox.Show("Solo se aceptan numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDescuentoProdNvo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar que la tecla presionada no sea CTRL u otra tecla no numerica
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // Si deseas, puedes permitir numeros decimales (o float)
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtDescuentoProdNvo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string texto = txtDescuentoProdNvo.Text;
                descuento = 0;
                if (texto != "" || texto == "0")
                {
                    descuento = (float)Convert.ToDouble(texto);
                }
                else
                {
                    descuento = -1;
                }

                if (descuento >= 0)
                {
                    Productos.DescuentoDatoExtraFinal = descuento;
                    txtCantidadProdNvo.Focus();
                    txtCantidadProdNvo.Select(txtDescuentoProdNvo.Text.Length, 0);
                }
                else
                {
                    MessageBox.Show("Solo se aceptan numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtDescuentoProdNvo_Leave(object sender, EventArgs e)
        {
            string texto = txtDescuentoProdNvo.Text;
            descuento = 0;
            if (texto != "" || texto == "0")
            {
                descuento = (float)Convert.ToDouble(texto);
            }
            else
            {
                descuento = -1;
            }

            if (descuento >= 0)
            {
                Productos.DescuentoDatoExtraFinal = descuento;
                txtCantidadProdNvo.Focus();
                txtCantidadProdNvo.Select(txtDescuentoProdNvo.Text.Length, 0);
            }
            else
            {
                MessageBox.Show("Solo se aceptan numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCantidadProdNvo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtCantidadProdNvo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string texto = txtCantidadProdNvo.Text;
                Cantidad = 0;
                if (texto != "" || texto == "0")
                {
                    Cantidad = Convert.ToInt32(texto);
                }
                else
                {
                    Cantidad = -1;
                }

                if (Cantidad >= 0)
                {
                    Productos.CantidadDatoExtraFinal = Cantidad;
                    limpiarCampos();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Solo se aceptan numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtCantidadProdNvo_Leave(object sender, EventArgs e)
        {
            string texto = txtCantidadProdNvo.Text;
            Cantidad = 0;
            if (texto != "" || texto == "0")
            {
                Cantidad = Convert.ToInt32(texto);
            }
            else
            {
                Cantidad = -1;
            }

            if (Cantidad >= 0)
            {
                Productos.CantidadDatoExtraFinal = Cantidad;
                limpiarCampos();
                this.Close();
            }
            else
            {
                MessageBox.Show("Solo se aceptan numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
