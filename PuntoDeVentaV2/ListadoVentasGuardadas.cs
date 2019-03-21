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
    public partial class ListadoVentasGuardadas : Form
    {
        public ListadoVentasGuardadas()
        {
            InitializeComponent();

            CargarDatos();
        }


        private void CargarDatos()
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + "\\BD\\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT * FROM Ventas WHERE Status = 2 AND IDUsuario = '{FormPrincipal.userID}'", sql_con);
            dr = sql_cmd.ExecuteReader();

            DGVListaVentasGuardadas.Rows.Clear();

            while (dr.Read())
            {
                if (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID"))) == Ventas.mostrarVenta)
                {
                    continue;
                }

                int rowId = DGVListaVentasGuardadas.Rows.Add();

                DataGridViewRow row = DGVListaVentasGuardadas.Rows[rowId];

                row.Cells["ID"].Value = dr.GetValue(dr.GetOrdinal("ID"));
                row.Cells["Cliente"].Value = "Público General";
                row.Cells["Importe"].Value = dr.GetValue(dr.GetOrdinal("Total"));
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");

                Image imgMostrar  = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\eye.png");
                Image imgEliminar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\trash.png");

                row.Cells["Mostrar"].Value = imgMostrar;
                row.Cells["Eliminar"].Value = imgEliminar;
            }

            dr.Close();
            sql_con.Close();
        }

        private void DGVListaVentasGuardadas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var fila = DGVListaVentasGuardadas.CurrentCell.RowIndex;
            var IDVenta = Convert.ToInt32(DGVListaVentasGuardadas.Rows[fila].Cells["ID"].Value);

            //Mostrar venta guardada
            if (e.ColumnIndex == 4)
            {
                Ventas.mostrarVenta = IDVenta;

                this.Close();
            }

            //Eliminar venta guardada
            if (e.ColumnIndex == 5)
            {
                DGVListaVentasGuardadas.ClearSelection();

                MessageBox.Show("Venta a cancelar: " + IDVenta.ToString());
            }
        }

        private void DGVListaVentasGuardadas_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 4)
            {
                DGVListaVentasGuardadas.Cursor = Cursors.Hand;
            }
            else
            {
                DGVListaVentasGuardadas.Cursor = Cursors.Default;
            }
        }
    }
}
