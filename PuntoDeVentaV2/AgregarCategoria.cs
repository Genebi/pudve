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
    public partial class AgregarCategoria : Form
    {
        Conexion cn = new Conexion();

        public AgregarCategoria()
        {
            InitializeComponent();
        }

        private void AgregarCategoria_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var categoria = txtNombre.Text;

            if (string.IsNullOrWhiteSpace(categoria))
            {
                MessageBox.Show("Introduzca un nombre para la categoría", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int resultado = cn.EjecutarConsulta($"INSERT INTO Categorias (IDUsuario, Nombre) VALUES ('{FormPrincipal.userID}', '{categoria}')");

            if (resultado > 0)
            {
                this.Dispose();
            }
        }

        private void AgregarCategoria_Shown(object sender, EventArgs e)
        {
            txtNombre.Focus();
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }
    }
}
