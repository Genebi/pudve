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
    public partial class AsignarCantidadProdACombo : Form
    {
        public decimal cantidadDeProducto = 0;
        public AsignarCantidadProdACombo()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            cantidadDeProducto = Convert.ToDecimal(txtCantidad.Text);
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AsignarCantidadProdACombo_Load(object sender, EventArgs e)
        {
            txtCantidad.SelectAll();

        }
    }
}
