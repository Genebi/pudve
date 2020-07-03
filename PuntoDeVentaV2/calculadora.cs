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
    public partial class calculadora : Form
    {
        double a = 0, b = 0;
        string c = "";

        public calculadora()
        {
            InitializeComponent();
        }

        private void calculadora_Load(object sender, EventArgs e)
        {
            tbCalculadora.Visible = false;
        }

        public void operaciones()
        {
            b = Convert.ToDouble(this.tbCalculadora.Text);
            switch (c)
            {
                case "+":
                    this.tbCalculadora.Text = Convert.ToString(a + b);
                    break;

                case "-":
                    this.tbCalculadora.Text = Convert.ToString(a - b);
                    break;

                case "*":
                    this.tbCalculadora.Text = Convert.ToString(a * b);
                    break;

                case "/":
                    this.tbCalculadora.Text = Convert.ToString(a / b);
                    break;
            }
        }

        private void btnResultado_Click(object sender, EventArgs e)
        {
            //b = Convert.ToDouble(this.tbCalculadora.Text);
            //switch (c)
            //{
            //    case "+":
            //        this.tbCalculadora.Text = Convert.ToString(a + b);
            //        break;

            //    case "-":
            //        this.tbCalculadora.Text = Convert.ToString(a - b);
            //        break;

            //    case "*":
            //        this.tbCalculadora.Text = Convert.ToString(a * b);
            //        break;

            //    case "/":
            //        this.tbCalculadora.Text = Convert.ToString(a / b);
            //        break;
            //}
        }
        //Botones del 0 al 9
        private void btn0_Click(object sender, EventArgs e)
        {
            if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "0";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "0";
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "1";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "1";
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "2";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "2";
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "3";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "3";
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "4";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "4";
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "5";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "5";
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "6";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "6";
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "7";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "7";
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "8";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "8";
            }
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "9";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "9";
            }
        }

        //Boton AC
        private void btnAC_Click(object sender, EventArgs e)
        {
            a = Convert.ToDouble(0);
            b = Convert.ToDouble(0);
            this.tbCalculadora.Text = "";
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            b = Convert.ToDouble(0);
            this.tbCalculadora.Text = "";
        }

        //Boton punto
        private void btnPunto_Click(object sender, EventArgs e)
        {
            if (this.tbCalculadora.Text.Contains('.') == false)
            {
                this.tbCalculadora.Text = this.tbCalculadora.Text + ".";
            }
        }

        //Botones de operaciones al dar click
        private void btnSumar_Click(object sender, EventArgs e)
        {
            a = Convert.ToDouble(this.lCalculadora.Text);
            c = "+";
            this.lCalculadora.Text = "";
            this.lCalculadora.Focus();
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            a = Convert.ToDouble(this.lCalculadora.Text);
            c = "-";
            this.lCalculadora.Text = "";
            this.lCalculadora.Focus();
        }

        private void btnMultiplicar_Click(object sender, EventArgs e)
        {
            a = Convert.ToDouble(this.lCalculadora.Text);
            c = "*";
            this.lCalculadora.Text = "";
            this.lCalculadora.Focus();
        }

        private void btnDividir_Click(object sender, EventArgs e)
        {
            //try pars
            a = Convert.ToDouble(this.lCalculadora.Text);
            c = "/";
            this.lCalculadora.Text = "";
            this.lCalculadora.Focus();
        }
        //TextBox
        private void tbCalculadora_KeyPress(object sender, KeyPressEventArgs e)
        {
            //No escribir letras
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            //Enter
            //if (e.KeyChar == Convert.ToChar(Keys.Enter))
            //{
            //    b = Convert.ToDouble(this.tbCalculadora.Text);
            //    switch (c)
            //    {
            //        case "+":
            //            this.tbCalculadora.Text = Convert.ToString(b + a);
            //            break;

            //        case "-":
            //            this.tbCalculadora.Text = Convert.ToString(a - b);
            //            break;

            //        case "*":
            //            this.tbCalculadora.Text = Convert.ToString(b * a);
            //            break;

            //        case "/":
            //            this.tbCalculadora.Text = Convert.ToString(a / b);
            //            break;
            //    }
            //}


        }

        private void calculadora_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 43)//SUMAR
            {
                btnSumar.PerformClick();
            }
            else if (e.KeyChar == 45)//RESTAR
            {
                btnRestar.PerformClick();
            }
            else if (e.KeyChar == 42)//MULTIPLICAR
            {
                btnMultiplicar.PerformClick();
            }
            else if (e.KeyChar == 47)//DIVIDIR
            {
                btnDividir.PerformClick();
            }
            else if(e.KeyChar == Convert.ToChar(Keys.Enter))//ENTER
            {
                operaciones();
            }
            else if (e.KeyChar == 27)//ESC
            {
                btnC.PerformClick();
            }
            
        }

        private void calculadora_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)//SUPRIMIR
            {
                btnAC.PerformClick();
            }
            //else if (e.KeyCode == Keys.Clear)//RETROCESO
            //{

            //}
        }

        private void lCalculadora_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           
        }

        private void cboResultado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))//ENTER
            {
                btnResultado.PerformClick();
            }
        }

    }
}
