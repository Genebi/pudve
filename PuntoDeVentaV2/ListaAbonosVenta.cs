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
    public partial class ListaAbonosVenta : Form
    {
        private int idVenta = 0;
        public ListaAbonosVenta(int idVenta)
        {
            InitializeComponent();

            this.idVenta = idVenta;
        }


        private void ListaAbonosVenta_Load(object sender, EventArgs e)
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

            var consulta = $"SELECT * FROM Abonos WHERE IDVenta = {idVenta} AND IDUsuario = {FormPrincipal.userID} ORDER BY FechaOperacion DESC";

            sql_cmd = new SQLiteCommand(consulta, sql_con);

            dr = sql_cmd.ExecuteReader();

            DGVAbonos.Rows.Clear();

            while (dr.Read())
            {
                int rowId = DGVAbonos.Rows.Add();

                DataGridViewRow row = DGVAbonos.Rows[rowId];

                row.Cells["Efectivo"].Value = Modificar(dr.GetValue(dr.GetOrdinal("Efectivo")).ToString());
                row.Cells["Tarjeta"].Value = Modificar(dr.GetValue(dr.GetOrdinal("Tarjeta")).ToString());
                row.Cells["Vales"].Value = Modificar(dr.GetValue(dr.GetOrdinal("Vales")).ToString());
                row.Cells["Cheque"].Value = Modificar(dr.GetValue(dr.GetOrdinal("Cheque")).ToString());
                row.Cells["Trans"].Value = Modificar(dr.GetValue(dr.GetOrdinal("Transferencia")).ToString());
                row.Cells["Total"].Value = Modificar(dr.GetValue(dr.GetOrdinal("Total")).ToString());
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
            }

            dr.Close();
            sql_con.Close();

            DGVAbonos.ClearSelection();
        }

        private string Modificar(string cadena)
        {
            var cantidad = Convert.ToDecimal(cadena).ToString("0.00");

            return cantidad;
        }
    }
}
