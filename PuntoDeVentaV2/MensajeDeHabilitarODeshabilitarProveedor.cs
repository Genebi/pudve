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
    public partial class MensajeDeHabilitarODeshabilitarProveedor : Form
    {
        string texto;
        public MensajeDeHabilitarODeshabilitarProveedor(string mensaje)
        {
            InitializeComponent();
            this.texto = mensaje;
        }

        private void MensajeDeHabilitarODeshabilitarProveedor_Load(object sender, EventArgs e)
        {
            lblTexto.Text = texto;
        }

        private void btnAceptarDesc_Click(object sender, EventArgs e)
        {
            Proveedores.HabilitarODeshabilitar = true;
            this.Close();
        }

        private void btnCancelarDesc_Click(object sender, EventArgs e)
        {
            Proveedores.HabilitarODeshabilitar = false;
            this.Close();
        }
    }
    
}
