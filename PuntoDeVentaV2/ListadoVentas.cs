﻿using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using TuesPechkin;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO.Compression;
using System.Threading;
using System.Globalization;

namespace PuntoDeVentaV2
{
    public partial class ListadoVentas : Form
    {
        //Status 1 = Venta terminada
        //Status 2 = Venta guardada
        //Status 3 = Venta cancelada
        //Status 4 = Venta a credito
        //Status 5 = Facturas
        //Status 6 = Presupuestos

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        Dictionary<int, string> idVentas = new Dictionary<int, string>();
        List<int> lista = new List<int>();

        public string rutaLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private string ticketGenerado = string.Empty;
        private string rutaTicketGenerado = string.Empty;
        private DateTime fechaUltimoCorte;
        private bool existenProductos;
        private bool hay_productos_habilitados;

        public static bool recargarDatos = false;
        public static bool abrirNuevaVenta = false;

        public static int retomarVentasCanceladas { get; set; }
        public static int obtenerIdVenta { get; set; }
        public static int folioVenta { get; set; }
        public static int obtenerIDVentaTimbrado { get; set; }

        public static int tipo_venta = 0;
        public static string[][] faltantes_productos;
        private static WebSettings _webSettings;
        #region Variables Globales Para Paginar
        private Paginar p;
        string DataMemberDGV = "Ventas";
        int maximo_x_pagina = 13;
        string FiltroAvanzado = string.Empty;
        int clickBoton = 0;
        #endregion Variables Globales Para Paginar

        bool ban_ver = false;

        CheckBox header_checkb = null;

        float saldoInicial = 0f;

        // Permisos de los botones
        int opcion1 = 1; // Cancelar venta
        int opcion2 = 1; // Ver nota venta
        int opcion3 = 1; // Ver ticket venta
        int opcion4 = 1; // Ver info venta
        int opcion5 = 1; // Timbrar factura
        int opcion6 = 1; // Enviar nota
        int opcion7 = 1; // Buscar venta
        int opcion8 = 1; // Nueva venta

        bool ban = false;

        IEnumerable<Ventas> FormVenta = Application.OpenForms.OfType<Ventas>();

        delegate void delegado(int valor);

        public static float validarEfectivo { get; set; }
        public static float validarTarjeta { get; set; }
        public static float validarVales { get; set; }
        public static float validarCheque { get; set; }
        public static float validarTrans { get; set; }

        string mensajeParaMostrar = string.Empty;

        string extra = string.Empty;
        int yaValidado = 0;

        public ListadoVentas()
        {
            InitializeComponent();

            MostrarCheckBox();
        }

        private void ListadoVentas_Load(object sender, EventArgs e)
        {
            // Se crea el directorio para almacenar los tickets y otros archivos relacionados con ventas
            Directory.CreateDirectory(@"C:\Archivos PUDVE\Ventas\Tickets");

            // Placeholder del campo buscador
            txtBuscador.GotFocus += new EventHandler(BuscarTieneFoco);
            txtBuscador.LostFocus += new EventHandler(BuscarPierdeFoco);
            fechaUltimoCorte = Convert.ToDateTime(mb.UltimaFechaCorte());
            //dpFechaInicial.Value = DateTime.Today.AddDays(-7);
            dpFechaInicial.Value = fechaUltimoCorte;
            dpFechaFinal.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Opciones para el combobox
            Dictionary<string, string> ventas = new Dictionary<string, string>();
            ventas.Add("VP", "Ventas pagadas");
            ventas.Add("VG", "Ventas guardadas (Presupuestos)");
            ventas.Add("VC", "Ventas canceladas");
            ventas.Add("VCC", "Ventas a crédito");

            cbTipoVentas.DataSource = ventas.ToArray();
            cbTipoVentas.DisplayMember = "Value";
            cbTipoVentas.ValueMember = "Key";

            cbVentas.SelectedIndex = 0;
            cbTipoVentas.SelectedIndex = 0;

            //fechaUltimoCorte = Convert.ToDateTime(mb.UltimaFechaCorte());

            clickBoton = 0;

            // Crea un checkbox en la cabecera de la tabla. Será para seleccionar todo.
            ag_checkb_header();

            existenProductos = mb.TieneProductos();

            var tipoDeBusqueda = 0;

            tipoDeBusqueda = verTipoDeBusqueda();

            CargarDatos(tipoDeBusqueda);

            actualizar();
            //btnUltimaPagina.PerformClick();
            btnPrimeraPagina.PerformClick();

            // Si es un empleado obtiene los permisos de los botones
            if (FormPrincipal.id_empleado > 0)
            {
                var permisos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Ventas");

                opcion1 = permisos[0];
                opcion2 = permisos[1];
                opcion3 = permisos[2];
                opcion4 = permisos[3];
                opcion5 = permisos[4];
                opcion6 = permisos[5];
                opcion7 = permisos[6];
                opcion8 = permisos[7];
            }

            hay_productos_habilitados = mb.tiene_productos_habilitados();
            this.Focus();

            string fechaCreacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            ////Realizar una operacion de retiro de caja para cuando sea una ceunta nueva 
            //cn.EjecutarConsulta($"INSERT INTO Caja (Operacion, Cantidad, Saldo, Concepto, FechaOperacion, IDUsuario, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, Anticipo ) VALUES('retiro', '0.00', '0.00', '', '{fechaCreacion}', '{FormPrincipal.userID}', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00' )");

            txtMaximoPorPagina.Text = maximo_x_pagina.ToString();

            verComboBoxAdministradorEmpleado();
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

        private void actualizar()
        {
            int BeforePage = 0, AfterPage = 0, LastPage = 0;

            linkLblPaginaAnterior.Visible = false;
            linkLblPaginaSiguiente.Visible = false;

            linkLblPaginaActual.Text = p.numPag().ToString();
            linkLblPaginaActual.LinkColor = System.Drawing.Color.White;
            linkLblPaginaActual.BackColor = System.Drawing.Color.Black;

            BeforePage = p.numPag() - 1;
            AfterPage = p.numPag() + 1;
            LastPage = p.countPag();

            if (Convert.ToInt32(linkLblPaginaActual.Text) >= 2)
            {
                linkLblPaginaAnterior.Text = BeforePage.ToString();
                linkLblPaginaAnterior.Visible = true;
                if (AfterPage <= LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = true;
                }
                else if (AfterPage > LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = false;
                }
            }
            else if (BeforePage < 1)
            {
                linkLblPaginaAnterior.Visible = false;
                if (AfterPage <= LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = true;
                }
                else if (AfterPage > LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = false;
                }
            }
        }

        #region Método para cargar los datos en el DataGridView
        public void CargarDatos(int estado = 1, bool busqueda = false, string clienteFolio = "")
        {
            var consulta = string.Empty;
            bool esNumero = false;

            extra = string.Empty;
            yaValidado = 0;

            if (clickBoton == 0)
            {
                if (busqueda)
                {
                    var buscador = txtBuscador.Text.Trim();
                    var fechaInicial = dpFechaInicial.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    var fechaFinal = dpFechaFinal.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    var opcion = cbTipoVentas.SelectedValue.ToString();

                    // Ventas pagadas
                    if (opcion == "VP") { estado = 1; }
                    // Ventas guardadas
                    if (opcion == "VG") { estado = 2; }
                    // Ventas canceladas
                    if (opcion == "VC") { estado = 3; }
                    // Ventas a credito
                    if (opcion == "VCC") { estado = 4; }


                    if (buscador.Equals("BUSCAR POR RFC, CLIENTE, EMPLEADO O FOLIO..."))
                    {
                        buscador = string.Empty;
                    }

                    if (string.IsNullOrWhiteSpace(buscador))
                    {
                        if (FormPrincipal.userNickName.Contains("@"))
                        {
                            //consulta = $"SELECT * FROM Ventas WHERE Status = {estado} AND IDEmpleado = {FormPrincipal.id_empleado} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' ORDER BY ID DESC";
                            if (estado.Equals(1)) // Ventas pagadas
                            {
                                consulta = cs.VerComoEpleadoTodasMisVentasPagadasPorFechas(estado, FormPrincipal.id_empleado, fechaInicial, fechaFinal);
                            }
                            else if (estado.Equals(2)) // Ventas guardadas
                            {
                                consulta = cs.VerComoEpleadoTodasLaVentasGuardadasPorFechas(estado, fechaInicial, fechaFinal);
                            }
                            else if (estado.Equals(3)) // Ventas canceladas
                            {
                                consulta = cs.VerComoEpleadoTodasMisVentasCanceladasPorFechas(estado, FormPrincipal.id_empleado, fechaInicial, fechaFinal);
                            }
                            else if (estado.Equals(4)) // Ventas a credito
                            {
                                consulta = cs.VerComoEpleadoTodasLaVentasACreditoPorFechas(estado, fechaInicial, fechaFinal);
                            }
                        }
                        else
                        {
                            //consulta = $"SELECT * FROM Ventas WHERE Status = {estado} AND IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' ORDER BY ID DESC";
                            if (estado.Equals(1)) // Ventas pagadas
                            {
                                consulta = cs.VerComoAdministradorTodasLaVentasMiasPagadasPorFechas(estado, fechaInicial, fechaFinal);
                            }
                            else if (estado.Equals(2)) // Ventas guardadas
                            {
                                consulta = cs.VerComoAdministradorTodasLaVentasGuardadasPorFechas(estado, fechaInicial, fechaFinal);
                            }
                            else if (estado.Equals(3)) // Ventas canceladas
                            {
                                consulta = cs.VerComoAdministradorTodasLaVentasCanceladasMiasPorFechas(estado, fechaInicial, fechaFinal);
                            }
                            else if (estado.Equals(4)) // Ventas a credito
                            {
                                consulta = cs.VerComoAdministradorTodasLaVentasACreditoPorFechas(estado, fechaInicial, fechaFinal);
                            }
                        }
                    }
                    else
                    {
                        int n;
                        
                        esNumero = int.TryParse(buscador, out n);

                        if (esNumero)
                        {
                            //extra = $"AND Folio = {buscador}";
                            extra = cs.ParametroDeBusquedaFolioSiendoAdministrador(buscador);
                        }
                        else
                        {
                            //var idEmpleado = cs.NombreEmpleado(buscador);

                            //if (idEmpleado == FormPrincipal.id_empleado.ToString() || !FormPrincipal.userNickName.Contains("@"))
                            //{
                            //    if (!string.IsNullOrEmpty(idEmpleado))
                            //    {
                            //        extra = $"AND (Cliente LIKE '%{buscador}%' OR RFC LIKE '%{buscador}%' OR IDEmpleado = '{idEmpleado}')";
                            //    }
                            //    else
                            //    {
                            //        //extra = $"AND (Cliente LIKE '%{buscador}%' OR RFC LIKE '%{buscador}%')";
                            //        extra = cs.ParametrosDeBusquedaNombreRFCSiendoAdministrador(buscador);
                            //    }
                            //}

                            validacionSiEstaVaciaLaCadenaExtra();
                            
                            using (DataTable dtCliete = cn.CargarDatos(cs.getDatosClienteVentas(buscador, fechaInicial, fechaFinal)))
                            {
                                if (!dtCliete.Rows.Count.Equals(0))
                                {
                                    validacionSiEstaVaciaLaCadenaExtra();
                                    extra += cs.ParametrosDeBusquedaNombreRFCSiendoAdministrador(buscador);
                                }
                            }

                            var opcionFiltrado = cbTipoVentas.SelectedValue.ToString();

                            if (FormPrincipal.userNickName.Contains("@"))
                            {
                                if (opcionFiltrado == "VP") //Ventas pagadas
                                {
                                    buscarSoloEmpleado(buscador, fechaInicial, fechaFinal);
                                }
                                else if (opcionFiltrado == "VG") //Ventas guardadas
                                {
                                    buscarEmpleadoYAdministrador(buscador, fechaInicial, fechaFinal);
                                }
                                else if (opcionFiltrado == "VC") //Ventas canceladas
                                {
                                    buscarSoloEmpleado(buscador, fechaInicial, fechaFinal);
                                }
                                else if (opcionFiltrado == "VCC") //Ventas a credito
                                {
                                    buscarEmpleadoYAdministrador(buscador, fechaInicial, fechaFinal);
                                }
                            }
                            else
                            {
                                if (opcionFiltrado == "VP") //Ventas pagadas
                                {
                                    buscarSoloAdministrador(buscador, fechaInicial, fechaFinal);
                                }
                                else if (opcionFiltrado == "VG") //Ventas guardadas
                                {
                                    buscarEmpleadoYAdministrador(buscador, fechaInicial, fechaFinal);
                                }
                                else if (opcionFiltrado == "VC") //Ventas canceladas
                                {
                                    buscarSoloAdministrador(buscador, fechaInicial, fechaFinal);
                                }
                                else if (opcionFiltrado == "VCC") //Ventas a credito
                                {
                                    buscarEmpleadoYAdministrador(buscador, fechaInicial, fechaFinal);
                                }
                            }

                            if (extra.Equals("AND ( "))
                            {
                                validacionSiEstaVaciaLaCadenaExtra();
                                extra += cs.ParametrosDeBusquedaNombreRFCSiendoAdministrador(buscador);
                            }

                            extra += " ) ";
                        }
                        if (FormPrincipal.userNickName.Contains("@"))
                        {
                            //consulta = $"SELECT * FROM Ventas WHERE Status = {estado} AND IDEmpleado = {FormPrincipal.id_empleado} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' {extra} ORDER BY ID DESC";
                            if (estado.Equals(1)) // Ventas pagadas
                            {
                                consulta = cs.VerComoEmpleadoTodasMisVentasPagadasPorFechasYBusqueda(estado, FormPrincipal.id_empleado, fechaInicial, fechaFinal, extra);
                            }
                            else if (estado.Equals(2)) // Ventas guardadas
                            {
                                consulta = cs.VerComoEmpleadoTodasLasVentasGuardadasPorFechasYBusqueda(estado, fechaInicial, fechaFinal, extra);
                            }
                            else if (estado.Equals(3)) // Ventas canceladas
                            {
                                consulta = cs.VerComoEmpleadoTodasMisVentasCanceladasPorFechasYBusqueda(estado, FormPrincipal.id_empleado, fechaInicial, fechaFinal, extra);
                            }
                            else if (estado.Equals(4)) // Ventas a credito
                            {
                                consulta = cs.VerComoEmpleadoTodasLasVentasACreditoPorFechasYBusqueda(estado, fechaInicial, fechaFinal, extra);
                            }
                        }
                        else
                        {
                            //consulta = $"SELECT * FROM Ventas WHERE Status = {estado} AND IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' {extra} ORDER BY ID DESC";

                            if (estado.Equals(1)) // Ventas pagadas
                            {
                                consulta = cs.VerComoAdministradorTodasLaVentasPagadasPorFechasYBusqueda(estado, fechaInicial, fechaFinal, extra);
                            }
                            else if (estado.Equals(2)) // Ventas guardadas
                            {
                                consulta = cs.VerComoAdministradorTodasLaVentasGuardadasPorFechasYBusqueda(estado, fechaInicial, fechaFinal, extra);
                            }
                            else if (estado.Equals(3)) // Ventas canceladas
                            {
                                consulta = cs.VerComoAdministradorTodasLaVentasCanceladasPorFechasYBusqueda(estado, fechaInicial, fechaFinal, extra);
                            }
                            else if (estado.Equals(4)) // Ventas a credito
                            {
                                consulta = cs.VerComoAdministradorTodasLaVentasACreditoPorFechasYBusqueda(estado, fechaInicial, fechaFinal, extra);
                            }
                        }
                        
                        txtBuscador.Text = string.Empty;
                    }
                }
                else
                {
                    var fechaInicial = dpFechaInicial.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    dpFechaFinal.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var fechaFinal = dpFechaFinal.Value.ToString("yyyy-MM-dd HH:mm:ss");

                    if (FormPrincipal.userNickName.Contains("@"))
                    {
                        //consulta = $"SELECT * FROM Ventas WHERE Status = {estado} AND IDEmpleado = {FormPrincipal.id_empleado} AND FechaOperacion > '{fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss")}' ORDER BY ID DESC";

                        if (estado.Equals(1)) // Ventas pagadas
                        {
                            consulta = cs.VerComoEmpleadoTodasMisVentasPagadas(estado, FormPrincipal.id_empleado, fechaInicial, fechaFinal);
                        }
                        else if (estado.Equals(2)) // Ventas guardadas
                        {
                            consulta = cs.VerComoEmpleadoTodasLasVentasGuardadas(estado, fechaInicial, fechaFinal);
                        }
                        else if (estado.Equals(3)) // Ventas canceladas
                        {
                            consulta = cs.VerComoEmpleadoTodasMisVentasCanceladas(estado, FormPrincipal.id_empleado, fechaInicial, fechaFinal);
                        }
                        else if (estado.Equals(4)) // Ventas a credito
                        {
                            consulta = cs.VerComoEmpleadoTodasLasVentasACredito(estado, fechaInicial, fechaFinal);
                        }
                    }
                    else
                    {
                        //consulta = $"SELECT * FROM Ventas WHERE Status = {estado} AND IDUsuario = {FormPrincipal.userID} AND FechaOperacion > '{fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss")}' ORDER BY ID DESC";
                        if (estado.Equals(1)) // Ventas pagadas
                        {
                            consulta = cs.VerComoAdministradorTodasLasVentasPagadas(estado, fechaInicial, fechaFinal);
                        }
                        else if (estado.Equals(2)) // Ventas guardadas
                        {
                            consulta = cs.VerComoAdministradorTodasLasVentasGuardadas(estado, fechaInicial, fechaFinal);
                        }
                        else if (estado.Equals(3)) // Ventas canceladas
                        {
                            consulta = cs.VerComoAdministradorTodasLasVentasCanceladas(estado, fechaInicial, fechaFinal);
                        }
                        else if (estado.Equals(4)) // Ventas a credito
                        {
                            consulta = cs.VerComoAdministradorTodasLasVentasACredito(estado, fechaInicial, fechaFinal);
                        }
                    }
                    
                }

                FiltroAvanzado = consulta;

                p = new Paginar(FiltroAvanzado, DataMemberDGV, maximo_x_pagina);
            }

            DGVListadoVentas.Rows.Clear();

            var opcionOcultarColumnasEnCancelar = cbTipoVentas.SelectedValue.ToString();

            if (estado.Equals(3) || opcionOcultarColumnasEnCancelar.Equals("VC"))
            {
                DGVListadoVentas.Columns["Cancelar"].Visible = false;
                DGVListadoVentas.Columns["Timbrar"].Visible = false;
            }
            else
            {
                DGVListadoVentas.Columns["Cancelar"].Visible = true;
                DGVListadoVentas.Columns["Timbrar"].Visible = true;
            }

            DataSet datos = p.cargar();
            DataTable dtDatos = datos.Tables[0];

            // Inicializacion de iconos
            Image cancelar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\remove.png");
            Image factura = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-pdf-o.png");
            Image ticket = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\ticket.png");
            Image credito = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\dollar.png");
            Image info = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\dollar.png");
            Image timbrar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\bell.png");
            Image informacion = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\info-circle.png");
            Image reusarVentas = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\repeat.png");


            Bitmap sinImagen = new Bitmap(1, 1);
            sinImagen.SetPixel(0, 0, Color.White);

            if (dtDatos.Rows.Count > 0)
            {
                float iva = 0f;
                float subtotal = 0f;
                float total = 0f;
                foreach (DataRow filaDatos in dtDatos.Rows)
                {
                    int idVenta = Convert.ToInt32(filaDatos["ID"].ToString());
                    int status = Convert.ToInt32(filaDatos["Status"].ToString());

                    //string cliente = "Público General";
                    //string rfc = "XAXX010101000";

                    string cliente = filaDatos["Cliente"].ToString();
                    string rfc = filaDatos["RFC"].ToString();
                    string vendedor = filaDatos["Vendedor"].ToString();

                    // Obtener detalle de venta y datos del cliente
                    var detalles = mb.ObtenerDetallesVenta(idVenta, FormPrincipal.userID);

                    if (detalles.Length > 0)
                    {
                        if (Convert.ToInt32(detalles[0]) > 0)
                        {
                            var infoCliente = mb.ObtenerDatosCliente(Convert.ToInt32(detalles[0]), FormPrincipal.userID);
                            cliente = infoCliente[0];
                            rfc = infoCliente[1];
                        }
                    }

                    // Obtener el cliente de la venta guardada
                    if (estado == 2)
                    {
                        var idCliente = Convert.ToInt32(filaDatos["IDCliente"]);
                        if (idCliente > 0)
                        {
                            var infoCliente = mb.ObtenerDatosCliente(idCliente, FormPrincipal.userID);
                            cliente = infoCliente[0];
                            rfc = infoCliente[1];
                        }
                    }

                    int rowId = DGVListadoVentas.Rows.Add();

                    DataGridViewRow row = DGVListadoVentas.Rows[rowId];

                    var ivaTmp = float.Parse(filaDatos["IVA16"].ToString());
                    var subtotalTmp = float.Parse(filaDatos["Subtotal"].ToString());
                    var totalTmp = float.Parse(filaDatos["Total"].ToString());
                    var iva8 = float.Parse(filaDatos["IVA8"].ToString());

                    if (iva8 > 0)
                    {
                        ivaTmp = iva8;
                    }

                    iva += ivaTmp;
                    subtotal += subtotalTmp;
                    total += totalTmp;

                    row.Cells["ID"].Value = idVenta;
                    row.Cells["col_checkbox"].Value = false;
                    row.Cells["Cliente"].Value = cliente;
                    row.Cells["RFC"].Value = rfc;
                    row.Cells["Vendedor"].Value = vendedor;
                    row.Cells["Subtotal"].Value = subtotalTmp.ToString("0.00");
                    row.Cells["IVA"].Value = ivaTmp.ToString("0.00");
                    row.Cells["Total"].Value = totalTmp.ToString("0.00");
                    row.Cells["Folio"].Value = filaDatos["Folio"].ToString();
                    row.Cells["Serie"].Value = filaDatos["Serie"].ToString();
                    row.Cells["Fecha"].Value = Convert.ToDateTime(filaDatos["FechaOperacion"].ToString());

                    row.Cells["Cancelar"].Value = cancelar;
                    row.Cells["Factura"].Value = factura;
                    row.Cells["Ticket"].Value = ticket;
                    row.Cells["Abono"].Value = credito;

                    row.Cells["Timbrar"].Value = timbrar;
                    // Ventas canceladas
                    if (estado == 3)
                    {
                        row.Cells["Timbrar"].Value = sinImagen;
                    }

                    row.Cells["cInformacion"].Value = informacion;
                    row.Cells["retomarVenta"].Value = reusarVentas;

                    // Ventas canceladas
                    if (status == 3)
                    {
                        row.Cells["Cancelar"].Value = sinImagen;
                    }

                    // Ventas a credito
                    if (status != 4)
                    {
                        row.Cells["Abono"].Value = info;
                    }

                    //Retomar Ventas Canceladas
                    if (status == 1 || status == 4)
                    {
                        row.Cells["retomarVenta"].Value = sinImagen;
                    }

                }

                AgregarTotales(iva, subtotal, total);

                using (DataTable dbTotalesGenerales = cn.CargarDatos(FiltroAvanzado))
                {
                    float ivaTmpGral = 0, subtotalTmpGral = 0, totalTmpGral = 0;
                    foreach (DataRow row in dbTotalesGenerales.Rows)
                    {
                        if (float.Parse(row["IVA8"].ToString()) > 0)
                        {
                            ivaTmpGral += float.Parse(row["IVA8"].ToString());
                        }
                        if (float.Parse(row["IVA16"].ToString()) > 0)
                        {
                            ivaTmpGral += float.Parse(row["IVA16"].ToString());
                        }
                        //ivaTmpGral += float.Parse(row["IVA16"].ToString());
                        subtotalTmpGral += float.Parse(row["Subtotal"].ToString());
                        totalTmpGral += float.Parse(row["Total"].ToString());
                    }
                    AgregarTotalesGenerales(ivaTmpGral, subtotalTmpGral, totalTmpGral);
                }

                DGVListadoVentas.FirstDisplayedScrollingRowIndex = DGVListadoVentas.RowCount - 1;

                DGVListadoVentas.FirstDisplayedScrollingColumnIndex = DGVListadoVentas.ColumnCount - 1;
            }
            else if (busqueda.Equals(true))
            {
                MessageBox.Show("No Se Encontraron Resultados\nDentro Del Rango De Búsqueda Seleccionada", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBuscador.Focus();
                busqueda = false;
                extra = string.Empty;
                esNumero = false;
                restaurarBusqueda();
            }

            tipo_venta = estado;

            llenarGDV();
        }

        private void validacionSiEstaVaciaLaCadenaExtra()
        {
            if (string.IsNullOrWhiteSpace(extra))
            {
                extra += "AND ( ";
            }
            else if (!string.IsNullOrWhiteSpace(extra))
            {
                if (extra.Contains("AND (") && yaValidado.Equals(0))
                {
                    yaValidado = 1;
                }
                else if (yaValidado.Equals(1))
                {
                    extra += "OR ";
                }
            }
        }

        private void buscarSoloAdministrador(string buscador, string fechaInicial, string fechaFinal)
        {
            var IDAdministrador = cs.NombreAdministrador(buscador);

            if (!string.IsNullOrWhiteSpace(IDAdministrador))
            {
                using (DataTable dtAdmin = cn.CargarDatos(cs.ParametroDeBusquedaIdUsuarioSiendoAdministrador(IDAdministrador, fechaInicial, fechaFinal)))
                {
                    if (!dtAdmin.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtAdmin.Rows)
                        {
                            validacionSiEstaVaciaLaCadenaExtra();
                            extra += cs.ParametrosDeBusquedaDeUsuarioSiendoAdministrador();
                        }
                    }
                }
            }
        }

        private void buscarEmpleadoYAdministrador(string buscador, string fechaInicial, string fechaFinal)
        {
            var IDEmpleado = cs.NombreEmpleado(buscador);

            if (!string.IsNullOrWhiteSpace(IDEmpleado))
            {
                using (DataTable dtEmpleado = cn.CargarDatos(cs.ParametroDeBusquedaIdEmpleadoSiendoAdministrador(IDEmpleado, fechaInicial, fechaFinal)))
                {
                    if (!dtEmpleado.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtEmpleado.Rows)
                        {
                            validacionSiEstaVaciaLaCadenaExtra();
                            extra += cs.ParametrosDeBusquedaDeEmpleadoSiendoAdministrador(buscador);
                        }
                    }
                }
            }

            var IDAdministrador = cs.NombreAdministrador(buscador);

            if (!string.IsNullOrWhiteSpace(IDAdministrador))
            {
                using (DataTable dtAdmin = cn.CargarDatos(cs.ParametroDeBusquedaIdUsuarioSiendoAdministrador(IDAdministrador, fechaInicial, fechaFinal)))
                {
                    if (!dtAdmin.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtAdmin.Rows)
                        {
                            validacionSiEstaVaciaLaCadenaExtra();
                            extra += cs.ParametrosDeBusquedaDeUsuarioSiendoAdministrador();
                        }
                    }
                }
            }
        }

