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
    public partial class PreguntasDevolucionProductos : Form
    {
        public PreguntasDevolucionProductos()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (rbDevolverDinero.Checked == true)
            {
                Inventario.operacionDevolucionProducto = 1;
            }
            else if (rbGenerarTicket.Checked == true)
            {
                Inventario.operacionDevolucionProducto = 2;
            }
            else if (rbNada.Checked == true)
            {
                this.Close();
            }
            this.Close();
        }
    }
}
