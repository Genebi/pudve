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
        float devoluciones = 0f;

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

            saldoInicial = mb.SaldoInicialCaja(FormPrincipal.userID);

            return saldoInicial;
        }

        public float[] CargarSaldo()
        {
            List<float> lista = new List<float>();
            //verificarCantidadAbonos();

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

            var consulta = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND FechaOperacion > '{fechaDefault.ToString("yyyy-MM-dd HH:mm:ss")}' ORDER BY FechaOperacion ASC";
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
                    if (saltar == 0 && !fechaDefault.ToString("yyyy-MM-dd HH:mm:ss").Equals("0001-01-01 00:00:00"))
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

            var credi = (vCredito - retiroCredito);
            if (credi < 0) { credi = 0; }

            var totalAbonos = (abonoEfectivoI + abonoTarjetaI + abonoValesI + abonoChequeI + abonoTransferenciaI);
            // Apartado VENTAS
            lista.Add(vEfectivo);//lbTEfectivo.Text = moneda + vEfectivo.ToString("0.00");  //0
            lista.Add(vTarjeta);//lbTTarjeta.Text = moneda + vTarjeta.ToString("0.00");     //1
            lista.Add(vVales);//lbTVales.Text = moneda + vVales.ToString("0.00");           //2
            lista.Add(vCheque);//lbTCheque.Text = moneda + vCheque.ToString("0.00");        //3
            lista.Add(vTrans);//lbTTrans.Text = moneda + vTrans.ToString("0.00");           //4
            lista.Add(credi);//lbTCredito.Text = moneda + credi.ToString("0.00");           //5
            ////lbTAnticipos.Text = "$" + vAnticipos.ToString("0.00");
            lista.Add(anticiposAplicados);//lbTAnticipos.Text = moneda + anticiposAplicados.ToString("0.00"); //6
            lista.Add((vEfectivo + vTarjeta + vVales + vCheque + vTrans + (credi) + anticiposAplicados) + totalAbonos);//lbTVentas.Text = moneda + (vEfectivo + vTarjeta + vVales + vCheque + vTrans + (credi) + /*vAnticipos*/anticiposAplicados).ToString("0.00"); //7

            ////Variables de Abonos en Ventas
            lista.Add(abonoEfectivoI);//lbEfectivoAbonos.Text = "$" + abonoEfectivoI.ToString("0.00");  //8
            lista.Add(abonoTarjetaI);//lbTarjetaAbonos.Text = "$" + abonoTarjetaI.ToString("0.00");     //9
            lista.Add(abonoValesI);//lbValesAbonos.Text = "$" + abonoValesI.ToString("0.00");           //10
            lista.Add(abonoChequeI);//lbChequeAbonos.Text = "$" + abonoChequeI.ToString("0.00");        //11
            lista.Add(abonoTransferenciaI);//lbTransferenciaAbonos.Text = "$" + abonoTransferenciaI.ToString("0.00");   //12
            lista.Add(totalAbonos);//lbTCreditoC.Text = moneda + abonos/*(abonoEfectivoI + abonoTarjetaI + abonoValesI + abonoChequeI + abonoTransferenciaI)*/.ToString("0.00");    //13

            //lbTotalAbonos.Text = "$" + abonoEfectivoI.ToString("0.00");

            // Apartado ANTICIPOS RECIBIDOS
            lista.Add(aEfectivo);//lbTEfectivoA.Text = moneda + aEfectivo.ToString("0.00");         //14
            lista.Add(aTarjeta);//lbTTarjetaA.Text = moneda + aTarjeta.ToString("0.00");            //15
            lista.Add(aVales);//lbTValesA.Text = moneda + aVales.ToString("0.00");                  //16
            lista.Add(aCheque);//lbTChequeA.Text = moneda + aCheque.ToString("0.00");               //17
            lista.Add(aTrans);//lbTTransA.Text = moneda + aTrans.ToString("0.00");                  //18
            lista.Add((aEfectivo + aTarjeta + aVales + aCheque + aTrans));//lbTAnticiposA.Text = moneda + (aEfectivo + aTarjeta + aVales + aCheque + aTrans).ToString("0.00");                                                   //19

            // Apartado DINERO AGREGADO
            lista.Add(dEfectivo);//lbTEfectivoD.Text = moneda + dEfectivo.ToString("0.00");         //20
            lista.Add(dTarjeta);//lbTTarjetaD.Text = moneda + dTarjeta.ToString("0.00");            //21
            lista.Add(dVales);//lbTValesD.Text = moneda + dVales.ToString("0.00");                  //22
            lista.Add(dCheque);//lbTChequeD.Text = moneda + dCheque.ToString("0.00");               //23
            lista.Add(dTrans);//lbTTransD.Text = moneda + dTrans.ToString("0.00");                  //24
            lista.Add((dEfectivo + dTarjeta + dVales + dCheque + dTrans));//lbTAgregado.Text = moneda + (dEfectivo + dTarjeta + dVales + dCheque + dTrans).ToString("0.00");                                                          //25

            // Apartado Dinero Retirado
            lista.Add(retiroEfectivo);//lbEfectivoR.Text = moneda + " -" + retiroEfectivo.ToString("0.00");         //26
            lista.Add(retiroTarjeta);//lbTarjetaR.Text = moneda + " -" + retiroTarjeta.ToString("0.00");            //27
            lista.Add(retiroVales);//lbValesR.Text = moneda + " -" + retiroVales.ToString("0.00");                  //28
            lista.Add(retiroCheque);//lbChequeR.Text = moneda + " -" + retiroCheque.ToString("0.00");               //29
            lista.Add(retiroTrans);//lbTransferenciaR.Text = moneda + " -" + retiroTrans.ToString("0.00");          //30
            ////lbTAnticiposC.Text = "$ -" + vAnticipos.ToString("0.00");
            lista.Add(anticiposAplicados);//lbTAnticiposC.Text = moneda + " -" + anticiposAplicados.ToString("0.00");//31
            lista.Add(devoluciones);//lbDevoluciones.Text = moneda + " -" + devoluciones.ToString("0.00");          //32
            lista.Add((retiroEfectivo + retiroTarjeta + retiroVales + retiroCheque + retiroTrans + /*vAnticipos*/anticiposAplicados + devoluciones));//lbTRetirado.Text = moneda + " -" + (retiroEfectivo + retiroTarjeta + retiroVales + retiroCheque + retiroTrans + /*vAnticipos*/anticiposAplicados + devoluciones).ToString("0.00");                                     //33

            // Apartado TOTAL EN CAJA
            efectivo = (vEfectivo + aEfectivo + dEfectivo + abonoEfectivoI) - rEfectivo; if (efectivo < 0) { efectivo = 0; }
            lista.Add(efectivo);        //34
            tarjeta = (vTarjeta + aTarjeta + dTarjeta + abonoTarjetaI) - rTarjeta; if (tarjeta < 0) { tarjeta = 0; }
            lista.Add(tarjeta);         //35
            vales = (vVales + aVales + dVales + abonoValesI) - rVales; if (vales < 0) { vales = 0; }
            lista.Add(vales);           //36
            cheque = (vCheque + aCheque + dCheque + abonoChequeI) - rCheque; if (cheque < 0) { cheque = 0; }
            lista.Add(cheque);          //37
            trans = (vTrans + aTrans + dTrans + abonoTransferenciaI) - rTransferencia; if (trans < 0) { trans = 0; }
            lista.Add(trans);           //38
            credito = credi;
            lista.Add(credito);         //39
            //anticipos = vAnticipos;
            anticipos = anticiposAplicados;
            lista.Add(anticipos);       //40
            subtotal = (efectivo + tarjeta + vales + cheque + trans /*+ credito*//*+ abonos*/ + CargarSaldoInicial() /*+ vCredito*/)/* - devoluciones*/; if (subtotal < 0) { subtotal = 0; }
            lista.Add(subtotal);        //41

            var totalF = (efectivo - retiroEfectivo); if (totalF < 0) { totalF = 0; }   
            var totalTa = (tarjeta - retiroTarjeta); if (totalTa < 0) { totalTa = 0; }       
            var totalV = (vales - retiroVales); if (totalV < 0) { totalV = 0; }              
            var totalC = (cheque - retiroCheque); if (totalC < 0) { totalC = 0; }            
            var totalTr = (trans - retiroTrans); if (totalTr < 0) { totalTr = 0; }           

            lista.Add(totalF); //lbTEfectivoC.Text = moneda + (totalF).ToString("0.00");         //42
            lista.Add(totalTa); //lbTTarjetaC.Text = moneda + (totalTa).ToString("0.00");        //43
            lista.Add(totalV); //lbTValesC.Text = moneda + (totalV).ToString("0.00");            //44
            lista.Add(totalC); //lbTChequeC.Text = moneda + (totalC).ToString("0.00");           //45
            lista.Add(totalTr); //lbTTransC.Text = moneda + (totalTr).ToString("0.00");          //46
            lista.Add(abonos); //lbTCreditoC.Text = "$" + /*credito*/abonos.ToString("0.00");   // lbTCreditoC Esta etiqueta es la de Abonos---------------------------------                                            //47

            //lbTAnticiposC.Text = "$" + anticipos.ToString("0.00"); 
            var ant = 0f;
            lista.Add(CargarSaldoInicial()); //lbTSaldoInicial.Text = moneda + saldoInicial.ToString("0.00"); //48
            if (credito < retiroCredito) { ant = 0f; } else { ant = (credi - retiroCredito); }
            lista.Add(ant);                                                                                   //49
            //lbTSubtotal.Text = "$" + subtotal.ToString("0.00");
            //lbTDineroRetirado.Text = "$" + dineroRetirado.ToString("0.00");
            lista.Add((subtotal - (dineroRetirado + devoluciones))); //lbTTotalCaja.Text = moneda + (subtotal - (dineroRetirado + devoluciones)).ToString("0.00");                                                                //50

            // Variables de clase
            totalEfectivo = efectivo - retiroEfectivo;  lista.Add(totalEfectivo);           //51
            totalTarjeta = tarjeta - retiroTarjeta;     lista.Add(totalTarjeta);            //52
            totalVales = vales - retiroVales;           lista.Add(totalVales);              //53
            totalCheque = cheque - retiroCheque;        lista.Add(totalCheque);             //54
            totalTransferencia = trans - retiroTrans;   lista.Add(totalTransferencia);      //55
            totalCredito = credito - retiroCredito;     lista.Add(totalCredito);            //56

            verificarCantidadAbonos();

            return lista.ToArray();
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

                    var fechaMovimientos = cn.CargarDatos($"SELECT sum(Total)AS Total FROM devoluciones WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{fechaFinAbonos.ToString("yyyy-MM-dd HH:mm:ss")}'");
                    var devolucion = "";

                    if (!fechaMovimientos.Rows.Count.Equals(0) /*&& !string.IsNullOrWhiteSpace(fechaMovimientos.ToString())*/)
                    {
                        foreach (DataRow cantidadAbono in fechaMovimientos.Rows)
                        {
                            if (string.IsNullOrWhiteSpace(cantidadAbono["Total"].ToString()))
                            {
                                devolucion = "0";
                            }
                            else
                            {
                                devolucion = cantidadAbono["Total"].ToString();
                            }
                        }
                        devoluciones = float.Parse(devolucion);
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

    }
}
