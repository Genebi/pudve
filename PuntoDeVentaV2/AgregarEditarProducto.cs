using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class AgregarEditarProducto : Form
    {
        static public int id = 1;
        static public string precioProducto = "";

        Conexion cn = new Conexion();

        public AgregarEditarProducto(string titulo)
        {
            InitializeComponent();
        }

        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string texto = txtCodigoBarras.Text;

                if (texto.Length >= 5)
                {
                    GenerarTextBox();
                    //MessageBox.Show(texto, "Mensaje");
                }
            }
        }


        private void GenerarTextBox()
        {
            FlowLayoutPanel panelHijo = new FlowLayoutPanel();
            panelHijo.Name = "panelGenerado" + id;
            panelHijo.Height = 25;
            panelHijo.Width = 320;
            panelHijo.HorizontalScroll.Visible = false;

            TextBox tb = new TextBox();
            tb.Name = "textboxGenerado" + id;
            tb.Width = 165;
            tb.Height = 20;
            tb.KeyDown += new KeyEventHandler(TextBox_Keydown);

            Button bt = new Button();
            bt.Cursor = Cursors.Hand;
            bt.Text = "X";
            bt.Name = "btnGenerado" + id;
            bt.Height = 23;
            bt.Width = 23;
            bt.BackColor = ColorTranslator.FromHtml("#C00000");
            bt.ForeColor = ColorTranslator.FromHtml("white");
            bt.FlatStyle = FlatStyle.Flat;
            bt.TextAlign = ContentAlignment.MiddleCenter;
            bt.Anchor = AnchorStyles.Top;
            bt.Click += new EventHandler(ClickBotones);

            panelHijo.Controls.Add(tb);
            panelHijo.Controls.Add(bt);
            panelHijo.FlowDirection = FlowDirection.LeftToRight;
            
            panelContenedor.Controls.Add(panelHijo);
            panelContenedor.FlowDirection = FlowDirection.TopDown;

            tb.Focus();
            id++;
        }


        private void TextBox_Keydown(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (e.KeyCode == Keys.Enter)
            {
                string texto = tbx.Text;

                if (texto.Length >= 5)
                {
                    GenerarTextBox();
                }
            }
        }

        private void ClickBotones(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            string nombreBoton = bt.Name;

            string idBoton = nombreBoton.Substring(11);
            string nombreTextBox = "textboxGenerado" + idBoton;
            string nombrePanel = "panelGenerado" + idBoton;

            foreach (Control item in panelContenedor.Controls.OfType<Control>())
            {
                if (item.Name == nombrePanel)
                {
                    panelContenedor.Controls.Remove(item);
                    panelContenedor.Controls.Remove(bt);
                }
            }
        }

        private void btnGenerarCB_Click(object sender, EventArgs e)
        {

            string fecha = DateTime.Now.ToString();
            fecha = fecha.Replace(" ", "");
            fecha = fecha.Replace("/", "");
            fecha = fecha.Replace(":", "");
            fecha = fecha.Substring(3, 11);

            txtCodigoBarras.Text = fecha;
        }

        private void btnAgregarDescuento_Click(object sender, EventArgs e)
        {
            if (txtPrecioProducto.Text == "")
            {
                MessageBox.Show("Es necesario agregar el precio del producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //AgregarDescuentoProducto FormAgregar = new AgregarDescuentoProducto();
                //FormAgregar.ShowDialog();
            }
        }

        private void btnDetalleFacturacion_Click(object sender, EventArgs e)
        {
            if (txtPrecioProducto.Text == "")
            {
                MessageBox.Show("Es necesario agregar el precio del producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                AgregarDetalleFacturacionProducto FormDetalle = new AgregarDetalleFacturacionProducto();
                FormDetalle.ShowDialog();
            }
        }

        private void txtPrecioProducto_KeyUp(object sender, KeyEventArgs e)
        {
            precioProducto = txtPrecioProducto.Text;
        }

        private void btnImagenes_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog imagenes = new OpenFileDialog();
                imagenes.Filter = "Imagenes JPG (*.jpg)|*.jpg| Imagenes PNG (.png)|.png";

                if (imagenes.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Imagen seleccionada");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {

            var nombre = txtNombreProducto.Text;
            var stock = txtStockProducto.Text;
            var precio = txtPrecioProducto.Text;
            var categoria = txtCategoriaProducto.Text;
            var claveIn = txtClaveProducto.Text;
            var codigoB = txtCodigoBarras.Text;

            string consulta = "INSERT INTO Productos(Nombre, Stock, Precio, Categoria, ClaveInterna, CodigoBarras, IDUsuario)" +
                              "VALUES('"+ nombre +"', '"+ stock +"', '"+ precio +"', '"+ categoria +"', '"+ claveIn +"', '"+ codigoB +"', '"+ FormPrincipal.userID +"')";

            int respuesta = cn.EjecutarConsulta(consulta);

            if (respuesta > 0)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error al intentar registrar el producto");
            }
        }
    }
}
