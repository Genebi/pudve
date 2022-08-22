using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SQLite;
using System.Net.NetworkInformation;
using System.Deployment.Application;
using System.Security.Cryptography;
using System.Globalization;
using System.Threading;

namespace PuntoDeVentaV2
{
    public partial class Login : Form
    {
        private string _pathAssets = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\assets\";
        private string _pathBarCode = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\BarCode\";
        private string _pathBD = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\BD\";
        private string _pathIconBlack = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\icon\black\";
        private string _pathIconBlack16 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\icon\black16\";
        private string _pathMisDatos = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\MisDatos\Usuarios\";
        private string _pathPdfCode = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\PdfCode\";
        private string _pathPdfTag = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\PdfTag\";
        private string _pathProductos = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\Productos\";
        private string _pathSetCodBar = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\settings\codbar\";
        private string _pathSetFolioVenta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\settings\folioventa\";
        private string _pathSetGanancia = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\settings\ganancia\";
        private string _pathnoCheckStock = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\settings\noCheckStock\";
        private string _pathSettignSound = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\Sounds\";
        private string _pathPlantillaEtiqueta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\Plantillas\";
        private string _pathPlantillaEtiquetaTmp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\Plantillas\Tmp\";
        private string _pathXSLTFiles = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\xslt\";
        private string _pathGifs = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\icon\gifs\";
        private string _pathTags = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\icon\Tags\";

        private string _Assets = Directory.GetCurrentDirectory() + @"\PUDVE\assets\";
        private string _BarCode = Directory.GetCurrentDirectory() + @"\PUDVE\BarCode\";
        private string _BD = Directory.GetCurrentDirectory() + @"\PUDVE\BD\";
        private string _IconBlack = Directory.GetCurrentDirectory() + @"\PUDVE\icon\black\";
        private string _IconBlack16 = Directory.GetCurrentDirectory() + @"\PUDVE\icon\black16\";
        private string _MisDatos = Directory.GetCurrentDirectory() + @"\PUDVE\MisDatos\Usuarios\";
        private string _PdfCode = Directory.GetCurrentDirectory() + @"\PUDVE\PdfCode\";
        private string _PdfTag = Directory.GetCurrentDirectory() + @"\PUDVE\PdfTag\";
        private string _Productos = Directory.GetCurrentDirectory() + @"\PUDVE\Productos\";
        private string _SetCodBar = Directory.GetCurrentDirectory() + @"\PUDVE\settings\codbar\";
        private string _SetFolioVenta = Directory.GetCurrentDirectory() + @"\PUDVE\settings\folioventa\";
        private string _SetGanancia = Directory.GetCurrentDirectory() + @"\PUDVE\settings\ganancia\";
        private string _SetnoCheckStock = Directory.GetCurrentDirectory() + @"\PUDVE\settings\noCheckStock\";
        private string _SetSettingSound = Directory.GetCurrentDirectory() + @"\PUDVE\Sounds\";
        private string _SetPlantillaEtiqueta = Directory.GetCurrentDirectory() + @"\PUDVE\Plantillas\";
        private string _SetPlantillaEtiquetaTmp = Directory.GetCurrentDirectory() + @"\PUDVE\Plantillas\Tmp\";
        private string _SetXSLT = Directory.GetCurrentDirectory() + @"\PUDVE\xslt\";
        private string _SetGifs = Directory.GetCurrentDirectory() + @"\PUDVE\icon\gifs\";
        private string _SetTags = Directory.GetCurrentDirectory() + @"\PUDVE\icon\Tags\";

        string[] pathsOrigen, pathsDestino;

        string tabla = string.Empty;
        string queryTabla = string.Empty;
        int count = 0;
        public int contadorMetodoTablas = 0;
        string correo, usuarioGuardado;
        int guardarUsuario = 1;
        bool IsEmpty;
        string hash = "Password@2021$";
        string eliminar;
        DBTables dbTables = new DBTables();
        string[] user;

       
        public static string[] datosUsuario = new string[] { };

        //public string rutaLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        Conexion cn = new Conexion();
        checarVersion vs = new checarVersion();
        Consultas cs = new Consultas();
        ConnectionHandler cnx = new ConnectionHandler();
        MetodosBusquedas mb = new MetodosBusquedas();

        string usuario;
        string password;

        public void GuardarDatosLoginUsuarios()
        {
            Properties.Settings.Default.Usuario = usuario;      // hacemos que se almacene el dato Password en la variable del sistema Password
            Properties.Settings.Default.Save();                 // Guardamos los dos Datos de las variables del sistema
            Properties.Settings.Default.Reload();               // Recargamos los datos de las variables del Sistema
        }

        public void BorrarDatosLogin()
        {
            Properties.Settings.Default.Usuario = "";       // hacemos que se limpie el dato del Usuario en la variable del sistema Usuario
            Properties.Settings.Default.Password = "";      // hacemos que se limpie el dato del Password en la variable del sistema Password
            txtUsuario.Text = "";                           // Limpiamos el Text Box de Usuario
            txtPassword.Text = "";                          // Limpiamos el Text Box de Password
            Properties.Settings.Default.Save();             // Guardamos los dos Datos de las variables del sistema
            Properties.Settings.Default.Reload();           // Recargamos los datos de las variables del Sistema
        }

        public Login()
        {
            InitializeComponent();
            Select();
        }

