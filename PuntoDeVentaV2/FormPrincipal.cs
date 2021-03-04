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

namespace PuntoDeVentaV2
{
    public partial class FormPrincipal : Form
    {
        Conexion cn = new Conexion();
        MetodosGenerales mg = new MetodosGenerales();
        MetodosBusquedas mb = new MetodosBusquedas();
        Consultas cs = new Consultas();
        //checarVersion vs = new checarVersion();

        public static string[] datosUsuario = new string[] { };
        private bool cerrarAplicacion = false;

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
            this.Close();
        }

        private void actualizarCaja_Tick_1(object sender, EventArgs e)
        {

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

        private void cerrarSesion()
        {
            FormCollection formulariosApp = Application.OpenForms;
            List<Form> formularioCerrar = new List<Form>();

            foreach (Form f in formulariosApp)
            {
                if (f.Name != "FormPrincipal" && f.Name != "Login")
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
                toClose.Close();
            }

            formularioCerrar.Clear();

            this.Hide();

            Login VentanaLogin = new Login();
            VentanaLogin.contadorMetodoTablas = 1;
            VentanaLogin.ShowDialog();
        }

        private void FormPrincipal_Paint(object sender, PaintEventArgs e)
        {
            var nameMachineServer = ObtenreComputadoraServidor();

            if (!string.IsNullOrWhiteSpace(nameMachineServer))
            {
                this.Text = $"PUDVE - Punto de Venta |  Usuario: {userNickName}  | Asociada a: {nameMachineServer}";
            }
            else
            {
                this.Text = $"PUDVE - Punto de Venta |  Usuario: {userNickName}";
            }
        }

        readonly ConnectionHandler _conHandler = new ConnectionHandler();

        public FormPrincipal()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            _conHandler.HookMainForm(this);

        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
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

            string formato_usuario = "^[A-Z&Ñ]+@[A-Z&Ñ0-9]+$";

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
            }

                InitializarTimerAndroid();
            
           
            //====================================================================

            // Verificar si existe registro de la tabla configuracion
            var existe = (bool)cn.EjecutarSelect($"SELECT * FROM Configuracion WHERE IDUsuario = {userID}");

            if (!existe)
            {
                cn.EjecutarConsulta($"INSERT INTO Configuracion (IDUsuario) VALUES ('{userID}')");
            }

            if (validarCierreDeSesion==1)
            {
                Application.Exit();
            }
            validarCierreDeSesion++;


            obtenerDatoClaveInterna(userID);
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
            //if (reportes == 1)
            //{
            //    AbrirFormulario<Reportes>();
            //}
            //else
            //{
            //    MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}

            //validarVentasVentanas();

            MessageBox.Show("Estamos trabajando en este apartado", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        /****************************
        ****** CODIGO KEVIN *********
        /****************************/

        public void InitializarTimerAndroid()
        {
            
                actualizarCaja.Interval = 60000;
                actualizarCaja.Tick += new EventHandler(actualizarCaja_Tick);
                actualizarCaja.Enabled = true;
        }

        private void actualizarCaja_Tick(object sender, EventArgs e)
        {
            
            //var datoMEtodoMAfufo = verificarInternet();
           
            //if (datoMEtodoMAfufo)
            //{
                if (pasar == 1)
                {
                    _conHandler.StartCheckConnectionState();
                //}
            }
}

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cerrarAplicacion.Equals(true) && this.Visible.Equals(true))
            {
                var respuesta = MessageBox.Show("¿Estás seguro de cerrar la Sesion\nde: " + userNickName + "?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (respuesta == DialogResult.Yes)
                {
                    e.Cancel = true;
                    cerrarSesion();
                    cerrarAplicacion = false;
                }
                else if (respuesta == DialogResult.No)
                {
                    e.Cancel = true;
                    cerrarAplicacion = false;
                }
            }
            else if (cerrarAplicacion.Equals(false) && this.Visible.Equals(true))
            {
                var respuesta = MessageBox.Show("¿Estás seguro de cerrar la Sesion\nde: " + userNickName + "?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (respuesta == DialogResult.Yes)
                {
                    e.Cancel = true;
                    cerrarSesion();
                    cerrarAplicacion = false;
                }
                else if (respuesta == DialogResult.No)
                {
                    e.Cancel = true;
                    cerrarAplicacion = false;
                }
            }
            else if (cerrarAplicacion.Equals(false) && this.Visible.Equals(false))
            {
                Application.Exit();
            }
            cerrarAplicacion = false;
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
                            <ol style='color: red; font-size: 0.8em;'>";

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
    }
}
