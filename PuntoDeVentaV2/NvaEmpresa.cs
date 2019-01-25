using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class NvaEmpresa : Form
    {
        //
        //******************* TODO **************************
        //

        /****************************************************
        *   Verificar la contraseña que sean iguales        *
        *   hacer el Insert en la base de datos             * 
        *****************************************************/

        // hacemos la conexion para la DB
        Conexion cn = new Conexion();

        // variable para almacenar el ID del Usuario principal
        string idUsuario;

        // variables ara almacenar los valores de los textBox
        string usuario; string password1;
        string password2; string razonSocial;
        string email; string telefono;

        int user;
        string buscar;
        DataTable dt;
        DataRow row;

        public void verificaUsuario()
        {
            user = 0;
            // String para hacer la consulta filtrada sobre
            // el usuario que inicia la sesion
            buscar = @"SELECT emp.ID_Empresa AS 'Identificador de la Empresa', emp.Usuario AS 'Usuario', emp.NombreCompleto AS 'Nombre Comercial', emp.RFC AS 'R.F.C.' 
                       FROM Usuarios u
                       LEFT JOIN Empresas emp
                       ON u.ID = emp.ID_Usuarios
                       WHERE emp.Usuario LIKE '" + usuario + "' AND emp.ID_Usuarios = '" + idUsuario + "'";

            // almacenamos el resultado de la Funcion CargarDatos
            // que esta en la calse Consultas
            dt = cn.GetEmpresas(buscar);

            // almacenamos el resultado de la consulta en el dt
            if (dt.Rows.Count == 1 || dt.Rows.Count >= 1)
            {
                row = dt.Rows[0];
                user = 1;
            }
            if (dt.Rows.Count == 0 || dt.Rows.Count <= 0)
            {
                user = 0;
            }

            dt.Clear();
        }

        public NvaEmpresa()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // almacenamos los valores en las variables
            // segun sea el caso en los textBox correspondientes
            usuario = txtUsuario.Text;
            password1 = txtPassword1.Text;
            password2 = txtPassword2.Text;
            email = txtEmail.Text;
            razonSocial = txtRazonSocial.Text;
            telefono = txtTelefono.Text;

            verificaUsuario();

            if (user == 1)
            {
                MessageBox.Show("El usuario ya existe \nfavor ingresar nuevo Nombre de usuario", "Error en los datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (user == 0)
            {
                MessageBox.Show("El usuario esta disponible", "Aceptado los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (!reg.IsMatch(txtEmail.Text))
                {
                    MessageBox.Show("Formato no valido de Email", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si No Es Numero  
            if (!(char.IsNumber(e.KeyChar)) &&
                // y Si No es Backspace(Borrar)
                (e.KeyChar != (char)Keys.Back) &&
                // y si no es Enter(Entrar) 
                (e.KeyChar != (char)Keys.Enter) &&
                // y si no es Return(Entrar o Intro del Teclado Numerico) 
                (e.KeyChar != (char)Keys.Return) &&
                // y si no es Delete(Del o Suprimir, Supr)  
                (e.KeyChar != (char)Keys.Delete))
            {
                // Entonces 
                // Lanzamos Mensaje diciendo que solo permite numeros 
                MessageBox.Show("Solo se permiten numeros en el Campo Telefono",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // Habilitamos el Handled = true para que no escriba nada si no son las teclas validadas 
                e.Handled = true;
                //Retornamos return; 
            }
            // entonces si las teclas son las permitidas Validamos
            // si se presiona Enter 
            else if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                // mandamos un Tab para ir al siguiene campo 
            }
            // si se presiona Return (Intro del Teclado Numerico) 
            else if (e.KeyChar == (char)Keys.Return)
            {
                SendKeys.Send("{TAB}");
                // mandamos un Tab para ir al siguiene campo 
            }
        }

        private void NvaEmpresa_Load(object sender, EventArgs e)
        {
            // tomamos el valor de la ID del Usuario principal
            idUsuario = FormPrincipal.userID.ToString();
        }
    }
}
