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
        //1 = por cliente
        //2 = por mayoreo
        int tipoDescuento = 1;

        public AgregarDescuentoProducto()
        {
            InitializeComponent();

            this.ControlBox = false;

            if (tipoDescuento == 1)
            {
                txtTituloDescuento.Text = "Descuento por Cliente";
                rbCliente.Checked = true;
            }
        }

        private void AgregarDescuentoProducto_Load(object sender, EventArgs e)
        {
            CargarFormularios(tipoDescuento);
        }

        private void btnCancelarDesc_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnAceptarDesc_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aceptar descuento");
        }

        private void CargarFormularios(int tipo)
        {
            panelContenedor.Controls.Clear();

            //Descuento por cliente
            if (tipo == 1)
            {
                FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                panelHijo.Name = "panelGeneradoCliente1";
                panelHijo.Width = 745;
                panelHijo.Height = 290;

                Label lb1 = new Label();
                lb1.Text = "Precio de producto";
                lb1.Margin = new Padding(270, 10, 0, 0);
                lb1.Font = new Font("Century Gothic", 11);
                lb1.AutoSize = false;
                lb1.Width = 220;
                lb1.Height = 20;
                lb1.TextAlign = ContentAlignment.MiddleCenter;

                TextBox tb1 = new TextBox();
                tb1.Name = "txtPrecio";
                tb1.Width = 220;
                tb1.Height = 20;
                tb1.Margin = new Padding(270, 5, 0, 0);
                tb1.TextAlign = HorizontalAlignment.Center;
                tb1.Enabled = false;
                tb1.BackColor = Color.White;
                

                Label lb2 = new Label();
                lb2.Text = "% de Descuento";
                lb2.AutoSize = false;
                lb2.Width = 220;
                lb2.Height = 20;
                lb2.Margin = new Padding(270, 20, 0, 0);
                lb2.Font = new Font("Century Gothic", 11);
                lb2.TextAlign = ContentAlignment.MiddleCenter;

                TextBox tb2 = new TextBox();
                tb2.Name = "txtPorcentaje";
                tb2.Width = 220;
                tb2.Height = 20;
                tb2.Margin = new Padding(270, 5, 0, 0);
                tb2.TextAlign = HorizontalAlignment.Center;

                Label lb3 = new Label();
                lb3.Text = "Precio con Descuento";
                lb3.AutoSize = false;
                lb3.Width = 220;
                lb3.Height = 20;
                lb3.Margin = new Padding(270, 20, 0, 0);
                lb3.Font = new Font("Century Gothic", 11);
                lb3.TextAlign = ContentAlignment.MiddleCenter;

                TextBox tb3 = new TextBox();
                tb3.Name = "txtPrecioDescuento";
                tb3.Width = 220;
                tb3.Height = 20;
                tb3.Margin = new Padding(270, 5, 0, 0);
                tb3.TextAlign = HorizontalAlignment.Center;
                tb3.Enabled = false;
                tb3.BackColor = Color.White;

                Label lb4 = new Label();
                lb4.Text = "Descuento";
                lb4.AutoSize = false;
                lb4.Width = 220;
                lb4.Height = 20;
                lb4.Margin = new Padding(270, 20, 0, 0);
                lb4.Font = new Font("Century Gothic", 11);
                lb4.TextAlign = ContentAlignment.MiddleCenter;

                TextBox tb4 = new TextBox();
                tb4.Name = "txtDescuento";
                tb4.Width = 220;
                tb4.Height = 20;
                tb4.Margin = new Padding(270, 5, 0, 0);
                tb4.TextAlign = HorizontalAlignment.Center;
                tb4.Enabled = false;
                tb4.BackColor = Color.White;

                panelHijo.Controls.Add(lb1);
                panelHijo.Controls.Add(tb1);
                panelHijo.Controls.Add(lb2);
                panelHijo.Controls.Add(tb2);
                panelHijo.Controls.Add(lb3);
                panelHijo.Controls.Add(tb3);
                panelHijo.Controls.Add(lb4);
                panelHijo.Controls.Add(tb4);
                panelHijo.FlowDirection = FlowDirection.TopDown;

                panelContenedor.Controls.Add(panelHijo);
                panelContenedor.FlowDirection = FlowDirection.TopDown;
            }

            //Descuento por mayoreo
            if (tipo == 2)
            {

            }
        }

        private void rbCliente_CheckedChanged(object sender, EventArgs e)
        {
            tipoDescuento = 1;
            CargarFormularios(tipoDescuento);
        }

        private void rbMayoreo_CheckedChanged(object sender, EventArgs e)
        {
            tipoDescuento = 2;
            CargarFormularios(tipoDescuento);
        }
    }
}
