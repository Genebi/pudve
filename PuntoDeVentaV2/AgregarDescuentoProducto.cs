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
            }
        }

        private void AgregarDescuentoProducto_Load(object sender, EventArgs e)
        {
            CargarFormularios(tipoDescuento);
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            tipoDescuento = 1;
            MessageBox.Show("Por cliente");
        }

        private void btnMayoreo_Click(object sender, EventArgs e)
        {
            tipoDescuento = 2;
            MessageBox.Show("Por mayoreo");
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
            //Descuento por cliente
            if (tipo == 1)
            {
                FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                panelHijo.Name = "panelGeneradoCliente";
                panelHijo.Width = 745;
                panelHijo.Height = 290;

                TextBox tbPrecio = new TextBox();
                tbPrecio.Name = "txtPrecio";
                tbPrecio.Width = 100;
                tbPrecio.Height = 20;
                tbPrecio.Margin = new Padding(20, 0, 0, 0);
                tbPrecio.TextAlign = HorizontalAlignment.Center;

                panelHijo.Controls.Add(tbPrecio);
                panelHijo.FlowDirection = FlowDirection.LeftToRight;

                panelContenedor.Controls.Add(panelHijo);
                panelContenedor.FlowDirection = FlowDirection.TopDown;
            }

            //Descuento por mayoreo
            if (tipo == 2)
            {

            }
        }
    }
}
