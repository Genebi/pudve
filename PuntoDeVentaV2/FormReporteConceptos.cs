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
    public partial class FormReporteConceptos : Form
    {
        DataTable DTFinal = new DataTable();
        public FormReporteConceptos()
        {
            InitializeComponent();
        }

        private void FormReporteConceptos_Load(object sender, EventArgs e)
        {
            CargarDatosCaja();
            this.reportViewer1.RefreshReport();
        }

        private void CargarDatosCaja()
        {
            throw new NotImplementedException();
        }
    }
}
