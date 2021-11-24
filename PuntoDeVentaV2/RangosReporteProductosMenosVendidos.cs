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
    }
}
