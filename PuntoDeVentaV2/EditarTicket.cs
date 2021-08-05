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
    public partial class EditarTicket : Form
    {
        public EditarTicket()
        {
            InitializeComponent();
        }

        private void EditarTicket_Load(object sender, EventArgs e)
        {
            //Ventas.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            editarMensajeTicket mensaje = new editarMensajeTicket();
            mensaje.Show();
        }
    }
}
