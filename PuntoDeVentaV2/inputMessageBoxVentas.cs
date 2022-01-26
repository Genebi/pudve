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
        string validGuion = string.Empty;
        string validPunto = string.Empty;
        int conteoPunto, conteoGuion;

        public static string cantidad = string.Empty;
        public static string cantidadMayoraStock = string.Empty;

        public inputMessageBoxVentas(string _Prompt, string _Title, string _DefaultResponse)
        {
            InitializeComponent();
            this.promptMsg = _Prompt;
            this.titleWindow = _Title;
            this.strDefaultResponse = _DefaultResponse;
        } 

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Ventas.sonido = false;
            cantidad = "Cancelar";
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
             Ventas.sonido = true;
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

            var validarGuion = txtCantidad.Text;
            Boolean guion = validarGuion.Contains('-');
            if (guion == true)
            {
                validGuion = "guion";
            }
            else
            {
                validGuion = "Todo";
                conteoGuion = 0;
            }

            var validarPunto = txtCantidad.Text;
            Boolean punto = validarPunto.Contains('.');
            if (punto == true)
            {
                validPunto = "punto";
            }
            else
            {
                validPunto = "Todo";
                conteoPunto = 0;
            }

            ////////////////////////////////////Validacion de punto y guion/////////////////////////////////////////

            if (!txtCantidad.Text.Equals(string.Empty))
            {
                validGuion = "guion";
            }
            if (validGuion == "Todo" && validPunto == "Todo")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
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
            }

            else if(validPunto == "punto" && conteoPunto == 0)
            {
                if (!char.IsControl(e.KeyChar))
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
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
                conteoGuion = 1;
            }

            else if (validGuion == "guion" && conteoGuion == 0)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
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
                conteoPunto = 1;
            }

            if (validGuion == "guion" && validPunto == "punto")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
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
            }

        }

        private void inputMessageBoxVentas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                btnCancelar.PerformClick();
            }
        }

        private void txtCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            
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
