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
        Consultas cs = new Consultas();

        private int IDProducto = 0;
        private string producto = string.Empty;
        private float precioProducto = 0;
        private int stockProducto = 0;

        private string[] listaProveedores = new string[] { };

        public AjustarProducto(int IDProducto)
        {
            InitializeComponent();

            this.IDProducto = IDProducto;
        }

        private void AjustarProducto_Load(object sender, EventArgs e)
        {
            string[] datos = cn.BuscarProducto(IDProducto, FormPrincipal.userID);

            listaProveedores = cn.ObtenerProveedores(FormPrincipal.userID);
            cbProveedores.Items.AddRange(listaProveedores);
            cbProveedores.SelectedIndex = 0;

            lbProducto.Text = datos[1];
            producto = datos[1];
            precioProducto = float.Parse(datos[2]);
            stockProducto = Convert.ToInt32(datos[4]);
            ActiveControl = txtCantidadCompra;
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
            var comentario = txtComentarios.Text;
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            //Producto comprado
            if (rbProducto.Checked)
            {
                var rfc = string.Empty;
                var proveedor = string.Empty;
                var fechaCompra = dpFechaCompra.Text;
                var precioCompra = txtPrecioCompra.Text;
                var cantidadCompra = txtCantidadCompra.Text;

                if (cbProveedores.SelectedIndex > 0)
                {
                    proveedor = cbProveedores.SelectedItem.ToString();
                    string[] tmp = cn.ObtenerProveedor(proveedor, FormPrincipal.userID);
                    rfc = tmp[1];
                }

                //Datos para la tabla historial de compras
                string[] datos = new string[] { producto, cantidadCompra, precioCompra, precioProducto.ToString(), fechaCompra, rfc, proveedor, comentario, "1", fechaOperacion, IDProducto.ToString(), FormPrincipal.userID.ToString() };

                stockProducto += Convert.ToInt32(cantidadCompra);

                int resultado = cn.EjecutarConsulta(cs.AjustarProducto(datos, 1));

                if (resultado > 0)
                {
                    //Datos del producto que se actualizara
                    datos = new string[] { IDProducto.ToString(), stockProducto.ToString() };

                    cn.EjecutarConsulta(cs.ActualizarStockProductos(datos));

                    this.Close();
                }
            }

            //Ajustar producto
            if (rbAjustar.Checked)
            {
                MessageBox.Show("Ajustar");
            }
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
