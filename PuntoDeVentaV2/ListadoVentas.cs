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
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        public string rutaLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private string ticketGenerado = string.Empty;
        private string rutaTicketGenerado = string.Empty;

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

            CargarDatos();
        }

        #region Método para cargar los datos en el DataGridView
        public void CargarDatos(int estado = 1)
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT * FROM Ventas WHERE Status = '{estado}' AND IDUsuario = '{FormPrincipal.userID}'", sql_con);
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
                    MessageBox.Show("Cancelar");
                }

                //Ver factura
                if (e.ColumnIndex == 11)
                {
                    MessageBox.Show("Factura");
                }

                //Ver ticket
                if (e.ColumnIndex == 12)
                {
                    ticketGenerado = $"ticket_venta_{idVenta}.pdf";
                    rutaTicketGenerado = @"C:\Archivos PUDVE\Ventas\Tickets\" + ticketGenerado;

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

        private void TTMensaje_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void ListadoVentas_Paint(object sender, PaintEventArgs e)
        {
            btnNuevaVenta.PerformClick();
        }
    }
}
