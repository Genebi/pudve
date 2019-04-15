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
    public partial class Anticipos : Form
    {
        public Anticipos()
        {
            InitializeComponent();
        }

        private void Anticipos_Load(object sender, EventArgs e)
        {
            cbAnticipos.SelectedIndex = 0;
            cbAnticipos.DropDownStyle = ComboBoxStyle.DropDownList;
            CargarDatos(1);
        }

        private void CargarDatos(int estado = 1)
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + "\\BD\\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT * FROM Anticipos WHERE IDUsuario = {FormPrincipal.userID} AND Status = {estado}", sql_con);
            dr = sql_cmd.ExecuteReader();

            DGVAnticipos.Rows.Clear();

            while (dr.Read())
            {
                int rowId = DGVAnticipos.Rows.Add();

                DataGridViewRow row = DGVAnticipos.Rows[rowId];

                row.Cells["Concepto"].Value = dr.GetValue(dr.GetOrdinal("Concepto"));
                row.Cells["Importe"].Value = dr.GetValue(dr.GetOrdinal("Importe"));
                row.Cells["Cliente"].Value = dr.GetValue(dr.GetOrdinal("Cliente"));
                row.Cells["Empleado"].Value = "Administrador";
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fecha"))).ToString("yyyy-MM-dd HH:mm:ss");
            }

            dr.Close();
            sql_con.Close();
        }

        private void btnNuevoAnticipo_Click(object sender, EventArgs e)
        {
            AgregarAnticipo anticipo = new AgregarAnticipo();

            anticipo.FormClosed += delegate
            {
                MessageBox.Show("Se cerro");
            };

            anticipo.ShowDialog();
        }
    }
}
