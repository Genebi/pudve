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
    public partial class AsignarAbonos : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        private int idVenta = 0;
        private float totalOriginal = 0f;
        private float totalPendiente = 0f;

        public AsignarAbonos(int idVenta, float totalOriginal)
        {
            InitializeComponent();

            this.idVenta = idVenta;
            this.totalOriginal = totalOriginal;
        }

        private void AsignarAbonos_Load(object sender, EventArgs e)
        {
            //Asignamos el evento para solo aceptar cantidades decimales
            txtEfectivo.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTarjeta.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtVales.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCheque.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTransferencia.KeyPress += new KeyPressEventHandler(SoloDecimales);

            var detalles = mb.ObtenerDetallesVenta(idVenta, FormPrincipal.userID);
            totalPendiente = float.Parse(detalles[2]);
            txtTotalOriginal.Text = "$" + totalOriginal.ToString("0.00");

            //Comprobamos que no existan abonos
            var existenAbonos = (bool)cn.EjecutarSelect($"SELECT * FROM Abonos WHERE IDVenta = {idVenta} AND IDUsuario = {FormPrincipal.userID}");

            if (!existenAbonos)
            {
                txtPendiente.Text = "$" + totalPendiente.ToString("0.00");
            }
            else
            {
                var abonado = mb.ObtenerTotalAbonado(idVenta, FormPrincipal.userID);
                var restante = totalPendiente - abonado;
                txtPendiente.Text = "$" + restante.ToString("0.00");
                totalPendiente = restante;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarCantidades())
            {
                var efectivo = CantidadDecimal(txtEfectivo.Text);
                var tarjeta = CantidadDecimal(txtTarjeta.Text);
                var vales = CantidadDecimal(txtVales.Text);
                var cheque = CantidadDecimal(txtCheque.Text);
                var transferencia = CantidadDecimal(txtTransferencia.Text);
                var referencia = txtReferencia.Text;
                var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                var totalAbonado = efectivo + tarjeta + vales + cheque + transferencia;

                string[] datos = new string[] {
                    idVenta.ToString(), FormPrincipal.userID.ToString(), totalAbonado.ToString(), efectivo.ToString(), tarjeta.ToString(),
                    vales.ToString(), cheque.ToString(), transferencia.ToString(), referencia, fechaOperacion
                };

                int resultado = cn.EjecutarConsulta(cs.GuardarAbonos(datos));

                if (resultado > 0)
                {
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show("La cantidad abonada es mayor al total pendiente de pago", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private float CantidadDecimal(string cantidad)
        {
            var valor = 0f;

            if (string.IsNullOrEmpty(cantidad))
            {
                valor = 0;
            }
            else
            {
                valor = float.Parse(cantidad);
            }

            return valor;
        }

        private bool ValidarCantidades()
        {
            var efectivo = CantidadDecimal(txtEfectivo.Text);
            var tarjeta = CantidadDecimal(txtTarjeta.Text);
            var vales = CantidadDecimal(txtVales.Text);
            var cheque = CantidadDecimal(txtCheque.Text);
            var transferencia = CantidadDecimal(txtTransferencia.Text);
            var total = efectivo + tarjeta + vales + cheque + transferencia;

            if (total <= totalPendiente)
            {
                return true;
            }

            return false;
        }

        private void lbVerAbonos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Ver abonos");
        }
    }
}
