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
    public partial class PedidoPorProducto : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        public string idProductoFinal { set; get; }
        
        string idProducto = string.Empty;
        
        public PedidoPorProducto()
        {
            InitializeComponent();
        }

        private void PedidoPorProducto_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void cargarDatos()
        {
            idProducto = idProductoFinal;
        }

        private void btnDoPrintOrderList_Click(object sender, EventArgs e)
        {
            doPrintOrderListByProduct();
        }

        private void doPrintOrderListByProduct()
        {
            string carpeta = @"C:\PDFs\";

            // Creamos el documento con el tamaño de página tradicional
            using (Document doc = new Document(PageSize.LETTER.Rotate(), 72, 72, 72, 72))
            {
                try
                {
                    if (!(Directory.Exists(carpeta)))
                    {
                        Directory.CreateDirectory(carpeta);
                    }
                    if (Directory.Exists(carpeta))
                    {
                        // Indicamos donde vamos a guardar el documento
                        using (PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\PDFs\prueba.pdf", FileMode.Create)))
                        {
                            //Nota: Esto no será visible en el documento
                            doc.AddTitle("Prueba");
                            doc.AddCreator("SIFO.com.mx");

                            // Abrimos el archivo
                            doc.Open();

                            // Creamos el tipo de Font que vamos utilizar
                            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                            var productSearch = cn.BuscarProducto(Convert.ToInt32(idProducto), FormPrincipal.userID);
                            
                            // Escribimos el encabezamiento en el documento
                            doc.Add(new Paragraph("Reporte de Pedido de: " + productSearch[1].ToString()));
                            doc.Add(Chunk.NEWLINE);

                            PdfPTable table = new PdfPTable(9);
                            table.WidthPercentage = 100;
                            PdfPCell header1 = new PdfPCell(new Phrase("Producto:"));
                            header1.BackgroundColor = new iTextSharp.text.BaseColor(0, 153, 204);
                            table.AddCell(header1).HorizontalAlignment=PdfPCell.ALIGN_CENTER;

                            PdfPCell header2 = new PdfPCell(new Phrase("Stock Actual:"));
                            header2.BackgroundColor = new iTextSharp.text.BaseColor(0, 153, 204);
                            table.AddCell(header2).HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                            PdfPCell header3 = new PdfPCell(new Phrase("Cantidad a Pedir:"));
                            header3.BackgroundColor = new iTextSharp.text.BaseColor(0, 153, 204);
                            table.AddCell(header3).HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                            PdfPCell header4 = new PdfPCell(new Phrase("Proveedor:"));
                            header4.BackgroundColor = new iTextSharp.text.BaseColor(0, 153, 204);
                            table.AddCell(header4).HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                            PdfPCell header5 = new PdfPCell(new Phrase("Precio Compra:"));
                            header5.BackgroundColor = new iTextSharp.text.BaseColor(0, 153, 204);
                            table.AddCell(header5).HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                            PdfPCell header6 = new PdfPCell(new Phrase("Precio Venta:"));
                            header6.BackgroundColor = new iTextSharp.text.BaseColor(0, 153, 204);
                            table.AddCell(header6).HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                            PdfPCell header7 = new PdfPCell(new Phrase("Código de Barras:"));
                            header7.BackgroundColor = new iTextSharp.text.BaseColor(0, 153, 204);
                            table.AddCell(header7).HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                            PdfPCell header8 = new PdfPCell(new Phrase("Clave Interna:"));
                            header8.BackgroundColor = new iTextSharp.text.BaseColor(0, 153, 204);
                            table.AddCell(header8).HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                            PdfPCell header9 = new PdfPCell(new Phrase("Código de Barras Extra"));
                            header9.BackgroundColor = new iTextSharp.text.BaseColor(0, 153, 204);
                            table.AddCell(header9).HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                            table.AddCell(productSearch[1].ToString());        // Nombre del Producto
                            table.AddCell(productSearch[4].ToString());        // Stock Actual
                            int pedido = 0;
                            if (Convert.ToInt32(productSearch[10].ToString()) <= Convert.ToInt32(productSearch[4].ToString()))
                            {
                                pedido = 0;
                            }
                            else if (Convert.ToInt32(productSearch[10].ToString()) > Convert.ToInt32(productSearch[4].ToString()))
                            {
                                pedido = Convert.ToInt32(productSearch[8].ToString()) - Convert.ToInt32(productSearch[4].ToString());
                            }
                            table.AddCell(pedido.ToString());                       // Cantidad a Pedir
                            var DetallesProveedor = mb.DetallesProducto(Convert.ToInt32(idProducto), FormPrincipal.userID);
                            table.AddCell(DetallesProveedor[2].ToString());        // Proveedor
                            double precioCompra = Convert.ToDouble(productSearch[2].ToString()) / 1.60;
                            table.AddCell("$ " + precioCompra.ToString("N2"));      // Precio Compra
                            table.AddCell("$ " + productSearch[2].ToString());      // Precio Venta
                            table.AddCell(productSearch[7].ToString());             // Codigo de Barras
                            table.AddCell(productSearch[6].ToString());             // Calve Interna
                            var CodigoBarraExtra = mb.ObtenerCodigoBarrasExtras(Convert.ToInt32(idProducto));
                            string strCodBarExt = string.Empty;
                            for (int i = 0; i < CodigoBarraExtra.Length; i++)
                            {
                                strCodBarExt += CodigoBarraExtra[i].ToString() + " ";
                            }
                            table.AddCell(strCodBarExt.TrimEnd());        // Codigo de Barras Extra

                            doc.Add(table);
                            doc.Close();
                            writer.Close();
                            //MessageBox.Show("Informe creado");
                        }
                        string rutaArchivo = @"C:\PDFs\prueba.pdf";

                        VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
                        vr.ShowDialog();

                        this.Close();
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Error al Intentar Generar el Reporte en PDF:\n" + ex.Message.ToString(),
                                    "Error al Generar PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
