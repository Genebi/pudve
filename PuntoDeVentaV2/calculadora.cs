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
        string c = "", result="";
        

        public calculadora()
        {
            InitializeComponent();
        }

        private void calculadora_Load(object sender, EventArgs e)
        {

        }

        public void operaciones()
        {
            b = Convert.ToDouble(this.lCalculadora.Text);
            switch (c)
            {
                case "+":
                    this.lCalculadora.Text = Convert.ToString(a + b);
                    break;

                case "-":
                    this.lCalculadora.Text = Convert.ToString(a - b);
                    break;

                case "*":
                    this.lCalculadora.Text = Convert.ToString(a * b);
                    break;

                case "/":
                    this.lCalculadora.Text = Convert.ToString(a / b);
                    break;
            }
        }

        private void btnResultado_Click(object sender, EventArgs e)
        {
            b = Convert.ToDouble(this.lCalculadora.Text);
            switch (c)
            {
                case "+":
                    this.lCalculadora.Text = Convert.ToString(a + b);
                    break;

                case "-":
                    this.lCalculadora.Text = Convert.ToString(a - b);
                    break;

                case "*":
                    this.lCalculadora.Text = Convert.ToString(a * b);
                    break;

                case "/":
                    this.lCalculadora.Text = Convert.ToString(a / b);
                    break;
            }
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
            this.lCalculadora.Text = "";
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            b = Convert.ToDouble(0);
            this.lCalculadora.Text = "";
        }

        //Boton punto
        private void btnPunto_Click(object sender, EventArgs e)
        {
            if (this.lCalculadora.Text.Contains('.') == false)
            {
                this.lCalculadora.Text = this.lCalculadora.Text + ".";
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

        private void btnResultado_Click_1(object sender, EventArgs e)
        {
            b = Convert.ToDouble(this.lCalculadora.Text);
            switch (c)
            {
                case "+":
                    this.lCalculadora.Text = Convert.ToString(a + b);
                    break;

                case "-":
                    this.lCalculadora.Text = Convert.ToString(a - b);
                    break;

                case "*":
                    this.lCalculadora.Text = Convert.ToString(a * b);
                    break;

                case "/":
                    this.lCalculadora.Text = Convert.ToString(a / b);
                    break;
            }
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
                btnResultado.PerformClick();
            }
            else if (e.KeyChar == 27)//ESC
            {
                btnC.PerformClick();
            }
            else if(e.KeyChar == 8)//Retroceso
            {
                lCalculadora.Text = lCalculadora.Text.Substring(0, lCalculadora.Text.Count()-1);
            }
            else if (e.KeyChar == 32)//ESPACIO
            {
                this.Close();
            }
            else if (e.KeyChar == 46)//0
            {
                btnPunto.PerformClick();
            }
            else if (e.KeyChar == 48)//0
            {
                btn0.PerformClick();
            }
            else if (e.KeyChar == 49)//1
            {
                btn1.PerformClick();
            }
            else if (e.KeyChar == 50)//2
            {
                btn2.PerformClick();
            }
            else if (e.KeyChar == 51)//3
            {
                btn3.PerformClick();
            }
            else if (e.KeyChar == 52)//4
            {
                btn4.PerformClick();
            }
            else if (e.KeyChar == 53)//5
            {
                btn5.PerformClick();
            }
            else if (e.KeyChar == 54)//6
            {
                btn6.PerformClick();
            }
            else if (e.KeyChar == 55)//7
            {
                btn7.PerformClick();
            }
            else if (e.KeyChar == 56)//8
            {
                btn8.PerformClick();
            }
            else if (e.KeyChar == 57)//9
            {
                btn9.PerformClick();
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

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void calculadora_FormClosing(object sender, FormClosingEventArgs e)
        {
            //AgregarEditarProducto stockMaximoAgregarEditarProductos = new AgregarEditarProducto();
            //stockMaximoAgregarEditarProductos.txtStockMaximo.Text = lCalculadora.Text;

        }

        private void cboResultado_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
