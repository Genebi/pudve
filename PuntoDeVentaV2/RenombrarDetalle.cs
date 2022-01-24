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
    public partial class RenombrarDetalle : Form
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
                    DGVConceptosRenombrar.Rows.Clear();
                    foreach (DataRow filaDatos in dtConceptoHabilitado.Rows)
                    {
                        int numberOfRows = DGVConceptosRenombrar.Rows.Add();
                        DataGridViewRow row = DGVConceptosRenombrar.Rows[numberOfRows];
                        row.Cells["ID"].Value = filaDatos["ID"].ToString();
                        row.Cells["Concepto"].Value = filaDatos["Concepto"].ToString().Replace("_"," ");
                        row.Cells["Usuario"].Value = filaDatos["Usuario"].ToString();
                        System.Drawing.Image renombrar = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\edit.png");
                        row.Cells["Renombrar"].Value = renombrar;
                    }
                }
            }
            
            notSortableDataGridView();
        }

        private void notSortableDataGridView()
        {
            foreach (DataGridViewColumn column in DGVConceptosRenombrar.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        #endregion

        public RenombrarDetalle()
        {
            InitializeComponent();
        }

        private void RenombrarDetalle_Load(object sender, EventArgs e)
        {
            llenarRegistros();
        }

        private void DGVConceptosRenombrar_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(3))
            {
                DGVConceptosRenombrar.Cursor = Cursors.Hand;
            }
            else
            {
                DGVConceptosRenombrar.Cursor = Cursors.Default;
            }
        }

        private void DGVConceptosRenombrar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(3))
            {
                if (e.RowIndex >= 0)
                {
                    var idReg = Convert.ToInt32(DGVConceptosRenombrar.Rows[e.RowIndex].Cells[0].Value.ToString());

                    setNombreConceptoDinamico setNameConcept = new setNombreConceptoDinamico();

                    setNameConcept.FormClosed += delegate
                    {
                        llenarRegistros();
                        this.Close();
                    };

                    setNameConcept.idRegConcept = idReg;
                    setNameConcept.Show();
                }
            }
        }

        private void RenombrarDetalle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }
    }
}
