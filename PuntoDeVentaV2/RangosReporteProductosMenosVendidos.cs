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
    public partial class RangosReporteProductosMenosVendidos : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        string fechaHoraInicio = string.Empty;
        string fechaHoraFinal = string.Empty;
        int cantidadLimite = 0;

        public RangosReporteProductosMenosVendidos()
        {
            InitializeComponent();
        }

        private void RangosReporteProductosMenosVendidos_Load(object sender, EventArgs e)
        {
            configurarDateTimePicker();
        }

        private void configurarDateTimePicker()
        {
            var personalizada = "dd / MM / yyyy h:mm:ss tt";

            dtpInicio.Format = DateTimePickerFormat.Custom;
            dtpInicio.CustomFormat = personalizada;
            dtpInicio.Text = DateTime.Parse(dtpInicio.Text).AddMonths(-1).ToString();

            dtpFin.Format = DateTimePickerFormat.Custom;
            dtpFin.CustomFormat = personalizada;
        }

        private void txtCantidadMostar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void botonRedondo1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void botonRedondo2_Click(object sender, EventArgs e)
        {
            if (dtpFin.Value >= dtpInicio.Value)
            {
                fechaHoraInicio = dtpInicio.Value.ToString("yyyy-MM-dd HH:mm:ss");
                fechaHoraFinal = dtpFin.Value.ToString("yyyy-MM-dd HH:mm:ss");

                if (!string.IsNullOrWhiteSpace(txtCantidadMostar.Text))
                {
                    cantidadLimite = Convert.ToInt32(txtCantidadMostar.Text);
                }

                using (DataTable dtProductosMenosVendidos = cn.CargarDatos(cs.productosMenosVendidos(fechaHoraInicio, fechaHoraFinal)))
                {
                    if (!dtProductosMenosVendidos.Rows.Count.Equals(0))
                    {
                        generarReporteMenosVendidos(dtProductosMenosVendidos);
                    }
                }
            }
        }

        private void generarReporteMenosVendidos(DataTable dtProductosMenosVendidos)
        {
            
        }
    }
}
