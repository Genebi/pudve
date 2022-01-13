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
    public partial class DetalleVenta : Form
    { 
        MetodosBusquedas mb = new MetodosBusquedas();
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public static string cliente = string.Empty;
        public static string nameClienteNameVenta = string.Empty;
        public static int idCliente = 0;
        public static float credito = 0f;

        private float total = 0;
        private float totalMetodos = 0;

        public static int validarNoDuplicarVentas = 0;

        float Total;
        float DineroRecibido ;
        float CambioTotal ;
        public DetalleVenta(float total, string idCliente = "")
        {
            InitializeComponent();
            validarNoDuplicarVentas = 1;

            this.total = total;

            //Obtenemos los datos del cliente en caso de que sea una venta guardada con clientes
            if (!string.IsNullOrWhiteSpace(idCliente))
            {
                int idClienteTmp = Convert.ToInt32(idCliente);

                if (idClienteTmp > 0)
                {
                    var infoCliente = mb.ObtenerDatosCliente(idClienteTmp, FormPrincipal.userID);

                    if (!infoCliente.Count().Equals(0))
                    {
                        lbCliente.Text = infoCliente[0];
                        lbEliminarCliente.Visible = true;

                        DetalleVenta.idCliente = idClienteTmp;
                        DetalleVenta.cliente = infoCliente[0];
                        idCliente = string.Empty;
                    }
                }
            }
        }

        private void DetalleVenta_Load(object sender, EventArgs e)
        {
            txtTotalVenta.Text = "$" + total.ToString("0.00");

            txtEfectivo.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTarjeta.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtVales.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCheque.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTransferencia.KeyPress += new KeyPressEventHandler(SoloDecimales);

            txtEfectivo.KeyDown += new KeyEventHandler(TerminarVenta);
            txtTarjeta.KeyDown += new KeyEventHandler(TerminarVenta);
            txtVales.KeyDown += new KeyEventHandler(TerminarVenta);
            txtCheque.KeyDown += new KeyEventHandler(TerminarVenta);
            txtTransferencia.KeyDown += new KeyEventHandler(TerminarVenta);

            txtEfectivo.PreviewKeyDown += new PreviewKeyDownEventHandler(EventoTab);
            txtTarjeta.PreviewKeyDown += new PreviewKeyDownEventHandler(EventoTab);
            txtVales.PreviewKeyDown += new PreviewKeyDownEventHandler(EventoTab);
            txtCheque.PreviewKeyDown += new PreviewKeyDownEventHandler(EventoTab);
            txtTransferencia.PreviewKeyDown += new PreviewKeyDownEventHandler(EventoTab);

            txtTarjeta.KeyUp += new KeyEventHandler(SumaMetodosPago);
            txtVales.KeyUp += new KeyEventHandler(SumaMetodosPago);
            txtCheque.KeyUp += new KeyEventHandler(SumaMetodosPago);
            txtTransferencia.KeyUp += new KeyEventHandler(SumaMetodosPago);

            txtEfectivo.Text = total.ToString("0.00");
            if (!idCliente.Equals(0))
            {
                lbCliente.Text = cliente;
            }
            else
            {
                lbCliente.Text = "Asignar cliente";
            }
        }

        private double getMayorNumber(List<string> listaNumeros)
        {
            double may, men;
            int i = 1;

            may = men = 0;

            foreach (var item in listaNumeros)
            {
                if (!item.Equals(""))
                {
                    double numero = Convert.ToDouble(item.ToString());
                    if (i.Equals(1))
                    {
                        may = numero;
                        men = numero;
                        i++;
                    }
                    else
                    {
                        if (numero > may)
                        {
                            may = numero;
                        }
                        if (numero < men)
                        {
                            men = numero;
                        }
                    }
                }
            }

            return may;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Ventas venta = new Ventas();
            float pagado = (CantidadDecimal(txtEfectivo.Text) + SumaMetodos() + credito) * 100 / 100;

            List<string> listaCantidades = new List<string>();

            //Comprobamos si las cantidades a pagar son mayores o igual al total de la venta para poder terminarla
            if ((pagado >= total) || credito > 0)
            {
                float totalEfectivo = 0f;

                //Si la suma de todos los metodos de pago excepto el de efectivo es mayor a cero
                //se hace la operacion para sacar el total de efectivo que se guardara en la tabla
                //DetallesVenta
                if ((SumaMetodos() + credito) > 0)
                {
                    totalEfectivo = total - (SumaMetodos() + credito);
                }
                else
                {
                    totalEfectivo = total;
                }

                Ventas.efectivo = totalEfectivo.ToString("0.00");
                Ventas.tarjeta = CantidadDecimal(txtTarjeta.Text).ToString("0.00");
                Ventas.vales = CantidadDecimal(txtVales.Text).ToString("0.00");
                Ventas.cheque = CantidadDecimal(txtCheque.Text).ToString("0.00");
                Ventas.transferencia = CantidadDecimal(txtTransferencia.Text).ToString("0.00");
                Ventas.referencia = txtReferencia.Text;

                listaCantidades.Add(txtEfectivo.Text);
                listaCantidades.Add(txtTarjeta.Text);
                listaCantidades.Add(txtTransferencia.Text);
                listaCantidades.Add(txtCheque.Text);
                listaCantidades.Add(txtVales.Text);

                var mayor = getMayorNumber(listaCantidades);

                float checarEfectivo = 0,
                        checarTarjeta = 0,
                        checarTransferencia = 0,
                        checarCheque = 0,
                        checarVales = 0;

                if (!txtEfectivo.Text.Equals(""))
                {
                    checarEfectivo = float.Parse(txtEfectivo.Text);
                }
                if (!txtTarjeta.Text.Equals(""))
                {
                    checarTarjeta = float.Parse(txtTarjeta.Text);
                }
                if (!txtTransferencia.Text.Equals(""))
                {
                    checarTransferencia = float.Parse(txtTransferencia.Text);
                }
                if (!txtCheque.Text.Equals(""))
                {
                    checarCheque = float.Parse(txtCheque.Text);
                }
                if (!txtVales.Text.Equals(""))
                {
                    checarVales = float.Parse(txtVales.Text);
                }

                Properties.Settings.Default.efectivoRecibido = (float)Convert.ToDouble(checarEfectivo.ToString());
                Properties.Settings.Default.tarjetaRecibido = (float)Convert.ToDouble(checarTarjeta.ToString());
                Properties.Settings.Default.transfRecibido = (float)Convert.ToDouble(checarTransferencia.ToString());
                Properties.Settings.Default.chequeRecibido = (float)Convert.ToDouble(checarCheque.ToString());
                Properties.Settings.Default.valesRecibido = (float)Convert.ToDouble(checarVales.ToString());

                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

                if (checarEfectivo.Equals((float)mayor))
                {
                    Ventas.formaDePagoDeVenta = "Efectivo";
                }
                else if (checarTarjeta.Equals((float)mayor))
                {
                    Ventas.formaDePagoDeVenta = "Tarjeta";
                }
                else if (checarTransferencia.Equals((float)mayor))
                {
                    Ventas.formaDePagoDeVenta = "Transferencia";
                }
                else if (checarCheque.Equals((float)mayor))
                {
                    Ventas.formaDePagoDeVenta = "Cheque";
                }
                else if (checarVales.Equals((float)mayor))
                {
                    Ventas.formaDePagoDeVenta = "Vales";
                }

                if (idCliente == 0)
                {
                    //validarCliente = cliente;
                    cliente = string.Empty;
                }

                if (credito > 0)
                {
                    Ventas.statusVenta = "4";
                    Ventas.formaDePagoDeVenta = "Crédito";
                }
                else
                {
                    Ventas.statusVenta = "1";
                }

                if (Ventas.etiqeutaCliente == "vacio")
                {
                    if (!cliente.Equals(string.Empty))
                    {
                        Ventas.cliente = cliente;
                        Ventas.idCliente = idCliente.ToString();
                    }
                    else
                    {
                        Ventas.cliente = "PUBLICO GENERAL";
                        Ventas.idCliente = "0";
                    }
                }
                else if (Ventas.etiqeutaCliente != "vacio")
                {
                    Ventas.cliente = cliente;
                    Ventas.idCliente = idCliente.ToString();
                }

                
                Ventas.credito = credito.ToString();
                Ventas.botonAceptar = true;

                ListadoVentas lstVentas = Application.OpenForms.OfType<ListadoVentas>().FirstOrDefault();

                if (lstVentas != null)
                {
                    lstVentas.cbTipoVentas.Text = "Ventas pagadas";
                    lstVentas.CargarDatos();
                }

                //idCliente = 0 ;
                //nameClienteNameVenta = string.Empty;
                
                this.Hide();
                this.Close();
            }
            else
            {               
                MessageBox.Show("La cantidades no coinciden con el total a pagar", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var sumaImportes = Ventas.pasarSumaImportes;
            var totalAnticipos = Ventas.pasarTotalAnticipos;

            var anticipoAplicado = (totalAnticipos - sumaImportes);
            if (totalAnticipos > 0)
            {
                var idAnticipo = ListadoAnticipos.obtenerIdAnticipo;
                var consultaImporteOriginal = cn.CargarDatos($"SELECT ImporteOriginal FROM Anticipos WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idAnticipo}'");
                var resultadoImporte = "";

                foreach (DataRow consultaImporOriginal in consultaImporteOriginal.Rows)
                {
                    resultadoImporte = consultaImporOriginal["ImporteOriginal"].ToString();
                }

                if (anticipoAplicado < 0)
                {
                    var actualizarAnticipoAplicado2 = cn.EjecutarConsulta($"UPDATE Anticipos SET AnticipoAplicado = '{resultadoImporte}' WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idAnticipo}'");
                }
                else
                {
                    var actualizarAnticipoAplicado = cn.EjecutarConsulta($"UPDATE Anticipos SET AnticipoAplicado = '{sumaImportes}' + AnticipoAplicado WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idAnticipo}'");
                }
            }
            //validarNoDuplicarVentas = 0;

            //var ticketTemporal = cn.CargarDatos("Select Total, DineroRecibido, CambioTotal FROM ventas WHERE ID ORDER BY ID DESC LIMIT 1");

            //foreach (DataRow item in ticketTemporal.Rows)
            //{
            //    Total = (float)Convert.ToDouble(item[0]);
            //    DineroRecibido = (float)Convert.ToDouble(item[1]);
            //    CambioTotal = (float)Convert.ToDouble(item[2]);
            //}


            //InfoUltimaVenta ticketUltimaVenta = new InfoUltimaVenta();
            //ticketUltimaVenta.ShowDialog();

            //if (resetCantidades.Equals(DialogResult.OK))
            //{
            //    Properties.Settings.Default.efectivoRecibido = 0;
            //    Properties.Settings.Default.tarjetaRecibido = 0;
            //    Properties.Settings.Default.transfRecibido = 0;
            //    Properties.Settings.Default.chequeRecibido = 0;
            //    Properties.Settings.Default.valesRecibido = 0;
            //    Properties.Settings.Default.Save();
            //    Properties.Settings.Default.Reload();
            //}
            //else
            //{
            //    Properties.Settings.Default.efectivoRecibido = 0;
            //    Properties.Settings.Default.tarjetaRecibido = 0;
            //    Properties.Settings.Default.transfRecibido = 0;
            //    Properties.Settings.Default.chequeRecibido = 0;
            //    Properties.Settings.Default.valesRecibido = 0;
            //    Properties.Settings.Default.Save();
            //    Properties.Settings.Default.Reload();
            //}


        }

        private void lbCliente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ListaClientes clientes = new ListaClientes(tipo: 2);

            clientes.FormClosed += delegate
            {
                lbCliente.Text = cliente;

                if (string.IsNullOrWhiteSpace(cliente))
                {
                    lbCliente.Text = "Asignar cliente";
                    lbEliminarCliente.Visible = false;
                    idCliente = 0;
                }
                else
                {
                    lbEliminarCliente.Visible = true;
                }

                //cliente = string.Empty;
            };

            clientes.ShowDialog();

            nameClienteNameVenta = lbCliente.Text;
        }

        private void btnCredito_Click(object sender, EventArgs e)
        {
            AsignarCreditoVenta agregarCredito = new AsignarCreditoVenta(total, SumaMetodos());

            agregarCredito.FormClosed += delegate
            {
                lbTotalCredito.Text = Convert.ToDouble(credito).ToString("0.00");

                CalcularCambio();

                lbCliente.Text = cliente;

                if (string.IsNullOrWhiteSpace(cliente))
                {
                    lbCliente.Text = "Asignar cliente";
                    lbEliminarCliente.Visible = false;
                    idCliente = 0;
                }
                else
                {
                    lbEliminarCliente.Visible = true;
                }
            };

            agregarCredito.ShowDialog();
        }

        //Obtiene el total de todos los metodos de pago excepto el de efectivo y lo usa para calcular
        //el cambio junto a otras cantidades de otros campos
        private void SumaMetodosPago(object sender, KeyEventArgs e)
        {
            float suma = SumaMetodos();

            // Se valida que si se pone solo un punto sin nada mas
            // lo elimine y deje el campo en blanco
            var campo = (TextBox)sender;

            if (campo.Text.Length == 1 && campo.Text.Equals("."))
            {
                campo.Text = string.Empty;
            }

            //Si es mayor al total a pagar vuelve a calcular las cantidades pero sin tomar en cuenta
            //el campo que hizo que fuera mayor a la cantidad a pagar
            if (suma > total)
            {
                TextBox tb = sender as TextBox;

                tb.Text = string.Empty;

                suma = SumaMetodos();
            }

            totalMetodos = suma;

            CalcularCambio();
        }

        //Este metodo suma todas las cantidades de los campos de metodos de pago excepto el de efectivo
        private float SumaMetodos()
        {
            float tarjeta = CantidadDecimal(txtTarjeta.Text);
            float vales = CantidadDecimal(txtVales.Text);
            float cheque = CantidadDecimal(txtCheque.Text);
            float transferencia = CantidadDecimal(txtTransferencia.Text);

            float suma = tarjeta + vales + cheque + transferencia;

            return suma;
        }

        private float CantidadDecimal(string cantidad)
        {
            float resultado = 0f;

            if (string.IsNullOrWhiteSpace(cantidad))
            {
                resultado = 0;
            }
            else
            {
                if (cantidad.Length == 1 && cantidad.Equals("."))
                {
                    return 0;
                }

                resultado = float.Parse(cantidad);
            }

            return resultado;
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

        private void txtEfectivo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEfectivo.TextLength == 1 && txtEfectivo.Text.Equals("."))
            {
                //txtEfectivo.Text = string.Empty;
                txtEfectivo.Text = "0.";
                txtEfectivo.Select(txtEfectivo.Text.Length, 0);
            }

            var totalVenta = float.Parse(txtTotalVenta.Text.Remove(0, 1));
            var totalEfectivo = 0f;

            if (!string.IsNullOrWhiteSpace(txtEfectivo.Text.Trim()))
            {
                totalEfectivo = float.Parse(txtEfectivo.Text.Trim());
            }

            //if (totalVenta != totalEfectivo)
            //{
            //    var totalTarjeta = totalVenta - (totalEfectivo + credito);

            //    if (totalTarjeta < 0)
            //    {
            //        txtTarjeta.Text = string.Empty;
            //    }
            //    else
            //    {
            //        txtTarjeta.Text = totalTarjeta.ToString();
            //    }

            //    if (totalEfectivo > totalVenta)
            //    {
            //        txtTarjeta.Text = string.Empty;
            //    }
            //}
            //else
            //{
            //    txtTarjeta.Text = string.Empty;
            //}

            CalcularCambio();
        }

        private void CalcularCambio()
        {
            double cambio = 0;

            //if (credito >= total)
            //{
            //    cambio = 0;
            //}
            //else
            //{
            //    //El total del campo efecto + la suma de los otros metodos de pago - total de venta
            //    cambio = Convert.ToDouble((CantidadDecimal(txtEfectivo.Text) + totalMetodos + credito) - total);
                
            //    if (cambio < 0)
            //    {
            //        cambio = 0;
            //    }
            //}

            //El total del campo efecto + la suma de los otros metodos de pago - total de venta
            cambio = Convert.ToDouble((CantidadDecimal(txtEfectivo.Text) + totalMetodos + credito) - total);

            if (cambio < 0)
            {
                cambio = 0;
            }

            lbTotalCambio.Text = "$" + cambio.ToString("0.00");
        }

        private void lbEliminarCliente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            credito = 0;
            idCliente = 0;
            nameClienteNameVenta = string.Empty;
            lbTotalCredito.Text = "0.00";
            lbEliminarCliente.Visible = false;
            lbCliente.Text = "Asignar cliente";

            CalcularCambio();
        }

        private void DetalleVenta_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!Ventas.botonAceptar)
            //{

            //}

            // Se limpian las variables
            lbTotalCredito.Text = "0.00";
            idCliente = 0;
            cliente = string.Empty;
            credito = 0;
        }

        private void TerminarVenta(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void DetalleVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.End)
            {
                btnAceptar.PerformClick();
            }

            if (e.KeyData == Keys.Escape)
            {
                Close();
            }
        }

        private void EventoTab(object sender, PreviewKeyDownEventArgs e)
        {
            var campos = new string[] {
                "txtEfectivo", "txtTarjeta", "txtTransferencia",
                "txtCheque", "txtVales"
            };

            if (e.KeyData == Keys.Tab)
            {
                var actual = (TextBox)sender;

                if (actual.SelectionLength > 0)
                {
                    if (campos.Contains(actual.Name))
                    {
                        var posicion = Array.IndexOf(campos, actual.Name);

                        if ((posicion + 1) >= 0 & (posicion + 1) <= 4)
                        {
                            var cantidad = actual.Text.Trim();
                            var siguiente = (TextBox)Controls.Find(campos[posicion + 1], true).First();

                            actual.Text = string.Empty;
                            siguiente.Text = cantidad;
                        }
                    }
                }
            }
        }

        private void txtTarjeta_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTarjeta.TextLength == 1 && txtTarjeta.Text.Equals("."))
            {
                txtTarjeta.Text = "0.";
                txtTarjeta.Select(txtTarjeta.Text.Length, 0);
            }
        }

        private void txtTransferencia_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTransferencia.TextLength == 1 && txtTransferencia.Text.Equals("."))
            {
                txtTransferencia.Text = "0.";
                txtTransferencia.Select(txtTransferencia.Text.Length, 0);
            }
        }

        private void txtCheque_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCheque.TextLength == 1 && txtCheque.Text.Equals("."))
            {
                txtCheque.Text = "0.";
                txtCheque.Select(txtCheque.Text.Length, 0);
            }
        }

        private void txtVales_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtVales.TextLength == 1 && txtVales.Text.Equals("."))
            {
                txtVales.Text = "0.";
                txtVales.Select(txtVales.Text.Length, 0);
            }
        }
    }
}
