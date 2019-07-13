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
    public partial class Proveedores : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public Proveedores()
        {
            InitializeComponent();
        }

        private void Proveedores_Load(object sender, EventArgs e)
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

            var consulta = $"SELECT * FROM Proveedores WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1";

            sql_cmd = new SQLiteCommand(consulta, sql_con);

            dr = sql_cmd.ExecuteReader();

            Image editar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\edit.png");
            Image eliminar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\remove.png");

            DGVProveedores.Rows.Clear();

            while (dr.Read())
            {
                int rowId = DGVProveedores.Rows.Add();

                DataGridViewRow row = DGVProveedores.Rows[rowId];

                row.Cells["ID"].Value = dr.GetValue(dr.GetOrdinal("ID"));
                row.Cells["Nombre"].Value = dr.GetValue(dr.GetOrdinal("Nombre"));
                row.Cells["RFC"].Value = dr.GetValue(dr.GetOrdinal("RFC"));
                row.Cells["Email"].Value = dr.GetValue(dr.GetOrdinal("Email"));
                row.Cells["Telefono"].Value = dr.GetValue(dr.GetOrdinal("Telefono"));
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                row.Cells["Editar"].Value = editar;
                row.Cells["Eliminar"].Value = eliminar;
            }

            DGVProveedores.ClearSelection();

            dr.Close();
            sql_con.Close();
        }

        private void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            AgregarProveedor ap = new AgregarProveedor();

            ap.FormClosed += delegate
            {
                CargarDatos();
            };

            ap.ShowDialog();
        }

        private void DGVProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idProveedor = Convert.ToInt32(DGVProveedores.Rows[e.RowIndex].Cells["ID"].Value);

                //Editar
                if (e.ColumnIndex == 6)
                {
                    AgregarProveedor editar = new AgregarProveedor(2, idProveedor);

                    editar.FormClosed += delegate
                    {
                        CargarDatos();
                    };

                    editar.ShowDialog();
                }

                //Eliminar
                if (e.ColumnIndex == 7)
                {
                    var respuesta = MessageBox.Show("¿Estás seguro de deshabilitar este proveedor?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes)
                    {
                        string[] datos = new string[] { idProveedor.ToString(), FormPrincipal.userID.ToString() };

                        int resultado = cn.EjecutarConsulta(cs.GuardarProveedor(datos, 2));

                        if (resultado > 0)
                        {
                            CargarDatos();
                        }
                    }
                }

                DGVProveedores.ClearSelection();
            }
        }

        private void DGVProveedores_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex >= 6)
                {
                    DGVProveedores.Cursor = Cursors.Hand;
                }
            }
        }

        private void DGVProveedores_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex >= 6)
                {
                    DGVProveedores.Cursor = Cursors.Default;
                }
            }
        }
    }
}
