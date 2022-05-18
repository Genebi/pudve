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

        int botonCerrar = 0, botonImprimir = 0;

        float Total;
        float DineroRecibido;
        float CambioTotal;
       
        public InfoUltimaVenta()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(InfoUltimaVenta_KeyDown);
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
            lbCambio.Text = DetalleVenta.cambio.ToString("C2");
            //lbCambio.Text = CambioTotal.ToString("C");
            lbTotalAPagar.Text = "El total a pagar es: " ;
            lbDineroRecibido.Text ="Recibio la cantidad de: ";
            lbTotalAPagar2.Text = DetalleVenta.restante.ToString("C2");
            //lbTotalAPagar2.Text = Total.ToString("C");
            lbDineroRecibido2.Text = DineroRecibido.ToString("C");
            lbUltimaCompra.Text = "Ultima venta realizada: ";
            lbFechaVenta.Text = ""+DateTime.Now;        

            

            string resultado = oMoneda.Convertir(DetalleVenta.cambio.ToString(), true, "PESOS");
            lbCambioTexto.Text = resultado;

            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");

        }

        private void InfoUltimaVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(27))
            {
                this.Close();
            }
            if (e.KeyCode.Equals(37))
            {
                SendKeys.Send("{TAB}");
                botonImprimir = 1;
                botonCerrar = 0;
                //btnCerrar.BorderColor = Color.FromArgb(33, 97, 140);
            }
            if (e.KeyCode.Equals(39))
            {
                SendKeys.Send("{TAB}");
                botonCerrar = 1;
                botonImprimir = 0;
                //botonRedondo1.BorderColor = Color.FromArgb(33, 97, 140);
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();
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

        private void botonRedondo1_Enter(object sender, EventArgs e)
        {
            //if (botonImprimir == 1)
            //{
            //    btnCerrar.BackColor = Color.Red;
            //}
            btnCerrar.BackColor = Color.FromArgb(33, 97, 140);

            botonRedondo1.BackColor = Color.White;
        }

        private void InfoUltimaVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void btnCerrar_Enter(object sender, EventArgs e)
        {
            //if (botonCerrar == 1)
            //{
            //    btnCerrar.BackColor = Color.Red;
            //}
            botonRedondo1.BackColor = Color.FromArgb(33, 97, 140);

            btnCerrar.BackColor = Color.White;
        }
    }
}
