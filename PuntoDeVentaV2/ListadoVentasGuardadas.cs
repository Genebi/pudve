using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        bool con_filtro = false;

        public ListadoVentasGuardadas()
        {
            InitializeComponent();
        }


        private void CargarDatos()
        {
            var servidor = Properties.Settings.Default.Hosting;

            MySqlConnection sql_con;
            MySqlCommand sql_cmd;
            MySqlDataReader dr;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                sql_con = new MySqlConnection("datasource=" + servidor + ";port=6666;username=root;password=;database=pudve;");
            }
            else
            {
                sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            }

            // Miri.
            //Buscar por cliente o folio

            string consulta_buscar_por = "";
            string buscar_por = txt_buscar_por.Text.Trim();

            if(con_filtro == true)
            {
                consulta_buscar_por = "AND (Cliente LIKE '%" + buscar_por + "%' OR Folio LIKE '%" + buscar_por + "%')";
            }

            sql_con.Open();
            sql_cmd = new MySqlCommand($"SELECT * FROM Ventas WHERE IDUsuario = {FormPrincipal.userID} AND Status = 2 " + consulta_buscar_por + " ORDER BY FechaOperacion DESC", sql_con);
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
                    if (!infoCliente.Length.Equals(0))
                    {
                        cliente = infoCliente[0];
                    }
                    else
                    {
                        return;
                    }
                   
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

                Image imgMostrar  = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\reply.png");
                Image imgEliminar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");

                row.Cells["Mostrar"].Value = imgMostrar;
                row.Cells["Eliminar"].Value = imgEliminar;
            }

            DGVListaVentasGuardadas.ClearSelection();

            if (DGVListaVentasGuardadas.Rows.Count != 0)
            {
                DGVListaVentasGuardadas.Focus();

                DGVListaVentasGuardadas.CurrentRow.Selected = true;
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

                // Verificar que todos los productos de la venta guardada esten habilitados
                var productosVenta = mb.ProductosGuardados(IDVenta);

                if (productosVenta.Count > 0)
                {
                    foreach (var producto in productosVenta)
                    {
                        var estaHabilitado = (bool)cn.EjecutarSelect($"SELECT * FROM Productos WHERE ID = {producto.Key} AND IDUsuario = {FormPrincipal.userID} AND Status = 1");

                        if (!estaHabilitado)
                        {
                            var datosProducto = cn.BuscarProducto(producto.Key, FormPrincipal.userID);

                            if (datosProducto.Count() > 0)
                            {
                                var tipo = datosProducto[5];
                                var tipoProducto = string.Empty;

                                if (tipo == "P") { tipoProducto = "Producto"; }
                                if (tipo == "S") { tipoProducto = "Servicio"; }
                                if (tipo == "PQ") { tipoProducto = "Combo"; }

                                MessageBox.Show($"El {tipoProducto} {datosProducto[1]} con código {datosProducto[7]} se encuentra deshabilitado, esto evitará que la venta guardada se cargue.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            Ventas.mostrarVenta = 0;
                            Ventas.idCliente = "0";
                        }
                    }
                }

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
            else if (e.KeyCode == Keys.Enter)
            {
                DGVListaVentasGuardadas_CellClick(this, new DataGridViewCellEventArgs(5, DGVListaVentasGuardadas.CurrentRow.Index));
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DGVListaVentasGuardadas_CellClick(this, new DataGridViewCellEventArgs(6, DGVListaVentasGuardadas.CurrentRow.Index));
            }
        }

        private void ListadoVentasGuardadas_Load(object sender, EventArgs e)
        {
            con_filtro = false;
            CargarDatos();

            // Placeholder del campo buscar por...
            txt_buscar_por.GotFocus += new EventHandler(buscar_por_confoco);
            txt_buscar_por.LostFocus += new EventHandler(buscar_por_sinfoco);
        }

        private void buscar_por(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                con_filtro = true;
                CargarDatos();
            }
        }

        private void buscar_por_confoco(object sender, EventArgs e)
        {
            if (txt_buscar_por.Text == "Buscar por cliente o folio")
            {
                txt_buscar_por.Text = "";
            }
        }

        private void buscar_por_sinfoco(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_buscar_por.Text))
            {
                txt_buscar_por.Text = "Buscar por cliente o folio";
                con_filtro = false;
            }
        }
    }
}
