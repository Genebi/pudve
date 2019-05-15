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
    public partial class CantidadProdServicio : Form
    {
        //AgregarEditarProducto addEditProd = new AgregarEditarProducto("");

        public CantidadProdServicio()
        {
            InitializeComponent();
        }

        private void txtBoxCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string texto = txtBoxCantidad.Text;
                int cantidad = 0;
                if (texto != "")
                {
                    cantidad = Convert.ToInt32(texto);
                }
                else
                {
                    cantidad = -1;
                }
                
                if (cantidad >= 0)
                {
                    //GenerarTextBox();
                    //MessageBox.Show(texto, "Mensaje");
                    //addEditProd.CantidadProdServicio = texto;
                    AgregarEditarProducto.CantProdServFinal = cantidad.ToString();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Solo se aceptan numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBoxCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
