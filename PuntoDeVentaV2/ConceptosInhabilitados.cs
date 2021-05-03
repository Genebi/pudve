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
                        row.Cells["Concepto"].Value = filaDatos["Concepto"].ToString();
                        row.Cells["Usuario"].Value = filaDatos["Usuario"].ToString();
                        System.Drawing.Image habilitar = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\eye.png");
                        row.Cells["Habilitar"].Value = habilitar;
                    }
                }
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
    }
}
