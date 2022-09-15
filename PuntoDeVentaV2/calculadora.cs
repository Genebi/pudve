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
        double a = 0, b = 0, resultado = 0, d = 0;
        string c = "";
        public static bool seEnvia = false;

        public calculadora()
        {
            InitializeComponent();
        }

        private void calculadora_Load(object sender, EventArgs e)
        {
            lCalculadora.Text = "0";
            label1.BackColor = Color.FromArgb(229, 231, 233);
            label2.BackColor = Color.FromArgb(229, 231, 233);

            this.ActiveControl = lCalculadora;
            foreach (Control control in this.Controls)
            {
                control.PreviewKeyDown += new PreviewKeyDownEventHandler(control_PreviewKeyDown);
            }

        }
        //Bloquear las teclas de dirección
        private void control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode==Keys.Up || e.KeyCode==Keys.Down || e.KeyCode==Keys.Left || e.KeyCode==Keys.Right)
            {
                e.IsInputKey = true;
                lCalculadora.Focus();
            }

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
            if (d > 0)
            {
                btnC.PerformClick();

                if (lCalculadora.Text == "" || lCalculadora.Text == "0")
                {
                    lCalculadora.Text = "0";
                }
                else
                {
                    lCalculadora.Text = lCalculadora.Text + "0";
                }
            }
            else if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "0";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "0";
            }
            d = 0;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (d > 0)
            {
                btnC.PerformClick();

                if (lCalculadora.Text == "" || lCalculadora.Text == "0")
                {
                    lCalculadora.Text = "1";
                }
                else
                {
                    lCalculadora.Text = lCalculadora.Text + "1";
                }
            }
            else if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "1";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "1";
            }
            d = 0;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (d > 0)
            {
                btnC.PerformClick();

                if (lCalculadora.Text == "" || lCalculadora.Text == "0")
                {
                    lCalculadora.Text = "2";
                }
                else
                {
                    lCalculadora.Text = lCalculadora.Text + "2";
                }
            }
            else if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "2";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "2";
            }
            d = 0;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (d > 0)
            {
                btnC.PerformClick();

                if (lCalculadora.Text == "" || lCalculadora.Text == "0")
                {
                    lCalculadora.Text = "3";
                }
                else
                {
                    lCalculadora.Text = lCalculadora.Text + "3";
                }
            }
            else if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "3";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "3";
            }
            d = 0;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (d > 0)
            {
                btnC.PerformClick();

                if (lCalculadora.Text == "" || lCalculadora.Text == "0")
                {
                    lCalculadora.Text = "4";
                }
                else
                {
                    lCalculadora.Text = lCalculadora.Text + "4";
                }
            }
            else if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "4";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "4";
            }
            d = 0;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (d > 0)
            {
                btnC.PerformClick();

                if (lCalculadora.Text == "" || lCalculadora.Text == "0")
                {
                    lCalculadora.Text = "5";
                }
                else
                {
                    lCalculadora.Text = lCalculadora.Text + "5";
                }
            }
            else if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "5";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "5";
            }
            d = 0;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (d > 0)
            {
                btnC.PerformClick();

                if (lCalculadora.Text == "" || lCalculadora.Text == "0")
                {
                    lCalculadora.Text = "6";
                }
                else
                {
                    lCalculadora.Text = lCalculadora.Text + "6";
                }
            }
            else if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "6";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "6";
            }
            d = 0;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
           
            if (d > 0)
            {
                btnC.PerformClick();

                if (lCalculadora.Text == "" || lCalculadora.Text == "0")
                {
                    lCalculadora.Text = "7";
                }
                else
                {
                    lCalculadora.Text = lCalculadora.Text + "7";
                }
            }
            else if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "7";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "7";
            }
            d = 0;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (d > 0)
            {
                btnC.PerformClick();

                if (lCalculadora.Text == "" || lCalculadora.Text == "0")
                {
                    lCalculadora.Text = "8";
                }
                else
                {
                    lCalculadora.Text = lCalculadora.Text + "8";
                }
            }
            else if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "8";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "8";
            }
            d = 0;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (d > 0)
            {
                btnC.PerformClick();

                if (lCalculadora.Text == "" || lCalculadora.Text == "0")
                {
                    lCalculadora.Text = "9";
                }
                else
                {
                    lCalculadora.Text = lCalculadora.Text + "9";
                }
            }
            else if (lCalculadora.Text == "" || lCalculadora.Text == "0")
            {
                lCalculadora.Text = "9";
            }
            else
            {
                lCalculadora.Text = lCalculadora.Text + "9";
            }
            d = 0;
        }

        //Boton C
        private void btnC_Click(object sender, EventArgs e)
        {
            lvista.Text = "";
            b = Convert.ToDouble(0);
            this.lCalculadora.Text = "0";
        }

        //Boton punto
        private void btnPunto_Click(object sender, EventArgs e)
        {
            if (d > 0)
            {
                btnC.PerformClick();

                if (this.lCalculadora.Text.Contains('.') == false)
                {
                    this.lCalculadora.Text = this.lCalculadora.Text + ".";
                }
            }
            else if (this.lCalculadora.Text.Contains('.') == false)
            {
                this.lCalculadora.Text = this.lCalculadora.Text + ".";
            }
            d = 0;
        }

        //Botones de operaciones al dar click
        private void btnSumar_Click(object sender, EventArgs e)
        {
            c = "+";
            if (!string.IsNullOrEmpty(lCalculadora.Text))
            {
                this.btnSumar.BackColor = Color.Gray;
                a = Convert.ToDouble(this.lCalculadora.Text);
                this.lCalculadora.Text = "";
                this.lCalculadora.Focus();
            }
            this.lvista.Text = a.ToString() + c;
        }
        
        private void btnRestar_Click(object sender, EventArgs e)
        {
            c = "-";
            if (!string.IsNullOrEmpty(lCalculadora.Text))
            {
                this.btnRestar.BackColor = Color.Gray;
                a = Convert.ToDouble(this.lCalculadora.Text);
                this.lCalculadora.Text = "";
                this.lCalculadora.Focus();
            }
            this.lvista.Text = a.ToString() + c;
        }

        private void btnMultiplicar_Click(object sender, EventArgs e)
        {
            c = "*";
            if (!string.IsNullOrEmpty(lCalculadora.Text))
            {
                this.btnMultiplicar.BackColor = Color.Gray;
                a = Convert.ToDouble(this.lCalculadora.Text);
                this.lCalculadora.Text = "";
                this.lCalculadora.Focus();
            }
            this.lvista.Text = a.ToString() + c;
        }

        private void btnDividir_Click(object sender, EventArgs e)
        {
            c = "/";
            if (!string.IsNullOrEmpty(lCalculadora.Text))
            {
                this.btnDividir.BackColor = Color.Gray;
                a = Convert.ToDouble(this.lCalculadora.Text);
                this.lCalculadora.Text = "";
                this.lCalculadora.Focus();
            }
             this.lvista.Text = a.ToString() + c;
        }

        private void btnResultado_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lCalculadora.Text))
            {
                
                lvista.Text = "";
                if (b == 0)
                {
                    b = Convert.ToDouble(this.lCalculadora.Text);
                }
                else if (!this.lCalculadora.Text.Equals(resultado.ToString()))
                {
                    b = Convert.ToDouble(this.lCalculadora.Text);
                }

                btnSumar.BackColor = Color.FromArgb(215, 219, 221);
                btnRestar.BackColor = Color.FromArgb(215, 219, 221);
                btnMultiplicar.BackColor = Color.FromArgb(215, 219, 221);
                btnDividir.BackColor = Color.FromArgb(215, 219, 221);
                d++;
                switch (c)
                {
                    case "+":
                        resultado = (a + b);
                        this.lCalculadora.Text = Convert.ToString(resultado);
                        a = resultado;
                        break;

                    case "-":
                        resultado = (a - b);
                        this.lCalculadora.Text = Convert.ToString(resultado);
                        a = resultado;
                        break;

                    case "*":
                        resultado = (a * b);
                        this.lCalculadora.Text = Convert.ToString(resultado);
                        a = resultado;
                        break;

                    case "/":
                        if (b != 0)
                        {
                            resultado = (a / b);
                            this.lCalculadora.Text = Convert.ToString(resultado);
                            a = resultado;
                        }
                        else
                        {
                            lCalculadora.Text = "No se puede dividir entre 0";
                            btnC.PerformClick();
                        }
                        break;
                }
            }
        }
       
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

        //FUNCIONALIDAD DE LAS TECLAS
        private void calculadora_KeyPress(object sender, KeyPressEventArgs e)
        {
            int subCerrarCalculadra=0;

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
            else if (e.KeyChar == Convert.ToChar(Keys.Enter))//ENTER
            {
                btnResultado.PerformClick();
            }
            else if (e.KeyChar == 27)//ESC
            {
                this.Hide();
            }
            else if (e.KeyChar == 8)//Retroceso
            {
                btnRetroceso.PerformClick();
            }
            else if (e.KeyChar == 32)//ESPACIO
            {
                this.Close();
            }
            else if (e.KeyChar == 46)//Punto
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
            else if (e.KeyChar == 99)//C
            {
                btnC.PerformClick();
            }
   
        }

        private void calculadora_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)//SUPRIMIR
            {
                btnC.PerformClick();
            } 
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            seEnvia = true;
            this.Close();
        }

        private void calculadora_FormClosing(object sender, FormClosingEventArgs e)
        {
            //AgregarEditarProducto stockMaximoAgregarEditarProductos = new AgregarEditarProducto();
            //stockMaximoAgregarEditarProductos.txtStockMaximo.Text = lCalculadora.Text;

        }

        private void btnRetroceso_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lCalculadora.Text))
            {
                lCalculadora.Text = lCalculadora.Text.Substring(0, lCalculadora.Text.Count() - 1);
            }
            if (string.IsNullOrEmpty(lCalculadora.Text))
            {
                lCalculadora.Text = "0";
            }
            if (lCalculadora.Text == "-")
            {
                lCalculadora.Text = "0";
            }
        }
    }
}
