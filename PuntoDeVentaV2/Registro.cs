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
    public partial class Registro : Form
    {
        Conexion cn = new Conexion();

        public Registro()
        {
            InitializeComponent();
        }

        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string password = txtPassword.Text;
            string password2 = txtPassword2.Text;
            string razonSocial = txtRazonSocial.Text;
            string email = txtEmail.Text;
            string telefono = txtTelefono.Text;

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


            string consulta = "INSERT INTO Usuarios (Usuario, Password, RazonSocial, Telefono, Email) VALUES ('"+usuario+"', '"+password+"', '"+razonSocial+"', '"+telefono+"', '"+email+"')";

            int respuesta = cn.EjecutarConsulta(consulta);

            if (respuesta > 0)
            {
                FormPrincipal fp = new FormPrincipal();

                this.Hide();

                fp.ShowDialog();

                this.Close();
            }
            else
            {
                txtMensajeError.Text = "Ocurrió un error al intentar hacer el registro";
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
    }
}
