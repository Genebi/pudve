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

        int numfila, index;
        string Id_Prod_select, buscar, id, Nombre, Precio, Stock, ClaveInterna, CodigoBarras, status, filtro;

        DataTable dt, dtConsulta;

        private void cbMostrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtro = Convert.ToString(cbMostrar.SelectedItem);
            if (filtro == "Habilitados")
            {
                dtConsulta = cn.CargarDatos(cs.StatusProductos(FormPrincipal.userID.ToString(), "1"));
                DGVProductos.DataSource = dtConsulta;
            }
            else if (filtro == "Deshabilitados")
            {
                dtConsulta = cn.CargarDatos(cs.StatusProductos(FormPrincipal.userID.ToString(), "0"));
                DGVProductos.DataSource = dtConsulta;
            }
            else if (filtro == "Todos")
            {
                CargarDatos();
            }
        }

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
            setup.Image = Image.FromFile(rutaDirectorio + @"\icon\black16\cogs.png");
            setup.Width = 40;
            setup.HeaderText = "Activar/Desactivar";
            DGVProductos.Columns.Add(setup);

            DGVProductos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DGVProductos.CellClick += new DataGridViewCellEventHandler(EditarProducto);
            DGVProductos.CellClick += new DataGridViewCellEventHandler(EditarStatus);
        }

        private void CargarDatos()
        {
            cn.CargarInformacion(cs.Productos(FormPrincipal.userID), DGVProductos);
        }

        private void CargarDatosStatus(string idUsuario, string idProducto, string Status)
        {
            cn.EjecutarConsulta(cs.SetStatusProductos(idUsuario, idProducto, Status));
            dtConsulta = cn.CargarDatos(cs.StatusProductos(idUsuario, "1"));
            DGVProductos.DataSource = dtConsulta;
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
                index = 0;
                
                DialogResult result = MessageBox.Show("Realmente desdea Modificar el estado?", "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    numfila = DGVProductos.CurrentRow.Index;
                    Nombre = DGVProductos[2, numfila].Value.ToString();    // Nombre Producto
                    Stock = DGVProductos[3, numfila].Value.ToString();    // Stock Producto
                    Precio = DGVProductos[4, numfila].Value.ToString();    // Precio Producto
                    ClaveInterna = DGVProductos[6, numfila].Value.ToString();    // ClaveInterna Producto
                    CodigoBarras = DGVProductos[7, numfila].Value.ToString();    // Codigo de Barras Producto
                    id = FormPrincipal.userID.ToString();
                    // Preparamos el Query que haremos segun la fila seleccionada
                    buscar = $"SELECT * FROM Productos WHERE Nombre = '{Nombre}' AND Precio = '{Precio}' AND Stock = '{Stock}' AND ClaveInterna = '{ClaveInterna}' AND CodigoBarras = '{CodigoBarras}' AND IDUsuario = '{id}'";
                    // almacenamos el resultado de la Funcion CargarDatos
                    // que esta en la calse Consultas
                    dt = cn.CargarDatos(buscar);
                    // almacenamos el Id del producto
                    Id_Prod_select = dt.Rows[index]["ID"].ToString();
                    buscar = $"UPDATE Productos SET Status = '{index}' WHERE ID = '{Id_Prod_select}' AND IDUsuario = '{id}'";
                    // acutualizamos los datos
                    dtConsulta = cn.CargarDatos(buscar);
                    status = dt.Rows[index]["Status"].ToString();
                    if (status == "0")
                    {
                        CargarDatosStatus(id, Id_Prod_select, "1");
                    }
                    else if (status == "1")
                    {
                        CargarDatosStatus(id, Id_Prod_select, "0");
                    }
                }
                else if (result == DialogResult.No)
                {
                    dtConsulta = cn.CargarDatos(cs.StatusProductos(FormPrincipal.userID.ToString(), "1"));
                    DGVProductos.DataSource = dtConsulta;
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
