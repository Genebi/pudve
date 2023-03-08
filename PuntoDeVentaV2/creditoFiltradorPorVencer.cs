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
    public partial class creditoFiltradorPorVencer : Form
    {
        public DateTime lamerafecha;
        public creditoFiltradorPorVencer()
        {
            InitializeComponent();
        }

        private void creditoFiltradorPorVencer_Load(object sender, EventArgs e)
        {
            cbPeriodos.SelectedIndex = 0;
        }

        private void numPeriodos_ValueChanged(object sender, EventArgs e)
        {
            updateFecha();
        }

        private void updateFecha()
        {
            switch (cbPeriodos.SelectedIndex)
            {
                case 0:
                    lamerafecha = DateTime.Today.AddDays((double)numPeriodos.Value);
                    break;
                case 1:
                    lamerafecha = DateTime.Today.AddDays((double)numPeriodos.Value*7);
                    break;
                case 2:
                    lamerafecha = DateTime.Today.AddMonths((int)numPeriodos.Value);
                    break;
                default:
                    break;
            }
            lblFecha.Text = $"A partir de: {lamerafecha.ToString("yyyy-MM-dd")}";
        }

        private void cbPeriodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateFecha();
        }

        private void btnVencidas_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numPeriodos_KeyDown(object sender, KeyEventArgs e)
        {
            updateFecha();
        }
    }
}
