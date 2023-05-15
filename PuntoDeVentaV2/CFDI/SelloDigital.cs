using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2.CFDI
{
    public class SelloDigital
    {
        public string Sellar(string CadenaOriginal, string ArchivoClavePrivada, string lPassword)
        {
            byte[] ClavePrivada = File.ReadAllBytes(ArchivoClavePrivada);
            byte[] bytesFirmados = null;
            byte[] bCadenaOriginal = null;

            SecureString lSecStr = new SecureString();
            SHA256Managed sham = new SHA256Managed();
            // SHA1Managed sham = new SHA1Managed(); version 3.2
            lSecStr.Clear();

            foreach (char c in lPassword.ToCharArray())
                lSecStr.AppendChar(c);

            RSACryptoServiceProvider lrsa = OpenSSLKey.DecodeEncryptedPrivateKeyInfo(ClavePrivada, lSecStr);
            //ExportPrivateKey(lrsa);

            //string ruta_cer = @"C:\Archivos PUDVE\MisDatos\CSD\CSD_NESTOR_DAVID_NUEZ_SOTO_NUSN900420SS5_20190316_134109s.cer";

            //byte[] pemstrcer = File.ReadAllBytes(ruta_cer);
            //OpenSSLKey.DecodeX509PublicKey(pemstrcer);

            bCadenaOriginal = Encoding.UTF8.GetBytes(CadenaOriginal);
            try
            {
                bytesFirmados = lrsa.SignData(Encoding.UTF8.GetBytes(CadenaOriginal), sham);

            }
            catch (NullReferenceException ex)
            {
                throw new NullReferenceException("Clave privada incorrecta, revisa que la clave que escribes corresponde a los sellos digitales cargados");
            }
            string sellodigital = Convert.ToBase64String(bytesFirmados);
            return sellodigital;
            //return "";
        }
        /// <summary>
        /// metodo que realiza el sello reciviendo el archivo key como matriaz de bytes
        /// </summary>
        /// <param name="CadenaOriginal"></param>
        /// <param name="ArchivoClavePrivada"></param>
        /// <param name="lPassword"></param>
        /// <returns></returns>
        public string Sellar(string CadenaOriginal, byte[] ArchivoClavePrivada, string lPassword)
        {
            byte[] ClavePrivada = ArchivoClavePrivada;
            byte[] bytesFirmados = null;
            byte[] bCadenaOriginal = null;

            SecureString lSecStr = new SecureString();
            SHA256Managed sham = new SHA256Managed();
            lSecStr.Clear();

            foreach (char c in lPassword.ToCharArray())
                lSecStr.AppendChar(c);

            
            RSACryptoServiceProvider lrsa = OpenSSLKey.DecodeEncryptedPrivateKeyInfo(ClavePrivada, lSecStr);
            bCadenaOriginal = Encoding.UTF8.GetBytes(CadenaOriginal);
            try
            {
                bytesFirmados = lrsa.SignData(Encoding.UTF8.GetBytes(CadenaOriginal), sham);

            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("Clave privada incorrecta.");
            }
            string sellodigital = Convert.ToBase64String(bytesFirmados);
            return sellodigital;
        }

        public static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    

    public bool verificarSello(string CadenaOriginal, string ArchivoClavePrivada, string lPassword, string ArchivoClavePublica)
        {
            byte[] ClavePrivada = File.ReadAllBytes(ArchivoClavePrivada);
            byte[] bytesFirmados = null;
            byte[] bCadenaOriginal = null;

            SecureString lSecStr = new SecureString();
            SHA1Managed sham = new SHA1Managed();
            lSecStr.Clear();

            foreach (char c in lPassword.ToCharArray())
                lSecStr.AppendChar(c);

            RSACryptoServiceProvider lrsa = OpenSSLKey.DecodeEncryptedPrivateKeyInfo(ClavePrivada, lSecStr);
            bCadenaOriginal = Encoding.UTF8.GetBytes(CadenaOriginal);
            try
            {
                bytesFirmados = lrsa.SignData(Encoding.UTF8.GetBytes(CadenaOriginal), sham);

            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("Clave privada incorrecta.");
            }

            string sellodigital = Convert.ToBase64String(bytesFirmados);

            RSACryptoServiceProvider rsaCSP = new RSACryptoServiceProvider();
            SHA1Managed hash = new SHA1Managed();
            byte[] hashedData;

            //rsaCSP.ImportParameters(rsaParams);
            //rsaCSP = JavaScience.opensslkey.(File.ReadAllBytes(ArchivoClavePublica));
            bool dataOK = rsaCSP.VerifyData(Encoding.UTF8.GetBytes(CadenaOriginal), CryptoConfig.MapNameToOID("SHA1"), bytesFirmados);
            hashedData = hash.ComputeHash(bytesFirmados);
            return rsaCSP.VerifyHash(hashedData, CryptoConfig.MapNameToOID("SHA1"), Encoding.UTF8.GetBytes(CadenaOriginal));
        }//*/

        public string SellarMD5(string CadenaOriginal, string ArchivoClavePrivada, string lPassword)
        {
            byte[] ClavePrivada = File.ReadAllBytes(ArchivoClavePrivada);
            byte[] bytesFirmados = null;
            byte[] bCadenaOriginal = null;
            SecureString lSecStr = new SecureString();
            lSecStr.Clear();
            foreach (char c in lPassword.ToCharArray())
                lSecStr.AppendChar(c);
            RSACryptoServiceProvider lrsa = OpenSSLKey.DecodeEncryptedPrivateKeyInfo(ClavePrivada, lSecStr);
            MD5CryptoServiceProvider hasher = new MD5CryptoServiceProvider();
            bCadenaOriginal = Encoding.UTF8.GetBytes(CadenaOriginal);
            hasher.ComputeHash(bCadenaOriginal);
            bytesFirmados = lrsa.SignData(bCadenaOriginal, hasher);
            string sellodigital = Convert.ToBase64String(bytesFirmados);
            return sellodigital;

        }
        public string Certificado(string ArchivoCER)
        {
            byte[] Certificado = File.ReadAllBytes(ArchivoCER);
            return Base64_Encode(Certificado);
        }
        public string Certificado(byte[] ArchivoCER)
        {
            return Base64_Encode(ArchivoCER);
        }
        string Base64_Encode(byte[] str)
        {
            return Convert.ToBase64String(str);
        }
        byte[] Base64_Decode(string str)
        {
            try
            {
                byte[] decbuff = Convert.FromBase64String(str);
                return decbuff;
            }
            catch
            {
                { return null; }
            }
        }
        public static string getCadenaOriginal(string NombreXML)
        {
            System.Xml.Xsl.XslCompiledTransform transformer = new System.Xml.Xsl.XslCompiledTransform();
            //Encoding utf8 = Encoding.UTF8;
            //byte[] encodedBytes;
            StringWriter strwriter = new StringWriter();
            if (File.Exists("cadenaoriginal_4_0.xslt"))
            {
                //cargamos el xslt transformer
                try
                {
                    transformer.Load("cadenaoriginal_4_0.xslt");
                    //procedemos a realizar la transfomración del archivo xml en base al xslt y lo almacenamos en un string que regresaremos 
                    transformer.Transform(NombreXML, null, strwriter);
                    //convertimos la cadena a utf8 y ya esta lista para ser utilizada en el hash
                    //encodedBytes = utf8.GetBytes(strwriter.ToString());
                    return strwriter.ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            if (File.Exists("cadenaoriginal_4_0.xslt"))
            {
                //cargamos el xslt transformer
                try
                {
                    transformer.Load("cadenaoriginal_4_0.xslt");
                    //procedemos a realizar la transfomración del archivo xml en base al xslt y lo almacenamos en un string que regresaremos 
                    transformer.Transform(NombreXML, null, strwriter);
                    //convertimos la cadena a utf8 y ya esta lista para ser utilizada en el hash
                    //encodedBytes = utf8.GetBytes(strwriter.ToString());
                    return strwriter.ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw e;
                }
            }
            else return "Error al cargar el validador.";


        }
        public static string md5(string Value)
        {


            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = Encoding.UTF8.GetBytes(Value);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            string ret = "";
            for (int i = 0; i < encodedBytes.Length; i++)
                ret += encodedBytes[i].ToString("x2").ToLower();
            return ret;

        }
        public static bool leerCER(string NombreArchivo, out string Inicio, out string Final, out string Serie, out string Numero)
        {
            

            if (NombreArchivo.Length < 1)
            {
                Inicio = "";
                Final = "";
                Serie = "INVALIDO";
                Numero = "";
                return false;
            }
            X509Certificate2 objCert = new X509Certificate2(NombreArchivo);
            StringBuilder objSB = new StringBuilder("Detalle del certificado: \n\n");

            //Detalle
            objSB.AppendLine("Persona = " + objCert.Subject);
            objSB.AppendLine("Emisor = " + objCert.Issuer);
            objSB.AppendLine("Válido desde = " + objCert.NotBefore.ToString());
            Inicio = objCert.NotBefore.ToString();
            objSB.AppendLine("Válido hasta = " + objCert.NotAfter.ToString());
            Final = objCert.NotAfter.ToString();
            objSB.AppendLine("Tamaño de la clave = " + objCert.PublicKey.Key.KeySize.ToString());
            objSB.AppendLine("Número de serie = " + objCert.SerialNumber);
            Serie = objCert.SerialNumber.ToString();
            Console.WriteLine("key="+objCert.PublicKey.Key.ToString()+ " tam key=" + objCert.PublicKey.Key.KeySize.ToString()+" sujeto="+ objCert.Subject);
            objSB.AppendLine("Hash = " + objCert.Thumbprint);
            objSB.AppendLine("SignatureAlgorithm = " + objCert.SignatureAlgorithm);
            /*

                        //Extensiones
                        objSB.AppendLine("\nExtensiones:\n");
                        foreach (X509Extension objExt in objCert.Extensions)
                        {
                            objSB.AppendLine(objExt.Oid.FriendlyName + " (" + objExt.Oid.Value + ')');

                            if (objExt.Oid.FriendlyName == "Key Usage" | objExt.Oid.FriendlyName == "Uso de la clave")
                            {
                                X509KeyUsageExtension ext = (X509KeyUsageExtension)objExt;
                                objSB.AppendLine("    " + ext.KeyUsages);
                            }

                            if (objExt.Oid.FriendlyName == "Basic Constraints" | objExt.Oid.FriendlyName == "Restricciones básicas")
                            {
                                X509BasicConstraintsExtension ext = (X509BasicConstraintsExtension)objExt;
                                objSB.AppendLine("    " + ext.CertificateAuthority);
                                objSB.AppendLine("    " + ext.HasPathLengthConstraint);
                                objSB.AppendLine("    " + ext.PathLengthConstraint);
                            }

                            if (objExt.Oid.FriendlyName == "Subject Key Identifier")
                            {
                                X509SubjectKeyIdentifierExtension ext = (X509SubjectKeyIdentifierExtension)objExt;
                                objSB.AppendLine("    " + ext.SubjectKeyIdentifier);
                            }

                            if (objExt.Oid.FriendlyName == "Enhanced Key Usage") //2.5.29.37
                            {
                                X509EnhancedKeyUsageExtension ext = (X509EnhancedKeyUsageExtension)objExt;
                                OidCollection objOids = ext.EnhancedKeyUsages;
                                foreach (Oid oid in objOids)
                                    objSB.AppendLine("    " + oid.FriendlyName + " (" + oid.Value + ')');
                            }
                        }
                        Console.Write(objSB.ToString());
                        string ruta_guardar_archivos = @"C:\Archivos PUDVE\MisDatos\CSD\CSD_NESTOR_DAVID_NUEZ_SOTO_NUSN900420SS5_20190316_134109.key";
                        string priv = File.ReadAllText(ruta_guardar_archivos);
                       ///// RSACryptoServiceProvider pub = RSACryptoServiceProvider(priv);

                        string strOriginal = "House121";

                        byte[] bytOriginal = Encoding.ASCII.GetBytes(strOriginal);

                        SHA256 objAlgoritmo = SHA256.Create();

                        byte[] bytHash = objAlgoritmo.ComputeHash(bytOriginal);

                        string strHash = Convert.ToBase64String(bytHash);

                        Console.WriteLine("resultado=" + strHash);
                        */
            //Numero = "?";
            string tNumero = "", rNumero = "", tNumero2 = "";

            int X;
            if (Serie.Length < 2)
                Numero = "";
            else
            {
                foreach (char c in Serie)
                {
                    switch (c)
                    {
                        case '0': tNumero += c; break;
                        case '1': tNumero += c; break;
                        case '2': tNumero += c; break;
                        case '3': tNumero += c; break;
                        case '4': tNumero += c; break;
                        case '5': tNumero += c; break;
                        case '6': tNumero += c; break;
                        case '7': tNumero += c; break;
                        case '8': tNumero += c; break;
                        case '9': tNumero += c; break;
                    }
                }
                for (X = 1; X < tNumero.Length; X++)
                {
                    //wNewString = wNewString & Right$(Left$(wCadena, x), 1)
                    X += 1;
                    //rNumero = rNumero + 
                    tNumero2 = tNumero.Substring(0, X);
                    rNumero = rNumero + tNumero2.Substring(tNumero2.Length - 1, 1);// Right$(Left$(wCadena, x), 1)
                }
                Numero = rNumero;

            }

            if (DateTime.Now < objCert.NotAfter && DateTime.Now > objCert.NotBefore)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// lee el codigo del certificado enviado este como matriz de bytes
        /// </summary>
        /// <param name="NombreArchivo">certificado en matriz de bytes</param>
        /// <param name="Inicio"></param>
        /// <param name="Final"></param>
        /// <param name="Serie"></param>
        /// <param name="Numero"></param>
        /// <returns></returns>
        public static bool leerCER(byte[] NombreArchivo, out string Inicio, out string Final, out string Serie, out string Numero)
        {

            if (NombreArchivo.Length < 1)
            {
                Inicio = "";
                Final = "";
                Serie = "INVALIDO";
                Numero = "";
                return false;
            }
            X509Certificate2 objCert = new X509Certificate2(NombreArchivo);
            StringBuilder objSB = new StringBuilder("Detalle del certificado: \n\n");

            //Detalle
            objSB.AppendLine("Persona = " + objCert.Subject);
            objSB.AppendLine("Emisor = " + objCert.Issuer);
            objSB.AppendLine("Válido desde = " + objCert.NotBefore.ToString());
            Inicio = objCert.NotBefore.ToString();
            objSB.AppendLine("Válido hasta = " + objCert.NotAfter.ToString());
            Final = objCert.NotAfter.ToString();
            objSB.AppendLine("Tamaño de la clave = " + objCert.PublicKey.Key.KeySize.ToString());
            objSB.AppendLine("Número de serie = " + objCert.SerialNumber);
            Serie = objCert.SerialNumber.ToString();

            objSB.AppendLine("Hash = " + objCert.Thumbprint);
            //Numero = "?";
            string tNumero = "", rNumero = "", tNumero2 = "";

            int X;
            if (Serie.Length < 2)
                Numero = "";
            else
            {
                foreach (char c in Serie)
                {
                    switch (c)
                    {
                        case '0': tNumero += c; break;
                        case '1': tNumero += c; break;
                        case '2': tNumero += c; break;
                        case '3': tNumero += c; break;
                        case '4': tNumero += c; break;
                        case '5': tNumero += c; break;
                        case '6': tNumero += c; break;
                        case '7': tNumero += c; break;
                        case '8': tNumero += c; break;
                        case '9': tNumero += c; break;
                    }
                }
                for (X = 1; X < tNumero.Length; X++)
                {
                    //wNewString = wNewString & Right$(Left$(wCadena, x), 1)
                    X += 1;
                    //rNumero = rNumero + 
                    tNumero2 = tNumero.Substring(0, X);
                    rNumero = rNumero + tNumero2.Substring(tNumero2.Length - 1, 1);// Right$(Left$(wCadena, x), 1)
                }
                Numero = rNumero;

            }

            if (DateTime.Now < objCert.NotAfter && DateTime.Now > objCert.NotBefore)
            {
                return true;
            }

            return false;
        }


        public static bool validarCERKEY(string NombreArchivoCER, string NombreArchivoKEY, string ClavePrivada)
        {
            X509Certificate2 certificado = new X509Certificate2(File.ReadAllBytes(NombreArchivoCER));
            //initialze the byte arrays to the public key information.
            byte[] pk = certificado.GetPublicKey();
            X509Certificate2 certPrivado = new X509Certificate2(File.ReadAllBytes(NombreArchivoKEY));
            return false;
        }





        
        /*private static void ExportPrivateKey(RSACryptoServiceProvider csp )
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


        private static void ExportPublicKey(RSACryptoServiceProvider csp )
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
            }
        }
        */
    }
}
