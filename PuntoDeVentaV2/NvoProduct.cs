using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class NvoProduct : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosGenerales mg = new MetodosGenerales();

        public string ProdNombre { get; set; }
        public string ProdStock { get; set; }
        public string ProdPrecio { get; set; }
        public string ProdPrecioCompra { get; set; }
        public string ProdCategoria { get; set; }
        public string ProdClaveInterna { get; set; }
        public string ProdCodBarras { get; set; }
        public string fechaProdServicioXML { get; set; }
        public string FolioProdServicioXML { get; set; }
        public string RFCProdServicioXML { get; set; }
        public string NobEmisorProdServicioXML { get; set; }
        public string ClaveProdEmisorProdServicioXML { get; set; }
        public string UnidadMedidaProdServicioXML { get; set; }
        public string DescuentoProdServicioXML { get; set; }
        public string NameNvoProveedorXML { get; set; }
        public string IdNvoProveedorXML { get; set; }

        static public string ProdNombreFin = "";
        static public string ProdStockFin = "";
        static public string ProdPrecioFin = "";
        static public string ProdPrecioCompraFin = "";
        static public string ProdCategoriaFin = "";
        static public string ProdClaveInternaFin = "";
        static public string ProdCodBarrasFin = "";
        static public string FechaXMLNvoProdServicio = "";
        static public string FolioXMLNvoProdServicio = "";
        static public string RFCXMLNvoProdServicio = "";
        static public string NobEmisorXMLNvoProdServicio = "";
        static public string ClaveProdEmisorXMLNvoProdServicio = "";
        static public string UnidadMedidaXMLProdServicio = "";
        static public string DescuentoXMLNvoProdServicio = "";
        static public string nameProveedorXML;
        static public string idProveedorXML;

        float stockNvo, precioNvo, PrecioCompraNvo;

        string baseProducto = null;
        string ivaProducto = null;
        string impuestoProducto = null;

        DataTable dtClaveInterna, dtCodBar, SearchProdResult, SearchCodBarExtResult;
        List<string> codigosBarrras = new List<string>();   // para agregar los datos extras de codigos de barras
        int resultadoSearchNoIdentificacion, resultadoSearchCodBar, idProducto, id;

        Control _lastEnteredControl;    // para saber cual fue el ultimo control con el cursor activo

        OpenFileDialog f;

        FileStream File, File1;
        FileInfo info;

        string fileName, oldDirectory, NvoFileName, logoTipo = "", queryBuscarProd, idProductoBuscado, tipoProdServ, queryBuscarCodBarExt;
        string saveDirectoryImg = string.Empty;

        const string fichero = @"\PUDVE\settings\codbar\setupCodBar.txt";       // directorio donde esta el archivo de numero de codigo de barras consecutivo
        string Contenido;                                                       // para obtener el numero que tiene el codigo de barras en el arhivo

        long CodigoDeBarras;                                                    // variable entera para llevar un consecutivo de codigo de barras

        string nombre;
        string stock;
        string precio;
        string categoria;
        string claveIn;
        string codigoB;
        string claveProducto = "";
        string claveUnidadMedida = "";
        string ProdServPaq = "P".ToString();
        string tipoDescuento = "0";
        string idUsrNvo;

        public NvoProduct()
        {
            InitializeComponent();
        }

        private void btnGenerarCB_Click(object sender, EventArgs e)
        {
            // leemos el archivo de codigo de barras que lleva el consecutivo
            using (StreamReader readfile = new StreamReader(Properties.Settings.Default.rutaDirectorio + fichero))
            {
                Contenido = readfile.ReadToEnd();   // se lee todo el archivo y se almacena en la variable Contenido
            }
            if (Contenido == "")        // si el contenido es vacio 
            {
                PrimerCodBarras();      // iniciamos el conteo del codigo de barras
                AumentarCodBarras();    // Aumentamos el codigo de barras para la siguiente vez que se utilice
            }
            else if (Contenido != "")   // si el contenido no es vacio
            {
                //MessageBox.Show("Trabajando en el Proceso");
                AumentarCodBarras();    // Aumentamos el codigo de barras para la siguiente vez que se utilice
            }
        }

        private void AumentarCodBarras()
        {
            string txtBoxName;
            txtBoxName = _lastEnteredControl.Name;
            if (txtBoxName != "cbTipo" && txtBoxName != "txtNombreProducto" && txtBoxName != "txtStockProducto" && txtBoxName != "txtPrecioProducto" && txtBoxName != "txtCategoriaProducto")
            {
                _lastEnteredControl.Text = Contenido;

                CodigoDeBarras = long.Parse(Contenido);
                CodigoDeBarras++;
                Contenido = CodigoDeBarras.ToString();

                using (StreamWriter outfile = new StreamWriter(Properties.Settings.Default.rutaDirectorio + fichero))
                {
                    outfile.WriteLine(Contenido);
                }
            }
            else
            {
                MessageBox.Show("Campo no Valido para generar\nCodigo de Barras los campos validos son\nClave Interna y Codigo de Barras... Gracias", "Anvertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrimerCodBarras()
        {
            Contenido = "7777000001";
        }

        private void txtNombreProducto_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;
        }

        private void txtStockProducto_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;
        }

        private void txtPrecioProducto_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;
        }

        private void txtCategoriaProducto_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;
        }

        private void txtClaveProducto_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;
        }

        private void txtCodigoBarras_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;
        }

        private void NvoProduct_Load(object sender, EventArgs e)
        {
            baseProducto = "0";
            ivaProducto = "0";

            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                saveDirectoryImg = $@"\\{servidor}\PUDVE\Productos\";
            }
            else
            {
                saveDirectoryImg = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Productos\";
            }

            cargarDatos();
        }

        private void cargarDatos()
        {
            ProdNombreFin = ProdNombre;
            ProdStockFin = ProdStock;
            ProdPrecioFin = ProdPrecio;
            ProdPrecioCompraFin = ProdPrecioCompra;
            ProdCategoriaFin = ProdCategoria;
            ProdClaveInternaFin = ProdClaveInterna;
            ProdCodBarrasFin = ProdCodBarras;

            FechaXMLNvoProdServicio = fechaProdServicioXML;
            FolioXMLNvoProdServicio = FolioProdServicioXML;
            RFCXMLNvoProdServicio = RFCProdServicioXML;
            NobEmisorXMLNvoProdServicio = NobEmisorProdServicioXML;
            ClaveProdEmisorXMLNvoProdServicio = ClaveProdEmisorProdServicioXML;
            UnidadMedidaXMLProdServicio = UnidadMedidaProdServicioXML;
            DescuentoXMLNvoProdServicio = DescuentoProdServicioXML;
            idProveedorXML = IdNvoProveedorXML;
            nameProveedorXML = NameNvoProveedorXML;

            txtNombreProducto.Text = ProdNombreFin;
            txtStockProducto.Text = ProdStockFin;
            cargarPrecio();
            txtPrecioVentaProducto.Text = precioNvo.ToString("N2");
            txtPrecioCompra.Text = PrecioCompraNvo.ToString("N2");
            txtCategoriaProducto.Text = ProdCategoriaFin;
            txtClaveProducto.Text = ProdClaveInternaFin;
            txtCodigoBarras.Text = ProdCodBarrasFin;
            cargarDatosExtra();
        }
        
        private void txtCategoriaProducto_TextChanged(object sender, EventArgs e)
        {
            txtCategoriaProducto.CharacterCasing = CharacterCasing.Upper;
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
            tb.Enter += new EventHandler(TextBox_Enter);
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

        private void TextBox_Enter(object sender, EventArgs e)
        {
            _lastEnteredControl = (Control)sender;
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

        private void btnImagenes_Click(object sender, EventArgs e)
        {
            try
            {
                // Abrirmos el OpenFileDialog para buscar y seleccionar la Imagen
                using (f = new OpenFileDialog())
                {
                    // le aplicamos un filtro para solo ver 
                    // imagenes de tipo *.jpg y *.png 
                    f.Filter = "Imagenes JPG (*.jpg)|*.jpg| Imagenes PNG (*.png)|*.png";

                    // si se selecciono correctamente un archivo en el OpenFileDialog
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        /************************************************
                        *   usamos el objeto File para almacenar las    *
                        *   propiedades de la imagen                    *
                        ************************************************/
                        using (File = new FileStream(f.FileName, FileMode.Open, FileAccess.Read))
                        {
                            pictureBoxProducto.Image = Image.FromStream(File);      // Cargamos la imagen en el PictureBox
                            info = new FileInfo(f.FileName);                        // Obtenemos toda la Informacion de la Imagen
                            fileName = Path.GetFileName(f.FileName);                // Obtenemos el nombre de la imagen
                            oldDirectory = info.DirectoryName;                      // Obtenemos el directorio origen de la Imagen
                            File.Dispose();                                         // Liberamos el objeto File
                        }
                    }
                }

                // verificamos que si no existe el directorio
                if (!Directory.Exists(saveDirectoryImg))
                {
                    // lo crea para poder almacenar la imagen
                    Directory.CreateDirectory(saveDirectoryImg);
                }

                // si el archivo existe
                if (f.CheckFileExists)
                {
                    // Intentamos la actualizacion de la imagen en la base de datos
                    try
                    {
                        // Obtenemos el Nuevo nombre de la imagen
                        // con la que se va hacer la copia de la imagen
                        var source = txtNombreProducto.Text;
                        var replacement = source.Replace('/', '_').Replace('\\', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('\"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_').Replace('-', '_').Replace(' ', '_');
                        NvoFileName = replacement + ".jpg";

                        // si Logotipo es diferente a ""
                        if (logoTipo != "")
                        {
                            // si el File1 es igual a null
                            if (File1 != null)
                            {
                                // liberamos el objeto File1
                                File1.Dispose();

                                // Verificamos si el pictureBox es null
                                if (pictureBoxProducto.Image != null)
                                {
                                    // Liberamos el PictureBox para poder borrar su imagen
                                    pictureBoxProducto.Image.Dispose();
                                    // borramos el archivo de la imagen
                                    System.IO.File.Delete(saveDirectoryImg + NvoFileName);
                                    // realizamos la copia de la imagen origen hacia el nuevo destino
                                    System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
                                    // Obtenemos el nuevo Path
                                    logoTipo = NvoFileName;
                                    // leemos el archivo de imagen y lo ponemos el pictureBox
                                    using (File = new FileStream(saveDirectoryImg + NvoFileName, FileMode.Open, FileAccess.Read))
                                    {
                                        // cargamos la imagen en el PictureBox
                                        pictureBoxProducto.Image = Image.FromStream(File);
                                    }
                                }
                            }
                            else
                            {
                                // si es que file1 es igual a null
                                // realizamos la copia de la imagen origen hacia el nuevo destino
                                System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
                                // obtenemos el nuevo path
                                logoTipo = NvoFileName;
                            }
                        }
                        // si el valor de la variable es Null o esta ""
                        if (logoTipo == "" || logoTipo == null)
                        {
                            // Liberamos el pictureBox para poder borrar su imagen
                            pictureBoxProducto.Image.Dispose();
                            // realizamos la copia de la imagen origen hacia el nuevo destino
                            System.IO.File.Copy(oldDirectory + @"\" + fileName, saveDirectoryImg + NvoFileName, true);
                            // obtenemos el nuevo path
                            logoTipo = NvoFileName;
                            // leemos el archivo de imagen y lo ponemos el pictureBox
                            using (File = new FileStream(saveDirectoryImg + NvoFileName, FileMode.Open, FileAccess.Read))
                            {
                                // carrgamos la imagen en el PictureBox
                                pictureBoxProducto.Image = Image.FromStream(File);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // si no se puede hacer el proceso
                        // si no se borra el archivo muestra este mensaje
                        MessageBox.Show("Error al hacer el borrado No: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                // si el nombre del archivo esta en blanco
                // si no selecciona un archivo valido o ningun archivo muestra este mensaje
                MessageBox.Show("selecciona una Imagen", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void cargarPrecio()
        {

            stockNvo = (float)Convert.ToDouble(txtStockProducto.Text) * AgregarStockXML.stockProdXML;
            txtStockProducto.Text = stockNvo.ToString();
            precioNvo = ((float)Convert.ToDouble(ProdPrecioFin) * AgregarStockXML.stockProdXML) / stockNvo;
            PrecioCompraNvo = (float)Convert.ToDouble(ProdPrecioCompraFin);
        }

        public void cargarDatosExtra()
        {
            //queryBuscarProd = $"SELECT * FROM Productos WHERE Nombre = '{ProdNombre}' AND Precio = '{ProdPrecio}' AND Categoria = '{ProdCategoria}' AND IDUsuario = '{FormPrincipal.userID}'";
            //SearchProdResult = cn.CargarDatos(queryBuscarProd);
            //idProductoBuscado = SearchProdResult.Rows[0]["ID"].ToString();
            //tipoProdServ = SearchProdResult.Rows[0]["Tipo"].ToString();
            //queryBuscarCodBarExt = $"SELECT * FROM CodigoBarrasExtras WHERE IDProducto = '{idProductoBuscado}'";
            //SearchCodBarExtResult = cn.CargarDatos(queryBuscarCodBarExt);
            //cargarCodBarExt();
            //queryBuscarDescuentoCliente = $"SELECT * FROM DescuentoCliente WHERE IDProducto = '{idProductoBuscado}'";
            //SearchDesCliente = cn.CargarDatos(queryBuscarDescuentoCliente);
            //queryDesMayoreo = $"SELECT * FROM DescuentoMayoreo WHERE IDProducto = '{idProductoBuscado}'";
            //SearchDesMayoreo = cn.CargarDatos(queryDesMayoreo);
            //if (tipoProdServ == "S")
            //{
            //    queryProductosDeServicios = $"SELECT * FROM ProductosDeServicios WHERE IDServicio = '{idProductoBuscado}'";
            //    dtProductosDeServicios = cn.CargarDatos(queryProductosDeServicios);
            //    cbTipo.Text = "Servicio / Paquete ó Combo";
            //    btnAdd.Visible = true;
            //}
            //else if (tipoProdServ == "P")
            //{
            //    cbTipo.Text = "Producto";
            //    btnAdd.Visible = false;
            //}
        }

        private void cargarCodBarExt()
        {
            id = 0;
            panelContenedor.Controls.Clear();
            foreach (DataRow renglon in SearchCodBarExtResult.Rows)
            {
                // generamos el panel dinamico
                FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                panelHijo.Name = "panelGenerado" + id;
                panelHijo.Height = 25;
                panelHijo.Width = 200;
                panelHijo.HorizontalScroll.Visible = false;

                // generamos el textbox dinamico 
                TextBox tb = new TextBox();
                tb.Name = "textboxGenerado" + id;
                tb.Width = 165;
                tb.Height = 20;
                tb.Text = renglon[1].ToString();
                tb.Enter += new EventHandler(TextBox_Enter);
                tb.KeyDown += new KeyEventHandler(TextBox_Keydown);

                // generamos el boton dinamico
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

                // agregamos al panel el textbox
                panelHijo.Controls.Add(tb);

                // agregamos el boton
                panelHijo.Controls.Add(bt);
                // le damos la direccion del panel
                panelHijo.FlowDirection = FlowDirection.LeftToRight;

                // agregamos el panel a la forma
                panelContenedor.Controls.Add(panelHijo);
                // darle direccion al panel
                panelContenedor.FlowDirection = FlowDirection.TopDown;

                tb.Focus();
                id++;
            }
        }

        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            nombre = txtNombreProducto.Text;
            ProdNombre = nombre;
            stock = txtStockProducto.Text;
            precio = txtPrecioVentaProducto.Text;
            categoria = txtCategoriaProducto.Text;
            claveIn = txtClaveProducto.Text;
            codigoB = txtCodigoBarras.Text;
            claveProducto = ClaveProdEmisorXMLNvoProdServicio;
            claveUnidadMedida = UnidadMedidaXMLProdServicio;
            ProdServPaq = "P".ToString();
            tipoDescuento = "0";
            idUsrNvo = FormPrincipal.userID.ToString();

            resultadoSearchNoIdentificacion = 0;    // ponemos los valores en 0
            resultadoSearchCodBar = 0;              // ponemos los valores en 0

            searchClavIntProd();                    // hacemos la busqueda que no se repita en CalveInterna
            searchCodBar();                         // hacemos la busqueda que no se repita en CodigoBarra
            if (resultadoSearchNoIdentificacion == 1 || resultadoSearchCodBar == 1)
            {
                MessageBox.Show("El Número que proporciono; ya se esta utilizando\ncomo clave interna ó como codigo de barras de algun producto", "Error de Actualizar el Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (resultadoSearchNoIdentificacion == 0 || resultadoSearchCodBar == 0)
            {
                string[] guardar;
                int respuesta = 0;
                guardar = new string[] {
                    nombre, stock, precio, categoria, claveIn, codigoB, claveProducto, claveUnidadMedida,
                    tipoDescuento, idUsrNvo, logoTipo, ProdServPaq, baseProducto, ivaProducto, impuestoProducto,
                    mg.RemoverCaracteres(nombre), mg.RemoverPreposiciones(nombre), "0", "0", txtPrecioCompra.Text,
                    "0"
                };
                //Se guardan los datos principales del producto
                respuesta = cn.EjecutarConsulta(cs.GuardarProducto(guardar, FormPrincipal.userID));

                if (respuesta > 0)
                {
                    int found = 10;
                    string fechaXML = FechaXMLNvoProdServicio;
                    string fecha = fechaXML.Substring(0, found);
                    string hora = fechaXML.Substring(found + 1);
                    string fechaCompleta = fecha + " " + hora;
                    string folio = FolioXMLNvoProdServicio;
                    string RFCEmisor = RFCXMLNvoProdServicio;
                    string nombreEmisor = NobEmisorXMLNvoProdServicio;
                    string claveProdEmisor = ClaveProdEmisorXMLNvoProdServicio;
                    string descuentoXML = DescuentoXMLNvoProdServicio;
                    string NombreProveedorXML = nameProveedorXML;
                    string numeroProveedorXML = idProveedorXML;

                    //Se obtiene la ID del último producto agregado
                    idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));

                    cn.EjecutarConsulta(cs.GuardarDetallesDelProducto(idProducto, FormPrincipal.userID, NombreProveedorXML, Convert.ToInt32(numeroProveedorXML)));
                    
                    string query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor,IDProducto,IDUsuario,IDEmpleado) VALUES('{nombre}','{stock}','{txtPrecioCompra.Text}','{descuentoXML}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}','{idProducto}','{FormPrincipal.userID}',{FormPrincipal.id_empleado})";
                    try
                    {
                        cn.EjecutarConsulta(query);
                        idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM HistorialCompras ORDER BY ID DESC LIMIT 1", 1));
                        //MessageBox.Show("Registrado Intento 1", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error :" + ex);
                    }

                    DateTime date1 = DateTime.Now;
                    fechaCompleta = date1.ToString("s");
                    string Year = fechaCompleta.Substring(0, found);
                    string Date = fechaCompleta.Substring(found + 1);
                    string FechaRegistrada = Year + " " + Date;
                    string queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{FormPrincipal.userID}','{idProducto}','{FechaRegistrada}')";
                    cn.EjecutarConsulta(queryRecordHistorialProd);

                    //Se obtiene la ID del último producto agregado
                    idProducto = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM Productos ORDER BY ID DESC LIMIT 1", 1));
                    // recorrido para FlowLayoutPanel para ver cuantos TextBox
                    foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
                    {
                        // hacemos un objeto para ver que tipo control es
                        foreach (Control item in panel.Controls)
                        {
                            // ver si el control es TextBox
                            if (item is TextBox)
                            {
                                var tb = item.Text;         // almacenamos en la variable tb el texto de cada TextBox
                                codigosBarrras.Add(tb);     // almacenamos en el List los codigos de barras
                            }
                        }
                    }
                    // verificamos si el List esta con algun registro 
                    if (codigosBarrras != null || codigosBarrras.Count != 0)
                    {
                        // hacemos recorrido del List para gregarlos en los codigos de barras extras
                        for (int pos = 0; pos < codigosBarrras.Count; pos++)
                        {
                            // preparamos el Query
                            string insert = $"INSERT INTO CodigoBarrasExtras(CodigoBarraExtra, IDProducto)VALUES('{codigosBarrras[pos]}','{idProducto}')";
                            cn.EjecutarConsulta(insert);    // Realizamos el insert en la base de datos
                        }
                    }
                    codigosBarrras.Clear();
                    //Cierra la ventana donde se agregan los datos del producto
                    this.Close();
                }
            }
        }

        // funsion para poder buscar en los productos 
        // si coincide con los campos de de CodigoBarras
        // respecto al stock del producto en su campo de NoIdentificacion
        private void searchCodBar()
        {
            string search = $@"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND (CodigoBarras  = '{txtCodigoBarras}' OR ClaveInterna = '{txtCodigoBarras}')";
            dtCodBar = cn.CargarDatos(search);    // alamcenamos el resultado de la busqueda en dtClaveInterna
            foreach (DataRow row in dtCodBar.Rows)
            {
                string textoBuscar = row["CodBar"].ToString().Replace("\r\n", "").Trim();
                string codBar = txtCodigoBarras.Text;
                if (textoBuscar != "")
                {
                    if (textoBuscar == codBar)
                    {
                        resultadoSearchCodBar = 1;    // busqueda positiva
                        break;
                    }
                    else if (textoBuscar != codBar)
                    {
                        resultadoSearchCodBar = 0; // busqueda negativa
                    }
                }
            }
        }

        // funsion para poder buscar en los productos 
        // si coincide con los campos de de ClaveInterna
        // respecto al stock del producto en su campo de NoIdentificacion
        public void searchClavIntProd()
        {
            string search = $@"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND (CodigoBarras  = '{txtClaveProducto}' OR ClaveInterna = '{txtClaveProducto}')";
            dtClaveInterna = cn.CargarDatos(search);    // alamcenamos el resultado de la busqueda en dtClaveInterna
            foreach (DataRow row in dtClaveInterna.Rows)
            {
                string textoBuscar = row["ClavInt"].ToString().Replace("\r\n", "").Trim();
                string clavInterna = txtClaveProducto.Text;
                if (textoBuscar != "")
                {
                    if (textoBuscar == clavInterna)
                    {
                        resultadoSearchNoIdentificacion = 1;    // busqueda positiva
                        break;
                    }
                    else if (textoBuscar != clavInterna)
                    {
                        resultadoSearchNoIdentificacion = 0; // busqueda negativa
                    }
                }
            }
        }
    }
}
