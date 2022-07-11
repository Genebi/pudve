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
    public partial class HistorialAnticipos : Form
    {
        public DataTable datosHistoria { get; set; }

        public HistorialAnticipos()
        {
            InitializeComponent();
        }

        private void HistorialAnticipos_Load(object sender, EventArgs e)
        {
            if (!datosHistoria.Rows.Count.Equals(0))
            {
                DGVAnticipos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                DGVAnticipos.DataSource = datosHistoria;
            }
        }
    }
}
