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
    public partial class AgregarUbicacion : Form
    {
        Conexion cn = new Conexion();

        public AgregarUbicacion()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var ubicacion = txtNombre.Text;

            if (string.IsNullOrWhiteSpace(ubicacion))
            {
                MessageBox.Show("Introduzca un nombre para la ubicación", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int resultado = cn.EjecutarConsulta($"INSERT INTO Ubicaciones (IDUsuario, Descripcion) VALUES ('{FormPrincipal.userID}', '{ubicacion}')");
            
            if (resultado > 0)
            {
                this.Dispose();
            }
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData  == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void AgregarUbicacion_Shown(object sender, EventArgs e)
        {
            txtNombre.Focus();
        }
    }
}
