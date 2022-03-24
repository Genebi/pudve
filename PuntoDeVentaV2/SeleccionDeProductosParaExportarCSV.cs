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
    public partial class SeleccionDeProductosParaExportarCSV : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        public SeleccionDeProductosParaExportarCSV()
        {
            InitializeComponent();

        }

        private void SeleccionDeProductosParaExportarCSV_Load(object sender, EventArgs e)
        {

            using (DataTable dtDatosProductos = cn.CargarDatos(cs.ProductosParaFiltrarCSV()))
            {
                if (!dtDatosProductos.Rows.Count.Equals(0))
                {
                    dgvProductos.DataSource = dtDatosProductos;
                }
            }
        }
    }
}
