using System;
using System.Collections.Specialized;
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

        private bool aplicarDescuentoG { get; set; }
        // Almacena los ID de los productos a los que se aplica descuento general
        private Dictionary<int, bool> productosDescuentoG = new Dictionary<int, bool>();
        float porcentajeGeneral = 0;
        float descuentoCliente = 0;

        public static bool ventaGuardada = false; //Para saber si la venta se guardo o no
        int cantidadExtra = 0;

        public static int indiceFila = 0; //Para guardar el indice de la fila cuando se elige agregar multiples productos
        public static int cantidadFila = 0; //Para guardar la cantidad de productos que se agregará a la fila correspondiente

        // Para las ventas guardadas
        public static int mostrarVenta = 0;

        // Estado de la venta
        public static string statusVenta = string.Empty;

        // Para los anticipos por aplicar
        public static string listaAnticipos = string.Empty;
        public static float importeAnticipo = 0f;

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
        // Variables para la configuracion referente a los productos con mayoreo
        private bool mayoreoActivo = false;
        private int cantidadMayoreo = 0;

        // Listas para guardar los ID's de los productos que se enviara correo
        private Dictionary<int, string> enviarStockMinimo;
        private Dictionary<int, string> enviarVentaProducto;

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

        public Ventas()
        {
            InitializeComponent();
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            label1.BackColor = Color.FromArgb(229, 231, 233);
            label2.BackColor = Color.FromArgb(229, 231, 233);
            label3.BackColor = Color.FromArgb(229, 231, 233);
            label4.BackColor = Color.FromArgb(229, 231, 233);
            label5.BackColor = Color.FromArgb(229, 231, 233);
            label6.BackColor = Color.FromArgb(229, 231, 233);
            label7.BackColor = Color.FromArgb(229, 231, 233);
            label8.BackColor = Color.FromArgb(229, 231, 233);
            label10.BackColor = Color.FromArgb(229, 231, 233);
            label11.BackColor = Color.FromArgb(229, 231, 233);

            txtBuscadorProducto.GotFocus  += new EventHandler(BuscarTieneFoco);
            txtBuscadorProducto.LostFocus += new EventHandler(BuscarPierdeFoco);
            txtDescuentoGeneral.GotFocus  += new EventHandler(DescuentoTieneFoco);
            txtDescuentoGeneral.LostFocus += new EventHandler(DescuentoPierdeFoco);

            btnProductoRapido.BackgroundImage = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\plus.png");
            btnEliminarUltimo.BackgroundImage = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");
            btnEliminarTodos.BackgroundImage  = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");
            btnUltimoTicket.BackgroundImage   = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\ticket.png");
            btnPresupuesto.BackgroundImage    = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\money.png");

            btnProductoRapido.BackgroundImageLayout = ImageLayout.Center;
            btnEliminarUltimo.BackgroundImageLayout = ImageLayout.Center;
            btnEliminarTodos.BackgroundImageLayout  = ImageLayout.Center;
            btnUltimoTicket.BackgroundImageLayout   = ImageLayout.Center;
            btnPresupuesto.BackgroundImageLayout    = ImageLayout.Center;


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
            }

            enviarStockMinimo = new Dictionary<int, string>();
            enviarVentaProducto = new Dictionary<int, string>();

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
            }
        }

        private void DescuentoTieneFoco(object sender, EventArgs e)
        {
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
                // Verificar si se selecciono el check para cancelar venta
                //if (checkCancelar.Checked)
                //{
                //    CancelarVenta();
                //    return;
                //}

                if (listaProductos.Visible)
                {
                    // Si busca un producto por codigo de barras o clave le da prioridad a este metodo
                    // si encuentra un producto lo va a mostrar de lo contrario la ejecucion seguira y
                    // se mostrara el primer producto que aparezca en la lista
                    var respuesta = BuscarProductoPorCodigo();

                    if (respuesta)
                    {
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
                    BuscarProductoPorCodigo();


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

                    sumarProducto = false;
                    restarProducto = false;
                    buscarVG = false;
                }  
            }
        }

        private void CancelarVenta()
        {
           // var folio = txtBuscadorProducto.Text.Trim();
            var folio = lFolio.Text.Trim();

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
                        CargarVentaGuardada();
                        mostrarVenta = 0;

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
                                    var cantidad = Convert.ToInt32(info[2]);

                                    cn.EjecutarConsulta($"UPDATE Productos SET Stock =  Stock + {cantidad} WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID}");
                                }
                            }

                            // Agregamos marca de agua al PDF del ticket de la venta cancelada
                            Utilidades.CrearMarcaDeAgua(idVenta, "CANCELADA");

                            var mensaje = MessageBox.Show("¿Desea devolver el dinero?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (mensaje == DialogResult.Yes)
                            {
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
                                            efectivo, tarjeta, vales, cheque, transferencia, credito, anticipo
                                        };

                                    cn.EjecutarConsulta(cs.OperacionCaja(datos));
                                }
                            }
                        }
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
                    var datosTmp = mb.BuscarCodigoBarrasExtra(codigo);

                    if (datosTmp.Length > 0)
                    {
                        foreach (var id in datosTmp)
                        {
                            // Verificar que pertenece al usuario
                            var verificarUsuario = (bool)cn.EjecutarSelect($"SELECT * FROM Productos WHERE ID = {id} AND IDUsuario = {FormPrincipal.userID} AND Status = 1");

                            if (verificarUsuario)
                            {
                                idProducto = Convert.ToInt32(id);
                            }
                        }
                    }


                    if (!string.IsNullOrWhiteSpace(txtBuscadorProducto.Text))
                    {
                        string querySearchProd = $"SELECT prod.ID FROM Productos AS prod WHERE IDUsuario = '{FormPrincipal.userID}' AND (ClaveInterna = '{codigo}' OR CodigoBarras = '{codigo}') AND Status = 1";

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
                                MessageBox.Show(drProdMessg["ProductOfMessage"].ToString().ToUpper(), "Mensaje para el cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                        existe = true;
                    }
                    else
                    {
                        // Se reproducto cuando el codigo o clave buscado no esta registrado
                        /*ReproducirSonido();

                        var respuesta = MessageBox.Show($"El código o clave {codigo} no esta registrado en el sistema", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        if (respuesta == DialogResult.OK)
                        {
                            txtBuscadorProducto.Text = string.Empty;
                            txtBuscadorProducto.Focus();
                        }*/
                    }
                }
            }
            
            return existe;
        }

        private void ReproducirSonido()
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

        private void AgregarProducto(string[] datosProducto, decimal cnt = 1)
        {
            if (DGVentas.Rows.Count == 0 && buscarvVentaGuardada == ".#")
            {
                AgregarProductoLista(datosProducto);
            }
            else if (DGVentas.Rows.Count > 0)
            {   
                bool existe = false;

                foreach (DataGridViewRow fila in DGVentas.Rows)
                {
                    //Compara el valor de la celda con el nombre del producto (Descripcion)
                    if (fila.Cells["Descripcion"].Value.Equals(datosProducto[1]))
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

                        decimal cantidad = Convert.ToDecimal(fila.Cells["Cantidad"].Value) + sumar;
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
                            CalcularDescuento(datosDescuento, tipoDescuento, Convert.ToInt32(cantidad), numeroFila);
                        }

                        CantidadesFinalesVenta();

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
            }

            CalculoMayoreo();
            CantidadesFinalesVenta();
        }

        private void AgregarProductoLista(string[] datosProducto, decimal cantidad = 1, bool ignorar = false)
        {
            decimal cantidadTmp = cantidad;

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
                    if (Convert.ToInt32(nudCantidadPS.Value) > 0)
                    {
                        cantidad = Convert.ToDecimal(nudCantidadPS.Value);
                    }

                    nudCantidadPS.Value = 1;
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
                row.Cells["Cantidad"].Value = cantidad;
                row.Cells["Precio"].Value = datosProducto[2];
                row.Cells["Descripcion"].Value = datosProducto[1];

                if ((datosProducto.Length - 1) == 13)
                {
                    row.Cells["Descuento"].Value = datosProducto[13];
                }
                else
                {
                    row.Cells["Descuento"].Value = "0.00";
                    row.Cells["TipoDescuento"].Value = "0";
                }

                row.Cells["ImagenProducto"].Value = datosProducto[9];

                var imagen = row.Cells["ImagenProducto"].Value.ToString();

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
                float importe = Convert.ToInt32(cantidad) * float.Parse(datosProducto[2]);

                row.Cells["Importe"].Value = importe;

                int idProducto = Convert.ToInt32(datosProducto[0]);
                int tipoDescuento = Convert.ToInt32(datosProducto[3]);
                
                if (tipoDescuento > 0)
                {
                    string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                    CalcularDescuento(datosDescuento, tipoDescuento, Convert.ToInt32(cantidad), rowId);
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
        {
            try
            {
                var celda = DGVentas.CurrentCell.RowIndex;

                // Descuento
                if (e.ColumnIndex == 8)
                {
                    var idProducto = DGVentas.Rows[celda].Cells["IDProducto"].Value.ToString();
                    var nombreProducto = DGVentas.Rows[celda].Cells["Descripcion"].Value.ToString();
                    var precioProducto = DGVentas.Rows[celda].Cells["Precio"].Value.ToString();
                    var cantidadProducto = DGVentas.Rows[celda].Cells["Cantidad"].Value.ToString();

                    var datos = new string[] { idProducto, nombreProducto, precioProducto, cantidadProducto };

                    using (var formDescuento = new AgregarDescuentoDirecto(datos))
                    {
                        // Aqui comprueba si el producto tiene un descuento directo
                        var quitarDescuento = false;

                        if (descuentosDirectos.ContainsKey(Convert.ToInt32(idProducto)))
                        {
                            quitarDescuento = true;
                        }

                        var resultado = formDescuento.ShowDialog();

                        if (resultado == DialogResult.OK)
                        {
                            DGVentas.Rows[celda].Cells["Descuento"].Value = formDescuento.TotalDescuento;
                            DGVentas.Rows[celda].Cells["TipoDescuento"].Value = formDescuento.TipoDescuento;
                        }
                        else
                        {
                            // Si el producto tenia un descuento directo previamente y detecta al cerrar el form
                            // que el descuento fue eliminado pone por defecto cero en la columna correspondiente
                            if (quitarDescuento)
                            {
                                if (!descuentosDirectos.ContainsKey(Convert.ToInt32(idProducto)))
                                {
                                    DGVentas.Rows[celda].Cells["Descuento"].Value = "0.00";
                                }
                            }
                        }
                    }
                }

                // Agregar multiple
                if (e.ColumnIndex == 10)
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

                // Agregar individual
                if (e.ColumnIndex == 11)
                {
                    int idProducto = Convert.ToInt32(DGVentas.Rows[celda].Cells["IDProducto"].Value);
                    int tipoDescuento = Convert.ToInt32(DGVentas.Rows[celda].Cells["DescuentoTipo"].Value);
                    var precio = float.Parse(DGVentas.Rows[celda].Cells["Precio"].Value.ToString());
                    int cantidad = Convert.ToInt32(DGVentas.Rows[celda].Cells["Cantidad"].Value) + 1;

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
                            DGVentas.Rows[celda].Cells["Descuento"].Value = $"{descuentoTmp.ToString("N2")} - {porcentaje}%";
                        }
                    }

                    DGVentas.Rows[celda].Cells["Cantidad"].Value = cantidad;
                    DGVentas.Rows[celda].Cells["Importe"].Value = importe;

                    if (tipoDescuento > 0)
                    {
                        string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                        CalcularDescuento(datosDescuento, tipoDescuento, cantidad, celda);
                    }
                }

                // Restar individual
                if (e.ColumnIndex == 12)
                {
                    int cantidad = Convert.ToInt32(DGVentas.Rows[celda].Cells["Cantidad"].Value);
                    cantidad -= 1;

                    if (cantidad > 0)
                    {
                        int idProducto = Convert.ToInt32(DGVentas.Rows[celda].Cells["IDProducto"].Value);
                        int tipoDescuento = Convert.ToInt32(DGVentas.Rows[celda].Cells["DescuentoTipo"].Value);
                        var precio = float.Parse(DGVentas.Rows[celda].Cells["Precio"].Value.ToString());

                        float importe = cantidad * precio;

                        // Verificar si tiene descuento directo
                        if (descuentosDirectos.ContainsKey(idProducto))
                        {
                            var tipoDescuentoDirecto = descuentosDirectos[idProducto].Item1;

                            // Si el descuento directo es el de cantidad
                            if (tipoDescuentoDirecto == 1)
                            {
                                var descuento = float.Parse(DGVentas.Rows[celda].Cells["Descuento"].Value.ToString());

                                // Cuando cantidad de producto sea igual a 1 hara esto
                                if (cantidad == 1)
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
                                var porcentaje = descuentosDirectos[idProducto].Item2;

                                var descuentoTmp = (precio * cantidad) * (porcentaje / 100);
                                var importeTmp = (precio * cantidad) - descuentoTmp;

                                importe = importeTmp;
                                DGVentas.Rows[celda].Cells["Descuento"].Value = $"{descuentoTmp.ToString("N2")} - {porcentaje}%";
                            }
                        }

                        DGVentas.Rows[celda].Cells["Cantidad"].Value = cantidad;
                        DGVentas.Rows[celda].Cells["Importe"].Value = importe;

                        if (tipoDescuento > 0)
                        {
                            string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                            CalcularDescuento(datosDescuento, tipoDescuento, cantidad, celda);
                        }
                    }
                    else
                    {
                        var idProducto = Convert.ToInt32(DGVentas.Rows[celda].Cells["IDProducto"].Value);

                        DGVentas.Rows.RemoveAt(celda);

                        if (productosDescuentoG.ContainsKey(idProducto))
                        {
                            productosDescuentoG.Remove(idProducto);
                        }

                        if (descuentosDirectos.ContainsKey(idProducto))
                        {
                            descuentosDirectos.Remove(idProducto);
                        }
                    }
                }

                // Eliminar individual
                if (e.ColumnIndex == 13)
                {
                    var idProducto = Convert.ToInt32(DGVentas.Rows[celda].Cells["IDProducto"].Value);

                    DGVentas.Rows.RemoveAt(celda);

                    if (productosDescuentoG.ContainsKey(idProducto))
                    {
                        productosDescuentoG.Remove(idProducto);
                    }

                    if (descuentosDirectos.ContainsKey(idProducto))
                    {
                        descuentosDirectos.Remove(idProducto);
                    }
                }
            }
            catch (Exception)
            {
                
            }
            
            DGVentas.ClearSelection();
            CalculoMayoreo();
            CantidadesFinalesVenta();
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
                CalcularDescuento(datosDescuento, tipoDescuento, cantidad, indiceFila);
            }
            
            indiceFila = 0;
            cantidadFila = 0;
        }

        private void CalcularDescuento(string[] datosDescuento, int tipo, int cantidad, int fila = 0)
        {
            //Cliente
            if (tipo == 1)
            {

            }

            //Mayoreo
            if (tipo == 2)
            {
                var restantes = 0;
                var totalImporte = 0f;
                // Esta variable almace el ultimo checkbox al que se le marco casilla en el descuento
                var ultimoCheckbox = string.Empty;

                List<string> listaDescuentos = new List<string>();

                foreach (var descuento in datosDescuento)
                {
                    var info = descuento.Split('-');
                    
                    if (Convert.ToInt32(info[3]) == 1)
                    {
                        ultimoCheckbox = descuento;
                        break;
                    }
                }

                // Invertimos los valores del array de los descuentos
                listaDescuentos = datosDescuento.ToList();
                listaDescuentos.Reverse();
                datosDescuento = listaDescuentos.ToArray();


                if (!string.IsNullOrWhiteSpace(ultimoCheckbox))
                {
                    var info = ultimoCheckbox.Split('-');
                    var rangoInicialCheck = Convert.ToInt32(info[0]);
                    var rangoFinalCheck = info[1];
                    var precioFinalCheck = float.Parse(info[2]);

                    if (rangoFinalCheck != "N")
                    {
                        // Aqui se puso directamente el 1 en lugar de la variable rangoInicialCheck
                        // para el caso cuando el checkbox marcado no es el primero sino el segundo por ejemplo
                        // y asi logre entrar a la condicion
                        if (cantidad >= 1 && cantidad <= Convert.ToInt32(rangoFinalCheck))
                        {
                            totalImporte += cantidad * precioFinalCheck;

                            DGVentas.Rows[fila].Cells["AplicarDescuento"].Value = 0;
                        }
                        else
                        {
                            if (cantidad > Convert.ToInt32(rangoFinalCheck))
                            {
                                restantes = Math.Abs(cantidad - Convert.ToInt32(rangoFinalCheck));

                                totalImporte += Convert.ToInt32(rangoFinalCheck) * precioFinalCheck;

                                DGVentas.Rows[fila].Cells["AplicarDescuento"].Value = 1;
                            }
                        }

                        // Creamos una lista auxiliar con los valores del array para eliminar el descuento
                        // que se tomo en cuenta con el checkbox y los que esten anteriores a el
                        var auxiliar = datosDescuento.ToList();

                        foreach (var descuento in datosDescuento)
                        {
                            if (descuento.Equals(ultimoCheckbox))
                            {
                                auxiliar.Remove(descuento);
                                break;
                            }

                            auxiliar.Remove(descuento);
                        }

                        datosDescuento = auxiliar.ToArray();
                    }
                    else
                    {
                        // Aqui no se hace porque no fue necesario
                    }
                }
                else
                {
                    restantes = cantidad;
                }

                var longitud = datosDescuento.Length;

                for (int i = 0; i < longitud; i++)
                {
                    var info = datosDescuento[i].Split('-');
                    var rangoInicial = Convert.ToInt32(info[0]);
                    var rangoFinal = info[1];
                    var precioFinal = float.Parse(info[2]);
                    var checkFinal = Convert.ToInt32(info[3]);

                    if (rangoFinal != "N")
                    {
                        if (cantidad >= rangoInicial && cantidad <= Convert.ToInt32(rangoFinal))
                        {
                            cantidad = restantes;

                            totalImporte += cantidad * precioFinal;
                        }
                        else
                        {
                            if (cantidad > Convert.ToInt32(rangoFinal))
                            {
                                // Aqui no se hace porque no fue necesario
                            }
                        }
                    }
                    else
                    {
                        if (cantidad >= rangoInicial)
                        {
                            cantidad = restantes;

                            totalImporte += cantidad * precioFinal;
                        }
                    }
                }

                float importe = float.Parse(DGVentas.Rows[fila].Cells["Importe"].Value.ToString());
                float descuentoFinal = importe - totalImporte;

                if (descuentoFinal < 0)
                {
                    descuentoFinal = 0;
                }

                DGVentas.Rows[fila].Cells["Descuento"].Value = descuentoFinal;
                DGVentas.Rows[fila].Cells["Importe"].Value = totalImporte;
            }
        }

        private void CantidadesFinalesVenta()
        {
            decimal totalArticulos = 0;
            double totalImporte   = 0;
            double totalDescuento = 0;
            double totalSubtotal  = 0;
            double totalIVA16     = 0;
            double totalAnticipos = 0;

            foreach (DataGridViewRow fila in DGVentas.Rows)
            {
                var idProducto = Convert.ToInt32(fila.Cells["IDProducto"].Value);
                var porcentajeGeneralAux = 0f;
                var descuentoClienteAux = 0f;
                var esDescuentoDirecto = false;

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

                    totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
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

                        totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
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

                        totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
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

                    totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
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

                    var importeProducto = (precioOriginal * Convert.ToDouble(cantidadProducto)) - cantidadDescuento;

                    fila.Cells["Importe"].Value = importeProducto.ToString("0.00");

                    totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
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
            }

            totalSubtotal = totalImporte / 1.16;
            totalIVA16    = totalSubtotal * 0.16;

            totalAnticipos = Convert.ToDouble(cAnticipo.Text);
            totalAnticipos += importeAnticipo;

            if (totalImporte > 0)
            {
                var importeTmp = totalImporte;

                if ((totalImporte - totalAnticipos) <= 0)
                {
                    totalImporte = 0;

                    if (totalAnticipos > 0)
                    {
                        if (importeTmp <= totalAnticipos)
                        {
                            cAnticipoUtilizado.Text = importeTmp.ToString("0.00");
                        }
                    }
                }

                if ((totalImporte - totalAnticipos) > 0)
                {
                    totalImporte -= totalAnticipos;

                    if (totalAnticipos > 0)
                    {
                        if (totalImporte <= totalAnticipos)
                        {
                            var diferencia = importeTmp - totalImporte;

                            cAnticipoUtilizado.Text = diferencia.ToString("0.00");
                        }
                        else
                        {
                            var diferencia = importeTmp - totalImporte;

                            cAnticipoUtilizado.Text = diferencia.ToString("0.00");
                        }
                    }
                }
            }

            cIVA.Text = totalIVA16.ToString("0.00");
            cTotal.Text = totalImporte.ToString("0.00");
            cSubtotal.Text = totalSubtotal.ToString("0.00");

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

            cAnticipo.Text = totalAnticipos.ToString("0.00");
            cDescuento.Text = totalDescuento.ToString("0.00");
            cNumeroArticulos.Text = totalArticulos.ToString();

            ComprobarProductos();
        }

        private void ComprobarProductos()
        {
            if (DGVentas.RowCount == 0)
            {
                listaAnticipos = string.Empty;
                importeAnticipo = 0f;
                cAnticipo.Text = "0.00";
                cAnticipoUtilizado.Text = "0.00";
            }
        }

        private void btnEliminarUltimo_Click(object sender, EventArgs e)
        {
            if (opcion17 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (DGVentas.Rows.Count > 0)
            {
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
            }
        }

        private void btnEliminarTodos_Click(object sender, EventArgs e)
        {
            DialogResult dialogoResult = MessageBox.Show("¿Desea remover todos los artículos?", "!Advertencia¡", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogoResult == DialogResult.Yes)
            {
                if (opcion18 == 0)
                {
                    Utilidades.MensajePermiso();
                    return;
                }

                DGVentas.Rows.Clear();
                // Almacena los ID de los productos a los que se aplica descuento general
                productosDescuentoG.Clear();
                // Guarda los datos de los descuentos directos que se han aplicado
                descuentosDirectos.Clear();

                CalculoMayoreo();
                CantidadesFinalesVenta();
            }
            else if (dialogoResult == DialogResult.No)
            {

            }

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
                    var totalVenta = float.Parse(cTotal.Text);

                    DetalleVenta detalle = new DetalleVenta(totalVenta, idCliente);

                    detalle.FormClosed += delegate
                    {
                        if (botonAceptar)
                        {
                            DatosVenta();
                            botonAceptar = false;
                            idCliente = string.Empty;
                            DetalleVenta.idCliente = 0;
                            DetalleVenta.cliente = string.Empty;
                            AsignarCreditoVenta.idCliente = 0;
                            AsignarCreditoVenta.cliente = string.Empty;
                        }
                        else
                        {
                            idCliente = string.Empty;
                            DetalleVenta.idCliente = 0;
                            DetalleVenta.cliente = string.Empty;
                            AsignarCreditoVenta.idCliente = 0;
                            AsignarCreditoVenta.cliente = string.Empty;
                        }
                    };

                    detalle.Show();
                }
            }
        }

        //Se procesa la informacion de los detalles de la venta para guardarse
        private void DetallesVenta(string IDVenta)
        {
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
                if (idCliente != "0")
                {
                    var datos = mb.ObtenerDatosCliente(Convert.ToInt32(idCliente), FormPrincipal.userID);
                    var cliente = datos[0];
                    var rfc = datos[1];

                    var info = new string[] { cliente, rfc, idVenta, FormPrincipal.userID.ToString() };

                    cn.EjecutarConsulta(cs.ActualizarClienteVenta(info));
                }
                else
                {
                    var info = new string[] {
                        "PUBLICO GENERAL", "XAXX010101000", idVenta,
                        FormPrincipal.userID.ToString()
                    };

                    cn.EjecutarConsulta(cs.ActualizarClienteVenta(info));
                }
            }
        }

        private void DatosVenta()
        {
            // Datos generales de la venta
            var IdEmpresa = FormPrincipal.userID.ToString();
            var Subtotal = cSubtotal.Text;
            var IVA16 = cIVA.Text;
            var Descuento = cDescuento.Text;
            var Total = cTotal.Text;
            var DescuentoGeneral = porcentajeGeneral.ToString("0.00");
            var Anticipo = cAnticipo.Text;
            var AnticipoUtilizado = cAnticipoUtilizado.Text;
            var FechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var Folio = "";
            var Serie = "A";
            var idClienteTmp = idCliente;

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

            var guardar = new string[] {
                IdEmpresa, idClienteTmp, IdEmpresa, Subtotal, IVA16, Total, Descuento,
                DescuentoGeneral, Anticipo, Folio, Serie, statusVenta, FechaOperacion,
                idClienteDescuento.ToString()
            };


            if (VerificarStockProducto())
            {
                // Se hace el guardado de la informacion general de la venta
                int respuesta = cn.EjecutarConsulta(cs.GuardarVenta(guardar, mostrarVenta));

                if (respuesta > 0)
                {
                    // Operacion para afectar la tabla de Caja
                    // var saldoActual = cn.ObtenerSaldoActual(FormPrincipal.userID);
                    // var totalTmp = saldoActual + Convert.ToDouble(Total);

                    string[] datos = new string[] {
                        "venta", Total, "0", "", FechaOperacion, FormPrincipal.userID.ToString(),
                        efectivo, tarjeta, vales, cheque, transferencia, credito, Anticipo
                    };

                    cn.EjecutarConsulta(cs.OperacionCaja(datos));


                    // Obtener ID de la venta
                    string idVenta = cn.EjecutarSelect("SELECT ID FROM Ventas ORDER BY ID DESC LIMIT 1", 1).ToString();

                    // Si la lista ventasGuardadas contiene elementos quiere decir que son ventas que deberian 
                    // eliminarse junto con sus productos de la tabla ProductosVenta
                    foreach (var venta in ventasGuardadas)
                    {
                        cn.EjecutarConsulta($"DELETE FROM Ventas WHERE ID = {venta} AND IDUsuario = {FormPrincipal.userID} AND Status = 2");
                        cn.EjecutarConsulta(cs.EliminarProductosVenta(venta));
                    }

                    // Array para almacenar la informacion de los productos vendidos
                    string[][] infoProductos = new string[DGVentas.Rows.Count][];

                    int contador = 0;

                    // Datos de los productos vendidos
                    foreach (DataGridViewRow fila in DGVentas.Rows)
                    {
                        var IDProducto = fila.Cells["IDProducto"].Value.ToString();
                        var Nombre = fila.Cells["Descripcion"].Value.ToString();
                        var Cantidad = fila.Cells["Cantidad"].Value.ToString();
                        var Precio = fila.Cells["Precio"].Value.ToString();
                        var Tipo = fila.Cells["TipoPS"].Value.ToString();

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

                        // A partir de la variable DescuentoGeneral esos valores y datos se toman solo para el ticket de venta
                        guardar = new string[] {
                            idVenta, IDProducto, Nombre, Cantidad, Precio,
                            DescuentoGeneral, DescuentoIndividual, ImporteIndividual,
                            Descuento, Total, Folio, AnticipoUtilizado, TipoDescuento
                        };

                        // Guardar info de los productos
                        infoProductos[contador] = guardar;

                        contador++;

                        // Si es un producto, paquete o servicio lo guarda en la tabla de productos de venta
                        if (Tipo == "P" || Tipo == "S" || Tipo == "PQ")
                        {
                            cn.EjecutarConsulta(cs.GuardarProductosVenta(guardar));
                        }

                        // Si la venta no fue guardada con el boton "Guardar"
                        if (!ventaGuardada)
                        {
                            // Producto
                            if (Tipo == "P")
                            {
                                // Actualizar stock de productos
                                var stock = Convert.ToDecimal(fila.Cells["Stock"].Value);
                                var vendidos = Convert.ToDecimal(fila.Cells["Cantidad"].Value);
                                var restantes = (stock - vendidos).ToString();

                                // Comprobar si aplica para el envio de correo ya sea de stock minimo, de venta o ambos
                                if (correoStockMinimo == 1 || correoVentaProducto == 1)
                                {
                                    var configProducto = mb.ComprobarCorreoProducto(Convert.ToInt32(IDProducto));

                                    if (configProducto.Count > 0)
                                    {
                                        var datosProductoTmp = cn.BuscarProducto(Convert.ToInt32(IDProducto), FormPrincipal.userID);

                                        // Correo de stock minimo
                                        if (configProducto[2] == 1)
                                        {
                                            // Obtener el stock minimo del producto
                                            var stockMinimo = Convert.ToInt32(datosProductoTmp[10]);
                                            var stockTmp = Convert.ToInt32(stock);

                                            if (stockTmp <= stockMinimo)
                                            {
                                                if (!enviarStockMinimo.ContainsKey(Convert.ToInt32(IDProducto)))
                                                {
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
                                                var nombre = $"{datosProductoTmp[1]} --- CÓDIGO BARRAS: {datosProductoTmp[7]} --- STOCK ACTUAL: {restantes}";
                                                enviarVentaProducto.Add(Convert.ToInt32(IDProducto), nombre);
                                            }
                                        }
                                    }
                                }

                                // Actualizar el stock
                                guardar = new string[] { IDProducto, restantes, FormPrincipal.userID.ToString() };

                                cn.EjecutarConsulta(cs.ActualizarStockProductos(guardar));
                            }

                            // Servicio o paquete
                            if (Tipo == "S" || Tipo == "PQ")
                            {
                                var vendidos = Convert.ToDecimal(fila.Cells["Cantidad"].Value);

                                var datosServicio = cn.ObtenerProductosServicio(Convert.ToInt32(IDProducto));

                                foreach (string producto in datosServicio)
                                {
                                    var datosProducto = producto.Split('|');
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
                                                var stockMinimo = Convert.ToInt32(datosProductoTmp[10]);
                                                var stockTmp = Convert.ToInt32(datosProductoTmp[4]);

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

                            // Guardar detalles de la venta
                            DetallesVenta(idVenta);
                            DetallesCliente(idVenta);
                        }
                        else
                        {
                            DetallesCliente(idVenta);
                        }
                    }

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
                                }
                            }

                            contadorAux++;
                        }

                        cn.EjecutarConsulta($"UPDATE DetallesVenta SET Anticipo = '{Anticipo}' WHERE IDVenta = {idVenta} AND IDUsuario = {FormPrincipal.userID}");
                    }

                    GenerarTicket(infoProductos);

                    if (ventaGuardada)
                    {
                        if (statusVenta.Equals("2"))
                        {
                            Utilidades.CrearMarcaDeAgua(Convert.ToInt32(idVenta), "PRESUPUESTO");
                        }
                    }

                    if (Utilidades.AdobeReaderInstalado())
                    {
                        ImprimirTicket(idVenta);
                    }
                    else
                    {
                        Utilidades.MensajeAdobeReader();
                    }
                }

                LimpiarVariables();

                // Hilo para envio de correos en segundo plano
                Thread envio = new Thread(() => CuerpoEmails());
                envio.Start();

                this.Dispose();
            }
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
            descuentosDirectos.Clear();
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
                ListaClientes cliente = new ListaClientes();

                cliente.FormClosed += delegate
                {
                    if (ventaGuardada)
                    {
                        DatosVenta();
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
            if (Properties.Settings.Default.StockNegativo == false)
            {
                if (DGVentas.Rows.Count > 0)
                {
                    foreach (DataGridViewRow fila in DGVentas.Rows)
                    {
                        var stock = Convert.ToInt32(fila.Cells["Stock"].Value);
                        var cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value);
                        var tipoPS = fila.Cells["TipoPS"].Value.ToString();

                        // Es producto
                        if (tipoPS == "P")
                        {
                            if (stock < cantidad)
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
                                    var stockRequerido = Convert.ToInt32(datosProducto[1]) * cantidad;

                                    datosProducto = cn.VerificarStockProducto(idProducto, FormPrincipal.userID);
                                    datosProducto = datosProducto[0].Split('|');

                                    var nombreProducto = datosProducto[0];
                                    var stockActual = Convert.ToInt32(datosProducto[1]);

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

            return respuesta;
        }

        private void btnVentasGuardadas_Click(object sender, EventArgs e)
        {
            if (opcion13 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

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


        private void CargarVentaGuardada()
        {
            string[] datos = cn.BuscarVentaGuardada(mostrarVenta, FormPrincipal.userID);

            cSubtotal.Text = datos[0];
            cIVA.Text = datos[1];
            cTotal.Text = datos[2];
            cDescuento.Text = datos[3];

            // Cuando la venta guardada tiene descuento por cliente
            var idClienteDesc = Convert.ToInt32(datos[8]);

            if (idClienteDesc > 0)
            {
                var datosCliente = mb.ObtenerDatosCliente(idClienteDesc, FormPrincipal.userID);
                var cliente = string.Empty;

                var auxPrimero = string.IsNullOrWhiteSpace(datosCliente[0]);
                var auxSegundo = string.IsNullOrWhiteSpace(datosCliente[1]);
                var auxTercero = string.IsNullOrWhiteSpace(datosCliente[17]);

                if (!auxPrimero) { cliente += $"Cliente: {datosCliente[0]}"; }
                if (!auxSegundo) { cliente += $" --- RFC: {datosCliente[1]}"; }
                if (!auxTercero) { cliente += $" --- No. {datosCliente[17]}"; }

                lbDatosCliente.Text = cliente;
                lbEliminarCliente.Visible = true;
            }

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

                    int cantidad = Convert.ToInt32(info[2]);

                    // Agregamos los descuentos directos de la venta guardada
                    if (Convert.ToInt16(info[4]) > 0)
                    {
                        if (!descuentosDirectos.ContainsKey(idProducto))
                        {
                            var tipoDescuento = Convert.ToInt32(info[4]);
                            var cantidadDescuento = 0f;

                            if (tipoDescuento == 3)
                            {
                                // Descuento cliente
                                var cantidadTmp = info[3].Split('-');
                                cantidadTmp[1] = cantidadTmp[1].Replace('%', ' ');
                                cantidadDescuento = float.Parse(cantidadTmp[1].Trim());

                            }
                            else if (tipoDescuento == 2)
                            {
                                // Descuento directo
                                var cantidadTmp = info[3].Split('-');
                                cantidadTmp[1] = cantidadTmp[1].Replace('%', ' ');
                                cantidadDescuento = float.Parse(cantidadTmp[1].Trim());
                            }
                            else if (tipoDescuento == 1)
                            {
                                // Descuento directo
                                cantidadDescuento = float.Parse(info[3].Trim());
                            }

                            descuentosDirectos.Add(idProducto, new Tuple<int, float>(tipoDescuento, cantidadDescuento));

                            datosProducto = new List<string>(datosProducto) { info[3] }.ToArray();
                        }
                    }

                    //AgregarProductoLista(datosProducto, cantidad, true);
                    AgregarProducto(datosProducto, cantidad);
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
                    mostrarVenta = 0;
                    listaAnticipos = string.Empty;
                    ventasGuardadas.Clear();
                    descuentosDirectos.Clear();
                }
            }
            else
            {
                mostrarVenta = 0;
                listaAnticipos = string.Empty;
                ventasGuardadas.Clear();
                descuentosDirectos.Clear();
            }
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
            var altoPapel  = Convert.ToInt32(anchoPapel + 72); // 54 64 68

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

            if (tipoPapel == 80)
            {
                anchoColumnas = new float[] { 7f, 24f, 10f, 10f, 10f };
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

            Paragraph titulo = new Paragraph(datos[0] + "\n", fuenteGrande);
            Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            domicilio.Alignment = Element.ALIGN_CENTER;
            domicilio.SetLeading(espacio, 0);

            /**************************************
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

            Paragraph mensaje = new Paragraph("\nCambios y Garantía máximo 7 días después de su compra, presentando el Ticket. Gracias por su preferencia.\n\n", fuenteNormal);
            mensaje.Alignment = Element.ALIGN_CENTER;

            var culture = new System.Globalization.CultureInfo("es-MX");
            var dia = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
            var fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm tt");

            dia = cn.Capitalizar(dia);

            Paragraph diaVenta = new Paragraph($"{dia} - {fecha} - Folio: {productos[0][10]}", fuenteNormal);
            diaVenta.Alignment = Element.ALIGN_CENTER;

            ticket.Add(titulo);
            ticket.Add(domicilio);
            ticket.Add(tabla);
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
                System.Threading.Thread.Sleep(5000);

                if (false == p.CloseMainWindow())
                {
                    p.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error No: "+ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //  -----------------------------------------------------------------
            //  |   Expresion Regular                       |   Ejemplo         |
            //  |---------------------------------------------------------------|
            //  |   (digito+espacioBlanco*espacioBlanco)    |   5 * 15665132    |
            //  |   ((Signo+)digito+ || digito+(Signo+))    |   +2 || 2+        |
            //  |   ((Signo-)digito+ || digito+(Signo-))    |   -1 || 1-        |
            //  |   (digito+(Signo*)+espacioBlanco)         |   5* 15665132     |
            //  |   (digito+(Signo*)+espacioBlanco)         |   5 *15665132     |
            //  |   (digito+(Signo*)+espacioBlanco)         |   5*15665132      |
            //  |   (#+espacioBlanco)                       |   # FolioDeVenta  |
            //  -----------------------------------------------------------------

            string primerPatron = @"^\d+\s\*\s";
            string segundoPatron = @"^(\+\d+)|(\d+\+)|(\+)|(\+\+)$";
            string tercerPatron = @"^(\-\d+)|(\d+\-)|(\-)|(\-\-)$";
            string cuartoPatron = @"^\d+\*\s";
            string quintoPatron = @"^\d+\s\*";
            string sextoPatron = @"^\d+\*";
            string septimoPatron = @"^.#\d+\.";
            //string septimoPatron = @"^#\s\d+";

            Match primeraCoincidencia = Regex.Match(cadena, primerPatron, RegexOptions.IgnoreCase);
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

                cantidadExtra = Convert.ToInt32(resultado[0]);

                if (cantidadExtra != 0)
                {
                    if (cantidadExtra >= nudCantidadPS.Minimum && cantidadExtra <= nudCantidadPS.Maximum)
                    {
                        nudCantidadPS.Value = cantidadExtra;
                    }
                }

                cadena = Regex.Replace(cadena, primerPatron, string.Empty);
            }
            else if (segundaCoincidencia.Success)
            {
                if (sumarProducto)
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
                            cantidadExtra = Convert.ToInt32(infoTmp[1]);
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
                                var cantidad = Convert.ToInt32(DGVentas.Rows[0].Cells["Cantidad"].Value);
                                
                                cantidad += cantidadExtra;

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
                                    CalcularDescuento(datosDescuento, tipoDescuento, cantidad, 0);
                                }

                                CalculoMayoreo();
                                CantidadesFinalesVenta();

                                cantidadExtra = 0;
                            }
                        }
                    }
                }  
            }
            else if (terceraCoincidencia.Success)
            {
                if (restarProducto)
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
                                var cantidad = Convert.ToInt32(DGVentas.Rows[0].Cells["Cantidad"].Value);

                                cantidad += cantidadExtra;

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
                                    CalcularDescuento(datosDescuento, tipoDescuento, cantidad, 0);
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

                cantidadExtra = Convert.ToInt32(resultado[0]);

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

            ocultarResultados();

            return cadena;
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

            // Cuando presiona la tecla fin hace click en el boton terminar venta
            if (e.KeyData == Keys.End)
            {
                btnTerminarVenta.PerformClick();
            }

            // Cuando presione la tecla escape debera cerrar la ventana
            if (e.KeyData == Keys.Escape)
            {
                Close();
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

            txtBuscadorProducto.Text = VerificarPatronesBusqueda(txtBuscadorProducto.Text);

            if (txtBuscadorProducto.Text.Length == 0)
            {
                ocultarResultados();
                return;
            }

            // Regresa un diccionario
            //var resultados = mb.BuscarProducto(txtBuscadorProducto.Text);
            var resultados = mb.BusquedaCoincidenciasVentas(txtBuscadorProducto.Text.Trim(), mostrarPrecioProducto, mostrarCBProducto);
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

        private void txtBuscadorProducto_KeyUp(object sender, KeyEventArgs e)
        {
            // Detecta si esta habilitado el checkbox para cancelar venta, si esta
            // habilitado se detienen todas las operaciones normales del evento keyup
            if (checkCancelar.Checked)
            {
                return;
            }

            var busqueda = txtBuscadorProducto.Text.Trim();

            // Verificar si el primer caracter es + o - para evitar la busqueda
            // y que tome en cuenta el atajo para aumentar o disminuir cantidad
            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                var caracter = busqueda[0].ToString();

                if (caracter.Equals("+") || caracter.Equals("-"))
                {
                    return;
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

            // Si encuentra una coincidencia de los patrones cambia el tiempo de busqueda
            // del timer para que haga la operación más rápido
            txtBuscadorProducto.Text = VerificarPatronesBusqueda(txtBuscadorProducto.Text);

            if (string.IsNullOrWhiteSpace(txtBuscadorProducto.Text))
            {
                timerBusqueda.Interval = 1;
            }

            timerBusqueda.Stop();
            timerBusqueda.Start();
        }

        private void listaProductos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ProductoSeleccionado();
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
            }
        }

        private void timerBusqueda_Tick(object sender, EventArgs e)
        {
            timerBusqueda.Interval = 1000;
            timerBusqueda.Stop();
            OperacionBusqueda();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ConsultarProductoVentas>().Count() == 1)
            {
                Application.OpenForms.OfType<ConsultarProductoVentas>().First().BringToFront();
            }
            else
            {
                var consulta = new ConsultarProductoVentas();

                consulta.Show();
            }
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
            if (opcion19 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            DescuentoGeneral();

            var mensaje = "¿Desea aplicar este descuento a los siguientes\nproductos que se agreguen a esta venta?";

            var respuesta = MessageBox.Show(mensaje, "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                aplicarDescuentoG = true;
            }
            else
            {
                aplicarDescuentoG = false;
            }
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
        }

        private void ProductoSeleccionado()
        {
            //Se obtiene el texto del item seleccionado del ListBox
            producto = listaProductos.Items[listaProductos.SelectedIndex].ToString();
            //Se obtiene el indice del array donde se encuentra el producto seleccionado
            //idProducto = Convert.ToInt32(datos.GetKey(Array.IndexOf(productos, producto)));
            idProducto = productosD.FirstOrDefault(x => x.Value == producto).Key;

            datosProducto = cn.BuscarProducto(idProducto, FormPrincipal.userID);

            using (dtProdMessg = cn.CargarDatos(cs.ObtenerProductMessage(Convert.ToString(idProducto))))
            {
                if (dtProdMessg.Rows.Count > 0)
                {
                    drProdMessg = dtProdMessg.Rows[0];

                    var mensaje = drProdMessg["ProductOfMessage"].ToString().ToUpper();

                    MessageBox.Show(mensaje, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            //borrar producto a buscar
            txtBuscadorProducto.Text = "";
            txtBuscadorProducto.Focus();
            ocultarResultados();

            if (!productosDescuentoG.ContainsKey(idProducto))
            {
                productosDescuentoG.Add(idProducto, aplicarDescuentoG);
            }

            AgregarProducto(datosProducto);
        }

        private void CalculoMayoreo()
        {
            int contadorMayoreo = 0;
            // Si la casilla de mayoreo de config esta activa
            if (mayoreoActivo)
            {
                // Si la cantidad minima es mayor a cero
                if (cantidadMayoreo > 0)
                {
                    foreach (DataGridViewRow fila in DGVentas.Rows)
                    {
                        var mayoreo = float.Parse(fila.Cells["PrecioMayoreo"].Value.ToString());
                        var cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value.ToString());

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
            txtDescuentoGeneral.Text = "";
            txtDescuentoGeneral.Select(0, 0);
        }

        private void checkCancelar_MouseClick(object sender, MouseEventArgs e)
        {
            if (checkCancelar.Checked)
            {
                lFolio.Visible = true;
                lFolio.Text = "";
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
                CancelarVenta();
                lFolio.Text = "";
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
        }

        private void lbEliminarCliente_Click(object sender, EventArgs e)
        {
            descuentoCliente = 0;
            CantidadesFinalesVenta();
            lbDatosCliente.Text = string.Empty;
            lbEliminarCliente.Visible = false;
        }

        private void CuerpoEmails()
        {
            var correo = FormPrincipal.datosUsuario[9];
            var asunto = "Información estado de Productos";
            var html = string.Empty;

            if (!string.IsNullOrWhiteSpace(correo))
            {
                // Comprobar stock minimo
                if (enviarStockMinimo.Count > 0)
                {
                    html = @"
                    <div style='margin-bottom: 50px;'>
                        <h4 style='text-align: center;'>PRODUCTOS CON STOCK MINIMO</h4><hr>
                        <ul style='color: red; font-size: 0.8em;'>";

                    foreach (var producto in enviarStockMinimo)
                    {
                        html += $"<li>{producto.Value}</li>";
                    }

                    html += @"
                        </ul>
                    </div>";
                }

                // Comprobar venta producto
                if (enviarVentaProducto.Count > 0)
                {
                    html += @"
                    <div>
                        <h4 style='text-align: center;'>LISTADO PRODUCTOS VENDIDOS</h4><hr>
                        <ul style='color: red; font-size: 0.8em;'>";

                    foreach (var producto in enviarVentaProducto)
                    {
                        html += $"<li>{producto.Value}</li>";
                    }

                    html += @"
                        </ul>
                        <p style='font-size: 0.8em; text-align: center;'><b>NOTA:</b> Es posible que se hayan vendido otros artículos junto a estos productos en la misma venta,
                        los productos aquí listados son sólo los que el usuario configuró para recibir notificaciones a través de correo electrónico.</p>
                    </div>";
                }

                if (!string.IsNullOrWhiteSpace(html))
                {
                    Utilidades.EnviarEmail(html, asunto, correo);
                }
            }
        }
    }
}