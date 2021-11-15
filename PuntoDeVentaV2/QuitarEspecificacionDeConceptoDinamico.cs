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
    public partial class QuitarEspecificacionDeConceptoDinamico : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public string getChkName { get; set; }

        public QuitarEspecificacionDeConceptoDinamico()
        {
            InitializeComponent();
        }

        private void QuitarEspecificacionDeConceptoDinamico_Load(object sender, EventArgs e)
        {
            llenarRegistros();
        }

        private void llenarRegistros()
        {
            lblConceptoDinamico.Text += $"{getChkName}".Replace("_", " ").Trim();

            using (DataTable dtEspecificacionesHabilitados = cn.CargarDatos(cs.especificacionesDetalleDinamicoParaQuitar(getChkName)))
            {
                if (!dtEspecificacionesHabilitados.Rows.Count.Equals(0))
                {
                    DGVEspecificacionesActivas.Rows.Clear();
                    foreach (DataRow item in dtEspecificacionesHabilitados.Rows)
                    {
                        int numeroDeFila = DGVEspecificacionesActivas.Rows.Add();
                        DataGridViewRow fila = DGVEspecificacionesActivas.Rows[numeroDeFila];
                        fila.Cells["ID"].Value = item["ID"].ToString();
                        fila.Cells["Concepto"].Value = item["Concepto"].ToString().Replace("_", " ");
                        fila.Cells["Usuario"].Value = item["Usuario"].ToString();
                        Image inhabilitar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");
                        fila.Cells["Inhabilitar"].Value = inhabilitar;
                    }
                }
                else if (dtEspecificacionesHabilitados.Rows.Count.Equals(0))
                {
                    DGVEspecificacionesActivas.Rows.Clear();
                }
            }

            notSortableDataGridView();
        }

        private void notSortableDataGridView()
        {
            foreach (DataGridViewColumn column in DGVEspecificacionesActivas.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void DGVEspecificacionesActivas_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(3))
            {
                DGVEspecificacionesActivas.Cursor = Cursors.Hand;
            }
            else
            {
                DGVEspecificacionesActivas.Cursor = Cursors.Default;
            }
        }

        private void DGVEspecificacionesActivas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(3))
            {
                if (e.RowIndex >= 0)
                {
                    var idReg = Convert.ToInt32(DGVEspecificacionesActivas.Rows[e.RowIndex].Cells[0].Value.ToString().Replace(" ", "_"));

                    try
                    {
                        cn.EjecutarConsulta(cs.inhabilitarEspecificacionConceptoDinamico(idReg));
                    }
                    catch (MySqlException exMySql)
                    {
                        MessageBox.Show($"Ocurrio una irregularidad al intentar\nInhabilitar la especificación...\nExcepción: " + exMySql.Message.ToString(), "Inhabilitado Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("El detalle: para  la especificación no se encuentra en los registros\nExcepción: " + ex.Message.ToString(), "Error al Inhabilitar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    llenarRegistros();
                }
            }
        }
    }
}
