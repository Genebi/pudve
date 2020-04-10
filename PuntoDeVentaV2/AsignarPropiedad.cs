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
    public partial class AsignarPropiedad : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        string propiedad = string.Empty;

        Dictionary<int, string> productos;
        Dictionary<string, string> clavesUnidades;

        public AsignarPropiedad(object propiedad)
        {
            InitializeComponent();

            this.propiedad = propiedad.ToString();

            productos = Productos.productosSeleccionados;
        }

        private void AsignarPropiedad_Load(object sender, EventArgs e)
        {
            lbNombrePropiedad.Text = $"ASIGNAR {propiedad.ToUpper()}";

            clavesUnidades = mb.CargarClavesUnidades();

            CargarPropiedad();
        }

        private void CargarPropiedad()
        {
            Font fuente = new Font("Century Gothic", 10.0f);
            Font fuenteChica = new Font("Century Gothic", 8.0f);
            
            if (propiedad == "Mensaje")
            {
                TextBox tbMensaje = new TextBox();
                tbMensaje.Name = "tb" + propiedad;
                tbMensaje.Width = 200;
                tbMensaje.Height = 40;
                tbMensaje.CharacterCasing = CharacterCasing.Upper;
                tbMensaje.Font = fuente;
                tbMensaje.Multiline = true;
                tbMensaje.ScrollBars = ScrollBars.Vertical;
                tbMensaje.Location = new Point(65, 70);

                panelContenedor.Controls.Add(tbMensaje);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarMensaje"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarMensaje"));
            }
            else if (propiedad == "MensajeInventario")
            {
                TextBox tbMensaje = new TextBox();
                tbMensaje.Name = "tb" + propiedad;
                tbMensaje.Width = 200;
                tbMensaje.Height = 40;
                tbMensaje.CharacterCasing = CharacterCasing.Upper;
                tbMensaje.Font = fuente;
                tbMensaje.Multiline = true;
                tbMensaje.ScrollBars = ScrollBars.Vertical;
                tbMensaje.Location = new Point(65, 70);

                panelContenedor.Controls.Add(tbMensaje);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarMensaje"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarMensaje"));
            }
            else if (propiedad == "Stock")
            {
                TextBox tbStock = new TextBox();
                tbStock.Name = "tb" + propiedad;
                tbStock.Width = 200;
                tbStock.Height = 20;
                tbStock.TextAlign = HorizontalAlignment.Center;
                tbStock.Font = fuente;
                tbStock.KeyPress += new KeyPressEventHandler(SoloDecimales);
                tbStock.Location = new Point(65, 70);

                panelContenedor.Controls.Add(tbStock);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarStock"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarStock"));
            }
            else if (propiedad == "StockMinimo")
            {
                TextBox tbStock = new TextBox();
                tbStock.Name = "tb" + propiedad;
                tbStock.Width = 200;
                tbStock.Height = 20;
                tbStock.TextAlign = HorizontalAlignment.Center;
                tbStock.Font = fuente;
                tbStock.KeyPress += new KeyPressEventHandler(SoloDecimales);
                tbStock.Location = new Point(65, 70);

                panelContenedor.Controls.Add(tbStock);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarStockMinimo"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarStockMinimo"));
            }
            else if (propiedad == "StockMaximo")
            {
                TextBox tbStock = new TextBox();
                tbStock.Name = "tb" + propiedad;
                tbStock.Width = 200;
                tbStock.Height = 20;
                tbStock.TextAlign = HorizontalAlignment.Center;
                tbStock.Font = fuente;
                tbStock.KeyPress += new KeyPressEventHandler(SoloDecimales);
                tbStock.Location = new Point(65, 70);

                panelContenedor.Controls.Add(tbStock);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarStockMaximo"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarStockMaximo"));
            }
            else if (propiedad == "Precio")
            {
                TextBox tbPrecio = new TextBox();
                tbPrecio.Name = "tb" + propiedad;
                tbPrecio.Width = 200;
                tbPrecio.Height = 20;
                tbPrecio.TextAlign = HorizontalAlignment.Center;
                tbPrecio.Font = fuente;
                tbPrecio.KeyPress += new KeyPressEventHandler(SoloDecimales);
                tbPrecio.Location = new Point(65, 70);

                panelContenedor.Controls.Add(tbPrecio);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarPrecio"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarPrecio"));
            }
            else if (propiedad == "NumeroRevision")
            {
                TextBox tbRevision = new TextBox();
                tbRevision.Name = "tb" + propiedad;
                tbRevision.Width = 200;
                tbRevision.Height = 20;
                tbRevision.TextAlign = HorizontalAlignment.Center;
                tbRevision.Font = fuente;
                tbRevision.KeyPress += new KeyPressEventHandler(SoloDecimales);
                tbRevision.Location = new Point(65, 70);

                panelContenedor.Controls.Add(tbRevision);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarRevision"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarRevision"));
            }
            else if (propiedad == "TipoIVA")
            {
                Dictionary<string, string> listaIVA = new Dictionary<string, string>();
                listaIVA.Add("0%", "IVA AL 0%");
                listaIVA.Add("8%", "IVA AL 8%");
                listaIVA.Add("16%", "IVA AL 16%");
                listaIVA.Add("Exento", "IVA EXENTO");

                ComboBox cbIVA = new ComboBox();
                cbIVA.Name = "cb" + propiedad;
                cbIVA.Width = 300;
                cbIVA.Height = 20;
                cbIVA.Font = fuente;
                cbIVA.DropDownStyle = ComboBoxStyle.DropDownList;
                cbIVA.DataSource = listaIVA.ToArray();
                cbIVA.DisplayMember = "Value";
                cbIVA.ValueMember = "Key";
                cbIVA.Location = new Point(15, 70);

                panelContenedor.Controls.Add(cbIVA);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarIVA"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarIVA"));
            }
            else if (propiedad == "ClaveProducto")
            {
                TextBox tbClaveProducto = new TextBox();
                tbClaveProducto.Name = "tb" + propiedad;
                tbClaveProducto.Width = 200;
                tbClaveProducto.Height = 40;
                tbClaveProducto.CharacterCasing = CharacterCasing.Upper;
                tbClaveProducto.KeyPress += new KeyPressEventHandler(SoloDecimales);
                tbClaveProducto.Font = fuente;
                tbClaveProducto.Location = new Point(65, 70);

                panelContenedor.Controls.Add(tbClaveProducto);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarClaveProducto"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarClaveProducto"));
            }
            else if (propiedad == "ClaveUnidad")
            {
                TextBox tbClaveUnidad = new TextBox();
                tbClaveUnidad.Name = "tb" + propiedad;
                tbClaveUnidad.Width = 200;
                tbClaveUnidad.Height = 40;
                tbClaveUnidad.CharacterCasing = CharacterCasing.Upper;
                tbClaveUnidad.Font = fuente;
                tbClaveUnidad.KeyUp += CambioTexto;
                tbClaveUnidad.Location = new Point(65, 50);

                ComboBox cbClaves = new ComboBox();
                cbClaves.Name = "cb" + propiedad;
                cbClaves.Width = 300;
                cbClaves.Height = 20;
                cbClaves.Font = fuenteChica;
                cbClaves.DropDownStyle = ComboBoxStyle.DropDownList;
                cbClaves.DataSource = clavesUnidades.ToArray();
                cbClaves.DisplayMember = "Value";
                cbClaves.ValueMember = "Key";
                cbClaves.SelectedValueChanged += new EventHandler(CambioComboBox);
                cbClaves.Location = new Point(15, 80);

                panelContenedor.Controls.Add(tbClaveUnidad);
                panelContenedor.Controls.Add(cbClaves);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarClaveUnidad"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarClaveUnidad"));
            }
            else if (propiedad == "CorreosProducto")
            {
                CheckBox primerCB = new CheckBox();
                primerCB.Text = "Correo al modificar precio de producto";
                primerCB.Location = new Point(40, 40);
                primerCB.Name = "CorreoPrecioProducto";
                primerCB.Width = 300;
                primerCB.Font = fuenteChica;

                CheckBox segundoCB = new CheckBox();
                segundoCB.Text = "Correo al modificar stock de producto";
                segundoCB.Location = new Point(40, 60);
                segundoCB.Name = "CorreoStockProducto";
                segundoCB.Width = 300;
                segundoCB.Font = fuenteChica;

                CheckBox tercerCB = new CheckBox();
                tercerCB.Text = "Correo al llegar a stock minimo";
                tercerCB.Location = new Point(40, 80);
                tercerCB.Name = "CorreoStockMinimo";
                tercerCB.Width = 300;
                tercerCB.Font = fuenteChica;

                CheckBox cuartoCB = new CheckBox();
                cuartoCB.Text = "Correo al hacer venta de producto";
                cuartoCB.Location = new Point(40, 100);
                cuartoCB.Name = "CorreoVentaProducto";
                cuartoCB.Width = 300;
                cuartoCB.Font = fuenteChica;

                var checkboxes = new CheckBox[] { primerCB, segundoCB, tercerCB, cuartoCB };

                panelContenedor.Controls.AddRange(checkboxes);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarCorreos", 150));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarCorreos", 150));
            }
            else if (propiedad == "Proveedor")
            {
                var listaProveedores = cn.ObtenerProveedores(FormPrincipal.userID);

                // Comprobamos que tenga proveedores
                if (listaProveedores.Length > 0)
                {
                    Dictionary<int, string> lista = new Dictionary<int, string>();

                    lista.Add(0, "Seleccionar proveedor...");

                    foreach (string proveedor in listaProveedores)
                    {
                        var info = proveedor.Split('-');

                        lista.Add(Convert.ToInt32(info[0].Trim()), info[1].Trim());
                    }

                    ComboBox cbProveedores = new ComboBox();
                    cbProveedores.Name = "cb" + propiedad;
                    cbProveedores.Width = 300;
                    cbProveedores.Height = 20;
                    cbProveedores.Font = fuente;
                    cbProveedores.DropDownStyle = ComboBoxStyle.DropDownList;
                    cbProveedores.DataSource = lista.ToArray();
                    cbProveedores.DisplayMember = "Value";
                    cbProveedores.ValueMember = "Key";
                    cbProveedores.Location = new Point(15, 70);

                    panelContenedor.Controls.Add(cbProveedores);
                    panelContenedor.Controls.Add(GenerarBoton(0, "cancelarProveedor"));
                    panelContenedor.Controls.Add(GenerarBoton(1, "aceptarProveedor"));

                    // Verificamos si fue solo un producto el que se selecciono para buscar si ya tiene
                    // proveedor registrado, si tiene uno registrado ese producto lo selecciona por defecto
                    // en el combobox
                    if (productos.Count == 1)
                    {
                        var idProducto = productos.Keys.First();
                        var detalleProducto = mb.DetallesProducto(idProducto, FormPrincipal.userID);

                        if (detalleProducto.Length > 0)
                        {
                            var idProveedor = Convert.ToInt32(detalleProducto[1]);

                            var datosProveedor = mb.ObtenerDatosProveedor(idProveedor, FormPrincipal.userID);

                            if (datosProveedor.Length > 0)
                            {
                                cbProveedores.SelectedValue = idProveedor;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No hay proveedores registrados", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Dispose();
                }
            }
            else
            {
                // Consulta para obtener todas las opciones registradas para esa propiedad
                var opciones = mb.ObtenerOpcionesPropiedad(FormPrincipal.userID, propiedad);

                if (opciones.Count > 0)
                {
                    // Aqui van todos los que son dinamicos agregados en detalle de producto
                    ComboBox cbPropiedad = new ComboBox();
                    cbPropiedad.Name = "cb" + propiedad;
                    cbPropiedad.Width = 300;
                    cbPropiedad.Height = 20;
                    cbPropiedad.Font = fuente;
                    cbPropiedad.DropDownStyle = ComboBoxStyle.DropDownList;
                    cbPropiedad.DataSource = opciones.ToArray();
                    cbPropiedad.DisplayMember = "Value";
                    cbPropiedad.ValueMember = "Key";
                    cbPropiedad.Location = new Point(15, 70);

                    panelContenedor.Controls.Add(cbPropiedad);
                    panelContenedor.Controls.Add(GenerarBoton(0, "cancelar" + propiedad));
                    panelContenedor.Controls.Add(GenerarBoton(1, "aceptar" + propiedad));
                }
                else
                {
                    MessageBox.Show("No hay opciones registradas para la propiedad " + propiedad, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Dispose();
                }
            }
        }

        private Button GenerarBoton(int tipo, string nombre, int ejeY = 125)
        {
            Button boton = new Button();
            Font fuenteBoton = new Font("Century Gothic", 9.5f);     

            if (tipo == 0)
            {
                Button btnCancelar = new Button();
                btnCancelar.Text = "Cancelar";
                btnCancelar.Name = nombre;
                btnCancelar.BackColor = Color.FromArgb(192, 0, 0);
                btnCancelar.ForeColor = Color.White;
                btnCancelar.FlatStyle = FlatStyle.Flat;
                btnCancelar.Cursor = Cursors.Hand;
                btnCancelar.Font = fuenteBoton;
                btnCancelar.Width = 95;
                btnCancelar.Height = 25;
                btnCancelar.Click += new EventHandler(botonCancelar_Click);
                btnCancelar.Location = new Point(65, ejeY);

                boton = btnCancelar;
            }
            
            if (tipo == 1)
            {
                Button btnAceptar = new Button();
                btnAceptar.Text = "Aceptar";
                btnAceptar.Name = nombre;
                btnAceptar.BackColor = Color.Green;
                btnAceptar.ForeColor = Color.White;
                btnAceptar.FlatStyle = FlatStyle.Flat;
                btnAceptar.Cursor = Cursors.Hand;
                btnAceptar.Font = fuenteBoton;
                btnAceptar.Width = 95;
                btnAceptar.Height = 25;
                btnAceptar.Click += new EventHandler(botonAceptar_Click);
                btnAceptar.Location = new Point(170, ejeY);

                boton = btnAceptar;
            }

            return boton;
        }

        private void botonAceptar_Click(object sender, EventArgs e)
        {
            Button boton = sender as Button;

            string[] datos;

            if (propiedad == "Mensaje")
            {
                TextBox txtMensaje = (TextBox)this.Controls.Find("tbMensaje", true)[0];

                var mensaje = txtMensaje.Text;

                if (string.IsNullOrWhiteSpace(mensaje))
                {
                    MessageBox.Show("Ingrese el mensaje para asignar", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var producto in productos)
                {
                    // Comprobar si existe ya un mensaje para este producto
                    var comprobar = Convert.ToInt32(cn.EjecutarSelect($"SELECT * FROM ProductMessage WHERE IDProducto = {producto.Key}"));

                    if (comprobar > 0)
                    {
                        // UPDATE
                        cn.EjecutarConsulta($"UPDATE ProductMessage SET ProductOfMessage = '{mensaje}' WHERE IDProducto = {producto.Key}");
                    }
                    else
                    {
                        // INSERT
                        cn.EjecutarConsulta($"INSERT INTO ProductMessage (IDProducto, ProductOfMessage, ProductMessageActivated) VALUES ('{producto.Key}', '{mensaje}', '1')");
                    }
                }
            }
            else if (propiedad == "MensajeInventario")
            {
                TextBox txtMensaje = (TextBox)this.Controls.Find("tbMensajeInventario", true)[0];

                var mensaje = txtMensaje.Text;

                if (string.IsNullOrWhiteSpace(mensaje))
                {
                    MessageBox.Show("Ingrese el mensaje para asignar", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var producto in productos)
                {
                    // Comprobar si existe ya un mensaje para este producto en inventario
                    var comprobar = Convert.ToInt32(cn.EjecutarSelect($"SELECT * FROM MensajesInventario WHERE IDUsuario = {FormPrincipal.userID} AND IDProducto = {producto.Key}"));

                    if (comprobar > 0)
                    {
                        // UPDATE
                        cn.EjecutarConsulta($"UPDATE MensajesInventario SET Mensaje = '{mensaje}' WHERE IDUsuario = {FormPrincipal.userID} AND IDProducto = {producto.Key}");
                    }
                    else
                    {
                        // INSERT
                        cn.EjecutarConsulta($"INSERT INTO MensajesInventario (IDUsuario, IDProducto, Mensaje, Activo) VALUES ('{FormPrincipal.userID}', '{producto.Key}', '{mensaje}', '1')");
                    }
                }
            }
            else if (propiedad == "Stock")
            {
                TextBox txtStock = (TextBox)this.Controls.Find("tbStock", true)[0];

                var stock = txtStock.Text;

                if (!string.IsNullOrWhiteSpace(stock))
                {
                    foreach (var producto in productos)
                    {
                        if (producto.Value == "P")
                        {
                            datos = new string[] { producto.Key.ToString(), stock, FormPrincipal.userID.ToString() };

                            cn.EjecutarConsulta(cs.ActualizarStockProductos(datos));
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese una cantidad para stock", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }    
            }
            else if (propiedad == "StockMinimo")
            {
                TextBox txtStock = (TextBox)this.Controls.Find("tbStockMinimo", true)[0];

                var stock = txtStock.Text;

                if (!string.IsNullOrWhiteSpace(stock))
                {
                    foreach (var producto in productos)
                    {
                        if (producto.Value == "P")
                        {
                            var consulta = $"UPDATE Productos SET StockMinimo = {stock} WHERE ID = {producto.Key} AND IDUsuario = {FormPrincipal.userID}";

                            cn.EjecutarConsulta(consulta);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese una cantidad para stock minimo", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (propiedad == "StockMaximo")
            {
                TextBox txtStock = (TextBox)this.Controls.Find("tbStockMaximo", true)[0];

                var stock = txtStock.Text;

                if (!string.IsNullOrWhiteSpace(stock))
                {
                    foreach (var producto in productos)
                    {
                        if (producto.Value == "P")
                        {
                            var consulta = $"UPDATE Productos SET StockNecesario = {stock} WHERE ID = {producto.Key} AND IDUsuario = {FormPrincipal.userID}";

                            cn.EjecutarConsulta(consulta);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese una cantidad para stock maximo", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (propiedad == "Precio")
            {
                TextBox txtPrecio = (TextBox)this.Controls.Find("tbPrecio", true)[0];

                var precioTmp = txtPrecio.Text;

                if (!string.IsNullOrWhiteSpace(precioTmp))
                {
                    var precio = float.Parse(precioTmp);

                    foreach (var producto in productos)
                    {
                        cn.EjecutarConsulta(cs.SetUpPrecioProductos(producto.Key, precio, FormPrincipal.userID));
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese el precio para asignar", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else if (propiedad == "NumeroRevision")
            {
                TextBox txtRevision = (TextBox)this.Controls.Find("tbNumeroRevision", true)[0];

                var numeroRevision = txtRevision.Text;

                if (!string.IsNullOrWhiteSpace(numeroRevision))
                {
                    foreach (var producto in productos)
                    {
                        var consulta = $"UPDATE Productos SET NumeroRevision = {numeroRevision} WHERE ID = {producto.Key} AND IDUsuario = {FormPrincipal.userID}";

                        cn.EjecutarConsulta(consulta);
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese el número de revisión para asignar", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (propiedad == "TipoIVA")
            {
                ComboBox combo = (ComboBox)this.Controls.Find("cbTipoIVA", true)[0];

                var iva = combo.SelectedValue.ToString();

                foreach (var producto in productos)
                {
                    cn.EjecutarConsulta($"UPDATE Productos SET Impuesto = '{iva}' WHERE ID = {producto.Key} AND IDUsuario = {FormPrincipal.userID}");
                }
            }
            else if (propiedad == "ClaveProducto")
            {
                TextBox txtClave = (TextBox)this.Controls.Find("tbClaveProducto", true).FirstOrDefault();

                var clave = txtClave.Text;

                if (!string.IsNullOrWhiteSpace(clave))
                {
                    foreach (var producto in productos)
                    {
                        cn.EjecutarConsulta($"UPDATE Productos SET ClaveProducto = '{clave}' WHERE ID = {producto.Key} AND IDUsuario = {FormPrincipal.userID}");
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese la clave de producto", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (propiedad == "ClaveUnidad")
            {
                TextBox txtClave = (TextBox)this.Controls.Find("tbClaveUnidad", true).FirstOrDefault();
                ComboBox combo = (ComboBox)this.Controls.Find("cbClaveUnidad", true).FirstOrDefault();

                var claveUnidad = txtClave.Text;
                var claveCombo = combo.SelectedValue.ToString();

                if (claveUnidad.Equals(claveCombo))
                {
                    foreach (var producto in productos)
                    {
                        cn.EjecutarConsulta($"UPDATE Productos SET UnidadMedida = '{claveUnidad}' WHERE ID = {producto.Key} AND IDUsuario = {FormPrincipal.userID}");
                    }
                }
                else
                {
                    MessageBox.Show("La clave de unidad no es válida", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (propiedad == "CorreosProducto")
            {
                var checkPrimero = (CheckBox)Controls.Find("CorreoPrecioProducto", true).First();
                var checkSegundo = (CheckBox)Controls.Find("CorreoStockProducto", true).First();
                var checkTercero = (CheckBox)Controls.Find("CorreoStockMinimo", true).First();
                var checkCuarto  = (CheckBox)Controls.Find("CorreoVentaProducto", true).First();

                var correoPrecioProducto = Convert.ToInt16(checkPrimero.Checked);
                var correoStockProducto  = Convert.ToInt16(checkSegundo.Checked);
                var correoStockMinimo    = Convert.ToInt16(checkTercero.Checked);
                var correoVentaProducto  = Convert.ToInt16(checkCuarto.Checked);

                foreach (var producto in productos)
                {
                    // Comprobar si existe registro en la tabla de correos
                    var comprobar = Convert.ToInt32(cn.EjecutarSelect($"SELECT * FROM CorreosProducto WHERE IDProducto = {producto.Key}"));

                    if (comprobar > 0)
                    {
                        // UPDATE
                        cn.EjecutarConsulta($"UPDATE CorreosProducto SET CorreoPrecioProducto = {correoPrecioProducto}, CorreoStockProducto = {correoStockProducto}, CorreoStockMinimo = {correoStockMinimo}, CorreoVentaProducto = {correoVentaProducto} WHERE IDUsuario = {FormPrincipal.userID} AND IDProducto = {producto.Key}");
                    }
                    else
                    {
                        // INSERT
                        cn.EjecutarConsulta($"INSERT INTO CorreosProducto (IDUsuario, IDProducto, CorreoPrecioProducto, CorreoStockProducto, CorreoStockMinimo, CorreoVentaProducto) VALUES ('{FormPrincipal.userID}', '{producto.Key}', '{correoPrecioProducto}', '{correoStockProducto}', '{correoStockMinimo}', '{correoVentaProducto}')");
                    }
                }
            }
            else if (propiedad == "Proveedor")
            {
                // Acceder al combobox de proveedores
                ComboBox combo = (ComboBox)this.Controls.Find("cbProveedor", true)[0];

                var idProveedor = Convert.ToInt32(combo.SelectedValue.ToString());
                var proveedor = combo.Text;
                
                if (idProveedor > 0)
                {
                    foreach (var producto in productos)
                    {
                        // Comprobar si existe registro en la tabla DetallesProducto
                        var existe = mb.DetallesProducto(producto.Key, FormPrincipal.userID);

                        datos = new string[] {
                            producto.Key.ToString(), FormPrincipal.userID.ToString(),
                            proveedor, idProveedor.ToString()
                        };

                        if (existe.Length > 0)
                        {
                            // Hacemos un UPDATE
                            cn.EjecutarConsulta(cs.GuardarProveedorProducto(datos, 1));
                        }
                        else
                        {
                            // Hacemos un INSERT
                            cn.EjecutarConsulta(cs.GuardarProveedorProducto(datos));
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Es necesario seleccionar un proveedor", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }  
            }
            else
            {
                ComboBox combo = (ComboBox)this.Controls.Find("cb" + propiedad, true)[0];

                var idPropiedad = combo.SelectedValue.ToString();
                var nombreOpcion = combo.Text;
                var nombrePanel = "panelContenido" + propiedad;

                foreach (var producto in productos)
                {
                    var existe = (bool)cn.EjecutarSelect($"SELECT * FROM DetallesProductoGenerales WHERE IDProducto = {producto.Key} AND IDUsuario = {FormPrincipal.userID} AND panelContenido = '{nombrePanel}'");

                    if (existe)
                    {
                        // UPDATE tabla DetallesProductoGenerales
                        cn.EjecutarConsulta($"UPDATE DetallesProductoGenerales SET IDDetalleGral = {idPropiedad} WHERE IDProducto = {producto.Key} AND IDUsuario = {FormPrincipal.userID}");
                    }
                    else
                    {
                        // INSERT tabla DetallesProductoGenerales
                        datos = new string[] {
                            producto.Key.ToString(), FormPrincipal.userID.ToString(),
                            idPropiedad, "1", nombrePanel
                        };

                        cn.EjecutarConsulta(cs.GuardarDetallesProductoGenerales(datos));
                    }
                }
            }

            Dispose();
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void CambioTexto(object sender, KeyEventArgs e)
        {
            var txtClave = sender as TextBox;
            var clave = txtClave.Text;

            var combo = (ComboBox)this.Controls.Find("cbClaveUnidad", true).FirstOrDefault();

            if (clavesUnidades.ContainsKey(clave))
            {
                combo.SelectedValue = clave;
            }
            else
            {
                combo.SelectedValue = "NoAplica";
            }
        }

        private void CambioComboBox(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;
            var clave = combo.SelectedValue.ToString();

            if (!clave.Equals("NoAplica"))
            {
                var txtClave = (TextBox)this.Controls.Find("tbClaveUnidad", true).FirstOrDefault();

                txtClave.Text = clave;
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
    }
}
