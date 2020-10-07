using System;
using System.Collections.Generic;
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
            bool nuevaInstancia = true;

            using (Mutex mutex = new Mutex(true, "PUDVE190590", out nuevaInstancia))
            {
                if (nuevaInstancia)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //Application.Run(new Login());
                    Application.Run(new ConfiguracionMariaDB());
                }
            }
        }
    }
}
