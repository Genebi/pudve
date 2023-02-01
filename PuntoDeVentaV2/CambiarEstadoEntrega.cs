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
    public partial class CambiarEstadoEntrega : Form
    {
        public int estado { get; set; }
        public CambiarEstadoEntrega()
        {
            InitializeComponent();
        }

        private void CambiarEstadoEntrega_Load(object sender, EventArgs e)
        {
            cbEstado.SelectedIndex = 0;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            estado = cbEstado.SelectedIndex;

            DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
