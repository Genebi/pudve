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
    public partial class VincularPC : Form
    {
        public VincularPC()
        {
            InitializeComponent();
        }

        private void VincularPC_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                txtNombre.Text = Properties.Settings.Default.Hosting;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var computadora = txtNombre.Text.Trim();

            if (string.IsNullOrWhiteSpace(computadora))
            {
                MessageBox.Show("Ingresa el nombre del servidor a la\nque se vinculará esta computadora", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombre.Focus();
                return;
            }

            Properties.Settings.Default.Hosting = computadora;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            MessageBox.Show("La computadora ha sido vinculada correctamente", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                btnAceptar.PerformClick();
            }
        }
    }
}
