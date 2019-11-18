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
    public partial class ListadoFuentes : Form
    {
        public ListadoFuentes()
        {
            InitializeComponent();
        }

        private void ListadoFuentes_Load(object sender, EventArgs e)
        {
            foreach (FontFamily fuente in FontFamily.Families)
            {
                cbFuentes.Items.Add(fuente.Name.ToString());
            }

            cbFuentes.SelectedItem = "Arial";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var fuente = cbFuentes.SelectedItem.ToString();

            GenerarEtiqueta.fuenteSeleccionada = fuente;

            Dispose();
        }
    }
}
