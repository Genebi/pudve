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
    public partial class AgregarDetalleProducto : Form
    {
        public AgregarDetalleProducto()
        {
            InitializeComponent();
        }

        private void listaOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int index in listaOpciones.CheckedIndices)
            {
                MessageBox.Show(index.ToString());
            }
        }
    }
}
