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
        public static string NombreProdFinal;
        public static string PrecioProdFinal;
        public static string CodigoBarProdFinal;

        public string NombreProd { set; get; }
        public string PrecioProd { set; get; }
        public string CodigoBarProd { set; get; }

        string saveDirectoryImg = Properties.Settings.Default.rutaDirectorio + @"\BarCode\";
        string saveDirectoryPdf = Properties.Settings.Default.rutaDirectorio + @"\PdfCode\";

        public void cargarDatos()
        {
            try
            {
                BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();

                NombreProdFinal = NombreProd;
                PrecioProdFinal = PrecioProd;
                CodigoBarProdFinal = CodigoBarProd;
                lblNombreProd.Text = NombreProdFinal;
                lblCodBarPrecio.Text = CodigoBarProdFinal + " - $" + PrecioProdFinal;

                panelResultado.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE128, CodigoBarProdFinal, Color.Black, Color.White, 118, 23);

                System.Drawing.Image imgFinal = (System.Drawing.Image)panelResultado.BackgroundImage.Clone();
                SaveFileDialog CajaDeDialogoGuardar = new SaveFileDialog();

                CajaDeDialogoGuardar.AddExtension = true;
                CajaDeDialogoGuardar.Filter = "Image PNG (*.png)|*.png";
                CajaDeDialogoGuardar.FileName = saveDirectoryImg + NombreProdFinal + " " + CodigoBarProdFinal + ".png";
                if (!Directory.Exists(saveDirectoryImg))
                {
                    Directory.CreateDirectory(saveDirectoryImg);
                }
                if (CajaDeDialogoGuardar.CheckFileExists)
                {
                    System.IO.File.Delete(saveDirectoryImg + NombreProdFinal + " " + CodigoBarProdFinal + ".png");
                }
                else if (!CajaDeDialogoGuardar.CheckFileExists)
                {
                    imgFinal.Save(CajaDeDialogoGuardar.FileName, ImageFormat.Png);
                }
                imgFinal.Dispose();

                Document doc = new Document(iTextSharp.text.PageSize.A10.Rotate(), 0, 0, 0, 0);
                PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(saveDirectoryPdf + NombreProdFinal + " " + CodigoBarProdFinal + ".pdf", FileMode.Create));
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);

                doc.Open();
                Paragraph paragraph = new Paragraph(new Chunk("www.pudve.com\nPunto de Venta Gratuito\n"+ NombreProdFinal, font));
                paragraph.Leading = 8;
                paragraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(paragraph);
                iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance(saveDirectoryImg + NombreProdFinal + " " + CodigoBarProdFinal + ".png");
                PNG.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                doc.Add(PNG);
                Paragraph paragraph1 = new Paragraph(new Chunk(CodigoBarProdFinal + " - $" + PrecioProdFinal, font));
                paragraph1.Leading = 8;
                paragraph1.Alignment = Element.ALIGN_CENTER;
                doc.Add(paragraph1);
                doc.AddAuthor("www.pudve.com");
                doc.AddCreator("PUDVE");
                doc.AddCreationDate();
                doc.Close();
                if (System.IO.File.Exists(saveDirectoryPdf + NombreProdFinal + " " + CodigoBarProdFinal + ".pdf"))
                {
                    System.Diagnostics.Process.Start(saveDirectoryPdf + NombreProdFinal + " " + CodigoBarProdFinal + ".pdf");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error al intentar convertir el texto a PDF: " + ex.Message, "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public CodeBarMake()
        {
            InitializeComponent();
        }

        private void CodeBarMake_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }
    }
}
