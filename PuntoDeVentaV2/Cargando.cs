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
    public partial class Cargando : Form
    {
        bool especial = false;
        public Cargando(bool especial = false)
        {
            InitializeComponent();

            this.especial = especial;
        }

        private void Cargando_Load(object sender, EventArgs e)
        {
            //var imagen = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\loading.gif";

            if (especial)
            {
                label2.Visible = false;
                label1.Text = "Buscando...";
                this.BackColor = Color.Gray;
                PBLoading.Load("loading.gif");
            }
            else
            {
                PBLoading.Load("loading.gif");
            }
            
        }
    }
}
