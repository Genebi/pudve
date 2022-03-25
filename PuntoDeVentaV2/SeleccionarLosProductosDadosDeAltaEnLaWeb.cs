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
            if (e.RowIndex >= 0)
            {
                cn.EjecutarConsulta(cs.EstadoDeRegistroDeProductoComoEnWeb(dgvNel.Rows[e.RowIndex].Cells["ID"].Value.ToString(), "'Si'"));
                actualizarDGV();
            }
            
        }

        private void dgvSis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                cn.EjecutarConsulta(cs.EstadoDeRegistroDeProductoComoEnWeb(dgvSis.Rows[e.RowIndex].Cells["ID"].Value.ToString(), "'No'"));
                actualizarDGV();
            }
            
        }

        private void actualizarDGV()
        {

            using (DataTable dtDatosProductos = cn.CargarDatos(cs.ProductosParaFiltrarCSVFiltroSiEstaEnWeb("AND EnWeb = 'Si'")))
            {
                if (!dtDatosProductos.Rows.Count.Equals(0))
                {
                    dgvSis.DataSource = dtDatosProductos;
                    dgvSis.ClearSelection();
                }
            }
            using (DataTable dtDatosProductos = cn.CargarDatos(cs.ProductosParaFiltrarCSVFiltroSiEstaEnWeb("")))
            {
                if (!dtDatosProductos.Rows.Count.Equals(0))
                {
                    dgvNel.DataSource = dtDatosProductos;
                    dgvNel.ClearSelection();
                }
            }
            
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBusquedaTodos_TextChanged(object sender, EventArgs e)
        {
            var concepto = txtBusquedaTodos.Text.Trim();
            var consulta = $"Nombre LIKE '%{concepto}%'";
            (dgvNel.DataSource as DataTable).DefaultView.RowFilter = string.Format(consulta);
            dgvNel.ClearSelection();
        }

        private void txtBusquedaEnWeb_TextChanged(object sender, EventArgs e)
        {
            var concepto = txtBusquedaEnWeb.Text.Trim();
            var consulta = $"Nombre LIKE '%{concepto}%'";
            (dgvSis.DataSource as DataTable).DefaultView.RowFilter = string.Format(consulta);
            dgvSis.ClearSelection();
        }

        private void txtBusquedaTodos_MouseDown(object sender, MouseEventArgs e)
        {
            txtBusquedaTodos.Clear();
        }

        private void txtBusquedaEnWeb_MouseDown(object sender, MouseEventArgs e)
        {
            txtBusquedaEnWeb.Text = "";
        }
    }
}
