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
    public partial class Anticipos : Form
    {
        //Status 1 = Por usar
        //Status 2 = Deshabilitado
        //Status 3 = Usado
        //Status 4 = Devuelto = Este no se muestra como opcion
        //Status 5 = Parciales

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        public static bool recargarDatos = false;

        private string ticketGenerado = string.Empty;
        private string rutaTicketGenerado = string.Empty;

        bool conBusqueda = false;

        // Permisos para botones
        int opcion1 = 1; // Generar ticket
        int opcion2 = 1; // Habilitar/deshabilitar
        int opcion3 = 1; // Devolver anticipo
        int opcion4 = 1; // Boton buscar
        int opcion5 = 1; // Nuevo anticipo



        string mensajeParaMostrar = string.Empty;
        int maximo_x_pagina = 14;
        int opcion;
        private Paginar p;
        int clickBoton = 0;
        string DataMemberDGV = "Anticipos";
        public static bool SeCancelo = false;
                
        IEnumerable<AgregarAnticipo> FormAnticipo = Application.OpenForms.OfType<AgregarAnticipo>();

        public Anticipos()
        {
            InitializeComponent();

        }

        private void Anticipos_Load(object sender, EventArgs e)
        {
            cbAnticipos.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            //Se crea el directorio para almacenar los tickets y otros archivos relacionados con ventas
            Directory.CreateDirectory(@"C:\Archivos PUDVE\Anticipos\Tickets");
            DateTime date = DateTime.Now;
            DateTime PrimerDia;
            if (!date.Month.Equals(1))
            {
                PrimerDia = new DateTime(date.Year, date.Month - 1, 1);
            }
            else
            {
                PrimerDia = new DateTime(date.Year-1, date.Month + 11, 1);
            }
            dpFechaInicial.Value = PrimerDia;
            dpFechaFinal.Value = DateTime.Now;
            cbAnticipos.DropDownStyle = ComboBoxStyle.DropDownList;
            CargarDatos();

            if (FormPrincipal.id_empleado > 0)
            {
                var permisos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Anticipos");

                opcion1 = permisos[0];
                opcion2 = permisos[1];
                opcion3 = permisos[2];
                opcion4 = permisos[3];
                opcion5 = permisos[4];
            }

            txtBuscarAnticipo.Focus();
           
        }

        private void CargarDatos(int estado = 1)
        {
            MySqlConnection sql_con;
            MySqlCommand sql_cmd;
            MySqlDataReader dr;

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

            var consulta = string.Empty;

            //if (estado.Equals(4))
            //{
            //    estado = 5;
            //}

            //Normal
            //if (tipo == 0)
            //{
            //    consulta = $"SELECT * FROM Anticipos WHERE IDUsuario = {FormPrincipal.userID} AND Status = {estado}"; //AND Status != 4
            //}

            //Con fechas de busqueda
            //if (tipo == 1)
            //{
            var fechaInicio = dpFechaInicial.Text;

            var fechaFinal = dpFechaFinal.Text;

            if (string.IsNullOrEmpty(txtBuscarAnticipo.Text))//Busqueda sin Cliente/Empleado
            {
                consulta = $"SELECT * FROM Anticipos WHERE IDUsuario = {FormPrincipal.userID} AND Status = {estado} AND DATE(Fecha) BETWEEN '{fechaInicio}' AND '{fechaFinal}'"; //AND Status != 4
                conBusqueda = false;
            }
            else//Busqueda con Cliente/Empleado
            {
                //var emp = consultaBuscarEmpledo(txtBuscarAnticipo.Text);
                //var client = consultaBuscarCliente(); 

                consulta = $"SELECT * FROM Anticipos WHERE IDUsuario = {FormPrincipal.userID} AND `Status` = {estado} AND (Concepto LIKE '%{txtBuscarAnticipo.Text}%' OR Cliente LIKE '%{txtBuscarAnticipo.Text}%'  OR ID LIKE '%{txtBuscarAnticipo.Text}%')AND DATE(Fecha) BETWEEN '{fechaInicio}' AND '{fechaFinal}'"; //AND Status != 4

                conBusqueda = true;
            }

            if (DGVAnticipos.Rows.Count.Equals(0) || clickBoton.Equals(0))
            {
                p = new Paginar(consulta, DataMemberDGV, maximo_x_pagina);
            }
            //DGVAnticipos.Rows.Clear();

            DataSet datos = p.cargar();
            DataTable dtDatos = datos.Tables[0];

            sql_cmd = new MySqlCommand(consulta, sql_con);

            dr = sql_cmd.ExecuteReader();
            if (dr.HasRows)
            {
                DGVAnticipos.Rows.Clear();
            }
            else
            {
                DGVAnticipos.Rows.Clear();
            }

            if (dr.HasRows)
            {
                int rows = 0;
                foreach (var item in dtDatos.Rows)
                {
                    Image ticket = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\ticket.png");
                    Image deshabilitar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\ban.png");
                    Image habilitar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\check.png");
                    Image devolver = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\reply.png");
                    Image info = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\info-circle.png");
                    Image historialAnticipos = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\clipboard_invoice16px.png");
                    Bitmap sinImagen = new Bitmap(1, 1);
                    sinImagen.SetPixel(0, 0, Color.White);

                    int rowId = DGVAnticipos.Rows.Add();

                    DataGridViewRow row = DGVAnticipos.Rows[rowId];
                    var empleado = string.Empty;
                    var empAdm = string.Empty;
                    empleado = dtDatos.Rows[rows]["IDEmpleado"].ToString();
                    if (!empleado.Equals("0"))
                    {
                        empAdm = "Empleado";
                    }
                    else
                    {
                        empAdm = "Administrador";
                    }
                    row.Cells["ID"].Value = dtDatos.Rows[rows]["ID"].ToString();
                    row.Cells["Concepto"].Value = dtDatos.Rows[rows]["Concepto"].ToString();
                    row.Cells["Importe"].Value = dtDatos.Rows[rows]["Importe"].ToString();
                    row.Cells["Cliente"].Value = dtDatos.Rows[rows]["Cliente"].ToString();
                    row.Cells["Empleado"].Value = empAdm;
                    row.Cells["Fecha"].Value = Convert.ToDateTime(dtDatos.Rows[rows]["Fecha"]);
                    row.Cells["IDVenta"].Value = dtDatos.Rows[rows]["IDVenta"].ToString();
                    row.Cells["FormaPago"].Value = dtDatos.Rows[rows]["FormaPago"].ToString();
                    row.Cells["Ticket"].Value = ticket;

                    var status = Convert.ToInt32(dtDatos.Rows[rows]["Status"]);
                    rows++;
                    if (status == 1)
                    {
                        row.Cells["Status"].Value = deshabilitar;
                        row.Cells["Devolver"].Value = devolver;
                        row.Cells["Info"].Value = sinImagen;
                    }
                    else if (status == 2)
                    {
                        row.Cells["Status"].Value = habilitar;
                        row.Cells["Devolver"].Value = sinImagen;
                        row.Cells["Info"].Value = sinImagen;
                    }
                    else
                    {
                        row.Cells["Ticket"].Value = historialAnticipos;
                        row.Cells["Status"].Value = sinImagen;
                        row.Cells["Devolver"].Value = sinImagen;
                        row.Cells["Info"].Value = info;
                    }
                }
            }
            else
            {
                if (conBusqueda)
                {
                    MessageBox.Show("No se encontraron resultados.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBuscarAnticipo.Text = string.Empty;
                    txtBuscarAnticipo.Focus();
                }
            }
            DGVAnticipos.ClearSelection();
            ActualizarPaginador();

            dr.Close();
            sql_con.Close();
        }

        //private string consultaBuscarCliente()
        //{
        //    string result = string.Empty;

        //    return result;
        //}

        private string consultaBuscarEmpledo(string nombreEmpleado)
        {
            string result = string.Empty;

            var query = cn.CargarDatos($"SELECT ID FROM Empleados WHERE IDUsuario = '{FormPrincipal.userID}' AND Nombre = '{nombreEmpleado}'");

            if (!query.Rows.Count.Equals(0))
            {
                result = query.Rows[0]["ID"].ToString();
            }

            return result;
        }

        private void btnNuevoAnticipo_Click(object sender, EventArgs e)
        {
            if (opcion5 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (FormAnticipo.Count() == 1)
            {
                if (FormAnticipo.First().WindowState == FormWindowState.Normal)
                {
                    FormAnticipo.First().BringToFront();
                }

                if (FormAnticipo.First().WindowState == FormWindowState.Minimized)
                {
                    FormAnticipo.First().WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                AgregarAnticipo anticipo = new AgregarAnticipo();

                anticipo.FormClosed += delegate
                {
                    CargarDatos(1);
                };

                anticipo.Show();
            }  
        }

        private void DGVAnticipos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Rectangle cellRect = DGVAnticipos.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                if (e.ColumnIndex >= 6)
                {
                    var textoTT = string.Empty;
                    int coordenadaX = 0;

                    DGVAnticipos.Cursor = Cursors.Hand;

                    if (e.ColumnIndex == 6) { textoTT = "Ver ticket"; coordenadaX = 62; }

                    if (e.ColumnIndex == 7) {

                        if (cbAnticipos.SelectedIndex + 1 == 1)
                        {
                            textoTT = "Deshabilitar";
                            coordenadaX = 76;
                        }
                        else if (cbAnticipos.SelectedIndex + 1 == 2)
                        {
                            textoTT = "Habilitar";
                            coordenadaX = 59;
                        }
                    }

                    if (e.ColumnIndex == 8)
                    {
                        if (cbAnticipos.SelectedIndex == 0)
                        {
                            textoTT = "Devolver";
                            coordenadaX = 59;
                        }
                    }

                    if (e.ColumnIndex == 9)
                    {
                        if (cbAnticipos.SelectedIndex == 2)
                        {
                            textoTT = "Ver detalles";
                            coordenadaX = 72;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(textoTT))
                    {
                        TTMensaje.Show(textoTT, this, DGVAnticipos.Location.X + cellRect.X - coordenadaX, DGVAnticipos.Location.Y + cellRect.Y, 1500);

                        textoTT = string.Empty;
                    }
                }
                else
                {
                    DGVAnticipos.Cursor = Cursors.Default;
                }
            }
        }

        private void cbAnticipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            opcion = cb.SelectedIndex;

            if (opcion >= 0)
            {
                //if ((opcion + 1) == 4)
                //    opcion = opcion + 1;

                CargarDatos(opcion +1);
            }
            btnActualizarMaximoProductos.PerformClick();
        }

        private void DGVAnticipos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var fila = e.RowIndex;
            var indice = cbAnticipos.SelectedIndex;

            if (fila >= 0)
            {
                var idAnticipo = Convert.ToInt32(DGVAnticipos.Rows[fila].Cells["ID"].Value);
                var fecha = DGVAnticipos.Rows[fila].Cells["Fecha"].Value.ToString();
                var importe = float.Parse(DGVAnticipos.Rows[fila].Cells["Importe"].Value.ToString());
                var idVenta = Convert.ToInt32(DGVAnticipos.Rows[fila].Cells["IDVenta"].Value);
                var formaPago = DGVAnticipos.Rows[fila].Cells["FormaPago"].Value.ToString();
                VisualizadorTicketAnticipo idanticipoVer = new VisualizadorTicketAnticipo();
                idanticipoVer.idAnticipoViz = idAnticipo;

                // Generar ticket
                if (e.ColumnIndex == 6)
                {
                    if (indice.Equals(0) || indice.Equals(1) || indice.Equals(3))
                    {
                        idanticipoVer.anticipoSinHistorial = 1;
                        idanticipoVer.ShowDialog();
                    }
                    else
                    {
                        idanticipoVer.anticipoSinHistorial = 0;

                        var datosHistorial = cn.CargarDatos($"SELECT vent.ID AS 'ID Venta', IF(vent.IDEmpleado!=0,'Empleado', 'Administrador') AS 'Empleado', ant.Concepto, ant.Cliente, ant.Comentarios, ant.ImporteOriginal AS 'Total Recibido', ((vent.Subtotal+vent.IVA16+vent.IVA8)) AS 'Anticipo Aplicado', vent.anticipo - (vent.Subtotal+vent.IVA16+vent.IVA8 - vent.Total) AS 'Saldo Restante', vent.FechaOperacion AS 'Fecha Operacion' FROM anticipos AS ant INNER JOIN ventas AS vent ON (Vent.IDAnticipo = ant.ID ) WHERE vent.IDAnticipo = '{idAnticipo}' ORDER BY vent.ID DESC");

                        HistorialAnticipos historialAnticipo = new HistorialAnticipos();
                        historialAnticipo.datosHistoria = datosHistorial;
                        historialAnticipo.ShowDialog();

                    }


                    //if (opcion1 == 0)
                    //{
                    //    Utilidades.MensajePermiso();
                    //    return;
                    //}

                    //if (!Utilidades.AdobeReaderInstalado())
                    //{
                    //    Utilidades.MensajeAdobeReader();
                    //    return;
                    //}

                    //rutaTicketGenerado = @"C:\Archivos PUDVE\Anticipos\Tickets\ticket_anticipo_" + idAnticipo + ".pdf";
                    //ticketGenerado = $"ticket_anticipo_{idAnticipo}.pdf";

                    //if (File.Exists(rutaTicketGenerado))
                    //{
                    //    VisualizadorTickets vt = new VisualizadorTickets(ticketGenerado, rutaTicketGenerado);

                    //    vt.FormClosed += delegate
                    //    {
                    //        vt.Dispose();

                    //        rutaTicketGenerado = string.Empty;
                    //        ticketGenerado = string.Empty;
                    //    };

                    //    vt.ShowDialog();
                    //}
                    //else
                    //{
                    //    MessageBox.Show($"El archivo solicitado con nombre '{ticketGenerado}' \nno se encuentra en el sistema.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }

                // Habilitar/Deshabilitar
                if (e.ColumnIndex == 7)
                {
                    if (opcion2 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    if (indice < 2)
                    {
                        // Deshabilitar
                        if (indice == 0)
                        {
                            var idUltimoCorteDeCaja = 0;
                            var fechaUltimoCorteDeCaja = string.Empty;
                            var EfectivoEnCaja = 0m;
                            var TarjetaEnCaja = 0m;
                            var ValesEnCaja = 0m;
                            var ChequeEnCaja = 0m;
                            var TransferenciaEnCaja = 0m;
                            var CreditoEnCaja = 0m;
                            var AnticipoEnCaja = 0m;

                            using (DataTable dtSaldosInicialesDeCaja = cn.CargarDatos(cs.CargarSaldoInicialSinAbrirCaja(FormPrincipal.userID, FormPrincipal.id_empleado)))
                            {
                                if (!dtSaldosInicialesDeCaja.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow item in dtSaldosInicialesDeCaja.Rows)
                                    {
                                        idUltimoCorteDeCaja = Convert.ToInt32(item["IDCaja"].ToString());
                                        var fechaUltimoCorte = Convert.ToDateTime(item["Fecha"].ToString());
                                        fechaUltimoCorteDeCaja = fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss");
                                        EfectivoEnCaja += (decimal)Convert.ToDouble(item["Efectivo"].ToString());
                                        TarjetaEnCaja += (decimal)Convert.ToDouble(item["Tarjeta"].ToString());
                                        ValesEnCaja += (decimal)Convert.ToDouble(item["Vales"].ToString());
                                        ChequeEnCaja += (decimal)Convert.ToDouble(item["Cheque"].ToString());
                                        TransferenciaEnCaja += (decimal)Convert.ToDouble(item["Transferencia"].ToString());
                                        CreditoEnCaja += (decimal)Convert.ToDouble(item["Credito"].ToString());
                                        AnticipoEnCaja += (decimal)Convert.ToDouble(item["Anticipo"].ToString());
                                    }

                                    using (DataTable dtSaldosInicialVentasDepostos = cn.CargarDatos(cs.SaldoVentasDepositos(FormPrincipal.userID, FormPrincipal.id_empleado, idUltimoCorteDeCaja)))
                                    {
                                        if (!dtSaldosInicialVentasDepostos.Rows.Count.Equals(0))
                                        {
                                            foreach (DataRow item in dtSaldosInicialVentasDepostos.Rows)
                                            {
                                                EfectivoEnCaja += (decimal)Convert.ToDouble(item["Efectivo"].ToString());
                                                TarjetaEnCaja += (decimal)Convert.ToDouble(item["Tarjeta"].ToString());
                                                ValesEnCaja += (decimal)Convert.ToDouble(item["Vales"].ToString());
                                                ChequeEnCaja += (decimal)Convert.ToDouble(item["Cheque"].ToString());
                                                TransferenciaEnCaja += (decimal)Convert.ToDouble(item["Transferencia"].ToString());
                                            }
                                        }
                                    }

                                    using (DataTable dtSaldoInicialRetiros = cn.CargarDatos(cs.SaldoInicialRetiros(FormPrincipal.userID, FormPrincipal.id_empleado, idUltimoCorteDeCaja)))
                                    {
                                        if (!dtSaldoInicialRetiros.Rows.Count.Equals(0))
                                        {
                                            foreach (DataRow item in dtSaldoInicialRetiros.Rows)
                                            {
                                                EfectivoEnCaja -= (decimal)Convert.ToDouble(item["Efectivo"].ToString());
                                                TarjetaEnCaja -= (decimal)Convert.ToDouble(item["Tarjeta"].ToString());
                                                ValesEnCaja -= (decimal)Convert.ToDouble(item["Vales"].ToString());
                                                ChequeEnCaja -= (decimal)Convert.ToDouble(item["Cheque"].ToString());
                                                TransferenciaEnCaja -= (decimal)Convert.ToDouble(item["Transferencia"].ToString());
                                            }
                                        }
                                    }
                                }
                            }

                            if (formaPago == "01" && importe > (float)EfectivoEnCaja || formaPago == "04" && importe > (float)TarjetaEnCaja || formaPago == "08" && importe > (float)ValesEnCaja || formaPago == "02" && importe > (float)ChequeEnCaja || formaPago == "03" && importe > (float)TransferenciaEnCaja)
                            {
                                MessageBox.Show("Saldo insuficiente para deshabilitar el anticipo", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                cn.EjecutarConsulta(cs.CambiarStatusAnticipo(2, idAnticipo, FormPrincipal.userID));
                                CajaDeshabilitar(formaPago, importe);
                                CargarDatos(cbAnticipos.SelectedIndex + 1);

                            }

                        }

                        // Habilitar
                        if (indice == 1)
                        {
                            //cn.EjecutarConsulta(cs.CambiarStatusAnticipo(1, idAnticipo, FormPrincipal.userID));

                            DevolverAnticipo da = new DevolverAnticipo(idAnticipo, importe, 2);

                            da.FormClosed += delegate
                            {
                                CargarDatos(cbAnticipos.SelectedIndex + 1);
                            };

                            da.ShowDialog();
                        }
                    }
                }

                // Devolver anticipo
                if (e.ColumnIndex == 8)
                {
                    if (opcion3 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    if (indice == 0)
                    {
                        var respuesta = MessageBox.Show("¿Está seguro de devolver el Anticipo?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (respuesta == DialogResult.Yes)
                        {
                            DevolverAnticipo da = new DevolverAnticipo(idAnticipo, importe);

                            da.FormClosed += delegate
                            {
                                CargarDatos(cbAnticipos.SelectedIndex + 1);
                            };

                            da.ShowDialog();
                        }
                        if (SeCancelo.Equals(true))
                        {
                            idanticipoVer.idAnticipoViz = idAnticipo;
                            idanticipoVer.anticipoSinHistorial = 1;
                            idanticipoVer.Show();
                        }
                    }
                }

                if (e.ColumnIndex == 9)
                {
                    if (indice == 2)
                    {
                        var infoVenta = cn.BuscarVentaGuardada(idVenta, FormPrincipal.userID);
                        var mensaje = $"ID de venta: {idVenta}\n\nTotal de venta: ${infoVenta[2]}\n\nFolio: {infoVenta[5]} Serie: {infoVenta[6]}\n\nFecha: {infoVenta[7]}";

                        MessageBox.Show(mensaje, "Detalles de Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            DGVAnticipos.Cursor = Cursors.Default;
            DGVAnticipos.ClearSelection();
        }

        private void btnBuscarAnticipos_Click(object sender, EventArgs e)
        {
            if (opcion4 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }
            clickBoton = 0;
            var status = cbAnticipos.SelectedIndex;

            CargarDatos(status + 1);
        }

        private void TTMensaje_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void Anticipos_Paint(object sender, PaintEventArgs e)
        {
            if (recargarDatos)
            {
                CargarDatos(cbAnticipos.SelectedIndex + 1);
                recargarDatos = false;
            }
            cbAnticipos.SelectedIndex = 0;
        }

        private void Anticipos_Resize(object sender, EventArgs e)
        {
            recargarDatos = false;
        }

        private void CajaDeshabilitar(string formaPago, float importe)
        {
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var efectivo = "0";
            var cheque = "0";
            var transferencia = "0";
            var tarjeta = "0";
            var vales = "0";
            var credito = "0";

            //Operacion para afectar la Caja
            if (formaPago == "01") { efectivo = importe.ToString(); }
            if (formaPago == "02") { cheque = importe.ToString(); }
            if (formaPago == "03") { transferencia = importe.ToString(); }
            if (formaPago == "04") { tarjeta = importe.ToString(); }
            if (formaPago == "08") { vales = importe.ToString(); }

            var cantidad = importe;

            string[] datos = new string[] {
                "retiro", cantidad.ToString("0.00"), "0", "anticipo deshabilitado", fechaOperacion, FormPrincipal.userID.ToString(),
                efectivo, tarjeta, vales, cheque, transferencia, credito, "0", FormPrincipal.id_empleado.ToString()
            };

            cn.EjecutarConsulta(cs.OperacionCaja(datos));
        }

        private void Anticipos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void DGVAnticipos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void cbAnticipos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }

            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                txtBuscarAnticipo.Focus();
                e.Handled = true;
                return;
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                txtBuscarAnticipo.Focus();
                e.Handled = true;
                return;
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                txtBuscarAnticipo.Focus();
                e.Handled = true;
                return;
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                txtBuscarAnticipo.Focus();
                e.Handled = true;
                return;
            }

        }

        private void btnNuevoAnticipo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnBuscarAnticipos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void txtBuscarAnticipo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscarAnticipos.PerformClick();
            }
        }

        private void txtBuscarAnticipo_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarAnticipo.Text.Contains("\'"))
            {
                string producto = txtBuscarAnticipo.Text.Replace("\'", ""); ;
                txtBuscarAnticipo.Text = producto;
                txtBuscarAnticipo.Select(txtBuscarAnticipo.Text.Length, 0);
            }
            txtBuscarAnticipo.CharacterCasing = CharacterCasing.Upper;
        }

        private void btnActualizarMaximoProductos_Click(object sender, EventArgs e)
        {

        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {

        }

        private void btnUltimaPagina_Click(object sender, EventArgs e)
        {

        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {

        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizarMaximoProductos_Click_1(object sender, EventArgs e)
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
                DGVAnticipos.Rows.Clear();
                maximo_x_pagina = cantidadAMostrar;
                p.actualizarTope(maximo_x_pagina);
                CargarDatos(cbAnticipos.SelectedIndex + 1);
            }
            else if (txtMaximoPorPagina.Text.Equals(string.Empty))
            {
                txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
            }
        }

        private void ActualizarPaginador()
        {
            int BeforePage = 0, AfterPage = 0, LastPage = 0;

            linkLblPaginaAnterior.Visible = false;
            linkLblPaginaSiguiente.Visible = false;

            linkLblPaginaActual.Text = p.numPag().ToString();
            linkLblPaginaActual.LinkColor = Color.White;
            linkLblPaginaActual.BackColor = Color.Black;

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
                linkLblPrimeraPagina.Visible = false;
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
                    linkLblUltimaPagina.Visible = false;
                }
            }
        }

        private void btnUltimaPagina_Click_1(object sender, EventArgs e)
        {
            p.ultimaPagina();
            clickBoton = 1;
            CargarDatos(cbAnticipos.SelectedIndex + 1);
            ActualizarPaginador();
           
        }

        private void btnSiguiente_Click_1(object sender, EventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            CargarDatos(cbAnticipos.SelectedIndex + 1);
            ActualizarPaginador();
        }

        private void btnAnterior_Click_1(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            CargarDatos(cbAnticipos.SelectedIndex + 1);
            ActualizarPaginador();
        }

        private void btnPrimeraPagina_Click_1(object sender, EventArgs e)
        {
            p.primerPagina();
            clickBoton = 1;
            CargarDatos(cbAnticipos.SelectedIndex + 1);
            ActualizarPaginador();
        }

        private void linkLblPrimeraPagina_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLblPaginaAnterior_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void txtMaximoPorPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void cbAnticipos_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtMaximoPorPagina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnActualizarMaximoProductos.PerformClick();
            }
        }

        //private void dpFechaFinal_ValueChanged(object sender, EventArgs e)
        //{
        //    dpFechaFinal.Value = DateTime.Now;
        //}

        //private void dpFechaFinal_ValueChanged_1(object sender, EventArgs e)
        //{

        //}
    }
}
