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
    public partial class AgregarConcepto : Form
    {
        public static string query { get; set; }
        public static int empty { get; set; }
        public AgregarConcepto()
        {
            InitializeComponent();
            txtConcepto.Select();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string agregarName = txtConcepto.Text;
            //agregarName = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el Concepto", "Agregar Concepto", "".ToUpper(), 500, 300);
            if (!string.IsNullOrEmpty(agregarName))
            {
                //var mensaje =  MessageBox.Show($"¿Desea agregar {agregarName.ToUpper()}?", "Mensaje de Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                var concepto = agregarName.ToUpper().Trim();
                var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                if (string.IsNullOrWhiteSpace(concepto))
                {
                    MessageBox.Show("Ingrese el nombre del concepto", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                query = "INSERT INTO ConceptosDinamicos (IDUsuario, Concepto, Origen, FechaOperacion)";
                query += $"VALUES ('{FormPrincipal.userID}', '{concepto}', '{ConceptosCaja.pasarOrigen}', '{fechaOperacion}')";
                this.Close();
            }
            else
            {
                empty = 0;
            }
        }

        private void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            txtConcepto.CharacterCasing = CharacterCasing.Upper;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
