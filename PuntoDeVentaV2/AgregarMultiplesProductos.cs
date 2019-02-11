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
    public partial class AgregarMultiplesProductos : Form
    {
        public AgregarMultiplesProductos()
        {
            InitializeComponent();
        }

        private void btnCancelarAM_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptarAM_Click(object sender, EventArgs e)
        {
            Ventas.cantidadFila = Convert.ToInt32(txtAgregarPM.Text);
            this.Close();
        }
    }
}
