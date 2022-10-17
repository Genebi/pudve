using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        //Cargando cargando = new Cargando();

        string propiedad = string.Empty;

        Dictionary<int, string> productos;
        Dictionary<string, string> clavesUnidades;
        Dictionary<int, float> datosHistPrecio;
         
        public static Dictionary<int, string> modificarPrecio = new Dictionary<int, string>();
        //public static Dictionary<int, string> disminuirPrecio = new Dictionary<int, string>();

        bool stateChkMostrarMensaje = true;

        bool stateChkEliminarMensajes = false;

        bool stateChkOcultarMensajes = false;

        string cantidadCompra = string.Empty;

        string mensajeCompra = string.Empty;

        private const char SignoDecimal = '.';

        private string nombreAsignar = string.Empty;

        bool estado = true;

        public static string tipoDeAsignacion = string.Empty;

        Button btnAceptar = new Button();


        public AsignarPropiedad(object propiedad)
        {
            InitializeComponent();

            this.propiedad = propiedad.ToString().Replace(" ", "_");

            productos = Productos.productosSeleccionados;

            datosHistPrecio = Productos.productosSeleccionadosParaHistorialPrecios;
        }

        private string SplitCamelCase(string str)
        {
            return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        private void AsignarPropiedad_Load(object sender, EventArgs e)
        {
            var propiedadNombre = propiedad.ToUpper().Replace("_", " ");
            string dato = string.Empty;
            if (propiedadNombre == "MENSAJEVENTAS")
            {
                dato = "MENSAJE VENTAS";
            }
            else if (propiedadNombre == "MENSAJEINVENTARIO")
            {
                dato = "MENSAJE INVENTARIO";
            }
            else 
            {

            }
            nombreAsignar = SplitCamelCase(propiedad.Replace("_"," "));
            lbNombrePropiedad.Text = $"ASIGNAR {nombreAsignar.ToUpper()}";
            //lbNombrePropiedad.Text = $"ASIGNAR {SplitCamelCase(propiedad.ToUpper())}";

            clavesUnidades = mb.CargarClavesUnidades();

            stateChkMostrarMensaje = true;

            if (propiedad == "MensajeVentas")
            {
                var consulta = cn.CargarDatos($"SELECT ProductMessageActivated FROM productmessage WHERE IDProducto = {productos.Keys.First()}");
                if (consulta.Rows.Count.Equals(1))
                {
                    estado = Convert.ToBoolean(consulta.Rows[0]["ProductMessageActivated"].ToString());
                    if (estado)
                    {
                        stateChkMostrarMensaje = true;
                        stateChkOcultarMensajes = false;
                    }
                    else
                    {
                        stateChkOcultarMensajes = true;
                        stateChkMostrarMensaje = false;
                    }
                }
            }
            else if (propiedad == "MensajeInventario")
            {
                var consulta = cn.CargarDatos($"SELECT Activo FROM mensajesinventario WHERE IDProducto = {productos.Keys.First()}");
                if (consulta.Rows.Count.Equals(1))
                {
                    var status = consulta.Rows[0]["Activo"].ToString();
                    if (status == "1")
                    {
                        stateChkMostrarMensaje = true;
                        stateChkOcultarMensajes = false;
                    }
                    else
                    {
                        stateChkOcultarMensajes = true;
                        stateChkMostrarMensaje = false;
                    }
                }
            }
            

            CargarPropiedad();

        }

        private void CargarPropiedad()
        {
            Font fuente = new Font("Century Gothic", 10.0f);
            Font fuenteChica = new Font("Century Gothic", 8.0f);

            if (propiedad == "MensajeVentas")
            {

                TextBox txtCantidadCompra = new TextBox();
                txtCantidadCompra.Name = "txtCantidadCompra";
                txtCantidadCompra.Width = 35;
                txtCantidadCompra.Height = 20;
                txtCantidadCompra.Leave += new EventHandler(txtCantidadCompra_Leave);
                txtCantidadCompra.KeyPress += new KeyPressEventHandler(txtCantidadCompra_Keypress);
                txtCantidadCompra.Location = new Point(265, 33);
                txtCantidadCompra.ShortcutsEnabled = false;

                TextBox tbMensaje = new TextBox();
                tbMensaje.Name = "tb" + propiedad;
                tbMensaje.Width = 200;
                tbMensaje.Height = 40;
                tbMensaje.CharacterCasing = CharacterCasing.Upper;
                tbMensaje.Font = fuente;
                tbMensaje.Multiline = true;
                tbMensaje.ScrollBars = ScrollBars.Vertical;
                tbMensaje.Location = new Point(65, 110);

                Label lbCantidadCompra = new Label();
                lbCantidadCompra.Text = "Cantidad minima en la venta para mostrar mensaje:";
                lbCantidadCompra.AutoSize = true;
                lbCantidadCompra.Location = new Point(15, 35);

                Label lbMostrarMensaje = new Label();
                lbMostrarMensaje.Text = "Mostrar mensaje:";
                lbMostrarMensaje.AutoSize = true;
                lbMostrarMensaje.Location = new Point(15, 62);

                Label lbOcultarMensaje = new Label();
                lbOcultarMensaje.Text = "Ocultar mensaje:";
                lbOcultarMensaje.AutoSize = true;
                lbOcultarMensaje.Location = new Point(15, 82);

                Label lbEliminarMensajes = new Label();
                lbEliminarMensajes.Text = "Eliminar mensaje:";
                lbEliminarMensajes.AutoSize = true;
                lbEliminarMensajes.Location = new Point(152, 78);

                var datos = Productos.checkboxMarcados;
                var cantidadDatos = datos.Count;

                if (cantidadDatos == 1)
                {
                    foreach (KeyValuePair<int,string> item in datos)
                    {
                        var id = item.Key;
                        var dato = cn.CargarDatos($"SELECT CantidadMinimaDeCompra FROM productmessage WHERE IDProducto = {id}");
                        if (!dato.Rows.Count.Equals(0) && !string.IsNullOrWhiteSpace(dato.ToString()))
                        {
                            int cantidad = Convert.ToInt32(dato.Rows[0]["CantidadMinimaDeCompra"]);
                            txtCantidadCompra.Text = cantidad.ToString();
                        }
                    }
                }
               
                RadioButton chkMostrarOcultarMensaje = new RadioButton();
                chkMostrarOcultarMensaje.Name = "chkMostrarMensaje";
                chkMostrarOcultarMensaje.Checked = stateChkMostrarMensaje;
                chkMostrarOcultarMensaje.AutoCheck = true;
                chkMostrarOcultarMensaje.CheckedChanged += new EventHandler(chkMostrarOcultarMensaje_CheckedChanged);
                chkMostrarOcultarMensaje.Height = 15;
                chkMostrarOcultarMensaje.Width = 15;
                chkMostrarOcultarMensaje.Location = new Point(100,62);

                RadioButton chkOcultarMensaje = new RadioButton();
                chkOcultarMensaje.Name = "chkOcultarMensaje";
                chkOcultarMensaje.Checked = false;
                chkOcultarMensaje.CheckedChanged += new EventHandler(chkOcultarMensaje_CheckedChanged);
                chkOcultarMensaje.Height = 15;
                chkOcultarMensaje.Width = 15;
                chkOcultarMensaje.Location = new Point(100, 82);

                RadioButton chkEliminarMensaje = new RadioButton();
                chkEliminarMensaje.Name = "chkEliminarMensaje";
                chkEliminarMensaje.Checked = false;
                stateChkEliminarMensajes = chkEliminarMensaje.Checked;
                chkEliminarMensaje.CheckedChanged += new EventHandler(chkEliminarMensaje_CheckedChanged);
                chkEliminarMensaje.Height = 15;
                chkEliminarMensaje.Width = 15;
                chkEliminarMensaje.Location = new Point(240, 78);

                panelContenedor.Controls.Add(tbMensaje);
                panelContenedor.Controls.Add(chkMostrarOcultarMensaje);
                panelContenedor.Controls.Add(chkOcultarMensaje);
                panelContenedor.Controls.Add(chkEliminarMensaje);
                panelContenedor.Controls.Add(lbMostrarMensaje);
                panelContenedor.Controls.Add(lbOcultarMensaje);
                panelContenedor.Controls.Add(lbCantidadCompra);
                panelContenedor.Controls.Add(lbEliminarMensajes);
                panelContenedor.Controls.Add(txtCantidadCompra);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarMensaje"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarMensaje"));

                if (cantidadDatos > 1)
                {
                    int mensajesRepetidos = 0;

                    var idProducto = datos.Keys.First();
                    var consultaPrimerProducto = cn.CargarDatos($"SELECT ProductOfMessage FROM productmessage WHERE IDProducto = {idProducto}");
                    if (!consultaPrimerProducto.Rows.Count.Equals(0))
                    {
                        string mensajeAComparar = consultaPrimerProducto.Rows[0]["ProductOfMessage"].ToString();

                        foreach (KeyValuePair<int, string> item in datos)
                        {
                            var id = item.Key;
                            var consulta = cn.CargarDatos($"SELECT ProductOfMessage FROM productmessage WHERE IDProducto = {id}");
                            if (!consulta.Rows.Count.Equals(0))
                            {
                                string mensaje = consulta.Rows[0]["ProductOfMessage"].ToString();
                        
                                if (mensajeAComparar.Equals(mensaje))
                                {
                                    mensajesRepetidos++;
                                }
                            }
                        }

                        if (datos.Count.Equals(mensajesRepetidos))
                        {
                            var consultaMensaje = cn.CargarDatos($"SELECT ProductOfMessage  FROM productmessage WHERE IDProducto = {idProducto}");
                            var consultaCantidad = cn.CargarDatos($"SELECT CantidadMinimaDeCompra  FROM productmessage WHERE IDProducto = {idProducto}");
                            string mensaje = consultaMensaje.Rows[0]["ProductOfMessage"].ToString();
                            string cantidad = consultaCantidad.Rows[0]["CantidadMinimaDeCompra"].ToString();
                            tbMensaje.Text = mensaje;
                            txtCantidadCompra.Text = cantidad;
                        }
                    }
                }
                    

                if (cantidadDatos == 1)
                {

                    foreach (KeyValuePair<int, string> item in datos)
                    {
                        var id = item.Key;
                        var dato = cn.CargarDatos($"SELECT ProductOfMessage FROM productmessage WHERE IDProducto = {id}");
                        if (!dato.Rows.Count.Equals(0))
                        {
                            string cantidad = dato.Rows[0]["ProductOfMessage"].ToString();
                            tbMensaje.Text = cantidad.ToString();
                        }
                        if (estado == false)
                        {
                            stateChkOcultarMensajes = true;
                            stateChkMostrarMensaje = false;
                            RadioButton rbOcultarMensaje = (RadioButton)Controls.Find("chkOcultarMensaje", true)[0];
                            if (!rbOcultarMensaje.Equals(null))
                            {
                                rbOcultarMensaje.Checked = true;
                            }
                        }
                    }

                }

                TextBox txtCantidadCompraFocus = (TextBox)Controls.Find("txtCantidadCompra", true)[0];
                this.ActiveControl = txtCantidadCompraFocus;
                if (stateChkOcultarMensajes.Equals(true))
                {
                    RadioButton rbOcultarMensaje = (RadioButton)Controls.Find("chkOcultarMensaje", true)[0];
                    rbOcultarMensaje.Checked = true;
                }
                else if (stateChkMostrarMensaje.Equals(true))
                {
                    RadioButton rbMostrarMensaje = (RadioButton)Controls.Find("chkMostrarMensaje", true)[0];
                    rbMostrarMensaje.Checked = true;
                }
            }
            else if (propiedad == "MensajeInventario")//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                tbMensaje.Location = new Point(65, 95);

                Label lbMostrarMensaje = new Label();
                lbMostrarMensaje.Text = "Mostrar mensaje:";
                lbMostrarMensaje.AutoSize = true;
                lbMostrarMensaje.Location = new Point(15, 50);

                Label lbOcultarMensaje = new Label();
                lbOcultarMensaje.Text = "Ocultar mensaje:";
                lbOcultarMensaje.AutoSize = true;
                lbOcultarMensaje.Location = new Point(15, 72);

                Label lbEliminarMensajes = new Label();
                lbEliminarMensajes.Text = "Eliminar mensaje:";
                lbEliminarMensajes.AutoSize = true;
                lbEliminarMensajes.Location = new Point(172, 50);

                RadioButton chkMostrarOcultarMensaje = new RadioButton();
                chkMostrarOcultarMensaje.Name = "chkMostrarMensaje";
                chkMostrarOcultarMensaje.Checked = stateChkMostrarMensaje;
                chkMostrarOcultarMensaje.AutoCheck = true;
                chkMostrarOcultarMensaje.CheckedChanged += new EventHandler(chkMostrarOcultarMensajeInventario_CheckedChanged);
                chkMostrarOcultarMensaje.Height = 15;
                chkMostrarOcultarMensaje.Width = 15;
                chkMostrarOcultarMensaje.Location = new Point(100, 52);

                RadioButton chkOcultarMensaje = new RadioButton();
                chkOcultarMensaje.Name = "chkOcultarMensaje";
                chkOcultarMensaje.Checked = stateChkOcultarMensajes;
                chkOcultarMensaje.CheckedChanged += new EventHandler(chkOcultarMensaje_CheckedChanged);
                chkOcultarMensaje.Height = 15;
                chkOcultarMensaje.Width = 15;
                chkOcultarMensaje.Location = new Point(100, 72);

                RadioButton chkEliminarMensaje = new RadioButton();
                chkEliminarMensaje.Name = "chkEliminarMensaje";
                chkEliminarMensaje.Checked = false;
                stateChkEliminarMensajes = chkEliminarMensaje.Checked;
                chkEliminarMensaje.CheckedChanged += new EventHandler(chkEliminarMensaje_CheckedChanged);
                chkEliminarMensaje.Height = 15;
                chkEliminarMensaje.Width = 15;
                chkEliminarMensaje.Location = new Point(260, 52);

                panelContenedor.Controls.Add(chkMostrarOcultarMensaje);
                panelContenedor.Controls.Add(chkOcultarMensaje);
                panelContenedor.Controls.Add(chkEliminarMensaje);
                panelContenedor.Controls.Add(lbMostrarMensaje);
                panelContenedor.Controls.Add(lbOcultarMensaje);
                panelContenedor.Controls.Add(lbEliminarMensajes);
                panelContenedor.Controls.Add(tbMensaje);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarMensaje"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarMensaje"));

                var datos = Productos.checkboxMarcados;
                var cantidadDatos = datos.Count;


                if (cantidadDatos == 1)
                {
                    foreach (KeyValuePair<int, string> item in datos)
                    {
                        var id = item.Key;
                        var dato = cn.CargarDatos($"SELECT Mensaje FROM mensajesinventario WHERE IDProducto = {id}");
                        if (!dato.Rows.Count.Equals(0))
                        {
                            string cantidad = dato.Rows[0]["Mensaje"].ToString();
                            tbMensaje.Text = cantidad.ToString();
                        }
                    }
                }

                if (cantidadDatos > 1)
                {
                    int mensajesRepetidos = 0;

                    var idProducto = datos.Keys.First();
                    var consultaPrimerProducto = cn.CargarDatos($"SELECT Mensaje FROM mensajesinventario WHERE IDProducto = {idProducto}");
                    if (!consultaPrimerProducto.Rows.Count.Equals(0))
                    {
                        string mensajeAComparar = consultaPrimerProducto.Rows[0]["Mensaje"].ToString();

                        foreach (KeyValuePair<int, string> item in datos)
                        {
                            var id = item.Key;
                            var consulta = cn.CargarDatos($"SELECT Mensaje FROM mensajesinventario WHERE IDProducto = {id}");
                            if (!consulta.Rows.Count.Equals(0))
                            {
                                string mensaje = consulta.Rows[0]["Mensaje"].ToString();

                                if (mensajeAComparar.Equals(mensaje))
                                {
                                    mensajesRepetidos++;
                                }
                            }
                        }

                        if (datos.Count.Equals(mensajesRepetidos))
                        {
                            var consultaMensaje = cn.CargarDatos($"SELECT Mensaje  FROM mensajesinventario WHERE IDProducto = {idProducto}");
                            string mensaje = consultaMensaje.Rows[0]["Mensaje"].ToString();
                            tbMensaje.Text = mensaje;
                        }
                    }
                }
            }
            else if (propiedad == "Stock")//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
            else if (propiedad == "StockMinimo")//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
            else if (propiedad == "StockMaximo")//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
            else if (propiedad == "Precio")//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
            else if (propiedad == "CantidadMayoreo")
            {
                TextBox tbCantidad = new TextBox();
                tbCantidad.Name = "tb" + propiedad;
                tbCantidad.Width = 200;
                tbCantidad.Height = 20;
                tbCantidad.TextAlign = HorizontalAlignment.Center;
                tbCantidad.Font = fuente;
                tbCantidad.KeyPress += new KeyPressEventHandler(SoloDecimales);
                tbCantidad.Location = new Point(65, 70);

                panelContenedor.Controls.Add(tbCantidad);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarPrecio"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarPrecio"));
            }
            else if (propiedad == "NumeroRevision")//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
            else if (propiedad == "TipoIVA")//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                cbIVA.MouseWheel += new MouseEventHandler(ComboBox_Quitar_MouseWheel);
                cbIVA.Location = new Point(15, 70);

                panelContenedor.Controls.Add(cbIVA);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarIVA"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarIVA"));
            }
            else if (propiedad == "ClaveProducto")//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            {
                TextBox tbClaveProducto = new TextBox();               
                tbClaveProducto.Name = "tb" + propiedad;
                tbClaveProducto.Width = 200;
                tbClaveProducto.Height = 40;
                tbClaveProducto.CharacterCasing = CharacterCasing.Upper;
                tbClaveProducto.KeyPress += new KeyPressEventHandler(SoloDecimales);
                tbClaveProducto.Font = fuente;
                tbClaveProducto.Location = new Point(65, 70);
                tbClaveProducto.MaxLength = 8;
                tbClaveProducto.TextChanged += new EventHandler(CambioDeColor);

                                    
                          
                panelContenedor.Controls.Add(tbClaveProducto);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarClaveProducto"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarClaveProducto"));
            }
            else if (propiedad == "ClaveUnidad")//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                cbClaves.MouseWheel += new MouseEventHandler(ComboBox_Quitar_MouseWheel);
                cbClaves.Location = new Point(15, 80);

                panelContenedor.Controls.Add(tbClaveUnidad);
                panelContenedor.Controls.Add(cbClaves);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarClaveUnidad"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarClaveUnidad"));
            }
            else if (propiedad == "Correos")//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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


                foreach (var producto in productos)
                {


                    var validar = cn.CargarDatos($"SELECT CorreoVentaProducto FROM CorreosProducto WHERE IDProducto = {producto.Key}");

                    if (!validar.Rows.Count.Equals(0) && !string.IsNullOrWhiteSpace(validar.ToString()))
                    {
                        int Status = Convert.ToInt32(validar.Rows[0]["CorreoVentaProducto"]);

                        if (Status.Equals(1))
                        {
                            cuartoCB.Checked = true;
                        }
                        else if (Status.Equals(0))
                        {
                            cuartoCB.Checked = false;
                        }
                    }
                }
                    


                var checkboxes = new CheckBox[] { cuartoCB };

                panelContenedor.Controls.AddRange(checkboxes);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarCorreos", 150));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarCorreos", 150));
            }
            else if (propiedad == "Proveedor")//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                    cbProveedores.MouseWheel += new MouseEventHandler(ComboBox_Quitar_MouseWheel);
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
            else if (propiedad == "AgregarDescuento")
            {
                TextBox tbAgregarDescuento = new TextBox();
                tbAgregarDescuento.Name = "tb" + propiedad;
                tbAgregarDescuento.Width = 200;
                tbAgregarDescuento.Height = 40;
                tbAgregarDescuento.CharacterCasing = CharacterCasing.Upper;
                tbAgregarDescuento.Font = fuente;
                tbAgregarDescuento.Location = new Point(65, 50);

                panelContenedor.Controls.Add(tbAgregarDescuento);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarMensaje"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarMensaje"));
            }
            else if (propiedad == "EliminarDescuento")
            {
                //TextBox tbEliminarDescuento = new TextBox();
                //tbEliminarDescuento.Name = "tb" + propiedad;
                //tbEliminarDescuento.Width = 200;
                //tbEliminarDescuento.Height = 40;
                //tbEliminarDescuento.CharacterCasing = CharacterCasing.Upper;
                //tbEliminarDescuento.Font = fuente;
                //tbEliminarDescuento.Location = new Point(65, 50);

                //panelContenedor.Controls.Add(tbEliminarDescuento);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarMensaje"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarMensaje"));
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
                    cbPropiedad.MouseWheel += new MouseEventHandler(ComboBox_Quitar_MouseWheel);
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

        private void CambioDeColor(object sender, EventArgs e)
        {
            
            if (!(sender as TextBox).Text.Length.Equals(8))
            {
                (sender as TextBox).ForeColor = Color.Red;
                btnAceptar.Enabled = false;
            }
            else
            {
                (sender as TextBox).ForeColor = Color.Black;
                btnAceptar.Enabled = true;
            }
        }

        private void chkOcultarMensaje_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton checkbox = (RadioButton)sender;

            if (checkbox.Name.Equals("chkOcultarMensaje") && checkbox.Checked == true)
            {
                stateChkOcultarMensajes = true;
                stateChkEliminarMensajes = false;
                stateChkMostrarMensaje = false;
            }
        }

        private void ComboBox_Quitar_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs ee = (HandledMouseEventArgs)e;
            ee.Handled = true;
        }

        private void chkEliminarMensaje_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton checkbox = (RadioButton)sender;

            if (checkbox.Name.Equals("chkEliminarMensaje") && checkbox.Checked == true)
            {
                stateChkEliminarMensajes = true;
                stateChkOcultarMensajes = false;
                stateChkMostrarMensaje = false;
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
            RadioButton checkbox = (RadioButton)sender;

            if (checkbox.Name.Equals("chkMostrarMensaje") && checkbox.Checked == true)
            {
                stateChkMostrarMensaje = true;
                stateChkOcultarMensajes = false;
                stateChkEliminarMensajes = false;
            }

        }

        private void chkMostrarOcultarMensaje_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton checkbox = (RadioButton)sender;

            if (checkbox.Name.Equals("chkMostrarMensaje") && checkbox.Checked == true)
            {
                stateChkMostrarMensaje = true;
                stateChkOcultarMensajes = false;
                stateChkEliminarMensajes = false;
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
                btnCancelar.Location = new Point(65, ejeY+10);

                boton = btnCancelar;
            }

            if (tipo == 1)
            {
                
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
                btnAceptar.Location = new Point(170, ejeY+10);

                boton = btnAceptar;
            }

            return boton;
        }

        private /*async*/ void botonAceptar_Click(object sender, EventArgs e)
        {
            if (propiedad == "MensajeVentas")
            {
                RadioButton rbOcultarMensaje = (RadioButton)Controls.Find("chkOcultarMensaje", true)[0];
                RadioButton rbMostrarMensaje = (RadioButton)Controls.Find("chkMostrarMensaje", true)[0];
                RadioButton rbEliminarMensaje = (RadioButton)Controls.Find("chkEliminarMensaje", true)[0];

                if (rbMostrarMensaje.Checked == true)
                {
                    tipoDeAsignacion = "Mensajes";
                }
                else if (rbOcultarMensaje.Checked == true)
                {
                    tipoDeAsignacion = "Ocultar mensajes";
                }
                else if (rbEliminarMensaje.Checked == true)
                {
                    tipoDeAsignacion = "Eliminar mensajes";
                }


                if (!rbMostrarMensaje.Checked.Equals(true) && !rbOcultarMensaje.Checked.Equals(true) && !rbEliminarMensaje.Checked.Equals(true))
                {
                    MessageBox.Show("Favor de marcar la opcion a realizar", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (propiedad == "MensajeInventario")
            {
                RadioButton rbOcultarMensaje = (RadioButton)Controls.Find("chkOcultarMensaje", true)[0];
                RadioButton rbMostrarMensaje = (RadioButton)Controls.Find("chkMostrarMensaje", true)[0];
                RadioButton rbEliminarMensaje = (RadioButton)Controls.Find("chkEliminarMensaje", true)[0];

                if (rbMostrarMensaje.Checked == true)
                {
                    tipoDeAsignacion = "Mensajes";
                }
                else if (rbOcultarMensaje.Checked == true)
                {
                    tipoDeAsignacion = "Ocultar mensajes";
                }
                else if (rbEliminarMensaje.Checked == true)
                {
                    tipoDeAsignacion = "Eliminar mensajes";
                }


                if (!rbMostrarMensaje.Checked.Equals(true) && !rbOcultarMensaje.Checked.Equals(true) && !rbEliminarMensaje.Checked.Equals(true))
                {
                    MessageBox.Show("Favor de marcar la opcion a realizar", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            Button btn = (Button)sender;
            btn.Enabled = false;
            //cargando.Show();

            //await Task.Run(() =>
            //{
            //    Thread.Sleep(2000);
            //});

            //OperacionBoton();

            //cargando.Close();

            //Dispose();

            if (propiedad == "MensajeVentas")
            {
                TextBox txtMensaje = (TextBox)this.Controls.Find("tbMensajeVentas", true)[0];
                var mensaje = txtMensaje.Text;

                TextBox txtCantidadCompra = (TextBox)this.Controls.Find("txtCantidadCompra", true)[0];
                var cantidad = txtCantidadCompra.Text;
                if (!string.IsNullOrWhiteSpace(mensaje) && !string.IsNullOrWhiteSpace(cantidad) || stateChkEliminarMensajes.Equals(true))
                {
                    //MensajePorFavorEspere porFavorEspere = new MensajePorFavorEspere();

                    // Mostrar formulario sin modo
                    //porFavorEspere.tiempoDeEspera = 150;
                    //porFavorEspere.propiedadCambiar = propiedad;
                    //porFavorEspere.ShowDialog();

                    OperacionBoton();

                    //// Permita que el hilo principal de la interfaz de usuario se muestre correctamente, espere el formulario.
                    //Application.DoEvents();

                    //porFavorEspere.Dispose();
                }
                else if (stateChkOcultarMensajes.Equals(true))
                {
                    OperacionBoton();
                }
                else
                {
                    MessageBox.Show("Favor de rellenar los 2 campos contengan informacion.", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (propiedad == "MensajeInventario")
            {
                TextBox txtMensaje = (TextBox)this.Controls.Find("tbMensajeInventario", true)[0];
                RadioButton rbEliminarMensaje = (RadioButton)Controls.Find("chkEliminarMensaje", true)[0];
                var mensaje = txtMensaje.Text;
                if (!string.IsNullOrWhiteSpace(mensaje) || rbEliminarMensaje.Checked.Equals(true))
                {
                    //MensajePorFavorEspere porFavorEspere = new MensajePorFavorEspere();

                    // Mostrar formulario sin modo
                    //porFavorEspere.tiempoDeEspera = 150;
                    //porFavorEspere.propiedadCambiar = propiedad;
                    //porFavorEspere.ShowDialog();
                    //// Permita que el hilo principal de la interfaz de usuario se muestre correctamente, espere el formulario.
                    //Application.DoEvents();
                    //porFavorEspere.Dispose();
                    OperacionBoton();
                }
                else
                {
                    MessageBox.Show("Favor de insertar un mensaje a mostrar.");
                }
            }
            else
            {
                OperacionBoton();
            }
            //MensajePorFavorEspere porFavorEspere = new MensajePorFavorEspere();

            //// Mostrar formulario sin modo
            //porFavorEspere.tiempoDeEspera = 150;
            //porFavorEspere.propiedadCambiar = propiedad;
            //porFavorEspere.ShowDialog();
            //Thread.Sleep(500);
            
                this.Close();

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

                if (!string.IsNullOrWhiteSpace(mensaje) && !string.IsNullOrWhiteSpace(cantidad) || stateChkEliminarMensajes.Equals(true))
                {
                    if (stateChkMostrarMensaje.Equals(true))
                    {
                        foreach (var producto in productos)
                        {
                            using (DataTable datosV = cn.CargarDatos($"SELECT * FROM productmessage WHERE IDProducto = '{producto.Key}'"))
                            {
                                if (!datosV.Rows.Count.Equals(0))
                                {

                                    cn.EjecutarConsulta($"UPDATE productmessage SET ProductMessageActivated = 1, ProductOfMessage = '{mensaje}', CantidadMinimaDeCompra = '{cantidadCompra}'  WHERE IDProducto = '{producto.Key}'");
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
                                    cn.EjecutarConsulta($"INSERT INTO productmessage (IDProducto,ProductOfMessage,ProductMessageActivated,CantidadMinimaDeCompra) VALUES ('{producto.Key}','{mensaje}','{status}','{cantidadCompra}')");
                                }
                            }

                        }
                    }
                    else if (stateChkMostrarMensaje.Equals(false) && stateChkOcultarMensajes.Equals(true))
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
                                    cn.EjecutarConsulta($"INSERT INTO productmessage (IDProducto,ProductOfMessage,ProductMessageActivated,CantidadMinimaDeCompra) VALUES ('{producto.Key}','{mensaje}','{status}','{cantidadCompra}')");
                                }
                            }
                        }
                        foreach (var item in productos)
                        {
                            cn.EjecutarConsulta($"UPDATE productmessage SET ProductOfMessage = '{mensaje}' WHERE IDProducto = '{item.Key}'");
                            cn.EjecutarConsulta(cs.actualizarCompraMinimaMultiple(item.Key, Convert.ToInt32(txtCantidadCompra.Text)));
                        }
                    }
                    else if (stateChkEliminarMensajes.Equals(true) && stateChkMostrarMensaje.Equals(false) && stateChkOcultarMensajes.Equals(false))
                    {
                        if (stateChkEliminarMensajes == true)
                        {
                            foreach (var ids in productos)
                            {
                                var id = ids.Key;
                                var propiedadNombre = propiedad.ToUpper().Replace("_", " ");
                                if (propiedadNombre.Equals("MENSAJEVENTAS"))
                                {
                                    cn.EjecutarConsulta($"DELETE FROM productmessage WHERE IDProducto = {id}");
                                }
                            }
                        }
                    }
                    MessageBoxTemporal.Show("ASIGNACION MULTIPLE REALIZADA CON EXITO", "Mensajes del sistema",3,true);
                }
                else if (stateChkEliminarMensajes.Equals(false) && stateChkMostrarMensaje.Equals(false) && stateChkOcultarMensajes.Equals(true))
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
                                cn.EjecutarConsulta($"INSERT INTO productmessage (IDProducto,ProductOfMessage,ProductMessageActivated,CantidadMinimaDeCompra) VALUES ('{producto.Key}','{mensaje}','{status}','0')");
                            }
                        }
                    }
                    MessageBoxTemporal.Show("ASIGNACION MULTIPLE REALIZADA CON EXITO", "Mensajes del sistema", 3, true);
                }
                else
                {
                    if (stateChkEliminarMensajes == false)
                    {
                        MessageBox.Show("Favor de rellenar los 2 campos contengan informacion.","Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                
            }
            else if (propiedad == "MensajeInventario")//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            {

                TextBox txtMensaje = (TextBox)this.Controls.Find("tbMensajeInventario", true)[0];
                var mensaje = txtMensaje.Text;

                if (!string.IsNullOrWhiteSpace(mensaje) || stateChkEliminarMensajes.Equals(true))
                {
                    if (stateChkMostrarMensaje.Equals(true))
                    {
                        foreach (var producto in productos)
                        {
                            using (DataTable datosV = cn.CargarDatos($"SELECT * FROM mensajesinventario WHERE IDProducto = '{producto.Key}'"))
                            {
                                if (!datosV.Rows.Count.Equals(0))
                                {

                                    cn.EjecutarConsulta($"UPDATE mensajesinventario SET Activo = 1, Mensaje = '{mensaje}' WHERE IDProducto = '{producto.Key}'");
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
                                    cn.EjecutarConsulta($"INSERT INTO mensajesinventario (IDProducto,Mensaje,Activo,IDUsuario) VALUES ('{producto.Key}','{mensaje}','{status}','{FormPrincipal.userID}')");
                                }
                            }

                        }
                    }
                    else if (stateChkMostrarMensaje.Equals(false) && stateChkOcultarMensajes.Equals(true))
                    {
                        foreach (var producto in productos)
                        {
                            using (DataTable datosV = cn.CargarDatos($"SELECT * FROM mensajesinventario WHERE IDProducto = '{producto.Key}'"))
                            {
                                if (!datosV.Rows.Count.Equals(0))
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
                                    cn.EjecutarConsulta($"INSERT INTO mensajesinventario (IDProducto,Mensaje,Activo,IDUsuario) VALUES ('{producto.Key}','{mensaje}','{status}','{FormPrincipal.userID}')");
                                }
                            }
                        }
                        foreach (var item in productos)
                        {
                            cn.EjecutarConsulta($"UPDATE mensajesinventario SET Mensaje = '{mensaje}' WHERE IDProducto = '{item.Key}'");
                        }
                    }
                    else if (stateChkEliminarMensajes.Equals(true) && stateChkMostrarMensaje.Equals(false) && stateChkOcultarMensajes.Equals(false))
                    {
                        if (stateChkEliminarMensajes == true)
                        {
                            foreach (var ids in productos)
                            {
                                var id = ids.Key;
                                var propiedadNombre = propiedad.ToUpper().Replace("_", " ");
                                if (propiedadNombre.Equals("MENSAJEINVENTARIO"))
                                {
                                    cn.EjecutarConsulta($"DELETE FROM mensajesinventario WHERE IDProducto = {id}");
                                }
                            }
                        }
                    }
                    MessageBoxTemporal.Show("ASIGNACION MULTIPLE REALIZADA CON EXITO", "Mensajes del sistema", 3,true);
                }
                
            }
            else if (propiedad == "Stock")//////////////////////////////////////////////////////////////////////////////////////////////////////
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
                                                    <span style='color: black;'>{datosProducto[1]}</span> 
                                                    --- <b>STOCK ANTERIOR:</b> 
                                                    <span style='color: black;'>{datosProducto[4]}</span> 
                                                    --- <b>STOCK NUEVO:</b> 
                                                    <span style='color: black;'>{stock}</span>
                                                </li>";
                                        }
                                    }
                                }
                            }

                            valores += $"({producto.Key}, {stock}),";

                            //datos = new string[] { producto.Key.ToString(), stock, FormPrincipal.userID.ToString() };

                            //cn.EjecutarConsulta(cs.ActualizarStockProductos(datos));
                            var datoStock = cn.CargarDatos($"SELECT Stock FROM Productos WHERE ID = {producto.Key}");
                            var stockActual = datoStock.Rows[0]["Stock"].ToString();
                            decimal stockNuevo = Convert.ToDecimal(stock);
                            var fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                            cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad) VALUES ('{producto.Key}','Asignacion de Stock ','{stockActual}','{stockNuevo}','{fecha}','{FormPrincipal.userNickName}', '0')");
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
                    MessageBoxTemporal.Show("ASIGNACION MULTIPLE REALIZADA CON EXITO", "Mensajes del sistema",3,true);
                }
                else
                {
                    MessageBox.Show("Ingrese una cantidad para stock", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (propiedad == "StockMinimo")//////////////////////////////////////////////////////////StockMinimo
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
                    MessageBoxTemporal.Show("ASIGNACION MULTIPLE REALIZADA CON EXITO", "Mensajes del sistema",3,true);
                }
                else
                {
                    MessageBox.Show("Ingrese una cantidad para stock minimo", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (propiedad == "StockMaximo")/////////////////////////////////////////////////////////////////
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
                    MessageBoxTemporal.Show("ASIGNACION MULTIPLE REALIZADA CON EXITO", "Mensajes del sistema",3,true);
                }
                else
                {
                    MessageBox.Show("Ingrese una cantidad para stock maximo", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (propiedad == "Precio")/////////////////////////////////////////////////////////////////
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
                                                    <span style='color: black;'>{datosProducto[1]}</span> 
                                                    --- <b>PRECIO ANTERIOR:</b> 
                                                    <span style='color: black;'>${float.Parse(datosProducto[2]).ToString("N2")}</span> 
                                                    --- <b>PRECIO NUEVO:</b> 
                                                    <span style='color: black;'>${precio.ToString("N2")}</span>
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
                    MessageBoxTemporal.Show("ASIGNACION MULTIPLE REALIZADA CON EXITO", "Mensajes del sistema",3,true);
                }
                else
                {
                    MessageBox.Show("Ingrese el precio para asignar", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else if (propiedad == "CantidadMayoreo")
            {
                TextBox txtmayoreo = (TextBox)this.Controls.Find("tbCantidadMayoreo", true)[0];

                var cantidadprodicto = txtmayoreo.Text;

                if (!string.IsNullOrWhiteSpace(cantidadprodicto))
                {
                    foreach (var dato in datosHistPrecio)
                    {
                        var idProd = dato.Key;
                        var precioActual = dato.Value;
                        var precioConDescuento = precioActual * 0.70;
                        float rangoFinalSinDescuento = (float)Convert.ToDecimal(cantidadprodicto) - 1;
                        cn.EjecutarConsulta($"DELETE FROM descuentomayoreo WHERE IDProducto = {idProd}");
                        cn.EjecutarConsulta($"DELETE FROM descuentocliente WHERE IDProducto = {idProd}");
                        cn.EjecutarConsulta($"UPDATE productos SET TipoDescuento = '2' WHERE ID = '{idProd}'");
                        cn.EjecutarConsulta($"INSERT INTO descuentomayoreo(RangoInicial,RangoFinal,Precio,Checkbox,IDProducto) VALUES('1','{rangoFinalSinDescuento}','{precioActual}','1','{idProd}')");
                        cn.EjecutarConsulta($"INSERT INTO descuentomayoreo(RangoInicial,RangoFinal,Precio,Checkbox,IDProducto) VALUES('{cantidadprodicto}','N','{precioConDescuento}','1','{idProd}')");
                    }

                    foreach (var producto in productos)
                    {
                        var idProd = producto.Key;
                        var DTPrecio = cn.CargarDatos($"SELECT Precio FROM productos WHERE ID = {idProd}");
                        decimal precioActual = Convert.ToDecimal(DTPrecio.Rows[0]["Precio"]);
                        decimal precioConDescuento = precioActual * Convert.ToDecimal(0.70);
                        float rangoFinalSinDescuento = (float)Convert.ToDecimal(cantidadprodicto) - 1;
                        cn.EjecutarConsulta($"DELETE FROM descuentomayoreo WHERE IDProducto = {idProd}");
                        cn.EjecutarConsulta($"DELETE FROM descuentocliente WHERE IDProducto = {idProd}");
                        cn.EjecutarConsulta($"UPDATE productos SET TipoDescuento = '2' WHERE ID = '{idProd}'");
                        cn.EjecutarConsulta($"INSERT INTO descuentomayoreo(RangoInicial,RangoFinal,Precio,Checkbox,IDProducto) VALUES('1','{rangoFinalSinDescuento}','{precioActual}','1','{idProd}')");
                        cn.EjecutarConsulta($"INSERT INTO descuentomayoreo(RangoInicial,RangoFinal,Precio,Checkbox,IDProducto) VALUES('{cantidadprodicto}','N','{precioConDescuento}','1','{idProd}')");
                    }
                    MessageBox.Show("Asignacion realizada con Exito", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ingrese la cantidad maxima de productos\n  para tener un descuento", "Aviso del sistema",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                datosHistPrecio.Clear();
            }
            else if (propiedad == "NumeroRevision")/////////////////////////////////////////////////////////////////
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
                    MessageBoxTemporal.Show("ASIGNACION MULTIPLE REALIZADA CON EXITO", "Mensajes del sistema", 3,true);
                }
                else
                {
                    MessageBox.Show("Ingrese el número de revisión para asignar", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (propiedad == "TipoIVA")/////////////////////////////////////////////////////////////////
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
                    MessageBoxTemporal.Show("ASIGNACION MULTIPLE REALIZADA CON EXITO", "Mensajes del sistema", 3,true);
                }
            }
            else if (propiedad == "ClaveProducto")/////////////////////////////////////////////////////////////////
            {
                TextBox txtClave = (TextBox)this.Controls.Find("tbClaveProducto", true).FirstOrDefault();

                var clave = txtClave.Text;
                var consulta = "INSERT IGNORE INTO Productos (ID, ClaveProducto) VALUES";
                var valores = string.Empty;

                //if (!txtClave.Text.Length.Equals(8))
                //{
                //  MessageBox.Show("Ingrese una clave con el formato Correcto", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                   
                //}
                
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
                        MessageBoxTemporal.Show("ASIGNACION MULTIPLE REALIZADA CON EXITO", "Mensajes del sistema", 3, true);
                    }
                    else
                    {
                        MessageBox.Show("Ingrese la clave de producto", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                
               
            }
            else if (propiedad == "ClaveUnidad")/////////////////////////////////////////////////////////////////
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
                    MessageBoxTemporal.Show("ASIGNACION MULTIPLE REALIZADA CON EXITO", "Mensajes del sistema", 3,true);
                }
                else
                {
                    MessageBox.Show("La clave de unidad no es válida", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (propiedad == "Correos")/////////////////////////////////////////////////////////////////
            {
                //var checkPrimero = (CheckBox)Controls.Find("CorreoPrecioProducto", true).First();
                //var checkSegundo = (CheckBox)Controls.Find("CorreoStockProducto", true).First();
                //var checkTercero = (CheckBox)Controls.Find("CorreoStockMinimo", true).First();
                var checkCuarto = (CheckBox)Controls.Find("CorreoVentaProducto", true).First();

                //var correoPrecioProducto = Convert.ToInt16(checkPrimero.Checked);
                //var correoStockProducto = Convert.ToInt16(checkSegundo.Checked);
                //var correoStockMinimo = Convert.ToInt16(checkTercero.Checked);
                var correoVentaProducto = Convert.ToInt16(checkCuarto.Checked);

                var consulta = "INSERT IGNORE INTO CorreosProducto (ID, IDUsuario,IDProducto, CorreoVentaProducto) VALUES";
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
                    MessageBoxTemporal.Show("ASIGNACION MULTIPLE REALIZADA CON EXITO", "Mensajes del sistema", 3,true);
                }
            }
            else if (propiedad == "Proveedor")/////////////////////////////////////////////////////////////////
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
                    MessageBoxTemporal.Show("ASIGNACION MULTIPLE REALIZADA CON EXITO", "Mensajes del sistema", 3,true);
                }
                else
                {
                    MessageBox.Show("Es necesario seleccionar un proveedor", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (propiedad == "AgregarDescuento")/////////////////////////////////////////////////////////////////AGREGAR DESCUENTO
            {
                DialogResult dialogResult = MessageBox.Show("El descuento se aplicara a todo los productos seleccionados\n si uno de estos productos ya contaba con un descuento se remplazara por este nuevo descuento.", "Aviso del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    TextBox txtDescuento = (TextBox)this.Controls.Find("tbAgregarDescuento", true)[0];
                    var descuento = txtDescuento.Text;

                    foreach (var item in productos)
                    {
                        var idprod = item.Key;

                        var datosProd = cn.CargarDatos($"SELECT * FROM productos WHERE ID = {idprod}");
                        string precioproducto = datosProd.Rows[0]["Precio"].ToString();
                        TextBox porcentaje = (TextBox)this.Controls.Find("tbAgregarDescuento", true)[0];
                        var porcentajedescuento = porcentaje.Text;
                        decimal preciodescuento = ((Convert.ToDecimal(precioproducto) * Convert.ToDecimal(porcentajedescuento)) / 100);
                        decimal preciodescuentofinal = Convert.ToDecimal(precioproducto) - preciodescuento;
                        decimal descuentoaplicado = ((Convert.ToDecimal(precioproducto) * Convert.ToDecimal(porcentajedescuento)) / 100);

                        var insAct = cn.CargarDatos($"SELECT * FROM descuentocliente WHERE IDProducto = {idprod}");

                        if (!insAct.Rows.Count.Equals(0))
                        {
                            cn.EjecutarConsulta($"UPDATE descuentocliente SET PorcentajeDescuento = {descuento}, PrecioDescuento = {preciodescuentofinal}, Descuento = {descuentoaplicado} WHERE IDProducto = {idprod}");
                        }
                        else
                        {
                            cn.EjecutarConsulta($"INSERT INTO descuentocliente (PrecioProducto,PorcentajeDescuento,PrecioDescuento,Descuento,IDProducto) VALUES ({precioproducto},{porcentajedescuento},{preciodescuentofinal},{descuentoaplicado},{idprod})");
                        }
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
               
            }
            else if (propiedad == "EliminarDescuento")/////////////////////////////////////////////////////////////////ELIMINAR DESCUENTO
            {
                DialogResult dialogResult = MessageBox.Show("Se eliminaran los descuentos de todos\n los productos seleccionados.", "Aviso del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (var item in productos)
                    {
                        var idprod = item.Key;
                        cn.EjecutarConsulta($"UPDATE productos SET TipoDescuento = 0 WHERE ID = {idprod}");
                        cn.EjecutarConsulta($"DELETE FROM descuentocliente WHERE IDProducto = {idprod}");
                        cn.EjecutarConsulta($"DELETE FROM descuentomayoreo WHERE IDProducto = {idprod}");
                    } 
                }
                else if (dialogResult == DialogResult.No)
                {
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
                    MessageBoxTemporal.Show("ASIGNACION MULTIPLE REALIZADA CON EXITO", "Mensajes del sistema", 3,true);
                }
            }
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            //Dispose();
            this.Close();
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
            //if (Application.OpenForms.OfType<Cargando>().Count() == 1)
            //{
            //    e.Cancel = true;

            //    Application.OpenForms.OfType<Cargando>().First().BringToFront();
            //}
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

        private void AsignarPropiedad_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void AsignarPropiedad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }
    }
}
