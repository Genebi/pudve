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
        static public string datosImpuestos = null;
        static public string claveProducto = null;
        static public string claveUnidadMedida = null;
        static public DataTable SearchDesCliente, SearchDesMayoreo;
        static public List<string> descuentos = new List<string>();

        List<string> prodServPaq = new List<string>();

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        AgregarDetalleFacturacionProducto FormDetalle;
        AgregarDescuentoProducto FormAgregar;
        public NvoProduct nvoProductoAdd = new NvoProduct();
        public CantidadProdServicio CantidadPordServPaq = new CantidadProdServicio();

        int idProducto;

        /****************************
		*   Codigo de Emmanuel      *
		****************************/
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

        static public int DatosSourceFinal = 0;
        static public string ProdNombreFinal = "";
        static public string ProdStockFinal = "";
        static public string ProdPrecioFinal = "";
        static public string ProdCategoriaFinal = "";
        static public string ProdClaveInternaFinal = "";
        static public string ProdCodBarrasFinal = "";
        static public string CantProdServFinal = "";

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

        // funsion para poder buscar en los productos 
        // si coincide con los campos de de ClaveInterna
        // respecto al stock del producto en su campo de NoIdentificacion
        public void searchClavIntProd()
        {
            // preparamos el Query
            string search = $"SELECT Prod.ID, Prod.Nombre, Prod.ClaveInterna, Prod.Stock, Prod.CodigoBarras, Prod.Precio FROM Productos Prod WHERE Prod.IDUsuario = '{FormPrincipal.userID}' AND Prod.ClaveInterna = '{txtClaveProducto.Text}' OR Prod.CodigoBarras = '{txtClaveProducto.Text}'";
            dtClaveInterna = cn.CargarDatos(search);    // alamcenamos el resultado de la busqueda en dtClaveInterna
            if (dtClaveInterna.Rows.Count > 0)  // si el resultado arroja al menos una fila
            {
                resultadoSearchNoIdentificacion = 1;    // busqueda positiva
                //MessageBox.Show("No Identificación Encontrado...\nen la claveInterna del Producto\nEsta siendo utilizada actualmente en el Stock", "El Producto no puede registrarse", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtClaveInterna.Rows.Count <= 0)    // si el resultado no arroja ninguna fila
            {
                resultadoSearchNoIdentificacion = 0; // busqueda negativa
                //MessageBox.Show("No Encontrado", "El Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // funsion para poder buscar en los productos 
        // si coincide con los campos de de CodigoBarras
        // respecto al stock del producto en su campo de NoIdentificacion
        public void searchCodBar()
        {
            // preparamos el Query
            string search = $"SELECT Prod.ID, Prod.Nombre, Prod.ClaveInterna, Prod.Stock, Prod.CodigoBarras, Prod.Precio FROM Productos Prod WHERE Prod.IDUsuario = '{FormPrincipal.userID}' AND Prod.CodigoBarras = '{txtCodigoBarras.Text}' OR Prod.ClaveInterna = '{txtCodigoBarras.Text}'";
            dtCodBar = cn.CargarDatos(search);  // alamcenamos el resultado de la busqueda en dtClaveInterna
            if (dtCodBar.Rows.Count > 0)        // si el resultado arroja al menos una fila
            {
                resultadoSearchCodBar = 1; // busqueda positiva
                //MessageBox.Show("No Identificación Encontrado...\nen el Código de Barras del Producto\nEsta siendo utilizada actualmente en el Stock", "El Producto no puede registrarse", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtCodBar.Rows.Count <= 0)  // si el resultado no arroja ninguna fila
            {
                resultadoSearchCodBar = 0; // busqueda negativa
                //MessageBox.Show("Codigo Bar Disponible", "Este Codigo libre", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            txtStockProducto.Text = "";
            txtPrecioProducto.Text = "";
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
            if (tipoProdServ == "S")
            {
                queryProductosDeServicios = $"SELECT * FROM ProductosDeServicios WHERE IDServicio = '{idProductoBuscado}'";
                dtProductosDeServicios = cn.CargarDatos(queryProductosDeServicios);
                cbTipo.Text = "Servicio / Paquete";
                btnAdd.Visible = true;
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

            id = 0;
            flowLayoutPanel2.Controls.Clear();

            Label Titulo = new Label();
            Titulo.Name = "Title";
            Titulo.Width = 400;
            Titulo.Height = 20;
            Titulo.Text = "Descripción de productos que contiene el paquete:";
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
            //if (dtNvoProductosDeServicios.Rows.Count > 0)
            //{
            //    foreach (DataRow dtRow in dtNvoProductosDeServicios.Rows)
            //    {
            //        NombreProducto = dtRow["Nombre"].ToString();
            //        IDProducto = dtRow["ID"].ToString();

            //        FlowLayoutPanel panelHijo = new FlowLayoutPanel();
            //        panelHijo.Name = "panelGenerado" + id;
            //        panelHijo.Width = 749;
            //        panelHijo.Height = 50;
            //        panelHijo.HorizontalScroll.Visible = false;

            //        Label lb1 = new Label();
            //        lb1.Name = "labelProductoGenerado" + id;
            //        lb1.Width = 60;
            //        lb1.Height = 17;
            //        lb1.Text = "Producto:";

            //        ComboBox cb = new ComboBox();
            //        cb.Name = "comboBoxGenerador" + id;
            //        cb.Width = 300;
            //        cb.Height = 24;
            //        try
            //        {
            //            foreach (var items in prodList)
            //            {
            //                cb.Items.Add(items.ToString());
            //            }
            //            cb.Text = NombreProducto;
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("error: " + ex.Message.ToString(), "error Text", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //        cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //        cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            //        cb.BackColor = System.Drawing.SystemColors.Window;
            //        cb.FormattingEnabled = true;
            //        cb.Enter += new EventHandler(ComboBox_Enter);

            //        Label lb2 = new Label();
            //        lb2.Name = "labelCantidadGenerado" + id;
            //        lb2.Width = 50;
            //        lb2.Height = 17;
            //        lb2.Text = "Cantidad:";

            //        TextBox tb = new TextBox();
            //        tb.Name = "textBoxGenerado" + id;
            //        tb.Width = 250;
            //        tb.Height = 22;
            //        tb.Text = "0";
            //        tb.Enter += new EventHandler(TextBoxProductosServ_Enter);
            //        tb.KeyDown += new KeyEventHandler(TexBoxProductosServ_Keydown);

            //        Button bt = new Button();
            //        bt.Cursor = Cursors.Hand;
            //        bt.Text = "X";
            //        bt.Name = "btnGenerado" + id;
            //        bt.Height = 23;
            //        bt.Width = 23;
            //        bt.BackColor = ColorTranslator.FromHtml("#C00000");
            //        bt.ForeColor = ColorTranslator.FromHtml("white");
            //        bt.FlatStyle = FlatStyle.Flat;
            //        bt.TextAlign = ContentAlignment.MiddleCenter;
            //        bt.Anchor = AnchorStyles.Top;
            //        bt.Click += new EventHandler(ClickBotonesProductos);

            //        panelHijo.Controls.Add(lb1);
            //        panelHijo.Controls.Add(cb);
            //        panelHijo.Controls.Add(lb2);
            //        panelHijo.Controls.Add(tb);
            //        panelHijo.Controls.Add(bt);
            //        panelHijo.FlowDirection = FlowDirection.LeftToRight;

            //        flowLayoutPanel2.Controls.Add(panelHijo);
            //        flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;

            //        tb.Focus();
            //        id++;
            //    }
            //}
        }

        private void ComboBox_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;
        }

        public void cargarDatos()
        {
            cbTipo.Text = "Producto";
            ProdNombreFinal = ProdNombre;
            ProdStockFinal = ProdStock;
            ProdPrecioFinal = ProdPrecio;
            ProdCategoriaFinal = ProdCategoria;
            ProdClaveInternaFinal = ProdClaveInterna;
            ProdCodBarrasFinal = ProdCodBarras;
            claveProducto = claveProductoxml;
            claveUnidadMedida = claveUnidadMedidaxml;

            txtNombreProducto.Text = ProdNombreFinal;
            txtStockProducto.Text = ProdStockFinal;
            txtPrecioProducto.Text = ProdPrecioFinal;
            txtCategoriaProducto.Text = ProdCategoriaFinal;
            txtClaveProducto.Text = ProdClaveInternaFinal;

            if (DatosSourceFinal == 2)
            {
                txtCodigoBarras.Text = ProdCodBarrasFinal;
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
                string texto = txtCodigoBarras.Text;

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
                    FormDetalle.Show();
                    FormDetalle.BringToFront();
                }
                else
                {
                    FormDetalle = new AgregarDetalleFacturacionProducto();
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
            var claveIn = txtClaveProducto.Text;
            var codigoB = txtCodigoBarras.Text;
            var ProdServPaq = "P".ToString();
            var tipoDescuento = "0";
            /*	Fin del codigo de Alejandro	*/

            /************************************
            *   iniciamos las variables a 0     *
			*	codigo de Emmanuel				*
            ************************************/
            resultadoSearchNoIdentificacion = 0;    // ponemos los valores en 0
            resultadoSearchCodBar = 0;              // ponemos los valores en 0
            if (DatosSourceFinal == 3 || DatosSourceFinal == 1)
            {
                //MessageBox.Show("Proceso de registrar Nvo producto seleccionado del XML o Productos");
                searchClavIntProd();                    // hacemos la busqueda que no se repita en CalveInterna
                searchCodBar();                         // hacemos la busqueda que no se repita en CodigoBarra
                if (resultadoSearchNoIdentificacion == 1 && resultadoSearchCodBar == 1)
                {
                    MessageBox.Show("El Número de Identificación; ya se esta utilizando en\ncomo clave interna ó codigo de barras de algun producto", "Error de Actualizar el Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                    if (tipoServPaq == "")
                    {
                        cbTipo.Text = "Producto";
                        tipoServPaq = "Producto";
                    }
                    else if (tipoServPaq == "Producto")
                    {
                        guardar = new string[] { nombre, stock, precio, categoria, claveIn, codigoB, claveProducto, claveUnidadMedida, tipoDescuento, logoTipo, ProdServPaq };
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
                                string insert = $"INSERT INTO CodigoBarrasExtras(CodigoBarraExtra, IDProducto)VALUES('{codigosBarrras[pos]}','{idProducto}')";
                                cn.EjecutarConsulta(insert);    // Realizamos el insert en la base de datos
                            }
                        }
                        codigosBarrras.Clear();
                        //Cierra la ventana donde se agregan los datos del producto
                        this.Close();
                    }
                    else if (tipoServPaq == "Servicio / Paquete")
                    {
                        ProdServPaq = "S";
                        stock = "";
                        guardar = new string[] { nombre, stock, precio, categoria, claveIn, codigoB, claveProducto, claveUnidadMedida, tipoDescuento, logoTipo, ProdServPaq };
                        //Se guardan los datos principales del producto
                        respuesta = cn.EjecutarConsulta(cs.GuardarProducto(guardar, FormPrincipal.userID));
                        //Se obtiene la ID del último producto agregado
                        idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));
                        if (respuesta > 0)
                        {
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
                                            
                                            if (item.Text == "Por favor selecciona un Producto")
                                            {
                                                prodSerPaq += fech + "|";
                                                prodSerPaq += idProducto + "|";
                                                prodSerPaq += idProducto + "|";
                                                prodSerPaq += txtNombreProducto.Text + "|";
                                                prodSerPaq += "0";
                                                break;
                                            }
                                            else
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
                                    cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
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
                                string insert = $"INSERT INTO CodigoBarrasExtras(CodigoBarraExtra, IDProducto)VALUES('{codigosBarrras[pos]}','{idProducto}')";
                                cn.EjecutarConsulta(insert);    // Realizamos el insert en la base de datos
                            }
                        }
                        codigosBarrras.Clear();
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

                                    if (item.Text == "Por favor selecciona un Producto")
                                    {
                                        prodSerPaq += fech + "|";
                                        prodSerPaq += idProductoBuscado + "|";
                                        prodSerPaq += idProducto + "|";
                                        prodSerPaq += txtNombreProducto.Text + "|";
                                        prodSerPaq += "0";
                                        break;
                                    }
                                    else
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
                            cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                        }
                        ProductosDeServicios.Clear();
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
                            string insert = $"INSERT INTO CodigoBarrasExtras(CodigoBarraExtra, IDProducto)VALUES('{codigosBarrras[pos]}','{idProductoBuscado}')";
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
                    //Cierra la ventana donde se agregan los datos del producto
                    this.Close();
                }
            }
            else if (DatosSourceFinal == 4)
            {
                //MessageBox.Show("Proceso de registrar Nvo producto seleccionado del XML o Productos");
                searchClavIntProd();                    // hacemos la busqueda que no se repita en CalveInterna
                searchCodBar();                         // hacemos la busqueda que no se repita en CodigoBarra
                if (resultadoSearchNoIdentificacion == 1 && resultadoSearchCodBar == 1)
                {
                    MessageBox.Show("El Número de Identificación; ya se esta utilizando en\ncomo clave interna ó codigo de barras de algun producto", "Error de Actualizar el Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                    var claveInNvoInsert = txtClaveProducto.Text;
                    var codigoBNvoInsert = txtCodigoBarras.Text;
                    var tipoDescuentoNvoInsert = "0";
                    /*	Fin del codigo de Alejandro	*/

                    string[] guardar = new string[] { nombreNvoInsert, stockNvoInsert, precioNvoInsert, categoriaNvoInsert, claveInNvoInsert, codigoBNvoInsert, claveProducto, claveUnidadMedida, tipoDescuentoNvoInsert, logoTipo };
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
                                string insert = $"INSERT INTO CodigoBarrasExtras(CodigoBarraExtra, IDProducto)VALUES('{codigosBarrras[pos]}','{idProducto}')";
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

                                        if (item.Text == "Por favor selecciona un Producto")
                                        {
                                            prodSerPaq += fech + "|";
                                            prodSerPaq += idProducto + "|";
                                            prodSerPaq += idProducto + "|";
                                            prodSerPaq += txtNombreProducto.Text + "|";
                                            prodSerPaq += "0";
                                            break;
                                        }
                                        else
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
                                cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
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
                    btnAdd.Visible = true;
                    btnAdd.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\angle-double-down.png");
                    Hided = false;
                    ocultarPanel();
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
                btnAdd.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\angle-double-up.png");
            }
            else
            {
                ocultarPanel();
                btnAdd.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\angle-double-down.png");
            }
        }

        private void cambiarCBText()
        {
            //id = 0;
            //string comboBoxNombre = "comboBoxGenerador" + id;

            //foreach (DataRow dtRow in dtProductosDeServicios.Rows)
            //{
            //    NombreProducto = dtRow["NombreProducto"].ToString();
            //    CantidadProducto = dtRow["Cantidad"].ToString();
            //    IDProducto = dtRow["IDProducto"].ToString();
            //    foreach (Control cComprobar in this.Controls)
            //    {
            //        if (cComprobar is ComboBox)
            //        {
            //            if (cComprobar.Name == comboBoxNombre)
            //            {
            //                cComprobar.Text = NombreProducto;
            //                id++;
            //            }
            //        }
            //    }
            //}
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
            id = 0;
            id = flowLayoutPanel2.Controls.Count;
            id++;
            //flowLayoutPanel2.Controls.Clear();

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
            //lb1.Location = new Point(10, 12);

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
            //cb.Location = new Point(75, 12);

            Label lb2 = new Label();
            lb2.Name = "labelCantidadGenerado" + id;
            lb2.Width = 50;
            lb2.Height = 17;
            lb2.Text = "Cantidad:";
            //lb2.Location = new Point(385, 12);

            TextBox tb = new TextBox();
            tb.Name = "textBoxGenerado" + id;
            tb.Width = 250;
            tb.Height = 22;
            tb.Text = "0";
            tb.Enter += new EventHandler(TextBoxProductosServ_Enter);
            tb.KeyDown += new KeyEventHandler(TexBoxProductosServ_Keydown);
            //tb.Location = new Point(440, 12);

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
            //bt.Location = new Point(695, 12);

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
        }

        private void txtCategoriaProducto_TextChanged(object sender, EventArgs e)
        {
            txtCategoriaProducto.CharacterCasing = CharacterCasing.Upper;
        }

        private void CargarDatos()
        {
            cargarCBProductos();
        }

        private void timerProdPaqSer_Tick(object sender, EventArgs e)
        {
            if (Hided)
            {
                PConteidoProducto.Height = PConteidoProducto.Height + 30;
                if (PConteidoProducto.Height >= PH)
                {
                    timerProdPaqSer.Stop();
                    Hided = false;
                    if (idProductoBuscado != null && tipoProdServ == "S")
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
                    this.Height = 800;
                    this.CenterToScreen();
                    this.Refresh();
                }
            }
            else
            {
                PConteidoProducto.Height = PConteidoProducto.Height - 30;
                if (PConteidoProducto.Height <= 0)
                {
                    timerProdPaqSer.Stop();
                    Hided = true;
                    this.Height = 615;
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
            LimpiarDatos();
        }

        private void AgregarEditarProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            LimpiarDatos();
        }

        private void AgregarEditarProducto_Load(object sender, EventArgs e)
        {
            PH = PConteidoProducto.Height;
            Hided = false;
            Hided1 = false;
            flowLayoutPanel2.Controls.Clear();
            DatosSourceFinal = DatosSource;

            if (ProdNombre.Equals(""))
            {
                LimpiarCampos();
                cbTipo.Text = "Producto";
                btnAdd.Visible = false;
                chkBoxConProductos.Visible = false;
                ocultarPanel();
                
            }
            else if (!ProdNombre.Equals(""))
            {
                cargarDatos();
                //btnAdd.Visible = true;
                ocultarPanel();
                cargarCBProductos();
            }
        }

        private void cargarCBProductos()
        {
            try
            {
                datosProductos = new DataTable();
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
