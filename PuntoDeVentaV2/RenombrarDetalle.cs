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
        public delegate void pasarOldNameNewName(string oldName, string newName);
        public event pasarOldNameNewName nombreDetalle;

        public RenombrarDetalle()
        {
            InitializeComponent();
        }

        private void RenombrarDetalle_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtOldValue.Text.Equals(""))
            {
                MessageBox.Show("Ningun campo debe de estar en limpio favor de checar.\nEl Primer campo por favor", "Error de captura", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtNewValue.Text.Equals(""))
            {
                MessageBox.Show("Ningun campo debe de estar en limpio favor de checar.\nEl Primer campo por favor", "Error de captura", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!txtOldValue.Text.Equals("") && !txtNewValue.Text.Equals(""))
            {
                nombreDetalle(txtOldValue.Text.ToString(), txtNewValue.Text.ToString());
            }

            this.Close();
        }
    }
}
