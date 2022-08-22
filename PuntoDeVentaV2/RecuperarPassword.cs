using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class RecuperarPassword : Form
    {
        MetodosBusquedas mb = new MetodosBusquedas();

        public RecuperarPassword()
        {
            InitializeComponent();
        }

        private void btnRecuperar_Click(object sender, EventArgs e)
        {
            var usuario = txtUsuario.Text.Trim();

            if (string.IsNullOrWhiteSpace(usuario))
            {
                MessageBox.Show("El nick de usuario es obligatorio", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            var datos = mb.RecuperarPassword(usuario);

            if (datos.Length > 0)
            {
                var email = datos[1];

                if (!string.IsNullOrWhiteSpace(email))
                {
                    var razon = datos[0];
                    var password = datos[2];
                    var asunto = "Contraseña cuenta PUDVE";

                    var html = $@"
                        <h3>HOLA, {razon}!</h3>
                        <p>Has solicitado recuperar la contraseña de tu cuenta <b>PUDVE</b>, tu contraseña es la
                        siguiente: <span style='color: red;'>{password}</span><br>te recomendamos mantenerla segura y eliminar
                        este mensaje una vez haya sido leido.</p>";

                    Thread notificacion = new Thread(
                        () => Utilidades.EnviarEmail(html, asunto, email)
                    );

                    notificacion.Start();

                    MessageBox.Show("Tu contraseña se ha enviado a tu correo electrónico", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }
            }
            else
            {
                MessageBox.Show("No se encontro ningun usuario registrado con ese nombre", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Close();
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnRecuperar.PerformClick();
            }
        }
    }
}
