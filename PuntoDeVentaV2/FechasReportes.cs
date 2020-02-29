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
    public partial class FechasReportes : Form
    {
        public string fechaInicial { get; set; }
        public string fechaFinal { get; set; }

        public FechasReportes()
        {
            InitializeComponent();
        }

        private void FechasReportes_Load(object sender, EventArgs e)
        {
            primerDatePicker.Value = DateTime.Today.AddDays(-30);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            fechaInicial = primerDatePicker.Value.ToString("yyyy-MM-dd");
            fechaFinal = segundoDatePicker.Value.ToString("yyyy-MM-dd");
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
