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
            var dato = cn.CargarDatos(cs.consultarMensajeTicket(FormPrincipal.userID));
            
            foreach (DataRow item in dato.Rows)
            {
                valor = Convert.ToInt32(item[0].ToString());
            }

            if (valor == 1)
            {
                cn.EjecutarConsulta(cs.editarMensajeDeTicket(FormPrincipal.userID, txtMensajeTicket.Text));
            }
            else if(valor == 0)
            {
                cn.EjecutarConsulta(cs.insertarMensajeDeTicket(FormPrincipal.userID, txtMensajeTicket.Text));
            }
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
