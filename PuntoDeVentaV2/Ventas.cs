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

                string[] datosProducto = cn.BuscarProducto(idProducto, FormPrincipal.userID);

                txtBuscadorProducto.Text = "";
                txtBuscadorProducto.Focus();
                ocultarResultados();

                //0 = Sin descuento
                //1 = Descuento Cliente
                //2 = Descuento Mayoreo
                if (datosProducto[3] == "1")
                {
                    //MessageBox.Show("Descuento por Cliente");
                }
                else if (datosProducto[3] == "2")
                {
                    //MessageBox.Show("Descuento por Mayoreo");
                }
                else
                {
                    //MessageBox.Show("Sin descuento");
                }

                AgregarProducto(datosProducto);
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

        private void AgregarProducto(string[] datosProducto)
        {
            if (DGVentas.Rows.Count > 0)
            {
                bool existe = false;

                foreach (DataGridViewRow fila in DGVentas.Rows)
                {
                    //Compara el valor de la celda con el nombre del producto (Descripcion)
                    if (fila.Cells["Descripcion"].Value.Equals(datosProducto[1]))
                    {
                        int cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value) + 1;
                        float importe = cantidad * float.Parse(fila.Cells["Precio"].Value.ToString());

                        fila.Cells["Cantidad"].Value = cantidad;
                        fila.Cells["Importe"].Value = importe;
                        existe = true;
                    }
                }

                if (!existe)
                {
                    //Se agrega la nueva fila y se obtiene el ID que tendrá
                    int rowId = DGVentas.Rows.Add();

                    //Obtener la nueva fila
                    DataGridViewRow row = DGVentas.Rows[rowId];

                    //Agregamos la información
                    row.Cells["Cantidad"].Value = 1;
                    row.Cells["Precio"].Value = datosProducto[2];
                    row.Cells["Descripcion"].Value = datosProducto[1];
                    row.Cells["Descuento"].Value = 0;
                    row.Cells["Importe"].Value = datosProducto[2];

                    Image img1 = Image.FromFile(rutaDirectorio + @"\icon\black16\plus-square.png");
                    Image img2 = Image.FromFile(rutaDirectorio + @"\icon\black16\plus.png");
                    Image img3 = Image.FromFile(rutaDirectorio + @"\icon\black16\minus.png");
                    Image img4 = Image.FromFile(rutaDirectorio + @"\icon\black16\remove.png");

                    DGVentas.Rows[rowId].Cells["AgregarMultiple"].Value = img1;
                    DGVentas.Rows[rowId].Cells["AgregarIndividual"].Value = img2;
                    DGVentas.Rows[rowId].Cells["RestarIndividual"].Value = img3;
                    DGVentas.Rows[rowId].Cells["EliminarIndividual"].Value = img4;
                }
            }
            else
            {
                //Se agrega la nueva fila y se obtiene el ID que tendrá
                int rowId = DGVentas.Rows.Add();

                //Obtener la nueva fila
                DataGridViewRow row = DGVentas.Rows[rowId];

                //Agregamos la información
                row.Cells["Cantidad"].Value = 1;
                row.Cells["Precio"].Value = datosProducto[2];
                row.Cells["Descripcion"].Value = datosProducto[1];
                row.Cells["Descuento"].Value = 0;
                row.Cells["Importe"].Value = datosProducto[2];

                Image img1 = Image.FromFile(rutaDirectorio + @"\icon\black16\plus-square.png");
                Image img2 = Image.FromFile(rutaDirectorio + @"\icon\black16\plus.png");
                Image img3 = Image.FromFile(rutaDirectorio + @"\icon\black16\minus.png");
                Image img4 = Image.FromFile(rutaDirectorio + @"\icon\black16\remove.png");

                DGVentas.Rows[rowId].Cells["AgregarMultiple"].Value = img1;
                DGVentas.Rows[rowId].Cells["AgregarIndividual"].Value = img2;
                DGVentas.Rows[rowId].Cells["RestarIndividual"].Value = img3;
                DGVentas.Rows[rowId].Cells["EliminarIndividual"].Value = img4;
            }
        }

        private void DGVentas_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Iconos operaciones por producto
            if (e.ColumnIndex >= 5)
            {
                DGVentas.Cursor = Cursors.Hand;
            }
            else
            {
                DGVentas.Cursor = Cursors.Default;
            }
        }

        private void DGVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Agregar multiple
            if (e.ColumnIndex == 5)
            {
                MessageBox.Show("Agregar multiple");
            }

            //Agregar individual
            if (e.ColumnIndex == 6)
            {
                MessageBox.Show("Agregar individual");
            }

            //Restar individual
            if (e.ColumnIndex == 7)
            {
                MessageBox.Show("Restar individual");
            }

            //Eliminar individual
            if (e.ColumnIndex == 8)
            {
                MessageBox.Show("Eliminar individual");
            }
        }
    }
}
