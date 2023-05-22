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
    public partial class DevolverProductoFolio : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        public static string folio;
        public DevolverProductoFolio()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Inventario.cancelarPresionado = false;
            if (string.IsNullOrWhiteSpace(txtFolio.Text))
            {
                MessageBox.Show("Favor de ingresar un folio valido.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var dato = cn.CargarDatos($"SELECT ID FROM ventas WHERE Folio = '{txtFolio.Text}'");
            if (dato.Rows.Count == 0)
            {
                MessageBox.Show("No se encontro ninguna venta asociada a este folio.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            folio = dato.Rows[0]["ID"].ToString();
            ListaDeProductosDevolverProducto products = new ListaDeProductosDevolverProducto();
            products.FormClosed += delegate
            {
                this.Close();
            };
            products.ShowDialog();

        }

        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Inventario.cancelarPresionado = true;
            this.Close();
            return;
        }
    }
}
