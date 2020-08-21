using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class AjustarProducto : Form
    {
        //Variables para calculadora
        int calcu = 0;
        ///////////////////////////

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        private int IDProducto = 0;
        private string producto = string.Empty;
        private float precioProducto = 0;
        private float stockProducto = 0f;
        private float stockExistencia = 0f;
        private int apartado = 0;
        private float precioAdquision = 0f; // Precio de compra

        private string[] listaProveedores = new string[] { };

        public int cantidadPasadaProductoCombo { set; get; }
        public static int cantidadProductoCombo = 0;

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

            Dictionary<string, string> proveedores = new Dictionary<string, string>();

            proveedores.Add("0", "Seleccionar un proveedor...");

            if (listaProveedores.Length > 0)
            {
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

                if (apartado.Equals(2))
                {
                    cantidadProductoCombo = cantidadPasadaProductoCombo;
                    txtCantidadCompra.Text = cantidadProductoCombo.ToString();
                }
            }
            else
            {
                cbProveedores.DataSource = proveedores.ToArray();
                cbProveedores.DisplayMember = "Value";
                cbProveedores.ValueMember = "Key";
            }
            

            //Se carga la informacion por defecto del producto registrado
            lbProducto.Text = datos[1];
            //lbPrecio.Text = "$" + float.Parse(datos[2]).ToString("N2");
            txtPrecio.Text = "$" + float.Parse(datos[2]).ToString("N2");
            producto = datos[1];
            precioProducto = float.Parse(datos[2]);
            stockProducto = Convert.ToInt32(datos[4]);
            stockExistencia = stockProducto;
            precioAdquision = float.Parse(datos[11]);
            ActiveControl = txtCantidadCompra;

            txt_en_stock.Text = stockProducto.ToString();
            cantidadStockActual.Text = stockProducto.ToString();
            

            //Eventos para los campos que solo requieren cantidades
            txtPrecioCompra.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCantidadCompra.KeyPress += new KeyPressEventHandler(SoloNumeros);
            txtAumentar.KeyPress += new KeyPressEventHandler(SoloNumeros);
            txtDisminuir.KeyPress += new KeyPressEventHandler(SoloNumeros);
            txtPrecio.KeyPress += new KeyPressEventHandler(SoloDecimales);

            CargarConceptos();
        }

        private void CargarConceptos()
        {
            // Cargar combobox de conceptos
            var conceptos = mb.ObtenerConceptosDinamicos(origen: "AJUSTAR");

            cbConceptos.DataSource = conceptos.ToArray();
            cbConceptos.DisplayMember = "Value";
            cbConceptos.ValueMember = "Key";
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

            // Se toma el precio que aparece visualmente al cargar el producto y se elimina el signo de $
            // en caso de que el usuario haya editado el precio del producto y se actualiza en la tabla Productos
            var precioTmp = txtPrecio.Text.Replace("$", "");
            var precioAux = float.Parse(precioTmp);

            if (precioAux != precioProducto)
            {
                var info = new string[] {
                    FormPrincipal.userID.ToString(), "0", IDProducto.ToString(),
                    precioProducto.ToString("N2"), precioAux.ToString("N2"),
                    "AJUSTAR PRODUCTO", fechaOperacion
                };

                // Guardamos los datos en la tabla historial de precios
                cn.EjecutarConsulta(cs.GuardarHistorialPrecios(info));

                // Ejecutar hilo para enviar notificacion
                var datosConfig = mb.ComprobarConfiguracion();

                if (datosConfig.Count > 0)
                {
                    if (Convert.ToInt16(datosConfig[0]) == 1)
                    {
                        var configProducto = mb.ComprobarCorreoProducto(IDProducto);

                        if (configProducto.Count > 0)
                        {
                            if (configProducto[0] == 1)
                            {
                                info = new string[] {
                                    lbProducto.Text, precioProducto.ToString("N2"),
                                    precioAux.ToString("N2"), "ajustar producto"
                                };

                                Thread notificacion = new Thread(
                                    () => Utilidades.CambioPrecioProductoEmail(info)
                                );

                                notificacion.Start();
                            }
                        }
                    }
                }

                // Actualizamos el precio de la tabla Productos
                precioProducto = precioAux;

                cn.EjecutarConsulta($"UPDATE Productos SET Precio = '{precioProducto}' WHERE ID = {IDProducto} AND IDUsuario = {FormPrincipal.userID}");
            }

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
                if (string.IsNullOrWhiteSpace(cantidadCompra))
                {
                    MessageBox.Show("Es necesario ingresar una cantidad de compra", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCantidadCompra.Focus();
                    return;
                }


                if (string.IsNullOrWhiteSpace(precioCompra))
                {
                    precioCompra = precioAdquision.ToString();
                }

                //float preCompra = (float)Convert.ToDouble(precioCompra);
                //float preProduct = preCompra * (float)1.60;

                string[] datos = new string[] { producto, cantidadCompra, precioCompra, precioProducto.ToString(), fechaCompra, rfc, proveedor, comentario, "1", fechaOperacion, reporte.ToString(), IDProducto.ToString(), FormPrincipal.userID.ToString() };

                Inventario Invent = Application.OpenForms.OfType<Inventario>().FirstOrDefault();

                if (Invent != null)
                {
                    Invent.getSuma = 0;
                    Invent.getResta = 0;

                    Invent.getSuma = Convert.ToInt32(cantidadCompra);
                    Invent.getStockAnterior = Convert.ToInt32(stockProducto);
                }

                var stockOriginal = stockProducto;
                var stockAgregado = cantidadCompra;

                stockProducto += Convert.ToInt32(cantidadCompra);

                var stockActual = stockProducto;

                int resultado = cn.EjecutarConsulta(cs.AjustarProducto(datos, 1));

                if (resultado > 0)
                {
                    // Envio de correo al agregar cantidad de producto
                    var datosConfig = mb.ComprobarConfiguracion();

                    if (datosConfig.Count > 0)
                    {
                        if (Convert.ToInt16(datosConfig[1]) == 1)
                        {
                            var configProducto = mb.ComprobarCorreoProducto(IDProducto);

                            if (configProducto.Count > 0)
                            {
                                if (configProducto[1] == 1)
                                {
                                    var info = new string[] {
                                        lbProducto.Text, stockOriginal.ToString(), stockAgregado,
                                        stockActual.ToString(), "ajustar producto", "agregó"
                                    };

                                    Thread notificacion = new Thread(
                                        () => Utilidades.CambioStockProductoEmail(info)
                                    );

                                    notificacion.Start();
                                }
                            }
                        }
                    }

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

                    int resul = cn.EjecutarConsulta(cs.SetUpPrecioProductos(IDProducto, (float)Convert.ToDouble(precioCompra), FormPrincipal.userID, 1));

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
                float auxiliar = 0f;
                var aumentar = txtAumentar.Text;
                var disminuir = txtDisminuir.Text;

                var concepto = cbConceptos.GetItemText(cbConceptos.SelectedItem);

                if (concepto.Equals("Seleccionar concepto..."))
                {
                    concepto = string.Empty;
                }

                var stockOriginal = stockProducto;
                var stockAgregado = 0f;
                var stockActual = 0f;
                var operacion = string.Empty;

                if (aumentar != "")
                {
                    auxiliar = float.Parse(aumentar);
                    stockAgregado = auxiliar;
                    stockProducto += auxiliar;
                    operacion = "agregó";
                }

                if (disminuir != "")
                {
                    auxiliar = float.Parse(disminuir);
                    stockAgregado = auxiliar;
                    stockProducto -= auxiliar;
                    operacion = "resto";

                    if (stockProducto < 0)
                    {
                        stockProducto = 0;
                    }

                    auxiliar *= -1;
                }

                stockActual = stockProducto;

                if (string.IsNullOrWhiteSpace(aumentar) && string.IsNullOrWhiteSpace(disminuir))
                {
                    MessageBox.Show("Ingrese una cantidad para aumentar y/o disminuir", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Datos para la tabla historial de compras
                string[] datos = new string[] 
                {
                    producto, auxiliar.ToString(), precioProducto.ToString(),
                    comentario, "2", fechaOperacion, IDProducto.ToString(),
                    FormPrincipal.userID.ToString(), "Ajuste", "Ajuste",
                    "Ajuste", fechaOperacion, concepto
                };

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

                    // Envio de correo al agregar cantidad de producto
                    var datosConfig = mb.ComprobarConfiguracion();

                    if (datosConfig.Count > 0)
                    {
                        if (Convert.ToInt16(datosConfig[1]) == 1)
                        {
                            var configProducto = mb.ComprobarCorreoProducto(IDProducto);

                            if (configProducto.Count > 0)
                            {
                                if (configProducto[1] == 1)
                                {
                                    var info = new string[] {
                                        lbProducto.Text, stockOriginal.ToString(), stockAgregado.ToString(),
                                        stockActual.ToString(), "ajustar producto", operacion
                                    };

                                    Thread notificacion = new Thread(
                                        () => Utilidades.CambioStockProductoEmail(info)
                                    );

                                    notificacion.Start();
                                }
                            }
                        }
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
            }else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void suma_resta_stock(int opc)
        {
            Inventario Invent = Application.OpenForms.OfType<Inventario>().FirstOrDefault();

            if (Invent != null)
            {
                Invent.getSuma = 0;
                Invent.getResta = 0;
            }

            float suma = 0f, resta = 0f, aumentar = 0f, disminuir = 0f;

            if (opc == 1)
            {
                if(txtAumentar.Text != "")
                {
                    aumentar = float.Parse(txtAumentar.Text);
                    if (Invent != null)
                    {
                        Invent.getSuma = aumentar;
                        Invent.getStockAnterior = float.Parse(txt_en_stock.Text);
                    }
                }

                suma = Convert.ToInt32(txt_en_stock.Text) + aumentar;
                lb_aumentar_stock_total.Text = suma.ToString();
            }
            if(opc == 2)
            {
                if(txtDisminuir.Text != "")
                {
                    disminuir = Convert.ToInt32(txtDisminuir.Text);
                    if (Invent != null)
                    {
                        Invent.getResta = disminuir;
                        Invent.getStockAnterior = Convert.ToInt32(txt_en_stock.Text);
                    }
                }

                resta = Convert.ToInt32(txt_en_stock.Text) - disminuir; 
                lb_disminuir_stock_total.Text = resta.ToString();
            }
        }

        private void lbEditarPrecio_Click(object sender, EventArgs e)
        {
            var precio = txtPrecio.Text.Replace("$", "");
            txtPrecio.Text = precio;

            txtPrecio.ReadOnly = false;
            txtPrecio.Focus();
            txtPrecio.Select(txtPrecio.Text.Length, 0);
        }

        private void txtPrecio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                var precio = txtPrecio.Text.Trim();

                if (!string.IsNullOrWhiteSpace(precio))
                {
                    var precioTmp = float.Parse(precio);
                    txtPrecio.Text = "$" + precioTmp.ToString("N2");
                    txtPrecio.ReadOnly = true;

                    if (rbProducto.Checked)
                    {
                        txtCantidadCompra.Select(txtCantidadCompra.Text.Length, 0);
                        txtCantidadCompra.Focus();
                    }
                    
                    if (rbAjustar.Checked)
                    {
                        txtAumentar.Select(txtAumentar.Text.Length, 0);
                        txtAumentar.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese el precio del producto", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAgregarConcepto_Click(object sender, EventArgs e)
        {
            using (var conceptos = new ConceptosCaja("AJUSTAR"))
            {
                conceptos.ShowDialog();

                CargarConceptos();
            }
        }

        private void txtDisminuir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        txtDisminuir.Text = calculadora.lCalculadora.Text;
                        calcu = 0;
                    };
                    if (!calculadora.Visible)
                    {
                        calculadora.Show();
                    }
                    else
                    {
                        calculadora.Show();
                    }
                }
            }
        }

        private void txtAumentar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        txtAumentar.Text = calculadora.lCalculadora.Text;
                        calcu = 0;
                    };
                    if (!calculadora.Visible)
                    {
                        calculadora.Show();
                    }
                    else
                    {
                        calculadora.Show();
                    }
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        private void btnActualiza_Click(object sender, EventArgs e)
        {
            var obtenerTxt = string.Empty;
            obtenerTxt = txtPrecioCompra.Text;
            var datoObtenido = "";

            if (!obtenerTxt.Equals("") && !obtenerTxt.Equals("."))
            {
                var precio = float.Parse(obtenerTxt);
                var descuento = cn.CargarDatos($"SELECT PorcentajePrecio FROM Configuracion WHERE IDUsuario = {FormPrincipal.userID}");

                for (int i = 0; i < descuento.Rows.Count; i++)
                {
                    datoObtenido = descuento.Rows[i]["PorcentajePrecio"].ToString();
                }
                var x = float.Parse(datoObtenido);
                var operacion = (precio * x);
                txtPrecio.Text = "$ " + operacion.ToString(); ;
            }
            else
            {
                MessageBox.Show("Por favor ingrese una cantidad", "¡Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioCompra.Text = "";
                txtPrecioCompra.Focus();
            }
        }

        private void txtPrecioCompra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtAumentar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtDisminuir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Escape)
            {
                this.Close();
            }
        }

        private void AjustarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
