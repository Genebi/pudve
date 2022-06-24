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

    public partial class MensajeSiNoCancelar : Form
    {
        public string opcionMensaje;
        AgregarEditarProducto opcion = new AgregarEditarProducto();
        public MensajeSiNoCancelar()
        {
            InitializeComponent();
        }
        private void btnNo_Click(object sender, EventArgs e)
        {
            opcionMensaje = "no";
            this.Close();
        }
        private void btnSi_Click(object sender, EventArgs e)
        {
            opcionMensaje = "si";
            this.Close();
        }
    }
}
