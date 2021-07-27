using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        #region Métodos que se implementaran externamente
        /// <summary>
        /// La declaración de API MessageBoxIndirect.
        /// </summary>
        [DllImport("user32", EntryPoint = "MessageBoxIndirect")]
        private static extern int _MessageBoxIndirect(ref MSGBOXPARAMS msgboxParams);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);
        #endregion

        #region VARIABLES DE MIEMBRO
        private string _text;
        private string _caption;
        private IWin32Window _owner;
        private IntPtr _instance;
        private IntPtr _sysSmallIcon;
        private int _contextID = 0;
        private uint _languageID;
        private MsgBoxCallback _callback;
        private MessageBoxButtons _buttons = MessageBoxButtons.OK;
        private bool _showHelp = false;
        private IntPtr _userIcon = IntPtr.Zero;
        private MessageBoxIcon _icon = MessageBoxIcon.None;
        private MessageBoxDefaultButton _defaultButton = MessageBoxDefaultButton.Button1;
        private MessageBoxOptions _options = 0;
        private MessageBoxExModality _modality = MessageBoxExModality.AppModal;
        private int hHook;
        #endregion

        #region Propiedades
        /// <summary>
        /// El texto del MessageBox.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// El título del MessageBox.
        /// </summary>
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        /// <summary>
        /// El ID del icono personalizado en el recurso no administrado.
        /// </summary>
        public IntPtr UserIcon
        {
            get { return _userIcon; }
            set { _userIcon = value; }
        }

        /// <summary>
        /// El ID del ícono personalizado en el recurso no administrado para usar como ícono
        /// en el menú del sistema de la ventana de alerta.
        /// </summary>
        public IntPtr SysSmallIcon
        {
            get { return _sysSmallIcon; }
            set { _sysSmallIcon = value; }
        }

        /// <summary>
        /// Ventana de propietario.
        /// </summary>
        public IWin32Window Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        /// <summary>
        /// Identificador de instancia utilizado para cargar un recurso de icono personalizado.
        /// </summary>
        public IntPtr Instance
        {
            get { return _instance; }
            set { _instance = value; }
        }

        /// <summary>
        /// ID de idioma para los botones del cuadro de mensajes.
        /// </summary>
        public uint LanguageID
        {
            get { return _languageID; }
            set { _languageID = value; }
        }

        /// <summary>
        /// El ID de contexto de ayuda que se transmite en la instancia HELPINFO cuando se invoca la ayuda.
        /// </summary>
        public int ContextHelpID
        {
            get { return _contextID; }
            set { _contextID = value; }
        }

        /// <summary>
        /// La devolución de llamada del botón de ayuda (si hay alguna).
        /// </summary>
        public MsgBoxCallback Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        /// <summary>
        /// Opciones de botones de cuadro de mensajes estándar.
        /// </summary>
        public MessageBoxButtons Buttons
        {
            get { return _buttons; }
            set { _buttons = value; }
        }

        /// <summary>
        /// Si queremos o no un botón de ayuda en nuestro MessageBox.
        /// </summary>
        public bool ShowHelp
        {
            get { return _showHelp; }
            set { _showHelp = value; }
        }

        /// <summary>
        /// Cualquier ícono estándar de Windows que deseemos mostrar.
        /// </summary>
        public MessageBoxIcon Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        /// <summary>
        /// Botón predeterminado del MessageBox estándar.
        /// </summary>
        public MessageBoxDefaultButton DefaultButton
        {
            get { return _defaultButton; }
            set { _defaultButton = value; }
        }

        /// <summary>
        /// Opciones de MessageBox estándar.
        /// </summary>
        public MessageBoxOptions Options
        {
            get { return _options; }
            set { _options = value; }
        }

        /// <summary>
        /// Indique la modalidad del cuadro de mensaje (sysmodal / appmodal / taskmodal).
        /// </summary>
        public MessageBoxExModality Modality
        {
            get { return _modality; }
            set { _modality = value; }
        }
        #endregion
    }
}
