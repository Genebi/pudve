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

        /****************************
		*   Codigo de Emmanuel      *
		****************************/

        #region Variables Globales

        List<string>    infoDetalle,
                        infoDetailProdGral;

        Dictionary<string, string> proveedores,
                                    categorias,
                                    ubicaciones,
                                    detallesGral;

        Dictionary<int, Tuple<string, string, string, string>> diccionarioDetallesGeneral = new Dictionary<int, Tuple<string, string, string, string>>(),
                                                               diccionarioDetalleBasicos = new Dictionary<int, Tuple<string, string, string, string>>();

        string[]    datosProveedor,
                    datosCategoria,
                    datosUbicacion,
                    datosDetalleGral,
                    separadas;

        string[]    listaProveedores = new string[] { },
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

        string  nvoDetalle = string.Empty,
                nvoValor = string.Empty,
                editValor = string.Empty,
                deleteDetalle = string.Empty,
                nombreProveedor = string.Empty,
                nombreCategoria = string.Empty,
                nombreUbicacion = string.Empty,
                nombreDetalleGral = string.Empty;

        public string getIdProducto { get; set; }

        public static string finalIdProducto = string.Empty;

        string  editDetelle = string.Empty,
                editDetalleNvo = string.Empty;

        #endregion Variables Globales
        
        bool habilitarComboBoxes = false;

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
        public string idProveedorXML { get; set; }
        public string nameProveedorXML { get; set; }

        static public int    DatosSourceFinal = 0;
        static public string ProdNombreFinal = "";
        static public string ProdStockFinal = "";
        static public string ProdPrecioFinal = "";
        static public string ProdCategoriaFinal = "";
        static public string ProdClaveInternaFinal = "";
        static public string ProdCodBarrasFinal = "";
        static public string CantProdServFinal = "";

        static public float ImporteNvoProd = 0;
        static public float DescuentoNvoProd = 0;
        static public int   CantidadNvoProd = 0;

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


        DataTable   SearchProdResult, 
                    SearchCodBarExtResult, 
                    datosProductos;

        OpenFileDialog f;       // declaramos el objeto de OpenFileDialog

        // objeto para el manejo de las imagenes
        FileStream  File, 
                    File1;

        FileInfo    info;

        string  queryBuscarProd, 
                idProductoBuscado, 
                queryUpdateProd, 
                queryInsertProd, 
                queryBuscarCodBarExt, 
                queryBuscarDescuentoCliente, 
                queryDesMayoreo, 
                queryProductosDeServicios, 
                queryNvoProductosDeServicios;

        DataTable   dtProductosDeServicios, 
                    dtNvoProductosDeServicios=null;

        // direccion de la carpeta donde se va poner las imagenes
        string saveDirectoryImg = string.Empty;

        string  fileName,           // nombre de archivo
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

        List<string> ProductosDeServicios = new List<string>(); // para agregar los productos del servicio o paquete
        List<ItemsProductoComboBox> prodList;

        int numCombo = 1, indexItem = 1, totCB=0;

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

        #region Iniciar Varaibles Globales
        List<string> datosProductosBtnGuardar,
                     datosProductoRelacionado;

        string[] guardar,
                 saveDetailProd = new string[] { "", "", "", "", "" },
                 idProveedorBtnGuardar;

        int     respuesta = 0,
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

        string  filtroTipoSerPaq = string.Empty,
                tipoServPaq = string.Empty,
                nombre = string.Empty,
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

        string  gralDetailSelected = string.Empty,
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

            string  nameChk = string.Empty,
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
                //name = lstListView.Items[i].Text.ToString();
                //value = lstListView.Items[i].SubItems[1].Text.ToString();
                name = lstListView.Items[i].SubItems[1].Text.ToString();
                value = lstListView.Items[i].Text.ToString();

                if (name.Equals("chkProveedor") && value.Equals("true"))
                {
                    nombrePanelContenedor = "panelContenedor" + name;
                    nombrePanelContenido = "panelContenido" + name;

                    Panel panelContenedor = new Panel();
                    panelContenedor.Width = 266;
                    panelContenedor.Height = 58;
                    panelContenedor.Name = nombrePanelContenedor;
                    //panelContenedor.BackColor = Color.Aqua;

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
                        panelContenido.Width = 258;
                        panelContenido.Height = 55;

                        Label lblNombreProveedor = new Label();
                        lblNombreProveedor.Name = "lblNombre" + name;
                        lblNombreProveedor.Width = 248;
                        lblNombreProveedor.Height = 20;
                        lblNombreProveedor.Location = new Point(3, 32);
                        lblNombreProveedor.TextAlign = ContentAlignment.MiddleCenter;
                        lblNombreProveedor.BackColor = Color.White;

                        int XcbProv = 0;
                        XcbProv = panelContenido.Width / 2;

                        CargarProveedores();

                        ComboBox cbProveedor = new ComboBox();
                        cbProveedor.Name = "cb" + name;
                        cbProveedor.Width = 200;
                        cbProveedor.Height = 30;
                        cbProveedor.Location = new Point(XcbProv - (cbProveedor.Width / 2), 5);
                        cbProveedor.SelectedIndexChanged += new System.EventHandler(comboBoxProveedor_SelectValueChanged);
                        if (listaProveedores.Length > 0)
                        {
                            cbProveedor.DataSource = proveedores.ToArray();
                            cbProveedor.DisplayMember = "Value";
                            cbProveedor.ValueMember = "Key";
                            cbProveedor.SelectedValue = "0";

                            // Cuando se le da click en la opcion editar producto
                            if (DatosSourceFinal == 2)
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
                    }
                } // aqui se continua con los demas else if
                else if (!name.Equals("chkProveedor") && value.Equals("true"))// cualquier otro 
                {
                    nombrePanelContenedor = "panelContenedor" + name.ToString().Remove(0, 3);
                    nombrePanelContenido = "panelContenido" + name.ToString().Remove(0, 3);

                    Panel panelContenedor = new Panel();
                    panelContenedor.Width = 266;
                    panelContenedor.Height = 58;
                    panelContenedor.Name = nombrePanelContenedor;

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
                        panelContenido.Width = 258;
                        panelContenido.Height = 55;

                        Label lblNombreDetalleGral = new Label();
                        lblNombreDetalleGral.Name = "lblNombre" + name;
                        lblNombreDetalleGral.Width = 248;
                        lblNombreDetalleGral.Height = 20;
                        lblNombreDetalleGral.Location = new Point(3, 32);
                        lblNombreDetalleGral.TextAlign = ContentAlignment.MiddleCenter;
                        lblNombreDetalleGral.BackColor = Color.White;

                        int XcbProv = 0;
                        XcbProv = panelContenido.Width / 2;

                        CargarDetallesGral(name.ToString().Remove(0, 3));

                        ComboBox cbDetalleGral = new ComboBox();
                        cbDetalleGral.Name = "cb" + name;
                        cbDetalleGral.Width = 200;
                        cbDetalleGral.Height = 30;
                        cbDetalleGral.Location = new Point(XcbProv - (cbDetalleGral.Width / 2), 5);
                        cbDetalleGral.SelectedIndexChanged += new System.EventHandler(ComboBoxDetalleGral_SelectValueChanged);
                        cbDetalleGral.DropDownStyle = ComboBoxStyle.DropDownList;

                        if (listaDetalleGral.Length > 0)
                        {
                            cbDetalleGral.DataSource = detallesGral.ToArray();
                            cbDetalleGral.DisplayMember = "value";
                            cbDetalleGral.ValueMember = "Key";
                            cbDetalleGral.SelectedValue = "0";
                        }
                        else if (cbDetalleGral.Items.Count == 0)
                        {
                            cbDetalleGral.Items.Add(name.ToString().Remove(0, 3) + "...");
                            cbDetalleGral.SelectedIndex = 0;
                        }
                        panelContenido.Controls.Add(cbDetalleGral);
                        panelContenido.Controls.Add(lblNombreDetalleGral);
                        panelContenedor.Controls.Add(panelContenido);
                        flowLayoutPanel3.Controls.Add(panelContenedor);

                        // Cuando se da click en la opcion editar producto
                        if (DatosSourceFinal == 2)
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
        }

        private void ComboBoxDetalleGral_SelectValueChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            string cadena = string.Empty, namePanel = string.Empty;
            char[] delimiterChars = { '|' };
            int comboBoxIndex = 0;

            comboBoxIndex = comboBox.SelectedIndex;
            namePanel = comboBox.Name.ToString().Remove(0, 2);
            gralDetailGralSelected = Convert.ToString(comboBox.Text);

            if (DatosSourceFinal.Equals(2))
            {
                listaDetalleGral = mb.ObtenerDetallesGral(FormPrincipal.userID, namePanel.Remove(0, 3));
            }

            if (listaDetalleGral.Length > 0)
            {
                idProductoDetalleGral = 0;
                if (comboBoxIndex > 0)
                {
                    cadena = string.Join("", listaDetalleGral[comboBoxIndex - 1]);
                    separadas = cadena.Split(delimiterChars);
                    idProductoDetalleGral = Convert.ToInt32(separadas[0]);
                    nombreDetalleGral = separadas[1];
                }
                else if (comboBoxIndex <= 0)
                {
                    idProductoDetalleGral = 0;
                }

                if (idProductoDetalleGral > 0)
                {
                    cargarDatosGral(Convert.ToInt32(idProductoDetalleGral));
                    llenarDatosGral(namePanel);
                }
            }
        }

        private void llenarDatosGral(string textoBuscado)
        {
            string namePanel = string.Empty;

            namePanel = "panelContenedor" + textoBuscado.Remove(0, 3);

            string nvoConceptoDetalleProducto = string.Empty;

            var idGralDetail = mb.GetDetalleGeneral(FormPrincipal.userID, gralDetailGralSelected);

            foreach (Control contHijo in flowLayoutPanel3.Controls.OfType<Control>())
            {
                if (contHijo.Name == namePanel)
                {
                    foreach (Control contSubHijo in contHijo.Controls.OfType<Control>())
                    {
                        namePanel = "panelContenido" + textoBuscado.Remove(0, 3);
                        if (contSubHijo.Name == namePanel)
                        {
                            foreach (Control contLblHijo in contSubHijo.Controls.OfType<Control>())
                            {
                                if (contLblHijo.Name == "lblNombre" + textoBuscado)
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

            concepto = textBuscado;

            listaDetalleGral = mb.ObtenerDetallesGral(FormPrincipal.userID, concepto);

            if (listaDetalleGral.Length > 0)
            {
                detallesGral.Add("0", concepto + "...");

                foreach (var DetailGral in listaDetalleGral)
                {
                    var auxiliar = DetailGral.Split('|');

                    detallesGral.Add(auxiliar[0], auxiliar[1]);
                }
            }
            else
            {
                detallesGral.Add("0", concepto + "...");
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
                }

                if (idProveedor > 0)
                {
                    cargarDatosProveedor(Convert.ToInt32(idProveedor));
                    llenarDatosProveedor(namePanel);
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
            nombreAddNvoProveedor();
            conceptoAddNvoDetalleGral();
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
                                            intoSubItemPanel.Text = words[5].ToString();
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
            if (txtNombreProducto.Text.Equals(""))
            {
                e.Cancel = true;
                txtNombreProducto.Select(0, txtNombreProducto.Text.Length);
                errorProvAgregarEditarProducto.SetError(txtNombreProducto, "Debe introducir el nombre");
            }
        }

        private void txtNombreProducto_Validated(object sender, EventArgs e)
        {
            errorProvAgregarEditarProducto.SetError(txtNombreProducto, "");
        }

        private void txtPrecioProducto_Validating(object sender, CancelEventArgs e)
        {
            if (txtPrecioProducto.Text.Equals("0") || txtPrecioProducto.Text.Equals("0.00"))
            {
                e.Cancel = true;
                txtPrecioProducto.Select(0, txtPrecioProducto.Text.Length);
                errorProvAgregarEditarProducto.SetError(txtPrecioProducto, "Debe tener un precio");
            }
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

                    if (maximo <= minimo)
                    {
                        MessageBox.Show("El stock máximo no puede ser menor \no igual que stock mínimo", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        txtStockMaximo.Focus();
                    }
                    else if (maximo > minimo)
                    {
                        stockMinimo = minimo.ToString();
                        stockNecesario = maximo.ToString();
                    }
                }
            }
        }

        private void txtPrecioProducto_Validated(object sender, EventArgs e)
        {
            errorProvAgregarEditarProducto.SetError(txtPrecioProducto, "");
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

        public void PrimerCodBarras()
        {
            Contenido = "7777000001";

            cn.EjecutarConsulta($"INSERT INTO CodigoBarrasGenerado (IDUsuario, CodigoBarras) VALUES ('{FormPrincipal.userID}', '{Contenido}')");
        }

        public void AumentarCodBarras()
        {
            string txtBoxName;
            txtBoxName=_lastEnteredControl.Name;
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
            if (SearchProdResult.Rows.Count > 0)
            {
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
                    if (tipoProdServ == "S")
                    {
                        cbTipo.Text = "Servicio";
                    }
                    else if (tipoProdServ == "PQ")
                    {
                        cbTipo.Text = "Paquete";
                    }
                    if (dtProductosDeServicios.Rows.Count != 0)
                    {
                        rowProdServPaq = dtProductosDeServicios.Rows[0];
                        if (dtProductosDeServicios.Rows.Count > 0)
                        {
                            if (rowProdServPaq["NombreProducto"].ToString() != "")
                            {
                                btnAdd.Visible = true;
                                Hided = true;
                                btnAdd.PerformClick();
                                txtCantPaqServ.Text = rowProdServPaq["Cantidad"].ToString();
                            }
                        }
                    }
                }
                else if (tipoProdServ == "P")
                {
                    cbTipo.Text = "Producto";
                    btnAdd.Visible = false;
                }
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
                        var datosTmp = cn.BuscarProducto(Convert.ToInt32(IDProducto), FormPrincipal.userID);

                        //cb.Text = NombreProducto;
                        cb.Text = datosTmp[1];
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
            DataTable dtHistorialCompras;
            DataRow rowHistorialCompras;

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

            txtNombreProducto.Text = ProdNombreFinal;
            txtStockProducto.Text = ProdStockFinal;

            txtPrecioProducto.Text = ProdPrecioFinal;
            txtCategoriaProducto.Text = ProdCategoriaFinal;
            txtClaveProducto.Text = ProdClaveInternaFinal.Trim();

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
            else if (DatosSourceFinal == 4)
            {
                cargarDatosExtra();
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
        }

        /* Fin del codigo de Emmanuel */

        public AgregarEditarProducto(string titulo = "")
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
            //MessageBox.Show("Entro aqui precio | " + precioProducto);
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
                // Abrirmos el OpenFileDialog para buscar y seleccionar la Imagen
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
            catch (Exception ex)
            {
                // si el nombre del archivo esta en blanco
                // si no selecciona un archivo valido o ningun archivo muestra este mensaje
                MessageBox.Show("selecciona una Imagen", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            #region Inicio Seccón de Cambio de Producto a Servicio/Combo ó Servicio/Combo a Producto
            // Condiciones para saber si se realiza el cambio de un producto a servicio y viceversa
            if (cambioProducto)
            {
                validarCambioProducto();
            }
            #endregion Final Seccón de Cambio de Producto a Servicio/Combo ó Servicio/Combo a Producto

            #region Inicio de Variables de Alejandro
            /****************************
	        *	codigo de Alejandro		*
	        ****************************/
            filtroTipoSerPaq = Convert.ToString(cbTipo.SelectedItem);
            tipoServPaq = filtroTipoSerPaq;
            nombre = txtNombreProducto.Text;
            stock = txtStockProducto.Text;
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
            if (DatosSourceFinal == 3 || DatosSourceFinal == 1)
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
                    if (this.Text.Trim() == "Productos")
                    {
                        if (!claveIn.Equals("") || !codigoB.Equals(""))
                        {
                            guardar = new string[] { nombre, stock, precio, categoria, claveIn, codigoB, claveProducto, claveUnidadMedida, tipoDescuento, idUsrNvo, logoTipo, ProdServPaq, baseProducto, ivaProducto, impuestoProducto, mg.RemoverCaracteres(nombre), mg.RemoverPreposiciones(nombre), stockNecesario, stockMinimo, txtPrecioCompra.Text };

                            #region Inicio Se guardan los datos principales del Producto
                            //Se guardan los datos principales del producto
                            try
                            {
                                respuesta = cn.EjecutarConsulta(cs.GuardarProducto(guardar, FormPrincipal.userID));

                                if (respuesta > 0)
                                {
                                    //Se obtiene la ID del último producto agregado
                                    idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));

                                    #region Inicio de Datos de Impuestos
                                    //Se realiza el proceso para guardar los detalles de facturación del producto
                                    if (datosImpuestos != null)
                                    {
                                        guardarDatosImpuestos();
                                    }
                                    #endregion Final de datos de Impuestos

                                    bool isEmpty = !detalleProductoBasico.Any();

                                    #region Inicio Detalles del Producto Basicos (Proveedor)
                                    if (!isEmpty)
                                    {
                                        // Para guardar los detalles del producto
                                        // Ejemplo: Proveedor, Categoria, Ubicacion, etc.
                                        guardarDetallesProductoBasico(detalleProductoBasico);
                                    }
                                    #endregion Final Detalles del Producto Basicos (Proveedor)

                                    isEmpty = !detalleProductoGeneral.Any();

                                    #region Inicio Detalles del Producto Generales (Dinamicos)
                                    if (!isEmpty)
                                    {
                                        guardarDetallesProductoDinamicos(detalleProductoGeneral);
                                    }
                                    #endregion Final Detalles del Producto Generales (Dinamicos)

                                    #region Inicio Agreado de forma manual Boton
                                    if (DatosSourceFinal == 1)
                                    {
                                        #region Inicio para relacionar productos con algun combo/servicio
                                        guardarRelacionDeProductoConComboServicio();
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

                                        string query = $@"INSERT INTO HistorialCompras(Concepto, Cantidad, ValorUnitario, Descuento, Precio, FechaLarga, Folio, RFCEmisor, NomEmisor, ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto, IDUsuario) VALUES('{nombre}','{stock}','{PrecioCompraXMLNvoProd}','{descuentoXML}','{precio}','{fechaCompleta}','{folio.Trim()}','{RFCEmisor.Trim()}','{nombreEmisor.Trim()}','{claveProdEmisor.Trim()}',datetime('now', 'localtime'),'{Inventario.idReporte}','{idProducto}','{FormPrincipal.userID}')";

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
                        else
                        {
                            MessageBox.Show("El Precio Venta, La Clave Interna ó Código de Barras\nNO DEBE ESTAR EN BLANCO", "Advertencia de llenado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    #endregion Final de Sección de Productos
                    #region Inicio de Sección de Combos y Servicios
                    else if (this.Text.Trim() == "Combos" || this.Text.Trim() == "Servicios")
                    {
                        if (!claveIn.Equals("") || !codigoB.Equals(""))
                        {
                            #region Inicio Saber si es Servicio ó Combo
                            string FuenteServPaq = string.Empty;

                            if (this.Text.Trim() == "Servicios")
                            {
                                ProdServPaq = "S";
                                FuenteServPaq = "Servicio";
                            }
                            else if (this.Text.Trim() == "Combos")
                            {
                                ProdServPaq = "PQ";
                                FuenteServPaq = "Combo";
                            }
                            #endregion Final Saber si es Servicio ó Combo

                            stock = "0";

                            guardar = new string[] { nombre, stock, precio, categoria, claveIn, codigoB, claveProducto, claveUnidadMedida, tipoDescuento, FormPrincipal.userID.ToString(), logoTipo, ProdServPaq, baseProducto, ivaProducto, impuestoProducto, mg.RemoverCaracteres(nombre), mg.RemoverPreposiciones(nombre), stockNecesario, "0", txtPrecioCompra.Text };

                            #region Inicio de guardado de los datos principales del Servicios o Combos
                            //Se guardan los datos principales del producto
                            try
                            {
                                respuesta = cn.EjecutarConsulta(cs.GuardarProducto(guardar, FormPrincipal.userID));

                                //Se obtiene la ID del último producto agregado
                                idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));

                                if (respuesta > 0)
                                {
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

                                        /*if (flowLayoutPanel2.Controls.Count == 0)
                                        {
                                            string[] tmp = { $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}", $"{idProducto}", "", "", $"{txtCantPaqServ.Text}" };
                                            cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                                        }*/

                                        // recorrido para FlowLayoutPanel2 para ver cuantos TextBox
                                        if (ProductosDeServicios.Count >= 0)
                                        {
                                            ProductosDeServicios.Clear();

                                            if (flowLayoutPanel2.Controls.Count > 0)
                                            {
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
                                            }
                                        }
                                        flowLayoutPanel2.Controls.Clear();
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

                                        string query = $@"INSERT INTO HistorialCompras(Concepto, Cantidad, ValorUnitario, Descuento, Precio, FechaLarga, Folio, RFCEmisor, NomEmisor, ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto, IDUsuario) VALUES('{nombre}','{stock}','{precio}','{descuentoXML}','{PrecioCompraXMLNvoProd}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}',datetime('now', 'localtime'),'{Inventario.idReporte}','{idProducto}','{FormPrincipal.userID}')";

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
                                #endregion Final Sección Código de Barras Extras

                                //Cierra la ventana donde se agregan los datos del producto
                                this.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al Agregar " + FuenteServPaq + "\n" + origen, "Error Agregar " + FuenteServPaq, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            #endregion Final de guardado de los datos principales del Servicios o Combos
                        }
                        else
                        {
                            MessageBox.Show("El Precio Venta, La Clave Interna ó Código de Barras\nNO DEBE ESTAR EN BLANCO", "Advertencia de llenado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    if (!claveIn.Equals("") || !codigoB.Equals(""))
                    {
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

                        // Comprobar precio del producto para saber si se edito
                        var precioTmp = cn.BuscarProducto(Convert.ToInt32(idProductoBuscado), FormPrincipal.userID);
                        var precioNuevo = float.Parse(precio);
                        var precioAnterior = float.Parse(precioTmp[2]);

                        #region Incio Seccion Cambio de Precio
                        if (precioNuevo != precioAnterior)
                        {
                            var datos = new string[] {
                            FormPrincipal.userID.ToString(), "0", idProductoBuscado,
                            precioAnterior.ToString("N2"), precioNuevo.ToString("N2"),
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

                        queryUpdateProd = $"UPDATE Productos SET Nombre = '{nombre}', Stock = '{stock}', Precio = '{precio}', Categoria = '{categoria}', TipoDescuento = '{tipoDescuento}', ClaveInterna = '{claveIn}', CodigoBarras = '{codigoB}', ClaveProducto = '{claveProducto}', UnidadMedida = '{claveUnidadMedida}', ProdImage = '{logoTipo}', NombreAlterno1 = '{mg.RemoverCaracteres(nombre)}', NombreAlterno2 = '{mg.RemoverPreposiciones(nombre)}', StockNecesario = '{stockNecesario}', StockMinimo = '{stockMinimo}' WHERE ID = '{idProductoBuscado}' AND IDUsuario = {FormPrincipal.userID}";

                        respuesta = cn.EjecutarConsulta(queryUpdateProd);

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
                        if (this.Text.Trim().Equals("Productos"))
                        {
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

                                    string DeleteProdAtService = $"DELETE FROM ProductosDeServicios WHERE IDServicio = '{CBIdProd}' AND (IDProducto = '' AND NombreProducto = '')";
                                    int DeleteProdAtPQS = cn.EjecutarConsulta(DeleteProdAtService);
                                    if (DeleteProdAtPQS > 0)
                                    {
                                        //MessageBox.Show("Productos Agregado al Paquete o Servicio", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        //MessageBox.Show("Algo salio mal al intentar Agregar el\nProducto al Paquete o Servicio", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }

                                // Limpiar para evitar error de relacionar producto a servicio 
                                // y despues editar cualquier producto cualquiera
                                CBIdProd = string.Empty;
                                CBNombProd = string.Empty;
                            }
                        }
                        #endregion Final de Seccion Productos

                        #region Inicio de Seccion Combos y Servicios
                        else if (this.Text.Trim().Equals("Combos") || this.Text.Trim().Equals("Servicios"))
                        {
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
                            flowLayoutPanel2.Controls.Clear();
                        }
                        #endregion Final de Seccion Combos y Servicios

                        // Cierra la ventana donde se agregan los datos del producto
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El Precio Venta, La Clave Interna ó Código de Barras\nNO DEBE ESTAR EN BLANCO", "Advertencia de llenado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    
                    guardar = new string[] { nombreNvoInsert, stockNvoInsert, precioNvoInsert, categoriaNvoInsert, claveInNvoInsert, codigoBNvoInsert, claveProducto, claveUnidadMedida, tipoDescuentoNvoInsert, idUsrNvoInsert, logoTipo, tipoProdNvoInsert, baseProducto, ivaProducto, impuestoProducto, mg.RemoverCaracteres(nombreNvoInsert), mg.RemoverPreposiciones(nombreNvoInsert), stockNecesario, stockMinimo, txtPrecioCompra.Text };

                    #region Inicio de Guardado de Producto
                    try
                    {
                        //Se guardan los datos principales del producto
                        int respuesta = cn.EjecutarConsulta(cs.GuardarProducto(guardar, FormPrincipal.userID));

                        if (respuesta > 0)
                        {
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

            guardar = new string[] { nombre, stock, precio, txtPrecioCompra.Text, fechaCompra, rfcProveedor, conceptoProveedor, "", "1", fechaOperacion, "", idProducto.ToString(), FormPrincipal.userID.ToString() };

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

        private void guardarDetallesProductoBasico(List<string> detalleProductoBasico)
        {
            guardar = detalleProductoBasico.ToArray();
            guardar[0] = idProducto.ToString();
            cn.EjecutarConsulta(cs.GuardarProveedorDetallesDelProducto(guardar));
        }

        private void guardarDatosImpuestos()
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
                    try
                    {
                        cn.EjecutarConsulta(cs.GuardarDetallesProducto(guardar, idProducto));
                    }
                    catch(Exception ex)
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
            string query = $"SELECT P.Nombre, P.ClaveInterna, P.CodigoBarras, P.Tipo, P.Status FROM Productos AS P WHERE P.IDUsuario = {FormPrincipal.userID} AND P.Status = 1 AND P.ClaveInterna = {claveIn}";
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

                    query = $"SELECT CB.IDProducto FROM CodigoBarrasExtras CB INNER JOIN Productos P ON P.ID = CB.IDProducto WHERE P.IDUsuario = {FormPrincipal.userID} AND CB.CodigoBarraExtra = {claveIn}";

                    // Cargar procuto registrado con esa Código de Barras Extra
                    productoRegistradoCodigoBarrasExtra(FormPrincipal.userID, claveIn);
                }
            }
        }

        private void productoRegistradoCodigoBarrasExtra(int userID, string claveBuscar)
        {
            datosProductosBtnGuardar = new List<string>();
            datosProductoRelacionado = new List<string>();

            string query = $"SELECT CB.IDProducto FROM CodigoBarrasExtras CB INNER JOIN Productos P ON P.ID = CB.IDProducto WHERE P.IDUsuario = {userID} AND CB.CodigoBarraExtra = {claveBuscar}";

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
            filtro = Convert.ToString(cbTipo.SelectedItem);      // tomamos el valor que se elige en el TextBox
            if (filtro == "Producto")                            // comparamos si el valor a filtrar es Producto
            {
                if (DatosSourceFinal == 1 || DatosSourceFinal == 3)
                {
                    Titulo = "Agregar Producto";
                    cadAux = TituloForm.Substring(8);   // extraemos que tipo es (Producto)
                }
                else if (DatosSourceFinal == 2)
                {
                    Titulo = "Editar Producto";
                    cadAux = TituloForm.Substring(7);   // extraemos que tipo es (Producto)
                }
                else if (DatosSourceFinal == 4)
                {
                    Titulo = "Copiar Producto";
                    cadAux = TituloForm.Substring(7);   // extraemos que tipo es (Producto)
                }
                TituloForm = Titulo;
                tituloSeccion.Text = TituloForm;
                this.Text = cadAux + "s";
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
                    txtCategoriaProducto.Text = "Productos";
                }
            }
            else if (filtro == "Combo")                    // comparamos si el valor a filtrar es Servicio / Paquete ó Combo
            {
                Titulo = "Agregar Combo";
                TituloForm = Titulo;
                cadAux = TituloForm.Substring(7);   // extraemos que tipo es (Paquete)
                tituloSeccion.Text = TituloForm;
                this.Text = cadAux + "s";
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
                    lblTipoProdPaq.Text = "Combo";
                    btnAdd.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\angle-double-down.png");
                    Hided = false;
                    btnAdd.Visible = true;
                    btnAdd.PerformClick();
                    chkBoxConProductos.Checked = false;
                    chkBoxConProductos.Visible = true;
                    txtCategoriaProducto.Text = "Combos";
                }
            }
            else if (filtro == "Servicio")                    // comparamos si el valor a filtrar es Servicio / Paquete ó Combo
            {
                Titulo = "Agregar Servicio";
                TituloForm = Titulo;
                cadAux = TituloForm.Substring(7);   // extraemos que tipo es (Servicio)
                tituloSeccion.Text = TituloForm;
                this.Text = cadAux + "s";
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
                    lblTipoProdPaq.Text = "Servicio";
                    btnAdd.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\angle-double-down.png");
                    Hided = false;
                    btnAdd.Visible = true;
                    btnAdd.PerformClick();
                    chkBoxConProductos.Checked = false;
                    chkBoxConProductos.Visible = true;
                    txtCategoriaProducto.Text = "Servicios";
                }
            }
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
            LoadPanelDatos();
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

            string  queryProdSearch, 
                    queryProdServFound, 
                    queryProdUpdate, 
                    queryProdServUpdate, 
                    fech, 
                    prodSerPaq, 
                    buscar, 
                    comboBoxText, 
                    comboBoxValue;

            DataTable   dtProdFound, 
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

            if (DatosSourceFinal == 1 || DatosSourceFinal == 3)
            {
                //Verifica que el formulario ya tenga una instancia creada, de lo contrario la crea
                if (FormDetalleProducto == null)
                {
                    FormDetalleProducto = new AgregarDetalleProducto();
                    //FormDetalleProducto.typeDatoProveedor = 1;
                    FormDetalleProducto.origenProdServCombo = DatosSourceFinal;
                    FormDetalleProducto.getIdProducto = idProductoFinal;
                    FormDetalleProducto.Show();
                    FormDetalleProducto.BringToFront();
                }
                else
                {
                    //FormDetalleProducto.typeDatoProveedor = 1;
                    FormDetalleProducto.origenProdServCombo = DatosSourceFinal;
                    FormDetalleProducto.getIdProducto = idProductoFinal;
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
            if (e.KeyCode == Keys.Enter)
            {
                precioOriginalConIVA = (float)Convert.ToDouble(txtPrecioCompra.Text);
                PrecioRecomendado = precioOriginalConIVA * porcentajeGanancia;
                txtPrecioProducto.Text = PrecioRecomendado.ToString("N2");
                txtPrecioProducto.Focus();
                txtPrecioProducto.Select(txtPrecioProducto.Text.Length, 0);
            }
        }

        private void txtPrecioCompra_Leave(object sender, EventArgs e)
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
                        words[1] = "0";
                    }
                    txtPrecioCompra.Text = words[0] + "." + words[1];
                }
            }
            precioOriginalConIVA = (float)Convert.ToDouble(txtPrecioCompra.Text);
            PrecioRecomendado = precioOriginalConIVA * porcentajeGanancia;
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
            if (this.Text.Trim() == "Productos")
            {
                ListStock.TypeStock = "Combos";
            }
            else if (this.Text.Trim() == "Combos" || this.Text.Trim() == "Servicios")
            {
                ListStock.TypeStock = "Productos";
            }
            ListStock.ShowDialog();
        }

        private void ejecutar(string nombProd_Paq_Serv, string id_Prod_Paq_Serv)
        {
            CBNombProd = nombProd_Paq_Serv;
            CBIdProd = id_Prod_Paq_Serv;
            int numero = 0;
            if (!int.TryParse(CBNombProd, out numero))
            {
                if ((this.Text.Trim() == "Combos" || this.Text.Trim() == "Servicios") && DatosSourceFinal == 1)
                {
                    btnAdd.Visible = true;
                    CargarDatos();
                    if (flowLayoutPanel2.Visible == true)
                    {
                        Hided = true;
                        ocultarPanel();
                    }
                    GenerarPanelProductosServPlus();
                }
                else if ((this.Text.Trim() == "Combos" || this.Text.Trim() == "Servicios") && DatosSourceFinal == 2)
                {
                    string[] tmp = { $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}", $"{idEditarProducto}", $"{CBIdProd}", $"{CBNombProd}", $"{txtCantPaqServ.Text}" };
                    cn.EjecutarConsulta(cs.GuardarProductosServPaq(tmp));
                    string queryDeleteProductosServPaq = $"DELETE FROM ProductosDeServicios WHERE IDServicio = '{idEditarProducto}' AND NombreProducto = ''";
                    cn.EjecutarConsulta(queryDeleteProductosServPaq);
                    btnAdd.Visible = true;
                    CargarDatos();
                    if (flowLayoutPanel2.Visible == true)
                    {
                        Hided = true;
                        ocultarPanel();
                    }
                    GenerarPanelProductosServPlus();
                }
            }
            else if (this.Text.Trim() == "Productos" && DatosSourceFinal == 1)
            {
                
            }

            // Limpiar para evitar error de relacionar producto a servicio 
            // y despues editar cualquier producto cualquiera
            CBIdProd = string.Empty;
            CBNombProd = string.Empty;
        }

        private void AgregarEditarProducto_Paint(object sender, PaintEventArgs e)
        {
            if (ejecutarMetodos)
            {
                if (this.Text.Trim() == "Combos" || this.Text.Trim() == "Servicios")
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

        private void timerProdPaqSer_Tick(object sender, EventArgs e)
        {
            if (Hided)  // si es valor true
            {
                int NewHeight = fLPContenidoProducto.Height + 123;
                if (fLPContenidoProducto.Height == 0)
                {
                    fLPContenidoProducto.Height = NewHeight;
                }
                if (fLPContenidoProducto.Height >= PH)
                {
                    timerProdPaqSer.Stop();
                    Hided = false;
                    if (idProductoBuscado != null && tipoProdServ == "S")
                    {
                        if (flowLayoutPanel2.Controls.Count == 0)
                        {
                            mostrarProdServPaq();
                        }
                        else if (flowLayoutPanel2.Controls.Count > 0)
                        {

                        }
                    }
                    else if (idProductoBuscado != null && tipoProdServ == "PQ")
                    {
                        if (flowLayoutPanel2.Controls.Count == 0)
                        {
                            mostrarProdServPaq();
                        }
                        else if (flowLayoutPanel2.Controls.Count > 0)
                        {

                        }
                    }
                    else if (idProductoBuscado != null && dtNvoProductosDeServicios != null)
                    {
                        mostrarProdServPaq();
                    }
                    else if ((idProductoBuscado != null || tipoProdServ != null) && DatosSourceFinal == 3)
                    {
                        if (flowLayoutPanel2.Controls.Count == 0)
                        {
                            GenerarPanelProductosServ();
                        }
                        else if (flowLayoutPanel2.Controls.Count > 0)
                        {

                        }
                    }
                    else if ((idProductoBuscado == null || tipoProdServ == null) && DatosSourceFinal == 3)
                    {
                        if (flowLayoutPanel2.Controls.Count == 0)
                        {
                            GenerarPanelProductosServ();
                        }
                        else if (flowLayoutPanel2.Controls.Count > 0)
                        {

                        }
                    }
                    else if ((idProductoBuscado != null || tipoProdServ != null) && DatosSourceFinal == 1)
                    {
                        if (flowLayoutPanel2.Controls.Count == 0)
                        {
                            GenerarPanelProductosServ();
                        }
                        else if (flowLayoutPanel2.Controls.Count > 0)
                        {

                        }
                    }
                    else if ((idProductoBuscado == null || tipoProdServ == null) && DatosSourceFinal == 1)
                    {
                        if (flowLayoutPanel2.Controls.Count == 0)
                        {
                            GenerarPanelProductosServ();
                        }
                        else if (flowLayoutPanel2.Controls.Count > 0)
                        {

                        }
                    }
                    this.Height = 725;
                    this.CenterToScreen();
                    this.Refresh();
                }
                else
                {
                    NewHeight = PH;
                    fLPContenidoProducto.Height = NewHeight;
                }
            }
            else    // si es false
            {
                fLPContenidoProducto.Height = fLPContenidoProducto.Height - 30;
                if (fLPContenidoProducto.Height <= 0)
                {
                    timerProdPaqSer.Stop();
                    Hided = true;
                    this.Height = 630;
                }
                this.CenterToScreen();
                this.Refresh();
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
            txtStockProducto.SelectAll();
            _lastEnteredControl = (Control)sender;      // capturamos el ultimo control en el que estaba el Focus
        }

        private void txtPrecioProducto_Enter(object sender, EventArgs e)
        {
            txtPrecioProducto.SelectAll();
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
            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                saveDirectoryImg = $@"\\{servidor}\PUDVE\Productos\";
            }
            else
            {
                saveDirectoryImg = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Productos\";
            }

            detalleProductoBasico.Clear();
            seleccionListaStock = 0;
            string cadAux = string.Empty;
            fLPType.Visible = false;
            TituloForm = Titulo;

            PH = fLPContenidoProducto.Height;
            Hided = false;
            Hided1 = false;
            flowLayoutPanel2.Controls.Clear();
            DatosSourceFinal = DatosSource;

            PCategoria.Visible = false;
            fLPDetallesProducto.Visible = true;

            flowLayoutPanel3.VerticalScroll.Visible = true;

            actualizarDetallesProducto();

            mostrarOcultarfLPDetallesProducto();

            if (DatosSourceFinal == 3)      // si el llamado de la ventana proviene del Archivo XML
            {
                cbTipo.SelectedIndex = 0;
                PCantidadPaqServ.Visible = false;
                fLPType.Visible = true;
                cadAux = TituloForm.Substring(8);   // extraemos que tipo es (Producto, Paquete, Servicio)
                cargarDatos();
                txtPrecioCompra.Enabled = true;
            }
            else if (DatosSourceFinal == 2)      // si el llamado de la ventana proviene del DataGridView (Ventana Productos)
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
                    if (pictureBoxProducto.Image != null)
                    {
                        pictureBoxProducto.Image.Dispose(); // Liberamos el pictureBox para poder borrar su imagen
                        if (!logoTipo.Equals(""))
                        {
                            // leemos el archivo de imagen y lo ponemos el pictureBox
                            using (File = new FileStream(saveDirectoryImg + logoTipo, FileMode.Open, FileAccess.Read))
                            {
                                pictureBoxProducto.Image = Image.FromStream(File);      // carrgamos la imagen en el PictureBox
                            }
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
                                }
                            }
                        }
                    }
                }
            }
            else if (DatosSourceFinal == 1)      // si el llamado de la ventana proviene del Boton Productos (Ventana Productos)
            {
                txtStockProducto.Enabled = true;
                cadAux = TituloForm.Substring(8);   // extraemos que tipo es (Producto, Paquete, Servicio)
                PCantidadPaqServ.Visible = true;
                button1.Visible = true;
                txtPrecioCompra.Enabled = true;
            }

            if (cadAux == "Producto")           // si es un Producto
            {
                if (!ProdNombre.Equals(""))
                {
                    cargarDatos();
                    ocultarPanel();
                    //cargarCBProductos();
                    PStock.Visible = true;
                    txtCantPaqServ.Visible = false;
                    lblCantPaqServ.Text = "Relacionar con \nPaquete/Servicio";
                    button1.Text = "Combo/Servicio";
                }
                else if (ProdNombre.Equals(""))
                {
                    LimpiarCampos();
                    cargarDatosNvoProd();
                    cbTipo.Text = "Producto";
                    btnAdd.Visible = false;
                    ocultarPanel();
                    PStock.Visible = true;
                    txtCantPaqServ.Visible = false;
                    lblCantPaqServ.Text = "Relacionar con \nCombo/Servicio";
                    button1.Text = "Combo/Servicio";
                }
                this.Text = cadAux + "s";             // Ponemos el titulo del form en plural "Productos"
                typeOfProduct = "P";
                lblTipoProdPaq.Text = "Nombre del Producto";
                txtCategoriaProducto.Text = cadAux + "s";
                if (DatosSourceFinal == 1 || DatosSourceFinal == 3)
                {
                    tituloSeccion.Text = "Agregar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                }
                else if (DatosSourceFinal == 2)
                {
                    tituloSeccion.Text = "Editar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                }
                else if (DatosSourceFinal == 4)
                {
                    tituloSeccion.Text = "Copiar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                }
            }
            else if (cadAux == "Combo")       // si es un Paquete
            {
                if (!ProdNombre.Equals(""))
                {
                    cargarDatos();
                    ocultarPanel();
                    cargarCBProductos(idEditarProducto);
                    PStock.Visible = false;
                    txtCantPaqServ.Visible = true;
                    lblCantPaqServ.Text = "Cantidad por combo";
                    button1.Text = "Productos";
                }
                else if (ProdNombre.Equals(""))
                {
                    LimpiarCampos();
                    cargarDatosNvoProd();
                    cbTipo.Text = "Producto";
                    btnAdd.Visible = false;
                    ocultarPanel();
                    PStock.Visible = false;
                    txtCantPaqServ.Visible = true;
                    lblCantPaqServ.Text = "Cantidad por combo";
                    button1.Text = "Productos";
                }
                this.Text = cadAux + "s";            // Ponemos el titulo del form en plural "Paquetes"
                typeOfProduct = "PQ";
                lblTipoProdPaq.Text = "Nombre del Combo";
                txtCategoriaProducto.Text = cadAux + "s";
                if (DatosSourceFinal == 1 || DatosSourceFinal == 3)
                {
                    tituloSeccion.Text = "Agregar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                }
                else if (DatosSourceFinal == 2)
                {
                    tituloSeccion.Text = "Editar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                }
                else if (DatosSourceFinal == 4)
                {
                    tituloSeccion.Text = "Copiar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                }
            }
            else if (cadAux == "Servicio")      // si es un Servicio
            {
                if (!ProdNombre.Equals(""))
                {
                    cargarDatos();
                    ocultarPanel();
                    cargarCBProductos(idEditarProducto);
                    PStock.Visible = false;
                    txtCantPaqServ.Visible = true;
                    lblCantPaqServ.Text = "Cantidad por servicio";
                    button1.Text = "Productos";
                }
                else if (ProdNombre.Equals(""))
                {
                    LimpiarCampos();
                    cargarDatosNvoProd();
                    cbTipo.Text = "Producto";
                    btnAdd.Visible = false;
                    ocultarPanel();
                    PStock.Visible = false;
                    txtCantPaqServ.Visible = true;
                    lblCantPaqServ.Text = "Cantidad por servicio";
                    button1.Text = "Productos";
                }
                this.Text = cadAux + "s";            // Ponemos el titulo del form en plural "Servicios"
                typeOfProduct = "S";
                lblTipoProdPaq.Text = "Nombre del Servicio";
                txtCategoriaProducto.Text = cadAux + "s";
                if (DatosSourceFinal == 1 || DatosSourceFinal == 3)
                {
                    tituloSeccion.Text = "Agregar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                }
                else if (DatosSourceFinal == 2)
                {
                    tituloSeccion.Text = "Editar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                }
                else if (DatosSourceFinal == 4)
                {
                    tituloSeccion.Text = "Copiar " + cadAux + "s";    // Ponemos el Text del label TituloSeccion
                }
            }

            var config = mb.ComprobarConfiguracion();

            if (config.Count > 0)
            {
                porcentajeGanancia = float.Parse(config[8].ToString());
            }
        }

        private void mostrarOcultarfLPDetallesProducto()
        {
            if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
            {
                fLPDetallesProducto.Visible = false;
                this.Height = 660;
            }
            else if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
            {
                fLPDetallesProducto.Visible = true;
                this.Height = 731;
            }
        }

        public void actualizarDetallesProducto()
        {
            //loadFormConfig();
            loadFromConfigDB();
            BuscarChkBoxListView(chkDatabase);
            bool isEmpty = !detalleProductoBasico.Any();
            if (!isEmpty)
            {
                // Cuando se da click en la opcion editar producto
                if (DatosSourceFinal == 1)
                {
                    string  Descripcion = string.Empty,
                            name = string.Empty,
                            value = string.Empty,
                            namegral = string.Empty;

                    for (int i = 0; i < chkDatabase.Items.Count; i++)
                    {
                        name = chkDatabase.Items[i].Text.ToString();
                        value = chkDatabase.Items[i].SubItems[1].Text.ToString();
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
                                            contItemSubHijo.Text = detalleProductoBasico[2].ToString();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    for (int i = 0; i < chkDatabase.Items.Count; i++)
                    {
                        name = chkDatabase.Items[i].Text.ToString().Remove(0, 3);
                        value = chkDatabase.Items[i].SubItems[1].Text.ToString();
                        foreach (Control contHijo in flowLayoutPanel3.Controls)
                        {
                            foreach (Control contSubHijo in contHijo.Controls)
                            {
                                if (contSubHijo.Name.Equals("panelContenido" + name) && value.Equals("true"))
                                {
                                    for (int j = 0; j < detalleProductoGeneral.Count; j++)
                                    {
                                        namegral = detalleProductoGeneral[j].ToString();
                                        if (namegral.Equals(name) &&
                                            contSubHijo.Name.Equals("panelContenido" + name))
                                        {
                                            foreach (Control contItemSubHijo in contSubHijo.Controls)
                                            {
                                                if (contItemSubHijo is Label)
                                                {
                                                    contItemSubHijo.Text = detalleProductoGeneral[j + 2].ToString();
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

        private void loadFromConfigDB()
        {
            var servidor = Properties.Settings.Default.Hosting;

            if (string.IsNullOrWhiteSpace(servidor))
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
                    MessageBox.Show("Error de lectura de los Datos Dinamicos: {0}" + ex.Message.ToString(), "Error de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
