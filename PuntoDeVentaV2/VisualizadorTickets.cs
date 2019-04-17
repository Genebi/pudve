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
    public partial class VisualizadorTickets : Form
    {
        public VisualizadorTickets()
        {
            InitializeComponent();
        }

        private void VisualizadorTickets_Load(object sender, EventArgs e)
        {
            axAcroPDF.src = @"C:\VentasPUDVE\" + Anticipos.ticketGenerado;
            axAcroPDF.setZoom(100);
        }
    }
}
