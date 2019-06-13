using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PuntoDeVentaV2
{
    public partial class Registro : Form
    {
        Conexion cn = new Conexion();

        public Registro()
        {
            InitializeComponent();
        }

        //Se necesita para saber si la computadora tiene conexion a internet
        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int Descripcion, int ValorReservado);

        public static bool ConectadoInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            if (ConectadoInternet())
            {
                MySqlConnection conexion = new MySqlConnection();

                conexion.ConnectionString = "server=208.109.252.94;database=PUDVE;uid=pudvesoftware;pwd=Steroids12;";

                string usuario = txtUsuario.Text;
                string password = txtPassword.Text;
                string password2 = txtPassword2.Text;
                string razonSocial = txtRazonSocial.Text;
                string email = txtEmail.Text;
                string telefono = txtTelefono.Text;
                string fechaCreacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                try
                {
                    conexion.Open();
                    MySqlCommand consultar = conexion.CreateCommand();
                    MySqlCommand registrar = conexion.CreateCommand();

                    //Verificamos si el usuario que se quiere registrar ya se encuentra registrado en la base de datos online
                    consultar.CommandText = $"SELECT * FROM Usuarios WHERE usuario = '{usuario}'";
                    MySqlDataReader dr = consultar.ExecuteReader();

                    if (dr.Read())
                    {
                        txtMensajeError.Text = "Este usuario ya se encuentra registrado";
                    }
                    else
                    {
                        //Cerramos el DataReader en caso que no entre en el IF
                        dr.Close();

                        //Limpiamos en caso que haya dado error si el usuario ya estaba registrado
                        txtMensajeError.Text = "";

                        string[] datos = new string[] { usuario, password, password2, razonSocial, email, telefono };

                        string respuestaValidacion = ValidarFormulario(datos);

                        if (respuestaValidacion != "")
                        {
                            txtMensajeError.Text = respuestaValidacion;
                            return;
                        }
                        else
                        {
                            txtMensajeError.Text = "";
                        }

                        //Consulta de MySQL
                        registrar.CommandText = $"INSERT INTO Usuarios (usuario, password, razonSocial, email, telefono, fechaCreacion) VALUES ('{usuario}', '{password}', '{razonSocial}', '{telefono}', '{email}', '{fechaCreacion}')";
                        int resultado = registrar.ExecuteNonQuery();

                        //Consulta de SQLite
                        string consulta = "INSERT INTO Usuarios (Usuario, Password, RazonSocial, Telefono, Email)";
                               consulta += $"VALUES ('{usuario}', '{password}', '{razonSocial}', '{telefono}', '{email}')";

                        int respuesta = cn.EjecutarConsulta(consulta);

                        if (respuesta > 0 && resultado > 0)
                        {
                            FormPrincipal fp = new FormPrincipal();

                            this.Hide();

                            fp.nickUsuario = usuario;
                            fp.passwordUsuario = password;
                            fp.ShowDialog();

                            this.Close();
                        }
                        else
                        {
                            txtMensajeError.Text = "Ocurrió un error al intentar hacer el registro";
                        }
                    }

                    //Cerramos la conexion de MySQL
                    dr.Close();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No hay conexión a Internet", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool VerificarUsuario(string usuario)
        {
            string consulta = "SELECT Usuario FROM Usuarios WHERE Usuario = '"+ usuario +"'";
            bool respuesta = (bool)cn.EjecutarSelect(consulta);
            return respuesta;
        }

        private bool ValidarEmail(string email)
        {
            try
            {
                var direccion = new System.Net.Mail.MailAddress(email);

                return direccion.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private string ValidarFormulario(string [] datos)
        {
            //Verificar si el usuario existe
            if (VerificarUsuario(datos[0]))
            {
                return "Este usuario no se encuentra disponible";
            }
            
            //Verificar si el usuario esta vacio
            if (String.IsNullOrWhiteSpace(datos[0]))
            {
                return "Ingrese un nombre de usuario";
            }

            //Verificar si el password esta vacio
            if (String.IsNullOrWhiteSpace(datos[1]))
            {
                return "La contraseña es obligatoria";
            }

            //Verificar si los password coinciden
            if (!datos[1].Equals(datos[2]))
            {
                return "Las contraseñas no coinciden";
            }

            //Verificar la razon social
            if (String.IsNullOrWhiteSpace(datos[3]))
            {
                return "Ingrese la razón social";
            }

            //Verificar que el email no este vacio
            if (String.IsNullOrWhiteSpace(datos[4]))
            {
                return "El email es obligatorio";
            }

            //Verificar que el email tenga un formato valido
            if (ValidarEmail(datos[4]) == false)
            {
                return "El formato de email no es valido";
            }

            //Validar el numero de telefono
            if (String.IsNullOrWhiteSpace(datos[5]))
            {
                return "Ingrese un número de teléfono";
            }

            return "";
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
            }
        }

        private void txtUsuario_KeyUp(object sender, KeyEventArgs e)
        {
            txtUsuario.CharacterCasing = CharacterCasing.Upper;
        }
    }
}
