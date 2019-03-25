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
        public CodeBarMake MakeBarCode = new CodeBarMake();
        public photoShow VentanaMostrarFoto = new photoShow();
        //public string rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        int numfila, index, number_of_rows, i;
        string Id_Prod_select, buscar, id, Nombre, Precio, Stock, ClaveInterna, CodigoBarras, status, filtro;

        DataTable dt, dtConsulta;
        DataGridViewButtonColumn setup, record, barcode, foto;
        DataGridViewImageCell cell;

        Icon image;

        OpenFileDialog f;       // declaramos el objeto de OpenFileDialog

        // objeto para el manejo de las imagenes
        FileStream File, File1;
        FileInfo info;

        // direccion de la carpeta donde se va poner las imagenes
        string saveDirectoryImg = Properties.Settings.Default.rutaDirectorio + @"\Productos\";
        // nombre de archivo
        string fileName;
        // directorio origen de la imagen
        string oldDirectory;
        // directorio para guardar el archivo
        string fileSavePath;
        // Nuevo nombre del archivo
        string NvoFileName;

        string logoTipo = "";

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
            HeaderCheckBox.Top = 5;                                 // lo posicionamos con respecto del top a 4 px
            HeaderCheckBox.Left = 99;                               // lo posicionamos con respecto del Left
            this.DGVProductos.Controls.Add(HeaderCheckBox);         // agregamos el checkBox dentro del DataGridView
        }

        // agregamos el envento para checar si se marca o no el CheckBox
        private void HeaderCheckBoxClick(CheckBox HCheckBox)
        {
            index = 0;
            IsHeaderCheckBoxClicked = true;                         // ponemos en true la variable
            if (HCheckBox.Checked == true)
            {
                DialogResult result = MessageBox.Show("Desdea Realmente Modificar el Estatus del Todo el Stock", "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow Row in DGVProductos.Rows)      // hacemos un recorrido de cada una de la filas del DataGridView
                    {
                        // verificamos que la celda pertenezca a la columna llamada chk
                        ((DataGridViewCheckBoxCell)Row.Cells["chk"]).Value = HCheckBox.Checked;                     // y le ponemos el valor en true el checar el CheckBox
                        Nombre = ((DataGridViewTextBoxCell)Row.Cells["Nombre"]).Value.ToString();                   // tomamos el valor de la celda
                        Stock = ((DataGridViewTextBoxCell)Row.Cells["Stock"]).Value.ToString();                     // tomamos el valor de la celda
                        Precio = ((DataGridViewTextBoxCell)Row.Cells["Precio"]).Value.ToString();                   // tomamos el valor de la celda
                        ClaveInterna = ((DataGridViewTextBoxCell)Row.Cells["Clave Interna"]).Value.ToString();      // tomamos el valor de la celda
                        CodigoBarras = ((DataGridViewTextBoxCell)Row.Cells["Código de Barras"]).Value.ToString();   // tomamos el valor de la celda
                        id = FormPrincipal.userID.ToString();                                                       // tomamos el valor del ID del Usuario
                        //ModificarStatusProducto();                                                                  // Llamamos el metodo de Modificar Status
                    }
                    //DGVProductos.RefreshEdit();                             // Refrescamos el DataGridView
                    //HCheckBox.Checked = false;
                    cbMostrar.Text = "Deshabilitados";
                    if (cbMostrar.Text == "Deshabilitados")
                    {
                        CargarDatosStatus(id, Id_Prod_select, "0");         // cambiamos el Status a 0
                    }
                }
                else if (result == DialogResult.No)
                {
                    HCheckBox.Checked = false;
                    cbMostrar.Text = "Habilitados";
                    if (cbMostrar.Text == "Habilitados")
                    {
                        CargarDatosStatus(id, Id_Prod_select, "1");         // cambiamos el Status a 1
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    HCheckBox.Checked = false;
                    cbMostrar.Text = "Todos";
                    if (cbMostrar.Text == "Todos")
                    {
                        CargarDatos();
                    }
                }
            }
            IsHeaderCheckBoxClicked = false;                        // ponemos en false la variable
        }

        // agregamos el evento de MouseClickEvent
        private void HeaderCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            HeaderCheckBoxClick((CheckBox)sender);                  // si es que se le da clic al HeaderCheckBox llamamos al metodo HeaderCheckBoxClick
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            dtConsulta.DefaultView.RowFilter = $"Nombre LIKE '{txtBusqueda.Text}%'";
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

            setup = new DataGridViewButtonColumn();
            setup.Width = 40;
            setup.Name = "status";
            setup.HeaderText = "Estado";
            DGVProductos.Columns.Add(setup);

            record = new DataGridViewButtonColumn();
            record.Width = 40;
            record.Name = "historial";
            record.HeaderText = "Historial";
            DGVProductos.Columns.Add(record);

            barcode = new DataGridViewButtonColumn();
            barcode.Width = 40;
            barcode.Name = "CodigoBarras";
            barcode.HeaderText = "Generar";
            DGVProductos.Columns.Add(barcode);

            foto = new DataGridViewButtonColumn();
            foto.Width = 40;
            foto.Name = "Fotos";
            foto.HeaderText = "Imagen";
            DGVProductos.Columns.Add(foto);

            DGVProductos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DGVProductos.CellClick += new DataGridViewCellEventHandler(EditarProducto);
            DGVProductos.CellClick += new DataGridViewCellEventHandler(EditarStatus);
            DGVProductos.CellClick += new DataGridViewCellEventHandler(RecordView);
            DGVProductos.CellClick += new DataGridViewCellEventHandler(BarCodeMake);
            DGVProductos.CellClick += new DataGridViewCellEventHandler(PhotoStatus);

            AddHeaderCheckBox();
            HeaderCheckBox.MouseClick += new MouseEventHandler(HeaderCheckBox_MouseClick);

            DGVProductos.Columns["Path"].Visible = false;
            DGVProductos.Columns["Activo"].Visible = false;
        }

        private void CargarDatos()
        {
            cn.CargarInformacion(cs.Productos(FormPrincipal.userID), DGVProductos);
            number_of_rows = DGVProductos.RowCount;
        }

        private void DGVProductos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.DGVProductos.Columns[e.ColumnIndex].Name == "status" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                string valor = DGVProductos.Rows[e.RowIndex].Cells["Activo"].Value.ToString();

                DataGridViewButtonCell statusBoton = this.DGVProductos.Rows[e.RowIndex].Cells["status"] as DataGridViewButtonCell;
                //statusBoton.FlatStyle = FlatStyle.Flat;
                //statusBoton.Style.BackColor = Color.GhostWhite;

                if (valor == "1")
                {
                    image = new Icon(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\check.ico");
                    e.Graphics.DrawIcon(image, e.CellBounds.Left + 18, e.CellBounds.Top + 3);
                    this.DGVProductos.Rows[e.RowIndex].Height = image.Height + 8;
                    this.DGVProductos.Columns[e.ColumnIndex].Width = image.Width + 36;
                }
                if (valor == "0")
                {
                    image = new Icon(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\close.ico");
                    e.Graphics.DrawIcon(image, e.CellBounds.Left + 18, e.CellBounds.Top + 3);
                    this.DGVProductos.Rows[e.RowIndex].Height = image.Height + 8;
                    this.DGVProductos.Columns[e.ColumnIndex].Width = image.Width + 36;
                }
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.DGVProductos.Columns[e.ColumnIndex].Name == "historial" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell historialBoton = this.DGVProductos.Rows[e.RowIndex].Cells["historial"] as DataGridViewButtonCell;
                //historialBoton.FlatStyle = FlatStyle.Flat;
                //historialBoton.Style.BackColor = Color.GhostWhite;

                image = new Icon(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\line-chart.ico");
                e.Graphics.DrawIcon(image, e.CellBounds.Left + 18, e.CellBounds.Top + 3);
                this.DGVProductos.Rows[e.RowIndex].Height = image.Height + 8;
                this.DGVProductos.Columns[e.ColumnIndex].Width = image.Width + 36;
                
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.DGVProductos.Columns[e.ColumnIndex].Name == "CodigoBarras" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell codigoBarrasBoton = this.DGVProductos.Rows[e.RowIndex].Cells["CodigoBarras"] as DataGridViewButtonCell;
                //codigoBarrasBoton.FlatStyle = FlatStyle.Flat;
                //codigoBarrasBoton.Style.BackColor = Color.GhostWhite;

                image = new Icon(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\barcode.ico");
                e.Graphics.DrawIcon(image, e.CellBounds.Left + 18, e.CellBounds.Top + 3);
                this.DGVProductos.Rows[e.RowIndex].Height = image.Height + 8;
                this.DGVProductos.Columns[e.ColumnIndex].Width = image.Width + 36;

                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.DGVProductos.Columns[e.ColumnIndex].Name == "Fotos" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                string valor = DGVProductos.Rows[e.RowIndex].Cells["Path"].Value.ToString();

                DataGridViewButtonCell photoBoton = this.DGVProductos.Rows[e.RowIndex].Cells["Fotos"] as DataGridViewButtonCell;
                //photoBoton.FlatStyle = FlatStyle.Flat;
                //photoBoton.Style.BackColor = Color.GhostWhite;

                if (valor == "")
                {
                    image = new Icon(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\file-o.ico");
                    e.Graphics.DrawIcon(image, e.CellBounds.Left + 18, e.CellBounds.Top+3);
                    this.DGVProductos.Rows[e.RowIndex].Height = image.Height + 8;
                    this.DGVProductos.Columns[e.ColumnIndex].Width = image.Width + 36;
                }
                if (valor != "")
                {
                    image = new Icon(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\file-picture-o.ico");
                    e.Graphics.DrawIcon(image, e.CellBounds.Left + 18, e.CellBounds.Top + 3);
                    this.DGVProductos.Rows[e.RowIndex].Height = image.Height + 8;
                    this.DGVProductos.Columns[e.ColumnIndex].Width = image.Width + 36;
                }
                e.Handled = true;
            }
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
            DataRow row;
            // Preparamos el Query que haremos segun la fila seleccionada
            buscar = $"SELECT * FROM Productos WHERE Nombre = '{Nombre}' AND Precio = '{Precio}' AND Stock = '{Stock}' AND ClaveInterna = '{ClaveInterna}' AND CodigoBarras = '{CodigoBarras}' AND IDUsuario = '{id}'";
            dt = cn.CargarDatos(buscar);    // almacenamos el resultado de la Funcion CargarDatos que esta en la calse Consultas
            //dataGridView1.DataSource = dt;
            //row = dt.Rows[1];
            //Id_Prod_select = Convert.ToString(row["ID"]);       // almacenamos el Id del producto
            //status = Convert.ToString(row["Status"]);           // almacenamos el status
            //if (status == "0")                              // si el status es 0
            //{
            //    // preparamos el Query 
            //    buscar = $"UPDATE Productos SET Status = '1' WHERE ID = '{Id_Prod_select}' AND IDUsuario = '{id}'";
            //    dtConsulta = cn.CargarDatos(buscar);                    // acutualizamos los datos
            //}
            //else if (status == "1")                         // si el status es 1
            //{
            //    // preparamos el Query 
            //    buscar = $"UPDATE Productos SET Status = '0' WHERE ID = '{Id_Prod_select}' AND IDUsuario = '{id}'";
            //    dtConsulta = cn.CargarDatos(buscar);                    // acutualizamos los datos
            //}
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
                    Nombre = DGVProductos[6, numfila].Value.ToString();             // Nombre Producto
                    Stock = DGVProductos[7, numfila].Value.ToString();              // Stock Producto
                    Precio = DGVProductos[8, numfila].Value.ToString();             // Precio Producto
                    ClaveInterna = DGVProductos[10, numfila].Value.ToString();       // ClaveInterna Producto
                    CodigoBarras = DGVProductos[11, numfila].Value.ToString();       // Codigo de Barras Producto
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
                Nombre = DGVProductos[6, numfila].Value.ToString();             // Nombre Producto
                Stock = DGVProductos[7, numfila].Value.ToString();              // Stock Producto
                Precio = DGVProductos[8, numfila].Value.ToString();             // Precio Producto
                ClaveInterna = DGVProductos[10, numfila].Value.ToString();       // ClaveInterna Producto
                CodigoBarras = DGVProductos[11, numfila].Value.ToString();       // Codigo de Barras Producto
                id = FormPrincipal.userID.ToString();
                ViewRecordProducto();
            }
        }

        private void BarCodeMake(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                string codiBarProd="";
                numfila = DGVProductos.CurrentRow.Index;
                //MessageBox.Show("Proceso de construccion de Codigo de Barras", "En Proceso de Construccion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MakeBarCode.FormClosed += delegate
                {
                    
                };
                if (!MakeBarCode.Visible)
                {
                    MakeBarCode.NombreProd = DGVProductos[6, numfila].Value.ToString();
                    MakeBarCode.PrecioProd = DGVProductos[8, numfila].Value.ToString();
                    codiBarProd = DGVProductos[11, numfila].Value.ToString();
                    if (codiBarProd != "")
                    {
                        MakeBarCode.CodigoBarProd = codiBarProd;
                        MakeBarCode.ShowDialog();
                    }
                    else if (codiBarProd == "")
                    {
                        MessageBox.Show("No se puede generar el codigo de barras\nPuesto que no tiene codigo de barras asignado","Error de Generar Codigo de Barras", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MakeBarCode.NombreProd = DGVProductos[6, numfila].Value.ToString();
                    MakeBarCode.PrecioProd = DGVProductos[8, numfila].Value.ToString();
                    codiBarProd = DGVProductos[11, numfila].Value.ToString();
                    if (codiBarProd != "")
                    {
                        MakeBarCode.CodigoBarProd = codiBarProd;
                        MakeBarCode.BringToFront();
                    }
                    else if (codiBarProd == "")
                    {
                        MessageBox.Show("No se puede generar el codigo de barras\nPuesto que no tiene codigo de barras asignado", "Error de Generar Codigo de Barras", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void agregarFoto()
        {
            try
            {
                using (f = new OpenFileDialog())    // Abrirmos el OpenFileDialog para buscar y seleccionar la Imagen
                {
                    // le aplicamos un filtro para solo ver 
                    // imagenes de tipo *.jpg y *.png 
                    f.Filter = "Imagenes JPG (*.jpg)|*.jpg| Imagenes PNG (*.png)|*.png";
                    if (f.ShowDialog() == DialogResult.OK)      // si se selecciono correctamente un archivo en el OpenFileDialog
                    {
                        /************************************************
                        *   usamos el objeto File para almacenar las    *
                        *   propiedades de la imagen                    * 
                        ************************************************/
                        using (File = new FileStream(f.FileName, FileMode.Open, FileAccess.Read))
                        {
                            info = new FileInfo(f.FileName);                        // Obtenemos toda la Informacion de la Imagen
                            fileName = Path.GetFileName(f.FileName);                // Obtenemos el nombre de la imagen
                            oldDirectory = info.DirectoryName;                      // Obtenemos el directorio origen de la Imagen
                            File.Dispose();                                         // Liberamos el objeto File
                        }
                    }
                }
                if (!Directory.Exists(saveDirectoryImg))        // verificamos que si no existe el directorio
                {
                    Directory.CreateDirectory(saveDirectoryImg);        // lo crea para poder almacenar la imagen
                }
                if (f.CheckFileExists)      // si el archivo existe
                {
                    try     // Intentamos la actualizacion de la imagen en la base de datos
                    {
                        // Obtenemos el Nuevo nombre de la imagen
                        // con la que se va hacer la copia de la imagen
                        NvoFileName = fileName;
                        // hacemos la nueva cadena de consulta para hacer el UpDate
                        string insertarImagen = $"UPDATE Productos SET ProdImage = '{saveDirectoryImg + NvoFileName}' WHERE Nombre = '{Nombre}' AND Stock = '{Stock}' AND Precio = '{Precio}' AND ClaveInterna = '{ClaveInterna}' AND CodigoBarras = '{CodigoBarras}'";
                        cn.EjecutarConsulta(insertarImagen);    // hacemos que se ejecute la consulta
                        // realizamos la copia de la imagen origen hacia el nuevo destino
                        System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
                        CargarDatos();
                    }
                    catch (Exception ex)	// si no se puede hacer el proceso
                    {
                        // si no se borra el archivo muestra este mensaje
                        MessageBox.Show("Error al hacer el borrado No: " + ex);
                    }
                }
            }
            catch (Exception ex)	// si el nombre del archivo esta en blanco
            {
                // si no selecciona un archivo valido o ningun archivo muestra este mensaje
                MessageBox.Show("selecciona una Imagen", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void mostrarFoto()
        {
            VentanaMostrarFoto.FormClosed += delegate
            {
                CargarDatos();
            };
            if (!VentanaMostrarFoto.Visible)
            {
                VentanaMostrarFoto.NombreProd = Nombre;
                VentanaMostrarFoto.StockProd = Stock;
                VentanaMostrarFoto.PrecioProd = Precio;
                VentanaMostrarFoto.ClaveInterna = ClaveInterna;
                VentanaMostrarFoto.CodigoBarras = CodigoBarras;
                VentanaMostrarFoto.ShowDialog();
            }
            else
            {
                VentanaMostrarFoto.NombreProd = Nombre;
                VentanaMostrarFoto.StockProd = Stock;
                VentanaMostrarFoto.PrecioProd = Precio;
                VentanaMostrarFoto.ClaveInterna = ClaveInterna;
                VentanaMostrarFoto.CodigoBarras = CodigoBarras;
                VentanaMostrarFoto.BringToFront();
            }
        }

        public void PhotoStatus(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                numfila = DGVProductos.CurrentRow.Index;

                Nombre = DGVProductos[6, numfila].Value.ToString();             // Nombre Producto
                Stock = DGVProductos[7, numfila].Value.ToString();              // Stock Producto
                Precio = DGVProductos[8, numfila].Value.ToString();             // Precio Producto
                ClaveInterna = DGVProductos[10, numfila].Value.ToString();       // ClaveInterna Producto
                CodigoBarras = DGVProductos[11, numfila].Value.ToString();       // Codigo de Barras Producto

                string pathString;

                pathString = DGVProductos[13, numfila].Value.ToString();

                if (pathString != "")
                {
                    mostrarFoto(); 
                }
                else if (pathString == "")
                {
                    agregarFoto();
                }
            }
        }

        private void DGVProductos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Boton editar producto
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
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
