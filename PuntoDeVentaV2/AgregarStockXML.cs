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

        // se declaran e inicializan las variables para poder hacer validaciones  etc.

        // objeto para poder abrir el openDialog
        OpenFileDialog f;

        // iniciamos un objeto de tipo conexion
        Conexion cn = new Conexion();

        // declaramos la variable que almacenara el valor de userNickName
        public string userName;
        public string passwordUser;
        public string userId;

        // variables para poder hacer las consulta y actualizacion
        string buscar;

        // variables para poder hacer el recorrido y asignacion de los valores que estan el base de datos
        int index, cantProductos, resultadoSearch, resultadoSearchProd, resultadoCambioPrecio, resultadoSearchNoIdentificacion, resultadoSearchCodBar, stockProd, stockProdXML, totalProd;
        public DataTable dt, dtProductos, dtClaveInterna, dtCodBar;
        float importe, descuento, cantidad, precioOriginalSinIVA, precioOriginalConIVA, PrecioRecomendado, importeReal, PrecioProd, PrecioProdToCompare;
        string ClaveInterna, NoClaveInterna, idProducto, query, NombreProd;
        DialogResult dialogResult;
        string textBoxNoIdentificacion;

        // variables para poder tomar el valor de los TxtBox y tambien hacer las actualizaciones
        // del valor que proviene de la base de datos ó tambien actualizar la Base de Datos
        string id;
        string rfc;

        // objetos para poder tratar la informacion del XML
        XmlSerializer serial;
        FileStream fs;
        Comprobante ds;

        // funcion para poder saber que cliente es el que esta iniciando sesion en el sistema
        public void cargarDatos()
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
            buscar = $"SELECT u.ID, u.Usuario, u.Password, u.RFC FROM Usuarios u WHERE Usuario = '{userName}' AND Password = '{passwordUser}'";

            // almacenamos el resultado de la Funcion CargarDatos
            // que esta en la calse Consultas
            dt = cn.CargarDatos(buscar);

            cargarDatos();
        }

        // funcion para ocultar el panel en el que
        // esta el label que dice click cargar XML 
        // y el boton de XML
        public void OcultarPanelCarga()
        {
            panel1.Hide();
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
            // alamcenamos el resultado de la busqueda en dtProductos
            dtProductos = cn.CargarDatos(search);
            // si el resultado arroja al menos una fila
            if (dtProductos.Rows.Count > 0)
            {
                resultadoSearchProd = 1; // busqueda positiva
                NoClaveInterna = dtProductos.Rows[0]["ClaveInterna"].ToString();
            }
            // si el resultado no arroja ninguna fila
            else if (dtProductos.Rows.Count<=0)
            {
                resultadoSearchProd = 0; // busqueda negativa
                limpiarLblProd(); // limpiamos los campos de producto
                MessageBox.Show("Nuevo Producto", "El Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // funsion para poder buscar en los productos 
        // si coincide con los campos de de ClaveInterna
        // respecto al stock del producto en su campo de NoIdentificacion
        public void searchClavIntProd()
        {
            // preparamos el Query
            string search = $"SELECT Prod.ID, Prod.Nombre, Prod.ClaveInterna, Prod.Stock, Prod.CodigoBarras, Prod.Precio FROM Productos Prod WHERE Prod.IDUsuario = '{userId}' AND Prod.ClaveInterna = '{textBoxNoIdentificacion}' OR Prod.CodigoBarras = '{textBoxNoIdentificacion}'";
            // alamcenamos el resultado de la busqueda en dtClaveInterna
            dtClaveInterna = cn.CargarDatos(search);
            // si el resultado arroja al menos una fila
            if (dtClaveInterna.Rows.Count > 0)
            {
                resultadoSearchNoIdentificacion = 1; // busqueda positiva
                //MessageBox.Show("Encontrado", "El Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // si el resultado no arroja ninguna fila
            else if (dtClaveInterna.Rows.Count <= 0)
            {
                resultadoSearchNoIdentificacion = 1; // busqueda negativa
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
            // alamcenamos el resultado de la busqueda en dtClaveInterna
            dtCodBar = cn.CargarDatos(search);
            // si el resultado arroja al menos una fila
            if (dtCodBar.Rows.Count > 0)
            {
                resultadoSearchCodBar = 1; // busqueda positiva
                //MessageBox.Show("Codigo Bar ya en uso", "Este Codigo esta en uso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // si el resultado no arroja ninguna fila
            else if (dtCodBar.Rows.Count <= 0)
            {
                resultadoSearchCodBar = 1; // busqueda negativa
                //MessageBox.Show("Codigo Bar Disponible", "Este Codigo libre", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // funsion para cargar los datos XML
        // que provienen del archivo XML
        public void datosXML()
        {
            lblPosicionActualXML.Text = (index + 1).ToString();
            lblDescripcionXML.Text = ds.Conceptos[index].Descripcion;
            stockProdXML = int.Parse(ds.Conceptos[index].Cantidad); // convertimos la cantidad del Archivo XML para su posterior manipulacion
            lblCantXML.Text = stockProdXML.ToString();
            importe = float.Parse(ds.Conceptos[index].Importe); // convertimos el Importe del Archivo XML para su posterior manipulacion
            descuento = float.Parse(ds.Conceptos[index].Descuento); // convertimos el Descuento del Archivo XML para su posterior manipulacion
            cantidad = float.Parse(ds.Conceptos[index].Cantidad); // convertimos la cantidad del Archivo XML para su posterior manipulacion
            precioOriginalSinIVA = (importe - descuento) / cantidad; // Calculamos el precio Original Sin IVA (importe - descuento)/cantidad
            precioOriginalConIVA = precioOriginalSinIVA * (float)1.16; // Calculamos el precio Original Con IVA (precioOriginalSinIVA)*1.16
            lblPrecioOriginalXML.Text = precioOriginalConIVA.ToString("N2");
            importeReal = cantidad * precioOriginalConIVA; // calculamos importe real (cantidad * precioOriginalConIVA)
            lblImpXML.Text = importeReal.ToString("N2");
            ClaveInterna = ds.Conceptos[index].NoIdentificacion;
            lblNoIdentificacionXML.Text = ClaveInterna;
            PrecioRecomendado = precioOriginalConIVA * (float)1.60; // calculamos Precio Recomendado (precioOriginalConIVA)*1.60
            lblPrecioRecomendadoXML.Text = PrecioRecomendado.ToString("N2");
        }

        // funsion para cargar los datos del Producto
        // que provienen del stock del producto
        public void datosProductos()
        {
            idProducto = dtProductos.Rows[0]["ID"].ToString();
            txtBoxDescripcionProd.Text = dtProductos.Rows[0]["Nombre"].ToString();
            txtBoxClaveInternaProd.Text = dtProductos.Rows[0]["ClaveInterna"].ToString();
            stockProd = int.Parse(dtProductos.Rows[0]["Stock"].ToString()); // almacenamos el Stock del Producto en stockProd para su posterior manipulacion
            lblStockProd.Text = stockProd.ToString();
            lblCodigoBarrasProd.Text = dtProductos.Rows[0]["CodigoBarras"].ToString();
            lblPrecioRecomendadoProd.Text = lblPrecioRecomendadoXML.Text;
            PrecioProd = float.Parse(dtProductos.Rows[0]["Precio"].ToString()); // almacenamos el Precio del Producto en PrecioProd para su posterior manipulacion
            txtBoxPrecioProd.Text = PrecioProd.ToString("N2");
        }

        // funsion para hacer la lectura del Archivo XML 
        public void RecorrerXML()
        {
            // variable para saber cuantos concepctos tiene el XML
            int totalRegistroXML;
            // almacenamos el total de conceptos del XML
            totalRegistroXML = ds.Conceptos.Count();
            // alamcenamos el valor de la etiqueta lblPosicionActuakXML
            // nos ayudara para saber en que concepto va del XML
            index = int.Parse(lblPosicionActualXML.Text);
            // comparamos la posicion actual y vemo si es igual al total de conceptos del XML
            if (index == totalRegistroXML)
            {
                //MessageBox.Show("Final del Archivo XML,\nrecorrido con exito", "Archivo XML recorrido en totalidad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // cerramos la ventana de Actualizar los datos
            }
            // comparamos si la etiqueta lblPosicionActualXML es igual al número 0
            else if (lblPosicionActualXML.Text == "0")
            {
                datosXML(); // llamamos la funsion datosXML para llenar datos que tiene el XML
                searchProd(); // buscamos el producto a ver si ya esta en el Stock
                // si el resultado es negativo
                if (resultadoSearch <= 0)
                {
                   // ToDo si es que no hay producto registrado
                }
                // si el resultado es positivo
                else if (resultadoSearch >= 1)
                {
                    datosProductos(); // mostramos los datos del producto en el stock
                }
                index++; // aumentamos en uno la variable index
            }
            else if(index <= ds.Conceptos.Count())
            {
                datosXML(); // llamamos la funsion datosXML para llenar datos que tiene el XML
                searchProd(); // buscamos el producto a ver si ya esta en el Stock
                // si el resultado es negativo
                if (resultadoSearch <= 0)
                {
                    // ToDo si es que no hay producto registrado
                }
                // si el resultado es positivo
                else if (resultadoSearch >= 1)
                {
                    datosProductos(); // mostramos los datos del producto en el stock
                }
                index++; // aumentamos en uno la variable index
            }
        }

        // funsion para comprobar que el precio del stock sea
        // mayor o igual al que esta sugerido por el XML
        public void comprobarPrecioMayorIgualRecomendado()
        {
            PrecioProd = float.Parse(txtBoxPrecioProd.Text); // almacenamos el precio que tiene la caja de texto
            PrecioProdToCompare = float.Parse(lblPrecioRecomendadoProd.Text); // almacenamos el precio sugerido para hacer la comparacion
            if (PrecioProd < PrecioProdToCompare) // comparamos si el precio es menor
            {
                // muestra un mensaje que el precio en stock es menor que el suegerido
                dialogResult = MessageBox.Show("El Precio del producto en el Stock,\nes Menor que el precio Recomendado\n\n\tDESEA CORREGIRLA...", "El precio del Producto es menor...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes) // si el usuario selecciona que si quiere modificarlo
                {
                    resultadoCambioPrecio = 1; // la variable la ponemos en valor 1 indicando que se hizo el cambio de precio
                    txtBoxPrecioProd.Text = PrecioProdToCompare.ToString();
                    txtBoxPrecioProd.Focus(); // mandamos el cursor al textBox para el cambio de precio del Stock
                    PrecioProd = float.Parse(txtBoxPrecioProd.Text); // actualizamos el precio de la caja de texto
                }
                else if (dialogResult == DialogResult.No) // si el usario selecciona que no quiere modificarlo
                {
                    resultadoCambioPrecio = 1; // la variable la ponemos en valor 0 indicando que no se hizo el cambio de precio
                }
            }
        }

        // funsion para ver cuanto aumentario el stock 
        public void verNvoStock()
        {
            totalProd = stockProd + stockProdXML; // realizamos el caulculo del nvo stock 
            lblStockProd.Text = totalProd.ToString(); // mostramos el nvo stock del producto
        }

        public void ActualizarStock()
        {
            int resultadoConsulta;
            NombreProd = txtBoxDescripcionProd.Text;
            if (resultadoSearchProd==1)
            {
                query = $"UPDATE Productos SET Nombre = '{NombreProd}', Stock = '{totalProd}', ClaveInterna = '{textBoxNoIdentificacion}', Precio = '{PrecioProd}' WHERE ID = '{idProducto}'";
                resultadoConsulta = cn.EjecutarConsulta(query);
                if (resultadoConsulta == 1)
                {
                    //MessageBox.Show("Se Acualizo el producto","Estado de Actualizacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //MessageBox.Show("se actualizo mas" + resultadoConsulta);
                }
            }
            else if (resultadoSearchProd==0)
            {
                //query = $"UPDATE Productos SET Nombre = '{NombreProd}', Stock = '{totalProd}', ClaveInterna = '{textBoxNoIdentificacion}', Precio = '{PrecioProd}' WHERE ID = '{idProducto}'";
                //resultadoConsulta = cn.EjecutarConsulta(query);
                //if (resultadoConsulta == 1)
                //{
                //    //MessageBox.Show("Se Acualizo el producto","Estado de Actualizacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    //MessageBox.Show("se actualizo mas" + resultadoConsulta);
                //}
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comprobarPrecioMayorIgualRecomendado(); // Llamamos la funsion para comparar el precio del producto con el sugerido
            verNvoStock(); // funsion para ver el nvo stock
            textBoxNoIdentificacion = txtBoxClaveInternaProd.Text; // tomamos el valor del TextBox para hacer la comparacion
            if (NoClaveInterna != textBoxNoIdentificacion) // si son diferentes los datos osea hubo un cambio
            {
                //MessageBox.Show("No son Iguales","los textos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                searchClavIntProd(); // hacemos la busqueda que no se repita en CalveInterna
                searchCodBar();  // hacemos la busqueda que no se repita en CodigoBarra
            }
            else
            {
                // ToDo 
                //MessageBox.Show("No esta dentro de lo planeado", "Algo salio Mal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // realizamos la actualizacion si se cumplen que se hubieran hecho las tres validaciones
            ActualizarStock(); // realizamos la actualizacion
            RecorrerXML(); // recorrer el archivo XML
        }

        public AgregarStockXML()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecorrerXML(); // recorrer el archivo XML
        }

        public void leerXMLFile()
        {
            // ejecutar el openFileDialog
            using (f = new OpenFileDialog())
            {
                // poner el setup de archivos ver etc
                f.DefaultExt = "xml";
                f.Filter = "Archivo XML (*.xml) | *.xml";
                f.Title = "Selecciona tu archivo XML";

                // si se le da clic en el boton Ok
                if (f.ShowDialog() == DialogResult.OK)
                {
                    // si es que el usuario pone otra extension no permitida se le pone este mensaje
                    if (!String.Equals(Path.GetExtension(f.FileName), ".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("El tipo de archivo seleccionado no es soportado en esta aplicación,\nDebes seleccionar un archivo con extension XML",
                                        "Tipo de Archivo Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // si es que todo esta bien se le da el procesamiento del archivo
                    else
                    {
                        //MessageBox.Show("El archivo seleccionado,\nse esta Procesando", "Tipo de Archivo Valido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        serial = new XmlSerializer(typeof(Comprobante));
                        fs = new FileStream(f.FileName, FileMode.Open, FileAccess.Read);
                        ds = (Comprobante)serial.Deserialize(fs);
                        if (ds.Receptor.Rfc == rfc)
                        {
                            cantProductos = 0;
                            OcultarPanelCarga();
                            btnLoadXML.Hide();
                            MostrarPanelRegistro();
                            for (index = 0; index < ds.Conceptos.Count(); index++)
                            {
                                cantProductos++;
                            }
                            lblLargodelXML.Text = cantProductos.ToString();
                            lblPosicionActualXML.Text = "0";
                            RecorrerXML();
                        }
                        else
                        {
                            MessageBox.Show("El archivo XML seleccionado no tiene la tu RFC,\nDebes seleccionar un archivo XML con tu RFC",
                                    "XML no contiene tu R.F.C.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MostrarPanelCarga();
                            btnLoadXML.Show();
                        }
                    }
                }
                else
                {
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
