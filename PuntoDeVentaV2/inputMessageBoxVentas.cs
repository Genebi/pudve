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
    public partial class inputMessageBoxVentas : Form
    {
        string promptMsg = string.Empty, 
                titleWindow = string.Empty,
                strDefaultResponse = string.Empty;

        public static string cantidad = string.Empty;

        public inputMessageBoxVentas(string _Prompt, string _Title, string _DefaultResponse)
        {
            InitializeComponent();
            this.promptMsg = _Prompt;
            this.titleWindow = _Title;
            this.strDefaultResponse = _DefaultResponse;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cantidad = "Cancelar";
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
             cantidad = txtCantidad.Text;
             this.Close();
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //Si deseas, puedes permitir numeros decimales (o float)
            //If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void inputMessageBoxVentas_Load(object sender, EventArgs e)
        {
            cargarValores();
        }

        private void cargarValores()
        {
            lblPrompt.Text = promptMsg;
            this.Text = titleWindow;
            txtCantidad.Text = strDefaultResponse;
            txtCantidad.Focus();
            txtCantidad.Select();
        }
    }
}
