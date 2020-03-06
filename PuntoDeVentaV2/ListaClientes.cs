using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
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

        private int idVenta = 0;
        private int tipo = 0;

        // Tipo: 0 = Por defecto
        // Tipo: 1 = Por parte de la ventana Venta
        public ListaClientes(int idVenta = 0, int tipo = 0)
        {
            InitializeComponent();

            this.idVenta = idVenta;
            this.tipo = tipo;
        }


        private void ListaClientes_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos(string busqueda = "")
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
            sql_cmd = new SQLiteCommand(consulta, sql_con);
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

        private void DGVClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 4)
                {
                    var idCliente = Convert.ToInt32(DGVClientes.Rows[e.RowIndex].Cells["ID"].Value);
                    var cliente = DGVClientes.Rows[e.RowIndex].Cells["RazonSocial"].Value.ToString();

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

                            AsignarCreditoVenta.idCliente = idCliente;
                            AsignarCreditoVenta.cliente = cliente;

                            Ventas.idCliente = idCliente.ToString();
                        }
                    }
                    
                    if (tipo == 1)
                    {
                        datosCliente = mb.ObtenerDatosCliente(idCliente, FormPrincipal.userID);

                        if (datosCliente.Length > 0)
                        {
                            DialogResult = DialogResult.OK;
                        }
                    }

                    this.Dispose();
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
            AgregarCliente nuevo = new AgregarCliente();

            nuevo.FormClosed += delegate
            {
                CargarDatos();
            };

            nuevo.ShowDialog();
        }

        private void txtBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                var busqueda = txtBuscador.Text.Trim();

                CargarDatos(busqueda);

                txtBuscador.Text = string.Empty;
                txtBuscador.Focus();
            }
        }

        private void ListaClientes_Shown(object sender, EventArgs e)
        {
            txtBuscador.Focus();
        }
    }
}
