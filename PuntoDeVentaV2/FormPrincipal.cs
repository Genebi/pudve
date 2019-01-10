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
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        Conexion datos = new Conexion();

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            //datos.SincronizarProductos();
            //Temporizador();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Productos>();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Ventas>();
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
    }
}
