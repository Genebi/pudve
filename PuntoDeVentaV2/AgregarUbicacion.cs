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
            var categoria = txtNombre.Text;

            if (string.IsNullOrWhiteSpace(categoria))
            {
                MessageBox.Show("Introduzca un nombre para la ubicación", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(categoria);
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
