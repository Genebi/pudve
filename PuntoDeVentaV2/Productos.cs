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
        public RecordViewProduct ProductoRecord = new RecordViewProduct();
        //public string rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        int numfila, index;
        string Id_Prod_select, buscar, id, Nombre, Precio, Stock, ClaveInterna, CodigoBarras, status, filtro;

        DataTable dt, dtConsulta;

        private void cbMostrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtro = Convert.ToString(cbMostrar.SelectedItem);      // tomamos el valor que se elige en el TextBox
            if (filtro == "Habilitados")                            // comparamos si el valor a filtrar es Habilitados
            {
                // almacenamos el resultado de la consulta en dtConsulta
                dtConsulta = cn.CargarDatos(cs.StatusProductos(FormPrincipal.userID.ToString(), "1"));
                DGVProductos.DataSource = dtConsulta;               // llenamos el DataGridView con el resultado de la consulta
            }
            else if (filtro == "Deshabilitados")                    // comparamos si el valor a filtrar es Deshabilitados
            {
                // almacenamos el resultado de la consulta en dtConsulta
                dtConsulta = cn.CargarDatos(cs.StatusProductos(FormPrincipal.userID.ToString(), "0"));
                DGVProductos.DataSource = dtConsulta;               // llenamos el DataGridView con el resultado de la consulta
            }
            else if (filtro == "Todos")                             // comparamos si el valor a filtrar es Todos
            {
                CargarDatos();                                      // cargamos todos los registros
            }
        }

        /************************************************
        *       Iniciamos codigo para agregar el        *
        *       CheckBox en el DataGridView             *
        ************************************************/
        // agregamos el checkbox en el DataGridView en el headercheckbox
        CheckBox HeaderCheckBox = null;                             // declaramos el objeto CheckBox en NULL
        bool IsHeaderCheckBoxClicked = false;                       // declaramos un Boolean para ver si se marco CheckBox

        // metodo para agregar el CheckBox al Header del DataGridView
        private void AddHeaderCheckBox()
        {
            HeaderCheckBox = new CheckBox();                        // hacemos un nuevo CheckBox
            HeaderCheckBox.Size = new Size(15,15);                  // le hacemos unas dimensiones
            HeaderCheckBox.Top = 4;                                 // lo posicionamos con respecto del top a 4 px
            HeaderCheckBox.Left = 97;                               // lo posicionamos con respecto del Left a 104 px
            this.DGVProductos.Controls.Add(HeaderCheckBox);         // agregamos el checkBox dentro del DataGridView
        }

        // agregamos el envento para checar si se marca o no el CheckBox
        private void HeaderCheckBoxClick(CheckBox HCheckBox)
        {
            index = 0;
            IsHeaderCheckBoxClicked = true;                         // ponemos en true la variable
            if (HCheckBox.Checked == true)
            {
                DialogResult result = MessageBox.Show("Desdea Realmente Deshabilitar Todo el Stock", "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow Row in DGVProductos.Rows)      // hacemos un recorrido de cada una de la filas del DataGridView
                    {
                        ((DataGridViewCheckBoxCell)Row.Cells["chk"]).Value = HCheckBox.Checked;                     // verificamos que la celda pertenezca a la columna llamada chk
                                                                                                                    // y le ponemos el valor en true el checar el CheckBox
                        Nombre = ((DataGridViewTextBoxCell)Row.Cells["Nombre"]).Value.ToString();                   // tomamos el valor de la celda
                        Stock = ((DataGridViewTextBoxCell)Row.Cells["Stock"]).Value.ToString();                     // tomamos el valor de la celda
                        Precio = ((DataGridViewTextBoxCell)Row.Cells["Precio"]).Value.ToString();                   // tomamos el valor de la celda
                        ClaveInterna = ((DataGridViewTextBoxCell)Row.Cells["Clave Interna"]).Value.ToString();      // tomamos el valor de la celda
                        CodigoBarras = ((DataGridViewTextBoxCell)Row.Cells["Código de Barras"]).Value.ToString();   // tomamos el valor de la celda
                        id = FormPrincipal.userID.ToString();                                                       // tomamos el valor del ID del Usuario
                        ModificarStatusProducto();                                                                  // Llamamos el metodo de Modificar Status
                    }
                    DGVProductos.RefreshEdit();                             // Refrescamos el DataGridView
                    HCheckBox.Checked = false;
                    cbMostrar.Text = "Deshabilitados";
                }
                else if (result == DialogResult.No)
                {
                    HCheckBox.Checked = false;
                    cbMostrar.Text = "Habilitados";
                }
                else if (result == DialogResult.Cancel)
                {
                    HCheckBox.Checked = false;
                    cbMostrar.Text = "Todos";
                }
            }
            //TotalCheckedCheckBoxes = HCheckBox.Checked ? TotalCheckBoxes : 0;
            IsHeaderCheckBoxClicked = false;                        // ponemos en false la variable
        }

        // agregamos el evento de MouseClickEvent
        private void HeaderCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            HeaderCheckBoxClick((CheckBox)sender);                  // si es que se le da clic al HeaderCheckBox llamamos al metodo HeaderCheckBoxClick
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
            editar.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\pencil.png");
            editar.Width = 50;
            editar.HeaderText = "Editar";
            DGVProductos.Columns.Add(editar);

            DataGridViewImageColumn setup = new DataGridViewImageColumn();
            setup.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\cogs.png");
            setup.Width = 40;
            setup.HeaderText = "Activar/Desactivar";
            DGVProductos.Columns.Add(setup);

            DataGridViewImageColumn record = new DataGridViewImageColumn();
            record.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\line-chart.png");
            record.Width = 40;
            record.HeaderText = "Historial";
            DGVProductos.Columns.Add(record);

            DGVProductos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DGVProductos.CellClick += new DataGridViewCellEventHandler(EditarProducto);
            DGVProductos.CellClick += new DataGridViewCellEventHandler(EditarStatus);
            DGVProductos.CellClick += new DataGridViewCellEventHandler(RecordView);

            AddHeaderCheckBox();
            HeaderCheckBox.MouseClick += new MouseEventHandler(HeaderCheckBox_MouseClick);
        }

        private void CargarDatos()
        {
            cn.CargarInformacion(cs.Productos(FormPrincipal.userID), DGVProductos);
        }

        // metodo para cargar los datos 
        private void CargarDatosStatus(string idUsuario, string idProducto, string Status)
        {
            cn.EjecutarConsulta(cs.SetStatusProductos(idUsuario, idProducto, Status));      // modificamos el Status del Producto
            if (Status == "0")
            {
                dtConsulta = cn.CargarDatos(cs.StatusProductos(idUsuario, "1"));
                cbMostrar.Text = "Habilitados";
            }
            else if (Status == "1")
            {
                dtConsulta = cn.CargarDatos(cs.StatusProductos(idUsuario, "0"));
                cbMostrar.Text = "Deshabilitados";
            }
            else
            {
                cbMostrar.Text = "Todos";
            }
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
            if (e.ColumnIndex == 1)
            {
                btnAgregarProducto.PerformClick();
            }
        }

        private void ModificarStatusProducto()
        {
            // Preparamos el Query que haremos segun la fila seleccionada
            buscar = $"SELECT * FROM Productos WHERE Nombre = '{Nombre}' AND Precio = '{Precio}' AND Stock = '{Stock}' AND ClaveInterna = '{ClaveInterna}' AND CodigoBarras = '{CodigoBarras}' AND IDUsuario = '{id}'";
            dt = cn.CargarDatos(buscar);    // almacenamos el resultado de la Funcion CargarDatos que esta en la calse Consultas
            Id_Prod_select = dt.Rows[index]["ID"].ToString();       // almacenamos el Id del producto
            // preparamos el Query 
            buscar = $"UPDATE Productos SET Status = '{index}' WHERE ID = '{Id_Prod_select}' AND IDUsuario = '{id}'";
            dtConsulta = cn.CargarDatos(buscar);                    // acutualizamos los datos
            status = dt.Rows[index]["Status"].ToString();           // almacenamos el status
            if (status == "0")                              // si el status es 0
            {
                CargarDatosStatus(id, Id_Prod_select, "1");         // cambiamos el Status a 1
            }
            else if (status == "1")                         // si el status es 1
            {
                CargarDatosStatus(id, Id_Prod_select, "0");         // cambiamos el Status a 0
            }
            else
            {
                cbMostrar.Text = "Todos";
            }
        }

        private void ViewRecordProducto()
        {
            ProductoRecord.FormClosed += delegate
            {
                
            };
            if (!FormXML.Visible)
            {
                ProductoRecord.nombreProd = Nombre;
                ProductoRecord.stockProd = Stock;
                ProductoRecord.precioProd = Precio;
                ProductoRecord.claveInternaProd = ClaveInterna;
                ProductoRecord.codigoBarrasProd = CodigoBarras;
                ProductoRecord.idUsuarioProd = id;
                ProductoRecord.lblFolioCompra.Text = "";
                ProductoRecord.lblRFCProveedor.Text = "";
                ProductoRecord.lblNombreProveedor.Text = "";
                ProductoRecord.lblClaveProducto.Text = "";
                ProductoRecord.lblFechaCompletaCompra.Text = "";
                ProductoRecord.lblCantidadCompra.Text = "";
                ProductoRecord.lblValorUnitarioProducto.Text = "";
                ProductoRecord.lblDescuentoProducto.Text = "";
                ProductoRecord.lblPrecioCompra.Text = "";
                ProductoRecord.ShowDialog();
            }
            else
            {
                ProductoRecord.lblFolioCompra.Text = "";
                ProductoRecord.lblRFCProveedor.Text = "";
                ProductoRecord.lblNombreProveedor.Text = "";
                ProductoRecord.lblClaveProducto.Text = "";
                ProductoRecord.lblFechaCompletaCompra.Text = "";
                ProductoRecord.lblCantidadCompra.Text = "";
                ProductoRecord.lblValorUnitarioProducto.Text = "";
                ProductoRecord.lblDescuentoProducto.Text = "";
                ProductoRecord.lblPrecioCompra.Text = "";
                ProductoRecord.SeleccionarFila();
                ProductoRecord.BringToFront();
            }
            
        }

        private void EditarStatus(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                index = 0;
                
                DialogResult result = MessageBox.Show("Realmente desdea Modificar el estado?", "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    numfila = DGVProductos.CurrentRow.Index;
                    Nombre = DGVProductos[4, numfila].Value.ToString();             // Nombre Producto
                    Stock = DGVProductos[5, numfila].Value.ToString();              // Stock Producto
                    Precio = DGVProductos[6, numfila].Value.ToString();             // Precio Producto
                    ClaveInterna = DGVProductos[8, numfila].Value.ToString();       // ClaveInterna Producto
                    CodigoBarras = DGVProductos[9, numfila].Value.ToString();       // Codigo de Barras Producto
                    id = FormPrincipal.userID.ToString();
                    ModificarStatusProducto();
                }
                else if (result == DialogResult.No)
                {
                    dtConsulta = cn.CargarDatos(cs.StatusProductos(FormPrincipal.userID.ToString(), "1"));
                    DGVProductos.DataSource = dtConsulta;
                }
                else if (result == DialogResult.Cancel)
                {
                    //code for Cancel
                    cbMostrar.Text = "Todos";
                }
            }
        }

        private void RecordView(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                //MessageBox.Show("Proceso de construccion de Historial de compra","En Proceso de Construccion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                numfila = DGVProductos.CurrentRow.Index;
                Nombre = DGVProductos[4, numfila].Value.ToString();             // Nombre Producto
                Stock = DGVProductos[5, numfila].Value.ToString();              // Stock Producto
                Precio = DGVProductos[6, numfila].Value.ToString();             // Precio Producto
                ClaveInterna = DGVProductos[8, numfila].Value.ToString();       // ClaveInterna Producto
                CodigoBarras = DGVProductos[9, numfila].Value.ToString();       // Codigo de Barras Producto
                id = FormPrincipal.userID.ToString();
                ViewRecordProducto();
            }
        }

        private void DGVProductos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Boton editar producto
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 )
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
