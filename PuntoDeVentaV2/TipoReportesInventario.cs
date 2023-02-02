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
            this.Text = "Escoger Tipo Reporte";
        }

        private void TipoReportesInventario_Load(object sender, EventArgs e)
        {

        }

        private void llamarVentana(string lugarProcedencia)
        {
            BuscadorReporteInventario BRInventario = new BuscadorReporteInventario(lugarProcedencia);
            BRInventario.ShowDialog();
        }

        private void btnActualizarInventarioNew_Click(object sender, EventArgs e)
        {
            llamarVentana("RInventario");
        }

        private void btnActualizarInventarioAumentar_Click(object sender, EventArgs e)
        {
            llamarVentana("AIAumentar");
        }

        private void btnActualizarInventarioDisminuir_Click(object sender, EventArgs e)
        {
            llamarVentana("AIDisminuir");
        }

        private void TipoReportesInventario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void botonRedondo1_Click(object sender, EventArgs e)
        {
            llamarVentana("AIDevolucion");
        }
    }
}
