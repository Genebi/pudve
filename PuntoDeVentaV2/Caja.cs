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
            panelMetodos.Visible = false;
            gbContenedor.Visible = true;
            gbContenedor.Text = "DEPOSITAR DINERO";

            panelRetirar.Visible = false;
            panelAgregar.Visible = true;
            txtAgregarDinero.Focus();
        }

        private void btnRetirarDinero_Click(object sender, EventArgs e)
        {
            panelMetodos.Visible = false;
            gbContenedor.Visible = true;
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

            //var actual = cn.ObtenerSaldoActual(FormPrincipal.userID);
            //var total = actual + cantidad;

            string[] datos = new string[] {
                operacion, cantidad.ToString("0.00"), "0", "", fechaOperacion, FormPrincipal.userID.ToString(),
                cantidad.ToString("0.00"), "0", "0", "0", "0", "0"
            };

            int resultado = cn.EjecutarConsulta(cs.OperacionCaja(datos));

            if (resultado > 0)
            {
                LimpiarCampos();

                CargarSaldo();
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

            //var actual = cn.ObtenerSaldoActual(FormPrincipal.userID);
            //var total = actual - cantidad;

            string[] datos = new string[] { operacion, cantidad.ToString("0.00"), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString() };

            int resultado = cn.EjecutarConsulta(cs.OperacionCaja(datos));

            if (resultado > 0)
            {
                LimpiarCampos();

                CargarSaldo();
            }
        }

        private void LimpiarCampos()
        {
            gbContenedor.Text = string.Empty;
            gbContenedor.Visible = false;
            txtAgregarDinero.Text = string.Empty;
            txtRetirarDinero.Text = string.Empty;
            txtConcepto.Text = string.Empty;
            panelAgregar.Visible = false;
            panelRetirar.Visible = false;
            panelMetodos.Visible = true;
        }

        private void CargarSaldo()
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();

            var consulta = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID}";

            sql_cmd = new SQLiteCommand(consulta, sql_con);

            dr = sql_cmd.ExecuteReader();

            float saldo = 0f;
            float efectivo = 0f;
            float tarjeta = 0f;
            float vales = 0f;
            float cheque = 0f;
            float trans = 0f;
            float credito = 0f;

            while (dr.Read())
            {
                string operacion = dr.GetValue(dr.GetOrdinal("Operacion")).ToString();

                if (operacion == "deposito")
                {
                    saldo += float.Parse(dr.GetValue(dr.GetOrdinal("Cantidad")).ToString());

                    efectivo += float.Parse(dr.GetValue(dr.GetOrdinal("Efectivo")).ToString());
                    tarjeta += float.Parse(dr.GetValue(dr.GetOrdinal("Tarjeta")).ToString());
                    vales += float.Parse(dr.GetValue(dr.GetOrdinal("Vales")).ToString());
                    cheque += float.Parse(dr.GetValue(dr.GetOrdinal("Cheque")).ToString());
                    trans += float.Parse(dr.GetValue(dr.GetOrdinal("Transferencia")).ToString());
                    credito += float.Parse(dr.GetValue(dr.GetOrdinal("Credito")).ToString());
                }
                
                if (operacion == "retiro")
                {
                    saldo -= float.Parse(dr.GetValue(dr.GetOrdinal("Cantidad")).ToString());

                    efectivo -= float.Parse(dr.GetValue(dr.GetOrdinal("Efectivo")).ToString());
                    tarjeta -= float.Parse(dr.GetValue(dr.GetOrdinal("Tarjeta")).ToString());
                    vales -= float.Parse(dr.GetValue(dr.GetOrdinal("Vales")).ToString());
                    cheque -= float.Parse(dr.GetValue(dr.GetOrdinal("Cheque")).ToString());
                    trans -= float.Parse(dr.GetValue(dr.GetOrdinal("Transferencia")).ToString());
                    credito -= float.Parse(dr.GetValue(dr.GetOrdinal("Credito")).ToString());
                }
            }

            dr.Close();
            sql_con.Close();

            //saldoActual = cn.ObtenerSaldoActual(FormPrincipal.userID);

            tituloSeccion.Text = "CAJA: $" + saldo.ToString("0.00");

            lbTEfectivo.Text = "$" + efectivo.ToString("0.00");
            lbTTarjeta.Text = "$" + tarjeta.ToString("0.00");
            lbTVales.Text = "$" + vales.ToString("0.00");
            lbTCheque.Text = "$" + cheque.ToString("0.00");
            lbTTrans.Text = "$" + trans.ToString("0.00");
            lbTCredito.Text = "$" + credito.ToString("0.00");
        }

        private void Caja_Paint(object sender, PaintEventArgs e)
        {
            CargarSaldo();
        }
    }
}
