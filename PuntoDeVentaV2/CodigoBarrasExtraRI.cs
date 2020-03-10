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
    public partial class CodigoBarrasExtraRI : Form
    {
        MetodosBusquedas mb = new MetodosBusquedas();

        private string[] codigos;
        public CodigoBarrasExtraRI(string[] codigos)
        {
            InitializeComponent();

            this.codigos = codigos;
        }

        private void CodigoBarrasExtraRI_Load(object sender, EventArgs e)
        {
            // Mostrar codigos de barra extra
            if (codigos.Length > 0)
            {
                listBox.Items.AddRange(codigos);
            }
        }

        private void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.C)
            {
                var codigo = listBox.SelectedItem.ToString();

                Clipboard.SetData(DataFormats.StringFormat, codigo);
            }
        }
    }
}
