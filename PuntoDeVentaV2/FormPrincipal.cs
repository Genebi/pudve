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
using System.Threading;
using System.Net.NetworkInformation;
using static System.Net.WebRequestMethods;
using System.Deployment.Application;
using System.Net.Mail;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;

namespace PuntoDeVentaV2
{
    public partial class FormPrincipal : Form
    {
        Conexion cn = new Conexion();
        MetodosGenerales mg = new MetodosGenerales();
        MetodosBusquedas mb = new MetodosBusquedas();
        Consultas cs = new Consultas();
        RespadoBaseDatos backUpDB = new RespadoBaseDatos();
        Cargando cargando = new Cargando();

        //checarVersion vs = new checarVersion();

        public static string[] datosUsuario = new string[] { };
        private bool cerrarAplicacion = false;
        public static int condicionarMensaje = 0;
        public static int idUsuarioPermisosParaConfiguracion = 0;

        IEnumerable<Ventas> FormVenta = Application.OpenForms.OfType<Ventas>();//Revisar esta linea

        //Obtener el nombre de la maquina
        public static string nameThisComputer = Environment.MachineName.ToString();

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
        private int basculas = 1;
        private int consulta = 1;


        //Variable para usuarios sin/con clave interna
        public static int clave { get; set; }

        // variables para poder tomar los datos que se pasaron del login a esta forma
        public int IdUsuario { get; set; }
        public string nickUsuario { get; set; }
        public string passwordUsuario { get; set; }
        public string DateCheckStock { get; set; }
        public long NumberRegCheckStock { get; set; }
        public int t_id_empleado { get; set; }

        ///Variables de SetUp
        public static int pasar = 0;
        public static int checkNoVendidos = 0;
        public static int diasNoVendidos = 0;

        public bool desdeCorteDeCaja = false;

        //Validar el cierre de sesion
        int validarCierreDeSesion = 0;

        // variables usasadas para que sea estatico los valores y asi en empresas
        // se agrege tambien la cuenta principal y poder hacer que regresemos a ella
        public int TempIdUsuario { get; set; }
        public string TempNickUsr { get; set; }
        public string TempPassUsr { get; set; }

        const string ficheroNumCheck = @"\PUDVE\settings\noCheckStock\checkStock.txt";  // directorio donde esta el archivo de numero 
        const string ficheroDateCheck = @"\PUDVE\settings\noCheckStock\checkDateStock.txt";  // directorio donde esta el archivo de fecha
        string Contenido;                                                       // para obtener el numero que tiene el codigo de barras en el arhivo

        string FechaFinal, saveDirectoryFile = string.Empty;

        int veces = 1;
        public int validacionDesdeCajaN = 0;
        public int validacionDesdeFormPrincipal = 0;


        DateTime FechaExpiracion;

        Dictionary<int, string> IdUsuarios = new Dictionary<int, string>();

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

        private void loadFromConfigDB()
        {
            string datosAppSetting = string.Empty, auxAppSetting = string.Empty;
            int completo = 0, insertar = 0;
            string[] str;

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
                            completo = 1;
                        }
                        if (found <= -1)
                        {
                            datosAppSetting += connStr + "|" + keyName + "|";
                            completo = 0;
                        }
                        if (completo.Equals(1))
                        {
                            auxAppSetting = datosAppSetting.TrimEnd('¬').TrimEnd();
                            str = auxAppSetting.Split('¬');
                            datosAppSettings.AddRange(str);
                            foreach (var item in datosAppSettings)
                            {
                                datosAppSettingToDB = item.Split('|');
                                for (int j = 0; j < datosAppSettingToDB.Length; j++)
                                {
                                    if (datosAppSettingToDB[j].Equals("true"))
                                    {
                                        datosAppSettingToDB[j] = "1";
                                    }
                                    else if (datosAppSettingToDB[j].Equals("false"))
                                    {
                                        datosAppSettingToDB[j] = "0";
                                    }
                                }
                                using (DataTable dtVerificarDatoDinamicoCompleto = cn.CargarDatos(cs.VerificarDatoDinamicoCompleto(datosAppSettingToDB[1].ToString(), datosAppSettingToDB[3].ToString(), FormPrincipal.userID)))
                                {
                                    if (dtVerificarDatoDinamicoCompleto.Rows.Count.Equals(0))
                                    {
                                        try
                                        {
                                            insertar = cn.EjecutarConsulta(cs.GuardarAppSettings(datosAppSettingToDB));
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Error al Agregar Registro a la Tabla appSettings\nTipo de Error : {0}" + ex.Message.ToString(), "Error al Agregar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }
                        }
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
            userID = IdUsuario;
            userNickName = nickUsuario;
            userPass = passwordUsuario;
            FechaCheckStock = DateCheckStock;
            NoRegCheckStock = NumberRegCheckStock;
            id_empleado = t_id_empleado;
        }

        private void btnSesion_Click(object sender, EventArgs e)
        {
            cerrarAplicacion = true;
            desdeDondeCerrarSesion();
        }

        private void actualizarCaja_Tick_1(object sender, EventArgs e)
        {
            //if (!FormPrincipal.userNickName.Contains("@"))
            //{
                if (pasar==1)
                {
                    if (!webListener.IsBusy)
                    {
                        webListener.RunWorkerAsync();
                    }
                }
            //}
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        public void cerrarSesion()
        {
            //if (validacionDesdeFormPrincipal.Equals(1) && cerrarAplicacion.Equals(true))
            //{
            //    validacionDesdeFormPrincipal = 0;
            //    cerrarAplicacion = false;
            //    Application.Restart();
            //    Environment.Exit(0);
            //}

            FormCollection formulariosApp = Application.OpenForms;
            List<Form> formularioCerrar = new List<Form>();

            foreach (Form f in formulariosApp)
            {
                if (f.Name != "FormPrincipal" && f.Name != "Login" && f.Name != "RespadoBaseDatos")
                {
                    formularioCerrar.Add(f);
                }
            }

            Form toClose = new Form();
            string name = string.Empty;

            formularioCerrar.Reverse();

            for (int i = 0; i <= formularioCerrar.Count - 1; i++)
            {
                toClose = formularioCerrar[i];
                name = toClose.Name;
                panelContenedor.Controls.Remove(toClose);
                toClose.Close();
                toClose = null;
            }

            formularioCerrar.Clear();

            this.Hide();

            desdeCorteDeCaja = false;
            validacionDesdeCajaN = 0;

            Login VentanaLogin = new Login();
            VentanaLogin.contadorMetodoTablas = 1;
            VentanaLogin.ShowDialog();
        }

        private void FormPrincipal_Paint(object sender, PaintEventArgs e)
        {
            var nameMachineServer = ObtenreComputadoraServidor();

            if (!string.IsNullOrWhiteSpace(nameMachineServer))
            {
                string[] temp;
                string titulo = this.Text += $" |  Usuario: {userNickName}  | Versión 2.5 | Asociada a: {nameMachineServer}";
                temp = titulo.Split('|');
                this.Text = $"{temp[0].ToString()} |  Usuario: {userNickName}  | Versión 2.5 | Asociada a: {nameMachineServer}";
            }
            else
            {
                string[] temp;
                string titulo = this.Text += $" |  Usuario: {userNickName} | Versión 2.5";
                temp = titulo.Split('|');
                this.Text = $"{temp[0].ToString()} |  Usuario: {userNickName} | Versión 2.5";
            }

            desdeCorteDeCaja = false;
            validacionDesdeCajaN = 0;
        }

        readonly ConnectionHandler _conHandler = new ConnectionHandler();

        private void btnImpresoras_Click(object sender, EventArgs e)
        {
            //vs.printProductVersion();

            if (basculas == 1)
            {
                AbrirFormulario<AgregarBasculas>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            validarVentasVentanas();
        }

        public FormPrincipal()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            _conHandler.HookMainForm(this);

            this.GotFocus += new EventHandler(FormPrincipal_GotFocus);
        }

        public static string Moneda { get; set; }

        private void FormPrincipal_GotFocus(object sender, EventArgs e)
        {
            var mensaje = string.Empty;

            if (desdeCorteDeCaja.Equals(true))
            {
                mensaje += $"Cerrar Sesion desde Corte de Caja\nEl valor de la variable: {validacionDesdeCajaN}";
            }
            else if (desdeCorteDeCaja.Equals(false))
            {
                mensaje += "No se cumple condición.";
            }

            //MessageBox.Show(mensaje, "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (desdeCorteDeCaja.Equals(false))
            {
                validacionDesdeCajaN = 0;
            }
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            desdeCorteDeCaja = false;
            validacionDesdeCajaN = 0;
            
            //if (ApplicationDeployment.IsNetworkDeployed)
            //{
            //    try
            //    {
            //        ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

            //        this.Text += "Versión: " + ad.CurrentVersion.ToString(); 
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Aviso de la operacion\nde optención de la versión del sistema\n\nReferencia: " + ex.Message.ToString(), "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}

            //vs.printProductVersion();

            // CargarSaldoInicial();
            //Envio de datos de Caja con el Timer
            // ConvertirMinutos();

            //Se crea el directorio principal para almacenar todos los archivos generados y carpetas
            Directory.CreateDirectory(@"C:\Archivos PUDVE");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\Reportes");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\Reportes\Historial");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\Reportes\Caja");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\Reportes\Pedidos");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\MisDatos");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\MisDatos\Usuarios");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\MisDatos\CFDI");

