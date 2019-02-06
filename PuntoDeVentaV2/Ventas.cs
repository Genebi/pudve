using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class Ventas : Form
    {
        Conexion cn = new Conexion();

        AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();

        NameValueCollection datos = new NameValueCollection();

        string[] productos;

        public string rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));

        public Ventas()
        {
            InitializeComponent();

            //Cargar lista de productos actuales
            datos = cn.ObtenerProductos(FormPrincipal.userID);
            productos = new string[datos.Count];
            datos.CopyTo(productos, 0);

            coleccion.AddRange(productos);
            txtBuscadorProducto.AutoCompleteCustomSource = coleccion;
            txtBuscadorProducto.AutoCompleteMode = AutoCompleteMode.None;
            txtBuscadorProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            tituloSeccion.Focus();
            txtBuscadorProducto.GotFocus += new EventHandler(BuscarTieneFoco);
            txtBuscadorProducto.LostFocus += new EventHandler(BuscarPierdeFoco);

            btnEliminarUltimo.BackgroundImage = Image.FromFile(rutaDirectorio + @"\icon\black16\trash.png");
            btnEliminarTodos.BackgroundImage = Image.FromFile(rutaDirectorio + @"\icon\black16\trash.png");

            btnEliminarUltimo.BackgroundImageLayout = ImageLayout.Center;
            btnEliminarTodos.BackgroundImageLayout = ImageLayout.Center;
        }

        private void BuscarTieneFoco(object sender, EventArgs e)
        {
            if (txtBuscadorProducto.Text == "Buscar producto...")
            {
                txtBuscadorProducto.Text = "";
            }
        }

        private void BuscarPierdeFoco(object sender, EventArgs e)
        {
            if (txtBuscadorProducto.Text == "")
            {
                txtBuscadorProducto.Text = "Buscar producto...";
            }
        }

        private void txtBuscadorProducto_TextChanged(object sender, EventArgs e)
        {
            listaProductos.Items.Clear();

            if (txtBuscadorProducto.Text.Length == 0)
            {
                ocultarResultados();
                return;
            }

            foreach (string s in txtBuscadorProducto.AutoCompleteCustomSource)
            {
                if (s.Contains(txtBuscadorProducto.Text))
                {
                    listaProductos.Items.Add(s);
                    listaProductos.Visible = true;
                }
            }
        }

        private void listaProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listaProductos.SelectedIndex > -1)
            {
                
                //Se obtiene el texto del item seleccionado del ListBox
                string producto = listaProductos.Items[listaProductos.SelectedIndex].ToString();

                //Se obtiene el indice del array donde se encuentra el producto seleccionado
                int idProducto = Convert.ToInt32(datos.GetKey(Array.IndexOf(productos, producto)));

                string[] tmp = cn.BuscarProducto(idProducto, FormPrincipal.userID);

                txtBuscadorProducto.Text = "";
                txtBuscadorProducto.Focus();
                ocultarResultados();

                //DGVentas.Rows.Insert(tmp[0], tmp[1], tmp[2], tmp[3]);

                int rowId = DGVentas.Rows.Add();

                // Grab the new row!
                DataGridViewRow row = DGVentas.Rows[rowId];

                // Add the data
                row.Cells["Cantidad"].Value = "1";
                row.Cells["Precio"].Value = tmp[2];
                row.Cells["Descripcion"].Value = tmp[1];
                row.Cells["Descuento"].Value = "0";
                row.Cells["Importe"].Value = tmp[2];
            }
        }

        private void listaProductos_LostFocus(object sender, EventArgs e)
        {
            ocultarResultados();
        }

        private void ocultarResultados()
        {
            listaProductos.Visible = false;
        }

        private void txtBuscadorProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Down)
            {
                listaProductos.Focus();
            }
        }

        private void btnEliminarUltimo_Click(object sender, EventArgs e)
        {
            string[] tmp = new string[datos.Count];
            datos.CopyTo(tmp, 0);

            var d = datos.GetKey(Array.IndexOf(tmp, tmp[1]));

            MessageBox.Show(d);

        }
    }
}
