﻿using MySql.Data.MySqlClient;
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
        private string _pathGifs = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\gifs\";
        private string _pathTags = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\Tags\";

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
        private string _SetGifs = Directory.GetCurrentDirectory() + @"\PUDVE\gifs\";
        private string _SetTags = Directory.GetCurrentDirectory() + @"\PUDVE\Tags";

        string[] pathsOrigen, pathsDestino;

        string tabla = string.Empty;
        string queryTabla = string.Empty;
        int count = 0;
        public int contadorMetodoTablas = 0;

        bool IsEmpty;

        DBTables dbTables = new DBTables();

        //public string rutaLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        Conexion cn = new Conexion();

        string usuario;
        string password;

        public void GuardarDatosLogin()
        {
            Properties.Settings.Default.Usuario = usuario;      // hacemos que se almacene el dato del Usuario en la variable del sistema Usuario
            Properties.Settings.Default.Password = password;    // hacemos que se almacene el dato del Password en la variable del sistema Password
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

        private void btnCerrarLogin_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            var servidor = Properties.Settings.Default.Hosting;

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
                string formato_usuario = "^[A-Z&Ñ&0-9]+@[A-Z&Ñ0-9]+$";

                Regex exp = new Regex(formato_usuario);
                
                if (exp.IsMatch(usuario)) // Es un empleado
                {
                    tipo_us = 1;
                    
                    resultado = (bool)cn.EjecutarSelect($"SELECT usuario FROM Empleados WHERE usuario='{usuario}' AND contrasena='{password}'");

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
                        }
                        else // Empleado
                        {
                            usuario = usuario + "@" + usuario_empleado;
                            password = password_empleado;

                            Id = Convert.ToInt32(cn.EjecutarSelect($"SELECT IDUsuario FROM Empleados WHERE usuario='{usuario}' AND contrasena='{password}'", 3));
                            // ID del empleado
                            id_emp = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Empleados WHERE usuario='{usuario}' AND contrasena='{password}'", 1));
                        }

                        FormPrincipal fp = new FormPrincipal();

                        // validacion para recordar los datos de Login
                        if (checkBoxRecordarDatos.Checked == true)      // si es que el Check Box de Recordar los Datos esta marcado
                        {
                            GuardarDatosLogin();
                        }

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
                    txtMensaje.Text = "El usuario y/o contraseña son incorrectos";
                    txtPassword.Text = "";
                    txtPassword.Focus();
                }
            }
            else
            {
                txtMensaje.Text = "Ingrese sus datos de inicio de sesión";
            }
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

        private void CopyWithProgress(string pathOrigen, string pathDestino)
        {
            DirectoryInfo source = new DirectoryInfo(pathOrigen);
            FileInfo[] filesToCopy = source.GetFiles();

            try
            {
                // Loop through all files to copy.
                for (int x = 1; x <= filesToCopy.Length; x++)
                {
                    if (filesToCopy[x - 1].ToString().Equals("DataDictionary.db"))
                    {
                        if (!File.Exists(pathDestino + filesToCopy[x - 1].ToString()))
                        {
                            File.Copy(pathOrigen + filesToCopy[x - 1].ToString(), pathDestino + filesToCopy[x - 1].ToString(), true);
                        }
                        else if (File.Exists(pathDestino + filesToCopy[x - 1].ToString()))
                        {
                            File.Delete(pathDestino + filesToCopy[x - 1].ToString());
                            File.Copy(pathOrigen + filesToCopy[x - 1].ToString(), pathDestino + filesToCopy[x - 1].ToString(), true);
                        }
                    }
                    else
                    {
                        if (!File.Exists(pathDestino + filesToCopy[x - 1].ToString()))
                        {
                            File.Copy(pathOrigen + filesToCopy[x - 1].ToString(), pathDestino + filesToCopy[x - 1].ToString(), true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al copiar error: " + ex.Message, "Error al copiar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
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

            /* Verifica si el directorio existe, este directorio es donde se instala
             * MariaDB descargado desde el servidor de SIFO, en caso de que no exista
             * significa que es la primera vez que se instala el programa y se manda a
             * llamar el form para configurar MariaDB por primera vez, crear la base de
             * datos y las tablas
             */
            //if (!Directory.Exists(@"C:\Program Files (x86)\PudveBD\"))
            //{
            //    Hide();

            //    using (ConfiguracionMariaDB config = new ConfiguracionMariaDB())
            //    {
            //        config.ShowDialog();

            //        config.FormClosed += delegate
            //        {
            //            Show();
            //        };
            //    }
            //}


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

            //RevisarTablas();

            //if (contadorMetodoTablas == 0)
            //{
            //    RevisarTablas();
            //    contadorMetodoTablas = 1;
            //}

            txtUsuario.Text = Properties.Settings.Default.Usuario;
            txtPassword.Text = Properties.Settings.Default.Password;

            if (txtUsuario.Text != "" && txtPassword.Text != "")
            {
                btnEntrar.Focus();
                checkBoxRecordarDatos.Checked = true;
            }
        }

        public void verificarVentanasAbiertas()
        {
            //foreach (Form frm in Application.OpenForms)
            //{
            //    if (frm.Name != "Login")    //Cerramos todos los formularios menos el formulario principal que contiene el menú
            //    {
            //        frm.Dispose();
            //    }
            //}
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

        private void RevisarTablas()
        {
            verificarBasesSQLite checkBasesSQLite = new verificarBasesSQLite();

            if (checkBasesSQLite.Visible)
            {
                checkBasesSQLite.ShowDialog();
            }
            else
            {
                checkBasesSQLite.ShowDialog();
            }
        }

        private bool checkEmpty(object tabla)
        {
            string queryTableCheck = $"SELECT * FROM '{tabla}'";
            IsEmpty = cn.IsEmptyTable(queryTableCheck);
            return IsEmpty;
        }

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            // limpiamos los datos de las variables del sistema para poder olvidar los datos de inicio de login
            BorrarDatosLogin();
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
            buscarArchivoBD.Filter = "SQL (*.db)|*.db";
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
                    // Convertimos la ruta en un arreglo
                    var infoArchivo = buscarArchivoBD.FileName.Split('\\');
                    // Guardamos SOLO el nombre del archivo original seleccionado
                    var nombreArchivo = infoArchivo[infoArchivo.Length - 1];
                    // Creamos una ruta temporal del archivo seleccionado sin tomar en cuenta el nombre del archivo
                    var rutaTmp = rutaArchivo.Replace(nombreArchivo, "");

                    try
                    {
                        // Copiamos el archivo original seleccionado en la misma ruta 
                        // pero con diferente nombre de manera temporal
                        File.Copy(rutaArchivo, rutaTmp + "pudveDB.db");

                        try
                        {
                            // Ruta del archivo actual de la base de datos
                            var rutaDestino = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db";

                            // Si existe el archivo se elimina
                            if (File.Exists(rutaDestino))
                            {
                                File.Delete(rutaDestino);
                            }

                            // Copiamos el archivo creado temporalmente en la ruta
                            // donde el programa buscara el archivo de la base de datos
                            File.Copy(rutaTmp + "pudveDB.db", rutaDestino);
                            // Finalmente borramos el archivo temporal de la base de datos que se importo
                            File.Delete(rutaTmp + "pudveDB.db");

                            MessageBox.Show("Importación realizada con éxito", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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

        private void btnRecuperarPassword_Click(object sender, EventArgs e)
        {
            using (var recuperar = new RecuperarPassword())
            {
                recuperar.ShowDialog();
            }
        }
    }
}