            obtenerDatosCheckStock();

            recargarDatos();

            TempUserID = TempIdUsuario;
            TempUserNickName = TempNickUsr;
            TempUserPass = TempPassUsr;

            ObtenerDatosUsuario(userID);

            var servidor = Properties.Settings.Default.Hosting;

            if (string.IsNullOrWhiteSpace(servidor))
            {
                //loadFormConfig();
                //loadFromConfigDB();
            }

            // Obtiene ID del empleado, y los permisos que tenga asignados.

            string formato_usuario = "^[A-Z&Ñ0-9]+@[A-Z&Ñ0-9]+$";

            Regex exp = new Regex(formato_usuario);

            if (exp.IsMatch(userNickName)) // Es un empleado
            {
                int id_empleado = obtener_id_empleado(userID);

                var datos_per = mb.obtener_permisos_empleado(id_empleado, userID);

                permisos_empleado(datos_per);

                /*var existenPermisos = (bool)cn.EjecutarSelect($"SELECT * FROM EmpleadosPermisos WHERE IDEmpleado='{id_empleado}' AND IDUsuario = {userID}");

                if (!existenPermisos)
                {
                    InsertarPermisosDefault(id_empleado);
                }*/
            }

            //====================================================================
            var datosConfig = mb.ComprobarConfiguracion();

            if (datosConfig.Count > 0)
            {
                pasar = Convert.ToInt16(datosConfig[5]);
                checkNoVendidos = Convert.ToInt16(datosConfig[11]);
                diasNoVendidos = Convert.ToInt32(datosConfig[12]);

                int inicio = Convert.ToInt32(datosConfig[22]);

                if (inicio == 1)
                {
                    // Hilo para envio de correos en segundo plano
                    Thread envio = new Thread(() => CorreoInicioSesion());
                    envio.Start();
                }
            }

            //InitializarTimerAndroid();

            //====================================================================

            // Verificar si existe registro de la tabla configuracion
            var existe = (bool)cn.EjecutarSelect($"SELECT * FROM Configuracion WHERE IDUsuario = {userID}");

            if (!existe)
            {
                cn.EjecutarConsulta($"INSERT INTO Configuracion (IDUsuario) VALUES ('{userID}')");
            }

            if (validarCierreDeSesion == 1)
            {
                Application.Exit();
            }
            validarCierreDeSesion++;


            obtenerDatoClaveInterna(userID);

            try
            {
                //string script = File.ReadAllText(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\Basculas.sql");
                var script = cs.registrarBasculas(userID);
                foreach (var item in script)
                {
                    cn.EjecutarConsulta(item);
                }
            }
            catch (Exception ex){}

            cn.EjecutarConsulta(cs.quitarSimboloRaroEspacios());

            actualizarNameReportesEmpleados();
            agregarCamposDinamicosPermisos();

            Moneda = "México / MXN - ($)";

            using (DataTable dtCuentaVieja = cn.CargarDatos(cs.verificarCuentaVieja()))
            {
                if (!dtCuentaVieja.Rows.Count.Equals(0))
                {
                    cn.EjecutarConsulta(cs.moverClaveInternaHaciaCodigoBarraExtra());
                    cn.EjecutarConsulta(cs.quitarContenidoClaveInterna());
                    cn.EjecutarConsulta(cs.configurarUsuarioParaObtenerCuentaNueva());
                    cn.EjecutarConsulta(cs.quitarCodigoBarraExtraVacios());
                }
            }

            Utilidades.remplazarComillasSimplesEnLaTablaProductos();

            //if (ConnectionHandler.ConectadoInternet())
            //{
            //    var fechaActual = Utilidades.UpdateDateTime();
            //    string dt = DateTime.Now.ToString();

            //    MessageBox.Show($"Fecha de la computadora: {dt}\nLa Fecha Actual es: {fechaActual.ToString()}", "Mensaje de Sistema");
            //}


            var consultaUsuarios = cn.CargarDatos($"SELECT IDUsuario FROM editarticket");

            foreach (DataRow usuarios in consultaUsuarios.Rows)
            {
                IdUsuarios.Add(Convert.ToInt32(usuarios[0]), "");
            }
            if (IdUsuarios.ContainsKey(FormPrincipal.userID))
            {
            }
            else
            {
                cn.EjecutarConsulta($"INSERT INTO editarticket (IDUsuario,MensajeTicket,Usuario,Direccion,ColyCP,RFC,Correo,Telefono,NombreC,DomicilioC,RFCC,CorreoC,TelefonoC,ColyCPC,FormaPagoC,logo) VALUES ('{FormPrincipal.userID}','','1','1','1','1','1','1','1','1','1','1','1','1','1','1')");
            }

            Utilidades.registrarNuevoEmpleadoPermisosConfiguracion(id_empleado);
            //Utilidades.registrarEmpleadosAntiguosPermisosConfiguracion();

            quitarSimbolosDePreguntaRegimenFiscalEnDescripcion();
            
            CorreoDe7DiasParaExpiracion();
            CorreoDe10DiasParaExpiracionDocumentosCSD();

