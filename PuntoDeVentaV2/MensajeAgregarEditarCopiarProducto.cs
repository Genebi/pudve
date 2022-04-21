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
        public MensajeAgregarEditarCopiarProducto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MensajeAgregarEditarCopiarProducto_Load(object sender, EventArgs e)
        {
            if (AgregarEditarProducto.producto ==1)
            {
                lblMensaje.Text = "Se guardo exitosamente el producto....";
            }
            else if (AgregarEditarProducto.producto == 2)
            {
                lblMensaje.Text = "Se actualizo exitosamente el producto....";
            }
            else if (AgregarEditarProducto.producto ==3)
            {
                lblMensaje.Text = "Se copio exitosamente el producto....";
            }

            if (AgregarEditarProducto.servicio == 1)
            {

                lblMensaje.Text = "Se guardo exitosamente el servicio....";
            }
            else if (AgregarEditarProducto.servicio == 2)
            {
                lblMensaje.Text = "Se actualizo exitosamente el servicio....";
            }
            else if (AgregarEditarProducto.servicio == 3)
            {
                lblMensaje.Text = "Se copio exitosamente el servicio....";
            }

            if (AgregarEditarProducto.combo == 1)
            {
                lblMensaje.Text = "Se guardo exitosamente el combo....";
            }
            else if (AgregarEditarProducto.combo == 2)
            {
                lblMensaje.Text = "Se actualizo exitosamente el combo....";
            }
            else if (AgregarEditarProducto.combo == 3)
            {
                lblMensaje.Text = "Se copio exitosamente el combo....";
            }
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
    }   
}
