using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    /*
    *   Primero tienes que copiar el código de más abajo y pegarlo en tu código fuente (observa que es una nueva clase). 
    *   Después debes llamar en tu código al MessageBoxTemporal de este modo:
    *   
    *   MessageBoxTemporal.Show("Texto", "Titulo", 8, false);
    *
    *   Donde:
    *
    *   Texto: Es el texto que se mostrará en el cuerpo del MessageBox
    *   Titulo: Es el título del MessageBox
    *   8: Son los segundos que durará el mensaje. No puedes poner más de 99 segundos = (1.39 min).
    *   false: true o false, bool que muestra o no el contador.
    *   
    *   False: aparecerá el MessageBox, se cerrará a los X segundos pero no aparecerá ninguna cadena de texto que diga: 
    *   "Este mensaje se cerrará en X segundos".
    *   
    *   True: aparecerá el MessageBox, se cerrará a los X segundos y aparecerá un mensaje con un contador ("Este mensaje se cerrará en X segundos.")
    *   
    *   MessageBoxTemporal.Show("Mensaje de prueba para karmany.NET", "Título", 22, true);
    *
    * 
    */
    public class MessageBoxTemporal
    {
        System.Threading.Timer IntervaloTiempo;
        IntPtr hndLabel = IntPtr.Zero;
        private string TituloMessageBox;
        private string TextoMessageBox;
        private int TiempoMaximo;
        private bool MostrarContador;

        public MessageBoxTemporal(string texto, string titulo, int tiempo, bool contador)
        {
            this.TextoMessageBox = texto;
            this.TituloMessageBox = titulo;
            this.TiempoMaximo = tiempo;
            this.MostrarContador = contador;

            if (TiempoMaximo > 99)
            {
                return; // Máixmo 99 segundos (1.39 min)
            }

            IntervaloTiempo = new System.Threading.Timer(EjecutaCada1Segundo, null, 1000, 1000);

            if (contador)
            {
                DialogResult ResultadoMensaje = MessageBox.Show($"{texto} \r", titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                IntervaloTiempo.Dispose();
            }
            else
            {
                DialogResult ResultadoMensaje = MessageBox.Show($"{texto} ...", titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (ResultadoMensaje.Equals(DialogResult.OK))
                {
                    IntervaloTiempo.Dispose();
                }
            }
        }

        private void EjecutaCada1Segundo(object state)
        {
            TiempoMaximo--;
            if (TiempoMaximo <= 0)
            {
                IntPtr hndMBox = FindWindow(null, TituloMessageBox);
                if (!hndMBox.Equals(IntPtr.Zero))
                {
                    SendMessage(hndMBox, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    IntervaloTiempo.Dispose();
                }
            }
            else if (MostrarContador)
            {
                // Ha pasado un intervalo de 1 seg:
                if (!hndLabel.Equals(IntPtr.Zero))
                {
                    SetWindowText(hndLabel, $"{TextoMessageBox}\r\nEste mensaje se cerrará dentro de {TiempoMaximo.ToString("00")} segundos");
                }
                else
                {
                    IntPtr hndMBox = FindWindow(null, TituloMessageBox);
                    if (!hndMBox.Equals(IntPtr.Zero))
                    {
                        // Ha encontrado el MessageBox, busca ahora el texto
                        hndLabel = FindWindowEx(hndMBox, IntPtr.Zero, "Static", null);
                        if (!hndLabel.Equals(IntPtr.Zero))
                        {
                            // Ha encontrado el texto porque el MessageBox
                            // solo tiene un control "Static".
                            SetWindowText(hndLabel, $"{TextoMessageBox}\r\nEste mensaje se cerrará dentro de {TiempoMaximo.ToString("00")} segundos");
                        }
                    }
                }
            }
        }

        const int WM_CLOSE = 0x0010;

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindows);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern bool SetWindowText(IntPtr hwnd, string lpString);

        /// <summary>
        /// MessageBox que se cierra tras X segundos
        /// </summary>
        /// <param name="texto">Es el texto que se mostrará en el cuerpo del MessageBox</param>
        /// <param name="titulo">Es el título del MessageBox</param>
        /// <param name="tiempo">Son los segundos que durará el mensaje. No puedes poner más de 99 segundos = (1.39 min).</param>
        /// <param name="contador">False: aparecerá el MessageBox, se cerrará a los X segundos pero no aparecerá ninguna cadena de texto que diga: "Este mensaje se cerrará en X segundos"; True: aparecerá el MessageBox, se cerrará a los X segundos y aparecerá un mensaje con un contador ("Este mensaje se cerrará en X segundos.")</param>
        public static void Show(string texto, string titulo, int tiempo, bool contador)
        {
            new MessageBoxTemporal(texto, titulo, tiempo, contador);
        }
    }
}
