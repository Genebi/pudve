﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    class ConnectionHandler
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        //Metodos de otros form

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
        float efectivo = 0f;
        float tarjeta = 0f;
        float vales = 0f;
        float cheque = 0f;
        float trans = 0f;
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


        #region declare
        //public MySqlConnection Con;
        private string _server;
        private string _database;
        private string _username;
        private string _password;

        public FormPrincipal Form;
        #region Threads
        public bool StopOpenConThread = true;
        public bool StopCloseConThread = true;
        public bool StopCheckConThread = true;
        #endregion
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public ConnectionHandler()
        {
            Initialize();
        }

        /// <summary>
        /// Set connection.
        /// </summary>
        private void Initialize()
        {
            _server = "74.208.135.60";
            _database = "pudve";
            _username = "pudvesoftware";
            _password = "Steroids12";

            var connectionString = "SERVER=" + _server + ";" + "DATABASE=" + _database + ";" + "UID=" + _username + ";" + "PASSWORD=" + _password + ";";

            //Con = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Hooks the main form.
        /// </summary>
        /// <param name="mainForm"></param>
        public void HookMainForm(FormPrincipal mainForm)
        {
            Form = mainForm;
        }

        /// <summary>
        /// Starts the thread.
        /// </summary>
        public void StartCheckConnectionState()
        {
            var thread = new Thread(CheckConnectionState);
            thread.Start();

        }

        //public string ErrorHandler;

        /// <summary>
        /// Checks connectionstate.
        /// </summary>
        public void CheckConnectionState()
        {
            MejoraMysql();

        }




        //Se necesita para saber si la computadora tiene conexion a internet
        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int Descripcion, int ValorReservado);

        public static bool ConectadoInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        //// Proceso del Hilo del Timer ////

        public void MejoraMysql()
        {

            if (ConectadoInternet())
            {
                var servidor = Properties.Settings.Default.Hosting;
                if (string.IsNullOrWhiteSpace(servidor))
                {
                    actualizarIdUsuario();
                    eliminarCaja();
                    insertarCaja();
                    eliminarProductos();
                    insertarProductos();
                }
            }

        }

        private void actualizarIdUsuario()
        {
            string connectionString = string.Empty;
            connectionString = "SERVER=" + _server + ";" + "DATABASE=" + _database + ";" + "UID=" + _username + ";" + "PASSWORD=" + _password + ";";

            //Actualizar IdUsuario en tabla Usuarios en MySQL
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    userNickName = FormPrincipal.userNickName;
                    MySqlCommand upDateUsr = conexion.CreateCommand();
                    conexion.Open();
                    upDateUsr.CommandText = $"UPDATE usuarios SET idLocal ='{FormPrincipal.userID.ToString()}' WHERE usuario = '{userNickName}'";
                    int actualizarUsr = upDateUsr.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception)
                {
                    //MessageBox.Show("Error al Tratar de Actualizar Usuarios; Causa: " + ex.Message.ToString(), "Error de Update Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void eliminarCaja()
        {
            string connectionString = string.Empty;
            connectionString = "SERVER=" + _server + ";" + "DATABASE=" + _database + ";" + "UID=" + _username + ";" + "PASSWORD=" + _password + ";";

            // Consulta Borrar de MySQL por ID de Usuario
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    MySqlCommand eliminarCaja = conexion.CreateCommand();
                    conexion.Open();
                    eliminarCaja.CommandText = $@"DELETE FROM seccionCaja WHERE idUsuario ='{FormPrincipal.userID.ToString()}' AND nickUsuario = '{FormPrincipal.userNickName}'";
                    int delete = eliminarCaja.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al Tratar de borrar Seccion Caja; Causa: " + ex.Message.ToString(), "Error de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void insertarCaja()
        {
            string connectionString = string.Empty;
            connectionString = "SERVER=" + _server + ";" + "DATABASE=" + _database + ";" + "UID=" + _username + ";" + "PASSWORD=" + _password + ";";

            iniciarVariablesWebService();
            CargarSaldoInicial();
            CargarSaldo();

            //Consulta para insertar seccionCaja
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                MySqlCommand agregar = conexion.CreateCommand();
                conexion.Open();

                agregar.CommandText = $@"INSERT INTO seccionCaja (efectivoVentas, tarjetaVentas, valesVentas, chequeVentas, transferenciaVentas, creditoVentas, anticiposUtilizadosVentas, totalVentas,  
                                                                  efectivoAnticipos, tarjetaAnticipos, valesAnticipos, chequeAnticipos, transferenciaAnticipos, totalAnticipos,   
                                                                  efectivoDineroAgregado, tarjetaDineroAgregado, valesDineroAgregado, chequeDineroAgregado, transferenciaDineroAgregado, totalDineroAgregado,   
                                                                  efectivoTotalCaja, tarjetaTotalCaja, valesTotalCaja, chequeTotalCaja, transferenciaTotalCaja, creditoTotalCaja, anticiposUtilizadosTotalCaja, saldoInicialTotalCaja, subtotalEnCajaTotalCaja, dineroRetiradoTotalCaja, totalEnCajaTotalCaja, 
                                                                  fechaActualizacion, nickUsuario, idUsuario) 
                                                         VALUES ('{vEfectivo}', '{vTarjeta}','{vVales}', '{vCheque}', '{vTrans}', '{vCredito}', '{vAnticipos}', '{totalVentas}',
                                                                 '{aEfectivo}', '{aTarjeta}', '{aVales}', '{aCheque}', '{aTrans}', '{totalAnticipos}', 
                                                                 '{dEfectivo}', '{dTarjeta}', '{dVales}', '{dCheque}', '{dTrans}', '{totalDineroAgregado}',  
                                                                 '{efectivo}', '{tarjeta}', '{vales}', '{cheque}', '{trans}', '{credito}', '{anticipos1}', '{saldoInicial}', '{subtotal}', '{dineroRetirado}', '{totalCaja}',
                                                                 '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{userNickName}', '{FormPrincipal.userID.ToString()}')";
                int add = agregar.ExecuteNonQuery();
                conexion.Close();


            }
        }

        private void eliminarProductos()
        {
            string connectionString = string.Empty;

            connectionString = "SERVER=" + _server + ";" + "DATABASE=" + _database + ";" + "UID=" + _username + ";" + "PASSWORD=" + _password + ";";

            //Consulta Borrar de MySQL por ID de Usuario
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    MySqlCommand eliminar = conexion.CreateCommand();
                    conexion.Open();
                    eliminar.CommandText = $@"DELETE FROM seccionProductos WHERE idUsuario ='{FormPrincipal.userID.ToString()}' AND nickUsuario = '{FormPrincipal.userNickName}'";
                    int borrrado1 = eliminar.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al Tratar de borrar Seccion Productos; Causa: " + ex.Message.ToString(), "Error de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void insertarProductos()
        {
            string connectionString = string.Empty;
            string queryCargarDatosProductos = string.Empty;

            connectionString = "SERVER=" + _server + ";" + "DATABASE=" + _database + ";" + "UID=" + _username + ";" + "PASSWORD=" + _password + ";";

            //MessageBox.Show("Empieza Insercion");
            StringBuilder comandS = new StringBuilder($@"INSERT INTO seccionProductos(idUsuario, nickUsuario, nombreProductos, nombreAlterno1, nombreAlterno2, 
                                                                                              stockProductos, precioProductos, revisionProductos, claveProductos, 
                                                                                              codigoProductos, historialProductos, tipoProductos, fechaUpdate)
                                                          VALUES ");

            //Consulta Insertar de MySQL por ID de Usuario lo de Productos
            queryCargarDatosProductos = $@"SELECT P.Nombre, P.NombreAlterno1, P.NombreAlterno2, P.Stock, P.Precio, P.NumeroRevision, P.ClaveInterna, P.CodigoBarras, P.Tipo 
                                                        FROM productos AS P 
                                                        WHERE Status = 1
                                                        AND IDUsuario = {FormPrincipal.userID.ToString()}
                                                        ORDER BY P.Nombre ASC";

            ////Actualizar IdUsuario en tabla Usuarios en MySQL
            //using (MySqlConnection conexion = new MySqlConnection(connectionString))
            //{
            //    try
            //    {
            //        MySqlCommand actualizarUser = conexion.CreateCommand();
            //        conexion.Open();
            //        actualizarUser.CommandText = $@"UPDATE usuarios SET idLocal = '{FormPrincipal.userID.ToString()}' WHERE usuario = '{userNickName}'";
            //        int actualizarUsrs = actualizarUser.ExecuteNonQuery();
            //        conexion.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error al Tratar de Actualizar Seccion Productos ; Causa: " + ex.Message.ToString(), "Error de Update Productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }

            //}

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                List<String> Rows = new List<string>();

                using (DataTable tablaProductos = cn.CargarDatos(queryCargarDatosProductos))
                {
                    if (tablaProductos.Rows.Count > 0)
                    {
                        //MessageBox.Show("Entra al if del ciclo for");
                        for (int i = 0; i < tablaProductos.Rows.Count; i++)
                        {
                            userNickName = FormPrincipal.userNickName;
                            nombreP = tablaProductos.Rows[i]["Nombre"].ToString();
                            nombreAlterno1P = tablaProductos.Rows[i]["NombreAlterno1"].ToString();
                            nombreAlterno2P = tablaProductos.Rows[i]["NombreAlterno2"].ToString();
                            stockP = (float)Convert.ToDouble(tablaProductos.Rows[i]["Stock"].ToString());
                            precioP = (float)Convert.ToDouble(tablaProductos.Rows[i]["Precio"].ToString());
                            revisionP = (float)Convert.ToDouble(tablaProductos.Rows[i]["NumeroRevision"].ToString());
                            claveP = tablaProductos.Rows[i]["ClaveInterna"].ToString();
                            codigoP = tablaProductos.Rows[i]["CodigoBarras"].ToString();
                            tipoP = tablaProductos.Rows[i]["Tipo"].ToString();


                            Rows.Add(String.Format("('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}')",
                                MySqlHelper.EscapeString($"{FormPrincipal.userID.ToString()}"), MySqlHelper.EscapeString($"{userNickName}"), MySqlHelper.EscapeString($"{nombreP}"),
                                MySqlHelper.EscapeString($"{nombreAlterno1P}"), MySqlHelper.EscapeString($"{nombreAlterno2P}"), MySqlHelper.EscapeString($"{stockP}"),
                                MySqlHelper.EscapeString($"{precioP}"), MySqlHelper.EscapeString($"{revisionP}"), MySqlHelper.EscapeString($"{claveP}"),
                                MySqlHelper.EscapeString($"{codigoP}"), MySqlHelper.EscapeString($"{historialP}"), MySqlHelper.EscapeString($"{tipoP}"),
                                MySqlHelper.EscapeString($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}")));
                        }
                        comandS.Append(String.Join(",", Rows));
                        //sComand.Append(":");

                        conexion.Open();
                        //MessageBox.Show("Fin Insercion");
                        string contenidoquery = comandS.ToString();
                        using (MySqlCommand myCmd = new MySqlCommand(comandS.ToString(), conexion))
                        {
                            myCmd.CommandType = CommandType.Text;
                            myCmd.ExecuteNonQuery();
                        }

                    }
                }

                conexion.Close();
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

            efectivo = 0f;
            tarjeta = 0f;
            vales = 0f;
            cheque = 0f;
            trans = 0f;
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

        private void CargarSaldo()
        {
            SQLiteConnection sql_con;
            SQLiteCommand consultaUno, consultaDos;
            SQLiteDataReader drUno, drDos;

            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                sql_con = new SQLiteConnection("Data source=//" + servidor + @"\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }
            else
            {
                sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }

            sql_con.Open();

            var fechaDefault = Convert.ToDateTime("0001-01-01 00:00:00");

            var consultarFecha = $"SELECT FechaOperacion FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'corte' ORDER BY FeChaOperacion DESC LIMIT 1";
            consultaUno = new SQLiteCommand(consultarFecha, sql_con);
            drUno = consultaUno.ExecuteReader();

            if (drUno.Read())
            {
                var fechaTmp = Convert.ToDateTime(drUno.GetValue(drUno.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                fechaDefault = Convert.ToDateTime(fechaTmp);
            }

            fechaGeneral = fechaDefault;

            var consulta = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID}";
            consultaDos = new SQLiteCommand(consulta, sql_con);
            drDos = consultaDos.ExecuteReader();

            int saltar = 0;

            while (drDos.Read())
            {
                string operacion = drDos.GetValue(drDos.GetOrdinal("Operacion")).ToString();
                var auxiliar = Convert.ToDateTime(drDos.GetValue(drDos.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                var fechaOperacion = Convert.ToDateTime(auxiliar);

                if (operacion == "venta" && fechaOperacion > fechaDefault)
                {
                    if (saltar == 0)
                    {
                        saltar++;
                        continue;
                    }

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
            efectivo = vEfectivo + aEfectivo + dEfectivo;
            tarjeta = vTarjeta + aTarjeta + dTarjeta;
            vales = vVales + aVales + dVales;
            cheque = vCheque + aCheque + dCheque;
            trans = vTrans + aTrans + dTrans;
            credito = vCredito;
            anticipos1 = vAnticipos;
            subtotal = efectivo + tarjeta + vales + cheque + trans + credito + saldoInicial;
            totalCaja = (subtotal - dineroRetirado);

            // Cerramos la conexion y el datareader
            drUno.Close();
            drDos.Close();
            sql_con.Close();
        }

        public void CargarSaldoInicial()
        {
            saldoInicial = mb.SaldoInicialCaja(FormPrincipal.userID);
        }

    }

}