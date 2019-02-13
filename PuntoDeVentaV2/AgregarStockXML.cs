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

        OpenFileDialog f;

        Conexion cn = new Conexion();

        // declaramos la variable que almacenara el valor de userNickName
        public string userName;
        public string passwordUser;
        public string userId;

        // variables para poder hacer las consulta y actualizacion
        string buscar;

        // variables para poder hacer el recorrido y asignacion de los valores que estan el base de datos
        int index,cantProductos, resultadoSearch;
        public DataTable dt, dtProductos;
        float importe, descuento, cantidad, precioOriginalSinIVA, precioOriginalConIVA, PrecioRecomendado, importeReal;
        string ClaveInterna;

        // variables para poder tomar el valor de los TxtBox y tambien hacer las actualizaciones
        // del valor que proviene de la base de datos ó tambien actualizar la Base de Datos
        string id;
        string rfc;

        XmlSerializer serial;
        FileStream fs;
        Comprobante ds;

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

        public void OcultarPanelCarga()
        {
            panel1.Hide();
        }

        public void MostrarPanelCarga()
        {
            panel1.Show();
            this.Size = new Size(500, 450);
            this.CenterToScreen();
        }

        public void OcultarPanelRegistro()
        {
            panel17.Hide();
            panel2.Hide();
            panel12.Hide();
            button1.Hide();
        }

        public void MostrarPanelRegistro()
        {
            panel17.Show();
            panel2.Show();
            panel12.Show();
            button1.Show();
            this.Size = new Size(950, 640);
            this.CenterToScreen();
        }

        public void limpiarLblProd()
        {
            lblDescripcionProd.Text = "";
            lblClaveInternaProd.Text = "";
            lblStockProd.Text = "";
            lblCodigoBarrasProd.Text = "";
            txtBoxPrecioProd.Text = "";
        }

        public void searchProd()
        {
            string search = $"SELECT Prod.Nombre, Prod.ClaveInterna, Prod.Stock, Prod.CodigoBarras, Prod.Precio FROM Productos Prod WHERE Prod.IDUsuario = '{userId}' AND Prod.ClaveInterna = '{ClaveInterna}'";
            dtProductos = cn.CargarDatos(search);
            if (dtProductos.Rows.Count > 0)
            {
                resultadoSearch = 1;
            }
            else if (dtProductos.Rows.Count<=0)
            {
                resultadoSearch = 0;
                limpiarLblProd();
            }
        }

        public void RecorrerXML()
        {
            int totalRegistroXML;
            totalRegistroXML = ds.Conceptos.Count();
            index = int.Parse(lblPosicionActualXML.Text);
            if (index == totalRegistroXML)
            {
                MessageBox.Show("Final del Archivo XML,\nrecorrido con exito", "Archivo XML recorrido en totalidad", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (lblPosicionActualXML.Text == "0")
            {   
                lblPosicionActualXML.Text = (index+1).ToString();
                lblDescripcionXML.Text = ds.Conceptos[index].Descripcion;
                lblCantXML.Text = ds.Conceptos[index].Cantidad;
                importe = float.Parse(ds.Conceptos[index].Importe);
                descuento = float.Parse(ds.Conceptos[index].Descuento);
                cantidad = float.Parse(ds.Conceptos[index].Cantidad);
                precioOriginalSinIVA = (importe - descuento) / cantidad;
                precioOriginalConIVA = precioOriginalSinIVA * (float)1.16;
                lblPrecioOriginalXML.Text = precioOriginalConIVA.ToString("N2");
                importeReal = cantidad * precioOriginalConIVA;
                lblImpXML.Text = importeReal.ToString("N2");
                ClaveInterna = ds.Conceptos[index].NoIdentificacion;
                lblNoIdentificacionXML.Text = ClaveInterna;
                PrecioRecomendado = precioOriginalConIVA * (float)1.60;
                lblPrecioRecomendadoXML.Text = PrecioRecomendado.ToString("N2");
                searchProd();
                if (resultadoSearch <= 0)
                {
                    DialogResult dialogResult = MessageBox.Show("No existe el producto en el Stock,\nDesea Agregarlo a su lista de Productos", "No existe Producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //do something
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }
                }
                else if (resultadoSearch>=1)
                {
                    lblDescripcionProd.Text = dtProductos.Rows[0]["Nombre"].ToString();
                    lblClaveInternaProd.Text = dtProductos.Rows[0]["ClaveInterna"].ToString();
                    lblStockProd.Text = dtProductos.Rows[0]["Stock"].ToString();
                    lblCodigoBarrasProd.Text = dtProductos.Rows[0]["CodigoBarras"].ToString();
                    txtBoxPrecioProd.Text = dtProductos.Rows[0]["Precio"].ToString();
                    //MessageBox.Show("Producto existente,\nen su Stock", "Prodcuto ya Registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);         
                }
                index++;
            }
            else if(index <= ds.Conceptos.Count())
            {
                lblPosicionActualXML.Text = (index + 1).ToString();
                lblDescripcionXML.Text = ds.Conceptos[index].Descripcion;
                lblCantXML.Text = ds.Conceptos[index].Cantidad;
                importe = float.Parse(ds.Conceptos[index].Importe);
                descuento = float.Parse(ds.Conceptos[index].Descuento);
                cantidad = float.Parse(ds.Conceptos[index].Cantidad);
                precioOriginalSinIVA = (importe - descuento) / cantidad;
                precioOriginalConIVA = precioOriginalSinIVA * (float)1.16;
                lblPrecioOriginalXML.Text = precioOriginalConIVA.ToString("N2");
                importeReal = cantidad * precioOriginalConIVA;
                lblImpXML.Text = importeReal.ToString("N2");
                ClaveInterna = ds.Conceptos[index].NoIdentificacion;
                lblNoIdentificacionXML.Text = ClaveInterna;
                PrecioRecomendado = precioOriginalConIVA * (float)1.60;
                lblPrecioRecomendadoXML.Text = PrecioRecomendado.ToString("N2");
                searchProd();
                if (resultadoSearch <= 0)
                {
                    DialogResult dialogResult = MessageBox.Show("No existe el producto en el Stock,\nDesea Agregarlo a su lista de Productos", "No existe Producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //do something
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }
                }
                else if (resultadoSearch >= 1)
                {
                    lblDescripcionProd.Text = dtProductos.Rows[0]["Nombre"].ToString();
                    lblClaveInternaProd.Text = dtProductos.Rows[0]["ClaveInterna"].ToString();
                    lblStockProd.Text = dtProductos.Rows[0]["Stock"].ToString();
                    lblCodigoBarrasProd.Text = dtProductos.Rows[0]["CodigoBarras"].ToString();
                    txtBoxPrecioProd.Text = dtProductos.Rows[0]["Precio"].ToString();
                    //MessageBox.Show("Producto existente,\nen su Stock", "Prodcuto ya Registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }
                index++;
            }
            
        }

        public AgregarStockXML()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecorrerXML();
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
            groupBox5.BackColor = Color.FromArgb(130, 130, 130);

            // asignamos el valor de userName que sea
            // el valor que tiene userNickUsuario en el formulario Principal
            userId = FormPrincipal.userID.ToString();
            userName = FormPrincipal.userNickName;
            passwordUser = FormPrincipal.userPass;

            consulta();
            MostrarPanelCarga();
            OcultarPanelRegistro();
            //panel1.Hide();
        }

        private void btnLoadXML_Click_1(object sender, EventArgs e)
        {
            leerXMLFile();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            leerXMLFile();
        }

        private void label11_Enter(object sender, EventArgs e)
        {
            //label11.BackColor = Color.FromArgb(255, 0, 0);
            label11.ForeColor = Color.FromArgb(0, 140, 255);
        }

        private void label11_Leave(object sender, EventArgs e)
        {
            //label11.BackColor = Color.FromArgb(130, 130, 130);
            label11.ForeColor = Color.FromArgb(0, 0, 0);
        }

        private void AgregarStockXML_FormClosing(object sender, FormClosingEventArgs e)
        {
            MostrarPanelCarga();
            btnLoadXML.Show();
        }
    }
}
