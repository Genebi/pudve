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
    public partial class TipoReportesInventario : Form
    {
        public TipoReportesInventario()
        {
            InitializeComponent();
        }

        private void TipoReportesInventario_Load(object sender, EventArgs e)
        {

        }

        private void llamarVentana(string lugarProcedencia)
        {
            BuscadorReporteInventario BRInventario = new BuscadorReporteInventario(lugarProcedencia);
            BRInventario.ShowDialog();
        }

        private void btnRevisarInventario_Click(object sender, EventArgs e)
        {
            llamarVentana("RInventario");
        }

        private void btnAIAumentar_Click(object sender, EventArgs e)
        {
            llamarVentana("AIAumentar");
        }

        private void btnAIDisminuir_Click(object sender, EventArgs e)
        {
            llamarVentana("AIDisminuir");
        }
    }
}
