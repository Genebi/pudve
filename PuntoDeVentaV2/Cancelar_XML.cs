using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml;
using PuntoDeVentaV2.ServiceReference_cancelar_prueba;

namespace PuntoDeVentaV2
{
    class Cancelar_XML
    {
        public string cancelar()
        {
            string ruta_archivos = @"C:\Archivos PUDVE\MisDatos\CSD";

            // Obtener archivos .pem
            string cer_pem = string_file(ruta_archivos);
            string key_pem = string_file(ruta_archivos);

            // Guardar en la lista los folios de xml a cancelar
            /*folios = new List<FoliosCancelar>();

            folios_fisc.Add(
                new FoliosCancelar()
                {
                    uuid = "",
                    rfc = "",
                    total = ""
                }
            );*/


            try
            {

            }
            catch {
            }


            return "";
        }

        private static string string_file(string ruta_arch)
        {
            string cont = File.ReadAllText(ruta_arch);

            return cont;
        }
    }
}
