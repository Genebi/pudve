using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            this.Text = "PUDVE - " + Anticipos.ticketGenerado;
            axAcroPDF.src = Anticipos.rutaTicketGenerado;
            axAcroPDF.setZoom(75);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = Anticipos.rutaTicketGenerado;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(1000);

            if (false == p.CloseMainWindow())
            {
                p.Kill();
            }

            this.Close();
        }
    }
}
