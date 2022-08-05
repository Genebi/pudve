using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class Visualizar_factura : Form
    {
        string nombre_pdf = "";
        string ruta = "";


        public Visualizar_factura(string nom_pdf)
        {
            InitializeComponent();

            nombre_pdf = nom_pdf;
        }

        private void Visualizar_factura_Load(object sender, EventArgs e)
        {
            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                ruta = $@"\\{servidor}\Archivos PUDVE\Facturas\" + nombre_pdf + ".pdf";
            }
            else
            {
                ruta = @"C:\Archivos PUDVE\Facturas\" + nombre_pdf + ".pdf";
            }
            
            axAcroPDFf.src = ruta;
            axAcroPDFf.setZoom(75);
        }

        private void btn_imprimir_Click(object sender, EventArgs e)
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
                MessageBox.Show("Error al imprimir No: " + ex, "Error al Imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
