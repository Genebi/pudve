using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class Caja : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        float saldoActual = 0f;

        public string SaldoActual { get; set; }

        public Caja()
        {
            InitializeComponent();
        }

        private void Caja_Load(object sender, EventArgs e)
        {
            CargarSaldo();
        }

        private void btnAgregarDinero_Click(object sender, EventArgs e)
        {
            gbContenedor.Text = "DEPOSITAR DINERO";

            panelRetirar.Visible = false;
            panelAgregar.Visible = true;
            txtAgregarDinero.Focus();
        }

        private void btnRetirarDinero_Click(object sender, EventArgs e)
        {
            gbContenedor.Text = "RETIRAR DINERO";

            panelAgregar.Visible = false;
            panelRetirar.Visible = true;
            txtRetirarDinero.Focus();
        }

        private void btnCancelarDeposito_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnAceptarDeposito_Click(object sender, EventArgs e)
        {
            var cantidad = Convert.ToDouble(txtAgregarDinero.Text);
            var operacion = "deposito";
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var actual = cn.ObtenerSaldoActual(FormPrincipal.userID);
            var total = actual + cantidad;

            string[] datos = new string[] { operacion, cantidad.ToString("0.00"), total.ToString("0.00"), "", fechaOperacion, FormPrincipal.userID.ToString() };

            int resultado = cn.EjecutarConsulta(cs.OperacionCaja(datos));

            if (resultado > 0)
            {
                LimpiarCampos();

                tituloSeccion.Text = "CAJA: $" + total.ToString("0.00");
            }
        }

        private void btnCancelarRetirar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnAceptarRetirar_Click(object sender, EventArgs e)
        {
            var cantidad = Convert.ToDouble(txtRetirarDinero.Text);
            var concepto = txtConcepto.Text;
            var operacion = "retiro";
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var actual = cn.ObtenerSaldoActual(FormPrincipal.userID);
            var total = actual - cantidad;

            string[] datos = new string[] { operacion, cantidad.ToString("0.00"), total.ToString("0.00"), concepto, fechaOperacion, FormPrincipal.userID.ToString() };

            int resultado = cn.EjecutarConsulta(cs.OperacionCaja(datos));

            if (resultado > 0)
            {
                LimpiarCampos();

                tituloSeccion.Text = "CAJA: $" + total.ToString("0.00");
            }
        }

        private void LimpiarCampos()
        {
            gbContenedor.Text = string.Empty;
            txtAgregarDinero.Text = string.Empty;
            txtRetirarDinero.Text = string.Empty;
            txtConcepto.Text = string.Empty;
            panelAgregar.Visible = false;
            panelRetirar.Visible = false;
        }

        private void CargarSaldo()
        {
            saldoActual = cn.ObtenerSaldoActual(FormPrincipal.userID);

            tituloSeccion.Text = "CAJA: $" + saldoActual.ToString("0.00");
        }

        private void Caja_Paint(object sender, PaintEventArgs e)
        {
            CargarSaldo();
        }
    }
}
