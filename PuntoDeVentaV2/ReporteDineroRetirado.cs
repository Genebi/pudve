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
    public partial class ReporteDineroRetirado : Form
    {
        DateTime fecha = Convert.ToDateTime("0001-01-01 00:00:00");
        public ReporteDineroRetirado(DateTime fecha)
        {
            InitializeComponent();

            this.fecha = fecha;
        }

        private void ReporteDineroRetirado_Load(object sender, EventArgs e)
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
            sql_cmd = new SQLiteCommand($"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'retiro' AND FechaOperacion > '{fecha}'", sql_con);
            dr = sql_cmd.ExecuteReader();

            DGVRetiros.Rows.Clear();

            while (dr.Read())
            {
                int rowId = DGVRetiros.Rows.Add();

                DataGridViewRow row = DGVRetiros.Rows[rowId];

                row.Cells["Empleado"].Value = "ADMIN";
                row.Cells["Efectivo"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Efectivo"))).ToString("0.00");
                row.Cells["Tarjeta"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Tarjeta"))).ToString("0.00");
                row.Cells["Vales"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vales"))).ToString("0.00");
                row.Cells["Cheque"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cheque"))).ToString("0.00");
                row.Cells["Trans"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Transferencia"))).ToString("0.00");
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
            }

            dr.Close();
            sql_con.Close();
        }

        private void ReporteDineroRetirado_Shown(object sender, EventArgs e)
        {
            if (DGVRetiros.Rows.Count == 0)
            {
                var resultado = MessageBox.Show("No existen retiros recientes", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (resultado == DialogResult.OK)
                {
                    Dispose();
                }
            }
        }
    }
}
