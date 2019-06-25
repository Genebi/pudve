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
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }

        private void Clientes_Load(object sender, EventArgs e)
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

                row.Cells["ID"].Value = dr.GetValue(dr.GetOrdinal("ID"));
                row.Cells["RFC"].Value = dr.GetValue(dr.GetOrdinal("RFC"));
                row.Cells["Cliente"].Value = dr.GetValue(dr.GetOrdinal("RazonSocial"));
                row.Cells["NombreComercial"].Value = dr.GetValue(dr.GetOrdinal("NombreComercial"));
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
            }

            DGVClientes.ClearSelection();

            dr.Close();
            sql_con.Close();
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            AgregarCliente cliente = new AgregarCliente();

            cliente.FormClosed += delegate
            {
                CargarDatos();
            };

            cliente.ShowDialog();
        }
    }
}
