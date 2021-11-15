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
    public partial class AgregarEspecificacionDeConceptoDinamico : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public string getIdUsr { get; set; }
        public string getChkName { get; set; }

        public AgregarEspecificacionDeConceptoDinamico()
        {
            InitializeComponent();
        }

        private void AgregarEspecificacionDeConceptoDinamico_Load(object sender, EventArgs e)
        {
            cargarEspecificacionesDelDatoDinamico();
        }

        private void cargarEspecificacionesDelDatoDinamico()
        {
            lblConceptoDinamico.Text += $"{getChkName}".Replace("_", " ").Trim();

            using (DataTable dtEspecificaciones = cn.CargarDatos(cs.obtenerEspecificacionesActivasDetalleDinamico(getChkName)))
            {
                if (!dtEspecificaciones.Rows.Count.Equals(0))
                {
                    DGVEspecificacionesActivas.Rows.Clear();
                    DGVEspecificacionesActivas.DataSource = dtEspecificaciones;
                }
                else
                {
                    DGVEspecificacionesActivas.Rows.Clear();
                }
            }
        }

        private void txtEspecificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                btnAgregarEspecificacion.PerformClick();
            }
        }

        private void btnAgregarEspecificacion_Click(object sender, EventArgs e)
        {
            var detalleGeneral = txtEspecificacion.Text.Replace(" ", "_");

            if (string.IsNullOrWhiteSpace(detalleGeneral))
            {
                MessageBox.Show("Introduzca una especificación para el detalle de Producto", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int resultado = cn.EjecutarConsulta(cs.agregarEspecificacionAlDetalleDinamico(getChkName, detalleGeneral));

                if (resultado > 0)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
