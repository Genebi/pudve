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

            //Se obtienen los proveedores del usuario
            listaProveedores = cn.ObtenerProveedores(FormPrincipal.userID);
            cbProveedores.Items.AddRange(listaProveedores);
            cbProveedores.SelectedIndex = Productos.proveedorElegido;

            //Se carga la informacion por defecto del producto registrado
            lbProducto.Text = datos[1];
            producto = datos[1];
            precioProducto = float.Parse(datos[2]);
            stockProducto = Convert.ToInt32(datos[4]);
            ActiveControl = txtCantidadCompra;

            //Eventos para los campos que solo requieren cantidades
            txtPrecioCompra.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCantidadCompra.KeyPress += new KeyPressEventHandler(SoloNumeros);
            txtAumentar.KeyPress += new KeyPressEventHandler(SoloNumeros);
            txtDisminuir.KeyPress += new KeyPressEventHandler(SoloNumeros);
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
            //Tipo de ajuste es cuando se hace desde una de estas dos opciones
            //Cuando se carga desde un XML o registro normal el tipo de ajuste es 0
            //Cuando se hace la moficiacion desde la opcion producto comprado es 1
            //Cuando se hace desde la opcion ajustar es 2

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
                    Productos.proveedorElegido = cbProveedores.SelectedIndex;
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
                    //Datos del producto que se actualizará
                    datos = new string[] { IDProducto.ToString(), stockProducto.ToString() };

                    cn.EjecutarConsulta(cs.ActualizarStockProductos(datos));

                    this.Close();
                }
            }

            //Ajustar producto
            if (rbAjustar.Checked)
            {
                int auxiliar = 0;
                var aumentar = txtAumentar.Text;
                var disminuir = txtDisminuir.Text;

                if (aumentar != "")
                {
                    auxiliar = Convert.ToInt32(aumentar);
                    stockProducto += auxiliar;
                }

                if (disminuir != "")
                {
                    auxiliar = Convert.ToInt32(disminuir);
                    stockProducto -= auxiliar;

                    if (stockProducto < 0)
                    {
                        stockProducto = 0;
                    }

                    auxiliar *= -1;
                }

                //Datos para la tabla historial de compras
                string[] datos = new string[] { producto, auxiliar.ToString(), precioProducto.ToString(), comentario, "2", fechaOperacion, IDProducto.ToString(), FormPrincipal.userID.ToString() };

                int resultado = cn.EjecutarConsulta(cs.AjustarProducto(datos, 2));

                if (resultado > 0)
                {
                    //Datos del producto que se actualizará
                    datos = new string[] { IDProducto.ToString(), stockProducto.ToString() };

                    cn.EjecutarConsulta(cs.ActualizarStockProductos(datos));

                    this.Close();
                }
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

        private void txtAumentar_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtAumentar.Text != "")
            {
                txtDisminuir.Enabled = false;
            }
            else
            {
                txtDisminuir.Enabled = true;
            }
        }

        private void txtDisminuir_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtDisminuir.Text != "")
            {
                txtAumentar.Enabled = false;
            }
            else
            {
                txtAumentar.Enabled = true;
            }
        }

        private void SoloNumeros(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8))
            {
                e.Handled = true;
                return;
            }
        }

        private void SoloDecimales(object sender, KeyPressEventArgs e)
        {
            //permite 0-9, eliminar y decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            //verifica que solo un decimal este permitido
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                {
                    e.Handled = true;
                }  
            }
        }

        private void txtCantidadCompra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }
    }
}
