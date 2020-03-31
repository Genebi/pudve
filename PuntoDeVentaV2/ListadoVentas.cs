using iTextSharp.text.pdf;
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

        public string rutaLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private string ticketGenerado = string.Empty;
        private string rutaTicketGenerado = string.Empty;
        private DateTime fechaUltimoCorte;

        public static bool recargarDatos = false;
        public static bool abrirNuevaVenta = false;

        public static int tipo_venta = 0;
        public static string[][] faltantes_productos;
        private static WebSettings _webSettings;
        #region Variables Globales Para Paginar
        private Paginar p;
        string DataMemberDGV = "Ventas";
        int maximo_x_pagina = 10;
        string FiltroAvanzado = string.Empty;
        int clickBoton = 0;
        #endregion Variables Globales Para Paginar

        public ListadoVentas()
        {
            InitializeComponent();
            // Almacenamos en el Tag el texto por defecto
            //txtBoxClienteFolio.Tag = "Busca Cliente ó Folio";
            txtBoxClienteFolio.Tag = "En Construcción";

            txtBoxClienteFolio.Text = txtBoxClienteFolio.Tag.ToString();
            
            // Ponemos el foreColor en gris
            txtBoxClienteFolio.ForeColor = Color.Gray;
            
            // Suscribimos el textBox a los eventos (Se puede hacer en el Diseñador)
            //txtBoxClienteFolio_Enter(sender, e)
            //txtBoxClienteFolio.GotFocus += new EventHandler(OnGetFocus);
            txtBoxClienteFolio.Enter += new EventHandler(txtBoxClienteFolio_Enter);
            
            //txtBoxClienteFolio_Leave
            //txtBoxClienteFolio.LostFocus += new EventHandler(OnLostFocus);
            txtBoxClienteFolio.Leave += new EventHandler(txtBoxClienteFolio_Leave);
        }

        private void ListadoVentas_Load(object sender, EventArgs e)
        {
            // Se crea el directorio para almacenar los tickets y otros archivos relacionados con ventas
            Directory.CreateDirectory(@"C:\Archivos PUDVE\Ventas\Tickets");

            Dictionary<string, string> ventas = new Dictionary<string, string>();
            ventas.Add("VP", "Ventas pagadas");
            ventas.Add("VG", "Ventas guardadas");
            ventas.Add("VC", "Ventas canceladas");
            ventas.Add("VCC", "Ventas a crédito");
            ventas.Add("FAC", "Facturas");
            ventas.Add("PRE", "Presupuestos");

            cbTipoVentas.DataSource = ventas.ToArray();
            cbTipoVentas.DisplayMember = "Value";
            cbTipoVentas.ValueMember = "Key";

            cbVentas.SelectedIndex = 0;
            cbTipoVentas.SelectedIndex = 0;

            fechaUltimoCorte = Convert.ToDateTime(mb.UltimaFechaCorte());

            clickBoton = 0;

            CargarDatos();
            actualizar();
            btnUltimaPagina.PerformClick();
        }

        public void OnGetFocus(object sender, EventArgs e)
        {
            // Comprobamos si el texto es el default, y si lo es borramos
            if (txtBoxClienteFolio.Text.Contains(txtBoxClienteFolio.Tag.ToString()))
            {
                txtBoxClienteFolio.Text = "";
            }
            // Ponemos el color en negro
            txtBoxClienteFolio.ForeColor = Color.Black;
        }

        public void OnLostFocus(object sender, EventArgs e)
        {
            // En caso de que no haya texto, añadimos el texto por defecto y ponemos el color en gris
            if (String.IsNullOrWhiteSpace(txtBoxClienteFolio.Text))
            {
                txtBoxClienteFolio.Text = txtBoxClienteFolio.Tag.ToString();
                txtBoxClienteFolio.ForeColor = Color.Gray;
            }
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
            if (clickBoton == 0)
            {
                //var consulta = string.Empty;
                //var buscarClienteFolio = string.Empty;
                //buscarClienteFolio = txtBoxClienteFolio.Text;

                //var fechaInicial = dpFechaInicial.Value.ToString("yyyy-MM-dd");
                //var fechaFinal = dpFechaFinal.Value.ToString("yyyy-MM-dd");
                //var opcion = cbTipoVentas.SelectedValue.ToString();

                //if (buscarClienteFolio.Equals(txtBoxClienteFolio.Tag.ToString().ToUpper()))
                //{
                //    //MessageBox.Show("Test1");
                //    if (busqueda)
                //    {
                //        // Ventas pagadas
                //        if (opcion == "VP") { estado = 1; }
                //        // Ventas guardadas
                //        if (opcion == "VG") { estado = 2; }
                //        // Ventas canceladas
                //        if (opcion == "VC") { estado = 3; }
                //        // Ventas a credito
                //        if (opcion == "VCC") { estado = 4; }
                //        // Facturas
                //        if (opcion == "FAC") { estado = 5; }
                //        // Presupuestos
                //        if (opcion == "PRE") { estado = 6; }

                //        consulta = $"SELECT * FROM Ventas WHERE Status = {estado} AND IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}'";
                //    }
                //    else
                //    {
                //        consulta = $"SELECT * FROM Ventas WHERE Status = {estado} AND IDUsuario = {FormPrincipal.userID} AND FechaOperacion > '{fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss")}'";
                //    }
                //}
                //else if (!buscarClienteFolio.Equals(txtBoxClienteFolio.Tag.ToString().ToUpper()))
                //{
                //    //MessageBox.Show("Test2");
                //}

                var consulta = string.Empty;
                var buscarClienteFolio = string.Empty;
                buscarClienteFolio = txtBoxClienteFolio.Text;

                if (busqueda)
                {
                    var fechaInicial = dpFechaInicial.Value.ToString("yyyy-MM-dd");
                    var fechaFinal = dpFechaFinal.Value.ToString("yyyy-MM-dd");
                    var opcion = cbTipoVentas.SelectedValue.ToString();

                    // Ventas pagadas
                    if (opcion == "VP") { estado = 1; }
                    // Ventas guardadas
                    if (opcion == "VG") { estado = 2; }
                    // Ventas canceladas
                    if (opcion == "VC") { estado = 3; }
                    // Ventas a credito
                    if (opcion == "VCC") { estado = 4; }
                    // Facturas
                    if (opcion == "FAC") { estado = 5; }
                    // Presupuestos
                    if (opcion == "PRE") { estado = 6; }

                    consulta = $"SELECT * FROM Ventas WHERE Status = {estado} AND IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}'";
                }
                else
                {
                    consulta = $"SELECT * FROM Ventas WHERE Status = {estado} AND IDUsuario = {FormPrincipal.userID} AND FechaOperacion > '{fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss")}'";
                }

                FiltroAvanzado = consulta;
                
                p = new Paginar(FiltroAvanzado, DataMemberDGV, maximo_x_pagina);
            }

            DGVListadoVentas.Rows.Clear();

            DataSet datos = p.cargar();
            DataTable dtDatos = datos.Tables[0];

            // Inicializacion de iconos
            Image cancelar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\remove.png");
            Image factura = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-pdf-o.png");
            Image ticket = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\ticket.png");
            Image credito = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\dollar.png");
            Image info = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\info-circle.png");
            Image timbrar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\bell.png");

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

                    string cliente = "Público General";
                    string rfc = "XAXX010101000";

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

                    iva += ivaTmp;
                    subtotal += subtotalTmp;
                    total += totalTmp;

                    row.Cells["ID"].Value = idVenta;
                    row.Cells["Cliente"].Value = cliente;
                    row.Cells["RFC"].Value = rfc;
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
                    if (status == 3)
                    {
                        row.Cells["Cancelar"].Value = sinImagen;
                    }

                    // Ventas a credito
                    if (status != 4)
                    {
                        row.Cells["Abono"].Value = info;
                    }
                }

                AgregarTotales(iva, subtotal, total);
                
                using (DataTable dbTotalesGenerales = cn.CargarDatos(FiltroAvanzado))
                {
                    float ivaTmpGral = 0, subtotalTmpGral = 0, totalTmpGral = 0;
                    foreach (DataRow row in dbTotalesGenerales.Rows)
                    {
                        ivaTmpGral += float.Parse(row["IVA16"].ToString());
                        subtotalTmpGral += float.Parse(row["Subtotal"].ToString());
                        totalTmpGral += float.Parse(row["Total"].ToString());
                    }
                    AgregarTotalesGenerales(ivaTmpGral, subtotalTmpGral, totalTmpGral);
                }

                DGVListadoVentas.FirstDisplayedScrollingRowIndex = DGVListadoVentas.RowCount - 1;
            }
            tipo_venta = estado;
        }
        #endregion

        private void AgregarTotalesGenerales(float ivaGral, float subtotalGral, float totalGral)
        {
            int idFila = DGVListadoVentas.Rows.Add();
            DataGridViewRow fila = DGVListadoVentas.Rows[idFila];
            fila.DefaultCellStyle.NullValue = null;
            fila.DefaultCellStyle.BackColor = Color.FromArgb(255, 207, 53, 20);
            fila.DefaultCellStyle.ForeColor = Color.White;
            fila.DefaultCellStyle.Font = new Font("Arial", 10f);
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
            fila.Cells["Cliente"].Value = "TOTAL";
            fila.Cells["Subtotal"].Value = subtotal.ToString("0.00");
            fila.Cells["IVA"].Value = iva.ToString("0.00");
            fila.Cells["Total"].Value = total.ToString("0.00");
        }

        private void btnBuscarVentas_Click(object sender, EventArgs e)
        {
            CargarDatos(busqueda: true);
        }

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            Ventas venta = new Ventas();

            venta.Disposed += delegate
            {
                AbrirVentanaVenta();
                CargarDatos();
            };

            venta.ShowDialog();
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
            //Facturas
            if (opcion == "FAC") { CargarDatos(5); }
            //Presupuestos
            if (opcion == "PRE") { CargarDatos(6); }
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
                
                if (e.ColumnIndex >= 10)
                {
                    var textoTT = string.Empty;
                    int coordenadaX = 0;

                    DGVListadoVentas.Cursor = Cursors.Hand;

                    if (e.ColumnIndex == 10)
                    {
                        textoTT = "Cancelar";
                        coordenadaX = 60;

                        if (opcion == "VC") { permitir = false; }
                    }

                    if (e.ColumnIndex == 11)
                    {
                        textoTT = "Ver factura";
                        coordenadaX = 70;
                    }

                    if (e.ColumnIndex == 12)
                    {
                        textoTT = "Ver ticket";
                        coordenadaX = 62;
                    }

                    if (e.ColumnIndex == 13)
                    {
                        textoTT = "Abonos";
                        coordenadaX = 54;

                        if (opcion != "VCC") { permitir = false; }
                    }

                    if (e.ColumnIndex == 14)
                    {
                        textoTT = "Timbrar";
                        coordenadaX = 56;
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

                //Cancelar
                if (e.ColumnIndex == 10)
                {
                    var mensaje = MessageBox.Show("¿Estás seguro de cancelar la venta?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (mensaje == DialogResult.Yes)
                    {
                        // Cancelar la venta
                        int resultado = cn.EjecutarConsulta(cs.ActualizarVenta(idVenta, 3, FormPrincipal.userID));

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
                            Utilidades.CrearMarcaDeAgua(idVenta);

                            mensaje = MessageBox.Show("¿Desea devolver el dinero?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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

                            CargarDatos();
                        }
                    }
                }

                //Ver factura
                if (e.ColumnIndex == 11)
                {
                    // Verifica si el PDF ya esta creado

                    string ruta_archivo = @"C:\Archivos PUDVE\Ventas\PDF\VENTA_" + idVenta + ".pdf";

                    if (!File.Exists(ruta_archivo))
                    {
                        MessageBox.Show("La generación del PDF tardará 10 segundos (aproximadamente) en ser visualizado. Un momento por favor...", "", MessageBoxButtons.OK);
                        // Genera PDF
                        ver_factura(idVenta);
                    }

                    // Visualiza PDF

                    string nombre = "VENTA_" + idVenta;

                    Visualizar_notaventa ver_nota = new Visualizar_notaventa(nombre);

                    ver_nota.FormClosed += delegate
                    {
                        ver_nota.Dispose();
                    };

                    ver_nota.ShowDialog();
                }

                //Ver ticket
                if (e.ColumnIndex == 12)
                {
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
                        MessageBox.Show($"El archivo solicitado con nombre '{ticketGenerado}' \nno se encuentra en el sistema.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //Abonos
                if (e.ColumnIndex == 13)
                {
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
                if (e.ColumnIndex == 14)
                {
                    // Se valida que la nota no tenga ya una factura creada
                    int r = Convert.ToInt32(cn.EjecutarSelect($"SELECT Timbrada FROM Ventas WHERE ID={idVenta}", 8));
                    
                    if(r == 1)
                    {
                        var resp = MessageBox.Show("La nota de venta ya tiene una factura creada. La generación de más de una factura para la misma nota queda a responsabilidad de usted. \n\n ¿Desea continuar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if(resp == DialogResult.Yes)
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
                    

                    //Comprobamos que la venta tenga cliente
                    /*var clienteRFC = DGVListadoVentas.Rows[fila].Cells["RFC"].Value.ToString();

                    if (!string.IsNullOrWhiteSpace(clienteRFC) && !clienteRFC.Equals("XAXX010101000"))
                    {
                        InformacionVenta info = new InformacionVenta(idVenta);

                        info.FormClosed += delegate
                        {

                        };

                        info.ShowDialog();
                    }
                    else
                    {
                        var respuesta = MessageBox.Show("Es necesario asignar un cliente a esta venta para poder timbrarla, haga click en Aceptar para seleccionar un cliente", "Mensaje del Sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        if (respuesta == DialogResult.OK)
                        {
                            //Comprobamos si tiene clientes registrados
                            var existenClientes = (bool)cn.EjecutarSelect($"SELECT * FROM Clientes WHERE IDUsuario = {FormPrincipal.userID}");

                            if (existenClientes)
                            {
                                ListaClientes clientes = new ListaClientes(idVenta);

                                clientes.FormClosed += delegate
                                {
                                    CargarDatos();
                                };

                                clientes.ShowDialog();
                            }
                            else
                            {
                                AgregarCliente nuevo = new AgregarCliente(1, 0, idVenta);

                                nuevo.FormClosed += delegate
                                {
                                    CargarDatos();
                                };

                                nuevo.ShowDialog();
                            }
                        }
                    }*/
                }

                DGVListadoVentas.ClearSelection();
            }
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

                CargarDatos();

                recargarDatos = false;

                btnUltimaPagina.PerformClick();
            }

            if (abrirNuevaVenta)
            {
                abrirNuevaVenta = false;
                btnNuevaVenta.PerformClick();
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
            DataTable d_id_productos;
            DataTable d_claves;

            //int sin_cliente = 0;
            //int n_filas = 0;
            int i = 1;
            

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
                        foreach(DataRow r_claves in d_claves.Rows)
                        { 
                            string clave_u = r_claves["UnidadMedida"].ToString();
                            string clave_p = r_claves["ClaveProducto"].ToString();

                            faltantes_productos[i] = new string[6];

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
                        }

                        i++;
                    }
                }
            }


            // Abrir ventana para agregar los datos faltantes para la factura

            Crear_factura crear_factura = new Crear_factura(id_cliente, n_filas, id_venta);

            crear_factura.ShowDialog();
        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            p.primerPagina();
            clickBoton = 1;
            CargarDatos();
            actualizar();
            clickBoton = 0;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            CargarDatos();
            actualizar();
            clickBoton = 0;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            CargarDatos();
            actualizar();
            clickBoton = 0;
        }

        private void btnUltimaPagina_Click(object sender, EventArgs e)
        {
            p.ultimaPagina();
            clickBoton = 1;
            CargarDatos();
            actualizar();
            clickBoton = 0;
        }

        private void linkLblPaginaAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
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
            CargarDatos();
            actualizar();
            clickBoton = 0;
        }

        private void txtBoxClienteFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int) Keys.Enter)
            {
                //if (!txtBoxClienteFolio.Text.Equals(""))
                //{
                //    clickBoton = 0;
                //    busquedaDelUsuario(txtBoxClienteFolio.Text);
                //}
            }
        }

        private void busquedaDelUsuario(string clienteFolio)
        {
            if (cbTipoVentas.Text.Equals("Ventas pagadas"))
            {
                CargarDatos(estado: 1, clienteFolio: clienteFolio);
            }
            else if (cbTipoVentas.Text.Equals("Ventas guardadas"))
            {
                CargarDatos(estado: 2, clienteFolio: clienteFolio);
            }
            else if (cbTipoVentas.Text.Equals("Ventas canceladas"))
            {
                CargarDatos(estado: 3, clienteFolio: clienteFolio);
            }
            else if (cbTipoVentas.Text.Equals("Ventas a crédito"))
            {
                CargarDatos(estado: 4, clienteFolio: clienteFolio);
            }
            else if (cbTipoVentas.Text.Equals("Facturas"))
            {
                CargarDatos(estado: 5, clienteFolio: clienteFolio);
            }
            else if (cbTipoVentas.Text.Equals("Presupuestos"))
            {
                CargarDatos(estado: 6, clienteFolio: clienteFolio);
            }
        }

        private void txtBoxClienteFolio_Enter(object sender, EventArgs e)
        {
            // Comprobamos si el texto es el default, y si lo es borramos
            if (txtBoxClienteFolio.Text.Contains(txtBoxClienteFolio.Tag.ToString().ToUpper()))
            {
                txtBoxClienteFolio.Text = "";
            }
            // Ponemos el color en negro
            txtBoxClienteFolio.ForeColor = Color.Black;
        }

        private void txtBoxClienteFolio_Leave(object sender, EventArgs e)
        {
            // En caso de que no haya texto, añadimos el texto por defecto y ponemos el color en gris
            if (String.IsNullOrWhiteSpace(txtBoxClienteFolio.Text))
            {
                txtBoxClienteFolio.Text = txtBoxClienteFolio.Tag.ToString();
                txtBoxClienteFolio.ForeColor = Color.Gray;
            }
        }


        private void ver_factura(int id_venta)
        {
            decimal suma_importe_concep = 0;
            decimal suma_importe_impuest = 0;
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

            string tipo_iva = "";

            if (Convert.ToDecimal(r_venta["IVA16"]) > 0) {  tipo_iva = "IVA16";  }
            if (Convert.ToDecimal(r_venta["IVA8"]) > 0) {  tipo_iva = "IVA8";  }

            // Consulta tabla DetallesVenta

            DataTable d_detallesventa = cn.CargarDatos(cs.consulta_dventa(2, id_venta));
            DataRow r_detallesventa = d_detallesventa.Rows[0];

            int id_cliente = Convert.ToInt32(r_detallesventa["IDCliente"]);
            string forma_pago = "";

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



            ComprobanteVenta comprobanteventa = new ComprobanteVenta();


            // Datos del usuario

            DataTable d_usuario = cn.CargarDatos(cs.cargar_datos_venta_xml(2, 0, id_usuario));
            DataRow r_usuario = d_usuario.Rows[0];

            string lugar_expedicion = r_usuario["Estado"].ToString();

            ComprobanteEmisorVenta emisor_v = new ComprobanteEmisorVenta();

            emisor_v.Nombre = r_usuario["RazonSocial"].ToString();
            emisor_v.Rfc= r_usuario["RFC"].ToString();
            emisor_v.RegimenFiscal= r_usuario["Regimen"].ToString();
            emisor_v.Estado = r_usuario["Estado"].ToString();
            emisor_v.Municipio = r_usuario["Municipio"].ToString();
            emisor_v.CP = r_usuario["CodigoPostal"].ToString();
            emisor_v.Colonia = r_usuario["Colonia"].ToString();
            emisor_v.Calle = r_usuario["Calle"].ToString();
            emisor_v.Numext = r_usuario["NoExterior"].ToString();
            emisor_v.Numint = r_usuario["NoInterior"].ToString();
            emisor_v.Correo = r_usuario["Email"].ToString();
            emisor_v.Telefono = r_usuario["Telefono"].ToString();

            comprobanteventa.Emisor = emisor_v;


            // Datos del cliente

            DataTable d_cliente = cn.CargarDatos(cs.cargar_datos_venta_xml(3, id_cliente, 0));

            if (d_cliente.Rows.Count > 0)
            {
                DataRow r_cliente = d_cliente.Rows[0];
                
                ComprobanteReceptorVenta receptor_v = new ComprobanteReceptorVenta();

                receptor_v.Nombre = r_cliente["RazonSocial"].ToString();
                receptor_v.Rfc = r_cliente["RFC"].ToString();
                receptor_v.Pais = r_cliente["Pais"].ToString();
                receptor_v.Estado = r_cliente["Estado"].ToString();
                receptor_v.Municipio = r_cliente["Municipio"].ToString();
                receptor_v.Localidad = r_cliente["Localidad"].ToString();
                receptor_v.CP = r_cliente["CodigoPostal"].ToString();
                receptor_v.Colonia = r_cliente["Colonia"].ToString();
                receptor_v.Calle = r_cliente["Calle"].ToString();
                receptor_v.Numext = r_cliente["NoExterior"].ToString();
                receptor_v.Numint = r_cliente["NoInterior"].ToString();
                receptor_v.Correo = r_cliente["Email"].ToString();
                receptor_v.Telefono = r_cliente["Telefono"].ToString();

                comprobanteventa.Receptor = receptor_v;
            }
            


            // Datos del producto

            List<ComprobanteConceptoVenta> listaconcepto_v = new List<ComprobanteConceptoVenta>();

            DataTable d_prodventa = cn.CargarDatos(cs.cargar_datos_venta_xml(4, id_venta, 0));

            if(d_prodventa.Rows.Count > 0)
            {
                foreach (DataRow r_prodventa in d_prodventa.Rows)
                {
                    ComprobanteConceptoVenta concepto_v = new ComprobanteConceptoVenta();

                    concepto_v.Cantidad = Convert.ToDecimal(r_prodventa["Cantidad"]);
                    concepto_v.Descripcion = r_prodventa["Nombre"].ToString();
                    concepto_v.ValorUnitario = Convert.ToDecimal(r_prodventa["Precio"]);

                    decimal importe_v = Convert.ToDecimal(r_prodventa["Cantidad"]) * Convert.ToDecimal(r_prodventa["Precio"]);
                    decimal tasa_cuota = 0.000000m;

                    if (tipo_iva == "IVA16")
                    {
                        importe_v = importe_v / 1.16m;
                        tasa_cuota = 0.160000m;
                    }
                    if (tipo_iva == "IVA8")
                    {
                        importe_v = importe_v / 1.08m;
                        tasa_cuota = 0.080000m;
                    }

                    concepto_v.Importe = importe_v;

                    suma_importe_concep += importe_v;


                    // Agrega impuestos

                    List<ComprobanteConceptoImpuestosTrasladoVenta> list_concepto_impuestos_traslados_v = new List<ComprobanteConceptoImpuestosTrasladoVenta>();
                    ComprobanteConceptoImpuestosTrasladoVenta concepto_traslado_v = new ComprobanteConceptoImpuestosTrasladoVenta();

                    concepto_traslado_v.Base = importe_v;
                    concepto_traslado_v.Impuesto = "002";
                    concepto_traslado_v.TipoFactor = "Tasa";
                    concepto_traslado_v.TasaOCuota = tasa_cuota;

                    decimal importe_imp = importe_v * tasa_cuota;
                    concepto_traslado_v.Importe = importe_imp;

                    suma_importe_impuest += importe_imp;

                    list_concepto_impuestos_traslados_v.Add(concepto_traslado_v);


                    // Guarda en la lista el tipo de impuesto
                    /*
                    string cadena = "002-Tasa" + "-" + tasa_cuota;

                    // Busca si la cadena existe en la lista
                    var indice = list_porprod_impuestos_trasladados.IndexOf(cadena);

                    // Si la cadena existe aumenta el importe del impuesto, de lo contrario la agrega como nueva
                    if (indice >= 0)
                    {
                        indice = indice + 1;
                        decimal monto_actual = Convert.ToDecimal(list_porprod_impuestos_trasladados[indice]);
                        decimal monto_nuevo = monto_actual + importe_imp;

                        list_porprod_impuestos_trasladados.RemoveAt(indice);
                        list_porprod_impuestos_trasladados.Insert(indice, Convert.ToString(monto_nuevo));
                    }
                    else
                    {
                        list_porprod_impuestos_trasladados.Add(cadena);
                        list_porprod_impuestos_trasladados.Add(importe_imp.ToString());
                    }*/


                    concepto_v.Impuestos = new ComprobanteConceptoImpuestosVenta();
                    concepto_v.Impuestos.Traslados = list_concepto_impuestos_traslados_v.ToArray();


                    listaconcepto_v.Add(concepto_v);
                }

                comprobanteventa.Conceptos = listaconcepto_v.ToArray();
            }


            // Datos generales de la venta 

            decimal total_general = suma_importe_concep + suma_importe_impuest;

            comprobanteventa.Serie = serie;
            comprobanteventa.Folio = folio;
            comprobanteventa.Fecha = fecha.ToString("yyyy-MM-dd HH:mm:ss");
            comprobanteventa.FormaPago = forma_pago;
            comprobanteventa.SubTotal = suma_importe_concep;
            comprobanteventa.Total = total_general;
            comprobanteventa.LugarExpedicion = lugar_expedicion;



            // .....................................................................
            // .    Inicia con la generación de la plantilla y conversión a PDF    .
            // .....................................................................


            // Nombre que tendrá el pdf de la venta
            string nombre_venta = "VENTA_" + id_venta;
            // Verifica si tiene creado el directorio
            string carpeta_venta = @"C:\Archivos PUDVE\Ventas\PDF\";

            if (!Directory.Exists(carpeta_venta))
            {
                Directory.CreateDirectory(carpeta_venta);
            }
            

            string origen_pdf_temp = nombre_venta + ".pdf";
            string destino_pdf = @"C:\Archivos PUDVE\Ventas\PDF\" + nombre_venta + ".pdf";

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
    }
}
