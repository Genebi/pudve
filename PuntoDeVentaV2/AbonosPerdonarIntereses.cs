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
    
    public partial class AbonosPerdonarIntereses : Form
    {
        public decimal interesPerdonado = 0;
        public AbonosPerdonarIntereses(decimal intereses)
        {
            InitializeComponent();
            lblintereses.Text = intereses.ToString("C2");
            numCantidad.Maximum = intereses;
        }

        private void numCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                interesPerdonado = numCantidad.Value;
                DialogResult dialogResult = MessageBox.Show($"Descontar de la deuda la siguiente cantidad:{interesPerdonado.ToString("C2")}", "Confirmar", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    return;
                }
            }
        }

        private void AbonosPerdonarIntereses_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Close();
            }
        }
    }
}
