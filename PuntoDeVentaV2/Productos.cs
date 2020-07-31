using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Linq;
namespace PuntoDeVentaV2
{
    public partial class Productos : Form
    {
        string strTag = string.Empty,
                path = string.Empty,
                saveDirectoryFile = string.Empty,
                line = string.Empty,
                fileReportOrder = string.Empty;

        bool isEmptyAuxWord,
            isEmptySetUpVariable,
            isEmptySetUpDinamicos;

        int Ancho = 80, Alto = 19, usrNo;
        Size size, strSize;

        string[] words;

        char[] delimiter = { '|' };

        private Paginar p;
        string DataMemberDGV = "Productos";
        string extra = string.Empty, extra2 = string.Empty;
        int maximo_x_pagina = 17;
        int clickBoton = 0;

        public string rutaLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static int proveedorElegido = 0;
        public static int idReporte = 0;
        public static bool botonAceptar = false;
        public static bool recargarDatos = false;
        // Este array es para guardar los productos seleccionados que seran tomados
        // en cuenta para el boton de "Asignar"
        public static Dictionary<int, string> productosSeleccionados;
        // Variables para saber si uso el boton de cambiar tipo
        public int idProductoCambio { get; set; }
        public bool cambioProducto { get; set; }

        //public AgregarEditarProducto FormAgregar = new AgregarEditarProducto("Agregar");
        public AgregarStockXML FormXML = new AgregarStockXML();
        public RecordViewProduct ProductoRecord = new RecordViewProduct();
        public CodeBarMake MakeBarCode = new CodeBarMake();
        public photoShow VentanaMostrarFoto = new photoShow();
        public TagMake MakeTagProd = new TagMake();
        public VentanaDetalleFotoProducto ProductoDetalle = new VentanaDetalleFotoProducto();
        public DetalleDescripcion Descripcion = new DetalleDescripcion();

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        int numfila, index, number_of_rows, i, seleccionadoDato, origenDeLosDatos = 0, editarEstado = 0, numerofila = 0;
        string Id_Prod_select, buscar, id, Nombre, Precio, Stock, ClaveInterna, CodigoBarras, status, ClaveProducto, UnidadMedida, filtro, idProductoEditar, impuestoProducto;

        DataTable dt, dtConsulta, fotos, registros;
        DataGridViewButtonColumn setup, record, barcode, foto, tag, copy;
        DataGridViewImageCell cell;

        Icon image;

        OpenFileDialog f;       // declaramos el objeto de OpenFileDialog

        // objeto para el manejo de las imagenes
        FileStream File, File1;
        FileInfo info;

        // direccion de la carpeta donde se va poner las imagenes
        string saveDirectoryImg = string.Empty;
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

        public float ImporteDatoExtra { get; set; }
        public float DescuentoDatoExtra { get; set; }
        public int CantidadDatoExtra { get; set; }

        static public float ImporteDatoExtraFinal;
        static public float DescuentoDatoExtraFinal;
        static public int CantidadDatoExtraFinal;

        static public string typeProduct;

        string filtroConSinFiltroAvanzado = string.Empty;

        string[] palabras;
        List<string> auxWord, setUpVariable, noEcontradoCodBar = new List<string>();
        List<Control> listVariables;

        Dictionary<string, Tuple<string, string, string, string>> setUpDinamicos = new Dictionary<string, Tuple<string, string, string, string>>();
        Dictionary<string, Tuple<string, string, string>> setUpFiltroDinamicos = new Dictionary<string, Tuple<string, string, string>>();
        Dictionary<int, int> listaCoincidenciasAux = new Dictionary<int, int>();

        public static iTextSharp.text.Image imgReporte;

        Timer actualizarDGVProductos = new Timer();

        string nuevoCodigoBarrasDeProducto = string.Empty;


        // Permisos botones
        int opcion1 = 1; // Agregar XML
        int opcion2 = 1; // Deshabilitar seleccionados
        int opcion3 = 1; // Cambiar tipo
        int opcion4 = 1; // Mostrar en lista
        int opcion5 = 1; // Boton asignar
        int opcion6 = 1; // Mostrar en mosaico
        int opcion7 = 1; // Boton etiqueta
        int opcion8 = 1; // Boton reporte
        int opcion9 = 1; // Boton imprimir

        int opcion10 = 1; // Agregar producto
        int opcion11 = 1; // Agregar combo
        int opcion12 = 1; // Agregar servicio
        int opcion13 = 1; // Boton filtro
        int opcion14 = 1; // Boton borrar filtro
        int opcion15 = 1; // Opcion editar
        int opcion16 = 1; // Opcion estado
        int opcion17 = 1; // Opcion historial
        int opcion18 = 1; // Generar codigo barras
        int opcion19 = 1; // Cargar imagen
        int opcion20 = 1; // Opcion etiqueta
        int opcion21 = 1; // Opcion copiar
        int opcion22 = 1; // Opcion ajustar


        //Este evento sirve para seleccionar mas de un checkbox al mismo tiempo sin que se desmarquen los demas
        private void DGVProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if ((bool)DGVProductos.SelectedRows[e.ColumnIndex].Cells["CheckProducto"].Value == false)
                {
                    DGVProductos.SelectedRows[e.ColumnIndex].Cells["CheckProducto"].Value = true;
                }
                else
                {
                    DGVProductos.SelectedRows[e.ColumnIndex].Cells["CheckProducto"].Value = false;
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

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            //p.primerPagina();
            //clickBoton = 1;
            //CargarDatos();
            //actualizar();
        }

        private void linkLblUltimaPagina_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //p.ultimaPagina();
            //clickBoton = 1;
            //CargarDatos();
            //actualizar();
        }

