using System;
using System.Drawing;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Threading;
using static System.Windows.Forms.DataGridView;
using System.IO.Ports;
using System.Media;
using System.Globalization;

namespace PuntoDeVentaV2
{
    public partial class Ventas : Form
    {
        // Status 1 = Venta terminada
        // Status 2 = Venta guardada
        // Status 3 = Venta cancelada
        // Status 4 = Venta a credito
        // Status 5 = Facturas
        // Status 6 = Presupuestos

        public static double pasarSumaImportes { get; set; }
        public static double pasarTotalAnticipos { get; set; }
        private bool aplicarDescuentoG { get; set; }

        public static bool sepresiono = false;
        public static string etiqeutaCliente { get; set; }
        public static bool EsGuardarVenta;
        decimal primeraCantidad;
        List<string> ListaProdutos;
        // Almacena los ID de los productos a los que se aplica descuento general
        private Dictionary<int, bool> productosDescuentoG = new Dictionary<int, bool>();
        float porcentajeGeneral = 0;
        float descuentoCliente = 0;
        bool yasemando = false;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public static string porcentaje;
        List<string> productoEliminadoCorreo;
        string PrecioDelProducto;
        bool ClienteConDescuento = false;
        string ClienteConDescuentoNombre, IDClienteConDescuento;
        public static bool AutorizacionConfirmada = false;
        public static bool VentaRealizada = false;

        public static string cantidadAPedir = string.Empty;
        public static List<string> listProductos = new List<string>();
        public static List<string> liststock = new List<string>();
        public static List<string> liststock2 = new List<string>();
        List<string> stockCantidad = new List<string>();
        List<string> productoDeshabilitado = new List<string>();
        string cargarmensaje;

        public static bool ventaGuardada = false; //Para saber si la venta se guardo o no
        decimal cantidadExtra = 0;

        public static int indiceFila = 0; //Para guardar el indice de la fila cuando se elige agregar multiples productos
        public static int cantidadFila = 0; //Para guardar la cantidad de productos que se agregará a la fila correspondiente

        // Para las ventas guardadas
        public static int mostrarVenta = 0;
        private bool mostrarMensajeProducto = true;

        // Estado de la venta
        public static string statusVenta = string.Empty;

        // Concepto de la Venta
        public static string formaDePagoDeVenta = string.Empty;

        // Para los anticipos por aplicar
        public static string listaAnticipos = string.Empty;
        public static float importeAnticipo = 0f;

        string filtro;

        int noDuplicadoVentas /*= DetalleVenta.validarNoDuplicarVentas*/= 0;

        // Variables para almacenar los valores agregados en el form DetalleVenta.cs
        public static string efectivoReal = string.Empty;
        public static string efectivo = string.Empty;
        public static string tarjeta = string.Empty;
        public static string vales = string.Empty;
        public static string cheque = string.Empty;
        public static string transferencia = string.Empty;
        public static string referencia = string.Empty;
        public static string cliente = string.Empty;
        public static string idCliente = string.Empty;
        public static string credito = string.Empty;

        public static string codBarras;
        // Para saber con que boton se cerro el form DetalleVenta.cs, en este caso saber si se cerro con el boton aceptar (terminar)
        public static bool botonAceptar = false;

        // Lista para almacenar los IDs de las ventas guardadas que se han cargado
        public static List<int> ventasGuardadas = new List<int>();

        // Diccionario para guardar los descuentos directos que se asigne a un producto, servicio, combo
        public static Dictionary<int, Tuple<int, float>> descuentosDirectos = new Dictionary<int, Tuple<int, float>>();

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        // Almacena temporalmente los productos encontrados con las coincidencias de la busqueda
        Dictionary<int, string> productosD;

        const string fichero = @"\PUDVE\settings\folioventa\setupFolioVenta.txt";       // directorio donde esta el archivo de numero de codigo de barras consecutivo
        string Contenido;                                                               // para obtener el numero que tiene el codigo de barras en el arhivo

        long folioVenta; // variable entera para llevar un consecutivo de codigo de barras

        string buscarvVentaGuardada = null, folio = null;

        int idProducto;
        string producto;
        string[] datosProducto;
        private bool sumarProducto = false;
        private bool restarProducto = false;
        private bool buscarVG = false; // Buscar venta guardada
        private int indiceColumna = 0;
        private bool imprimirCodigo = false;
        int idClienteDescuento = 0;

        DataTable dtProdMessg;
        DataRow drProdMessg;

        // Variables para la configuracion de los correos que se enviaran
        private int correoStockMinimo = 0;
        private int correoVentaProducto = 0;
        private int mostrarPrecioProducto = 0;
        private int mostrarCBProducto = 0;
        private int correoVenta = 0;
        private int correoDescuento = 0;
        // Variables para la configuracion referente a los productos con mayoreo
        private bool mayoreoActivo = false;
        private int cantidadMayoreo = 0;

        // Listas para guardar los ID's de los productos que se enviara correo
        private Dictionary<int, string> enviarStockMinimo;
        private Dictionary<int, string> enviarVentaProducto;
        private List<string> enviarVenta;

        // Permisos de los botones
        int opcion9 = 1; // Boton cancelar
        int opcion10 = 1; // Guardar venta
        int opcion11 = 1; // Boton anticipos
        int opcion12 = 1; // Abrir caja
        int opcion13 = 1; // Ventas guardadas
        int opcion14 = 1; // Ver ultimo ticket
        int opcion15 = 1; // Guardar presupuesto
        int opcion16 = 1; // Descuento cliente
        int opcion17 = 1; // Eliminar ultimo
        int opcion18 = 1; // Eliminar todos
        int opcion19 = 1; // Aplicar descuento
        int opcion20 = 1; // Terminar venta

        bool ventaFinalizada = false;

        public static decimal cantidadAnterior = 0;
        int idComboServicio = 0;

        List<string> productoRestado = new List<string>(),
                     productoEliminado = new List<string>(),
                     productoUltimoAgregadoEliminado = new List<string>();

        string fechaSistema = string.Empty;

        string peso = string.Empty;

        bool primerClickRestarIndividual = false,
             primerClickEliminarIndividual = false,
             primerClickBtnUltimoEliminado = false;

        bool isOpen = false, isExists = false;

        string puerto = string.Empty,
                baudRate = string.Empty,
                dataBits = string.Empty,
                handshake = string.Empty,
                parity = string.Empty,
                stopBits = string.Empty,
                sendData = string.Empty;

        int contadorMensaje = 0;
        int contadorChangeValue = 0;

        int celdaCellClick;
        int columnaCellClick;

        string idprodCombo = string.Empty;
        int cantidadCombo = 0;
        int ventaGuardadappt = 0;

        public static string NombreCliente = string.Empty;

        public static string nombreprodCantidad = string.Empty;

        public int idAnticipoVentas;

        string descuentoGuardado = string.Empty;

        Dictionary<int, string> listaMensajesEnviados = new Dictionary<int, string>();

        int desdeVentaGuardada = 0;

        private string FolioVentaCorreo = string.Empty;

        string descuentoDirectoPorAplicar = string.Empty;

        int cambioCantidadProd = 0;

        public static string gananciaTotalPorVenta;

        public static string PorcentajeDescuento;
        public static string AplicarPorcentaje;
        public static string AplicarCantidad;
        public static bool HizoUnaAccion = false;
        public static bool SeCambioCantidad = false;

        int calcu = 0;

        public static bool EsEnVentas = false;

        public static string codBarProdVentaRapida;

        public static int IDAnticipo = 0;
        #region Proceso de Bascula
        // Constructores
        private SerialPort BasculaCom = new SerialPort();       // Puerto conectado a la báscula
        public delegate void MostrarRecepcion(string Texto);    // Delegado para asignar el valor recibido

        int nombreus, nombComercial, direccionus, colycpus, rfcus, correous, telefonous, nombrec, domicilioc, rfcc, correoc, telefonoc, colycpc, formapagoc;

        public static bool sonido = true;
        int contador = 0;

        float CantidadAnteriorEdit, NuevaCantidadEdit;

        bool tieneElCursorElTxtBuscadorProducto = true;

        // al recibir de la bascula los bytesToRead indicara
        // un valor superior a 0, indicando el numero de caracteres
        private void Recibir(object sender, SerialDataReceivedEventArgs e)
        {
            MostrarRececibidos(BasculaCom.ReadExisting().ToString());
        }

        // Enviar una solicitud a la bascula
        public void EnviarDatos()
        {
            // enviar una P para Torrey o segun sea el caso
            BasculaCom.Write(sendData);
        }

        // Mostrar los bytes recibidos en el Label recibidos
        private void MostrarRececibidos(string texto)
        {
            bool success = false;
            float number;

            if (lblPesoRecibido.InvokeRequired)
            {
                var delegado = new MostrarRecepcion(MostrarRececibidos);
                this.Invoke(delegado, new object[] { texto });
            }
            else
            {
                texto = texto.Replace(System.Environment.NewLine, string.Empty).Trim().Replace(" kg", string.Empty);
                lblPesoRecibido.Text = texto;
                if (!DGVentas.Rows.Count.Equals(0))
                {
                    success = float.TryParse(lblPesoRecibido.Text, out number);
                    if (success)
                    {
                        DGVentas.Rows[0].Cells["Cantidad"].Value = lblPesoRecibido.Text;
                        CantidadesFinalesVenta();
                    }
                }
            }
        }
        #endregion

        public Ventas()
        {
            InitializeComponent();

            metodoCancelarVentaDesdeListadoVentas();
        }

        private void metodoCancelarVentaDesdeListadoVentas()
        {
            var retomarVentas = ListadoVentas.retomarVentasCanceladas;

            if (retomarVentas == 1)
            {
                var idVentaObtenida = ListadoVentas.obtenerIdVenta;
                var numFolio = ListadoVentas.folioVenta;
                mostrarVenta = idVentaObtenida;
                CargarVentaGuardada(true);
                mostrarVenta = 0;
            }
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            CBTipo.SelectedItem = "Todos";
            CBTipo.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);

            mostrarBotonCSV();

            ventaGuardadappt = 0;
            if (string.IsNullOrEmpty(lbDatosCliente.Text))
            {
                etiqeutaCliente = "vacio";
                this.Visible = false;
            }
            else
            {
                etiqeutaCliente = "lleno";
                this.Visible = true;
            }

            label1.BackColor = Color.FromArgb(229, 231, 233);
            label3.BackColor = Color.FromArgb(229, 231, 233);

            txtBuscadorProducto.GotFocus += new EventHandler(BuscarTieneFoco);
            txtBuscadorProducto.LostFocus += new EventHandler(BuscarPierdeFoco);
            txtDescuentoGeneral.GotFocus += new EventHandler(DescuentoTieneFoco);
            txtDescuentoGeneral.LostFocus += new EventHandler(DescuentoPierdeFoco);

            btnEliminarUltimo.BackgroundImage = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");
            btnEliminarTodos.BackgroundImage = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");
            btnUltimoTicket.BackgroundImage = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\ticket.png");

            btnEliminarUltimo.BackgroundImageLayout = ImageLayout.Center;
            btnEliminarTodos.BackgroundImageLayout = ImageLayout.Center;
            btnUltimoTicket.BackgroundImageLayout = ImageLayout.Center;


            var datosConfig = mb.DatosConfiguracion();

            if (Convert.ToInt16(datosConfig[0]) == 1)
            {
                imprimirCodigo = true;
            }


            var configCorreos = mb.ComprobarConfiguracion();

            if (configCorreos.Count > 0)
            {
                correoStockMinimo = Convert.ToInt16(configCorreos[2]);
                correoVentaProducto = Convert.ToInt16(configCorreos[3]);
                mostrarPrecioProducto = Convert.ToInt16(configCorreos[6]);
                mostrarCBProducto = Convert.ToInt16(configCorreos[7]);
                mayoreoActivo = Convert.ToBoolean(configCorreos[9]);
                cantidadMayoreo = Convert.ToInt32(configCorreos[10]);
                correoVenta = Convert.ToInt32(configCorreos[21]);
                correoDescuento = Convert.ToInt32(configCorreos[23]);
            }

            enviarStockMinimo = new Dictionary<int, string>();
            enviarVentaProducto = new Dictionary<int, string>();
            enviarVenta = new List<string>();

            // Si es un empleado obtiene los permisos de los botones
            if (FormPrincipal.id_empleado > 0)
            {
                var permisos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Ventas");

                opcion9 = permisos[8];
                opcion10 = permisos[9];
                opcion11 = permisos[10];
                opcion12 = permisos[11];
                opcion13 = permisos[12];
                opcion14 = permisos[13];
                opcion15 = permisos[14];
                opcion16 = permisos[15];
                opcion17 = permisos[16];
                opcion18 = permisos[17];
                opcion19 = permisos[18];
                opcion20 = permisos[19];
                btnGanancia.Visible = false;
            }
            else
            {
                btnGanancia.Visible = true;
            }



            //iniciarBasculaPredeterminada();
            txtBuscadorProducto.Focus();
        }

        private void mostrarBotonCSV()
        {
            if (FormPrincipal.userNickName.Equals("HOUSEDEPOTAUTLAN") || FormPrincipal.userNickName.Equals("HOUSEDEPOTREPARTO"))
            {
                btnCSV.Visible = true;
            }
            else
            {
                btnCSV.Visible = false;
            }
        }

        private void BuscarTieneFoco(object sender, EventArgs e)
        {
            if (txtBuscadorProducto.Text == "BUSCAR PRODUCTO O SERVICIO...")
            {
                txtBuscadorProducto.Text = "";
            }
        }

        private void BuscarPierdeFoco(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscadorProducto.Text))
            {
                txtBuscadorProducto.Text = "BUSCAR PRODUCTO O SERVICIO...";

                mostrarMensajeProducto = true;
            }
        }

        private void DescuentoTieneFoco(object sender, EventArgs e)
        {
            if (FormPrincipal.userNickName.Contains('@'))
            {
                var datos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Ventas");
                if (datos[18].Equals(0))
                {
                    return;
                }
            }
            if (txtDescuentoGeneral.Text == "% descuento")
            {
                txtDescuentoGeneral.Text = "";
            }
        }

        private void DescuentoPierdeFoco(object sender, EventArgs e)
        {
            if (txtDescuentoGeneral.Text == "")
            {
                txtDescuentoGeneral.Text = "% descuento";
            }
        }

        private void ocultarResultados()
        {
            listaProductos.Visible = false;
        }

        private void txtBuscadorProducto_KeyDown(object sender, KeyEventArgs e)
        {
            sonido = true;
            // Tecla borrar y suprimir
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                if (txtBuscadorProducto.Text == string.Empty)
                {
                    nudCantidadPS.Value = 1;
                }
            }

            // Enter
            if (e.KeyData == Keys.Enter)
            {


                if (txtBuscadorProducto.Text == "+")
                {
                    txtBuscadorProducto.Text = "+1";
                }
                if (txtBuscadorProducto.Text == "-")
                {
                    txtBuscadorProducto.Text = "-1";
                }
                if (!string.IsNullOrWhiteSpace(txtBuscadorProducto.Text.Trim()))
                {
                    if (!txtBuscadorProducto.Text.Equals("BUSCAR PRODUCTO O SERVICIO..."))
                    {
                        if (checkCancelar.Checked)
                        {
                            return;
                        }


                        e.Handled = true;
                        e.SuppressKeyPress = true;

                        var busqueda = txtBuscadorProducto.Text.Trim();

                        //var estaDentroDelLimite = false;
                        //var esNumeroLaBusqueda = 0;

                        //estaDentroDelLimite = int.TryParse(busqueda, out esNumeroLaBusqueda);

                        //if (estaDentroDelLimite.Equals(false))
                        //{
                        //    MessageBox.Show("El valor que ingreso es mayor al limite permitido");
                        //    txtBuscadorProducto.Clear();
                        //    return;
                        //}

                        contadorMensaje = 0;
                        sonido = true;
                        contador = 0;

                        if (!DGVentas.Rows.Count.Equals(0))
                        {
                            string cantidadProductos = DGVentas.Rows[0].Cells[5].Value.ToString();
                            var valorMaximo = 2139999999;
                            var siEsCantidadValidadDGVentas = false;
                            var noEsCantidadValidaDGVentas = false;
                            decimal cantidadEnDGVEntas = 0;
                            double cantidadDoubleNoValida = 0;

                            siEsCantidadValidadDGVentas = decimal.TryParse(cantidadProductos, out cantidadEnDGVEntas);
                            noEsCantidadValidaDGVentas = double.TryParse(cantidadProductos, out cantidadDoubleNoValida);

                            if (siEsCantidadValidadDGVentas)
                            {
                                var patternInicioSimboloMas = @"^[\+]";
                                var patternInicioSimboloMenos = @"^[\-]";
                                var patternFinalizaSimboloMas = @"[\+]$";
                                var patternFinalizaSimboloMenos = @"[\-]$";

                                if (Regex.IsMatch(busqueda, patternInicioSimboloMas))
                                {
                                    var words = busqueda.Split('+');
                                    var siEsNumero = false;
                                    var numeroCapturado = 0;
                                    var maximoParaVender = valorMaximo;

                                    siEsNumero = int.TryParse(words[1].ToString().Trim(), out numeroCapturado);

                                    if (siEsNumero)
                                    {
                                        if ((numeroCapturado + (int)cantidadEnDGVEntas) > maximoParaVender)
                                        {
                                            MessageBox.Show($"La cantidad maxima para vender es {maximoParaVender}", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            txtBuscadorProducto.Text = string.Empty;
                                            txtBuscadorProducto.Focus();
                                            return;
                                        }
                                    }
                                }
                                else if (Regex.IsMatch(busqueda, patternInicioSimboloMenos))
                                {
                                    var words = busqueda.Split('-');
                                    var siEsNumero = false;
                                    var numeroCapturado = 0;
                                    var minimoParaVender = valorMaximo;

                                    siEsNumero = int.TryParse(words[1].ToString().Trim(), out numeroCapturado);

                                    if (siEsNumero)
                                    {
                                        if ((numeroCapturado + (int)cantidadEnDGVEntas) > minimoParaVender)
                                        {
                                            MessageBox.Show($"La cantidad maxima para disminuir es {minimoParaVender}", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            txtBuscadorProducto.Text = string.Empty;
                                            txtBuscadorProducto.Focus();
                                            return;
                                        }
                                    }
                                }
                                else if (Regex.IsMatch(busqueda, patternFinalizaSimboloMas))
                                {
                                    var words = busqueda.Split('+');
                                    var siEsNumero = false;
                                    var numeroCapturado = 0;
                                    var maximoParaVender = valorMaximo;

                                    siEsNumero = int.TryParse(words[0].ToString().Trim(), out numeroCapturado);

                                    if (siEsNumero)
                                    {
                                        if ((numeroCapturado + (int)cantidadEnDGVEntas) > maximoParaVender)
                                        {
                                            MessageBox.Show($"La cantidad maxima para vender es {maximoParaVender}", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            txtBuscadorProducto.Text = string.Empty;
                                            txtBuscadorProducto.Focus();
                                            return;
                                        }
                                    }
                                }
                                else if (Regex.IsMatch(busqueda, patternFinalizaSimboloMenos))
                                {
                                    var words = busqueda.Split('-');
                                    var siEsNumero = false;
                                    var numeroCapturado = 0;
                                    var minimoParaVender = valorMaximo;

                                    siEsNumero = int.TryParse(words[0].ToString().Trim(), out numeroCapturado);

                                    if (siEsNumero)
                                    {
                                        if ((numeroCapturado + (int)cantidadEnDGVEntas) > minimoParaVender)
                                        {
                                            MessageBox.Show($"La cantidad maxima para disminuir es {minimoParaVender}", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            txtBuscadorProducto.Text = string.Empty;
                                            txtBuscadorProducto.Focus();
                                            return;
                                        }
                                    }
                                }
                            }
                            else if (noEsCantidadValidaDGVentas)
                            {
                                MessageBox.Show($"La cantidad maxima para vender es {valorMaximo.ToString("N")}", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtBuscadorProducto.Text = string.Empty;
                                return;
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(busqueda))
                        {
                            var caracter = busqueda[0].ToString();

                            if (caracter.Equals("+") || caracter.Equals("-"))
                            {
                                reproducirProductoAgregado();
                            }
                        }

                        // Combinación para abrir caja

                        if (busqueda.Equals(".4."))
                        {
                            timerBusqueda.Interval = 1;
                            txtBuscadorProducto.Text = string.Empty;
                            btnAbrirCaja.PerformClick();
                            return;
                        }

                        if (listaProductos.Visible)
                        {
                            // Si busca un producto por codigo de barras o clave le da prioridad a este metodo
                            // si encuentra un producto lo va a mostrar de lo contrario la ejecucion seguira y
                            // se mostrara el primer producto que aparezca en la lista
                            var respuesta = BuscarProductoPorCodigo();

                            if (respuesta.Equals(false))
                            {
                                OperacionBusqueda();
                                txtBuscadorProducto.Focus();
                                txtBuscadorProducto.Select(txtBuscadorProducto.Text.Length, 0);
                                return;
                            }



                            if (respuesta)
                            {
                                reproducirProductoAgregado();
                                return;
                            }

                            // Verifica si la lista esta visible y contiene un producto al menos
                            // si la lista no tiene el focus le agregada el focus y selecciona el primer producto
                            // que muestra por defecto y ejecuta el evento enter de la lista para agregar el producto
                            if (!listaProductos.Focused)
                            {
                                listaProductos.SelectedIndex = 0;
                                listaProductos.Focus();
                                listaProductos_KeyDown(sender, e);
                            }

                        }
                        else
                        {
                            // Le da prioridad a buscar un producto por codigo de barras o clave
                            var respuesta = BuscarProductoPorCodigo();

                            if (respuesta)
                            {
                                reproducirProductoAgregado();
                                //return;
                            }

                            //===============================================================
                            // Este codigo es para cuando intenta agregar mas cantidad de algun producto
                            // desde el buscador de producto, solo aplica para sumar y restar producto
                            sumarProducto = true;
                            restarProducto = true;
                            //===============================================================
                            // Esto se ejecuta para si el patron de busqueda coincide con la 
                            // busqueda de una venta guardada
                            buscarVG = true;

                            //txtBuscadorProducto_KeyUp(sender, e);
                            OperacionBusqueda(1);
                            funteListBox();

                            sumarProducto = false;
                            restarProducto = false;
                            buscarVG = false;

                        }
                        listaProductosVenta();
                        listaProductos.Focus();
                        txtBuscadorProducto.Focus();
                    }
                }
                listaProductos.Focus();
            }
        }

        private void CancelarVenta(string numFolio)
        {
            // var folio = txtBuscadorProducto.Text.Trim();
            var folio = numFolio.Trim();

            if (!string.IsNullOrWhiteSpace(folio))
            {
                var consulta = $"SELECT ID FROM Ventas WHERE IDUsuario = {FormPrincipal.userID} AND Folio = {folio} AND Status = 1";
                // Verificar y obtener ID de la venta utilizando el folio
                var existe = (bool)cn.EjecutarSelect(consulta);

                if (existe)
                {
                    var respuesta = MessageBox.Show("¿Desea cancelar la venta?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes)
                    {
                        var idVenta = Convert.ToInt32(cn.EjecutarSelect(consulta, 1));
                        mostrarVenta = idVenta;

                        // Cancelar la venta
                        var resultado = cn.EjecutarConsulta(cs.ActualizarVenta(idVenta, 3, FormPrincipal.userID));

                        if (resultado > 0)
                        {
                            // Regresar la cantidad de producto vendido al stock
                            var productos = cn.ObtenerProductosVenta(idVenta);

                            if (productos.Length > 0)
                            {
                                foreach (var producto in productos)
                                {
                                    var info = producto.Split('|');
                                    var idProducto = info[0];
                                    var cantidad = float.Parse(info[2]);

                                    cn.EjecutarConsulta($"UPDATE Productos SET Stock =  Stock + {cantidad} WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID}");
                                }
                            }

                            // Agregamos marca de agua al PDF del ticket de la venta cancelada
                            Utilidades.CrearMarcaDeAgua(idVenta, "CANCELADA");

                            var formasPago = mb.ObtenerFormasPagoVenta(idVenta, FormPrincipal.userID);

                            // Operacion para que la devolucion del dinero afecte al apartado Caja
                            if (formasPago.Length > 0)
                            {
                                var total = formasPago.Sum().ToString();
                                var efectivo = formasPago[0].ToString();
                                var tarjeta = formasPago[1].ToString();
                                var vales = formasPago[2].ToString();
                                var cheque = formasPago[3].ToString();
                                var transferencia = formasPago[4].ToString();
                                var credito = formasPago[5].ToString();
                                var anticipo = "0";

                                var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                var concepto = $"DEVOLUCION DINERO VENTA CANCELADA ID {idVenta}";

                                string[] datos = new string[] {
                                            "retiro", total, "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(),
                                            efectivo, tarjeta, vales, cheque, transferencia, credito, anticipo,FormPrincipal.id_empleado.ToString()
                                        };

                                cn.EjecutarConsulta(cs.OperacionCaja(datos));
                            }

                        }
                        //Cargar la venta cancelada 
                        CargarVentaGuardada();
                        mostrarVenta = 0;
                    }
                }

                txtBuscadorProducto.Text = string.Empty;
                checkCancelar.Checked = false;
            }
        }

        private bool BuscarProductoPorCodigo()
        {
            bool existe = false;

            var busqueda = txtBuscadorProducto.Text;

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                if (txtBuscadorProducto.Text != "BUSCAR PRODUCTO O SERVICIO...")
                {
                    int idProducto = 0;

                    string codigo = txtBuscadorProducto.Text.Trim();

                    // Verificamos si existe en la tabla de codigos de barra extra
                    var datosTmp = mb.BuscarCodigoBarrasExtra2(codigo, filtro);

                    if (datosTmp.Length > 0)
                    {
                        foreach (var id in datosTmp)
                        {
                            // Verificar que pertenece al usuario
                            var verificarUsuario = (bool)cn.EjecutarSelect($"SELECT * FROM Productos WHERE ID = {id} AND IDUsuario = {FormPrincipal.userID} AND Status = 1");

                            if (verificarUsuario)
                            {
                                idProducto = Convert.ToInt32(id);

                                var dato = cn.CargarDatos($"SELECT FormatoDeVenta FROM productos WHERE ID = '{id}' AND IDUSuario = '{FormPrincipal.userID}' AND Status = '1'");
                                var estado = dato.Rows[0]["FormatoDeVenta"].ToString();

                                decimal result = Convert.ToDecimal(nudCantidadPS.Value);
                                if (result.ToString().Contains('.'))
                                {
                                    MessageBox.Show("Este producto se vende solo por unidades enteras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return existe;
                                }

                            }
                        }
                    }


                    if (!string.IsNullOrWhiteSpace(txtBuscadorProducto.Text))
                    {
                        string querySearchProd;
                        if (CBTipo.SelectedItem.Equals(0))
                        {
                            querySearchProd = $"SELECT prod.ID FROM Productos AS prod WHERE IDUsuario = '{FormPrincipal.userID}' AND (ClaveInterna = '{codigo}' OR CodigoBarras = '{codigo}') AND Status = 1";
                        }
                        else
                        {
                            querySearchProd = $"SELECT prod.ID FROM Productos AS prod WHERE IDUsuario = '{FormPrincipal.userID}' AND (ClaveInterna = '{codigo}' OR CodigoBarras = '{codigo}') AND Status = 1 AND Tipo = '{filtro}'";
                        }


                        DataTable searchProd = cn.CargarDatos(querySearchProd);

                        if (searchProd.Rows.Count > 0)
                        {
                            idProducto = Convert.ToInt32(searchProd.Rows[0]["ID"].ToString());
                        }

                    }


                    if (idProducto > 0)
                    {
                        using (dtProdMessg = cn.CargarDatos(cs.ObtenerProductMessage(Convert.ToString(idProducto))))
                        {
                            if (dtProdMessg.Rows.Count > 0)
                            {
                                drProdMessg = dtProdMessg.Rows[0];
                                //MessageBox.Show(drProdMessg["ProductOfMessage"].ToString().ToUpper(), "Mensaje para el cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        string[] datosProducto = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                        txtBuscadorProducto.Text = "";
                        txtBuscadorProducto.Focus();
                        ocultarResultados();

                        if (!productosDescuentoG.ContainsKey(idProducto))
                        {
                            productosDescuentoG.Add(idProducto, aplicarDescuentoG);
                        }

                        AgregarProducto(datosProducto);
                        nudCantidadPS.Value = 1;
                        existe = true;
                    }
                }
            }

            return existe;
        }

        private void ReproducirSonido()
        {
            try
            {
                var rutaSonido = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Sounds\sonido_alarma.wav";

                var archivo = new AudioFileReader(rutaSonido);
                var trimmed = new OffsetSampleProvider(archivo);
                trimmed.SkipOver = TimeSpan.FromSeconds(0);
                trimmed.Take = TimeSpan.FromSeconds(2);

                var player = new WaveOutEvent();
                player.Init(trimmed);
                player.Play();
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"El produto(código o clave) escaneado:\n\n{txtBuscadorProducto.Text}\n\nNo se encuentra registrado en el Sistema", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void reproducirProductoAgregado()
        {
            if (sonido == true)
            {
                try
                {
                    var rutaSonido = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Sounds\sonidoProductoAgregado.wav";

                    var archivo = new AudioFileReader(rutaSonido);
                    var trimmed = new OffsetSampleProvider(archivo);
                    trimmed.SkipOver = TimeSpan.FromSeconds(0);
                    trimmed.Take = TimeSpan.FromSeconds(2);

                    var player = new WaveOutEvent();
                    player.Init(trimmed);
                    player.Play();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show($"El produto(código o clave) escaneado:\n\n{txtBuscadorProducto.Text}\n\nNo se encuentra registrado en el Sistema", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void AgregarProducto(string[] datosProducto, decimal cnt = 1)
        {
            stockCantidad.Clear();
            foreach (DataGridViewRow cantidades in DGVentas.Rows)
            {
                stockCantidad.Add(cantidades.Cells["Cantidad"].Value.ToString());
            }
            if (DGVentas.Rows.Count == 0 && buscarvVentaGuardada == ".#")
            {
                if (cnt >= 1)
                {
                    AgregarProductoLista(datosProducto, 1, false);
                    validarStockDGV();
                }
                else if (cnt < 1)
                {
                    var ignorar = false;
                    AgregarProductoLista(datosProducto, cnt, ignorar);
                    validarStockDGV();
                }
            }
            else if (DGVentas.Rows.Count > 0)
            {
                bool existe = false;

                foreach (DataGridViewRow fila in DGVentas.Rows)
                {
                    //Compara el valor de la celda con el nombre del producto (Descripcion)
                    if (fila.Cells["Descripcion"].Value.Equals(datosProducto[1]) && fila.Cells["IDProducto"].Value.Equals(datosProducto[0]))
                    {
                        decimal sumar = cnt;// 1;

                        if (cantidadExtra > 0)
                        {
                            sumar = cantidadExtra;
                            nudCantidadPS.Value = 1;
                            cantidadExtra = 0;
                        }
                        else if (cantidadExtra == 0)
                        {
                            if (Convert.ToDecimal(nudCantidadPS.Value) > 0)
                            {
                                sumar = Convert.ToDecimal(nudCantidadPS.Value);
                            }

                            if (cnt > 1)
                            {
                                sumar = cnt;
                            }

                            nudCantidadPS.Value = 1;
                        }
                        decimal cantidad = Convert.ToDecimal(fila.Cells["Cantidad"].Value);
                        var resultado = Convert.ToString(sumar).Contains('-');
                        if (resultado == true && ConsultarProductoVentas.cancelarResta == "return")
                        {
                            return;
                        }
                        else
                        {
                            cantidad = Convert.ToDecimal(fila.Cells["Cantidad"].Value) + sumar;
                        }

                        float importe = float.Parse(cantidad.ToString()) * float.Parse(fila.Cells["Precio"].Value.ToString());

                        fila.Cells["Cantidad"].Value = cantidad;
                        fila.Cells["Importe"].Value = importe;
                        existe = true;

                        int idProducto = Convert.ToInt32(fila.Cells["IDProducto"].Value);
                        int tipoDescuento = Convert.ToInt32(fila.Cells["DescuentoTipo"].Value);
                        int numeroFila = fila.Index;// DGVentas.CurrentCell.RowIndex;

                        if (tipoDescuento > 0)
                        {
                            string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                            if (!datosDescuento.Equals(null) && datosDescuento.Length > 0)
                            {
                                CalcularDescuento(datosDescuento, tipoDescuento, Convert.ToDecimal(cantidad), numeroFila);
                            }
                        }

                        CantidadesFinalesVenta();

                        timer_img_producto.Stop();

                        // Imagen del producto
                        var imagen = fila.Cells["ImagenProducto"].Value.ToString();

                        if (!string.IsNullOrEmpty(imagen))
                        {
                            var servidor = Properties.Settings.Default.Hosting;
                            var rutaImagen = string.Empty;

                            if (!string.IsNullOrWhiteSpace(servidor))
                            {
                                rutaImagen = $@"\\{servidor}\PUDVE\Productos\" + imagen;
                            }
                            else
                            {
                                rutaImagen = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Productos\" + imagen;
                            }

                            if (File.Exists(rutaImagen))
                            {
                                PBImagen.Image = System.Drawing.Image.FromFile(rutaImagen);
                                timer_img_producto.Start();
                            }
                            else
                            {
                                PBImagen.Image = null;
                                PBImagen.Refresh();
                            }
                        }
                        else
                        {
                            PBImagen.Image = null;
                            PBImagen.Refresh();
                        }

                        fila.Cells["NumeroColumna"].Value = indiceColumna;
                        DGVentas.Sort(DGVentas.Columns["NumeroColumna"], System.ComponentModel.ListSortDirection.Descending);
                        DGVentas.ClearSelection();
                        indiceColumna++;
                        break;
                    }
                }

                if (!existe)
                {
                    var ignorar = false;

                    if (cnt > 1)
                    {
                        ignorar = true;
                    }

                    AgregarProductoLista(datosProducto, cnt, ignorar);
                    validarStockDGV();
                }

            }
            else
            {
                var ignorar = false;

                if (cnt > 1)
                {
                    ignorar = true;
                }

                AgregarProductoLista(datosProducto, cnt, ignorar);
                validarStockDGV();
            }
            CalculoMayoreo();
            CantidadesFinalesVenta();
            //validarStockDGV();

        }

        private void AgregarProductoLista(string[] datosProducto, decimal cantidad, bool ignorar = false)
        {
            decimal cantidadTmp = cantidad;

            if (cantidad.ToString().Contains('-'))
            {
                //MessageBox.Show("Uno de los productos a disminuir es menor a la                 cantidad indicada", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Se agrega la nueva fila y se obtiene el ID que tendrá
            int rowId = DGVentas.Rows.Add();

            // Obtener la nueva fila
            DataGridViewRow row = DGVentas.Rows[rowId];

            if (buscarvVentaGuardada == ".#")
            {
                // Agregamos la información
                row.Cells["NumeroColumna"].Value = indiceColumna;
                row.Cells["IDProducto"].Value = datosProducto[0]; // Este campo no es visible
                row.Cells["PrecioOriginal"].Value = datosProducto[2]; // Este campo no es visible
                row.Cells["PrecioAuxiliar"].Value = datosProducto[2]; // Este campo no es visible
                row.Cells["DescuentoTipo"].Value = datosProducto[3]; // Este campo tampoco es visible
                row.Cells["Stock"].Value = datosProducto[4]; // Este campo no es visible
                row.Cells["TipoPS"].Value = datosProducto[5]; // Este campo no es visible
                row.Cells["PrecioMayoreo"].Value = datosProducto[12]; // Este campo no es visible
                row.Cells["Impuesto"].Value = datosProducto[13]; // Este campo no es visible
                row.Cells["Cantidad"].Value = datosProducto[6];
                row.Cells["Precio"].Value = datosProducto[2];
                row.Cells["Descripcion"].Value = datosProducto[1];
                row.Cells["Descuento"].Value = datosProducto[3];
                row.Cells["Importe"].Value = datosProducto[2];

            }
            else
            {
                if (cantidadExtra > 0)
                {
                    cantidad = cantidadExtra;
                    nudCantidadPS.Value = 1;
                    cantidadExtra = 0;
                }
                else if (cantidadExtra == 0)
                {
                    if (cantidad.Equals(1))
                    {
                        if (Convert.ToDecimal(nudCantidadPS.Value) > 0)
                        {
                            cantidad = Convert.ToDecimal(nudCantidadPS.Value);
                            primeraCantidad = cantidad;
                        }
                    }

                    nudCantidadPS.Value = cantidad;
                }


                //Esto es para cuando se agregan los productos al momento de cargar venta guardada desde la lista
                if (ignorar == true)
                {
                    cantidad = cantidadTmp;
                }

                //Agregamos la información
                row.Cells["NumeroColumna"].Value = indiceColumna;
                row.Cells["IDProducto"].Value = datosProducto[0]; // Este campo no es visible
                row.Cells["PrecioOriginal"].Value = datosProducto[2]; // Este campo no es visible
                row.Cells["PrecioAuxiliar"].Value = datosProducto[2]; // Este campo no es visible
                row.Cells["DescuentoTipo"].Value = datosProducto[3]; // Este campo tampoco es visible
                row.Cells["Stock"].Value = datosProducto[4]; // Este campo no es visible
                row.Cells["TipoPS"].Value = datosProducto[5]; // Este campo no es visible
                row.Cells["PrecioMayoreo"].Value = datosProducto[12]; // Este campo no es visible
                row.Cells["Impuesto"].Value = datosProducto[13]; // Este campo no es visible
                row.Cells["Cantidad"].Value = cantidad;
                row.Cells["Precio"].Value = datosProducto[2];
                row.Cells["Descripcion"].Value = datosProducto[1];
                listProductos.Add(datosProducto[0] + "|" + cantidadTmp.ToString());//ID producto

                if (desdeVentaGuardada.Equals(1))
                {
                    if ((datosProducto.Length - 1) == 14)
                    {
                        row.Cells["Descuento"].Value = datosProducto[14];
                        row.Cells["TipoDescuento"].Value = datosProducto[3];
                    }
                    else if (datosProducto.Length.Equals(19))
                    {
                        row.Cells["Descuento"].Value = datosProducto[18];
                        row.Cells["TipoDescuento"].Value = datosProducto[3];
                    }
                    else
                    {
                        row.Cells["Descuento"].Value = "0.00";
                        row.Cells["TipoDescuento"].Value = "0";
                    }
                }
                else
                {
                    if ((datosProducto.Length - 1) == 14)
                    {
                        row.Cells["Descuento"].Value = datosProducto[14];
                        row.Cells["TipoDescuento"].Value = "0";
                    }
                    else if (datosProducto.Length.Equals(19))
                    {
                        row.Cells["Descuento"].Value = datosProducto[18];
                        row.Cells["TipoDescuento"].Value = "0";
                    }
                    else
                    {
                        row.Cells["Descuento"].Value = "0.00";
                        row.Cells["TipoDescuento"].Value = "0";
                    }
                }

                row.Cells["ImagenProducto"].Value = datosProducto[9];

                var imagen = row.Cells["ImagenProducto"].Value.ToString();

                timer_img_producto.Stop();

                if (!string.IsNullOrEmpty(imagen))
                {
                    var servidor = Properties.Settings.Default.Hosting;
                    var rutaImagen = string.Empty;

                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        rutaImagen = $@"\\{servidor}\PUDVE\Productos\" + imagen;
                    }
                    else
                    {
                        rutaImagen = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Productos\" + imagen;
                    }

                    if (File.Exists(rutaImagen))
                    {
                        PBImagen.Image = System.Drawing.Image.FromFile(rutaImagen);
                        //timer_img_producto.Start();
                    }
                    else
                    {
                        PBImagen.Image = null;
                        PBImagen.Refresh();
                    }
                }
                else
                {
                    PBImagen.Image = null;
                    PBImagen.Refresh();
                }

                //row.Cells["Importe"].Value = datosProducto[2];

                // Se agrego esto para calcular el descuento del producto cuando se agrega por primera vez
                // y se agrega la cantidad con una de las combinaciones del teclado en la barra de busqueda
                //float importe = Convert.ToInt32(cantidad) * float.Parse(datosProducto[2]);

                float importe = 0;

                if (desdeVentaGuardada.Equals(1))
                {
                    if (datosProducto.Length.Equals(19))
                    {
                        var precioProducto = datosProducto[2].ToString();
                        var montoDescontado = string.Empty;

                        if (datosProducto[18].Contains("-"))
                        {
                            var cantidadPorciento = datosProducto[18].Split('-');
                            montoDescontado = cantidadPorciento[0].ToString().Trim();
                        }
                        else
                        {
                            montoDescontado = datosProducto[18].ToString().Trim();
                        }

                        importe = ((float)Convert.ToDouble(cantidad) * (float)Convert.ToDouble(precioProducto)) - (float)Convert.ToDouble(montoDescontado);
                    }
                }
                else
                {
                    var precioProducto = datosProducto[2].ToString();
                    importe = (float)Convert.ToDouble(cantidad) * (float)Convert.ToDouble(precioProducto);
                }

                row.Cells["Importe"].Value = importe;

                int idProducto = Convert.ToInt32(datosProducto[0]);
                int tipoDescuento = Convert.ToInt32(datosProducto[3]);

                if (desdeVentaGuardada.Equals(0))
                {
                    if (tipoDescuento > 0)
                    {
                        string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                        if (!datosDescuento.Equals(null) && datosDescuento.Length > 0)
                        {
                            CalcularDescuento(datosDescuento, tipoDescuento, Convert.ToDecimal(cantidad), rowId);
                        }
                    }
                }

                CantidadesFinalesVenta();
            }

            System.Drawing.Image img1 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\plus-square.png");
            System.Drawing.Image img2 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\plus.png");
            System.Drawing.Image img3 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\minus.png");
            System.Drawing.Image img4 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\remove.png");

            DGVentas.Rows[rowId].Cells["AgregarMultiple"].Value = img1;
            DGVentas.Rows[rowId].Cells["AgregarIndividual"].Value = img2;
            DGVentas.Rows[rowId].Cells["RestarIndividual"].Value = img3;
            DGVentas.Rows[rowId].Cells["EliminarIndividual"].Value = img4;

            DGVentas.Sort(DGVentas.Columns["NumeroColumna"], System.ComponentModel.ListSortDirection.Descending);
            DGVentas.ClearSelection();
            indiceColumna++;

            var datos = cn.CargarDatos($"SELECT FormatoDeVenta FROM productos WHERE IDUsuario = '{FormPrincipal.userID}' AND CodigoBarras = '{datosProducto[7]}' ORDER BY ID DESC LIMIT 1");
            var pesoAutomatico = datos.Rows[0]["FormatoDeVenta"].ToString();
            if (pesoAutomatico == "2")
            {
                btnBascula.PerformClick();
            }

        }

        private void validarStockDGV()
        {
            //var idClient = 0;

            //var celda = DGVentas.CurrentCell.RowIndex;
            //var columna = DGVentas.CurrentCell.ColumnIndex;

            //DGVentas.Columns["Descripcion"].DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 9.75F, FontStyle.Regular);

            //foreach (DataGridViewRow dgv in DGVentas.Rows)
            //{

            //    idClient = Convert.ToInt32(dgv.Cells["IDProducto"].Value.ToString());
            //    var resultado = buscarInfo(idClient);
            //    var stockObtenido = float.Parse(resultado[0].ToString());//Stock
            //    var stockMinimoObtenido = float.Parse(resultado[1].ToString());//Stock Minimo
            //    var nombreProducto = resultado[2].ToString();//Nombre del Producto

            //    if (stockObtenido <= stockMinimoObtenido && stockObtenido > 0)
            //    {
            //        //DGVentas.Columns["Descripcion"].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12F, FontStyle.Bold);
            //        DGVentas.Rows[celda].Cells["Descripcion"].Style.Font = new System.Drawing.Font("Verdana", 12F, FontStyle.Bold);
            //    }
            //    else if (stockObtenido < 1)
            //    {
            //        DGVentas.Rows[celda].Cells["Descripcion"].Value = $"**{nombreProducto}";
            //        DGVentas.Rows[celda].Cells["Descripcion"].Style.Font = new System.Drawing.Font("Verdana", 12F, FontStyle.Bold);
            //    }
            //    else
            //    {
            //        //DGVentas.Columns["Descripcion"].DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 9.75F, FontStyle.Regular);
            //        DGVentas.Rows[celda].Cells["Descripcion"].Style.Font = new System.Drawing.Font("Century Gothic", 9.75F, FontStyle.Regular);
            //    }
            //}
        }

        private string[] buscarInfo(int idDelPRoducto)
        {
            List<string> lista = new List<string>();
            var nombreProd = string.Empty; var cantidadStock = string.Empty; var cantidadStockMinimo = string.Empty;

            var query = cn.CargarDatos($"SELECT Nombre, Stock, StockMinimo FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idDelPRoducto}'");

            if (!query.Rows.Count.Equals(0))
            {
                cantidadStock = query.Rows[0]["Stock"].ToString();
                cantidadStockMinimo = query.Rows[0]["StockMinimo"].ToString();
                nombreProd = query.Rows[0]["Nombre"].ToString();
            }

            lista.Add(cantidadStock);
            lista.Add(cantidadStockMinimo);
            lista.Add(nombreProd);

            return lista.ToArray(); ;
        }

        private void DGVentas_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Iconos operaciones por producto
            if (e.ColumnIndex >= 5)
            {
                DGVentas.Cursor = Cursors.Hand;
            }
            else
            {
                DGVentas.Cursor = Cursors.Default;
            }
        }

        private void DGVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        { //consultarListaRelacionados 356 (por si no jala)
            if (e.RowIndex >= 0)
            {
                var noSeBorroFila = true;
                celdaCellClick = DGVentas.CurrentCell.RowIndex;
                columnaCellClick = DGVentas.CurrentCell.ColumnIndex;

                if (celdaCellClick.Equals(-1))
                {
                    return;
                }

                var imagen = DGVentas.Rows[celdaCellClick].Cells["ImagenProducto"].Value.ToString();

                if (!string.IsNullOrEmpty(imagen))
                {
                    var servidor = Properties.Settings.Default.Hosting;
                    var rutaImagen = string.Empty;

                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        rutaImagen = $@"\\{servidor}\PUDVE\Productos\" + imagen;
                    }
                    else
                    {
                        rutaImagen = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Productos\" + imagen;
                    }

                    if (File.Exists(rutaImagen))
                    {
                        PBImagen.Image = System.Drawing.Image.FromFile(rutaImagen);
                        //timer_img_producto.Start();
                    }
                    else
                    {
                        PBImagen.Image = null;
                        PBImagen.Refresh();
                    }
                }
                else
                {
                    PBImagen.Image = null;
                    PBImagen.Refresh();
                }

                // Cantidad
                if (columnaCellClick.Equals(5))
                {
                    if (!DGVentas.CurrentCell.Equals(null) && !DGVentas.CurrentCell.Value.Equals(null))
                    {
                        cambioCantidadProd = 1;
                        nombreprodCantidad = DGVentas.Rows[celdaCellClick].Cells["Descripcion"].Value.ToString();
                        cantidadAnterior = Convert.ToDecimal(DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value.ToString());
                        var idProductoModificado = DGVentas.Rows[celdaCellClick].Cells["IDProducto"].Value.ToString();
                        var datos = cn.CargarDatos($"SELECT CodigoBarras FROM productos WHERE ID = {idProductoModificado}");
                        codBarras = datos.Rows[0]["CodigoBarras"].ToString();


                        DGVentas.Rows.Remove(DGVentas.CurrentRow);
                        DGVentas.Refresh();
                        txtBuscadorProducto.Text = codBarras;
                        OperacionBusqueda(1);
                        funteListBox();
                        ProductoSeleccionado();
                        listaProductos.Visible = false;

                        cantidadComprada cantidad = new cantidadComprada();
                        cantidad.ShowDialog();
                        //if (cantidadComprada.nuevaCantidad > cantidadAnterior)
                        //{
                        
                        txtBuscadorProducto.Text = "+" + (cantidadComprada.nuevaCantidad - 1);
                        txtBuscadorProducto.Focus();
                       SendKeys.Send("{ENTER}");
                     listaProductos.Visible = false;
                        //}
                        //else
                        //{
                        //    txtBuscadorProducto.Text = (cantidadAnterior * -1) + cantidadComprada.nuevaCantidad + "";
                        //    txtBuscadorProducto.Focus();
                        //    SendKeys.Send("{ENTER}");
                        //    listaProductos.Visible = false;
                        //}
                    }
                  SendKeys.Send("{BACKSPACE}");
                    cambioCantidadProd = 0;
                    if (SeCambioCantidad == true)
                    {
                        float cantidad = 0;

                        if (!descuentosDirectos.Count.Equals(0))
                        {
                            try
                            {
                                cantidad = descuentosDirectos[idProducto].Item2;
                            }
                            catch (Exception)
                            {

                                cantidad = 0;
                            }

                        }
                        else
                        {
                            cantidad = 0;
                        }
                        if (string.IsNullOrWhiteSpace(AplicarPorcentaje) && string.IsNullOrWhiteSpace(AplicarCantidad) && cantidad > 0 || !string.IsNullOrWhiteSpace(AplicarPorcentaje) && cantidad > 0 || !string.IsNullOrWhiteSpace(AplicarCantidad) && cantidad > 0)
                        {
                            CargarDescuento(cantidadComprada.nuevaCantidad);
                        }

                    }

                    SeCambioCantidad = false;
                }

                // Descuento
                if (columnaCellClick.Equals(8))
                {
                    if (FormPrincipal.userNickName.Contains('@'))
                    {
                        var datos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Ventas");
                        if (datos[18].Equals(0))
                        {
                            Utilidades.MensajePermiso();
                            return;
                        }
                    }
                    //txtBuscadorProducto.Text = string.Empty;
                    //txtBuscadorProducto.Focus();

                    if (!DGVentas.CurrentCell.Equals(null) && !DGVentas.CurrentCell.Value.Equals(null))
                    {
                        if (opcion19 == 0)
                        {
                            Utilidades.MensajePermiso();
                            return;
                        }
                        var idProducto = DGVentas.Rows[celdaCellClick].Cells["IDProducto"].Value.ToString();
                        var nombreProducto = DGVentas.Rows[celdaCellClick].Cells["Descripcion"].Value.ToString();
                        var precioProducto = DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString();
                        var cantidadProducto = DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value.ToString();

                        var datos = new string[] { idProducto, nombreProducto, precioProducto, cantidadProducto };

                        using (var formDescuento = new AgregarDescuentoDirecto(datos))
                        {
                            // Aqui comprueba si el producto tiene un descuento directo
                            var quitarDescuento = false;

                            if (descuentosDirectos.ContainsKey(Convert.ToInt32(idProducto)))
                            {
                                quitarDescuento = true;
                                //txtBuscadorProducto.Text = string.Empty;
                                txtBuscadorProducto.Focus();
                            }
                            var resultado = formDescuento.ShowDialog();

                            if (resultado == DialogResult.OK)
                            {
                                enviarVenta.Clear();
                                DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value = formDescuento.TotalDescuento;
                                DGVentas.Rows[celdaCellClick].Cells["TipoDescuento"].Value = formDescuento.TipoDescuento;
                            }
                            else
                            {
                                // Si el producto tenia un descuento directo previamente y detecta al cerrar el form
                                // que el descuento fue eliminado pone por defecto cero en la columna correspondiente
                                if (quitarDescuento)
                                {
                                    if (!descuentosDirectos.ContainsKey(Convert.ToInt32(idProducto)))
                                    {
                                        DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value = "0.00";
                                    }
                                }
                            }
                        }



                        if (yasemando.Equals(false))
                        {
                            enviarVenta.Clear();
                            string Folio = Contenido;
                            string datosCorreoVenta = formaDePagoDeVenta + "|" + cliente + "|" + Folio;
                            foreach (DataGridViewRow articulo in DGVentas.Rows)
                            {
                                enviarVenta.Add(articulo.Cells["Cantidad"].Value.ToString() + "|" + articulo.Cells["Precio"].Value.ToString() + "|" + articulo.Cells["Descripcion"].Value.ToString() + "|" + articulo.Cells["Descuento"].Value.ToString() + "|" + articulo.Cells["Importe"].Value.ToString() + "|" + datosCorreoVenta + "|" + cAnticipo.Text.Trim() + "|" + cAnticipoUtilizado.Text.Trim() + "|" + cDescuento.Text.Trim());
                            }
                            yasemando = true;
                        }

                        SendKeys.Send("{ENTER}");
                        SendKeys.Send("{ENTER}");
                    }
                }
                // Agregar multiple
                if (columnaCellClick.Equals(10))
                {
                    contadorChangeValue = 0;
                    if (!DGVentas.CurrentCell.Equals(null) && !DGVentas.CurrentCell.Value.Equals(null))
                    {
                        indiceFila = e.RowIndex;

                        AgregarMultiplesProductos agregarMultiple = new AgregarMultiplesProductos();

                        agregarMultiple.FormClosed += delegate
                        {
                            AgregarMultiplesProductos();
                            agregarMultiple.Dispose();
                        };

                        agregarMultiple.ShowDialog();
                    }
                }

                // Agregar individual
                if (columnaCellClick.Equals(11))
                {
                    contadorChangeValue = 0;
                    if (!DGVentas.CurrentCell.Equals(null) && !DGVentas.CurrentCell.Value.Equals(null))
                    {
                        int idProducto = Convert.ToInt32(DGVentas.Rows[celdaCellClick].Cells["IDProducto"].Value);
                        int tipoDescuento = Convert.ToInt32(DGVentas.Rows[celdaCellClick].Cells["DescuentoTipo"].Value);
                        var precio = decimal.Parse(DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString());
                        CantidadAnteriorEdit = float.Parse(DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value.ToString());

                        var cantidadTexto = DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value.ToString();
                        decimal cantidad = decimal.Parse(cantidadTexto.ToString(), System.Globalization.NumberStyles.Float);


                        var nuevaCantidad = decimal.Parse(cantidad.ToString(), System.Globalization.NumberStyles.Float);
                        nuevaCantidad = nuevaCantidad + 1;

                        cantidad = nuevaCantidad;

                        decimal importe = cantidad * precio;

                        //MessageBox.Show(importe.ToString());
                        // Verificar si tiene descuento directo 
                        if (descuentosDirectos.ContainsKey(idProducto))
                        {
                            var tipoDescuentoDirecto = descuentosDirectos[idProducto].Item1;

                            // Si el descuento directo es por descuento
                            if (tipoDescuentoDirecto == 2)
                            {
                                decimal porcentaje = (decimal)descuentosDirectos[idProducto].Item2;

                                var descuentoTmp = (precio * cantidad) * (porcentaje / 100);
                                var importeTmp = (precio * cantidad) - descuentoTmp;

                                importe = importeTmp;
                                DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value = $"{descuentoTmp.ToString("N2")} - {porcentaje}%";
                            }
                        }

                        DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value = cantidad;
                        DGVentas.Rows[celdaCellClick].Cells["Importe"].Value = importe;

                        if (tipoDescuento > 0)
                        {
                            string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                            if (!datosDescuento.Equals(null) && datosDescuento.Length > 0)
                            {
                                CalcularDescuento(datosDescuento, tipoDescuento, (int)cantidad, celdaCellClick);
                            }
                        }
                        reproducirProductoAgregado();
                        txtBuscadorProducto.Focus();
                    }
                }

                // Restar individual
                if (columnaCellClick.Equals(12))
                {
                    if (DGVentas.Rows.Count.Equals(1))
                    {
                        PorcentajeDescuento = "";
                        AplicarCantidad = "";
                        AplicarPorcentaje = "";
                    }
                    contadorChangeValue = 0;
                    if (!DGVentas.CurrentCell.Equals(null) && !DGVentas.CurrentCell.Value.Equals(null))
                    {
                        var cantidadTexto = DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value.ToString();
                        decimal cantidad = decimal.Parse(cantidadTexto.ToString(), System.Globalization.NumberStyles.Float);
                        //cantidad -= 1;

                        var nuevaCantidad = decimal.Parse(cantidad.ToString(), System.Globalization.NumberStyles.Float);
                        nuevaCantidad = nuevaCantidad - 1;
                        if (nuevaCantidad > 0)
                        {
                            fechaSistema = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                            if (primerClickRestarIndividual.Equals(false))
                            {

                                // MIRI.
                                // Primero debe eliminar de la cadena el porcentaje del descuento para que no de error al momento de convertirlo a decimal.
                                var cad_descuento = DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString();
                                var res = cad_descuento.IndexOf("-");
                                decimal descuento_conv = 0;

                                if (res >= 0)
                                {
                                    var d = cad_descuento.Split('-');
                                    descuento_conv = Convert.ToDecimal(d[0]);
                                }
                                else
                                {
                                    descuento_conv = Convert.ToDecimal(DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString());
                                }

                                productoRestado.Add("1|" + DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString() + "|" + ((1 * Convert.ToDecimal(DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString())) - descuento_conv));
                                primerClickRestarIndividual = true;
                            }
                            else
                            {
                                string[] word;
                                string palabra = string.Empty;
                                bool descripcionEncontrada = false;
                                palabra = DGVentas.Rows[celdaCellClick].Cells["Descripcion"].Value.ToString();
                                for (int i = 0; i < productoRestado.Count; i++)
                                {
                                    word = productoRestado[i].Split('|');
                                    descripcionEncontrada = Array.Exists(word, element => element == palabra);
                                    if (descripcionEncontrada)
                                    {
                                        var count = Convert.ToDecimal(word[0].ToString());
                                        count++;

                                        // MIRI.
                                        // Primero debe eliminar de la cadena el porcentaje del descuento para que no de error al momento de convertirlo a decimal.
                                        var cad_descuento = DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString();
                                        var res = cad_descuento.IndexOf("-");
                                        decimal descuento_conv = 0;

                                        if (res >= 0)
                                        {
                                            var d = cad_descuento.Split('-');
                                            descuento_conv = Convert.ToDecimal(d[0]);
                                        }
                                        else
                                        {
                                            descuento_conv = Convert.ToDecimal(DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString());
                                        }

                                        productoRestado[i] = count + "|" + DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString() + "|" + ((count * Convert.ToDecimal(DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString())) - descuento_conv);
                                        break;
                                    }
                                }

                                if (!descripcionEncontrada)
                                {
                                    productoRestado.Add("1|" + DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString() + "|" + ((1 * Convert.ToDecimal(DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString())) - Convert.ToDecimal(DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString())));
                                }
                            }

                            int idProducto = Convert.ToInt32(DGVentas.Rows[celdaCellClick].Cells["IDProducto"].Value);
                            int tipoDescuento = Convert.ToInt32(DGVentas.Rows[celdaCellClick].Cells["DescuentoTipo"].Value);
                            var precio = decimal.Parse(DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString());

                            decimal importe = nuevaCantidad * precio;

                            // Verificar si tiene descuento directo
                            if (descuentosDirectos.ContainsKey(idProducto))
                            {
                                var tipoDescuentoDirecto = descuentosDirectos[idProducto].Item1;

                                // Si el descuento directo es el de cantidad
                                if (tipoDescuentoDirecto == 1)
                                {
                                    var descuento = decimal.Parse(DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString());

                                    // Cuando cantidad de producto sea igual a 1 hara esto
                                    if (nuevaCantidad == 1)
                                    {
                                        // Si el importe es menor al descuento debe terminar la operacion
                                        if (importe < descuento)
                                        {
                                            DGVentas.ClearSelection();
                                            MessageBox.Show("El descuento no puede ser mayor al precio del producto", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                    }
                                }

                                // Si el descuento directo es por descuento
                                if (tipoDescuentoDirecto == 2)
                                {
                                    var porcentaje = Convert.ToDecimal(descuentosDirectos[idProducto].Item2);

                                    var descuentoTmp = (precio * nuevaCantidad) * (porcentaje / 100);
                                    var importeTmp = (precio * nuevaCantidad) - descuentoTmp;

                                    importe = importeTmp;
                                    DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value = $"{descuentoTmp.ToString("N2")} - {porcentaje}%";
                                }
                            }

                            DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value = nuevaCantidad;
                            DGVentas.Rows[celdaCellClick].Cells["Importe"].Value = importe;

                            if (tipoDescuento > 0)
                            {
                                string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                                if (!datosDescuento.Equals(null) && datosDescuento.Length > 0)
                                {
                                    CalcularDescuento(datosDescuento, tipoDescuento, (int)nuevaCantidad, celdaCellClick);
                                }
                            }

                            noSeBorroFila = true;
                        }
                        else
                        {
                            var idProducto = Convert.ToInt32(DGVentas.Rows[celdaCellClick].Cells["IDProducto"].Value);

                            var datosConfig = mb.ComprobarConfiguracion();

                            if (datosConfig.Count > 0)
                            {
                                if (Convert.ToInt32(datosConfig[19]).Equals(1))
                                {
                                    string precio = DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString();
                                    string productoEliminadoCorreo = DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Importe"].Value.ToString();
                                    Thread EliminarProducto = new Thread(
                                        () => Utilidades.CorreoElimitarVentasConLaX(productoEliminadoCorreo, fechaSistema, precio, FormPrincipal.datosUsuario));
                                    EliminarProducto.Start();
                                }
                            }
                            DGVentas.Rows.RemoveAt(celdaCellClick);

                            if (DGVentas.Rows.Count == 0)
                            {
                                btnCSV.Enabled = true;
                            }

                            if (productosDescuentoG.ContainsKey(idProducto))
                            {
                                productosDescuentoG.Remove(idProducto);
                            }

                            if (descuentosDirectos.ContainsKey(idProducto))
                            {
                                descuentosDirectos.Remove(idProducto);
                            }


                            noSeBorroFila = false;
                        }
                    }
                    reproducirProductoAgregado();
                    txtBuscadorProducto.Focus();
                }

                // Eliminar individual
                if (columnaCellClick.Equals(13))
                {
                    if (DGVentas.Rows.Count.Equals(1))
                    {
                        PorcentajeDescuento = "";
                        AplicarCantidad = "";
                        AplicarPorcentaje = "";
                    }

                    if (!DGVentas.CurrentCell.Equals(null) && !DGVentas.CurrentCell.Value.Equals(null))
                    {
                        var idProducto = Convert.ToInt32(DGVentas.Rows[celdaCellClick].Cells["IDProducto"].Value);

                        listaMensajesEnviados.Remove(idProducto);

                        fechaSistema = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                        if (primerClickEliminarIndividual.Equals(false))
                        {
                            productoEliminado.Add(DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Importe"].Value.ToString());
                            primerClickEliminarIndividual = true;

                            var datosConfig = mb.ComprobarConfiguracion();

                            if (datosConfig.Count > 0)
                            {
                                if (Convert.ToInt32(datosConfig[19]).Equals(1))
                                {
                                    string precio = DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString();
                                    string productoEliminadoCorreo = DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Importe"].Value.ToString();
                                    Thread EliminarProducto = new Thread(
                                        () => Utilidades.CorreoElimitarVentasConLaX(productoEliminadoCorreo, fechaSistema, precio, FormPrincipal.datosUsuario));
                                    EliminarProducto.Start();
                                }
                            }

                            //string precio = DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString();
                            //string productoEliminadoCorreo = DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Importe"].Value.ToString();
                            //Thread EliminarProducto = new Thread(
                            //    () => Utilidades.CorreoElimitarVentasConLaX(productoEliminadoCorreo, fechaSistema, precio, FormPrincipal.datosUsuario));
                            //EliminarProducto.Start();
                        }
                        else
                        {
                            string[] word;
                            string palabra = string.Empty;
                            bool descripcionEncontrada = false;
                            palabra = DGVentas.Rows[celdaCellClick].Cells["Descripcion"].Value.ToString();

                            for (int i = 0; i < productoEliminado.Count; i++)
                            {
                                word = productoEliminado[i].Split('|');
                                descripcionEncontrada = Array.Exists(word, element => element == palabra);
                                if (descripcionEncontrada)
                                {
                                    var count = Convert.ToDecimal(DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value.ToString());
                                    count++;
                                    productoEliminado[i] = count + "|" + DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString() + "|" + ((count * Convert.ToDecimal(DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString())) - Convert.ToDecimal("0.00"));
                                    break;
                                }
                            }


                            if (!descripcionEncontrada)
                            {
                                var count = Decimal.Parse(DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value.ToString(), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
                                //var count = Convert.ToDecimal(DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value.ToString());

                                // MIRI.
                                // Primero debe eliminar de la cadena el porcentaje del descuento para que no de error al momento de convertirlo a decimal.
                                var cad_descuento = DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString();
                                var res = cad_descuento.IndexOf("-");
                                decimal descuento_conv = 0;

                                if (res >= 0)
                                {
                                    var d = cad_descuento.Split('-');
                                    descuento_conv = Convert.ToDecimal(d[0]);
                                }
                                else
                                {
                                    descuento_conv = Convert.ToDecimal(DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString());
                                }

                                productoEliminado.Add(count + "|" + DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString() + "|" + ((count * Convert.ToDecimal(DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString())) - descuento_conv));
                                //productoEliminado.Add(count + "|" + DGVentas.Rows[celda].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Descuento"].Value.ToString() + "|" + ((count * Convert.ToDecimal(DGVentas.Rows[celda].Cells["Precio"].Value.ToString())) - Convert.ToDecimal(DGVentas.Rows[celda].Cells["Descuento"].Value.ToString())));

                            }

                            var datosConfig = mb.ComprobarConfiguracion();

                            if (datosConfig.Count > 0)
                            {
                                if (Convert.ToInt32(datosConfig[19]).Equals(1))
                                {
                                    string precio = DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString();
                                    string productoEliminadoCorreo = DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Importe"].Value.ToString();
                                    Thread EliminarProducto = new Thread(
                                        () => Utilidades.CorreoElimitarVentasConLaX(productoEliminadoCorreo, fechaSistema, precio, FormPrincipal.datosUsuario));
                                    EliminarProducto.Start();
                                }
                            }

                            //string precio = DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString();
                            //string productoEliminadoCorreo = DGVentas.Rows[celdaCellClick].Cells["Cantidad"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Descuento"].Value.ToString() + "|" + DGVentas.Rows[celdaCellClick].Cells["Importe"].Value.ToString();
                            //Thread EliminarProducto = new Thread(() =>
                            //Utilidades.CorreoElimitarVentasConLaX(productoEliminadoCorreo, fechaSistema, precio, FormPrincipal.datosUsuario));
                            //EliminarProducto.Start();
                        }

                        DGVentas.Rows.RemoveAt(celdaCellClick);

                        noSeBorroFila = false;

                        if (productosDescuentoG.ContainsKey(idProducto))
                        {
                            productosDescuentoG.Remove(idProducto);
                        }

                        if (descuentosDirectos.ContainsKey(idProducto))
                        {
                            descuentosDirectos.Remove(idProducto);
                        }
                    }
                    if (DGVentas.Rows.Count == 0)
                    {
                        btnCSV.Enabled = true;
                    }
                    txtBuscadorProducto.Focus();

                }

                if (!DGVentas.Rows.Count.Equals(0) && !columnaCellClick.Equals(5))
                {
                    if (noSeBorroFila)
                    {
                        DGVentas.Rows[celdaCellClick].Selected = true;
                        DGVentas.ClearSelection();
                    }
                    else
                    {
                        limpiarImagenDelProducto();
                    }
                }

                CalculoMayoreo();
                CantidadesFinalesVenta();
                //try
                //{
                //    var celda = DGVentas.CurrentCell.RowIndex;

                //// Cantidad
                //if (e.ColumnIndex == 5)
                //{
                //    DGVentas.Rows[celda].Cells["Cantidad"].ReadOnly = false;
                //    cantidadAnterior = Convert.ToDecimal(DGVentas.Rows[celda].Cells["Cantidad"].Value.ToString());
                //}

                //// Descuento
                //if (e.ColumnIndex == 8)
                //{
                //    DGVentas.Rows[celda].Cells["Descuento"].ReadOnly = false;

                //    var idProducto = DGVentas.Rows[celda].Cells["IDProducto"].Value.ToString();
                //    var nombreProducto = DGVentas.Rows[celda].Cells["Descripcion"].Value.ToString();
                //    var precioProducto = DGVentas.Rows[celda].Cells["Precio"].Value.ToString();
                //    var cantidadProducto = DGVentas.Rows[celda].Cells["Cantidad"].Value.ToString();

                //    var datos = new string[] { idProducto, nombreProducto, precioProducto, cantidadProducto };

                //    using (var formDescuento = new AgregarDescuentoDirecto(datos))
                //    {
                //        // Aqui comprueba si el producto tiene un descuento directo
                //        var quitarDescuento = false;

                //        if (descuentosDirectos.ContainsKey(Convert.ToInt32(idProducto)))
                //        {
                //            quitarDescuento = true;
                //        }

                //        var resultado = formDescuento.ShowDialog();

                //        if (resultado == DialogResult.OK)
                //        {
                //            DGVentas.Rows[celda].Cells["Descuento"].Value = formDescuento.TotalDescuento;
                //            DGVentas.Rows[celda].Cells["TipoDescuento"].Value = formDescuento.TipoDescuento;
                //        }
                //        else
                //        {
                //            // Si el producto tenia un descuento directo previamente y detecta al cerrar el form
                //            // que el descuento fue eliminado pone por defecto cero en la columna correspondiente
                //            if (quitarDescuento)
                //            {
                //                if (!descuentosDirectos.ContainsKey(Convert.ToInt32(idProducto)))
                //                {
                //                    DGVentas.Rows[celda].Cells["Descuento"].Value = "0.00";
                //                }
                //            }
                //        }
                //    }
                //}

                //// Agregar multiple
                //if (e.ColumnIndex == 10)
                //{
                //    indiceFila = e.RowIndex;

                //    AgregarMultiplesProductos agregarMultiple = new AgregarMultiplesProductos();

                //    agregarMultiple.FormClosed += delegate
                //    {
                //        AgregarMultiplesProductos();
                //        agregarMultiple.Dispose();
                //    };

                //    agregarMultiple.ShowDialog();
                //}

                //// Agregar individual
                //if (e.ColumnIndex == 11)
                //{
                //    int idProducto = Convert.ToInt32(DGVentas.Rows[celda].Cells["IDProducto"].Value);
                //    int tipoDescuento = Convert.ToInt32(DGVentas.Rows[celda].Cells["DescuentoTipo"].Value);
                //    var precio = float.Parse(DGVentas.Rows[celda].Cells["Precio"].Value.ToString());
                //    int cantidad = Convert.ToInt32(DGVentas.Rows[celda].Cells["Cantidad"].Value) + 1;

                //    float importe = cantidad * precio;

                //    // Verificar si tiene descuento directo
                //    if (descuentosDirectos.ContainsKey(idProducto))
                //    {
                //        var tipoDescuentoDirecto = descuentosDirectos[idProducto].Item1;

                //        // Si el descuento directo es por descuento
                //        if (tipoDescuentoDirecto == 2)
                //        {
                //            var porcentaje = descuentosDirectos[idProducto].Item2;

                //            var descuentoTmp = (precio * cantidad) * (porcentaje / 100);
                //            var importeTmp = (precio * cantidad) - descuentoTmp;

                //            importe = importeTmp;
                //            DGVentas.Rows[celda].Cells["Descuento"].Value = $"{descuentoTmp.ToString("N2")} - {porcentaje}%";
                //        }
                //    }

                //    DGVentas.Rows[celda].Cells["Cantidad"].Value = cantidad;
                //    DGVentas.Rows[celda].Cells["Importe"].Value = importe;

                //    if (tipoDescuento > 0)
                //    {
                //        string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                //        CalcularDescuento(datosDescuento, tipoDescuento, cantidad, celda);
                //    }
                //}

                //// Restar individual
                //if (e.ColumnIndex == 12)
                //{
                //    int cantidad = Convert.ToInt32(DGVentas.Rows[celda].Cells["Cantidad"].Value);
                //    cantidad -= 1;

                //    if (cantidad > 0)
                //    {
                //        fechaSistema = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                //        if (primerClickRestarIndividual.Equals(false))
                //        {
                //            productoRestado.Add("1|" + DGVentas.Rows[celda].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Descuento"].Value.ToString() + "|" + ((1*Convert.ToDecimal(DGVentas.Rows[celda].Cells["Precio"].Value.ToString())) - Convert.ToDecimal(DGVentas.Rows[celda].Cells["Descuento"].Value.ToString())));
                //            primerClickRestarIndividual = true;
                //        }
                //        else
                //        {
                //            string[] word;
                //            string palabra = string.Empty;
                //            bool descripcionEncontrada = false;
                //            palabra = DGVentas.Rows[celda].Cells["Descripcion"].Value.ToString();
                //            for (int i = 0; i < productoRestado.Count; i++)
                //            {
                //                word = productoRestado[i].Split('|');
                //                descripcionEncontrada = Array.Exists(word, element => element == palabra);
                //                if (descripcionEncontrada)
                //                {
                //                    var count = Convert.ToDecimal(word[0].ToString());
                //                    count++;
                //                    productoRestado[i] = count + "|" + DGVentas.Rows[celda].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Descuento"].Value.ToString() + "|" + ((count * Convert.ToDecimal(DGVentas.Rows[celda].Cells["Precio"].Value.ToString())) - Convert.ToDecimal(DGVentas.Rows[celda].Cells["Descuento"].Value.ToString()));
                //                    break;
                //                }
                //            }

                //            if (!descripcionEncontrada)
                //            {
                //                productoRestado.Add("1|" + DGVentas.Rows[celda].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Descuento"].Value.ToString() + "|" + ((1 * Convert.ToDecimal(DGVentas.Rows[celda].Cells["Precio"].Value.ToString())) - Convert.ToDecimal(DGVentas.Rows[celda].Cells["Descuento"].Value.ToString())));
                //            }
                //        }

                //        int idProducto = Convert.ToInt32(DGVentas.Rows[celda].Cells["IDProducto"].Value);
                //        int tipoDescuento = Convert.ToInt32(DGVentas.Rows[celda].Cells["DescuentoTipo"].Value);
                //        var precio = float.Parse(DGVentas.Rows[celda].Cells["Precio"].Value.ToString());

                //        float importe = cantidad * precio;

                //        // Verificar si tiene descuento directo
                //        if (descuentosDirectos.ContainsKey(idProducto))
                //        {
                //            var tipoDescuentoDirecto = descuentosDirectos[idProducto].Item1;

                //            // Si el descuento directo es el de cantidad
                //            if (tipoDescuentoDirecto == 1)
                //            {
                //                var descuento = float.Parse(DGVentas.Rows[celda].Cells["Descuento"].Value.ToString());

                //                // Cuando cantidad de producto sea igual a 1 hara esto
                //                if (cantidad == 1)
                //                {
                //                    // Si el importe es menor al descuento debe terminar la operacion
                //                    if (importe < descuento)
                //                    {
                //                        DGVentas.ClearSelection();
                //                        MessageBox.Show("El descuento no puede ser mayor al precio del producto", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                        return;
                //                    }
                //                }
                //            }

                //            // Si el descuento directo es por descuento
                //            if (tipoDescuentoDirecto == 2)
                //            {
                //                var porcentaje = descuentosDirectos[idProducto].Item2;

                //                var descuentoTmp = (precio * cantidad) * (porcentaje / 100);
                //                var importeTmp = (precio * cantidad) - descuentoTmp;

                //                importe = importeTmp;
                //                DGVentas.Rows[celda].Cells["Descuento"].Value = $"{descuentoTmp.ToString("N2")} - {porcentaje}%";
                //            }
                //        }

                //        DGVentas.Rows[celda].Cells["Cantidad"].Value = cantidad;
                //        DGVentas.Rows[celda].Cells["Importe"].Value = importe;

                //        if (tipoDescuento > 0)
                //        {
                //            string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                //            CalcularDescuento(datosDescuento, tipoDescuento, cantidad, celda);
                //        }
                //    }
                //    else
                //    {
                //        var idProducto = Convert.ToInt32(DGVentas.Rows[celda].Cells["IDProducto"].Value);

                //        DGVentas.Rows.RemoveAt(celda);

                //        if (productosDescuentoG.ContainsKey(idProducto))
                //        {
                //            productosDescuentoG.Remove(idProducto);
                //        }

                //        if (descuentosDirectos.ContainsKey(idProducto))
                //        {
                //            descuentosDirectos.Remove(idProducto);
                //        }
                //    }
                //}

                //// Eliminar individual
                //if (e.ColumnIndex == 13)
                //{
                //    var idProducto = Convert.ToInt32(DGVentas.Rows[celda].Cells["IDProducto"].Value);

                //    fechaSistema = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                //    if (primerClickEliminarIndividual.Equals(false))
                //    {
                //        productoEliminado.Add(DGVentas.Rows[celda].Cells["Cantidad"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Descuento"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Importe"].Value.ToString());
                //        primerClickEliminarIndividual = true;
                //    }
                //    else
                //    {
                //        string[] word;
                //        string palabra = string.Empty;
                //        bool descripcionEncontrada = false;
                //        palabra = DGVentas.Rows[celda].Cells["Descripcion"].Value.ToString();

                //        for (int i = 0; i < productoEliminado.Count; i++)
                //        {
                //            word = productoEliminado[i].Split('|');
                //            descripcionEncontrada = Array.Exists(word, element => element == palabra);
                //            if (descripcionEncontrada)
                //            {
                //                var count = Convert.ToDecimal(DGVentas.Rows[celda].Cells["Cantidad"].Value.ToString());
                //                count++;
                //                productoEliminado[i] = count + "|" + DGVentas.Rows[celda].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Descuento"].Value.ToString() + "|" + ((count * Convert.ToDecimal(DGVentas.Rows[celda].Cells["Precio"].Value.ToString())) - Convert.ToDecimal(DGVentas.Rows[celda].Cells["Descuento"].Value.ToString()));
                //                break;
                //            }
                //        }

                //        if (!descripcionEncontrada)
                //        {
                //            var count = Convert.ToDecimal(DGVentas.Rows[celda].Cells["Cantidad"].Value.ToString());
                //            productoEliminado.Add(count + "|" + DGVentas.Rows[celda].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[celda].Cells["Descuento"].Value.ToString() + "|" + ((count * Convert.ToDecimal(DGVentas.Rows[celda].Cells["Precio"].Value.ToString())) - Convert.ToDecimal(DGVentas.Rows[celda].Cells["Descuento"].Value.ToString())));
                //        }
                //    }

                //    DGVentas.Rows.RemoveAt(celda);

                //    if (productosDescuentoG.ContainsKey(idProducto))
                //    {
                //        productosDescuentoG.Remove(idProducto);
                //    }

                //    if (descuentosDirectos.ContainsKey(idProducto))
                //    {
                //        descuentosDirectos.Remove(idProducto);
                //    }
                //}
                //}
                //catch (Exception)
                //{

                //}

                //DGVentas.ClearSelection();
                //CalculoMayoreo();
                //CantidadesFinalesVenta();
                listaProductosVenta();
            }
        }

        private void CargarDescuento(decimal cantidad)
        {
            if (!DGVentas.CurrentCell.Equals(null) && !DGVentas.CurrentCell.Value.Equals(null))
            {
                if (opcion19 == 0)
                {
                    Utilidades.MensajePermiso();
                    return;
                }
                var idProducto = DGVentas.Rows[0].Cells["IDProducto"].Value.ToString();
                var nombreProducto = DGVentas.Rows[0].Cells["Descripcion"].Value.ToString();
                var precioProducto = DGVentas.Rows[0].Cells["Precio"].Value.ToString();
                var cantidadProducto = DGVentas.Rows[0].Cells["Cantidad"].Value.ToString();

                var datos = new string[] { idProducto, nombreProducto, precioProducto, cantidad.ToString() };

                using (var formDescuento = new AgregarDescuentoDirecto(datos))
                {
                    // Aqui comprueba si el producto tiene un descuento directo
                    var quitarDescuento = false;

                    if (descuentosDirectos.ContainsKey(Convert.ToInt32(idProducto)))
                    {
                        quitarDescuento = true;
                        //txtBuscadorProducto.Text = string.Empty;
                        txtBuscadorProducto.Focus();
                    }
                    var resultado = formDescuento.ShowDialog();
                    if (resultado == DialogResult.OK)
                    {
                        enviarVenta.Clear();
                        DGVentas.Rows[0].Cells["Descuento"].Value = formDescuento.TotalDescuento;
                        DGVentas.Rows[0].Cells["TipoDescuento"].Value = formDescuento.TipoDescuento;
                    }
                    else
                    {
                        // Si el producto tenia un descuento directo previamente y detecta al cerrar el form
                        // que el descuento fue eliminado pone por defecto cero en la columna correspondiente
                        if (quitarDescuento)
                        {
                            if (!descuentosDirectos.ContainsKey(Convert.ToInt32(idProducto)))
                            {
                                DGVentas.Rows[0].Cells["Descuento"].Value = "0.00";
                            }
                        }
                    }
                }
                SendKeys.Send("{ENTER}");
                SendKeys.Send("{ENTER}");

                Ventas.SeCambioCantidad = false;
            }
        }

        private void AgregarMultiplesProductos()
        {
            int idProducto = Convert.ToInt32(DGVentas.Rows[indiceFila].Cells["IDProducto"].Value);
            int tipoDescuento = Convert.ToInt32(DGVentas.Rows[indiceFila].Cells["DescuentoTipo"].Value);
            var precio = float.Parse(DGVentas.Rows[indiceFila].Cells["Precio"].Value.ToString());
            int cantidad = Convert.ToInt32(DGVentas.Rows[indiceFila].Cells["Cantidad"].Value) + cantidadFila;

            float importe = cantidad * precio;

            // Verificar si tiene descuento directo
            if (descuentosDirectos.ContainsKey(idProducto))
            {
                var tipoDescuentoDirecto = descuentosDirectos[idProducto].Item1;

                // Si el descuento directo es por descuento
                if (tipoDescuentoDirecto == 2)
                {
                    var porcentaje = descuentosDirectos[idProducto].Item2;

                    var descuentoTmp = (precio * cantidad) * (porcentaje / 100);
                    var importeTmp = (precio * cantidad) - descuentoTmp;

                    importe = importeTmp;
                    DGVentas.Rows[indiceFila].Cells["Descuento"].Value = $"{descuentoTmp.ToString("N2")} - {porcentaje}%";
                }
            }

            DGVentas.Rows[indiceFila].Cells["Cantidad"].Value = cantidad;
            DGVentas.Rows[indiceFila].Cells["Importe"].Value = importe;

            if (tipoDescuento > 0)
            {
                string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                if (!datosDescuento.Equals(null) && datosDescuento.Length > 0)
                {
                    CalcularDescuento(datosDescuento, tipoDescuento, cantidad, indiceFila);
                }
            }

            indiceFila = 0;
            cantidadFila = 0;
        }

        private void CalcularDescuento(string[] datosDescuento, int tipo, decimal cantidad, int fila = 0)
        {
            if (!datosDescuento.Equals(null) && datosDescuento.Length > 0)
            {
                //Cliente
                if (tipo == 1)
                {
                    var descuento = datosDescuento[0].Split('-');
                    var precioAux = Convert.ToDecimal(descuento[0]) * Convert.ToDecimal(cantidad);
                    var porcentajeAux = Convert.ToDecimal(descuento[1]);

                    var descuentoFinal = precioAux * (porcentajeAux / 100);

                    DGVentas.Rows[fila].Cells["Descuento"].Value = descuentoFinal;
                    DGVentas.Rows[fila].Cells["Importe"].Value = precioAux;
                }

                //Mayoreo
                if (tipo == 2)
                {
                    decimal totalImporte = 0;

                    // Esta variable almace el ultimo checkbox al que se le marco casilla en el descuento
                    var ultimoCheckbox = string.Empty;

                    List<string> tomarDescuentos = new List<string>();
                    string ultimoCheck = string.Empty;
                    var precioUltimoDescuento = string.Empty;
                    bool menorACero = false;
                    foreach (var descuento in datosDescuento.Reverse())
                    {
                        var descIndividual = descuento.Split('-');


                        if (cantidad >= Convert.ToDecimal(descIndividual[0]))
                        {
                            tomarDescuentos.Add(descuento);

                            precioUltimoDescuento = descIndividual[2];

                            if (descIndividual[3] == "1")
                            {
                                ultimoCheck = descuento;
                            }
                            menorACero = true;
                        }
                        else
                        {
                            if (menorACero.Equals(false))
                            {
                                precioUltimoDescuento = descIndividual[2];
                                menorACero = true;
                            }

                        }
                    }

                    var auxDatos = tomarDescuentos.ToArray().Reverse();
                    tomarDescuentos = auxDatos.ToList();

                    foreach (var descuento in tomarDescuentos)
                    {
                        var descIndividual = descuento.Split('-');

                        if (cantidad >= Convert.ToDecimal(descIndividual[0]) && descIndividual[3] == "0")
                        {
                            //restantes = cantidad;
                            decimal diferencia = Math.Abs(cantidad - Convert.ToInt32(descIndividual[0]));
                            totalImporte += diferencia * Convert.ToDecimal(descIndividual[2]);
                            primeraCantidad = cantidad;
                            cantidad = cantidad - diferencia;
                        }

                        if (cantidad >= Convert.ToDecimal(descIndividual[0]) && descIndividual[3] == "1")
                        {
                            var datosUltimoCheck = ultimoCheck.Split('-');

                            decimal diferencia = Math.Abs(cantidad - Convert.ToInt32(descIndividual[0]));
                            totalImporte += diferencia * Convert.ToDecimal(datosUltimoCheck[2]);
                            cantidad = cantidad - diferencia;
                        }
                    }

                    if (cantidad == 1)
                    {
                        totalImporte += cantidad * Convert.ToDecimal(precioUltimoDescuento);
                    }
                    float importe = float.Parse(DGVentas.Rows[fila].Cells["Importe"].Value.ToString());
                    float descuentoFinal = importe - float.Parse(totalImporte.ToString("0.00"));

                    if (primeraCantidad > 0 && primeraCantidad < 1)
                    {
                        totalImporte += primeraCantidad * Convert.ToDecimal(precioUltimoDescuento);
                        importe = float.Parse(DGVentas.Rows[fila].Cells["Importe"].Value.ToString());
                        descuentoFinal = importe - float.Parse(totalImporte.ToString("0.00"));
                    }



                    if (descuentoFinal < 0)
                    {
                        descuentoFinal = 0;
                    }

                    DGVentas.Rows[fila].Cells["Descuento"].Value = descuentoFinal.ToString("0.00");
                    DGVentas.Rows[fila].Cells["Importe"].Value = totalImporte;
                }
            }
        }

        private void CantidadesFinalesVenta()
        {
            decimal totalArticulos = 0;
            double totalImporte = 0;
            double totalImporte8 = 0;
            double totalDescuento = 0;
            double totalSubtotal = 0;
            double totalSubtotal8 = 0;
            double totalIVA16 = 0;
            double totalIVA8 = 0;
            double totalAnticipos = 0;
            double totalOtrosImpuestos = 0;
            double total_impuestos_retenidos = 0;
            double total_importe_cero_exe = 0;
            double total_subtotal_cero_exe = 0;

            foreach (DataGridViewRow fila in DGVentas.Rows)
            {
                var idProducto = Convert.ToInt32(fila.Cells["IDProducto"].Value);
                var porcentajeGeneralAux = 0f;
                var descuentoClienteAux = 0f;
                var esDescuentoDirecto = false;


                // MIRI.
                // Busca si tiene impuestos extra (Traslado, Retención, Locales).

                DataTable dbusca_impuestos = cn.CargarDatos(cs.cargar_impuestos_en_editar_producto(idProducto));

                if (dbusca_impuestos.Rows.Count > 0)
                {
                    double cantidad_producto = Convert.ToDouble(fila.Cells["Cantidad"].Value.ToString());


                    foreach (DataRow rbusca_impuestos in dbusca_impuestos.Rows)
                    {
                        var precioOriginalAux = float.Parse(fila.Cells["PrecioOriginal"].Value.ToString());

                        if (rbusca_impuestos["tipo"].ToString() == "Traslado" | rbusca_impuestos["tipo"].ToString() == "Loc. Traslado")
                        {
                            double impt = cantidad_producto * Convert.ToDouble(rbusca_impuestos["Importe"].ToString());
                            totalOtrosImpuestos += impt;

                            // Se suma el impuesto a las filas de precio, no se el porque
                            // se haga esto pero yo sigo el mismo paso que hacen con el IEPS. 
                            fila.Cells["Precio"].Value = precioOriginalAux + Convert.ToDouble(rbusca_impuestos["Importe"].ToString());
                            fila.Cells["PrecioOriginal"].Value = precioOriginalAux + Convert.ToDouble(rbusca_impuestos["Importe"].ToString());
                        }
                        if (rbusca_impuestos["tipo"].ToString() == "Retención" | rbusca_impuestos["tipo"].ToString() == "Loc. Retenido")
                        {
                            double impr = cantidad_producto * Convert.ToDouble(rbusca_impuestos["Importe"].ToString());
                            total_impuestos_retenidos += impr;

                            // Se suma el impuesto a las filas de precio, no se el porque
                            // se haga esto pero yo sigo el mismo paso que hacen con el IEPS
                            fila.Cells["Precio"].Value = precioOriginalAux - Convert.ToDouble(rbusca_impuestos["Importe"].ToString());
                            fila.Cells["PrecioOriginal"].Value = precioOriginalAux - Convert.ToDouble(rbusca_impuestos["Importe"].ToString());
                        }
                    }
                }



                // Buscar si tiene IEPS para la etiqueta otros impuestos
                /*var ieps = mb.ObtenerImpuestoProducto(idProducto);

                if (ieps > 0)
                {
                    var cantidadAux = float.Parse(fila.Cells["Cantidad"].Value.ToString());

                    //totalOtrosImpuestos += (ieps * cantidadAux);

                    var precioOriginalAux = float.Parse(fila.Cells["PrecioOriginal"].Value.ToString());

                    fila.Cells["Precio"].Value = precioOriginalAux + ieps;
                    fila.Cells["PrecioOriginal"].Value = precioOriginalAux + ieps;
                }*/

                var impuesto = fila.Cells["Impuesto"].Value.ToString();

                if (string.IsNullOrWhiteSpace(impuesto))
                {
                    impuesto = "16%";
                }

                // Obtenemos el descuento individual y lo convertimos en array para dividir la cantidad
                // y el texto del porcentaje aplicado a ese producto en caso de que tenga descuento
                var descuentoIndividual = fila.Cells["Descuento"].Value.ToString().Split('-');

                if (string.IsNullOrWhiteSpace(descuentoIndividual[0]))
                {
                    descuentoIndividual[0] = "0";
                }

                // Si el producto tiene un descuento directo aplicado respaldamos el valor de los descuentos en variables auxiliares
                // de esa manera no tomara en cuenta ese producto con los otros descuentos y aplicara solamente el directo
                if (descuentosDirectos.ContainsKey(idProducto))
                {
                    porcentajeGeneralAux = porcentajeGeneral;
                    descuentoClienteAux = descuentoCliente;
                    porcentajeGeneral = 0;
                    descuentoCliente = 0;
                    esDescuentoDirecto = true;
                }

                if (buscarvVentaGuardada == ".#")
                {
                    var precioOriginal = Convert.ToDouble(fila.Cells["PrecioOriginal"].Value);
                    var cantidadProducto = Convert.ToInt32(fila.Cells["Cantidad"].Value);
                    var cantidadDescuento = Convert.ToDouble(descuentoIndividual[0].Trim());

                    var importeProducto = (precioOriginal * cantidadProducto) - cantidadDescuento;

                    fila.Cells["Importe"].Value = importeProducto.ToString("0.00");

                    if (impuesto.Equals("16%"))
                    {
                        totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
                    }
                    else if (impuesto.Equals("8%"))
                    {
                        totalImporte8 += Convert.ToDouble(fila.Cells["Importe"].Value);
                    }
                    else
                    {
                        // MIRI.
                        // Se agrega para el caso de IVA 0% y exento. 
                        // Si no se crea esta nueva variable entonces el total final mostrará cero porque nunca se le asigna nada a las variables "totalImporte" y "totalImporte8".

                        total_importe_cero_exe += Convert.ToDouble(fila.Cells["Importe"].Value);
                    }

                    //totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
                    totalArticulos += cantidadProducto;
                    totalDescuento += cantidadDescuento;
                }
                else if (porcentajeGeneral > 0)
                {
                    // Obtenemos el valor de ese key
                    var aplicar = productosDescuentoG.FirstOrDefault(x => x.Key == idProducto).Value;

                    // Si el valor obtenido es true hace esto
                    if (aplicar)
                    {
                        // Precio original del producto
                        var precioOriginal = Convert.ToDouble(fila.Cells["PrecioOriginal"].Value);
                        // Cantidad de producto
                        var cantidadProducto = Convert.ToDecimal(fila.Cells["Cantidad"].Value);
                        // Cantidad descuento del producto
                        var cantidadDescuento = Convert.ToDouble(0);
                        // Para saber que tipo de descuento tiene el producto desde que se registro
                        var descuentoTipo = Convert.ToInt16(fila.Cells["DescuentoTipo"].Value);

                        var mensajeDescuento = string.Empty;

                        double descuento = 0;

                        if (descuentoTipo > 0)
                        {
                            var descuentoAux = float.Parse(descuentoIndividual[0]);

                            if (descuentoAux > 0)
                            {
                                var aplicarDescuento = Convert.ToInt16(fila.Cells["AplicarDescuento"].Value);

                                if (aplicarDescuento == 1)
                                {
                                    descuento = descuentoAux;
                                    mensajeDescuento = $"{descuento.ToString("0.00")}";
                                }
                                else
                                {
                                    descuento = descuentoAux;
                                    mensajeDescuento = $"{descuento.ToString("0.00")} - {(porcentajeGeneral * 100)}%";
                                }
                            }
                            else
                            {
                                descuento = (precioOriginal * Convert.ToDouble(cantidadProducto)) - cantidadDescuento;
                                descuento *= porcentajeGeneral;
                                mensajeDescuento = $"{descuento.ToString("0.00")} - {(porcentajeGeneral * 100)}%";
                            }
                        }
                        else
                        {
                            descuento = (precioOriginal * Convert.ToDouble(cantidadProducto)) - cantidadDescuento;
                            descuento *= porcentajeGeneral;
                            mensajeDescuento = $"{descuento.ToString("0.00")} - {(porcentajeGeneral * 100)}%";
                        }

                        var importeProducto = precioOriginal * Convert.ToDouble(cantidadProducto);

                        importeProducto -= descuento;
                        importeProducto -= cantidadDescuento;

                        fila.Cells["Descuento"].Value = mensajeDescuento;
                        fila.Cells["Importe"].Value = importeProducto.ToString("0.00");

                        if (impuesto.Equals("16%"))
                        {
                            totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
                        }
                        else if (impuesto.Equals("8%"))
                        {
                            totalImporte8 += Convert.ToDouble(fila.Cells["Importe"].Value);
                        }
                        else
                        {
                            // MIRI.
                            // Se agrega para el caso de IVA 0% y exento. 
                            // Si no se crea esta nueva variable entonces el total final mostrará cero porque nunca se le asigna nada a las variables "totalImporte" y "totalImporte8".

                            total_importe_cero_exe += Convert.ToDouble(fila.Cells["Importe"].Value);
                        }

                        //totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
                        totalArticulos += cantidadProducto;
                        totalDescuento += descuento + cantidadDescuento;
                    }
                    else
                    {
                        var precioOriginal = Convert.ToDouble(fila.Cells["PrecioOriginal"].Value);
                        var cantidadProducto = Convert.ToDecimal(fila.Cells["Cantidad"].Value);
                        var cantidadDescuento = Convert.ToDouble(descuentoIndividual[0].Trim());

                        var importeProducto = (precioOriginal * Convert.ToDouble(cantidadProducto)) - cantidadDescuento;

                        fila.Cells["Importe"].Value = importeProducto.ToString("0.00");

                        if (impuesto.Equals("16%"))
                        {
                            totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
                        }
                        else if (impuesto.Equals("8%"))
                        {
                            totalImporte8 += Convert.ToDouble(fila.Cells["Importe"].Value);
                        }
                        else
                        {
                            // MIRI.
                            // Se agrega para el caso de IVA 0% y exento. 
                            // Si no se crea esta nueva variable entonces el total final mostrará cero porque nunca se le asigna nada a las variables "totalImporte" y "totalImporte8".

                            total_importe_cero_exe += Convert.ToDouble(fila.Cells["Importe"].Value);
                        }

                        //totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
                        totalArticulos += cantidadProducto;
                        totalDescuento += cantidadDescuento;
                    }
                }
                else if (descuentoCliente > 0)
                {
                    // Precio original del producto
                    var precioOriginal = Convert.ToDouble(fila.Cells["PrecioOriginal"].Value);
                    // Cantidad de producto
                    var cantidadProducto = Convert.ToDecimal(fila.Cells["Cantidad"].Value);
                    // Cantidad descuento del producto
                    var cantidadDescuento = Convert.ToDouble(0);
                    // Para saber que tipo de descuento tiene el producto desde que se registro
                    var descuentoTipo = Convert.ToInt16(fila.Cells["DescuentoTipo"].Value);

                    var mensajeDescuento = string.Empty;

                    double descuento = 0;

                    if (descuentoTipo > 0)
                    {
                        var descuentoAux = float.Parse(descuentoIndividual[0]);

                        var aplicarDescuento = Convert.ToInt16(fila.Cells["AplicarDescuento"].Value);

                        if (aplicarDescuento == 1)
                        {
                            descuento = descuentoAux;
                            mensajeDescuento = $"{descuento.ToString("0.00")}";
                        }
                        else
                        {
                            descuento = (precioOriginal * Convert.ToDouble(cantidadProducto)) - cantidadDescuento;
                            descuento *= descuentoCliente;
                            mensajeDescuento = $"{descuento.ToString("0.00")} - {(descuentoCliente * 100)}%";
                        }
                    }
                    else
                    {
                        descuento = (precioOriginal * Convert.ToDouble(cantidadProducto)) - cantidadDescuento;
                        descuento *= descuentoCliente;
                        mensajeDescuento = $"{descuento.ToString("0.00")} - {(descuentoCliente * 100)}%";
                    }

                    var importeProducto = precioOriginal * Convert.ToDouble(cantidadProducto);
                    importeProducto -= descuento;
                    importeProducto -= cantidadDescuento;

                    fila.Cells["Descuento"].Value = mensajeDescuento;
                    fila.Cells["Importe"].Value = importeProducto.ToString("0.00");

                    if (impuesto.Equals("16%"))
                    {
                        totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
                    }
                    else if (impuesto.Equals("8%"))
                    {
                        totalImporte8 += Convert.ToDouble(fila.Cells["Importe"].Value);
                    }
                    else
                    {
                        // MIRI.
                        // Se agrega para el caso de IVA 0% y exento. 
                        // Si no se crea esta nueva variable entonces el total final mostrará cero porque nunca se le asigna nada a las variables "totalImporte" y "totalImporte8".

                        total_importe_cero_exe += Convert.ToDouble(fila.Cells["Importe"].Value);
                    }

                    //totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
                    totalArticulos += cantidadProducto;
                    totalDescuento += descuento + cantidadDescuento;
                }
                else
                {
                    var precioOriginal = Convert.ToDouble(fila.Cells["PrecioOriginal"].Value);
                    var cantidadProducto = Convert.ToDecimal(fila.Cells["Cantidad"].Value);
                    var descuentoTipo = Convert.ToInt16(fila.Cells["DescuentoTipo"].Value);

                    double cantidadDescuento = 0;

                    if (esDescuentoDirecto || descuentoTipo > 0)
                    {
                        cantidadDescuento = Convert.ToDouble(descuentoIndividual[0]);
                    }
                    double importeProducto;
                    if (cantidadProducto > 0 && cantidadProducto < 1)
                    {
                        importeProducto = (precioOriginal * Convert.ToDouble(cantidadProducto));
                    }
                    else
                    {
                        importeProducto = (precioOriginal * Convert.ToDouble(cantidadProducto)) - cantidadDescuento;
                    }


                    fila.Cells["Importe"].Value = importeProducto.ToString("0.00");

                    if (impuesto.Equals("16%"))
                    {
                        totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
                    }
                    else if (impuesto.Equals("8%"))
                    {
                        totalImporte8 += Convert.ToDouble(fila.Cells["Importe"].Value);
                    }
                    else
                    {
                        // MIRI.
                        // Se agrega para el caso de IVA 0% y exento. 
                        // Si no se crea esta nueva variable entonces el total final mostrará cero porque nunca se le asigna nada a las variables "totalImporte" y "totalImporte8".

                        total_importe_cero_exe += Convert.ToDouble(fila.Cells["Importe"].Value);
                    }

                    //totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
                    totalArticulos += cantidadProducto;
                    totalDescuento += cantidadDescuento;
                }

                // Reestablecemos el valor de los descuentos diferentes al directo para que siga funcionando
                // normalmente el metodo de calcular los totales
                if (descuentosDirectos.ContainsKey(idProducto))
                {
                    porcentajeGeneral = porcentajeGeneralAux;
                    descuentoCliente = descuentoClienteAux;
                }

                // Se vuelve a restaurar el valor original despues de haber hecho los calculos
                // cuando el producto tiene detalle de facturacion en especifico tiene IEPS
                /*if (ieps > 0)
                {
                    var precioOriginalAux = float.Parse(fila.Cells["PrecioOriginal"].Value.ToString());

                    fila.Cells["PrecioOriginal"].Value = precioOriginalAux - ieps;
                }*/

                // MIRI.
                // Aquí se va a quitar los imuestos extras, no se porque pero sigo el mismo procedimiento que hicieron con el IEPS. 

                //DataTable dbusca_impuestosq = cn.CargarDatos(cs.cargar_impuestos_en_editar_producto(idProducto));

                if (dbusca_impuestos.Rows.Count > 0)
                {
                    foreach (DataRow rbusca_impuestos in dbusca_impuestos.Rows)
                    {
                        var precioOriginalAux = float.Parse(fila.Cells["PrecioOriginal"].Value.ToString());

                        if (rbusca_impuestos["tipo"].ToString() == "Traslado" | rbusca_impuestos["tipo"].ToString() == "Loc. Traslado")
                        {
                            fila.Cells["PrecioOriginal"].Value = precioOriginalAux - Convert.ToDouble(rbusca_impuestos["Importe"].ToString());
                        }
                        if (rbusca_impuestos["tipo"].ToString() == "Retención" | rbusca_impuestos["tipo"].ToString() == "Loc. Retenido")
                        {
                            fila.Cells["PrecioOriginal"].Value = precioOriginalAux + Convert.ToDouble(rbusca_impuestos["Importe"].ToString());
                        }
                    }
                }
            }

            // Calculo del subtotal al 16
            if (totalImporte > 0)
            {
                totalSubtotal = ((totalImporte - totalOtrosImpuestos) + total_impuestos_retenidos) / 1.16;
                totalIVA16 = totalSubtotal * 0.16;
            }

            // Calculo del subtotal al 8
            if (totalImporte8 > 0)
            {
                totalSubtotal8 = ((totalImporte8 - totalOtrosImpuestos) + total_impuestos_retenidos) / 1.08;
                totalIVA8 = totalSubtotal8 * 0.08;
            }

            // Cálculo subtotal, IVA 0% y exento
            if (total_importe_cero_exe > 0)
            {
                total_subtotal_cero_exe = (total_importe_cero_exe - totalOtrosImpuestos) + total_impuestos_retenidos;
            }

            //totalSubtotal = totalImporte / 1.16;
            //totalIVA16    = totalSubtotal * 0.16;

            totalAnticipos = Convert.ToDouble(cAnticipo.Text);
            totalAnticipos += importeAnticipo;


            var sumaImportes = totalImporte + totalImporte8 + total_importe_cero_exe;



            pasarTotalAnticipos = totalAnticipos;
            pasarSumaImportes = sumaImportes;
            //totalAnticipos
            //totalImporte

            var anticipoAplicado = (totalAnticipos - sumaImportes);
            //Este codigo lo movi a DetalleVenta
            //if (totalAnticipos > 0)
            //{
            //    var idAnticipo = ListadoAnticipos.obtenerIdAnticipo;
            //    var consultaImporteOriginal = cn.CargarDatos($"SELECT ImporteOriginal FROM Anticipos WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idAnticipo}'");
            //    var resultadoImporte = "";

            //    foreach (DataRow consultaImporOriginal in consultaImporteOriginal.Rows)
            //    {
            //        resultadoImporte = consultaImporOriginal["ImporteOriginal"].ToString();
            //    }

            //    if (anticipoAplicado < 0) 
            //    {
            //        var actualizarAnticipoAplicado2 = cn.EjecutarConsulta($"UPDATE Anticipos SET AnticipoAplicado = '{resultadoImporte}' WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idAnticipo}'");
            //    }
            //    else
            //    {
            //        var actualizarAnticipoAplicado = cn.EjecutarConsulta($"UPDATE Anticipos SET AnticipoAplicado = '{sumaImportes}' + AnticipoAplicado WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idAnticipo}'");
            //    }
            //}

            if (sumaImportes > 0)
            {
                var importeTmp = sumaImportes;

                if ((sumaImportes - totalAnticipos) <= 0)
                {
                    sumaImportes = 0;

                    if (totalAnticipos > 0)
                    {
                        if (importeTmp <= totalAnticipos)
                        {
                            cAnticipoUtilizado.Text = importeTmp.ToString("0.00");
                        }
                    }
                }

                if ((sumaImportes - totalAnticipos) > 0)
                {
                    sumaImportes -= totalAnticipos;

                    if (totalAnticipos > 0)
                    {
                        if (sumaImportes <= totalAnticipos)
                        {
                            var diferencia = importeTmp - sumaImportes;

                            cAnticipoUtilizado.Text = diferencia.ToString("N");
                        }
                        else
                        {
                            var diferencia = importeTmp - sumaImportes;
                            cAnticipoUtilizado.Text = diferencia.ToString("N");
                        }
                    }
                }
            }
            using (var dt = cn.CargarDatos($"SELECT mostrarIVA FROM configuracion WHERE IDUsuario= {FormPrincipal.userID}"))
            {
                if (dt.Rows[0][0].Equals(1))
                {
                    if (total_importe_cero_exe > 0)
                    {
                        lblIVA0Exento.Visible = true;
                        lblCIVA0Exento.Visible = true;
                    }
                    else
                    {
                        lblIVA0Exento.Visible = false;
                        lblCIVA0Exento.Visible = false;
                    }
                    if (totalIVA8 > 0)
                    {
                        lbIVA8.Visible = true;
                        cIVA8.Visible = true;
                        if (totalIVA16 > 0)
                        {

                        }
                        else
                        {
                            lbIVA.Visible = false;
                            cIVA.Visible = false;
                        }

                    }
                    else
                    {
                        lbIVA8.Visible = false;
                        cIVA8.Visible = false;
                        lbIVA.Visible = true;
                        cIVA.Visible = true;
                    }

                    if (totalIVA16 > 0)
                    {
                        lbIVA.Visible = true;
                        cIVA.Visible = true;
                    }
                    else
                    {
                        lbIVA.Visible = false;
                        cIVA.Visible = false;
                    }

                }
                else
                {
                    lbIVA.Visible = false;
                    lbIVA8.Visible = false;
                    cIVA.Visible = false;
                }
            }
            

            cIVA.Text = totalIVA16.ToString("N");
            cIVA8.Text = totalIVA8.ToString("N");
            cTotal.Text = sumaImportes.ToString("N");
            if (sumaImportes <= 0)
            {
                cTotal.Text = "0.00";
            }
            cSubtotal.Text = (totalSubtotal + totalSubtotal8 + total_subtotal_cero_exe).ToString("N");

            // Se ocultan si las cantidades de este campo son igual a 0
            if (totalAnticipos > 0)
            {
                lbAnticipo.Visible = true;
                lbAnticipoUtilizado.Visible = true;
                cAnticipo.Visible = true;
                cAnticipoUtilizado.Visible = true;
            }
            else
            {
                lbAnticipo.Visible = false;
                lbAnticipoUtilizado.Visible = false;
                cAnticipo.Visible = false;
                cAnticipoUtilizado.Visible = false;
            }

            if (totalDescuento > 0)
            {
                lbDescuento.Visible = true;
                cDescuento.Visible = true;
            }
            else
            {
                lbDescuento.Visible = false;
                cDescuento.Visible = false;
            }

            if (totalOtrosImpuestos > 0)
            {
                lbOtrosImpuestos.Visible = true;
                cOtrosImpuestos.Visible = true;
            }
            else
            {
                lbOtrosImpuestos.Visible = false;
                cOtrosImpuestos.Visible = false;
            }

            // Retenciones
            if (total_impuestos_retenidos > 0)
            {
                lb_impuestos_retenidos.Visible = true;
                lb_cant_impuestos_retenidos.Visible = true;
            }
            else
            {
                lb_impuestos_retenidos.Visible = false;
                lb_cant_impuestos_retenidos.Visible = false;
            }

            cOtrosImpuestos.Text = totalOtrosImpuestos.ToString("N");
            lb_cant_impuestos_retenidos.Text = total_impuestos_retenidos.ToString("N");
            cAnticipo.Text = totalAnticipos.ToString("N");
            cDescuento.Text = totalDescuento.ToString("N");
            cNumeroArticulos.Text = totalArticulos.ToString("N");

            ComprobarProductos();
            //txtBuscadorProducto.Clear();
        }

        private void ComprobarProductos()
        {
            if (DGVentas.RowCount == 0)
            {
                listaAnticipos = string.Empty;
                importeAnticipo = 0f;
                cAnticipo.Text = "0.00";
                cAnticipoUtilizado.Text = "0.00";

                // MIRI.
                lbAnticipo.Visible = false;
                lbAnticipoUtilizado.Visible = false;
                cAnticipo.Visible = false;
                cAnticipoUtilizado.Visible = false;
            }
        }

        private void btnEliminarUltimo_Click(object sender, EventArgs e)
        {
            if (opcion17 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            int contFila = DGVentas.Rows.Count;

            if (DGVentas.Rows.Count > 0)
            {
                fechaSistema = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                if (primerClickBtnUltimoEliminado.Equals(false))
                {
                    productoUltimoAgregadoEliminado.Add(DGVentas.Rows[0].Cells["Cantidad"].Value.ToString() + "|" + DGVentas.Rows[0].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[0].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[0].Cells["Descuento"].Value.ToString() + "|" + DGVentas.Rows[0].Cells["Importe"].Value.ToString());

                    primerClickBtnUltimoEliminado = true;
                }
                else
                {
                    string[] word;
                    string palabra = string.Empty;
                    bool descripcionEncontrada = false;

                    palabra = DGVentas.Rows[0].Cells["Descripcion"].Value.ToString();

                    for (int i = 0; i < productoUltimoAgregadoEliminado.Count; i++)
                    {
                        word = productoUltimoAgregadoEliminado[i].Split('|');
                        descripcionEncontrada = Array.Exists(word, element => element == palabra);
                        if (descripcionEncontrada)
                        {
                            var count = Convert.ToDecimal(DGVentas.Rows[0].Cells["Cantidad"].Value.ToString());
                            var nuevaCantidad = (count + Convert.ToDecimal(word[0].ToString()));

                            productoUltimoAgregadoEliminado[i] = nuevaCantidad + "|" + DGVentas.Rows[0].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[0].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[0].Cells["Descuento"].Value.ToString() + "|" + ((nuevaCantidad * Convert.ToDecimal(DGVentas.Rows[0].Cells["Precio"].Value.ToString())) - Convert.ToDecimal(DGVentas.Rows[0].Cells["Descuento"].Value.ToString()));
                            break;
                        }
                    }

                    if (!descripcionEncontrada)
                    {
                        var count = Convert.ToDecimal(DGVentas.Rows[0].Cells["Cantidad"].Value.ToString());

                        productoUltimoAgregadoEliminado.Add(count + "|" + DGVentas.Rows[0].Cells["Precio"].Value.ToString() + "|" + DGVentas.Rows[0].Cells["Descripcion"].Value.ToString() + "|" + DGVentas.Rows[0].Cells["Descuento"].Value.ToString() + "|" + ((count * Convert.ToDecimal(DGVentas.Rows[0].Cells["Precio"].Value.ToString())) - Convert.ToDecimal(DGVentas.Rows[0].Cells["Descuento"].Value.ToString())));
                    }
                }

                var id = Convert.ToInt32(DGVentas.Rows[0].Cells["IDProducto"].Value);

                DGVentas.Rows.RemoveAt(0);

                if (productosDescuentoG.ContainsKey(id))
                {
                    productosDescuentoG.Remove(id);
                }

                if (descuentosDirectos.ContainsKey(id))
                {
                    descuentosDirectos.Remove(id);
                }

                CalculoMayoreo();
                CantidadesFinalesVenta();
                listaMensajesEnviados.Remove(id);
                listaProductosVenta();

            }
        }

        private void btnEliminarTodos_Click(object sender, EventArgs e)
        {
            if (!DGVentas.Rows.Count.Equals(0))
            {
                DialogResult dialogoResult = MessageBox.Show("¿Desea remover todos los artículos?", "¡Advertencia!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogoResult == DialogResult.Yes)
                {
                    if (opcion18 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    // Ejecutar hilo para envio notificacion
                    var datosConfig = mb.ComprobarConfiguracion();

                    if (datosConfig.Count > 0)
                    {
                        if (Convert.ToInt32(datosConfig[19]).Equals(1))
                        {
                            List<string> productosNoVendidos = new List<string>();
                            string fechaSistema = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), importeTotal = string.Empty;

                            foreach (DataGridViewRow dgvRenglon in DGVentas.Rows)
                            {
                                productosNoVendidos.Add(dgvRenglon.Cells["Cantidad"].Value.ToString() + "|" + dgvRenglon.Cells["Precio"].Value.ToString() + "|" + dgvRenglon.Cells["Descripcion"].Value.ToString() + "|" + dgvRenglon.Cells["Descuento"].Value.ToString() + "|" + dgvRenglon.Cells["Importe"].Value.ToString());
                            }

                            importeTotal = cTotal.Text.ToString();

                            Thread btnClearAllItemSale = new Thread(
                                () => Utilidades.ventaBtnClarAllItemSaleEmail(productosNoVendidos, fechaSistema, importeTotal, FormPrincipal.datosUsuario)
                            );

                            btnClearAllItemSale.Start();
                        }
                    }

                    DGVentas.Rows.Clear();
                    // Almacena los ID de los productos a los que se aplica descuento general
                    productosDescuentoG.Clear();
                    // Guarda los datos de los descuentos directos que se han aplicado
                    descuentosDirectos.Clear();

                    nudCantidadPS.Value = 1;

                    CalculoMayoreo();
                    CantidadesFinalesVenta();
                    limpiarImagenDelProducto();
                    PorcentajeDescuento = "";
                    AplicarCantidad = "";
                    AplicarPorcentaje = "";
                }
                else if (dialogoResult == DialogResult.No)
                {
                    return;
                }
                listProductos.Clear();
                liststock2.Clear();
                listaMensajesEnviados.Clear();
            }
            else
            {
                MessageBox.Show("No tiene artículos agregados\na la venta para eliminar", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            txtBuscadorProducto.Focus();
        }

        private void btnCancelarVenta_Click(object sender, EventArgs e)
        {
            if (opcion9 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            mostrarVenta = 0;

            this.Dispose();
        }
        private void btnTerminarVenta_Click(object sender, EventArgs e)
        {
            Ganancia.gananciaGrafica = 3;
            //sepresiono = true;
            btnGanancia.PerformClick();
            EsGuardarVenta = true;
            if (FormPrincipal.userNickName.Contains("@"))
            {
                decimal tieneDescuento = Convert.ToDecimal(cDescuento.Text);
                if (tieneDescuento > 0)
                {
                    var datos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Ventas");
                    if (datos[43].Equals(0))
                    {
                        MessageBox.Show("No cuenta con permiso para realizar una venta con Descuento", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }

            //if (ClienteConDescuento.Equals(true))
            //{
            //    if (!FormPrincipal.id_empleado.Equals(0))
            //    {

            //        int permisoVentaSinAutorizacion,permisoVentaConAutorizacion;
            //        using (DataTable DTPermisoClienteDescuento = cn.CargarDatos(cs.PermissoVentaClienteDescuento(FormPrincipal.userID,FormPrincipal.id_empleado)))
            //        {
            //            permisoVentaConAutorizacion = Convert.ToInt32(DTPermisoClienteDescuento.Rows[0]["PermisoVentaClienteDescuento"]);
            //            permisoVentaSinAutorizacion = Convert.ToInt32(DTPermisoClienteDescuento.Rows[0]["PermisoVentaClienteDescuentoSinAutorizacion"]);
            //        }
            //        if (permisoVentaSinAutorizacion.Equals(0))
            //        {
            //            MessageBox.Show("No tienes permiso para realizar una\n venta a un cliente con descuento","Aviso del Sistema",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return;
            //        }
            //        if (permisoVentaConAutorizacion.Equals(0))
            //        {
            //            MessageBox.Show("No tienes permiso para realizar esta venta\n Solicita la autorizacion del Administrador", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            Autorizacion aut = new Autorizacion();
            //            aut.ShowDialog();
            //            if (AutorizacionConfirmada.Equals(false))
            //            {
            //                MessageBox.Show("No se puede Ejecutar esta venta", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                return;
            //            }
            //        }
            //    } 
            //}

            foreach (DataGridViewRow fila in DGVentas.Rows)
            {
                var idProdutoInactivo = fila.Cells["IDProducto"].Value.ToString();

                using (DataTable dtProductoInactivo = cn.CargarDatos(cs.productoInactivo(idProdutoInactivo)))
                {
                    if (!dtProductoInactivo.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtProductoInactivo.Rows)
                        {
                            productoDeshabilitado.Add($"{item["ID"].ToString()}|{item["Nombre"].ToString()}");
                        }
                    }
                }
            }
            if (!productoDeshabilitado.Count.Equals(0))
            {
                // Code to search the  alphanumneric Part Number (in Column1 header called "PART NUMBER") and highlihgt the row
                foreach (var item in productoDeshabilitado)
                {
                    var palabraParaBuscar = item.Split('|');

                    foreach (DataGridViewRow row in DGVentas.Rows)
                    {
                        var contenidoDeCelda = row.Cells["Descripcion"].Value.ToString();
                        if (!string.IsNullOrWhiteSpace(contenidoDeCelda) && contenidoDeCelda.Equals(palabraParaBuscar[1].ToString()))
                        {
                            DGVentas.Rows[row.Index].DefaultCellStyle.BackColor = Color.DarkSlateGray;
                        }
                    }
                }
                productoDeshabilitado.Clear();
                MessageBox.Show("Su venta contiene productos, combos o servicios\nque no estan activos en el sistema\nfavor de revisar el listado", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (string.IsNullOrEmpty(lbDatosCliente.Text))
                {
                    etiqeutaCliente = "vacio";
                    //this.Visible = false;

                }
                else
                {
                    etiqeutaCliente = "lleno";
                    this.Visible = true;
                }

                if (opcion20 == 0)
                {
                    Utilidades.MensajePermiso();
                    return;
                }

                if (VerificarStockProducto())
                {
                    if (Application.OpenForms.OfType<DetalleVenta>().Count() == 1)
                    {
                        Application.OpenForms.OfType<DetalleVenta>().First().BringToFront();
                    }
                    else
                    {
                        if (primerClickRestarIndividual)
                        {
                            if (productoRestado.Any())
                            {
                                // Ejecutamos hilo para envió notificación
                                var datosConfig = mb.ComprobarConfiguracion();

                                if (datosConfig.Count > 0)
                                {
                                    if (Convert.ToInt32(datosConfig[16]).Equals(1))
                                    {
                                        Thread ProductLessSale = new Thread(
                                            () => Utilidades.ventaProductLessEmail(productoRestado, fechaSistema, FormPrincipal.datosUsuario)
                                        );

                                        ProductLessSale.Start();
                                    }
                                }
                            }

                            primerClickRestarIndividual = false;
                        }

                        if (primerClickEliminarIndividual)
                        {
                            if (productoEliminado.Any())
                            {
                                // Ejecutamos hilo para envió notificación
                                var datosConfig = mb.ComprobarConfiguracion();

                                if (datosConfig.Count > 0)
                                {
                                    if (Convert.ToInt32(datosConfig[17]).Equals(1))
                                    {
                                        Thread ProductDeleteSale = new Thread(
                                            () => Utilidades.ventaProductDeleteEmail(productoEliminado, fechaSistema, FormPrincipal.datosUsuario)
                                        );

                                        ProductDeleteSale.Start();
                                    }
                                }
                            }

                            primerClickEliminarIndividual = false;
                        }

                        if (primerClickBtnUltimoEliminado)
                        {
                            if (productoUltimoAgregadoEliminado.Any())
                            {
                                // Ejecutamos hilo para envió notificación
                                var datosConfig = mb.ComprobarConfiguracion();

                                if (datosConfig.Count > 0)
                                {
                                    if (Convert.ToInt32(datosConfig[18]).Equals(1))
                                    {
                                        Thread ProductDeleteLast = new Thread(
                                            () => Utilidades.ventaBtnUltimoEliminadoEmail(productoUltimoAgregadoEliminado, fechaSistema, FormPrincipal.datosUsuario)
                                        );

                                        ProductDeleteLast.Start();
                                    }
                                }
                            }

                            primerClickBtnUltimoEliminado = false;
                        }

                        //ventaFinalizada = true;

                        var totalVenta = float.Parse(cTotal.Text);

                        DetalleVenta detalle = new DetalleVenta(totalVenta, idCliente);

                        detalle.FormClosed += delegate
                        {
                            if (botonAceptar)
                            {
                                if (!DetalleVenta.nameClienteNameVenta.Equals(string.Empty) && !DetalleVenta.nameClienteNameVenta.Equals("PUBLICO GENERAL"))//aqui para cuando se asigna un cliente en detalle ventas
                                {
                                    idCliente = buscarIdCliente(DetalleVenta.nameClienteNameVenta);
                                    DetalleVenta.nameClienteNameVenta = string.Empty;
                                }
                                else//Aqui es para cuando el cliente tiene un descuento o para cuando se trae un cliente de ventas guardadas
                                {
                                    if (!string.IsNullOrEmpty(lbDatosCliente.Text))
                                    {
                                        if (DetalleVenta.nameClienteNameVenta.Equals(string.Empty) && lbDatosCliente.Text.Equals(string.Empty))
                                        {
                                            idCliente = buscarIdCliente("PUBLICO GENERAL");
                                        }
                                        else
                                        {
                                            if (lbDatosCliente.Text.Contains("---"))
                                            {
                                                String datos = lbDatosCliente.Text;
                                                datos = datos.Replace("---", "|");
                                                string[] words = datos.Split('|');
                                                idCliente = buscarIdCliente(words[0].Replace("Cliente:", string.Empty).Trim());
                                            }
                                            else if (!idCliente.Equals("0"))
                                            {
                                                idCliente = buscarIdCliente(lbDatosCliente.Text);
                                            }
                                            else
                                            {
                                                idCliente = idClienteDescuento.ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //POR SI FALLA
                                        using (DataTable dtPublicoGeneral = cn.CargarDatos(cs.BuscarPublicaGeneral()))
                                        {
                                            if (!dtPublicoGeneral.Rows.Count.Equals(0))
                                            {
                                                DataRow drPublicoGeneral = dtPublicoGeneral.Rows[0];
                                                var IDPublicoGeneral = Convert.ToInt32(drPublicoGeneral["ID"].ToString());
                                                var razonSocialPublicoGeneral = drPublicoGeneral["RazonSocial"].ToString();

                                                idCliente = IDPublicoGeneral.ToString();
                                                //ventaGuardada = true;
                                            }
                                            else
                                            {
                                                //var UltimoNumeroCliente = string.Empty;
                                                //using (DataTable dtUltimocliente = cn.CargarDatos(cs.UltimoNumerodeCliente()))
                                                //{
                                                //    if (!dtUltimocliente.Rows.Count.Equals(0))
                                                //    {
                                                //        foreach (DataRow item in dtUltimocliente.Rows)
                                                //        {
                                                //            var numCliente = Convert.ToInt32(item["NumeroCliente"]);
                                                //            var longitud = 6 - numCliente.ToString().Length;
                                                //            if (longitud.Equals(5))
                                                //            {
                                                //                UltimoNumeroCliente = $"00000{numCliente + 1}";
                                                //            }
                                                //            if (longitud.Equals(4))
                                                //            {
                                                //                UltimoNumeroCliente = $"0000{numCliente + 1}";
                                                //            }
                                                //            if (longitud.Equals(3))
                                                //            {
                                                //                UltimoNumeroCliente = $"000{numCliente + 1}";
                                                //            }
                                                //            if (longitud.Equals(2))
                                                //            {
                                                //                UltimoNumeroCliente = $"00{numCliente + 1}";
                                                //            }
                                                //            if (longitud.Equals(1))
                                                //            {
                                                //                UltimoNumeroCliente = $"0{numCliente + 1}";
                                                //            }
                                                //            if (longitud.Equals(0))
                                                //            {
                                                //                UltimoNumeroCliente = $"{numCliente + 1}";
                                                //            }
                                                //        }
                                                //    }
                                                //    else
                                                //    {
                                                //        UltimoNumeroCliente = "000001";
                                                //    }
                                                //}
                                                //var resultado = cn.EjecutarConsulta(cs.AgregarPublicoGeneral(UltimoNumeroCliente));
                                                //if (resultado.Equals(1))
                                                //{
                                                //    using (DataTable dtNuevoClienteGeneral = cn.CargarDatos(cs.ObtenerDatosClientePublicoGeneral()))
                                                //    {
                                                //        if (!dtNuevoClienteGeneral.Rows.Count.Equals(0))
                                                //        {
                                                //            DataRow drNuevoPublicoGeneral = dtNuevoClienteGeneral.Rows[0];
                                                //            var IDPublicoGeneral = Convert.ToInt32(drNuevoPublicoGeneral["ID"].ToString());
                                                //            var razonSocialPublicoGeneral = drNuevoPublicoGeneral["RazonSocial"].ToString();
                                                //            idCliente = IDPublicoGeneral.ToString();
                                                //            ventaGuardada = true;

                                                //        }
                                                //    }
                                                //}
                                            }
                                        }
                                    }
                                }

                                ventaFinalizada = true;
                                DatosVenta();
                                botonAceptar = false;
                                idCliente = string.Empty;
                                DetalleVenta.idCliente = 0;
                                DetalleVenta.cliente = string.Empty;
                                AsignarCreditoVenta.idCliente = 0;
                                AsignarCreditoVenta.cliente = string.Empty;
                                cargarTicketAnticipo();
                                ultimaVentaInformacion();
                                
                                panel1.Focus();
                            }
                            else
                            {
                                idCliente = string.Empty;
                                DetalleVenta.idCliente = 0;
                                DetalleVenta.cliente = string.Empty;
                                AsignarCreditoVenta.idCliente = 0;
                                AsignarCreditoVenta.cliente = string.Empty;
                            }
                            noDuplicadoVentas = 0;
                            panel1.Focus();
                        };
                        detalle.ShowDialog();

                        if (VentaRealizada.Equals(true))
                        {
                            if (ClienteConDescuento.Equals(true))
                            {
                                var datosConfig = mb.ComprobarConfiguracion();
                                if (datosConfig.Count > 0)
                                {
                                    if (datosConfig[23].Equals(1))
                                    {
                                        string UsuOEmp = "usuario";
                                        string nombre = FormPrincipal.userNickName;
                                        if (FormPrincipal.userNickName.Contains('@'))
                                        {
                                            var split = FormPrincipal.userNickName.Split('@');
                                            nombre = split[1].ToString();
                                            UsuOEmp = "empleado";
                                        }
                                        string asunto = "Venta a un Cliente con Descento";
                                        string correo;
                                        int TipoCliente;
                                        string[] TipoClienteDatos;
                                        string idUltimaVenta;
                                        string[] Precios;
                                        using (DataTable dtTipoCliente = cn.CargarDatos($"SELECT TipoCliente FROM clientes WHERE ID ={IDClienteConDescuento}"))
                                        {
                                            TipoCliente = Convert.ToInt32(dtTipoCliente.Rows[0]["TipoCliente"]);
                                        }
                                        using (DataTable DTDatos = cn.CargarDatos($"SELECT CONCAT(Nombre,'|',DescuentoPorcentaje) AS Datos FROM `tipoclientes` WHERE ID = {TipoCliente}"))
                                        {
                                            string abc = DTDatos.Rows[0]["Datos"].ToString();
                                            TipoClienteDatos = abc.Split('|');
                                        }
                                        using (DataTable ConsultaCorreo = cn.CargarDatos(cs.BuscarCorreoDelUsuario(Convert.ToInt32(FormPrincipal.userID))))
                                        {
                                            correo = ConsultaCorreo.Rows[0]["Email"].ToString();
                                        }
                                        using (DataTable DTIdventa = cn.CargarDatos($"SELECT ID FROM ventas ORDER BY ID DESC LIMIT 1"))
                                        {
                                            idUltimaVenta = DTIdventa.Rows[0]["ID"].ToString();
                                        }
                                        using (DataTable DTDatosPrecios = cn.CargarDatos($"SELECT CONCAT(Total + Descuento,'|',Descuento,'|',Total) 'DatosPrecio' FROM ventas WHERE ID = {idUltimaVenta}"))
                                        {
                                            string PreciosPegados = DTDatosPrecios.Rows[0]["DatosPrecio"].ToString();
                                            Precios = PreciosPegados.Split('|');
                                        }
                                        DateTime fecha = DateTime.Now;
                                        string html = $@"<!DOCTYPE html> <html> <head> </head> <body> <H1 style='text-align: center;'>Venta a Cliente con descuento</H1> <hr> <p style='text-align: center;'> Se realizo una venta al Cliente <b>{ClienteConDescuentoNombre}</b><br><br>El dia <b>{fecha.ToString("dd-MM-yyyy hh:mm:ss")}</b><br><br> Por el {UsuOEmp} <b>{nombre}</b><br>";
                                        html += $@"<br>Por el concepto de <b>{TipoClienteDatos[0]}</b><br><br> Con un descuento del <b>{TipoClienteDatos[1].ToString()}% <br><br>SubTotal: <b>{Precios[0]}</b>----Descuento:{Precios[1]}----Total:{Precios[2]}</p> </body> </html>";


                                        Thread btnClearAllItemSale = new Thread(
                                         () => Utilidades.EnviarEmail(html, asunto, correo)
                                        );

                                        btnClearAllItemSale.Start();

                                    }
                                }
                            }
                            PorcentajeDescuento = "";
                            AplicarCantidad = "";
                            AplicarPorcentaje = "";
                        }
                        VentaRealizada = false;
                        noDuplicadoVentas = 1;
                    }
                }
            }
            txtBuscadorProducto.Focus();
            yasemando = false;
        }

        private void cargarTicketAnticipo()
        {
            if (!IDAnticipo.Equals(0))
            {
                int idVenta = 0;
                using (var DTidventa = cn.CargarDatos($"SELECT ID FROM ventas WHERE IDUsuario = {FormPrincipal.userID} ORDER BY ID DESC LIMIT 1"))
                {
                    idVenta = Convert.ToInt32(DTidventa.Rows[0][0]);
                }
                EsEnVentas = true;
                VisualizadorTicketAnticipo ticketAnt = new VisualizadorTicketAnticipo();
                ticketAnt.idAnticipoViz = IDAnticipo;
                ticketAnt.idVentaViz = idVenta;
                ticketAnt.ShowDialog();
            }
            IDAnticipo = 0;
            EsEnVentas = false;
        }

        private void ultimaVentaInformacion()
        {
            //var ticketTemporal = cn.CargarDatos("Select Total, DineroRecibido, CambioTotal FROM ventas WHERE ID ORDER BY ID DESC LIMIT 1");

            //foreach (DataRow item in ticketTemporal.Rows)
            //{
            //    Total = (float)Convert.ToDouble(item[0]);
            //    DineroRecibido = (float)Convert.ToDouble(item[1]);
            //    CambioTotal = (float)Convert.ToDouble(item[2]);
            //}


            InfoUltimaVenta ticketUltimaVenta = new InfoUltimaVenta();
            ticketUltimaVenta.ShowDialog();
            txtBuscadorProducto.Focus();
        }

        private string buscarIdCliente(string nameCliente)
        {
            string result = string.Empty;

            var queryCliente = cn.CargarDatos($"SELECT ID FROM Clientes WHERE IDUsuario = '{FormPrincipal.userID}' AND RazonSocial = '{nameCliente}'");

            if (queryCliente.Rows.Count.Equals(0) || idCliente.Equals(0))
            {
                result = "0";
            }
            else if (!queryCliente.Rows.Count.Equals(0))
            {
                result = queryCliente.Rows[0]["ID"].ToString();
            }

            return result;
        }

        //Se procesa la informacion de los detalles de la venta para guardarse
        private void DetallesVenta(string IDVenta)
        {
            var cliente = string.Empty;

            using (DataTable dtClientInfo = cn.CargarDatos(cs.getRazonNombreRfcCliente(idCliente)))
            {
                if (!dtClientInfo.Rows.Count.Equals(0))
                {
                    foreach (DataRow drInfoCliente in dtClientInfo.Rows)
                    {
                        cliente = drInfoCliente["RazonSocial"].ToString();
                    }
                }
            }

            string[] info = new string[] {
                IDVenta, FormPrincipal.userID.ToString(), efectivo, tarjeta, vales,
                cheque, transferencia, credito, referencia, idCliente, cliente
            };

            cn.EjecutarConsulta(cs.GuardarDetallesVenta(info));
        }

        private void DetallesCliente(string idVenta)
        {
            if (!string.IsNullOrWhiteSpace(idCliente))
            {
                if (idCliente != "0" && !string.IsNullOrEmpty(lbDatosCliente.Text.Trim()))
                {
                    var datos = mb.ObtenerDatosCliente(Convert.ToInt32(idCliente), FormPrincipal.userID);
                    var cliente = datos[0];
                    var rfc = datos[1];

                    var info = new string[] { cliente, rfc, idVenta, FormPrincipal.userID.ToString() };

                    cn.EjecutarConsulta(cs.ActualizarClienteVenta(info));
                }
                else
                {
                    if (!idCliente.Equals("0"))
                    {
                        var datos = mb.ObtenerDatosCliente(Convert.ToInt32(idCliente), FormPrincipal.userID);
                        var cliente = datos[0];
                        var rfc = datos[1];

                        var info = new string[] { cliente, rfc, idVenta, FormPrincipal.userID.ToString() };

                        cn.EjecutarConsulta(cs.ActualizarClienteVenta(info));
                    }
                    else if (idCliente.Equals("0"))
                    {
                        var info = new string[] {
                            "PUBLICO GENERAL", "XAXX010101000", idVenta,
                            FormPrincipal.userID.ToString()
                        };
                        cn.EjecutarConsulta(cs.ActualizarClienteVenta(info));
                    }
                }
            }

            //idCliente = string.Empty;
        }

        private void DatosVenta()
        {
            // Datos generales de la venta
            var IdEmpresa = FormPrincipal.userID.ToString();
            var Subtotal = formatoNumericoEstandar(cSubtotal.Text);
            var IVA16 = formatoNumericoEstandar(cIVA.Text);
            var Descuento = formatoNumericoEstandar(cDescuento.Text);
            var Total = formatoNumericoEstandar(cTotal.Text);
            var DescuentoGeneral = formatoNumericoEstandar(porcentajeGeneral.ToString("0.00"));
            var Anticipo = formatoNumericoEstandar(cAnticipo.Text);
            var AnticipoUtilizado = formatoNumericoEstandar(cAnticipoUtilizado.Text);
            var FechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var Folio = "";
            var Serie = "A";
            var idClienteTmp = idCliente;
            string id_empleado = Convert.ToString(FormPrincipal.id_empleado);
            var tipoDeVenta = "";

            // variable para saber si esta facturada la venta Guardada
            var estaTimbrada = false;

            if (ventaGuardada)
            {
                if (statusVenta.Equals(""))
                {
                    statusVenta = "2";
                }

                if (string.IsNullOrWhiteSpace(idClienteTmp))
                {
                    idClienteTmp = "0";
                }
            }
            else
            {
                if (idClienteTmp.Equals(""))
                {
                    idClienteTmp = "0";
                }
            }

            aumentoFolio();
            Folio = Contenido;
            FolioVentaCorreo = Folio;

            if (formaDePagoDeVenta.Equals(string.Empty))
            {
                if (cAnticipoUtilizado.Visible.Equals(true))
                {
                    formaDePagoDeVenta = "Anticipos";
                }
                else
                {
                    formaDePagoDeVenta = "Presupuesto";
                }
            }
          

            var guardar = new string[] {
                IdEmpresa, idClienteTmp, IdEmpresa, Subtotal, IVA16, Total, Descuento,
                DescuentoGeneral, Anticipo, Folio, Serie, statusVenta, FechaOperacion,
                idClienteDescuento.ToString(), id_empleado, formaDePagoDeVenta, tipoDeVenta
            };


            if (VerificarStockProducto())
            {
                if (!ventasGuardadas.Count.Equals(0))
                {
                    foreach (var venta in ventasGuardadas)
                    {
                        using (DataTable dtVentaGuardada = cn.CargarDatos(cs.ventaGuardadaEstaTimbrada(venta)))
                        {
                            if (!dtVentaGuardada.Rows.Count.Equals(0))
                            {
                                foreach (DataRow drVentaGuardada in dtVentaGuardada.Rows)
                                {
                                    if (drVentaGuardada["Timbrada"].Equals(1))
                                    {
                                        //MessageBox.Show("Venta ya Facturada");
                                        estaTimbrada = true;
                                    }
                                    else if (drVentaGuardada["Timbrada"].Equals(0))
                                    {
                                        //MessageBox.Show("Venta no Facturada");
                                        estaTimbrada = false;
                                    }
                                }
                            }
                        }
                    }
                }

                int respuesta = 0;

                // Se hace el guardado de la informacion general de la venta
                if (estaTimbrada)
                {
                    foreach (var venta in ventasGuardadas)
                    {
                        using (DataTable dtVentaGuardada = cn.CargarDatos(cs.ventaGuardadaEstaTimbrada(venta)))
                        {
                            var nombreCliente = string.Empty;

                            using (DataTable dtCliente = cn.CargarDatos(cs.getRazonNombreRfcCliente(idClienteTmp)))
                            {
                                if (!dtCliente.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow drCliente in dtCliente.Rows)
                                    {
                                        nombreCliente = drCliente["RazonSocial"].ToString();
                                    }
                                }
                            }

                            foreach (DataRow drVentaGuardada in dtVentaGuardada.Rows)
                            {
                                if (idClienteTmp.Equals(drVentaGuardada["IDCliente"].ToString()))
                                {
                                    MessageBox.Show($"Esta venta ya fue guardada y facturada con el cliente {nombreCliente},\npor lo tanto se guardara como una venta nueva.", "Aviso Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    mostrarVenta = 0;
                                    respuesta = cn.EjecutarConsulta(cs.GuardarVenta(guardar, mostrarVenta, idAnticipoVentas));

                                }
                                else if (!idClienteTmp.Equals(drVentaGuardada["IDCliente"].ToString()))
                                {
                                    guardar[1] = idClienteTmp;
                                    mostrarVenta = 0;
                                    MessageBox.Show($"Esta venta ya fue guardada y facturada con un cliente distinto,\npor lo tanto se guardara como una venta nueva\ncon el cliente: {nombreCliente}.", "Aviso Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    respuesta = cn.EjecutarConsulta(cs.GuardarVenta(guardar, mostrarVenta, idAnticipoVentas));
                                }
                            }
                        }
                    }
                }
                else if (!estaTimbrada)
                {
                    if (!ventasGuardadas.Count.Equals(0))
                    {
                        foreach (var venta in ventasGuardadas)
                        {
                            using (DataTable dtVentaGuardada = cn.CargarDatos(cs.ventaGuardadaEstaTimbrada(venta)))
                            {
                                var nombreCliente = string.Empty;

                                using (DataTable dtCliente = cn.CargarDatos(cs.getRazonNombreRfcCliente(idClienteTmp)))
                                {
                                    if (!dtCliente.Rows.Count.Equals(0))
                                    {
                                        foreach (DataRow drCliente in dtCliente.Rows)
                                        {
                                            nombreCliente = drCliente["RazonSocial"].ToString();
                                        }
                                    }
                                }

                                foreach (DataRow drVentaGuardada in dtVentaGuardada.Rows)
                                {
                                    if (idClienteTmp.Equals(drVentaGuardada["IDCliente"].ToString()))
                                    {
                                        mostrarVenta = 0;

                                        var existeVenta = mb.ExisteVentaDatosRepetidos(guardar);

                                        if (!existeVenta)
                                        {
                                            respuesta = cn.EjecutarConsulta(cs.GuardarVenta(guardar, mostrarVenta, idAnticipoVentas));
                                        }
                                        else
                                        {
                                            respuesta = 1;
                                        }
                                    }
                                    else if (!idClienteTmp.Equals(drVentaGuardada["IDCliente"].ToString()))
                                    {
                                        guardar[1] = idClienteTmp;
                                        mostrarVenta = 0;

                                        var existeVenta = mb.ExisteVentaDatosRepetidos(guardar);

                                        if (!existeVenta)
                                        {
                                            respuesta = cn.EjecutarConsulta(cs.GuardarVenta(guardar, mostrarVenta, idAnticipoVentas));
                                        }
                                        else
                                        {
                                            respuesta = 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        mostrarVenta = 0;
                        respuesta = cn.EjecutarConsulta(cs.GuardarVenta(guardar, mostrarVenta, idAnticipoVentas, gananciaTotalPorVenta));
                        //Venta normal
                    }
                }

                if (respuesta > 0)
                {
                    // Operacion para afectar la tabla de Caja
                    // var saldoActual = cn.ObtenerSaldoActual(FormPrincipal.userID);
                    // var totalTmp = saldoActual + Convert.ToDouble(Total);

                    if (string.IsNullOrWhiteSpace(efectivo)) { efectivo = "0"; }
                    if (string.IsNullOrWhiteSpace(tarjeta)) { tarjeta = "0"; }
                    if (string.IsNullOrWhiteSpace(vales)) { vales = "0"; }
                    if (string.IsNullOrWhiteSpace(cheque)) { cheque = "0"; }
                    if (string.IsNullOrWhiteSpace(transferencia)) { transferencia = "0"; }
                    if (string.IsNullOrWhiteSpace(credito)) { credito = "0"; }
                    if (string.IsNullOrWhiteSpace(Anticipo)) { Anticipo = "0"; }

                    int idOperacionCaja = 0;

                    if (!statusVenta.Equals("2"))
                    {
                        if (FormPrincipal.userNickName.Contains("@"))
                        {
                            string[] datos = new string[] {
                                "venta", Total, "0", "", FechaOperacion, FormPrincipal.userID.ToString(),
                                 efectivo, tarjeta, vales, cheque, transferencia, credito, Anticipo, FormPrincipal.id_empleado.ToString()
                            };

                            idOperacionCaja = cn.EjecutarConsulta(cs.OperacionCajaEmpleado(datos), regresarID: true);
                        }
                        else
                        {
                            string[] datos = new string[] {
                                "venta", Total, "0", "", FechaOperacion, FormPrincipal.userID.ToString(),
                                efectivo, tarjeta, vales, cheque, transferencia, credito, Anticipo, FormPrincipal.id_empleado.ToString()
                            };

                            idOperacionCaja = cn.EjecutarConsulta(cs.OperacionCaja(datos), regresarID: true);
                        }
                    }

                    // Obtener ID de la venta
                    string idVenta = cn.EjecutarSelect("SELECT ID FROM Ventas ORDER BY ID DESC LIMIT 1", 1).ToString();

                    var sumaEfectivo = Properties.Settings.Default.efectivoRecibido + Properties.Settings.Default.tarjetaRecibido + Properties.Settings.Default.transfRecibido + Properties.Settings.Default.chequeRecibido + Properties.Settings.Default.valesRecibido;

                    var cambio = sumaEfectivo - (float)Convert.ToDouble(Total);
                    if (cambio < 0)
                    {
                        cambio = 0;
                    }
                    cn.EjecutarConsulta(cs.actualizarDatosVenta(sumaEfectivo, cambio, Convert.ToInt32(idVenta)));

                    // Si la lista ventasGuardadas contiene elementos quiere decir que son ventas que deberian 
                    // eliminarse junto con sus productos de la tabla ProductosVenta
                    if (!estaTimbrada)
                    {
                        foreach (var venta in ventasGuardadas)
                        {
                            cn.EjecutarConsulta($"DELETE FROM Ventas WHERE ID = {venta} AND IDUsuario = {FormPrincipal.userID} AND Status = 2");
                            cn.EjecutarConsulta(cs.EliminarProductosVenta(venta));
                        }
                    }

                    // Array para almacenar la informacion de los productos vendidos
                    string[][] infoProductos = new string[DGVentas.Rows.Count][];

                    int contador = 0;

                    string datosCorreoVenta = string.Empty;

                    // Datos de los productos vendidos
                    foreach (DataGridViewRow fila in DGVentas.Rows)
                    {
                        var IDProducto = fila.Cells["IDProducto"].Value.ToString();
                        var Nombre = fila.Cells["Descripcion"].Value.ToString();
                        var Cantidad = fila.Cells["Cantidad"].Value.ToString();
                        var Precio = fila.Cells["Precio"].Value.ToString();
                        var Tipo = fila.Cells["TipoPS"].Value.ToString();
                        tipoDeVenta = Tipo;



                        var DescuentoIndividual = fila.Cells["Descuento"].Value.ToString();
                        var ImporteIndividual = fila.Cells["Importe"].Value.ToString();
                        var TipoDescuento = fila.Cells["TipoDescuento"].Value.ToString();

                        if (TipoDescuento.Equals("0"))
                        {
                            if (descuentoCliente > 0)
                            {
                                TipoDescuento = "3";
                            }
                        }

                        if (string.IsNullOrEmpty(lbDatosCliente.Text) && idClienteTmp.Equals("0"))
                        {
                            cliente = "PUBLICO GENERAL";
                        }

                        // A partir de la variable DescuentoGeneral esos valores y datos se toman solo para el ticket de venta
                        guardar = new string[] {
                            idVenta, IDProducto, Nombre, Cantidad, Precio,
                            DescuentoGeneral, DescuentoIndividual, ImporteIndividual,
                            Descuento, Total, Folio, AnticipoUtilizado, TipoDescuento,
                            formaDePagoDeVenta, cliente, referencia, idClienteTmp
                        };

                        datosCorreoVenta = formaDePagoDeVenta + "|" + cliente + "|" + Folio;

                        // Guardar info de los productos
                        infoProductos[contador] = guardar;

                        contador++;

                        // Si es un producto, paquete o servicio lo guarda en la tabla de productos de venta
                        if (Tipo == "P" || Tipo == "S" || Tipo == "PQ" || Tipo == "VR")
                        {
                            if (!statusVenta.Equals("2"))
                            {
                                using (DataTable dtProductosVenta = cn.CargarDatos(cs.checarProductosVenta(idVenta)))
                                {
                                    bool contains = dtProductosVenta.AsEnumerable().Any(row => Convert.ToInt32(IDProducto) == row.Field<int>("IDProducto"));
                                    if (contains)
                                    {
                                        cn.EjecutarConsulta(cs.GuardarProductosVenta(guardar, 1));
                                    }
                                    else
                                    {
                                        cn.EjecutarConsulta(cs.GuardarProductosVenta(guardar));
                                    }

                                    //var dato = cn.CargarDatos($"SELECT Stock FROM productos WHERE ID = {guardar[1]}");
                                    //decimal stockActual = Convert.ToDecimal(dato.Rows[0]["Stock"]);
                                    //decimal stockNuevo = stockActual - Convert.ToDecimal(guardar[3]);

                                    //cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad) VALUES ('{guardar[1]}','Venta Ralizada Folio: {guardar[10]}','{stockActual}','{stockNuevo}','{FechaOperacion}','{FormPrincipal.userNickName}','-{Convert.ToDecimal(guardar[3])}')");
                                    if (tipoDeVenta.Equals("PQ") || tipoDeVenta.Equals("S"))
                                    {
                                        var tipoDeVentaComboServicio = string.Empty;

                                        var consulta = cn.CargarDatos($"SELECT * FROM productosdeservicios WHERE IDServicio = '{IDProducto}'");
                                        // faltaba esa validacion de si no tenia nada la consulta
                                        if (consulta.Rows.Count > 1) //Combo o Servicio con mas de 1 producto asignado
                                        {

                                            foreach (DataRow item in consulta.Rows)
                                            {
                                                idprodCombo = item[3].ToString();
                                                var consulta2 = cn.CargarDatos($"SELECT Cantidad FROM productosdeservicios WHERE IDServicio = '{IDProducto}' AND IDProducto = '{idprodCombo}'");
                                                cantidadCombo = Convert.ToInt32(consulta2.Rows[0]["cantidad"]);
                                                idComboServicio = Convert.ToInt32(consulta.Rows[0]["IDServicio"].ToString());

                                                var dato = cn.CargarDatos($"SELECT Stock FROM productos WHERE ID = {idprodCombo}");
                                                // faltaba esa validacion de si no tenia nada la consulta
                                                if (!dato.Rows.Count.Equals(0))
                                                {
                                                    decimal stockActual = Convert.ToDecimal(dato.Rows[0]["Stock"]);
                                                    decimal stockNuevo = stockActual - cantidadCombo * Convert.ToDecimal(guardar[3]);
                                                    if (tipoDeVenta.Equals("PQ"))
                                                    {
                                                        tipoDeVentaComboServicio = "de combo";
                                                    }
                                                    else if (tipoDeVenta.Equals("S"))
                                                    {
                                                        tipoDeVentaComboServicio = "de servicio";
                                                    }
                                                    cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad, tipoDeVenta,idComboServicio) VALUES ('{idprodCombo}','Venta Ralizada {tipoDeVentaComboServicio} Folio: {guardar[10]}','{stockActual}','{stockNuevo}','{FechaOperacion}','{FormPrincipal.userNickName}','-{cantidadCombo * Convert.ToDecimal(guardar[3])}','{tipoDeVenta}',{idComboServicio})");
                                                }
                                            }
                                            cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad, tipoDeVenta,idComboServicio) VALUES ('{idComboServicio}','Venta Ralizada {tipoDeVentaComboServicio} Folio: {guardar[10]}','N/A','N/A','{FechaOperacion}','{FormPrincipal.userNickName}','-{Convert.ToDecimal(guardar[3])      }','{tipoDeVenta}',{idComboServicio})");


                                        }
                                        else if (!consulta.Rows.Count.Equals(0))//Combo o Servicio con 1 solo producto agregado
                                        {
                                            idprodCombo = consulta.Rows[0]["IDProducto"].ToString();
                                            var consulta2 = cn.CargarDatos($"SELECT Cantidad FROM productosdeservicios WHERE IDServicio = '{IDProducto}' AND IDProducto = '{idprodCombo}'");
                                            cantidadCombo = Convert.ToInt32(consulta.Rows[0]["cantidad"]);
                                            idComboServicio = Convert.ToInt32(consulta.Rows[0]["IDServicio"].ToString());

                                            var dato = cn.CargarDatos($"SELECT Stock FROM productos WHERE ID = {idprodCombo}");
                                            // faltaba esa validacion de si no tenia nada la consulta
                                            if (!dato.Rows.Count.Equals(0))
                                            {
                                                decimal stockActual = Convert.ToDecimal(dato.Rows[0]["Stock"]);
                                                decimal stockNuevo = stockActual - cantidadCombo * Convert.ToDecimal(guardar[3]);
                                                if (tipoDeVenta.Equals("PQ"))
                                                {
                                                    tipoDeVentaComboServicio = "de combo";
                                                }
                                                else if (tipoDeVenta.Equals("S"))
                                                {
                                                    tipoDeVentaComboServicio = "de servicio";
                                                }

                                                cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad, tipoDeVenta,idComboServicio) VALUES ('{idprodCombo}','Venta Ralizada {tipoDeVentaComboServicio} Folio: {guardar[10]}','{stockActual}','{stockNuevo}','{FechaOperacion}','{FormPrincipal.userNickName}','-{cantidadCombo * Convert.ToDecimal(guardar[3])}','{tipoDeVenta}',{idComboServicio})");
                                            }
                                            cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad, tipoDeVenta,idComboServicio) VALUES ('{idComboServicio}','Venta Ralizada {tipoDeVentaComboServicio} Folio: {guardar[10]}','N/A','N/A','{FechaOperacion}','{FormPrincipal.userNickName}','-{Convert.ToDecimal(guardar[3])}','{tipoDeVenta}',{idComboServicio})");
                                        }       
                                    }
                                    else
                                    {
                                        var dato = cn.CargarDatos($"SELECT Stock FROM productos WHERE ID = {guardar[1]}");
                                        // faltaba esa validacion de si no tenia nada la consulta
                                        if (!dato.Rows.Count.Equals(0))
                                        {
                                            decimal stockActual = Convert.ToDecimal(dato.Rows[0]["Stock"]);
                                            decimal stockNuevo = stockActual - Decimal.Parse(guardar[3], NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
                                            //decimal stockNuevo = stockActual - Convert.ToDecimal(guardar[3]);
                                            idComboServicio = 0;

                                            cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad, tipoDeVenta,idComboServicio) VALUES ('{guardar[1]}','Venta Ralizada Folio: {guardar[10]}','{stockActual}','{stockNuevo}','{FechaOperacion}','{FormPrincipal.userNickName}','-{Decimal.Parse(guardar[3], NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint)}','{tipoDeVenta}','{idComboServicio}')");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                using (DataTable dtProductosVenta = cn.CargarDatos(cs.checarProductosVenta(idVenta)))
                                {
                                    bool contains = dtProductosVenta.AsEnumerable().Any(row => Convert.ToInt32(IDProducto) == row.Field<int>("IDProducto"));

                                    if (contains)
                                    {
                                        cn.EjecutarConsulta(cs.GuardarProductosVenta(guardar, 1));
                                    }
                                    else
                                    {
                                        cn.EjecutarConsulta(cs.GuardarProductosVenta(guardar));
                                    }
                                }
                                if (tipoDeVenta.Equals("PQ") || tipoDeVenta.Equals("S"))
                                {
                                    var tipoDeVentaComboServicio = string.Empty;
                                    var consulta = cn.CargarDatos($"SELECT * FROM productosdeservicios WHERE IDServicio = '{IDProducto}'");
                                    // faltaba esa validacion de si no tenia nada la consulta
                                    if (!consulta.Rows.Count.Equals(0) && ventaGuardadappt == 0)
                                    {
                                        idprodCombo = consulta.Rows[0]["IDProducto"].ToString();
                                        var consulta2 = cn.CargarDatos($"SELECT Cantidad FROM productosdeservicios WHERE IDServicio = '{IDProducto}' AND IDProducto = '{idprodCombo}'");
                                        cantidadCombo = Convert.ToInt32(consulta.Rows[0]["cantidad"]);
                                        idComboServicio = Convert.ToInt32(consulta.Rows[0]["IDServicio"].ToString());

                                        var dato = cn.CargarDatos($"SELECT Stock FROM productos WHERE ID = {idprodCombo}");
                                        // faltaba esa validacion de si no tenia nada la consulta
                                        if (!dato.Rows.Count.Equals(0) && ventaGuardadappt == 0)
                                        {
                                            decimal stockActual = Convert.ToDecimal(dato.Rows[0]["Stock"]);
                                            decimal stockNuevo = stockActual - cantidadCombo * Convert.ToDecimal(guardar[3]);
                                            if (tipoDeVenta.Equals("PQ"))
                                            {
                                                tipoDeVentaComboServicio = "de combo";
                                            }
                                            else if (tipoDeVenta.Equals("S"))
                                            {
                                                tipoDeVentaComboServicio = "de servicio";
                                            }

                                            cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad, tipoDeVenta,idComboServicio) VALUES ('{idprodCombo}','Venta Ralizada {tipoDeVentaComboServicio} Folio: {guardar[10]}','{stockActual}','{stockNuevo}','{FechaOperacion}','{FormPrincipal.userNickName}','-{cantidadCombo * Convert.ToDecimal(guardar[3])}','{tipoDeVenta}',{idComboServicio})");
                                        }
                                    }
                                }
                                else
                                {
                                    var dato = cn.CargarDatos($"SELECT Stock FROM productos WHERE ID = {guardar[1]}");
                                    // faltaba esa validacion de si no tenia nada la consulta
                                    if (!dato.Rows.Count.Equals(0) && ventaGuardadappt == 0)
                                    {
                                        decimal stockActual = Convert.ToDecimal(dato.Rows[0]["Stock"]);
                                        decimal stockNuevo = stockActual - Convert.ToDecimal(guardar[3]);
                                        idComboServicio = 0;

                                        cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad, tipoDeVenta,idComboServicio) VALUES ('{guardar[1]}','Venta Ralizada Folio: {guardar[10]}','{stockActual}','{stockNuevo}','{FechaOperacion}','{FormPrincipal.userNickName}','-{Convert.ToDecimal(guardar[3])}','{tipoDeVenta}','{idComboServicio}')");
                                    }
                                }
                            }

                        }

                        // Si la venta no fue guardada con el boton "Guardar"
                        if (!ventaGuardada)
                        {
                            // Producto
                            if (Tipo == "P")
                            {
                                // Actualizar stock de productos
                                var datosProductoAux = cn.VerificarStockProducto(Convert.ToInt32(IDProducto), FormPrincipal.userID);
                                datosProductoAux = datosProductoAux[0].Split('|');

                                //var stock = Convert.ToDecimal(fila.Cells["Stock"].Value);
                                var stock = Convert.ToDecimal(datosProductoAux[1]);
                                var vendidos = Convert.ToDecimal(fila.Cells["Cantidad"].Value);
                                var restantes = (stock - vendidos).ToString();

                                // Actualizar el stock
                                guardar = new string[] { IDProducto, restantes, FormPrincipal.userID.ToString() };

                                cn.EjecutarConsulta(cs.ActualizarStockProductos(guardar));

                                // Comprobar si aplica para el envio de correo ya sea de stock minimo, de venta o ambos
                                if (correoStockMinimo == 1 || correoVentaProducto == 1)
                                {
                                    var configProducto = mb.ComprobarCorreoProducto(Convert.ToInt32(IDProducto));

                                    if (configProducto.Count > 0)
                                    {
                                        var datosProductoTmp = cn.BuscarProducto(Convert.ToInt32(IDProducto), FormPrincipal.userID);

                                        // Correo de stock minimo
                                        if (configProducto[2] == 1 && correoStockMinimo == 1)
                                        {
                                            // Obtener el stock minimo del producto
                                            var stockMinimo = Convert.ToInt32(datosProductoTmp[10]);
                                            var stockTmp = Convert.ToDecimal(datosProductoTmp[4]);

                                            if (stockTmp <= stockMinimo)
                                            {
                                                if (!enviarStockMinimo.ContainsKey(Convert.ToInt32(IDProducto)))
                                                {
                                                    var terminacion = datosProductoTmp[4].Split('.');

                                                    if (terminacion.Count() > 0)
                                                    {
                                                        if (terminacion[1] == "00")
                                                        {
                                                            datosProductoTmp[4] = terminacion[0];
                                                        }
                                                    }

                                                    var nombre = $"{datosProductoTmp[1]} --- CÓDIGO BARRAS: {datosProductoTmp[7]} --- STOCK MINIMO: {datosProductoTmp[10]} --- STOCK ACTUAL: {datosProductoTmp[4]}";
                                                    enviarStockMinimo.Add(Convert.ToInt32(IDProducto), nombre);
                                                }
                                            }
                                        }

                                        // Correo venta de producto
                                        if (configProducto[3] == 1)
                                        {
                                            if (!enviarVentaProducto.ContainsKey(Convert.ToInt32(IDProducto)))
                                            {
                                                var nombre = $"{datosProductoTmp[1]} --- CÓDIGO BARRAS: {datosProductoTmp[7]} --- STOCK ACTUAL: {restantes}---CANTIDAD VENDIDA: {Cantidad}";
                                                enviarVentaProducto.Add(Convert.ToInt32(IDProducto), nombre);
                                            }
                                        }
                                    }
                                }
                            }

                            // Servicio o paquete
                            if (Tipo == "S" || Tipo == "PQ")
                            {
                                var vendidos = Convert.ToDecimal(fila.Cells["Cantidad"].Value);

                                if (correoVentaProducto == 1)
                                {
                                    var configServicio = mb.ComprobarCorreoProducto(Convert.ToInt32(IDProducto));

                                    if (configServicio.Count > 0)
                                    {
                                        // Correo venta de servicio o paquete
                                        if (configServicio[3] == 1)
                                        {
                                            if (!enviarVentaProducto.ContainsKey(Convert.ToInt32(IDProducto)))
                                            {
                                                var datosServicioTmp = cn.BuscarProducto(Convert.ToInt32(IDProducto), FormPrincipal.userID);

                                                var nombre = $"{datosServicioTmp[1]} --- CÓDIGO BARRAS: {datosServicioTmp[7]}";
                                                enviarVentaProducto.Add(Convert.ToInt32(IDProducto), nombre);
                                            }
                                        }
                                    }
                                }

                                var datosServicio = cn.ObtenerProductosServicio(Convert.ToInt32(IDProducto));

                                foreach (string producto in datosServicio)
                                {
                                    var datosProducto = producto.Split('|');
                                    if (!datosProducto[0].Equals("0"))
                                    {
                                        using (DataTable dtCheckProdIsActive = cn.CargarDatos(cs.checarProductoEstaActivo(datosProducto[0])))
                                        {
                                            if (!dtCheckProdIsActive.Rows.Count.Equals(0))
                                            {
                                                var idProducto = Convert.ToInt32(datosProducto[0]);
                                                var stockRequerido = Convert.ToDecimal(datosProducto[1]) * vendidos;

                                                // Actualizar el stock de los productos de los servicios o paquetes
                                                datosProducto = cn.VerificarStockProducto(idProducto, FormPrincipal.userID);
                                                datosProducto = datosProducto[0].Split('|');
                                                var stockActual = Convert.ToDecimal(datosProducto[1]);

                                                var restantes = (stockActual - stockRequerido).ToString();

                                                guardar = new string[] { idProducto.ToString(), restantes, FormPrincipal.userID.ToString() };

                                                cn.EjecutarConsulta(cs.ActualizarStockProductos(guardar));


                                                // Comprobar si aplica para el envio de correo ya sea de stock minimo, de venta o ambos
                                                if (correoStockMinimo == 1 || correoVentaProducto == 1)
                                                {
                                                    var configProducto = mb.ComprobarCorreoProducto(idProducto);

                                                    if (configProducto.Count > 0)
                                                    {
                                                        var datosProductoTmp = cn.BuscarProducto(idProducto, FormPrincipal.userID);

                                                        // Correo de stock minimo
                                                        if (configProducto[2] == 1)
                                                        {
                                                            // Obtener el stock minimo del producto
                                                            var stockMinimo = (float)Convert.ToDouble(datosProductoTmp[10]);
                                                            var stockTmp = (float)Convert.ToDouble(datosProductoTmp[4]);

                                                            if (stockTmp <= stockMinimo)
                                                            {
                                                                if (!enviarStockMinimo.ContainsKey(idProducto))
                                                                {
                                                                    var nombre = $"{datosProductoTmp[1]} --- CÓDIGO BARRAS: {datosProductoTmp[7]} --- STOCK MINIMO: {datosProductoTmp[10]} --- STOCK ACTUAL: {datosProductoTmp[4]}";
                                                                    enviarStockMinimo.Add(idProducto, nombre);
                                                                }
                                                            }
                                                        }

                                                        // Correo venta de producto
                                                        if (configProducto[3] == 1)
                                                        {
                                                            if (!enviarVentaProducto.ContainsKey(idProducto))
                                                            {
                                                                var nombre = $"{datosProductoTmp[1]} --- CÓDIGO BARRAS: {datosProductoTmp[7]} --- STOCK ACTUAL: {restantes}";
                                                                enviarVentaProducto.Add(idProducto, nombre);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            // Guardar detalles de la venta
                            DetallesVenta(idVenta);
                            DetallesCliente(idVenta);
                        }
                        else
                        {
                            DetallesVenta(idVenta);
                            DetallesCliente(idVenta);
                        }
                    }

                    if (!ventaGuardada)
                    {
                        if (correoVenta == 1 || correoDescuento == 1)
                        {
                            enviarVenta.Clear();
                            foreach (DataGridViewRow articulo in DGVentas.Rows)
                            {
                                enviarVenta.Add(articulo.Cells["Cantidad"].Value.ToString() + "|" + articulo.Cells["Precio"].Value.ToString() + "|" + articulo.Cells["Descripcion"].Value.ToString() + "|" + articulo.Cells["Descuento"].Value.ToString() + "|" + articulo.Cells["Importe"].Value.ToString() + "|" + datosCorreoVenta + "|" + cAnticipo.Text.Trim() + "|" + cAnticipoUtilizado.Text.Trim() + "|" + cDescuento.Text.Trim());
                            }
                        }
                    }

                    idCliente = string.Empty;

                    // Convertir la cadena que guarda los IDs de los anticipos usados en Array
                    if (!string.IsNullOrEmpty(listaAnticipos))
                    {
                        var auxiliar = listaAnticipos.Remove(listaAnticipos.Length - 1);

                        var anticipos = auxiliar.Split('-');

                        // Diferencia del anticipo recibido menos el anticipo utilizado
                        var diferencia = float.Parse(cAnticipo.Text) - float.Parse(cAnticipoUtilizado.Text);

                        var contadorAux = 0;
                        var longitud = anticipos.Length;

                        foreach (string anticipo in anticipos)
                        {
                            var idAnticipo = Convert.ToInt32(anticipo);

                            cn.EjecutarConsulta(cs.CambiarStatusAnticipo(3, idAnticipo, FormPrincipal.userID));
                            cn.EjecutarConsulta($"UPDATE Anticipos SET IDVenta = {idVenta} WHERE ID = {idAnticipo} AND IDUsuario = {FormPrincipal.userID}");

                            if (contadorAux == (longitud - 1))
                            {
                                if (diferencia > 0)
                                {
                                    cn.EjecutarConsulta($"UPDATE Anticipos SET Importe = {diferencia}, Status = 5 WHERE ID = {idAnticipo} AND IDUsuario = {FormPrincipal.userID}");

                                    if (idOperacionCaja > 0)
                                    {
                                        cn.EjecutarConsulta($"UPDATE Caja SET Anticipo = {float.Parse(cAnticipoUtilizado.Text)} WHERE ID = {idOperacionCaja} AND IDUsuario = {FormPrincipal.userID}");
                                    }
                                }
                            }

                            contadorAux++;
                        }

                        cn.EjecutarConsulta($"UPDATE DetallesVenta SET Anticipo = '{Anticipo}' WHERE IDVenta = {idVenta} AND IDUsuario = {FormPrincipal.userID}");
                    }

                    //GenerarTicket(infoProductos);

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
                            var ticket8cm = 0;
                            var ticket6cm = 0;
                            var codigoBarraTicket = 0;
                            var referencia = 0;

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
                                referencia = Convert.ToInt32(item["Referencia"].ToString());
                            }

                            var tipoDeVentaRealizada = 0;

                            using (DataTable dtTipoDeVentaRealizada = cn.CargarDatos(cs.StatusVenta(Convert.ToInt32(idVenta))))
                            {
                                if (!dtTipoDeVentaRealizada.Rows.Count.Equals(0))
                                {
                                    DataRow drTipoVentaRealizada = dtTipoDeVentaRealizada.Rows[0];
                                    tipoDeVentaRealizada = Convert.ToInt32(drTipoVentaRealizada["Status"].ToString());
                                }
                            }

                            // Imprimir Ticket Venta Terminada
                            if (tipoDeVentaRealizada.Equals(1))
                            {
                                int Permiso = 0;
                                int TicketPDF = 0;
                                using (var DTpermiso = cn.CargarDatos($"SELECT HabilitarTicketVentas,TicketOPDF FROM `configuracion` WHERE IDUsuario = {FormPrincipal.userID}"))
                                {
                                    Permiso = Convert.ToInt32(DTpermiso.Rows[0]["HabilitarTicketVentas"]);
                                    TicketPDF = Convert.ToInt32(DTpermiso.Rows[0]["TicketOPDF"]);
                                }
                                if (Permiso.Equals(1))
                                {
                                    if (TicketPDF.Equals(1))
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
                                            imprimirTicketVenta.Referencia = referencia;
                                            imprimirTicketVenta.ShowDialog();
                                        }
                                    }
                                    else if (TicketPDF.Equals(2))
                                    {
                                        FormNotaDeVenta form = new FormNotaDeVenta(Convert.ToInt32(idVenta));
                                        FormNotaDeVenta.fuePorVenta = true;
                                        form.ShowDialog();
                                    }
                                }
                                else
                                {
                                    using (var DTpermiso = cn.CargarDatos($"SELECT PreguntarTicketVenta FROM `configuracion` WHERE IDUsuario = {FormPrincipal.userID}"))
                                    {
                                        if (DTpermiso.Rows[0][0].Equals(1))
                                        {
                                            DialogResult RespuestaPregunta = MessageBox.Show("Desea imprimir el Ticket", "Aviso del Sistem",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                                           
                                            if (RespuestaPregunta.Equals(DialogResult.Yes))
                                            {
                                                if (TicketPDF.Equals(1))
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
                                                        imprimirTicketVenta.Referencia = referencia;
                                                        imprimirTicketVenta.ShowDialog();
                                                    }
                                                }
                                                else if (TicketPDF.Equals(2))
                                                {
                                                    FormNotaDeVenta form = new FormNotaDeVenta(Convert.ToInt32(idVenta));
                                                    FormNotaDeVenta.fuePorVenta = true;
                                                    form.ShowDialog();
                                                }

                                            }
                                            else 
                                            {
                                                using (var dt = cn.CargarDatos($"SELECT AbrirCajaVentas FROM configuraciondetickets WHERE IDUsuario = {FormPrincipal.userID}"))
                                                {
                                                    if (dt.Rows[0][0].Equals(1))
                                                    {
                                                        AbrirSinTicket abrirSin1 = new AbrirSinTicket();
                                                        abrirSin1.Show();
                                                    }
                                                }
                                                
                                            }
                                        }
                                        else
                                        {
                                            using (var dt = cn.CargarDatos($"SELECT AbrirCajaVentas FROM configuraciondetickets WHERE IDUsuario = {FormPrincipal.userID}"))
                                            {
                                                if (dt.Rows[0][0].Equals(1))
                                                {
                                                    AbrirSinTicket abrirSin1 = new AbrirSinTicket();
                                                    abrirSin1.Show();
                                                }
                                            }
                                        }
                                    }
                                }
                            }


                            // Imprimir Ticket Venta Guardada
                            if (tipoDeVentaRealizada.Equals(2))
                            {

                                using (var dt =  cn.CargarDatos($"SELECT TicketPresupuesto,PreguntarTicketPresupuesto,TicketOPDFPresupuesto,AbrirCajaGuardada FROM configuraciondetickets where IDUsuario = {FormPrincipal.userID}"))
                                {
                                    if (dt.Rows[0]["TicketPresupuesto"].Equals(1))
                                    {
                                        if (dt.Rows[0]["TicketOPDFPresupuesto"].Equals(1))
                                        {
                                            if (ticket6cm.Equals(1))
                                            {

                                                using (imprimirTicketPresupuesto8cm imprimirTicketVenta = new imprimirTicketPresupuesto8cm())
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
                                                    imprimirTicketVenta.Referencia = referencia;

                                                    imprimirTicketVenta.ShowDialog();
                                                }
                                            }
                                            else if (ticket8cm.Equals(1))
                                            {
                                                using (imprimirTicketPresupuesto8cm imprimirTicketVenta = new imprimirTicketPresupuesto8cm())
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
                                                    imprimirTicketVenta.Referencia = referencia;

                                                    imprimirTicketVenta.ShowDialog();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            FormNotaDeVenta formNota = new FormNotaDeVenta(Convert.ToInt32(idVenta));
                                            FormNotaDeVenta.fuePorVenta = true;
                                            formNota.ShowDialog();
                                        }
                                        
                                    }
                                    else if (dt.Rows[0]["PreguntarTicketPresupuesto"].Equals(1))
                                    {
                                        DialogResult respuestaImpresion = MessageBox.Show("Desea Imprimir El Ticket Del Presupuesto", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                                        if (respuestaImpresion.Equals(DialogResult.Yes))
                                        {
                                            if (dt.Rows[0]["TicketOPDFPresupuesto"].Equals(1))
                                            {
                                                if (ticket6cm.Equals(1))
                                                {

                                                    using (imprimirTicketPresupuesto8cm imprimirTicketVenta = new imprimirTicketPresupuesto8cm())
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
                                                        imprimirTicketVenta.Referencia = referencia;

                                                        imprimirTicketVenta.ShowDialog();
                                                    }
                                                }
                                                else if (ticket8cm.Equals(1))
                                                {
                                                    using (imprimirTicketPresupuesto8cm imprimirTicketVenta = new imprimirTicketPresupuesto8cm())
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
                                                        imprimirTicketVenta.Referencia = referencia;

                                                        imprimirTicketVenta.ShowDialog();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                FormNotaDeVenta formNota = new FormNotaDeVenta(Convert.ToInt32(idVenta));
                                                FormNotaDeVenta.fuePorVenta = true;
                                                formNota.ShowDialog();
                                            }
                                        }
                                        else if (dt.Rows[0]["AbrirCajaGuardada"].Equals(1))
                                        {
                                            AbrirSinTicket abrirSin = new AbrirSinTicket();
                                            abrirSin.Show();
                                        }
                                    }
                                    else if (dt.Rows[0]["AbrirCajaGuardada"].Equals(1))
                                    {
                                        AbrirSinTicket abrirSin = new AbrirSinTicket();
                                        abrirSin.Show();
                                    }
                                  
                                }
                                
                                txtBuscadorProducto.Focus();
                            }
                            // Imprimir Ticket Venta Cancelada
                            if (tipoDeVentaRealizada.Equals(3))
                            {
                                txtBuscadorProducto.Focus();
                            }
                            // Imprimir Ticket Venta a Credito
                            if (tipoDeVentaRealizada.Equals(4))
                            {
                                using (var dt = cn.CargarDatos($"SELECT CreditoRealizado,PreguntarCreditoRealizado,TicketOPDFCreditoRealizado,AbrirCajaCredito FROM configuraciondetickets where IDUsuario = {FormPrincipal.userID}"))
                                {
                                    if (dt.Rows[0]["CreditoRealizado"].Equals(1))
                                    {
                                        if (dt.Rows[0]["TicketOPDFCreditoRealizado"].Equals(1))
                                        {
                                            using (ImprimirTicketCredito8cm imprimirTicketVenta = new ImprimirTicketCredito8cm())
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
                                                imprimirTicketVenta.Referencia = referencia;

                                                imprimirTicketVenta.ShowDialog();
                                            }
                                        }
                                        else
                                        {
                                            FormNotaDeVenta venta = new FormNotaDeVenta(Convert.ToInt32(idVenta));
                                            FormNotaDeVenta.fuePorVenta = true;
                                            venta.ShowDialog();
                                        }
                                    }
                                    else if (dt.Rows[0]["PreguntarCreditoRealizado"].Equals(1))
                                    {
                                        DialogResult mensaje = MessageBox.Show("¿Desea imprimir el ticket?","Aviso del sistema",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                                        if (mensaje.Equals(DialogResult.Yes))
                                        {
                                            if (dt.Rows[0]["TicketOPDFCreditoRealizado"].Equals(1))
                                            {
                                                using (ImprimirTicketCredito8cm imprimirTicketVenta = new ImprimirTicketCredito8cm())
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
                                                    imprimirTicketVenta.Referencia = referencia;

                                                    imprimirTicketVenta.ShowDialog();
                                                }
                                            }
                                            else
                                            {
                                                FormNotaDeVenta venta = new FormNotaDeVenta(Convert.ToInt32(idVenta));
                                                FormNotaDeVenta.fuePorVenta = true;
                                                venta.ShowDialog();
                                            }
                                        }
                                        else if (dt.Rows[0]["AbrirCajaCredito"].Equals(1))
                                        {
                                            AbrirSinTicket abrirSin = new AbrirSinTicket();
                                            abrirSin.Show();
                                        }
                                    }
                                    else if (dt.Rows[0]["AbrirCajaCredito"].Equals(1))
                                    {
                                        AbrirSinTicket abrirSin = new AbrirSinTicket();
                                        abrirSin.Show();
                                    }
                                }
                                txtBuscadorProducto.Focus();
                            }
                        }
                    }

                    //if (ventaGuardada)
                    //{
                    //    if (statusVenta.Equals("2"))
                    //    {
                    //        Utilidades.CrearMarcaDeAgua(Convert.ToInt32(idVenta), "PRESUPUESTO");
                    //    }
                    //}

                    //var validarImpresionTicket = verificarImpresionTicket();

                    //if (validarImpresionTicket)
                    //{
                    //    if (Utilidades.AdobeReaderInstalado())
                    //    {
                    //        if (statusVenta.Equals("2"))
                    //        {
                    //            DialogResult respuestaImpresion = MessageBox.Show("Desea Imprimir El Ticket Del Presupuesto", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    //            if (respuestaImpresion.Equals(DialogResult.Yes))
                    //            {
                    //                ImprimirTicket(idVenta);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            ImprimirTicket(idVenta);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Utilidades.MensajeAdobeReader();
                    //    }
                    //}
                }

                LimpiarVariables();

                // Hilo para envio de correos en segundo plano
                Thread envio = new Thread(() => CuerpoEmails());
                envio.Start();

                this.Dispose();
                txtBuscadorProducto.Focus();
            }
        }

        private string formatoNumericoEstandar(string cantidad)
        {
            var resultado = string.Empty;
            string especificador;

            // Usar especificadores de formato numérico estándar
            especificador = "G";

            resultado = Convert.ToDecimal(cantidad).ToString(especificador, CultureInfo.InvariantCulture);

            return resultado;
        }

        private void LimpiarVariables()
        {
            ListadoVentas.abrirNuevaVenta = true;
            ventaGuardada = false;
            mostrarVenta = 0;
            listaAnticipos = string.Empty;
            idClienteDescuento = 0;
            ventasGuardadas.Clear();

            // Limpiar variables de cantidades asignadas en el form DetalleVenta
            efectivo = tarjeta = vales = cheque = transferencia = credito = string.Empty;

            // Limpiamos las variables y diccionarios relacionados a los descuentos
            // de los productos en general
            porcentajeGeneral = 0;
            descuentoCliente = 0;
            txtDescuentoGeneral.Text = "% descuento";
            productosDescuentoG.Clear();
            productosDescuentoG.Clear();
            descuentosDirectos.Clear();

            idCliente = string.Empty;
            cliente = string.Empty;
        }

        private void aumentoFolio()
        {
            Contenido = mb.ObtenerMaximoFolio(FormPrincipal.userID);

            // si el contenido es vacio 
            if (Contenido == "")
            {
                // iniciamos el conteo del folio de venta
                PrimerFolioVenta();
                // Aumentamos el folio de venta para la siguiente vez que se utilice
                AumentarFolioVenta();
            }
            // si el contenido no es vacio
            else if (Contenido != "")
            {
                // Aumentamos el codigo de barras para la siguiente vez que se utilice
                AumentarFolioVenta();
            }
        }

        private void AumentarFolioVenta()
        {
            folioVenta = long.Parse(Contenido);
            folioVenta++;
            Contenido = folioVenta.ToString();
        }

        private void PrimerFolioVenta()
        {
            Contenido = "000000000";
        }

        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            if (opcion10 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }


            if (DGVentas.RowCount > 0)
            {
                if (!string.IsNullOrWhiteSpace(listaAnticipos))
                {
                    MessageBox.Show("No se puede guardar esta venta ya que\ntiene un anticipo aplicado", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ListaClientes cliente = new ListaClientes();

                cliente.FormClosed += delegate
                {
                    if (ventaGuardada)
                    {
                        DatosVenta();
                        liststock2.Clear();
                        idCliente = string.Empty;
                        statusVenta = string.Empty;
                        ventaGuardada = false;
                        DetalleVenta.idCliente = 0;
                        DetalleVenta.cliente = string.Empty;
                        DetalleVenta.nameClienteNameVenta = string.Empty;

                    }
                };

                cliente.ShowDialog();
            }
            else
            {
                MessageBox.Show("No hay productos agregados a la lista", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private bool VerificarStockProducto()
        {
            bool respuesta = true;

            // Comprobamos que la opcion stock negativo sea false para que se pueda realizar la venta
            // verificando si el stock es suficiente para realizar la venta, de lo contrario se
            // permitira hacer la venta incluso si el stock es insuficiente
            using (DataTable dtStockNegativo = cn.CargarDatos(cs.cargarDatosDeConfiguracion()))
            {
                if (!dtStockNegativo.Rows.Count.Equals(0))
                {
                    // if (Properties.Settings.Default.StockNegativo == false)
                    if (dtStockNegativo.Rows[0]["StockNegativo"].Equals(0))
                    {
                        if (DGVentas.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow fila in DGVentas.Rows)
                            {
                                var stock = float.Parse(fila.Cells["Stock"].Value.ToString());
                                var cantidad = float.Parse(fila.Cells["Cantidad"].Value.ToString());
                                var tipoPS = fila.Cells["TipoPS"].Value.ToString();

                                // Es producto
                                if (tipoPS == "P")
                                {
                                    if (stock < cantidad && statusVenta != "2")
                                    {
                                        var producto = fila.Cells["Descripcion"].Value;

                                        MessageBox.Show($"El stock de {producto} es insuficiente\nStock actual: {stock}\nRequerido: {cantidad}", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        respuesta = false;

                                        break;
                                    }
                                }

                                // Es servicio o paquete
                                if (tipoPS == "S" || tipoPS == "PQ")
                                {
                                    var servicio = fila.Cells["Descripcion"].Value;
                                    var idServicio = Convert.ToInt32(fila.Cells["IDProducto"].Value);
                                    var categoria = string.Empty;

                                    if (tipoPS == "S") { categoria = "Servicio"; }
                                    if (tipoPS == "PQ") { categoria = "Paquete"; }

                                    // Obtener los productos relacionados (ID, Cantidad)
                                    var datosServicio = cn.ObtenerProductosServicio(idServicio);

                                    if (datosServicio.Length > 0)
                                    {
                                        // Verificar la cantidad de cada producto con el stock actual de ese producto individual
                                        foreach (string producto in datosServicio)
                                        {
                                            var datosProducto = producto.Split('|');
                                            var idProducto = Convert.ToInt32(datosProducto[0]);

                                            if (idProducto > 0)
                                            {
                                                var stockRequerido = (int)Convert.ToDouble(datosProducto[1]) * cantidad;

                                                datosProducto = cn.VerificarStockProducto(idProducto, FormPrincipal.userID);
                                                datosProducto = datosProducto[0].Split('|');

                                                var nombreProducto = datosProducto[0];
                                                var stockActual = (int)Convert.ToDouble(datosProducto[1]);

                                                if (stockActual < stockRequerido)
                                                {
                                                    var mensaje = $"El stock de {nombreProducto} es insuficiente\n{categoria}: {servicio}\nStock actual: {stockActual}\nRequerido: {stockRequerido}";

                                                    MessageBox.Show(mensaje, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                                    respuesta = false;

                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            respuesta = false;
                        }
                    }
                    else
                    {
                        if (DGVentas.Rows.Count == 0)
                        {
                            respuesta = false;
                        }
                    }
                }
            }
            //if (Properties.Settings.Default.StockNegativo == false)
            //{
            //    if (DGVentas.Rows.Count > 0)
            //    {
            //        foreach (DataGridViewRow fila in DGVentas.Rows)
            //        {
            //            var stock = float.Parse(fila.Cells["Stock"].Value.ToString());
            //            var cantidad = float.Parse(fila.Cells["Cantidad"].Value.ToString());
            //            var tipoPS = fila.Cells["TipoPS"].Value.ToString();

            //            // Es producto
            //            if (tipoPS == "P")
            //            {
            //                if (stock < cantidad && statusVenta != "2")
            //                {
            //                    var producto = fila.Cells["Descripcion"].Value;

            //                    MessageBox.Show($"El stock de {producto} es insuficiente\nStock actual: {stock}\nRequerido: {cantidad}", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //                    respuesta = false;

            //                    break;
            //                }
            //            }

            //            // Es servicio o paquete
            //            if (tipoPS == "S" || tipoPS == "PQ")
            //            {
            //                var servicio = fila.Cells["Descripcion"].Value;
            //                var idServicio = Convert.ToInt32(fila.Cells["IDProducto"].Value);
            //                var categoria = string.Empty;

            //                if (tipoPS == "S") { categoria = "Servicio"; }
            //                if (tipoPS == "PQ") { categoria = "Paquete"; }

            //                // Obtener los productos relacionados (ID, Cantidad)
            //                var datosServicio = cn.ObtenerProductosServicio(idServicio);

            //                if (datosServicio.Length > 0)
            //                {
            //                    // Verificar la cantidad de cada producto con el stock actual de ese producto individual
            //                    foreach (string producto in datosServicio)
            //                    {
            //                        var datosProducto = producto.Split('|');
            //                        var idProducto = Convert.ToInt32(datosProducto[0]);
            //                        var stockRequerido = (int)Convert.ToDouble(datosProducto[1]) * cantidad;

            //                        datosProducto = cn.VerificarStockProducto(idProducto, FormPrincipal.userID);
            //                        datosProducto = datosProducto[0].Split('|');

            //                        var nombreProducto = datosProducto[0];
            //                        var stockActual = (int)Convert.ToDouble(datosProducto[1]);

            //                        if (stockActual < stockRequerido)
            //                        {
            //                            var mensaje = $"El stock de {nombreProducto} es insuficiente\n{categoria}: {servicio}\nStock actual: {stockActual}\nRequerido: {stockRequerido}";

            //                            MessageBox.Show(mensaje, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //                            respuesta = false;

            //                            break;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        respuesta = false;
            //    }
            //}
            //else
            //{
            //    if (DGVentas.Rows.Count == 0)
            //    {
            //        respuesta = false;
            //    }
            //}

            return respuesta;
        }

        private void btnVentasGuardadas_Click(object sender, EventArgs e)
        {
            

            if (Application.OpenForms.OfType<ListadoVentasGuardadas>().Count() == 1)
            {
                Application.OpenForms.OfType<ListadoVentasGuardadas>().First().BringToFront();
            }
            else
            {
                ListadoVentasGuardadas venta = new ListadoVentasGuardadas();

                venta.FormClosed += delegate
                {
                    if (mostrarVenta > 0)
                    {
                        // Verifica si los productos guardados tienen descuento
                        var datos = mb.ProductosGuardados(mostrarVenta);

                        if (datos.Count > 0)
                        {
                            if (!ComprobarDescuento(datos))
                            {
                                mostrarVenta = 0;
                                return;
                            }
                        }

                        CargarVentaGuardada();

                        ventasGuardadas.Add(mostrarVenta);

                        mostrarVenta = 0;
                    }
                };

                venta.Show();
            }
        }


        private void CargarVentaGuardada(bool ventaCancelada = false)
        {
            string[] datos = cn.BuscarVentaGuardada(mostrarVenta, FormPrincipal.userID);

            cSubtotal.Text = datos[0];
            cIVA.Text = datos[1];
            cTotal.Text = datos[2];
            cDescuento.Text = datos[3];

            string[] datosAnticipo = cn.BuscarAnticipo(mostrarVenta, FormPrincipal.userID);

            if (datosAnticipo.Length > 0)
            {
                cAnticipo.Text = datosAnticipo[1].ToString();
                cAnticipoUtilizado.Text = datosAnticipo[1].ToString();
            }

            // Cuando la venta guardada tiene descuento por cliente
            var idClienteDesc = Convert.ToInt32(datos[8]);
            var nombreCliente = datos[9].ToString();

            if (idClienteDesc > 0)
            {
                var datosCliente = mb.ObtenerDatosCliente(idClienteDesc, FormPrincipal.userID);
                var cliente = string.Empty;

                var auxPrimero = string.IsNullOrWhiteSpace(datosCliente[0]);
                var auxSegundo = string.IsNullOrWhiteSpace(datosCliente[1]);
                var auxTercero = string.IsNullOrWhiteSpace(datosCliente[17]);

                if (!auxPrimero)
                {
                    cliente += $"Cliente: {datosCliente[0]}";
                }
                if (!auxSegundo)
                {
                    cliente += $" --- RFC: {datosCliente[1]}";
                }
                if (!auxTercero)
                {
                    cliente += $" --- No. {datosCliente[17]}";
                }

                if (!ventaCancelada)
                {
                    lbDatosCliente.Text = cliente;
                    lbDatosCliente.Visible = true;
                    lbEliminarCliente.Visible = true;
                }
            }
            else
            {
                if (!ventaCancelada)
                {
                    lbDatosCliente.Text = nombreCliente;
                    lbDatosCliente.Visible = true;
                    lbEliminarCliente.Visible = true;
                }
            }

            //using (DataTable dtIdCliente = cn.CargarDatos(cs.getIdCliente(nombreCliente)))
            //{
            //    if (!dtIdCliente.Rows.Count.Equals(0))
            //    {
            //        idCliente = dtIdCliente.Rows[0]["ID"].ToString();
            //    }
            //}

            //MessageBox.Show("ID CLiente: " + idCliente);

            //Verificar si tiene productos la venta
            bool tieneProductos = (bool)cn.EjecutarSelect($"SELECT * FROM ProductosVenta WHERE IDVenta = '{mostrarVenta}'");

            if (tieneProductos)
            {
                string[] productos = cn.ObtenerProductosVenta(mostrarVenta);

                foreach (string producto in productos)
                {
                    string[] info = producto.Split('|');

                    var idProducto = Convert.ToInt32(info[0]);

                    string[] datosProducto = cn.BuscarProducto(idProducto, FormPrincipal.userID);

                    decimal cantidad = (decimal)float.Parse(info[2].ToString());

                    // Agregamos los descuentos directos de la venta guardada
                    if (Convert.ToInt32(info[4]) > 0)
                    {
                        descuentoGuardado = info[3].ToString();
                        if (!descuentosDirectos.ContainsKey(idProducto))
                        {
                            var tipoDescuento = Convert.ToInt32(info[4]);
                            var cantidadDescuento = 0f;

                            if (tipoDescuento == 3)
                            {
                                // Descuento cliente
                                if (info[3].Contains("-"))
                                {
                                    var cantidadTmp = info[3].Split('-');
                                    cantidadTmp[1] = cantidadTmp[1].Replace('%', ' ');
                                    cantidadDescuento = float.Parse(cantidadTmp[1].Trim());
                                }
                                else
                                {
                                    var cantidadTmp = info[3].ToString().Trim();
                                    cantidadDescuento = float.Parse(cantidadTmp);
                                }
                            }
                            else if (tipoDescuento == 2)
                            {
                                // Descuento directo
                                if (info[3].Contains("-"))
                                {
                                    var cantidadTmp = info[3].Split('-');
                                    cantidadTmp[1] = cantidadTmp[1].Replace('%', ' ');
                                    cantidadDescuento = float.Parse(cantidadTmp[1].Trim());
                                }
                                else
                                {
                                    var cantidadTmp = info[3].ToString().Trim();
                                    cantidadDescuento = float.Parse(cantidadTmp);
                                }
                            }
                            else if (tipoDescuento == 1)
                            {
                                // Descuento directo
                                cantidadDescuento = float.Parse(info[3].ToString().Trim());
                            }

                            descuentosDirectos.Add(idProducto, new Tuple<int, float>(tipoDescuento, cantidadDescuento));

                            datosProducto = new List<string>(datosProducto) { info[3] }.ToArray();
                        }
                    }
                    else if (Convert.ToInt32(info[4]).Equals(0))
                    {
                        var tipoDescuento = Convert.ToInt32(info[4]);
                        var cantidadDescuento = 0f;

                        if (tipoDescuento.Equals(0))
                        {
                            if (info[3].Contains("-"))
                            {
                                var cantidadTmp = info[3].Split('-');
                                cantidadTmp[1] = cantidadTmp[1].Replace('%', ' ');
                                cantidadDescuento = float.Parse(cantidadTmp[1].Trim());
                            }
                            else
                            {
                                var cantidadTmp = info[3].ToString().Trim();
                                cantidadDescuento = float.Parse(cantidadTmp);
                            }
                        }

                        descuentosDirectos.Add(idProducto, new Tuple<int, float>(tipoDescuento, cantidadDescuento));
                        datosProducto = new List<string>(datosProducto) { info[3] }.ToArray();
                    }

                    //AgregarProductoLista(datosProducto, cantidad, true);
                    AgregarProducto(datosProducto, cantidad);
                    nudCantidadPS.Value = 1;
                }

                foreach (DataGridViewRow fila in DGVentas.Rows)
                {
                    var idProdutoInactivo = fila.Cells["IDProducto"].Value.ToString();

                    using (DataTable dtProductoInactivo = cn.CargarDatos(cs.productoInactivo(idProdutoInactivo)))
                    {
                        if (!dtProductoInactivo.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtProductoInactivo.Rows)
                            {
                                productoDeshabilitado.Add($"{item["ID"].ToString()}|{item["Nombre"].ToString()}");
                            }
                        }
                    }
                }
                if (!productoDeshabilitado.Count.Equals(0))
                {
                    // Code to search the  alphanumneric Part Number (in Column1 header called "PART NUMBER") and highlihgt the row
                    foreach (var item in productoDeshabilitado)
                    {
                        var palabraParaBuscar = item.Split('|');

                        foreach (DataGridViewRow row in DGVentas.Rows)
                        {
                            var contenidoDeCelda = row.Cells["Descripcion"].Value.ToString();
                            if (!string.IsNullOrWhiteSpace(contenidoDeCelda) && contenidoDeCelda.Equals(palabraParaBuscar[1].ToString()))
                            {
                                DGVentas.Rows[row.Index].DefaultCellStyle.BackColor = Color.DarkSlateGray;
                            }
                        }
                    }
                    productoDeshabilitado.Clear();
                }
            }

            //Descuento general
            if (float.Parse(datos[4]) > 0)
            {
                var porcentaje = float.Parse(datos[4]);
                var resultado = porcentaje * 100;
                txtDescuentoGeneral.Text = resultado.ToString();
                DescuentoGeneral();
            }
        }

        private bool ComprobarDescuento(Dictionary<int, string> guardados)
        {
            bool respuesta = true;

            foreach (DataGridViewRow fila in DGVentas.Rows)
            {
                var idProducto = Convert.ToInt32(fila.Cells["IDProducto"].Value.ToString());

                if (guardados.ContainsKey(idProducto))
                {
                    var nombre = fila.Cells["Descripcion"].Value.ToString();
                    var descuentoListado = fila.Cells["Descuento"].Value.ToString();
                    var descuentoGuardado = guardados[idProducto];

                    if (!descuentoListado.Equals("0.00") || !descuentoGuardado.Equals("0.00"))
                    {
                        respuesta = false;
                        MessageBox.Show($"No se puede cargar la venta guardada porque\nel siguiente producto ya tiene un descuento\n\n{nombre}", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                }
            }

            return respuesta;
        }

        private void Ventas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DGVentas.RowCount > 0)
            {
                var respuesta = MessageBox.Show("¿Estás seguro de cerrar la ventana?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    if (ventaFinalizada.Equals(false))
                    {
                        // Ejecutar hilo para enviar notificación por correo
                        var datosConfig = mb.ComprobarConfiguracion();

                        if (datosConfig.Count > 0)
                        {
                            if (Convert.ToInt32(datosConfig[15]).Equals(1))
                            {
                                List<string> productosNoVendidos = new List<string>();
                                string fechaSistema = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), importeTotal = string.Empty;

                                foreach (DataGridViewRow dgvRenglon in DGVentas.Rows)
                                {
                                    productosNoVendidos.Add(dgvRenglon.Cells["Cantidad"].Value.ToString() + "|" + dgvRenglon.Cells["Precio"].Value.ToString() + "|" + dgvRenglon.Cells["Descripcion"].Value.ToString() + "|" + dgvRenglon.Cells["Descuento"].Value.ToString() + "|" + dgvRenglon.Cells["Importe"].Value.ToString());
                                }

                                importeTotal = cTotal.Text.ToString();

                                Thread NotSuccessfulSale = new Thread(
                                    () => Utilidades.ventaNotSuccessfulFinalizadaEmail(productosNoVendidos, fechaSistema, importeTotal, FormPrincipal.datosUsuario)
                                );

                                NotSuccessfulSale.Start();
                            }
                        }

                        mostrarVenta = 0;
                        listaAnticipos = string.Empty;
                        ventasGuardadas.Clear();
                        descuentosDirectos.Clear();

                    }
                    else
                    {
                        mostrarVenta = 0;
                        listaAnticipos = string.Empty;
                        ventasGuardadas.Clear();
                        descuentosDirectos.Clear();
                    }
                    PorcentajeDescuento = "";
                    AplicarCantidad = "";
                    AplicarPorcentaje = "";
                    BasculaCom.Close();
                }
            }
            else
            {
                mostrarVenta = 0;
                listaAnticipos = string.Empty;
                ventasGuardadas.Clear();
                descuentosDirectos.Clear();
                PorcentajeDescuento = "";
                AplicarCantidad = "";
                AplicarPorcentaje = "";
                BasculaCom.Close();
            }
            //PuertoSerieBascula.Close();
            listProductos.Clear();
            liststock.Clear();
        }

        private void GenerarTicket(string[][] productos)
        {
            var datos = FormPrincipal.datosUsuario;

            // Medidas de ticket de 57 y 80 mm
            // 1 pulgada = 2.54 cm = 72 puntos = 25.4 mm
            // 57mm = 161.28 pt
            // 80mm = 226.08 pt

            var tipoPapel = 80;
            var anchoPapel = Convert.ToInt32(Math.Floor((((tipoPapel * 0.10) * 72) / 2.54)));
            var altoPapel = Convert.ToInt32(anchoPapel + 72); // 54 64 68

            if (productos.Length > 3)
            {
                var filas = productos.Length / 2.54;
                filas *= 25.4;
                altoPapel += Convert.ToInt32(filas);

                for (int i = 0; i < productos.Length; i++)
                {
                    if (productos[i][2].Length > 18)
                    {
                        altoPapel += 20;
                    }
                }
            }

            //Variables y arreglos para el contenido de la tabla
            float[] anchoColumnas = new float[] { };

            string txtFormaPago = string.Empty;
            string strFormaPago = string.Empty;
            string txtDescripcion = string.Empty;
            string txtCantidad = string.Empty;
            string txtImporte = string.Empty;
            string txtPrecio = string.Empty;
            // Descuento general
            string txtDesc = string.Empty;
            string salto = string.Empty;

            int medidaFuenteMensaje = 0;
            int medidaFuenteNegrita = 0;
            int medidaFuenteNormal = 0;
            int medidaFuenteGrande = 0;

            int separadores = 0;
            int anchoLogo = 0;
            int altoLogo = 0;
            int espacio = 0;

            //if (statusVenta.Equals("1"))
            //{
            //    strFormaPago = "Efectivo";
            //}
            //else if (statusVenta.Equals("2"))
            //{
            //    strFormaPago = "Presupuesto";
            //}
            //else if (statusVenta.Equals("3"))
            //{
            //    strFormaPago = "Cancelada";
            //}
            //else if (statusVenta.Equals("4"))
            //{
            //    strFormaPago = "Crédito";
            //}
            //else if (statusVenta.Equals("5"))
            //{
            //    strFormaPago = "Factura";
            //}
            //else if (statusVenta.Equals("6"))
            //{
            //    strFormaPago = "Presupuestos";
            //}

            if (tipoPapel == 80)
            {
                anchoColumnas = new float[] { 7f, 24f, 10f, 10f, 10f };
                txtFormaPago = "Forma de pago:";
                txtDescripcion = "Descripción";
                txtCantidad = "Cant.";
                txtImporte = "Imp.";
                txtPrecio = "Precio";
                txtDesc = "Desc.";
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
                anchoColumnas = new float[] { 7f, 20f, 11f, 11f, 13f };
                txtFormaPago = "Forma de pago:";
                txtDescripcion = "Descripción";
                txtImporte = "Imp.";
                txtCantidad = "Cant.";
                txtPrecio = "Prec.";
                txtDesc = "Desc.";
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

            var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Ventas\Tickets\ticket_venta_" + productos[0][0] + ".pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Ventas\Tickets\ticket_venta_" + productos[0][0] + ".pdf";
            }


            Document ticket = new Document(new iTextSharp.text.Rectangle(anchoPapel, altoPapel), 3, 3, 3, 0);
            PdfWriter writer = PdfWriter.GetInstance(ticket, new FileStream(rutaArchivo, FileMode.Create));

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteNormal);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, medidaFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteGrande);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteMensaje);

            string logotipo = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                logotipo = $@"\\{servidor}\Archivos PUDVE\MisDatos\Usuarios\" + datos[11];
            }
            else
            {
                logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + datos[11];
            }

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
            var nomComercial = cn.CargarDatos($"SELECT  nombre_comercial FROM usuarios WHERE ID = '{FormPrincipal.userID}' ");
            string nombreComercial = nomComercial.Rows[0]["nombre_comercial"].ToString();

            Paragraph titulo = new Paragraph(datos[0] + "\n", fuenteGrande);
            Paragraph NombreComercial = new Paragraph("Nombre Comercial: " + nombreComercial + "\n", fuenteNormal);
            Paragraph direccion = new Paragraph("Direccion: " + datos[1] + ", " + datos[2] + ", " + datos[4] + ", " + datos[5] + "\n", fuenteNormal);
            Paragraph colYCP = new Paragraph("Colonia: " + datos[6] + ", " + "C.P.: " + datos[7] + "\n", fuenteNormal);
            Paragraph RFC = new Paragraph("RFC: " + datos[8] + "\n", fuenteNormal);
            Paragraph correo = new Paragraph("Correo: " + datos[9] + "\n", fuenteNormal);
            Paragraph telefono = new Paragraph("Telefono: " + datos[10] + "\n" + "\n", fuenteNormal);
            //Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            NombreComercial.Alignment = Element.ALIGN_CENTER;
            direccion.Alignment = Element.ALIGN_CENTER;
            colYCP.Alignment = Element.ALIGN_CENTER;
            RFC.Alignment = Element.ALIGN_CENTER;
            correo.Alignment = Element.ALIGN_CENTER;
            telefono.Alignment = Element.ALIGN_CENTER;
            //domicilio.Alignment = Element.ALIGN_CENTER;
            //domicilio.SetLeading(espacio, 0);


            Paragraph cliente = new Paragraph($"{productos[0][14]}", fuenteNormal);

            string datosCliente = string.Empty;
            string RFCcliente = string.Empty;
            string domicilioCliente = string.Empty;
            string correoCliente = string.Empty;
            string telefonoCliente = string.Empty;
            string colYCPCliente = string.Empty;

            if (!productos[0][16].Equals("0"))
            {
                var infoCliente = mb.ObtenerDatosCliente(Convert.ToInt32(productos[0][16]), FormPrincipal.userID);

                if (infoCliente.Length > 0)
                {
                    RFCcliente = $"RFC: {infoCliente[1]}";
                    datosCliente += $"{infoCliente[1]}\n";
                    datosCliente += $"Domicilio: ";

                    if (!string.IsNullOrWhiteSpace(infoCliente[10]))
                    {
                        domicilioCliente = $"{infoCliente[10]}, ";
                        datosCliente += $"{infoCliente[10]}, ";
                    }

                    if (!string.IsNullOrWhiteSpace(infoCliente[11]))
                    {
                        domicilioCliente += $"{ infoCliente[11]}, ";
                        datosCliente += $"{infoCliente[11]}, ";
                    }

                    if (!string.IsNullOrWhiteSpace(infoCliente[12]))
                    {
                        datosCliente += $"Int. {infoCliente[12]}, ";
                    }

                    if (!string.IsNullOrWhiteSpace(infoCliente[9]))
                    {
                        colYCPCliente = $"Col. {infoCliente[9]}, ";
                        datosCliente += $"Col. {infoCliente[9]}, ";
                    }

                    if (!string.IsNullOrWhiteSpace(infoCliente[8]))
                    {
                        colYCPCliente += $"C.P. { infoCliente[8]}";
                        datosCliente += $"C.P. {infoCliente[8]}, ";
                    }

                    if (!string.IsNullOrWhiteSpace(infoCliente[7]))
                    {
                        domicilioCliente += $"{infoCliente[7]}, ";
                        datosCliente += $"{infoCliente[7]}, ";
                    }

                    if (!string.IsNullOrWhiteSpace(infoCliente[6]))
                    {
                        datosCliente += $"{infoCliente[6]}, ";
                    }

                    if (!string.IsNullOrWhiteSpace(infoCliente[5]))
                    {
                        domicilioCliente += $"{infoCliente[5]}, ";
                        datosCliente += $"{infoCliente[5]}, ";
                    }

                    if (!string.IsNullOrWhiteSpace(infoCliente[4]))
                    {
                        domicilioCliente += $"{infoCliente[4]}";
                        datosCliente += $"{infoCliente[4]}";
                    }

                    if (!string.IsNullOrWhiteSpace(infoCliente[13]))
                    {
                        correoCliente = $"{infoCliente[13]}";
                        datosCliente += $"{infoCliente[13]}";
                    }
                    if (!string.IsNullOrWhiteSpace(infoCliente[14]))
                    {
                        telefonoCliente = $" {infoCliente[14]}";
                    }

                    datosCliente = datosCliente.TrimEnd(',');
                }
            }

            Paragraph colCliente = new Paragraph($"{datosCliente}", fuenteNormal);
            Paragraph referencia = new Paragraph($"Referencia: {productos[0][15]}", fuenteNormal);
            Paragraph FormPago = new Paragraph(txtFormaPago + " " + productos[0][13] + "\n\n", fuenteNormal);
            Paragraph formapagoCliente = new Paragraph(txtFormaPago + " " + productos[0][13]);
            Paragraph RFCclientep = new Paragraph("RFC: " + RFCcliente, fuenteNormal);
            Paragraph domicilioClientep = new Paragraph("Domicilio: " + domicilioCliente, fuenteNormal);
            Paragraph correoClientep = new Paragraph("Correo: " + correoCliente, fuenteNormal);
            Paragraph telefonoClientep = new Paragraph("Telefono: " + telefonoCliente, fuenteNormal);
            Paragraph colYCPp = new Paragraph("Colonia y C.P.: " + colYCPCliente, fuenteNormal);

            /**************************************"
             ** Tabla con los productos vendidos **
             **************************************/

            PdfPTable tabla = new PdfPTable(5);
            tabla.WidthPercentage = 100;
            tabla.SetWidths(anchoColumnas);

            PdfPCell colCantidad = new PdfPCell(new Phrase(txtCantidad, fuenteNegrita));
            colCantidad.BorderWidth = 0;

            PdfPCell colDescripcion = new PdfPCell(new Phrase(txtDescripcion, fuenteNegrita));
            colDescripcion.BorderWidth = 0;

            PdfPCell colPrecio = new PdfPCell(new Phrase(txtPrecio, fuenteNegrita));
            colPrecio.BorderWidth = 0;

            PdfPCell colDesc = new PdfPCell(new Phrase(txtDesc, fuenteNegrita));
            colDesc.BorderWidth = 0;

            PdfPCell colImporte = new PdfPCell(new Phrase(txtImporte, fuenteNegrita));
            colImporte.BorderWidth = 0;

            tabla.AddCell(colCantidad);
            tabla.AddCell(colDescripcion);
            tabla.AddCell(colPrecio);
            tabla.AddCell(colDesc);
            tabla.AddCell(colImporte);

            PdfPCell separadorInicial = new PdfPCell(new Phrase(new string('-', separadores), fuenteNormal));
            separadorInicial.BorderWidth = 0;
            separadorInicial.Colspan = 5;

            tabla.AddCell(separadorInicial);

            float descuentoProductos = 0;
            float descuentoGeneral = 0;
            float totalDescuento = float.Parse(productos[0][8]);
            float totalTicket = float.Parse(productos[0][9]);
            float totalAnticipo = float.Parse(productos[0][11]);

            var longitud = productos.Length;

            for (int i = 0; i < longitud; i++)
            {
                PdfPCell colCantidadTmp = new PdfPCell(new Phrase(productos[i][3], fuenteNormal));
                colCantidadTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                colCantidadTmp.BorderWidth = 0;

                PdfPCell colDescripcionTmp = new PdfPCell(new Phrase(productos[i][2], fuenteNormal));
                colDescripcionTmp.BorderWidth = 0;

                PdfPCell colPrecioTmp = new PdfPCell(new Phrase("$" + float.Parse(productos[i][4]).ToString("0.00"), fuenteNormal));
                colPrecioTmp.BorderWidth = 0;

                // Convertimos el descuento en array para poder mostrar el porcentaje y sumar
                // el descuento a la variable del total descuentoProductos
                var descuentoAux = productos[i][6].Split('-');

                float descuento = float.Parse(descuentoAux[0].Trim());

                var cadenaDescuento = string.Empty;

                cadenaDescuento += descuento.ToString("0.00");

                if (1 < descuentoAux.Length)
                {
                    cadenaDescuento += $" - {descuentoAux[1].Trim()}";
                }

                float importe = float.Parse(productos[i][7]);

                descuentoProductos += descuento;

                PdfPCell colDescTmp = new PdfPCell(new Phrase("$" + cadenaDescuento, fuenteNormal));
                colDescTmp.BorderWidth = 0;

                PdfPCell colImporteTmp = new PdfPCell(new Phrase("$" + importe.ToString("0.00"), fuenteNormal));
                colImporteTmp.BorderWidth = 0;

                tabla.AddCell(colCantidadTmp);
                tabla.AddCell(colDescripcionTmp);
                tabla.AddCell(colPrecioTmp);
                tabla.AddCell(colDescTmp);
                tabla.AddCell(colImporteTmp);
            }

            PdfPCell separadorFinal = new PdfPCell(new Phrase(new string('-', separadores), fuenteNormal));
            separadorFinal.BorderWidth = 0;
            separadorFinal.Colspan = 5;

            PdfPCell colTotalAnticipo = new PdfPCell(new Phrase("Anticipo: $" + totalAnticipo.ToString("0.00"), fuenteNormal));
            colTotalAnticipo.BorderWidth = 0;
            colTotalAnticipo.HorizontalAlignment = Element.ALIGN_RIGHT;
            colTotalAnticipo.Colspan = 5;

            PdfPCell colTotalDescuento = new PdfPCell(new Phrase("Descuento productos: $" + descuentoProductos.ToString("0.00"), fuenteNormal));
            colTotalDescuento.BorderWidth = 0;
            colTotalDescuento.HorizontalAlignment = Element.ALIGN_RIGHT;
            colTotalDescuento.Colspan = 5;

            var descuentoG = txtDescuentoGeneral.Text;
            descuentoGeneral = totalDescuento - descuentoProductos;

            PdfPCell colDescuentoGeneral = new PdfPCell(new Phrase($"Descuento general ({descuentoG}%): $" + descuentoGeneral.ToString("0.00"), fuenteNormal));
            colDescuentoGeneral.BorderWidth = 0;
            colDescuentoGeneral.HorizontalAlignment = Element.ALIGN_RIGHT;
            colDescuentoGeneral.Colspan = 5;

            PdfPCell totalVenta = new PdfPCell(new Phrase("TOTAL: $" + totalTicket.ToString("0.00"), fuenteNormal));
            totalVenta.BorderWidth = 0;
            totalVenta.HorizontalAlignment = Element.ALIGN_RIGHT;
            totalVenta.Colspan = 5;

            tabla.AddCell(separadorFinal);

            if (totalAnticipo > 0)
            {
                tabla.AddCell(colTotalAnticipo);
            }

            tabla.AddCell(colTotalDescuento);

            if (descuentoGeneral > 0)
            {
                tabla.AddCell(colDescuentoGeneral);
            }

            tabla.AddCell(totalVenta);

            /******************************************
             ** Fin tabla con los productos vendidos **
             ******************************************/
            var checkboxTicket = cn.CargarDatos($"SELECT * FROM `editarticket` WHERE IDUsuario = '{FormPrincipal.userID}'");

            foreach (DataRow item in checkboxTicket.Rows)
            {
                var datos2 = item;
                nombreus = Convert.ToInt32(datos2[3]);
                nombComercial = Convert.ToInt32(datos2[17]);
                direccionus = Convert.ToInt32(datos2[4]);
                colycpus = Convert.ToInt32(datos2[5]);
                rfcus = Convert.ToInt32(datos2[6]);
                correous = Convert.ToInt32(datos2[7]);
                telefonous = Convert.ToInt32(datos2[8]);

                nombrec = Convert.ToInt32(datos2[9]);
                domicilioc = Convert.ToInt32(datos2[10]);
                rfcc = Convert.ToInt32(datos2[11]);
                correoc = Convert.ToInt32(datos2[12]);
                telefonoc = Convert.ToInt32(datos2[13]);
                colycpc = Convert.ToInt32(datos2[14]);
                formapagoc = Convert.ToInt32(datos2[15]);
            }

            var mensaje2 = cn.CargarDatos(cs.MensajeTicket(FormPrincipal.userID));
            foreach (DataRow item in mensaje2.Rows)
            {
                cargarmensaje = item[0].ToString();
            }
            var mensajeTicket = cargarmensaje;

            Paragraph mensaje = new Paragraph(mensajeTicket, fuenteNormal);
            mensaje.Alignment = Element.ALIGN_CENTER;

            var culture = new System.Globalization.CultureInfo("es-MX");
            var dia = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
            var fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm tt");

            dia = cn.Capitalizar(dia);

            Paragraph diaVenta = new Paragraph($"{dia} - {fecha} - Folio: {productos[0][10]}", fuenteNormal);
            diaVenta.Alignment = Element.ALIGN_CENTER;
            if (nombreus == 1)
            {
                ticket.Add(titulo);
            }
            if (nombComercial == 1)
            {
                ticket.Add(NombreComercial);
            }
            if (direccionus == 1)
            {
                ticket.Add(direccion);
            }
            if (colycpus == 1)
            {
                ticket.Add(colYCP);
            }
            if (rfcus == 1)
            {
                ticket.Add(RFC);
            }
            if (correous == 1)
            {
                ticket.Add(correo);
            }
            if (telefonous == 1)
            {
                ticket.Add(telefono);
            }

            //ticket.Add(domicilio);

            /////////////////////////////DATOS DEL  CLIENTE "TICKET"////////////////////////////////
            if (!string.IsNullOrWhiteSpace(productos[0][14]))
            {
                if (nombrec == 1)
                {
                    ticket.Add(cliente);
                }

            }

            if (!string.IsNullOrWhiteSpace(datosCliente))
            {
                if (rfcc == 1)
                {
                    ticket.Add(RFCclientep);
                }
                if (domicilioc == 1)
                {
                    ticket.Add(domicilioClientep);
                }
                if (colycpc == 1)
                {
                    ticket.Add(colYCPp);
                }
                if (correoc == 1)
                {
                    ticket.Add(correoClientep);
                }
                if (telefonoc == 1)
                {
                    ticket.Add(telefonoClientep);
                }

                //ticket.Add(formapagoCliente);
                //ticket.Add(colCliente);
            }

            if (!string.IsNullOrWhiteSpace(productos[0][15]))
            {
                ticket.Add(referencia);
            }
            if (formapagoc == 1)
            {
                ticket.Add(FormPago);
            }

            ticket.Add(tabla);//De la venta
            ticket.Add(mensaje);

            // Imprimir codigo de barras en el ticket
            if (imprimirCodigo)
            {
                // Generar el codigo de barras
                var codigoBarra = GenerarCodigoBarras(productos[0][10], anchoPapel);

                iTextSharp.text.Image imagenCodigo = iTextSharp.text.Image.GetInstance(codigoBarra, System.Drawing.Imaging.ImageFormat.Jpeg);
                imagenCodigo.Alignment = Element.ALIGN_CENTER;

                ticket.Add(imagenCodigo);
            }

            ticket.Add(diaVenta);
            ticket.AddTitle("Ticket Venta");
            ticket.AddAuthor("PUDVE");
            ticket.Close();
            writer.Close();
        }

        private void GenerarTicketCaja()
        {
            int folioTicket = Properties.Settings.Default.folioAbrirCaja + 1;

            Properties.Settings.Default.folioAbrirCaja = folioTicket;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            var datos = FormPrincipal.datosUsuario;

            // Medidas de ticket de 57 y 80 mm
            // 1 pulgada = 2.54 cm = 72 puntos = 25.4 mm
            // 57mm = 161.28 pt
            // 80mm = 226.08 pt

            var tipoPapel = 80;
            var anchoPapel = Convert.ToInt32(Math.Floor((((tipoPapel * 0.10) * 72) / 2.54)));
            var altoPapel = Convert.ToInt32(anchoPapel + 64); // 54

            string txtDescripcion = string.Empty;
            string txtCantidad = string.Empty;
            string txtImporte = string.Empty;
            string txtPrecio = string.Empty;
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


            var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Ventas\Tickets\ticket_caja_abierta_" + folioTicket + ".pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Ventas\Tickets\ticket_caja_abierta_" + folioTicket + ".pdf";
            }

            Document ticket = new Document(new iTextSharp.text.Rectangle(anchoPapel, altoPapel), 3, 3, 3, 0);
            PdfWriter writer = PdfWriter.GetInstance(ticket, new FileStream(rutaArchivo, FileMode.Create));

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteNormal);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, medidaFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteGrande);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteMensaje);

            string logotipo = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                logotipo = $@"\\{servidor}\Archivos PUDVE\MisDatos\Usuarios\" + datos[11];
            }
            else
            {
                logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + datos[11];
            }

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

            var fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt");

            Paragraph mensaje = new Paragraph("\n*** CAJA ABIERTA ***\n" + fecha, fuenteGrande);
            mensaje.Alignment = Element.ALIGN_CENTER;

            ticket.Add(titulo);
            ticket.Add(domicilio);
            ticket.Add(mensaje);

            ticket.AddTitle("Ticket Caja Abierta");
            ticket.AddAuthor("PUDVE");
            ticket.Close();
            writer.Close();

            ImprimirTicket(folioTicket.ToString(), 1);


        }

        private void ImprimirTicket(string idVenta, int tipo = 0)
        {
            try
            {
                var servidor = Properties.Settings.Default.Hosting;
                var ruta = string.Empty;

                if (tipo == 0)
                {
                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        ruta = $@"\\{servidor}\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}.pdf";
                    }
                    else
                    {
                        ruta = $@"C:\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}.pdf";
                    }

                    ProcessStartInfo info = new ProcessStartInfo();
                    info.Verb = "print";
                    info.FileName = ruta;
                    info.CreateNoWindow = true;
                    info.WindowStyle = ProcessWindowStyle.Hidden;

                    Process p = new Process();
                    p.StartInfo = info;
                    p.Start();
                    p.WaitForInputIdle();
                    Thread.Sleep(5000);

                    if (false == p.CloseMainWindow())
                    {
                        p.Kill();
                    }
                }

                if (tipo == 1)
                {
                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        ruta = $@"\\{servidor}\Archivos PUDVE\Ventas\Tickets\ticket_caja_abierta_{idVenta}.pdf";
                    }
                    else
                    {
                        ruta = $@"C:\Archivos PUDVE\Ventas\Tickets\ticket_caja_abierta_{idVenta}.pdf";
                    }

                    ProcessStartInfo pi = new ProcessStartInfo(ruta);
                    pi.UseShellExecute = true;
                    pi.Verb = "print";
                    Process process = Process.Start(pi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error No: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private System.Drawing.Image GenerarCodigoBarras(string txtCodigo, int ancho)
        {
            System.Drawing.Image imagen;

            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();

            try
            {
                var anchoTmp = ancho / 2;
                var auxiliar = anchoTmp;

                anchoTmp = auxiliar / 2;
                ancho = ancho - anchoTmp;

                imagen = codigo.Encode(BarcodeLib.TYPE.CODE128, txtCodigo, Color.Black, Color.White, ancho, 40);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar código de barras para el ticket", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                imagen = null;
            }

            return imagen;
        }

        private void btnAnticipos_Click(object sender, EventArgs e)
        {
            if (opcion11 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<ListadoAnticipos>().Count() == 1)
            {
                Application.OpenForms.OfType<ListadoAnticipos>().First().BringToFront();
            }
            else
            {
                ListadoAnticipos anticipo = new ListadoAnticipos(DGVentas.Rows.Count);

                anticipo.FormClosed += delegate
                {
                    if (importeAnticipo > 0)
                    {
                        CantidadesFinalesVenta();
                        importeAnticipo = 0f;
                        btnEliminarAnticipos.Visible = true;
                    }
                };

                anticipo.Show();
            }
        }

        private void btnUltimoTicket_Click(object sender, EventArgs e)
        {
            if (opcion14 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            try
            {
                var idVenta = cn.EjecutarSelect($"SELECT * FROM Ventas WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 ORDER BY ID DESC LIMIT 1", 1).ToString();

                if (Utilidades.AdobeReaderInstalado())
                {
                    ImprimirTicket(idVenta);
                }
                else
                {
                    Utilidades.MensajeAdobeReader();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Aun no se han creado Tickets", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPresupuesto_Click(object sender, EventArgs e)
        {
            if (opcion15 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            //Sin acciones
        }

        #region Expresiones regulares para buscar producto
        private string VerificarPatronesBusqueda(string cadena)
        {
            /*********************************************************************************************
            *                                                                                            * 
            *   --------------------------------------------------------------------------------------   *
            *   |   Expresion Regular                       |   Ejemplo         |   No: de Patron    |   *
            *   |---------------------------------------------------------------|---------------------   *
            *   |   (digito+espacioBlanco*espacioBlanco)    |   5 * 15665132    |   primerPatron     |   *
            *   |   ((Signo+)digito+ || digito+(Signo+))    |   +2 || 2+        |   segundoPatron    |   *
            *   |   ((Signo-)digito+ || digito+(Signo-))    |   -1 || 1-        |   tercerPatron     |   *
            *   |   (digito+(Signo*)+espacioBlanco)         |   5* 15665132     |   cuartoPatron     |   *
            *   |   (digito+(Signo*)+espacioBlanco)         |   5 *15665132     |   quintoPatron     |   *
            *   |   (digito+(Signo*)+espacioBlanco)         |   5*15665132      |   sextoPatron      |   *
            *   |   (#+espacioBlanco)                       |   # FolioDeVenta  |   septimoPatron    |   *
            *   --------------------------------------------------------------------------------------   *
            *                                                                                            *
            *********************************************************************************************/

            // No: de Patron
            string primerPatron = @"^\d+\s\*\s";
            string segundoPatron = @"^(\+\d+)|(\d+\+)|(\+)|(\+\+)$";
            string tercerPatron = @"^(\-\d+)|(\d+\-)|(\-)|(\-\-)$";
            string cuartoPatron = @"^\d+\*\s";
            string quintoPatron = @"^\d+\s\*";
            string sextoPatron = @"^-?[0-9]*\.?[0-9]+\*$";
            string septimoPatron = @"^.#\d+\.";
            //string septimoPatron = @"^#\s\d+";

            Match primeraCoincidencia = Regex.Match(cadena, primerPatron, RegexOptions.IgnoreCase);
            //Match octavaCoincidencia = Regex.Match(cadena, octavoPatron, RegexOptions.IgnoreCase);
            Match segundaCoincidencia = Regex.Match(cadena, segundoPatron, RegexOptions.IgnoreCase);
            Match terceraCoincidencia = Regex.Match(cadena, tercerPatron, RegexOptions.IgnoreCase);
            Match cuartaCoincidencia = Regex.Match(cadena, cuartoPatron, RegexOptions.IgnoreCase);
            Match quintaCoincidencia = Regex.Match(cadena, quintoPatron, RegexOptions.IgnoreCase);
            Match sextaCoincidencia = Regex.Match(cadena, sextoPatron, RegexOptions.IgnoreCase);
            Match septimaCoincidencia = Regex.Match(cadena, septimoPatron, RegexOptions.IgnoreCase);
            //Se necesita crear una clave para las ventas guardadas y cambiar la forma en que hara la buscada
            //usando enter, etc

            // Si encuentra coincidencia asigna la cantidad a la variable multiplicar 
            // y se visualiza en el campo numerico
            if (primeraCoincidencia.Success)
            {
                var resultado = primeraCoincidencia.Value.Trim().Split(' ');

                cantidadExtra = Convert.ToDecimal(resultado[0]);

                if (cantidadExtra != 0)
                {
                    if (cantidadExtra >= nudCantidadPS.Minimum && cantidadExtra <= nudCantidadPS.Maximum)
                    {
                        nudCantidadPS.Value = cantidadExtra;
                    }
                }

                cadena = Regex.Replace(cadena, primerPatron, string.Empty);
            }
            else if (segundaCoincidencia.Success)// AQUI ENTRA
            {
                bool checkFoundPlusAndDot = false;

                checkFoundPlusAndDot = verifiedContainsPlusSymbol(cadena);

                var estaDentroDelLimite = false;
                decimal esNumeroLaBusqueda;
                string vacia = string.Empty;
                estaDentroDelLimite = decimal.TryParse(cadena, out esNumeroLaBusqueda);//Se cambio el tipo de dato INT por DECIMAL ( CREO QUE ES AQUI )

                if (estaDentroDelLimite.Equals(false))
                {
                    MessageBox.Show("El valor que ingreso es mayor al limite permitido");
                    txtBuscadorProducto.Clear();
                    return vacia;
                }

                if (sumarProducto)
                {
                    if (checkFoundPlusAndDot)           //AQUI SE BRINCA CUANDO TIENE +9.9
                    {
                        var infoTmp = cadena.Split('+');
                        float cantidadExtraDecimal = 0;

                        if (!infoTmp[0].Equals(string.Empty))
                        {
                            cantidadExtraDecimal = (float)Convert.ToDouble(infoTmp[0].ToString());
                        }

                        if (!infoTmp[1].Equals(string.Empty))
                        {
                            cantidadExtraDecimal = (float)Convert.ToDouble(infoTmp[1].ToString());
                        }

                        cadena = Regex.Replace(cadena, segundoPatron, string.Empty);

                        //Verifica que exista algun producto o servicio en el datagridview
                        if (DGVentas.Rows.Count > 0)
                        {
                            if (cantidadExtraDecimal != 0)
                            {
                                //Si contiene un valor que este dentro del rango a los definidos del control NumericUpDown
                                var cantidad = (float)Convert.ToDouble(DGVentas.Rows[0].Cells["Cantidad"].Value);

                                int idprod = Convert.ToInt32(DGVentas.Rows[0].Cells["IDProducto"].Value);
                                var dato = cn.CargarDatos($"SELECT FormatoDeVenta FROM productos WHERE ID = '{idprod}' AND IDUSuario = '{FormPrincipal.userID}' AND Status = '1'");
                                var estado = dato.Rows[0]["FormatoDeVenta"].ToString();

                                if (estado == "1")
                                {
                                    decimal result = Convert.ToDecimal(cantidadExtraDecimal);
                                    if (result.ToString().Contains('.'))
                                    {
                                        MessageBox.Show("Este producto se vende solo por unidades enteras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return cadena;
                                    }
                                }

                                cantidad += cantidadExtraDecimal;

                                // Se agrego esta opcion para calcular bien las cantidades cuando se aplica descuento
                                float importe = cantidad * float.Parse(DGVentas.Rows[0].Cells["Precio"].Value.ToString());

                                DGVentas.Rows[0].Cells["Cantidad"].Value = cantidad;
                                DGVentas.Rows[0].Cells["Importe"].Value = importe;

                                // Se agrego esta parte de descuento
                                int idProducto = Convert.ToInt32(DGVentas.Rows[0].Cells["IDProducto"].Value);
                                int tipoDescuento = Convert.ToInt32(DGVentas.Rows[0].Cells["DescuentoTipo"].Value);

                                if (tipoDescuento > 0)
                                {
                                    string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                                    if (!datosDescuento.Equals(null) && datosDescuento.Length > 0)
                                    {
                                        CalcularDescuento(datosDescuento, tipoDescuento, (int)cantidad, 0);
                                    }
                                }

                                CalculoMayoreo();
                                CantidadesFinalesVenta();

                                cantidadExtraDecimal = 0;
                            }
                        }
                    }
                    else
                    {
                        var resultado = segundaCoincidencia.Value.Trim();

                        if (resultado.Equals("+") || resultado.Equals("++"))
                        {
                            cantidadExtra = 1;
                        }
                        else
                        {
                            var infoTmp = resultado.Split('+');

                            if (infoTmp[0] != string.Empty)
                            {
                                cantidadExtra = Convert.ToInt32(infoTmp[0]);
                            }
                            else
                            {
                                if (!DGVentas.Rows.Count.Equals(0))
                                {
                                    cantidadExtra = (int)Convert.ToDouble(infoTmp[1]);
                                }
                            }
                        }

                        cadena = Regex.Replace(cadena, segundoPatron, string.Empty);

                        //Verifica que exista algun producto o servicio en el datagridview
                        if (DGVentas.Rows.Count > 0)
                        {
                            if (cantidadExtra != 0)
                            {
                                //Si contiene un valor que este dentro del rango a los definidos del control NumericUpDown
                                if (cantidadExtra >= nudCantidadPS.Minimum && cantidadExtra <= nudCantidadPS.Maximum)
                                {
                                    //Se obtiene la cantidad del ultimo producto agregado para despues sumarse la que se puso con el comando
                                    var cantidad = Convert.ToDecimal(DGVentas.Rows[0].Cells["Cantidad"].Value.ToString());

                                    cantidad += cantidadExtra;

                                    // Se agrego esta opcion para calcular bien las cantidades cuando se aplica descuento
                                    decimal importe = cantidad * Convert.ToDecimal(DGVentas.Rows[0].Cells["Precio"].Value.ToString());

                                    DGVentas.Rows[0].Cells["Cantidad"].Value = cantidad;
                                    DGVentas.Rows[0].Cells["Importe"].Value = importe;

                                    // Se agrego esta parte de descuento
                                    int idProducto = Convert.ToInt32(DGVentas.Rows[0].Cells["IDProducto"].Value);
                                    int tipoDescuento = Convert.ToInt32(DGVentas.Rows[0].Cells["DescuentoTipo"].Value);

                                    if (tipoDescuento > 0)
                                    {
                                        var cantidadNueva = Decimal.Parse(cantidad.ToString(), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
                                        var cantidadInt = Convert.ToInt32(cantidadNueva);
                                        string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                                        if (!datosDescuento.Equals(null) && datosDescuento.Length > 0)
                                        {
                                            CalcularDescuento(datosDescuento, tipoDescuento, cantidadInt, 0);
                                        }
                                    }

                                    CalculoMayoreo();
                                    CantidadesFinalesVenta();

                                    cantidadExtra = 0;
                                }
                            }
                        }
                    }
                }
            }
            else if (terceraCoincidencia.Success)
            {
                bool checkFoundMinusAndDot = false;

                checkFoundMinusAndDot = verifiedContainsMinusSymbol(cadena);

                var estaDentroDelLimite = false;
                decimal esNumeroLaBusqueda = 0;
                string vacia = string.Empty;
                estaDentroDelLimite = decimal.TryParse(cadena, out esNumeroLaBusqueda);

                if (estaDentroDelLimite.Equals(false))
                {
                    MessageBox.Show("El valor que ingreso es mayor al limite permitido");
                    txtBuscadorProducto.Clear();
                    return vacia;
                }

                if (restarProducto)
                {
                    if (checkFoundMinusAndDot)
                    {
                        var infoTmp = cadena.Split('-');
                        float cantidadExtraDecimal = 0;

                        if (!infoTmp[0].Equals(string.Empty))
                        {
                            cantidadExtraDecimal = (float)Convert.ToDouble(infoTmp[0].ToString()) * -1;
                        }
                        else if (!infoTmp[1].Equals(string.Empty))
                        {
                            cantidadExtraDecimal = (float)Convert.ToDouble(infoTmp[1].ToString()) * -1;
                        }

                        cadena = Regex.Replace(cadena, tercerPatron, string.Empty);

                        //Verifica que exista algun producto o servicio en el datagridview
                        if (DGVentas.Rows.Count > 0)
                        {
                            if (cantidadExtraDecimal != 0)
                            {
                                //Si contiene un valor que este dentro del rango a los definidos del control NumericUpDown
                                if (cantidadExtraDecimal >= (float)nudCantidadPS.Minimum && cantidadExtraDecimal <= (float)nudCantidadPS.Maximum)
                                {
                                    //Se obtiene la cantidad del ultimo producto agregado para despues sumarse la que se puso con el comando
                                    var cantidad = float.Parse(DGVentas.Rows[0].Cells["Cantidad"].Value.ToString());

                                    float cantResult = cantidad + cantidadExtraDecimal;

                                    if (cantResult <= 0)
                                    {
                                        MessageBox.Show("La cantidad a vender debe ser mayor a 0", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        cantResult = 1;
                                    }
                                    // Se agrego esta opcion para calcular bien las cantidades cuando se aplica descuento
                                    float importe = cantResult * float.Parse(DGVentas.Rows[0].Cells["Precio"].Value.ToString());

                                    DGVentas.Rows[0].Cells["Cantidad"].Value = cantResult;
                                    DGVentas.Rows[0].Cells["Importe"].Value = importe;

                                    // Se agrego esta parte de descuento
                                    int idProducto = Convert.ToInt32(DGVentas.Rows[0].Cells["IDProducto"].Value);
                                    int tipoDescuento = Convert.ToInt32(DGVentas.Rows[0].Cells["DescuentoTipo"].Value);

                                    if (tipoDescuento > 0)
                                    {
                                        string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                                        if (!datosDescuento.Equals(null) && datosDescuento.Length > 0)
                                        {
                                            CalcularDescuento(datosDescuento, tipoDescuento, (int)cantidad, 0);
                                        }
                                    }

                                    if (cantidad <= 0)
                                    {
                                        DGVentas.Rows.RemoveAt(0);
                                    }

                                    CalculoMayoreo();
                                    CantidadesFinalesVenta();

                                    cantidadExtra = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        //var resultado = terceraCoincidencia.Value.Trim().Split('-');
                        var resultado = terceraCoincidencia.Value.Trim();

                        if (resultado.Equals("-") || resultado.Equals("--"))
                        {
                            cantidadExtra = -1;
                        }
                        else
                        {
                            var infoTmp = resultado.Split('-');

                            if (infoTmp[0] != string.Empty)
                            {
                                cantidadExtra = Convert.ToInt32(infoTmp[0]) * -1;
                            }
                            else
                            {
                                cantidadExtra = Convert.ToInt32(infoTmp[1]) * -1;
                            }
                        }

                        cadena = Regex.Replace(cadena, tercerPatron, string.Empty);

                        //Verifica que exista algun producto o servicio en el datagridview
                        if (DGVentas.Rows.Count > 0)
                        {
                            if (cantidadExtra != 0)
                            {
                                //Si contiene un valor que este dentro del rango a los definidos del control NumericUpDown
                                if (cantidadExtra >= nudCantidadPS.Minimum && cantidadExtra <= nudCantidadPS.Maximum)
                                {
                                    //Se obtiene la cantidad del ultimo producto agregado para despues sumarse la que se puso con el comando
                                    var cantidad = Convert.ToDecimal(DGVentas.Rows[0].Cells["Cantidad"].Value);

                                    cantidad += cantidadExtra;

                                    int idproductoCantidad = Convert.ToInt32(DGVentas.Rows[0].Cells["IDProducto"].Value);
                                    var MinimaCompra = cn.CargarDatos(cs.cantidadCompraMinima(Convert.ToInt32(idproductoCantidad)));
                                    if (!MinimaCompra.Rows.Count.Equals(0))
                                    {
                                        var cantidadMinima = Convert.ToInt32(MinimaCompra.Rows[0].ItemArray[0]);
                                        if (cantidad < cantidadMinima && listaMensajesEnviados.ContainsKey(Convert.ToInt32(idproductoCantidad)))
                                        {
                                            listaMensajesEnviados.Remove(Convert.ToInt32(idproductoCantidad));
                                        }
                                    }





                                    // Se agrego esta opcion para calcular bien las cantidades cuando se aplica descuento
                                    decimal importe = cantidad * Convert.ToDecimal(DGVentas.Rows[0].Cells["Precio"].Value.ToString());

                                    DGVentas.Rows[0].Cells["Cantidad"].Value = cantidad;
                                    DGVentas.Rows[0].Cells["Importe"].Value = importe;

                                    // Se agrego esta parte de descuento
                                    int idProducto = Convert.ToInt32(DGVentas.Rows[0].Cells["IDProducto"].Value);
                                    int tipoDescuento = Convert.ToInt32(DGVentas.Rows[0].Cells["DescuentoTipo"].Value);

                                    if (tipoDescuento > 0)
                                    {
                                        string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                                        if (!datosDescuento.Equals(null) && datosDescuento.Length > 0)
                                        {
                                            CalcularDescuento(datosDescuento, tipoDescuento, cantidad, 0);
                                        }
                                    }

                                    if (cantidad <= 0)
                                    {
                                        DGVentas.Rows.RemoveAt(0);
                                    }

                                    CalculoMayoreo();
                                    CantidadesFinalesVenta();

                                    cantidadExtra = 0;
                                }
                            }
                        }
                    }
                }
            }
            else if (cuartaCoincidencia.Success)
            {
                // 5* 652651651651
                var resultado = cuartaCoincidencia.Value.Trim().Split('*');

                cantidadExtra = Convert.ToInt32(resultado[0]);

                if (cantidadExtra != 0)
                {
                    if (cantidadExtra >= nudCantidadPS.Minimum && cantidadExtra <= nudCantidadPS.Maximum)
                    {
                        nudCantidadPS.Value = cantidadExtra;
                    }
                }

                cadena = Regex.Replace(cadena, cuartoPatron, string.Empty);
            }
            else if (quintaCoincidencia.Success)
            {
                // 5 *652651651651
                var resultado = quintaCoincidencia.Value.Trim().Split('*');

                cantidadExtra = Convert.ToInt32(resultado[0]);

                if (cantidadExtra != 0)
                {
                    if (cantidadExtra >= nudCantidadPS.Minimum && cantidadExtra <= nudCantidadPS.Maximum)
                    {
                        nudCantidadPS.Value = cantidadExtra;
                    }
                }

                cadena = Regex.Replace(cadena, quintoPatron, string.Empty);
            }
            else if (sextaCoincidencia.Success)
            {
                // 5*652651651651
                var resultado = sextaCoincidencia.Value.Trim().Split('*');

                cantidadExtra = Convert.ToDecimal(resultado[0]);

                if (cantidadExtra != 0)
                {
                    if (cantidadExtra >= nudCantidadPS.Minimum && cantidadExtra <= nudCantidadPS.Maximum)
                    {
                        nudCantidadPS.Value = cantidadExtra;
                    }
                }

                cadena = Regex.Replace(cadena, sextoPatron, string.Empty);
            }
            else if (septimaCoincidencia.Success)
            {
                // .#FolioDeVenta.

                if (buscarVG)
                {
                    var resultado = septimaCoincidencia.Value.Trim().Split('#');
                    resultado = resultado[1].Split('.');

                    buscarvVentaGuardada = ".#";
                    folio = resultado[0];
                }
            }
            //else if (octavaCoincidencia.Success)
            //{

            //}

            ocultarResultados();

            return cadena;
        }

        private bool verifiedContainsMinusSymbol(string cadena)
        {
            Regex regex1 = new Regex(@"^(\-\.\d+)");
            Regex regex2 = new Regex(@"^(\.\d+\-)");
            Regex regex3 = new Regex(@"^(\-\d\.\d+)");
            Regex regex4 = new Regex(@"^(\d\.\d+\-)");

            Match match1 = regex1.Match(cadena);
            Match match2 = regex2.Match(cadena);
            Match match3 = regex3.Match(cadena);
            Match match4 = regex4.Match(cadena);

            return match1.Success || match2.Success || match3.Success || match4.Success;
        }

        private bool verifiedContainsPlusSymbol(string cadena)
        {
            Regex regex1 = new Regex(@"^(\+\.\d+)");        
            Regex regex2 = new Regex(@"^(\.\d+\+)");        
            Regex regex3 = new Regex(@"^(\+\d+\.\d+)");    
            Regex regex4 = new Regex(@"^(\d+\.\d+\+)");    
            Regex regex5 = new Regex(@"^(\d+\.\d+)");     

            Match match1 = regex1.Match(cadena);
            Match match2 = regex2.Match(cadena);
            Match match3 = regex3.Match(cadena);
            Match match4 = regex4.Match(cadena);
            Match match5 = regex5.Match(cadena);

            return match1.Success || match2.Success || match3.Success || match4.Success || match5.Success;
        }
        #endregion

        private void Ventas_KeyDown(object sender, KeyEventArgs e)
        {
            if (listaProductos.Visible == true && !string.IsNullOrWhiteSpace(txtBuscadorProducto.Text))
            {
                if (listaProductos.Items.Count == 0)
                {
                    return;
                }

                // Presiono hacia arriba
                if (e.KeyCode == Keys.Up)
                {
                    listaProductos.Focus();

                    if (listaProductos.SelectedIndex > 0)
                    {
                        listaProductos.SelectedIndex--;
                        e.Handled = true;
                    }
                }

                // Presiono hacia abajo
                if (e.KeyCode == Keys.Down)
                {
                    listaProductos.Focus();

                    if (listaProductos.SelectedIndex < (listaProductos.Items.Count - 1))
                    {
                        listaProductos.SelectedIndex++;
                        e.Handled = true;
                    }
                }



            }
            if (e.KeyCode.Equals(Keys.End))
            {
                btnTerminarVenta.PerformClick();

            }

            // Tecla fin terminar venta
            //if (e.KeyData == Keys.End)
            //{
            //    if (noDuplicadoVentas == 0)
            //    {
            //        btnTerminarVenta.PerformClick();
            //    }
            //    else if (noDuplicadoVentas == 1)
            //    {

            //    }
            //    else
            //    {
            //        btnTerminarVenta.PerformClick();
            //    }
            //}

            // Cuando presione la tecla escape debera cerrar la ventana
            if (e.KeyData == Keys.Escape)
            {
                Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                //txtBuscadorProducto.Focus();
            }


            if (e.KeyCode == Keys.B && (e.Control))// Boton Consultar
            {
                btnConsultar.PerformClick();
            }
            else if (e.KeyCode == Keys.D && (e.Control))//Boton Descuento Cliente
            {
                btnClientes.PerformClick();
            }
            else if (e.KeyCode == Keys.G && (e.Control))//Boton Guardar
            {
                btnGuardarVenta.PerformClick();
            }
            else if (e.KeyCode == Keys.A && (e.Control))//Boton Anticipos
            {
                btnAnticipos.PerformClick();
            }
            else if (e.KeyCode == Keys.T && (e.Control))//Boton Tomar Peso desde Bascula
            {
                btnBascula.PerformClick();
            }
            else if (e.KeyCode == Keys.F2)//Boton Abrir Caja
            {
                btnAbrirCaja.PerformClick();
            }
            else if (e.KeyCode == Keys.M && (e.Control))//Boton Ventas Guardadas
            {
                btnVentasGuardadas.PerformClick();
            }
            else if (e.KeyCode == Keys.NumPad1 && (e.Alt) || e.KeyCode == Keys.D1 && (e.Alt))//Boton Eliminar
            {
                btnEliminarDescuentos.PerformClick();
            }
            else if (e.KeyCode == Keys.NumPad3 && (e.Alt) || e.KeyCode == Keys.D3 && (e.Alt))//Boton Aplicar
            {
                btnAplicarDescuento.PerformClick();
            }

        }

        private void OperacionBusqueda(int tipo = 0)

        {
            listaProductos.Items.Clear();

            string auxTxtBuscadorProducto = string.Empty, pattern = string.Empty, output = string.Empty;

            pattern = @"^(\.\d+)$";

            txtBuscadorProducto.Text = VerificarPatronesBusqueda(txtBuscadorProducto.Text);

            auxTxtBuscadorProducto = txtBuscadorProducto.Text;

            output = Regex.Replace(auxTxtBuscadorProducto, pattern, string.Empty);

            if (output.Equals(string.Empty))
            {
                txtBuscadorProducto.Text = string.Empty;
            }
            else
            {
                txtBuscadorProducto.Text = output;
            }

            if (txtBuscadorProducto.Text.Length == 0)
            {
                ocultarResultados();
                return;
            }

            if (auxTxtBuscadorProducto.Contains("+."))
            {
                return;
            }
            else
            {
                // Regresa un diccionario
                //var resultados = mb.BuscarProducto(txtBuscadorProducto.Text);
                var resultados = new Dictionary<int, string>();

                var coincidenciaExacta = cn.CargarDatos($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND STATUS = 1 AND CodigoBarras = '{txtBuscadorProducto.Text.Trim()}'");
                if (coincidenciaExacta.Rows.Count.Equals(0))
                {
                    resultados = mb.BusquedaCoincidenciasVentas(txtBuscadorProducto.Text.Trim(), filtro, mostrarPrecioProducto, mostrarCBProducto);
                }
                else
                {
                    resultados = mb.BusquedaCoincidenciaExacta(txtBuscadorProducto.Text.Trim(), filtro, mostrarPrecioProducto, mostrarCBProducto);
                }

                int coincidencias = resultados.Count;

                if (coincidencias > 0)
                {
                    // Guardamos los datos devueltos temporalmente en productosD
                    productosD = resultados;

                    // Calculamos la altura del listBox
                    var alturaLista = (coincidencias * 17) + 20;

                    listaProductos.Height = alturaLista;

                    // Si la cantidad de productos es mayor o igual a 15 establecemos una altura maxima para que haga scroll
                    if (coincidencias >= 15)
                    {
                        listaProductos.Height = 275;
                    }

                    foreach (var item in resultados)
                    {
                        listaProductos.Items.Add(item.Value);
                        listaProductos.Visible = true;
                        listaProductos.SelectedIndex = 0;
                    }
                    if (coincidenciaExacta.Rows.Count.Equals(1) && cambioCantidadProd.Equals(0))
                    {
                        SendKeys.Send("{ENTER}");
                    }

                    //foreach (var item in listaProductos.Items)
                    //{
                    //    var stockObtenido = 0f;
                    //    var stockMinim = 0f;
                    //    var name = string.Empty;

                    //    string[] datos = buscarProductoPorCodigoClave(item.ToString());

                    //    name = datos[0].ToString();
                    //    stockObtenido = float.Parse(datos[1]);
                    //    stockMinim = float.Parse(datos[2]);

                    //    if (stockObtenido <= stockMinim)
                    //    {
                    //        listaProductos.Font = new System.Drawing.Font("Century Gothic", 9F, FontStyle.Bold);
                    //        //listaProductos.Items[1].;

                    //    }
                    //    else if (stockObtenido < 1)
                    //    {

                    //    }
                    //    else
                    //    {
                    //        //listaProductos.Font = new System.Drawing.Font("Century Gothic", 8F);

                    //    }
                    //}


                }
                else
                {
                    if (tipo == 1 && string.IsNullOrEmpty(buscarvVentaGuardada))
                    {
                        // Se reproducto cuando el codigo o clave buscado no esta registrado
                        ReproducirSonido();

                        var respuesta = MessageBox.Show($"El código o clave {txtBuscadorProducto.Text} no esta registrado en el sistema", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        if (respuesta == DialogResult.OK)
                        {
                            txtBuscadorProducto.Text = string.Empty;
                            txtBuscadorProducto.Focus();
                        }
                    }
                }

                if (buscarvVentaGuardada == ".#")
                {
                    txtBuscadorProducto.Text = "";
                    string[] datosVentaGuardada = cn.ObtenerVentaGuardada(FormPrincipal.userID, Convert.ToInt32(folio));

                    var idVentaTmp = Convert.ToInt32(datosVentaGuardada[7]);

                    if (!ventasGuardadas.Contains(idVentaTmp))
                    {
                        ventasGuardadas.Add(idVentaTmp);
                        AgregarProducto(datosVentaGuardada);
                    }
                    else
                    {
                        MessageBox.Show($"La venta guardada con folio: {folio} ya\nfue seleccionada previamente", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    buscarvVentaGuardada = "";
                }

                if (listaProductos.Visible == true)
                {
                    this.KeyPreview = true;
                }
            }
        }


        private string[] buscarProductoPorCodigoClave(string fila)
        {
            List<string> lista = new List<string>();

            var nombre = string.Empty; var stock = string.Empty; var stockMinimo = string.Empty;

            char delimitador = ':';
            string[] words = fila.Split(delimitador);
            var eliminarCaracter = words[1].Replace("]", "");

            var palabra = eliminarCaracter.Trim().ToString();

            var query = cn.CargarDatos($"SELECT Nombre, Stock, StockMinimo FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND CodigoBarras = '{palabra}' OR ClaveProducto = '{palabra}' AND Status = 1");

            if (!query.Rows.Count.Equals(0))
            {
                nombre = query.Rows[0]["Nombre"].ToString();
                stock = query.Rows[0]["Stock"].ToString();
                stockMinimo = query.Rows[0]["StockMinimo"].ToString();

                lista.Add(nombre);
                lista.Add(stock);
                lista.Add(stockMinimo);
            }

            return lista.ToArray();
        }



        private void listaProductos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ProductoSeleccionado();
            nudCantidadPS.Value = 1;
            sonido = true;
            contador = 0;
            contadorMensaje = 0;
        }

        private void listaProductos_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            if (opcion12 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Utilidades.AdobeReaderInstalado())
            {
                GenerarTicketCaja();
            }
            else
            {
                Utilidades.MensajeAdobeReader();
            }
        }

        private void listaProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                ProductoSeleccionado();
                nudCantidadPS.Value = 1;
            }
            else
            {
                txtBuscadorProducto.Focus();
                txtBuscadorProducto.Select(txtBuscadorProducto.Text.Length, 0);
                listaProductos.Visible = false;
            }
        }

        private void timerBusqueda_Tick(object sender, EventArgs e)
        {
            timerBusqueda.Interval = 1000;
            timerBusqueda.Stop();
            //OperacionBusqueda();
            funteListBox();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            sonido = false;
            listaProductosVenta();
            cantidadAPedir = nudCantidadPS.Value.ToString();
            if (Application.OpenForms.OfType<ConsultarProductoVentas>().Count() == 1)
            {
                Application.OpenForms.OfType<ConsultarProductoVentas>().First().BringToFront();
            }
            else
            {
                var consulta = new ConsultarProductoVentas();

                consulta.FormClosing += delegate
                {
                    mostrarDatosTraidosBuscador();
                    reproducirProductoAgregado();
                    contadorMensaje = 0;
                    if (DGVentas.Rows.Count > 0)
                    {
                        btnCSV.Enabled = false;
                    }
                };

                //listProductos.Add(datosProducto[0] + "|" + cantidad.ToString());
                consulta.ShowDialog();
            }
            nudCantidadPS.Value = 1;
            //SendKeys.Send("{TAB}");
            //SendKeys.Send("{ENTER}");
        }

        private void listaProductosVenta()
        {
            liststock2.Clear();
            foreach (DataGridViewRow row in DGVentas.Rows)
            {
                liststock2.Add(row.Cells["Cantidad"].Value.ToString() + "|" + row.Cells["IDProducto"].Value.ToString());
            }
        }

        private void mostrarDatosTraidosBuscador()
        {
            var datoObtenidoBuscador = ConsultarProductoVentas.datosDeProducto;
            foreach (var producto in datoObtenidoBuscador)
            {
                datosProducto = producto.Split('|');

                if (!datoObtenidoBuscador.Count.Equals(0))
                {
                    if (datoObtenidoBuscador.Count.Equals(1))
                    {
                        var cantidadaPedir = ConsultarProductoVentas.cantidadPedida;

                        if (cantidadaPedir.Equals("Cancelar") || cantidadAPedir.Equals("return"))
                        {
                            return;
                        }
                        else
                        {
                            var cantidadCero = Convert.ToDecimal(cantidadaPedir.ToString());
                            if (string.IsNullOrWhiteSpace(cantidadaPedir.ToString()) || cantidadCero.Equals(0))//Se valida si la cantidad es igual a 0 o si viene vacio.
                            {
                            }
                            else
                            {
                                nudCantidadPS.Value = (long)Convert.ToDouble(cantidadaPedir);
                                AgregarProducto(datosProducto.ToArray(), Convert.ToDecimal(nudCantidadPS.Value));
                            }
                        }
                    }
                    else
                    {
                        var cantidadaPedir = ConsultarProductoVentas.cantidadPedida;
                        if (cantidadaPedir.Equals("Cancelar") || cantidadAPedir.Equals("return"))
                        {
                            return;
                        }
                        else
                        {
                            if (cantidadaPedir.Equals(string.Empty) || cantidadaPedir.Equals(0))
                            {
                                nudCantidadPS.Value = Convert.ToInt32(1);
                                AgregarProducto(datosProducto.ToArray(), Convert.ToDecimal(nudCantidadPS.Value));
                            }
                            else
                            {
                                nudCantidadPS.Value = Convert.ToDecimal(cantidadaPedir);
                                AgregarProducto(datosProducto.ToArray(), Convert.ToDecimal(nudCantidadPS.Value));
                            }
                        }
                    }
                }

            }
            ConsultarProductoVentas.datosDeProducto.Clear();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            if (opcion16 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (DGVentas.RowCount == 0)
            {
                MessageBox.Show("No hay productos agregados a la lista\npara aplicar el descuento", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var clientes = new ListaClientes(tipo: 1))
            {
                var respuesta = clientes.ShowDialog();

                if (respuesta == DialogResult.OK)
                {
                    var datos = clientes.datosCliente;
                    var cliente = string.Empty;

                    idCliente = datos[18];

                    var auxPrimero = string.IsNullOrWhiteSpace(datos[0]);
                    var auxSegundo = string.IsNullOrWhiteSpace(datos[1]);
                    var auxTercero = string.IsNullOrWhiteSpace(datos[17]);

                    if (!auxPrimero) { cliente += $"Cliente: {datos[0]}"; }
                    if (!auxSegundo) { cliente += $" --- RFC: {datos[1]}"; }
                    if (!auxTercero) { cliente += $" --- No. {datos[17]}"; }

                    var idTipoCliente = Convert.ToInt32(datos[16]);

                    idClienteDescuento = Convert.ToInt32(datos[18]);

                    if (idTipoCliente > 0)
                    {
                        var datosDescuento = mb.ObtenerTipoCliente(idTipoCliente);

                        if (datosDescuento.Length > 0)
                        {
                            descuentoCliente = float.Parse(datosDescuento[1]) / 100;
                            // Se reinicia a los valores por defecto el descuento general
                            porcentajeGeneral = 0;
                            txtDescuentoGeneral.Text = "% descuento";

                            CantidadesFinalesVenta();
                        }
                    }

                    lbDatosCliente.Text = cliente;
                    lbDatosCliente.Visible = true;
                    lbEliminarCliente.Visible = true;



                }
            }
        }

        private void DescuentoGeneral()
        {
            // Antigua funcionalidad del evento keyup del textBox descuentoGeneral
            if (!txtDescuentoGeneral.Text.Equals("% descuento"))
            {
                if (string.IsNullOrWhiteSpace(txtDescuentoGeneral.Text))
                {
                    return;
                }

                string[] words = txtDescuentoGeneral.Text.ToString().Split('%');

                if (words.Count() > 0)
                {
                    if (txtDescuentoGeneral.Text.Equals("\r\n% descuento"))
                    {
                        return;
                    }
                    var descuentoG = float.Parse(words[0].ToString());

                    if (descuentoG > 0)
                    {
                        // Reiniciamos a su valor por defecto la variable del descuento por cliente
                        descuentoCliente = 0;

                        porcentajeGeneral = descuentoG / 100;

                        productosDescuentoG.Clear();

                        foreach (DataGridViewRow fila in DGVentas.Rows)
                        {
                            var idProducto = Convert.ToInt32(fila.Cells["IDProducto"].Value);

                            if (!productosDescuentoG.ContainsKey(idProducto))
                            {
                                productosDescuentoG.Add(idProducto, true);
                            }
                        }

                        CantidadesFinalesVenta();
                    }
                }
                else if (words.Count() == 0)
                {
                    var descuentoG = float.Parse(words[0].ToString());

                    if (descuentoG > 0)
                    {
                        // Reiniciamos a su valor por defecto la variable del descuento por cliente
                        descuentoCliente = 0;

                        porcentajeGeneral = descuentoG / 100;

                        productosDescuentoG.Clear();

                        foreach (DataGridViewRow fila in DGVentas.Rows)
                        {
                            var idProducto = Convert.ToInt32(fila.Cells["IDProducto"].Value);

                            if (!productosDescuentoG.ContainsKey(idProducto))
                            {
                                productosDescuentoG.Add(idProducto, true);
                            }
                        }
                        CantidadesFinalesVenta();
                    }
                }
            }
        }

        private void btnAplicarDescuento_Click(object sender, EventArgs e)
        {
            if (txtDescuentoGeneral.Text.Equals("% descuento") || string.IsNullOrWhiteSpace(txtDescuentoGeneral.Text))
            {
                return;
            }
            descuentoDirectoPorAplicar = txtDescuentoGeneral.Text.Trim();
            btnEliminarDescuentos.PerformClick();
            txtDescuentoGeneral.Text = descuentoDirectoPorAplicar;
            productosDescuentoG.Clear();
            descuentosDirectos.Clear();
            if (!txtDescuentoGeneral.Text.Equals("."))
            {
                if (opcion19 == 0)
                {
                    Utilidades.MensajePermiso();
                    return;
                }

                DescuentoGeneral();

                //var mensaje = "¿Desea aplicar este descuento a los siguientes\nproductos que se agreguen a esta venta?";

                //var respuesta = MessageBox.Show(mensaje, "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //if (respuesta == DialogResult.Yes)
                //{
                //    aplicarDescuentoG = true;
                //}
                //else
                //{
                //    aplicarDescuentoG = false;
                //}
            }
            else
            {
                MessageBox.Show("Porfavor introduzca un porcentaje", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescuentoGeneral.Text = "% descuento";
            }
            txtBuscadorProducto.Focus();
            txtDescuentoGeneral.Text = "% descuento";
        }

        private void btnEliminarDescuentos_Click(object sender, EventArgs e)
        {
            porcentajeGeneral = 0;
            descuentoCliente = 0;
            txtDescuentoGeneral.Text = "% descuento";

            foreach (DataGridViewRow fila in DGVentas.Rows)
            {
                //var idProducto = Convert.ToInt32(fila.Cells["IDProducto"].Value);

                fila.Cells["Descuento"].Value = "0.00";
            }

            // Almacena los ID de los productos a los que se aplica descuento general
            productosDescuentoG.Clear();
            // Guarda los datos de los descuentos directos que se han aplicado
            descuentosDirectos.Clear();

            CantidadesFinalesVenta();
            txtBuscadorProducto.Focus();
            PorcentajeDescuento = "";
            AplicarCantidad = "";
            AplicarPorcentaje = "";
        }

        private void ProductoSeleccionado()
        {
            //Se obtiene el texto del item seleccionado del ListBox
            producto = listaProductos.Items[listaProductos.SelectedIndex].ToString();
            //Se obtiene el indice del array donde se encuentra el producto seleccionado
            //idProducto = Convert.ToInt32(datos.GetKey(Array.IndexOf(productos, producto)));
            idProducto = productosD.FirstOrDefault(x => x.Value == producto).Key;

            datosProducto = cn.BuscarProducto(idProducto, FormPrincipal.userID);

            //using (dtProdMessg = cn.CargarDatos(cs.ObtenerProductMessage(Convert.ToString(idProducto))))
            //{
            //    //Condicionar con compra minima de productos para mostrar el mensaje
            //    //var estadoMensaje = cn.CargarDatos(cs.EstadomensajeVentas(Productos.codProductoEditarVenta));
            //    //string status = estadoMensaje.Rows[0].ItemArray[0].ToString();

            //    if (dtProdMessg.Rows.Count > 0)
            //    {
            //        drProdMessg = dtProdMessg.Rows[0];

            //        var mensaje = drProdMessg["ProductOfMessage"].ToString().ToUpper();

            //        MessageBox.Show(mensaje, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}

            //borrar producto a buscar
            txtBuscadorProducto.Text = "";
            txtBuscadorProducto.Focus();
            ocultarResultados();

            if (!productosDescuentoG.ContainsKey(idProducto))
            {
                productosDescuentoG.Add(idProducto, aplicarDescuentoG);
            }

            //if (datosProducto[5].ToString().Equals("P"))
            //{
            //    AgregarProducto(datosProducto, Convert.ToDecimal(nudCantidadPS.Value));
            //}
            //else
            //{
            //    using (DataTable dtComboServicio = cn.CargarDatos(cs.cantidadDeComboServicio(idProducto)))
            //    {
            //        if (!dtComboServicio.Rows.Count.Equals(0))
            //        {
            //            foreach (DataRow drCombServ in dtComboServicio.Rows)
            //            {
            //                AgregarProducto(datosProducto, Convert.ToDecimal(drCombServ["Cantidad"]));
            //            }
            //        }
            //    }
            //}

            var dato = cn.CargarDatos($"SELECT FormatoDeVenta FROM productos WHERE ID = '{idProducto}' AND IDUSuario = '{FormPrincipal.userID}' AND Status = '1'");
            var estado = dato.Rows[0]["FormatoDeVenta"].ToString();

            if (estado == "1")
            {
                decimal result = Convert.ToDecimal(nudCantidadPS.Value);
                if (result.ToString().Contains('.'))
                {
                    MessageBox.Show("Este producto se vende solo por unidades enteras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            AgregarProducto(datosProducto, Convert.ToDecimal(nudCantidadPS.Value));

        }

        private void CalculoMayoreo()
        {
            float contadorMayoreo = 0;
            // Si la casilla de mayoreo de config esta activa
            if (mayoreoActivo)
            {
                // Si la cantidad minima es mayor a cero
                if (cantidadMayoreo > 0)
                {
                    foreach (DataGridViewRow fila in DGVentas.Rows)
                    {
                        var mayoreo = float.Parse(fila.Cells["PrecioMayoreo"].Value.ToString());
                        var cantidad = float.Parse(fila.Cells["Cantidad"].Value.ToString());

                        if (mayoreo > 0)
                        {
                            contadorMayoreo += cantidad;
                        }
                    }

                    if (contadorMayoreo >= cantidadMayoreo)
                    {
                        foreach (DataGridViewRow fila in DGVentas.Rows)
                        {
                            var precio = float.Parse(fila.Cells["PrecioMayoreo"].Value.ToString());

                            if (precio > 0)
                            {
                                var nombre = fila.Cells["Descripcion"].Value.ToString();
                                var cantidad = float.Parse(fila.Cells["Cantidad"].Value.ToString());
                                var importe = cantidad * precio;

                                if (nombre.Length > 3)
                                {
                                    var caracteres = nombre.Substring(0, 3);

                                    if (caracteres.Equals("***"))
                                    {
                                        nombre = nombre.Remove(0, 3);
                                        nombre = "***" + nombre;
                                    }
                                    else
                                    {
                                        nombre = "***" + nombre;
                                    }
                                }

                                fila.Cells["Descripcion"].Value = nombre;
                                fila.Cells["PrecioOriginal"].Value = precio;
                                fila.Cells["Precio"].Value = precio;
                                fila.Cells["Importe"].Value = importe;
                            }
                        }

                        lbMayoreo.Visible = true;
                    }
                    else
                    {
                        foreach (DataGridViewRow fila in DGVentas.Rows)
                        {
                            var precio = float.Parse(fila.Cells["PrecioAuxiliar"].Value.ToString());

                            if (precio > 0)
                            {
                                var nombre = fila.Cells["Descripcion"].Value.ToString();
                                var cantidad = float.Parse(fila.Cells["Cantidad"].Value.ToString());
                                var importe = cantidad * precio;

                                if (nombre.Length > 3)
                                {
                                    var caracteres = nombre.Substring(0, 3);

                                    if (caracteres.Equals("***"))
                                    {
                                        nombre = nombre.Remove(0, 3);

                                        fila.Cells["Descripcion"].Value = nombre;
                                    }
                                }

                                fila.Cells["PrecioOriginal"].Value = precio;
                                fila.Cells["Precio"].Value = precio;
                                fila.Cells["Importe"].Value = importe;
                            }
                        }

                        lbMayoreo.Visible = false;
                    }
                }
            }
            //txtBuscadorProducto.Focus();
        }



        private void Ventas_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }

        }

        private void txtDescuentoGeneral_Enter(object sender, EventArgs e)
        {
            if (!DGVentas.Rows.Count.Equals(0))
            {
                decimal Descuento = Convert.ToDecimal(cTotal.Text) + Convert.ToDecimal(cDescuento.Text);
                AplicarDecuentoGeneral aplicar = new AplicarDecuentoGeneral(Descuento.ToString());
                aplicar.ShowDialog();
                if (HizoUnaAccion.Equals(true))
                {
                    if (string.IsNullOrWhiteSpace(PorcentajeDescuento))
                    {
                        txtDescuentoGeneral.Clear();
                        txtBuscadorProducto.Focus();
                        return;
                    }
                    else
                    {
                        txtDescuentoGeneral.Text = PorcentajeDescuento;
                        if (txtDescuentoGeneral.Text.Equals("% descuento") || string.IsNullOrWhiteSpace(txtDescuentoGeneral.Text))
                        {
                            return;
                        }
                        descuentoDirectoPorAplicar = txtDescuentoGeneral.Text.Trim();


                        porcentajeGeneral = 0;
                        descuentoCliente = 0;
                        txtDescuentoGeneral.Text = "% descuento";

                        foreach (DataGridViewRow fila in DGVentas.Rows)
                        {
                            //var idProducto = Convert.ToInt32(fila.Cells["IDProducto"].Value);

                            fila.Cells["Descuento"].Value = "0.00";
                        }

                        // Almacena los ID de los productos a los que se aplica descuento general
                        productosDescuentoG.Clear();
                        // Guarda los datos de los descuentos directos que se han aplicado
                        descuentosDirectos.Clear();

                        CantidadesFinalesVenta();
                        txtBuscadorProducto.Focus();





                        txtDescuentoGeneral.Text = descuentoDirectoPorAplicar;
                        productosDescuentoG.Clear();
                        descuentosDirectos.Clear();
                        if (!txtDescuentoGeneral.Text.Equals("."))
                        {
                            if (opcion19 == 0)
                            {
                                Utilidades.MensajePermiso();
                                return;
                            }

                            DescuentoGeneral();
                        }
                    }
                    HizoUnaAccion = false;
                }
                txtBuscadorProducto.Focus();
                txtDescuentoGeneral.Text = "% descuento";
                txtBuscadorProducto.Focus();

            }
            else
            {
                MessageBox.Show("Agrega un Producto a la venta", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescuentoGeneral.Clear();
                txtBuscadorProducto.Focus();
            }


            //if (FormPrincipal.userNickName.Contains('@'))
            //{
            //    var datos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Ventas");
            //    if (datos[18].Equals(0))
            //    {
            //        Utilidades.MensajePermiso();
            //        txtDescuentoGeneral.Enabled = false;
            //        return;
            //    }
            //    else
            //    {
            //        txtDescuentoGeneral.Text = "";
            //        txtDescuentoGeneral.Select(0, 0);
            //    }
            //}
            //else
            //{
            //    txtDescuentoGeneral.Text = "";
            //    txtDescuentoGeneral.Select(0, 0);
            //}


        }

        private void checkCancelar_MouseClick(object sender, MouseEventArgs e)
        {
            if (checkCancelar.Checked)
            {
                lFolio.Visible = true;
                lFolio.Text = "";
                lFolio.Focus();
            }
            else
            {
                lFolio.Visible = false;
            }
        }

        private void lFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var numFolio = lFolio.Text;
                var obtenerIdFolio = mb.obtenerIdDelFolio(numFolio);

                var ultimaFechaCorte = mb.ObtenerFechaUltimoCorte();
                var fechaVenta = mb.ObtenerFechaVenta(obtenerIdFolio);

                DateTime validarFechaCorte = Convert.ToDateTime(ultimaFechaCorte);
                DateTime validarFechaVenta = Convert.ToDateTime(fechaVenta);

                if (validarFechaVenta > validarFechaCorte)
                {
                    CancelarVenta(numFolio);

                    ////Obtener el ID de la venta
                    //var obtenerIdVenta = cn.CargarDatos($"SELECT ID FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND Folio = '{numFolio}'");
                    //var idVentaObtenido = "";

                    //foreach (DataRow getId in obtenerIdVenta.Rows)
                    //{
                    //    idVentaObtenido = getId["ID"].ToString();
                    //}
                    //var idVenta = Convert.ToInt32(idVentaObtenido);

                    ////Caneclar la venta
                    //int resultado = cn.EjecutarConsulta(cs.ActualizarVenta(idVenta, 3, FormPrincipal.userID));

                    //var mensajeCancelarVenta = MessageBox.Show($"¿Desea cancelar la venta con el folio '{numFolio}'?", "Mensaje de Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    //if (mensajeCancelarVenta == DialogResult.Yes)
                    //{
                    //    // Regresar la cantidad de producto vendido al stock
                    //    if (resultado > 0)
                    //    {
                    //        var productos = cn.ObtenerProductosVenta(idVenta);

                    //        if (productos.Length > 0)
                    //        {
                    //            foreach (var producto in productos)
                    //            {
                    //                var info = producto.Split('|');
                    //                var idProducto = info[0];
                    //                var cantidad = Convert.ToInt32(info[2]);

                    //                cn.EjecutarConsulta($"UPDATE Productos SET Stock =  Stock + {cantidad} WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID}");
                    //            }
                    //        }

                    //        // Agregamos marca de agua al PDF del ticket de la venta cancelada
                    //        Utilidades.CrearMarcaDeAgua(idVenta, "CANCELADA");
                    //    }

                    //    mensajeCancelarVenta = MessageBox.Show("¿Desea devolver el dinero?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    //    if (mensajeCancelarVenta == DialogResult.Yes)
                    //    {
                    //        var formasPago = mb.ObtenerFormasPagoVenta(idVenta, FormPrincipal.userID);

                    //        // Operacion para que la devolucion del dinero afecte al apartado Caja
                    //        if (formasPago.Length > 0)
                    //        {
                    //            var total = formasPago.Sum().ToString();
                    //            var efectivo = formasPago[0].ToString();
                    //            var tarjeta = formasPago[1].ToString();
                    //            var vales = formasPago[2].ToString();
                    //            var cheque = formasPago[3].ToString();
                    //            var transferencia = formasPago[4].ToString();
                    //            var credito = formasPago[5].ToString();
                    //            var anticipo = "0";

                    //            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //            var concepto = $"DEVOLUCION DINERO VENTA CANCELADA ID {idVenta}";

                    //            string[] datos = new string[] {
                    //                        "retiro", total, "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(),
                    //                        efectivo, tarjeta, vales, cheque, transferencia, credito, anticipo
                    //                    };

                    //            cn.EjecutarConsulta(cs.OperacionCaja(datos));
                    //        }
                    //    }
                    //}
                    lFolio.Text = "";
                }
                else
                {
                    MessageBox.Show("No es posible cancelar ventas \nanteriores al cote de caja", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void Ventas_Shown(object sender, EventArgs e)
        {
            txtBuscadorProducto.Focus();
            txtBuscadorProducto.Text = string.Empty;
        }

        private void lbEliminarCliente_Click(object sender, EventArgs e)
        {
            descuentoCliente = 0;
            CantidadesFinalesVenta();
            lbDatosCliente.Text = string.Empty;
            lbDatosCliente.Visible = false;
            lbEliminarCliente.Visible = false;
            btnEliminarDescuentos.PerformClick();
            idCliente = "";
            ClienteConDescuento = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            btnCancelarVenta.PerformClick();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            btnEliminarDescuentos.PerformClick();
        }

        private void botonRedondo1_Click(object sender, EventArgs e)
        {
            if (opcion12 == 0)
            {


                Utilidades.MensajePermiso();
                return;
            }

            var FechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var datos = new string[] { FormPrincipal.userID.ToString(), "0", "0", "0", "0", "0", "0", "0", "0", "0", "N/A", "1", FechaOperacion, "Apertura de Caja", FormPrincipal.id_empleado.ToString(), "0" };
            cn.EjecutarConsulta(cs.GuardarAperturaDeCaja(datos));

            DialogResult respuesta = MessageBox.Show("Desea imprimir Ticket de Apertura de Caja", "Aviso del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta.Equals(DialogResult.Yes))
            {
                imprimirUltimoTicket();
            }

            //if (Utilidades.AdobeReaderInstalado())
            //{
            //    var FechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //    var datos = new string[] { FormPrincipal.userID.ToString(), "0", "0", "0", "0", "0", "0", "0", "0", "0", "N/A", "1", FechaOperacion, "Apertura de Caja", FormPrincipal.id_empleado.ToString(), "0" };
            //    cn.EjecutarConsulta(cs.GuardarAperturaDeCaja(datos));

            //    GenerarTicketCaja();
            //}
            //else
            //{
            //    Utilidades.MensajeAdobeReader();
            //}
            txtBuscadorProducto.Focus();
        }

        private void botonRedondo2_Click(object sender, EventArgs e)
        {
            if (opcion11 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<ListadoAnticipos>().Count() == 1)
            {
                Application.OpenForms.OfType<ListadoAnticipos>().First().BringToFront();
            }
            else
            {
                ListadoAnticipos anticipo = new ListadoAnticipos(DGVentas.Rows.Count);

                anticipo.FormClosed += delegate
                {
                    if (importeAnticipo > 0)
                    {
                        CantidadesFinalesVenta();
                        importeAnticipo = 0f;
                        btnEliminarAnticipos.Visible = true;
                    }
                };

                anticipo.ShowDialog();
            }
        }

        private void botonRedondo3_Click(object sender, EventArgs e)
        {

            using (DataTable dt = cn.CargarDatos($"SELECT opcion16 FROM empleadospermisos WHERE Seccion='ventas' AND idUsuario = {FormPrincipal.userID} AND IDEmpleado = {FormPrincipal.id_empleado}"))
            {
                if (!dt.Rows.Count.Equals(0))
                {
                    if (dt.Rows[0][0].ToString().Equals("0") && !FormPrincipal.id_empleado.Equals(0))
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }
                }
            }

            EsGuardarVenta = false;
            //if (opcion16 == 0)
            //{
            //    Utilidades.MensajePermiso();
            //    return;
            //}

            if (DGVentas.RowCount == 0)
            {
                MessageBox.Show("No hay productos agregados a la lista\npara aplicar el descuento", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var clientes = new ListaClientes(tipo: 1))
            {
                var respuesta = clientes.ShowDialog();

                if (respuesta == DialogResult.OK)
                {
                    lbEliminarCliente_Click(lbEliminarCliente, e);
                    var datos = clientes.datosCliente;
                    string cliente = string.Empty;

                    idCliente = datos[18];

                    var auxPrimero = string.IsNullOrWhiteSpace(datos[0]);
                    var auxSegundo = string.IsNullOrWhiteSpace(datos[1]);
                    var auxTercero = string.IsNullOrWhiteSpace(datos[17]);

                    if (!auxPrimero) { cliente += $"Cliente: {datos[0]}"; }
                    if (!auxSegundo) { cliente += $" --- RFC: {datos[1]}"; }
                    if (!auxTercero) { cliente += $" --- No. {datos[17]}"; }

                    NombreCliente = datos[0];
                    var idTipoCliente = Convert.ToInt32(datos[16]);

                    idClienteDescuento = Convert.ToInt32(datos[18]);
                    ClienteConDescuentoNombre = datos[0].ToString();
                    IDClienteConDescuento = idCliente;
                    if (idTipoCliente > 0)
                    {
                        var datosDescuento = mb.ObtenerTipoCliente(idTipoCliente);

                        if (datosDescuento.Length > 0)
                        {
                            descuentoCliente = float.Parse(datosDescuento[1]) / 100;
                            // Se reinicia a los valores por defecto el descuento general
                            porcentajeGeneral = 0;
                            txtDescuentoGeneral.Text = "% descuento";

                            CantidadesFinalesVenta();
                        }
                    }

                    lbDatosCliente.Text = cliente;
                    lbDatosCliente.Visible = true;
                    lbEliminarCliente.Visible = true;
                    ClienteConDescuento = true;
                }
            }
        }

        private void botonRedondo4_Click(object sender, EventArgs e)
        {
            EsGuardarVenta = true;
            ventaGuardadappt = 1;
            if (opcion10 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (DGVentas.RowCount > 0)
            {
                if (!string.IsNullOrWhiteSpace(listaAnticipos))
                {
                    MessageBox.Show("No se puede guardar esta venta ya que\ntiene un anticipo aplicado", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ListaClientes cliente = new ListaClientes();

                cliente.FormClosed += delegate
                {
                    if (ventaGuardada)
                    {
                        DatosVenta();
                        liststock2.Clear();
                        idCliente = string.Empty;
                        statusVenta = string.Empty;
                        ventaGuardada = false;
                        DetalleVenta.idCliente = 0;
                        DetalleVenta.cliente = string.Empty;
                        DetalleVenta.nameClienteNameVenta = string.Empty;

                    }
                };

                cliente.ShowDialog();
            }
            else
            {
                MessageBox.Show("No hay productos agregados a la lista", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void botonRedondo5_Click(object sender, EventArgs e)
        {
            if (opcion13 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<ListadoVentasGuardadas>().Count() == 1)
            {
                ListadoVentasGuardadas ventasFusion = new ListadoVentasGuardadas();
                if (!DGVentas.Rows.Count.Equals(0))
                {
                    ventasFusion.mensajeFusionarProductos = 1;
                }
                else
                {
                    ventasFusion.mensajeFusionarProductos = 0;
                }
                Application.OpenForms.OfType<ListadoVentasGuardadas>().First().BringToFront();
            }
            else
            {
                ListadoVentasGuardadas venta = new ListadoVentasGuardadas();

                venta.FormClosed += delegate
                {
                    if (mostrarVenta > 0)
                    {
                        desdeVentaGuardada = 1;

                        // Verifica si los productos guardados tienen descuento
                        var datos = mb.ProductosGuardados(mostrarVenta);

                        if (datos.Count > 0)
                        {
                            if (!ComprobarDescuento(datos))
                            {
                                mostrarVenta = 0;
                                return;
                            }
                        }

                        mostrarMensajeProducto = false;

                        CargarVentaGuardada();

                        ventasGuardadas.Add(mostrarVenta);

                        desdeVentaGuardada = 0;
                        mostrarVenta = 0;
                        txtDescuentoGeneral.Text = "% descuento";
                    }
                };

                if (!DGVentas.Rows.Count.Equals(0))
                {
                    venta.mensajeFusionarProductos = 1;
                }
                else
                {
                    venta.mensajeFusionarProductos = 0;
                }
                venta.Show();
            }
        }

        private void botonRedondo6_Click(object sender, EventArgs e)
        {
            Ventana_cancelar_venta vnt_cancelar = new Ventana_cancelar_venta();

            vnt_cancelar.FormClosed += delegate
            {
                if (mostrarVenta > 0)
                {
                    DialogResult respuesta = MessageBox.Show("Venta cancelada exitosamente\n\n¿desea recargar artículos de la venta?", "Aviso del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (respuesta.Equals(DialogResult.Yes))
                    {
                        //Cargar la venta cancelada 
                        CargarVentaGuardada();
                    }

                    mostrarVenta = 0;
                    txtBuscadorProducto.Text = string.Empty;
                }
            };

            vnt_cancelar.ShowDialog();
        }

        public void botonRedondo7_Click(object sender, EventArgs e)
        {
            if (opcion14 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            imprimirUltimoTicket();

            //try
            //{
            //    var idVenta = cn.EjecutarSelect($"SELECT * FROM Ventas WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 ORDER BY ID DESC LIMIT 1", 1).ToString();

            //    var Folio = string.Empty;
            //    var Serie = string.Empty;

            //    using (DataTable dtDatosVentas = cn.CargarDatos(cs.DatosVentaParaLaNota(Convert.ToInt32(idVenta))))
            //    {
            //        if (!dtDatosVentas.Rows.Count.Equals(0))
            //        {
            //            foreach (DataRow item in dtDatosVentas.Rows)
            //            {
            //                Folio = item["Folio"].ToString();
            //                Serie = item["Serie"].ToString();

            //                if (Folio.Equals("0"))
            //                {
            //                    MessageBox.Show($"En esta operación se realizo la apertura de la Caja\nRealizada por el Usuario: {item["Usuario"].ToString()}", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    return;
            //                }
            //            }
            //        }
            //    }

            //    if (Utilidades.AdobeReaderInstalado())
            //    {
            //        ImprimirTicket(idVenta);
            //    }
            //    else
            //    {
            //        Utilidades.MensajeAdobeReader();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Aun no se han creado Tickets", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
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

        private void txtDescuentoGeneral_KeyDown(object sender, KeyEventArgs e)
        {
            var cantidadDescuento = txtDescuentoGeneral.Text;
            if (e.KeyCode == Keys.Enter)
            {
                btnAplicarDescuento.PerformClick();
                txtBuscadorProducto.Text = string.Empty;
                txtBuscadorProducto.Focus();
                txtDescuentoGeneral.Text = "% descuento";
                btnAplicarDescuento.Enabled = false;
            }
        }

        private void DGVentas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (contadorChangeValue.Equals(0))
            {
                if (!DGVentas.Rows.Count.Equals(0))
                {
                    var celda = e.RowIndex;

                    decimal cantidad = 0;

                    bool isDecimal = Decimal.TryParse(DGVentas.Rows[celda].Cells[5].FormattedValue.ToString(), out cantidad);

                    var idproductoCantidad = DGVentas.Rows[celda].Cells[0].Value;

                    var MinimaCompra = cn.CargarDatos(cs.cantidadCompraMinima(Convert.ToInt32(idproductoCantidad)));

                    if (!MinimaCompra.Rows.Count.Equals(0) && !string.IsNullOrWhiteSpace(MinimaCompra.ToString()))
                    {
                        var cantidadMinima = Convert.ToInt32(MinimaCompra.Rows[0].ItemArray[0]);

                        if (cantidad >= cantidadMinima)
                        {
                            using (dtProdMessg = cn.CargarDatos(cs.ObtenerProductMessage(Convert.ToString(idproductoCantidad))))
                            {
                                if (dtProdMessg.Rows.Count > 0)
                                {
                                    drProdMessg = dtProdMessg.Rows[0];

                                    if (!listaMensajesEnviados.ContainsKey(Convert.ToInt32(idproductoCantidad)))
                                    {
                                        listaMensajesEnviados.Add(Convert.ToInt32(idproductoCantidad), "Mensaje");
                                        var mensaje = drProdMessg["ProductOfMessage"].ToString().ToUpper();
                                        MessageBox.Show(mensaje, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                        else if (cantidad < cantidadMinima && listaMensajesEnviados.ContainsKey(Convert.ToInt32(idproductoCantidad)))
                        {
                            listaMensajesEnviados.Remove(Convert.ToInt32(idproductoCantidad));
                        }
                    }
                }
                contadorChangeValue++;
            }

            //txtBuscadorProducto.Focus();
        }

        private void DGVentas_Enter(object sender, EventArgs e)
        {

            //var celda = DGVentas.Rows[0].Cells["Descuento"].Value.ToString();
            //if (!celda.Equals("0.00"))
            //{
            //    txtBuscadorProducto.Focus();
            //}
        }

        private void DGVentas_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            //Aqui reproducir sonido de producto al carrito
            ReproducirSonido();
            //txtBuscadorProducto.Focus();
            txtBuscadorProducto.Select();
        }

        private void nudCantidadPS_Click(object sender, EventArgs e)
        {
            var canitdad = Convert.ToDecimal(nudCantidadPS.Value);
            nudCantidadPS.Select(0, canitdad.ToString().Length + 3);
        }

        private void btnConsultar_Enter(object sender, EventArgs e)
        {
            txtBuscadorProducto.Focus();
        }

        private void DGVentas_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            contadorMensaje = 0;

            if (sonido == true && contador == 0)
            {
                reproducirProductoAgregado();
                contador++;
            }
        }

        private void Ventas_Click(object sender, EventArgs e)
        {
            limpiarImagenDelProducto();
        }

        private void limpiarImagenDelProducto()
        {
            PBImagen.Image = null;
            PBImagen.Refresh();
            DGVentas.ClearSelection();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            limpiarImagenDelProducto();
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            limpiarImagenDelProducto();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            limpiarImagenDelProducto();
        }
        private void btnCSV_Click(object sender, EventArgs e)
        {
            ImportarExportarCSV frmCsv = new ImportarExportarCSV();
            frmCsv.FormClosed += delegate
            {
                if (0 < frmCsv.dtVentas.Rows.Count)
                {
                    for (int i = 0; i < frmCsv.dtVentas.Rows.Count; i++)
                    {
                        var stringArr = frmCsv.dtVentas.Rows[i].ItemArray.Select(x => x.ToString()).ToArray();
                        decimal d = decimal.Parse(stringArr[6]);
                        AgregarProductoLista(stringArr, d);

                    }
                    btnCSV.Enabled = false;
                }

            };
            frmCsv.ShowDialog();
        }
        private void CBTipo_TextChanged(object sender, EventArgs e)
        {
            if (CBTipo.SelectedItem.Equals("Todos"))
            {
                filtro = "Todos";
            }
            if (CBTipo.SelectedItem.Equals("Servicios"))
            {
                filtro = "S";
            }
            if (CBTipo.SelectedItem.Equals("Productos"))
            {
                filtro = "P";
            }
            if (CBTipo.SelectedItem.Equals("Combos"))
            {
                filtro = "PQ";
            }
            if (!string.IsNullOrWhiteSpace(txtBuscadorProducto.Text) || !txtBuscadorProducto.Text.Equals("BUSCAR PRODUCTO O SERVICIO..."))
            {
                string texto = txtBuscadorProducto.Text;
                var tamaño = texto.Length;
                var ultimaLetra = texto[tamaño - 1].ToString();

                //Borrar la ultima letra
                if (txtBuscadorProducto.Text.Length > tamaño - 1)
                {
                    txtBuscadorProducto.Text = txtBuscadorProducto.Text.Substring(0, tamaño - 1);
                    txtBuscadorProducto.Select(txtBuscadorProducto.Text.Length, 0);
                    var sinLetra = txtBuscadorProducto.Text;
                    txtBuscadorProducto.Text = $"{sinLetra}{ultimaLetra}";
                }

            }


        }

        private void txtDescuentoGeneral_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // solo 1 punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }




        private void txtBuscadorProducto_Leave(object sender, EventArgs e)
        {
            tieneElCursorElTxtBuscadorProducto = false;
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void txtDescuentoGeneral_TextChanged(object sender, EventArgs e)
        {
            if (txtDescuentoGeneral.Text.Equals("% descuento") || txtDescuentoGeneral.Text.Contains("%"))
            {
                btnAplicarDescuento.Enabled = false;
            }
            else
            {
                btnAplicarDescuento.Enabled = true;
            }
        }

        private void txtBuscadorProducto_Click(object sender, EventArgs e)
        {
            listaProductos.Visible = false;
        }

        private void btnGanancia_Click(object sender, EventArgs e)
        {
            if (!DGVentas.Rows.Count.Equals(0))
            {
                Ganancia cantidadGanancia = new Ganancia();
                cantidadGanancia.totalVenta = Convert.ToDecimal(cTotal.Text);
                cantidadGanancia.lugarGanancia = 2;

                foreach (DataGridViewRow item in DGVentas.Rows)
                {
                    var id = item.Cells["IDProducto"].Value.ToString() + "|" + item.Cells["Cantidad"].Value.ToString();
                    cantidadGanancia.idsprods.Add(id);
                }

                cantidadGanancia.ShowDialog();
            }

        }

        private void nudCantidadPS_KeyPress(object sender, KeyPressEventArgs e)
        {
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
                            nudCantidadPS.Value = Convert.ToDecimal(calculadora.lCalculadora.Text);
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

                    //if ()
                    //{
                    //    txtStockMaximo.Text = calculadora.lCalculadora.Text;
                    //}
                }
            }
        }

        private void nudCantidadPS_ValueChanged(object sender, EventArgs e)
        {
            if (nudCantidadPS.Value <= 0)
            {
                nudCantidadPS.Value = 1;
            }
        }

        private void txtBuscadorProducto_Enter(object sender, EventArgs e)
        {
            tieneElCursorElTxtBuscadorProducto = true;
        }

        private void botonRedondo1_Click_1(object sender, EventArgs e)
        {
            VentaRapida venta = new VentaRapida();
            venta.ShowDialog();
            if (!string.IsNullOrWhiteSpace(codBarProdVentaRapida))
            {
                txtBuscadorProducto.Text = codBarProdVentaRapida;
                txtBuscadorProducto.Focus();
                SendKeys.Send("{ENTER}");
            }
        }

        private void DGVentas_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtBuscadorProducto_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscadorProducto.Text.Contains("\'"))
            {
                string producto = txtBuscadorProducto.Text.Replace("\'", ""); ;
                txtBuscadorProducto.Text = producto;
                txtBuscadorProducto.Select(txtBuscadorProducto.Text.Length, 0);
            }


            if (string.IsNullOrWhiteSpace(txtBuscadorProducto.Text.Trim()))
            {
                listaProductos.Visible = false;
            }
        }

        private void DGVentas_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var celda = e.RowIndex;

            decimal cantidad = 0;

            bool isDecimal = Decimal.TryParse(DGVentas.Rows[celda].Cells[5].FormattedValue.ToString(), out cantidad);//Se obtiene y guarda la cantidad en cantidad"
            var idproductoCantidad = DGVentas.Rows[celda].Cells[0].Value;

            var MinimaCompra = cn.CargarDatos(cs.cantidadCompraMinima(Convert.ToInt32(idproductoCantidad)));

            if (!MinimaCompra.Rows.Count.Equals(0))
            {
                foreach (DataRow row in MinimaCompra.Rows)
                {
                    if (!row.IsNull("CantidadMinimaDeCompra"))
                    {
                        var cantidadMinima = Convert.ToInt32(MinimaCompra.Rows[0].ItemArray[0]);

                        if (cantidad >= cantidadMinima)
                        {
                            if (mostrarMensajeProducto)
                            {
                                using (DataTable dtMensajesVentas = cn.CargarDatos(cs.verificarMensajesProductosVentas(Convert.ToInt32(idproductoCantidad))))
                                {
                                    var estado = dtMensajesVentas.Rows[0];

                                    if (!dtMensajesVentas.Rows.Count.Equals(0))
                                    {
                                        if (estado["ProductMessageActivated"].Equals(true))
                                        {
                                            if (!listaMensajesEnviados.ContainsKey(Convert.ToInt32(idproductoCantidad)))
                                            {
                                                listaMensajesEnviados.Add(Convert.ToInt32(idproductoCantidad), "Mensaje");
                                                foreach (DataRow item in dtMensajesVentas.Rows)
                                                {
                                                    MessageBox.Show(item["ProductOfMessage"].ToString(), "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (cantidad < cantidadMinima && listaMensajesEnviados.ContainsKey(Convert.ToInt32(idproductoCantidad)))
                        {
                            listaMensajesEnviados.Remove(Convert.ToInt32(idproductoCantidad));
                        }
                    }
                }
            }
            txtBuscadorProducto.Clear();
            txtBuscadorProducto.Focus();
        }

        private void btnCancelarVenta_Enter(object sender, EventArgs e)
        {
            txtBuscadorProducto.Focus();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            btnAplicarDescuento.PerformClick();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            btnTerminarVenta.PerformClick();
        }

        private void btnEliminarAnticipos_Click(object sender, EventArgs e)
        {
            listaAnticipos = string.Empty;
            importeAnticipo = 0f;
            cAnticipo.Text = "0.00";
            cAnticipoUtilizado.Text = "0.00";
            btnEliminarAnticipos.Visible = false;
            CantidadesFinalesVenta();
        }

        private void CuerpoEmails()
        {
            var correo = FormPrincipal.datosUsuario[9];
            var asunto = string.Empty;
            var asuntoAdicional = string.Empty;
            var html = string.Empty;
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var correoMultiple = 0;

            if (!string.IsNullOrWhiteSpace(correo))
            {
                // Comprobar stock minimo
                if (enviarStockMinimo.Count > 0)
                {
                    correoMultiple++;
                    asunto = "¡AVISO! STOCK MINIMO ALCANZADO POR VENTAS.";

                    html = @"
                    <div style='margin-bottom: 50px;'>
                        <h3 style='text-align: center;'>PRODUCTOS CON STOCK MINIMO</h3><hr>
                        <ul style='color: black; font-size: 0.9em;'>";

                    foreach (var producto in enviarStockMinimo)
                    {
                        html += $"<li>{producto.Value}</li>";
                    }

                    var footerCorreo = string.Empty;


                    if (FormPrincipal.id_empleado > 0)
                    {
                        var datosEmpleado = mb.obtener_permisos_empleado(FormPrincipal.id_empleado, FormPrincipal.userID);

                        string nombreEmpleado = datosEmpleado[15];
                        string usuarioEmpleado = datosEmpleado[16];

                        var infoEmpleado = usuarioEmpleado.Split('@');

                        footerCorreo = $"<p style='font-size: 14px;'>En la venta con folio <span style='color: black;'>{FolioVentaCorreo}</span> realizada por el empleado <b>{nombreEmpleado} ({infoEmpleado[1]})</b> del usuario <b>{infoEmpleado[0]}</b>, los siguientes productos llegaron al stock mínimo con <span style='color: black;'>fecha de {fechaOperacion}</span></p>";
                    }
                    else
                    {
                        footerCorreo = $"<p style='font-size: 14px;'>En la venta con folio <span style='color: black;'>{FolioVentaCorreo}</span> realizada por el <b>ADMIN</b> del usuario <b>{FormPrincipal.userNickName}</b>, los siguientes productos llegaron al stock mínimo con <span style='color: black;'>fecha de {fechaOperacion}</span></p>";
                    }

                    html += $@"
                        </ul><hr>
                        {footerCorreo}
                    </div>";
                }

                // Comprobar venta producto
                if (enviarVentaProducto.Count > 0)
                {
                    correoMultiple++;
                    asunto = "INFORMACION DE ESTADO DE PRODUCTOS";

                    html += @"
                    <div>
                        <h3 style='text-align: center;'>VENTA DE PRODUCTOS SELECCIONADOS</h3>
                        <hr>
                        <ul style='color: black; font-size: 0.9em;'>";

                    foreach (var producto in enviarVentaProducto)
                    {
                        html += $"<li>{producto.Value}</li>";
                    }

                    html += @"
                        </ul>
                        <p style='font-size: 0.9em; text-align: center;'><b>NOTA:</b> Es posible que se hayan vendido otros artículos junto a estos productos en la misma venta,
                        los productos aquí listados son sólo los que el usuario configuró para recibir notificaciones a través de correo electrónico.</p>
                    </div>";
                    html += "<hr>";
                    var usuarioEmpleado = FormPrincipal.userNickName.ToString();
                    if (usuarioEmpleado.Contains("@"))
                    {
                        html += $"La venta fue realizada por el <b>EMPLEADO</b> del usuario <b>{FormPrincipal.userNickName}</b> <span style='color: black;'> Fecha de Modificación: {DateTime.Now}</span></span></p></div>";
                    }
                    else
                    {
                        html += $"La venta fue realizada por el <b>ADMIN</b> del usuario <b>{FormPrincipal.userNickName}</b> <span style='color: black;'> Fecha de Modificación: {DateTime.Now}</span></span></p></div>";
                    }
                }

                if (enviarVenta.Count > 0)
                {
                    html += @"
                    <div>
                        <h4 style='text-align: center;'>LISTADO DE PRODUCTOS VENDIDOS</h4><hr>
 
                        <table style= 'width:100%'>
                            <tr>
                                <th style='text-align: left;'>Cantidad</th>
                                <th style='text-align: left;'>Precio</th>
                                <th>Descripcion</th>
                                <th style='text-align: left;'>Descuento</th>
                                <th style='text-align: right;'>Importe</th>
                            </tr>";

                    float totalVenta = 0;

                    string folio = string.Empty;
                    string cliente = string.Empty;
                    string formaDePago = string.Empty;
                    string anticipoRecibido = string.Empty;
                    string anticipoUtilizado = string.Empty;
                    string descuentoVenta = string.Empty;

                    foreach (var producto in enviarVenta)
                    {
                        string[] articulos = producto.Split('|');

                        html += $@"<tr>
                                    <td style = 'text-align: left;'>
                                        <span style='color: blue;'>{articulos[0]}</span>
                                    </td>
                                    <td style = 'text-align: left;'>
                                        <span style='color: blue;'>{articulos[1]}</span>
                                    </td>
                                    <td style = 'text-align: center;'>
                                        <span style='color: black;'><b>{articulos[2]}</b></span>
                                    </td>
                                    <td style = 'text-align: left;'>
                                        <span style='color: blue;'>{articulos[3]}</span>
                                    </td>
                                    <td style = 'text-align: right;'>
                                        <span style='color: blue;'><b>{articulos[4]}</b></span>
                                    </td>
                                </tr>";

                        totalVenta += float.Parse(articulos[4]);

                        formaDePago = articulos[5];
                        cliente = articulos[6];
                        folio = articulos[7];
                        anticipoRecibido = articulos[8];
                        anticipoUtilizado = articulos[9];
                        descuentoVenta = articulos[10];
                    }

                    string cadenaDatos = string.Empty;

                    if (!string.IsNullOrWhiteSpace(formaDePago))
                    {
                        cadenaDatos += "<br><br>";
                        cadenaDatos += "<span style='font-size: 12px;'>Forma de Pago: <span style='color: black;'>" + formaDePago + "</span></span><br>";
                    }

                    if (!string.IsNullOrWhiteSpace(cliente))
                    {
                        cadenaDatos += "<span style='font-size: 12px;'>Cliente: <span style='color: black;'>" + cliente + "</span></span><br>";
                    }
                    else
                    {
                        cadenaDatos += "<span style='font-size: 12px;'>Cliente: <span style='color: black;'>Público General</span></span>";
                    }

                    if (!string.IsNullOrWhiteSpace(folio))
                    {
                        cadenaDatos += "<span style='font-size: 12px;'>Folio: <span style='color: black;'>" + folio + "</span></span>";
                    }

                    if (FormPrincipal.id_empleado > 0)
                    {
                        correoMultiple++;
                        var datosEmpleado = mb.obtener_permisos_empleado(FormPrincipal.id_empleado, FormPrincipal.userID);

                        string nombreEmpleado = datosEmpleado[15];
                        string usuarioEmpleado = datosEmpleado[16];

                        var infoEmpleado = usuarioEmpleado.Split('@');

                        if (!string.IsNullOrWhiteSpace(descuentoVenta) && !descuentoVenta.Equals("0.00"))
                        {
                            html += $@"
                                    <tr>
                                        <td colspan='4' style='text-align: right;'>
                                            Descuento
                                        </td>
                                        <td style='text-align: right;'>
                                            <span style='color: black'><b>{float.Parse(descuentoVenta).ToString("0.00")}</b></span>
                                        </td>
                                    </tr>";
                        }

                        if (!string.IsNullOrWhiteSpace(anticipoRecibido) && !string.IsNullOrWhiteSpace(anticipoUtilizado))
                        {
                            if (!anticipoRecibido.Equals("0.00") && !anticipoUtilizado.Equals("0.00"))
                            {
                                html += $@"
                                    <tr>
                                        <td colspan='4' style='text-align: right;'>
                                            Anticipo recibido
                                        </td>
                                        <td style='text-align: right;'>
                                            <span style='color: black'><b>{float.Parse(anticipoRecibido).ToString("0.00")}</b></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='4' style='text-align: right;'>
                                            Anticipo utilizado
                                        </td>
                                        <td style='text-align: right;'>
                                            <span style='color: black'><b>{float.Parse(anticipoUtilizado).ToString("0.00")}</b></span>
                                        </td>
                                    </tr>";

                                totalVenta -= float.Parse(anticipoUtilizado);
                            }
                        }

                        html += $@"
                                    <tr>
                                        <td colspan='4' style='text-align: right;'>
                                            Total
                                        </td>
                                        <td style='text-align: right;'>
                                            <span style='color: black'><b>{totalVenta.ToString("0.00")}</b></span>
                                        </td>
                                    </tr>";

                        html += " </table>";
                        html += "<hr>";
                        html += cadenaDatos;
                        html += $"<p style='font-size: 14px;'>La venta fue realizada por el empleado <b>{nombreEmpleado} ({infoEmpleado[1]})</b> del usuario <b>{infoEmpleado[0]}</b> con <span style='color: black;'>fecha de {fechaOperacion}</span></p>";

                        asunto = $"VENTA REALIZADA - {infoEmpleado[0]}@{infoEmpleado[1]}";
                        asuntoAdicional = $"Venta realizada con descuento - {infoEmpleado[0]}@{infoEmpleado[1]}";
                    }
                    else
                    {
                        correoMultiple++;
                        if (!string.IsNullOrWhiteSpace(descuentoVenta) && !descuentoVenta.Equals("0.00"))
                        {
                            html += $@"
                                    <tr>
                                        <td colspan='4' style='text-align: right;'>
                                            Descuento
                                        </td>
                                        <td style='text-align: right;'>
                                            <span style='color: black'><b>{float.Parse(descuentoVenta).ToString("0.00")}</b></span>
                                        </td>
                                    </tr>";
                        }

                        if (!string.IsNullOrWhiteSpace(anticipoRecibido) && !string.IsNullOrWhiteSpace(anticipoUtilizado))
                        {
                            if (!anticipoRecibido.Equals("0.00") && !anticipoUtilizado.Equals("0.00"))
                            {
                                html += $@"
                                    <tr>
                                        <td colspan='4' style='text-align: right;'>
                                            Anticipo recibido
                                        </td>
                                        <td style='text-align: right;'>
                                            <span style='color: black'><b>{float.Parse(anticipoRecibido).ToString("0.00")}</b></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='4' style='text-align: right;'>
                                            Anticipo utilizado
                                        </td>
                                        <td style='text-align: right;'>
                                            <span style='color: black'><b>{float.Parse(anticipoUtilizado).ToString("0.00")}</b></span>
                                        </td>
                                    </tr>";

                                totalVenta -= float.Parse(anticipoUtilizado);
                            }
                        }

                        html += $@"
                                    <tr>
                                        <td colspan='4' style='text-align: right;'>
                                            Total
                                        </td>
                                        <td style='text-align: right;'>
                                            <span style='color: black'><b>{totalVenta.ToString("0.00")}</b></span>
                                        </td>
                                    </tr>";

                        html += "</table>";
                        html += "<hr>";
                        html += cadenaDatos;
                        html += $"<p style='font-size: 14px;'>La venta fue realizada por el <b>ADMIN</b> del usuario <b>{FormPrincipal.userNickName}</b> con <span style='color: black;'>fecha de {fechaOperacion}</span></p>";

                        asunto = $"VENTA REALIZADA - {FormPrincipal.userNickName}";
                        asuntoAdicional = $"SE REALIZO VENTA CON DESCUENTO - {FormPrincipal.userNickName}";
                    }

                    if (correoVenta == 1 && correoDescuento == 1)
                    {
                        if (correoMultiple >= 2)
                        {
                            asunto = $"MODIFICACION REALIZADA - {FormPrincipal.userNickName}";
                        }

                        if (!string.IsNullOrWhiteSpace(html))
                        {
                            Utilidades.EnviarEmail(html, asunto, correo);

                            if (!string.IsNullOrWhiteSpace(descuentoVenta) && !descuentoVenta.Equals("0.00"))
                            {
                                Utilidades.EnviarEmail(html, asuntoAdicional, correo);
                            }
                        }
                    }

                    if (correoVenta == 1 && correoDescuento == 0)
                    {
                        if (correoMultiple >= 2)
                        {
                            asunto = $"MODIFICACION REALIZADA - {FormPrincipal.userNickName}";
                        }

                        if (!string.IsNullOrWhiteSpace(html))
                        {
                            Utilidades.EnviarEmail(html, asunto, correo);
                        }
                    }

                    if (correoVenta == 0 && correoDescuento == 1)
                    {
                        if (!string.IsNullOrWhiteSpace(html))
                        {
                            if (correoMultiple >= 2)
                            {
                                asunto = $"MODIFICACION REALIZADA - {FormPrincipal.userNickName}";
                            }

                            if (!string.IsNullOrWhiteSpace(descuentoVenta) && !descuentoVenta.Equals("0.00"))
                            {
                                Utilidades.EnviarEmail(html, asuntoAdicional, correo);
                            }
                        }
                    }

                    return;
                }

                if (!string.IsNullOrWhiteSpace(html))
                {
                    if (correoMultiple >= 2)
                    {
                        asunto = $"MODIFICACION REALIZADA - {FormPrincipal.userNickName}";
                    }
                    Utilidades.EnviarEmail(html, asunto, correo);
                }
            }
        }

        private void txtDescuentoGeneral_KeyUp(object sender, KeyEventArgs e)
        {


            if (txtDescuentoGeneral.Text == ".")
            {
                txtDescuentoGeneral.Text = "0.";
                txtDescuentoGeneral.Select(txtDescuentoGeneral.Text.Length, 0);
            }

            var cantidadDescuento = txtDescuentoGeneral.Text;
            if (cantidadDescuento.Length > 0)
            {
                if (Convert.ToDouble(cantidadDescuento) >= 99.999999999999999)
                {
                    MessageBox.Show("Favor de ingresar un porcentaje menor al 100%");
                    txtDescuentoGeneral.Text = "";
                    return;
                }
            }
        }

        private void DGVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var celda = DGVentas.CurrentCell.RowIndex;
            var columna = DGVentas.CurrentCell.ColumnIndex;

            // Cantidad
            //if (columna.Equals(5))
            //{
            //    DGVentas.Rows[celda].Cells["Cantidad"].ReadOnly = false;
            //    cantidadAnterior = Convert.ToDecimal(DGVentas.Rows[celda].Cells["Cantidad"].Value.ToString());
            //}

            // Descuento
            //if (e.ColumnIndex == 8)
            //{
            //    DGVentas.Rows[celda].Cells["Descuento"].ReadOnly = false;
            //}
        }

        private void DGVentas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //COMENTADO TEMPORALMENTE PARA ENCONTRAR SOLUCION --GOOFY
            var celda = e.RowIndex;

            if (e.ColumnIndex == 5)
            {
                decimal cantidad = 0;

                if (String.IsNullOrWhiteSpace(DGVentas.Rows[celda].Cells[5].Value as String) || DGVentas.Rows[celda].Cells[5].Value.Equals("0"))
                {
                    DGVentas.Rows[celda].Cells[5].Value = "1";
                }

                bool isDecimal = Decimal.TryParse(DGVentas.Rows[celda].Cells[5].Value.ToString(), out cantidad);//Se obtiene y guarda la cantidad en cantidad"

                if (cantidadComprada.nuevaCantidadCambio == 1)
                {
                    cantidad = cantidadComprada.nuevaCantidad;
                }

                var idproductoCantidad = DGVentas.Rows[celda].Cells[0].Value;

                var MinimaCompra = cn.CargarDatos(cs.cantidadCompraMinima(Convert.ToInt32(idproductoCantidad)));

                if (!MinimaCompra.Rows.Count.Equals(0))
                {
                    var cantidadMinima = Convert.ToInt32(MinimaCompra.Rows[0].ItemArray[0]);

                    if (cantidad >= cantidadMinima)
                    {
                        using (dtProdMessg = cn.CargarDatos(cs.ObtenerProductMessage(Convert.ToString(idproductoCantidad))))
                        {
                            if (dtProdMessg.Rows.Count > 0)
                            {
                                drProdMessg = dtProdMessg.Rows[0];

                                var mensaje = drProdMessg["ProductOfMessage"].ToString().ToUpper();
                            }
                        }
                    }
                    else if (cantidad < cantidadMinima && listaMensajesEnviados.ContainsKey(Convert.ToInt32(idproductoCantidad)))
                    {
                        listaMensajesEnviados.Remove(Convert.ToInt32(idproductoCantidad));
                    }
                }

                if (isDecimal)
                {
                    DGVentas.Rows[celda].Cells[9].Value = (cantidad * Convert.ToDecimal(DGVentas.Rows[celda].Cells[6].Value));
                    txtBuscadorProducto.Focus();
                }
                else
                {
                    DGVentas.Rows[celda].Cells[5].Value = cantidadAnterior;
                    MessageBox.Show("El formato que introdujo no es el correcto; los siguientes son los permitidos:\n0.5(cualquier número despues del punto decimal)\n.5(cualquier número despues del punto decimal)");
                    txtBuscadorProducto.Focus();
                    return;
                }

                // Se agrego esta parte de descuento
                int idProducto = Convert.ToInt32(DGVentas.Rows[0].Cells["IDProducto"].Value);
                int tipoDescuento = Convert.ToInt32(DGVentas.Rows[0].Cells["DescuentoTipo"].Value);

                if (tipoDescuento > 0)
                {
                    string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                    CalcularDescuento(datosDescuento, tipoDescuento, (int)cantidad, 0);
                }

                CalculoMayoreo();
                //CantidadesFinalesVenta();

                CantidadesFinalesVenta();
                if (CantidadAnteriorEdit != NuevaCantidadEdit)
                {
                    reproducirProductoAgregado();
                }

            }
        }

        private void cOtrosImpuestos_Click(object sender, EventArgs e)
        {

        }

        private void cIVA8_Click(object sender, EventArgs e)
        {

        }

        private void cIVA_Click(object sender, EventArgs e)
        {

        }

        private void cSubtotal_Click(object sender, EventArgs e)
        {

        }

        private void cNumeroArticulos_Click(object sender, EventArgs e)
        {

        }

        private void cAnticipo_Click(object sender, EventArgs e)
        {

        }

        private void btn_cancelar_venta_Click(object sender, EventArgs e)
        {
            Ventana_cancelar_venta vnt_cancelar = new Ventana_cancelar_venta();

            vnt_cancelar.FormClosed += delegate
            {
                if (mostrarVenta > 0)
                {
                    //Cargar la venta cancelada 
                    CargarVentaGuardada();

                    mostrarVenta = 0;
                    txtBuscadorProducto.Text = string.Empty;
                }
            };

            vnt_cancelar.ShowDialog();
        }

        //private void listaProductos_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    Brush fuente = Brushes.Black;

        //    foreach (var fila in listaProductos.Items)
        //    {
        //        var stockObtenido = 0f;
        //        var stockMinim = 0f;

        //        string[] datos = buscarProductoPorCodigoClave(fila.ToString());

        //        stockObtenido = float.Parse(datos[1]);
        //        stockMinim = float.Parse(datos[2]);

        //        if (stockObtenido <= stockMinim)
        //        {
        //            // e.Graphics.DrawString(listaProductos.Items[e.Index].ToString(), e.Font, fuente, e.Bounds, StringFormat.GenericDefault);
        //        }
        //        else if (stockObtenido < 1)
        //        {

        //        }
        //        else
        //        {

        //        }
        //    }
        //}

        private void funteListBox()
        {
            //foreach (var fila in listaProductos.Items)
            //{
            //    var stockObtenido = 0f;
            //    var stockMinim = 0f;
            //    var name = string.Empty;

            //    string[] datos = buscarProductoPorCodigoClave(fila.ToString());

            //    name = datos[0].ToString();
            //    stockObtenido = float.Parse(datos[1]);
            //    stockMinim = float.Parse(datos[2]);

            //    if (stockObtenido <= stockMinim)
            //    {
            //        // e.Graphics.DrawString(listaProductos.Items[e.Index].ToString(), e.Font, fuente, e.Bounds, StringFormat.GenericDefault);
            //    }
            //    else if (stockObtenido < 1)
            //    {

            //    }
            //    else
            //    {

            //    }
            //}
        }

        private void btnBascula_Click(object sender, EventArgs e)
        {
            if (DGVentas.Rows.Count > 0)
            {
                decimal elmeropesoxd = 0;
                ObtenerPesoVasculaVentas pesoVentas = new ObtenerPesoVasculaVentas();
                pesoVentas.FormClosed += delegate
                {
                    elmeropesoxd = pesoVentas.peso;
                    //MessageBox.Show(elmeropesoxd.ToString());
                    DGVentas.Rows[0].Cells[5].Value = elmeropesoxd;
                    var id = DGVentas.Rows[0].Cells[0].Value.ToString();
                    cn.EjecutarConsulta($"UPDATE productos SET FormatoDeVenta = '2' WHERE ID = '{id}' AND `Status` = '1' and IDUsuario = {FormPrincipal.userID}");
                    timer.Interval = 2000; // here time in milliseconds
                    timer.Tick += timer_Tick;
                    timer.Start();
                    btnBascula.Enabled = false;
                };
                pesoVentas.ShowDialog();
                //EnviarDatos(); 
            }

        }
        void timer_Tick(object sender, System.EventArgs e)
        {
            btnBascula.Enabled = true;
            timer.Stop();
        }

        private void iniciarBasculaPredeterminada()
        {
            using (DataTable dtBascula = cn.CargarDatos(cs.getBasculaPredeterminada()))
            {
                if (!dtBascula.Rows.Count.Equals(0))
                {
                    btnBascula.Enabled = true;

                    foreach (DataRow drBascula in dtBascula.Rows)
                    {
                        puerto = drBascula["puerto"].ToString();
                        baudRate = drBascula["baudRate"].ToString();
                        dataBits = drBascula["dataBits"].ToString();
                        handshake = drBascula["handshake"].ToString();
                        parity = drBascula["parity"].ToString();
                        stopBits = drBascula["stopBits"].ToString();
                        sendData = drBascula["sendData"].ToString();
                    }

                    try
                    {
                        // Ajustar los parámetros de comunicaciones
                        // adaptándolos a las especificaciones o configuración
                        // de la bascula en concreto.
                        BasculaCom.PortName = puerto;                                               // Conectaremos la bascula al puerto
                        BasculaCom.BaudRate = Convert.ToInt32(baudRate);                            // La velocidad de intercambio
                        BasculaCom.Parity = (Parity)Enum.Parse(typeof(Parity), parity);             // No verificaremos la paridad
                        BasculaCom.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBits);     // Final de Byte con 1 bit de Stop
                        BasculaCom.Open();                                                          // Abrir las comunicaciones con la bascula

                        // Dirigir los eventos a las funciones para procesarlos
                        BasculaCom.DataReceived += new SerialDataReceivedEventHandler(this.Recibir);   // Ejecución de ‘Recibir’ al recibir respuesta de la bascula
                    }
                    catch (Exception error)
                    {
                        //btnBascula.Enabled = false;
                        //MessageBox.Show("Error de conexión con el dispositivo (Bascula)...\n\n" + error.Message.ToString() + "\n\nFavor de revisar los parametros de su bascula para configurarlos correctamente", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    //btnBascula.Enabled = false;
                }
            }
        }

        private void agregarPesoDGVentas()
        {
            if (!peso.Equals(string.Empty))
            {
                if (!DGVentas.Rows.Count.Equals(0))
                {
                    DGVentas.Rows[0].Cells["Cantidad"].Value = peso;
                    //peso = string.Empty;
                    CantidadesFinalesVenta();
                }
            }
        }

        //private string[] buscarProductoPorCodigoClave(string fila)
        //{
        //    List<string> lista = new List<string>();

        //    var nombre = string.Empty; var stock = string.Empty; var stockMinimo = string.Empty;

        //    char delimitador = ':';
        //    string[] words = fila.Split(delimitador);

        //    var palabra = words[1].Trim().ToString();

        //    var query = cn.CargarDatos($"SELECT Nombre, Stock, StockMinimo FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND CodigoBarras = '{palabra}' OR ClaveProducto = '{palabra}' AND Status = 1");

        //    if (!query.Rows.Count.Equals(0))
        //    {
        //        nombre = query.Rows[0]["Nombre"].ToString();
        //        stock = query.Rows[0]["Stock"].ToString();
        //        stockMinimo = query.Rows[0]["StockMinimo"].ToString();

        //        lista.Add(nombre);
        //        lista.Add(stock);
        //        lista.Add(stockMinimo);
        //    }

        //    return lista.ToArray();
        //}

        private void DGVentas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                HitTestInfo hitTestInfo = DGVentas.HitTest(e.X, e.Y);
                int indexColumn = hitTestInfo.ColumnIndex;
                int indexRow = hitTestInfo.RowIndex;

                if ((hitTestInfo.Type == DataGridViewHitTestType.Cell) && (indexColumn.Equals(5) || indexColumn.Equals(8)))
                {
                    DGVentas.BeginEdit(true);
                    if (indexColumn.Equals(5))
                    {
                        cantidadAnterior = Decimal.Parse(DGVentas.Rows[indexRow].Cells[indexColumn].Value.ToString(), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
                        //cantidadAnterior = Convert.ToDecimal(DGVentas.Rows[indexRow].Cells[indexColumn].Value.ToString()); 
                    }
                }
                else
                {
                    DGVentas.EndEdit();
                }
            }
        }

        private void timer_img_producto_Tick(object sender, EventArgs e)
        {
            PBImagen.Image = null;
            PBImagen.Refresh();
            timer_img_producto.Stop();
        }

        private void DGVentas_SelectionChanged(object sender, EventArgs e)
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("NombreCol", typeof(System.Double));
            foreach (DataGridViewRow item in DGVentas.Rows)
            {
                DataRow row = DT.NewRow();
                row["NombreCol"] = Convert.ToDouble(item.Cells["Cantidad"].Value);
                DT.Rows.Add(row);
                break;
            }
            if (!DT.Rows.Count.Equals(0))
            {
                if (Convert.ToDecimal(DT.Rows[0][0]).Equals(0))
                {
                    DGVentas.Rows[0].Cells["Cantidad"].Value = "1";
                }
            }


            //if (!DGVentas.Rows.Count.Equals(0))
            //{
            //    DGVentas.CurrentCell = DGVentas.CurrentRow.Cells["Cantidad"];
            //    DGVentas.BeginEdit(true);
            //}
        }

        private void Ventas_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.OfType<ListadoVentasGuardadas>().Count() == 1)
            {
                Application.OpenForms.OfType<ListadoVentasGuardadas>().First().Close();
            }
            listProductos.Clear();
            liststock2.Clear();
        }

        private bool verificarImpresionTicket()
        {
            var result = false;

            var consulta = cn.CargarDatos($"SELECT HabilitarTicketVentas FROM Configuracion WHERE IDUsuario = '{FormPrincipal.userID}'");

            if (!consulta.Rows.Count.Equals(0))
            {
                result = Convert.ToBoolean(consulta.Rows[0]["HabilitarTicketVentas"].ToString());
            }

            return result;
        }


        /*private void DrawEllipseInt(PaintEventArgs e)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            // Create location and size of ellipse.
            int x = 0;
            int y = 0;
            int width = 20;
            int height = 10;

            // Draw ellipse to screen.
            e.Graphics.DrawEllipse(blackPen, x, y, width, height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen myPen = new Pen(Color.Black);
            Brush myBrush = new HatchBrush(HatchStyle.DottedDiamond, Color.RosyBrown, Color.Brown);
            SolidBrush textBrush = new SolidBrush(Color.Yellow);
            // Draw the button in the form of a rectangle
            graphics.FillRectangle(myBrush, 0, 0, 80, 45);
            graphics.DrawString("Display Drawings", new System.Drawing.Font("Verdana", 11), textBrush, 10, 10);
            myPen.Dispose();
        }*/

    }
}