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
        private Paginar p;
        string DataMemberDGV = "Productos";
        string extra = string.Empty;
        int maximo_x_pagina = 18;
        int clickBoton = 0;

        public string rutaLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static int proveedorElegido = 0;
        public static int idReporte = 0;
        public static bool botonAceptar = false;
        public static bool recargarDatos = false;

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

        int numfila, index, number_of_rows, i, seleccionadoDato, origenDeLosDatos=0, editarEstado = 0, numerofila = 0;
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

        public float ImporteDatoExtra { get; set; }
        public float DescuentoDatoExtra { get; set; }
        public int CantidadDatoExtra { get; set; }

        static public float ImporteDatoExtraFinal;
        static public float DescuentoDatoExtraFinal;
        static public int CantidadDatoExtraFinal;

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
            p.primerPagina();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void linkLblUltimaPagina_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            p.ultimaPagina();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void linkLblPaginaActual_Click(object sender, EventArgs e)
        {
            actualizar();
        }

        private void linkLblPaginaAnterior_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            p.atras();
            clickBoton = 1;
            CargarDatos();
            actualizar();
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
                panel2.Visible = false;
                panelShowPhotoView.Visible = true;
                searchPhotoProd();
                photoShow();
            }
            else if (panelShowPhotoView.Visible == true || panelShowPhotoView.Visible == false)
            {
                panelShowDGVProductosView.Visible = false;
                panel2.Visible = false;
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
                panel2.Visible = true;
                searchToProdGral();
            }
            else if (panelShowPhotoView.Visible == true || panelShowPhotoView.Visible == false)
            {
                panelShowPhotoView.Visible = false;
                panelShowDGVProductosView.Visible = true;
                panel2.Visible = true;
                searchToProdGral();
            }
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

        private void DGVProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var fila = DGVProductos.CurrentCell.RowIndex;

            int idProducto = Convert.ToInt32(DGVProductos.Rows[fila].Cells["_IDProducto"].Value);

            //Esta condicion es para que no de error al momento que se haga click en el header de la columna por error
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)    // CheckBox del Producto
                {
                    numerofila = e.RowIndex;
                    //obtenerDatosDGVProductos(numerofila);
                    //editarEstado = 4;
                    //DGVProductos.Rows[fila].Cells["CheckProducto"].Value = true;
                }
                else if (e.ColumnIndex == 7)    // Editar del Producto
                {
                    if (seleccionadoDato == 0)
                    {
                        seleccionadoDato = 1;
                        numerofila = e.RowIndex;
                        obtenerDatosDGVProductos(numerofila);
                        origenDeLosDatos = 2;
                    }
                    var producto = cn.BuscarProducto(Convert.ToInt32(idProductoEditar), Convert.ToInt32(id));
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
                else if (e.ColumnIndex == 8)    // Estado del Producto
                {
                    index = 0;

                    var resultado = MessageBox.Show("¿Realmente desea cambiar el estado del producto?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (resultado == DialogResult.Yes)
                    {
                        /*numerofila = e.RowIndex;
                        //obtenerDatosDGVProductos(numerofila);
                        //status = DGVProductos.Rows[numerofila].Cells["Column14"].Value.ToString();
                        //ModificarStatusProducto();
                        if (status == "1")
                        {
                            cbMostrar.Text = "Deshabilitados";
                        }
                        else if (status == "0")
                        {
                            cbMostrar.Text = "Habilitados";
                        }

                        DGVProductos.Refresh();*/
                        DGVProductos.Rows[fila].Cells["CheckProducto"].Value = true;
                        btnModificarEstado.PerformClick();
                    }
                }
                else if (e.ColumnIndex == 9)    // Historial de compras del Producto
                {
                    numerofila = e.RowIndex;
                    obtenerDatosDGVProductos(numerofila);
                    ViewRecordProducto();
                }
                else if (e.ColumnIndex == 10)    // Generar Codigo de Barras del Producto
                {
                    string codiBarProd = "";
                    numfila = e.RowIndex;
                    obtenerDatosDGVProductos(numfila);

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
                else if (e.ColumnIndex == 11)    // Imagen del Producto
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
                else if (e.ColumnIndex == 12)    // Etiqueta del Producto
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
                else if (e.ColumnIndex == 13)    // Copiar el Producto
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
                else if (e.ColumnIndex == 17)    // Ajustar del Producto
                {
                    //Esta es la columna de la opcion "Ajustar"
                    AjustarProducto ap = new AjustarProducto(idProducto);

                    ap.FormClosed += delegate
                    {
                        if (botonAceptar)
                        {
                            CargarDatos();

                            idReporte++;

                            botonAceptar = false;
                        }
                    };

                    ap.ShowDialog();
                }
            }
        }

        private void btnModificarEstado_Click(object sender, EventArgs e)
        {
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
                        cn.EjecutarConsulta(cs.ActualizarStatusProducto(estado, idProducto, FormPrincipal.userID));
                    }
                }
            }

            if (estado == 0)
            {
                CargarDatos(1);
            }

            if (estado == 1)
            {
                CargarDatos(0);
            }

            CheckBox master = ((CheckBox)DGVProductos.Controls.Find("checkBoxMaster", true)[0]);
            master.Checked = false;
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
            ClaveProducto = DGVProductos.Rows[fila].Cells["_ClavProdXML"].Value.ToString();
            UnidadMedida = DGVProductos.Rows[fila].Cells["_ClavUnidMedXML"].Value.ToString();
            id = FormPrincipal.userID.ToString();
            idProductoEditar = DGVProductos.Rows[fila].Cells["_IDProducto"].Value.ToString();
            impuestoProducto = DGVProductos.Rows[fila].Cells["Impuesto"].Value.ToString();
        }

        private void cbMostrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtro = Convert.ToString(cbMostrar.SelectedItem);      // tomamos el valor que se elige en el TextBox

            if (filtro == "Habilitados")                            // comparamos si el valor a filtrar es Habilitados
            {
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
            else if (filtro == "Todos")                             // comparamos si el valor a filtrar es Todos
            {
                if (panelShowDGVProductosView.Visible == true)
                {
                    clickBoton = 0;
                    CargarDatos(2);                                      // cargamos todos los registros
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
                //CargarDatos(1, txtBusqueda.Text);
                timerBusqueda.Stop();
                clickBoton = 0;
                timerBusqueda.Start();
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

            MostrarCheckBox();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            txtMaximoPorPagina.Text = maximo_x_pagina.ToString();

            panelShowPhotoView.Visible = false;
            panelShowDGVProductosView.Visible = true;

            p = new Paginar($"SELECT * FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = {FormPrincipal.userID} AND P.Status = 1 {extra}", DataMemberDGV, maximo_x_pagina);

            linkLblUltimaPagina.Text = p.countPag().ToString();

            CargarDatos();
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
            idReporte = cn.ObtenerUltimoIdReporte(FormPrincipal.userID) + 1;
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

        private void CargarDatos(int status = 1, string busqueda = "")
        {
            int idProducto = 0;

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                // Original
                extra = $"AND (P.Nombre LIKE '%{busqueda}%' OR P.NombreAlterno LIKE '%{busqueda}%' OR P.CodigoBarras LIKE '%{busqueda}%' OR P.ClaveInterna LIKE '%{busqueda}%')";

                // Verificar si la variable busqueda es un codigo de barras y existe en la tabla CodigoBarrasExtras
                var infoProducto = mb.BuscarCodigoBarrasExtra(busqueda.Trim());

                if (infoProducto.Length > 0)
                {
                    // Verificar que el ID del producto pertenezca al usuasio
                    var verificarUsuario = cn.BuscarProducto(Convert.ToInt32(infoProducto[0]), FormPrincipal.userID);
                    
                    // Si el producto pertenece a este usuario con el que se tiene la sesion iniciada en la consulta
                    // se busca directamente con base en su ID sobreescribiendo la variable "extra"
                    if (verificarUsuario.Length > 0)
                    {
                        extra = $"AND P.ID = {infoProducto[0]}";
                    }
                }
            }

            if (status == 2)
            {
                if (DGVProductos.RowCount <= 0 || DGVProductos.RowCount >= 0)
                {
                    if (busqueda == "")
                    {
                        p = new Paginar($"SELECT * FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = {FormPrincipal.userID}", DataMemberDGV, maximo_x_pagina);
                    }
                    else if (busqueda != "")
                    {
                        p = new Paginar($"SELECT * FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = {FormPrincipal.userID} {extra}", DataMemberDGV, maximo_x_pagina);
                    }
                }
            }

            if (status == 1)
            {
                if (busqueda == "")
                {
                    extra = busqueda;
                    if (DGVProductos.RowCount <= 0)
                    {
                        p = new Paginar($"SELECT * FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = {FormPrincipal.userID} AND P.Status = {status} {extra}", DataMemberDGV, maximo_x_pagina);
                    }
                    else if (DGVProductos.RowCount >= 1 && clickBoton == 0)
                    {
                        p = new Paginar($"SELECT * FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = {FormPrincipal.userID} AND P.Status = {status} {extra}", DataMemberDGV, maximo_x_pagina);
                    }
                }
                else if (busqueda != "")
                {
                    if (DGVProductos.RowCount >= 0 && clickBoton == 0)
                    {
                        p = new Paginar($"SELECT * FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = {FormPrincipal.userID} AND P.Status = {status} {extra}", DataMemberDGV, maximo_x_pagina);
                    }
                }
            }

            if (status == 0)
            {
                if (busqueda == "")
                {
                    if (DGVProductos.RowCount <= 0 || DGVProductos.RowCount >= 0)
                    {
                        p = new Paginar($"SELECT * FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = {FormPrincipal.userID} AND P.Status = {status}", DataMemberDGV, maximo_x_pagina);
                    }
                }
                else if (busqueda != "")
                {
                    if (DGVProductos.RowCount >= 0)
                    {
                        p = new Paginar($"SELECT * FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = {FormPrincipal.userID} AND P.Status = {status} {extra}", DataMemberDGV, maximo_x_pagina);
                    }
                }
            }

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

                row.Cells["Column1"].Value = filaDatos["Nombre"].ToString();

                if (TipoProd == "P")
                {
                    row.Cells["Column2"].Value = filaDatos["Stock"].ToString(); ;
                }
                else if (TipoProd == "S")
                {
                    row.Cells["Column2"].Value = "0";
                }
                else if (TipoProd == "PQ")
                {
                    row.Cells["Column2"].Value = "0";
                }

                row.Cells["Column3"].Value = decimal.Parse(filaDatos["Precio"].ToString());
                row.Cells["Column4"].Value = filaDatos["Categoria"].ToString();
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
                System.Drawing.Image package = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\Servicio.png");
                System.Drawing.Image product = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\Producto.png");
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
                }
                else if (TipoProd == "S")
                {
                    row.Cells["Column16"].Value = package;
                }
                else if (TipoProd == "PQ")
                {
                    row.Cells["Column16"].Value = package;
                }

                row.Cells["Ajustar"].Value = ajustar;

                row.Cells["_ClavProdXML"].Value = filaDatos["ClaveProducto"].ToString();
                row.Cells["_ClavUnidMedXML"].Value = filaDatos["UnidadMedida"].ToString();
                row.Cells["Impuesto"].Value = filaDatos["Impuesto"].ToString();
            }

            actualizar();
        }

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
            actualizar();
        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            p.primerPagina();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            CargarDatos();
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
            AgregarEditarProducto FormAgregar = new AgregarEditarProducto("Agregar");
            if (origenDeLosDatos == 0)
            {
                FormAgregar.DatosSource = 1;
                FormAgregar.Titulo = "Agregar Producto";
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

            FormAgregar.FormClosed += delegate
            {
                CargarDatos();
            };

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
                    FormAgregar.claveProductoxml = ClaveProducto;
                    FormAgregar.claveUnidadMedidaxml = UnidadMedida;
                    FormAgregar.idEditarProducto = idProductoEditar;
                    FormAgregar.impuestoSeleccionado = impuestoProducto;
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
                    FormAgregar.claveProductoxml = ClaveProducto;
                    FormAgregar.claveUnidadMedidaxml = UnidadMedida;
                    FormAgregar.idEditarProducto = idProductoEditar;
                    FormAgregar.impuestoSeleccionado = impuestoProducto;
                    FormAgregar.ShowDialog();
                }
            }

            if (origenDeLosDatos == 2 || origenDeLosDatos == 4)
            {
                actualizar();
            }
            else if (origenDeLosDatos == 0)
            {
                btnUltimaPagina.PerformClick();
            }

            origenDeLosDatos = 0;
        }

        private void btnAgregarPaquete_Click(object sender, EventArgs e)
        {
            AgregarEditarProducto FormAgregar = new AgregarEditarProducto("Agregar");
            if (origenDeLosDatos == 0)
            {
                FormAgregar.DatosSource = 1;
                FormAgregar.Titulo = "Agregar Paquete";
            }
            else if (origenDeLosDatos == 2)
            {
                FormAgregar.DatosSource = 2;
                FormAgregar.Titulo = "Editar Paquete";
            }
            else if (origenDeLosDatos == 4)
            {
                FormAgregar.DatosSource = 4;
                FormAgregar.Titulo = "Copiar Paquete";
            }

            FormAgregar.FormClosed += delegate 
            {
                CargarDatos();
            };
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
                    FormAgregar.claveProductoxml = ClaveProducto;
                    FormAgregar.claveUnidadMedidaxml = UnidadMedida;
                    FormAgregar.idEditarProducto = idProductoEditar;
                    FormAgregar.impuestoSeleccionado = impuestoProducto;
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
                    FormAgregar.claveProductoxml = ClaveProducto;
                    FormAgregar.claveUnidadMedidaxml = UnidadMedida;
                    FormAgregar.idEditarProducto = idProductoEditar;
                    FormAgregar.impuestoSeleccionado = impuestoProducto;
                    FormAgregar.ShowDialog();
                }
            }
            if (origenDeLosDatos == 2 || origenDeLosDatos == 4)
            {
                actualizar();
            }
            else if (origenDeLosDatos == 0)
            {
                btnUltimaPagina.PerformClick();
            }
            origenDeLosDatos = 0;
        }

        private void btnAgregarServicio_Click(object sender, EventArgs e)
        {
            AgregarEditarProducto FormAgregar = new AgregarEditarProducto("Agregar");
            if (origenDeLosDatos == 0)
            {
                FormAgregar.DatosSource = 1;
                FormAgregar.Titulo = "Agregar Servicio";
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
                CargarDatos();
            };
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
                    FormAgregar.claveProductoxml = ClaveProducto;
                    FormAgregar.claveUnidadMedidaxml = UnidadMedida;
                    FormAgregar.idEditarProducto = idProductoEditar;
                    FormAgregar.impuestoSeleccionado = impuestoProducto;
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
                    FormAgregar.claveProductoxml = ClaveProducto;
                    FormAgregar.claveUnidadMedidaxml = UnidadMedida;
                    FormAgregar.idEditarProducto = idProductoEditar;
                    FormAgregar.impuestoSeleccionado = impuestoProducto;
                    FormAgregar.ShowDialog();
                }
            }
            if (origenDeLosDatos == 2 || origenDeLosDatos == 4)
            {
                actualizar();
            }
            else if (origenDeLosDatos == 0)
            {
                btnUltimaPagina.PerformClick();
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

        /*private void ModificarStatusProductoChkBox()
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
        }*/

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

        private void timerBusqueda_Tick(object sender, EventArgs e)
        {
            timerBusqueda.Stop();

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
        }

        private void Productos_Paint(object sender, PaintEventArgs e)
        {
            if (recargarDatos)
            {
                CargarDatos();

                recargarDatos = false;

                cbOrden_SelectedIndexChanged(sender, EventArgs.Empty);

                txtBusqueda.Text = string.Empty;
            }
        }

        private void Productos_Resize(object sender, EventArgs e)
        {
            recargarDatos = false;
        }
    }
}
