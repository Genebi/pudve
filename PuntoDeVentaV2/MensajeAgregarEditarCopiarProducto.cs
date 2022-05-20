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
    public partial class MensajeAgregarEditarCopiarProducto : Form
    {

        private string mensaje = string.Empty;
        private string imagen = string.Empty;
        public MensajeAgregarEditarCopiarProducto(string _titulo, string _texto, string _imagen)
        {
            InitializeComponent();
            this.Text = _titulo;
            this.mensaje = _texto;            
            this.imagen = _imagen;
            timer1.Start();
        }

      
            

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            

        }

        private void MensajeAgregarEditarCopiarProducto_Load(object sender, EventArgs e)
        {
            lblMensaje.Text = mensaje;
            Image icono = Image.FromFile(Properties.Settings.Default.rutaDirectorio + $@"\PUDVE\icon\black\{imagen}.png");
            pbImagen.Image = icono;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MensajeAgregarEditarCopiarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) 
            {
                this.Close(); 
            }                        
        }

        private void botonRedondo1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }   
}
