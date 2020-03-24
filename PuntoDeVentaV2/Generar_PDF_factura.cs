using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PuntoDeVentaV2
{
    class Generar_PDF_factura
    {
        public static void generar_PDF(string nombre_xml)
        {

            // .........................................
            // .    Deserealiza el XML ya timbrado    .
            // .........................................


            Comprobante comprobante;
            string ruta_xml = @"C:\Archivos PUDVE\Facturas\" + nombre_xml + ".xml";
            
            XmlSerializer serializer = new XmlSerializer(typeof(Comprobante));

            // Desserealizar el xml
            using (StreamReader sr = new StreamReader(ruta_xml))
            {
                comprobante = (Comprobante)serializer.Deserialize(sr);

                // Dessearializar complementos
                foreach (var complementos in comprobante.Complemento)
                {
                    foreach(var complemento in complementos.Any)
                    {
                        if (complemento.Name.Contains("TimbreFiscalDigital"))
                        {
                            XmlSerializer serializer_complemento = new XmlSerializer(typeof(TimbreFiscalDigital));

                            using (var sr_c = new StringReader(complemento.OuterXml))
                            {
                                comprobante.timbre_fiscal_digital = (TimbreFiscalDigital)serializer_complemento.Deserialize(sr_c);
                            }
                        }

                        if (complemento.Name.Contains("Pagos"))
                        {
                            XmlSerializer serializer_complemento_pagos = new XmlSerializer(typeof(Pagos));

                            using (var sr_cp = new StringReader(complemento.OuterXml))
                            {
                                comprobante.cpagos = (Pagos)serializer_complemento_pagos.Deserialize(sr_cp);


                                foreach(var cpagos_pg in comprobante.cpagos.Pago)
                                {
                                    comprobante.cpagos.cpagos_pago = cpagos_pg;
                                    
                                    foreach(var cpagos_pg_docrel in comprobante.cpagos.cpagos_pago.DoctoRelacionado)
                                    {
                                        comprobante.cpagos.cpagos_pago_docrelacionado = cpagos_pg_docrel;
                                    }
                                }
                            }
                        }
                    }
                }
            }





            // .....................................................................
            // .    Inicia con la generación de la plantilla y conversión a PDF    .
            // .....................................................................

            string origen_pdf_temp = nombre_xml + ".pdf";
            string destino_pdf = @"C:\Archivos PUDVE\Facturas\" + nombre_xml + ".pdf";

            string ruta = AppDomain.CurrentDomain.BaseDirectory + "/";
            // Creación de un arhivo html temporal
            string ruta_html_temp = ruta + "facturahtml.html";
            // Plantilla que contiene el acomodo del PDF
            string ruta_plantilla_html = ruta + "Plantilla_factura.html";
            string s_html = GetStringOfFile(ruta_plantilla_html);
            string result_html = "";
 
            result_html = RazorEngine.Razor.Parse(s_html, comprobante);

            //Console.WriteLine(result_html);

            // Se crea archivo temporal
            File.WriteAllText(ruta_html_temp, result_html);
      
            // Ruta de archivo conversor
            string ruta_wkhtml_topdf = Properties.Settings.Default.rutaDirectorio + @"\wkhtmltopdf\bin\wkhtmltopdf.exe";

            ProcessStartInfo proc_start_info = new ProcessStartInfo();
            proc_start_info.UseShellExecute = false;
            proc_start_info.FileName = ruta_wkhtml_topdf;
            proc_start_info.Arguments = "facturahtml.html "+ origen_pdf_temp;

            using(Process process= Process.Start(proc_start_info))
            {
                process.WaitForExit();
            }

            // Copiar el PDF a otra carpeta

            if (File.Exists(origen_pdf_temp))
            {
                File.Copy(origen_pdf_temp, destino_pdf);
            }
            
            // Eliminar archivo temporal
            File.Delete(ruta_html_temp);
            // Elimina el PDF creado
            File.Delete(origen_pdf_temp);
        }

        private static string GetStringOfFile(string ruta_arch)
        {
            string cont = File.ReadAllText(ruta_arch);

            return cont;
        }
    }
}
