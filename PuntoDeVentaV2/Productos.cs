using System;
using System.Collections.Generic;
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
    public partial class Productos : Form
    {

        public AgregarEditarProducto FormAgregar = new AgregarEditarProducto("Agregar Producto");
        public AgregarStockXML FormXML = new AgregarStockXML();
        public string rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public Productos()
        {
            InitializeComponent();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            CargarDatos();
            cbOrden.SelectedIndex = 0;
            cbOrden.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMostrar.SelectedIndex = 0;
            cbMostrar.DropDownStyle = ComboBoxStyle.DropDownList;

            DataGridViewImageColumn editar = new DataGridViewImageColumn();
            editar.Image = Image.FromFile(rutaDirectorio + @"\icon\black\pencil.png");
            editar.Width = 50;
            DGVProductos.Columns.Add(editar);

            DGVProductos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DGVProductos.CellClick += new DataGridViewCellEventHandler(EditarProducto);
        }

        private void CargarDatos()
        {
            cn.CargarInformacion(cs.Productos(FormPrincipal.userID), DGVProductos);
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            FormAgregar.FormClosed += delegate
            {
                CargarDatos();
            };

            if (FormAgregar.Text == "")
            {
                FormAgregar = new AgregarEditarProducto("Agregar Producto");
            }

            if (!FormAgregar.Visible)
            {
                FormAgregar.ShowDialog();
            }
            else
            {
                FormAgregar.BringToFront();
            }
        }

        private void EditarProducto(object sender, DataGridViewCellEventArgs e)
        {
            //Editar producto
            if (e.ColumnIndex == 0)
            {
                btnAgregarProducto.PerformClick();
            }
        }

        private void DGVProductos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Boton editar producto
            if (e.ColumnIndex == 0)
            {
                DGVProductos.Cursor = Cursors.Hand;
            }
            else
            {
                DGVProductos.Cursor = Cursors.Default;
            }
        }

        private void btnAgregarXML_Click(object sender, EventArgs e)
        {
            FormXML.FormClosed += delegate 
            {
                CargarDatos();
            };
            if (!FormXML.Visible)
            {
                FormXML.ShowDialog();
            }
            else
            {
                FormXML.BringToFront();
            }
        }
    }
}
