using iTextSharp.text;
using iTextSharp.text.pdf;
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
    public partial class TagMake : Form
    {
        // variables internas para el manejo de datos para la etiqueta
        public static string NombreProdFinal;
        public static float PrecioProdFinal;
        public static string CodigoBarProdFinal;

        // variables publicas para que ellas obtengan los datos de la forma de productos
        public string NombreProd { set; get; }
        public float PrecioProd { set; get; }
        public string CodigoBarProd { set; get; }

        // Path para guardar el pdf generado con la etiqueta del Producto
        string saveDirectoryPdf = Properties.Settings.Default.rutaDirectorio + @"\PdfTag\";

        // Nombre completo para el archivo
        string FileName;

        public void PdfFile()
        {
            FileName = saveDirectoryPdf + NombreProdFinal + " - " + CodigoBarProdFinal + ".pdf";
            if (!Directory.Exists(saveDirectoryPdf))
            {
                Directory.CreateDirectory(saveDirectoryPdf);
            }
            Document doc = new Document(iTextSharp.text.PageSize.A10.Rotate(),0,0,0,0);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(FileName, FileMode.Create));
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            BaseFont bf1 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font font1 = new iTextSharp.text.Font(bf1, 13, iTextSharp.text.Font.ITALIC);

            doc.Open();

            Paragraph paragraph1 = new Paragraph(new Chunk("www.pudve.com\nPunto de Venta Gratuito\n", font));
            paragraph1.Leading = 8;
            paragraph1.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph1);

            Paragraph paragraph2 = new Paragraph(new Chunk(" ", font));
            paragraph2.Leading = 8;
            paragraph2.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph2);

            Paragraph paragraph3 = new Paragraph(new Chunk(NombreProdFinal, font1));
            paragraph3.Leading = 8;
            paragraph3.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph3);

            Paragraph paragraph4 = new Paragraph(new Chunk("Cod Bar: " + CodigoBarProdFinal, font));
            paragraph4.Leading = 8;
            paragraph4.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph4);

            Paragraph paragraph5 = new Paragraph(new Chunk("Precio: $" + PrecioProdFinal.ToString("N2"), font));
            paragraph5.Leading = 8;
            paragraph5.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph5);

            doc.AddAuthor("www.pudve.com");
            doc.AddCreator("PUDVE");
            doc.AddCreationDate();

            doc.Close();

            if (System.IO.File.Exists(FileName))
            {
                System.Diagnostics.Process.Start(FileName);
            }
        }

        // metodo haecho para que muestre la vista previa de como seria la etiqueta de codigo de barras
        public void cargarDatos()
        {
            try // intentamos hacer el proceso
            {
                NombreProdFinal = NombreProd;
                PrecioProdFinal = PrecioProd;
                CodigoBarProdFinal = CodigoBarProd;
                lblNombreProd.Text = NombreProdFinal;
                lblCodBarPrecio.Text = CodigoBarProdFinal + " - $" + PrecioProdFinal;
                /************************************************************
                *       Iniciamos la seccion de generar el archivo PDF      *
                ************************************************************/
                PdfFile();
                /************************************************************
                *       Terminamos la seccion de generar el archivo PDF     *
                ************************************************************/
            }
            catch (Exception ex)
            {
                // muestra mensaje que tal ves no tengas permiso sobre ese Path en especifico
                MessageBox.Show("Se ha producido un error al intentar convertir el texto a PDF: " + ex.Message, "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public TagMake()
        {
            InitializeComponent();
        }

        private void TagMake_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }
    }
}
