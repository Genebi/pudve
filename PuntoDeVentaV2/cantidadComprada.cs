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
    public partial class cantidadComprada : Form
    {
         
        public static decimal nuevaCantidad;
        public static int nuevaCantidadCambio = 0;
        public cantidadComprada()
        {
            InitializeComponent();
        }

        private void cantidadComprada_Load(object sender, EventArgs e)
        {
            txtCantidad.Text = Convert.ToString(Ventas.cantidadAnterior);
            lblNombreProducto.Text = Ventas.nombreprodCantidad;
            nuevaCantidadCambio = 0;
            txtCantidad.SelectAll();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            nuevaCantidad = Convert.ToDecimal(txtCantidad.Text);
            nuevaCantidadCambio = 1;
            this.Close();
        }
    }
}
