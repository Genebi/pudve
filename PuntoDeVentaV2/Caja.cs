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
    public partial class Caja : Form
    {
        public Caja()
        {
            InitializeComponent();
        }

        private void btnAgregarDinero_Click(object sender, EventArgs e)
        {
            gbContenedor.Text = "DEPOSITAR DINERO";

            panelRetirar.Visible = false;
            panelAgregar.Visible = true;
            txtAgregarDinero.Focus();
        }

        private void btnRetirarDinero_Click(object sender, EventArgs e)
        {
            gbContenedor.Text = "RETIRAR DINERO";

            panelAgregar.Visible = false;
            panelRetirar.Visible = true;
            txtRetirarDinero.Focus();
        }

        private void btnCancelarDeposito_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnAceptarDeposito_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aceptar");
        }

        private void btnCancelarRetirar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnAceptarRetirar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aceptar retiro");
        }

        private void LimpiarCampos()
        {
            gbContenedor.Text = string.Empty;
            txtAgregarDinero.Text = string.Empty;
            txtRetirarDinero.Text = string.Empty;
            txtConcepto.Text = string.Empty;
            panelAgregar.Visible = false;
            panelRetirar.Visible = false;
        }
    }
}