        private void buscarSoloEmpleado(string buscador, string fechaInicial, string fechaFinal)
        {
            var IDEmpleado = cs.NombreEmpleado(buscador);

            if (!string.IsNullOrWhiteSpace(IDEmpleado))
            {
                using (DataTable dtEmpleado = cn.CargarDatos(cs.ParametroDeBusquedaIdEmpleadoSiendoAdministrador(IDEmpleado, fechaInicial, fechaFinal)))
                {
                    if (!dtEmpleado.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtEmpleado.Rows)
                        {
                            validacionSiEstaVaciaLaCadenaExtra();
                            extra += cs.ParametrosDeBusquedaDeEmpleadoSiendoAdministrador(buscador);
                        }
                    }
                }
            }
        }
        #endregion

        private void restaurarBusqueda()
        {
            var opcion = cbTipoVentas.SelectedValue.ToString();
            clickBoton = 0;

            if (opcion == "VP") //Ventas pagadas
            {
                CargarDatos(1);
            }
            else if (opcion == "VG") //Ventas guardadas
            {
                CargarDatos(2);
            }
            else if (opcion == "VC") //Ventas canceladas
            {
                CargarDatos(3);
            }
            else if (opcion == "VCC") //Ventas a credito
            {
                CargarDatos(4);
            }
        }

        private void AgregarTotalesGenerales(float ivaGral, float subtotalGral, float totalGral)
        {
            int idFila = DGVListadoVentas.Rows.Add();
            DataGridViewRow fila = DGVListadoVentas.Rows[idFila];
            fila.DefaultCellStyle.NullValue = null;
            fila.DefaultCellStyle.BackColor = Color.FromArgb(255, 207, 53, 20);
            fila.DefaultCellStyle.ForeColor = Color.White;
            fila.DefaultCellStyle.Font = new Font("Arial", 10f);
            fila.Cells["col_checkbox"].Value = false;
            fila.Cells["Cliente"].Value = "TOTAL GENERAL";
            fila.Cells["Subtotal"].Value = subtotalGral.ToString("0.00");
            fila.Cells["IVA"].Value = ivaGral.ToString("0.00");
            fila.Cells["Total"].Value = totalGral.ToString("0.00");
        }

        private void AgregarTotales(float iva, float subtotal, float total)
        {
            int idFila = DGVListadoVentas.Rows.Add();
            DataGridViewRow fila = DGVListadoVentas.Rows[idFila];
            fila.DefaultCellStyle.NullValue = null;
            fila.DefaultCellStyle.BackColor = Color.FromArgb(255, 207, 53, 20);
            fila.DefaultCellStyle.ForeColor = Color.White;
            fila.DefaultCellStyle.Font = new Font("Arial", 10f);
            fila.Cells["col_checkbox"].Value = false;
            //DGVListadoVentas.Rows[0].Cells[0].Visible = false;
            fila.Cells["Cliente"].Value = "TOTAL";
            fila.Cells["Subtotal"].Value = subtotal.ToString("0.00");
            fila.Cells["IVA"].Value = iva.ToString("0.00");
            fila.Cells["Total"].Value = total.ToString("0.00");
        }

        private void btnBuscarVentas_Click(object sender, EventArgs e)
        {
            if (opcion7 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            bool fechasValidas = validarFechasDeBusqueda();

            if (fechasValidas.Equals(false))
            {
                MessageBox.Show("Favor de verificar el rango de fechas seleccionadas", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var tipoDeBusqueda = 0;

            tipoDeBusqueda = verTipoDeBusqueda();

            CargarDatos(tipoDeBusqueda, busqueda: true);
            btnPrimeraPagina.PerformClick();
            //+++btnUltimaPagina.PerformClick();
        }

        private int verTipoDeBusqueda()
        {
            var estado = 1;

            var opcion = cbTipoVentas.SelectedValue.ToString();
            clickBoton = 0;

            if (opcion == "VP") //Ventas pagadas
            {
                estado = 1;
            }
            else if (opcion == "VG") //Ventas guardadas
            {
                estado = 2;
            }
            else if (opcion == "VC") //Ventas canceladas
            {
                estado = 3;
            }
            else if (opcion == "VCC") //Ventas a credito
            {
                estado = 4;
            }

            return estado;
        }

        private bool validarFechasDeBusqueda()
        {
            var validacionFecha = false;

            var fechaInicial = dpFechaInicial.Value.ToString("yyyy-MM-dd");
            var fechaFinal = dpFechaFinal.Value.ToString("yyyy-MM-dd");

            if (DateTime.Parse(fechaInicial) <= DateTime.Parse(fechaFinal))
            {
                validacionFecha = true;
            }

            return validacionFecha;
        }

        public void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            if (opcion8 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (existenProductos)
            {
                if (hay_productos_habilitados)
                {
                    if (FormVenta.Count() == 1)
                    {
                        if (FormVenta.First().WindowState == FormWindowState.Normal)
                        {
                            FormVenta.First().BringToFront();
                        }

                        if (FormVenta.First().WindowState == FormWindowState.Minimized)
                        {
                            FormVenta.First().WindowState = FormWindowState.Normal;
                        }
                    }
                    else
                    {
                        Ventas venta = new Ventas();

                        venta.Disposed += delegate
                        {
                            AbrirVentanaVenta();

                            cbTipoVentas.Text = "Ventas pagadas";

                            clickBoton = 0;

                            var tipoDeBusqueda = 0;

                            tipoDeBusqueda = verTipoDeBusqueda();

                            CargarDatos(tipoDeBusqueda);
                            actualizar();

                            //+++btnUltimaPagina.PerformClick();
                            btnPrimeraPagina.PerformClick();
                        };

                        venta.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Se ha detectado que tiene productos registrados pero estos estan deshabilitados, para poder realizar una venta es necesario tener productos habilitados.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                var mensaje = string.Join(
                    Environment.NewLine,
                    "Para poder realizar una venta es necesario registrar",
                    "productos, ya que actualmente el sistema ha detectado",
                    "que no hay productos registrados."
                );

                MessageBox.Show(mensaje, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //agregarFocus();
        }


        public void AbrirVentanaVenta()
        {
            if (abrirNuevaVenta)
            {
                abrirNuevaVenta = false;
                btnNuevaVenta.PerformClick();
            }
        }

        private void cbTipoVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var opcion = cbTipoVentas.SelectedValue.ToString();
            clickBoton = 0;

            //Ventas pagadas
            if (opcion == "VP") { CargarDatos(1); }
            //Ventas guardadas
            if (opcion == "VG") { CargarDatos(2); }
            //Ventas canceladas
            if (opcion == "VC") { CargarDatos(3); }
            //Ventas a credito
            if (opcion == "VCC") { CargarDatos(4); }
        }

        #region Manejo del evento MouseEnter para el DataGridView
        private void DGVListadoVentas_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            var ultimaFila = DGVListadoVentas.Rows.Count - 1;

            if (e.RowIndex >= 0 && e.RowIndex != ultimaFila)
            {
                var opcion = cbTipoVentas.SelectedValue.ToString();
                var permitir = true;

                Rectangle cellRect = DGVListadoVentas.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                if (e.ColumnIndex >= 11)
                {
                    var textoTT = string.Empty;
                    int coordenadaX = 0;

                    DGVListadoVentas.Cursor = Cursors.Hand;

                    if (e.ColumnIndex == 11)
                    {
                        textoTT = "Cancelar";
                        coordenadaX = 60;

                        if (opcion == "VC") { permitir = false; }
                    }

                    if (e.ColumnIndex == 12)
                    {
                        textoTT = "Ver nota";
                        coordenadaX = 70;
                    }

                    if (e.ColumnIndex == 13)
                    {
                        textoTT = "Ver ticket";
                        coordenadaX = 62;
                    }

                    if (e.ColumnIndex == 14)
                    {
                        textoTT = "Abonos";
                        coordenadaX = 54;

                        if (opcion != "VCC") { permitir = false; }
                    }

                    if (e.ColumnIndex == 15)
                    {
                        textoTT = "Timbrar";
                        coordenadaX = 56;
                    }

                    if (e.ColumnIndex == 16)
                    {
                        textoTT = "Información";
                        coordenadaX = 75;
                    }
                    if (e.ColumnIndex == 17)
                    {
                        textoTT = "Retomar esta venta";
                        coordenadaX = 120;
                    }

                    // Si es diferente a la fila donde se muestran los totales
                    if (e.RowIndex != DGVListadoVentas.Rows.Count - 1)
                    {
                        VerToolTip(textoTT, cellRect.X, coordenadaX, cellRect.Y, permitir);

                        textoTT = string.Empty;
                    }
                }
                else
                {
                    DGVListadoVentas.Cursor = Cursors.Default;
                }
            }
        }
        #endregion

        private void VerToolTip(string texto, int cellRectX, int coordX, int cellRectY, bool mostrar)
        {
            if (mostrar)
            {
                TTMensaje.Show(texto, this, DGVListadoVentas.Location.X + cellRectX - coordX, DGVListadoVentas.Location.Y + cellRectY, 1500);
            }
        }

        private void DGVListadoVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var ultimaFila = DGVListadoVentas.Rows.Count - 1;

            if (e.RowIndex >= 0 && e.RowIndex != ultimaFila)
            {
                var opcion = cbTipoVentas.SelectedValue.ToString();

                var fila = DGVListadoVentas.CurrentCell.RowIndex;

                int idVenta = Convert.ToInt32(DGVListadoVentas.Rows[fila].Cells["ID"].Value);
                folioVenta = Convert.ToInt32(DGVListadoVentas.Rows[fila].Cells["Folio"].Value);

                if (e.ColumnIndex == 0)
                {
                    var estado = Convert.ToBoolean(DGVListadoVentas.Rows[fila].Cells["col_checkbox"].Value);

                    //En esta condicion se ponen 
                    if (estado.Equals(false))//Se pone falso por que al dar click inicialmente esta en false
                    {
                        if (!idVentas.ContainsKey(idVenta))
                        {
                            idVentas.Add(idVenta, string.Empty);
                        }
                    }
                    else if (estado.Equals(true))//Se pone verdadero por que al dar click inicialmente esta en true
                    {
                        idVentas.Remove(idVenta);                    }
                }

                //Cancelar
                if (e.ColumnIndex == 11)
                {
                    var Folio = string.Empty;
                    var Serie = string.Empty;

                    using (DataTable dtDatosVentas = cn.CargarDatos(cs.DatosVentaParaLaNota(idVenta)))
                    {
                        if (!dtDatosVentas.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtDatosVentas.Rows)
                            {
                                Folio = item["Folio"].ToString();
                                Serie = item["Serie"].ToString();

                                if (Folio.Equals("0"))
                                {
                                    MessageBox.Show($"En esta operación se realizo la apertura de la Caja\nRealizada por el Usuario: {item["Usuario"].ToString()}", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }

                    var ultimaFechaCorte = mb.ObtenerFechaUltimoCorte();
                    var fechaVenta = mb.ObtenerFechaVenta(idVenta);
                    DateTime validarFechaCorte = Convert.ToDateTime(ultimaFechaCorte);
                    DateTime validarFechaVenta = Convert.ToDateTime(fechaVenta);

                    if (validarFechaVenta > validarFechaCorte)
                    {
                        var datoResultado = string.Empty;

                        if (opcion1 == 0)
                        {
                            Utilidades.MensajePermiso();
                            return;
                        }

                        var stopCancelar = false;
                        var preguntaCancelacion = string.Empty;
                        preguntaCancelacion = preguntarCancelacion();
                        var mensaje = MessageBox.Show(preguntaCancelacion, "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (mensaje == DialogResult.Yes)
                        {
                            var obtenerValorSiSeAbono = cn.CargarDatos($"SELECT * FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND IDVenta = '{idVenta}'");
                            var abonoObtenido = string.Empty;

                            var cantidadesAbono = cantidadAbonada(idVenta);

                            //var credit = revisarSiFueVentaACredito(idVenta);

                            if (!obtenerValorSiSeAbono.Rows.Count.Equals(0))
                            {
                                foreach (DataRow result in obtenerValorSiSeAbono.Rows)
                                {
                                    abonoObtenido = result["Total"].ToString();
                                }
                                var totalObtenidoAbono = float.Parse(abonoObtenido);

                                if (totalObtenidoAbono > 0)
                                {
                                    mensaje = MessageBox.Show("¿Desea devolver el dinero?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                }

                                if (mensaje == DialogResult.Yes)
                                {
                                    var formasPago = mb.ObtenerFormasPagoVenta(idVenta, FormPrincipal.userID);

                                    var t = formasPago.Sum().ToString();
                                    var total = float.Parse(t);
                                    var ventaCancelada = 0;
                                    if (cbTipoVentas.SelectedIndex != 3)
                                    {
                                        ventaCancelada = 1;
                                    }
                                    else if (cbTipoVentas.SelectedIndex == 3)
                                    {
                                        ventaCancelada = 2;
                                    }

                                    //var obtenerMontoAbonado = cn.CargarDatos($"SELECT Total FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND IDVenta = '{idVenta}'");
                                    //var obtenerTotalAbonado = string.Empty;
                                    var totalAbonado = 0f;
                                    //if (!obtenerMontoAbonado.Rows.Count.Equals(0))
                                    //{
                                    //    foreach (DataRow datosConsulta in obtenerMontoAbonado.Rows)
                                    //    {
                                    //        obtenerTotalAbonado = datosConsulta["Total"].ToString();
                                    //    }
                                    //    totalAbonado = float.Parse(obtenerTotalAbonado);
                                    //}
                                    totalAbonado = float.Parse(cantidadesAbono[0]);

                                    if (totalAbonado > 0)
                                    {
                                        var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                        var conceptoCredito = $"DEVOLUCION DINERO VENTA A CREDITO CANCELADA ID {idVenta}";

                                        var revisarSiTieneAbono = cn.CargarDatos($"SELECT sum(Total), sum(Efectivo), sum(Tarjeta), sum(Vales), sum(Cheque), sum(Transferencia), FechaOperacion FROM Abonos WHERE IDUsuario = {FormPrincipal.userID} AND IDVenta = {idVenta}");
                                        string ultimoDate = string.Empty;
                                        if (!revisarSiTieneAbono.Rows.Count.Equals(0))// valida si la consulta esta vacia 
                                        {
                                            var fechaCorteUltima = cn.CargarDatos($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");
                                            if (!fechaCorteUltima.Rows.Count.Equals(0))
                                            {
                                                var resultadoConsultaAbonos = string.Empty; var efectivoAbonadoADevolver = string.Empty; var tarjetaAbonadoADevolver = string.Empty; var valesAbonadoADevolver = string.Empty; var chequeAbonadoADevolver = string.Empty; var transAbonadoADevolver = string.Empty; var fechaOperacionAbonadoADevolver = string.Empty;
                                                foreach (DataRow fechaUltimoCorte in fechaCorteUltima.Rows)
                                                {
                                                    ultimoDate = fechaUltimoCorte["FechaOperacion"].ToString();
                                                }
                                                DateTime fechaDelCorteCaja = DateTime.Parse(ultimoDate);

                                                //var resultadoConsultaAbonos = string.Empty;
                                                //var efectivoAbonadoADevolver = string.Empty;
                                                //var tarjetaAbonadoADevolver = string.Empty;
                                                //var valesAbonadoADevolver = string.Empty;
                                                //var chequeAbonadoADevolver = string.Empty;
                                                //var transAbonadoADevolver = string.Empty;
                                                //var fechaOperacionAbonadoADevolver = string.Empty;

                                                foreach (DataRow contenido in revisarSiTieneAbono.Rows)
                                                {
                                                    resultadoConsultaAbonos = contenido["sum(Total)"].ToString();
                                                    efectivoAbonadoADevolver = contenido["sum(Efectivo)"].ToString();
                                                    tarjetaAbonadoADevolver = contenido["sum(Tarjeta)"].ToString();
                                                    valesAbonadoADevolver = contenido["sum(Vales)"].ToString();
                                                    chequeAbonadoADevolver = contenido["sum(Cheque)"].ToString();
                                                    transAbonadoADevolver = contenido["sum(Transferencia)"].ToString();
                                                    fechaOperacionAbonadoADevolver = contenido["FechaOperacion"].ToString();
                                                }
                                                DateTime fechaAbonoRealizado = DateTime.Parse(fechaOperacionAbonadoADevolver);

                                                //string[] datos = new string[] {
                                                //    "retiro", resultadoConsultaAbonos, "0", conceptoCredito, fechaDelCorteCaja.ToString("yyyy-MM-dd HH:mm:ss"), FormPrincipal.userID.ToString(),
                                                //    efectivoAbonadoADevolver, tarjetaAbonadoADevolver, valesAbonadoADevolver, chequeAbonadoADevolver, transAbonadoADevolver, formasPago[5].ToString(), "0"
                                                //};

                                                string[] datos = new string[]
                                                        {
                                                            idVenta.ToString(), FormPrincipal.userID.ToString(), resultadoConsultaAbonos, efectivoAbonadoADevolver, tarjetaAbonadoADevolver, valesAbonadoADevolver,
                                                            chequeAbonadoADevolver, transAbonadoADevolver, conceptoCredito, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                        };

                                                //cn.EjecutarConsulta(cs.OperacionCaja(datos));
                                                cn.EjecutarConsulta(cs.OperacionDevoluciones(datos));

                                                //if (fechaAbonoRealizado > fechaDelCorteCaja)//Escoger como devolver dinero
                                                //{
                                                //    if (!string.IsNullOrEmpty(abonoObtenido)) { total = float.Parse(abonoObtenido); }
                                                //    DevolverAnticipo da = new DevolverAnticipo(idVenta, total, 3, ventaCancelada);
                                                //    da.ShowDialog();

                                                //    if (DevolverAnticipo.cancel == 1)
                                                //    {
                                                //        stopCancelar = true;
                                                //    }
                                                //    else
                                                //    {
                                                stopCancelar = false;
                                                //    }
                                                //    //cbTipoVentas.SelectedIndex = 1;
                                                //    //cbTipoVentas.SelectedIndex = 3;
                                                //    CargarDatos(4);
                                                //}
                                                //else if (fechaAbonoRealizado < fechaDelCorteCaja)//Devolver dinero en efectivo
                                                //{
                                                //    saldoInicial = mb.SaldoInicialCaja(FormPrincipal.userID);
                                                //    var sEfectivo = MetodosBusquedas.efectivoInicial;
                                                //    var sTarjeta = MetodosBusquedas.tarjetaInicial;
                                                //    var sVales = MetodosBusquedas.valesInicial;
                                                //    var sCheque = MetodosBusquedas.chequeInicial;
                                                //    var sTrans = MetodosBusquedas.transInicial;

                                                //    float efe = 0f, tar = 0f, val = 0f, che = 0f, trans = 0f;

                                                //    //Comprovar que se cuente con dinero suficiente
                                                //    var obtenerDinero = cn.CargarDatos($"SELECT sum(Efectivo), sum(Tarjeta), sum(Vales), sum(Cheque), sum(Transferencia) FROM CAJA WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{fechaDelCorteCaja.ToString("yyyy-MM:dd HH:mm:ss")}'");

                                                //    var efectivoObtenido = string.Empty; var tarjetaObtenido = string.Empty; var valesObtenido = string.Empty; var chequeObtenido = string.Empty; var transObtenido = string.Empty;
                                                //    if (!obtenerDinero.Rows.Count.Equals(0))
                                                //    {
                                                //        foreach (DataRow getCash in obtenerDinero.Rows)
                                                //        {
                                                //            efectivoObtenido = getCash["sum(Efectivo)"].ToString();
                                                //            //tarjetaObtenido = getCash["sum(Tarjeta)"].ToString();
                                                //            //valesObtenido = getCash["sum(Vales)"].ToString();
                                                //            //chequeObtenido = getCash["sum(Cheque)"].ToString();
                                                //            //transObtenido = getCash["sum(Transferencia)"].ToString();
                                                //        }
                                                //        efe = (float.Parse(efectivoObtenido) + sEfectivo);
                                                //        //tar = (float.Parse(tarjetaObtenido) + sTarjeta);
                                                //        //val = (float.Parse(valesObtenido) + sVales);
                                                //        //che = (float.Parse(chequeObtenido) + sCheque);
                                                //        //trans = (float.Parse(transObtenido) +sTrans);

                                                //    }

                                                //    var totalCaja = (efe + tar + val + che + trans);

                                                //    var efectivoAbonado = float.Parse(efectivoAbonadoADevolver);
                                                //    //var tarjetaAbonado = float.Parse(tarjetaAbonadoADevolver);
                                                //    //var valesAbonado = float.Parse(valesAbonadoADevolver);
                                                //    //var chequeAbonado = float.Parse(chequeAbonadoADevolver);
                                                //    //var transAbonado = float.Parse(transAbonadoADevolver);

                                                //    if (efectivoAbonado > efe /*|| tarjetaAbonado > tar || valesAbonado > val || chequeAbonado > che || transAbonado > trans*/)
                                                //    {
                                                //        MessageBox.Show("No tiene suficiente dinero en efectivo para retirar", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                //        //cn.EjecutarConsulta(cs.ActualizarVenta(idVenta, 3, FormPrincipal.userID));
                                                //        stopCancelar = true;
                                                //    }
                                                //    else
                                                //    {
                                                //        string[] datos = new string[]
                                                //        {
                                                //            idVenta.ToString(), FormPrincipal.userID.ToString(), resultadoConsultaAbonos, efectivoAbonadoADevolver, tarjetaAbonadoADevolver, valesAbonadoADevolver,
                                                //            chequeAbonadoADevolver, transAbonadoADevolver, conceptoCredito, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                //        };

                                                //         cn.EjecutarConsulta(cs.OperacionDevoluciones(datos));

                                                //        //    string[] datos2 = new string[] {
                                                //        //"retiro", resultadoConsultaAbonos, "0", conceptoCredito, fechaOperacion, FormPrincipal.userID.ToString(),
                                                //        //    efectivoAbonadoADevolver, tarjetaAbonadoADevolver, valesAbonadoADevolver, chequeAbonadoADevolver, transAbonadoADevolver, /*credito*/"0.00", /*anticipo*/"0"
                                                //        //};
                                                //        //    cn.EjecutarConsulta(cs.OperacionCaja(datos2));
                                                //    }
                                                //}
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (formasPago.Length > 0)
                                        {
                                            var conceptoCredito = $"DEVOLUCION DINERO VENTA A CREDITO CANCELADA ID {idVenta}";

                                            var total1 = formasPago.Sum().ToString();
                                            var efectivo1 = formasPago[0].ToString();
                                            var tarjeta1 = formasPago[1].ToString();
                                            var vales1 = formasPago[2].ToString();
                                            var cheque1 = formasPago[3].ToString();
                                            var transferencia1 = formasPago[4].ToString();
                                            var credito1 = formasPago[5].ToString();
                                            //var anticipo1 = "0";

                                            var fechaOperacion1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                            string[] datos = new string[] {
                                                        "retiro", total1, "0", conceptoCredito, fechaOperacion1, FormPrincipal.userID.ToString(),
                                                        efectivo1, tarjeta1, vales1, cheque1, transferencia1, credito1/*"0.00"*/, /*anticipo*/"0"
                                                    };
                                            cn.EjecutarConsulta(cs.OperacionCaja(datos));
                                        }
                                    }
                                }
                            }
                            else if (obtenerValorSiSeAbono.Rows.Count.Equals(0))
                            {
                                if (cbTipoVentas.SelectedIndex == 0)
                                {
                                    saldoInicial = mb.SaldoInicialCaja(FormPrincipal.userID);
                                    var sEfectivo = (MetodosBusquedas.efectivoInicial + float.Parse(cantidadesAbono[1]));
                                    var sTarjeta = (MetodosBusquedas.tarjetaInicial + float.Parse(cantidadesAbono[2]));
                                    var sVales = (MetodosBusquedas.valesInicial + float.Parse(cantidadesAbono[3]));
                                    var sCheque = (MetodosBusquedas.chequeInicial + float.Parse(cantidadesAbono[4]));
                                    var sTrans = (MetodosBusquedas.transInicial + float.Parse(cantidadesAbono[5]));

                                    //Valida las cantidades para cuando sea una cuenta nueva
                                    var totalUno = string.Empty; var efectivoUno = string.Empty; var tarjetaUno = string.Empty; var valesUno = string.Empty; var chequeUno = string.Empty; var transUno = string.Empty;
                                    var validarCuentaNuevaCaja = cn.CargarDatos($"SELECT ID FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'corte'");

                                    if (validarCuentaNuevaCaja.Rows.Count.Equals(0))
                                    {
                                        var cantidandesNuevaCuenta = cn.CargarDatos($"SELECT sum(Cantidad), sum(Efectivo), sum(Tarjeta), sum(Vales), sum(Cheque), sum(Transferencia) FROM Caja WHERE IDUsuario = {FormPrincipal.userID}");

                                        totalUno = cantidandesNuevaCuenta.Rows[0]["sum(Cantidad)"].ToString();
                                        efectivoUno = cantidandesNuevaCuenta.Rows[0]["sum(Efectivo)"].ToString();
                                        tarjetaUno = cantidandesNuevaCuenta.Rows[0]["sum(Tarjeta)"].ToString();
                                        valesUno = cantidandesNuevaCuenta.Rows[0]["sum(Vales)"].ToString();
                                        chequeUno = cantidandesNuevaCuenta.Rows[0]["sum(Cheque)"].ToString();
                                        transUno = cantidandesNuevaCuenta.Rows[0]["sum(Transferencia)"].ToString();

                                        //Agregamos las cantidades que se tienen en caja a estas variables (solo en cuentas nuevas)
                                        sEfectivo = (float.Parse(efectivoUno) + float.Parse(cantidadesAbono[1]));
                                        sTarjeta = (float.Parse(tarjetaUno) + float.Parse(cantidadesAbono[2]));
                                        sVales = (float.Parse(valesUno) + float.Parse(cantidadesAbono[3]));
                                        sCheque = (float.Parse(chequeUno) + float.Parse(cantidadesAbono[4]));
                                        sTrans = (float.Parse(transUno) + float.Parse(cantidadesAbono[5]));

                                    }

                                    mensaje = MessageBox.Show("¿Desea devolver el dinero?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (mensaje == DialogResult.Yes)
                                    {
                                        float tot = 0f, efe = 0f, tar = 0f, val = 0f, che = 0f, trans = 0f;

                                        var formasPago = mb.ObtenerFormasPagoVenta(idVenta, FormPrincipal.userID);

                                        if (formasPago.Length > 0)
                                        {
                                            var conceptoCredito = $"DEVOLUCION DINERO VENTA CANCELADA ID {idVenta}";

                                            var total1 = formasPago.Sum().ToString();
                                            var efectivo1 = formasPago[0].ToString();
                                            var tarjeta1 = formasPago[1].ToString();
                                            var vales1 = formasPago[2].ToString();
                                            var cheque1 = formasPago[3].ToString();
                                            var transferencia1 = formasPago[4].ToString();
                                            var credito1 = formasPago[5].ToString();
                                            //var anticipo1 = "0";

                                            var fechaOperacion1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                            var ultimoDate = string.Empty;
                                            var obtenerFecha = cn.CargarDatos($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");
                                            if (!obtenerFecha.Rows.Count.Equals(0))
                                            {
                                                foreach (DataRow fechaUltimoCorte in obtenerFecha.Rows)
                                                {
                                                    ultimoDate = fechaUltimoCorte["FechaOperacion"].ToString();
                                                }
                                                DateTime fechaDelCorteCaja = DateTime.Parse(ultimoDate);

                                                //Se busca si se retiro dinero despues del corte
                                                var dineroRetiradoCorte = cn.CargarDatos($"SELECT sum(Cantidad), sum(Efectivo), sum(Tarjeta), sum(Vales), sum(Cheque), sum(Transferencia) FROM CAJA WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'retiro' AND FechaOperacion > '{fechaDelCorteCaja.ToString("yyyy-MM:dd HH:mm:ss")}'");
                                                var rTotal = string.Empty; var rEfectivo = string.Empty; var rTarjeta = string.Empty; var rVales = string.Empty; var rCheque = string.Empty; var rTrans = string.Empty;


                                                if (!dineroRetiradoCorte.Rows.Count.Equals(0)/* && !string.IsNullOrWhiteSpace(dineroRetiradoCorte.ToString())*/)
                                                {
                                                    foreach (DataRow getRetirado in dineroRetiradoCorte.Rows)
                                                    {
                                                        rTotal = getRetirado["sum(Cantidad)"].ToString();
                                                        rEfectivo = getRetirado["sum(Cantidad)"].ToString();
                                                        rTarjeta = getRetirado["sum(Cantidad)"].ToString();
                                                        rVales = getRetirado["sum(Cantidad)"].ToString();
                                                        rCheque = getRetirado["sum(Cantidad)"].ToString();
                                                        rTrans = getRetirado["sum(Cantidad)"].ToString();

                                                        if (string.IsNullOrEmpty(rTotal))
                                                        {
                                                            rTotal = "0";
                                                            rEfectivo = "0";
                                                            rTarjeta = "0";
                                                            rVales = "0";
                                                            rCheque = "0";
                                                            rTrans = "0";
                                                        }
                                                    }
                                                }
                                                else if (dineroRetiradoCorte.Rows.Count.Equals(0))
                                                {
                                                    rTotal = "0";
                                                    rEfectivo = "0";
                                                    rTarjeta = "0";
                                                    rVales = "0";
                                                    rCheque = "0";
                                                    rTrans = "0";
                                                }

                                                var cantidadRetirada = float.Parse(rTotal);
                                                var efeRetirado = float.Parse(rEfectivo);
                                                var tarRetirado = float.Parse(rTarjeta);
                                                var valRetirado = float.Parse(rVales);
                                                var cheRetirado = float.Parse(rCheque);
                                                var transRetirado = float.Parse(rTrans);

                                                //Comprovar que se cuente con dinero suficiente
                                                var obtenerDinero = cn.CargarDatos($"SELECT sum(Cantidad), sum(Efectivo), sum(Tarjeta), sum(Vales), sum(Cheque), sum(Transferencia)  FROM CAJA WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion != 'retiro' AND FechaOperacion > '{fechaDelCorteCaja.ToString("yyyy-MM:dd HH:mm:ss")}'");
                                                var cantidadT = string.Empty; var efectivoObtenido = string.Empty; var tarjetaObtenido = string.Empty; var valesObtenido = string.Empty; var chequeObtenido = string.Empty; var transObtenido = string.Empty;

                                                if (!obtenerDinero.Rows.Count.Equals(0) /*&& !string.IsNullOrWhiteSpace(obtenerDinero.ToString())*/)
                                                {
                                                    foreach (DataRow getCash in obtenerDinero.Rows)
                                                    {
                                                        cantidadT = getCash["sum(Cantidad)"].ToString();
                                                        efectivoObtenido = getCash["sum(Efectivo)"].ToString();
                                                        tarjetaObtenido = getCash["sum(Tarjeta)"].ToString();
                                                        valesObtenido = getCash["sum(Vales)"].ToString();
                                                        chequeObtenido = getCash["sum(Cheque)"].ToString();
                                                        transObtenido = getCash["sum(Transferencia)"].ToString();
                                                    }
                                                    tot = ((float.Parse(cantidadT) - cantidadRetirada) /*+ sEfectivo*/ + float.Parse(cantidadesAbono[0]));
                                                    efe = ((float.Parse(efectivoObtenido) - efeRetirado) /*+ sEfectivo*/ + float.Parse(cantidadesAbono[1]));
                                                    tar = ((float.Parse(tarjetaObtenido) - tarRetirado) /*+ sEfectivo*/ + float.Parse(cantidadesAbono[2]));
                                                    val = ((float.Parse(valesObtenido) - valRetirado) /*+ sEfectivo*/ + float.Parse(cantidadesAbono[3]));
                                                    che = ((float.Parse(chequeObtenido) - valRetirado) /*+ sEfectivo*/ + float.Parse(cantidadesAbono[4]));
                                                    trans = ((float.Parse(transObtenido) - transRetirado) /*+ sEfectivo*/ + float.Parse(cantidadesAbono[5]));

                                                }
                                                else if (string.IsNullOrWhiteSpace(obtenerDinero.ToString()))
                                                {
                                                    tot = 0;
                                                    efe = 0;
                                                    tar = 0;
                                                    val = 0;
                                                    che = 0;
                                                    trans = 0;
                                                }
                                            }

                                            var totalActual = float.Parse(total1);////////
                                            if (tot < totalActual)
                                            {
                                                MessageBox.Show("No tiene suficiente dinero en efectivo para retirar", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                //cn.EjecutarConsulta(cs.ActualizarVenta(idVenta, 3, FormPrincipal.userID));
                                                stopCancelar = true;
                                            }
                                            else
                                            {
                                                string[] datos = new string[] {
                                                        "retiro", total1, "0", conceptoCredito, fechaOperacion1, FormPrincipal.userID.ToString(),
                                                        efectivo1, tarjeta1, vales1, cheque1, transferencia1, credito1/*"0.00"*/, /*anticipo*/"0"
                                                    };
                                                cn.EjecutarConsulta(cs.OperacionCaja(datos));
                                                //string[] datos = new string[]
                                                //        {
                                                //            idVenta.ToString(), FormPrincipal.userID.ToString(), total1, efectivo1, tarjeta1, vales1,
                                                //            cheque1, transferencia1, conceptoCredito, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                //        };

                                                //cn.EjecutarConsulta(cs.OperacionDevoluciones(datos));
                                                stopCancelar = false;
                                            }
                                        }
                                    }
                                    //stopCancelar = false;
                                }
                            }

                            if (stopCancelar == false)
                            {
                                //Obtener Status de la venta
                                var obtenerStatusVenta = $"SELECT Status FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idVenta}'";
                                var statusObtenido = cn.CargarDatos(obtenerStatusVenta);

                                datoResultado = statusObtenido.Rows[0]["Status"].ToString();

                                // Cancelar la venta
                                int resultado = cn.EjecutarConsulta(cs.ActualizarVenta(idVenta, 3, FormPrincipal.userID));

                                if (resultado > 0)
                                {
                                    // Miri. Modificado.
                                    // Obtiene el id del combo cancelado
                                     DataTable d_prod_venta = cn.CargarDatos($"SELECT IDProducto, Cantidad FROM ProductosVenta WHERE IDVenta='{idVenta}'");
                                    //var productos = cn.ObtenerProductosVenta(idVenta);

                                    if (!d_prod_venta.Rows.Equals(0))
                                    {
                                        foreach (DataRow item in d_prod_venta.Rows)
                                        {
                                            var idprod = item["IDProducto"].ToString();
                                            var cantidad = item["Cantidad"].ToString();
                                            var consulta = cn.CargarDatos($"SELECT * FROM productosdeservicios WHERE IDServicio = {idprod}");
                                            var consultaCombo = cn.CargarDatos($"SELECT IDProducto FROM productosdeservicios WHERE IDServicio = {idprod}");

                                            if (!consulta.Rows.Count.Equals(0)) //En caso que el producto sea un combo o servicio
                                            {
                                                var cantidadCombo = consulta.Rows[0]["Cantidad"].ToString();
                                                var idProd = consulta.Rows[0]["IDProducto"].ToString();
                                                var stock = cn.CargarDatos($"SELECT StockNuevo FROM `historialstock` WHERE IDProducto = {idProd} ORDER BY ID DESC");
                                                var stockOriginal = stock.Rows[0]["StockNuevo"].ToString();
                                                var stockActual = Convert.ToDecimal(stockOriginal) + Convert.ToDecimal(Convert.ToDecimal(cantidadCombo) * Convert.ToDecimal(cantidad));
                                                var datoFolio = cn.CargarDatos($"SELECT Folio FROM ventas WHERE ID = {idVenta}");
                                                var FolioDeCancelacion = datoFolio.Rows[0]["Folio"];
                                                decimal stockAnterior = Convert.ToDecimal(stockOriginal);
                                                decimal stockNuevo = Convert.ToInt32(stockActual);
                                                var cantidadR = Convert.ToDecimal(cantidad) * Convert.ToDecimal(cantidadCombo);
                                                var fechaDeOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                var paqueteServicio = string.Empty;

                                                var tipoComboSerbicio = cn.CargarDatos($"SELECT tipoDeVenta FROM `historialstock` WHERE idComboServicio = {idprod} ");
                                                paqueteServicio = tipoComboSerbicio.Rows[0]["tipoDeVenta"].ToString();

                                                if (paqueteServicio.Equals("PQ"))
                                                {
                                                    paqueteServicio = "de combo";
                                                }
                                                else
                                                {
                                                    paqueteServicio = "de servicio";
                                                }

                                                if (!consulta.Rows.Count.Equals(1))
                                                {
                                                    foreach (DataRow products in consulta.Rows)
                                                    {
                                                        int cant = Convert.ToInt32(products[5]);
                                                        var stockActual2 = cn.CargarDatos($"SELECT StockNuevo FROM `historialstock` WHERE IDProducto = {products[3]} ORDER BY ID DESC");
                                                        var stockAnterior2 = stockActual2.Rows[0]["StockNuevo"].ToString();
                                                        var multiplicacionComboServicio = Convert.ToDecimal(cantidad) * Convert.ToDecimal(cant);
                                                        var cantidadNuevoStock = Convert.ToDecimal(stockAnterior2) + multiplicacionComboServicio;

                                                        cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad) VALUES ('{products[3]}','Venta Cancelada {paqueteServicio} folio: {FolioDeCancelacion}','{stockAnterior2}','{cantidadNuevoStock}','{fechaDeOperacion}','{FormPrincipal.userNickName}','+{multiplicacionComboServicio.ToString("N")}')");
                                                    }
                                                }
                                                else
                                                {
                                                    cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad) VALUES ('{idProd}','Venta Cancelada {paqueteServicio} folio: {FolioDeCancelacion}','{stockAnterior}','{stockNuevo}','{fechaDeOperacion}','{FormPrincipal.userNickName}','+{cantidadR.ToString("N")}')");
                                                }

                                            }
                                            else //En caso de ser un producto
                                            {
                                                var stock = cn.CargarDatos($"SELECT Stock FROM productos WHERE ID = '{idprod}'");
                                                var stockOriginal = stock.Rows[0]["Stock"].ToString();
                                                var stockActual = Convert.ToDecimal(stockOriginal) + Convert.ToDecimal(cantidad);
                                                var datoFolio = cn.CargarDatos($"SELECT Folio FROM ventas WHERE ID = {idVenta}");
                                                var FolioDeCancelacion = datoFolio.Rows[0]["Folio"];
                                                decimal stockAnterior = Convert.ToDecimal(stockOriginal);
                                                decimal stockNuevo = Convert.ToInt32(stockActual);

                                                var fechaDeOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                                cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad) VALUES ('{idprod}','Venta Cancelada folio: {FolioDeCancelacion}','{stockAnterior}','{stockNuevo}','{fechaDeOperacion}','{FormPrincipal.userNickName}','+{cantidad}')");

                                            }
                                        }
                                    } 
                                   

                                    if (d_prod_venta.Rows.Count > 0)
                                    {
                                        DataRow r_prod_venta = d_prod_venta.Rows[0];
                                        int id_prod = Convert.ToInt32(r_prod_venta["IDProducto"]);
                                        decimal cantidad_combo = Convert.ToDecimal(r_prod_venta["Cantidad"]);

                                        // Busca los productos relacionados al combo y trae la cantidad para aumentar el stock
                                        DataTable dtprod_relacionados = cn.CargarDatos(cs.productos_relacionados(id_prod));
                                        
                                        if(dtprod_relacionados.Rows.Count > 0)
                                        {
                                            foreach(DataRow drprod_relacionados in dtprod_relacionados.Rows)
                                            {
                                                decimal cantidad_prod_rel = Convert.ToDecimal(drprod_relacionados["Cantidad"]);
                                                decimal cantidad_prod_rel_canc = cantidad_combo * cantidad_prod_rel;

                                                cn.EjecutarConsulta($"UPDATE Productos SET Stock = Stock + {cantidad_prod_rel_canc} WHERE ID = {drprod_relacionados["IDProducto"]} AND IDUsuario = {FormPrincipal.userID}");
                                            }
                                        }
                                        else if (dtprod_relacionados.Rows.Count.Equals(0))
                                        {
                                            using (DataTable dtProdVenta = cn.CargarDatos(cs.ObtenerProdDeLaVenta(idVenta)))
                                            {
                                                if (!dtProdVenta.Rows.Count.Equals(0))
                                                {
                                                    foreach (DataRow drProdVenta in dtProdVenta.Rows)
                                                    {
                                                        cn.EjecutarConsulta(cs.aumentarStockVentaCancelada(Convert.ToInt32(drProdVenta["ID"].ToString()), (float)(Convert.ToDecimal(drProdVenta["Stock"].ToString()) + Convert.ToDecimal(drProdVenta["Cantidad"].ToString()))));
                                                    }
                                                }
                                            }
                                        }

                                        /* foreach (var producto in productos)
                                         {
                                             var info = producto.Split('|');
                                             var idProducto = info[0];
                                             var cantidad = Convert.ToDecimal(info[2]);

                                             cn.EjecutarConsulta($"UPDATE Productos SET Stock = Stock + {cantidad} WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID}");
                                         }*/
                                    }
                                    
                                    var formasPago2 = mb.ObtenerFormasPagoVenta(idVenta, FormPrincipal.userID);

                                    var conceptoCreditoC = $"DELOLUVION CREDITO VENTA CANCELADA ID {idVenta}";
                                    if (formasPago2.Length > 0)
                                    {
                                        var total1 = "0";
                                        var efectivo1 = "0";
                                        var tarjeta1 = "0";
                                        var vales1 = "0";
                                        var cheque1 = "0";
                                        var transferencia1 = "0";
                                        var credito1 = formasPago2[5].ToString();
                                        //var anticipo1 = "0";

                                        var fechaOperacion1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                        if (!DevolverAnticipo.ventaCanceladaCredito.Equals(true))
                                        {
                                            string[] datos = new string[] {
                                                        "retiro", total1, "0", conceptoCreditoC, fechaOperacion1, FormPrincipal.userID.ToString(),
                                                        efectivo1, tarjeta1, vales1, cheque1, transferencia1, credito1/*"0.00"*/, /*anticipo*/"0"
                                                    };
                                            cn.EjecutarConsulta(cs.OperacionCaja(datos));
                                        }
                                    }

                                    // Agregamos marca de agua al PDF del ticket de la venta cancelada
                                    Utilidades.CrearMarcaDeAgua(idVenta, "CANCELADA");

                                    // Agregamos marca de agua al PDF de la nota de venta cancelada
                                    Utilidades.CrearMarcaDeAguaNota(idVenta, "CANCELADA");

                                    //CargarDatos();
                                    restaurarBusqueda();
                                }
                            }

                            var mensajeCancelar = string.Empty;

                            mensajeCancelar = cancelarMensajeExitoso();

                            MessageBox.Show(mensajeCancelar, "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No es posible cancelar ventas, presupuestos o creditos \nanteriores al corte de caja", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //Ver nota
                if (e.ColumnIndex == 12)
                {
                    if (opcion2 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    // Comprobar si adobe esta instalado
                    if (!Utilidades.AdobeReaderInstalado())
                    {
                        Utilidades.MensajeAdobeReader();
                        return;
                    }

                    // Verifica si el PDF ya esta creado
                    var servidor = Properties.Settings.Default.Hosting;
                    var Usuario = FormPrincipal.userNickName;
                    var Folio = string.Empty;
                    var Serie = string.Empty;

                    string ruta_archivo = string.Empty;

                    using (DataTable dtDatosVentas = cn.CargarDatos(cs.DatosVentaParaLaNota(idVenta)))
                    {
                        if (!dtDatosVentas.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtDatosVentas.Rows)
                            {
                                Folio = item["Folio"].ToString();
                                Serie = item["Serie"].ToString();

                                if (Folio.Equals("0"))
                                {
                                    MessageBox.Show($"En esta operación se realizo la apertura de la Caja\nRealizada por el Usuario: {item["Usuario"].ToString()}", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        ruta_archivo = $@"\\{servidor}\Archivos PUDVE\Ventas\PDF\{Usuario}\";
                    }
                    else
                    {
                        ruta_archivo = $@"C:\Archivos PUDVE\Ventas\PDF\{Usuario}\";
                    }

                    if (!Directory.Exists(ruta_archivo))
                    {
                        Directory.CreateDirectory(ruta_archivo);
                    }

                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        ruta_archivo = $@"\\{servidor}\Archivos PUDVE\Ventas\PDF\{Usuario}\VENTA_NoVenta{idVenta}_Folio{Folio}{Serie}.pdf";
                    }
                    else
                    {
                        ruta_archivo = $@"C:\Archivos PUDVE\Ventas\PDF\{Usuario}\VENTA_NoVenta{idVenta}_Folio{Folio}{Serie}.pdf";
                    }

                    Thread hilo;
                    //pictureBox1.Visible = true;
                    if (!File.Exists(ruta_archivo) || File.Exists(ruta_archivo))
                    {// () => mnsj(),
                        //Parallel.Invoke(() => mnsj(), () => verfactura(idVenta));
                        
                        //pBar_descarga_verpdf.Visible = true;
                        //lb_texto_descarga_verpdf.Visible = true;
                        //lb_texto_descarga_verpdf.Text = "Cargando PDF. (La generación del PDF tardará 10 segundos (aproximadamente) en ser visualizado.)";
                        ///Thread hilo = new Thread(() => RealizarProcesoProductos());
                        hilo = new Thread(() => mnsj());
                        hilo.Start();
                        
                        hilo = new Thread(verfactura);
                        hilo.Start(idVenta);

                        hilo.Join();
                        //MessageBox.Show("La generación del PDF tardará 10 segundos (aproximadamente) en ser visualizado. Un momento por favor...", "", MessageBoxButtons.OK);
                        // Genera PDF
                        //ver_factura(idVenta);
                    }
                    
                    // poner marca de agua a la nota si es presupuesto
                    using (var dtVentaRealizada = cn.CargarDatos(cs.consulta_dventa(1, idVenta)))
                    {
                        if (!dtVentaRealizada.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtVentaRealizada.Rows)
                            {
                                if (item["Status"].ToString().Equals("2"))
                                {
                                    Utilidades.CrearMarcaDeAguaNotaVenta(idVenta, "PRESUPUESTO");
                                }
                            }
                        }
                    }

                    // Visualiza PDF

                    VisualizadorReportes vr = new VisualizadorReportes(ruta_archivo);
                    vr.ShowDialog();

                    //string nombre = "VENTA_" + idVenta;

                    //Visualizar_notaventa ver_nota = new Visualizar_notaventa(nombre);

                    //ver_nota.FormClosed += delegate
                    //{
                    //    ver_nota.Dispose();
                    //};

                    //ver_nota.ShowDialog();
                }
 
                //Ver ticket
                if (e.ColumnIndex == 13)
                {
                    if (opcion3 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    // Comprobar si adobe esta instalado
                    if (!Utilidades.AdobeReaderInstalado())
                    {
                        Utilidades.MensajeAdobeReader();
                        return;
                    }

                    var Folio = string.Empty;
                    var Serie = string.Empty;

                    using (DataTable dtDatosVentas = cn.CargarDatos(cs.DatosVentaParaLaNota(idVenta)))
                    {
                        if (!dtDatosVentas.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtDatosVentas.Rows)
                            {
                                Folio = item["Folio"].ToString();
                                Serie = item["Serie"].ToString();

                                if (Folio.Equals("0"))
                                {
                                    MessageBox.Show($"En esta operación se realizo la apertura de la Caja\nRealizada por el Usuario: {item["Usuario"].ToString()}", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }

                    var servidor = Properties.Settings.Default.Hosting;

                    ticketGenerado = $"ticket_venta_{idVenta}.pdf";

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
                        //MessageBox.Show($"El archivo solicitado con nombre '{ticketGenerado}' \nno se encuentra en el sistema.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        var dtImpVenta = cn.CargarDatos(cs.ReimprimirTicket(idVenta));

                        var datosConfig = mb.DatosConfiguracion();
                        bool imprimirCodigo = false;

                        if (Convert.ToInt16(datosConfig[0]) == 1)
                        {
                            imprimirCodigo = true;
                        }

                        if (dtImpVenta.Rows.Count > 0)
                        {
                            Utilidades.GenerarTicket(dtImpVenta, imprimirCodigo);

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
                        }
                    }
                }

                //Abonos
                if (e.ColumnIndex == 14)
                {
                    if (opcion4 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    var Folio = string.Empty;
                    var Serie = string.Empty;

                    using (DataTable dtDatosVentas = cn.CargarDatos(cs.DatosVentaParaLaNota(idVenta)))
                    {
                        if (!dtDatosVentas.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtDatosVentas.Rows)
                            {
                                Folio = item["Folio"].ToString();
                                Serie = item["Serie"].ToString();

                                if (Folio.Equals("0"))
                                {
                                    MessageBox.Show($"En esta operación se realizo la apertura de la Caja\nRealizada por el Usuario: {item["Usuario"].ToString()}", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }

                    //Verificamos si tiene seleccionada la opcion de ventas a credito
                    if (opcion == "VCC")
                    {
                        var total = float.Parse(DGVListadoVentas.Rows[fila].Cells["Total"].Value.ToString());

                        AsignarAbonos abono = new AsignarAbonos(idVenta, total);

                        abono.FormClosed += delegate
                        {
                            CargarDatos(4);
                        };

                        abono.ShowDialog();
                    }
                    else
                    {
                        //Comprobamos si tiene historial de abonos
                        var existenAbonos = (bool)cn.EjecutarSelect($"SELECT * FROM Abonos WHERE IDVenta = {idVenta} AND IDUsuario = {FormPrincipal.userID}");

                        if (existenAbonos)
                        {
                            ListaAbonosVenta abonos = new ListaAbonosVenta(idVenta);

                            abonos.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("No hay información adicional sobre esta venta", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                //Timbrar
                if (e.ColumnIndex == 15)
                {
                    if (opcion5 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    var Folio = string.Empty;
                    var Serie = string.Empty;

                    using (DataTable dtDatosVentas = cn.CargarDatos(cs.DatosVentaParaLaNota(idVenta)))
                    {
                        if (!dtDatosVentas.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtDatosVentas.Rows)
                            {
                                Folio = item["Folio"].ToString();
                                Serie = item["Serie"].ToString();

                                if (Folio.Equals("0"))
                                {
                                    MessageBox.Show($"En esta operación se realizo la apertura de la Caja\nRealizada por el Usuario: {item["Usuario"].ToString()}", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }

                    // Si la nota ya ha sido cancelada entonces no será facturada
                    if (opcion == "VC")
                    {
                        //MessageBox.Show("Las notas canceladas ya no pueden ser facturadas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    } 

                    // Se valida que la nota no tenga ya una factura creada
                    int r = Convert.ToInt32(cn.EjecutarSelect($"SELECT Timbrada FROM Ventas WHERE ID={idVenta}", 8));

                    if (r == 1)
                    {
                        var resp = MessageBox.Show("La nota de venta ya tiene una factura creada. La generación de más de una factura para la misma nota queda a responsabilidad de usted. \n\n ¿Desea continuar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (resp == DialogResult.Yes)
                        {
                            // Verifica que la venta tenga todos los datos para facturar
                            comprobar_venta_f(idVenta);
                        }
                    }
                    else
                    {
                        // Verifica que la venta tenga todos los datos para facturar
                        comprobar_venta_f(idVenta);
                    }
                }

                // Información
                if (e.ColumnIndex == 16)
                {
                    Ventas_ventana_informacion info = new Ventas_ventana_informacion(idVenta);
                    info.ShowDialog();
                }

                // Retomar Venta
                if (e.ColumnIndex == 17)
                {
                    retomarVentasCanceladas = 1;

                    if (retomarVentasCanceladas == 1 && opcion == "VC")
                    {
                        if (Application.OpenForms.OfType<Ventas>().Count() == 1)
                        {
                            Application.OpenForms.OfType<Ventas>().FirstOrDefault().Close();
                        }

                        obtenerIdVenta = idVenta; // numeroDeFolio
                        btnNuevaVenta.PerformClick();
                    }
                    else if (retomarVentasCanceladas == 1 && opcion == "VG")
                    {
                        MessageBox.Show("Para retomar la venta debe ir a la ventana \"Nueva Venta (F5)\" \nen el botón \"Ventas Guardadas (ctrol + M)\"", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    retomarVentasCanceladas = 0;
                }
   
                DGVListadoVentas.ClearSelection();
            }
        }

        private string preguntarCancelacion()
        {
            var preguntaDesdeDonde = string.Empty;

            var opcion = cbTipoVentas.SelectedValue.ToString();

            if (opcion == "VP") //Ventas pagadas
            {
                preguntaDesdeDonde = "¿Estás seguro de cancelar la venta?";
            }
            else if (opcion == "VG") //Ventas guardadas
            {
                preguntaDesdeDonde = "¿Estás seguro de cancelar el presupuesto?";
            }
            else if (opcion == "VC") //Ventas canceladas
            {
                preguntaDesdeDonde = "¿Estás seguro de cancelar la venta cancelada?";
            }
            else if (opcion == "VCC") //Ventas a credito
            {
                preguntaDesdeDonde = "¿Estás seguro de cancelar el crédito?";
            }

            return preguntaDesdeDonde;
        }

        private string cancelarMensajeExitoso()
        {
            var cancelarDesdeDonde = string.Empty;

            var opcion = cbTipoVentas.SelectedValue.ToString();

            if (opcion == "VP") //Ventas pagadas
            {
                cancelarDesdeDonde = "Venta (Pagada) cancelada exitosamente";
            }
            else if (opcion == "VG") //Ventas guardadas
            {
                cancelarDesdeDonde = "Presupuesto (Venta Guardada) cancelado exitosamente";
            }
            else if (opcion == "VC") //Ventas canceladas
            {
                cancelarDesdeDonde = "Venta (Cancelada) cancelada exitosamente";
            }
            else if (opcion == "VCC") //Ventas a credito
            {
                cancelarDesdeDonde = "Venta a credito cancelada exitosamente";
            }

            return cancelarDesdeDonde;
        }

        public void mnsj()
        {
            MessageBox.Show("La generación del PDF tardará 10 segundos (aproximadamente) en ser visualizado. Un momento por favor...", "", MessageBoxButtons.OK);

            //pictureBox1.Visible = true;
            //Console.WriteLine("uno");
            //pBar_descarga_verpdf.Visible = true;
            //lb_texto_descarga_verpdf.Visible = true;
            //lb_texto_descarga_verpdf.Text = "Cargando PDF. (La generación del PDF tardará 10 segundos (aproximadamente) en ser visualizado.)";

            ///          this.Invoke(barra, new object[] { 1 });
            //            Thread.Sleep(10);


            // (ban_ver == false)
            //{
            //pBar_descarga_verpdf.Maximum = 100;
            //pBar_descarga_verpdf.Value = 2; // Valor inicial
            /*
                        for (var p = 0; p <= 100; p++)
             {
                            //pBar_descarga_verpdf.Value = p;
                            pBar_descarga_verpdf.Increment(1);
                            lb_txt_ruta_descargar.Text = p.ToString();

                            if (ban_ver == true)
                            {
                                pBar_descarga_verpdf.Visible = false;
                                lb_texto_descarga_verpdf.Visible = false;
                            }*/

            ///Console.WriteLine("progreso="+pBar_descarga_verpdf.Value);
            //refrescar(p);
            /*delegado barra = new delegado(refrescar);
            this.Invoke(barra, new object[] { p });
        Thread.Sleep(10);*/

            //}
            //}
        }

        private void verfactura(object id)
        {
            int id_vent = Convert.ToInt32(id);
            ver_factura(id_vent);
        }

        private void TTMensaje_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void ListadoVentas_Paint(object sender, PaintEventArgs e)
        {
            if (recargarDatos)
            {
                fechaUltimoCorte = Convert.ToDateTime(mb.UltimaFechaCorte());

                var tipoDeBusqueda = 0;

                tipoDeBusqueda = verTipoDeBusqueda();

                CargarDatos(tipoDeBusqueda);

                existenProductos = mb.TieneProductos();

                recargarDatos = false;

                //+++btnUltimaPagina.PerformClick();
                btnPrimeraPagina.PerformClick();

                hay_productos_habilitados = mb.tiene_productos_habilitados();
                cbTipoVentas.SelectedIndex = 0;
            }

            if (abrirNuevaVenta)
            {
                abrirNuevaVenta = false;

                if (opcion8 == 1)
                {
                    btnNuevaVenta.PerformClick();
                }
            }

            FormPrincipal mainForm = Application.OpenForms.OfType<FormPrincipal>().FirstOrDefault();

            if (mainForm != null)
            {
                if (mainForm.WindowState == FormWindowState.Minimized)
                {
                    mainForm.WindowState = FormWindowState.Maximized;
                    mainForm.SendToBack();
                }
                //if (mainForm.WindowState == FormWindowState.Maximized)
                //{
                //    mainForm.WindowState = FormWindowState.Maximized;
                //    mainForm.SendToBack();
                //}
                //if (mainForm.Visible == false)
                //{
                //    mainForm.Visible = true;
                //    mainForm.WindowState = FormWindowState.Minimized;//Aqui es donde me muestra la nueva ventana
                //    mainForm.SendToBack();
                //}
            }
        }

        //Se agrego para que no se abra la ventana nueva venta al cambiar el tamaño del form
        private void ListadoVentas_Resize(object sender, EventArgs e)
        {
            recargarDatos = false;
            abrirNuevaVenta = false;
        }


        private void comprobar_venta_f(int id_venta)
        {
            obtenerIDVentaTimbrado = id_venta;

            DataTable d_id_productos;
            DataTable d_claves;

            string ruta_carpeta_csd = @"C:\Archivos PUDVE\MisDatos\CSD\";
            var servidor = Properties.Settings.Default.Hosting;
            string arch_cer = "";
            string arch_key = "";
            string numero_cer = "";
            string cp = "";
            bool cambia_nombre_carpeta = false;

            //int sin_cliente = 0;
            //int n_filas = 0;
            int i = 1;

            
            
            // Obtiene el número de usuarios. 

            DataTable dt_usuarios = cn.CargarDatos("SELECT ID, Usuario FROM usuarios");
            int tam_usuarios = dt_usuarios.Rows.Count;

            if (tam_usuarios > 1)
            {
                DataRow dr_usuarios = dt_usuarios.Rows[0];

                if (dr_usuarios["Usuario"].ToString() == FormPrincipal.userNickName)
                {
                    // Verifica si existe la carpeta CSD.
                    // Si la carpeta CSD no existe, entonces se deberá modificar la ruta de acceso a los archivos CSD.

                    if (!Directory.Exists(ruta_carpeta_csd))
                    {
                        cambia_nombre_carpeta = true;
                        ruta_carpeta_csd = @"C:\Archivos PUDVE\MisDatos\CSD_" + FormPrincipal.userNickName + @"\";
                    }
                }
                else
                {
                    cambia_nombre_carpeta = true;
                    ruta_carpeta_csd = @"C:\Archivos PUDVE\MisDatos\CSD_" + FormPrincipal.userNickName + @"\";
                }
            }

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                ruta_carpeta_csd = $@"\\{servidor}\Archivos PUDVE\MisDatos\CSD\";

                if(cambia_nombre_carpeta == true)
                {
                    ruta_carpeta_csd = $@"\\{servidor}\Archivos PUDVE\MisDatos\CSD_" + FormPrincipal.userNickName + @"\";
                }
            }

            // Consulta IDCliente
            int id_cliente = Convert.ToInt32(cn.EjecutarSelect($"SELECT IDCliente FROM DetallesVenta WHERE IDVenta='{id_venta}'", 6));

            /*if(id_cliente == 0)
            {
                sin_cliente = 1;
            }*/

            // Consulta claves de producto y unidad del producto
            d_id_productos = cn.CargarDatos(cs.cargar_datos_venta_xml(4, id_venta, 0));

            // Declara arreglo y tamaño
            int n_filas = d_id_productos.Rows.Count + 1;
            faltantes_productos = new string[n_filas][];


            if (d_id_productos.Rows.Count > 0)
            {
                foreach (DataRow r_id_productos in d_id_productos.Rows)
                {
                    int id_p = 0;

                    id_p = Convert.ToInt32(r_id_productos["IDProducto"]);


                    // Busca claves
                    d_claves = cn.CargarDatos(cs.cargar_datos_venta_xml(5, id_p, 0));

                    if (d_claves.Rows.Count > 0)
                    {
                        foreach (DataRow r_claves in d_claves.Rows)
                        {
                            string clave_u = r_claves["UnidadMedida"].ToString();
                            string clave_p = r_claves["ClaveProducto"].ToString();

                            faltantes_productos[i] = new string[11];

                            if (clave_p == "" | clave_u == "")
                            {
                                faltantes_productos[i][0] = "1";
                            }
                            else
                            {
                                faltantes_productos[i][0] = "0";
                            }

                            faltantes_productos[i][1] = id_p.ToString();
                            faltantes_productos[i][2] = clave_p;
                            faltantes_productos[i][3] = clave_u;
                            faltantes_productos[i][4] = r_id_productos["Nombre"].ToString();
                            faltantes_productos[i][5] = r_id_productos["Cantidad"].ToString();
                            faltantes_productos[i][6] = r_id_productos["Precio"].ToString();
                            faltantes_productos[i][7] = r_id_productos["descuento"].ToString();
                            faltantes_productos[i][8] = r_id_productos["TipoDescuento"].ToString();
                            faltantes_productos[i][9] = r_claves["Base"].ToString();
                            faltantes_productos[i][10] = r_claves["Impuesto"].ToString();
                        }

                        i++;
                    }
                }
            }

            int tiene_timbres = mb.obtener_cantidad_timbres();

            if (tiene_timbres <= 0)
            {
                MessageBox.Show("No cuenta con timbres para timbrar su factura. Le sugerimos realizar una compra de timbres.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Abrir ventana para agregar los datos faltantes para la factura

            // Antes de abrir ventana se verificará que tenga los archivos digitales agregados.

            if (Directory.Exists(ruta_carpeta_csd))
            {
                DirectoryInfo dir = new DirectoryInfo(ruta_carpeta_csd);

                foreach (var arch in dir.GetFiles())
                {
                    // Obtiene extención del archivo
                    string extencion = arch.Name.Substring(arch.Name.Length - 4, 4);

                    if (extencion == ".cer")
                    {
                        arch_cer = ruta_carpeta_csd + arch.Name;
                    }
                    if (extencion == ".key")
                    {
                        arch_key = ruta_carpeta_csd + arch.Name;
                    }
                }
            }

            if (arch_cer != "" & arch_key != "")
            {
                // Consulta que se halla guardado el número de certificado
                DataTable dcer = cn.CargarDatos(cs.cargar_info_certificado(FormPrincipal.userID.ToString()));

                if (dcer.Rows.Count > 0)
                {
                    DataRow r_dcer = dcer.Rows[0];
                    numero_cer = r_dcer["num_certificado"].ToString();
                    cp = r_dcer["CodigoPostal"].ToString();
                }

                if (numero_cer != "" & cp != "")
                {
                    Crear_factura crear_factura = new Crear_factura(id_cliente, n_filas, id_venta);

                    crear_factura.ShowDialog();
                }
                else
                {
                    if (cp == "")
                    {
                        MessageBox.Show("No ha ingresado su código postal, por favor vaya al apartado Mis Datos para agregarlo.  \n\n Esta acción es necesaria para poder facturar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Se encontro un inconveniente derivado de sus archivos digitales CSD. Por favor vaya al apartado Mis Datos, elimine sus archivos actuales y vuelva a subirlos.  \n\n Esta acción es necesaria para poder facturar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe subir sus archivos digitales (CSD) para poder timbrar sus facturas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            p.primerPagina();
            clickBoton = 1;
            //var tipoDeBusqueda = 0;
            //tipoDeBusqueda = verTipoDeBusqueda();
            CargarDatos();
            actualizar();
            clickBoton = 0;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            //var tipoDeBusqueda = 0;
            //tipoDeBusqueda = verTipoDeBusqueda();
            CargarDatos();
            actualizar();
            clickBoton = 0;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            //var tipoDeBusqueda = 0;
            //tipoDeBusqueda = verTipoDeBusqueda();
            CargarDatos();
            actualizar();
            clickBoton = 0;
        }

        private void btnUltimaPagina_Click(object sender, EventArgs e)
        {
            p.ultimaPagina();
            clickBoton = 1;
            //var tipoDeBusqueda = 0;
            //tipoDeBusqueda = verTipoDeBusqueda();
            CargarDatos();
            actualizar();
            clickBoton = 0;
        }

        private void linkLblPaginaAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            //var tipoDeBusqueda = 0;
            //tipoDeBusqueda = verTipoDeBusqueda();
            CargarDatos();
            actualizar();
            clickBoton = 0;
        }

        private void linkLblPaginaActual_Click(object sender, EventArgs e)
        {
            actualizar();
        }

        private void linkLblPaginaSiguiente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            //var tipoDeBusqueda = 0;
            //tipoDeBusqueda = verTipoDeBusqueda();
            CargarDatos();
            actualizar();
            clickBoton = 0;
        }

        public void ver_factura(int id_venta)
        {
            decimal suma_importe_concep = 0;
            decimal suma_descuento = 0;
            List<string> list_porprod_impuestos_trasladados = new List<string>();


            // ..................................
            // .    Obtiene datos de la venta   .
            // ..................................

            // Consulta tabla venta

            DataTable d_venta = cn.CargarDatos(cs.consulta_dventa(1, id_venta));
            DataRow r_venta = d_venta.Rows[0];

            int id_usuario = Convert.ToInt32(r_venta["IDUsuario"]);
            string folio = r_venta["Folio"].ToString();
            string serie = r_venta["Serie"].ToString();
            DateTime fecha = Convert.ToDateTime(r_venta["FechaOperacion"]);
            decimal anticipo = Convert.ToDecimal(r_venta["Anticipo"]);


            /*string tipo_iva = "";

            if (Convert.ToDecimal(r_venta["IVA16"]) > 0) {  tipo_iva = "IVA16";  }
            if (Convert.ToDecimal(r_venta["IVA8"]) > 0) {  tipo_iva = "IVA8";  }*/

            // Consulta tabla DetallesVenta

            DataTable d_detallesventa = cn.CargarDatos(cs.consulta_dventa(2, id_venta));


            int id_cliente = 0;
            string forma_pago = "";

            if (r_venta["Status"].ToString().Equals("2"))
            {
                forma_pago += "Presupuesto";
            }

            if (d_detallesventa.Rows.Count > 0)
            {
                DataRow r_detallesventa = d_detallesventa.Rows[0];

                id_cliente = Convert.ToInt32(r_detallesventa["IDCliente"]);

                if (Convert.ToDecimal(r_detallesventa["Efectivo"]) > 0)
                {
                    forma_pago += "Efectivo";
                }
                if (Convert.ToDecimal(r_detallesventa["Tarjeta"]) > 0)
                {
                    if (forma_pago != "") { forma_pago += "/"; }
                    forma_pago += "Tarjeta";
                }
                if (Convert.ToDecimal(r_detallesventa["Vales"]) > 0)
                {
                    if (forma_pago != "") { forma_pago += "/"; }
                    forma_pago += "Vales";
                }
                if (Convert.ToDecimal(r_detallesventa["Cheque"]) > 0)
                {
                    if (forma_pago != "") { forma_pago += "/"; }
                    forma_pago += "Cheque";
                }
                if (Convert.ToDecimal(r_detallesventa["Transferencia"]) > 0)
                {
                    if (forma_pago != "") { forma_pago += "/"; }
                    forma_pago += "Transferencia";
                }
                if (Convert.ToDecimal(r_detallesventa["Credito"]) > 0)
                {
                    if (forma_pago != "") { forma_pago += "/"; }
                    forma_pago += "Crédito";
                }
            }
            else
            {
                id_cliente = Convert.ToInt32(cn.EjecutarSelect($"SELECT IDCliente FROM Ventas WHERE ID='{id_venta}'", 6));
            }





            ComprobanteVenta comprobanteventa = new ComprobanteVenta();


            // Datos del usuario

            DataTable d_usuario = cn.CargarDatos(cs.cargar_datos_venta_xml(2, 0, id_usuario));
            DataRow r_usuario = d_usuario.Rows[0];

            string lugar_expedicion = r_usuario["Estado"].ToString();

            ComprobanteEmisorVenta emisor_v = new ComprobanteEmisorVenta();

            emisor_v.Nombre = r_usuario["RazonSocial"].ToString();
            emisor_v.Rfc = r_usuario["RFC"].ToString();
            emisor_v.RegimenFiscal = r_usuario["Regimen"].ToString();
            emisor_v.Correo = r_usuario["Email"].ToString();
            emisor_v.Telefono = r_usuario["Telefono"].ToString();

            // Obtiene el nombre comercial del emisor
            if (r_usuario["nombre_comercial"].ToString() != "" & r_usuario["nombre_comercial"].ToString() != null)
            {
                emisor_v.NombreComercialEmisor = r_usuario["nombre_comercial"].ToString();
            }

            string domicilio_emisor = "";

            if (r_usuario["Calle"].ToString() != "")
            {
                domicilio_emisor = r_usuario["Calle"].ToString();
            }
            if (r_usuario["NoExterior"].ToString() != "")
            {
                if (domicilio_emisor != "") { domicilio_emisor += ", "; }

                domicilio_emisor += r_usuario["NoExterior"].ToString();
            }
            if (r_usuario["NoInterior"].ToString() != "")
            {
                if (domicilio_emisor != "") { domicilio_emisor += ", int. "; }

                domicilio_emisor += r_usuario["NoInterior"].ToString();
            }
            if (r_usuario["Colonia"].ToString() != "")
            {
                if (domicilio_emisor != "") { domicilio_emisor += ", Col. "; }

                domicilio_emisor += r_usuario["Colonia"].ToString();
            }
            if (r_usuario["CodigoPostal"].ToString() != "")
            {
                if (domicilio_emisor != "") { domicilio_emisor += ", CP. "; }

                domicilio_emisor += r_usuario["CodigoPostal"].ToString();
            }
            if (r_usuario["Municipio"].ToString() != "")
            {
                if (domicilio_emisor != "") { domicilio_emisor += ", "; }

                domicilio_emisor += r_usuario["Municipio"].ToString();
            }
            if (r_usuario["Estado"].ToString() != "")
            {
                if (domicilio_emisor != "") { domicilio_emisor += ", "; }

                domicilio_emisor += r_usuario["Estado"].ToString();
            }

            if (domicilio_emisor != "")
            {
                emisor_v.DomicilioEmisor = domicilio_emisor;
            }

            comprobanteventa.Emisor = emisor_v;


            // Datos del cliente

            DataTable d_cliente = cn.CargarDatos(cs.cargar_datos_venta_xml(3, id_cliente, 0));

            if (d_cliente.Rows.Count > 0)
            {
                DataRow r_cliente = d_cliente.Rows[0];

                ComprobanteReceptorVenta receptor_v = new ComprobanteReceptorVenta();

                receptor_v.Nombre = r_cliente["RazonSocial"].ToString();
                receptor_v.Rfc = r_cliente["RFC"].ToString();
                receptor_v.Correo = r_cliente["Email"].ToString();
                receptor_v.Telefono = r_cliente["Telefono"].ToString();

                string domicilio_receptor = "";

                if (r_cliente["Calle"].ToString() != "")
                {
                    domicilio_receptor = r_cliente["Calle"].ToString();
                }
                if (r_cliente["NoExterior"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", "; }

                    domicilio_receptor += r_cliente["NoExterior"].ToString();
                }
                if (r_cliente["NoInterior"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", int. "; }

                    domicilio_receptor += r_cliente["NoInterior"].ToString();
                }
                if (r_cliente["Colonia"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", Col. "; }

                    domicilio_receptor += r_cliente["Colonia"].ToString();
                }
                if (r_cliente["CodigoPostal"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", CP. "; }

                    domicilio_receptor += r_cliente["CodigoPostal"].ToString();
                }
                if (r_cliente["Localidad"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", "; }

                    domicilio_receptor += r_cliente["Localidad"].ToString();
                }
                if (r_cliente["Municipio"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", "; }

                    domicilio_receptor += r_cliente["Municipio"].ToString();
                }
                if (r_cliente["Estado"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", "; }

                    domicilio_receptor += r_cliente["Estado"].ToString();
                }
                if (r_cliente["Pais"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", "; }

                    domicilio_receptor += r_cliente["Pais"].ToString();
                }

                if (domicilio_receptor != "")
                {
                    receptor_v.DomicilioReceptor = domicilio_receptor;
                }

                comprobanteventa.Receptor = receptor_v;
            }



            // Datos del producto

            List<ComprobanteConceptoVenta> listaconcepto_v = new List<ComprobanteConceptoVenta>();

            DataTable d_prodventa = cn.CargarDatos(cs.cargar_datos_venta_xml(4, id_venta, 0));

            if (d_prodventa.Rows.Count > 0)
            {
                foreach (DataRow r_prodventa in d_prodventa.Rows)
                {
                    ComprobanteConceptoVenta concepto_v = new ComprobanteConceptoVenta();

                    r_prodventa["Cantidad"] = Utilidades.RemoverCeroStock(r_prodventa["Cantidad"].ToString());

                    concepto_v.Cantidad = Convert.ToDecimal(r_prodventa["Cantidad"]);
                    concepto_v.Descripcion = r_prodventa["Nombre"].ToString();
                    concepto_v.ValorUnitario = Convert.ToDecimal(r_prodventa["Precio"]);

                    decimal importe_v = Convert.ToDecimal(r_prodventa["Cantidad"]) * Convert.ToDecimal(r_prodventa["Precio"]);

                    concepto_v.Importe = importe_v;

                    suma_importe_concep += importe_v;

                    // Descuento
                    if (r_prodventa["descuento"].ToString() != "")
                    {
                        var tdesc = (r_prodventa["descuento"].ToString()).IndexOf("-");

                        if (tdesc > -1)
                        {
                            string d = r_prodventa["descuento"].ToString();
                            int tam = r_prodventa["descuento"].ToString().Length;

                            string cdesc = d.Substring(0, tdesc);
                            string porc_desc = d.Substring((tdesc + 2), (tam - (tdesc + 2)));


                            concepto_v.Descuento = Convert.ToDecimal(cdesc);
                            concepto_v.PorcentajeDescuento = porc_desc;
                            suma_descuento += Convert.ToDecimal(cdesc);
                        }
                        else
                        {
                            concepto_v.Descuento = Convert.ToDecimal(r_prodventa["descuento"]);
                            suma_descuento += Convert.ToDecimal(r_prodventa["descuento"]);
                        }
                    }


                    listaconcepto_v.Add(concepto_v);
                }

                comprobanteventa.Conceptos = listaconcepto_v.ToArray();
            }


            // Datos generales de la venta 

            decimal total_general = suma_importe_concep - suma_descuento; //+ suma_importe_impuest;

            if (total_general >= anticipo)
            {
                total_general = total_general - anticipo;
            }
            else
            {
                if (total_general < anticipo)
                {
                    anticipo = total_general;
                    total_general = total_general - anticipo;
                }
            }


            comprobanteventa.Serie = serie;
            comprobanteventa.Folio = folio;
            comprobanteventa.Fecha = fecha.ToString("yyyy-MM-dd HH:mm:ss");
            comprobanteventa.FormaPago = forma_pago;
            comprobanteventa.SubTotal = suma_importe_concep;
            comprobanteventa.Descuento = suma_descuento;
            comprobanteventa.Total = total_general;
            comprobanteventa.LugarExpedicion = lugar_expedicion;
            comprobanteventa.Anticipo = anticipo;



            // .....................................................................
            // .    Inicia con la generación de la plantilla y conversión a PDF    .
            // .....................................................................


            var servidor = Properties.Settings.Default.Hosting;
            string carpeta_venta = string.Empty;
            // Nombre que tendrá el pdf de la venta
            string nombre_venta = string.Empty;
            var Usuario = FormPrincipal.userNickName;
            // Verifica si tiene creado el directorio
            //string carpeta_venta = @"C:\Archivos PUDVE\Ventas\PDF\";
            var Folio = string.Empty;
            var Serie = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                carpeta_venta = $@"\\{servidor}\Archivos PUDVE\Ventas\PDF\{Usuario}\";
            }
            else
            {
                carpeta_venta = $@"C:\Archivos PUDVE\Ventas\PDF\{Usuario}\";
            }

            if (!Directory.Exists(carpeta_venta))
            {
                Directory.CreateDirectory(carpeta_venta);
            }

            using (DataTable dtDatosVentas = cn.CargarDatos(cs.DatosVentaParaLaNota(id_venta)))
            {
                if (!dtDatosVentas.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in dtDatosVentas.Rows)
                    {
                        Folio = item["Folio"].ToString();
                        Serie = item["Serie"].ToString();
                    }
                }
            }

            nombre_venta += $"VENTA_NoVenta{id_venta}_Folio{Folio}{Serie}";

            string origen_pdf_temp = nombre_venta + ".pdf";
            string destino_pdf = carpeta_venta + nombre_venta + ".pdf";

            string ruta = AppDomain.CurrentDomain.BaseDirectory + "/";
            // Creación de un arhivo html temporal
            string ruta_html_temp = ruta + "ventahtml.html";
            // Plantilla que contiene el acomodo del PDF
            string ruta_plantilla_html = ruta + "Plantilla_notaventa.html";
            string s_html = GetStringOfFile(ruta_plantilla_html);
            string result_html = "";


            result_html = RazorEngine.Razor.Parse(s_html, comprobanteventa);

            // Se crea archivo temporal
            //File.WriteAllText(ruta_html_temp, result_html);



            // Configuracion de footer y header
            var _footerSettings = new FooterSettings
            {
                ContentSpacing = 10,
                FontSize = 10,
                RightText = "[page] / [topage]"
            };
            var _headerSettings = new HeaderSettings
            {
                ContentSpacing = 8,
                FontSize = 9,
                FontName = "Lucida Sans",
                LeftText = "Folio " + comprobanteventa.Folio + " Serie " + comprobanteventa.Serie
            };


            var document = new HtmlToPdfDocument
            {
                GlobalSettings =
                {
                    ProduceOutline = true,
                    PaperSize = PaperKind.Letter,
                    Margins =
                    {
                        Top = 2.3,
                        Right = 1.5,
                        Bottom = 2.3,
                        Left = 1.5,
                        Unit = Unit.Centimeters,
                    }
                },
                Objects = {
                    new ObjectSettings
                    {
                        HtmlText = result_html,
                        HeaderSettings =_headerSettings,
                        FooterSettings = _footerSettings
                    }
                }
            };


            // Convertir el documento
            byte[] result = converter.Convert(document);

            ByteArrayToFile(result, destino_pdf);

            ban_ver = true;


            // .    CODIGO DE LA LIBRERIA WKHTMLTOPDF   .
            // ..........................................

            // Ruta de archivo conversor
            /*string ruta_wkhtml_topdf = Properties.Settings.Default.rutaDirectorio + @"\wkhtmltopdf\bin\wkhtmltopdf.exe";

            ProcessStartInfo proc_start_info = new ProcessStartInfo();
            proc_start_info.UseShellExecute = false;
            proc_start_info.FileName = ruta_wkhtml_topdf;
            proc_start_info.Arguments = "ventahtml.html " + origen_pdf_temp;

            using (Process process = Process.Start(proc_start_info))
            {
                process.WaitForExit();
            }

            // Copiar el PDF a otra carpeta

            if (File.Exists(origen_pdf_temp))
            {
                File.Copy(origen_pdf_temp, destino_pdf);
            }*/

            // Eliminar archivo temporal
            //File.Delete(ruta_html_temp);
            // Elimina el PDF creado
            //File.Delete(origen_pdf_temp);

        }

        public static IConverter converter =
                new ThreadSafeConverter(
                    new RemotingToolset<PdfToolset>(
                        new Win32EmbeddedDeployment(
                            new TempFolderDeployment()
                        )
                    )
                );


        public static bool ByteArrayToFile(byte[] _ByteArray, string _FileName)
        {
            try
            {
                // Abre el archivo
                FileStream _FileStream = new FileStream(_FileName, FileMode.Create, FileAccess.Write);
                // Escribe un bloque de bytes para este stream usando datos de una matriz de bytes
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            return false;
        }

        private static string GetStringOfFile(string ruta_arch)
        {
            string cont = File.ReadAllText(ruta_arch);

            return cont;
        }

        private void ag_checkb_header()
        {
            header_checkb = new CheckBox();
            header_checkb.Name = "cbox_seleccionar_todo";
            header_checkb.Size = new Size(15, 15);
            header_checkb.Location = new Point(12, 6);
            header_checkb.CheckedChanged += new EventHandler(des_activa_t_checkbox);
            DGVListadoVentas.Controls.Add(header_checkb);
        }

        private void des_activa_t_checkbox(object sender, EventArgs e)
        {
            int c = 0;
            int t = DGVListadoVentas.Rows.Count - 2;

            CheckBox headerBox = ((CheckBox)DGVListadoVentas.Controls.Find("cbox_seleccionar_todo", true)[0]);

            foreach (DataGridViewRow row in DGVListadoVentas.Rows)
            {
                if (c < t)
                {
                    row.Cells["col_checkbox"].Value = headerBox.Checked;
                }

                c++;
            }
        }

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            if (opcion6 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            int cont = 0;
            int en = 0;
            int c = 0;
            int t = DGVListadoVentas.Rows.Count - 2;
            string mnsj_error = "";
            string[][] arr_id_env;


            foreach (DataGridViewRow row in DGVListadoVentas.Rows)
            {
                if (c < t)
                {
                    bool estado = (bool)row.Cells["col_checkbox"].Value;

                    if (estado == true)
                    {
                        cont++;
                    }
                    else
                    {
                        mnsj_error = "No ha seleccionado alguna nota de venta para enviar.";
                    }

                    c++;
                }
            }


            // Obtener el id de la factura a enviar

            if (cont > 0)
            {
                arr_id_env = new string[cont][];
                c = 0;

                foreach (DataGridViewRow row in DGVListadoVentas.Rows)
                {
                    if (c < t)
                    {
                        bool estado = (bool)row.Cells["col_checkbox"].Value;

                        if (estado == true)
                        {
                            arr_id_env[en] = new string[2];

                            arr_id_env[en][0] = Convert.ToString(row.Cells["ID"].Value);
                            arr_id_env[en][1] = "";
                            en++;
                        }
                        c++;
                    }
                }

                // Formulario envío de correo

                Enviar_correo correo = new Enviar_correo(arr_id_env, "nota de venta", 4);
                correo.ShowDialog();
            }
            else
            {
                MessageBox.Show(mnsj_error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            agregarFocus();
        }

        private void clickcellc_checkbox(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DGVListadoVentas.Columns[0].Index)
            {
                DataGridViewCheckBoxCell celda = (DataGridViewCheckBoxCell)this.DGVListadoVentas.Rows[e.RowIndex].Cells[0];

                if (Convert.ToBoolean(celda.Value) == false)
                {
                    celda.Value = true;
                    DGVListadoVentas.Rows[e.RowIndex].Selected = true;
                }
                else
                {
                    celda.Value = false;
                    DGVListadoVentas.Rows[e.RowIndex].Selected = false;
                }
            }
        }


        private void BuscarTieneFoco(object sender, EventArgs e)
        {
            if (txtBuscador.Text == "BUSCAR POR RFC, CLIENTE, EMPLEADO O FOLIO...")
            {
                txtBuscador.Text = "";
            }
        }

        private void BuscarPierdeFoco(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscador.Text))
            {
                txtBuscador.Text = "BUSCAR POR RFC, CLIENTE, EMPLEADO O FOLIO...";
            }
        }

        private void txtBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (opcion7 == 1)
                {
                    btnBuscarVentas.PerformClick();
                }
            }

            if (e.KeyCode == Keys.F5)
            {
                btnNuevaVenta.PerformClick();
            }
        }

        private void btn_descargar_Click(object sender, EventArgs e)
        {
            int cont = 0;
            int c = 0;
            int d = 0;
            int t = DGVListadoVentas.Rows.Count - 2;
            string mnsj_error = "";
            int[] arr_id_desc;
            string opc_tipo_nota = cbTipoVentas.SelectedValue.ToString();



            foreach (DataGridViewRow row in DGVListadoVentas.Rows)
            {
                if (c < t)
                {
                    bool estado = (bool)row.Cells["col_checkbox"].Value;

                    if (estado == true)
                    {
                        cont++;
                    }
                    else
                    {
                        mnsj_error = "No ha seleccionado alguna nota de venta para descargar.";
                    }

                    c++;
                }
            }


            // Obtener el id de la nota a descargar

            if (cont > 0)
            {
                c = 0;
                arr_id_desc = new int[cont];

                foreach (DataGridViewRow row in DGVListadoVentas.Rows)
                {
                    if (c < t)
                    {
                        bool estado = (bool)row.Cells["col_checkbox"].Value;

                        if (estado == true)
                        {
                            arr_id_desc[d] = Convert.ToInt32(row.Cells["ID"].Value);
                            d++;
                        }
                        c++;
                    }
                }

                // Elige carpeta donde guardar el comprimido
                if (elegir_carpeta_descarga.ShowDialog() == DialogResult.OK)
                {
                    string carpeta = elegir_carpeta_descarga.SelectedPath;

                    inicia_descarga(arr_id_desc, carpeta);
                }
            }
            else
            {
                MessageBox.Show(mnsj_error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            agregarFocus();
        }

        public void inicia_descarga(int[] idnv, string carpeta_elegida)
        {
            pBar_descarga_verpdf.Visible = true;
            lb_texto_descarga_verpdf.Visible = true;


            pBar_descarga_verpdf.Minimum = 1;
            pBar_descarga_verpdf.Maximum = 6;
            pBar_descarga_verpdf.Value = 2; // Valor inicial
            pBar_descarga_verpdf.Step = 1;

            for (int x = 3; x <= 6; x++)
            {
                if (descargar_nota(idnv, x, carpeta_elegida) == true)
                {
                    pBar_descarga_verpdf.PerformStep();
                }
            }

            ban = false;

            MessageBox.Show("La(s) nota(s) de venta ha sido descargada(s).", "Mensaje del sistema", MessageBoxButtons.OK);

            pBar_descarga_verpdf.Visible = false;
            lb_texto_descarga_verpdf.Visible = false;
        }

        private bool descargar_nota(int[] idnv, int opc, string carpeta_elegida)
        {
            string n_user = Environment.UserName;
            string ruta_new_carpeta = @"C:\Archivos PUDVE\Ventas\PDF\VENTA_" + idnv[0];
            var servidor = Properties.Settings.Default.Hosting;

            if (ban == false)
            {
                MessageBox.Show("El archivo comprimido será descargado en la carpeta." + carpeta_elegida, "Mensaje del sistema", MessageBoxButtons.OK);

                ban = true;
            }


            // Si la conexión es en red cambia ruta de guardado
            if (!string.IsNullOrWhiteSpace(servidor))
            {
                ruta_new_carpeta = $@"\\{servidor}\Archivos PUDVE\Facturas\PDF\VENTA_" + idnv[0];
            }


            // Crear carpeta a comprimir

            if (opc == 3)
            {
                if (!Directory.Exists(ruta_new_carpeta))
                {
                    Directory.CreateDirectory(ruta_new_carpeta);
                }
                else
                {
                    DirectoryInfo dir_arch = new DirectoryInfo(ruta_new_carpeta);

                    foreach (FileInfo f in dir_arch.GetFiles())
                    {
                        f.Delete();
                    }
                }
            }


            // Verifica que el PDF ya este creado, de no ser así lo crea

            if (opc == 4)
            {
                for (var i = 0; i < idnv.Length; i++)
                {
                    string ruta_archivos = @"C:\Archivos PUDVE\Ventas\PDF\VENTA_" + idnv[i];
                    // Si la conexión es en red cambia ruta de guardado
                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        ruta_archivos = $@"\\{servidor}\Archivos PUDVE\Facturas\PDF\VENTA_" + idnv[i];
                    }


                    if (!File.Exists(ruta_archivos + ".pdf"))
                    {
                        ver_factura(idnv[i]);
                    }
                }
            }


            // Copiar archivos a la carpeta

            if (opc == 5)
            {
                for (var i = 0; i < idnv.Length; i++)
                {
                    string ruta_archivos = @"C:\Archivos PUDVE\Ventas\PDF\VENTA_" + idnv[i];
                    // Si la conexión es en red cambia ruta de guardado
                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        ruta_archivos = $@"\\{servidor}\Archivos PUDVE\Facturas\PDF\VENTA_" + idnv[i];
                    }

                    File.Copy(ruta_archivos + ".pdf", ruta_new_carpeta + "\\VENTA_" + idnv[i] + ".pdf");
                }
            }


            // Comprimir carpeta

            if (opc == 6)
            {
                DateTime fecha_actual = DateTime.UtcNow;
                string fech = fecha_actual.ToString("yyyyMMddhhmmss");

                string ruta_carpet_comprimida = $@"{carpeta_elegida}\VENTA_" + idnv[0] + "_" + fech + ".zip";


                ZipFile.CreateFromDirectory(ruta_new_carpeta, ruta_carpet_comprimida);


                // Eliminar carpeta 
                DirectoryInfo dir_arch = new DirectoryInfo(ruta_new_carpeta);

                foreach (FileInfo f in dir_arch.GetFiles())
                {
                    f.Delete();
                }

                Directory.Delete(ruta_new_carpeta);
            }


            return true;
        }

        private void btn_timbrar_Click(object sender, EventArgs e)
        {
            /*int cont = 0;
            int b = 0;
            int c = 0;
            int t = DGVListadoVentas.Rows.Count - 2;
            string mnsj_error = "";
            int[] arr_id_timb;


            foreach (DataGridViewRow row in DGVListadoVentas.Rows)
            {
                if (c < t)
                {
                    bool estado = (bool)row.Cells["col_checkbox"].Value;

                    if (estado == true)
                    {
                        cont++;
                    }
                    else
                    {
                        mnsj_error = "No ha seleccionado alguna nota de venta para timbrar.";
                    }

                    c++;
                }
            }


            // Obtener el id de la factura a enviar

            if (cont > 0)
            {
                c = 0;
                arr_id_timb = new int[cont];

                foreach (DataGridViewRow row in DGVListadoVentas.Rows)
                {
                    if (c < t)
                    {
                        bool estado = (bool)row.Cells["col_checkbox"].Value;

                        if (estado == true)
                        {
                            arr_id_timb[b] = Convert.ToInt32(row.Cells["ID"].Value);

                            b++;
                        }
                        c++;
                    }
                }


            }
            else
            {
                MessageBox.Show(mnsj_error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
            agregarFocus();
        }

        private void agregarFocus()
        {
            this.Focus();
        }
        private void ListadoVentas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnNuevaVenta.PerformClick();
            }

        }
        private void DGVListadoVentas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnNuevaVenta.PerformClick();
            }
        }

        private void cbTipoVentas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnNuevaVenta.PerformClick();
            }
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            var opcion = cbTipoVentas.SelectedValue.ToString();

            var codigosBuscar = recorrerDGVAlmacenarDiccionario();

            if (!string.IsNullOrEmpty(codigosBuscar))
            {
                //Se quita el * de la consulta para obtener solo los campos que me interesan y se guarda en una nueva variable
                //var ajustarQuery = FiltroAvanzado.Replace("*", "Cliente, RFC, IDEmpleado, Total, Folio, Serie, FechaOperacion");

                var ajustarQuery = $"SELECT Cliente, RFC, IDEmpleado, Total, Folio, Serie, FechaOperacion FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID IN ({codigosBuscar})";


                var query = cn.CargarDatos(ajustarQuery);

                if (!query.Rows.Count.Equals(0))
                {
                    Utilidades.GenerarReporteVentas(opcion, query);
                }
            }
            else
            {
                MessageBox.Show("No tiene ninguna venta seleccionada", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string recorrerDGVAlmacenarDiccionario()
        {
            //Recorre el Diccionario y agregar todo en una sola cadena para la consulta
            var cadenaCompleta = string.Empty;
            foreach (KeyValuePair<int, string> item in idVentas)
            {
                cadenaCompleta += $"{item.Key},".ToString();
            }
            cadenaCompleta = cadenaCompleta.TrimEnd(',');

            return cadenaCompleta;
        }

        private void chTodos_CheckedChanged(object sender, EventArgs e)
        {
            obtenerIDSeleccionados();
        }

        private void obtenerIDSeleccionados()
        {
            var incremento = -1;
            if (chTodos.Checked)
            {
                var query = cn.CargarDatos(FiltroAvanzado);

                if (!query.Rows.Count.Equals(0))
                {
                    foreach (DataRow dgv in query.Rows)
                    {
                        if (!idVentas.ContainsKey(Convert.ToInt32(dgv["ID"].ToString())))
                        {
                            idVentas.Add(Convert.ToInt32(dgv["ID"].ToString()), string.Empty);
                        }
                    }
                }
                
                foreach (DataGridViewRow marcarDGV in DGVListadoVentas.Rows)
                {
                    try
                    {
                        incremento += 1;
                        DGVListadoVentas.Rows[incremento].Cells["col_checkbox"].Value = true;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else if (!chTodos.Checked)
            {
                idVentas.Clear();
                foreach (DataGridViewRow desmarcarDGV in DGVListadoVentas.Rows)
                {
                    try
                    {
                        incremento += 1;
                        DGVListadoVentas.Rows[incremento].Cells["col_checkbox"].Value = false;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private void llenarGDV ()
        {
            //Los try son para las finas que son para totales que no se marquen

            var incremento = -1;

            foreach (DataGridViewRow dgv in DGVListadoVentas.Rows)
            {
                try
                {
                    if (dgv.Cells["ID"].Value == null) continue;

                    incremento += 1;

                    var idRevision = Convert.ToInt32(dgv.Cells["ID"].Value.ToString());

                    if (idVentas.ContainsKey(idRevision))
                    {
                        DGVListadoVentas.Rows[incremento].Cells["col_checkbox"].Value = true;
                    }
                    else
                    {
                        DGVListadoVentas.Rows[incremento].Cells["col_checkbox"].Value = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR AQUI: " + ex.Message);
                }
            }
        }

        private void MostrarCheckBox()
        {
            System.Drawing.Rectangle rect = DGVListadoVentas.GetCellDisplayRectangle(0, -1, true);
            // set checkbox header to center of header cell. +1 pixel to position 
            rect.Y = 5;
            rect.X = 10;// rect.Location.X + (rect.Width / 4);
            CheckBox checkBoxHeader = new CheckBox();
            checkBoxHeader.Name = "checkBoxMaster";
            checkBoxHeader.Size = new Size(15, 15);
            checkBoxHeader.Location = rect.Location;
            checkBoxHeader.CheckedChanged += new EventHandler(checkBoxMaster_CheckedChanged);
            DGVListadoVentas.Controls.Add(checkBoxHeader);
        }

        private void checkBoxMaster_CheckedChanged(object sender, EventArgs e)
        {
            var incremento = -1;

            CheckBox headerBox = ((CheckBox)DGVListadoVentas.Controls.Find("checkBoxMaster", true)[0]);

            foreach (DataGridViewRow dgv in DGVListadoVentas.Rows)
            {
                incremento += 1;

                try
                {
                    //var recorrerCheckBox = Convert.ToBoolean(DGVListadoVentas.Rows[incremento].Cells["col_checkbox"].Value);

                    var idRevision = Convert.ToInt32(dgv.Cells["ID"].Value.ToString());

                    if (headerBox.Checked)
                    {
                        if (!idVentas.ContainsKey(idRevision))
                        {
                            idVentas.Add(idRevision, string.Empty);
                            DGVListadoVentas.Rows[incremento].Cells["col_checkbox"].Value = true;
                        }
                    }
                    else if (!headerBox.Checked)
                    {
                        idVentas.Remove(idRevision);
                        DGVListadoVentas.Rows[incremento].Cells["col_checkbox"].Value = false;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }


        private bool revisarSiFueVentaACredito(int idAsignado)
        {
            var result = false;
            var sum = 0;

            var query = cn.CargarDatos($"SELECT Credito FROM DetallesVenta WHERE IDVenta = '{idAsignado}'");
            var queryDetalleVentas = cn.CargarDatos($"SELECT `Status` AS estatus FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idAsignado}'");

            if (!query.Rows.Count.Equals(0) && query.Rows[0]["Credito"].ToString() != "0.00")
            {
                sum += 1;
            }

            if (!queryDetalleVentas.Rows.Count.Equals(0) && queryDetalleVentas.Rows[0]["estatus"].ToString().Equals("1"))
            {
                sum += 1;
            }

            if (sum.Equals(2))
            {
                result = true;
            }

            return result;
        }

        private string[] cantidadAbonada(int idVenta)
        {
            List<string> lista = new List<string>();

            var obtenerMontoAbonado = cn.CargarDatos($"SELECT Total, Efectivo, Tarjeta, Vales, Cheque, Transferencia FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND IDVenta = '{idVenta}'");
            var obtenerTotalAbonado = string.Empty; var efectivo = string.Empty; var tarjeta = string.Empty; var vales = string.Empty; var cheque = string.Empty; var transferencia = string.Empty;

            if (!obtenerMontoAbonado.Rows.Count.Equals(0))
            {
                foreach (DataRow datosConsulta in obtenerMontoAbonado.Rows)
                {
                    obtenerTotalAbonado = datosConsulta["Total"].ToString();
                    efectivo = datosConsulta["Efectivo"].ToString();
                    tarjeta = datosConsulta["Tarjeta"].ToString();
                    vales = datosConsulta["Vales"].ToString();
                    cheque = datosConsulta["Cheque"].ToString();
                    transferencia = datosConsulta["Transferencia"].ToString();
                }
            }
            else
            {
                obtenerTotalAbonado = "0.00";
                efectivo = "0.00";
                tarjeta = "0.00";
                vales = "0.00";
                cheque = "0.00";
                transferencia = "0.00";
            }

            lista.Add(obtenerTotalAbonado);
            lista.Add(efectivo);
            lista.Add(tarjeta);
            lista.Add(vales);
            lista.Add(cheque);
            lista.Add(transferencia);

            return lista.ToArray();
        }

        private void txtMaximoPorPagina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!txtMaximoPorPagina.Text.Equals(string.Empty))
                {
                    var cantidadAMostrar = Convert.ToInt32(txtMaximoPorPagina.Text);

                    if (cantidadAMostrar <= 0)
                    {
                        mensajeParaMostrar = "Catidad a mostrar debe ser mayor a 0";
                        Utilidades.MensajeCuandoSeaCeroEnElListado(mensajeParaMostrar);
                        txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                        return;
                    }

                    maximo_x_pagina = cantidadAMostrar;
                    p.actualizarTope(maximo_x_pagina);
                    var tipoDeBusqueda = 0;
                    tipoDeBusqueda = verTipoDeBusqueda();
                    CargarDatos(tipoDeBusqueda);
                    actualizar();
                }
                else if (txtMaximoPorPagina.Text.Equals(string.Empty))
                {
                    txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                }
            }
        }

        private void txtMaximoPorPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void btnActualizarMaximoProductos_Click(object sender, EventArgs e)
        {
            if (!txtMaximoPorPagina.Text.Equals(string.Empty))
            {
                var cantidadAMostrar = Convert.ToInt32(txtMaximoPorPagina.Text);

                if (cantidadAMostrar <= 0)
                {
                    mensajeParaMostrar = "Catidad a mostrar debe ser mayor a 0";
                    Utilidades.MensajeCuandoSeaCeroEnElListado(mensajeParaMostrar);
                    txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                    return;
                }
                 
                maximo_x_pagina = cantidadAMostrar;
                p.actualizarTope(maximo_x_pagina);
                var tipoDeBusqueda = 0;
                tipoDeBusqueda = verTipoDeBusqueda();
                CargarDatos(tipoDeBusqueda);
                actualizar();
            }
            else if (txtMaximoPorPagina.Text.Equals(string.Empty))
            {
                txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
            }
        }

        private void cbFiltroAdminEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            var opcion = cbFiltroAdminEmpleado.SelectedValue.ToString();

            
        }
    }
}
