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

        private float totalEfectivo = 0f;
        private float totalTarjeta = 0f;
        private float totalVales = 0f;
        private float totalCheque = 0f;
        private float totalTransferencia = 0f;
        private float totalCredito = 0f;

        public Caja()
        {
            InitializeComponent();
        }

        private void Caja_Load(object sender, EventArgs e)
        {
            CargarSaldo();

            txtAgregarDinero.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtEfectivo.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCredito.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTarjeta.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCheque.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTrans.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtVales.KeyPress += new KeyPressEventHandler(SoloDecimales);
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
            
            txtEfectivo.Text = totalEfectivo.ToString();
            txtTarjeta.Text = totalTarjeta.ToString();
            txtCheque.Text = totalCheque.ToString();
            txtVales.Text = totalVales.ToString();
            txtTrans.Text = totalTransferencia.ToString();
            txtCredito.Text = totalCredito.ToString();

            txtEfectivo.Focus();
            txtEfectivo.SelectionStart = txtEfectivo.Text.Length;
            txtEfectivo.SelectionLength = 0;
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

            string[] datos = new string[] { operacion, cantidad.ToString("0.00"), "0", "", fechaOperacion, FormPrincipal.userID.ToString(), cantidad.ToString("0.00"), "0", "0", "0", "0", "0" };

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
            var concepto = txtConcepto.Text;
            var operacion = "retiro";
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var efectivo = ValidarCampos(txtEfectivo.Text);
            var tarjeta = ValidarCampos(txtTarjeta.Text);
            var cheque = ValidarCampos(txtCheque.Text);
            var vales = ValidarCampos(txtVales.Text);
            var trans = ValidarCampos(txtTrans.Text);
            var credito = ValidarCampos(txtCredito.Text);

            var cantidad = efectivo + tarjeta + cheque + vales + trans + credito;

            string[] datos = new string[] {
                operacion, cantidad.ToString("0.00"), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(),
                efectivo.ToString("0.00"), tarjeta.ToString("0.00"), vales.ToString("0.00"), cheque.ToString("0.00"), trans.ToString("0.00"), credito.ToString("0.00")
            };

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
            txtCheque.Text = string.Empty;
            txtConcepto.Text = string.Empty;
            panelAgregar.Visible = false;
            panelRetirar.Visible = false;
            panelMetodos.Visible = true;
        }

        #region Funcion para obtener saldo y calcular metodos de pago
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

            //Cerramos la conexion y el datareader
            dr.Close();
            sql_con.Close();

            tituloSeccion.Text = "CAJA: $" + saldo.ToString("0.00");

            lbTEfectivo.Text = "$" + efectivo.ToString("0.00");
            lbTTarjeta.Text = "$" + tarjeta.ToString("0.00");
            lbTVales.Text = "$" + vales.ToString("0.00");
            lbTCheque.Text = "$" + cheque.ToString("0.00");
            lbTTrans.Text = "$" + trans.ToString("0.00");
            lbTCredito.Text = "$" + credito.ToString("0.00");

            //Variables de clase
            totalEfectivo = efectivo;
            totalTarjeta = tarjeta;
            totalVales = vales;
            totalCheque = cheque;
            totalTransferencia = trans;
            totalCredito = credito;
        }
        #endregion

        private void Caja_Paint(object sender, PaintEventArgs e)
        {
            CargarSaldo();
        }

        private void SoloDecimales(object sender, KeyPressEventArgs e)
        {
            //permite 0-9, eliminar y decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            //verifica que solo un decimal este permitido
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                {
                    e.Handled = true;
                }
            }
        }

        #region Validaciones de los campos para retirar
        private void txtEfectivo_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEfectivo.Text))
            {
                float efectivo = float.Parse(txtEfectivo.Text);

                if (efectivo > totalEfectivo)
                {
                    MensajeCantidad(totalEfectivo, sender);
                }
            }
        }

        private void txtTarjeta_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTarjeta.Text))
            {
                float tarjeta = float.Parse(txtTarjeta.Text);

                if (tarjeta > totalTarjeta)
                {
                    MensajeCantidad(totalTarjeta, sender);
                }
            }
        }

        private void txtVales_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtVales.Text))
            {
                float vales = float.Parse(txtVales.Text);

                if (vales > totalVales)
                {
                    MensajeCantidad(totalVales, sender);
                }
            }
        }

        private void txtCheque_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCheque.Text))
            {
                float cheque = float.Parse(txtCheque.Text);

                if (cheque > totalCheque)
                {
                    MensajeCantidad(totalCheque, sender);
                }
            }
        }

        private void txtTrans_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTrans.Text))
            {
                float trans = float.Parse(txtTrans.Text);

                if (trans > totalTransferencia)
                {
                    MensajeCantidad(totalTransferencia, sender);
                }
            }
        }

        private void txtCredito_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCredito.Text))
            {
                float credito = float.Parse(txtCredito.Text);

                if (credito > totalCredito)
                {
                    MensajeCantidad(totalCredito, sender);
                }
            }
        }

        private void MensajeCantidad(float cantidad, object tb)
        {
            TextBox campo = tb as TextBox;

            MessageBox.Show("La cantidad a retirar no puede ser mayor a $" + cantidad, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            campo.Text = cantidad.ToString();
            campo.SelectionStart = campo.Text.Length;
            campo.SelectionLength = 0;
        }
        #endregion

        private float ValidarCampos(string cantidad)
        {
            float valor = 0f;

            if (!string.IsNullOrWhiteSpace(cantidad))
            {
                valor = float.Parse(cantidad);
            }

            return valor;
        }
    }
}
