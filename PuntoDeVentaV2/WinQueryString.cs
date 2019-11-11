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
    }
}
