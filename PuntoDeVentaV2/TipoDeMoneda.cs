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
    public partial class TipoDeMoneda : Form
    {
        public TipoDeMoneda()
        {
            InitializeComponent();
        }

        private void TipoDeMoneda_Load(object sender, EventArgs e)
        {
            if (cboTipoMoneda.Text.Equals(string.Empty))
            {
                cboTipoMoneda.SelectedIndex = 113;
                FormPrincipal.Moneda = cboTipoMoneda.Text;
            }
        }

        private void cboTipoMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormPrincipal.Moneda = cboTipoMoneda.SelectedItem.ToString();
            Productos producto = Application.OpenForms.OfType<Productos>().FirstOrDefault();

            if (producto != null)
            {
                producto.recargarDGV();
            }
        }
    }
}
