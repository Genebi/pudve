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
    public partial class VisualizadorReporteInventario : Form
    {
        public VisualizadorReporteInventario()
        {
            InitializeComponent();
        }

        private void VisualizadorReporteInventario_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
