using iTextSharp.text.pdf;
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

        public ListadoVentas()
        {
            InitializeComponent();
        }

        private void ListadoVentas_Load(object sender, EventArgs e)
        {
            //Se crea el directorio para almacenar los tickets y otros archivos relacionados con ventas
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

            CargarDatos();
        }

        #region Método para cargar los datos en el DataGridView
        public void CargarDatos(int estado = 1)
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                sql_con = new SQLiteConnection("Data source=//" + Properties.Settings.Default.Hosting + @"\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }
            else
            {
                sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }

            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT * FROM Ventas WHERE Status = {estado} AND IDUsuario = {FormPrincipal.userID} AND FechaOperacion > '{fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss")}'", sql_con);
            dr = sql_cmd.ExecuteReader();

            DGVListadoVentas.Rows.Clear();

            //Inicializacion de iconos
            Image cancelar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\remove.png");
            Image factura = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-pdf-o.png");
            Image ticket = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\ticket.png");
            Image credito = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\dollar.png");
            Image info = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\info-circle.png");
            Image timbrar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\bell.png");

            Bitmap sinImagen = new Bitmap(1, 1);
            sinImagen.SetPixel(0, 0, Color.White);

            while (dr.Read())
            {

                int idVenta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID")));
                int status = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Status")));

                string cliente = "Público General";
                string rfc = "XAXX010101000";

                //Obtener detalle de venta y datos del cliente
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


                //Obtener el cliente de la venta guardada
                if (estado == 2)
                {
                    var idCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDCliente")));

                    if (idCliente > 0)
                    {
                        var infoCliente = mb.ObtenerDatosCliente(idCliente, FormPrincipal.userID);
                        cliente = infoCliente[0];
                        rfc = infoCliente[1];
                    }
                }

                int rowId = DGVListadoVentas.Rows.Add();

                DataGridViewRow row = DGVListadoVentas.Rows[rowId];

                row.Cells["ID"].Value = idVenta;
                row.Cells["Cliente"].Value = cliente;
                row.Cells["RFC"].Value = rfc;
                row.Cells["Subtotal"].Value = dr.GetValue(dr.GetOrdinal("Subtotal"));
                row.Cells["IVA"].Value = dr.GetValue(dr.GetOrdinal("IVA16"));
                row.Cells["Total"].Value = dr.GetValue(dr.GetOrdinal("Total"));
                row.Cells["Folio"].Value = dr.GetValue(dr.GetOrdinal("Folio"));
                row.Cells["Serie"].Value = dr.GetValue(dr.GetOrdinal("Serie"));
                //row.Cells["Pago"].Value = dr.GetValue(dr.GetOrdinal("MetodoPago"));
                //row.Cells["Empleado"].Value = dr.GetValue(dr.GetOrdinal("IDEmpleado"));
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");

                row.Cells["Cancelar"].Value = cancelar;
                row.Cells["Factura"].Value = factura;
                row.Cells["Ticket"].Value = ticket;
                row.Cells["Abono"].Value = credito;
                row.Cells["Timbrar"].Value = timbrar;

                //Ventas canceladas
                if (status == 3)
                {
                    row.Cells["Cancelar"].Value = sinImagen;
                }

                //Ventas a credito
                if (status != 4)
                {
                    row.Cells["Abono"].Value = info;
                }
            }

            dr.Close();
            sql_con.Close();
        }
        #endregion

        private void btnBuscarVentas_Click(object sender, EventArgs e)
        {
            string fechaInicial = dpFechaInicial.Value.ToString("yyyy-MM-dd");
            string fechaFinal   = dpFechaFinal.Value.ToString("yyyy-MM-dd");

            MessageBox.Show(fechaInicial + " " + fechaFinal);
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
            if (e.RowIndex >= 0)
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

                    VerToolTip(textoTT, cellRect.X, coordenadaX, cellRect.Y, permitir);

                    textoTT = string.Empty;
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
            if (e.RowIndex >= 0)
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
                            CrearMarcaDeAgua(idVenta);

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
                    MessageBox.Show("Factura");
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
                    //Comprobamos que la venta tenga cliente
                    var clienteRFC = DGVListadoVentas.Rows[fila].Cells["RFC"].Value.ToString();

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
                    }
                }

                DGVListadoVentas.ClearSelection();
            }
        }

        private void CrearMarcaDeAgua(int idVenta)
        {
            var servidor = Properties.Settings.Default.Hosting;
            var archivoCopia = string.Empty;
            var archivoPDF = string.Empty;
            var nuevoPDF = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                archivoCopia = $@"\\{servidor}\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}_tmp.pdf";
                archivoPDF = $@"\\{servidor}\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}.pdf";
                nuevoPDF = archivoPDF;
                // Renombramos el archivo PDF
                File.Move(archivoPDF, archivoCopia);
            }
            else
            {
                archivoCopia = $@"C:\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}_tmp.pdf";
                archivoPDF = $@"C:\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}.pdf";
                nuevoPDF = archivoPDF;
                // Renombramos el archivo PDF
                File.Move(archivoPDF, archivoCopia);
            }


            using (PdfReader reader = new PdfReader(archivoCopia))
            {
                FileStream fs = new FileStream(nuevoPDF, FileMode.Create, FileAccess.Write, FileShare.None);

                using (PdfStamper stamper = new PdfStamper(reader, fs))
                {
                    int numeroPaginas = reader.NumberOfPages;

                    PdfLayer layer = new PdfLayer("WatermarkLayer", stamper.Writer);

                    for (int i = 1; i <= numeroPaginas; i++)
                    {
                        iTextSharp.text.Rectangle rec = reader.GetPageSize(i);
                        PdfContentByte cb = stamper.GetUnderContent(i);

                        cb.BeginLayer(layer);
                        cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 40);

                        PdfGState gstate = new PdfGState();
                        gstate.FillOpacity = 0.25f;
                        cb.SetGState(gstate);

                        cb.SetColorFill(iTextSharp.text.BaseColor.RED);
                        cb.BeginText();
                        cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "CANCELADA", rec.Width / 2, rec.Height / 2, 45f);
                        cb.EndText();
                        cb.EndLayer();
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(archivoCopia))
            {
                if (File.Exists(archivoCopia))
                {
                    File.Delete(archivoCopia);
                }
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
            }

            if (abrirNuevaVenta)
            {
                btnNuevaVenta.PerformClick();

                abrirNuevaVenta = false;
            }
        }

        //Se agrego para que no se abra la ventana nueva venta al cambiar el tamaño del form
        private void ListadoVentas_Resize(object sender, EventArgs e)
        {
            recargarDatos = false;
            abrirNuevaVenta = false;
        }
    }
}
