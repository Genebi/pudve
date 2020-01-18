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
        MetodosBusquedas mb = new MetodosBusquedas();

        private int IDProducto = 0;
        private string producto = string.Empty;
        private float precioProducto = 0;
        private int stockProducto = 0;
        private int stockExistencia = 0;
        private int apartado = 0;

        private string[] listaProveedores = new string[] { };

        //apartado 1 = Productos
        //apartado 2 = Inventario
        public AjustarProducto(int IDProducto, int apartado = 1)
        {
            InitializeComponent();

            this.IDProducto = IDProducto;
            this.apartado = apartado;
        }

        private void AjustarProducto_Load(object sender, EventArgs e)
        {
            string[] datos = cn.BuscarProducto(IDProducto, FormPrincipal.userID);

            //Se obtienen los proveedores del usuario
            listaProveedores = cn.ObtenerProveedores(FormPrincipal.userID);

            if (listaProveedores.Length > 0)
            {
                Dictionary<string, string> proveedores = new Dictionary<string, string>();

                proveedores.Add("0", "Seleccionar un proveedor...");

                foreach (var proveedor in listaProveedores)
                {
                    var tmp = proveedor.Split('-');

                    proveedores.Add(tmp[0].Trim(), tmp[1].Trim());
                }

                cbProveedores.DataSource = proveedores.ToArray();
                cbProveedores.DisplayMember = "Value";
                cbProveedores.ValueMember = "Key";

                var proveedorActual = mb.DetallesProducto(IDProducto, FormPrincipal.userID);

                // Comprueba si el producto tiene un proveedor asignado
                if (proveedorActual.Length > 0)
                {
                    cbProveedores.SelectedValue = proveedorActual[1];
                }
                else
                {
                    cbProveedores.SelectedValue = "0";
                }
            }
            else
            {
                cbProveedores.Items.Add("Seleccionar un proveedor...");
                cbProveedores.SelectedIndex = 0;
            }
            

            //Se carga la informacion por defecto del producto registrado
            lbProducto.Text = datos[1];
            producto = datos[1];
            precioProducto = float.Parse(datos[2]);
            stockProducto = Convert.ToInt32(datos[4]);
            stockExistencia = stockProducto;
            ActiveControl = txtCantidadCompra;

            txt_en_stock.Text = stockProducto.ToString();
            

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
            var reporte = 0;

            if (apartado == 1)
            {
                reporte = Productos.idReporte;
            }

            if (apartado == 2)
            {
                reporte = Inventario.idReporte;
            }


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

                if (cbProveedores.SelectedValue.ToString() != "0")
                {
                    if (apartado == 1)
                    {
                        Productos.proveedorElegido = cbProveedores.SelectedIndex;
                    }

                    if (apartado == 2)
                    {
                        Inventario.proveedorElegido = cbProveedores.SelectedValue.ToString();
                    }

                    proveedor = cbProveedores.SelectedValue.ToString();
                    string[] tmp = cn.ObtenerProveedor(Convert.ToInt32(proveedor), FormPrincipal.userID);

                    rfc = tmp[1];
                    proveedor = tmp[0];
                }

                //Datos para la tabla historial de compras
                //string[] datos = new string[] { producto, cantidadCompra, precioProducto.ToString(), precioCompra, fechaCompra, rfc, proveedor, comentario, "1", fechaOperacion, reporte.ToString(), IDProducto.ToString(), FormPrincipal.userID.ToString() };
                if (string.IsNullOrWhiteSpace(precioCompra))
                {
                    precioCompra = "0";
                }

                if (string.IsNullOrWhiteSpace(cantidadCompra))
                {
                    MessageBox.Show("Es necesario ingresar una cantidad de compra", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCantidadCompra.Focus();
                    return;
                }

                float preCompra = (float)Convert.ToDouble(precioCompra);
                float preProduct = preCompra * (float)1.60;

                string[] datos = new string[] { producto, cantidadCompra, preProduct.ToString(), precioCompra, fechaCompra, rfc, proveedor, comentario, "1", fechaOperacion, reporte.ToString(), IDProducto.ToString(), FormPrincipal.userID.ToString() };

                stockProducto += Convert.ToInt32(cantidadCompra);

                int resultado = cn.EjecutarConsulta(cs.AjustarProducto(datos, 1));

                if (resultado > 0)
                {
                    //Datos del producto que se actualizará
                    datos = new string[] { IDProducto.ToString(), stockProducto.ToString(), FormPrincipal.userID.ToString() };

                    cn.EjecutarConsulta(cs.ActualizarStockProductos(datos));

                    //Productos
                    if (apartado == 1)
                    {
                        Productos.botonAceptar = true;
                    }

                    //Inventario
                    if (apartado == 2)
                    {
                        Inventario.botonAceptar = true;
                    }

                    int resul = cn.EjecutarConsulta(cs.SetUpPrecioProductos(IDProducto, (float)Convert.ToDouble(precioCompra), FormPrincipal.userID));

                    if (resul > 0)
                    {
                        //MessageBox.Show("Precio de producto Actualizado del producto: " + IDProducto + " por el precio: " + precioCompra, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //MessageBox.Show("Precio de producto No se Actualizo del producto: " + IDProducto, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

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

                if (string.IsNullOrWhiteSpace(aumentar) && string.IsNullOrWhiteSpace(disminuir))
                {
                    MessageBox.Show("Ingrese una cantidad para aumentar y/o disminuir", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Datos para la tabla historial de compras
                string[] datos = new string[] { producto, auxiliar.ToString(), precioProducto.ToString(), comentario, "2", fechaOperacion, IDProducto.ToString(), FormPrincipal.userID.ToString(), "Ajuste", "Ajuste", "Ajuste", fechaOperacion };

                int resultado = cn.EjecutarConsulta(cs.AjustarProducto(datos, 2));

                if (resultado > 0)
                {
                    //Productos
                    if (apartado == 1)
                    {
                        Productos.botonAceptar = true;
                    }

                    //Inventario
                    if (apartado == 2)
                    {
                        Inventario.botonAceptar = true;
                    }

                    //Datos del producto que se actualizará
                    datos = new string[] { IDProducto.ToString(), stockProducto.ToString(), FormPrincipal.userID.ToString() };

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

            lb_aumentar_stock.Visible = false;
            lb_aumentar_stock_total.Visible = false;
            lb_disminuir_stock.Visible = false;
            lb_disminuir_stock_total.Visible = false;
            lb_aumentar_stock_total.Text = "0";
            lb_disminuir_stock_total.Text = "0";


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
                lb_aumentar_stock.Visible = true;
                lb_aumentar_stock_total.Visible = true;
            }
            else
            {
                txtDisminuir.Enabled = true;
                lb_aumentar_stock.Visible = false;
                lb_aumentar_stock_total.Visible = false;
            }

            suma_resta_stock(1);
        }

        private void txtDisminuir_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtDisminuir.Text != "")
            {
                txtAumentar.Enabled = false;
                lb_disminuir_stock.Visible = true;
                lb_disminuir_stock_total.Visible = true;
            }
            else
            {
                txtAumentar.Enabled = true;
                lb_disminuir_stock.Visible = false;
                lb_disminuir_stock_total.Visible = false;
            }

            suma_resta_stock(2);
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

        private void suma_resta_stock(int opc)
        {
            int suma = 0, resta = 0, aumentar = 0, disminuir = 0;

            if (opc == 1)
            {
                if(txtAumentar.Text != "")
                {
                    aumentar = Convert.ToInt32(txtAumentar.Text);
                }

                suma = Convert.ToInt32(txt_en_stock.Text) + aumentar;
                lb_aumentar_stock_total.Text = suma.ToString();
            }
            if(opc == 2)
            {
                if(txtDisminuir.Text != "")
                {
                    disminuir = Convert.ToInt32(txtDisminuir.Text);
                }

                resta = Convert.ToInt32(txt_en_stock.Text) - disminuir; 
                lb_disminuir_stock_total.Text = resta.ToString();
            }
        }
    }
}
