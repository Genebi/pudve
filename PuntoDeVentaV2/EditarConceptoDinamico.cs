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
    public partial class EditarConceptoDinamico : Form
    {
        public string nuevoConcepto { get; set; }
        private string concepto;

        public EditarConceptoDinamico(string concepto)
        {
            InitializeComponent();

            this.concepto = concepto;
        }

        private void EditarConceptoDinamico_Load(object sender, EventArgs e)
        {
            txtConcepto.Text = concepto;
            txtConcepto.Focus();
            txtConcepto.Select(txtConcepto.Text.Length, 0);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var concepto = txtConcepto.Text.Trim();

            if (string.IsNullOrWhiteSpace(concepto))
            {
                MessageBox.Show("El concepto es obligatorio", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            nuevoConcepto = concepto;
            Close();
        }

        private void txtConcepto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }
    }
}
