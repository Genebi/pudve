using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class NvaEmpresa : Form
    {
        // variable de text para poder dirigirnos a la carpeta principal para
        // poder jalar las imagenes o cualquier cosa que tengamos hay en ese directorio
        public string rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

        // hacemos la conexion para la DB
        Conexion cn = new Conexion();

        // variable para almacenar el ID del Usuario principal
        string idUsuario;

        // variables ara almacenar los valores de los textBox
        string usuario; string password1;
        string password2; string razonSocial;
        string email; string telefono;

        // variables para control de los eventos realizados
        int user,pass,agregado;
        string buscar,mensaje,insertar;
        DataTable dt;
        DataRow row;

        // objeto de tipo FileStream para poder manejar la imagen
        FileStream File;

        public void verificaUsuario()
        {
            user = 0;
            // String para hacer la consulta filtrada sobre
            // el usuario que inicia la sesion
            buscar = $"SELECT u.Usuario AS 'Usuario', u.NombreCompleto AS 'Nombre Comercial', u.RFC AS 'R.F.C.' FROM Usuarios u WHERE u.Usuario LIKE '{usuario}' AND u.Referencia_ID = '{FormPrincipal.TempUserID}'";

            // almacenamos el resultado de la Funcion CargarDatos
            // que esta en la calse Consultas
            dt = cn.GetEmpresas(buscar);

            // almacenamos el resultado de la consulta en el dt
            if (dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
                user = 1;
            }
            if (dt.Rows.Count < 0)
            {
                user = 0;
            }

            dt.Clear();
        }

        // metodo para poder agregar la nueva empresa
        public void registrarEmpresa()
        {
            // variable para poder saber si se agrego o no
            agregado = 0;
            // string para hacer el Query a la consulta de SQLite3
            insertar = $"INSERT INTO Usuarios(Usuario,Password,RazonSocial,Telefono,Email,Referencia_ID)VALUES('{usuario}','{password1}','{razonSocial}','{telefono}','{email}','{FormPrincipal.TempUserID}')";
            // segun el resultado del Query se le asigna al agregado
            agregado = cn.EjecutarConsulta(insertar);
        }

        public void verificarPassWord()
        {
            // iniciamos la variable que es para comprobar
            // si esta bien o no el password
            pass = 0;
            // si el TxtPassword1 no esta en blanco
            if (txtPassword1.Text != "")
            {
                // si el TxtPassword2 no esta en blanco
                if (txtPassword2.Text != "")
                {
                    // si los dos campos de password son iguales
                    if (txtPassword1.Text == txtPassword2.Text)
                    {
                        // si todo esta lleno los campos
                        // y los dos campos coinciden
                        // se le da el valor de 0 a la variable
                        pass = 0;
                    }
                    // si no son iguales los dos campos de password
                    else
                    {
                        // la variable le damos el valor de 1
                        pass = 3;
                        // enviamos un mensaje segun el error
                        mensaje = "Los campos de Contraseña no coinciden";
                    }
                }
                // si esta en blanco el TxtPassword2
                else
                {
                    // la variable le damos el valor de 1
                    pass = 2;
                    // enviamos un mensaje segun el error
                    mensaje = "favor de no dejar vacio el campo de Confirma Contraseña";
                }
            }
            // si esta en blanco el TxtPassword1
            else
            {
                // la variable le damos el valor de 1
                pass = 1;
                // enviamos un mensaje segun el error
                mensaje = "favor de no dejar vacio el campo de Contraseña";
            }
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
            
            // mandamos llamar la funcion de verificarUsuario()
            verificaUsuario();

            // si la variable user es igual a 1 tenemos algun error
            // y arrojaria un mensaje de error
            if (user == 1)
            {
                MessageBox.Show("El usuario ya existe \nfavor ingresar nuevo Nombre de usuario", "Error en los datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Focus();
            }
            
            // mandamos llamar la funsion de verficarPassWord()
            verificarPassWord();

            // y arrojaria un mensaje de error
            // si dejamos limpio el campo Contraseña
            if (pass == 1)
            {
                MessageBox.Show(mensaje + "\nFavor de Verificar Gracias...", "Algo salio Mal...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword1.Focus();
            }
            // si dejamos limpio el campo Confirmar Contraseña
            if (pass == 2)
            {
                MessageBox.Show(mensaje + "\nFavor de Verificar Gracias...", "Algo salio Mal...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword2.Focus();
            }
            // si ambos campos no son iguales
            if (pass == 3)
            {
                MessageBox.Show(mensaje + "\nFavor de Verificar Gracias...", "Algo salio Mal...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // si las variables de control si coinciden las dos en el valor 0 
            if ((user == 0) && (pass == 0))
            {
                // llamamos al metodo registrar
                registrarEmpresa();
                // segun lo que regrese el metodo registrarEmpresa
                // sea mayor a 0 entonces mostramos mensaje y cerramos la ventana
                if (agregado > 0)
                {
                    MessageBox.Show("El registro se agrego exitosamente", "Datos Insertados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            // si las variables de control no coinciden las dos en el valor 0 
            // mostramos mensaje y enviamos el cursor al TxtUsuario
            else
            {
                MessageBox.Show("Ocurrio un error durante \nel resgitro de la Empresa", "Error en el Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Focus();
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
                    txtEmail.Focus();
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
                txtTelefono.Focus();
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
            // usamos la variable File para abrir el archivo de imagen, poder leerlo y agregarlo al boton
            // despues de agregado se libera la imagen para su posterior manipulacion si asi fuera
            using (File = new FileStream(rutaDirectorio + @"\icon\black\save.png", FileMode.Open, FileAccess.Read))
            {
                // Asignamos la imagen al BtnRegistrar
                btnRegistrar.Image = Image.FromStream(File);
            }
        }
    }
}
