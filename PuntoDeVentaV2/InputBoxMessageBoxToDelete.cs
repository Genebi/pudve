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
    public partial class InputBoxMessageBoxToDelete : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        #region llenando concepto dinamico
        private void llenarRegistros()
        {
            using (DataTable dtConceptoHabilitado = cn.CargarDatos(cs.VerificarContenidoDinamicoHabilitado(FormPrincipal.userID)))
            {
                if (!dtConceptoHabilitado.Rows.Count.Equals(0))
                {
                    DGVConceptosHabilitados.Rows.Clear();
                    foreach (DataRow filaDatos in dtConceptoHabilitado.Rows)
                    {
                        int numberOfRows = DGVConceptosHabilitados.Rows.Add();
                        DataGridViewRow row = DGVConceptosHabilitados.Rows[numberOfRows];
                        row.Cells["ID"].Value = filaDatos["ID"].ToString();
                        row.Cells["Concepto"].Value = filaDatos["Concepto"].ToString().Replace("_", " ");
                        row.Cells["Usuario"].Value = filaDatos["Usuario"].ToString();
                        System.Drawing.Image inhabilitar = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");
                        row.Cells["Inhabilitar"].Value = inhabilitar;
                    }
                }
                else if (dtConceptoHabilitado.Rows.Count.Equals(0))
                {
                    DGVConceptosHabilitados.Rows.Clear();
                }
            }

            notSortableDataGridView();
        }

        private void notSortableDataGridView()
        {
            foreach (DataGridViewColumn column in DGVConceptosHabilitados.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        #endregion

        public InputBoxMessageBoxToDelete(/*string _Title, string _DefaultResponse*/)
        {
            InitializeComponent();
        }
        
        private void InputBoxMessageBoxToDelete_Load(object sender, EventArgs e)
        {
            llenarRegistros();
        }

        private void DGVConceptosHabilitados_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(3))
            {
                DGVConceptosHabilitados.Cursor = Cursors.Hand;
            }
            else
            {
                DGVConceptosHabilitados.Cursor = Cursors.Default;
            }
        }

        private void DGVConceptosHabilitados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(3))
            {
                if (e.RowIndex >= 0)
                {
                    var idReg = Convert.ToInt32(DGVConceptosHabilitados.Rows[e.RowIndex].Cells[0].Value.ToString().Replace(" ", "_"));

                    try
                    {
                        cn.EjecutarConsulta(cs.inhabilitarConceptoDinamico(idReg));
                    }
                    catch (MySqlException exMySql)
                    {
                        MessageBox.Show($"Ocurrio una irregularidad al intentar\nInhabilitar Detalle Producto...\nExcepción: " + exMySql.Message.ToString(), "Inhabilitado Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("El detalle: para Inhabilitar no se encuentra en los registros\nExcepción: " + ex.Message.ToString(), "Error al Inhabilitar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    llenarRegistros();
                }
            }
        }

        private void InputBoxMessageBoxToDelete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }
    }
}
