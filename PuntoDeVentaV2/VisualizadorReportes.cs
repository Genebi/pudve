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
    public partial class VisualizadorReportes : Form
    {
        string ruta = string.Empty;
        public VisualizadorReportes(string ruta = "")
        {
            InitializeComponent();

            this.ruta = ruta;
        }

        private void VisualizadorReportes_Load(object sender, EventArgs e)
        {
            axAcroPDF.src = ruta;
            axAcroPDF.setZoom(75);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.Verb = "print";
                info.FileName = ruta;
                info.CreateNoWindow = true;
                info.WindowStyle = ProcessWindowStyle.Hidden;

                Process p = new Process();
                p.StartInfo = info;
                p.Start();

                p.WaitForInputIdle();
                System.Threading.Thread.Sleep(5000);

                if (false == p.CloseMainWindow())
                {
                    p.Kill();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Imprimir No: " + ex, "Error al Imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
