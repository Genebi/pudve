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
    public partial class ListaClientes : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();


        

        public string[] datosCliente;
        public string origenOperacion { get; set; }
        public int clienteId { get; set; }

        private int idVenta = 0;
        private int tipo = 0;
        
        int idClienteGlobal = 0;

        // Tipo: 0 = Por defecto
        // Tipo: 1 = Por parte de la ventana Venta
        // Tipo: 2 = Por parte de la ventana DetalleVenta
        public ListaClientes(int idVenta = 0, int tipo = 0)
        {
            InitializeComponent();

            this.idVenta = idVenta;
            this.tipo = tipo;
        }


        private void ListaClientes_Load(object sender, EventArgs e)
        {
            if (!Ventas.idCliente.Equals(""))
            {
                using (DataTable ConsultaNombre= cn.CargarDatos(cs.NombreClientePorID(Ventas.idCliente)))
                {
                    string Nombre = ConsultaNombre.Rows[0]["RazonSocial"].ToString();
                    lblCliente.Visible = true;
                    lblCliente.Text = $"Cliente Recomendado: {Nombre}";
                }
            }

            CargarDatos();
            btnPublicoGeneral.Focus();
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

            if (Ventas.EsClienteConDescuento.Equals(true))
            {
                if (string.IsNullOrWhiteSpace(busqueda))
                {
                    consulta = $"SELECT * FROM Clientes WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND TipoCliente > 0";
                }
                else
                {
                    consulta = $"SELECT * FROM Clientes WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND (RazonSocial LIKE '%{busqueda}%' OR RFC LIKE '%{busqueda}%' OR NumeroCliente LIKE '%{busqueda}%') AND TipoCliente > 0";
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(busqueda))
                {
                    consulta = $"SELECT * FROM Clientes WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1";
                }
                else
                {
                    consulta = $"SELECT * FROM Clientes WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND (RazonSocial LIKE '%{busqueda}%' OR RFC LIKE '%{busqueda}%' OR NumeroCliente LIKE '%{busqueda}%')";
                }
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

        private void realizarMovimiento()
        {

        }

        private void DGVClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 4)
                {
                    var idCliente = Convert.ToInt32(DGVClientes.Rows[e.RowIndex].Cells["ID"].Value);
                    idClienteGlobal = idCliente;
                    var cliente = DGVClientes.Rows[e.RowIndex].Cells["RazonSocial"].Value.ToString();

                    if (tipo == 0)
                    {
                        if (origenOperacion == null)
                        {
                            if (idVenta > 0)
                            {
                                AsignarCliente(idVenta, idCliente, cliente);
                            }
                            else
                            {
                                DetalleVenta.idCliente = idCliente;
                                DetalleVenta.cliente = cliente;
                                DetalleVenta.nameClienteNameVenta = cliente;

                                AsignarCreditoVenta.idCliente = idCliente;
                                AsignarCreditoVenta.cliente = cliente;

                                Ventas.idCliente = idCliente.ToString();
                                Ventas.statusVenta = "2";
                                Ventas.ventaGuardada = true;
                            }
                        }
                        else
                        {
                            if (origenOperacion.Equals("VentaGlobal"))
                            {
                                clienteId = idCliente;
                                DialogResult = DialogResult.OK;
                            }
                        }
                        
                        
                    }

                    //Editar
                    if (tipo == 1)
                    {
                        datosCliente = mb.ObtenerDatosCliente(idCliente, FormPrincipal.userID);

                        if (datosCliente.Length > 0)
                        {
                            datosCliente = new List<string>(datosCliente) { idCliente.ToString() }.ToArray();
                            DialogResult = DialogResult.OK;
                        }
                    }

                    if (tipo == 2)
                    {
                        DetalleVenta.idCliente = idCliente;
                        DetalleVenta.cliente = cliente;
                        DetalleVenta.nameClienteNameVenta = cliente;

                        AsignarCreditoVenta.idCliente = idCliente;
                        AsignarCreditoVenta.cliente = cliente;

                        Ventas.idCliente = idCliente.ToString();
                        Ventas.ventaGuardada = false;
                    }

                    this.Close();
                }
            }
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


        //Para asignar un cliente de los ya registrados a una venta la cual se quiera timbrar pero no
        //se le haya asignado cliente al momento de realizarse la venta
        private void AsignarCliente(int idVenta, int idCliente, string cliente)
        {
            //Actualizar a la tabla detalle de venta
            var razonSocial = cliente;

            string[] datos = new string[] { idCliente.ToString(), razonSocial, idVenta.ToString(), FormPrincipal.userID.ToString() };

            cn.EjecutarConsulta(cs.GuardarDetallesVenta(datos, 1));
        }

        private void lbAgregarCliente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
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

        private void ListaClientes_Shown(object sender, EventArgs e)
        {
            btnPublicoGeneral.Focus();
        }

        private void btnPublicoG_Click(object sender, EventArgs e)
        {
            Ventas.idCliente = "0";

            Dispose();
        }

        private void ListaClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
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

        

        private void btnPublicoGeneral_Click(object sender, EventArgs e)
        {
            if (origenOperacion == null)
            {
                using (DataTable dtPublicoGeneral = cn.CargarDatos(cs.BuscarPublicaGeneral()))
                {
                    if (!dtPublicoGeneral.Rows.Count.Equals(0))
                    {
                        DataRow drPublicoGeneral = dtPublicoGeneral.Rows[0];
                        var IDPublicoGeneral = Convert.ToInt32(drPublicoGeneral["ID"].ToString());
                        var razonSocialPublicoGeneral = drPublicoGeneral["RazonSocial"].ToString();
                        DetalleVenta.idCliente = IDPublicoGeneral;
                        DetalleVenta.cliente = razonSocialPublicoGeneral;
                        DetalleVenta.nameClienteNameVenta = razonSocialPublicoGeneral;
                        AsignarCreditoVenta.idCliente = IDPublicoGeneral;
                        AsignarCreditoVenta.cliente = razonSocialPublicoGeneral;
                        Ventas.idCliente = IDPublicoGeneral.ToString();
                        Ventas.statusVenta = "2";
                        Ventas.ventaGuardada = true;
                        this.Close();
                    }
                    else
                    {
                        var UltimoNumeroCliente = string.Empty;
                        using (DataTable dtUltimocliente = cn.CargarDatos(cs.UltimoNumerodeCliente()))
                        {
                            if (!dtUltimocliente.Rows.Count.Equals(0))
                            {
                                foreach (DataRow item in dtUltimocliente.Rows)
                                {
                                    var numCliente = Convert.ToInt32(item["NumeroCliente"]);
                                    var longitud = 6 - numCliente.ToString().Length;
                                    if (longitud.Equals(5))
                                    {
                                        UltimoNumeroCliente = $"00000{numCliente + 1}";
                                    }
                                    if (longitud.Equals(4))
                                    {
                                        UltimoNumeroCliente = $"0000{numCliente + 1}";
                                    }
                                    if (longitud.Equals(3))
                                    {
                                        UltimoNumeroCliente = $"000{numCliente + 1}";
                                    }
                                    if (longitud.Equals(2))
                                    {
                                        UltimoNumeroCliente = $"00{numCliente + 1}";
                                    }
                                    if (longitud.Equals(1))
                                    {
                                        UltimoNumeroCliente = $"0{numCliente + 1}";
                                    }
                                    if (longitud.Equals(0))
                                    {
                                        UltimoNumeroCliente = $"{numCliente + 1}";
                                    }
                                }
                            }
                            else
                            {
                                UltimoNumeroCliente = "000001";
                            }
                        }
                        var resultado = cn.EjecutarConsulta(cs.AgregarPublicoGeneral(UltimoNumeroCliente));
                        if (resultado.Equals(1))
                        {
                            using (DataTable dtNuevoClienteGeneral = cn.CargarDatos(cs.ObtenerDatosClientePublicoGeneral()))
                            {
                                if (!dtNuevoClienteGeneral.Rows.Count.Equals(0))
                                {
                                    DataRow drNuevoPublicoGeneral = dtNuevoClienteGeneral.Rows[0];
                                    var IDPublicoGeneral = Convert.ToInt32(drNuevoPublicoGeneral["ID"].ToString());
                                    var razonSocialPublicoGeneral = drNuevoPublicoGeneral["RazonSocial"].ToString();
                                    DetalleVenta.idCliente = IDPublicoGeneral;
                                    DetalleVenta.cliente = razonSocialPublicoGeneral;
                                    DetalleVenta.nameClienteNameVenta = razonSocialPublicoGeneral;
                                    AsignarCreditoVenta.idCliente = IDPublicoGeneral;
                                    AsignarCreditoVenta.cliente = razonSocialPublicoGeneral;
                                    Ventas.idCliente = IDPublicoGeneral.ToString();
                                    Ventas.statusVenta = "2";
                                    Ventas.ventaGuardada = true;
                                    this.Close();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (origenOperacion.Equals("VentaGlobal"))
                {
                    var clientePG = mb.ExisteClientePublicoGeneral(FormPrincipal.userID);

                    if (clientePG == 0)
                    {
                        clientePG = cn.EjecutarConsulta($"INSERT INTO Clientes (IDUsuario, RazonSocial, RFC, FechaOperacion) VALUES ('{FormPrincipal.userID}', 'PUBLICO GENERAL', 'XAXX010101000', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')", regresarID: true);
                    }

                    clienteId = clientePG;
                    DialogResult = DialogResult.OK;

                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            AgregarCliente nuevo = new AgregarCliente();

            nuevo.FormClosed += delegate
            {
                CargarDatos();
            };

            nuevo.ShowDialog();
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var cliente = Ventas.NombreCliente;
            int idCliente = Convert.ToInt32(Ventas.idCliente);

            if (tipo == 0)
            {
                if (idVenta > 0)
                {
                    AsignarCliente(idVenta, idCliente, cliente);
                }
                else
                {
                    DetalleVenta.idCliente = idCliente;
                    DetalleVenta.cliente = cliente;
                    DetalleVenta.nameClienteNameVenta = cliente;

                    AsignarCreditoVenta.idCliente = idCliente;
                    AsignarCreditoVenta.cliente = cliente;

                    Ventas.idCliente = idCliente.ToString();
                    Ventas.statusVenta = "2";
                    Ventas.ventaGuardada = true;
                }
            }

            //Editar
            if (tipo == 1)
            {
                datosCliente = mb.ObtenerDatosCliente(idCliente, FormPrincipal.userID);

                if (datosCliente.Length > 0)
                {
                    datosCliente = new List<string>(datosCliente) { idCliente.ToString() }.ToArray();
                    DialogResult = DialogResult.OK;
                }
            }

            if (tipo == 2)
            {
                DetalleVenta.idCliente = idCliente;
                DetalleVenta.cliente = cliente;
                DetalleVenta.nameClienteNameVenta = cliente;

                AsignarCreditoVenta.idCliente = idCliente;
                AsignarCreditoVenta.cliente = cliente;

                Ventas.idCliente = idCliente.ToString();
                Ventas.ventaGuardada = false;
            }

            this.Close();
        }
    }
}
