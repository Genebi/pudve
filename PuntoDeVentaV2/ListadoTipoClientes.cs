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

            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                sql_con = new SQLiteConnection("Data source=//" + Properties.Settings.Default.Hosting + @"\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }
            else
            {
                sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }

            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT * FROM TipoClientes WHERE IDUsuario = {FormPrincipal.userID} AND Habilitar = 1 ORDER BY FechaOperacion ASC", sql_con);
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
                    MessageBox.Show(id.ToString());
                }

                DGVClientes.ClearSelection();
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
    }
}
