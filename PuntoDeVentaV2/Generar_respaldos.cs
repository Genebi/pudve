using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PuntoDeVentaV2
{
    class Generar_respaldos
    {

        Conexion cn = new Conexion();


        public static void respaldar()
        {

            string name_user = FormPrincipal.TempUserNickName;
            string file_db = Properties.Settings.Default.rutaDirectorio + "\\PUDVE\\BD\\pudveDBtxt.txt";

            string fecha = System.DateTime.Today.ToString("yyyy-MM-dd");
            string hora = System.DateTime.Now.ToString("hh-mm-ss");

            string file_name = "PUDVE_DB"; //_" + fecha + "-" + hora;


            // Verificar que la carpeta exista, si no existe, se crea con el archivo php

            WebRequest request = WebRequest.Create("http://pudve.com/crear_carpeta_archivo.php?u=" + name_user + "&nf=" + file_name);
            request.Credentials = CredentialCache.DefaultCredentials;

            // Obtener respuesta  
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Obtener respuesta del servidor
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                Console.WriteLine("servidor=" + responseFromServer);
            }

            response.Close();


            // Subir respaldo de base de datos

            if (((HttpWebResponse)response).StatusDescription.ToString() == "OK")
            {

                FtpWebRequest request_ftp = (FtpWebRequest)WebRequest.Create("ftp://pudve.com/PUDVE/respaldos_db/" + name_user + "/" + file_name + ".txt");
                request_ftp.Method = WebRequestMethods.Ftp.UploadFile;

                request_ftp.Credentials = new NetworkCredential("adminsifo", "Steroids12");

                // Copie el contenido del archivo para la solicitud
                byte[] file_content;
                using (StreamReader sourceStream = new StreamReader(file_db))
                {
                    file_content = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                }

                request_ftp.ContentLength = file_content.Length;

                using (Stream requestStream = request_ftp.GetRequestStream())
                {
                    requestStream.Write(file_content, 0, file_content.Length);
                }

                // Obtener respuesta
                using (FtpWebResponse response_ftp = (FtpWebResponse)request_ftp.GetResponse())
                {
                    Console.WriteLine($"Upload File Complete, status {response_ftp.StatusDescription}");
                }

            }


            /*try
            {
                WebClient wc = new WebClient();

                string myfile = Properties.Settings.Default.rutaDirectorio + "\\PUDVE\\BD\\pudveDBuser8v2.txt";
                wc.Credentials = CredentialCache.DefaultCredentials;
                wc.UploadFile(@"http.://pudve.com/respaldo_BD/upload.php", "POST", myfile);

                wc.Dispose();
            }
            catch (WebException we)
            {
                MessageBox.Show(we.Message+" CÓDIGO STATUS: "+we.Status);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }*/
        }
    }
}
