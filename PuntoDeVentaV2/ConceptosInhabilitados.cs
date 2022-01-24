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
    public partial class ConceptosInhabilitados : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        #region llenando concepto dinamico
        private void llenarRegistros()
        {
            using (DataTable dtConceptoInhabilitado = cn.CargarDatos(cs.VerificarContenidoDinamicoInhabilitado(FormPrincipal.userID)))
            {
                if (!dtConceptoInhabilitado.Rows.Count.Equals(0))
                {
                    DGVConceptosInhabilitados.Rows.Clear();
                    foreach (DataRow filaDatos in dtConceptoInhabilitado.Rows)
                    {
                        int numberOfRows = DGVConceptosInhabilitados.Rows.Add();
                        DataGridViewRow row = DGVConceptosInhabilitados.Rows[numberOfRows];
                        row.Cells["ID"].Value = filaDatos["ID"].ToString();
                        row.Cells["Concepto"].Value = filaDatos["Concepto"].ToString().Replace("_", " ");
                        row.Cells["Usuario"].Value = filaDatos["Usuario"].ToString();
                        System.Drawing.Image habilitar = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\arrow-up.png");
                        row.Cells["Habilitar"].Value = habilitar;
                    }
                }
                else if (dtConceptoInhabilitado.Rows.Count.Equals(0))
                {
                    DGVConceptosInhabilitados.Rows.Clear();
                }
            }
            notSortableDataGridView();
        }

        private void notSortableDataGridView()
        {
            foreach (DataGridViewColumn column in DGVConceptosInhabilitados.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        #endregion

        public ConceptosInhabilitados()
        {
            InitializeComponent();
        }

        private void ConceptosInhabilitados_Load(object sender, EventArgs e)
        {
            llenarRegistros();
        }

        private void DGVConceptosInhabilitados_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(3))
            {
                DGVConceptosInhabilitados.Cursor = Cursors.Hand;
            }
            else
            {
                DGVConceptosInhabilitados.Cursor = Cursors.Default;
            }
        }

        private void DGVConceptosInhabilitados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(3))
            {
                if (e.RowIndex >= 0)
                {
                    var idReg = Convert.ToInt32(DGVConceptosInhabilitados.Rows[e.RowIndex].Cells[0].Value.ToString());

                    try
                    {
                        using (DataTable dtConceptoDinamico = cn.CargarDatos(cs.buscarDatoDinamicoPorIDRegistro(idReg)))
                        {
                            if (dtConceptoDinamico.Rows.Count.Equals(0))
                            {
                                
                            }
                            else if (!dtConceptoDinamico.Rows.Count.Equals(0))
                            {
                                foreach (DataRow drConceptoDinamico in dtConceptoDinamico.Rows)
                                {
                                    string concepto = drConceptoDinamico["concepto"].ToString();
                                    using (DataTable dtFindConcepto = cn.CargarDatos(cs.VerificarDatoDinamico(concepto, FormPrincipal.userID)))
                                    {
                                        if (dtFindConcepto.Rows.Count.Equals(0))
                                        {
                                            cn.EjecutarConsulta(cs.habilitarConceptoDinamico(idReg));
                                        }
                                        else if (!dtFindConcepto.Rows.Equals(0))
                                        {
                                            DialogResult dialogResult = MessageBox.Show("Está acción habilitara un concepto que\nya se encuentra registrado actualmente,\ndesea realmente volver habilitarlo.", "Aviso del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                            if (dialogResult.Equals(DialogResult.Yes))
                                            {
                                                cn.EjecutarConsulta(cs.habilitarConceptoDinamico(idReg));
                                                break;
                                            }
                                            else if (dialogResult.Equals(DialogResult.No))
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (MySqlException exMySql)
                    {
                        MessageBox.Show($"Ocurrio una irregularidad al intentar\nHabilitar Detalle Producto...\nExcepción: " + exMySql.Message.ToString(), "Habilitado Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("El detalle: para Habilitar no se encuentra en los registros\nExcepción: " + ex.Message.ToString(), "Error al Habilitar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    llenarRegistros();
                }
            }
        }

        private void ConceptosInhabilitados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }
    }
}
