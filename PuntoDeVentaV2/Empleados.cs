using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace PuntoDeVentaV2
{
    public partial class Empleados : Form
    {
        public Empleados()
        {
            InitializeComponent();
        }

        private void cargar_empleados(object sender, EventArgs e)
        {
            cargar_lista_empleados();
        }

        public void cargar_lista_empleados()
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


            string cons = $"SELECT * FROM Empleados WHERE IDUsuario='{FormPrincipal.userID}' AND estatus=1";
            sql_cmd = new SQLiteCommand(cons, sql_con);
            dr = sql_cmd.ExecuteReader();


            dgv_empleados.Rows.Clear();


            while (dr.Read())
            {
                int fila_id = dgv_empleados.Rows.Add();

                DataGridViewRow fila = dgv_empleados.Rows[fila_id];

                fila.Cells["id"].Value = dr.GetValue(dr.GetOrdinal("ID"));
                fila.Cells["nombre"].Value = dr.GetValue(dr.GetOrdinal("nombre"));
                fila.Cells["usuario"].Value = dr.GetValue(dr.GetOrdinal("usuario"));

                Image permisos = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\unlock-alt.png");
                
                fila.Cells["Permisos"].Value = permisos; 
            }

            dgv_empleados.ClearSelection();
            sql_con.Close();
        }

        private void btn_agregar_empleado_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Agregar_empleado>().Count() == 1)
            {
                Application.OpenForms.OfType<Agregar_empleado>().First().BringToFront();
            }
            else
            {
                Agregar_empleado agregar_emp = new Agregar_empleado();

                agregar_emp.FormClosed += delegate
                {
                    cargar_lista_empleados();
                };

                agregar_emp.Show();
            }
        }

        private void cursor_en_icono(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 & e.ColumnIndex >= 3)
            {
                dgv_empleados.Cursor = Cursors.Hand;
            }
        }

        private void cursor_no_icono(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 & e.ColumnIndex >= 3)
            {
                dgv_empleados.Cursor = Cursors.Default;
            }
        }

        private void click_en_icono(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                int id_empleado = Convert.ToInt32(dgv_empleados.Rows[e.RowIndex].Cells["id"].Value);

                // Asignar permisos

                if (e.ColumnIndex == 4) 
                {
                    Agregar_empleado_permisos permisos = new Agregar_empleado_permisos(id_empleado);
                    permisos.ShowDialog();
                }


                dgv_empleados.ClearSelection();
            }
        }
    }
}
