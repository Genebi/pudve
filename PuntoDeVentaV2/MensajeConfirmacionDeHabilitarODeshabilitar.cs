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
    public partial class MensajeConfirmacionDeHabilitarODeshabilitar : Form
    {
        string textolbl;
        public MensajeConfirmacionDeHabilitarODeshabilitar(string texto)
        {
            InitializeComponent();
            this.textolbl = texto;
        }

        private void MensajeConfirmacionDeHabilitarODeshabilitar_Load(object sender, EventArgs e)
        {
            label1.Text = textolbl;
        }

        private void btnAceptarDesc_Click(object sender, EventArgs e)
        {
            Empleados.SIoNO = true;
            this.Close();
        }

        private void btnCancelarDesc_Click(object sender, EventArgs e)
        {
            Empleados.SIoNO = false;
            this.Close();
        }
    }
}
