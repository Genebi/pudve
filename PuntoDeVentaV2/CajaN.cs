﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class CajaN : Form
    {

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        public static bool recargarDatos = false;
        public static bool botones = false;

        // Pasar Variables de Total en Caja
        public static float efectivo { get; set; }
        public static float tarjeta { get; set; }
        public static float vales { get; set; }
        public static float cheque { get; set; }
        public static float trans { get; set; }

        //Variables para el corte de caja
        public static string efectivoCorte { get; set; }
        public static string tarjetaCorte { get; set; }
        public static string valesCorte { get; set; }
        public static string chequeCorte { get; set; }
        public static string transCorte { get; set; }
        public static string totCorte { get; set; }
        public static string date { get; set; }

        //float anticiposAplicados = 0f;
        float abonos = 0f;
        float devoluciones = 0f;

        //Validar si se mostrara abonos o devoluciones
        public static string abonos_devoluciones { get; set; }

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

        public static DateTime fechaGeneral;
        public static DateTime fechaUltimoCorte;// = Convert.ToDateTime("2019-10-10 12:00:35");

        public static float saldoInicial = 0f;
        private string[] cantidadesReporte;

        // Permisos de los botones
        int opcion1 = 1; // Boton agregar dinero 
        int opcion2 = 1; // Boton historial dinero agregado
        int opcion3 = 1; // Boton retirar dinero
        int opcion4 = 1; // Boton historial dinero retirado
        int opcion5 = 1; // Boton abrir caja
        int opcion6 = 1; // Boton corte caja
        int opcion7 = 1; // Mostrar saldo inicial
        int opcion8 = 1; // Mostrar panel ventas
        int opcion9 = 1; // Mostrar panel anticipos
        int opcion10 = 1; // Mostrar panel dinero agregado
        int opcion11 = 1; // Mostrar panel total caja

        public CajaN()
        {
            InitializeComponent();

        }

        private void CajaN_Load(object sender, EventArgs e)
        {
            // Obtener saldo inicial
            CargarSaldoInicial();

            if (FormPrincipal.id_empleado > 0)
            {
                var datos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Caja");

                opcion1 = datos[0];
                opcion2 = datos[1];
                opcion3 = datos[2];
                opcion4 = datos[3];
                opcion5 = datos[4];
                opcion6 = datos[5];
                opcion7 = datos[6];
                opcion8 = datos[7];
                opcion9 = datos[8];
                opcion10 = datos[9];
                opcion11 = datos[10];
            }

            tituloSeccion.Visible = Convert.ToBoolean(opcion7);
            panelVentas.Visible = Convert.ToBoolean(opcion8);
            panelAnticipos.Visible = Convert.ToBoolean(opcion9);
            panelDineroAgregado.Visible = Convert.ToBoolean(opcion10);
            panelTotales.Visible = Convert.ToBoolean(opcion11);

            // verificarCantidadAbonos();
        }

        private void CargarSaldoInicial()
        {
            saldoInicial = mb.SaldoInicialCaja(FormPrincipal.userID);
            tituloSeccion.Text = "SALDO INICIAL: $" + saldoInicial.ToString("0.00");
        }

        private void btnReporteAgregar_Click(object sender, EventArgs e)
        {
            if (opcion2 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<ReporteDineroAgregado>().Count() == 1)
            {
                Application.OpenForms.OfType<ReporteDineroAgregado>().First().BringToFront();
            }
            else
            {
                var agregado = new ReporteDineroAgregado(fechaGeneral);

                agregado.Show();
            }
        }

        private void btnReporteRetirar_Click(object sender, EventArgs e)
        {
            if (opcion4 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<ReporteDineroRetirado>().Count() == 1)
            {
                Application.OpenForms.OfType<ReporteDineroRetirado>().First().BringToFront();
            }
            else
            {
                var retirado = new ReporteDineroRetirado(fechaGeneral);

                retirado.Show();
            }
        }

        private void btnAgregarDinero_Click(object sender, EventArgs e)
        {
            if (opcion1 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<AgregarRetirarDinero>().Count() == 1)
            {
                Application.OpenForms.OfType<AgregarRetirarDinero>().First().BringToFront();
            }
            else
            {
                AgregarRetirarDinero agregar = new AgregarRetirarDinero();

                agregar.FormClosed += delegate
                {
                    CargarSaldoInicial();
                    CargarSaldo();
                };

                agregar.Show();
            }
        }

        private void btnRetirarDinero_Click(object sender, EventArgs e)
        {
            if (opcion3 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<AgregarRetirarDinero>().Count() == 1)
            {
                Application.OpenForms.OfType<AgregarRetirarDinero>().First().BringToFront();
            }
            else
            {
                AgregarRetirarDinero retirar = new AgregarRetirarDinero(1);

                retirar.FormClosed += delegate
                {
                    CargarSaldoInicial();
                    CargarSaldo();
                };

                retirar.Show();
            }
        }

        private string correoUdiario()
        {
            var dato = string.Empty;
            var consulta = cn.CargarDatos($"SELECT Email FROM Usuarios WHERE ID = '{FormPrincipal.userID}' AND Usuario = '{FormPrincipal.userNickName}'");
            if (!consulta.Rows.Count.Equals(0))
            {
                dato = consulta.Rows[0]["Email"].ToString();
            }

            return dato;
        }

        private void btnCorteCaja_Click(object sender, EventArgs e)
        {
            var f = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            date = f;
            if (opcion6 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<AgregarRetirarDinero>().Count() == 1)
            {
                Application.OpenForms.OfType<AgregarRetirarDinero>().First().BringToFront();
            }
            else
            {
                cantidadesReporte = ObtenerCantidades();

                AgregarRetirarDinero corte = new AgregarRetirarDinero(2);

                corte.FormClosed += delegate
                {
                    if (botones == true)
                    {
                        cn.EjecutarConsulta($"UPDATE Anticipos Set AnticipoAplicado = 0 WHERE IDUsuario = '{FormPrincipal.userID}'");
                        if (Utilidades.AdobeReaderInstalado())
                        {
                            GenerarReporte();
                        }
                        else
                        {
                            Utilidades.MensajeAdobeReader();
                        }

                        botones = false;
                    }

                    CargarSaldoInicial();
                    CargarSaldo();

                    var correo = correoUdiario();
                    var correoCantidades = cargarDatosCorteCaja();
                    Utilidades.enviarCorreoCorteCaja(correo, correoCantidades);
                };

                corte.Show();

                //GenerarTicket();
            }
            abonos = 0;
        }

        private string[] cargarDatosCorteCaja()
        {
            List<string> lista = new List<string>();

            //Apartado Ventas
            lista.Add(lbTEfectivo.Text);
            lista.Add(lbTTarjeta.Text);
            lista.Add(lbTVales.Text);
            lista.Add(lbTCheque.Text);
            lista.Add(lbTTrans.Text);
            lista.Add(lbTCredito.Text);
            lista.Add(lbTCreditoC.Text);
            lista.Add(lbTAnticipos.Text);
            lista.Add(lbTVentas.Text);

            //Apartado Anticipos Recibidos
            lista.Add(lbTEfectivoA.Text);
            lista.Add(lbTTarjetaA.Text);
            lista.Add(lbTValesA.Text);
            lista.Add(lbTChequeA.Text);
            lista.Add(lbTTransA.Text);
            lista.Add(lbTAnticiposA.Text);

            //Apartado Dinero Agregado
            lista.Add(lbTEfectivoD.Text);
            lista.Add(lbTTarjetaD.Text);
            lista.Add(lbTValesD.Text);
            lista.Add(lbTChequeD.Text);
            lista.Add(lbTTransD.Text);
            lista.Add(lbTAgregado.Text);

            //Apartado Dinero Retirado
            lista.Add(lbEfectivoR.Text);
            lista.Add(lbTarjetaR.Text);
            lista.Add(lbValesR.Text);
            lista.Add(lbChequeR.Text);
            lista.Add(lbTransferenciaR.Text);
            lista.Add(lbTAnticiposC.Text);
            lista.Add(lbDevoluciones.Text);
            lista.Add(lbTRetirado.Text);

            //Apartado Total Caja
            lista.Add(lbTEfectivoC.Text);
            lista.Add(lbTTarjetaC.Text);
            lista.Add(lbTValesC.Text);
            lista.Add(lbTChequeC.Text);
            lista.Add(lbTTransC.Text);
            lista.Add(lbTSaldoInicial.Text);
            lista.Add(lbTCreditoTotal.Text);
            lista.Add(lbTTotalCaja.Text);


            return lista.ToArray();
        }

        private string[] ObtenerCantidades()
        {
            List<string> lista = new List<string>();

            lista.Add(lbTEfectivo.Text);
            lista.Add(lbTEfectivoA.Text);
            lista.Add(lbTEfectivoD.Text);
            lista.Add(lbTEfectivoC.Text);

            lista.Add(lbTTarjeta.Text);
            lista.Add(lbTTarjetaA.Text);
            lista.Add(lbTTarjetaD.Text);
            lista.Add(lbTTarjetaC.Text);

            lista.Add(lbTVales.Text);
            lista.Add(lbTValesA.Text);
            lista.Add(lbTValesD.Text);
            lista.Add(lbTValesC.Text);

            lista.Add(lbTCheque.Text);
            lista.Add(lbTChequeA.Text);
            lista.Add(lbTChequeD.Text);
            lista.Add(lbTChequeC.Text);

            lista.Add(lbTTrans.Text);
            lista.Add(lbTTransA.Text);
            lista.Add(lbTTransD.Text);
            lista.Add(lbTTransC.Text);

            lista.Add(lbTCredito.Text);
            lista.Add(lbTCreditoC.Text);

            lista.Add(lbTAnticipos.Text);
            lista.Add(lbTAnticiposC.Text);

            lista.Add(lbTSubtotal.Text);
            lista.Add(lbTDineroRetirado.Text);

            lista.Add(lbTVentas.Text);
            lista.Add(lbTAnticiposA.Text);
            lista.Add(lbTAgregado.Text);
            lista.Add(lbTTotalCaja.Text);

            lista.Add(lbTSaldoInicial.Text);

            return lista.ToArray();
        }

        #region Metodo para cargar saldos y totales
        private void CargarSaldo()
        {
            this.Focus();
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
                if (!segundaConsulta.Rows.Count.Equals(0))
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
                        if (!fechaMovimientos.Rows.Count.Equals(0))
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
                        if (!obtenerDevoluciones.Rows.Count.Equals(0))
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

            var credi = (vCredito - retiroCredito);
            if (credi < 0) { credi = 0; }
            // Apartado VENTAS
            lbTEfectivo.Text = "$" + vEfectivo.ToString("0.00");
            lbTTarjeta.Text = "$" + vTarjeta.ToString("0.00");
            lbTVales.Text = "$" + vVales.ToString("0.00");
            lbTCheque.Text = "$" + vCheque.ToString("0.00");
            lbTTrans.Text = "$" + vTrans.ToString("0.00");
            lbTCredito.Text = "$" + credi.ToString("0.00");
            //lbTAnticipos.Text = "$" + vAnticipos.ToString("0.00");
            lbTAnticipos.Text = "$" + anticiposAplicados.ToString("0.00");
            lbTVentas.Text = "$" + (vEfectivo + vTarjeta + vVales + vCheque + vTrans + (credi) + /*vAnticipos*/anticiposAplicados).ToString("0.00");

            ////Variables de Abonos en Ventas
            //lbEfectivoAbonos.Text = "$" + abonoEfectivoI.ToString("0.00");
            //lbTarjetaAbonos.Text = "$" + abonoTarjetaI.ToString("0.00");
            //lbValesAbonos.Text = "$" + abonoValesI.ToString("0.00");
            //lbChequeAbonos.Text = "$" + abonoChequeI.ToString("0.00");
            //lbTransferenciaAbonos.Text = "$" + abonoTransferenciaI.ToString("0.00");
            lbTCreditoC.Text = "$" + abonos/*(abonoEfectivoI + abonoTarjetaI + abonoValesI + abonoChequeI + abonoTransferenciaI)*/.ToString("0.00");

            //lbTotalAbonos.Text = "$" + abonoEfectivoI.ToString("0.00");

            // Apartado ANTICIPOS RECIBIDOS
            lbTEfectivoA.Text = "$" + aEfectivo.ToString("0.00");
            lbTTarjetaA.Text = "$" + aTarjeta.ToString("0.00");
            lbTValesA.Text = "$" + aVales.ToString("0.00");
            lbTChequeA.Text = "$" + aCheque.ToString("0.00");
            lbTTransA.Text = "$" + aTrans.ToString("0.00");
            lbTAnticiposA.Text = "$" + (aEfectivo + aTarjeta + aVales + aCheque + aTrans).ToString("0.00");

            // Apartado DINERO AGREGADO
            lbTEfectivoD.Text = "$" + dEfectivo.ToString("0.00");
            lbTTarjetaD.Text = "$" + dTarjeta.ToString("0.00");
            lbTValesD.Text = "$" + dVales.ToString("0.00");
            lbTChequeD.Text = "$" + dCheque.ToString("0.00");
            lbTTransD.Text = "$" + dTrans.ToString("0.00");
            lbTAgregado.Text = "$" + (dEfectivo + dTarjeta + dVales + dCheque + dTrans).ToString("0.00");

            // Apartado Dinero Retirado
            lbEfectivoR.Text = "$ -" + retiroEfectivo.ToString("0.00");
            lbTarjetaR.Text = "$ -" + retiroTarjeta.ToString("0.00");
            lbValesR.Text = "$ -" + retiroVales.ToString("0.00");
            lbChequeR.Text = "$ -" + retiroCheque.ToString("0.00");
            lbTransferenciaR.Text = "$ -" + retiroTrans.ToString("0.00");
            //lbTAnticiposC.Text = "$ -" + vAnticipos.ToString("0.00");
            lbTAnticiposC.Text = "$ -" + anticiposAplicados.ToString("0.00");
            lbDevoluciones.Text = "$-" + devoluciones.ToString("0.00");
            lbTRetirado.Text = "$ -" + (retiroEfectivo + retiroTarjeta + retiroVales + retiroCheque + retiroTrans + /*vAnticipos*/anticiposAplicados + devoluciones).ToString("0.00");

            // Apartado TOTAL EN CAJA
            efectivo = (vEfectivo + aEfectivo + dEfectivo + abonoEfectivoI) - rEfectivo; if (efectivo < 0) { efectivo = 0; }
            tarjeta = (vTarjeta + aTarjeta + dTarjeta + abonoTarjetaI) - rTarjeta; if (tarjeta < 0) { tarjeta = 0; }
            vales = (vVales + aVales + dVales + abonoValesI) - rVales; if (vales < 0) { vales = 0; }
            cheque = (vCheque + aCheque + dCheque + abonoChequeI) - rCheque; if (cheque < 0) { cheque = 0; }
            trans = (vTrans + aTrans + dTrans + abonoTransferenciaI) - rTransferencia; if (trans < 0) { trans = 0; }
            credito = vCredito;
            //anticipos = vAnticipos;
            anticipos = anticiposAplicados;
            subtotal = (efectivo + tarjeta + vales + cheque + trans /*+ credito*//*+ abonos*/ + saldoInicial /*+ vCredito*/)/* - devoluciones*/; if (subtotal < 0) { subtotal = 0; }

            var totalF = (efectivo - retiroEfectivo); if (totalF < 0) { totalF = 0; }
            var totalTa = (tarjeta - retiroTarjeta); if (totalTa < 0) { totalTa = 0; }
            var totalV = (vales - retiroVales); if (totalV < 0) { totalV = 0; }
            var totalC = (cheque - retiroCheque); if (totalC < 0) { totalC = 0; }
            var totalTr = (trans - retiroTrans); if (totalTr < 0) { totalTr = 0; }

            lbTEfectivoC.Text = "$" + (totalF).ToString("0.00");
            lbTTarjetaC.Text = "$" + (totalTa).ToString("0.00");
            lbTValesC.Text = "$" + (totalV).ToString("0.00");
            lbTChequeC.Text = "$" + (totalC).ToString("0.00");
            lbTTransC.Text = "$" + (totalTr).ToString("0.00");
            //lbTCreditoC.Text = "$" + /*credito*/abonos.ToString("0.00");   // lbTCreditoC Esta etiqueta es la de Abonos---------------------------------
            //lbTAnticiposC.Text = "$" + anticipos.ToString("0.00"); 
            lbTSaldoInicial.Text = "$" + saldoInicial.ToString("0.00");
            if (credito < retiroCredito) { lbTCreditoTotal.Text = "$" + "0.00"; } else { lbTCreditoTotal.Text = "$" + (vCredito - retiroCredito).ToString("0.00"); }
            //lbTSubtotal.Text = "$" + subtotal.ToString("0.00");
            //lbTDineroRetirado.Text = "$" + dineroRetirado.ToString("0.00");
            lbTTotalCaja.Text = "$" + (subtotal - (dineroRetirado + devoluciones)).ToString("0.00");

            // Variables de clase
            totalEfectivo = efectivo - retiroEfectivo;
            totalTarjeta = tarjeta - retiroTarjeta;
            totalVales = vales - retiroVales;
            totalCheque = cheque - retiroCheque;
            totalTransferencia = trans - retiroTrans;
            totalCredito = credito - retiroCredito;

            verificarCantidadAbonos();
        }
        #endregion

        private void CajaN_Paint(object sender, PaintEventArgs e)
        {
            if (recargarDatos)
            {
                CargarSaldoInicial();
                CargarSaldo();
                recargarDatos = false;
            }
        }

        private void CajaN_Resize(object sender, EventArgs e)
        {
            recargarDatos = false;
        }

        private void switcharVentasAbonos(int ventasAbonos)
        {
            ////ventasAbonos = 0 = informacion Ventas  
            ////ventasAbonos = 1 = informacion Abonos
            //if (ventasAbonos == 0)
            //{
            //    //Deshabilitar Abonos
            //    tituloAbonos.Visible = false;
            //    lbEfectivoAbonos.Visible = false;
            //    lbTarjetaAbonos.Visible = false;
            //    lbValesAbonos.Visible = false;
            //    lbChequeAbonos.Visible = false;
            //    lbTransferenciaAbonos.Visible = false;
            //    lbCreditoC.Visible = false;
            //    lbTCreditoC.Visible = false;

            //    //Habilitar Ventas
            //    tituloVentas.Visible = true;
            //    lbTEfectivo.Visible = true;
            //    lbTTarjeta.Visible = true;
            //    lbTVales.Visible = true;
            //    lbTCheque.Visible = true;
            //    lbTTrans.Visible = true;
            //    lbCredito.Visible = true;
            //    lbTCredito.Visible = true;
            //    lbAnticipos.Visible = true;
            //    lbTAnticipos.Visible = true;
            //    lbVentas.Visible = true;
            //    lbTVentas.Visible = true;
            //}else if (ventasAbonos == 1)
            //{
            //    //Deshabilitar Ventas
            //    tituloVentas.Visible = false;
            //    lbTEfectivo.Visible = false;
            //    lbTTarjeta.Visible = false;
            //    lbTVales.Visible = false;
            //    lbTCheque.Visible = false;
            //    lbTTrans.Visible = false;
            //    lbCredito.Visible = false;
            //    lbTCredito.Visible = false;
            //    lbAnticipos.Visible = false;
            //    lbTAnticipos.Visible = false;
            //    lbVentas.Visible = false;
            //    lbTVentas.Visible = false;

            //    //Habilitar Abonos
            //    tituloAbonos.Visible = true;
            //    lbEfectivoAbonos.Visible = true;
            //    lbTarjetaAbonos.Visible = true;
            //    lbValesAbonos.Visible = true;
            //    lbChequeAbonos.Visible = true;
            //    lbTransferenciaAbonos.Visible = true;
            //    lbCreditoC.Visible = true;
            //    lbTCreditoC.Visible = true;

            //}
        }

        private void GenerarReporte()
        {
            // Datos del usuario
            var datos = FormPrincipal.datosUsuario;

            // Fuentes y Colores
            var colorFuenteNegrita = new BaseColor(Color.Black);
            var colorFuenteBlanca = new BaseColor(Color.White);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, colorFuenteBlanca);

            // Ruta donde se creara el archivo PDF
            var rutaArchivo = string.Empty;
            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\Caja\reporte_corte_" + fechaUltimoCorte.ToString("yyyyMMddHHmmss") + ".pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\Caja\reporte_corte_" + fechaUltimoCorte.ToString("yyyyMMddHHmmss") + ".pdf";
            }


            Document reporte = new Document(PageSize.A3);
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
            Paragraph subTitulo = new Paragraph("CORTE DE CAJA\nFecha: " + fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;

            //===========================================
            //===       TABLAS DE CORTE DE CAJA       ===
            //===========================================
            #region Tabla de Venta

            // Cantidades de las columnas
            var cantidades = cantidadesReporte;

            float[] anchoColumnas = new float[] { 120f, 80f, 100f, 100f, 100f, 100f, 120f, 80f };

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            // Encabezados
            PdfPTable tabla = new PdfPTable(8);
            tabla.WidthPercentage = 100;
            tabla.SetWidths(anchoColumnas);

            PdfPCell colVentas = new PdfPCell(new Phrase("VENTAS", fuenteNegrita));
            colVentas.Colspan = 2;
            colVentas.BorderWidth = 0;
            colVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colVentas.Padding = 3;

            PdfPCell colAnticipos = new PdfPCell(new Phrase("ANTICIPOS RECIBIDOS", fuenteNegrita));
            colAnticipos.Colspan = 2;
            colAnticipos.BorderWidth = 0;
            colAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticipos.Padding = 3;

            PdfPCell colDinero = new PdfPCell(new Phrase("DINERO AGREGADO", fuenteNegrita));
            colDinero.Colspan = 2;
            colDinero.BorderWidth = 0;
            colDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colDinero.Padding = 3;

            PdfPCell colTotal = new PdfPCell(new Phrase("TOTAL EN CAJA", fuenteNegrita));
            colTotal.Colspan = 2;
            colTotal.BorderWidth = 0;
            colTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotal.Padding = 3;

            tabla.AddCell(colVentas);
            tabla.AddCell(colAnticipos);
            tabla.AddCell(colDinero);
            tabla.AddCell(colTotal);

            // Linea de EFECTIVO
            PdfPCell colEfectivoVentas = new PdfPCell(new Phrase($"Efectivo", fuenteNormal));
            colEfectivoVentas.BorderWidth = 0;
            colEfectivoVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoVentas.Padding = 3;

            PdfPCell colEfectivoVentasC = new PdfPCell(new Phrase($"{cantidades[0]}", fuenteNormal));
            colEfectivoVentasC.BorderWidth = 0;
            colEfectivoVentasC.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoVentasC.Padding = 3;

            PdfPCell colEfectivoAnticipos = new PdfPCell(new Phrase($"Efectivo", fuenteNormal));
            colEfectivoAnticipos.BorderWidth = 0;
            colEfectivoAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoAnticipos.Padding = 3;

            PdfPCell colEfectivoAnticiposC = new PdfPCell(new Phrase($"{cantidades[1]}", fuenteNormal));
            colEfectivoAnticiposC.BorderWidth = 0;
            colEfectivoAnticiposC.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoAnticiposC.Padding = 3;

            PdfPCell colEfectivoDinero = new PdfPCell(new Phrase($"Efectivo", fuenteNormal));
            colEfectivoDinero.BorderWidth = 0;
            colEfectivoDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoDinero.Padding = 3;

            PdfPCell colEfectivoDineroC = new PdfPCell(new Phrase($"{cantidades[2]}", fuenteNormal));
            colEfectivoDineroC.BorderWidth = 0;
            colEfectivoDineroC.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoDineroC.Padding = 3;

            PdfPCell colEfectivoTotal = new PdfPCell(new Phrase($"Efectivo", fuenteNormal));
            colEfectivoTotal.BorderWidth = 0;
            colEfectivoTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoTotal.Padding = 3;

            PdfPCell colEfectivoTotalC = new PdfPCell(new Phrase($"{cantidades[3]}", fuenteNormal));
            colEfectivoTotalC.BorderWidth = 0;
            colEfectivoTotalC.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoTotalC.Padding = 3;

            tabla.AddCell(colEfectivoVentas);
            tabla.AddCell(colEfectivoVentasC);
            tabla.AddCell(colEfectivoAnticipos);
            tabla.AddCell(colEfectivoAnticiposC);
            tabla.AddCell(colEfectivoDinero);
            tabla.AddCell(colEfectivoDineroC);
            tabla.AddCell(colEfectivoTotal);
            tabla.AddCell(colEfectivoTotalC);


            // Linea de TARJETA
            PdfPCell colTarjetaVentas = new PdfPCell(new Phrase($"Tarjeta", fuenteNormal));
            colTarjetaVentas.BorderWidth = 0;
            colTarjetaVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaVentas.Padding = 3;

            PdfPCell colTarjetaVentasC = new PdfPCell(new Phrase($"{cantidades[4]}", fuenteNormal));
            colTarjetaVentasC.BorderWidth = 0;
            colTarjetaVentasC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaVentasC.Padding = 3;

            PdfPCell colTarjetaAnticipos = new PdfPCell(new Phrase($"Tarjeta", fuenteNormal));
            colTarjetaAnticipos.BorderWidth = 0;
            colTarjetaAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaAnticipos.Padding = 3;

            PdfPCell colTarjetaAnticiposC = new PdfPCell(new Phrase($"{cantidades[5]}", fuenteNormal));
            colTarjetaAnticiposC.BorderWidth = 0;
            colTarjetaAnticiposC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaAnticiposC.Padding = 3;

            PdfPCell colTarjetaDinero = new PdfPCell(new Phrase($"Tarjeta", fuenteNormal));
            colTarjetaDinero.BorderWidth = 0;
            colTarjetaDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaDinero.Padding = 3;

            PdfPCell colTarjetaDineroC = new PdfPCell(new Phrase($"{cantidades[6]}", fuenteNormal));
            colTarjetaDineroC.BorderWidth = 0;
            colTarjetaDineroC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaDineroC.Padding = 3;

            PdfPCell colTarjetaTotal = new PdfPCell(new Phrase($"Tarjeta", fuenteNormal));
            colTarjetaTotal.BorderWidth = 0;
            colTarjetaTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaTotal.Padding = 3;

            PdfPCell colTarjetaTotalC = new PdfPCell(new Phrase($"{cantidades[7]}", fuenteNormal));
            colTarjetaTotalC.BorderWidth = 0;
            colTarjetaTotalC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaTotalC.Padding = 3;

            tabla.AddCell(colTarjetaVentas);
            tabla.AddCell(colTarjetaVentasC);
            tabla.AddCell(colTarjetaAnticipos);
            tabla.AddCell(colTarjetaAnticiposC);
            tabla.AddCell(colTarjetaDinero);
            tabla.AddCell(colTarjetaDineroC);
            tabla.AddCell(colTarjetaTotal);
            tabla.AddCell(colTarjetaTotalC);

            // Linea de VALES
            PdfPCell colValesVentas = new PdfPCell(new Phrase($"Vales", fuenteNormal));
            colValesVentas.BorderWidth = 0;
            colValesVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesVentas.Padding = 3;

            PdfPCell colValesVentasC = new PdfPCell(new Phrase($"{cantidades[8]}", fuenteNormal));
            colValesVentasC.BorderWidth = 0;
            colValesVentasC.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesVentasC.Padding = 3;

            PdfPCell colValesAnticipos = new PdfPCell(new Phrase($"Vales", fuenteNormal));
            colValesAnticipos.BorderWidth = 0;
            colValesAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesAnticipos.Padding = 3;

            PdfPCell colValesAnticiposC = new PdfPCell(new Phrase($"{cantidades[9]}", fuenteNormal));
            colValesAnticiposC.BorderWidth = 0;
            colValesAnticiposC.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesAnticiposC.Padding = 3;

            PdfPCell colValesDinero = new PdfPCell(new Phrase($"Vales", fuenteNormal));
            colValesDinero.BorderWidth = 0;
            colValesDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesDinero.Padding = 3;

            PdfPCell colValesDineroC = new PdfPCell(new Phrase($"{cantidades[10]}", fuenteNormal));
            colValesDineroC.BorderWidth = 0;
            colValesDineroC.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesDineroC.Padding = 3;

            PdfPCell colValesTotal = new PdfPCell(new Phrase($"Vales", fuenteNormal));
            colValesTotal.BorderWidth = 0;
            colValesTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesTotal.Padding = 3;

            PdfPCell colValesTotalC = new PdfPCell(new Phrase($"{cantidades[11]}", fuenteNormal));
            colValesTotalC.BorderWidth = 0;
            colValesTotalC.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesTotalC.Padding = 3;

            tabla.AddCell(colValesVentas);
            tabla.AddCell(colValesVentasC);
            tabla.AddCell(colValesAnticipos);
            tabla.AddCell(colValesAnticiposC);
            tabla.AddCell(colValesDinero);
            tabla.AddCell(colValesDineroC);
            tabla.AddCell(colValesTotal);
            tabla.AddCell(colValesTotalC);


            // Linea de CHEQUE
            PdfPCell colChequeVentas = new PdfPCell(new Phrase($"Cheque", fuenteNormal));
            colChequeVentas.BorderWidth = 0;
            colChequeVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeVentas.Padding = 3;

            PdfPCell colChequeVentasC = new PdfPCell(new Phrase($"{cantidades[12]}", fuenteNormal));
            colChequeVentasC.BorderWidth = 0;
            colChequeVentasC.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeVentasC.Padding = 3;

            PdfPCell colChequeAnticipos = new PdfPCell(new Phrase($"Cheque", fuenteNormal));
            colChequeAnticipos.BorderWidth = 0;
            colChequeAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeAnticipos.Padding = 3;

            PdfPCell colChequeAnticiposC = new PdfPCell(new Phrase($"{cantidades[13]}", fuenteNormal));
            colChequeAnticiposC.BorderWidth = 0;
            colChequeAnticiposC.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeAnticiposC.Padding = 3;

            PdfPCell colChequeDinero = new PdfPCell(new Phrase($"Cheque", fuenteNormal));
            colChequeDinero.BorderWidth = 0;
            colChequeDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeDinero.Padding = 3;

            PdfPCell colChequeDineroC = new PdfPCell(new Phrase($"{cantidades[14]}", fuenteNormal));
            colChequeDineroC.BorderWidth = 0;
            colChequeDineroC.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeDineroC.Padding = 3;

            PdfPCell colChequeTotal = new PdfPCell(new Phrase($"Cheque", fuenteNormal));
            colChequeTotal.BorderWidth = 0;
            colChequeTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeTotal.Padding = 3;

            PdfPCell colChequeTotalC = new PdfPCell(new Phrase($"{cantidades[15]}", fuenteNormal));
            colChequeTotalC.BorderWidth = 0;
            colChequeTotalC.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeTotalC.Padding = 3;

            tabla.AddCell(colChequeVentas);
            tabla.AddCell(colChequeVentasC);
            tabla.AddCell(colChequeAnticipos);
            tabla.AddCell(colChequeAnticiposC);
            tabla.AddCell(colChequeDinero);
            tabla.AddCell(colChequeDineroC);
            tabla.AddCell(colChequeTotal);
            tabla.AddCell(colChequeTotalC);


            // Linea de TRANSFERENCIA
            PdfPCell colTransVentas = new PdfPCell(new Phrase($"Transferencia", fuenteNormal));
            colTransVentas.BorderWidth = 0;
            colTransVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransVentas.Padding = 3;

            PdfPCell colTransVentasC = new PdfPCell(new Phrase($"{cantidades[16]}", fuenteNormal));
            colTransVentasC.BorderWidth = 0;
            colTransVentasC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransVentasC.Padding = 3;

            PdfPCell colTransAnticipos = new PdfPCell(new Phrase($"Transferencia", fuenteNormal));
            colTransAnticipos.BorderWidth = 0;
            colTransAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransAnticipos.Padding = 3;

            PdfPCell colTransAnticiposC = new PdfPCell(new Phrase($"{cantidades[17]}", fuenteNormal));
            colTransAnticiposC.BorderWidth = 0;
            colTransAnticiposC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransAnticiposC.Padding = 3;

            PdfPCell colTransDinero = new PdfPCell(new Phrase($"Transferencia", fuenteNormal));
            colTransDinero.BorderWidth = 0;
            colTransDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransDinero.Padding = 3;

            PdfPCell colTransDineroC = new PdfPCell(new Phrase($"{cantidades[18]}", fuenteNormal));
            colTransDineroC.BorderWidth = 0;
            colTransDineroC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransDineroC.Padding = 3;

            PdfPCell colTransTotal = new PdfPCell(new Phrase($"Transferencia", fuenteNormal));
            colTransTotal.BorderWidth = 0;
            colTransTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransTotal.Padding = 3;

            PdfPCell colTransTotalC = new PdfPCell(new Phrase($"{cantidades[19]}", fuenteNormal));
            colTransTotalC.BorderWidth = 0;
            colTransTotalC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransTotalC.Padding = 3;

            tabla.AddCell(colTransVentas);
            tabla.AddCell(colTransVentasC);
            tabla.AddCell(colTransAnticipos);
            tabla.AddCell(colTransAnticiposC);
            tabla.AddCell(colTransDinero);
            tabla.AddCell(colTransDineroC);
            tabla.AddCell(colTransTotal);
            tabla.AddCell(colTransTotalC);


            // Linea de CREDITO
            PdfPCell colCreditoVentas = new PdfPCell(new Phrase($"Crédito", fuenteNormal));
            colCreditoVentas.BorderWidth = 0;
            colCreditoVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoVentas.Padding = 3;

            PdfPCell colCreditoVentasC = new PdfPCell(new Phrase($"{cantidades[20]}", fuenteNormal));
            colCreditoVentasC.BorderWidth = 0;
            colCreditoVentasC.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoVentasC.Padding = 3;

            PdfPCell colCreditoAnticipos = new PdfPCell(new Phrase("", fuenteNormal));
            colCreditoAnticipos.BorderWidth = 0;
            colCreditoAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoAnticipos.Padding = 3;

            PdfPCell colCreditoAnticiposC = new PdfPCell(new Phrase("", fuenteNormal));
            colCreditoAnticiposC.BorderWidth = 0;
            colCreditoAnticiposC.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoAnticiposC.Padding = 3;

            PdfPCell colCreditoDinero = new PdfPCell(new Phrase("", fuenteNormal));
            colCreditoDinero.BorderWidth = 0;
            colCreditoDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoDinero.Padding = 3;

            PdfPCell colCreditoDineroC = new PdfPCell(new Phrase("", fuenteNormal));
            colCreditoDineroC.BorderWidth = 0;
            colCreditoDineroC.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoDineroC.Padding = 3;

            PdfPCell colCreditoTotal = new PdfPCell(new Phrase($"Crédito", fuenteNormal));
            colCreditoTotal.BorderWidth = 0;
            colCreditoTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoTotal.Padding = 3;

            PdfPCell colCreditoTotalC = new PdfPCell(new Phrase($"{cantidades[21]}", fuenteNormal));
            colCreditoTotalC.BorderWidth = 0;
            colCreditoTotalC.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoTotalC.Padding = 3;

            tabla.AddCell(colCreditoVentas);
            tabla.AddCell(colCreditoVentasC);
            tabla.AddCell(colCreditoAnticipos);
            tabla.AddCell(colCreditoAnticiposC);
            tabla.AddCell(colCreditoDinero);
            tabla.AddCell(colCreditoDineroC);
            tabla.AddCell(colCreditoTotal);
            tabla.AddCell(colCreditoTotalC);

            // Linea de ANTICIPOS
            PdfPCell colAnticiposVentas = new PdfPCell(new Phrase($"Anticipos utilizados al corte", fuenteNormal));
            colAnticiposVentas.BorderWidth = 0;
            colAnticiposVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposVentas.Padding = 3;

            PdfPCell colAnticiposVentasC = new PdfPCell(new Phrase($"{cantidades[22]}", fuenteNormal));
            colAnticiposVentasC.BorderWidth = 0;
            colAnticiposVentasC.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposVentasC.Padding = 3;

            PdfPCell colAnticiposUtilizados = new PdfPCell(new Phrase("", fuenteNormal));
            colAnticiposUtilizados.BorderWidth = 0;
            colAnticiposUtilizados.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposUtilizados.Padding = 3;

            PdfPCell colAnticiposUtilizadosC = new PdfPCell(new Phrase("", fuenteNormal));
            colAnticiposUtilizadosC.BorderWidth = 0;
            colAnticiposUtilizadosC.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposUtilizadosC.Padding = 3;

            PdfPCell colAnticiposDinero = new PdfPCell(new Phrase("", fuenteNormal));
            colAnticiposDinero.BorderWidth = 0;
            colAnticiposDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposDinero.Padding = 3;

            PdfPCell colAnticiposDineroC = new PdfPCell(new Phrase("", fuenteNormal));
            colAnticiposDineroC.BorderWidth = 0;
            colAnticiposDineroC.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposDineroC.Padding = 3;

            PdfPCell colAnticiposTotal = new PdfPCell(new Phrase($"Anticipos utilizados al corte", fuenteNormal));
            colAnticiposTotal.BorderWidth = 0;
            colAnticiposTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposTotal.Padding = 3;

            PdfPCell colAnticiposTotalC = new PdfPCell(new Phrase($"{cantidades[23]}", fuenteNormal));
            colAnticiposTotalC.BorderWidth = 0;
            colAnticiposTotalC.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposTotalC.Padding = 3;

            tabla.AddCell(colAnticiposVentas);
            tabla.AddCell(colAnticiposVentasC);
            tabla.AddCell(colAnticiposUtilizados);
            tabla.AddCell(colAnticiposUtilizadosC);
            tabla.AddCell(colAnticiposDinero);
            tabla.AddCell(colAnticiposDineroC);
            tabla.AddCell(colAnticiposTotal);
            tabla.AddCell(colAnticiposTotalC);

            // Linea de SALDO INICIAL
            PdfPCell colSaldoInicial_I = new PdfPCell(new Phrase("", fuenteNormal));
            colSaldoInicial_I.Colspan = 2;
            colSaldoInicial_I.BorderWidth = 0;
            colSaldoInicial_I.HorizontalAlignment = Element.ALIGN_CENTER;
            colSaldoInicial_I.Padding = 3;

            PdfPCell colSaldoInicial_II = new PdfPCell(new Phrase("", fuenteNormal));
            colSaldoInicial_II.Colspan = 2;
            colSaldoInicial_II.BorderWidth = 0;
            colSaldoInicial_II.HorizontalAlignment = Element.ALIGN_CENTER;
            colSaldoInicial_II.Padding = 3;

            PdfPCell colSaldoInicial_III = new PdfPCell(new Phrase("", fuenteNormal));
            colSaldoInicial_III.Colspan = 2;
            colSaldoInicial_III.BorderWidth = 0;
            colSaldoInicial_III.HorizontalAlignment = Element.ALIGN_CENTER;
            colSaldoInicial_III.Padding = 3;

            PdfPCell colSaldoInicial_IV = new PdfPCell(new Phrase($"Saldo inicial", fuenteNormal));
            colSaldoInicial_IV.BorderWidth = 0;
            colSaldoInicial_IV.HorizontalAlignment = Element.ALIGN_CENTER;
            colSaldoInicial_IV.Padding = 3;

            PdfPCell colSaldoInicialC_IV = new PdfPCell(new Phrase($"{cantidades[30]}", fuenteNormal));
            colSaldoInicialC_IV.BorderWidth = 0;
            colSaldoInicialC_IV.HorizontalAlignment = Element.ALIGN_CENTER;
            colSaldoInicialC_IV.Padding = 3;

            tabla.AddCell(colSaldoInicial_I);
            tabla.AddCell(colSaldoInicial_II);
            tabla.AddCell(colSaldoInicial_III);
            tabla.AddCell(colSaldoInicial_IV);
            tabla.AddCell(colSaldoInicialC_IV);

            // Linea de SUBTOTAL
            PdfPCell colSubVentas = new PdfPCell(new Phrase("", fuenteNormal));
            colSubVentas.Colspan = 2;
            colSubVentas.BorderWidth = 0;
            colSubVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colSubVentas.Padding = 3;

            PdfPCell colSubAnticipos = new PdfPCell(new Phrase("", fuenteNormal));
            colSubAnticipos.Colspan = 2;
            colSubAnticipos.BorderWidth = 0;
            colSubAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colSubAnticipos.Padding = 3;

            PdfPCell colSubDinero = new PdfPCell(new Phrase("", fuenteNormal));
            colSubDinero.Colspan = 2;
            colSubDinero.BorderWidth = 0;
            colSubDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colSubDinero.Padding = 3;

            PdfPCell colSubTotal = new PdfPCell(new Phrase($"Subtotal en caja", fuenteNormal));
            colSubTotal.BorderWidth = 0;
            colSubTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colSubTotal.Padding = 3;

            PdfPCell colSubTotalC = new PdfPCell(new Phrase($"{cantidades[24]}", fuenteNormal));
            colSubTotalC.BorderWidth = 0;
            colSubTotalC.HorizontalAlignment = Element.ALIGN_CENTER;
            colSubTotalC.Padding = 3;

            tabla.AddCell(colSubVentas);
            tabla.AddCell(colSubAnticipos);
            tabla.AddCell(colSubDinero);
            tabla.AddCell(colSubTotal);
            tabla.AddCell(colSubTotalC);

            // Linea de RETIRADO
            PdfPCell colRetiradoVentas = new PdfPCell(new Phrase("", fuenteNormal));
            colRetiradoVentas.Colspan = 2;
            colRetiradoVentas.BorderWidth = 0;
            colRetiradoVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetiradoVentas.Padding = 3;

            PdfPCell colRetiradoAnticipos = new PdfPCell(new Phrase("", fuenteNormal));
            colRetiradoAnticipos.Colspan = 2;
            colRetiradoAnticipos.BorderWidth = 0;
            colRetiradoAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetiradoAnticipos.Padding = 3;

            PdfPCell colRetiradoDinero = new PdfPCell(new Phrase("", fuenteNormal));
            colRetiradoDinero.Colspan = 2;
            colRetiradoDinero.BorderWidth = 0;
            colRetiradoDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetiradoDinero.Padding = 3;

            PdfPCell colRetiradoTotal = new PdfPCell(new Phrase($"Dinero retirado", fuenteNormal));
            colRetiradoTotal.BorderWidth = 0;
            colRetiradoTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetiradoTotal.Padding = 3;

            PdfPCell colRetiradoTotalC = new PdfPCell(new Phrase($"{cantidades[25]}", fuenteNormal));
            colRetiradoTotalC.BorderWidth = 0;
            colRetiradoTotalC.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetiradoTotalC.Padding = 3;

            tabla.AddCell(colRetiradoVentas);
            tabla.AddCell(colRetiradoAnticipos);
            tabla.AddCell(colRetiradoDinero);
            tabla.AddCell(colRetiradoTotal);
            tabla.AddCell(colRetiradoTotalC);

            PdfPCell colSeparador = new PdfPCell(new Phrase(Chunk.NEWLINE));
            colSeparador.Colspan = 10;
            colSeparador.BorderWidth = 0;

            tabla.AddCell(colSeparador);

            // Linea de TOTALES
            PdfPCell colTotalVentas = new PdfPCell(new Phrase($"Total de Ventas", fuenteTotales));
            colTotalVentas.BorderWidth = 0;
            colTotalVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalVentas.Padding = 3;
            colTotalVentas.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colTotalVentasC = new PdfPCell(new Phrase($"{cantidades[26]}", fuenteTotales));
            colTotalVentasC.BorderWidth = 0;
            colTotalVentasC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalVentasC.Padding = 3;
            colTotalVentasC.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colTotalAnticipos = new PdfPCell(new Phrase($"Total Anticipos", fuenteTotales));
            colTotalAnticipos.BorderWidth = 0;
            colTotalAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalAnticipos.Padding = 3;
            colTotalAnticipos.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colTotalAnticiposC = new PdfPCell(new Phrase($"{cantidades[27]}", fuenteTotales));
            colTotalAnticiposC.BorderWidth = 0;
            colTotalAnticiposC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalAnticiposC.Padding = 3;
            colTotalAnticiposC.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colTotalDinero = new PdfPCell(new Phrase($"Total Agregado", fuenteTotales));
            colTotalDinero.BorderWidth = 0;
            colTotalDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalDinero.Padding = 3;
            colTotalDinero.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colTotalDineroC = new PdfPCell(new Phrase($"{cantidades[28]}", fuenteTotales));
            colTotalDineroC.BorderWidth = 0;
            colTotalDineroC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalDineroC.Padding = 3;
            colTotalDineroC.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colTotalFinal = new PdfPCell(new Phrase($"Total en Caja", fuenteTotales));
            colTotalFinal.BorderWidth = 0;
            colTotalFinal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalFinal.Padding = 3;
            colTotalFinal.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colTotalFinalC = new PdfPCell(new Phrase($"{cantidades[29]}", fuenteTotales));
            colTotalFinalC.BorderWidth = 0;
            colTotalFinalC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalFinalC.Padding = 3;
            colTotalFinalC.BackgroundColor = new BaseColor(Color.Red);

            tabla.AddCell(colTotalVentas);
            tabla.AddCell(colTotalVentasC);
            tabla.AddCell(colTotalAnticipos);
            tabla.AddCell(colTotalAnticiposC);
            tabla.AddCell(colTotalDinero);
            tabla.AddCell(colTotalDineroC);
            tabla.AddCell(colTotalFinal);
            tabla.AddCell(colTotalFinalC);

            #endregion
            //===========================================
            //===    FIN  TABLAS DE CORTE DE CAJA     ===
            //===========================================

            reporte.Add(titulo);
            reporte.Add(subTitulo);
            reporte.Add(tabla);
            reporte.Add(linea);

            //===============================
            //===    TABLA DE DEPOSITOS   ===
            //===============================
            #region Tabla de Depositos
            anchoColumnas = new float[] { 100f, 100f, 100f, 100f, 100f, 100f, 100f };

            Paragraph tituloDepositos = new Paragraph("HISTORIAL DE DEPOSITOS\n\n", fuenteGrande);
            tituloDepositos.Alignment = Element.ALIGN_CENTER;

            MySqlConnection sql_con;
            MySqlCommand sql_cmd;
            MySqlDataReader dr;

            sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            sql_con.Open();
            sql_cmd = new MySqlCommand($"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'deposito' AND FechaOperacion > '{fechaGeneral.ToString("yyyy-MM-dd HH:mm:ss")}'", sql_con);
            dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                PdfPTable tablaDepositos = new PdfPTable(7);
                tablaDepositos.WidthPercentage = 100;
                tablaDepositos.SetWidths(anchoColumnas);

                PdfPCell colEmpleado = new PdfPCell(new Phrase("EMPLEADO", fuenteNegrita));
                colEmpleado.BorderWidth = 0;
                colEmpleado.HorizontalAlignment = Element.ALIGN_CENTER;
                colEmpleado.Padding = 3;

                PdfPCell colDepositoEfectivo = new PdfPCell(new Phrase("EFECTIVO", fuenteNegrita));
                colDepositoEfectivo.BorderWidth = 0;
                colDepositoEfectivo.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoEfectivo.Padding = 3;

                PdfPCell colDepositoTarjeta = new PdfPCell(new Phrase("TARJETA", fuenteNegrita));
                colDepositoTarjeta.BorderWidth = 0;
                colDepositoTarjeta.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoTarjeta.Padding = 3;

                PdfPCell colDepositoVales = new PdfPCell(new Phrase("VALES", fuenteNegrita));
                colDepositoVales.BorderWidth = 0;
                colDepositoVales.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoVales.Padding = 3;

                PdfPCell colDepositoCheque = new PdfPCell(new Phrase("CHEQUE", fuenteNegrita));
                colDepositoCheque.BorderWidth = 0;
                colDepositoCheque.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoCheque.Padding = 3;

                PdfPCell colDepositoTrans = new PdfPCell(new Phrase("TRANSFERENCIA", fuenteNegrita));
                colDepositoTrans.BorderWidth = 0;
                colDepositoTrans.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoTrans.Padding = 3;

                PdfPCell colDepositoFecha = new PdfPCell(new Phrase("FECHA", fuenteNegrita));
                colDepositoFecha.BorderWidth = 0;
                colDepositoFecha.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoFecha.Padding = 3;

                tablaDepositos.AddCell(colEmpleado);
                tablaDepositos.AddCell(colDepositoEfectivo);
                tablaDepositos.AddCell(colDepositoTarjeta);
                tablaDepositos.AddCell(colDepositoVales);
                tablaDepositos.AddCell(colDepositoCheque);
                tablaDepositos.AddCell(colDepositoTrans);
                tablaDepositos.AddCell(colDepositoFecha);

                while (dr.Read())
                {
                    var efectivo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Efectivo"))).ToString("0.00");
                    var tarjeta = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Tarjeta"))).ToString("0.00");
                    var vales = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vales"))).ToString("0.00");
                    var cheque = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cheque"))).ToString("0.00");
                    var trans = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Transferencia"))).ToString("0.00");
                    var fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");

                    PdfPCell colEmpleadoTmp = new PdfPCell(new Phrase("ADMIN", fuenteNormal));
                    colEmpleadoTmp.BorderWidth = 0;
                    colEmpleadoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoEfectivoTmp = new PdfPCell(new Phrase("$" + efectivo, fuenteNormal));
                    colDepositoEfectivoTmp.BorderWidth = 0;
                    colDepositoEfectivoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoTarjetaTmp = new PdfPCell(new Phrase("$" + tarjeta, fuenteNormal));
                    colDepositoTarjetaTmp.BorderWidth = 0;
                    colDepositoTarjetaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoValesTmp = new PdfPCell(new Phrase("$" + vales, fuenteNormal));
                    colDepositoValesTmp.BorderWidth = 0;
                    colDepositoValesTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoChequeTmp = new PdfPCell(new Phrase("$" + cheque, fuenteNormal));
                    colDepositoChequeTmp.BorderWidth = 0;
                    colDepositoChequeTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoTransTmp = new PdfPCell(new Phrase("$" + trans, fuenteNormal));
                    colDepositoTransTmp.BorderWidth = 0;
                    colDepositoTransTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoFechaTmp = new PdfPCell(new Phrase(fecha, fuenteNormal));
                    colDepositoFechaTmp.BorderWidth = 0;
                    colDepositoFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    tablaDepositos.AddCell(colEmpleadoTmp);
                    tablaDepositos.AddCell(colDepositoEfectivoTmp);
                    tablaDepositos.AddCell(colDepositoTarjetaTmp);
                    tablaDepositos.AddCell(colDepositoValesTmp);
                    tablaDepositos.AddCell(colDepositoChequeTmp);
                    tablaDepositos.AddCell(colDepositoTransTmp);
                    tablaDepositos.AddCell(colDepositoFechaTmp);
                }

                reporte.Add(tituloDepositos);
                reporte.Add(tablaDepositos);
                reporte.Add(linea);
            }

            dr.Close();
            sql_con.Close();

            #endregion
            //===============================
            //=== FIN TABLA DE DEPOSITOS  ===
            //===============================


            //=========================
            //=== TABLA DE RETIROS  ===
            //=========================
            #region Tabla de Retiros
            Paragraph tituloRetiros = new Paragraph("HISTORIAL DE RETIROS\n\n", fuenteGrande);
            tituloRetiros.Alignment = Element.ALIGN_CENTER;

            anchoColumnas = new float[] { 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f };

            sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            sql_con.Open();
            sql_cmd = new MySqlCommand($"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'retiro' AND FechaOperacion > '{fechaGeneral.ToString("yyyy-MM-dd HH:mm:ss")}'", sql_con);
            dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                PdfPTable tablaRetiros = new PdfPTable(8);
                tablaRetiros.WidthPercentage = 100;
                tablaRetiros.SetWidths(anchoColumnas);

                PdfPCell colEmpleadoR = new PdfPCell(new Phrase("EMPLEADO", fuenteNegrita));
                colEmpleadoR.BorderWidth = 0;
                colEmpleadoR.HorizontalAlignment = Element.ALIGN_CENTER;
                colEmpleadoR.Padding = 3;

                PdfPCell colDepositoEfectivoR = new PdfPCell(new Phrase("EFECTIVO", fuenteNegrita));
                colDepositoEfectivoR.BorderWidth = 0;
                colDepositoEfectivoR.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoEfectivoR.Padding = 3;

                PdfPCell colDepositoTarjetaR = new PdfPCell(new Phrase("TARJETA", fuenteNegrita));
                colDepositoTarjetaR.BorderWidth = 0;
                colDepositoTarjetaR.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoTarjetaR.Padding = 3;

                PdfPCell colDepositoValesR = new PdfPCell(new Phrase("VALES", fuenteNegrita));
                colDepositoValesR.BorderWidth = 0;
                colDepositoValesR.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoValesR.Padding = 3;

                PdfPCell colDepositoChequeR = new PdfPCell(new Phrase("CHEQUE", fuenteNegrita));
                colDepositoChequeR.BorderWidth = 0;
                colDepositoChequeR.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoChequeR.Padding = 3;

                PdfPCell colDepositoTransR = new PdfPCell(new Phrase("TRANSFERENCIA", fuenteNegrita));
                colDepositoTransR.BorderWidth = 0;
                colDepositoTransR.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoTransR.Padding = 3;

                PdfPCell colDepositoCreditoR = new PdfPCell(new Phrase("CRÉDITO", fuenteNegrita));
                colDepositoCreditoR.BorderWidth = 0;
                colDepositoCreditoR.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoCreditoR.Padding = 3;

                PdfPCell colDepositoFechaR = new PdfPCell(new Phrase("FECHA", fuenteNegrita));
                colDepositoFechaR.BorderWidth = 0;
                colDepositoFechaR.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoFechaR.Padding = 3;

                tablaRetiros.AddCell(colEmpleadoR);
                tablaRetiros.AddCell(colDepositoEfectivoR);
                tablaRetiros.AddCell(colDepositoTarjetaR);
                tablaRetiros.AddCell(colDepositoValesR);
                tablaRetiros.AddCell(colDepositoChequeR);
                tablaRetiros.AddCell(colDepositoTransR);
                tablaRetiros.AddCell(colDepositoCreditoR);
                tablaRetiros.AddCell(colDepositoFechaR);

                //MySqlCommand sql_cmd;
                //MySqlDataReader dr;

                while (dr.Read())
                {
                    var efectivo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Efectivo"))).ToString("0.00");
                    var tarjeta = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Tarjeta"))).ToString("0.00");
                    var vales = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vales"))).ToString("0.00");
                    var cheque = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cheque"))).ToString("0.00");
                    var trans = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Transferencia"))).ToString("0.00");
                    var credito = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Credito"))).ToString("0.00");
                    var fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");

                    PdfPCell colEmpleadoTmpR = new PdfPCell(new Phrase("ADMIN", fuenteNormal));
                    colEmpleadoTmpR.BorderWidth = 0;
                    colEmpleadoTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoEfectivoTmpR = new PdfPCell(new Phrase("$" + efectivo, fuenteNormal));
                    colDepositoEfectivoTmpR.BorderWidth = 0;
                    colDepositoEfectivoTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoTarjetaTmpR = new PdfPCell(new Phrase("$" + tarjeta, fuenteNormal));
                    colDepositoTarjetaTmpR.BorderWidth = 0;
                    colDepositoTarjetaTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoValesTmpR = new PdfPCell(new Phrase("$" + vales, fuenteNormal));
                    colDepositoValesTmpR.BorderWidth = 0;
                    colDepositoValesTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoChequeTmpR = new PdfPCell(new Phrase("$" + cheque, fuenteNormal));
                    colDepositoChequeTmpR.BorderWidth = 0;
                    colDepositoChequeTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoTransTmpR = new PdfPCell(new Phrase("$" + trans, fuenteNormal));
                    colDepositoTransTmpR.BorderWidth = 0;
                    colDepositoTransTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoCreditoTmpR = new PdfPCell(new Phrase("$" + credito, fuenteNormal));
                    colDepositoCreditoTmpR.BorderWidth = 0;
                    colDepositoCreditoTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoFechaTmpR = new PdfPCell(new Phrase(fecha, fuenteNormal));
                    colDepositoFechaTmpR.BorderWidth = 0;
                    colDepositoFechaTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    tablaRetiros.AddCell(colEmpleadoTmpR);
                    tablaRetiros.AddCell(colDepositoEfectivoTmpR);
                    tablaRetiros.AddCell(colDepositoTarjetaTmpR);
                    tablaRetiros.AddCell(colDepositoValesTmpR);
                    tablaRetiros.AddCell(colDepositoChequeTmpR);
                    tablaRetiros.AddCell(colDepositoTransTmpR);
                    tablaRetiros.AddCell(colDepositoCreditoTmpR);
                    tablaRetiros.AddCell(colDepositoFechaTmpR);
                }

                reporte.Add(tituloRetiros);
                reporte.Add(tablaRetiros);
            }

            dr.Close();
            sql_con.Close();

            #endregion
            //=============================
            //=== FIN TABLA DE RETIROS  ===
            //=============================

            reporte.AddTitle("Reporte Corte de Caja");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }


        private void GenerarTicket()
        {
            var datos = FormPrincipal.datosUsuario;

            //Medidas de ticket de 57 y 80 mm
            //57mm = 161.28 pt
            //80mm = 226.08 pt

            var tipoPapel = 57;
            var anchoPapel = Convert.ToInt32(Math.Floor((((tipoPapel * 0.10) * 72) / 2.54)));
            var altoPapel = Convert.ToInt32(anchoPapel + 54);

            //Variables y arreglos para el contenido de la tabla
            float[] anchoColumnas = new float[] { };

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
                anchoColumnas = new float[] { 20f, 20f };
                medidaFuenteMensaje = 10;
                medidaFuenteGrande = 10;
                medidaFuenteNegrita = 8;
                medidaFuenteNormal = 8;
            }
            else if (tipoPapel == 57)
            {
                anchoColumnas = new float[] { 20f, 20f };
                medidaFuenteMensaje = 6;
                medidaFuenteGrande = 8;
                medidaFuenteNegrita = 6;
                medidaFuenteNormal = 6;
            }

            // Ruta donde se creara el archivo PDF
            var rutaArchivo = @"C:\Archivos PUDVE\Reportes\Caja\reporte_corte_" + fechaUltimoCorte.ToString("yyyyMMddHHmmss") + ".pdf";

            Document ticket = new Document(new iTextSharp.text.Rectangle(anchoPapel, altoPapel), 3, 3, 5, 0);
            PdfWriter writer = PdfWriter.GetInstance(ticket, new FileStream(rutaArchivo, FileMode.Create));

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteNormal);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, medidaFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteGrande);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteMensaje);

            ticket.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
            Paragraph subTitulo = new Paragraph("CORTE DE CAJA\nFecha: " + fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss"), fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;

            /**************************************
             ** TABLA VENTAS **
             **************************************/

            PdfPTable tabla = new PdfPTable(2);
            tabla.WidthPercentage = 100;
            tabla.SetWidths(anchoColumnas);

            PdfPCell colEfectivo = new PdfPCell(new Phrase("Efectivo", fuenteNormal));
            colEfectivo.BorderWidth = 0;
            colEfectivo.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colEfectivoC = new PdfPCell(new Phrase("0.00", fuenteNormal));
            colEfectivoC.BorderWidth = 0;
            colEfectivoC.HorizontalAlignment = Element.ALIGN_CENTER;

            tabla.AddCell(colEfectivo);
            tabla.AddCell(colEfectivoC);

            /*************************
             ** FIN TABLA DE VENTAS **
             *************************/

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            ticket.Add(titulo);
            ticket.Add(subTitulo);
            ticket.Add(linea);
            ticket.Add(tabla);

            ticket.AddTitle("Ticket Corte Caja");
            ticket.AddAuthor("PUDVE");
            ticket.Close();
            writer.Close();

            VisualizadorTickets vt = new VisualizadorTickets(rutaArchivo, rutaArchivo);
            vt.ShowDialog();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (opcion5 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (!Utilidades.AdobeReaderInstalado())
            {
                Utilidades.MensajeAdobeReader();
                return;
            }

            Utilidades.GenerarTicketCaja();
        }

        private void btnCambioAbonos_Click(object sender, EventArgs e)
        {
            CajaAbonos mostrarAbonos = new CajaAbonos();
            mostrarAbonos.Show();
        }
        public void verificarCantidadAbonos()
        {
            string ultimoDate = "";
            try
            {
                var fechaCorteUltima = cn.CargarDatos($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");
                if (!fechaCorteUltima.Rows.Count.Equals(0))
                {
                    foreach (DataRow fechaUltimoCorte in fechaCorteUltima.Rows)
                    {
                        ultimoDate = fechaUltimoCorte["FechaOperacion"].ToString();
                    }
                    DateTime fechaFinAbonos = DateTime.Parse(ultimoDate);

                    var fechaMovimientos = cn.CargarDatos($"SELECT sum(Total)AS Total FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{fechaFinAbonos.ToString("yyyy-MM-dd HH:mm:ss")}'");
                    var abono = "";

                    if (!fechaMovimientos.Rows.Count.Equals(0))
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
                if (!fechaCorteUltima.Rows.Count.Equals(0))
                {
                    foreach (DataRow fechaUltimoCorte in fechaCorteUltima.Rows)
                    {
                        ultimoDate = fechaUltimoCorte["FechaOperacion"].ToString();
                    }
                    DateTime fechaFinAbonos = DateTime.Parse(ultimoDate);

                    var fechaMovimientos = cn.CargarDatos($"SELECT sum(Total)AS Total FROM devoluciones WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{fechaFinAbonos.ToString("yyyy-MM-dd HH:mm:ss")}'");
                    var devolucion = "";

                    if (!fechaMovimientos.Rows.Count.Equals(0))
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


            if (abonos > 0)
            {
                lbCambioAbonos.Visible = true;
            }
            else
            {
                lbCambioAbonos.Visible = false;
            }

            if (devoluciones > 0)
            {
                lbCambioDevoluciones.Visible = true;
            }
            else
            {
                lbCambioDevoluciones.Visible = false;
            }

            if (MetodosBusquedas.totalSInicial > 0)
            {
                lbSaldoInicialInfo.Visible = true;
            }
            else
            {
                lbSaldoInicialInfo.Visible = false;
            }
        }

        private void lbCambioAbonos_Click(object sender, EventArgs e)
        {
            CajaAbonos mostrarAbonosCaja = Application.OpenForms.OfType<CajaAbonos>().FirstOrDefault();

            var validarAbono = "abono";
            if (mostrarAbonosCaja == null)
            {
                abonos_devoluciones = "abonos";
                CajaAbonos mostrarAbonos = new CajaAbonos();
                mostrarAbonos.Show();
            }

            if (mostrarAbonosCaja != null)
            {
                if (mostrarAbonosCaja.WindowState == FormWindowState.Minimized || mostrarAbonosCaja.WindowState == FormWindowState.Normal)
                {
                    mostrarAbonosCaja.BringToFront();
                }
            }
        }

        private void lbCambioDevoluciones_Click(object sender, EventArgs e)
        {
            CajaAbonos mostrarDevolucionesCaja = Application.OpenForms.OfType<CajaAbonos>().FirstOrDefault();

            var validarDEvolucion = "devolucion";
            if (mostrarDevolucionesCaja == null)
            {
                abonos_devoluciones = "devoluciones";
                CajaAbonos mostrarDevoluciones = new CajaAbonos();
                mostrarDevoluciones.Show();
            }

            if (mostrarDevolucionesCaja != null)
            {
                if (mostrarDevolucionesCaja.WindowState == FormWindowState.Minimized || mostrarDevolucionesCaja.WindowState == FormWindowState.Normal)
                {
                    mostrarDevolucionesCaja.BringToFront();
                }
            }
        }

        private void CajaN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnAgregarDinero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnRetirarDinero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnReporteAgregar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnReporteRetirar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnCorteCaja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            CajaAbonos mostrarAbonosCaja = Application.OpenForms.OfType<CajaAbonos>().FirstOrDefault();

            var validarSaldoInicial = "Saldo Inicial";
            if (mostrarAbonosCaja == null)
            {
                abonos_devoluciones = "Saldo Inicial";
                CajaAbonos mostrarAbonos = new CajaAbonos();
                mostrarAbonos.Show();
            }

            if (mostrarAbonosCaja != null)
            {
                if (mostrarAbonosCaja.WindowState == FormWindowState.Minimized || mostrarAbonosCaja.WindowState == FormWindowState.Normal)
                {
                    mostrarAbonosCaja.BringToFront();
                }
            }
        }
    }
}
