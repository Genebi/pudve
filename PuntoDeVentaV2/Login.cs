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

        string[] pathsOrigen, pathsDestino;

        string tabla = string.Empty;
        string queryTabla = string.Empty;
        int count = 0;
        int contadorMetodoTablas = 0;

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
                var ping = new Ping();

                try
                {
                    if (ping.Send(servidor).Status == IPStatus.Success)
                    {
                        respuesta = true;
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

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            var servidor = Properties.Settings.Default.Hosting;

            if (!VerificarServidor())
            {
                MessageBox.Show($"La computadora {servidor} no se encuentra en la Red, le \nrecomendamos verificar o desvincular esta computadora\npara poder continuar", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Condicion para ejecutar el metodo que comprueba los cambios en las tablas
            // existentes, de esta manera ejecutamos el metodo una sola vez y no lo hacemos
            // en el metodo Load ya que daba error para cuando se queria importar un archivo
            // de base de datos en el boton que se agregue en el form de Login

            if (contadorMetodoTablas == 0)
            {
                RevisarTablas();
                contadorMetodoTablas = 1;
            }

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

                string formato_usuario = "^[A-Z&Ñ]+@[A-Z&Ñ0-9]+$";

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
                    if (!File.Exists(pathDestino + filesToCopy[x - 1].ToString()))
                    {
                        File.Copy(pathOrigen + filesToCopy[x - 1].ToString(), pathDestino + filesToCopy[x - 1].ToString(), true);
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
                                        _SetPlantillaEtiquetaTmp };

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
                                            _pathPlantillaEtiquetaTmp };

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

            var pathPUDVESistema = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Varaiable para cuando hacemos el instalador
            Properties.Settings.Default.pathPUDVE = pathPUDVESistema;
            Properties.Settings.Default.Save();                 // Guardamos los dos Datos de las variables del sistema
            Properties.Settings.Default.Reload();               // Recargamos los datos de las variables del Sistema
            
            //RevisarTablas();

            txtUsuario.Text = Properties.Settings.Default.Usuario;
            txtPassword.Text = Properties.Settings.Default.Password;

            if (txtUsuario.Text != "" && txtPassword.Text != "")
            {
                btnEntrar.Focus();
                checkBoxRecordarDatos.Checked = true;
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

        private void RevisarTablas()
        {
            // 01 Anticipos
            #region Tabla Anticipos
            tabla = "Anticipos";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaAnticipos(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaAnticipos(tabla));
                    if (dbTables.GetAnticipos() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaAnticipos(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetAnticipos())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameAnticipos(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaAnticipos(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaAnticipos(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaAnticipos(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaAnticipos(tabla));
                    if (dbTables.GetAnticipos() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaAnticipos(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetAnticipos())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameAnticipos(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaAnticipos(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaAnticipos(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaAnticipos(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Anticipos
            // 02 Caja
            #region Tabla Caja
            tabla = "Caja";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaCaja(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaCaja(tabla));
                    if (dbTables.GetCaja() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaCaja(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetCaja())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameCaja(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaCaja(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaCaja(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaCaja(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaCaja(tabla));
                    if (dbTables.GetCaja() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaCaja(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetCaja())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameCaja(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaCaja(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaCaja(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaCaja(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Caja
            // 03 CatalogoUnidadesMedida
            #region Tabla CatalogoUnidadesMedida
            tabla = "CatalogoUnidadesMedida";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaCatalogoUnidadesMedida(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaCatalogoUnidadesMedida(tabla));
                    if (dbTables.GetCatalogoUnidadesMedida() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaCatalogoUnidadesMedida(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetCatalogoUnidadesMedida())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameCatalogoUnidadesMedida(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaCatalogoUnidadesMedida(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaCatalogoUnidadesMedida(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaCatalogoUnidadesMedida(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaCatalogoUnidadesMedida(tabla));
                    if (dbTables.GetCatalogoUnidadesMedida() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaCatalogoUnidadesMedida(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetCatalogoUnidadesMedida())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameCatalogoUnidadesMedida(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaCatalogoUnidadesMedida(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaCatalogoUnidadesMedida(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaCatalogoUnidadesMedida(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla CatalogoUnidadesMedida
            // 04 CodigoBarrasExtras
            #region Tabla CodigoBarrasExtras
            tabla = "CodigoBarrasExtras";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaCodigoBarrasExtras(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaCodigoBarrasExtras(tabla));
                    if (dbTables.GetCodigoBarrasExtras() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaCodigoBarrasExtras(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetCodigoBarrasExtras())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameCodigoBarrasExtras(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaCodigoBarrasExtras(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaCodigoBarrasExtras(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaCodigoBarrasExtras(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaCodigoBarrasExtras(tabla));
                    if (dbTables.GetCodigoBarrasExtras() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaCodigoBarrasExtras(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetCodigoBarrasExtras())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameCodigoBarrasExtras(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaCodigoBarrasExtras(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaCodigoBarrasExtras(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaCodigoBarrasExtras(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla CodigoBarrasExtras
            // 05 DescuentoCLiente
            #region Tabla DescuentoCliente
            tabla = "DescuentoCliente";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaDescuentoCliente(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaDescuentoCliente(tabla));
                    if (dbTables.GetDescuentoCliente() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaDescuentoCliente(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetDescuentoCliente())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameDescuentoCliente(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaDescuentoCliente(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaDescuentoCliente(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaDescuentoCliente(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaDescuentoCliente(tabla));
                    if (dbTables.GetDescuentoCliente() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaDescuentoCliente(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetDescuentoCliente())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameDescuentoCliente(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaDescuentoCliente(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaDescuentoCliente(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaDescuentoCliente(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla DescuentoCliente
            // 06 DescuentoMayoreo
            #region Tabla DescuentoMayoreo
            tabla = "DescuentoMayoreo";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaDescuentoMayoreo(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaDescuentoMayoreo(tabla));
                    if (dbTables.GetDescuentoMayoreo() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaDescuentoMayoreo(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetDescuentoMayoreo())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameDescuentoMayoreo(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaDescuentoMayoreo(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaDescuentoMayoreo(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaDescuentoMayoreo(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaDescuentoMayoreo(tabla));
                    if (dbTables.GetDescuentoMayoreo() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaDescuentoMayoreo(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetDescuentoMayoreo())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameDescuentoMayoreo(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaDescuentoMayoreo(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaDescuentoMayoreo(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaDescuentoMayoreo(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla DescuentoMayoreo
            // 07 DetallesFacturacionProductos
            #region Tabla DetallesFacturacionProductos
            tabla = "DetallesFacturacionProductos";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaDetallesFacturacionProductos(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaDetallesFacturacionProductos(tabla));
                    if (dbTables.GetDetallesFacturacionProductos() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaDetallesFacturacionProductos(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetDetallesFacturacionProductos())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameDetallesFacturacionProductos(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaDetallesFacturacionProductos(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaDetallesFacturacionProductos(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaDetallesFacturacionProductos(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaDetallesFacturacionProductos(tabla));
                    if (dbTables.GetDetallesFacturacionProductos() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaDetallesFacturacionProductos(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetDetallesFacturacionProductos())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameDetallesFacturacionProductos(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaDetallesFacturacionProductos(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaDetallesFacturacionProductos(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaDetallesFacturacionProductos(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla DetallesFacturacionProductos
            // 08 DetallesProductos
            #region Tabla DetallesProducto
            tabla = "DetallesProducto";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaDetallesProducto(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaDetallesProducto(tabla));
                    if (dbTables.GetDetallesProducto() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaDetallesProducto(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetDetallesProducto())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameDetallesProducto(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaDetallesProducto(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaDetallesProducto(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaDetallesProducto(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaDetallesProducto(tabla));
                    if (dbTables.GetDetallesProducto() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaDetallesProducto(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetDetallesProducto())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameDetallesProducto(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaDetallesProducto(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaDetallesProducto(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaDetallesProducto(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla DetallesProducto
            // 09 Empresas
            #region Tabla Empresas
            tabla = "Empresas";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaEmpresas(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaEmpresas(tabla));
                    if (dbTables.GetEmpresas() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaEmpresas(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetEmpresas())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameEmpresas(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaEmpresas(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaEmpresas(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaEmpresas(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaEmpresas(tabla));
                    if (dbTables.GetEmpresas() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaEmpresas(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetEmpresas())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameEmpresas(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaEmpresas(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaEmpresas(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaEmpresas(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Empresas
            // 10 HistorialCompras
            #region Tabla HistorialCompras
            tabla = "HistorialCompras";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaHistorialCompras(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaHistorialCompras(tabla));
                    if (dbTables.GetHistorialCompras() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaHistorialCompras(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetHistorialCompras())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameHistorialCompras(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaHistorialCompras(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaHistorialCompras(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaHistorialCompras(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaHistorialCompras(tabla));
                    if (dbTables.GetHistorialCompras() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaHistorialCompras(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetHistorialCompras())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameHistorialCompras(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaHistorialCompras(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaHistorialCompras(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaHistorialCompras(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla HistorialCompras
            // 11 HistorialModificacionRecordProduct
            #region Tabla HistorialModificacionRecordProduct
            tabla = "HistorialModificacionRecordProduct";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaHistorialModificacionRecordProduct(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaHistorialModificacionRecordProduct(tabla));
                    if (dbTables.GetHistorialModificacionRecordProduct() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaHistorialModificacionRecordProduct(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetHistorialModificacionRecordProduct())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameHistorialModificacionRecordProduct(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaHistorialModificacionRecordProduct(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaHistorialModificacionRecordProduct(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaHistorialModificacionRecordProduct(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaHistorialModificacionRecordProduct(tabla));
                    if (dbTables.GetHistorialModificacionRecordProduct() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaHistorialModificacionRecordProduct(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetHistorialModificacionRecordProduct())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameHistorialModificacionRecordProduct(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaHistorialModificacionRecordProduct(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaHistorialModificacionRecordProduct(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaHistorialModificacionRecordProduct(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla HistorialModificacionRecordProduct
            // 12 ProductoRelacionadoXML
            #region Tabla ProductoRelacionadoXML
            tabla = "ProductoRelacionadoXML";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaProductoRelacionadoXML(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaProductoRelacionadoXML(tabla));
                    if (dbTables.GetProductoRelacionadoXML() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaProductoRelacionadoXML(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetProductoRelacionadoXML())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameProductoRelacionadoXML(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaProductoRelacionadoXML(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaProductoRelacionadoXML(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaProductoRelacionadoXML(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaProductoRelacionadoXML(tabla));
                    if (dbTables.GetProductoRelacionadoXML() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaProductoRelacionadoXML(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetProductoRelacionadoXML())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameProductoRelacionadoXML(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaProductoRelacionadoXML(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaProductoRelacionadoXML(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaProductoRelacionadoXML(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla ProductoRelacionadoXML
            // 13 Productos
            #region Tabla Productos
            tabla = "Productos";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaProductos(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaProductos(tabla));
                    if (dbTables.GetProductos() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaProductos(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetProductos())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameProductos(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaProductos(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaProductos(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaProductos(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaProductos(tabla));
                    if (dbTables.GetProductos() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaProductos(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetProductos())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameProductos(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaProductos(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaProductos(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaProductos(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Productos
            // 14 ProductosDeServicios
            #region Tabla ProductosDeServicios
            tabla = "ProductosDeServicios";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaProductosDeServicios(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaProductosDeServicios(tabla));
                    if (dbTables.GetProductosDeServicios() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaProductosDeServicios(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetProductosDeServicios())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameProductosDeServicios(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaProductosDeServicios(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaProductosDeServicios(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaProductosDeServicios(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaProductosDeServicios(tabla));
                    if (dbTables.GetProductosDeServicios() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaProductosDeServicios(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetProductosDeServicios())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameProductosDeServicios(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaProductosDeServicios(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaProductosDeServicios(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaProductosDeServicios(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla ProductosDeServicios
            // 15 ProductosVenta
            #region Tabla ProductosVenta
            tabla = "ProductosVenta";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaProductosVenta(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaProductosVenta(tabla));
                    if (dbTables.GetProductosVenta() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaProductosVenta(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetProductosVenta())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameProductosVenta(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaProductosVenta(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaProductosVenta(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaProductosVenta(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaProductosVenta(tabla));
                    if (dbTables.GetProductosVenta() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaProductosVenta(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetProductosVenta())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameProductosVenta(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaProductosVenta(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaProductosVenta(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaProductosVenta(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla ProductosVenta
            // 16 Proveedores
            #region Tabla Proveedores
            tabla = "Proveedores";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaProveedores(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaProveedores(tabla));
                    if (dbTables.GetProveedores() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaProveedores(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetProveedores())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameProveedores(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaProveedores(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaProveedores(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaProveedores(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaProveedores(tabla));
                    if (dbTables.GetProveedores() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaProveedores(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetProveedores())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameProveedores(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaProveedores(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaProveedores(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaProveedores(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla ProductosVenta
            // 17 RegimenDeUsuarios
            #region Tabla RegimenDeUsuarios
            tabla = "RegimenDeUsuarios";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaRegimenDeUsuarios(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaRegimenDeUsuarios(tabla));
                    if (dbTables.GetRegimenDeUsuarios() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaRegimenDeUsuarios(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetRegimenDeUsuarios())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameRegimenDeUsuarios(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaRegimenDeUsuarios(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaRegimenDeUsuarios(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaRegimenDeUsuarios(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaRegimenDeUsuarios(tabla));
                    if (dbTables.GetRegimenDeUsuarios() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaRegimenDeUsuarios(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetRegimenDeUsuarios())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameRegimenDeUsuarios(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaRegimenDeUsuarios(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaRegimenDeUsuarios(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaRegimenDeUsuarios(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla RegimenDeUsuarios
            // 18 RegimenFiscal
            #region Tabla RegimenFiscal
            tabla = "RegimenFiscal";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaRegimenFiscal(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaRegimenFiscal(tabla));
                    if (dbTables.GetRegimenFiscal() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaRegimenFiscal(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetRegimenFiscal())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameRegimenFiscal(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaRegimenFiscal(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaRegimenFiscal(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaRegimenFiscal(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaRegimenFiscal(tabla));
                    if (dbTables.GetRegimenFiscal() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaRegimenFiscal(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetRegimenFiscal())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameRegimenFiscal(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaRegimenFiscal(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaRegimenFiscal(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaRegimenFiscal(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla RegimenFiscal
            // 19 Usuarios
            #region Tabla Usuarios
            tabla = "Usuarios";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaUsuarios(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaUsuarios(tabla));
                    if (dbTables.GetUsuarios() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaUsuarios(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetUsuarios())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameUsuarios(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaUsuarios(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaUsuarios(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaUsuarios(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaUsuarios(tabla));
                    if (dbTables.GetUsuarios() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaUsuarios(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetUsuarios())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameUsuarios(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaUsuarios(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaUsuarios(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaUsuarios(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Usuarios
            // 20 Ventas
            #region Tabla Ventas
            tabla = "Ventas";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaVentas(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaVentas(tabla));
                    if (dbTables.GetVentas() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaVentas(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetVentas())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameVentas(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaVentas(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaVentas(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaVentas(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaVentas(tabla));
                    if (dbTables.GetVentas() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaVentas(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetVentas())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameVentas(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaVentas(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaVentas(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaVentas(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Ventas
            // 21 Clientes
            #region Tabla Clientes
            tabla = "Clientes";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaClientes(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaClientes(tabla));
                    if (dbTables.GetClientes() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaClientes(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetClientes())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameClientes(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaClientes(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaClientes(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaClientes(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaClientes(tabla));
                    if (dbTables.GetClientes() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaClientes(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetClientes())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameClientes(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaClientes(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaClientes(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaClientes(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Clientes
            // 22 RevisarInventario
            #region Tabla RevisarInventario
            tabla = "RevisarInventario";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaRevisarInventario(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaRevisarInventario(tabla));
                    if (dbTables.GetRevisarInventario() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaRevisarInventario(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetRevisarInventario())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameRevisarInventario(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaRevisarInventario(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaRevisarInventario(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaRevisarInventario(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaRevisarInventario(tabla));
                    if (dbTables.GetRevisarInventario() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaRevisarInventario(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetRevisarInventario())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameRevisarInventario(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaRevisarInventario(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaRevisarInventario(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaRevisarInventario(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla RevisarInventario
            // 23 DetallesVenta
            #region Tabla DetallesVenta
            tabla = "DetallesVenta";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaDetallesVenta(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaDetallesVenta(tabla));
                    if (dbTables.GetDetallesVenta() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaDetallesVenta(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetDetallesVenta())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameDetallesVenta(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaDetallesVenta(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaDetallesVenta(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaDetallesVenta(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaDetallesVenta(tabla));
                    if (dbTables.GetDetallesVenta() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaDetallesVenta(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetDetallesVenta())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameDetallesVenta(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaDetallesVenta(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaDetallesVenta(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaDetallesVenta(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla DetallesVenta
            // 24 Abonos
            #region Tabla Abonos
            tabla = "Abonos";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaAbonos(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaAbonos(tabla));
                    if (dbTables.GetAbonos() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaAbonos(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetAbonos())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameAbonos(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaAbonos(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaAbonos(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaAbonos(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaAbonos(tabla));
                    if (dbTables.GetAbonos() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaAbonos(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetAbonos())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameAbonos(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaAbonos(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaAbonos(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaAbonos(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Abonos
            // 25 Categorias
            #region Tabla Categorias
            tabla = "Categorias";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaCategorias(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaCategorias(tabla));
                    if (dbTables.GetCategorias() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaCategorias(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetCategorias())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameCategorias(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaCategorias(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaCategorias(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaCategorias(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaCategorias(tabla));
                    if (dbTables.GetCategorias() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaCategorias(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetCategorias())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameCategorias(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaCategorias(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaCategorias(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaCategorias(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Categorias
            // 26 Ubicaciones
            #region Tabla Ubicaciones
            tabla = "Ubicaciones";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaUbicaciones(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaUbicaciones(tabla));
                    if (dbTables.GetUbicaciones() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaUbicaciones(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetUbicaciones())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameUbicaciones(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaUbicaciones(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaUbicaciones(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaUbicaciones(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaUbicaciones(tabla));
                    if (dbTables.GetUbicaciones() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaUbicaciones(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetUbicaciones())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameUbicaciones(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaUbicaciones(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaUbicaciones(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaUbicaciones(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Ubicaciones
            // 27 DetalleGeneral
            #region Tabla DetalleGeneral
            tabla = "DetalleGeneral";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaDetalleGeneral(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaDetalleGeneral(tabla));
                    if (dbTables.GetDetalleGeneral() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaDetalleGeneral(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetDetalleGeneral())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameDetalleGeneral(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaDetalleGeneral(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaDetalleGeneral(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaDetalleGeneral(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaDetalleGeneral(tabla));
                    if (dbTables.GetDetalleGeneral() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaDetalleGeneral(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetDetalleGeneral())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameDetalleGeneral(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaDetalleGeneral(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaDetalleGeneral(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaDetalleGeneral(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla DetalleGeneral
            // 28 DetallesProductoGenerales
            #region Tabla DetallesProductoGenerales
            tabla = "DetallesProductoGenerales";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaDetallesProductoGenerales(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaDetallesProductoGenerales(tabla));
                    if (dbTables.GetDetallesProductoGenerales() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaDetallesProductoGenerales(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetDetallesProductoGenerales())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameDetallesProductoGenerales(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaDetallesProductoGenerales(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaDetallesProductoGenerales(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaDetallesProductoGenerales(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaDetallesProductoGenerales(tabla));
                    if (dbTables.GetDetallesProductoGenerales() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaDetallesProductoGenerales(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetDetallesProductoGenerales())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameDetallesProductoGenerales(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaDetallesProductoGenerales(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaDetallesProductoGenerales(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaDetallesProductoGenerales(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla DetallesProductoGenerales
            // 29 ProductMessage
            #region Tabla ProductMessage
            tabla = "ProductMessage";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaProductMessage(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaProductMessage(tabla));
                    if (dbTables.GetProductMessage() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaProductMessage(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetProductMessage())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameProductMessage(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaProductMessage(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaProductMessage(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaProductMessage(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaProductMessage(tabla));
                    if (dbTables.GetProductMessage() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaProductMessage(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetProductMessage())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameProductMessage(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaProductMessage(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaProductMessage(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaProductMessage(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla ProductMessage
            // 30 CodigoBarrasGenerado
            #region Tabla CodigoBarrasGenerado
            tabla = "CodigoBarrasGenerado";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaCodigoBarrasGenerado(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaCodigoBarrasGenerado(tabla));
                    if (dbTables.GetCodigoBarrasGenerado() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaCodigoBarrasGenerado(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetCodigoBarrasGenerado())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameCodigoBarrasGenerado(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaCodigoBarrasGenerado(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaCodigoBarrasGenerado(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaCodigoBarrasGenerado(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaCodigoBarrasGenerado(tabla));
                    if (dbTables.GetCodigoBarrasGenerado() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaCodigoBarrasGenerado(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetCodigoBarrasGenerado())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameCodigoBarrasGenerado(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaCodigoBarrasGenerado(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaCodigoBarrasGenerado(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaCodigoBarrasGenerado(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla CodigoBarrasGenerado
            // 31 Empleados
            #region Tabla Empleados
            tabla = "Empleados";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaEmpleados(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaEmpleados(tabla));
                    if (dbTables.GetEmpleados() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaEmpleados(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetEmpleados())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameEmpleados(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaEmpleados(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaEmpleados(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaEmpleados(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaEmpleados(tabla));
                    if (dbTables.GetEmpleados() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaEmpleados(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetEmpleados())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameEmpleados(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaEmpleados(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaEmpleados(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaEmpleados(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Empleados
            // 32 MensajesInventario
            #region Tabla MensajesInventario
            tabla = "MensajesInventario";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaMensajesInventario(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaMensajesInventario(tabla));
                    if (dbTables.GetMensajesInventario() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaMensajesInventario(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetMensajesInventario())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameMensajesInventario(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaMensajesInventario(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaMensajesInventario(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaMensajesInventario(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaMensajesInventario(tabla));
                    if (dbTables.GetMensajesInventario() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaMensajesInventario(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetMensajesInventario())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameMensajesInventario(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaMensajesInventario(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaMensajesInventario(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaMensajesInventario(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla MensajesInventario
            // 33 Catalogo_claves_producto
            #region Tabla Catalogo_claves_producto
            tabla = "Catalogo_claves_producto";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaCatalogo_claves_producto(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaCatalogo_claves_producto(tabla));
                    if (dbTables.GetCatalogo_claves_producto() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaCatalogo_claves_producto(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetCatalogo_claves_producto())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameCatalogo_claves_producto(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaCatalogo_claves_producto(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaCatalogo_claves_producto(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaCatalogo_claves_producto(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaCatalogo_claves_producto(tabla));
                    if (dbTables.GetCatalogo_claves_producto() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaCatalogo_claves_producto(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetCatalogo_claves_producto())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameCatalogo_claves_producto(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaCatalogo_claves_producto(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaCatalogo_claves_producto(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaCatalogo_claves_producto(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Catalogo_claves_producto
            // 34 Catalogo_monedas
            #region Tabla Catalogo_monedas
            tabla = "Catalogo_monedas";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaCatalogo_monedas(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaCatalogo_monedas(tabla));
                    if (dbTables.GetCatalogo_monedas() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaCatalogo_monedas(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetCatalogo_monedas())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameCatalogo_monedas(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaCatalogo_monedas(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaCatalogo_monedas(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaCatalogo_monedas(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaCatalogo_monedas(tabla));
                    if (dbTables.GetCatalogo_monedas() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaCatalogo_monedas(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetCatalogo_monedas())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameCatalogo_monedas(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaCatalogo_monedas(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaCatalogo_monedas(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaCatalogo_monedas(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Catalogo_monedas
            // 35 HistorialPrecios
            #region Tabla HistorialPrecios
            tabla = "HistorialPrecios";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaHistorialPrecios(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaHistorialPrecios(tabla));
                    if (dbTables.GetHistorialPrecios() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaHistorialPrecios(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetHistorialPrecios())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameHistorialPrecios(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaHistorialPrecios(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaHistorialPrecios(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaHistorialPrecios(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaHistorialPrecios(tabla));
                    if (dbTables.GetHistorialPrecios() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaHistorialPrecios(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetHistorialPrecios())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameHistorialPrecios(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaHistorialPrecios(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaHistorialPrecios(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaHistorialPrecios(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla HistorialPrecios
            // 36 appSettings
            #region Tabla aappSettings
            tabla = "appSettings";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaappSettings(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaappSettings(tabla));
                    if (dbTables.GetappSettings() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaappSettings(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetappSettings())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameappSettings(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaappSettings(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaappSettings(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaappSettings(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaappSettings(tabla));
                    if (dbTables.GetappSettings() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaappSettings(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetappSettings())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameappSettings(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaappSettings(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaappSettings(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaappSettings(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla appSettings
            // 37 Configuracion
            #region Tabla Configuracion
            tabla = "Configuracion";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaConfiguracion(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaConfiguracion(tabla));
                    if (dbTables.GetConfiguracion() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaConfiguracion(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetConfiguracion())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameConfiguracion(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaConfiguracion(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaConfiguracion(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaConfiguracion(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaConfiguracion(tabla));
                    if (dbTables.GetConfiguracion() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaConfiguracion(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetConfiguracion())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameConfiguracion(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaConfiguracion(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaConfiguracion(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaConfiguracion(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Configuracion
            // 38 TipoClientes
            #region Tabla TipoClientes
            tabla = "TipoClientes";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaTipoClientes(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaTipoClientes(tabla));
                    if (dbTables.GetTipoClientes() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaTipoClientes(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetTipoClientes())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameTipoClientes(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaTipoClientes(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaTipoClientes(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaTipoClientes(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaTipoClientes(tabla));
                    if (dbTables.GetTipoClientes() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaTipoClientes(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetTipoClientes())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameTipoClientes(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaTipoClientes(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaTipoClientes(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaTipoClientes(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla TipoClientes
            // 39 FiltroProducto
            #region Tabla FiltroProducto
            tabla = "FiltroProducto";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaFiltroProducto(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaFiltroProducto(tabla));
                    if (dbTables.GetFiltroProducto() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaFiltroProducto(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetFiltroProducto())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameFiltroProducto(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaFiltroProducto(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaFiltroProducto(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaFiltroProducto(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaFiltroProducto(tabla));
                    if (dbTables.GetFiltroProducto() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaFiltroProducto(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetFiltroProducto())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameFiltroProducto(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaFiltroProducto(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaFiltroProducto(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaFiltroProducto(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla appSettings
            // 40 Facturas
            #region Tabla Facturas
            tabla = "Facturas";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaFiltroFacturas(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaFiltroFacturas(tabla));
                    if (dbTables.GetFiltroFacturas() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaFiltroFacturas(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetFiltroFacturas())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameFiltroFacturas(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaFiltroFacturas(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaFiltroFacturas(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaFiltroFacturas(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaFiltroFacturas(tabla));
                    if (dbTables.GetFiltroFacturas() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaFiltroFacturas(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetFiltroFacturas())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameFiltroFacturas(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaFiltroFacturas(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaFiltroFacturas(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaFiltroFacturas(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Facturas
            // 41 Facturas_impuestos
            #region Tabla Facturas_impuestos
            tabla = "Facturas_impuestos";
            try
            {
                checkEmpty(tabla);
            }
            catch (Exception ex)
            {
                queryTabla = dbTables.QueryNvaTablaFiltroFacturas_impuestos(tabla);
                cn.CrearTabla(queryTabla);
            }
            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaFiltroFacturas_impuestos(tabla));
                    if (dbTables.GetFiltroFacturas_impuestos() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaFiltroFacturas_impuestos(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetFiltroFacturas_impuestos())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameFiltroFacturas_impuestos(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaFiltroFacturas_impuestos(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaFiltroFacturas_impuestos(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaFiltroFacturas_impuestos(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTablaFiltroFacturas_impuestos(tabla));
                    if (dbTables.GetFiltroFacturas_impuestos() > count)
                    {
                        if (count == 0)
                        {
                            queryTabla = dbTables.QueryNvaTablaFiltroFacturas_impuestos(tabla);
                            cn.CrearTabla(queryTabla);
                        }
                        if (count > 0 && count < dbTables.GetFiltroFacturas_impuestos())
                        {
                            cn.ForeginKeysOff();
                            queryTabla = dbTables.QueryRenameFiltroFacturas_impuestos(tabla);
                            cn.renameTable(queryTabla);
                            queryTabla = dbTables.QueryNvaTablaFiltroFacturas_impuestos(tabla);
                            cn.CrearTabla(queryTabla);
                            cn.ForeginKeysOn();
                            queryTabla = dbTables.QueryUpdateTablaFiltroFacturas_impuestos(tabla);
                            cn.insertDataIntoTable(queryTabla);
                            queryTabla = dbTables.DropTablaFiltroFacturas_impuestos(tabla);
                            cn.dropOldTable(queryTabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + tabla + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion Tabla Facturas_impuestos
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

        private void checkBoxRecordarDatos_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
