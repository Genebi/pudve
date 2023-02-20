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
        private decimal precioProducto = 0;
        private decimal precioProductoAux = 0;
        private decimal stockProducto = 0;
        private decimal stockExistencia = 0;
        private int apartado = 0;
        private float precioAdquision = 0f; // Precio de compra

        private string[] listaProveedores = new string[] { };
        // Listas para guardar los ID's de los productos que se enviara correo
        private Dictionary<int, string> enviarStockMinimo;

        public int cantidadPasadaProductoCombo { set; get; }
        public static int cantidadProductoCombo = 0;

        private int tipoOperacion = 0;

        public static string mensaje = string.Empty;
        public static string nombreDePorducto = string.Empty;
        public static decimal cantidadRegresada = 0;
        string proveedor = string.Empty;

        //apartado 1 = Productos
        //apartado 2 = Inventario
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDProducto">ID de Producto a Modificar</param>
        /// <param name="apartado">apartado=1(Sección Productos) apartado=2(Sección Invetario)</param> 
        /// <param name="operacion"></param>
        public AjustarProducto(int IDProducto, int apartado = 1, int operacion = 0)
        {
            InitializeComponent();

            this.IDProducto = IDProducto;
            this.apartado = apartado;

            if (operacion > 0)
            {
                this.tipoOperacion = operacion;
            }
        }

        private void AjustarProducto_Load(object sender, EventArgs e)
        {
            
            cbConceptos.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbProveedores.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            string[] datos = cn.BuscarProducto(IDProducto, FormPrincipal.userID);

            //Se obtienen los proveedores del usuario
            listaProveedores = cn.ObtenerProveedores(FormPrincipal.userID);

            Dictionary<string, string> proveedores = new Dictionary<string, string>();

            enviarStockMinimo = new Dictionary<int, string>();

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
                    if (tipoOperacion.Equals(1))
                    {
                        txtCantidadCompra.Text = cantidadProductoCombo.ToString();
                    }
                    else if (tipoOperacion.Equals(2))
                    {
                        txtDisminuir.Text = cantidadPasadaProductoCombo.ToString();
                    }
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
            precioProducto = decimal.Parse(datos[2]);
            stockProducto = decimal.Parse(datos[4]);
            stockExistencia = stockProducto;
            precioAdquision = float.Parse(datos[11]);
            ActiveControl = txtCantidadCompra;

            txt_en_stock.Text = stockProducto.ToString();
            cantidadStockActual.Text = stockProducto.ToString();


            //Eventos para los campos que solo requieren cantidades
            using (var DTPrecioCommpra = cn.CargarDatos($"SELECT PrecioCompra FROM productos WHERE ID = {IDProducto}"))
            {
                if (!DTPrecioCommpra.Rows.Count.Equals(0))
                {
                    string Precio = DTPrecioCommpra.Rows[0]["PrecioCompra"].ToString();
                    txtPrecioCompra.Text = Precio;
                }
                else
                {
                    txtPrecioCompra.Text = "0.00";
                }
                
            }
            txtPrecioCompra.Click += txtPrecioCompra_Click;
            txtPrecioCompra.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtCantidadCompra.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtAumentar.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtDisminuir.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtPrecio.KeyPress += new KeyPressEventHandler(SoloDecimales);

            CargarConceptos();

            if (apartado.Equals(2))
            {
                pnlMensajeOperacionInventario.BringToFront();

                //cantidadProductoCombo = cantidadPasadaProductoCombo;
                
                if (tipoOperacion.Equals(1))
                {
                    rbProducto.Checked = true;
                    lblOperacionInventario.Text = "Aumentar Producto(s)";
                    txtCantidadCompra.Text = cantidadProductoCombo.ToString();
                }
                else if (tipoOperacion.Equals(2))
                {
                    rbAjustar.Checked = true;

                    btnActualiza.Visible = false;
                    txtAumentar.Visible = false;
                    lbEditarPrecio.Visible = false;
                    lbAumentar.Visible = false;

                    txtAumentar.TabStop = true;
                    txtDisminuir.TabStop = true;

                    txtAumentar.TabIndex = 1;
                    txtDisminuir.TabIndex = 0;
                    
                    txtDisminuir.Focus();
                    txtDisminuir.Select();
                    
                    lblOperacionInventario.Text = "Reducir Producto(s)";

                    txtDisminuir.Text = cantidadPasadaProductoCombo.ToString();
                }
            }
            var mensajeInventario = cn.CargarDatos($"SELECT Mensaje FROM `mensajesinventario` WHERE IDProducto = {datos[0]}");
            
            if (!mensajeInventario.Rows.Count.Equals(0))
            {
                mensaje = mensajeInventario.Rows[0]["Mensaje"].ToString();
            }

            if (Inventario.desdeRegresarProdcuto == 1)
            {
                this.Opacity = 0;
                nombreDePorducto = lbProducto.Text;
                CantidadRegresada regreso = new CantidadRegresada();
                regreso.ShowDialog();
                txtCantidadCompra.Text = cantidadRegresada.ToString();
                if (string.IsNullOrWhiteSpace(cantidadRegresada.ToString()) || cantidadRegresada == 0)
                {
                    this.Close();
                }
                else
                {
                    btnAceptar.PerformClick();
                }
               
            }
        }

        private void Permiso(object sender, KeyPressEventArgs e)
        {
            if (FormPrincipal.userNickName.Contains('@'))
            {
                var datos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Ventas");
                if (datos[29].Equals(0))
                {
                    Utilidades.MensajePermiso();
                    return;
                }
            }
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

            txtPrecio.Enabled = true;
            lbEditarPrecio.Enabled = true;
            btnActualiza.Enabled = true;
        }

        private void rbAjustar_CheckedChanged(object sender, EventArgs e)
        {
            panelComprado.Visible = false; 
            panelAjustar.Visible = true; 
            RestaurarValores(2);

            txtPrecio.Enabled = false;
            lbEditarPrecio.Enabled = false;
            btnActualiza.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            decimal parser;
            if (Inventario.AumentarDisminuir == 0)
            {
                if (txtCantidadCompra.Text.Equals("0") || string.IsNullOrWhiteSpace(txtCantidadCompra.Text.ToString()))
                {
                    MessageBox.Show("El campo de cantidad tiene que ser mayor a 0.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                if (txtDisminuir.Text.Equals("0") || string.IsNullOrWhiteSpace(txtDisminuir.Text.ToString()))
                {
                    MessageBox.Show("El campo de cantidad tiene que ser mayor a 0.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (!decimal.TryParse(txtCantidadCompra.Text,out parser) && !string.IsNullOrEmpty(txtCantidadCompra.Text))
            {
                MessageBox.Show("El campo no tiene un formato válido", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCantidadCompra.Clear();
                txtCantidadCompra.Focus();
                return;
            }
            if (!decimal.TryParse(txtPrecioCompra.Text, out parser) && !string.IsNullOrEmpty(txtPrecioCompra.Text))
            {
                MessageBox.Show("El campo no tiene un formato válido", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioCompra.Clear();
                txtPrecioCompra.Focus();
                return; 
            }
            if (!decimal.TryParse(txtDisminuir.Text, out parser) && !string.IsNullOrEmpty(txtDisminuir.Text))
                    {
                        MessageBox.Show("El campo no tiene un formato válido", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDisminuir.Clear();
                txtDisminuir.Focus();
                        return;
                    }
            var datoUsuario = FormPrincipal.userNickName;
            var empleado = "0";

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
            var precioAux = decimal.Parse(precioTmp);

            if (precioAux != precioProducto)
            {
                string precioAnteriorTmp = precioProducto.ToString();
                string precioNuevoTmp = precioAux.ToString();

                precioAnteriorTmp = precioAnteriorTmp.Replace(",", "");
                precioNuevoTmp = precioNuevoTmp.Replace(",", "");

                if (datoUsuario.Contains('@'))
                {
                    empleado = cs.buscarIDEmpleado(datoUsuario);
                }

                var info = new string[] {
                    FormPrincipal.userID.ToString(), empleado, IDProducto.ToString(),
                    precioAnteriorTmp, precioNuevoTmp,
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

                    Invent.getSuma = Convert.ToDecimal(cantidadCompra);
                    Invent.getStockAnterior = (float)stockProducto;
                }

                var stockOriginal = stockProducto; //Stoack Anterior
                var stockAgregado = cantidadCompra; //Cantidad

                stockProducto += decimal.Parse(cantidadCompra); //Nuevo Stock

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
                                    //var info = new string[] {
                                    //    lbProducto.Text, stockOriginal.ToString(), stockAgregado,
                                    //    stockActual.ToString(), "ajustar producto", "agregó"
                                    //};

                                    //Thread notificacion = new Thread(
                                    //    () => Utilidades.CambioStockProductoEmail(info, titulo: "AUMENTADO")
                                    //);

                                    //notificacion.Start();

                                    string cadenaProducto = $"{lbProducto.Text}|{stockOriginal}|{stockAgregado}|{stockActual}|ajustar producto|agregó";

                                    Inventario.productosAumentoDecremento.Add(cadenaProducto);
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
                        var mensaje = cn.CargarDatos(cs.mostrarMensajeInventario(IDProducto));
                        if (mensaje.Rows.Count > 0)
                        {
                            string mensajeInventario = mensaje.Rows[0]["Mensaje"].ToString();
                            //MessageBox.Show(mensajeInventario);
                        }
                       
                        
                        //MessageBox.Show("Precio de producto Actualizado del producto: " + IDProducto + " por el precio: " + precioCompra, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //MessageBox.Show("Precio de producto No se Actualizo del producto: " + IDProducto, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    var stockNuevo = stockActual;
                    var stockAnterior = stockOriginal;
                    var numRevision = cn.CargarDatos($"SELECT * FROM NoRevisionAumentarInventario WHERE IdUsuario = {FormPrincipal.userID}");

                    if (numRevision.Rows.Count > 0)
                    {
                        var numeroRevision = numRevision.Rows[0]["NoRevisionAumentarInventario"].ToString();

                        if (Inventario.desdeRegresarProdcuto.Equals(1))
                        {
                            cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad) VALUES ('{IDProducto}','Devolución','{stockAnterior}','{stockNuevo.ToString()}','{fechaOperacion}','{FormPrincipal.userNickName}','+{cantidadCompra}')");
                        }
                        else
                        {
                            cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad) VALUES ('{IDProducto}','Actualizar Stock (Aumentar): N° Revision: {numeroRevision}','{stockAnterior}','{stockNuevo.ToString()}','{fechaOperacion}','{FormPrincipal.userNickName}','+{cantidadCompra}')");
                        }
                    }
                    
                    this.Close();
                }
            }

            //Ajustar producto
            if (rbAjustar.Checked)
            {
                using (DataTable dtVerificarPermitirStockNegativo = cn.CargarDatos(cs.obtenerStcokNegativoConfiguracion()))
                {
                    if (!dtVerificarPermitirStockNegativo.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtVerificarPermitirStockNegativo.Rows)
                        {
                            if (item["StockNegativo"].Equals(1))
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
                                    stockProducto +=Convert.ToDecimal(auxiliar);
                                    operacion = "agregó";
                                }

                                if (disminuir != "")
                                {
                                    auxiliar = float.Parse(disminuir);
                                    stockAgregado = auxiliar;
                                    stockProducto -= Convert.ToDecimal(auxiliar);
                                    operacion = "restó";

                                    if (stockProducto < 0)
                                    {
                                        stockProducto = 0;
                                    }

                                    auxiliar *= -1;
                                }

                                stockActual = (float)stockProducto;

                                if (string.IsNullOrWhiteSpace(aumentar) && string.IsNullOrWhiteSpace(disminuir))
                                {
                                    MessageBox.Show("Ingrese una cantidad para aumentar y/o disminuir", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                var emisor = string.Empty;
                                using (DataTable dtProvedor = cn.CargarDatos($"SELECT IDProducto, Proveedor FROM detallesproducto WHERE IDProducto = '{IDProducto}'"))
                                {
                                    if (!dtProvedor.Rows.Count.Equals(0))
                                    {
                                        emisor = dtProvedor.Rows[0]["Proveedor"].ToString();
                                    }
                                    else
                                    {
                                        emisor = "Ajuste";
                                    }
                                }

                                //Datos para la tabla historial de compras
                                string[] datos = new string[]
                                {
                                    producto, auxiliar.ToString(), precioProducto.ToString(),
                                    comentario, "2", fechaOperacion, IDProducto.ToString(),
                                    FormPrincipal.userID.ToString(), "Ajuste", "Ajuste",
                                    emisor, fechaOperacion, concepto
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

                                    //Datos del producto que se actualizará
                                    datos = new string[] { IDProducto.ToString(), stockProducto.ToString(), FormPrincipal.userID.ToString() };

                                    cn.EjecutarConsulta(cs.ActualizarStockProductos(datos));

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
                                                    //var info = new string[] {
                                                    //    lbProducto.Text, stockOriginal.ToString(), stockAgregado.ToString(),
                                                    //    stockActual.ToString(), "ajustar producto", operacion
                                                    //};

                                                    //Thread notificacion = new Thread(
                                                    //    () => Utilidades.CambioStockProductoEmail(info)
                                                    //);

                                                    //notificacion.Start();

                                                    string cadenaProducto = $"{lbProducto.Text}|{stockOriginal}|{stockAgregado}|{stockActual}|ajustar producto|{operacion}";

                                                    Inventario.productosAumentoDecremento.Add(cadenaProducto);
                                                }
                                            }
                                        }

                                        // Correo de stock minimo
                                        if (Convert.ToInt16(datosConfig[2]) == 1)
                                        {
                                            var datosProductoTmp = cn.BuscarProducto(Convert.ToInt32(IDProducto), FormPrincipal.userID);

                                            var configProducto = mb.ComprobarCorreoProducto(IDProducto);

                                            if (configProducto.Count > 0)
                                            {
                                                if (configProducto[2] == 1)
                                                {
                                                    // Obtener el stock minimo del producto
                                                    var stockMinimo = Convert.ToInt32(datosProductoTmp[10]);
                                                    var stockTmp = Convert.ToDecimal(datosProductoTmp[4]);

                                                    if (stockTmp <= stockMinimo)
                                                    {
                                                        if (!enviarStockMinimo.ContainsKey(Convert.ToInt32(IDProducto)))
                                                        {
                                                            var terminacion = datosProductoTmp[4].Split('.');

                                                            if (terminacion.Count() > 0)
                                                            {
                                                                if (terminacion[1] == "00")
                                                                {
                                                                    datosProductoTmp[4] = terminacion[0];
                                                                }
                                                            }

                                                            var nombre = $"{datosProductoTmp[1]} --- CÓDIGO BARRAS: {datosProductoTmp[7]} --- STOCK MINIMO: {datosProductoTmp[10]} --- STOCK ACTUAL: {datosProductoTmp[4]}";
                                                            enviarStockMinimo.Add(Convert.ToInt32(IDProducto), nombre);
                                                        }
                                                    }

                                                    // Hilo para envio de correos en segundo plano
                                                    Thread envio = new Thread(() => CorreoStockMinimo());
                                                    envio.Start();
                                                }
                                            }
                                        }
                                    } 

                                    decimal stockNuevo = Convert.ToInt32(stockActual);
                                    decimal stockAnterior = Convert.ToDecimal( stockOriginal);
                                    var numRevision = cn.CargarDatos($"SELECT * FROM NoRevisionDisminuirInventario WHERE IdUsuario = {FormPrincipal.userID}");
                                    var numeroRevision = numRevision.Rows[0]["NoRevisionDisminuirInventario"].ToString();
                                    decimal cantidad = Convert.ToInt32( stockOriginal) - Convert.ToInt32(stockActual);

                                    cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad) VALUES ('{IDProducto}','Actualizar Stock (Disminuir): N° Revision: {numeroRevision}','{stockAnterior}','{stockNuevo}','{fechaOperacion}','{FormPrincipal.userNickName}','-{cantidad}')");

                                    this.Close();
                                }
                            }
                            else if (item["StockNegativo"].Equals(0))
                            {
                                bool esNumeroStock = false, esNumeroCantidadDisminuir = false;
                                decimal numeroStock = 0, cantidadDisminuir = 0;

                                esNumeroStock = Decimal.TryParse(txt_en_stock.Text, out numeroStock);
                                esNumeroCantidadDisminuir = Decimal.TryParse(txtDisminuir.Text, out cantidadDisminuir);

                                if (cantidadDisminuir <= numeroStock)
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
                                        stockProducto += Convert.ToDecimal(auxiliar);
                                        operacion = "agregó";
                                    }

                                    if (disminuir != "")
                                    {
                                        auxiliar = float.Parse(disminuir);
                                        stockAgregado = auxiliar;
                                        stockProducto -= Convert.ToDecimal(auxiliar);
                                        operacion = "restó";

                                        if (stockProducto < 0)
                                        {
                                            stockProducto = 0;
                                        }
                                        if (auxiliar==0)
                                        {
                                            MessageBox.Show("Ingrese una cantidad para aumentar y/o disminuir", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }

                                        auxiliar *= -1;
                                    }

                                    stockActual = (float)stockProducto;

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

                                        //Datos del producto que se actualizará
                                        datos = new string[] { IDProducto.ToString(), stockProducto.ToString(), FormPrincipal.userID.ToString() };

                                        cn.EjecutarConsulta(cs.ActualizarStockProductos(datos));

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
                                                        //var info = new string[] {
                                                        //    lbProducto.Text, stockOriginal.ToString(), stockAgregado.ToString(),
                                                        //    stockActual.ToString(), "ajustar producto", operacion
                                                        //};

                                                        //Thread notificacion = new Thread(
                                                        //    () => Utilidades.CambioStockProductoEmail(info)
                                                        //);

                                                        //notificacion.Start();

                                                        string cadenaProducto = $"{lbProducto.Text}|{stockOriginal}|{stockAgregado}|{stockActual}|ajustar producto|{operacion}";

                                                        Inventario.productosAumentoDecremento.Add(cadenaProducto);
                                                    }
                                                }
                                            }

                                            // Correo de stock minimo
                                            if (Convert.ToInt16(datosConfig[2]) == 1)
                                            {
                                                var datosProductoTmp = cn.BuscarProducto(Convert.ToInt32(IDProducto), FormPrincipal.userID);

                                                var configProducto = mb.ComprobarCorreoProducto(IDProducto);

                                                if (configProducto.Count > 0)
                                                {
                                                    if (configProducto[2] == 1)
                                                    {
                                                        // Obtener el stock minimo del producto
                                                        var stockMinimo = Convert.ToInt32(datosProductoTmp[10]);
                                                        var stockTmp = Convert.ToDecimal(datosProductoTmp[4]);

                                                        if (stockTmp <= stockMinimo)
                                                        {
                                                            if (!enviarStockMinimo.ContainsKey(Convert.ToInt32(IDProducto)))
                                                            {
                                                                var terminacion = datosProductoTmp[4].Split('.');

                                                                if (terminacion.Count() > 0)
                                                                {
                                                                    if (terminacion[1] == "00")
                                                                    {
                                                                        datosProductoTmp[4] = terminacion[0];
                                                                    }
                                                                }

                                                                var nombre = $"{datosProductoTmp[1]} --- CÓDIGO BARRAS: {datosProductoTmp[7]} --- STOCK MINIMO: {datosProductoTmp[10]} --- STOCK ACTUAL: {datosProductoTmp[4]}";
                                                                enviarStockMinimo.Add(Convert.ToInt32(IDProducto), nombre);
                                                            }
                                                        }

                                                        // Hilo para envio de correos en segundo plano
                                                        Thread envio = new Thread(() => CorreoStockMinimo());
                                                        envio.Start();
                                                    }
                                                }
                                            }
                                        }

                                        decimal stockNuevo = Convert.ToInt32(stockActual);
                                        decimal stockAnterior = Convert.ToDecimal(stockOriginal);
                                        var numRevision = cn.CargarDatos($"SELECT * FROM NoRevisionDisminuirInventario WHERE IdUsuario = {FormPrincipal.userID}");
                                        var numeroRevision = numRevision.Rows[0]["NoRevisionDisminuirInventario"].ToString();
                                        decimal cantidad = Convert.ToInt32(stockOriginal) - Convert.ToInt32(stockActual);

                                        cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad) VALUES ('{IDProducto}','Actualizar Stock (Disminuir): N° Revision: {numeroRevision}','{stockAnterior}','{stockNuevo}','{fechaOperacion}','{FormPrincipal.userNickName}','-{cantidad}')");

                                        this.Close();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("De acuerdo a su configuración no es posible tener stock\nnegativo, si desea modificarlo, puede hacerlo desde\nel apartado \"Configuración general\"", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtDisminuir.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }
            }



            this.Close();

            //Recargar el DGV de Productos
            Productos productos = Application.OpenForms.OfType<Productos>().FirstOrDefault();

            if (productos != null)
            {
                productos.CargarDatos();
            }

            var Consultaid = cn.CargarDatos($"SELECT ID FROM proveedores WHERE Nombre= '{proveedor}' AND IDUsuario = {FormPrincipal.userID}");
            if (!Consultaid.Rows.Count.Equals(0))
            {
            string id = Consultaid.Rows[0]["ID"].ToString();
            cn.EjecutarConsulta($"UPDATE detallesproducto SET Proveedor = '{proveedor}', IDProveedor = {id} WHERE IDProducto = {IDProducto}");
            }

            var nombreProducto = lbProducto.Text;
            cn.EjecutarConsulta($"UPDATE Productos SET Nombre = '{nombreProducto}' WHERE ID = {IDProducto} AND IDUsuario = {FormPrincipal.userID}");
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
            if (tipoOperacion.Equals(2))
            {
                if (!txtDisminuir.Text.Equals(string.Empty))
                {
                    if (txtDisminuir.Text.Equals(".") || txtDisminuir.Text.Equals("0.") || txtDisminuir.Text.Equals("0.0"))
                    {
                        return;
                    }
                    lb_disminuir_stock.Visible = true;
                    lb_disminuir_stock_total.Visible = true;
                }
                else if (txtDisminuir.Text.Equals(string.Empty))
                {
                    lb_disminuir_stock.Visible = false;
                    lb_disminuir_stock_total.Visible = false;
                }
            }
            else if (tipoOperacion.Equals(0))
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
            TextBox txt = new TextBox();
            txt = (TextBox)sender;
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        if (calculadora.seEnvia.Equals(true))
                        {
                            txt.Text = calculadora.lCalculadora.Text;
                        }
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
                if (!txtCantidadCompra.Text.Equals(string.Empty))
                {
                    float cantidadAumentar = 0;
                    cantidadAumentar = float.Parse(txtCantidadCompra.Text);
                    if (cantidadAumentar.Equals(0))
                    {
                        MessageBox.Show("La cantidad comprada debe de ser mayor a Cero", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (!cantidadAumentar.Equals(0))
                    {
                        btnAceptar.PerformClick();
                    }
                }
                //btnAceptar.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
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
                        Invent.getSuma = Convert.ToDecimal(aumentar);
                        Invent.getStockAnterior = float.Parse(txt_en_stock.Text);
                    }
                }

                suma = float.Parse(txt_en_stock.Text) + aumentar;
                lb_aumentar_stock_total.Text = suma.ToString();
            }
            if(opc == 2)
            {
                if(txtDisminuir.Text != "")
                {
                    disminuir = float.Parse(txtDisminuir.Text);
                    if (Invent != null)
                    {
                        Invent.getResta = disminuir;
                        Invent.getStockAnterior = float.Parse(txt_en_stock.Text);
                    }
                }

                resta = float.Parse(txt_en_stock.Text) - disminuir; 
                lb_disminuir_stock_total.Text = resta.ToString();
            }
        }

        private void lbEditarPrecio_Click(object sender, EventArgs e)
        {
            if (FormPrincipal.userNickName.Contains('@'))
            {
                var datos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Ventas");
                if (datos[29].Equals(0))
                {
                    Utilidades.MensajePermiso();
                    return;
                }
            }
            int comprobar = 0;
            string idempleado = cs.buscarIDEmpleado(FormPrincipal.userNickName);

            using (DataTable dtUsuarios = cn.CargarDatos(cs.validarUsuario(FormPrincipal.userNickName)))
            {
                if (!dtUsuarios.Rows.Count.Equals(0))
                {
                    var precio = txtPrecio.Text.Replace("$", "");
                    txtPrecio.Text = precio;

                    txtPrecio.ReadOnly = false;
                    lbProducto.ReadOnly = false;
                    txtPrecio.SelectAll();
                    txtPrecio.Focus();
                    //txtPrecio.Select(txtPrecio.Text.Length, 0);
                }
                else
                {
                    using (DataTable dtEmpleadosPermisos = cn.CargarDatos(cs.condicionAsignar("Precio", idempleado)))
                    {
                        if (!dtEmpleadosPermisos.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtEmpleadosPermisos.Rows)
                            {
                                comprobar = Convert.ToInt32(item["total"]);
                            }
                        }
                    }
                    if (comprobar > 0)
                    {
                        var precio = txtPrecio.Text.Replace("$", "");
                        txtPrecio.Text = precio;

                        txtPrecio.ReadOnly = false;
                        txtPrecio.SelectAll();
                        txtPrecio.Focus();
                        //txtPrecio.Select(txtPrecio.Text.Length, 0);
                    }
                    else
                    {
                        MessageBox.Show("No cuentas con los permisos para modificar este dato", "Alerta Sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
           
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
                conceptos.FormClosed += delegate
                {
                    var idParaComboBox = ConceptosCaja.id;
                    CargarConceptos();

                    var x = string.Empty;

                    var getConcepto = cn.CargarDatos($"SELECT Concepto FROM ConceptosDinamicos WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idParaComboBox}'");
                    if (!getConcepto.Rows.Count.Equals(0))
                    {
                        foreach (DataRow concepto in getConcepto.Rows)
                        {
                            x = concepto["Concepto"].ToString();
                        }
                    }
                    cbConceptos.SelectedIndex = cbConceptos.FindString(x);

                };
                conceptos.ShowDialog();

            }
        }

        private void txtDisminuir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (rbProducto.Checked)
                {
                    if (!txtAumentar.Text.Equals(string.Empty))
                    {
                        float cantidadAumentar = 0;
                        cantidadAumentar = float.Parse(txtAumentar.Text);
                        if (cantidadAumentar.Equals(0))
                        {
                            MessageBox.Show("La cantidad comprada debe de ser mayor a Cero", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else if (!cantidadAumentar.Equals(0))
                        {
                            btnAceptar.PerformClick();
                        }
                    }
                }
                if (rbAjustar.Checked)
                {
                    if (!txtDisminuir.Text.Equals(string.Empty))
                    {
                        float cantidadDisminuir = 0;
                        cantidadDisminuir = float.Parse(txtDisminuir.Text);
                        if (cantidadDisminuir.Equals(0))
                        {
                            MessageBox.Show("La cantidad a disminuir debe de ser mayor a Cero", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else if (!cantidadDisminuir.Equals(0))
                        {
                            btnAceptar.PerformClick();
                        }
                    }
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        if (calculadora.seEnvia.Equals(true))
                        {
                            txtDisminuir.Text = calculadora.lCalculadora.Text;
                        }
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
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        if (calculadora.seEnvia.Equals(true))
                        {
                            txtAumentar.Text = calculadora.lCalculadora.Text;
                        }
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
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }
        
        private void btnActualiza_Click(object sender, EventArgs e)
        {
            var obtenerTxt = string.Empty;
            var datoObtenido = "";

            if (rbProducto.Checked.Equals(true))
            {
                obtenerTxt = txtPrecioCompra.Text;
            }
            else if (rbAjustar.Checked.Equals(true))
            {
                if (precioProductoAux.Equals(0))
                {
                    var oldPrecio = txtPrecio.Text.Replace("$", string.Empty);
                    obtenerTxt = oldPrecio;
                }
                else
                {
                    obtenerTxt = precioProductoAux.ToString();
                }
            }

            if (!obtenerTxt.Equals("") && !obtenerTxt.Equals("."))
            {
                var precio = float.Parse(obtenerTxt);
                var descuento = cn.CargarDatos($"SELECT PorcentajePrecio FROM Configuracion WHERE IDUsuario = {FormPrincipal.userID}");

                for (int i = 0; i < descuento.Rows.Count; i++)
                {
                    datoObtenido = descuento.Rows[i]["PorcentajePrecio"].ToString();
                }
                var x = float.Parse(datoObtenido);
                var operacion = (precio * (x / 100));
                var precioFinal = operacion + precio;
                txtPrecio.Text = "$ " + precioFinal.ToString(); 
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
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtAumentar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtDisminuir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void AjustarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            string contenidoTxtPrecio = txtPrecio.Text;
            bool siEsta = false;

            siEsta = contenidoTxtPrecio.Contains("$");

            if (siEsta)
            {
                //contenidoTxtPrecio.Remove(0, 1);
                contenidoTxtPrecio = contenidoTxtPrecio.Substring(1);
                precioProductoAux = decimal.Parse(contenidoTxtPrecio);

            }
            else
            {
                bool esNumero = false;
                double precio = 0;

                esNumero = Double.TryParse(txtPrecio.Text, out precio);
                if (esNumero)
                {
                    precioProductoAux = decimal.Parse(precio.ToString());
                }
                else
                {
                    MessageBox.Show("Favor de ingresar una cantidad valida","Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPrecio.SelectAll();
                    txtPrecio.Focus();
                    return;
                }
                
            }
        }

        private void CorreoStockMinimo()
        {
            var correo = FormPrincipal.datosUsuario[9];
            var asunto = string.Empty;
            var asuntoAdicional = string.Empty;
            var html = string.Empty;
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (!string.IsNullOrWhiteSpace(correo))
            {
                // Comprobar stock minimo
                if (enviarStockMinimo.Count > 0)
                {
                    asunto = "¡AVISO! Stock mínimo alcanzado por ventas.";

                    html = @"
                        <div style='margin-bottom: 50px;'>
                            <h4 style='text-align: center;'>PRODUCTOS CON STOCK MINIMO</h4><hr>
                            <ul style='color: black; font-size: 0.8em;'>";

                    foreach (var producto in enviarStockMinimo)
                    {
                        html += $"<li>{producto.Value}</li>";
                    }

                    var footerCorreo = string.Empty;


                    if (FormPrincipal.id_empleado > 0)
                    {
                        var datosEmpleado = mb.obtener_permisos_empleado(FormPrincipal.id_empleado, FormPrincipal.userID);

                        string nombreEmpleado = datosEmpleado[15];
                        string usuarioEmpleado = datosEmpleado[16];

                        var infoEmpleado = usuarioEmpleado.Split('@');

                        footerCorreo = $"<p style='font-size: 12px;'>Este ajuste fue realizado por el empleado <b>{nombreEmpleado} ({infoEmpleado[1]})</b> del usuario <b>{infoEmpleado[0]}</b>, los siguientes productos llegaron al stock mínimo con <span style='color: black;'>fecha de {fechaOperacion}</span></p>";
                    }
                    else
                    {
                        footerCorreo = $"<p style='font-size: 12px;'>Este ajuste fue realizado por el <b>ADMIN</b> del usuario <b>{FormPrincipal.userNickName}</b>, los siguientes productos llegaron al stock mínimo con <span style='color: black;'>fecha de {fechaOperacion}</span></p>";
                    }

                    html += $@"
                        </ul><hr>
                        {footerCorreo}
                    </div>";
                }

                if (!string.IsNullOrWhiteSpace(html))
                {
                    Utilidades.EnviarEmail(html, asunto, correo);
                }
            }
        }

        private void txtPrecioCompra_Enter(object sender, EventArgs e)
        {
            int comprobar = 0;
            string idempleado = cs.buscarIDEmpleado(FormPrincipal.userNickName);

            using (DataTable dtUsuarios = cn.CargarDatos(cs.validarUsuario(FormPrincipal.userNickName)))
            {
                if (!dtUsuarios.Rows.Count.Equals(0))
                {

                }
                else
                {
                    using (DataTable dtEmpleadosPermisos = cn.CargarDatos(cs.condicionAsignar("Precio", idempleado)))
                    {
                        if (!dtEmpleadosPermisos.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtEmpleadosPermisos.Rows)
                            {
                                comprobar = Convert.ToInt32(item["total"]);
                            }
                        }
                    }
                    if (comprobar > 0)
                    {
                    }
                    else
                    {
                        txtPrecioCompra.Enabled = false;
                        MessageBox.Show("No cuentas con los permisos para modificar este dato", "Alerta Sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void AjustarProducto_Shown(object sender, EventArgs e)
        {
            string[] datos = cn.BuscarProducto(IDProducto, FormPrincipal.userID);

            var estado = cn.CargarDatos($"SELECT Activo FROM `mensajesinventario` WHERE IDProducto = {datos[0]}");
            if (estado.Rows.Count.Equals(0))
            {

            }
            else
            {
                var activo = estado.Rows[0]["Activo"].ToString();
                if (activo == "1")
                {
                    MessageBox.Show(AjustarProducto.mensaje, "Aviso Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            
           
        }

        private void txtPrecioCompra_Click(object sender, EventArgs e)
        {
            if (FormPrincipal.userNickName.Contains('@'))
            {
                var datos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Ventas");
                if (datos[29].Equals(0))
                {
                    txtPrecioCompra.Enabled = false;
                    Utilidades.MensajePermiso();
                    return;
                }
            }
        }

        private void txtCantidadCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        if (calculadora.seEnvia.Equals(true))
                        {
                            txtCantidadCompra.Text = calculadora.lCalculadora.Text;
                        }
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

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        if (calculadora.seEnvia.Equals(true))
                        {
                            txtPrecioCompra.Text = calculadora.lCalculadora.Text;
                        }
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
    }
}
