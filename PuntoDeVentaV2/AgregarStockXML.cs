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
            [XmlArrayItemAttribute("Traslado")]
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


        /****************************************************
        *       se declaran e inicializan las variables     * 
        *       para poder hacer validaciones  etc.         *
        ****************************************************/
        // se declara el objeto para poder usarlo y llamar la ventana para agregrar Nvo Producto
        public AgregarEditarProducto FormAgregar = new AgregarEditarProducto("Agregar Producto");

        OpenFileDialog f;   // objeto para poder abrir el openDialog

        Conexion cn = new Conexion();   // iniciamos un objeto de tipo conexion

        public string userName;         // declaramos la variable que almacenara el valor de userName
        public string passwordUser;     // declaramos la variable que almacenara el valor de passwordUser
        public string userId;           // declaramos la variable que almacenara el valor de userId

        // variables para poder hacer las consulta y actualizacion a la base de datos
        string buscar;                      // almacena el query para buscar el usuario al que se le va agregar los productos
        string ClaveInterna;                // almacena la calveInterna(NoIdentificacion) del XML
        string NoClaveInterna;              // almacena el Número de clave Interna del XML para su posterior uso en las validaciones
        string idProducto;                  // almacena el Número de ID del producto para si hay alguna actualizacion poder hacerlo
        string query;                       // almacena el query para poder hacer la actualizacion del producto
        string NombreProd;                  // almacena el contenido del TextBox para poder hacer la actualizacion en la base de datos
        string textBoxNoIdentificacion;     // almacena el contenido del TextBox para poder hacer la actualizacion en la base de datos

        // variables para poder almacenar la tabla que resulta sobre la consulta el base de datos
        public DataTable dt;                    // almacena el resultado de la funcion de CargarDatos de la funcion consulta
        public DataTable dtProductos;           // almacena el resultado de la funcion de CargarDatos de la funcion serachDatos
        public DataTable dtClaveInterna;        // almacena el resultado de la funcion de CargarDatos de la funcion searchClavIntProd
        public DataTable dtCodBar;              // almacena el resultado de la funcion de CargarDatos de la funcion searchCodBar

        // variables para poder realizar el recorrido, calculo de valores etc
        int index;                              // sirve para saber que Row de la Tabla estamos y poder obtener el valode de alguna celda
        int cantProductos;                      // sirve para poder mostrar la cantidad de productos que tiene el Archivo XML
        int resultadoSearch;                    // sirve para ver si la consulta de buscar arroja alguna fila
        int resultadoSearchProd;                // srive para ver si el producto existe busca en los campos de CodigoBarras y ClaveInterna
        int resultadoCambioPrecio;              // sirve para ver si el usuario hizo alguna actualizacion en el precio
        int resultadoSearchNoIdentificacion;    // sirve para ver si el producto existe en los campos CodigoBarras y ClaveInterna en la funcion searchClavIntProd()
        int resultadoSearchCodBar;              // sirve para ver si el producto existe en los campos CodigoBarras y ClaveInterna en la funcion searchCodBar()
        int stockProd;                          // sirve para almacenar en ella, la cantidad de stock que tenemos de ese producto
        int stockProdXML;                       // sirve para almacenar en ella, la cantidad del stock que nos llego en el archivo XML
        int totalProd;                          // sirve para en ella almacenar la suma del Stock del producto mas el stock del archivo XML
        
        // variables para poder hacer los calculos sobre el producto
        float importe;                          // convertimos el importe del Archivo XML para su posterior manipulacion
        float descuento;                        // convertimos el descuento del Archivo XML para su posterior manipulacion
        float cantidad;                         // convertimos el cantidad del Archivo XML para su posterior manipulacion
        float precioOriginalSinIVA;             // Calculamos el precio Original Sin IVA (importe - descuento)/cantidad
        float precioOriginalConIVA;             // Calculamos el precio Original Con IVA (precioOriginalSinIVA)*1.16
        float PrecioRecomendado;                // calculamos Precio Recomendado (precioOriginalConIVA)*1.60
        float importeReal;                      // calculamos importe real (cantidad * precioOriginalConIVA)
        float PrecioProd;                       // almacenamos el Precio del Producto en PrecioProd para su posterior manipulacion
        float PrecioProdToCompare;              // almacenamos el precio sugerido para hacer la comparacion

        DialogResult dialogResult;              // creamos el objeto para poder abrir el cuadro de dialogo


        // variables para poder tomar el valor de los TxtBox y tambien hacer las actualizaciones
        // del valor que proviene de la base de datos ó tambien actualizar la Base de Datos
        string id;
        string rfc;

        // objetos para poder tratar la informacion del XML
        XmlSerializer serial;
        FileStream fs;
        Comprobante ds;

        // funcion para poder asignar los datos del XML a la ventana de Nvo Producto
        public void datosAgregarNvoProd()
        {
            FormAgregar.txtNombreProducto.Text = ds.Conceptos[index - 1].Descripcion;       // pasamos la descripcion
            FormAgregar.txtStockProducto.Text = ds.Conceptos[index - 1].Cantidad;           // pasamos la cantidad del XML
            FormAgregar.txtPrecioProducto.Text = PrecioRecomendado.ToString("N2");          // pasamos el precio recomendado
            FormAgregar.lblPrecioOriginal.Text = precioOriginalConIVA.ToString("N2");       // pasamos el precio origianl del XML
            FormAgregar.txtClaveProducto.Text = ds.Conceptos[index - 1].NoIdentificacion;   // pasamos la claveInterna del XML
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

            cargarDatosXML();
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
            FormAgregar.FormClosed += delegate
            {
                RecorrerXML(); // recorrer el archivo XML
            };

            if (FormAgregar.Text == "")
            {
                FormAgregar = new AgregarEditarProducto("Agregar Producto");
            }

            if (!FormAgregar.Visible)
            {
                datosAgregarNvoProd();
                FormAgregar.ShowDialog();
            }
            else
            {
                datosAgregarNvoProd();
                FormAgregar.BringToFront();
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

        // funsion para poder buscar los productos 
        // que coincidan con los campos de de ClaveInterna o el CodigoBarras
        // respecto al archivo XML en su campo de NoIdentificacion
        public void searchProd()
        {
            // preparamos el Query
            string search = $"SELECT Prod.ID, Prod.Nombre, Prod.ClaveInterna, Prod.Stock, Prod.CodigoBarras, Prod.Precio FROM Productos Prod WHERE Prod.IDUsuario = '{userId}' AND Prod.ClaveInterna = '{ClaveInterna}' OR Prod.CodigoBarras = '{ClaveInterna}'";
            dtProductos = cn.CargarDatos(search); // alamcenamos el resultado de la busqueda en dtProductos
            if (dtProductos.Rows.Count >= 1) // si el resultado arroja al menos una fila
            {
                resultadoSearchProd = 1;                                            // busqueda positiva
                NoClaveInterna = dtProductos.Rows[0]["ClaveInterna"].ToString();    // almacenamos el valor del NoClaveInterna
                datosProductos();                                                   // llamamos la funcion de datosProductos
                //MessageBox.Show("Producto Encontrado", "El Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtProductos.Rows.Count<=0) // si el resultado no arroja ninguna fila
            {
                resultadoSearchProd = 0;        // busqueda negativa
                limpiarLblProd();               // limpiamos los campos de producto
                //MessageBox.Show("Nuevo Producto", "El Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // funsion para poder buscar en los productos 
        // si coincide con los campos de de ClaveInterna
        // respecto al stock del producto en su campo de NoIdentificacion
        public void searchClavIntProd()
        {
            // preparamos el Query
            string search = $"SELECT Prod.ID, Prod.Nombre, Prod.ClaveInterna, Prod.Stock, Prod.CodigoBarras, Prod.Precio FROM Productos Prod WHERE Prod.IDUsuario = '{userId}' AND Prod.ClaveInterna = '{textBoxNoIdentificacion}' OR Prod.CodigoBarras = '{textBoxNoIdentificacion}'";
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
            string search = $"SELECT Prod.ID, Prod.Nombre, Prod.ClaveInterna, Prod.Stock, Prod.CodigoBarras, Prod.Precio FROM Productos Prod WHERE Prod.IDUsuario = '{userId}' AND Prod.CodigoBarras = '{textBoxNoIdentificacion}' OR Prod.ClaveInterna = '{textBoxNoIdentificacion}'";
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
            lblPosicionActualXML.Text = (index + 1).ToString();
            lblDescripcionXML.Text = ds.Conceptos[index].Descripcion;
            stockProdXML = int.Parse(ds.Conceptos[index].Cantidad);             // convertimos la cantidad del Archivo XML para su posterior manipulacion
            lblCantXML.Text = stockProdXML.ToString();
            importe = float.Parse(ds.Conceptos[index].Importe);                 // convertimos el Importe del Archivo XML para su posterior manipulacion
            descuento = float.Parse(ds.Conceptos[index].Descuento);             // convertimos el Descuento del Archivo XML para su posterior manipulacion
            cantidad = float.Parse(ds.Conceptos[index].Cantidad);               // convertimos la cantidad del Archivo XML para su posterior manipulacion
            precioOriginalSinIVA = (importe - descuento) / cantidad;            // Calculamos el precio Original Sin IVA (importe - descuento)/cantidad
            precioOriginalConIVA = precioOriginalSinIVA * (float)1.16;          // Calculamos el precio Original Con IVA (precioOriginalSinIVA)*1.16
            lblPrecioOriginalXML.Text = precioOriginalConIVA.ToString("N2");
            importeReal = cantidad * precioOriginalConIVA;                      // calculamos importe real (cantidad * precioOriginalConIVA)
            lblImpXML.Text = importeReal.ToString("N2");
            ClaveInterna = ds.Conceptos[index].NoIdentificacion;
            lblNoIdentificacionXML.Text = ClaveInterna;
            PrecioRecomendado = precioOriginalConIVA * (float)1.60;             // calculamos Precio Recomendado (precioOriginalConIVA)*1.60
            lblPrecioRecomendadoXML.Text = PrecioRecomendado.ToString("N2");
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
                index++;            // aumentamos en uno la variable index
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
                index++;            // aumentamos en uno la variable index
            }
        }

        // funsion para comprobar que el precio del stock sea
        // mayor o igual al que esta sugerido por el XML
        public void comprobarPrecioMayorIgualRecomendado()
        {
            if (PrecioProd > 0)
            {
                PrecioProd = float.Parse(txtBoxPrecioProd.Text);                    // almacenamos el precio que tiene la caja de texto
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

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxNoIdentificacion = txtBoxClaveInternaProd.Text;  // tomamos el valor del TextBox para hacer la comparacion
            if (resultadoSearchProd == 0)       // si la busqueda del producto da negativo
            {
                if (txtBoxPrecioProd.Text == "")    // si el TextBox esta sin contenido
                {
                    PrecioProd = 0;     // la variable PrecioProd la iniciamos a 0
                    //MessageBox.Show("Yes...... valor:"+ PrecioProdToCompare);
                }
                if (lblPrecioRecomendadoProd.Text == "")    // si el Label esta sin contenido
                {
                    PrecioProdToCompare = 1;    // la variable la ponemos en 1
                    lblPrecioRecomendadoProd.Text = PrecioRecomendado.ToString("N2");   // asignamos el valor del lblPrecioRecomendado 
                    //MessageBox.Show("No...... valor:" + PrecioProdToCompare);
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
                    //MessageBox.Show("No son Iguales", "los textos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (resultadoSearchNoIdentificacion==1 && resultadoSearchCodBar==1)
                    {
                        MessageBox.Show("El Número de Identificación; ya se esta utilizando en\ncomo clave interna ó codigo de barras de algun producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (resultadoSearchNoIdentificacion == 0 || resultadoSearchCodBar == 0)
                    {
                        ActualizarStock();      // realizamos la actualizacion
                        MessageBox.Show("Actualización del Stock exitosa", "Actualziacion del Producto exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RecorrerXML();          // recorrer el archivo XML
                    }
                }
                else                                                    // si no hubo un cambio
                {
                    // ToDo 
                    //MessageBox.Show("No esta dentro de lo planeado", "Algo salio Mal", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    //MessageBox.Show("No son Iguales", "los textos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else                                                    // si no hubo un cambio
                {
                    // ToDo 
                    //MessageBox.Show("No esta dentro de lo planeado", "Algo salio Mal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (resultadoSearchNoIdentificacion == 1 && resultadoSearchCodBar == 1)
                {
                    MessageBox.Show("El Número de Identificación; ya se esta utilizando en\ncomo clave interna ó codigo de barras de algun producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (resultadoSearchNoIdentificacion == 0 || resultadoSearchCodBar == 0)
                {
                    ActualizarStock();      // realizamos la actualizacion
                    MessageBox.Show("Actualización del Stock exitosa", "Actualziacion del Producto exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RecorrerXML();          // recorrer el archivo XML
                }
            }
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
                            MessageBox.Show("El archivo XML seleccionado no tiene la tu RFC,\nDebes seleccionar un archivo XML con tu RFC",
                                    "XML no contiene tu R.F.C.", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }
    }
}
