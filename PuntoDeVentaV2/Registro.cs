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
using System.Management;
using System.Net.NetworkInformation;
using System.Net.Mail;
using System.Threading;
using System.Security.Cryptography;

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

        // Obtiene el numero serie de la tarjeta madre para guardarse al momento del registro
        public static string TarjetaMadreID()
        {
            string mbInfo = string.Empty;
            ManagementScope scope = new ManagementScope("\\\\" + Environment.MachineName + "\\root\\cimv2");
            scope.Connect();
            ManagementObject wmiClass = new ManagementObject(scope, new ManagementPath("Win32_BaseBoard.Tag=\"Base Board\""), new ObjectGetOptions());

            foreach (PropertyData propData in wmiClass.Properties)
            {
                if (propData.Name == "SerialNumber")
                {
                    mbInfo = Convert.ToString(propData.Value);
                }    
            }

            return mbInfo;
        }

        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            if (ConectadoInternet())
            {
                MySqlConnection conexion = new MySqlConnection();

                conexion.ConnectionString = "server=74.208.135.60;database=pudve;uid=pudvesoftware;pwd=Steroids12;";

                string usuario = txtUsuario.Text;
                string password = txtPassword.Text;
                string password2 = txtPassword2.Text;
                string razonSocial = txtRazonSocial.Text;
                string email = txtEmail.Text.Trim().Replace(" ","");
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
                        txtMensajeError.Text = string.Empty;

                        string[] datos = new string[] { usuario, password, password2, razonSocial, email, telefono };

                        string respuestaValidacion = ValidarFormulario(datos);

                        if (respuestaValidacion != "")
                        {
                            txtMensajeError.Text = respuestaValidacion;
                            return;
                        }
                        else
                        {
                            txtMensajeError.Text = string.Empty;
                        }

                        string licencia = GenerarLicencia();

                        
                        //Consulta de MySQL local 
                        string consulta = "INSERT INTO Usuarios (Usuario, Password, RazonSocial, Telefono, Email, Licencia)";
                               consulta += $"VALUES ('{usuario}', '{password}', '{razonSocial}', '{telefono}', '{email}', '{licencia}')";


                        int respuesta = cn.EjecutarConsulta(consulta, regresarID: true);

                        

                        //var consultaIdUsuario = cn.CargarDatos($"SELECT * FROM usuarios ORDER BY ID DESC LIMIT 1");

                        //foreach (DataRow IdUser in consultaIdUsuario.Rows)
                        //{
                        //    cn.EjecutarConsulta($"UPDATE editarticket SET MensajeTicket = '1', Usuario = '1', Direccion = '1', ColyCP = '1', RFC = '1', Correo = '1', Telefono = '1', NombreC = '1', DomicilioC = '1', RFCC = '1', CorreoC = '1', TelefonoC = '1', ColyCPC = '1', FormaPagoC = '1', logo = '1' WHERE ID = '{IdUser[0].ToString()}'");
                        //}

                        //Consulta de MySQL
                        registrar.CommandText = $"INSERT INTO Usuarios (usuario, password, razonSocial, email, telefono, numeroSerie, idLocal, licencia) VALUES ('{usuario}', '{password}', '{razonSocial}', '{email}', '{telefono}', '{TarjetaMadreID()}', '{respuesta}', '{licencia}')";
                        int resultado = registrar.ExecuteNonQuery();

                        if (respuesta > 0 && resultado > 0)
                        {
                            // Datos para el envio del correo de registro
                            Thread notificacion = new Thread(
                                () => EnviarEmail(new string[] { usuario, password, email })
                            );

                            notificacion.Start();

                            // Datos para el inicio de sesion
                            int Id = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Usuarios WHERE Usuario = '{usuario}' AND Password = '{password}'", 1));

                            //Realizar una operacion de corte de caja para cuando sea una ceunta nueva 
                            cn.EjecutarConsulta($"INSERT INTO Caja (Operacion, Cantidad, Saldo, Concepto, FechaOperacion, IDUsuario, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, Anticipo ) VALUES('corte', '0.00', '0.00', '', '{fechaCreacion}', '{Id}', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00' )");

                            //Se agrega un segundo para las consultas que se hacen en cuentas nuevas
                            DateTime dt = Convert.ToDateTime(fechaCreacion);
                            var fechaFinal = dt.AddSeconds(1).ToString("yyyy-MM-dd HH:mm:ss");

                            //Realizar una operacion de corte de caja para cuando sea una ceunta nueva 
                            cn.EjecutarConsulta($"INSERT INTO Caja (Operacion, Cantidad, Saldo, Concepto, FechaOperacion, IDUsuario, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, Anticipo ) VALUES('venta', '0.00', '0.00', '', '{fechaFinal}', '{Id}', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00' )");

                            ////Realizar una operacion de retiro de caja para cuando sea una ceunta nueva 
                            cn.EjecutarConsulta($"INSERT INTO Caja (Operacion, Cantidad, Saldo, Concepto, FechaOperacion, IDUsuario, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, Anticipo ) VALUES('retiro', '0.00', '0.00', '', '{fechaFinal}', '{Id}', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00' )");

                            FormPrincipal fp = new FormPrincipal();

                            UsuariosN(usuario);

                            ActualizarFechaFinLicencia(usuario);

                            Hide();
                            fp.IdUsuario = Id;
                            fp.nickUsuario = usuario;
                            fp.passwordUsuario = password;
                            fp.ShowDialog();
                            Close();

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
                    MessageBox.Show(ex.Message, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No hay conexión a Internet", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarFechaFinLicencia(string usuario)
        {
            // Verificar que sea cuenta principal y no subusuario
            if (usuario.Contains('@'))
            {
                string[] auxiliar = usuario.Split('@');

                usuario = auxiliar[0];
            }

            MySqlConnection conexion = new MySqlConnection();

            conexion.ConnectionString = "server=74.208.135.60;database=pudve;uid=pudvesoftware;pwd=Steroids12;";

            try
            {
                conexion.Open();

                MySqlCommand consultar = conexion.CreateCommand();
                consultar.CommandText = $"SELECT fechaFinLicencia FROM usuarios WHERE usuario = '{usuario}'";
                MySqlDataReader dr = consultar.ExecuteReader();

                if (dr.Read())
                {
                    DateTime fechaFin = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("fechaFinLicencia"))).AddYears(1);

                    dr.Close();

                    // Actualizar fecha de fin de la licencia
                    MySqlCommand actualizarFinLicencia = conexion.CreateCommand();

                    actualizarFinLicencia.CommandText = $"UPDATE usuarios SET fechaFinLicencia = '{fechaFin.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE usuario = '{usuario}'";

                    actualizarFinLicencia.ExecuteNonQuery();
                }

                dr.Close();
                conexion.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        private string GenerarLicencia(int tipo = 1)
        {
            string licencia = string.Empty;

            // Licencia cliente
            if (tipo == 1)
            {
                licencia = "PVLC";
            }

            // Licencia servidor
            if (tipo == 2)
            {
                licencia = "PVLS";
            }

            string fecha = DateTime.Now.ToString("yyyyMmddHHmmss");
            byte[] hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(fecha));
            string resultado = Convert.ToBase64String(hash).ToUpper();

            resultado = resultado.Replace('+', 'X');
            resultado = resultado.Replace('=', 'Y');
            resultado = resultado.Replace('/', 'Z');

            string primerRango = resultado.Substring(0, 4);
            string segundoRango = resultado.Substring(4, 4);
            string tercerRango = resultado.Substring(8, 4);
            string cuartoRango = resultado.Substring(12, 4);

            licencia = $"{licencia}-{primerRango}-{segundoRango}-{tercerRango}-{cuartoRango}";

            return licencia;
        }

        public void UsuariosN(string usuario)
        {
            // 0 =  Usuarios sin clave interna
            // 1 =  Usuarios con clave interna

            var query = cn.CargarDatos($"SELECT ID FROM Usuarios ORDER BY ID DESC LIMIT 1");

            var dato = query.Rows[0]["ID"].ToString();

            var validarClaveInterna = cn.EjecutarConsulta($"UPDATE Usuarios SET SinClaveInterna = '0' WHERE ID = '{dato}'"); 

        }

        private bool VerificarUsuario(string usuario)
        {
            bool respuesta = (bool)cn.EjecutarSelect($"SELECT Usuario FROM Usuarios WHERE Usuario = '{usuario}'");

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
            if (string.IsNullOrWhiteSpace(datos[0]))
            {
                return "Ingrese un nombre de usuario";
            }

            //Verificar si el password esta vacio
            if (string.IsNullOrWhiteSpace(datos[1]))
            {
                return "La contraseña es obligatoria";
            }

            //Verificar si los password coinciden
            if (!datos[1].Equals(datos[2]))
            {
                return "Las contraseñas no coinciden";
            }

            //Verificar la razon social
            if (string.IsNullOrWhiteSpace(datos[3]))
            {
                return "Ingrese la razón social";
            }

            //Verificar que el email no este vacio
            if (string.IsNullOrWhiteSpace(datos[4]))
            {
                return "El email es obligatorio";
            }

            //Verificar que el email tenga un formato valido
            if (ValidarEmail(datos[4]) == false)
            {
                return "El formato de email no es valido";
            }

            //Validar el numero de telefono
            if (string.IsNullOrWhiteSpace(datos[5]))
            {
                return "Ingrese un número de teléfono";
            }

            return "";
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            char chr = e.KeyChar;

            if (!char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
            }
        }

        private void EnviarEmail(string[] datos)
        {
            var usuario = datos[0].Trim();
            var password = datos[1].Trim();
            var email = datos[2].Trim();

            var mensajeHTML = string.Empty;

            try
            {
                mensajeHTML += "<div style='text-align: center;'>";
                mensajeHTML += "    <h2>INFORMACIÓN DE REGISTRO</h2>";
                mensajeHTML += "    <hr>";
                mensajeHTML += "    <h3>USUARIO: <span style='color: red;'>"+ usuario +"</span></h3>";
                mensajeHTML += "    <h3>PASSWORD: <span style='color: red;'>"+ password +"</span>";
                mensajeHTML += "</div>";

                MailMessage mensaje = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                mensaje.From = new MailAddress("pudve.contacto@gmail.com", "PUDVE");
                mensaje.To.Add(new MailAddress(email));
                mensaje.Subject = "Información de registro PUDVE";
                mensaje.IsBodyHtml = true; // para hacer el cuerpo del mensaje como html 
                mensaje.Body = mensajeHTML;

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; // para host gmail
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("pudve.contacto@gmail.com", "grtpoxrdmngbozwm");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mensaje);
            }
            catch (Exception ex)
            {
                // Se comento el mensaje de exception ya que el usuario no sabe que se le enviara correo
                // y que no aparezca el messagebox
                //MessageBox.Show(ex.Message.ToString(), "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = (e.KeyChar == (char)Keys.Space);

            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Solo se permiten letras y numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
                return;
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Login FrmLogin = new Login();
            FrmLogin.Show();
            this.Close();
        }
    }
}
