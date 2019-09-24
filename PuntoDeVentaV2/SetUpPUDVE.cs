using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        private void SetUpPUDVE_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.StockNegativo)
            {
                cbStockNegativo.Checked = true;
            }
        }

        private void btnRespaldo_Click(object sender, EventArgs e)
        {
            guardarArchivo.Filter = "SQL (*.db)|*.db";
            guardarArchivo.FilterIndex = 1;
            guardarArchivo.RestoreDirectory = true;

            if (guardarArchivo.ShowDialog() == DialogResult.OK)
            {
                string archivoBD = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db";
                string copiaDB = guardarArchivo.FileName;
                //string fecha = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");

                try
                {
                    File.Copy(archivoBD, copiaDB);

                    MessageBox.Show("Información respaldada exitosamente", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbStockNegativo_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.StockNegativo = cbStockNegativo.Checked;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
    }
}
