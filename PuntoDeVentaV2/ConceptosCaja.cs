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
    public partial class ConceptosCaja : Form
    {
        Conexion cn = new Conexion();

        public ConceptosCaja()
        {
            InitializeComponent();
        }

        private void ConceptosCaja_Load(object sender, EventArgs e)
        {
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
            sql_cmd = new SQLiteCommand($"SELECT * FROM ConceptosDinamicos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 ORDER BY FechaOperacion ASC", sql_con);
            dr = sql_cmd.ExecuteReader();

            DGVConceptos.Rows.Clear();

            while (dr.Read())
            {
                int rowId = DGVConceptos.Rows.Add();

                DataGridViewRow row = DGVConceptos.Rows[rowId];

                Image imgEditar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\pencil.png");
                Image imgEliminar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");

                row.Cells["ID"].Value = dr.GetValue(dr.GetOrdinal("ID"));
                row.Cells["Concepto"].Value = dr.GetValue(dr.GetOrdinal("Concepto"));
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                row.Cells["Editar"].Value = imgEditar;
                row.Cells["Eliminar"].Value = imgEliminar;
            }

            dr.Close();
            sql_con.Close();

            DGVConceptos.ClearSelection();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var concepto = txtConcepto.Text.Trim();
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (string.IsNullOrWhiteSpace(concepto))
            {
                MessageBox.Show("Ingrese el nombre del concepto", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var consulta = "INSERT INTO ConceptosDinamicos (IDUsuario, Concepto, Origen, FechaOperacion)";
                consulta += $"VALUES ('{FormPrincipal.userID}', '{concepto}', 'CAJA', '{fechaOperacion}')";

            var resultado = cn.EjecutarConsulta(consulta);

            if (resultado > 0)
            {
                txtConcepto.Text = string.Empty;
                txtConcepto.Focus();
                CargarDatos();
            }
        }

        private void ConceptosCaja_Shown(object sender, EventArgs e)
        {
            txtConcepto.Focus();
        }

        private void txtConcepto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAgregar.PerformClick();
            }
        }

        private void DGVConceptos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var fila = DGVConceptos.CurrentCell.RowIndex;
                var id = Convert.ToInt32(DGVConceptos.Rows[fila].Cells["ID"].Value);

                // Editar
                if (e.ColumnIndex == 3)
                {
                    var concepto = DGVConceptos.Rows[fila].Cells["Concepto"].Value.ToString();

                    using (var editar = new EditarConceptoDinamico(concepto))
                    {
                        var respuesta = editar.ShowDialog();

                        if (respuesta == DialogResult.OK)
                        {
                            cn.EjecutarConsulta($"UPDATE ConceptosDinamicos SET Concepto = '{editar.nuevoConcepto}' WHERE ID = {id} AND IDUsuario = {FormPrincipal.userID}");
                            CargarDatos();
                        }
                    }
                }

                // Eliminar
                if (e.ColumnIndex == 4)
                {
                    var respuesta = MessageBox.Show("¿Estás seguro de deshabilitar este concepto?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes)
                    {
                        cn.EjecutarConsulta($"UPDATE ConceptosDinamicos SET Status = 0 WHERE ID = {id} AND IDUsuario = {FormPrincipal.userID}");
                        CargarDatos();
                    }
                }

                DGVConceptos.ClearSelection();
            }
        }

        private void DGVConceptos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 3)
            {
                DGVConceptos.Cursor = Cursors.Hand;
            }
            else
            {
                DGVConceptos.Cursor = Cursors.Default;
            }
        }
    }
}
