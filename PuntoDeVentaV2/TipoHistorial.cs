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
    public partial class TipoHistorial : Form
    {
        public int tipoRespuesta { get; set; }

        private string fechaInicial;
        private string fechaFinal;

        private int idProducto;

        public TipoHistorial(int idProducto)
        {
            InitializeComponent();

            this.idProducto = idProducto;
        }

        private void btnHistorialCompras_Click(object sender, EventArgs e)
        {
            tipoRespuesta = 1;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnHistorialVentas_Click(object sender, EventArgs e)
        {
            using (var fechas = new FechasReportes())
            {
                var respuesta = fechas.ShowDialog();

                if (respuesta == DialogResult.OK)
                {
                    tipoRespuesta = 2;

                    fechaInicial = fechas.fechaInicial;
                    fechaFinal = fechas.fechaFinal;
                }
            }
        }

        private void GenerarReporte()
        {
            //fechaInicial
            //fechaFinal
            //idProducto
        }
    }
}
