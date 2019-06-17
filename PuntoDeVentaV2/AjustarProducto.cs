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
    public partial class AjustarProducto : Form
    {
        Conexion cn = new Conexion();

        private int IDProducto = 0;
        private string[] listaProveedores = new string[] { };

        public AjustarProducto(int IDProducto)
        {
            InitializeComponent();

            this.IDProducto = IDProducto;
        }

        private void AjustarProducto_Load(object sender, EventArgs e)
        {
            listaProveedores = cn.ObtenerProveedores(FormPrincipal.userID);
            cbProveedores.Items.AddRange(listaProveedores);
            cbProveedores.SelectedIndex = 0;

            string[] datos = cn.BuscarProducto(IDProducto, FormPrincipal.userID);

            lbProducto.Text = datos[1];
            this.ActiveControl = txtCantidadCompra;
        }

        private void rbProducto_CheckedChanged(object sender, EventArgs e)
        {
            panelAjustar.Visible = false;
            panelComprado.Visible = true;
            RestaurarValores(1);
        }

        private void rbAjustar_CheckedChanged(object sender, EventArgs e)
        {
            panelComprado.Visible = false;
            panelAjustar.Visible = true;
            RestaurarValores(2);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("aceptar");
        }

        private void RestaurarValores(int valorCB)
        {
            cbProveedores.SelectedIndex = 0;
            txtPrecioCompra.Text = string.Empty;
            txtCantidadCompra.Text = string.Empty;
            txtAumentar.Text = string.Empty;
            txtDisminuir.Text = string.Empty;

            if (valorCB == 1)
            {
                txtCantidadCompra.Focus();
            }

            if (valorCB == 2)
            {
                txtAumentar.Focus();
            }
        }
    }
}
