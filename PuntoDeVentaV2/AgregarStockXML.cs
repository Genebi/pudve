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
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PuntoDeVentaV2
{
    public partial class AgregarStockXML : Form
    {
        /************************************************************
        *                                                           * 
        *   Se Inicia la clase para el recorrido y lectura del XML  *
        *   con sus respectivas partial class para hacer los array  *
        *                                                           *   
        ************************************************************/
        [XmlRootAttribute(Namespace = "http://www.sat.gob.mx/cfd/3")]
        public class Comprobante
        {
            [XmlAttributeAttribute()]
            public string Version;
            [XmlAttributeAttribute()]
            public string Folio;
            [XmlAttributeAttribute()]
            public string Fecha;
            [XmlAttributeAttribute()]
            public string Sello;
            [XmlAttributeAttribute()]
            public string FormaPago;
            [XmlAttributeAttribute()]
            public string NoCertificado;
            [XmlAttributeAttribute()]
            public string Certificado;
            [XmlAttributeAttribute()]
            public string SubTotal;
            [XmlAttributeAttribute()]
            public string Descuento;
            [XmlAttributeAttribute()]
            public string Moneda;
            [XmlAttributeAttribute()]
            public string Total;
            [XmlAttributeAttribute()]
            public string TipoDeComprobante;
            [XmlAttributeAttribute()]
            public string MetodoPago;
            [XmlAttributeAttribute()]
            public string LugarExpedicion;
            [XmlElementAttribute()]
            public TEmisor Emisor;
            [XmlElementAttribute()]
            public TReceptor Receptor;
            [XmlArrayItemAttribute("Concepto")]
            public TConcepto[] Conceptos;
            [XmlElementAttribute()]
            public TImpuestos Impuestos;
        }

        public class TEmisor
        {
            [XmlAttributeAttribute()]
            public string Rfc;
            [XmlAttributeAttribute()]
            public string Nombre;
            [XmlElementAttribute()]
            public TRegimenFiscal Regimen;
        }

        public class TRegimenFiscal
        {
            [XmlAttributeAttribute()]
            public string RegimenFiscal;
        }

        public class TReceptor
        {
            [XmlAttributeAttribute()]
            public string Rfc;
            [XmlAttributeAttribute()]
            public string Nombre;
        }

        public class TConcepto
        {
            [XmlAttributeAttribute()]
            public string ClaveProdServ;
            [XmlAttributeAttribute()]
            public string NoIdentificacion;
            [XmlAttributeAttribute()]
            public string Cantidad;
            [XmlAttributeAttribute()]
            public string ClaveUnidad;
            [XmlAttributeAttribute()]
            public string Unidad;
            [XmlAttributeAttribute()]
            public string Descripcion;
            [XmlAttributeAttribute()]
            public string ValorUnitario;
            [XmlAttributeAttribute()]
            public string Importe;
            [XmlAttributeAttribute()]
            public string Descuento;
        }

        public class TImpuestos
        {
            [XmlArrayItem("Traslado")]
            public TTraslado[] Traslados;
        }

        public class TTraslado
        {
            [XmlAttributeAttribute()]
            public string Impuesto;
            [XmlAttributeAttribute()]
            public string Importe;
        }

        /************************************************************
        *   Termina la clase para leer el XML y sus respectivas     *
        *   sub class para hacer los array                          *   
        ************************************************************/
        private string rutaXML = string.Empty;
        private string[] impuestosXML;

        /****************************************************
        *       se declaran e inicializan las variables     * 
        *       para poder hacer validaciones  etc.         *
        ****************************************************/
        // se declara el objeto para poder usarlo y llamar la ventana para agregrar Nvo Producto
        public AgregarEditarProducto FormAgregar = new AgregarEditarProducto("Agregar Producto");
        // se declara el objeto para poder usarlo y llamar la ventana para el listado de Productos
        public ListaProductos ListProd = new ListaProductos();

        int consultListProd;    // variable para poder saber si entro a consultar la lista de producto
        string idListProd;      // para poder almacenar la consulta de la lista

        OpenFileDialog f;   // objeto para poder abrir el openDialog

        Conexion cn = new Conexion();   // iniciamos un objeto de tipo conexion

        public string userName;         // declaramos la variable que almacenara el valor de userName
        public string passwordUser;     // declaramos la variable que almacenara el valor de passwordUser
        public string userId;           // declaramos la variable que almacenara el valor de userId

        // variables para poder hacer las consulta y actualizacion a la base de datos
        string buscar;                      // almacena el query para buscar el usuario al que se le va agregar los productos
        string ClaveInterna;                // almacena la calveInterna(NoIdentificacion) del XML
        string NoClaveInterna;              // almacena el Número de clave Interna del XML para su posterior uso en las validaciones
        string NoCodBar;
        string NoCodBarExt;
        string idProducto;                  // almacena el Número de ID del producto para si hay alguna actualizacion poder hacerlo
        string query;                       // almacena el query para poder hacer la actualizacion del producto
        string NombreProd;                  // almacena el contenido del TextBox para poder hacer la actualizacion en la base de datos
        string textBoxNoIdentificacion;     // almacena el contenido del TextBox para poder hacer la actualizacion en la base de datos
        int resultadoConsulta;              // almacenamos el resultado sea 1 o 0

        string concepto;
        float precio;
        string fechaXML;
        string fechaCompleta;
        string fecha;
        string hora;
        string folio;
        string RFCEmisor;
        string nombreEmisor;
        string claveProdEmisor;
        int found = 10;
        int idRecordProd;
        string FechaRegistrada, DateComplete, Year, Date, queryRecordHistorialProd;
        DateTime date1;

        // variables para poder almacenar la tabla que resulta sobre la consulta el base de datos
        public DataTable dt;                    // almacena el resultado de la funcion de CargarDatos de la funcion consulta
        public DataTable dtProductos;           // almacena el resultado de la funcion de CargarDatos de la funcion serachDatos
        public DataTable dtClaveInterna;        // almacena el resultado de la funcion de CargarDatos de la funcion searchClavIntProd
        public DataTable dtCodBar;              // almacena el resultado de la funcion de CargarDatos de la funcion searchCodBar
        public DataTable dtSugeridos;           // almacena el resultado de la funcion de CargarDatos de la funcion buscarSugeridos
        public DataTable dtSugeridosGral;

        DataTable dtSelectSugerido;

        string seleccionSugeridoNomb;

        int match = 0;
        string queryBuscarSugeridos, insertarNoMatch, queryBuscarSugeridosGral;
        int PuntajeMatch = 0;

        string FraseXML, FraseStock;
        string[] PalabrasXML, PalabrasStock;

        // variables para poder realizar el recorrido, calculo de valores etc
        int index;                              // sirve para saber que Row de la Tabla estamos y poder obtener el valode de alguna celda
        int cantProductos;                      // sirve para poder mostrar la cantidad de productos que tiene el Archivo XML
        int resultadoSearch;                    // sirve para ver si la consulta de buscar arroja alguna fila
        int resultadoSearchProd;                // srive para ver si el producto existe busca en los campos de CodigoBarras y ClaveInterna
        int resultadoCambioPrecio;              // sirve para ver si el usuario hizo alguna actualizacion en el precio
        int resultadoSearchNoIdentificacion;    // sirve para ver si el producto existe en los campos CodigoBarras y ClaveInterna en la funcion searchClavIntProd()
        int resultadoSearchCodBar;              // sirve para ver si el producto existe en los campos CodigoBarras y ClaveInterna en la funcion searchCodBar()
        int stockProd;                          // sirve para almacenar en ella, la cantidad de stock que tenemos de ese producto
        public static int stockProdXML;         // sirve para almacenar en ella, la cantidad del stock que nos llego en el archivo XML
        int totalProd;                          // sirve para en ella almacenar la suma del Stock del producto mas el stock del archivo XML

        // variables para poder hacer los calculos sobre el producto
        float importe;                          // convertimos el importe del Archivo XML para su posterior manipulacion
        float descuento;                        // convertimos el descuento del Archivo XML para su posterior manipulacion
        float cantidad;                         // convertimos el cantidad del Archivo XML para su posterior manipulacion
        float precioOriginalSinIVA;             // Calculamos el precio Original Sin IVA (importe - descuento)/cantidad
        float precioOriginalConIVA;             // Calculamos el precio Original Con IVA (precioOriginalSinIVA)*1.16
        public static float PrecioRecomendado;                // calculamos Precio Recomendado (precioOriginalConIVA)*1.60
        float importeReal;                      // calculamos importe real (cantidad * precioOriginalConIVA)
        float PrecioProd;                       // almacenamos el Precio del Producto en PrecioProd para su posterior manipulacion
        float PrecioProdToCompare;              // almacenamos el precio sugerido para hacer la comparacion

        DialogResult dialogResult;              // creamos el objeto para poder abrir el cuadro de dialogo

        int numFila;
        string fechaSitema, fechaSola, horaSola, fechaCompletaRelacionada;
        DateTime fechaHoy = DateTime.Now;
        string queryrelacionXMLTable;
        int seleccionarSugerido, seleccionarDefault;
        DataTable dtConfirmarProdRelXML;
        string idProdRelXML;
        DataTable dtUpdateConfirmarProdRelXML;
        string queryUpdateConfirmarProdRelXML;
        DataTable dtBarrExtSelectSugerido;

        // variables para poder tomar el valor de los TxtBox y tambien hacer las actualizaciones
        // del valor que proviene de la base de datos ó tambien actualizar la Base de Datos
        string id;
        string rfc;

        // objetos para poder tratar la informacion del XML
        XmlSerializer serial;
        FileStream fs;
        Comprobante ds;


        string IdProductoSugerido;          // Obtiene el ID del Producto sugerido al darle click en la lista
        string NombProductoSugerido;        // Obtiene el Nombre del Producto sugerido al darle click en la lista
        string StockProdSugerido;           // Obtiene el Stock del Producto sugerido al darle click en la lista
        string CoincidenciaSugerido;        // Obtiene las Coincidencias del Producto sugerido al darle click en la lista
        int totalProdSugerido;              // Se obtiene la cantidad de productos sugeridos
        int origenDeLosDatos = 0;

        private void ActivarBtnSi()
        {
            button2.Enabled = true;
        }

        private void DesactivarBtnSi()
        {
            button2.Enabled = false;
        }

        // funcion para poder asignar los datos del XML a la ventana de Nvo Producto
        public void datosAgregarNvoProd()
        {
            if (index == 0)
            {
                FormAgregar.ProdNombre = ds.Conceptos[index].Descripcion;                   // pasamos la descripcion
                FormAgregar.ProdStock = ds.Conceptos[index].Cantidad;                       // pasamos la cantidad del XML
                FormAgregar.ProdPrecio = PrecioRecomendado.ToString("N2");                      // pasamos el precio recomendado
                FormAgregar.txtPrecioCompra.Text = precioOriginalConIVA.ToString("N2");       // pasamos el precio origianl del XML
                FormAgregar.ProdClaveInterna = ds.Conceptos[index].NoIdentificacion;        // pasamos la claveInterna del XML

                FormAgregar.claveProductoxml = ds.Conceptos[index].ClaveProdServ;
                FormAgregar.claveUnidadMedidaxml = ds.Conceptos[index].ClaveUnidad;
                FormAgregar.fechaProdXML = ds.Fecha;
                FormAgregar.FolioProdXML = ds.Folio;
                FormAgregar.RFCProdXML = ds.Emisor.Rfc;
                FormAgregar.NobEmisorProdXML = ds.Emisor.Nombre;
                FormAgregar.ClaveProdEmisorProdXML = ds.Conceptos[index].ClaveProdServ;
                FormAgregar.DescuentoProdXML = ds.Conceptos[index].Descuento;
                FormAgregar.PrecioCompraXML = precioOriginalConIVA.ToString("N2");
            }
            else if (index >= 1)
            {
                FormAgregar.ProdNombre = ds.Conceptos[index - 1].Descripcion;                   // pasamos la descripcion
                FormAgregar.ProdStock = ds.Conceptos[index - 1].Cantidad;                       // pasamos la cantidad del XML
                FormAgregar.ProdPrecio = PrecioRecomendado.ToString("N2");                      // pasamos el precio recomendado
                FormAgregar.txtPrecioCompra.Text = precioOriginalConIVA.ToString("N2");       // pasamos el precio origianl del XML
                FormAgregar.ProdClaveInterna = ds.Conceptos[index - 1].NoIdentificacion;        // pasamos la claveInterna del XML
                
                FormAgregar.claveProductoxml = ds.Conceptos[index - 1].ClaveProdServ;
                FormAgregar.claveUnidadMedidaxml = ds.Conceptos[index - 1].ClaveUnidad;
                FormAgregar.fechaProdXML = ds.Fecha;
                FormAgregar.FolioProdXML = ds.Folio;
                FormAgregar.RFCProdXML = ds.Emisor.Rfc;
                FormAgregar.NobEmisorProdXML = ds.Emisor.Nombre;
                FormAgregar.ClaveProdEmisorProdXML = ds.Conceptos[index - 1].ClaveProdServ;
                FormAgregar.DescuentoProdXML = ds.Conceptos[index - 1].Descuento;
            }
            //Asignamos el impuesto y el importe
            /*var cadena = ObtenerImpuestos(rutaXML, index - 1);
            var tmp = cadena.Split('|');

            FormAgregar.impuestoProdXML = tmp[0];
            FormAgregar.importeProdXML = tmp[1];*/

        }

        private string ObtenerImpuestos(string archivo, int indice)
        {
            XDocument doc = XDocument.Load(archivo);

            List<string> tmp = new List<string>();

            foreach (var datos in doc.Descendants().Where(x => x.Name.LocalName == "Traslado"))
            {
                string impuesto = (string)datos.Attribute("Impuesto");
                string importe = (string)datos.Attribute("Importe");

                tmp.Add(impuesto + "|" + importe);
            }

            impuestosXML = tmp.ToArray();
            //Para eliminar el ultimo elemento del array
            impuestosXML = impuestosXML.Take(impuestosXML.Count() - 1).ToArray();

            return impuestosXML[indice];
        }

        // funcion para poder saber que cliente es el que esta iniciando sesion en el sistema
        public void cargarDatosXML()
        {
            index = 0;
            /****************************************************
            *   obtenemos los datos almacenados en el dt        *
            *   y se los asignamos a cada uno de los variables  *
            ****************************************************/
            id = dt.Rows[index]["ID"].ToString();
            rfc = dt.Rows[index]["RFC"].ToString();
        }

        // funcion para hacer la consulta del cliente que inicio sesion y esto
        // para posteriormente tener sus datos por separado
        public void consulta()
        {
            // String para hacer la consulta filtrada sobre
            // el usuario que inicia la sesion
            buscar = $"SELECT u.ID, u.Usuario, u.Password, u.RFC FROM Usuarios u WHERE u.Usuario = '{userName}' AND u.Password = '{passwordUser}'";

            // almacenamos el resultado de la Funcion CargarDatos
            // que esta en la calse Consultas
            dt = cn.CargarDatos(buscar);

            cargarDatosXML();   // metodo para cargar los datos del XML
        }

        // funcion para ocultar el panel en el que
        // esta el label que dice click cargar XML 
        // y el boton de XML
        public void OcultarPanelCarga()
        {
            panel1.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            origenDeLosDatos = 3;

            string querySearchProveedor = $@"SELECT * FROM Proveedores WHERE IDUsuario = '{FormPrincipal.userID}' AND Nombre = '{ds.Emisor.Nombre.Trim()}' AND RFC = '{ds.Emisor.Rfc.Trim()}'";
            DataTable dtSearchProveedor = cn.CargarDatos(querySearchProveedor);

            if (dtSearchProveedor.Rows.Count <= 0)
            {
                string fechaOperacion, fechaXML = ds.Fecha, fecha, hora;
                fecha = fechaXML.Substring(0,10);
                hora = fechaXML.Substring(11);
                fechaOperacion = fecha + " " + hora;
                string queryAddProveedor = $@"INSERT INTO Proveedores (IDUsuario, Nombre, RFC, FechaOperacion) VALUES ('{FormPrincipal.userID}', '{ds.Emisor.Nombre.Trim()}', '{ds.Emisor.Rfc.Trim()}', '{fechaOperacion}')";
                cn.EjecutarConsulta(queryAddProveedor);
            }

            FormAgregar.FormClosed += delegate
            {
                // recorrer el archivo XML
                //RecorrerXML(); 
                searchProd();
                if (resultadoSearchProd == 1)
                {
                    RecorrerXML();
                }
            };

            if (FormAgregar.Text == "")
            {
                FormAgregar = new AgregarEditarProducto("Agregar Producto");
            }

            if (!FormAgregar.Visible)
            {
                datosAgregarNvoProd();
                FormAgregar.DatosSource = origenDeLosDatos;
                FormAgregar.ShowDialog();
                origenDeLosDatos = 0;
            }
            else
            {
                datosAgregarNvoProd();
                FormAgregar.DatosSource = origenDeLosDatos;
                FormAgregar.BringToFront();
                origenDeLosDatos = 0;
            }
        }

        // funcion para Mostrar el panel en el que
        // esta el label que dice click cargar XML 
        // y el boton de XML
        public void MostrarPanelCarga()
        {
            panel1.Show();
            this.Size = new Size(500, 450);
            this.CenterToScreen();
        }

        // funcion para mostrar el panel:
        // No. 6, este se muestra solo si
        // es que no hay registro del Producto
        public void MostarPanelSinRegistro()
        {
            panel6.Show();
        }

        // funcion para ocultar los paneles:
        // 17, 2, 12 y el boton de XML esta hace
        // que se muestren los datos del XML
        public void OcultarPanelRegistro()
        {
            panel17.Hide();
            panel2.Hide();
            panel12.Hide();
            button1.Hide();
        }

        // funcion para ocultar el panel:
        // No. 6, este se muestra solo si
        // es que no hay registro del Producto
        public void OcultarPanelSinRegistro()
        {
            panel6.Hide();
        }

        // funcion para Muestra los paneles:
        // 17, 2, 12 y el boton de XML esta hace
        // que se muestren los datos del XML
        public void MostrarPanelRegistro()
        {
            panel17.Show();
            panel2.Show();
            panel12.Show();
            button1.Show();
            this.Size = new Size(950, 640);
            this.CenterToScreen();
        }

        // funcion para limpiar los datos que
        // provienen del archivo XML en los campos
        // que pretenecen al XML
        public void limpiarLblXNL()
        {
            lblDescripcionXML.Text = "";
            lblCantXML.Text = "";
            lblPrecioOriginalXML.Text = "";
            lblImpXML.Text = "";
            lblNoIdentificacionXML.Text = "";
            lblPrecioRecomendadoXML.Text = "";

            txtBoxDescripcionProd.Text = "";
            txtBoxClaveInternaProd.Text = "";
        }

        // funcion para limpiar los datos que
        // provienen del del Stock en los campos
        // que pretenecen al stock del producto
        public void limpiarLblProd()
        {
            lblStockProd.Text = "";
            lblCodigoBarrasProd.Text = "";
            lblPrecioRecomendadoProd.Text = "";
            txtBoxPrecioProd.Text = "";

            txtBoxDescripcionProd.Text = "";
            txtBoxClaveInternaProd.Text = "";
        }

        // metodo para buscar los sugeridos
        public void buscarSugeridos()
        {
            DataTable table = new DataTable();                              // creamos una tabla 

            DataColumn column;                                              // creamos una columna
            DataRow row;                                                    // creamos una fila
            DataView view;                                                  // creamos una vista

            List<DataGridViewRow> temp = new List<DataGridViewRow>();       // Lista Auxiliar para quitar con cero del DataGridView
            List<DataRow> listAux = new List<DataRow>();                    // Lista Auxiliar para borrar repetido de la Tabla

            string nombre = "", coincidencias = "";                         // variables para buscar nombre repetido o coincidencias en 0

            column = new DataColumn();                                      // creamos una nueva columna
            column.DataType = System.Type.GetType("System.Int32");          // declaramos el tipo
            column.ColumnName = "ID";                                       // ponemos el nombre
            table.Columns.Add(column);                                      // agregamos columna a la tabla

            column = new DataColumn();                                      // creamos una nueva columna
            column.DataType = System.Type.GetType("System.String");         // declaramos el tipo
            column.ColumnName = "Nombre";                                   // ponemos el nombre
            table.Columns.Add(column);                                      // agregamos columna a la tabla

            column = new DataColumn();                                      // creamos una nueva columna
            column.DataType = System.Type.GetType("System.String");         // declaramos el tipo
            column.ColumnName = "Existencia";                               // ponemos el nombre
            table.Columns.Add(column);                                      // agregamos columna a la tabla

            column = new DataColumn();                                      // creamos una nueva columna
            column.DataType = System.Type.GetType("System.Int32");          // declaramos el tipo
            column.ColumnName = "Coincidencias";                            // ponemos el nombre
            table.Columns.Add(column);                                      // agregamos columna a la tabla

            FraseXML = concepto;                    // almacena el nombre del concepto
            PalabrasXML = FraseXML.Split(' ');      // las separa en un array por palabras el concepto

            // hacemos la consulta de los productos segun el usuario
            queryBuscarSugeridos = $"SELECT prod.ID AS 'ID', prod.Nombre AS 'Nombre',  prod.Stock AS 'Existencia' FROM ProductoRelacionadoXML AS prxml LEFT JOIN Productos AS prod ON prod.ID = prxml.IDProducto LEFT JOIN CodigoBarrasExtras AS codext ON codext.IDProducto = prod.ID LEFT JOIN Usuarios AS usr ON prxml.IDUsuario = usr.ID WHERE prxml.NombreXML = '{concepto}' AND prxml.IDUsuario = '{userId}' AND prod.Status = '1'";
            dtSugeridos = cn.CargarDatos(queryBuscarSugeridos);             // realizamos la consulta a la Base de Datos
            dtSugeridos.Columns.Add("Coincidencias");                       // agregamos la columna de Coincidencias
            DGVSugeridos.DataSource = dtSugeridos;


            if (DGVSugeridos.Rows.Count == 0)       // si el DataGridView no tiene registros
            {
                // hacemos la consulta de los productos segun el usuario
                queryBuscarSugeridosGral = $"SELECT prod.ID AS 'ID', prod.Nombre AS 'Nombre', prod.Stock AS 'Existencia' FROM Productos AS prod WHERE prod.IDUsuario = '{userId}' AND prod.Status = '1'";
                dtSugeridosGral = cn.CargarDatos(queryBuscarSugeridosGral);     // realizamos la consulta a la Base de Datos
                dtSugeridosGral.Columns.Add("Coincidencias");                   // agregamos la columna de Coincidencias
                if (dtSugeridosGral.Rows.Count == 0)
                {
                    DGVSugeridos.Enabled = false;
                }
                else if (dtSugeridosGral.Rows.Count != 0)
                {
                    DGVSugeridos.Enabled = true;
                    DGVSugeridos.DataSource = dtSugeridosGral;                      // Llenamos de informacion el DataGridView
                    DGVSugeridos.Columns["ID"].Visible = false;                     // Columna 0 de ID la ocultamos para el usuario solamente
                    DGVSugeridos.Columns["Nombre"].Visible = true;                  // Columna 1 de Nombre la dejamos visible para el usuario
                    DGVSugeridos.Columns["Existencia"].Visible = false;             // Columna 2 de Existencia la ocultamos para el usuario solamente
                    DGVSugeridos.Columns["Coincidencias"].Visible = false;          // Columna 3 de Coincidencia la ocultamos para el usuario solamente

                    for (int Fila = 0; Fila < DGVSugeridos.Rows.Count; Fila++)  // hacemos el recorrido del DataGridView
                    {
                        FraseStock = DGVSugeridos.Rows[Fila].Cells["Nombre"].Value.ToString();  // almacenamos el nombre del concepto del DataGridView que son el Stock de Productos
                        PalabrasStock = FraseStock.Split(' ');                                  // separamos en un array palabra por palabra
                        foreach (var palabraSearch in PalabrasXML)      // hacemos el recorrido del contenido de las palabras de XML (Concepto)
                        {
                            foreach (var palabraFound in PalabrasStock)     // hacemos el recorrido del contenido de las palabras del Stock (Productos)
                            {
                                if (palabraFound.ToLower() == palabraSearch.ToLower())      // comparamos ambas palabras y si son iguales
                                {
                                    match++;    // solo para saber si que hay una coincidencia en palabras en el stock
                                    break;      // rompemos el ciclo para salir
                                }
                            }
                        }
                    }

                    match = 0;  // ponemos la variable del match en 0 
                    for (int fila = 0; fila < DGVSugeridos.Rows.Count; fila++)  // recorremos el DataGridView 
                    {
                        FraseStock = DGVSugeridos.Rows[fila].Cells["Nombre"].Value.ToString();  // almacenamos el nombre de la columna Nombre
                        PalabrasStock = FraseStock.Split(' ');                                  // lo separamos por palabras en un array
                        foreach (var palabraSearch in PalabrasXML)      // hacemos el recorrido del contenido de las palabras de XML (Concepto) 
                        {
                            foreach (var palabraFound in PalabrasStock)     // hacemos el recorrido del contenido de las palabras del Stock (Productos)
                            {
                                if (palabraFound.ToLower() == palabraSearch.ToLower())      // comparamos ambas palabras y si son iguales
                                {
                                    match++;                                                    // sumamos el valor del totalMatch con lo del match
                                }
                            }
                        }
                        DGVSugeridos.Rows[fila].Cells["Coincidencias"].Value = match;    // asignamos las coincidencias al campo de Coincidencias
                        match = 0;
                    }

                    coincidencias = "";                                                             // ponemos las coincidencias en vacio
                    foreach (DataGridViewRow renglon in DGVSugeridos.Rows)                          // hacemos el recorrido del DataGridView
                    {
                        coincidencias = ((DataGridViewTextBoxCell)renglon.Cells["Coincidencias"]).Value.ToString();     // Obtenemos el valor de la coincidencia
                        if (coincidencias.Equals("0"))                                                                  // Comparamos si es 0
                        {
                            temp.Add(renglon);                                                                              // la agregamos a lista Auxiliar
                        }
                    }
                    foreach (var renglon in temp)                                                   // hacemos el recorrido de la lista Auxiliar
                    {
                        DGVSugeridos.Rows.Remove(renglon);                                                              // eliminamos los registros del DataGridView
                    }

                    if (DGVSugeridos.Rows.Count != 0)
                    {
                        DGVSugeridos.Sort(DGVSugeridos.Columns["Coincidencias"], ListSortDirection.Descending);     // ordenamos desendentemente el DatGridView

                        DGVSugeridos.CurrentCell = DGVSugeridos.Rows[0].Cells[1];

                        fechaSitema = fechaHoy.ToString("s");
                        fechaSola = fechaSitema.Substring(0, 10);
                        horaSola = fechaSitema.Substring(11);
                        fechaCompletaRelacionada = fechaSola + " " + horaSola;

                        numFila = DGVSugeridos.CurrentRow.Index;
                        IdProductoSugerido = DGVSugeridos[0, numFila].Value.ToString();
                        NombProductoSugerido = DGVSugeridos[1, numFila].Value.ToString();
                        StockProdSugerido = DGVSugeridos[2, numFila].Value.ToString();
                        CoincidenciaSugerido = DGVSugeridos[3, numFila].Value.ToString();

                        queryrelacionXMLTable = $"SELECT * FROM ProductoRelacionadoXML WHERE NombreXML = '{concepto}'";
                        dtConfirmarProdRelXML = cn.CargarDatos(queryrelacionXMLTable);

                        if (dtConfirmarProdRelXML.Rows.Count != 0)
                        {
                            seleccionarDefault = 1;
                        }
                        else
                        {
                            seleccionarDefault = 0;
                        }
                    }
                }
            } 
            else if (DGVSugeridos.Rows.Count != 0)      // si el DataGrdiView si tiene registros
            {
                for (int i = 0; i < dtSugeridos.Rows.Count; i++)                        // recorremos el DataGridView
                {
                    row = table.NewRow();                                                       // a la tabla le agregamos una nueva fila
                    row["ID"] = Convert.ToInt32(dtSugeridos.Rows[i]["ID"].ToString());          // agregamos la celda de la columna ID
                    row["Nombre"] = dtSugeridos.Rows[i]["Nombre"].ToString();                   // agregamos la celda de la columna Nombre 
                    row["Existencia"] = dtSugeridos.Rows[i]["Existencia"].ToString();           // agregamos la celda de la columna Existencia
                    row["Coincidencias"] = Convert.ToInt32("999");                              // agregamos la celda de la columna Coincidencia
                    table.Rows.Add(row);                                                        // agregamos a la tabla la nueva fila
                }

                // hacemos la consulta de los productos segun el usuario
                queryBuscarSugeridosGral = $"SELECT prod.ID AS 'ID', prod.Nombre AS 'Nombre', prod.Stock AS 'Existencia' FROM Productos AS prod WHERE prod.IDUsuario = '{userId}' AND prod.Status = '1'";
                dtSugeridosGral = cn.CargarDatos(queryBuscarSugeridosGral);     // realizamos la consulta a la Base de Datos
                dtSugeridosGral.Columns.Add("Coincidencias");                       // agregamos la columna de Coincidencias

                foreach (DataRow dr in dtSugeridosGral.Rows)                            // recorremos el DataTable dtSugeridosGral
                {
                    nombre = DGVSugeridos.Rows[0].Cells["Nombre"].Value.ToString();             // tomamos el valor del campo nombre
                    if (dr["Nombre"].ToString() == nombre)                                      // comparamos si es igual al que ya estaba al inicio
                    {
                        listAux.Add(dr);                                                                // agregamos a lista Auxiliar
                    }
                }

                foreach (DataRow dr in listAux)                                         // hacemos el recorrido de la lista Auxiliar
                {
                    dtSugeridosGral.Rows.Remove(dr);                                            // eliminamos del DataTable el repetido en Nombre
                }

                for (int i = 0; i < dtSugeridosGral.Rows.Count; i++)                        // recorremos el dtSugerenciaGral
                {
                    row = table.NewRow();                                                           // a la tabla le agregamos una nueva fila  
                    row["ID"] = Convert.ToInt32(dtSugeridosGral.Rows[i]["ID"].ToString());          // agregamos la celda de la columna ID
                    row["Nombre"] = dtSugeridosGral.Rows[i]["Nombre"].ToString();                   // agregamos la celda de la columna Nombre 
                    row["Existencia"] = dtSugeridosGral.Rows[i]["Existencia"].ToString();           // agregamos la celda de la columna Existencia
                    row["Coincidencias"] = Convert.ToInt32("0");                                    // agregamos la celda de la columna Coincidencia
                    table.Rows.Add(row);                                                            // agregamos a la tabla la nueva fila
                }

                view = new DataView(table);                                     // agregamos la tabla a una vista
                DGVSugeridos.DataSource = view;                                 // llenamos el DataGridView con informacion

                DGVSugeridos.Columns["ID"].Visible = false;                     // Columna 0 de ID la ocultamos para el usuario solamente
                DGVSugeridos.Columns["Nombre"].Visible = true;                  // Columna 1 de Nombre la dejamos visible para el usuario
                DGVSugeridos.Columns["Existencia"].Visible = false;             // Columna 2 de Existencia la ocultamos para el usuario solamente
                DGVSugeridos.Columns["Coincidencias"].Visible = false;          // Columna 3 de Coincidencia la ocultamos para el usuario solamente
                
                match = 0;  // ponemos la variable del match en 0 
                for (int fila = 1; fila < DGVSugeridos.Rows.Count; fila++)  // recorremos el DataGridView 
                {
                    FraseStock = DGVSugeridos.Rows[fila].Cells["Nombre"].Value.ToString();  // almacenamos el nombre de la columna Nombre
                    PalabrasStock = FraseStock.Split(' ');                                  // lo separamos por palabras en un array
                    foreach (var palabraSearch in PalabrasXML)      // hacemos el recorrido del contenido de las palabras de XML (Concepto) 
                    {
                        foreach (var palabraFound in PalabrasStock)     // hacemos el recorrido del contenido de las palabras del Stock (Productos)
                        {
                            if (palabraFound.ToLower() == palabraSearch.ToLower())      // comparamos ambas palabras y si son iguales
                            {
                                match++;                                                    // sumamos el valor del totalMatch con lo del match
                            }
                        }
                    }
                    DGVSugeridos.Rows[fila].Cells["Coincidencias"].Value = match;    // asignamos las coincidencias al campo de Coincidencias
                    match = 0;
                }

                coincidencias = "";                                                             // ponemos las coincidencias en vacio
                foreach (DataGridViewRow renglon in DGVSugeridos.Rows)                          // hacemos el recorrido del DataGridView
                {
                    coincidencias = ((DataGridViewTextBoxCell)renglon.Cells["Coincidencias"]).Value.ToString();         // Obtenemos el valor de la coincidencia
                    if (coincidencias.Equals("0"))                                                                      // Comparamos si es 0 
                    {
                        temp.Add(renglon);                                                                                      // la agregamos a lista Auxiliar
                    }
                }
                foreach (var renglon in temp)                                                   // hacemos el recorrido de la lista Auxiliar 
                {
                    DGVSugeridos.Rows.Remove(renglon);                                                  // eliminamos del DataTable el repetido en Nombre
                }

                DGVSugeridos.Sort(DGVSugeridos.Columns["Coincidencias"], ListSortDirection.Descending);          // ordenamos Acendentemente el DatGridView

                DGVSugeridos.CurrentCell = DGVSugeridos.Rows[0].Cells[1];

                fechaSitema = fechaHoy.ToString("s");
                fechaSola = fechaSitema.Substring(0, 10);
                horaSola = fechaSitema.Substring(11);
                fechaCompletaRelacionada = fechaSola + " " + horaSola;

                numFila = DGVSugeridos.CurrentRow.Index;
                IdProductoSugerido = DGVSugeridos[0, numFila].Value.ToString();
                NombProductoSugerido = DGVSugeridos[1, numFila].Value.ToString();
                StockProdSugerido = DGVSugeridos[2, numFila].Value.ToString();
                CoincidenciaSugerido = DGVSugeridos[3, numFila].Value.ToString();
                
                queryrelacionXMLTable = $"SELECT * FROM ProductoRelacionadoXML WHERE NombreXML = '{concepto}'";
                dtConfirmarProdRelXML = cn.CargarDatos(queryrelacionXMLTable);
                if (dtConfirmarProdRelXML.Rows.Count != 0)
                {
                    seleccionarDefault = 1;
                }
                else
                {
                    seleccionarDefault = 0;
                }
            }
        }

        // funsion para poder buscar los productos 
        // que coincidan con los campos de de ClaveInterna o el CodigoBarras
        // respecto al archivo XML en su campo de NoIdentificacion
        public void searchProd()
        {
            // preparamos el Query
            string search = $"SELECT Prod.ID, Prod.Nombre, Prod.Stock, Prod.ClaveInterna, Prod.CodigoBarras, Prod.Precio, Prod.Tipo, Prod.Status, codbarext.CodigoBarraExtra, codbarext.IDProducto FROM Productos Prod LEFT JOIN CodigoBarrasExtras codbarext ON codbarext.IDProducto = prod.ID WHERE Prod.IDUsuario = '{userId}' AND Prod.ClaveInterna = '{ClaveInterna}' OR Prod.CodigoBarras = '{ClaveInterna}' OR codbarext.CodigoBarraExtra = '{ClaveInterna}'";
            dtProductos = cn.CargarDatos(search); // alamcenamos el resultado de la busqueda en dtProductos
            if (dtProductos.Rows.Count >= 1) // si el resultado arroja al menos una fila
            {
                if (dtProductos.Rows[0]["Status"].ToString() == "1")
                {
                    resultadoSearchProd = 1;                                            // busqueda positiva
                    NoClaveInterna = dtProductos.Rows[0]["ClaveInterna"].ToString();    // almacenamos el valor del NoClaveInterna
                    NoCodBar = dtProductos.Rows[0]["CodigoBarras"].ToString();          // almacenamos el valor del NoCodbar
                    NoCodBarExt = dtProductos.Rows[0]["CodigoBarraExtra"].ToString();   // almacenamos el valor del NoCodBarExt
                    if (NoClaveInterna == ClaveInterna)
                    {
                        datosProductos();                                                   // llamamos la funcion de datosProductos
                        OcultarPanelSinRegistro();                                          // si es que hay registro ocultamos el panel sin registro
                        ActivarBtnSi();
                    }
                    else if (NoCodBar == ClaveInterna)
                    {
                        datosProductos();                                                   // llamamos la funcion de datosProductos
                        OcultarPanelSinRegistro();                                          // si es que hay registro ocultamos el panel sin registro
                        ActivarBtnSi();
                    }
                    else if (NoCodBarExt == ClaveInterna)
                    {
                        datosProductos();                                                   // llamamos la funcion de datosProductos
                        OcultarPanelSinRegistro();                                          // si es que hay registro ocultamos el panel sin registro
                        ActivarBtnSi();
                    }
                    else
                    {
                        limpiarLblProd();               // limpiamos los campos de producto
                        MostarPanelSinRegistro();       // si es que no hay registro muestra este panel
                        buscarSugeridos();
                        DesactivarBtnSi();
                    }
                }
                else if (dtProductos.Rows[0]["Status"].ToString() == "0")
                {
                    resultadoSearchProd = 0;        // busqueda negativa
                    DesactivarBtnSi();
                    RecorrerXML();
                }
                //MessageBox.Show("Producto Encontrado", "El Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtProductos.Rows.Count <= 0) // si el resultado no arroja ninguna fila
            {
                resultadoSearchProd = 0;        // busqueda negativa
                limpiarLblProd();               // limpiamos los campos de producto
                MostarPanelSinRegistro();       // si es que no hay registro muestra este panel
                buscarSugeridos();
                DesactivarBtnSi();
            }
        }

        // funsion para poder buscar en los productos 
        // si coincide con los campos de de ClaveInterna
        // respecto al stock del producto en su campo de NoIdentificacion
        public void searchClavIntProd()
        {
            // preparamos el Query
            string search = $"SELECT Prod.ID, Prod.Nombre, Prod.ClaveInterna, Prod.Stock, Prod.CodigoBarras, Prod.Precio FROM Productos Prod LEFT JOIN CodigoBarrasExtras codbarext ON codbarext.IDProducto = prod.ID WHERE Prod.IDUsuario = '{userId}' AND Prod.ClaveInterna = '{ClaveInterna}' OR Prod.CodigoBarras = '{ClaveInterna}' OR codbarext.CodigoBarraExtra = '{ClaveInterna}'";
            dtClaveInterna = cn.CargarDatos(search);    // alamcenamos el resultado de la busqueda en dtClaveInterna
            if (dtClaveInterna.Rows.Count > 0)          // si el resultado arroja al menos una fila
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
            string search = $"SELECT Prod.ID, Prod.Nombre, Prod.ClaveInterna, Prod.Stock, Prod.CodigoBarras, Prod.Precio FROM Productos Prod LEFT JOIN CodigoBarrasExtras codbarext ON codbarext.IDProducto = prod.ID WHERE Prod.IDUsuario = '{userId}' AND Prod.ClaveInterna = '{ClaveInterna}' OR Prod.CodigoBarras = '{ClaveInterna}' OR codbarext.CodigoBarraExtra = '{ClaveInterna}'";
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

        // funsion para cargar los datos XML
        // que provienen del archivo XML
        public void datosXML()
        {
            descuento = 0;
            ClaveInterna = "0";
            lblPosicionActualXML.Text = (index + 1).ToString();
            if (index > 0)
            {
                index = Convert.ToInt32(lblPosicionActualXML.Text);
            }
            if (index == 0)
            {
                concepto = ds.Conceptos[index].Descripcion;
            }
            else if (index >= 1)
            {
                concepto = ds.Conceptos[index - 1].Descripcion;
            }
            lblDescripcionXML.Text = concepto;
            try
            {
                // convertimos la cantidad del Archivo XML para su posterior manipulacion
                if (index == 0)
                {
                    stockProdXML = (int)Convert.ToDouble(ds.Conceptos[index].Cantidad);
                }
                else if (index >= 1)
                {
                    stockProdXML = (int)Convert.ToDouble(ds.Conceptos[index - 1].Cantidad);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            lblCantXML.Text = stockProdXML.ToString();
            try
            {
                // convertimos el Importe del Archivo XML para su posterior manipulacion
                if (index == 0)
                {
                    importe = (float)Convert.ToDouble(ds.Conceptos[index].Importe);
                }
                else if (index >= 1)
                {
                    importe = (float)Convert.ToDouble(ds.Conceptos[index - 1].Importe);
                }     
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (index == 0)
            {
                if (ds.Conceptos[index].Descuento == "" || ds.Conceptos[index].Descuento == null)
                {
                    ds.Conceptos[index].Descuento = descuento.ToString();
                }
                else if (ds.Conceptos[index].Descuento != "")
                {
                    try
                    {
                        descuento = (float)Convert.ToDouble(ds.Conceptos[index].Descuento);         // convertimos el Descuento del Archivo XML para su posterior manipulacion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (index >= 1)
            {
                if (ds.Conceptos[index - 1].Descuento == "" || ds.Conceptos[index - 1].Descuento == null)
                {
                    ds.Conceptos[index - 1].Descuento = descuento.ToString();
                }
                else if (ds.Conceptos[index - 1].Descuento != "")
                {
                    try
                    {
                        descuento = (float)Convert.ToDouble(ds.Conceptos[index - 1].Descuento);         // convertimos el Descuento del Archivo XML para su posterior manipulacion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            try
            {
                // convertimos la cantidad del Archivo XML para su posterior manipulacion
                if (index == 0)
                {
                    cantidad = (float)Convert.ToDouble(ds.Conceptos[index].Cantidad);
                }
                else if (index >= 1)
                {
                    cantidad = (float)Convert.ToDouble(ds.Conceptos[index - 1].Cantidad);
                }            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            precioOriginalSinIVA = (importe - descuento) / cantidad;                        // Calculamos el precio Original Sin IVA (importe - descuento)/cantidad
            precioOriginalConIVA = precioOriginalSinIVA * (float)1.16;                      // Calculamos el precio Original Con IVA (precioOriginalSinIVA)*1.16
            lblPrecioOriginalXML.Text = precioOriginalConIVA.ToString("N2");
            importeReal = cantidad * precioOriginalConIVA;                                  // calculamos importe real (cantidad * precioOriginalConIVA)
            lblImpXML.Text = importeReal.ToString("N2");
            if (index == 0)
            {
                if (ds.Conceptos[index].NoIdentificacion == "" || ds.Conceptos[index].NoIdentificacion == null)
                {
                    ds.Conceptos[index].NoIdentificacion = ClaveInterna;
                }
                else if (ds.Conceptos[index].NoIdentificacion != "")
                {
                    ClaveInterna = ds.Conceptos[index].NoIdentificacion;
                }
            }
            else if (index >= 1)
            {
                if (ds.Conceptos[index - 1].NoIdentificacion == "" || ds.Conceptos[index - 1].NoIdentificacion == null)
                {
                    ds.Conceptos[index - 1].NoIdentificacion = ClaveInterna;
                }
                else if (ds.Conceptos[index - 1].NoIdentificacion != "")
                {
                    ClaveInterna = ds.Conceptos[index - 1].NoIdentificacion;
                }
            }
            lblNoIdentificacionXML.Text = ClaveInterna;
            PrecioRecomendado = precioOriginalConIVA * (float)1.60;             // calculamos Precio Recomendado (precioOriginalConIVA)*1.60
            lblPrecioRecomendadoXML.Text = PrecioRecomendado.ToString("N2");
            try
            {
                if (index == 0)
                {
                    precio = (float)Convert.ToDouble(ds.Conceptos[index].ValorUnitario);
                }
                else if (index >= 1)
                {
                    precio = (float)Convert.ToDouble(ds.Conceptos[index - 1].ValorUnitario);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            fechaXML = ds.Fecha;
            fecha = fechaXML.Substring(0, found);
            hora = fechaXML.Substring(found+1);
            fechaCompleta = fecha + " " + hora;
            folio = ds.Folio;
            RFCEmisor = ds.Emisor.Rfc;
            nombreEmisor = ds.Emisor.Nombre;
            if (index == 0)
            {
                claveProdEmisor = ds.Conceptos[index].ClaveProdServ;
            }
            else if (index >= 1)
            {
                claveProdEmisor = ds.Conceptos[index - 1].ClaveProdServ;
            }
        }

        // funsion para cargar los datos del Producto
        // que provienen del stock del producto
        public void datosProductos()
        {
            idProducto = dtProductos.Rows[0]["ID"].ToString();
            txtBoxDescripcionProd.Text = dtProductos.Rows[0]["Nombre"].ToString();
            txtBoxClaveInternaProd.Text = dtProductos.Rows[0]["ClaveInterna"].ToString();
            stockProd = int.Parse(dtProductos.Rows[0]["Stock"].ToString());                 // almacenamos el Stock del Producto en stockProd para su posterior manipulacion
            lblStockProd.Text = stockProd.ToString();
            lblCodigoBarrasProd.Text = dtProductos.Rows[0]["CodigoBarras"].ToString();
            lblPrecioRecomendadoProd.Text = lblPrecioRecomendadoXML.Text;
            PrecioProd = float.Parse(dtProductos.Rows[0]["Precio"].ToString());             // almacenamos el Precio del Producto en PrecioProd para su posterior manipulacion
            txtBoxPrecioProd.Text = PrecioProd.ToString("N2");
        }

        // funsion para hacer la lectura del Archivo XML 
        public void RecorrerXML()
        {
            int totalRegistroXML;                           // variable para saber cuantos concepctos tiene el XML
            totalRegistroXML = ds.Conceptos.Count();        // almacenamos el total de conceptos del XML
            index = int.Parse(lblPosicionActualXML.Text);   // alamcenamos el valor de la etiqueta lblPosicionActuakXML nos ayudara para saber en que concepto va del XML
            if (index == totalRegistroXML)                  // comparamos la posicion actual y vemo si es igual al total de conceptos del XML
            {
                //MessageBox.Show("Final del Archivo XML,\nrecorrido con exito", "Archivo XML recorrido en totalidad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();       // cerramos la ventana de Actualizar los datos
            }
            else if (lblPosicionActualXML.Text == "0")      // comparamos si la etiqueta lblPosicionActualXML es igual al número 0
            {
                datosXML();         // llamamos la funsion datosXML para llenar datos que tiene el XML
                searchProd();       // buscamos el producto a ver si ya esta en el Stock
                if (resultadoSearch <= 0)                   // si el resultado es negativo
                {
                   // ToDo si es que no hay producto registrado
                }
                else if (resultadoSearch >= 1)              // si el resultado es positivo
                {
                    datosProductos();       // mostramos los datos del producto en el stock
                }
                //index++;            // aumentamos en uno la variable index
            }
            else if(index <= ds.Conceptos.Count())
            {
                datosXML();         // llamamos la funsion datosXML para llenar datos que tiene el XML
                searchProd();       // buscamos el producto a ver si ya esta en el Stock
                if (resultadoSearch <= 0)               // si el resultado es negativo
                {
                    // ToDo si es que no hay producto registrado
                }
                else if (resultadoSearch >= 1)          // si el resultado es positivo
                {
                    datosProductos();       // mostramos los datos del producto en el stock
                }
                //index++;            // aumentamos en uno la variable index
            }
        }

        // funsion para comprobar que el precio del stock sea
        // mayor o igual al que esta sugerido por el XML
        public void comprobarPrecioMayorIgualRecomendado()
        {
            if (PrecioProd >= 0)
            {
                if (txtBoxPrecioProd.Text == "")
                {
                    //PrecioProd = 0;
                }
                else
                {
                    PrecioProd = float.Parse(txtBoxPrecioProd.Text);                    // almacenamos el precio que tiene la caja de texto
                }
            }
            if (PrecioProdToCompare > 1)
            {
                PrecioProdToCompare = float.Parse(lblPrecioRecomendadoProd.Text);   // almacenamos el precio sugerido para hacer la comparacion
            }
            if (PrecioProd < PrecioProdToCompare)                               // comparamos si el precio es menor
            {
                // muestra un mensaje que el precio en stock es menor que el suegerido
                dialogResult = MessageBox.Show("El Precio del producto en el Stock,\nes Menor que el precio Recomendado\n\n\tDESEA CORREGIRLA...", "El precio del Producto es menor...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)       // si el usuario selecciona que si quiere modificarlo
                {
                    resultadoCambioPrecio = 1;                                  // la variable la ponemos en valor 1 indicando que se hizo el cambio de precio
                    txtBoxPrecioProd.Text = PrecioRecomendado.ToString("N2");     // ponemos en el TxtBox el contenido PrecioProdToCompare
                    txtBoxPrecioProd.Focus();                                   // mandamos el cursor al textBox para el cambio de precio del Stock
                    PrecioProd = float.Parse(txtBoxPrecioProd.Text);            // actualizamos el precio de la caja de texto
                }
                else if (dialogResult == DialogResult.No)           // si el usario selecciona que no quiere modificarlo
                {
                    resultadoCambioPrecio = 0;      // la variable la ponemos en valor 0 indicando que no se hizo el cambio de precio
                }
            }
        }

        // funsion para ver cuanto aumentario el stock 
        public void verNvoStock()
        {
            totalProd = stockProd + stockProdXML;       // realizamos el caulculo del nvo stock 
            lblStockProd.Text = totalProd.ToString();   // mostramos el nvo stock del producto
        }

        // funcion para actualizar el Stock
        public void ActualizarStock()
        {
            int resultadoConsulta;                              // almacenamos el resultado sea 1 o 0
            NombreProd = txtBoxDescripcionProd.Text;            // almacenamos el contenido del TextBox
            if (dtProductos.Rows[0]["Tipo"].ToString() == "P")
            {
                // hacemos el query para la actualizacion del Stock
                query = $"UPDATE Productos SET Nombre = '{NombreProd}', Stock = '{totalProd}', ClaveInterna = '{textBoxNoIdentificacion}', Precio = '{PrecioProd}' WHERE ID = '{idProducto}'";
                resultadoConsulta = cn.EjecutarConsulta(query);     // aqui vemos el resultado de la consulta
                if (resultadoConsulta == 1)                         // si el resultado es 1
                {
                    //MessageBox.Show("Se Acualizo el producto","Estado de Actualizacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else                                                // si el resultado es 0
                {
                    //MessageBox.Show("se actualizo mas" + resultadoConsulta);
                }
            }
            else if (dtProductos.Rows[0]["Tipo"].ToString() == "S")
            {
                query = $"SELECT usr.NombreCompleto AS 'Usuario', prod.ID AS 'Producto', prod.Nombre AS 'Nombre', prod.Stock AS 'Existencia', prod.Precio AS 'Precio', prod.Categoria AS 'Categoria', prod.ClaveInterna AS 'Calve Interna', prod.CodigoBarras AS 'Codigo  de Barra', prod.Tipo AS 'Producto o Servicio', codBarExt.CodigoBarraExtra AS 'Codigo de Barras Extra', prodOfServ.IDServicio AS 'No de Servicio', prodOfServ.IDProducto AS 'No de Producto', prodOfServ.Cantidad AS 'Cantidad', prodOfServ.NombreProducto AS 'Producto Incluido' FROM Usuarios AS usr LEFT JOIN Productos AS prod ON prod.IDUsuario = usr.ID LEFT JOIN CodigoBarrasExtras AS codBarExt ON codBarExt.IDProducto = prod.ID LEFT JOIN ProductosDeServicios AS prodOfServ ON prodOfServ.IDServicio = prod.ID WHERE usr.ID = '{userId}' AND prod.ClaveInterna = '{ClaveInterna}' OR prod.CodigoBarras = '{ClaveInterna}' OR codBarExt.CodigoBarraExtra = '{ClaveInterna}'";
                DataTable resultadoServicio = cn.CargarDatos(query);
                int nvoStock, oldStock, stockService, numProd, numServicio;
                numServicio = Convert.ToInt32(resultadoServicio.Rows[0]["Producto"].ToString());
                string queryConsultaSinProductos = $"SELECT usr.NombreCompleto AS 'Usuario', prod.ID AS 'Producto', prod.Nombre AS 'Nombre',  prod.Stock AS 'Existencia', prod.Precio AS 'Precio', prod.Categoria AS 'Categoria', prod.ClaveInterna AS 'Calve Interna', prod.CodigoBarras AS 'Codigo  de Barra', prod.Tipo AS 'Producto o Servicio', prodOfServ.IDServicio AS 'No de Servicio', prodOfServ.IDProducto AS 'No de Producto', prodOfServ.Cantidad AS 'Cantidad', prodOfServ.NombreProducto AS 'Producto Incluido' FROM Usuarios AS usr LEFT JOIN Productos AS prod ON prod.IDUsuario = usr.ID LEFT JOIN ProductosDeServicios AS prodOfServ ON prodOfServ.IDServicio = prod.ID WHERE usr.ID = '{userId}' AND prodOfServ.IDServicio = '{numServicio}'";
                DataTable resultadoSinProducto = cn.CargarDatos(queryConsultaSinProductos);
                if (resultadoSinProducto.Rows.Count == 0)
                {
                    // si hay algo por hacer para solo los paquetes 
                    // sin producto relacionado
                }
                else if (resultadoSinProducto.Rows.Count > 0)
                {
                    numProd = Convert.ToInt32(resultadoServicio.Rows[0]["No de Producto"].ToString());
                    string searchProducto = $"SELECT * FROM Productos WHERE ID = '{numProd}'";
                    DataTable resultadoBuscarProd = cn.CargarDatos(searchProducto);
                    oldStock = Convert.ToInt32(resultadoBuscarProd.Rows[0]["Stock"].ToString());
                    stockService = Convert.ToInt32(resultadoServicio.Rows[0]["Cantidad"].ToString()) * stockProdXML;
                    nvoStock = oldStock + stockService;
                    string queryUpDateServicio = $"UPDATE Productos SET Stock = '{nvoStock}' WHERE ID = '{numProd}'";
                    resultadoConsulta = cn.EjecutarConsulta(queryUpDateServicio);     // aqui vemos el resultado de la consulta
                    if (resultadoConsulta == 1)                         // si el resultado es 1
                    {
                        //MessageBox.Show("Se Acualizo el producto","Estado de Actualizacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else                                                // si el resultado es 0
                    {
                        //MessageBox.Show("se actualizo mas" + resultadoConsulta);
                    }
                }
            }

            //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}', datetime('now', 'localtime'), '{Inventario.idReporte}', '{idProducto}','{userId}')";

            query = $@"INSERT INTO 
	                        HistorialCompras(Concepto,
		                    Cantidad,
		                    ValorUnitario,
		                    Descuento,
		                    Precio,
		                    FechaLarga,
		                    Folio,
		                    RFCEmisor,
		                    NomEmisor,
		                    ClaveProdEmisor,
                            FechaOperacion, 
                            IDReporte,
		                    IDProducto,
		                    IDUsuario) 
                     VALUES('{concepto}',
	                        '{cantidad}',
	                        '{PrecioProd}',
	                        '{descuento}',
	                        '{precioOriginalConIVA.ToString("N2")}',
	                        '{fechaCompleta}',
	                        '{folio}',
	                        '{RFCEmisor}',
	                        '{nombreEmisor}',
	                        '{claveProdEmisor}',
                            datetime('now', 'localtime'), 
                            '{Inventario.idReporte}',
	                        '{idProducto}',
	                        '{userId}')";
            try
            {
                cn.EjecutarConsulta(query);
                idRecordProd = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM HistorialCompras ORDER BY ID DESC LIMIT 1", 1));
                //MessageBox.Show("Registrado Intento 1", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }
            date1 = DateTime.Now;
            fechaCompleta = date1.ToString("s");
            Year = fechaCompleta.Substring(0, found);
            Date = fechaCompleta.Substring(found + 1);
            FechaRegistrada = Year + " " + Date;
            queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{userId}','{idRecordProd}','{FechaRegistrada}')";
            cn.EjecutarConsulta(queryRecordHistorialProd);
        }

        public void RelacionarStockClaveInterna()
        {
            query = $"UPDATE Productos SET Nombre = '{NombreProd}', Stock = '{totalProd}', ClaveInterna = '{txtBoxClaveInternaProd.Text}', Precio = '{PrecioProd}' WHERE ID = '{idListProd}'";
            resultadoConsulta = cn.EjecutarConsulta(query);     // aqui vemos el resultado de la consulta

            //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}', datetime('now', 'localtime'), '{Inventario.idReporte}', '{idProducto}','{userId}')";

            //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor,IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}','{idProducto}','{userId}')";
            query = $@"INSERT INTO 
	                        HistorialCompras(Concepto,
		                    Cantidad,
		                    ValorUnitario,
		                    Descuento,
		                    Precio,
		                    FechaLarga,
		                    Folio,
		                    RFCEmisor,
		                    NomEmisor,
		                    ClaveProdEmisor,
                            FechaOperacion, 
                            IDReporte,
		                    IDProducto,
		                    IDUsuario) 
                     VALUES('{concepto}',
	                        '{cantidad}',
	                        '{PrecioProd}',
	                        '{descuento}',
	                        '{precioOriginalConIVA.ToString("N2")}',
	                        '{fechaCompleta}',
	                        '{folio}',
	                        '{RFCEmisor}',
	                        '{nombreEmisor}',
	                        '{claveProdEmisor}',
                            datetime('now', 'localtime'), 
                            '{Inventario.idReporte}',
	                        '{idProducto}',
	                        '{userId}')";
            try
            {
                cn.EjecutarConsulta(query);
                idRecordProd = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM HistorialCompras ORDER BY ID DESC LIMIT 1", 1));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }
            date1 = DateTime.Now;
            fechaCompleta = date1.ToString("s");
            Year = fechaCompleta.Substring(0, found);
            Date = fechaCompleta.Substring(found + 1);
            FechaRegistrada = Year + " " + Date;
            queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{userId}','{idRecordProd}','{FechaRegistrada}')";
            cn.EjecutarConsulta(queryRecordHistorialProd);
        }

        public void RelacionarStockCodigoBarras()
        {
            query = $"UPDATE Productos SET Nombre = '{NombreProd}', Stock = '{totalProd}', CodigoBarras = '{lblCodigoBarrasProd.Text}', Precio = '{PrecioProd}' WHERE ID = '{idListProd}'";
            resultadoConsulta = cn.EjecutarConsulta(query);     // aqui vemos el resultado de la consulta

            //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}', datetime('now', 'localtime'), '{Inventario.idReporte}', '{idProducto}','{userId}')";

            //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor,IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}','{idProducto}','{userId}')";
            query = $@"INSERT INTO 
	                        HistorialCompras(Concepto,
		                    Cantidad,
		                    ValorUnitario,
		                    Descuento,
		                    Precio,
		                    FechaLarga,
		                    Folio,
		                    RFCEmisor,
		                    NomEmisor,
		                    ClaveProdEmisor,
                            FechaOperacion, 
                            IDReporte,
		                    IDProducto,
		                    IDUsuario) 
                     VALUES('{concepto}',
	                        '{cantidad}',
	                        '{PrecioProd}',
	                        '{descuento}',
	                        '{precioOriginalConIVA.ToString("N2")}',
	                        '{fechaCompleta}',
	                        '{folio}',
	                        '{RFCEmisor}',
	                        '{nombreEmisor}',
	                        '{claveProdEmisor}',
                            datetime('now', 'localtime'), 
                            '{Inventario.idReporte}',
	                        '{idProducto}',
	                        '{userId}')";
            try
            {
                cn.EjecutarConsulta(query);
                idRecordProd = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM HistorialCompras ORDER BY ID DESC LIMIT 1", 1));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }
            date1 = DateTime.Now;
            fechaCompleta = date1.ToString("s");
            Year = fechaCompleta.Substring(0, found);
            Date = fechaCompleta.Substring(found + 1);
            FechaRegistrada = Year + " " + Date;
            queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{userId}','{idRecordProd}','{FechaRegistrada}')";
            cn.EjecutarConsulta(queryRecordHistorialProd);
        }

        public void CodigoBarrasExtras()
        {
            query = $"INSERT INTO CodigoBarrasExtras(CodigoBarraExtra, IDProducto) VALUES('{ClaveInterna}','{idListProd}')";
            cn.EjecutarConsulta(query);

            //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}', datetime('now', 'localtime'), '{Inventario.idReporte}', '{idProducto}','{userId}')";

            //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor,IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompleta}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}','{idProducto}','{userId}')";
            query = $@"INSERT INTO 
	                        HistorialCompras(Concepto,
		                    Cantidad,
		                    ValorUnitario,
		                    Descuento,
		                    Precio,
		                    FechaLarga,
		                    Folio,
		                    RFCEmisor,
		                    NomEmisor,
		                    ClaveProdEmisor,
                            FechaOperacion, 
                            IDReporte,
		                    IDProducto,
		                    IDUsuario) 
                     VALUES('{concepto}',
	                        '{cantidad}',
	                        '{PrecioProd}',
	                        '{descuento}',
	                        '{precioOriginalConIVA.ToString("N2")}',
	                        '{fechaCompleta}',
	                        '{folio}',
	                        '{RFCEmisor}',
	                        '{nombreEmisor}',
	                        '{claveProdEmisor}',
                            datetime('now', 'localtime'), 
                            '{Inventario.idReporte}',
	                        '{idProducto}',
	                        '{userId}')";
            try
            {
                cn.EjecutarConsulta(query);
                idRecordProd = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM HistorialCompras ORDER BY ID DESC LIMIT 1", 1));
                //MessageBox.Show("Registrado Intento 3", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex);
            }
            date1 = DateTime.Now;
            fechaCompleta = date1.ToString("s");
            Year = fechaCompleta.Substring(0, found);
            Date = fechaCompleta.Substring(found + 1);
            FechaRegistrada = Year + " " + Date;
            queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{userId}','{idRecordProd}','{FechaRegistrada}')";
            cn.EjecutarConsulta(queryRecordHistorialProd);
        }

        public void prodRelacionadoXML()
        {
            if (seleccionarSugerido == 0)   // si no selecciono ningun producto sugerido
            {
                if (seleccionarDefault == 0)        // si el primer producto mostrado no esta relacionado
                {
                    if (dtConfirmarProdRelXML.Rows.Count == 0) // confirmando que no hay ningun registro relacionado
                    {
                        totalProdSugerido = stockProdXML + Convert.ToInt32(StockProdSugerido);
                        // hacemos el query para la actualizacion del Stock
                        query = $"UPDATE Productos SET Stock = '{totalProdSugerido}' WHERE ID = '{IdProductoSugerido}'";
                        resultadoConsulta = cn.EjecutarConsulta(query);     // aqui vemos el resultado de la consulta

                        //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompletaRelacionada}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}', datetime('now', 'localtime'), '{Inventario.idReporte}', '{IdProductoSugerido}','{userId}')";

                        //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor,IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompletaRelacionada}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}','{IdProductoSugerido}','{userId}')";
                        query = $@"INSERT INTO 
	                        HistorialCompras(Concepto,
		                    Cantidad,
		                    ValorUnitario,
		                    Descuento,
		                    Precio,
		                    FechaLarga,
		                    Folio,
		                    RFCEmisor,
		                    NomEmisor,
		                    ClaveProdEmisor,
                            FechaOperacion, 
                            IDReporte,
		                    IDProducto,
		                    IDUsuario) 
                     VALUES('{concepto}',
	                        '{cantidad}',
	                        '{PrecioProd}',
	                        '{descuento}',
	                        '{precioOriginalConIVA.ToString("N2")}',
	                        '{fechaCompleta}',
	                        '{folio}',
	                        '{RFCEmisor}',
	                        '{nombreEmisor}',
	                        '{claveProdEmisor}',
                            datetime('now', 'localtime'), 
                            '{Inventario.idReporte}',
	                        '{idProducto}',
	                        '{userId}')";
                        cn.EjecutarConsulta(query);
                        idRecordProd = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM HistorialCompras ORDER BY ID DESC LIMIT 1", 1));
                        queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{userId}','{idRecordProd}','{fechaCompletaRelacionada}')";
                        cn.EjecutarConsulta(queryRecordHistorialProd);
                        queryrelacionXMLTable = $"INSERT INTO ProductoRelacionadoXML(NombreXML, Fecha, IDProducto, IDUsuario) VALUES('{concepto}', '{fechaCompletaRelacionada}', '{IdProductoSugerido}', '{userId}')";
                        cn.EjecutarConsulta(queryrelacionXMLTable);
                        seleccionarSugerido = 0;
                    }
                }
                else if (seleccionarDefault == 1)   // si el primer producto esta relacionado
                {
                    queryUpdateConfirmarProdRelXML = $"SELECT * FROM ProductoRelacionadoXML WHERE IDProducto = '{IdProductoSugerido}'";
                    dtUpdateConfirmarProdRelXML = cn.CargarDatos(queryUpdateConfirmarProdRelXML);
                    if (dtUpdateConfirmarProdRelXML.Rows.Count != 0)        // confirmando que esta relacionado
                    {
                        idProdRelXML = Convert.ToString(dtUpdateConfirmarProdRelXML.Rows[0]["IDProductoRelacionadoXML"].ToString());
                        totalProdSugerido = stockProdXML + Convert.ToInt32(StockProdSugerido);
                        // hacemos el query para la actualizacion del Stock
                        query = $"UPDATE Productos SET Stock = '{totalProdSugerido}' WHERE ID = '{IdProductoSugerido}'";
                        resultadoConsulta = cn.EjecutarConsulta(query);     // aqui vemos el resultado de la consulta

                        //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompletaRelacionada}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}', datetime('now', 'localtime'), '{Inventario.idReporte}', '{IdProductoSugerido}','{userId}')";

                        //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor,IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompletaRelacionada}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}','{IdProductoSugerido}','{userId}')";
                        query = $@"INSERT INTO 
	                        HistorialCompras(Concepto,
		                    Cantidad,
		                    ValorUnitario,
		                    Descuento,
		                    Precio,
		                    FechaLarga,
		                    Folio,
		                    RFCEmisor,
		                    NomEmisor,
		                    ClaveProdEmisor,
                            FechaOperacion, 
                            IDReporte,
		                    IDProducto,
		                    IDUsuario) 
                     VALUES('{concepto}',
	                        '{cantidad}',
	                        '{PrecioProd}',
	                        '{descuento}',
	                        '{precioOriginalConIVA.ToString("N2")}',
	                        '{fechaCompleta}',
	                        '{folio}',
	                        '{RFCEmisor}',
	                        '{nombreEmisor}',
	                        '{claveProdEmisor}',
                            datetime('now', 'localtime'), 
                            '{Inventario.idReporte}',
	                        '{idProducto}',
	                        '{userId}')";
                        cn.EjecutarConsulta(query);
                        idRecordProd = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM HistorialCompras ORDER BY ID DESC LIMIT 1", 1));
                        queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{userId}','{idRecordProd}','{fechaCompletaRelacionada}')";
                        cn.EjecutarConsulta(queryRecordHistorialProd);
                        queryrelacionXMLTable = $"UPDATE ProductoRelacionadoXML SET Fecha = '{fechaCompletaRelacionada}', IDProducto = '{IdProductoSugerido}', IDUsuario = '{userId}' WHERE NombreXML = '{concepto}' AND IDProductoRelacionadoXML = '{idProdRelXML}'";
                        cn.EjecutarConsulta(queryrelacionXMLTable);
                        seleccionarSugerido = 0;
                    }
                }
            }
            else if (seleccionarSugerido == 2)  // si selecciono algun producto
            {
                queryUpdateConfirmarProdRelXML = $"SELECT * FROM ProductoRelacionadoXML WHERE IDProducto = '{IdProductoSugerido}'";
                dtUpdateConfirmarProdRelXML = cn.CargarDatos(queryUpdateConfirmarProdRelXML);
                if (dtUpdateConfirmarProdRelXML.Rows.Count == 0)        // si no tiene producto relacionado esa ID
                {
                    queryrelacionXMLTable = $"SELECT * FROM ProductoRelacionadoXML WHERE NombreXML = '{concepto}'";
                    dtConfirmarProdRelXML = cn.CargarDatos(queryrelacionXMLTable);
                    if (dtConfirmarProdRelXML.Rows.Count == 0)          // si es que no esta tampoco el producto relacionado
                    {
                        totalProdSugerido = stockProdXML + Convert.ToInt32(StockProdSugerido);
                        // hacemos el query para la actualizacion del Stock
                        query = $"UPDATE Productos SET Stock = '{totalProdSugerido}' WHERE ID = '{IdProductoSugerido}'";
                        resultadoConsulta = cn.EjecutarConsulta(query);     // aqui vemos el resultado de la consulta

                        //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompletaRelacionada}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}', datetime('now', 'localtime'), '{Inventario.idReporte}', '{IdProductoSugerido}','{userId}')";

                        //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor,IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompletaRelacionada}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}','{IdProductoSugerido}','{userId}')";
                        query = $@"INSERT INTO 
	                        HistorialCompras(Concepto,
		                    Cantidad,
		                    ValorUnitario,
		                    Descuento,
		                    Precio,
		                    FechaLarga,
		                    Folio,
		                    RFCEmisor,
		                    NomEmisor,
		                    ClaveProdEmisor,
                            FechaOperacion, 
                            IDReporte,
		                    IDProducto,
		                    IDUsuario) 
                     VALUES('{concepto}',
	                        '{cantidad}',
	                        '{PrecioProd}',
	                        '{descuento}',
	                        '{precioOriginalConIVA.ToString("N2")}',
	                        '{fechaCompleta}',
	                        '{folio}',
	                        '{RFCEmisor}',
	                        '{nombreEmisor}',
	                        '{claveProdEmisor}',
                            datetime('now', 'localtime'), 
                            '{Inventario.idReporte}',
	                        '{idProducto}',
	                        '{userId}')";
                        cn.EjecutarConsulta(query);
                        idRecordProd = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM HistorialCompras ORDER BY ID DESC LIMIT 1", 1));
                        queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{userId}','{idRecordProd}','{fechaCompletaRelacionada}')";
                        cn.EjecutarConsulta(queryRecordHistorialProd);
                        queryrelacionXMLTable = $"INSERT INTO ProductoRelacionadoXML(NombreXML, Fecha, IDProducto, IDUsuario) VALUES('{concepto}', '{fechaCompletaRelacionada}', '{IdProductoSugerido}', '{userId}')";
                        cn.EjecutarConsulta(queryrelacionXMLTable);
                        seleccionarSugerido = 0;
                    }
                    else if (dtConfirmarProdRelXML.Rows.Count != 0)
                    {
                        idProdRelXML = Convert.ToString(dtConfirmarProdRelXML.Rows[0]["IDProductoRelacionadoXML"].ToString());
                        totalProdSugerido = stockProdXML + Convert.ToInt32(StockProdSugerido);
                        // hacemos el query para la actualizacion del Stock
                        query = $"UPDATE Productos SET Stock = '{totalProdSugerido}' WHERE ID = '{IdProductoSugerido}'";
                        resultadoConsulta = cn.EjecutarConsulta(query);     // aqui vemos el resultado de la consulta

                        //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompletaRelacionada}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}', datetime('now', 'localtime'), '{Inventario.idReporte}', '{IdProductoSugerido}','{userId}')";

                        //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor,IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompletaRelacionada}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}','{IdProductoSugerido}','{userId}')";
                        query = $@"INSERT INTO 
	                        HistorialCompras(Concepto,
		                    Cantidad,
		                    ValorUnitario,
		                    Descuento,
		                    Precio,
		                    FechaLarga,
		                    Folio,
		                    RFCEmisor,
		                    NomEmisor,
		                    ClaveProdEmisor,
                            FechaOperacion, 
                            IDReporte,
		                    IDProducto,
		                    IDUsuario) 
                     VALUES('{concepto}',
	                        '{cantidad}',
	                        '{PrecioProd}',
	                        '{descuento}',
	                        '{precioOriginalConIVA.ToString("N2")}',
	                        '{fechaCompleta}',
	                        '{folio}',
	                        '{RFCEmisor}',
	                        '{nombreEmisor}',
	                        '{claveProdEmisor}',
                            datetime('now', 'localtime'), 
                            '{Inventario.idReporte}',
	                        '{idProducto}',
	                        '{userId}')";
                        cn.EjecutarConsulta(query);
                        idRecordProd = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM HistorialCompras ORDER BY ID DESC LIMIT 1", 1));
                        queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{userId}','{idRecordProd}','{fechaCompletaRelacionada}')";
                        cn.EjecutarConsulta(queryRecordHistorialProd);
                        queryrelacionXMLTable = $"UPDATE ProductoRelacionadoXML SET Fecha = '{fechaCompletaRelacionada}', IDProducto = '{IdProductoSugerido}', IDUsuario = '{userId}' WHERE NombreXML = '{concepto}' AND IDProductoRelacionadoXML = '{idProdRelXML}'";
                        cn.EjecutarConsulta(queryrelacionXMLTable);
                        seleccionarSugerido = 0;
                    }
                }
                else if (dtUpdateConfirmarProdRelXML.Rows.Count != 0)   // si es que el producto ya esta relacionado
                {
                    idProdRelXML = Convert.ToString(dtUpdateConfirmarProdRelXML.Rows[0]["IDProductoRelacionadoXML"].ToString());
                    totalProdSugerido = stockProdXML + Convert.ToInt32(StockProdSugerido);
                    // hacemos el query para la actualizacion del Stock
                    query = $"UPDATE Productos SET Stock = '{totalProdSugerido}' WHERE ID = '{IdProductoSugerido}'";
                    resultadoConsulta = cn.EjecutarConsulta(query);     // aqui vemos el resultado de la consulta

                    //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor, FechaOperacion, IDReporte, IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompletaRelacionada}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}', datetime('now', 'localtime'), '{Inventario.idReporte}', '{IdProductoSugerido}','{userId}')";

                    //query = $"INSERT INTO HistorialCompras(Concepto,Cantidad,ValorUnitario,Descuento,Precio,FechaLarga,Folio,RFCEmisor,NomEmisor,ClaveProdEmisor,IDProducto,IDUsuario) VALUES('{concepto}','{cantidad}','{precioOriginalConIVA.ToString("N2")}','{descuento}','{precio}','{fechaCompletaRelacionada}','{folio}','{RFCEmisor}','{nombreEmisor}','{claveProdEmisor}','{IdProductoSugerido}','{userId}')";
                    query = $@"INSERT INTO 
	                        HistorialCompras(Concepto,
		                    Cantidad,
		                    ValorUnitario,
		                    Descuento,
		                    Precio,
		                    FechaLarga,
		                    Folio,
		                    RFCEmisor,
		                    NomEmisor,
		                    ClaveProdEmisor,
                            FechaOperacion, 
                            IDReporte,
		                    IDProducto,
		                    IDUsuario) 
                     VALUES('{concepto}',
	                        '{cantidad}',
	                        '{PrecioProd}',
	                        '{descuento}',
	                        '{precioOriginalConIVA.ToString("N2")}',
	                        '{fechaCompleta}',
	                        '{folio}',
	                        '{RFCEmisor}',
	                        '{nombreEmisor}',
	                        '{claveProdEmisor}',
                            datetime('now', 'localtime'), 
                            '{Inventario.idReporte}',
	                        '{idProducto}',
	                        '{userId}')";
                    cn.EjecutarConsulta(query);
                    idRecordProd = Convert.ToInt32(cn.EjecutarSelect("SELECT ID FROM HistorialCompras ORDER BY ID DESC LIMIT 1", 1));
                    queryRecordHistorialProd = $"INSERT INTO HistorialModificacionRecordProduct(IDUsuario,IDRecordProd,FechaEditRecord) VALUES('{userId}','{idRecordProd}','{fechaCompletaRelacionada}')";
                    cn.EjecutarConsulta(queryRecordHistorialProd);
                    string queryDelProdRelXML = $"DELETE FROM ProductoRelacionadoXML WHERE IDProductoRelacionadoXML = '{idProdRelXML}'";
                    cn.EjecutarConsulta(queryDelProdRelXML);
                    queryrelacionXMLTable = $"UPDATE ProductoRelacionadoXML SET Fecha = '{fechaCompletaRelacionada}', IDProducto = '{IdProductoSugerido}', IDUsuario = '{userId}' WHERE NombreXML = '{concepto}'";
                    cn.EjecutarConsulta(queryrelacionXMLTable);
                    seleccionarSugerido = 0;
                }
            }
            else if (seleccionarSugerido == 3)
            {
                string queryBarrExtSelectSugerido, queryProdAtService, queryUpdateProd;
                DataRow row, Row;
                int Resultado, resul;

                Row = dtSelectSugerido.Rows[0];

                if (Row["ClaveInterna"].ToString() == "" && Row["CodigoBarras"].ToString() == "" && Row["Tipo"].ToString() == "P")
                {
                    queryUpdateProd = $"UPDATE Productos SET ClaveInterna = '{lblNoIdentificacionXML.Text}' WHERE IDUsuario = '{FormPrincipal.userID}' AND Nombre = '{Row["Nombre"].ToString()}'";
                    Resultado = cn.EjecutarConsulta(queryUpdateProd);
                    if (Resultado > 0)
                    {

                    }
                    else if (Resultado <= 0)
                    {
                        MessageBox.Show("El Producto No fue agregado a la Clave Interna...", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else if (Row["ClaveInterna"].ToString() == "" && Row["CodigoBarras"].ToString() != "" && Row["Tipo"].ToString() == "P")
                {
                    queryUpdateProd = $"UPDATE Productos SET ClaveInterna = '{lblNoIdentificacionXML.Text}' WHERE IDUsuario = '{FormPrincipal.userID}' AND Nombre = '{Row["Nombre"].ToString()}'";
                    Resultado = cn.EjecutarConsulta(queryUpdateProd);
                    if (Resultado > 0)
                    {

                    }
                    else if (Resultado <= 0)
                    {
                        MessageBox.Show("El Producto No fue agregado a la Clave Interna...", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else if (Row["ClaveInterna"].ToString() != "" && Row["CodigoBarras"].ToString() == "" && Row["Tipo"].ToString() == "P")
                {
                    queryUpdateProd = $"UPDATE Productos SET CodigoBarras = '{lblNoIdentificacionXML.Text}' WHERE IDUsuario = '{FormPrincipal.userID}' AND Nombre = '{Row["Nombre"].ToString()}'";
                    Resultado = cn.EjecutarConsulta(queryUpdateProd);
                    if (Resultado > 0)
                    {

                    }
                    else if (Resultado <= 0)
                    {
                        MessageBox.Show("El Producto No fue agregado a la Clave Interna...", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else if (Row["ClaveInterna"].ToString() != "" && Row["CodigoBarras"].ToString() != "" && Row["Tipo"].ToString() == "P")
                {
                    queryUpdateProd = $"INSERT INTO CodigoBarrasExtras(CodigoBarraExtra, IDProducto) VALUES('{lblNoIdentificacionXML.Text}', '{IdProductoSugerido}')";
                    Resultado = cn.EjecutarConsulta(queryUpdateProd);
                    if (Resultado > 0)
                    {

                    }
                    else if (Resultado <= 0)
                    {
                        MessageBox.Show("El Producto No fue agregado al Código de Barras Extra...", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else if (Row["ClaveInterna"].ToString() != "" && Row["CodigoBarras"].ToString() != "" && Row["Tipo"].ToString() == "S")
                {
                    queryBarrExtSelectSugerido = $"INSERT INTO CodigoBarrasExtras(CodigoBarraExtra, IDProducto) VALUES('{lblNoIdentificacionXML.Text}', '{IdProductoSugerido}')";
                    Resultado = cn.EjecutarConsulta(queryBarrExtSelectSugerido);

                    if (Resultado > 0)
                    {
                        
                    }
                    else if (Resultado <= 0)
                    {
                        MessageBox.Show("Prodcuto No agregado...", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    queryProdAtService = $"INSERT INTO ProductosDeServicios(Fecha, IDServicio, IDProducto, NombreProducto, Cantidad) VALUES('{fechaCompletaRelacionada}', '{IdProductoSugerido}', '{Row["ID"].ToString()}', '{Row["Nombre"].ToString()}', '1')";
                    resul = cn.EjecutarConsulta(queryProdAtService);
                    if (resul > 0)
                    {

                    }
                    else if (resul <= 0)
                    {
                        MessageBox.Show("El Servicio / Paquete / Combo No agregado a codigo de barras extra...", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            dtConfirmarProdRelXML.Rows.Clear();
            dtConfirmarProdRelXML.Clear();
            //dtUpdateConfirmarProdRelXML.Rows.Clear();
            //dtUpdateConfirmarProdRelXML.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /************************************
            *   iniciamos las variables a 0     *
            ************************************/
            resultadoSearchNoIdentificacion = 0;                        // ponemos los valores en 0
            resultadoSearchCodBar = 0;                                  // ponemos los valores en 0
            textBoxNoIdentificacion = txtBoxClaveInternaProd.Text;      // tomamos el valor del TextBox para hacer la comparacion

            if (consultListProd == 1)       // si el producto es seleccionado desde la lista del Producto
            {
                PrecioProd = float.Parse(txtBoxPrecioProd.Text);                    // almacenamos el precio que tiene la caja de texto
                PrecioProdToCompare = float.Parse(lblPrecioRecomendadoProd.Text);   // almacenamos el precio sugerido para hacer la comparacion
                comprobarPrecioMayorIgualRecomendado();                             // Llamamos la funsion para comparar el precio del producto con el sugerido
                NombreProd = txtBoxDescripcionProd.Text;                            // almacenamos el contenido del TextBox
                verNvoStock();                                                      // funsion para ver el nvo stock
                if (resultadoSearchNoIdentificacion == 1 && resultadoSearchCodBar == 1)
                {
                    //MessageBox.Show("El Número de Identificación; ya se esta utilizando en\ncomo clave interna ó codigo de barras de algun producto", "Error de Actualizar el Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (resultadoSearchNoIdentificacion == 0 || resultadoSearchCodBar == 0)
                {
                    if (ListProd.opcionGuardarFin == 1 || ListProd.opcionGuardarFin == 2)   // en caso que alguno de los dos campos esten en blanco
                    {
                        RelacionarStockClaveInterna();
                    }
                    else if (ListProd.opcionGuardarFin == 3)                    // en el caso que tenga en blanco el campo de CodigoBarras en blanco va ir en el de codigo de barras
                    {
                        RelacionarStockCodigoBarras();
                    }
                    else if (ListProd.opcionGuardarFin == 4)                    // en el caso que los dos campos tengan contenido se asigna el siguiente valor
                    {
                        CodigoBarrasExtras();
                    }
                    
                    if (resultadoConsulta == 1)                         // si el resultado es 1
                    {
                        
                    }
                    else                                                // si el resultado es 0
                    {
                        
                    }
                }
            }
            else if (seleccionarSugerido == 3 && txtBoxDescripcionProd.Text != "")
            {
                prodRelacionadoXML();   // llamamos el metodo relacionar por XML
                RecorrerXML();          // recorrer el archivo XML
            }
            else if (seleccionarSugerido == 2 && seleccionSugeridoNomb != "")
            {
                prodRelacionadoXML();   // llamamos el metodo relacionar por XML
                RecorrerXML();          // recorrer el archivo XML
                
            }
            else if (seleccionarSugerido == 1 && seleccionSugeridoNomb != "")
            {
                prodRelacionadoXML();   // llamamos el metodo relacionar por XML
                RecorrerXML();          // recorrer el archivo XML

            }
            else if (consultListProd == 0)
            {
                if (resultadoSearchProd == 0)       // si la busqueda del producto da negativo (No esta el producto registrado asi con esa Clave Interna)
                {
                    if (txtBoxPrecioProd.Text == "")    // si el TextBox esta sin contenido
                    {
                        PrecioProd = 0;     // la variable PrecioProd la iniciamos a 0
                    }
                    if (lblPrecioRecomendadoProd.Text == "")    // si el Label esta sin contenido
                    {
                        PrecioProdToCompare = 1;    // la variable la ponemos en 1
                        lblPrecioRecomendadoProd.Text = PrecioRecomendado.ToString("N2");   // asignamos el valor del lblPrecioRecomendado
                    }
                    comprobarPrecioMayorIgualRecomendado();                 // Llamamos la funsion para comparar el precio del producto con el sugerido
                    if (lblStockProd.Text == "")    // si el label esta sin contenido
                    {
                        stockProd = 0;  // la variable la ponemos a 0
                    }
                    verNvoStock();          // funsion para ver el nvo stock
                    if (NoClaveInterna != textBoxNoIdentificacion)          // si son diferentes los datos osea hubo un cambio
                    {
                        searchClavIntProd();        // hacemos la busqueda que no se repita en CalveInterna
                        searchCodBar();             // hacemos la busqueda que no se repita en CodigoBarra
                        if (resultadoSearchNoIdentificacion == 1 && resultadoSearchCodBar == 1)     // si es que 
                        {

                        }
                        else if (resultadoSearchNoIdentificacion == 0 || resultadoSearchCodBar == 0)
                        {
                            ActualizarStock();      // realizamos la actualizacion
                            RecorrerXML();          // recorrer el archivo XML
                        }
                    }
                    else                                                    // si no hubo un cambio
                    {

                    }

                }
                else if (resultadoSearchProd == 1)       // si la busqueda del producto da positivo
                {
                    comprobarPrecioMayorIgualRecomendado();                 // Llamamos la funsion para comparar el precio del producto con el sugerido
                    verNvoStock();                                          // funsion para ver el nvo stock
                    if (NoClaveInterna != textBoxNoIdentificacion)          // si son diferentes los datos osea hubo un cambio
                    {
                        searchClavIntProd();        // hacemos la busqueda que no se repita en CalveInterna
                        searchCodBar();             // hacemos la busqueda que no se repita en CodigoBarra
                        ActualizarStock();      // realizamos la actualizacion
                        RecorrerXML();          // recorrer el archivo XML
                    }
                    else                                                    // si no hubo un cambio
                    {
                        if (resultadoSearchNoIdentificacion == 1 && resultadoSearchCodBar == 1)
                        {
                            //MessageBox.Show("El Número de Identificación; ya se esta utilizando en\ncomo clave interna ó codigo de barras de algun producto", "Error de Actualizar el Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else if (resultadoSearchNoIdentificacion == 0 || resultadoSearchCodBar == 0)
                        {
                            ActualizarStock();      // realizamos la actualizacion
                            //MessageBox.Show("Actualización del Stock exitosa", "Actualziacion del Producto exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RecorrerXML();          // recorrer el archivo XML
                        }
                    }
                }
            }
        }

        private void picBoxBuscar_Click(object sender, EventArgs e)
        {
            ListProd.FormClosing += delegate    // dectecta cuando se esta cerrando la forma ListProd
            {
                consultListProd = ListProd.consultadoDesdeListProdFin;  // en esta variable almacenamos su valor
                if (consultListProd == 1)   // si el valor es 1 es positiva la seleccion
                {
                    OcultarPanelSinRegistro();                                              // ocultamos la ventana Si tiene registro del Stock
                    idListProd = ListProd.IdProdStrFin;                                     // almacenamos el valor del ID del roducto
                    txtBoxDescripcionProd.Text = ListProd.NombreProdStrFin;                 // mostramos los datos ya almacenado del producto
                    if (ListProd.opcionGuardarFin == 1 || ListProd.opcionGuardarFin == 2)   // en caso que alguno de los dos campos esten en blanco
                    {
                        txtBoxClaveInternaProd.Text = lblNoIdentificacionXML.Text;          // reasignamos la clave interna del producto al que trae el XML
                        lblCodigoBarrasProd.Text = ListProd.CodigoBarrasProdStrFin;         // mostramos los datos ya almacenado del producto
                    }
                    else if (ListProd.opcionGuardarFin == 3)                    // en el caso que tenga en blanco el campo de CodigoBarras en blanco va ir en el de codigo de barras
                    {
                        txtBoxClaveInternaProd.Text = ListProd.ClaveInternaProdStrFin;      // mostramos los datos ya almacenado del producto
                        lblCodigoBarrasProd.Text = lblNoIdentificacionXML.Text;             // mostramos los datos ya almacenado del producto
                    }
                    else if (ListProd.opcionGuardarFin == 4)                    // en el caso que los dos campos tengan contenido se asigna el siguiente valor
                    {
                        txtBoxClaveInternaProd.Text = ListProd.ClaveInternaProdStrFin;      // mostramos los datos ya almacenado del producto
                        lblCodigoBarrasProd.Text = ListProd.CodigoBarrasProdStrFin;         // mostramos los datos ya almacenado del producto
                    }
                    lblStockProd.Text = ListProd.StockProdStrFin;                       // mostramos los datos ya almacenado del producto
                    stockProd = int.Parse(lblStockProd.Text);                           // almacenamos el Stock del Producto en stockProd para su posterior manipulacion
                    lblPrecioRecomendadoProd.Text = lblPrecioRecomendadoXML.Text;       // mostramos los datos ya almacenado del producto
                    txtBoxPrecioProd.Text = ListProd.PrecioDelProdStrFin;               // mostramos los datos ya almacenado del producto
                    PrecioProd = float.Parse(txtBoxPrecioProd.Text);                    // almacenamos el Precio del Producto en PrecioProd para su posterior manipulacion
                    seleccionarSugerido = 0;
                    ActivarBtnSi();
                }
                if (consultListProd == 0)   // si el valor es 0 si es que no selecciono nada
                {
                    MostarPanelSinRegistro();                                           // Mostramos la ventana Si no tiene registro del Stock
                    DesactivarBtnSi();
                }
            };

            if (!ListProd.Visible)
            {
                ListProd.ShowDialog();
            }
            else
            {
                ListProd.BringToFront();
            }
        }

        private void DGVSugeridos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string querySeleccionado;

            fechaSitema = fechaHoy.ToString("s");
            fechaSola = fechaSitema.Substring(0, 10);
            horaSola = fechaSitema.Substring(11);
            fechaCompletaRelacionada = fechaSola + " " + horaSola;

            numFila = DGVSugeridos.CurrentRow.Index;
            
            IdProductoSugerido = DGVSugeridos[0, numFila].Value.ToString();
            NombProductoSugerido = DGVSugeridos[1, numFila].Value.ToString();

            querySeleccionado = $"SELECT * FROM Productos WHERE ID = '{IdProductoSugerido}' AND Nombre = '{NombProductoSugerido}' AND IDUsuario = '{FormPrincipal.userID}'";
            dtSelectSugerido = cn.CargarDatos(querySeleccionado);

            DataRow row = dtSelectSugerido.Rows[0];

            txtBoxDescripcionProd.Text = row["Nombre"].ToString();
            txtBoxClaveInternaProd.Text = row["ClaveInterna"].ToString();
            lblCodigoBarrasProd.Text = row["CodigoBarras"].ToString();
            lblStockProd.Text = row["Stock"].ToString();
            lblPrecioRecomendadoProd.Text = lblPrecioRecomendadoXML.Text;
            txtBoxPrecioProd.Text = row["Precio"].ToString();
            
            if (row["ClaveInterna"].ToString() == "" && row["CodigoBarras"].ToString() == "")   // si claveInterna y CodigoBarras esta sin datos
            {
                txtBoxClaveInternaProd.Text = lblNoIdentificacionXML.Text;
            }
            else if (row["ClaveInterna"].ToString() == "" && row["CodigoBarras"].ToString() != "")   // si claveInterna esta sin datos y CodigoBarras esta con datos
            {
                txtBoxClaveInternaProd.Text = lblNoIdentificacionXML.Text;
            }
            else if (row["ClaveInterna"].ToString() != "" && row["CodigoBarras"].ToString() == "")   // si claveInterna esta con datos y CodigoBarras esta sin datos
            {
                lblCodigoBarrasProd.Text = lblNoIdentificacionXML.Text;
            }
            else if (row["ClaveInterna"].ToString() != "" && row["CodigoBarras"].ToString() != "")   // si claveInterna esta con datos y CodigoBarras esta con datos
            {
                //MessageBox.Show("Aqui proceso para codigo de barras extra del producto.", "En construcción", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            seleccionarSugerido = 3;
            ActivarBtnSi();
            OcultarPanelSinRegistro();
        }

        public AgregarStockXML()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecorrerXML();      // recorrer el archivo XML
        }

        public void leerXMLFile()
        {
            using (f = new OpenFileDialog())        // ejecutar el openFileDialog
            {
                // poner el setup de archivos ver etc
                f.DefaultExt = "xml";                       // poemos que solo vea extensiones .XML
                f.Filter = "Archivo XML (*.xml) | *.xml";   // hacer filtros de puro XML no de otra opcion de archivos
                f.Title = "Selecciona tu archivo XML";      // el titulo de la ventana
                if (f.ShowDialog() == DialogResult.OK)      // si se le da clic en el boton Ok
                {
                    // si es que el usuario pone otra extension no permitida se le pone este mensaje
                    if (!String.Equals(Path.GetExtension(f.FileName), ".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("El tipo de archivo seleccionado no es soportado en esta aplicación,\nDebes seleccionar un archivo con extension XML",
                                        "Tipo de Archivo Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else    // si es que todo esta bien se le da el procesamiento del archivo
                    {
                        //MessageBox.Show("El archivo seleccionado,\nse esta Procesando", "Tipo de Archivo Valido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        serial = new XmlSerializer(typeof(Comprobante));                    // iniciamos el objeto serial para leer el XML
                        fs = new FileStream(f.FileName, FileMode.Open, FileAccess.Read);    // iniciamos el objeto fs para poder leer el archivo XML y no dejarlo en uso
                        ds = (Comprobante)serial.Deserialize(fs);                           // iniciamos el objeto ds y le hacemos un cast con la clase Comprobante y le pasamos la lectura del XML

                        if (ds.Receptor.Rfc == rfc)                                         // comparamos si el RFC-Receptor(del archivo XML) es igual al RFC del usruario del sistema
                        {
                            rutaXML = f.FileName; //Almacenamos el nombre y ruta completa del archivo cargado
                            cantProductos = 0;          // la cantidad de Productos la ponemos en 0
                            OcultarPanelCarga();        // ocultamos el panel de cargar XML
                            btnLoadXML.Hide();          // ocultamos el botonXML
                            MostrarPanelRegistro();     // mostramos el panelRegistro para actualizar el stock
                            // hacemos el recorrido de lo que se almaceno en la clase Conceptos
                            for (index = 0; index < ds.Conceptos.Count(); index++)
                            {
                                cantProductos++;    // aumentamos la variable en uno
                            }
                            lblLargodelXML.Text = cantProductos.ToString();     // asignamos la cantidad de Productos al Label
                            lblPosicionActualXML.Text = "0";                    // iniciamos el Label a 0
                            RecorrerXML();          // recorremos el XML
                        }
                        else                                                                // so es que no son iguales los RFC
                        {
                            // mostramos este mensaje al usuario del sistema
                            MessageBox.Show("El archivo XML seleccionado no contiene tu RFC,\nDebes seleccionar un archivo XML con tu RFC",
                                    "No tiene tu RFC.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MostrarPanelCarga();        // mostramos el panel de carga del archivo XML
                            btnLoadXML.Show();          // mostramos el botonXML
                        }
                    }
                }
                else    // si cierra el cuadro de dialogo
                {
                    // muestra el siguiente mensaje al usuario
                    MessageBox.Show("El tipo de archivo seleccionado no es soportado en esta aplicación,\nDebes seleccionar un archivo con extension XML",
                                    "Tipo de Archivo Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AgregarStockXML_Load(object sender, EventArgs e)
        {
            DesactivarBtnSi();
            limpiarLblXNL(); // Llamamos la funsion para poner en limpio los datos que vienen del XML

            groupBox5.BackColor = Color.FromArgb(130, 130, 130);

            // asignamos el valor de userName que sea
            // el valor que tiene userNickUsuario en el formulario Principal
            userId = FormPrincipal.userID.ToString();
            userName = FormPrincipal.userNickName;
            passwordUser = FormPrincipal.userPass;

            consulta(); // Llamamos la funsion de consulta para buscar el producto
            MostrarPanelCarga(); // hacemos visible la ventana de cargar archivo XML
            OcultarPanelRegistro(); // ocultamos la ventana de ver registro del Stock
            OcultarPanelSinRegistro(); // ocultamos la ventana Si no tiene registro del Stock
            consultListProd = 0;
            seleccionarSugerido = 1;
            seleccionarDefault = 0;
            seleccionSugeridoNomb = "";
        }

        private void DGVSugeridos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            fechaSitema = fechaHoy.ToString("s");
            fechaSola = fechaSitema.Substring(0, 10);
            horaSola = fechaSitema.Substring(11);
            fechaCompletaRelacionada = fechaSola + " " + horaSola;

            numFila = DGVSugeridos.CurrentRow.Index;
            //label2.Text = numFila.ToString();
            IdProductoSugerido = DGVSugeridos[0, numFila].Value.ToString();
            NombProductoSugerido = DGVSugeridos[1, numFila].Value.ToString();
            seleccionSugeridoNomb = NombProductoSugerido;
            StockProdSugerido = DGVSugeridos[2, numFila].Value.ToString();
            CoincidenciaSugerido = DGVSugeridos[3, numFila].Value.ToString();
            seleccionarSugerido = 2;
            ActivarBtnSi();
        }

        private void btnLoadXML_Click_1(object sender, EventArgs e)
        {
            leerXMLFile(); // mandamos llamar la funsion de leerXML
        }

        private void label11_Click(object sender, EventArgs e)
        {
            leerXMLFile(); // mandamos llamar la funsion de leerXML
        }

        private void label11_Enter(object sender, EventArgs e)
        {
            label11.ForeColor = Color.FromArgb(0, 140, 255);
        }

        private void label11_Leave(object sender, EventArgs e)
        {
            label11.ForeColor = Color.FromArgb(0, 0, 0);
        }

        private void AgregarStockXML_FormClosing(object sender, FormClosingEventArgs e)
        {
            MostrarPanelCarga(); // hacemos visible la ventana de cargar archivo XML
            btnLoadXML.Show(); // hacemos visible el botonXML de la ventana de cargar archivo XML
            consultListProd = 0;
        }
    }
}
