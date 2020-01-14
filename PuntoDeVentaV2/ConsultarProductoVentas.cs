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
    public partial class ConsultarProductoVentas : Form
    {
        Conexion cn = new Conexion();

        public ConsultarProductoVentas()
        {
            InitializeComponent();
        }

        private void ConsultarProductoVentas_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
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

            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1", sql_con);
            dr = sql_cmd.ExecuteReader();

            DGVProductos.Rows.Clear();

            while (dr.Read())
            {
                int rowId = DGVProductos.Rows.Add();

                DataGridViewRow row = DGVProductos.Rows[rowId];

                var precio = float.Parse(dr.GetValue(dr.GetOrdinal("Precio")).ToString());
                var tipo_aux = dr.GetValue(dr.GetOrdinal("Tipo")).ToString();
                var tipo = string.Empty;

                if (tipo_aux == "P") { tipo = "Producto"; }
                if (tipo_aux == "S") { tipo = "Servicio"; }
                if (tipo_aux == "PQ") { tipo = "Paquete"; }

                row.Cells["Nombre"].Value = dr.GetValue(dr.GetOrdinal("Nombre"));
                row.Cells["Stock"].Value = dr.GetValue(dr.GetOrdinal("Stock"));
                row.Cells["Precio"].Value = precio.ToString("0.00");
                row.Cells["Clave"].Value = dr.GetValue(dr.GetOrdinal("ClaveInterna"));
                row.Cells["Codigo"].Value = dr.GetValue(dr.GetOrdinal("CodigoBarras"));
                row.Cells["Tipo"].Value = tipo;
            }

            DGVProductos.ClearSelection();

            dr.Close();
            sql_con.Close();
        }

        private void ConsultarProductoVentas_Shown(object sender, EventArgs e)
        {
            txtBuscar.Focus();
        }
    }
}
