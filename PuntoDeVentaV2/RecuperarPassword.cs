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
                    
                    var html = $@"";

                    //Utilidades.EnviarEmail();
                }
            }
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
