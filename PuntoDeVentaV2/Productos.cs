using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
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
        public string rutaLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public AgregarEditarProducto FormAgregar = new AgregarEditarProducto("Agregar Producto");
        public AgregarStockXML FormXML = new AgregarStockXML();
        public RecordViewProduct ProductoRecord = new RecordViewProduct();
        public CodeBarMake MakeBarCode = new CodeBarMake();
        public photoShow VentanaMostrarFoto = new photoShow();
        public TagMake MakeTagProd = new TagMake();
        public VentanaDetalleFotoProducto ProductoDetalle = new VentanaDetalleFotoProducto();
        public DetalleDescripcion Descripcion = new DetalleDescripcion();

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        int numfila, index, number_of_rows, i, seleccionadoDato, origenDeLosDatos=0, editarEstado = 0, numerofila = 0;
        string Id_Prod_select, buscar, id, Nombre, Precio, Stock, ClaveInterna, CodigoBarras, status, filtro;

        DataTable dt, dtConsulta, fotos, registros;
        DataGridViewButtonColumn setup, record, barcode, foto, tag, copy;
        DataGridViewImageCell cell;

        Icon image;

        OpenFileDialog f;       // declaramos el objeto de OpenFileDialog

        // objeto para el manejo de las imagenes
        FileStream File, File1;
        FileInfo info;

        // direccion de la carpeta donde se va poner las imagenes
        string saveDirectoryImg = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Productos\";
        // nombre de archivo
        string fileName;
        // directorio origen de la imagen
        string oldDirectory;
        // directorio para guardar el archivo
        string fileSavePath;
        // Nuevo nombre del archivo
        string NvoFileName;

        string logoTipo = "";

        string ProductoNombre, ProductoStock, ProductoPrecio, ProductoCategoria, ProductoClaveInterna, ProductoCodigoBarras;

        string savePath;

        string queryFotos, queryGral;

        string ID_ProdSerPaq;

        private void DGVProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                foreach (DataGridViewRow row in DGVProductos.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[e.ColumnIndex].Value))
                    {
                        row.Cells[e.ColumnIndex].Value = false;
                    }
                }
            }
        }

        private void TTipButtonText_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        // objeto de FileStream para poder hacer el manejo de las imagenes
        FileStream fs;

        int IDProducto;

        private void searchPhotoProd()
        {
            queryFotos = $"SELECT prod.ID, prod.Nombre, prod.ProdImage, prod.Precio, prod.Status FROM Productos prod WHERE prod.IDUsuario = '{FormPrincipal.userID}'";
            fotos = cn.CargarDatos(queryFotos);
        }

        private void searchPhotoProdActivo()
        {
            queryFotos = $"SELECT prod.ID, prod.Nombre, prod.ProdImage, prod.Precio, prod.Status FROM Productos prod WHERE prod.IDUsuario = '{FormPrincipal.userID}' AND prod.Status = 1";
            fotos = cn.CargarDatos(queryFotos);
        }

        private void searchPhotoProdInactivo()
        {
            queryFotos = $"SELECT prod.ID, prod.Nombre, prod.ProdImage, prod.Precio, prod.Status FROM Productos prod WHERE prod.IDUsuario = '{FormPrincipal.userID}' AND prod.Status = 0";
            fotos = cn.CargarDatos(queryFotos);
        }
        
        private void searchToProdGral()
        {
            CargarDatos();
        }

        private void photoShow()
        {
            fLPShowPhoto.Controls.Clear();
            foreach (DataRow row in fotos.Rows)
            {
                Button btn = new Button();
                btn.Text = row["Nombre"].ToString()+ "\n $"+Convert.ToDecimal(row["Precio"]).ToString("N2");
                btn.Size = new System.Drawing.Size(150, 150);
                btn.Font = new Font("Tahoma", 14, FontStyle.Bold | FontStyle.Italic);
                btn.TextAlign = ContentAlignment.TopCenter;
                if (row["ProdImage"].ToString() == "" || row["ProdImage"].ToString() == null)
                {
                    btn.ForeColor = Color.Red;
                    using (fs = new FileStream(fileSavePath + @"\no-image.png", FileMode.Open))
                    {
                        btn.Image = Image.FromStream(fs);
                        btn.Image = new Bitmap(btn.Image, btn.Size);
                    }
                }
                else if (row["ProdImage"].ToString() != "" || row["ProdImage"].ToString() != null)
                {
                    btn.ForeColor = Color.Red;
                    using (fs = new FileStream(fileSavePath + row["ProdImage"].ToString(), FileMode.Open))
                    {
                        btn.Image = Image.FromStream(fs);
                        btn.Image = new Bitmap(btn.Image, btn.Size);
                    }
                }
                btn.Tag = row["ID"].ToString();
                fLPShowPhoto.Controls.Add(btn);
                btn.Click += new EventHandler(ProductPhotoButtonClick);
            }
        }

        private void cbOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtro = Convert.ToString(cbOrden.SelectedItem);
            if (filtro == "A - Z")
            {
                if (panelShowDGVProductosView.Visible == true)
                {
                    DGVProductos.Sort(DGVProductos.Columns["Column1"], ListSortDirection.Ascending);
                }
                else if (panelShowPhotoView.Visible == true)
                {
                    fotos.DefaultView.Sort = "Nombre ASC";
                    fotos = fotos.DefaultView.ToTable();
                    photoShow();
                }
            }
            else if (filtro == "Z - A")
            {
                if (panelShowDGVProductosView.Visible == true)
                {
                    DGVProductos.Sort(DGVProductos.Columns["Column1"], ListSortDirection.Descending);
                }
                else if (panelShowPhotoView.Visible == true)
                {
                    fotos.DefaultView.Sort = "Nombre DESC";
                    fotos = fotos.DefaultView.ToTable();
                    photoShow();
                }
            }
            else if (filtro == "Mayor precio")
            {
                if (panelShowDGVProductosView.Visible == true)
                {
                    DGVProductos.Sort(DGVProductos.Columns["Column3"], ListSortDirection.Descending);
                }
                else if (panelShowPhotoView.Visible == true)
                {
                    fotos.DefaultView.Sort = "Precio DESC";
                    fotos = fotos.DefaultView.ToTable();
                    photoShow(); 
                }
            }
            else if (filtro == "Menor precio")
            {
                if (panelShowDGVProductosView.Visible == true)
                {
                    DGVProductos.Sort(DGVProductos.Columns["Column3"], ListSortDirection.Ascending);
                }
                else if (panelShowPhotoView.Visible == true)
                {
                    fotos.DefaultView.Sort = "Precio ASC";
                    fotos = fotos.DefaultView.ToTable();
                    photoShow();
                }
            }
            else if (filtro == "Ordenar por:")
            {
                if (panelShowDGVProductosView.Visible == true)
                {
                    CargarDatos();
                }
                else if (panelShowPhotoView.Visible == true)
                {
                    fotos.DefaultView.Sort = "ID ASC";
                    fotos = fotos.DefaultView.ToTable();
                    photoShow();
                }
            }
        }

        // Metodo creado para manejo de mostrar ventana
        private void ProductPhotoButtonClick(object sender, EventArgs e)
        {
            //MessageBox.Show("Ventana de Informacion en Construccion", "Ventana de Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Button btn = (Button)sender;
            IDProducto = Convert.ToInt32(btn.Tag);
            ProductoDetalle.FormClosed += delegate
            {

            };
            if (!ProductoDetalle.Visible)
            {
                ProductoDetalle.IDProducto = IDProducto;
                ProductoDetalle.ShowDialog();
            }
            else
            {
                ProductoDetalle.IDProducto = IDProducto;
                ProductoDetalle.BringToFront();
            }
        }

        private void btnPhotoView_Click(object sender, EventArgs e)
        {
            fileSavePath = saveDirectoryImg;
            if (panelShowDGVProductosView.Visible == true || panelShowDGVProductosView.Visible == false)
            {
                panelShowDGVProductosView.Visible = false;
                panelShowPhotoView.Visible = true;
                searchPhotoProd();
                photoShow();
            }
            else if (panelShowPhotoView.Visible == true || panelShowPhotoView.Visible == false)
            {
                panelShowDGVProductosView.Visible = false;
                panelShowPhotoView.Visible = true;
                searchPhotoProd();
                photoShow();
            }
        }

        private void btnListView_Click(object sender, EventArgs e)
        {
            if (panelShowDGVProductosView.Visible == true || panelShowDGVProductosView.Visible == false)
            {
                panelShowPhotoView.Visible = false;
                panelShowDGVProductosView.Visible = true;
                searchToProdGral();
            }
            else if (panelShowPhotoView.Visible == true || panelShowPhotoView.Visible == false)
            {
                panelShowPhotoView.Visible = false;
                panelShowDGVProductosView.Visible = true;
                searchToProdGral();
            }
        }
        
        private void DGVProductos_CellMouseEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var textoTTipButtonMsg = string.Empty;
                int coordenadaX = 0, coordenadaY = 0;
                Rectangle cellRect = DGVProductos.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                if (e.ColumnIndex == 0)
                {
                    textoTTipButtonMsg = "Seleccionar Producto";
                    coordenadaX = 110;
                    coordenadaY = -200;
                    TTipButtonText.Show(textoTTipButtonMsg, this, DGVProductos.Location.X + cellRect.X - coordenadaX, DGVProductos.Location.Y + cellRect.Y - coordenadaY, 1500);
                    textoTTipButtonMsg = string.Empty;
                }
                else if (e.ColumnIndex >= 7)
                {
                    DGVProductos.Cursor = Cursors.Hand;
                    if (e.ColumnIndex == 7)
                    {
                        textoTTipButtonMsg = "Editar el Producto";
                        coordenadaX = 90;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 8)
                    {
                        textoTTipButtonMsg = "Modificar estado del Producto";
                        coordenadaX = 160;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 9)
                    {
                        textoTTipButtonMsg = "Historial de Compra";
                        coordenadaX = 105;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 10)
                    {
                        textoTTipButtonMsg = "Generar Código de Barras";
                        coordenadaX = 130;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 11)
                    {
                        textoTTipButtonMsg = "Imagen del Producto";
                        coordenadaX = 110;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 12)
                    {
                        textoTTipButtonMsg = "Generar Etiqueta de Producto";
                        coordenadaX = 155;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 13)
                    {
                        textoTTipButtonMsg = "Copiar Producto";
                        coordenadaX = 85;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 16)
                    {
                        textoTTipButtonMsg = "Producto/Servicio";
                        coordenadaX = 90;
                        coordenadaY = -200;
                    }
                    TTipButtonText.Show(textoTTipButtonMsg, this, DGVProductos.Location.X + cellRect.X - coordenadaX, DGVProductos.Location.Y + cellRect.Y - coordenadaY, 1500);
                    textoTTipButtonMsg = string.Empty;
                }
                else
                {
                    DGVProductos.Cursor = Cursors.Default;
                }
            }
        }

        private void DGVProductos_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                numerofila = e.RowIndex;
                obtenerDatosDGVProductos(numerofila);
                editarEstado = 4;
            }
            else if (e.ColumnIndex == 7)
            {
                if (seleccionadoDato == 0)
                {
                    seleccionadoDato = 1;
                    numerofila = e.RowIndex;
                    obtenerDatosDGVProductos(numerofila);
                    origenDeLosDatos = 2;
                }
                btnAgregarProducto.PerformClick();
            }
            else if (e.ColumnIndex == 8)
            {
                index = 0;
                DialogResult result = MessageBox.Show("Realmente desdea Modificar el estado?", "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    numerofila = e.RowIndex;
                    obtenerDatosDGVProductos(numerofila);
                    status = DGVProductos.Rows[numerofila].Cells["Column14"].Value.ToString();
                    ModificarStatusProducto();
                    if (status == "1")
                    {
                        cbMostrar.Text = "Deshabilitados";
                    }
                    else if (status == "0")
                    {
                        cbMostrar.Text = "Habilitados";
                    }
                    DGVProductos.Refresh();
                }
            }
            else if (e.ColumnIndex == 9)
            {
                numerofila = e.RowIndex;
                obtenerDatosDGVProductos(numerofila);
                ViewRecordProducto();
            }
            else if (e.ColumnIndex == 10)
            {
                string codiBarProd = "";
                numfila = e.RowIndex;
                obtenerDatosDGVProductos(numfila);
                //MessageBox.Show("Proceso de construccion de Codigo de Barras", "En Proceso de Construccion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MakeBarCode.FormClosed += delegate
                {

                };
                if (!MakeBarCode.Visible)
                {
                    MakeBarCode.NombreProd = Nombre;
                    MakeBarCode.PrecioProd = Precio;
                    codiBarProd = CodigoBarras;
                    if (codiBarProd != "")
                    {
                        MakeBarCode.CodigoBarProd = codiBarProd;
                        MakeBarCode.ShowDialog();
                    }
                    else if (codiBarProd == "")
                    {
                        MessageBox.Show("No se puede generar el codigo de barras\nPuesto que no tiene codigo de barras asignado", "Error de Generar Codigo de Barras", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MakeBarCode.NombreProd = Nombre;
                    MakeBarCode.PrecioProd = Precio;
                    codiBarProd = CodigoBarras;
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
            else if (e.ColumnIndex == 11)
            {
                numfila = e.RowIndex;
                obtenerDatosDGVProductos(numfila);

                string pathString;

                pathString = savePath;

                if (pathString != "")
                {
                    mostrarFoto();
                }
                else if (pathString == "")
                {
                    agregarFoto();
                }
            }
            else if (e.ColumnIndex == 12)
            {
                numerofila = e.RowIndex;
                obtenerDatosDGVProductos(numerofila);
                MakeTagProd.FormClosed += delegate
                {

                };
                if (!MakeTagProd.Visible)
                {
                    MakeTagProd.NombreProd = Nombre;
                    MakeTagProd.PrecioProd = float.Parse(Precio);
                    MakeTagProd.CodigoBarProd = CodigoBarras;
                    MakeTagProd.ShowDialog();
                }
                else
                {
                    MakeTagProd.NombreProd = Nombre;
                    MakeTagProd.PrecioProd = float.Parse(Precio);
                    MakeTagProd.CodigoBarProd = CodigoBarras;
                    MakeTagProd.BringToFront();
                }
            }
            else if (e.ColumnIndex == 13)
            {
                if (seleccionadoDato == 0)
                {
                    seleccionadoDato = 1;
                    numerofila = e.RowIndex;
                    obtenerDatosDGVProductos(numerofila);
                    origenDeLosDatos = 4;
                }
                btnAgregarProducto.PerformClick();
            }
        }

        private void obtenerIDProdServPaq()
        {
            string queryIDProdServPaq = $"SELECT * FROM Productos WHERE Nombre = '{Nombre}' AND Stock = '{Stock}' AND ClaveInterna = '{ClaveInterna}' AND CodigoBarras = '{CodigoBarras}' AND IDUsuario = '{FormPrincipal.userID}'";
            DataTable idProdServPaq;
            idProdServPaq = cn.CargarDatos(queryIDProdServPaq);
            ID_ProdSerPaq = idProdServPaq.Rows[0]["ID"].ToString();
        }

        public void limpiarDGV()
        {
            if (DGVProductos.DataSource is DataTable)
            {
                ((DataTable)DGVProductos.DataSource).Rows.Clear();
                DGVProductos.Refresh();
            }
        }

        private void btnModificarEstado_Click(object sender, EventArgs e)
        {
            if (editarEstado == 4 && Convert.ToBoolean(DGVProductos.Rows[numerofila].Cells[0].Value) == true)
            {
                //MessageBox.Show("Proceso de Cambiar el estado del\nProducto: " + ProductoNombre, "Proceso de Actividades", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                string status;
                DialogResult result = MessageBox.Show("Desdea Realmente Modificar el Estatus del\nProducto: " + Nombre + "\nde su Stock Existente", "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    status = DGVProductos.Rows[numerofila].Cells["Column14"].Value.ToString();
                    ModificarStatusProductoChkBox();
                    if (status == "1")
                    {
                        cbMostrar.Text = "Deshabilitados";
                    }
                    else if (status == "0")
                    {
                        cbMostrar.Text = "Habilitados";
                    }
                    DGVProductos.Refresh();
                }
                else if (result == DialogResult.No)
                {
                    status = cbMostrar.Text;
                    DGVProductos.Rows[numerofila].Cells[0].Value = false;
                    if (status == "Habilitados")
                    {
                        DGVProductos.Refresh();
                    }
                    else if (status == "Deshabilitados")
                    {
                        DGVProductos.Refresh();
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    DGVProductos.Rows[numerofila].Cells[0].Value = false;
                    DGVProductos.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Favor de seleccionar (Marcar un)\nalgun CheckBox (Casilla de Verificación)", "Verificar Selección", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void obtenerDatosDGVProductos(int fila)
        {
            Nombre = DGVProductos.Rows[fila].Cells["Column1"].Value.ToString();
            Stock = DGVProductos.Rows[fila].Cells["Column2"].Value.ToString();
            Precio = DGVProductos.Rows[fila].Cells["Column3"].Value.ToString();
            ProductoCategoria = DGVProductos.Rows[fila].Cells["Column4"].Value.ToString();
            ClaveInterna = DGVProductos.Rows[fila].Cells["Column5"].Value.ToString();
            CodigoBarras = DGVProductos.Rows[fila].Cells["Column6"].Value.ToString();
            savePath = DGVProductos.Rows[fila].Cells["Column15"].Value.ToString();
            id = FormPrincipal.userID.ToString();
        }

        private void cbMostrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtro = Convert.ToString(cbMostrar.SelectedItem);      // tomamos el valor que se elige en el TextBox
            if (filtro == "Habilitados")                            // comparamos si el valor a filtrar es Habilitados
            {
                if (panelShowDGVProductosView.Visible == true)
                {
                    CargarDatosActivos();
                }
                else if (panelShowPhotoView.Visible == true)
                {
                    searchPhotoProdActivo();
                    photoShow();
                }
            }
            else if (filtro == "Deshabilitados")                    // comparamos si el valor a filtrar es Deshabilitados
            {
                if (panelShowDGVProductosView.Visible == true)
                {
                    CargarDatosInactivos();
                }
                else if (panelShowPhotoView.Visible == true)
                {
                    searchPhotoProdInactivo();
                    photoShow();
                }
            }
            else if (filtro == "Todos")                             // comparamos si el valor a filtrar es Todos
            {
                if (panelShowDGVProductosView.Visible == true)
                {
                    CargarDatos();                                      // cargamos todos los registros
                }
                else if (panelShowPhotoView.Visible == true)
                {
                    searchPhotoProd();
                    photoShow();
                }
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            //dtConsulta.DefaultView.RowFilter = $"Nombre LIKE '{txtBusqueda.Text}%'";
            string buscarStock;
            if (panelShowDGVProductosView.Visible == true)
            {
                CargarDatosBusqueda(txtBusqueda.Text);
            }
            else if (panelShowPhotoView.Visible == true)
            {
                buscarStock = $"SELECT prod.ID, prod.Nombre, prod.ProdImage, prod.Precio FROM Productos prod WHERE prod.IDUsuario = '{FormPrincipal.userID}' AND prod.Nombre LIKE '%" + txtBusqueda.Text + "%'";
                fotos = cn.CargarDatos(buscarStock);
                photoShow();
            }
        }

        public Productos()
        {
            InitializeComponent();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            panelShowPhotoView.Visible = false;
            panelShowDGVProductosView.Visible = true;

            CargarDatos();
            cbOrden.SelectedIndex = 0;
            cbOrden.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMostrar.SelectedIndex = 0;
            cbMostrar.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void CargarDatos()
        {
            //cn.CargarInformacion(cs.Productos(FormPrincipal.userID), DGVProductos);
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            //sql_con = new SQLiteConnection("Data source=" + rutaLocal + @"\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT P.Nombre, P.Stock, P.Precio, P.Categoria, P.ClaveInterna, P.CodigoBarras, P.Status, P.ProdImage, P.Tipo FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = '{FormPrincipal.userID}'", sql_con);
            dr = sql_cmd.ExecuteReader();

            //limpiarDGV();
            DGVProductos.Rows.Clear();

            while (dr.Read())
            {
                number_of_rows = DGVProductos.Rows.Add();

                DataGridViewRow row = DGVProductos.Rows[number_of_rows];

                string TipoProd = dr.GetValue(dr.GetOrdinal("Tipo")).ToString();

                row.Cells["Column1"].Value = dr.GetValue(dr.GetOrdinal("Nombre"));
                if (TipoProd == "P")
                {
                    row.Cells["Column2"].Value = dr.GetValue(dr.GetOrdinal("Stock"));
                }
                else if (TipoProd == "S")
                {
                    row.Cells["Column2"].Value = "";
                }
                row.Cells["Column3"].Value = dr.GetValue(dr.GetOrdinal("Precio"));
                row.Cells["Column4"].Value = dr.GetValue(dr.GetOrdinal("Categoria"));
                row.Cells["Column5"].Value = dr.GetValue(dr.GetOrdinal("ClaveInterna"));
                row.Cells["Column6"].Value = dr.GetValue(dr.GetOrdinal("CodigoBarras"));
                row.Cells["Column14"].Value = dr.GetValue(dr.GetOrdinal("Status"));
                row.Cells["Column15"].Value = dr.GetValue(dr.GetOrdinal("ProdImage"));

                Image editar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\pencil.png");
                Image estado1 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\check.png");
                Image estado2 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\close.png");
                Image historial = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\line-chart.png");
                Image generar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\barcode.png");
                Image imagen1 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-o.png");
                Image imagen2 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-picture-o.png");
                Image etiqueta = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\tag.png");
                Image copy = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\copy.png");
                Image package = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\Servicio.png");
                Image product = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\Producto.png");

                row.Cells["Column7"].Value = editar;

                string estado = dr.GetValue(dr.GetOrdinal("Status")).ToString();
                if (estado == "1")
                {
                    row.Cells["Column8"].Value = estado1;
                }
                else if (estado == "0")
                {
                    row.Cells["Column8"].Value = estado2;
                }

                row.Cells["Column9"].Value = historial;

                row.Cells["Column10"].Value = generar;

                string ImgPath = dr.GetValue(dr.GetOrdinal("ProdImage")).ToString();
                if (ImgPath == "" || ImgPath == null)
                {
                    row.Cells["Column11"].Value = imagen1;
                }
                else if (ImgPath != "" || ImgPath != null)
                {
                    row.Cells["Column11"].Value = imagen2;
                }

                row.Cells["Column12"].Value = etiqueta;
                
                row.Cells["Column13"].Value = copy;
                
                
                if (TipoProd == "P")
                {
                    row.Cells["Column16"].Value = product;
                }
                else if (TipoProd == "S")
                {
                    row.Cells["Column16"].Value = package;
                }
            }
            dr.Close();
            sql_con.Close();
        }

        private void CargarDatosActivos()
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT P.Nombre, P.Stock, P.Precio, P.Categoria, P.ClaveInterna, P.CodigoBarras, P.Status, P.ProdImage, P.Tipo FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = '{FormPrincipal.userID}' AND P.Status = 1", sql_con);
            dr = sql_cmd.ExecuteReader();

            //limpiarDGV();
            DGVProductos.Rows.Clear();

            while (dr.Read())
            {
                number_of_rows = DGVProductos.Rows.Add();

                DataGridViewRow row = DGVProductos.Rows[number_of_rows];

                string TipoProd = dr.GetValue(dr.GetOrdinal("Tipo")).ToString();

                row.Cells["Column1"].Value = dr.GetValue(dr.GetOrdinal("Nombre"));
                if (TipoProd == "P")
                {
                    row.Cells["Column2"].Value = dr.GetValue(dr.GetOrdinal("Stock"));
                }
                else if (TipoProd == "S")
                {
                    row.Cells["Column2"].Value = "";
                }
                row.Cells["Column3"].Value = dr.GetValue(dr.GetOrdinal("Precio"));
                row.Cells["Column4"].Value = dr.GetValue(dr.GetOrdinal("Categoria"));
                row.Cells["Column5"].Value = dr.GetValue(dr.GetOrdinal("ClaveInterna"));
                row.Cells["Column6"].Value = dr.GetValue(dr.GetOrdinal("CodigoBarras"));
                row.Cells["Column14"].Value = dr.GetValue(dr.GetOrdinal("Status"));
                row.Cells["Column15"].Value = dr.GetValue(dr.GetOrdinal("ProdImage"));

                Image editar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\pencil.png");
                Image estado1 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\check.png");
                Image estado2 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\close.png");
                Image historial = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\line-chart.png");
                Image generar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\barcode.png");
                Image imagen1 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-o.png");
                Image imagen2 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-picture-o.png");
                Image etiqueta = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\tag.png");
                Image copy = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\copy.png");
                Image package = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\Servicio.png");
                Image product = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\Producto.png");

                row.Cells["Column7"].Value = editar;

                string estado = dr.GetValue(dr.GetOrdinal("Status")).ToString();
                if (estado == "1")
                {
                    row.Cells["Column8"].Value = estado1;
                }
                else if (estado == "0")
                {
                    row.Cells["Column8"].Value = estado2;
                }

                row.Cells["Column9"].Value = historial;

                row.Cells["Column10"].Value = generar;

                string ImgPath = dr.GetValue(dr.GetOrdinal("ProdImage")).ToString();
                if (ImgPath == "" || ImgPath == null)
                {
                    row.Cells["Column11"].Value = imagen1;
                }
                else if (ImgPath != "" || ImgPath != null)
                {
                    row.Cells["Column11"].Value = imagen2;
                }

                row.Cells["Column12"].Value = etiqueta;

                row.Cells["Column13"].Value = copy;

                if (TipoProd == "P")
                {
                    row.Cells["Column16"].Value = product;
                }
                else if (TipoProd == "S")
                {
                    row.Cells["Column16"].Value = package;
                }
            }
            dr.Close();
            sql_con.Close();
        }

        private void CargarDatosBusqueda(string busqueda)
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT P.Nombre, P.Stock, P.Precio, P.Categoria, P.ClaveInterna, P.CodigoBarras, P.Status, P.ProdImage, P.Tipo FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = '{FormPrincipal.userID}' AND P.Nombre LIKE '%" + busqueda + "%' ", sql_con);
            dr = sql_cmd.ExecuteReader();

            //limpiarDGV();
            DGVProductos.Rows.Clear();

            while (dr.Read())
            {
                number_of_rows = DGVProductos.Rows.Add();

                DataGridViewRow row = DGVProductos.Rows[number_of_rows];

                string TipoProd = dr.GetValue(dr.GetOrdinal("Tipo")).ToString();

                row.Cells["Column1"].Value = dr.GetValue(dr.GetOrdinal("Nombre"));
                if (TipoProd == "P")
                {
                    row.Cells["Column2"].Value = dr.GetValue(dr.GetOrdinal("Stock"));
                }
                else if (TipoProd == "S")
                {
                    row.Cells["Column2"].Value = "";
                }
                row.Cells["Column3"].Value = dr.GetValue(dr.GetOrdinal("Precio"));
                row.Cells["Column4"].Value = dr.GetValue(dr.GetOrdinal("Categoria"));
                row.Cells["Column5"].Value = dr.GetValue(dr.GetOrdinal("ClaveInterna"));
                row.Cells["Column6"].Value = dr.GetValue(dr.GetOrdinal("CodigoBarras"));
                row.Cells["Column14"].Value = dr.GetValue(dr.GetOrdinal("Status"));
                row.Cells["Column15"].Value = dr.GetValue(dr.GetOrdinal("ProdImage"));

                Image editar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\pencil.png");
                Image estado1 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\check.png");
                Image estado2 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\close.png");
                Image historial = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\line-chart.png");
                Image generar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\barcode.png");
                Image imagen1 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-o.png");
                Image imagen2 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-picture-o.png");
                Image etiqueta = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\tag.png");
                Image copy = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\copy.png");
                Image package = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\Servicio.png");
                Image product = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\Producto.png");

                row.Cells["Column7"].Value = editar;

                string estado = dr.GetValue(dr.GetOrdinal("Status")).ToString();
                if (estado == "1")
                {
                    row.Cells["Column8"].Value = estado1;
                }
                else if (estado == "0")
                {
                    row.Cells["Column8"].Value = estado2;
                }

                row.Cells["Column9"].Value = historial;

                row.Cells["Column10"].Value = generar;

                string ImgPath = dr.GetValue(dr.GetOrdinal("ProdImage")).ToString();
                if (ImgPath == "" || ImgPath == null)
                {
                    row.Cells["Column11"].Value = imagen1;
                }
                else if (ImgPath != "" || ImgPath != null)
                {
                    row.Cells["Column11"].Value = imagen2;
                }

                row.Cells["Column12"].Value = etiqueta;

                row.Cells["Column13"].Value = copy;

                if (TipoProd == "P")
                {
                    row.Cells["Column16"].Value = product;
                }
                else if (TipoProd == "S")
                {
                    row.Cells["Column16"].Value = package;
                }
            }
            dr.Close();
            sql_con.Close();
        }

        private void CargarDatosInactivos()
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT P.Nombre, P.Stock, P.Precio, P.Categoria, P.ClaveInterna, P.CodigoBarras, P.Status, P.ProdImage, P.Tipo FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = '{FormPrincipal.userID}' AND P.Status = 0", sql_con);
            dr = sql_cmd.ExecuteReader();

            //limpiarDGV();
            DGVProductos.Rows.Clear();

            while (dr.Read())
            {
                number_of_rows = DGVProductos.Rows.Add();

                DataGridViewRow row = DGVProductos.Rows[number_of_rows];

                string TipoProd = dr.GetValue(dr.GetOrdinal("Tipo")).ToString();

                row.Cells["Column1"].Value = dr.GetValue(dr.GetOrdinal("Nombre"));
                if (TipoProd == "P")
                {
                    row.Cells["Column2"].Value = dr.GetValue(dr.GetOrdinal("Stock"));
                }
                else if (TipoProd == "S")
                {
                    row.Cells["Column2"].Value = "";
                }
                row.Cells["Column3"].Value = dr.GetValue(dr.GetOrdinal("Precio"));
                row.Cells["Column4"].Value = dr.GetValue(dr.GetOrdinal("Categoria"));
                row.Cells["Column5"].Value = dr.GetValue(dr.GetOrdinal("ClaveInterna"));
                row.Cells["Column6"].Value = dr.GetValue(dr.GetOrdinal("CodigoBarras"));
                row.Cells["Column14"].Value = dr.GetValue(dr.GetOrdinal("Status"));
                row.Cells["Column15"].Value = dr.GetValue(dr.GetOrdinal("ProdImage"));

                Image editar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\pencil.png");
                Image estado1 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\check.png");
                Image estado2 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\close.png");
                Image historial = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\line-chart.png");
                Image generar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\barcode.png");
                Image imagen1 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-o.png");
                Image imagen2 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-picture-o.png");
                Image etiqueta = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\tag.png");
                Image copy = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\copy.png");
                Image package = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\Servicio.png");
                Image product = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\Producto.png");

                row.Cells["Column7"].Value = editar;

                string estado = dr.GetValue(dr.GetOrdinal("Status")).ToString();
                if (estado == "1")
                {
                    row.Cells["Column8"].Value = estado1;
                }
                else if (estado == "0")
                {
                    row.Cells["Column8"].Value = estado2;
                }

                row.Cells["Column9"].Value = historial;

                row.Cells["Column10"].Value = generar;

                string ImgPath = dr.GetValue(dr.GetOrdinal("ProdImage")).ToString();
                if (ImgPath == "" || ImgPath == null)
                {
                    row.Cells["Column11"].Value = imagen1;
                }
                else if (ImgPath != "" || ImgPath != null)
                {
                    row.Cells["Column11"].Value = imagen2;
                }

                row.Cells["Column12"].Value = etiqueta;

                row.Cells["Column13"].Value = copy;

                if (TipoProd == "P")
                {
                    row.Cells["Column16"].Value = product;
                }
                else if (TipoProd == "S")
                {
                    row.Cells["Column16"].Value = package;
                }
            }
            dr.Close();
            sql_con.Close();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (origenDeLosDatos == 0)
            {
                FormAgregar.DatosSource = 1;
            }
            if (origenDeLosDatos == 2)
            {
                FormAgregar.DatosSource = 2;
            }
            if (origenDeLosDatos == 4)
            {
                FormAgregar.DatosSource = 4;
            }

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
                if (seleccionadoDato == 0)
                {
                    FormAgregar.ProdNombre = "";
                    FormAgregar.ShowDialog();
                }
                else if (seleccionadoDato == 1)
                {
                    seleccionadoDato = 0;
                    FormAgregar.ProdNombre = Nombre;
                    FormAgregar.ProdStock = Stock;
                    FormAgregar.ProdPrecio = Precio;
                    FormAgregar.ProdCategoria = ProductoCategoria;
                    FormAgregar.ProdClaveInterna = ClaveInterna;
                    FormAgregar.ProdCodBarras = CodigoBarras;
                    FormAgregar.ShowDialog();
                }
            }
            else
            {
                if (seleccionadoDato == 0)
                {
                    FormAgregar.ProdNombre = "";
                    FormAgregar.ShowDialog();
                }
                else if (seleccionadoDato == 1)
                {
                    seleccionadoDato = 0;
                    FormAgregar.ProdNombre = Nombre;
                    FormAgregar.ProdStock = Stock;
                    FormAgregar.ProdPrecio = Precio;
                    FormAgregar.ProdCategoria = ProductoCategoria;
                    FormAgregar.ProdClaveInterna = ClaveInterna;
                    FormAgregar.ProdCodBarras = CodigoBarras;
                    FormAgregar.ShowDialog();
                }
            }
            origenDeLosDatos = 0;
        }

        private void ModificarStatusProducto()
        {
            DataRow row;
            // Preparamos el Query que haremos segun la fila seleccionada
            buscar = $"SELECT * FROM Productos WHERE Nombre = '{Nombre}' AND Precio = '{Precio}' AND Stock = '{Stock}' AND ClaveInterna = '{ClaveInterna}' AND CodigoBarras = '{CodigoBarras}' AND IDUsuario = '{id}'";
            dt = cn.CargarDatos(buscar);    // almacenamos el resultado de la Funcion CargarDatos que esta en la calse Consultas
            row = dt.Rows[0];
            Id_Prod_select = Convert.ToString(row["ID"]);       // almacenamos el Id del producto
            status = Convert.ToString(row["Status"]);           // almacenamos el status
            if (status == "0")                              // si el status es 0
            {
                // preparamos el Query 
                buscar = $"UPDATE Productos SET Status = '1' WHERE ID = '{Id_Prod_select}' AND IDUsuario = '{id}'";
                dtConsulta = cn.CargarDatos(buscar);                    // acutualizamos los datos
            }
            else if (status == "1")                         // si el status es 1
            {
                // preparamos el Query 
                buscar = $"UPDATE Productos SET Status = '0' WHERE ID = '{Id_Prod_select}' AND IDUsuario = '{id}'";
                dtConsulta = cn.CargarDatos(buscar);                    // acutualizamos los datos
            }
        }

        private void ModificarStatusProductoChkBox()
        {
            DataRow row;
            // Preparamos el Query que haremos segun la fila seleccionada
            buscar = $"SELECT * FROM Productos WHERE Nombre = '{Nombre}' AND Precio = '{Precio}' AND Stock = '{Stock}' AND ClaveInterna = '{ClaveInterna}' AND CodigoBarras = '{CodigoBarras}' AND IDUsuario = '{id}'";
            dt = cn.CargarDatos(buscar);    // almacenamos el resultado de la Funcion CargarDatos que esta en la calse Consultas
            row = dt.Rows[0];
            Id_Prod_select = Convert.ToString(row["ID"]);       // almacenamos el Id del producto
            status = Convert.ToString(row["Status"]);           // almacenamos el status
            if (status == "0")                              // si el status es 0
            {
                // preparamos el Query 
                buscar = $"UPDATE Productos SET Status = '1' WHERE ID = '{Id_Prod_select}' AND IDUsuario = '{id}'";
                dtConsulta = cn.CargarDatos(buscar);                    // acutualizamos los datos
                //cn.EjecutarConsulta(buscar);                              // acutualizamos los datos
            }
            else if (status == "1")                         // si el status es 1
            {
                // preparamos el Query 
                buscar = $"UPDATE Productos SET Status = '0' WHERE ID = '{Id_Prod_select}' AND IDUsuario = '{id}'";
                dtConsulta = cn.CargarDatos(buscar);                    // acutualizamos los datos
                //cn.EjecutarConsulta(buscar);                              // acutualizamos los datos
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
                if (f.CheckFileExists && f.FileName != "")      // si el archivo existe
                {
                    try     // Intentamos la actualizacion de la imagen en la base de datos
                    {
                        // Obtenemos el Nuevo nombre de la imagen
                        // con la que se va hacer la copia de la imagen
                        var source = fileName;
                        var replacement = source.Replace('/', '_').Replace('\\', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('\"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_').Replace('-', '_').Replace(' ', '_');
                        NvoFileName = replacement;
                        //NvoFileName = replacement + ".jpg";
                        //NvoFileName = fileName;
                        // hacemos la nueva cadena de consulta para hacer el UpDate
                        string insertarImagen = $"UPDATE Productos SET ProdImage = '{NvoFileName}' WHERE Nombre = '{Nombre}' AND Stock = '{Stock}' AND Precio = '{Precio}' AND ClaveInterna = '{ClaveInterna}' AND CodigoBarras = '{CodigoBarras}'";
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
