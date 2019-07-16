using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PuntoDeVentaV2
{
    public partial class Productos : Form
    {
        public string rutaLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static int proveedorElegido = 0;
        public static bool generarIdReporte = false;
        public static int idReporte = 0;

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
                btn.Font = new System.Drawing.Font("Tahoma", 14, FontStyle.Bold | FontStyle.Italic);
                btn.TextAlign = ContentAlignment.TopCenter;
                if (row["ProdImage"].ToString() == "" || row["ProdImage"].ToString() == null)
                {
                    btn.ForeColor = Color.Red;
                    using (fs = new FileStream(fileSavePath + @"\no-image.png", FileMode.Open))
                    {
                        btn.Image = System.Drawing.Image.FromStream(fs);
                        btn.Image = new Bitmap(btn.Image, btn.Size);
                    }
                }
                else if (row["ProdImage"].ToString() != "" || row["ProdImage"].ToString() != null)
                {
                    btn.ForeColor = Color.Red;
                    using (fs = new FileStream(fileSavePath + row["ProdImage"].ToString(), FileMode.Open))
                    {
                        btn.Image = System.Drawing.Image.FromStream(fs);
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
                System.Drawing.Rectangle cellRect = DGVProductos.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

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
            var fila = DGVProductos.CurrentCell.RowIndex;

            int idProducto = Convert.ToInt32(DGVProductos.Rows[fila].Cells["_IDProducto"].Value);

            //Esta condicion es para que no de error al momento que se haga click en el header de la columna por error
            if (e.RowIndex >= 0)
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
                else if (e.ColumnIndex == 17)
                {
                    //Esta es la columna de la opcion "Ajustar"
                    AjustarProducto ap = new AjustarProducto(idProducto);

                    ap.FormClosed += delegate
                    {
                        CargarDatos();
                        txtBusqueda.Text = string.Empty;
                        txtBusqueda.Focus();
                    };

                    ap.ShowDialog();
                }
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
                    CargarDatos();
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
                    CargarDatos(0);
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
                CargarDatos(1, txtBusqueda.Text);
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

            //Verifica si el checkbox de producto comprado fue marcado
            if (Properties.Settings.Default.opcionProductoComprado)
            {
                cbProductoComprado.Checked = true;
            }

            idReporte = cn.ObtenerUltimoIdReporte(FormPrincipal.userID) + 1;
        }

        private void CargarDatos(int status = 1, string busqueda = "")
        {
            //Para la ventana de ajustar producto cuando el checkbox producto comprado esta marcado
            bool abierta = true;
            int idProducto = 0;
            string extra = string.Empty;

            if (string.IsNullOrWhiteSpace(busqueda))
            {
                abierta = false;
            }
            else
            {
                extra = $"AND (P.Nombre LIKE '%{busqueda}%' OR P.CodigoBarras LIKE '%{busqueda}%')";
            }

            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            //AND (P.Nombre LIKE '%{busqueda}%' OR P.CodigoBarras LIKE '%{busqueda}%')

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT * FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = {FormPrincipal.userID} AND P.Status = {status} {extra}", sql_con);
            dr = sql_cmd.ExecuteReader();

            DGVProductos.Rows.Clear();

            while (dr.Read())
            {
                number_of_rows = DGVProductos.Rows.Add();

                DataGridViewRow row = DGVProductos.Rows[number_of_rows];

                idProducto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID")));

                row.Cells["_IDProducto"].Value = idProducto;

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

                System.Drawing.Image editar = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\pencil.png");
                System.Drawing.Image estado1 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\check.png");
                System.Drawing.Image estado2 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\close.png");
                System.Drawing.Image historial = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\line-chart.png");
                System.Drawing.Image generar = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\barcode.png");
                System.Drawing.Image imagen1 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-o.png");
                System.Drawing.Image imagen2 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-picture-o.png");
                System.Drawing.Image etiqueta = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\tag.png");
                System.Drawing.Image copy = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\copy.png");
                System.Drawing.Image package = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\Servicio.png");
                System.Drawing.Image product = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\Producto.png");
                System.Drawing.Image ajustar = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\cog.png");

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

                row.Cells["Ajustar"].Value = ajustar;
            }

            dr.Close();
            sql_con.Close();

            if (abierta)
            {
                if (cbProductoComprado.Checked)
                {
                    AjustarProducto ap = new AjustarProducto(idProducto);

                    ap.FormClosed += delegate
                    {
                        CargarDatos();
                        txtBusqueda.Text = string.Empty;
                        txtBusqueda.Focus();
                        generarIdReporte = false;
                    };

                    generarIdReporte = true;

                    ap.ShowDialog();
                }

                abierta = false;
            }
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

        private void cbProductoComprado_CheckedChanged(object sender, EventArgs e)
        {
            bool estado = false;

            if (cbProductoComprado.Checked)
            {
                estado = true;
            }
            else
            {
                var respuesta = MessageBox.Show("Generar reporte?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    GenerarTicket(idReporte);
                }

                idReporte++;
            }

            Properties.Settings.Default.opcionProductoComprado = estado;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }


        private void GenerarTicket(int idReporte)
        {
            var datos = FormPrincipal.datosUsuario;

            var colorFuenteNegrita = new BaseColor(Color.Black);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            int anchoLogo = 110;
            int altoLogo = 60;

            var rutaArchivo = @"C:\Archivos PUDVE\Reportes\Historial\reporte_"+ idReporte +".pdf";

            //float[] anchoColumnas = new float[] { 10f, 24f, 9f, 9f };

            Document reporte = new Document(PageSize.A3);
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            string logotipo = datos[11];
            //string encabezado = $"\n{datos[1]} {datos[2]} {datos[3]}, {datos[4]}, {datos[5]}\nCol. {datos[6]} C.P. {datos[7]}\nRFC: {datos[8]}\n{datos[9]}\nTel. {datos[10]}\n\n";

            reporte.Open();

            //Validación para verificar si existe logotipo
            if (logotipo != "")
            {
                logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + logotipo;

                if (System.IO.File.Exists(logotipo))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logotipo);
                    logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    logo.ScaleAbsolute(anchoLogo, altoLogo);
                    reporte.Add(logo);
                }
            }

            Paragraph titulo = new Paragraph(datos[0] + "\n\n", fuenteGrande);
            //Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            //domicilio.Alignment = Element.ALIGN_CENTER;
            //domicilio.SetLeading(10, 0);

            /***************************************
             ** Tabla con los productos ajustados **
             ***************************************/
            PdfPTable tabla = new PdfPTable(7);
            tabla.WidthPercentage = 100;
            //tabla.SetWidths(anchoColumnas);

            PdfPCell colProveedor = new PdfPCell(new Phrase("Proveedor", fuenteNegrita));
            colProveedor.BorderWidth = 1;
            colProveedor.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colUnidades = new PdfPCell(new Phrase("Unidades compradas", fuenteNegrita));
            colUnidades.BorderWidth = 1;
            colUnidades.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colPrecioCompra = new PdfPCell(new Phrase("Precio compra", fuenteNegrita));
            colPrecioCompra.BorderWidth = 1;
            colPrecioCompra.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colPrecioVenta = new PdfPCell(new Phrase("Precio venta", fuenteNegrita));
            colPrecioVenta.BorderWidth = 1;
            colPrecioVenta.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colStock = new PdfPCell(new Phrase("Stock actual", fuenteNegrita));
            colStock.BorderWidth = 1;
            colStock.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colFechaCompra = new PdfPCell(new Phrase("Fecha de compra", fuenteNegrita));
            colFechaCompra.BorderWidth = 1;
            colFechaCompra.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colFechaOperacion = new PdfPCell(new Phrase("Fecha de operación", fuenteNegrita));
            colFechaOperacion.BorderWidth = 1;
            colFechaOperacion.HorizontalAlignment = Element.ALIGN_CENTER;

            tabla.AddCell(colProveedor);
            tabla.AddCell(colUnidades);
            tabla.AddCell(colPrecioCompra);
            tabla.AddCell(colPrecioVenta);
            tabla.AddCell(colStock);
            tabla.AddCell(colFechaCompra);
            tabla.AddCell(colFechaOperacion);


            //Consulta para obtener los registros del Historial de compras
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT * FROM HistorialCompras WHERE IDUsuario = {FormPrincipal.userID} AND IDReporte = {idReporte}", sql_con);
            dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                var idProducto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDProducto")));
                var proveedor = dr.GetValue(dr.GetOrdinal("NomEmisor")).ToString();
                var unidades = dr.GetValue(dr.GetOrdinal("Cantidad")).ToString();
                var compra = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ValorUnitario"))).ToString("0.00");
                var venta = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Precio"))).ToString("0.00");

                var tmp = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                var stock = tmp[4];

                DateTime fecha = (DateTime)dr.GetValue(dr.GetOrdinal("FechaLarga"));
                var fechaCompra = fecha.ToString("yyyy-MM-dd");

                DateTime fechaOp = (DateTime)dr.GetValue(dr.GetOrdinal("FechaOperacion"));
                var fechaOperacion = fechaOp.ToString("yyyy-MM-dd HH:mm tt");

                PdfPCell colProveedorTmp = new PdfPCell(new Phrase(proveedor, fuenteNormal));
                colProveedorTmp.BorderWidth = 1;
                colProveedorTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colUnidadesTmp = new PdfPCell(new Phrase(unidades, fuenteNormal));
                colUnidadesTmp.BorderWidth = 1;
                colUnidadesTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioCompraTmp = new PdfPCell(new Phrase("$" + compra, fuenteNormal));
                colPrecioCompraTmp.BorderWidth = 1;
                colPrecioCompraTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioVentaTmp = new PdfPCell(new Phrase("$" + venta, fuenteNormal));
                colPrecioVentaTmp.BorderWidth = 1;
                colPrecioVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockTmp = new PdfPCell(new Phrase(stock, fuenteNormal));
                colStockTmp.BorderWidth = 1;
                colStockTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaCompraTmp = new PdfPCell(new Phrase(fechaCompra, fuenteNormal));
                colFechaCompraTmp.BorderWidth = 1;
                colFechaCompraTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaOperacionTmp = new PdfPCell(new Phrase(fechaOperacion, fuenteNormal));
                colFechaOperacionTmp.BorderWidth = 1;
                colFechaOperacionTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tabla.AddCell(colProveedorTmp);
                tabla.AddCell(colUnidadesTmp);
                tabla.AddCell(colPrecioCompraTmp);
                tabla.AddCell(colPrecioVentaTmp);
                tabla.AddCell(colStockTmp);
                tabla.AddCell(colFechaCompraTmp);
                tabla.AddCell(colFechaOperacionTmp);
            }

            /******************************************
             ** Fin de la tabla                      **
             ******************************************/

            reporte.Add(titulo);
            //reporte.Add(domicilio);
            reporte.Add(tabla);

            reporte.AddTitle("Reporte Historial");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }
    }
}
