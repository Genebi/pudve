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
    public partial class AsignarPropiedad : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();
        Cargando cargando = new Cargando();

        string propiedad = string.Empty;

        Dictionary<int, string> productos;
        Dictionary<string, string> clavesUnidades;
        Dictionary<int, float> datosHistPrecio;
         
        public static Dictionary<int, string> modificarPrecio = new Dictionary<int, string>();
        //public static Dictionary<int, string> disminuirPrecio = new Dictionary<int, string>();

        bool stateChkMostrarMensaje = false;

        string cantidadCompra = string.Empty;

        string mensajeCompra = string.Empty;

        private const char SignoDecimal = '.';

        public AsignarPropiedad(object propiedad)
        {
            InitializeComponent();

            this.propiedad = propiedad.ToString().Replace(" ", "_");

            productos = Productos.productosSeleccionados;

            datosHistPrecio = Productos.productosSeleccionadosParaHistorialPrecios;
        }

        private void AsignarPropiedad_Load(object sender, EventArgs e)
        {
            lbNombrePropiedad.Text = $"ASIGNAR {propiedad.ToUpper().Replace("_"," ")}";

            clavesUnidades = mb.CargarClavesUnidades();

            CargarPropiedad();
        }

        private void CargarPropiedad()
        {
            Font fuente = new Font("Century Gothic", 10.0f);
            Font fuenteChica = new Font("Century Gothic", 8.0f);

            if (propiedad == "MensajeVentas")
            {
                TextBox tbMensaje = new TextBox();
                tbMensaje.Name = "tb" + propiedad;
                tbMensaje.Width = 200;
                tbMensaje.Height = 40;
                tbMensaje.CharacterCasing = CharacterCasing.Upper;
                tbMensaje.Font = fuente;
                tbMensaje.Multiline = true;
                tbMensaje.ScrollBars = ScrollBars.Vertical;
                tbMensaje.Location = new Point(65, 95);

                Label lbCantidadCompra = new Label();
                lbCantidadCompra.Text = "Cantidad minima en la venta para mostrar mensaje:";
                lbCantidadCompra.AutoSize = true;
                lbCantidadCompra.Location = new Point(15, 42);

                Label lbMostrarMensaje = new Label();
                lbMostrarMensaje.Text = "Mostrar mensaje:";
                lbMostrarMensaje.AutoSize = true;
                lbMostrarMensaje.Location = new Point(15, 70);

                TextBox txtCantidadCompra = new TextBox();
                txtCantidadCompra.Name = "txtCantidadCompra";
                txtCantidadCompra.Width = 35;
                txtCantidadCompra.Height = 20;
                txtCantidadCompra.Leave += new EventHandler(txtCantidadCompra_Leave);
                txtCantidadCompra.KeyPress += new KeyPressEventHandler(txtCantidadCompra_Keypress);
                txtCantidadCompra.Location = new Point(265, 40);

                CheckBox chkMostrarOcultarMensaje = new CheckBox();
                chkMostrarOcultarMensaje.Name = "chkMostrarMensaje";
                chkMostrarOcultarMensaje.Checked = true;
                stateChkMostrarMensaje = chkMostrarOcultarMensaje.Checked;
                chkMostrarOcultarMensaje.CheckedChanged += new EventHandler(chkMostrarOcultarMensaje_CheckedChanged);
                chkMostrarOcultarMensaje.Height = 15;
                chkMostrarOcultarMensaje.Width = 15;
                chkMostrarOcultarMensaje.Location = new Point(100,72);

                panelContenedor.Controls.Add(tbMensaje);
                panelContenedor.Controls.Add(chkMostrarOcultarMensaje);
                panelContenedor.Controls.Add(lbMostrarMensaje);
                panelContenedor.Controls.Add(lbCantidadCompra);
                panelContenedor.Controls.Add(txtCantidadCompra);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarMensaje"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarMensaje"));
            }
            else if (propiedad == "MensajeInventario")
            {
                TextBox tbMensaje = new TextBox();
                tbMensaje.Name = "tb" + propiedad;
                tbMensaje.Width = 200;
                tbMensaje.Height = 40;
                tbMensaje.Leave += new EventHandler(tbMensaje_Leave);
                tbMensaje.CharacterCasing = CharacterCasing.Upper;
                tbMensaje.Font = fuente;
                tbMensaje.Multiline = true;
                tbMensaje.ScrollBars = ScrollBars.Vertical;
                tbMensaje.Location = new Point(65, 70);

                Label lbMostrarMensaje = new Label();
                lbMostrarMensaje.Text = "Mostrar mensaje:";
                lbMostrarMensaje.AutoSize = true;
                lbMostrarMensaje.Location = new Point(15, 50);

                CheckBox chkMostrarOcultarMensaje = new CheckBox();
                chkMostrarOcultarMensaje.Name = "chkMostrarMensajeInventario";
                chkMostrarOcultarMensaje.Checked = false;
                stateChkMostrarMensaje = chkMostrarOcultarMensaje.Checked;
                chkMostrarOcultarMensaje.CheckedChanged += new EventHandler(chkMostrarOcultarMensajeInventario_CheckedChanged);
                chkMostrarOcultarMensaje.Height = 15;
                chkMostrarOcultarMensaje.Width = 15;
                chkMostrarOcultarMensaje.Location = new Point(100, 52);

                panelContenedor.Controls.Add(chkMostrarOcultarMensaje);
                panelContenedor.Controls.Add(lbMostrarMensaje);
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
                DialogResult respuesta = MessageBox.Show("Al modificar el precio de los productos/servicios/combos todos aquellos que tengan descuentos agregados estos mismos descuentos serán eliminados y será necesario agregarlos nuevamente, ¿Está de acuerdo con esta operación?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (respuesta == DialogResult.Yes)
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
                else
                {
                    Dispose();
                }
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
            else if (propiedad == "Correos")
            {
                //CheckBox primerCB = new CheckBox();
                //primerCB.Text = "Correo al modificar precio de producto";
                //primerCB.Location = new Point(40, 40);
                //primerCB.Name = "CorreoPrecioProducto";
                //primerCB.Width = 300;
                //primerCB.Font = fuenteChica;

                //CheckBox segundoCB = new CheckBox();
                //segundoCB.Text = "Correo al modificar stock de producto";
                //segundoCB.Location = new Point(40, 60);
                //segundoCB.Name = "CorreoStockProducto";
                //segundoCB.Width = 300;
                //segundoCB.Font = fuenteChica;

                //CheckBox tercerCB = new CheckBox();
                //tercerCB.Text = "Correo al llegar a stock minimo";
                //tercerCB.Location = new Point(40, 80);
                //tercerCB.Name = "CorreoStockMinimo";
                //tercerCB.Width = 300;
                //tercerCB.Font = fuenteChica;

                CheckBox cuartoCB = new CheckBox
                {
                    Text = "Correo al hacer venta de producto",
                    Location = new Point(40, 100),
                    Name = "CorreoVentaProducto",
                    Width = 300,
                    Font = fuenteChica
                };

                var checkboxes = new CheckBox[] { cuartoCB };

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
                    cbPropiedad.Name = "cb" + propiedad.Replace("_"," ");
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

        private void txtCantidadCompra_Keypress(object sender, KeyPressEventArgs e)
        {
            var textBox = (TextBox)sender;
            // Si el carácter pulsado no es un carácter válido se anula
            e.Handled = !char.IsDigit(e.KeyChar) // No es dígito
                        && !char.IsControl(e.KeyChar) // No es carácter de control (backspace)
                        && (e.KeyChar != SignoDecimal // No es signo decimal o es la 1ª posición o ya hay un signo decimal
                            || textBox.SelectionStart == 0
                            || textBox.Text.Contains(SignoDecimal));
        }

        private void tbMensaje_Leave(object sender, EventArgs e)
        {
            TextBox mensajeMostrarCompra = (TextBox)sender;
            mensajeCompra = mensajeMostrarCompra.Text;
        }

        private void txtCantidadCompra_Leave(object sender, EventArgs e)
        {
            TextBox textoCompra = (TextBox)sender;
            if (!string.IsNullOrWhiteSpace(textoCompra.Text))
            {
                cantidadCompra = Convert.ToDecimal(textoCompra.Text).ToString();
            }
            
        }

        private void chkMostrarOcultarMensajeInventario_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            if (checkbox.Name.Equals("chkMostrarMensajeInventario") && checkbox.Checked == true)
            {
                stateChkMostrarMensaje = true;
            }
            else
            {
                stateChkMostrarMensaje = false;
            }
        }

        private void chkMostrarOcultarMensaje_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            if (checkbox.Name.Equals("chkMostrarMensaje") && checkbox.Checked == true)
            {
                stateChkMostrarMensaje = true;
            }
            else
            {
                stateChkMostrarMensaje = false;
            }
        }

        private Button GenerarBoton(int tipo, string nombre, int ejeY = 155)
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

        private async void botonAceptar_Click(object sender, EventArgs e)
        {
            
                cargando.Show();

                await Task.Run(() =>
                {
                    Thread.Sleep(2000);
                });

                OperacionBoton();

                cargando.Close();

                Dispose();
        }

        private void OperacionBoton()
        {
            string[] datos;
           
                if (propiedad == "MensajeVentas")
                {

                TextBox txtMensaje = (TextBox)this.Controls.Find("tbMensajeVentas", true)[0];
                var mensaje = txtMensaje.Text;

                TextBox txtCantidadCompra = (TextBox)this.Controls.Find("txtCantidadCompra", true)[0];
                var cantidad = txtCantidadCompra.Text;

                if (!string.IsNullOrWhiteSpace(mensaje) && !string.IsNullOrWhiteSpace(cantidad))
                {

                    if (stateChkMostrarMensaje.Equals(true))
                    {
                        foreach (var producto in productos)
                        {
                            using (DataTable datosV = cn.CargarDatos($"SELECT * FROM productmessage WHERE IDProducto = '{producto.Key}'"))
                            {
                                if (!datosV.Rows.Count.Equals(0))
                                {
                                    cn.EjecutarConsulta($"UPDATE productmessage SET ProductMessageActivated = 1 WHERE IDProducto = '{producto.Key}'");
                                }
                                else
                                {
                                    var status = 0;
                                    if (stateChkMostrarMensaje)
                                    {
                                        status = 1;
                                    }
                                    else
                                    {
                                        status = 0;
                                    }
                                    cn.EjecutarConsulta($"INSERT INTO productmessage (IDProducto,ProductOfMessage,ProductMessageActivated,CantidadMinimaDeCompra) VALUES ('{producto.Key}','{mensajeCompra}','{status}','{cantidadCompra}')");
                                }
                            }

                        }
                    }
                    else
                    {
                        foreach (var producto in productos)
                        {
                            using (DataTable datosV = cn.CargarDatos($"SELECT * FROM productmessage WHERE IDProducto = '{producto.Key}'"))
                            {
                                if (!datosV.Rows.Count.Equals(0))
                                {
                                    cn.EjecutarConsulta($"UPDATE productmessage SET ProductMessageActivated = 0 WHERE IDProducto = '{producto.Key}'");
                                }
                                else
                                {
                                    var status = 0;
                                    if (stateChkMostrarMensaje)
                                    {
                                        status = 1;
                                    }
                                    else
                                    {
                                        status = 0;
                                    }
                                    cn.EjecutarConsulta($"INSERT INTO productmessage (IDProducto,ProductOfMessage,ProductMessageActivated,CantidadMinimaDeCompra) VALUES ('{producto.Key}','{mensajeCompra}','{status}','{cantidadCompra}')");
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Test Mensaje incompleto");
                }
                //TextBox txtMensaje = (TextBox)this.Controls.Find("tbMensajeVentas", true)[0];

                //var mensaje = txtMensaje.Text;
                //var consulta = "INSERT IGNORE INTO ProductMessage (ID, IDProducto, ProductOfMessage) VALUES";
                //var valores = string.Empty;



                //if (string.IsNullOrWhiteSpace(mensaje))
                //{
                //    MessageBox.Show("Ingrese el mensaje para asignar", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                //foreach (var producto in productos)
                //{
                //    // Comprobar si existe ya un mensaje para este producto
                //    var id = Convert.ToInt32(cn.EjecutarSelect($"SELECT * FROM ProductMessage WHERE IDProducto = {producto.Key}", 1));


                //    TextBox txtCantidadMinima = (TextBox)this.Controls.Find("txtCantidadCompra", true)[0];
                //    var cantMinima = txtCantidadMinima.Text;
                //    cn.EjecutarConsulta(cs.actualizarCompraMinimaMultiple(producto.Key,Convert.ToInt32(cantMinima)));

                //    if (id > 0)
                //    {
                //        valores += $"({id}, {producto.Key}, '{mensaje}'),";
                //    }
                //    else
                //    {
                //        valores += $"(null, {producto.Key}, '{mensaje}'),";
                //    }
                //}

                //if (!string.IsNullOrWhiteSpace(valores))
                //{
                //    valores = valores.TrimEnd(',');

                //    consulta += valores + " ON DUPLICATE KEY UPDATE ID = VALUES(ID), IDProducto = VALUES(IDProducto), ProductOfMessage = VALUES(ProductOfMessage);";

                //    cn.EjecutarConsulta(consulta);
                //}
            }
            else if (propiedad == "MensajeInventario")
            {

                if (stateChkMostrarMensaje.Equals(true))
                {
                    foreach (var producto in productos)
                    {
                        using (DataTable datosI = cn.CargarDatos($"SELECT * FROM mensajesinventario WHERE IDProducto = '{producto.Key}'"))
                        {
                            if (!datosI.Rows.Count.Equals(0))
                            {
                                cn.EjecutarConsulta($"UPDATE mensajesinventario SET Activo = 1 WHERE IDProducto = '{producto.Key}'");
                            }
                            else
                            {
                                var status = 0;
                                if (stateChkMostrarMensaje)
                                {
                                    status = 1;
                                }
                                else
                                {
                                    status = 0;
                                }
                                cn.EjecutarConsulta($"INSERT INTO mensajesinventario (IDUsuario,IDProducto,Mensaje,Activo) VALUES ({FormPrincipal.userID},'{producto.Key}','{mensajeCompra}','{status}')");
                            }
                        }

                    }
                }
                else
                {
                    foreach (var producto in productos)
                    {
                        using (DataTable datosI = cn.CargarDatos($"SELECT * FROM mensajesinventario WHERE IDProducto = '{producto.Key}'"))
                        {
                            if (!datosI.Rows.Count.Equals(0))
                            {
                                cn.EjecutarConsulta($"UPDATE mensajesinventario SET Activo = 0 WHERE IDProducto = '{producto.Key}'");
                            }
                            else
                            {
                                var status = 0;
                                if (stateChkMostrarMensaje)
                                {
                                    status = 1;
                                }
                                else
                                {
                                    status = 0;
                                }
                                cn.EjecutarConsulta($"INSERT INTO mensajesinventario (IDUsuario,IDProducto,Mensaje,Activo) VALUES ({FormPrincipal.userID},{producto.Key},'{mensajeCompra}','{status}')");
                            }
                        }
                    }
                }

                //TextBox txtMensaje = (TextBox)this.Controls.Find("tbMensajeInventario", true)[0];

                //var mensaje = txtMensaje.Text;
                //var consulta = "INSERT IGNORE INTO MensajesInventario (ID, IDUsuario, IDProducto, Mensaje) VALUES";
                //var valores = string.Empty;

                //if (string.IsNullOrWhiteSpace(mensaje))
                //{
                //    MessageBox.Show("Ingrese el mensaje para asignar", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                //foreach (var producto in productos)
                //{
                //    // Comprobar si existe ya un mensaje para este producto en inventario
                //    var id = Convert.ToInt32(cn.EjecutarSelect($"SELECT * FROM MensajesInventario WHERE IDUsuario = {FormPrincipal.userID} AND IDProducto = {producto.Key}", 1));

                //    if (id > 0)
                //    {
                //        valores += $"({id}, {FormPrincipal.userID}, {producto.Key}, '{mensaje}'),";
                //    }
                //    else
                //    {
                //        valores += $"(null, {FormPrincipal.userID}, {producto.Key}, '{mensaje}'),";
                //    }
                //}

                //if (!string.IsNullOrWhiteSpace(valores))
                //{
                //    valores = valores.TrimEnd(',');

                //    consulta += valores + " ON DUPLICATE KEY UPDATE ID = VALUES(ID), IDUsuario = VALUES(IDUsuario), IDProducto = VALUES(IDProducto), Mensaje = VALUES(Mensaje);";

                //    cn.EjecutarConsulta(consulta);
                //}
            }
            else if (propiedad == "Stock")
            {
                TextBox txtStock = (TextBox)this.Controls.Find("tbStock", true)[0];

                var stock = txtStock.Text;
                var html = string.Empty;

                var consulta = "INSERT IGNORE INTO Productos (ID, Stock) VALUES";
                var segundaConsulta = "INSERT IGNORE INTO RevisarInventario (ID, StockFisico) VALUES";
                var valores = string.Empty;

                if (!string.IsNullOrWhiteSpace(stock))
                {
                    foreach (var producto in productos)
                    {
                        if (producto.Value == "P")
                        {
                            var datosConfig = mb.ComprobarConfiguracion();

                            if (datosConfig.Count > 0)
                            {
                                if (Convert.ToInt16(datosConfig[1]) == 1)
                                {
                                    var configProducto = mb.ComprobarCorreoProducto(producto.Key);

                                    if (configProducto.Count > 0)
                                    {
                                        if (configProducto[1] == 1)
                                        {
                                            // Obtenemos los datos del producto para el email
                                            var datosProducto = cn.BuscarProducto(producto.Key, FormPrincipal.userID);

                                            html += $@"<li>
                                                    <span style='color: red;'>{datosProducto[1]}</span> 
                                                    --- <b>STOCK ANTERIOR:</b> 
                                                    <span style='color: red;'>{datosProducto[4]}</span> 
                                                    --- <b>STOCK NUEVO:</b> 
                                                    <span style='color: red;'>{stock}</span>
                                                </li>";
                                        }
                                    }
                                }
                            }

                            valores += $"({producto.Key}, {stock}),";

                            //datos = new string[] { producto.Key.ToString(), stock, FormPrincipal.userID.ToString() };

                            //cn.EjecutarConsulta(cs.ActualizarStockProductos(datos));
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(valores))
                    {
                        valores = valores.TrimEnd(',');

                        consulta += valores + " ON DUPLICATE KEY UPDATE ID = VALUES(ID), Stock = VALUES(Stock);";

                        segundaConsulta += valores + "ON DUPLICATE KEY UPDATE ID = VALUES(ID), StockFisico = VALUES(StockFisico);";

                        cn.EjecutarConsulta(consulta);
                        cn.EjecutarConsulta(segundaConsulta);
                    }

                    if (!string.IsNullOrWhiteSpace(html))
                    {
                        // Ejecutar hilo para enviar notificacion
                        datos = new string[] { html, "", "", "", "ASIGNAR", "" };

                        Thread notificacion = new Thread(
                            () => Utilidades.CambioStockProductoEmail(datos, 1)
                        );

                        notificacion.Start();
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
                var consulta = "INSERT IGNORE INTO Productos (ID, StockMinimo) VALUES";
                var valores = string.Empty;

                if (!string.IsNullOrWhiteSpace(stock))
                {
                    foreach (var producto in productos)
                    {
                        if (producto.Value == "P")
                        {
                            valores += $"({producto.Key}, {stock}),";

                            //cn.EjecutarConsulta(consulta);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(valores))
                    {
                        valores = valores.TrimEnd(',');

                        consulta += valores + " ON DUPLICATE KEY UPDATE ID = VALUES(ID), StockMinimo = VALUES(StockMinimo);";

                        cn.EjecutarConsulta(consulta);
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
                var consulta = "INSERT IGNORE INTO Productos (ID, StockNecesario) VALUES";
                var valores = string.Empty;

                if (!string.IsNullOrWhiteSpace(stock))
                {
                    foreach (var producto in productos)
                    {
                        if (producto.Value == "P")
                        {
                            valores += $"({producto.Key}, {stock}),";

                            //cn.EjecutarConsulta(consulta);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(valores))
                    {
                        valores = valores.TrimEnd(',');

                        consulta += valores + " ON DUPLICATE KEY UPDATE ID = VALUES(ID), StockNecesario = VALUES(StockNecesario);";

                        cn.EjecutarConsulta(consulta);
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
                    //Thread hilo = new Thread(() => asignarActualizarInventario(datosHistPrecio, Convert.ToInt32(precioTmp)));
                    //hilo.Start();
                    //asignarActualizarInventario(datosHistPrecio, Convert.ToInt32(precioTmp));

                    var precio = float.Parse(precioTmp);
                    var html = string.Empty;
                    var consulta = "INSERT IGNORE INTO Productos (ID, Precio) VALUES";
                    var valores = string.Empty;
                    var empleado = "0";

                    if (FormPrincipal.userNickName.Contains('@'))
                    {
                        empleado = cs.buscarIDEmpleado(FormPrincipal.userNickName);
                    }

                    // Guardamos los datos en la tabla historial de precios
                    foreach (var dato in datosHistPrecio)
                    {
                        var idProd = dato.Key;
                        var precioActual = dato.Value;


                        var info = new string[] {
                        FormPrincipal.userID.ToString(), empleado, idProd.ToString(),
                        precioActual.ToString(), txtPrecio.Text,
                        "ASIGNAR PRODUCTO", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        };

                        cn.EjecutarConsulta(cs.GuardarHistorialPrecios(info));
                    }

                    foreach (var producto in productos)
                    {
                        var datosConfig = mb.ComprobarConfiguracion();

                        if (datosConfig.Count > 0)
                        {
                            if (Convert.ToInt16(datosConfig[0]) == 1)
                            {
                                var configProducto = mb.ComprobarCorreoProducto(producto.Key);

                                if (configProducto.Count > 0)
                                {
                                    if (configProducto[0] == 1)
                                    {
                                        // Obtenemos los datos del producto para el email
                                        var datosProducto = cn.BuscarProducto(producto.Key, FormPrincipal.userID);

                                        html += $@"<li>
                                                    <span style='color: red;'>{datosProducto[1]}</span> 
                                                    --- <b>PRECIO ANTERIOR:</b> 
                                                    <span style='color: red;'>${float.Parse(datosProducto[2]).ToString("N2")}</span> 
                                                    --- <b>PRECIO NUEVO:</b> 
                                                    <span style='color: red;'>${precio.ToString("N2")}</span>
                                                </li>";
                                    }
                                }
                            }
                        }

                        // Actualizar el nuevo precio

                        valores += $"({producto.Key}, {precio}),";

                        // Eliminamos los descuentos del producto
                        cn.EjecutarConsulta($"DELETE FROM DescuentoCliente WHERE IDProducto = {producto.Key}");
                        cn.EjecutarConsulta($"DELETE FROM DescuentoMayoreo WHERE IDProducto = {producto.Key}");
                        //cn.EjecutarConsulta(cs.SetUpPrecioProductos(producto.Key, precio, FormPrincipal.userID));
                    }

                    if (!string.IsNullOrWhiteSpace(valores))
                    {
                        valores = valores.TrimEnd(',');

                        consulta += valores + " ON DUPLICATE KEY UPDATE ID = VALUES(ID), Precio = VALUES(Precio)";

                        cn.EjecutarConsulta(consulta);
                    }

                    if (!string.IsNullOrWhiteSpace(html))
                    {
                        // Ejecutar hilo para enviar notificacion
                        datos = new string[] { html, "", "", "ASIGNAR" };

                        Thread notificacion = new Thread(
                            () => Utilidades.CambioPrecioProductoEmail(datos, 1)
                        );

                        notificacion.Start();
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
                var consulta = "INSERT IGNORE INTO Productos (ID, NumeroRevision) VALUES";
                var valores = string.Empty;

                if (!string.IsNullOrWhiteSpace(numeroRevision))
                {
                    foreach (var producto in productos)
                    {
                        valores += $"({producto.Key}, {numeroRevision}),";

                        //cn.EjecutarConsulta(consulta);
                    }

                    if (!string.IsNullOrWhiteSpace(valores))
                    {
                        valores = valores.TrimEnd(',');

                        consulta += valores + " ON DUPLICATE KEY UPDATE ID = VALUES(ID), NumeroRevision = VALUES(NumeroRevision);";

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
                var consulta = "INSERT IGNORE INTO Productos (ID, Impuesto) VALUES";
                var valores = string.Empty;

                foreach (var producto in productos)
                {
                    valores += $"({producto.Key}, '{iva}'),";

                    //cn.EjecutarConsulta($"UPDATE Productos SET Impuesto = '{iva}' WHERE ID = {producto.Key} AND IDUsuario = {FormPrincipal.userID}");
                }

                if (!string.IsNullOrWhiteSpace(valores))
                {
                    valores = valores.TrimEnd(',');

                    consulta += valores + " ON DUPLICATE KEY UPDATE ID = VALUES(ID), Impuesto = VALUES(Impuesto);";

                    cn.EjecutarConsulta(consulta);
                }
            }
            else if (propiedad == "ClaveProducto")
            {
                TextBox txtClave = (TextBox)this.Controls.Find("tbClaveProducto", true).FirstOrDefault();

                var clave = txtClave.Text;
                var consulta = "INSERT IGNORE INTO Productos (ID, ClaveProducto) VALUES";
                var valores = string.Empty;

                if (!string.IsNullOrWhiteSpace(clave))
                {
                    foreach (var producto in productos)
                    {
                        valores += $"({producto.Key}, '{clave}'),";
                        //cn.EjecutarConsulta($"UPDATE Productos SET ClaveProducto = '{clave}' WHERE ID = {producto.Key} AND IDUsuario = {FormPrincipal.userID}");
                    }

                    if (!string.IsNullOrWhiteSpace(valores))
                    {
                        valores = valores.TrimEnd(',');

                        consulta += valores + " ON DUPLICATE KEY UPDATE ID = VALUES(ID), ClaveProducto = VALUES(ClaveProducto);";

                        cn.EjecutarConsulta(consulta);
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
                var consulta = "INSERT IGNORE INTO Productos (ID, UnidadMedida) VALUES";
                var valores = string.Empty;

                if (claveUnidad.Equals(claveCombo))
                {
                    foreach (var producto in productos)
                    {
                        valores += $"({producto.Key}, '{claveUnidad}'),";
                        //cn.EjecutarConsulta($"UPDATE Productos SET UnidadMedida = '{claveUnidad}' WHERE ID = {producto.Key} AND IDUsuario = {FormPrincipal.userID}");
                    }

                    if (!string.IsNullOrWhiteSpace(valores))
                    {
                        valores = valores.TrimEnd(',');

                        consulta += valores + " ON DUPLICATE KEY UPDATE ID = VALUES(ID), UnidadMedida = VALUES(UnidadMedida);";

                        cn.EjecutarConsulta(consulta);
                    }
                }
                else
                {
                    MessageBox.Show("La clave de unidad no es válida", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (propiedad == "Correos")
            {
                //var checkPrimero = (CheckBox)Controls.Find("CorreoPrecioProducto", true).First();
                //var checkSegundo = (CheckBox)Controls.Find("CorreoStockProducto", true).First();
                //var checkTercero = (CheckBox)Controls.Find("CorreoStockMinimo", true).First();
                var checkCuarto = (CheckBox)Controls.Find("CorreoVentaProducto", true).First();

                //var correoPrecioProducto = Convert.ToInt16(checkPrimero.Checked);
                //var correoStockProducto = Convert.ToInt16(checkSegundo.Checked);
                //var correoStockMinimo = Convert.ToInt16(checkTercero.Checked);
                var correoVentaProducto = Convert.ToInt16(checkCuarto.Checked);

                var consulta = "INSERT IGNORE INTO CorreosProducto (ID, IDUsuario, IDProducto, CorreoVentaProducto) VALUES";
                var valores = string.Empty;

                foreach (var producto in productos)
                {
                    // Comprobar si existe registro en la tabla de correos
                    var id = Convert.ToInt32(cn.EjecutarSelect($"SELECT * FROM CorreosProducto WHERE IDProducto = {producto.Key}", 1));

                    if (id > 0)
                    {
                        valores += $"({id}, {FormPrincipal.userID}, {producto.Key}, {correoVentaProducto}),";
                    }
                    else
                    {
                        valores += $"(null, {FormPrincipal.userID}, {producto.Key}, {correoVentaProducto}),";
                    }
                }

                if (!string.IsNullOrWhiteSpace(valores))
                {
                    valores = valores.TrimEnd(',');

                    consulta += valores + " ON DUPLICATE KEY UPDATE ID = VALUES(ID), IDUsuario = VALUES(IDUsuario), IDProducto = VALUES(IDProducto), CorreoVentaProducto = VALUES(CorreoVentaProducto);";

                    cn.EjecutarConsulta(consulta);
                }
            }
            else if (propiedad == "Proveedor")
            {
                // Acceder al combobox de proveedores
                ComboBox combo = (ComboBox)this.Controls.Find("cbProveedor", true)[0];

                var idProveedor = Convert.ToInt32(combo.SelectedValue.ToString());
                var proveedor = combo.Text;
                var consulta = "INSERT IGNORE INTO DetallesProducto (ID, IDProducto, IDUsuario, Proveedor, IDProveedor) VALUES";
                var valores = string.Empty;

                if (idProveedor > 0)
                {
                    foreach (var producto in productos)
                    {
                        // Comprobar si existe registro en la tabla DetallesProducto
                        var existe = mb.DetallesProducto(producto.Key, FormPrincipal.userID);

                        if (existe.Length > 0)
                        {
                            valores += $"({existe[0]}, {producto.Key}, {FormPrincipal.userID}, '{proveedor}', {idProveedor}),";
                        }
                        else
                        {
                            valores += $"(null, {producto.Key}, {FormPrincipal.userID}, '{proveedor}', {idProveedor}),";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(valores))
                    {
                        valores = valores.TrimEnd(',');

                        consulta += valores + " ON DUPLICATE KEY UPDATE ID = VALUES(ID), IDProducto = VALUES(IDProducto), IDUsuario = VALUES(IDUsuario), Proveedor = VALUES(Proveedor), IDProveedor = VALUES(IDProveedor);";

                        cn.EjecutarConsulta(consulta);
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
                ComboBox combo = (ComboBox)this.Controls.Find("cb" + propiedad.Replace("_"," "), true)[0];

                var idPropiedad = combo.SelectedValue.ToString();
                var nombreOpcion = combo.Text;
                var nombrePanel = "panelContenido" + propiedad;
                var consulta = "INSERT IGNORE INTO DetallesproductoGenerales (ID, IDProducto, IDUsuario, IDDetalleGral, StatusDetalleGral, panelContenido) VALUES";
                var valores = string.Empty;

                foreach (var producto in productos)
                {
                    var existe = (bool)cn.EjecutarSelect($"SELECT * FROM DetallesProductoGenerales WHERE IDProducto = {producto.Key} AND IDUsuario = {FormPrincipal.userID} AND panelContenido = '{nombrePanel}'");

                    if (existe)
                    {
                        // UPDATE tabla DetallesProductoGenerales
                        var info = mb.DetallesProductoGralPorPanel(nombrePanel, FormPrincipal.userID, producto.Key);
                        //var idDetalle = info[3];

                        //cn.EjecutarConsulta($"UPDATE DetallesProductoGenerales SET IDDetalleGral = {idPropiedad} WHERE IDProducto = {producto.Key} AND IDUsuario = {FormPrincipal.userID} AND IDDetalleGral = {idDetalle}");

                        valores += $"({info[0]}, {producto.Key}, {FormPrincipal.userID}, {idPropiedad}, {info[4]}, '{nombrePanel}'),";
                    }
                    else
                    {
                        valores += $"(null, {producto.Key}, {FormPrincipal.userID}, {idPropiedad}, '1', '{nombrePanel}'),";
                        // INSERT tabla DetallesProductoGenerales
                        //datos = new string[] {
                        //    producto.Key.ToString(), FormPrincipal.userID.ToString(),
                        //    idPropiedad, "1", nombrePanel
                        //};

                        //cn.EjecutarConsulta(cs.GuardarDetallesProductoGenerales(datos));
                    }
                }

                if (!string.IsNullOrWhiteSpace(valores))
                {
                    valores = valores.TrimEnd(',');

                    consulta += valores + " ON DUPLICATE KEY UPDATE ID = VALUES(ID), IDProducto = VALUES(IDProducto), IDUsuario = VALUES(IDUsuario), IDDetalleGral = VALUES(IDDetalleGral), StatusDetalleGral = VALUES(StatusDetalleGral), panelContenido = VALUES(panelContenido);";

                    cn.EjecutarConsulta(consulta);
                }
            }
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

        private void AsignarPropiedad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Application.OpenForms.OfType<Cargando>().Count() == 1)
            {
                e.Cancel = true;

                Application.OpenForms.OfType<Cargando>().First().BringToFront();
            }
        }

        private void asignarActualizarInventario(Dictionary<int, float> recorrerDiccionario, int nuevoPrecio)
        {
            modificarPrecio.Clear();
            //disminuirPrecio.Clear();

            var datoUsuario = FormPrincipal.userNickName;
            var empleado = "0";

            if (datoUsuario.Contains('@'))
            {
                empleado = cs.buscarIDEmpleado(datoUsuario);
            }

            var codigos = Productos.diccionarioAjustePrecio;

            TextBox txtPrecio = (TextBox)this.Controls.Find("tbPrecio", true)[0];

            var precioTmp = txtPrecio.Text;

            foreach (var dato in codigos)
            {
                var idProd = dato.Key;
                var tipoProducto = dato.Value;
                
                separarDiccionario(idProd, nuevoPrecio, tipoProducto, float.Parse(precioTmp));
            }

            //Verificar que el diccionario no esta vacio. 
            string cadenaCompleta = string.Empty;
            if (!modificarPrecio.Count.Equals(0))
            {//Agregar datos en tabla de aumentar
                foreach (KeyValuePair<int, string> item in modificarPrecio)
                {
                    cadenaCompleta += item.ToString();
                }
                //remplazar caracteress y comas para hacer el filtro por ID
                cadenaCompleta = cadenaCompleta.Replace("[", "");
                cadenaCompleta = cadenaCompleta.Replace("]", "");
                cadenaCompleta = cadenaCompleta.Replace("PQ", "");
                cadenaCompleta = cadenaCompleta.Replace("P", "");
                cadenaCompleta = cadenaCompleta.Replace("S", "");
                cadenaCompleta = cadenaCompleta.Replace(", ,", ",");
                cadenaCompleta = cadenaCompleta.Replace(" ", "");
                cadenaCompleta = cadenaCompleta.Remove(cadenaCompleta.Length - 1);

                var separarId = cadenaCompleta.Split(',');
                
                foreach (var item in separarId)
                {
                    var fechaActual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    var datos = datosProducto(Convert.ToInt32(item));

                    var datoObtenidoPrecio = float.Parse(datos[3].ToString());
                    var newPrecio = float.Parse(precioTmp);
                    //var operacionPrecio = 0f;


                    //if (datoObtenidoPrecio > newPrecio)
                    //{
                    //    operacionPrecio = newPrecio - datoObtenidoPrecio;
                    //}
                    //else if (datoObtenidoPrecio < newPrecio)
                    //{
                    //    operacionPrecio = datoObtenidoPrecio - newPrecio;
                    //}

                    //var sentenciaAgregarInfoAumentar = "INSERT INTO DGVAumentarInventario (IdProducto, NombreProducto, StockActual, DiferenciaUnidades, NuevoStock, Precio, Clave, Codigo, Fecha, NoRevision, StatusActualizacion, NombreEmisor, Comentarios, ValorUnitario, IDUsuario) VALUES";
                    //sentenciaAgregarInfoAumentar += $"('{datos[0]}', '{datos[1]}', '{datos[2]}', '{""}','{datos[2]}', '{precioTmp}', '{datos[4]}', '{datos[5]}', '{fechaActual}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{FormPrincipal.userID}')";

                    //cn.EjecutarConsulta(sentenciaAgregarInfoAumentar);
                    //sentenciaAgregarInfoAumentar = string.Empty;

                    var info = new string[] {
                        FormPrincipal.userID.ToString(), empleado, item.ToString(), datoObtenidoPrecio.ToString(), newPrecio.ToString(), "ASIGNAR PRODUCTO", fechaActual
                    };
                    cn.EjecutarConsulta(cs.GuardarHistorialPrecios(info));
                }
            }

            //if (!disminuirPrecio.Count.Equals(0))
            //{//Agregar datos en tabla de disminuir
            //    foreach (KeyValuePair<int, string> item in disminuirPrecio)
            //    {
            //        cadenaCompleta += item.ToString();
            //    }
            //    //remplazar caracteress y comas para hacer el filtro por ID
            //    cadenaCompleta = cadenaCompleta.Replace("[", "");
            //    cadenaCompleta = cadenaCompleta.Replace("]", "");
            //    cadenaCompleta = cadenaCompleta.Replace("P", "");
            //    cadenaCompleta = cadenaCompleta.Replace("PQ", "");
            //    cadenaCompleta = cadenaCompleta.Replace("S", "");
            //    cadenaCompleta = cadenaCompleta.Replace(", ,", ",");
            //    cadenaCompleta = cadenaCompleta.Replace(" ", "");
            //    cadenaCompleta = cadenaCompleta.Remove(cadenaCompleta.Length - 1);

            //    var separarId = cadenaCompleta.Split(',');
                
            //    foreach (var item in separarId)
            //    {
            //        var fechaActual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //        var datos = datosProducto(Convert.ToInt32(item));

            //        var datoObtenidoPrecio = float.Parse(datos[3].ToString());
            //        var newPrecio = float.Parse(precioTmp);
            //        //var operacionPrecio = 0f;


            //        //if (datoObtenidoPrecio > newPrecio)
            //        //{
            //        //    operacionPrecio = newPrecio - datoObtenidoPrecio;
            //        //}
            //        //else if (datoObtenidoPrecio < newPrecio)
            //        //{
            //        //    operacionPrecio = datoObtenidoPrecio - newPrecio;
            //        //}

            //        var sentenciaAgregarInfoDisminuir = $"INSERT INTO DGVDisminuirInventario (IdProducto, NombreProducto, StockActual, DiferenciaUnidades, NuevoStock, Precio, Clave, Codigo, Fecha, NoRevision, StatusActualizacion, NombreEmisor, Comentarios, ValorUnitario, IDUsuario) VALUES";

            //        sentenciaAgregarInfoDisminuir += $"('{datos[0]}', '{datos[1]}', '{datos[2]}', '{""}','{datos[2]}', '{precioTmp}', '{datos[4]}', '{datos[5]}', '{fechaActual}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{FormPrincipal.userID}')";

            //        cn.EjecutarConsulta(sentenciaAgregarInfoDisminuir);
            //        sentenciaAgregarInfoDisminuir = string.Empty;
            //    }
            //}

            //Inventario inventario = Application.OpenForms.OfType<Inventario>().FirstOrDefault();

            //if (inventario != null)
            //{
            //    inventario.populateAumentarDGVInventario();
            //}

        }

        //Se guatdan en el diccionario los ID y el tipo de producto
        private void separarDiccionario(int id, int nuevoPrecio, string tipoProducto, float precioActual)
        {
            var query = cn.CargarDatos($"SELECT Precio FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{id}'");
            if (!query.Rows.Count.Equals(0))
            {
                var precio = float.Parse(query.Rows[0]["Precio"].ToString());

                modificarPrecio.Add(id,tipoProducto);

                //if (precio < precioActual)
                //{
                //    modificarPrecio.Add(id,tipoProducto);
                //}
                //else if(precio > precioActual)
                //{
                //    disminuirPrecio.Add(id, tipoProducto);
                //}
            }
        }

        private string[] datosProducto(int id)
        {
            List<string> lista = new List<string>();

            //var consultaA = $"SELECT ID, Nombre, Stock, Precio, ClaveInterna, CodigoBarras, NumeroRevision, PrecioCompra FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{id}'";

            var query = cn.CargarDatos($"SELECT P.ID AS ID, P.Nombre AS Nombre, P.Stock AS Stock, P.Precio AS Precio, P.ClaveInterna AS ClaveInterna, P.CodigoBarras AS CodigoBarras, P.NumeroRevision AS NumeroRevision, P.PrecioCompra AS PrecioCompra, DP.Proveedor AS Proveedor  FROM Productos AS P INNER JOIN detallesproducto AS DP ON P.IDUsuario = DP.IDUsuario WHERE P.IDUsuario = '{FormPrincipal.userID}' AND P.ID = '{id}' LIMIT 1");

            if (!query.Rows.Count.Equals(0))
            {
                lista.Add(id.ToString());//0
                lista.Add(query.Rows[0]["Nombre"].ToString());//1
                lista.Add(query.Rows[0]["Stock"].ToString());//2
                //lista.Add(diferencia);
                //lista.Add(nuevo stock);
                lista.Add(query.Rows[0]["Precio"].ToString());//3
                lista.Add(query.Rows[0]["ClaveInterna"].ToString());//4
                lista.Add(query.Rows[0]["CodigoBarras"].ToString());//5
                //lista.Add(Fecha);
                lista.Add(query.Rows[0]["NumeroRevision"].ToString());//6
                lista.Add("1");//7
                lista.Add(query.Rows[0]["Proveedor"].ToString());//8
                lista.Add("");//9
                lista.Add(query.Rows[0]["PrecioCompra"].ToString());//10
            }

            return lista.ToArray();
        }

    }
}
