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

        private void AgregarMultiplesProductos_Load(object sender, EventArgs e)
        {
            txtAgregarPM.KeyPress += new KeyPressEventHandler(SoloEnteros);
        }

        private void btnAceptarAM_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAgregarPM.Text))
            {
                var cantidad = Convert.ToInt32(txtAgregarPM.Text);

                if (cantidad > 0)
                {
                    Ventas.cantidadFila = cantidad;

                    this.Close();
                }  
            }
        }

        private void txtAgregarPM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAceptarAM.PerformClick();
            }
        }

        private void SoloEnteros(object sender, KeyPressEventArgs e)
        {
            //permite 0-9, eliminar y decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8))
            {
                e.Handled = true;
                return;
            }
        }
    }
}
