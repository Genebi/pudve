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
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public static string ticketGenerado = string.Empty;

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

        private void CargarDatos(int estado = 1, int tipo = 0)
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + "\\BD\\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();

            var consulta = string.Empty;

            //Normal
            if (tipo == 0)
            {
                consulta = $"SELECT * FROM Anticipos WHERE IDUsuario = {FormPrincipal.userID} AND Status = {estado}";
            }

            //Con fechas de busqueda
            if (tipo == 1)
            {
                var fechaInicio = dpFechaInicial.Text;
                var fechaFinal = dpFechaFinal.Text;

                consulta = $"SELECT * FROM Anticipos WHERE IDUsuario = {FormPrincipal.userID} AND Status = {estado} AND DATE(Fecha) BETWEEN '{fechaInicio}' AND '{fechaFinal}'";
            }

            sql_cmd = new SQLiteCommand(consulta, sql_con);

            dr = sql_cmd.ExecuteReader();

            DGVAnticipos.Rows.Clear();

            while (dr.Read())
            {
                int rowId = DGVAnticipos.Rows.Add();

                DataGridViewRow row = DGVAnticipos.Rows[rowId];

                row.Cells["ID"].Value = dr.GetValue(dr.GetOrdinal("ID"));
                row.Cells["Concepto"].Value = dr.GetValue(dr.GetOrdinal("Concepto"));
                row.Cells["Importe"].Value = dr.GetValue(dr.GetOrdinal("Importe"));
                row.Cells["Cliente"].Value = dr.GetValue(dr.GetOrdinal("Cliente"));
                row.Cells["Empleado"].Value = "Administrador";
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Fecha"))).ToString("yyyy-MM-dd HH:mm:ss");

                Image ticket = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\ticket.png");

                row.Cells["Ticket"].Value = ticket;
                row.Cells["Ticket"].ToolTipText = "Generar ticket";

                var status = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Status")));

                if (status == 1)
                {
                    Image deshabilitar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\ban.png");

                    row.Cells["Status"].Value = deshabilitar;
                    row.Cells["Status"].ToolTipText = "Deshabilitar";
                }
                else if (status == 2)
                {
                    Image habilitar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\check.png");

                    row.Cells["Status"].Value = habilitar;
                    row.Cells["Status"].ToolTipText = "Habilitar";
                }
                else
                {
                    Bitmap sinImagen = new Bitmap(1, 1);
                    sinImagen.SetPixel(0, 0, Color.White);

                    row.Cells["Status"].Value = sinImagen;
                }
            }

            DGVAnticipos.ClearSelection();

            dr.Close();
            sql_con.Close();
        }

        private void btnNuevoAnticipo_Click(object sender, EventArgs e)
        {
            AgregarAnticipo anticipo = new AgregarAnticipo();

            anticipo.FormClosed += delegate
            {
                CargarDatos(1);
            };

            anticipo.ShowDialog();
        }

        private void DGVAnticipos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex >= 6)
                {
                    DGVAnticipos.Cursor = Cursors.Hand;
                }
                else
                {
                    DGVAnticipos.Cursor = Cursors.Default;
                }
            }
        }

        private void cbAnticipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedIndex >= 0)
            {
                CargarDatos(cb.SelectedIndex + 1);
            }
        }

        private void DGVAnticipos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var fila = DGVAnticipos.CurrentCell.RowIndex;

            //Generar ticket
            if (e.ColumnIndex == 6)
            {
                //int idAnticipo = Convert.ToInt32(DGVAnticipos.Rows[fila].Cells["ID"].Value);
                ticketGenerado = "ticket_venta_6.pdf";

                VisualizadorTickets vt = new VisualizadorTickets();

                vt.FormClosed += delegate
                {
                    vt.Dispose();
                };

                vt.ShowDialog();

                ticketGenerado = string.Empty;
            }

            //Habilitar/Deshabilitar
            if (e.ColumnIndex == 7)
            {
                var indice = cbAnticipos.SelectedIndex;

                if (indice < 2)
                {
                    int idAnticipo = Convert.ToInt32(DGVAnticipos.Rows[fila].Cells["ID"].Value);

                    //Deshabilitar
                    if (indice == 0)
                    {
                        cn.EjecutarConsulta(cs.CambiarStatusAnticipo(2, idAnticipo, FormPrincipal.userID));
                    }

                    //Habilitar
                    if (indice == 1)
                    {
                        cn.EjecutarConsulta(cs.CambiarStatusAnticipo(1, idAnticipo, FormPrincipal.userID));
                    }

                    CargarDatos(cbAnticipos.SelectedIndex + 1);
                }
            }

            DGVAnticipos.Cursor = Cursors.Default;
            DGVAnticipos.ClearSelection();
        }

        private void btnBuscarAnticipos_Click(object sender, EventArgs e)
        {
            var status = cbAnticipos.SelectedIndex;

            CargarDatos(status + 1, 1);
        }
    }
}
