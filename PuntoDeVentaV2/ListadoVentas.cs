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
    public partial class ListadoVentas : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();


        public static bool abrirNuevaVenta = false;
        public ListadoVentas()
        {
            InitializeComponent();
        }

        private void ListadoVentas_Load(object sender, EventArgs e)
        {
            CargarDatos();

            cbVentas.SelectedIndex = 0;
            cbTipoVentas.SelectedIndex = 0;
            cbVentas.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTipoVentas.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void CargarDatos(int estado = 1)
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + "\\BD\\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT * FROM Ventas WHERE Status = '{estado}' AND IDUsuario = '{FormPrincipal.userID}'", sql_con);
            dr = sql_cmd.ExecuteReader();

            DGVListadoVentas.Rows.Clear();

            while (dr.Read())
            {
                int rowId = DGVListadoVentas.Rows.Add();

                DataGridViewRow row = DGVListadoVentas.Rows[rowId];

                row.Cells["Cliente"].Value = "Público General";
                row.Cells["RFC"].Value = "XAXX010101000";
                row.Cells["Subtotal"].Value = dr.GetValue(dr.GetOrdinal("Subtotal"));
                row.Cells["IVA"].Value = dr.GetValue(dr.GetOrdinal("IVA16"));
                row.Cells["Total"].Value = dr.GetValue(dr.GetOrdinal("Total"));
                row.Cells["Folio"].Value = dr.GetValue(dr.GetOrdinal("Folio"));
                row.Cells["Serie"].Value = dr.GetValue(dr.GetOrdinal("Serie"));
                row.Cells["Pago"].Value = dr.GetValue(dr.GetOrdinal("MetodoPago"));
                row.Cells["Empleado"].Value = dr.GetValue(dr.GetOrdinal("IDEmpleado"));
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
            }

            dr.Close();
            sql_con.Close();
        }

        private void btnBuscarVentas_Click(object sender, EventArgs e)
        {
            string fechaInicial = dpFechaInicial.Value.ToString("yyyy-MM-dd");
            string fechaFinal   = dpFechaFinal.Value.ToString("yyyy-MM-dd");

            MessageBox.Show(fechaInicial + " " + fechaFinal);
        }

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            Ventas venta = new Ventas();

            venta.Disposed += delegate
            {
                AbrirVentanaVenta();
                CargarDatos();
            };

            venta.ShowDialog();
        }


        private void AbrirVentanaVenta()
        {
            if (abrirNuevaVenta)
            {
                abrirNuevaVenta = false;
                btnNuevaVenta.PerformClick();
            }
        }

        private void cbTipoVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = cbTipoVentas.SelectedIndex;

            //Pagadas
            if (indice == 0) { CargarDatos(); }

            //Guardadas
            if (indice == 2) { CargarDatos(2); }

            //Canceladas
            if (indice == 3) { CargarDatos(3); }
        }
    }
}
