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
            editar.Image = Image.FromFile(rutaDirectorio + @"\icon\black16\pencil.png");
            editar.Width = 50;
            editar.HeaderText = "Editar";
            DGVProductos.Columns.Add(editar);

            DataGridViewImageColumn setup = new DataGridViewImageColumn();
            setup.Image = Image.FromFile(rutaDirectorio + @"\icon\black16\check.png");
            setup.Width = 40;
            setup.HeaderText = "Estado";
            DGVProductos.Columns.Add(setup);

            DGVProductos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DGVProductos.CellClick += new DataGridViewCellEventHandler(EditarProducto);
            DGVProductos.CellClick += new DataGridViewCellEventHandler(EditarStatus);
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

        private void EditarStatus(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DialogResult result = MessageBox.Show("Realmente desdea Modificar el estado?", "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    //code for Yes
                }
                else if (result == DialogResult.No)
                {
                    //code for No
                }
                else if (result == DialogResult.Cancel)
                {
                    //code for Cancel
                }
            }
        }

        private void DGVProductos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Boton editar producto
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
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
                FormXML.OcultarPanelRegistro();
                FormXML.ShowDialog();
            }
            else
            {
                FormXML.BringToFront();
            }
        }
    }
}
