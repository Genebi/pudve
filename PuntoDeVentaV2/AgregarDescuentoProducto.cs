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
    public partial class AgregarDescuentoProducto : Form
    {
        static private int id = 2;

        public AgregarDescuentoProducto()
        {
            InitializeComponent();

            txtPorcentajeDescuento.Text = "0";
            txtPrecioDescuento.Text = "0.00";
            txtDescuentoTotal.Text = "0.00";

            this.ControlBox = false;
        }

        private void radioCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCliente.Checked)
            {
                tabControl.Visible = true;
                tabControl.SelectedTab = primeraPagina;

                if (!btnAceptarDesc.Visible)
                    btnAceptarDesc.Visible = true;
            }
        }

        private void radioMayoreo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioMayoreo.Checked)
            {
                tabControl.Visible = true;
                tabControl.SelectedTab = segundaPagina;

                if (!btnAceptarDesc.Visible)
                    btnAceptarDesc.Visible = true;

                txtRango1_3.Text = AgregarEditarProducto.precioProducto;
            }
        }

        private void txtPorcentajeDescuento_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPorcentajeDescuento.Text == "")
            {
                txtPrecioDescuento.Text = "0.00";
                txtDescuentoTotal.Text = "0.00";
                return;
            }

            float precioProducto = float.Parse(AgregarEditarProducto.precioProducto);
            float descuento = float.Parse(txtPorcentajeDescuento.Text);
            float totalDescontado = (precioProducto * descuento) / 100;
            float precioDescuento = precioProducto - totalDescontado;

            txtPrecioDescuento.Text = precioDescuento.ToString();
            txtDescuentoTotal.Text = totalDescontado.ToString();
        }


        private void GenerarTextBox()
        {
            FlowLayoutPanel panelHijo = new FlowLayoutPanel();
            panelHijo.Name = "panelGeneradoR" + id;
            panelHijo.Height = 25;
            panelHijo.Width = 600;
            //panelHijo.HorizontalScroll.Visible = false;

            TextBox tb1 = new TextBox();
            tb1.Name = "txtRango" + id + "_1";
            tb1.Width = 100;
            tb1.Height = 20;
            tb1.TextAlign = HorizontalAlignment.Center;
            tb1.ReadOnly = true;
            tb1.Margin = new Padding(102, 3, 30, 3);


            TextBox tb2 = new TextBox();
            tb2.Name = "txtRango" + id + "_2";
            tb2.Width = 100;
            tb2.Height = 20;
            tb2.TextAlign = HorizontalAlignment.Center;

            TextBox tb3 = new TextBox();
            tb3.Name = "txtRango" + id + "_3";
            tb3.Width = 100;
            tb3.Height = 20;
            tb3.TextAlign = HorizontalAlignment.Center;
            tb3.Margin = new Padding(65, 3, 3, 3);
            tb3.KeyDown += new KeyEventHandler(TextBox_Keydown);

            Button bt = new Button();
            bt.Cursor = Cursors.Hand;
            bt.Text = "X";
            bt.Name = "btnGeneradoR" + id;
            bt.Width = 22;
            bt.Height = 22;
            bt.BackColor = ColorTranslator.FromHtml("#C00000");
            bt.ForeColor = ColorTranslator.FromHtml("white");
            bt.FlatStyle = FlatStyle.Flat;
            bt.Click += new EventHandler(ClickBotones);


            panelHijo.Controls.Add(tb1);
            panelHijo.Controls.Add(tb2);
            panelHijo.Controls.Add(tb3);
            panelHijo.Controls.Add(bt);
            panelHijo.FlowDirection = FlowDirection.LeftToRight;

            panelRangos.Controls.Add(panelHijo);
            panelRangos.FlowDirection = FlowDirection.TopDown;

            tb2.Focus();

            //Checkbox
            FlowLayoutPanel panelHijo2 = new FlowLayoutPanel();
            panelHijo2.Height = 25;
            panelHijo2.Width = 500;
            panelHijo2.Name = "panelGeneradoR_" + id;
            //panelHijo2.HorizontalScroll.Visible = false;

            CheckBox cb = new CheckBox();
            cb.Name = "cbRango" + id;
            cb.Height = 14;
            cb.Width = 15;
            cb.CheckedChanged += Cb_CheckedChanged1;

            Label lb = new Label();
            lb.Name = "lbRango" + id;
            lb.Width = 450;
            lb.Margin = new Padding(3, 5, 3, 3);
      

            panelHijo2.Controls.Add(cb);
            panelHijo2.Controls.Add(lb);
            panelHijo2.Margin = new Padding(102, 3, 3, 3);
            panelHijo2.FlowDirection = FlowDirection.LeftToRight;

            panelRangos.Controls.Add(panelHijo2);
            panelRangos.FlowDirection = FlowDirection.TopDown;

            if (id == 2)
            {
                txtRango1_2.Enabled = false;
                int cantidadTmp = Int32.Parse(txtRango1_2.Text);
                cantidadTmp++;
                tb1.Text = cantidadTmp.ToString();
            }
            else
            {
                string tb1Tmp = "txtRango" + (id - 1).ToString() + "_1";
                string tb2Tmp = "txtRango" + (id - 1).ToString() + "_2";
                string tb3Tmp = "txtRango" + (id - 1).ToString() + "_3";
                string lbTmp = "lbRango" + (id - 1).ToString();
                string btnTmp = "btnGeneradoR" + (id - 1).ToString();
                string textoLabel = "";       

                foreach (Control item in panelRangos.Controls.OfType<Control>())
                {
                    foreach (Control tbHijo in item.Controls.OfType<Control>())
                    {
                        if (tbHijo.Name == tb1Tmp)
                        {
                            textoLabel += "De entre " + tbHijo.Text;
                        }

                        if (tbHijo.Name == tb2Tmp)
                        {
                            tbHijo.Enabled = false;

                            if (tbHijo.Text != "")
                            {
                                int cantidadTmp = Int32.Parse(tbHijo.Text);
                                cantidadTmp++;
                                tb1.Text = cantidadTmp.ToString();
                                textoLabel += " a " + tbHijo.Text + " siempre costarán a ";
                            }
                        }

                        if (tbHijo.Name == tb3Tmp)
                        {
                            tbHijo.Enabled = false;
                            textoLabel += tbHijo.Text;
                        }

                        if (tbHijo.Name == lbTmp)
                        {
                            tbHijo.Text = textoLabel;
                        }

                        if (tbHijo.Name == btnTmp)
                        {
                            tbHijo.Enabled = false;
                        }
                    }
                }
            }

            id++;
        }

        private void Cb_CheckedChanged1(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            var numCB = cb.Name.Substring(7);
            var numTemp = Int32.Parse(numCB);

            foreach (Control elemento1 in panelRangos.Controls.OfType<Control>())
            {
                foreach (Control elemento2 in elemento1.Controls.OfType<CheckBox>())
                {
                    var numero = Int32.Parse(elemento2.Name.Substring(7));

                    if (numTemp >= numero)
                    {
                        CheckBox cbTmp = elemento2 as CheckBox;

                        if (cb.CheckState == CheckState.Checked)
                        {
                            cbRango1.Checked = true;
                            cbTmp.Checked = true;
                        }
                        else
                        {
                            cbRango1.Checked = false;
                            cbTmp.Checked = false;
                        }
                    }
                }
            }
        }

        private void btnCancelarDesc_Click(object sender, EventArgs e)
        {
            id = 2;
            this.Dispose();
        }

        private void btnAceptarDesc_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Prueba loca");
        }

        private void txtRango1_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtRango1_2.Text != "")
                {
                    lbRango1.Text = "Las primeras " + txtRango1_2.Text + " siempre costarán a " + txtRango1_3.Text;
                    GenerarTextBox();
                }
            }
        }

        private void TextBox_Keydown(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (e.KeyCode == Keys.Enter)
            {
                if (tbx.Text != "")
                {
                    GenerarTextBox();
                }
            }
        }


        private void ClickBotones(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            string nombreBoton = bt.Name;
            int idBoton = Int32.Parse(nombreBoton.Substring(12));

            string nombrePanel = "panelGeneradoR" + idBoton;
            string nombrePanel2 = "panelGeneradoR_" + idBoton;
            string tbTmp2 = "txtRango" + (idBoton - 1) + "_2";
            string tbTmp3 = "txtRango" + (idBoton - 1) + "_3";
            string btnTmp = "btnGeneradoR" + (idBoton - 1);

            foreach (Control item in panelRangos.Controls.OfType<Control>())
            {
                if (item.Name == nombrePanel2)
                {
                    panelRangos.Controls.Remove(item);
                }
            }

            foreach (Control item in panelRangos.Controls.OfType<Control>())
            {
                if (item.Name == nombrePanel)
                {
                    panelRangos.Controls.Remove(item);
                }
            }

            foreach (Control item in panelRangos.Controls.OfType<Control>())
            {
                foreach (Control item2 in item.Controls.OfType<Control>())
                {
                    if (item2.Name == tbTmp2)
                    {
                        item2.Enabled = true;
                    }

                    if (item2.Name == tbTmp3)
                    {
                        item2.Enabled = true;
                    }

                    if (item2.Name == btnTmp)
                    {
                        item2.Enabled = true;
                    }
                }
            }

            id--;

            if (id == 2)
            {
                txtRango1_2.Enabled = true;
                txtRango1_2.Focus();
            } 
        }

        private void AgregarDescuentoProducto_Load(object sender, EventArgs e)
        {

        }
    }
}
