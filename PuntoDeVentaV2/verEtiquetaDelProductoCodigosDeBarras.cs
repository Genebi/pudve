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
    public partial class verEtiquetaDelProductoCodigosDeBarras : Form
    {
        public string NombreDelProducto { get; set; }
        public string PrecioDelProducto { get; set; }
        public string CodigoBarraDelProducto { get; set; }

        public verEtiquetaDelProductoCodigosDeBarras()
        {
            InitializeComponent();
        }

        private void verEtiquetaDelProductoCodigosDeBarras_Load(object sender, EventArgs e)
        {
            cargarDatosEtiqueta();
            this.reportViewer1.RefreshReport();
        }

        private void cargarDatosEtiqueta()
        {
            
        }
    }
}
