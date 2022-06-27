using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2.ReportesImpresion
{
    public class EnviarImprimir : IDisposable
    {
        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        public void Dispose()
        {
            if (!m_streams.Equals(null))
            {
                foreach (Stream stream in m_streams)
                {
                    stream.Close();
                }
                m_streams = null;
            }
        }

        public void Imprime(LocalReport rdlc)
        {
            Export(rdlc);
            Print();
        }

        private void Export(LocalReport report)
        {
            string deviceInfo = @"<DeviceInfo><OutputFormat>EMF</OutputFormat><PageWith>8in</PageWith><PageHeight>10in</PageHeight><MarginTop>0cm</MarginTop><MarginLeft>0cm</MarginLeft><MarginRight>0cm</MarginRight><MarginBottom>0cm</MarginBottom></DeviceInfo>";

            Warning[] warnings;
            m_streams = new List<Stream>();

            // Rennderizamos el reporte
            report.Render("Image", deviceInfo, CreateSream, out warnings);

            foreach (Stream stream in m_streams)
            {
                stream.Position = 0;
            }
        }

        private Stream CreateSream(string name, string extension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);

            return stream;
        }

        private void Print()
        {
            PrintDocument printDoc;

            string printerName = ImpresoraPredeterminada();

            if (m_streams == null || m_streams.Count == 0)
            {
                throw new Exception("Error: no hay datos que imprimir.");
            }

            printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printerName;

            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception(String.Format("No puedo encontrar la impresora \"{0}\".", printerName));
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        private string ImpresoraPredeterminada()
        {
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                PrinterSettings a = new PrinterSettings();
                a.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();

                if (a.IsDefaultPrinter)
                {
                    return PrinterSettings.InstalledPrinters[i].ToString();
                }
            }
            return "";
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height
            );

            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            ev.Graphics.DrawImage(pageImage, adjustedRect);

            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
    }
}
