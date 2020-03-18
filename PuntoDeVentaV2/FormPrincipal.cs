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
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.Xml;
using System.Collections.Specialized;
using System.Configuration;

namespace PuntoDeVentaV2
{
    public partial class FormPrincipal : Form
    {
        Conexion cn = new Conexion();
        MetodosGenerales mg = new MetodosGenerales();
        MetodosBusquedas mb = new MetodosBusquedas();
        Consultas cs = new Consultas();

        public static string[] datosUsuario = new string[] { };
        private bool cerrarAplicacion = false;

        // declaramos la variable que se pasara entre los dos formularios
        // FormPrincipal y MisDatos
        public static int userID;
        public static string userNickName;
        public static string userPass;
        public static int TempUserID;
        public static string TempUserNickName;
        public static string TempUserPass;
        public static string FechaCheckStock;
        public static long NoRegCheckStock;
        public static int id_empleado;

        // Variables donde guarda los permisos del empleado.

        private int anticipos = 1;
        private int caja = 1;
        private int clientes = 1;
        private int config = 1;
        private int empleados = 1;
        private int empresas = 1;
        private int facturas = 1;
        private int inventarios = 1;
        private int misdatos = 1;
        private int productos = 1;
        private int proveedores = 1;
        private int reportes = 1;
        private int servicios = 1;
        private int ventas = 1;

        // variables para poder tomar los datos que se pasaron del login a esta forma
        public int IdUsuario { get; set; }
        public string nickUsuario { get; set; }
        public string passwordUsuario { get; set; }
        public string DateCheckStock { get; set; }
        public long NumberRegCheckStock { get; set; }
        public int t_id_empleado { get; set; }

        // variables usasadas para que sea estatico los valores y asi en empresas
        // se agrege tambien la cuenta principal y poder hacer que regresemos a ella
        public int TempIdUsuario { get; set; }
        public string TempNickUsr { get; set; }
        public string TempPassUsr { get; set; }

        const string ficheroNumCheck = @"\PUDVE\settings\noCheckStock\checkStock.txt";  // directorio donde esta el archivo de numero 
        const string ficheroDateCheck = @"\PUDVE\settings\noCheckStock\checkDateStock.txt";  // directorio donde esta el archivo de fecha
        string Contenido;                                                       // para obtener el numero que tiene el codigo de barras en el arhivo

        string FechaFinal, saveDirectoryFile = string.Empty;

        #region Variables Globales	

        List<string> infoDetalle, infoDetailProdGral;

        Dictionary<string, string> proveedoresDictionary, categorias, ubicaciones, detallesGral;

        Dictionary<int, Tuple<string, string, string, string>> diccionarioDetallesGeneral = new Dictionary<int, Tuple<string, string, string, string>>(), diccionarioDetalleBasicos = new Dictionary<int, Tuple<string, string, string, string>>();

        DataTable dtProdMessg;
        DataRow drProdMessg;

        string[] datosProveedor, datosCategoria, datosUbicacion, datosDetalleGral, separadas, guardar, datosAppSettingToDB;

        string[] listaProveedores = new string[] { }, listaCategorias = new string[] { }, listaUbicaciones = new string[] { }, listaDetalleGral = new string[] { };

        int XPos = 0, YPos = 0, contadorIndex = 0, idProveedor = 0, idCategoria = 0, idUbicacion = 0, idProductoDetalleGral;

        string nvoDetalle = string.Empty, nvoValor = string.Empty, editValor = string.Empty, deleteDetalle = string.Empty, nombreProveedor = string.Empty, nombreCategoria = string.Empty, nombreUbicacion = string.Empty, mensajeDetalleProducto = string.Empty;

        public string getIdProducto { get; set; }

        public static string finalIdProducto = string.Empty;

        string editDetelle = string.Empty, editDetalleNvo = string.Empty;

        List<string> datosAppSettings;

        #endregion Variables Globales	

        #region Modifying Configuration Settings at Runtime	
        XmlDocument xmlDoc = new XmlDocument();
        XmlNode appSettingsNode, newChild;
        ListView chkDatabase = new ListView();  // ListView para los CheckBox de solo detalle	
        ListView settingDatabases = new ListView(); // ListView para los CheckBox de Sistema	
        ListViewItem lvi;
        string connStr, keyName;
        int found = 0;
        NameValueCollection appSettings;

