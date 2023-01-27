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
    public partial class CantidadRegresada : Form
    {
        public CantidadRegresada()
        {
            InitializeComponent();
        }

        private void CantidadRegresada_Load(object sender, EventArgs e)
        {
            lblNombreProducto.Text = AjustarProducto.nombreDePorducto;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            AjustarProducto.cantidadRegresada = Convert.ToDecimal(txtCantidad.Text);
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                btnAceptar.PerformClick();
            }
        }
    }
}
