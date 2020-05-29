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


        public string[] cancelar(int idf, string tipof)
        {
            string ruta_archivos = @"C:\Archivos PUDVE\MisDatos\CSD\";
            string ruta_acuse = @"C:\Archivos PUDVE\Facturas\";

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
            ruta_acuse = ruta_acuse + nombre_xml + ".xml";

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

/*
        public void Sellar(string ArchivoClavePrivada, string lPassword)
        {
            byte[] ClavePrivada = File.ReadAllBytes(ArchivoClavePrivada);
            
            SecureString lSecStr = new SecureString();
            SHA256Managed sham = new SHA256Managed();

            lSecStr.Clear();

            foreach (char c in lPassword.ToCharArray())
                lSecStr.AppendChar(c);

            RSACryptoServiceProvider lrsa = CFDI.OpenSSLKey.DecodeEncryptedPrivateKeyInfo(ClavePrivada, lSecStr);
            Console.WriteLine(lrsa);
            Console.WriteLine("\n------------------------------------------------\n");
            ExportPrivateKey(lrsa);
            Console.WriteLine("\n------------------------------------------------\n");
            ExportPublicKey(lrsa);
            //string sellodigital = ByteConverter.(lrsa);
            // byte[] d = lrsa;


            Console.WriteLine(ExportPublicKeyToPEMFormat(lrsa));

            Console.WriteLine("\n------------------------------------------------\n");
            Console.WriteLine("DOS");

            Console.WriteLine("\n------------------------------------------------\n");
            ///////Console.WriteLine(Convert.ToBase64String(lrsa));
        }


        private static void ExportPrivateKey(RSACryptoServiceProvider csp)
        {
            Console.WriteLine("HOLA");
            TextWriter outputStream = new StringWriter();
            if (csp.PublicOnly) throw new ArgumentException("CSP does not contain a private key", "csp");
            var parameters = csp.ExportParameters(true);
            using (var stream = new MemoryStream())
            {
                Console.WriteLine("HOLA 2");
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    Console.WriteLine("HOLA 3");
                    var innerWriter = new BinaryWriter(innerStream);
                    EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
                    EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent);
                    EncodeIntegerBigEndian(innerWriter, parameters.D);
                    EncodeIntegerBigEndian(innerWriter, parameters.P);
                    EncodeIntegerBigEndian(innerWriter, parameters.Q);
                    EncodeIntegerBigEndian(innerWriter, parameters.DP);
                    EncodeIntegerBigEndian(innerWriter, parameters.DQ);
                    EncodeIntegerBigEndian(innerWriter, parameters.InverseQ);
                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                outputStream.WriteLine("-----BEGIN RSA PRIVATE KEY-----");
                // Output as Base64 with lines chopped at 64 characters
                for (var i = 0; i < base64.Length; i += 64)
                {
                    outputStream.WriteLine(base64, i, Math.Min(64, base64.Length - i));
                }
                outputStream.WriteLine("-----END RSA PRIVATE KEY-----");

                Console.WriteLine("HOLA outputStream" + outputStream);
            }
        }

        private static void EncodeLength(BinaryWriter stream, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
            if (length < 0x80)
            {
                // Short form
                stream.Write((byte)length);
            }
            else
            {
                // Long form
                var temp = length;
                var bytesRequired = 0;
                while (temp > 0)
                {
                    temp >>= 8;
                    bytesRequired++;
                }
                stream.Write((byte)(bytesRequired | 0x80));
                for (var i = bytesRequired - 1; i >= 0; i--)
                {
                    stream.Write((byte)(length >> (8 * i) & 0xff));
                }
            }
        }

        private static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
        {
            stream.Write((byte)0x02); // INTEGER
            var prefixZeros = 0;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] != 0) break;
                prefixZeros++;
            }
            if (value.Length - prefixZeros == 0)
            {
                EncodeLength(stream, 1);
                stream.Write((byte)0);
            }
            else
            {
                if (forceUnsigned && value[prefixZeros] > 0x7f)
                {
                    // Add a prefix zero to force unsigned if the MSB is 1
                    EncodeLength(stream, value.Length - prefixZeros + 1);
                    stream.Write((byte)0);
                }
                else
                {
                    EncodeLength(stream, value.Length - prefixZeros);
                }
                for (var i = prefixZeros; i < value.Length; i++)
                {
                    stream.Write(value[i]);
                }
            }
        }


        private static void ExportPublicKey(RSACryptoServiceProvider csp)
        {
            TextWriter outputStream = new StringWriter();

            var parameters = csp.ExportParameters(false);
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    var innerWriter = new BinaryWriter(innerStream);
                    innerWriter.Write((byte)0x30); // SEQUENCE
                    EncodeLength(innerWriter, 13);
                    innerWriter.Write((byte)0x06); // OBJECT IDENTIFIER
                    var rsaEncryptionOid = new byte[] { 0x2a, 0x86, 0x48, 0x86, 0xf7, 0x0d, 0x01, 0x01, 0x01 };
                    EncodeLength(innerWriter, rsaEncryptionOid.Length);
                    innerWriter.Write(rsaEncryptionOid);
                    innerWriter.Write((byte)0x05); // NULL
                    EncodeLength(innerWriter, 0);
                    innerWriter.Write((byte)0x03); // BIT STRING
                    using (var bitStringStream = new MemoryStream())
                    {
                        var bitStringWriter = new BinaryWriter(bitStringStream);
                        bitStringWriter.Write((byte)0x00); // # of unused bits
                        bitStringWriter.Write((byte)0x30); // SEQUENCE
                        using (var paramsStream = new MemoryStream())
                        {
                            var paramsWriter = new BinaryWriter(paramsStream);
                            EncodeIntegerBigEndian(paramsWriter, parameters.Modulus); // Modulus
                            EncodeIntegerBigEndian(paramsWriter, parameters.Exponent); // Exponent
                            var paramsLength = (int)paramsStream.Length;
                            EncodeLength(bitStringWriter, paramsLength);
                            bitStringWriter.Write(paramsStream.GetBuffer(), 0, paramsLength);
                        }
                        var bitStringLength = (int)bitStringStream.Length;
                        EncodeLength(innerWriter, bitStringLength);
                        innerWriter.Write(bitStringStream.GetBuffer(), 0, bitStringLength);
                    }
                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                outputStream.WriteLine("-----BEGIN PUBLIC KEY-----");
                for (var i = 0; i < base64.Length; i += 64)
                {
                    outputStream.WriteLine(base64, i, Math.Min(64, base64.Length - i));
                }
                outputStream.WriteLine("-----END PUBLIC KEY-----");


                Console.WriteLine("HOLA outputStream CER--" + outputStream);


                CFDI.OpenSSLKey.DecodePEMKey(outputStream.ToString());

                //int t= outputStream.
                //byte[] df= Convert.ToByte(File.ReadAllBytes(outputStream));

                //CFDI.OpenSSLKey.DecodeX509PublicKey(df);
            }
        }



        public static byte[] DecodePkcs8EncPrivateKey(String instr)
        {
            const String pemp8encheader = "-----BEGIN ENCRYPTED PRIVATE KEY-----";
            const String pemp8encfooter = "-----END ENCRYPTED PRIVATE KEY-----";
            String pemstr = instr.Trim();
            byte[] binkey;
            if (!pemstr.StartsWith(pemp8encheader) || !pemstr.EndsWith(pemp8encfooter))
                return null;
            StringBuilder sb = new StringBuilder(pemstr);
            sb.Replace(pemp8encheader, "");  //remove headers/footers, if present
            sb.Replace(pemp8encfooter, "");

            String pubstr = sb.ToString().Trim();   //get string after removing leading/trailing whitespace

            try
            {
                binkey = Convert.FromBase64String(pubstr);
            }
            catch (System.FormatException)
            {       //if can't b64 decode, data is not valid
                return null;
            }
            Console.WriteLine("-........................--");
            Console.WriteLine("||||||--" + binkey);
            return binkey;
        }


        public static String ExportPublicKeyToPEMFormat(RSACryptoServiceProvider csp)
        {
            TextWriter outputStream = new StringWriter();

            var parameters = csp.ExportParameters(false);
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    var innerWriter = new BinaryWriter(innerStream);
                    EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
                    EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent);

                    //All Parameter Must Have Value so Set Other Parameter Value Whit Invalid Data  (for keeping Key Structure  use "parameters.Exponent" value for invalid data)
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.D
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.P
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.Q
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.DP
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.DQ
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.InverseQ

                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                outputStream.WriteLine("-----BEGIN PUBLIC KEY-----");
                // Output as Base64 with lines chopped at 64 characters
                for (var i = 0; i < base64.Length; i += 64)
                {
                    outputStream.WriteLine(base64, i, Math.Min(64, base64.Length - i));
                }
                outputStream.WriteLine("-----END PUBLIC KEY-----");


                Console.WriteLine("...............................");
                Console.WriteLine("............ OTRO .........");
                Console.WriteLine("...............................");
                return outputStream.ToString();

            }
        }
        */
    }
}
