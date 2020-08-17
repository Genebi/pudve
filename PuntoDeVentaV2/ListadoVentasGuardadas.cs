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
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        public string rutaLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public ListadoVentasGuardadas()
        {
            InitializeComponent();

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
            sql_cmd = new SQLiteCommand($"SELECT * FROM Ventas WHERE IDUsuario = {FormPrincipal.userID} AND Status = 2 ORDER BY FechaOperacion DESC", sql_con);
            dr = sql_cmd.ExecuteReader();

            DGVListaVentasGuardadas.Rows.Clear();

            while (dr.Read())
            {
                int idVenta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID")));
                string cliente = "Público General";

                if (Ventas.ventasGuardadas.ToArray().Contains(idVenta))
                {
                    continue;
                }

                var idCliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDCliente")));

                if (idCliente > 0)
                {
                    var infoCliente = mb.ObtenerDatosCliente(idCliente, FormPrincipal.userID);
                    cliente = infoCliente[0];
                    //rfc = infoCliente[1];
                }

                int rowId = DGVListaVentasGuardadas.Rows.Add();

                DataGridViewRow row = DGVListaVentasGuardadas.Rows[rowId];

                row.Cells["ID"].Value = idVenta;
                row.Cells["Folio"].Value = dr.GetValue(dr.GetOrdinal("Folio"));
                row.Cells["Cliente"].Value = cliente;
                row.Cells["IDCliente"].Value = idCliente;
                row.Cells["Importe"].Value = dr.GetValue(dr.GetOrdinal("Total"));
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");

                Image imgMostrar  = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\eye.png");
                Image imgEliminar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");

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
            var IDCliente = DGVListaVentasGuardadas.Rows[fila].Cells["IDCliente"].Value.ToString();

            //Mostrar venta guardada
            if (e.ColumnIndex == 5)
            {
                Ventas.mostrarVenta = IDVenta;
                Ventas.idCliente = IDCliente;
                this.Close();
            }

            //Eliminar venta guardada
            if (e.ColumnIndex == 6)
            {
                DGVListaVentasGuardadas.ClearSelection();

                int resultado = cn.EjecutarConsulta(cs.ActualizarVenta(IDVenta, 3, FormPrincipal.userID));

                if (resultado > 0)
                {
                    DGVListaVentasGuardadas.Rows.RemoveAt(fila);

                    if (DGVListaVentasGuardadas.Rows.Count == 0)
                    {
                        this.Close();
                    }
                }
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

        private void ListadoVentasGuardadas_Shown(object sender, EventArgs e)
        {
            if (DGVListaVentasGuardadas.Rows.Count == 0)
            {
                var resultadoDialogo = MessageBox.Show("No existen ventas guardadas actualmente.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (resultadoDialogo == DialogResult.OK)
                {
                    this.Close();
                }
            }
        }

        private void ListadoVentasGuardadas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void DGVListaVentasGuardadas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
