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
    public partial class RevisarInventario : Form
    {
        int cantidadStock;
        string SearchBarCode;

        public RevisarInventario()
        {
            InitializeComponent();
            cantidadStock = 0;
            SearchBarCode = string.Empty;
        }

        private void btnReducirStock_Click(object sender, EventArgs e)
        {
            if (lblCantidadStock.Text == "0")
            {
                MessageBox.Show("No se permite tener stock menor a = " + lblCantidadStock.Text, "Error al Disminuir Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!lblCantidadStock.Equals("0"))
            {
                cantidadStock = Convert.ToInt32(lblCantidadStock.Text);
                cantidadStock--;
                if (cantidadStock >= 0)
                {
                    lblCantidadStock.Text = Convert.ToString(cantidadStock);
                }
            }
        }

        private void btnAumentarStock_Click(object sender, EventArgs e)
        {
            cantidadStock = Convert.ToInt32(lblCantidadStock.Text);
            cantidadStock++;
            lblCantidadStock.Text = Convert.ToString(cantidadStock);
        }
    }
}
