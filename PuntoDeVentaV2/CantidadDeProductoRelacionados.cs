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
    public partial class CantidadDeProductoRelacionados : Form
    {
        public static string cantidad ;
        public CantidadDeProductoRelacionados()
        {
            InitializeComponent();
        }

        private void btnAcpetar_Click(object sender, EventArgs e)
        {
            cantidad = txtCantidad.ToString();
            this.Close();
        }
    }
}
