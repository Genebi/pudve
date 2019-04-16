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
    public partial class ListadoAnticipos : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        string[] anticipos = new string[] { };

        public ListadoAnticipos()
        {
            InitializeComponent();
            CargarDatos();
        }

        private void CargarDatos()
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + "\\BD\\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT * FROM Anticipos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1", sql_con);
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
                    Image aplicar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\ban.png");

                    row.Cells["Aplicar"].Value = aplicar;

                    row.Cells["Aplicar"].ToolTipText = "Ya fue aplicado";
                }
                else
                {
                    Image aplicar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\reply.png");

                    row.Cells["Aplicar"].Value = aplicar;

                    row.Cells["Aplicar"].ToolTipText = "Aplicar";
                }
            }

            DGVListaAnticipos.ClearSelection();

            dr.Close();
            sql_con.Close();
        }

        private void DGVListaAnticipos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Boton Aplicar
            if (e.ColumnIndex == 5)
            {
                var fila = DGVListaAnticipos.CurrentCell.RowIndex;

                int idAnticipo = Convert.ToInt32(DGVListaAnticipos.Rows[fila].Cells["ID"].Value);

                int seleccionado = Array.IndexOf(anticipos, idAnticipo.ToString());

                //Si el anticipo no ha sido aplicado se hace la operacion normalmente
                if (seleccionado < 0)
                {
                    Ventas.listaAnticipos += idAnticipo + "-";

                    Ventas.importeAnticipo = float.Parse(DGVListaAnticipos.Rows[fila].Cells["Importe"].Value.ToString());

                    this.Close();
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
    }
}
