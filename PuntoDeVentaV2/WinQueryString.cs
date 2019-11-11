using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class WinQueryString : Form
    {
        public WinQueryString()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbTipoFiltro_Click(object sender, EventArgs e)
        {
            cbTipoFiltro.DroppedDown = true;
        }

        private void txtCantStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

            if (char.IsNumber(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Soló son permitidos numeros\nen este campo de Stock", 
                                "Error de captura", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WinQueryString_Load(object sender, EventArgs e)
        {
            validarChkBox();
        }

        private void validarChkBox()
        {
            if (chkBoxStock.Checked.Equals(true))
            {
                txtCantStock.Enabled = true;
            }
            else if (chkBoxStock.Checked.Equals(false))
            {
                txtCantStock.Enabled = false;
            }
        }
    }
}
