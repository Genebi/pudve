using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BarcodeLib;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PuntoDeVentaV2
{
    public partial class CodeBarMake : Form
    {
        // variables internas para el manejo de datos para la etiqueta
        public static string NombreProdFinal;
        public static string PrecioProdFinal;
        public static string CodigoBarProdFinal;

        // variables publicas para que ellas obtengan los datos de la forma de productos
        public string NombreProd { set; get; }
        public string PrecioProd { set; get; }
        public string CodigoBarProd { set; get; }

        // Path para guardar la imagen del codigo de barras y el pdf generado con la etiqueta del codigo de barras
        string saveDirectoryImg = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BarCode\";
        string saveDirectoryPdf = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\PdfCode\";

        // Nombre completo para el archivo
        string FileName, FileNamePng;

        public void CodigoBarras()
        {
            var source = NombreProdFinal + " - " + CodigoBarProdFinal;
            var replacement = source.Replace('/', '_').Replace('\\', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('\"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_').Replace('-', '_').Replace(' ', '_').Replace("\r\n", "");
            FileNamePng = saveDirectoryImg + replacement;
            BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();   // declaramos el objeto Codigo para hacer la imagen 

            try
            {
                // hacemos que en el panel se ponga de fondo el codigo de barras generado en imagen
                //panelResultado.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE128, CodigoBarProdFinal, Color.Black, Color.White, 118, 23);
                panelResultado.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE128, CodigoBarProdFinal, Color.Black, Color.White, 400, 100);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Convertir Imagen: " + ex.Message.ToString(), "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            // clonamos el fondo del panel para despues guardarlo
            System.Drawing.Image imgFinal = (System.Drawing.Image)panelResultado.BackgroundImage.Clone();
            SaveFileDialog CajaDeDialogoGuardar = new SaveFileDialog();     // declaramos un objeto de SaveFileDialog

            CajaDeDialogoGuardar.AddExtension = true;   // le ponemos en true la propiedad de extension de archivos
            CajaDeDialogoGuardar.Filter = "Image PNG (*.png)|*.png";    // indicamos que tipo de archivo seran los permitidos
            CajaDeDialogoGuardar.FileName = FileNamePng + ".png";     //le indicamos la ruta y nombre del archivo PNG
            if (!Directory.Exists(saveDirectoryImg))    // si el directorio destino no existe el sistema lo va crear
            {
                Directory.CreateDirectory(saveDirectoryImg);    // creamos el directorio donde van guardados las imagenes
            }
            if (CajaDeDialogoGuardar.CheckFileExists)   // si el archivo de PNG existe
            {
                System.IO.File.Delete(FileNamePng + ".png");      // borramos el archivo PNG
                imgFinal.Save(CajaDeDialogoGuardar.FileName, ImageFormat.Png);      // guardamos el archivo PNG modificado
            }
            else if (!CajaDeDialogoGuardar.CheckFileExists)     // si el archivo de PNG no existe
            {
                imgFinal.Save(CajaDeDialogoGuardar.FileName, ImageFormat.Png);      // guardamos el archivo PNG
            }
            imgFinal.Dispose();     // liberamos el objeto de imagen 
        }

        public void PdfFile()
        {
            var source = NombreProdFinal + " - " + CodigoBarProdFinal + ".pdf";
            var replacement = source.Replace('/', '_').Replace('\\', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('\"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_').Replace('-', '_').Replace(' ', '_');
            FileName = saveDirectoryPdf + replacement;
            if (!Directory.Exists(saveDirectoryPdf))    // si el directorio destino no existe el sistema lo va crear
            {
                Directory.CreateDirectory(saveDirectoryPdf);    // creamos el directorio donde van guardados los PDF
            }
            // creamos un objeto de tipo Documento
            // el cual usara el iTextSharp con la medida de una pagina A10
            // y hacemos rotar para que se ponga en pocision horizontal
            // todo se empezara a dibujar en la posicion 0 px, 0 px
            Document doc = new Document(iTextSharp.text.PageSize.A10.Rotate(), 0, 0, 0, 0);
            // creamos el objeto PdfWriter el cual usara el objeto doc
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(FileName, FileMode.Create));
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);    // declaramos un objeto de tipo fuente para el texto
            iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);      // aqui indicamos al iTextSharp use la fuente declarada

            doc.Open();     // comenzamos a escribir el documento de PDF
            Paragraph paragraph = new Paragraph(new Chunk("www.pudve.com\nPunto de Venta Gratuito\n" + NombreProdFinal, font));  // declaramos la etiqueta
            paragraph.Leading = 8;      // le damos el interlineado del texto
            paragraph.Alignment = Element.ALIGN_CENTER;     // centramos el texto
            doc.Add(paragraph);     // lo agregamos la etiqueta al documento PDF
                                    // Declaramos el objeto PNG 
            iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance(FileNamePng + ".png");
            PNG.Alignment = iTextSharp.text.Image.ALIGN_CENTER;     // centramos el archivo de PNG
            PNG.ScaleToFit(doc.PageSize);
            doc.Add(PNG);       // agregamos la imagen al Documento PDF
            Paragraph paragraph1 = new Paragraph(new Chunk(CodigoBarProdFinal + " - $" + PrecioProdFinal, font));   // declaramos una etiqueta
            paragraph1.Leading = 8;     // le damos el interlineado del texto
            paragraph1.Alignment = Element.ALIGN_CENTER;    // centramos el texto
            doc.Add(paragraph1);    // agregamos la etiqueta al documento PDF
            doc.AddAuthor("www.pudve.com");     // agregamos el metadato de quien es el Autor
            doc.AddCreator("PUDVE");            // egregamos el metadato de quien es el Creador
            doc.AddCreationDate();              // agregamos el metadato de la fecha de creacion
            doc.Close();            // cerramos el documento y la escritura y asi guardarlo
                                    // 
            if (System.IO.File.Exists(FileName))
            {
                System.Diagnostics.Process.Start(FileName);
            }
        }

        // metodo haecho para que muestre la vista previa de como seria la etiqueta de codigo de barras
        public void cargarDatos()
        {
            try     // intentar realizar el proceso
            {
                NombreProdFinal = NombreProd;   // almacenamos datos alfanumericos
                PrecioProdFinal = PrecioProd;   // almacenamos datos alfanumericos
                CodigoBarProdFinal = CodigoBarProd;   // almacenamos datos alfanumericos
                lblNombreProd.Text = NombreProdFinal;   // ponemos el texto del Label con el nombre del producto
                lblCodBarPrecio.Text = CodigoBarProdFinal + " - $" + PrecioProdFinal;   // ponemos el texto del Label con el Codigo de barras del producto

                /* **********************************************************
                *       Iniciamos la seccion para guardar la imagen         *
                ************************************************************/
                CodigoBarras();
                /* ********************************************************** 
                *       Terminamos la seccion para guardar la imagen        *
                ************************************************************/

                /************************************************************
                *       Iniciamos la seccion de generar el archivo PDF      *
                ************************************************************/
                PdfFile();
                /************************************************************
                *       Terminamos la seccion de generar el archivo PDF     *
                ************************************************************/
            }
            catch (Exception ex)        // si es que el sistema no puede realizar el proceso
            {
                // muestra mensaje que tal ves no tengas permiso sobre ese Path en especifico
                MessageBox.Show("Se ha producido un error al intentar convertir el texto a PDF: " + ex.Message, "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public CodeBarMake()
        {
            InitializeComponent();
        }

        private void CodeBarMake_Load(object sender, EventArgs e)
        {
            cargarDatos();  // llamamos el metodo para crear el codigo de barras
        }
    }
}
