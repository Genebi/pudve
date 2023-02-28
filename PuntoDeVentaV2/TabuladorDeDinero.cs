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
    public partial class TabuladorDeDinero : Form
    {

        decimal TotalBilletes, TotalMonedas, TotalCaja;
        decimal Billete20, Billete50, billete100, billete200, billete500, billete1000;
        decimal diezcentavos, veintecentavos, cincuentaCentavos, unPeso, dosPesos, cincoPesos, diezPesos, veintePesos;

        private void txt1peso_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
            SumaDeMonedas();
        }

        private void txt2pesos_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
            SumaDeMonedas();
        }

        private void txt5pesos_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
            SumaDeMonedas();
        }

        private void txt10pesos_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
            SumaDeMonedas();
        }

        private void txt20pesos_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
            SumaDeMonedas();
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                txtbillete20.Text = "0";
            
         
                txtBillete50.Text = "0";
            
            
                txtBillete100.Text = "0";
            
            
                txtBillete200.Text = "0";
            
            
                txtBillete500.Text = "0";
            
            
                txtBillete1000.Text = "0";
            
           
                txt10centavo.Text = "0";
            
            
                txt20centavos.Text = "0";
            
            
                txt50centavos.Text = "0";
            
            
                txt1peso.Text = "0";
            
            
                txt2pesos.Text = "0";
            
           
                txt5pesos.Text = "0";
            
            
                txt10pesos.Text = "0";
            
            
                txt20pesos.Text = "0";
            
        }

        
        private void txtbillete20_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '0'))
            {
                e.Handled = true;
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

        private void txtBillete50_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '0'))
            {
                e.Handled = true;
            }
        }

        private void txtBillete100_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
               (e.KeyChar != '0'))
            {
                e.Handled = true;
            }
        }

        private void txtBillete200_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '0'))
            {
                e.Handled = true;
            }
        }

        private void txtBillete500_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '0'))
            {
                e.Handled = true;
            }
        }

        private void txtBillete1000_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '0'))
            {
                e.Handled = true;
            }
        }

        private void txt10centavo_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '0'))
            {
                e.Handled = true;
            }
        }

        private void txt20centavos_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
               (e.KeyChar != '0'))
            {
                e.Handled = true;
            }
        }

        private void txt50centavos_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '0'))
            {
                e.Handled = true;
            }
        }

        private void txt1peso_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '0'))
            {
                e.Handled = true;
            }
        }

        private void txt2pesos_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '0'))
            {
                e.Handled = true;
            }
        }

        private void txt5pesos_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '0'))
            {
                e.Handled = true;
            }
        }

        private void txt10pesos_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '0'))
            {
                e.Handled = true;
            }
        }

        private void txt20pesos_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '0'))
            {
                e.Handled = true;
            }
        }

        private void TabuladorDeDinero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtbillete20_Enter(object sender, EventArgs e)
        {
            
           
        }

        private void seleccionarCantidad(object sender, EventArgs e)
        {
            TextBox txtcantidad = (TextBox)sender;
            bool esNum;
            decimal cantidad = 0;
            esNum = decimal.TryParse(txtcantidad.Text, out cantidad);
            if (esNum)
            {
                if (cantidad.Equals(0))
                {
                    txtcantidad.Focus();
                    txtcantidad.SelectAll();
                }
                else
                {
                    txtcantidad.Focus();
                    
                }
            }
           
        }

        private void txtbillete20_Click(object sender, EventArgs e)
        {
            seleccionarCantidad(sender, e);
        }

        private void txtBillete50_Click(object sender, EventArgs e)
        {
            seleccionarCantidad(sender, e);
        }

        private void txtBillete100_Click(object sender, EventArgs e)
        {
            seleccionarCantidad(sender, e);
        }

        private void txtBillete200_Click(object sender, EventArgs e)
        {
            seleccionarCantidad(sender, e);
        }

        private void txtBillete500_Click(object sender, EventArgs e)
        {
            seleccionarCantidad(sender, e);
        }

        private void txtBillete1000_Click(object sender, EventArgs e)
        {
            seleccionarCantidad(sender, e);
        }

        private void txt10centavo_Click(object sender, EventArgs e)
        {
            seleccionarCantidad(sender, e);
        }

        private void txt20centavos_Click(object sender, EventArgs e)
        {
            seleccionarCantidad(sender, e);
        }

        private void txt50centavos_Click(object sender, EventArgs e)
        {
            seleccionarCantidad(sender, e);
        }

        private void txt1peso_Click(object sender, EventArgs e)
        {
            seleccionarCantidad(sender, e);
        }

        private void txt2pesos_Click(object sender, EventArgs e)
        {
            seleccionarCantidad(sender, e);
        }

        private void txt5pesos_Click(object sender, EventArgs e)
        {
            seleccionarCantidad(sender, e);
        }

        private void txt10pesos_Click(object sender, EventArgs e)
        {
            seleccionarCantidad(sender, e);
        }

        private void txt20pesos_Click(object sender, EventArgs e)
        {
            seleccionarCantidad(sender, e);
        }

        private void txt50centavos_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
            SumaDeMonedas();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt20centavos_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
            SumaDeMonedas();
        }

        public TabuladorDeDinero()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void TabuladorDeDinero_Load(object sender, EventArgs e)
        {
            //if (txtbillete20.Text == "")
            //{
            //    txtbillete20.Text = "0";
            //}
            //if (txtBillete50.Text == "")
            //{
            //    txtBillete50.Text = "0";
            //}
            //if (txtBillete100.Text == "")
            //{
            //    txtBillete100.Text = "0";
            //}
            //if (txtBillete200.Text == "")
            //{
            //    txtBillete200.Text = "0";
            //}
            //if (txtBillete500.Text == "")
            //{
            //    txtBillete500.Text = "0";
            //}
            //if (txtBillete1000.Text == "")
            //{
            //    txtBillete1000.Text = "0";
            //}
            //if (txt10centavo.Text == "")
            //{
            //    txt10centavo.Text = "0";
            //}
            //if (txt20centavos.Text == "")
            //{
            //    txt20centavos.Text = "0";
            //}
            //if (txt50centavos.Text == "")
            //{
            //    txt50centavos.Text = "0";
            //}
            //if (txt1peso.Text == "")
            //{
            //    txt1peso.Text = "0";
            //}
            //if (txt2pesos.Text == "")
            //{
            //    txt2pesos.Text = "0";
            //}
            //if (txt5pesos.Text == "")
            //{
            //    txt5pesos.Text = "0";
            //}
            //if (txt10pesos.Text == "")
            //{
            //    txt10pesos.Text = "0";
            //}
            //if (txt20pesos.Text== "")
            //{
            //    txt20pesos.Text = "0";
            //}
        }

        private void txt10centavo_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
            SumaDeMonedas();
        }

        private void SumaDeMonedas()
        {
            if (txt10centavo.Text == "")
            {
                txt10centavo.Text = "0";
                txt10centavo.Focus();
                txt10centavo.SelectAll();
            }
            else
            {
                
                diezcentavos = Convert.ToDecimal(txt10centavo.Text) * (decimal).10;
                lbl10centavos.Text = diezcentavos.ToString("C2");
            }
            if (txt20centavos.Text == "")
            {
                txt20centavos.Text = "0";
                txt20centavos.Focus();
                txt20centavos.SelectAll();
            }
            else
            {
                veintecentavos = Convert.ToDecimal(txt20centavos.Text) * (decimal).20;
                lbl20centavos.Text = veintecentavos.ToString("C2");
            }
            if (txt50centavos.Text == "")
            {
                txt50centavos.Text = "0";
                txt50centavos.Focus();
                txt50centavos.SelectAll();
            }
            else
            {
                cincuentaCentavos = Convert.ToDecimal(txt50centavos.Text) * (decimal).50;
                lbl50centavos.Text = cincuentaCentavos.ToString("C2");
            }
            if (txt1peso.Text == "")
            {
                txt1peso.Text = "0";
                txt1peso.Focus();
                txt1peso.SelectAll();
            }
            else
            {
                unPeso = Convert.ToDecimal(txt1peso.Text);
                lbl1peso.Text = unPeso.ToString("C2");
            }
            if (txt2pesos.Text == "")
            {
                txt2pesos.Text = "0";
                txt2pesos.Focus();
                txt2pesos.SelectAll();
            }
            else
            {
                dosPesos = Convert.ToDecimal(txt2pesos.Text) * 2;
                lbl2pesos.Text = dosPesos.ToString("C2");
            }
            if (txt5pesos.Text == "")
            {
                txt5pesos.Text = "0";
                txt5pesos.Focus();
                txt5pesos.SelectAll();
            }
            else
            {
                cincoPesos = Convert.ToDecimal(txt5pesos.Text) * 5;
                lbl5pesos.Text = cincoPesos.ToString("C2");
            }
            if (txt10pesos.Text == "")
            {
                txt10pesos.Text = "0";
                txt10pesos.Focus();
                txt10pesos.SelectAll();
            }
            else
            {
                diezPesos = Convert.ToDecimal(txt10pesos.Text) * 10;
                lbl10pesos.Text = diezPesos.ToString("C2");
            }
            if (txt20pesos.Text == "")
            {
                txt20pesos.Text = "0";
                txt20pesos.Focus();
                txt20pesos.SelectAll();
            }
            else
            {
                veintePesos = Convert.ToDecimal(txt20pesos.Text) * 20;
                lbl20pesos.Text = veintePesos.ToString("C2");
            }

            TotalMonedas = diezcentavos + veintecentavos + cincuentaCentavos + unPeso + dosPesos + cincoPesos + diezPesos + veintePesos;
            lblTotalMonedas.Text = TotalMonedas.ToString("C2");

            TotalCaja = TotalBilletes + TotalMonedas;
            lblTotalCAJA.Text = TotalCaja.ToString("C2");
        }


        private void txtbillete20_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
            SumaDeBilletes();
        }

        private void SumaDeBilletes()
        {
            

            if (txtbillete20.Text == "")
            {
                txtbillete20.Text = "0";
                txtbillete20.Focus();
                txtbillete20.SelectAll();
            }
            else
            {
                Billete20 = Convert.ToDecimal(txtbillete20.Text) * 20;
                lbl20.Text = Billete20.ToString("C2");
            }

            if (txtBillete50.Text == "")
            {
                txtBillete50.Text = "0";
                txtBillete50.Focus();
                txtBillete50.SelectAll();
            }
            else
            {
                Billete50 = Convert.ToDecimal(txtBillete50.Text) * 50;
                lbl50.Text = Billete50.ToString("C2");
            }

            if (txtBillete100.Text == "")
            {
                txtBillete100.Text = "0";
                txtBillete100.Focus();
                txtBillete100.SelectAll();
            }
            else
            {

                billete100 = Convert.ToDecimal(txtBillete100.Text) * 100;
                lbl100.Text = billete100.ToString("C2");
            }

            if (txtBillete200.Text == "")
            {
                txtBillete200.Text = "0";
                txtBillete200.Focus();
                txtBillete200.SelectAll();
            }
            else
            {
                billete200 = Convert.ToDecimal(txtBillete200.Text) * 200;
                lbl200.Text = billete200.ToString("C2");
            }

            if (txtBillete500.Text == "")
            {
                txtBillete500.Text = "0";
                txtBillete500.Focus();
                txtBillete500.SelectAll();
            }
            else
            {
                billete500 = Convert.ToDecimal(txtBillete500.Text) * 500;
                lbl500.Text = billete500.ToString("C2");
            }

            if (txtBillete1000.Text == "")
            {
                txtBillete1000.Text = "0";
                txtBillete1000.Focus();
                txtBillete1000.SelectAll();
            }
            else
            {
                billete1000 = Convert.ToDecimal(txtBillete1000.Text) * 1000;
                lbl1000.Text = billete1000.ToString("C2");
            }

            TotalBilletes =  Billete20 + Billete50 + billete100 + billete200 + billete500 + billete1000;

            lblTotalBilletes.Text = TotalBilletes.ToString("C2");

            TotalCaja = TotalBilletes + TotalMonedas;
            lblTotalCAJA.Text = TotalCaja.ToString("C2");
        }

        private void txtBillete50_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
            SumaDeBilletes();
        }

        private void txtBillete100_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
            SumaDeBilletes();
        }

        private void txtBillete200_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
            SumaDeBilletes();
        }

        private void txtBillete500_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
            SumaDeBilletes();
        }

        private void txtBillete1000_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
            SumaDeBilletes();
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
    }
}
