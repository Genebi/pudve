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
    public partial class DatosOrden : Form
    {
        public DatosOrden()
        {
            InitializeComponent();
        }

        private void DatosOrden_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var dias = nudDias.Value.ToString();
            var horas = nudHoras.Value.ToString();
            var minutos = nudMinutos.Value.ToString();

            dias = dias.Length == 1 ? $"0{dias}" : dias;
            horas = horas.Length == 1 ? $"0{horas}" : horas;
            minutos = minutos.Length == 1 ? $"0{minutos}" : minutos;

            var tiempoEntrega = $"{dias}|{horas}|{minutos}";
            var fechaEntrega = dtpFechaEntrega.Value.ToString("yyyy-MM-dd");

            Ventas.tiempoElaboracion = tiempoEntrega;
            Ventas.fechaEntrega = fechaEntrega;

            Close();
        }
    }
}
