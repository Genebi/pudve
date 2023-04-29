using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        private float totalMetodos = 0f;
        private bool existenAbonos = false;
        private string siguienteFechaAbono = string.Empty;
        private int mocho = 0;
        private string ticketGenerado = string.Empty;
        private string rutaTicketGenerado = string.Empty;
        private string IDCliente = string.Empty;
        private string tipoCredito = string.Empty;

        //MIOOOOOOOOOOOOO
        float efectivo;
        float tarjeta;
        float vales;
        float cheque;
        float transferencia;
        float restante;
        float cambio;
        string nameOfControl = string.Empty;
        float perdonado;
        float restanteDePago = 0;
        decimal intereses;
        decimal abonoTotal = 0;
        decimal saldo = 0;
        decimal porcentajeDeInteres;
        decimal dias = 0;
        decimal restadodeinteres = 0;
        decimal restadodeabonos = 0;
        decimal diasTrascurridos = 0;
        decimal interesPorDia;
        DateTime lameraFecha;
        DateTime interesesDesde;
        decimal calculoIntereses = 0;
        string saldoAnterior = string.Empty;

        public AsignarAbonos(int idVenta, float totalOriginal, string tipoCredito)
        {
            InitializeComponent();

            this.idVenta = idVenta;
            this.totalOriginal = totalOriginal;
            this.tipoCredito = tipoCredito;
        }

        private void AsignarAbonos_Load(object sender, EventArgs e)
        {
            //Asignamos el evento para solo aceptar cantidades decimales
            txtEfectivo.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTarjeta.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtVales.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCheque.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTransferencia.KeyPress += new KeyPressEventHandler(SoloDecimales);

            //Terminar abono presionando Enter
            txtEfectivo.KeyDown += new KeyEventHandler(TerminarVenta);
            txtTarjeta.KeyDown += new KeyEventHandler(TerminarVenta);
            txtVales.KeyDown += new KeyEventHandler(TerminarVenta);
            txtCheque.KeyDown += new KeyEventHandler(TerminarVenta);
            txtTransferencia.KeyDown += new KeyEventHandler(TerminarVenta);

            //Suma de los metodos de pago excepto efectivo
            //txtTarjeta.KeyUp += new KeyEventHandler(SumaMetodosPago);
            //txtVales.KeyUp += new KeyEventHandler(SumaMetodosPago);
            //txtCheque.KeyUp += new KeyEventHandler(SumaMetodosPago);
            //txtTransferencia.KeyUp += new KeyEventHandler(SumaMetodosPago);

            var detalles = mb.ObtenerDetallesVenta(idVenta, FormPrincipal.userID);
            totalPendiente = float.Parse(detalles[2]);
            txtTotalOriginal.Text = totalOriginal.ToString("C2");

            using (DataTable dtBuscarCliente = cn.CargarDatos($"SELECT IDCliente FROM ventas WHERE ID = {idVenta}"))
            {
                IDCliente = dtBuscarCliente.Rows[0]["IDCliente"].ToString();
            }

            //Comprobamos que no existan abonos
            existenAbonos = (bool)cn.EjecutarSelect($"SELECT * FROM Abonos WHERE IDVenta = {idVenta} AND IDUsuario = {FormPrincipal.userID}");

            //Restamos los abonos mochos con interes
            using (DataTable dtSumadelosabonitosmochoshehe = cn.CargarDatos($"SELECT SUM(Total) AS abonitos FROM Abonos WHERE IDventa ={idVenta} AND intereses > 0"))
            {
                if (!String.IsNullOrEmpty(dtSumadelosabonitosmochoshehe.Rows[0]["abonitos"].ToString()))
                {
                    restadodeinteres = Decimal.Parse(dtSumadelosabonitosmochoshehe.Rows[0]["abonitos"].ToString());
                }
            }

            //Restamos los abonos mochos sin interes
            using (DataTable dtSumadelosabonitosmochoshehe = cn.CargarDatos($"SELECT SUM(Total) AS abonitos FROM Abonos WHERE IDventa ={idVenta} AND intereses = 0 AND NOT(referencia='Enganche')"))
            {
                if (!String.IsNullOrEmpty(dtSumadelosabonitosmochoshehe.Rows[0]["abonitos"].ToString()))
                {
                    restadodeabonos = Decimal.Parse(dtSumadelosabonitosmochoshehe.Rows[0]["abonitos"].ToString());
                }
            }

            if (!existenAbonos)
            {
                txtPendiente.Text = totalPendiente.ToString("C2");
                restanteDePago = totalPendiente;
                intereses = calcularIntereses(restanteDePago);
                abonoTotal = (abonoTotal + intereses);
                //if ((intereses - restadodeinteres) < 0)
                //{
                //    abonoTotal = (abonoTotal + intereses) - restadodeinteres;
                //    intereses = 0;
                //}
                //else
                //{
                //    intereses -= restadodeinteres;
                //}
                restanteDePago += (float)intereses;
                txtPendiente.Text = restanteDePago.ToString("C2");

            }
            else
            {
                var abonado = mb.ObtenerTotalAbonado(idVenta, FormPrincipal.userID);
                restanteDePago = totalPendiente - abonado;
                intereses = calcularIntereses(restanteDePago);
                abonoTotal = (abonoTotal + intereses);
                //if ((intereses - restadodeinteres) < 0)
                //{
                //    abonoTotal = (abonoTotal + intereses) - restadodeinteres;
                //    intereses = 0;
                //}
                //else
                //{
                //    intereses -= restadodeinteres;
                //}
                abonoTotal -= restadodeabonos;
                restanteDePago += (float)intereses;
                txtPendiente.Text = restanteDePago.ToString("C2");
                totalPendiente = restanteDePago;
            }

            bool ultimopago = false;
            using (DataTable dtReglasCreditoVenta = cn.CargarDatos($"SELECT * FROM reglasCreditoVenta WHERE IDVenta = {idVenta}"))
            {
                if (dtReglasCreditoVenta.Rows[0]["creditoPerdon"].ToString().Equals("1"))
                {
                    llPerdonarInteres.Visible = true;
                }

                if (Decimal.Parse(dtReglasCreditoVenta.Rows[0]["creditoMinimoAbono"].ToString()) <= (decimal)restanteDePago)
                {
                    //using (DataTable dtAbonos = cn.CargarDatos($"SELECT FechaOperacion FROM abonos WHERE IDVenta = {idVenta} AND estado = 1 ORDER BY FechaOperacion DESC LIMIT 1 "))
                    //{
                    //    int fechasAtrasadas = 0;
                    //    if (dtAbonos.Rows.Count > 0)
                    //    {
                    bool esasigue = true;
                    foreach (var fecha in dtReglasCreditoVenta.Rows[0]["FechaInteres"].ToString().Split('%'))
                    {
                        if (DateTime.Parse(fecha) >= lameraFecha && DateTime.Parse(fecha) < DateTime.Now)
                        {
                            abonoTotal = abonoTotal + decimal.Parse(dtReglasCreditoVenta.Rows[0]["creditoMinimoAbono"].ToString());
                        }

                        if (DateTime.Parse(fecha) > DateTime.Now)
                        {
                            if (esasigue)
                            {
                                lblSiguienteAbono.Text = "Siguiente abono: " + DateTime.Parse(fecha).ToString("yyyy/MM/dd") + " por: " + decimal.Parse(dtReglasCreditoVenta.Rows[0]["creditoMinimoAbono"].ToString()).ToString("C2"); 
                                esasigue = false;
                            }
                        }
                        else
                        {
                            lblSiguienteAbono.Text = "Abono final";
                        }
                    }
                    //    }
                    //}
                    if (abonoTotal < 0)
                    {
                        //Esta adelantando pero aun no es el ultimo pago, le damos el minimo de abono
                        abonoTotal = 0;
                    }
                    ////Restamos los abonos mochos
                    //using (DataTable dtSumadelosabonitosmochoshehe = cn.CargarDatos($"SELECT SUM(Total) AS abonitos FROM  WHERE IDventa ={idVenta}"))
                    //{
                    //    if (!String.IsNullOrEmpty(dtSumadelosabonitosmochoshehe.Rows[0]["abonitos"].ToString()))
                    //    {
                    //        restadodeinteres= Decimal.Parse(dtSumadelosabonitosmochoshehe.Rows[0]["abonitos"].ToString());
                    //        if (restadodeinteres>0)
                    //        {
                    //            abonoTotal = abonoTotal - (restadodeinteres * -1);
                    //        }
                    //        //abonoTotal = abonoTotal - Decimal.Parse(dtSumadelosabonitosmochoshehe.Rows[0]["abonitos"].ToString());
                    //    }
                    //}
                    lblTotalAbono.Text = abonoTotal.ToString("C2");
                }
                else
                {
                    ultimopago = true;
                    abonoTotal = (decimal)totalPendiente;
                    lblTotalAbono.Text = totalPendiente.ToString("C2");
                    cbAdelanto.Enabled = false;
                }
                int dias = 0;
                switch (dtReglasCreditoVenta.Rows[0]["creditoperiodocobro"].ToString())
                {
                    case "Mensual":
                        dias = 30;
                        break;
                    case "Quincenal":
                        dias = 15;
                        break;
                    case "Semanal":
                        dias = 7;
                        break;
                    default:
                        break;
                }
                //siguienteFechaAbono = DateTime.Today.AddDays(dias + 1).ToString("yyyy-MM-dd");
                saldoAnterior = txtPendiente.Text;
            }
            txtIntereses.Text = intereses.ToString("C2");
            if (intereses > 0)
            {
                //lblFechaAnterior.Text = $"({(Int32)diasTrascurridos} días\n";
                lblFechaAnterior.Text += $"(desde: {interesesDesde.ToString("yyyy-MM-dd")}";
                lblFechaAnterior.Visible = true;
            }
            if (ultimopago)
            {
                lblPendiente.Text = (abonoTotal).ToString("C2");
                lblabonominimo.Text = (abonoTotal).ToString("C2");
            }
            else
            {
                lblPendiente.Text = (abonoTotal + intereses).ToString("C2");
                lblabonominimo.Text = (abonoTotal + intereses).ToString("C2");
            }
        }
        private decimal calcularIntereses(float restante)
        {
            saldo = (decimal)restante;
            decimal restanteInteres = 0;
            Conexion cn = new Conexion();
            calculoIntereses = 0;
            using (DataTable dtReglasCreditoVenta = cn.CargarDatos($"SELECT * FROM reglasCreditoVenta WHERE IDVenta = {idVenta}"))
            {
                using (DataTable dtAbonos = cn.CargarDatos($"SELECT FechaOperacion,restanteInteres FROM abonos WHERE IDVenta = {idVenta} AND NOT(referencia = 'enganche') ORDER BY FechaOperacion DESC LIMIT 1 "))//Aqui se ignora el "abono" inicial que se hace cuando se pone algun otro modo de pago en venta.
                {
                    if (dtAbonos.Rows.Count > 0)
                    {
                        lameraFecha = DateTime.Parse(dtAbonos.Rows[0]["FechaOperacion"].ToString());
                        restanteInteres = decimal.Parse(dtAbonos.Rows[0]["restanteInteres"].ToString());
                    }
                    else
                    {
                        lameraFecha = DateTime.Parse(dtReglasCreditoVenta.Rows[0]["FechaApertura"].ToString());
                    }
                }


                //Fecha del ultimo abono para que el cliente no se surre
                using (DataTable dtAbonos = cn.CargarDatos($"SELECT FechaOperacion,restanteInteres FROM abonos WHERE IDVenta = {idVenta} AND estado = '1' AND NOT(referencia = 'enganche') ORDER BY FechaOperacion DESC LIMIT 1 "))//Aqui se ignora el "abono" inicial que se hace cuando se pone algun otro modo de pago en venta.
                {
                    if (dtAbonos.Rows.Count > 0)
                    {
                        interesesDesde = DateTime.Parse(dtAbonos.Rows[0]["FechaOperacion"].ToString());
                    }
                    else
                    {
                        interesesDesde = DateTime.Parse(dtReglasCreditoVenta.Rows[0]["FechaApertura"].ToString());
                    }
                }

                //Ya se cobra interes
                int test = Int32.Parse(dtReglasCreditoVenta.Rows[0]["creditodiassincobro"].ToString());
                if (DateTime.Now.Date > DateTime.Parse(dtReglasCreditoVenta.Rows[0]["FechaApertura"].ToString()).AddDays(test))
                {
                    {
                        porcentajeDeInteres = Decimal.Parse(dtReglasCreditoVenta.Rows[0]["creditoPorcentajeinteres"].ToString());
                        switch (dtReglasCreditoVenta.Rows[0]["creditoperiodocobro"].ToString())
                        {
                            case "Mensual":
                                dias = 30;
                                break;
                            case "Quincenal":
                                dias = 15;
                                break;
                            case "Semanal":
                                dias = 7;
                                break;
                            default:
                                dias = 0;
                                //Literalmente deberia ser imposible llegar aqui
                                break;
                        }
                        if (dtReglasCreditoVenta.Rows[0]["creditomodocobro"].ToString() == "Dias trascurridos")
                        {
                            diasTrascurridos = Decimal.Parse((DateTime.Now.Date - lameraFecha.Date).ToString().Split(':')[0]);
                        }
                        else
                        {
                            if (dtReglasCreditoVenta.Rows[0]["creditoperiodocobro"].ToString() == "Mensual")
                            {
                                diasTrascurridos = 30;
                                foreach (var fecha in dtReglasCreditoVenta.Rows[0]["FechaInteres"].ToString().Split('%'))
                                {
                                    if (DateTime.Parse(fecha) < DateTime.Now)
                                    {
                                        diasTrascurridos += 30;
                                    }
                                }
                            }
                            else
                            {
                                diasTrascurridos = Decimal.Parse((DateTime.Now.Date - lameraFecha.Date).ToString().Split(':')[0]);
                                diasTrascurridos = (diasTrascurridos / dias);
                                diasTrascurridos = Math.Ceiling(diasTrascurridos) * dias;
                            }
                        }
                        interesPorDia = porcentajeDeInteres / 100 / dias;
                        calculoIntereses = interesPorDia * saldo * diasTrascurridos;
                        //if (!calculoIntereses.Equals(0))
                        //{
                        //    lblFechaAnterior.Text = $"({(Int32)diasTrascurridos} días\n";
                        //    lblFechaAnterior.Text += $"desde: {lameraFecha.ToString("yyyy-MM-dd")}";
                        //    lblFechaAnterior.Visible = true;
                        //}

                    }
                }
            }
            return calculoIntereses+restanteInteres;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Validar huella digital si es necesario
            using (DataTable dtReglasCreditoVenta = cn.CargarDatos($"SELECT * FROM reglasCreditoVenta WHERE IDVenta = {idVenta}"))
            {
                if (dtReglasCreditoVenta.Rows[0]["creditoHuella"].ToString().Equals("1"))
                {
                    //Si se pide huella
                    using (DataTable dtHuellas = cn.CargarDatos($"SELECT * FROM huellasClientes WHERE IDUsuario = {FormPrincipal.userID} AND IDCliente = {IDCliente}"))
                    {
                        if (dtHuellas.Rows.Count.Equals(0))
                        {
                            //No tiene huella, vamos a ver si la quiere registrar
                            DialogResult dialogResult = MessageBox.Show("El cliente seleccionado no cuenta con un registro de datos biométricos, quieres registrarlos ahora?", "Error de registro biométrico", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                clientesAltaHuella capturar = new clientesAltaHuella(Int32.Parse(IDCliente));
                                capturar.ShowDialog();
                                return;
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                MessageBox.Show("Esta venta no se puede consolidar sin revisión biométrica de acuerdo a la configuración actual", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            //Si tiene huella
                            if (!revisarHuella())
                            {
                                MessageBox.Show($"No coincidio la muestra", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }
            }
            decimal vuelto = decimal.Parse(lbTotalCambio.Text.Split('$')[1]);

            if (sumarMetodosTemporal() > 0)
            {
                float total = 0f;
                float efectiv = 0f;

                var tarjeta = CantidadDecimal(txtTarjeta.Text);
                var vales = CantidadDecimal(txtVales.Text);
                var cheque = CantidadDecimal(txtCheque.Text);
                var transferencia = CantidadDecimal(txtTransferencia.Text);
                var referencia = txtReferencia.Text;
                var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var perdonado = CantidadDecimal(txtPerdonado.Text);

                if (SumaMetodos() > 0)/////////////////////////////////////
                {
                    //totalEfectivo = totalPendiente - (SumaMetodos() + CantidadDecimal(txtEfectivo.Text));
                    total = (SumaMetodos() + CantidadDecimal(txtEfectivo.Text)); //=100
                    efectiv = CantidadDecimal(txtEfectivo.Text);
                    //totalEfectivo = CantidadDecimal(txtEfectivo.Text);
                }
                else
                {
                    efectiv = CantidadDecimal(txtEfectivo.Text);

                    if (total > totalPendiente && SumaMetodos() == 0)
                    {
                        total = Math.Abs(CantidadDecimal(txtEfectivo.Text) - totalPendiente);
                    }
                    else
                    {
                        total = efectiv;
                    }
                }

                //var totalAbonado = totalEfectivo + tarjeta + vales + cheque + transferencia; //=150
                //var totalAbonado = total;

                //Condicion para saber si se termino de pagar y cambiar el status de la venta
                //if (totalAbonado >= totalPendiente)
                //{
                //    int status = tipoCredito.Equals("RCC") ? 6 : 1;

                //    cn.EjecutarConsulta(cs.ActualizarVenta(idVenta, status, FormPrincipal.userID));
                //}

                
                if (restante <= 0)
                {
                    int status = tipoCredito.Equals("RCC") ? 6 : 1;

                    cn.EjecutarConsulta(cs.estatusFinalizacionPagoCredito(idVenta, status));
                }

                if (lblPendiente.Text.Equals("$0.00"))
                {
                    {
                        mocho = 1;
                        string nuevasfechas = string.Empty;
                        using (DataTable dt = cn.CargarDatos($"SELECT FechasFaltantes FROM reglascreditoventa WHERE IDVenta = {idVenta}"))
                        {
                            string[] fechas = dt.Rows[0][0].ToString().Split('%');
                            foreach (string fecha in fechas)
                            {
                                if (DateTime.Parse(fecha) > DateTime.Now)
                                {
                                    nuevasfechas += fecha + "%";
                                }
                            }
                        }
                        nuevasfechas = nuevasfechas.TrimEnd('%'); // Remover cualquier % al final
                        if (!string.IsNullOrEmpty(nuevasfechas))
                        {
                            cn.EjecutarConsulta($"UPDATE reglascreditoventa SET FechasFaltantes = '{nuevasfechas}' WHERE IDVenta = {idVenta}");
                        }
                    }
                }

                //var pagoPendiente = txtPendiente.Text;
                //var cantidadPendiente = float.Parse(pagoPendiente);
                //var operacionTotal = (total - totalPendiente);

                if (efectiv > totalPendiente) { efectiv = totalPendiente; }
                if (tarjeta > totalPendiente) { tarjeta = totalPendiente; }
                if (vales > totalPendiente) { vales = totalPendiente; }
                if (cheque > totalPendiente) { cheque = totalPendiente; }
                if (transferencia > totalPendiente) { transferencia = totalPendiente; }

                string[] datos;
                int resultado = 0;
                string fechaNueva;
                //total -= (float)vuelto;
                decimal interesesPagados;
                if (float.Parse(intereses.ToString()) > total)
                {
                    interesesPagados = (decimal)total;
                }
                else
                {
                    interesesPagados = decimal.Parse(intereses.ToString());
                }

                if (chbAdelantoMes.Checked)
                {
                    interesesPagados = 0;

                }

                total = total - (float)vuelto;
                //var totalAbonado = totalEfectivo + tarjeta + vales + cheque + transferencia; //=150
                var totalAbonado = total;
                //Condicion para saber si se termino de pagar y cambiar el status de la venta
                //if (totalAbonado >= totalPendiente)
                if (txtPendiente.Text.Equals("$0.00"))
                {
                    cn.EjecutarConsulta(cs.ActualizarVenta(idVenta, 1, FormPrincipal.userID));
                }


                //Validar que se se guarde una cantidad mayor que el total pendiente
                if (totalPendiente > total)
                {
                    if (!FormPrincipal.userNickName.Contains("@"))
                    {
                        datos = new string[] {
                            idVenta.ToString(), FormPrincipal.userID.ToString(), total.ToString(), efectiv.ToString(), tarjeta.ToString(), vales.ToString(), cheque.ToString(), transferencia.ToString(), referencia, fechaOperacion,interesesPagados.ToString(), vuelto.ToString(), mocho.ToString(),perdonado.ToString(),(intereses-interesesPagados).ToString(),"0.00"
                        };

                        resultado = cn.EjecutarConsulta(cs.GuardarAbonos(datos));

                    }
                    else
                    {
                        datos = new string[] {
                            idVenta.ToString(), FormPrincipal.userID.ToString(), total.ToString(), efectiv.ToString(), tarjeta.ToString(), vales.ToString(), cheque.ToString(), transferencia.ToString(), referencia, fechaOperacion, FormPrincipal.id_empleado.ToString(),interesesPagados.ToString(),vuelto.ToString(), mocho.ToString(),perdonado.ToString(),(intereses-interesesPagados).ToString(),"0.00"
                        };

                        resultado = cn.EjecutarConsulta(cs.GuardarAbonosEmpleados(datos));
                    }

                    if (resultado > 0)
                    {
                        var idAbono = cn.EjecutarSelect($"SELECT * FROM Abonos WHERE IDVenta = {idVenta} AND IDUsuario = {FormPrincipal.userID} ORDER BY FechaOperacion DESC LIMIT 1", 1).ToString();
                        var restante = totalPendiente - totalAbonado;

                        datos = new string[] { idVenta.ToString(), idAbono, totalOriginal.ToString("0.00"), totalPendiente.ToString("0.00"), totalAbonado.ToString("0.00"), restante.ToString("0.00"), fechaOperacion };

                        //visualizadorAbonoPrimero primerAbono = new visualizadorAbonoPrimero(existenAbonos, IDCliente);
                        //primerAbono.idAbono = idAbono;
                        //primerAbono.idVenta = idVenta.ToString();
                        //primerAbono.SaldoRestante = txtPendiente.Text;
                        //primerAbono.saldoAnterior = saldoAnterior;
                        //datos = new string[] { idVenta.ToString(), idAbono, totalOriginal.ToString("0.00"), totalPendiente.ToString("0.00"), totalAbonado.ToString("0.00"), restante.ToString("0.00"), fechaOperacion };
                        //primerAbono.ShowDialog();

                        //GenerarTicket(datos);

                        using (var dt = cn.CargarDatos($"SELECT TicketAbono,PreguntarTicketAbono,AbrirCajaAbonos FROM configuraciondetickets WHERE IDUSuario = {FormPrincipal.userID}"))
                        {
                            if (dt.Rows[0]["TicketAbono"].Equals(1))
                            {
                                ImprimirTicketAbono impresionTicketAbono = new ImprimirTicketAbono();
                                impresionTicketAbono.idAbono = idVenta;
                                impresionTicketAbono.ShowDialog();
                            }
                            else if (dt.Rows[0]["PreguntarTicketAbono"].Equals(1))
                            {
                                DialogResult result = MessageBox.Show("¿Desea imprimir Ticket?", "Aviso del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (result.Equals(DialogResult.Yes))
                                {
                                    ImprimirTicketAbono impresionTicketAbono = new ImprimirTicketAbono();
                                    impresionTicketAbono.idAbono = idVenta;
                                    impresionTicketAbono.ShowDialog();
                                }
                                else if (dt.Rows[0]["AbrirCajaAbonos"].Equals(1))
                                {
                                    AbrirSinTicket abrir = new AbrirSinTicket();
                                    abrir.Show();
                                }
                            }
                            else if (dt.Rows[0]["AbrirCajaAbonos"].Equals(1))
                            {
                                AbrirSinTicket abrir = new AbrirSinTicket();
                                abrir.Show();
                            }
                        }

                        //ImprimirTicket(idVenta.ToString(), idAbono);
                        //MostrarTicketAbonos(idVenta.ToString(), idAbono);

                        this.Dispose();
                    }
                }
                else
                {
                    if (!FormPrincipal.userNickName.Contains("@"))
                    {
                        datos = new string[] {
                            idVenta.ToString(), FormPrincipal.userID.ToString(), totalPendiente.ToString(), efectiv.ToString(), tarjeta.ToString(), vales.ToString(), cheque.ToString(), transferencia.ToString(), referencia, fechaOperacion, interesesPagados.ToString(),vuelto.ToString(), mocho.ToString(),perdonado.ToString(),(intereses-interesesPagados).ToString(),"0.00"
                        };

                        resultado = cn.EjecutarConsulta(cs.GuardarAbonos(datos));
                    }
                    else
                    {
                        datos = new string[] {
                            idVenta.ToString(), FormPrincipal.userID.ToString(), total.ToString(), efectiv.ToString(), tarjeta.ToString(), vales.ToString(), cheque.ToString(), transferencia.ToString(), referencia, fechaOperacion, FormPrincipal.id_empleado.ToString(),interesesPagados.ToString(),vuelto.ToString(), mocho.ToString(),perdonado.ToString(),(intereses-interesesPagados).ToString(),"0.00"
                        };

                        resultado = cn.EjecutarConsulta(cs.GuardarAbonosEmpleados(datos));
                    }

                    if (resultado > 0)
                    {
                        var idAbono = cn.EjecutarSelect($"SELECT * FROM Abonos WHERE IDVenta = {idVenta} AND IDUsuario = {FormPrincipal.userID} ORDER BY FechaOperacion DESC LIMIT 1", 1).ToString();
                        var restante = totalPendiente - totalAbonado;

                        datos = new string[] { idVenta.ToString(), idAbono, totalOriginal.ToString("0.00"), totalPendiente.ToString("0.00"), totalAbonado.ToString("0.00"), restante.ToString("0.00"), fechaOperacion };

                        //GenerarTicket(datos);
                        //ImprimirTicketAbono impresionTicketAbono = new ImprimirTicketAbono();
                        //impresionTicketAbono.idAbono = idVenta;
                        //impresionTicketAbono.ShowDialog();
                        //ImprimirTicket(idVenta.ToString(), idAbono);
                        //MostrarTicketAbonos(idVenta.ToString(), idAbono);

                        //visualizadorAbonoPrimero primerAbono = new visualizadorAbonoPrimero(existenAbonos, IDCliente);
                        //primerAbono.idAbono = idAbono;
                        //primerAbono.idVenta = idVenta.ToString();
                        //primerAbono.SaldoRestante = txtPendiente.Text;
                        //primerAbono.saldoAnterior = saldoAnterior;
                        ////datos = new string[] { idVenta.ToString(), idAbono, totalOriginal.ToString("0.00"), totalPendiente.ToString("0.00"), totalAbonado.ToString("0.00"), restante.ToString("0.00"), fechaOperacion };
                        ////GenerarTicket(datos);
                        ////ImprimirTicketAbono impresionTicketAbono = new ImprimirTicketAbono();
                        ////impresionTicketAbono.idAbono = idVenta;
                        ////impresionTicketAbono.ShowDialog();
                        ////ImprimirTicket(idVenta.ToString(), idAbono);
                        ////MostrarTicketAbonos(idVenta.ToString(), idAbono);
                        //primerAbono.ShowDialog();
                        this.Dispose();
                    }
                }
            }
            else
            {
                MessageBox.Show("Ingrese una cantidad para poder realizar el abono.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private float sumarMetodosTemporal()
        {
            float efectivo = CantidadDecimal(txtEfectivo.Text);
            float tarjeta = CantidadDecimal(txtTarjeta.Text);
            float vales = CantidadDecimal(txtVales.Text);
            float cheque = CantidadDecimal(txtCheque.Text);
            float transferencia = CantidadDecimal(txtTransferencia.Text);
            float perdonado = CantidadDecimal(txtPerdonado.Text);

            float suma = efectivo + tarjeta + vales + cheque + transferencia + perdonado;

            return suma;
        }

        private void MostrarTicketAbonos(string idVenta, string idAbono)
        {
            var servidor = Properties.Settings.Default.Hosting;

            ticketGenerado = $"ticket_abono_{idVenta}" + "_" + idAbono + ".pdf";

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaTicketGenerado = $@"\\{servidor}\Archivos PUDVE\Ventas\Tickets\" + ticketGenerado;
            }
            else
            {
                rutaTicketGenerado = @"C:\Archivos PUDVE\Ventas\Tickets\" + ticketGenerado;
            }

            if (File.Exists(rutaTicketGenerado))
            {
                VisualizadorTickets vt = new VisualizadorTickets(ticketGenerado, rutaTicketGenerado);

                vt.FormClosed += delegate
                {
                    vt.Dispose();

                    rutaTicketGenerado = string.Empty;
                    ticketGenerado = string.Empty;
                };

                vt.ShowDialog();
            }
            else
            {
                MessageBox.Show($"El archivo solicitado con nombre '{ticketGenerado}' \nno se encuentra en el sistema.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SoloDecimales(object sender, KeyPressEventArgs e)
        {
            TextBox cantidad = (TextBox)sender;
            //if (cantidad.Name.Equals("txtEfectivo"))
            //{
            //    txtEfectivo.Text = "0.0";
            //}else if (cantidad.Name.Equals("txtTarjeta"))
            //{
            //    txtTarjeta.Text = "0.0";
            //}else if (cantidad.Name.Equals("txtVales"))
            //{
            //    txtVales.Text = "0.0";
            //}else if (cantidad.Name.Equals("txtCheque"))
            //{
            //    txtCheque.Text = "0.0";
            //}else if (cantidad.Name.Equals("txtTransferencia"))
            //{
            //    txtTransferencia.Text = "0.0";
            //}

            if (cantidad.Text.Equals("."))
            {
                cantidad.Text = "0.0";
            }

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
                if (cantidad.Equals("."))
                {
                    cantidad = "0.0";
                    valor = float.Parse(cantidad);
                }
                else
                {
                    valor = float.Parse(cantidad);
                }
            }

            return valor;
        }

        //private bool ValidarCantidades()
        //{
        //    var efectivo = CantidadDecimal(txtEfectivo.Text);
        //    var tarjeta = CantidadDecimal(txtTarjeta.Text);
        //    var vales = CantidadDecimal(txtVales.Text);
        //    var cheque = CantidadDecimal(txtCheque.Text);
        //    var transferencia = CantidadDecimal(txtTransferencia.Text);
        //    var total = efectivo + tarjeta + vales + cheque + transferencia;

        //    if (total <= totalPendiente)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        private void lbVerAbonos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (existenAbonos)
            {
                this.Hide();

                ListaAbonosVenta abonos = new ListaAbonosVenta(idVenta);

                abonos.FormClosed += delegate
                {
                    this.Show();
                };

                abonos.ShowDialog();
            }
            else
            {
                MessageBox.Show("No existen abonos previos para esta venta", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TerminarVenta(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private float SumaMetodos()
        {
            float tarjeta = CantidadDecimal(txtTarjeta.Text);
            float vales = CantidadDecimal(txtVales.Text);
            float cheque = CantidadDecimal(txtCheque.Text);
            float transferencia = CantidadDecimal(txtTransferencia.Text);
            float perdonado = CantidadDecimal(txtPerdonado.Text);
            float suma = tarjeta + vales + cheque + transferencia + perdonado;

            return suma;
        }

        //private void CalcularCambio()
        //{
            ////El total del campo efectivo + la suma de los otros metodos de pago - total de venta
            //double cambio = Convert.ToDouble((CantidadDecimal(txtEfectivo.Text) + totalMetodos) - totalPendiente);

            //// validar para que en el cambio las cantidades no sean negativas
            //if (cambio > 0)
            //{
            //    lbTotalCambio.Text = "$" + cambio.ToString("0.00");
            //}
            //else
            //{
            //    lbTotalCambio.Text = "$0.00";
            //}
        //}


        private void SumaMetodosPago(object sender, KeyEventArgs e)
        {
            float suma = SumaMetodos();

            //Si es mayor al total a pagar vuelve a calcular las cantidades pero sin tomar en cuenta
            //el campo que hizo que fuera mayor a la cantidad a pagar
            if (suma > totalPendiente)
            {
                TextBox tb = sender as TextBox;

                tb.Text = string.Empty;

                suma = SumaMetodos();
            }

            totalMetodos = suma;

            //CalcularCambio();
        }

        private void txtEfectivo_KeyUp(object sender, KeyEventArgs e)
        {
            //CalcularCambio();

            var cantidadEfectivo = txtEfectivo.Text;
            if (cantidadEfectivo.Equals("."))
            {
                txtEfectivo.Text = "0.";
                txtEfectivo.Select(txtEfectivo.Text.Length, 0);
            }
        }


        private void GenerarTicket(string[] info)
        {
            var datos = FormPrincipal.datosUsuario;

            //Medidas de ticket de 57 y 80 mm
            //57mm = 161.28 pt
            //80mm = 226.08 pt

            var tipoPapel = 57;
            var anchoPapel = Convert.ToInt32(Math.Floor((((tipoPapel * 0.10) * 72) / 2.54)));
            var altoPapel = Convert.ToInt32(anchoPapel + 54);

            string salto = string.Empty;

            int medidaFuenteMensaje = 0;
            int medidaFuenteNegrita = 0;
            int medidaFuenteNormal = 0;
            int medidaFuenteGrande = 0;

            int separadores = 0;
            int anchoLogo = 0;
            int altoLogo = 0;
            int espacio = 0;

            if (tipoPapel == 80)
            {
                separadores = 81;
                anchoLogo = 110;
                altoLogo = 60;
                espacio = 10;

                medidaFuenteMensaje = 10;
                medidaFuenteGrande = 10;
                medidaFuenteNegrita = 8;
                medidaFuenteNormal = 8;

                salto = "\n";
            }
            else if (tipoPapel == 57)
            {
                separadores = 75;
                anchoLogo = 80;
                altoLogo = 40;
                espacio = 8;

                medidaFuenteMensaje = 6;
                medidaFuenteGrande = 8;
                medidaFuenteNegrita = 6;
                medidaFuenteNormal = 6;

                salto = string.Empty;
            }

            Document ticket = new Document(new iTextSharp.text.Rectangle(anchoPapel, altoPapel), 3, 3, 5, 0);
            PdfWriter writer = PdfWriter.GetInstance(ticket, new FileStream(@"C:\Archivos PUDVE\Ventas\Tickets\ticket_abono_" + info[0] + "_" + info[1] + ".pdf", FileMode.Create));

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteNormal);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, medidaFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteGrande);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteMensaje);

            string logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + datos[11];
            string encabezado = $"{salto}{datos[1]} {datos[2]} {datos[3]}, {datos[4]}, {datos[5]}\nCol. {datos[6]} C.P. {datos[7]}\nRFC: {datos[8]}\n{datos[9]}\nTel. {datos[10]}\n\n";

            ticket.Open();

            //Validación para verificar si existe logotipo
            if (logotipo != "")
            {
                if (File.Exists(logotipo))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logotipo);
                    logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    logo.ScaleAbsolute(anchoLogo, altoLogo);
                    ticket.Add(logo);
                }
            }

            Paragraph titulo = new Paragraph(datos[0] + "\n", fuenteGrande);
            Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            domicilio.Alignment = Element.ALIGN_CENTER;
            domicilio.SetLeading(espacio, 0);

            Paragraph separadorInicial = new Paragraph(new string('-', separadores), fuenteNormal);

            //Contenido del Ticket
            var restante = restanteDePago - float.Parse(info[4]);
            string contenido = $"ID de venta: {info[0]}\nTotal original: ${info[2]}\nSaldo anterior: ${restanteDePago.ToString()}\nCantidad abonada: ${info[4]}\nCantidad restante: ${restante.ToString()}\n{info[6]}";

            Paragraph cuerpo = new Paragraph(contenido, fuenteNormal);
            cuerpo.Alignment = Element.ALIGN_CENTER;

            Paragraph mensaje = new Paragraph("Comprobante de Abono.", fuenteGrande);
            mensaje.Alignment = Element.ALIGN_CENTER;

            Paragraph separadorFinal = new Paragraph(new string('-', separadores), fuenteNormal);

            ticket.Add(titulo);
            ticket.Add(domicilio);
            ticket.Add(separadorInicial);
            ticket.Add(cuerpo);
            ticket.Add(separadorFinal);
            ticket.Add(mensaje);

            ticket.AddTitle("Ticket Abono");
            ticket.AddAuthor("PUDVE");
            ticket.Close();
            writer.Close();
        }

        private void ImprimirTicket(string idVenta, string idAbono)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.Verb = "print";
                info.FileName = @"C:\Archivos PUDVE\Ventas\Tickets\ticket_abono_" + idVenta + "_" + idAbono + ".pdf";
                info.CreateNoWindow = true;
                info.WindowStyle = ProcessWindowStyle.Hidden;

                Process p = new Process();
                p.StartInfo = info;
                p.Start();

                p.WaitForInputIdle();
                System.Threading.Thread.Sleep(1000);

                if (false == p.CloseMainWindow())
                {
                    p.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error No: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTarjeta_KeyUp(object sender, KeyEventArgs e)
        {
            var cantidadEfectivo = txtTarjeta.Text;
            if (cantidadEfectivo.Equals("."))
            {
                txtTarjeta.Text = "0.";
                txtTarjeta.Select(txtTarjeta.Text.Length, 0);
            }
        }

        private void txtVales_KeyUp(object sender, KeyEventArgs e)
        {
            var cantidadEfectivo = txtVales.Text;
            if (cantidadEfectivo.Equals("."))
            {
                txtVales.Text = "0.";
                txtVales.Select(txtVales.Text.Length, 0);
            }
        }

        private void txtCheque_KeyUp(object sender, KeyEventArgs e)
        {
            var cantidadEfectivo = txtCheque.Text;
            if (cantidadEfectivo.Equals("."))
            {
                txtCheque.Text = "0.";
                txtCheque.Select(txtCheque.Text.Length, 0);
            }
        }

        private void txtTransferencia_KeyUp(object sender, KeyEventArgs e)
        {
            var cantidadEfectivo = txtTransferencia.Text;
            if (cantidadEfectivo.Equals("."))
            {
                txtTransferencia.Text = "0.";
                txtTransferencia.Select(txtTransferencia.Text.Length, 0);
            }
        }

        private void validacionRestanteDeAbonos(object sender, EventArgs e)
        {
            TextBox txtCajaDeTexto;

            if (sender.GetType().Name.Equals("CheckBox"))
            {
                txtCajaDeTexto = txtEfectivo;
            }
            else
            {
                txtCajaDeTexto = (TextBox)sender;
            }


            var detalles = mb.ObtenerDetallesVenta(idVenta, FormPrincipal.userID);
            totalPendiente = float.Parse(detalles[2]);

            if (txtCajaDeTexto.Text == "")
            {
                txtCajaDeTexto.Text = "0";
                txtCajaDeTexto.Focus();
                txtCajaDeTexto.SelectAll();
            }
            else if (txtCajaDeTexto.Text.Equals("."))
            {
                txtCajaDeTexto.Text = "0.";
                txtCajaDeTexto.Focus();
                txtCajaDeTexto.SelectionStart = txtCajaDeTexto.Text.Length;
                txtCajaDeTexto.SelectionLength = 0;
            }

            using (DataTable dtAbonos = cn.CargarDatos(cs.cargarAbonosDeLaVentaACredito(idVenta)))
            {
                if (!dtAbonos.Rows.Count.Equals(0))
                {
                    var abonado = mb.ObtenerTotalAbonado(idVenta, FormPrincipal.userID);

                    if (txtCajaDeTexto.Name.Equals("txtEfectivo"))
                    {
                        efectivo = (float)Convert.ToDecimal(txtCajaDeTexto.Text);
                    }
                    else if (txtCajaDeTexto.Name.Equals("txtTarjeta"))
                    {
                        tarjeta = (float)Convert.ToDecimal(txtCajaDeTexto.Text);
                    }
                    else if (txtCajaDeTexto.Name.Equals("txtVales"))
                    {
                        vales = (float)Convert.ToDecimal(txtCajaDeTexto.Text);
                    }
                    else if (txtCajaDeTexto.Name.Equals("txtCheque"))
                    {
                        cheque = (float)Convert.ToDecimal(txtCajaDeTexto.Text);
                    }
                    else if (txtCajaDeTexto.Name.Equals("txtTransferencia"))
                    {
                        transferencia = (float)Convert.ToDecimal(txtCajaDeTexto.Text);
                    }
                    else if (txtCajaDeTexto.Name.Equals("txtPerdonado"))
                    {
                        perdonado = (float)Convert.ToDecimal(txtCajaDeTexto.Text);
                    }

                    var nuevoabono = abonado + efectivo + tarjeta + vales + cheque + transferencia + perdonado;
                    restante = totalPendiente - nuevoabono;
                    restante += (float)intereses;
                    var restanteAbono = ((intereses + abonoTotal) - (decimal)nuevoabono) + (decimal)abonado;

                    if (restanteAbono <= 0)
                    {
                        lblPendiente.Text = "$0.00";
                        cambio = (float)restanteAbono * (-1);
                        lbTotalCambio.Text = cambio.ToString("C2");
                    }
                    else
                    {
                        lblPendiente.Text = restanteAbono.ToString("C2");
                        lbTotalCambio.Text = "$0.00";
                        //lblfechainteres.Visible = false;
                    }

                    //adelantos
                    if ( !cbAdelanto.Checked && lblPendiente.Text == "$0.00" && !chbAdelantoMes.Checked)
                    {
                        txtPendiente.Text = ((decimal)totalPendiente - (decimal)(abonado) - (decimal)(abonoTotal)).ToString("C2");
                    }
                    else
                    {
                        txtPendiente.Text = restante.ToString("C2");
                        //cambio = restante * (-1);
                        //lbTotalCambio.Text = cambio.ToString("C2");
                        if (restante <= 0)
                        {
                            txtPendiente.Text = "$0.00";
                            cambio = restante * (-1);
                            lbTotalCambio.Text = cambio.ToString("C2");
                        }
                        else
                        {
                            lbTotalCambio.Text = "$0.00";
                        }
                    }
                }

                else
                {
                    float abonado = 0;

                    if (txtCajaDeTexto.Name.Equals("txtEfectivo"))
                    {
                        efectivo = (float)Convert.ToDecimal(txtCajaDeTexto.Text);
                    }
                    else if (txtCajaDeTexto.Name.Equals("txtTarjeta"))
                    {
                        tarjeta = (float)Convert.ToDecimal(txtCajaDeTexto.Text);
                    }
                    else if (txtCajaDeTexto.Name.Equals("txtVales"))
                    {
                        vales = (float)Convert.ToDecimal(txtCajaDeTexto.Text);
                    }
                    else if (txtCajaDeTexto.Name.Equals("txtCheque"))
                    {
                        cheque = (float)Convert.ToDecimal(txtCajaDeTexto.Text);
                    }
                    else if (txtCajaDeTexto.Name.Equals("txtTransferencia"))
                    {
                        transferencia = (float)Convert.ToDecimal(txtCajaDeTexto.Text);
                    }
                    else if (txtCajaDeTexto.Name.Equals("txtPerdonado"))
                    {
                        perdonado = (float)Convert.ToDecimal(txtCajaDeTexto.Text);
                    }

                    var nuevoabono = abonado + efectivo + tarjeta + vales + cheque + transferencia + perdonado;
                    restante = totalPendiente - nuevoabono;
                    restante += (float)intereses;
                    var restanteAbono = abonoTotal - (decimal)nuevoabono - (decimal)abonado + intereses;


                    if (restanteAbono <= 0)
                    {
                        lblPendiente.Text = "$0.00";
                        cambio = (float)restanteAbono * (-1);
                        lbTotalCambio.Text = cambio.ToString("C2");
                        //lblfechainteres.Visible = true;
                        //lblfechainteres.Text = $"Siguiente fecha de pago\nEl día {siguienteFechaAbono}";
                    }
                    else
                    {
                        lblPendiente.Text = restanteAbono.ToString("C2");
                        lbTotalCambio.Text = "$0.00";
                        //lblfechainteres.Visible = false;
                    }

                    //adelantos
                    if (!cbAdelanto.Checked && lblPendiente.Text == "$0.00" && !chbAdelantoMes.Checked)
                    {
                        txtPendiente.Text = (totalPendiente - (float)(abonoTotal)).ToString("C2");
                    }
                    else
                    {
                        txtPendiente.Text = restante.ToString("C2");
                        //cambio = restante * (-1);
                        //lbTotalCambio.Text = cambio.ToString("C2");
                        if (restante <= 0)
                        {
                            txtPendiente.Text = "$0.00";
                            cambio = restante * (-1);
                            lbTotalCambio.Text = cambio.ToString("C2");
                        }
                        else
                        {
                            lbTotalCambio.Text = "$0.00";
                        }
                    }

                }

            }
        }

        private void txtEfectivo_TextChanged(object sender, EventArgs e)
        {
            validacionRestanteDeAbonos(sender, e);

            //var detalles = mb.ObtenerDetallesVenta(idVenta, FormPrincipal.userID);
            //totalPendiente = float.Parse(detalles[2]);

            //using (DataTable dtAbonos = cn.CargarDatos(cs.cargarAbonosDeLaVentaACredito(idVenta)))
            //{
            //    if (txtEfectivo.Text == "")
            //    {
            //        txtEfectivo.Text = "0";
            //        txtEfectivo.Focus();
            //        txtEfectivo.SelectAll();
            //    }
            //    else
            //    {
            //        if (!dtAbonos.Rows.Count.Equals(0))
            //        {
            //            var abonado = mb.ObtenerTotalAbonado(idVenta, FormPrincipal.userID);
            //            efectivo = (float)Convert.ToDecimal(txtEfectivo.Text);
            //            var nuevoabono = abonado + efectivo + tarjeta + vales + cheque + transferencia;
            //            restante = totalPendiente - nuevoabono;
            //            txtPendiente.Text = restante.ToString("C2");
            //            if (restante < 1)
            //            {
            //                txtPendiente.Text = "$0.00";
            //                cambio = restante * (-1);
            //                lbTotalCambio.Text = cambio.ToString("C2");
            //            }
            //            else
            //            {
            //                lbTotalCambio.Text = "$0.00";
            //            }

            //        }
            //        else
            //        {
            //            float abonado = 0;
            //            efectivo = (float)Convert.ToDecimal(txtEfectivo.Text);
            //            var nuevoabono = abonado + efectivo + tarjeta + vales + cheque + transferencia;
            //            restante = totalPendiente - nuevoabono;
            //            txtPendiente.Text = restante.ToString("C2");
            //            if (restante < 1)
            //            {
            //                txtPendiente.Text = "$0.00";
            //                cambio = restante * (-1);
            //                lbTotalCambio.Text = cambio.ToString("C2");
            //            }
            //            else
            //            {
            //                lbTotalCambio.Text = "$0.00";
            //            }
            //        }
            //    }

            //}
        }

        private void txtTarjeta_TextChanged(object sender, EventArgs e)
        {
            validacionRestanteDeAbonos(sender, e);

            //var detalles = mb.ObtenerDetallesVenta(idVenta, FormPrincipal.userID);
            //totalPendiente = float.Parse(detalles[2]);

            //using (DataTable dtAbonos = cn.CargarDatos(cs.cargarAbonosDeLaVentaACredito(idVenta)))
            //{
            //    if (txtTarjeta.Text == "")
            //    {
            //        txtTarjeta.Text = "0";
            //        txtTarjeta.Focus();
            //        txtTarjeta.SelectAll();
            //    }
            //    else
            //    {
            //        if (!dtAbonos.Rows.Count.Equals(0))
            //        {
            //            var abonado = mb.ObtenerTotalAbonado(idVenta, FormPrincipal.userID);
            //            tarjeta = (float)Convert.ToDecimal(txtTarjeta.Text);
            //            var nuevoabono = abonado + efectivo + tarjeta + vales + cheque + transferencia;
            //            restante = totalPendiente - nuevoabono;
            //            txtPendiente.Text = restante.ToString("C2");
            //            if (restante < 1)
            //            {
            //                txtPendiente.Text = "$0.00";
            //                cambio = restante * (-1);
            //                lbTotalCambio.Text = cambio.ToString("C2");
            //            }
            //            else
            //            {
            //                lbTotalCambio.Text = "$0.00";
            //            }

            //        }
            //        else
            //        {
            //            float abonado = 0;
            //            tarjeta = (float)Convert.ToDecimal(txtTarjeta.Text);
            //            var nuevoabono = abonado + efectivo + tarjeta + vales + cheque + transferencia;
            //            restante = totalPendiente - nuevoabono;
            //            txtPendiente.Text = restante.ToString("C2");
            //            if (restante < 1)
            //            {
            //                txtPendiente.Text = "$0.00";
            //                cambio = restante * (-1);
            //                lbTotalCambio.Text = cambio.ToString("C2");
            //            }
            //            else
            //            {
            //                lbTotalCambio.Text = "$0.00";
            //            }
            //        }
            //    }
            //}
        }

        private void txtVales_TextChanged(object sender, EventArgs e)
        {
            validacionRestanteDeAbonos(sender, e);

            //var detalles = mb.ObtenerDetallesVenta(idVenta, FormPrincipal.userID);
            //totalPendiente = float.Parse(detalles[2]);

            //using (DataTable dtAbonos = cn.CargarDatos(cs.cargarAbonosDeLaVentaACredito(idVenta)))
            //{
            //    if (txtVales.Text == "")
            //    {
            //        txtVales.Text = "0";
            //        txtVales.Focus();
            //        txtVales.SelectAll();
            //    }
            //    else
            //    {
            //        if (!dtAbonos.Rows.Count.Equals(0))
            //        {
            //            var abonado = mb.ObtenerTotalAbonado(idVenta, FormPrincipal.userID);
            //            vales = (float)Convert.ToDecimal(txtVales.Text);
            //            var nuevoabono = abonado + efectivo + tarjeta + vales + cheque + transferencia;
            //            restante = totalPendiente - nuevoabono;
            //            txtPendiente.Text = restante.ToString("C2");
            //            if (restante < 1)
            //            {
            //                txtPendiente.Text = "$0.00";
            //                cambio = restante * (-1);
            //                lbTotalCambio.Text = cambio.ToString("C2");
            //            }
            //            else
            //            {
            //                lbTotalCambio.Text = "$0.00";
            //            }

            //        }
            //        else
            //        {
            //            float abonado = 0;
            //            vales = (float)Convert.ToDecimal(txtVales.Text);
            //            var nuevoabono = abonado + efectivo + tarjeta + vales + cheque + transferencia;
            //            restante = totalPendiente - nuevoabono;
            //            txtPendiente.Text = restante.ToString("C2");
            //            if (restante < 1)
            //            {
            //                txtPendiente.Text = "$0.00";
            //                cambio = restante * (-1);
            //                lbTotalCambio.Text = cambio.ToString("C2");
            //            }
            //            else
            //            {
            //                lbTotalCambio.Text = "$0.00";
            //            }
            //        }
            //    }
            //}
        }

        private void txtCheque_TextChanged(object sender, EventArgs e)
        {
            validacionRestanteDeAbonos(sender, e);

            //var detalles = mb.ObtenerDetallesVenta(idVenta, FormPrincipal.userID);
            //totalPendiente = float.Parse(detalles[2]);

            //using (DataTable dtAbonos = cn.CargarDatos(cs.cargarAbonosDeLaVentaACredito(idVenta)))
            //{
            //    if (txtCheque.Text == "")
            //    {
            //        txtCheque.Text = "0";
            //        txtCheque.Focus();
            //        txtCheque.SelectAll();
            //    }
            //    else
            //    {
            //        if (!dtAbonos.Rows.Count.Equals(0))
            //        {
            //            var abonado = mb.ObtenerTotalAbonado(idVenta, FormPrincipal.userID);
            //            cheque = (float)Convert.ToDecimal(txtCheque.Text);
            //            var nuevoabono = abonado + efectivo + tarjeta + vales + cheque + transferencia;
            //            restante = totalPendiente - nuevoabono;
            //            txtPendiente.Text = restante.ToString("C2");
            //            if (restante < 1)
            //            {
            //                txtPendiente.Text = "$0.00";
            //                cambio = restante * (-1);
            //                lbTotalCambio.Text = cambio.ToString("C2");
            //            }
            //            else
            //            {
            //                lbTotalCambio.Text = "$0.00";
            //            }
            //        }
            //        else
            //        {
            //            float abonado = 0;
            //            cheque = (float)Convert.ToDecimal(txtCheque.Text);
            //            var nuevoabono = abonado + efectivo + tarjeta + vales + cheque + transferencia;
            //            restante = totalPendiente - nuevoabono;
            //            txtPendiente.Text = restante.ToString("C2");
            //            if (restante < 1)
            //            {
            //                txtPendiente.Text = "$0.00";
            //                cambio = restante * (-1);
            //                lbTotalCambio.Text = cambio.ToString("C2");
            //            }
            //            else
            //            {
            //                lbTotalCambio.Text = "$0.00";
            //            }
            //        }
            //    }
            //}
        }

        private void txtTransferencia_TextChanged(object sender, EventArgs e)
        {
            validacionRestanteDeAbonos(sender, e);

            //var detalles = mb.ObtenerDetallesVenta(idVenta, FormPrincipal.userID);
            //totalPendiente = float.Parse(detalles[2]);

            //using (DataTable dtAbonos = cn.CargarDatos(cs.cargarAbonosDeLaVentaACredito(idVenta)))
            //{
            //    if (txtTransferencia.Text == "")
            //    {
            //        txtTransferencia.Text = "0";
            //        txtTransferencia.Focus();
            //        txtTransferencia.SelectAll();
            //    }
            //    else
            //    {
            //        if (!dtAbonos.Rows.Count.Equals(0))
            //        {
            //            var abonado = mb.ObtenerTotalAbonado(idVenta, FormPrincipal.userID);
            //            transferencia = (float)Convert.ToDecimal(txtTransferencia.Text);
            //            var nuevoabono = abonado + efectivo + tarjeta + vales + cheque + transferencia;
            //            restante = totalPendiente - nuevoabono;
            //            txtPendiente.Text = restante.ToString("C2");
            //            if (restante < 1)
            //            {
            //                txtPendiente.Text = "$0.00";
            //                cambio = restante * (-1);
            //                lbTotalCambio.Text = cambio.ToString("C2");
            //            }
            //            else
            //            {
            //                lbTotalCambio.Text = "$0.00";
            //            }

            //        }
            //        else
            //        {
            //            float abonado = 0;
            //            transferencia = (float)Convert.ToDecimal(txtTransferencia.Text);
            //            var nuevoabono = abonado + efectivo + tarjeta + vales + cheque + transferencia;
            //            restante = totalPendiente - nuevoabono;
            //            txtPendiente.Text = restante.ToString("C2");

            //            if (restante < 1)
            //            {
            //                txtPendiente.Text = "$0.00";
            //                cambio = restante * (-1);
            //                lbTotalCambio.Text = cambio.ToString("C2");
            //            }
            //            else
            //            {
            //                lbTotalCambio.Text = "$0.00";
            //            }

            //        }
            //    }
            //}
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

        private void txtCheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Right))
                SendKeys.Send("{TAB}");

            if (e.KeyCode.Equals(Keys.Left))
                SendKeys.Send("+{TAB}");
        }

        private void txtTransferencia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Left))
                SendKeys.Send("+{TAB}");
        }

        private void txtVales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Right))
                SendKeys.Send("{TAB}");

            if (e.KeyCode.Equals(Keys.Left))
                SendKeys.Send("+{TAB}");
        }

        private void txtEfectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
        }

        private void calculadora(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int calcu = 0;
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        if (calculadora.seEnvia.Equals(true))
                        {
                            txt.Text = calculadora.lCalculadora.Text;
                        }
                        calcu = 0;
                    };
                    if (!calculadora.Visible)
                    {
                        calculadora.Show();
                    }
                    else
                    {
                        calculadora.Show();
                    }
                }
            }
        }

        private void txtTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
        }

        private void txtVales_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
        }

        private void txtCheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
        }

        private void txtTransferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
        }
        private void txtPendiente_TextChanged(object sender, EventArgs e)
        {
            decimal m3 = Decimal.Parse(txtPendiente.Text.Split('$')[1]) - Decimal.Parse(lblmirror.Text.Split('$')[1]);
            if (m3 < 0)
            {
                m3 = 0;
            }
            lblMirror3.Text = m3.ToString("");
        }

        private void lblmirror_TextChanged(object sender, EventArgs e)
        {
            decimal m3 = Decimal.Parse(txtPendiente.Text.Split('$')[1]) - Decimal.Parse(lblmirror.Text.Split('$')[1]);
            if (m3 < 0)
            {
                m3 = 0;
            }
            lblMirror3.Text = m3.ToString("");
        }
        private void cbAdelanto_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAdelanto.Checked)
            {
                chbAdelantoMes.Checked = false;
                validacionRestanteDeAbonos(sender, e);
            }
        }

        private void txtIntereses_TextChanged(object sender, EventArgs e)
        {
            lblmirror.Text = txtIntereses.Text;
        }

        private void lblFechaAnterior_TextChanged(object sender, EventArgs e)
        {
            lblmirror2.Text = lblFechaAnterior.Text;
            lblmirror2.Visible = true;
        }

        private void lblpagos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //using (DataTable dtReglasCreditoVenta = cn.CargarDatos($"SELECT * FROM reglasCreditoVenta WHERE IDVenta = {idVenta}"))
            //{
            //    string mensaje = "Fechas de los abonos";
            //    foreach (var fecha in dtReglasCreditoVenta.Rows[0]["FechaInteres"].ToString().Split('%'))
            //    {
            //        mensaje += $"\n{fecha}";
            //    }
            //    mensaje += $"\nSe cobran intereses desde el día: {DateTime.Parse(dtReglasCreditoVenta.Rows[0]["FechaApertura"].ToString()).AddDays(double.Parse(dtReglasCreditoVenta.Rows[0]["creditodiassincobro"].ToString())).ToString("yyyy-MM-dd")}";
            //    MessageBox.Show(mensaje, "Fechas relevantes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            vizualisarAmortizacion vizAm= new vizualisarAmortizacion(idVenta,(decimal)totalOriginal);
            vizAm.ShowDialog();

        }

        private bool revisarHuella()
        {
            bool coincidencia = false;

            clientesVerificarHuella checador = new clientesVerificarHuella(IDCliente);
            checador.FormClosed += delegate
            {
                if (!string.IsNullOrEmpty(checador.idCliente))
                {
                    if (checador.idCliente == IDCliente)
                    {
                        coincidencia = true;
                    }

                }
            };
            checador.ShowDialog();
            return coincidencia;
        }

        private void llPerdonarInteres_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbonosPerdonarIntereses perdonar = new AbonosPerdonarIntereses(intereses);
            perdonar.FormClosed += delegate
            {
                string valor = perdonar.interesPerdonado.ToString("C2");
                txtPerdonado.Text = valor.Split('$')[1];
                txtIntereses.Text = (intereses - perdonar.interesPerdonado).ToString("C2");
                lblabonominimo.Text = (Decimal.Parse(lblabonominimo.Text.Split('$')[1]) - perdonar.interesPerdonado).ToString("C2");
            };
            perdonar.ShowDialog();
        }

        private void txtPerdonado_TextChanged(object sender, EventArgs e)
        {
            validacionRestanteDeAbonos(sender, e);
        }

        private void chbAdelantoMes_CheckedChanged(object sender, EventArgs e)
        {
            if (chbAdelantoMes.Checked)
            {

                cbAdelanto.Checked = false;
                validacionRestanteDeAbonos(sender, e);
            }

        }
    }
}
