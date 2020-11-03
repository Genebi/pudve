using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PuntoDeVentaV2
{
    public partial class Empleados : Form
    {
        MetodosBusquedas mb = new MetodosBusquedas();

        // Permisos botones
        int opcion1 = 1; // Nuevo empleado
        int opcion2 = 1; // Editar empleado
        int opcion3 = 1; // Permisos empleado

        public Empleados()
        {
            InitializeComponent();
        }

        private void cargar_empleados(object sender, EventArgs e)
        {
            cargar_lista_empleados();

            if (FormPrincipal.id_empleado > 0)
            {
                var permisos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Empleados");

                opcion1 = permisos[0];
                opcion2 = permisos[1];
                opcion3 = permisos[2];
            }
        }

        public void cargar_lista_empleados()
        {
            MySqlConnection sql_con;
            MySqlCommand sql_cmd;
            MySqlDataReader dr;


            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                sql_con = new MySqlConnection("datasource="+ Properties.Settings.Default.Hosting +";port=6666;username=root;password=;database=mysql;");
            }
            else
            {
                sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            }

            sql_con.Open();


            string cons = $"SELECT * FROM Empleados WHERE IDUsuario='{FormPrincipal.userID}' AND estatus=1";
            sql_cmd = new MySqlCommand(cons, sql_con);
            dr = sql_cmd.ExecuteReader();


            dgv_empleados.Rows.Clear();


            while (dr.Read())
            {
                int fila_id = dgv_empleados.Rows.Add();

                DataGridViewRow fila = dgv_empleados.Rows[fila_id];

                fila.Cells["id"].Value = dr.GetValue(dr.GetOrdinal("ID"));
                fila.Cells["nombre"].Value = dr.GetValue(dr.GetOrdinal("nombre"));
                fila.Cells["usuario"].Value = dr.GetValue(dr.GetOrdinal("usuario"));

                Image editar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\edit.png");
                Image permisos = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\unlock-alt.png");

                fila.Cells["editar"].Value = editar;
                fila.Cells["Permisos"].Value = permisos; 
            }

            dgv_empleados.ClearSelection();
            sql_con.Close();
        }

        private void btn_agregar_empleado_Click(object sender, EventArgs e)
        {
            if (opcion1 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

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

                // Editar empleado
                if (e.ColumnIndex == 3)
                {
                    if (opcion2 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    var empleado = new Agregar_empleado(2, id_empleado);

                    empleado.FormClosed += delegate
                    {
                        cargar_lista_empleados();
                    };

                    empleado.Show();
                }

                // Asignar permisos
                if (e.ColumnIndex == 4) 
                {
                    if (opcion3 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    Agregar_empleado_permisos permisos = new Agregar_empleado_permisos(id_empleado);

                    permisos.ShowDialog();
                }

                dgv_empleados.ClearSelection();
            }
        }
        
    }
}