        private bool VerificarServidor()
        {
            var respuesta = false;

            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                Ping pingSender = new Ping();

                // Crea el buffer de 32 bytes de datos para ser transmitidos
                string datos = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(datos);

                // Espera 30 segundos por una respuesta
                int tiempoEspera = 30000;

                PingOptions opciones = new PingOptions(64, true);

                // Envia la peticion
                PingReply solicitud = pingSender.Send(servidor, tiempoEspera, buffer, opciones);

                try
                {
                    if (solicitud.Status == IPStatus.Success)
                    {
                        respuesta = true;
                    }

                    if (solicitud.Status == IPStatus.TimedOut)
                    {
                        respuesta = false;
                    }
                }
                catch (PingException)
                {
                    respuesta = false;
                }
            }
            else
            {
                respuesta = true;
            }

            return respuesta;
        }

        private void modificarDateTime(string maquinaServidor)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/C cd C:\  & net time \\" + maquinaServidor + " /set /yes & exit";
            startInfo.Verb = "runas";
            process.StartInfo = startInfo;
            process.Start();

            ////Si el usuario pone que si
            ////if (Process.Start(startInfo) != null)
            ////{
            ////    Application.Exit();
            ////}
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            var IDUsuario = 0;
            FormPrincipal.condicionarMensaje = 0;
            var query = cn.CargarDatos(cs.obtenerIDUsuario(txtUsuario.Text, txtPassword.Text));
            if (!query.Rows.Count.Equals(0))
            {
                foreach (DataRow dato in query.Rows)
                {
                    IDUsuario = Convert.ToInt32(dato["ID"].ToString());
                }
            }

            int contadorSesiones = 0;
            var servidor = Properties.Settings.Default.Hosting;
            var usuarioemp = txtUsuario.Text.Split('@');

            if (!query.Rows.Count.Equals(0))
            {
                if (usuarioemp.Length.Equals(1))
                {
                    datosUsuario = cn.DatosUsuario(IDUsuario: IDUsuario, tipo: 0);
                    correo = datosUsuario[9].ToString();
                }
                else
                {
                    var emp = cn.CargarDatos(cs.IDUsuarioSinContraseña(usuarioemp[0].ToString()));
                    foreach (DataRow dato in emp.Rows)
                    {
                        IDUsuario = Convert.ToInt32(dato["ID"].ToString());
                    }
                    datosUsuario = cn.DatosUsuario(IDUsuario: IDUsuario, tipo: 0);
                    correo = datosUsuario[9].ToString();
                }
            }

            //vs.printProductVersion();

