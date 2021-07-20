using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            bool nuevaInstancia = true;

            using (Mutex mutex = new Mutex(true, "PUDVE190590", out nuevaInstancia))
            {
                if (nuevaInstancia)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Login());
                }
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            StreamWriter log;

            if (!Directory.Exists(@"C:\Archivos PUDVE\LogFile"))
            {
                Directory.CreateDirectory(@"C:\Archivos PUDVE\LogFile");
            }

            if (!File.Exists(@"C:\Archivos PUDVE\LogFile\LogFileError.txt"))
            {
                log = new StreamWriter(@"C:\Archivos PUDVE\LogFile\LogFileError.txt");
            }
            else
            {
                log = File.AppendText(@"C:\Archivos PUDVE\LogFile\LogFileError.txt");
            }

            //if (!File.Exists("LogFileError.txt"))
            //{
            //    log = new StreamWriter("LogFileError.txt");
            //}
            //else
            //{
            //    log = File.AppendText("LogFileError.txt");
            //}

            // Write to the File:
            log.WriteLine(" ");
            log.WriteLine("-----------------------------------------------------------------------");
            log.WriteLine("Date Time: " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
            log.WriteLine("Exception ToString: " + e.ToString());
            log.WriteLine("Exception: " + e.Exception);
            log.WriteLine("Exception GetHashCode: " + e.GetHashCode().ToString());
            log.WriteLine("Exception GetType: " + e.GetType().ToString());
        }
    }
}
