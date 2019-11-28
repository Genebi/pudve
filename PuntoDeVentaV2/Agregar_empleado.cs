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
    public partial class Agregar_empleado : Form
    {

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();



        public Agregar_empleado()
        {
            InitializeComponent();
        }

        private void solo_letras_digitos(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) | Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void muestra_usuarioc(object sender, KeyEventArgs e)
        {
            if (txt_usuario.Text != "")
            {
                lb_usuario.Visible = true;
                lb_usuario_completo.Visible = true;

                string n_completo = FormPrincipal.userNickName + "@" + txt_usuario.Text;
                lb_usuario_completo.Text = n_completo;
            }
            else
            {
                lb_usuario.Visible = false;
                lb_usuario_completo.Visible = false;
                lb_usuario_completo.Text = string.Empty;
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            string mnsj_er = "";
            string error = "0";

            string val_campos = valida_campos();
            string[] m_e = val_campos.Split('-');

            mnsj_er = m_e[0];
            error = m_e[1];

            if (error == "0")
            {
                string[] datos = new string[]
                {
                    FormPrincipal.userID.ToString(), txt_nombre.Text, lb_usuario_completo.Text, txt_conttraseña.Text
                };

                int r = cn.EjecutarConsulta(cs.guardar_editar_empleado(datos, 1));

                if(r > 0)
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(mnsj_er, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public string valida_campos()
        {
            string mnsj = "";
            int error = 0;


            if (txt_conttraseña.Text.Trim() == "")
            {
                error = 1;
                mnsj = "La contraseña es obligatoria.";
            }
            if (txt_usuario.Text.Trim() == "")
            {
                error = 1;
                mnsj = "El usuario es obligatorio";
            }
            else
            {
                bool existe = (bool)cn.EjecutarSelect($"SELECT * FROM Empleados WHERE usuario='{lb_usuario_completo.Text}' AND IDUsuario='{FormPrincipal.userID}'");

                if (existe == true)
                {
                    error = 1;
                    mnsj =  "Ya existe ese nombre de usuario, elegir otro.";
                }
            }
            if (txt_nombre.Text.Trim() == "")
            {
                error = 1;
                mnsj = "El nombre es obligatorio.";
            }


            return mnsj + "-" + error;
        }

        private void verifica_usuario_empleado(object sender, EventArgs e)
        {
            bool existe = (bool)cn.EjecutarSelect($"SELECT * FROM Empleados WHERE usuario='{lb_usuario_completo.Text}' AND IDUsuario='{FormPrincipal.userID}'");

            if(existe == true)
            {
                MessageBox.Show("Ya existe ese nombre de usuario, elegir otro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
