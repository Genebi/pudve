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
    public partial class SetUpPUDVE : Form
    {
        Conexion cn = new Conexion();

        public SetUpPUDVE()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Seccion en proceso de hacer BackUp", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            try
            {
                cn.BackUpDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al hacer BackUp: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