            if (!VerificarServidor())
            {
                MessageBox.Show($"La computadora {servidor} no se encuentra en la Red, le \nrecomendamos verificar o desvincular esta computadora\npara poder continuar", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (!string.IsNullOrEmpty(servidor.ToString()))
                {
                    modificarDateTime(servidor.ToString());
                }
            }
            catch (Exception ex)
            {
                Application.Exit();
            }

            // Condicion para ejecutar el metodo que comprueba los cambios en las tablas
            // existentes, de esta manera ejecutamos el metodo una sola vez y no lo hacemos
            // en el metodo Load ya que daba error para cuando se queria importar un archivo
            // de base de datos en el boton que se agregue en el form de Login

            //if (contadorMetodoTablas == 0)
            //{
            //    RevisarTablas();
            //    contadorMetodoTablas = 1;
            //}

            //========================================================
            usuario = txtUsuario.Text;
            password = txtPassword.Text;

            if (usuario != "" && password != "")
            {
                // Verifica si es el usuaro principal, o un empleado 
                bool resultado = false;
                int tipo_us = 0;
                string usuario_empleado = "";
                string password_empleado = "";

                //"^[A-Z&Ñ]+@[A-Z&Ñ0-9]+$";
                string formato_usuario = "^[A-Z&Ñ&0-9_]+@[A-Z&Ñ0-9_]+$";

                Regex exp = new Regex(formato_usuario);

                if (exp.IsMatch(usuario)) // Es un empleado
                {
                    tipo_us = 1;

                    resultado = (bool)cn.EjecutarSelect($"SELECT usuario FROM Empleados WHERE usuario='{usuario}' AND contrasena='{password}'AND estatus = 1");
                    

                    // Obtiene solo el nombre de usuario principal 

                    string[] partir = usuario.Split('@');

                    usuario = partir[0];
                    usuario_empleado = partir[1];
                    password_empleado = password;
                    
                    // Consulta password de la cuenta principal.
                    // Se hace para que al momento de enviar los datos a la comprobación de la licencia
                    // no marque error por el password incorrecto.

                    string c_password_usuariop = Convert.ToString(cn.EjecutarSelect($"SELECT Password FROM Usuarios WHERE Usuario = '{usuario}'", 4));
                    password = c_password_usuariop;
                }
                else // Es el usuario principal
                {
                    resultado = (bool)cn.EjecutarSelect($"SELECT Usuario FROM Usuarios WHERE Usuario = '{usuario}' AND Password = '{password}'");
                }

                if (resultado == true)
                {
                    // Ejecutamos el metodo para comprobar que la licencia registrada corresponda al usuario
                    if (ComprobarLicencia())
                    {
                        int Id = 0;
                        int id_emp = 0;

                        if (tipo_us == 0) // Usuario principal
                        {
                            Id = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Usuarios WHERE Usuario = '{usuario}' AND Password = '{password}'", 1));
                            cn.EjecutarConsulta(cs.aumentoContadorSesiones(Id));//actualiza el conteo de forma local

                            var DateInicioSesion = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                            ConnectionHandler manejadorConexion = new ConnectionHandler();
                            var siHayConexion = manejadorConexion.verificarInternet();

                            if (siHayConexion)
                            {
                                cnx.actualizarConteo(usuario);//actualiza el conteo online
                                cnx.registrarInicio(usuario, DateInicioSesion);
                            }

                            cn.EjecutarConsulta(cs.registroSesiones(usuario, DateInicioSesion,correo));
                            
                            if (chkRecordarContraseña.Checked == true || checkBoxRecordarUsuarui.Checked == true)
                            {
                                guardarUsuarioyContraseñaEntxt();
                            }
                            
                        }
                        else // Empleado
                        {
                            //cn.DatosUsuario
                            usuario = usuario + "@" + usuario_empleado;
                            password = password_empleado;
                            var DateInicioSesion = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            cn.EjecutarConsulta(cs.registroSesiones(usuario, DateInicioSesion, correo));
                            
                            ConnectionHandler manejadorConexion = new ConnectionHandler();
                            var siHayConexion = manejadorConexion.verificarInternet();

                            Id = Convert.ToInt32(cn.EjecutarSelect($"SELECT IDUsuario FROM Empleados WHERE usuario='{usuario}' AND contrasena='{password}'", 3));
                            // ID del empleado
                            id_emp = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Empleados WHERE usuario='{usuario}' AND contrasena='{password}'", 1));
                            cn.EjecutarConsulta(cs.aumentoContadorSesiones(Id));
                            string [] newUsuario = usuario.Split('@');

                            if (siHayConexion)
                            {
                                cnx.registrarInicio(usuario, DateInicioSesion);
                                cnx.actualizarConteo(newUsuario[0].ToString());
                            }

                            if (chkRecordarContraseña.Checked == true || checkBoxRecordarUsuarui.Checked == true)
                            {
                                guardarUsuarioyContraseñaEntxt();
                            }

                        }

                        if (!ComprobarEstadoLicencia(usuario))
                        {
                            MessageBox.Show("Tu licencia ha expirado, te recomendamos ponerte en contacto en el sitio web para renovar tu licencia.",  "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (!ComprobarInternetMensualmente(usuario))
                        {
                            MessageBox.Show("Es necesario conectarse a internet para verificar su licencia.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        FormPrincipal fp = new FormPrincipal();

                        // validacion para recordar los datos de Login
                        //if (checkBoxRecordarUsuarui.Checked == true)      // si es que el Check Box de Recordar los Datos esta marcado
                        //{
                        //    GuardarDatosLoginUsuarios();
                        //}
                        //if (chkRecordarContraseña.Checked == true)
                        //{
                        //    GuardarDatosLoginContraseñas();
                        //}

                        this.Hide();

                        fp.IdUsuario = Id;
                        fp.nickUsuario = usuario;
                        fp.passwordUsuario = password;
                        fp.TempIdUsuario = Id;
                        fp.TempNickUsr = usuario;
                        fp.TempPassUsr = password;
                        fp.t_id_empleado = id_emp;
                        fp.ShowDialog();


                        this.Close();
                    }
                }
                else
                {

                    using (DataTable dtEmp = cn.CargarDatos($"SELECT usuario FROM Empleados WHERE usuario='{txtUsuario.Text}' AND contrasena='{txtPassword.Text}'AND estatus = 0"))
                    {
                        if (!dtEmp.Rows.Count.Equals(0))
                        {
                            MessageBox.Show($"El empleado '{txtUsuario.Text}' se encuentra deshabilitado", "Aviso del Sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    txtMensaje.Text = "El usuario y/o contraseña son incorrectos\nO se encuentra Inhabilitado";
                    txtPassword.Text = "";
                    txtPassword.Focus();
                }
            }
            else
            {
                txtMensaje.Text = "Ingrese sus datos de inicio de sesión";
            }
        }

        private void guardarUsuarioyContraseñaEntxt()
        {
            ///// SE CREA LA CARPETA DONDE ESTARA EL ARCHIVO CON LAS CONTRASEÑAS Y USUARIOS RECORDADOS.
            string folderPath = @"C:\Archivos PUDVE\DatosDeUsuarios";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine(folderPath);
            }
            ///// SE CREA EL ARCHIVO TXT DONDE SE GUARDAN LOS USUARIOS Y CONTRASEÑAS.
            string path = @"C:\Archivos PUDVE\DatosDeUsuarios\UsuarioyContraseña.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                }
            }

            if (!txtUsuario.Text.Equals(string.Empty) && !txtPassword.Text.Equals(string.Empty))
            {
                if (File.Exists(path))
                {
                    if (new FileInfo(path).Length.Equals(0))
                    {
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            if (chkRecordarContraseña.Checked == false)
                            {
                                var contraseñaEncriptada2 = Encriptar("");
                                sw.WriteLine("[" + usuario + "," + contraseñaEncriptada2 + "]");
                            }
                            else
                            {
                            var contraseñaEncriptada = Encriptar(password);
                            sw.WriteLine("[" + usuario + "," + contraseñaEncriptada + "]");
                            }
                            
                        }
                    }
                }
            }

            ///// SE GUARDAN LOS USUARIOS RECORDADOS Y EN SU DEFECTO LAS CONTRASEÑAS.
            using (StreamReader sr = File.OpenText(path))
            {
                if (new FileInfo(path).Length != 0)
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        user = s.Split(',');
                        if (usuario == user[0].ToString().Replace("[", ""))
                        {
                            guardarUsuario = 0;
                        }
                    }
                }
            }

            using (StreamReader sr2 = File.OpenText(path))
            {
                if (new FileInfo(path).Length != 0)
                {
                    string s2 = "";
                    while ((s2 = sr2.ReadLine()) != null)
                    {
                        user = s2.Split(',');
                        if ("o8VKT4mG4Gg=" == user[1].ToString().Replace("]", ""))
                        {
                            guardarUsuario = 1;
                            eliminar = "1";
                        }
                    }
                }
            }

            if (eliminar == "1")
            {
                string item = "[" + txtUsuario.Text.Trim()+",o8VKT4mG4Gg=]";
                var lines = File.ReadAllLines(path).Where(line => line.Trim() != item).ToArray();
                File.WriteAllLines(path, lines);
            }



            if (guardarUsuario == 1)
            {
                if (checkBoxRecordarUsuarui.Checked == true)
                {
                    if (chkRecordarContraseña.Checked == false)
                    {
                        password = "";
                    }
                    string usuarios = @"C:\Archivos PUDVE\DatosDeUsuarios\UsuarioyContraseña.txt";

                    using (StreamWriter sw = File.AppendText(usuarios))
                    {
                        var contraseñaEncriptada = Encriptar(password);
                        sw.WriteLine("[" + usuario + "," + contraseñaEncriptada + "]");
                    }
                }
                
            }
            

        }

        private void GuardarDatosLoginContraseñas()
        {
            Properties.Settings.Default.Password = password;    // hacemos que se almacene el dato del Password en la variable del sistema Password
            Properties.Settings.Default.Save();                 // Guardamos los dos Datos de las variables del sistema
            Properties.Settings.Default.Reload();               // Recargamos los datos de las variables del Sistema
        }

        private bool ComprobarLicencia()
        {
            var datos = cn.DatosUsuario(tipo: 1, usuario: usuario, password: password);

            bool verificado = Convert.ToBoolean(Convert.ToInt32(datos[12]));

            if (!verificado)
            {
                // Verificamos que haya conexion a internet
                if (Registro.ConectadoInternet())
                {
                    MySqlConnection conexion = new MySqlConnection();

                    conexion.ConnectionString = "server=74.208.135.60;database=pudve;uid=pudvesoftware;pwd=Steroids12;";

                    try
                    {
                        conexion.Open();
                        MySqlCommand consultar = conexion.CreateCommand();
                        MySqlCommand actualizar = conexion.CreateCommand();

                        // Verificamos si el usuario que se quiere registrar ya se encuentra registrado en la base de datos online
                        consultar.CommandText = $"SELECT numeroSerie FROM Usuarios WHERE usuario = '{usuario}' AND password = '{password}' AND numeroSerie = '{Registro.TarjetaMadreID()}'";
                        MySqlDataReader reader = consultar.ExecuteReader();
                        
                        // Los datos del usuario y el numero de serie coincide
                        if (reader.Read())
                        {
                            reader.Close();

                            // Actualizamos el cmapo de la base de datos de MySQL
                            actualizar.CommandText = $"UPDATE Usuarios SET verificacionNS = 1 WHERE usuario = '{usuario}' AND password = '{password}'";
                            int resultado = actualizar.ExecuteNonQuery();

                            if (resultado > 0)
                            {
                                // Cambiamos la variable de configuracion a true para que permita hacer el inicio de sesion normal
                                cn.EjecutarConsulta($"UPDATE Usuarios SET VerificacionNS = 1 WHERE Usuario = '{usuario}' AND Password = '{password}'");

                                verificado = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ha ocurrido un error al comprobar su licencia", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            reader.Close();
                        }

                        // Cerrar conexion de MySQL
                        conexion.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Se requiere conexión a internet para el primer inicio de sesión", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return verificado;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEntrar_Click(this, new EventArgs());
            }
        }

        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            Registro ventanaRegistro = new Registro();
        
            this.Hide();

            ventanaRegistro.ShowDialog();

            this.Close();
        }

        private void createDir(string[] path)
        {
            try
            {
                for (int x = 1; x <= path.Length; x++)
                {
                    if (!Directory.Exists(path[x - 1]))    // verificamos que si no existe el directorio
                    {
                        Directory.CreateDirectory(path[x - 1]);    // lo crea para poder almacenar los archivos
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el directorio error: " + ex.Message, "Error en el Directorio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llenarUsuarioGuardado()
        {
            txtUsuario.Text = Properties.Settings.Default.Usuario;
            txtPassword.Text = Properties.Settings.Default.Password;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (Thread.CurrentThread.CurrentCulture.Name != "es-MX")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-MX");
            }
            //if (ApplicationDeployment.IsNetworkDeployed)
            //{
            //    try
            //    {
            //        ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

            //        this.Text += "Versión del Sistema: " + ad.CurrentVersion.ToString();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Aviso de la operacion\nde optención de la versión del sistema\n\nReferencia: " + ex.Message.ToString(), "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}

            //iniciarVariablesSistema();

            //verificarVentanasAbiertas();

            var pathPUDVESistema = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Variable para cuando hacemos el instalador
            Properties.Settings.Default.pathPUDVE = pathPUDVESistema;
            Properties.Settings.Default.Save();                 // Guardamos los dos Datos de las variables del sistema
            Properties.Settings.Default.Reload();               // Recargamos los datos de las variables del Sistema

            // Cuando estemos en Release Descomenta la siguiente linea
            //modoRelease();

            // Cuando estemos en Debug Descomenta la siguiente linea
            modoDebug();

            Properties.Settings.Default.Save();                 // Guardamos los dos Datos de las variables del sistema
            Properties.Settings.Default.Reload();               // Recargamos los datos de las variables del Sistema

            pathsOrigen = new string[] {_Assets,
                                        _BarCode,
                                        _BD,
                                        _IconBlack,
                                        _IconBlack16,
                                        _MisDatos,
                                        _PdfCode,
                                        _PdfTag,
                                        _Productos,
                                        _SetCodBar,
                                        _SetFolioVenta,
                                        _SetGanancia,
                                        _SetnoCheckStock,
                                        _SetSettingSound,
                                        _SetPlantillaEtiqueta,
                                        _SetPlantillaEtiquetaTmp,
                                        _SetXSLT,
                                        _SetGifs,
                                        _SetTags };

            pathsDestino = new string[] {   _pathAssets,
                                            _pathBarCode,
                                            _pathBD,
                                            _pathIconBlack,
                                            _pathIconBlack16,
                                            _pathMisDatos,
                                            _pathPdfCode,
                                            _pathPdfTag,
                                            _pathProductos,
                                            _pathSetCodBar,
                                            _pathSetFolioVenta,
                                            _pathSetGanancia,
                                            _pathnoCheckStock,
                                            _pathSettignSound,
                                            _pathPlantillaEtiqueta,
                                            _pathPlantillaEtiquetaTmp,
                                            _pathXSLTFiles,
                                            _pathGifs,
                                            _pathTags };

            createDir(pathsOrigen);

            createDir(pathsDestino);

            try
            {
                for (int i = 0; i < pathsOrigen.Length; i++)
                {
                    CopyWithProgress(pathsOrigen[i], pathsDestino[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar parametro error: " + ex.Message, "Error al pasar parametro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /* Verifica si el directorio existe, este directorio es donde se instala
             * MariaDB descargado desde el servidor de SIFO, en caso de que no exista
             * significa que es la primera vez que se instala el programa y se manda a
             * llamar el form para configurar MariaDB por primera vez, crear la base de
             * datos y las tablas
             */
            if (!Directory.Exists(@"C:\Program Files (x86)\PudveBD\"))
            {
                Hide();

                using (ConfiguracionMariaDB config = new ConfiguracionMariaDB())
                {
                    config.ShowDialog();

                    config.FormClosed += delegate
                    {
                        Show();
                    };
                }
            }

            if (Directory.Exists(@"C:\Program Files (x86)\PudveBD\"))
            {
                Hide();

                ConfiguracionMariaDB config = new ConfiguracionMariaDB(false);
                BaseDatosMySQL bd = new BaseDatosMySQL();
                TablasMySQL tablas = new TablasMySQL();

                config.Show();

                var database = Task.Run(() => bd.buildDataBase());
                database.Wait();

                var tarea = Task.Run(() => tablas.buildTables(false));                                                                                                                                                                                                     database.Wait();
                tarea.Wait();

                config.Close();
                Show();
            }

            llenarUsuarioGuardado();

            if (txtUsuario.Text != "" && txtPassword.Text != "")
            {
                btnEntrar.Focus();
                //checkBoxRecordarUsuarui.Checked = true;
            }

            cargarUsuariosGuardados();
            lbUsuarios.Visible = false;
            txtUsuario.Select();

        }


        private void cargarUsuariosGuardados()
        {
            string verificarArchivo = @"C:\Archivos PUDVE\DatosDeUsuarios\UsuarioyContraseña.txt";
            bool result = File.Exists(verificarArchivo);
            if (result == true)
            {
                if (!Properties.Settings.Default.Hosting.Equals(string.Empty))
                {
                    //vs.printProductVersion();
                }
                //string path = @"C:\Archivos PUDVE\DatosDeUsuarios\UsuarioyContraseña.txt";
                using (StreamReader sr = File.OpenText(verificarArchivo))
                {
                    if (new FileInfo(verificarArchivo).Length != 0)
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            var user = s.Split(',');
                            usuarioGuardado = user[0].ToString().Replace("[", "");
                            lbUsuarios.Items.Add(usuarioGuardado);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Metodo para hacer el copiado de archivos 
        /// necesarios para el funcionamiento
        /// correcto del sistema 
        /// </summary>
        /// <param name="pathOrigen">archivos desde ruta ofuscada</param>
        /// <param name="pathDestino">los archivos van a ir a la carpeta Roamin</param>
        private void CopyWithProgress(string pathOrigen, string pathDestino)
        {
            DirectoryInfo source = new DirectoryInfo(pathOrigen);
            FileInfo[] filesToCopy = source.GetFiles();

            try
            {
                // Loop through all files to copy.
                for (int x = 1; x <= filesToCopy.Length; x++)
                {
                    string rutaOrigen = pathOrigen + filesToCopy[x - 1].ToString();
                    string rutaDestino = pathDestino + filesToCopy[x - 1].ToString();

                    //if (filesToCopy[x - 1].ToString().Equals("DataDictionary.db"))
                    //{
                    //    //if (!File.Exists(rutaDestino))
                    //    //{
                    //    //    File.Copy(rutaOrigen, rutaDestino, true);
                    //    //}
                    //    //else if (File.Exists(rutaDestino))
                    //    //{
                    //    //    File.Delete(rutaDestino);
                    //    //    File.Copy(rutaOrigen, rutaDestino, true);
                    //    //}
                    //}
                    //else 
                    if (filesToCopy[x - 1].ToString().Equals("Tablas.sql"))
                    {
                        if (!File.Exists(rutaDestino))
                        {
                            File.Copy(rutaOrigen, rutaDestino, true);
                        }
                        else if (File.Exists(rutaDestino))
                        {
                            File.Delete(rutaDestino);
                            File.Copy(rutaOrigen, rutaDestino, true);
                        }
                    }
                    else
                    {
                        if (!File.Exists(rutaDestino))
                        {
                            File.Copy(rutaOrigen, rutaDestino, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al copiar error: " + ex.Message, "Error al copiar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void filtrarUsuariosGuardados()
        {
            if (txtUsuario.Text.Equals("") || txtUsuario.Text.Equals(string.Empty))
            {
                lbUsuarios.Visible = false;
            }
            else
            {
                string verificarArchivo = @"C:\Archivos PUDVE\DatosDeUsuarios\UsuarioyContraseña.txt";
                bool result = File.Exists(verificarArchivo);
                if (result == true)
                {
                    if (!Properties.Settings.Default.Hosting.Equals(string.Empty))
                    {
                        //vs.printProductVersion();
                    }

                    using (StreamReader sr = File.OpenText(verificarArchivo))
                    {
                        //MessageBox.Show("Proceso de lectura");
                        if (new FileInfo(verificarArchivo).Length != 0)
                        {
                            lbUsuarios.Items.Clear();
                            string s = "";
                            while ((s = sr.ReadLine()) != null)
                            {
                                var user = s.Split(',');
                                usuarioGuardado = user[0].ToString().Replace("[", "");
                                if (usuarioGuardado.StartsWith(txtUsuario.Text))
                                {

                                    lbUsuarios.Visible = true;
                                    lbUsuarios.Items.Add(usuarioGuardado);
                                }
                                else if (lbUsuarios.Items.Count == 0 || string.IsNullOrEmpty(txtUsuario.Text))
                                {

                                    lbUsuarios.Visible = false;
                                }

                            }
                        }
                    }
                }
            

                //string path = @"C:\Archivos PUDVE\DatosDeUsuarios\UsuarioyContraseña.txt";
                //if (new FileInfo(verificarArchivo).Length != 0)
                //{
                //    //MessageBox.Show("Proceso de lectura");
                //    using (StreamReader sr = File.OpenText(verificarArchivo))
                //    {
                //        lbUsuarios.Items.Clear();
                //        string s = "";
                //        while ((s = sr.ReadLine()) != null)
                //        {
                //            var user = s.Split(',');
                //            usuarioGuardado = user[0].ToString().Replace("[", "");
                //            if (usuarioGuardado.StartsWith(txtUsuario.Text))
                //            {

                //                lbUsuarios.Visible = true;
                //                lbUsuarios.Items.Add(usuarioGuardado);
                //            }
                //            else if (lbUsuarios.Items.Count == 0 || string.IsNullOrEmpty(txtUsuario.Text))
                //            {

                //                lbUsuarios.Visible = false;
                //            }

                //        }
                //    }
                //}
                //else
                //{
                //    //MessageBox.Show("No hacer nada");
                //}
            }
        }

        private void modoDebug()
        {
            Properties.Settings.Default.baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Properties.Settings.Default.archivo = @"..\..\App.config";
            Properties.Settings.Default.TipoEjecucion = 1;

            Properties.Settings.Default.rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        }

        private void modoRelease()
        {
            
            Properties.Settings.Default.baseDirectory = System.Windows.Forms.Application.StartupPath.ToString();
            Properties.Settings.Default.archivo = @"\PuntoDeVentaV2.exe.config";
            Properties.Settings.Default.TipoEjecucion = 2;

            Properties.Settings.Default.rutaDirectorio = Properties.Settings.Default.pathPUDVE;
        }

        private void iniciarVariablesSistema()
        {
            Properties.Settings.Default.AlterProductos = 0;
            Properties.Settings.Default.AlterProductosDeServicios = 0;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            // limpiamos los datos de las variables del sistema para poder olvidar los datos de inicio de login
            txtUsuario.Clear();
            txtPassword.Clear();
            BorrarDatosLogin();
            limpiarUsuarioGuardado();
        }

        private void desvincularPCMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Hosting = "";
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            MessageBox.Show("La PC ha sido desvinculada correctamente", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void importarBaseDeDatosMenuItem_Click(object sender, EventArgs e)
        {
            // Inicializamos los valores por defecto del openFileDialog
            buscarArchivoBD.FileName = string.Empty;
            buscarArchivoBD.Filter = "SQL (*.sql)|*.sql";
            buscarArchivoBD.FilterIndex = 1;
            buscarArchivoBD.RestoreDirectory = true;

            // Si ya selecciona el archivo de la base de datos se le muestra el siguiente mensaje
            if (buscarArchivoBD.ShowDialog() == DialogResult.OK)
            {
                var mensaje = string.Join(
                    Environment.NewLine,
                    "¿Estás seguro de importar el siguiente archivo",
                    $"{buscarArchivoBD.FileName}?\n",
                    "NOTA: Esta operación sobreescribirá el archivo",
                    "actual de tu base de datos en caso de que exista",
                    "alguno."
                );

                var respuesta = MessageBox.Show(mensaje, "Mensaje de confirmación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                // Si acepta el mensaje de confirmación realiza el siguiente procedimiento
                if (respuesta == DialogResult.OK)
                {
                    // Se guarda la ruta completa junto con el nombre del archivo que se selecciono
                    var rutaArchivo = buscarArchivoBD.FileName;

                    try
                    {
                        string conexion = string.Empty;

                        if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
                        {
                            conexion = "datasource=" + Properties.Settings.Default.Hosting + ";port=6666;username=root;password=;database=pudve;";
                        }
                        else
                        {
                            conexion = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
                        }

                        // Important Additional Connection Options
                        conexion += "charset=utf8;convertzerodatetime=true;";

                        using (MySqlConnection con = new MySqlConnection(conexion))
                        {
                            using (MySqlCommand cmd = new MySqlCommand())
                            {
                                using (MySqlBackup backup = new MySqlBackup(cmd))
                                {
                                    cmd.Connection = con;
                                    con.Open();
                                    backup.ImportFromFile(rutaArchivo);
                                    con.Close();
                                }
                            }
                        }

                        MessageBox.Show("Importación realizada con éxito", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {    
                e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void vincularPCEnRedMenuItem_Click(object sender, EventArgs e)
        {
            using (var vincular = new VincularPC())
            {
                vincular.ShowDialog();
            }
        }

        private void registroIniciosDeSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistroIniciosDeSesiones inicios = new RegistroIniciosDeSesiones();
            inicios.ShowDialog();
        }

        private void btnRecuperarPassword_Click(object sender, EventArgs e)
        {
            using (var recuperar = new RecuperarPassword())
            {
                recuperar.ShowDialog();
            }
        }

        private bool ComprobarEstadoLicencia(string usuario)
        {
            // Verificar que sea cuenta principal y no subusuario
            if (usuario.Contains('@'))
            {
                string[] auxiliar = usuario.Split('@');

                usuario = auxiliar[0];
            }

            bool respuesta = true;

            if (Registro.ConectadoInternet())
            {
                MySqlConnection conexion = new MySqlConnection();

                conexion.ConnectionString = "server=74.208.135.60;database=pudve;uid=pudvesoftware;pwd=Steroids12;";

                try
                {
                    conexion.Open();

                    MySqlCommand consultar = conexion.CreateCommand();
                    consultar.CommandText = $"SELECT estadoLicencia, fechaCreacion, fechaInicioLicencia, fechaFinLicencia, current_timestamp as fechaHoy, licencia FROM Usuarios WHERE usuario = '{usuario}'";
                    MySqlDataReader dr = consultar.ExecuteReader();

                    if (dr.Read())
                    {
                        int estado = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("estadoLicencia")));
                        string fechaCreacion = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("fechaCreacion"))).ToString("yyyy-MM-dd");
                        string fechaInicio = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("fechaInicioLicencia"))).ToString("yyyy-MM-dd");
                        string fechaFin = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("fechaFinLicencia"))).ToString("yyyy-MM-dd");
                        string fechaHoy = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("fechaHoy"))).ToString("yyyy-MM-dd");
                        string licencia = dr.GetValue(dr.GetOrdinal("licencia")).ToString();

                        Properties.Settings.Default.licencia = licencia;
                        Properties.Settings.Default.fechaFinLicencia = fechaFin;
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.Reload();

                        // Comprobar que la licencia esta guardada localmente
                        if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.licencia))
                        {
                            cn.EjecutarConsulta($"UPDATE usuarios SET Licencia = '{Properties.Settings.Default.licencia}' WHERE usuario = '{usuario}'");
                        }

                        // Comparar fecha actual con la fecha de caducidad de la licencia
                        int comparacion = DateTime.Compare(Convert.ToDateTime(fechaHoy), Convert.ToDateTime(fechaFin));

                        if (comparacion >= 0)
                        {
                            dr.Close();

                            // Cambiar el estado de la licencia
                            MySqlCommand actualizar = conexion.CreateCommand();

                            actualizar.CommandText = $"UPDATE usuarios SET estadoLicencia = 2 WHERE usuario = '{usuario}'";

                            int resultado = actualizar.ExecuteNonQuery();

                            if (resultado > 0)
                            {
                                respuesta = false;
                            }
                        }
                        
                        int correcto = cn.EjecutarConsulta($"UPDATE Usuarios SET EstadoLicencia = {estado}, FechaHoy = '{fechaCreacion}', FechaInicioLicencia = '{fechaInicio}', FechaFinLicencia = '{fechaFin}' WHERE Usuario = '{usuario.Trim()}'");

                        if (correcto > 0)
                        {
                            // 1 = Pagada
                            // 2 = Vencida
                            // 3 = Demo
                            if (estado == 2)
                            {
                                respuesta = false;
                            }
                        }
                    }

                    dr.Close();
                    conexion.Close();

                }
                catch (Exception ex)
                {

                }
            }

            return respuesta;
        }

        private string ComprobarLicenciaOnline(string usuario, string password)
        {
            string licencia = string.Empty;

            if (Registro.ConectadoInternet())
            {
                MySqlConnection conexion = new MySqlConnection();

                conexion.ConnectionString = "server=74.208.135.60;database=pudve;uid=pudvesoftware;pwd=Steroids12;";

                try
                {
                    conexion.Open();

                    MySqlCommand consultar = conexion.CreateCommand();
                    consultar.CommandText = $"SELECT licencia FROM Usuarios WHERE usuario = '{usuario}' AND password = '{password}' AND (estadoLicencia = 1 OR estadoLicencia = 3)";
                    MySqlDataReader dr = consultar.ExecuteReader();

                    if (dr.Read())
                    {
                        licencia = dr.GetValue(dr.GetOrdinal("licencia")).ToString();

                        Properties.Settings.Default.licencia = licencia;
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.Reload();

                        // Comprobar que la licencia esta guardada localmente
                        if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.licencia))
                        {
                            cn.EjecutarConsulta($"UPDATE usuarios SET Licencia = '{Properties.Settings.Default.licencia}' WHERE usuario = '{usuario}'");
                        }
                    }

                    dr.Close();
                    conexion.Close();

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message.ToString());
                }
            }

            return licencia;
        }

        private void opcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Verificar que sea cuenta principal y no subusuario
            if (usuario.Contains('@'))
            {
                string[] auxiliar = usuario.Split('@');

                usuario = auxiliar[0];
            }

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Para utilizar esta opción es necesario ingresar tu usuario y contraseña en la ventana de inicio de sesión.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Comprobar licencia online
            string licenciaOnline = ComprobarLicenciaOnline(usuario, password);

            // Comprobar licencia
            bool valido = (bool)cn.EjecutarSelect($"SELECT Usuario FROM Usuarios WHERE Usuario = '{usuario}' AND Password = '{password}'");

            if (valido)
            {
                string[] datos = mb.ObtenerFechaComprobacionInternet(usuario, password);

                if (datos.Count() > 0)
                {
                    string licencia = datos[4];

                    if (!string.IsNullOrWhiteSpace(licenciaOnline))
                    {
                        licencia = licenciaOnline;
                    }

                    string[] datosLicencia = licencia.Split('-');

                    string tipoLicencia = datosLicencia[0].Trim();

                    // Tipo servidor
                    if (tipoLicencia.Equals("PVLS"))
                    {
                        vincularPCEnRedMenuItem.Enabled = true;
                    }
                    else
                    {
                        vincularPCEnRedMenuItem.Enabled = false;
                    }
                }
            }
        }

        public string Encriptar(string decrypted)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(decrypted);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();

            tripDes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripDes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripDes.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }

        public string Desencriptar(string encrypted)
        {
            byte[] data = Convert.FromBase64String(encrypted);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();

            tripDes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripDes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripDes.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }

        private void txtUsuario_Click(object sender, EventArgs e)
        {
            //if (txtUsuario.Text.Equals(string.Empty))
            //{
            //    lbUsuarios.Visible = true;
            //}
        }

        private void Login_Click(object sender, EventArgs e)
        {
            lbUsuarios.Visible = false;
        }

        private void lbUsuarios_Click(object sender, EventArgs e)
        {
            var txtuser= string.Empty;
            if (lbUsuarios.SelectedItem != null)
            {
                txtuser = lbUsuarios.SelectedItem.ToString();
            }
          
            if (!string.IsNullOrWhiteSpace(txtuser))
            {
                var usuarioSeleccionado = lbUsuarios.SelectedItem.ToString();

                string path = @"C:\Archivos PUDVE\DatosDeUsuarios\UsuarioyContraseña.txt";
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        var user = s.Split(',');
                        if (usuarioSeleccionado == user[0].ToString().Replace("[", ""))
                        {
                            txtUsuario.Clear();
                            txtPassword.Clear();
                            txtUsuario.Text = user[0].ToString().Replace("[", "");
                            txtPassword.Text = Desencriptar(user[1].ToString().Replace("]", ""));
                            lbUsuarios.Visible = false;
                        }
                    }
                }
            }
            
            
        }

        private void OlvidarUsuariosGuardados_Click(object sender, EventArgs e)
        {
            UsuariosGuardados olvidarUsuarios = new UsuariosGuardados();
            olvidarUsuarios.FormClosing += delegate 
            {
                if (UsuariosGuardados.verificarBorrado.Equals(true))
                {
                    limpiarUsuarioGuardado();
                    lbUsuarios.Items.Clear();
                    cargarUsuariosGuardados();
                }
            };
            olvidarUsuarios.Show();
        }

        public void limpiarUsuarioGuardado()
        {
            Properties.Settings.Default.Password = string.Empty;
            Properties.Settings.Default.Usuario = string.Empty;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
            llenarUsuarioGuardado();
        }

        private void txtUsuario_KeyUp(object sender, KeyEventArgs e)
        {

            
            filtrarUsuariosGuardados();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            lbUsuarios.Visible = false;
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            //if (txtUsuario.Text.Equals(string.Empty))
            //{
            //    lbUsuarios.Visible = true;
            //}
            //else
            //{
            //    lbUsuarios.Visible = false;
            //}
        }

        private bool ComprobarInternetMensualmente(string usuario)
        {
            bool respuesta = true;

            // Verificar que sea cuenta principal y no subusuario
            if (usuario.Contains('@'))
            {
                string[] auxiliar = usuario.Split('@');

                usuario = auxiliar[0];
            }

            string[] datosFecha = mb.ObtenerFechaComprobacionInternet(usuario);

            DateTime fechaConexionInternet = Convert.ToDateTime(datosFecha[0]);
            DateTime fechaLimiteInternet = Convert.ToDateTime(datosFecha[1]);
            DateTime fechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime fechaUltimaVerificacion = Convert.ToDateTime(datosFecha[3]);

            int numeroDias = Convert.ToInt16(datosFecha[2]);

            int resultadoComparacion = DateTime.Compare(fechaConexionInternet, fechaLimiteInternet);

            // Si la fecha de conexion a internet agregada manualmente es menor al limite que se agrego manualmente
            if (resultadoComparacion < 0)
            {
                // Si la columna de los dias tiene cero
                if (numeroDias >= 0 && numeroDias < 30)
                {
                    // Si es igual a este valor se hace la primer actualizacion
                    if (fechaUltimaVerificacion.ToString("yyyy-MM-dd").Equals("0001-01-01"))
                    {
                        numeroDias += 1;

                        cn.EjecutarConsulta($"UPDATE usuarios SET DiasVerificacionInternet = {numeroDias}, UltimaVerificacion = '{fechaHoy.ToString("yyyy-MM-dd")}' WHERE Usuario = '{usuario}'");
                    }
                    else
                    {
                        if (!fechaHoy.ToString("yyyy-MM-dd").Equals(fechaUltimaVerificacion.ToString("yyyy-MM-dd")))
                        {
                            numeroDias += 1;

                            cn.EjecutarConsulta($"UPDATE usuarios SET DiasVerificacionInternet = {numeroDias}, UltimaVerificacion = '{fechaHoy.ToString("yyyy-MM-dd")}' WHERE Usuario = '{usuario}'");
                        }
                    }
                }

                if (numeroDias >= 30)
                {
                    // Comprobar si tiene internet
                    if (Registro.ConectadoInternet())
                    {
                        cn.EjecutarConsulta($"UPDATE usuarios SET DiasVerificacionInternet = 0, FechaConexionInternet = '{fechaHoy.ToString("yyyy-MM-dd")}', UltimaVerificacion = '{fechaHoy.ToString("yyyy-MM-dd")}' WHERE Usuario = '{usuario}'");

                        ComprobarEstadoLicencia(usuario);
                    }
                    else
                    {
                        respuesta = false;
                    }
                }
            }
            else
            {
                respuesta = false;

                MessageBox.Show("Verificar el límite de la fecha establecida", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return respuesta;
        }


    }
}
