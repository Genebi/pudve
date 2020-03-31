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

            // Proveedor
            string usuario = "NUSN900420SS5";
            string clave_u = "c.ofis09NSUNotcatno5SS0240";

            // Datos del xml a cancelar
            string rfc_emisor = "";
            string uuid_xml = "F0B60888-BC93-4851-A345-03C238572A8D";
            string rfc_rec = "IAD121214B34";
            string total_xml = "7261.60";


            try
            {
                folios folios_datos = new folios();
                var lista_folios = new List<folio>();

                // Guardar en la lista los folios de xml a cancelar
                lista_folios.Add(new folio
                {
                    uuid = uuid_xml,
                    rfc_receptor = rfc_rec,
                    total = total_xml
                });

                var folio_array = lista_folios.ToArray();
                folios_datos.folio = folio_array;

                // Petición al PAC para la cancelación del XML
                cancelacion_portClient cliente_cancelar = new cancelacion_portClient();
                cancelar_cfdi_result response = new cancelar_cfdi_result();

                response = cliente_cancelar.cancelar_cfdi(usuario, clave_u, rfc_emisor, folios_datos, cer_pem, key_pem);

                XmlDocument acuse_cancelacion = new XmlDocument();
                acuse_cancelacion.LoadXml(response.folios_cancelacion);
                Console.WriteLine(response.folios_cancelacion.ToString());


                return response.folios_cancelacion.ToString();
            }
            catch (FaultException e)
            {
                Console.WriteLine("Codigo de rror " + e.Code.Name + ": " + e.Message);


                return "Codigo de rror " + e.Code.Name + "\n" + e.Message;
            }


            //..............................................................
            // .    Consulta estatus del XML que fue enviado a cancelar    .
            //..............................................................

            try
            {
                cancelacion_portClient consulta_estatus = new cancelacion_portClient();
                consultar_estatus_result res_estatus = new consultar_estatus_result();

                res_estatus = consulta_estatus.consultar_estatus(usuario, clave_u, uuid_xml, rfc_emisor, rfc_rec, total_xml);
                Console.WriteLine(res_estatus.estatus_cancelacion.ToString());

                return res_estatus.estatus_cancelacion.ToString();
            }
            catch (FaultException e)
            {
                Console.WriteLine("Código de error " + e.Code.Name + ": " + e.Message);
                return "Código de error: " + e.Code.Name + "\n" + e.Message;
            }
            
        }

        private static string string_file(string ruta_arch)
        {
            string cont = File.ReadAllText(ruta_arch);

            return cont;
        }
    }
}
