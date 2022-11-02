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
    public partial class SugerenciasGoogle : Form
    {
        List<string> sugerencias;

        public string seleccionada { get; set; }

        public SugerenciasGoogle(List<string> sugerencias)
        {
            InitializeComponent();

            this.sugerencias = sugerencias;
        }

        private void SugerenciasGoogle_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            DGVSugerencias.Rows.Clear();

            if (sugerencias.Count > 0)
            {
                foreach (var sugerencia in sugerencias)
                {
                    int rowId = DGVSugerencias.Rows.Add();

                    DataGridViewRow row = DGVSugerencias.Rows[rowId];

                    Image agregar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\reply.png");

                    row.Cells["Sugerencia"].Value = sugerencia;
                    row.Cells["Agregar"].Value = agregar;
                }
            }

            DGVSugerencias.ClearSelection();
        }

        private void DGVSugerencias_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 1)
                {
                    DGVSugerencias.Cursor = Cursors.Hand;
                }
            }
        }

        private void DGVSugerencias_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 1)
                {
                    DGVSugerencias.Cursor = Cursors.Default;
                }
            }
        }

        private void DGVSugerencias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 1)
                {
                    var sugerencia = DGVSugerencias.Rows[e.RowIndex].Cells["Sugerencia"].Value.ToString();

                    seleccionada = sugerencia;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}
