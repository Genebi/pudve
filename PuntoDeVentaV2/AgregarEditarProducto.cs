using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class AgregarEditarProducto : Form
    {
        static public int id = 1;
        static public string precioProducto = "";
        static public string datosImpuestos = null;
        static public string claveProducto = null;
        static public string claveUnidadMedida = null;
        static public List<string> descuentos = new List<string>();

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        AgregarDetalleFacturacionProducto FormDetalle;
        AgregarDescuentoProducto FormAgregar;

        int idProducto;

        /************************
		*	Codigo de Emmanuel	*
		************************/

        List<string> codigosBarrras = new List<string>();   // para agregar los datos extras de codigos de barras

        public DataTable dtClaveInterna;        // almacena el resultado de la funcion de CargarDatos de la funcion searchClavIntProd
        public DataTable dtCodBar;              // almacena el resultado de la funcion de CargarDatos de la funcion searchCodBar

        int resultadoSearchNoIdentificacion;    // sirve para ver si el producto existe en los campos CodigoBarras y ClaveInterna en la funcion searchClavIntProd()
        int resultadoSearchCodBar;              // sirve para ver si el producto existe en los campos CodigoBarras y ClaveInterna en la funcion searchCodBar()

        // funsion para poder buscar en los productos 
        // si coincide con los campos de de ClaveInterna
        // respecto al stock del producto en su campo de NoIdentificacion
        public void searchClavIntProd()
        {
            // preparamos el Query
            string search = $"SELECT Prod.ID, Prod.Nombre, Prod.ClaveInterna, Prod.Stock, Prod.CodigoBarras, Prod.Precio FROM Productos Prod WHERE Prod.IDUsuario = '{FormPrincipal.userID}' AND Prod.ClaveInterna = '{txtClaveProducto.Text}' OR Prod.CodigoBarras = '{txtClaveProducto.Text}'";
            dtClaveInterna = cn.CargarDatos(search);    // alamcenamos el resultado de la busqueda en dtClaveInterna
            if (dtClaveInterna.Rows.Count > 0)  // si el resultado arroja al menos una fila
            {
                resultadoSearchNoIdentificacion = 1;    // busqueda positiva
                //MessageBox.Show("No Identificación Encontrado...\nen la claveInterna del Producto\nEsta siendo utilizada actualmente en el Stock", "El Producto no puede registrarse", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtClaveInterna.Rows.Count <= 0)    // si el resultado no arroja ninguna fila
            {
                resultadoSearchNoIdentificacion = 0; // busqueda negativa
                //MessageBox.Show("No Encontrado", "El Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // funsion para poder buscar en los productos 
        // si coincide con los campos de de CodigoBarras
        // respecto al stock del producto en su campo de NoIdentificacion
        public void searchCodBar()
        {
            // preparamos el Query
            string search = $"SELECT Prod.ID, Prod.Nombre, Prod.ClaveInterna, Prod.Stock, Prod.CodigoBarras, Prod.Precio FROM Productos Prod WHERE Prod.IDUsuario = '{FormPrincipal.userID}' AND Prod.CodigoBarras = '{txtCodigoBarras.Text}' OR Prod.ClaveInterna = '{txtCodigoBarras.Text}'";
            dtCodBar = cn.CargarDatos(search);  // alamcenamos el resultado de la busqueda en dtClaveInterna
            if (dtCodBar.Rows.Count > 0)        // si el resultado arroja al menos una fila
            {
                resultadoSearchCodBar = 1; // busqueda positiva
                //MessageBox.Show("No Identificación Encontrado...\nen el Código de Barras del Producto\nEsta siendo utilizada actualmente en el Stock", "El Producto no puede registrarse", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtCodBar.Rows.Count <= 0)  // si el resultado no arroja ninguna fila
            {
                resultadoSearchCodBar = 0; // busqueda negativa
                //MessageBox.Show("Codigo Bar Disponible", "Este Codigo libre", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /* Fin del codigo de Emmanuel */

        public AgregarEditarProducto(string titulo)
        {
            InitializeComponent();
        }

        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string texto = txtCodigoBarras.Text;

                if (texto.Length >= 5)
                {
                    GenerarTextBox();
                    //MessageBox.Show(texto, "Mensaje");
                }
            }
        }

        private void GenerarTextBox()
        {
            FlowLayoutPanel panelHijo = new FlowLayoutPanel();
            panelHijo.Name = "panelGenerado" + id;
            panelHijo.Height = 25;
            panelHijo.Width = 200;
            panelHijo.HorizontalScroll.Visible = false;

            TextBox tb = new TextBox();
            tb.Name = "textboxGenerado" + id;
            tb.Width = 165;
            tb.Height = 20;
            tb.KeyDown += new KeyEventHandler(TextBox_Keydown);

            Button bt = new Button();
            bt.Cursor = Cursors.Hand;
            bt.Text = "X";
            bt.Name = "btnGenerado" + id;
            bt.Height = 23;
            bt.Width = 23;
            bt.BackColor = ColorTranslator.FromHtml("#C00000");
            bt.ForeColor = ColorTranslator.FromHtml("white");
            bt.FlatStyle = FlatStyle.Flat;
            bt.TextAlign = ContentAlignment.MiddleCenter;
            bt.Anchor = AnchorStyles.Top;
            bt.Click += new EventHandler(ClickBotones);

            panelHijo.Controls.Add(tb);
            panelHijo.Controls.Add(bt);
            panelHijo.FlowDirection = FlowDirection.LeftToRight;
            
            panelContenedor.Controls.Add(panelHijo);
            panelContenedor.FlowDirection = FlowDirection.TopDown;

            tb.Focus();
            id++;
        }

        private void TextBox_Keydown(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (e.KeyCode == Keys.Enter)
            {
                string texto = tbx.Text;

                if (texto.Length >= 5)
                {
                    GenerarTextBox();
                }
            }
        }

        private void ClickBotones(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            string nombreBoton = bt.Name;

            string idBoton = nombreBoton.Substring(11);
            string nombreTextBox = "textboxGenerado" + idBoton;
            string nombrePanel = "panelGenerado" + idBoton;

            foreach (Control item in panelContenedor.Controls.OfType<Control>())
            {
                if (item.Name == nombrePanel)
                {
                    panelContenedor.Controls.Remove(item);
                    panelContenedor.Controls.Remove(bt);
                }
            }
        }

        private void btnGenerarCB_Click(object sender, EventArgs e)
        {

            string fecha = DateTime.Now.ToString();
            fecha = fecha.Replace(" ", "");
            fecha = fecha.Replace("/", "");
            fecha = fecha.Replace(":", "");
            fecha = fecha.Substring(3, 11);

            txtCodigoBarras.Text = fecha;
        }

        private void btnAgregarDescuento_Click(object sender, EventArgs e)
        {
            if (txtPrecioProducto.Text == "")
            {
                MessageBox.Show("Es necesario agregar el precio del producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtStockProducto.Text == "")
            {
                MessageBox.Show("Es necesario agregar el stock del producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (FormAgregar != null)
                {
                    FormAgregar.Show();
                    FormAgregar.BringToFront();
                }
                else
                {
                    FormAgregar = new AgregarDescuentoProducto();
                    FormAgregar.ShowDialog();
                }
            }
        }

        private void btnDetalleFacturacion_Click(object sender, EventArgs e)
        {
            if (txtPrecioProducto.Text == "")
            {
                MessageBox.Show("Es necesario agregar el precio del producto", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //Verifica que el formulario ya tenga una instancia creada, de lo contrario la crea
                if (FormDetalle != null)
                {
                    FormDetalle.Show();
                    FormDetalle.BringToFront();
                }
                else
                {
                    FormDetalle = new AgregarDetalleFacturacionProducto();
                    FormDetalle.ShowDialog();
                }
            }
        }

        private void txtPrecioProducto_KeyUp(object sender, KeyEventArgs e)
        {
            precioProducto = txtPrecioProducto.Text;
        }

        private void btnImagenes_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog imagenes = new OpenFileDialog();
                imagenes.Filter = "Imagenes JPG (*.jpg)|*.jpg| Imagenes PNG (.png)|.png";

                if (imagenes.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Imagen seleccionada");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            /****************************
			*	codigo de Alejandro		*
			****************************/
            var nombre = txtNombreProducto.Text;
            var stock = txtStockProducto.Text;
            var precio = txtPrecioProducto.Text;
            var categoria = txtCategoriaProducto.Text;
            var claveIn = txtClaveProducto.Text;
            var codigoB = txtCodigoBarras.Text;
            var tipoDescuento = "0";

            /*	Fin del codigo de Alejandro	*/

            /************************************
            *   iniciamos las variables a 0     *
			*	codigo de Emmanuel				*
            ************************************/
            resultadoSearchNoIdentificacion = 0;    // ponemos los valores en 0
            resultadoSearchCodBar = 0;              // ponemos los valores en 0

            searchClavIntProd();        // hacemos la busqueda que no se repita en CalveInterna
            searchCodBar();             // hacemos la busqueda que no se repita en CodigoBarra

            if (resultadoSearchNoIdentificacion == 1 && resultadoSearchCodBar == 1)
            {
                MessageBox.Show("El Número de Identificación; ya se esta utilizando en\ncomo clave interna ó codigo de barras de algun producto", "Error de Actualizar el Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (resultadoSearchNoIdentificacion == 0 || resultadoSearchCodBar == 0)
            {
                /****************************
				*	codigo de Alejandro		*
				****************************/
                if (descuentos.Any())
                {
                    //Cerramos la ventana donde se eligen los descuentos
                    FormAgregar.Close();
                    tipoDescuento = descuentos[0];
                }

                string[] guardar = new string[] { nombre, stock, precio, categoria, claveIn, codigoB, claveProducto, claveUnidadMedida, tipoDescuento };

                //Se guardan los datos principales del producto
                int respuesta = cn.EjecutarConsulta(cs.GuardarProducto(guardar, FormPrincipal.userID));

                if (respuesta > 0)
                {
                    //Se obtiene la ID del último producto agregado
                    idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));

                    //Se realiza el proceso para guardar los detalles de facturación del producto
                    if (datosImpuestos != null)
                    {
                        //Cerramos la ventana donde se eligen los impuestos
                        FormDetalle.Close();

                        string[] listaImpuestos = datosImpuestos.Split('|');

                        int longitud = listaImpuestos.Length;

                        if (longitud > 0)
                        {
                            for (int i = 0; i < longitud; i++)
                            {
                                string[] imp = listaImpuestos[i].Split(',');

                                if (imp[3] == " - ") { imp[3] = "0"; }
                                if (imp[4] == " - ") { imp[4] = "0"; }
                                if (imp[5] == " - ") { imp[5] = "0"; }

                                guardar = new string[] { imp[0], imp[1], imp[2], imp[3], imp[4], imp[5] };

                                cn.EjecutarConsulta(cs.GuardarDetallesProducto(guardar, idProducto));
                            }
                        }

                        datosImpuestos = null;
                    }

                    //Se realiza el proceso para guardar el descuento del producto en caso de que se haya agregado uno
                    if (descuentos.Any())
                    {
                        //Descuento por Cliente
                        if (descuentos[0] == "1")
                        {
                            guardar = new string[] { descuentos[1], descuentos[2], descuentos[3], descuentos[4] };

                            cn.EjecutarConsulta(cs.GuardarDescuentoCliente(guardar, idProducto));
                        }

                        //Descuento por Mayoreo
                        if (descuentos[0] == "2")
                        {
                            foreach (var descuento in descuentos)
                            {
                                if (descuento == "2") { continue; }

                                string[] tmp = descuento.Split('-');

                                cn.EjecutarConsulta(cs.GuardarDescuentoMayoreo(tmp, idProducto));
                            }
                        }
                    }
                    foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
                    {
                        foreach (Control item in panel.Controls)
                        {
                            if (item is TextBox)
                            {
                                var tb = item.Text;
                                codigosBarrras.Add(tb);
                            }
                        }
                    }
                    if (codigosBarrras!=null || codigosBarrras.Count!=0)
                    {
                        for(int pos=0; pos<codigosBarrras.Count; pos++)
                        {
                            string insert = $"INSERT INTO CodigoBarrasExtras(CodigoBarraExtra, IDProducto)VALUES('{codigosBarrras[pos]}','{idProducto}')";
                            cn.EjecutarConsulta(insert);
                        }
                    }
                    //Cierra la ventana donde se agregan los datos del producto
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al intentar registrar el producto");
                }
                /*	Fin del codigo de Alejandro	*/
            }
            /* Fin del codigo de Emmanuel */
        }
    }
}
