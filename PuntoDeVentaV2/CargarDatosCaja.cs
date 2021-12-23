using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    class CargarDatosCaja
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        float abonos = 0f;

        ///////////////////Devoluciones//////////////////////
        float devoluciones = 0f;
        float devolucionesEfectivo = 0f;
        float devolucionesTarjeta = 0f;
        float devolucionesVales = 0f;
        float devolucionesCheque = 0f;
        float devolucionesTransferencia = 0f;
        /// ////////////////////////////////////////////////

        public static DateTime fechaGeneral;

        // Variables Totales
        public static float totalEfectivo = 0f;
        public static float totalTarjeta = 0f;
        public static float totalVales = 0f;
        public static float totalCheque = 0f;
        public static float totalTransferencia = 0f;
        public static float totalCredito = 0f;

        // Variables Retiro
        public static float retiroEfectivo = 0f;
        public static float retiroTarjeta = 0f;
        public static float retiroVales = 0f;
        public static float retiroCheque = 0f;
        public static float retiroTrans = 0f;
        public static float retiroCredito = 0f;

        public static string efectivoCorte { get; set; }
        public static string tarjetaCorte { get; set; }
        public static string valesCorte { get; set; }
        public static string chequeCorte { get; set; }
        public static string transCorte { get; set; }
        public static string totCorte { get; set; }
        public static string date { get; set; }

        public float CargarSaldoInicial()
        {
            var saldoInicial = 0f;

            var tipodeMoneda = FormPrincipal.Moneda.Split('-');
            var moneda = tipodeMoneda[1].ToString().Trim().Replace("(", "").Replace(")", " ");

            //if (procedencia.Equals("Reportes"))
            //{
            //    saldoInicial = mb.SaldoInicialCajaReportes(FormPrincipal.userID, id);

            //}
            //else
            //{
                saldoInicial = mb.SaldoInicialCaja(FormPrincipal.userID);
            //}

            return saldoInicial;
        }

        public string[] CargarSaldo(string procedencia, int id = 0)
        {
            List<string> listaCaja = new List<string>();
            List<string> listaReportes = new List<string>();

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

            string consulta;

            if (FormPrincipal.userNickName.Contains("@"))
            {
                consulta = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND IdEmpleado = '{FormPrincipal.id_empleado}' AND FechaOperacion > '{fechaDefault.ToString("yyyy-MM-dd HH:mm:ss")}' ORDER BY FechaOperacion ASC";
            }
            else
            {
                consulta = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND FechaOperacion > '{fechaDefault.ToString("yyyy-MM-dd HH:mm:ss")}' ORDER BY FechaOperacion ASC";
            }

            consultaDos = new MySqlCommand(consulta, sql_con);
            drDos = consultaDos.ExecuteReader();

            // Variables ventas
            float vEfectivo = 0f;
            float vTarjeta = 0f;
            float vVales = 0f;
            float vCheque = 0f;
            float vTrans = 0f;
            float vCredito = 0f;
            float vAnticipos = 0f;

            // Variables anticipos
            float aEfectivo = 0f;
            float aTarjeta = 0f;
            float aVales = 0f;
            float aCheque = 0f;
            float aTrans = 0f;

            // Variables depositos
            float dEfectivo = 0f;
            float dTarjeta = 0f;
            float dVales = 0f;
            float dCheque = 0f;
            float dTrans = 0f;

            // Variables retiros
            float rEfectivo = 0f;
            float rTarjeta = 0f;
            float rVales = 0f;
            float rCheque = 0f;
            float rTransferencia = 0f;
            float devoluciones = 0f;

            // Variables caja
            float efectivo = 0f;
            float tarjeta = 0f;
            float vales = 0f;
            float cheque = 0f;
            float trans = 0f;
            //float abono = 0f;////
            float credito = 0f;
            float subtotal = 0f;
            float anticipos = 0f;

            // Variable retiro
            float dineroRetirado = 0f;
            retiroEfectivo = 0f;
            retiroTarjeta = 0f;
            retiroVales = 0f;
            retiroCheque = 0f;
            retiroTrans = 0f;
            retiroCredito = 0f;

            int saltar = 0;

            //Variables para anticipos y abonos
            float anticiposAplicados = 0f;
            float abonos = 0f;
            float abonoEfectivoI = 0f;
            float abonoTarjetaI = 0f;
            float abonoValesI = 0f;
            float abonoChequeI = 0f;
            float abonoTransferenciaI = 0f;

            var consultaAnticipoAplicado = ""; //Se agrego esta linea desde esta linea...
            string ultimoDate = "";
            try
            {
                var segundaConsulta = cn.CargarDatos($"SELECT sum(AnticipoAplicado) AS AnticipoAplicado FROM Anticipos  WHERE IDUsuario = '{FormPrincipal.userID}'");
                if (!segundaConsulta.Rows.Count.Equals(0) /*&& !string.IsNullOrWhiteSpace(segundaConsulta.ToString())*/)
                {
                    foreach (DataRow obtenerAnticipoAplicado in segundaConsulta.Rows)
                    {
                        if (string.IsNullOrWhiteSpace(obtenerAnticipoAplicado["AnticipoAplicado"].ToString()))
                        {
                            consultaAnticipoAplicado = "0";
                        }
                        else
                        {
                            consultaAnticipoAplicado = obtenerAnticipoAplicado["AnticipoAplicado"].ToString();
                        }
                    }
                    anticiposAplicados = float.Parse(consultaAnticipoAplicado); //Hasta esta linea.
                }
                //Obtenemos la fecha del ultimo corte
                var fechaCorteUltima = cn.CargarDatos($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");
                if (!fechaCorteUltima.Rows.Count.Equals(0))
                {
                    //if (!fechaCorteUltima.Rows.Count.Equals(0)) {  }

                    foreach (DataRow fechaUltimoCorte in fechaCorteUltima.Rows)
                    {
                        ultimoDate = fechaUltimoCorte["FechaOperacion"].ToString();
                    }
                    DateTime ultimoDateCorte = DateTime.Parse(ultimoDate);

                    //Obtenemos la cantidad de abonos realizados despues del ultimo corte de caja
                    var abonoEfectivo = ""; var abonoTarjeta = ""; var abonoVales = ""; var abonoCheque = ""; var abonoTransferencia = "";
                    using (var fechaMovimientos = cn.CargarDatos($"SELECT sum(Total)AS Total, sum(Efectivo)AS Efectivo, sum(Tarjeta)AS Tarjeta, sum(Vales)AS Vales, sum(Cheque)AS Cheque, sum(Transferencia)AS Transferencia FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{ultimoDateCorte.ToString("yyyy-MM-dd HH:mm:ss")}'"))
                    {
                        //var abono = "";
                        if (!fechaMovimientos.Rows.Count.Equals(0) /*&& !string.IsNullOrWhiteSpace(fechaMovimientos.ToString())*/)
                        {

                            foreach (DataRow cantidadAbono in fechaMovimientos.Rows)
                            {
                                if (string.IsNullOrWhiteSpace(cantidadAbono["Efectivo"].ToString()))
                                {
                                    abonoEfectivo = "0";
                                }
                                else
                                {
                                    abonoEfectivo = cantidadAbono["Efectivo"].ToString();
                                }

                                if (string.IsNullOrWhiteSpace(cantidadAbono["Tarjeta"].ToString()))
                                {
                                    abonoTarjeta = "0";
                                }
                                else
                                {
                                    abonoTarjeta = cantidadAbono["Tarjeta"].ToString();
                                }

                                if (string.IsNullOrWhiteSpace(cantidadAbono["Vales"].ToString()))
                                {
                                    abonoVales = "0";
                                }
                                else
                                {
                                    abonoVales = cantidadAbono["Vales"].ToString();
                                }

                                if (string.IsNullOrWhiteSpace(cantidadAbono["Cheque"].ToString()))
                                {
                                    abonoCheque = "0";
                                }
                                else
                                {
                                    abonoCheque = cantidadAbono["Cheque"].ToString();
                                }

                                if (string.IsNullOrWhiteSpace(cantidadAbono["Transferencia"].ToString()))
                                {
                                    abonoTransferencia = "0";
                                }
                                else
                                {
                                    abonoTransferencia = cantidadAbono["Transferencia"].ToString();
                                }

                            }
                            //abonos = float.Parse(abono);
                            abonoEfectivoI = float.Parse(abonoEfectivo);
                            abonoTarjetaI = float.Parse(abonoTarjeta);
                            abonoValesI = float.Parse(abonoVales);
                            abonoChequeI = float.Parse(abonoCheque);
                            abonoTransferenciaI = float.Parse(abonoTransferencia);
                            abonos = (abonoEfectivoI + abonoTarjetaI + abonoValesI + abonoChequeI + abonoTransferenciaI);
                        }
                    }



                    //Obtenemos la cantidad de Devoluciones realizados despues del ultimo corte de caja
                    using (var obtenerDevoluciones = cn.CargarDatos($@"SELECT sum(Total)AS Total, sum(Efectivo)AS Efectivo, sum(Tarjeta)AS Tarjeta, sum(Vales)AS Vales, sum(Cheque)AS Cheque, sum(Transferencia)AS Transferencia FROM Devoluciones WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{ultimoDateCorte.ToString("yyyy-MM-dd HH:mm:ss")}'"))
                    {
                        if (!obtenerDevoluciones.Rows.Count.Equals(0) /*&& !string.IsNullOrWhiteSpace(obtenerDevoluciones.ToString())*/)
                        {
                            string devolucionTotal = string.Empty, devolucionEfectivo = string.Empty, devolucionTarjeta = string.Empty, devolucionVales = string.Empty, devolucionCheque = string.Empty, devolucionTrans = string.Empty;
                            foreach (DataRow devol in obtenerDevoluciones.Rows)
                            {
                                if (string.IsNullOrWhiteSpace(devol["Total"].ToString()))
                                {
                                    devolucionTotal = "0";
                                    devolucionEfectivo = "0";
                                    devolucionTarjeta = "0";
                                    devolucionVales = "0";
                                    devolucionCheque = "0";
                                    devolucionTrans = "0";

                                }
                                else
                                {
                                    devolucionTotal = devol["Total"].ToString();
                                    devolucionEfectivo = devol["Efectivo"].ToString();
                                    devolucionTarjeta = devol["Tarjeta"].ToString();
                                    devolucionVales = devol["Vales"].ToString();
                                    devolucionCheque = devol["Cheque"].ToString();
                                    devolucionTrans = devol["Transferencia"].ToString();
                                }
                            }

                            devoluciones = float.Parse(devolucionTotal);

                            efectivoCorte = devolucionEfectivo.ToString();
                            tarjetaCorte = devolucionTarjeta.ToString();
                            valesCorte = devolucionVales.ToString();
                            chequeCorte = devolucionCheque.ToString();
                            transCorte = devolucionTrans.ToString();
                            totCorte = devoluciones.ToString();
                        }
                    }


                }
            }
            catch
            {

            }

            while (drDos.Read())
            {
                string operacion = drDos.GetValue(drDos.GetOrdinal("Operacion")).ToString();
                var auxiliar = Convert.ToDateTime(drDos.GetValue(drDos.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                var fechaOperacion = Convert.ToDateTime(auxiliar);

                if (operacion == "venta" && fechaOperacion > fechaDefault)
                {

                    if (saltar == 0 && !fechaDefault.ToString("yyyy-MM-dd HH:mm:ss").Equals("0001-01-01 00:00:00") && !CajaN.corteCaja.Equals(0) || !FormPrincipal.userNickName.Contains("@") && saltar == 0)
                    {
                        saltar++;
                        continue;
                    }

                    vEfectivo += (float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString())/*+MetodosBusquedas.efectivoInicial*/);
                    vTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    vVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    vCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    vTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    vCredito += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());
                    vAnticipos += float.Parse(drDos.GetValue(drDos.GetOrdinal("Anticipo")).ToString());
                }

                if (operacion == "anticipo" && fechaOperacion > fechaDefault)
                {
                    aEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    aTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    aVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    aCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    aTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                }

                if (operacion == "deposito" && fechaOperacion > fechaDefault)
                {
                    dEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    dTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    dVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    dCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    dTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
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

                    //dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());
                    retiroCredito += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());
                }
            }

            // Cerramos la conexion y el datareader
            //drUno.Close();
            drDos.Close();
            sql_con.Close();

            var tipodeMoneda = FormPrincipal.Moneda.Split('-');
            var moneda = tipodeMoneda[1].ToString().Trim().Replace("(", "").Replace(")", " ");

            //var datos = cdc.CargarSaldo("Caja");

            var credi = (vCredito - retiroCredito);
            if (credi < 0) { credi = 0; }

            verificarCantidadAbonos();

            //// Apartado TOTAL EN CAJA
            //efectivo = (vEfectivo + aEfectivo + dEfectivo + abonoEfectivoI) - rEfectivo; if (efectivo < 0) { efectivo = 0; }
            //tarjeta = (vTarjeta + aTarjeta + dTarjeta + abonoTarjetaI) - rTarjeta; if (tarjeta < 0) { tarjeta = 0; }
            //vales = (vVales + aVales + dVales + abonoValesI) - rVales; if (vales < 0) { vales = 0; }
            //cheque = (vCheque + aCheque + dCheque + abonoChequeI) - rCheque; if (cheque < 0) { cheque = 0; }
            //trans = (vTrans + aTrans + dTrans + abonoTransferenciaI) - rTransferencia; if (trans < 0) { trans = 0; }
            //credito = vCredito;
            ////anticipos = vAnticipos;
            //anticipos = anticiposAplicados;
            //subtotal = (efectivo + tarjeta + vales + cheque + trans + /*saldoInicial*/CargarSaldoInicial()); if (subtotal < 0) { subtotal = 0; }

            // Apartado VENTAS
            listaCaja.Add(vEfectivo.ToString());//lbTEfectivo.Text = moneda + vEfectivo.ToString("0.00");  //0
            listaCaja.Add(vTarjeta.ToString());//lbTTarjeta.Text = moneda + vTarjeta.ToString("0.00");     //1
            listaCaja.Add(vVales.ToString());//lbTVales.Text = moneda + vVales.ToString("0.00");           //2
            listaCaja.Add(vCheque.ToString());//lbTCheque.Text = moneda + vCheque.ToString("0.00");        //3
            listaCaja.Add(vTrans.ToString());//lbTTrans.Text = moneda + vTrans.ToString("0.00");           //4
            listaCaja.Add(credi.ToString());//lbTCredito.Text = moneda + credi.ToString("0.00");           //5
            ////lbTAnticipos.Text = "$" + vAnticipos.ToString("0.00");
            listaCaja.Add(anticiposAplicados.ToString());//lbTAnticipos.Text = moneda + anticiposAplicados.ToString("0.00"); //6
            listaCaja.Add(((vEfectivo + vTarjeta + vVales + vCheque + vTrans + (credi) + anticiposAplicados) + /*totalAbonos*/abonos).ToString());//lbTVentas.Text = moneda + (vEfectivo + vTarjeta + vVales + vCheque + vTrans + (credi) + /*vAnticipos*/anticiposAplicados).ToString("0.00"); //7

            ////Variables de Abonos en Ventas
            listaCaja.Add(abonoEfectivoI.ToString());//lbEfectivoAbonos.Text = "$" + abonoEfectivoI.ToString("0.00");  //8
            listaCaja.Add(abonoTarjetaI.ToString());//lbTarjetaAbonos.Text = "$" + abonoTarjetaI.ToString("0.00");     //9
            listaCaja.Add(abonoValesI.ToString());//lbValesAbonos.Text = "$" + abonoValesI.ToString("0.00");           //10
            listaCaja.Add(abonoChequeI.ToString());//lbChequeAbonos.Text = "$" + abonoChequeI.ToString("0.00");        //11
            listaCaja.Add(abonoTransferenciaI.ToString());//lbTransferenciaAbonos.Text = "$" + abonoTransferenciaI.ToString("0.00");   //12
            listaCaja.Add(/*totalAbonos*/abonos.ToString());//lbTCreditoC.Text = moneda + abonos/*(abonoEfectivoI + abonoTarjetaI + abonoValesI + abonoChequeI + abonoTransferenciaI)*/.ToString("0.00");    //13

            //lbTotalAbonos.Text = "$" + abonoEfectivoI.ToString("0.00");

            // Apartado ANTICIPOS RECIBIDOS
            listaCaja.Add(aEfectivo.ToString());//lbTEfectivoA.Text = moneda + aEfectivo.ToString("0.00");         //14
            listaCaja.Add(aTarjeta.ToString());//lbTTarjetaA.Text = moneda + aTarjeta.ToString("0.00");            //15
            listaCaja.Add(aVales.ToString());//lbTValesA.Text = moneda + aVales.ToString("0.00");                  //16
            listaCaja.Add(aCheque.ToString());//lbTChequeA.Text = moneda + aCheque.ToString("0.00");               //17
            listaCaja.Add(aTrans.ToString());//lbTTransA.Text = moneda + aTrans.ToString("0.00");                  //18
            listaCaja.Add((aEfectivo + aTarjeta + aVales + aCheque + aTrans).ToString());//lbTAnticiposA.Text = moneda + (aEfectivo + aTarjeta + aVales + aCheque + aTrans).ToString("0.00");                                                   //19

            // Apartado DINERO AGREGADO
            listaCaja.Add(dEfectivo.ToString());//lbTEfectivoD.Text = moneda + dEfectivo.ToString("0.00");         //20
            listaCaja.Add(dTarjeta.ToString());//lbTTarjetaD.Text = moneda + dTarjeta.ToString("0.00");            //21
            listaCaja.Add(dVales.ToString());//lbTValesD.Text = moneda + dVales.ToString("0.00");                  //22
            listaCaja.Add(dCheque.ToString());//lbTChequeD.Text = moneda + dCheque.ToString("0.00");               //23
            listaCaja.Add(dTrans.ToString());//lbTTransD.Text = moneda + dTrans.ToString("0.00");                  //24
            listaCaja.Add((dEfectivo + dTarjeta + dVales + dCheque + dTrans).ToString());//lbTAgregado.Text = moneda + (dEfectivo + dTarjeta + dVales + dCheque + dTrans).ToString("0.00");                                                          //25

            // Apartado Dinero Retirado
            listaCaja.Add(retiroEfectivo.ToString());//lbEfectivoR.Text = moneda + " -" + retiroEfectivo.ToString("0.00");         //26
            listaCaja.Add(retiroTarjeta.ToString());//lbTarjetaR.Text = moneda + " -" + retiroTarjeta.ToString("0.00");            //27
            listaCaja.Add(retiroVales.ToString());//lbValesR.Text = moneda + " -" + retiroVales.ToString("0.00");                  //28
            listaCaja.Add(retiroCheque.ToString());//lbChequeR.Text = moneda + " -" + retiroCheque.ToString("0.00");               //29
            listaCaja.Add(retiroTrans.ToString());//lbTransferenciaR.Text = moneda + " -" + retiroTrans.ToString("0.00");          //30
            ////lbTAnticiposC.Text = "$ -" + vAnticipos.ToString("0.00");
            listaCaja.Add(anticiposAplicados.ToString());//lbTAnticiposC.Text = moneda + " -" + anticiposAplicados.ToString("0.00");//31
            listaCaja.Add(devoluciones.ToString());//lbDevoluciones.Text = moneda + " -" + devoluciones.ToString("0.00");          //32
            listaCaja.Add((retiroEfectivo + retiroTarjeta + retiroVales + retiroCheque + retiroTrans + /*anticiposAplicados +*/ devoluciones).ToString());//lbTRetirado.Text = moneda + " -" + (retiroEfectivo + retiroTarjeta + retiroVales + retiroCheque + retiroTrans + /*vAnticipos*/anticiposAplicados + devoluciones).ToString("0.00");                                     //33

            // Apartado TOTAL EN CAJA
            efectivo = (vEfectivo + aEfectivo + dEfectivo + abonoEfectivoI) - (rEfectivo + devolucionesEfectivo); if (efectivo < 0) { efectivo = 0; }
            listaCaja.Add(efectivo.ToString());        //34
            tarjeta = (vTarjeta + aTarjeta + dTarjeta + abonoTarjetaI) - (rTarjeta + devolucionesTarjeta); if (tarjeta < 0) { tarjeta = 0; }
            listaCaja.Add(tarjeta.ToString());         //35
            vales = (vVales + aVales + dVales + abonoValesI) - (rVales + devolucionesVales); if (vales < 0) { vales = 0; }
            listaCaja.Add(vales.ToString());           //36
            cheque = (vCheque + aCheque + dCheque + abonoChequeI) - (rCheque + devolucionesCheque); if (cheque < 0) { cheque = 0; }
            listaCaja.Add(cheque.ToString());          //37
            trans = (vTrans + aTrans + dTrans + abonoTransferenciaI) - (rTransferencia + devolucionesTransferencia); if (trans < 0) { trans = 0; }
            listaCaja.Add(trans.ToString());           //38
            credito = credi;
            listaCaja.Add(credito.ToString());         //39
            //anticipos = vAnticipos;
            anticipos = anticiposAplicados;
            listaCaja.Add(anticipos.ToString());       //40
            subtotal = (efectivo + tarjeta + vales + cheque + trans /*+ credito*//*+ abonos */+ CargarSaldoInicial() /*+ vCredito*/)/* - devoluciones*/; if (subtotal < 0) { subtotal = 0; }
            listaCaja.Add(subtotal.ToString());        //41

            var totalF = (efectivo - retiroEfectivo); if (totalF < 0) { totalF = 0; }
            var totalTa = (tarjeta - retiroTarjeta); if (totalTa < 0) { totalTa = 0; }
            var totalV = (vales - retiroVales); if (totalV < 0) { totalV = 0; }
            var totalC = (cheque - retiroCheque); if (totalC < 0) { totalC = 0; }
            var totalTr = (trans - retiroTrans); if (totalTr < 0) { totalTr = 0; }

            listaCaja.Add(totalF.ToString()); //lbTEfectivoC.Text = moneda + (totalF).ToString("0.00");         //42
            listaCaja.Add(totalTa.ToString()); //lbTTarjetaC.Text = moneda + (totalTa).ToString("0.00");        //43
            listaCaja.Add(totalV.ToString()); //lbTValesC.Text = moneda + (totalV).ToString("0.00");            //44
            listaCaja.Add(totalC.ToString()); //lbTChequeC.Text = moneda + (totalC).ToString("0.00");           //45
            listaCaja.Add(totalTr.ToString()); //lbTTransC.Text = moneda + (totalTr).ToString("0.00");          //46
            listaCaja.Add(abonos.ToString()); //lbTCreditoC.Text = "$" + /*credito*/abonos.ToString("0.00");   // lbTCreditoC Esta etiqueta es la de Abonos---------------------------------                                            //47

            //lbTAnticiposC.Text = "$" + anticipos.ToString("0.00"); 
            var ant = 0f;
            listaCaja.Add(CargarSaldoInicial().ToString()); //lbTSaldoInicial.Text = moneda + saldoInicial.ToString("0.00"); //48
            if (credito < retiroCredito) { ant = 0f; } else { ant = (credi - retiroCredito); }
            listaCaja.Add(ant.ToString());                                                                                   //49
            //lbTSubtotal.Text = "$" + subtotal.ToString("0.00");
            //lbTDineroRetirado.Text = "$" + dineroRetirado.ToString("0.00");
            listaCaja.Add((subtotal - (dineroRetirado /*+ devoluciones*/)).ToString()); //lbTTotalCaja.Text = moneda + (subtotal - (dineroRetirado + devoluciones)).ToString("0.00");                                                                //50

            // Variables de clase
            totalEfectivo = efectivo - retiroEfectivo; listaCaja.Add(totalEfectivo.ToString());           //51
            totalTarjeta = tarjeta - retiroTarjeta; listaCaja.Add(totalTarjeta.ToString());            //52
            totalVales = vales - retiroVales; listaCaja.Add(totalVales.ToString());              //53
            totalCheque = cheque - retiroCheque; listaCaja.Add(totalCheque.ToString());             //54
            totalTransferencia = trans - retiroTrans; listaCaja.Add(totalTransferencia.ToString());      //55
            totalCredito = credito - retiroCredito; listaCaja.Add(totalCredito.ToString());            //56




            ///////////////////Variables con los datos de los abonos////////////////////////
            listaCaja.Add(efectivoCorte);//57
            listaCaja.Add(tarjetaCorte);//58
            listaCaja.Add(valesCorte);//59
            listaCaja.Add(chequeCorte);//60
            listaCaja.Add(transCorte);//61
            listaCaja.Add(totCorte);//62
            ////////////////////////////////////////////////////////////////////////////////
            

            ///////////////////////////////////////////////// Lista para losreportes /////////////////////////////////////////

            //if (!id.Equals(0))
            //{
            //    var cantRetiradaCorte = consultaTotales(id); v 


            //    var totalesVentas = ((vEfectivo + vTarjeta + vVales + vCheque + vTrans + (credi) + anticiposAplicados) + totalAbonos);
            //    var totalesAnticipos = (aEfectivo + aTarjeta + aVales + aCheque + aTrans);
            //    var totalesAgregado = (dEfectivo + dTarjeta + dVales + dCheque + dTrans);
            //    var totalesRetirado = (retiroEfectivo + retiroTarjeta + retiroVales + retiroCheque + retiroTrans + /*vAnticipos*/anticiposAplicados + devoluciones);
            //    var totales = (subtotal - (dineroRetirado + devoluciones));

            //    listaReportes.Add($"Efectivo:|{vEfectivo}|Efectivo:|{aEfectivo}|Efectivo:|{dEfectivo}|Efectivo:|{retiroEfectivo}|Efectivo:|{efectivo}"); //1
            //    listaReportes.Add($"Tarjeta:|{vTarjeta}|Tarjeta:|{aTarjeta}|Tarjeta:|{dTarjeta}|Tarjeta:|{retiroTarjeta}|Tarjeta:|{tarjeta}");
            //    listaReportes.Add($"Vales:|{vVales}|Vales:|{aVales}|Vales:|{dVales}|Vales:|{retiroVales}|Vales:|{vales}");
            //    listaReportes.Add($"Cheque:|{vCheque}|Cheque:|{aCheque}|Cheque:|{dCheque}|Cheque:|{retiroCheque}|Cheque:|{cheque}");
            //    listaReportes.Add($"Transferencia:|{vTrans}|Transferencia:|{aTrans}|Transferencia:|{dTrans}|Transferencia:|{retiroTrans}|Transferencia:|{trans}");
            //    listaReportes.Add($"Crédito:|{credi}|{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|Anticipos Utilizados::|{anticiposAplicados}|Saldo Inicial::|{CargarSaldoInicial().ToString()}");
            //    listaReportes.Add($"Abonos:|{totalAbonos}|{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|Devoluciones:|{devoluciones}|Crédito:|{ant}");
            //    listaReportes.Add($"Anticipos Utilizados:|{anticiposAplicados}|{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}");
            //    listaReportes.Add($"{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|Cantidad retirada al corte:|{cantRetiradaCorte}");
            //    listaReportes.Add($"{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|{string.Empty}|Total en Caja antes del corte:|{(subtotal - (dineroRetirado + devoluciones))}");

            //    listaReportes.Add($"Total Ventas:|{totalesVentas}|Total Anticipos:|{totalesAnticipos}|Total Agregado:|{totalesAgregado}|Total Retirado:|{totalesRetirado}|Total en Caja despues del corte:|{totales}");

            //}
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            string[] result = { };
            //if (procedencia.Equals("Caja"))
            //{
            //    result = listaCaja.ToArray();
            //}
            //else if (procedencia.Equals("Reportes"))
            //{
            //    result = listaReportes.ToArray();
            //}
            result = listaCaja.ToArray();

            return result;

        }

        public void verificarCantidadAbonos()
        {
            string ultimoDate = "";
            try
            {
                var fechaCorteUltima = cn.CargarDatos($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");
                if (!fechaCorteUltima.Rows.Count.Equals(0) /*&& !string.IsNullOrWhiteSpace(fechaCorteUltima.ToString())*/)
                {
                    foreach (DataRow fechaUltimoCorte in fechaCorteUltima.Rows)
                    {
                        ultimoDate = fechaUltimoCorte["FechaOperacion"].ToString();
                    }
                    DateTime fechaFinAbonos = DateTime.Parse(ultimoDate);

                    var fechaMovimientos = cn.CargarDatos($"SELECT sum(Total)AS Total FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{fechaFinAbonos.ToString("yyyy-MM-dd HH:mm:ss")}'");
                    var abono = "";

                    if (!fechaMovimientos.Rows.Count.Equals(0) /*&& !string.IsNullOrWhiteSpace(fechaMovimientos.ToString())*/)
                    {
                        foreach (DataRow cantidadAbono in fechaMovimientos.Rows)
                        {
                            if (string.IsNullOrWhiteSpace(cantidadAbono["Total"].ToString()))
                            {
                                abono = "0";
                            }
                            else
                            {
                                abono = cantidadAbono["Total"].ToString();
                            }
                        }
                        abonos = float.Parse(abono);
                    }
                    else
                    {
                        abonos = 0f;
                    }
                }
                //else
                //{
                //    var fechaMovimientos = cn.CargarDatos($"SELECT sum(Total) FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}'");
                //        var abono = "";
                //        foreach (DataRow cantidadAbono in fechaMovimientos.Rows)
                //        {
                //            abono = cantidadAbono["sum(Total)"].ToString();
                //        }
                //        abonos = float.Parse(abono);
                //    }
            }
            catch
            {

            }

            try
            {
                var fechaCorteUltima = cn.CargarDatos($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");
                if (!fechaCorteUltima.Rows.Count.Equals(0) /*&& !string.IsNullOrWhiteSpace(fechaCorteUltima.ToString())*/)
                {
                    foreach (DataRow fechaUltimoCorte in fechaCorteUltima.Rows)
                    {
                        ultimoDate = fechaUltimoCorte["FechaOperacion"].ToString();
                    }
                    DateTime fechaFinAbonos = DateTime.Parse(ultimoDate);

                    var fechaMovimientos = cn.CargarDatos($"SELECT sum(Total) AS Total, sum(Efectivo) AS Efectivo, sum(Tarjeta) AS Tarjeta, sum(Vales) AS Vales,  sum(Cheque) AS Cheque,  sum(Transferencia) AS Transferencia FROM devoluciones WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{fechaFinAbonos.ToString("yyyy-MM-dd HH:mm:ss")}'");
                    var devolucion = ""; var devolucionEfectivo = ""; var devolucionTarjeta = ""; var devolucionVales = ""; var devolucionCheque = ""; var devolucionTrans = "";

                    if (!fechaMovimientos.Rows.Count.Equals(0) /*&& !string.IsNullOrWhiteSpace(fechaMovimientos.ToString())*/)
                    {
                        foreach (DataRow cantidadAbono in fechaMovimientos.Rows)
                        {
                            if (string.IsNullOrWhiteSpace(cantidadAbono["Total"].ToString()))
                            {
                                devolucion = "0";
                                devolucionEfectivo = "0";
                                devolucionTarjeta = "0";
                                devolucionVales = "0";
                                devolucionCheque = "0";
                                devolucionTrans = "0";
                            }
                            else
                            {
                                devolucion = cantidadAbono["Total"].ToString();
                                devolucionEfectivo = cantidadAbono["Efectivo"].ToString();
                                devolucionTarjeta = cantidadAbono["Tarjeta"].ToString();
                                devolucionVales = cantidadAbono["Vales"].ToString();
                                devolucionCheque = cantidadAbono["Cheque"].ToString();
                                devolucionTrans = cantidadAbono["Transferencia"].ToString();
                            }
                        }
                        devoluciones = float.Parse(devolucion);
                        devolucionesEfectivo = float.Parse(devolucionEfectivo);
                        devolucionesTarjeta = float.Parse(devolucionTarjeta);
                        devolucionesVales = float.Parse(devolucionVales);
                        devolucionesCheque = float.Parse(devolucionCheque);
                        devolucionesTransferencia = float.Parse(devolucionTrans);
                    }
                    else
                    {
                        devoluciones = 0f;
                    }
                }
            }
            catch
            {

            }


            //if (abonos > 0)
            //{
            //    lbCambioAbonos.Visible = true;
            //}
            //else
            //{
            //    lbCambioAbonos.Visible = false;
            //}

            //if (devoluciones > 0)
            //{
            //    lbCambioDevoluciones.Visible = true;
            //}
            //else
            //{
            //    lbCambioDevoluciones.Visible = false;
            //}

            //if (MetodosBusquedas.totalSInicial > 0)
            //{
            //    lbSaldoInicialInfo.Visible = true;
            //}
            //else
            //{
            //    lbSaldoInicialInfo.Visible = false;
            //}
        }

        private string consultaTotales(int id)
        {
            var result = string.Empty;
            var consultaUltimoCorte = cn.CargarDatos($"SELECT CantidadRetiradaCorte FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' AND ID  = '{id}' ORDER BY FechaOperacion DESC");

            if (consultaUltimoCorte.Rows.Count.Equals(0))
            {
                result = consultaUltimoCorte.Rows[0]["CantidadRetiradaCorte"].ToString();
            }
            else
            {
                result = "0";
            }

            return result;
        }

        public DataTable obtenerDepositosRetiros(string procedencia, string operacion, int id = 0)
        {
            string[] fechas;
            DateTime primerFecha = new DateTime();
            DateTime segundaFecha = new DateTime();
            //DateTime fechaGeneral = new DateTime();

            if (procedencia.Equals("Reportes"))
            {
                fechas = obtenerFechas(id);
                primerFecha = Convert.ToDateTime(fechas[1]);
                segundaFecha = Convert.ToDateTime(fechas[0]);
            }


            //anchoColumnas = new float[] { 100f, 100f, 100f, 100f, 100f, 100f, 100f };

            //Paragraph tituloDepositos = new Paragraph("HISTORIAL DE DEPOSITOS\n\n", fuenteGrande);
            //tituloDepositos.Alignment = Element.ALIGN_CENTER;

            //MySqlConnection sql_con;
            //MySqlCommand sql_cmd;
            //MySqlDataReader dr;


            var query = string.Empty;
            if (procedencia.Equals("Caja"))
            {
                query = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = '{operacion}' AND FechaOperacion > '{fechaGeneral.ToString("yyyy-MM-dd HH:mm:ss")}'";
            }
            else if (procedencia.Equals("Reportes"))
            {
                query = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = '{operacion}' AND (FechaOperacion BETWEEN '{primerFecha.ToString("yyyy-MM-dd HH:mm:ss")}' AND  '{segundaFecha.ToString("yyyy-MM-dd HH:mm:ss")}')";
            }

            //using (sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;")) {
            //    //sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            //    sql_con.Open();
            //    sql_cmd = new MySqlCommand(query, sql_con);
            //    dr = sql_cmd.ExecuteReader();

            var dr = cn.CargarDatos(query);
            return dr;
        }

        //dr.Close();
        //sql_con.Close();

        private string[] obtenerFechas(int id)
        {
            List<string> lista = new List<string>();

            var fecha = string.Empty;

            var primerFecha = string.Empty; var segundaFecha = string.Empty;
            var query = cn.CargarDatos($"SELECT DISTINCT FechaOperacion FROM Caja  WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{id}'");
            var segundaQuery = cn.CargarDatos($"SELECT DISTINCT FechaOperacion FROM Caja  WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' AND ID < '{id}' ORDER BY FechaOperacion DESC LIMIT 1");

            if (!query.Rows.Count.Equals(0))
            {
                segundaFecha = query.Rows[0]["FechaOperacion"].ToString();

                if (!segundaQuery.Rows.Count.Equals(0))
                {
                    primerFecha = segundaQuery.Rows[0]["FechaOperacion"].ToString();
                }
                else
                {
                    primerFecha = $"<{segundaFecha}";
                }

                lista.Add(segundaFecha.ToString());
                lista.Add(primerFecha);
            }

            //if (tipoBusqueda.Equals("Corte"))//Cuando es corte de caja
            //{

            //}
            //else if (tipoBusqueda.Equals("DA"))//Cuanto es dinero agregado
            //{

            //}
            //else if (tipoBusqueda.Equals("DR"))//Cuando es dinero retirado
            //{

            //}
            return lista.ToArray();
        }
    }
}

