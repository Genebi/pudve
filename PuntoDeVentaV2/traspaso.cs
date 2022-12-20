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
    public partial class traspaso : Form
    {
        public traspaso(DataTable datosTraspaso)
        {
            InitializeComponent();
            DGVTraspaso.DataSource = datosTraspaso;
        }
    }
}
