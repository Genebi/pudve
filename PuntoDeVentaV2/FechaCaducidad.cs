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
    public partial class FechaCaducidad : Form
    {
        public FechaCaducidad()
        {
            InitializeComponent();
        }

        private void FechaCaducidad_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DateTime fecha = dateTimePicker1.Value;
            subDetallesDeProducto.FechaCaducidad = fecha.ToString("yyyy-MM-dd");
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            subDetallesDeProducto.FechaCaducidad = "";
            this.Close();
        }
    }
}
