using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        string[] pathsOrigen, pathsDestino;

        string tabla = string.Empty;
        string queryTabla = string.Empty;
        int count = 0;

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
        }

        private void btnCerrarLogin_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            usuario = txtUsuario.Text;
            password = txtPassword.Text;

            if (usuario != "" && password != "")
            {
                bool resultado = (bool)cn.EjecutarSelect("SELECT Usuario FROM Usuarios WHERE Usuario = '" + usuario + "' AND Password = '" + password + "'");

                if (resultado == true)
                {
                    int Id = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Usuarios WHERE Usuario = '" + usuario + "' AND Password = '" + password + "'", 1));

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
                    fp.ShowDialog();

                    this.Close();
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
                                        _SetnoCheckStock };

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
                                            _pathnoCheckStock };

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

            // descomentar la linea de abajo en caso de hacer el Release
            //Properties.Settings.Default.rutaDirectorio = Properties.Settings.Default.pathPUDVE;

            //MessageBox.Show("Path: " + Properties.Settings.Default.rutaDirectorio, "Path...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            // descomentar la Linea de abajo cuando estemos en Debug
            Properties.Settings.Default.rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));

            RevisarTablas();

            txtUsuario.Text = Properties.Settings.Default.Usuario;
            txtPassword.Text = Properties.Settings.Default.Password;
        }

        private void RevisarTablas()
        {
            // 01 Anticipos
            #region TablaAnticipos
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
            #endregion TablaAnticipos
            // 02 Caja
            #region TablaCaja
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
            #endregion TablaCaja
            // 03 CatalogoUnidadesMedida
            #region TablaCatalogoUnidadesMedida
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
            #endregion TablaCatalogoUnidadesMedida
            // 04 CodigoBarrasExtras
            #region TablaCodigoBarrasExtras
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
            #endregion TablaCodigoBarrasExtras
            // 05 DescuentoCLiente
            #region TablaDescuentoCliente
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
            #endregion TablaDescuentoCliente
            // 06 DescuentoMayoreo
            #region TablaDescuentoMayoreo
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
            #endregion TablaDescuentoMayoreo
            // 07 DetallesFacturacionProductos
            #region TablaDetallesFacturacionProductos
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
            #endregion TablaDetallesFacturacionProductos
            // 08 DetallesProductos
            #region TablaDetallesProducto
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
            #endregion TablaDetallesProducto
            // 09 Empresas
            #region TablaEmpresas
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
            #endregion TablaEmpresas
            // 10 HistorialCompras
            #region TablaHistorialCompras
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
            #endregion TablaHistorialCompras
            // 11 HistorialModificacionRecordProduct
            #region TablaHistorialModificacionRecordProduct
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
            #endregion TablaHistorialModificacionRecordProduct
            // 12 ProductoRelacionadoXML
            #region TablaProductoRelacionadoXML
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
            #endregion TablaProductoRelacionadoXML
            // 13 Productos
            #region TablaProductos
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
            #endregion TablaProductos
            // 14 ProductosDeServicios
            #region TablaProductosDeServicios
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
            #endregion TablaProductosDeServicios
            // 15 ProductosVenta
            #region TablaProductosVenta
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
            #endregion TablaProductosVenta
            // 16 Proveedores
            #region TablaProveedores
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
            #endregion TablaProductosVenta
            // 17 RegimenDeUsuarios
            #region TablaRegimenDeUsuarios
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
            #endregion TablaRegimenDeUsuarios
            // 18 RegimenFiscal
            #region TablaRegimenFiscal
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

            }
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
            #endregion TablaRegimenFiscal
            // 19 Usuarios
            #region TablaUsuarios
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
            #endregion TablaUsuarios
            // 20 Ventas
            #region TablaVentas
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
            #endregion TablaVentas
            // 21 Clientes
            #region TablaClientes
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
            #endregion TablaClientes
            // 22 RevisarInventario
            #region TablaRevisarInventario
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
            #endregion TablaRevisarInventario
            // 23 DetallesVenta
            #region TablaDetallesVenta
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
            #endregion TablaDetallesVenta
            // 24 Abonos
            #region TablaAbonos
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
            #endregion TablaAbonos
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

        private void checkBoxRecordarDatos_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtUsuario_KeyUp(object sender, KeyEventArgs e)
        {
            txtUsuario.CharacterCasing = CharacterCasing.Upper;
        }
    }
}
