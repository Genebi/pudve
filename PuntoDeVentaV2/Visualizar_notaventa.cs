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
    public partial class Visualizar_notaventa : Form
    {

        string nombre_pdf = "";
        string ruta = "";


        public Visualizar_notaventa(string nom_pdf)
        {
            InitializeComponent();

            nombre_pdf = nom_pdf;
        }

        private void Visualizar_notaventa_Load(object sender, EventArgs e)
        {
            //ruta = @"C:\Archivos PUDVE\Ventas\PDF\" + nombre_pdf + ".pdf";

            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                ruta = $@"\\{servidor}\Archivos PUDVE\Ventas\PDF\VENTA_" + nombre_pdf + ".pdf";
            }
            else
            {
                ruta = $@"C:\Archivos PUDVE\Ventas\PDF\VENTA_" + nombre_pdf + ".pdf";
            }

            axAcroPDFf.src = ruta;
            axAcroPDFf.setZoom(75);
        }
    }
}
