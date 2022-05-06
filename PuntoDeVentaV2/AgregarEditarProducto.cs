using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

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
        static public string stockNecesario = string.Empty;
        static public string stockMinimo = string.Empty;
        static public string typeOfProduct = string.Empty;


        static public DataTable SearchDesCliente, SearchDesMayoreo;
        static public List<string> descuentos = new List<string>();
        static public List<string> detalleProductoBasico = new List<string>();
        static public List<string> detalleProductoGeneral = new List<string>();


        //Miooooooooooo
         public string titulo;
         public string texto;
         public string SeleccionaImagen;


        List<string> prodServPaq = new List<string>();

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();
        MetodosGenerales mg = new MetodosGenerales();

        AgregarDetalleFacturacionProducto FormDetalle;
        AgregarDescuentoProducto FormAgregar;
        AgregarDetalleProducto FormDetalleProducto;

        public NvoProduct nvoProductoAdd = new NvoProduct();
        public CantidadProdServicio CantidadPordServPaq = new CantidadProdServicio();

        int idProducto;
        public int idProductoCambio { get; set; }
        public bool cambioProducto { get; set; }

        //Variables para calculadora
        int calcu = 0;
        ///////////////////////////

        /****************************
		*   Codigo de Emmanuel      *
		****************************/

        #region Variables Globales

        List<string> infoDetalle,
                        infoDetailProdGral;

        Dictionary<string, string> proveedores,
                                    categorias,
                                    ubicaciones,
                                    detallesGral;

        Dictionary<int, Tuple<string, string, string, string>> diccionarioDetallesGeneral = new Dictionary<int, Tuple<string, string, string, string>>(),
                                                               diccionarioDetalleBasicos = new Dictionary<int, Tuple<string, string, string, string>>();

        string[] datosProveedor,
                    datosCategoria,
                    datosUbicacion,
                    datosDetalleGral,
                    separadas;

        string[] listaProveedores = new string[] { },
                    listaCategorias = new string[] { },
                    listaUbicaciones = new string[] { },
                    listaDetalleGral = new string[] { };

        int XPos = 0,
            YPos = 0,
            contadorIndex = 0,
            idProveedor = 0,
            idCategoria = 0,
            idUbicacion = 0,
            idProductoDetalleGral = 0;

        string nvoDetalle = string.Empty,
                nvoValor = string.Empty,
                editValor = string.Empty,
                deleteDetalle = string.Empty,
                nombreProveedor = string.Empty,
                nombreCategoria = string.Empty,
                nombreUbicacion = string.Empty,
                nombreDetalleGral = string.Empty,
                idReporte = string.Empty;

        public string getIdProducto { get; set; }

        public static string finalIdProducto = string.Empty;

        string editDetelle = string.Empty,
                editDetalleNvo = string.Empty;

        #endregion Variables Globales

        bool habilitarComboBoxes = false;

        string TituloForm = string.Empty;

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
        public string idProveedorXML { get; set; }
        public string nameProveedorXML { get; set; }

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


        DataTable SearchProdResult,
                    SearchCodBarExtResult,
                    datosProductos;

        OpenFileDialog f;       // declaramos el objeto de OpenFileDialog

        // objeto para el manejo de las imagenes
        FileStream File,
                    File1;

        FileInfo info;

        string queryBuscarProd,
                idProductoBuscado,
                queryUpdateProd,
                queryInsertProd,
                queryBuscarCodBarExt,
                queryBuscarDescuentoCliente,
                queryDesMayoreo,
                queryProductosDeServicios,
                queryNvoProductosDeServicios;

        DataTable dtProductosDeServicios,
                    dtNvoProductosDeServicios = null;

        // direccion de la carpeta donde se va poner las imagenes
        string saveDirectoryImg = string.Empty;

        string fileName,           // nombre de archivo
                oldDirectory,       // directorio origen de la imagen
                fileSavePath,       // directorio para guardar el archivo
                NvoFileName,        // Nuevo nombre del archivo
                logoTipo = "",      // Path de la Imagen de Logotipo
                tipoProdServ,       // Que tipo de Servicio es
                queryProductos;     // Query para busquedas de Productos

        DataRow row, rowNvoProd;

        Control _lastEnteredControl;    // para saber cual fue el ultimo control con el cursor activo

        // variable de text para poder dirigirnos a la carpeta principal para
        // poder jalar las imagenes o cualquier cosa que tengamos hay en ese directorio
        //public string rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

        // directorio donde esta el archivo de numero de codigo de barras consecutivo
        const string fichero = @"\PUDVE\settings\codbar\setupCodBar.txt";
        string Contenido;   // para obtener el numero que tiene el codigo de barras en el arhivo

        long CodigoDeBarras;    // variable entera para llevar un consecutivo de codigo de barras

        List<string> codigosBarrras = new List<string>();   // para agregar los datos extras de codigos de barras

        public DataTable dtClaveInterna;    // almacena el resultado de la funcion de CargarDatos de la funcion searchClavIntProd
        public DataTable dtCodBar;  // almacena el resultado de la funcion de CargarDatos de la funcion searchCodBar

        int resultadoSearchNoIdentificacion;    // sirve para ver si el producto existe en los campos CodigoBarras y ClaveInterna en la funcion searchClavIntProd()
        int resultadoSearchCodBar;  // sirve para ver si el producto existe en los campos CodigoBarras y ClaveInterna en la funcion searchCodBar()
        int resultadoSearchCodBarExtra; // sirve para ver si el producto existe en Código de Barra extra en la funcion productoRegistradoCodigoBarrasExtra()

        string filtro;

        int PH;
        bool Hided, Hided1;

        public static List<string> listaProductoToCombo = new List<string>(); // para agregar los combos o servicios a un producto
        public static List<string> ProductosDeServicios = new List<string>(); // para agregar los productos del servicio o paquete
        List<ItemsProductoComboBox> prodList;

        int numCombo = 1, indexItem = 1, totCB = 0;

        string comboBoxNombre;

        string NombreProducto = "";
        string CantidadProducto = "";
        string IDProducto = "";


        public string CBNombProdPasado { get; set; }
        static public string CBNombProd = string.Empty;
        static public string CBIdProd = string.Empty;
        static public int seleccionListaStock;

        public static bool ejecutarMetodos = false;
        private object cbProveedor_SelectValueChanged;

        private float porcentajeGanancia = 1.60f;

        public static string tmp_clave_interna = "";   // Almacenará la clave que se guarde en el producto. El valor será usado en AgregarStockXML una vez la ventana sea cerrada. Miri.  
        public static string tmp_codigo_barras = "";   // Almacenará el código que se guarde en el producto. El valor será usado en AgregarStockXML una vez la ventana sea cerrada. Miri. 

        public static string nombreProductoEditar;

        #region Iniciar Varaibles Globales
        List<string> datosProductosBtnGuardar,
                     datosProductoRelacionado;

        string[] guardar,
                 saveDetailProd = new string[] { "", "", "", "", "" },
                 idProveedorBtnGuardar;

        int respuesta = 0,
                idHistorialCompraProducto = 0,
                found = 10;

        bool foundClaveInterna, foundCodigoBarras;

        double valorDePrecioVenta;

        DateTime thisDay = DateTime.Today;
        DataTable dtServiciosPaquetes;
        DataRow rowServPaq;

        string origen = string.Empty,
                fechaXML = string.Empty,
                fecha = string.Empty,
                hora = string.Empty,
                fechaCompleta = string.Empty,
                folio = string.Empty,
                RFCEmisor = string.Empty,
                nombreEmisor = string.Empty,
                claveProdEmisor = string.Empty,
                descuentoXML = string.Empty,
                cantidadProdAtService = string.Empty;

        string filtroTipoSerPaq = string.Empty,
                tipoServPaq = string.Empty,
                nombre = string.Empty,
                parteEntera = string.Empty,
                parteDecimal = string.Empty,
                stock = string.Empty,
                precio = string.Empty,
                categoria = string.Empty,
                claveIn = string.Empty,
                codigoB = string.Empty,
                ProdServPaq = string.Empty,
                tipoDescuento = string.Empty,
                idUsrNvo = string.Empty,
                fechaCompra = string.Empty,
                fechaOperacion = string.Empty;

        string nombreNvoInsert = string.Empty,
                stockNvoInsert = string.Empty,
                precioNvoInsert = string.Empty,
                categoriaNvoInsert = string.Empty,
                claveInNvoInsert = string.Empty,
                codigoBNvoInsert = string.Empty,
                tipoDescuentoNvoInsert = string.Empty,
                idUsrNvoInsert = string.Empty,
                tipoProdNvoInsert = string.Empty;
        #endregion Final Varaibles Globales

        #region Modifying Configuration Settings at Runtime

        XmlDocument xmlDoc = new XmlDocument();
        XmlNode appSettingsNode, newChild;
        ListView chkDatabase = new ListView();  // ListView para los CheckBox de solo detalle
        ListView settingDatabases = new ListView(); // ListView para los CheckBox de Sistema
        ListViewItem lvi;
        string connStr, keyName;
        //int found = 0;
        NameValueCollection appSettings;

        string gralDetailSelected = string.Empty,
                gralDetailGralSelected = string.Empty;


        // this code will add a listviewtem
        // to a listview for each database entry
        // in the appSettings section of an App.config file.
        private void loadFormConfig()
        {
            if (Properties.Settings.Default.TipoEjecucion == 1)
            {
                xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            }

            if (Properties.Settings.Default.TipoEjecucion == 2)
            {
                xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            }

            appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

            chkDatabase.Items.Clear();
            settingDatabases.Items.Clear();

            lvi = new ListViewItem();

            try
            {
                chkDatabase.Clear();
                settingDatabases.Clear();

                appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    MessageBox.Show("Lectura App.Config/AppSettings: La Sección de AppSettings está vacia",
                                    "Archivo Vacio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (appSettings.Count > 0)
                {
                    for (int i = 0; i < appSettings.Count; i++)
                    {
                        connStr = appSettings[i];
                        keyName = appSettings.GetKey(i);
                        //found = keyName.IndexOf("chk", 0, 3);
                        //if (found >= 0)
                        //{
                        //    lvi = new ListViewItem(keyName);
                        //    lvi.SubItems.Add(connStr);
                        //    chkDatabase.Items.Add(lvi);
                        //}
                    }

                    for (int i = 0; i < appSettings.Count; i++)
                    {
                        string foundSetting = string.Empty;
                        connStr = appSettings[i];
                        keyName = appSettings.GetKey(i);
                        //found = keyName.IndexOf("chk", 0, 3);
                        //if (found <= -1)
                        //{
                        //    lvi = new ListViewItem(keyName);
                        //    lvi.SubItems.Add(connStr);
                        //    settingDatabases.Items.Add(lvi);
                        //}
                    }
                }
            }
            catch (ConfigurationException e)
            {
                MessageBox.Show("Lectura App.Config/AppSettings: {0}" + e.Message.ToString(),
                                "Error de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Force a reload of the changed section. This 
        // makes the new values available for reading.
        public static void RefreshAppSettings()
        {
            ConfigurationManager.RefreshSection("appSettings");
        }

        // Determines if a key exist within the App.config
        public bool KeyExist(string strKey)
        {
            appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");
            if (appSettingsNode != null)
            {
                // Attempt to locate the requested setting.
                foreach (XmlNode childNode in appSettingsNode)
                {
                    if (childNode.Attributes["key"].Value == strKey)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void BuscarChkBoxListView(ListView lstListView)
        {
            int id = 0,
                row = 0;

            string nameChk = string.Empty,
                    valorChk = string.Empty,
                    chkSettingVariableTxt = string.Empty,
                    chkSettingVariableVal = string.Empty,
                    name = string.Empty,
                    value = string.Empty,
                    nombrePanelContenedor = string.Empty,
                    nombrePanelContenido = string.Empty;

            flowLayoutPanel3.Controls.Clear();

            for (int i = 0; i < lstListView.Items.Count; i++)
            {
                flowLayoutPanel3.Visible = true;
                lblMsgSinSelecDetalles.Visible = false;
                //name = lstListView.Items[i].Text.ToString();
                //value = lstListView.Items[i].SubItems[1].Text.ToString();
                name = lstListView.Items[i].SubItems[1].Text.ToString();
                value = lstListView.Items[i].Text.ToString();

                if (name.Equals("chkProveedor") && value.Equals("true"))
                {
                    nombrePanelContenedor = "panelContenedor" + name;
                    nombrePanelContenido = "panelContenido" + name;

                    Panel panelContenedor = new Panel();
                    panelContenedor.Width = 830;
                    panelContenedor.Height = 58;
                    panelContenedor.Name = nombrePanelContenedor;
                    //panelContenedor.BackColor = Color.DeepPink;

                    //chkSettingVariableTxt = lstListView.Items[i].Text.ToString();
                    //chkSettingVariableVal = lstListView.Items[i].SubItems[1].Text.ToString();
                    chkSettingVariableTxt = lstListView.Items[i].SubItems[1].Text.ToString();
                    chkSettingVariableVal = lstListView.Items[i].Text.ToString();

                    if (chkSettingVariableVal.Equals("true"))
                    {
                        name = chkSettingVariableTxt;
                        value = chkSettingVariableVal;
                        Panel panelContenido = new Panel();
                        panelContenido.Name = nombrePanelContenido;
                        panelContenido.Width = 825;
                        panelContenido.Height = 55;
                        //panelContenido.BackColor = Color.DeepSkyBlue;

                        Label lblNombreProveedor = new Label();
                        lblNombreProveedor.Name = "lblNombre" + name;
                        lblNombreProveedor.Width = 815;
                        lblNombreProveedor.Height = 20;
                        lblNombreProveedor.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                        lblNombreProveedor.Location = new Point(3, 32);
                        lblNombreProveedor.TextAlign = ContentAlignment.MiddleCenter;
                        lblNombreProveedor.BackColor = Color.White;

                        int XcbProv = 0;
                        XcbProv = panelContenido.Width / 2;

                        CargarProveedores();

                        ComboBox cbProveedor = new ComboBox();
                        cbProveedor.Name = "cb" + name;
                        cbProveedor.Width = 815;
                        cbProveedor.Height = 30;
                        cbProveedor.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                        cbProveedor.Location = new Point(XcbProv - (cbProveedor.Width / 2), 5);
                        cbProveedor.SelectedIndexChanged += new EventHandler(comboBoxProveedor_SelectValueChanged);
                        cbProveedor.MouseWheel += new MouseEventHandler(ComboBox_Quitar_MouseWheel);

                        if (listaProveedores.Length > 0)
                        {
                            cbProveedor.DataSource = proveedores.ToArray();
                            cbProveedor.DisplayMember = "Value";
                            cbProveedor.ValueMember = "Key";
                            cbProveedor.SelectedValue = "0";
                        }
                        else if (listaProveedores.Length < 0)
                        {
                            cbProveedor.Items.Add("Proveedores...");
                            cbProveedor.SelectedIndex = 0;
                        }
                        else if (cbProveedor.Items.Count == 0)
                        {
                            cbProveedor.Items.Add("Proveedores...");
                            cbProveedor.SelectedIndex = 0;
                        }
                        cbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;

                        panelContenido.Controls.Add(cbProveedor);
                        panelContenido.Controls.Add(lblNombreProveedor);

                        panelContenedor.Controls.Add(panelContenido);
                        flowLayoutPanel3.Controls.Add(panelContenedor);

                        // Cuando se le da click en la opcion editar producto
                        if (DatosSourceFinal == 2 || DatosSourceFinal == 4)
                        {
                            var idProducto = Convert.ToInt32(idEditarProducto);
                            var idProveedor = mb.DetallesProducto(idProducto, FormPrincipal.userID);

                            int cantidad = idProveedor.Length;

                            if (cantidad > 0)
                            {
                                if (!idProveedor[1].Equals(""))
                                {
                                    if (Convert.ToInt32(idProveedor[1].ToString()) > 0)
                                    {
                                        cargarDatosProveedor(Convert.ToInt32(idProveedor[1]));
                                        if (!datosProveedor.Equals(null))
                                        {
                                            lblNombreProveedor.Text = datosProveedor[0];
                                            diccionarioDetalleBasicos.Add(contadorIndex, new Tuple<string, string, string, string>(idProveedor[0].ToString(), nombrePanelContenido, idProveedor[0].ToString(), datosProveedor[0].ToString()));
                                            contadorIndex++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                } // aqui se continua con los demas else if
                else if (!name.Equals("chkProveedor") && value.Equals("true"))// cualquier otro 
                {
                    var nameTag = name.ToString().Remove(0, 3);
                    nombrePanelContenedor = "panelContenedor" + name.ToString().Remove(0, 3);
                    nombrePanelContenido = "panelContenido" + name.ToString().Remove(0, 3);

                    Panel panelContenedor = new Panel();
                    panelContenedor.Width = 830;
                    panelContenedor.Height = 58;
                    panelContenedor.Name = nombrePanelContenedor;
                    //panelContenedor.BackColor = Color.DeepPink;

                    //chkSettingVariableTxt = lstListView.Items[i].Text.ToString();
                    //chkSettingVariableVal = lstListView.Items[i].SubItems[1].Text.ToString();

                    chkSettingVariableTxt = lstListView.Items[i].SubItems[1].Text.ToString().Replace("_", " ");
                    chkSettingVariableVal = lstListView.Items[i].Text.ToString();

                    if (chkSettingVariableVal.Equals("true"))
                    {
                        name = chkSettingVariableTxt;
                        value = chkSettingVariableVal;

                        Panel panelContenido = new Panel();
                        panelContenido.Name = nombrePanelContenido;
                        panelContenido.Width = 825;
                        panelContenido.Height = 55;
                        //panelContenido.BackColor = Color.DeepSkyBlue;

                        Label lblNombreDetalleGral = new Label();
                        lblNombreDetalleGral.Name = "lblNombre" + name;
                        lblNombreDetalleGral.Width = 815;
                        lblNombreDetalleGral.Height = 20;
                        lblNombreDetalleGral.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                        lblNombreDetalleGral.Location = new Point(3, 32);
                        lblNombreDetalleGral.TextAlign = ContentAlignment.MiddleCenter;
                        lblNombreDetalleGral.BackColor = Color.White;

                        int XcbProv = 0;
                        XcbProv = panelContenido.Width / 2;

                        CargarDetallesGral(name.ToString().Remove(0, 3));

                        ComboBox cbDetalleGral = new ComboBox();
                        cbDetalleGral.Tag = nameTag;
                        cbDetalleGral.Name = "cb" + name.Replace("_", " ");
                        cbDetalleGral.Width = 815;
                        cbDetalleGral.Height = 30;
                        cbDetalleGral.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                        cbDetalleGral.Location = new Point(XcbProv - (cbDetalleGral.Width / 2), 5);
                        cbDetalleGral.SelectedIndexChanged += new System.EventHandler(ComboBoxDetalleGral_SelectValueChanged);
                        cbDetalleGral.MouseWheel += new MouseEventHandler(ComboBox_Quitar_MouseWheel);
                        cbDetalleGral.DropDownStyle = ComboBoxStyle.DropDownList;
                        //cbDetalleGral.BackColor = Color.CadetBlue;

                        if (listaDetalleGral.Length > 0)
                        {
                            cbDetalleGral.DataSource = detallesGral.ToArray();
                            cbDetalleGral.DisplayMember = "value";
                            cbDetalleGral.ValueMember = "Key";
                            cbDetalleGral.SelectedValue = "0";
                        }
                        else if (cbDetalleGral.Items.Count == 0)
                        {
                            cbDetalleGral.Items.Add(name.ToString().Remove(0, 3).Replace("_", " ") + "...");
                            cbDetalleGral.SelectedIndex = 0;
                        }
                        panelContenido.Controls.Add(cbDetalleGral);
                        panelContenido.Controls.Add(lblNombreDetalleGral);
                        panelContenedor.Controls.Add(panelContenido);
                        flowLayoutPanel3.Controls.Add(panelContenedor);

                        // Cuando se da click en la opcion editar producto
                        if (DatosSourceFinal == 2 || DatosSourceFinal == 4)
                        {
                            string Descripcion = string.Empty;

                            foreach (Control contHijo in flowLayoutPanel3.Controls)
                            {
                                foreach (Control contSubHijo in contHijo.Controls)
                                {
                                    if (contSubHijo.Name == nombrePanelContenido)
                                    {
                                        foreach (Control contItemSubHijo in contSubHijo.Controls)
                                        {
                                            if (contItemSubHijo is Label)
                                            {
                                                Descripcion = contItemSubHijo.Text;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            if (Descripcion.Equals("") || Descripcion.Equals(null))
                            {
                                Descripcion = nombrePanelContenido;
                            }
                            else if (!Descripcion.Equals(""))
                            {

                            }

                            idProductoDetalleGral = Convert.ToInt32(idEditarProducto);
                            var DetalleGralPorPanel = mb.DetallesProductoGralPorPanel(Descripcion, FormPrincipal.userID, idProductoDetalleGral);

                            int cantidad = DetalleGralPorPanel.Length;

                            if (cantidad > 0)
                            {
                                if (Descripcion.Equals(nombrePanelContenido))
                                {
                                    int idDetailGral = 0;
                                    idDetailGral = Convert.ToInt32(DetalleGralPorPanel[3].ToString());

                                    foreach (Control contHijo in flowLayoutPanel3.Controls)
                                    {
                                        foreach (Control contSubHijo in contHijo.Controls)
                                        {
                                            if (contSubHijo.Name == nombrePanelContenido)
                                            {
                                                var idDetalleGral = mb.DetallesProductoGral(FormPrincipal.userID, idDetailGral);

                                                foreach (Control contItemSubHijo in contSubHijo.Controls)
                                                {
                                                    if (contItemSubHijo is Label)
                                                    {
                                                        contItemSubHijo.Text = idDetalleGral[2].ToString();
                                                        diccionarioDetallesGeneral.Add(contadorIndex, new Tuple<string, string, string, string>(DetalleGralPorPanel[0].ToString(), nombrePanelContenido, idDetailGral.ToString(), idDetalleGral[2].ToString()));
                                                        contadorIndex++;
                                                        break;
                                                    }
                                                }

                                                idDetalleGral = new string[] { };
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (flowLayoutPanel3.Controls.Count.Equals(0))
            {
                flowLayoutPanel3.Visible = false;
                lblMsgSinSelecDetalles.Visible = true;
                lblMsgSinSelecDetalles.Height = 250;
                lblMsgSinSelecDetalles.Text = Utilidades.JustifyParagraph(lblMsgSinSelecDetalles.Text, lblMsgSinSelecDetalles.Font, lblMsgSinSelecDetalles.ClientSize.Width);
            }
        }

        private void ComboBox_Quitar_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs ee = (HandledMouseEventArgs)e;
            ee.Handled = true;
        }

        private void ComboBoxDetalleGral_SelectValueChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            string cadena = string.Empty, namePanel = string.Empty;
            char[] delimiterChars = { '|' };
            int comboBoxIndex = 0;

            comboBoxIndex = comboBox.SelectedIndex;
            //namePanel = comboBox.Name.ToString().Remove(0, 2);
            namePanel = comboBox.Tag.ToString();
            gralDetailGralSelected = Convert.ToString(comboBox.Text).Replace(" ", "_");

            if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3) || DatosSourceFinal.Equals(2))
            {
                //listaDetalleGral = mb.ObtenerDetallesGral(FormPrincipal.userID, namePanel.Remove(0, 3));
                listaDetalleGral = mb.ObtenerDetallesGral(FormPrincipal.userID, namePanel);
            }

            if (listaDetalleGral.Length > 0)
            {
                idProductoDetalleGral = 0;
                if (comboBoxIndex > 0)
                {
                    cadena = string.Join("", listaDetalleGral[comboBoxIndex - 1]);
                    separadas = cadena.Split(delimiterChars);
                    idProductoDetalleGral = Convert.ToInt32(separadas[0]);
                    nombreDetalleGral = separadas[1].Replace("_", " ");
                }
                else if (comboBoxIndex <= 0)
                {
                    idProductoDetalleGral = 0;
                    limpiarDatosGral(namePanel);
                }

                if (idProductoDetalleGral > 0)
                {
                    cargarDatosGral(Convert.ToInt32(idProductoDetalleGral));
                    llenarDatosGral(namePanel);
                }

                llenarListaDatosDinamicos();
            }
        }

        private void limpiarDatosGral(string textoBuscado)
        {
            string namePanel = string.Empty;

            namePanel = "panelContenedor" + textoBuscado;

            foreach (Control contHijo in flowLayoutPanel3.Controls.OfType<Control>())
            {
                if (contHijo.Name == namePanel)
                {
                    foreach (Control contSubHijo in contHijo.Controls.OfType<Control>())
                    {
                        namePanel = "panelContenido" + textoBuscado;
                        if (contSubHijo.Name == namePanel)
                        {
                            foreach (Control contLblHijo in contSubHijo.Controls.OfType<Control>())
                            {
                                var nuevoTextoBuscado = textoBuscado.TrimEnd("[_]".ToCharArray());
                                if (contLblHijo.Name.Trim() == "lblNombrechk" + nuevoTextoBuscado)
                                {
                                    //contLblHijo.Text = separadas[1].ToString().Replace("_", " ");
                                    //contLblHijo.Text = "En Construcción está sección...";
                                    contLblHijo.Text = string.Empty;
                                    using (DataTable dtResultado = cn.CargarDatos(cs.limpiarEspecificacionDetalleProducto(idEditarProducto, namePanel, gralDetailGralSelected)))
                                    {
                                        if (!dtResultado.Rows.Count.Equals(0))
                                        {
                                            foreach (DataRow item in dtResultado.Rows)
                                            {
                                                var resultadoDeBorrado = cn.EjecutarConsulta(cs.borrarEspecificacionDetalleProducto(item["ID"].ToString()));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void llenarDatosGral(string textoBuscado)
        {
            string namePanel = string.Empty;

            //namePanel = "panelContenedor" + textoBuscado.Remove(0, 3);
            namePanel = "panelContenedor" + textoBuscado;

            string nvoConceptoDetalleProducto = string.Empty;

            var idGralDetail = mb.GetDetalleGeneral(FormPrincipal.userID, gralDetailGralSelected);

            foreach (Control contHijo in flowLayoutPanel3.Controls.OfType<Control>())
            {
                if (contHijo.Name == namePanel)
                {
                    foreach (Control contSubHijo in contHijo.Controls.OfType<Control>())
                    {
                        //namePanel = "panelContenido" + textoBuscado.Remove(0, 3);
                        namePanel = "panelContenido" + textoBuscado;
                        if (contSubHijo.Name == namePanel)
                        {
                            foreach (Control contLblHijo in contSubHijo.Controls.OfType<Control>())
                            {
                                var nuevoTextoBuscado = textoBuscado.TrimEnd("[_]".ToCharArray());
                                nuevoTextoBuscado = nuevoTextoBuscado.Replace("_", " ");

                                if (contLblHijo.Name.Trim() == "lblNombrechk" + nuevoTextoBuscado)
                                {
                                    contLblHijo.Text = datosDetalleGral[3];
                                    if (DatosSourceFinal == 2)
                                    {
                                        nvoConceptoDetalleProducto = contLblHijo.Text;
                                        string[] dataSave = { idProductoBuscado, Convert.ToString(FormPrincipal.userID), namePanel, idGralDetail[0].ToString(), "1" };
                                        var resultadoBuscarDetalleGeneralProducto = mb.obtenerUnDetalleProductoGenerales(idProductoBuscado, Convert.ToString(FormPrincipal.userID), namePanel);
                                        if (!resultadoBuscarDetalleGeneralProducto.Count().Equals(0))
                                        {
                                            int respuestaChangeDetailProducto = cn.EjecutarConsulta(cs.GuardarDetallesProductoGeneralesDesdeAgregarEditarProducto(dataSave));
                                        }
                                        else if (resultadoBuscarDetalleGeneralProducto.Count().Equals(0))
                                        {
                                            int respuestaChangeDetailProducto = cn.EjecutarConsulta(cs.GuardarDetallesProductoGeneralesComboBox(dataSave));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void cargarDatosGral(int idProdDetGral)
        {
            // Para que no de error ya que nunca va a existir un proveedor en ID = 0
            if (idProdDetGral > 0)
            {
                datosDetalleGral = mb.ObtenerDetalleGral(idProdDetGral, FormPrincipal.userID);
            }
        }

        private void CargarDetallesGral(string textBuscado)
        {
            string concepto = string.Empty;
            detallesGral = new Dictionary<string, string>();

            concepto = textBuscado.Replace(" ", "_");

            listaDetalleGral = mb.ObtenerDetallesGral(FormPrincipal.userID, concepto);

            if (listaDetalleGral.Length > 0)
            {
                detallesGral.Add("0", concepto.Replace("_", " ") + "...");

                foreach (var DetailGral in listaDetalleGral)
                {
                    var auxiliar = DetailGral.Split('|');

                    detallesGral.Add(auxiliar[0], auxiliar[1].Replace("_", " "));
                }
                Array.Clear(listaDetalleGral, 0, listaDetalleGral.Length);
            }
            else
            {
                detallesGral.Add("0", concepto.Replace("_", " ") + "...");
            }
        }

        private void comboBoxProveedor_SelectValueChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            string cadena = string.Empty, namePanel = string.Empty;
            char[] delimiterChars = { '-' };
            int comboBoxIndex = 0;

            comboBoxIndex = comboBox.SelectedIndex;
            namePanel = comboBox.Name.ToString().Remove(0, 2);
            gralDetailSelected = comboBox.SelectedItem.ToString();

            if (listaProveedores.Length > 0)
            {
                idProveedor = 0;
                if (comboBoxIndex > 0)
                {
                    cadena = string.Join("", listaProveedores[comboBoxIndex - 1]);
                    separadas = cadena.Split(delimiterChars);
                    idProveedor = Convert.ToInt32(separadas[0]);
                    nombreProveedor = separadas[1];
                }
                else if (comboBoxIndex <= 0)
                {
                    idProveedor = 0;
                    limpiarDatosProveedor(namePanel);
                }

                if (idProveedor > 0)
                {
                    cargarDatosProveedor(Convert.ToInt32(idProveedor));
                    llenarDatosProveedor(namePanel);
                }
                llenarListaDatosDinamicos();
            }
        }

        private void limpiarDatosProveedor(string textoBuscado)
        {
            string namePanel = string.Empty;
            string nvoNombreProveedorDetalleProducto = string.Empty;

            var idProveedor = mb.DetallesProducto(Convert.ToInt32(idProductoBuscado), FormPrincipal.userID);

            namePanel = "panelContenedor" + textoBuscado;

            foreach (Control contHijo in flowLayoutPanel3.Controls.OfType<Control>())
            {
                if (contHijo.Name == namePanel)
                {
                    foreach (Control contSubHijo in contHijo.Controls.OfType<Control>())
                    {
                        namePanel = "panelContenido" + textoBuscado;
                        if (contSubHijo.Name == namePanel)
                        {
                            foreach (Control contLblHijo in contSubHijo.Controls.OfType<Control>())
                            {
                                if (contLblHijo.Name == "cb" + textoBuscado)
                                {
                                    //contLblHijo.Text = datosProveedor[0];
                                    contLblHijo.Text = string.Empty;
                                }
                                if (contLblHijo.Name == "lblNombre" + textoBuscado)
                                {
                                    //contLblHijo.Text = datosProveedor[0];
                                    contLblHijo.Text = string.Empty;
                                }
                                else if (contLblHijo.Name == "lblRFC" + textoBuscado)
                                {
                                    //contLblHijo.Text = datosProveedor[1];
                                    contLblHijo.Text = string.Empty;
                                }
                                else if (contLblHijo.Name == "lblTel" + textoBuscado)
                                {
                                    //contLblHijo.Text = datosProveedor[10];
                                    contLblHijo.Text = string.Empty;
                                }
                                if (DatosSourceFinal == 2)
                                {
                                    string[] dataSave = { idProductoBuscado, Convert.ToString(FormPrincipal.userID), null, "0" };
                                    int resultChangeProvaider = cn.EjecutarConsulta(cs.GuardarProveedorProducto(dataSave, 1));
                                    //nvoNombreProveedorDetalleProducto = datosProveedor[0];
                                    //var dataProvaider = mb.obtenerIdDetallesProveedor(FormPrincipal.userID, nvoNombreProveedorDetalleProducto);
                                    //string[] dataSave = { idProductoBuscado, Convert.ToString(FormPrincipal.userID), dataProvaider[2].ToString(), dataProvaider[0].ToString() };
                                    //var resultadoBusquedaDetallesProducto = mb.DetallesProducto(Convert.ToInt32(idProductoBuscado), FormPrincipal.userID);
                                    //if (!resultadoBusquedaDetallesProducto.Count().Equals(0))
                                    //{
                                    //    int resultChangeProvaider = cn.EjecutarConsulta(cs.GuardarProveedorProducto(dataSave, 1));
                                    //}
                                    //else if (resultadoBusquedaDetallesProducto.Count().Equals(0))
                                    //{
                                    //    int resultChangeProvaider = cn.EjecutarConsulta(cs.GuardarProveedorProducto(dataSave));
                                    //}
                                }
                            }
                        }
                    }
                }
            }
        }

        private void llenarDatosProveedor(string textoBuscado)
        {
            string namePanel = string.Empty;
            string nvoNombreProveedorDetalleProducto = string.Empty;

            var idProveedor = mb.DetallesProducto(Convert.ToInt32(idProductoBuscado), FormPrincipal.userID);

            namePanel = "panelContenedor" + textoBuscado;

            foreach (Control contHijo in flowLayoutPanel3.Controls.OfType<Control>())
            {
                if (contHijo.Name == namePanel)
                {
                    foreach (Control contSubHijo in contHijo.Controls.OfType<Control>())
                    {
                        namePanel = "panelContenido" + textoBuscado;
                        if (contSubHijo.Name == namePanel)
                        {
                            foreach (Control contLblHijo in contSubHijo.Controls.OfType<Control>())
                            {
                                if (contLblHijo.Name == "cb" + textoBuscado)
                                {
                                    contLblHijo.Text = datosProveedor[0];
                                }
                                if (contLblHijo.Name == "lblNombre" + textoBuscado)
                                {
                                    contLblHijo.Text = datosProveedor[0];
                                }
                                else if (contLblHijo.Name == "lblRFC" + textoBuscado)
                                {
                                    contLblHijo.Text = datosProveedor[1];
                                }
                                else if (contLblHijo.Name == "lblTel" + textoBuscado)
                                {
                                    contLblHijo.Text = datosProveedor[10];
                                }
                                if (DatosSourceFinal == 2)
                                {
                                    nvoNombreProveedorDetalleProducto = datosProveedor[0];
                                    var dataProvaider = mb.obtenerIdDetallesProveedor(FormPrincipal.userID, nvoNombreProveedorDetalleProducto);
                                    string[] dataSave = { idProductoBuscado, Convert.ToString(FormPrincipal.userID), dataProvaider[2].ToString(), dataProvaider[0].ToString() };
                                    var resultadoBusquedaDetallesProducto = mb.DetallesProducto(Convert.ToInt32(idProductoBuscado), FormPrincipal.userID);
                                    if (!resultadoBusquedaDetallesProducto.Count().Equals(0))
                                    {
                                        int resultChangeProvaider = cn.EjecutarConsulta(cs.GuardarProveedorProducto(dataSave, 1));
                                    }
                                    else if (resultadoBusquedaDetallesProducto.Count().Equals(0))
                                    {
                                        int resultChangeProvaider = cn.EjecutarConsulta(cs.GuardarProveedorProducto(dataSave));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AgregarEditarProducto_Activated(object sender, EventArgs e)
        {
            VerificarDatosDeDetalleProducto();
        }

        private void VerificarDatosDeDetalleProducto()
        {
            if ((detalleProductoBasico.Count().Equals(0)) && (detalleProductoGeneral.Count().Equals(0)))
            {
                mostrarOcultarfLPDetallesProducto();
            }
            else if ((detalleProductoBasico.Count() != 0) || (detalleProductoGeneral.Count() != 0))
            {
                fLPDetallesProducto.Visible = true;
                nombreAddNvoProveedor();
                conceptoAddNvoDetalleGral();
            }
        }

        private void nombreAddNvoProveedor()
        {
            bool isEmpty = !detalleProductoBasico.Any();
            if (!isEmpty)
            {
                foreach (Control item in flowLayoutPanel3.Controls)
                {
                    if (item.Name.Equals("panelContenedorchkProveedor"))
                    {
                        foreach (Control subItem in item.Controls)
                        {
                            if (subItem.Name.Equals("panelContenidochkProveedor"))
                            {
                                foreach (Control intoSubItem in subItem.Controls)
                                {
                                    if (intoSubItem is Label)
                                    {
                                        if (intoSubItem.Name.Equals("lblNombrechkProveedor"))
                                        {
                                            intoSubItem.Text = detalleProductoBasico[2].ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void conceptoAddNvoDetalleGral()
        {
            string nombreDetalle = string.Empty;
            string[] words;
            char delimeter = '|';
            bool isEmpty = !detalleProductoGeneral.Any();
            if (!isEmpty)
            {
                foreach (var itemDetalleGral in detalleProductoGeneral)
                {
                    words = itemDetalleGral.Split(delimeter);
                    nombreDetalle = words[4].ToString().Remove(0, 14);
                    foreach (Control itemPanel in flowLayoutPanel3.Controls)
                    {
                        if (itemPanel.Name.Equals("panelContenedor" + nombreDetalle))
                        {
                            foreach (Control subItemPanel in itemPanel.Controls)
                            {
                                if (subItemPanel.Name.Equals("panelContenido" + nombreDetalle))
                                {
                                    foreach (Control intoSubItemPanel in subItemPanel.Controls)
                                    {
                                        if (intoSubItemPanel is Label)
                                        {
                                            intoSubItemPanel.Text = words[5].ToString().Replace("_", " ");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void txtNombreProducto_Validating(object sender, CancelEventArgs e)
        {
            ValidateNombreProducto();
        }

        private bool ValidateNombreProducto()
        {
            bool bStatus = true;

            if (txtNombreProducto.Text.Equals(""))
            {
                errorProvAgregarEditarProducto.SetError(txtNombreProducto, "Debe introducir el Nombre de Producto\npara poder continuar el proceso.");
                bStatus = false;
            }
            else
            {
                errorProvAgregarEditarProducto.SetError(txtNombreProducto, "");
            }

            return bStatus;
        }

        private void txtPrecioProducto_Validating(object sender, CancelEventArgs e)
        {
            ValidatePrecioProducto();
        }

        private bool ValidatePrecioProducto()
        {
            bool bStatus = true;

            if (txtPrecioProducto.Text.Equals("0") ||
                txtPrecioProducto.Text.Equals("0.00") ||
                Convert.ToDouble(txtPrecioProducto.Text) == 0)
            {
                txtPrecioProducto.Select(0, txtPrecioProducto.Text.Length);
                errorProvAgregarEditarProducto.SetError(txtPrecioProducto, "Debe tener un Precio de Venta\npara poder continuar el proceso.");
                bStatus = false;
            }
            else
            {
                errorProvAgregarEditarProducto.SetError(txtPrecioProducto, "");
            }

            return bStatus;
        }

        private void txtStockMinimo_Enter(object sender, EventArgs e)
        {
            txtStockMinimo.SelectAll();
            _lastEnteredControl = (Control)sender;      // capturamos el ultimo control en el que estaba el Focus
        }

        private void txtStockMinimo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar que la tecla presionada no sea CTRL u otra tecla no Numerica
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //Si deseas, puedes permitir numeros decimales (o float)
            //If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        txtStockMinimo.Text = calculadora.lCalculadora.Text;
                    };

                    calcu = 0;
                    if (!calculadora.Visible)
                    {
                        calculadora.Show();
                    }
                    else
                    {
                        calculadora.Show();
                    }
                }
            }

        }

        private void txtStockMinimo_KeyUp(object sender, KeyEventArgs e)
        {
            stockMinimo = txtStockMinimo.Text;
        }

        private void txtStockMinimo_Leave(object sender, EventArgs e)
        {
            string[] words;

            if (txtStockMinimo.Text.Equals(""))
            {
                txtStockMinimo.Text = "0";
            }
            else if (!txtStockMinimo.Text.Equals(""))
            {
                words = txtStockMinimo.Text.Split('.');
                if (words[0].Equals(""))
                {
                    words[0] = "0";
                }
                if (words.Length > 1)
                {
                    if (words[1].Equals(""))
                    {
                        words[1] = "0";
                    }
                    txtStockMinimo.Text = words[0] + "." + words[1];
                }
            }
            ValidarStockMinimo();
        }

        private void ValidarStockMinimo()
        {
            var minimoAux = txtStockMinimo.Text.Trim();
            var maximoAux = txtStockMaximo.Text.Trim();

            if (!string.IsNullOrWhiteSpace(minimoAux))
            {
                if (!string.IsNullOrWhiteSpace(maximoAux))
                {
                    var minimo = float.Parse(minimoAux);
                    var maximo = float.Parse(maximoAux);

                    if ((minimo.Equals(0) && (maximo.Equals(0))))
                    {
                        txtStockMinimo.Text = Convert.ToString(minimo);
                        txtStockMaximo.Text = Convert.ToString(maximo);
                        return;
                    }
                    else if (maximo <= minimo)
                    {
                        //MessageBox.Show("El stock máximo no puede ser menor \no igual que stock mínimo", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        var numero = Convert.ToInt32(minimo);
                        var agregar = numero + 1;
                        txtStockMaximo.Text = agregar.ToString();
                        txtStockMaximo.Focus();
                        return;
                    }
                    else if (maximo > minimo)
                    {
                        stockMinimo = minimo.ToString();
                        stockNecesario = maximo.ToString();
                        return;
                    }
                }
            }
        }

        private void txtStockMaximo_Enter(object sender, EventArgs e)
        {
            txtStockMaximo.SelectAll();
            _lastEnteredControl = (Control)sender;      // capturamos el ultimo control en el que estaba el Focus
        }

        private void txtStockMaximo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar que la tecla presionada no sea CTRL u otra tecla no Numerica
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //Si deseas, puedes permitir numeros decimales (o float)
            //If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                   {
                       txtStockMaximo.Text = calculadora.lCalculadora.Text;
                   };

                    calcu = 0;
                    if (!calculadora.Visible)
                    {
                        calculadora.Show();
                    }
                    else
                    {
                        calculadora.Show();
                    }
                }
            }

        }

        private void txtStockMaximo_KeyUp(object sender, KeyEventArgs e)
        {
            stockNecesario = txtStockMaximo.Text;
        }

        private void txtStockMaximo_Leave(object sender, EventArgs e)
        {
            string[] words;

            if (txtStockMaximo.Text.Equals(""))
            {
                txtStockMaximo.Text = "0";
            }
            else if (!txtStockMaximo.Text.Equals(""))
            {
                words = txtStockMaximo.Text.Split('.');
                if (words[0].Equals(""))
                {
                    words[0] = "0";
                }
                if (words.Length > 1)
                {
                    if (words[1].Equals(""))
                    {
                        words[1] = "0";
                    }
                    txtStockMaximo.Text = words[0] + "." + words[1];
                }
            }

            ValidarStockMaximo();
        }

        private void txtClaveProducto_Validating(object sender, CancelEventArgs e)
        {
            ValidateClaveProducto();
        }

        private bool ValidateClaveProducto()
        {
            bool bStatus = true;

            if (txtClaveProducto.Text.Trim().Equals(""))
            {
                bStatus = false;
            }

            return bStatus;
        }

        private void txtCodigoBarras_Validating(object sender, CancelEventArgs e)
        {
            ValidateCodigoBarras();
        }

        private bool ValidateCodigoBarras()
        {
            bool bStatus = true;

            if (txtCodigoBarras.Text.Trim().Equals(""))
            {
                bStatus = false;
            }

            return bStatus;
        }

        private void txtPrecioMayoreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar que la tecla presionada no sea CTRL u otra tecla no Numerica
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //Si deseas, puedes permitir numeros decimales (o float)
            //If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        txtPrecioMayoreo.Text = calculadora.lCalculadora.Text;
                        calcu = 0;
                    };
                    if (!calculadora.Visible)
                    {
                        calculadora.Show();
                    }
                    else
                    {
                        calculadora.Show();
                    }

                    //if ()
                    //{
                    //    txtStockMaximo.Text = calculadora.lCalculadora.Text;
                    //}
                }
            }
        }

        private void ValidarStockMaximo()
        {
            var minimoAux = txtStockMinimo.Text.Trim();
            var maximoAux = txtStockMaximo.Text.Trim();

            if (!string.IsNullOrWhiteSpace(minimoAux))
            {
                if (!string.IsNullOrWhiteSpace(maximoAux))
                {
                    var minimo = float.Parse(minimoAux);
                    var maximo = float.Parse(maximoAux);

                    if ((minimo.Equals(0)) && (maximo.Equals(0)))
                    {
                        //MessageBox.Show("El stock máximo y mínimo no puede ser cero", "Mensaje del Sistema Stock Cero", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //var numero = Convert.ToInt32(minimo);
                        //var agregar = numero + 1;
                        //txtStockMaximo.Text = agregar.ToString();
                        //txtStockMaximo.Focus();
                        //return;
                    }
                    else if (maximo <= minimo)
                    {
                        MessageBox.Show("El stock máximo no puede ser menor/igual \nque stock mínimo", "Mensaje del Sistema Stock Menor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        var numero = Convert.ToInt32(minimo);
                        var agregar = numero + 1;
                        txtStockMaximo.Text = agregar.ToString();
                        txtStockMaximo.Focus();
                        return;
                    }
                    else if (maximo > minimo)
                    {
                        stockMinimo = minimo.ToString();
                        stockNecesario = maximo.ToString();
                        return;
                    }
                }
            }
        }

        private void cargarDatosProveedor(int idProveedor)
        {
            // Para que no de error ya que nunca va a existir un proveedor en ID = 0
            if (idProveedor > 0)
            {
                datosProveedor = mb.ObtenerDatosProveedor(idProveedor, FormPrincipal.userID);
            }
        }

        private void CargarProveedores()
        {
            // Asignamos el Array con los nombres de los proveedores al comboBox
            listaProveedores = cn.ObtenerProveedores(FormPrincipal.userID);

            proveedores = new Dictionary<string, string>();

            // Comprobar que ya exista al menos un Proveedor
            if (listaProveedores.Length > 0)
            {
                proveedores.Add("0", "Proveedores...");

                foreach (var proveedor in listaProveedores)
                {
                    var tmp = proveedor.Split('-');
                    proveedores.Add(tmp[0].Trim(), tmp[1].Trim());
                }
            }
            else
            {
                proveedores.Add("0", "Proveedores...");
            }
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            //if ((e.Column + e.Row) % 2 == 1)
            //{
            //    using (SolidBrush brush = new SolidBrush(Color.AliceBlue))
            //    {
            //        e.Graphics.FillRectangle(brush, e.CellBounds);
            //    }
            //}
            //else
            //{
            //    using (SolidBrush brush = new SolidBrush(Color.FromArgb(123, 234, 0)))
            //    {
            //        e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
            //    }
            //}
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void bt_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxSetting_CheckedChanged(object sender, EventArgs e)
        {

        }

        #endregion Modifying Configuration Settings at Runtime

        private void tLPServicio_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            //if ((e.Column + e.Row) % 2 == 1)
            //{
            //    using (SolidBrush brush = new SolidBrush(Color.AliceBlue))
            //    {
            //        e.Graphics.FillRectangle(brush, e.CellBounds);
            //    }
            //}
            //else
            //{
            //    using (SolidBrush brush = new SolidBrush(Color.FromArgb(123, 234, 0)))
            //    {
            //        e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
            //    }
            //}
        }

        private void lblStockMinimo_MouseEnter(object sender, EventArgs e)
        {
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            //Graphics gfx = e.Graphics;
            //Pen p = new Pen(Color.Black, 1);
            //gfx.DrawLine(p, 0, 5, 0, e.ClipRectangle.Height - 2);

            //gfx.DrawLine(p, 0, 5, 10, 5);
            //gfx.DrawLine(p, 62, 5, e.ClipRectangle.Width - 2, 5);

            //gfx.DrawLine(p, e.ClipRectangle.Width - 2, 5, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2);

            //gfx.DrawLine(p, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2, 0, e.ClipRectangle.Height - 2);
        }

        private void tLPCombo_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            //if ((e.Column + e.Row) % 2 == 1)
            //{
            //    using (SolidBrush brush = new SolidBrush(Color.AliceBlue))
            //    {
            //        e.Graphics.FillRectangle(brush, e.CellBounds);
            //    }
            //}
            //else
            //{
            //    using (SolidBrush brush = new SolidBrush(Color.FromArgb(123, 234, 0)))
            //    {
            //        e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
            //    }
            //}
        }

        private void fLPDetalleProducto_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            if (DatosSourceFinal.Equals(1))
            {
                if (this.Text.Equals("AGREGAR PRODUCTO"))
                {
                    btnAdd.Visible = true;
                    toolTip1.SetToolTip(btnAdd, "En este apartado podrá visualizar a cual\nCombo ó Servicio esta relacionado.");
                }
                if (this.Text.Equals("AGREGAR COMBOS"))
                {
                    btnAdd.Visible = true;
                    toolTip1.SetToolTip(btnAdd, "En este apartado podrá visualizar la cantidad\ny el producto que esta relacionado con el Combo");
                }
                if (this.Text.Equals("AGREGAR SERVICIOS"))
                {
                    btnAdd.Visible = true;
                    toolTip1.SetToolTip(btnAdd, "En este apartado podrá visualizar la cantidad\ny el producto que esta relacionado con el Servicio");
                }
            }
            if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
            {
                if (this.Text.Equals("EDITAR PRODUCTO"))
                {
                    btnAdd.Visible = true;
                    toolTip1.SetToolTip(btnAdd, "En este apartado podrá visualizar a cual\nCombo ó Servicio esta relacionado.");
                }
                else if (this.Text.Equals("COPIAR PRODUCTO"))
                {
                    btnAdd.Visible = true;
                    toolTip1.SetToolTip(btnAdd, "En este apartado podrá visualizar a cual\nCombo ó Servicio esta relacionado.");
                }

                if (this.Text.Equals("EDITAR COMBOS"))
                {
                    btnAdd.Visible = true;
                    toolTip1.SetToolTip(btnAdd, "En este apartado podrá visualizar la cantidad\ny el producto que esta relacionado con el Combo");
                }
                else if (this.Text.Equals("COPIAR COMBOS"))
                {
                    btnAdd.Visible = true;
                    toolTip1.SetToolTip(btnAdd, "En este apartado podrá visualizar la cantidad\ny el producto que esta relacionado con el Combo");
                }

                if (this.Text.Equals("EDITAR SERVICIOS"))
                {
                    btnAdd.Visible = true;
                    toolTip1.SetToolTip(btnAdd, "En este apartado podrá visualizar la cantidad\ny el producto que esta relacionado con el Servicio");
                }
                else if (this.Text.Equals("COPIAR SERVICIOS"))
                {
                    btnAdd.Visible = true;
                    toolTip1.SetToolTip(btnAdd, "En este apartado podrá visualizar la cantidad\ny el producto que esta relacionado con el Servicio");
                }
            }
        }

        public void PrimerCodBarras()
        {
            Contenido = "7777000001";

            cn.EjecutarConsulta($"INSERT INTO CodigoBarrasGenerado (IDUsuario, CodigoBarras) VALUES ('{FormPrincipal.userID}', '{Contenido}')");
        }

        public void AumentarCodBarras()
        {
            string txtBoxName;
            txtBoxName = _lastEnteredControl.Name;
            if (txtBoxName != "cbTipo" && txtBoxName != "txtNombreProducto" && txtBoxName != "txtStockProducto" && txtBoxName != "txtPrecioProducto" && txtBoxName != "txtCategoriaProducto")
            {
                _lastEnteredControl.Text = Contenido;

                CodigoDeBarras = long.Parse(Contenido);
                CodigoDeBarras++;
                Contenido = CodigoDeBarras.ToString();
                txtCodigoBarras.Text = Contenido;

                cn.EjecutarConsulta(cs.ActualizarCBGenerado(Contenido, FormPrincipal.userID));

                /*using (StreamWriter outfile = new StreamWriter(Properties.Settings.Default.rutaDirectorio + fichero))
                {
                    outfile.WriteLine(Contenido);
                }*/
            }
            else
            {
                MessageBox.Show("Campo no Valido para generar\nCodigo de Barras los campos validos son\nClave Interna y Codigo de Barras... Gracias", "Anvertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblArrow_Click(object sender, EventArgs e)
        {
            btnAdd.PerformClick();
        }

        private void btnAddCodBar_Click(object sender, EventArgs e)
        {
            agregarCodigoBarraExtra();
        }

        public void LimpiarCampos()
        {
            txtNombreProducto.Text = "";
            txtStockProducto.Text = "0";
            txtPrecioCompra.Text = "0";
            txtPrecioProducto.Text = "0";
            txtPrecioMayoreo.Text = "0";
            txtCategoriaProducto.Text = "";
            txtClaveProducto.Text = "";
            txtCodigoBarras.Text = "";
        }

        private void botonRedondo1_Click(object sender, EventArgs e)
        {
            ListaProductos ListStock = new ListaProductos();
            ListStock.nombreProducto += new ListaProductos.pasarProducto(ejecutar);

            if (this.Text.Trim().Equals("AGREGAR PRODUCTO") ||
                this.Text.Trim().Equals("EDITAR PRODUCTO") ||
                this.Text.Trim().Equals("COPIAR PRODUCTO"))
            {
                ListStock.TypeStock = "Combos";

                if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
                {
                    ListStock.listaServCombo = listaProductoToCombo;
                    ListStock.listaProd = ProductosDeServicios;
                }

                if (DatosSourceFinal.Equals(2))
                {
                    if (!Convert.ToInt32(idEditarProducto).Equals(0))
                    {
                        ListStock.idProdEdit = Convert.ToInt32(idEditarProducto);
                    }
                }
            }
            else if (this.Text.Trim().Equals("AGREGAR COMBOS") ||
                    this.Text.Trim().Equals("EDITAR COMBOS") ||
                    this.Text.Trim().Equals("COPIAR COMBOS") ||
                    this.Text.Trim().Equals("AGREGAR SERVICIOS") ||
                    this.Text.Trim().Equals("EDITAR SERVICIOS") ||
                    this.Text.Trim().Equals("COPIAR SERVICIOS"))
            {
                ListStock.TypeStock = "Productos";

                if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
                {
                    ListStock.listaServCombo = listaProductoToCombo;
                    ListStock.listaProd = ProductosDeServicios;
                }

                if (DatosSourceFinal.Equals(2))
                {
                    if (!Convert.ToInt32(idEditarProducto).Equals(0))
                    {
                        ListStock.idProdEdit = Convert.ToInt32(idEditarProducto);
                    }
                }
            }

            ListStock.DatosSourceFinal = DatosSourceFinal;
            ListStock.ShowDialog();
        }

        private void botonRedondo4_Click(object sender, EventArgs e)
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
                if (DatosSourceFinal == 2 || DatosSourceFinal == 4 || DatosSourceFinal.Equals(1))
                {
                    precioProducto = txtPrecioProducto.Text;
                }

                if (FormAgregar != null)
                {
                    try
                    {
                        FormAgregar.ShowDialog();
                        FormAgregar.BringToFront();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Excepción: " + ex.Message.ToString(), "Advertencia " + this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    FormAgregar = new AgregarDescuentoProducto();
                    FormAgregar.ShowDialog();
                }
            }

            //List<string> usuariosPermitidos = new List<string>()
            //{
            //    "HOUSEDEPOTAUTLAN",
            //    "HOUSEDEPOTGRULLO",
            //    "HOUSEDEPOTREPARTO",
            //    "OXXOCLARA6"
            //};


            //if (usuariosPermitidos.Contains(FormPrincipal.userNickName))
            //{
            //    if (txtPrecioProducto.Text == "")
            //    {
            //        MessageBox.Show("Es necesario agregar el precio del producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else if (txtStockProducto.Text == "")
            //    {
            //        MessageBox.Show("Es necesario agregar el stock del producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    {
            //        if (DatosSourceFinal == 2 || DatosSourceFinal == 4 || DatosSourceFinal.Equals(1))
            //        {
            //            precioProducto = txtPrecioProducto.Text;
            //        }

            //        if (FormAgregar != null)
            //        {
            //            try
            //            {
            //                FormAgregar.Show();
            //                FormAgregar.BringToFront();
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show("Excepción: " + ex.Message.ToString(), "Advertencia " + this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            }
            //        }
            //        else
            //        {
            //            FormAgregar = new AgregarDescuentoProducto();
            //            FormAgregar.Show();
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Estamos trabajando en este apartado", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void botonRedondo2_Click(object sender, EventArgs e)
        {
            FormDetalleProducto = Application.OpenForms.OfType<AgregarDetalleProducto>().FirstOrDefault();

            if (DatosSourceFinal == 1 || DatosSourceFinal == 3 || DatosSourceFinal == 4)
            {
                //Verifica que el formulario ya tenga una instancia creada, de lo contrario la crea
                if (FormDetalleProducto == null)
                {
                    FormDetalleProducto = new AgregarDetalleProducto();
                    //FormDetalleProducto.typeDatoProveedor = 1;
                    FormDetalleProducto.origenProdServCombo = DatosSourceFinal;
                    FormDetalleProducto.getIdProducto = idProductoFinal;
                    if (DatosSourceFinal.Equals(3))
                    {
                        FormDetalleProducto.nameXMLProveedor = nameProveedorXML;
                    }
                    FormDetalleProducto.ShowDialog();
                    FormDetalleProducto.BringToFront();
                }
                else
                {
                    //FormDetalleProducto.typeDatoProveedor = 1;
                    FormDetalleProducto.origenProdServCombo = DatosSourceFinal;
                    FormDetalleProducto.getIdProducto = idProductoFinal;
                    if (DatosSourceFinal.Equals(3))
                    {
                        FormDetalleProducto.nameXMLProveedor = nameProveedorXML;
                    }
                    FormDetalleProducto.ShowDialog();
                    FormDetalleProducto.BringToFront();
                }
            }
            if (DatosSourceFinal == 2)
            {
                //Verifica que el formulario ya tenga una instancia creada, de lo contrario la crea
                if (FormDetalleProducto == null)
                {
                    FormDetalleProducto = new AgregarDetalleProducto();
                    FormDetalleProducto.origenProdServCombo = DatosSourceFinal;
                    FormDetalleProducto.getIdProducto = idProductoFinal;
                    FormDetalleProducto.ShowDialog();
                    FormDetalleProducto.BringToFront();
                }
                else
                {
                    FormDetalleProducto.origenProdServCombo = DatosSourceFinal;
                    FormDetalleProducto.getIdProducto = idProductoFinal;
                    FormDetalleProducto.ShowDialog();
                    FormDetalleProducto.BringToFront();
                }
            }
        }

        private void botonRedondo3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPrecioProducto.Text))
            {
                MessageBox.Show("Es necesario agregar el precio del producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //if (DatosSourceFinal == 2)
                //{
                //    precioProducto = txtPrecioProducto.Text;
                //}

                precioProducto = txtPrecioProducto.Text;

                //Verifica que el formulario ya tenga una instancia creada, de lo contrario la crea
                if (FormDetalle != null)
                {
                    //FormDetalle.id_producto_edit = Convert.ToInt32(idEditarProducto);
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
                    FormDetalle.id_producto_edit = Convert.ToInt32(idEditarProducto);
                    FormDetalle.limpiarCampos();
                    FormDetalle.ShowDialog();
                }
            }
        }

        private void botonRedondo5_Click(object sender, EventArgs e)
        {
            var tituloVentana = string.Empty;

            #region Inicio Sección de Cambio de Producto a Servicio/Combo ó Servicio/Combo a Producto
            // Condiciones para saber si se realiza el cambio de un producto a servicio y viceversa
            if (cambioProducto)
            {
                validarCambioProducto();
            }
            #endregion Final Sección de Cambio de Producto a Servicio/Combo ó Servicio/Combo a Producto

            #region Inicio de Variables de Alejandro
            /****************************
	        *	codigo de Alejandro		*
	        ****************************/
            filtroTipoSerPaq = Convert.ToString(cbTipo.SelectedItem);
            tipoServPaq = filtroTipoSerPaq;
            nombre = txtNombreProducto.Text.ToString().Replace("'", "\\'");
            if (this.Text.Trim() == "AGREGAR PRODUCTO" | this.Text.Trim() == "EDITAR PRODUCTO" | this.Text.Trim() == "COPIAR PRODUCTO")
            {
                validarDecimales(txtStockProducto.Text.ToString());
            }
            if (!parteDecimal.Equals(""))
            {
                stock = parteEntera + "." + parteDecimal;
            }
            else if (parteDecimal.Equals(""))
            {
                stock = parteEntera;
            }
            //stock = txtStockProducto.Text;
            valorDePrecioVenta = Convert.ToDouble(txtPrecioProducto.Text);
            precio = valorDePrecioVenta.ToString();
            categoria = txtCategoriaProducto.Text;
            claveIn = txtClaveProducto.Text.Trim();
            codigoB = txtCodigoBarras.Text.Trim();
            ProdServPaq = "P".ToString();
            tipoDescuento = "0";
            idUsrNvo = FormPrincipal.userID.ToString();
            fechaCompra = DateTime.Now.ToString("yyyy-MM-dd");
            fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            stockMinimo = txtStockMinimo.Text;
            stockNecesario = txtStockMaximo.Text;
            var precioMayoreo = txtPrecioMayoreo.Text.Trim();

            if (string.IsNullOrWhiteSpace(precioMayoreo))
            {
                precioMayoreo = "0";
            }

            /*	Fin del codigo de Alejandro	*/
            #endregion Final de Variables de Alejandro

            #region Inicio de Variables a Cero
            /************************************
	        *  iniciamos las variables a 0     *
	        *	codigo de Emmanuel				*
	        ************************************/

            resultadoSearchNoIdentificacion = 0;    // ponemos los valores en 0
            resultadoSearchCodBar = 0;              // ponemos los valores en 0
            resultadoSearchCodBarExtra = 0;         // ponemos los valores en 0

            /* Fin de iniciamos las variables a 0 */
            #endregion Final de Variables a Cero

            #region Inicio de Sección Origen (Forma Manual / XML), (Editar) ó ( Hacer Copia)
            saberOrigenProducto();
            #endregion Final de Sección Origen (Forma Manual / XML), (Editar) ó ( Hacer Copia)

            #region Inicio Sección De Agregar Producto, Combo ó Servicio Desde XML / Botón manual
            if (DatosSourceFinal == 3 || DatosSourceFinal == 1 || DatosSourceFinal == 5 || DatosSourceFinal == 4)
            {
                #region Inicio Sección que el precio no sea menor al precio original del producto servicio/combo
                //Validar que el precio no sea menor al precio original del producto/servicio
                if (Convert.ToDouble(precio) < Convert.ToDouble(txtPrecioCompra.Text))
                {
                    MessageBox.Show("El precio no puede ser mayor al precio original", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrecioProducto.Focus();
                    return;
                }

                if (ProductosDeServicios.Count() > 0)
                {
                    string tipoAux = string.Empty;
                    bool tipoValido = false;

                    if (this.Text.Trim() == "AGREGAR SERVICIOS" || this.Text.Trim() == "EDITAR SERVICIOS" || this.Text.Trim() == "COPIAR SERVICIOS")
                    {
                        tipoAux = "servicio";
                        tipoValido = true;
                    }

                    if (this.Text.Trim() == "AGREGAR COMBOS" || this.Text.Trim() == "EDITAR COMBOS" || this.Text.Trim() == "COPIAR COMBOS")
                    {
                        tipoAux = "combo";
                        tipoValido = true;
                    }

                    if (tipoValido)
                    {
                        if (Convert.ToDecimal(txtCantPaqServ.Text) <= 0)
                        {
                            MessageBox.Show($"Ingrese una cantidad para el campo \"Cantidad por {tipoAux}\"", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCantPaqServ.Focus();
                            return;
                        }
                    }
                }

                #endregion Final Sección que el precio no sea menor al precio original del producto servicio/combo

                #region Inicio Sección busqueda que no se repita la ClaveInterna
                //Hacemos la busqueda que no se repita en CalveInterna
                //searchClavIntProd();
                if (mb.ComprobarCodigoClave(claveIn, FormPrincipal.userID))
                {
                    string query = string.Empty;
                    datosProductosBtnGuardar = new List<string>();
                    datosProductoRelacionado = new List<string>();

                    // Cargar procuto registrado con esa Clave Interna
                    productoRegistradoClaveInterna(claveIn);

                    // Buscamos Producto con codigo de barras
                    productoRegistradoCodigoBarras(claveIn);
                }
                #endregion Final Sección busqueda que no se repita la ClaveInterna

                #region Inicio Sección busqueda que no se repita en CodigoBarra
                //Hacemos la busqueda que no se repita en CodigoBarra
                //searchCodBar();
                if (mb.ComprobarCodigoClave(codigoB, FormPrincipal.userID))
                {
                    string query = string.Empty;
                    List<string> datosProductos = new List<string>(), datosProductoRelacionado = new List<string>();

                    // Buscamos Producto con codigo de barras
                    productoRegistradoCodigoBarras(codigoB);

                    // Cargar procuto registrado con esa Clave Interna
                    productoRegistradoClaveInterna(codigoB);
                }
                #endregion Final Sección busqueda que no se repita en CodigoBarra

                #region Inicio Seccion Código de Barras Extras
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
                            codigosBarrras.Clear();
                            return;
                        }
                    }
                }
                #endregion Final Seccion Código de Barras Extras

                if (resultadoSearchNoIdentificacion == 1 || resultadoSearchCodBar == 1 || resultadoSearchCodBarExtra == 1)
                {
                    if (!txtClaveProducto.Text.Equals(""))
                    {
                        txtClaveProducto.Focus();
                    }
                    else if (!txtCodigoBarras.Text.Equals(""))
                    {
                        txtCodigoBarras.Focus();
                    }
                }
                else if (resultadoSearchNoIdentificacion == 0 || resultadoSearchCodBar == 0)
                {
                    /****************************
                    *	codigo de Alejandro		*
                    ****************************/
                    #region Inicio de Descuento seleccionado
                    if (descuentos.Any())
                    {
                        //Cerramos la ventana donde se eligen los descuentos
                        FormAgregar.Close();
                        tipoDescuento = descuentos[0];
                    }
                    #endregion Final de Descuento seleccionado

                    #region Inicio de Sección de Productos
                    if (this.Text.Trim() == "AGREGAR PRODUCTO" | this.Text.Trim() == "EDITAR PRODUCTO" | this.Text.Trim() == "COPIAR PRODUCTO")
                    {
                        bool bValidNombreProducto = ValidateNombreProducto();
                        bool bValidPrecioProducto = ValidatePrecioProducto();

                        if (bValidNombreProducto && bValidPrecioProducto)
                        {
                            bool bValidClaveInterna = ValidateClaveProducto();
                            bool bValidCodigoBarras = ValidateCodigoBarras();

                            if (bValidClaveInterna || bValidCodigoBarras)
                            {
                                errorProvAgregarEditarProducto.SetError(txtClaveProducto, "");
                                errorProvAgregarEditarProducto.SetError(txtCodigoBarras, "");

                                // Miri.
                                // Si el producto es guardado sin abrir la ventana de datos de facturación
                                // y el producto ha agregar trae impuestos, entonces verificará el tipo de impuesto princial 
                                // para re-calcular: base, tipo de impuesto e importe del impuesto.
                                if (datosImpuestos == null & DatosSourceFinal == 3 & AgregarStockXML.tipo_impuesto_delxml != "")
                                {
                                    string tmp_tipo_impuesto_delxml = AgregarStockXML.tipo_impuesto_delxml + "%";

                                    // Se verifica si el impuesto principal fue modificado, si no fue editado entonces,
                                    // obtiene el impuesto por default del xml, caso contrario toma el impuesto del radio elegido.
                                    // Si solo hay un impuesto y este es editado, los cambios no los tomaba en cuenta por eso se agrega esta condición, y se modifican algunas cosas que ya se tenian. 
                                    if (impuestoProducto == tmp_tipo_impuesto_delxml)
                                    {
                                        impuestoProducto = AgregarStockXML.tipo_impuesto_delxml;
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrWhiteSpace(impuestoProducto))
                                        {
                                            var impuestop = impuestoProducto.Split('%');
                                            impuestoProducto = impuestop[0].Trim();
                                        }
                                    }

                                    //impuestoProducto = AgregarStockXML.tipo_impuesto_delxml;
                                    baseProducto = precio;
                                    ivaProducto = "0.00";

                                    /*if (AgregarStockXML.tipo_impuesto_delxml != "Exento")
                                    {
                                        impuestoProducto += "%";
                                    }*/

                                    if (impuestoProducto == "8")
                                    {
                                        baseProducto = (Convert.ToDouble(precio) / 1.08).ToString("0.00");
                                        ivaProducto = (Convert.ToDouble(baseProducto) * 0.08).ToString("0.00");
                                    }
                                    if (impuestoProducto == "16")
                                    {
                                        baseProducto = (Convert.ToDouble(precio) / 1.16).ToString("0.00");
                                        ivaProducto = (Convert.ToDouble(baseProducto) * 0.16).ToString("0.00");
                                    }

                                    if (impuestoProducto != "Exento")
                                    {
                                        impuestoProducto += "%";
                                    }
                                }

                                tmp_clave_interna = claveIn;
                                tmp_codigo_barras = codigoB;


                                guardar = new string[] {
                                    nombre, stock, precio, categoria, claveIn, codigoB, claveProducto, claveUnidadMedida,
                                    tipoDescuento, idUsrNvo, logoTipo, ProdServPaq, baseProducto, ivaProducto, impuestoProducto,
                                    mg.RemoverCaracteres(nombre), mg.RemoverPreposiciones(nombre), stockNecesario, stockMinimo,
                                    txtPrecioCompra.Text, precioMayoreo };

                                #region Inicio Se guardan los datos principales del Producto
                                //Se guardan los datos principales del producto
                                try
                                {
                                    respuesta = cn.EjecutarConsulta(cs.GuardarProducto(guardar, FormPrincipal.userID));

                                    if (respuesta > 0)
                                    {
                                        claveProducto = string.Empty;
                                        claveUnidadMedida = string.Empty;

                                        //Se obtiene la ID del último producto agregado
                                        idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));
                                        var claveP = txtClaveProducto.Text;

                                        // Agregar la relacion de producto ya registrado con Combo Servicio
                                        #region Agregar a tabla productosdeservicios
                                        if (!listaProductoToCombo.Count().Equals(0))
                                        {
                                            if (this.Text.Trim() == "AGREGAR PRODUCTO")
                                            {
                                                string[] datos;
                                                foreach (var item in listaProductoToCombo)
                                                {
                                                    datos = item.Split('|');
                                                    string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                    datos[0] = fech.Trim();
                                                    using (DataTable dtDatosProducto = cn.CargarDatos(cs.getDatosProducto(Convert.ToString(idProducto))))
                                                    {
                                                        if (!dtDatosProducto.Rows.Count.Equals(0))
                                                        {
                                                            foreach (DataRow drDatosProd in dtDatosProducto.Rows)
                                                            {
                                                                datos[2] = drDatosProd["ID"].ToString();
                                                                datos[3] = drDatosProd["Nombre"].ToString();
                                                                cn.EjecutarConsulta(cs.insertarProductosServicios(datos));
                                                            }
                                                        }
                                                    }
                                                    cn.EjecutarConsulta(cs.borrarProdRelBlanco(Convert.ToInt32(datos[1].ToString())));
                                                }
                                            }
                                        }
                                        #endregion

                                        // Crear registro en tabla de correos para que por defecto se habilite la opcion de enviar correo al hacer venta
                                        cn.EjecutarConsulta($"INSERT INTO CorreosProducto (IDUsuario,IDEmpleado, IDProducto, CorreoPrecioProducto, CorreoStockProducto, CorreoStockMinimo) VALUES ('{FormPrincipal.userID}','{FormPrincipal.id_empleado}' ,'{idProducto}', 1, 1, 1);");

                                        #region Inicio de Datos de Impuestos
                                        //Se realiza el proceso para guardar los detalles de facturación del producto
                                        if (datosImpuestos != null)
                                        {
                                            guardarDatosImpuestos();
                                        }
                                        if (datosImpuestos == null & DatosSourceFinal == 3 & AgregarStockXML.list_impuestos_traslado_retenido.Count() > 0)
                                        {
                                            guardar_impuestos_dexml(Convert.ToDouble(baseProducto), idProducto);
                                        }
                                        #endregion Final de datos de Impuestos

                                        bool isEmpty = detalleProductoBasico.Any();

                                        if (isEmpty.Equals(false))
                                        {
                                            if (!flowLayoutPanel3.Controls.Count.Equals(0))
                                            {
                                                string panel = string.Empty;
                                                foreach (Control contHijo in flowLayoutPanel3.Controls)
                                                {
                                                    panel = contHijo.Name;
                                                    if (contHijo.Name.Equals("panelContenedorchkProveedor"))
                                                    {
                                                        foreach (Control contSubHijo in contHijo.Controls)
                                                        {
                                                            foreach (Control contItemSubHijo in contSubHijo.Controls)
                                                            {
                                                                if (contItemSubHijo is Label)
                                                                {
                                                                    if (contItemSubHijo.Name.Equals("lblNombrechkProveedor"))
                                                                    {
                                                                        string textoConcepto = string.Empty;
                                                                        if (!contItemSubHijo.Text.Equals(string.Empty))
                                                                        {
                                                                            textoConcepto = contItemSubHijo.Text;
                                                                            var idFound = mb.obtenerIdDetallesProveedor(FormPrincipal.userID, textoConcepto);
                                                                            detalleProductoBasico.Add(Convert.ToString(idProducto));
                                                                            detalleProductoBasico.Add(Convert.ToString(FormPrincipal.userID));
                                                                            detalleProductoBasico.Add(idFound[2].ToString());
                                                                            detalleProductoBasico.Add(idFound[0].ToString());
                                                                            isEmpty = true;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        #region Inicio Detalles del Producto Basicos (Proveedor)
                                        if (isEmpty.Equals(true))
                                        {
                                            // Para guardar los detalles del producto
                                            // Ejemplo: Proveedor, Categoria, Ubicacion, etc.
                                            guardarDetallesProductoBasico(detalleProductoBasico);
                                        }
                                        #endregion Final Detalles del Producto Basicos (Proveedor)

                                        isEmpty = detalleProductoGeneral.Any();

                                        if (isEmpty.Equals(false))
                                        {
                                            if (!flowLayoutPanel3.Controls.Count.Equals(0))
                                            {
                                                string panel = string.Empty;
                                                foreach (Control contHijo in flowLayoutPanel3.Controls)
                                                {
                                                    panel = contHijo.Name;
                                                    if (!contHijo.Name.Equals("panelContenedorchkProveedor"))
                                                    {
                                                        foreach (Control contSubHijo in contHijo.Controls)
                                                        {
                                                            foreach (Control contItemSubHijo in contSubHijo.Controls)
                                                            {
                                                                if (contItemSubHijo is Label)
                                                                {
                                                                    if (!contItemSubHijo.Name.Equals("lblNombrechkProveedor"))
                                                                    {
                                                                        string textoConcepto = string.Empty;
                                                                        if (!contItemSubHijo.Text.Equals(string.Empty))
                                                                        {
                                                                            textoConcepto = contItemSubHijo.Text;
                                                                            string rowDataList = string.Empty;
                                                                            var idFoundNew = mb.obtenerIdDetalleGeneral(FormPrincipal.userID, textoConcepto);

                                                                            rowDataList = Convert.ToString(idProducto) + "|" + Convert.ToString(FormPrincipal.userID) + "|" + idFoundNew[0].ToString() + "|1|panelContenido" + idFoundNew[2].ToString() + "|" + idFoundNew[3].ToString();
                                                                            detalleProductoGeneral.Add(rowDataList);

                                                                            isEmpty = true;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        #region Inicio Detalles del Producto Generales (Dinamicos)
                                        if (isEmpty.Equals(true))
                                        {
                                            guardarDetallesProductoDinamicos(detalleProductoGeneral);
                                        }
                                        #endregion Final Detalles del Producto Generales (Dinamicos)

                                        #region Inicio Agreado de forma manual Boton
                                        if (DatosSourceFinal == 1)
                                        {
                                            #region Inicio para relacionar productos con algun combo/servicio
                                            guardarRelacionDeProductoConComboServicio();
                                            var contenido = cn.EjecutarConsulta($"UPDATE HistorialCompras SET RFCEmisor = '{AgregarDetalleProducto.rfc}', NomEmisor = '{AgregarDetalleProducto.nameProveedor}', ClaveProdEmisor = '{claveP}' WHERE IDProducto = {idProducto}");
                                            #endregion Final para relacionar productos con algun paquete/servicio
                                        }
                                        #endregion Final Agreado de forma manual Boton

                                        #region Inicio Agreado desde XML
                                        if (DatosSourceFinal == 3)
                                        {
                                            idHistorialCompraProducto = 0;
                                            found = 10;
                                            fechaXML = FechaXMLNvoProd;
                                            fecha = fechaXML.Substring(0, found);
                                            hora = fechaXML.Substring(found + 1);
                                            fechaCompleta = fecha + " " + hora;
                                            folio = FolioXMLNvoProd;
                                            RFCEmisor = RFCXMLNvoProd;
                                            nombreEmisor = NobEmisorXMLNvoProd;
                                            claveProdEmisor = ClaveProdEmisorXMLNvoProd;
                                            descuentoXML = DescuentoXMLNvoProd;
                                            PrecioCompraXMLNvoProd = txtPrecioCompra.Text;

                                            #region Inicio Agregar Proveedor a Detalle Producto
                                            try
                                            {
                                                cn.EjecutarConsulta(cs.GuardarDetallesDelProducto(idProducto, FormPrincipal.userID, nameProveedorXML.ToString(), Convert.ToInt32(idProveedorXML.ToString())));
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al Agregar Proveedor a Detalle Producto\n" + origen, "Error Agregar Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            #endregion Final Agregar Proveedor a Detalle Producto

                                            DateTime dt = DateTime.Now;

                                            string query = $@"INSERT INTO HistorialCompras(Concepto, Cantidad, ValorUnitario, Descuento, Precio, FechaLarga, Folio, RFCEmisor, NomEmisor, ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto, IDUsuario) VALUES('{nombre}','{stock}','{PrecioCompraXMLNvoProd}','{descuentoXML}','{precio}','{fechaCompleta}','{folio.Trim()}','{RFCEmisor.Trim()}','{nombreEmisor.Trim()}','{claveProdEmisor.Trim()}','{dt.ToString("yyyy-MM-dd hh:mm:ss")}','{Inventario.idReporte}','{idProducto}','{FormPrincipal.userID}')";

                                            #region Inicio Historial de Compras
                                            try
                                            {
                                                cn.EjecutarConsulta(query);
                                                idHistorialCompraProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM HistorialCompras ORDER BY ID DESC LIMIT 1", 1));
                                                //MessageBox.Show("Registrado Intento 1", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al Agregar al Historial de Compras\n" + origen, "Error Agregar Historial", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            #endregion Final Historial de Compras

                                            DateTime date1 = DateTime.Now;
                                            fechaCompleta = date1.ToString("s");
                                            string Year = fechaCompleta.Substring(0, found);
                                            string Date = fechaCompleta.Substring(found + 1);
                                            string FechaRegistrada = Year + " " + Date;
                                            string queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{FormPrincipal.userID}','{idHistorialCompraProducto}','{FechaRegistrada}')";

                                            try
                                            {
                                                cn.EjecutarConsulta(queryRecordHistorialProd);
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al Agregar Historial de Modificaciones del Producto, Combo, Servicio\n" + origen, "Error Historial de Modificaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        #endregion Final Agregado desde XML

                                        #region Inicio Guardar Descuento del Producto
                                        //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                                        if (descuentos.Any())
                                        {
                                            //Descuento por Cliente
                                            if (descuentos[0] == "1")
                                            {
                                                guardar = new string[] { descuentos[1], descuentos[2], descuentos[3], descuentos[4] };

                                                try
                                                {
                                                    cn.EjecutarConsulta(cs.GuardarDescuentoCliente(guardar, idProducto));
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("Error al Agregar Descuento de Cliente\n" + origen, "Error Descuento de Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                            //Descuento por Mayoreo
                                            if (descuentos[0] == "2")
                                            {
                                                foreach (var descuento in descuentos)
                                                {
                                                    if (descuento == "2") { continue; }

                                                    string[] tmp = descuento.Split('-');

                                                    try
                                                    {
                                                        cn.EjecutarConsulta(cs.GuardarDescuentoMayoreo(tmp, idProducto));
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        MessageBox.Show("Error al Agregar Descuento de Mayoreo\n" + origen, "Error Descuento de Mayoreo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion Final Guardar Descuento del Producto

                                        #region Inicio de Codigos de Barra Extra
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
                                        #endregion Final de Codigos de Barra Extra

                                        if (this.Text.Trim().Equals("AGREGAR PRODUCTO"))
                                        {
                                            titulo = "Producto Agregado";
                                           texto= "Se guardo exitosamente el producto....";
                                            SeleccionaImagen = "correcto";
                                        }
                                        else if (this.Text.Trim().Equals("EDITAR PRODUCTO"))
                                        {
                                            titulo = "Producto Actualizado";
                                            texto = "Se actualizo exitosamente el producto....";
                                            SeleccionaImagen = "correcto";
                                        }
                                        else if (this.Text.Trim().Equals("COPIAR PRODUCTO"))
                                        {
                                            titulo = "Producto Copiado";
                                            texto = "Se copio exitosamente el producto....";
                                            SeleccionaImagen = "correcto";
                                        }

                                        if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
                                        {
                                            
                                            MensajeAgregarEditarCopiarProducto MAECP = new MensajeAgregarEditarCopiarProducto(titulo , texto, SeleccionaImagen);
                                            MAECP.ShowDialog();
                                            //MessageBoxTemporal.Show(tituloVentana, "Aviso del Sistema", 2, false);
                                        }
                                        else if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
                                        {
                                            
                                            MensajeAgregarEditarCopiarProducto MAECP = new MensajeAgregarEditarCopiarProducto(titulo,texto,SeleccionaImagen);
                                            MAECP.ShowDialog();
                                            //MessageBoxTemporal.Show(tituloVentana, "Aviso del Sistema", 2, false);
                                        }

                                        //Cierra la ventana donde se agregan los datos del producto
                                        this.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al Agregar Producto\n" + origen, "Error Agregar Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                #endregion Final Se guardan los datos principales del Producto
                            }
                            else if (!bValidClaveInterna || !bValidCodigoBarras)
                            {
                                errorProvAgregarEditarProducto.SetError(txtClaveProducto, "Debe tener una Clave Interna\npara poder continuar el proceso.");
                                errorProvAgregarEditarProducto.SetError(txtCodigoBarras, "Debe tener un Código de Barras\npara poder continuar el proceso.");
                                MessageBox.Show("La Clave Interna ó Código de Barras\nNO DEBE ESTAR EN BLANCO", "Advertencia de llenado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            if (!bValidNombreProducto)
                            {
                                MessageBox.Show("Por favor poner Datos Validos\npara el campo de NOMBRE DE PRODUCTO", "Alerta del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtNombreProducto.Select(0, txtNombreProducto.Text.Length);
                                txtNombreProducto.Focus();
                            }
                            else if (!bValidPrecioProducto)
                            {
                                MessageBox.Show("Por favor poner Datos Validos\npara el campo de PRECIO DE VENTA", "Alerta del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtPrecioProducto.Select(0, txtPrecioProducto.Text.Length);
                                txtPrecioProducto.Focus();
                            }
                        }
                    }
                    #endregion Final de Sección de Productos
                    #region Inicio de Sección de Combos y Servicios
                    else if (this.Text.Trim() == "AGREGAR COMBOS" || this.Text.Trim() == "EDITAR COMBOS" || this.Text.Trim() == "COPIAR COMBOS" || this.Text.Trim() == "AGREGAR SERVICIOS" || this.Text.Trim() == "EDITAR SERVICIOS" || this.Text.Trim() == "COPIAR SERVICIOS")
                    {
                        bool bValidNombreProducto = ValidateNombreProducto();
                        bool bValidPrecioProducto = ValidatePrecioProducto();

                        if (bValidNombreProducto && bValidPrecioProducto)
                        {
                            bool bValidClaveInterna = ValidateClaveProducto();
                            bool bValidCodigoBarras = ValidateCodigoBarras();

                            if (bValidClaveInterna || bValidCodigoBarras)
                            {
                                errorProvAgregarEditarProducto.SetError(txtClaveProducto, "");
                                errorProvAgregarEditarProducto.SetError(txtCodigoBarras, "");

                                #region Inicio Saber si es Servicio ó Combo
                                string FuenteServPaq = string.Empty;

                                if (this.Text.Trim() == "AGREGAR SERVICIOS" | this.Text.Trim() == "EDITAR SERVICIOS" | this.Text.Trim() == "COPIAR SERVICIOS")
                                {
                                    ProdServPaq = "S";
                                    FuenteServPaq = "Servicio";
                                    categoria = "SERVICIOS";
                                }
                                else if (this.Text.Trim() == "AGREGAR COMBOS" | this.Text.Trim() == "EDITAR COMBOS" | this.Text.Trim() == "COPIAR COMBOS")
                                {
                                    ProdServPaq = "PQ";
                                    FuenteServPaq = "Combo";
                                    categoria = "COMBOS";
                                }
                                #endregion Final Saber si es Servicio ó Combo

                                stock = "0";

                                // Miri.
                                // Si el producto es guardado sin abrir la ventana de datos de facturación
                                // y el producto ha agregar trae impuestos, entonces verificará el tipo de impuesto princial 
                                // para re-calcular: base, tipo de impuesto e importe del impuesto.
                                if (datosImpuestos == null & DatosSourceFinal == 3 & AgregarStockXML.tipo_impuesto_delxml != "")
                                {
                                    impuestoProducto = AgregarStockXML.tipo_impuesto_delxml;
                                    baseProducto = precio;
                                    ivaProducto = "0.00";

                                    if (AgregarStockXML.tipo_impuesto_delxml != "Exento")
                                    {
                                        impuestoProducto += "%";
                                    }

                                    if (AgregarStockXML.tipo_impuesto_delxml == "8")
                                    {
                                        baseProducto = (Convert.ToDouble(precio) / 1.08).ToString("0.00");
                                        ivaProducto = (Convert.ToDouble(baseProducto) * 0.08).ToString("0.00");
                                    }
                                    if (AgregarStockXML.tipo_impuesto_delxml == "16")
                                    {
                                        baseProducto = (Convert.ToDouble(precio) / 1.16).ToString("0.00");
                                        ivaProducto = (Convert.ToDouble(baseProducto) * 0.16).ToString("0.00");
                                    }
                                }
                                // Uso en declaración.
                                tmp_clave_interna = claveIn;
                                tmp_codigo_barras = codigoB;


                                guardar = new string[] {
                                    nombre, stock, precio, categoria, claveIn, codigoB, claveProducto, claveUnidadMedida,
                                    tipoDescuento, FormPrincipal.userID.ToString(), logoTipo, ProdServPaq, baseProducto,
                                    ivaProducto, impuestoProducto, mg.RemoverCaracteres(nombre), mg.RemoverPreposiciones(nombre),
                                    stockNecesario, "0", txtPrecioCompra.Text, precioMayoreo
                                };

                                #region Inicio de guardado de los datos principales del Servicios o Combos
                                //Se guardan los datos principales del producto
                                try
                                {
                                    respuesta = cn.EjecutarConsulta(cs.GuardarProducto(guardar, FormPrincipal.userID));

                                    //Se obtiene la ID del último producto agregado
                                    idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));

                                    if (respuesta > 0)
                                    {
                                        claveProducto = string.Empty;
                                        claveUnidadMedida = string.Empty;

                                        bool isEmpty = !detalleProductoBasico.Any();

                                        #region Inicio Guardar Datos Basicos Detalle Producto
                                        // para guardar los detalles del producto
                                        if (!isEmpty)
                                        {
                                            // Para guardar los detalles del producto
                                            // Ejemplo: Proveedor, Categoria, Ubicacion, etc.
                                            guardarDetallesProductoBasico(detalleProductoBasico);
                                        }
                                        #endregion Final Guardar Datos Basico Detalles Producto

                                        isEmpty = !detalleProductoGeneral.Any();

                                        #region Inicio Guardar Datos Generales Detalle Producto
                                        if (!isEmpty)
                                        {
                                            guardarDetallesProductoDinamicos(detalleProductoGeneral);
                                        }
                                        #endregion Final  Guardar Datos Generales Detalle Producto

                                        #region Inicio Seccion de Agregar desde Boton Producto y de Editar Producto
                                        if (DatosSourceFinal == 1 || DatosSourceFinal == 2)
                                        {
                                            var conceptoProveedor = string.Empty;
                                            var rfcProveedor = string.Empty;

                                            //Datos para la tabla historial de compras
                                            if (idProveedorBtnGuardar != null)
                                            {
                                                var proveedorTmp = mb.ObtenerDatosProveedor(Convert.ToInt32(idProveedorBtnGuardar), FormPrincipal.userID);
                                                conceptoProveedor = proveedorTmp[0];
                                                rfcProveedor = proveedorTmp[1];
                                            }

                                            guardar = new string[] { nombre, stock, precio, txtPrecioCompra.Text, fechaCompra, rfcProveedor, conceptoProveedor, "", "1", fechaOperacion, idReporte.ToString(), idProducto.ToString(), FormPrincipal.userID.ToString() };

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

                                            /*if (flowLayoutPanel2.Controls.Count == 0)
                                            {
                                                string[] tmp = { $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}", $"{idProducto}", "", "", $"{txtCantPaqServ.Text}" };
                                                cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                                            }*/

                                            if (!ProductosDeServicios.Count.Equals(0))
                                            {
                                                foreach (var item in ProductosDeServicios)
                                                {
                                                    var datos = item.Split('|');
                                                    datos[1] = idProducto.ToString();
                                                    cn.EjecutarConsulta(cs.GuardarProductosServPaq(datos));
                                                }
                                            }
                                            else if (ProductosDeServicios.Count.Equals(0))
                                            {
                                                string[] datos = new string[5];
                                                string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                datos[0] = fech;
                                                datos[1] = idProducto.ToString();
                                                datos[2] = "0";
                                                datos[3] = string.Empty;
                                                decimal numeroCantidadProducto = 0;
                                                numeroCantidadProducto = Convert.ToDecimal(txtCantPaqServ.Text);
                                                if (numeroCantidadProducto.Equals(0))
                                                {
                                                    txtCantPaqServ.Text = "1";
                                                    numeroCantidadProducto = Convert.ToDecimal(txtCantPaqServ.Text);
                                                }
                                                else if (numeroCantidadProducto > 0)
                                                {
                                                    numeroCantidadProducto = Convert.ToDecimal(txtCantPaqServ.Text);
                                                }
                                                datos[4] = Convert.ToString(numeroCantidadProducto);
                                                cn.EjecutarConsulta(cs.GuardarProductosServPaq(datos));
                                            }

                                            // recorrido para FlowLayoutPanel2 para ver cuantos TextBox
                                            //if (ProductosDeServicios.Count > 0 || ProductosDeServicios.Count == 0)
                                            //{
                                            //ProductosDeServicios.Clear();

                                            //if (flowLayoutPanel2.Controls.Count > 0)
                                            //{
                                            //    // recorrido del panel de Prodcutos de Productos para ver cuantos Productos fueron seleccionados
                                            //    foreach (Control panel in flowLayoutPanel2.Controls.OfType<FlowLayoutPanel>())
                                            //    {
                                            //        // agregamos la variable para egregar los procutos
                                            //        string prodSerPaq = null;
                                            //        DataTable dtProductos;
                                            //        foreach (Control item in panel.Controls)
                                            //        {
                                            //            string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                            //            if (item is ComboBox)
                                            //            {
                                            //                if (item.Text != "Por favor selecciona un Producto")
                                            //                {
                                            //                    string buscar = null;
                                            //                    string comboBoxText = item.Text;
                                            //                    string comboBoxValue = null;
                                            //                    buscar = $"SELECT ID, Nombre FROM Productos WHERE Nombre = '{comboBoxText}' AND IDUsuario = '{FormPrincipal.userID}'";
                                            //                    dtProductos = cn.CargarDatos(buscar);
                                            //                    comboBoxValue = dtProductos.Rows[0]["ID"].ToString();
                                            //                    prodSerPaq += fech + "|";
                                            //                    prodSerPaq += idProducto + "|";
                                            //                    prodSerPaq += comboBoxValue + "|";
                                            //                    prodSerPaq += comboBoxText + "|";
                                            //                }
                                            //            }
                                            //            if (item is TextBox)
                                            //            {
                                            //                var tb = item.Text;
                                            //                if (item.Text == "0")
                                            //                {
                                            //                    tb = "0";
                                            //                    prodSerPaq += tb;
                                            //                }
                                            //                else
                                            //                {
                                            //                    prodSerPaq += tb;
                                            //                }
                                            //            }
                                            //        }
                                            //        ProductosDeServicios.Add(prodSerPaq);
                                            //        prodSerPaq = null;
                                            //    }
                                            //    //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                                            //    if (ProductosDeServicios.Any())
                                            //    {
                                            //        string queryBorrarProductosDeServicios = $"DELETE FROM ProductosDeServicios WHERE IDServicio = '{idProductoBuscado}'";
                                            //        cn.EjecutarConsulta(queryBorrarProductosDeServicios);
                                            //        foreach (var productosSP in ProductosDeServicios)
                                            //        {
                                            //            string[] tmp = productosSP.Split('|');
                                            //            if (tmp.Length == 5)
                                            //            {
                                            //                cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                                            //            }
                                            //        }
                                            //        ProductosDeServicios.Clear();
                                            //    }
                                            //}
                                            //if (flowLayoutPanel2.Controls.Count == 0)
                                            //{
                                            //    if (ProductosDeServicios.Count.Equals(0))
                                            //    {
                                            //        string[] datos = new string[5];
                                            //        string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                            //        datos[0] = fech;
                                            //        datos[1] = idProducto.ToString();
                                            //        datos[2] = "0";
                                            //        datos[3] = string.Empty;
                                            //        datos[4] = txtCantPaqServ.Text;
                                            //        cn.EjecutarConsulta(cs.GuardarProductosServPaq(datos));
                                            //    }
                                            //}
                                            //}
                                            //    flowLayoutPanel2.Controls.Clear();
                                        }
                                        #endregion Final Seccion de Agregar desde Boton Producto y de Editar Producto
                                        #region  Inicio Seccion de Agregar desde XML
                                        if (DatosSourceFinal == 3)
                                        {
                                            idHistorialCompraProducto = 0;
                                            found = 10;
                                            fechaXML = FechaXMLNvoProd;
                                            fecha = fechaXML.Substring(0, found);
                                            hora = fechaXML.Substring(found + 1);
                                            fechaCompleta = fecha + " " + hora;
                                            folio = FolioXMLNvoProd;
                                            RFCEmisor = RFCXMLNvoProd;
                                            nombreEmisor = NobEmisorXMLNvoProd;
                                            claveProdEmisor = ClaveProdEmisorXMLNvoProd;
                                            descuentoXML = DescuentoXMLNvoProd;
                                            PrecioCompraXMLNvoProd = txtPrecioCompra.Text;

                                            //Se obtiene la ID del último producto agregado
                                            idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));

                                            try
                                            {
                                                cn.EjecutarConsulta(cs.GuardarDetallesDelProducto(idProducto, FormPrincipal.userID, nameProveedorXML.ToString(), Convert.ToInt32(idProveedorXML.ToString())));
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al Agregar Detalles del " + FuenteServPaq + " Basico\n" + origen, "Error Agregar " + FuenteServPaq, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }

                                            //string query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto,IDUsuario) VALUES('{nombre}','{stock}','{precioOriginalConIVA.ToString("N2")}','{descuentoXML}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}', datetime('now', 'localtime'), '{Inventario.idReporte}', '{idProducto}','{FormPrincipal.userID}')";

                                            //string query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor,IDProducto,IDUsuario) VALUES('{nombre}','{stock}','{precioOriginalConIVA.ToString("N2")}','{descuentoXML}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}','{idProducto}','{FormPrincipal.userID}')";

                                            string query = $@"INSERT INTO HistorialCompras(Concepto, Cantidad, ValorUnitario, Descuento, Precio, FechaLarga, Folio, RFCEmisor, NomEmisor, ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto, IDUsuario) VALUES('{nombre}','{stock}','{PrecioCompraXMLNvoProd}','{descuentoXML}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{Inventario.idReporte}','{idProducto}','{FormPrincipal.userID}')";

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

                                            //if (ProductosDeServicios.Count >= 1 || ProductosDeServicios.Count == 0)
                                            //{
                                            //    ProductosDeServicios.Clear();
                                            //    // recorrido del panel de Prodcutos de Productos para ver cuantos Productos fueron seleccionados
                                            //    foreach (Control panel in flowLayoutPanel2.Controls.OfType<FlowLayoutPanel>())
                                            //    {
                                            //        // agregamos la variable para egregar los procutos
                                            //        string prodSerPaq = null;
                                            //        DataTable dtProductos;
                                            //        foreach (Control item in panel.Controls)
                                            //        {
                                            //            string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                            //            if (item is ComboBox)
                                            //            {
                                            //                if (item.Text != "Por favor selecciona un Producto")
                                            //                {
                                            //                    string buscar = null;
                                            //                    string comboBoxText = item.Text;
                                            //                    string comboBoxValue = null;
                                            //                    buscar = $"SELECT ID, Nombre FROM Productos WHERE Nombre = '{comboBoxText}' AND IDUsuario = '{FormPrincipal.userID}' AND Status = '1'";
                                            //                    dtProductos = cn.CargarDatos(buscar);
                                            //                    DataRow row = dtProductos.Rows[0];
                                            //                    comboBoxValue = row["ID"].ToString();
                                            //                    prodSerPaq += fech + "|";
                                            //                    prodSerPaq += idProducto + "|";
                                            //                    prodSerPaq += comboBoxValue + "|";
                                            //                    prodSerPaq += comboBoxText + "|";
                                            //                }
                                            //            }
                                            //            if (item is TextBox)
                                            //            {
                                            //                var tb = item.Text;
                                            //                if (item.Text == "0")
                                            //                {
                                            //                    tb = "0";
                                            //                    prodSerPaq += tb;
                                            //                }
                                            //                else
                                            //                {
                                            //                    prodSerPaq += tb;
                                            //                }
                                            //            }
                                            //        }
                                            //        ProductosDeServicios.Add(prodSerPaq);
                                            //        prodSerPaq = null;
                                            //    }

                                            //    //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                                            //    if (ProductosDeServicios.Any())
                                            //    {
                                            //        foreach (var productosSP in ProductosDeServicios)
                                            //        {
                                            //            string[] tmp = productosSP.Split('|');
                                            //            if (tmp.Length == 5)
                                            //            {
                                            //                cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                                            //            }
                                            //        }
                                            //        ProductosDeServicios.Clear();
                                            //    }
                                            //}
                                            //flowLayoutPanel2.Controls.Clear();

                                            using (DataTable dtProdDeServComb = cn.CargarDatos(cs.ProductosDeServicios(idProducto)))
                                            {
                                                if (!dtProdDeServComb.Rows.Count.Equals(0))
                                                {
                                                    foreach (DataRow drProdServComb in dtProdDeServComb.Rows)
                                                    {
                                                        var cantidadProducto = Convert.ToDecimal(drProdServComb["Cantidad"].ToString());
                                                        var NoProducto = Convert.ToInt32(drProdServComb["IDProducto"].ToString());
                                                        try
                                                        {
                                                            if (!this.Text.Equals("AGREGAR COMBOS"))
                                                            {
                                                                cn.EjecutarConsulta(cs.actualizarStockProdServCombo(cantidadProducto, NoProducto));
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            MessageBox.Show("Advertencia en el proceso de aumento de Stock de producto relacionado", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }
                                                        finally
                                                        {
                                                            cantidadProducto = 0;
                                                            NoProducto = 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        #endregion  Final Seccion de Agregar desde XML

                                        //Se obtiene la ID del último producto agregado
                                        idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));

                                        #region Inicio Sección proceso para guardar los detalles de facturación del producto
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
                                        if (datosImpuestos == null & DatosSourceFinal == 3 & AgregarStockXML.list_impuestos_traslado_retenido.Count() > 0)
                                        {
                                            guardar_impuestos_dexml(Convert.ToDouble(baseProducto), idProducto);
                                        }

                                        #endregion Final Sección proceso para guardar los detalles de facturación del producto

                                        #region Inicio Sección proceso para guardar el descuento del producto en caso de que se haya agregado uno
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
                                        #endregion Final Sección proceso para guardar el descuento del producto en caso de que se haya agregado uno
                                    }

                                    #region Inicio Sección Código de Barras Extras
                                    //// recorrido para FlowLayoutPanel para ver cuantos TextBox
                                    //foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
                                    //{
                                    //    // hacemos un objeto para ver que tipo control es
                                    //    foreach (Control item in panel.Controls)
                                    //    {
                                    //        // ver si el control es TextBox
                                    //        if (item is TextBox)
                                    //        {
                                    //            var tb = item.Text;         // almacenamos en la variable tb el texto de cada TextBox
                                    //            codigosBarrras.Add(tb);     // almacenamos en el List los codigos de barras
                                    //        }
                                    //    }
                                    //}
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
                                    #endregion Final Sección Código de Barras Extras

                                    if (this.Text.Trim().Equals("AGREGAR COMBOS"))
                                    {
                                        titulo = "Combo Guardado";
                                        texto = "Se guardo exitosamente el combo....";
                                        SeleccionaImagen = "correcto";
                                    }
                                    else if (this.Text.Trim().Equals("EDITAR COMBOS"))
                                    {
                                        titulo = "Combo Actualizado"; 
                                        texto = "Se actualizo exitosamente el combo....";
                                        SeleccionaImagen = "correcto";
                                    }
                                    else if (this.Text.Trim().Equals("COPIAR COMBOS"))
                                    {
                                        titulo = "Combo Copiado";
                                        texto = "Se copio exitosamente el combo....";
                                        SeleccionaImagen = "correcto";
                                    }
                                    else if (this.Text.Trim().Equals("AGREGAR SERVICIOS"))
                                    {
                                        titulo = "Servicio Agregado";
                                        texto = "Se guardo exitosamente el servicio....";
                                        SeleccionaImagen = "correcto";
                                    }
                                    else if (this.Text.Trim().Equals("EDITAR SERVICIOS"))
                                    {
                                        titulo = "Servicio Actualizado";
                                        texto = "Se actualizo exitosamente el servicio....";
                                        SeleccionaImagen = "correcto";
                                    }
                                    else if (this.Text.Trim().Equals("COPIAR SERVICIOS"))
                                    {
                                        titulo = "Servicio Copiado";
                                        texto = "Se copio exitosamente el servicio....";
                                        SeleccionaImagen = "correcto";
                                    }

                                    if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
                                    {
                                        
                                        MensajeAgregarEditarCopiarProducto MAECP = new MensajeAgregarEditarCopiarProducto(titulo, texto,SeleccionaImagen);
                                        MAECP.ShowDialog();
                                        //MessageBoxTemporal.Show(tituloVentana, "Aviso del Sistema", 2, false);
                                    }
                                    else if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
                                    {
                                        MensajeAgregarEditarCopiarProducto MAECP = new MensajeAgregarEditarCopiarProducto(titulo, texto,SeleccionaImagen);
                                        MAECP.ShowDialog();
                                      //  MessageBoxTemporal.Show(tituloVentana, "Aviso del Sistema", 2, false);
                                    }

                                    //Cierra la ventana donde se agregan los datos del producto
                                    this.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al Agregar " + FuenteServPaq + "\n" + origen + ex.Message, "Error Agregar " + FuenteServPaq, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                #endregion Final de guardado de los datos principales del Servicios o Combos
                            }
                            else if (!bValidClaveInterna || !bValidCodigoBarras)
                            {
                                errorProvAgregarEditarProducto.SetError(txtClaveProducto, "Debe tener una Clave Interna\npara poder continuar el proceso.");
                                errorProvAgregarEditarProducto.SetError(txtCodigoBarras, "Debe tener un Código de Barras\npara poder continuar el proceso.");
                                MessageBox.Show("La Clave Interna ó Código de Barras\nNO DEBE ESTAR EN BLANCO", "Advertencia de llenado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            if (!bValidNombreProducto)
                            {
                                MessageBox.Show("Por favor poner Datos Validos\npara el campo de NOMBRE DE PRODUCTO", "Alerta del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtNombreProducto.Select(0, txtNombreProducto.Text.Length);
                                txtNombreProducto.Focus();
                            }
                            else if (!bValidPrecioProducto)
                            {
                                MessageBox.Show("Por favor poner Datos Validos\npara el campo de PRECIO DE VENTA", "Alerta del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtPrecioProducto.Select(0, txtPrecioProducto.Text.Length);
                                txtPrecioProducto.Focus();
                            }
                        }
                    }
                    #endregion Final de Sección de Combos y Servicios
                    #region Inicio de Sección de si NO ES Producto Combos y Servicios
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error al intentar registrar el producto", "Error de tipo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion Final de Sección de si NO ES Producto Combos y Servicios
                    /*	Fin del codigo de Alejandro	*/
                }
            }
            #endregion Final Sección De Agregar Producto, Combo ó Servicio Desde XML / Botón manual
            #region Inicio Sección De Editar Producto
            else if (DatosSourceFinal == 2)
            {
                if (SearchProdResult.Rows.Count != 0)
                {
                    // Comprobar precio del producto para saber si se edito
                    var precioTmp = cn.BuscarProducto(Convert.ToInt32(idProductoBuscado), FormPrincipal.userID);
                    var precioNuevo = float.Parse(precio);
                    var precioAnterior = float.Parse(precioTmp[2]);

                    if (precioNuevo != precioAnterior)
                    {
                        var respuesta = MessageBox.Show("El precio esta siendo modificado, los descuentos vinculados a este producto/servicio/combo serán eliminados por lo que\nserá necesario agregarlos nuevamente.", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        
                        if (respuesta == DialogResult.Yes)
                        {
                            cn.EjecutarConsulta($"DELETE FROM DescuentoCliente WHERE IDProducto = {idProductoBuscado}");
                            cn.EjecutarConsulta($"DELETE FROM DescuentoMayoreo WHERE IDProducto = {idProductoBuscado}");
                            descuentos.Clear();
                        }

                        if (respuesta == DialogResult.No)
                        {
                            return;
                        }
                    }

                    bool bValidNombreProducto = ValidateNombreProducto();
                    bool bValidPrecioProducto = ValidatePrecioProducto();

                    if (bValidNombreProducto && bValidPrecioProducto)
                    {
                        bool bValidClaveInterna = ValidateClaveProducto();
                        bool bValidCodigoBarras = ValidateCodigoBarras();

                        if (bValidClaveInterna || bValidCodigoBarras)
                        {
                            errorProvAgregarEditarProducto.SetError(txtClaveProducto, "");
                            errorProvAgregarEditarProducto.SetError(txtCodigoBarras, "");

                            #region Inicio existencia de codigo de barra al actualizar
                            // Verificar existencia de codigo de barra al actualizar
                            if (mb.ComprobarCodigoClave(codigoB, FormPrincipal.userID, Convert.ToInt32(idProductoBuscado)))
                            {
                                MessageBox.Show($"El número de identificación {codigoB}\nya se esta utilizando como clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            #endregion Final existencia de codigo de barra al actualizar

                            #region Inicio existencia de clave interna al actualizar
                            // Verificar existencia de codigo de barra al actualizar
                            if (mb.ComprobarCodigoClave(claveIn, FormPrincipal.userID, Convert.ToInt32(idProductoBuscado)))
                            {
                                MessageBox.Show($"El número de identificación {claveIn}\nya se esta utilizando como clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            #endregion Final existencia de clave interna al actualizar

                            #region Inicio Codigo de barras extras
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

                            // Verificar si los codigos de barra extra ya existen al actualizar producto, servicio o paquete
                            if (codigosBarrras != null || codigosBarrras.Count != 0)
                            {
                                for (int pos = 0; pos < codigosBarrras.Count; pos++)
                                {
                                    var existe = mb.ComprobarCodigoClave(codigosBarrras[pos], FormPrincipal.userID, Convert.ToInt32(idProductoBuscado));

                                    if (existe)
                                    {
                                        MessageBox.Show($"El número de identificación {codigosBarrras[pos]}\nya se esta utilizando como clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        codigosBarrras.Clear();
                                        return;
                                    }
                                }
                            }

                            //label10.Text = idProductoBuscado;
                            if (SearchCodBarExtResult.Rows.Count != 0)
                            {
                                string deleteCodBarExt = $"DELETE FROM CodigoBarrasExtras WHERE IDProducto = '{idProductoBuscado}'";
                                cn.EjecutarConsulta(deleteCodBarExt);
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
                            #endregion Final Codigo de barras extras

                            var empleado = "0";

                            #region Incio Seccion Cambio de Precio
                            if (precioNuevo != precioAnterior)
                            {
                                string precioAnteriorTmp = precioAnterior.ToString();
                                string precioNuevoTmp = precioNuevo.ToString();

                                precioAnteriorTmp = precioAnteriorTmp.Replace(",", "");
                                precioNuevoTmp = precioNuevoTmp.Replace(",", "");

                                if (FormPrincipal.userNickName.Contains('@'))
                                {
                                    empleado = cs.buscarIDEmpleado(FormPrincipal.userNickName);
                                }

                                var datos = new string[] {
                                    FormPrincipal.userID.ToString(), empleado, idProductoBuscado,
                                    precioAnteriorTmp, precioNuevoTmp,
                                    "EDITAR PRODUCTO", fechaOperacion
                                };

                                // Se guarda historial del cambio de precio
                                cn.EjecutarConsulta(cs.GuardarHistorialPrecios(datos));

                                // Ejecutar hilo para enviar notificacion
                                var datosConfig = mb.ComprobarConfiguracion();

                                if (datosConfig.Count > 0)
                                {
                                    if (Convert.ToInt16(datosConfig[0]) == 1)
                                    {
                                        var configProducto = mb.ComprobarCorreoProducto(Convert.ToInt32(idProductoBuscado));

                                        if (configProducto.Count > 0)
                                        {
                                            if (configProducto[0] == 1)
                                            {
                                                datos = new string[] {
                                                    nombre, precioAnterior.ToString("N2"),
                                                    precioNuevo.ToString("N2"), "editar producto"
                                                };

                                                Thread notificacion = new Thread(
                                                    () => Utilidades.CambioPrecioProductoEmail(datos)
                                                );

                                                notificacion.Start();
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion Final Seccion Cambio de Precio

                            #region Inicio Seccion Descuentos
                            if (descuentos.Any())
                            {
                                if (FormAgregar != null)
                                {
                                    FormAgregar.Close();
                                }
                                
                                tipoDescuento = descuentos[0];
                            }

                            //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                            if (descuentos.Any())
                            {
                                // Se borra de las dos tablas en caso de que haya tenido un tipo de descuento diferente al que se actualizo
                                string deleteDescuentoCLiente = $"DELETE FROM DescuentoCliente WHERE IDProducto = '{idProductoBuscado}'";
                                cn.EjecutarConsulta(deleteDescuentoCLiente);

                                string deleteDescuentoMayoreo = $"DELETE FROM DescuentoMayoreo WHERE IDProducto = '{idProductoBuscado}'";
                                cn.EjecutarConsulta(deleteDescuentoMayoreo);

                                //Descuento por Cliente
                                if (descuentos[0] == "1")
                                {
                                    string[] guardar = new string[] { descuentos[1], descuentos[2], descuentos[3], descuentos[4] };
                                    cn.EjecutarConsulta(cs.GuardarDescuentoCliente(guardar, Convert.ToInt32(idProductoBuscado)));
                                }
                                //Descuento por Mayoreo
                                if (descuentos[0] == "2")
                                {
                                    foreach (var descuento in descuentos)
                                    {
                                        if (descuento == "2") { continue; }

                                        string[] tmp = descuento.Split('-');

                                        cn.EjecutarConsulta(cs.GuardarDescuentoMayoreo(tmp, Convert.ToInt32(idProductoBuscado)));
                                    }
                                }
                            }
                            #endregion Final Seccion Descuentos

                            if (!(this.Text.Trim() == "AGREGAR PRODUCTO" | this.Text.Trim() == "EDITAR PRODUCTO" | this.Text.Trim() == "COPIAR PRODUCTO"))
                            {
                                stock = "0";
                            }

                            queryUpdateProd = $@"UPDATE Productos SET Nombre = '{nombre}', 
                                                Stock = '{stock}', Precio = '{precio}', Categoria = '{categoria}',
                                                TipoDescuento = '{tipoDescuento}', ClaveInterna = '{claveIn}', 
                                                CodigoBarras = '{codigoB}', ClaveProducto = '{claveProducto}', 
                                                UnidadMedida = '{claveUnidadMedida}', ProdImage = '{logoTipo}',
                                                NombreAlterno1 = '{mg.RemoverCaracteres(nombre)}', 
                                                NombreAlterno2 = '{mg.RemoverPreposiciones(nombre)}', 
                                                StockNecesario = '{stockNecesario}', StockMinimo = '{stockMinimo}',
                                                PrecioMayoreo = '{precioMayoreo}'
                                                WHERE ID = '{idProductoBuscado}' AND IDUsuario = {FormPrincipal.userID}";

                            respuesta = cn.EjecutarConsulta(queryUpdateProd);

                            claveProducto = string.Empty;
                            claveUnidadMedida = string.Empty;


                            //  MIRI.
                            // °°°°°°°

                            if (AgregarDetalleFacturacionProducto.editado == true)
                            {
                                // Modifica tabla producto
                                string edit_p = $@"UPDATE Productos SET Base='{AgregarDetalleFacturacionProducto.tmp_edit_base}',
                                                IVA='{AgregarDetalleFacturacionProducto.tmp_edit_IVA}',
                                                Impuesto='{AgregarDetalleFacturacionProducto.tmp_edit_impuesto}'
                                                WHERE ID='{idProductoBuscado}' AND IDUsuario={FormPrincipal.userID}";

                                cn.EjecutarConsulta(edit_p);

                                AgregarDetalleFacturacionProducto.tmp_edit_base = 0;
                                AgregarDetalleFacturacionProducto.tmp_edit_IVA = 0;
                                AgregarDetalleFacturacionProducto.tmp_edit_impuesto = "";

                                // Busca si antes de la edición tenia impuestos extras, si es así entonces, los eliminará. 
                                bool existen_imp_extra = (bool)cn.EjecutarSelect($"SELECT * FROM detallesfacturacionproductos WHERE IDProducto='{idProductoBuscado}'", 0);

                                if (existen_imp_extra == true)
                                {
                                    int elimina_imp_extras = cn.EjecutarConsulta($"DELETE FROM detallesfacturacionproductos WHERE IDProducto= '{idProductoBuscado}'");
                                }

                                // Comprueba si hay impuestos extra, de ser así se procede al guardado.  
                                if (datosImpuestos != null)
                                {
                                    guardarDatosImpuestos(2);
                                }
                            }

                            /*//Se obtiene la ID del último producto agregado
                                        idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));
                                        var claveP = txtClaveProducto.Text;
                                        #region Inicio de Datos de Impuestos
                                        //Se realiza el proceso para guardar los detalles de facturación del producto
                                        if (datosImpuestos != null)
                                        {
                                            guardarDatosImpuestos();
                                        }*/


                            #region Inicio De Detalle Producto Basicos
                            bool isEmpty = !detalleProductoBasico.Any();

                            if (!isEmpty)
                            {
                                // Para guardar los detalles del producto
                                // Ejemplo: Proveedor, Categoria, Ubicacion, etc.
                                guardar = detalleProductoBasico.ToArray();
                                guardar[0] = idProductoBuscado.ToString();
                                cn.EjecutarConsulta(cs.GuardarProveedorDetallesDelProducto(guardar));
                            }
                            #endregion Final De Detalle Producto Basicos

                            #region Inicio de Seccion Productos
                            if (this.Text.Trim() == "AGREGAR PRODUCTO" | this.Text.Trim() == "EDITAR PRODUCTO" | this.Text.Trim() == "COPIAR PRODUCTO")
                            {
                                // Agregar la relacion de producto ya registrado con Combo Servicio
                                #region Agregar a tabla productosdeservicios
                                if (listaProductoToCombo.Count().Equals(1))
                                {
                                    if (!listaProductoToCombo[0].ToString().Equals(string.Empty))
                                    {
                                        if (this.Text.Trim() == "EDITAR PRODUCTO")
                                        {
                                            string[] datos = new string[5];
                                            foreach (var item in listaProductoToCombo)
                                            {
                                                datos = item.Split('|');
                                                string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                datos[0] = fech.Trim();
                                                string[] nuevosDatos = { datos[0], datos[1], datos[3], datos[4], datos[5] };
                                                cn.EjecutarConsulta(cs.insertarProductosServicios(nuevosDatos));
                                            }
                                            using (DataTable dtProductosDeServicios = cn.CargarDatos(cs.ObtenerProductosServPaq(datos[1].ToString())))
                                            {
                                                if (dtProductosDeServicios.Rows.Count > 1)
                                                {
                                                    cn.EjecutarConsulta(cs.borrarProdRelBlanco(Convert.ToInt32(datos[1].ToString())));
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                if (!CBNombProd.Equals("") || !CBIdProd.Equals(""))
                                {
                                    DateTime today = DateTime.Now;
                                    DataTable dtSearchServPaq;
                                    DataRow rowServPaq;
                                    dtSearchServPaq = cn.CargarDatos(cs.ProductosDeServicios(Convert.ToInt32(CBIdProd)));

                                    if (dtSearchServPaq.Rows.Count > 0)
                                    {
                                        rowServPaq = dtSearchServPaq.Rows[0];
                                        string[] tmp = { $"{today.ToString("yyyy-MM-dd hh:mm:ss")}", $"{CBIdProd}", $"{idProductoBuscado}", $"{nombre}", $"{rowServPaq["Cantidad"].ToString()}" };

                                        try
                                        {
                                            cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Algo ocurrio al tratar de guardar Productos que contienen los Combos/Servicios\n" + ex.Message.ToString(), "Advertencia del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }

                                        //string DeleteProdAtService = $"DELETE FROM ProductosDeServicios WHERE IDServicio = '{CBIdProd}' AND (IDProducto = '' AND NombreProducto = '')";
                                        //int DeleteProdAtPQS = cn.EjecutarConsulta(DeleteProdAtService);
                                        //if (DeleteProdAtPQS > 0)
                                        //{
                                        //MessageBox.Show("Productos Agregado al Paquete o Servicio", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //}
                                        //else
                                        //{
                                        //MessageBox.Show("Algo salio mal al intentar Agregar el\nProducto al Paquete o Servicio", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //}
                                    }

                                    // Limpiar para evitar error de relacionar producto a servicio 
                                    // y despues editar cualquier producto cualquiera
                                    CBIdProd = string.Empty;
                                    CBNombProd = string.Empty;
                                }
                            }
                            #endregion Final de Seccion Productos

                            if (this.Text.Trim().Equals("AGREGAR PRODUCTO"))
                            {
                                titulo = "Producto Agregado";
                                texto = "Se guardo exitosamente el producto....";
                                SeleccionaImagen = "correcto";
                            }
                            else if (this.Text.Trim().Equals("EDITAR PRODUCTO"))
                            {
                                titulo = "Producto Actualizado";
                                texto = "Se actualizo exitosamente el producto....";
                                SeleccionaImagen = "correcto";
                            }
                            else if (this.Text.Trim().Equals("COPIAR PRODUCTO"))
                            {
                                titulo = "Producto Copiado";
                                texto = "Se copio exitosamente el producto....";
                                SeleccionaImagen = "correcto";
                            }

                            #region Inicio de Seccion Combos y Servicios
                            else if (this.Text.Trim() == "AGREGAR COMBOS" | this.Text.Trim() == "EDITAR COMBOS" | this.Text.Trim() == "COPIAR COMBOS" || this.Text.Trim() == "AGREGAR SERVICIOS" | this.Text.Trim() == "EDITAR SERVICIOS" | this.Text.Trim() == "COPIAR SERVICIOS")
                            {
                                using (DataTable dtComboServicio = cn.CargarDatos(cs.ObtenerProductosServPaq(idProductoBuscado)))
                                {
                                    if (!dtComboServicio.Rows.Count.Equals(0))
                                    {
                                        foreach (DataRow item in dtComboServicio.Rows)
                                        {
                                            cn.EjecutarConsulta(cs.actualizarCantidadRelacionProdComboServicio(Convert.ToInt32(item["IDServicio"].ToString()), (float)Convert.ToDecimal(txtCantPaqServ.Text)));
                                        }
                                    }
                                }

                                if (ProductosDeServicios.Count() > 0)
                                {
                                    string tipoAux = string.Empty;
                                    bool tipoValido = false;

                                    if (this.Text.Trim() == "AGREGAR SERVICIOS" || this.Text.Trim() == "EDITAR SERVICIOS" || this.Text.Trim() == "COPIAR SERVICIOS")
                                    {
                                        tipoAux = "servicio";
                                        tipoValido = true;
                                    }

                                    if (this.Text.Trim() == "AGREGAR COMBOS" || this.Text.Trim() == "EDITAR COMBOS" || this.Text.Trim() == "COPIAR COMBOS")
                                    {
                                        tipoAux = "combo";
                                        tipoValido = true;
                                    }

                                    if (tipoValido)
                                    {
                                        if (Convert.ToDecimal(txtCantPaqServ.Text) <= 0)
                                        {
                                            MessageBox.Show($"Ingrese una cantidad para el campo \"Cantidad por {tipoAux}\"", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            txtCantPaqServ.Focus();
                                            return;
                                        }
                                    }
                                }

                                // recorrido para FlowLayoutPanel2 para ver cuantos TextBox
                                if (ProductosDeServicios.Count >= 1 || ProductosDeServicios.Count == 0)
                                {
                                    if (!ProductosDeServicios.Count.Equals(0))
                                    {
                                        foreach (var item in ProductosDeServicios)
                                        {
                                            var datos = item.Split('|');
                                            cn.EjecutarConsulta(cs.GuardarProductosServPaq(datos));
                                        }
                                    }
                                    //ProductosDeServicios.Clear();
                                    //// recorrido del panel de Prodcutos de Productos para ver cuantos Productos fueron seleccionados
                                    //foreach (Control panel in flowLayoutPanel2.Controls.OfType<FlowLayoutPanel>())
                                    //{
                                    //    // agregamos la variable para egregar los procutos
                                    //    string prodSerPaq = null;
                                    //    DataTable dtProductos;
                                    //    foreach (Control item in panel.Controls)
                                    //    {
                                    //        string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    //        string buscar = string.Empty;
                                    //        string comboBoxText = item.Text;
                                    //        string comboBoxValue = string.Empty;

                                    //        if (item is ComboBox)
                                    //        {
                                    //            if (item.Text != "Por favor selecciona un Producto")
                                    //            {
                                    //                if (!comboBoxText.Equals(string.Empty))
                                    //                {
                                    //                    buscar = $"SELECT ID, Nombre FROM Productos WHERE Nombre = '{comboBoxText}' AND IDUsuario = '{FormPrincipal.userID}' AND Status = '1'";
                                    //                    dtProductos = cn.CargarDatos(buscar);
                                    //                    comboBoxValue = dtProductos.Rows[0]["ID"].ToString();
                                    //                    prodSerPaq += fech + "|";
                                    //                    prodSerPaq += idProductoBuscado + "|";
                                    //                    prodSerPaq += comboBoxValue + "|";
                                    //                    prodSerPaq += comboBoxText + "|";
                                    //                }
                                    //            }
                                    //        }
                                    //        if (item is TextBox)
                                    //        {
                                    //            var tb = item.Text;
                                    //            if (item.Text == "0")
                                    //            {
                                    //                tb = "0";
                                    //                prodSerPaq += tb;
                                    //            }
                                    //            else
                                    //            {
                                    //                prodSerPaq += tb;
                                    //            }
                                    //        }
                                    //    }
                                    //    ProductosDeServicios.Add(prodSerPaq);
                                    //    prodSerPaq = null;
                                    //}
                                }
                                //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                                if (ProductosDeServicios.Any())
                                {
                                    string queryBorrarProductosDeServicios = $"DELETE FROM ProductosDeServicios WHERE IDServicio = '{idProductoBuscado}' AND IDProducto = 0 AND NombreProducto = ''";
                                    cn.EjecutarConsulta(queryBorrarProductosDeServicios);
                                    foreach (var productosSP in ProductosDeServicios)
                                    {
                                        string[] tmp = productosSP.Split('|');
                                        if (tmp.Length == 5)
                                        {
                                            using (DataTable dtProdComboServ = cn.CargarDatos(cs.verSiExisteRelacionRegistrada(tmp[1], tmp[2])))
                                            {
                                                if (dtProdComboServ.Rows.Count.Equals(0))
                                                {
                                                    cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                                                }
                                            }
                                        }
                                    }
                                    ProductosDeServicios.Clear();
                                }
                                flowLayoutPanel2.Controls.Clear();

                                if (this.Text.Trim().Equals("AGREGAR COMBOS"))
                                {
                                    titulo = "Combo Guardado";
                                    texto = "Se guardo exitosamente el combo....";
                                    SeleccionaImagen = "correcto";
                                }
                                else if (this.Text.Trim().Equals("EDITAR COMBOS"))
                                {
                                    titulo = "Combo Actualizado";
                                    texto = "Se actualizo exitosamente el combo....";
                                    SeleccionaImagen = "correcto";
                                }

                                else if (this.Text.Trim().Equals("COPIAR COMBOS"))
                                {
                                    titulo = "Combo Copiado";
                                    texto = "Se copio exitosamente el combo....";
                                    SeleccionaImagen = "correcto";
                                }
                                else if (this.Text.Trim().Equals("AGREGAR SERVICIOS"))
                                {
                                    titulo = "Servicio Agregado";
                                    texto = "Se guardo exitosamente el servicio....";
                                    SeleccionaImagen = "correcto";
                                }
                                else if (this.Text.Trim().Equals("EDITAR SERVICIOS"))
                                {
                                    titulo = "Servicio Actualizado";
                                    texto = "Se actualizo exitosamente el servicio....";
                                    SeleccionaImagen = "correcto";
                                }
                                else if (this.Text.Trim().Equals("COPIAR SERVICIOS"))
                                {
                                    titulo = "Servicio Copiado";
                                    texto = "Se copio exitosamente el servicio....";
                                    SeleccionaImagen = "correcto";
                                }
                            }
                            #endregion Final de Seccion Combos y Servicios

                            if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
                            {
                                MensajeAgregarEditarCopiarProducto MAECP = new MensajeAgregarEditarCopiarProducto(titulo, texto, SeleccionaImagen);
                                MAECP.ShowDialog();
                                //MessageBoxTemporal.Show(tituloVentana, "Aviso del Sistema", 2, false);
                            }
                            else if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
                            {
                                MensajeAgregarEditarCopiarProducto MAECP = new MensajeAgregarEditarCopiarProducto(titulo, texto, SeleccionaImagen);
                                MAECP.ShowDialog();
                                //MessageBoxTemporal.Show(tituloVentana, "Aviso del Sistema", 2, false);
                            }

                            // Cierra la ventana donde se agregan los datos del producto
                            this.Close();
                        }
                        else if (!bValidClaveInterna || !bValidCodigoBarras)
                        {
                            errorProvAgregarEditarProducto.SetError(txtClaveProducto, "Debe tener una Clave Interna\npara poder continuar el proceso.");
                            errorProvAgregarEditarProducto.SetError(txtCodigoBarras, "Debe tener un Código de Barras\npara poder continuar el proceso.");
                            MessageBox.Show("La Clave Interna ó Código de Barras\nNO DEBE ESTAR EN BLANCO", "Advertencia de llenado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        if (!bValidNombreProducto)
                        {
                            MessageBox.Show("Por favor poner Datos validos\npara el campo de NOMBRE DE PRODUCTO", "Alerta del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtNombreProducto.Select(0, txtNombreProducto.Text.Length);
                        }
                        else if (!bValidPrecioProducto)
                        {
                            MessageBox.Show("Por favor poner Datos validos\npara el campo de PRECIO DE VENTA", "Alerta del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtNombreProducto.Select(0, txtNombreProducto.Text.Length);
                        }
                    }
                }
            }
            #endregion Final Sección De Editar Producto
            #region Inicio Sección De Copiado Producto
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
                    #region Inicio de Inicializacion de Variables
                    /****************************
			        *	codigo de Alejandro		*
			        ****************************/
                    nombreNvoInsert = txtNombreProducto.Text;
                    stockNvoInsert = txtStockProducto.Text;
                    valorDePrecioVenta = Convert.ToDouble(txtPrecioProducto.Text);
                    precioNvoInsert = valorDePrecioVenta.ToString();
                    categoriaNvoInsert = txtCategoriaProducto.Text;
                    claveInNvoInsert = txtClaveProducto.Text.Trim();
                    codigoBNvoInsert = txtCodigoBarras.Text.Trim();
                    tipoDescuentoNvoInsert = "0";
                    idUsrNvoInsert = FormPrincipal.userID.ToString();
                    tipoProdNvoInsert = "";

                    if (cbTipo.Text == "Producto")
                    {
                        tipoProdNvoInsert = "P";
                    }
                    else
                    {
                        tipoProdNvoInsert = "S";
                    }
                    /*	Fin del codigo de Alejandro	*/
                    #endregion Final de Inicializacion de Variables

                    #region Inicio Busqueda Clave Interna
                    //Hacemos la busqueda que no se repita en CalveInterna
                    //searchClavIntProd();
                    if (mb.ComprobarCodigoClave(claveInNvoInsert, FormPrincipal.userID))
                    {
                        MessageBox.Show($"El número de identificación {claveIn}\nya se esta utilizando como clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                    #endregion Final Busqueda Clave Interna

                    #region Inicio Busqueda Codigo de Barras
                    //Hacemos la busqueda que no se repita en CodigoBarra
                    //searchCodBar();
                    if (mb.ComprobarCodigoClave(codigoBNvoInsert, FormPrincipal.userID))
                    {
                        MessageBox.Show($"El número de identificación {codigoB}\nya se esta utilizando como clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                    #endregion Final Busqueda Codigo de Barras

                    guardar = new string[] {
                        nombreNvoInsert, stockNvoInsert, precioNvoInsert, categoriaNvoInsert, claveInNvoInsert,
                        codigoBNvoInsert, claveProducto, claveUnidadMedida, tipoDescuentoNvoInsert, idUsrNvoInsert,
                        logoTipo, tipoProdNvoInsert, baseProducto, ivaProducto, impuestoProducto, mg.RemoverCaracteres(nombreNvoInsert),
                        mg.RemoverPreposiciones(nombreNvoInsert), stockNecesario, stockMinimo, txtPrecioCompra.Text, precioMayoreo
                    };

                    #region Inicio de Guardado de Producto
                    try
                    {
                        //Se guardan los datos principales del producto
                        int respuesta = cn.EjecutarConsulta(cs.GuardarProducto(guardar, FormPrincipal.userID));

                        if (respuesta > 0)
                        {
                            claveProducto = string.Empty;
                            claveUnidadMedida = string.Empty;

                            //Se obtiene la ID del último producto agregado
                            idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));

                            #region Inicio Sección de Datos de Impuestos
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
                            #endregion Final  Sección de Datos de Impuestos

                            #region Inicio de Sección de Descuentos
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
                            #endregion Final de Sección de Descuentos

                            #region Inicio De Codigo de Barras Extra
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
                            #endregion Final De Codigo de Barras Extra

                            #region Inicio de Sección de Productos Del Servicio ó Combo
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
                            #endregion Final de Sección de Productos Del Servicio ó Combo

                            //Cierra la ventana donde se agregan los datos del producto
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ha ocurrido un error al intentar registrar el producto\n" + ex.Message.ToString(), "Advertencia Copiar Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    #endregion Final de Guardado de Producto
                }
            }
            #endregion Final  Sección De Copiado Producto
            /* Fin del codigo de Emmanuel */

            listaProductoToCombo.Clear();
            ProductosDeServicios.Clear();
            AgregarEditarProducto.descuentos.Clear();

            listaProductoToCombo = new List<string>();
            ProductosDeServicios = new List<string>();
        }

        private void txtCantPaqServ_Validating(object sender, CancelEventArgs e)
        {
            ValidarCantidadDePaqueteServicio();
        }

        private bool ValidarCantidadDePaqueteServicio()
        {
            bool bStatus = true;
            decimal cantidadProductoCombosServicios = 0;

            if (Decimal.TryParse(txtCantPaqServ.Text, out cantidadProductoCombosServicios))
            {
                if (cantidadProductoCombosServicios.Equals(0))
                {
                    errorProvAgregarEditarProducto.SetError(txtCantPaqServ, "Debe tener una Cantidad Mayor a 0\npara poder continuar el proceso.");
                    txtCantPaqServ.Focus();
                    txtCantPaqServ.Select(0, txtCantPaqServ.Text.Length);
                    MessageBox.Show("Debe tener una Cantidad Mayor a 0\npara poder continuar el proceso.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bStatus = false;
                }
                else if (cantidadProductoCombosServicios > 0)
                {
                    errorProvAgregarEditarProducto.SetError(txtCantPaqServ, "");
                }
            }
            else
            {
                txtCantPaqServ.Focus();
                txtCantPaqServ.Select(0, txtCantPaqServ.Text.Length);
                MessageBox.Show("Debe tener una Cantidad numérica y no este vacio\npara poder continuar el proceso.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return bStatus;
        }

        private void AgregarEditarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); 
            }
        }

        private void btnMensajeVenta_Click(object sender, EventArgs e)
        {
            if (Productos.codProductoEditarVenta.Equals(0) && Productos.dobleClickProducto.Equals(0))
            {
                MessageBox.Show("Se requiere guardar el producto antes de asignarle los mensajes", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            } 
            MensajeVentasYMensajeInventario mensajes = new MensajeVentasYMensajeInventario();
            nombreProductoEditar = txtNombreProducto.Text;
            mensajes.ShowDialog();
        }

        private void txtPrecioProducto_KeyDown(object sender, KeyEventArgs e)
        {
            //Aqui se condicionara para ver si el "Empleado" tiene permisos

            //int comprobar = 0;
            //string idempleado = cs.buscarIDEmpleado(FormPrincipal.userNickName);

            //using (DataTable dtUsuarios = cn.CargarDatos(cs.validarUsuario(FormPrincipal.userNickName)))
            //{
            //    if (!dtUsuarios.Rows.Count.Equals(0))
            //    {

            //    }
            //    else
            //    {
            //        using (DataTable dtEmpleadosPermisos = cn.CargarDatos(cs.condicionAsignar("Precio", idempleado)))
            //        {
            //            if (!dtEmpleadosPermisos.Rows.Count.Equals(0))
            //            {
            //                foreach (DataRow item in dtEmpleadosPermisos.Rows)
            //                {
            //                    comprobar = Convert.ToInt32(item["total"]);
            //                }
            //            }
            //        }
            //        if (comprobar > 0)
            //        {

            //        }
            //        else
            //        {
            //            txtPrecioProducto.Enabled = false;
            //            MessageBox.Show("No cuentas con los permisos para modificar este dato", "Alerta Sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //    }
            //}
        }

        private void AgregarEditarProducto_FormClosing(object sender, FormClosingEventArgs e)
        {
            listaProductoToCombo.Clear();
            ProductosDeServicios.Clear();
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
                panelHijo.Width = 180;
                panelHijo.HorizontalScroll.Visible = false;

                // generamos el textbox dinamico 
                TextBox tb = new TextBox();
                tb.Name = "textboxGenerado" + id;
                if (this.Text.Trim() == "AGREGAR PRODUCTO" | this.Text.Trim() == "EDITAR PRODUCTO" | this.Text.Trim() == "COPIAR PRODUCTO")
                {
                    tb.Width = 122;
                }
                else
                {
                    tb.Width = 122;
                }
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
            queryBuscarProd = $"SELECT * FROM Productos WHERE Nombre = '{ProdNombre}' AND Precio = '{ProdPrecio}' AND Categoria = '{ProdCategoria}' AND Status = 1 AND IDUsuario = '{FormPrincipal.userID}'";
            SearchProdResult = cn.CargarDatos(queryBuscarProd);

            if (SearchProdResult.Rows.Count > 0)
            {
                idProductoBuscado = SearchProdResult.Rows[0]["ID"].ToString();
                tipoProdServ = SearchProdResult.Rows[0]["Tipo"].ToString();
                queryBuscarCodBarExt = $"SELECT * FROM CodigoBarrasExtras WHERE IDProducto = '{idProductoBuscado}'";
                SearchCodBarExtResult = cn.CargarDatos(queryBuscarCodBarExt);

                // Para que no cargue los codigo de barra extra cuando sea un producto copiado
                if (DatosSourceFinal != 4)
                {
                    cargarCodBarExt();
                }

                queryBuscarDescuentoCliente = $"SELECT * FROM DescuentoCliente WHERE IDProducto = '{idProductoBuscado}'";
                SearchDesCliente = cn.CargarDatos(queryBuscarDescuentoCliente);
                queryDesMayoreo = $"SELECT * FROM DescuentoMayoreo WHERE IDProducto = '{idProductoBuscado}'";
                SearchDesMayoreo = cn.CargarDatos(queryDesMayoreo);

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
            else if (dtProductosDeServicios != null)
            {
                foreach (DataRow dtRow in dtProductosDeServicios.Rows)
                {
                    string[] datosTmp = new string[17];

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

                        // Actualizar nombre del producto que se muestra
                        datosTmp = cn.BuscarProducto(Convert.ToInt32(IDProducto), FormPrincipal.userID);

                        if (datosTmp.Count() > 0)
                        {
                            //cb.Text = NombreProducto;
                            cb.Text = datosTmp[1];
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error: " + ex.Message.ToString(), "error Text", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cb.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cb.BackColor = System.Drawing.SystemColors.Window;
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;
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

                    if (datosTmp.Count() > 0)
                    {
                        if (!datosTmp[1].ToString().Equals(string.Empty))
                        {
                            panelHijo.Controls.Add(lb1);
                            panelHijo.Controls.Add(cb);
                            panelHijo.Controls.Add(lb2);
                            panelHijo.Controls.Add(tb);
                            panelHijo.Controls.Add(bt);
                            panelHijo.FlowDirection = FlowDirection.LeftToRight;

                            flowLayoutPanel2.Controls.Add(panelHijo);
                            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
                        }
                    }

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
            DataTable dtHistorialCompras;
            DataRow rowHistorialCompras;

            if (DatosSourceFinal == 2)
            {
                ProdNombreFinal = ProdNombre;
            }
            else if (DatosSourceFinal == 4)
            {
                ProdNombreFinal = /*"Copia de " +*/ ProdNombre;
            }
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

            txtNombreProducto.Text = ProdNombreFinal.ToString().Replace("\\'", "'");
            txtStockProducto.Text = ProdStockFinal;

            txtPrecioProducto.Text = ProdPrecioFinal;
            txtCategoriaProducto.Text = ProdCategoriaFinal;
            if (Productos.noMostrarClave == true) { ProdClaveInternaFinal = string.Empty; }

            // Miri.
            if (DatosSourceFinal != 3)
            {
                txtClaveProducto.Text = ProdClaveInternaFinal.Trim();
            }


            if (DatosSourceFinal.Equals(2))
            {
                txtCodigoBarras.Text = ProdCodBarrasFinal.Trim();
                cargarDatosExtra();
            }
            else if (DatosSourceFinal.Equals(3))
            {
                FechaXMLNvoProd = fechaProdXML;
                FolioXMLNvoProd = FolioProdXML;
                RFCXMLNvoProd = RFCProdXML;
                NobEmisorXMLNvoProd = NobEmisorProdXML;
                ClaveProdEmisorXMLNvoProd = ClaveProdEmisorProdXML;
                DescuentoXMLNvoProd = DescuentoProdXML;
                PrecioCompraXMLNvoProd = PrecioCompraXML;
                txtPrecioCompra.Text = PrecioCompraXMLNvoProd;

                // Miri.
                // Agregado para que incluya el nombre del producto.
                // Clave interna en blanco si en el xml no hay noidentificación, se hizo así para evitar que el producto
                // sea guardado con una clave 0, o de lo contrario dara problemas en sección "Datos producto" de agregarstockxml.

                txtNombreProducto.Text = ProdNombre;
                txtCodigoBarras.Text = "";
                //txtCodigoBarras.Text = ProdCodBarrasFinal.Trim();

                if (ProdCodBarrasFinal.Trim() != "" & ProdCodBarrasFinal.Trim() != "0" & ProdCodBarrasFinal.Trim() != null)
                {
                    //txtCodigoBarras.Text = "";
                    txtCodigoBarras.Text = ProdCodBarrasFinal.Trim();
                }

                panelContenedor.Controls.Clear();
            }
            else if (DatosSourceFinal == 4)
            {
                cargarDatosExtra();
            }
            else if (DatosSourceFinal.Equals(5))
            {
                txtCodigoBarras.Text = ProdCodBarrasFinal.Trim();
            }
            else if (!DatosSourceFinal.Equals(3))
            {
                dtHistorialCompras = cn.CargarDatos(cs.CargarHistorialDeCompras(idProductoFinal));
                if (dtHistorialCompras.Rows.Count > 0)
                {
                    rowHistorialCompras = dtHistorialCompras.Rows[0];
                    //txtPrecioCompra.Text = PrecioCompraXMLNvoProd;
                    txtPrecioCompra.Text = rowHistorialCompras["ValorUnitario"].ToString();
                }
                else
                {
                    txtPrecioCompra.Text = "";
                }
            }
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            PedidoPorProducto orderListByProduct = new PedidoPorProducto();
            orderListByProduct.FormClosed += delegate
            {

            };
            if (!orderListByProduct.Visible)
            {
                orderListByProduct.idProductoFinal = idProductoFinal;
                orderListByProduct.ShowDialog();
            }
            else
            {
                orderListByProduct.idProductoFinal = idProductoFinal;
                orderListByProduct.ShowDialog();
            }
        }

        private void txtStockProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar que la tecla presionada no sea CTRL u otra tecla no Numerica
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //Si deseas, puedes permitir numeros decimales (o float)
            //If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        txtStockProducto.Text = calculadora.lCalculadora.Text;
                        calcu = 0;
                    };
                    if (!calculadora.Visible)
                    {
                        calculadora.Show();
                    }
                    else
                    {
                        calculadora.Show();
                    }

                    //if ()
                    //{
                    //    txtStockMaximo.Text = calculadora.lCalculadora.Text;
                    //}
                }
            }
        }

        private void txtStockProducto_Leave(object sender, EventArgs e)
        {
            string[] words;
            if (txtStockProducto.Text.Equals(""))
            {
                txtStockProducto.Text = "0";
            }
            else if (!txtStockProducto.Text.Equals(""))
            {
                words = txtStockProducto.Text.Split('.');
                if (words[0].Equals(""))
                {
                    words[0] = "0";
                }
                if (words.Length > 1)
                {
                    if (words[1].Equals(""))
                    {
                        words[1] = "0";
                    }
                    txtStockProducto.Text = words[0] + "." + words[1];
                }
            }
        }

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar que la tecla presionada no sea CTRL u otra tecla no Numerica
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //Si deseas, puedes permitir numeros decimales (o float)
            //If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        txtPrecioCompra.Text = calculadora.lCalculadora.Text;
                        calcu = 0;
                    };
                    if (!calculadora.Visible)
                    {
                        calculadora.Show();
                    }
                    else
                    {
                        calculadora.Show();
                    }

                    //if ()
                    //{
                    //    txtStockMaximo.Text = calculadora.lCalculadora.Text;
                    //}
                }
            }
        }

        private void txtPrecioProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Verificar que la tecla presionada no sea CTRL u otra tecla no Numerica
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //Si deseas, puedes permitir numeros decimales (o float)
            //If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        txtPrecioProducto.Text = calculadora.lCalculadora.Text;
                        calcu = 0;
                    };
                    if (!calculadora.Visible)
                    {
                        calculadora.Show();
                    }
                    else
                    {
                        calculadora.Show();
                    }

                    //if ()
                    //{
                    //    txtStockMaximo.Text = calculadora.lCalculadora.Text;
                    //}
                }
            }
        }

        private void txtPrecioProducto_Leave(object sender, EventArgs e)
        {
            string[] words;

            if (txtPrecioProducto.Text.Equals(""))
            {
                txtPrecioProducto.Text = "0";
            }
            else if (!txtPrecioProducto.Text.Equals(""))
            {
                words = txtPrecioProducto.Text.Split('.');
                if (words[0].Equals(""))
                {
                    words[0] = "0";
                }
                if (words.Length > 1)
                {
                    if (words[1].Equals(""))
                    {
                        words[1] = "0";
                    }
                    txtPrecioProducto.Text = words[0] + "." + words[1];
                }
            }

            if (!txtPrecioProducto.Text.Equals(""))
            {
                btnAgregarDescuento.Enabled = true;
            }
        }

        private void txtPrecioCompra_Enter(object sender, EventArgs e)
        {
            txtPrecioCompra.SelectAll();
        }

        public void LimpiarDatos()
        {
            stockNecesario = string.Empty;
            stockMinimo = string.Empty;

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

            AgregarDetalleFacturacionProducto.id = 1;
        }

        private void AgregarEditarProducto_Shown(object sender, EventArgs e)
        {
            if (DatosSourceFinal == 1)
            {
                // Obtenemos los datos del producto, servicio o paquete cuando se hace click
                // en el boton cambiar tipo del apartado Productos
                if (cambioProducto)
                {
                    if (idProductoCambio > 0)
                    {
                        var detallesProducto = cn.BuscarProducto(idProductoCambio, FormPrincipal.userID);

                        if (detallesProducto.Length > 0)
                        {
                            txtNombreProducto.Text = detallesProducto[1];
                            txtPrecioCompra.Text = detallesProducto[11];
                            txtPrecioProducto.Text = detallesProducto[2];
                            txtCategoriaProducto.Text = string.Empty;
                            txtClaveProducto.Text = detallesProducto[6];
                            txtCodigoBarras.Text = detallesProducto[7];
                        }
                    }
                }
            }
            if (DatosSourceFinal.Equals(3))
            {
                if (!nameProveedorXML.Equals(string.Empty))
                {
                    // ToDo recorrer el flowLayoutPanel y poder ver si esta Proveedor
                    foreach (Control panelDinamico in flowLayoutPanel3.Controls)
                    {
                        if (panelDinamico is Panel)
                        {
                            Panel pnlDin = (Panel)panelDinamico;

                            var namePanel = pnlDin.Name;

                            if (namePanel.Equals("panelContenedorchkProveedor"))
                            {
                                foreach (Control pnlContenido in pnlDin.Controls)
                                {
                                    if (pnlContenido is Panel)
                                    {
                                        if (pnlContenido.Name.Equals("panelContenidochkProveedor"))
                                        {
                                            foreach (Control cbProveedor in pnlContenido.Controls)
                                            {
                                                if (cbProveedor is ComboBox)
                                                {
                                                    cbProveedor.Text = nameProveedorXML;
                                                    //MessageBox.Show("nameProveedorXML: " + nameProveedorXML + "\nComboBox Proveedor: " + cbProveedor.Text, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /* Fin del codigo de Emmanuel */

        public AgregarEditarProducto(string titulo = "")
        {
            InitializeComponent();
            this.toolTip1.SetToolTip(this.lblStockMinimo, "En este campo puede indicar al sistema el minimo\nde inventario necesario para sus operaciones,\ncon esta información el programa podrá indicarle\ncuando es necesario adquirir mercancía.");
            this.toolTip1.SetToolTip(this.lbStockMaximo, "En este campo puede indicar al sistema el maximo\nde inventario necesario para sus operaciones,\ncon esta información el programa podrá indicarle\ncuando se tenga un exceso de inventario.");
            this.toolTip1.SetToolTip(this.lbStock, "En este campo puede indicar el número de\nproductos con los que cuenta actualmente.");
            this.toolTip1.SetToolTip(this.lbPrecioCompra, "El precio que le costó adquirir el producto.");
            this.toolTip1.SetToolTip(this.lbPrecioVenta, "El precio que tendrá su producto al público.");
            this.toolTip1.SetToolTip(this.lbClaveInterna, "En este campo podra ingresar un folio interno,\npara mayor control de sus productos.");
            this.toolTip1.SetToolTip(this.lblCantCombServ, "En este apartado podrá indicarle al sistema la cantidad\nde productos que se descontaran al venderse un Combo/Servicio");
            this.toolTip1.SetToolTip(this.lblCodigoBarras, "En este campo puede agregar códigos de barras\no claves internas de sus productos.");
            this.toolTip1.SetToolTip(this.btnAddCodBar, "Agregar código de barra extra o clave interna");
            this.toolTip1.SetToolTip(this.lblCodBarExtra, "En este apartado podres agregar un código de barra o clave interna de tus productos.");
        }

        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                agregarCodigoBarraExtra();
            }
        }

        private void agregarCodigoBarraExtra()
        {
            var texto = string.Empty;
            var message = "Ingrese minimo 5 caracteres\npara agregar un código de barra extra";
            var title = "Mensaje de Sistema";
            var id = 0;

            if (!txtCodigoBarras.Text.Equals(string.Empty) || !panelContenedor.Controls.Count.Equals(0))
            {
                if (!txtCodigoBarras.Text.Equals(string.Empty) && !panelContenedor.Controls.Count.Equals(0))
                {
                    int countControles = panelContenedor.Controls.Count;

                    foreach (Control ctrPanelContenedor in panelContenedor.Controls)
                    {
                        if (id.Equals(countControles - 1))
                        {
                            foreach (Control subCtrPanelCont in ctrPanelContenedor.Controls)
                            {
                                if (subCtrPanelCont is TextBox)
                                {
                                    texto = subCtrPanelCont.Text;

                                    if (texto.Length >= 5)
                                    {
                                        GenerarTextBox();
                                    }
                                    else if (texto.Length <= 4)
                                    {
                                        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                        }
                        id++;
                    }
                }
                if (!txtCodigoBarras.Text.Equals(string.Empty) && panelContenedor.Controls.Count.Equals(0))
                {
                    texto = txtCodigoBarras.Text.Trim();

                    if (texto.Length >= 5)
                    {
                        GenerarTextBox();
                    }
                    else if (texto.Length <= 4)
                    {
                        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void GenerarTextBox()
        {
            FlowLayoutPanel panelHijo = new FlowLayoutPanel();
            panelHijo.Name = "panelGenerado" + id;
            panelHijo.Height = 25;
            panelHijo.Width = 180;
            //panelHijo.BackColor = Color.AliceBlue;
            //panelHijo.Location = new Point(0, 0);
            panelHijo.HorizontalScroll.Visible = false;

            TextBox tb = new TextBox();
            tb.Name = "textboxGenerado" + id;
            if (this.Text.Trim() == "AGREGAR PRODUCTO" | this.Text.Trim() == "EDITAR PRODUCTO" | this.Text.Trim() == "COPIAR PRODUCTO")
            {
                tb.Width = 122;
            }
            else
            {
                tb.Width = 122;
            }
            tb.Height = 20;
            //tb.BackColor = Color.Red;
            //tb.Location = new Point(0, 0);
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
                else if (texto.Length <= 4)
                {
                    MessageBox.Show("Ingrese minimo 5 numeros\npara agregar un código de barra extra", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            // leemos el archivo de codigo de barras que lleva el consecutivo
            /*using (StreamReader readfile = new StreamReader(Properties.Settings.Default.rutaDirectorio + fichero))
            {
                Contenido = readfile.ReadToEnd();   // se lee todo el archivo y se almacena en la variable Contenido
            }*/

            txtCodigoBarras.Focus();

            Contenido = mb.ObtenerCBGenerado(FormPrincipal.userID);

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
                if (DatosSourceFinal == 2 || DatosSourceFinal == 4 || DatosSourceFinal.Equals(1))
                {
                    precioProducto = txtPrecioProducto.Text;
                }

                if (FormAgregar != null)
                {
                    try
                    {
                        FormAgregar.Show();
                        FormAgregar.BringToFront();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Excepción: " + ex.Message.ToString(), "Advertencia " + this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    FormAgregar = new AgregarDescuentoProducto();
                    FormAgregar.Show();
                }
            }
            
            //List<string> usuariosPermitidos = new List<string>()
            //{
            //    "HOUSEDEPOTAUTLAN",
            //    "HOUSEDEPOTGRULLO",
            //    "HOUSEDEPOTREPARTO",
            //    "OXXOCLARA6"
            //};


            //if (usuariosPermitidos.Contains(FormPrincipal.userNickName))
            //{
            //    if (txtPrecioProducto.Text == "")
            //    {
            //        MessageBox.Show("Es necesario agregar el precio del producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else if (txtStockProducto.Text == "")
            //    {
            //        MessageBox.Show("Es necesario agregar el stock del producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    {
            //        if (DatosSourceFinal == 2 || DatosSourceFinal == 4 || DatosSourceFinal.Equals(1))
            //        {
            //            precioProducto = txtPrecioProducto.Text;
            //        }

            //        if (FormAgregar != null)
            //        {
            //            try
            //            {
            //                FormAgregar.Show();
            //                FormAgregar.BringToFront();
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show("Excepción: " + ex.Message.ToString(), "Advertencia " + this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            }
            //        }
            //        else
            //        {
            //            FormAgregar = new AgregarDescuentoProducto();
            //            FormAgregar.Show();
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Estamos trabajando en este apartado", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void btnDetalleFacturacion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPrecioProducto.Text))
            {
                MessageBox.Show("Es necesario agregar el precio del producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //if (DatosSourceFinal == 2)
                //{
                //    precioProducto = txtPrecioProducto.Text;
                //}

                precioProducto = txtPrecioProducto.Text;

                //Verifica que el formulario ya tenga una instancia creada, de lo contrario la crea
                if (FormDetalle != null)
                {
                    //FormDetalle.id_producto_edit = Convert.ToInt32(idEditarProducto);
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
                    FormDetalle.id_producto_edit = Convert.ToInt32(idEditarProducto);
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
                if (logoTipo.Equals("") || logoTipo.Equals(null))
                {
                    //btnImagenes.Text = "Seleccionar imagen";
                    //btnImagenes.Text = "Borrar imagen";
                    using (f = new OpenFileDialog())
                    {
                        // le aplicamos un filtro para solo ver 
                        // imagenes de tipo *.jpg y *.png 
                        f.Filter = "Imagenes JPG (*.jpg)|*.jpg| Imagenes PNG (*.png)|*.png";

                        // si se selecciono correctamente un archivo en el OpenFileDialog
                        if (f.ShowDialog() == DialogResult.OK)
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
                        else 
                        {
                            btnImagenes.Text = "Seleccionar imagen";
                            return;
                        }
                    }

                    // verificamos que si no existe el directorio
                    if (!Directory.Exists(saveDirectoryImg))
                    {
                        // lo crea para poder almacenar la imagen
                        Directory.CreateDirectory(saveDirectoryImg);
                    }

                    // si el archivo existe
                    if (f.CheckFileExists)
                    {
                        // Intentamos la actualizacion de la imagen en la base de datos
                        try
                        {
                            // Obtenemos el Nuevo nombre de la imagen
                            // con la que se va hacer la copia de la imagen
                            var source = txtNombreProducto.Text;
                            var replacement = source.Replace('/', '_').Replace('\\', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('\"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_').Replace('-', '_').Replace(' ', '_');
                            NvoFileName = replacement + ".jpg";

                            // si Logotipo es diferente a ""
                            if (logoTipo != "")
                            {
                                if (File1 != null)
                                {
                                    File1.Dispose();

                                    // Verificamos si el pictureBox es null
                                    if (pictureBoxProducto.Image != null)
                                    {
                                        // Liberamos el PictureBox para poder borrar su imagen
                                        pictureBoxProducto.Image.Dispose();
                                        // borramos el archivo de la imagen
                                        System.IO.File.Delete(saveDirectoryImg + NvoFileName);
                                        // realizamos la copia de la imagen origen hacia el nuevo destino
                                        System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
                                        // Obtenemos el nuevo Path
                                        logoTipo = NvoFileName;
                                        // leemos el archivo de imagen y lo ponemos el pictureBox
                                        using (File = new FileStream(saveDirectoryImg + logoTipo, FileMode.Open, FileAccess.Read))
                                        {
                                            // cargamos la imagen en el PictureBox
                                            pictureBoxProducto.Image = Image.FromStream(File);

                                        }
                                    }
                                }
                                else
                                {
                                    // si es que file1 es igual a null
                                    // realizamos la copia de la imagen origen hacia el nuevo destino
                                    System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
                                    // Obtenemos el nuevo Path
                                    logoTipo = NvoFileName;
                                }
                                btnImagenes.Text = "Borrar imagen";
                            }

                            // si el valor de la variable es Null o esta ""
                            if (logoTipo == "" || logoTipo == null)
                            {
                                // Liberamos el pictureBox para poder borrar su imagen
                                pictureBoxProducto.Image.Dispose();
                                // realizamos la copia de la imagen origen hacia el nuevo destino
                                System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
                                // Obtenemos el nuevo Path
                                logoTipo = NvoFileName;
                                // leemos el archivo de imagen y lo ponemos el pictureBox
                                using (File = new FileStream(saveDirectoryImg + logoTipo, FileMode.Open, FileAccess.Read))
                                {
                                    // carrgamos la imagen en el PictureBox
                                    pictureBoxProducto.Image = Image.FromStream(File);
                                }
                                btnImagenes.Text = "Borrar imagen";
                            }
                        }
                        catch (Exception ex)
                        {
                            // si no se puede hacer el proceso
                            // si no se borra el archivo muestra este mensaje
                            MessageBox.Show("Error al hacer el borrado No: " + ex);
                        }
                    }
                }
                else if (!logoTipo.Equals("") || !logoTipo.Equals(null))
                {
                    string path = string.Empty, queryDeleteImageProd = string.Empty, DeleteImage = string.Empty;
                    // ponemos la direccion y nombre de la imagen
                    path = saveDirectoryImg + logoTipo;
                    // cambiamos el texto del boton de Imagen
                    //btnImagenes.Text = "Borrar imagen";
                    btnImagenes.Text = "Seleccionar imagen";
                    // Liberamos el pictureBox para poder borrar su imagen
                    pictureBoxProducto.Image.Dispose();
                    // Establecemos a Nothing el valor de la propiedad Image
                    // del control PictureBox
                    pictureBoxProducto.Image = null;
                    // borramos el archivo de la imagen
                    System.IO.File.Delete(path);

                    if (DatosSourceFinal.Equals(2))
                    {
                        //var idProducto = Convert.ToInt32(idEditarProducto);
                        queryDeleteImageProd = $"UPDATE Productos SET ProdImage ='{DeleteImage}' WHERE ID = {idEditarProducto}";
                        try
                        {
                            cn.EjecutarConsulta(queryDeleteImageProd);
                            logoTipo = string.Empty;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al borrar el nombre de la imagen\nde la base de datos:\n" + ex.Message.ToString(), "información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    logoTipo = string.Empty;
                }
            }
            catch (Exception ex)
            {
                // si el nombre del archivo esta en blanco
                // si no selecciona un archivo valido o ningun archivo muestra este mensaje
                MessageBox.Show("selecciona una Imagen", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            //try
            //{
            //    // Abrirmos el OpenFileDialog para buscar y seleccionar la Imagen
            //    using (f = new OpenFileDialog())
            //    {
            //        // le aplicamos un filtro para solo ver 
            //        // imagenes de tipo *.jpg y *.png 
            //        f.Filter = "Imagenes JPG (*.jpg)|*.jpg| Imagenes PNG (*.png)|*.png";

            //        // si se selecciono correctamente un archivo en el OpenFileDialog
            //        if (f.ShowDialog() == DialogResult.OK)
            //        {
            //            /************************************************
            //            *   usamos el objeto File para almacenar las    *
            //            *   propiedades de la imagen                    * 
            //            ************************************************/
            //            using (File = new FileStream(f.FileName, FileMode.Open, FileAccess.Read))
            //            {
            //                pictureBoxProducto.Image = Image.FromStream(File);      // Cargamos la imagen en el PictureBox
            //                info = new FileInfo(f.FileName);                        // Obtenemos toda la Informacion de la Imagen
            //                fileName = Path.GetFileName(f.FileName);                // Obtenemos el nombre de la imagen
            //                oldDirectory = info.DirectoryName;                      // Obtenemos el directorio origen de la Imagen
            //                File.Dispose();                                         // Liberamos el objeto File
            //            }
            //        }
            //    }

            //    // verificamos que si no existe el directorio
            //    if (!Directory.Exists(saveDirectoryImg))
            //    {
            //        // lo crea para poder almacenar la imagen
            //        Directory.CreateDirectory(saveDirectoryImg);
            //    }

            //    // si el archivo existe
            //    if (f.CheckFileExists)
            //    {
            //        // Intentamos la actualizacion de la imagen en la base de datos
            //        try
            //        {
            //            // Obtenemos el Nuevo nombre de la imagen
            //            // con la que se va hacer la copia de la imagen
            //            var source = txtNombreProducto.Text;
            //            var replacement = source.Replace('/', '_').Replace('\\', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('\"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_').Replace('-', '_').Replace(' ', '_');
            //            NvoFileName = replacement + ".jpg";

            //            // si Logotipo es diferente a ""
            //            if (logoTipo != "")
            //            {
            //                if (File1 != null)
            //                {
            //                    File1.Dispose();

            //                    // Verificamos si el pictureBox es null
            //                    if (pictureBoxProducto.Image != null)
            //                    {
            //                        // Liberamos el PictureBox para poder borrar su imagen
            //                        pictureBoxProducto.Image.Dispose();
            //                        // borramos el archivo de la imagen
            //                        System.IO.File.Delete(saveDirectoryImg + NvoFileName);
            //                        // realizamos la copia de la imagen origen hacia el nuevo destino
            //                        System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
            //                        // Obtenemos el nuevo Path
            //                        logoTipo = NvoFileName;
            //                        // leemos el archivo de imagen y lo ponemos el pictureBox
            //                        using (File = new FileStream(saveDirectoryImg + logoTipo, FileMode.Open, FileAccess.Read))
            //                        {
            //                            // cargamos la imagen en el PictureBox
            //                            pictureBoxProducto.Image = Image.FromStream(File);
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    // si es que file1 es igual a null
            //                    // realizamos la copia de la imagen origen hacia el nuevo destino
            //                    System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
            //                    // Obtenemos el nuevo Path
            //                    logoTipo = NvoFileName;
            //                }
            //            }

            //            // si el valor de la variable es Null o esta ""
            //            if (logoTipo == "" || logoTipo == null)
            //            {
            //                // Liberamos el pictureBox para poder borrar su imagen
            //                pictureBoxProducto.Image.Dispose();
            //                // realizamos la copia de la imagen origen hacia el nuevo destino
            //                System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
            //                // Obtenemos el nuevo Path
            //                logoTipo = NvoFileName;
            //                // leemos el archivo de imagen y lo ponemos el pictureBox
            //                using (File = new FileStream(saveDirectoryImg + logoTipo, FileMode.Open, FileAccess.Read))
            //                {
            //                    // carrgamos la imagen en el PictureBox
            //                    pictureBoxProducto.Image = Image.FromStream(File);
            //                }
            //            }
            //        }
            //        catch (Exception ex)	
            //        {
            //            // si no se puede hacer el proceso
            //            // si no se borra el archivo muestra este mensaje
            //            MessageBox.Show("Error al hacer el borrado No: " + ex);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // si el nombre del archivo esta en blanco
            //    // si no selecciona un archivo valido o ningun archivo muestra este mensaje
            //    MessageBox.Show("selecciona una Imagen", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }

        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            var tituloVentana = string.Empty;
            Console.WriteLine("holaaaa");

            #region Inicio Sección de Cambio de Producto a Servicio/Combo ó Servicio/Combo a Producto
            // Condiciones para saber si se realiza el cambio de un producto a servicio y viceversa
            if (cambioProducto)
            {
                validarCambioProducto();
            }
            #endregion Final Sección de Cambio de Producto a Servicio/Combo ó Servicio/Combo a Producto

            #region Inicio de Variables de Alejandro
            /****************************
	        *	codigo de Alejandro		*
	        ****************************/
            filtroTipoSerPaq = Convert.ToString(cbTipo.SelectedItem);
            tipoServPaq = filtroTipoSerPaq;
            nombre = txtNombreProducto.Text;
            if (this.Text.Trim() == "AGREGAR PRODUCTO" | this.Text.Trim() == "EDITAR PRODUCTO" | this.Text.Trim() == "COPIAR PRODUCTO")
            {
                validarDecimales(txtStockProducto.Text.ToString());
            }
            if (!parteDecimal.Equals(""))
            {
                stock = parteEntera + "." + parteDecimal;
            }
            else if (parteDecimal.Equals(""))
            {
                stock = parteEntera;
            }
            //stock = txtStockProducto.Text;
            valorDePrecioVenta = Convert.ToDouble(txtPrecioProducto.Text);
            precio = valorDePrecioVenta.ToString();
            categoria = txtCategoriaProducto.Text;
            claveIn = txtClaveProducto.Text.Trim();
            codigoB = txtCodigoBarras.Text.Trim();
            ProdServPaq = "P".ToString();
            tipoDescuento = "0";
            idUsrNvo = FormPrincipal.userID.ToString();
            fechaCompra = DateTime.Now.ToString("yyyy-MM-dd");
            fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            stockMinimo = txtStockMinimo.Text;
            stockNecesario = txtStockMaximo.Text;
            var precioMayoreo = txtPrecioMayoreo.Text.Trim();

            if (string.IsNullOrWhiteSpace(precioMayoreo))
            {
                precioMayoreo = "0";
            }

            /*	Fin del codigo de Alejandro	*/
            #endregion Final de Variables de Alejandro

            #region Inicio de Variables a Cero
            /************************************
	        *  iniciamos las variables a 0     *
	        *	codigo de Emmanuel				*
	        ************************************/

            resultadoSearchNoIdentificacion = 0;    // ponemos los valores en 0
            resultadoSearchCodBar = 0;              // ponemos los valores en 0
            resultadoSearchCodBarExtra = 0;         // ponemos los valores en 0

            /* Fin de iniciamos las variables a 0 */
            #endregion Final de Variables a Cero

            #region Inicio de Sección Origen (Forma Manual / XML), (Editar) ó ( Hacer Copia)
            saberOrigenProducto();
            #endregion Final de Sección Origen (Forma Manual / XML), (Editar) ó ( Hacer Copia)

            #region Inicio Sección De Agregar Producto, Combo ó Servicio Desde XML / Botón manual
            if (DatosSourceFinal == 3 || DatosSourceFinal == 1 || DatosSourceFinal == 5 || DatosSourceFinal == 4)
            {
                #region Inicio Sección que el precio no sea menor al precio original del producto servicio/combo
                //Validar que el precio no sea menor al precio original del producto/servicio
                if (Convert.ToDouble(precio) < Convert.ToDouble(txtPrecioCompra.Text))
                {
                    MessageBox.Show("El precio no puede ser mayor al precio original", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrecioProducto.Focus();
                    return;
                }
                #endregion Final Sección que el precio no sea menor al precio original del producto servicio/combo

                #region Inicio Sección busqueda que no se repita la ClaveInterna
                //Hacemos la busqueda que no se repita en CalveInterna
                //searchClavIntProd();
                if (mb.ComprobarCodigoClave(claveIn, FormPrincipal.userID))
                {
                    string query = string.Empty;
                    datosProductosBtnGuardar = new List<string>();
                    datosProductoRelacionado = new List<string>();

                    // Cargar procuto registrado con esa Clave Interna
                    productoRegistradoClaveInterna(claveIn);

                    // Buscamos Producto con codigo de barras
                    productoRegistradoCodigoBarras(claveIn);
                }
                #endregion Final Sección busqueda que no se repita la ClaveInterna

                #region Inicio Sección busqueda que no se repita en CodigoBarra
                //Hacemos la busqueda que no se repita en CodigoBarra
                //searchCodBar();
                if (mb.ComprobarCodigoClave(codigoB, FormPrincipal.userID))
                {
                    string query = string.Empty;
                    List<string> datosProductos = new List<string>(), datosProductoRelacionado = new List<string>();

                    // Buscamos Producto con codigo de barras
                    productoRegistradoCodigoBarras(codigoB);

                    // Cargar procuto registrado con esa Clave Interna
                    productoRegistradoClaveInterna(codigoB);
                }
                #endregion Final Sección busqueda que no se repita en CodigoBarra

                #region Inicio Seccion Código de Barras Extras
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
                            codigosBarrras.Clear();
                            return;
                        }
                    }
                }
                #endregion Final Seccion Código de Barras Extras

                if (resultadoSearchNoIdentificacion == 1 || resultadoSearchCodBar == 1 || resultadoSearchCodBarExtra == 1)
                {
                    if (!txtClaveProducto.Text.Equals(""))
                    {
                        txtClaveProducto.Focus();
                    }
                    else if (!txtCodigoBarras.Text.Equals(""))
                    {
                        txtCodigoBarras.Focus();
                    }
                }
                else if (resultadoSearchNoIdentificacion == 0 || resultadoSearchCodBar == 0)
                {
                    /****************************
                    *	codigo de Alejandro		*
                    ****************************/
                    #region Inicio de Descuento seleccionado
                    if (descuentos.Any())
                    {
                        //Cerramos la ventana donde se eligen los descuentos
                        FormAgregar.Close();
                        tipoDescuento = descuentos[0];
                    }
                    #endregion Final de Descuento seleccionado

                    #region Inicio de Sección de Productos
                    if (this.Text.Trim() == "AGREGAR PRODUCTO" | this.Text.Trim() == "EDITAR PRODUCTO" | this.Text.Trim() == "COPIAR PRODUCTO")
                    {
                        bool bValidNombreProducto = ValidateNombreProducto();
                        bool bValidPrecioProducto = ValidatePrecioProducto();

                        if (bValidNombreProducto && bValidPrecioProducto)
                        {
                            bool bValidClaveInterna = ValidateClaveProducto();
                            bool bValidCodigoBarras = ValidateCodigoBarras();

                            if (bValidClaveInterna || bValidCodigoBarras)
                            {
                                errorProvAgregarEditarProducto.SetError(txtClaveProducto, "");
                                errorProvAgregarEditarProducto.SetError(txtCodigoBarras, "");

                                // Miri.
                                // Si el producto es guardado sin abrir la ventana de datos de facturación
                                // y el producto ha agregar trae impuestos, entonces verificará el tipo de impuesto princial 
                                // para re-calcular: base, tipo de impuesto e importe del impuesto.
                                if (datosImpuestos == null & DatosSourceFinal == 3 & AgregarStockXML.tipo_impuesto_delxml != "")
                                {
                                    impuestoProducto = AgregarStockXML.tipo_impuesto_delxml;
                                    baseProducto = precio;
                                    ivaProducto = "0.00";

                                    if (AgregarStockXML.tipo_impuesto_delxml != "Exento")
                                    {
                                        impuestoProducto += "%";
                                    }

                                    if (AgregarStockXML.tipo_impuesto_delxml == "8")
                                    {
                                        baseProducto = (Convert.ToDouble(precio) / 1.08).ToString("0.00");
                                        ivaProducto = (Convert.ToDouble(baseProducto) * 0.08).ToString("0.00");
                                    }
                                    if (AgregarStockXML.tipo_impuesto_delxml == "16")
                                    {
                                        baseProducto = (Convert.ToDouble(precio) / 1.16).ToString("0.00");
                                        ivaProducto = (Convert.ToDouble(baseProducto) * 0.16).ToString("0.00");
                                    }
                                }

                                tmp_clave_interna = claveIn;
                                tmp_codigo_barras = codigoB;


                                guardar = new string[] {
                                    nombre, stock, precio, categoria, claveIn, codigoB, claveProducto, claveUnidadMedida,
                                    tipoDescuento, idUsrNvo, logoTipo, ProdServPaq, baseProducto, ivaProducto, impuestoProducto,
                                    mg.RemoverCaracteres(nombre), mg.RemoverPreposiciones(nombre), stockNecesario, stockMinimo,
                                    txtPrecioCompra.Text, precioMayoreo };

                                #region Inicio Se guardan los datos principales del Producto
                                //Se guardan los datos principales del producto
                                try
                                {
                                    respuesta = cn.EjecutarConsulta(cs.GuardarProducto(guardar, FormPrincipal.userID));

                                    if (respuesta > 0)
                                    {
                                        claveProducto = string.Empty;
                                        claveUnidadMedida = string.Empty;

                                        //Se obtiene la ID del último producto agregado
                                        idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));
                                        var claveP = txtClaveProducto.Text;

                                        // Agregar la relacion de producto ya registrado con Combo Servicio
                                        #region Agregar a tabla productosdeservicios
                                        if (!listaProductoToCombo.Count().Equals(0))
                                        {
                                            if (this.Text.Trim() == "AGREGAR PRODUCTO")
                                            {
                                                string[] datos;
                                                foreach (var item in listaProductoToCombo)
                                                {
                                                    datos = item.Split('|');
                                                    string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                    datos[0] = fech.Trim();
                                                    using (DataTable dtDatosProducto = cn.CargarDatos(cs.getDatosProducto(Convert.ToString(idProducto))))
                                                    {
                                                        if (!dtDatosProducto.Rows.Count.Equals(0))
                                                        {
                                                            foreach (DataRow drDatosProd in dtDatosProducto.Rows)
                                                            {
                                                                datos[2] = drDatosProd["ID"].ToString();
                                                                datos[3] = drDatosProd["Nombre"].ToString();
                                                                cn.EjecutarConsulta(cs.insertarProductosServicios(datos));
                                                            }
                                                        }
                                                    }
                                                    cn.EjecutarConsulta(cs.borrarProdRelBlanco(Convert.ToInt32(datos[1].ToString())));
                                                }
                                            }
                                        }
                                        #endregion

                                        // Crear registro en tabla de correos para que por defecto se habilite la opcion de enviar correo al hacer venta
                                        cn.EjecutarConsulta($"INSERT INTO CorreosProducto (IDUsuario,IDEmpleado, IDProducto, CorreoPrecioProducto, CorreoStockProducto, CorreoStockMinimo) VALUES ('{FormPrincipal.userID}','{FormPrincipal.id_empleado}', '{idProducto}', 1, 1, 1);");

                                        #region Inicio de Datos de Impuestos
                                        //Se realiza el proceso para guardar los detalles de facturación del producto
                                        if (datosImpuestos != null)
                                        {
                                            guardarDatosImpuestos();
                                        }
                                        if (datosImpuestos == null & DatosSourceFinal == 3 & AgregarStockXML.list_impuestos_traslado_retenido.Count() > 0)
                                        {
                                            guardar_impuestos_dexml(Convert.ToDouble(baseProducto), idProducto);
                                        }
                                        #endregion Final de datos de Impuestos

                                        bool isEmpty = detalleProductoBasico.Any();

                                        if (isEmpty.Equals(false))
                                        {
                                            if (!flowLayoutPanel3.Controls.Count.Equals(0))
                                            {
                                                string panel = string.Empty;
                                                foreach (Control contHijo in flowLayoutPanel3.Controls)
                                                {
                                                    panel = contHijo.Name;
                                                    if (contHijo.Name.Equals("panelContenedorchkProveedor"))
                                                    {
                                                        foreach (Control contSubHijo in contHijo.Controls)
                                                        {
                                                            foreach (Control contItemSubHijo in contSubHijo.Controls)
                                                            {
                                                                if (contItemSubHijo is Label)
                                                                {
                                                                    if (contItemSubHijo.Name.Equals("lblNombrechkProveedor"))
                                                                    {
                                                                        string textoConcepto = string.Empty;
                                                                        if (!contItemSubHijo.Text.Equals(string.Empty))
                                                                        {
                                                                            textoConcepto = contItemSubHijo.Text;
                                                                            var idFound = mb.obtenerIdDetallesProveedor(FormPrincipal.userID, textoConcepto);
                                                                            detalleProductoBasico.Add(Convert.ToString(idProducto));
                                                                            detalleProductoBasico.Add(Convert.ToString(FormPrincipal.userID));
                                                                            detalleProductoBasico.Add(idFound[2].ToString());
                                                                            detalleProductoBasico.Add(idFound[0].ToString());
                                                                            isEmpty = true;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        #region Inicio Detalles del Producto Basicos (Proveedor)
                                        if (isEmpty.Equals(true))
                                        {
                                            // Para guardar los detalles del producto
                                            // Ejemplo: Proveedor, Categoria, Ubicacion, etc.
                                            guardarDetallesProductoBasico(detalleProductoBasico);
                                        }
                                        #endregion Final Detalles del Producto Basicos (Proveedor)

                                        isEmpty = detalleProductoGeneral.Any();

                                        if (isEmpty.Equals(false))
                                        {
                                            if (!flowLayoutPanel3.Controls.Count.Equals(0))
                                            {
                                                string panel = string.Empty;
                                                foreach (Control contHijo in flowLayoutPanel3.Controls)
                                                {
                                                    panel = contHijo.Name;
                                                    if (!contHijo.Name.Equals("panelContenedorchkProveedor"))
                                                    {
                                                        foreach (Control contSubHijo in contHijo.Controls)
                                                        {
                                                            foreach (Control contItemSubHijo in contSubHijo.Controls)
                                                            {
                                                                if (contItemSubHijo is Label)
                                                                {
                                                                    if (!contItemSubHijo.Name.Equals("lblNombrechkProveedor"))
                                                                    {
                                                                        string textoConcepto = string.Empty;
                                                                        if (!contItemSubHijo.Text.Equals(string.Empty))
                                                                        {
                                                                            textoConcepto = contItemSubHijo.Text;
                                                                            string rowDataList = string.Empty;
                                                                            var idFoundNew = mb.obtenerIdDetalleGeneral(FormPrincipal.userID, textoConcepto);

                                                                            rowDataList = Convert.ToString(idProducto) + "|" + Convert.ToString(FormPrincipal.userID) + "|" + idFoundNew[0].ToString() + "|1|panelContenido" + idFoundNew[2].ToString() + "|" + idFoundNew[3].ToString();
                                                                            detalleProductoGeneral.Add(rowDataList);

                                                                            isEmpty = true;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        #region Inicio Detalles del Producto Generales (Dinamicos)
                                        if (isEmpty.Equals(true))
                                        {
                                            guardarDetallesProductoDinamicos(detalleProductoGeneral);
                                        }
                                        #endregion Final Detalles del Producto Generales (Dinamicos)

                                        #region Inicio Agreado de forma manual Boton
                                        if (DatosSourceFinal == 1)
                                        {
                                            #region Inicio para relacionar productos con algun combo/servicio
                                            guardarRelacionDeProductoConComboServicio();
                                            var contenido = cn.EjecutarConsulta($"UPDATE HistorialCompras SET RFCEmisor = '{AgregarDetalleProducto.rfc}', NomEmisor = '{AgregarDetalleProducto.nameProveedor}', ClaveProdEmisor = '{claveP}' WHERE IDProducto = {idProducto}");
                                            #endregion Final para relacionar productos con algun paquete/servicio
                                        }
                                        #endregion Final Agreado de forma manual Boton

                                        #region Inicio Agreado desde XML
                                        if (DatosSourceFinal == 3)
                                        {
                                            idHistorialCompraProducto = 0;
                                            found = 10;
                                            fechaXML = FechaXMLNvoProd;
                                            fecha = fechaXML.Substring(0, found);
                                            hora = fechaXML.Substring(found + 1);
                                            fechaCompleta = fecha + " " + hora;
                                            folio = FolioXMLNvoProd;
                                            RFCEmisor = RFCXMLNvoProd;
                                            nombreEmisor = NobEmisorXMLNvoProd;
                                            claveProdEmisor = ClaveProdEmisorXMLNvoProd;
                                            descuentoXML = DescuentoXMLNvoProd;
                                            PrecioCompraXMLNvoProd = txtPrecioCompra.Text;

                                            #region Inicio Agregar Proveedor a Detalle Producto
                                            try
                                            {
                                                cn.EjecutarConsulta(cs.GuardarDetallesDelProducto(idProducto, FormPrincipal.userID, nameProveedorXML.ToString(), Convert.ToInt32(idProveedorXML.ToString())));
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al Agregar Proveedor a Detalle Producto\n" + origen, "Error Agregar Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            #endregion Final Agregar Proveedor a Detalle Producto

                                            DateTime dt = DateTime.Now;

                                            string query = $@"INSERT INTO HistorialCompras(Concepto, Cantidad, ValorUnitario, Descuento, Precio, FechaLarga, Folio, RFCEmisor, NomEmisor, ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto, IDUsuario) VALUES('{nombre}','{stock}','{PrecioCompraXMLNvoProd}','{descuentoXML}','{precio}','{fechaCompleta}','{folio.Trim()}','{RFCEmisor.Trim()}','{nombreEmisor.Trim()}','{claveProdEmisor.Trim()}','{dt.ToString("yyyy-MM-dd hh:mm:ss")}','{Inventario.idReporte}','{idProducto}','{FormPrincipal.userID}')";

                                            #region Inicio Historial de Compras
                                            try
                                            {
                                                cn.EjecutarConsulta(query);
                                                idHistorialCompraProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM HistorialCompras ORDER BY ID DESC LIMIT 1", 1));
                                                //MessageBox.Show("Registrado Intento 1", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al Agregar al Historial de Compras\n" + origen, "Error Agregar Historial", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            #endregion Final Historial de Compras

                                            DateTime date1 = DateTime.Now;
                                            fechaCompleta = date1.ToString("s");
                                            string Year = fechaCompleta.Substring(0, found);
                                            string Date = fechaCompleta.Substring(found + 1);
                                            string FechaRegistrada = Year + " " + Date;
                                            string queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{FormPrincipal.userID}','{idHistorialCompraProducto}','{FechaRegistrada}')";

                                            try
                                            {
                                                cn.EjecutarConsulta(queryRecordHistorialProd);
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al Agregar Historial de Modificaciones del Producto, Combo, Servicio\n" + origen, "Error Historial de Modificaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        #endregion Final Agregado desde XML

                                        #region Inicio Guardar Descuento del Producto
                                        //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                                        if (descuentos.Any())
                                        {
                                            //Descuento por Cliente
                                            if (descuentos[0] == "1")
                                            {
                                                guardar = new string[] { descuentos[1], descuentos[2], descuentos[3], descuentos[4] };

                                                try
                                                {
                                                    cn.EjecutarConsulta(cs.GuardarDescuentoCliente(guardar, idProducto));
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("Error al Agregar Descuento de Cliente\n" + origen, "Error Descuento de Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                            //Descuento por Mayoreo
                                            if (descuentos[0] == "2")
                                            {
                                                foreach (var descuento in descuentos)
                                                {
                                                    if (descuento == "2") { continue; }

                                                    string[] tmp = descuento.Split('-');

                                                    try
                                                    {
                                                        cn.EjecutarConsulta(cs.GuardarDescuentoMayoreo(tmp, idProducto));
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        MessageBox.Show("Error al Agregar Descuento de Mayoreo\n" + origen, "Error Descuento de Mayoreo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion Final Guardar Descuento del Producto

                                        #region Inicio de Codigos de Barra Extra
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
                                        #endregion Final de Codigos de Barra Extra

                                        if (this.Text.Trim().Equals("AGREGAR PRODUCTO"))
                                        {
                                            titulo = "Producto Agregado";
                                            texto = "Se guardo exitosamente el producto...."; 
                                            SeleccionaImagen = "correcto";
                                        }
                                        else if (this.Text.Trim().Equals("EDITAR PRODUCTO"))
                                        {
                                            titulo = "Producto Actualizado";
                                            texto = "Se actualizo exitosamente el producto....";
                                            SeleccionaImagen = "correcto";
                                        }
                                        else if (this.Text.Trim().Equals("COPIAR PRODUCTO"))
                                        {
                                            titulo = "Producto Copiado";
                                            texto = "Se copio exitosamente el producto....";
                                            SeleccionaImagen = "correcto";
                                        }

                                        if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
                                        {
                                            MensajeAgregarEditarCopiarProducto MAECP = new MensajeAgregarEditarCopiarProducto(titulo, texto, SeleccionaImagen);
                                            MAECP.ShowDialog();
                                           // MessageBoxTemporal.Show(tituloVentana, "Aviso del Sistema", 2, false);
                                        }
                                        else if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
                                        {
                                            MensajeAgregarEditarCopiarProducto MAECP = new MensajeAgregarEditarCopiarProducto(titulo, texto, SeleccionaImagen);
                                            MAECP.ShowDialog();
                                            //MessageBoxTemporal.Show(tituloVentana, "Aviso del Sistema", 2, false);
                                        }

                                        //Cierra la ventana donde se agregan los datos del producto
                                        this.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al Agregar Producto\n" + origen, "Error Agregar Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                #endregion Final Se guardan los datos principales del Producto
                            }
                            else if (!bValidClaveInterna || !bValidCodigoBarras)
                            {
                                errorProvAgregarEditarProducto.SetError(txtClaveProducto, "Debe tener una Clave Interna\npara poder continuar el proceso.");
                                errorProvAgregarEditarProducto.SetError(txtCodigoBarras, "Debe tener un Código de Barras\npara poder continuar el proceso.");
                                MessageBox.Show("La Clave Interna ó Código de Barras\nNO DEBE ESTAR EN BLANCO", "Advertencia de llenado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            if (!bValidNombreProducto)
                            {
                                MessageBox.Show("Por favor poner Datos Validos\npara el campo de NOMBRE DE PRODUCTO", "Alerta del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtNombreProducto.Select(0, txtNombreProducto.Text.Length);
                                txtNombreProducto.Focus();
                            }
                            else if (!bValidPrecioProducto)
                            {
                                MessageBox.Show("Por favor poner Datos Validos\npara el campo de PRECIO DE VENTA", "Alerta del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtPrecioProducto.Select(0, txtPrecioProducto.Text.Length);
                                txtPrecioProducto.Focus();
                            }
                        }
                    }
                    #endregion Final de Sección de Productos
                    #region Inicio de Sección de Combos y Servicios
                    else if (this.Text.Trim() == "AGREGAR COMBOS" || this.Text.Trim() == "EDITAR COMBOS" || this.Text.Trim() == "COPIAR COMBOS" || this.Text.Trim() == "AGREGAR SERVICIOS" || this.Text.Trim() == "EDITAR SERVICIOS" || this.Text.Trim() == "COPIAR SERVICIOS")
                    {
                        bool bValidNombreProducto = ValidateNombreProducto();
                        bool bValidPrecioProducto = ValidatePrecioProducto();

                        if (bValidNombreProducto && bValidPrecioProducto)
                        {
                            bool bValidClaveInterna = ValidateClaveProducto();
                            bool bValidCodigoBarras = ValidateCodigoBarras();

                            if (bValidClaveInterna || bValidCodigoBarras)
                            {
                                errorProvAgregarEditarProducto.SetError(txtClaveProducto, "");
                                errorProvAgregarEditarProducto.SetError(txtCodigoBarras, "");

                                #region Inicio Saber si es Servicio ó Combo
                                string FuenteServPaq = string.Empty;

                                if (this.Text.Trim() == "AGREGAR SERVICIOS" | this.Text.Trim() == "EDITAR SERVICIOS" | this.Text.Trim() == "COPIAR SERVICIOS")
                                {
                                    ProdServPaq = "S";
                                    FuenteServPaq = "Servicio";
                                    categoria = "SERVICIOS";
                                }
                                else if (this.Text.Trim() == "AGREGAR COMBOS" | this.Text.Trim() == "EDITAR COMBOS" | this.Text.Trim() == "COPIAR COMBOS")
                                {
                                    ProdServPaq = "PQ";
                                    FuenteServPaq = "Combo";
                                    categoria = "COMBOS";
                                }
                                #endregion Final Saber si es Servicio ó Combo

                                stock = "0";

                                // Miri.
                                // Si el producto es guardado sin abrir la ventana de datos de facturación
                                // y el producto ha agregar trae impuestos, entonces verificará el tipo de impuesto princial 
                                // para re-calcular: base, tipo de impuesto e importe del impuesto.
                                if (datosImpuestos == null & DatosSourceFinal == 3 & AgregarStockXML.tipo_impuesto_delxml != "")
                                {
                                    impuestoProducto = AgregarStockXML.tipo_impuesto_delxml;
                                    baseProducto = precio;
                                    ivaProducto = "0.00";

                                    if (AgregarStockXML.tipo_impuesto_delxml != "Exento")
                                    {
                                        impuestoProducto += "%";
                                    }

                                    if (AgregarStockXML.tipo_impuesto_delxml == "8")
                                    {
                                        baseProducto = (Convert.ToDouble(precio) / 1.08).ToString("0.00");
                                        ivaProducto = (Convert.ToDouble(baseProducto) * 0.08).ToString("0.00");
                                    }
                                    if (AgregarStockXML.tipo_impuesto_delxml == "16")
                                    {
                                        baseProducto = (Convert.ToDouble(precio) / 1.16).ToString("0.00");
                                        ivaProducto = (Convert.ToDouble(baseProducto) * 0.16).ToString("0.00");
                                    }
                                }
                                // Uso en declaración.
                                tmp_clave_interna = claveIn;
                                tmp_codigo_barras = codigoB;


                                guardar = new string[] {
                                    nombre, stock, precio, categoria, claveIn, codigoB, claveProducto, claveUnidadMedida,
                                    tipoDescuento, FormPrincipal.userID.ToString(), logoTipo, ProdServPaq, baseProducto,
                                    ivaProducto, impuestoProducto, mg.RemoverCaracteres(nombre), mg.RemoverPreposiciones(nombre),
                                    stockNecesario, "0", txtPrecioCompra.Text, precioMayoreo
                                };

                                #region Inicio de guardado de los datos principales del Servicios o Combos
                                //Se guardan los datos principales del producto
                                try
                                {
                                    respuesta = cn.EjecutarConsulta(cs.GuardarProducto(guardar, FormPrincipal.userID));

                                    //Se obtiene la ID del último producto agregado
                                    idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));

                                    if (respuesta > 0)
                                    {
                                        claveProducto = string.Empty;
                                        claveUnidadMedida = string.Empty;

                                        bool isEmpty = !detalleProductoBasico.Any();

                                        #region Inicio Guardar Datos Basicos Detalle Producto
                                        // para guardar los detalles del producto
                                        if (!isEmpty)
                                        {
                                            // Para guardar los detalles del producto
                                            // Ejemplo: Proveedor, Categoria, Ubicacion, etc.
                                            guardarDetallesProductoBasico(detalleProductoBasico);
                                        }
                                        #endregion Final Guardar Datos Basico Detalles Producto

                                        isEmpty = !detalleProductoGeneral.Any();

                                        #region Inicio Guardar Datos Generales Detalle Producto
                                        if (!isEmpty)
                                        {
                                            guardarDetallesProductoDinamicos(detalleProductoGeneral);
                                        }
                                        #endregion Final  Guardar Datos Generales Detalle Producto

                                        #region Inicio Seccion de Agregar desde Boton Producto y de Editar Producto
                                        if (DatosSourceFinal == 1 || DatosSourceFinal == 2)
                                        {
                                            var conceptoProveedor = string.Empty;
                                            var rfcProveedor = string.Empty;

                                            //Datos para la tabla historial de compras
                                            if (idProveedorBtnGuardar != null)
                                            {
                                                var proveedorTmp = mb.ObtenerDatosProveedor(Convert.ToInt32(idProveedorBtnGuardar), FormPrincipal.userID);
                                                conceptoProveedor = proveedorTmp[0];
                                                rfcProveedor = proveedorTmp[1];
                                            }

                                            guardar = new string[] { nombre, stock, precio, txtPrecioCompra.Text, fechaCompra, rfcProveedor, conceptoProveedor, "", "1", fechaOperacion, idReporte.ToString(), idProducto.ToString(), FormPrincipal.userID.ToString() };

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

                                            /*if (flowLayoutPanel2.Controls.Count == 0)
                                            {
                                                string[] tmp = { $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}", $"{idProducto}", "", "", $"{txtCantPaqServ.Text}" };
                                                cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                                            }*/
                                            if (!ProductosDeServicios.Count.Equals(0))
                                            {
                                                foreach (var item in ProductosDeServicios)
                                                {
                                                    var datos = item.Split('|');
                                                    datos[1] = idProducto.ToString();
                                                    cn.EjecutarConsulta(cs.GuardarProductosServPaq(datos));
                                                }
                                            }
                                            else if (ProductosDeServicios.Count.Equals(0))
                                            {
                                                string[] datos = new string[5];
                                                string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                datos[0] = fech;
                                                datos[1] = idProducto.ToString();
                                                datos[2] = "0";
                                                datos[3] = string.Empty;
                                                datos[4] = txtCantPaqServ.Text;
                                                cn.EjecutarConsulta(cs.GuardarProductosServPaq(datos));
                                            }
                                            // recorrido para FlowLayoutPanel2 para ver cuantos TextBox
                                            //if (ProductosDeServicios.Count > 0 || ProductosDeServicios.Count == 0)
                                            //{
                                            //ProductosDeServicios.Clear();

                                            //if (flowLayoutPanel2.Controls.Count > 0)
                                            //{
                                            //    // recorrido del panel de Prodcutos de Productos para ver cuantos Productos fueron seleccionados
                                            //    foreach (Control panel in flowLayoutPanel2.Controls.OfType<FlowLayoutPanel>())
                                            //    {
                                            //        // agregamos la variable para egregar los procutos
                                            //        string prodSerPaq = null;
                                            //        DataTable dtProductos;
                                            //        foreach (Control item in panel.Controls)
                                            //        {
                                            //            string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                            //            if (item is ComboBox)
                                            //            {
                                            //                if (item.Text != "Por favor selecciona un Producto")
                                            //                {
                                            //                    string buscar = null;
                                            //                    string comboBoxText = item.Text;
                                            //                    string comboBoxValue = null;
                                            //                    buscar = $"SELECT ID, Nombre FROM Productos WHERE Nombre = '{comboBoxText}' AND IDUsuario = '{FormPrincipal.userID}'";
                                            //                    dtProductos = cn.CargarDatos(buscar);
                                            //                    comboBoxValue = dtProductos.Rows[0]["ID"].ToString();
                                            //                    prodSerPaq += fech + "|";
                                            //                    prodSerPaq += idProducto + "|";
                                            //                    prodSerPaq += comboBoxValue + "|";
                                            //                    prodSerPaq += comboBoxText + "|";
                                            //                }
                                            //            }
                                            //            if (item is TextBox)
                                            //            {
                                            //                var tb = item.Text;
                                            //                if (item.Text == "0")
                                            //                {
                                            //                    tb = "0";
                                            //                    prodSerPaq += tb;
                                            //                }
                                            //                else
                                            //                {
                                            //                    prodSerPaq += tb;
                                            //                }
                                            //            }
                                            //        }
                                            //        ProductosDeServicios.Add(prodSerPaq);
                                            //        prodSerPaq = null;
                                            //    }
                                            //    //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                                            //    if (ProductosDeServicios.Any())
                                            //    {
                                            //        string queryBorrarProductosDeServicios = $"DELETE FROM ProductosDeServicios WHERE IDServicio = '{idProductoBuscado}'";
                                            //        cn.EjecutarConsulta(queryBorrarProductosDeServicios);
                                            //        foreach (var productosSP in ProductosDeServicios)
                                            //        {
                                            //            string[] tmp = productosSP.Split('|');
                                            //            if (tmp.Length == 5)
                                            //            {
                                            //                cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                                            //            }
                                            //        }
                                            //        ProductosDeServicios.Clear();
                                            //    }
                                            //}
                                            //if (flowLayoutPanel2.Controls.Count == 0)
                                            //{
                                            //    if (ProductosDeServicios.Count.Equals(0))
                                            //    {
                                            //        string[] datos = new string[5];
                                            //        string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                            //        datos[0] = fech;
                                            //        datos[1] = idProducto.ToString();
                                            //        datos[2] = "0";
                                            //        datos[3] = string.Empty;
                                            //        datos[4] = txtCantPaqServ.Text;
                                            //        cn.EjecutarConsulta(cs.GuardarProductosServPaq(datos));
                                            //    }
                                            //}
                                            //}
                                            //    flowLayoutPanel2.Controls.Clear();
                                        }
                                        #endregion Final Seccion de Agregar desde Boton Producto y de Editar Producto
                                        #region  Inicio Seccion de Agregar desde XML
                                        if (DatosSourceFinal == 3)
                                        {
                                            idHistorialCompraProducto = 0;
                                            found = 10;
                                            fechaXML = FechaXMLNvoProd;
                                            fecha = fechaXML.Substring(0, found);
                                            hora = fechaXML.Substring(found + 1);
                                            fechaCompleta = fecha + " " + hora;
                                            folio = FolioXMLNvoProd;
                                            RFCEmisor = RFCXMLNvoProd;
                                            nombreEmisor = NobEmisorXMLNvoProd;
                                            claveProdEmisor = ClaveProdEmisorXMLNvoProd;
                                            descuentoXML = DescuentoXMLNvoProd;
                                            PrecioCompraXMLNvoProd = txtPrecioCompra.Text;

                                            //Se obtiene la ID del último producto agregado
                                            idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));

                                            try
                                            {
                                                cn.EjecutarConsulta(cs.GuardarDetallesDelProducto(idProducto, FormPrincipal.userID, nameProveedorXML.ToString(), Convert.ToInt32(idProveedorXML.ToString())));
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al Agregar Detalles del " + FuenteServPaq + " Basico\n" + origen, "Error Agregar " + FuenteServPaq, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }

                                            //string query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto,IDUsuario) VALUES('{nombre}','{stock}','{precioOriginalConIVA.ToString("N2")}','{descuentoXML}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}', datetime('now', 'localtime'), '{Inventario.idReporte}', '{idProducto}','{FormPrincipal.userID}')";

                                            //string query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor,IDProducto,IDUsuario) VALUES('{nombre}','{stock}','{precioOriginalConIVA.ToString("N2")}','{descuentoXML}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}','{idProducto}','{FormPrincipal.userID}')";

                                            string query = $@"INSERT INTO HistorialCompras(Concepto, Cantidad, ValorUnitario, Descuento, Precio, FechaLarga, Folio, RFCEmisor, NomEmisor, ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto, IDUsuario) VALUES('{nombre}','{stock}','{PrecioCompraXMLNvoProd}','{descuentoXML}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{Inventario.idReporte}','{idProducto}','{FormPrincipal.userID}')";

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
                                                                buscar = $"SELECT ID, Nombre FROM Productos WHERE Nombre = '{comboBoxText}' AND IDUsuario = '{FormPrincipal.userID}' AND Status = '1'";
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
                                            flowLayoutPanel2.Controls.Clear();
                                        }

                                        using (DataTable dtProdDeServComb = cn.CargarDatos(cs.ProductosDeServicios(idProducto)))
                                        {
                                            if (!dtProdDeServComb.Rows.Count.Equals(0))
                                            {
                                                foreach (DataRow drProdServComb in dtProdDeServComb.Rows)
                                                {
                                                    var cantidadProducto = Convert.ToDecimal(drProdServComb["Cantidad"].ToString());
                                                    var NoProducto = Convert.ToInt32(drProdServComb["IDProducto"].ToString());
                                                    try
                                                    {
                                                        if (!this.Text.Equals("AGREGAR COMBOS"))
                                                        {
                                                            cn.EjecutarConsulta(cs.actualizarStockProdServCombo(cantidadProducto, NoProducto));
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        MessageBox.Show("Advertencia en el proceso de aumento de Stock de producto relacionado", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    }
                                                    finally
                                                    {
                                                        cantidadProducto = 0;
                                                        NoProducto = 0;
                                                    }
                                                }
                                            }
                                        }
                                        #endregion  Final Seccion de Agregar desde XML

                                        //Se obtiene la ID del último producto agregado
                                        idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));

                                        #region Inicio Sección proceso para guardar los detalles de facturación del producto
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
                                        if (datosImpuestos == null & DatosSourceFinal == 3 & AgregarStockXML.list_impuestos_traslado_retenido.Count() > 0)
                                        {
                                            guardar_impuestos_dexml(Convert.ToDouble(baseProducto), idProducto);
                                        }

                                        #endregion Final Sección proceso para guardar los detalles de facturación del producto

                                        #region Inicio Sección proceso para guardar el descuento del producto en caso de que se haya agregado uno
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
                                        #endregion Final Sección proceso para guardar el descuento del producto en caso de que se haya agregado uno
                                    }

                                    #region Inicio Sección Código de Barras Extras
                                    //// recorrido para FlowLayoutPanel para ver cuantos TextBox
                                    //foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
                                    //{
                                    //    // hacemos un objeto para ver que tipo control es
                                    //    foreach (Control item in panel.Controls)
                                    //    {
                                    //        // ver si el control es TextBox
                                    //        if (item is TextBox)
                                    //        {
                                    //            var tb = item.Text;         // almacenamos en la variable tb el texto de cada TextBox
                                    //            codigosBarrras.Add(tb);     // almacenamos en el List los codigos de barras
                                    //        }
                                    //    }
                                    //}
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
                                    #endregion Final Sección Código de Barras Extras

                                    if (this.Text.Trim().Equals("AGREGAR COMBOS"))
                                    {
                                        titulo = "Combo Guardado";
                                        texto = "Se guardo exitosamente el combo....";
                                        SeleccionaImagen = "correcto";
                                    }
                                    else if (this.Text.Trim().Equals("EDITAR COMBOS"))
                                    {
                                        titulo = "Combo Actualizado";
                                        texto = "Se actualizo exitosamente el combo....";
                                        SeleccionaImagen = "correcto";
                                    }
                                    else if (this.Text.Trim().Equals("COPIAR COMBOS"))
                                    {
                                        titulo = "Combo Copiado";
                                        texto = "Se copio exitosamente el combo....";
                                        SeleccionaImagen = "correcto";
                                    }
                                    else if (this.Text.Trim().Equals("AGREGAR SERVICIOS"))
                                    {
                                        titulo = "Servicio Agregado";
                                        texto = "Se guardo exitosamente el servicio....";
                                        SeleccionaImagen = "correcto";
                                    }
                                    else if (this.Text.Trim().Equals("EDITAR SERVICIOS"))
                                    {
                                        titulo = "Servicio Actualizado";
                                        texto = "Se actualizo exitosamente el servicio....";
                                        SeleccionaImagen = "correcto";
                                    }
                                    else if (this.Text.Trim().Equals("COPIAR SERVICIOS"))
                                    {
                                        titulo = "Servicio Copiado";
                                        texto = "Se copio exitosamente el servicio....";
                                        SeleccionaImagen = "correcto";
                                    }

                                    if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
                                    {
                                        MensajeAgregarEditarCopiarProducto MAECP = new MensajeAgregarEditarCopiarProducto(titulo, texto, SeleccionaImagen);
                                        MAECP.ShowDialog();
                                        //MessageBoxTemporal.Show(tituloVentana, "Aviso del Sistema", 2, false);
                                    }
                                    else if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
                                    {
                                        MensajeAgregarEditarCopiarProducto MAECP = new MensajeAgregarEditarCopiarProducto(titulo, texto, SeleccionaImagen);
                                        MAECP.ShowDialog();
                                        //MessageBoxTemporal.Show(tituloVentana, "Aviso del Sistema", 2, false);
                                    }

                                    //Cierra la ventana donde se agregan los datos del producto
                                    this.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al Agregar " + FuenteServPaq + "\n" + origen + ex.Message, "Error Agregar " + FuenteServPaq, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                #endregion Final de guardado de los datos principales del Servicios o Combos
                            }
                            else if (!bValidClaveInterna || !bValidCodigoBarras)
                            {
                                errorProvAgregarEditarProducto.SetError(txtClaveProducto, "Debe tener una Clave Interna\npara poder continuar el proceso.");
                                errorProvAgregarEditarProducto.SetError(txtCodigoBarras, "Debe tener un Código de Barras\npara poder continuar el proceso.");
                                MessageBox.Show("La Clave Interna ó Código de Barras\nNO DEBE ESTAR EN BLANCO", "Advertencia de llenado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            if (!bValidNombreProducto)
                            {
                                MessageBox.Show("Por favor poner Datos Validos\npara el campo de NOMBRE DE PRODUCTO", "Alerta del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtNombreProducto.Select(0, txtNombreProducto.Text.Length);
                                txtNombreProducto.Focus();
                            }
                            else if (!bValidPrecioProducto)
                            {
                                MessageBox.Show("Por favor poner Datos Validos\npara el campo de PRECIO DE VENTA", "Alerta del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtPrecioProducto.Select(0, txtPrecioProducto.Text.Length);
                                txtPrecioProducto.Focus();
                            }
                        }
                    }
                    #endregion Final de Sección de Combos y Servicios
                    #region Inicio de Sección de si NO ES Producto Combos y Servicios
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error al intentar registrar el producto", "Error de tipo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion Final de Sección de si NO ES Producto Combos y Servicios
                    /*	Fin del codigo de Alejandro	*/
                }
            }
            #endregion Final Sección De Agregar Producto, Combo ó Servicio Desde XML / Botón manual
            #region Inicio Sección De Editar Producto
            else if (DatosSourceFinal == 2)
            {
                if (SearchProdResult.Rows.Count != 0)
                {
                    bool bValidNombreProducto = ValidateNombreProducto();
                    bool bValidPrecioProducto = ValidatePrecioProducto();

                    if (bValidNombreProducto && bValidPrecioProducto)
                    {
                        bool bValidClaveInterna = ValidateClaveProducto();
                        bool bValidCodigoBarras = ValidateCodigoBarras();

                        if (bValidClaveInterna || bValidCodigoBarras)
                        {
                            errorProvAgregarEditarProducto.SetError(txtClaveProducto, "");
                            errorProvAgregarEditarProducto.SetError(txtCodigoBarras, "");

                            #region Inicio existencia de codigo de barra al actualizar
                            // Verificar existencia de codigo de barra al actualizar
                            if (mb.ComprobarCodigoClave(codigoB, FormPrincipal.userID, Convert.ToInt32(idProductoBuscado)))
                            {
                                MessageBox.Show($"El número de identificación {codigoB}\nya se esta utilizando como clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            #endregion Final existencia de codigo de barra al actualizar

                            #region Inicio existencia de clave interna al actualizar
                            // Verificar existencia de codigo de barra al actualizar
                            if (mb.ComprobarCodigoClave(claveIn, FormPrincipal.userID, Convert.ToInt32(idProductoBuscado)))
                            {
                                MessageBox.Show($"El número de identificación {claveIn}\nya se esta utilizando como clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            #endregion Final existencia de clave interna al actualizar

                            #region Inicio Codigo de barras extras
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

                            // Verificar si los codigos de barra extra ya existen al actualizar producto, servicio o paquete
                            if (codigosBarrras != null || codigosBarrras.Count != 0)
                            {
                                for (int pos = 0; pos < codigosBarrras.Count; pos++)
                                {
                                    var existe = mb.ComprobarCodigoClave(codigosBarrras[pos], FormPrincipal.userID, Convert.ToInt32(idProductoBuscado));

                                    if (existe)
                                    {
                                        MessageBox.Show($"El número de identificación {codigosBarrras[pos]}\nya se esta utilizando como clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        codigosBarrras.Clear();
                                        return;
                                    }
                                }
                            }

                            //label10.Text = idProductoBuscado;
                            if (SearchCodBarExtResult.Rows.Count != 0)
                            {
                                string deleteCodBarExt = $"DELETE FROM CodigoBarrasExtras WHERE IDProducto = '{idProductoBuscado}'";
                                cn.EjecutarConsulta(deleteCodBarExt);
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
                            #endregion Final Codigo de barras extras

                            var empleado = "0";

                            // Comprobar precio del producto para saber si se edito
                            var precioTmp = cn.BuscarProducto(Convert.ToInt32(idProductoBuscado), FormPrincipal.userID);
                            var precioNuevo = float.Parse(precio);
                            var precioAnterior = float.Parse(precioTmp[2]);

                            #region Incio Seccion Cambio de Precio
                            if (precioNuevo != precioAnterior)
                            {
                                string precioAnteriorTmp = precioAnterior.ToString();
                                string precioNuevoTmp = precioNuevo.ToString();

                                precioAnteriorTmp = precioAnteriorTmp.Replace(",", "");
                                precioNuevoTmp = precioNuevoTmp.Replace(",", "");

                                if (FormPrincipal.userNickName.Contains('@'))
                                {
                                    empleado = cs.buscarIDEmpleado(FormPrincipal.userNickName);
                                }

                                var datos = new string[] {
                                    FormPrincipal.userID.ToString(), empleado, idProductoBuscado,
                                    precioAnteriorTmp, precioNuevoTmp,
                                    "EDITAR PRODUCTO", fechaOperacion
                                };

                                // Se guarda historial del cambio de precio
                                cn.EjecutarConsulta(cs.GuardarHistorialPrecios(datos));

                                // Ejecutar hilo para enviar notificacion
                                var datosConfig = mb.ComprobarConfiguracion();

                                if (datosConfig.Count > 0)
                                {
                                    if (Convert.ToInt16(datosConfig[0]) == 1)
                                    {
                                        var configProducto = mb.ComprobarCorreoProducto(Convert.ToInt32(idProductoBuscado));

                                        if (configProducto.Count > 0)
                                        {
                                            if (configProducto[0] == 1)
                                            {
                                                datos = new string[] {
                                                    nombre, precioAnterior.ToString("N2"),
                                                    precioNuevo.ToString("N2"), "editar producto"
                                                };

                                                Thread notificacion = new Thread(
                                                    () => Utilidades.CambioPrecioProductoEmail(datos)
                                                );

                                                notificacion.Start();
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion Final Seccion Cambio de Precio

                            #region Inicio Seccion Descuentos
                            if (descuentos.Any())
                            {
                                FormAgregar.Close();
                                tipoDescuento = descuentos[0];
                            }

                            //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                            if (descuentos.Any())
                            {
                                // Se borra de las dos tablas en caso de que haya tenido un tipo de descuento diferente al que se actualizo
                                string deleteDescuentoCLiente = $"DELETE FROM DescuentoCliente WHERE IDProducto = '{idProductoBuscado}'";
                                cn.EjecutarConsulta(deleteDescuentoCLiente);

                                string deleteDescuentoMayoreo = $"DELETE FROM DescuentoMayoreo WHERE IDProducto = '{idProductoBuscado}'";
                                cn.EjecutarConsulta(deleteDescuentoMayoreo);

                                //Descuento por Cliente
                                if (descuentos[0] == "1")
                                {
                                    string[] guardar = new string[] { descuentos[1], descuentos[2], descuentos[3], descuentos[4] };
                                    cn.EjecutarConsulta(cs.GuardarDescuentoCliente(guardar, Convert.ToInt32(idProductoBuscado)));
                                }
                                //Descuento por Mayoreo
                                if (descuentos[0] == "2")
                                {
                                    foreach (var descuento in descuentos)
                                    {
                                        if (descuento == "2") { continue; }

                                        string[] tmp = descuento.Split('-');

                                        cn.EjecutarConsulta(cs.GuardarDescuentoMayoreo(tmp, Convert.ToInt32(idProductoBuscado)));
                                    }
                                }
                            }
                            #endregion Final Seccion Descuentos

                            if (!(this.Text.Trim() == "AGREGAR PRODUCTO" | this.Text.Trim() == "EDITAR PRODUCTO" | this.Text.Trim() == "COPIAR PRODUCTO"))
                            {
                                stock = "0";
                            }

                            queryUpdateProd = $@"UPDATE Productos SET Nombre = '{nombre}', 
                                                Stock = '{stock}', Precio = '{precio}', Categoria = '{categoria}',
                                                TipoDescuento = '{tipoDescuento}', ClaveInterna = '{claveIn}', 
                                                CodigoBarras = '{codigoB}', ClaveProducto = '{claveProducto}', 
                                                UnidadMedida = '{claveUnidadMedida}', ProdImage = '{logoTipo}',
                                                NombreAlterno1 = '{mg.RemoverCaracteres(nombre)}', 
                                                NombreAlterno2 = '{mg.RemoverPreposiciones(nombre)}', 
                                                StockNecesario = '{stockNecesario}', StockMinimo = '{stockMinimo}',
                                                PrecioMayoreo = '{precioMayoreo}'
                                                WHERE ID = '{idProductoBuscado}' AND IDUsuario = {FormPrincipal.userID}";

                            respuesta = cn.EjecutarConsulta(queryUpdateProd);

                            claveProducto = string.Empty;
                            claveUnidadMedida = string.Empty;


                            //  MIRI.
                            // °°°°°°°

                            if (AgregarDetalleFacturacionProducto.editado == true)
                            {
                                // Modifica tabla producto
                                string edit_p = $@"UPDATE Productos SET Base='{AgregarDetalleFacturacionProducto.tmp_edit_base}',
                                                IVA='{AgregarDetalleFacturacionProducto.tmp_edit_IVA}',
                                                Impuesto='{AgregarDetalleFacturacionProducto.tmp_edit_impuesto}'
                                                WHERE ID='{idProductoBuscado}' AND IDUsuario={FormPrincipal.userID}";

                                cn.EjecutarConsulta(edit_p);

                                AgregarDetalleFacturacionProducto.tmp_edit_base = 0;
                                AgregarDetalleFacturacionProducto.tmp_edit_IVA = 0;
                                AgregarDetalleFacturacionProducto.tmp_edit_impuesto = "";

                                // Busca si antes de la edición tenia impuestos extras, si es así entonces, los eliminará. 
                                bool existen_imp_extra = (bool)cn.EjecutarSelect($"SELECT * FROM detallesfacturacionproductos WHERE IDProducto='{idProductoBuscado}'", 0);

                                if (existen_imp_extra == true)
                                {
                                    int elimina_imp_extras = cn.EjecutarConsulta($"DELETE FROM detallesfacturacionproductos WHERE IDProducto= '{idProductoBuscado}'");
                                }

                                // Comprueba si hay impuestos extra, de ser así se procede al guardado.  
                                if (datosImpuestos != null)
                                {
                                    guardarDatosImpuestos(2);
                                }
                            }

                            /*//Se obtiene la ID del último producto agregado
                                        idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));
                                        var claveP = txtClaveProducto.Text;
                                        #region Inicio de Datos de Impuestos
                                        //Se realiza el proceso para guardar los detalles de facturación del producto
                                        if (datosImpuestos != null)
                                        {
                                            guardarDatosImpuestos();
                                        }*/


                            #region Inicio De Detalle Producto Basicos
                            bool isEmpty = !detalleProductoBasico.Any();

                            if (!isEmpty)
                            {
                                // Para guardar los detalles del producto
                                // Ejemplo: Proveedor, Categoria, Ubicacion, etc.
                                guardar = detalleProductoBasico.ToArray();
                                guardar[0] = idProductoBuscado.ToString();
                                cn.EjecutarConsulta(cs.GuardarProveedorDetallesDelProducto(guardar));
                            }
                            #endregion Final De Detalle Producto Basicos

                            #region Inicio de Seccion Productos
                            if (this.Text.Trim() == "AGREGAR PRODUCTO" | this.Text.Trim() == "EDITAR PRODUCTO" | this.Text.Trim() == "COPIAR PRODUCTO")
                            {
                                // Agregar la relacion de producto ya registrado con Combo Servicio
                                #region Agregar a tabla productosdeservicios
                                if (listaProductoToCombo.Count().Equals(1))
                                {
                                    if (!listaProductoToCombo[0].ToString().Equals(string.Empty))
                                    {
                                        if (this.Text.Trim() == "EDITAR PRODUCTO")
                                        {
                                            string[] datos = new string[5];
                                            foreach (var item in listaProductoToCombo)
                                            {
                                                datos = item.Split('|');
                                                string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                datos[0] = fech.Trim();
                                                string[] nuevosDatos = { datos[0], datos[1], datos[3], datos[4], datos[5] };
                                                cn.EjecutarConsulta(cs.insertarProductosServicios(nuevosDatos));
                                            }
                                            using (DataTable dtProductosDeServicios = cn.CargarDatos(cs.ObtenerProductosServPaq(datos[1].ToString())))
                                            {
                                                if (dtProductosDeServicios.Rows.Count > 1)
                                                {
                                                    cn.EjecutarConsulta(cs.borrarProdRelBlanco(Convert.ToInt32(datos[1].ToString())));
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                if (!CBNombProd.Equals("") || !CBIdProd.Equals(""))
                                {
                                    DateTime today = DateTime.Now;
                                    DataTable dtSearchServPaq;
                                    DataRow rowServPaq;
                                    dtSearchServPaq = cn.CargarDatos(cs.ProductosDeServicios(Convert.ToInt32(CBIdProd)));

                                    if (dtSearchServPaq.Rows.Count > 0)
                                    {
                                        rowServPaq = dtSearchServPaq.Rows[0];
                                        string[] tmp = { $"{today.ToString("yyyy-MM-dd hh:mm:ss")}", $"{CBIdProd}", $"{idProductoBuscado}", $"{nombre}", $"{rowServPaq["Cantidad"].ToString()}" };

                                        try
                                        {
                                            cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Algo ocurrio al tratar de guardar Productos que contienen los Combos/Servicios\n" + ex.Message.ToString(), "Advertencia del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }

                                        //string DeleteProdAtService = $"DELETE FROM ProductosDeServicios WHERE IDServicio = '{CBIdProd}' AND (IDProducto = '' AND NombreProducto = '')";
                                        //int DeleteProdAtPQS = cn.EjecutarConsulta(DeleteProdAtService);
                                        //if (DeleteProdAtPQS > 0)
                                        //{
                                        //MessageBox.Show("Productos Agregado al Paquete o Servicio", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //}
                                        //else
                                        //{
                                        //MessageBox.Show("Algo salio mal al intentar Agregar el\nProducto al Paquete o Servicio", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //}
                                    }

                                    // Limpiar para evitar error de relacionar producto a servicio 
                                    // y despues editar cualquier producto cualquiera
                                    CBIdProd = string.Empty;
                                    CBNombProd = string.Empty;
                                }
                            }
                            #endregion Final de Seccion Productos

                            if (this.Text.Trim().Equals("AGREGAR PRODUCTO"))
                            {
                                titulo = "Producto Agregado";
                                texto = "Se guardo exitosamente el producto....";
                                SeleccionaImagen = "correcto";
                            }
                            else if (this.Text.Trim().Equals("EDITAR PRODUCTO"))
                            {
                                titulo = "Producto Actualizado";
                                texto = "Se actualizo exitosamente el producto....";
                                SeleccionaImagen = "correcto";
                            }
                            else if (this.Text.Trim().Equals("COPIAR PRODUCTO"))
                            {
                                titulo = "Producto Copiado";
                                texto = "Se copio exitosamente el producto....";
                                SeleccionaImagen = "correcto";
                            }

                            #region Inicio de Seccion Combos y Servicios
                            else if (this.Text.Trim() == "AGREGAR COMBOS" | this.Text.Trim() == "EDITAR COMBOS" | this.Text.Trim() == "COPIAR COMBOS" || this.Text.Trim() == "AGREGAR SERVICIOS" | this.Text.Trim() == "EDITAR SERVICIOS" | this.Text.Trim() == "COPIAR SERVICIOS")
                            {
                                // recorrido para FlowLayoutPanel2 para ver cuantos TextBox
                                if (ProductosDeServicios.Count >= 1 || ProductosDeServicios.Count == 0)
                                {
                                    if (!ProductosDeServicios.Count.Equals(0))
                                    {
                                        foreach (var item in ProductosDeServicios)
                                        {
                                            var datos = item.Split('|');
                                            cn.EjecutarConsulta(cs.GuardarProductosServPaq(datos));
                                        }
                                    }
                                    //ProductosDeServicios.Clear();
                                    //// recorrido del panel de Prodcutos de Productos para ver cuantos Productos fueron seleccionados
                                    //foreach (Control panel in flowLayoutPanel2.Controls.OfType<FlowLayoutPanel>())
                                    //{
                                    //    // agregamos la variable para egregar los procutos
                                    //    string prodSerPaq = null;
                                    //    DataTable dtProductos;
                                    //    foreach (Control item in panel.Controls)
                                    //    {
                                    //        string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    //        string buscar = string.Empty;
                                    //        string comboBoxText = item.Text;
                                    //        string comboBoxValue = string.Empty;

                                    //        if (item is ComboBox)
                                    //        {
                                    //            if (item.Text != "Por favor selecciona un Producto")
                                    //            {
                                    //                if (!comboBoxText.Equals(string.Empty))
                                    //                {
                                    //                    buscar = $"SELECT ID, Nombre FROM Productos WHERE Nombre = '{comboBoxText}' AND IDUsuario = '{FormPrincipal.userID}' AND Status = '1'";
                                    //                    dtProductos = cn.CargarDatos(buscar);
                                    //                    comboBoxValue = dtProductos.Rows[0]["ID"].ToString();
                                    //                    prodSerPaq += fech + "|";
                                    //                    prodSerPaq += idProductoBuscado + "|";
                                    //                    prodSerPaq += comboBoxValue + "|";
                                    //                    prodSerPaq += comboBoxText + "|";
                                    //                }
                                    //            }
                                    //        }
                                    //        if (item is TextBox)
                                    //        {
                                    //            var tb = item.Text;
                                    //            if (item.Text == "0")
                                    //            {
                                    //                tb = "0";
                                    //                prodSerPaq += tb;
                                    //            }
                                    //            else
                                    //            {
                                    //                prodSerPaq += tb;
                                    //            }
                                    //        }
                                    //    }
                                    //    ProductosDeServicios.Add(prodSerPaq);
                                    //    prodSerPaq = null;
                                    //}
                                }
                                //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                                if (ProductosDeServicios.Any())
                                {
                                    string queryBorrarProductosDeServicios = $"DELETE FROM ProductosDeServicios WHERE IDServicio = '{idProductoBuscado}' AND IDProducto = 0 AND NombreProducto = ''";
                                    cn.EjecutarConsulta(queryBorrarProductosDeServicios);
                                    foreach (var productosSP in ProductosDeServicios)
                                    {
                                        string[] tmp = productosSP.Split('|');
                                        if (tmp.Length == 5)
                                        {
                                            using (DataTable dtProdComboServ = cn.CargarDatos(cs.verSiExisteRelacionRegistrada(tmp[1], tmp[2])))
                                            {
                                                if (dtProdComboServ.Rows.Count.Equals(0))
                                                {
                                                    cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                                                }
                                            }
                                        }
                                    }
                                    ProductosDeServicios.Clear();
                                }
                                flowLayoutPanel2.Controls.Clear();

                                if (this.Text.Trim().Equals("AGREGAR COMBOS"))
                                {
                                    titulo = "Combo Guardado";
                                    texto = "Se guardo exitosamente el combo....";
                                    SeleccionaImagen = "correcto";
                                }
                                else if (this.Text.Trim().Equals("EDITAR COMBOS"))
                                {
                                    titulo = "Combo Actualizado";
                                    texto = "Se actualizo exitosamente el combo....";
                                    SeleccionaImagen = "correcto";
                                }
                                else if (this.Text.Trim().Equals("COPIAR COMBOS"))
                                {
                                    titulo = "Combo Copiado";
                                    texto = "Se copio exitosamente el combo....";
                                    SeleccionaImagen = "correcto";
                                }
                                else if (this.Text.Trim().Equals("AGREGAR SERVICIOS"))
                                {
                                    titulo = "Servicio Agregado";
                                    texto = "Se guardo exitosamente el servicio....";
                                    SeleccionaImagen = "correcto";
                                }
                                else if (this.Text.Trim().Equals("EDITAR SERVICIOS"))
                                {
                                    titulo = "Servicio Actualizado";
                                    texto = "Se actualizo exitosamente el servicio....";
                                    SeleccionaImagen = "correcto";
                                }
                                else if (this.Text.Trim().Equals("COPIAR SERVICIOS"))
                                {
                                    titulo = "Servicio Copiado";
                                    texto = "Se copio exitosamente el servicio....";
                                    SeleccionaImagen = "correcto";
                                }
                            }
                            #endregion Final de Seccion Combos y Servicios

                            if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
                            {
                                MensajeAgregarEditarCopiarProducto MAECP = new MensajeAgregarEditarCopiarProducto(titulo,texto,SeleccionaImagen);
                                MAECP.ShowDialog();
                                //MessageBoxTemporal.Show(tituloVentana, "Aviso del Sistema", 2, false);
                            }
                            else if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
                            {
                                MensajeAgregarEditarCopiarProducto MAECP = new MensajeAgregarEditarCopiarProducto(titulo, texto, SeleccionaImagen);
                                MAECP.ShowDialog();
                                //MessageBoxTemporal.Show(tituloVentana, "Aviso del Sistema", 2, false);
                            }

                            // Cierra la ventana donde se agregan los datos del producto
                            this.Close();
                        }
                        else if (!bValidClaveInterna || !bValidCodigoBarras)
                        {
                            errorProvAgregarEditarProducto.SetError(txtClaveProducto, "Debe tener una Clave Interna\npara poder continuar el proceso.");
                            errorProvAgregarEditarProducto.SetError(txtCodigoBarras, "Debe tener un Código de Barras\npara poder continuar el proceso.");
                            MessageBox.Show("La Clave Interna ó Código de Barras\nNO DEBE ESTAR EN BLANCO", "Advertencia de llenado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        if (!bValidNombreProducto)
                        {
                            MessageBox.Show("Por favor poner Datos validos\npara el campo de NOMBRE DE PRODUCTO", "Alerta del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtNombreProducto.Select(0, txtNombreProducto.Text.Length);
                        }
                        else if (!bValidPrecioProducto)
                        {
                            MessageBox.Show("Por favor poner Datos validos\npara el campo de PRECIO DE VENTA", "Alerta del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtNombreProducto.Select(0, txtNombreProducto.Text.Length);
                        }
                    }
                }
            }
            #endregion Final Sección De Editar Producto
            #region Inicio Sección De Copiado Producto
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
                    #region Inicio de Inicializacion de Variables
                    /****************************
			        *	codigo de Alejandro		*
			        ****************************/
                    nombreNvoInsert = txtNombreProducto.Text;
                    stockNvoInsert = txtStockProducto.Text;
                    valorDePrecioVenta = Convert.ToDouble(txtPrecioProducto.Text);
                    precioNvoInsert = valorDePrecioVenta.ToString();
                    categoriaNvoInsert = txtCategoriaProducto.Text;
                    claveInNvoInsert = txtClaveProducto.Text.Trim();
                    codigoBNvoInsert = txtCodigoBarras.Text.Trim();
                    tipoDescuentoNvoInsert = "0";
                    idUsrNvoInsert = FormPrincipal.userID.ToString();
                    tipoProdNvoInsert = "";

                    if (cbTipo.Text == "Producto")
                    {
                        tipoProdNvoInsert = "P";
                    }
                    else
                    {
                        tipoProdNvoInsert = "S";
                    }
                    /*	Fin del codigo de Alejandro	*/
                    #endregion Final de Inicializacion de Variables

                    #region Inicio Busqueda Clave Interna
                    //Hacemos la busqueda que no se repita en CalveInterna
                    //searchClavIntProd();
                    if (mb.ComprobarCodigoClave(claveInNvoInsert, FormPrincipal.userID))
                    {
                        MessageBox.Show($"El número de identificación {claveIn}\nya se esta utilizando como clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                    #endregion Final Busqueda Clave Interna

                    #region Inicio Busqueda Codigo de Barras
                    //Hacemos la busqueda que no se repita en CodigoBarra
                    //searchCodBar();
                    if (mb.ComprobarCodigoClave(codigoBNvoInsert, FormPrincipal.userID))
                    {
                        MessageBox.Show($"El número de identificación {codigoB}\nya se esta utilizando como clave interna o\ncódigo de barras de algún producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                    #endregion Final Busqueda Codigo de Barras

                    guardar = new string[] {
                        nombreNvoInsert, stockNvoInsert, precioNvoInsert, categoriaNvoInsert, claveInNvoInsert,
                        codigoBNvoInsert, claveProducto, claveUnidadMedida, tipoDescuentoNvoInsert, idUsrNvoInsert,
                        logoTipo, tipoProdNvoInsert, baseProducto, ivaProducto, impuestoProducto, mg.RemoverCaracteres(nombreNvoInsert),
                        mg.RemoverPreposiciones(nombreNvoInsert), stockNecesario, stockMinimo, txtPrecioCompra.Text, precioMayoreo
                    };

                    #region Inicio de Guardado de Producto
                    try
                    {
                        //Se guardan los datos principales del producto
                        int respuesta = cn.EjecutarConsulta(cs.GuardarProducto(guardar, FormPrincipal.userID));

                        if (respuesta > 0)
                        {
                            claveProducto = string.Empty;
                            claveUnidadMedida = string.Empty;

                            //Se obtiene la ID del último producto agregado
                            idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));

                            #region Inicio Sección de Datos de Impuestos
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
                            #endregion Final  Sección de Datos de Impuestos

                            #region Inicio de Sección de Descuentos
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
                            #endregion Final de Sección de Descuentos

                            #region Inicio De Codigo de Barras Extra
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
                            #endregion Final De Codigo de Barras Extra

                            #region Inicio de Sección de Productos Del Servicio ó Combo
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
                            #endregion Final de Sección de Productos Del Servicio ó Combo

                            //Cierra la ventana donde se agregan los datos del producto
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ha ocurrido un error al intentar registrar el producto\n" + ex.Message.ToString(), "Advertencia Copiar Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    #endregion Final de Guardado de Producto
                }
            }
            #endregion Final  Sección De Copiado Producto
            /* Fin del codigo de Emmanuel */

            listaProductoToCombo.Clear();
            ProductosDeServicios.Clear();

            listaProductoToCombo = new List<string>();
            ProductosDeServicios = new List<string>();
        }

        private void validarDecimales(string valorEntrada)
        {
            bool esDouble;
            double valorSalida;
            int count;
            string[] valores;

            esDouble = Double.TryParse(valorEntrada, out valorSalida);

            if (esDouble)
            {
                count = getDecimalCount(valorSalida);
                valores = valorEntrada.Split('.');
                if (count > 0)
                {
                    parteEntera = valores[0].ToString();
                    parteDecimal = valores[1].ToString();
                }
                else
                {
                    parteEntera = valores[0].ToString();
                    parteDecimal = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("Sólo se aceptan numeros enteros y decimales\n", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private int getDecimalCount(double valorSalida)
        {
            int i = 0;
            while (Math.Round(valorSalida, i) != valorSalida)
            {
                i++;
            }
            return i;
        }

        private void guardarRelacionDeProductoConComboServicio()
        {
            // para relacionar productos con algun paquete/servicio
            int numero = 0;
            string cantidadProdAtService = string.Empty;
            if (int.TryParse(CBIdProd, out numero))
            {
                verificarProductosDeServicios(CBIdProd);
            }

            // Limpiar variables para evitar error de agregar servicio y despues editar producto
            CBIdProd = string.Empty;
            CBNombProd = string.Empty;

            var conceptoProveedor = string.Empty;
            var rfcProveedor = string.Empty;

            // Datos para la tabla historial de compras al momento de registrar
            // Un producto nuevo manualmente
            if (idProveedorBtnGuardar != null)
            {
                var proveedorTmp = mb.ObtenerDatosProveedor(Convert.ToInt32(idProveedorBtnGuardar), FormPrincipal.userID);
                conceptoProveedor = proveedorTmp[0];
                rfcProveedor = proveedorTmp[1];
            }

            guardar = new string[] { nombre, stock, txtPrecioCompra.Text, precio, fechaCompra, rfcProveedor, conceptoProveedor, "", "1", fechaOperacion, idReporte.ToString(), idProducto.ToString(), FormPrincipal.userID.ToString() };

            cn.EjecutarConsulta(cs.AjustarProducto(guardar, 1));

            int found = 10;
            DateTime date1 = DateTime.Now;
            string fechaCompleta = date1.ToString("s");
            string Year = fechaCompleta.Substring(0, found);
            string Date = fechaCompleta.Substring(found + 1);
            string FechaRegistrada = Year + " " + Date;
            string queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{FormPrincipal.userID}','{idProducto}','{FechaRegistrada}')";
            try
            {
                cn.EjecutarConsulta(queryRecordHistorialProd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar Agregar el Historial de Modificaciones del Producto, Combo o Servicio\n" + origen + "\nerror No: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void verificarProductosDeServicios(string cBIdProd)
        {
            using (dtServiciosPaquetes = cn.CargarDatos(cs.ProductosDeServicios(Convert.ToInt32(CBIdProd))))
            {
                if (dtServiciosPaquetes.Rows.Count != 0)
                {
                    rowServPaq = dtServiciosPaquetes.Rows[0];
                    cantidadProdAtService = rowServPaq["Cantidad"].ToString();
                    string[] SaveProdAtService = new string[] { $"{thisDay.ToString("yyyy-MM-dd hh:mm:ss")}", CBIdProd, Convert.ToString(idProducto), nombre, cantidadProdAtService };

                    try
                    {
                        int SaveProdAtPQS = cn.EjecutarConsulta(cs.GuardarProductosServPaq(SaveProdAtService));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar Agregar Producto al Combo o Servicio\n" + origen + "\nerror No: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    string DeleteProdAtService = $"DELETE FROM ProductosDeServicios WHERE IDServicio = '{CBIdProd}' AND (IDProducto = '' AND NombreProducto = '')";

                    try
                    {
                        int DeleteProdAtPQS = cn.EjecutarConsulta(DeleteProdAtService);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar Eliminar Producto al Combo o Servicio\n" + origen + "\nerror No: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (dtServiciosPaquetes.Rows.Count == 0)
                {
                    string[] SaveProdAtService = new string[] { $"{thisDay.ToString("yyyy-MM-dd hh:mm:ss")}", CBNombProd, Convert.ToString(idProducto), nombre, cantidadProdAtService };

                    try
                    {
                        int SaveProdAtPQS = cn.EjecutarConsulta(cs.GuardarProductosServPaq(SaveProdAtService));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar Agregar Producto al Combo o Servicio\n" + origen + "\nerror No: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void guardarDetallesProductoDinamicos(List<string> detalleProductoGeneral)
        {
            char delimiter = '|';
            string[] words;
            foreach (var itemList in detalleProductoGeneral)
            {
                guardar = detalleProductoGeneral.ToArray();
                foreach (var item in guardar)
                {
                    words = item.Split(delimiter);
                    saveDetailProd[0] = idProducto.ToString();
                    saveDetailProd[1] = words[1].ToString();
                    saveDetailProd[2] = words[2].ToString();
                    saveDetailProd[3] = words[3].ToString();
                    saveDetailProd[4] = words[4].ToString();
                    cn.EjecutarConsulta(cs.GuardarDetallesProductoGenerales(saveDetailProd));
                }
            }
        }

        private void guardarDetallesProductoBasico(List<string> detalleProductoBasico)
        {
            guardar = detalleProductoBasico.ToArray();
            guardar[0] = idProducto.ToString();
            cn.EjecutarConsulta(cs.GuardarProveedorDetallesDelProducto(guardar));
        }

        private void guardarDatosImpuestos(int accion = 0)
        {
            //Cerramos la ventana donde se eligen los impuestos
            FormDetalle.Close();

            string[] listaImpuestos = datosImpuestos.Split('|');

            int longitud = listaImpuestos.Length;
            int id_producto_i = idProducto;

            if (longitud > 0)
            {
                // Si la acción es igual a 2, entonces será una edición y el id del producto cambiara 
                if (accion == 2)
                {
                    id_producto_i = Convert.ToInt32(idProductoBuscado);
                }

                for (int i = 0; i < longitud; i++)
                {
                    string[] imp = listaImpuestos[i].Split(',');
                    if (imp[3] == " - ") { imp[3] = "0"; }
                    if (imp[4] == " - ") { imp[4] = "0"; }
                    if (imp[5] == " - ") { imp[5] = "0"; }
                    guardar = new string[] { imp[0], imp[1], imp[2], imp[3], imp[4], imp[5] };

                    try
                    {
                        cn.EjecutarConsulta(cs.GuardarDetallesProducto(guardar, id_producto_i));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al Agregar Detalles Facturación de Productos\n" + origen, "Error al Agregar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            datosImpuestos = null;
        }

        private void productoRegistradoCodigoBarras(string codigoB)
        {
            datosProductosBtnGuardar = new List<string>();
            string query = $"SELECT P.Nombre, P.ClaveInterna, P.CodigoBarras, P.Tipo, P.Status FROM Productos AS P WHERE P.IDUsuario = {FormPrincipal.userID} AND P.Status = 1 AND P.CodigoBarras = '{codigoB}'";

            using (DataTable dtProductoRegistrado = cn.CargarDatos(query))
            {
                if (dtProductoRegistrado.Rows.Count > 0)
                {
                    foreach (DataRow row in dtProductoRegistrado.Rows)
                    {
                        datosProductosBtnGuardar.Add("Nombre: " + row["Nombre"].ToString());
                        datosProductosBtnGuardar.Add("Código de Barras: " + row["CodigoBarras"].ToString());
                        if (row["Tipo"].ToString().Equals("P"))
                        {
                            datosProductosBtnGuardar.Add("El artículo es: Producto");
                        }
                        else if (row["Tipo"].ToString().Equals("PQ"))
                        {
                            datosProductosBtnGuardar.Add("El artículo es: Combo");
                        }
                        else if (row["Tipo"].ToString().Equals("S"))
                        {
                            datosProductosBtnGuardar.Add("El artículo es: Servicio");
                        }

                        if (row["Status"].ToString().Equals("1"))
                        {
                            datosProductosBtnGuardar.Add("El Status es: Activo");
                        }
                    }
                    MessageBox.Show($"El número de identificación {codigoB}\nya se esta utilizando en algún producto\n\n{datosProductosBtnGuardar[0].ToString()}\n{datosProductosBtnGuardar[1].ToString()}\n{datosProductosBtnGuardar[2].ToString()}\n{datosProductosBtnGuardar[3].ToString()}", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    datosProductosBtnGuardar.Clear();
                    resultadoSearchCodBar = 1;
                    return;
                }
                else if (dtProductoRegistrado.Rows.Count.Equals(0))
                {
                    query = string.Empty;

                    // Cargar procuto registrado con esa Código de Barras Extra
                    productoRegistradoCodigoBarrasExtra(FormPrincipal.userID, codigoB);
                }
            }
        }

        private void productoRegistradoClaveInterna(string claveIn)
        {
            datosProductosBtnGuardar = new List<string>();
            string query = $"SELECT P.Nombre, P.ClaveInterna, P.CodigoBarras, P.Tipo, P.Status FROM Productos AS P WHERE P.IDUsuario = {FormPrincipal.userID} AND P.Status = 1 AND P.ClaveInterna = '{claveIn}'";
            using (DataTable dtProductoRegistrado = cn.CargarDatos(query))
            {
                if (dtProductoRegistrado.Rows.Count > 0)
                {
                    foreach (DataRow row in dtProductoRegistrado.Rows)
                    {
                        datosProductosBtnGuardar.Add("Nombre: " + row["Nombre"].ToString());
                        datosProductosBtnGuardar.Add("Código buscado: " + row["ClaveInterna"].ToString());
                        if (row["Tipo"].ToString().Equals("P"))
                        {
                            datosProductosBtnGuardar.Add("El artículo es: Producto");
                        }
                        else if (row["Tipo"].ToString().Equals("PQ"))
                        {
                            datosProductosBtnGuardar.Add("El artículo es: Combo");
                        }
                        else if (row["Tipo"].ToString().Equals("S"))
                        {
                            datosProductosBtnGuardar.Add("El artículo es: Servicio");
                        }

                        if (row["Status"].ToString().Equals("1"))
                        {
                            datosProductosBtnGuardar.Add("El Status es: Activo");
                        }
                    }
                    MessageBox.Show($"El número de identificación {claveIn}\nya se esta utilizando en algún producto\nComo Clave Interna, Código de Barras ó Codigo de Barras Extra\n\n{datosProductosBtnGuardar[0].ToString()}\n{datosProductosBtnGuardar[1].ToString()}\n{datosProductosBtnGuardar[2].ToString()}\n{datosProductosBtnGuardar[3].ToString()}", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    datosProductosBtnGuardar.Clear();
                    resultadoSearchNoIdentificacion = 1;
                    return;
                }
                else if (dtProductoRegistrado.Rows.Count.Equals(0))
                {
                    query = string.Empty;

                    query = $"SELECT CB.IDProducto FROM CodigoBarrasExtras CB INNER JOIN Productos P ON P.ID = CB.IDProducto WHERE P.IDUsuario = {FormPrincipal.userID} AND CB.CodigoBarraExtra = '{claveIn}'";

                    // Cargar procuto registrado con esa Código de Barras Extra
                    productoRegistradoCodigoBarrasExtra(FormPrincipal.userID, claveIn);
                }
            }
        }

        private void productoRegistradoCodigoBarrasExtra(int userID, string claveBuscar)
        {
            datosProductosBtnGuardar = new List<string>();
            datosProductoRelacionado = new List<string>();

            string query = $"SELECT CB.IDProducto FROM CodigoBarrasExtras CB INNER JOIN Productos P ON P.ID = CB.IDProducto WHERE P.IDUsuario = {userID} AND CB.CodigoBarraExtra = '{claveBuscar}'";

            using (DataTable dtCodigosBarraExtraProductos = cn.CargarDatos(query))
            {
                if (dtCodigosBarraExtraProductos.Rows.Count > 0)
                {
                    foreach (DataRow row in dtCodigosBarraExtraProductos.Rows)
                    {
                        datosProductosBtnGuardar.Add(row["IDProducto"].ToString());
                    }
                    using (DataTable dtProductoRelacionado = cn.CargarDatos($"SELECT P.Nombre, P.ClaveInterna, P.CodigoBarras, P.Tipo, P.Status FROM Productos AS P WHERE P.IDUsuario = {FormPrincipal.userID} AND P.ID = {datosProductosBtnGuardar[0].ToString()}"))
                    {
                        if (dtProductoRelacionado.Rows.Count > 0)
                        {
                            if (resultadoSearchCodBarExtra.Equals(0))
                            {
                                foreach (DataRow row in dtProductoRelacionado.Rows)
                                {
                                    datosProductoRelacionado.Add("Nombre: " + row["Nombre"].ToString());                                // datosProductoRelacionado[0]
                                    datosProductoRelacionado.Add("Clave Interna buscada: " + row["ClaveInterna"].ToString());           // datosProductoRelacionado[1]
                                    datosProductoRelacionado.Add("Código de Barras buscado: " + row["CodigoBarras"].ToString());        // datosProductoRelacionado[2]
                                    if (row["Tipo"].ToString().Equals("P"))
                                    {
                                        datosProductoRelacionado.Add("El artículo es: Producto");                                       // datosProductoRelacionado[3]
                                    }
                                    else if (row["Tipo"].ToString().Equals("PQ"))
                                    {
                                        datosProductoRelacionado.Add("El artículo es: Combo");                                          // datosProductoRelacionado[3]
                                    }
                                    else if (row["Tipo"].ToString().Equals("S"))
                                    {
                                        datosProductoRelacionado.Add("El artículo es: Servicio");                                       // datosProductoRelacionado[3]
                                    }

                                    if (row["Status"].ToString().Equals("1"))
                                    {
                                        datosProductoRelacionado.Add("El Status es: Activo");                                           // datosProductoRelacionado[4]
                                    }
                                }
                                MessageBox.Show($"El número de identificación {claveIn}\nya se esta utilizando en algún producto\n\n{datosProductoRelacionado[0].ToString()}\n{datosProductoRelacionado[1].ToString()}\n{datosProductoRelacionado[2].ToString()}\n{datosProductoRelacionado[3].ToString()}\n{datosProductoRelacionado[4].ToString()}", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                datosProductosBtnGuardar.Clear();
                                datosProductoRelacionado.Clear();
                                resultadoSearchCodBarExtra = 1;
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void saberOrigenProducto()
        {
            if (DatosSourceFinal.Equals(1))
            {
                origen = "Agregado de Forma Manual";
            }
            else if (DatosSourceFinal.Equals(2))
            {
                origen = "Edición del Producto";
            }
            else if (DatosSourceFinal.Equals(3))
            {
                origen = "Agregado desde XML";
            }
            else if (DatosSourceFinal.Equals(4))
            {
                origen = "Copia del Producto, Servicio/Combo";
            }
        }

        private void validarCambioProducto()
        {
            if (idProductoCambio > 0)
            {
                var respuesta = MessageBox.Show("¿Estás seguro de realizar el cambio?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    // Va a deshabilitar
                    cn.EjecutarConsulta($"UPDATE Productos SET Status = 0 WHERE ID = {idProductoCambio} AND IDUsuario = {FormPrincipal.userID}");
                }
                else
                {
                    return;
                }
            }
        }

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cadAux = string.Empty;
            //resetControlesTableGridLayout();
            filtro = Convert.ToString(cbTipo.SelectedItem);      // tomamos el valor que se elige en el TextBox

            if (DatosSourceFinal.Equals(3))
            {
                if (filtro == "Producto")                            // comparamos si el valor a filtrar es Producto
                {
                    agregarProducto();
                    Titulo = "Agregar Producto";
                    cadAux = TituloForm.Substring(8);   // extraemos que tipo es (Producto)
                    TituloForm = Titulo;
                    tituloSeccion.Text = TituloForm;
                    this.Text = Titulo.ToUpper();
                    chkBoxConProductos.Checked = false;
                    chkBoxConProductos.Visible = false;

                    if (!ProdNombre.Equals(""))
                    {
                        cargarDatos();
                        ocultarPanel();
                        txtCantPaqServ.Visible = false;
                        lblCantPaqServ.Text = "Relacionar con \nPaquete/Servicio";
                        button1.Text = "Relacionar con \ncombo/servicio";
                    }
                    else if (ProdNombre.Equals(""))
                    {
                        LimpiarCampos();
                        if (DatosSourceFinal == 5)
                        {
                            cargarDatos();
                        }
                        else
                        {
                            cargarDatosNvoProd();
                        }
                        cbTipo.Text = "Producto";
                        ocultarPanel();
                        txtCantPaqServ.Visible = false;
                        lblCantPaqServ.Text = "Relacionar con \nCombo/Servicio";
                        button1.Text = "Relacionar con \ncombo/servicio";
                    }
                }
                else if (filtro == "Combo")                    // comparamos si el valor a filtrar es Servicio / Paquete ó Combo
                {
                    toolTip1.SetToolTip(lblCantCombServ, "En este apartado podrá indicarle al sistema la cantidad\nde productos que se descontaran al venderse un Combo");
                    agregarCombo();
                    Titulo = "Agregar Combos";
                    TituloForm = Titulo;
                    cadAux = TituloForm.Substring(7);   // extraemos que tipo es (Combo)
                    tituloSeccion.Text = TituloForm;
                    this.Text = Titulo.ToUpper();
                    chkBoxConProductos.Checked = false;

                    lblCantPaqServ.Text = "Cantidad por combo";
                    button1.Text = "Productos";

                    // Miri.
                    // El combo ya no será visible para cuando un combo se le quiera relacionar productos. 
                    if (DatosSourceFinal != 3)
                    {
                        chkBoxConProductos.Visible = true;
                    }

                    if (!string.IsNullOrWhiteSpace(ProdStock) && !string.IsNullOrWhiteSpace(PrecioCompraXML))
                    {
                        var ProdStockXML = Convert.ToDecimal(ProdStock.ToString());
                        var CostoCompraXML = Convert.ToDecimal(PrecioCompraXML.ToString());

                        var precioVentaComboServicio = CostoCompraXML * ProdStockXML;
                        precioVentaComboServicio = Decimal.Ceiling(precioVentaComboServicio);

                        txtPrecioCompra.Text = Convert.ToString(precioVentaComboServicio);
                        txtPrecioCompra.Focus();
                        txtPrecioProducto.Focus();
                        txtCantPaqServ.Text = Convert.ToString(ProdStockXML);
                        txtCodigoBarras.Clear();
                        txtCodigoBarras.Focus();
                    }
                }
                else if (filtro == "Servicio")                    // comparamos si el valor a filtrar es Servicio / Paquete ó Combo
                {
                    toolTip1.SetToolTip(lblCantCombServ, "En este apartado podrá indicarle al sistema la cantidad\nde productos que se descontaran al venderse un Servicio");
                    agregarServicio();
                    Titulo = "Agregar Servicios";
                    TituloForm = Titulo;
                    cadAux = TituloForm.Substring(7);   // extraemos que tipo es (Servicio)
                    tituloSeccion.Text = TituloForm;
                    this.Text = Titulo.ToUpper();
                    chkBoxConProductos.Checked = false;
                    chkBoxConProductos.Visible = true;

                    lblCantPaqServ.Text = "Cantidad por servicio";
                    button1.Text = "Productos";

                    if (!string.IsNullOrWhiteSpace(ProdStock) && !string.IsNullOrWhiteSpace(PrecioCompraXML))
                    {
                        var ProdStockXML = Convert.ToDecimal(ProdStock.ToString());
                        var CostoCompraXML = Convert.ToDecimal(PrecioCompraXML.ToString());

                        var precioVentaComboServicio = CostoCompraXML * ProdStockXML;
                        precioVentaComboServicio = Decimal.Ceiling(precioVentaComboServicio);

                        txtPrecioCompra.Text = Convert.ToString(precioVentaComboServicio);
                        txtPrecioCompra.Focus();
                        txtPrecioProducto.Focus();
                        txtCantPaqServ.Text = Convert.ToString(ProdStockXML);
                        txtCodigoBarras.Clear();
                        txtCodigoBarras.Focus();
                    }
                }
            }
        }

        private void resetControlesTableGridLayout()
        {
            label1.Visible = false;
            txtStockMinimo.Visible = false;

            label12.Visible = false;
            txtStockMaximo.Visible = false;

            label6.Visible = false;
            txtStockProducto.Visible = false;

            label7.Visible = false;
            txtPrecioCompra.Visible = false;

            label4.Visible = false;
            txtPrecioProducto.Visible = false;

            label5.Visible = false;
            txtClaveProducto.Visible = false;

            label2.Visible = false;
            txtCodigoBarras.Visible = false;
            btnGenerarCB.Visible = false;
            panelContenedor.Visible = false;

            PImagen.Visible = false;

            lblCantPaqServ.Visible = false;
            button1.Visible = false;

            txtCantPaqServ.Visible = false;
        }

        private void ocultarPanel()
        {
            if (Hided)  // Si su valor es True
            {
                timerProdPaqSer.Start();
            }
            else  // Si su valor es False
            {
                timerProdPaqSer.Start();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //LoadPanelDatos();
            //MessageBox.Show("Mensaje desde:\n" + this.Text, "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ConsultarListaRelacionados consultarListaRelacion = new ConsultarListaRelacionados();
            consultarListaRelacion.FormClosing += delegate
            {
                if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(3))
                {
                    listaProductoToCombo = consultarListaRelacion.listaServCombo;
                    ProductosDeServicios = consultarListaRelacion.listaProd;
                    mostrarOcultarLblArrow();
                }
                mostrarOcultarLblArrow();
            };

            consultarListaRelacion.listaServCombo = listaProductoToCombo;
            consultarListaRelacion.listaProd = ProductosDeServicios;
            consultarListaRelacion.tipoOperacion = this.Text;
            consultarListaRelacion.DatosSourceFinal = DatosSourceFinal;
            if (DatosSourceFinal.Equals(2))
            {
                consultarListaRelacion.idProdEdit = Convert.ToInt32(idEditarProducto);
            }
            consultarListaRelacion.ShowDialog();
        }

        private void LoadPanelDatos()
        {
            if (Hided)  // Si su valor es True
            {
                ocultarPanel();
                btnAdd.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\angle-double-up.png");
                //MessageBox.Show("Valor true", "Valor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else  // Si su valor es False
            {
                ocultarPanel();
                btnAdd.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\angle-double-down.png");
                //MessageBox.Show("Valor false", "Valor", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!txtCantPaqServ.Equals("0"))
                {
                    tb.Text = txtCantPaqServ.Text;
                }
                else if (txtCantPaqServ.Equals("0"))
                {
                    tb.Text = txtCantPaqServ.Text;
                }
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
            }
            else if (CBNombProd == "" || CBNombProd == null)
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
                        activo = 1;
                        return;
                    }
                    else if (nombreForm.Contains("NvoProduct") == true)
                    {
                        activo = 0;
                        return;
                    }
                }
                if (activo == 0)
                {
                    queryNvoProductosDeServicios = $"SELECT * FROM Productos WHERE Nombre = '{nvoProductoAdd.ProdNombre}'";
                    dtNvoProductosDeServicios = cn.CargarDatos(queryNvoProductosDeServicios);
                }
                else if (activo == 1)
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

            string queryProdSearch,
                    queryProdServFound,
                    queryProdUpdate,
                    queryProdServUpdate,
                    fech,
                    prodSerPaq,
                    buscar,
                    comboBoxText,
                    comboBoxValue;

            DataTable dtProdFound,
                        dtProdSerFound,
                        rowProdUpdate,
                        dtProductos;

            DataRow rowProdFound,
                    row;

            queryProdSearch = $"SELECT * FROM Productos WHERE Nombre = '{nombre}' AND Precio = '{precio}'";
            dtProdFound = cn.CargarDatos(queryProdSearch);
            rowProdFound = dtProdFound.Rows[0];
            queryProdServFound = $"SELECT * FROM ProductosDeServicios WHERE IDServicio = '{rowProdFound["ID"].ToString()}'";
            idProducto = Convert.ToInt32(rowProdFound["ID"].ToString());
            dtProdSerFound = cn.CargarDatos(queryProdServFound);
            if (dtProdSerFound.Rows.Count >= 1)
            {
                queryProdUpdate = $"UPDATE Productos SET Nombre = '{nombre}', Precio = '{precio}', ClaveInterna = '{claveIn}', CodigoBarras = '{codigoB}', NombreAlterno1 = '{mg.RemoverCaracteres(nombre)}', NombreAlterno2 = '{mg.RemoverPreposiciones(nombre)}' WHERE ID = '{rowProdFound["ID"].ToString()}' AND IDUsuario = {FormPrincipal.userID}";
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
                queryProdUpdate = $"UPDATE Productos SET Nombre = '{nombre}', Precio = '{precio}', ClaveInterna = '{claveIn}', CodigoBarras = '{codigoB}', NombreAlterno1 = '{mg.RemoverCaracteres(nombre)}', NombreAlterno2 = '{mg.RemoverPreposiciones(nombre)}' WHERE ID = '{rowProdFound["ID"].ToString()}' AND IDUsuario = {FormPrincipal.userID}";
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

            if (DatosSourceFinal == 1 || DatosSourceFinal == 3 || DatosSourceFinal == 4)
            {
                //Verifica que el formulario ya tenga una instancia creada, de lo contrario la crea
                if (FormDetalleProducto == null)
                {
                    FormDetalleProducto = new AgregarDetalleProducto();
                    //FormDetalleProducto.typeDatoProveedor = 1;
                    FormDetalleProducto.origenProdServCombo = DatosSourceFinal;
                    FormDetalleProducto.getIdProducto = idProductoFinal;
                    if (DatosSourceFinal.Equals(3))
                    {
                        FormDetalleProducto.nameXMLProveedor = nameProveedorXML;
                    }
                    FormDetalleProducto.Show();
                    FormDetalleProducto.BringToFront();
                }
                else
                {
                    //FormDetalleProducto.typeDatoProveedor = 1;
                    FormDetalleProducto.origenProdServCombo = DatosSourceFinal;
                    FormDetalleProducto.getIdProducto = idProductoFinal;
                    if (DatosSourceFinal.Equals(3))
                    {
                        FormDetalleProducto.nameXMLProveedor = nameProveedorXML;
                    }
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
                    FormDetalleProducto.origenProdServCombo = DatosSourceFinal;
                    FormDetalleProducto.getIdProducto = idProductoFinal;
                    FormDetalleProducto.Show();
                    FormDetalleProducto.BringToFront();
                }
                else
                {
                    FormDetalleProducto.origenProdServCombo = DatosSourceFinal;
                    FormDetalleProducto.getIdProducto = idProductoFinal;
                    FormDetalleProducto.Show();
                    FormDetalleProducto.BringToFront();
                }
            }
        }

        private void txtPrecioCompra_KeyDown(object sender, KeyEventArgs e)
        {
            float procedimiento;
            if (e.KeyCode == Keys.Enter)
            {
                string[] words;

                if (txtPrecioCompra.Text.Equals(""))
                {
                    txtPrecioCompra.Text = "0";
                }
                else if (!txtPrecioCompra.Text.Equals(""))
                {
                    words = txtPrecioCompra.Text.Split('.');
                    if (words[0].Equals(""))
                    {
                        words[0] = "0";
                    }
                    if (words.Length > 1)
                    {
                        if (words[1].Equals(""))
                        {
                            words[1] = "00";
                        }
                        txtPrecioCompra.Text = words[0] + "." + words[1];
                    }
                }
                precioOriginalConIVA = (float)Convert.ToDouble(txtPrecioCompra.Text);
                procedimiento = (porcentajeGanancia / 100) + 1;
                PrecioRecomendado = precioOriginalConIVA * procedimiento;

                decimal cantidadPrecioProducto = 0;
                bool siEsNumero = false;

                siEsNumero = Decimal.TryParse(txtPrecioProducto.Text, out cantidadPrecioProducto);

                if (siEsNumero.Equals(true))
                {
                    if (precioOriginalConIVA > 0)
                    {
                        txtPrecioProducto.Text = PrecioRecomendado.ToString("N2");
                    }
                }
                
                txtPrecioProducto.Focus();
                txtPrecioProducto.Select(txtPrecioProducto.Text.Length, 0);
            }
        }

        private void txtPrecioCompra_Leave(object sender, EventArgs e)
        {
            float procedimiento;
            string[] words;

            if (txtPrecioCompra.Text.Equals(""))
            {
                txtPrecioCompra.Text = "0";
            }
            else if (!txtPrecioCompra.Text.Equals(""))
            {
                words = txtPrecioCompra.Text.Split('.');
                if (words[0].Equals(""))
                {
                    words[0] = "0";
                }
                if (words.Length > 1)
                {
                    if (words[1].Equals(""))
                    {
                        words[1] = "00";
                    }
                    txtPrecioCompra.Text = words[0] + "." + words[1];
                }
            }
            precioOriginalConIVA = (float)Convert.ToDouble(txtPrecioCompra.Text);
            procedimiento = (porcentajeGanancia / 100) + 1;
            PrecioRecomendado = precioOriginalConIVA * procedimiento;

            decimal cantidadPrecioProducto = 0;
            bool siEsNumero = false;

            siEsNumero = Decimal.TryParse(txtPrecioProducto.Text, out cantidadPrecioProducto);

            if (siEsNumero.Equals(true))
            {
                if (precioOriginalConIVA > 0)
                {
                    txtPrecioProducto.Text = PrecioRecomendado.ToString("N2");
                }
                else
                {
                    txtPrecioProducto.Text = "0";
                }
            }
            
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
            //if (e.KeyChar == 8)     // tecla BackSpace
            //{
            //    e.Handled = false;
            //    return;
            //}
            bool IsDec = false;
            int nroDec = 0;
            //for (int i = 0; i < txtCantPaqServ.Text.Length; i++)       // recorrer la caja de texto
            //{
            //    if (txtCantPaqServ.Text[i] == '.')      // ver si es un punto decimal
            //    {
            //        IsDec = true;
            //    }
            //    if (IsDec && nroDec++ >= 2)     // incrementar la variable nroDec
            //    {
            //        e.Handled = true;
            //        return;
            //    }
            //}
            if (e.KeyChar >= 48 && e.KeyChar <= 57)     // teclas del 0 hasta el 9
            {
                e.Handled = false;
            }
            else if (e.KeyChar == 46)   // tecla punto decimal " . "
            {
                e.Handled = (IsDec) ? true : false;
            }
            else if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
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

            if (this.Text.Trim().Equals("AGREGAR PRODUCTO") ||
                this.Text.Trim().Equals("EDITAR PRODUCTO") ||
                this.Text.Trim().Equals("COPIAR PRODUCTO"))
            {
                ListStock.TypeStock = "Combos";

                if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
                {
                    ListStock.listaServCombo = listaProductoToCombo;
                    ListStock.listaProd = ProductosDeServicios;
                }

                if (DatosSourceFinal.Equals(2))
                {
                    if (!Convert.ToInt32(idEditarProducto).Equals(0))
                    {
                        ListStock.idProdEdit = Convert.ToInt32(idEditarProducto);
                    }
                }
            }
            else if (this.Text.Trim().Equals("AGREGAR COMBOS") ||
                    this.Text.Trim().Equals("EDITAR COMBOS") ||
                    this.Text.Trim().Equals("COPIAR COMBOS") ||
                    this.Text.Trim().Equals("AGREGAR SERVICIOS") ||
                    this.Text.Trim().Equals("EDITAR SERVICIOS") ||
                    this.Text.Trim().Equals("COPIAR SERVICIOS"))
            {
                ListStock.TypeStock = "Productos";

                if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
                {
                    ListStock.listaServCombo = listaProductoToCombo;
                    ListStock.listaProd = ProductosDeServicios;
                }

                if (DatosSourceFinal.Equals(2))
                {
                    if (!Convert.ToInt32(idEditarProducto).Equals(0))
                    {
                        ListStock.idProdEdit = Convert.ToInt32(idEditarProducto);
                    }
                }
            }

            ListStock.DatosSourceFinal = DatosSourceFinal;
            ListStock.ShowDialog();
        }

        private void ejecutar(string nombProd_Paq_Serv, string id_Prod_Paq_Serv)
        {
            CBNombProd = nombProd_Paq_Serv;
            CBIdProd = id_Prod_Paq_Serv;
            int numero = 0;
            if (!int.TryParse(CBNombProd, out numero))
            {
                if ((this.Text.Trim().Equals("AGREGAR COMBOS") ||
                     this.Text.Trim().Equals("AGREGAR SERVICIOS")) &&
                     (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3)))
                {
                    string prodSerPaq = null;
                    DataTable dtProductos;
                    string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string buscar = null;
                    buscar = $"SELECT ID, Nombre FROM Productos WHERE Nombre = '{CBNombProd}' AND ID = '{CBIdProd}' AND IDUsuario = '{FormPrincipal.userID}'";

                    using (dtProductos = cn.CargarDatos(buscar))
                    {
                        if (!dtProductos.Rows.Count.Equals(0))
                        {
                            DataRow row = dtProductos.Rows[0];
                            prodSerPaq += fech + "|";
                            prodSerPaq += "|";
                            prodSerPaq += row["ID"].ToString() + "|";
                            prodSerPaq += row["Nombre"].ToString() + "|";
                            prodSerPaq += txtCantPaqServ.Text;

                            ProductosDeServicios.Add(prodSerPaq);
                            prodSerPaq = null;
                        }
                    }

                    //btnAdd.Visible = true;
                    CargarDatos();
                    if (flowLayoutPanel2.Visible == true)
                    {
                        Hided = true;
                        ocultarPanel();
                    }
                    GenerarPanelProductosServPlus();
                }
                else if ((this.Text.Trim().Equals("EDITAR COMBOS") ||
                          this.Text.Trim().Equals("COPIAR COMBOS") ||
                          this.Text.Trim().Equals("EDITAR SERVICIOS") ||
                          this.Text.Trim().Equals("COPIAR SERVICIOS")) && DatosSourceFinal.Equals(2))
                {
                    //string[] tmp = { $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}", $"{idEditarProducto}", $"{CBIdProd}", $"{CBNombProd}", $"{txtCantPaqServ.Text}" };
                    //cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                    //string queryDeleteProductosServPaq = $"DELETE FROM ProductosDeServicios WHERE IDServicio = '{idEditarProducto}' AND NombreProducto = ''";
                    //cn.EjecutarConsulta(queryDeleteProductosServPaq);
                    string prodSerPaq = null;
                    DataTable dtProductos;
                    string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string buscar = null;
                    buscar = $"SELECT ID, Nombre FROM Productos WHERE Nombre = '{CBNombProd}' AND ID = '{CBIdProd}' AND IDUsuario = '{FormPrincipal.userID}'";

                    using (dtProductos = cn.CargarDatos(buscar))
                    {
                        if (!dtProductos.Rows.Count.Equals(0))
                        {
                            DataRow row = dtProductos.Rows[0];
                            prodSerPaq += fech + "|";
                            prodSerPaq += idEditarProducto + "|";
                            prodSerPaq += row["ID"].ToString() + "|";
                            prodSerPaq += row["Nombre"].ToString() + "|";
                            prodSerPaq += txtCantPaqServ.Text;

                            ProductosDeServicios.Add(prodSerPaq);
                            prodSerPaq = null;
                        }
                    }
                    //btnAdd.Visible = true;
                    CargarDatos();
                    if (flowLayoutPanel2.Visible == true)
                    {
                        Hided = true;
                        ocultarPanel();
                    }
                    GenerarPanelProductosServPlus();
                }
                else if (this.Text.Trim().Equals("AGREGAR PRODUCTO") &&
                        (DatosSourceFinal.Equals(1) ||
                         DatosSourceFinal.Equals(3))) // Se agrega el DatosSourceFinal= 3 para que también aplique cuando es desde XML. Miri.
                {
                    using (DataTable dtDatosComboServicio = cn.CargarDatos(cs.getDatosServCombo(CBIdProd)))
                    {
                        string fechaServCombo = string.Empty,
                                idServCombo = string.Empty,
                                cantidadServCombo = string.Empty,
                                idProducto = string.Empty,
                                nombreProducto = string.Empty;

                        if (!dtDatosComboServicio.Rows.Count.Equals(0))
                        {
                            foreach (DataRow drComboServ in dtDatosComboServicio.Rows)
                            {
                                fechaServCombo = drComboServ["Fecha"].ToString();
                                idServCombo = drComboServ["IDServicio"].ToString();
                                cantidadServCombo = drComboServ["Cantidad"].ToString();
                            }

                            using (DataTable dtDatosProductos = cn.CargarDatos(cs.getDatosProducto(idEditarProducto)))
                            {
                                if (!dtDatosProductos.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow drProducto in dtDatosProductos.Rows)
                                    {
                                        idProducto = drProducto["ID"].ToString();
                                        nombreProducto = drProducto["Nombre"].ToString();
                                    }
                                }
                            }
                        }
                        if (dtDatosComboServicio.Rows.Count.Equals(0))
                        {
                            fechaServCombo = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt");
                            idServCombo = CBIdProd;
                            cantidadServCombo = "1.00";
                        }
                        listaProductoToCombo.Add(fechaServCombo + "|" + idServCombo + "|" + idProducto + "|" + nombreProducto + "|" + cantidadServCombo);
                    }
                }
                else if (this.Text.Trim().Equals("EDITAR PRODUCTO") && DatosSourceFinal.Equals(2))
                {
                    //using (DataTable dtDatosComboServicio = cn.CargarDatos(cs.getDatosServCombo(CBIdProd)))
                    //{
                    //    string fechaServCombo = string.Empty,
                    //            idServCombo = string.Empty,
                    //            cantidadServCombo = string.Empty,
                    //            idProducto = string.Empty,
                    //            nombreProducto = string.Empty;

                    //    if (!dtDatosComboServicio.Rows.Count.Equals(0))
                    //    {
                    //        foreach (DataRow drComboServ in dtDatosComboServicio.Rows)
                    //        {
                    //            fechaServCombo = drComboServ["Fecha"].ToString();
                    //            idServCombo = drComboServ["IDServicio"].ToString();
                    //            cantidadServCombo = drComboServ["Cantidad"].ToString();
                    //        }

                    //        using (DataTable dtDatosProductos = cn.CargarDatos(cs.getDatosProducto(idEditarProducto)))
                    //        {
                    //            if (!dtDatosProductos.Rows.Count.Equals(0))
                    //            {
                    //                foreach (DataRow drProducto in dtDatosProductos.Rows)
                    //                {
                    //                    idProducto = drProducto["ID"].ToString();
                    //                    nombreProducto = drProducto["Nombre"].ToString();
                    //                }
                    //            }
                    //        }
                    //    }
                    //    if (dtDatosComboServicio.Rows.Count.Equals(0))
                    //    {
                    //        fechaServCombo = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt");
                    //        idServCombo = CBIdProd;
                    //        cantidadServCombo = "1.00";
                    //    }
                    //    listaProductoToCombo.Add(fechaServCombo + "|" + idServCombo + "|" + idProducto + "|" + nombreProducto + "|" + cantidadServCombo);
                    //}
                    string prodSerPaq = null;
                    DataTable dtComboServicio;
                    string fech = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string buscar = null;
                    buscar = $"SELECT ID, Nombre FROM Productos WHERE Nombre = '{CBNombProd}' AND ID = '{CBIdProd}' AND IDUsuario = '{FormPrincipal.userID}'";

                    using (dtComboServicio = cn.CargarDatos(buscar))
                    {
                        if (!dtComboServicio.Rows.Count.Equals(0))
                        {
                            DataRow row = dtComboServicio.Rows[0];
                            prodSerPaq += fech + "|";
                            //prodSerPaq += idEditarProducto + "|";
                            //prodSerPaq += row["ID"].ToString() + "|";
                            //prodSerPaq += row["Nombre"].ToString() + "|";
                            prodSerPaq += row["ID"].ToString() + "|";
                            prodSerPaq += row["Nombre"].ToString() + "|";
                            using (DataTable dtIdEditarProducto = cn.CargarDatos(cs.obtenerNombreDelProducto(idEditarProducto)))
                            {
                                if (!dtIdEditarProducto.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow item in dtIdEditarProducto.Rows)
                                    {
                                        prodSerPaq += item["ID"].ToString() + "|";
                                        prodSerPaq += item["Nombre"].ToString() + "|";
                                    }
                                }
                            }
                            using (DataTable dtProductosDeServicio = cn.CargarDatos(cs.obtenerCantidadProductosDeServicios(row["ID"].ToString())))
                            {
                                if (!dtProductosDeServicio.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow item in dtProductosDeServicio.Rows)
                                    {
                                        prodSerPaq += item["Cantidad"].ToString();
                                    }
                                }
                            }

                            listaProductoToCombo.Add(prodSerPaq);
                            prodSerPaq = null;
                        }
                    }
                }
            }

            // Limpiar para evitar error de relacionar producto a servicio 
            // y despues editar cualquier producto cualquiera
            CBIdProd = string.Empty;
            CBNombProd = string.Empty;

            mostrarOcultarLblArrow();
        }

        private void AgregarEditarProducto_Paint(object sender, PaintEventArgs e)
        {
            if (ejecutarMetodos)
            {
                if (this.Text.Trim() == "AGREGAR COMBOS" | this.Text.Trim() == "EDITAR COMBOS" | this.Text.Trim() == "COPIAR COMBOS" || this.Text.Trim() == "AGREGAR SERVICIOS" | this.Text.Trim() == "EDITAR SERVICIOS" | this.Text.Trim() == "COPIAR SERVICIOS")
                {
                    if (Hided)  // Si su valor es True
                    {
                        ocultarPanel();
                        btnAdd.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\angle-double-up.png");
                    }
                    else  // Si su valor es False
                    {
                        ocultarPanel();
                        btnAdd.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\angle-double-down.png");
                    }
                }
                ejecutarMetodos = false;
            }
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
            float priceCompra = (float)Convert.ToDouble(PrecioCompraXML) / Convert.ToUInt32(CantProdServFinal);
            nvoProductoAdd.ProdPrecioCompra = priceCompra.ToString();
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
            nvoProductoAdd.IdNvoProveedorXML = idProveedorXML;
            nvoProductoAdd.NameNvoProveedorXML = nameProveedorXML;
        }

        private void txtCategoriaProducto_TextChanged(object sender, EventArgs e)
        {
            txtCategoriaProducto.CharacterCasing = CharacterCasing.Upper;
        }

        private void CargarDatos()
        {
            cargarCBProductos(idEditarProducto);
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
            txtStockProducto.SelectAll();
            _lastEnteredControl = (Control)sender;      // capturamos el ultimo control en el que estaba el Focus
        }

        private void txtPrecioProducto_Enter(object sender, EventArgs e)
        {

            int comprobar = 0;
            string idempleado = cs.buscarIDEmpleado(FormPrincipal.userNickName);

            using (DataTable dtUsuarios = cn.CargarDatos(cs.validarUsuario(FormPrincipal.userNickName)))
            {
                if (!dtUsuarios.Rows.Count.Equals(0))
                {

                }
                else
                {
                    using (DataTable dtEmpleadosPermisos = cn.CargarDatos(cs.condicionAsignar("Precio", idempleado)))
                    {
                        if (!dtEmpleadosPermisos.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtEmpleadosPermisos.Rows)
                            {
                                comprobar = Convert.ToInt32(item["total"]);
                            }
                        }
                    }
                    if (comprobar > 0)
                    {

                    }
                    else
                    {
                        txtPrecioProducto.Enabled = false;
                        MessageBox.Show("No cuentas con los permisos para modificar este dato", "Alerta Sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            txtPrecioProducto.SelectAll();
            _lastEnteredControl = (Control)sender;      // capturamos el ultimo control en el que estaba el Focus
        }

        private void txtCategoriaProducto_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;      // capturamos el ultimo control en el que estaba el Focus
        }

        private void AgregarEditarProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            Productos producto = Application.OpenForms.OfType<Productos>().FirstOrDefault();

            if (producto != null)
            {
                producto.retornoAgregarEditarProductoDatosSourceFinal = DatosSourceFinal;
                producto.recargarDGV();
            }

            LimpiarDatos();
            Productos.codProductoEditarVenta = 0;
        }

        private void mostrarOcultarLblArrow()
        {
            if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
            {
                if (listaProductoToCombo.Count() > 0 || ProductosDeServicios.Count() > 0)
                {
                    lblArrow.Visible = true;
                }
                else if (listaProductoToCombo.Count().Equals(0) && ProductosDeServicios.Count().Equals(0))
                {
                    lblArrow.Visible = false;
                }
            }
            else if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
            {
                using (DataTable dtProductos = cn.CargarDatos(cs.encontrarProductoComboServicio(Convert.ToInt32(idEditarProducto))))
                {
                    if (!dtProductos.Rows.Count.Equals(0))
                    {
                        foreach (DataRow drProd in dtProductos.Rows)
                        {
                            if (!drProd["Tipo"].Equals("P"))
                            {
                                using (DataTable dtCombdServ = cn.CargarDatos(cs.ObtenerServPaqRelacionados(drProd["ID"].ToString())))
                                {
                                    if (!dtCombdServ.Rows.Count.Equals(0))
                                    {
                                        lblArrow.Visible = true;
                                    }
                                    else if (dtCombdServ.Rows.Count.Equals(0))
                                    {
                                        //lblArrow.Visible = false;
                                        if (listaProductoToCombo.Count() > 0 || ProductosDeServicios.Count() > 0)
                                        {
                                            lblArrow.Visible = true;
                                        }
                                        else if (listaProductoToCombo.Count().Equals(0) && ProductosDeServicios.Count().Equals(0))
                                        {
                                            lblArrow.Visible = false;
                                        }
                                    }
                                }
                            }
                            else if (drProd["Tipo"].Equals("P"))
                            {
                                using (DataTable dtProd = cn.CargarDatos(cs.obtenerProdRelacionados(drProd["ID"].ToString())))
                                {
                                    if (!dtProd.Rows.Count.Equals(0))
                                    {
                                        lblArrow.Visible = true;
                                    }
                                    else if (dtProd.Rows.Count.Equals(0))
                                    {
                                        //lblArrow.Visible = false;
                                        if (listaProductoToCombo.Count() > 0 || ProductosDeServicios.Count() > 0)
                                        {
                                            lblArrow.Visible = true;
                                        }
                                        else if (listaProductoToCombo.Count().Equals(0) && ProductosDeServicios.Count().Equals(0))
                                        {
                                            lblArrow.Visible = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AgregarEditarProducto_Load(object sender, EventArgs e)
        {
            baseProducto = "0";
            ivaProducto = "0";
            idReporte = "0";

            var servidor = Properties.Settings.Default.Hosting;

            errorProvAgregarEditarProducto.ContainerControl = this;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                saveDirectoryImg = $@"\\{servidor}\PUDVE\Productos\";
            }
            else
            {
                saveDirectoryImg = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Productos\";
            }

            detalleProductoBasico.Clear();
            detalleProductoGeneral.Clear();
            seleccionListaStock = 0;
            string cadAux = string.Empty;
            fLPType.Visible = false;
            fLPContenidoProducto.Visible = false;
            TituloForm = Titulo;

            PH = fLPContenidoProducto.Height;
            Hided = false;
            Hided1 = false;
            flowLayoutPanel2.Controls.Clear();
            DatosSourceFinal = DatosSource;

            if (DatosSourceFinal.Equals(2))
            {
                btnAgregarDescuento.Enabled = true;
            }
            if (DatosSourceFinal.Equals(1) ||
                DatosSourceFinal.Equals(3) ||
                DatosSourceFinal.Equals(5))
            {
                btnAgregarDescuento.Enabled = false;
            }

            PCategoria.Visible = false;
            fLPDetallesProducto.Visible = true;

            flowLayoutPanel3.VerticalScroll.Visible = true;

            actualizarDetallesProducto();

            mostrarOcultarfLPDetallesProducto();

            if (DatosSourceFinal == 3)      // si el llamado de la ventana proviene del Archivo XML
            {
                cbTipo.SelectedIndex = 0;
                //PCantidadPaqServ.Visible = false;
                fLPType.Visible = true;
                cadAux = TituloForm.Substring(8);   // extraemos que tipo es (Producto, Paquete, Servicio)
                cargarDatos();
                txtPrecioCompra.Enabled = true;

                // Miri. 
                // Se agrega para que inicialmente no lo muestre puesto que por default es un producto en el select. 
                //btnAdd.Visible = false; 
            }
            // si el llamado de la ventana proviene del DataGridView (Ventana Productos)
            // si el llamado de la ventana proviene del DataGridView (Copiar Producto)
            else if (DatosSourceFinal == 2 || DatosSourceFinal == 4)
            {
                txtStockProducto.Enabled = false;
                button1.Visible = true;
                cadAux = TituloForm.Substring(7);   // extraemos que tipo es (Producto, Paquete, Servicio)
                txtPrecioCompra.Enabled = false;

                var detallesProductoTmp = cn.BuscarProducto(Convert.ToInt32(idEditarProducto), FormPrincipal.userID);

                if (detallesProductoTmp.Length > 0)
                {
                    stockNecesario = detallesProductoTmp[8];
                    stockMinimo = detallesProductoTmp[10];
                    typeOfProduct = detallesProductoTmp[5];
                    txtPrecioCompra.Text = detallesProductoTmp[11];
                    logoTipo = detallesProductoTmp[9];      // Obtenemos el nuevo Path
                    txtStockMaximo.Text = detallesProductoTmp[8];
                    txtStockMinimo.Text = detallesProductoTmp[10];
                    txtPrecioMayoreo.Text = detallesProductoTmp[12];

                    if (pictureBoxProducto.Image != null)
                    {
                        pictureBoxProducto.Image.Dispose(); // Liberamos el pictureBox para poder borrar su imagen
                        if (!logoTipo.Equals(""))
                        {
                            // leemos el archivo de imagen y lo ponemos el pictureBox
                            using (File = new FileStream(saveDirectoryImg + logoTipo, FileMode.Open, FileAccess.Read))
                            {
                                pictureBoxProducto.Image = Image.FromStream(File);      // carrgamos la imagen en el PictureBox
                                btnImagenes.Text = "Borrar imagen";
                            }
                        }
                        else if (logoTipo.Equals(""))
                        {
                            btnImagenes.Text = "Seleccionar imagen";
                        }
                    }
                    else if (pictureBoxProducto.Image == null)
                    {
                        if (!logoTipo.Equals(""))
                        {
                            if (System.IO.File.Exists(saveDirectoryImg + logoTipo))
                            {
                                // leemos el archivo de imagen y lo ponemos el pictureBox
                                using (File = new FileStream(saveDirectoryImg + logoTipo, FileMode.Open, FileAccess.Read))
                                {
                                    pictureBoxProducto.Image = Image.FromStream(File);      // carrgamos la imagen en el PictureBox
                                    btnImagenes.Text = "Borrar imagen";
                                }
                            }
                        }
                        else if (logoTipo.Equals(""))
                        {
                            btnImagenes.Text = "Seleccionar imagen";
                        }
                    }
                }
            }
            else if (DatosSourceFinal == 1)      // si el llamado de la ventana proviene del Boton Productos (Ventana Productos)
            {
                txtStockProducto.Enabled = true;
                cadAux = TituloForm.Substring(8);   // extraemos que tipo es (Producto, Paquete, Servicio)
                //PCantidadPaqServ.Visible = true;
                button1.Visible = true;
                txtPrecioCompra.Enabled = true;
                detalleProductoBasico.Clear();
                detalleProductoGeneral.Clear();
                VerificarDatosDeDetalleProducto();
            }
            else if (DatosSourceFinal == 5)
            {
                txtStockProducto.Enabled = true;
                cadAux = TituloForm.Substring(8);   // extraemos que tipo es (Producto, Paquete, Servicio)
                //PCantidadPaqServ.Visible = true;
                button1.Visible = true;
                txtPrecioCompra.Enabled = true;
                detalleProductoBasico.Clear();
                detalleProductoGeneral.Clear();
                VerificarDatosDeDetalleProducto();
            }

            if (cadAux == "Producto")           // si es un Producto
            {
                agregarProducto();
                if (!ProdNombre.Equals(""))
                {
                    cargarDatos();
                    ocultarPanel();
                    //cargarCBProductos();
                    //PStock.Visible = true;
                    txtCantPaqServ.Visible = false;
                    lblCantPaqServ.Text = "Relacionar con \nPaquete/Servicio";
                    button1.Text = "Relacionar con \ncombo/servicio";
                    //button1.Text = "Combo/Servicio";
                }
                else if (ProdNombre.Equals(""))
                {
                    LimpiarCampos();
                    if (DatosSourceFinal == 5)
                    {
                        cargarDatos();
                    }
                    else
                    {
                        cargarDatosNvoProd();
                    }
                    cbTipo.Text = "Producto";
                    //btnAdd.Visible = false;
                    ocultarPanel();
                    //PStock.Visible = true;
                    txtCantPaqServ.Visible = false;
                    lblCantPaqServ.Text = "Relacionar con \nCombo/Servicio";
                    button1.Text = "Relacionar con \ncombo/servicio";
                    //button1.Text = "Combo/Servicio";
                }
                this.Text = Titulo.ToUpper();
                //this.Text = cadAux + "s";             // Ponemos el titulo del form en plural "Productos"
                typeOfProduct = "P";
                lblTipoProdPaq.Text = "Nombre del Producto";
                txtCategoriaProducto.Text = cadAux + "s";
                if (DatosSourceFinal == 1 || DatosSourceFinal == 3 || DatosSourceFinal == 5)
                {
                    tituloSeccion.Text = "Agregar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                    //tituloSeccion.Text = tituloSeccion.Text.ToUpper();
                }
                else if (DatosSourceFinal == 2)
                {
                    tituloSeccion.Text = "Editar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                    //tituloSeccion.Text = tituloSeccion.Text.ToUpper();
                }
                else if (DatosSourceFinal == 4)
                {
                    tituloSeccion.Text = "Copiar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                    //tituloSeccion.Text = tituloSeccion.Text.ToUpper();
                }
            }
            else if (cadAux == "Combo")       // si es un Paquete
            {
                toolTip1.SetToolTip(lblCantCombServ, "En este apartado podrá indicarle al sistema la cantidad\nde productos que se descontaran al venderse un Combo");
                agregarCombo();
                if (!ProdNombre.Equals(""))
                {
                    cargarDatos();
                    ocultarPanel();
                    cargarCBProductos(idEditarProducto);
                    //PStock.Visible = false;
                    //txtCantPaqServ.Visible = true;
                    lblCantPaqServ.Text = "Cantidad por combo";
                    button1.Text = "Productos";
                }
                else if (ProdNombre.Equals(""))
                {
                    LimpiarCampos();
                    cargarDatosNvoProd();
                    cbTipo.Text = "Producto";
                    //btnAdd.Visible = false;
                    ocultarPanel();
                    //PStock.Visible = false;
                    txtCantPaqServ.Visible = true;
                    lblCantPaqServ.Text = "Cantidad por combo";
                    button1.Text = "Productos";
                }
                //this.Text = cadAux + "s";            // Ponemos el titulo del form en plural "Paquetes"
                typeOfProduct = "PQ";
                lblTipoProdPaq.Text = "Nombre del Combo";
                txtCategoriaProducto.Text = cadAux + "s";
                if (DatosSourceFinal == 1 || DatosSourceFinal == 3 || DatosSourceFinal == 5)
                {
                    tituloSeccion.Text = "Agregar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                    //tituloSeccion.Text = tituloSeccion.Text.ToUpper();
                }
                else if (DatosSourceFinal == 2)
                {
                    tituloSeccion.Text = "Editar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                    //tituloSeccion.Text = tituloSeccion.Text.ToUpper();
                }
                else if (DatosSourceFinal == 4)
                {
                    tituloSeccion.Text = "Copiar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                    //tituloSeccion.Text = tituloSeccion.Text.ToUpper();
                }

                this.Text = tituloSeccion.Text.ToUpper();   // Título del form
            }
            else if (cadAux == "Servicio")      // si es un Servicio
            {
                toolTip1.SetToolTip(lblCantCombServ, "En este apartado podrá indicarle al sistema la cantidad\nde productos que se descontaran al venderse un Servicio");
                agregarServicio();
                if (!ProdNombre.Equals(""))
                {
                    cargarDatos();
                    ocultarPanel();
                    cargarCBProductos(idEditarProducto);
                    //PStock.Visible = false;
                    txtCantPaqServ.Visible = true;
                    lblCantPaqServ.Text = "Cantidad por servicio";
                    button1.Text = "Productos";
                }
                else if (ProdNombre.Equals(""))
                {
                    LimpiarCampos();
                    cargarDatosNvoProd();
                    cbTipo.Text = "Producto";
                    //btnAdd.Visible = false;
                    ocultarPanel();
                    //PStock.Visible = false;
                    txtCantPaqServ.Visible = true;
                    lblCantPaqServ.Text = "Cantidad por servicio";
                    button1.Text = "Productos";
                }
                //this.Text = cadAux + "s";            // Ponemos el titulo del form en plural "Servicios"
                typeOfProduct = "S";
                lblTipoProdPaq.Text = "Nombre del Servicio";
                txtCategoriaProducto.Text = cadAux + "s";
                if (DatosSourceFinal == 1 || DatosSourceFinal == 3 || DatosSourceFinal == 5)
                {
                    tituloSeccion.Text = "Agregar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                    //tituloSeccion.Text = tituloSeccion.Text.ToUpper();
                }
                else if (DatosSourceFinal == 2)
                {
                    tituloSeccion.Text = "Editar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                    //tituloSeccion.Text = tituloSeccion.Text.ToUpper();
                }
                else if (DatosSourceFinal == 4)
                {
                    tituloSeccion.Text = "Copiar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                    //tituloSeccion.Text = tituloSeccion.Text.ToUpper();
                }

                this.Text = tituloSeccion.Text.ToUpper();   // Título del form 
            }

            var config = mb.ComprobarConfiguracion();

            if (config.Count > 0)
            {
                porcentajeGanancia = float.Parse(config[8].ToString());
                //panelMayoreo.Visible = Convert.ToBoolean(config[9]);
            }
            tituloSeccion.Text = tituloSeccion.Text.ToUpper();

            if (!txtPrecioProducto.Text.Equals(""))
            {
                var valor = double.Parse(txtPrecioProducto.Text);
                if (valor > 0)
                {
                    btnAgregarDescuento.Enabled = true;
                }
            }

            validarClave();

            mostrarOcultarLblArrow();

            llenarListaDatosDinamicos();

            //if (DatosSourceFinal.Equals(3))
            //{
            //    if (!nameProveedorXML.Equals(string.Empty))
            //    {
            //        // ToDo recorrer el flowLayoutPanel y poder ver si esta Proveedor
            //        foreach (Control panelDinamico in flowLayoutPanel3.Controls)
            //        {
            //            if (panelDinamico is Panel)
            //            {
            //                Panel pnlDin = (Panel)panelDinamico;

            //                var namePanel = pnlDin.Name;

            //                if (namePanel.Equals("panelContenedorchkProveedor"))
            //                {
            //                    foreach (Control pnlContenido in pnlDin.Controls)
            //                    {
            //                        if (pnlContenido is Panel)
            //                        {
            //                            if (pnlContenido.Name.Equals("panelContenidochkProveedor"))
            //                            {
            //                                foreach (Control cbProveedor in pnlContenido.Controls)
            //                                {
            //                                    if (cbProveedor is ComboBox)
            //                                    {
            //                                        cbProveedor.Text = nameProveedorXML;
            //                                        MessageBox.Show("nameProveedorXML: " + nameProveedorXML + "\nComboBox Proveedor: " + cbProveedor.Text, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                                        break;
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private void llenarListaDatosDinamicos()
        {
            if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
            {
                detalleProductoBasico.Clear();
                detalleProductoGeneral.Clear();
                foreach (Control ctrPanel in flowLayoutPanel3.Controls)
                {
                    if (ctrPanel is Panel)
                    {
                        if (ctrPanel.Name.Equals("panelContenedorchkProveedor"))
                        {
                            foreach (Control subCtrPanel in ctrPanel.Controls)
                            {
                                if (subCtrPanel is Panel)
                                {
                                    if (subCtrPanel.Name.Equals("panelContenidochkProveedor"))
                                    {
                                        foreach (Control itemSubCtrPanel in subCtrPanel.Controls)
                                        {
                                            if (itemSubCtrPanel is Label)
                                            {
                                                if (!itemSubCtrPanel.Text.Equals(string.Empty))
                                                {
                                                    detalleProductoBasico.Clear();
                                                    detalleProductoBasico.Add(string.Empty);
                                                    detalleProductoBasico.Add(Convert.ToString(FormPrincipal.userID));
                                                    detalleProductoBasico.Add(itemSubCtrPanel.Text);
                                                    using (DataTable dtProveedores = cn.CargarDatos(cs.obtenerIDProveedor(itemSubCtrPanel.Text.ToString())))
                                                    {
                                                        if (!dtProveedores.Rows.Count.Equals(0))
                                                        {
                                                            foreach (DataRow drProveedor in dtProveedores.Rows)
                                                            {
                                                                detalleProductoBasico.Add(drProveedor["ID"].ToString());
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (!ctrPanel.Name.Equals("panelContenedorchkProveedor"))
                        {
                            foreach (Control subCtrPanel in ctrPanel.Controls)
                            {
                                if (subCtrPanel is Panel)
                                {
                                    foreach (Control itemSubCtrPanel in subCtrPanel.Controls)
                                    {
                                        if (!subCtrPanel.Name.Equals("panelContenidochkProveedor"))
                                        {
                                            if (itemSubCtrPanel is Label)
                                            {
                                                if (!itemSubCtrPanel.Text.Equals(string.Empty))
                                                {
                                                    using (DataTable dtDetalleGeneral = cn.CargarDatos(cs.obtenerIDDetalleGeneral(itemSubCtrPanel.Text.ToString().Replace(" ", "_"))))
                                                    {
                                                        if (!dtDetalleGeneral.Rows.Count.Equals(0))
                                                        {
                                                            foreach (DataRow drDetalleGeneral in dtDetalleGeneral.Rows)
                                                            {
                                                                detalleProductoGeneral.Add($"|{drDetalleGeneral[1]}|{drDetalleGeneral[0]}|1|panelContenido{drDetalleGeneral[2]}|{drDetalleGeneral[3]}");
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void validarClave()
        {
            var mostrarClave = FormPrincipal.clave;

            if (mostrarClave == 0)
            {
                txtClaveProducto.Visible = false;
                lbClaveInterna.Visible = false;
                label5.Visible = false; //Esta es la etiquta que dice que es clave interna
            }
            else if (mostrarClave == 1)
            {
                txtClaveProducto.Visible = true;
                lbClaveInterna.Visible = true;
                label5.Visible = true; //Esta es la etiquta que dice que es clave interna
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void agregarProducto()
        {
            // pasar controles a form principal
            cambiarControlesAgregarEditarProducto();

            // borrar todas las filas y columnas del TableLayoutPanel
            clearSetUpTableLayoutPanel("Producto");

            // creamos 6 columnas en el TableLayoutPanel
            for (int i = 0; i <= 7; i++)
            {
                tLPProducto.ColumnCount++;
                ///if (i.Equals(2) || i.Equals(5) || i.Equals(8))
                if (i.Equals(0) || i.Equals(2) || i.Equals(4))
                {
                    tLPProducto.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135F));
                }
                if (i.Equals(1) || i.Equals(3) || i.Equals(5) || i.Equals(6))
                {
                    tLPProducto.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
                }
                if (i.Equals(7))
                {
                    tLPProducto.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210F));
                }
            }

            // creamos 7 filas en el TableLayoutPanel
            for (int i = 0; i <= 8; i++)
            {
                tLPProducto.RowCount++;
                tLPProducto.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            }

            bool conSinClaveInterna = false;

            using (DataTable dtChecarConSinClaveInternaUsuario = cn.CargarDatos(cs.verificarConSinClaveInterna(FormPrincipal.userID)))
            {
                if (!dtChecarConSinClaveInternaUsuario.Rows.Count.Equals(0))
                {
                    foreach (DataRow drChecarConSin in dtChecarConSinClaveInternaUsuario.Rows)
                    {
                        if (drChecarConSin["SinClaveInterna"].Equals(0))
                        {
                            conSinClaveInterna = false;
                        }
                        else if (drChecarConSin["SinClaveInterna"].Equals(1))
                        {
                            conSinClaveInterna = true;
                        }
                    }
                }
            }

            if (conSinClaveInterna.Equals(false))
            {
                // Primera Fila del TableLayoutPanel
                // Label titulos
                #region Begin Row 1

                // label para Stock Minimo
                label1.Visible = true;
                label1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

                // label para Stock Maximo
                label12.Visible = true;
                label12.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                //label12.TextAlign = ContentAlignment.MiddleCenter;

                // label de cantidad Stock Compra
                label6.Visible = true;
                label6.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                //label6.TextAlign = ContentAlignment.MiddleCenter;


                tLPProducto.Controls.Add(label1, 0, 0);               // Stock Minimo Label
                tLPProducto.Controls.Add(label12, 2, 0);              // Stock Maximo Label
                tLPProducto.Controls.Add(label6, 4, 0);               // Stock Label

                #endregion End Row 1

                // Segunda Fila del TableLayoutPanel
                // TextBox y label de información
                #region Begin Row 2

                // TextBox de Stock Minimo
                txtStockMinimo.Visible = true;
                txtStockMinimo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtStockMinimo.TabIndex = 1;
                txtStockMinimo.TabStop = true;
                // label de esclamation txtStockMinimo
                lblStockMinimo.Visible = true;
                lblStockMinimo.Anchor = AnchorStyles.Left; // | AnchorStyles.Right

                // TextBox de Stock Maxio
                txtStockMaximo.Visible = true;
                txtStockMaximo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtStockMaximo.TabIndex = 2;
                txtStockMaximo.TabStop = true;
                //Label de Excalmation Stock
                lbStockMaximo.Visible = true;
                lbStockMaximo.Anchor = AnchorStyles.Left;// | AnchorStyles.Right

                // TextBox de Stock
                txtStockProducto.Visible = true;
                txtStockProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtStockProducto.TabIndex = 3;
                txtStockProducto.TabStop = true;
                //Label de Excalmation Stock
                lbStock.Visible = true;
                lbStock.Anchor = AnchorStyles.Left; // | AnchorStyles.Right


                tLPProducto.Controls.Add(txtStockMinimo, 0, 1);       // Stock Minimo TextBox
                tLPProducto.Controls.Add(lblStockMinimo, 1, 1);       // Label de exclamation Stock Minimo
                tLPProducto.Controls.Add(txtStockMaximo, 2, 1);       // Stock Maximo TextBox
                tLPProducto.Controls.Add(lbStockMaximo, 3, 1);        // Label de Excalmation Stock Maximo
                tLPProducto.Controls.Add(txtStockProducto, 4, 1);     // Stock TextBox
                tLPProducto.Controls.Add(lbStock, 5, 1);              // Label de Excalmation Stock

                #endregion End Row 1

                // Tercera Fila del TableLayoutPanel
                // Label titulos
                #region Begin Row 3

                // label para Precio Compra
                label7.Visible = true;
                label7.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;


                // label para Precio Venta
                label4.Visible = true;
                label4.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                //label4.TextAlign = ContentAlignment.MiddleCenter;

                // label para Código de Barras
                label2.Visible = true;
                label2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right;

                lblCodigoBarras.Visible = true;
                lblCodigoBarras.Anchor = AnchorStyles.Left;

                //lblCodBarExtra.Visible = true;
                //lblCodBarExtra.Anchor = AnchorStyles.Left | AnchorStyles.Right;

                tLPProducto.Controls.Add(label7, 0, 2);               // Precio Compra Label
                tLPProducto.Controls.Add(label4, 2, 2);               // Precio Venta Label
                tLPProducto.Controls.Add(label2, 4, 2);               // Código de Barras Label
                tLPProducto.Controls.Add(lblCodigoBarras, 5, 2);
                //tLPProducto.Controls.Add(lblCodBarExtra, 6, 2);

                #endregion End Row 3

                // Cuarta Fila del TableLayoutPanel
                // TextBox y label de información
                #region Begin Row 4

                // TextBox para Precio Compra
                txtPrecioCompra.Visible = true;
                txtPrecioCompra.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtPrecioCompra.TabIndex = 4;
                txtPrecioCompra.TabStop = true;
                // Label de exclamation Precio de Compra
                lbPrecioCompra.Visible = true;
                lbPrecioCompra.Anchor = AnchorStyles.Left; // | AnchorStyles.Right

                // TextBox para Precio Venta
                txtPrecioProducto.Visible = true;
                txtPrecioProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtPrecioProducto.TabIndex = 5;
                txtPrecioProducto.TabStop = true;

                // Label de exclamation Precio de Venta
                lbPrecioVenta.Visible = true;
                lbPrecioVenta.Anchor = AnchorStyles.Left; // | AnchorStyles.Right

                // TextBox Código de Barras
                txtCodigoBarras.Visible = true;
                txtCodigoBarras.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtCodigoBarras.TabIndex = 7;
                txtCodigoBarras.TabStop = true;

                // Button Generar Código de Barras
                btnGenerarCB.Visible = true;
                btnGenerarCB.Anchor = AnchorStyles.Left; // | AnchorStyles.Right
                btnGenerarCB.TabIndex = 8;
                btnGenerarCB.TabStop = true;

                // Button Agregar Codigo Barras extra
                btnAddCodBar.Visible = true;
                btnAddCodBar.Anchor = AnchorStyles.Left; // | AnchorStyles.Right
                btnAddCodBar.TabIndex = 9;
                btnAddCodBar.TabStop = true;

                tLPProducto.Controls.Add(txtPrecioCompra, 0, 3);      // Precio Compra TextBox
                tLPProducto.Controls.Add(lbPrecioCompra, 1, 3);       // Label de exclamation Precio de Compra
                tLPProducto.Controls.Add(txtPrecioProducto, 2, 3);    // Precio Venta TextBox
                tLPProducto.Controls.Add(lbPrecioVenta, 3, 3);        // Label de exclamation Precio de Venta
                tLPProducto.Controls.Add(txtCodigoBarras, 4, 3);      // Código de Barras TextBox
                tLPProducto.Controls.Add(btnGenerarCB, 5, 3);         // Generar Button
                tLPProducto.Controls.Add(btnAddCodBar, 6, 3);         // Botón de generar códigos de barra extra

                #endregion End Row 4

                // Quinta Fila del TableLayoutPanel
                // Label titulos
                #region Begin Row 5

                // label para Clave Interna
                //label5.Visible = true;
                //label5.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                //label5.TextAlign = ContentAlignment.MiddleCenter;

                // label para Código de Barras
                //label2.Visible = true;
                //label2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

                // Panel para Generar Código de Barra Extra
                panelContenedor.Visible = true;
                panelContenedor.Anchor = AnchorStyles.Left;
                panelContenedor.TabIndex = 10;
                panelContenedor.TabStop = true;

                tLPProducto.Controls.Add(label5, 0, 4);               // Clave Interna Label
                //tLPProducto.Controls.Add(label2, 2, 4);               // Código de Barras Label

                tLPProducto.Controls.Add(panelContenedor, 4, 4);
                tLPProducto.SetColumnSpan(panelContenedor, 3);
                tLPProducto.SetRowSpan(panelContenedor, 2);
                #endregion End Row 5
            }
            if (conSinClaveInterna.Equals(true))
            {

            }

            // Sexta Fila del TableLayoutPanel
            // TextBox
            #region Begin Row 6

            // TextBox para Clave Interna
            //txtClaveProducto.Visible = true;
            //txtClaveProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            //txtClaveProducto.TabIndex = 6;
            //txtClaveProducto.TabStop = true;
            // Label de exclamation Clave Interna
            //lbClaveInterna.Visible = true;
            //lbClaveInterna.Anchor = AnchorStyles.Left; // | AnchorStyles.Right

            //// TextBox Código de Barras
            //txtCodigoBarras.Visible = true;
            //txtCodigoBarras.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            //txtCodigoBarras.TabIndex = 7;
            //txtCodigoBarras.TabStop = true;

            //// Button Generar Código de Barras
            //btnGenerarCB.Visible = true;
            //btnGenerarCB.Anchor = AnchorStyles.Left; // | AnchorStyles.Right
            //btnGenerarCB.TabIndex = 8;
            //btnGenerarCB.TabStop = true;


            //tLPProducto.Controls.Add(txtClaveProducto, 0, 5);     // Clave Interna TextBox
            //tLPProducto.Controls.Add(lbClaveInterna, 1, 5);       // Label de exclamation Clave Interna
            //tLPProducto.Controls.Add(txtCodigoBarras, 2, 5);      // Código de Barras TextBox
            //tLPProducto.Controls.Add(btnGenerarCB, 3, 5);         // Generar Button

            #endregion End Row 6

            #region Begin Row 7
            //// Panel para Generar Código de Barra Extra
            //panelContenedor.Visible = true;
            //panelContenedor.Anchor = AnchorStyles.Left;
            //panelContenedor.TabIndex = 10;
            //panelContenedor.TabStop = true;

            //tLPProducto.Controls.Add(panelContenedor, 2, 6);
            //tLPProducto.SetColumnSpan(panelContenedor, 3);
            //tLPProducto.SetRowSpan(panelContenedor, 3);
            #endregion End Row 7

            // Celda de imagen del TableLayoutPanel
            // Imagen
            #region Begin celda imagen

            // Panel Para Imagen del Producto
            PImagen.Visible = true;
            PImagen.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            PImagen.TabIndex = 9;
            PImagen.TabStop = true;


            //.SetColumnSpan(btnGenerarCB, 0);           // Columnas hacia derecha
            tLPProducto.Controls.Add(PImagen, 7, 0);     // Cuadro para agregar Imagen Panel
            tLPProducto.SetRowSpan(PImagen, 6);          // Filas hacia abajo (Cantidad de filas que abarcará)
                                                         //tLPProducto.SetColumnSpan(PImagen, 2);     // Columnas hacia derecha

            #endregion End celda imagen

            // Button para Relacionar con Combo/Servicio
            button1.Visible = true;
            button1.TabIndex = 11;

            /*
            // Cuarta Fila del TableLayoutPanel
            #region Begin Row 4

            // Panel para Generar Código de Barra Extra
            panelContenedor.Visible = true;
            panelContenedor.Anchor = AnchorStyles.Left;
            panelContenedor.TabIndex = 10;
            panelContenedor.TabStop = true;

            tLPProducto.Controls.Add(panelContenedor, 1, 3);
            tLPProducto.SetColumnSpan(panelContenedor, 3);
            tLPProducto.SetRowSpan(panelContenedor, 2);

            #endregion End Row 4

            // Sexta Fila del TableLayoutPanel
            #region Begin Row 6

            // Label para Relacionar con Combo/Servicio
            lblCantPaqServ.Visible = true;
            lblCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            // Button para Relacionar con Combo/Servicio
            button1.Visible = true;
            button1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            button1.TabIndex = 11;
            button1.TabStop = true;

            tLPProducto.Controls.Add(lblCantPaqServ, 6, 6);       // Relacionar Combo/Servicio Label9
            tLPProducto.Controls.Add(button1, 7, 6);              // Combo / Servicio Button2

            #endregion End Row 6
            */
        }

        private void cambiarControlesAgregarEditarProducto()
        {
            label1.Visible = false;
            this.Controls.Add(label1);
            txtStockMinimo.Visible = false;
            this.Controls.Add(txtStockMinimo);
            label12.Visible = false;
            this.Controls.Add(label12);
            txtStockMaximo.Visible = false;
            this.Controls.Add(txtStockMaximo);
            label6.Visible = false;
            this.Controls.Add(label6);
            txtStockProducto.Visible = false;
            this.Controls.Add(txtStockProducto);
            label7.Visible = false;
            this.Controls.Add(label7);
            txtPrecioCompra.Visible = false;
            this.Controls.Add(txtPrecioCompra);
            label4.Visible = false;
            this.Controls.Add(label4);
            txtPrecioProducto.Visible = false;
            this.Controls.Add(txtPrecioProducto);
            label5.Visible = false;
            this.Controls.Add(label5);
            txtClaveProducto.Visible = false;
            this.Controls.Add(txtClaveProducto);
            label2.Visible = false;
            this.Controls.Add(label2);
            txtCodigoBarras.Visible = false;
            this.Controls.Add(txtCodigoBarras);
            btnGenerarCB.Visible = false;
            this.Controls.Add(btnGenerarCB);
            panelContenedor.Visible = false;
            this.Controls.Add(panelContenedor);
            PImagen.Visible = false;
            this.Controls.Add(PImagen);
            lblCantPaqServ.Visible = false;
            this.Controls.Add(lblCantPaqServ);
            button1.Visible = false;
            //this.Controls.Add(button1);
            txtCantPaqServ.Visible = false;
            this.Controls.Add(txtCantPaqServ);
            lblStockMinimo.Visible = false;
            this.Controls.Add(lblStockMinimo);
            lbPrecioCompra.Visible = false;
            this.Controls.Add(lbPrecioCompra);
            lblCantCombServ.Visible = false;
            this.Controls.Add(lblCantCombServ);
            lblCodigoBarras.Visible = false;
            this.Controls.Add(lblCodigoBarras);
            btnAddCodBar.Visible = false;
            this.Controls.Add(btnAddCodBar);
            lblCodBarExtra.Visible = false;
            this.Controls.Add(lblCodBarExtra);
        }

        private void agregarCombo()
        {
            // pasar controles a form principal
            cambiarControlesAgregarEditarProducto();

            // borrar todas las filas y columnas del TableLayoutPanel
            clearSetUpTableLayoutPanel("Combo");

            bool conSinClaveInterna = false;

            using (DataTable dtChecarConSinClaveInternaUsuario = cn.CargarDatos(cs.verificarConSinClaveInterna(FormPrincipal.userID)))
            {
                if (!dtChecarConSinClaveInternaUsuario.Rows.Count.Equals(0))
                {
                    foreach (DataRow drChecarConSin in dtChecarConSinClaveInternaUsuario.Rows)
                    {
                        if (drChecarConSin["SinClaveInterna"].Equals(0))
                        {
                            conSinClaveInterna = false;
                        }
                        else if (drChecarConSin["SinClaveInterna"].Equals(1))
                        {
                            conSinClaveInterna = true;
                        }
                    }
                }
            }

            if (conSinClaveInterna.Equals(false))
            {
                // creamos 5 columnas en el TableLayoutPanel
                for (int i = 0; i < 6; i++)
                {
                    tLPCombo.ColumnCount++;
                    //if (i.Equals(2) || i.Equals(5))
                    //{
                    //    tLPCombo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 22F));
                    //}
                    //else
                    //{
                    //    tLPCombo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F));
                    //}
                    if (i.Equals(0) || i.Equals(2) || i.Equals(5))
                    {
                        tLPCombo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
                    }
                    if (i.Equals(1) || i.Equals(3) || i.Equals(4))
                    {
                        tLPCombo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
                    }
                }
            }
            else if (conSinClaveInterna.Equals(true))
            {
                // creamos 8 columnas en el TableLayoutPanel
                for (int i = 0; i < 8; i++)
                {
                    tLPCombo.ColumnCount++;
                    if (i.Equals(0) || i.Equals(2) || i.Equals(4) || i.Equals(6))
                    {
                        tLPCombo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135F));
                    }
                    else if (i.Equals(1) || i.Equals(3) || i.Equals(5) || i.Equals(7))
                    {
                        tLPCombo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
                    }
                }
            }

            // creamos 6 filas en el TableLayoutPanel
            for (int i = 0; i < 6; i++)
            {
                tLPCombo.RowCount++;
                tLPCombo.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            }

            if (conSinClaveInterna.Equals(false))
            {
                if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(2))
                {
                    // Primera Fila del TableLayoutPanel
                    // Label títulos
                    #region Begin Row 1

                    // Label para Precio Compra
                    label7.Visible = true;
                    label7.Anchor = AnchorStyles.Left | AnchorStyles.Bottom; // | AnchorStyles.Right

                    // Label para Precio Venta
                    label4.Visible = true;
                    label4.Anchor = AnchorStyles.Left | AnchorStyles.Bottom; // | AnchorStyles.Right
                                                                             //label4.TextAlign = ContentAlignment.MiddleCenter;

                    tLPCombo.Controls.Add(label7, 0, 0);               // Precio Compra Label
                    tLPCombo.Controls.Add(label4, 2, 0);               // Precio Venta Label

                    #endregion End Row 1

                    // Segunda Fila del TableLayoutPanel
                    // TextBox y label informativo
                    #region Begin Row 2

                    // TextBox para Precio Compra
                    txtPrecioCompra.Visible = true;
                    txtPrecioCompra.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtPrecioCompra.TabIndex = 1;
                    txtPrecioCompra.TabStop = true;
                    // Label de exclamation Precio Compra
                    lbPrecioCompra.Visible = true;
                    lbPrecioCompra.Anchor = AnchorStyles.Left; // | AnchorStyles.Right

                    // TextBox para Precio Venta
                    txtPrecioProducto.Visible = true;
                    txtPrecioProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtPrecioProducto.TabIndex = 2;
                    txtPrecioProducto.TabStop = true;
                    // Label de exclamation Precio Venta
                    lbPrecioVenta.Visible = true;
                    lbPrecioVenta.Anchor = AnchorStyles.Left; // | AnchorStyles.Righ

                    tLPCombo.Controls.Add(txtPrecioCompra, 0, 1);      // Precio Compra TextBox
                    tLPCombo.Controls.Add(lbPrecioCompra, 1, 1);       // Label de exclamation Precio Compra
                    tLPCombo.Controls.Add(txtPrecioProducto, 2, 1);    // Precio Venta TextBox
                    tLPCombo.Controls.Add(lbPrecioVenta, 3, 1);        // Label de exclamation Precio Venta

                    #endregion End Row 2

                    // Tercera Fila del TableLayoutPanel
                    // Label títulos
                    #region Begin Row 3

                    // Label para Clave Interna
                    label5.Visible = true;
                    label5.Anchor = AnchorStyles.Left | AnchorStyles.Bottom; // | AnchorStyles.Right

                    // Label para Código de Barras
                    label2.Visible = true;
                    label2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top;
                    //label2.TextAlign = ContentAlignment.MiddleCenter;

                    // Label informativo para Código de Barras o Clave Interna
                    lblCodigoBarras.Visible = true;
                    lblCodigoBarras.Anchor = AnchorStyles.Left;

                    //lblCodBarExtra.Visible = true;
                    //lblCodBarExtra.Anchor = AnchorStyles.Left | AnchorStyles.Right;

                    // Label para Cantidad por Combo
                    lblCantPaqServ.Visible = true;
                    lblCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    //lblCantPaqServ.TextAlign = ContentAlignment.MiddleLeft;

                    tLPCombo.Controls.Add(label5, 0, 2);               // Clave Interna Label
                    tLPCombo.Controls.Add(label2, 2, 2);               // Código de Barras Label
                    tLPCombo.Controls.Add(lblCodigoBarras, 3, 2);      // Simbolo Informativo
                                                                       //tLPCombo.Controls.Add(lblCodBarExtra, 4, 2);       // Simbolo Informativo gregar código Barras extra
                    tLPCombo.Controls.Add(lblCantPaqServ, 0, 2);       // Relacionar con Combo/Servicio Label

                    #endregion End Row 3

                    // Cuarta Fila del TableLayoutPanel
                    // TextBox y label
                    #region Begin Row 4

                    // TextBox para Clave Interna
                    txtClaveProducto.Visible = true;
                    txtClaveProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtClaveProducto.TabIndex = 3;
                    txtClaveProducto.TabStop = true;
                    // Label de exclamation Clave Interna
                    lbClaveInterna.Visible = true;
                    lbClaveInterna.Anchor = AnchorStyles.Left;

                    // TextBox para Cantidad por Combo
                    txtCantPaqServ.Visible = true;
                    txtCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtCantPaqServ.TabIndex = 4;
                    txtCantPaqServ.TabStop = true;

                    // TextBox para Código de Barras
                    txtCodigoBarras.Visible = true;
                    txtCodigoBarras.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtCodigoBarras.TabIndex = 5;
                    txtCodigoBarras.TabStop = true;

                    // Button para Generar Código de Barras
                    btnGenerarCB.Visible = true;
                    btnGenerarCB.Anchor = AnchorStyles.Left;
                    btnGenerarCB.TabIndex = 6;
                    btnGenerarCB.TabStop = true;

                    btnAddCodBar.Visible = true;
                    btnAddCodBar.Anchor = AnchorStyles.Left; // | AnchorStyles.Right
                    btnAddCodBar.TabIndex = 7;
                    btnAddCodBar.TabStop = true;

                    if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
                    {
                        using (DataTable dtProdServCombo = cn.CargarDatos(cs.buscarProductosDeServicios(Convert.ToInt32(idEditarProducto))))
                        {
                            if (!dtProdServCombo.Rows.Count.Equals(0))
                            {
                                foreach (DataRow drProdServCombo in dtProdServCombo.Rows)
                                {
                                    txtCantPaqServ.Text = drProdServCombo["Cantidad"].ToString();
                                }
                            }
                            else
                            {
                                txtCantPaqServ.Text = "1";
                            }
                        }
                    }
                    else if (DatosSourceFinal.Equals(1))
                    {
                        txtCantPaqServ.Text = "0";
                    }

                    // Label signo de ayuda
                    lblCantCombServ.Visible = true;
                    lblCantCombServ.Anchor = AnchorStyles.Left;

                    tLPCombo.Controls.Add(txtClaveProducto, 0, 3);     // Clave Interna TextBox
                    tLPCombo.Controls.Add(lbClaveInterna, 1, 3);       // Label de exclamation Clave Interna
                    tLPCombo.Controls.Add(txtCodigoBarras, 2, 3);      // Código de Barras TextBox
                    tLPCombo.Controls.Add(btnGenerarCB, 3, 3);         // Código de Barras Button
                    tLPCombo.Controls.Add(btnAddCodBar, 4, 3);         // Botón de generar códigos de barra extra
                    tLPCombo.Controls.Add(txtCantPaqServ, 0, 3);       // Relacionar con Combo/Servicio TextBox
                    tLPCombo.Controls.Add(lblCantCombServ, 1, 3);      // Label signo de ayuda

                    #endregion End Row 4

                    // Quinta Fila del TableLayoutPanel
                    // Label título
                    #region Begin Row 5

                    //// Label para Cantidad por Combo
                    //lblCantPaqServ.Visible = true;
                    //lblCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    ////lblCantPaqServ.TextAlign = ContentAlignment.MiddleLeft;

                    //tLPCombo.Controls.Add(lblCantPaqServ, 0, 4);       // Relacionar con Combo/Servicio Label

                    panelContenedor.Visible = true;
                    //panelContenedor.TabIndex = 8;
                    panelContenedor.TabStop = true;

                    tLPCombo.Controls.Add(panelContenedor, 2, 4);      // Contenedor de Código Barras extra Panel
                    tLPCombo.SetColumnSpan(panelContenedor, 2);
                    tLPCombo.SetRowSpan(panelContenedor, 2);

                    #endregion End Row 3

                    // Sexta Fila del TableLayoutPanel
                    // TextBox  
                    #region Begin Row 6

                    //// TextBox para Cantidad por Combo
                    //txtCantPaqServ.Visible = true;
                    //txtCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    //txtCantPaqServ.TabIndex = 6;
                    //txtCantPaqServ.TabStop = true;


                    //tLPCombo.Controls.Add(txtCantPaqServ, 0, 5);       // Relacionar con Combo/Servicio TextBox

                    #endregion End Row 6

                    // Fila del TableLayoutPanel
                    #region Begin Row imagen

                    // Panel par Imagen de Cmbo
                    PImagen.Visible = true;
                    //PImagen.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    PImagen.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    PImagen.TabIndex = 9;
                    PImagen.TabStop = true;

                    tLPCombo.Controls.Add(PImagen, 5, 0);              // Imagen del Producto Panel
                                                                       //tLPCombo.SetColumnSpan(PImagen, 2);
                    tLPCombo.SetRowSpan(PImagen, 5);

                    #endregion End Row 4
                }
                if (DatosSourceFinal.Equals(3))
                {
                    // Primera Fila del TableLayoutPanel
                    // Label títulos
                    #region Begin Row 1

                    // Label para Precio Compra
                    label7.Visible = true;
                    label7.Anchor = AnchorStyles.Left | AnchorStyles.Bottom; // | AnchorStyles.Right

                    // Label para Precio Venta
                    label4.Visible = true;
                    label4.Anchor = AnchorStyles.Left | AnchorStyles.Bottom; // | AnchorStyles.Right

                    tLPCombo.Controls.Add(label7, 0, 0);               // Precio Compra Label
                    tLPCombo.Controls.Add(label4, 2, 0);               // Precio Venta Label

                    #endregion End Row 1

                    // Segunda Fila del TableLayoutPanel
                    // TextBox y label informativo
                    #region Begin Row 2

                    // TextBox para Precio Compra
                    txtPrecioCompra.Visible = true;
                    txtPrecioCompra.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtPrecioCompra.TabIndex = 1;
                    txtPrecioCompra.TabStop = true;
                    // Label de exclamation Precio Compra
                    lbPrecioCompra.Visible = true;
                    lbPrecioCompra.Anchor = AnchorStyles.Left; // | AnchorStyles.Right

                    // TextBox para Precio Venta
                    txtPrecioProducto.Visible = true;
                    txtPrecioProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtPrecioProducto.TabIndex = 2;
                    txtPrecioProducto.TabStop = true;
                    // Label de exclamation Precio Venta
                    lbPrecioVenta.Visible = true;
                    lbPrecioVenta.Anchor = AnchorStyles.Left; // | AnchorStyles.Righ

                    tLPCombo.Controls.Add(txtPrecioCompra, 0, 1);      // Precio Compra TextBox
                    tLPCombo.Controls.Add(lbPrecioCompra, 1, 1);       // Label de exclamation Precio Compra
                    tLPCombo.Controls.Add(txtPrecioProducto, 2, 1);    // Precio Venta TextBox
                    tLPCombo.Controls.Add(lbPrecioVenta, 3, 1);        // Label de exclamation Precio Venta

                    #endregion End Row 2

                    // Tercera Fila del TableLayoutPanel
                    // Label títulos
                    #region Begin Row 3

                    // Label para Código de Barras
                    label2.Visible = true;
                    label2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top;
                    //label2.TextAlign = ContentAlignment.MiddleCenter;

                    // Label informativo para Código de Barras o Clave Interna
                    lblCodigoBarras.Visible = true;
                    lblCodigoBarras.Anchor = AnchorStyles.Left;

                    // Label para Cantidad por Combo
                    lblCantPaqServ.Visible = true;
                    lblCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    lblCantPaqServ.TextAlign = ContentAlignment.MiddleLeft;

                    //tLPCombo.Controls.Add(label5, 0, 2);               // Clave Interna Label
                    tLPCombo.Controls.Add(label2, 2, 2);               // Código de Barras Label
                    tLPCombo.Controls.Add(lblCodigoBarras, 3, 2);      // Simbolo Informativo
                    tLPCombo.Controls.Add(lblCodBarExtra, 4, 2);       // Simbolo Informativo gregar código Barras extra
                    tLPCombo.Controls.Add(lblCantPaqServ, 0, 2);       // Relacionar con Combo/Servicio Label

                    #endregion End Row 3

                    // Cuarta Fila del TableLayoutPanel
                    // TextBox y label
                    #region Begin Row 4

                    // TextBox para Código de Barras
                    txtCodigoBarras.Visible = true;
                    txtCodigoBarras.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtCodigoBarras.TabIndex = 4;
                    txtCodigoBarras.TabStop = true;

                    // Button para Generar Código de Barras
                    btnGenerarCB.Visible = true;
                    btnGenerarCB.Anchor = AnchorStyles.Left;
                    btnGenerarCB.TabIndex = 5;
                    btnGenerarCB.TabStop = true;

                    btnAddCodBar.Visible = true;
                    btnAddCodBar.Anchor = AnchorStyles.Left;
                    btnAddCodBar.TabIndex = 6;
                    btnAddCodBar.TabStop = true;

                    // TextBox para Cantidad por Combo
                    txtCantPaqServ.Visible = true;
                    txtCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtCantPaqServ.TabIndex = 7;
                    txtCantPaqServ.TabStop = true;

                    if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
                    {
                        using (DataTable dtProdServCombo = cn.CargarDatos(cs.buscarProductosDeServicios(Convert.ToInt32(idEditarProducto))))
                        {
                            if (!dtProdServCombo.Rows.Count.Equals(0))
                            {
                                foreach (DataRow drProdServCombo in dtProdServCombo.Rows)
                                {
                                    txtCantPaqServ.Text = drProdServCombo["Cantidad"].ToString();
                                }
                            }
                            else
                            {
                                txtCantPaqServ.Text = "1";
                            }
                        }
                    }
                    else if (DatosSourceFinal.Equals(1))
                    {
                        txtCantPaqServ.Text = "0";
                    }

                    // Label signo de ayuda
                    lblCantCombServ.Visible = true;
                    lblCantCombServ.Anchor = AnchorStyles.Left;

                    tLPCombo.Controls.Add(txtCodigoBarras, 2, 3);      // Código de Barras TextBox
                    tLPCombo.Controls.Add(btnGenerarCB, 3, 3);         // Código de Barras Button
                    tLPCombo.Controls.Add(btnAddCodBar, 4, 3);         // Botón de generar códigos de barra extra
                    tLPCombo.Controls.Add(txtCantPaqServ, 0, 3);       // Relacionar con Combo/Servicio TextBox
                    tLPCombo.Controls.Add(lblCantCombServ, 1, 3);      // Label signo de ayuda

                    #endregion End Row 4

                    // Quinta Fila del TableLayoutPanel
                    // Label título
                    #region Begin Row 5

                    panelContenedor.Visible = true;
                    panelContenedor.TabStop = true;
                    panelContenedor.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

                    tLPCombo.Controls.Add(panelContenedor, 2, 4);      // Contenedor de Código Barras extra Panel
                    tLPCombo.SetColumnSpan(panelContenedor, 2);
                    tLPCombo.SetRowSpan(panelContenedor, 2);

                    #endregion End Row 3

                    // Fila del TableLayoutPanel
                    #region Begin Row imagen

                    // Panel par Imagen de Cmbo
                    PImagen.Visible = true;
                    PImagen.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    PImagen.TabIndex = 9;
                    PImagen.TabStop = true;

                    tLPCombo.Controls.Add(PImagen, 5, 0);              // Imagen del Producto Panel
                    tLPCombo.SetRowSpan(PImagen, 5);

                    #endregion End Row 4
                }
            }
            else if (conSinClaveInterna.Equals(true))
            {
                // Primera Fila del TableLayoutPanel
                // Label títulos
                #region Begin Row 1

                // Label para Precio Compra
                label7.Visible = true;
                label7.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

                // Label para Precio Venta
                label4.Visible = true;
                label4.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                //label4.TextAlign = ContentAlignment.MiddleCenter;

                // Label para Cantidad por Servicio
                lblCantPaqServ.Visible = true;
                lblCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                //lblCantPaqServ.TextAlign = ContentAlignment.MiddleLeft;

                // Panel para Imagen del Servicio
                PImagen.Visible = true;
                PImagen.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                PImagen.TabIndex = 7;
                PImagen.TabStop = true;

                tLPCombo.Controls.Add(label7, 0, 0);               // Precio Compra Label
                tLPCombo.Controls.Add(label4, 2, 0);               // Precio Venta Label
                tLPCombo.Controls.Add(lblCantPaqServ, 4, 0);       // Cantidad Servicio Label

                tLPCombo.Controls.Add(PImagen, 6, 0);              // Imagen del Producto Panel
                tLPCombo.SetColumnSpan(PImagen, 2);
                tLPCombo.SetRowSpan(PImagen, 5);

                #endregion

                // Segunda Fila del TableLayoutPanel
                // TextBox y label
                #region Begin Row 2

                // TextBox para Precio Compra
                txtPrecioCompra.Visible = true;
                txtPrecioCompra.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtPrecioCompra.TabIndex = 1;
                txtPrecioCompra.TabStop = true;
                // Label de exclamation Precio Compra
                lbPrecioCompra.Visible = true;
                lbPrecioCompra.Anchor = AnchorStyles.Left;

                // TextBox para Precio Venta
                txtPrecioProducto.Visible = true;
                txtPrecioProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtPrecioProducto.TabIndex = 2;
                txtPrecioProducto.TabStop = true;
                // Label de exclamation Precio Venta
                lbPrecioVenta.Visible = true;
                lbPrecioVenta.Anchor = AnchorStyles.Left;

                // TextBox para Cantidad por Servicio
                txtCantPaqServ.Visible = true;
                txtCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtCantPaqServ.TabIndex = 5;
                txtCantPaqServ.TabStop = true;
                // Label signo de ayuda
                lblCantCombServ.Visible = true;
                lblCantCombServ.Anchor = AnchorStyles.Left;

                if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
                {
                    using (DataTable dtProdServCombo = cn.CargarDatos(cs.buscarProductosDeServicios(Convert.ToInt32(idEditarProducto))))
                    {
                        if (!dtProdServCombo.Rows.Count.Equals(0))
                        {
                            foreach (DataRow drProdServCombo in dtProdServCombo.Rows)
                            {
                                txtCantPaqServ.Text = drProdServCombo["Cantidad"].ToString();
                            }
                        }
                        else
                        {
                            txtCantPaqServ.Text = "1";
                        }
                    }
                }
                else if (DatosSourceFinal.Equals(1))
                {
                    txtCantPaqServ.Text = "0";
                }

                tLPCombo.Controls.Add(txtPrecioCompra, 0, 1);      // Precio Compra TextBox
                tLPCombo.Controls.Add(lbPrecioCompra, 1, 1);       // Label de exclamation Precio Compra
                tLPCombo.Controls.Add(txtPrecioProducto, 2, 1);    // Precio Venta TextBox
                tLPCombo.Controls.Add(lbPrecioVenta, 3, 1);        // Label de exclamation Precio Venta
                tLPCombo.Controls.Add(txtCantPaqServ, 4, 1);
                tLPCombo.Controls.Add(lblCantCombServ, 5, 1);

                #endregion

                // Tercera Fila del TableLayoutPanel
                // Label títulos
                #region Begin Row 3

                // Label para Clave Interna
                label5.Visible = true;
                label5.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

                // Label para Código de Barras
                label2.Visible = true;
                label2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                //label2.TextAlign = ContentAlignment.MiddleCenter;

                tLPCombo.Controls.Add(label5, 0, 2);               // Clave Interna Label
                tLPCombo.Controls.Add(label2, 2, 2);               // Código de Barras Label

                #endregion

                // Cuarta Fila del TableLayoutPanel
                // TextBox y Label 
                #region Begin Row 4

                // TextBox para Clave Interna
                txtClaveProducto.Visible = true;
                txtClaveProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtClaveProducto.TabIndex = 3;
                txtClaveProducto.TabStop = true;
                // Label de exclamation Clave Interna
                lbClaveInterna.Visible = true;
                lbClaveInterna.Anchor = AnchorStyles.Left;

                // TextBox para Código de Barras
                txtCodigoBarras.Visible = true;
                txtCodigoBarras.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtCodigoBarras.TabIndex = 4;
                txtCodigoBarras.TabStop = true;
                // Button para Gnerar Código de Barras
                btnGenerarCB.Visible = true;
                btnGenerarCB.Anchor = AnchorStyles.Left;
                btnGenerarCB.TabIndex = 5;
                btnGenerarCB.TabStop = true;

                tLPCombo.Controls.Add(txtClaveProducto, 0, 3);     // Clave Interna TextBox
                tLPCombo.Controls.Add(lbClaveInterna, 1, 3);
                tLPCombo.Controls.Add(txtCodigoBarras, 2, 3);      // Código de Barras TextBox
                tLPCombo.Controls.Add(btnGenerarCB, 3, 3);         // Código de Barras Button

                #endregion

                // Quinta Fila del TableLayoutPanel
                // Label títulos
                #region Begin Row 5

                panelContenedor.Visible = true;
                panelContenedor.TabIndex = 6;
                panelContenedor.TabStop = true;

                tLPCombo.Controls.Add(panelContenedor, 2, 4);      // Código de Barras TextBox
                tLPCombo.SetColumnSpan(panelContenedor, 2);
                tLPCombo.SetRowSpan(panelContenedor, 2);

                #endregion
            }

            // Button para Relacionar Productos al Combo
            button1.Visible = true;
            //button1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            button1.TabIndex = 7;
            button1.TabStop = true;

            /*// Panel para Generar Códigos de Barra Extra
            panelContenedor.Visible = true;
            panelContenedor.TabIndex = 8;
            panelContenedor.TabStop = true;
            
             tLPCombo.Controls.Add(button1, 2, 2);              // Combo/Servicio Button
            tLPCombo.SetColumnSpan(button1, 2);
            tLPCombo.Controls.Add(panelContenedor, 4, 2);      // Contenedor de Código Barras extra Panel
            tLPCombo.SetColumnSpan(panelContenedor, 3);
            tLPCombo.SetRowSpan(panelContenedor, 2);*/
        }

        private void agregarServicio()
        {
            // pasar controles a form principal
            cambiarControlesAgregarEditarProducto();

            // borrar todas las filas y columnas del TableLayoutPanel
            clearSetUpTableLayoutPanel("Servicio");

            bool conSinClaveInterna = false;

            using (DataTable dtChecarConSinClaveInternaUsuario = cn.CargarDatos(cs.verificarConSinClaveInterna(FormPrincipal.userID)))
            {
                if (!dtChecarConSinClaveInternaUsuario.Rows.Count.Equals(0))
                {
                    foreach (DataRow drChecarConSin in dtChecarConSinClaveInternaUsuario.Rows)
                    {
                        if (drChecarConSin["SinClaveInterna"].Equals(0))
                        {
                            conSinClaveInterna = false;
                        }
                        else if (drChecarConSin["SinClaveInterna"].Equals(1))
                        {
                            conSinClaveInterna = true;
                        }
                    }
                }
            }

            if (conSinClaveInterna.Equals(false))
            {
                // creamos 5 columnas en el TableLayoutPanel
                for (int i = 0; i < 6; i++)
                {
                    tLPServicio.ColumnCount++;

                    if (i.Equals(0) || i.Equals(2) || i.Equals(5))
                    {
                        tLPServicio.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
                    }
                    if (i.Equals(1) || i.Equals(3) || i.Equals(4))
                    {
                        tLPServicio.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
                    }
                }
            }
            else if (conSinClaveInterna.Equals(true))
            {
                // creamos 8 columnas en el TableLayoutPanel
                for (int i = 0; i < 8; i++)
                {
                    tLPServicio.ColumnCount++;
                    if (i.Equals(0) || i.Equals(2) || i.Equals(4) || i.Equals(6))
                    {
                        tLPServicio.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
                    }
                    else if (i.Equals(1) || i.Equals(3) || i.Equals(5) || i.Equals(7))
                    {
                        tLPServicio.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
                    }
                }
            }

            // creamos 6 filas en el TableLayoutPanel
            for (int i = 0; i < 6; i++)
            {
                tLPServicio.RowCount++;
                tLPServicio.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            }

            if (conSinClaveInterna.Equals(false))
            {
                if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(2))
                {
                    // Primera Fila del TableLayoutPanel
                    // Label títulos
                    #region Begin Row 1

                    // Label para Precio Compra
                    label7.Visible = true;
                    label7.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

                    // Label para Precio Venta
                    label4.Visible = true;
                    label4.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                    //label4.TextAlign = ContentAlignment.MiddleCenter;

                    tLPServicio.Controls.Add(label7, 0, 0);               // Precio Compra Label
                    tLPServicio.Controls.Add(label4, 2, 0);               // Precio Venta Label

                    #endregion End Row 1

                    // Segunda Fila del TableLayoutPanel
                    // TextBox y label
                    #region Begin Row 2

                    // TextBox para Precio Compra
                    txtPrecioCompra.Visible = true;
                    txtPrecioCompra.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtPrecioCompra.TabIndex = 1;
                    txtPrecioCompra.TabStop = true;
                    // Label de exclamation Precio Compra
                    lbPrecioCompra.Visible = true;
                    lbPrecioCompra.Anchor = AnchorStyles.Left;

                    // TextBox para Precio Venta
                    txtPrecioProducto.Visible = true;
                    txtPrecioProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtPrecioProducto.TabIndex = 2;
                    txtPrecioProducto.TabStop = true;
                    // Label de exclamation Precio Venta
                    lbPrecioVenta.Visible = true;
                    lbPrecioVenta.Anchor = AnchorStyles.Left;

                    tLPServicio.Controls.Add(txtPrecioCompra, 0, 1);      // Precio Compra TextBox
                    tLPServicio.Controls.Add(lbPrecioCompra, 1, 1);       // Label de exclamation Precio Compra
                    tLPServicio.Controls.Add(txtPrecioProducto, 2, 1);    // Precio Venta TextBox
                    tLPServicio.Controls.Add(lbPrecioVenta, 3, 1);        // Label de exclamation Precio Venta

                    #endregion End Row 2

                    // Tercera Fila del TableLayoutPanel
                    // Label títulos
                    #region Begin Row 3

                    // Label para Clave Interna
                    label5.Visible = true;
                    label5.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

                    // Label para Código de Barras
                    label2.Visible = true;
                    label2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top;
                    //label2.TextAlign = ContentAlignment.MiddleCenter;

                    // Label informativo para Código de Barras o Clave Interna
                    lblCodigoBarras.Visible = true;
                    lblCodigoBarras.Anchor = AnchorStyles.Left;

                    //lblCodBarExtra.Visible = true;
                    //lblCodBarExtra.Anchor = AnchorStyles.Left | AnchorStyles.Right;

                    // Label para Cantidad por Servicio
                    lblCantPaqServ.Visible = true;
                    lblCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    //lblCantPaqServ.TextAlign = ContentAlignment.MiddleLeft;

                    tLPServicio.Controls.Add(label5, 0, 2);               // Clave Interna Label
                    tLPServicio.Controls.Add(label2, 2, 2);               // Código de Barras Label
                    tLPServicio.Controls.Add(lblCodigoBarras, 3, 2);      // Simbolo Informativo
                                                                          //tLPServicio.Controls.Add(lblCodBarExtra, 4, 2);
                    tLPServicio.Controls.Add(lblCantPaqServ, 0, 2);       // Clave Interna Label

                    #endregion End Row 3

                    // Cuarta Fila del TableLayoutPanel
                    // TextBox y Label 
                    #region Begin Row 4

                    // TextBox para Clave Interna
                    txtClaveProducto.Visible = true;
                    txtClaveProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtClaveProducto.TabIndex = 3;
                    txtClaveProducto.TabStop = true;
                    // Label de exclamation Clave Interna
                    lbClaveInterna.Visible = true;
                    lbClaveInterna.Anchor = AnchorStyles.Left;

                    // TextBox para Cantidad por Servicio
                    txtCantPaqServ.Visible = true;
                    txtCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtCantPaqServ.TabIndex = 4;
                    txtCantPaqServ.TabStop = true;

                    // TextBox para Código de Barras
                    txtCodigoBarras.Visible = true;
                    txtCodigoBarras.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtCodigoBarras.TabIndex = 5;
                    txtCodigoBarras.TabStop = true;
                    // Button para Gnerar Código de Barras
                    btnGenerarCB.Visible = true;
                    btnGenerarCB.Anchor = AnchorStyles.Left;
                    btnGenerarCB.TabIndex = 6;
                    btnGenerarCB.TabStop = true;

                    // Button Agregar Codigo Barras extra
                    btnAddCodBar.Visible = true;
                    btnAddCodBar.Anchor = AnchorStyles.Left; // | AnchorStyles.Right
                    btnAddCodBar.TabIndex = 7;
                    btnAddCodBar.TabStop = true;

                    // Label signo de ayuda
                    lblCantCombServ.Visible = true;
                    lblCantCombServ.Anchor = AnchorStyles.Left;

                    if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
                    {
                        using (DataTable dtProdServCombo = cn.CargarDatos(cs.buscarProductosDeServicios(Convert.ToInt32(idEditarProducto))))
                        {
                            if (!dtProdServCombo.Rows.Count.Equals(0))
                            {
                                foreach (DataRow drProdServCombo in dtProdServCombo.Rows)
                                {
                                    txtCantPaqServ.Text = drProdServCombo["Cantidad"].ToString();
                                }
                            }
                            else
                            {
                                txtCantPaqServ.Text = "1";
                            }
                        }
                    }
                    else if (DatosSourceFinal.Equals(1))
                    {
                        txtCantPaqServ.Text = "0";
                    }

                    tLPServicio.Controls.Add(txtClaveProducto, 0, 3);     // Clave Interna TextBox
                    tLPServicio.Controls.Add(lbClaveInterna, 1, 3);
                    tLPServicio.Controls.Add(txtCodigoBarras, 2, 3);      // Código de Barras TextBox
                    tLPServicio.Controls.Add(btnGenerarCB, 3, 3);         // Código de Barras Button
                    tLPServicio.Controls.Add(btnAddCodBar, 4, 3);         // Botón de generar códigos de barra extra
                    tLPServicio.Controls.Add(txtCantPaqServ, 0, 3);       // Clave Interna TextBox
                    tLPServicio.Controls.Add(lblCantCombServ, 1, 3);      // Label signo de ayuda

                    #endregion End Row 4

                    // Quinta Fila del TableLayoutPanel
                    // Label títulos
                    #region Begin Row 5

                    //// Label para Cantidad por Servicio
                    //lblCantPaqServ.Visible = true;
                    //lblCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    ////lblCantPaqServ.TextAlign = ContentAlignment.MiddleLeft;

                    //tLPServicio.Controls.Add(lblCantPaqServ, 0, 4);       // Clave Interna Label

                    panelContenedor.Visible = true;
                    panelContenedor.TabIndex = 6;
                    panelContenedor.TabStop = true;

                    tLPServicio.Controls.Add(panelContenedor, 2, 4);      // Código de Barras TextBox
                    tLPServicio.SetColumnSpan(panelContenedor, 2);
                    tLPServicio.SetRowSpan(panelContenedor, 2);

                    #endregion End Row 5

                    // Sexta Fila del TableLayoutPanel
                    // TextBox y Label
                    #region Begin Row 6

                    //// TextBox para Cantidad por Servicio
                    //txtCantPaqServ.Visible = true;
                    //txtCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    //txtCantPaqServ.TabIndex = 5;
                    //txtCantPaqServ.TabStop = true;

                    //tLPServicio.Controls.Add(txtCantPaqServ, 0, 5);       // Clave Interna TextBox

                    #endregion End Row 6


                    // Cuarta Fila del TableLayoutPanel
                    #region Begin Row 4

                    // Panel para Imagen del Servicio
                    PImagen.Visible = true;
                    PImagen.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    PImagen.TabIndex = 7;
                    PImagen.TabStop = true;

                    tLPServicio.Controls.Add(PImagen, 5, 0);              // Imagen del Producto Panel
                    tLPServicio.SetRowSpan(PImagen, 5);

                    #endregion End Row 4
                }
                if (DatosSourceFinal.Equals(3))
                {
                    // Primera Fila del TableLayoutPanel
                    // Label títulos
                    #region Begin Row 1

                    // Label para Precio Compra
                    label7.Visible = true;
                    label7.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

                    // Label para Precio Venta
                    label4.Visible = true;
                    label4.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                    //label4.TextAlign = ContentAlignment.MiddleCenter;

                    tLPServicio.Controls.Add(label7, 0, 0);               // Precio Compra Label
                    tLPServicio.Controls.Add(label4, 2, 0);               // Precio Venta Label

                    #endregion End Row 1

                    // Segunda Fila del TableLayoutPanel
                    // TextBox y label
                    #region Begin Row 2

                    // TextBox para Precio Compra
                    txtPrecioCompra.Visible = true;
                    txtPrecioCompra.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtPrecioCompra.TabIndex = 1;
                    txtPrecioCompra.TabStop = true;
                    // Label de exclamation Precio Compra
                    lbPrecioCompra.Visible = true;
                    lbPrecioCompra.Anchor = AnchorStyles.Left;

                    // TextBox para Precio Venta
                    txtPrecioProducto.Visible = true;
                    txtPrecioProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtPrecioProducto.TabIndex = 2;
                    txtPrecioProducto.TabStop = true;
                    // Label de exclamation Precio Venta
                    lbPrecioVenta.Visible = true;
                    lbPrecioVenta.Anchor = AnchorStyles.Left;

                    tLPServicio.Controls.Add(txtPrecioCompra, 0, 1);      // Precio Compra TextBox
                    tLPServicio.Controls.Add(lbPrecioCompra, 1, 1);       // Label de exclamation Precio Compra
                    tLPServicio.Controls.Add(txtPrecioProducto, 2, 1);    // Precio Venta TextBox
                    tLPServicio.Controls.Add(lbPrecioVenta, 3, 1);        // Label de exclamation Precio Venta

                    #endregion End Row 2

                    // Tercera Fila del TableLayoutPanel
                    // Label títulos
                    #region Begin Row 3

                    // Label para Código de Barras
                    label2.Visible = true;
                    label2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top;
                    //label2.TextAlign = ContentAlignment.MiddleCenter;

                    // Label informativo para Código de Barras o Clave Interna
                    lblCodigoBarras.Visible = true;
                    lblCodigoBarras.Anchor = AnchorStyles.Left;

                    // Label para Cantidad por Combo
                    lblCantPaqServ.Visible = true;
                    lblCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    lblCantPaqServ.TextAlign = ContentAlignment.MiddleLeft;

                    //tLPCombo.Controls.Add(label5, 0, 2);               // Clave Interna Label
                    tLPServicio.Controls.Add(label2, 2, 2);               // Código de Barras Label
                    tLPServicio.Controls.Add(lblCodigoBarras, 3, 2);      // Simbolo Informativo
                    tLPServicio.Controls.Add(lblCodBarExtra, 4, 2);       // Simbolo Informativo gregar código Barras extra
                    tLPServicio.Controls.Add(lblCantPaqServ, 0, 2);       // Relacionar con Combo/Servicio Label

                    #endregion End Row 3

                    // Cuarta Fila del TableLayoutPanel
                    // TextBox y label
                    #region Begin Row 4

                    // TextBox para Código de Barras
                    txtCodigoBarras.Visible = true;
                    txtCodigoBarras.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtCodigoBarras.TabIndex = 4;
                    txtCodigoBarras.TabStop = true;

                    // Button para Generar Código de Barras
                    btnGenerarCB.Visible = true;
                    btnGenerarCB.Anchor = AnchorStyles.Left;
                    btnGenerarCB.TabIndex = 5;
                    btnGenerarCB.TabStop = true;

                    btnAddCodBar.Visible = true;
                    btnAddCodBar.Anchor = AnchorStyles.Left;
                    btnAddCodBar.TabIndex = 6;
                    btnAddCodBar.TabStop = true;

                    // TextBox para Cantidad por Combo
                    txtCantPaqServ.Visible = true;
                    txtCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    txtCantPaqServ.TabIndex = 7;
                    txtCantPaqServ.TabStop = true;

                    if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
                    {
                        using (DataTable dtProdServCombo = cn.CargarDatos(cs.buscarProductosDeServicios(Convert.ToInt32(idEditarProducto))))
                        {
                            if (!dtProdServCombo.Rows.Count.Equals(0))
                            {
                                foreach (DataRow drProdServCombo in dtProdServCombo.Rows)
                                {
                                    txtCantPaqServ.Text = drProdServCombo["Cantidad"].ToString();
                                }
                            }
                            else
                            {
                                txtCantPaqServ.Text = "1";
                            }
                        }
                    }
                    else if (DatosSourceFinal.Equals(1))
                    {
                        txtCantPaqServ.Text = "0";
                    }

                    // Label signo de ayuda
                    lblCantCombServ.Visible = true;
                    lblCantCombServ.Anchor = AnchorStyles.Left;

                    tLPServicio.Controls.Add(txtCodigoBarras, 2, 3);      // Código de Barras TextBox
                    tLPServicio.Controls.Add(btnGenerarCB, 3, 3);         // Código de Barras Button
                    tLPServicio.Controls.Add(btnAddCodBar, 4, 3);         // Botón de generar códigos de barra extra
                    tLPServicio.Controls.Add(txtCantPaqServ, 0, 3);       // Relacionar con Combo/Servicio TextBox
                    tLPServicio.Controls.Add(lblCantCombServ, 1, 3);      // Label signo de ayuda

                    #endregion End Row 4

                    // Quinta Fila del TableLayoutPanel
                    // Label título
                    #region Begin Row 5

                    panelContenedor.Visible = true;
                    panelContenedor.TabStop = true;
                    panelContenedor.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

                    tLPServicio.Controls.Add(panelContenedor, 2, 4);      // Contenedor de Código Barras extra Panel
                    tLPServicio.SetColumnSpan(panelContenedor, 2);
                    tLPServicio.SetRowSpan(panelContenedor, 2);

                    #endregion End Row 3

                    // Fila del TableLayoutPanel
                    #region Begin Row imagen

                    // Panel par Imagen de Cmbo
                    PImagen.Visible = true;
                    PImagen.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    PImagen.TabIndex = 9;
                    PImagen.TabStop = true;

                    tLPServicio.Controls.Add(PImagen, 5, 0);              // Imagen del Producto Panel
                    tLPServicio.SetRowSpan(PImagen, 5);

                    #endregion End Row imagen
                }
            }
            else if (conSinClaveInterna.Equals(true))
            {
                // Primera Fila del TableLayoutPanel
                // Label títulos
                #region Begin Row 1

                // Label para Precio Compra
                label7.Visible = true;
                label7.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

                // Label para Precio Venta
                label4.Visible = true;
                label4.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                //label4.TextAlign = ContentAlignment.MiddleCenter;

                // Label para Cantidad por Servicio
                lblCantPaqServ.Visible = true;
                lblCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                //lblCantPaqServ.TextAlign = ContentAlignment.MiddleLeft;

                // Panel para Imagen del Servicio
                PImagen.Visible = true;
                PImagen.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                PImagen.TabIndex = 7;
                PImagen.TabStop = true;

                tLPServicio.Controls.Add(label7, 0, 0);               // Precio Compra Label
                tLPServicio.Controls.Add(label4, 2, 0);               // Precio Venta Label
                tLPServicio.Controls.Add(lblCantPaqServ, 4, 0);       // Cantidad Servicio Label

                tLPServicio.Controls.Add(PImagen, 6, 0);              // Imagen del Producto Panel
                tLPServicio.SetColumnSpan(PImagen, 2);
                tLPServicio.SetRowSpan(PImagen, 5);

                #endregion End Row 1

                // Segunda Fila del TableLayoutPanel
                // TextBox y label
                #region Begin Row 2

                // TextBox para Precio Compra
                txtPrecioCompra.Visible = true;
                txtPrecioCompra.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtPrecioCompra.TabIndex = 1;
                txtPrecioCompra.TabStop = true;
                // Label de exclamation Precio Compra
                lbPrecioCompra.Visible = true;
                lbPrecioCompra.Anchor = AnchorStyles.Left;

                // TextBox para Precio Venta
                txtPrecioProducto.Visible = true;
                txtPrecioProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtPrecioProducto.TabIndex = 2;
                txtPrecioProducto.TabStop = true;
                // Label de exclamation Precio Venta
                lbPrecioVenta.Visible = true;
                lbPrecioVenta.Anchor = AnchorStyles.Left;

                // TextBox para Cantidad por Servicio
                txtCantPaqServ.Visible = true;
                txtCantPaqServ.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtCantPaqServ.TabIndex = 5;
                txtCantPaqServ.TabStop = true;
                // Label signo de ayuda
                lblCantCombServ.Visible = true;
                lblCantCombServ.Anchor = AnchorStyles.Left;

                if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
                {
                    using (DataTable dtProdServCombo = cn.CargarDatos(cs.buscarProductosDeServicios(Convert.ToInt32(idEditarProducto))))
                    {
                        if (!dtProdServCombo.Rows.Count.Equals(0))
                        {
                            foreach (DataRow drProdServCombo in dtProdServCombo.Rows)
                            {
                                txtCantPaqServ.Text = drProdServCombo["Cantidad"].ToString();
                            }
                        }
                        else
                        {
                            txtCantPaqServ.Text = "1";
                        }
                    }
                }
                else if (DatosSourceFinal.Equals(1))
                {
                    txtCantPaqServ.Text = "0";
                }

                tLPServicio.Controls.Add(txtPrecioCompra, 0, 1);      // Precio Compra TextBox
                tLPServicio.Controls.Add(lbPrecioCompra, 1, 1);       // Label de exclamation Precio Compra
                tLPServicio.Controls.Add(txtPrecioProducto, 2, 1);    // Precio Venta TextBox
                tLPServicio.Controls.Add(lbPrecioVenta, 3, 1);        // Label de exclamation Precio Venta
                tLPServicio.Controls.Add(txtCantPaqServ, 4, 1);
                tLPServicio.Controls.Add(lblCantCombServ, 5, 1);

                #endregion End Row 2

                // Tercera Fila del TableLayoutPanel
                // Label títulos
                #region Begin Row 3

                // Label para Clave Interna
                label5.Visible = true;
                label5.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

                // Label para Código de Barras
                label2.Visible = true;
                label2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                //label2.TextAlign = ContentAlignment.MiddleCenter;

                tLPServicio.Controls.Add(label5, 0, 2);               // Clave Interna Label
                tLPServicio.Controls.Add(label2, 2, 2);               // Código de Barras Label

                #endregion

                // Cuarta Fila del TableLayoutPanel
                // TextBox y Label 
                #region Begin Row 4

                // TextBox para Clave Interna
                txtClaveProducto.Visible = true;
                txtClaveProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtClaveProducto.TabIndex = 3;
                txtClaveProducto.TabStop = true;
                // Label de exclamation Clave Interna
                lbClaveInterna.Visible = true;
                lbClaveInterna.Anchor = AnchorStyles.Left;

                // TextBox para Código de Barras
                txtCodigoBarras.Visible = true;
                txtCodigoBarras.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                txtCodigoBarras.TabIndex = 4;
                txtCodigoBarras.TabStop = true;
                // Button para Gnerar Código de Barras
                btnGenerarCB.Visible = true;
                btnGenerarCB.Anchor = AnchorStyles.Left;
                btnGenerarCB.TabIndex = 5;
                btnGenerarCB.TabStop = true;

                tLPServicio.Controls.Add(txtClaveProducto, 0, 3);     // Clave Interna TextBox
                tLPServicio.Controls.Add(lbClaveInterna, 1, 3);
                tLPServicio.Controls.Add(txtCodigoBarras, 2, 3);      // Código de Barras TextBox
                tLPServicio.Controls.Add(btnGenerarCB, 3, 3);         // Código de Barras Button

                #endregion

                // Quinta Fila del TableLayoutPanel
                // Label títulos
                #region Begin Row 5

                panelContenedor.Visible = true;
                panelContenedor.TabIndex = 6;
                panelContenedor.TabStop = true;

                tLPServicio.Controls.Add(panelContenedor, 2, 4);      // Código de Barras TextBox
                tLPServicio.SetColumnSpan(panelContenedor, 2);
                tLPServicio.SetRowSpan(panelContenedor, 2);

                #endregion
            }

            // Button para Relacionar Producto para Servicio
            button1.Visible = true;
            //button1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            button1.TabIndex = 8;
            button1.TabStop = true;

            // Panel para Generar Códigos de Barra Extra
            /*panelContenedor.Visible = true;
            panelContenedor.TabIndex = 7;
            panelContenedor.TabStop = true;
            
             tLPServicio.Controls.Add(button1, 2, 2);              // Código de Barras Label
            tLPServicio.SetColumnSpan(button1, 2);
            tLPServicio.Controls.Add(panelContenedor, 4, 2);      // Código de Barras TextBox
            tLPServicio.SetColumnSpan(panelContenedor, 3);
            tLPServicio.SetRowSpan(panelContenedor, 2);*/
        }

        private void clearSetUpTableLayoutPanel(string origin)
        {
            //tLPProducto.Controls.Clear();
            tLPProducto.Visible = false;
            //tLPCombo.Controls.Clear();
            tLPCombo.Visible = false;
            //tLPServicio.Controls.Clear();
            tLPServicio.Visible = false;

            if (origin.Equals("Producto"))
            {
                tLPProducto.AutoSize = true;
                tLPProducto.ColumnCount = 0;
                tLPProducto.ColumnStyles.Clear();
                tLPProducto.RowCount = 0;
                tLPProducto.RowStyles.Clear();
                tLPProducto.Visible = true;
            }
            else if (origin.Equals("Combo"))
            {
                tLPCombo.AutoSize = true;
                tLPCombo.ColumnCount = 0;
                tLPCombo.ColumnStyles.Clear();
                tLPCombo.RowCount = 0;
                tLPCombo.RowStyles.Clear();
                tLPCombo.Visible = true;
            }
            else if (origin.Equals("Servicio"))
            {
                tLPServicio.AutoSize = true;
                tLPServicio.ColumnCount = 0;
                tLPServicio.ColumnStyles.Clear();
                tLPServicio.RowCount = 0;
                tLPServicio.RowStyles.Clear();
                tLPServicio.Visible = true;
            }
        }

        private void mostrarOcultarfLPDetallesProducto()
        {
            if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3) || DatosSourceFinal.Equals(5))
            {
                if (detalleProductoBasico.Count.Equals(0) && detalleProductoGeneral.Count.Equals(0))
                {
                    fLPDetallesProducto.Visible = true;
                    //this.Height = 660;
                }
            }
            else if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
            {
                if (detalleProductoBasico.Count > 0 || detalleProductoGeneral.Count > 0)
                {
                    fLPDetallesProducto.Visible = true;
                    //this.Height = 731;
                }

                if (mb.TieneDetallesProducto(Convert.ToInt32(idEditarProducto)) == 0)
                {
                    fLPDetallesProducto.Visible = true;
                }
            }
        }

        public void actualizarDetallesProducto()
        {
            //loadFormConfig();
            loadFromConfigDB();
            BuscarChkBoxListView(chkDatabase);

            bool isEmptyProdBasic = !detalleProductoBasico.Any();
            bool isEmpyDetailProdGral = !detalleProductoGeneral.Any();

            if (!isEmptyProdBasic)
            {
                if (DatosSourceFinal.Equals(1) ||
                    DatosSourceFinal.Equals(3) ||
                    DatosSourceFinal.Equals(4))
                {
                    string Descripcion = string.Empty,
                        name = string.Empty,
                        value = string.Empty,
                        namegral = string.Empty;

                    for (int i = 0; i < detalleProductoBasico.Count; i++)
                    {
                        if (i.Equals(2))
                        {
                            var prveedorNombre = detalleProductoBasico[i].ToString();
                            for (int x = 0; x < chkDatabase.Items.Count; x++)
                            {
                                name = chkDatabase.Items[x].Text.ToString();
                                value = chkDatabase.Items[x].SubItems[1].Text.ToString();
                                foreach (Control contHijo in flowLayoutPanel3.Controls)
                                {
                                    foreach (Control contSubHijo in contHijo.Controls)
                                    {
                                        if (contSubHijo.Name.Equals("panelContenido" + name) && value.Equals("true"))
                                        {
                                            foreach (Control contItemSubHijo in contSubHijo.Controls)
                                            {
                                                if (contItemSubHijo is Label)
                                                {
                                                    contItemSubHijo.Text = detalleProductoBasico[i].ToString();
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (!isEmpyDetailProdGral)
            {
                if (DatosSourceFinal.Equals(1) ||
                    DatosSourceFinal.Equals(3) ||
                    DatosSourceFinal.Equals(4))
                {
                    string Descripcion = string.Empty,
                           name = string.Empty,
                           value = string.Empty,
                           namegral = string.Empty;

                    foreach (var item in detalleProductoGeneral)
                    {
                        var words = item.Split('|');
                        name = words[4].ToString();
                        foreach (Control contHijo in flowLayoutPanel3.Controls)
                        {
                            foreach (Control contSubHijo in contHijo.Controls)
                            {
                                if (contSubHijo.Name.Equals(name))
                                {
                                    foreach (Control contItemSubHijo in contSubHijo.Controls)
                                    {
                                        if (contItemSubHijo is Label)
                                        {
                                            var conceptoDinamico = words[5].ToString().Replace("_", " ");
                                            contItemSubHijo.Text = conceptoDinamico;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //if (!isEmpty)
            //{
            //    // Cuando se da click en la opcion editar producto
            //    if (DatosSourceFinal.Equals(1) || 
            //        DatosSourceFinal.Equals(3) || 
            //        DatosSourceFinal.Equals(4))
            //    {
            //        string Descripcion = string.Empty,
            //                name = string.Empty,
            //                value = string.Empty,
            //                namegral = string.Empty;

            //        for (int i = 0; i < chkDatabase.Items.Count; i++)
            //        {
            //            name = chkDatabase.Items[i].Text.ToString();
            //            value = chkDatabase.Items[i].SubItems[1].Text.ToString();
            //            foreach (Control contHijo in flowLayoutPanel3.Controls)
            //            {
            //              foreach (Control contSubHijo in contHijo.Controls)
            //              {
            //                  if (contSubHijo.Name.Equals("panelContenido" + name) && value.Equals("true"))
            //                  {
            //                      foreach (Control contItemSubHijo in contSubHijo.Controls)
            //                      {
            //                          if (contItemSubHijo is Label)
            //                          {
            //                              contItemSubHijo.Text = detalleProductoBasico[2].ToString();
            //                              break;
            //                          }
            //                      }
            //                  }
            //              }
            //            }
            //        } 
            //        for (int i = 0; i < chkDatabase.Items.Count; i++)
            //        {
            //            name = chkDatabase.Items[i].Text.ToString().Remove(0, 3);
            //            value = chkDatabase.Items[i].SubItems[1].Text.ToString();
            //            foreach (Control contHijo in flowLayoutPanel3.Controls)
            //            {
            //                foreach (Control contSubHijo in contHijo.Controls)
            //                {
            //                    if (contSubHijo.Name.Equals("panelContenido" + name) && value.Equals("true"))
            //                    {
            //                        for (int j = 0; j < detalleProductoGeneral.Count; j++)
            //                        {
            //                            namegral = detalleProductoGeneral[j].ToString();
            //                            if (namegral.Equals(name) &&
            //                                contSubHijo.Name.Equals("panelContenido" + name))
            //                            {
            //                                foreach (Control contItemSubHijo in contSubHijo.Controls)
            //                                {
            //                                    if (contItemSubHijo is Label)
            //                                    {
            //                                        contItemSubHijo.Text = detalleProductoGeneral[j + 2].ToString();
            //                                        break;
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private void loadFromConfigDB()
        {
            llenarDetalleProductoEspecificaciones();

            //var servidor = Properties.Settings.Default.Hosting;

            //if (string.IsNullOrWhiteSpace(servidor))
            //{
                
            //    chkDatabase.Items.Clear();
            //    settingDatabases.Items.Clear();

            //    lvi = new ListViewItem();

            //    try
            //    {
            //        chkDatabase.Clear();
            //        settingDatabases.Clear();

            //        using (DataTable dtChecarSihayDatosDinamicos = cn.CargarDatos(cs.VerificarContenidoDinamico(FormPrincipal.userID)))
            //        {
            //            if (dtChecarSihayDatosDinamicos.Rows.Count > 0)
            //            {
            //                foreach (DataRow row in dtChecarSihayDatosDinamicos.Rows)
            //                {
            //                    connStr = row["textComboBoxConcepto"].ToString();
            //                    if (row["checkBoxComboBoxConcepto"].ToString().Equals("1"))
            //                    {
            //                        keyName = "true";
            //                    }
            //                    else if (row["checkBoxComboBoxConcepto"].ToString().Equals("0"))
            //                    {
            //                        keyName = "false";
            //                    }
            //                    lvi = new ListViewItem(keyName);
            //                    lvi.SubItems.Add(connStr);
            //                    chkDatabase.Items.Add(lvi);
            //                }
            //                foreach (DataRow row in dtChecarSihayDatosDinamicos.Rows)
            //                {
            //                    connStr = row["concepto"].ToString();
            //                    if (row["checkBoxConcepto"].ToString().Equals("1"))
            //                    {
            //                        keyName = "true";
            //                    }
            //                    else if (row["checkBoxConcepto"].ToString().Equals("0"))
            //                    {
            //                        keyName = "false";
            //                    }
            //                    lvi = new ListViewItem(keyName);
            //                    lvi.SubItems.Add(connStr);
            //                    settingDatabases.Items.Add(lvi);
            //                }
            //            }
            //            else if (dtChecarSihayDatosDinamicos.Rows.Count == 0)
            //            {
            //                //MessageBox.Show("No cuenta con Cofiguración en su sistema", "Sin Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error de lectura de los Datos Dinamicos: " + ex.Message.ToString(), "Error de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private void llenarDetalleProductoEspecificaciones()
        {
            chkDatabase.Items.Clear();
            settingDatabases.Items.Clear();

            lvi = new ListViewItem();

            try
            {
                chkDatabase.Clear();
                settingDatabases.Clear();

                using (DataTable dtChecarSihayDatosDinamicos = cn.CargarDatos(cs.VerificarContenidoDinamico(FormPrincipal.userID)))
                {
                    if (dtChecarSihayDatosDinamicos.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtChecarSihayDatosDinamicos.Rows)
                        {
                            connStr = row["textComboBoxConcepto"].ToString();
                            if (row["checkBoxComboBoxConcepto"].ToString().Equals("1"))
                            {
                                keyName = "true";
                            }
                            else if (row["checkBoxComboBoxConcepto"].ToString().Equals("0"))
                            {
                                keyName = "false";
                            }
                            lvi = new ListViewItem(keyName);
                            lvi.SubItems.Add(connStr);
                            chkDatabase.Items.Add(lvi);
                        }
                        foreach (DataRow row in dtChecarSihayDatosDinamicos.Rows)
                        {
                            connStr = row["concepto"].ToString();
                            if (row["checkBoxConcepto"].ToString().Equals("1"))
                            {
                                keyName = "true";
                            }
                            else if (row["checkBoxConcepto"].ToString().Equals("0"))
                            {
                                keyName = "false";
                            }
                            lvi = new ListViewItem(keyName);
                            lvi.SubItems.Add(connStr);
                            settingDatabases.Items.Add(lvi);
                        }
                    }
                    else if (dtChecarSihayDatosDinamicos.Rows.Count == 0)
                    {
                        //MessageBox.Show("No cuenta con Cofiguración en su sistema", "Sin Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de lectura de los Datos Dinamicos: " + ex.Message.ToString(), "Error de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarCBProductos(string typePaqServ)
        {
            try
            {
                datosProductos = new DataTable();
                //queryProductos = $"SELECT ID, Nombre FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND Tipo = 'P'";
                queryProductos = $"SELECT ID, Nombre FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND Tipo = 'P' AND Status = '1'";
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
                itemCBProd.Value = datosProductos.Rows[i]["Nombre"].ToString().Replace("_", " ");
                itemCBProd.text = datosProductos.Rows[i]["ID"].ToString();
                prodList.Add(itemCBProd);
            }
        }

        private void guardar_impuestos_dexml(double basep, int id_producto)
        {
            List<string> tasasCuotas = new List<string>();


            tasasCuotas.Add("0 %");
            tasasCuotas.Add("16 %");
            tasasCuotas.Add("Definir %");
            tasasCuotas.Add("26.5 %");
            tasasCuotas.Add("30 %");
            tasasCuotas.Add("53 %");
            tasasCuotas.Add("50 %");
            tasasCuotas.Add("1.600000");
            tasasCuotas.Add("30.4 %");
            tasasCuotas.Add("25 %");
            tasasCuotas.Add("9 %");
            tasasCuotas.Add("8 %");
            tasasCuotas.Add("7 %");
            tasasCuotas.Add("6 %");
            tasasCuotas.Add("3 %");


            foreach (var list_tras in AgregarStockXML.list_impuestos_traslado_retenido)
            {
                string tra_ret = "";
                string t_impuesto = "";
                string tasa_cuota = "";
                string definir = "0.00";
                double importe_impuesto = 0;


                var dato = list_tras.Split('-');


                // Traslado - retención

                if (dato[0] == "t") { tra_ret = "Traslado"; }
                if (dato[0] == "r") { tra_ret = "Retención"; }

                // Tipo impuesto

                if (dato[1] == "001") { t_impuesto = "ISR"; }
                if (dato[1] == "002") { t_impuesto = "IVA"; }
                if (dato[1] == "003") { t_impuesto = "IEPS"; }

                // Tipo factor

                string tipo_factor = dato[2];

                // Tasa/cuota

                if (tipo_factor == "Tasa")
                {
                    tasa_cuota = dato[3];
                    if (Convert.ToDecimal(dato[3]) < 1)
                    {
                        tasa_cuota = Convert.ToString(Convert.ToDecimal(dato[3]) * 100);

                        var indice_pdec = tasa_cuota.IndexOf('.');

                        if (indice_pdec > 0)
                        {
                            // Número decimal
                            int hasta = tasa_cuota.Length - (indice_pdec + 1);
                            string decim = tasa_cuota.Substring((indice_pdec + 1), hasta);

                            if (Convert.ToDecimal(decim) == 0)
                            {
                                var num_entero = tasa_cuota.Split('.');
                                tasa_cuota = num_entero[0];
                            }
                        }
                    }

                    var exi = tasasCuotas.IndexOf(tasa_cuota + " %");
                    if (exi < 0)
                    {
                        definir = tasa_cuota;
                        tasa_cuota = "Definir %";
                    }
                    else
                    {
                        tasa_cuota += " %";
                    }
                }
                if (tipo_factor == "Cuota")
                {
                    definir = dato[3];
                    tasa_cuota = "Definir %";
                }
                if (tipo_factor == "Exento")
                {
                    tasa_cuota = "0 %";
                }

                // Importe

                if (dato[2] == "Tasa" | dato[2] == "Exento")
                {
                    if (dato[3] != "" | dato[3] != null)
                    {
                        importe_impuesto = Convert.ToDouble(dato[3]) * basep;
                    }
                }
                if (dato[2] == "Cuota")
                {
                    importe_impuesto = Convert.ToDouble(dato[3]);
                }

                if (Convert.ToDecimal(dato[3]) > 1 & dato[2] == "Tasa")
                {
                    importe_impuesto = importe_impuesto / 100;
                }


                // Guardar

                string[] datos = new string[]{
                    tra_ret, t_impuesto, tipo_factor, tasa_cuota, definir, importe_impuesto.ToString("0.00")
                };

                try
                {
                    cn.EjecutarConsulta(cs.GuardarDetallesProducto(datos, id_producto));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar los impuestos del producto.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
