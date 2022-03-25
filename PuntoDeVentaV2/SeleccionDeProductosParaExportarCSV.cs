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
        public DataTable dt = new DataTable();
        public SeleccionDeProductosParaExportarCSV()
        {
            InitializeComponent();

        }

        private void SeleccionDeProductosParaExportarCSV_Load(object sender, EventArgs e)
        {
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            using (DataTable dtDatosProductos = cn.CargarDatos(cs.ProductosParaFiltrarCSV()))
            {
                if (!dtDatosProductos.Rows.Count.Equals(0))
                {
                    dgvProductos.DataSource = dtDatosProductos;
                    
                    foreach (DataGridViewColumn column in dgvProductos.Columns)
                        dt.Columns.Add(column.Name); 
                    
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvProductos.SelectedRows)
            {
                dt.ImportRow(((DataTable)dgvProductos.DataSource).Rows[row.Index]);
            }
            dt.AcceptChanges();
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
