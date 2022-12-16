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
    public partial class ConsultaPrecio : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        public static string CodigoDeBarras;
        public static bool OriginalOExtra;
        public static string id;
        public ConsultaPrecio()
        {
            InitializeComponent();
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
                txtBusqueda.Clear();
                txtBusqueda.Focus();
            }
            
        }

        private void CargarDatos(string busqueda = "")
        {
            DataTable producto,ProductoCodigoExtra;
            using (producto = cn.CargarDatos(cs.BuscarProductoPorCodigoDeBarras(busqueda)))
            {
                using (ProductoCodigoExtra = cn.CargarDatos(cs.obtenerProductoPorCodigoExtra(busqueda)))
                {
                    if (!producto.Rows.Count.Equals(0))
                    {
                        OriginalOExtra = true;
                        PreciosProducto precios = new PreciosProducto();
                        precios.ShowDialog();
                        
                    }
                    else if (!ProductoCodigoExtra.Rows.Count.Equals(0))
                    {
                        id = ProductoCodigoExtra.Rows[0]["IDProducto"].ToString();
                        OriginalOExtra = false;
                        PreciosProducto precios = new PreciosProducto();
                        precios.ShowDialog();
                    }
                    else
                    {
                        MessageBoxTemporal.Show("El producto no Existe en la base de datos", "Aviso del Sistema", 3, true);
                    }
                } 
            }
        }

        private void ConsultaPrecio_Load(object sender, EventArgs e)
        {
            //this.TopMost = true;
            //this.FormBorderStyle=FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            pantallaCompleta();
            txtBusqueda.Focus();
            txtBusqueda.SelectAll();
            
        }
        public void pantallaCompleta()
        {
            int lx, ly;
            int sw, sh;
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ConsultaPrecio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.B && (e.Control))
            {
                this.Close();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!txtBusqueda.Text.Equals(string.Empty))
            {
                CodigoDeBarras = txtBusqueda.Text;
                CargarDatos(CodigoDeBarras);
            }
            else
            {
                MessageBox.Show("Ingrese un Codigo de Barras", "Aviso del Sitema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            txtBusqueda.SelectAll();
        }

        private void btnEnvioCorreo_Click(object sender, EventArgs e)
        {
            empleadoVerificarHuella comparar = new empleadoVerificarHuella();
            comparar.ShowDialog();
        }
    }
}
