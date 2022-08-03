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
    public partial class Autoriza_conpassword : Form
    {
        Conexion cn = new Conexion();

        public Autoriza_conpassword()
        {
            InitializeComponent();
        }

        private void btn_aceptar_Click(object sender, EventArgs e) 
        {
            // Comprueba que el password sea el correcto
            bool password_correcto = (bool) cn.EjecutarSelect($"SELECT * FROM usuarios WHERE ID ='{FormPrincipal.userID}' AND Password='{txt_password.Text}'");

            if(password_correcto == true)
            {
                MisDatos.autorizacion_correcta = true;

                this.Dispose();
            }
            else
            {
                MisDatos.autorizacion_correcta = false;
                txt_password.Text = string.Empty;

                MessageBox.Show("La contraseña es incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
    }
}
