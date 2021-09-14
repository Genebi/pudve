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
    public partial class InfoUltimaVenta : Form
    {
        MetodosBusquedas mb = new MetodosBusquedas();
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        Moneda oMoneda = new Moneda();


        float Total;
        float DineroRecibido;
        float CambioTotal;
        public InfoUltimaVenta()
        {
            InitializeComponent();
        }

        private void InfoUltimaVenta_Load(object sender, EventArgs e)
        {
            var ticketTemporal = cn.CargarDatos("Select Total, DineroRecibido, CambioTotal FROM ventas WHERE ID ORDER BY ID DESC LIMIT 1");

            foreach (DataRow item in ticketTemporal.Rows)
            {
                Total = (float)Convert.ToDouble(item[0]);
                DineroRecibido = (float)Convert.ToDouble(item[1]);
                CambioTotal = (float)Convert.ToDouble(item[2]);
            }
            lbSucambio.Text = "Su cambio es de:";
            lbCambio.Text = CambioTotal.ToString("C");
            lbTotalAPagar.Text = "El total a pagar es: " ;
            lbDineroRecibido.Text ="Recibio la cantidad de: ";
            lbTotalAPagar2.Text = Total.ToString("C");
            lbDineroRecibido2.Text = DineroRecibido.ToString("C");
            lbUltimaCompra.Text = "Ultima venta realizada: ";
            lbFechaVenta.Text = ""+DateTime.Now;

            string resultado = oMoneda.Convertir(CambioTotal.ToString(), true, "PESOS");
            lbCambioTexto.Text = resultado;
        }

        private void InfoUltimaVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                btnCerrar.PerformClick();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void botonRedondo1_Click(object sender, EventArgs e)
        {
            Ventas ventas = Application.OpenForms.OfType<Ventas>().FirstOrDefault();

            if (ventas != null)
            {
                ventas.btnUltimoTicket.PerformClick();
            }
        }
    }
}
