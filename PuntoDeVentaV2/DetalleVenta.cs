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
        public static float cambio = 0f;
        public static float restante = 0f;
        private float total = 0;
        private float totalMetodos = 0;

        public static int validarNoDuplicarVentas = 0;

        float Total;
        float DineroRecibido;
        float CambioTotal;

        string nameOfControl = string.Empty;
        string contenidoCantidad = string.Empty;

        bool dioClickEnTextBox = false;

        //mio pruebas a ver si jala xd
        int escredito = 0;
        int primer = 0;
        bool dioClickEnCredito = false;
       

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

            //txtEfectivo.PreviewKeyDown += new PreviewKeyDownEventHandler(EventoTab);
            //txtTarjeta.PreviewKeyDown += new PreviewKeyDownEventHandler(EventoTab);
            //txtVales.PreviewKeyDown += new PreviewKeyDownEventHandler(EventoTab);
            //txtCheque.PreviewKeyDown += new PreviewKeyDownEventHandler(EventoTab);
            //txtTransferencia.PreviewKeyDown += new PreviewKeyDownEventHandler(EventoTab);

            //txtTarjeta.KeyUp += new KeyEventHandler(SumaMetodosPago);
            //txtVales.KeyUp += new KeyEventHandler(SumaMetodosPago);
            //txtCheque.KeyUp += new KeyEventHandler(SumaMetodosPago);
            //txtTransferencia.KeyUp += new KeyEventHandler(SumaMetodosPago);

            txtEfectivo.Text = total.ToString("0.00");
            if (!idCliente.Equals(0))
            {
                lbCliente.Text = cliente;
            }
            else
            {
                lbCliente.Text = "Asignar cliente";
            }

            if (primer == 0)
            {
                txtTotalVenta.Text = total.ToString("C2");
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
            if (idCliente.Equals(0) && !txtCredito.Text.Equals(""))
            {
                MessageBox.Show("Asigné un Cliente para hacer una venta a Crédito", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Ventas venta = new Ventas();
                    float pagado = (CantidadDecimal(txtEfectivo.Text) + SumaMetodos()) * 100 / 100;

            List<string> listaCantidades = new List<string>();

            //Comprobamos si las cantidades a pagar son mayores o igual al total de la venta para poder terminarla
            if ((pagado >= total))
            {
                float totalEfectivo = 0f;

                //Si la suma de todos los metodos de pago excepto el de efectivo es mayor a cero
                //se hace la operacion para sacar el total de efectivo que se guardara en la tabla
                //DetallesVenta
                if (SumaMetodos() > 0)
                {
                    totalEfectivo = total - SumaMetodos();
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
                        checarVales = 0,
                        checarCredito = 0;

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
                if (!txtCredito.Text.Equals(""))
                {
                    checarCredito = float.Parse(txtCredito.Text);
                }

                Properties.Settings.Default.efectivoRecibido = (float)Convert.ToDouble(checarEfectivo.ToString());
                Properties.Settings.Default.tarjetaRecibido = (float)Convert.ToDouble(checarTarjeta.ToString());
                Properties.Settings.Default.transfRecibido = (float)Convert.ToDouble(checarTransferencia.ToString());
                Properties.Settings.Default.chequeRecibido = (float)Convert.ToDouble(checarCheque.ToString());
                Properties.Settings.Default.valesRecibido = (float)Convert.ToDouble(checarVales.ToString());


                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
                
                if (!txtCredito.Text.Equals(""))
                {
                    Ventas.statusVenta = "4";
                    Ventas.formaDePagoDeVenta = "Crédito";
                    credito = (float)Convert.ToDecimal(txtCredito.Text);
                }
                else
                {
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
                    Ventas.statusVenta = "1";
                }
                if (idCliente == 0)
                {
                    //validarCliente = cliente;
                    cliente = string.Empty;
                }

                //if (credito > 0)
                //{
                //    Ventas.statusVenta = "4";
                //    Ventas.formaDePagoDeVenta = "Crédito";
                //}
                //else
                //{
                //    Ventas.statusVenta = "1";
                //}

                if (Ventas.etiqeutaCliente == "vacio")
                {
                    if (!cliente.Equals(string.Empty))
                    {
                        Ventas.cliente = cliente;
                        Ventas.idCliente = idCliente.ToString();
                    }
                    else
                    {
                        string razonSocialPublicoGeneral;
                        int IDPublicoGeneral;
                        using (DataTable dtPublicoGeneral = cn.CargarDatos(cs.BuscarPublicaGeneral()))
                        {
                            if (!dtPublicoGeneral.Rows.Count.Equals(0))
                            {
                                DataRow drPublicoGeneral = dtPublicoGeneral.Rows[0];
                                IDPublicoGeneral = Convert.ToInt32(drPublicoGeneral["ID"].ToString());
                                razonSocialPublicoGeneral = drPublicoGeneral["RazonSocial"].ToString();
                               
                                Ventas.idCliente = IDPublicoGeneral.ToString();
                                //Ventas.ventaGuardada = true;
                                Ventas.cliente = razonSocialPublicoGeneral;
                                
                            }
                            else
                            {
                                var UltimoNumeroCliente = string.Empty;
                                using (DataTable dtUltimocliente = cn.CargarDatos(cs.UltimoNumerodeCliente()))
                                {
                                    if (!dtUltimocliente.Rows.Count.Equals(0))
                                    {
                                        foreach (DataRow item in dtUltimocliente.Rows)
                                        {
                                            var numCliente = Convert.ToInt32(item["NumeroCliente"]);
                                            var longitud = 6 - numCliente.ToString().Length;
                                            if (longitud.Equals(5))
                                            {
                                                UltimoNumeroCliente = $"00000{numCliente + 1}";
                                            }
                                            if (longitud.Equals(4))
                                            {
                                                UltimoNumeroCliente = $"0000{numCliente + 1}";
                                            }
                                            if (longitud.Equals(3))
                                            {
                                                UltimoNumeroCliente = $"000{numCliente + 1}";
                                            }
                                            if (longitud.Equals(2))
                                            {
                                                UltimoNumeroCliente = $"00{numCliente + 1}";
                                            }
                                            if (longitud.Equals(1))
                                            {
                                                UltimoNumeroCliente = $"0{numCliente + 1}";
                                            }
                                            if (longitud.Equals(0))
                                            {
                                                UltimoNumeroCliente = $"{numCliente + 1}";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        UltimoNumeroCliente = "000001";
                                    }
                                }
                                var resultado = cn.EjecutarConsulta(cs.AgregarPublicoGeneral(UltimoNumeroCliente));
                                if (resultado.Equals(1))
                                {
                                    using (DataTable dtNuevoClienteGeneral = cn.CargarDatos(cs.ObtenerDatosClientePublicoGeneral()))
                                    {
                                        if (!dtNuevoClienteGeneral.Rows.Count.Equals(0))
                                        {
                                            DataRow drNuevoPublicoGeneral = dtNuevoClienteGeneral.Rows[0];
                                            IDPublicoGeneral = Convert.ToInt32(drNuevoPublicoGeneral["ID"].ToString());
                                            razonSocialPublicoGeneral = drNuevoPublicoGeneral["RazonSocial"].ToString();
                                            
                                            Ventas.idCliente = IDPublicoGeneral.ToString();
                                            Ventas.cliente = razonSocialPublicoGeneral;
                                        }
                                    }
                                }
                            }
                        }
                        
                        
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
                Ventas.VentaRealizada = true;
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

            if (dioClickEnCredito.Equals(false))
            {
                restante = total;
            }
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
            dioClickEnCredito = true;
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
                
                restante = total - credito;
                txtEfectivo.Text = restante.ToString();
                

            };
            escredito = 1;
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
            float creditotxt = CantidadDecimal(txtCredito.Text);
            float suma = tarjeta + vales + cheque + transferencia+ creditotxt;

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
            float tarjeta = CantidadDecimal(txtTarjeta.Text);
            float vales = CantidadDecimal(txtVales.Text);
            float cheque = CantidadDecimal(txtCheque.Text);
            float transferencia = CantidadDecimal(txtTransferencia.Text);
            float creditotxt = CantidadDecimal(txtCredito.Text);

            float suma = tarjeta + vales + cheque + transferencia;


            if (escredito == 1)
            {
                cambio = (CantidadDecimal(txtEfectivo.Text) + suma + creditotxt) - total;

                if (cambio < 0)
                {
                    cambio = 0;
                }

                lbTotalCambio.Text = "$" + cambio.ToString("0.00");
            }
            else
            {
                cambio = (CantidadDecimal(txtEfectivo.Text) + suma + creditotxt) - total;

                if (cambio < 0)
                {
                    cambio = 0;
                }

                lbTotalCambio.Text = cambio.ToString("C2");
            }

            decimal mandar = Convert.ToDecimal(cambio);
            InformacionVenta informacionVenta = new InformacionVenta((long)Convert.ToDouble(mandar));

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
            Ventas.AutorizacionConfirmada = false;
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
            //if (e.KeyData == Keys.End)
            //{
            //    btnAceptar.PerformClick();
            //}

            if (e.KeyData == Keys.Escape)
            {
                Close();
            }

            //var campos = new string[] {
            //    "txtEfectivo",
            //    "txtTarjeta",
            //    "txtTransferencia",
            //    "txtCheque",
            //    "txtVales"
            //};

            //if (e.KeyData.Equals(Keys.Right))
            //{
            //    var indexCampos = Array.IndexOf(campos, nameOfControl);
            //    var indexAnteriorSiguiente = indexCampos + 1;

            //    if (indexAnteriorSiguiente >= 0 && indexAnteriorSiguiente <= 4)
            //    {
            //        TextBox actual, siguiente;
            //        var textBoxActual = campos[indexCampos].ToString();
            //        var textBoxSiguiente = campos[indexAnteriorSiguiente].ToString();
            //        actual = (TextBox)Controls.Find(textBoxActual, true)[0];
            //        siguiente = (TextBox)Controls.Find(textBoxSiguiente, true)[0];

            //        //obtenerCantidad(actual.Text, textBoxSiguiente);
            //        siguiente.SelectAll();
            //        siguiente.Focus();
            //    }
            //}
            //else if (e.KeyData.Equals(Keys.Left))
            //{
            //    var indexCampos = Array.IndexOf(campos, nameOfControl);
            //    var indexAnteriorSiguiente = indexCampos - 1;

            //    if (indexAnteriorSiguiente >= 0 && indexAnteriorSiguiente <= 4)
            //    {
            //        TextBox actual, anterior;
            //        var textBoxActual = campos[indexCampos].ToString();
            //        var textBoxAnterior = campos[indexAnteriorSiguiente].ToString();
            //        actual = (TextBox)Controls.Find(textBoxActual, true)[0];
            //        anterior = (TextBox)Controls.Find(textBoxAnterior, true)[0];

            //        //obtenerCantidad(actual.Text, textBoxAnterior);
            //        anterior.SelectAll();
            //        anterior.Focus();
            //    }
            //}
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

        private void txtEfectivo_Enter(object sender, EventArgs e)
        {
            var contenidoTexto = txtEfectivo.Text;

            nameOfControl = txtEfectivo.Name.ToString();

            if (string.IsNullOrWhiteSpace(contenidoTexto) && dioClickEnTextBox.Equals(false))
            {
                obtenerCantidad(txtEfectivo.Text, nameOfControl);
            }

            if (dioClickEnTextBox)
            {
                obtenerCantidad(txtEfectivo.Text, nameOfControl);
                dioClickEnTextBox = false;
            }
        }

        private void txtTarjeta_Enter(object sender, EventArgs e)
        {
            var contenidoTexto = txtEfectivo.Text;

            nameOfControl = txtTarjeta.Name.ToString();

            if (dioClickEnTextBox)
            {
                obtenerCantidad(txtTarjeta.Text, nameOfControl);
                dioClickEnTextBox = false;
            }
        }

        private void txtTransferencia_Enter(object sender, EventArgs e)
        {
            var contenidoTexto = txtEfectivo.Text;

            nameOfControl = txtTransferencia.Name.ToString();

            if (dioClickEnTextBox)
            {
                obtenerCantidad(txtTransferencia.Text, nameOfControl);
                dioClickEnTextBox = false;
            }
        }

        private void txtCheque_Enter(object sender, EventArgs e)
        {
            var contenidoTexto = txtEfectivo.Text;

            nameOfControl = txtCheque.Name.ToString();

            if (dioClickEnTextBox)
            {
                obtenerCantidad(txtCheque.Text, nameOfControl);
                dioClickEnTextBox = false;
            }
        }

        private void txtVales_Enter(object sender, EventArgs e)
        {
            var contenidoTexto = txtEfectivo.Text;

            nameOfControl = txtVales.Name.ToString();

            if (dioClickEnTextBox)
            {
                obtenerCantidad(txtVales.Text, nameOfControl);
                dioClickEnTextBox = false;
            }
        }

        private void obtenerCantidad(string cotenidoTextBox, string nombreControl)
        {
            if (!string.IsNullOrWhiteSpace(txtEfectivo.Text.ToString()))
            {
                contenidoCantidad = txtEfectivo.Text;
            }
            else if (!string.IsNullOrWhiteSpace(txtTarjeta.Text.ToString()))
            {
                contenidoCantidad = txtTarjeta.Text;
            }
            else if (!string.IsNullOrWhiteSpace(txtTransferencia.Text.ToString()))
            {
                contenidoCantidad = txtTransferencia.Text;
            }
            else if (!string.IsNullOrWhiteSpace(txtCheque.Text.ToString()))
            {
                contenidoCantidad = txtCheque.Text;
            }
            else if (!string.IsNullOrWhiteSpace(txtVales.Text.ToString()))
            {
                contenidoCantidad = txtVales.Text;
            }

            limpiarTextBoxMetodoDePago();

            if (txtEfectivo.Name.Equals(nombreControl))
            {
                txtEfectivo.Text = contenidoCantidad;
            }
            else if (txtTarjeta.Name.Equals(nombreControl))
            {
                txtTarjeta.Text = contenidoCantidad;
            }
            else if (txtTransferencia.Name.Equals(nombreControl))
            {
                txtTransferencia.Text = contenidoCantidad;
            }
            else if (txtCheque.Name.Equals(nombreControl))
            {
                txtCheque.Text = contenidoCantidad;
            }
            else if (txtVales.Name.Equals(nombreControl))
            {
                txtVales.Text = contenidoCantidad;
            }
        }

        private void limpiarTextBoxMetodoDePago()
        {
            txtEfectivo.Clear();
            txtTarjeta.Clear();
            txtTransferencia.Clear();
            txtCheque.Clear();
            txtVales.Clear();
        }

        private void txtVales_Click(object sender, EventArgs e)
        {
            //dioClickEnTextBox = true;
            //nameOfControl = txtVales.Name.ToString();
            //txtVales_Enter(sender, e);
            //txtVales.SelectAll();
            //txtVales.Focus();
        }

        private void txtCheque_Click(object sender, EventArgs e)
        {
            //dioClickEnTextBox = true;
            //nameOfControl = txtCheque.Name.ToString();
            //txtCheque_Enter(sender, e);
            //txtCheque.SelectAll();
            //txtCheque.Focus();
        }

        private void txtTransferencia_Click(object sender, EventArgs e)
        {
            //dioClickEnTextBox = true;
            //nameOfControl = txtTransferencia.Name.ToString();
            //txtTransferencia_Enter(sender, e);
            //txtTransferencia.SelectAll();
            //txtTransferencia.Focus();
        }

        private void txtTarjeta_Click(object sender, EventArgs e)
        {
            //dioClickEnTextBox = true;
            //nameOfControl = txtTarjeta.Name.ToString();
            //txtTarjeta_Enter(sender, e);
            //txtTarjeta.SelectAll();
            //txtTarjeta.Focus();
        }

        private void txtEfectivo_Click(object sender, EventArgs e)
        {
            //dioClickEnTextBox = true;
            //nameOfControl = txtEfectivo.Name.ToString();
            //txtEfectivo_Enter(sender, e);
            //txtEfectivo.SelectAll();
            //txtEfectivo.Focus();
        }

        private void txtReferencia_Enter(object sender, EventArgs e)
        {
            nameOfControl = txtReferencia.Name.ToString();
        }

        private void lbEliminarCliente_Enter(object sender, EventArgs e)
        {
            nameOfControl = lbEliminarCliente.Name.ToString();
        }

        private void lbCliente_Enter(object sender, EventArgs e)
        {
            nameOfControl = lbCliente.Name.ToString();
        }

        private void btnCredito_Enter(object sender, EventArgs e)
        {
            nameOfControl = btnCredito.Name.ToString();
        }

        private void btnAceptar_Enter(object sender, EventArgs e)
        {
            nameOfControl = btnAceptar.Name.ToString();
        }

        private void txtEfectivo_TextChanged(object sender, EventArgs e)
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



                CalcularCambio();
                RestaPrecio();
            
            
        }

        private void RestaPrecio()
        {
            float efectivo = CantidadDecimal(txtEfectivo.Text);
            float tarjeta = CantidadDecimal(txtTarjeta.Text);
            float vales = CantidadDecimal(txtVales.Text);
            float cheque = CantidadDecimal(txtCheque.Text);
            float transferencia = CantidadDecimal(txtTransferencia.Text);
            float creditotxt = CantidadDecimal(txtCredito.Text);

            if (escredito == 1)
            {
                float suma = efectivo + tarjeta + vales + cheque + transferencia + creditotxt;
                float restante = total - suma;

                if (restante < 0)
                {
                    txtTotalVenta.Text = "0.00";
                }
                else
                {
                    txtTotalVenta.Text = restante.ToString("C2");
                }

            }
            else
            {
                float suma = efectivo + tarjeta + vales + cheque + transferencia + creditotxt;
                float restante = total - suma;
                if (restante < 0)
                {
                    txtTotalVenta.Text = "0.00";
                }
                else
                {
                    txtTotalVenta.Text = restante.ToString("C2");
                }

            }

           

        }

        private void txtTransferencia_TextChanged(object sender, EventArgs e)
        {
            CalcularCambio();
            RestaPrecio();
        }

        private void txtTarjeta_TextChanged(object sender, EventArgs e)
        {
            CalcularCambio();
            RestaPrecio();
        }

        private void txtCheque_TextChanged(object sender, EventArgs e)
        {
            CalcularCambio();
            RestaPrecio();
        }

        private void txtVales_TextChanged(object sender, EventArgs e)
        {
            CalcularCambio();
            RestaPrecio();
        }

        private void txtEfectivo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Right))
                SendKeys.Send("{TAB}");

            
        }

        private void txtTarjeta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Right))
                SendKeys.Send("{TAB}");

            if (e.KeyCode.Equals(Keys.Left))
                SendKeys.Send("+{TAB}");
        }

        private void txtTransferencia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Right))
                SendKeys.Send("{TAB}");

            if (e.KeyCode.Equals(Keys.Left))
                SendKeys.Send("+{TAB}");
        }

        private void txtCheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Right))
                SendKeys.Send("{TAB}");

            if (e.KeyCode.Equals(Keys.Left))
                SendKeys.Send("+{TAB}");
        }

        private void txtVales_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode.Equals(Keys.Left))
                SendKeys.Send("+{TAB}");
        }

        private void txtCredito_Enter(object sender, EventArgs e)
        {
            var contenidoTexto = txtEfectivo.Text;

            nameOfControl = txtCredito.Name.ToString();

            if (dioClickEnTextBox)
            {
                obtenerCantidad(txtCredito.Text, nameOfControl);
                dioClickEnTextBox = false;
            }
        }

        private void txtCredito_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Left))
                SendKeys.Send("+{TAB}");
        }

        private void txtCredito_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCredito.TextLength == 1 && txtCredito.Text.Equals("."))
            {
                txtCredito.Text = "0.";
                txtCredito.Select(txtCredito.Text.Length, 0);
            }
        }
        private void txtCredito_TextChanged(object sender, EventArgs e)
        {
            CalcularCambio();
            RestaPrecio();
        }

       
    }
}
