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
    public partial class SeleccionarLosProductosDadosDeAltaEnLaWeb : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        
        public SeleccionarLosProductosDadosDeAltaEnLaWeb()
        {
            InitializeComponent();
        }

        private void SeleccionarLosProductosDadosDeAltaEnLaWeb_Load(object sender, EventArgs e)
        {
            dgvSis.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            actualizarDGV();
        }

        private void dgvNel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cn.EjecutarConsulta(cs.EstadoDeRegistroDeProductoComoEnWeb(dgvNel.Rows[e.RowIndex].Cells["SKU"].Value.ToString(), "'Si'"));
            actualizarDGV();
        }

        private void dgvSis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cn.EjecutarConsulta(cs.EstadoDeRegistroDeProductoComoEnWeb(dgvSis.Rows[e.RowIndex].Cells["SKU"].Value.ToString(), "'No'"));
            actualizarDGV();
        }

        private void actualizarDGV()
        {

            using (DataTable dtDatosProductos = cn.CargarDatos(cs.ProductosParaFiltrarCSV("AND EnWeb = 'Si'")))
            {
                if (!dtDatosProductos.Rows.Count.Equals(0))
                {
                    dgvSis.DataSource = dtDatosProductos;

                }
            }
            using (DataTable dtDatosProductos = cn.CargarDatos(cs.ProductosParaFiltrarCSV("")))
            {
                if (!dtDatosProductos.Rows.Count.Equals(0))
                {
                    dgvNel.DataSource = dtDatosProductos;

                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
