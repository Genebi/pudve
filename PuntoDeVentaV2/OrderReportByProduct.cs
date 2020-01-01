using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace PuntoDeVentaV2
{
    public partial class OrderReportByProduct : Form
    {
        public OrderReportByProduct()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            // idicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\PDFs\prueba.pdf", FileMode.Create));
            // Nota esto no sera visible en el documento
            doc.AddTitle("Prueba");
            doc.AddCreator("PUDVE");
            // Abrimos el archivo
            doc.Open();
            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Escribimos el encabezamiento en el documento
            doc.Add(new Paragraph("Parte de averías diario"));
            doc.Add(Chunk.NEWLINE);

            PdfPTable table = new PdfPTable(4);
            table.TotalWidth = 400f;
            table.LockedWidth = true;

            PdfPCell header = new PdfPCell(new Phrase("CodAveria:"));
            header.Colspan = 2;
            table.AddCell(header);
            table.AddCell("Hora Incio:");
            table.AddCell("Duración:");

            PdfPCell[] celdas = new PdfPCell[]
            {
                new PdfPCell(new Phrase("CodAveria: ")),
                new PdfPCell(new Phrase("Hara Inicio:")),
                new PdfPCell(new Phrase("Duracion:")),
            };

            PdfPRow fila1 = new PdfPRow(celdas);
            table.Rows.Add(fila1);

            // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
            try
            {
                doc.Add(table);
                doc.Close();
                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
            
            MessageBox.Show("Informe creado");
            this.Close();
        }
    }
}
