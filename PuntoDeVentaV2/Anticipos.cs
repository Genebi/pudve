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

        IEnumerable<AgregarAnticipo> FormAnticipo = Application.OpenForms.OfType<AgregarAnticipo>();

        public Anticipos()
        {
            InitializeComponent();
        }

        private void Anticipos_Load(object sender, EventArgs e)
        {
            //Se crea el directorio para almacenar los tickets y otros archivos relacionados con ventas
            Directory.CreateDirectory(@"C:\Archivos PUDVE\Anticipos\Tickets");
            DateTime date = DateTime.Now;
            DateTime PrimerDia = new DateTime(date.Year, date.Month-1, 1);
            dpFechaInicial.Value = PrimerDia;
            dpFechaFinal.Value = DateTime.Now;
            cbAnticipos.DropDownStyle = ComboBoxStyle.DropDownList;
            CargarDatos(1);

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

        private void CargarDatos(int estado = 1, int tipo = 0)
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

            if (estado.Equals(4))
            {
                estado = 5;
            }

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

                    consulta = $"SELECT * FROM Anticipos WHERE IDUsuario = {FormPrincipal.userID} AND Status = {estado} AND Cliente LIKE '%{txtBuscarAnticipo.Text}%' AND DATE(Fecha) BETWEEN '{fechaInicio}' AND '{fechaFinal}'"; //AND Status != 4

                    conBusqueda = true;
                }

            //}

            sql_cmd = new MySqlCommand(consulta, sql_con);

            dr = sql_cmd.ExecuteReader();

            if (dr.HasRows )
            {
                DGVAnticipos.Rows.Clear();
            }
            else
            {
                DGVAnticipos.Rows.Clear();
            }

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Image ticket = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\ticket.png");
                    Image deshabilitar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\ban.png");
                    Image habilitar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\check.png");
                    Image devolver = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\reply.png");
                    Image info = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\info-circle.png");
                    Bitmap sinImagen = new Bitmap(1, 1);
                    sinImagen.SetPixel(0, 0, Color.White);

                    int rowId = DGVAnticipos.Rows.Add();

                    DataGridViewRow row = DGVAnticipos.Rows[rowId];
                    var empleado = string.Empty;
                    var empAdm = string.Empty;
                    empleado = dr.GetValue(dr.GetOrdinal("IDEmpleado")).ToString();
                    if (!empleado.Equals("0"))
                    {
                        empAdm = "Empleado";
                    }
                    else
                    {
                        empAdm = "Administrador";
                    }
                    row.Cells["ID"].Value = dr.GetValue(dr.GetOrdinal("ID"));
                    row.Cells["Concepto"].Value = dr.GetValue(dr.GetOrdinal("Concepto"));
                    row.Cells["Importe"].Value = dr.GetValue(dr.GetOrdinal("Importe"));
                    row.Cells["Cliente"].Value = dr.GetValue(dr.GetOrdinal("Cliente"));
                    row.Cells["Empleado"].Value = empAdm;
                    row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fecha"))).ToString("yyyy-MM-dd HH:mm:ss");
                    row.Cells["IDVenta"].Value = dr.GetValue(dr.GetOrdinal("IDVenta"));
                    row.Cells["FormaPago"].Value = dr.GetValue(dr.GetOrdinal("FormaPago"));
                    row.Cells["Ticket"].Value = ticket;

                    var status = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Status")));

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

            int opcion = cb.SelectedIndex;

            if (opcion >= 0)
            {
                if ((opcion + 1) == 4)
                    opcion = opcion + 1;

                CargarDatos(opcion + 1);
            }
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


                // Generar ticket
                if (e.ColumnIndex == 6)
                {
                    if (opcion1 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    if (!Utilidades.AdobeReaderInstalado())
                    {
                        Utilidades.MensajeAdobeReader();
                        return;
                    }

                    rutaTicketGenerado = @"C:\Archivos PUDVE\Anticipos\Tickets\ticket_anticipo_" + idAnticipo + ".pdf";
                    ticketGenerado = $"ticket_anticipo_{idAnticipo}.pdf";

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
                            cn.EjecutarConsulta(cs.CambiarStatusAnticipo(2, idAnticipo, FormPrincipal.userID));
                            CajaDeshabilitar(formaPago, importe);
                            CargarDatos(cbAnticipos.SelectedIndex + 1);
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
                        var respuesta = MessageBox.Show("¿Estás seguro de realizar esta acción?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (respuesta == DialogResult.Yes)
                        {
                            DevolverAnticipo da = new DevolverAnticipo(idAnticipo, importe);

                            da.FormClosed += delegate
                            {
                                CargarDatos(cbAnticipos.SelectedIndex + 1);
                            };

                            da.ShowDialog();
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

            var status = cbAnticipos.SelectedIndex;

            CargarDatos(status + 1, 1);
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
                efectivo, tarjeta, vales, cheque, transferencia, credito, "0"
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
            txtBuscarAnticipo.CharacterCasing = CharacterCasing.Upper;
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
