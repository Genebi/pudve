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
    public partial class CajaN : Form
    {
        public CajaN()
        {
            InitializeComponent();
        }

        private void btnReporteAgregar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reporte agregar");
        }

        private void btnReporteRetirar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reporte retirar");
        }

        private void btnAgregarDinero_Click(object sender, EventArgs e)
        {
            AgregarRetirarDinero agregar = new AgregarRetirarDinero();

            agregar.FormClosed += delegate
            {

            };

            agregar.ShowDialog();
        }

        private void btnRetirarDinero_Click(object sender, EventArgs e)
        {
            AgregarRetirarDinero retirar = new AgregarRetirarDinero(1);

            retirar.FormClosed += delegate
            {

            };

            retirar.ShowDialog();
        }
    }
}
