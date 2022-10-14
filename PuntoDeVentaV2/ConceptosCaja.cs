using MySql.Data.MySqlClient;
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

        private string origen;
        public static string id { get; set; }

        public static string pasarOrigen { get; set; }

        public ConceptosCaja(string origen)
        {
            InitializeComponent();

            this.origen = origen;
        }

        private void ConceptosCaja_Load(object sender, EventArgs e)
        {
            CargarDatos();

            if (rbHabilitados.Checked)
            {
                DGVConceptos.Columns["Habilitar"].Visible = false;
            }
        }

        private void CargarDatos(int status = 1)
        {
            var servidor = Properties.Settings.Default.Hosting;

            MySqlConnection sql_con;
            MySqlCommand sql_cmd;
            MySqlDataReader dr;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                sql_con = new MySqlConnection($"datasource={servidor};port=6666;username=root;password=;database=pudve;");
            }
            else
            {
                sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            }

            sql_con.Open();
            sql_cmd = new MySqlCommand($"SELECT * FROM ConceptosDinamicos WHERE IDUsuario = {FormPrincipal.userID} AND Origen = '{origen}' AND Status = {status} ORDER BY FechaOperacion ASC", sql_con);
            dr = sql_cmd.ExecuteReader();

            DGVConceptos.Rows.Clear();

            while (dr.Read())
            {
                int rowId = DGVConceptos.Rows.Add();

                DataGridViewRow row = DGVConceptos.Rows[rowId];

                //Image imgHabilitar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\level-up.png");
                //Image imgDeshabilitar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\level-down.png");
                System.Drawing.Image imgDeshabilitar = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");
                System.Drawing.Image imgHabilitar = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\arrow-up.png");

                row.Cells["ID"].Value = dr.GetValue(dr.GetOrdinal("ID"));
                row.Cells["Concepto"].Value = dr.GetValue(dr.GetOrdinal("Concepto"));
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                row.Cells["Habilitar"].Value = imgHabilitar;
                row.Cells["Deshabilitar"].Value = imgDeshabilitar;
            }

            dr.Close();
            sql_con.Close();

            DGVConceptos.ClearSelection();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //var concepto = txtConcepto.Text.Trim();
            //var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //if (string.IsNullOrWhiteSpace(concepto))
            //{
            //    MessageBox.Show("Ingrese el nombre del concepto", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //var consulta = "INSERT INTO ConceptosDinamicos (IDUsuario, Concepto, Origen, FechaOperacion)";
            //    consulta += $"VALUES ('{FormPrincipal.userID}', '{concepto}', '{origen}', '{fechaOperacion}')";

            //var resultado = cn.EjecutarConsulta(consulta);

            //if (resultado > 0)
            //{
            //    txtConcepto.Text = string.Empty;
            //    txtConcepto.Focus();
            //    CargarDatos();
            //}

            pasarOrigen = origen;

            //string agregarName = string.Empty.ToUpper();
            ////agregarName = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el Concepto", "Agregar Concepto","".ToUpper(), 500, 300);
            //if (!string.IsNullOrEmpty(agregarName))
            //{
            //    //var mensaje =  MessageBox.Show($"¿Desea agregar {agregarName.ToUpper()}?", "Mensaje de Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //        var concepto = agregarName.ToUpper().Trim();
            //        var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //        if (string.IsNullOrWhiteSpace(concepto))
            //        {
            //            MessageBox.Show("Ingrese el nombre del concepto", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return;
            //        }

            //        var consulta = "INSERT INTO ConceptosDinamicos (IDUsuario, Concepto, Origen, FechaOperacion)";
            //        consulta += $"VALUES ('{FormPrincipal.userID}', '{concepto}', '{origen}', '{fechaOperacion}')";

            //        var resultado = cn.EjecutarConsulta(consulta);

            //        if (resultado > 0)
            //        {
            //            //txtConcepto.Text = string.Empty;
            //            //txtConcepto.Focus();
            //            CargarDatos();
            //        }
            //}
            AgregarConcepto addConcepto = new AgregarConcepto(origen);

            addConcepto.FormClosed += delegate
            {
                if (AgregarConcepto.empty != 0)
                {
                    var resultado = cn.EjecutarConsulta(AgregarConcepto.query);

                    if (resultado > 0)
                    {
                        //txtConcepto.Text = string.Empty;
                        //txtConcepto.Focus();
                        CargarDatos();
                    }
                }
            };
            txtConcepto.Focus();
            addConcepto.ShowDialog();
        }

        private void ConceptosCaja_Shown(object sender, EventArgs e)
        {
            txtConcepto.Focus();
        }

        private void txtConcepto_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtConcepto.Text.Equals(string.Empty))
            {
                CargarDatos();
            }
            else if (!txtConcepto.Text.Equals(string.Empty))
            {
                if (e.KeyData == Keys.Enter)
                {
                    //btnAgregar.PerformClick();
                    btnBuscar.PerformClick();
                }
                else if (e.KeyCode == Keys.Down)
                {
                    DGVConceptos.Focus();
                }
            } 
        }

        private void DGVConceptos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var fila = DGVConceptos.CurrentCell.RowIndex;
                var id = Convert.ToInt32(DGVConceptos.Rows[fila].Cells["ID"].Value);

                // Habilitar
                if (e.ColumnIndex == 3)
                {
                    var respuesta = MessageBox.Show("¿Estás seguro de habilitar este concepto?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes)
                    {
                        cn.EjecutarConsulta($"UPDATE ConceptosDinamicos SET Status = 1 WHERE ID = {id} AND IDUsuario = {FormPrincipal.userID}");
                        CargarDatos(0);
                    }
                }

                // Deshabilitar
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

        private void rbHabilitados_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHabilitados.Checked)
            {
                DGVConceptos.Columns["Habilitar"].Visible = false;
                DGVConceptos.Columns["Deshabilitar"].Visible = true;
                CargarDatos();
            }
        }

        private void rbDeshabilitados_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDeshabilitados.Checked)
            {
                DGVConceptos.Columns["Deshabilitar"].Visible = false;
                DGVConceptos.Columns["Habilitar"].Visible = true;
                CargarDatos(0);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var texto = txtConcepto.Text;
            var status = 2;

            if (rbHabilitados.Checked)
            {
                status = 1;

            }
            else if (rbDeshabilitados.Checked)
            {
                status = 0;
            }

            Image imgHabilitar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\level-up.png");
            Image imgDeshabilitar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\level-down.png");

            if (!string.IsNullOrEmpty(texto))
            {
                var cadena = texto.Trim();
                char delimitar = (' ');

                //Image imgHabilitar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\level-up.png");

                string[] separarPalabras = cadena.Split(delimitar);

                foreach (var iterar in separarPalabras)
                {
                    using (var buscarDatos = cn.CargarDatos($"SELECT ID, Concepto, FechaOperacion FROM conceptosDinamicos WHERE IDUsuario = '{FormPrincipal.userID}' AND Origen = '{origen}'  AND Status = '{status}' AND Concepto LIKE '%{iterar}%'"))
                    {
                        if (!buscarDatos.Rows.Count.Equals(0))
                        {
                            DGVConceptos.Rows.Clear();
                            foreach (DataRow llenarCampos in buscarDatos.Rows)
                            {
                                int number_of_rows = DGVConceptos.Rows.Add();
                                DataGridViewRow row = DGVConceptos.Rows[number_of_rows];

                                if (status == 1)
                                {
                                    row.Cells["ID"].Value = llenarCampos["ID"].ToString();
                                    row.Cells["Concepto"].Value = llenarCampos["Concepto"].ToString();
                                    row.Cells["Fecha"].Value = llenarCampos["FechaOperacion"].ToString();
                                    row.Cells["Habilitar"].Value = imgHabilitar;
                                    row.Cells["Deshabilitar"].Value = imgDeshabilitar;

                                    //DGVConceptos.Rows.Add(llenarCampos["ID"].ToString(), llenarCampos["Concepto"].ToString(), llenarCampos["FechaOperacion"]);
                                }
                                else if (status == 0)
                                {
                                    row.Cells["ID"].Value = llenarCampos["ID"].ToString();
                                    row.Cells["Concepto"].Value = llenarCampos["Concepto"].ToString();
                                    row.Cells["Fecha"].Value = llenarCampos["FechaOperacion"].ToString();
                                    row.Cells["Habilitar"].Value = imgHabilitar;
                                    row.Cells["Deshabilitar"].Value = imgDeshabilitar;

                                    //DGVConceptos.Rows.Add(llenarCampos["ID"].ToString(), llenarCampos["Concepto"].ToString(), llenarCampos["FechaOperacion"]);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show($"No se encontraron conceptos con: {txtConcepto.Text}", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Ingrese el nombre de algun concepto", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConcepto.Focus();
                //using (var buscarDatos = cn.CargarDatos($"SELECT ID, Concepto, FechaOperacion FROM conceptosDinamicos WHERE IDUsuario = '{FormPrincipal.userID}' AND Origen = 'CAJA' AND Status = '{status}'"))
                //{
                //    if (!buscarDatos.Rows.Count.Equals(0))
                //    {
                //        DGVConceptos.Rows.Clear();
                //        foreach (DataRow llenarCampos in buscarDatos.Rows)
                //        {
                //            int number_of_rows = DGVConceptos.Rows.Add();
                //            DataGridViewRow row = DGVConceptos.Rows[number_of_rows];

                //            row.Cells["ID"].Value = llenarCampos["ID"].ToString();
                //            row.Cells["Concepto"].Value = llenarCampos["Concepto"].ToString();
                //            row.Cells["Fecha"].Value = llenarCampos["FechaOperacion"].ToString();
                //            row.Cells["Habilitar"].Value = imgHabilitar;
                //            row.Cells["Deshabilitar"].Value = imgDeshabilitar;

                //            //DGVConceptos.Rows.Add(llenarCampos["ID"].ToString(), llenarCampos["Concepto"].ToString(), llenarCampos["FechaOperacion"]);
                //        }
                //    }
                //}
            }
        }

        private void DGVConceptos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ejecutarAccion();
            }
            else if (e.KeyCode == Keys.Up && DGVConceptos.CurrentRow.Index == 0)
            {
                txtConcepto.Focus();
            }
        }

        private void ejecutarAccion()
        {
            id = DGVConceptos.Rows[DGVConceptos.CurrentRow.Index].Cells[0].Value.ToString();

            this.Dispose();
        }

        private void DGVConceptos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ejecutarAccion();
        }

        private void ConceptosCaja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }

        private void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            if (txtConcepto.Text.Contains("\'"))
            {
                string producto = txtConcepto.Text.Replace("\'", ""); ;
                txtConcepto.Text = producto;
                txtConcepto.Select(txtConcepto.Text.Length, 0);
            }
        }
    }
}
