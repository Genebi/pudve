using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomControlPUDVE;

namespace PuntoDeVentaV2
{
    public partial class AgregarRetirarDinero : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();
        MetodosGenerales mg = new MetodosGenerales();
        CargarDatosCaja cdc = new CargarDatosCaja();

        // 0 = Depositar
        // 1 = Retirar
        // 2 = Corte
        public int operacion = 0;


        private decimal totalEfectivo = 0;
        private decimal totalTarjeta = 0;
        private decimal totalVales = 0;
        private decimal totalCheque = 0;
        private decimal totalTransferencia = 0;
        private decimal totalCredito = 0;
        private decimal totalSaldoInicial = 0;
        private decimal totalEnCaja = 0;

        private decimal cantidadTotalEfectivoEnCaja = 0;
        private decimal cantidadEfectivoSaldoInicialEnCaja = 0;
        private decimal cantidadTotalTarjetaEnCaja = 0;
        private decimal cantidadTarjetaSaldoInicialEnCaja = 0;
        private decimal cantidadTotalValesEnCaja = 0;
        private decimal cantidadValesSaldoInicialEnCaja = 0;
        private decimal cantidadTotalCehqueEnCaja = 0;
        private decimal cantidadChequeSaldoInicialEnCaja = 0;
        private decimal cantidadTotalTransferenciaEnCaja = 0;
        private decimal cantidadTransferenciaSaldoInicialEnCaja = 0;

        //Variables del corte de caja
        float convertEfectivo = 0f;
        float convertTarjeta = 0f;
        float convertCheque = 0f;
        float convertVales = 0f;
        float convertTrans = 0f;


        public static string totalRetiradoCorte { get; set; }
        public static string obtenerRutaPDF { get; set; }

        string idParaComboBox = string.Empty;

        public AgregarRetirarDinero(int operacion = 0)
        {
            InitializeComponent();

            this.operacion = operacion;
            obtenerVariablesCaja();
        }

        private void obtenerVariablesCaja()
        {
            if (CajaN.totCorte != "")
            {
                if (CajaN.efectivoCorte == null) { convertEfectivo = 0; } else { convertEfectivo = float.Parse(CajaN.efectivoCorte); }
                if (CajaN.tarjetaCorte == null) { convertTarjeta = 0; } else { convertTarjeta = float.Parse(CajaN.tarjetaCorte); }
                if (CajaN.chequeCorte == null) { convertCheque = 0; } else { convertCheque = float.Parse(CajaN.chequeCorte); }
                if (CajaN.valesCorte == null) { convertVales = 0; } else { convertVales = float.Parse(CajaN.valesCorte); }
                if (CajaN.transCorte == null) { convertTrans = 0; } else { convertTrans = float.Parse(CajaN.transCorte); }
            }
        }

        private void AgregarRetirarDinero_Load(object sender, EventArgs e)
        {
            if (operacion == 0)
            {
                lbTitulo.Text = "Cantidad a depositar";
                lbSubtitulo.Text = "Concepto del depósito";
                lbCredito.Visible = false;
                txtCredito.Visible = false;
            }
            else if (operacion == 1)
            {
                lbTitulo.Text = "Cantidad a retirar";
                lbSubtitulo.Text = "Concepto del retiro";
            }
            else if (operacion == 2)
            {
                lbTitulo.Text = "Cantidad a retirar";
                lbSubtitulo.Text = "Concepto del retiro";
                //btnCancelar.Text = "Corte sin retiro";
            }

            if (!operacion.Equals(0))
            {
                btnRetirarTodoElDinero.Visible = true;
            }
            else
            {
                btnRetirarTodoElDinero.Visible = false;
            }

            txtEfectivo.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCredito.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTarjeta.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCheque.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtTrans.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtVales.KeyPress += new KeyPressEventHandler(SoloDecimales);

            // NOTA: Se le suma saldo inicial a efectivo porque se tiene que guardar el valor y anteriormente
            // se tomaba en cuenta el saldo inicial en el apartado de totales del apartado Ventas
            //totalEfectivo = CajaN.totalEfectivo + CajaN.saldoInicial;

            cantidadTotalEfectivoEnCaja = CajaN.cantidadTotalEfectivoEnCaja;
            cantidadEfectivoSaldoInicialEnCaja = CajaN.cantidadEfectivoSaldoInicialEnCaja;
            cantidadTotalTarjetaEnCaja = CajaN.cantidadTotalTarjetaEnCaja;
            cantidadTarjetaSaldoInicialEnCaja = CajaN.cantidadTarjetaSaldoInicialEnCaja;
            cantidadTotalValesEnCaja = CajaN.cantidadTotalValesEnCaja;
            cantidadValesSaldoInicialEnCaja = CajaN.cantidadValesSaldoInicialEnCaja;
            cantidadTotalCehqueEnCaja = CajaN.cantidadTotalCehqueEnCaja;
            cantidadChequeSaldoInicialEnCaja = CajaN.cantidadChequeSaldoInicialEnCaja;
            cantidadTotalTransferenciaEnCaja = CajaN.cantidadTotalTransferenciaEnCaja;
            cantidadTransferenciaSaldoInicialEnCaja = CajaN.cantidadTransferenciaSaldoInicialEnCaja;

            totalEfectivo = cantidadTotalEfectivoEnCaja + cantidadEfectivoSaldoInicialEnCaja;
            totalTarjeta = cantidadTotalTarjetaEnCaja + cantidadTarjetaSaldoInicialEnCaja;
            totalVales = cantidadTotalValesEnCaja + cantidadValesSaldoInicialEnCaja;
            totalCheque = cantidadTotalCehqueEnCaja + cantidadChequeSaldoInicialEnCaja;
            totalTransferencia = cantidadTotalTransferenciaEnCaja + cantidadTransferenciaSaldoInicialEnCaja;
            totalSaldoInicial = CajaN.totalSaldoInicial;
            totalEnCaja = CajaN.sumaDeTotalesEnCaja;
            //totalCredito = CajaN.totalCredito;

            //totalEfectivo = CajaN.efectivo;
            //totalTarjeta = CajaN.tarjeta;
            //totalVales = CajaN.vales;
            //totalCheque = CajaN.cheque;
            //totalTransferencia = CajaN.trans;
            ////totalCredito = CajaN.cred;

            CargarConceptos();
        }


