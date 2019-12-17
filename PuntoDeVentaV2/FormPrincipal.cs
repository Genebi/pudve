using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PuntoDeVentaV2
{
    public partial class FormPrincipal : Form
    {
        Conexion cn = new Conexion();
        MetodosGenerales mg = new MetodosGenerales();
        MetodosBusquedas mb = new MetodosBusquedas();

        public static string[] datosUsuario = new string[] { };

        // declaramos la variable que se pasara entre los dos formularios
        // FormPrincipal y MisDatos
        public static int userID;
        public static string userNickName;
        public static string userPass;
        public static int TempUserID;
        public static string TempUserNickName;
        public static string TempUserPass;
        public static string FechaCheckStock;
        public static long NoRegCheckStock;

        // Variables donde guarda los permisos del empleado.

        private int anticipos = 1;
        private int caja = 1;
        private int clientes = 1;
        private int config = 1;
        private int empleados = 1;
        private int empresas = 1;
        private int facturas = 1;
        private int inventarios = 1;
        private int misdatos = 1;
        private int productos = 1;
        private int proveedores = 1;
        private int reportes = 1;
        private int servicios = 1;
        private int ventas = 1;
        
        // variables para poder tomar los datos que se pasaron del login a esta forma
        public int IdUsuario { get; set; }
        public string nickUsuario { get; set; }
        public string passwordUsuario { get; set; }
        public string DateCheckStock { get; set; }
        public long NumberRegCheckStock { get; set; }

        // variables usasadas para que sea estatico los valores y asi en empresas
        // se agrege tambien la cuenta principal y poder hacer que regresemos a ella
        public int TempIdUsuario { get; set; }
        public string TempNickUsr { get; set; }
        public string TempPassUsr { get; set; }

        const string ficheroNumCheck = @"\PUDVE\settings\noCheckStock\checkStock.txt";  // directorio donde esta el archivo de numero 
        const string ficheroDateCheck = @"\PUDVE\settings\noCheckStock\checkDateStock.txt";  // directorio donde esta el archivo de fecha
        string Contenido;                                                       // para obtener el numero que tiene el codigo de barras en el arhivo

        string FechaFinal;

        // funcion para que podamos recargar variables desde otro formulario
        public void recargarDatos()
        {
            userID = IdUsuario; Console.WriteLine("userID"+userID);
            userNickName = nickUsuario; Console.WriteLine("userNickName" + userNickName);
            userPass = passwordUsuario;
            FechaCheckStock = DateCheckStock;
            NoRegCheckStock = NumberRegCheckStock;
        }

        public FormPrincipal()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            //Se crea el directorio principal para almacenar todos los archivos generados y carpetas
            Directory.CreateDirectory(@"C:\Archivos PUDVE");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\Reportes");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\Reportes\Historial");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\Reportes\Caja");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\MisDatos");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\MisDatos\Usuarios");
            Directory.CreateDirectory(@"C:\Archivos PUDVE\MisDatos\CFDI");

            obtenerDatosCheckStock();

            recargarDatos();

            TempUserID = TempIdUsuario;
            TempUserNickName = TempNickUsr;
            TempUserPass = TempPassUsr;

            ObtenerDatosUsuario(userID);
            

            this.Text = "PUDVE - Punto de Venta | " + userNickName;


            // Obtiene ID del empleado, y los permisos que tenga asignados.

            string formato_usuario = "^[A-Z&Ñ]+@[A-Z&Ñ0-9]+$";

            Regex exp = new Regex(formato_usuario);

            if (exp.IsMatch(userNickName)) // Es un empleado
            {
                int id_empleado = obtener_id_empleado(userID);

                var datos_per = mb.obtener_permisos_empleado(id_empleado, userID);
                
                permisos_empleado(datos_per);
            }
                


            //ActualizarNombres();
        }

        /*private void ActualizarNombres()
        {
            IDictionary<int, string> datos = new Dictionary<int, string>();

            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                sql_con = new SQLiteConnection("Data source=//" + Properties.Settings.Default.Hosting + @"\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }
            else
            {
                sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }

            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID}", sql_con);
            dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                var idProducto = Convert.ToInt32(dr["ID"].ToString());
                var nombreProducto = dr["Nombre"].ToString();

                datos.Add(new KeyValuePair<int, string>(idProducto, nombreProducto));
            }

            dr.Close();
            sql_con.Close();

            foreach (KeyValuePair<int, string> ele  in datos)
            {
                var idProducto = ele.Key;
                var nombreAlterno1 = mg.RemoverCaracteres(ele.Value);
                var nombreAlterno2 = mg.RemoverPreposiciones(ele.Value);

                cn.EjecutarConsulta($"UPDATE Productos SET NombreAlterno1 = '{nombreAlterno1}', NombreAlterno2 = '{nombreAlterno2}' WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID}");
            }
        }*/

        private void obtenerDatosCheckStock()
        {
            // Leer fichero que tiene el numero de CheckInventario

            using (StreamReader readfile = new StreamReader(Properties.Settings.Default.rutaDirectorio + ficheroNumCheck))
            {
                Contenido = readfile.ReadToEnd();   // se lee todo el archivo y se almacena en la variable Contenido
            }
            if (Contenido != "")   // si el contenido no es vacio
            {
                NumberRegCheckStock = (long)Convert.ToDouble(Contenido);
            }

            // Leer fichero que tiene la fecha de Inventario
            using (StreamReader readfile = new StreamReader(Properties.Settings.Default.rutaDirectorio + ficheroDateCheck))
            {
                Contenido = readfile.ReadToEnd();   // se lee todo el archivo y se almacena en la variable Contenido
            }
            if (Contenido != "")   // si el contenido no es vacio
            {
                FechaFinal = Contenido.Replace("\r\n", "");
                DateCheckStock = FechaFinal;
            }
        }

        private void ObtenerDatosUsuario(int IDUsuario)
        {
            datosUsuario = cn.DatosUsuario(IDUsuario: IDUsuario, tipo: 0);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            if(productos == 1)
            {
                AbrirFormulario<Productos>();

                Productos.recargarDatos = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            if(ventas == 1)
            {
                AbrirFormulario<ListadoVentas>();

                ListadoVentas.recargarDatos = true;
                ListadoVentas.abrirNuevaVenta = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            if(clientes == 1)
            {
                AbrirFormulario<Clientes>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            if(proveedores == 1)
            {
                AbrirFormulario<Proveedores>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //Metodo para abrir formularios dentro del panel
        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;

            formulario = panelContenedor.Controls.OfType<MiForm>().FirstOrDefault(); //Busca en la coleccion el formulario

            //Si el formulario/instancia no existe
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panelContenedor.Controls.Add(formulario);
                panelContenedor.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
            }
            else
            {
                formulario.BringToFront();
            }
        }


        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de cerrar la aplicación?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnMisDatos_Click(object sender, EventArgs e)
        {
            if(misdatos == 1)
            {
                AbrirFormulario<MisDatos>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEmpresas_Click(object sender, EventArgs e)
        {
            if(empresas == 1)
            {
                AbrirFormulario<Empresas>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnAnticipos_Click(object sender, EventArgs e)
        {
            if(anticipos == 1)
            {
                AbrirFormulario<Anticipos>();

                Anticipos.recargarDatos = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }            
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if(config == 1)
            {
                AbrirFormulario<SetUpPUDVE>();

                SetUpPUDVE.recargarDatos = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnCaja_Click(object sender, EventArgs e)
        {
            if(caja == 1)
            {
                AbrirFormulario<CajaN>();

                CajaN.recargarDatos = true;
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            if(inventarios == 1)
            {
                AbrirFormulario<Inventario>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            if(empleados == 1)
            {
                AbrirFormulario<Empleados>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void temporizador_respaldo_Tick(object sender, EventArgs e)
        {
            //Por el momento en comentarios, no eliminarlo
            //Genera_respaldos.respaldar();
        }

        
        private int obtener_id_empleado(int id)
        {
            int id_e = 0;

            id_e = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Empleados WHERE IDUsuario='{id}' AND usuario='{userNickName}'", 5));

            return id_e;
        }

        private void permisos_empleado(string[] datos_e)
        {
            anticipos = Convert.ToInt32(datos_e[0]);
            caja = Convert.ToInt32(datos_e[1]);
            clientes = Convert.ToInt32(datos_e[2]);
            config = Convert.ToInt32(datos_e[3]);
            empleados = Convert.ToInt32(datos_e[4]);
            empresas = Convert.ToInt32(datos_e[5]);
            facturas = Convert.ToInt32(datos_e[6]);
            inventarios =Convert.ToInt32(datos_e[7]);
            misdatos = Convert.ToInt32(datos_e[8]);
            productos = Convert.ToInt32(datos_e[9]);
            proveedores = Convert.ToInt32(datos_e[10]);
            reportes = Convert.ToInt32(datos_e[11]);
            servicios = Convert.ToInt32(datos_e[12]);
            ventas = Convert.ToInt32(datos_e[13]);
        }
    }
}
