using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class Login : Form
    {

        Conexion cn = new Conexion();

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
            string usuario = txtUsuario.Text;
            string password = txtPassword.Text;

            if (usuario != "" && password != "")
            {
                bool resultado = (bool)cn.EjecutarSelect("SELECT Usuario FROM Usuarios WHERE Usuario = '" + usuario + "' AND Password = '" + password + "'");

                if (resultado == true)
                {
                    int Id = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Usuarios WHERE Usuario = '" + usuario + "' AND Password = '" + password + "'", 1));

                    FormPrincipal fp = new FormPrincipal();

                    // validacion para recordar los datos de Login
                    if (checkBoxRecordarDatos.Checked == true)
                    {
                        Properties.Settings.Default.Usuario = usuario;
                        Properties.Settings.Default.Password = password;
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.Reload();
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
            txtUsuario.Text = Properties.Settings.Default.Usuario;
            txtPassword.Text = Properties.Settings.Default.Password;
        }

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Usuario = "";
            Properties.Settings.Default.Password = "";
            txtUsuario.Text = "";
            txtPassword.Text = "";
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
    }
}