        private void CargarConceptos()
        {
            var conceptos = mb.ObtenerConceptosDinamicos(origen: "CAJA");

            //cbConceptos.DataSource = conceptos.ToArray();
            //cbConceptos.DisplayMember = "Value";
            //cbConceptos.ValueMember = "Key";

            cbConceptoConBusqueda.DataSource = conceptos.ToArray();
            cbConceptoConBusqueda.DisplayMember = "Value";
            cbConceptoConBusqueda.ValueMember = "Key";
            cbConceptoConBusqueda.SelectedIndex = 0;

            cbConceptoConBusqueda.MatchingMethod = StringMatchingMethod.NoWildcards;
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
            // Solo se ejecuta cuando es Corte de caja
            /*if (operacion == 2)
            {
                string fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                CajaN.fechaUltimoCorte = Convert.ToDateTime(fechaOperacion);

                //totalEfectivo -= CajaN.retiroEfectivo;
                //totalTarjeta -= CajaN.retiroTarjeta;
                //totalVales -= CajaN.retiroVales;
                //totalCheque -= CajaN.retiroCheque;
                //totalTransferencia -= CajaN.retiroTrans;

                if (totalEfectivo <= 0) { totalEfectivo = 0; }
                if (totalTarjeta <= 0) { totalTarjeta = 0; }
                if (totalVales <= 0) { totalVales = 0; }
                if (totalCheque <= 0) { totalCheque = 0; }
                if (totalTransferencia <= 0) { totalTransferencia = 0; }


                var cantidad = totalEfectivo + totalTarjeta + totalVales + totalCheque + totalTransferencia + totalCredito;

                string[] datos = new string[] {
                    "corte", cantidad.ToString("0.00"), "0", "sin retiro", fechaOperacion, FormPrincipal.userID.ToString(),
                    totalEfectivo.ToString("0.00"), totalTarjeta.ToString("0.00"), totalVales.ToString("0.00"), totalCheque.ToString("0.00"),
                    totalTransferencia.ToString("0.00"), totalCredito.ToString("0.00"), "0"
                };

                int resultado = cn.EjecutarConsulta(cs.OperacionCaja(datos));

                if (resultado > 0)
                {
                    // Se pausa por 1 segundo
                    Thread.Sleep(1000);

                    fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    datos = new string[] {
                        "venta", cantidad.ToString("0.00"), "0", "sin retiro", fechaOperacion, FormPrincipal.userID.ToString(),
                        totalEfectivo.ToString("0.00"), totalTarjeta.ToString("0.00"), totalVales.ToString("0.00"), totalCheque.ToString("0.00"),
                        totalTransferencia.ToString("0.00"), totalCredito.ToString("0.00"), "0"
                    };

                    cn.EjecutarConsulta(cs.OperacionCaja(datos));
                }

                CajaN.botones = true;
            }*/

            //Dispose();
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var tipoOperacion = string.Empty;
            bool tipoCorte = true;

            var siRetiroEfectivo = false;
            var siRetiroTarjeta = false;
            var siRetiroVales = false;
            var siRetiroCheque = false;
            var siRetiroTransferencia = false;

            FormPrincipal.condicionarMensaje = 1;

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

            // Corte de caja
            if (operacion == 2)
            {
                tipoOperacion = "corte";
            }

            var numFolio = obtenerNumFolio(tipoOperacion);

            //var concepto = cbConceptos.GetItemText(cbConceptos.SelectedItem);
            var concepto = cbConceptoConBusqueda.GetItemText(cbConceptoConBusqueda.SelectedItem);

            if (concepto.Equals("Seleccionar concepto..."))
            {
                concepto = string.Empty;
            }

            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            CajaN.fechaUltimoCorte = Convert.ToDateTime(fechaOperacion);

            var efectivo = ValidarCampos(txtEfectivo.Text);
            var tarjeta = ValidarCampos(txtTarjeta.Text);
            var cheque = ValidarCampos(txtCheque.Text);
            var vales = ValidarCampos(txtVales.Text);
            var trans = ValidarCampos(txtTrans.Text);
            var credito = ValidarCampos(txtCredito.Text);

            // Se guardan las cantidades que el usuario es lo que va a retirar
            //var cantidad = efectivo + tarjeta + cheque + vales + trans + credito;
            float cantidad = 0;

            if (!operacion.Equals(0))
            {
                if (efectivo < 0 && efectivo > (float)totalEfectivo)
                {
                    if (efectivo < 0)
                    {
                        efectivo = 0;
                    }
                    else
                    {
                        efectivo = (float)totalEfectivo;
                        MessageBox.Show($"El monto de efectivo tiene que ser menor o igual que el monto que hay en caja: {totalEfectivo.ToString("C2")}", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    txtEfectivo.Text = efectivo.ToString();
                }
                else
                {
                    siRetiroEfectivo = true;
                }

                if (tarjeta < 0 && tarjeta > (float)totalTarjeta)
                {
                    if (tarjeta < 0)
                    {
                        tarjeta = 0;
                    }
                    else
                    {
                        tarjeta = (float)totalTarjeta;
                        MessageBox.Show($"El monto de tarjeta tiene que ser menor o igual que el monto que hay en caja: {totalTarjeta.ToString("C2")}", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    txtTarjeta.Text = tarjeta.ToString();
                }
                else
                {
                    siRetiroTarjeta = true;
                }

                if (cheque < 0 && cheque > (float)totalCheque)
                {
                    if (cheque < 0)
                    {
                        cheque = 0;
                    }
                    else
                    {
                        cheque = (float)totalCheque;
                        MessageBox.Show($"El monto de cheque tiene que ser menor o igual que el monto que hay en caja: {totalCheque.ToString("C2")}", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    txtCheque.Text = cheque.ToString();
                }
                else
                {
                    siRetiroCheque = true;
                }

                if (vales < 0 && vales > (float)totalVales)
                {
                    if (vales < 0)
                    {
                        vales = 0;
                    }
                    else
                    {
                        vales = (float)totalVales;
                        MessageBox.Show($"El monto de vales tiene que ser menor o igual que el monto que hay en caja: {totalVales.ToString("C2")}", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    txtVales.Text = vales.ToString();
                }
                else
                {
                    siRetiroVales = true;
                }

                if (trans < 0 && trans > (float)totalTransferencia)
                {
                    if (trans < 0)
                    {
                        trans = 0;
                    }
                    else
                    {
                        trans = (float)totalTransferencia;
                        MessageBox.Show($"El monto de transferencia tiene que ser menor o igual que el monto que hay en caja: {totalTransferencia.ToString("C2")}", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    txtTrans.Text = trans.ToString();
                }
                else
                {
                    siRetiroTransferencia = true;
                }
            }

            cantidad = efectivo + tarjeta + cheque + vales + trans + credito;

            //if (cantidad < 0) { cantidad = 0; }

            if (!operacion.Equals(0))
            {
                if (cantidad < 0 && cantidad > (float)totalEnCaja)
                {
                    if (cantidad < 0)
                    {
                        cantidad = 0;
                    }
                    else
                    {
                        cantidad = (float)totalEnCaja;
                        MessageBox.Show($"El monto de retirar tiene que ser menor o igual que el monto que hay en caja: {totalEnCaja.ToString("C2")}", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            if (operacion.Equals(2))
            {
                totalRetiradoCorte = cantidad.ToString();
            }

            totalRetiradoCorte = !string.IsNullOrWhiteSpace(totalRetiradoCorte) ? totalRetiradoCorte : "0";

            // Si es igual a cero no procede la operacion de depositar o retirar
            if (cantidad == 0)
            {
                if (operacion == 0 || operacion == 1)
                {
                    var mensaje = string.Empty;

                    if (operacion == 1)
                    {
                        mensaje = "La cantidad a retirar debe ser mayor a cero";
                    }
                    else if (operacion == 0)
                    {
                        mensaje = "La cantidad a depositar debe ser mayor a cero";
                    }

                    MessageBox.Show(mensaje, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            string nombreEmpleado = string.Empty;
            string usuarioEmpleado = string.Empty;

            if (FormPrincipal.id_empleado > 0)
            {
                var datosEmpleado = mb.obtener_permisos_empleado(FormPrincipal.id_empleado, FormPrincipal.userID);

                nombreEmpleado = datosEmpleado[14];
                usuarioEmpleado = datosEmpleado[15];
            }

            string[] datos = new string[] { };
            int resultado = 0;
            bool retirarEfectivoDeSaldoInicial = false,
                 retirarTarjetaDeSaldoInicial = false,
                 retirarValesDeSaldoInicial = false,
                 retirarChequeDeSaldoInicial = false,
                 retirarTransferenciaDeSaldoInicial = false;

            if (operacion.Equals(1))
            {
                if (cantidadTotalEfectivoEnCaja <= 0 && efectivo >= 1)
                {
                    retirarEfectivoDeSaldoInicial = true;
                    concepto += " Retiro de efectivo del Saldo Inicial";
                    datos = new string[] {
                        tipoOperacion, cantidad.ToString("0.00"), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(), efectivo.ToString("0.00"), tarjeta.ToString("0.00"), vales.ToString("0.00"), cheque.ToString("0.00"), trans.ToString("0.00"), credito.ToString("0.00"), "0", FormPrincipal.id_empleado.ToString(), numFolio, totalRetiradoCorte
                    };
                }
                if (cantidadTotalTarjetaEnCaja <= 0 && tarjeta >= 1)
                {
                    retirarTarjetaDeSaldoInicial = true;
                    concepto += " Retiro de tarjeta del Saldo Inicial";
                    datos = new string[] {
                        tipoOperacion, cantidad.ToString("0.00"), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(), efectivo.ToString("0.00"), tarjeta.ToString("0.00"), vales.ToString("0.00"), cheque.ToString("0.00"), trans.ToString("0.00"), credito.ToString("0.00"), "0", FormPrincipal.id_empleado.ToString(), numFolio, totalRetiradoCorte
                    };
                }
                if (cantidadTotalValesEnCaja <= 0 && vales >= 1)
                {
                    retirarValesDeSaldoInicial = true;
                    concepto += " Retiro de vales del Saldo Inicial";
                    datos = new string[] {
                        tipoOperacion, cantidad.ToString("0.00"), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(), efectivo.ToString("0.00"), tarjeta.ToString("0.00"), vales.ToString("0.00"), cheque.ToString("0.00"), trans.ToString("0.00"), credito.ToString("0.00"), "0", FormPrincipal.id_empleado.ToString(), numFolio, totalRetiradoCorte
                    };
                }
                if (cantidadTotalCehqueEnCaja <= 0 && cheque >= 1)
                {
                    retirarChequeDeSaldoInicial = true;
                    concepto += " Retiro de cheque del Saldo Inicial";
                    datos = new string[] {
                        tipoOperacion, cantidad.ToString("0.00"), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(), efectivo.ToString("0.00"), tarjeta.ToString("0.00"), vales.ToString("0.00"), cheque.ToString("0.00"), trans.ToString("0.00"), credito.ToString("0.00"), "0", FormPrincipal.id_empleado.ToString(), numFolio, totalRetiradoCorte
                    };
                }
                if (cantidadTotalTransferenciaEnCaja <= 0 && trans >= 1)
                {
                    retirarTransferenciaDeSaldoInicial = true;
                    concepto += " Retiro de transferencia del Saldo Inicial";
                    datos = new string[] {
                        tipoOperacion, cantidad.ToString("0.00"), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(), efectivo.ToString("0.00"), tarjeta.ToString("0.00"), vales.ToString("0.00"), cheque.ToString("0.00"), trans.ToString("0.00"), credito.ToString("0.00"), "0", FormPrincipal.id_empleado.ToString(), numFolio, totalRetiradoCorte
                    };
                }
                else
                {
                    datos = new string[] {
                        tipoOperacion, cantidad.ToString("0.00"), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(), efectivo.ToString("0.00"), tarjeta.ToString("0.00"), vales.ToString("0.00"), cheque.ToString("0.00"), trans.ToString("0.00"), credito.ToString("0.00"), "0", FormPrincipal.id_empleado.ToString(), numFolio, totalRetiradoCorte
                    };
                }
            }
            else
            {
                if (operacion.Equals(2))
                {
                    fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    datos = new string[] {
                        tipoOperacion, cantidad.ToString("0.00"), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(), efectivo.ToString("0.00"), tarjeta.ToString("0.00"), vales.ToString("0.00"), cheque.ToString("0.00"), trans.ToString("0.00"), credito.ToString("0.00"), "0", FormPrincipal.id_empleado.ToString(), numFolio, totalRetiradoCorte
                    };

                    CajaN.botones = true;
                }
                else
                {
                    datos = new string[] {
                        tipoOperacion, cantidad.ToString("0.00"), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(), efectivo.ToString("0.00"), tarjeta.ToString("0.00"), vales.ToString("0.00"), cheque.ToString("0.00"), trans.ToString("0.00"), credito.ToString("0.00"), "0", FormPrincipal.id_empleado.ToString(), numFolio, totalRetiradoCorte
                    };
                }
            }

            tipoCorte = false;

            int idHistorialCorteDeCaja = 0;

            if (!FormPrincipal.userNickName.Contains("@"))
            {
                idHistorialCorteDeCaja = obtenerIDHistorialCorteDeCaja(CajaN.opcionComboBoxFiltroAdminEmp);
            }
            else
            {
                idHistorialCorteDeCaja = obtenerIDHistorialCorteDeCaja(FormPrincipal.id_empleado.ToString());
            }

            decimal montoRestante = 0;

            using (DataTable dtMontosDeSaldoInicialAModificar = cn.CargarDatos(cs.obtenerSaldoInicialPorIDDelHistorialCorteDeCaja(idHistorialCorteDeCaja)))
            {
                DataRow row = dtMontosDeSaldoInicialAModificar.Rows[0];

                if (retirarEfectivoDeSaldoInicial)
                {
                    montoRestante = (Convert.ToDecimal(row["SaldoInicialEfectivo"].ToString()) - (decimal)efectivo);
                    resultado = cn.EjecutarConsulta(cs.actualizarSaldoInicialDeEfectivo(idHistorialCorteDeCaja, montoRestante));
                }
                if (retirarTarjetaDeSaldoInicial)
                {
                    montoRestante = (Convert.ToDecimal(row["SaldoInicialTarjeta"].ToString()) - (decimal)tarjeta);
                    resultado = cn.EjecutarConsulta(cs.actualizarSaldoInicialDeTarjeta(idHistorialCorteDeCaja, montoRestante));
                }
                if (retirarValesDeSaldoInicial)
                {
                    montoRestante = (Convert.ToDecimal(row["SaldoInicialVales"].ToString()) - (decimal)vales);
                    resultado = cn.EjecutarConsulta(cs.actualizarSaldoInicialDeVales(idHistorialCorteDeCaja, montoRestante));
                }
                if (retirarChequeDeSaldoInicial)
                {
                    montoRestante = (Convert.ToDecimal(row["SaldoInicialCheque"].ToString()) - (decimal)cheque);
                    resultado = cn.EjecutarConsulta(cs.actualizarSaldoInicialDeCheque(idHistorialCorteDeCaja, montoRestante));
                }
                if (retirarTransferenciaDeSaldoInicial)
                {
                    montoRestante = (Convert.ToDecimal(row["SaldoInicialTransferencia"].ToString()) - (decimal)trans);
                    resultado = cn.EjecutarConsulta(cs.actualizarSaldoInicialDeTransferencia(idHistorialCorteDeCaja, montoRestante));
                }
            }

            if (retirarEfectivoDeSaldoInicial.Equals(false) && 
                retirarTarjetaDeSaldoInicial.Equals(false) && 
                retirarValesDeSaldoInicial.Equals(false) && 
                retirarChequeDeSaldoInicial.Equals(false) && 
                retirarTransferenciaDeSaldoInicial.Equals(false))
            {
                if (!FormPrincipal.userNickName.Contains("@"))
                {
                    resultado = cn.EjecutarConsulta(cs.OperacionCaja(datos, tipoCorte));
                }
                else
                {
                    tipoCorte = true;
                    resultado = cn.EjecutarConsulta(cs.OperacionCaja(datos, tipoCorte));
                }
            }

            // Ejecutr hilo para enviarnotificación
            var datosConfig = mb.ComprobarConfiguracion();

            if (datosConfig.Count > 0)
            {
                if (Convert.ToInt32(datosConfig[13]).Equals(1) && datos[0].ToString().Equals("deposito"))
                {
                    Thread AgregarRetiroDinero = new Thread(
                        () => Utilidades.cajaBtnAgregarRetiroCorteDineroCajaEmail(datos)
                    );

                    AgregarRetiroDinero.Start();
                }
                else if (Convert.ToInt32(datosConfig[14]).Equals(1) && datos[0].ToString().Equals("retiro"))
                {
                    Thread AgregarAgregarDinero = new Thread(
                        () => Utilidades.cajaBtnAgregarRetiroCorteDineroCajaEmail(datos)
                    );

                    AgregarAgregarDinero.Start();
                }
            }

            if (resultado > 0)
            {
                // Para generar Ticket al depositar dinero
                if (operacion == 0)
                {
                    if (Utilidades.AdobeReaderInstalado())
                    {
                        GenerarTicket(datos);
                    }
                    else
                    {
                        Utilidades.MensajeAdobeReader();
                    }
                }

                // Para generar Ticket al retirar dinero
                if (operacion == 1)
                {
                    if (Utilidades.AdobeReaderInstalado())
                    {
                        GenerarTicket(datos);
                    }
                    else
                    {
                        Utilidades.MensajeAdobeReader();
                    }
                }

                // Corte
                if (operacion == 2)
                {
                    // Se pausa por 1 segundo
                    Thread.Sleep(1000);

                    decimal cantidadObtenido = 0;

                    if (cantidad > 0)
                    {
                        cantidadObtenido = totalEnCaja - (decimal)cantidad;
                    }
                    else if (cantidad.Equals(0))
                    {
                        cantidadObtenido = totalEnCaja;
                    }

                    if (siRetiroEfectivo)
                    {
                        totalEfectivo -= (decimal)efectivo;
                    }
                    if (siRetiroTarjeta)
                    {
                        totalTarjeta -= (decimal)tarjeta;
                    }
                    if (siRetiroVales)
                    {
                        totalVales -= (decimal)vales;
                    }
                    if (siRetiroCheque)
                    {
                        totalCheque -= (decimal)cheque;
                    }
                    if (siRetiroTransferencia)
                    {
                        totalTransferencia -= (decimal)trans;
                    }

                    fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    var ultimoIDCaja = string.Empty;

                    using (DataTable dtUltimoIDCaja = cn.CargarDatos(cs.obtenerUltimoIDInsertadoEnCaja()))
                    {
                        if (!dtUltimoIDCaja.Rows[0][0].Equals(DBNull.Value) && !dtUltimoIDCaja.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtUltimoIDCaja.Rows)
                            {
                                ultimoIDCaja = item["ID"].ToString();
                            }
                        }
                    }

                    datos = new string[] {
                        ultimoIDCaja, FormPrincipal.userID.ToString(), FormPrincipal.id_empleado.ToString(), fechaOperacion, totalEfectivo.ToString(), totalTarjeta.ToString(), totalVales.ToString(), totalCheque.ToString(), totalTransferencia.ToString(), totalCredito.ToString(), "0", cantidad.ToString()
                    };

                    cn.EjecutarConsulta(cs.guardarHistorialCorteDeCaja(datos));

                    CajaN.botones = true;

                    //var efectivoRetirar = ValidarCampos(txtEfectivo.Text);
                    //var tarjetaRetirar = ValidarCampos(txtTarjeta.Text);
                    //var chequeRetirar = ValidarCampos(txtCheque.Text);
                    //var valesRetirar = ValidarCampos(txtVales.Text);
                    //var transRetirar = ValidarCampos(txtTrans.Text);
                    //var creditoRetirar = ValidarCampos(txtCredito.Text);

                    // Solo cuando es corte se hace esta resta, al total de cada forma de pago
                    // se le resta lo que el usuario quiere retirar menos el total retirado de cada
                    // forma de pago antes de que se haga el corte de caja
                    //var efectivoobtenido = 0f; 
                    //var tarjetaobtenido = 0f; 
                    //var chequeobtenido = 0f; 
                    //var valesobtenido = 0f; 
                    //var transobtenido = 0f; 
                    //var creditoobtenido = 0f; 
                    //var cantidadObtenido = 0f;

                    //if (CajaN.totCorte != "0")
                    //{
                    //efectivoobtenido = (totalEfectivo - efectivoRetirar - (convertEfectivo - CajaN.totalEfectivoAbono));// - CajaN.retiroEfectivo;
                    //tarjetaobtenido = (totalTarjeta - tarjetaRetirar - (convertTarjeta - CajaN.totalTarjetaAbono));// - CajaN.retiroTarjeta;
                    //chequeobtenido = (totalCheque - chequeRetirar - (convertCheque - CajaN.totalChequeAbono));// - CajaN.retiroCheque;
                    //valesobtenido = (totalVales - valesRetirar - (convertVales - CajaN.totalValesAbono));// - CajaN.retiroVales;
                    //transobtenido = (totalTransferencia - transRetirar - (convertTrans - CajaN.totalTransferenciaAbono));// - CajaN.retiroTrans;
                    //creditoobtenido = totalCredito - creditoRetirar;
                    //}
                    //else
                    //{
                    //efectivoobtenido = (totalEfectivo - efectivoRetirar);// - CajaN.retiroEfectivo;
                    //tarjetaobtenido = (totalTarjeta - tarjetaRetirar);// - CajaN.retiroTarjeta;
                    //chequeobtenido = (totalCheque - chequeRetirar);// - CajaN.retiroCheque;
                    //valesobtenido = (totalVales - valesRetirar);// - CajaN.retiroVales;
                    //transobtenido = (totalTransferencia - transRetirar);// - CajaN.retiroTrans;
                    //creditoobtenido = totalCredito - creditoRetirar;
                    //}

                    //if (cantidad > 0)
                    //{
                    //    cantidadObtenido = totalEnCaja - (decimal)cantidad;
                    //}
                    //else if (cantidad.Equals(0))
                    //{
                    //    cantidadObtenido = totalEnCaja;
                    //}

                    //if (siRetiroEfectivo)
                    //{
                    //    totalEfectivo -= (decimal)efectivo;
                    //}
                    //if (siRetiroTarjeta)
                    //{
                    //    totalTarjeta -= (decimal)tarjeta;
                    //}
                    //if (siRetiroVales)
                    //{
                    //    totalVales -= (decimal)vales;
                    //}
                    //if (siRetiroCheque)
                    //{
                    //    totalCheque -= (decimal)cheque;
                    //}
                    //if (siRetiroTransferencia)
                    //{
                    //    totalTransferencia -= (decimal)trans;
                    //}

                    //fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    //datos = new string[] {
                    //    "venta", cantidadObtenido.ToString(), "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(),
                    //    totalEfectivo.ToString(), totalTarjeta.ToString(), totalVales.ToString(), totalCheque.ToString(), totalTransferencia.ToString(), totalCredito.ToString(), "0", FormPrincipal.id_empleado.ToString()
                    //};

                    //cn.EjecutarConsulta(cs.OperacionCaja(datos));

                    //Thread CorteDinero = new Thread(
                    //    () => Utilidades.cajaBtnAgregarRetiroCorteDineroCajaEmail(datos)
                    //);

                    //CorteDinero.Start();

                    //CajaN.botones = true;
                    //if (Utilidades.AdobeReaderInstalado())
                    //{
                    //    GenerarTicket(datos);
                    //}
                    //else
                    //{
                    //    Utilidades.MensajeAdobeReader();
                    //}
                }
                this.Close();
            }
        }

        private int obtenerIDHistorialCorteDeCaja(string opcionComboBoxFiltroAdminEmp)
        {
            var IDHistorialCorteDeCaja = 0;

            if (opcionComboBoxFiltroAdminEmp.Equals("Admin"))
            {
                using (DataTable dtIDUltimoSaldoInicial = cn.CargarDatos(cs.obtenerUltimoIDSaldoInicialDelAdministrador()))
                {
                    if (!dtIDUltimoSaldoInicial.Rows.Count.Equals(0))
                    {
                        DataRow row = dtIDUltimoSaldoInicial.Rows[0];
                        IDHistorialCorteDeCaja = Convert.ToInt32(row["ID"].ToString());
                    }
                }
            }
            else if (!opcionComboBoxFiltroAdminEmp.Equals("Admin"))
            {
                using (DataTable dtIDUltimoSaldoInicial = cn.CargarDatos(cs.obtenerUltimoIDSaldoInicialDelEmpleado(opcionComboBoxFiltroAdminEmp)))
                {
                    if (!dtIDUltimoSaldoInicial.Rows.Count.Equals(0))
                    {
                        DataRow row = dtIDUltimoSaldoInicial.Rows[0];
                        IDHistorialCorteDeCaja = Convert.ToInt32(row["ID"].ToString());
                    }
                }
            }

            return IDHistorialCorteDeCaja;
        }

        private void GenerarTicket(string[] info)
        {

            var datos = FormPrincipal.datosUsuario;

            // tipo 0 = Agregar dinero
            // tipo 1 = Retirar dinero

            //Medidas de ticket de 57 y 80 mm
            //57mm = 161.28 pt
            //80mm = 226.08 pt

            var tipoPapel = 57;
            var anchoPapel = Convert.ToInt32(Math.Floor((((tipoPapel * 0.10) * 72) / 2.54)));
            var altoPapel = Convert.ToInt32(anchoPapel + 54);

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
                medidaFuenteMensaje = 10;
                medidaFuenteGrande = 10;
                medidaFuenteNegrita = 8;
                medidaFuenteNormal = 8;
            }
            else if (tipoPapel == 57)
            {
                medidaFuenteMensaje = 6;
                medidaFuenteGrande = 8;
                medidaFuenteNegrita = 6;
                medidaFuenteNormal = 6;
            }

            // Ruta donde se creara el archivo PDF
            var rutaArchivo = string.Empty;
            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\Caja\ticket_caja_" + Convert.ToDateTime(info[4]).ToString("yyyyMMddHHmmss") + ".pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\Caja\ticket_caja_" + Convert.ToDateTime(info[4]).ToString("yyyyMMddHHmmss") + ".pdf";
            }


            Document ticket = new Document(new iTextSharp.text.Rectangle(anchoPapel, altoPapel), 3, 3, 5, 0);
            PdfWriter writer = PdfWriter.GetInstance(ticket, new FileStream(rutaArchivo, FileMode.Create));

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteNormal);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, medidaFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteGrande);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteMensaje);

            ticket.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
            Paragraph subTitulo = new Paragraph("Ticket " + info[0].ToUpper() + "\nFecha: " + info[4] + "\nEmpleado: ADMIN", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;

            string contenido = $"Empleado: ADMIN\nEfectivo: ${info[6]}\nTarjeta: ${info[7]}\nVales: ${info[8]}\nCheque: ${info[9]}\nTransferencia: ${info[10]}";
            Paragraph cuerpo = new Paragraph(contenido, fuenteNormal);
            cuerpo.Alignment = Element.ALIGN_CENTER;

            float[] anchoColumnas = new float[] { 20f, 20f };

            PdfPTable tabla = new PdfPTable(2);
            tabla.WidthPercentage = 100;
            tabla.SetWidths(anchoColumnas);

            

            //======================================================================
            PdfPCell colEfectivo = new PdfPCell(new Phrase("Efectivo", fuenteNormal));
            colEfectivo.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivo.BorderWidth = 0;

            PdfPCell colEfectivoC = new PdfPCell(new Phrase($"${info[6]}", fuenteNormal));
            colEfectivoC.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoC.BorderWidth = 0;

            PdfPCell colTarjeta = new PdfPCell(new Phrase("Tarjeta", fuenteNormal));
            colTarjeta.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjeta.BorderWidth = 0;

            PdfPCell colTarjetaC = new PdfPCell(new Phrase($"${info[7]}", fuenteNormal));
            colTarjetaC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaC.BorderWidth = 0;

            PdfPCell colVales = new PdfPCell(new Phrase("Vales", fuenteNormal));
            colVales.HorizontalAlignment = Element.ALIGN_CENTER;
            colVales.BorderWidth = 0;

            PdfPCell colValesC = new PdfPCell(new Phrase($"${info[8]}", fuenteNormal));
            colValesC.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesC.BorderWidth = 0;

            PdfPCell colCheque = new PdfPCell(new Phrase("Cheque", fuenteNormal));
            colCheque.HorizontalAlignment = Element.ALIGN_CENTER;
            colCheque.BorderWidth = 0;

            PdfPCell colChequeC = new PdfPCell(new Phrase($"${info[9]}", fuenteNormal));
            colChequeC.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeC.BorderWidth = 0;

            PdfPCell colTrans = new PdfPCell(new Phrase("Transferencia", fuenteNormal));
            colTrans.HorizontalAlignment = Element.ALIGN_CENTER;
            colTrans.BorderWidth = 0;

            PdfPCell colTransC = new PdfPCell(new Phrase($"${info[10]}", fuenteNormal));
            colTransC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransC.BorderWidth = 0;

            PdfPCell colCredito = new PdfPCell(new Phrase("Crédito", fuenteNormal));
            colCredito.HorizontalAlignment = Element.ALIGN_CENTER;
            colCredito.BorderWidth = 0;

            PdfPCell colCreditoC = new PdfPCell(new Phrase($"${info[11]}", fuenteNormal));
            colCreditoC.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoC.BorderWidth = 0;

            var total = Convert.ToDecimal(info[6].ToString()) + Convert.ToDecimal(info[7].ToString()) + Convert.ToDecimal(info[8].ToString()) + Convert.ToDecimal(info[9].ToString()) + Convert.ToDecimal(info[10].ToString()) + Convert.ToDecimal(info[11].ToString());

            PdfPTable tabla2 = new PdfPTable(2);
            tabla2.WidthPercentage = 100;
            tabla2.SetWidths(anchoColumnas);

            PdfPCell celdaEnBlanco = new PdfPCell(new Phrase("TOTAL: ",fuenteNormal));
            celdaEnBlanco.HorizontalAlignment = Element.ALIGN_RIGHT;
            celdaEnBlanco.BorderWidth = 0;

            PdfPCell totalC = new PdfPCell(new Phrase($"{total.ToString("C2")}", fuenteNormal));
            totalC.HorizontalAlignment = Element.ALIGN_CENTER;
            totalC.BorderWidth = 0;

            tabla.AddCell(colEfectivo);
            tabla.AddCell(colEfectivoC);
            tabla.AddCell(colTarjeta);
            tabla.AddCell(colTarjetaC);
            tabla.AddCell(colVales);
            tabla.AddCell(colValesC);
            tabla.AddCell(colCheque);
            tabla.AddCell(colChequeC);
            tabla.AddCell(colTrans);
            tabla.AddCell(colTransC);
            tabla.AddCell(colCredito);
            tabla.AddCell(colCreditoC);
            tabla2.AddCell(celdaEnBlanco);
            tabla2.AddCell(totalC);

            //======================================================================

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));
            linea.SetLeading(0.5F, 0.5F);

            Paragraph concepto = new Paragraph("CONCEPTO", fuenteNegrita);
            concepto.Alignment = Element.ALIGN_CENTER;

            Paragraph conceptoExtra = new Paragraph(info[3], fuenteNormal);
            conceptoExtra.Alignment = Element.ALIGN_CENTER;

            ticket.Add(titulo);
            ticket.Add(subTitulo);
            ticket.Add(linea);
            ticket.Add(tabla);
            ticket.Add(linea);
            ticket.Add(tabla2);

            if (!string.IsNullOrWhiteSpace(info[3]))
            {
                ticket.Add(concepto);
                ticket.Add(conceptoExtra);
            }

            ticket.AddTitle("Ticket Corte Caja");
            ticket.AddAuthor("PUDVE");
            ticket.Close();
            writer.Close();

            VisualizadorTickets vt = new VisualizadorTickets(rutaArchivo, rutaArchivo);
            vt.ShowDialog();
        }

        private string obtenerNumFolio(string tipoOperacion)
        {
            var result = "1";

            var query = cn.CargarDatos($"SELECT NumFolio FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = '{tipoOperacion}' ORDER BY FechaOperacion DESC LIMIT 1");

            if (!query.Rows.Count.Equals(0))
            {
                result = query.Rows[0]["NumFolio"].ToString();

                result = (Convert.ToInt32(result) + 1).ToString();
            }

            return result;
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

        private void MensajeCantidad(decimal totalAParaRetirar, decimal cantidad, decimal saldoInicial, string tituloMensaje, object tb)
        {
            TextBox campo = tb as TextBox;

            MessageBox.Show($"La cantidad a retirar no puede ser mayor a {totalAParaRetirar.ToString("C2")}\n\nTotal de ventas en {tituloMensaje} = {cantidad.ToString("C2")}\nTotal de saldo inicial en {tituloMensaje} = {saldoInicial.ToString("C2")}", "Cantidad a retirar de " + tituloMensaje, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            campo.Text = totalAParaRetirar.ToString();
            campo.Focus();
            campo.SelectAll();
            //campo.SelectionStart = campo.Text.Length;
            //campo.SelectionLength = 0;
        }

        private void txtEfectivo_KeyUp(object sender, KeyEventArgs e)
        {
            var obtenerTxt = string.Empty;
            obtenerTxt = txtEfectivo.Text;
            if (!string.IsNullOrWhiteSpace(txtEfectivo.Text))
            {
                if (obtenerTxt.Equals("."))
                {
                    txtEfectivo.Text = "0.";
                    txtEfectivo.Select(txtEfectivo.Text.Length, 0);
                }
                else
                {
                    float efectivo = float.Parse(txtEfectivo.Text);

                    if (!operacion.Equals(0))
                    {
                        if ((decimal)efectivo > totalEfectivo)
                        {
                            MensajeCantidad(totalEfectivo, cantidadTotalEfectivoEnCaja, cantidadEfectivoSaldoInicialEnCaja, "Efectivo", sender);
                        }

                        //if (efectivo > (totalEfectivo/* - convertEfectivo*/) && operacion > 0)
                        //{
                        //    MensajeCantidad((totalEfectivo/* - convertEfectivo*/), sender);
                        //}
                    }
                }
            }
        }

        private void txtTarjeta_KeyUp(object sender, KeyEventArgs e)
        {
            var obtenerTxt = string.Empty;
            obtenerTxt = txtTarjeta.Text;

            if (!string.IsNullOrWhiteSpace(txtTarjeta.Text))
            {
                if (obtenerTxt.Equals("."))
                {
                    txtTarjeta.Text = "0.";
                    txtTarjeta.Select(txtTarjeta.Text.Length, 0);
                }
                else
                {
                    float tarjeta = float.Parse(txtTarjeta.Text);

                    if (!operacion.Equals(0))
                    {
                        if ((decimal)tarjeta > totalTarjeta)
                        {
                            MensajeCantidad(totalTarjeta, cantidadTotalTarjetaEnCaja, cantidadTarjetaSaldoInicialEnCaja, "Tarjeta", sender);
                        }
                        //if (tarjeta > (totalTarjeta /*- convertTarjeta*/) && operacion > 0)
                        //{
                        //    MensajeCantidad((totalTarjeta /*- convertTarjeta*/), sender);
                        //}
                    }
                }
            }
        }

        private void txtVales_KeyUp(object sender, KeyEventArgs e)
        {
            var obtenerTxt = string.Empty;
            obtenerTxt = txtVales.Text;

            if (!string.IsNullOrWhiteSpace(txtVales.Text))
            {
                if (obtenerTxt.Equals("."))
                {
                    txtVales.Text = "0.";
                    txtVales.Select(txtVales.Text.Length, 0);
                }
                else
                {
                    float vales = float.Parse(txtVales.Text);

                    if (!operacion.Equals(0))
                    {
                        if ((decimal)vales > totalVales)
                        {
                            MensajeCantidad(totalVales, cantidadTotalValesEnCaja, cantidadValesSaldoInicialEnCaja, "Vales", sender);
                        }
                        //if (vales > (totalVales /*- convertVales*/) && operacion > 0)
                        //{
                        //    MensajeCantidad((totalVales /*- convertVales*/), sender);
                        //}
                    }
                }
            }
        }

        private void txtCheque_KeyUp(object sender, KeyEventArgs e)
        {
            var obtenerTxt = string.Empty;
            obtenerTxt = txtCheque.Text;

            if (!string.IsNullOrWhiteSpace(txtCheque.Text))
            {
                if (obtenerTxt.Equals("."))
                {
                    txtCheque.Text = "0.";
                    txtCheque.Select(txtCheque.Text.Length, 0);
                }
                else
                {
                    float cheque = float.Parse(txtCheque.Text);

                    if (!operacion.Equals(0))
                    {
                        if ((decimal)cheque > totalCheque)
                        {
                            MensajeCantidad(totalCheque, cantidadTotalCehqueEnCaja, cantidadChequeSaldoInicialEnCaja, "Cheque", sender);
                        }
                        //if (cheque > (totalCheque /*- convertCheque*/) && operacion > 0)
                        //{
                        //    MensajeCantidad((totalCheque /*- convertCheque*/), sender);
                        //}
                    }
                }
            }
        }

        private void txtTrans_KeyUp(object sender, KeyEventArgs e)
        {
            var obtenerTxt = string.Empty;
            obtenerTxt = txtTrans.Text;

            if (!string.IsNullOrWhiteSpace(txtTrans.Text))
            {
                if (obtenerTxt.Equals("."))
                {
                    txtTrans.Text = "0.";
                    txtTrans.Select(txtTrans.Text.Length, 0);
                }
                else
                {
                    float trans = float.Parse(txtTrans.Text);

                    if (!operacion.Equals(0))
                    {
                        if ((decimal)trans > totalTransferencia)
                        {
                            MensajeCantidad(totalTransferencia, cantidadTotalTransferenciaEnCaja, cantidadTransferenciaSaldoInicialEnCaja, "Transferencia", sender);
                        }
                        //if (trans > (totalTransferencia /*- convertTrans*/) && operacion > 0)
                        //{
                        //    MensajeCantidad((totalTransferencia /*- convertTrans*/), sender);
                        //}
                    }
                }
            }
        }

        private void txtCredito_KeyUp(object sender, KeyEventArgs e)
        {
            //var obtenerTxt = string.Empty;
            //obtenerTxt = txtCredito.Text;

            //if (!string.IsNullOrWhiteSpace(txtCredito.Text))
            //{
            //    if (obtenerTxt.Equals("."))
            //    {
            //        txtCredito.Text = "0.";
            //        txtCredito.Select(txtCredito.Text.Length, 0);
            //    }
            //    else
            //    {
            //        float credito = float.Parse(txtCredito.Text);

            //        if ((decimal)credito > totalCredito)
            //        {
            //            MensajeCantidad(cantidadTotalCehqueEnCaja, cantidadChequeSaldoInicialEnCaja, "Crédito", sender);
            //        }
            //        //if (credito > totalCredito && operacion > 0)
            //        //{
            //        //    MensajeCantidad(totalCredito, sender);
            //        //}
            //    }
            //}
        }

        private void cerrarSesion()
        {
            FormCollection formulariosApp = Application.OpenForms;
            List<Form> formularioCerrar = new List<Form>();

            foreach (Form f in formulariosApp)
            {
                if (f.Name != "Login" && f.Name != "RespadoBaseDatos")
                {
                    formularioCerrar.Add(f);
                }
            }

            Form toClose = new Form();
            string name = string.Empty;

            formularioCerrar.Reverse();

            for (int i = 0; i <= formularioCerrar.Count - 1; i++)
            {
                toClose = formularioCerrar[i];
                name = toClose.Name;
                toClose.Close();
            }

            formularioCerrar.Clear();

            this.Hide();

            Login VentanaLogin = new Login();
            VentanaLogin.contadorMetodoTablas = 1;
            VentanaLogin.ShowDialog();
        }

        private void btnAgregarConcepto_Click(object sender, EventArgs e)
        {
            using (var conceptos = new ConceptosCaja("CAJA"))
            {
                conceptos.FormClosed += delegate
                {
                    idParaComboBox = ConceptosCaja.id;
                    CargarConceptos();

                    var x = string.Empty;

                    var getConcepto = cn.CargarDatos($"SELECT Concepto FROM ConceptosDinamicos WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idParaComboBox}'");
                    if (!getConcepto.Rows.Count.Equals(0))
                    {
                        foreach (DataRow concepto in getConcepto.Rows)
                        {
                            x = concepto["Concepto"].ToString();
                        }
                    }
                    //cbConceptos.SelectedIndex = cbConceptos.FindString(x);
                    cbConceptoConBusqueda.SelectedIndex = cbConceptos.FindString(x);
                };
                conceptos.ShowDialog();
            }
        }

        private void txtEfectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            int calcu = 0;

            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        txtEfectivo.Text = calculadora.lCalculadora.Text;
                    };

                    calcu = 0;
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
            int calcu = 0;
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        txtTarjeta.Text = calculadora.lCalculadora.Text;
                    };

                    calcu = 0;
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

        private void txtVales_KeyPress(object sender, KeyPressEventArgs e)
        {
            int calcu = 0;
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        txtVales.Text = calculadora.lCalculadora.Text;
                    };

                    calcu = 0;
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

        private void txtCheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            int calcu = 0;
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        txtCheque.Text = calculadora.lCalculadora.Text;
                    };

                    calcu = 0;
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

        private void txtTrans_KeyPress(object sender, KeyPressEventArgs e)
        {
            int calcu = 0;
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        txtTrans.Text = calculadora.lCalculadora.Text;
                    };

                    calcu = 0;
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

        private void txtCredito_KeyPress(object sender, KeyPressEventArgs e)
        {
            int calcu = 0;
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        txtCredito.Text = calculadora.lCalculadora.Text;
                    };

                    calcu = 0;
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

        private void txtEfectivo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtTarjeta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtVales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtCheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtTrans_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void AgregarRetirarDinero_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void AgregarRetirarDinero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnRetirarTodoElDinero_Click(object sender, EventArgs e)
        {
            ritirarTodoElDineroDisponible();
        }

        private void ritirarTodoElDineroDisponible()
        {
            txtEfectivo.Text = totalEfectivo.ToString();
            txtTarjeta.Text = totalTarjeta.ToString();
            txtVales.Text = totalVales.ToString();
            txtCheque.Text = totalCheque.ToString();
            txtTrans.Text = totalTransferencia.ToString();
            btnAceptar.Focus();
        }
    }
}
