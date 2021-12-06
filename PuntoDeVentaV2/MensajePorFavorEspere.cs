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
    public partial class MensajePorFavorEspere : Form
    {
        public int tiempoDeEspera { get; set; }
        public string propiedadCambiar { get; set; }

        public MensajePorFavorEspere()
        {
            InitializeComponent();
        }

        private void MensajePorFavorEspere_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(propiedadCambiar))
            {
                this.Text = propiedadCambiar;
            }
        }
    }
}
