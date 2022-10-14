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
    public partial class ListadoAnticipos : Form
    {
        public static int obtenerIdAnticipo { get; set; }

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        string[] anticipos = new string[] { };

        int numeroProductos = 0;

        public ListadoAnticipos(int numeroProductos)
        {
            InitializeComponent();

            this.numeroProductos = numeroProductos;
        }

        private void ListadoAnticipos_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            MySqlConnection sql_con;
            MySqlCommand sql_cmd;
            MySqlDataReader dr;

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                sql_con = new MySqlConnection("datasource=" + Properties.Settings.Default.Hosting + ";port=6666;username=root;password=;database=pudve;");
            }
            else
            {
                sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            }

            sql_con.Open();
            if (!string.IsNullOrWhiteSpace(txtbusqueda.Text))
            {
                sql_cmd = new MySqlCommand(cs.BuscarAnticiposPorTexto(txtbusqueda.Text), sql_con);
            }
            else
            {
                sql_cmd = new MySqlCommand($"SELECT * FROM Anticipos WHERE IDUsuario = {FormPrincipal.userID} AND (Status = 1 OR Status = 5)", sql_con);
            }            
            dr = sql_cmd.ExecuteReader();

            DGVListaAnticipos.Rows.Clear();

            //Si la variable no esta vacia eliminamos el ultimo elemento de la cadena "-"
            //Convertimos la cadena en Array y se asigna al Array anticipos
            if (Ventas.listaAnticipos != "")
            {
                var auxiliar = Ventas.listaAnticipos.Remove(Ventas.listaAnticipos.Length - 1);

                var cadena = auxiliar.Split('-');

                anticipos = cadena;
            }
            
            while (dr.Read())
            {
                
                int rowId = DGVListaAnticipos.Rows.Add();

                DataGridViewRow row = DGVListaAnticipos.Rows[rowId];

                row.Cells["ID"].Value = dr.GetValue(dr.GetOrdinal("ID"));
                row.Cells["Concepto"].Value = dr.GetValue(dr.GetOrdinal("Concepto"));
                row.Cells["Importe"].Value = dr.GetValue(dr.GetOrdinal("Importe"));
                row.Cells["Cliente"].Value = dr.GetValue(dr.GetOrdinal("Cliente"));  
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fecha"))).ToString("yyyy-MM-dd HH:mm:ss");

                int seleccionado = Array.IndexOf(anticipos, dr.GetValue(dr.GetOrdinal("ID")).ToString());

                //Si ya fue seleccionado anteriormente agregamos un icono diferente al momento de visualizarlos
                if (seleccionado > -1)
                {
                    Image aplicar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\ban.png");

                    row.Cells["Aplicar"].Value = aplicar;

                    row.Cells["Aplicar"].ToolTipText = "Ya fue aplicado";
                }
                else
                {
                    Image aplicar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\reply.png");

                    row.Cells["Aplicar"].Value = aplicar;

                    row.Cells["Aplicar"].ToolTipText = "Aplicar";
                }

            }

            DGVListaAnticipos.ClearSelection();

            dr.Close();
            sql_con.Close();
            
            //DGVListaAnticipos.Rows[0].Cells["Concepto"].Selected = true;
            if (DGVListaAnticipos.Rows.Count != 0)
            {
                DGVListaAnticipos.Focus();

                //DGVListaAnticipos.CurrentRow.Selected = true;
            }
        }

        private void DGVListaAnticipos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Boton Aplicar
            if (e.ColumnIndex == 5)
            {
                var fila = DGVListaAnticipos.CurrentCell.RowIndex;

                int idAnticipo = Convert.ToInt32(DGVListaAnticipos.Rows[fila].Cells["ID"].Value);
                obtenerIdAnticipo = idAnticipo;

                var form = Application.OpenForms.OfType<Ventas>().FirstOrDefault();
                if (form != null)
                {
                    form.idAnticipoVentas = idAnticipo;
                }

                int seleccionado = Array.IndexOf(anticipos, idAnticipo.ToString());

                //Si el anticipo no ha sido aplicado se hace la operacion normalmente
                if (seleccionado < 0)
                {
                    if (numeroProductos > 0)
                    {
                        Ventas.listaAnticipos += idAnticipo + "-";

                        Ventas.importeAnticipo = float.Parse(DGVListaAnticipos.Rows[fila].Cells["Importe"].Value.ToString());

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se puede aplicar un anticipo a la venta\nya que no hay productos agregados", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            DGVListaAnticipos.ClearSelection();
        }

        private void DGVListaAnticipos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //Aplicar
                if (e.ColumnIndex == 5)
                {
                    DGVListaAnticipos.Cursor = Cursors.Hand;
                }
                else
                {
                    DGVListaAnticipos.Cursor = Cursors.Default;
                }
            }
        }

        private void ListadoAnticipos_Shown(object sender, EventArgs e)
        {
            if (DGVListaAnticipos.Rows.Count == 0)
            {
                var resultadoDialogo = MessageBox.Show("No existen anticipos guardados actualmente.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (resultadoDialogo == DialogResult.OK)
                {
                    this.Close();
                }
            }
        }

        private void ListadoAnticipos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void DGVListaAnticipos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            else if (e.KeyCode == Keys.Enter && !DGVListaAnticipos.Rows.Count.Equals(0))
            {
                DGVListaAnticipos_CellClick(this, new DataGridViewCellEventArgs(5, DGVListaAnticipos.CurrentRow.Index));
            }
        }

        private void txtbusqueda_TextChanged(object sender, EventArgs e)
        {
            if (txtbusqueda.Text.Contains("\'"))
            {
                string producto = txtbusqueda.Text.Replace("\'", ""); ;
                txtbusqueda.Text = producto;
                txtbusqueda.Select(txtbusqueda.Text.Length, 0);
            }
            CargarDatos();
            txtbusqueda.Focus();
            
        }
    }
}
