using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    public class MessageBoxIndirect
    {
        #region Constantes de Usuarios de Windows
        // desde winuser
        private const uint MB_OK = 0x00000000;
        private const uint MB_OKCANCEL = 0x00000001;
        private const uint MB_ABORTRETRYIGNORE = 0x00000002;
        private const uint MB_YESNOCANCEL = 0x00000003;
        private const uint MB_YESNO = 0x00000004;
        private const uint MB_RETRYCANCEL = 0x00000005;
        private const uint MB_HELP = 0x00004000;

        private const uint MB_USERICON = 0x00000080;

        private const uint MB_ICONHAND = 0x00000010;
        private const uint MB_ICONQUESTION = 0x00000020;
        private const uint MB_ICONEXCLAMATION = 0x00000030;
        private const uint MB_ICONASTERISK = 0x00000040;
        private const uint MB_ICONWARNING = MB_ICONEXCLAMATION;
        private const uint MB_ICONERROR = MB_ICONHAND;
        private const uint MB_ICONINFORMATION = MB_ICONASTERISK;
        private const uint MB_ICONSTOP = MB_ICONHAND;

        private const uint MB_DEFBUTTON1 = 0x00000000;
        private const uint MB_DEFBUTTON2 = 0x00000100;
        private const uint MB_DEFBUTTON3 = 0x00000200;

        private const uint MB_RTLREADING = 0x00100000;
        private const uint MB_DEFAULT_DESKTOP_ONLY = 0x00020000;
        private const uint MB_SERVICE_NOTIFICATION = 0x00200000;  // assumes WNT >= 4
        private const uint MB_RIGHT = 0x00080000;

        private const uint MB_APPLMODAL = 0x00000000;
        private const uint MB_SYSTEMMODAL = 0x00001000;
        private const uint MB_TASKMODAL = 0x00002000;

        // Para configurar el icono de la ventana.
        private const uint WM_SETICON = 0x00000080;
        private const uint ICON_SMALL = 0;
        private const uint ICON_BIG = 1;

        private const int WH_CBT = 5;

        private const int HCBT_CREATEWND = 3;
        #endregion

        #region Tipos de enumeración como uint
        public enum MessageBoxExModality : uint
        {
            AppModal = MB_APPLMODAL,
            SystemModal = MB_SYSTEMMODAL,
            TaskModal = MB_TASKMODAL
        }
        #endregion

        #region Estructura API
        /// <summary>
        /// Punto estándar de la Ventana (MessageBox)
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public long x;
            public long y;
        };

        /// <summary>
        /// Desde winuser.h
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct HELPINFO
        {
            public int cbSize;
            public int iContextType;
            public int iCtrlId;
            public IntPtr hItemHandle;
            public IntPtr dwContextId;
            public POINT MousePos;

            /// <summary>
            /// Desmarque la información de ayuda del intptr dado, que es presumiblemente es el lParam recibido
            /// en un mensaje WM_HELP.
            /// </summary>
            /// <param name="lParam"></param>
            /// <returns></returns>
            public static HELPINFO UnmarshalForm(IntPtr lParam)
            {
                return (HELPINFO)Marshal.PtrToStructure(lParam, typeof(HELPINFO));
            }
        };

        /// <summary>
        /// Desde winuser.h
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MSGBOXPARAMS
        {
            public uint cbSize;
            public IntPtr hwndOwner;
            public IntPtr hInstance;
            public string lpszText;
            public string lpszCaption;
            public uint dwStyle;
            public IntPtr lpszIcon;
            public IntPtr dwContextHelpId;
            public MsgBoxCallback lpfnMsgBoxCallback;
            public uint dwLanguageId;
        };
        #endregion

        #region Delegados
        /// <summary>
        /// Declaración de delegado para una devolución de llamada que se llama cuando se presiona el botón de ayuda.
        /// </summary>
        public delegate void MsgBoxCallback(HELPINFO lpHelpInfo);

        /// <summary>
        /// Delegado para las anclas locales de la Ventana.
        /// </summary>
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        #endregion
    }
}
