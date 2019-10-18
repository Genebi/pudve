﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class FormPrincipal : Form
    {
        Conexion cn = new Conexion();

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
            userID = IdUsuario;
            userNickName = nickUsuario;
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
        }

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
            AbrirFormulario<Productos>();

            Productos.recargarDatos = true;
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            AbrirFormulario<ListadoVentas>();

            ListadoVentas.abrirNuevaVenta = true;
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Clientes>();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Proveedores>();
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
            AbrirFormulario<MisDatos>();
        }

        private void btnEmpresas_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Empresas>();
        }

        private void btnAnticipos_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Anticipos>();

            Anticipos.recargarDatos = true;
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            AbrirFormulario<SetUpPUDVE>();
        }

        private void btnCaja_Click(object sender, EventArgs e)
        {
            AbrirFormulario<CajaN>();

            CajaN.recargarDatos = true;
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Inventario>();
        }
    }
}
