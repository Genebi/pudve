using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml;
//using PuntoDeVentaV2.ServiceReference_cancelar_prueba;
using PuntoDeVentaV2.ServiceReference_cancelar_produccion;
using System.Data;
using System.Security;


namespace PuntoDeVentaV2
{
    class Cancelar_XML
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();


        public string[] cancelar(int idf, string tipof, string motivo_canc, string uuid_sust)
        {
            var usuarioCancelacion = string.Empty;

            if (!FormPrincipal.userNickName.Equals(string.Empty))
            {
                usuarioCancelacion = FormPrincipal.userNickName;
            }

            string ruta_archivos = $@"C:\Archivos PUDVE\MisDatos\CSD_{usuarioCancelacion}\";
            string ruta_acuse = @"C:\Archivos PUDVE\Facturas\";
            var servidor = Properties.Settings.Default.Hosting;

            // Proveedor
            string usuario = "NUSN900420SS5";
            //string clave_u = "c.ofis09NSUNotcatno5SS0240";
            string clave_u = "pGoyQq-RHsaij_yNJfHp";

            // Datos del xml a cancelar
            string rfc_emisor = "";
            string uuid_xml = "";
            string rfc_rec = "";
            string total_xml = "";
            // Datos resultado
            string r_uuid = "";
            string r_codigo = "";
            string r_mensaje = "";
            string[] rsp = new string[2];



            // Nombre del xml

            string nombre_xml = "XML_INGRESOS_ACUSE_" + idf;
            if (tipof == "P")
            {
                nombre_xml = "XML_PAGO_ACUSE_" + idf;
            }

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                ruta_archivos = $@"\\{servidor}\Archivos PUDVE\MisDatos\CSD_{usuarioCancelacion}\";
                ruta_acuse = $@"\\{servidor}\Archivos PUDVE\Facturas\" + nombre_xml + ".xml";
            }
            else
            {
                ruta_acuse = ruta_acuse + nombre_xml + ".xml";
            }

            // Consulta datos

            DataTable d_factura = cn.CargarDatos(cs.cargar_datos_venta_xml(1, idf, FormPrincipal.userID));
            DataRow r_factura = d_factura.Rows[0];

            rfc_emisor = r_factura["e_rfc"].ToString();
            rfc_rec = r_factura["r_rfc"].ToString();
            uuid_xml =  r_factura["UUID"].ToString();
            total_xml = r_factura["total"].ToString();


            // Obtener archivos .pem

            string nm_kpem = "";
            string nm_cpem = "";

            DirectoryInfo dir = new DirectoryInfo(ruta_archivos);

            foreach (var arch in dir.GetFiles())
            {
                string nm = arch.Name;
                string nombre = nm.Substring(nm.Length - 7, 7);

                if (nombre == "key.pem")
                {
                    nm_kpem = arch.Name;
                }
                if (nombre == "cer.pem")
                {
                    nm_cpem = arch.Name; 
                }
            }

            string key_pem = string_file(ruta_archivos + nm_kpem);
            string cer_pem = string_file(ruta_archivos + nm_cpem);

            var pos = cer_pem.IndexOf("-----BEGIN CERTIFICATE-----");
            cer_pem = cer_pem.Substring(pos, cer_pem.Length - pos);
            //Console.WriteLine("RESULTADO" + cer_pem);
            Console.WriteLine("uuid = " + uuid_xml + " rfc_receptor = " + rfc_rec + " total = " + total_xml+ "motivo = "+motivo_canc + "folio_sustituto = " + uuid_sust);
            
            try
            {
                folios folios_datos = new folios();
                var lista_folios = new List<folio>();

                // Guardar en la lista los folios de xml a cancelar
                lista_folios.Add(new folio
                {
                    uuid = uuid_xml,
                    rfc_receptor = rfc_rec,
                    total = total_xml,
                    motivo = motivo_canc,
                    folio_sustituto = uuid_sust                    
                });

                var folio_array = lista_folios.ToArray();
                folios_datos.folio = folio_array;

                // Petición al PAC para la cancelación del XML
                cancelacion_portClient cliente_cancelar = new cancelacion_portClient();
                cancelar_cfdi_result response = new cancelar_cfdi_result();


                response = cliente_cancelar.cancelar_cfdi(usuario, clave_u, rfc_emisor, folios_datos, cer_pem, key_pem);

                // Obtener datos de la respuesta
                XmlDocument acuse_cancelacion = new XmlDocument();
                acuse_cancelacion.LoadXml(response.folios_cancelacion);
                

                foreach (XmlNode node in acuse_cancelacion.DocumentElement.ChildNodes)
                {
                    if (node.HasChildNodes)
                    {
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            if(i == 0)
                            {
                                r_uuid = node.ChildNodes[i].InnerText;
                            }
                            if (i == 1)
                            {
                                r_codigo = node.ChildNodes[i].InnerText;
                            }
                            if (i == 2)
                            {
                                r_mensaje = node.ChildNodes[i].InnerText;
                            }
                            //node.ChildNodes[i].Name
                        }
                    }
                }

