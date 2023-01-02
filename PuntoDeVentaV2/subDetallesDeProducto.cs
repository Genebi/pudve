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
    public partial class subDetallesDeProducto : Form
    {
        public subDetallesDeProducto()
        {
            InitializeComponent();
                    //TIPOS DE DATO PARA LA DB
            //--0 DATE
            //--1 Decimal
            //--2 String
        }

        private void subDetallesDeProducto_Load(object sender, EventArgs e)
        {
            if (AgregarEditarProducto.DatosSourceFinal == 1)
            {
                //MessageBox.Show("Agregar");
            }
            else if (AgregarEditarProducto.DatosSourceFinal == 2)
            {
                lblNombreProducto.Text = AgregarEditarProducto.nombreProdSubDetalles;
                //MessageBox.Show("Editar");
            }
        }

        private void btnAddDetalle_Click(object sender, EventArgs e)
        {
            categoriaSubdetalle subdetalle = new categoriaSubdetalle();
            subdetalle.ShowDialog();
        }
    }
}
