using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class AgregarEditarProducto : Form
    {
        static public int id = 1;
        static public string precioProducto = "";
        //Esta variables se usan en el form detalle de facturacion
        static public string datosImpuestos = null;
        static public string claveProducto = null;
        static public string claveUnidadMedida = null;
        static public string baseProducto = null;
        static public string ivaProducto = null;
        static public string impuestoProducto = null;
        //Para los impuestos obtenidos desde el XML
        static public string impuestoProductoXML = string.Empty;
        static public string importeProductoXML = string.Empty;
 
        // Para los detalles del producto: Proveedor, Categoria, etc.
        static public string detallesProducto = null;
        static public string infoProveedor = string.Empty;
        static public string infoCategoria = string.Empty;
        static public string infoUbicacion = string.Empty;


        static public DataTable SearchDesCliente, SearchDesMayoreo;
        static public List<string> descuentos = new List<string>();

        List<string> prodServPaq = new List<string>();

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        AgregarDetalleFacturacionProducto FormDetalle;
        AgregarDescuentoProducto FormAgregar;
        AgregarDetalleProducto FormDetalleProducto;

        public NvoProduct nvoProductoAdd = new NvoProduct();
        public CantidadProdServicio CantidadPordServPaq = new CantidadProdServicio();
        
        int idProducto;

        /****************************
		*   Codigo de Emmanuel      *
		****************************/
        string TituloForm=string.Empty;

        public string Titulo { set; get; }
        public int DatosSource { set; get; }
        public string ProdNombre { set; get; }
        public string ProdStock { set; get; }
        public string ProdPrecio { set; get; }
        public string ProdCategoria { set; get; }
        public string ProdClaveInterna { set; get; }
        public string ProdCodBarras { set; get; }
        public string claveProductoxml { set; get; }
        public string claveUnidadMedidaxml { set; get; }
        public string CantidadProdServicio { get; set; }
        public string impuestoProdXML { get; set; }
        public string importeProdXML { get; set; }

        public string fechaProdXML { get; set; }
        public string FolioProdXML { get; set; }
        public string RFCProdXML { get; set; }
        public string NobEmisorProdXML { get; set; }
        public string ClaveProdEmisorProdXML { get; set; }
        public string DescuentoProdXML { get; set; }
        public string PrecioCompraXML { get; set; }

        public string idEditarProducto { get; set; }
        public string impuestoSeleccionado { get; set; }

        public float ImporteProdNvo { get; set; }
        public float DescuentoProdNvo { get; set; }
        public int CantidadProdNvo { get; set; }

        static public int DatosSourceFinal = 0;
        static public string ProdNombreFinal = "";
        static public string ProdStockFinal = "";
        static public string ProdPrecioFinal = "";
        static public string ProdCategoriaFinal = "";
        static public string ProdClaveInternaFinal = "";
        static public string ProdCodBarrasFinal = "";
        static public string CantProdServFinal = "";

        static public float ImporteNvoProd = 0;
        static public float DescuentoNvoProd = 0;
        static public int CantidadNvoProd = 0;

        float precioOriginalSinIVA = 0;
        float precioOriginalConIVA = 0;
        float importeReal = 0;
        float PrecioRecomendado = 0;

        static public string FechaXMLNvoProd;
        static public string FolioXMLNvoProd;
        static public string RFCXMLNvoProd;
        static public string NobEmisorXMLNvoProd;
        static public string ClaveProdEmisorXMLNvoProd;
        static public string DescuentoXMLNvoProd;
        static public string PrecioCompraXMLNvoProd;

        static public string idProductoFinal = string.Empty;
        static public string impuestoProductoFinal = string.Empty;


        DataTable SearchProdResult, SearchCodBarExtResult, datosProductos;

        OpenFileDialog f;       // declaramos el objeto de OpenFileDialog

        // objeto para el manejo de las imagenes
        FileStream File, File1;
        FileInfo info;

        string queryBuscarProd, idProductoBuscado, queryUpdateProd, queryInsertProd, queryBuscarCodBarExt, queryBuscarDescuentoCliente, queryDesMayoreo, queryProductosDeServicios, queryNvoProductosDeServicios;
        int respuesta;

        DataTable dtProductosDeServicios, dtNvoProductosDeServicios=null;

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

        string tipoProdServ;

        string queryProductos;

        DataRow row, rowNvoProd;

        Control _lastEnteredControl;    // para saber cual fue el ultimo control con el cursor activo

        // variable de text para poder dirigirnos a la carpeta principal para
        // poder jalar las imagenes o cualquier cosa que tengamos hay en ese directorio
        //public string rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

        const string fichero = @"\PUDVE\settings\codbar\setupCodBar.txt";     // directorio donde esta el archivo de numero de codigo de barras consecutivo
        string Contenido;                                               // para obtener el numero que tiene el codigo de barras en el arhivo

        long CodigoDeBarras;                                            // variable entera para llevar un consecutivo de codigo de barras

        List<string> codigosBarrras = new List<string>();   // para agregar los datos extras de codigos de barras

        public DataTable dtClaveInterna;        // almacena el resultado de la funcion de CargarDatos de la funcion searchClavIntProd
        public DataTable dtCodBar;              // almacena el resultado de la funcion de CargarDatos de la funcion searchCodBar

        int resultadoSearchNoIdentificacion;    // sirve para ver si el producto existe en los campos CodigoBarras y ClaveInterna en la funcion searchClavIntProd()
        int resultadoSearchCodBar;              // sirve para ver si el producto existe en los campos CodigoBarras y ClaveInterna en la funcion searchCodBar()

        string filtro;

        int PH;
        bool Hided, Hided1;

        List<string> ProductosDeServicios = new List<string>();     // para agregar los productos del servicio o paquete
        List<ItemsProductoComboBox> prodList;

        int numCombo = 1, indexItem = 1, totCB=0;

        string comboBoxNombre;

        string NombreProducto = "";
        string CantidadProducto = "";
        string IDProducto = "";


        public string CBNombProdPasado { get; set; }
        static public string CBNombProd = String.Empty;
        static public int seleccionListaStock;

        public void PrimerCodBarras()
        {
            Contenido = "7777000001";
        }

        public void AumentarCodBarras()
        {
            //txtCodigoBarras.Text = Contenido;
            string txtBoxName;
            txtBoxName=_lastEnteredControl.Name;
            if (txtBoxName != "cbTipo" && txtBoxName != "txtNombreProducto" && txtBoxName != "txtStockProducto" && txtBoxName != "txtPrecioProducto" && txtBoxName != "txtCategoriaProducto")
            {
                _lastEnteredControl.Text = Contenido;

                CodigoDeBarras = long.Parse(Contenido);
                CodigoDeBarras++;
                Contenido = CodigoDeBarras.ToString();

                using (StreamWriter outfile = new StreamWriter(Properties.Settings.Default.rutaDirectorio + fichero))
                {
                    outfile.WriteLine(Contenido);
                }
            }
            else
            {
                MessageBox.Show("Campo no Valido para generar\nCodigo de Barras los campos validos son\nClave Interna y Codigo de Barras... Gracias", "Anvertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LimpiarCampos()
        {
            txtNombreProducto.Text = "";
            txtStockProducto.Text = "0";
            txtPrecioCompra.Text = "0";
            txtPrecioProducto.Text = "0";
            txtCategoriaProducto.Text = "";
            txtClaveProducto.Text = "";
            txtCodigoBarras.Text = "";
        }

        public void cargarCodBarExt()
        {
            id = 0;
            panelContenedor.Controls.Clear();
            foreach (DataRow renglon in SearchCodBarExtResult.Rows)
            {
                // generamos el panel dinamico
                FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                panelHijo.Name = "panelGenerado" + id;
                panelHijo.Height = 25;
                panelHijo.Width = 200;
                panelHijo.HorizontalScroll.Visible = false;

                // generamos el textbox dinamico 
                TextBox tb = new TextBox();
                tb.Name = "textboxGenerado" + id;
                tb.Width = 165;
                tb.Height = 20;
                tb.Text = renglon[1].ToString();
                tb.Enter += new EventHandler(TextBox_Enter);
                tb.KeyDown += new KeyEventHandler(TextBox_Keydown);
                
                // generamos el boton dinamico
                Button bt = new Button();
                bt.Cursor = Cursors.Hand;
                bt.Text = "X";
                bt.Name = "btnGenerado" + id;
                bt.Height = 23;
                bt.Width = 23;
                bt.BackColor = ColorTranslator.FromHtml("#C00000");
                bt.ForeColor = ColorTranslator.FromHtml("white");
                bt.FlatStyle = FlatStyle.Flat;
                bt.TextAlign = ContentAlignment.MiddleCenter;
                bt.Anchor = AnchorStyles.Top;
                bt.Click += new EventHandler(ClickBotones);
                
                // agregamos al panel el textbox
                panelHijo.Controls.Add(tb);

                // agregamos el boton
                panelHijo.Controls.Add(bt);
                // le damos la direccion del panel
                panelHijo.FlowDirection = FlowDirection.LeftToRight;

                // agregamos el panel a la forma
                panelContenedor.Controls.Add(panelHijo);
                // darle direccion al panel
                panelContenedor.FlowDirection = FlowDirection.TopDown;

                tb.Focus();
                id++;
            }
        }

        public void cargarDatosExtra()
        {
            queryBuscarProd = $"SELECT * FROM Productos WHERE Nombre = '{ProdNombre}' AND Precio = '{ProdPrecio}' AND Categoria = '{ProdCategoria}' AND IDUsuario = '{FormPrincipal.userID}'";
            SearchProdResult = cn.CargarDatos(queryBuscarProd);
            idProductoBuscado = SearchProdResult.Rows[0]["ID"].ToString();
            tipoProdServ = SearchProdResult.Rows[0]["Tipo"].ToString();
            queryBuscarCodBarExt = $"SELECT * FROM CodigoBarrasExtras WHERE IDProducto = '{idProductoBuscado}'";
            SearchCodBarExtResult = cn.CargarDatos(queryBuscarCodBarExt);
            cargarCodBarExt();
            queryBuscarDescuentoCliente = $"SELECT * FROM DescuentoCliente WHERE IDProducto = '{idProductoBuscado}'";
            SearchDesCliente = cn.CargarDatos(queryBuscarDescuentoCliente);
            queryDesMayoreo = $"SELECT * FROM DescuentoMayoreo WHERE IDProducto = '{idProductoBuscado}'";
            SearchDesMayoreo = cn.CargarDatos(queryDesMayoreo);
            if (tipoProdServ == "S" || tipoProdServ == "PQ")
            {
                DataRow rowProdServPaq;
                queryProductosDeServicios = $"SELECT * FROM ProductosDeServicios WHERE IDServicio = '{idProductoBuscado}'";
                dtProductosDeServicios = cn.CargarDatos(queryProductosDeServicios);
                rowProdServPaq = dtProductosDeServicios.Rows[0];
                cbTipo.Text = "Servicio / Paquete";
                if (dtProductosDeServicios.Rows.Count > 0)
                {
                    if (rowProdServPaq["NombreProducto"].ToString() != "")
                    {
                        btnAdd.Visible = true;
                        Hided = true;
                        btnAdd.PerformClick();
                    }
                }
            }
            else if (tipoProdServ == "P")
            {
                cbTipo.Text = "Producto";
                btnAdd.Visible = false;
            }
        }

        private void mostrarProdServPaq()
        {
            NombreProducto = "";
            CantidadProducto = "";
            IDProducto = "";


            chkBoxConProductos.Visible = true;
            id = 0;
            flowLayoutPanel2.Controls.Clear();

            Label Titulo = new Label();
            Titulo.Name = "Title";
            Titulo.Width = 400;
            Titulo.Height = 20;
            Titulo.Text = "Descripción de productos que contiene:";
            Titulo.Location = new Point(0, 0);

            flowLayoutPanel2.Controls.Add(Titulo);
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;

            if (dtNvoProductosDeServicios != null)
            {
                //MessageBox.Show("En proceso de construcción", "Proceso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                foreach (DataRow dtRow in dtNvoProductosDeServicios.Rows)
                {
                    NombreProducto = dtRow["Nombre"].ToString();
                    IDProducto = dtRow["ID"].ToString();

                    FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                    panelHijo.Name = "panelGenerado" + id;
                    panelHijo.Width = 749;
                    panelHijo.Height = 50;
                    panelHijo.HorizontalScroll.Visible = false;

                    Label lb1 = new Label();
                    lb1.Name = "labelProductoGenerado" + id;
                    lb1.Width = 60;
                    lb1.Height = 17;
                    lb1.Text = "Producto:";

                    ComboBox cb = new ComboBox();
                    cb.Name = "comboBoxGenerador" + id;
                    cb.Width = 300;
                    cb.Height = 24;
                    try
                    {
                        foreach (var items in prodList)
                        {
                            cb.Items.Add(items.ToString());
                        }
                        cb.Text = NombreProducto;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error: " + ex.Message.ToString(), "error Text", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cb.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cb.BackColor = System.Drawing.SystemColors.Window;
                    cb.FormattingEnabled = true;
                    cb.Enter += new EventHandler(ComboBox_Enter);

                    Label lb2 = new Label();
                    lb2.Name = "labelCantidadGenerado" + id;
                    lb2.Width = 50;
                    lb2.Height = 17;
                    lb2.Text = "Cantidad:";

                    TextBox tb = new TextBox();
                    tb.Name = "textBoxGenerado" + id;
                    tb.Width = 250;
                    tb.Height = 22;
                    tb.Text = "0";
                    tb.Enter += new EventHandler(TextBoxProductosServ_Enter);
                    tb.KeyDown += new KeyEventHandler(TexBoxProductosServ_Keydown);

                    Button bt = new Button();
                    bt.Cursor = Cursors.Hand;
                    bt.Text = "X";
                    bt.Name = "btnGenerado" + id;
                    bt.Height = 23;
                    bt.Width = 23;
                    bt.BackColor = ColorTranslator.FromHtml("#C00000");
                    bt.ForeColor = ColorTranslator.FromHtml("white");
                    bt.FlatStyle = FlatStyle.Flat;
                    bt.TextAlign = ContentAlignment.MiddleCenter;
                    bt.Anchor = AnchorStyles.Top;
                    bt.Click += new EventHandler(ClickBotonesProductos);

                    panelHijo.Controls.Add(lb1);
                    panelHijo.Controls.Add(cb);
                    panelHijo.Controls.Add(lb2);
                    panelHijo.Controls.Add(tb);
                    panelHijo.Controls.Add(bt);
                    panelHijo.FlowDirection = FlowDirection.LeftToRight;

                    flowLayoutPanel2.Controls.Add(panelHijo);
                    flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;

                    tb.Focus();
                    id++;
                }
            }
            if (dtProductosDeServicios != null)
            {
                foreach (DataRow dtRow in dtProductosDeServicios.Rows)
                {
                    NombreProducto = dtRow["NombreProducto"].ToString();
                    CantidadProducto = dtRow["Cantidad"].ToString();
                    IDProducto = dtRow["IDProducto"].ToString();

                    FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                    panelHijo.Name = "panelGenerado" + id;
                    panelHijo.Width = 749;
                    panelHijo.Height = 50;
                    panelHijo.HorizontalScroll.Visible = false;

                    Label lb1 = new Label();
                    lb1.Name = "labelProductoGenerado" + id;
                    lb1.Width = 60;
                    lb1.Height = 17;
                    lb1.Text = "Producto:";

                    ComboBox cb = new ComboBox();
                    cb.Name = "comboBoxGenerador" + id;
                    cb.Width = 300;
                    cb.Height = 24;
                    try
                    {
                        foreach (var items in prodList)
                        {
                            cb.Items.Add(items.ToString());
                        }
                        cb.Text = NombreProducto;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error: " + ex.Message.ToString(), "error Text", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cb.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cb.BackColor = System.Drawing.SystemColors.Window;
                    cb.FormattingEnabled = true;
                    cb.Enter += new EventHandler(ComboBox_Enter);

                    Label lb2 = new Label();
                    lb2.Name = "labelCantidadGenerado" + id;
                    lb2.Width = 50;
                    lb2.Height = 17;
                    lb2.Text = "Cantidad:";

                    TextBox tb = new TextBox();
                    tb.Name = "textBoxGenerado" + id;
                    tb.Width = 250;
                    tb.Height = 22;
                    tb.Text = CantidadProducto;
                    tb.Enter += new EventHandler(TextBoxProductosServ_Enter);
                    tb.KeyDown += new KeyEventHandler(TexBoxProductosServ_Keydown);

                    Button bt = new Button();
                    bt.Cursor = Cursors.Hand;
                    bt.Text = "X";
                    bt.Name = "btnGenerado" + id;
                    bt.Height = 23;
                    bt.Width = 23;
                    bt.BackColor = ColorTranslator.FromHtml("#C00000");
                    bt.ForeColor = ColorTranslator.FromHtml("white");
                    bt.FlatStyle = FlatStyle.Flat;
                    bt.TextAlign = ContentAlignment.MiddleCenter;
                    bt.Anchor = AnchorStyles.Top;
                    bt.Click += new EventHandler(ClickBotonesProductos);

                    panelHijo.Controls.Add(lb1);
                    panelHijo.Controls.Add(cb);
                    panelHijo.Controls.Add(lb2);
                    panelHijo.Controls.Add(tb);
                    panelHijo.Controls.Add(bt);
                    panelHijo.FlowDirection = FlowDirection.LeftToRight;

                    flowLayoutPanel2.Controls.Add(panelHijo);
                    flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;

                    tb.Focus();
                    id++;
                }
            }
        }

        private void ComboBox_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;
        }

        public void cargarDatosNvoProd()
        {
            if (DatosSourceFinal == 1)
            {
                ImporteNvoProd = ImporteProdNvo;
                DescuentoNvoProd = DescuentoProdNvo;
                CantidadNvoProd = CantidadProdNvo;
            }
        }

        public void cargarDatos()
        {
            ProdNombreFinal = ProdNombre;
            ProdStockFinal = ProdStock;
            ProdPrecioFinal = ProdPrecio;
            precioProducto = ProdPrecio; //Se asigna a la variable que se utiliza en detalles de facturacion
            ProdCategoriaFinal = ProdCategoria;
            ProdClaveInternaFinal = ProdClaveInterna;
            ProdCodBarrasFinal = ProdCodBarras;
            claveProducto = claveProductoxml;
            claveUnidadMedida = claveUnidadMedidaxml;
            impuestoProductoXML = impuestoProdXML;
            importeProductoXML = importeProdXML;
            idProductoFinal = idEditarProducto;
            impuestoProductoFinal = impuestoSeleccionado;

            if (DatosSourceFinal == 3)
            {
                FechaXMLNvoProd = fechaProdXML;
                FolioXMLNvoProd = FolioProdXML;
                RFCXMLNvoProd = RFCProdXML;
                NobEmisorXMLNvoProd = NobEmisorProdXML;
                ClaveProdEmisorXMLNvoProd = ClaveProdEmisorProdXML;
                DescuentoXMLNvoProd = DescuentoProdXML;
                PrecioCompraXMLNvoProd = PrecioCompraXML;
            }

            txtNombreProducto.Text = ProdNombreFinal;
            txtStockProducto.Text = ProdStockFinal;
            txtPrecioProducto.Text = ProdPrecioFinal;
            txtCategoriaProducto.Text = ProdCategoriaFinal;
            txtClaveProducto.Text = ProdClaveInternaFinal.Trim();

            if (DatosSourceFinal == 2)
            {
                txtCodigoBarras.Text = ProdCodBarrasFinal.Trim();
                cargarDatosExtra();
            }
            else if (DatosSourceFinal == 4)
            {
                cargarDatosExtra();
            }
        }

        public void LimpiarDatos()
        {
            DatosSourceFinal = 0;
            ProdNombreFinal = "";
            ProdStockFinal = "";
            ProdPrecioFinal = "";
            ProdCategoriaFinal = "";
            ProdClaveInternaFinal = "";
            ProdCodBarrasFinal = "";

            DatosSource = 0;
            ProdNombre = "";
            ProdStock = "";
            ProdPrecio = "";
            ProdCategoria = "";
            ProdClaveInterna = "";
            ProdCodBarras = "";

            txtNombreProducto.Text = "";
            txtStockProducto.Text = "";
            txtPrecioProducto.Text = "";
            txtCategoriaProducto.Text = "";
            txtClaveProducto.Text = "";
            txtCodigoBarras.Text = "";
        }
        /* Fin del codigo de Emmanuel */

        public AgregarEditarProducto(string titulo)
        {
            InitializeComponent();
        }

        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string texto = txtCodigoBarras.Text.Trim();

                if (texto.Length >= 5)
                {
                    GenerarTextBox();
                    //MessageBox.Show(texto, "Mensaje");
                }
            }
        }

        private void GenerarTextBox()
        {
            FlowLayoutPanel panelHijo = new FlowLayoutPanel();
            panelHijo.Name = "panelGenerado" + id;
            panelHijo.Height = 25;
            panelHijo.Width = 200;
            panelHijo.HorizontalScroll.Visible = false;

            TextBox tb = new TextBox();
            tb.Name = "textboxGenerado" + id;
            tb.Width = 165;
            tb.Height = 20;
            tb.Enter += new EventHandler(TextBox_Enter);
            tb.KeyDown += new KeyEventHandler(TextBox_Keydown);

            Button bt = new Button();
            bt.Cursor = Cursors.Hand;
            bt.Text = "X";
            bt.Name = "btnGenerado" + id;
            bt.Height = 23;
            bt.Width = 23;
            bt.BackColor = ColorTranslator.FromHtml("#C00000");
            bt.ForeColor = ColorTranslator.FromHtml("white");
            bt.FlatStyle = FlatStyle.Flat;
            bt.TextAlign = ContentAlignment.MiddleCenter;
            bt.Anchor = AnchorStyles.Top;
            bt.Click += new EventHandler(ClickBotones);

            panelHijo.Controls.Add(tb);
            panelHijo.Controls.Add(bt);
            panelHijo.FlowDirection = FlowDirection.LeftToRight;
            
            panelContenedor.Controls.Add(panelHijo);
            panelContenedor.FlowDirection = FlowDirection.TopDown;

            tb.Focus();
            id++;
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;
        }

        private void TextBox_Keydown(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (e.KeyCode == Keys.Enter)
            {
                string texto = tbx.Text;

                if (texto.Length >= 5)
                {
                    GenerarTextBox();
                }
            }
        }

        private void ClickBotones(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            string nombreBoton = bt.Name;

            string idBoton = nombreBoton.Substring(11);
            string nombreTextBox = "textboxGenerado" + idBoton;
            string nombrePanel = "panelGenerado" + idBoton;

            foreach (Control item in panelContenedor.Controls.OfType<Control>())
            {
                if (item.Name == nombrePanel)
                {
                    panelContenedor.Controls.Remove(item);
                    panelContenedor.Controls.Remove(bt);
                }
            }
        }

        private void btnGenerarCB_Click(object sender, EventArgs e)
        {
            /****************************
            *                           *
            *   Codigo de Alejandro     *
            *                           *
            ****************************/

            //string fecha = DateTime.Now.ToString();
            //fecha = fecha.Replace(" ", "");
            //fecha = fecha.Replace("/", "");
            //fecha = fecha.Replace(":", "");
            //fecha = fecha.Substring(3, 11);

            //txtCodigoBarras.Text = fecha;

            /********************************
            *   Fin de Codigo Alejandro     *
            ********************************/

            // leemos el archivo de codigo de barras que lleva el consecutivo
            using (StreamReader readfile = new StreamReader(Properties.Settings.Default.rutaDirectorio + fichero))
            {
                Contenido = readfile.ReadToEnd();   // se lee todo el archivo y se almacena en la variable Contenido
            }
            if (Contenido == "")        // si el contenido es vacio 
            {
                PrimerCodBarras();      // iniciamos el conteo del codigo de barras
                AumentarCodBarras();    // Aumentamos el codigo de barras para la siguiente vez que se utilice
            }
            else if (Contenido != "")   // si el contenido no es vacio
            {
                //MessageBox.Show("Trabajando en el Proceso");
                AumentarCodBarras();    // Aumentamos el codigo de barras para la siguiente vez que se utilice
            }
        }

        private void btnAgregarDescuento_Click(object sender, EventArgs e)
        {
            if (txtPrecioProducto.Text == "")
            {
                MessageBox.Show("Es necesario agregar el precio del producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtStockProducto.Text == "")
            {
                MessageBox.Show("Es necesario agregar el stock del producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (DatosSourceFinal == 2 || DatosSourceFinal == 4)
                {
                    precioProducto = txtPrecioProducto.Text;
                }

                if (FormAgregar != null)
                {
                    FormAgregar.Show();
                    FormAgregar.BringToFront();
                }
                else
                {
                    FormAgregar = new AgregarDescuentoProducto();
                    FormAgregar.ShowDialog();
                }
            }
        }

        private void btnDetalleFacturacion_Click(object sender, EventArgs e)
        {
            if (txtPrecioProducto.Text == "")
            {
                MessageBox.Show("Es necesario agregar el precio del producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (DatosSourceFinal == 2)
                {
                    precioProducto = txtPrecioProducto.Text;
                }

                //Verifica que el formulario ya tenga una instancia creada, de lo contrario la crea
                if (FormDetalle != null)
                {
                    FormDetalle.txtBoxBase.Text = Convert.ToDouble(precioProducto).ToString("N2");
                    AgregarDetalleFacturacionProducto.ejecutarMetodos = true;
                    FormDetalle.typeOriginData = 2;
                    FormDetalle.UnidadMedida = claveUnidadMedida;
                    FormDetalle.ClaveProducto = claveProducto;
                    FormDetalle.Show();
                    FormDetalle.BringToFront();
                }
                else
                {
                    FormDetalle = new AgregarDetalleFacturacionProducto();
                    FormDetalle.typeOriginData = 2;
                    FormDetalle.UnidadMedida = claveUnidadMedida;
                    FormDetalle.ClaveProducto = claveProducto;
                    FormDetalle.limpiarCampos();
                    FormDetalle.ShowDialog();
                }
            }
        }

        private void txtPrecioProducto_KeyUp(object sender, KeyEventArgs e)
        {
            precioProducto = txtPrecioProducto.Text;
        }

        private void btnImagenes_Click(object sender, EventArgs e)
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
                            pictureBoxProducto.Image = Image.FromStream(File);      // Cargamos la imagen en el PictureBox
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
                        var source = txtNombreProducto.Text;
                        var replacement = source.Replace('/', '_').Replace('\\', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('\"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_').Replace('-', '_').Replace(' ', '_');
                        NvoFileName = replacement + ".jpg";
                        if (logoTipo != "")     // si Logotipo es diferente a ""
                        {
                            if (File1 != null)      // si el File1 es igual a null
                            {
                                File1.Dispose();    // liberamos el objeto File1
                                // hacemos la nueva cadena de consulta para hacer el UpDate
                                //string insertarImagen = $"UPDATE Productos SET ProdImage = '{saveDirectoryImg + NvoFileName}' WHERE ID = '{id}'";
                                //cn.EjecutarConsulta(insertarImagen);    // hacemos que se ejecute la consulta
                                if (pictureBoxProducto.Image != null)   // Verificamos si el pictureBox es null
                                {
                                    pictureBoxProducto.Image.Dispose();     // Liberamos el PictureBox para poder borrar su imagen
                                    System.IO.File.Delete(saveDirectoryImg + NvoFileName);  // borramos el archivo de la imagen
                                    // realizamos la copia de la imagen origen hacia el nuevo destino
                                    System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
                                    //logoTipo = saveDirectoryImg + NvoFileName;      // Obtenemos el nuevo Path
                                    logoTipo = NvoFileName;      // Obtenemos el nuevo Path
                                    // leemos el archivo de imagen y lo ponemos el pictureBox
                                    using (File = new FileStream(saveDirectoryImg + logoTipo, FileMode.Open, FileAccess.Read))
                                    {
                                        pictureBoxProducto.Image = Image.FromStream(File);      // cargamos la imagen en el PictureBox
                                    }
                                }
                                // hacemos la nueva cadena de consulta para hacer el update
                                //insertarImagen = $"UPDATE Productos SET ProdImage = '{logoTipo}' WHERE ID = '{id}'";
                                //cn.EjecutarConsulta(insertarImagen);    // hacemos que se ejecute la consulta
                            }
                            else    // si es que file1 es igual a null
                            {
                                // realizamos la copia de la imagen origen hacia el nuevo destino
                                System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
                                //logoTipo = saveDirectoryImg + NvoFileName;      // Obtenemos el nuevo Path
                                logoTipo = NvoFileName;      // Obtenemos el nuevo Path
                            }
                        }
                        if (logoTipo == "" || logoTipo == null)		// si el valor de la variable es Null o esta ""
                        {
                            pictureBoxProducto.Image.Dispose();	// Liberamos el pictureBox para poder borrar su imagen
                            // realizamos la copia de la imagen origen hacia el nuevo destino
                            System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
                            //logoTipo = saveDirectoryImg + NvoFileName;      // Obtenemos el nuevo Path
                            logoTipo = NvoFileName;      // Obtenemos el nuevo Path
                            // leemos el archivo de imagen y lo ponemos el pictureBox
                            using (File = new FileStream(saveDirectoryImg + logoTipo, FileMode.Open, FileAccess.Read))
                            {
                                pictureBoxProducto.Image = Image.FromStream(File);		// carrgamos la imagen en el PictureBox
                            }
                        }
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

        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            /****************************
			*	codigo de Alejandro		*
			****************************/
            string filtroTipoSerPaq = Convert.ToString(cbTipo.SelectedItem);
            var tipoServPaq = filtroTipoSerPaq;
            var nombre = txtNombreProducto.Text;
            var stock = txtStockProducto.Text;
            var precio = txtPrecioProducto.Text;
            var categoria = txtCategoriaProducto.Text;
            var claveIn = txtClaveProducto.Text.Trim();
            var codigoB = txtCodigoBarras.Text.Trim();
            var ProdServPaq = "P".ToString();
            var tipoDescuento = "0";
            var idUsrNvo = FormPrincipal.userID.ToString();
            var fechaCompra = DateTime.Now.ToString("yyyy-MM-dd");
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            /*	Fin del codigo de Alejandro	*/

            /************************************
            *   iniciamos las variables a 0     *
			*	codigo de Emmanuel				*
            ************************************/
            resultadoSearchNoIdentificacion = 0;    // ponemos los valores en 0
            resultadoSearchCodBar = 0;              // ponemos los valores en 0

            if (DatosSourceFinal == 3 || DatosSourceFinal == 1)
            {
                //Validar que el precio no sea menor al precio original del producto/servicio
                if (Convert.ToDouble(precio) < Convert.ToDouble(txtPrecioCompra.Text))
                {
                    MessageBox.Show("El precio no puede ser mayor al precio original", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                //Hacemos la busqueda que no se repita en CalveInterna
                //searchClavIntProd();
                if (mb.ComprobarCodigoClave(claveIn, FormPrincipal.userID))
                {
                    MessageBox.Show($"El número de identificación {claveIn}\nya se esta utilizando como clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
                //Hacemos la busqueda que no se repita en CodigoBarra
                //searchCodBar();
                if (mb.ComprobarCodigoClave(codigoB, FormPrincipal.userID))
                {
                    MessageBox.Show($"El número de identificación {codigoB}\nya se esta utilizando como clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                // recorrido para FlowLayoutPanel para ver cuantos TextBox
                foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
                {
                    // hacemos un objeto para ver que tipo control es
                    foreach (Control item in panel.Controls)
                    {
                        // ver si el control es TextBox
                        if (item is TextBox)
                        {
                            var tb = item.Text;         // almacenamos en la variable tb el texto de cada TextBox
                            codigosBarrras.Add(tb);     // almacenamos en el List los codigos de barras
                        }
                    }
                }

                //Verificamos que los codigos de barra extra no esten registrados
                if (codigosBarrras != null || codigosBarrras.Count != 0)
                {
                    // hacemos recorrido del List para gregarlos en los codigos de barras extras
                    for (int pos = 0; pos < codigosBarrras.Count; pos++)
                    {
                        var existe = mb.ComprobarCodigoClave(codigosBarrras[pos], FormPrincipal.userID);

                        if (existe)
                        {
                            MessageBox.Show($"El número de identificación {codigosBarrras[pos]}\nya se esta utilizando como clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }
                    }
                }

                if (resultadoSearchNoIdentificacion == 1 || resultadoSearchCodBar == 1)
                {
                    //MessageBox.Show("El número de identificación ya se esta utilizando\ncomo clave interna o código de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (resultadoSearchNoIdentificacion == 0 || resultadoSearchCodBar == 0)
                {
                    string[] guardar;
                    int respuesta=0;
                    /****************************
                    *	codigo de Alejandro		*
                    ****************************/
                    if (descuentos.Any())
                    {
                        //Cerramos la ventana donde se eligen los descuentos
                        FormAgregar.Close();
                        tipoDescuento = descuentos[0];
                    }
                    //if (tipoServPaq == "")
                    //{
                    //    cbTipo.Text = "Producto";
                    //    tipoServPaq = "Producto";
                    //}
                    if (this.Text == "Productos")
                    {
                        guardar = new string[] { nombre, stock, precio, categoria, claveIn, codigoB, claveProducto, claveUnidadMedida, tipoDescuento, idUsrNvo, logoTipo, ProdServPaq, baseProducto, ivaProducto, impuestoProducto };
                        //Se guardan los datos principales del producto
                        respuesta = cn.EjecutarConsulta(cs.GuardarProducto(guardar, FormPrincipal.userID));

                        if (respuesta > 0)
                        {
                            //Se obtiene la ID del último producto agregado
                            idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));

                            //Se realiza el proceso para guardar los detalles de facturación del producto
                            if (datosImpuestos != null)
                            {
                                //Cerramos la ventana donde se eligen los impuestos
                                FormDetalle.Close();

                                string[] listaImpuestos = datosImpuestos.Split('|');

                                int longitud = listaImpuestos.Length;

                                if (longitud > 0)
                                {
                                    for (int i = 0; i < longitud; i++)
                                    {
                                        string[] imp = listaImpuestos[i].Split(',');
                                        if (imp[3] == " - ") { imp[3] = "0"; }
                                        if (imp[4] == " - ") { imp[4] = "0"; }
                                        if (imp[5] == " - ") { imp[5] = "0"; }
                                        guardar = new string[] { imp[0], imp[1], imp[2], imp[3], imp[4], imp[5] };
                                        cn.EjecutarConsulta(cs.GuardarDetallesProducto(guardar, idProducto));
                                    }
                                }

                                datosImpuestos = null;
                            }

                            var idProveedor = string.Empty;

                            // Para guardar los detalles del producto
                            // Ejemplo: Proveedor, Categoria, Ubicacion, etc.
                            int contador = 0;
                            List<string> infoDetalle = new List<string>();

                            infoDetalle.Add(idProducto.ToString());
                            infoDetalle.Add(FormPrincipal.userID.ToString());

                            if (!string.IsNullOrWhiteSpace(infoProveedor))
                            {
                                var auxiliar = infoProveedor.Split('|');
                                var idProveedorTmp = auxiliar[0];
                                var nombreProveedor = auxiliar[1];

                                idProveedor = idProveedorTmp;
                                infoDetalle.Add(nombreProveedor);
                                infoDetalle.Add(idProveedor);
                                contador++;
                            }
                            else
                            {
                                infoDetalle.Add("");
                                infoDetalle.Add("0");
                            }

                            if (!string.IsNullOrWhiteSpace(infoCategoria))
                            {
                                var auxiliar = infoCategoria.Split('|');
                                var idCategoria = auxiliar[0];
                                var nombreCategoria = auxiliar[1];

                                infoDetalle.Add(nombreCategoria);
                                infoDetalle.Add(idCategoria);
                                contador++;
                            }
                            else
                            {
                                infoDetalle.Add("");
                                infoDetalle.Add("0");
                            }

                            if (!string.IsNullOrWhiteSpace(infoUbicacion))
                            {
                                var auxiliar = infoUbicacion.Split('|');
                                var idUbicacion = auxiliar[0];
                                var nombreUbicacion = auxiliar[1];

                                infoDetalle.Add(nombreUbicacion);
                                infoDetalle.Add(idUbicacion);
                                contador++;
                            }
                            else
                            {
                                infoDetalle.Add("");
                                infoDetalle.Add("0");
                            }

                            if (contador > 0)
                            {
                                guardar = infoDetalle.ToArray();
                                //guardar = new string[] { idProducto.ToString(), FormPrincipal.userID.ToString(), nombreProveedor, idProveedorTmp };

                                cn.EjecutarConsulta(cs.GuardarDetallesDelProducto(guardar));
                                
                                FormDetalleProducto.Close();
                            }

                            infoProveedor = string.Empty;
                            infoCategoria = string.Empty;
                            infoUbicacion = string.Empty;
                            // Fin del guardado de detalles del producto
                            

                            if (DatosSourceFinal == 1)
                            {
                                var conceptoProveedor = string.Empty;
                                var rfcProveedor = string.Empty;

                                // Datos para la tabla historial de compras al momento de registrar
                                // Un producto nuevo manualmente
                                if (idProveedor != "")
                                {
                                    var proveedorTmp = mb.ObtenerDatosProveedor(Convert.ToInt32(idProveedor), FormPrincipal.userID);
                                    conceptoProveedor = proveedorTmp[0];
                                    rfcProveedor = proveedorTmp[1];
                                }

                                guardar = new string[] { nombre, stock, precio, txtPrecioCompra.Text, fechaCompra, rfcProveedor, conceptoProveedor, "", "1", fechaOperacion, "", idProducto.ToString(), FormPrincipal.userID.ToString() };

                                cn.EjecutarConsulta(cs.AjustarProducto(guardar, 1));

                                int found = 10;
                                DateTime date1 = DateTime.Now;
                                string fechaCompleta = date1.ToString("s");
                                string Year = fechaCompleta.Substring(0, found);
                                string Date = fechaCompleta.Substring(found + 1);
                                string FechaRegistrada = Year + " " + Date;
                                string queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{FormPrincipal.userID}','{idProducto}','{FechaRegistrada}')";
                                cn.EjecutarConsulta(queryRecordHistorialProd);
                            }

                            if (DatosSourceFinal == 3)
                            {
                                int idHistorialCompraProducto = 0;
                                int found = 10;
                                string fechaXML = FechaXMLNvoProd;
                                string fecha = fechaXML.Substring(0, found);
                                string hora = fechaXML.Substring(found + 1);
                                string fechaCompleta = fecha + " " + hora;
                                string folio = FolioXMLNvoProd;
                                string RFCEmisor = RFCXMLNvoProd;
                                string nombreEmisor = NobEmisorXMLNvoProd;
                                string claveProdEmisor = ClaveProdEmisorXMLNvoProd;
                                string descuentoXML = DescuentoXMLNvoProd;
                                PrecioCompraXMLNvoProd = txtPrecioCompra.Text;

                                string query = $@"INSERT INTO HistorialCompras(Concepto, Cantidad, ValorUnitario, Descuento, Precio, FechaLarga, Folio, RFCEmisor, NomEmisor, ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto, IDUsuario) VALUES('{nombre}','{stock}','{precio}','{descuentoXML}','{PrecioCompraXMLNvoProd}','{fechaCompleta}','{folio.Trim()}','{RFCEmisor.Trim()}','{nombreEmisor.Trim()}','{claveProdEmisor.Trim()}',datetime('now', 'localtime'),'{Inventario.idReporte}','{idProducto}','{FormPrincipal.userID}')";

                                try
                                {
                                    cn.EjecutarConsulta(query);
                                    idHistorialCompraProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM HistorialCompras ORDER BY ID DESC LIMIT 1", 1));
                                    //MessageBox.Show("Registrado Intento 1", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error :" + ex);
                                }

                                DateTime date1 = DateTime.Now;
                                fechaCompleta = date1.ToString("s");
                                string Year = fechaCompleta.Substring(0, found);
                                string Date = fechaCompleta.Substring(found + 1);
                                string FechaRegistrada = Year + " " + Date;
                                string queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{FormPrincipal.userID}','{idHistorialCompraProducto}','{FechaRegistrada}')";
                                cn.EjecutarConsulta(queryRecordHistorialProd);
                            }

                            //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                            if (descuentos.Any())
                            {
                                //Descuento por Cliente
                                if (descuentos[0] == "1")
                                {
                                    guardar = new string[] { descuentos[1], descuentos[2], descuentos[3], descuentos[4] };

                                    cn.EjecutarConsulta(cs.GuardarDescuentoCliente(guardar, idProducto));
                                }
                                //Descuento por Mayoreo
                                if (descuentos[0] == "2")
                                {
                                    foreach (var descuento in descuentos)
                                    {
                                        if (descuento == "2") { continue; }

                                        string[] tmp = descuento.Split('-');

                                        cn.EjecutarConsulta(cs.GuardarDescuentoMayoreo(tmp, idProducto));
                                    }
                                }
                            }
                        }

                        // verificamos si el List esta con algun registro 
                        if (codigosBarrras != null || codigosBarrras.Count != 0)
                        {
                            // hacemos recorrido del List para gregarlos en los codigos de barras extras
                            for (int pos = 0; pos < codigosBarrras.Count; pos++)
                            {
                                // preparamos el Query
                                string insert = $"INSERT INTO CodigoBarrasExtras(CodigoBarraExtra, IDProducto)VALUES('{codigosBarrras[pos].Trim()}','{idProducto}')";
                                cn.EjecutarConsulta(insert);    // Realizamos el insert en la base de datos
                            }
                        }
                        codigosBarrras.Clear();
                        //Cierra la ventana donde se agregan los datos del producto
                        this.Close();
                    }
                    else if (this.Text == "Paquetes" || this.Text == "Servicios")
                    {
                        if (this.Text == "Servicios")
                        {
                            ProdServPaq = "S";
                        }
                        else if (this.Text == "Paquetes")
                        {
                            ProdServPaq = "PQ";
                        }
                        stock = "0";
                        guardar = new string[] { nombre, stock, precio, categoria, claveIn, codigoB, claveProducto, claveUnidadMedida, tipoDescuento, FormPrincipal.userID.ToString(), logoTipo, ProdServPaq, baseProducto, ivaProducto, impuestoProducto };
                        //Se guardan los datos principales del producto
                        respuesta = cn.EjecutarConsulta(cs.GuardarProducto(guardar, FormPrincipal.userID));
                        //Se obtiene la ID del último producto agregado
                        idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));
                        if (respuesta > 0)
                        {
                            var idProveedor = string.Empty;

                            // Para guardar los detalles del producto
                            // Ejemplo: Proveedor, Categoria, Ubicacion, etc.
                            int contador = 0;
                            List<string> infoDetalle = new List<string>();

                            infoDetalle.Add(idProducto.ToString());
                            infoDetalle.Add(FormPrincipal.userID.ToString());

                            if (!string.IsNullOrWhiteSpace(infoProveedor))
                            {
                                var auxiliar = infoProveedor.Split('|');
                                var idProveedorTmp = auxiliar[0];
                                var nombreProveedor = auxiliar[1];

                                idProveedor = idProveedorTmp;
                                infoDetalle.Add(nombreProveedor);
                                infoDetalle.Add(idProveedor);
                                contador++;
                            }
                            else
                            {
                                infoDetalle.Add("");
                                infoDetalle.Add("0");
                            }

                            if (!string.IsNullOrWhiteSpace(infoCategoria))
                            {
                                var auxiliar = infoCategoria.Split('|');
                                var idCategoria = auxiliar[0];
                                var nombreCategoria = auxiliar[1];

                                infoDetalle.Add(nombreCategoria);
                                infoDetalle.Add(idCategoria);
                                contador++;
                            }
                            else
                            {
                                infoDetalle.Add("");
                                infoDetalle.Add("0");
                            }

                            if (!string.IsNullOrWhiteSpace(infoUbicacion))
                            {
                                var auxiliar = infoUbicacion.Split('|');
                                var idUbicacion = auxiliar[0];
                                var nombreUbicacion = auxiliar[1];

                                infoDetalle.Add(nombreUbicacion);
                                infoDetalle.Add(idUbicacion);
                                contador++;
                            }
                            else
                            {
                                infoDetalle.Add("");
                                infoDetalle.Add("0");
                            }

                            if (contador > 0)
                            {
                                guardar = infoDetalle.ToArray();
                                //guardar = new string[] { idProducto.ToString(), FormPrincipal.userID.ToString(), nombreProveedor, idProveedorTmp };

                                cn.EjecutarConsulta(cs.GuardarDetallesDelProducto(guardar));

                                FormDetalleProducto.Close();
                            }

                            infoProveedor = string.Empty;
                            infoCategoria = string.Empty;
                            infoUbicacion = string.Empty;
                            // Fin del guardado de detalles del producto

                            if (DatosSourceFinal == 1)
                            {
                                var conceptoProveedor = string.Empty;
                                var rfcProveedor = string.Empty;

                                //Datos para la tabla historial de compras
                                if (idProveedor != "")
                                {
                                    var proveedorTmp = mb.ObtenerDatosProveedor(Convert.ToInt32(idProveedor), FormPrincipal.userID);
                                    conceptoProveedor = proveedorTmp[0];
                                    rfcProveedor = proveedorTmp[1];
                                }

                                guardar = new string[] { nombre, stock, precio, txtPrecioCompra.Text, fechaCompra, rfcProveedor, conceptoProveedor, "", "1", fechaOperacion, "", idProducto.ToString(), FormPrincipal.userID.ToString() };

                                cn.EjecutarConsulta(cs.AjustarProducto(guardar, 1));

                                int foundServicio = 10;
                                DateTime dateServ = DateTime.Now;
                                string fechaCompletaServ = dateServ.ToString("s");
                                string YearServ = fechaCompletaServ.Substring(0, foundServicio);
                                string DateServ = fechaCompletaServ.Substring(foundServicio + 1);
                                string FechaRegistradaServ = YearServ + " " + DateServ;
                                string queryRecordHistorialServ = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{FormPrincipal.userID}','{idProducto}','{FechaRegistradaServ}')";
                                cn.EjecutarConsulta(queryRecordHistorialServ);


                                int found = 10;
                                DateTime date1 = DateTime.Now;
                                string fechaCompleta = date1.ToString("s");
                                string Year = fechaCompleta.Substring(0, found);
                                string Date = fechaCompleta.Substring(found + 1);
                                string FechaRegistrada = Year + " " + Date;
                                string queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{FormPrincipal.userID}','{idProducto}','{FechaRegistrada}')";
                                cn.EjecutarConsulta(queryRecordHistorialProd);
                            }

                            if (DatosSourceFinal == 3)
                            {
                                int idHistorialCompraProducto = 0;
                                int found = 10;
                                string fechaXML = FechaXMLNvoProd;
                                string fecha = fechaXML.Substring(0, found);
                                string hora = fechaXML.Substring(found + 1);
                                string fechaCompleta = fecha + " " + hora;
                                string folio = FolioXMLNvoProd;
                                string RFCEmisor = RFCXMLNvoProd;
                                string nombreEmisor = NobEmisorXMLNvoProd;
                                string claveProdEmisor = ClaveProdEmisorXMLNvoProd;
                                string descuentoXML = DescuentoXMLNvoProd;
                                PrecioCompraXMLNvoProd = txtPrecioCompra.Text;

                                //Se obtiene la ID del último producto agregado
                                idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));

                                //string query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto,IDUsuario) VALUES('{nombre}','{stock}','{precioOriginalConIVA.ToString("N2")}','{descuentoXML}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}', datetime('now', 'localtime'), '{Inventario.idReporte}', '{idProducto}','{FormPrincipal.userID}')";

                                //string query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor,IDProducto,IDUsuario) VALUES('{nombre}','{stock}','{precioOriginalConIVA.ToString("N2")}','{descuentoXML}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}','{idProducto}','{FormPrincipal.userID}')";
                                string query = $@"INSERT INTO HistorialCompras(Concepto, Cantidad, ValorUnitario, Descuento, Precio, FechaLarga, Folio, RFCEmisor, NomEmisor, ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto, IDUsuario) 
                                VALUES('{nombre}','{stock}','{precio}','{descuentoXML}','{PrecioCompraXMLNvoProd}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}',datetime('now', 'localtime'),'{Inventario.idReporte}','{idProducto}','{FormPrincipal.userID}')";
                                try
                                {
                                    cn.EjecutarConsulta(query);
                                    idHistorialCompraProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM HistorialCompras ORDER BY ID DESC LIMIT 1", 1));
                                    //MessageBox.Show("Registrado Intento 1", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error :" + ex);
                                }

                                DateTime date1 = DateTime.Now;
                                fechaCompleta = date1.ToString("s");
                                string Year = fechaCompleta.Substring(0, found);
                                string Date = fechaCompleta.Substring(found + 1);
                                string FechaRegistrada = Year + " " + Date;
                                string queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{FormPrincipal.userID}','{idHistorialCompraProducto}','{FechaRegistrada}')";
                                cn.EjecutarConsulta(queryRecordHistorialProd);
                            }

                            if (ProductosDeServicios.Count >= 1 || ProductosDeServicios.Count == 0)
                            {
                                ProductosDeServicios.Clear();
                                // recorrido del panel de Prodcutos de Productos para ver cuantos Productos fueron seleccionados
                                foreach (Control panel in flowLayoutPanel2.Controls.OfType<FlowLayoutPanel>())
                                {
                                    // agregamos la variable para egregar los procutos
                                    string prodSerPaq = null;
                                    DataTable dtProductos;
                                    foreach (Control item in panel.Controls)
                                    {
                                        string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                        if (item is ComboBox)
                                        {
                                            if (item.Text != "Por favor selecciona un Producto")
                                            {
                                                string buscar = null;
                                                string comboBoxText = item.Text;
                                                string comboBoxValue = null;
                                                buscar = $"SELECT ID, Nombre FROM Productos WHERE Nombre = '{comboBoxText}' AND IDUsuario = '{FormPrincipal.userID}'";
                                                dtProductos = cn.CargarDatos(buscar);
                                                DataRow row = dtProductos.Rows[0];
                                                comboBoxValue = row["ID"].ToString();
                                                prodSerPaq += fech + "|";
                                                prodSerPaq += idProducto + "|";
                                                prodSerPaq += comboBoxValue + "|";
                                                prodSerPaq += comboBoxText + "|";
                                            }
                                        }
                                        if (item is TextBox)
                                        {
                                            var tb = item.Text;
                                            if (item.Text == "0")
                                            {
                                                tb = "0";
                                                prodSerPaq += tb;
                                            }
                                            else
                                            {
                                                prodSerPaq += tb;
                                            }
                                        }
                                    }
                                    ProductosDeServicios.Add(prodSerPaq);
                                    prodSerPaq = null;
                                }
                            }
                            //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                            if (ProductosDeServicios.Any())
                            {
                                foreach (var productosSP in ProductosDeServicios)
                                {
                                    string[] tmp = productosSP.Split('|');
                                    if (tmp.Length == 5)
                                    {
                                        cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                                    }
                                }
                                ProductosDeServicios.Clear();
                            }
                        }
                        if (respuesta > 0)
                        {
                            //Se obtiene la ID del último producto agregado
                            idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));
                            //Se realiza el proceso para guardar los detalles de facturación del producto
                            if (datosImpuestos != null)
                            {
                                //Cerramos la ventana donde se eligen los impuestos
                                FormDetalle.Close();
                                string[] listaImpuestos = datosImpuestos.Split('|');
                                int longitud = listaImpuestos.Length;
                                if (longitud > 0)
                                {
                                    for (int i = 0; i < longitud; i++)
                                    {
                                        string[] imp = listaImpuestos[i].Split(',');
                                        if (imp[3] == " - ") { imp[3] = "0"; }
                                        if (imp[4] == " - ") { imp[4] = "0"; }
                                        if (imp[5] == " - ") { imp[5] = "0"; }
                                        guardar = new string[] { imp[0], imp[1], imp[2], imp[3], imp[4], imp[5] };
                                        cn.EjecutarConsulta(cs.GuardarDetallesProducto(guardar, idProducto));
                                    }
                                }
                                datosImpuestos = null;
                            }
                            //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                            if (descuentos.Any())
                            {
                                //Descuento por Cliente
                                if (descuentos[0] == "1")
                                {
                                    guardar = new string[] { descuentos[1], descuentos[2], descuentos[3], descuentos[4] };

                                    cn.EjecutarConsulta(cs.GuardarDescuentoCliente(guardar, idProducto));
                                }
                                //Descuento por Mayoreo
                                if (descuentos[0] == "2")
                                {
                                    foreach (var descuento in descuentos)
                                    {
                                        if (descuento == "2") { continue; }

                                        string[] tmp = descuento.Split('-');

                                        cn.EjecutarConsulta(cs.GuardarDescuentoMayoreo(tmp, idProducto));
                                    }
                                }
                            }
                        }
                        // recorrido para FlowLayoutPanel para ver cuantos TextBox
                        foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
                        {
                            // hacemos un objeto para ver que tipo control es
                            foreach (Control item in panel.Controls)
                            {
                                // ver si el control es TextBox
                                if (item is TextBox)
                                {
                                    var tb = item.Text;         // almacenamos en la variable tb el texto de cada TextBox
                                    codigosBarrras.Add(tb);     // almacenamos en el List los codigos de barras
                                }
                            }
                        }
                        // verificamos si el List esta con algun registro 
                        if (codigosBarrras != null || codigosBarrras.Count != 0)
                        {
                            // hacemos recorrido del List para gregarlos en los codigos de barras extras
                            for (int pos = 0; pos < codigosBarrras.Count; pos++)
                            {
                                // preparamos el Query
                                string insert = $"INSERT INTO CodigoBarrasExtras(CodigoBarraExtra, IDProducto)VALUES('{codigosBarrras[pos].Trim()}','{idProducto}')";
                                cn.EjecutarConsulta(insert);    // Realizamos el insert en la base de datos
                            }
                        }
                        codigosBarrras.Clear();
                        if (ProductosDeServicios == null || ProductosDeServicios.Count == 0)
                        {
                            string queryBorrarProductosDeServicios = $"DELETE FROM ProductosDeServicios WHERE IDServicio = '{idProducto}'";
                            cn.EjecutarConsulta(queryBorrarProductosDeServicios);
                            string[] tmp = { $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}", $"{idProducto}", "", "", $"{txtCantPaqServ.Text}" };
                            cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                        } 
                        //Cierra la ventana donde se agregan los datos del producto
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error al intentar registrar el producto");
                    }
                    /*	Fin del codigo de Alejandro	*/
                }
            }
            else if (DatosSourceFinal == 2)
            {
                if (SearchProdResult.Rows.Count != 0)
                {
                    queryUpdateProd = $"UPDATE Productos SET Nombre = '{nombre}', Stock = '{stock}', Precio = '{precio}', Categoria = '{categoria}', ClaveInterna = '{claveIn}', CodigoBarras = '{codigoB}', ClaveProducto = '{claveProducto}', UnidadMedida = '{claveUnidadMedida}', ProdImage = '{logoTipo}'  WHERE ID = '{idProductoBuscado}'";
                    respuesta = cn.EjecutarConsulta(queryUpdateProd);
                    //label10.Text = idProductoBuscado;
                    if (SearchCodBarExtResult.Rows.Count != 0)
                    {
                        string deleteCodBarExt = $"DELETE FROM CodigoBarrasExtras WHERE IDProducto = '{idProductoBuscado}'";
                        cn.EjecutarConsulta(deleteCodBarExt);
                    }
                    // recorrido para FlowLayoutPanel2 para ver cuantos TextBox
                    if (ProductosDeServicios.Count >= 1 || ProductosDeServicios.Count == 0)
                    {
                        ProductosDeServicios.Clear();
                        // recorrido del panel de Prodcutos de Productos para ver cuantos Productos fueron seleccionados
                        foreach (Control panel in flowLayoutPanel2.Controls.OfType<FlowLayoutPanel>())
                        {
                            // agregamos la variable para egregar los procutos
                            string prodSerPaq = null;
                            DataTable dtProductos;
                            foreach (Control item in panel.Controls)
                            {
                                string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                if (item is ComboBox)
                                {
                                    if (item.Text != "Por favor selecciona un Producto")
                                    {
                                        string buscar = null;
                                        string comboBoxText = item.Text;
                                        string comboBoxValue = null;
                                        buscar = $"SELECT ID, Nombre FROM Productos WHERE Nombre = '{comboBoxText}' AND IDUsuario = '{FormPrincipal.userID}'";
                                        dtProductos = cn.CargarDatos(buscar);
                                        comboBoxValue = dtProductos.Rows[0]["ID"].ToString();
                                        prodSerPaq += fech + "|";
                                        prodSerPaq += idProductoBuscado + "|";
                                        prodSerPaq += comboBoxValue + "|";
                                        prodSerPaq += comboBoxText + "|";
                                    }
                                }
                                if (item is TextBox)
                                {
                                    var tb = item.Text;
                                    if (item.Text == "0")
                                    {
                                        tb = "0";
                                        prodSerPaq += tb;
                                    }
                                    else
                                    {
                                        prodSerPaq += tb;
                                    }
                                }
                            }
                            ProductosDeServicios.Add(prodSerPaq);
                            prodSerPaq = null;
                        }
                    }
                    //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                    if (ProductosDeServicios.Any())
                    {
                        string queryBorrarProductosDeServicios = $"DELETE FROM ProductosDeServicios WHERE IDServicio = '{idProductoBuscado}'";
                        cn.EjecutarConsulta(queryBorrarProductosDeServicios);
                        foreach (var productosSP in ProductosDeServicios)
                        {
                            string[] tmp = productosSP.Split('|');
                            if (tmp.Length == 5)
                            {
                                cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                            }
                        }
                        ProductosDeServicios.Clear();
                    }
                    //else
                    //{
                    //    string queryBorrarProductosDeServicios = $"DELETE FROM ProductosDeServicios WHERE IDServicio = '{idProductoBuscado}'";
                    //    cn.EjecutarConsulta(queryBorrarProductosDeServicios);
                    //    string[] tmp = { "datetime('now', 'localtime')", "'{idProductoBuscado}'", "", "" };
                    //    cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                    //}
                    // recorrido para FlowLayoutPanel para ver cuantos TextBox
                    foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
                    {
                        // hacemos un objeto para ver que tipo control es
                        foreach (Control item in panel.Controls)
                        {
                            // ver si el control es TextBox
                            if (item is TextBox)
                            {
                                var tb = item.Text;         // almacenamos en la variable tb el texto de cada TextBox
                                codigosBarrras.Add(tb);     // almacenamos en el List los codigos de barras
                            }
                        }
                    }
                    // verificamos si el List esta con algun registro 
                    if (codigosBarrras != null || codigosBarrras.Count != 0)
                    {
                        // hacemos recorrido del List para gregarlos en los codigos de barras extras
                        for (int pos = 0; pos < codigosBarrras.Count; pos++)
                        {
                            // preparamos el Query
                            string insert = $"INSERT INTO CodigoBarrasExtras(CodigoBarraExtra, IDProducto)VALUES('{codigosBarrras[pos].Trim()}','{idProductoBuscado}')";
                            cn.EjecutarConsulta(insert);    // Realizamos el insert en la base de datos
                        }
                    }
                    codigosBarrras.Clear();
                    //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                    if (descuentos.Any())
                    {
                        //Descuento por Cliente
                        if (descuentos[0] == "1")
                        {
                            string deleteDescuentoCLiente = $"DELETE FROM DescuentoCliente WHERE IDProducto = '{idProductoBuscado}'";
                            cn.EjecutarConsulta(deleteDescuentoCLiente);
                            string[] guardar = new string[] { descuentos[1], descuentos[2], descuentos[3], descuentos[4] };
                            cn.EjecutarConsulta(cs.GuardarDescuentoCliente(guardar, Convert.ToInt32(idProductoBuscado)));
                        }
                        //Descuento por Mayoreo
                        if (descuentos[0] == "2")
                        {
                            string deleteDescuentoMayoreo = $"DELETE FROM DescuentoMayoreo WHERE IDProducto = '{idProductoBuscado}'";
                            cn.EjecutarConsulta(deleteDescuentoMayoreo);
                            foreach (var descuento in descuentos)
                            {
                                if (descuento == "2") { continue; }

                                string[] tmp = descuento.Split('-');

                                cn.EjecutarConsulta(cs.GuardarDescuentoMayoreo(tmp, Convert.ToInt32(idProductoBuscado)));
                            }
                        }
                    }

                    // Para actualizar los detalles del producto
                    List<string> infoDetalle = new List<string>();

                    infoDetalle.Add(idProductoFinal.ToString());
                    infoDetalle.Add(FormPrincipal.userID.ToString());

                    if (!string.IsNullOrWhiteSpace(infoProveedor))
                    {
                        var auxiliar = infoProveedor.Split('|');
                        var idProveedorTmp = auxiliar[0];
                        var nombreProveedor = auxiliar[1];

                        infoDetalle.Add(nombreProveedor);
                        infoDetalle.Add(idProveedorTmp);
                    }
                    else
                    {
                        infoDetalle.Add("");
                        infoDetalle.Add("0");
                    }

                    if (!string.IsNullOrWhiteSpace(infoCategoria))
                    {
                        var auxiliar = infoCategoria.Split('|');
                        var idCategoria = auxiliar[0];
                        var nombreCategoria = auxiliar[1];

                        infoDetalle.Add(nombreCategoria);
                        infoDetalle.Add(idCategoria);
                    }
                    else
                    {
                        infoDetalle.Add("");
                        infoDetalle.Add("0");
                    }

                    if (!string.IsNullOrWhiteSpace(infoUbicacion))
                    {
                        var auxiliar = infoUbicacion.Split('|');
                        var idUbicacion = auxiliar[0];
                        var nombreUbicacion = auxiliar[1];

                        infoDetalle.Add(nombreUbicacion);
                        infoDetalle.Add(idUbicacion);
                    }
                    else
                    {
                        infoDetalle.Add("");
                        infoDetalle.Add("0");
                    }


                    string[] guardarDetalles = infoDetalle.ToArray();
                    //guardar = new string[] { idProducto.ToString(), FormPrincipal.userID.ToString(), nombreProveedor, idProveedorTmp };

                    cn.EjecutarConsulta(cs.GuardarDetallesDelProducto(guardarDetalles, 1));

                    if (Convert.ToInt32(idProductoFinal) > 0)
                    {
                        FormDetalleProducto.Close();
                    }

                    infoProveedor = string.Empty;
                    infoCategoria = string.Empty;
                    infoUbicacion = string.Empty;
                    idProductoFinal = string.Empty;
                    // Fin de actualizar detalles de producto

                    // Cierra la ventana donde se agregan los datos del producto
                    this.Close();
                }
            }
            else if (DatosSourceFinal == 4)
            {
                
                //MessageBox.Show("Proceso de registrar Nvo producto seleccionado del XML o Productos");
                //searchClavIntProd();                    // hacemos la busqueda que no se repita en CalveInterna
                //searchCodBar();                         // hacemos la busqueda que no se repita en CodigoBarra
                if (resultadoSearchNoIdentificacion == 1 || resultadoSearchCodBar == 1)
                {
                    //MessageBox.Show("El Número de Identificación; ya se esta utilizando en\ncomo clave interna ó codigo de barras de algun producto", "Error de Actualizar el Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                if (SearchProdResult.Rows.Count != 0 && resultadoSearchNoIdentificacion == 0)
                {
                    /****************************
			        *	codigo de Alejandro		*
			        ****************************/
                    var nombreNvoInsert = txtNombreProducto.Text;
                    var stockNvoInsert = txtStockProducto.Text;
                    var precioNvoInsert = txtPrecioProducto.Text;
                    var categoriaNvoInsert = txtCategoriaProducto.Text;
                    var claveInNvoInsert = txtClaveProducto.Text.Trim();
                    var codigoBNvoInsert = txtCodigoBarras.Text.Trim();
                    var tipoDescuentoNvoInsert = "0";
                    var idUsrNvoInsert = FormPrincipal.userID.ToString();
                    var tipoProdNvoInsert = "";
                    if (cbTipo.Text == "Producto")
                    {
                        tipoProdNvoInsert = "P";
                    }
                    else
                    {
                        tipoProdNvoInsert = "S";
                    }
                    /*	Fin del codigo de Alejandro	*/

                    //Hacemos la busqueda que no se repita en CalveInterna
                    //searchClavIntProd();
                    if (mb.ComprobarCodigoClave(claveInNvoInsert, FormPrincipal.userID))
                    {
                        MessageBox.Show($"El número de identificación {claveIn}\nya se esta utilizando como clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                    //Hacemos la busqueda que no se repita en CodigoBarra
                    //searchCodBar();
                    if (mb.ComprobarCodigoClave(codigoBNvoInsert, FormPrincipal.userID))
                    {
                        MessageBox.Show($"El número de identificación {codigoB}\nya se esta utilizando como clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                    // recorrido para FlowLayoutPanel para ver cuantos TextBox
                    foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
                    {
                        // hacemos un objeto para ver que tipo control es
                        foreach (Control item in panel.Controls)
                        {
                            // ver si el control es TextBox
                            if (item is TextBox)
                            {
                                var tb = item.Text;         // almacenamos en la variable tb el texto de cada TextBox
                                codigosBarrras.Add(tb);     // almacenamos en el List los codigos de barras
                            }
                        }
                    }

                    //Verificamos que los codigos de barra extra no esten registrados
                    if (codigosBarrras != null || codigosBarrras.Count != 0)
                    {
                        // hacemos recorrido del List para gregarlos en los codigos de barras extras
                        for (int pos = 0; pos < codigosBarrras.Count; pos++)
                        {
                            var existe = mb.ComprobarCodigoClave(codigosBarrras[pos], FormPrincipal.userID);

                            if (existe)
                            {
                                MessageBox.Show($"El número de identificación {codigosBarrras[pos]}\nya se esta utilizando\ncomo clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return;
                            }
                        }
                    }

                    string[] guardar = new string[] { nombreNvoInsert, stockNvoInsert, precioNvoInsert, categoriaNvoInsert, claveInNvoInsert, codigoBNvoInsert, claveProducto, claveUnidadMedida, tipoDescuentoNvoInsert, idUsrNvoInsert, logoTipo, tipoProdNvoInsert, baseProducto, ivaProducto, impuestoProducto };
                    //Se guardan los datos principales del producto
                    int respuesta = cn.EjecutarConsulta(cs.GuardarProducto(guardar, FormPrincipal.userID));
                    if (respuesta > 0)
                    {
                        //Se obtiene la ID del último producto agregado
                        idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));
                        //Se realiza el proceso para guardar los detalles de facturación del producto
                        if (datosImpuestos != null)
                        {
                            //Cerramos la ventana donde se eligen los impuestos
                            FormDetalle.Close();
                            string[] listaImpuestos = datosImpuestos.Split('|');
                            int longitud = listaImpuestos.Length;
                            if (longitud > 0)
                            {
                                for (int i = 0; i < longitud; i++)
                                {
                                    string[] imp = listaImpuestos[i].Split(',');
                                    if (imp[3] == " - ") { imp[3] = "0"; }
                                    if (imp[4] == " - ") { imp[4] = "0"; }
                                    if (imp[5] == " - ") { imp[5] = "0"; }
                                    guardar = new string[] { imp[0], imp[1], imp[2], imp[3], imp[4], imp[5] };
                                    cn.EjecutarConsulta(cs.GuardarDetallesProducto(guardar, idProducto));
                                }
                            }
                            datosImpuestos = null;
                        }
                        //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                        if (descuentos.Any())
                        {
                            //Descuento por Cliente
                            if (descuentos[0] == "1")
                            {
                                guardar = new string[] { descuentos[1], descuentos[2], descuentos[3], descuentos[4] };

                                cn.EjecutarConsulta(cs.GuardarDescuentoCliente(guardar, idProducto));
                            }
                            //Descuento por Mayoreo
                            if (descuentos[0] == "2")
                            {
                                foreach (var descuento in descuentos)
                                {
                                    if (descuento == "2") { continue; }

                                    string[] tmp = descuento.Split('-');

                                    cn.EjecutarConsulta(cs.GuardarDescuentoMayoreo(tmp, idProducto));
                                }
                            }
                        }

                        // verificamos si el List esta con algun registro 
                        if (codigosBarrras != null || codigosBarrras.Count != 0)
                        {
                            // hacemos recorrido del List para gregarlos en los codigos de barras extras
                            for (int pos = 0; pos < codigosBarrras.Count; pos++)
                            {
                                // preparamos el Query
                                string insert = $"INSERT INTO CodigoBarrasExtras(CodigoBarraExtra, IDProducto)VALUES('{codigosBarrras[pos].Trim()}','{idProducto}')";
                                cn.EjecutarConsulta(insert);    // Realizamos el insert en la base de datos
                            }
                        }
                        codigosBarrras.Clear();
                        // recorrido para FlowLayoutPanel2 para ver cuantos TextBox
                        if (ProductosDeServicios.Count >= 1 || ProductosDeServicios.Count == 0)
                        {
                            ProductosDeServicios.Clear();
                            // recorrido del panel de Prodcutos de Productos para ver cuantos Productos fueron seleccionados
                            foreach (Control panel in flowLayoutPanel2.Controls.OfType<FlowLayoutPanel>())
                            {
                                // agregamos la variable para egregar los procutos
                                string prodSerPaq = null;
                                DataTable dtProductos;
                                foreach (Control item in panel.Controls)
                                {
                                    string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    if (item is ComboBox)
                                    {
                                        if (item.Text != "Por favor selecciona un Producto")
                                        {
                                            string buscar = null;
                                            string comboBoxText = item.Text;
                                            string comboBoxValue = null;
                                            buscar = $"SELECT ID, Nombre FROM Productos WHERE Nombre = '{comboBoxText}' AND IDUsuario = '{FormPrincipal.userID}'";
                                            dtProductos = cn.CargarDatos(buscar);
                                            comboBoxValue = dtProductos.Rows[0]["ID"].ToString();
                                            prodSerPaq += fech + "|";
                                            prodSerPaq += idProducto + "|";
                                            prodSerPaq += comboBoxValue + "|";
                                            prodSerPaq += comboBoxText + "|";
                                        }
                                    }
                                    if (item is TextBox)
                                    {
                                        if (item.Text == "0")
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            var tb = item.Text;
                                            if (tb == "0")
                                            {
                                                tb = "0";
                                            }
                                            prodSerPaq += tb;
                                        }
                                    }
                                }
                                ProductosDeServicios.Add(prodSerPaq);
                                prodSerPaq = null;
                            }
                        }
                        //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                        if (ProductosDeServicios.Any())
                        {
                            foreach (var productosSP in ProductosDeServicios)
                            {
                                string[] tmp = productosSP.Split('|');
                                if (tmp.Length == 5)
                                {
                                    cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                                }
                            }
                            ProductosDeServicios.Clear();
                        }
                        //Cierra la ventana donde se agregan los datos del producto
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error al intentar registrar el producto");
                    }
                }
            }
            /* Fin del codigo de Emmanuel */
        }

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtro = Convert.ToString(cbTipo.SelectedItem);      // tomamos el valor que se elige en el TextBox
            if (filtro == "Producto")                            // comparamos si el valor a filtrar es Producto
            {
                if (PStock.Visible == false)
                {
                    PStock.Visible = true;
                }
                if (PPrecioOriginal.Visible == false)
                {
                    PPrecioOriginal.Visible = true;
                }
                if (PStock.Visible == true && PPrecioOriginal.Visible == true)
                {
                    lblTipoProdPaq.Text = "Producto";
                    btnAdd.Visible = false;
                    Hided = false;
                    ocultarPanel();
                    chkBoxConProductos.Checked = false;
                    chkBoxConProductos.Visible = false;
                }
            }
            else if (filtro == "Servicio / Paquete")                    // comparamos si el valor a filtrar es Servicio / Paquete ó Combo
            {
                if (PStock.Visible == true)
                {
                    PStock.Visible = false;
                }
                if (PPrecioOriginal.Visible == true)
                {
                    PPrecioOriginal.Visible = false;
                }
                if (PStock.Visible == false && PPrecioOriginal.Visible == false)
                {
                    lblTipoProdPaq.Text = "Servicio / Paquete";
                    btnAdd.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\angle-double-down.png");
                    Hided = false;
                    btnAdd.Visible = true;
                    btnAdd.PerformClick();
                    chkBoxConProductos.Checked = false;
                    chkBoxConProductos.Visible = true;
                }
            }
        }

        private void ocultarPanel()
        {
            if (Hided)
            {
                timerProdPaqSer.Start();
            }
            else
            {
                timerProdPaqSer.Start();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Hided)
            {
                ocultarPanel();
                btnAdd.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\angle-double-up.png");
            }
            else
            {
                ocultarPanel();
                btnAdd.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\angle-double-down.png");
            }
        }

        private void GenerarPanelProductosServ()
        {
            id = 0;
            flowLayoutPanel2.Controls.Clear();

            if (dtNvoProductosDeServicios != null)
            {
                foreach (DataRow dtRow in dtNvoProductosDeServicios.Rows)
                {
                    NombreProducto = dtRow["Nombre"].ToString();
                    IDProducto = dtRow["ID"].ToString();

                    FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                    panelHijo.Name = "panelGenerado" + id;
                    panelHijo.Width = 749;
                    panelHijo.Height = 50;
                    panelHijo.HorizontalScroll.Visible = false;

                    Label lb1 = new Label();
                    lb1.Name = "labelProductoGenerado" + id;
                    lb1.Width = 60;
                    lb1.Height = 17;
                    lb1.Text = "Producto:";

                    ComboBox cb = new ComboBox();
                    cb.Name = "comboBoxGenerador" + id;
                    cb.Width = 300;
                    cb.Height = 24;
                    try
                    {
                        foreach (var items in prodList)
                        {
                            cb.Items.Add(items.ToString());
                        }
                        cb.Text = NombreProducto;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error: " + ex.Message.ToString(), "error Text", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cb.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cb.BackColor = System.Drawing.SystemColors.Window;
                    cb.FormattingEnabled = true;
                    cb.Enter += new EventHandler(ComboBox_Enter);

                    Label lb2 = new Label();
                    lb2.Name = "labelCantidadGenerado" + id;
                    lb2.Width = 50;
                    lb2.Height = 17;
                    lb2.Text = "Cantidad:";

                    TextBox tb = new TextBox();
                    tb.Name = "textBoxGenerado" + id;
                    tb.Width = 250;
                    tb.Height = 22;
                    tb.Text = "0";
                    tb.Enter += new EventHandler(TextBoxProductosServ_Enter);
                    tb.KeyDown += new KeyEventHandler(TexBoxProductosServ_Keydown);

                    Button bt = new Button();
                    bt.Cursor = Cursors.Hand;
                    bt.Text = "X";
                    bt.Name = "btnGenerado" + id;
                    bt.Height = 23;
                    bt.Width = 23;
                    bt.BackColor = ColorTranslator.FromHtml("#C00000");
                    bt.ForeColor = ColorTranslator.FromHtml("white");
                    bt.FlatStyle = FlatStyle.Flat;
                    bt.TextAlign = ContentAlignment.MiddleCenter;
                    bt.Anchor = AnchorStyles.Top;
                    bt.Click += new EventHandler(ClickBotonesProductos);

                    panelHijo.Controls.Add(lb1);
                    panelHijo.Controls.Add(cb);
                    panelHijo.Controls.Add(lb2);
                    panelHijo.Controls.Add(tb);
                    panelHijo.Controls.Add(bt);
                    panelHijo.FlowDirection = FlowDirection.LeftToRight;

                    flowLayoutPanel2.Controls.Add(panelHijo);
                    flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;

                    tb.Focus();
                    id++;
                }
            }
            else if (dtProductosDeServicios == null)
            {
                FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                panelHijo.Name = "panelGenerado" + id;
                panelHijo.Width = 749;
                panelHijo.Height = 50;
                panelHijo.HorizontalScroll.Visible = false;

                Label lb1 = new Label();
                lb1.Name = "labelProductoGenerado" + id;
                lb1.Width = 69;
                lb1.Height = 17;
                lb1.Text = "Producto:";

                ComboBox cb = new ComboBox();
                cb.Name = "comboBoxGenerador" + id;
                cb.Width = 300;
                cb.Height = 24;
                try
                {
                    cb.DisplayMember = "Nombre";
                    cb.ValueMember = "ID";
                    cb.DataSource = prodList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Se produjo el siguiente error: CBProductos\n" + ex.Message.ToString(), "Error de aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
                cb.BackColor = System.Drawing.SystemColors.Window;
                cb.FormattingEnabled = true;

                Label lb2 = new Label();
                lb2.Name = "labelCantidadGenerado" + id;
                lb2.Width = 68;
                lb2.Height = 17;
                lb2.Text = "Cantidad:";

                TextBox tb = new TextBox();
                tb.Name = "textBoxGenerado" + id;
                tb.Width = 250;
                tb.Height = 22;
                tb.Text = "0";
                tb.Enter += new EventHandler(TextBoxProductos_Enter);
                tb.KeyDown += new KeyEventHandler(TexBoxProductos_Keydown);

                Button bt = new Button();
                bt.Cursor = Cursors.Hand;
                bt.Text = "X";
                bt.Name = "btnGenerado" + id;
                bt.Height = 23;
                bt.Width = 23;
                bt.BackColor = ColorTranslator.FromHtml("#C00000");
                bt.ForeColor = ColorTranslator.FromHtml("white");
                bt.FlatStyle = FlatStyle.Flat;
                bt.TextAlign = ContentAlignment.MiddleCenter;
                bt.Anchor = AnchorStyles.Top;
                bt.Click += new EventHandler(ClickBotonesProductos);

                panelHijo.Controls.Add(lb1);
                panelHijo.Controls.Add(cb);
                panelHijo.Controls.Add(lb2);
                panelHijo.Controls.Add(tb);
                panelHijo.Controls.Add(bt);
                panelHijo.FlowDirection = FlowDirection.LeftToRight;

                flowLayoutPanel2.Controls.Add(panelHijo);
                flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;

                tb.Focus();
                id++;
            }
            else if (dtProductosDeServicios != null)
            {
                foreach (DataRow dtRow in dtProductosDeServicios.Rows)
                {
                    NombreProducto = dtRow["NombreProducto"].ToString();
                    CantidadProducto = dtRow["Cantidad"].ToString();
                    IDProducto = dtRow["IDProducto"].ToString();

                    FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                    panelHijo.Name = "panelGenerado" + id;
                    panelHijo.Width = 749;
                    panelHijo.Height = 50;
                    panelHijo.HorizontalScroll.Visible = false;

                    Label lb1 = new Label();
                    lb1.Name = "labelProductoGenerado" + id;
                    lb1.Width = 60;
                    lb1.Height = 17;
                    lb1.Text = "Producto:";

                    ComboBox cb = new ComboBox();
                    cb.Name = "comboBoxGenerador" + id;
                    cb.Width = 300;
                    cb.Height = 24;
                    try
                    {
                        foreach (var items in prodList)
                        {
                            cb.Items.Add(items.ToString());
                        }
                        cb.Text = NombreProducto;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error: " + ex.Message.ToString(), "error Text", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cb.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cb.BackColor = System.Drawing.SystemColors.Window;
                    cb.FormattingEnabled = true;
                    cb.Enter += new EventHandler(ComboBox_Enter);

                    Label lb2 = new Label();
                    lb2.Name = "labelCantidadGenerado" + id;
                    lb2.Width = 50;
                    lb2.Height = 17;
                    lb2.Text = "Cantidad:";

                    TextBox tb = new TextBox();
                    tb.Name = "textBoxGenerado" + id;
                    tb.Width = 250;
                    tb.Height = 22;
                    tb.Text = CantidadProducto;
                    tb.Enter += new EventHandler(TextBoxProductosServ_Enter);
                    tb.KeyDown += new KeyEventHandler(TexBoxProductosServ_Keydown);

                    Button bt = new Button();
                    bt.Cursor = Cursors.Hand;
                    bt.Text = "X";
                    bt.Name = "btnGenerado" + id;
                    bt.Height = 23;
                    bt.Width = 23;
                    bt.BackColor = ColorTranslator.FromHtml("#C00000");
                    bt.ForeColor = ColorTranslator.FromHtml("white");
                    bt.FlatStyle = FlatStyle.Flat;
                    bt.TextAlign = ContentAlignment.MiddleCenter;
                    bt.Anchor = AnchorStyles.Top;
                    bt.Click += new EventHandler(ClickBotonesProductos);

                    panelHijo.Controls.Add(lb1);
                    panelHijo.Controls.Add(cb);
                    panelHijo.Controls.Add(lb2);
                    panelHijo.Controls.Add(tb);
                    panelHijo.Controls.Add(bt);
                    panelHijo.FlowDirection = FlowDirection.LeftToRight;

                    flowLayoutPanel2.Controls.Add(panelHijo);
                    flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;

                    tb.Focus();
                    id++;
                }
            }
        }

        private void GenerarPanelProductosServPlus()
        {
            id = flowLayoutPanel2.Controls.Count;
            //id++;
            //flowLayoutPanel2.Controls.Clear();

            FlowLayoutPanel panelHijo = new FlowLayoutPanel();
            Label lb1 = new Label();
            ComboBox cb = new ComboBox();
            Label lb2 = new Label();
            TextBox tb = new TextBox();
            Button bt = new Button();

            if (CBNombProd != "" || CBNombProd != null)
            {
                panelHijo.Name = "panelGenerado" + id;
                panelHijo.Width = 749;
                panelHijo.Height = 50;
                panelHijo.HorizontalScroll.Visible = false;

                lb1.Name = "labelProductoGenerado" + id;
                lb1.Width = 60;
                lb1.Height = 17;
                lb1.Text = "Producto:";

                cb.Name = "comboBoxGenerador" + id;
                cb.Width = 300;
                cb.Height = 24;
                try
                {
                    foreach (var items in prodList)
                    {
                        cb.Items.Add(items.ToString());
                    }
                    cb.Text = CBNombProd;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error: " + ex.Message.ToString(), "error Text", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
                cb.BackColor = System.Drawing.SystemColors.Window;
                cb.FormattingEnabled = true;
                cb.Enter += new EventHandler(ComboBox_Enter);

                lb2.Name = "labelCantidadGenerado" + id;
                lb2.Width = 50;
                lb2.Height = 17;
                lb2.Text = "Cantidad:";

                tb.Name = "textBoxGenerado" + id;
                tb.Width = 250;
                tb.Height = 22;
                tb.Text = "0";
                tb.Enter += new EventHandler(TextBoxProductosServ_Enter);
                tb.KeyDown += new KeyEventHandler(TexBoxProductosServ_Keydown);

                bt.Cursor = Cursors.Hand;
                bt.Text = "X";
                bt.Name = "btnGenerado" + id;
                bt.Height = 23;
                bt.Width = 23;
                bt.BackColor = ColorTranslator.FromHtml("#C00000");
                bt.ForeColor = ColorTranslator.FromHtml("white");
                bt.FlatStyle = FlatStyle.Flat;
                bt.TextAlign = ContentAlignment.MiddleCenter;
                bt.Anchor = AnchorStyles.Top;
                bt.Click += new EventHandler(ClickBotonesProductos);
                //id++;
            }
            else if(CBNombProd == "" || CBNombProd == null)
            {
                panelHijo.Name = "panelGenerado" + id;
                panelHijo.Width = 749;
                panelHijo.Height = 50;
                panelHijo.HorizontalScroll.Visible = false;
                
                lb1.Name = "labelProductoGenerado" + id;
                lb1.Width = 60;
                lb1.Height = 17;
                lb1.Text = "Producto:";
                //lb1.Location = new Point(10, 12);

                cb.Name = "comboBoxGenerador" + id;
                cb.Width = 300;
                cb.Height = 24;
                try
                {
                    cb.DisplayMember = "Nombre";
                    cb.ValueMember = "ID";
                    cb.DataSource = prodList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Se produjo el siguiente error: CBProductos\n" + ex.Message.ToString(), "Error de aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
                cb.BackColor = System.Drawing.SystemColors.Window;
                cb.FormattingEnabled = true;
                //cb.Location = new Point(75, 12);

                lb2.Name = "labelCantidadGenerado" + id;
                lb2.Width = 50;
                lb2.Height = 17;
                lb2.Text = "Cantidad:";
                //lb2.Location = new Point(385, 12);

                tb.Name = "textBoxGenerado" + id;
                tb.Width = 250;
                tb.Height = 22;
                tb.Text = "0";
                tb.Enter += new EventHandler(TextBoxProductosServ_Enter);
                tb.KeyDown += new KeyEventHandler(TexBoxProductosServ_Keydown);
                //tb.Location = new Point(440, 12);

                bt.Cursor = Cursors.Hand;
                bt.Text = "X";
                bt.Name = "btnGenerado" + id;
                bt.Height = 23;
                bt.Width = 23;
                bt.BackColor = ColorTranslator.FromHtml("#C00000");
                bt.ForeColor = ColorTranslator.FromHtml("white");
                bt.FlatStyle = FlatStyle.Flat;
                bt.TextAlign = ContentAlignment.MiddleCenter;
                bt.Anchor = AnchorStyles.Top;
                bt.Click += new EventHandler(ClickBotonesProductos);
                //id++;
                //bt.Location = new Point(695, 12);
            }
            
            panelHijo.Controls.Add(lb1);
            panelHijo.Controls.Add(cb);
            panelHijo.Controls.Add(lb2);
            panelHijo.Controls.Add(tb);
            panelHijo.Controls.Add(bt);
            panelHijo.FlowDirection = FlowDirection.LeftToRight;

            flowLayoutPanel2.Controls.Add(panelHijo);
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;

            tb.Focus();
            //id++;
        }

        private void ClickBotonesProductos(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            string nombreBoton = bt.Name;

            string idBoton = nombreBoton.Substring(11);
            string nombreTextBox = "textboxGenerado" + idBoton;
            string nombrePanel = "panelGenerado" + idBoton;

            foreach (Control item in flowLayoutPanel2.Controls.OfType<Control>())
            {
                if (item.Name == nombrePanel)
                {
                    flowLayoutPanel2.Controls.Remove(item);
                    flowLayoutPanel2.Controls.Remove(bt);
                }
            }
        }

        private void TexBoxProductos_Keydown(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (e.KeyCode == Keys.Enter)
            {
                string texto = tbx.Text;
                int cant = Convert.ToInt32(texto);
                if (cant > 0)
                {
                    GenerarPanelProductosServ();
                }
            }
        }

        private void TextBoxProductos_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;
        }

        private void TexBoxProductosServ_Keydown(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (e.KeyCode == Keys.Enter)
            {
                string texto = tbx.Text;
                int cant = Convert.ToInt32(texto);
                if (cant > 0)
                {
                    GenerarPanelProductosServPlus();
                }
            }
        }

        private void TextBoxProductosServ_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;
        }

        private void chkBoxConProductos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxConProductos.Checked == true)
            {
                ocultarVentanaProd();
            }
        }

        private void ocultarVentanaProd()
        {
            int activo = 0;
            nvoProductoAdd.FormClosed += delegate
            {
                CargarDatos();
                chkBoxConProductos.Checked = false;
                btnAdd.PerformClick();
                chkBoxConProductos.Visible = false;
            };

            if (!nvoProductoAdd.Visible)
            {
                pasarNumStockServicios();
                pasarDatos();
                nvoProductoAdd.ShowDialog();
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    string nombreForm = Application.OpenForms[i].ToString();
                    if (nombreForm.Contains("NvoProduct") != false)
                    {
                        MessageBox.Show("Esta Abierto");
                        activo = 1;
                        return;
                    }
                    else if (nombreForm.Contains("NvoProduct") == true)
                    {
                        MessageBox.Show("No Esta Abierto");
                        activo = 0;
                        return;
                    }
                }
                if (activo == 0)
                {
                    queryNvoProductosDeServicios = $"SELECT * FROM Productos WHERE Nombre = '{nvoProductoAdd.ProdNombre}'";
                    dtNvoProductosDeServicios = cn.CargarDatos(queryNvoProductosDeServicios);
                }
                else if ( activo == 1)
                {
                    queryNvoProductosDeServicios = $"SELECT * FROM Productos WHERE Nombre = '{nvoProductoAdd.ProdNombre}'";
                    dtNvoProductosDeServicios = cn.CargarDatos(queryNvoProductosDeServicios);
                }
            }
            else
            {
                pasarNumStockServicios();
                pasarDatos();
                nvoProductoAdd.ShowDialog();
            }
        }

        private void actualizarCBProd()
        {
            mostrarProdServPaq();
        }

        private void btnProdUpdate_Click(object sender, EventArgs e)
        {
            var nombre = txtNombreProducto.Text;
            var stock = txtStockProducto.Text;
            var precio = txtPrecioProducto.Text;
            var categoria = txtCategoriaProducto.Text;
            var claveIn = txtClaveProducto.Text.Trim();
            var codigoB = txtCodigoBarras.Text.Trim();

            string queryProdSearch, queryProdServFound, queryProdUpdate, queryProdServUpdate, fech, prodSerPaq, buscar, comboBoxText, comboBoxValue;

            DataTable dtProdFound, dtProdSerFound, rowProdUpdate, dtProductos;

            DataRow rowProdFound, row;

            queryProdSearch = $"SELECT * FROM Productos WHERE Nombre = '{nombre}' AND Precio = '{precio}'";
            dtProdFound = cn.CargarDatos(queryProdSearch);
            rowProdFound = dtProdFound.Rows[0];
            queryProdServFound = $"SELECT * FROM ProductosDeServicios WHERE IDServicio = '{rowProdFound["ID"].ToString()}'";
            idProducto = Convert.ToInt32(rowProdFound["ID"].ToString());
            dtProdSerFound = cn.CargarDatos(queryProdServFound);
            if (dtProdSerFound.Rows.Count >= 1)
            {
                queryProdUpdate = $"UPDATE Productos SET Nombre = '{nombre}', Precio = '{precio}', ClaveInterna = '{claveIn}', CodigoBarras = '{codigoB}' WHERE ID = '{rowProdFound["ID"].ToString()}'";
                rowProdUpdate = cn.CargarDatos(queryProdUpdate);
                ProductosDeServicios.Clear();
                // recorrido del panel de Prodcutos de Productos para ver cuantos Productos fueron seleccionados
                foreach (Control panel in flowLayoutPanel2.Controls.OfType<FlowLayoutPanel>())
                {
                    // agregamos la variable para egregar los procutos
                    prodSerPaq = null;
                    foreach (Control item in panel.Controls)
                    {
                        fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        if ((item is ComboBox) && (item.Text != "Por favor selecciona un Producto"))
                        {
                            buscar = null;
                            comboBoxText = item.Text;
                            comboBoxValue = null;
                            buscar = $"SELECT ID, Nombre FROM Productos WHERE Nombre = '{comboBoxText}' AND IDUsuario = '{FormPrincipal.userID}'";
                            dtProductos = cn.CargarDatos(buscar);
                            row = dtProductos.Rows[0];
                            comboBoxValue = row["ID"].ToString();
                            prodSerPaq += fech + "|";
                            prodSerPaq += idProducto + "|";
                            prodSerPaq += comboBoxValue + "|";
                            prodSerPaq += comboBoxText + "|";
                        }
                        if ((item is TextBox) && (item.Text != "0"))
                        {
                            var tb = item.Text;
                            prodSerPaq += tb;
                        }
                    }
                    ProductosDeServicios.Add(prodSerPaq);
                    prodSerPaq = null;
                    //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                    if (ProductosDeServicios.Any())
                    {
                        foreach (var productosSP in ProductosDeServicios)
                        {
                            string[] tmp = productosSP.Split('|');
                            cn.EjecutarConsulta(cs.ActualizarProductosServPaq(tmp));
                        }
                        ProductosDeServicios.Clear();
                    }
                }
                this.Close();
            }
            else if (dtProdSerFound.Rows.Count <= 0)
            {
                queryProdUpdate = $"UPDATE Productos SET Nombre = '{nombre}', Precio = '{precio}', ClaveInterna = '{claveIn}', CodigoBarras = '{codigoB}' WHERE ID = '{rowProdFound["ID"].ToString()}'";
                rowProdUpdate = cn.CargarDatos(queryProdUpdate);
                ProductosDeServicios.Clear();
                // recorrido del panel de Prodcutos de Productos para ver cuantos Productos fueron seleccionados
                foreach (Control panel in flowLayoutPanel2.Controls.OfType<FlowLayoutPanel>())
                {
                    // agregamos la variable para egregar los procutos
                    prodSerPaq = null;
                    foreach (Control item in panel.Controls)
                    {
                        fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        if ((item is ComboBox) && (item.Text != "Por favor selecciona un Producto"))
                        {
                            buscar = null;
                            comboBoxText = item.Text;
                            comboBoxValue = null;
                            buscar = $"SELECT ID, Nombre FROM Productos WHERE Nombre = '{comboBoxText}' AND IDUsuario = '{FormPrincipal.userID}'";
                            dtProductos = cn.CargarDatos(buscar);
                            row = dtProductos.Rows[0];
                            comboBoxValue = row["ID"].ToString();
                            prodSerPaq += fech + "|";
                            prodSerPaq += idProducto + "|";
                            prodSerPaq += comboBoxValue + "|";
                            prodSerPaq += comboBoxText + "|";
                        }
                        if ((item is TextBox) && (item.Text != "0"))
                        {
                            var tb = item.Text;
                            prodSerPaq += tb;
                        }
                    }
                    ProductosDeServicios.Add(prodSerPaq);
                    prodSerPaq = null;
                    //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                    if (ProductosDeServicios.Any())
                    {
                        foreach (var productosSP in ProductosDeServicios)
                        {
                            string[] tmp = productosSP.Split('|');
                            cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                        }
                        ProductosDeServicios.Clear();
                    }
                }
                this.Close();
            }
        }

        private void btnDetalleProducto_Click(object sender, EventArgs e)
        {
            FormDetalleProducto = Application.OpenForms.OfType<AgregarDetalleProducto>().FirstOrDefault();

            if (DatosSourceFinal == 1 || DatosSourceFinal == 3)
            {
                //Verifica que el formulario ya tenga una instancia creada, de lo contrario la crea
                if (FormDetalleProducto == null)
                {
                    FormDetalleProducto = new AgregarDetalleProducto();
                    //FormDetalleProducto.typeDatoProveedor = 1;
                    FormDetalleProducto.Show();
                    FormDetalleProducto.BringToFront();
                }
                else
                {
                    //FormDetalleProducto.typeDatoProveedor = 1;
                    FormDetalleProducto.Show();
                    FormDetalleProducto.BringToFront();
                }
            }
            if (DatosSourceFinal == 2)
            {

                //Verifica que el formulario ya tenga una instancia creada, de lo contrario la crea
                if (FormDetalleProducto == null)
                {
                    FormDetalleProducto = new AgregarDetalleProducto();
                    FormDetalleProducto.Show();
                    FormDetalleProducto.BringToFront();
                }
                else
                {
                    FormDetalleProducto.Show();
                    FormDetalleProducto.BringToFront();
                }
            }
        }

        private void txtPrecioCompra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                precioOriginalConIVA = (float)Convert.ToDouble(txtPrecioCompra.Text);
                PrecioRecomendado = precioOriginalConIVA * (float)1.60;
                txtPrecioProducto.Text = PrecioRecomendado.ToString("N2");
                txtPrecioProducto.Focus();
                txtPrecioProducto.Select(txtPrecioProducto.Text.Length, 0);
            }
        }

        private void txtPrecioCompra_Leave(object sender, EventArgs e)
        {
            precioOriginalConIVA = (float)Convert.ToDouble(txtPrecioCompra.Text);
            PrecioRecomendado = precioOriginalConIVA * (float)1.60;
            txtPrecioProducto.Text = PrecioRecomendado.ToString("N2");
            txtPrecioProducto.Focus();
            txtPrecioProducto.Select(txtPrecioProducto.Text.Length, 0);
        }

        private void txtCantPaqServ_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Para obligar a que sólo se introduzcan números
            //if (Char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else
            //{
            //    if (Char.IsControl(e.KeyChar))  // permitir teclas de control como retroceso
            //    {
            //        e.Handled = false;
            //    }
            //    else
            //    {
            //        // el resto de teclas pulsadas se desactivan
            //        e.Handled = true;
            //    }
            //}
            if (e.KeyChar == 8)     // tecla BackSpace
            {
                e.Handled = false;
                return;
            }
            bool IsDec = false;
            int nroDec = 0;
            for (int i = 0; i < txtCantPaqServ.Text.Length; i++)       // recorrer la caja de texto
            {
                if (txtCantPaqServ.Text[i] == '.')      // ver si es un punto decimal
                {
                    IsDec = true;
                }
                if (IsDec && nroDec++ >= 2)     // incrementar la variable nroDec
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57)     // teclas del 0 hasta el 9
            {
                e.Handled = false;
            }
            else if (e.KeyChar == 46)   // tecla punto decimal " . "
            {
                e.Handled = (IsDec) ? true : false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListaProductos ListStock = new ListaProductos();
            ListStock.nombreProducto += new ListaProductos.pasarProducto(ejecutar);
            ListStock.ShowDialog();
        }

        private void ejecutar(string dato)
        {
            CBNombProd = dato;
            GenerarPanelProductosServPlus();
        }

        private void pasarNumStockServicios()
        {
            CantidadPordServPaq.ShowDialog();
        }

        private void pasarDatos()
        {
            nvoProductoAdd.ProdNombre = txtNombreProducto.Text;
            nvoProductoAdd.ProdStock = CantProdServFinal;
            float price = (float)Convert.ToDouble(txtPrecioProducto.Text);
            nvoProductoAdd.ProdPrecio = price.ToString();
            nvoProductoAdd.ProdCategoria = "";
            nvoProductoAdd.ProdClaveInterna = "";
            nvoProductoAdd.ProdCodBarras = "";
            nvoProductoAdd.fechaProdServicioXML = FechaXMLNvoProd;
            nvoProductoAdd.FolioProdServicioXML = FolioXMLNvoProd;
            nvoProductoAdd.RFCProdServicioXML = RFCXMLNvoProd;
            nvoProductoAdd.NobEmisorProdServicioXML = NobEmisorXMLNvoProd;
            nvoProductoAdd.ClaveProdEmisorProdServicioXML = ClaveProdEmisorXMLNvoProd;
            nvoProductoAdd.UnidadMedidaProdServicioXML = claveUnidadMedida;
            nvoProductoAdd.DescuentoProdServicioXML = DescuentoXMLNvoProd;
        }

        private void txtCategoriaProducto_TextChanged(object sender, EventArgs e)
        {
            txtCategoriaProducto.CharacterCasing = CharacterCasing.Upper;
        }

        private void CargarDatos()
        {
            cargarCBProductos(idEditarProducto);
        }

        private void timerProdPaqSer_Tick(object sender, EventArgs e)
        {
            if (Hided)
            {
                if (fLPContenidoProducto.Height == 0)
                {
                    fLPContenidoProducto.Height = fLPContenidoProducto.Height + 123;
                }
                if (fLPContenidoProducto.Height >= PH)
                {
                    timerProdPaqSer.Stop();
                    Hided = false;
                    if (idProductoBuscado != null && tipoProdServ == "S")
                    {
                        mostrarProdServPaq();
                    }
                    else if (idProductoBuscado != null && tipoProdServ == "PQ")
                    {
                        mostrarProdServPaq();
                    }
                    else if (idProductoBuscado != null && dtNvoProductosDeServicios != null)
                    {
                        mostrarProdServPaq();
                    }
                    else if ((idProductoBuscado != null || tipoProdServ != null) && DatosSourceFinal == 3)
                    {
                        GenerarPanelProductosServ();
                    }
                    else if ((idProductoBuscado == null || tipoProdServ == null) && DatosSourceFinal == 3)
                    {
                        GenerarPanelProductosServ();
                    }
                    else if ((idProductoBuscado != null || tipoProdServ != null) && DatosSourceFinal == 1)
                    {
                        GenerarPanelProductosServ();
                    }
                    else if ((idProductoBuscado == null || tipoProdServ == null) && DatosSourceFinal == 1)
                    {
                        GenerarPanelProductosServ();
                    }
                    this.Height = 700;
                    this.CenterToScreen();
                    this.Refresh();
                }
            }
            else
            {
                fLPContenidoProducto.Height = fLPContenidoProducto.Height - 30;
                if (fLPContenidoProducto.Height <= 0)
                {
                    timerProdPaqSer.Stop();
                    Hided = true;
                    this.Height = 600;
                    this.CenterToScreen();
                    this.Refresh();
                }
            }
        }

        private void cbTipo_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;
        }

        private void txtClaveProducto_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;      // capturamos el ultimo control en el que estaba el Focus
        }

        private void txtCodigoBarras_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;      // capturamos el ultimo control en el que estaba el Focus
        }

        private void txtNombreProducto_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;      // capturamos el ultimo control en el que estaba el Focus
        }

        private void txtStockProducto_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;      // capturamos el ultimo control en el que estaba el Focus
        }

        private void txtPrecioProducto_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;      // capturamos el ultimo control en el que estaba el Focus
        }

        private void txtCategoriaProducto_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;      // capturamos el ultimo control en el que estaba el Focus
        }

        private void AgregarEditarProducto_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (FormDetalle.Visible == false)
            //{
            //    FormDetalleProducto.Close();
            //}
            LimpiarDatos();
        }

        private void AgregarEditarProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            LimpiarDatos();
        }

        private void AgregarEditarProducto_Load(object sender, EventArgs e)
        {
            seleccionListaStock = 0;
            string cadAux = string.Empty;
            fLPType.Visible = false;
            TituloForm = Titulo;

            PH = PConteidoProducto.Height;
            Hided = false;
            Hided1 = false;
            flowLayoutPanel2.Controls.Clear();
            DatosSourceFinal = DatosSource;
            
            if (DatosSourceFinal == 3)      // si el llamado de la ventana proviene del Archivo XML
            {
                cbTipo.SelectedIndex = 0;
                PCantidadPaqServ.Visible = false;
                fLPType.Visible = true;
                cadAux = TituloForm.Substring(8);   // extraemos que tipo es (Producto, Paquete, Servicio)
                cargarDatos();
            }

            if (DatosSourceFinal == 2)      // si el llamado de la ventana proviene del DataGridView (Ventana Productos)
            {
                txtStockProducto.Enabled = false;
                button1.Visible = true;
                cadAux = TituloForm.Substring(7);   // extraemos que tipo es (Producto, Paquete, Servicio)
            }

            if (DatosSourceFinal == 1)      // si el llamado de la ventana proviene del Boton Productos (Ventana Productos)
            {
                txtStockProducto.Enabled = true;
                cadAux = TituloForm.Substring(8);   // extraemos que tipo es (Producto, Paquete, Servicio)
                button1.Visible = false;
            }

            if (cadAux == "Producto")           // si es un Producto
            {
                this.Text = cadAux + "s";             // Ponemos el titulo del form en plural "Productos"
                if (!ProdNombre.Equals(""))
                {
                    cargarDatos();
                    ocultarPanel();
                    //cargarCBProductos();
                    PStock.Visible = true;
                    PCantidadPaqServ.Visible = false;
                }
                else if (ProdNombre.Equals(""))
                {
                    LimpiarCampos();
                    cargarDatosNvoProd();
                    cbTipo.Text = "Producto";
                    btnAdd.Visible = false;
                    ocultarPanel();
                    PStock.Visible = true;
                    PCantidadPaqServ.Visible = false;
                }
                lblTipoProdPaq.Text = "Nombre del Producto";
            }
            else if (cadAux == "Paquete")       // si es un Paquete
            {
                this.Text = cadAux + "s";            // Ponemos el titulo del form en plural "Paquetes"
                if (!ProdNombre.Equals(""))
                {
                    cargarDatos();
                    ocultarPanel();
                    cargarCBProductos(idEditarProducto);
                    PStock.Visible = false;
                    lblCantPaqServ.Text = "Cantidad por paquete";
                    PCantidadPaqServ.Visible = true;
                }
                else if (ProdNombre.Equals(""))
                {
                    LimpiarCampos();
                    cargarDatosNvoProd();
                    cbTipo.Text = "Producto";
                    btnAdd.Visible = false;
                    ocultarPanel();
                    PStock.Visible = false;
                    lblCantPaqServ.Text = "Cantidad por paquete";
                    PCantidadPaqServ.Visible = true;
                }
                lblTipoProdPaq.Text = "Nombre del Paquete";
            }
            else if (cadAux == "Servicio")      // si es un Servicio
            {
                this.Text = cadAux + "s";            // Ponemos el titulo del form en plural "Servicios"
                if (!ProdNombre.Equals(""))
                {
                    cargarDatos();
                    ocultarPanel();
                    cargarCBProductos(idEditarProducto);
                    PStock.Visible = false;
                    lblCantPaqServ.Text = "Cantidad por servicio";
                    PCantidadPaqServ.Visible = true;
                }
                else if (ProdNombre.Equals(""))
                {
                    LimpiarCampos();
                    cargarDatosNvoProd();
                    cbTipo.Text = "Producto";
                    btnAdd.Visible = false;
                    ocultarPanel();
                    PStock.Visible = false;
                    lblCantPaqServ.Text = "Cantidad por servicio";
                    PCantidadPaqServ.Visible = true;
                }
                lblTipoProdPaq.Text = "Nombre del Servicio";
            }
            tituloSeccion.Text = TituloForm;    // Ponemos el Text del label TituloSeccion
        }

        private void cargarCBProductos(string typePaqServ)
        {
            try
            {
                datosProductos = new DataTable();
                //queryProductos = $"SELECT ID, Nombre FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND Tipo = 'P'";
                queryProductos = $"SELECT ID, Nombre FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND Tipo = 'P'";
                datosProductos = cn.CargarDatos(queryProductos);
                row = datosProductos.NewRow();
                row["ID"] = -1;
                row["Nombre"] = "Por favor selecciona un Producto";
                datosProductos.Rows.InsertAt(row, 0);
                ProductoList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo el siguiente error: CBProductos\n" + ex.Message.ToString(), "Error de aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void ProductoList()
        {
            prodList = new List<ItemsProductoComboBox>();
            for (int i = 0; i < datosProductos.Rows.Count; i++)
            {
                ItemsProductoComboBox itemCBProd = new ItemsProductoComboBox();
                itemCBProd.Value = datosProductos.Rows[i]["Nombre"].ToString();
                itemCBProd.text = datosProductos.Rows[i]["ID"].ToString();
                prodList.Add(itemCBProd);
            }
        }
    }
}
