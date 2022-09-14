using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class AsignarClienteYMetodoPago : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();
        CargarDatosCaja cdc = new CargarDatosCaja();


        public string[] datosCliente;
        public string origenOperacion { get; set; }
        public int clienteId { get; set; }

        string formadepagoantigua;
        private int idVenta = 0;
        private int tipo = 0;
        string ultimoCorteDeCaja;
        int idClienteGlobal = 0;
        string NombreCliente = string.Empty, metodoDePago =string.Empty;
        List<string> IDVenta = new List<string>();
        decimal totalEfectivoVentaEnCaja = 0;
        public AsignarClienteYMetodoPago(List<string> ID)
        {
            InitializeComponent();
            this.IDVenta = ID;
        }

        private void DGVClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && DGVClientes.CurrentRow.Index == 0)
            {
                DGVClientes.ClearSelection();
                txtBuscador.Focus();
            }
            else if (e.KeyCode == Keys.Enter && !DGVClientes.Rows.Count.Equals(0))
            {
                DGVClientes_CellClick(this, new DataGridViewCellEventArgs(4, DGVClientes.CurrentRow.Index));

                //Evitar el salto de linea en el GDV
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void DGVClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 4)
                {
                    var idCliente = Convert.ToInt32(DGVClientes.Rows[e.RowIndex].Cells["ID"].Value);
                    idClienteGlobal = idCliente;
                    NombreCliente = DGVClientes.Rows[e.RowIndex].Cells["RazonSocial"].Value.ToString();

                    lblCliente.Text = NombreCliente;
                    btnEliminarCliente.Visible = true;
                    lblEliminar.Visible = true;
                }
            }
        }
        private void AsignarCliente(int idVenta, int idCliente, string cliente)
        {
            //Actualizar a la tabla detalle de venta
            var razonSocial = cliente;

            string[] datos = new string[] { idCliente.ToString(), razonSocial, idVenta.ToString(), FormPrincipal.userID.ToString() };

            cn.EjecutarConsulta(cs.GuardarDetallesVenta(datos, 1));
        }


        private void DGVClientes_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 4)
                {
                    DGVClientes.Cursor = Cursors.Hand;
                }
            }
        }

        private void DGVClientes_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 4)
                {
                    DGVClientes.Cursor = Cursors.Default;
                }
            }
        }

        private void AsignarClienteYMetodoPago_Load(object sender, EventArgs e)
        {
            CargarDatos();
            CBMetodoPago.SelectedIndex = 0;
        }
        private void CargarDatos(string busqueda = "")
        {
            MySqlConnection sql_con;
            MySqlCommand sql_cmd;
            MySqlDataReader dr;

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                sql_con = new MySqlConnection("datasource=" + Properties.Settings.Default.Hosting + ";port=6666;username=root;password=;database=pudve;");
            }
            else
            {
                sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            }

            var consulta = string.Empty;

            if (string.IsNullOrWhiteSpace(busqueda))
            {
                consulta = $"SELECT * FROM Clientes WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1";
            }
            else
            {
                consulta = $"SELECT * FROM Clientes WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND (RazonSocial LIKE '%{busqueda}%' OR RFC LIKE '%{busqueda}%' OR NumeroCliente LIKE '%{busqueda}%')";
            }
           
            sql_con.Open();
            sql_cmd = new MySqlCommand(consulta, sql_con);
            dr = sql_cmd.ExecuteReader();

            DGVClientes.Rows.Clear();

            while (dr.Read())
            {
                int rowId = DGVClientes.Rows.Add();

                DataGridViewRow row = DGVClientes.Rows[rowId];

                Image agregar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\reply.png");

                var numeroCliente = dr.GetValue(dr.GetOrdinal("NumeroCliente")).ToString();

                if (string.IsNullOrWhiteSpace(numeroCliente))
                {
                    numeroCliente = "N/A";
                }

                row.Cells["ID"].Value = dr.GetValue(dr.GetOrdinal("ID"));
                row.Cells["RFC"].Value = dr.GetValue(dr.GetOrdinal("RFC"));
                row.Cells["RazonSocial"].Value = dr.GetValue(dr.GetOrdinal("RazonSocial"));
                row.Cells["NumeroCliente"].Value = numeroCliente;
                row.Cells["Agregar"].Value = agregar;
            }

            DGVClientes.ClearSelection();

            dr.Close();
            sql_con.Close();
        }

        private void btnBucarCliente_Click(object sender, EventArgs e)
        {
            buscarCliente();
        }
        private void buscarCliente()
        {
            var busqueda = txtBuscador.Text.Trim();

            CargarDatos(busqueda);

            txtBuscador.Text = string.Empty;
            txtBuscador.Focus();
        }

        private void txtBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                buscarCliente();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            else if (e.KeyCode == Keys.Down && !DGVClientes.Rows.Count.Equals(0))
            {

                //DGVClientes.Rows[0].Cells["RFC"].Selected = true;
                if (DGVClientes.Rows.Count != 0)
                {
                    DGVClientes.Focus();
                    DGVClientes.CurrentRow.Selected = true;
                }
            }
        }

        private void btnEliminarCliente_Click(object sender, EventArgs e)
        {
            NombreCliente = string.Empty;
            lblCliente.Text = "";
            lblEliminar.Visible = false;
            btnEliminarCliente.Visible = false;
        }

        private void btnCancelarDesc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptarDesc_Click(object sender, EventArgs e)
        {
            if (NombreCliente.Equals("") && metodoDePago.Equals(""))
            {
                MessageBox.Show("Seleccione un cliente o un metodo de pago","Aviso del Sistema",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            foreach (var item in IDVenta)
            {
                if (!NombreCliente.Equals("") && !metodoDePago.Equals(""))
                {
                    CambioDeAmbasVariables(item);
                }
                else if (NombreCliente.Equals("") && !metodoDePago.Equals(""))
                {
                    CambiarSoloStatus(item);
                }
                else
                {
                    CambiarSoloNombre(item);
                }
            }
            this.Close();
        }

        private void CambiarSoloNombre(string id)
        {
            string RFC;
            using (var RFCCliente = cn.CargarDatos($"SELECT RFC FROM clientes WHERE ID = {idClienteGlobal}"))
            {
                RFC = RFCCliente.Rows[0]["RFC"].ToString();
            }
            cn.EjecutarConsulta($"UPDATE ventas SET IDCliente = '{idClienteGlobal}', Cliente = '{NombreCliente}',RFC ='{RFC}' WHERE ID = {id}");
            cn.EjecutarConsulta($"UPDATE detallesventa SET IDCliente = {idClienteGlobal}, Cliente = '{NombreCliente}' WHERE IDVenta ={id}");
            
        }

        private void CambiarSoloStatus(string id)
        {
            string Columna;
            if (!metodoDePago.Equals("Efectivo") && !metodoDePago.Equals(""))
            {
                var DTDatosVenta = cn.CargarDatos($"SELECT Total,`Status`,FormaPago,FechaOperacion FROM ventas WHERE ID = {id}");
                string total = DTDatosVenta.Rows[0]["Total"].ToString();
                string status = "1";
                string FormaPago = DTDatosVenta.Rows[0]["FormaPago"].ToString();
                DateTime FechaDePagoSinFormato = Convert.ToDateTime(DTDatosVenta.Rows[0]["FechaOperacion"]);
                string FechaDePago = FechaDePagoSinFormato.ToString("yyyy-MM-dd hh:mm:ss");
                bool esCredito = false;
                formadepagoantigua = DTDatosVenta.Rows[0]["FormaPago"].ToString();
                if (FormaPago.Equals("Efectivo"))
                {
                    using (DataTable dtHistorialCorteDeCaja = cn.CargarDatos(cs.cargarSaldoInicialAdministrador()))
                    {
                        if (!dtHistorialCorteDeCaja.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtHistorialCorteDeCaja.Rows)
                            {
                                ultimoCorteDeCaja = item["IDCaja"].ToString();
                            }
                        }
                    }
                    var idCajaUltimoCorte = ultimoCorteDeCaja;
                    decimal cantidadEfectivo = 0;
                    using (DataTable dtSeccionVentaAdministrador = cn.CargarDatos(cs.totalCantidadesVentasAdministrador(idCajaUltimoCorte)))
                    {
                        if (!dtSeccionVentaAdministrador.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtSeccionVentaAdministrador.Rows)
                            {
                                if (!string.IsNullOrWhiteSpace(item["Efectivo"].ToString()))
                                {
                                    cantidadEfectivo = Convert.ToDecimal(item["Efectivo"].ToString());
                                    totalEfectivoVentaEnCaja = cantidadEfectivo;
                                }
                            }
                        }
                    }
                    var datos = cdc.CargarSaldo("Caja");
                    decimal ventas = cantidadEfectivo;
                    decimal anticipos = Convert.ToDecimal(datos[14]);
                    decimal dineroAgregado = Convert.ToDecimal(datos[20]);
                    decimal dineroRtirado = Convert.ToDecimal(datos[26]);
                    decimal EfectivoCaja = ventas + anticipos + dineroAgregado - dineroRtirado;
                    if (Convert.ToDecimal(EfectivoCaja) < Convert.ToDecimal(total))
                    {
                        MessageBox.Show("No cuenta con suficiente Efectivo en caja", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (metodoDePago.Equals("Crédito"))
                {
                    FormaPago = "Credito";
                }
                else
                {
                    FormaPago = metodoDePago;
                }
                if (metodoDePago.Equals("Crédito"))
                {
                    status = "4";
                    esCredito = true;
                }
                if (esCredito.Equals(true))
                {
                    using (var DTDetalleVenta = cn.CargarDatos($"SELECT {formadepagoantigua} FROM `detallesventa` WHERE IDVenta = {id} ORDER BY IDVenta DESC LIMIT 1"))
                    {
                        cn.EjecutarConsulta($"UPDATE detallesventa SET Efectivo = '0.00',Tarjeta = '0.00', Transferencia = '0.00', Vales = '0.00', Cheque = '0.00', Credito = '0.00' WHERE IDVenta = '{id}'");
                        string cambioCredito = DTDetalleVenta.Rows[0][$"{formadepagoantigua}"].ToString();

                        cn.EjecutarConsulta($"UPDATE detallesventa SET Credito = {cambioCredito} WHERE IDVenta = '{id}'");
                    }
                    cn.EjecutarConsulta($"UPDATE ventas SET FormaPago = '{FormaPago}', `Status` = '{status}',DineroRecibido = '0.00' WHERE ID = {id}");
                }
                else
                {
                    cn.EjecutarConsulta($"UPDATE ventas SET FormaPago = '{FormaPago}', `Status` = '{status}' WHERE ID = {id}");
                }


                cn.EjecutarConsulta($"UPDATE caja SET Efectivo = '0.00',Tarjeta = '0.00', Transferencia = '0.00', Vales = '0.00', Cheque = '0.00', Credito = '0.00' WHERE FechaOperacion = '{FechaDePago}'");

                if (metodoDePago.Equals("Crédito"))
                {
                    Columna = "Credito";
                }
                else
                {
                    Columna = metodoDePago;
                }

                cn.EjecutarConsulta($"UPDATE caja SET {Columna} = {total} WHERE FechaOperacion = '{FechaDePago}'");
            }
            else
            {
                var DTDatosVenta = cn.CargarDatos($"SELECT Total,`Status`,FormaPago,FechaOperacion FROM ventas WHERE ID = {id}");
                string total = DTDatosVenta.Rows[0]["Total"].ToString();
                string status = "1";
                string FormaPago;
                DateTime FechaDePagoSinFormato = Convert.ToDateTime(DTDatosVenta.Rows[0]["FechaOperacion"]);
                string FechaDePago = FechaDePagoSinFormato.ToString("yyyy-MM-dd hh:mm:ss");
                bool esCredito = false;
                formadepagoantigua = DTDatosVenta.Rows[0]["FormaPago"].ToString();
                if (metodoDePago.Equals("Crédito"))
                {
                    FormaPago = "Credito";
                    status = "4";
                }
                else
                {
                    FormaPago = metodoDePago;
                }
                if (esCredito.Equals(true))
                {
                    using (var DTDetalleVenta = cn.CargarDatos($"SELECT {formadepagoantigua} FROM `detallesventa` WHERE IDVenta = {id} ORDER BY IDVenta DESC LIMIT 1"))
                    {
                        cn.EjecutarConsulta($"UPDATE detallesventa SET Efectivo = '0.00',Tarjeta = '0.00', Transferencia = '0.00', Vales = '0.00', Cheque = '0.00', Credito = '0.00' WHERE IDVenta = '{id}'");
                        string cambioCredito = DTDetalleVenta.Rows[0][$"{formadepagoantigua}"].ToString();

                        cn.EjecutarConsulta($"UPDATE detallesventa SET Credito = {cambioCredito} WHERE IDVenta = '{id}'");
                    }
                    cn.EjecutarConsulta($"UPDATE ventas SET FormaPago = '{FormaPago}', `Status` = '{status}',DineroRecibido = '0.00' WHERE ID = {id}");
                }
                else
                {
                    cn.EjecutarConsulta($"UPDATE ventas SET FormaPago = '{FormaPago}', `Status` = '{status}' WHERE ID = {id}");
                }
                cn.EjecutarConsulta($"UPDATE caja SET Efectivo = '0.00',Tarjeta = '0.00', Transferencia = '0.00', Vales = '0.00', Cheque = '0.00', Credito = '0.00' WHERE FechaOperacion = '{FechaDePago}'");
                
                if (metodoDePago.Equals("Crédito"))
                {
                    Columna = "Credito";
                }
                else
                {
                    Columna = metodoDePago;
                }

                cn.EjecutarConsulta($"UPDATE caja SET {Columna} = {total} WHERE FechaOperacion = '{FechaDePago}'");
            }
        }

        private void CambioDeAmbasVariables(string id)
        {
            string Columna;
            string total;
            string status = "1";
            string FormaPago= string.Empty;
            string FechaDePago;
            string RFC;
            bool esCredito = false;

            if (!metodoDePago.Equals("Efectivo") && !metodoDePago.Equals(""))
            {
                using (DataTable DTDatosVenta = cn.CargarDatos($"SELECT Total,`Status`,FormaPago,FechaOperacion FROM ventas WHERE ID = {id}"))
                {
                    total = DTDatosVenta.Rows[0]["Total"].ToString();
                    status = "1";
                    formadepagoantigua = DTDatosVenta.Rows[0]["FormaPago"].ToString();
                    DateTime FechaDePagoSinFormato = Convert.ToDateTime(DTDatosVenta.Rows[0]["FechaOperacion"]);
                    FechaDePago = FechaDePagoSinFormato.ToString("yyyy-MM-dd hh:mm:ss");
                    FormaPago = DTDatosVenta.Rows[0]["FormaPago"].ToString();

                }
                using (var RFCCliente = cn.CargarDatos($"SELECT RFC FROM clientes WHERE ID = {idClienteGlobal}"))
                {
                    RFC = RFCCliente.Rows[0]["RFC"].ToString();
                }
                if (FormaPago.Equals("Efectivo"))
                {
                    var DTDatosVenta = cn.CargarDatos($"SELECT Total,`Status`,FormaPago,FechaOperacion FROM ventas WHERE ID = {id}");
                     total = DTDatosVenta.Rows[0]["Total"].ToString();
                     status = "1";
                     FormaPago = DTDatosVenta.Rows[0]["FormaPago"].ToString();
                    DateTime FechaDePagoSinFormato = Convert.ToDateTime(DTDatosVenta.Rows[0]["FechaOperacion"]);
                     FechaDePago = FechaDePagoSinFormato.ToString("yyyy-MM-dd hh:mm:ss");
                     esCredito = false;
                    formadepagoantigua = DTDatosVenta.Rows[0]["FormaPago"].ToString();
                    if (FormaPago.Equals("Efectivo"))
                    {
                        using (DataTable dtHistorialCorteDeCaja = cn.CargarDatos(cs.cargarSaldoInicialAdministrador()))
                        {
                            if (!dtHistorialCorteDeCaja.Rows.Count.Equals(0))
                            {
                                foreach (DataRow item in dtHistorialCorteDeCaja.Rows)
                                {
                                    ultimoCorteDeCaja = item["IDCaja"].ToString();
                                }
                            }
                        }
                        var idCajaUltimoCorte = ultimoCorteDeCaja;
                        decimal cantidadEfectivo = 0;
                        using (DataTable dtSeccionVentaAdministrador = cn.CargarDatos(cs.totalCantidadesVentasAdministrador(idCajaUltimoCorte)))
                        {
                            if (!dtSeccionVentaAdministrador.Rows.Count.Equals(0))
                            {
                                foreach (DataRow item in dtSeccionVentaAdministrador.Rows)
                                {
                                    if (!string.IsNullOrWhiteSpace(item["Efectivo"].ToString()))
                                    {
                                        cantidadEfectivo = Convert.ToDecimal(item["Efectivo"].ToString());
                                        totalEfectivoVentaEnCaja = cantidadEfectivo;
                                    }
                                }
                            }
                        }
                        var datos = cdc.CargarSaldo("Caja");
                        decimal ventas = cantidadEfectivo;
                        decimal anticipos = Convert.ToDecimal(datos[14]);
                        decimal dineroAgregado = Convert.ToDecimal(datos[20]);
                        decimal dineroRtirado = Convert.ToDecimal(datos[26]);
                        decimal EfectivoCaja = ventas + anticipos + dineroAgregado - dineroRtirado;
                        if (Convert.ToDecimal(EfectivoCaja) < Convert.ToDecimal(total))
                        {
                            MessageBox.Show("No cuenta con suficiente Efectivo en caja", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                if (metodoDePago.Equals("Crédito"))
                {
                    status = "4";
                    FormaPago = "Credito";
                    esCredito = true;
                }
                else
                {
                    FormaPago = metodoDePago;
                }
                if (esCredito.Equals(true))
                {
                    using (var DTDetalleVenta = cn.CargarDatos($"SELECT {formadepagoantigua} FROM `detallesventa` WHERE IDVenta = {id} ORDER BY IDVenta DESC LIMIT 1"))
                    {
                        cn.EjecutarConsulta($"UPDATE detallesventa SET Efectivo = '0.00',Tarjeta = '0.00', Transferencia = '0.00', Vales = '0.00', Cheque = '0.00', Credito = '0.00' WHERE IDVenta = '{id}'");
                        string cambioCredito = DTDetalleVenta.Rows[0][$"{formadepagoantigua}"].ToString();

                        cn.EjecutarConsulta($"UPDATE detallesventa SET Credito = {cambioCredito} WHERE IDVenta = '{id}'");
                    }
                    cn.EjecutarConsulta($"UPDATE ventas SET IDCliente = '{idClienteGlobal}', Cliente = '{NombreCliente}',RFC ='{RFC}', FormaPago = '{FormaPago}', `Status` = '{status}',DineroRecibido = '0.00' WHERE ID = {id}");
                }
                else
                {
                    cn.EjecutarConsulta($"UPDATE ventas SET IDCliente = '{idClienteGlobal}', Cliente = '{NombreCliente}',RFC ='{RFC}', FormaPago = '{FormaPago}', `Status` = '{status}' WHERE ID = {id}");
                }
                cn.EjecutarConsulta($"UPDATE caja SET Efectivo = '0.00',Tarjeta = '0.00', Transferencia = '0.00', Vales = '0.00', Cheque = '0.00', Credito = '0.00' WHERE FechaOperacion = '{FechaDePago}'");
                cn.EjecutarConsulta($"UPDATE detallesventa SET IDCliente = {idClienteGlobal}, Cliente = '{NombreCliente}' WHERE IDVenta ={id}");

                if (metodoDePago.Equals("Crédito"))
                {
                    Columna = "Credito";
                }
                else
                {
                    Columna = metodoDePago;
                }

                cn.EjecutarConsulta($"UPDATE caja SET {Columna} = {total} WHERE FechaOperacion = '{FechaDePago}'");
            }
            else
            {
                using (DataTable DTDatosVenta = cn.CargarDatos($"SELECT Total,`Status`,FormaPago,FechaOperacion FROM ventas WHERE ID = {id}"))
                {
                    total = DTDatosVenta.Rows[0]["Total"].ToString();
                    status = "1";
                    DateTime FechaDePagoSinFormato = Convert.ToDateTime(DTDatosVenta.Rows[0]["FechaOperacion"]);
                    FechaDePago = FechaDePagoSinFormato.ToString("yyyy-MM-dd hh:mm:ss");
                    formadepagoantigua = DTDatosVenta.Rows[0]["FormaPago"].ToString();
                }
                using (var RFCCliente = cn.CargarDatos($"SELECT RFC FROM clientes WHERE ID = {idClienteGlobal}"))
                {
                    RFC = RFCCliente.Rows[0]["RFC"].ToString();
                }
                if (metodoDePago.Equals("Crédito"))
                {
                    status = "4";
                }
                if (metodoDePago.Equals("Crédito"))
                {
                    FormaPago = "Credito";
                    esCredito = true;
                }
                else
                {
                    FormaPago = metodoDePago;
                }
                if (esCredito.Equals(true))
                {
                    using (var DTDetalleVenta = cn.CargarDatos($"SELECT {formadepagoantigua} FROM `detallesventa` WHERE IDVenta = {id} ORDER BY IDVenta DESC LIMIT 1"))
                    {
                        cn.EjecutarConsulta($"UPDATE detallesventa SET Efectivo = '0.00',Tarjeta = '0.00', Transferencia = '0.00', Vales = '0.00', Cheque = '0.00', Credito = '0.00' WHERE IDVenta = '{id}'");
                        string cambioCredito = DTDetalleVenta.Rows[0][$"{formadepagoantigua}"].ToString();

                        cn.EjecutarConsulta($"UPDATE detallesventa SET Credito = {cambioCredito} WHERE IDVenta = '{id}'");
                    }
                    cn.EjecutarConsulta($"UPDATE ventas SET IDCliente = '{idClienteGlobal}', Cliente = '{NombreCliente}',RFC ='{RFC}', FormaPago = '{FormaPago}', `Status` = '{status}',DineroRecibido = '0.00' WHERE ID = {id}");

                }
                else
                {
                    cn.EjecutarConsulta($"UPDATE ventas SET IDCliente = '{idClienteGlobal}', Cliente = '{NombreCliente}',RFC ='{RFC}',FormaPago = '{FormaPago}', `Status` = '{status}' WHERE ID = {id}");
                }
                cn.EjecutarConsulta($"UPDATE caja SET Efectivo = '0.00',Tarjeta = '0.00', Transferencia = '0.00', Vales = '0.00', Cheque = '0.00', Credito = '0.00' WHERE FechaOperacion = '{FechaDePago}'");
                cn.EjecutarConsulta($"UPDATE detallesventa SET IDCliente = {idClienteGlobal}, Cliente = '{NombreCliente}' WHERE IDVenta ={id}");
                if (metodoDePago.Equals("Crédito"))
                {
                    Columna = "Credito";
                }
                else
                {
                    Columna = metodoDePago;
                }

                cn.EjecutarConsulta($"UPDATE caja SET {Columna} = {total} WHERE FechaOperacion = '{FechaDePago}'");
            }
        }

        private void CBMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBMetodoPago.SelectedIndex==0)
            {
                lblPago.Text = "";
                metodoDePago = "";
            }
            else if(CBMetodoPago.SelectedIndex == 1)
            {
                lblPago.Text = CBMetodoPago.Text;
                metodoDePago = "Efectivo";
            }
            else if (CBMetodoPago.SelectedIndex == 2)
            {
                lblPago.Text = CBMetodoPago.Text;
                metodoDePago = "Tarjeta";
            }
            else if (CBMetodoPago.SelectedIndex == 3)
            {
                lblPago.Text = CBMetodoPago.Text;
                metodoDePago = "Transferencia";
            }
            else if (CBMetodoPago.SelectedIndex == 4)
            {
                lblPago.Text = CBMetodoPago.Text;
                metodoDePago = "Cheque";
            }
            else if (CBMetodoPago.SelectedIndex == 5)
            {
                lblPago.Text = CBMetodoPago.Text;
                metodoDePago = "Vales";
            }
            else if (CBMetodoPago.SelectedIndex == 6)
            {
                lblPago.Text = CBMetodoPago.Text;
                metodoDePago = "Crédito";
            }
        }
    }
}
