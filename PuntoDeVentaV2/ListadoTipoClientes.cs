using MySql.Data.MySqlClient;
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
    public partial class ListadoTipoClientes : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        public ListadoTipoClientes()
        {
            InitializeComponent();
        }

        private void ListadoTipoClientes_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            var servidor = Properties.Settings.Default.Hosting;
            MySqlConnection sql_con;
            MySqlCommand sql_cmd;
            MySqlDataReader dr;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                sql_con = new MySqlConnection($"datasource={servidor};port=6666;username=root;password=;database=pudve;");
            }
            else
            {
                sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            }

            sql_con.Open();
            sql_cmd = new MySqlCommand($"SELECT * FROM TipoClientes WHERE IDUsuario = {FormPrincipal.userID} AND Habilitar = 1 ORDER BY FechaOperacion ASC", sql_con);
            dr = sql_cmd.ExecuteReader();

            DGVClientes.Rows.Clear();

            while (dr.Read())
            {
                int rowId = DGVClientes.Rows.Add();

                DataGridViewRow row = DGVClientes.Rows[rowId];

                row.Cells["ID"].Value = dr.GetValue(dr.GetOrdinal("ID"));
                row.Cells["TipoCliente"].Value = dr.GetValue(dr.GetOrdinal("Nombre"));
                row.Cells["Descuento"].Value = dr.GetValue(dr.GetOrdinal("DescuentoPorcentaje"));
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");

                Image imgMostrar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\pencil.png");
                Image imgEliminar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");

                row.Cells["Editar"].Value = imgMostrar;
                row.Cells["Deshabilitar"].Value = imgEliminar;
            }

            dr.Close();
            sql_con.Close();

            DGVClientes.ClearSelection();
        }

        private void DGVClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var fila = DGVClientes.CurrentCell.RowIndex;
                var id = Convert.ToInt32(DGVClientes.Rows[fila].Cells["ID"].Value);

                // Editar
                if (e.ColumnIndex == 4)
                {
                    using (var editar = new AgregarTipoCliente(id, 2))
                    {
                        editar.ShowDialog();

                        CargarDatos();
                    }
                }

                // Eliminar
                if (e.ColumnIndex == 5)
                {
                    var mensaje = "¿Estás seguro de eliminar este tipo de cliente?\n\n" +
                                  "NOTA: Este descuento se removerá de los clientes\n" +
                                  "a los que se le haya aplicado";

                    var respuesta = MessageBox.Show(mensaje, "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes)
                    {
                        var primerResultado = cn.EjecutarConsulta($"UPDATE TipoClientes SET Habilitar = 2 WHERE ID = {id} AND IDUsuario = {FormPrincipal.userID}");
                        var segundoResultado = cn.EjecutarConsulta($"UPDATE Clientes SET TipoCliente = 0 WHERE IDUsuario = {FormPrincipal.userID} AND TipoCliente = {id}");

                        if (primerResultado > 0 && segundoResultado > 0)
                        {
                            CargarDatos();
                        }
                    }
                }

                DGVClientes.ClearSelection();
                CargarDatos();
            }
        }

        private void DGVClientes_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 4)
            {
                DGVClientes.Cursor = Cursors.Hand;
            }
            else
            {
                DGVClientes.Cursor = Cursors.Default;
            }
        }

        private void ListadoTipoClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
