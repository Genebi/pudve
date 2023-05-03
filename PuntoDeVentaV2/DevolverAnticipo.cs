using MySql.Data.MySqlClient;
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
    public partial class DevolverAnticipo : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();
        CargarDatosCaja cdc = new CargarDatosCaja();

        //Variables Abonos
        string resultadoConsultaAbonos = string.Empty;
        string efectivoAbonadoADevolver = string.Empty;
        string tarjetaAbonadoADevolver = string.Empty;
        string valesAbonadoADevolver = string.Empty;
        string chequeAbonadoADevolver = string.Empty;
        string transAbonadoADevolver = string.Empty;
        string fechaOperacionAbonadoADevolver = string.Empty;

        public static bool noCash { get; set; }
        public static int cancel { get; set; }
        public static bool ventaCanceladaCredito { get; set; }

        // Variables ventas
        float vEfectivo = 0f;
        float vTarjeta = 0f;
        float vVales = 0f;
        float vCheque = 0f;
        float vTrans = 0f;
        float vCredito = 0f;
        float vAnticipos = 0f;
        float totalVentas = 0f;

        // Variables anticipos
        float aEfectivo = 0f;
        float aTarjeta = 0f;
        float aVales = 0f;
        float aCheque = 0f;
        float aTrans = 0f;
        float totalAnticipos = 0f;

        // Variables depositos-Dinero Agregado
        float dEfectivo = 0f;
        float dTarjeta = 0f;
        float dVales = 0f;
        float dCheque = 0f;
        float dTrans = 0f;
        float totalDineroAgregado = 0f;

        // Variables caja
        float efectivo1 = 0f;
        float tarjeta1 = 0f;
        float vales1 = 0f;
        float cheque1 = 0f;
        float trans1 = 0f;
        float credito = 0f;
        float anticipos1 = 0f;
        float saldoInicial = 0f;
        float subtotal = 0f;
        float dineroRetirado = 0f;
        float totalCaja = 0f;

        // Variable retiro
        float retiroEfectivo = 0f;
        float retiroTarjeta = 0f;
        float retiroVales = 0f;
        float retiroCheque = 0f;
        float retiroTrans = 0f;
        float retiroCredito = 0f;

        // Variables de la seccionProductos
        string nombreP = "";
        string nombreAlterno1P = "";
        string nombreAlterno2P = "";
        float stockP = 0f;
        float precioP = 0f;
        float revisionP = 0f;
        string claveP = "";
        string codigoP = "";
        string historialP = "";
        string tipoP = "";

        string userNickName = "";
        //string nickUsuarioP = "";
        //string nickUsuarioC = "";

        public static DateTime fechaGeneral;

        private int idAnticipo = 0;
        private float importe = 0;
        private int tipo = 0;
        private int cancelarVenta = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idAnticipo">primero va el ID del Anticipo o la Venta segun sea el caso</param>
        /// <param name="importe">segundo va el importe</param>
        /// <param name="tipo">tercero va el tipo de operacion que va realizar (1, 2 o 3 segun sea el caso)</param>
        /// <param name="cancelarVenta">En cuarto va si es cancelar venta pagada(1) o a credito(2) </param>
        public DevolverAnticipo(int idAnticipo, float importe, int tipo = 1, int cancelarVenta = 0)
        {
            InitializeComponent();

            this.idAnticipo = idAnticipo;
            this.importe = importe;
            this.tipo = tipo;
            this.cancelarVenta = cancelarVenta;
        }

        private void DevolverAnticipo_Load(object sender, EventArgs e)
        {
            cbFormaPago.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            
            ventaCanceladaCredito = false;
            if (tipo == 1)
            {
                this.Text = "PUDVE - Devolver Anticipo";
                lbTitulo.Text = "Forma de pago para devolución";
            }

            if (tipo == 2)
            {
                this.Text = "PUDVE - Habilitar Anticipo";
                lbTitulo.Text = "Forma de pago para anticipo";
            }

            if (tipo == 3)
            {
                cancel = 1;
                ventaCanceladaCredito = true;
                this.Text = "PUDVE - Devolucion";
                lbTitulo.Text = "Forma de pago para devolucion";
            }

            //ComboBox Formas de pago
            Dictionary<string, string> pagos = new Dictionary<string, string>();
            pagos.Add("01", "01 - Efectivo");
            pagos.Add("02", "02 - Cheque nominativo");
            pagos.Add("03", "03 - Transferencia electrónica de fondos");
            pagos.Add("04", "04 - Tarjeta de crédito");
            pagos.Add("08", "08 - Vales de despensa");

            cbFormaPago.DataSource = pagos.ToArray();
            cbFormaPago.DisplayMember = "Value";
            cbFormaPago.ValueMember = "Key";

            Anticipos.Cancelado = false;
        }

        private void CargarSaldo()
        {
            MySqlConnection sql_con;
            MySqlCommand consultaUno, consultaDos;
            MySqlDataReader drUno, drDos;

            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                sql_con = new MySqlConnection($"datasource={servidor};port=6666;username=root;password=;database=pudve;");
            }
            else
            {
                sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            }

            sql_con.Open();

            var fechaDefault = Convert.ToDateTime("0001-01-01 00:00:00");

            var consultarFecha = $"SELECT FechaOperacion FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'corte' ORDER BY FeChaOperacion DESC LIMIT 1";
            consultaUno = new MySqlCommand(consultarFecha, sql_con);
            drUno = consultaUno.ExecuteReader();

            if (drUno.Read())
            {
                var fechaTmp = Convert.ToDateTime(drUno.GetValue(drUno.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                fechaDefault = Convert.ToDateTime(fechaTmp);
            }

            fechaGeneral = fechaDefault;
            drUno.Close();

            var consulta = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID}";
            consultaDos = new MySqlCommand(consulta, sql_con);
            drDos = consultaDos.ExecuteReader();

            //int saltar = 0;

            while (drDos.Read())
            {
                string operacion = drDos.GetValue(drDos.GetOrdinal("Operacion")).ToString();
                var auxiliar = Convert.ToDateTime(drDos.GetValue(drDos.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                var fechaOperacion = Convert.ToDateTime(auxiliar);

                if (operacion == "venta" && fechaOperacion > fechaDefault)
                {
                    // Se comento esto por que no guardaba el efectivo de la primera venta.
                    //if (saltar == 0) 
                    //{
                    //    saltar++;
                    //    continue;
                    //}

                    vEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    vTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    vVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    vCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    vTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    vCredito += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());
                    vAnticipos += float.Parse(drDos.GetValue(drDos.GetOrdinal("Anticipo")).ToString());
                    totalVentas = (vEfectivo + vTarjeta + vVales + vCheque + vTrans + vCredito + vAnticipos);
                }

                if (operacion == "anticipo" && fechaOperacion > fechaDefault)
                {
                    aEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    aTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    aVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    aCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    aTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    totalAnticipos = (aEfectivo + aTarjeta + aVales + aCheque + aTrans);

                }

                if (operacion == "deposito" && fechaOperacion > fechaDefault)
                {
                    dEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    dTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    dVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    dCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    dTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    totalDineroAgregado = (dEfectivo + dTarjeta + dVales + dCheque + dTrans);
                }

                if (operacion == "retiro" && fechaOperacion > fechaDefault)
                {
                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    retiroEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    retiroTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    retiroVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    retiroCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    retiroTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());
                    retiroCredito += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());


                }
            }

            // Apartado TOTAL EN CAJA
            efectivo1 = (vEfectivo + aEfectivo + dEfectivo) - retiroEfectivo;
            tarjeta1 = (vTarjeta + aTarjeta + dTarjeta) - retiroTarjeta;
            vales1 = (vVales + aVales + dVales) - retiroVales;
            cheque1 = (vCheque + aCheque + dCheque) - retiroCheque;
            trans1 = (vTrans + aTrans + dTrans) - retiroTrans;
            credito = vCredito;
            anticipos1 = vAnticipos;
            subtotal = efectivo1 + tarjeta1 + vales1 + cheque1 + trans1 + credito + saldoInicial;
            totalCaja = (subtotal - dineroRetirado);

            // Cerramos la conexion y el datareader
            //drUno.Close();
            drDos.Close();
            sql_con.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Anticipos.Cancelado = true;
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool realizado = false;
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var formaPago = cbFormaPago.SelectedValue.ToString();

            var efectivo = "0";
            var cheque = "0";
            var transferencia = "0";
            var tarjeta = "0";
            var vales = "0";
            var credito = "0";

            //Operacion para afectar la Caja
            if (formaPago == "01") { efectivo = importe.ToString(); }
            if (formaPago == "02") { cheque = importe.ToString(); }
            if (formaPago == "03") { transferencia = importe.ToString(); }
            if (formaPago == "04") { tarjeta = importe.ToString(); }
            if (formaPago == "08") { vales = importe.ToString(); }

            var status = 0;
            var operacion = string.Empty;
            var comentario = string.Empty;

            var efe = float.Parse(efectivo);
            var che = float.Parse(cheque);
            var tra = float.Parse(transferencia);
            var tar = float.Parse(tarjeta);
            var val = float.Parse(vales);

            // Devolver anticipo
            if (tipo == 1)
            {
                status = 4;
                operacion = "retiro";
                comentario = "devolucion anticipo";
                iniciarVariablesWebService();
                CargarSaldo();

                var datos = cdc.CargarSaldo("Caja");

                datos[50] = datos[50] == null ? "0" : datos[50];

                efectivo1 = float.Parse(datos[50]);
                Anticipos.SeCancelo = true;
            }

            // Habilitar anticipo
            if (tipo == 2)
            {
                status = 1;
                operacion = "deposito";
                comentario = "anticipo habilitado";
            }

            //Devolver dinero de venta cancelada
            if (tipo == 3)
            {
                iniciarVariablesWebService();
                CargarSaldo();
                var idVenta = idAnticipo;
                var formasPago = mb.ObtenerFormasPagoVenta(idVenta, FormPrincipal.userID);

                if (formasPago.Length > 0)
                {
                    var total1 = formasPago.Sum().ToString();
                    var efectivo1 = formasPago[0].ToString();
                    var tarjeta1 = formasPago[1].ToString();
                    var vales1 = formasPago[2].ToString();
                    var cheque1 = formasPago[3].ToString();
                    var transferencia1 = formasPago[4].ToString();
                    var credito1 = formasPago[5].ToString();
                    var anticipo1 = "0";

                    var fechaOperacion1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    var concepto = $"DEVOLUCION DINERO VENTA CANCELADA ID {idVenta}";
                    //var conceptoCredito = $"DEVOLUCION DINERO VENTA A CREDITO CANCELADA ID {idVenta}";

                    var dato = cdc.CargarSaldo("Ventas Canceladas");
                    //Estas variables debo cambiarles el saldo
                    //var efectivoConvert = float.Parse(efectivo1);
                    //var chequeConvet = float.Parse(cheque1);
                    //var tarjetaConvert = float.Parse(tarjeta1);
                    //var valesConvert = float.Parse(vales1);
                    //var transConvert = float.Parse(transferencia1);
                    var efectivoConvert = float.Parse(dato[51]);
                    var chequeConvet = float.Parse(dato[52]);
                    var tarjetaConvert = float.Parse(dato[53]);
                    var valesConvert = float.Parse(dato[54]);
                    var transConvert = float.Parse(dato[55]);


                    obtenerDatos(idVenta);

                    if (resultadoConsultaAbonos == "") { resultadoConsultaAbonos = "0"; }
                    if (efectivoAbonadoADevolver == "") { efectivoAbonadoADevolver = "0"; }
                    if (tarjetaAbonadoADevolver == "") { tarjetaAbonadoADevolver = "0"; }
                    if (valesAbonadoADevolver == "") { valesAbonadoADevolver = "0"; }
                    if (chequeAbonadoADevolver == "") { chequeAbonadoADevolver = "0"; }
                    if (transAbonadoADevolver == "") { transAbonadoADevolver = "0"; }

                    var totalBuscar = float.Parse(resultadoConsultaAbonos);
                    var efectivoBuscar = float.Parse(efectivoAbonadoADevolver);
                    var tarjetaBuscar = float.Parse(tarjetaAbonadoADevolver);
                    var valesBuscar = float.Parse(valesAbonadoADevolver);
                    var chequeBuscar = float.Parse(chequeAbonadoADevolver);
                    var transferenciaBuscar = float.Parse(transAbonadoADevolver);

                    ListadoVentas.validarEfectivo = efectivoBuscar;
                    ListadoVentas.validarTarjeta = tarjetaBuscar;
                    ListadoVentas.validarVales = valesBuscar;
                    ListadoVentas.validarCheque = chequeBuscar;
                    ListadoVentas.validarTrans = transferenciaBuscar;


                    //if (tipo == 3 && cbFormaPago.SelectedIndex == 0 && importe > (efectivoConvert + MetodosBusquedas.efectivoInicial /*+ ListadoVentas.validarEfectivo*/) || cbFormaPago.SelectedIndex == 1 && importe > (chequeConvet + MetodosBusquedas.chequeInicial /*+ ListadoVentas.validarCheque*/) ||
                    //    cbFormaPago.SelectedIndex == 2 && importe > (transConvert + MetodosBusquedas.transInicial /*+ ListadoVentas.validarTrans*/) || cbFormaPago.SelectedIndex == 3 && importe > (tarjetaConvert + MetodosBusquedas.tarjetaInicial /*+ ListadoVentas.validarTarjeta*/) ||
                    //    cbFormaPago.SelectedIndex == 4 && importe > (valesConvert + MetodosBusquedas.valesInicial /*+ ListadoVentas.validarVales*/))
                    //{

                    var idUltimoCorteDeCajaT = 0;
                    var fechaUltimoCorteDeCajaT = string.Empty;
                    var EfectivoEnCajaT = 0m;
                    var TarjetaEnCajaT = 0m;
                    var ValesEnCajaT = 0m;
                    var ChequeEnCajaT = 0m;
                    var TransferenciaEnCajaT = 0m;
                    var CreditoEnCajaT = 0m;
                    var AnticipoEnCajaT = 0m;

                    using (DataTable dtSaldosInicialesDeCaja = cn.CargarDatos(cs.CargarSaldoInicialSinAbrirCaja(FormPrincipal.userID, FormPrincipal.id_empleado)))
                    {
                        if (!dtSaldosInicialesDeCaja.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtSaldosInicialesDeCaja.Rows)
                            {
                                idUltimoCorteDeCajaT = Convert.ToInt32(item["IDCaja"].ToString());
                                var fechaUltimoCorteT = Convert.ToDateTime(item["Fecha"].ToString());
                                fechaUltimoCorteDeCajaT = fechaUltimoCorteT.ToString("yyyy-MM-dd HH:mm:ss");
                                EfectivoEnCajaT += (decimal)Convert.ToDouble(item["Efectivo"].ToString());
                                TarjetaEnCajaT += (decimal)Convert.ToDouble(item["Tarjeta"].ToString());
                                ValesEnCajaT += (decimal)Convert.ToDouble(item["Vales"].ToString());
                                ChequeEnCajaT += (decimal)Convert.ToDouble(item["Cheque"].ToString());
                                TransferenciaEnCajaT += (decimal)Convert.ToDouble(item["Transferencia"].ToString());
                                CreditoEnCajaT += (decimal)Convert.ToDouble(item["Credito"].ToString());
                                AnticipoEnCajaT += (decimal)Convert.ToDouble(item["Anticipo"].ToString());
                            }

                            using (DataTable dtSaldosInicialVentasDepostos = cn.CargarDatos(cs.SaldoVentasDepositos(FormPrincipal.userID, FormPrincipal.id_empleado, idUltimoCorteDeCajaT)))
                            {
                                if (!dtSaldosInicialVentasDepostos.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow item in dtSaldosInicialVentasDepostos.Rows)
                                    {
                                        EfectivoEnCajaT += (decimal)Convert.ToDouble(item["Efectivo"].ToString());
                                        TarjetaEnCajaT += (decimal)Convert.ToDouble(item["Tarjeta"].ToString());
                                        ValesEnCajaT += (decimal)Convert.ToDouble(item["Vales"].ToString());
                                        ChequeEnCajaT += (decimal)Convert.ToDouble(item["Cheque"].ToString());
                                        TransferenciaEnCajaT += (decimal)Convert.ToDouble(item["Transferencia"].ToString());
                                    }
                                }
                            }

                            using (DataTable dtSaldoInicialRetiros = cn.CargarDatos(cs.SaldoInicialRetiros(FormPrincipal.userID, FormPrincipal.id_empleado, idUltimoCorteDeCajaT)))
                            {
                                if (!dtSaldoInicialRetiros.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow item in dtSaldoInicialRetiros.Rows)
                                    {
                                        EfectivoEnCajaT -= (decimal)Convert.ToDouble(item["Efectivo"].ToString());
                                        TarjetaEnCajaT -= (decimal)Convert.ToDouble(item["Tarjeta"].ToString());
                                        ValesEnCajaT -= (decimal)Convert.ToDouble(item["Vales"].ToString());
                                        ChequeEnCajaT -= (decimal)Convert.ToDouble(item["Cheque"].ToString());
                                        TransferenciaEnCajaT -= (decimal)Convert.ToDouble(item["Transferencia"].ToString());
                                    }
                                }
                            }
                        }
                    }

                    if (formaPago == "01" && importe > (float)EfectivoEnCajaT || formaPago == "04" && importe > (float)TransferenciaEnCajaT || formaPago == "08" && importe > (float)ValesEnCajaT || formaPago == "02" && importe > (float)ChequeEnCajaT || formaPago == "03" && importe > (float)TarjetaEnCajaT)
                    {

                        MessageBox.Show("Dinero Insuficuente", "¡Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        noCash = true;
                        cancel = 1;
                        //realizado = false;

                    }
                    else
                    {
                        if (cancelarVenta == 1)
                        {
                            //Cancela las ventas
                            string[] datos = new string[] {
                                            "retiro", total1, "0", concepto, fechaOperacion1, FormPrincipal.userID.ToString(),
                                            efectivo1, tarjeta1, vales1, cheque1, transferencia1, credito1, anticipo1,FormPrincipal.id_empleado.ToString()
                                        };

                            cn.EjecutarConsulta(cs.OperacionCaja(datos));
                            realizado = true;
                            
                            this.Dispose();
                        }
                        else if (cancelarVenta == 2)
                        {
                            //Cancela las ventas a credito
                            realizado = true;
                        }
                    }
                }

            }

            var idUltimoCorteDeCaja = 0;
            var fechaUltimoCorteDeCaja = string.Empty;
            var EfectivoEnCaja = 0m;
            var TarjetaEnCaja = 0m;
            var ValesEnCaja = 0m;
            var ChequeEnCaja = 0m;
            var TransferenciaEnCaja = 0m;
            var CreditoEnCaja = 0m;
            var AnticipoEnCaja = 0m;

            using (DataTable dtSaldosInicialesDeCaja = cn.CargarDatos(cs.CargarSaldoInicialSinAbrirCaja(FormPrincipal.userID, FormPrincipal.id_empleado)))
            {
                if (!dtSaldosInicialesDeCaja.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in dtSaldosInicialesDeCaja.Rows)
                    {
                        idUltimoCorteDeCaja = Convert.ToInt32(item["IDCaja"].ToString());
                        var fechaUltimoCorte = Convert.ToDateTime(item["Fecha"].ToString());
                        fechaUltimoCorteDeCaja = fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss");
                        EfectivoEnCaja += (decimal)Convert.ToDouble(item["Efectivo"].ToString());
                        TarjetaEnCaja += (decimal)Convert.ToDouble(item["Tarjeta"].ToString());
                        ValesEnCaja += (decimal)Convert.ToDouble(item["Vales"].ToString());
                        ChequeEnCaja += (decimal)Convert.ToDouble(item["Cheque"].ToString());
                        TransferenciaEnCaja += (decimal)Convert.ToDouble(item["Transferencia"].ToString());
                        CreditoEnCaja += (decimal)Convert.ToDouble(item["Credito"].ToString());
                        AnticipoEnCaja += (decimal)Convert.ToDouble(item["Anticipo"].ToString());
                    }

                    using (DataTable dtSaldosInicialVentasDepostos = cn.CargarDatos(cs.SaldoVentasDepositos(FormPrincipal.userID, FormPrincipal.id_empleado, idUltimoCorteDeCaja)))
                    {
                        if (!dtSaldosInicialVentasDepostos.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtSaldosInicialVentasDepostos.Rows)
                            {
                                EfectivoEnCaja += (decimal)Convert.ToDouble(item["Efectivo"].ToString());
                                TarjetaEnCaja += (decimal)Convert.ToDouble(item["Tarjeta"].ToString());
                                ValesEnCaja += (decimal)Convert.ToDouble(item["Vales"].ToString());
                                ChequeEnCaja += (decimal)Convert.ToDouble(item["Cheque"].ToString());
                                TransferenciaEnCaja += (decimal)Convert.ToDouble(item["Transferencia"].ToString());
                            }
                        }
                    }

                    using (DataTable dtSaldoInicialRetiros = cn.CargarDatos(cs.SaldoInicialRetiros(FormPrincipal.userID, FormPrincipal.id_empleado, idUltimoCorteDeCaja)))
                    {
                        if (!dtSaldoInicialRetiros.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtSaldoInicialRetiros.Rows)
                            {
                                EfectivoEnCaja -= (decimal)Convert.ToDouble(item["Efectivo"].ToString());
                                TarjetaEnCaja -= (decimal)Convert.ToDouble(item["Tarjeta"].ToString());
                                ValesEnCaja -= (decimal)Convert.ToDouble(item["Vales"].ToString());
                                ChequeEnCaja -= (decimal)Convert.ToDouble(item["Cheque"].ToString());
                                TransferenciaEnCaja -= (decimal)Convert.ToDouble(item["Transferencia"].ToString());
                            }
                        }
                    }
                }
            }

            decimal dineroAntesDevolver = 0;

            if (formaPago == "01") { dineroAntesDevolver = EfectivoEnCaja; }
            if (formaPago == "02") { dineroAntesDevolver = ChequeEnCaja; }
            if (formaPago == "03") { dineroAntesDevolver = TransferenciaEnCaja; }
            if (formaPago == "04") { dineroAntesDevolver = TarjetaEnCaja; }
            if (formaPago == "08") { dineroAntesDevolver = ValesEnCaja; }

            if (tipo == 1 && Convert.ToDecimal(importe) > dineroAntesDevolver)
            {
                MessageBox.Show("Dinero Insuficuente", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cancel = 1;
            }
            else
            {
                if (tipo != 3)
                {
                    int resultado = cn.EjecutarConsulta(cs.CambiarStatusAnticipo(status, idAnticipo, FormPrincipal.userID));

                    if (resultado > 0)
                    {
                        //if ()
                        //{
                        //}
                        //var datoObtenido = "";
                        //var consultarVacio = cn.CargarDatos($"SELECT Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, FechaOperacion FROM Caja WHERE IDUsuario = {FormPrincipal.userID} ORDER BY FechaOperacion DESC LIMIT 1");
                        //for (int i=0; i<consultarVacio.Rows.Count; i++)
                        //{
                        //    //datoObtenido = consultarVacio.Rows[i][0,1].ToString();
                        //}
                        cn.EjecutarConsulta($"UPDATE Anticipos SET FormaPago = '{formaPago}' WHERE ID = {idAnticipo} AND IDUsuario = {FormPrincipal.userID}");

                        //Se devuelve el dinero del anticipo y se elimina el registro de la tabla Caja para que la cantidad total
                        //Que hay en caja sea correcta
                        //cn.EjecutarConsulta($"DELETE FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND FechaOperacion = '{fecha}'");
                        var cantidad = importe;

                        string[] datos = new string[] {
                            operacion, cantidad.ToString("0.00"), "0", comentario, fechaOperacion, FormPrincipal.userID.ToString(),
                            efectivo, tarjeta, vales, cheque, transferencia, credito, "0", FormPrincipal.id_empleado.ToString()
                        };

                        resultado = cn.EjecutarConsulta(cs.OperacionCaja(datos));
                        if (resultado > 0)
                        {
                            this.Dispose();
                        }
                    }
                }

                if (realizado == true)
                {
                    cancel = 2;

                    List<string> listaAbono = new List<string>();
                    var idVenta = idAnticipo;
                    var conceptoCredito = string.Empty;

                    //if (tipo.Equals(1))
                    //{
                    //    conceptoCredito = $"DEVOLUCION DINERO VENTA CANCELADA ID {idVenta}";
                    //}
                    //else
                    //{
                        conceptoCredito = $"DEVOLUCION DINERO VENTA A CREDITO CANCELADA ID {idVenta}";
                    //}
                    //var revisarSiTieneAbono = cn.CargarDatos($"SELECT sum(Total), sum(Efectivo), sum(Tarjeta), sum(Vales), sum(Cheque), sum(Transferencia), FechaOperacion FROM Abonos WHERE IDUsuario = {FormPrincipal.userID} AND IDVenta = {idVenta}");
                    string ultimoDate = string.Empty;
                    //if (!revisarSiTieneAbono.Rows.Count.Equals(0))// valida si la consulta esta vacia 
                    //{
                    //var fechaCorteUltima = cn.CargarDatos($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");
                    //if (!fechaCorteUltima.Rows.Count.Equals(0))
                    //{
                    //foreach (DataRow fechaUltimoCorte in fechaCorteUltima.Rows)
                    //{
                    //    ultimoDate = fechaUltimoCorte["FechaOperacion"].ToString();
                    //}
                    //DateTime fechaDelCorteCaja = DateTime.Parse(ultimoDate);

                    //var resultadoConsultaAbonos = string.Empty;
                    //var efectivoAbonadoADevolver = string.Empty;
                    //var tarjetaAbonadoADevolver = string.Empty;
                    //var valesAbonadoADevolver = string.Empty;
                    //var chequeAbonadoADevolver = string.Empty;
                    //var transAbonadoADevolver = string.Empty;
                    //var fechaOperacionAbonadoADevolver = string.Empty;

                    //foreach (DataRow contenido in revisarSiTieneAbono.Rows)
                    //        {
                    //            resultadoConsultaAbonos = contenido["sum(Total)"].ToString();
                    //            efectivoAbonadoADevolver = contenido["sum(Efectivo)"].ToString();
                    //            tarjetaAbonadoADevolver = contenido["sum(Tarjeta)"].ToString();
                    //            valesAbonadoADevolver = contenido["sum(Vales)"].ToString();
                    //            chequeAbonadoADevolver = contenido["sum(Cheque)"].ToString();
                    //            transAbonadoADevolver = contenido["sum(Transferencia)"].ToString();
                    //            fechaOperacionAbonadoADevolver = contenido["FechaOperacion"].ToString();
                    //        }
                    //        DateTime fechaAbonoRealizado = DateTime.Parse(fechaOperacionAbonadoADevolver);

                            listaAbono.Add(efectivoAbonadoADevolver);
                            listaAbono.Add(tarjetaAbonadoADevolver);
                            listaAbono.Add(valesAbonadoADevolver);
                            listaAbono.Add(chequeAbonadoADevolver);
                            listaAbono.Add(transAbonadoADevolver);
                            listaAbono.Add(resultadoConsultaAbonos);

                    //Valida si se hizo antes o despues del corte
                    //if (fechaAbonoRealizado > fechaDelCorteCaja)
                    //{
                    //    //string[] datos = new string[] {
                    //    //                "retiro", resultadoConsultaAbonos, "0", conceptoCredito, fechaOperacion, FormPrincipal.userID.ToString(),
                    //    //                efectivoAbonadoADevolver, tarjetaAbonadoADevolver, valesAbonadoADevolver, chequeAbonadoADevolver, transAbonadoADevolver, /*credito*/"0.00", /*anticipo*/"0"
                    //    //            };
                    //    //cn.EjecutarConsulta(cs.OperacionCaja(datos));
                    //}
                    /*else*/
                    //if (fechaAbonoRealizado > fechaDelCorteCaja)//Este es el bueno
                    //{
                    var saldoActual = ObtenerDatosCaja(/*fechaDelCorteCaja*/);

                                var resultado = tipoDevolucion(formaPago, saldoActual, listaAbono.ToArray());
                                //var tipoEstado = resultado[5].ToString();
                                if (resultado[5].Equals("False"))
                                {
                                    MessageBox.Show("Dinero Insuficuente", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    cancel = 1;
                                }
                                else
                                {
                                    if (tipo == 3)
                                    {
                                        cancel = 2;
                                        string[] datos = new string[] {
                                                        "retiro", resultadoConsultaAbonos, "0", conceptoCredito, fechaOperacion, FormPrincipal.userID.ToString(), efectivoAbonadoADevolver, tarjetaAbonadoADevolver, valesAbonadoADevolver, chequeAbonadoADevolver, transAbonadoADevolver, /*credito*/"0.00", /*anticipo*/"0",FormPrincipal.id_empleado.ToString()
                                                    };
                                        cn.EjecutarConsulta(cs.OperacionCaja(datos));
                                        this.Dispose();
                                    }
                                }
                            //}
                        //}
                    //}
                }
            }
        }

        private string[] ObtenerDatosCaja(/*DateTime fecha*/)
        {
            List<string> listaSaldo = new List<string>();

            //var total = string.Empty; var efectivo = string.Empty; var tarjeta = string.Empty; var vales = string.Empty; var cheque = string.Empty; var transferencia = string.Empty;
            //var query = cn.CargarDatos($"SELECT sum(Cantidad), sum(Efectivo), sum(Tarjeta), sum(Vales), sum(Cheque), sum(Transferencia) FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{fecha.ToString("yyyy-MM-dd HH:mm:ss")}' AND Operacion != 'retiro' AND Credito = '0.00'");

            //if (!query.Rows.Count.Equals(0))
            //{
            //    total = query.Rows[0]["sum(Cantidad)"].ToString();
            //    efectivo = query.Rows[0]["sum(Efectivo)"].ToString();
            //    tarjeta = query.Rows[0]["sum(Tarjeta)"].ToString();
            //    vales = query.Rows[0]["sum(Vales)"].ToString();
            //    cheque = query.Rows[0]["sum(Cheque)"].ToString();
            //    transferencia = query.Rows[0]["sum(Transferencia)"].ToString();
            //}

            //if (total == "") { total = "0"; }
            //if (efectivo == "") { efectivo = "0"; }
            //if (tarjeta == "") { tarjeta = "0"; }
            //if (vales == "") { vales = "0"; }
            //if (cheque == "") { cheque = "0"; }
            //if (transferencia == "") { transferencia = "0"; }

            //listaSaldo.Add(total);
            //listaSaldo.Add(efectivo);
            //listaSaldo.Add(tarjeta);
            //listaSaldo.Add(vales);
            //listaSaldo.Add(cheque);
            //listaSaldo.Add(transferencia);
            listaSaldo.Add(resultadoConsultaAbonos);
            listaSaldo.Add(efectivoAbonadoADevolver);
            listaSaldo.Add(tarjetaAbonadoADevolver);
            listaSaldo.Add(valesAbonadoADevolver);
            listaSaldo.Add(chequeAbonadoADevolver);
            listaSaldo.Add(transAbonadoADevolver);

            return listaSaldo.ToArray();
        }

        private string[] tipoDevolucion(string formaPago, string[] saldoActual, string[] cantidadAbono )
        {
            List<string> lista = new List<string>();

            var status = false;

            efectivoAbonadoADevolver = "0";
            tarjetaAbonadoADevolver = "0";
            valesAbonadoADevolver = "0";
            chequeAbonadoADevolver = "0";
            transAbonadoADevolver = "0";

            if (formaPago == "01")//Efectivo
            {
                //validarEfectivo = (double.Parse(efectivoAbonadoADevolver) - (double.Parse(saldoActual[1]) + float.Parse(cantidadAbono[5])));

                //if (validarEfectivo > 0)
                //{
                //    status = true;

                //}

                if ((double.Parse(saldoActual[1]) + float.Parse(cantidadAbono[0])) > (double.Parse(resultadoConsultaAbonos)))
                {
                    status = true;
                    efectivoAbonadoADevolver = cantidadAbono[5];
                }else
                {
                    efectivoAbonadoADevolver = "0";
                }

            }
            else if (formaPago == "04")//Tarjeta
            {
                //validarTarjeta = (double.Parse(tarjetaAbonadoADevolver) - (double.Parse(saldoActual[2]) + float.Parse(cantidadAbono[5])));

                //if (validarTarjeta > 0)
                //{
                //    status = true;

                //}

                if ((double.Parse(saldoActual[2]) + float.Parse(cantidadAbono[1])) > (double.Parse(resultadoConsultaAbonos)))
                {
                    status = true;
                    tarjetaAbonadoADevolver = cantidadAbono[5];
                }
                else
                {
                    tarjetaAbonadoADevolver = "0";
                }
            }
            else if (formaPago == "08")//Vales
            {
                //validarVales = (double.Parse(valesAbonadoADevolver) - (double.Parse(saldoActual[3]) + float.Parse(cantidadAbono[5])));

                //if (validarVales > 0)
                //{
                //    status = true;

                //}

                if ((double.Parse(saldoActual[3]) + float.Parse(cantidadAbono[2])) > (double.Parse(resultadoConsultaAbonos)))
                {
                    status = true;
                    valesAbonadoADevolver = cantidadAbono[5];
                }
                else
                {
                    valesAbonadoADevolver = "0";
                }
            }
            else if (formaPago == "02")//Cheque
            {
                //validarCheque = (double.Parse(chequeAbonadoADevolver) - (double.Parse(saldoActual[4]) + float.Parse(cantidadAbono[5])));

                //if (validarCheque > 0)
                //{
                //    status = true;

                //}

                if ((double.Parse(saldoActual[4]) + float.Parse(cantidadAbono[3])) > (double.Parse(resultadoConsultaAbonos)))
                {
                    status = true;
                    chequeAbonadoADevolver = cantidadAbono[5];
                }
                else
                {
                    chequeAbonadoADevolver  = "0";
                }
            }
            else if (formaPago == "03")//Transferencia
            {
                //validarTrans = (double.Parse(transAbonadoADevolver) - (double.Parse(saldoActual[5]) + float.Parse(cantidadAbono[5])));

                //if (validarTrans > 0)
                //{
                //    status = true;

                //}

                if ((double.Parse(saldoActual[5]) + float.Parse(cantidadAbono[4])) > (double.Parse(resultadoConsultaAbonos)))
                {
                    status = true;
                    transAbonadoADevolver = cantidadAbono[5];
                }
                else
                {
                    transAbonadoADevolver = "0";
                }
            }

            lista.Add(efectivoAbonadoADevolver.ToString());
            lista.Add(tarjetaAbonadoADevolver.ToString());
            lista.Add(valesAbonadoADevolver.ToString());
            lista.Add(chequeAbonadoADevolver.ToString());
            lista.Add(transAbonadoADevolver.ToString());
            lista.Add(status.ToString());

            return lista.ToArray();
        }


        private string[] obtenerDatosCAjaRetiro(DateTime fecha)
        {
            List<string> lista = new List<string>();

            var total = string.Empty; var efectivo = string.Empty; var tarjeta = string.Empty; var vales = string.Empty; var cheque = string.Empty; var transferencia = string.Empty;
            var query = cn.CargarDatos($"SELECT sum(Cantidad), sum(Efectivo), sum(Tarjeta), sum(Vales), sum(Cheque), sum(Transferencia) FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{fecha.ToString("yyyy-MM-dd HH:mm:ss")}' AND Operacion = 'retiro' AND Credito = '0.00'");
            
            if (!query.Rows.Count.Equals(0))
            {
                total = query.Rows[0]["sum(Cantidad)"].ToString();
                efectivo = query.Rows[0]["sum(Efectivo)"].ToString();
                tarjeta = query.Rows[0]["sum(Tarjeta)"].ToString();
                vales = query.Rows[0]["sum(Vales)"].ToString();
                cheque = query.Rows[0]["sum(Cheque)"].ToString();
                transferencia = query.Rows[0]["sum(Transferencia)"].ToString();
            }

            if (total == "") { total = "0"; }
            if (efectivo == "") { efectivo = "0"; }
            if (tarjeta == "") { tarjeta = "0"; }
            if (vales == "") { vales = "0"; }
            if (cheque == "") { cheque = "0"; }
            if (transferencia == "") { transferencia = "0"; }

            return lista.ToArray();
        }

        private void obtenerDatos(int idVenta)
        {
            var revisarSiTieneAbono = cn.CargarDatos($"SELECT * FROM Abonos WHERE IDUsuario = {FormPrincipal.userID} AND IDVenta = {idVenta}");
            string ultimoDate = string.Empty;

            var fechaCorteUltima = cn.CargarDatos($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");
            if (!revisarSiTieneAbono.Rows.Count.Equals(0))
            {
                foreach (DataRow fechaUltimoCorte in fechaCorteUltima.Rows)
                {
                    ultimoDate = fechaUltimoCorte["FechaOperacion"].ToString();
                }
                DateTime fechaDelCorteCaja = DateTime.Parse(ultimoDate);
                //var resultadoConsultaAbonos1 = string.Empty;
                //var efectivoAbonadoADevolver1 = string.Empty;
                //var tarjetaAbonadoADevolver1 = string.Empty;
                //var valesAbonadoADevolver1 = string.Empty;
                //var chequeAbonadoADevolver1 = string.Empty;
                //var transAbonadoADevolver1 = string.Empty;
                //var fechaOperacionAbonadoADevolver1 = string.Empty;

                foreach (DataRow contenido in revisarSiTieneAbono.Rows)
                {
                    resultadoConsultaAbonos = contenido["Total"].ToString();
                    efectivoAbonadoADevolver = contenido["Efectivo"].ToString();
                    tarjetaAbonadoADevolver = contenido["Tarjeta"].ToString();
                    valesAbonadoADevolver = contenido["Vales"].ToString();
                    chequeAbonadoADevolver = contenido["Cheque"].ToString();
                    transAbonadoADevolver = contenido["Transferencia"].ToString();
                    //fechaOperacionAbonadoADevolver = contenido["FechaOperacion"].ToString();
                }

            }

        }

        private void iniciarVariablesWebService()
        {
            //  Apartado de Caja  //
            vEfectivo = 0f;
            vTarjeta = 0f;
            vVales = 0f;
            vCheque = 0f;
            vTrans = 0f;
            vCredito = 0f;
            vAnticipos = 0f;
            totalVentas = 0f;

            aEfectivo = 0f;
            aTarjeta = 0f;
            aVales = 0f;
            aCheque = 0f;
            aTrans = 0f;
            totalAnticipos = 0f;

            dEfectivo = 0f;
            dTarjeta = 0f;
            dVales = 0f;
            dCheque = 0f;
            dTrans = 0f;
            totalDineroAgregado = 0f;

            efectivo1 = 0f;
            tarjeta1 = 0f;
            vales1 = 0f;
            cheque1 = 0f;
            trans1 = 0f;
            credito = 0f;
            anticipos1 = 0f;
            saldoInicial = 0f;
            subtotal = 0f;
            dineroRetirado = 0f;
            totalCaja = 0f;

            //  Apartado de Productos  //
            nombreP = "";
            nombreAlterno1P = "";
            nombreAlterno2P = "";
            stockP = 0f;
            precioP = 0f;
            revisionP = 0f;
            claveP = "";
            codigoP = "";
            historialP = "";
            tipoP = "";

            userNickName = FormPrincipal.userNickName;

        }

        private void cbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