            cn.EjecutarConsulta($"UPDATE correosproducto SET CorreoPrecioProducto = 1 ,CorreoStockProducto = 1,CorreoStockMinimo = 1 ,CorreoVentaProducto = 1 WHERE IDUsuario ={userID}");

            EnvioCorreoLicenciaActiva();
            if (pasar.Equals(1))
            {
                actualizarCaja.Enabled = true;
            }

            using (var dtTickets = cn.CargarDatos($"SELECT * FROM configuraciondetickets WHERE IDUsuario = {IdUsuario}"))
            {
                if (dtTickets.Rows.Count.Equals(0))
                {
                    cn.EjecutarConsulta($"INSERT INTO configuraciondetickets(IDUsuario)VALUES({IdUsuario})");
                }
            }
        }

        private void EnvioCorreoLicenciaActiva()
        {
            using (var DTEstadoLicencia = cn.CargarDatos($"SELECT EstadoLicencia FROM usuarios WHERE ID = {IdUsuario}"))
            {
                string status = DTEstadoLicencia.Rows[0]["EstadoLicencia"].ToString();
                if (status.Equals("1"))
                {
                    using (var DTEnvioCorreo = cn.CargarDatos($"SELECT CorreoLicenciaPagada FROM usuarios WHERE ID = {IdUsuario}"))
                    {
                        string statusCorreo = DTEnvioCorreo.Rows[0]["CorreoLicenciaPagada"].ToString();
                        if (statusCorreo.Equals("0"))
                        {
                            string correo;
                            using (DataTable email = cn.CargarDatos(cs.BuscarCorreoDelUsuario(IdUsuario)))
                            {
                                correo = email.Rows[0]["Email"].ToString();
                            }
                            string fecha = DateTime.Now.ToString("dd-MM-yyyy");
                            var asunto = "Licencia Activada PUDVE";
                            var html = $"<!DOCTYPE html> <html lang='es'> <head> <meta charset='UTF-8'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <meta name='viewport' content='width=device-width, initial-scale=1.0'> <title>Document</title> </head> <body> <h1 style='text-align:center;'>Licencia Activada</h1> <hr> <div style='text-align:center;'> <b>LICENCIA ACTIVA DE POR VIDA CON EXITO SIFO PUNTO DE VENTA</b><br> <b>PARA EL USUARIO {userNickName}<br>EL DIA {fecha} </b> </div> </body> </html>";

                            Utilidades.EnviarEmail(html, asunto, correo);
                            cn.EjecutarConsulta($"UPDATE usuarios SET CorreoLicenciaPagada = 1 WHERE ID = {IdUsuario}");

                        }
                    }
                }
            }
        }

