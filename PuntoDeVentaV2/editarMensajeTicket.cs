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
    public partial class editarMensajeTicket : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        int valor;
        string cargarmensaje;
        public editarMensajeTicket()
        {
            InitializeComponent();
        }

        private void editarMensajeTicket_Load(object sender, EventArgs e)
        {
            var mensaje = cn.CargarDatos(cs.MensajeTicket(FormPrincipal.userID));
            foreach (DataRow item in mensaje.Rows)
            {
                cargarmensaje = item[0].ToString();
                txtMensajeTicket.Text = cargarmensaje+"";
            }
        }

        private void btnGuardarMensaje_Click(object sender, EventArgs e)
        {
          
            EditarTicket.Mensaje = txtMensajeTicket.Text;
            MessageBox.Show("Guardado Correctamente");
            this.Close();
        }

        private void editarMensajeTicket_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }
    }
}
