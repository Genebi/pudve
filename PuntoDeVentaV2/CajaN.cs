using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class CajaN : Form
    {

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();
        CargarDatosCaja cdc = new CargarDatosCaja();

        int clickBotonCorteDeCaja = 0;

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

        //Variable que obtiene la ruta del PDF del corte de caja
        string obtenerRutaPDF = string.Empty;

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

        // Variables Abonos
        public static float totalEfectivoAbono = 0f;
        public static float totalTarjetaAbono = 0f;
        public static float totalValesAbono = 0f;
        public static float totalChequeAbono = 0f;
        public static float totalTransferenciaAbono = 0f;

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

        int verificar = 0;

        int tipoDeMovimiento = 0;

        public static int corteCaja = 0;

        public static string opcionComboBoxFiltroAdminEmp = string.Empty;

        int idAdministradorOrUsuario = 0;
        string nombreDeUsuario = string.Empty;
        string razonSocialUsuario = string.Empty;

        public static decimal sumaDeTotalesEnCaja = 0,
            cantidadTotalEfectivoEnCaja = 0,
            cantidadTotalTarjetaEnCaja = 0,
            cantidadTotalValesEnCaja = 0,
            cantidadTotalCehqueEnCaja = 0,
            cantidadTotalTransferenciaEnCaja = 0,
            totalSaldoInicial = 0,
            cantidadEfectivoSaldoInicialEnCaja = 0,
            cantidadTarjetaSaldoInicialEnCaja = 0,
            cantidadValesSaldoInicialEnCaja = 0,
            cantidadChequeSaldoInicialEnCaja = 0,
            cantidadTransferenciaSaldoInicialEnCaja = 0;

        decimal totalEfectivoVentaEnCaja = 0,
                totalTarjetaVentaEnCaja = 0,
                totalValesEnVentaCaja = 0,
                totalChequesVentaEnCaja = 0,
                totalTransferenciaVentaEnCaja = 0,
                totalSaldoInicialVentaEnCaja = 0,
                totalEfectivoAnticiposEnCaja = 0,
                totalTarjetaAnticiposEnCaja = 0,
                totalValesAnticiposEnCaja = 0,
                totalChequesAnticipoEnCaja = 0,
                totalTransferenciaAnticiposEnCaja = 0,
                totalEfectivoDepsitosEnCaja = 0,
                totalTarjetaDepositosEnCaja = 0,
                totalValesDepositosEnCaja = 0,
                totalChequesDepsoitosEnCaja = 0,
                totalTransferenciasDepositosEnCaja = 0,
                totalEfectivoRetiroEnCaja = 0,
                totalTarjetaRetiroEnCaja = 0,
                totalValesRetiroEnCaja = 0,
                totalChequesRetiroEnCaja = 0,
                totalTransferenciaRetiroEnCaja = 0,
                totalAbonoEfectivo = 0,
                totalAbonoTarjeta = 0,
                totalAbonoVales = 0,
                totalAbonoCheque = 0,
                totalAbonoTransferencia = 0,
                totalAbonoRealizado = 0;

        string ultimaFechaDeCorteParaAbonos = string.Empty,
                fechaFormateadaCorteParaAbonos = string.Empty,
                ultimoCorteDeCaja = string.Empty,
                idUltimoCorteDeCaja = string.Empty;

        decimal cantidadEfectivo = 0,
                cantidadTarjeta = 0,
                cantidadVales = 0,
                cantidadCheque = 0,
                cantidadTransferencia = 0,
                cantidadCredito = 0,
                cantidadAnticipos = 0,
                cantidadTotalVentas = 0,
                // Ventas sección de variables del apartado de efectivo para Administrador o Empleado
                cantidadEfectivoAnticipos = 0,
                cantidadTarjetaAnticipos = 0,
                cantidadValesAnticipos = 0,
                cantidadChequeAnticipos = 0,
                cantidadTransferenciaAnticipos = 0,
                cantidadTotalAnticipos = 0,
                // Anticipos sección de variables del apartado de efectivo para Administrador o Empleado
                cantidadEfectivoAgregado = 0,
                cantidadTarjetaAgregado = 0,
                cantidadValesAgregado = 0,
                cantidadChequeAgregado = 0,
                cantidadTransferenciaAgregado = 0,
                cantidadTotalDineroAgregadoAgregado = 0,
                // Dinero Aegragdo sección de variables del apartado de efectivo para Administrador o Empleado
                cantidadEfectivoVentaTodos = 0,
                cantidadTarjetaVentaTodos = 0,
                cantidadValesVentaTodos = 0,
                cantidadChequeVentaTodos = 0,
                cantidadTransferenciaVentaTodos = 0,
                cantidadCreditoVentaTodos = 0,
                cantidadAbonosVentaTodos = 0,
                cantidadAnticiposVentaTodos = 0,
                cantidadTotalVentasVentaTodos = 0,
                // Ventas sección de todos 
                cantidadEfectivoAgregaddo = 0,
                cantidadTarjetaAgregaddo = 0,
                cantidadValesAgregaddo = 0,
                cantidadChequeAgregaddo = 0,
                cantidadTransferenciaAgregaddo = 0,
                cantidadTotalDineroAgregado = 0,
                // Dinero Agregado de todos
                cantidadEfectivoRetirado = 0,
                cantidadTarjetaRetirado = 0,
                cantidadValesRetirado = 0,
                cantidadChequeRetirado = 0,
                cantidadTransferenciaRetirado = 0,
                cantidadTotalDineroRetirado = 0;
        // Dinero Retirado de todos

        public CajaN()
        {
            InitializeComponent();
        }

        private void CajaN_Load(object sender, EventArgs e)
        {
            cbFiltroAdminEmpleado.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            recargarDatos = true;
            verificarSiExisteCorteDeCaja();
            verComboBoxAdministradorEmpleado();
            // Obtener saldo inicial
            CargarSaldoInicial();
            recargarDatosConCantidades(sender, e);

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

        private void verificarSiExisteCorteDeCaja()
        {
            using (DataTable dtVerificarSiTieneCorteDeCaja = cn.CargarDatos(cs.verificarSiTieneCorteDeCajaDesdeCaja(FormPrincipal.id_empleado)))
            {
                List<string> datos = new List<string>();
                List<string> datosCorteCaja = new List<string>();
                if (!dtVerificarSiTieneCorteDeCaja.Rows.Count.Equals(0))
                {
                    bool siEstaHechoCorteEnHistorialCorteDeCaja = true;
                    foreach (DataRow item in dtVerificarSiTieneCorteDeCaja.Rows)
                    {
                        siEstaHechoCorteEnHistorialCorteDeCaja = verificarHistorialCorteDeCaja(item["ID"].ToString());
                        if (siEstaHechoCorteEnHistorialCorteDeCaja.Equals(false))
                        {
                            datos.Add(item["ID"].ToString());
                            datos.Add(item["IDUsuario"].ToString());
                            datos.Add(item["IdEmpleado"].ToString());
                            var fechaOperacion = item["FechaOperacion"].ToString();
                            datos.Add(Convert.ToDateTime(fechaOperacion).ToString("yyyy-MM-dd HH:mm:ss"));
                            datos.Add(item["Efectivo"].ToString());
                            datos.Add(item["Tarjeta"].ToString());
                            datos.Add(item["Vales"].ToString());
                            datos.Add(item["Cheque"].ToString());
                            datos.Add(item["Transferencia"].ToString());
                            datos.Add(item["Credito"].ToString());
                            datos.Add(item["Anticipo"].ToString());
                            datos.Add(item["CantidadRetiradaCorte"].ToString());
                        }
                    }
                    if (siEstaHechoCorteEnHistorialCorteDeCaja.Equals(false))
                    {
                        cn.EjecutarConsulta(cs.guardarHistorialCorteDeCaja(datos.ToArray()));
                    }
                }
                else
                {
                    var fechaDeCorteDeCaja = string.Empty;
                    var ultimaIdDeCorteCaja = string.Empty;
                    var IdUsuario = string.Empty;
                    var IdEmpleado = string.Empty;

                    fechaDeCorteDeCaja = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    IdUsuario = FormPrincipal.userID.ToString();
                    IdEmpleado = FormPrincipal.id_empleado.ToString();

                    datosCorteCaja.Add(IdUsuario);
                    datosCorteCaja.Add(IdEmpleado);
                    datosCorteCaja.Add(fechaDeCorteDeCaja);
                    datosCorteCaja.Add("0");
                    datosCorteCaja.Add("0");
                    datosCorteCaja.Add("0");
                    datosCorteCaja.Add("0");
                    datosCorteCaja.Add("0");
                    datosCorteCaja.Add("0");
                    datosCorteCaja.Add("0");
                    datosCorteCaja.Add("0");
                    datosCorteCaja.Add("corte");

                    var resultadoEjecutarConsulta = cn.EjecutarConsulta(cs.registroInicialCorteDeCaja(datosCorteCaja.ToArray()));

                    if (resultadoEjecutarConsulta.Equals(1))
                    {
                        using (DataTable dtUltimaIdDeCorteDeCaja = cn.CargarDatos(cs.ultimaIdInsertadaDeCaja(FormPrincipal.id_empleado)))
                        {
                            if (!dtUltimaIdDeCorteDeCaja.Rows.Count.Equals(0))
                            {
                                foreach (DataRow item in dtUltimaIdDeCorteDeCaja.Rows)
                                {
                                    ultimaIdDeCorteCaja = item["ID"].ToString();
                                }
                            }
                        }

                        datos.Add(ultimaIdDeCorteCaja);
                        datos.Add(IdUsuario);
                        datos.Add(IdEmpleado);
                        datos.Add(fechaDeCorteDeCaja);
                        datos.Add("0");
                        datos.Add("0");
                        datos.Add("0");
                        datos.Add("0");
                        datos.Add("0");
                        datos.Add("0");
                        datos.Add("0");
                        datos.Add("0");

                        cn.EjecutarConsulta(cs.guardarHistorialCorteDeCaja(datos.ToArray()));
                    }
                }
            }
        }

        private bool verificarHistorialCorteDeCaja(string idDeCaja)
        {
            var siSeEncuentraRegistrado = true;

            using (DataTable dtHistorialCorteDeCaja = cn.CargarDatos(cs.corteHistorialCortesDeCaja(idDeCaja)))
            {
                if (dtHistorialCorteDeCaja.Rows.Count.Equals(0))
                {
                    siSeEncuentraRegistrado = false;
                }
            }

            return siSeEncuentraRegistrado;
        }

        private void recargarDatosConCantidades(object sender, EventArgs e)
        {
            limpiarVariablesParaTotales();
            CargarSaldoInicial();
            mostrarInformacionAbonos();
            mostrarInformacionAnticipos();
            if (!FormPrincipal.userNickName.Contains("@"))
            {
                cbFiltroAdminEmpleado_SelectedIndexChanged(sender, e);
                mostrarTotalEnCaja();
            }
            else
            {
                seccionEmpleadoCaja(FormPrincipal.id_empleado.ToString());
                mostrarTotalEnCaja();
            }
        }

        private void mostrarInformacionAnticipos()
        {
            
        }

        private void mostrarInformacionAbonos()
        {
            var idUltimoCorteDeCaja = ultimoCorteDeCaja;
            var idUsuarioEmpleado = string.Empty;

            if (opcionComboBoxFiltroAdminEmp.Equals("Admin"))
            {
                idUsuarioEmpleado = FormPrincipal.userID.ToString();

                if (!string.IsNullOrWhiteSpace(idUltimoCorteDeCaja))
                {
                    if (!string.IsNullOrWhiteSpace(fechaFormateadaCorteParaAbonos))
                    {
                        using (DataTable dtAbonos = cn.CargarDatos(cs.cargarAbonosDesdeUltimoCorteRealizadoAdministrador(idUsuarioEmpleado, fechaFormateadaCorteParaAbonos)))
                        {
                            if (!dtAbonos.Rows[0][0].Equals(DBNull.Value) && !dtAbonos.Rows.Count.Equals(0))
                            {
                                lbCambioAbonos.Visible = true;
                                foreach (DataRow item in dtAbonos.Rows)
                                {
                                    totalAbonoEfectivo = Convert.ToDecimal(item["Efectivo"].ToString());
                                    totalAbonoTarjeta = Convert.ToDecimal(item["Tarjeta"].ToString());
                                    totalAbonoVales = Convert.ToDecimal(item["Vales"].ToString());
                                    totalAbonoCheque = Convert.ToDecimal(item["Cheque"].ToString());
                                    totalAbonoTransferencia = Convert.ToDecimal(item["Transferencia"].ToString());
                                    totalAbonoRealizado = Convert.ToDecimal(item["Total"].ToString());
                                    lbTCreditoC.Text = totalAbonoRealizado.ToString("C2");
                                }
                            }
                            else
                            {
                                lbCambioAbonos.Visible = false;
                                limpirVariablesDeAbonos();
                                lbTCreditoC.Text = totalAbonoRealizado.ToString("C2");
                            }
                        }
                    }
                }
            }
            else if (opcionComboBoxFiltroAdminEmp.Equals("All"))
            {
                if (!string.IsNullOrWhiteSpace(idUltimoCorteDeCaja))
                {
                    if (!string.IsNullOrWhiteSpace(fechaFormateadaCorteParaAbonos))
                    {
                        using (DataTable dtAbonos = cn.CargarDatos(cs.AbonosCreditoDesdeUltimoCorteRealizadoTodos(fechaFormateadaCorteParaAbonos)))
                        {
                            if (!dtAbonos.Rows[0][0].Equals(DBNull.Value) && !dtAbonos.Rows.Count.Equals(0))
                            {
                                lbCambioAbonos.Visible = true;
                                foreach (DataRow item in dtAbonos.Rows)
                                {
                                    totalAbonoEfectivo = Convert.ToDecimal(item["Efectivo"].ToString());
                                    totalAbonoTarjeta = Convert.ToDecimal(item["Tarjeta"].ToString());
                                    totalAbonoVales = Convert.ToDecimal(item["Vales"].ToString());
                                    totalAbonoCheque = Convert.ToDecimal(item["Cheque"].ToString());
                                    totalAbonoTransferencia = Convert.ToDecimal(item["Transferencia"].ToString());
                                    totalAbonoRealizado = Convert.ToDecimal(item["Total"].ToString());
                                    lbTCreditoC.Text = totalAbonoRealizado.ToString("C2");
                                }
                            }
                            else
                            {
                                lbCambioAbonos.Visible = false;
                                limpirVariablesDeAbonos();
                                lbTCreditoC.Text = totalAbonoRealizado.ToString("C2");
                            }
                        }
                    }
                }
            }
            else
            {
                idUsuarioEmpleado = opcionComboBoxFiltroAdminEmp;

                if (!string.IsNullOrWhiteSpace(idUltimoCorteDeCaja))
                {
                    if (!string.IsNullOrWhiteSpace(fechaFormateadaCorteParaAbonos))
                    {
                        if (FormPrincipal.userNickName.Contains("@"))
                        {
                            idUsuarioEmpleado = FormPrincipal.id_empleado.ToString();
                        }

                        using (DataTable dtAbonos = cn.CargarDatos(cs.cargarAbonosDesdeUltimoCorteRealizadoEmpleado(idUsuarioEmpleado, fechaFormateadaCorteParaAbonos)))
                        {
                            if (!dtAbonos.Rows[0][0].Equals(DBNull.Value) && !dtAbonos.Rows.Count.Equals(0))
                            {
                                lbCambioAbonos.Visible = true;
                                foreach (DataRow item in dtAbonos.Rows)
                                {
                                    totalAbonoEfectivo = Convert.ToDecimal(item["Efectivo"].ToString());
                                    totalAbonoTarjeta = Convert.ToDecimal(item["Tarjeta"].ToString());
                                    totalAbonoVales = Convert.ToDecimal(item["Vales"].ToString());
                                    totalAbonoCheque = Convert.ToDecimal(item["Cheque"].ToString());
                                    totalAbonoTransferencia = Convert.ToDecimal(item["Transferencia"].ToString());
                                    totalAbonoRealizado = Convert.ToDecimal(item["Total"].ToString());
                                    lbTCreditoC.Text = totalAbonoRealizado.ToString("C2");
                                }
                            }
                            else
                            {
                                lbCambioAbonos.Visible = false;
                                limpirVariablesDeAbonos();
                                lbTCreditoC.Text = totalAbonoRealizado.ToString("C2");
                            }
                        }
                    }
                }
            }
        }

        private void limpirVariablesDeAbonos()
        {
            totalAbonoEfectivo = totalAbonoTarjeta = totalAbonoVales = totalAbonoCheque = totalAbonoTransferencia = totalAbonoRealizado = 0;
        }

        private void verComboBoxAdministradorEmpleado()
        {
            if (FormPrincipal.userNickName.Contains("@"))
            {
                cbFiltroAdminEmpleado.Visible = false;
            }
            else
            {
                cbFiltroAdminEmpleado.Visible = true;
                llenarComboBoxTipoDeEmpleado();
            }
        }

        private void llenarComboBoxTipoDeEmpleado()
        {
            Dictionary<string, string> tipoUsuario = new Dictionary<string, string>();
            tipoUsuario.Add("Admin", $"{FormPrincipal.userNickName} (ADMIN)");

            using (DataTable dtEmpleados = cn.CargarDatos(cs.obtenerEmpleados(FormPrincipal.userID)))
            {
                if (!dtEmpleados.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in dtEmpleados.Rows)
                    {
                        var nombreEmpleado = $"{item["nombre"].ToString()} (EMP)";
                        var idEmpleado = item["ID"].ToString();
                        tipoUsuario.Add(idEmpleado, nombreEmpleado);
                    }
                }
            }
            tipoUsuario.Add("All", "TODOS");

            cbFiltroAdminEmpleado.DataSource = tipoUsuario.ToArray();
            cbFiltroAdminEmpleado.DisplayMember = "Value";
            cbFiltroAdminEmpleado.ValueMember = "Key";

            cbFiltroAdminEmpleado.SelectedIndex = 0;
        }

        private void CargarSaldoInicial()
        {
            var tipodeMoneda = FormPrincipal.Moneda.Split('-');
            var moneda = tipodeMoneda[1].ToString().Trim().Replace("(", "").Replace(")", " ");

            //saldoInicial = mb.SaldoInicialCaja(FormPrincipal.userID);
            //saldoInicial = cdc.CargarSaldoInicial();

            if (!FormPrincipal.userNickName.Contains("@"))
            {
                clasificarTipoDeUsuario();

                if (opcionComboBoxFiltroAdminEmp.Equals("Admin"))
                {
                    using (DataTable dtHistorialCorteDeCaja = cn.CargarDatos(cs.cargarSaldoInicialAdministrador()))
                    {
                        if (!dtHistorialCorteDeCaja.Rows.Count.Equals(0))
                        {
                            lbSaldoInicialInfo.Visible = false;
                            foreach (DataRow item in dtHistorialCorteDeCaja.Rows)
                            {
                                fechaUltimoCorte = Convert.ToDateTime(item["Fecha"].ToString());
                                fechaFormateadaCorteParaAbonos = fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss");
                                ultimoCorteDeCaja = fechaFormateadaCorteParaAbonos;
                                cantidadEfectivoSaldoInicialEnCaja = cantidadTotalEfectivoEnCaja = Convert.ToDecimal(item["Efectivo"].ToString());
                                cantidadTarjetaSaldoInicialEnCaja = cantidadTotalTarjetaEnCaja = Convert.ToDecimal(item["Tarjeta"].ToString());
                                cantidadValesSaldoInicialEnCaja = cantidadTotalValesEnCaja = Convert.ToDecimal(item["Vales"].ToString());
                                cantidadChequeSaldoInicialEnCaja = cantidadTotalCehqueEnCaja = Convert.ToDecimal(item["Cheque"].ToString());
                                cantidadTransferenciaSaldoInicialEnCaja = cantidadTotalTransferenciaEnCaja = Convert.ToDecimal(item["Transferencia"].ToString());
                                idUltimoCorteDeCaja = item["IDCaja"].ToString();
                                saldoInicial = (float)Convert.ToDecimal(item["SaldoInicial"].ToString());
                                if (saldoInicial <= 0)
                                {
                                    lbSaldoInicialInfo.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            lbSaldoInicialInfo.Visible = false;
                            limpiarVariablesCantidadesDeCaja();
                        }
                    }
                }
                else if (opcionComboBoxFiltroAdminEmp.Equals("All"))
                {
                    List<int> IDsDeCroteDeCaja = new List<int>();
                    List<int> IDEmpleados = new List<int>();

                    using (DataTable dtIDsEpleados = cn.CargarDatos(cs.cargarIDsDeEmpleados()))
                    {
                        if (!dtIDsEpleados.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtIDsEpleados.Rows)
                            {
                                IDEmpleados.Add(Convert.ToInt32(item["ID"].ToString()));
                            }
                        }
                    }

                    var noEstaVacia = IsEmpty(IDEmpleados);

                    if (noEstaVacia)
                    {
                        var resultadoIDEmpleados = string.Join(",", IDEmpleados);

                        using (DataTable dtcargarNuevoSaldoInicial = cn.CargarDatos(cs.cargarNuevoSaldoInicial(resultadoIDEmpleados)))
                        {
                            if (!dtcargarNuevoSaldoInicial.Rows.Count.Equals(0))
                            {
                                foreach (DataRow item in dtcargarNuevoSaldoInicial.Rows)
                                {
                                    IDsDeCroteDeCaja.Add(Convert.ToInt32(item["IDCaja"].ToString()));
                                }
                            }
                        }

                        noEstaVacia = IsEmpty(IDsDeCroteDeCaja);

                        if (noEstaVacia)
                        {
                            using (DataTable dtResultadoConcentradooHistorialCorteDeCaja = cn.CargarDatos(cs.resultadoConcentradooHistorialCorteDeCaja(IDsDeCroteDeCaja.ToArray())))
                            {
                                if (!dtResultadoConcentradooHistorialCorteDeCaja.Rows.Count.Equals(0))
                                {
                                    lbSaldoInicialInfo.Visible = false;
                                    foreach (DataRow item in dtResultadoConcentradooHistorialCorteDeCaja.Rows)
                                    {
                                        cantidadEfectivoSaldoInicialEnCaja = cantidadTotalEfectivoEnCaja = Convert.ToDecimal(item["Efectivo"].ToString());
                                        cantidadTarjetaSaldoInicialEnCaja = cantidadTotalTarjetaEnCaja = Convert.ToDecimal(item["Tarjeta"].ToString());
                                        cantidadValesSaldoInicialEnCaja = cantidadTotalValesEnCaja = Convert.ToDecimal(item["Vales"].ToString());
                                        cantidadChequeSaldoInicialEnCaja = cantidadTotalCehqueEnCaja = Convert.ToDecimal(item["Cheque"].ToString());
                                        cantidadTransferenciaSaldoInicialEnCaja = cantidadTotalTransferenciaEnCaja = Convert.ToDecimal(item["Transferencia"].ToString());
                                        saldoInicial = (float)Convert.ToDecimal(item["SaldoInicial"].ToString());
                                        if (saldoInicial <= 0)
                                        {
                                            lbSaldoInicialInfo.Visible = false;
                                        }
                                    }
                                }
                                else
                                {
                                    lbSaldoInicialInfo.Visible = false;
                                    limpiarVariablesCantidadesDeCaja();
                                }
                            }
                        }
                    }
                    else
                    {
                        using (DataTable dtHistorialCorteDeCaja = cn.CargarDatos(cs.cargarSaldoInicialAdministrador()))
                        {
                            if (!dtHistorialCorteDeCaja.Rows.Count.Equals(0))
                            {
                                lbSaldoInicialInfo.Visible = false;
                                foreach (DataRow item in dtHistorialCorteDeCaja.Rows)
                                {
                                    fechaUltimoCorte = Convert.ToDateTime(item["Fecha"].ToString());
                                    fechaFormateadaCorteParaAbonos = fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss");
                                    ultimoCorteDeCaja = fechaFormateadaCorteParaAbonos;
                                    cantidadEfectivoSaldoInicialEnCaja = cantidadTotalEfectivoEnCaja = Convert.ToDecimal(item["Efectivo"].ToString());
                                    cantidadTarjetaSaldoInicialEnCaja = cantidadTotalTarjetaEnCaja = Convert.ToDecimal(item["Tarjeta"].ToString());
                                    cantidadValesSaldoInicialEnCaja = cantidadTotalValesEnCaja = Convert.ToDecimal(item["Vales"].ToString());
                                    cantidadChequeSaldoInicialEnCaja = cantidadTotalCehqueEnCaja = Convert.ToDecimal(item["Cheque"].ToString());
                                    cantidadTransferenciaSaldoInicialEnCaja = cantidadTotalTransferenciaEnCaja = Convert.ToDecimal(item["Transferencia"].ToString());
                                    idUltimoCorteDeCaja = item["IDCaja"].ToString();
                                    saldoInicial = (float)Convert.ToDecimal(item["SaldoInicial"].ToString());
                                    if (saldoInicial <= 0)
                                    {
                                        lbSaldoInicialInfo.Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                lbSaldoInicialInfo.Visible = false;
                                limpiarVariablesCantidadesDeCaja();
                            }
                        }
                    }
                }
                else
                {
                    using (DataTable dtHistorialCorteDeCaja = cn.CargarDatos(cs.cargarSaldoInicialEmpleado(opcionComboBoxFiltroAdminEmp)))
                    {
                        if (!dtHistorialCorteDeCaja.Rows.Count.Equals(0))
                        {
                            lbSaldoInicialInfo.Visible = false;
                            foreach (DataRow item in dtHistorialCorteDeCaja.Rows)
                            {
                                fechaUltimoCorte = Convert.ToDateTime(item["Fecha"].ToString());
                                fechaFormateadaCorteParaAbonos = fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss");
                                ultimoCorteDeCaja = fechaFormateadaCorteParaAbonos;
                                cantidadEfectivoSaldoInicialEnCaja = cantidadTotalEfectivoEnCaja = Convert.ToDecimal(item["Efectivo"].ToString());
                                cantidadTarjetaSaldoInicialEnCaja = cantidadTotalTarjetaEnCaja = Convert.ToDecimal(item["Tarjeta"].ToString());
                                cantidadValesSaldoInicialEnCaja = cantidadTotalValesEnCaja = Convert.ToDecimal(item["Vales"].ToString());
                                cantidadChequeSaldoInicialEnCaja = cantidadTotalCehqueEnCaja = Convert.ToDecimal(item["Cheque"].ToString());
                                cantidadTransferenciaSaldoInicialEnCaja = cantidadTotalTransferenciaEnCaja = Convert.ToDecimal(item["Transferencia"].ToString());
                                idUltimoCorteDeCaja = item["IDCaja"].ToString();
                                saldoInicial = (float)Convert.ToDecimal(item["SaldoInicial"].ToString());
                                if (saldoInicial <= 0)
                                {
                                    lbSaldoInicialInfo.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            lbSaldoInicialInfo.Visible = false;
                            limpiarVariablesCantidadesDeCaja();
                        }
                    }
                }
            }
            else
            {
                using (DataTable dtHistorialCorteDeCaja = cn.CargarDatos(cs.cargarSaldoInicialEmpleado(Convert.ToString(FormPrincipal.id_empleado))))
                {
                    if (!dtHistorialCorteDeCaja.Rows.Count.Equals(0))
                    {
                        lbSaldoInicialInfo.Visible = false;
                        foreach (DataRow item in dtHistorialCorteDeCaja.Rows)
                        {
                            fechaUltimoCorte = Convert.ToDateTime(item["Fecha"].ToString());
                            fechaFormateadaCorteParaAbonos = fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss");
                            ultimoCorteDeCaja = fechaFormateadaCorteParaAbonos;
                            cantidadEfectivoSaldoInicialEnCaja = cantidadTotalEfectivoEnCaja = Convert.ToDecimal(item["Efectivo"].ToString());
                            cantidadTarjetaSaldoInicialEnCaja = cantidadTotalTarjetaEnCaja = Convert.ToDecimal(item["Tarjeta"].ToString());
                            cantidadValesSaldoInicialEnCaja = cantidadTotalValesEnCaja = Convert.ToDecimal(item["Vales"].ToString());
                            cantidadChequeSaldoInicialEnCaja = cantidadTotalCehqueEnCaja = Convert.ToDecimal(item["Cheque"].ToString());
                            cantidadTransferenciaSaldoInicialEnCaja = cantidadTotalTransferenciaEnCaja = Convert.ToDecimal(item["Transferencia"].ToString());
                            idUltimoCorteDeCaja = item["IDCaja"].ToString();
                            saldoInicial = (float)Convert.ToDecimal(item["SaldoInicial"].ToString());
                            if (saldoInicial <= 0)
                            {
                                lbSaldoInicialInfo.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        lbSaldoInicialInfo.Visible = false;
                        limpiarVariablesCantidadesDeCaja();
                    }
                }
            }

            totalSaldoInicial = (decimal)saldoInicial;

            //tituloSeccion.Text = "SALDO INICIAL: \r\n" + moneda + cdc.CargarSaldoInicial().ToString("0.00");
            btnRedondoSaldoInicial.Text = "SALDO INICIAL: \r\n" + moneda + totalSaldoInicial /*cdc.CargarSaldoInicial().ToString("0.00")*/;
        }

        private bool IsEmpty(List<int> iDEmpleados)
        {
            var isEmpty = true;

            if (iDEmpleados.Equals(null) || iDEmpleados.Count.Equals(0))
            {
                isEmpty = false;
            }

            return isEmpty;
        }

        private void limpiarVariablesCantidadesDeCaja()
        {
            //fechaUltimoCorte = null;
            fechaFormateadaCorteParaAbonos = string.Empty;
            ultimoCorteDeCaja = string.Empty;
            cantidadTotalEfectivoEnCaja = 0;
            cantidadTotalTarjetaEnCaja = 0;
            cantidadTotalValesEnCaja = 0;
            cantidadTotalCehqueEnCaja = 0;
            cantidadTotalTransferenciaEnCaja = 0;
            idUltimoCorteDeCaja = string.Empty;
            saldoInicial = 0;
            cantidadEfectivoSaldoInicialEnCaja = 0;
            cantidadTarjetaSaldoInicialEnCaja = 0;
            cantidadValesSaldoInicialEnCaja = 0;
            cantidadChequeSaldoInicialEnCaja = 0;
            cantidadTransferenciaSaldoInicialEnCaja = 0;
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
                    //CargarSaldo();
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
                    //CargarSaldo();
                };

                retirar.Show();
            }
        }

        //private string correoUsuario()
        //{
        //    var dato = string.Empty;
        //    var consulta = cn.CargarDatos($"SELECT Email FROM Usuarios WHERE ID = '{FormPrincipal.userID}' AND Usuario = '{FormPrincipal.userNickName}'");
        //    if (!consulta.Rows.Count.Equals(0))
        //    {
        //        dato = consulta.Rows[0]["Email"].ToString();
        //    }

        //    return dato;
        //}



        private void btnCorteCaja_Click(object sender, EventArgs e)
        {
            corteCaja = 1;

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
                        tipoDeMovimiento = corte.operacion;

                        cn.EjecutarConsulta($"UPDATE Anticipos Set AnticipoAplicado = 0 WHERE IDUsuario = '{FormPrincipal.userID}'");

                        if (Utilidades.AdobeReaderInstalado())
                        {
                            GenerarReporte();
                            using (DataTable dtCerrarSesionDesdeCorteCaja = cn.CargarDatos(cs.validarCerrarSesionCorteCaja()))
                            {
                                if (!dtCerrarSesionDesdeCorteCaja.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow item in dtCerrarSesionDesdeCorteCaja.Rows)
                                    {
                                        if (item["CerrarSesionAuto"].ToString().Equals("1"))
                                        {
                                            recargarDatos = true;
                                            clickBotonCorteDeCaja = 1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Utilidades.MensajeAdobeReader();
                        }

                        botones = false;

                        var correo = mb.correoUsuario();
                        var correoCantidades = cargarDatosCorteCaja();

                        // Ejecutar hilo para enviar notificación
                        var datosConfig = mb.ComprobarConfiguracion();

                        if (datosConfig.Count > 0)
                        {
                            if (Convert.ToInt32(datosConfig[20]).Equals(1))
                            {
                                Thread mandarCorreo = new Thread(
                                    () => Utilidades.enviarCorreoCorteCaja(correo, correoCantidades, obtenerRutaPDF)
                                );
                                mandarCorreo.Start();
                            }
                        }
                    }

                    CargarSaldoInicial();
                    //CargarSaldo();

                    //var correo = mb.correoUsuario();
                    //var correoCantidades = cargarDatosCorteCaja();

                    //// Ejecutar hilo para enviar notificación
                    //var datosConfig = mb.ComprobarConfiguracion();

                    //if (datosConfig.Count > 0)
                    //{
                    //    if (Convert.ToInt32(datosConfig[20]).Equals(1))
                    //    {
                    //        Thread mandarCorreo = new Thread(
                    //            () => Utilidades.enviarCorreoCorteCaja(correo, correoCantidades, obtenerRutaPDF)
                    //        );
                    //        mandarCorreo.Start();
                    //    }
                    //} 

                    this.Refresh();
                    Application.DoEvents();
                };

                corte.Show();

                //GenerarTicket();
            }
            abonos = 0;

        }

        private void cerrarSesionEnCorteDeCaja()
        {
            if (tipoDeMovimiento.Equals(2))
            {
                FormPrincipal frmPrincipal = Application.OpenForms.OfType<FormPrincipal>().FirstOrDefault();

                if (frmPrincipal != null)
                {
                    frmPrincipal.desdeCorteDeCaja = true;
                    frmPrincipal.desdeDondeCerrarSesion();
                }
            }
        }

        //public void cerrarSesionCorte()
        //{
        //    FormPrincipal cerrarS = Application.OpenForms.OfType<FormPrincipal>().FirstOrDefault();
        //    if (cerrarS != null)
        //    {
        //        cerrarS.cerrarSesionCorteCaja();
        //    }
        //}

        private string[] cargarDatosCorteCaja()
        {
            List<string> lista = new List<string>();

            //Apartado Ventas
            lista.Add(lbTEfectivo.Text);//0
            lista.Add(lbTTarjeta.Text);
            lista.Add(lbTVales.Text);
            lista.Add(lbTCheque.Text);
            lista.Add(lbTTrans.Text);
            lista.Add(lbTCredito.Text);
            lista.Add(lbTCreditoC.Text);
            lista.Add(lbTAnticipos.Text);
            lista.Add(lbTVentas.Text);//8

            //Apartado Anticipos Recibidos
            lista.Add(lbTEfectivoA.Text);//9
            lista.Add(lbTTarjetaA.Text);
            lista.Add(lbTValesA.Text);
            lista.Add(lbTChequeA.Text);
            lista.Add(lbTTransA.Text);
            lista.Add(lbTAnticiposA.Text);//14

            //Apartado Dinero Agregado
            lista.Add(lbTEfectivoD.Text);//15
            lista.Add(lbTTarjetaD.Text);
            lista.Add(lbTValesD.Text);
            lista.Add(lbTChequeD.Text);
            lista.Add(lbTTransD.Text);
            lista.Add(lbTAgregado.Text);//20

            //Apartado Dinero Retirado
            lista.Add(lbEfectivoR.Text);//21
            lista.Add(lbTarjetaR.Text);//22
            lista.Add(lbValesR.Text);//23
            lista.Add(lbChequeR.Text);//24
            lista.Add(lbTransferenciaR.Text);//25
            lista.Add(lbTAnticiposC.Text);//26
            lista.Add(lbDevoluciones.Text);//27
            lista.Add(lbTRetirado.Text);//28

            //Apartado Total Caja
            lista.Add(lbTEfectivoC.Text);//29
            lista.Add(lbTTarjetaC.Text);
            lista.Add(lbTValesC.Text);
            lista.Add(lbTChequeC.Text);
            lista.Add(lbTTransC.Text);
            lista.Add(lbTSaldoInicial.Text);
            lista.Add(lbTCreditoTotal.Text);
            lista.Add(lbTTotalCaja.Text);//36


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

            var datos = cdc.CargarSaldo("Caja");

            datos[57] = datos[57] == null ? "0" : datos[57];
            datos[58] = datos[58] == null ? "0" : datos[58];
            datos[59] = datos[59] == null ? "0" : datos[59];
            datos[60] = datos[60] == null ? "0" : datos[60];
            datos[61] = datos[61] == null ? "0" : datos[61];
            datos[62] = datos[62] == null ? "0" : datos[62];

            //drUno.Close();

            //var consulta = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND FechaOperacion > '{fechaDefault.ToString("yyyy-MM-dd HH:mm:ss")}' ORDER BY FechaOperacion ASC";
            //consultaDos = new MySqlCommand(consulta, sql_con);
            //drDos = consultaDos.ExecuteReader();

            //// Variables ventas
            //float vEfectivo = 0f;
            //float vTarjeta = 0f;
            //float vVales = 0f;
            //float vCheque = 0f;
            //float vTrans = 0f;
            //float vCredito = 0f;
            //float vAnticipos = 0f;

            //// Variables anticipos
            //float aEfectivo = 0f;
            //float aTarjeta = 0f;
            //float aVales = 0f;
            //float aCheque = 0f;
            //float aTrans = 0f;

            //// Variables depositos
            //float dEfectivo = 0f;
            //float dTarjeta = 0f;
            //float dVales = 0f;
            //float dCheque = 0f;
            //float dTrans = 0f;

            //// Variables retiros
            //float rEfectivo = 0f;
            //float rTarjeta = 0f;
            //float rVales = 0f;
            //float rCheque = 0f;
            //float rTransferencia = 0f;
            //float devoluciones = 0f;

            //// Variables caja
            //float efectivo = 0f;
            //float tarjeta = 0f;
            //float vales = 0f;
            //float cheque = 0f;
            //float trans = 0f;
            ////float abono = 0f;////
            //float credito = 0f;
            //float subtotal = 0f;
            //float anticipos = 0f;

            //// Variable retiro
            //float dineroRetirado = 0f;
            //retiroEfectivo = 0f;
            //retiroTarjeta = 0f;
            //retiroVales = 0f;
            //retiroCheque = 0f;
            //retiroTrans = 0f;
            //retiroCredito = 0f;

            //int saltar = 0;

            ////Variables para anticipos y abonos
            //float anticiposAplicados = 0f;
            //float abonos = 0f;
            //float abonoEfectivoI = 0f;
            //float abonoTarjetaI = 0f;
            //float abonoValesI = 0f;
            //float abonoChequeI = 0f;
            //float abonoTransferenciaI = 0f;

            //var consultaAnticipoAplicado = ""; //Se agrego esta linea desde esta linea...
            //string ultimoDate = "";
            //try
            //{
            //    var segundaConsulta = cn.CargarDatos($"SELECT sum(AnticipoAplicado) AS AnticipoAplicado FROM Anticipos  WHERE IDUsuario = '{FormPrincipal.userID}'");
            //    if (!segundaConsulta.Rows.Count.Equals(0) /*&& !string.IsNullOrWhiteSpace(segundaConsulta.ToString())*/)
            //    {
            //        foreach (DataRow obtenerAnticipoAplicado in segundaConsulta.Rows)
            //        {
            //            if (string.IsNullOrWhiteSpace(obtenerAnticipoAplicado["AnticipoAplicado"].ToString()))
            //            {
            //                consultaAnticipoAplicado = "0";
            //            }
            //            else
            //            {
            //                consultaAnticipoAplicado = obtenerAnticipoAplicado["AnticipoAplicado"].ToString();
            //            }
            //        }
            //        anticiposAplicados = float.Parse(consultaAnticipoAplicado); //Hasta esta linea.
            //    }
            //    //Obtenemos la fecha del ultimo corte
            //    var fechaCorteUltima = cn.CargarDatos($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");
            //    if (!fechaCorteUltima.Rows.Count.Equals(0))
            //    {
            //        //if (!fechaCorteUltima.Rows.Count.Equals(0)) {  }

            //        foreach (DataRow fechaUltimoCorte in fechaCorteUltima.Rows)
            //        {
            //            ultimoDate = fechaUltimoCorte["FechaOperacion"].ToString();
            //        }
            //        DateTime ultimoDateCorte = DateTime.Parse(ultimoDate);

            //        //Obtenemos la cantidad de abonos realizados despues del ultimo corte de caja
            //        var abonoEfectivo = ""; var abonoTarjeta = ""; var abonoVales = ""; var abonoCheque = ""; var abonoTransferencia = "";
            //        using (var fechaMovimientos = cn.CargarDatos($"SELECT sum(Total)AS Total, sum(Efectivo)AS Efectivo, sum(Tarjeta)AS Tarjeta, sum(Vales)AS Vales, sum(Cheque)AS Cheque, sum(Transferencia)AS Transferencia FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{ultimoDateCorte.ToString("yyyy-MM-dd HH:mm:ss")}'"))
            //        {
            //            //var abono = "";
            //            if (!fechaMovimientos.Rows.Count.Equals(0) /*&& !string.IsNullOrWhiteSpace(fechaMovimientos.ToString())*/)
            //            {

            //                foreach (DataRow cantidadAbono in fechaMovimientos.Rows)
            //                {
            //                    if (string.IsNullOrWhiteSpace(cantidadAbono["Efectivo"].ToString()))
            //                    {
            //                        abonoEfectivo = "0";
            //                    }
            //                    else
            //                    {
            //                        abonoEfectivo = cantidadAbono["Efectivo"].ToString();
            //                    }

            //                    if (string.IsNullOrWhiteSpace(cantidadAbono["Tarjeta"].ToString()))
            //                    {
            //                        abonoTarjeta = "0";
            //                    }
            //                    else
            //                    {
            //                        abonoTarjeta = cantidadAbono["Tarjeta"].ToString();
            //                    }

            //                    if (string.IsNullOrWhiteSpace(cantidadAbono["Vales"].ToString()))
            //                    {
            //                        abonoVales = "0";
            //                    }
            //                    else
            //                    {
            //                        abonoVales = cantidadAbono["Vales"].ToString();
            //                    }

            //                    if (string.IsNullOrWhiteSpace(cantidadAbono["Cheque"].ToString()))
            //                    {
            //                        abonoCheque = "0";
            //                    }
            //                    else
            //                    {
            //                        abonoCheque = cantidadAbono["Cheque"].ToString();
            //                    }

            //                    if (string.IsNullOrWhiteSpace(cantidadAbono["Transferencia"].ToString()))
            //                    {
            //                        abonoTransferencia = "0";
            //                    }
            //                    else
            //                    {
            //                        abonoTransferencia = cantidadAbono["Transferencia"].ToString();
            //                    }

            //                }
            //                //abonos = float.Parse(abono);
            //                abonoEfectivoI = float.Parse(abonoEfectivo);
            //                abonoTarjetaI = float.Parse(abonoTarjeta);
            //                abonoValesI = float.Parse(abonoVales);
            //                abonoChequeI = float.Parse(abonoCheque);
            //                abonoTransferenciaI = float.Parse(abonoTransferencia);
            //                abonos = (abonoEfectivoI + abonoTarjetaI + abonoValesI + abonoChequeI + abonoTransferenciaI);
            //            }
            //        }



            //        //Obtenemos la cantidad de Devoluciones realizados despues del ultimo corte de caja
            //        using (var obtenerDevoluciones = cn.CargarDatos($@"SELECT sum(Total)AS Total, sum(Efectivo)AS Efectivo, sum(Tarjeta)AS Tarjeta, sum(Vales)AS Vales, sum(Cheque)AS Cheque, sum(Transferencia)AS Transferencia FROM Devoluciones WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{ultimoDateCorte.ToString("yyyy-MM-dd HH:mm:ss")}'"))
            //        {
            //            if (!obtenerDevoluciones.Rows.Count.Equals(0) /*&& !string.IsNullOrWhiteSpace(obtenerDevoluciones.ToString())*/)
            //            {
            //                string devolucionTotal = string.Empty, devolucionEfectivo = string.Empty, devolucionTarjeta = string.Empty, devolucionVales = string.Empty, devolucionCheque = string.Empty, devolucionTrans = string.Empty;
            //                foreach (DataRow devol in obtenerDevoluciones.Rows)
            //                {
            //                    if (string.IsNullOrWhiteSpace(devol["Total"].ToString()))
            //                    {
            //                        devolucionTotal = "0";
            //                        devolucionEfectivo = "0";
            //                        devolucionTarjeta = "0";
            //                        devolucionVales = "0";
            //                        devolucionCheque = "0";
            //                        devolucionTrans = "0";

            //                    }
            //                    else
            //                    {
            //                        devolucionTotal = devol["Total"].ToString();
            //                        devolucionEfectivo = devol["Efectivo"].ToString();
            //                        devolucionTarjeta = devol["Tarjeta"].ToString();
            //                        devolucionVales = devol["Vales"].ToString();
            //                        devolucionCheque = devol["Cheque"].ToString();
            //                        devolucionTrans = devol["Transferencia"].ToString();
            //                    }
            //                }

            //                devoluciones = float.Parse(devolucionTotal);

            //                efectivoCorte = devolucionEfectivo.ToString();
            //                tarjetaCorte = devolucionTarjeta.ToString();
            //                valesCorte = devolucionVales.ToString();
            //                chequeCorte = devolucionCheque.ToString();
            //                transCorte = devolucionTrans.ToString();
            //                totCorte = devoluciones.ToString();

            efectivoCorte = datos[57].ToString();
            tarjetaCorte = datos[58].ToString();
            valesCorte = datos[59].ToString();
            chequeCorte = datos[60].ToString();
            transCorte = datos[61].ToString();
            totCorte = datos[62].ToString();
            //            }
            //        }


            //    }
            //}
            //catch
            //{

            //}

            //while (drDos.Read())
            //{
            //    string operacion = drDos.GetValue(drDos.GetOrdinal("Operacion")).ToString();
            //    var auxiliar = Convert.ToDateTime(drDos.GetValue(drDos.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
            //    var fechaOperacion = Convert.ToDateTime(auxiliar);

            //    if (operacion == "venta" && fechaOperacion > fechaDefault)
            //    {
            //        if (saltar == 0 && !fechaDefault.ToString("yyyy-MM-dd HH:mm:ss").Equals("0001-01-01 00:00:00"))
            //        {
            //            saltar++;
            //            continue;
            //        }

            //        vEfectivo += (float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString())/*+MetodosBusquedas.efectivoInicial*/);
            //        vTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
            //        vVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
            //        vCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
            //        vTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
            //        vCredito += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());
            //        vAnticipos += float.Parse(drDos.GetValue(drDos.GetOrdinal("Anticipo")).ToString());
            //    }

            //    if (operacion == "anticipo" && fechaOperacion > fechaDefault)
            //    {
            //        aEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
            //        aTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
            //        aVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
            //        aCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
            //        aTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
            //    }

            //    if (operacion == "deposito" && fechaOperacion > fechaDefault)
            //    {
            //        dEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
            //        dTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
            //        dVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
            //        dCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
            //        dTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
            //    }

            //    if (operacion == "retiro" && fechaOperacion > fechaDefault)
            //    {
            //        dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
            //        retiroEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());

            //        dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
            //        retiroTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());

            //        dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
            //        retiroVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());

            //        dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
            //        retiroCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());

            //        dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
            //        retiroTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());

            //        //dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());
            //        retiroCredito += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());
            //    }
            //}

            //// Cerramos la conexion y el datareader
            ////drUno.Close();
            //drDos.Close();
            //sql_con.Close();

            //var tipodeMoneda = FormPrincipal.Moneda.Split('-');
            //var moneda = tipodeMoneda[1].ToString().Trim().Replace("(", "").Replace(")", " ");

            ////var datos = cdc.CargarSaldo("Caja");

            //var credi = (vCredito - retiroCredito);
            //if (credi < 0) { credi = 0; }

            var tipodeMoneda = FormPrincipal.Moneda.Split('-');
            var moneda = tipodeMoneda[1].ToString().Trim().Replace("(", "").Replace(")", " ");

            //// Apartado VENTAS
            lbTEfectivo.Text = moneda + float.Parse(datos[0]).ToString("0.00");
            lbTTarjeta.Text = moneda + float.Parse(datos[1]).ToString("0.00");
            lbTVales.Text = moneda + float.Parse(datos[2]).ToString("0.00");
            lbTCheque.Text = moneda + float.Parse(datos[3]).ToString("0.00");
            lbTTrans.Text = moneda + float.Parse(datos[4]).ToString("0.00");
            lbTCredito.Text = moneda + float.Parse(datos[5]).ToString("0.00");
            //lbTAnticipos.Text = "$" + vAnticipos.ToString("0.00");
            lbTAnticipos.Text = moneda + float.Parse(datos[6]).ToString("0.00");
            lbTVentas.Text = moneda + float.Parse(datos[7]).ToString("0.00");


            ////Variables de Abonos en Ventas
            ////lbEfectivoAbonos.Text = "$" + abonoEfectivoI.ToString("0.00");
            ////lbTarjetaAbonos.Text = "$" + abonoTarjetaI.ToString("0.00");
            ////lbValesAbonos.Text = "$" + abonoValesI.ToString("0.00");
            ////lbChequeAbonos.Text = "$" + abonoChequeI.ToString("0.00");
            ////lbTransferenciaAbonos.Text = "$" + abonoTransferenciaI.ToString("0.00");
            //lbTCreditoC.Text = moneda + abonos/*(abonoEfectivoI + abonoTarjetaI + abonoValesI + abonoChequeI + abonoTransferenciaI)*/.ToString("0.00");
            lbTCreditoC.Text = moneda + float.Parse(datos[13]).ToString("0.00");

            ////lbTotalAbonos.Text = "$" + abonoEfectivoI.ToString("0.00");

            ////// Apartado ANTICIPOS RECIBIDOS
            ////lbTEfectivoA.Text = moneda + aEfectivo.ToString("0.00");
            ////lbTTarjetaA.Text = moneda + aTarjeta.ToString("0.00");
            ////lbTValesA.Text = moneda + aVales.ToString("0.00");
            ////lbTChequeA.Text = moneda + aCheque.ToString("0.00");
            ////lbTTransA.Text = moneda + aTrans.ToString("0.00");
            ////lbTAnticiposA.Text = moneda + (aEfectivo + aTarjeta + aVales + aCheque + aTrans).ToString("0.00");
            lbTEfectivoA.Text = moneda + float.Parse(datos[14]).ToString("0.00");
            lbTTarjetaA.Text = moneda + float.Parse(datos[15]).ToString("0.00");
            lbTValesA.Text = moneda + float.Parse(datos[16]).ToString("0.00");
            lbTChequeA.Text = moneda + float.Parse(datos[17]).ToString("0.00");
            lbTTransA.Text = moneda + float.Parse(datos[18]).ToString("0.00");
            lbTAnticiposA.Text = moneda + float.Parse(datos[19]).ToString("0.00");


            ////// Apartado DINERO AGREGADO
            ////lbTEfectivoD.Text = moneda + dEfectivo.ToString("0.00");
            ////lbTTarjetaD.Text = moneda + dTarjeta.ToString("0.00");
            ////lbTValesD.Text = moneda + dVales.ToString("0.00");
            ////lbTChequeD.Text = moneda + dCheque.ToString("0.00");
            ////lbTTransD.Text = moneda + dTrans.ToString("0.00");
            ////lbTAgregado.Text = moneda + (dEfectivo + dTarjeta + dVales + dCheque + dTrans).ToString("0.00");
            lbTEfectivoD.Text = moneda + float.Parse(datos[20]).ToString("0.00");
            lbTTarjetaD.Text = moneda + float.Parse(datos[21]).ToString("0.00");
            lbTValesD.Text = moneda + float.Parse(datos[22]).ToString("0.00");
            lbTChequeD.Text = moneda + float.Parse(datos[23]).ToString("0.00");
            lbTTransD.Text = moneda + float.Parse(datos[24]).ToString("0.00");
            lbTAgregado.Text = moneda + float.Parse(datos[25]).ToString("0.00");

            // Apartado Dinero Retirado
            ////lbEfectivoR.Text = moneda + " -" + retiroEfectivo.ToString("0.00");
            ////lbTarjetaR.Text = moneda + " -" + retiroTarjeta.ToString("0.00");
            ////lbValesR.Text = moneda + " -" + retiroVales.ToString("0.00");
            ////lbChequeR.Text = moneda + " -" + retiroCheque.ToString("0.00");
            ////lbTransferenciaR.Text = moneda + " -" + retiroTrans.ToString("0.00");
            //////lbTAnticiposC.Text = "$ -" + vAnticipos.ToString("0.00");
            ////lbTAnticiposC.Text = moneda + " -" + anticiposAplicados.ToString("0.00");
            ////lbDevoluciones.Text = moneda + " -" + devoluciones.ToString("0.00");
            ////lbTRetirado.Text = moneda + " -" + (retiroEfectivo + retiroTarjeta + retiroVales + retiroCheque + retiroTrans + /*vAnticipos*/anticiposAplicados + devoluciones).ToString("0.00");
            lbEfectivoR.Text = moneda + " -" + float.Parse(datos[26]).ToString("0.00");
            lbTarjetaR.Text = moneda + " -" + float.Parse(datos[27]).ToString("0.00");
            lbValesR.Text = moneda + " -" + float.Parse(datos[28]).ToString("0.00");
            lbChequeR.Text = moneda + " -" + float.Parse(datos[29]).ToString("0.00");
            lbTransferenciaR.Text = moneda + " -" + float.Parse(datos[30]).ToString("0.00");
            //lbTAnticiposC.Text = "$ -" + vAnticipos.ToString("0.00");
            lbTAnticiposC.Text = moneda + " -" + float.Parse(datos[31]).ToString("0.00");
            lbDevoluciones.Text = moneda + " -" + float.Parse(datos[32]).ToString("0.00");
            lbTRetirado.Text = moneda + " -" + float.Parse(datos[33]).ToString("0.00");

            //// Apartado TOTAL EN CAJA
            //efectivo = (vEfectivo + aEfectivo + dEfectivo + abonoEfectivoI) - rEfectivo; if (efectivo < 0) { efectivo = 0; }
            //tarjeta = (vTarjeta + aTarjeta + dTarjeta + abonoTarjetaI) - rTarjeta; if (tarjeta < 0) { tarjeta = 0; }
            //vales = (vVales + aVales + dVales + abonoValesI) - rVales; if (vales < 0) { vales = 0; }
            //cheque = (vCheque + aCheque + dCheque + abonoChequeI) - rCheque; if (cheque < 0) { cheque = 0; }
            //trans = (vTrans + aTrans + dTrans + abonoTransferenciaI) - rTransferencia; if (trans < 0) { trans = 0; }
            //credito = vCredito;
            ////anticipos = vAnticipos;
            //anticipos = anticiposAplicados;
            //subtotal = (efectivo + tarjeta + vales + cheque + trans /*+ credito*//*+ abonos*/ + saldoInicial /*+ vCredito*/)/* - devoluciones*/; if (subtotal < 0) { subtotal = 0; }


            var totalF = (efectivo - retiroEfectivo); if (totalF < 0) { totalF = 0; }
            var totalTa = (tarjeta - retiroTarjeta); if (totalTa < 0) { totalTa = 0; }
            var totalV = (vales - retiroVales); if (totalV < 0) { totalV = 0; }
            var totalC = (cheque - retiroCheque); if (totalC < 0) { totalC = 0; }
            var totalTr = (trans - retiroTrans); if (totalTr < 0) { totalTr = 0; }

            lbTEfectivoC.Text = moneda + (totalF).ToString("0.00");
            lbTTarjetaC.Text = moneda + (totalTa).ToString("0.00");
            lbTValesC.Text = moneda + (totalV).ToString("0.00");
            lbTChequeC.Text = moneda + (totalC).ToString("0.00");
            lbTTransC.Text = moneda + (totalTr).ToString("0.00");
            //lbTCreditoC.Text = "$" + /*credito*/abonos.ToString("0.00");   // lbTCreditoC Esta etiqueta es la de Abonos---------------------------------
            //lbTAnticiposC.Text = "$" + anticipos.ToString("0.00"); 
            ////lbTSaldoInicial.Text = moneda + saldoInicial.ToString("0.00");
            ////if (credito < retiroCredito) { lbTCreditoTotal.Text = "$" + "0.00"; } else { lbTCreditoTotal.Text = moneda + (vCredito - retiroCredito).ToString("0.00"); }
            ////lbTSubtotal.Text = "$" + subtotal.ToString("0.00");
            ////lbTDineroRetirado.Text = "$" + dineroRetirado.ToString("0.00");
            lbTTotalCaja.Text = moneda + float.Parse(datos[50]).ToString("0.00");
            lbTEfectivoC.Text = moneda + float.Parse(datos[42]).ToString("0.00");
            lbTTarjetaC.Text = moneda + float.Parse(datos[43]).ToString("0.00");
            lbTValesC.Text = moneda + float.Parse(datos[44]).ToString("0.00");
            lbTChequeC.Text = moneda + float.Parse(datos[45]).ToString("0.00");
            lbTTransC.Text = moneda + float.Parse(datos[46]).ToString("0.00");

            //efectivo = float.Parse(datos[42]);
            //tarjeta = float.Parse(datos[43]);
            //vales = float.Parse(datos[44]);
            //cheque = float.Parse(datos[45]);
            //trans = float.Parse(datos[46]);

            ////lbTTotalCaja.Text = moneda + subtotal.ToString("0.00");
            ////lbTEfectivoC.Text = moneda + totalF.ToString("0.00");
            ////lbTTarjetaC.Text = moneda + totalTa.ToString("0.00");
            ////lbTValesC.Text = moneda + totalV.ToString("0.00");
            ////lbTChequeC.Text = moneda + totalC.ToString("0.00");
            ////lbTTransC.Text = moneda + totalTr.ToString("0.00");
            ////lbTCreditoC.Text = "$" + /*credito*/abonos.ToString("0.00");   // lbTCreditoC Esta etiqueta es la de Abonos---------------------------------
            ////lbTAnticiposC.Text = "$" + anticipos.ToString("0.00");
            lbTSaldoInicial.Text = moneda + float.Parse(datos[48]).ToString("0.00");
            if (float.Parse(datos[39])/*credito*/ < retiroCredito) { lbTCreditoTotal.Text = "$" + "0.00"; }
            else
            {
                lbTCreditoTotal.Text = moneda + float.Parse(datos[5]).ToString("0.00");
                ////lbTCreditoTotal.Text = moneda + (vCredito - retiroCredito).ToString("0.00");
                //////lbTCreditoTotal.Text = "$" + float.Parse(datos[49]).ToString("0.00");
                ////lbTSubtotal.Text = "$" + subtotal.ToString("0.00");
                ////lbTDineroRetirado.Text = "$" + dineroRetirado.ToString("0.00");
                //////lbTTotalCaja.Text = moneda + float.Parse(datos[50]).ToString("0.00");

                // Variables de totales, ayuda a retirar dinero
                ////totalEfectivo = efectivo - retiroEfectivo;
                ////totalTarjeta = tarjeta - retiroTarjeta;
                ////totalVales = vales - retiroVales;
                ////totalCheque = cheque - retiroCheque;
                ////totalTransferencia = trans - retiroTrans;
                ////totalCredito = credito - retiroCredito;
                totalEfectivo = float.Parse(datos[51]);
                totalTarjeta = float.Parse(datos[52]);
                totalVales = float.Parse(datos[53]);
                totalCheque = float.Parse(datos[54]);
                totalTransferencia = float.Parse(datos[55]);
                totalCredito = float.Parse(datos[56]);

                totalEfectivoAbono = float.Parse(datos[57]);
                totalTarjetaAbono = float.Parse(datos[58]);
                totalValesAbono = float.Parse(datos[59]);
                totalChequeAbono = float.Parse(datos[60]);
                totalTransferenciaAbono = float.Parse(datos[61]);

                verificarCantidadAbonos();
            }
        }
        #endregion

        private void CajaN_Paint(object sender, PaintEventArgs e)
        {
            if (recargarDatos)
            {
                verComboBoxAdministradorEmpleado();

                limpiarVariablesParaTotales();
                CargarSaldoInicial();
                mostrarInformacionAbonos();
                if (!FormPrincipal.userNickName.Contains("@"))
                {
                    cbFiltroAdminEmpleado_SelectedIndexChanged(sender, e);
                    mostrarTotalEnCaja();
                }
                else
                {
                    seccionEmpleadoCaja(FormPrincipal.id_empleado.ToString());
                    mostrarTotalEnCaja();
                }

                //CargarSaldo();
                recargarDatos = false;

                if (clickBotonCorteDeCaja.Equals(1))
                {
                    FormPrincipal frmPrincipal = Application.OpenForms.OfType<FormPrincipal>().FirstOrDefault();

                    if (frmPrincipal != null)
                    {
                        if (frmPrincipal.Controls.Count > 0)
                        {
                            foreach (Control item in frmPrincipal.Controls)
                            {
                                if (item.Name.Equals("panelMaestro"))
                                {
                                    foreach (Control itemSubControl in item.Controls)
                                    {
                                        if (itemSubControl.Name.Equals("panelContenedor"))
                                        {
                                            foreach (Control itemSubControlHijo in itemSubControl.Controls)
                                            {
                                                var nombreDeForma = itemSubControlHijo.Name.ToString();
                                                if (nombreDeForma.Equals("CajaN"))
                                                {
                                                    clickBotonCorteDeCaja = 0;
                                                    cerrarSesionEnCorteDeCaja();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
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

        private void generarNuevoReporte()
        {
            #region variables
            var datos = FormPrincipal.datosUsuario;
            var colorFuenteNegrita = new BaseColor(Color.Black);
            var colorFuenteBlanca = new BaseColor(Color.White);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, 1, colorFuenteNegrita);
            var fuenteNegritaDepositoRetiro = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, 1, colorFuenteNegrita);
            var fuenteTituloHistorialDepositoRetiro = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, colorFuenteBlanca);

            // Ruta donde se creara el archivo PDF
            var rutaArchivo = string.Empty;
            var servidor = Properties.Settings.Default.Hosting;

            var UsuarioActivo = cs.validarEmpleado(FormPrincipal.userNickName, true);
            var obtenerUsuarioPrincipal = cs.validarEmpleadoPorID();

            var cantidadRetiradaAlCorteDeCaja = 0m;
            var cantidadEnCajaAntesDelCorteDeCaja = 0m;
            var idUltimoCorteDeCaja = 0;

            float[] anchoColumnasTablasDepositosRetiros = new float[] { 200f, 120f, 120f, 120f, 120f, 120f, 180f, 180f };
            #endregion

            #region Ruta para el archivo de PDF
            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\Caja\reporte_corte_" + fechaUltimoCorte.ToString("yyyyMMddHHmmss") + ".pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\Caja\reporte_corte_" + fechaUltimoCorte.ToString("yyyyMMddHHmmss") + ".pdf";
            }
            obtenerRutaPDF = rutaArchivo;
            #endregion

            Paragraph Usuario = new Paragraph();
            Paragraph Empleado = new Paragraph();
            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            #region Encabezado del reporte
            Usuario = new Paragraph($"USUARIO: ADMIN ({obtenerUsuarioPrincipal})", fuenteNegrita);
            if (!string.IsNullOrEmpty(UsuarioActivo))
            {
                Empleado = new Paragraph($"EMPLEADO: {UsuarioActivo}", fuenteNegrita);
            }

            var numFolio = obtenerFolioCorte();

            Document reporte = new Document(PageSize.A3);
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));
            Paragraph NumeroFolio = new Paragraph("No. Folio: " + numFolio, fuenteGrande);

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
            Paragraph subTitulo = new Paragraph("CORTE DE CAJA\nFecha: " + fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            NumeroFolio.Alignment = Element.ALIGN_CENTER;
            if (!string.IsNullOrEmpty(UsuarioActivo))
            {
                Empleado.Alignment = Element.ALIGN_CENTER;
            }

            reporte.Add(titulo);
            reporte.Add(Usuario);
            if (!string.IsNullOrEmpty(UsuarioActivo))
            {
                reporte.Add(Empleado);
            }
            reporte.Add(NumeroFolio);
            reporte.Add(subTitulo);
            #endregion

            #region Contenido Valores de Caja
            float[] anchoColumnasContenido = new float[] { 200f, 200f, 200f, 200f, 200f };

            PdfPTable tablaContenido = new PdfPTable(5);
            tablaContenido.WidthPercentage = 100;
            tablaContenido.SetWidths(anchoColumnasContenido);

            float[] anchoColumnasTablas = new float[] { 120f, 110f };

            /************************************
            *                                   * 
            *   Taabla con los valores de la    * 
            *   sección de Ventas de Caja       *
            *                                   *
            ************************************/
            #region Tabla de Ventas
            PdfPTable tablaVentas = new PdfPTable(2);
            tablaVentas.WidthPercentage = 100;
            tablaVentas.SetWidths(anchoColumnasTablas);

            // Encabezado de La Tabla
            #region Encabezado Ventas
            PdfPCell columnaTituloVentas = new PdfPCell(new Phrase(tituloVentas.Text, fuenteNegrita));
            columnaTituloVentas.Colspan = 2;
            columnaTituloVentas.BorderWidth = 0;
            columnaTituloVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            columnaTituloVentas.Padding = 3;
            tablaVentas.AddCell(columnaTituloVentas);
            #endregion

            // Conceptos de la Tabla
            #region Concepto Efectivo
            PdfPCell columnaConceptoEfectivo = new PdfPCell(new Phrase(lbEfectivo.Text, fuenteNormal));
            columnaConceptoEfectivo.BorderWidth = 0;
            columnaConceptoEfectivo.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoEfectivo.Padding = 3;

            PdfPCell columnaCantidadEfectivo = new PdfPCell(new Phrase(lbTEfectivo.Text, fuenteNormal));
            columnaCantidadEfectivo.BorderWidth = 0;
            columnaCantidadEfectivo.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadEfectivo.Padding = 3;
            #endregion

            #region Concepto Tarjeta
            PdfPCell columnaConceptoTarjeta = new PdfPCell(new Phrase(lbTarjeta.Text, fuenteNormal));
            columnaConceptoTarjeta.BorderWidth = 0;
            columnaConceptoTarjeta.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoTarjeta.Padding = 3;

            PdfPCell columnaCantidadTarjeta = new PdfPCell(new Phrase(lbTTarjeta.Text, fuenteNormal));
            columnaCantidadTarjeta.BorderWidth = 0;
            columnaCantidadTarjeta.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadTarjeta.Padding = 3;
            #endregion

            #region Concepto Vales
            PdfPCell columnaConceptoVales = new PdfPCell(new Phrase(lbVales.Text, fuenteNormal));
            columnaConceptoVales.BorderWidth = 0;
            columnaConceptoVales.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoVales.Padding = 3;

            PdfPCell columnaCantidadVales = new PdfPCell(new Phrase(lbTVales.Text, fuenteNormal));
            columnaCantidadVales.BorderWidth = 0;
            columnaCantidadVales.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadVales.Padding = 3;
            #endregion

            #region Concepto Cheques
            PdfPCell columnaConceptoCheques = new PdfPCell(new Phrase(lbCheque.Text, fuenteNormal));
            columnaConceptoCheques.BorderWidth = 0;
            columnaConceptoCheques.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoCheques.Padding = 3;

            PdfPCell columnaCantidadCheques = new PdfPCell(new Phrase(lbTCheque.Text, fuenteNormal));
            columnaCantidadCheques.BorderWidth = 0;
            columnaCantidadCheques.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadCheques.Padding = 3;
            #endregion

            #region Concepto Transferencias
            PdfPCell columnaConceptoTransferencias = new PdfPCell(new Phrase(lbTrans.Text, fuenteNormal));
            columnaConceptoTransferencias.BorderWidth = 0;
            columnaConceptoTransferencias.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoTransferencias.Padding = 3;

            PdfPCell columnaCantidadTransferencias = new PdfPCell(new Phrase(lbTTrans.Text, fuenteNormal));
            columnaCantidadTransferencias.BorderWidth = 0;
            columnaCantidadTransferencias.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadTransferencias.Padding = 3;
            #endregion

            #region Concepto Crédito
            PdfPCell columnaConceptoCredito = new PdfPCell(new Phrase(lbCredito.Text, fuenteNormal));
            columnaConceptoCredito.BorderWidth = 0;
            columnaConceptoCredito.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoCredito.Padding = 3;

            PdfPCell columnaCantidadCredito = new PdfPCell(new Phrase(lbTCredito.Text, fuenteNormal));
            columnaCantidadCredito.BorderWidth = 0;
            columnaCantidadCredito.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadCredito.Padding = 3;
            #endregion

            #region Concepto Abonos
            PdfPCell columnaConceptoAbonos = new PdfPCell(new Phrase(lbCreditoC.Text, fuenteNormal));
            columnaConceptoAbonos.BorderWidth = 0;
            columnaConceptoAbonos.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoAbonos.Padding = 3;

            PdfPCell columnaCantidadAbonos = new PdfPCell(new Phrase(lbTCreditoC.Text, fuenteNormal));
            columnaCantidadAbonos.BorderWidth = 0;
            columnaCantidadAbonos.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadAbonos.Padding = 3;
            #endregion

            #region Concepto Anticipos Utilizados
            PdfPCell columnaConceptoAnticiposUtilizados = new PdfPCell(new Phrase(lbAnticipos.Text, fuenteNormal));
            columnaConceptoAnticiposUtilizados.BorderWidth = 0;
            columnaConceptoAnticiposUtilizados.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoAnticiposUtilizados.Padding = 3;

            PdfPCell columnaCantidadAnticiposUtilizados = new PdfPCell(new Phrase(lbTCreditoC.Text, fuenteNormal));
            columnaCantidadAnticiposUtilizados.BorderWidth = 0;
            columnaCantidadAnticiposUtilizados.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadAnticiposUtilizados.Padding = 3;
            #endregion

            #region Agregar Conceptos A Tabla Ventas
            tablaVentas.AddCell(columnaConceptoEfectivo);
            tablaVentas.AddCell(columnaCantidadEfectivo);
            tablaVentas.AddCell(columnaConceptoTarjeta);
            tablaVentas.AddCell(columnaCantidadTarjeta);
            tablaVentas.AddCell(columnaConceptoVales);
            tablaVentas.AddCell(columnaCantidadVales);
            tablaVentas.AddCell(columnaConceptoCheques);
            tablaVentas.AddCell(columnaCantidadCheques);
            tablaVentas.AddCell(columnaConceptoTransferencias);
            tablaVentas.AddCell(columnaCantidadTransferencias);
            tablaVentas.AddCell(columnaConceptoCredito);
            tablaVentas.AddCell(columnaCantidadCredito);
            tablaVentas.AddCell(columnaConceptoAbonos);
            tablaVentas.AddCell(columnaCantidadAbonos);
            tablaVentas.AddCell(columnaConceptoAnticiposUtilizados);
            tablaVentas.AddCell(columnaCantidadAnticiposUtilizados);
            #endregion

            // Agregando Conceptos Ventas a Tabla Contenido
            tablaContenido.AddCell(tablaVentas);
            #endregion

            #region Tabla de Anticipos Recibidos
            PdfPTable tablaAnticipos = new PdfPTable(2);
            tablaAnticipos.WidthPercentage = 100;
            tablaAnticipos.SetWidths(anchoColumnasTablas);

            // Encabezado de La Tabla
            #region Encabezado Anticipos
            PdfPCell columnaTituloAnticipo = new PdfPCell(new Phrase(tituloAnticipos.Text, fuenteNegrita));
            columnaTituloAnticipo.Colspan = 2;
            columnaTituloAnticipo.BorderWidth = 0;
            columnaTituloAnticipo.HorizontalAlignment = Element.ALIGN_CENTER;
            columnaTituloAnticipo.Padding = 3;
            tablaAnticipos.AddCell(columnaTituloAnticipo);
            #endregion

            // Conceptos de la Tabla
            #region Concepto Efectivo
            PdfPCell columnaConceptoEfectivoAnticipo = new PdfPCell(new Phrase(lbEfectivoA.Text, fuenteNormal));
            columnaConceptoEfectivoAnticipo.BorderWidth = 0;
            columnaConceptoEfectivoAnticipo.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoEfectivoAnticipo.Padding = 3;

            PdfPCell columnaCantidadEfectivoAnticipo = new PdfPCell(new Phrase(lbTEfectivoA.Text, fuenteNormal));
            columnaCantidadEfectivoAnticipo.BorderWidth = 0;
            columnaCantidadEfectivoAnticipo.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadEfectivoAnticipo.Padding = 3;
            #endregion

            #region Concepto Tarjeta
            PdfPCell columnaConceptoTarjetaAnticipo = new PdfPCell(new Phrase(lbTarjetaA.Text, fuenteNormal));
            columnaConceptoTarjetaAnticipo.BorderWidth = 0;
            columnaConceptoTarjetaAnticipo.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoTarjetaAnticipo.Padding = 3;

            PdfPCell columnaCantidadTarjetaAnticipo = new PdfPCell(new Phrase(lbTTarjetaA.Text, fuenteNormal));
            columnaCantidadTarjetaAnticipo.BorderWidth = 0;
            columnaCantidadTarjetaAnticipo.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadTarjetaAnticipo.Padding = 3;
            #endregion

            #region Concepto Vales
            PdfPCell columnaConceptoValesAnticipo = new PdfPCell(new Phrase(lbValesA.Text, fuenteNormal));
            columnaConceptoValesAnticipo.BorderWidth = 0;
            columnaConceptoValesAnticipo.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoValesAnticipo.Padding = 3;

            PdfPCell columnaCantidadValesAnticipo = new PdfPCell(new Phrase(lbTValesA.Text, fuenteNormal));
            columnaCantidadValesAnticipo.BorderWidth = 0;
            columnaCantidadValesAnticipo.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadValesAnticipo.Padding = 3;
            #endregion

            #region Concepto Cheques
            PdfPCell columnaConceptoChequesAnticipo = new PdfPCell(new Phrase(lbChequeA.Text, fuenteNormal));
            columnaConceptoChequesAnticipo.BorderWidth = 0;
            columnaConceptoChequesAnticipo.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoChequesAnticipo.Padding = 3;

            PdfPCell columnaCantidadChequesAnticipo = new PdfPCell(new Phrase(lbTChequeA.Text, fuenteNormal));
            columnaCantidadChequesAnticipo.BorderWidth = 0;
            columnaCantidadChequesAnticipo.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadChequesAnticipo.Padding = 3;
            #endregion

            #region Concepto Transferencias
            PdfPCell columnaConceptoTransferenciasAnticipo = new PdfPCell(new Phrase(lbTransA.Text, fuenteNormal));
            columnaConceptoTransferenciasAnticipo.BorderWidth = 0;
            columnaConceptoTransferenciasAnticipo.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoTransferenciasAnticipo.Padding = 3;

            PdfPCell columnaCantidadTransferenciasAnticipo = new PdfPCell(new Phrase(lbTTransA.Text, fuenteNormal));
            columnaCantidadTransferenciasAnticipo.BorderWidth = 0;
            columnaCantidadTransferenciasAnticipo.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadTransferenciasAnticipo.Padding = 3;
            #endregion

            #region Agregar Conceptos A Tabla Ventas
            tablaAnticipos.AddCell(columnaConceptoEfectivoAnticipo);
            tablaAnticipos.AddCell(columnaCantidadEfectivoAnticipo);
            tablaAnticipos.AddCell(columnaConceptoTarjetaAnticipo);
            tablaAnticipos.AddCell(columnaCantidadTarjetaAnticipo);
            tablaAnticipos.AddCell(columnaConceptoValesAnticipo);
            tablaAnticipos.AddCell(columnaCantidadValesAnticipo);
            tablaAnticipos.AddCell(columnaConceptoChequesAnticipo);
            tablaAnticipos.AddCell(columnaCantidadChequesAnticipo);
            tablaAnticipos.AddCell(columnaConceptoTransferenciasAnticipo);
            tablaAnticipos.AddCell(columnaCantidadTransferenciasAnticipo);
            #endregion

            // Agregando Conceptos Anticipo a Tabla Contenido
            tablaContenido.AddCell(tablaAnticipos);
            #endregion

            #region Tabla de Dinero Agregado
            PdfPTable tablaDeposito = new PdfPTable(2);
            tablaDeposito.WidthPercentage = 100;
            tablaDeposito.SetWidths(anchoColumnasTablas);

            // Encabezado de La Tabla
            #region Encabezado Anticipos
            PdfPCell columnaTituloDeposito = new PdfPCell(new Phrase(tituloDinero.Text, fuenteNegrita));
            columnaTituloDeposito.Colspan = 2;
            columnaTituloDeposito.BorderWidth = 0;
            columnaTituloDeposito.HorizontalAlignment = Element.ALIGN_CENTER;
            columnaTituloDeposito.Padding = 3;
            tablaDeposito.AddCell(columnaTituloDeposito);
            #endregion

            // Conceptos de la Tabla
            #region Concepto Efectivo
            PdfPCell columnaConceptoEfectivoDeposito = new PdfPCell(new Phrase(lbEfectivoD.Text, fuenteNormal));
            columnaConceptoEfectivoDeposito.BorderWidth = 0;
            columnaConceptoEfectivoDeposito.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoEfectivoDeposito.Padding = 3;

            PdfPCell columnaCantidadEfectivoDeposito = new PdfPCell(new Phrase(lbTEfectivoD.Text, fuenteNormal));
            columnaCantidadEfectivoDeposito.BorderWidth = 0;
            columnaCantidadEfectivoDeposito.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadEfectivoDeposito.Padding = 3;
            #endregion

            #region Concepto Tarjeta
            PdfPCell columnaConceptoTarjetaDeposito = new PdfPCell(new Phrase(lbTarjetaD.Text, fuenteNormal));
            columnaConceptoTarjetaDeposito.BorderWidth = 0;
            columnaConceptoTarjetaDeposito.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoTarjetaDeposito.Padding = 3;

            PdfPCell columnaCantidadTarjetaDeposito = new PdfPCell(new Phrase(lbTTarjetaD.Text, fuenteNormal));
            columnaCantidadTarjetaDeposito.BorderWidth = 0;
            columnaCantidadTarjetaDeposito.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadTarjetaDeposito.Padding = 3;
            #endregion

            #region Concepto Vales
            PdfPCell columnaConceptoValesDeposito = new PdfPCell(new Phrase(lbValesD.Text, fuenteNormal));
            columnaConceptoValesDeposito.BorderWidth = 0;
            columnaConceptoValesDeposito.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoValesDeposito.Padding = 3;

            PdfPCell columnaCantidadValesDeposito = new PdfPCell(new Phrase(lbTValesD.Text, fuenteNormal));
            columnaCantidadValesDeposito.BorderWidth = 0;
            columnaCantidadValesDeposito.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadValesDeposito.Padding = 3;
            #endregion

            #region Concepto Cheques
            PdfPCell columnaConceptoChequesDeposito = new PdfPCell(new Phrase(lbChequeD.Text, fuenteNormal));
            columnaConceptoChequesDeposito.BorderWidth = 0;
            columnaConceptoChequesDeposito.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoChequesDeposito.Padding = 3;

            PdfPCell columnaCantidadChequesDeposito = new PdfPCell(new Phrase(lbTChequeD.Text, fuenteNormal));
            columnaCantidadChequesDeposito.BorderWidth = 0;
            columnaCantidadChequesDeposito.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadChequesDeposito.Padding = 3;
            #endregion

            #region Concepto Transferencias
            PdfPCell columnaConceptoTransferenciasDeposito = new PdfPCell(new Phrase(lbTransD.Text, fuenteNormal));
            columnaConceptoTransferenciasDeposito.BorderWidth = 0;
            columnaConceptoTransferenciasDeposito.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoTransferenciasDeposito.Padding = 3;

            PdfPCell columnaCantidadTransferenciasDeposito = new PdfPCell(new Phrase(lbTTransD.Text, fuenteNormal));
            columnaCantidadTransferenciasDeposito.BorderWidth = 0;
            columnaCantidadTransferenciasDeposito.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadTransferenciasDeposito.Padding = 3;
            #endregion

            #region Agregar Conceptos A Tabla Ventas
            tablaDeposito.AddCell(columnaConceptoEfectivoDeposito);
            tablaDeposito.AddCell(columnaCantidadEfectivoDeposito);
            tablaDeposito.AddCell(columnaConceptoTarjetaDeposito);
            tablaDeposito.AddCell(columnaCantidadTarjetaDeposito);
            tablaDeposito.AddCell(columnaConceptoValesDeposito);
            tablaDeposito.AddCell(columnaCantidadValesDeposito);
            tablaDeposito.AddCell(columnaConceptoChequesDeposito);
            tablaDeposito.AddCell(columnaCantidadChequesDeposito);
            tablaDeposito.AddCell(columnaConceptoTransferenciasDeposito);
            tablaDeposito.AddCell(columnaCantidadTransferenciasDeposito);
            #endregion

            // Agregando Conceptos Anticipo a Tabla Contenido
            tablaContenido.AddCell(tablaDeposito);
            #endregion

            #region Tabla de Dinero Retirado
            PdfPTable tablaRetiros = new PdfPTable(2);
            tablaRetiros.WidthPercentage = 100;
            tablaRetiros.SetWidths(anchoColumnasTablas);

            // Encabezado de La Tabla
            #region Encabezado Anticipos
            PdfPCell columnaTituloRetiro = new PdfPCell(new Phrase(label1.Text, fuenteNegrita));
            columnaTituloRetiro.Colspan = 2;
            columnaTituloRetiro.BorderWidth = 0;
            columnaTituloRetiro.HorizontalAlignment = Element.ALIGN_CENTER;
            columnaTituloRetiro.Padding = 3;
            tablaRetiros.AddCell(columnaTituloRetiro);
            #endregion

            // Conceptos de la Tabla
            #region Concepto Efectivo
            PdfPCell columnaConceptoEfectivoRetiro = new PdfPCell(new Phrase(label13.Text, fuenteNormal));
            columnaConceptoEfectivoRetiro.BorderWidth = 0;
            columnaConceptoEfectivoRetiro.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoEfectivoRetiro.Padding = 3;

            PdfPCell columnaCantidadEfectivoRetiro = new PdfPCell(new Phrase(lbEfectivoR.Text, fuenteNormal));
            columnaCantidadEfectivoRetiro.BorderWidth = 0;
            columnaCantidadEfectivoRetiro.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadEfectivoRetiro.Padding = 3;
            #endregion

            #region Concepto Tarjeta
            PdfPCell columnaConceptoTarjetaRetiro = new PdfPCell(new Phrase(label12.Text, fuenteNormal));
            columnaConceptoTarjetaRetiro.BorderWidth = 0;
            columnaConceptoTarjetaRetiro.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoTarjetaRetiro.Padding = 3;

            PdfPCell columnaCantidadTarjetaRetiro = new PdfPCell(new Phrase(lbTarjetaR.Text, fuenteNormal));
            columnaCantidadTarjetaRetiro.BorderWidth = 0;
            columnaCantidadTarjetaRetiro.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadTarjetaRetiro.Padding = 3;
            #endregion

            #region Concepto Vales
            PdfPCell columnaConceptoValesRetiro = new PdfPCell(new Phrase(label11.Text, fuenteNormal));
            columnaConceptoValesRetiro.BorderWidth = 0;
            columnaConceptoValesRetiro.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoValesRetiro.Padding = 3;

            PdfPCell columnaCantidadValesRetiro = new PdfPCell(new Phrase(lbValesR.Text, fuenteNormal));
            columnaCantidadValesRetiro.BorderWidth = 0;
            columnaCantidadValesRetiro.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadValesRetiro.Padding = 3;
            #endregion

            #region Concepto Cheques
            PdfPCell columnaConceptoChequesRetiro = new PdfPCell(new Phrase(label10.Text, fuenteNormal));
            columnaConceptoChequesRetiro.BorderWidth = 0;
            columnaConceptoChequesRetiro.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoChequesRetiro.Padding = 3;

            PdfPCell columnaCantidadChequesRetiro = new PdfPCell(new Phrase(lbChequeR.Text, fuenteNormal));
            columnaCantidadChequesRetiro.BorderWidth = 0;
            columnaCantidadChequesRetiro.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadChequesRetiro.Padding = 3;
            #endregion

            #region Concepto Transferencias
            PdfPCell columnaConceptoTransferenciasRetiro = new PdfPCell(new Phrase(label9.Text, fuenteNormal));
            columnaConceptoTransferenciasRetiro.BorderWidth = 0;
            columnaConceptoTransferenciasRetiro.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoTransferenciasRetiro.Padding = 3;

            PdfPCell columnaCantidadTransferenciasRetiro = new PdfPCell(new Phrase(lbTransferenciaR.Text, fuenteNormal));
            columnaCantidadTransferenciasRetiro.BorderWidth = 0;
            columnaCantidadTransferenciasRetiro.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadTransferenciasRetiro.Padding = 3;
            #endregion

            #region Concepto Devoluciones
            PdfPCell columnaConceptoDevolucionesRetiro = new PdfPCell(new Phrase(label4.Text, fuenteNormal));
            columnaConceptoDevolucionesRetiro.BorderWidth = 0;
            columnaConceptoDevolucionesRetiro.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoDevolucionesRetiro.Padding = 3;

            PdfPCell columnaCantidadDevolucionesRetiro = new PdfPCell(new Phrase(lbDevoluciones.Text, fuenteNormal));
            columnaCantidadDevolucionesRetiro.BorderWidth = 0;
            columnaCantidadDevolucionesRetiro.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadDevolucionesRetiro.Padding = 3;
            #endregion

            #region Agregar Conceptos A Tabla Ventas
            tablaRetiros.AddCell(columnaConceptoEfectivoRetiro);
            tablaRetiros.AddCell(columnaCantidadEfectivoRetiro);
            tablaRetiros.AddCell(columnaConceptoTarjetaRetiro);
            tablaRetiros.AddCell(columnaCantidadTarjetaRetiro);
            tablaRetiros.AddCell(columnaConceptoValesRetiro);
            tablaRetiros.AddCell(columnaCantidadValesRetiro);
            tablaRetiros.AddCell(columnaConceptoChequesRetiro);
            tablaRetiros.AddCell(columnaCantidadChequesRetiro);
            tablaRetiros.AddCell(columnaConceptoTransferenciasRetiro);
            tablaRetiros.AddCell(columnaCantidadTransferenciasRetiro);
            tablaRetiros.AddCell(columnaConceptoDevolucionesRetiro);
            tablaRetiros.AddCell(columnaCantidadDevolucionesRetiro);
            #endregion

            // Agregando Conceptos Anticipo a Tabla Contenido
            tablaContenido.AddCell(tablaRetiros);
            #endregion

            #region Tabla de Total Caja
            PdfPTable tablaTotalCaja = new PdfPTable(2);
            tablaTotalCaja.WidthPercentage = 100;
            tablaTotalCaja.SetWidths(anchoColumnasTablas);

            // Encabezado de La Tabla
            #region Encabezado TotalCaja
            PdfPCell columnaTituloTotalCaja = new PdfPCell(new Phrase(tituloCaja.Text, fuenteNegrita));
            columnaTituloTotalCaja.Colspan = 2;
            columnaTituloTotalCaja.BorderWidth = 0;
            columnaTituloTotalCaja.HorizontalAlignment = Element.ALIGN_CENTER;
            columnaTituloTotalCaja.Padding = 3;
            tablaTotalCaja.AddCell(columnaTituloTotalCaja);
            #endregion

            // Conceptos de la Tabla
            #region Concepto Efectivo
            PdfPCell columnaConceptoEfectivoTotalCaja = new PdfPCell(new Phrase(lbEfectivoC.Text, fuenteNormal));
            columnaConceptoEfectivoTotalCaja.BorderWidth = 0;
            columnaConceptoEfectivoTotalCaja.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoEfectivoTotalCaja.Padding = 3;

            PdfPCell columnaCantidadEfectivoTotalCaja = new PdfPCell(new Phrase(lbTEfectivoC.Text, fuenteNormal));
            columnaCantidadEfectivoTotalCaja.BorderWidth = 0;
            columnaCantidadEfectivoTotalCaja.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadEfectivoTotalCaja.Padding = 3;
            #endregion

            #region Concepto Tarjeta
            PdfPCell columnaConceptoTarjetaTotalCaja = new PdfPCell(new Phrase(lbTarjetaC.Text, fuenteNormal));
            columnaConceptoTarjetaTotalCaja.BorderWidth = 0;
            columnaConceptoTarjetaTotalCaja.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoTarjetaTotalCaja.Padding = 3;

            PdfPCell columnaCantidadTarjetaTotalCaja = new PdfPCell(new Phrase(lbTTarjetaC.Text, fuenteNormal));
            columnaCantidadTarjetaTotalCaja.BorderWidth = 0;
            columnaCantidadTarjetaTotalCaja.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadTarjetaTotalCaja.Padding = 3;
            #endregion

            #region Concepto Vales
            PdfPCell columnaConceptoValesTotalCaja = new PdfPCell(new Phrase(lbValesC.Text, fuenteNormal));
            columnaConceptoValesTotalCaja.BorderWidth = 0;
            columnaConceptoValesTotalCaja.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoValesTotalCaja.Padding = 3;

            PdfPCell columnaCantidadValesTotalCaja = new PdfPCell(new Phrase(lbTValesC.Text, fuenteNormal));
            columnaCantidadValesTotalCaja.BorderWidth = 0;
            columnaCantidadValesTotalCaja.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadValesTotalCaja.Padding = 3;
            #endregion

            #region Concepto Cheques
            PdfPCell columnaConceptoChequesTotalCaja = new PdfPCell(new Phrase(lbChequeC.Text, fuenteNormal));
            columnaConceptoChequesTotalCaja.BorderWidth = 0;
            columnaConceptoChequesTotalCaja.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoChequesTotalCaja.Padding = 3;

            PdfPCell columnaCantidadChequesTotalCaja = new PdfPCell(new Phrase(lbTChequeC.Text, fuenteNormal));
            columnaCantidadChequesTotalCaja.BorderWidth = 0;
            columnaCantidadChequesTotalCaja.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadChequesTotalCaja.Padding = 3;
            #endregion

            #region Concepto Transferencias
            PdfPCell columnaConceptoTransferenciasTotalCaja = new PdfPCell(new Phrase(lbTransC.Text, fuenteNormal));
            columnaConceptoTransferenciasTotalCaja.BorderWidth = 0;
            columnaConceptoTransferenciasTotalCaja.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoTransferenciasTotalCaja.Padding = 3;

            PdfPCell columnaCantidadTransferenciasTotalCaja = new PdfPCell(new Phrase(lbTTransC.Text, fuenteNormal));
            columnaCantidadTransferenciasTotalCaja.BorderWidth = 0;
            columnaCantidadTransferenciasTotalCaja.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadTransferenciasTotalCaja.Padding = 3;
            #endregion

            #region Concepto SaldoInicial
            PdfPCell columnaConceptoSaldoInicialTotalCaja = new PdfPCell(new Phrase(lbSaldoInicial.Text, fuenteNormal));
            columnaConceptoSaldoInicialTotalCaja.BorderWidth = 0;
            columnaConceptoSaldoInicialTotalCaja.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoSaldoInicialTotalCaja.Padding = 3;

            PdfPCell columnaCantidadSaldoInicialTotalCaja = new PdfPCell(new Phrase(lbTSaldoInicial.Text, fuenteNormal));
            columnaCantidadSaldoInicialTotalCaja.BorderWidth = 0;
            columnaCantidadSaldoInicialTotalCaja.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadSaldoInicialTotalCaja.Padding = 3;
            #endregion

            #region Agregar Conceptos A Tabla Ventas
            tablaTotalCaja.AddCell(columnaConceptoEfectivoTotalCaja);
            tablaTotalCaja.AddCell(columnaCantidadEfectivoTotalCaja);
            tablaTotalCaja.AddCell(columnaConceptoTarjetaTotalCaja);
            tablaTotalCaja.AddCell(columnaCantidadTarjetaTotalCaja);
            tablaTotalCaja.AddCell(columnaConceptoValesTotalCaja);
            tablaTotalCaja.AddCell(columnaCantidadValesTotalCaja);
            tablaTotalCaja.AddCell(columnaConceptoChequesTotalCaja);
            tablaTotalCaja.AddCell(columnaCantidadChequesTotalCaja);
            tablaTotalCaja.AddCell(columnaConceptoTransferenciasTotalCaja);
            tablaTotalCaja.AddCell(columnaCantidadTransferenciasTotalCaja);
            tablaTotalCaja.AddCell(columnaConceptoSaldoInicialTotalCaja);
            tablaTotalCaja.AddCell(columnaCantidadSaldoInicialTotalCaja);
            #endregion

            // Agregando Conceptos Anticipo a Tabla Contenido
            tablaContenido.AddCell(tablaTotalCaja);
            #endregion

            #region espacio en blanco
            PdfPCell cell = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            cell.Colspan = 4;
            cell.BorderWidth = 0;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Padding = 3;

            tablaContenido.AddCell(cell);
            #endregion

            #region Monto Retirado

            #region Tabla Monto Retirado
            PdfPTable tablaMontoRetirado = new PdfPTable(2);
            tablaMontoRetirado.WidthPercentage = 100;
            tablaMontoRetirado.SetWidths(anchoColumnasTablas);

            #region Concepto Monto antes del Corte
            PdfPCell columnaConceptoMontoAntesDelCorte = new PdfPCell(new Phrase(lbTotalCaja.Text + " antes del corte:", fuenteNormal));
            columnaConceptoMontoAntesDelCorte.BorderWidth = 0;
            columnaConceptoMontoAntesDelCorte.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoMontoAntesDelCorte.Padding = 3;

            cantidadEnCajaAntesDelCorteDeCaja = Convert.ToDecimal(lbTTotalCaja.Text.ToString().Replace("$", string.Empty));

            PdfPCell columnaCantidadMontoAntesDelCorte = new PdfPCell(new Phrase(cantidadEnCajaAntesDelCorteDeCaja.ToString("C2"), fuenteNormal));
            columnaCantidadMontoAntesDelCorte.BorderWidth = 0;
            columnaCantidadMontoAntesDelCorte.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadMontoAntesDelCorte.Padding = 3;
            #endregion

            #region Concepto Cantidad Retirada en el corte
            PdfPCell columnaConceptoMontoRetiradaAlCorte = new PdfPCell(new Phrase("Cantidad retirada al corte:", fuenteNormal));
            columnaConceptoMontoRetiradaAlCorte.BorderWidth = 0;
            columnaConceptoMontoRetiradaAlCorte.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoMontoRetiradaAlCorte.Padding = 3;

            if (!FormPrincipal.userNickName.Contains("@"))
            {
                using (DataTable dtSaldoInicial = cn.CargarDatos(cs.cargarSaldoInicialAdministrador()))
                {
                    cantidadRetiradaAlCorteDeCaja = obtenerCantidadRetiradaAlCorteDeCaja(dtSaldoInicial);
                }

                using (DataTable dtPenultimoSaldoInicial = cn.CargarDatos(cs.cargarPenultimoSaldoInicialAdministrador()))
                {
                    idUltimoCorteDeCaja = obtenerIdCajaUltimoCorteDeCaja(dtPenultimoSaldoInicial);
                }
            }
            else if (FormPrincipal.userNickName.Contains("@"))
            {
                using (DataTable dtSaldoInicial = cn.CargarDatos(cs.cargarSaldoInicialEmpleado(Convert.ToString(FormPrincipal.id_empleado))))
                {
                    cantidadRetiradaAlCorteDeCaja = obtenerCantidadRetiradaAlCorteDeCaja(dtSaldoInicial);
                }

                using (DataTable dtPenultimoSaldoInicial = cn.CargarDatos(cs.cargarPenultimaSaldoInicialEmpleado(Convert.ToString(FormPrincipal.id_empleado))))
                {
                    idUltimoCorteDeCaja = obtenerIdCajaUltimoCorteDeCaja(dtPenultimoSaldoInicial);
                }
            }

            PdfPCell columnaCantidadMontoRetiradaAlCorte = new PdfPCell(new Phrase(cantidadRetiradaAlCorteDeCaja.ToString("C2"), fuenteNormal));
            columnaCantidadMontoRetiradaAlCorte.BorderWidth = 0;
            columnaCantidadMontoRetiradaAlCorte.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadMontoRetiradaAlCorte.Padding = 3;
            #endregion

            #region Agregar Conceptos A Tabla Ventas
            tablaMontoRetirado.AddCell(columnaConceptoMontoAntesDelCorte);
            tablaMontoRetirado.AddCell(columnaCantidadMontoAntesDelCorte);
            tablaMontoRetirado.AddCell(columnaConceptoMontoRetiradaAlCorte);
            tablaMontoRetirado.AddCell(columnaCantidadMontoRetiradaAlCorte);
            #endregion

            #endregion

            // Agregando Conceptos Anticipo a Tabla Contenido
            tablaContenido.AddCell(tablaMontoRetirado);
            #endregion

            #region Totales Generales

            #region Total De Ventas
            PdfPTable tablaTotalVentas = new PdfPTable(2);
            tablaTotalVentas.WidthPercentage = 100;
            tablaTotalVentas.SetWidths(anchoColumnasTablas);

            #region Concepto Total de Ventas
            PdfPCell columnaConceptoTotalDeVentas = new PdfPCell(new Phrase(lbVentas.Text, fuenteTotales));
            columnaConceptoTotalDeVentas.BorderWidth = 0;
            columnaConceptoTotalDeVentas.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoTotalDeVentas.Padding = 3;
            columnaConceptoTotalDeVentas.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell columnaCantidadTotalDeVentas = new PdfPCell(new Phrase(lbTVentas.Text, fuenteTotales));
            columnaCantidadTotalDeVentas.BorderWidth = 0;
            columnaCantidadTotalDeVentas.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadTotalDeVentas.Padding = 3;
            columnaCantidadTotalDeVentas.BackgroundColor = new BaseColor(Color.Red);
            #endregion

            #region Agregar Conceptos A Tabla TotalVentas
            tablaTotalVentas.AddCell(columnaConceptoTotalDeVentas);
            tablaTotalVentas.AddCell(columnaCantidadTotalDeVentas);
            #endregion

            #endregion

            #region Total De Anticipos
            PdfPTable tablaTotalAnticipos = new PdfPTable(2);
            tablaTotalAnticipos.WidthPercentage = 100;
            tablaTotalAnticipos.SetWidths(anchoColumnasTablas);

            #region Concepto Total de Anticipo
            PdfPCell columnaConceptoTotalDeAnticipo = new PdfPCell(new Phrase(lbTotalAnticipos.Text, fuenteTotales));
            columnaConceptoTotalDeAnticipo.BorderWidth = 0;
            columnaConceptoTotalDeAnticipo.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoTotalDeAnticipo.Padding = 3;
            columnaConceptoTotalDeAnticipo.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell columnaCantidadTotalDeAnticipo = new PdfPCell(new Phrase(lbTAnticiposA.Text, fuenteTotales));
            columnaCantidadTotalDeAnticipo.BorderWidth = 0;
            columnaCantidadTotalDeAnticipo.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadTotalDeAnticipo.Padding = 3;
            columnaCantidadTotalDeAnticipo.BackgroundColor = new BaseColor(Color.Red);
            #endregion

            #region Agregar Conceptos A Tabla TotalVentas
            tablaTotalAnticipos.AddCell(columnaConceptoTotalDeAnticipo);
            tablaTotalAnticipos.AddCell(columnaCantidadTotalDeAnticipo);
            #endregion

            #endregion

            #region Total De Dinero Agregado
            PdfPTable tablaTotalDineroAgregado = new PdfPTable(2);
            tablaTotalDineroAgregado.WidthPercentage = 100;
            tablaTotalDineroAgregado.SetWidths(anchoColumnasTablas);

            #region Concepto Total de Anticipo
            PdfPCell columnaConceptoTotalDineroAgregado = new PdfPCell(new Phrase(lbTotalAgregado.Text, fuenteTotales));
            columnaConceptoTotalDineroAgregado.BorderWidth = 0;
            columnaConceptoTotalDineroAgregado.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoTotalDineroAgregado.PaddingBottom = 0;
            columnaConceptoTotalDineroAgregado.PaddingTop = 0;
            columnaConceptoTotalDineroAgregado.PaddingLeft = 0;
            columnaConceptoTotalDineroAgregado.PaddingRight = 0;
            columnaConceptoTotalDineroAgregado.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell columnaCantidadTotalDineroAgregado = new PdfPCell(new Phrase(lbTAgregado.Text, fuenteTotales));
            columnaCantidadTotalDineroAgregado.BorderWidth = 0;
            columnaCantidadTotalDineroAgregado.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadTotalDineroAgregado.Padding = 3;
            columnaCantidadTotalDineroAgregado.BackgroundColor = new BaseColor(Color.Red);
            #endregion

            #region Agregar Conceptos A Tabla TotalVentas
            tablaTotalDineroAgregado.AddCell(columnaConceptoTotalDineroAgregado);
            tablaTotalDineroAgregado.AddCell(columnaCantidadTotalDineroAgregado);
            #endregion

            #endregion

            #region Total De Dinero Retirado
            PdfPTable tablaTotalDineroRetirado = new PdfPTable(2);
            tablaTotalDineroRetirado.WidthPercentage = 100;
            tablaTotalDineroRetirado.SetWidths(anchoColumnasTablas);

            #region Concepto Total de Anticipo
            PdfPCell columnaConceptoTotalDineroRetirado = new PdfPCell(new Phrase(label3.Text, fuenteTotales));
            columnaConceptoTotalDineroRetirado.BorderWidth = 0;
            columnaConceptoTotalDineroRetirado.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoTotalDineroRetirado.Padding = 3;
            columnaConceptoTotalDineroRetirado.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell columnaCantidadTotalDineroRetirado = new PdfPCell(new Phrase(lbTRetirado.Text, fuenteTotales));
            columnaCantidadTotalDineroRetirado.BorderWidth = 0;
            columnaCantidadTotalDineroRetirado.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadTotalDineroRetirado.Padding = 3;
            columnaCantidadTotalDineroRetirado.BackgroundColor = new BaseColor(Color.Red);
            #endregion

            #region Agregar Conceptos A Tabla TotalVentas
            tablaTotalDineroRetirado.AddCell(columnaConceptoTotalDineroRetirado);
            tablaTotalDineroRetirado.AddCell(columnaCantidadTotalDineroRetirado);
            #endregion

            #endregion

            #region Total En Caja Despues Del Corte
            PdfPTable tablaTotalEnCajaDespuesDelCorte = new PdfPTable(2);
            tablaTotalEnCajaDespuesDelCorte.WidthPercentage = 100;
            tablaTotalEnCajaDespuesDelCorte.SetWidths(anchoColumnasTablas);

            #region Concepto Total Restante al Corte de Caja
            PdfPCell columnaConceptoTotalEnCajaDespuesDelCorte = new PdfPCell(new Phrase("Total en Caja despues del corte:", fuenteTotales));
            columnaConceptoTotalEnCajaDespuesDelCorte.BorderWidth = 0;
            columnaConceptoTotalEnCajaDespuesDelCorte.HorizontalAlignment = Element.ALIGN_LEFT;
            columnaConceptoTotalEnCajaDespuesDelCorte.Padding = 3;
            columnaConceptoTotalEnCajaDespuesDelCorte.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell columnaCantidadTotalEnCajaDespuesDelCorte = new PdfPCell(new Phrase((cantidadEnCajaAntesDelCorteDeCaja - cantidadRetiradaAlCorteDeCaja).ToString("C2"), fuenteTotales));
            columnaCantidadTotalEnCajaDespuesDelCorte.BorderWidth = 0;
            columnaCantidadTotalEnCajaDespuesDelCorte.HorizontalAlignment = Element.ALIGN_RIGHT;
            columnaCantidadTotalEnCajaDespuesDelCorte.Padding = 3;
            columnaCantidadTotalEnCajaDespuesDelCorte.BackgroundColor = new BaseColor(Color.Red);
            #endregion

            #region Agregar Conceptos A Tabla TotalVentas
            tablaTotalEnCajaDespuesDelCorte.AddCell(columnaConceptoTotalEnCajaDespuesDelCorte);
            tablaTotalEnCajaDespuesDelCorte.AddCell(columnaCantidadTotalEnCajaDespuesDelCorte);
            #endregion

            #endregion

            // Agregando Conceptos Anticipo a Tabla Contenido
            tablaContenido.AddCell(tablaTotalVentas);
            tablaContenido.AddCell(tablaTotalAnticipos);
            tablaContenido.AddCell(tablaTotalDineroAgregado);
            tablaContenido.AddCell(tablaTotalDineroRetirado);
            tablaContenido.AddCell(tablaTotalEnCajaDespuesDelCorte);
            #endregion

            reporte.Add(tablaContenido);

            Paragraph HistorialDeDepositosDelCorteDeCaja = new Paragraph("HISTORIAL DE DEPOSITOS", fuenteTituloHistorialDepositoRetiro);
            Paragraph HistorialDeRetirosDelCorteDeCaja = new Paragraph("HISTORIAL DE RETIROS", fuenteTituloHistorialDepositoRetiro);

            HistorialDeDepositosDelCorteDeCaja.Alignment = Element.ALIGN_CENTER;
            HistorialDeRetirosDelCorteDeCaja.Alignment = Element.ALIGN_CENTER;

            #region Historial de Depositos
            #region Seccion Administrador
            if (!FormPrincipal.userNickName.Contains("@"))
            {
                using (DataTable dtHistorialDepositos = cn.CargarDatos(cs.HistorialDepositosAdminsitrador(idUltimoCorteDeCaja)))
                {
                    if (!dtHistorialDepositos.Rows.Count.Equals(0))
                    {
                        reporte.Add(linea);
                        reporte.Add(HistorialDeDepositosDelCorteDeCaja);

                        PdfPTable tablaHistorialDepositosDespuesDelCorte = new PdfPTable(8);
                        tablaHistorialDepositosDespuesDelCorte.WidthPercentage = 100;
                        tablaHistorialDepositosDespuesDelCorte.SetWidths(anchoColumnasTablasDepositosRetiros);
                        tablaHistorialDepositosDespuesDelCorte.DefaultCell.Border = (int)BorderStyle.FixedSingle;

                        var encabezadoRealizo = "Realizó";
                        var encabezadoEfectivo = "Efectivo";
                        var encabezadoTarjeta = "Tarjeta";
                        var encabezadoVales = "Vales";
                        var encabezadoCheque = "Cheque";
                        var encabezadoTransferencia = "Transferencia";
                        var encabezadoFecha = "Fecha";
                        var encabezadoMotivo = "Concepto";

                        PdfPCell columnaTituloRealizo = new PdfPCell(new Phrase(encabezadoRealizo, fuenteNegritaDepositoRetiro));
                        columnaTituloRealizo.BorderWidth = 0;
                        columnaTituloRealizo.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloRealizo.Padding = 3;

                        PdfPCell columnaTituloEfectivo = new PdfPCell(new Phrase(encabezadoEfectivo, fuenteNegritaDepositoRetiro));
                        columnaTituloEfectivo.BorderWidth = 0;
                        columnaTituloEfectivo.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloEfectivo.Padding = 3;

                        PdfPCell columnaTituloTarjeta = new PdfPCell(new Phrase(encabezadoTarjeta, fuenteNegritaDepositoRetiro));
                        columnaTituloTarjeta.BorderWidth = 0;
                        columnaTituloTarjeta.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloTarjeta.Padding = 0;

                        PdfPCell columnaTituloVales = new PdfPCell(new Phrase(encabezadoVales, fuenteNegritaDepositoRetiro));
                        columnaTituloVales.BorderWidth = 0;
                        columnaTituloVales.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloVales.Padding = 3;

                        PdfPCell columnaTituloCheques = new PdfPCell(new Phrase(encabezadoCheque, fuenteNegritaDepositoRetiro));
                        columnaTituloCheques.BorderWidth = 0;
                        columnaTituloCheques.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloCheques.Padding = 3;

                        PdfPCell columnaTituloTransferencias = new PdfPCell(new Phrase(encabezadoTransferencia, fuenteNegritaDepositoRetiro));
                        columnaTituloTransferencias.BorderWidth = 0;
                        columnaTituloTransferencias.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloTransferencias.Padding = 3;

                        PdfPCell columnaTituloFechas = new PdfPCell(new Phrase(encabezadoFecha, fuenteNegritaDepositoRetiro));
                        columnaTituloFechas.BorderWidth = 0;
                        columnaTituloFechas.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloFechas.Padding = 3;

                        PdfPCell columnaTituloMotivo = new PdfPCell(new Phrase(encabezadoMotivo, fuenteNegritaDepositoRetiro));
                        columnaTituloMotivo.BorderWidth = 0;
                        columnaTituloMotivo.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloMotivo.Padding = 3;

                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloRealizo);
                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloEfectivo);
                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloTarjeta);
                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloVales);
                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloCheques);
                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloTransferencias);
                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloFechas);
                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloMotivo);

                        for (int i = 0; i < dtHistorialDepositos.Rows.Count; i++)
                        {
                            var conceptoRealizo = dtHistorialDepositos.Rows[i]["Realizo"].ToString();
                            var conceptoEfectivo = Convert.ToDecimal(dtHistorialDepositos.Rows[i]["Efectivo"].ToString());
                            var conceptoTarjeta = Convert.ToDecimal(dtHistorialDepositos.Rows[i]["Tarjeta"].ToString());
                            var conceptoVales = Convert.ToDecimal(dtHistorialDepositos.Rows[i]["Vales"].ToString());
                            var conceptoCheque = Convert.ToDecimal(dtHistorialDepositos.Rows[i]["Cheque"].ToString());
                            var conceptoTransferencia = Convert.ToDecimal(dtHistorialDepositos.Rows[i]["Transferencia"].ToString());
                            var conceptoFecha = dtHistorialDepositos.Rows[i]["Fecha"].ToString();
                            var conceptoMotivo = dtHistorialDepositos.Rows[i]["Concepto"].ToString();

                            PdfPCell columnaRealizo = new PdfPCell(new Phrase(conceptoRealizo, fuenteNormal));
                            columnaRealizo.BorderWidth = 1;
                            columnaRealizo.HorizontalAlignment = Element.ALIGN_LEFT;
                            columnaRealizo.Padding = 3;

                            PdfPCell columnaEfectivo = new PdfPCell(new Phrase(conceptoEfectivo.ToString("C2"), fuenteNormal));
                            columnaEfectivo.BorderWidth = 1;
                            columnaEfectivo.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaEfectivo.Padding = 3;

                            PdfPCell columnaTarjeta = new PdfPCell(new Phrase(conceptoTarjeta.ToString("C2"), fuenteNormal));
                            columnaTarjeta.BorderWidth = 1;
                            columnaTarjeta.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaTarjeta.Padding = 3;

                            PdfPCell columnaVales = new PdfPCell(new Phrase(conceptoVales.ToString("C2"), fuenteNormal));
                            columnaVales.BorderWidth = 1;
                            columnaVales.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaVales.Padding = 3;

                            PdfPCell columnaCheques = new PdfPCell(new Phrase(conceptoCheque.ToString("C2"), fuenteNormal));
                            columnaCheques.BorderWidth = 1;
                            columnaCheques.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaCheques.Padding = 3;

                            PdfPCell columnaTransferencias = new PdfPCell(new Phrase(conceptoTransferencia.ToString("C2"), fuenteNormal));
                            columnaTransferencias.BorderWidth = 1;
                            columnaTransferencias.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaTransferencias.Padding = 3;

                            PdfPCell columnaFechas = new PdfPCell(new Phrase(conceptoFecha, fuenteNormal));
                            columnaFechas.BorderWidth = 1;
                            columnaFechas.HorizontalAlignment = Element.ALIGN_CENTER;
                            columnaFechas.Padding = 3;

                            PdfPCell columnaMotivo = new PdfPCell(new Phrase(conceptoMotivo, fuenteNormal));
                            columnaMotivo.BorderWidth = 1;
                            columnaMotivo.HorizontalAlignment = Element.ALIGN_LEFT;
                            columnaMotivo.Padding = 3;

                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaRealizo);
                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaEfectivo);
                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTarjeta);
                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaVales);
                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaCheques);
                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTransferencias);
                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaFechas);
                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaMotivo);
                        }

                        reporte.Add(tablaHistorialDepositosDespuesDelCorte);

                        using (DataTable dtSumaDeDepositos = cn.CargarDatos(cs.cargarHistorialdepositosAdministradorSumaTotal(idUltimoCorteDeCaja)))
                        {
                            if (!dtSumaDeDepositos.Rows.Count.Equals(0))
                            {
                                PdfPTable tablaSumaHistorialDepositosDespuesDelCorte = new PdfPTable(8);
                                tablaSumaHistorialDepositosDespuesDelCorte.WidthPercentage = 100;
                                tablaSumaHistorialDepositosDespuesDelCorte.SetWidths(anchoColumnasTablasDepositosRetiros);
                                tablaSumaHistorialDepositosDespuesDelCorte.DefaultCell.Border = (int)BorderStyle.FixedSingle;

                                foreach (DataRow item in dtSumaDeDepositos.Rows)
                                {
                                    var conceptoRealizo = string.Empty;
                                    var conceptoEfectivo = Convert.ToDecimal(item["Efectivo"].ToString());
                                    var conceptoTarjeta = Convert.ToDecimal(item["Tarjeta"].ToString());
                                    var conceptoVales = Convert.ToDecimal(item["Vales"].ToString());
                                    var conceptoCheque = Convert.ToDecimal(item["Cheque"].ToString());
                                    var conceptoTransferencia = Convert.ToDecimal(item["Transferencia"].ToString());
                                    var conceptoFecha = string.Empty;
                                    var conceptoMotivo = string.Empty;

                                    PdfPCell columnaRealizo = new PdfPCell(new Phrase(conceptoRealizo, fuenteNormal));
                                    columnaRealizo.BorderWidth = 0;
                                    columnaRealizo.HorizontalAlignment = Element.ALIGN_LEFT;
                                    columnaRealizo.Padding = 3;

                                    PdfPCell columnaEfectivo = new PdfPCell(new Phrase(conceptoEfectivo.ToString("C2"), fuenteNormal));
                                    columnaEfectivo.BorderWidth = 1;
                                    columnaEfectivo.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaEfectivo.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaEfectivo.Padding = 3;

                                    PdfPCell columnaTarjeta = new PdfPCell(new Phrase(conceptoTarjeta.ToString("C2"), fuenteNormal));
                                    columnaTarjeta.BorderWidth = 1;
                                    columnaTarjeta.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaTarjeta.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaTarjeta.Padding = 3;

                                    PdfPCell columnaVales = new PdfPCell(new Phrase(conceptoVales.ToString("C2"), fuenteNormal));
                                    columnaVales.BorderWidth = 1;
                                    columnaVales.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaVales.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaVales.Padding = 3;

                                    PdfPCell columnaCheques = new PdfPCell(new Phrase(conceptoCheque.ToString("C2"), fuenteNormal));
                                    columnaCheques.BorderWidth = 1;
                                    columnaCheques.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaCheques.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaCheques.Padding = 3;

                                    PdfPCell columnaTransferencias = new PdfPCell(new Phrase(conceptoTransferencia.ToString("C2"), fuenteNormal));
                                    columnaTransferencias.BorderWidth = 1;
                                    columnaTransferencias.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaTransferencias.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaTransferencias.Padding = 3;

                                    PdfPCell columnaFechas = new PdfPCell(new Phrase(conceptoFecha, fuenteNormal));
                                    columnaFechas.BorderWidth = 0;
                                    columnaFechas.HorizontalAlignment = Element.ALIGN_CENTER;
                                    columnaFechas.Padding = 3;

                                    PdfPCell columnaMotivo = new PdfPCell(new Phrase(conceptoMotivo, fuenteNormal));
                                    columnaMotivo.BorderWidth = 0;
                                    columnaMotivo.HorizontalAlignment = Element.ALIGN_LEFT;
                                    columnaMotivo.Padding = 3;

                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaRealizo);
                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaEfectivo);
                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaTarjeta);
                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaVales);
                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaCheques);
                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaTransferencias);
                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaFechas);
                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaMotivo);
                                }

                                reporte.Add(tablaSumaHistorialDepositosDespuesDelCorte);
                            }
                        }
                    }
                }
            }
            #endregion
            #region Seccion de Empleados
            else if (FormPrincipal.userNickName.Contains("@"))
            {
                using (DataTable dtHistorialDepositos = cn.CargarDatos(cs.HistorialDepositosEmpleado(idUltimoCorteDeCaja, FormPrincipal.id_empleado)))
                {
                    if (!dtHistorialDepositos.Rows.Count.Equals(0))
                    {
                        reporte.Add(linea);
                        reporte.Add(HistorialDeDepositosDelCorteDeCaja);

                        PdfPTable tablaHistorialDepositosDespuesDelCorte = new PdfPTable(8);
                        tablaHistorialDepositosDespuesDelCorte.WidthPercentage = 100;
                        tablaHistorialDepositosDespuesDelCorte.SetWidths(anchoColumnasTablasDepositosRetiros);
                        tablaHistorialDepositosDespuesDelCorte.DefaultCell.Border = (int)BorderStyle.FixedSingle;

                        var encabezadoRealizo = "Realizó";
                        var encabezadoEfectivo = "Efectivo";
                        var encabezadoTarjeta = "Tarjeta";
                        var encabezadoVales = "Vales";
                        var encabezadoCheque = "Cheque";
                        var encabezadoTransferencia = "Transferencia";
                        var encabezadoFecha = "Fecha";
                        var encabezadoMotivo = "Concepto";

                        PdfPCell columnaTituloRealizo = new PdfPCell(new Phrase(encabezadoRealizo, fuenteNegrita));
                        columnaTituloRealizo.BorderWidth = 0;
                        columnaTituloRealizo.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloRealizo.Padding = 3;

                        PdfPCell columnaTituloEfectivo = new PdfPCell(new Phrase(encabezadoEfectivo, fuenteNegrita));
                        columnaTituloEfectivo.BorderWidth = 0;
                        columnaTituloEfectivo.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloEfectivo.Padding = 3;

                        PdfPCell columnaTituloTarjeta = new PdfPCell(new Phrase(encabezadoTarjeta, fuenteNegrita));
                        columnaTituloTarjeta.BorderWidth = 0;
                        columnaTituloTarjeta.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloTarjeta.Padding = 0;

                        PdfPCell columnaTituloVales = new PdfPCell(new Phrase(encabezadoVales, fuenteNegrita));
                        columnaTituloVales.BorderWidth = 0;
                        columnaTituloVales.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloVales.Padding = 3;

                        PdfPCell columnaTituloCheques = new PdfPCell(new Phrase(encabezadoCheque, fuenteNegrita));
                        columnaTituloCheques.BorderWidth = 0;
                        columnaTituloCheques.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloCheques.Padding = 3;

                        PdfPCell columnaTituloTransferencias = new PdfPCell(new Phrase(encabezadoTransferencia, fuenteNegrita));
                        columnaTituloTransferencias.BorderWidth = 0;
                        columnaTituloTransferencias.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloTransferencias.Padding = 3;

                        PdfPCell columnaTituloFechas = new PdfPCell(new Phrase(encabezadoFecha, fuenteNegrita));
                        columnaTituloFechas.BorderWidth = 0;
                        columnaTituloFechas.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloFechas.Padding = 3;

                        PdfPCell columnaTituloMotivo = new PdfPCell(new Phrase(encabezadoMotivo, fuenteNegrita));
                        columnaTituloMotivo.BorderWidth = 0;
                        columnaTituloMotivo.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloMotivo.Padding = 3;

                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloRealizo);
                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloEfectivo);
                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloTarjeta);
                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloVales);
                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloCheques);
                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloTransferencias);
                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloFechas);
                        tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTituloMotivo);

                        for (int i = 0; i < dtHistorialDepositos.Rows.Count; i++)
                        {
                            var conceptoRealizo = dtHistorialDepositos.Rows[i]["Realizo"].ToString();
                            var conceptoEfectivo = Convert.ToDecimal(dtHistorialDepositos.Rows[i]["Efectivo"].ToString());
                            var conceptoTarjeta = Convert.ToDecimal(dtHistorialDepositos.Rows[i]["Tarjeta"].ToString());
                            var conceptoVales = Convert.ToDecimal(dtHistorialDepositos.Rows[i]["Vales"].ToString());
                            var conceptoCheque = Convert.ToDecimal(dtHistorialDepositos.Rows[i]["Cheque"].ToString());
                            var conceptoTransferencia = Convert.ToDecimal(dtHistorialDepositos.Rows[i]["Transferencia"].ToString());
                            var conceptoFecha = dtHistorialDepositos.Rows[i]["Fecha"].ToString();
                            var conceptoMotivo = dtHistorialDepositos.Rows[i]["Concepto"].ToString();

                            PdfPCell columnaRealizo = new PdfPCell(new Phrase(conceptoRealizo, fuenteNormal));
                            columnaRealizo.BorderWidth = 1;
                            columnaRealizo.HorizontalAlignment = Element.ALIGN_LEFT;
                            columnaRealizo.Padding = 3;

                            PdfPCell columnaEfectivo = new PdfPCell(new Phrase(conceptoEfectivo.ToString("C2"), fuenteNormal));
                            columnaEfectivo.BorderWidth = 1;
                            columnaEfectivo.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaEfectivo.Padding = 3;

                            PdfPCell columnaTarjeta = new PdfPCell(new Phrase(conceptoTarjeta.ToString("C2"), fuenteNormal));
                            columnaTarjeta.BorderWidth = 1;
                            columnaTarjeta.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaTarjeta.Padding = 3;

                            PdfPCell columnaVales = new PdfPCell(new Phrase(conceptoVales.ToString("C2"), fuenteNormal));
                            columnaVales.BorderWidth = 1;
                            columnaVales.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaVales.Padding = 3;

                            PdfPCell columnaCheques = new PdfPCell(new Phrase(conceptoCheque.ToString("C2"), fuenteNormal));
                            columnaCheques.BorderWidth = 1;
                            columnaCheques.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaCheques.Padding = 3;

                            PdfPCell columnaTransferencias = new PdfPCell(new Phrase(conceptoTransferencia.ToString("C2"), fuenteNormal));
                            columnaTransferencias.BorderWidth = 1;
                            columnaTransferencias.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaTransferencias.Padding = 3;

                            PdfPCell columnaFechas = new PdfPCell(new Phrase(conceptoFecha, fuenteNormal));
                            columnaFechas.BorderWidth = 1;
                            columnaFechas.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaFechas.Padding = 3;

                            PdfPCell columnaMotivo = new PdfPCell(new Phrase(conceptoMotivo, fuenteNormal));
                            columnaMotivo.BorderWidth = 1;
                            columnaMotivo.HorizontalAlignment = Element.ALIGN_LEFT;
                            columnaMotivo.Padding = 3;

                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaRealizo);
                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaEfectivo);
                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTarjeta);
                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaVales);
                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaCheques);
                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaTransferencias);
                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaFechas);
                            tablaHistorialDepositosDespuesDelCorte.AddCell(columnaMotivo);
                        }

                        reporte.Add(tablaHistorialDepositosDespuesDelCorte);

                        using (DataTable dtSumaDeDepositos = cn.CargarDatos(cs.cargarHistorialdepositosEmpleadoSumaTotal(idUltimoCorteDeCaja, FormPrincipal.id_empleado)))
                        {
                            if (!dtSumaDeDepositos.Rows.Count.Equals(0))
                            {
                                PdfPTable tablaSumaHistorialDepositosDespuesDelCorte = new PdfPTable(8);
                                tablaSumaHistorialDepositosDespuesDelCorte.WidthPercentage = 100;
                                tablaSumaHistorialDepositosDespuesDelCorte.SetWidths(anchoColumnasTablasDepositosRetiros);
                                tablaSumaHistorialDepositosDespuesDelCorte.DefaultCell.Border = (int)BorderStyle.FixedSingle;

                                foreach (DataRow item in dtSumaDeDepositos.Rows)
                                {
                                    var conceptoRealizo = string.Empty;
                                    var conceptoEfectivo = Convert.ToDecimal(item["Efectivo"].ToString());
                                    var conceptoTarjeta = Convert.ToDecimal(item["Tarjeta"].ToString());
                                    var conceptoVales = Convert.ToDecimal(item["Vales"].ToString());
                                    var conceptoCheque = Convert.ToDecimal(item["Cheque"].ToString());
                                    var conceptoTransferencia = Convert.ToDecimal(item["Transferencia"].ToString());
                                    var conceptoFecha = string.Empty;
                                    var conceptoMotivo = string.Empty;

                                    PdfPCell columnaRealizo = new PdfPCell(new Phrase(conceptoRealizo, fuenteNormal));
                                    columnaRealizo.BorderWidth = 0;
                                    columnaRealizo.HorizontalAlignment = Element.ALIGN_LEFT;
                                    columnaRealizo.Padding = 3;

                                    PdfPCell columnaEfectivo = new PdfPCell(new Phrase(conceptoEfectivo.ToString("C2"), fuenteNormal));
                                    columnaEfectivo.BorderWidth = 1;
                                    columnaEfectivo.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaEfectivo.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaEfectivo.Padding = 3;

                                    PdfPCell columnaTarjeta = new PdfPCell(new Phrase(conceptoTarjeta.ToString("C2"), fuenteNormal));
                                    columnaTarjeta.BorderWidth = 1;
                                    columnaTarjeta.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaTarjeta.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaTarjeta.Padding = 3;

                                    PdfPCell columnaVales = new PdfPCell(new Phrase(conceptoVales.ToString("C2"), fuenteNormal));
                                    columnaVales.BorderWidth = 1;
                                    columnaVales.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaVales.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaVales.Padding = 3;

                                    PdfPCell columnaCheques = new PdfPCell(new Phrase(conceptoCheque.ToString("C2"), fuenteNormal));
                                    columnaCheques.BorderWidth = 1;
                                    columnaCheques.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaCheques.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaCheques.Padding = 3;

                                    PdfPCell columnaTransferencias = new PdfPCell(new Phrase(conceptoTransferencia.ToString("C2"), fuenteNormal));
                                    columnaTransferencias.BorderWidth = 1;
                                    columnaTransferencias.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaTransferencias.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaTransferencias.Padding = 3;

                                    PdfPCell columnaFechas = new PdfPCell(new Phrase(conceptoFecha, fuenteNormal));
                                    columnaFechas.BorderWidth = 0;
                                    columnaFechas.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaFechas.Padding = 3;

                                    PdfPCell columnaMotivo = new PdfPCell(new Phrase(conceptoMotivo, fuenteNormal));
                                    columnaMotivo.BorderWidth = 0;
                                    columnaMotivo.HorizontalAlignment = Element.ALIGN_LEFT;
                                    columnaMotivo.Padding = 3;

                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaRealizo);
                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaEfectivo);
                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaTarjeta);
                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaVales);
                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaCheques);
                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaTransferencias);
                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaFechas);
                                    tablaSumaHistorialDepositosDespuesDelCorte.AddCell(columnaMotivo);
                                }

                                reporte.Add(tablaSumaHistorialDepositosDespuesDelCorte);
                            }
                        }
                    }
                }
            }
            #endregion
            #endregion

            #region Historial de Retiros
            #region Seccion Administrador
            if (!FormPrincipal.userNickName.Contains("@"))
            {
                using (DataTable dtHistorialRetiros = cn.CargarDatos(cs.HistorialRetirosAdminsitrador(idUltimoCorteDeCaja)))
                {
                    if (!dtHistorialRetiros.Rows.Count.Equals(0))
                    {
                        reporte.Add(linea);
                        reporte.Add(HistorialDeRetirosDelCorteDeCaja);

                        PdfPTable tablaHistorialRetirosDespuesDelCorte = new PdfPTable(8);
                        tablaHistorialRetirosDespuesDelCorte.WidthPercentage = 100;
                        tablaHistorialRetirosDespuesDelCorte.SetWidths(anchoColumnasTablasDepositosRetiros);
                        tablaHistorialRetirosDespuesDelCorte.DefaultCell.Border = (int)BorderStyle.FixedSingle;

                        var encabezadoRealizo = "Realizó";
                        var encabezadoEfectivo = "Efectivo";
                        var encabezadoTarjeta = "Tarjeta";
                        var encabezadoVales = "Vales";
                        var encabezadoCheque = "Cheque";
                        var encabezadoTransferencia = "Transferencia";
                        var encabezadoFecha = "Fecha";
                        var encabezadoMotivo = "Concepto";

                        PdfPCell columnaTituloRealizo = new PdfPCell(new Phrase(encabezadoRealizo, fuenteNegritaDepositoRetiro));
                        columnaTituloRealizo.BorderWidth = 0;
                        columnaTituloRealizo.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloRealizo.Padding = 3;

                        PdfPCell columnaTituloEfectivo = new PdfPCell(new Phrase(encabezadoEfectivo, fuenteNegritaDepositoRetiro));
                        columnaTituloEfectivo.BorderWidth = 0;
                        columnaTituloEfectivo.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloEfectivo.Padding = 3;

                        PdfPCell columnaTituloTarjeta = new PdfPCell(new Phrase(encabezadoTarjeta, fuenteNegritaDepositoRetiro));
                        columnaTituloTarjeta.BorderWidth = 0;
                        columnaTituloTarjeta.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloTarjeta.Padding = 0;

                        PdfPCell columnaTituloVales = new PdfPCell(new Phrase(encabezadoVales, fuenteNegritaDepositoRetiro));
                        columnaTituloVales.BorderWidth = 0;
                        columnaTituloVales.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloVales.Padding = 3;

                        PdfPCell columnaTituloCheques = new PdfPCell(new Phrase(encabezadoCheque, fuenteNegritaDepositoRetiro));
                        columnaTituloCheques.BorderWidth = 0;
                        columnaTituloCheques.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloCheques.Padding = 3;

                        PdfPCell columnaTituloTransferencias = new PdfPCell(new Phrase(encabezadoTransferencia, fuenteNegritaDepositoRetiro));
                        columnaTituloTransferencias.BorderWidth = 0;
                        columnaTituloTransferencias.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloTransferencias.Padding = 3;

                        PdfPCell columnaTituloFechas = new PdfPCell(new Phrase(encabezadoFecha, fuenteNegritaDepositoRetiro));
                        columnaTituloFechas.BorderWidth = 0;
                        columnaTituloFechas.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloFechas.Padding = 3;

                        PdfPCell columnaTituloMotivo = new PdfPCell(new Phrase(encabezadoMotivo, fuenteNegritaDepositoRetiro));
                        columnaTituloMotivo.BorderWidth = 0;
                        columnaTituloMotivo.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloMotivo.Padding = 3;

                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloRealizo);
                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloEfectivo);
                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloTarjeta);
                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloVales);
                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloCheques);
                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloTransferencias);
                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloFechas);
                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloMotivo);

                        for (int i = 0; i < dtHistorialRetiros.Rows.Count; i++)
                        {
                            var conceptoRealizo = dtHistorialRetiros.Rows[i]["Realizo"].ToString();
                            var conceptoEfectivo = Convert.ToDecimal(dtHistorialRetiros.Rows[i]["Efectivo"].ToString());
                            var conceptoTarjeta = Convert.ToDecimal(dtHistorialRetiros.Rows[i]["Tarjeta"].ToString());
                            var conceptoVales = Convert.ToDecimal(dtHistorialRetiros.Rows[i]["Vales"].ToString());
                            var conceptoCheque = Convert.ToDecimal(dtHistorialRetiros.Rows[i]["Cheque"].ToString());
                            var conceptoTransferencia = Convert.ToDecimal(dtHistorialRetiros.Rows[i]["Transferencia"].ToString());
                            var conceptoFecha = dtHistorialRetiros.Rows[i]["Fecha"].ToString();
                            var conceptoMotivo = dtHistorialRetiros.Rows[i]["Concepto"].ToString();

                            PdfPCell columnaRealizo = new PdfPCell(new Phrase(conceptoRealizo, fuenteNormal));
                            columnaRealizo.BorderWidth = 1;
                            columnaRealizo.HorizontalAlignment = Element.ALIGN_LEFT;
                            columnaRealizo.Padding = 3;

                            PdfPCell columnaEfectivo = new PdfPCell(new Phrase(conceptoEfectivo.ToString("C2"), fuenteNormal));
                            columnaEfectivo.BorderWidth = 1;
                            columnaEfectivo.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaEfectivo.Padding = 3;

                            PdfPCell columnaTarjeta = new PdfPCell(new Phrase(conceptoTarjeta.ToString("C2"), fuenteNormal));
                            columnaTarjeta.BorderWidth = 1;
                            columnaTarjeta.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaTarjeta.Padding = 3;

                            PdfPCell columnaVales = new PdfPCell(new Phrase(conceptoVales.ToString("C2"), fuenteNormal));
                            columnaVales.BorderWidth = 1;
                            columnaVales.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaVales.Padding = 3;

                            PdfPCell columnaCheques = new PdfPCell(new Phrase(conceptoCheque.ToString("C2"), fuenteNormal));
                            columnaCheques.BorderWidth = 1;
                            columnaCheques.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaCheques.Padding = 3;

                            PdfPCell columnaTransferencias = new PdfPCell(new Phrase(conceptoTransferencia.ToString("C2"), fuenteNormal));
                            columnaTransferencias.BorderWidth = 1;
                            columnaTransferencias.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaTransferencias.Padding = 3;

                            PdfPCell columnaFechas = new PdfPCell(new Phrase(conceptoFecha, fuenteNormal));
                            columnaFechas.BorderWidth = 1;
                            columnaFechas.HorizontalAlignment = Element.ALIGN_CENTER;
                            columnaFechas.Padding = 3;

                            PdfPCell columnaMotivo = new PdfPCell(new Phrase(conceptoMotivo, fuenteNormal));
                            columnaMotivo.BorderWidth = 1;
                            columnaMotivo.HorizontalAlignment = Element.ALIGN_LEFT;
                            columnaMotivo.Padding = 3;

                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaRealizo);
                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaEfectivo);
                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTarjeta);
                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaVales);
                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaCheques);
                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTransferencias);
                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaFechas);
                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaMotivo);
                        }

                        reporte.Add(tablaHistorialRetirosDespuesDelCorte);

                        using (DataTable dtSumaDeRetiros = cn.CargarDatos(cs.cargarHistorialRetirosAdministradorSumaTotal(idUltimoCorteDeCaja)))
                        {
                            if (!dtSumaDeRetiros.Rows.Count.Equals(0))
                            {
                                PdfPTable tablaSumaHistorialRetirosDespuesDelCorte = new PdfPTable(8);
                                tablaSumaHistorialRetirosDespuesDelCorte.WidthPercentage = 100;
                                tablaSumaHistorialRetirosDespuesDelCorte.SetWidths(anchoColumnasTablasDepositosRetiros);
                                tablaSumaHistorialRetirosDespuesDelCorte.DefaultCell.Border = (int)BorderStyle.FixedSingle;

                                foreach (DataRow item in dtSumaDeRetiros.Rows)
                                {
                                    var conceptoRealizo = string.Empty;
                                    var conceptoEfectivo = Convert.ToDecimal(item["Efectivo"].ToString());
                                    var conceptoTarjeta = Convert.ToDecimal(item["Tarjeta"].ToString());
                                    var conceptoVales = Convert.ToDecimal(item["Vales"].ToString());
                                    var conceptoCheque = Convert.ToDecimal(item["Cheque"].ToString());
                                    var conceptoTransferencia = Convert.ToDecimal(item["Transferencia"].ToString());
                                    var conceptoFecha = string.Empty;
                                    var conceptoMotivo = string.Empty;

                                    PdfPCell columnaRealizo = new PdfPCell(new Phrase(conceptoRealizo, fuenteNormal));
                                    columnaRealizo.BorderWidth = 0;
                                    columnaRealizo.HorizontalAlignment = Element.ALIGN_LEFT;
                                    columnaRealizo.Padding = 3;

                                    PdfPCell columnaEfectivo = new PdfPCell(new Phrase(conceptoEfectivo.ToString("C2"), fuenteNormal));
                                    columnaEfectivo.BorderWidth = 1;
                                    columnaEfectivo.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaEfectivo.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaEfectivo.Padding = 3;

                                    PdfPCell columnaTarjeta = new PdfPCell(new Phrase(conceptoTarjeta.ToString("C2"), fuenteNormal));
                                    columnaTarjeta.BorderWidth = 1;
                                    columnaTarjeta.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaTarjeta.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaTarjeta.Padding = 3;

                                    PdfPCell columnaVales = new PdfPCell(new Phrase(conceptoVales.ToString("C2"), fuenteNormal));
                                    columnaVales.BorderWidth = 1;
                                    columnaVales.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaVales.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaVales.Padding = 3;

                                    PdfPCell columnaCheques = new PdfPCell(new Phrase(conceptoCheque.ToString("C2"), fuenteNormal));
                                    columnaCheques.BorderWidth = 1;
                                    columnaCheques.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaCheques.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaCheques.Padding = 3;

                                    PdfPCell columnaTransferencias = new PdfPCell(new Phrase(conceptoTransferencia.ToString("C2"), fuenteNormal));
                                    columnaTransferencias.BorderWidth = 1;
                                    columnaTransferencias.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaTransferencias.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaTransferencias.Padding = 3;

                                    PdfPCell columnaFechas = new PdfPCell(new Phrase(conceptoFecha, fuenteNormal));
                                    columnaFechas.BorderWidth = 0;
                                    columnaFechas.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaFechas.Padding = 3;

                                    PdfPCell columnaMotivo = new PdfPCell(new Phrase(conceptoMotivo, fuenteNormal));
                                    columnaMotivo.BorderWidth = 0;
                                    columnaMotivo.HorizontalAlignment = Element.ALIGN_LEFT;
                                    columnaMotivo.Padding = 3;

                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaRealizo);
                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaEfectivo);
                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaTarjeta);
                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaVales);
                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaCheques);
                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaTransferencias);
                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaFechas);
                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaMotivo);
                                }

                                reporte.Add(tablaSumaHistorialRetirosDespuesDelCorte);
                            }
                        }
                    }
                }
            }
            #endregion
            #region Seccion Empleado
            if (FormPrincipal.userNickName.Contains("@"))
            {
                using (DataTable dtHistorialRetiros = cn.CargarDatos(cs.HistorialRetirosEmpleado(idUltimoCorteDeCaja, FormPrincipal.id_empleado)))
                {
                    if (!dtHistorialRetiros.Rows.Count.Equals(0))
                    {
                        reporte.Add(linea);
                        reporte.Add(HistorialDeRetirosDelCorteDeCaja);

                        PdfPTable tablaHistorialRetirosDespuesDelCorte = new PdfPTable(8);
                        tablaHistorialRetirosDespuesDelCorte.WidthPercentage = 100;
                        tablaHistorialRetirosDespuesDelCorte.SetWidths(anchoColumnasTablasDepositosRetiros);
                        tablaHistorialRetirosDespuesDelCorte.DefaultCell.Border = (int)BorderStyle.FixedSingle;

                        var encabezadoRealizo = "Realizó";
                        var encabezadoEfectivo = "Efectivo";
                        var encabezadoTarjeta = "Tarjeta";
                        var encabezadoVales = "Vales";
                        var encabezadoCheque = "Cheque";
                        var encabezadoTransferencia = "Transferencia";
                        var encabezadoFecha = "Fecha";
                        var encabezadoMotivo = "Concepto";

                        PdfPCell columnaTituloRealizo = new PdfPCell(new Phrase(encabezadoRealizo, fuenteNegritaDepositoRetiro));
                        columnaTituloRealizo.BorderWidth = 0;
                        columnaTituloRealizo.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloRealizo.Padding = 3;

                        PdfPCell columnaTituloEfectivo = new PdfPCell(new Phrase(encabezadoEfectivo, fuenteNegritaDepositoRetiro));
                        columnaTituloEfectivo.BorderWidth = 0;
                        columnaTituloEfectivo.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloEfectivo.Padding = 3;

                        PdfPCell columnaTituloTarjeta = new PdfPCell(new Phrase(encabezadoTarjeta, fuenteNegritaDepositoRetiro));
                        columnaTituloTarjeta.BorderWidth = 0;
                        columnaTituloTarjeta.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloTarjeta.Padding = 0;

                        PdfPCell columnaTituloVales = new PdfPCell(new Phrase(encabezadoVales, fuenteNegritaDepositoRetiro));
                        columnaTituloVales.BorderWidth = 0;
                        columnaTituloVales.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloVales.Padding = 3;

                        PdfPCell columnaTituloCheques = new PdfPCell(new Phrase(encabezadoCheque, fuenteNegritaDepositoRetiro));
                        columnaTituloCheques.BorderWidth = 0;
                        columnaTituloCheques.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloCheques.Padding = 3;

                        PdfPCell columnaTituloTransferencias = new PdfPCell(new Phrase(encabezadoTransferencia, fuenteNegritaDepositoRetiro));
                        columnaTituloTransferencias.BorderWidth = 0;
                        columnaTituloTransferencias.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloTransferencias.Padding = 3;

                        PdfPCell columnaTituloFechas = new PdfPCell(new Phrase(encabezadoFecha, fuenteNegritaDepositoRetiro));
                        columnaTituloFechas.BorderWidth = 0;
                        columnaTituloFechas.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloFechas.Padding = 3;

                        PdfPCell columnaTituloMotivo = new PdfPCell(new Phrase(encabezadoMotivo, fuenteNegritaDepositoRetiro));
                        columnaTituloMotivo.BorderWidth = 0;
                        columnaTituloMotivo.HorizontalAlignment = Element.ALIGN_CENTER;
                        columnaTituloMotivo.Padding = 3;

                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloRealizo);
                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloEfectivo);
                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloTarjeta);
                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloVales);
                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloCheques);
                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloTransferencias);
                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloFechas);
                        tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTituloMotivo);

                        for (int i = 0; i < dtHistorialRetiros.Rows.Count; i++)
                        {
                            var conceptoRealizo = dtHistorialRetiros.Rows[i]["Realizo"].ToString();
                            var conceptoEfectivo = Convert.ToDecimal(dtHistorialRetiros.Rows[i]["Efectivo"].ToString());
                            var conceptoTarjeta = Convert.ToDecimal(dtHistorialRetiros.Rows[i]["Tarjeta"].ToString());
                            var conceptoVales = Convert.ToDecimal(dtHistorialRetiros.Rows[i]["Vales"].ToString());
                            var conceptoCheque = Convert.ToDecimal(dtHistorialRetiros.Rows[i]["Cheque"].ToString());
                            var conceptoTransferencia = Convert.ToDecimal(dtHistorialRetiros.Rows[i]["Transferencia"].ToString());
                            var conceptoFecha = dtHistorialRetiros.Rows[i]["Fecha"].ToString();
                            var conceptoMotivo = dtHistorialRetiros.Rows[i]["Concepto"].ToString();

                            PdfPCell columnaRealizo = new PdfPCell(new Phrase(conceptoRealizo, fuenteNormal));
                            columnaRealizo.BorderWidth = 1;
                            columnaRealizo.HorizontalAlignment = Element.ALIGN_LEFT;
                            columnaRealizo.Padding = 3;

                            PdfPCell columnaEfectivo = new PdfPCell(new Phrase(conceptoEfectivo.ToString("C2"), fuenteNormal));
                            columnaEfectivo.BorderWidth = 1;
                            columnaEfectivo.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaEfectivo.Padding = 3;

                            PdfPCell columnaTarjeta = new PdfPCell(new Phrase(conceptoTarjeta.ToString("C2"), fuenteNormal));
                            columnaTarjeta.BorderWidth = 1;
                            columnaTarjeta.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaTarjeta.Padding = 3;

                            PdfPCell columnaVales = new PdfPCell(new Phrase(conceptoVales.ToString("C2"), fuenteNormal));
                            columnaVales.BorderWidth = 1;
                            columnaVales.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaVales.Padding = 3;

                            PdfPCell columnaCheques = new PdfPCell(new Phrase(conceptoCheque.ToString("C2"), fuenteNormal));
                            columnaCheques.BorderWidth = 1;
                            columnaCheques.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaCheques.Padding = 3;

                            PdfPCell columnaTransferencias = new PdfPCell(new Phrase(conceptoTransferencia.ToString("C2"), fuenteNormal));
                            columnaTransferencias.BorderWidth = 1;
                            columnaTransferencias.HorizontalAlignment = Element.ALIGN_RIGHT;
                            columnaTransferencias.Padding = 3;

                            PdfPCell columnaFechas = new PdfPCell(new Phrase(conceptoFecha, fuenteNormal));
                            columnaFechas.BorderWidth = 1;
                            columnaFechas.HorizontalAlignment = Element.ALIGN_CENTER;
                            columnaFechas.Padding = 3;

                            PdfPCell columnaMotivo = new PdfPCell(new Phrase(conceptoMotivo, fuenteNormal));
                            columnaMotivo.BorderWidth = 1;
                            columnaMotivo.HorizontalAlignment = Element.ALIGN_LEFT;
                            columnaMotivo.Padding = 3;

                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaRealizo);
                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaEfectivo);
                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTarjeta);
                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaVales);
                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaCheques);
                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaTransferencias);
                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaFechas);
                            tablaHistorialRetirosDespuesDelCorte.AddCell(columnaMotivo);
                        }

                        reporte.Add(tablaHistorialRetirosDespuesDelCorte);

                        using (DataTable dtSumaDeRetiros = cn.CargarDatos(cs.cargarHistorialRetirosEmpleadoSumaTotal(idUltimoCorteDeCaja, FormPrincipal.id_empleado)))
                        {
                            if (!dtSumaDeRetiros.Rows.Count.Equals(0))
                            {
                                PdfPTable tablaSumaHistorialRetirosDespuesDelCorte = new PdfPTable(8);
                                tablaSumaHistorialRetirosDespuesDelCorte.WidthPercentage = 100;
                                tablaSumaHistorialRetirosDespuesDelCorte.SetWidths(anchoColumnasTablasDepositosRetiros);
                                tablaSumaHistorialRetirosDespuesDelCorte.DefaultCell.Border = (int)BorderStyle.FixedSingle;

                                foreach (DataRow item in dtSumaDeRetiros.Rows)
                                {
                                    var conceptoRealizo = string.Empty;
                                    var conceptoEfectivo = Convert.ToDecimal(item["Efectivo"].ToString());
                                    var conceptoTarjeta = Convert.ToDecimal(item["Tarjeta"].ToString());
                                    var conceptoVales = Convert.ToDecimal(item["Vales"].ToString());
                                    var conceptoCheque = Convert.ToDecimal(item["Cheque"].ToString());
                                    var conceptoTransferencia = Convert.ToDecimal(item["Transferencia"].ToString());
                                    var conceptoFecha = string.Empty;
                                    var conceptoMotivo = string.Empty;

                                    PdfPCell columnaRealizo = new PdfPCell(new Phrase(conceptoRealizo, fuenteNormal));
                                    columnaRealizo.BorderWidth = 0;
                                    columnaRealizo.HorizontalAlignment = Element.ALIGN_LEFT;
                                    columnaRealizo.Padding = 3;

                                    PdfPCell columnaEfectivo = new PdfPCell(new Phrase(conceptoEfectivo.ToString("C2"), fuenteNormal));
                                    columnaEfectivo.BorderWidth = 1;
                                    columnaEfectivo.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaEfectivo.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaEfectivo.Padding = 3;

                                    PdfPCell columnaTarjeta = new PdfPCell(new Phrase(conceptoTarjeta.ToString("C2"), fuenteNormal));
                                    columnaTarjeta.BorderWidth = 1;
                                    columnaTarjeta.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaTarjeta.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaTarjeta.Padding = 3;

                                    PdfPCell columnaVales = new PdfPCell(new Phrase(conceptoVales.ToString("C2"), fuenteNormal));
                                    columnaVales.BorderWidth = 1;
                                    columnaVales.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaVales.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaVales.Padding = 3;

                                    PdfPCell columnaCheques = new PdfPCell(new Phrase(conceptoCheque.ToString("C2"), fuenteNormal));
                                    columnaCheques.BorderWidth = 1;
                                    columnaCheques.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaCheques.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaCheques.Padding = 3;

                                    PdfPCell columnaTransferencias = new PdfPCell(new Phrase(conceptoTransferencia.ToString("C2"), fuenteNormal));
                                    columnaTransferencias.BorderWidth = 1;
                                    columnaTransferencias.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    columnaTransferencias.BackgroundColor = new BaseColor(Color.LightSkyBlue);
                                    columnaTransferencias.Padding = 3;

                                    PdfPCell columnaFechas = new PdfPCell(new Phrase(conceptoFecha, fuenteNormal));
                                    columnaFechas.BorderWidth = 0;
                                    columnaFechas.HorizontalAlignment = Element.ALIGN_CENTER;
                                    columnaFechas.Padding = 3;

                                    PdfPCell columnaMotivo = new PdfPCell(new Phrase(conceptoMotivo, fuenteNormal));
                                    columnaMotivo.BorderWidth = 0;
                                    columnaMotivo.HorizontalAlignment = Element.ALIGN_LEFT;
                                    columnaMotivo.Padding = 3;

                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaRealizo);
                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaEfectivo);
                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaTarjeta);
                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaVales);
                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaCheques);
                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaTransferencias);
                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaFechas);
                                    tablaSumaHistorialRetirosDespuesDelCorte.AddCell(columnaMotivo);
                                }

                                reporte.Add(tablaSumaHistorialRetirosDespuesDelCorte);
                            }
                        }
                    }
                }
            }
            #endregion
            #endregion
            
            #endregion

            reporte.AddTitle("Reporte Corte de Caja");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }

        private int obtenerIdCajaUltimoCorteDeCaja(DataTable dtSaldoInicial)
        {
            var id = 0;

            if (!dtSaldoInicial.Rows.Count.Equals(0))
            {
                foreach (DataRow item in dtSaldoInicial.Rows)
                {
                    id = Convert.ToInt32(item["IDCaja"].ToString());
                }
            }

            return id;
        }

        private decimal obtenerCantidadRetiradaAlCorteDeCaja(DataTable dtSaldoInicial)
        {
            var cantidad = 0m;

            if (!dtSaldoInicial.Rows.Count.Equals(0))
            {
                foreach (DataRow item in dtSaldoInicial.Rows)
                {
                    cantidad = Convert.ToDecimal(item["CantidadRetirada"].ToString());
                }
            }

            return cantidad;
        }

        private void GenerarReporte()
        {
            var datosCajaShow = cargarDatosCorteCaja();
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
            obtenerRutaPDF = rutaArchivo;

            Paragraph Usuario = new Paragraph();
            Paragraph Empleado = new Paragraph();

            var UsuarioActivo = cs.validarEmpleado(FormPrincipal.userNickName, true);
            var obtenerUsuarioPrincipal = cs.validarEmpleadoPorID();

            Usuario = new Paragraph($"USUARIO: ADMIN ({obtenerUsuarioPrincipal})", fuenteNegrita);
            if (!string.IsNullOrEmpty(UsuarioActivo))
            {
                Empleado = new Paragraph($"EMPLEADO: {UsuarioActivo}", fuenteNegrita);
            }

            var numFolio = obtenerFolioCorte();

            Document reporte = new Document(PageSize.A3);
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));
            Paragraph NumeroFolio = new Paragraph("No. Folio: " + numFolio, fuenteGrande);

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
            Paragraph subTitulo = new Paragraph("CORTE DE CAJA\nFecha: " + fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            NumeroFolio.Alignment = Element.ALIGN_CENTER;
            if (!string.IsNullOrEmpty(UsuarioActivo)) { Empleado.Alignment = Element.ALIGN_CENTER; }

            reporte.Add(titulo);
            reporte.Add(Usuario);
            if (!string.IsNullOrEmpty(UsuarioActivo)) { reporte.Add(Empleado); }
            reporte.Add(NumeroFolio);
            reporte.Add(subTitulo);

            //===========================================
            //===       TABLAS DE CORTE DE CAJA       ===
            //===========================================
            #region Tabla de Venta

            // Cantidades de las columnas
            var cantidades = cantidadesReporte;

            float[] anchoColumnas = new float[] { 120f, 80f, 100f, 100f, 100f, 100f, 100f, 100f, 120f, 80f };

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            // Encabezados
            PdfPTable tabla = new PdfPTable(10);
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

            PdfPCell colRetirado = new PdfPCell(new Phrase("DINERO RETIRADO", fuenteNegrita));
            colRetirado.Colspan = 2;
            colRetirado.BorderWidth = 0;
            colRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetirado.Padding = 3;

            PdfPCell colTotal = new PdfPCell(new Phrase("TOTAL EN CAJA", fuenteNegrita));
            colTotal.Colspan = 2;
            colTotal.BorderWidth = 0;
            colTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotal.Padding = 3;

            tabla.AddCell(colVentas);
            tabla.AddCell(colAnticipos);
            tabla.AddCell(colDinero);
            tabla.AddCell(colRetirado);
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

            PdfPCell colEfectivoDineroRetirado = new PdfPCell(new Phrase($"Efectivo", fuenteNormal));/////////
            colEfectivoDineroRetirado.BorderWidth = 0;
            colEfectivoDineroRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoDineroRetirado.Padding = 3;

            PdfPCell colEfectivoDineroR = new PdfPCell(new Phrase($"{datosCajaShow[21]}", fuenteNormal));//agregar datos de cantidades
            colEfectivoDineroR.BorderWidth = 0;
            colEfectivoDineroR.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoDineroR.Padding = 3;

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
            tabla.AddCell(colEfectivoDineroRetirado);
            tabla.AddCell(colEfectivoDineroR);
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

            PdfPCell colTarjetaDineroRetirado = new PdfPCell(new Phrase($"Tarjeta", fuenteNormal));///////////
            colTarjetaDineroRetirado.BorderWidth = 0;
            colTarjetaDineroRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaDineroRetirado.Padding = 3;

            PdfPCell colTarjetaDineroR = new PdfPCell(new Phrase($"{datosCajaShow[22]}", fuenteNormal));/////////////
            colTarjetaDineroR.BorderWidth = 0;
            colTarjetaDineroR.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaDineroR.Padding = 3;

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
            tabla.AddCell(colTarjetaDineroRetirado);
            tabla.AddCell(colTarjetaDineroR);
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

            PdfPCell colValesDineroRetirado = new PdfPCell(new Phrase($"Vales", fuenteNormal));////////////////
            colValesDineroRetirado.BorderWidth = 0;
            colValesDineroRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesDineroRetirado.Padding = 3;

            PdfPCell colValesDineroR = new PdfPCell(new Phrase($"{datosCajaShow[23]}", fuenteNormal));//////////////
            colValesDineroR.BorderWidth = 0;
            colValesDineroR.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesDineroR.Padding = 3;

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
            tabla.AddCell(colValesDineroRetirado);
            tabla.AddCell(colValesDineroR);
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

            PdfPCell colChequeDineroRetirado = new PdfPCell(new Phrase($"Cheque", fuenteNormal));///////////////////
            colChequeDineroRetirado.BorderWidth = 0;
            colChequeDineroRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeDineroRetirado.Padding = 3;

            PdfPCell colChequeDineroR = new PdfPCell(new Phrase($"{datosCajaShow[24]}", fuenteNormal));/////////////////
            colChequeDineroR.BorderWidth = 0;
            colChequeDineroR.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeDineroR.Padding = 3;

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
            tabla.AddCell(colChequeDineroRetirado);
            tabla.AddCell(colChequeDineroR);
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

            PdfPCell colTransDineroRetirado = new PdfPCell(new Phrase($"Transferencia", fuenteNormal));///////////
            colTransDineroRetirado.BorderWidth = 0;
            colTransDineroRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransDineroRetirado.Padding = 3;

            PdfPCell colTransDineroR = new PdfPCell(new Phrase($"{datosCajaShow[25]}", fuenteNormal));/////////////
            colTransDineroR.BorderWidth = 0;
            colTransDineroR.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransDineroR.Padding = 3;

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
            tabla.AddCell(colTransDineroRetirado);
            tabla.AddCell(colTransDineroR);
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

            //PdfPCell colCreditoAnticiposRetirado = new PdfPCell(new Phrase($"Anticipos utilizados al corte", fuenteNormal));/////////////
            //colCreditoAnticiposRetirado.BorderWidth = 0;
            //colCreditoAnticiposRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            //colCreditoAnticiposRetirado.Padding = 3;

            //PdfPCell colCreditoAnticiposTotalRetirado = new PdfPCell(new Phrase($"{datosCajaShow[26]}", fuenteNormal));////////////
            //colCreditoAnticiposTotalRetirado.BorderWidth = 0;
            //colCreditoAnticiposTotalRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            //colCreditoAnticiposTotalRetirado.Padding = 3;
            PdfPCell colAbonosDevolucionesRetirado = new PdfPCell(new Phrase($"Devoluciones", fuenteNormal));/////////////
            colAbonosDevolucionesRetirado.BorderWidth = 0;
            colAbonosDevolucionesRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            colAbonosDevolucionesRetirado.Padding = 3;

            PdfPCell colAbonosDevolucionesR = new PdfPCell(new Phrase($"{datosCajaShow[27]}", fuenteNormal));////////////
            colAbonosDevolucionesR.BorderWidth = 0;
            colAbonosDevolucionesR.HorizontalAlignment = Element.ALIGN_CENTER;
            colAbonosDevolucionesR.Padding = 3;

            PdfPCell colSaldoInicial_IV = new PdfPCell(new Phrase($"Saldo inicial", fuenteNormal));
            colSaldoInicial_IV.BorderWidth = 0;
            colSaldoInicial_IV.HorizontalAlignment = Element.ALIGN_CENTER;
            colSaldoInicial_IV.Padding = 3;

            PdfPCell colSaldoInicialC_IV = new PdfPCell(new Phrase($"{cantidades[30]}", fuenteNormal));
            colSaldoInicialC_IV.BorderWidth = 0;
            colSaldoInicialC_IV.HorizontalAlignment = Element.ALIGN_CENTER;
            colSaldoInicialC_IV.Padding = 3;

            tabla.AddCell(colCreditoVentas);
            tabla.AddCell(colCreditoVentasC);
            tabla.AddCell(colCreditoAnticipos);
            tabla.AddCell(colCreditoAnticiposC);
            tabla.AddCell(colCreditoDinero);
            tabla.AddCell(colCreditoDineroC);
            tabla.AddCell(colAbonosDevolucionesRetirado);
            tabla.AddCell(colAbonosDevolucionesR);
            tabla.AddCell(colSaldoInicial_IV);
            tabla.AddCell(colSaldoInicialC_IV);

            // Linea de Abonos
            PdfPCell colAbonosVentas = new PdfPCell(new Phrase($"Abonos", fuenteNormal));
            colAbonosVentas.BorderWidth = 0;
            colAbonosVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colAbonosVentas.Padding = 3;

            PdfPCell colAbonosVentasC = new PdfPCell(new Phrase($"{datosCajaShow[6]}", fuenteNormal));
            colAbonosVentasC.BorderWidth = 0;
            colAbonosVentasC.HorizontalAlignment = Element.ALIGN_CENTER;
            colAbonosVentasC.Padding = 3;

            PdfPCell colAbonosUtilizados = new PdfPCell(new Phrase("", fuenteNormal));
            colAbonosUtilizados.BorderWidth = 0;
            colAbonosUtilizados.HorizontalAlignment = Element.ALIGN_CENTER;
            colAbonosUtilizados.Padding = 3;

            PdfPCell colAbonosUtilizadosC = new PdfPCell(new Phrase("", fuenteNormal));
            colAbonosUtilizadosC.BorderWidth = 0;
            colAbonosUtilizadosC.HorizontalAlignment = Element.ALIGN_CENTER;
            colAbonosUtilizadosC.Padding = 3;

            PdfPCell colAbonosDinero = new PdfPCell(new Phrase("", fuenteNormal));
            colAbonosDinero.BorderWidth = 0;
            colAbonosDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colAbonosDinero.Padding = 3;

            PdfPCell colAbonosDineroC = new PdfPCell(new Phrase("", fuenteNormal));
            colAbonosDineroC.BorderWidth = 0;
            colAbonosDineroC.HorizontalAlignment = Element.ALIGN_CENTER;
            colAbonosDineroC.Padding = 3;

            //PdfPCell colAbonosDevolucionesRetirado = new PdfPCell(new Phrase($"Devoluciones", fuenteNormal));/////////////
            //colAbonosDevolucionesRetirado.BorderWidth = 0;
            //colAbonosDevolucionesRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            //colAbonosDevolucionesRetirado.Padding = 3;

            //PdfPCell colAbonosDevolucionesR = new PdfPCell(new Phrase($"{datosCajaShow[27]}", fuenteNormal));////////////
            //colAbonosDevolucionesR.BorderWidth = 0;
            //colAbonosDevolucionesR.HorizontalAlignment = Element.ALIGN_CENTER;
            //colAbonosDevolucionesR.Padding = 3;
            PdfPCell colCreditoAnticiposRetirado = new PdfPCell(new Phrase("", fuenteNormal));///////////// Anticipos
            colCreditoAnticiposRetirado.BorderWidth = 0;
            colCreditoAnticiposRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoAnticiposRetirado.Padding = 3;

            PdfPCell colCreditoAnticiposTotalRetirado = new PdfPCell(new Phrase("", fuenteNormal));////////////Anticipos
            colCreditoAnticiposTotalRetirado.BorderWidth = 0;
            colCreditoAnticiposTotalRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoAnticiposTotalRetirado.Padding = 3;

            PdfPCell colCreditoTotal = new PdfPCell(new Phrase($"Total Crédito", fuenteNormal));
            colCreditoTotal.BorderWidth = 0;
            colCreditoTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoTotal.Padding = 3;

            PdfPCell colCreditoTotalC = new PdfPCell(new Phrase($"{cantidades[21]}", fuenteNormal));
            colCreditoTotalC.BorderWidth = 0;
            colCreditoTotalC.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoTotalC.Padding = 3;

            tabla.AddCell(colAbonosVentas);
            tabla.AddCell(colAbonosVentasC);
            tabla.AddCell(colAbonosUtilizados);
            tabla.AddCell(colAbonosUtilizadosC);
            tabla.AddCell(colAbonosDinero);
            tabla.AddCell(colAbonosDineroC);
            tabla.AddCell(colCreditoAnticiposRetirado);
            tabla.AddCell(colCreditoAnticiposTotalRetirado);
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

            PdfPCell colAnticiposRetirado = new PdfPCell(new Phrase($"", fuenteNormal));/////////////
            colAnticiposRetirado.BorderWidth = 0;
            colAnticiposRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposRetirado.Padding = 3;

            PdfPCell colAnticiposTotalRetirado = new PdfPCell(new Phrase($"", fuenteNormal));////////////
            colAnticiposTotalRetirado.BorderWidth = 0;
            colAnticiposTotalRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposTotalRetirado.Padding = 3;

            PdfPCell colAnticiposTotal = new PdfPCell(new Phrase("", fuenteNormal));
            colAnticiposTotal.BorderWidth = 0;
            colAnticiposTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposTotal.Padding = 3;

            PdfPCell colAnticiposTotalC = new PdfPCell(new Phrase("", fuenteNormal));
            colAnticiposTotalC.BorderWidth = 0;
            colAnticiposTotalC.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposTotalC.Padding = 3;

            tabla.AddCell(colAnticiposVentas);
            tabla.AddCell(colAnticiposVentasC);
            tabla.AddCell(colAnticiposUtilizados);
            tabla.AddCell(colAnticiposUtilizadosC);
            tabla.AddCell(colAnticiposDinero);
            tabla.AddCell(colAnticiposDineroC);
            tabla.AddCell(colAnticiposRetirado);
            tabla.AddCell(colAnticiposTotalRetirado);
            tabla.AddCell(colAnticiposTotal);
            tabla.AddCell(colAnticiposTotalC);

            // Linea de SALDO INICIAL
            //PdfPCell colSaldoInicial_I = new PdfPCell(new Phrase("", fuenteNormal));
            //colSaldoInicial_I.Colspan = 2;
            //colSaldoInicial_I.BorderWidth = 0;
            //colSaldoInicial_I.HorizontalAlignment = Element.ALIGN_CENTER;
            //colSaldoInicial_I.Padding = 3;

            //PdfPCell colSaldoInicial_II = new PdfPCell(new Phrase("", fuenteNormal));
            //colSaldoInicial_II.Colspan = 2;
            //colSaldoInicial_II.BorderWidth = 0;
            //colSaldoInicial_II.HorizontalAlignment = Element.ALIGN_CENTER;
            //colSaldoInicial_II.Padding = 3;

            //PdfPCell colSaldoInicial_III = new PdfPCell(new Phrase("", fuenteNormal));
            //colSaldoInicial_III.Colspan = 2;
            //colSaldoInicial_III.BorderWidth = 0;
            //colSaldoInicial_III.HorizontalAlignment = Element.ALIGN_CENTER;
            //colSaldoInicial_III.Padding = 3;

            //PdfPCell colSaldoInicial_IV = new PdfPCell(new Phrase($"Saldo inicial", fuenteNormal));
            //colSaldoInicial_IV.BorderWidth = 0;
            //colSaldoInicial_IV.HorizontalAlignment = Element.ALIGN_CENTER;
            //colSaldoInicial_IV.Padding = 3;

            //PdfPCell colSaldoInicialC_IV = new PdfPCell(new Phrase($"{cantidades[30]}", fuenteNormal));
            //colSaldoInicialC_IV.BorderWidth = 0;
            //colSaldoInicialC_IV.HorizontalAlignment = Element.ALIGN_CENTER;
            //colSaldoInicialC_IV.Padding = 3;

            //tabla.AddCell(colSaldoInicial_I);
            //tabla.AddCell(colSaldoInicial_II);
            //tabla.AddCell(colSaldoInicial_III);
            //tabla.AddCell(colSaldoInicial_IV);
            //tabla.AddCell(colSaldoInicialC_IV);

            // Linea de SUBTOTAL
            //PdfPCell colSubVentas = new PdfPCell(new Phrase("", fuenteNormal));
            //colSubVentas.Colspan = 2;
            //colSubVentas.BorderWidth = 0;
            //colSubVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            //colSubVentas.Padding = 3;

            //PdfPCell colSubAnticipos = new PdfPCell(new Phrase("", fuenteNormal));
            //colSubAnticipos.Colspan = 2;
            //colSubAnticipos.BorderWidth = 0;
            //colSubAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            //colSubAnticipos.Padding = 3;

            //PdfPCell colSubDinero = new PdfPCell(new Phrase("", fuenteNormal));
            //colSubDinero.Colspan = 2;
            //colSubDinero.BorderWidth = 0;
            //colSubDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            //colSubDinero.Padding = 3;

            //PdfPCell colSubTotal = new PdfPCell(new Phrase($"Subtotal en caja", fuenteNormal));
            //colSubTotal.BorderWidth = 0;
            //colSubTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            //colSubTotal.Padding = 3;

            //PdfPCell colSubTotalC = new PdfPCell(new Phrase($"{cantidades[24]}", fuenteNormal));
            //colSubTotalC.BorderWidth = 0;
            //colSubTotalC.HorizontalAlignment = Element.ALIGN_CENTER;
            //colSubTotalC.Padding = 3;

            //tabla.AddCell(colSubVentas);
            //tabla.AddCell(colSubAnticipos);
            //tabla.AddCell(colSubDinero);
            //tabla.AddCell(colSubTotal);
            //tabla.AddCell(colSubTotalC);

            // Linea de RETIRADO
            //PdfPCell colRetiradoVentas = new PdfPCell(new Phrase("", fuenteNormal));
            //colRetiradoVentas.Colspan = 2;
            //colRetiradoVentas.BorderWidth = 0;
            //colRetiradoVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            //colRetiradoVentas.Padding = 3;

            //PdfPCell colRetiradoAnticipos = new PdfPCell(new Phrase("", fuenteNormal));
            //colRetiradoAnticipos.Colspan = 2;
            //colRetiradoAnticipos.BorderWidth = 0;
            //colRetiradoAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            //colRetiradoAnticipos.Padding = 3;

            //PdfPCell colRetiradoDinero = new PdfPCell(new Phrase("", fuenteNormal));
            //colRetiradoDinero.Colspan = 2;
            //colRetiradoDinero.BorderWidth = 0;
            //colRetiradoDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            //colRetiradoDinero.Padding = 3;

            //PdfPCell colRetiradoTotal = new PdfPCell(new Phrase($"Dinero retirado", fuenteNormal));
            //colRetiradoTotal.BorderWidth = 0;
            //colRetiradoTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            //colRetiradoTotal.Padding = 3;

            //PdfPCell colRetiradoTotalC = new PdfPCell(new Phrase($"{cantidades[25]}", fuenteNormal));
            //colRetiradoTotalC.BorderWidth = 0;
            //colRetiradoTotalC.HorizontalAlignment = Element.ALIGN_CENTER;
            //colRetiradoTotalC.Padding = 3;

            //tabla.AddCell(colRetiradoVentas);
            //tabla.AddCell(colRetiradoAnticipos);
            //tabla.AddCell(colRetiradoDinero);
            //tabla.AddCell(colRetiradoTotal);
            //tabla.AddCell(colRetiradoTotalC);

            //PdfPCell colSeparador = new PdfPCell(new Phrase(Chunk.NEWLINE));
            //colSeparador.Colspan = 10;
            //colSeparador.BorderWidth = 0;

            //tabla.AddCell(colSeparador);

            // Linea de Dinero Retirado en el Corte
            PdfPCell colVacioPrimero = new PdfPCell(new Phrase(string.Empty, fuenteTotales));
            colVacioPrimero.BorderWidth = 0;
            colVacioPrimero.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioPrimero.Padding = 3;

            PdfPCell colVacioSegundo = new PdfPCell(new Phrase(string.Empty, fuenteTotales));
            colVacioSegundo.BorderWidth = 0;
            colVacioSegundo.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioSegundo.Padding = 3;

            PdfPCell colVaciotercero = new PdfPCell(new Phrase(string.Empty, fuenteTotales));
            colVaciotercero.BorderWidth = 0;
            colVaciotercero.HorizontalAlignment = Element.ALIGN_CENTER;
            colVaciotercero.Padding = 3;

            PdfPCell colVacioCuarto = new PdfPCell(new Phrase(string.Empty, fuenteTotales));
            colVacioCuarto.BorderWidth = 0;
            colVacioCuarto.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioCuarto.Padding = 3;

            PdfPCell colVacioQuinto = new PdfPCell(new Phrase(string.Empty, fuenteTotales));
            colVacioQuinto.BorderWidth = 0;
            colVacioQuinto.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioQuinto.Padding = 3;

            PdfPCell colVacioSexto = new PdfPCell(new Phrase(string.Empty, fuenteTotales));
            colVacioSexto.BorderWidth = 0;
            colVacioSexto.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioSexto.Padding = 3;

            PdfPCell colVacioSeptimo = new PdfPCell(new Phrase(string.Empty, fuenteTotales));////////
            colVacioSeptimo.BorderWidth = 0;
            colVacioSeptimo.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioSeptimo.Padding = 3;

            PdfPCell colVacioOctavo = new PdfPCell(new Phrase(string.Empty, fuenteTotales));//////////////
            colVacioOctavo.BorderWidth = 0;
            colVacioOctavo.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioOctavo.Padding = 3;

            PdfPCell colVacioNoveno = new PdfPCell(new Phrase($"Cantidad retirada al corte:", fuenteNegrita));
            colVacioNoveno.BorderWidth = 0;
            colVacioNoveno.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioNoveno.Padding = 3;

            PdfPCell colVacioDecimo = new PdfPCell(new Phrase($"{AgregarRetirarDinero.totalRetiradoCorte}", fuenteNegrita));
            colVacioDecimo.BorderWidth = 0;
            colVacioDecimo.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioDecimo.Padding = 3;

            tabla.AddCell(colVacioPrimero);
            tabla.AddCell(colVacioSegundo);
            tabla.AddCell(colVaciotercero);
            tabla.AddCell(colVacioCuarto);
            tabla.AddCell(colVacioQuinto);
            tabla.AddCell(colVacioSexto);
            tabla.AddCell(colVacioSeptimo);
            tabla.AddCell(colVacioOctavo);
            tabla.AddCell(colVacioNoveno);
            tabla.AddCell(colVacioDecimo);

            saldoInicial = mb.SaldoInicialCaja(FormPrincipal.userID);

            // Linea de Total en caja despues del corte
            PdfPCell colVacioPrimeroSaldo = new PdfPCell(new Phrase(string.Empty, fuenteTotales));
            colVacioPrimeroSaldo.BorderWidth = 0;
            colVacioPrimeroSaldo.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioPrimeroSaldo.Padding = 3;

            PdfPCell colVacioSegundoSaldo = new PdfPCell(new Phrase(string.Empty, fuenteTotales));
            colVacioSegundoSaldo.BorderWidth = 0;
            colVacioSegundoSaldo.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioSegundoSaldo.Padding = 3;

            PdfPCell colVacioterceroSaldo = new PdfPCell(new Phrase(string.Empty, fuenteTotales));
            colVacioterceroSaldo.BorderWidth = 0;
            colVacioterceroSaldo.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioterceroSaldo.Padding = 3;

            PdfPCell colVacioCuartoSaldo = new PdfPCell(new Phrase(string.Empty, fuenteTotales));
            colVacioCuartoSaldo.BorderWidth = 0;
            colVacioCuartoSaldo.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioCuartoSaldo.Padding = 3;

            PdfPCell colVacioQuintoSaldo = new PdfPCell(new Phrase(string.Empty, fuenteTotales));
            colVacioQuintoSaldo.BorderWidth = 0;
            colVacioQuintoSaldo.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioQuintoSaldo.Padding = 3;

            PdfPCell colVacioSextoSaldo = new PdfPCell(new Phrase(string.Empty, fuenteTotales));
            colVacioSextoSaldo.BorderWidth = 0;
            colVacioSextoSaldo.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioSextoSaldo.Padding = 3;

            PdfPCell colVacioSeptimoSaldo = new PdfPCell(new Phrase(string.Empty, fuenteTotales));////////
            colVacioSeptimoSaldo.BorderWidth = 0;
            colVacioSeptimoSaldo.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioSeptimoSaldo.Padding = 3;

            PdfPCell colVacioOctavoSaldo = new PdfPCell(new Phrase(string.Empty, fuenteTotales));//////////////
            colVacioOctavoSaldo.BorderWidth = 0;
            colVacioOctavoSaldo.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioOctavoSaldo.Padding = 3;

            PdfPCell colVacioNovenoSaldo = new PdfPCell(new Phrase($"Total en Caja antes del corte:", fuenteNegrita));
            colVacioNovenoSaldo.BorderWidth = 0;
            colVacioNovenoSaldo.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioNovenoSaldo.Padding = 3;

            PdfPCell colVacioDecimoSaldo = new PdfPCell(new Phrase($"{cantidades[29]}", fuenteNegrita));
            colVacioDecimoSaldo.BorderWidth = 0;
            colVacioDecimoSaldo.HorizontalAlignment = Element.ALIGN_CENTER;
            colVacioDecimoSaldo.Padding = 3;

            tabla.AddCell(colVacioPrimeroSaldo);
            tabla.AddCell(colVacioSegundoSaldo);
            tabla.AddCell(colVacioterceroSaldo);
            tabla.AddCell(colVacioCuartoSaldo);
            tabla.AddCell(colVacioQuintoSaldo);
            tabla.AddCell(colVacioSextoSaldo);
            tabla.AddCell(colVacioSeptimoSaldo);
            tabla.AddCell(colVacioOctavoSaldo);
            tabla.AddCell(colVacioNovenoSaldo);
            tabla.AddCell(colVacioDecimoSaldo);

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

            PdfPCell colTotalDineroRetirado = new PdfPCell(new Phrase($"Total Retirado", fuenteTotales));////////
            colTotalDineroRetirado.BorderWidth = 0;
            colTotalDineroRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalDineroRetirado.Padding = 3;
            colTotalDineroRetirado.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colTotalDineroR = new PdfPCell(new Phrase($"{datosCajaShow[28]}", fuenteTotales));//////////////
            colTotalDineroR.BorderWidth = 0;
            colTotalDineroR.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalDineroR.Padding = 3;
            colTotalDineroR.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colTotalFinal = new PdfPCell(new Phrase($"Total en Caja despues del corte", fuenteTotales));
            colTotalFinal.BorderWidth = 0;
            colTotalFinal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalFinal.Padding = 3;
            colTotalFinal.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colTotalFinalC = new PdfPCell(new Phrase($"${saldoInicial.ToString("0.00")}", fuenteTotales));
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
            tabla.AddCell(colTotalDineroRetirado);
            tabla.AddCell(colTotalDineroR);
            tabla.AddCell(colTotalFinal);
            tabla.AddCell(colTotalFinalC);

            #endregion
            //===========================================
            //===    FIN  TABLAS DE CORTE DE CAJA     ===
            //===========================================

            reporte.Add(tabla);
            reporte.Add(linea);

            //===============================
            //===    TABLA DE DEPOSITOS   ===
            //===============================
            var procedencia = "Caja";
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

            //var dr = cdc.obtenerDepositosRetiros(procedencia, "deposito");

            while (dr.HasRows)
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
                    //foreach (DataRow item in dr.Rows)
                    //{
                    var efectivo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Efectivo"))).ToString("0.00");
                    var tarjeta = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Tarjeta"))).ToString("0.00");
                    var vales = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vales"))).ToString("0.00");
                    var cheque = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cheque"))).ToString("0.00");
                    var trans = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Transferencia"))).ToString("0.00");
                    var fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                    //var efectivo = Convert.ToDouble(item["Efectivo"]).ToString("0.00");
                    //var tarjeta = Convert.ToDouble(item["Tarjeta"]).ToString("0.00");
                    //var vales = Convert.ToDouble(item["Vales"]).ToString("0.00");
                    //var cheque = Convert.ToDouble(item["Cheque"]).ToString("0.00");
                    //var trans = Convert.ToDouble(item["Transferencia"]).ToString("0.00");
                    //var fecha = Convert.ToDateTime(item["FechaOperacion"]).ToString("0.00");

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
                    //}

                }

                reporte.Add(tituloDepositos);
                reporte.Add(tablaDepositos);
                reporte.Add(linea);

                dr.Close();
                sql_con.Close();
            }



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

            anchoColumnas = new float[] { 100f, 100f, 100f, 100f, 100f, 100f, 100f };

            sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            sql_con.Open();
            sql_cmd = new MySqlCommand($"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'retiro' AND FechaOperacion > '{fechaGeneral.ToString("yyyy-MM-dd HH:mm:ss")}'", sql_con);
            dr = sql_cmd.ExecuteReader();

            //dr = cdc.obtenerDepositosRetiros(procedencia, "retiro");

            while (dr.HasRows)
            {
                PdfPTable tablaRetiros = new PdfPTable(7);
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

                //PdfPCell colDepositoCreditoR = new PdfPCell(new Phrase("CRÉDITO", fuenteNegrita));
                //colDepositoCreditoR.BorderWidth = 0;
                //colDepositoCreditoR.HorizontalAlignment = Element.ALIGN_CENTER;
                //colDepositoCreditoR.Padding = 3;

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
                //tablaRetiros.AddCell(colDepositoCreditoR);
                tablaRetiros.AddCell(colDepositoFechaR);

                //MySqlCommand sql_cmd;
                //MySqlDataReader dr;

                while (dr.Read())
                {
                    //foreach (DataRow item in dr.Rows)
                    //{
                    var efectivo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Efectivo"))).ToString("0.00");
                    var tarjeta = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Tarjeta"))).ToString("0.00");
                    var vales = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vales"))).ToString("0.00");
                    var cheque = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cheque"))).ToString("0.00");
                    var trans = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Transferencia"))).ToString("0.00");
                    var credito = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Credito"))).ToString("0.00");
                    var fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                    //var efectivo = Convert.ToDouble(item["Efectivo"]).ToString("0.00");
                    //var tarjeta = Convert.ToDouble(item["Tarjeta"]).ToString("0.00");
                    //var vales = Convert.ToDouble(item["Vales"]).ToString("0.00");
                    //var cheque = Convert.ToDouble(item["Cheque"]).ToString("0.00");
                    //var trans = Convert.ToDouble(item["Transferencia"]).ToString("0.00");
                    //var fecha = Convert.ToDateTime(item["FechaOperacion"]).ToString("0.00");

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

                    //PdfPCell colDepositoCreditoTmpR = new PdfPCell(new Phrase("$" + credito, fuenteNormal));
                    //colDepositoCreditoTmpR.BorderWidth = 0;
                    //colDepositoCreditoTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoFechaTmpR = new PdfPCell(new Phrase(fecha, fuenteNormal));
                    colDepositoFechaTmpR.BorderWidth = 0;
                    colDepositoFechaTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    tablaRetiros.AddCell(colEmpleadoTmpR);
                    tablaRetiros.AddCell(colDepositoEfectivoTmpR);
                    tablaRetiros.AddCell(colDepositoTarjetaTmpR);
                    tablaRetiros.AddCell(colDepositoValesTmpR);
                    tablaRetiros.AddCell(colDepositoChequeTmpR);
                    tablaRetiros.AddCell(colDepositoTransTmpR);
                    //tablaRetiros.AddCell(colDepositoCreditoTmpR);
                    tablaRetiros.AddCell(colDepositoFechaTmpR);
                    //}

                }

                reporte.Add(tituloRetiros);
                reporte.Add(tablaRetiros);

                dr.Close();
                sql_con.Close();
            }



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

        private int obtenerFolioCorte()
        {
            var result = 0;

            var query = cn.CargarDatos($"SELECT NumFolio FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY ID DESC LIMIT 1");

            if (!query.Rows.Count.Equals(0))
            {
                result = Convert.ToInt32(query.Rows[0]["NumFolio"].ToString());
            }

            return result;
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
            //if (opcion5 == 0)
            //{
            //    Utilidades.MensajePermiso();
            //    return;
            //}

            //if (!Utilidades.AdobeReaderInstalado())
            //{
            //    Utilidades.MensajeAdobeReader();
            //    return;
            //}

            //var FechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //var datos = new string[] { FormPrincipal.userID.ToString(), "0", "0", "0", "0", "0", "0", "0", "0", "0", "N/A", "1", FechaOperacion, "Apertura de Caja", FormPrincipal.id_empleado.ToString(), "N/A" };
            //cn.EjecutarConsulta(cs.GuardarAperturaDeCaja(datos));

            //Utilidades.GenerarTicketCaja();
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
                lbSaldoInicialInfo.Visible = false;
            }
            else
            {
                lbSaldoInicialInfo.Visible = false;
            }
        }

        private void lbCambioAbonos_Click(object sender, EventArgs e)
        {
            CajaAbonos detalleAbonos = new CajaAbonos();

            var IDUsuario = 0;
            var IDEmpleado = 0;
            var ultimaFechaDeCorte = string.Empty;
            var todosLosAbonos = string.Empty;

            ultimaFechaDeCorte = fechaFormateadaCorteParaAbonos;
            IDUsuario = FormPrincipal.userID;

            if (!FormPrincipal.userNickName.Contains("@"))
            {
                if (opcionComboBoxFiltroAdminEmp.Equals("Admin"))
                {
                    todosLosAbonos = "No";
                    IDEmpleado = 0;
                }
                else if (opcionComboBoxFiltroAdminEmp.Equals("All"))
                {
                    todosLosAbonos = "Si";
                    IDEmpleado = 0;
                }
                else
                {
                    todosLosAbonos = "No";
                    IDEmpleado = Convert.ToInt32(opcionComboBoxFiltroAdminEmp);
                }
            }
            else
            {
                todosLosAbonos = "No";
                IDEmpleado = FormPrincipal.id_empleado;
            }

            detalleAbonos.IDUsuario = IDUsuario;
            detalleAbonos.IDEmpleado = IDEmpleado;
            detalleAbonos.ultimaFechaDeCorteDeCaja = ultimaFechaDeCorte;
            detalleAbonos.todosLosAbonos = todosLosAbonos;
            detalleAbonos.ShowDialog();

            //CajaAbonos mostrarAbonosCaja = Application.OpenForms.OfType<CajaAbonos>().FirstOrDefault();

            //var validarAbono = "abono";
            //if (mostrarAbonosCaja == null)
            //{
            //    abonos_devoluciones = "abonos";
            //    CajaAbonos mostrarAbonos = new CajaAbonos();
            //    mostrarAbonos.ShowDialog();
            //}

            //if (mostrarAbonosCaja != null)
            //{
            //    if (mostrarAbonosCaja.WindowState == FormWindowState.Minimized || mostrarAbonosCaja.WindowState == FormWindowState.Normal)
            //    {
            //        mostrarAbonosCaja.BringToFront();
            //    }
            //}
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

            if (e.KeyCode == Keys.B && (e.Control))// Boton Tabulador
            {
                btnRedondoTabuladorDeDinero.PerformClick();
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

        private void lbSaldoInicialInfoBtn_Click(object sender, EventArgs e)
        {

        }

        private void btnRedondoTabuladorDeDinero_Click(object sender, EventArgs e)
        {
            TabuladorDeDinero tabDeDinero = new TabuladorDeDinero();

            tabDeDinero.ShowDialog();
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

        private void btnRedondoSaldoInicial_Click(object sender, EventArgs e)
        {
            mostrarSaldoInicialDezglozado();
        }

        private void CajaN_Shown(object sender, EventArgs e)
        {

        }

        private void CajaN_Activated(object sender, EventArgs e)
        {
            //this.Refresh();
            //Application.DoEvents();
        }

        private void btnRedondoAgregarDinero_Click(object sender, EventArgs e)
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
                    recargarDatosConCantidades(sender, e);
                    //filtradoInicial(sender, e);
                    //CargarSaldo();
                };

                agregar.Show();
            }
        }

        private void filtradoInicial(object sender)
        {
            //if (!FormPrincipal.userNickName.Contains("@"))
            //{
            //    cbFiltroAdminEmpleado_SelectedIndexChanged(sender, e);
            //}
            //else
            //{
            //    seccionEmpleadoCaja(FormPrincipal.id_empleado.ToString());
            //}
        }

        private void btnRedondoRetirarDinero_Click(object sender, EventArgs e)
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
                    recargarDatosConCantidades(sender, e);
                    //CargarSaldo();
                };

                retirar.Show();
            }
        }

        private void btnRedondoCorteCaja_Click(object sender, EventArgs e)
        {
            corteCaja = 1;

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
                        tipoDeMovimiento = corte.operacion;

                        cn.EjecutarConsulta($"UPDATE Anticipos Set AnticipoAplicado = 0 WHERE IDUsuario = '{FormPrincipal.userID}'");

                        if (Utilidades.AdobeReaderInstalado())
                        {
                            //GenerarReporte();
                            generarNuevoReporte();
                            using (DataTable dtCerrarSesionDesdeCorteCaja = cn.CargarDatos(cs.validarCerrarSesionCorteCaja()))
                            {
                                if (!dtCerrarSesionDesdeCorteCaja.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow item in dtCerrarSesionDesdeCorteCaja.Rows)
                                    {
                                        if (item["CerrarSesionAuto"].ToString().Equals("1"))
                                        {
                                            recargarDatos = true;
                                            clickBotonCorteDeCaja = 1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Utilidades.MensajeAdobeReader();
                        }

                        botones = false;

                        var correo = mb.correoUsuario();
                        var correoCantidades = cargarDatosCorteCaja();

                        // Ejecutar hilo para enviar notificación
                        var datosConfig = mb.ComprobarConfiguracion();

                        if (datosConfig.Count > 0)
                        {
                            if (Convert.ToInt32(datosConfig[20]).Equals(1))
                            {
                                Thread mandarCorreo = new Thread(
                                    () => Utilidades.enviarCorreoCorteCaja(correo, correoCantidades, obtenerRutaPDF)
                                );
                                mandarCorreo.Start();
                            }
                        }
                    }

                    CargarSaldoInicial();
                    recargarDatosConCantidades(sender, e);
                    //CargarSaldo();

                    //var correo = mb.correoUsuario();
                    //var correoCantidades = cargarDatosCorteCaja();

                    //// Ejecutar hilo para enviar notificación
                    //var datosConfig = mb.ComprobarConfiguracion();

                    //if (datosConfig.Count > 0)
                    //{
                    //    if (Convert.ToInt32(datosConfig[20]).Equals(1))
                    //    {
                    //        Thread mandarCorreo = new Thread(
                    //            () => Utilidades.enviarCorreoCorteCaja(correo, correoCantidades, obtenerRutaPDF)
                    //        );
                    //        mandarCorreo.Start();
                    //    }
                    //} 

                    this.Refresh();
                    Application.DoEvents();
                };

                corte.Show();

                //GenerarTicket();
            }
            abonos = 0;
        }

        private void btnRedondoAbrirCaja_Click(object sender, EventArgs e)
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

            var FechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var datos = new string[] { FormPrincipal.userID.ToString(), "0", "0", "0", "0", "0", "0", "0", "0", "0", "N/A", "1", FechaOperacion, "Apertura de Caja", FormPrincipal.id_empleado.ToString(), "N/A" };
            cn.EjecutarConsulta(cs.GuardarAperturaDeCaja(datos));

            //Utilidades.GenerarTicketCaja();
            imprimirUltimoTicket(); 

            recargarDatosConCantidades(sender, e);
        }

        private void imprimirUltimoTicket()
        {
            var Folio = string.Empty;
            var Serie = string.Empty;
            var StatusUltimoTicket = string.Empty;
            var usuarioActivo = FormPrincipal.userNickName;
            var ticket8cm = 0;
            var ticket6cm = 0;

            using (DataTable dtConfiguracionTipoTicket = cn.CargarDatos(cs.tipoDeTicket()))
            {
                if (!dtConfiguracionTipoTicket.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in dtConfiguracionTipoTicket.Rows)
                    {
                        ticket6cm = Convert.ToInt32(item["ticket58mm"].ToString());
                        ticket8cm = Convert.ToInt32(item["ticket80mm"].ToString());
                    }
                }
            }

            if (usuarioActivo.Contains("@"))
            {
                var idVenta = cn.EjecutarSelect($"SELECT * FROM Ventas WHERE IDUsuario = {FormPrincipal.userID} AND IDEmpleado = {FormPrincipal.id_empleado} AND Status = 1 ORDER BY ID DESC LIMIT 1", 1).ToString();

                using (DataTable dtDatosVentas = cn.CargarDatos($"SELECT Folio, Serie, Status FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idVenta}'"))
                {
                    if (!dtDatosVentas.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtDatosVentas.Rows)
                        {
                            Folio = item["Folio"].ToString();
                            Serie = item["Serie"].ToString();
                            StatusUltimoTicket = item["Status"].ToString();
                        }
                    }
                }

                if (Folio.Equals("0"))
                {
                    if (StatusUltimoTicket.Equals("1"))
                    {
                        if (ticket6cm.Equals(1))
                        {
                            if (usuarioActivo.Contains("@"))
                            {
                                using (ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.idEmpleado = FormPrincipal.id_empleado;
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                            else
                            {
                                using (ImprimirTicketCajaAbierta8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbierta8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                        }
                        else if (ticket8cm.Equals(1))
                        {
                            if (usuarioActivo.Contains("@"))
                            {
                                using (ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.idEmpleado = FormPrincipal.id_empleado;
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                            else
                            {
                                using (ImprimirTicketCajaAbierta8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbierta8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                        }
                    }
                }
                else
                {
                    using (DataTable dtConfiguracionTipoTicket = cn.CargarDatos(cs.tipoDeTicket()))
                    {
                        if (!dtConfiguracionTipoTicket.Rows.Count.Equals(0))
                        {
                            var Usuario = 0;
                            var NombreComercial = 0;
                            var Direccion = 0;
                            var ColyCP = 0;
                            var RFC = 0;
                            var Correo = 0;
                            var Telefono = 0;
                            var NombreC = 0;
                            var DomicilioC = 0;
                            var RFCC = 0;
                            var CorreoC = 0;
                            var TelefonoC = 0;
                            var ColyCPC = 0;
                            var FormaPagoC = 0;
                            var logo = 0;
                            var codigoBarraTicket = 0;

                            foreach (DataRow item in dtConfiguracionTipoTicket.Rows)
                            {
                                Usuario = Convert.ToInt32(item["Usuario"].ToString());
                                NombreComercial = Convert.ToInt32(item["NombreComercial"].ToString());
                                Direccion = Convert.ToInt32(item["Direccion"].ToString());
                                ColyCP = Convert.ToInt32(item["ColyCP"].ToString());
                                RFC = Convert.ToInt32(item["RFC"].ToString());
                                Correo = Convert.ToInt32(item["Correo"].ToString());
                                Telefono = Convert.ToInt32(item["Telefono"].ToString());
                                NombreC = Convert.ToInt32(item["NombreC"].ToString());
                                DomicilioC = Convert.ToInt32(item["DomicilioC"].ToString());
                                RFCC = Convert.ToInt32(item["RFCC"].ToString());
                                CorreoC = Convert.ToInt32(item["CorreoC"].ToString());
                                TelefonoC = Convert.ToInt32(item["TelefonoC"].ToString());
                                ColyCPC = Convert.ToInt32(item["ColyCPC"].ToString());
                                FormaPagoC = Convert.ToInt32(item["FormaPagoC"].ToString());
                                logo = Convert.ToInt32(item["logo"].ToString());
                                ticket6cm = Convert.ToInt32(item["ticket58mm"].ToString());
                                ticket8cm = Convert.ToInt32(item["ticket80mm"].ToString());
                                codigoBarraTicket = Convert.ToInt32(item["TicketVenta"].ToString());
                            }

                            // Ventas Realizadas
                            if (StatusUltimoTicket.Equals("1"))
                            {
                                if (ticket6cm.Equals(1))
                                {
                                    using (imprimirTicket8cm imprimirTicketVenta = new imprimirTicket8cm())
                                    {
                                        imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);

                                        imprimirTicketVenta.Logo = logo;
                                        imprimirTicketVenta.Nombre = Usuario;
                                        imprimirTicketVenta.NombreComercial = NombreComercial;
                                        imprimirTicketVenta.DireccionCiudad = Direccion;
                                        imprimirTicketVenta.ColoniaCodigoPostal = ColyCP;
                                        imprimirTicketVenta.RFC = RFC;
                                        imprimirTicketVenta.Correo = Correo;
                                        imprimirTicketVenta.Telefono = Telefono;
                                        imprimirTicketVenta.NombreCliente = NombreC;
                                        imprimirTicketVenta.RFCCliente = RFCC;
                                        imprimirTicketVenta.DomicilioCliente = DomicilioC;
                                        imprimirTicketVenta.ColoniaCodigoPostalCliente = ColyCPC;
                                        imprimirTicketVenta.CorreoCliente = CorreoC;
                                        imprimirTicketVenta.TelefonoCliente = TelefonoC;
                                        imprimirTicketVenta.FormaDePagoCliente = FormaPagoC;
                                        imprimirTicketVenta.CodigoBarra = codigoBarraTicket;

                                        imprimirTicketVenta.ShowDialog();
                                    }
                                }
                                else if (ticket8cm.Equals(1))
                                {
                                    using (imprimirTicket8cm imprimirTicketVenta = new imprimirTicket8cm())
                                    {
                                        imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);

                                        imprimirTicketVenta.Logo = logo;
                                        imprimirTicketVenta.Nombre = Usuario;
                                        imprimirTicketVenta.NombreComercial = NombreComercial;
                                        imprimirTicketVenta.DireccionCiudad = Direccion;
                                        imprimirTicketVenta.ColoniaCodigoPostal = ColyCP;
                                        imprimirTicketVenta.RFC = RFC;
                                        imprimirTicketVenta.Correo = Correo;
                                        imprimirTicketVenta.Telefono = Telefono;
                                        imprimirTicketVenta.NombreCliente = NombreC;
                                        imprimirTicketVenta.RFCCliente = RFCC;
                                        imprimirTicketVenta.DomicilioCliente = DomicilioC;
                                        imprimirTicketVenta.ColoniaCodigoPostalCliente = ColyCPC;
                                        imprimirTicketVenta.CorreoCliente = CorreoC;
                                        imprimirTicketVenta.TelefonoCliente = TelefonoC;
                                        imprimirTicketVenta.FormaDePagoCliente = FormaPagoC;
                                        imprimirTicketVenta.CodigoBarra = codigoBarraTicket;

                                        imprimirTicketVenta.ShowDialog();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                var idVenta = cn.EjecutarSelect($"SELECT * FROM Ventas WHERE IDUsuario = {FormPrincipal.userID} AND IDEmpleado = 0 AND Status = 1 ORDER BY ID DESC LIMIT 1", 1).ToString();

                using (DataTable dtDatosVentas = cn.CargarDatos($"SELECT Folio, Serie, Status FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idVenta}'"))
                {
                    if (!dtDatosVentas.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtDatosVentas.Rows)
                        {
                            Folio = item["Folio"].ToString();
                            Serie = item["Serie"].ToString();
                            StatusUltimoTicket = item["Status"].ToString();
                        }
                    }
                }

                if (Folio.Equals("0"))
                {
                    if (StatusUltimoTicket.Equals("1"))
                    {
                        if (ticket6cm.Equals(1))
                        {
                            if (usuarioActivo.Contains("@"))
                            {
                                using (ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.idEmpleado = FormPrincipal.id_empleado;
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                            else
                            {
                                using (ImprimirTicketCajaAbierta8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbierta8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                        }
                        else if (ticket8cm.Equals(1))
                        {
                            if (usuarioActivo.Contains("@"))
                            {
                                using (ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.idEmpleado = FormPrincipal.id_empleado;
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                            else
                            {
                                using (ImprimirTicketCajaAbierta8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbierta8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                        }
                    }
                }
                else
                {
                    using (DataTable dtConfiguracionTipoTicket = cn.CargarDatos(cs.tipoDeTicket()))
                    {
                        if (!dtConfiguracionTipoTicket.Rows.Count.Equals(0))
                        {
                            var Usuario = 0;
                            var NombreComercial = 0;
                            var Direccion = 0;
                            var ColyCP = 0;
                            var RFC = 0;
                            var Correo = 0;
                            var Telefono = 0;
                            var NombreC = 0;
                            var DomicilioC = 0;
                            var RFCC = 0;
                            var CorreoC = 0;
                            var TelefonoC = 0;
                            var ColyCPC = 0;
                            var FormaPagoC = 0;
                            var logo = 0;
                            var codigoBarraTicket = 0;

                            foreach (DataRow item in dtConfiguracionTipoTicket.Rows)
                            {
                                Usuario = Convert.ToInt32(item["Usuario"].ToString());
                                NombreComercial = Convert.ToInt32(item["NombreComercial"].ToString());
                                Direccion = Convert.ToInt32(item["Direccion"].ToString());
                                ColyCP = Convert.ToInt32(item["ColyCP"].ToString());
                                RFC = Convert.ToInt32(item["RFC"].ToString());
                                Correo = Convert.ToInt32(item["Correo"].ToString());
                                Telefono = Convert.ToInt32(item["Telefono"].ToString());
                                NombreC = Convert.ToInt32(item["NombreC"].ToString());
                                DomicilioC = Convert.ToInt32(item["DomicilioC"].ToString());
                                RFCC = Convert.ToInt32(item["RFCC"].ToString());
                                CorreoC = Convert.ToInt32(item["CorreoC"].ToString());
                                TelefonoC = Convert.ToInt32(item["TelefonoC"].ToString());
                                ColyCPC = Convert.ToInt32(item["ColyCPC"].ToString());
                                FormaPagoC = Convert.ToInt32(item["FormaPagoC"].ToString());
                                logo = Convert.ToInt32(item["logo"].ToString());
                                ticket6cm = Convert.ToInt32(item["ticket58mm"].ToString());
                                ticket8cm = Convert.ToInt32(item["ticket80mm"].ToString());
                                codigoBarraTicket = Convert.ToInt32(item["TicketVenta"].ToString());
                            }

                            // Ventas Realizadas
                            if (StatusUltimoTicket.Equals("1"))
                            {
                                if (ticket6cm.Equals(1))
                                {
                                    using (imprimirTicket8cm imprimirTicketVenta = new imprimirTicket8cm())
                                    {
                                        imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);

                                        imprimirTicketVenta.Logo = logo;
                                        imprimirTicketVenta.Nombre = Usuario;
                                        imprimirTicketVenta.NombreComercial = NombreComercial;
                                        imprimirTicketVenta.DireccionCiudad = Direccion;
                                        imprimirTicketVenta.ColoniaCodigoPostal = ColyCP;
                                        imprimirTicketVenta.RFC = RFC;
                                        imprimirTicketVenta.Correo = Correo;
                                        imprimirTicketVenta.Telefono = Telefono;
                                        imprimirTicketVenta.NombreCliente = NombreC;
                                        imprimirTicketVenta.RFCCliente = RFCC;
                                        imprimirTicketVenta.DomicilioCliente = DomicilioC;
                                        imprimirTicketVenta.ColoniaCodigoPostalCliente = ColyCPC;
                                        imprimirTicketVenta.CorreoCliente = CorreoC;
                                        imprimirTicketVenta.TelefonoCliente = TelefonoC;
                                        imprimirTicketVenta.FormaDePagoCliente = FormaPagoC;
                                        imprimirTicketVenta.CodigoBarra = codigoBarraTicket;

                                        imprimirTicketVenta.ShowDialog();
                                    }
                                }
                                else if (ticket8cm.Equals(1))
                                {
                                    using (imprimirTicket8cm imprimirTicketVenta = new imprimirTicket8cm())
                                    {
                                        imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);

                                        imprimirTicketVenta.Logo = logo;
                                        imprimirTicketVenta.Nombre = Usuario;
                                        imprimirTicketVenta.NombreComercial = NombreComercial;
                                        imprimirTicketVenta.DireccionCiudad = Direccion;
                                        imprimirTicketVenta.ColoniaCodigoPostal = ColyCP;
                                        imprimirTicketVenta.RFC = RFC;
                                        imprimirTicketVenta.Correo = Correo;
                                        imprimirTicketVenta.Telefono = Telefono;
                                        imprimirTicketVenta.NombreCliente = NombreC;
                                        imprimirTicketVenta.RFCCliente = RFCC;
                                        imprimirTicketVenta.DomicilioCliente = DomicilioC;
                                        imprimirTicketVenta.ColoniaCodigoPostalCliente = ColyCPC;
                                        imprimirTicketVenta.CorreoCliente = CorreoC;
                                        imprimirTicketVenta.TelefonoCliente = TelefonoC;
                                        imprimirTicketVenta.FormaDePagoCliente = FormaPagoC;
                                        imprimirTicketVenta.CodigoBarra = codigoBarraTicket;

                                        imprimirTicketVenta.ShowDialog();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void lbSaldoInicialInfo_Click(object sender, EventArgs e)
        {
            mostrarSaldoInicialDezglozado();

            //CajaAbonos mostrarAbonosCaja = Application.OpenForms.OfType<CajaAbonos>().FirstOrDefault();

            //var validarSaldoInicial = "Saldo Inicial";

            //if (mostrarAbonosCaja == null)
            //{
            //    abonos_devoluciones = "Saldo Inicial";
            //    CajaAbonos mostrarAbonos = new CajaAbonos();
            //    mostrarAbonos.ShowDialog();
            //}

            //if (mostrarAbonosCaja != null)
            //{
            //    if (mostrarAbonosCaja.WindowState == FormWindowState.Minimized || mostrarAbonosCaja.WindowState == FormWindowState.Normal)
            //    {
            //        mostrarAbonosCaja.BringToFront();
            //    }
            //}
        }

        private void mostrarSaldoInicialDezglozado()
        {
            CajaSaldoInicial detalleSaldoInicial = new CajaSaldoInicial();

            var IDUsuario = 0;
            var IDEmpleado = 0;
            var ultimaFechaDeCorte = string.Empty;
            var todosLosAbonos = string.Empty;

            ultimaFechaDeCorte = fechaFormateadaCorteParaAbonos;
            IDUsuario = FormPrincipal.userID;

            if (!FormPrincipal.userNickName.Contains("@"))
            {
                if (opcionComboBoxFiltroAdminEmp.Equals("Admin"))
                {
                    todosLosAbonos = "No";
                    IDEmpleado = 0;
                }
                else if (opcionComboBoxFiltroAdminEmp.Equals("All"))
                {
                    todosLosAbonos = "Si";
                    IDEmpleado = 0;
                }
                else
                {
                    todosLosAbonos = "No";
                    IDEmpleado = Convert.ToInt32(opcionComboBoxFiltroAdminEmp);
                }
            }
            else
            {
                todosLosAbonos = "No";
                IDEmpleado = FormPrincipal.id_empleado;
            }

            detalleSaldoInicial.IDUsuario = IDUsuario;
            detalleSaldoInicial.IDEmpleado = IDEmpleado;
            detalleSaldoInicial.ultimaFechaDeCorteDeCaja = ultimaFechaDeCorte;
            detalleSaldoInicial.todosLosAbonos = todosLosAbonos;
            detalleSaldoInicial.ShowDialog();
        }

        private void cbFiltroAdminEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormPrincipal.userNickName.Contains("@"))
            {
                limpiarVariablesParaTotales();
                clasificarTipoDeUsuario();
                CargarSaldoInicial();
                mostrarInformacionAbonos();
                filtrarInformacionSeleccionada();
                mostrarTotalEnCaja();
            }
        }

        private void filtrarInformacionSeleccionada()
        {
            if (opcionComboBoxFiltroAdminEmp.Equals("Admin"))
            {
                seccionAdminCaja();
                activarBotonesDeCaja();
            }
            else if (opcionComboBoxFiltroAdminEmp.Equals("All"))
            {
                seccionTodosCaja();
                desactivarBotonesDeCaja();
            }
            else
            {
                seccionEmpleadoCaja(opcionComboBoxFiltroAdminEmp);
                desactivarBotonesDeCaja();
            }
        }

        private void activarBotonesDeCaja()
        {
            btnRedondoAgregarDinero.Enabled = true;
            btnRedondoRetirarDinero.Enabled = true;
            btnRedondoCorteCaja.Enabled = true;
            btnRedondoAbrirCaja.Enabled = true;
            btnRedondoTabuladorDeDinero.Enabled = true;
        }

        private void desactivarBotonesDeCaja()
        {
            btnRedondoAgregarDinero.Enabled = false;
            btnRedondoRetirarDinero.Enabled = false;
            btnRedondoCorteCaja.Enabled = false;
            btnRedondoAbrirCaja.Enabled = false;
            btnRedondoTabuladorDeDinero.Enabled = false;
        }

        private void limpiarVariablesParaTotales()
        {
            totalEfectivoVentaEnCaja = totalTarjetaVentaEnCaja = totalValesEnVentaCaja = totalChequesVentaEnCaja = totalTransferenciaVentaEnCaja = totalSaldoInicialVentaEnCaja = totalEfectivoAnticiposEnCaja = totalTarjetaAnticiposEnCaja = totalValesAnticiposEnCaja = totalChequesAnticipoEnCaja = totalTransferenciaAnticiposEnCaja = totalEfectivoDepsitosEnCaja = totalTarjetaDepositosEnCaja = totalValesDepositosEnCaja = totalChequesDepsoitosEnCaja = totalTransferenciasDepositosEnCaja = totalEfectivoRetiroEnCaja = totalTarjetaRetiroEnCaja = totalValesRetiroEnCaja = totalChequesRetiroEnCaja = totalTransferenciaRetiroEnCaja = totalAbonoEfectivo = totalAbonoTarjeta = totalAbonoVales = totalAbonoCheque = totalAbonoTransferencia = totalAbonoRealizado = cantidadEfectivoSaldoInicialEnCaja = cantidadTarjetaSaldoInicialEnCaja = cantidadValesSaldoInicialEnCaja = cantidadChequeSaldoInicialEnCaja = cantidadTransferenciaSaldoInicialEnCaja = 0;

            cantidadEfectivoAgregado = cantidadTarjetaAgregado = cantidadValesAgregado = cantidadChequeAgregado = cantidadTransferenciaAgregado = cantidadTotalDineroAgregado = cantidadEfectivoRetirado = cantidadTarjetaRetirado = cantidadValesRetirado = cantidadChequeRetirado = cantidadTransferenciaRetirado = cantidadTotalDineroRetirado = 0;
        }

        private void mostrarTotalEnCaja()
        {
            if (opcionComboBoxFiltroAdminEmp.Equals("All"))
            {
                cantidadTotalEfectivoEnCaja = ((cantidadEfectivoVentaTodos + totalEfectivoAnticiposEnCaja + totalEfectivoDepsitosEnCaja + totalAbonoEfectivo) - totalEfectivoRetiroEnCaja);
                cantidadTotalTarjetaEnCaja = ((cantidadTarjetaVentaTodos + totalTarjetaAnticiposEnCaja + totalTarjetaDepositosEnCaja + totalAbonoTarjeta) - totalTarjetaRetiroEnCaja);
                cantidadTotalValesEnCaja = ((cantidadValesVentaTodos + totalValesAnticiposEnCaja + totalValesDepositosEnCaja + totalAbonoVales) - totalValesRetiroEnCaja);
                cantidadTotalCehqueEnCaja = ((cantidadChequeVentaTodos + totalChequesAnticipoEnCaja + totalChequesDepsoitosEnCaja + totalAbonoCheque) - totalChequesRetiroEnCaja);
                cantidadTotalTransferenciaEnCaja = ((cantidadTransferenciaVentaTodos + totalTransferenciaAnticiposEnCaja + totalTransferenciasDepositosEnCaja + totalAbonoTransferencia) - totalTransferenciaRetiroEnCaja);
                sumaDeTotalesEnCaja = cantidadTotalEfectivoEnCaja + cantidadTotalTarjetaEnCaja + cantidadTotalValesEnCaja + cantidadTotalCehqueEnCaja + cantidadTotalTransferenciaEnCaja + cantidadEfectivoSaldoInicialEnCaja + cantidadTarjetaSaldoInicialEnCaja + cantidadValesSaldoInicialEnCaja + cantidadChequeSaldoInicialEnCaja + cantidadTransferenciaSaldoInicialEnCaja;

                lbTEfectivoC.Text = (cantidadTotalEfectivoEnCaja + cantidadEfectivoSaldoInicialEnCaja).ToString("C2");
                lbTTarjetaC.Text = (cantidadTotalTarjetaEnCaja + cantidadTarjetaSaldoInicialEnCaja).ToString("C2");
                lbTValesC.Text = (cantidadTotalValesEnCaja + cantidadValesSaldoInicialEnCaja).ToString("C2");
                lbTChequeC.Text = (cantidadTotalCehqueEnCaja + cantidadChequeSaldoInicialEnCaja).ToString("C2");
                lbTTransC.Text = (cantidadTotalTransferenciaEnCaja + cantidadTransferenciaSaldoInicialEnCaja).ToString("C2");
                lbTSaldoInicial.Text = totalSaldoInicial.ToString("C2");

                lbTTotalCaja.Text = sumaDeTotalesEnCaja.ToString("C2");
            }
            else
            {
                cantidadTotalEfectivoEnCaja = ((totalEfectivoVentaEnCaja + totalEfectivoAnticiposEnCaja + totalEfectivoDepsitosEnCaja + totalAbonoEfectivo) - totalEfectivoRetiroEnCaja);
                cantidadTotalTarjetaEnCaja = ((totalTarjetaVentaEnCaja + totalTarjetaAnticiposEnCaja + totalTarjetaDepositosEnCaja + totalAbonoTarjeta) - totalTarjetaRetiroEnCaja);
                cantidadTotalValesEnCaja = ((totalValesEnVentaCaja + totalValesAnticiposEnCaja + totalValesDepositosEnCaja + totalAbonoVales) - totalValesRetiroEnCaja);
                cantidadTotalCehqueEnCaja = ((totalChequesVentaEnCaja + totalChequesAnticipoEnCaja + totalChequesDepsoitosEnCaja + totalAbonoCheque) - totalChequesRetiroEnCaja);
                cantidadTotalTransferenciaEnCaja = ((totalTransferenciaVentaEnCaja + totalTransferenciaAnticiposEnCaja + totalTransferenciasDepositosEnCaja + totalAbonoTransferencia) - totalTransferenciaRetiroEnCaja);
                sumaDeTotalesEnCaja = cantidadTotalEfectivoEnCaja + cantidadTotalTarjetaEnCaja + cantidadTotalValesEnCaja + cantidadTotalCehqueEnCaja + cantidadTotalTransferenciaEnCaja + cantidadEfectivoSaldoInicialEnCaja + cantidadTarjetaSaldoInicialEnCaja + cantidadValesSaldoInicialEnCaja + cantidadChequeSaldoInicialEnCaja + cantidadTransferenciaSaldoInicialEnCaja;

                lbTEfectivoC.Text = (cantidadTotalEfectivoEnCaja + cantidadEfectivoSaldoInicialEnCaja).ToString("C2");
                lbTTarjetaC.Text = (cantidadTotalTarjetaEnCaja + cantidadTarjetaSaldoInicialEnCaja).ToString("C2");
                lbTValesC.Text = (cantidadTotalValesEnCaja + cantidadValesSaldoInicialEnCaja).ToString("C2");
                lbTChequeC.Text = (cantidadTotalCehqueEnCaja + cantidadChequeSaldoInicialEnCaja).ToString("C2");
                lbTTransC.Text = (cantidadTotalTransferenciaEnCaja + cantidadTransferenciaSaldoInicialEnCaja).ToString("C2");
                lbTSaldoInicial.Text = totalSaldoInicial.ToString("C2");

                lbTTotalCaja.Text = sumaDeTotalesEnCaja.ToString("C2");
            }
        }

        private void seccionTodosCaja()
        {
            limpiarVariablesTotalesDeTodos();
            seccionTodosVentas();
            seccionTodosAnticipos();
            seccionTodosDineroAgregado();
            seccionTodosDineroRetirado();
        }

        private void limpiarVariablesTotalesDeTodos()
        {
            cantidadEfectivoVentaTodos = totalEfectivoVentaEnCaja = cantidadTarjetaVentaTodos = totalTarjetaVentaEnCaja = cantidadValesVentaTodos = totalValesEnVentaCaja = cantidadChequeVentaTodos = totalChequesVentaEnCaja = cantidadTransferenciaVentaTodos = totalTransferenciaVentaEnCaja = cantidadCreditoVentaTodos = cantidadAnticiposVentaTodos = cantidadTotalVentasVentaTodos = cantidadEfectivoAnticipos = totalEfectivoAnticiposEnCaja = cantidadTarjetaAnticipos = totalTarjetaAnticiposEnCaja = cantidadValesAnticipos = totalValesAnticiposEnCaja = cantidadChequeAnticipos = totalChequesAnticipoEnCaja = cantidadTransferenciaAnticipos = totalTransferenciaAnticiposEnCaja = cantidadTotalAnticipos = cantidadEfectivoAgregaddo = totalEfectivoDepsitosEnCaja = cantidadTarjetaAgregaddo = totalTarjetaDepositosEnCaja = cantidadValesAgregaddo = totalValesDepositosEnCaja = cantidadChequeAgregaddo = totalChequesDepsoitosEnCaja = cantidadTransferenciaAgregaddo = totalTransferenciasDepositosEnCaja = cantidadTotalDineroAgregado = cantidadEfectivoRetirado = totalEfectivoDepsitosEnCaja = cantidadTarjetaRetirado = totalTarjetaDepositosEnCaja = cantidadValesRetirado = totalValesDepositosEnCaja = cantidadChequeRetirado = totalChequesDepsoitosEnCaja = cantidadTransferenciaRetirado = totalTransferenciasDepositosEnCaja = cantidadTotalDineroRetirado = 0;
        }

        private void seccionTodosDineroRetirado()
        {
            // Total de Dinero Retirado
            List<int> IDEmpleados = new List<int>();
            List<string> QuerysDeTodosLosTotals = new List<string>();

            using (DataTable dtIDsEpleados = cn.CargarDatos(cs.cargarIDsDeEmpleados()))
            {
                if (!dtIDsEpleados.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in dtIDsEpleados.Rows)
                    {
                        IDEmpleados.Add(Convert.ToInt32(item["ID"].ToString()));
                    }
                }
            }

            var noEstaVacia = IsEmpty(IDEmpleados);

            if (noEstaVacia)
            {
                var resultadoIDEmpleados = string.Join(",", IDEmpleados);

                using (DataTable dtcargarNuevoSaldoInicial = cn.CargarDatos(cs.cargarNuevoSaldoInicial(resultadoIDEmpleados)))
                {
                    if (!dtcargarNuevoSaldoInicial.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtcargarNuevoSaldoInicial.Rows)
                        {
                            QuerysDeTodosLosTotals.Add($"({cs.totalCantidadesRetirosTodos(item["IDUsuario"].ToString(), item["IDEmpleado"].ToString(), item["IDCaja"].ToString())})");
                        }

                        var UnionQuerysTodosLosTotales = string.Join("UNION", QuerysDeTodosLosTotals);

                        if (!string.IsNullOrWhiteSpace(UnionQuerysTodosLosTotales))
                        {
                            using (DataTable dtUnionQuerysTodosLosTotales = cn.CargarDatos(UnionQuerysTodosLosTotales))
                            {
                                if (!dtUnionQuerysTodosLosTotales.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow item in dtUnionQuerysTodosLosTotales.Rows)
                                    {
                                        if (!string.IsNullOrWhiteSpace(item["Efectivo"].ToString()))
                                        {
                                            cantidadEfectivoRetirado += Convert.ToDecimal(item["Efectivo"].ToString());
                                            totalEfectivoRetiroEnCaja = cantidadEfectivoRetirado;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Tarjeta"].ToString()))
                                        {
                                            cantidadTarjetaRetirado += Convert.ToDecimal(item["Tarjeta"].ToString());
                                            totalTarjetaRetiroEnCaja = cantidadTarjetaRetirado;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Vales"].ToString()))
                                        {
                                            cantidadValesRetirado += Convert.ToDecimal(item["Vales"].ToString());
                                            totalValesRetiroEnCaja = cantidadValesRetirado;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Cheque"].ToString()))
                                        {
                                            cantidadChequeRetirado += Convert.ToDecimal(item["Cheque"].ToString());
                                            totalChequesRetiroEnCaja = cantidadChequeRetirado;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Transferencia"].ToString()))
                                        {
                                            cantidadTransferenciaRetirado += Convert.ToDecimal(item["Transferencia"].ToString());
                                            totalTransferenciaRetiroEnCaja = cantidadTransferenciaRetirado;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["TotalRetiros"].ToString()))
                                        {
                                            cantidadTotalDineroRetirado += Convert.ToDecimal(item["TotalRetiros"].ToString());
                                        }

                                        lbEfectivoR.Text = cantidadEfectivoRetirado.ToString("C2");
                                        lbTarjetaR.Text = cantidadTarjetaRetirado.ToString("C2");
                                        lbValesR.Text = cantidadValesRetirado.ToString("C2");
                                        lbChequeR.Text = cantidadChequeRetirado.ToString("C2");
                                        lbTransferenciaR.Text = cantidadTransferenciaRetirado.ToString("C2");
                                        lbTRetirado.Text = cantidadTotalDineroRetirado.ToString("C2");
                                    }
                                }
                                else
                                {
                                    limpiarVariablesCantidadesDeDineroRetirado();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void seccionTodosDineroAgregado()
        {
            // Total de Dinero Agregado
            List<int> IDEmpleados = new List<int>();
            List<string> QuerysDeTodosLosTotals = new List<string>();

            using (DataTable dtIDsEpleados = cn.CargarDatos(cs.cargarIDsDeEmpleados()))
            {
                if (!dtIDsEpleados.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in dtIDsEpleados.Rows)
                    {
                        IDEmpleados.Add(Convert.ToInt32(item["ID"].ToString()));
                    }
                }
            }

            var noEstaVacia = IsEmpty(IDEmpleados);

            if (noEstaVacia)
            {
                var resultadoIDEmpleados = string.Join(",", IDEmpleados);

                using (DataTable dtcargarNuevoSaldoInicial = cn.CargarDatos(cs.cargarNuevoSaldoInicial(resultadoIDEmpleados)))
                {
                    if (!dtcargarNuevoSaldoInicial.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtcargarNuevoSaldoInicial.Rows)
                        {
                            QuerysDeTodosLosTotals.Add($"({cs.totalCantidadesDepositosTodos(item["IDUsuario"].ToString(), item["IDEmpleado"].ToString(), item["IDCaja"].ToString())})");
                        }

                        var UnionQuerysTodosLosTotales = string.Join("UNION", QuerysDeTodosLosTotals);

                        if (!string.IsNullOrWhiteSpace(UnionQuerysTodosLosTotales))
                        {
                            using (DataTable dtUnionQuerysTodosLosTotales = cn.CargarDatos(UnionQuerysTodosLosTotales))
                            {
                                if (!dtUnionQuerysTodosLosTotales.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow item in dtUnionQuerysTodosLosTotales.Rows)
                                    {
                                        if (!string.IsNullOrWhiteSpace(item["Efectivo"].ToString()))
                                        {
                                            cantidadEfectivoAgregaddo += Convert.ToDecimal(item["Efectivo"].ToString());
                                            totalEfectivoDepsitosEnCaja = cantidadEfectivoAgregaddo;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Tarjeta"].ToString()))
                                        {
                                            cantidadTarjetaAgregaddo += Convert.ToDecimal(item["Tarjeta"].ToString());
                                            totalTarjetaDepositosEnCaja = cantidadTarjetaAgregaddo;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Vales"].ToString()))
                                        {
                                            cantidadValesAgregaddo += Convert.ToDecimal(item["Vales"].ToString());
                                            totalValesDepositosEnCaja = cantidadValesAgregaddo;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Cheque"].ToString()))
                                        {
                                            cantidadChequeAgregaddo += Convert.ToDecimal(item["Cheque"].ToString());
                                            totalChequesDepsoitosEnCaja = cantidadChequeAgregaddo;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Transferencia"].ToString()))
                                        {
                                            cantidadTransferenciaAgregaddo += Convert.ToDecimal(item["Transferencia"].ToString());
                                            totalTransferenciasDepositosEnCaja = cantidadTransferenciaAgregaddo;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["TotalDepositos"].ToString()))
                                        {
                                            cantidadTotalDineroAgregado += Convert.ToDecimal(item["TotalDepositos"].ToString());
                                        }

                                        lbTEfectivoD.Text = cantidadEfectivoAgregaddo.ToString("C2");
                                        lbTTarjetaD.Text = cantidadTarjetaAgregaddo.ToString("C2");
                                        lbTValesD.Text = cantidadValesAgregaddo.ToString("C2");
                                        lbTChequeD.Text = cantidadChequeAgregaddo.ToString("C2");
                                        lbTTransD.Text = cantidadTransferenciaAgregaddo.ToString("C2");
                                        lbTAgregado.Text = cantidadTotalDineroAgregado.ToString("C2");
                                    }
                                }
                                else
                                {
                                    limpiarVariablesCantidadesDeDineroAgregado();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                using (DataTable dtHistorialCorteDeCaja = cn.CargarDatos(cs.cargarSaldoInicialAdministrador()))
                {
                    if (!dtHistorialCorteDeCaja.Rows.Count.Equals(0))
                    {
                        lbSaldoInicialInfo.Visible = true;
                        foreach (DataRow item in dtHistorialCorteDeCaja.Rows)
                        {
                            fechaUltimoCorte = Convert.ToDateTime(item["Fecha"].ToString());
                            fechaFormateadaCorteParaAbonos = fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss");
                            ultimoCorteDeCaja = fechaFormateadaCorteParaAbonos;
                            cantidadEfectivoSaldoInicialEnCaja = cantidadTotalEfectivoEnCaja = Convert.ToDecimal(item["Efectivo"].ToString());
                            cantidadTarjetaSaldoInicialEnCaja = cantidadTotalTarjetaEnCaja = Convert.ToDecimal(item["Tarjeta"].ToString());
                            cantidadValesSaldoInicialEnCaja = cantidadTotalValesEnCaja = Convert.ToDecimal(item["Vales"].ToString());
                            cantidadChequeSaldoInicialEnCaja = cantidadTotalCehqueEnCaja = Convert.ToDecimal(item["Cheque"].ToString());
                            cantidadTransferenciaSaldoInicialEnCaja = cantidadTotalTransferenciaEnCaja = Convert.ToDecimal(item["Transferencia"].ToString());
                            idUltimoCorteDeCaja = item["IDCaja"].ToString();
                            saldoInicial = (float)Convert.ToDecimal(item["SaldoInicial"].ToString());
                            if (saldoInicial <= 0)
                            {
                                lbSaldoInicialInfo.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        lbSaldoInicialInfo.Visible = false;
                        limpiarVariablesCantidadesDeVentas();
                    }
                }
            }
        }

        private void seccionTodosAnticipos()
        {
            List<int> IDEmpleados = new List<int>();
            List<string> QuerysDeTodosLosTotals = new List<string>();

            using (DataTable dtIDsEpleados = cn.CargarDatos(cs.cargarIDsDeEmpleados()))
            {
                if (!dtIDsEpleados.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in dtIDsEpleados.Rows)
                    {
                        IDEmpleados.Add(Convert.ToInt32(item["ID"].ToString()));
                    }
                }
            }

            var noEstaVacia = IsEmpty(IDEmpleados);

            if (noEstaVacia)
            {
                var resultadoIDEmpleados = string.Join(",", IDEmpleados);

                using (DataTable dtcargarNuevoSaldoInicial = cn.CargarDatos(cs.cargarNuevoSaldoInicial(resultadoIDEmpleados)))
                {
                    if (!dtcargarNuevoSaldoInicial.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtcargarNuevoSaldoInicial.Rows)
                        {
                            QuerysDeTodosLosTotals.Add($"({cs.totalCantidadesAnticposTodos(item["IDUsuario"].ToString(), item["IDEmpleado"].ToString(), item["IDCaja"].ToString())})");
                        }

                        var UnionQuerysTodosLosTotales = string.Join("UNION", QuerysDeTodosLosTotals);

                        if (!string.IsNullOrWhiteSpace(UnionQuerysTodosLosTotales))
                        {
                            using (DataTable dtUnionQuerysTodosLosTotales = cn.CargarDatos(UnionQuerysTodosLosTotales))
                            {
                                if (!dtUnionQuerysTodosLosTotales.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow item in dtUnionQuerysTodosLosTotales.Rows)
                                    {
                                        if (!string.IsNullOrWhiteSpace(item["Efectivo"].ToString()))
                                        {
                                            cantidadEfectivoAnticipos += Convert.ToDecimal(item["Efectivo"].ToString());
                                            totalEfectivoAnticiposEnCaja = cantidadEfectivoAnticipos;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Tarjeta"].ToString()))
                                        {
                                            cantidadTarjetaAnticipos += Convert.ToDecimal(item["Tarjeta"].ToString());
                                            totalTarjetaAnticiposEnCaja = cantidadTarjetaAnticipos;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Vales"].ToString()))
                                        {
                                            cantidadValesAnticipos += Convert.ToDecimal(item["Vales"].ToString());
                                            totalValesAnticiposEnCaja = cantidadValesAnticipos;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Cheque"].ToString()))
                                        {
                                            cantidadChequeAnticipos += Convert.ToDecimal(item["Cheque"].ToString());
                                            totalChequesAnticipoEnCaja = cantidadChequeAnticipos;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Transferencia"].ToString()))
                                        {
                                            cantidadTransferenciaAnticipos += Convert.ToDecimal(item["Transferencia"].ToString());
                                            totalTransferenciaAnticiposEnCaja = cantidadTransferenciaAnticipos;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["TotalAnticipos"].ToString()))
                                        {
                                            cantidadTotalAnticipos += Convert.ToDecimal(item["TotalAnticipos"].ToString());
                                        }

                                        lbTEfectivoA.Text = cantidadEfectivoAnticipos.ToString("C2");
                                        lbTTarjetaA.Text = cantidadTarjetaAnticipos.ToString("C2");
                                        lbTValesA.Text = cantidadValesAnticipos.ToString("C2");
                                        lbTChequeA.Text = cantidadChequeAnticipos.ToString("C2");
                                        lbTTransA.Text = cantidadTransferenciaAnticipos.ToString("C2");
                                        lbTAnticiposA.Text = cantidadTotalAnticipos.ToString("C2");
                                    }
                                }
                                else
                                {
                                    limpiarVariablesCantidadesDeAnticipos();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                using (DataTable dtHistorialCorteDeCaja = cn.CargarDatos(cs.cargarSaldoInicialAdministrador()))
                {
                    if (!dtHistorialCorteDeCaja.Rows.Count.Equals(0))
                    {
                        lbSaldoInicialInfo.Visible = true;
                        foreach (DataRow item in dtHistorialCorteDeCaja.Rows)
                        {
                            fechaUltimoCorte = Convert.ToDateTime(item["Fecha"].ToString());
                            fechaFormateadaCorteParaAbonos = fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss");
                            ultimoCorteDeCaja = fechaFormateadaCorteParaAbonos;
                            cantidadEfectivoSaldoInicialEnCaja = cantidadTotalEfectivoEnCaja = Convert.ToDecimal(item["Efectivo"].ToString());
                            cantidadTarjetaSaldoInicialEnCaja = cantidadTotalTarjetaEnCaja = Convert.ToDecimal(item["Tarjeta"].ToString());
                            cantidadValesSaldoInicialEnCaja = cantidadTotalValesEnCaja = Convert.ToDecimal(item["Vales"].ToString());
                            cantidadChequeSaldoInicialEnCaja = cantidadTotalCehqueEnCaja = Convert.ToDecimal(item["Cheque"].ToString());
                            cantidadTransferenciaSaldoInicialEnCaja = cantidadTotalTransferenciaEnCaja = Convert.ToDecimal(item["Transferencia"].ToString());
                            idUltimoCorteDeCaja = item["IDCaja"].ToString();
                            saldoInicial = (float)Convert.ToDecimal(item["SaldoInicial"].ToString());
                            if (saldoInicial <= 0)
                            {
                                lbSaldoInicialInfo.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        lbSaldoInicialInfo.Visible = false;
                        limpiarVariablesCantidadesDeVentas();
                    }
                }
            }
        }

        private void seccionTodosVentas()
        {
            List<int> IDEmpleados = new List<int>();
            List<string> QuerysDeTodosLosTotals = new List<string>();

            using (DataTable dtIDsEpleados = cn.CargarDatos(cs.cargarIDsDeEmpleados()))
            {
                if (!dtIDsEpleados.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in dtIDsEpleados.Rows)
                    {
                        IDEmpleados.Add(Convert.ToInt32(item["ID"].ToString()));
                    }
                }
            }

            var noEstaVacia = IsEmpty(IDEmpleados);

            if (noEstaVacia)
            {
                var resultadoIDEmpleados = string.Join(",", IDEmpleados);

                using (DataTable dtcargarNuevoSaldoInicial = cn.CargarDatos(cs.cargarNuevoSaldoInicial(resultadoIDEmpleados)))
                {
                    if (!dtcargarNuevoSaldoInicial.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtcargarNuevoSaldoInicial.Rows)
                        {
                            QuerysDeTodosLosTotals.Add($"({cs.totalCantidadesVentasTodos(item["IDUsuario"].ToString(), item["IDEmpleado"].ToString(), item["IDCaja"].ToString())})");
                        }

                        var UnionQuerysTodosLosTotales = string.Join("UNION", QuerysDeTodosLosTotals);

                        if (!string.IsNullOrWhiteSpace(UnionQuerysTodosLosTotales))
                        {
                            using (DataTable dtUnionQuerysTodosLosTotales = cn.CargarDatos(UnionQuerysTodosLosTotales))
                            {
                                if (!dtUnionQuerysTodosLosTotales.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow item in dtUnionQuerysTodosLosTotales.Rows)
                                    {
                                        if (!string.IsNullOrWhiteSpace(item["Efectivo"].ToString()))
                                        {
                                            cantidadEfectivoVentaTodos += Convert.ToDecimal(item["Efectivo"].ToString());
                                            totalEfectivoVentaEnCaja = cantidadEfectivoVentaTodos;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Tarjeta"].ToString()))
                                        {
                                            cantidadTarjetaVentaTodos += Convert.ToDecimal(item["Tarjeta"].ToString());
                                            totalTarjetaVentaEnCaja = cantidadTarjetaVentaTodos;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Vales"].ToString()))
                                        {
                                            cantidadValesVentaTodos += Convert.ToDecimal(item["Vales"].ToString());
                                            totalValesEnVentaCaja = cantidadValesVentaTodos;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Cheque"].ToString()))
                                        {
                                            cantidadChequeVentaTodos += Convert.ToDecimal(item["Cheque"].ToString());
                                            totalChequesVentaEnCaja = cantidadChequeVentaTodos;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Transferencia"].ToString()))
                                        {
                                            cantidadTransferenciaVentaTodos += Convert.ToDecimal(item["Transferencia"].ToString());
                                            totalTransferenciaVentaEnCaja = cantidadTransferenciaVentaTodos;
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Credito"].ToString()))
                                        {
                                            var cantidadCreditoResultadoBaseDeDatos = Convert.ToDecimal(item["Credito"].ToString());

                                            if (cantidadCreditoResultadoBaseDeDatos > 0)
                                            {
                                                cantidadCreditoVentaTodos += cantidadCreditoResultadoBaseDeDatos;
                                            }
                                            else if (cantidadCreditoResultadoBaseDeDatos.Equals(0))
                                            {
                                                cantidadCreditoVentaTodos += 0;
                                            }
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["Anticipo"].ToString()))
                                        {
                                            cantidadAnticiposVentaTodos += Convert.ToDecimal(item["Anticipo"].ToString());
                                        }

                                        if (!string.IsNullOrWhiteSpace(item["TotalVentas"].ToString()))
                                        {
                                            cantidadTotalVentasVentaTodos += Convert.ToDecimal(item["TotalVentas"].ToString());
                                        }

                                        lbTEfectivo.Text = cantidadEfectivoVentaTodos.ToString("C2");
                                        lbTTarjeta.Text = cantidadTarjetaVentaTodos.ToString("C2");
                                        lbTVales.Text = cantidadValesVentaTodos.ToString("C2");
                                        lbTCheque.Text = cantidadChequeVentaTodos.ToString("C2");
                                        lbTTrans.Text = cantidadTransferenciaVentaTodos.ToString("C2");
                                        lbTCredito.Text = (cantidadCreditoVentaTodos - totalAbonoRealizado).ToString("C2");
                                        //lbTCreditoC.Text = cantidadAbonosVentaTodos.ToString("C2");
                                        lbTAnticipos.Text = cantidadAnticiposVentaTodos.ToString("C2");
                                        lbTVentas.Text = cantidadTotalVentasVentaTodos.ToString("C2");
                                    }
                                }
                                else
                                {
                                    limpiarVariablesCantidadesDeVentas();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                using (DataTable dtHistorialCorteDeCaja = cn.CargarDatos(cs.cargarSaldoInicialAdministrador()))
                {
                    if (!dtHistorialCorteDeCaja.Rows.Count.Equals(0))
                    {
                        lbSaldoInicialInfo.Visible = false;
                        foreach (DataRow item in dtHistorialCorteDeCaja.Rows)
                        {
                            fechaUltimoCorte = Convert.ToDateTime(item["Fecha"].ToString());
                            fechaFormateadaCorteParaAbonos = fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss");
                            ultimoCorteDeCaja = fechaFormateadaCorteParaAbonos;
                            cantidadEfectivoSaldoInicialEnCaja = cantidadTotalEfectivoEnCaja = Convert.ToDecimal(item["Efectivo"].ToString());
                            cantidadTarjetaSaldoInicialEnCaja = cantidadTotalTarjetaEnCaja = Convert.ToDecimal(item["Tarjeta"].ToString());
                            cantidadValesSaldoInicialEnCaja = cantidadTotalValesEnCaja = Convert.ToDecimal(item["Vales"].ToString());
                            cantidadChequeSaldoInicialEnCaja = cantidadTotalCehqueEnCaja = Convert.ToDecimal(item["Cheque"].ToString());
                            cantidadTransferenciaSaldoInicialEnCaja = cantidadTotalTransferenciaEnCaja = Convert.ToDecimal(item["Transferencia"].ToString());
                            idUltimoCorteDeCaja = item["IDCaja"].ToString();
                            saldoInicial = (float)Convert.ToDecimal(item["SaldoInicial"].ToString());
                            if (saldoInicial <= 0)
                            {
                                lbSaldoInicialInfo.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        lbSaldoInicialInfo.Visible = false;
                        limpiarVariablesCantidadesDeVentas();
                    }
                }
            }
        }

        private void seccionEmpleadoCaja(string idEmpleado)
        {
            seccionEmpleadoVentas(idEmpleado);
            seccionEmpleadoAnticipos(idEmpleado);
            seccionEmpleadoDineroAgregado(idEmpleado);
            seccionEmpleadoDineroRetirado(idEmpleado);
        }

        private void seccionEmpleadoDineroRetirado(string idEmpleado)
        {
            // Total de Dinero Retirado
            var idCajaUltimoCorte = idUltimoCorteDeCaja;

            if (!string.IsNullOrWhiteSpace(idCajaUltimoCorte))
            {
                using (DataTable dtSeccionDineroRetiradoEmpleado = cn.CargarDatos(cs.totalCantiadesRetirosEmpleado(idCajaUltimoCorte, idEmpleado)))
                {
                    if (!dtSeccionDineroRetiradoEmpleado.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtSeccionDineroRetiradoEmpleado.Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(item["Efectivo"].ToString()))
                            {
                                cantidadEfectivoRetirado = Convert.ToDecimal(item["Efectivo"].ToString());
                                totalEfectivoRetiroEnCaja += cantidadEfectivoRetirado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Tarjeta"].ToString()))
                            {
                                cantidadTarjetaRetirado = Convert.ToDecimal(item["Tarjeta"].ToString());
                                totalTarjetaRetiroEnCaja += cantidadTarjetaRetirado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Vales"].ToString()))
                            {
                                cantidadValesRetirado = Convert.ToDecimal(item["Vales"].ToString());
                                totalValesRetiroEnCaja += cantidadValesRetirado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Cheque"].ToString()))
                            {
                                cantidadChequeRetirado = Convert.ToDecimal(item["Cheque"].ToString());
                                totalChequesRetiroEnCaja += cantidadChequeRetirado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Transferencia"].ToString()))
                            {
                                cantidadTransferenciaRetirado = Convert.ToDecimal(item["Transferencia"].ToString());
                                totalTransferenciaRetiroEnCaja += cantidadTransferenciaRetirado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["TotalRetiros"].ToString()))
                            {
                                cantidadTotalDineroRetirado = Convert.ToDecimal(item["TotalRetiros"].ToString());
                            }

                            lbEfectivoR.Text = cantidadEfectivoRetirado.ToString("C2");
                            lbTarjetaR.Text = cantidadTarjetaRetirado.ToString("C2");
                            lbValesR.Text = cantidadValesRetirado.ToString("C2");
                            lbChequeR.Text = cantidadChequeRetirado.ToString("C2");
                            lbTransferenciaR.Text = cantidadTransferenciaRetirado.ToString("C2");
                            lbTRetirado.Text = cantidadTotalDineroRetirado.ToString("C2");
                        }
                    }
                }
            }
            else
            {
                limpiarVariablesCantidadesDeDineroRetirado();
            }
        }

        private void limpiarVariablesCantidadesDeDineroRetirado()
        {
            cantidadEfectivoRetirado = cantidadTarjetaRetirado = cantidadValesRetirado = cantidadChequeRetirado = cantidadTransferenciaRetirado = cantidadTotalDineroRetirado = 0;

            lbEfectivoR.Text = cantidadEfectivoRetirado.ToString("C2");
            lbTarjetaR.Text = cantidadTarjetaRetirado.ToString("C2");
            lbValesR.Text = cantidadValesRetirado.ToString("C2");
            lbChequeR.Text = cantidadChequeRetirado.ToString("C2");
            lbTransferenciaR.Text = cantidadTransferenciaRetirado.ToString("C2");
            lbTRetirado.Text = cantidadTotalDineroRetirado.ToString("C2");
        }

        private void seccionEmpleadoDineroAgregado(string idEmpleado)
        {
            // Total de Dinero Agregado
            var idCajaUltimoCorte = idUltimoCorteDeCaja;

            if (!string.IsNullOrWhiteSpace(idCajaUltimoCorte))
            {
                using (DataTable dtSeccionDineroAgregadoEpleado = cn.CargarDatos(cs.totalCantiadesDepositosEmpleado(idCajaUltimoCorte, idEmpleado)))
                {
                    if (!dtSeccionDineroAgregadoEpleado.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtSeccionDineroAgregadoEpleado.Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(item["Efectivo"].ToString()))
                            {
                                cantidadEfectivoAgregado = Convert.ToDecimal(item["Efectivo"].ToString());
                                totalEfectivoDepsitosEnCaja += cantidadEfectivoAgregado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Tarjeta"].ToString()))
                            {
                                cantidadTarjetaAgregado = Convert.ToDecimal(item["Tarjeta"].ToString());
                                totalTarjetaDepositosEnCaja += cantidadTarjetaAgregado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Vales"].ToString()))
                            {
                                cantidadValesAgregado = Convert.ToDecimal(item["Vales"].ToString());
                                totalValesDepositosEnCaja += cantidadValesAgregado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Cheque"].ToString()))
                            {
                                cantidadChequeAgregado = Convert.ToDecimal(item["Cheque"].ToString());
                                totalChequesDepsoitosEnCaja += cantidadChequeAgregado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Transferencia"].ToString()))
                            {
                                cantidadTransferenciaAgregado = Convert.ToDecimal(item["Transferencia"].ToString());
                                totalTransferenciasDepositosEnCaja += cantidadTransferenciaAgregado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["TotalDepositos"].ToString()))
                            {
                                cantidadTotalDineroAgregado = Convert.ToDecimal(item["TotalDepositos"].ToString());
                            }

                            lbTEfectivoD.Text = cantidadEfectivoAgregado.ToString("C2");
                            lbTTarjetaD.Text = cantidadTarjetaAgregado.ToString("C2");
                            lbTValesD.Text = cantidadValesAgregado.ToString("C2");
                            lbTChequeD.Text = cantidadChequeAgregado.ToString("C2");
                            lbTTransD.Text = cantidadTransferenciaAgregado.ToString("C2");
                            lbTAgregado.Text = cantidadTotalDineroAgregado.ToString("C2");
                        }
                    }
                }
            }
            else
            {
                limpiarVariablesCantidadesDeDineroAgregado();
            }
        }

        private void limpiarVariablesCantidadesDeDineroAgregado()
        {
            if (opcionComboBoxFiltroAdminEmp.Equals("All"))
            {
                cantidadEfectivoAgregaddo = cantidadTarjetaAgregaddo = cantidadValesAgregaddo = cantidadChequeAgregaddo = cantidadTransferenciaAgregaddo = cantidadTotalDineroAgregado = 0;

                lbTEfectivoD.Text = cantidadEfectivoAgregaddo.ToString("C2");
                lbTTarjetaD.Text = cantidadTarjetaAgregaddo.ToString("C2");
                lbTValesD.Text = cantidadValesAgregaddo.ToString("C2");
                lbTChequeD.Text = cantidadChequeAgregaddo.ToString("C2");
                lbTTransD.Text = cantidadTransferenciaAgregaddo.ToString("C2");
                lbTAgregado.Text = cantidadTotalDineroAgregado.ToString("C2");
            }
            else
            {
                cantidadEfectivoAgregado = cantidadTarjetaAgregado = cantidadValesAgregado = cantidadChequeAgregado = cantidadTransferenciaAgregado = cantidadTotalDineroAgregado = 0;

                lbTEfectivoD.Text = cantidadEfectivoAgregado.ToString("C2");
                lbTTarjetaD.Text = cantidadTarjetaAgregado.ToString("C2");
                lbTValesD.Text = cantidadValesAgregado.ToString("C2");
                lbTChequeD.Text = cantidadChequeAgregado.ToString("C2");
                lbTTransD.Text = cantidadTransferenciaAgregado.ToString("C2");
                lbTAgregado.Text = cantidadTotalDineroAgregado.ToString("C2");
            }
        }

        private void seccionEmpleadoAnticipos(string idEmpleado)
        {
            var idCajaUltimoCorte = idUltimoCorteDeCaja;

            if (!string.IsNullOrWhiteSpace(idCajaUltimoCorte))
            {
                using (DataTable dtSeccionAnticiposEpleado = cn.CargarDatos(cs.totalCantiadesAnticiposEmpleado(idCajaUltimoCorte, idEmpleado)))
                {
                    if (!dtSeccionAnticiposEpleado.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtSeccionAnticiposEpleado.Rows)
                        {
                            decimal cantidadEfectivo = 0,
                                    cantidadTarjeta = 0,
                                    cantidadVales = 0,
                                    cantidadCheque = 0,
                                    cantidadTransferencia = 0,
                                    cantidadTotalAnticipos = 0;

                            if (!string.IsNullOrWhiteSpace(item["Efectivo"].ToString()))
                            {
                                cantidadEfectivo = Convert.ToDecimal(item["Efectivo"].ToString());
                                totalEfectivoAnticiposEnCaja += cantidadEfectivo;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Tarjeta"].ToString()))
                            {
                                cantidadTarjeta = Convert.ToDecimal(item["Tarjeta"].ToString());
                                totalTarjetaAnticiposEnCaja += cantidadTarjeta;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Vales"].ToString()))
                            {
                                cantidadVales = Convert.ToDecimal(item["Vales"].ToString());
                                totalValesAnticiposEnCaja += cantidadVales;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Cheque"].ToString()))
                            {
                                cantidadCheque = Convert.ToDecimal(item["Cheque"].ToString());
                                totalChequesAnticipoEnCaja += cantidadCheque;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Transferencia"].ToString()))
                            {
                                cantidadTransferencia = Convert.ToDecimal(item["Transferencia"].ToString());
                                totalTransferenciaAnticiposEnCaja += cantidadTransferencia;
                            }

                            if (!string.IsNullOrWhiteSpace(item["TotalAnticipos"].ToString()))
                            {
                                cantidadTotalAnticipos = Convert.ToDecimal(item["TotalAnticipos"].ToString());
                            }

                            lbTEfectivoA.Text = cantidadEfectivo.ToString("C2");
                            lbTTarjetaA.Text = cantidadTarjeta.ToString("C2");
                            lbTValesA.Text = cantidadVales.ToString("C2");
                            lbTChequeA.Text = cantidadCheque.ToString("C2");
                            lbTTransA.Text = cantidadTransferencia.ToString("C2");
                            lbTAnticiposA.Text = cantidadTotalAnticipos.ToString("C2");
                        }
                    }
                }
            }
            else
            {
                limpiarVariablesCantidadesDeAnticipos();
            }
        }

        private void limpiarVariablesCantidadesDeAnticipos()
        {
            cantidadEfectivoAnticipos = cantidadTarjetaAnticipos = cantidadValesAnticipos = cantidadChequeAnticipos = cantidadTransferenciaAnticipos = cantidadTotalAnticipos = 0;

            lbTEfectivoA.Text = cantidadEfectivo.ToString("C2");
            lbTTarjetaA.Text = cantidadTarjeta.ToString("C2");
            lbTValesA.Text = cantidadVales.ToString("C2");
            lbTChequeA.Text = cantidadCheque.ToString("C2");
            lbTTransA.Text = cantidadTransferencia.ToString("C2");
            lbTAnticiposA.Text = cantidadTotalAnticipos.ToString("C2");
        }

        private void seccionEmpleadoVentas(string idEmpleado)
        {
            var idCajaUltimoCorte = idUltimoCorteDeCaja;

            if (!string.IsNullOrWhiteSpace(idCajaUltimoCorte))
            {
                using (DataTable dtSeccionVentaEmpleado = cn.CargarDatos(cs.totalCantidadesVentasEmpleado(idCajaUltimoCorte, idEmpleado)))
                {
                    if (!dtSeccionVentaEmpleado.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtSeccionVentaEmpleado.Rows)
                        {
                            decimal cantidadEfectivo = 0,
                                    cantidadTarjeta = 0,
                                    cantidadVales = 0,
                                    cantidadCheque = 0,
                                    cantidadTransferencia = 0,
                                    cantidadCredito = 0,
                                    cantidadAbonos = 0,
                                    cantidadAnticipos = 0,
                                    cantidadTotalVentas = 0;

                            if (!string.IsNullOrWhiteSpace(item["Efectivo"].ToString()))
                            {
                                cantidadEfectivo = Convert.ToDecimal(item["Efectivo"].ToString());
                                totalEfectivoVentaEnCaja += cantidadEfectivo;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Tarjeta"].ToString()))
                            {
                                cantidadTarjeta = Convert.ToDecimal(item["Tarjeta"].ToString());
                                totalTarjetaVentaEnCaja += cantidadTarjeta;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Vales"].ToString()))
                            {
                                cantidadVales = Convert.ToDecimal(item["Vales"].ToString());
                                totalValesEnVentaCaja += cantidadVales;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Cheque"].ToString()))
                            {
                                cantidadCheque = Convert.ToDecimal(item["Cheque"].ToString());
                                totalChequesVentaEnCaja += cantidadCheque;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Transferencia"].ToString()))
                            {
                                cantidadTransferencia = Convert.ToDecimal(item["Transferencia"].ToString());
                                totalTransferenciaVentaEnCaja += cantidadTransferencia;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Credito"].ToString()))
                            {
                                var cantidadCreditoResultadoBaseDeDatos = Convert.ToDecimal(item["Credito"].ToString());

                                if (cantidadCreditoResultadoBaseDeDatos > 0)
                                {
                                    cantidadCredito = cantidadCreditoResultadoBaseDeDatos - totalAbonoRealizado;
                                }
                                else if (cantidadCreditoResultadoBaseDeDatos.Equals(0))
                                {
                                    cantidadCredito = 0;
                                }
                            }

                            //if (!string.IsNullOrWhiteSpace(item["Anticipo"].ToString()))
                            //{
                            //    cantidadAbonos = Convert.ToDecimal(item["Anticipo"].ToString());
                            //}

                            if (!string.IsNullOrWhiteSpace(item["Anticipo"].ToString()))
                            {
                                cantidadAnticipos = Convert.ToDecimal(item["Anticipo"].ToString());
                            }

                            if (!string.IsNullOrWhiteSpace(item["TotalVentas"].ToString()))
                            {
                                cantidadTotalVentas = Convert.ToDecimal(item["TotalVentas"].ToString());
                            }

                            lbTEfectivo.Text = cantidadEfectivo.ToString("C2");
                            lbTTarjeta.Text = cantidadTarjeta.ToString("C2");
                            lbTVales.Text = cantidadVales.ToString("C2");
                            lbTCheque.Text = cantidadCheque.ToString("C2");
                            lbTTrans.Text = cantidadTransferencia.ToString("C2");
                            lbTCredito.Text = cantidadCredito.ToString("C2");
                            //lbTCreditoC.Text = cantidadAbonos.ToString("C2");
                            lbTAnticipos.Text = cantidadAnticipos.ToString("C2");
                            lbTVentas.Text = cantidadTotalVentas.ToString("C2");
                        }
                    }
                }
            }
            else
            {
                limpiarVariablesCantidadesDeVentas();
            }
        }

        private void limpiarVariablesCantidadesDeVentas()
        {
            if (opcionComboBoxFiltroAdminEmp.Equals("All"))
            {
                cantidadEfectivoVentaTodos = cantidadTarjetaVentaTodos = cantidadValesVentaTodos = cantidadChequeVentaTodos = cantidadTransferenciaVentaTodos = cantidadCreditoVentaTodos = cantidadAbonosVentaTodos = cantidadAnticiposVentaTodos = cantidadTotalVentasVentaTodos = 0;

                lbTEfectivo.Text = cantidadEfectivoVentaTodos.ToString("C2");
                lbTTarjeta.Text = cantidadTarjetaVentaTodos.ToString("C2");
                lbTVales.Text = cantidadValesVentaTodos.ToString("C2");
                lbTCheque.Text = cantidadChequeVentaTodos.ToString("C2");
                lbTTrans.Text = cantidadTransferenciaVentaTodos.ToString("C2");
                lbTCredito.Text = cantidadCreditoVentaTodos.ToString("C2");
                //lbTCreditoC.Text = cantidadAbonos.ToString("C2");
                lbTAnticipos.Text = cantidadAnticiposVentaTodos.ToString("C2");
                lbTVentas.Text = cantidadTotalVentasVentaTodos.ToString("C2");
            }
            else
            {
                cantidadEfectivo = cantidadTarjeta = cantidadVales = cantidadCheque = cantidadTransferencia = cantidadCredito = cantidadAnticipos = cantidadTotalVentas = 0;

                lbTEfectivo.Text = cantidadEfectivo.ToString("C2");
                lbTTarjeta.Text = cantidadTarjeta.ToString("C2");
                lbTVales.Text = cantidadVales.ToString("C2");
                lbTCheque.Text = cantidadCheque.ToString("C2");
                lbTTrans.Text = cantidadTransferencia.ToString("C2");
                lbTCredito.Text = cantidadCredito.ToString("C2");
                //lbTCreditoC.Text = cantidadAbonos.ToString("C2");
                lbTAnticipos.Text = cantidadAnticipos.ToString("C2");
                lbTVentas.Text = cantidadTotalVentas.ToString("C2");
            }
        }

        private void seccionAdminCaja()
        {
            seccionAdminVentas();
            seccionAdminAnticipos();
            seccionAdminDineroAgregado();
            seccionAdminDineroRetirado();
        }

        private void seccionAdminDineroRetirado()
        {
            // Total de Dinero Retirado
            var idCajaUltimoCorte = idUltimoCorteDeCaja;

            if (!string.IsNullOrWhiteSpace(idCajaUltimoCorte))
            {
                using (DataTable dtSeccionDineroRetiradoAdministrador = cn.CargarDatos(cs.totalCantidadesRetirosAdministrador(idCajaUltimoCorte)))
                {
                    if (!dtSeccionDineroRetiradoAdministrador.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtSeccionDineroRetiradoAdministrador.Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(item["Efectivo"].ToString()))
                            {
                                cantidadEfectivoRetirado = Convert.ToDecimal(item["Efectivo"].ToString());
                                totalEfectivoRetiroEnCaja += cantidadEfectivoRetirado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Tarjeta"].ToString()))
                            {
                                cantidadTarjetaRetirado = Convert.ToDecimal(item["Tarjeta"].ToString());
                                totalTarjetaRetiroEnCaja += cantidadTarjetaRetirado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Vales"].ToString()))
                            {
                                cantidadValesRetirado = Convert.ToDecimal(item["Vales"].ToString());
                                totalValesRetiroEnCaja += cantidadValesRetirado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Cheque"].ToString()))
                            {
                                cantidadChequeRetirado = Convert.ToDecimal(item["Cheque"].ToString());
                                totalChequesRetiroEnCaja += cantidadChequeRetirado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Transferencia"].ToString()))
                            {
                                cantidadTransferenciaRetirado = Convert.ToDecimal(item["Transferencia"].ToString());
                                totalTransferenciaRetiroEnCaja += cantidadTransferenciaRetirado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["TotalRetiros"].ToString()))
                            {
                                cantidadTotalDineroRetirado = Convert.ToDecimal(item["TotalRetiros"].ToString());
                            }

                            lbEfectivoR.Text = cantidadEfectivoRetirado.ToString("C2");
                            lbTarjetaR.Text = cantidadTarjetaRetirado.ToString("C2");
                            lbValesR.Text = cantidadValesRetirado.ToString("C2");
                            lbChequeR.Text = cantidadChequeRetirado.ToString("C2");
                            lbTransferenciaR.Text = cantidadTransferenciaRetirado.ToString("C2");
                            lbTRetirado.Text = cantidadTotalDineroRetirado.ToString("C2");
                        }
                    }
                }
            }
            else
            {
                limpiarVariablesCantidadesDeDineroRetirado();
            }
        }

        private void seccionAdminDineroAgregado()
        {
            // Total de Dinero Agregado
            var idCajaUltimoCorte = idUltimoCorteDeCaja;

            if (!string.IsNullOrWhiteSpace(idCajaUltimoCorte))
            {
                using (DataTable dtSeccionDineroAgregadoAdministrador = cn.CargarDatos(cs.totalCantidadesDepositosAdministrador(idCajaUltimoCorte)))
                {
                    if (!dtSeccionDineroAgregadoAdministrador.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtSeccionDineroAgregadoAdministrador.Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(item["Efectivo"].ToString()))
                            {
                                cantidadEfectivoAgregado = Convert.ToDecimal(item["Efectivo"].ToString());
                                totalEfectivoDepsitosEnCaja += cantidadEfectivoAgregado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Tarjeta"].ToString()))
                            {
                                cantidadTarjetaAgregado = Convert.ToDecimal(item["Tarjeta"].ToString());
                                totalTarjetaDepositosEnCaja += cantidadTarjetaAgregado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Vales"].ToString()))
                            {
                                cantidadValesAgregado = Convert.ToDecimal(item["Vales"].ToString());
                                totalValesDepositosEnCaja += cantidadValesAgregado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Cheque"].ToString()))
                            {
                                cantidadChequeAgregado = Convert.ToDecimal(item["Cheque"].ToString());
                                totalChequesDepsoitosEnCaja += cantidadChequeAgregado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Transferencia"].ToString()))
                            {
                                cantidadTransferenciaAgregado = Convert.ToDecimal(item["Transferencia"].ToString());
                                totalTransferenciasDepositosEnCaja += cantidadTransferenciaAgregado;
                            }

                            if (!string.IsNullOrWhiteSpace(item["TotalDepositos"].ToString()))
                            {
                                cantidadTotalDineroAgregado = Convert.ToDecimal(item["TotalDepositos"].ToString());
                            }

                            lbTEfectivoD.Text = cantidadEfectivoAgregado.ToString("C2");
                            lbTTarjetaD.Text = cantidadTarjetaAgregado.ToString("C2");
                            lbTValesD.Text = cantidadValesAgregado.ToString("C2");
                            lbTChequeD.Text = cantidadChequeAgregado.ToString("C2");
                            lbTTransD.Text = cantidadTransferenciaAgregado.ToString("C2");
                            lbTAgregado.Text = cantidadTotalDineroAgregado.ToString("C2");
                        }
                    }
                }
            }
            else
            {
                limpiarVariablesCantidadesDeDineroAgregado();
            }
        }

        private void seccionAdminAnticipos()
        {
            // Total de Anticipos
            var idCajaUltimoCorte = idUltimoCorteDeCaja;

            if (!string.IsNullOrWhiteSpace(idCajaUltimoCorte))
            {
                using (DataTable dtSeccionAnticiposAdministrador = cn.CargarDatos(cs.totalCantidadesAnticiposAdministrador(idCajaUltimoCorte)))
                {
                    if (!dtSeccionAnticiposAdministrador.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtSeccionAnticiposAdministrador.Rows)
                        {
                            decimal cantidadEfectivo = 0,
                                    cantidadTarjeta = 0,
                                    cantidadVales = 0,
                                    cantidadCheque = 0,
                                    cantidadTransferencia = 0,
                                    cantidadTotalAnticipos = 0;

                            if (!string.IsNullOrWhiteSpace(item["Efectivo"].ToString()))
                            {
                                cantidadEfectivo = Convert.ToDecimal(item["Efectivo"].ToString());
                                totalEfectivoAnticiposEnCaja += cantidadEfectivo;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Tarjeta"].ToString()))
                            {
                                cantidadTarjeta = Convert.ToDecimal(item["Tarjeta"].ToString());
                                totalTarjetaAnticiposEnCaja += cantidadTarjeta;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Vales"].ToString()))
                            {
                                cantidadVales = Convert.ToDecimal(item["Vales"].ToString());
                                totalValesAnticiposEnCaja += cantidadVales;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Cheque"].ToString()))
                            {
                                cantidadCheque = Convert.ToDecimal(item["Cheque"].ToString());
                                totalChequesAnticipoEnCaja += cantidadCheque;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Transferencia"].ToString()))
                            {
                                cantidadTransferencia = Convert.ToDecimal(item["Transferencia"].ToString());
                                totalTransferenciaAnticiposEnCaja += cantidadTransferencia;
                            }

                            if (!string.IsNullOrWhiteSpace(item["TotalAnticipos"].ToString()))
                            {
                                cantidadTotalAnticipos = Convert.ToDecimal(item["TotalAnticipos"].ToString());
                            }

                            lbTEfectivoA.Text = cantidadEfectivo.ToString("C2");
                            lbTTarjetaA.Text = cantidadTarjeta.ToString("C2");
                            lbTValesA.Text = cantidadVales.ToString("C2");
                            lbTChequeA.Text = cantidadCheque.ToString("C2");
                            lbTTransA.Text = cantidadTransferencia.ToString("C2");
                            lbTAnticiposA.Text = cantidadTotalAnticipos.ToString("C2");
                        }
                    }
                }
            }
            else
            {
                limpiarVariablesCantidadesDeAnticipos();
            }
        }

        private void seccionAdminVentas()
        {
            // Total de Efectivo
            var idCajaUltimoCorte = idUltimoCorteDeCaja;

            if (!string.IsNullOrWhiteSpace(idCajaUltimoCorte))
            {
                using (DataTable dtSeccionVentaAdministrador = cn.CargarDatos(cs.totalCantidadesVentasAdministrador(idCajaUltimoCorte)))
                {
                    if (!dtSeccionVentaAdministrador.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtSeccionVentaAdministrador.Rows)
                        {
                            decimal cantidadEfectivo = 0,
                                    cantidadTarjeta = 0,
                                    cantidadVales = 0,
                                    cantidadCheque = 0,
                                    cantidadTransferencia = 0,
                                    cantidadCredito = 0,
                                    cantidadAbonos = 0,
                                    cantidadAnticipos = 0,
                                    cantidadTotalVentas = 0;

                            if (!string.IsNullOrWhiteSpace(item["Efectivo"].ToString()))
                            {
                                cantidadEfectivo = Convert.ToDecimal(item["Efectivo"].ToString());
                                totalEfectivoVentaEnCaja += cantidadEfectivo;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Tarjeta"].ToString()))
                            {
                                cantidadTarjeta = Convert.ToDecimal(item["Tarjeta"].ToString());
                                totalTarjetaVentaEnCaja += cantidadTarjeta;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Vales"].ToString()))
                            {
                                cantidadVales = Convert.ToDecimal(item["Vales"].ToString());
                                totalValesEnVentaCaja += cantidadVales;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Cheque"].ToString()))
                            {
                                cantidadCheque = Convert.ToDecimal(item["Cheque"].ToString());
                                totalChequesVentaEnCaja += cantidadCheque;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Transferencia"].ToString()))
                            {
                                cantidadTransferencia = Convert.ToDecimal(item["Transferencia"].ToString());
                                totalTransferenciaVentaEnCaja += cantidadTransferencia;
                            }

                            if (!string.IsNullOrWhiteSpace(item["Credito"].ToString()))
                            {
                                var cantidadCreditoResultadoBaseDeDatos = Convert.ToDecimal(item["Credito"].ToString());

                                if (cantidadCreditoResultadoBaseDeDatos > 0)
                                {
                                    cantidadCredito = cantidadCreditoResultadoBaseDeDatos - totalAbonoRealizado;
                                }
                                else if (cantidadCreditoResultadoBaseDeDatos.Equals(0))
                                {
                                    cantidadCredito = 0;
                                }
                            }

                            //if (!string.IsNullOrWhiteSpace(item["Anticipo"].ToString()))
                            //{
                            //    cantidadAbonos = Convert.ToDecimal(item["Anticipo"].ToString());
                            //}

                            if (!string.IsNullOrWhiteSpace(item["Anticipo"].ToString()))
                            {
                                cantidadAnticipos = Convert.ToDecimal(item["Anticipo"].ToString());
                            }

                            if (!string.IsNullOrWhiteSpace(item["TotalVentas"].ToString()))
                            {
                                cantidadTotalVentas = Convert.ToDecimal(item["TotalVentas"].ToString());
                            }


                            lbTEfectivo.Text = cantidadEfectivo.ToString("C2");
                            lbTTarjeta.Text = cantidadTarjeta.ToString("C2");
                            lbTVales.Text = cantidadVales.ToString("C2");
                            lbTCheque.Text = cantidadCheque.ToString("C2");
                            lbTTrans.Text = cantidadTransferencia.ToString("C2");
                            lbTCredito.Text = cantidadCredito.ToString("C2");
                            lbTCreditoC.Text = totalAbonoRealizado.ToString("C2");
                            lbTAnticipos.Text = cantidadAnticipos.ToString("C2");
                            lbTVentas.Text = (cantidadEfectivo + cantidadTarjeta + cantidadVales + cantidadCheque + cantidadTransferencia + cantidadCredito + totalAbonoRealizado).ToString("C2");
                        }
                    }
                }
            }
            else
            {
                limpiarVariablesCantidadesDeVentas();
            }
        }

        private void clasificarTipoDeUsuario()
        {
            //opcionComboBoxFiltroAdminEmp = (string)cbFiltroAdminEmpleado.SelectedValue;
            opcionComboBoxFiltroAdminEmp = ((KeyValuePair<string, string>)cbFiltroAdminEmpleado.SelectedItem).Key;

            if (opcionComboBoxFiltroAdminEmp.Equals("Admin"))
            {
                using (DataTable dtAdmin = cn.CargarDatos(cs.obtenerDatosDeAdministrador(FormPrincipal.userID)))
                {
                    if (!dtAdmin.Rows.Count.Equals(0))
                    {
                        foreach (DataRow drAdmin in dtAdmin.Rows)
                        {
                            idAdministradorOrUsuario = Convert.ToInt32(drAdmin["ID"].ToString());
                            nombreDeUsuario = drAdmin["Usuario"].ToString();
                            razonSocialUsuario = drAdmin["RazonSocial"].ToString();
                        }
                    }
                }
            }
            else if (opcionComboBoxFiltroAdminEmp.Equals("All"))
            {

            }
            else
            {
                using (DataTable dtEmpleado = cn.CargarDatos(cs.obtenerDatosDeEmpleado(Convert.ToInt32(opcionComboBoxFiltroAdminEmp))))
                {
                    if (!dtEmpleado.Rows.Count.Equals(0))
                    {
                        foreach (DataRow drEmpleado in dtEmpleado.Rows)
                        {
                            idAdministradorOrUsuario = Convert.ToInt32(drEmpleado["ID"].ToString());
                            nombreDeUsuario = drEmpleado["nombre"].ToString();
                        }
                    }
                }
            }
        }
    }
}