                Console.WriteLine(r_uuid+"---" +r_codigo+ "---"+r_mensaje);

                // Operación a realizar
                
                string r = codigos_respuesta(r_codigo, r_uuid, r_mensaje);
                rsp[0] = r;
                rsp[1] = r_codigo;

                // Guarda el acuse recibido solo cuando la factura ya fue cancelada

                if(r_codigo == "201")
                {
                    File.WriteAllText(ruta_acuse, response.acuse_cancelacion);

                    // Resta timbre

                    cn.EjecutarConsulta(cs.descontar_timbres(FormPrincipal.userID));
                }
                

                return rsp;
            }
            catch (FaultException e)
            {
                rsp[0] = "Codigo de error " + e.Code.Name + "\n" + e.Message;
                rsp[1] = "";


                return rsp;
            }
            
        }

        private static string string_file(string ruta_arch)
        {
            string cont = File.ReadAllText(ruta_arch);

            return cont;
        }

        private string codigos_respuesta(string cod, string uuid, string mnsj)
        {
            string mensaje = "";
            string tipo_codigo = "";

            switch (cod)
            {
                case "201":
                    mensaje = "Factura cancelada con éxito.";
                    break;
                case "202":
                    mensaje = "La factura ya habia sido cancelada anteriormente.";
                    break;
                case "204":
                    mensaje = "La factura no aplica para ser cancelada. Su factura no puede ser cancelada.";
                    break;
                case "205":
                    mensaje = "La factura a cancelar no existe en la lista de facturas timbradas, verifique que el folio fiscal sea igual a la factura a cancelar. \n Folio fiscal: " + uuid;
                    break;
                    
                
                case "CANC304":
                    mensaje = mnsj + ". Si recién tramito sus archivos CSD, debe esperar 72 hrs. habiles (en el SAT) para iniciar a timbrar/cancelar sus facturas.";
                    break;
                case "CANC306":
                    mensaje = "El certificado es incorrecto, revise que sus archivos digitales esten vigentes, sean de tipo CSD y hallan sido subidos correctamente.";
                    break;
                case "CANC998":
                    mensaje = mnsj + ". Vuelva a intentar en un lapso de 5 a 10 min..";
                    break;
                case "CANC999":
                    mensaje = mnsj + ". Vuelva a intentar en un lapso de 5 a 10 min. si el problema persiste contacte al servicio de atención a clientes.";
                    break;


                default:
                    mensaje = mnsj;
                    break;
            }


            if(cod == "CANC001" | cod == "CANC002" | cod == "CANC003" | cod == "CANC301")
            {
                if (cod == "CANC001") { tipo_codigo = "'Error CANC001AUT'"; }
                if (cod == "CANC002") { tipo_codigo = "'Error CANC002TIM'"; }
                if (cod == "CANC003") { tipo_codigo = "'Error CANC003PAR'"; }
                if (cod == "CANC301") { tipo_codigo = "'Error CANC301XML'"; }

                mensaje = "No fue posible cancelar su CFDI. Contacte al servicio de atención a clientes y proporcione el siguiente código de error:" + tipo_codigo;
            }
            if (cod == "CANC302" | cod == "CANC303")
            {
                if (cod == "CANC302") { tipo_codigo = "'Error CANC302PEM'"; }
                if (cod == "CANC303") { tipo_codigo = "'Error CANC303CER'"; }
                if (cod == "CANC308") { tipo_codigo = "'Error CANC308LLA'"; }

                mensaje = mnsj + ". Verifique que los archivos digitales sean los pertenecientes al RFC del emisor, y hallan sido subidos correctamente. Si el problema persiste contacte al servicio de atención a clientes y proporcione el siguiente código de error:" + tipo_codigo;
            }


            return mensaje;
        }


    }
}
