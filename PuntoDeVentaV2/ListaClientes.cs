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

        private int idVenta = 0;

        public ListaClientes(int idVenta = 0)
        {
            InitializeComponent();

            this.idVenta = idVenta;
        }


        private void ListaClientes_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();

            var consulta = $"SELECT * FROM Clientes WHERE IDUsuario = {FormPrincipal.userID}";

            sql_cmd = new SQLiteCommand(consulta, sql_con);

            dr = sql_cmd.ExecuteReader();

            DGVClientes.Rows.Clear();

            while (dr.Read())
            {
                int rowId = DGVClientes.Rows.Add();

                DataGridViewRow row = DGVClientes.Rows[rowId];

                Image agregar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\reply.png");

                row.Cells["ID"].Value = dr.GetValue(dr.GetOrdinal("ID"));
                row.Cells["RFC"].Value = dr.GetValue(dr.GetOrdinal("RFC"));
                row.Cells["RazonSocial"].Value = dr.GetValue(dr.GetOrdinal("RazonSocial"));
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
                if (e.ColumnIndex == 3)
                {
                    var idCliente = Convert.ToInt32(DGVClientes.Rows[e.RowIndex].Cells["ID"].Value);
                    var cliente = DGVClientes.Rows[e.RowIndex].Cells["RazonSocial"].Value.ToString();

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
                    }

                    this.Dispose();
                }
            }
        }

        private void DGVClientes_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 3)
                {
                    DGVClientes.Cursor = Cursors.Hand;
                }
            }
        }

        private void DGVClientes_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 3)
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
    }
}