        private void linkLblPaginaAnterior_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //p.atras();
            //clickBoton = 1;
            //CargarDatos();
            //actualizar();
        }

        private void linkLblPaginaSiguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void searchPhotoProdActivo()
        {
            queryFotos = $"SELECT prod.ID, prod.Nombre, prod.ProdImage, prod.Precio, prod.Status FROM Productos prod WHERE prod.IDUsuario = '{FormPrincipal.userID}' AND prod.Status = 1";
            fotos = cn.CargarDatos(queryFotos);
        }

        private void txtMaximoPorPagina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
                p.actualizarTope(maximo_x_pagina);
                CargarDatos();
                actualizar();
            }
        }

        private void DGVProductos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var apartado = cbMostrar.SelectedItem.ToString();

            if (apartado.Equals("Deshabilitados"))
            {
                return;
            }

            var fila = DGVProductos.CurrentCell.RowIndex;

            int idProducto = Convert.ToInt32(DGVProductos.Rows[fila].Cells["_IDProducto"].Value);

            //Esta condicion es para que no de error al momento que se haga click en el header de la columna por error
            if (e.RowIndex >= 0)
            {
                // Editar el Producto, Paquete o Servicio
                if (e.ColumnIndex == 1 ||
                    e.ColumnIndex == 2 ||
                    e.ColumnIndex == 3 ||
                    e.ColumnIndex == 4 ||
                    e.ColumnIndex == 5 ||
                    e.ColumnIndex == 6)
                {
                    if (Application.OpenForms.OfType<AgregarEditarProducto>().Count() == 1)
                    {
                        Application.OpenForms.OfType<AgregarEditarProducto>().First().Close();
                    }

                    if (seleccionadoDato == 0)
                    {
                        seleccionadoDato = 1;
                        numerofila = e.RowIndex;
                        obtenerDatosDGVProductos(numerofila);
                        origenDeLosDatos = 2;
                    }

                    var producto = cn.BuscarProducto(Convert.ToInt32(idProducto), Convert.ToInt32(id));

                    string typeProduct = producto[5];

                    if (typeProduct == "S")
                    {
                        btnAgregarServicio.PerformClick();
                    }
                    else if (typeProduct == "PQ")
                    {
                        btnAgregarPaquete.PerformClick();
                    }
                    else if (typeProduct == "P")
                    {
                        btnAgregarProducto.PerformClick();
                    }
                }
            }
        }

        private void DGVProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var apartado = cbMostrar.SelectedItem.ToString();

            if (apartado.Equals("Deshabilitados"))
            {
                return;
            }

            var fila = DGVProductos.CurrentCell.RowIndex;

            int idProducto = Convert.ToInt32(DGVProductos.Rows[fila].Cells["_IDProducto"].Value);

            //Esta condicion es para que no de error al momento que se haga click en el header de la columna por error
            if (e.RowIndex >= 0)
            {
                // Editar el Producto, Paquete o Servicio
                if (e.ColumnIndex == 1 ||
                    e.ColumnIndex == 2 ||
                    e.ColumnIndex == 3 ||
                    e.ColumnIndex == 4 ||
                    e.ColumnIndex == 5 ||
                    e.ColumnIndex == 6)
                {
                    if (seleccionadoDato == 0)
                    {
                        seleccionadoDato = 1;
                        numerofila = e.RowIndex;
                        obtenerDatosDGVProductos(numerofila);
                        origenDeLosDatos = 2;
                    }
                    var producto = cn.BuscarProducto(Convert.ToInt32(idProducto), Convert.ToInt32(id));
                    string typeProduct = producto[5];
                    if (typeProduct == "S")
                    {
                        btnAgregarServicio.PerformClick();
                    }
                    else if (typeProduct == "PQ")
                    {
                        btnAgregarPaquete.PerformClick();
                    }
                    else if (typeProduct == "P")
                    {
                        btnAgregarProducto.PerformClick();
                    }
                }
            }
        }

        private void DGVProductos_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var textoTTipButtonMsg = string.Empty;
                int coordenadaX = 0, coordenadaY = 0;
                System.Drawing.Rectangle cellRect = DGVProductos.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                if (e.ColumnIndex == 0)
                {
                    textoTTipButtonMsg = "";
                    coordenadaX = 110;
                    coordenadaY = -200;
                    TTipButtonText.Show(textoTTipButtonMsg, this, DGVProductos.Location.X + cellRect.X - coordenadaX,
                                        DGVProductos.Location.Y + cellRect.Y - coordenadaY, 1500);
                    textoTTipButtonMsg = string.Empty;
                }
                else if (e.ColumnIndex >= 7)
                {
                    DGVProductos.Cursor = Cursors.Hand;
                    if (e.ColumnIndex == 7)
                    {
                        textoTTipButtonMsg = "";
                        coordenadaX = 90;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 8)
                    {
                        textoTTipButtonMsg = "";
                        coordenadaX = 160;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 9)
                    {
                        textoTTipButtonMsg = "";
                        coordenadaX = 105;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 10)
                    {
                        textoTTipButtonMsg = "";
                        coordenadaX = 130;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 11)
                    {
                        textoTTipButtonMsg = "";
                        coordenadaX = 110;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 12)
                    {
                        textoTTipButtonMsg = "";
                        coordenadaX = 155;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 13)
                    {
                        textoTTipButtonMsg = "";
                        coordenadaX = 85;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 16)
                    {
                        textoTTipButtonMsg = "";
                        coordenadaX = 90;
                        coordenadaY = -200;
                    }
                    TTipButtonText.Show(textoTTipButtonMsg, this, DGVProductos.Location.X + cellRect.X - coordenadaX,
                                        DGVProductos.Location.Y + cellRect.Y - coordenadaY, 1500);
                    textoTTipButtonMsg = string.Empty;
                }
                else
                {
                    DGVProductos.Cursor = Cursors.Default;
                }
            }
        }

        private void btnFilterSearch_Click(object sender, EventArgs e)
        {
            ///Mostrar Mensaje ne la etiqueta de atajos
            timer1.Start();
            lAtajo.Visible = true;
            lAtajo.Text = "Ctrl + F";
            ///

            if (opcion13 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<WinQueryString>().Count() == 1)
            {
                Application.OpenForms.OfType<WinQueryString>().First().BringToFront();
            }
            else
            {
                WinQueryString FiltroAvanzado = new WinQueryString();

                FiltroAvanzado.FormClosed += delegate
                {
                    if (txtBusqueda.Text.Equals(""))
                    {
                        CargarDatos();
                    }
                    else if (!txtBusqueda.Text.Equals(""))
                    {
                        quitarEspacioEnBlanco();
                        busquedaDelUsuario();
                    }

                    verificarBotonLimpiarTags();
                };

                FiltroAvanzado.Show();
            }
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
                btn.Text = row["Nombre"].ToString() + "\n $" + Convert.ToDecimal(row["Precio"]).ToString("N2");
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

            Properties.Settings.Default.FiltroOrdenar = filtro;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            if (Properties.Settings.Default.FiltroOrdenar == "A - Z")
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
            else if (Properties.Settings.Default.FiltroOrdenar == "Z - A")
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
            else if (Properties.Settings.Default.FiltroOrdenar == "Mayor precio")
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
            else if (Properties.Settings.Default.FiltroOrdenar == "Menor precio")
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
            else if (Properties.Settings.Default.FiltroOrdenar == "Ordenar por:")
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
            txtBusqueda.Focus();
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
            ///Mostrar Mensaje ne la etiqueta de atajos
            timer1.Start();
            lAtajo.Visible = true;
            lAtajo.Text = "Ctrl + M";
            ///

            if (opcion6 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            fileSavePath = saveDirectoryImg;
            if (panelShowDGVProductosView.Visible == true || panelShowDGVProductosView.Visible == false)
            {
                panelShowDGVProductosView.Visible = false;
                panel2.Visible = false;
                panelShowPhotoView.Visible = true;
                searchPhotoProd();
                photoShow();
                txtBusqueda.Focus();
            }
            else if (panelShowPhotoView.Visible == true || panelShowPhotoView.Visible == false)
            {
                panelShowDGVProductosView.Visible = false;
                panel2.Visible = false;
                panelShowPhotoView.Visible = true;
                searchPhotoProd();
                photoShow();
                txtBusqueda.Focus();
            }
        }

        private void btnListView_Click(object sender, EventArgs e)
        {
            ///Mostrar Mensaje ne la etiqueta de atajos
            timer1.Start();
            lAtajo.Visible = true;
            lAtajo.Text = "Ctrl + L";
            ///

            if (opcion4 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (panelShowDGVProductosView.Visible == true || panelShowDGVProductosView.Visible == false)
            {
                panelShowPhotoView.Visible = false;
                panelShowDGVProductosView.Visible = true;
                panel2.Visible = true;
                searchToProdGral();
                txtBusqueda.Focus();
            }
            else if (panelShowPhotoView.Visible == true || panelShowPhotoView.Visible == false)
            {
                panelShowPhotoView.Visible = false;
                panelShowDGVProductosView.Visible = true;
                panel2.Visible = true;
                searchToProdGral();
                txtBusqueda.Focus();
            }
            actualizarDatosDespuesDeAgregarProducto();
        }

        private void DGVProductos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
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
                    TTipButtonText.Show(textoTTipButtonMsg, this, DGVProductos.Location.X + cellRect.X - coordenadaX, DGVProductos.Location.Y + cellRect.Y - coordenadaY, 980);
                    textoTTipButtonMsg = string.Empty;
                }
                else if (e.ColumnIndex >= 7)
                {
                    DGVProductos.Cursor = Cursors.Hand;
                    if (e.ColumnIndex == 7)
                    {
                        textoTTipButtonMsg = "Editar el Producto (F2)";
                        coordenadaX = 90;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 8)
                    {
                        textoTTipButtonMsg = "Modificar estado del Producto (ALt + E)";
                        coordenadaX = 160;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 9)
                    {
                        textoTTipButtonMsg = "Historial de Compra (Alt + H)";
                        coordenadaX = 105;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 10)
                    {
                        textoTTipButtonMsg = "Generar Código de Barras (Alt + G)";
                        coordenadaX = 130;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 11)
                    {
                        textoTTipButtonMsg = "Imagen del Producto (Alt + I)";
                        coordenadaX = 110;
                        coordenadaY = -200;
                    }
                    if (e.ColumnIndex == 12)
                    {
                        textoTTipButtonMsg = "Generar Etiqueta de Producto (Alt + E)";
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
                        textoTTipButtonMsg = "Producto, Combo ó Servicio";
                        coordenadaX = 150;
                        coordenadaY = -200;
                    }
                    TTipButtonText.Show(textoTTipButtonMsg, this, DGVProductos.Location.X + cellRect.X - coordenadaX, DGVProductos.Location.Y + cellRect.Y - coordenadaY, 980);
                    textoTTipButtonMsg = string.Empty;
                }
                else
                {
                    DGVProductos.Cursor = Cursors.Default;
                }
            }
        }

        private void DGVProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var apartado = cbMostrar.SelectedItem.ToString();

            if (apartado.Equals("Deshabilitados"))
            {
                return;
            }


            var fila = DGVProductos.CurrentCell.RowIndex;
            int idProducto = Convert.ToInt32(DGVProductos.Rows[fila].Cells["_IDProducto"].Value);
            var tipoProducto = DGVProductos.Rows[fila].Cells["TipoProducto"].Value.ToString();

            //Esta condicion es para que no de error al momento que se haga click en el header de la columna por error
            if (e.RowIndex >= 0)
            {
                // CheckBox del producto
                if (e.ColumnIndex == 0)
                {
                    numerofila = e.RowIndex;
                }
                else if (e.ColumnIndex == 7)
                {
                    if (opcion15 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    // Editar el producto
                    if (seleccionadoDato == 0)
                    {
                        seleccionadoDato = 1;
                        numerofila = e.RowIndex;
                        obtenerDatosDGVProductos(numerofila);
                        origenDeLosDatos = 2;
                    }

                    var producto = cn.BuscarProducto(Convert.ToInt32(idProductoEditar), Convert.ToInt32(id));
                    typeProduct = producto[5];

                    if (typeProduct == "S")
                    {
                        btnAgregarServicio.PerformClick();
                    }
                    else if (typeProduct == "PQ")
                    {
                        btnAgregarPaquete.PerformClick();
                    }
                    else if (typeProduct == "P")
                    {
                        btnAgregarProducto.PerformClick();
                    }
                }
                else if (e.ColumnIndex == 8)
                {
                    if (opcion16 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    // Estado del producto
                    index = 0;

                    var resultado = MessageBox.Show("¿Realmente desea cambiar el estado del producto?",
                                                    "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (resultado == DialogResult.Yes)
                    {
                        DGVProductos.Rows[fila].Cells["CheckProducto"].Value = true;
                        btnModificarEstado.PerformClick();
                    }
                }
                else if (e.ColumnIndex == 9)
                {
                    if (opcion17 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    using (var historial = new TipoHistorial(idProducto))
                    {
                        var respuesta = historial.ShowDialog();

                        if (respuesta == DialogResult.OK)
                        {
                            if (historial.tipoRespuesta == 1)
                            {
                                // Historial de compras del Producto
                                numerofila = e.RowIndex;
                                obtenerDatosDGVProductos(numerofila);
                                ViewRecordProducto();
                            }
                        }
                    }
                }
                else if (e.ColumnIndex == 10)
                {
                    if (opcion18 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    // Generar Codigo de Barras del Producto
                    string codiBarProd = "";
                    numfila = e.RowIndex;
                    obtenerDatosDGVProductos(numfila);

                    if (!MakeBarCode.Visible)
                    {
                        MakeBarCode.NombreProd = Nombre;
                        MakeBarCode.PrecioProd = Precio;
                        codiBarProd = CodigoBarras;
                        if (codiBarProd != "")
                        {
                            MakeBarCode.CodigoBarProd = codiBarProd.Replace("\r\n", string.Empty);
                            MakeBarCode.ShowDialog();
                        }
                        else if (codiBarProd == "")
                        {
                            MessageBox.Show("No se puede generar el codigo de barras\nPuesto que no tiene codigo de barras asignado",
                                            "Error de Generar Codigo de Barras", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MakeBarCode.NombreProd = Nombre;
                        MakeBarCode.PrecioProd = Precio;
                        codiBarProd = CodigoBarras;
                        if (codiBarProd != "")
                        {
                            MakeBarCode.CodigoBarProd = codiBarProd.Replace("\r\n", string.Empty);
                            MakeBarCode.BringToFront();
                        }
                        else if (codiBarProd == "")
                        {
                            MessageBox.Show("No se puede generar el codigo de barras\nPuesto que no tiene codigo de barras asignado",
                                            "Error de Generar Codigo de Barras", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if (e.ColumnIndex == 11)
                {
                    if (opcion19 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    // Imagen del Producto
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
                    //CargarDatos();
                    actualizarDatosDespuesDeAgregarProducto();
                }
                else if (e.ColumnIndex == 12)
                {
                    if (opcion20 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    // Etiqueta del Producto
                    numerofila = e.RowIndex;
                    obtenerDatosDGVProductos(numerofila);

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
                    if (opcion21 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    // Copiar el Producto
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
                    if (opcion22 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    // Ajustar el Producto

                    // La opcion ajustar solo debe funcionar para los tipo Producto
                    if (tipoProducto == "P")
                    {
                        //Esta es la columna de la opcion "Ajustar"
                        AjustarProducto ap = new AjustarProducto(idProducto);

                        ap.FormClosed += delegate
                        {
                            if (botonAceptar)
                            {
                                if (txtBusqueda.Text.Equals(""))
                                {
                                    CargarDatos();
                                }
                                else if (!txtBusqueda.Text.Equals(""))
                                {
                                    quitarEspacioEnBlanco();
                                    busquedaDelUsuario();
                                }

                                idReporte++;

                                botonAceptar = false;
                            }
                        };

                        ap.ShowDialog();
                    }
                }
            }

            ///////////////////////////////////////////////
            ////if (DGVProductos.SelectedRows.Count == 0)
            ////{

            ////}
                
                  
        }

        private void btnModificarEstado_Click(object sender, EventArgs e)
        {
            ///Mostrar Mensaje ne la etiqueta de atajos
            timer1.Start();
            lAtajo.Visible = true;
            lAtajo.Text = "Ctrl + D";
            ///

            if (opcion2 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            int estado = 2;

            if (cbMostrar.Text == "Habilitados")
            {
                estado = 0;
            }
            else if (cbMostrar.Text == "Deshabilitados")
            {
                estado = 1;
            }

            foreach (DataGridViewRow row in DGVProductos.Rows)
            {
                if ((bool)row.Cells["CheckProducto"].Value == true)
                {
                    var idProducto = Convert.ToInt32(row.Cells["_IDProducto"].Value);

                    if (estado < 2)
                    {
                        //Verificamos si el codigo de barras o clave ya esta usada en unos de los productos
                        //actualmente habilitados, si es asi no debe dejar habilitar el producto y mostrara
                        //un mensaje
                        if (estado == 1)
                        {
                            var claveCodigos = mb.ObtenerClaveCodigosProducto(idProducto, FormPrincipal.userID);

                            foreach (var codigo in claveCodigos)
                            {
                                if (mb.ComprobarCodigoClave(codigo, FormPrincipal.userID))
                                {
                                    MessageBox.Show($"El número de identificación {codigo} ya se esta utilizando\ncomo clave interna o código de barras de algún producto habilitado", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    return;
                                }
                            }
                        }

                        if (estado == 0)
                        {
                            // Comprobar si esta vinculado a un servicio o combo
                            var vinculado = (bool)cn.EjecutarSelect($"SELECT * FROM ProductosDeServicios WHERE IDProducto = {idProducto}");

                            if (vinculado)
                            {
                                var datos = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                                var producto = datos[1];

                                var mensaje = string.Join(
                                    Environment.NewLine,
                                    $"El producto {producto}",
                                    "se encuentra vinculado a un servicio o combo, al",
                                    "deshabilitar este producto se perderá la vinculación,",
                                    "¿Estás seguro de deshabilitar el producto?"
                                );

                                var respuesta = MessageBox.Show(mensaje, "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (respuesta == DialogResult.Yes)
                                {
                                    cn.EjecutarConsulta($"DELETE FROM ProductosDeServicios WHERE IDProducto = {idProducto}");
                                }
                                else
                                {
                                    continue;
                                }
                            } 
                        }

                        cn.EjecutarConsulta(cs.ActualizarStatusProducto(estado, idProducto, FormPrincipal.userID));
                    }
                }
            }

            if (estado == 0)
            {
                //CargarDatos(1);
                p.actualizarPagina();
                clickBoton = 1;
                CargarDatos();
                actualizar();
                txtBusqueda.Focus();
            }

            if (estado == 1)
            {
                //CargarDatos(0);
                p.actualizarPagina();
                clickBoton = 1;
                CargarDatos();
                actualizar();
                txtBusqueda.Focus();
            }

            CheckBox master = ((CheckBox)DGVProductos.Controls.Find("checkBoxMaster", true)[0]);
            master.Checked = false;
            //linkLblPaginaActual_Click_1(sender, e);
            //actualizarDatosDespuesDeAgregarProducto();
        }

        public void obtenerDatosDGVProductos(int fila)
        {
            Nombre = DGVProductos.Rows[fila].Cells["Column1"].Value.ToString();
            Stock = DGVProductos.Rows[fila].Cells["Column2"].Value.ToString();
            Precio = DGVProductos.Rows[fila].Cells["Column3"].Value.ToString();
            //ProductoCategoria = DGVProductos.Rows[fila].Cells["Column4"].Value.ToString();
            ProductoCategoria = DGVProductos.Rows[fila].Cells["Categoria"].Value.ToString();
            ClaveInterna = DGVProductos.Rows[fila].Cells["Column5"].Value.ToString();
            CodigoBarras = DGVProductos.Rows[fila].Cells["Column6"].Value.ToString();
            savePath = DGVProductos.Rows[fila].Cells["Column15"].Value.ToString();
            ClaveProducto = DGVProductos.Rows[fila].Cells["_ClavProdXML"].Value.ToString();
            UnidadMedida = DGVProductos.Rows[fila].Cells["_ClavUnidMedXML"].Value.ToString();
            id = FormPrincipal.userID.ToString();
            idProductoEditar = DGVProductos.Rows[fila].Cells["_IDProducto"].Value.ToString();
            //MessageBox.Show("ID: " + idProductoEditar + "\nProducto: " + Nombre, "Alerta Del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            impuestoProducto = DGVProductos.Rows[fila].Cells["Impuesto"].Value.ToString();
        }

        private void cbMostrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtro = Convert.ToString(cbMostrar.SelectedItem);      // tomamos el valor que se elige en el TextBox

            if (filtro == "Habilitados")                            // comparamos si el valor a filtrar es Habilitados
            {
                btnModificarEstado.Enabled = true;
                btnModificarEstado.Text = "Deshabilitar seleccionados";

                if (panelShowDGVProductosView.Visible == true)
                {
                    clickBoton = 0;
                    CargarDatos(1);
                }
                else if (panelShowPhotoView.Visible == true)
                {
                    searchPhotoProdActivo();
                    photoShow();
                }
            }
            else if (filtro == "Deshabilitados")                    // comparamos si el valor a filtrar es Deshabilitados
            {
                btnModificarEstado.Enabled = true;
                btnModificarEstado.Text = "Habilitar seleccionados";

                if (panelShowDGVProductosView.Visible == true)
                {
                    clickBoton = 0;
                    CargarDatos(0);
                }
                else if (panelShowPhotoView.Visible == true)
                {
                    searchPhotoProdInactivo();
                    photoShow();
                }
            }
            else if (filtro == "Todos")
            {
                // comparamos si el valor a filtrar es Todos
                btnModificarEstado.Enabled = false;

                if (panelShowDGVProductosView.Visible == true)
                {
                    clickBoton = 0;
                    // cargamos todos los registros
                    CargarDatos(2);
                }
                else if (panelShowPhotoView.Visible == true)
                {
                    searchPhotoProd();
                    photoShow();
                }
            }

            recargarBusqueda();
            txtBusqueda.Focus();
        }

        private void btnCleanFilter_Click(object sender, EventArgs e)
        {
            ///Mostrar Mensaje ne la etiqueta de atajos
            timer1.Start();
            lAtajo.Visible = true;
            lAtajo.Text = "ESC";
            ///

            if (opcion14 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            removeAllSystemTags(setUpVariable);
            modificarDiccionarioEtiquetas(fLPDynamicTags);

            txtBusqueda.Text = string.Empty;

            //if (txtBusqueda.Text.Equals(""))
            //{
            //    CargarDatos();
            //}
            //else if (!txtBusqueda.Text.Equals(""))
            //{
            //    quitarEspacioEnBlanco();
            //    busquedaDelUsuario();
            //}

            recargarBusqueda();

            verificarBotonLimpiarTags();
        }

        private void modificarDiccionarioEtiquetas(Control contenedor)
        {
            string nameTag = string.Empty,
            rutaCompletaFile = string.Empty,
            fileNameDictionary = "DiccionarioDetalleBasicos.txt";

            string[] words;

            List<string> listDictionary = new List<string>();

            listDictionary.Clear();

            foreach (Control item in contenedor.Controls)
            {
                foreach (var itemSetUpDinamicos in setUpDinamicos)
                {
                    nameTag = itemSetUpDinamicos.Value.Item1.Remove(0, 9);
                    if (item is Panel)
                    {
                        if (item.Name.Equals("pEtiqueta" + nameTag) && itemSetUpDinamicos.Value.Item2.Equals("True"))
                        {
                            try
                            {
                                var myKey = setUpDinamicos.FirstOrDefault(x => x.Value.Item1 == "chkBoxchk" + nameTag).Key;
                                //listDictionary.Add(myKey + "|" + itemSetUpDinamicos.Value.Item1 + "|False|Selecciona " + nameTag + "|" + itemSetUpDinamicos.Value.Item4);
                                listDictionary.Add(myKey + "|chkBoxchk" + nameTag + "|False|Selecciona " + nameTag + "|cbchk" + nameTag);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message.ToString());
                            }
                        }
                    }
                }
            }

            foreach (Control control in contenedor.Controls)
            {
                control.Dispose();
            }
            contenedor.Controls.Clear();

            if (listDictionary.Count > 0)
            {
                string queryUpdateDatoDinamico = string.Empty;

                foreach (var itemDicc in listDictionary)
                {
                    words = itemDicc.Split('|');

                    setUpDinamicos[words[0]] = Tuple.Create(words[1], words[2], words[3], words[4]);

                    if (words[2].ToString().Equals("False"))
                    {
                        queryUpdateDatoDinamico += $"UPDATE FiltrosDinamicosVetanaFiltros SET checkBoxValue = '{0}', strFiltro = 'Selecciona {words[3].ToString().Remove(0, 9)}' WHERE ID = '{words[0].ToString()}'; ";
                    }
                    else if (words[2].ToString().Equals("True"))
                    {
                        queryUpdateDatoDinamico += $"UPDATE FiltrosDinamicosVetanaFiltros SET checkBoxValue = '{1}', strFiltro = 'Selecciona {words[3].ToString().Remove(0, 9)}' WHERE ID = '{words[0].ToString()}'; ";
                    }
                }

                try
                {
                    var UpdateDatoDinamico = cn.EjecutarConsulta(queryUpdateDatoDinamico);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al Actualizar el Dato dinamico: " + ex.Message.ToString(), "Error de Actualización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                listDictionary.Clear();

                //rutaCompletaFile = path + fileNameDictionary;
                //using (StreamWriter file = new StreamWriter(rutaCompletaFile))
                //{
                //    foreach (var entry in setUpDinamicos)
                //    {
                //        file.WriteLine("{0}|{1}|{2}|{3}|{4}", entry.Key, entry.Value.Item1, entry.Value.Item2, entry.Value.Item3, entry.Value.Item4);
                //    }
                //    file.Close();
                //}

                setUpDinamicos.Clear();
            }
        }

        private void removeAllSystemTags(List<string> VariablesSistema)
        {
            string[] words;

            foreach (var control in VariablesSistema)
            {
                words = control.Split(' ');
                foreach (Control panel in fLPDynamicTags.Controls)
                {
                    if (panel.Name.Equals("pEtiqueta" + words[0]))
                    {
                        fLPDynamicTags.Controls.Remove(panel);
                        panel.Dispose();
                    }
                }
            }

            setUpVariable.Clear();
            auxWord.Clear();
            setUpDinamicos.Clear();

            reiniciarVariablesDeSistemaPrecio();
            reiniciarVariablesDeSistemaStock();
            reiniciarVariablesDeSistemaNoRevision();
            reiniciarVariablesDeSistemaTipo();
            reiniciarVariablesImagen();

            actualizarBtnFiltro();

            CargarDatos();

            verificarBotonLimpiarTags();
        }

        public Productos()
        {
            InitializeComponent();

            MostrarCheckBox();

            size = new Size(Ancho, Alto);
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            productosSeleccionados = new Dictionary<int, string>();

            listVariables = new List<Control>();

            auxWord = new List<string>();

            setUpVariable = new List<string>();

            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                saveDirectoryImg = $@"\\{servidor}\PUDVE\Productos\";
                saveDirectoryFile = $@"\\{servidor}\PUDVE\settings\Dictionary\";
            }
            else
            {
                saveDirectoryImg = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Productos\";
                saveDirectoryFile = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\settings\Dictionary\";
            }

            txtMaximoPorPagina.Text = maximo_x_pagina.ToString();

            panelShowPhotoView.Visible = false;
            panelShowDGVProductosView.Visible = true;

            filtroLoadProductos();

            linkLblUltimaPagina.Text = p.countPag().ToString();

            actualizar();

            actualizarBtnFiltro();

            CargarDatos();

            filtroOrdenarPor();

            idReporte = cn.ObtenerUltimoIdReporte(FormPrincipal.userID) + 1;

            // Calcular capital temporalmente (informativo)
            lbCapital.Text = "Capital: " + mb.CalcularCapital().ToString("N2");

            //reloadTags();

            //creacionEtiquetasDinamicas();

            //actualizarDGVProductos.Interval = 3000;
            //actualizarDGVProductos.Tick += actualizar_automatico_Tick;
            //actualizarDGVProductos.Enabled = true;

            if (FormPrincipal.id_empleado > 0)
            {
                var permisos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Productos");

                opcion1 = permisos[0]; opcion2 = permisos[1]; opcion3 = permisos[2];
                opcion4 = permisos[3]; opcion5 = permisos[4]; opcion6 = permisos[5];
                opcion7 = permisos[6]; opcion8 = permisos[7]; opcion9 = permisos[8];
                opcion10 = permisos[9]; opcion11 = permisos[10]; opcion12 = permisos[11];
                opcion13 = permisos[12]; opcion14 = permisos[13]; opcion15 = permisos[14];
                opcion16 = permisos[15]; opcion17 = permisos[16]; opcion18 = permisos[17];
                opcion19 = permisos[18]; opcion20 = permisos[19]; opcion21 = permisos[20];
                opcion22 = permisos[21];
            }
        }

        private void actualizar_automatico_Tick(object sender, EventArgs e)
        {
            //recargarDGV();
        }

        public void recargarDGV()
        {
            int ultimaPagina = 0, 
                currentPage = 0;

            if (!txtBusqueda.Text.Equals(""))
            {
                ultimaPagina = p.countPag();
                currentPage = Convert.ToInt32(linkLblPaginaActual.Text);
                actualizarDatosDespuesDeAgregarProducto();
                goToPageNumber(ultimaPagina);
            }
            else if (txtBusqueda.Text.Equals(""))
            {
                ultimaPagina = p.countPag();
                currentPage = Convert.ToInt32(linkLblPaginaActual.Text);
                //goToPageNumber(Convert.ToInt32(linkLblPaginaActual.Text));
                //actualizar();
                if (currentPage.Equals(ultimaPagina))
                {
                    //CargarDatos();
                    //ultimaPagina = p.countPag();
                    //goToPageNumber(ultimaPagina);
                    //currentPage = p.numPag();
                    goToPageNumber(currentPage);
                }
                else if (currentPage < ultimaPagina)
                {
                    //CargarDatos();
                    //currentPage = p.numPag();
                    goToPageNumber(currentPage);
                }
            }
            //goToPageNumber(Convert.ToInt32(linkLblPaginaActual.Text));
        }

        private void btnPedido_Click(object sender, EventArgs e)
        {
            ///Mostrar Mensaje ne la etiqueta de atajos
            timer1.Start();
            lAtajo.Visible = true;
            lAtajo.Text = "Ctrl + R";
            ///

            if (opcion8 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<OpcionesReporteProducto>().Count() == 1)
            {
                Application.OpenForms.OfType<OpcionesReporteProducto>().First().BringToFront();
            }
            else
            {
                var opciones = new OpcionesReporteProducto();

                opciones.Show();

                opciones.FormClosed += delegate
                {
                    txtBusqueda.Focus(); 
                };
            }

            
        }

        private void AddPageNumber()
        {
            try
            {
                byte[] bytes = System.IO.File.ReadAllBytes(fileReportOrder);
                iTextSharp.text.Font blackFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                using (MemoryStream stream = new MemoryStream())
                {
                    PdfReader reader = new PdfReader(bytes);
                    using (PdfStamper stamper = new PdfStamper(reader, stream))
                    {
                        int pages = reader.NumberOfPages;
                        for (int i = 1; i <= pages; i++)
                        {
                            ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase("Pagina: " + i.ToString(), blackFont), 568f, 15f, 0);
                        }
                        stamper.Close();
                    }
                    bytes = stream.ToArray();
                    reader.Close();
                }
                System.IO.File.WriteAllBytes(fileReportOrder, bytes);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error al Intentar Paginar el Reporte en PDF:\n" + ex.Message.ToString(),
                                "Error al Paginar PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int) Keys.Enter)
            {
                recargarBusqueda();
            }
            else if (txtBusqueda.Text.Equals(""))
            {
                recargarBusqueda();
            }
        }

        private void recargarBusqueda()
        {
            if (txtBusqueda.Text.Equals(""))
            {
                //CargarDatos();
                busquedaDelUsuario();
            }
            else if (!txtBusqueda.Text.Equals(""))
            {
                quitarEspacioEnBlanco();
                busquedaDelUsuario();
                agregarEspacioAlFinal();
                borrarCodigosNoValidos();
            }
        }

        private void borrarCodigosNoValidos()
        {
            if (!noEcontradoCodBar.Count().Equals(0))
            {
                for (int i = 0; i < noEcontradoCodBar.Count; i++)
                {
                    foreach (Control item in fLPDynamicTags.Controls.OfType<Control>())
                    {
                        if (item is Panel)
                        {
                            string toDelete = string.Empty, noFoundToDelete = string.Empty;
                            toDelete = item.Name.ToString();
                            noFoundToDelete = "pEtiqueta" + noEcontradoCodBar[i].ToString().Trim();
                            if (toDelete.Equals(noFoundToDelete))
                            {
                                fLPDynamicTags.Controls.Remove(item);
                            }
                        }
                    }

                    for (int u = 0; u < auxWord.Count; u++)
                    {
                        if (auxWord[u].Equals(noEcontradoCodBar[i].ToString().Trim()))
                        {
                            auxWord.RemoveAt(u);
                        }
                    }

                    string newCadenaBusqueda = string.Empty;

                    for (int u = 0; u < auxWord.Count; u++)
                    {
                        newCadenaBusqueda += auxWord[u].ToString() + " ";
                    }

                    txtBusqueda.Text = newCadenaBusqueda.Trim();
                    txtBusqueda.Select(txtBusqueda.Text.Length, 0);

                    if (txtBusqueda.Text.Equals(""))
                    {
                        CargarDatos();
                    }
                    else if (!txtBusqueda.Text.Equals(""))
                    {
                        quitarEspacioEnBlanco();
                        busquedaDelUsuario();
                    }

                    txtBusqueda.Focus();

                    verificarBotonLimpiarTags();
                }
            }
        }

        private void agregarEspacioAlFinal()
        {
            txtBusqueda.Text += " ";
            txtBusqueda.Select(txtBusqueda.Text.Length, 0);
        }

        private void busquedaDelUsuario()
        {
            if (cbMostrar.Text == "Habilitados")
            {
                CargarDatos(1, txtBusqueda.Text);
            }
            else if (cbMostrar.Text == "Deshabilitados")
            {
                CargarDatos(0, txtBusqueda.Text);
            }
            else if (cbMostrar.Text == "Todos")
            {
                CargarDatos(2, txtBusqueda.Text);
            }
            borrarAuxWordTags();
            cargarListaDeEtiquetas();
            verificarBotonLimpiarTags();
        }

        public void creacionEtiquetasDinamicas()
        {
            cargarListaSetUpVaribale();

            dictionaryLoad();

            verificarBotonLimpiarTags();
        }

        public void verificarBotonLimpiarTags()
        {
            if (auxWord.Count == 0 && setUpVariable.Count == 0 && setUpDinamicos.Count == 0)
            {
                btnCleanFilter.Image = global::PuntoDeVentaV2.Properties.Resources.tag1;
                btnCleanFilter.Enabled = false;
            }
            else
            {
                btnCleanFilter.Visible = true;
                if (fLPDynamicTags.Controls.Count == 1)
                {
                    btnCleanFilter.Image = global::PuntoDeVentaV2.Properties.Resources.tag1;
                    btnCleanFilter.Enabled = true;
                }
                else if (fLPDynamicTags.Controls.Count > 1)
                {
                    btnCleanFilter.Image = global::PuntoDeVentaV2.Properties.Resources.tags;
                    btnCleanFilter.Enabled = true;
                }
                else if (fLPDynamicTags.Controls.Count <= 0)
                {
                    btnCleanFilter.Image = global::PuntoDeVentaV2.Properties.Resources.tag1;
                    btnCleanFilter.Enabled = false;
                }
            }
        }

        private void linkLblPaginaActual_Click_1(object sender, EventArgs e)
        {
            actualizar();
        }

        public void FiltroDinamicoLoad()
        {
            string strFiltro = string.Empty;
            int num = 0;

            setUpFiltroDinamicos.Clear();

            strFiltro = "SELECT * FROM FiltroDinamico WHERE IDUsuario = '11'";

            using (DataTable dtFiltroDinamico = cn.CargarDatos(strFiltro))
            {
                foreach (DataRow dtRow in dtFiltroDinamico.Rows)
                {
                    setUpFiltroDinamicos.Add(Convert.ToString(num), new Tuple<string, string, string>(dtRow["concepto"].ToString(), dtRow["checkBoxConcepto"].ToString(), dtRow["textCantidad"].ToString()));
                    num++;
                }
            }
        }

        public void dictionaryLoad()
        {
            using (DataTable dtFiltrosDinamicosVetanaFiltros = cn.CargarDatos(cs.VerificarVentanaFiltros(FormPrincipal.userID)))
            {
                if (dtFiltrosDinamicosVetanaFiltros.Rows.Count.Equals(0))
                {
                    borrarEtiquetasDinamicasSetUpDinamicos();
                    setUpDinamicos.Clear();
                }
                else if (!dtFiltrosDinamicosVetanaFiltros.Rows.Count.Equals(0))
                {
                    borrarEtiquetasDinamicasSetUpDinamicos();
                    setUpDinamicos.Clear();

                    foreach (DataRow drFiltrosDinamicos in dtFiltrosDinamicosVetanaFiltros.Rows)
                    {
                        string valueChkBox = string.Empty;

                        if (drFiltrosDinamicos["checkBoxValue"].ToString().Equals("1"))
                        {
                            valueChkBox = "True";
                        }
                        else if (drFiltrosDinamicos["checkBoxValue"].ToString().Equals("0"))
                        {
                            valueChkBox = "False";
                        }
                        setUpDinamicos.Add(drFiltrosDinamicos["ID"].ToString(), new Tuple<string, string, string, string>("chkBoxchk" + drFiltrosDinamicos["concepto"].ToString(), valueChkBox.ToString(), drFiltrosDinamicos["strFiltro"].ToString(), "cbchk" + drFiltrosDinamicos["concepto"].ToString()));
                    }

                    crearEtiquetaDinamicaSetUpDinamicos();
                }
            }

            //bool isEmpty = (setUpDinamicos.Count == 0);
            //usrNo = FormPrincipal.userID;
            //fileName = "DiccionarioDetalleBasicos.txt";

            //if (!path.Equals(""))
            //{
            //    path = saveDirectoryFile + usrNo + @"\";
            //}
            //else if (path.Equals(""))
            //{
            //    path = saveDirectoryFile + usrNo + @"\";
            //}

            //if (usrNo > 0)
            //{
            //    if (!isEmpty)
            //    {
            //        borrarEtiquetasDinamicasSetUpDinamicos();
            //        setUpDinamicos.Clear();

            //        using (StreamReader file = new StreamReader(path + @"\" + fileName))
            //        {
            //            while ((line = file.ReadLine()) != null)
            //            {
            //                words = line.Split(delimiter);
            //                setUpDinamicos.Add(words[0], new Tuple<string, string, string, string>(words[1], words[2], words[3], words[4]));
            //            }
            //            file.Close();
            //        }
            //        crearEtiquetaDinamicaSetUpDinamicos();
            //    }
            //    else if (isEmpty)
            //    {
            //        if (!System.IO.File.Exists(path + fileName))
            //        {
            //            Directory.CreateDirectory(path);
            //            using (System.IO.File.Create(path + fileName)) { }
            //        }
            //        if (new FileInfo(path + fileName).Length > 0)
            //        {
            //            borrarEtiquetasDinamicasSetUpDinamicos();
            //            setUpDinamicos.Clear();
            //            using (StreamReader file = new StreamReader(path + @"\" + fileName))
            //            {
            //                while ((line = file.ReadLine()) != null)
            //                {
            //                    words = line.Split(delimiter);
            //                    setUpDinamicos.Add(words[0], new Tuple<string, string, string, string>(words[1], words[2], words[3], words[4]));
            //                }
            //                file.Close();
            //            }
            //            crearEtiquetaDinamicaSetUpDinamicos();
            //        }
            //        else if (new FileInfo(path + fileName).Length < 0)
            //        {
            //            setUpDinamicos.Clear();
            //        }
            //    }
            //    verificarBotonLimpiarTags();
            //}
            //else if (usrNo.Equals(0))
            //{
            //    MessageBox.Show("Favor de Seleccionar un valor\ndiferente o Mayor a 0 en Campo Usuario",
            //                    "Error de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        public void borrarEtiquetasDinamicasSetUpDinamicos()
        {
            string nameTag = string.Empty,
                    rutaCompletaFile = string.Empty;

            List<string> listDictionary = new List<string>();

            listDictionary.Clear();

            foreach (var itemSetUpDinamicos in setUpDinamicos)
            {
                nameTag = itemSetUpDinamicos.Value.Item1.Remove(0, 9);
                foreach (Control item in fLPDynamicTags.Controls.OfType<Control>())
                {
                    if (item is Panel)
                    {
                        if (item.Name.Equals("pEtiqueta" + nameTag))
                        {
                            try
                            {
                                fLPDynamicTags.Controls.Remove(item);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("No se logro hacer el limpiado de Panel\nde Etiquetas Error: " + ex.Message.ToString());
                            }
                        }
                    }
                }
            }
            setUpDinamicos.Clear();
        }

        private void crearEtiquetaDinamicaSetUpDinamicos()
        {
            //borrarEtiquetasDinamicasSetUpDinamicos();

            verificarListaDeVariablesEtiquetas();

            string nameTag = string.Empty;

            if (!isEmptySetUpDinamicos)
            {
                foreach (var item in setUpDinamicos)
                {
                    nameTag = item.Value.Item1.Remove(0, 9);

                    if (item.Value.Item2.Equals("True"))
                    {
                        Panel panelEtiqueta = new Panel();
                        panelEtiqueta.Name = "pEtiqueta" + nameTag;
                        panelEtiqueta.Width = 140;
                        panelEtiqueta.Height = 32;
                        panelEtiqueta.Location = new Point(0, 2);
                        //panelEtiqueta.BackColor = Color.Red;

                        Label lblLeft = new Label();
                        lblLeft.Name = "LabelIzquierdo" + nameTag;
                        lblLeft.Width = 9;
                        lblLeft.Height = 23;
                        lblLeft.Image = global::PuntoDeVentaV2.Properties.Resources.imageLabelLeft;
                        lblLeft.Location = new Point(2, 2);
                        panelEtiqueta.Controls.Add(lblLeft);

                        Panel panelTagText = new Panel();
                        panelTagText.Name = "PanelTagText" + nameTag;
                        panelTagText.Size = size;
                        panelTagText.BackgroundImage = global::PuntoDeVentaV2.Properties.Resources.backgroundMiddleLabel;
                        panelTagText.BackgroundImageLayout = ImageLayout.Stretch;
                        panelTagText.Location = new Point(lblLeft.Location.X + lblLeft.Width, 4);
                        panelEtiqueta.Controls.Add(panelTagText);
                        //panelEtiqueta.BackColor = Color.Beige;

                        string textoPanel = string.Empty;

                        textoPanel = nameTag + ": " + item.Value.Item3;

                        label2.Text = textoPanel;
                        var infoText = TextRenderer.MeasureText(label2.Text, new System.Drawing.Font(label2.Font.FontFamily, label2.Font.Size));
                        Ancho = infoText.Width;

                        Label lblTextFiltro = new Label();
                        lblTextFiltro.AutoSize = false;
                        lblTextFiltro.Height = 17;
                        lblTextFiltro.Width = Ancho;
                        lblTextFiltro.Location = new Point(0, 2);
                        lblTextFiltro.BackColor = Color.Transparent;
                        lblTextFiltro.Text = label2.Text;
                        lblTextFiltro.ForeColor = Color.Red;
                        //lblTextFiltro.Font = new System.Drawing.Font("Century Gothic", 10, FontStyle.Regular);

                        panelTagText.Controls.Add(lblTextFiltro);

                        panelTagText.Size = new Size(Ancho - 10, Alto);

                        Button btnRight = new Button();
                        btnRight.Name = "btnRight" + nameTag;
                        btnRight.Width = 20;
                        btnRight.Height = 20;
                        btnRight.FlatStyle = FlatStyle.Flat;
                        btnRight.FlatAppearance.BorderSize = 0;
                        btnRight.ImageAlign = ContentAlignment.MiddleCenter;
                        btnRight.Image = global::PuntoDeVentaV2.Properties.Resources.imageLabelRight;
                        btnRight.Cursor = Cursors.Hand;
                        btnRight.Location = new Point(panelTagText.Location.X + panelTagText.Width - 3, 3);
                        btnRight.Click += new EventHandler(btnRightSetUpDinamico_Click);
                        panelEtiqueta.Controls.Add(btnRight);

                        panelEtiqueta.Width = Ancho + 35;

                        fLPDynamicTags.Controls.Add(panelEtiqueta);
                    }
                }
            }
        }

        private void linkLblPaginaAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void btnRightSetUpDinamico_Click(object sender, EventArgs e)
        {
            Button btnTag = (Button)sender;
            string name = string.Empty,
                    newtext = string.Empty,
                    fileNameDictionary = "DiccionarioDetalleBasicos.txt",
                    rutaCompletaFile = string.Empty;

            string[] words;

            List<string> listDictionary = new List<string>();

            listDictionary.Clear();

            name = btnTag.Name.Remove(0, 8);

            DialogResult result = MessageBox.Show("Seguro desea borrar\nel Tag(Filtro): " + name + "?", "Eliminar Filtro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (Control item in fLPDynamicTags.Controls.OfType<Control>())
                {
                    if (item is Panel)
                    {
                        if (item.Name.Equals("pEtiqueta" + name))
                        {
                            try
                            {
                                fLPDynamicTags.Controls.Remove(item);
                                var myKey = setUpDinamicos.FirstOrDefault(x => x.Value.Item1 == "chkBoxchk" + name).Key;
                                listDictionary.Add(myKey + "|chkBoxchk" + name + "|False|Selecciona " + name + "|cbchk" + name);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message.ToString());
                            }
                        }
                    }
                }

                if (listDictionary.Count > 0)
                {
                    string queryUpdateDatoDinamico = string.Empty;
                    foreach (var itemDicc in listDictionary)
                    {
                        words = itemDicc.Split('|');
                        setUpDinamicos[words[0]] = Tuple.Create(words[1], words[2], words[3], words[4]);

                        if (words[2].ToString().Equals("False"))
                        {
                            queryUpdateDatoDinamico = $"UPDATE FiltrosDinamicosVetanaFiltros SET checkBoxValue = '{0}', strFiltro = 'Selecciona {words[3].ToString().Remove(0, 9)}' WHERE ID = '{words[0].ToString()}'";
                        }
                        else if (words[2].ToString().Equals("True"))
                        {
                            queryUpdateDatoDinamico = $"UPDATE FiltrosDinamicosVetanaFiltros SET checkBoxValue = '{1}', strFiltro = 'Selecciona {words[3].ToString().Remove(0, 9)}' WHERE ID = '{words[0].ToString()}'";
                        }
                    }
                    try
                    {
                        var UpdateDatoDinamico = cn.EjecutarConsulta(queryUpdateDatoDinamico);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al Actualizar el Dato dinamico: " + ex.Message.ToString(), "Error de Actualización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    listDictionary.Clear();
                    //rutaCompletaFile = path + fileNameDictionary;
                    //using (StreamWriter file = new StreamWriter(rutaCompletaFile))
                    //{
                    //    foreach (var entry in setUpDinamicos)
                    //    {
                    //        file.WriteLine("{0}|{1}|{2}|{3}|{4}", entry.Key, entry.Value.Item1, entry.Value.Item2, entry.Value.Item3, entry.Value.Item4);
                    //    }
                    //    file.Close();
                    //}
                    setUpDinamicos.Clear();
                }

                for (int i = 0; i < auxWord.Count; i++)
                {
                    if (auxWord[i].Equals(name))
                    {
                        auxWord.RemoveAt(i);
                    }
                }

                if (txtBusqueda.Text.Equals(""))
                {
                    CargarDatos();
                }
                else if (!txtBusqueda.Text.Equals(""))
                {
                    quitarEspacioEnBlanco();
                    busquedaDelUsuario();
                }

                actualizarBtnFiltro();
                txtBusqueda.Focus();
            }
            else if (result == DialogResult.No)
            {
                txtBusqueda.Focus();
            }
        }

        public void cargarListaSetUpVaribale()
        {
            string queryFiltroProducto = string.Empty;

            setUpVariable.Clear();

            queryFiltroProducto = $"SELECT * FROM FiltroProducto WHERE IDUsuario = '{FormPrincipal.userID}'";

            using (DataTable dtFiltroProducto = cn.CargarDatos(queryFiltroProducto))
            {
                if (!dtFiltroProducto.Rows.Count.Equals(0))
                {
                    foreach (DataRow drFiltroProducto in dtFiltroProducto.Rows)
                    {
                        if (drFiltroProducto["concepto"].ToString().Equals("chkBoxStock"))
                        {
                            if (drFiltroProducto["checkBoxConcepto"].ToString().Equals("1"))
                            {
                                setUpVariable.Add(drFiltroProducto["textComboBoxConcepto"].ToString() + drFiltroProducto["textCantidad"].ToString());
                            }
                        }
                        if (drFiltroProducto["concepto"].ToString().Equals("chkBoxPrecio"))
                        {
                            if (drFiltroProducto["checkBoxConcepto"].ToString().Equals("1"))
                            {
                                setUpVariable.Add(drFiltroProducto["textComboBoxConcepto"].ToString() + drFiltroProducto["textCantidad"].ToString());
                            }
                        }
                        if (drFiltroProducto["concepto"].ToString().Equals("chkBoxRevision"))
                        {
                            if (drFiltroProducto["checkBoxConcepto"].ToString().Equals("1"))
                            {
                                setUpVariable.Add(drFiltroProducto["textComboBoxConcepto"].ToString() + drFiltroProducto["textCantidad"].ToString());
                            }
                        }
                        if (drFiltroProducto["concepto"].ToString().Equals("chkBoxImagen"))
                        {
                            if (drFiltroProducto["checkBoxConcepto"].ToString().Equals("1"))
                            {
                                setUpVariable.Add(drFiltroProducto["textComboBoxConcepto"].ToString());
                            }
                        }
                        if (drFiltroProducto["concepto"].ToString().Equals("chkBoxTipo"))
                        {
                            if (drFiltroProducto["checkBoxConcepto"].ToString().Equals("1"))
                            {
                                setUpVariable.Add(drFiltroProducto["textComboBoxConcepto"].ToString());
                            }
                        }
                    }
                }
            }
            crearEtiquetaSetUpVariable();

            //setUpVariable.Clear();
            //if (Properties.Settings.Default.chkFiltroStock.Equals(true))
            //{
            //    if (!Properties.Settings.Default.strFiltroStock.Equals(""))
            //    {
            //        setUpVariable.Add(Properties.Settings.Default.strFiltroStock);
            //    }
            //}
            //if (Properties.Settings.Default.chkFiltroPrecio.Equals(true))
            //{
            //    if (!Properties.Settings.Default.strFiltroPrecio.Equals(""))
            //    {
            //        setUpVariable.Add(Properties.Settings.Default.strFiltroPrecio);
            //    }
            //}
            //if (Properties.Settings.Default.chkFiltroRevisionInventario.Equals(true))
            //{
            //    if (!Properties.Settings.Default.strFiltroRevisionInventario.Equals(""))
            //    {
            //        setUpVariable.Add(Properties.Settings.Default.strFiltroRevisionInventario);
            //    }
            //}
            //if (Properties.Settings.Default.chkFiltroCombProdServ.Equals(true))
            //{
            //    if (!Properties.Settings.Default.strFiltroCombProdServ.Equals(""))
            //    {
            //        setUpVariable.Add(Properties.Settings.Default.strFiltroCombProdServ);
            //    }
            //}
            //if (Properties.Settings.Default.chkFiltroImagen.Equals(true))
            //{
            //    if (!Properties.Settings.Default.strFiltroImagen.Equals(""))
            //    {
            //        setUpVariable.Add(Properties.Settings.Default.strFiltroImagen);
            //    }
            //}
            //crearEtiquetaSetUpVariable();
        }

        private void crearEtiquetaSetUpVariable()
        {
            verificarListaDeVariablesEtiquetas();

            string nameItemLista = string.Empty;

            if (!isEmptySetUpVariable)
            {
                foreach (var itemLista in setUpVariable)
                {
                    nameItemLista = itemLista;
                    string[] words = nameItemLista.Split(' ');

                    Panel panelEtiqueta = new Panel();
                    panelEtiqueta.Name = "pEtiqueta" + words[0];
                    panelEtiqueta.Width = 140;
                    panelEtiqueta.Height = 32;
                    panelEtiqueta.Location = new Point(0, 2);
                    //panelEtiqueta.BackColor = Color.Red;

                    Label lblLeft = new Label();
                    lblLeft.Name = "LabelIzquierdo" + words[0];
                    lblLeft.Width = 9;
                    lblLeft.Height = 23;
                    lblLeft.Image = global::PuntoDeVentaV2.Properties.Resources.imageLabelLeft;
                    lblLeft.Location = new Point(2, 2);
                    panelEtiqueta.Controls.Add(lblLeft);

                    Panel panelTagText = new Panel();
                    panelTagText.Name = "PanelTagText" + words[0];
                    panelTagText.Size = size;
                    panelTagText.BackgroundImage = global::PuntoDeVentaV2.Properties.Resources.backgroundMiddleLabel;
                    panelTagText.BackgroundImageLayout = ImageLayout.Stretch;
                    panelTagText.Location = new Point(lblLeft.Location.X + lblLeft.Width, 4);
                    panelEtiqueta.Controls.Add(panelTagText);
                    //panelEtiqueta.BackColor = Color.Beige;

                    string textoPanel = string.Empty;

                    if (words[0].Equals("ProdImage"))
                    {
                        textoPanel += "Producto ";
                        if (words[1].Equals("<>"))
                        {
                            textoPanel += "Con Imagen";
                        }
                        else if (words[1].Equals("="))
                        {
                            textoPanel += "Sin Imagen";
                        }
                    }
                    else if (!words[0].Equals("ProdImage"))
                    {
                        textoPanel += words[0];

                        if (words[1].Equals(">="))
                        {
                            textoPanel += " Mayor o Igual Que ";
                        }
                        else if (words[1].Equals("<="))
                        {
                            textoPanel += " Menor o Igual Que ";
                        }
                        else if (words[1].Equals("="))
                        {
                            textoPanel += " Igual Que ";
                        }
                        else if (words[1].Equals(">"))
                        {
                            textoPanel += " Mayor Que ";
                        }
                        else if (words[1].Equals("<"))
                        {
                            textoPanel += " Menor Que ";
                        }

                        if (words[2].Equals("'P'"))
                        {
                            textoPanel += "Producto";
                        }
                        else if (words[2].Equals("'PQ'"))
                        {
                            textoPanel += "Combo";
                        }
                        else if (words[2].Equals("'S'"))
                        {
                            textoPanel += "Servicio";
                        }
                        else
                        {
                            textoPanel += words[2];
                        }
                    }

                    label2.Text = textoPanel;
                    var infoText = TextRenderer.MeasureText(label2.Text, new System.Drawing.Font(label2.Font.FontFamily, label2.Font.Size));
                    Ancho = infoText.Width;

                    Label lblTextFiltro = new Label();
                    lblTextFiltro.AutoSize = false;
                    lblTextFiltro.Height = 17;
                    lblTextFiltro.Width = Ancho;
                    lblTextFiltro.Location = new Point(0, 2);
                    lblTextFiltro.BackColor = Color.Transparent;
                    lblTextFiltro.Text = textoPanel.ToString();
                    lblTextFiltro.ForeColor = Color.Red;
                    //lblTextFiltro.Font = new System.Drawing.Font("Century Gothic", 9, FontStyle.Regular);

                    panelTagText.Controls.Add(lblTextFiltro);

                    panelTagText.Size = new Size(Ancho, Alto);

                    Button btnRight = new Button();
                    btnRight.Name = "btnRight" + words[0];
                    btnRight.Width = 20;
                    btnRight.Height = 20;
                    btnRight.FlatStyle = FlatStyle.Flat;
                    btnRight.FlatAppearance.BorderSize = 0;
                    btnRight.ImageAlign = ContentAlignment.MiddleCenter;
                    btnRight.Image = global::PuntoDeVentaV2.Properties.Resources.imageLabelRight;
                    btnRight.Cursor = Cursors.Hand;
                    btnRight.Location = new Point(panelTagText.Location.X + panelTagText.Width - 3, 3);
                    btnRight.Click += new EventHandler(btnRightSetUpVariable_Click);
                    panelEtiqueta.Controls.Add(btnRight);

                    panelEtiqueta.Width = Ancho + 30;

                    fLPDynamicTags.Controls.Add(panelEtiqueta);
                }
            }
            else if (isEmptySetUpVariable)
            {
                txtBusqueda.Focus();
            }
        }

        private void btnCambiarTipo_Click(object sender, EventArgs e)
        {
            ///Mostrar Mensaje ne la etiqueta de atajos
            timer1.Start();
            lAtajo.Visible = true;
            lAtajo.Text = "Ctrl + T";
            ///

            if (opcion3 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            int contador = 0;
            int idProducto = 0;
            var tipo = string.Empty;

            foreach (DataGridViewRow row in DGVProductos.Rows)
            {
                if ((bool)row.Cells["CheckProducto"].Value == true)
                {
                    idProducto = Convert.ToInt32(row.Cells["_IDProducto"].Value);
                    tipo = row.Cells["TipoProducto"].Value.ToString();
                    contador++;
                }
            }

            if (contador == 1)
            {
                // Es un servicio
                if (tipo.Equals("S") || tipo.Equals("PQ"))
                {
                    idProductoCambio = idProducto;
                    cambioProducto = true;
                    btnAgregarProducto.PerformClick();
                }

                // Es un producto
                if (tipo.Equals("P"))
                {
                    idProductoCambio = idProducto;
                    cambioProducto = true;
                    btnAgregarServicio.PerformClick();
                }

                idProductoCambio = 0;
                cambioProducto = false;
            }
            else if (contador > 1)
            {
                MessageBox.Show("No se puede cambiar el tipo de 2\nelementos o más al mismo tiempo", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBusqueda.Focus();
            }
            else
            {
                MessageBox.Show("Seleccione un elemento para activar esta opción", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBusqueda.Focus();
            }
        }

        private void txtIrPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            // para obligar a que solo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                // permitir teclas de control como retroceso
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                // el resto de teclas pulsadas se desactivan
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtIrPagina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!txtIrPagina.Text.Equals(""))
                {
                    goToPageNumber(Convert.ToInt32(txtIrPagina.Text));
                    txtIrPagina.Text = string.Empty;
                }
                else if (txtIrPagina.Text.Equals(""))
                {
                    MessageBox.Show("Favor de verificar que\nel campo de ir a Página\nno este vacio ó su teclado\nnúmerico este activo", "Favor de verificar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void goToPageNumber(int pageNumber)
        {
            //p.primerPagina();
            //p.irAPagina(pageNumber);
            //p.cargar();
            //clickBoton = 1;
            //CargarDatos();
            //actualizar();
            if (pageNumber.Equals(p.numPag()))
            {
                p.cargar();
                clickBoton = 1;
            }
            else if (!pageNumber.Equals(p.numPag()))
            {
                p.primerPagina();
                p.irAPagina(pageNumber);
                p.cargar();
                clickBoton = 1;
            }
            CargarDatos();
            actualizar();
        }

        private void txtIrPagina_Leave(object sender, EventArgs e)
        {
            txtIrPagina.Text = string.Empty;
        }
        
        private void DGVProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // for the Name cell paint, paint the background as highligth and text as white in case of slection
            if (e.ColumnIndex == 1)
            {
                e.CellStyle.SelectionBackColor = SystemColors.Highlight;
                e.CellStyle.SelectionForeColor = Color.White;
            }
            else
            {
                e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
            }
        }



        private void Productos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)//Editar
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "F2";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(7, 0));
            }
            //else if (e.KeyCode == Keys.D && (e.Alt))//Estado
            //{
            //    timer1.Start();
            //    lAtajo.Visible = true;
            //    lAtajo.Text = "Alt + D";

            //    DGVProductos_CellClick(this, new DataGridViewCellEventArgs(8, 0));
            //}
            else if (e.KeyCode == Keys.H && (e.Alt))//Historial
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Alt + H";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(9, 0));
            }
            else if (e.KeyCode == Keys.G && (e.Alt))//Generar codigo de barras
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Alt + G";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(10, 0));
            }
            else if (e.KeyCode == Keys.I && (e.Alt))//Imagen
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Alt + I";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(11, 0));
            }
            else if (e.KeyCode == Keys.E && (e.Alt))//Etiqueta
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Alt + E";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(12, 0));
            }
            else if (e.KeyCode == Keys.F3)//Ajustar
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "F3";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(15, 0));
            }
            else if (e.KeyCode == Keys.N && (e.Control))//Agregar Producto
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + N";

                btnAgregarProducto.PerformClick();
            }
            else if (e.KeyCode == Keys.C && (e.Alt))//Agregar Combo
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Alt + C";

                btnAgregarPaquete.PerformClick();
            }
            else if (e.KeyCode == Keys.S && (e.Control))//Agregar Servicio
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + S";

                btnAgregarServicio.PerformClick();
            }
            else if (e.KeyCode == Keys.F && (e.Control))//Filtro
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + F";

                btnFilterSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)//Borrar
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "ESC";

                btnCleanFilter.PerformClick();
            }
            else if (e.KeyCode == Keys.X && (e.Control))//Agregar XML
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + X";

                btnAgregarXML.PerformClick();
            }
            else if (e.KeyCode == Keys.R && (e.Control))//Reporte
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + R";

                btnPedido.PerformClick();
            }
            else if (e.KeyCode == Keys.L && (e.Control))//Lista
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + L";

                btnListView.PerformClick();
            }
            else if (e.KeyCode == Keys.M && (e.Control))//Mosaico
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + M";

                btnPhotoView.PerformClick();
            }
            else if (e.KeyCode == Keys.P && (e.Control))//Imprimir
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + P";

                btnImprimir.PerformClick();
            }
            else if (e.KeyCode == Keys.E && (e.Control))//Boton Etiqueta
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + E";

                btnEtiqueta.PerformClick();
            }
            else if(e.KeyCode==Keys.A && (e.Control))//Boton Asignar
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + A";

                btnAsignarMultiple.PerformClick();
            }
            else if (e.KeyCode==Keys.T && (e.Control))//Boton Cmbiar Tipo
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + T";

                btnCambiarTipo.PerformClick();
            }
            else if (e.KeyCode==Keys.D && (e.Control))//Boton Deshabilitar Seleccionado
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + D";

                btnModificarEstado.PerformClick();
            }

        }
        //Atajos para el DataGridView y Botones
        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)//Editar
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "F2";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(7, 0));
            }
            //else if (e.KeyCode == Keys.D && (e.Alt))//Estado
            //{
            //    timer1.Start();
            //    lAtajo.Visible = true;
            //    lAtajo.Text = "Alt + D";

            //    DGVProductos_CellClick(this, new DataGridViewCellEventArgs(8, 0));
            //}
            else if (e.KeyCode == Keys.H && (e.Alt))//Historial
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Alt + H";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(9, 0));
            }
            else if (e.KeyCode == Keys.G && (e.Alt))//Generar codigo de barras
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Alt + G";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(10, 0));
            }
            else if (e.KeyCode == Keys.I && (e.Alt))//Imagen
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Alt + I";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(11, 0));
            }
            else if (e.KeyCode == Keys.E && (e.Alt))//Etiqueta
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Alt + E";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(12, 0));
            }
            else if (e.KeyCode == Keys.F3)//Ajustar
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "F3";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(15, 0));
            }
            else if (e.KeyCode == Keys.N && (e.Control))//Agregar Producto
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + N";

                btnAgregarProducto.PerformClick();
            }
            else if (e.KeyCode == Keys.C && (e.Alt))//Agregar Combo
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Alt + C";

                btnAgregarPaquete.PerformClick();
            }
            else if (e.KeyCode == Keys.S && (e.Control))//Agregar Servicio
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + S";

                btnAgregarServicio.PerformClick();
            }
            else if (e.KeyCode == Keys.F && (e.Control))//Filtro
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + F";

                btnFilterSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)//Borrar
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "ESC";

                btnCleanFilter.PerformClick();
            }
            else if (e.KeyCode == Keys.X && (e.Control))//Agregar XML
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + X";

                btnAgregarXML.PerformClick();
            }
            else if (e.KeyCode == Keys.R && (e.Control))//Reporte
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + R";

                btnPedido.PerformClick();
            }
            else if (e.KeyCode == Keys.L && (e.Control))//Lista
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + L";

                btnListView.PerformClick();
            }
            else if (e.KeyCode == Keys.M && (e.Control))//Mosaico
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + M";

                btnPhotoView.PerformClick();
            }
            else if (e.KeyCode == Keys.P && (e.Control))//Imprimir
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + P";

                btnImprimir.PerformClick();
            }
            else if (e.KeyCode == Keys.E && (e.Control))//Boton Etiqueta
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + E";

                btnEtiqueta.PerformClick();
            }
            else if (e.KeyCode == Keys.A && (e.Control))//Boton Asignar
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + A";

                btnAsignarMultiple.PerformClick();
            }
            else if (e.KeyCode == Keys.T && (e.Control))//Boton Cmbiar Tipo
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + T";

                btnCambiarTipo.PerformClick();
            }
            else if (e.KeyCode == Keys.D && (e.Control))//Boton Deshabilitar Seleccionado
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + D";

                btnModificarEstado.PerformClick();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int conteo = 0;
            conteo++;
            
            if (conteo == 1)
            {
                lAtajo.Visible = false;
                timer1.Enabled = false;
            }
            conteo = 0;
        }

        private void DGVProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)//Editar
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "F2";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(7, 0));
            }
            //else if (e.KeyCode == Keys.D && (e.Alt))//Estado
            //{
            //    timer1.Start();
            //    lAtajo.Visible = true;
            //    lAtajo.Text = "Alt + D";

            //    DGVProductos_CellClick(this, new DataGridViewCellEventArgs(8, 0));
            //}
            else if (e.KeyCode == Keys.H && (e.Alt))//Historial
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Alt + H";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(9, 0));
            }
            else if (e.KeyCode == Keys.G && (e.Alt))//Generar codigo de barras
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Alt + G";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(10, 0));
            }
            else if (e.KeyCode == Keys.I && (e.Alt))//Imagen
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Alt + I";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(11, 0));
            }
            else if (e.KeyCode == Keys.E && (e.Alt))//Etiqueta
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Alt + E";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(12, 0));
            }
            else if (e.KeyCode == Keys.F3)//Ajustar
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "F3";

                DGVProductos_CellClick(this, new DataGridViewCellEventArgs(15, 0));
            }
            else if (e.KeyCode == Keys.N && (e.Control))//Agregar Producto
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + N";

                btnAgregarProducto.PerformClick();
            }
            else if (e.KeyCode == Keys.C && (e.Alt))//Agregar Combo
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Alt + C";

                btnAgregarPaquete.PerformClick();
            }
            else if (e.KeyCode == Keys.S && (e.Control))//Agregar Servicio
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + S";

                btnAgregarServicio.PerformClick();
            }
            else if (e.KeyCode == Keys.F && (e.Control))//Filtro
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + F";

                btnFilterSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)//Borrar
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "ESC";

                btnCleanFilter.PerformClick();
            }
            else if (e.KeyCode == Keys.X && (e.Control))//Agregar XML
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + X";

                btnAgregarXML.PerformClick();
            }
            else if (e.KeyCode == Keys.R && (e.Control))//Reporte
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + R";

                btnPedido.PerformClick();
            }
            else if (e.KeyCode == Keys.L && (e.Control))//Lista
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + L";

                btnListView.PerformClick();
            }
            else if (e.KeyCode == Keys.M && (e.Control))//Mosaico
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + M";

                btnPhotoView.PerformClick();
            }
            else if (e.KeyCode == Keys.P && (e.Control))//Imprimir
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + P";

                btnImprimir.PerformClick();
            }
            else if (e.KeyCode == Keys.E && (e.Control))//Boton Etiqueta
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + E";

                btnEtiqueta.PerformClick();
            }
            else if (e.KeyCode == Keys.A && (e.Control))//Boton Asignar
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + A";

                btnAsignarMultiple.PerformClick();
            }
            else if (e.KeyCode == Keys.T && (e.Control))//Boton Cmbiar Tipo
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + T";

                btnCambiarTipo.PerformClick();
            }
            else if (e.KeyCode == Keys.D && (e.Control))//Boton Deshabilitar Seleccionado
            {
                timer1.Start();
                lAtajo.Visible = true;
                lAtajo.Text = "Ctrl + D";

                btnModificarEstado.PerformClick();
            }
        }

        private void DGVProductos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;

            // run this pice of code only for the selected row
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = DGVProductos.Width;
                System.Drawing.Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                var rect = new System.Drawing.Rectangle(r.X, r.Y, width - 1, r.Height - 1);
                // draw the border around the selected row using the highlight color and using a border width of 2
                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);
            }
        }

        private void btnRightSetUpVariable_Click(object sender, EventArgs e)
        {
            Button btnTag = (Button)sender;
            string name = string.Empty, newtext = string.Empty;
            name = btnTag.Name.Remove(0, 8);
            DialogResult result = MessageBox.Show("Seguro desea borrar\nel Tag(Filtro): " + name + "?", "Eliminar Filtro", MessageBoxButtons
                .YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                foreach (Control item in fLPDynamicTags.Controls.OfType<Control>())
                {
                    if (item is Panel)
                    {
                        if (item.Name.Equals("pEtiqueta" + name))
                        {
                            fLPDynamicTags.Controls.Remove(item);
                        }
                    }
                }

                for (int i = 0; i < auxWord.Count; i++)
                {
                    if (auxWord[i].Equals(name))
                    {
                        auxWord.RemoveAt(i);
                    }
                }

                if (name.Equals("Precio"))
                {
                    reiniciarVariablesDeSistemaPrecio();
                }
                else if (name.Equals("Stock"))
                {
                    reiniciarVariablesDeSistemaStock();
                }
                else if (name.Equals("NumeroRevision"))
                {
                    reiniciarVariablesDeSistemaNoRevision();
                }
                else if (name.Equals("Tipo"))
                {
                    reiniciarVariablesDeSistemaTipo();
                }
                else if (name.Equals("ProdImage"))
                {
                    reiniciarVariablesImagen();
                }
                
                if (txtBusqueda.Text.Equals(""))
                {
                    CargarDatos();
                }
                else if (!txtBusqueda.Text.Equals(""))
                {
                    quitarEspacioEnBlanco();
                    busquedaDelUsuario();
                }

                actualizarBtnFiltro();

                verificarBotonLimpiarTags();

                txtBusqueda.Focus();
            }
            else if (result == DialogResult.No)
            {
                txtBusqueda.Focus();
            }
        }

        private void filtroOrdenarPor()
        {
            if (!Properties.Settings.Default.FiltroOrdenar.Equals("Ordenar por:"))
            {
                cbOrden.Text = Properties.Settings.Default.FiltroOrdenar;
                cbOrden.DropDownStyle = ComboBoxStyle.DropDownList;
                cbMostrar.SelectedIndex = 0;
                cbMostrar.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else if (Properties.Settings.Default.FiltroOrdenar.Equals("Ordenar por:"))
            {
                cbOrden.SelectedIndex = 0;
                cbOrden.DropDownStyle = ComboBoxStyle.DropDownList;
                cbMostrar.SelectedIndex = 0;
                cbMostrar.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        private void filtroLoadProductos()
        {
            extra = string.Empty;

            filtroConSinFiltroAvanzado = cs.IniciarFiltroConSinFiltroAvanzado(FormPrincipal.userID) + $"{extra}";

            ChecarFiltroDinamicoDelSistema();

            p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);

            //string queryFiltroProducto = string.Empty;

            //filtroConSinFiltroAvanzado = $@"SELECT P.* FROM Productos AS P INNER JOIN Usuarios AS U ON P.IDUsuario = u.ID WHERE U.ID = '{FormPrincipal.userID}' AND P.Status = 1 {extra}";

            //queryFiltroProducto = $"SELECT * FROM FiltroProducto WHERE IDUsuario = '{FormPrincipal.userID}'";

            //using (DataTable dtFiltroProducto = cn.CargarDatos(queryFiltroProducto))
            //{
            //    if (!dtFiltroProducto.Rows.Count.Equals(0))
            //    {
            //        foreach (DataRow row in dtFiltroProducto.Rows)
            //        {
            //            if (row["checkBoxConcepto"].ToString().Equals("1"))
            //            {
            //                if (!row["textComboBoxConcepto"].ToString().Equals(""))
            //                {
            //                    if (row["concepto"].ToString().Equals("chkBoxImagen"))
            //                    {
            //                        filtroConSinFiltroAvanzado += $" AND P.{row["textComboBoxConcepto"].ToString()}";
            //                    }
            //                    else if (row["concepto"].ToString().Equals("chkBoxTipo"))
            //                    {
            //                        string[] words;
            //                        words = row["textComboBoxConcepto"].ToString().Split(' ');
            //                        filtroConSinFiltroAvanzado += $" AND P.{words[0].ToString()} {words[1].ToString()} '{words[2].ToString()}'";
            //                    }
            //                    else if (!row["concepto"].ToString().Equals("chkBoxImagen") || !row["concepto"].ToString().Equals("chkBoxTipo"))
            //                    {
            //                        filtroConSinFiltroAvanzado += $" AND P.{row["textComboBoxConcepto"].ToString()}{row["textCantidad"].ToString()}";
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            //if (Properties.Settings.Default.chkFiltroStock.Equals(true) && !Properties.Settings.Default.strFiltroStock.Equals(""))
            //{
            //    filtroConSinFiltroAvanzado += $" AND P.{Properties.Settings.Default.strFiltroStock}";
            //}
            //if (Properties.Settings.Default.chkFiltroPrecio.Equals(true) && !Properties.Settings.Default.strFiltroPrecio.Equals(""))
            //{
            //    filtroConSinFiltroAvanzado += $" AND P.{Properties.Settings.Default.strFiltroPrecio}";
            //}
            //if (Properties.Settings.Default.chkFiltroRevisionInventario.Equals(true) && !Properties.Settings.Default.strFiltroRevisionInventario.Equals(""))
            //{
            //    filtroConSinFiltroAvanzado += $" AND P.{Properties.Settings.Default.strFiltroRevisionInventario}";
            //}
            //if (Properties.Settings.Default.chkFiltroCombProdServ.Equals(true) && !Properties.Settings.Default.strFiltroCombProdServ.Equals(""))
            //{
            //    filtroConSinFiltroAvanzado += $" AND P.{Properties.Settings.Default.strFiltroCombProdServ}";
            //}
            //if (Properties.Settings.Default.chkFiltroImagen.Equals(true) && !Properties.Settings.Default.strFiltroImagen.Equals(""))
            //{
            //    filtroConSinFiltroAvanzado += $" AND P.{Properties.Settings.Default.strFiltroImagen}";
            //}

            //p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
        }

        private void ChecarFiltroDinamicoDelSistema()
        {
            string queryFiltroProducto = string.Empty;

            queryFiltroProducto = cs.VerificarContenidoFiltroProducto(FormPrincipal.userID);

            using (DataTable dtFiltroProducto = cn.CargarDatos(queryFiltroProducto))
            {
                if (!dtFiltroProducto.Rows.Count.Equals(0))
                {
                    foreach (DataRow row in dtFiltroProducto.Rows)
                    {
                        if (row["checkBoxConcepto"].ToString().Equals("1"))
                        {
                            if (!row["textComboBoxConcepto"].ToString().Equals(""))
                            {
                                if (row["concepto"].ToString().Equals("chkBoxImagen"))
                                {
                                    filtroConSinFiltroAvanzado += $" AND P.{row["textComboBoxConcepto"].ToString()}";
                                }
                                else if (row["concepto"].ToString().Equals("chkBoxTipo"))
                                {
                                    string[] words;
                                    words = row["textComboBoxConcepto"].ToString().Split(' ');
                                    if (!words[2].ToString().Equals(""))
                                    {
                                        filtroConSinFiltroAvanzado += $" AND P.{words[0].ToString()} {words[1].ToString()} '{words[2].ToString()}'";
                                    }
                                }
                                else if (!row["concepto"].ToString().Equals("chkBoxImagen") || !row["concepto"].ToString().Equals("chkBoxTipo"))
                                {
                                    filtroConSinFiltroAvanzado += $" AND P.{row["textComboBoxConcepto"].ToString()}{row["textCantidad"].ToString()}";
                                }
                            }
                        }
                    }
                }
            }
        }

        public void inicializarVariablesFiltro()
        {
            reiniciarVariablesDeSistemaPrecio();
            reiniciarVariablesDeSistemaStock();
            reiniciarVariablesDeSistemaNoRevision();
            reiniciarVariablesDeSistemaTipo();
            reiniciarVariablesImagen();
        }

        private void reiniciarVariablesImagen()
        {
            //Properties.Settings.Default.chkFiltroImagen = false;
            //Properties.Settings.Default.strFiltroImagen = "";
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default.Reload();

            string FiltroProducto = "chkBoxImagen";

            reiniciarFiltroDinamicoDosCampos(FiltroProducto);
        }

        private void reiniciarVariablesDeSistemaNoRevision()
        {
            //Properties.Settings.Default.chkFiltroRevisionInventario = false;
            //Properties.Settings.Default.strFiltroRevisionInventario = "";
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default.Reload();

            string FiltroProducto = "chkBoxRevision";

            reiniciarFiltroDinamicoTresCampos(FiltroProducto);
        }

        private void reiniciarVariablesDeSistemaTipo()
        {
            //Properties.Settings.Default.chkFiltroCombProdServ = false;
            //Properties.Settings.Default.strFiltroCombProdServ = "";
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default.Reload();

            string FiltroProducto = "chkBoxTipo";

            reiniciarFiltroDinamicoDosCampos(FiltroProducto);
        }

        private void reiniciarFiltroDinamicoDosCampos(string filtroProducto)
        {
            string msgFiltroProducto = filtroProducto.Remove(0, 6);

            using (DataTable dtFiltroProducto = cn.CargarDatos(cs.ReiniciarFiltroDinamico(FormPrincipal.userID, filtroProducto)))
            {
                if (!dtFiltroProducto.Rows.Count.Equals(0))
                {
                    try
                    {
                        var updateFiltroProducto = cn.EjecutarConsulta(cs.ReiniciarFiltroDinamicoDosCampos(0, string.Empty, FormPrincipal.userID, filtroProducto));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al Reiniciar el filtro de " + msgFiltroProducto + ":" + ex.Message.ToString(), "Error al Reiniciar " + msgFiltroProducto, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void reiniciarVariablesDeSistemaStock()
        {
            //Properties.Settings.Default.chkFiltroStock = false;
            //Properties.Settings.Default.strFiltroStock = "";
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default.Reload();

            string FiltroProducto = "chkBoxStock";

            reiniciarFiltroDinamicoTresCampos(FiltroProducto);
        }

        private void reiniciarVariablesDeSistemaPrecio()
        {
            //Properties.Settings.Default.chkFiltroPrecio = false;
            //Properties.Settings.Default.strFiltroPrecio = "";
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default.Reload();

            string FiltroProducto = "chkBoxPrecio";

            reiniciarFiltroDinamicoTresCampos(FiltroProducto);
        }

        private void reiniciarFiltroDinamicoTresCampos(string filtroProducto)
        {
            string msgFiltroProducto = filtroProducto.Remove(0, 6);

            using (DataTable dtFiltroProducto = cn.CargarDatos(cs.ReiniciarFiltroDinamico(FormPrincipal.userID, filtroProducto)))
            {
                if (!dtFiltroProducto.Rows.Count.Equals(0))
                {
                    try
                    {
                        var updateFiltroProducto = cn.EjecutarConsulta(cs.ReiniciarFiltroDinamicoTresCampos(0, string.Empty, FormPrincipal.userID, filtroProducto));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al Reiniciar el filtro de " + msgFiltroProducto + ":" + ex.Message.ToString(), "Error al Reiniciar " + msgFiltroProducto, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void actualizarBtnFiltro()
        {
            //if (!Properties.Settings.Default.strFiltroStock.Equals(""))
            //{
            //    btnFilterSearch.Image = global::PuntoDeVentaV2.Properties.Resources.remove;
            //}
            //else if (Properties.Settings.Default.strFiltroStock.Equals(""))
            //{
            //    btnFilterSearch.Image = global::PuntoDeVentaV2.Properties.Resources.filter;
            //}

            using (DataTable dtFiltroProducto = cn.CargarDatos(cs.VerificarContenidoFiltroProducto(FormPrincipal.userID)))
            {
                if (!dtFiltroProducto.Rows.Count.Equals(0))
                {
                    foreach (DataRow drFiltroProducto in dtFiltroProducto.Rows)
                    {
                        if (drFiltroProducto["checkBoxConcepto"].ToString().Equals("1"))
                        {
                            btnFilterSearch.Image = global::PuntoDeVentaV2.Properties.Resources.remove;
                        }
                    }
                }
            }

            using (DataTable dtFiltrosDinamicosVetanaFiltros = cn.CargarDatos(cs.VerificarVentanaFiltros(FormPrincipal.userID)))
            {
                if (!dtFiltrosDinamicosVetanaFiltros.Rows.Count.Equals(0))
                {
                    foreach (DataRow drVerificarVentanaFiltros in dtFiltrosDinamicosVetanaFiltros.Rows)
                    {
                        if (drVerificarVentanaFiltros["checkBoxValue"].ToString().Equals("1"))
                        {
                            btnFilterSearch.Image = global::PuntoDeVentaV2.Properties.Resources.remove;
                        }
                    }
                }
            }

            if (setUpVariable.Count.Equals(0))
            {
                btnFilterSearch.Image = global::PuntoDeVentaV2.Properties.Resources.filter;
            }
        }

        private void MostrarCheckBox()
        {
            System.Drawing.Rectangle rect = DGVProductos.GetCellDisplayRectangle(0, -1, true);
            // set checkbox header to center of header cell. +1 pixel to position 
            rect.Y = 5;
            rect.X = 10;// rect.Location.X + (rect.Width / 4);
            CheckBox checkBoxHeader = new CheckBox();
            checkBoxHeader.Name = "checkBoxMaster";
            checkBoxHeader.Size = new Size(15, 15);
            checkBoxHeader.Location = rect.Location;
            checkBoxHeader.CheckedChanged += new EventHandler(checkBoxMaster_CheckedChanged);
            DGVProductos.Controls.Add(checkBoxHeader);
        }

        private void checkBoxMaster_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox headerBox = ((CheckBox)DGVProductos.Controls.Find("checkBoxMaster", true)[0]);

            for (int i = 0; i < DGVProductos.RowCount; i++)
            {
                DGVProductos.Rows[i].Cells[0].Value = headerBox.Checked;
            }
        }

        /// <summary>
        /// Metodo CargarDatos
        /// </summary>
        /// <param name="status">El estatus del Producto: 1 = Activo, 0 = Inactivo, 2 = Tdodos</param>
        /// <param name="busqueda">Cadena de texto que introduce el Usuario para coincidencias</param>
        public void CargarDatos(int status = 1, string busqueda = "")
        {
            int idProducto = 0, 
                countSetUpDinamicos = 0,
                contadorTmp = 0;

            string queryHead = string.Empty,
                   queryWhereAnd = string.Empty,
                   nameTag = string.Empty,
                   queryHeadAdvancedProveedor = string.Empty,
                   queryAndAdvancedProveedor = string.Empty,
                   queryHeadAdvancedOtherTags = string.Empty,
                   queryResultOtherTags = string.Empty,
                   queryAndAdvancedOtherTagsBegin = string.Empty,
                   queryAndAdvancedOtherTags = string.Empty,
                   queryAndAdvancedOtherTagsEnd = string.Empty,
                   buscarCodigosBarraExtra = string.Empty,
                   nuevosCodigos = string.Empty,
                   theNumberAsAString = string.Empty;

            long theNumber;

            dictionaryLoad();

            if (setUpDinamicos.Count > 0)
            {
                queryHead = "SELECT DISTINCT P.* FROM Productos AS P INNER JOIN Usuarios AS U ON P.IDUsuario = U.ID ";
                queryAndAdvancedOtherTagsBegin = "AND (";
                queryAndAdvancedOtherTagsEnd = ") ";

                foreach (var item in setUpDinamicos)
                {
                    nameTag = item.Value.Item1.Remove(0, 9);
                    if (item.Value.Item2.Equals("True") && nameTag.Equals("Proveedor"))
                    {
                        queryHeadAdvancedProveedor = "INNER JOIN DetallesProducto AS ProdDetail ON ProdDetail.IDProducto = P.ID INNER JOIN Proveedores AS Prove ON Prove.ID = ProdDetail.IDProveedor ";
                        queryAndAdvancedProveedor = $"AND ProdDetail.Proveedor = '{item.Value.Item3.ToString()}' ";
                    }
                    else if (item.Value.Item2.Equals("True") && !nameTag.Equals("Proveedor"))
                    {
                        if (countSetUpDinamicos.Equals(0))
                        {
                            queryHeadAdvancedOtherTags = "INNER JOIN DetallesProductoGenerales AS GralProdDetail ON GralProdDetail.IDProducto = P.ID INNER JOIN DetalleGeneral AS GralDetail ON GralDetail.ID = GralProdDetail.IDDetalleGral ";
                            queryAndAdvancedOtherTags += $"GralDetail.Descripcion = '{item.Value.Item3.ToString()}' ";
                            countSetUpDinamicos++;
                        }
                        else if (!countSetUpDinamicos.Equals(0))
                        {
                            if (queryHeadAdvancedOtherTags.Equals(string.Empty) || queryHeadAdvancedOtherTags.Equals(""))
                            {
                                queryHeadAdvancedOtherTags = "INNER JOIN DetallesProductoGenerales AS GralProdDetail ON GralProdDetail.IDProducto = P.ID INNER JOIN DetalleGeneral AS GralDetail ON GralDetail.ID = GralProdDetail.IDDetalleGral ";
                            }
                            queryAndAdvancedOtherTags += $"OR GralDetail.Descripcion = '{item.Value.Item3.ToString()}' ";
                            //queryAndAdvancedOtherTags += $"AND GralDetail.Descripcion = '{item.Value.Item3.ToString()}' ";
                            countSetUpDinamicos++;
                        }
                        queryResultOtherTags = queryAndAdvancedOtherTagsBegin + queryAndAdvancedOtherTags + queryAndAdvancedOtherTagsEnd;
                        //queryResultOtherTags = queryAndAdvancedOtherTags;
                    }
                }
            }
            else if (setUpDinamicos.Count <= 0)
            {
                queryHead = "SELECT P.* FROM Productos AS P INNER JOIN Usuarios AS U ON P.IDUsuario = U.ID ";
            } 

            if (status.Equals(0))   // Productos Desactivados
            {
                queryWhereAnd = $"WHERE U.ID = '{FormPrincipal.userID}' AND P.Status = {status} ";
            }
            else if (status.Equals(1))  // Productos Activos
            {
                queryWhereAnd = $"WHERE U.ID = '{FormPrincipal.userID}' AND P.Status = {status} ";
            }
            else if (status.Equals(2))  // Productos Activos y Desactivados
            {
                queryWhereAnd = $"WHERE U.ID = '{FormPrincipal.userID}' ";
            }

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                extra = string.Empty;
                extra2 = string.Empty;

                listaCoincidenciasAux.Clear();

                string txtBusquedaString = string.Empty;
                string numBusqueda = string.Empty;
                string[] separatingStrings = { ") ORDER BY CASE P.ID ", "END " };
                string[] words;

                words = busqueda.Split(' ');

                foreach (var item in words)
                {
                    theNumberAsAString = item;
                    if (long.TryParse(theNumberAsAString, out theNumber))
                    {
                        numBusqueda += theNumberAsAString + " ";
                    }
                    else
                    {
                        txtBusquedaString += theNumberAsAString + " ";
                    }
                }

                if (!txtBusquedaString.Equals(""))
                {
                    var coincidencias = mb.BusquedaCoincidencias(txtBusquedaString.Trim());
                    // Si hay concidencias de la busqueda de la palabra
                    if (coincidencias.Count > 0)
                    {
                        // recorremos diccionario para eliminar coincidencias de la busqueda
                        foreach (KeyValuePair<int, int> prod in coincidencias)
                        {
                            //MessageBox.Show("ID del Producto: " + prod.Key);
                            using (DataTable dtCoincidenciaProducto = cn.CargarDatos($"SELECT ID, Nombre FROM Productos WHERE ID = '{prod.Key.ToString()}'"))
                            {
                                if (!dtCoincidenciaProducto.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow drProd in dtCoincidenciaProducto.Rows)
                                    {
                                        bool found = false;
                                        int begin, end;
                                        string producto = drProd["Nombre"].ToString();
                                        string auxTxtBusquedaString = string.Empty;
                                        string[] wordSearch;

                                        wordSearch = txtBusquedaString.Trim().Split(' ');

                                        foreach (var item in wordSearch)
                                        {
                                            found = producto.Contains(item);
                                            if (found)
                                            {
                                                begin = txtBusquedaString.IndexOf(item);
                                                end = item.Length;
                                                auxTxtBusquedaString = txtBusquedaString.Remove(begin, end);
                                                txtBusquedaString.Trim();
                                                auxTxtBusquedaString.Trim();
                                                txtBusquedaString = auxTxtBusquedaString;
                                                //MessageBox.Show("txtBusquedaString: " + txtBusquedaString + "\nauxTxtBusquedaString: " + auxTxtBusquedaString);
                                                //txtBusquedaString.Replace(item.ToString(), "");
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        extra = string.Empty;
                        // Declaramos estas variables, extra2 es para concatenar los valores para la clausula WHEN
                        // Y contadorTmp es para indicar el orden de prioridad que tendra al momento de mostrarse
                        extra2 = string.Empty;
                        contadorTmp = 1;
                        var listaCoincidencias = from entry in coincidencias orderby entry.Value descending select entry;
                        listaCoincidenciasAux = listaCoincidenciasAux.Concat(coincidencias).GroupBy(d => d.Key).ToDictionary(d => d.Key, d => d.First().Value);
                        extra += "AND P.ID IN (";
                        foreach (var producto in listaCoincidencias)
                        {
                            extra += $"{producto.Key},";
                            extra2 += $"WHEN {producto.Key} THEN {contadorTmp} ";
                            contadorTmp++;
                        }
                        // Eliminamos el último caracter que es una coma (,)
                        extra = extra.Remove(extra.Length - 1);
                        extra += ") ORDER BY CASE P.ID ";
                        extra2 += "END ";
                        // Concatenamos las dos variables para formar por completo la sentencia sql
                        extra += extra2;
                    }
                    else
                    {
                        // Original
                        extra = $" AND (P.Nombre LIKE '%{busqueda}%' OR P.NombreAlterno1 LIKE '%{busqueda}%' OR P.NombreAlterno2 LIKE '%{busqueda}%')";
                    }
                }

                string txtAndNumSearch = string.Empty;

                if (!txtBusquedaString.Equals(""))
                {
                    txtAndNumSearch += txtBusquedaString.Trim();
                }
                else if (!numBusqueda.Equals(""))
                {
                    txtAndNumSearch += " " + numBusqueda.Trim();
                }

                if (!txtAndNumSearch.Trim().Equals(""))
                {
                    // Verificar si la variable numBusqueda es un codigo de barras ó clave Interna en la tabla Prodcutos
                    var resultadoCodBarClavInt = mb.BusquedaCodigosBarrasClaveInterna(txtAndNumSearch.Trim());

                    if (resultadoCodBarClavInt.Length > 0)
                    {
                        bool isEmpty = (listaCoincidenciasAux.Count == 0);
                        if (!isEmpty)
                        {
                            foreach (var infoId in resultadoCodBarClavInt)
                            {
                                string[] palabras = infoId.Split('|');

                                if (palabras[0].Equals("1"))
                                {
                                    // Verificar que el ID del producto pertenezca al usuasio
                                    var verificarUsuario = cn.BuscarProducto(Convert.ToInt32(palabras[1].ToString()), FormPrincipal.userID);

                                    // Si el producto pertenece a este usuario con el que se tiene la sesion iniciada en la consulta
                                    // se busca directamente con base en su ID sobreescribiendo la variable "extra"
                                    if (verificarUsuario.Length > 0)
                                    {
                                        bool contieneIDProducto = listaCoincidenciasAux.Contains(new KeyValuePair<int, int>(Convert.ToInt32(palabras[1].ToString()), 1));

                                        if (!contieneIDProducto)
                                        {
                                            listaCoincidenciasAux.Add(Convert.ToInt32(palabras[1].ToString()), 1);
                                        }
                                    }
                                }
                                else if (palabras[0].Equals("0"))
                                {
                                    string nvoTheNumberAsAString = string.Empty;
                                    long nvoTheNumber;
                                    nvoTheNumberAsAString = palabras[1].ToString();
                                    if (long.TryParse(nvoTheNumberAsAString, out nvoTheNumber))
                                    {
                                        //numBusqueda += nvoTheNumberAsAString + " ";
                                        buscarCodigosBarraExtra += palabras[1].ToString() + " ";
                                    }
                                    else
                                    {
                                        nuevosCodigos += palabras[1].ToString() + " ";
                                    }
                                }
                            }
                            extra = string.Empty;
                            // Declaramos estas variables, extra2 es para concatenar los valores para la clausula WHEN
                            // Y contadorTmp es para indicar el orden de prioridad que tendra al momento de mostrarse
                            extra2 = string.Empty;
                            contadorTmp = 1;
                            var listaCoincidencias = from entry in listaCoincidenciasAux orderby entry.Value descending select entry;
                            extra += "AND P.ID IN (";
                            foreach (var producto in listaCoincidencias)
                            {
                                extra += $"{producto.Key},";
                                extra2 += $"WHEN {producto.Key} THEN {contadorTmp} ";
                                contadorTmp++;
                            }
                            // Eliminamos el último caracter que es una coma (,)
                            extra = extra.Remove(extra.Length - 1);
                            extra += ") ORDER BY CASE P.ID ";
                            extra2 += "END ";
                            // Concatenamos las dos variables para formar por completo la sentencia sql
                            extra += extra2;
                            //listaCoincidenciasAux.Clear();
                        }
                        else if (isEmpty && resultadoCodBarClavInt.Length > 0)
                        {
                            foreach (var infoId in resultadoCodBarClavInt)
                            {
                                string[] palabras = infoId.Split('|');

                                if (palabras[0].Equals("1"))
                                {
                                    // Verificar que el ID del producto pertenezca al usuasio
                                    var verificarUsuario = cn.BuscarProducto(Convert.ToInt32(palabras[1].ToString()), FormPrincipal.userID);

                                    // Si el producto pertenece a este usuario con el que se tiene la sesion iniciada en la consulta
                                    // se busca directamente con base en su ID sobreescribiendo la variable "extra"
                                    if (verificarUsuario.Length > 0)
                                    {
                                        bool contieneIDProducto = listaCoincidenciasAux.Contains(new KeyValuePair<int, int>(Convert.ToInt32(palabras[1].ToString()), 1));

                                        if (!contieneIDProducto)
                                        {
                                            listaCoincidenciasAux.Add(Convert.ToInt32(palabras[1].ToString()), 1);
                                        }
                                    }
                                }
                                else if (palabras[0].Equals("0"))
                                {
                                    buscarCodigosBarraExtra += palabras[1].ToString() + " ";
                                }
                            }
                            extra = string.Empty;
                            // Declaramos estas variables, extra2 es para concatenar los valores para la clausula WHEN
                            // Y contadorTmp es para indicar el orden de prioridad que tendra al momento de mostrarse
                            extra2 = string.Empty;
                            contadorTmp = 1;
                            var listaCoincidencias = from entry in listaCoincidenciasAux orderby entry.Value descending select entry;
                            extra += "AND P.ID IN (";
                            foreach (var producto in listaCoincidencias)
                            {
                                extra += $"{producto.Key},";
                                extra2 += $"WHEN {producto.Key} THEN {contadorTmp} ";
                                contadorTmp++;
                            }
                            // Eliminamos el último caracter que es una coma (,)
                            extra = extra.Remove(extra.Length - 1);
                            extra += ") ORDER BY CASE P.ID ";
                            extra2 += "END ";
                            // Concatenamos las dos variables para formar por completo la sentencia sql
                            extra += extra2;
                            //listaCoincidenciasAux.Clear();
                        }
                        else if (isEmpty && resultadoCodBarClavInt.Length == 0)
                        {
                            listaCoincidenciasAux.Clear();
                            foreach (var infoId in resultadoCodBarClavInt)
                            {
                                string[] palabras = infoId.Split('|');

                                if (palabras[0].Equals("1"))
                                {
                                    extra += $" AND P.ID = {palabras[1].ToString()} ";
                                }
                                else if (palabras[0].Equals("0"))
                                {

                                    buscarCodigosBarraExtra += palabras[1].ToString() + " ";
                                }
                            }
                        }
                    }
                }

                if (!buscarCodigosBarraExtra.Equals(""))
                {
                    // Verificar si la variable busqueda es un codigo de barras y existe en la tabla CodigoBarrasExtras
                    var infoProducto = mb.BuscarCodigoBarrasExtraFormProductos(buscarCodigosBarraExtra.Trim());

                    if (infoProducto.Length > 0)
                    {
                        bool isEmpty = (listaCoincidenciasAux.Count == 0);
                        if (!isEmpty)
                        {
                            foreach (var id in infoProducto)
                            {
                                string[] palabras = id.Split('|');
                                if (palabras[0].Equals("1"))
                                {
                                    // Verificar que el ID del producto pertenezca al usuasio
                                    var verificarUsuario = cn.BuscarProducto(Convert.ToInt32(palabras[1].ToString()), FormPrincipal.userID);
                                    // Si el producto pertenece a este usuario con el que se tiene la sesion iniciada en la consulta
                                    // se busca directamente con base en su ID sobreescribiendo la variable "extra"
                                    if (verificarUsuario.Length > 0)
                                    {
                                        if (!isEmpty)
                                        {
                                            bool contieneIDProducto = listaCoincidenciasAux.Contains(new KeyValuePair<int, int>(Convert.ToInt32(palabras[1].ToString()), 1));
                                            if (!contieneIDProducto)
                                            {
                                                listaCoincidenciasAux.Add(Convert.ToInt32(palabras[1].ToString()), 1);
                                            }
                                        }
                                    }
                                }
                                else if (palabras[0].Equals("0"))
                                {
                                    nuevosCodigos += palabras[1].ToString() + " ";
                                }
                            }
                            extra = string.Empty;
                            // Declaramos estas variables, extra2 es para concatenar los valores para la clausula WHEN
                            // Y contadorTmp es para indicar el orden de prioridad que tendra al momento de mostrarse
                            extra2 = string.Empty;
                            contadorTmp = 1;
                            var listaCoincidencias = from entry in listaCoincidenciasAux orderby entry.Value descending select entry;
                            extra += "AND P.ID IN (";
                            foreach (var producto in listaCoincidencias)
                            {
                                extra += $"{producto.Key},";
                                extra2 += $"WHEN {producto.Key} THEN {contadorTmp} ";
                                contadorTmp++;
                            }
                            // Eliminamos el último caracter que es una coma (,)
                            extra = extra.Remove(extra.Length - 1);
                            extra += ") ORDER BY CASE P.ID ";
                            extra2 += "END ";
                            // Concatenamos las dos variables para formar por completo la sentencia sql
                            extra += extra2;
                            listaCoincidenciasAux.Clear();
                        }
                        else if (isEmpty)
                        {
                            listaCoincidenciasAux.Clear();
                            foreach (var id in infoProducto)
                            {
                                string[] palabras = id.Split('|');
                                if (palabras[0].Equals("1"))
                                {
                                    extra = $" AND P.ID = {palabras[1].ToString()}";
                                }
                                else if (palabras[0].Equals("0"))
                                {
                                    nuevosCodigos += palabras[1].ToString() + " ";
                                }
                            }
                        }
                    }
                }

                if (!nuevosCodigos.Equals(""))
                {
                    theNumberAsAString = string.Empty;
                    theNumber = 0;
                    string[] nvoCodBar;
                    nvoCodBar = nuevosCodigos.Trim().Split(' ');
                    foreach (var item in nvoCodBar)
                    {
                        theNumberAsAString = item;
                        if (long.TryParse(theNumberAsAString, out theNumber))
                        {
                            var respuesta = MessageBox.Show("El código escaneado: " + theNumberAsAString + "\nno pertenece a un producto existente,\n\t¿Desea Registrarlo?", "Advertencia del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (respuesta == DialogResult.Yes)
                            {
                                //MessageBox.Show("Iniciando Agregado");
                                origenDeLosDatos = 5;
                                seleccionadoDato = 2;
                                nuevoCodigoBarrasDeProducto = theNumberAsAString;
                                btnAgregarProducto.PerformClick();
                                clickBoton = 1;
                            }
                            else if (respuesta == DialogResult.No)
                            {
                                busqueda = txtBusqueda.Text.ToString().Replace(theNumberAsAString, "");
                                busqueda.Trim();
                                busqueda.Replace("\\s{2,}", " ");
                                quitarEspacioEnBlanco();
                                agregarEspacioAlFinal();
                                txtBusqueda.Text = busqueda.Trim();
                            }
                        }
                        else
                        {
                            noEcontradoCodBar.Add(nuevosCodigos);
                            //MessageBox.Show("Cóodigo proporcionado: " + nuevosCodigos + "\nNo esta registrado", "Código no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            nuevosCodigos = string.Empty;
                            //extra = string.Empty;
                            //extra2 = string.Empty;
                        }
                    }
                    if (!noEcontradoCodBar.Count.Equals(0))
                    {
                        string mensajeNoEncontrado = string.Empty;
                        for (int i = 0; i < noEcontradoCodBar.Count; i++)
                        {
                            mensajeNoEncontrado += noEcontradoCodBar[i] + "\n";
                        }
                        MessageBox.Show("Cóodigo proporcionado:\n" + mensajeNoEncontrado + "No esta registrado ó no es valido su formato.", "Código no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                }
            }
            else if (string.IsNullOrWhiteSpace(busqueda))
            {
                extra = string.Empty;
                extra2 = string.Empty;
            }
            // Status 2 es poner el listado en todos los 
            // productos y servecios sin importar es activo o desactivado
            if (status == 2)
            {
                if (DGVProductos.RowCount <= 0 || DGVProductos.RowCount >= 0)
                {
                    if (busqueda == "")
                    {
                        if (!queryHeadAdvancedProveedor.Equals(""))
                        {
                            filtroConSinFiltroAvanzado = queryHead + queryHeadAdvancedProveedor + queryHeadAdvancedOtherTags + queryWhereAnd + extra + queryAndAdvancedProveedor;
                        }
                        else if (queryHeadAdvancedProveedor.Equals(""))
                        {
                            if (!queryHeadAdvancedOtherTags.Equals(""))
                            {
                                filtroConSinFiltroAvanzado = queryHead + queryHeadAdvancedOtherTags + queryWhereAnd + extra;
                            }
                            else if (queryHeadAdvancedOtherTags.Equals(""))
                            {
                                filtroConSinFiltroAvanzado = queryHead + queryWhereAnd + extra;
                            }
                        }

                        if (!queryResultOtherTags.Equals(""))
                        {
                            filtroConSinFiltroAvanzado += queryResultOtherTags;
                        }
                        else if (queryResultOtherTags.Equals(""))
                        {
                            filtroConSinFiltroAvanzado += queryResultOtherTags;
                        }

                        ChecarFiltroDinamicoDelSistema();

                        p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                    }
                    else if (busqueda != "")
                    {
                        if (!queryHeadAdvancedProveedor.Equals(""))
                        {
                            filtroConSinFiltroAvanzado = queryHead + queryHeadAdvancedProveedor + queryHeadAdvancedOtherTags + queryWhereAnd + extra + queryAndAdvancedProveedor;
                        }
                        else if (queryHeadAdvancedProveedor.Equals(""))
                        {
                            if (!queryHeadAdvancedOtherTags.Equals(""))
                            {
                                filtroConSinFiltroAvanzado = queryHead + queryHeadAdvancedOtherTags + queryWhereAnd + extra;
                            }
                            else if (queryHeadAdvancedOtherTags.Equals(""))
                            {
                                filtroConSinFiltroAvanzado = queryHead + queryWhereAnd + extra;
                                //if (busqueda.All(Char.IsLetterOrDigit))
                                //{
                                //    filtroConSinFiltroAvanzado = queryHead + queryWhereAnd + extra;
                                //}
                                //else
                                //{
                                //    filtroConSinFiltroAvanzado = queryHead + queryWhereAnd;
                                //}
                            }
                        }

                        if (!queryResultOtherTags.Equals(""))
                        {
                            filtroConSinFiltroAvanzado += queryResultOtherTags;
                        }
                        else if (queryResultOtherTags.Equals(""))
                        {
                            filtroConSinFiltroAvanzado += queryResultOtherTags;
                        }

                        ChecarFiltroDinamicoDelSistema();

                        p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                    }
                }
            }
            // Status 1 es poner el listado en todos los
            // productos activos
            if (status == 1)
            {
                if (busqueda == "")
                {
                    extra = busqueda;
                    if (DGVProductos.RowCount <= 0)
                    {
                        if (!queryHeadAdvancedProveedor.Equals(""))
                        {
                            filtroConSinFiltroAvanzado = queryHead + queryHeadAdvancedProveedor + queryHeadAdvancedOtherTags + queryWhereAnd + extra + queryAndAdvancedProveedor;
                        }
                        else if (queryHeadAdvancedProveedor.Equals(""))
                        {
                            if (!queryHeadAdvancedOtherTags.Equals(""))
                            {
                                filtroConSinFiltroAvanzado = queryHead + queryHeadAdvancedOtherTags + queryWhereAnd + extra;
                            }
                            else if (queryHeadAdvancedOtherTags.Equals(""))
                            {
                                filtroConSinFiltroAvanzado = queryHead + queryWhereAnd + extra;
                            }
                        }

                        if (!queryResultOtherTags.Equals(""))
                        {
                            filtroConSinFiltroAvanzado += queryResultOtherTags;
                        }
                        else if (queryResultOtherTags.Equals(""))
                        {
                            filtroConSinFiltroAvanzado += queryResultOtherTags;
                        }

                        ChecarFiltroDinamicoDelSistema();

                        p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                    }
                    else if (DGVProductos.RowCount >= 1 && clickBoton == 0)
                    {
                        if (!queryHeadAdvancedProveedor.Equals(""))
                        {
                            filtroConSinFiltroAvanzado = queryHead + queryHeadAdvancedProveedor + queryHeadAdvancedOtherTags + queryWhereAnd + extra + queryAndAdvancedProveedor;
                        }
                        else if (queryHeadAdvancedProveedor.Equals(""))
                        {
                            if (!queryHeadAdvancedOtherTags.Equals(""))
                            {
                                filtroConSinFiltroAvanzado = queryHead + queryHeadAdvancedOtherTags + queryWhereAnd + extra;
                            }
                            else if (queryHeadAdvancedOtherTags.Equals(""))
                            {
                                filtroConSinFiltroAvanzado = queryHead + queryWhereAnd + extra;
                            }
                        }

                        if (!queryResultOtherTags.Equals(""))
                        {
                            filtroConSinFiltroAvanzado += queryResultOtherTags;
                        }
                        else if (queryResultOtherTags.Equals(""))
                        {
                            filtroConSinFiltroAvanzado += queryResultOtherTags;
                        }

                        ChecarFiltroDinamicoDelSistema();

                        p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                    }
                }
                else if (busqueda != "")
                {
                    if (DGVProductos.RowCount >= 0 && clickBoton == 0)
                    {
                        if (!queryHeadAdvancedProveedor.Equals(""))
                        {
                            filtroConSinFiltroAvanzado = queryHead + queryHeadAdvancedProveedor + queryHeadAdvancedOtherTags + queryWhereAnd + extra + queryAndAdvancedProveedor;
                        }
                        else if (queryHeadAdvancedProveedor.Equals(""))
                        {
                            if (!queryHeadAdvancedOtherTags.Equals(""))
                            {
                                filtroConSinFiltroAvanzado = queryHead + queryHeadAdvancedOtherTags + queryWhereAnd + extra;
                            }
                            else if (queryHeadAdvancedOtherTags.Equals(""))
                            {
                                filtroConSinFiltroAvanzado = queryHead + queryWhereAnd + extra;
                                //if (busqueda.All(Char.IsLetterOrDigit))
                                //{
                                //    filtroConSinFiltroAvanzado = queryHead + queryWhereAnd + extra;
                                //}
                                //else
                                //{
                                //    filtroConSinFiltroAvanzado = queryHead + queryWhereAnd;
                                //}
                            }
                        }

                        if (!queryResultOtherTags.Equals(""))
                        {
                            filtroConSinFiltroAvanzado += queryResultOtherTags;
                        }
                        else if (queryResultOtherTags.Equals(""))
                        {
                            filtroConSinFiltroAvanzado += queryResultOtherTags;
                        }

                        ChecarFiltroDinamicoDelSistema();

                        p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                    }
                }
            }
            // Status 0 es poner el listado en todos los
            // productos Desactivados
            if (status == 0)
            {
                if (busqueda == "")
                {
                    if (DGVProductos.RowCount <= 0 || DGVProductos.RowCount >= 0)
                    {
                        if (!queryHeadAdvancedProveedor.Equals(""))
                        {
                            filtroConSinFiltroAvanzado = queryHead + queryHeadAdvancedProveedor + queryHeadAdvancedOtherTags + queryWhereAnd + extra + queryAndAdvancedProveedor;
                        }
                        else if (queryHeadAdvancedProveedor.Equals(""))
                        {
                            if (!queryHeadAdvancedOtherTags.Equals(""))
                            {
                                filtroConSinFiltroAvanzado = queryHead + queryHeadAdvancedOtherTags + queryWhereAnd + extra;
                            }
                            else if (queryHeadAdvancedOtherTags.Equals(""))
                            {
                                filtroConSinFiltroAvanzado = queryHead + queryWhereAnd + extra;
                            }
                        }

                        if (!queryResultOtherTags.Equals(""))
                        {
                            filtroConSinFiltroAvanzado += queryResultOtherTags;
                        }
                        else if (queryResultOtherTags.Equals(""))
                        {
                            filtroConSinFiltroAvanzado += queryResultOtherTags;
                        }

                        ChecarFiltroDinamicoDelSistema();

                        p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                    }
                }
                else if (busqueda != "")
                {
                    if (DGVProductos.RowCount >= 0)
                    {
                        if (!queryHeadAdvancedProveedor.Equals(""))
                        {
                            filtroConSinFiltroAvanzado = queryHead + queryHeadAdvancedProveedor + queryHeadAdvancedOtherTags + queryWhereAnd + extra + queryAndAdvancedProveedor;
                        }
                        else if (queryHeadAdvancedProveedor.Equals(""))
                        {
                            if (!queryHeadAdvancedOtherTags.Equals(""))
                            {
                                filtroConSinFiltroAvanzado = queryHead + queryHeadAdvancedOtherTags + queryWhereAnd + extra;
                            }
                            else if (queryHeadAdvancedOtherTags.Equals(""))
                            {
                                if (busqueda.All(Char.IsLetterOrDigit))
                                {
                                    filtroConSinFiltroAvanzado = queryHead + queryWhereAnd + extra;
                                }
                                else
                                {
                                    filtroConSinFiltroAvanzado = queryHead + queryWhereAnd;
                                }
                            }
                        }

                        if (!queryResultOtherTags.Equals(""))
                        {
                            filtroConSinFiltroAvanzado += queryResultOtherTags;
                        }
                        else if (queryResultOtherTags.Equals(""))
                        {
                            filtroConSinFiltroAvanzado += queryResultOtherTags;
                        }

                        ChecarFiltroDinamicoDelSistema();

                        p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                    }
                }
            }

            // Pixel en blanco para poner en las columnas que no va a mostrar un icono
            Bitmap sinImagen = new Bitmap(1, 1);
            sinImagen.SetPixel(0, 0, Color.White);

            DataSet datos = p.cargar();
            DataTable dtDatos = datos.Tables[0];

            DGVProductos.Rows.Clear();

            foreach (DataRow filaDatos in dtDatos.Rows)
            {
                number_of_rows = DGVProductos.Rows.Add();
                DataGridViewRow row = DGVProductos.Rows[number_of_rows];

                idProducto = Convert.ToInt32(filaDatos["ID"].ToString());
                row.Cells["_IDProducto"].Value = idProducto;

                string TipoProd = filaDatos["Tipo"].ToString();
                row.Cells["CheckProducto"].Value = false;

                if (productosSeleccionados.Count > 0)
                {
                    if (productosSeleccionados.ContainsKey(idProducto))
                    {
                        row.Cells["CheckProducto"].Value = true;
                    }
                }


                row.Cells["Column1"].Value = filaDatos["Nombre"].ToString();

                if (TipoProd == "P")
                {
                    //var minimoTest = filaDatos["StockMinimo"].ToString();
                    //var stockTest = filaDatos["Stock"].ToString();
                    // Verificar si el stock es menor al minimo y cambiar el texto de color
                    var minimo = Convert.ToInt32(filaDatos["StockMinimo"].ToString());
                    var maximo = Convert.ToInt32(filaDatos["StockNecesario"].ToString());
                    var stock = Convert.ToDecimal(filaDatos["Stock"].ToString());

                    if (stock < minimo)
                    {
                        row.Cells["Column2"].Value = filaDatos["Stock"].ToString();

                        row.Cells["Column2"].Style = new DataGridViewCellStyle {
                            ForeColor = Color.White,
                            BackColor = Color.Black
                        };
                    }
                    else if (stock > maximo)
                    {
                        row.Cells["Column2"].Value = filaDatos["Stock"].ToString();

                        row.Cells["Column2"].Style = new DataGridViewCellStyle
                        {
                            ForeColor = Color.White,
                            BackColor = Color.Blue
                        };
                    }
                    else
                    {
                        row.Cells["Column2"].Value = filaDatos["Stock"].ToString();
                    }
                }
                else if (TipoProd == "S" || TipoProd == "PQ")
                {
                    row.Cells["Column2"].Value = "N/A";
                }

                row.Cells["Column3"].Value = decimal.Parse(filaDatos["Precio"].ToString());
                row.Cells["Column4"].Value = filaDatos["NumeroRevision"].ToString(); //filaDatos["Categoria"].ToString(); esta era la de categoria
                row.Cells["Column5"].Value = filaDatos["ClaveInterna"].ToString();
                row.Cells["Column6"].Value = filaDatos["CodigoBarras"].ToString();
                row.Cells["Column14"].Value = filaDatos["Status"].ToString();
                row.Cells["Column15"].Value = filaDatos["ProdImage"].ToString();

                System.Drawing.Image editar = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\pencil.png");
                System.Drawing.Image estado1 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\check.png");
                System.Drawing.Image estado2 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\close.png");
                System.Drawing.Image historial = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\line-chart.png");
                System.Drawing.Image generar = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\barcode.png");
                System.Drawing.Image imagen1 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-o.png");
                System.Drawing.Image imagen2 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-picture-o.png");
                System.Drawing.Image etiqueta = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\tag.png");
                System.Drawing.Image copy = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\copy.png");
                System.Drawing.Image package = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\Combo01.png");
                System.Drawing.Image product = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\Producto01.png");
                System.Drawing.Image servicio = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\Servicio01.png");
                System.Drawing.Image ajustar = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\cog.png");

                row.Cells["Column7"].Value = editar;

                string estado = filaDatos["Status"].ToString();
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

                string ImgPath = filaDatos["ProdImage"].ToString();
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
                    row.Cells["Ajustar"].Value = ajustar;
                }
                else if (TipoProd == "S")
                {
                    row.Cells["Column16"].Value = servicio;
                    row.Cells["Ajustar"].Value = sinImagen;
                }
                else if (TipoProd == "PQ")
                {
                    row.Cells["Column16"].Value = package;
                    row.Cells["Ajustar"].Value = sinImagen;
                }


                row.Cells["_ClavProdXML"].Value = filaDatos["ClaveProducto"].ToString();
                row.Cells["_ClavUnidMedXML"].Value = filaDatos["UnidadMedida"].ToString();
                row.Cells["Impuesto"].Value = filaDatos["Impuesto"].ToString();
                row.Cells["TipoProducto"].Value = TipoProd;
                row.Cells["Categoria"].Value = filaDatos["Categoria"].ToString();
            }

            actualizar();
            // Calcular capital temporalmente (informativo)
            lbCapital.Text = "Capital: " + mb.CalcularCapital().ToString("N2");

            clickBoton = 0;
        }

        /// <summary>
        /// Fin CargarDatos
        /// </summary>

        private void actualizar()
        {
            int BeforePage = 0, AfterPage = 0, LastPage = 0;

            linkLblPaginaAnterior.Visible = false;
            linkLblPaginaSiguiente.Visible = false;

            lblCantidadRegistros.Text = p.countRow().ToString();

            linkLblPaginaActual.Text = p.numPag().ToString();
            linkLblPaginaActual.LinkColor = System.Drawing.Color.White;
            linkLblPaginaActual.BackColor = System.Drawing.Color.Black;

            BeforePage = p.numPag() - 1;
            AfterPage = p.numPag() + 1;
            LastPage = p.countPag();

            if (Convert.ToInt32(linkLblPaginaActual.Text) >= 2)
            {
                linkLblPaginaAnterior.Text = BeforePage.ToString();
                linkLblPaginaAnterior.Visible = true;
                if (AfterPage <= LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = true;
                }
                else if (AfterPage > LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = false;
                }
            }
            else if (BeforePage < 1)
            {
                linkLblPrimeraPagina.Visible = false;
                linkLblPaginaAnterior.Visible = false;
                if (AfterPage <= LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = true;
                }
                else if (AfterPage > LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = false;
                    linkLblUltimaPagina.Visible = false;
                }
            }

            txtMaximoPorPagina.Text = p.limitRow().ToString();
        }

        private void btnActualizarMaximoProductos_Click(object sender, EventArgs e)
        {
            maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
            p.actualizarTope(maximo_x_pagina);
            CargarDatos();
            //actualizarDatosDespuesDeAgregarProducto();
            actualizar();
        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            p.primerPagina();
            clickBoton = 1;
            CargarDatos();
            //actualizarDatosDespuesDeAgregarProducto();
            actualizar();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            CargarDatos();
            //actualizarDatosDespuesDeAgregarProducto();
            actualizar();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void btnUltimaPagina_Click(object sender, EventArgs e)
        {
            p.ultimaPagina();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            ///Mostrar Mensaje en la etiqueta de atajos
            //timer1.Start();
            //lAtajo.Visible = true;
            //lAtajo.Text = "Ctrl + N";
            ///

            if (opcion10 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<AgregarEditarProducto>().Count() == 1)
            {
                Application.OpenForms.OfType<AgregarEditarProducto>().First().Close();
            }

            var FormAgregar = new AgregarEditarProducto("Agregar");

            if (origenDeLosDatos == 0)
            {
                FormAgregar.DatosSource = 1;
                FormAgregar.Titulo = "Agregar Producto";
                FormAgregar.idProductoCambio = idProductoCambio;
                FormAgregar.cambioProducto = cambioProducto;
            }
            else if (origenDeLosDatos == 2)
            {
                FormAgregar.DatosSource = 2;
                FormAgregar.Titulo = "Editar Producto";
            }
            else if (origenDeLosDatos == 4)
            {
                FormAgregar.DatosSource = 4;
                FormAgregar.Titulo = "Copiar Producto";
            }
            else if (origenDeLosDatos == 5)
            {
                FormAgregar.DatosSource = 5;
                FormAgregar.Titulo = "Agregar Producto";
            }

            FormAgregar.FormClosed += delegate
            {
                //actualizarDatosDespuesDeAgregarProducto();
                //linkLblPaginaActual_Click_1(sender, e);
                //MessageBox.Show("Super mega Dislike");
                AgregarEditarProducto.stockNecesario = "0";
                clickBoton = 0;
                agregarEspacioAlFinal();
                txtBusqueda.Focus();
                CargarDatos();
            };

            if (!FormAgregar.Visible)
            {
                if (seleccionadoDato == 0)
                {
                    FormAgregar.ProdNombre = "";
                    FormAgregar.Show();

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
                    FormAgregar.claveProductoxml = ClaveProducto;
                    FormAgregar.claveUnidadMedidaxml = UnidadMedida;
                    FormAgregar.idEditarProducto = idProductoEditar;
                    FormAgregar.impuestoSeleccionado = impuestoProducto;
                    FormAgregar.Show();
                }
                else if (seleccionadoDato == 2)
                {
                    seleccionadoDato = 0;
                    FormAgregar.ProdNombre = "";
                    FormAgregar.ProdStock = "";
                    FormAgregar.ProdPrecio = "";
                    FormAgregar.ProdCategoria = "";
                    FormAgregar.ProdClaveInterna = "";
                    FormAgregar.ProdCodBarras = nuevoCodigoBarrasDeProducto;
                    FormAgregar.claveProductoxml = "";
                    FormAgregar.claveUnidadMedidaxml = "";
                    FormAgregar.idEditarProducto = "";
                    FormAgregar.impuestoSeleccionado = "";
                    FormAgregar.Show();
                }
            }
            else
            {
                if (seleccionadoDato == 0)
                {
                    FormAgregar.ProdNombre = "";
                    FormAgregar.Show();
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
                    FormAgregar.claveProductoxml = ClaveProducto;
                    FormAgregar.claveUnidadMedidaxml = UnidadMedida;
                    FormAgregar.idEditarProducto = idProductoEditar;
                    FormAgregar.impuestoSeleccionado = impuestoProducto;
                    FormAgregar.Show();
                }
                else if (seleccionadoDato == 2)
                {
                    seleccionadoDato = 0;
                    FormAgregar.ProdNombre = "";
                    FormAgregar.ProdStock = "0";
                    FormAgregar.ProdPrecio = "0";
                    FormAgregar.ProdCategoria = "";
                    FormAgregar.ProdClaveInterna = "";
                    FormAgregar.ProdCodBarras = nuevoCodigoBarrasDeProducto;
                    FormAgregar.claveProductoxml = "";
                    FormAgregar.claveUnidadMedidaxml = "";
                    FormAgregar.idEditarProducto = "";
                    FormAgregar.impuestoSeleccionado = "";
                    FormAgregar.Show();
                }
            }

            //if (origenDeLosDatos == 2 || origenDeLosDatos == 4 || origenDeLosDatos == 5)
            //{
            //    if (!txtBusqueda.Text.Equals(""))
            //    {
            //        actualizarDatosDespuesDeAgregarProducto();
            //    }
            //    else if (txtBusqueda.Text.Equals(""))
            //    {
            //        goToPageNumber(Convert.ToInt32(linkLblPaginaActual.Text));
            //        actualizar();
            //    }
            //}
            //else if (origenDeLosDatos == 0)
            //{
            //    if (txtBusqueda.Text.Equals(""))
            //    {
            //        btnUltimaPagina.PerformClick();
            //    }
            //    else
            //    {
            //        actualizarDatosDespuesDeAgregarProducto();
            //    }

            //    int ultimaPagina = p.countPag(), currentPage = Convert.ToInt32(linkLblPaginaActual.Text);

            //    if (currentPage.Equals(ultimaPagina))
            //    {
            //        CargarDatos();
            //        ultimaPagina = p.countPag();
            //        goToPageNumber(ultimaPagina);
            //    }
            //    else if (currentPage < ultimaPagina)
            //    {
            //        goToPageNumber(currentPage);
            //    }
                
            //}

            origenDeLosDatos = 0;
        }

        public void actualizarDatosDespuesDeAgregarProducto()
        {
            if (txtBusqueda.Text.Equals(""))
            {
                //CargarDatos();
                busquedaDelUsuario();
            }
            else if (!txtBusqueda.Text.Equals(""))
            {
                quitarEspacioEnBlanco();
                busquedaDelUsuario();
            }
        }

        private void btnAgregarPaquete_Click(object sender, EventArgs e)
        {
            ///Mostrar Mensaje ne la etiqueta de atajos
            timer1.Start();
            lAtajo.Visible = true;
            lAtajo.Text = "Ctrl + C";
            ///

            if (opcion11 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<AgregarEditarProducto>().Count() == 1)
            {
                Application.OpenForms.OfType<AgregarEditarProducto>().First().Close();
            }

            var FormAgregar = new AgregarEditarProducto("Agregar");

            if (origenDeLosDatos == 0)
            {
                FormAgregar.DatosSource = 1;
                FormAgregar.Titulo = "Agregar Combo";
            }
            else if (origenDeLosDatos == 2)
            {
                FormAgregar.DatosSource = 2;
                FormAgregar.Titulo = "Editar Combo";
            }
            else if (origenDeLosDatos == 4)
            {
                FormAgregar.DatosSource = 4;
                FormAgregar.Titulo = "Copiar Combo";
            }

            FormAgregar.FormClosed += delegate
            {
                //actualizarDatosDespuesDeAgregarProducto();
                linkLblPaginaActual_Click_1(sender, e);
                txtBusqueda.Focus();
            };

            if (!FormAgregar.Visible)
            {
                if (seleccionadoDato == 0)
                {
                    FormAgregar.ProdNombre = "";
                    FormAgregar.Show();
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
                    FormAgregar.claveProductoxml = ClaveProducto;
                    FormAgregar.claveUnidadMedidaxml = UnidadMedida;
                    FormAgregar.idEditarProducto = idProductoEditar;
                    FormAgregar.impuestoSeleccionado = impuestoProducto;
                    FormAgregar.Show();
                }
            }
            else
            {
                if (seleccionadoDato == 0)
                {
                    FormAgregar.ProdNombre = "";
                    FormAgregar.Show();
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
                    FormAgregar.claveProductoxml = ClaveProducto;
                    FormAgregar.claveUnidadMedidaxml = UnidadMedida;
                    FormAgregar.idEditarProducto = idProductoEditar;
                    FormAgregar.impuestoSeleccionado = impuestoProducto;
                    FormAgregar.Show();
                }
            }

            if (origenDeLosDatos == 2 || origenDeLosDatos == 4)
            {
                actualizar();
            }
            else if (origenDeLosDatos == 0)
            {
                if (txtBusqueda.Text.Equals(""))
                {
                    btnUltimaPagina.PerformClick();
                }
                else
                {
                    actualizarDatosDespuesDeAgregarProducto();
                }
            }

            origenDeLosDatos = 0;
        }

        private void btnAgregarServicio_Click(object sender, EventArgs e)
        {
            ///Mostrar Mensaje ne la etiqueta de atajos
            timer1.Start();
            lAtajo.Visible = true;
            lAtajo.Text = "Ctrl + S";
            ///

            if (opcion12 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<AgregarEditarProducto>().Count() == 1)
            {
                Application.OpenForms.OfType<AgregarEditarProducto>().First().Close();
            }

            var FormAgregar = new AgregarEditarProducto("Agregar");

            if (origenDeLosDatos == 0)
            {
                FormAgregar.DatosSource = 1;
                FormAgregar.Titulo = "Agregar Servicio";
                FormAgregar.idProductoCambio = idProductoCambio;
                FormAgregar.cambioProducto = cambioProducto;
            }
            else if (origenDeLosDatos == 2)
            {
                FormAgregar.DatosSource = 2;
                FormAgregar.Titulo = "Editar Servicio";
            }
            else if (origenDeLosDatos == 4)
            {
                FormAgregar.DatosSource = 4;
                FormAgregar.Titulo = "Copiar Servicio";
            }

            FormAgregar.FormClosed += delegate
            {
                //actualizarDatosDespuesDeAgregarProducto();
                linkLblPaginaActual_Click_1(sender, e);
                txtBusqueda.Focus();
            };

            if (!FormAgregar.Visible)
            {
                if (seleccionadoDato == 0)
                {
                    FormAgregar.ProdNombre = "";
                    FormAgregar.Show();
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
                    FormAgregar.claveProductoxml = ClaveProducto;
                    FormAgregar.claveUnidadMedidaxml = UnidadMedida;
                    FormAgregar.idEditarProducto = idProductoEditar;
                    FormAgregar.impuestoSeleccionado = impuestoProducto;
                    FormAgregar.Show();
                }
            }
            else
            {
                if (seleccionadoDato == 0)
                {
                    FormAgregar.ProdNombre = "";
                    FormAgregar.Show();
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
                    FormAgregar.claveProductoxml = ClaveProducto;
                    FormAgregar.claveUnidadMedidaxml = UnidadMedida;
                    FormAgregar.idEditarProducto = idProductoEditar;
                    FormAgregar.impuestoSeleccionado = impuestoProducto;
                    FormAgregar.Show();
                }
            }
            if (origenDeLosDatos == 2 || origenDeLosDatos == 4)
            {
                actualizar();
            }
            else if (origenDeLosDatos == 0)
            {
                if (txtBusqueda.Text.Equals(""))
                {
                    btnUltimaPagina.PerformClick();
                }
                else
                {
                    actualizarDatosDespuesDeAgregarProducto();
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
                    if (f.ShowDialog() == DialogResult.OK)  // si se selecciono correctamente un archivo en el OpenFileDialog
                    {
                        /************************************************
                        *   usamos el objeto File para almacenar las    *
                        *   propiedades de la imagen                    * 
                        ************************************************/
                        using (File = new FileStream(f.FileName, FileMode.Open, FileAccess.Read))
                        {
                            //pictureBoxProducto.Image = Image.FromStream(File);      // Cargamos la imagen en el PictureBox
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
                if (f.CheckFileExists)          // si el archivo existe
                {
                    try     // Intentamos la actualizacion de la imagen en la base de datos
                    {
                        // Obtenemos el Nuevo nombre de la imagen
                        // con la que se va hacer la copia de la imagen
                        int respuesta;
                        var source = Nombre;
                        var replacement = source.Replace('/', '_').Replace('\\', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('\"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_').Replace('-', '_').Replace(' ', '_');
                        NvoFileName = replacement + ".jpg";
                        if (logoTipo != "")     // si Logotipo es diferente a ""
                        {
                            if (File1 != null)      // si el File1 es igual a null
                            {
                                File1.Dispose();    // liberamos el objeto File1
                                System.IO.File.Delete(saveDirectoryImg + NvoFileName);  // borramos el archivo de la imagen
                                // realizamos la copia de la imagen origen hacia el nuevo destino
                                System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
                                //logoTipo = saveDirectoryImg + NvoFileName;      // Obtenemos el nuevo Path
                                logoTipo = NvoFileName;      // Obtenemos el nuevo Path
                            }
                            else    // si es que file1 es igual a null
                            {
                                // realizamos la copia de la imagen origen hacia el nuevo destino
                                System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
                                //logoTipo = saveDirectoryImg + NvoFileName;      // Obtenemos el nuevo Path
                                logoTipo = NvoFileName;      // Obtenemos el nuevo Path
                            }
                            respuesta = cn.EjecutarConsulta(cs.GuardarNvaImagen(Convert.ToInt32(idProductoEditar), logoTipo));
                        }
                        if (logoTipo == "" || logoTipo == null)		// si el valor de la variable es Null o esta ""
                        {
                            // realizamos la copia de la imagen origen hacia el nuevo destino
                            System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
                            //logoTipo = saveDirectoryImg + NvoFileName;      // Obtenemos el nuevo Path
                            logoTipo = NvoFileName;      // Obtenemos el nuevo Path
                            respuesta = cn.EjecutarConsulta(cs.GuardarNvaImagen(Convert.ToInt32(idProductoEditar), logoTipo));
                        }
                    }
                    catch (Exception ex)    // si no se puede hacer el proceso
                    {
                        // si no se borra el archivo muestra este mensaje
                        MessageBox.Show("Error al hacer el borrado No: " + ex.Message.ToString(),
                            "Error de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // si no seleccionas un archivo valido o ningun archivo muestra este mensja
                MessageBox.Show("Selecciona una Imagen", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //try
            //{
            //    using (f = new OpenFileDialog())    // Abrirmos el OpenFileDialog para buscar y seleccionar la Imagen
            //    {
            //        // le aplicamos un filtro para solo ver 
            //        // imagenes de tipo *.jpg y *.png 
            //        f.Filter = "Imagenes JPG (*.jpg)|*.jpg| Imagenes PNG (*.png)|*.png";
            //        if (f.ShowDialog() == DialogResult.OK)      // si se selecciono correctamente un archivo en el OpenFileDialog
            //        {
            //            /************************************************
            //            *   usamos el objeto File para almacenar las    *
            //            *   propiedades de la imagen                    * 
            //            ************************************************/
            //            using (File = new FileStream(f.FileName, FileMode.Open, FileAccess.Read))
            //            {
            //                info = new FileInfo(f.FileName);                        // Obtenemos toda la Informacion de la Imagen
            //                fileName = Path.GetFileName(f.FileName);                // Obtenemos el nombre de la imagen
            //                oldDirectory = info.DirectoryName;                      // Obtenemos el directorio origen de la Imagen
            //                File.Dispose();                                         // Liberamos el objeto File
            //            }
            //        }
            //    }
            //    if (!Directory.Exists(saveDirectoryImg))        // verificamos que si no existe el directorio
            //    {
            //        Directory.CreateDirectory(saveDirectoryImg);        // lo crea para poder almacenar la imagen
            //    }
            //    if (f.CheckFileExists && f.FileName != "")      // si el archivo existe
            //    {
            //        try     // Intentamos la actualizacion de la imagen en la base de datos
            //        {
            //            // Obtenemos el Nuevo nombre de la imagen
            //            // con la que se va hacer la copia de la imagen
            //            var source = fileName;
            //            var replacement = source.Replace('/', '_').Replace('\\', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('\"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_').Replace('-', '_').Replace(' ', '_');
            //            NvoFileName = replacement;
            //            //NvoFileName = replacement + ".jpg";
            //            //NvoFileName = fileName;
            //            // hacemos la nueva cadena de consulta para hacer el UpDate
            //            string insertarImagen = $"UPDATE Productos SET ProdImage = '{NvoFileName}' WHERE Nombre = '{Nombre}' AND Stock = '{Stock}' AND Precio = '{Precio}' AND ClaveInterna = '{ClaveInterna}' AND CodigoBarras = '{CodigoBarras}'";
            //            cn.EjecutarConsulta(insertarImagen);    // hacemos que se ejecute la consulta
            //            // realizamos la copia de la imagen origen hacia el nuevo destino
            //            System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
            //            CargarDatos();
            //        }
            //        catch (Exception ex)	// si no se puede hacer el proceso
            //        {
            //            // si no se borra el archivo muestra este mensaje
            //            MessageBox.Show("Error al hacer el borrado No: " + ex);
            //        }
            //    }
            //}
            //catch (Exception ex)	// si el nombre del archivo esta en blanco
            //{
            //    // si no selecciona un archivo valido o ningun archivo muestra este mensaje
            //    MessageBox.Show("selecciona una Imagen", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }

        public void mostrarFoto()
        {
            VentanaMostrarFoto.FormClosed += delegate
            {
                //CargarDatos();
                actualizarDatosDespuesDeAgregarProducto();
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
            ///Mostrar Mensaje ne la etiqueta de atajos
            timer1.Start();
            lAtajo.Visible = true;
            lAtajo.Text = "Ctrl + X";
            ///

            if (opcion1 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            FormXML.FormClosed += delegate
            {
                //CargarDatos();
                actualizarDatosDespuesDeAgregarProducto();
            };

            if (!FormXML.Visible)
            {
                FormXML.OcultarPanelRegistro();
                FormXML.ShowDialog();
                txtBusqueda.Focus();
            }
            else
            {
                FormXML.BringToFront();
            }
        }

        private void timerBusqueda_Tick(object sender, EventArgs e)
        {
            //timerBusqueda.Stop();

            //if (cbMostrar.Text == "Habilitados")
            //{
            //    CargarDatos(1, txtBusqueda.Text);
            //}
            //else if (cbMostrar.Text == "Deshabilitados")
            //{
            //    CargarDatos(0, txtBusqueda.Text);
            //}
            //else if (cbMostrar.Text == "Todos")
            //{
            //    CargarDatos(2, txtBusqueda.Text);
            //}
            //borrarAuxWordTags();
            //cargarListaDeEtiquetas();
            //verificarBotonLimpiarTags();
        }

        private void borrarAuxWordTags()
        {
            foreach (var itemAuxWord in auxWord)
            {
                foreach (Control item in fLPDynamicTags.Controls.OfType<Control>())
                {
                    if (item is Panel)
                    {
                        if (item.Name.Equals("pEtiqueta" + itemAuxWord))
                        {
                            fLPDynamicTags.Controls.Remove(item);
                        }
                    }
                }
            }
            auxWord.Clear();
        }

        public void borrarVariablesStockPrecio()
        {
            string[] words;

            foreach (var itemSetUpVariable in setUpVariable)
            {
                words = itemSetUpVariable.Split(' ');
                foreach (Control item in fLPDynamicTags.Controls.OfType<Control>())
                {
                    if (item.Name.Equals("pEtiqueta" + words[0]))
                    {
                        fLPDynamicTags.Controls.Remove(item);
                    }
                }
            }
            setUpVariable.Clear();
        }

        private void crearEtiquetaDinamica()
        {
            verificarListaDeVariablesEtiquetas();

            string nameItemLista = string.Empty;

            //borrarEtiquetasDinamicasSetUpDinamicos();

            if (!isEmptyAuxWord)
            {
                foreach (var itemLista in auxWord)
                {
                    nameItemLista = itemLista;

                    Panel panelEtiqueta = new Panel();
                    panelEtiqueta.Name = "pEtiqueta" + nameItemLista;
                    panelEtiqueta.Width = 140;
                    panelEtiqueta.Height = 32;
                    panelEtiqueta.Location = new Point(0, 2);
                    //panelEtiqueta.BackColor = Color.Red;

                    Label lblLeft = new Label();
                    lblLeft.Name = "LabelIzquierdo" + nameItemLista;
                    lblLeft.Width = 9;
                    lblLeft.Height = 23;
                    lblLeft.Image = global::PuntoDeVentaV2.Properties.Resources.imageLabelLeft;
                    lblLeft.Location = new Point(2, 2);
                    panelEtiqueta.Controls.Add(lblLeft);

                    Panel panelTagTex = new Panel();
                    panelTagTex.Name = "PanelTagTex" + nameItemLista;
                    panelTagTex.Size = size;
                    panelTagTex.BackgroundImage = global::PuntoDeVentaV2.Properties.Resources.backgroundMiddleLabel;
                    panelTagTex.BackgroundImageLayout = ImageLayout.Stretch;
                    panelTagTex.Location = new Point(lblLeft.Location.X + lblLeft.Width, 4);
                    panelEtiqueta.Controls.Add(panelTagTex);

                    label2.Text = nameItemLista;
                    var infoText = TextRenderer.MeasureText(label2.Text, new System.Drawing.Font(label2.Font.FontFamily, label2.Font.Size));
                    Ancho = infoText.Width;

                    Label lblTextFiltro = new Label();
                    lblTextFiltro.Height = 17;
                    lblTextFiltro.Location = new Point(0, 2);
                    lblTextFiltro.BackColor = Color.Transparent;
                    lblTextFiltro.Text = nameItemLista.ToString();
                    lblTextFiltro.ForeColor = Color.Red;
                    //lblTextFiltro.Font = new System.Drawing.Font("Century Gothic", 10, FontStyle.Regular);

                    panelTagTex.Controls.Add(lblTextFiltro);

                    panelTagTex.Size = new Size(Ancho + 4, Alto);

                    Button btnRight = new Button();
                    btnRight.Name = "btnRight" + nameItemLista;
                    btnRight.Width = 20;
                    btnRight.Height = 20;
                    btnRight.FlatStyle = FlatStyle.Flat;
                    btnRight.FlatAppearance.BorderSize = 0;
                    btnRight.ImageAlign = ContentAlignment.MiddleCenter;
                    btnRight.Image = global::PuntoDeVentaV2.Properties.Resources.imageLabelRight;
                    btnRight.Cursor = Cursors.Hand;
                    btnRight.Location = new Point(panelTagTex.Location.X + panelTagTex.Width - 3, 3);
                    btnRight.Click += new EventHandler(btnRight_Click);
                    panelEtiqueta.Controls.Add(btnRight);

                    panelEtiqueta.Width = Ancho + 35;

                    fLPDynamicTags.Controls.Add(panelEtiqueta);
                }
            }
            else if (isEmptyAuxWord)
            {
                txtBusqueda.Focus();
            }
        }

        private void verificarListaDeVariablesEtiquetas()
        {
            try
            {
                if (!auxWord.Equals(null))
                {
                    isEmptyAuxWord = !auxWord.Any();
                }
                if (!setUpVariable.Equals(null))
                {
                    isEmptySetUpVariable = !setUpVariable.Any();
                }
                if (!setUpDinamicos.Equals(null))
                {
                    isEmptySetUpDinamicos = !setUpDinamicos.Any();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Intentar verificar si hay algo en\nLista (De palabras a Buscar o Filtro Dinamicos) o en el Diccionario de filtro dinamico\n" + ex.Message.ToString(), "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            if (isEmptyAuxWord && isEmptySetUpVariable && isEmptySetUpDinamicos)
            {
                fLPDynamicTags.Controls.Clear();
                verificarBotonLimpiarTags();
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            Button btnTag = (Button)sender;
            string name = string.Empty, newtext = string.Empty;
            name = btnTag.Name.Remove(0, 8);
            DialogResult result = MessageBox.Show("Seguro desea borrar\nel Tag(Filtro): " + name + "?", "Eliminar Filtro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                foreach (Control item in fLPDynamicTags.Controls.OfType<Control>())
                {
                    if (item is Panel)
                    {
                        if (item.Name.Equals("pEtiqueta" + name))
                        {
                            fLPDynamicTags.Controls.Remove(item);
                        }
                    }
                }

                for (int i = 0; i < auxWord.Count; i++)
                {
                    if (auxWord[i].Equals(name))
                    {
                        auxWord.RemoveAt(i);
                    }
                }

                string newCadenaBusqueda = string.Empty;

                for (int i = 0; i < auxWord.Count; i++)
                {
                    newCadenaBusqueda += auxWord[i].ToString() + " ";
                }

                txtBusqueda.Text = newCadenaBusqueda.Trim();
                txtBusqueda.Select(txtBusqueda.Text.Length, 0);

                if (txtBusqueda.Text.Equals(""))
                {
                    CargarDatos();
                }
                else if (!txtBusqueda.Text.Equals(""))
                {
                    quitarEspacioEnBlanco();
                    busquedaDelUsuario();
                }

                txtBusqueda.Focus();

                verificarBotonLimpiarTags();
            }
            else if (result == DialogResult.No)
            {
                txtBusqueda.Focus();
            }
        }

        private void picBoxTagTex_Paint(object sender, PaintEventArgs e)
        {
            //label2.Text = auxWord[0].ToString();
            //label3.Text = auxWord[0].Length.ToString();
            //Ancho = CalculateHeightWidth(label2.Text);
            //label3.Text = Ancho.ToString();
            Ancho = auxWord[0].Length * 11;
            //label3.Text = Ancho.ToString();

            using (System.Drawing.Font myFont = new System.Drawing.Font("Century Gothic", 10))
            {
                foreach (var item in auxWord)
                {
                    strTag = item;
                    e.Graphics.DrawString(strTag, myFont, Brushes.Red, new Point(0, 1));
                    break;
                }
            }
        }

        private void cargarListaDeEtiquetas()
        {
            if (!txtBusqueda.Text.Equals(""))
            {
                palabras = txtBusqueda.Text.Split(' ');
                auxWord.Clear();
                for (int i = 0; i < palabras.Length; i++)
                {
                    if (auxWord.Count == 0)
                    {
                        auxWord.Add(palabras[i]);
                    }
                    else if (!auxWord.Contains(palabras[i]))
                    {
                        if (auxWord.Count != 0)
                        {
                            auxWord.Add(palabras[i]);
                        }
                    }
                }
                crearEtiquetaDinamica();
            }
        }

        private void quitarEspacioEnBlanco()
        {
            //txtBusqueda.Text = txtBusqueda.Text.TrimEnd();
            txtBusqueda.Text = txtBusqueda.Text.Trim();
            txtBusqueda.Select(txtBusqueda.Text.Length, 0);
        }

        private void Productos_Paint(object sender, PaintEventArgs e)
        {
            if (recargarDatos)
            {
                txtBusqueda.Text = string.Empty;

                if (txtBusqueda.Text.Equals(""))
                {
                    CargarDatos();
                }

                recargarDatos = false;

                cbOrden_SelectedIndexChanged(sender, EventArgs.Empty);

                txtBusqueda.Text = string.Empty;

                // Calcular capital temporalmente (informativo)
                lbCapital.Text = "Capital: " + mb.CalcularCapital().ToString("N2");

                borrarAuxWordTags();

                creacionEtiquetasDinamicas();

                //seleccionPersonalizadaDataGridView();
            }
        }

        private void btnAsignarMultiple_Click(object sender, EventArgs e)
        {
            ///Mostrar Mensaje ne la etiqueta de atajos
            timer1.Start();
            lAtajo.Visible = true;
            lAtajo.Text = "Ctrl + A";
            ///

            if (opcion5 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            Dictionary<int, string> lista = new Dictionary<int, string>();

            // Obtener ID de los productos seleccionados
            foreach (DataGridViewRow row in DGVProductos.Rows)
            {
                // Verificamos que el checkbox este marcado
                if ((bool)row.Cells["CheckProducto"].Value == true)
                {
                    var idProducto = Convert.ToInt32(row.Cells["_IDProducto"].Value);
                    var tipoProducto = Convert.ToString(row.Cells["TipoProducto"].Value);

                    lista.Add(idProducto, tipoProducto);
                }
            }

            productosSeleccionados = lista;

            if (productosSeleccionados.Count > 0)
            {
                if (Application.OpenForms.OfType<AsignarMultipleProductos>().Count() == 1)
                {
                    Application.OpenForms.OfType<AsignarMultipleProductos>().First().BringToFront();
                }
                else
                {
                    AsignarMultipleProductos am = new AsignarMultipleProductos();

                    am.FormClosed += delegate
                    {
                        //CargarDatos();
                        //actualizarDatosDespuesDeAgregarProducto();
                        linkLblPaginaActual_Click_1(sender, e);

                        productosSeleccionados.Clear();

                        foreach (DataGridViewRow row in DGVProductos.Rows)
                        {
                            if ((bool)row.Cells["CheckProducto"].Value == true)
                            {
                                row.Cells["CheckProducto"].Value = false;
                            }
                        }
                        txtBusqueda.Focus();
                    };

                    am.Show();
                }
            }
            else
            {
                var mensaje = "Seleccione al menos un producto para habilitar esta opción";

                MessageBox.Show(mensaje, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBusqueda.Focus();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ///Mostrar Mensaje ne la etiqueta de atajos
            timer1.Start();
            lAtajo.Visible = true;
            lAtajo.Text = "Ctrl + P";
            ///

            if (opcion9 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            Dictionary<int, string> lista = new Dictionary<int, string>();

            // Obtener ID de los productos seleccionados
            foreach (DataGridViewRow row in DGVProductos.Rows)
            {
                // Verificamos que el checkbox este marcado
                if ((bool)row.Cells["CheckProducto"].Value == true)
                {
                    var idProducto = Convert.ToInt32(row.Cells["_IDProducto"].Value);
                    var tipoProducto = Convert.ToString(row.Cells["TipoProducto"].Value);

                    lista.Add(idProducto, tipoProducto);
                }
            }

            if (lista.Count > 0)
            {
                using (var formImprimir = new ImprimirEtiqueta(lista))
                {
                    var resultado = formImprimir.ShowDialog();
                    txtBusqueda.Focus();
                }
                
            }
            else
            {
                MessageBox.Show("Seleccione al menos un producto para habilitar esta opción", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBusqueda.Focus();
            }
        }

        private void btnEtiqueta_Click(object sender, EventArgs e)
        {
            ///Mostrar Mensaje ne la etiqueta de atajos
            timer1.Start();
            lAtajo.Visible = true;
            lAtajo.Text = "Ctrl + E";
            ///

            if (opcion7 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            GenerarEtiqueta ge = new GenerarEtiqueta();

            ge.ShowDialog();
            txtBusqueda.Focus();
        }

        private void Productos_Resize(object sender, EventArgs e)
        {
            recargarDatos = false;
        }
    }
}
