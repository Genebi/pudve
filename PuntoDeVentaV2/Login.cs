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

        private void Login_Load(object sender, EventArgs e)
        {
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

            txtUsuario.Text = Properties.Settings.Default.Usuario;
            txtPassword.Text = Properties.Settings.Default.Password;
        }

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            // limpiamos los datos de las variables del sistema para poder olvidar los datos de inicio de login
            BorrarDatosLogin();
        }
    }
}
