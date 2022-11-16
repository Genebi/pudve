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
        Conexion cn = new Conexion();

        private string origen;

        public static string query { get; set; }
        public static int empty { get; set; }
        public AgregarConcepto(string origen)
        {
            InitializeComponent();
            txtConcepto.Select();
            this.origen = origen;
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

                var existeConcepto = cn.CargarDatos($"SELECT * FROM ConceptosDinamicos WHERE IDUsuario = {FormPrincipal.userID} AND Concepto = '{concepto}'");

                if (existeConcepto.Rows.Count > 0)
                {
                    MessageBox.Show("Este concepto ya se encuentra registrado.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                empty = 1;

                query = "INSERT INTO ConceptosDinamicos (IDUsuario, Concepto, Origen, FechaOperacion)";
                query += $"VALUES ('{FormPrincipal.userID}', '{concepto}', '{origen}', '{fechaOperacion}')";
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

        private void txtConcepto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtConcepto.Text))
                {
                    btnAgregar.PerformClick();
                }
                else
                {
                    MessageBox.Show("Ingrese el concepto", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void AgregarConcepto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }
    }
}