        // this code will add a listviewtem	
        // to a listview for each database entry	
        // in the appSettings section of an App.config file.	
        private void loadFormConfig()
        {
            string datosAppSetting = string.Empty;

            if (Properties.Settings.Default.TipoEjecucion == 1)
            {
                xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            }

            if (Properties.Settings.Default.TipoEjecucion == 2)
            {
                xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            }

            appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

            try
            {
                appSettings = ConfigurationManager.AppSettings;
                datosAppSettings = new List<string>();

                if (appSettings.Count == 0)
                {
                    MessageBox.Show("Lectura de la Sección de AppSettings está vacia", "Archivo Vacio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (appSettings.Count > 0)
                {
                    for (int i = 0; i < appSettings.Count; i++)
                    {
                        connStr = appSettings[i];
                        keyName = appSettings.GetKey(i);
                        found = keyName.IndexOf("chk", 0, 3);
                        if (found >= 0)
                        {
                            datosAppSetting += connStr + "|" + keyName + "|" + userID.ToString() + "¬";
                        }
                        if (found <= -1)
                        {
                            datosAppSetting += connStr + "|" + keyName + "|";
                        }
                    }
                    int borrar = 0;
                    string deleteData = string.Empty;
                    deleteData = $"DELETE FROM appSettings WHERE IDUsuario = {userID.ToString()}";
                    borrar = cn.EjecutarConsulta(deleteData);
                    string auxAppSetting = string.Empty;
                    string[] str;
                    int insertar = 0;
                    auxAppSetting = datosAppSetting.TrimEnd('¬').TrimEnd();
                    str = auxAppSetting.Split('¬');
                    datosAppSettings.AddRange(str);
                    foreach (var item in datosAppSettings)
                    {
                        datosAppSettingToDB = item.Split('|');
                        for (int i = 0; i < datosAppSettingToDB.Length; i++)
                        {
                            if (datosAppSettingToDB[i].Equals("true"))
                            {
                                datosAppSettingToDB[i] = "1";
                            }
                            else if (datosAppSettingToDB[i].Equals("false"))
                            {
                                datosAppSettingToDB[i] = "0";
                            }
                        }
                        insertar = cn.EjecutarConsulta(cs.GuardarAppSettings(datosAppSettingToDB));
                    }
                }
            }
            catch (ConfigurationException e)
            {
                MessageBox.Show("Lectura App.Config/AppSettings: {0}" + e.Message.ToString(), "Error de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion Modifying Configuration Settings at Runtime

        // funcion para que podamos recargar variables desde otro formulario
        public void recargarDatos()
        {
            userID = IdUsuario; Console.WriteLine("userID" + userID);
            userNickName = nickUsuario; Console.WriteLine("userNickName" + userNickName);
            userPass = passwordUsuario;
            FechaCheckStock = DateCheckStock;
            NoRegCheckStock = NumberRegCheckStock;
            id_empleado = t_id_empleado;
        }

        public FormPrincipal()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            CargarSaldoInicial();
            //Envio de datos de Caja con el Timer
            ConvertirMinutos();

            //Se crea el directorio principal para almacenar todos los archivos generados y carpetas
            Directory.CreateDirectory(@"C:\Archivos PUDVE");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\Reportes");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\Reportes\Historial");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\Reportes\Caja");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\MisDatos");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\MisDatos\Usuarios");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\MisDatos\CFDI");

            obtenerDatosCheckStock();

            recargarDatos();

            TempUserID = TempIdUsuario;
            TempUserNickName = TempNickUsr;
            TempUserPass = TempPassUsr;

            ObtenerDatosUsuario(userID);

            //var servidor = Properties.Settings.Default.Hosting;

            //if (string.IsNullOrWhiteSpace(servidor))
            //{
            //    loadFormConfig();
            //}

            this.Text = "PUDVE - Punto de Venta | " + userNickName;

            // Obtiene ID del empleado, y los permisos que tenga asignados.

            string formato_usuario = "^[A-Z&Ñ]+@[A-Z&Ñ0-9]+$";

            Regex exp = new Regex(formato_usuario);

            if (exp.IsMatch(userNickName)) // Es un empleado
            {
                int id_empleado = obtener_id_empleado(userID);

                var datos_per = mb.obtener_permisos_empleado(id_empleado, userID);

                permisos_empleado(datos_per);
            }

            InitializarTimerAndroid();

            // Verificar si existe registro de la tabla configuracion
            var existe = (bool)cn.EjecutarSelect($"SELECT * FROM Configuracion WHERE IDUsuario = {userID}");

            if (!existe)
            {
                cn.EjecutarConsulta($"INSERT INTO Configuracion (IDUsuario) VALUES ('{userID}')");
            }
        }

        private void CargarSaldoInicial()
        {
            saldoInicial = mb.SaldoInicialCaja(userID);
        }

        public void ConvertirMinutos()
        {
            if (!Properties.Settings.Default.tiempoTimerAndroid.Equals(0))
            {
                actualizarCaja.Interval = Properties.Settings.Default.tiempoTimerAndroid;
                actualizarCaja.Tick += new EventHandler(controlar_Tick);
                actualizarCaja.Enabled = true;
            }

        }

        private void controlar_Tick(object sender, EventArgs e)
        {
            //  MessageBox.Show("Mensaje de prueba");
        }

        private void obtenerDatosCheckStock()
        {
            // Leer fichero que tiene el numero de CheckInventario

            using (StreamReader readfile = new StreamReader(Properties.Settings.Default.rutaDirectorio + ficheroNumCheck))
            {
                Contenido = readfile.ReadToEnd();   // se lee todo el archivo y se almacena en la variable Contenido
            }
            if (Contenido != "")   // si el contenido no es vacio
            {
                NumberRegCheckStock = (long)Convert.ToDouble(Contenido);
            }

            // Leer fichero que tiene la fecha de Inventario
            using (StreamReader readfile = new StreamReader(Properties.Settings.Default.rutaDirectorio + ficheroDateCheck))
            {
                Contenido = readfile.ReadToEnd();   // se lee todo el archivo y se almacena en la variable Contenido
            }
            if (Contenido != "")   // si el contenido no es vacio
            {
                FechaFinal = Contenido.Replace("\r\n", "");
                DateCheckStock = FechaFinal;
            }
        }

        private void ObtenerDatosUsuario(int IDUsuario)
        {
            datosUsuario = cn.DatosUsuario(IDUsuario: IDUsuario, tipo: 0);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            if (productos == 1)
            {
                AbrirFormulario<Productos>();

                Productos.recargarDatos = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            if (ventas == 1)
            {
                AbrirFormulario<ListadoVentas>();

                ListadoVentas.recargarDatos = true;
                ListadoVentas.abrirNuevaVenta = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            if (clientes == 1)
            {
                AbrirFormulario<Clientes>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            if (proveedores == 1)
            {
                AbrirFormulario<Proveedores>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //Metodo para abrir formularios dentro del panel
        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;

            formulario = panelContenedor.Controls.OfType<MiForm>().FirstOrDefault(); //Busca en la coleccion el formulario

            //Si el formulario/instancia no existe
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panelContenedor.Controls.Add(formulario);
                panelContenedor.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
            }
            else
            {
                formulario.BringToFront();
            }
        }


        private void btnMisDatos_Click(object sender, EventArgs e)
        {
            if (misdatos == 1)
            {
                AbrirFormulario<MisDatos>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEmpresas_Click(object sender, EventArgs e)
        {
            if (empresas == 1)
            {
                AbrirFormulario<Empresas>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnAnticipos_Click(object sender, EventArgs e)
        {
            if (anticipos == 1)
            {
                AbrirFormulario<Anticipos>();

                Anticipos.recargarDatos = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (config == 1)
            {
                AbrirFormulario<SetUpPUDVE>();

                SetUpPUDVE.recargarDatos = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnCaja_Click(object sender, EventArgs e)
        {
            if (caja == 1)
            {
                AbrirFormulario<CajaN>();

                CajaN.recargarDatos = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            if (inventarios == 1)
            {
                AbrirFormulario<Inventario>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            if (empleados == 1)
            {
                AbrirFormulario<Empleados>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (reportes == 1)
            {
                AbrirFormulario<Reportes>();
            }
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            if(facturas == 1)
            {
                AbrirFormulario<Facturas>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void temporizador_respaldo_Tick(object sender, EventArgs e)
        {
            //Por el momento en comentarios, no eliminarlo
            //Genera_respaldos.respaldar();
        }


        private int obtener_id_empleado(int id)
        {
            int id_e = 0;

            id_e = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Empleados WHERE IDUsuario='{id}' AND usuario='{userNickName}'", 5));

            return id_e;
        }

        private void permisos_empleado(string[] datos_e)
        {
            anticipos = Convert.ToInt32(datos_e[0]);
            caja = Convert.ToInt32(datos_e[1]);
            clientes = Convert.ToInt32(datos_e[2]);
            config = Convert.ToInt32(datos_e[3]);
            empleados = Convert.ToInt32(datos_e[4]);
            empresas = Convert.ToInt32(datos_e[5]);
            facturas = Convert.ToInt32(datos_e[6]);
            inventarios = Convert.ToInt32(datos_e[7]);
            misdatos = Convert.ToInt32(datos_e[8]);
            productos = Convert.ToInt32(datos_e[9]);
            proveedores = Convert.ToInt32(datos_e[10]);
            reportes = Convert.ToInt32(datos_e[11]);
            servicios = Convert.ToInt32(datos_e[12]);
            ventas = Convert.ToInt32(datos_e[13]);
        }

        /****************************
        ****** CODIGO KEVIN *********
        /****************************/

        public static float saldoInicial = 0f;


        // Variables ventas
        float vEfectivo = 0f;
        float vTarjeta = 0f;
        float vVales = 0f;
        float vCheque = 0f;
        float vTrans = 0f;
        float vCredito = 0f;
        float vAnticipos = 0f;
        float totalVentas = 0f;

        // Variables anticipos
        float aEfectivo = 0f;
        float aTarjeta = 0f;
        float aVales = 0f;
        float aCheque = 0f;
        float aTrans = 0f;
        float totalAnticipos = 0f;

        // Variables depositos-Dinero Agregado
        float dEfectivo = 0f;
        float dTarjeta = 0f;
        float dVales = 0f;
        float dCheque = 0f;
        float dTrans = 0f;
        float totalDineroAgregado = 0f;

        // Variables caja
        float efectivo = 0f;
        float tarjeta = 0f;
        float vales = 0f;
        float cheque = 0f;
        float trans = 0f;
        float credito = 0f;

        private void temporizadorConsulta_Tick(object sender, EventArgs e)
        {

        }

        float subtotal = 0f;
        float anticipos1 = 0f;
        float totalCaja = 0f;

        // Variable retiro
        float dineroRetirado = 0f;
        float retiroEfectivo = 0f;
        float retiroTarjeta = 0f;
        float retiroVales = 0f;
        float retiroCheque = 0f;
        float retiroTrans = 0f;
        float retiroCredito = 0f;

        // Variables de la seccionProductos
        string nombreP = "";
        string nombreAlterno1P = "";
        string nombreAlterno2P = "";
        float stockP = 0f;
        float precioP = 0f;
        float revisionP = 0f;
        string claveP = "";
        string codigoP = "";
        string historialP = "";
        string tipoP = "";

        string nickUsuarioP = "";
        string nickUsuarioC = "";

        public static DateTime fechaGeneral;

        public void InitializarTimerAndroid()
        {
            actualizarCaja.Interval = 600000;
            actualizarCaja.Tick += new EventHandler(actualizarCaja_Tick);
            actualizarCaja.Enabled = true;
        }

        //Se necesita para saber si la computadora tiene conexion a internet
        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int Descripcion, int ValorReservado);

        public static bool ConectadoInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }






        private void actualizarCaja_Tick(object sender, EventArgs e)
        {
            if (ConectadoInternet())
            {
                var servidor = Properties.Settings.Default.Hosting;
                if (string.IsNullOrWhiteSpace(servidor))
                {

                    MySqlConnection conexion = new MySqlConnection();
                    conexion.ConnectionString = "server=74.208.135.60;database=pudve;uid=pudvesoftware;pwd=Steroids12;";

                    CargarSaldoInicial();

                    CargarSaldo();

                    try
                    {
                        conexion.Open();
                        MySqlCommand upDateUsr = conexion.CreateCommand();
                        MySqlCommand agregar = conexion.CreateCommand();
                        MySqlCommand eliminar = conexion.CreateCommand();

                        //Actualizar IdUsuario en tabla Usuarios en MySQL
                        upDateUsr.CommandText = $"UPDATE usuarios SET idLocal ='{FormPrincipal.userID.ToString()}' WHERE usuario = '{userNickName}'";
                        int actualizarUsr = upDateUsr.ExecuteNonQuery();

                        //Consulta Borrar de MySQL por ID de Usuario
                        eliminar.CommandText = $@"DELETE FROM seccionCaja WHERE idUsuario ='{FormPrincipal.userID.ToString()}'";
                        int borrrado = eliminar.ExecuteNonQuery();

                        //Consulta Insertar de MySQL por ID de Usuario
                        agregar.CommandText = $@"INSERT INTO seccionCaja (efectivoVentas, tarjetaVentas, valesVentas, chequeVentas, transferenciaVentas, creditoVentas, anticiposUtilizadosVentas, totalVentas,  
                                                                  efectivoAnticipos, tarjetaAnticipos, valesAnticipos, chequeAnticipos, transferenciaAnticipos, totalAnticipos,   
                                                                  efectivoDineroAgregado, tarjetaDineroAgregado, valesDineroAgregado, chequeDineroAgregado, transferenciaDineroAgregado, totalDineroAgregado,   
                                                                  efectivoTotalCaja, tarjetaTotalCaja, valesTotalCaja, chequeTotalCaja, transferenciaTotalCaja, creditoTotalCaja, anticiposUtilizadosTotalCaja, saldoInicialTotalCaja, subtotalEnCajaTotalCaja, dineroRetiradoTotalCaja, totalEnCajaTotalCaja, 
                                                                  fechaActualizacion, nickUsuario, idUsuario) 
                                                         VALUES ('{vEfectivo}', '{vTarjeta}','{vVales}', '{vCheque}', '{vTrans}', '{vCredito}', '{vAnticipos}', '{totalVentas}',
                                                                 '{aEfectivo}', '{aTarjeta}', '{aVales}', '{aCheque}', '{aTrans}', '{totalAnticipos}', 
                                                                 '{dEfectivo}', '{dTarjeta}', '{dVales}', '{dCheque}', '{dTrans}', '{totalDineroAgregado}',  
                                                                 '{efectivo}', '{tarjeta}', '{vales}', '{cheque}', '{trans}', '{credito}', '{anticipos1}', '{saldoInicial}', '{subtotal}', '{dineroRetirado}', '{totalCaja}',
                                                                 '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{userNickName}', '{FormPrincipal.userID.ToString()}')";
                        int resultado = agregar.ExecuteNonQuery();
                        if (resultado > 0)
                        {
                            //MessageBox.Show("Exito ");
                            //iniciarVariablesWebService();
                        }
                        iniciarVariablesWebService();

                        DataTable tablaProductos = new DataTable();
                        string queryCargarDatosProductos = string.Empty;

                        //Actualizar IdUsuario en tabla Usuarios en MySQL
                        upDateUsr.CommandText = $"UPDATE usuarios SET idLocal ='{FormPrincipal.userID.ToString()}' WHERE usuario = '{userNickName}'";
                        int actualizarUsr1 = upDateUsr.ExecuteNonQuery();



                        //Consulta Insertar de MySQL por ID de Usuario
                        queryCargarDatosProductos = $@"SELECT P.Nombre, P.NombreAlterno1, P.NombreAlterno2, P.Stock, P.Precio, P.NumeroRevision, P.ClaveInterna, P.CodigoBarras, P.Tipo 
                                                    FROM productos AS P 
                                                    WHERE Status = 1
                                                    AND IDUsuario = {FormPrincipal.userID.ToString()}
                                                    ORDER BY P.Nombre ASC";

                        tablaProductos = cn.CargarDatos(queryCargarDatosProductos);

                        //Consulta Borrar de MySQL por ID de Usuario
                        eliminar.CommandText = $@"DELETE FROM seccionProductos WHERE idUsuario ='{FormPrincipal.userID.ToString()}' LIMIT {tablaProductos.Rows.Count}  ";
                        int borrrado1 = eliminar.ExecuteNonQuery();

                        //Consulta Agregar de MySQL 
                        StringBuilder sComand = new StringBuilder($@"INSERT INTO seccionProductos(idUsuario, nickUsuario, nombreProductos, nombreAlterno1, nombreAlterno2, 
                                                                                              stockProductos, precioProductos, revisionProductos, claveProductos, 
                                                                                              codigoProductos, historialProductos, tipoProductos, fechaUpdate)
                                                          VALUES ");
                        List<String> Rows = new List<string>();
                        if (tablaProductos.Rows.Count > 0)
                        {
                            for (int i = 0; i < tablaProductos.Rows.Count; i++)
                            {


                                nickUsuario = userNickName;
                                nombreP = tablaProductos.Rows[i]["Nombre"].ToString();
                                nombreAlterno1P = tablaProductos.Rows[i]["NombreAlterno1"].ToString();
                                nombreAlterno2P = tablaProductos.Rows[i]["NombreAlterno2"].ToString();
                                stockP = (float)Convert.ToDouble(tablaProductos.Rows[i]["Stock"].ToString());
                                precioP = (float)Convert.ToDouble(tablaProductos.Rows[i]["Precio"].ToString());
                                revisionP = (float)Convert.ToDouble(tablaProductos.Rows[i]["NumeroRevision"].ToString());
                                claveP = tablaProductos.Rows[i]["ClaveInterna"].ToString();
                                codigoP = tablaProductos.Rows[i]["CodigoBarras"].ToString();
                                tipoP = tablaProductos.Rows[i]["Tipo"].ToString();
                                



                                Rows.Add(String.Format("('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}')",
                                    MySqlHelper.EscapeString($"{FormPrincipal.userID.ToString()}"), MySqlHelper.EscapeString($"{nickUsuario}"), MySqlHelper.EscapeString($"{nombreP}"),
                                    MySqlHelper.EscapeString($"{nombreAlterno1P}"), MySqlHelper.EscapeString($"{nombreAlterno2P}"), MySqlHelper.EscapeString($"{stockP}"), 
                                    MySqlHelper.EscapeString($"{precioP}"), MySqlHelper.EscapeString($"{revisionP}"), MySqlHelper.EscapeString($"{claveP}"),
                                    MySqlHelper.EscapeString($"{codigoP}"), MySqlHelper.EscapeString($"{historialP}"), MySqlHelper.EscapeString($"{tipoP}"),
                                    MySqlHelper.EscapeString($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}")));
                            }
                            sComand.Append(String.Join(",", Rows));
                            //  sComand.Append(":");
                            // conexion.Open();
                            string contenidoquery = sComand.ToString();
                            try
                            {
                                using (MySqlCommand myCmd = new MySqlCommand(sComand.ToString(), conexion))
                                {
                                    myCmd.CommandType = CommandType.Text;
                                    myCmd.ExecuteNonQuery();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("" + ex.Message.ToString());
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                        //MessageBox.Show("No se pudo concretar correctamente: \n" + ex.Message.ToString(),
                        //                "Fallo de conexion al dispositivo movil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

        }

        private void iniciarVariablesWebService()
        {
            //  Apartado de Caja  //
            vEfectivo = 0f;
            vTarjeta = 0f;
            vVales = 0f;
            vCheque = 0f;
            vTrans = 0f;
            vCredito = 0f;
            vAnticipos = 0f;
            totalVentas = 0f;
            aEfectivo = 0f;
            aTarjeta = 0f;
            aVales = 0f;
            aCheque = 0f;
            aTrans = 0f;
            totalAnticipos = 0f;
            dEfectivo = 0f;
            dTarjeta = 0f;
            dVales = 0f;
            dCheque = 0f;
            dTrans = 0f;
            totalDineroAgregado = 0f;
            efectivo = 0f;
            tarjeta = 0f;
            vales = 0f;
            cheque = 0f;
            trans = 0f;
            credito = 0f;
            anticipos1 = 0f;
            saldoInicial = 0f;
            subtotal = 0f;
            dineroRetirado = 0f;
            totalCaja = 0f;

            //  Apartado de Productos  //
            nombreP = "";
            nombreAlterno1P = "";
            nombreAlterno2P = "";
            stockP = 0f;
            precioP = 0f;
            revisionP = 0f;
            claveP = "";
            codigoP = "";
            historialP = "";
            tipoP = "";

        }

        private void CargarSaldo()
        {
            SQLiteConnection sql_con;
            SQLiteCommand consultaUno, consultaDos;
            SQLiteDataReader drUno, drDos;

            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                sql_con = new SQLiteConnection("Data source=//" + servidor + @"\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }
            else
            {
                sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }

            sql_con.Open();

            var fechaDefault = Convert.ToDateTime("0001-01-01 00:00:00");

            var consultarFecha = $"SELECT FechaOperacion FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'corte' ORDER BY FeChaOperacion DESC LIMIT 1";
            consultaUno = new SQLiteCommand(consultarFecha, sql_con);
            drUno = consultaUno.ExecuteReader();

            if (drUno.Read())
            {
                var fechaTmp = Convert.ToDateTime(drUno.GetValue(drUno.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                fechaDefault = Convert.ToDateTime(fechaTmp);
            }

            fechaGeneral = fechaDefault;

            var consulta = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID}";
            consultaDos = new SQLiteCommand(consulta, sql_con);
            drDos = consultaDos.ExecuteReader();

            int saltar = 0;

            while (drDos.Read())
            {
                string operacion = drDos.GetValue(drDos.GetOrdinal("Operacion")).ToString();
                var auxiliar = Convert.ToDateTime(drDos.GetValue(drDos.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                var fechaOperacion = Convert.ToDateTime(auxiliar);

                if (operacion == "venta" && fechaOperacion > fechaDefault)
                {
                    if (saltar == 0)
                    {
                        saltar++;
                        continue;
                    }

                    vEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    vTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    vVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    vCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    vTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    vCredito += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());
                    vAnticipos += float.Parse(drDos.GetValue(drDos.GetOrdinal("Anticipo")).ToString());
                    totalVentas = (vEfectivo + vTarjeta + vVales + vCheque + vTrans + vCredito + vAnticipos);
                }

                if (operacion == "anticipo" && fechaOperacion > fechaDefault)
                {
                    aEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    aTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    aVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    aCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    aTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    totalAnticipos = (aEfectivo + aTarjeta + aVales + aCheque + aTrans);

                }

                if (operacion == "deposito" && fechaOperacion > fechaDefault)
                {
                    dEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    dTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    dVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    dCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    dTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    totalDineroAgregado = (dEfectivo + dTarjeta + dVales + dCheque + dTrans);
                }

                if (operacion == "retiro" && fechaOperacion > fechaDefault)
                {
                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    retiroEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    retiroTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    retiroVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    retiroCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    retiroTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());
                    retiroCredito += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());


                }
            }

            // Apartado TOTAL EN CAJA
            efectivo = vEfectivo + aEfectivo + dEfectivo;
            tarjeta = vTarjeta + aTarjeta + dTarjeta;
            vales = vVales + aVales + dVales;
            cheque = vCheque + aCheque + dCheque;
            trans = vTrans + aTrans + dTrans;
            credito = vCredito;
            anticipos1 = vAnticipos;
            subtotal = efectivo + tarjeta + vales + cheque + trans + credito + saldoInicial;
            totalCaja = (subtotal - dineroRetirado);

            // Cerramos la conexion y el datareader
            drUno.Close();
            drDos.Close();
            sql_con.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            cerrarAplicacion = true;
            Application.Exit();
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            cerrarAplicacion = true;

            if (cerrarAplicacion)
            {
                var respuesta = MessageBox.Show("¿Estás seguro de cerrar la aplicación?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

            cerrarAplicacion = false;
        }
    }
}