        private void CorreoDe10DiasParaExpiracionDocumentosCSD()
        {
            string fechaNuevo = FechaExpiracion.ToString("dd-MM-yyyy");

            if (!fechaNuevo.Equals("01-01-0001"))
            {
                using (DataTable Fecha = cn.CargarDatos(cs.BusquedaFechaExpiracionDocumentosSCD(userID)))
                {
                    foreach (DataRow item in Fecha.Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(item["fechaCSD"].ToString()))
                        {
                            var dato = item["fechaCSD"].ToString();
                            string fechaConError = Fecha.Rows[0]["fechaCSD"].ToString();
                            if (fechaConError.Contains("a.m.") || fechaConError.Contains("p.m."))
                            {
                                var palabra = fechaConError.Split('.');
                                if (palabra[1].Equals("m"))
                                {
                                    palabra[1] = ". m.";
                                }
                                string FechaModificada = $"{palabra[0]}{palabra[1]}{palabra[2]}";

                                cn.CargarDatos($"UPDATE usuarios SET fecha_caducidad_cer = '{FechaModificada}'WHERE ID = '{FormPrincipal.userID}'");
                                FechaExpiracion = Convert.ToDateTime(FechaModificada.ToString());
                                var fechahoy = DateTime.Now;
                                TimeSpan comparaciondeTiempo = FechaExpiracion.Subtract(fechahoy);
                                int DiasRestantes = comparaciondeTiempo.Days;

                                if (DiasRestantes <= 10)
                                {
                                    CorreoFechaCaducidadCertificado();
                                }
                            }
                            else
                            {
                                FechaExpiracion = Convert.ToDateTime(Fecha.Rows[0]["fechaCSD"].ToString());

                                var fechahoy = DateTime.Now;
                                TimeSpan comparaciondeTiempo = FechaExpiracion.Subtract(fechahoy);
                                int DiasRestantes = comparaciondeTiempo.Days;

                                if (DiasRestantes <= 10)
                                {
                                    CorreoFechaCaducidadCertificado();
                                }
                            }

                        }
                    }
                }
                using (DataTable Fecha = cn.CargarDatos(cs.BusquedaFechaExpiracionDocumentosSCD(userID)))
                {
                    foreach (DataRow item in Fecha.Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(item["fechaCSD"].ToString()))
                        {
                            var dato = item["fechaCSD"].ToString();
                            string fechaConError = Fecha.Rows[0]["fechaCSD"].ToString();
                            if (fechaConError.Contains("a.m.") || fechaConError.Contains("p.m."))
                            {
                                var palabra = fechaConError.Split('.');
                                if (palabra[1].Equals("m"))
                                {
                                    palabra[1] = ". m.";
                                }
                                string FechaModificada = $"{palabra[0]}{palabra[1]}{palabra[2]}";

                                cn.CargarDatos($"UPDATE usuarios SET fecha_caducidad_cer = '{FechaModificada}'WHERE ID = '{FormPrincipal.userID}'");
                                FechaExpiracion = Convert.ToDateTime(FechaModificada.ToString());
                                var fechahoy = DateTime.Now;
                                TimeSpan comparaciondeTiempo = FechaExpiracion.Subtract(fechahoy);
                                int DiasRestantes = comparaciondeTiempo.Days;

                                if (DiasRestantes <= 10)
                                {
                                    CorreoFechaCaducidadCertificado();
                                }
                            }
                            else
                            {
                                FechaExpiracion = Convert.ToDateTime(Fecha.Rows[0]["fechaCSD"].ToString());

                                var fechahoy = DateTime.Now;
                                TimeSpan comparaciondeTiempo = FechaExpiracion.Subtract(fechahoy);
                                int DiasRestantes = comparaciondeTiempo.Days;

                                if (DiasRestantes <= 10)
                                {
                                    CorreoFechaCaducidadCertificado();
                                }
                            }

                        }
                    }
                }
            }
        }

        private void CorreoFechaCaducidadCertificado()
        {
            string correo;
            string certificado;
            string Nombre;
            using (DataTable ConsultaNombre = cn.CargarDatos(cs.BuscarNombreDelUsuario(IdUsuario)))
            {
                Nombre = ConsultaNombre.Rows[0]["NombreCompleto"].ToString();
            }
            using (DataTable ConsultaLicencia = cn.CargarDatos(cs.BuscarNumeroCertificado(IdUsuario)))
            {
                certificado = ConsultaLicencia.Rows[0]["num_certificado"].ToString();
            }
            using (DataTable email = cn.CargarDatos(cs.BuscarCorreoDelUsuario(IdUsuario)))
            {
                correo = email.Rows[0]["Email"].ToString();
            }
            var FechaFin = FechaExpiracion.ToString("dd-MM-yyyy");
            var asunto = "Aviso de Expiracion de Archivos CSD";
            var html = $@"<div>

            <div style = 'text-align: center;' >
            <h3>Fecha de Expiracion Proxima</h3>
            </div>
            <hr>
            <center>
               Sus Archivos CSD del punto de venta SIFO estan por vencer<br>
                 el dia <b>{FechaFin}</b> <b>{Nombre} </b>
             </center>
             <hr>
            </div>";

            Utilidades.EnviarEmail(html, asunto, correo);
        }

        private void CorreoDe7DiasParaExpiracion()
        {
            using (DataTable Fecha = cn.CargarDatos(cs.BuscarFechaDeExpiracion(userID)))
            {
                FechaExpiracion = Convert.ToDateTime(Fecha.Rows[0]["FechaFinLicencia"].ToString());
                string fechaNuevo = FechaExpiracion.ToString("dd-MM-yyyy");

                if (!fechaNuevo.Equals("01-01-0001"))
                {
                    var fechahoy = DateTime.Now;  
                
                    TimeSpan comparaciondeTiempo = FechaExpiracion.Subtract(fechahoy);
                    int DiasRestantes = comparaciondeTiempo.Days;

                     if (DiasRestantes <= 7)
                     {
                        EnvioDeCorreoAdvertenciaFechaExpiracion();
                     }
                }
            }
        }

        private void EnvioDeCorreoAdvertenciaFechaExpiracion()
        {
            string correo;
            string Licencia;
            string Nombre;
            using (DataTable ConsultaNombre = cn.CargarDatos(cs.BuscarNombreDelUsuario(IdUsuario)))
            {
                Nombre= ConsultaNombre.Rows[0]["NombreCompleto"].ToString();
            }
            using (DataTable ConsultaLicencia = cn.CargarDatos(cs.BuscarLicenciaDelUsuario(IdUsuario)))
            {
                Licencia = ConsultaLicencia.Rows[0]["Licencia"].ToString();

            }
            using (DataTable email = cn.CargarDatos(cs.BuscarCorreoDelUsuario(IdUsuario)))
            {
                 correo = email.Rows[0]["Email"].ToString();
            }
            var FechaFin = FechaExpiracion.ToString("dd-MM-yyyy");
            var asunto = "Aviso de Expiracion de licencia";
            var html = $@"<div>

            <div style = 'text-align: center;' >
            <h3>Fecha de Expiracion Proxima</h3>
            </div>
            <hr>
            <center>
              Su licencia del punto de venta SIFO <b>{Licencia} </b>esta proxima a vencer <br>
                <b>{Nombre} </b> el dia <b>{FechaFin}</b>
             </center>
             <hr>
            <center>
                    <p>
                        ¿Desea Adquirir el Punto de Venta SIFO? Precione el siguiente enlace https://sifo.com.mx/puntodeventa.php
                     </ p >
            </center>
            </div>";  

                Utilidades.EnviarEmail(html, asunto, correo);
            
        }

        private void quitarSimbolosDePreguntaRegimenFiscalEnDescripcion()
        {
            // Quitar Simbolos de Pregunta en campos AplicaFisica, AplicaMoral
            cn.EjecutarConsulta(cs.ActualizarRegimenFiscal());
            // Quitar Simbolos de Pregunta en el campo Descripcion
            cn.EjecutarConsulta(cs.SimbolosDePreguntaRegimenFiscalEnDescripcion());
        }

        public void agregarCamposDinamicosPermisos()
        {
            using (DataTable dtPermisosDinamicos = cn.CargarDatos(cs.VerificarContenidoDinamico(userID)))
            {
                if (!dtPermisosDinamicos.Rows.Count.Equals(0))
                {
                    foreach (DataRow drConcepto in dtPermisosDinamicos.Rows)
                    {
                        try
                        {
                            var concepto = drConcepto["concepto"].ToString();
                            cn.EjecutarConsulta(cs.agregarDetalleProductoPermisosDinamicos(concepto));
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void actualizarNameReportesEmpleados()
        {
            var nombre = string.Empty;
            var query = cn.CargarDatos($"SELECT NameUsr FROM RevisarInventarioReportes WHERE IDUsuario = '{FormPrincipal.userID}'");

            if (!query.Rows.Count.Equals(0))
            {
                foreach (DataRow iterar in query.Rows)
                {
                    nombre = iterar["NameUsr"].ToString();

                    if (nombre.Contains('@'))
                    {
                        var cambioNombre = cs.validarEmpleado(nombre);
                        var idEmp = buscarEmpleado(cambioNombre);

                        var cambiarNames = cn.CargarDatos($"UPDATE RevisarInventarioReportes SET NameUsr = '{cambioNombre}', IDEmpleado = '{idEmp}' WHERE IDUsuario = '{FormPrincipal.userID}' AND NameUsr = '{nombre}'");
                    }
                }
            }
        }

        private int buscarEmpleado(string name)
        {
            int result = 0;

            var query = cn.CargarDatos($"SELECT ID FROM Empleados WHERE IDUsuario = '{FormPrincipal.userID}' AND Nombre = '{name}'");

            if (!query.Rows.Count.Equals(0))
            {
                result = Convert.ToInt32(query.Rows[0]["ID"].ToString());
            }

            return result;
        }

        private void obtenerDatoClaveInterna(int idUsuario)
        {
            var consulta = cn.CargarDatos($"SELECT SinClaveInterna FROM Usuarios WHERE ID = '{idUsuario}'");

            clave = Convert.ToInt32(consulta.Rows[0]["SinClaveInterna"].ToString());

        }

        //public bool verificarInternet()
        //{
        //    string host = "google.com.mx";
        //    bool resul = false;
        //    Ping p = new Ping();
        //    try
        //    {
        //        PingReply reply = p.Send(host, 1000);
        //        if (reply.Status == IPStatus.Success)
        //        {
        //            resul = true;
        //        }
        //    }
        //    catch
        //    {
        //        resul = false;
        //    }
        //    return resul;
        //}

        /*private void InsertarPermisosDefault(int idEmpleado)
        {
            var secciones = new string[] {
                "Caja", "Ventas", "Inventario", "Anticipos",
                "MisDatos", "Facturas", "Configuracion", "Reportes",
                "Clientes", "Proveedores", "Empleados", "Productos"
            };

            foreach (var seccion in secciones)
            {
                cn.EjecutarConsulta($"INSERT INTO EmpleadosPermisos (IDEmpleado, IDUsuario, Seccion) VALUES ('{idEmpleado}', '{userID}', '{seccion}')");
            }
        }*/

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

        public string ObtenreComputadoraServidor()
        {
            var nameComputerServer = string.Empty;
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                nameComputerServer = Properties.Settings.Default.Hosting;
            }
            return nameComputerServer;
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            //vs.printProductVersion();

            if (productos == 1)
            {
                AbrirFormulario<Productos>();

                Productos.recargarDatos = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            validarVentasVentanas();
        }

        private void menuVertical_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {

            mg.EliminarFiltros();
            
            bool ayylmao = true;
            using (DataTable dtConfiguracionWeb = cn.CargarDatos($"SELECT WebCerrar,WebTotal FROM Configuracion WHERE IDUsuario = {userID}"))
            {
                if (dtConfiguracionWeb.Rows[0][1].ToString() == "1" && pasar==1)
                { 
                    FormCollection fc = Application.OpenForms;
                foreach (Form frm in fc)
                 {
                if (frm.Name == "WebUploader")
                {
                    DialogResult dialogResult = MessageBox.Show("Esperar y cerrar?", "Respaldo en curso", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        e.Cancel = true;
                        ayylmao = false;
                        frm.Refresh();
                        frm.Opacity = 1;
                        frm.TopMost = true;
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                      }
                  }
                }

                if (dtConfiguracionWeb.Rows[0][0].ToString() == "1" && pasar==1)
                {
                    if (enviarCajaAWeb())
                    {
                        enviarProdctosWeb();
                        if (dtConfiguracionWeb.Rows[0][1].ToString() == "1")
                        {
                            if (ayylmao)
                            {
                                DialogResult dialogResult = MessageBox.Show("¿Quiere realizar una copia de seguridad antes de cerrar sesión?", "¿Respaldar antes de salir?", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    WebUploader respaldazo = new WebUploader(true, this);
                                    respaldazo.ShowDialog();
                                }
                                else
                                {
                                    Environment.Exit(0);
                                }

                            }
                        }
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    
                    
                }            
            }
        }

        

        private void webListener_DoWork(object sender, DoWorkEventArgs e)
        {
            if (userNickName.Split('@')[0]=="HOUSEDEPOTAUTLAN")
            {
                string path = @"C:\Archivos PUDVE\Monosas.txt";
                if (!System.IO.File.Exists(path))
                {
                    return;
                }                
            }
            
            solicitudWEB();
        }

        private void solicitudWEB()
        {
            try
            {
                ConexionAPPWEB cn2 = new ConexionAPPWEB();
                using (DataTable dt = cn2.CargarDatos($"SELECT * FROM peticiones WHERE Cliente = '{userNickName.Split('@')[0]}'"))
                {
                    if (dt.Rows.Count > 0)
                    //if (true)
                    {
                        foreach (DataRow peticion in dt.Rows)
                        {
                            switch (peticion["Solicitud"].ToString())
                            {
                                case "Caja":
                                    if (enviarCajaAWeb())
                                    {
                                        cn2.EjecutarConsulta($"DELETE FROM peticiones WHERE Cliente = '{userNickName.Split('@')[0]}' AND Solicitud = 'Caja';");
                                    }
                                    break;
                                case "Producto":
                                    enviarProdctosWeb();
                                    cn2.EjecutarConsulta($"DELETE FROM peticiones WHERE Cliente = '{userNickName.Split('@')[0]}' AND Solicitud = 'Producto';");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
        }
            catch (Exception)
            {
                Console.WriteLine("Error garrafal");
                return;
            }
}

        private void enviarProdctosWeb()
        {
            try
            {

                ConexionAPPWEB con = new ConexionAPPWEB();
                DataTable valoresProducto = cn.CargarDatos($"SELECT Nombre,Stock,Precio,CodigoBarras AS Codigo FROM productos WHERE IDUsuario={userID} AND `Status`=1");      

                using (DataTable dt = con.CargarDatos($"SELECT DISTINCT(timestamp) FROM mirrorproductoregistro WHERE cliente = '{userNickName.Split('@')[0]}' ORDER BY timestamp ASC"))
                {
                    if (dt.Rows.Count > 2)
                    {
                        string consulta = $"DELETE FROM mirrorproductoregistro WHERE cliente = '{userNickName.Split('@')[0]}' AND timestamp = '{DateTime.Parse(dt.Rows[0]["timestamp"].ToString()).ToString("yyyy-MM-dd HH:mm:ss")}'";
                        con.EjecutarConsulta(consulta);
                    }
                    string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    string consultaregistro = "INSERT INTO mirrorproductoregistro (Cliente,timestamp)";
                    consultaregistro += $"VALUES ('{userNickName.Split('@')[0]}','{fecha}')"; 
                    con.EjecutarConsulta(consultaregistro);
                //foreach (DataRow registroProducto in valoresProducto.Rows)
                //{
                //    string consulta = "INSERT INTO mirrorproductosdatos (IDregistro, Nombre, Stock, Precio, Codigo)";
                //    consulta += $"VALUES ((SELECT MAX(ID) FROM mirrorproductoregistro),'{registroProducto[0]}','{registroProducto[1]}','{registroProducto[2]}','{registroProducto[3]}')";
                //    con.EjecutarConsulta(consulta);
                //}

                System.Data.DataColumn newColumn = new System.Data.DataColumn("IDregistro", typeof(System.String));

                using (DataTable dtT = con.CargarDatos($"SELECT MAX(ID) as ID FROM mirrorproductoregistro WHERE Cliente = '{userNickName.Split('@')[0]}'"))
                {
                    newColumn.DefaultValue = dtT.Rows[0]["ID"];
                }
               
                valoresProducto.Columns.Add(newColumn);
                newColumn.SetOrdinal(0);
                ToCSV(valoresProducto, @"C:\Archivos PUDVE\export.txt");
                bulkInsertAsync("mirrorproductosdatos");
                con.EjecutarConsulta($"UPDATE mirrorproductoregistro SET Completo = 'Completo' WHERE ID = (SELECT MAX(ID))");
                }
        }
            catch (Exception)
            {
                //No se logro la conexion a internet.
                return;
            }
}

        private void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write("|");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write("+");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();

            
        }

        private void webAuto_Tick(object sender, EventArgs e)
        {
            if (pasar == 1)
            {
                if (!webSender.IsBusy )
                {
                    webSender.RunWorkerAsync();
                }
            }
        }

        private void webSender_DoWork(object sender, DoWorkEventArgs e)
        {
                using (DataTable dtConfiguracionWeb = cn.CargarDatos($"SELECT WebAuto,WebTotal FROM Configuracion WHERE IDUsuario = {userID}"))
                {
                    if (dtConfiguracionWeb.Rows[0][0].ToString() == "1" && pasar == 1)
                    {
                        if (enviarCajaAWeb())
                        {
                            enviarProdctosWeb();
                            bool chambiador = false;
                            if (dtConfiguracionWeb.Rows[0][1].ToString() == "1")
                            {
                                FormCollection fc = Application.OpenForms;

                                foreach (Form frm in fc)
                                {
                                    if (frm.Name == "WebUploader")
                                    {
                                        chambiador = true;
                                    }
                                }

                                if (!chambiador)
                                {
                                    CheckForIllegalCrossThreadCalls = false;
                                    WebUploader respaldazo = new WebUploader(false, this);
                                    respaldazo.ShowDialog();
                                    CheckForIllegalCrossThreadCalls = true;
                                }
                            }
                    }
                    else
                    {
                        //Error de red 
                        return;
                    }
                    }
                    else
                    {
                        return;
                    }
                }
            
        }

        public async Task bulkInsertAsync(string tablename)
        {
            string connStr = "server=74.208.135.60;user=app;database=pudve;port=3306;password=12Steroids12;AllowLoadLocalInfile=true;";
            MySqlConnection conn = new MySqlConnection(connStr);

            MySqlBulkLoader bl = new MySqlBulkLoader(conn);
            bl.Local = true;
            
            bl.TableName = tablename;
           
           
            bl.FileName = @"C:\Archivos PUDVE\export.txt";
            bl.NumberOfLinesToSkip = 1;
            switch (tablename)
            {
                case "Respaldos":
                    bl.Columns.AddRange(new List<string>() { "IDCliente","FECHA", "Datos"});
                    bl.FieldTerminator = "~";
                    bl.LineTerminator = "^";
                    break;
                case "mirrorproductosdatos":
                    bl.Columns.AddRange(new List<string>() { "IDregistro", "Nombre", "Stock", "Precio", "Codigo" });
                    bl.FieldTerminator = "+";
                    bl.LineTerminator = "\n";
                    break;
                default:
                    break;
            }
            
            bl.Timeout = 50000;
            try
            {
                conn.Open();
                int count = bl.Load();

                conn.Close();
            }
            catch (Exception ex)
            {
                return;
            }
            if (System.IO.File.Exists(@"C:\Archivos PUDVE\export.txt"))
            {
                System.IO.File.Delete(@"C:\Archivos PUDVE\export.txt");
            }
        }

        private bool enviarCajaAWeb()
        {
            try
            {

            ConexionAPPWEB con = new ConexionAPPWEB();
            DataTable valoresCaja = new DataTable();
            DataTable valoresCajaDep = new DataTable();
            DataTable valoresCajaRet = new DataTable();
            WEBCaja test = new WEBCaja();
            int slot=1;

            test.FormClosed += delegate
            {
                valoresCaja = test.datosWeb;
                valoresCajaDep = test.detallesDepositoWeb;
                valoresCajaRet = test.detallesRetiroWeb;
            };
            test.ShowDialog();

            using (DataTable dt = con.CargarDatos($"SELECT DISTINCT(Fecha) FROM cajamirror WHERE cliente = '{userNickName.Split('@')[0]}' ORDER BY Fecha ASC"))
            {
                if (dt.Rows.Count>2)
                {
                    string consulta = $"DELETE FROM cajamirror WHERE cliente = '{userNickName.Split('@')[0]}' AND Fecha = '{DateTime.Parse(dt.Rows[0]["Fecha"].ToString()).ToString("yyyy-MM-dd HH:mm:ss")}'";
                    con.EjecutarConsulta(consulta);
                    consulta = $"DELETE FROM cajamirrorDetalles WHERE cliente = '{userNickName.Split('@')[0]}' AND Fecha = '{DateTime.Parse(dt.Rows[0]["Fecha"].ToString()).ToString("yyyy-MM-dd HH:mm:ss")}'";
                    con.EjecutarConsulta(consulta);
                    }
                    string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    foreach (DataRow registroCaja in valoresCaja.Rows)
                    {
                        string consulta = "INSERT INTO cajamirror (Cliente, Empleado, Fecha, ventasEfectivo, ventasTarjeta, ventasVales, ventasCheque, ventasTransferencia, ventasCredito, ventasAbonos, ventasAnticipos, ventasTotal, anticiposEfectivo, anticiposTarjeta, anticiposVales, anticiposCheque, anticiposTransferencia, anticiposTotal, agregadoEfectivo, agregadoTarjeta, agregadoVales, agregadoCheque, agregadoTransferencia, agregadoTotal, retiradoEfectivo, retiradoTarjeta, retiradoVales, retiradoCheque, retiradoTransferencia, retiradoDevolucones, totalRetirado, cajaEfectivo, cajaTarjeta, cajaVales, cajaCheque, cajaTransferencia, cajaTotal, saldoInicial, saldoInicialActual)";
                        consulta += $"VALUES ('{userNickName.Split('@')[0]}','{registroCaja[0]}','{fecha}', '{registroCaja[1]}','{registroCaja[2]}','{registroCaja[3]}','{registroCaja[4]}','{registroCaja[5]}','{registroCaja[6]}','{registroCaja[7]}','{registroCaja[8]}','{registroCaja[9]}','{registroCaja[10]}','{registroCaja[11]}','{registroCaja[12]}','{registroCaja[13]}','{registroCaja[14]}','{registroCaja[15]}','{registroCaja[16]}','{registroCaja[17]}','{registroCaja[18]}','{registroCaja[19]}','{registroCaja[20]}','{registroCaja[21]}','{registroCaja[22]}','{registroCaja[23]}','{registroCaja[24]}','{registroCaja[25]}','{registroCaja[26]}','{registroCaja[27]}','{registroCaja[28]}','{registroCaja[29]}','{registroCaja[30]}','{registroCaja[31]}','{registroCaja[32]}','{registroCaja[33]}','{registroCaja[34]}','{registroCaja[35]}','{registroCaja[36]}')";
                        con.EjecutarConsulta(consulta);                    
                    }

                    foreach (DataRow dataRow in valoresCajaDep.Rows)
                    {
                        string consulta = "INSERT INTO cajamirrorDetalles (IDCliente, IDEmpleado, Fecha, Tipo, Concepto, Cantidad, Efectivo, Tarjeta, Vales, Cheque, Transferencia, FechaOperacion)";
                        consulta += $"VALUES ('{userNickName.Split('@')[0]}','{dataRow[0]}','{fecha}', '{dataRow[1]}','{dataRow[2]}','{dataRow[3]}','{dataRow[4]}','{dataRow[5]}','{dataRow[6]}','{dataRow[7]}','{dataRow[8]}','{dataRow[9]}')";
                        con.EjecutarConsulta(consulta);
                    }

                    foreach (DataRow dataRow in valoresCajaRet.Rows)
                    {
                        string consulta = "INSERT INTO cajamirrorDetalles (IDCliente, IDEmpleado, Fecha, Tipo, Concepto, Cantidad, Efectivo, Tarjeta, Vales, Cheque, Transferencia, FechaOperacion)";
                        consulta += $"VALUES ('{userNickName.Split('@')[0]}','{dataRow[0]}','{fecha}', '{dataRow[1]}','{dataRow[2]}','{dataRow[3]}','{dataRow[4]}','{dataRow[5]}','{dataRow[6]}','{dataRow[7]}','{dataRow[8]}','{dataRow[9]}')";
                        con.EjecutarConsulta(consulta);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void BtnConsulta_Click(object sender, EventArgs e)      
        {
            if (consulta == 1)
            {
                ConsultaPrecio consultaPrecio = new ConsultaPrecio();
                consultaPrecio.ShowDialog();
               
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void validarCerrarSesion()
        {
            FormPrincipal fPrincipal = Application.OpenForms.OfType<FormPrincipal>().FirstOrDefault();

            if (fPrincipal != null)
            {
                fPrincipal.Close();
            }
        }
        private void btnVentas_Click(object sender, EventArgs e)
        {
            //Form exist = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "ListadoVentas").SingleOrDefault<Form>();

            //vs.printProductVersion();
            if (veces == 1)
            {
                if (ventas == 1)
                {
                    AbrirFormulario<ListadoVentas>();

                    ListadoVentas.recargarDatos = true;
                    ListadoVentas.abrirNuevaVenta = true;
                    veces = 2;
                    //Form exist = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "ListadoVentas").SingleOrDefault<Form>();
                }
                else
                {
                    MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else if (veces == 2)
            {
                ListadoVentas lVentas = Application.OpenForms.OfType<ListadoVentas>().FirstOrDefault();

                if (lVentas != null)
                {
                    lVentas.btnNuevaVenta_Click(this, null);
                    //Form exist = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "ListadoVentas").SingleOrDefault<Form>();
                }
            }
            cerrarAgregarBasculas();
        }

        private void cerrarAgregarBasculas()
        {
            FormCollection formulariosApp = Application.OpenForms;
            List<Form> formularioCerrar = new List<Form>();

            foreach (Form f in formulariosApp)
            {
                if (f.Name.Equals("AgregarBasculas") || f.Name.Equals("Productos"))
                {
                    formularioCerrar.Add(f);
                }
            }

            if (!formularioCerrar.Count.Equals(0))
            {
                Form toClose = new Form();
                string name = string.Empty;

                formularioCerrar.Reverse();

                for (int i = 0; i <= formularioCerrar.Count - 1; i++)
                {
                    toClose = formularioCerrar[i];
                    name = toClose.Name;
                    toClose.Close();
                }

                formularioCerrar.Clear();
            }
        }

        private void validarVentasVentanas()
        {
            veces = 1;
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            //vs.printProductVersion();

            if (clientes == 1)
            {
                AbrirFormulario<Clientes>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            validarVentasVentanas();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            //vs.printProductVersion();

            if (proveedores == 1)
            {
                AbrirFormulario<Proveedores>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            validarVentasVentanas();
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
                if (formulario.Name == "Productos")
                {
                    formulario.Visible = false;
                    formulario.Visible = true;
                }

                formulario.BringToFront();
            }
        }


        private void btnMisDatos_Click(object sender, EventArgs e)
        {
            //vs.printProductVersion();

            if (misdatos == 1)
            {
                AbrirFormulario<MisDatos>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            validarVentasVentanas();
        }

        private void btnEmpresas_Click(object sender, EventArgs e)
        {
            //vs.printProductVersion();

            if (empresas == 1)
            {
                AbrirFormulario<Empresas>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            validarVentasVentanas();
        }

        private void btnAnticipos_Click(object sender, EventArgs e)
        {
            //vs.printProductVersion();

            if (anticipos == 1)
            {
                AbrirFormulario<Anticipos>();

                Anticipos.recargarDatos = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            validarVentasVentanas();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            //vs.printProductVersion();

            if (config == 1)
            {
                AbrirFormulario<SetUpPUDVE>();

                SetUpPUDVE.recargarDatos = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            validarVentasVentanas();
        }

        private void btnCaja_Click(object sender, EventArgs e)
        {
            //vs.printProductVersion();

            Inventario.desdeRegresarProdcuto = 0;

            if (caja == 1)
            {
                AbrirFormulario<CajaN>();

                CajaN.recargarDatos = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            validarVentasVentanas();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            //vs.printProductVersion();
            
            if (inventarios == 1)
            {
                AbrirFormulario<Inventario>();

                //Inventario inventario = Application.OpenForms.OfType<Inventario>().FirstOrDefault();

                //if (inventario != null)
                //{

                //    inventario.populateAumentarDGVInventario();
                //}
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            validarVentasVentanas();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            //vs.printProductVersion();

            if (empleados == 1)
            {
                AbrirFormulario<Empleados>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            validarVentasVentanas(); ;
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            //vs.printProductVersion();

            //==================================================
            // Solo descomentar lo de abajo cuando sea necesario
            if (reportes == 1)
            {
                AbrirFormulario<s>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            validarVentasVentanas();
            //if (nickUsuario.Equals("OXXOCLARA3") || nickUsuario.Equals("ALEXHIT"))
            //{
            //    if (reportes == 1)
            //    {
            //        AbrirFormulario<Reportes>();
            //        validarVentasVentanas();
            //    }
            //    else
            //    {
            //        MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Estamos trabajando en este apartado", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            //vs.printProductVersion();

            if (facturas == 1)
            {
                AbrirFormulario<Facturas>();
                Facturas.volver_a_recargar_datos = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            validarVentasVentanas();
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
            basculas = Convert.ToInt32(datos_e[14]);
            consulta = Convert.ToInt32(datos_e[18]);
        }

        /****************************
        ****** CODIGO KEVIN *********
        /****************************/

        //public void InitializarTimerAndroid()
        //{

        //    actualizarCaja.Interval = 60000;
        //    actualizarCaja.Tick += new EventHandler(actualizarCaja_Tick);
        //    actualizarCaja.Enabled = true;
        //}

        //private void actualizarCaja_Tick(object sender, EventArgs e)
        //{

        //    //var datoMEtodoMAfufo = verificarInternet();

        //    //if (datoMEtodoMAfufo)
        //    //{
        //    if (pasar == 1)
        //    {
        //        _conHandler.StartCheckConnectionState();
        //        //}
        //    }
        //}

        public void desdeDondeCerrarSesion()
        {
            var mensajeDelMessageBox = string.Empty;
            var tituloDelMessageBox = "Mensaje del Sistema";
            
            if (desdeCorteDeCaja.Equals(true))
            {
                mensajeDelMessageBox = "Se finalizará sesión de acuerdo con sus ajustes de la configuración";
            }
            if (cerrarAplicacion.Equals(true))
            {
                mensajeDelMessageBox = "¿Estás seguro de cerrar la Sesion\nde: " + userNickName + "?";
            }

            mg.EliminarFiltros();

            if (desdeCorteDeCaja.Equals(true) && this.Visible.Equals(true))
            {
                mostrarMensajeDeCerrarSesion(mensajeDelMessageBox, tituloDelMessageBox);
            }
            else if (desdeCorteDeCaja.Equals(true) && this.Visible.Equals(false))
            {
                mostrarMensajeDeCerrarSesion(mensajeDelMessageBox, tituloDelMessageBox);
            }

            if (cerrarAplicacion.Equals(true) && this.Visible.Equals(true))
            {
                mostrarMensajeDeCerrarSesion(mensajeDelMessageBox, tituloDelMessageBox);
            }
            else if (cerrarAplicacion.Equals(true) && this.Visible.Equals(false))
            {
                mostrarMensajeDeCerrarSesion(mensajeDelMessageBox, tituloDelMessageBox);
            }
        }

        private void mostrarMensajeDeCerrarSesion(string mensajeDelMessageBox, string tituloDelMessageBox)
        {
            DialogResult respuesta;

            if (desdeCorteDeCaja)
            {
                MessageBox.Show(mensajeDelMessageBox, tituloDelMessageBox, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cerrarSesionDesdeCaja();
            }
            else if (cerrarAplicacion)
            {
                respuesta = MessageBox.Show(mensajeDelMessageBox, tituloDelMessageBox, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (respuesta.Equals(DialogResult.Yes))
                {
                    cerrarSesionDesdeFormPricipal();
                }
                else if (respuesta.Equals(DialogResult.No))
                {
                    cerrarAplicacion = false;
                }
            }
        }

        private void cerrarSesionDesdeCaja()
        {
            validarRespaldoBaseDeDatos();
            desdeCorteDeCaja = false;

            Application.Restart();
            Environment.Exit(0);
        }

        private void cerrarSesionDesdeFormPricipal()
        {
            validarRespaldoBaseDeDatos();
            cerrarAplicacion = false;

            Application.Restart();
            Environment.Exit(0);
        }

        private void validarRespaldoBaseDeDatos()
       { 
            if (backUpDB.RespaldarDBAlCerrarSesion())
            {
                MessageBox.Show("Este proceso tardara unos segundos por favor espere.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (Application.OpenForms.OfType<Cargando>().Count() == 1)
                {
                    //e.Cancel = true;
                    Application.OpenForms.OfType<Cargando>().First().BringToFront();
                }
                // Se comento para evitar que haga respaldo al cerrar sesion
                //backUpDB.crearsaveFile();
            }
            //if (backUpDB.validarMandarRespaldoCorreo())
            //{
            //    MessageBox.Show("Este proceso tardara unos segundos por favor espere.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    if (Application.OpenForms.OfType<Cargando>().Count() == 1)
            //    {
            //        //e.Cancel = true;
            //        Application.OpenForms.OfType<Cargando>().First().BringToFront();
            //    }
            //    backUpDB.crearsaveFile();
            //}
        }

        private void timerProductos_Tick(object sender, EventArgs e)
        {
            if (checkNoVendidos == 1)
            {
                var ultimaFecha = datosUsuario[15];
                var fechaHoy = DateTime.Now.ToString("yyyy-MM-dd");

                if (!ultimaFecha.Equals(fechaHoy))
                {
                    if (string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
                    {
                        Thread hilo = new Thread(() => RealizarProcesoProductos());

                        if (!hilo.IsAlive)
                        {
                            hilo.Start();
                        }

                        // Actualizar fecha y variable de datos de usuario
                        cn.EjecutarConsulta($"UPDATE Usuarios SET FechaHoy = '{fechaHoy}' WHERE ID = {userID}");

                        datosUsuario[15] = fechaHoy;
                    }
                }
            }
        }

        private void RealizarProcesoProductos()
        {
            try
            {
                var fechaHoy = DateTime.Now;
                var vendidosTotales = new Dictionary<int, Tuple<int, string>>();

                // Obtener ID de los productos habilitados y buscarlos en la tabla HistorialCompras
                var productos = mb.ProductosActivos();

                if (productos.Count > 0)
                {
                    foreach (var producto in productos)
                    {
                        if (producto > 0)
                        {
                            // Si los encuentra obtener la primer fecha (registro del producto)
                            var fechaRegistro = mb.ObtenerFechaProductoRegistro(producto);

                            // Despues buscar los ID de productos en la tabla ProductosVenta
                            // Buscar el ultimo registro que aparece en esa tabla del producto en especifico
                            if (fechaRegistro > DateTime.MinValue)
                            {
                                var ventas = mb.ObtenerIDVentas(producto);
                                var vendidos = 0;
                                var fechaUltimaVenta = "N/A";

                                if (ventas.Count > 0)
                                {
                                    foreach (var venta in ventas)
                                    {
                                        // Obtener el ID de la venta y buscarlo en la tabla Ventas y obtener la fecha de esa venta
                                        var fechaVenta = mb.ObtenerFechaVentaProducto(venta.Key);

                                        if (fechaVenta >= fechaRegistro && fechaVenta <= fechaHoy)
                                        {
                                            vendidos += venta.Value;
                                            fechaUltimaVenta = fechaVenta.ToString();
                                        }
                                    }
                                }

                                vendidosTotales.Add(producto, new Tuple<int, string>(vendidos, fechaUltimaVenta));
                            }
                        }
                    }

                    if (vendidosTotales.Count > 0)
                    {
                        // Enviar correo de los mas y menos vendidos
                        string[] datos;

                        datos = new string[] { "menos", "MENOS" };
                        CorreoVendidos(datos, vendidosTotales, 1);

                        datos = new string[] { "más", "MAS" };
                        CorreoVendidos(datos, vendidosTotales, 2);
                    }
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message.ToString(), "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CorreoVendidos(string[] datos, Dictionary<int, Tuple<int, string>> vendidos, int tipo)
        {
            IEnumerable<KeyValuePair<int, Tuple<int, string>>> vendidosFinales;

            var asunto = $"Top 30 productos {datos[0]} vendidos";
            var email = datosUsuario[9];

            var html = $@"
                        <div>
                            <h4 style='text-align: center;'>TOP 30 PRODUCTOS {datos[1]} VENDIDOS</h4><hr>
                            <ol style='color: black; font-size: 0.8em;'>";

            // Menos vendidos
            if (tipo == 1)
            {
                vendidosFinales = vendidos.OrderBy(pair => pair.Value.Item1).Take(30);

                foreach (var vendido in vendidosFinales)
                {
                    var producto = cn.BuscarProducto(vendido.Key, userID);

                    html += $@"<li>
                            {producto[1]} <span style='color: black'>--- 
                            VENDIDOS:</span> {vendido.Value.Item1} <span style='color: black'>--- 
                            ULTIMA VENTA:</span> {vendido.Value.Item2} <span style='color: black'>---
                            STOCK:</span> {producto[4]} <span style='color: black'>---
                            PRECIO:</span> ${float.Parse(producto[2]).ToString("N2")}
                        </li>";
                }
            }

            // Mas vendidos
            if (tipo == 2)
            {
                vendidosFinales = vendidos.OrderByDescending(pair => pair.Value.Item1).Take(30);

                foreach (var vendido in vendidosFinales)
                {
                    var producto = cn.BuscarProducto(vendido.Key, userID);

                    html += $@"<li>
                            {producto[1]} <span style='color: black'>--- 
                            VENDIDOS:</span> {vendido.Value.Item1} <span style='color: black'>--- 
                            ULTIMA VENTA:</span> {vendido.Value.Item2} <span style='color: black'>---
                            STOCK:</span> {producto[4]} <span style='color: black'>---
                            PRECIO:</span> ${float.Parse(producto[2]).ToString("N2")}
                        </li>";
                }
            }

            html += @"
                    </ol>
                </div>";

            Utilidades.EnviarEmail(html, asunto, email);
        }

        private void CorreoInicioSesion()
        {
            string html = string.Empty;
            string email = datosUsuario[9];
            string asunto = string.Empty;
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            html += @"
                <div>
                    <h2>NUEVO INICIO DE SESIÓN</h2>";

            if (id_empleado > 0)
            {
                var datosEmpleado = mb.obtener_permisos_empleado(id_empleado, userID);

                string nombreEmpleado = datosEmpleado[15];//se aumento la posicion en +1(por si da error)
                string usuarioEmpleado = datosEmpleado[16];//se aumento la posicion en +1(por si da error)

                var infoEmpleado = usuarioEmpleado.Split('@');

                html += $@"
                    <p>Se ha iniciado sesión en el sistema por parte del usuario <span style='color: black;'>{infoEmpleado[0]}@{infoEmpleado[1]}</span></p>
                    <p>El ingreso al sistema fue realizado el día <span style='color: black;'>{fechaOperacion}</span>
                </div>";

                asunto = $"INICIO DE SESÍON DE{infoEmpleado[0]}@{infoEmpleado[1]}";
            }
            else
            {
                html += $@"
                    <p>Se ha iniciado sesión en el sistema por parte del usuario <span style='color: black;'>{userNickName}</span></p>
                    <p>El ingreso al sistema fue realizado el día <span style='color: black;'>{fechaOperacion}</span>
                </div>";

                asunto = $"INICIO DE SESÍON DE {userNickName}";
            }

            Utilidades.EnviarEmail(html, asunto, email);
        }
    }
}
