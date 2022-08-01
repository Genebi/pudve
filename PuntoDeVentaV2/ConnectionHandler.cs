using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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
        CargarDatosCaja cdc = new CargarDatosCaja();

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

        float anticiposAplicados2 = 0f;
        //int anticiposAplicados = 0;
        //double abonos;

        //Variables para anticipos y abonos
        float anticiposAplicados = 0f;
        float abonos = 0f;
        float abonoEfectivoI = 0f;
        float abonoTarjetaI = 0f;
        float abonoValesI = 0f;
        float abonoChequeI = 0f;
        float abonoTransferenciaI = 0f;

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

        public bool verificarInternet()
        {
            //string host = "google.com.mx";
            //bool resul = false;
            //Ping p = new Ping();
            //try
            //{
            //    PingReply reply = p.Send(host, 2000);
            //    if (reply.Status == IPStatus.Success)
            //    {
            //        resul = true;
            //        //MessageBox.Show("Conecxion Exitosa");
            //    }
            //}
            //catch
            //{
            //    resul = false;
            //    //MessageBox.Show("Conecxion Fallida");
            //}
            //return resul;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://sifo.com.mx");
            request.AllowAutoRedirect = false;
            request.Method = "HEAD";

            try
            {
                using (var response = request.GetResponse())
                {
                    return true;
                }
            }
            catch (WebException wex)
            {
                return false;
            }
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
            var datoMEtodoMAfufo = verificarInternet(); // Este metodo no funciona siempre es false

            if (datoMEtodoMAfufo)
            {
                if (ConectadoInternet())
                {
                    var servidor = Properties.Settings.Default.Hosting;

                    if (string.IsNullOrWhiteSpace(servidor))
                    {
                        bool consultarCaja = false;
                        bool consultarProductos = false;

                        MySqlConnection conexion = new MySqlConnection();

                        conexion.ConnectionString = $"server={_server};database={_database};uid={_username};pwd={_password};";

                        try
                        {
                            conexion.Open();
                            MySqlCommand consultar = conexion.CreateCommand();
                            MySqlCommand actualizar = conexion.CreateCommand();

                            //Verificamos si el usuario que se quiere registrar ya se encuentra registrado en la base de datos online
                            consultar.CommandText = $"SELECT consultarProductos, consultarCaja FROM usuarios WHERE usuario = '{FormPrincipal.userNickName}'";
                            MySqlDataReader dr = consultar.ExecuteReader();

                            if (dr.Read())
                            {
                                consultarProductos = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("consultarProductos")));
                                consultarCaja = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("consultarCaja")));
                            }

                            dr.Close();

                            if (consultarProductos)
                            {
                                actualizar.CommandText = $"UPDATE usuarios SET consultarProductos = 0 WHERE usuario = '{FormPrincipal.userNickName}'";

                                actualizar.ExecuteNonQuery();
                            }

                            if (consultarCaja)
                            {
                                actualizar.CommandText = $"UPDATE usuarios SET consultarCaja = 0 WHERE usuario = '{FormPrincipal.userNickName}'";

                                actualizar.ExecuteNonQuery();
                            }

                            conexion.Close();
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message.ToString());
                        }

                        if (consultarCaja)
                        {
                            insertarCaja();
                        }

                        if (consultarProductos)
                        {
                            insertarProductos();
                        }
                    }
                }
            }
        }

        private void insertarCaja()
        {
            string connectionString = string.Empty;
            connectionString = "SERVER=" + _server + ";" + "DATABASE=" + _database + ";" + "UID=" + _username + ";" + "PASSWORD=" + _password + ";";

            iniciarVariablesWebService();
            CargarSaldoInicial();
            var datos = cdc.CargarSaldo("Web Service");
            //CargarSaldo();

            //Consulta para insertar seccionCaja
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                MySqlCommand agregar = conexion.CreateCommand();
                conexion.Open();

                //////////////////////////////// Antiguos datos y forma de mandar datos a web service ////////////////////////////
                //agregar.CommandText = $@"INSERT INTO seccionCaja (efectivoVentas, tarjetaVentas, valesVentas, chequeVentas, transferenciaVentas, creditoVentas, abonosVentas, anticiposUtilizadosVentas, totalVentas,  
                //                                                  efectivoAbonos, tarjetaAbonos, valesAbonos, chequeAbonos, transferenciaAbonos, totalAbonos,
                //                                                  efectivoAnticipos, tarjetaAnticipos, valesAnticipos, chequeAnticipos, transferenciaAnticipos, totalAnticipos,   
                //                                                  efectivoDineroAgregado, tarjetaDineroAgregado, valesDineroAgregado, chequeDineroAgregado, transferenciaDineroAgregado, totalDineroAgregado,   
                //                                                  efectivoRetirado, tarjetaRetirado, valesRetirado, chequeRetirado, transferenciaRetirado, anticiposUtilizadosRetiro, totalRetirado,
                //                                                  efectivoTotalCaja, tarjetaTotalCaja, valesTotalCaja, chequeTotalCaja, transferenciaTotalCaja, creditoTotalCaja, anticiposUtilizadosTotalCaja, saldoInicialTotalCaja, subtotalEnCajaTotalCaja, dineroRetiradoTotalCaja, totalEnCajaTotalCaja, 
                //                                                  fechaActualizacion, nickUsuario, idUsuario) 
                //                                         VALUES ('{vEfectivo}', '{vTarjeta}','{vVales}', '{vCheque}', '{vTrans}', '{vCredito}', '{abonos}', '{vAnticipos}', '{totalVentas}',
                //                                                 '{abonoEfectivoI}', '{abonoTarjetaI}', '{abonoValesI}', '{abonoChequeI}', '{abonoTransferenciaI}', '{abonos}',
                //                                                 '{aEfectivo}', '{aTarjeta}', '{aVales}', '{aCheque}', '{aTrans}', '{totalAnticipos}', 
                //                                                 '{dEfectivo}', '{dTarjeta}', '{dVales}', '{dCheque}', '{dTrans}', '{totalDineroAgregado}', 
                //                                                 '{retiroEfectivo}', '{retiroTarjeta}', '{retiroVales}', '{retiroCheque}', '{retiroTrans}', '{anticipos1}','{dineroRetirado}', 
                //                                                 '{efectivo}', '{tarjeta}', '{vales}', '{cheque}', '{trans}', '{credito}', '{anticipos1}', '{saldoInicial}', '{subtotal}', '{dineroRetirado}', '{totalCaja}',
                //                                                 '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{userNickName}', '{FormPrincipal.userID.ToString()}')";
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                /////////////////////////////// Nuevos datos y forma de mandar datos a web service /////////////////////////////

                agregar.CommandText = $@"INSERT INTO seccionCaja (efectivoVentas, tarjetaVentas, valesVentas, chequeVentas, transferenciaVentas, creditoVentas, abonosVentas, anticiposUtilizadosVentas, totalVentas,  
                                                                  efectivoAbonos, tarjetaAbonos, valesAbonos, chequeAbonos, transferenciaAbonos, totalAbonos,
                                                                  efectivoAnticipos, tarjetaAnticipos, valesAnticipos, chequeAnticipos, transferenciaAnticipos, totalAnticipos,   
                                                                  efectivoDineroAgregado, tarjetaDineroAgregado, valesDineroAgregado, chequeDineroAgregado, transferenciaDineroAgregado, totalDineroAgregado,   
                                                                  efectivoRetirado, tarjetaRetirado, valesRetirado, chequeRetirado, transferenciaRetirado, anticiposUtilizadosRetiro, totalRetirado,
                                                                  efectivoTotalCaja, tarjetaTotalCaja, valesTotalCaja, chequeTotalCaja, transferenciaTotalCaja, creditoTotalCaja, anticiposUtilizadosTotalCaja, saldoInicialTotalCaja, subtotalEnCajaTotalCaja, dineroRetiradoTotalCaja, totalEnCajaTotalCaja, 
                                                                  fechaActualizacion, nickUsuario, idUsuario, totalDevoluciones) 
                                                         VALUES ('{datos[0]}', '{datos[1]}','{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[13]}', '{datos[6]}', '{datos[7]}',
                                                                 '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}',
                                                                 '{datos[14]}', '{datos[15]}', '{datos[16]}', '{datos[17]}', '{datos[18]}', '{datos[19]}', 
                                                                 '{datos[20]}', '{datos[21]}', '{datos[22]}', '{datos[23]}', '{datos[24]}', '{datos[25]}', 
                                                                 '{datos[26]}', '{datos[27]}', '{datos[28]}', '{datos[29]}', '{datos[30]}', '{datos[31]}','{datos[33]}', 
                                                                 '{datos[51]}', '{datos[52]}', '{datos[53]}', '{datos[54]}', '{datos[55]}', '{datos[39]}', '{datos[31]}', '{mb.SaldoInicialCaja(FormPrincipal.userID)}', '{datos[41]}', '{datos[33]}', '{datos[41]}',
                                                                 '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{userNickName}', '{FormPrincipal.userID.ToString()}', '{datos[32]}')";

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                int add = agregar.ExecuteNonQuery();
                conexion.Close();
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
                                                        ORDER BY P.Nombre DESC";

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

                using (MySqlCommand comando = new MySqlCommand())

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

            anticiposAplicados = 0f;
            abonoEfectivoI = 0f;
            abonoTarjetaI = 0f;
            abonoValesI = 0f;
            abonoChequeI = 0f;
            abonoTransferenciaI = 0f;
            abonos = 0f;

            aEfectivo = 0f;
            aTarjeta = 0f;
            aVales = 0f;
            aCheque = 0f;
            aTrans = 0f;
            totalAnticipos = 0f;
            anticiposAplicados2 = 0f;

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

            retiroEfectivo = 0f;
            retiroTarjeta = 0f;
            retiroVales = 0f;
            retiroCheque = 0f;
            retiroTrans = 0f;
            retiroCredito = 0f;

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

            var consultaAnticipoAplicado = "";
            try
            {
                //Anticipos
                var segundaConsulta = cn.CargarDatos($"SELECT sum(AnticipoAplicado) FROM Anticipos  WHERE IDUsuario = '{FormPrincipal.userID}'");
                if (segundaConsulta.Rows.Count > 0 && !string.IsNullOrWhiteSpace(segundaConsulta.ToString()))
                {
                    foreach (DataRow obtenerAnticipoAplicado in segundaConsulta.Rows)
                    {
                        consultaAnticipoAplicado = obtenerAnticipoAplicado["sum(AnticipoAplicado)"].ToString();
                    }
                    anticiposAplicados = float.Parse(consultaAnticipoAplicado); //Hasta esta linea.
                }
                //Abonos
                var fechaCorteUltima = cn.CargarDatos($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");
                string ultimoDate = "";
                if (fechaCorteUltima.Rows.Count > 0 && string.IsNullOrWhiteSpace(fechaCorteUltima.ToString()))
                {
                    foreach (DataRow fechaUltimoCorte in fechaCorteUltima.Rows)
                    {
                        ultimoDate = fechaUltimoCorte["FechaOperacion"].ToString();
                    }
                    DateTime fechaFinAbonos = DateTime.Parse(ultimoDate);

                    var fechaMovimientos = cn.CargarDatos($"SELECT sum(Total), sum(Efectivo), sum(Tarjeta), sum(Vales), sum(Cheque), sum(Transferencia) FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{fechaFinAbonos.ToString("yyyy-MM-dd HH:mm:ss")}'");
                    var abono = "";
                    var abonoEfectivo = ""; var abonoTarjeta = ""; var abonoVales = ""; var abonoCheque = ""; var abonoTransferencia = "";
                    foreach (DataRow cantidadAbono in fechaMovimientos.Rows)
                    {
                        abono = cantidadAbono["sum(Total)"].ToString();
                        abonoEfectivo = cantidadAbono["sum(Efectivo)"].ToString();
                        abonoTarjeta = cantidadAbono["sum(Tarjeta)"].ToString();
                        abonoVales = cantidadAbono["sum(Vales)"].ToString();
                        abonoCheque = cantidadAbono["sum(Cheque)"].ToString();
                        abonoTransferencia = cantidadAbono["sum(Transferencia)"].ToString();
                    }
                    abonos = float.Parse(abono);
                    abonoEfectivoI = float.Parse(abonoEfectivo);
                    abonoTarjetaI = float.Parse(abonoTarjeta);
                    abonoValesI = float.Parse(abonoVales);
                    abonoChequeI = float.Parse(abonoCheque);
                    abonoTransferenciaI = float.Parse(abonoTransferencia);
                }
            }
            catch
            {

            }

            fechaGeneral = fechaDefault;
            drUno.Close();

            var consulta = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID}";
            consultaDos = new MySqlCommand(consulta, sql_con);
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
                    totalVentas = (vEfectivo + vTarjeta + vVales + vCheque + vTrans + vCredito + anticiposAplicados/*vAnticipos*/);
                }

                if (operacion == "anticipo" && fechaOperacion > fechaDefault)
                {
                    aEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    aTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    aVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    aCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    aTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    totalAnticipos /*anticiposAplicados2*/ = (aEfectivo + aTarjeta + aVales + aCheque + aTrans);

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
            efectivo = vEfectivo + aEfectivo + dEfectivo + abonoEfectivoI;
            tarjeta = vTarjeta + aTarjeta + dTarjeta + abonoTarjetaI;
            vales = vVales + aVales + dVales + abonoValesI;
            cheque = vCheque + aCheque + dCheque + abonoChequeI;
            trans = vTrans + aTrans + dTrans + abonoTransferenciaI ;
            credito = vCredito ;
            anticipos1 = totalAnticipos + anticiposAplicados/* totalAnticipos*/ /*vAnticipos*/;
            subtotal = efectivo + tarjeta + vales + cheque + trans /*+ credito*/ + saldoInicial;
            totalCaja = (subtotal - dineroRetirado);

            // Cerramos la conexion y el datareader
            //drUno.Close();
            drDos.Close();
            sql_con.Close();
        }

        public void CargarSaldoInicial()
        {
            saldoInicial = mb.SaldoInicialCaja(FormPrincipal.userID);
        }

        public void actualizarConteo(string usr)
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
                    upDateUsr.CommandText = $"UPDATE usuarios SET ConteoInicioDeSesion = ConteoInicioDeSesion +1 WHERE usuario = '{usr}'";
                    int actualizarUsr = upDateUsr.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception)
                {
                    //MessageBox.Show("Error al Tratar de Actualizar Usuarios; Causa: " + ex.Message.ToString(), "Error de Update Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void registrarInicio(string usr, string fecha)
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
                    upDateUsr.CommandText = $"INSERT INTO iniciosdesesion (Usuario, Fecha) VALUES ('{usr}', '{fecha}')"; 
                    int actualizarUsr = upDateUsr.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception)
                {
                    //MessageBox.Show("Error al Tratar de Actualizar Usuarios; Causa: " + ex.Message.ToString(), "Error de Update Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}