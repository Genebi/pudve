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
    public partial class AgregarRetirarDinero : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        // 0 = Depositar
        // 1 = Retirar
        int operacion = 0;

        public AgregarRetirarDinero(int operacion = 0)
        {
            InitializeComponent();

            this.operacion = operacion;
        }

        private void AgregarRetirarDinero_Load(object sender, EventArgs e)
        {
            if (operacion == 0)
            {
                lbTitulo.Text = "Cantidad a depositar";
                lbSubtitulo.Text = "Concepto del depósito";
            }
            else if (operacion == 1)
            {
                lbTitulo.Text = "Cantidad a retirar";
                lbSubtitulo.Text = "Concepto del retiro";
            }


            txtEfectivo.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCredito.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTarjeta.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCheque.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTrans.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtVales.KeyPress += new KeyPressEventHandler(SoloDecimales);
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var tipoOperacion = string.Empty;

            // Depositar
            if (operacion == 0)
            {
                tipoOperacion = "deposito";
            }

            // Retirar
            if (operacion == 1)
            {
                tipoOperacion = "retiro";
            }

            var concepto = txtConcepto.Text;
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var efectivo = ValidarCampos(txtEfectivo.Text);
            var tarjeta = ValidarCampos(txtTarjeta.Text);
            var cheque = ValidarCampos(txtCheque.Text);
            var vales = ValidarCampos(txtVales.Text);
            var trans = ValidarCampos(txtTrans.Text);
            var credito = ValidarCampos(txtCredito.Text);

            var cantidad = efectivo + tarjeta + cheque + vales + trans + credito;

            string[] datos = new string[] {
                tipoOperacion, cantidad.ToString("0.00"), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(),
                efectivo.ToString("0.00"), tarjeta.ToString("0.00"), vales.ToString("0.00"), cheque.ToString("0.00"),
                trans.ToString("0.00"), credito.ToString("0.00"), "0"
            };

            int resultado = cn.EjecutarConsulta(cs.OperacionCaja(datos));

            if (resultado > 0)
            {
                Dispose();
            }
        }

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
