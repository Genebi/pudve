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

        #region Implementación
        /// <summary>
        /// Asegúrese de que tengamos una instancia desde la que cargar recursos.
        /// </summary>
        private void EnsureInstance()
        {
            if (Instance.Equals(IntPtr.Zero))
            {
                // El usuario no especificó una instancia desde la que cargar, por lo que el valor predeterminado es el que se ejecuta actualmente.
                // módulo.
                Instance = Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0]);
            }
        }

        /// <summary>
        /// Convierta todas las propiedades relacionadas con el estilo establecidas en la clase en una uint adecuada para pasar a la API.
        /// </summary>
        /// <returns>Regresa todas las propiedades con los estilos establecidos</returns>
        private uint BuildStyle()
        {
            uint result = 0;

            // Buttons
            if (Buttons.Equals(MessageBoxButtons.OK))
            {
                result |= MB_OK;
            }
            else if (Buttons.Equals(MessageBoxButtons.OKCancel))
            {
                result |= MB_OKCANCEL;
            }
            else if (Buttons.Equals(MessageBoxButtons.AbortRetryIgnore))
            {
                result |= MB_ABORTRETRYIGNORE;
            }
            else if (Buttons.Equals(MessageBoxButtons.RetryCancel))
            {
                result |= MB_RETRYCANCEL;
            }
            else if (Buttons.Equals(MessageBoxButtons.YesNo))
            {
                result |= MB_YESNO;
            }
            else if (Buttons.Equals(MessageBoxButtons.YesNoCancel))
            {
                result |= MB_YESNOCANCEL;
            }

            // Help
            if (ShowHelp)
            {
                result |= MB_HELP;
            }

            // Icono de usuario
            if (!UserIcon.Equals(IntPtr.Zero))
            {
                result |= MB_USERICON;
                EnsureInstance();
            }

            // Icono
            if (Icon.Equals(MessageBoxIcon.Asterisk))
            {
                result |= MB_ICONASTERISK;
            }
            else if (Icon.Equals(MessageBoxIcon.Error))
            {
                result |= MB_ICONERROR;
            }
            else if (Icon.Equals(MessageBoxIcon.Exclamation))
            {
                result |= MB_ICONEXCLAMATION;
            }
            else if (Icon.Equals(MessageBoxIcon.Hand))
            {
                result |= MB_ICONHAND;
            }
            else if (Icon.Equals(MessageBoxIcon.Information))
            {
                result |= MB_ICONINFORMATION;
            }
            else if (Icon.Equals(MessageBoxIcon.Question))
            {
                result |= MB_ICONQUESTION;
            }
            else if (Icon.Equals(MessageBoxIcon.Stop))
            {
                result |= MB_ICONSTOP;
            }
            else if (Icon.Equals(MessageBoxIcon.Warning))
            {
                result |= MB_ICONWARNING;
            }

            // Botones default
            if (DefaultButton.Equals(MessageBoxDefaultButton.Button1))
            {
                result |= MB_DEFBUTTON1;
            }
            else if (DefaultButton.Equals(MessageBoxDefaultButton.Button2))
            {
                result |= MB_DEFBUTTON2;
            }
            else if (DefaultButton.Equals(MessageBoxDefaultButton.Button3))
            {
                result |= MB_DEFBUTTON3;
            }

            // Opciones
            result |= (uint)Options;

            // Modalidad
            result |= (uint)Modality;

            return result;
        }

        /// <summary>
        /// Utilice el enlace CBT para utilizar el identificador de la ventana del MessageBox en la creación.
        /// </summary>
        private int CbtHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode.Equals(HCBT_CREATEWND))
            {
                StringBuilder sb = new StringBuilder();
                sb.Capacity = 100;
                GetClassName(wParam, sb, sb.Capacity);
                string className = sb.ToString();

                if (className.Equals("#32770"))
                {
                    if (!_sysSmallIcon.Equals(IntPtr.Zero))
                    {
                        EnsureInstance();
                        IntPtr hSmallsysIcon = LoadIcon(Instance, new IntPtr((long)((short)_sysSmallIcon.ToInt32())));
                        if (!hSmallsysIcon.Equals(IntPtr.Zero))
                        {
                            SendMessage(wParam, WM_SETICON, new IntPtr(ICON_SMALL), hSmallsysIcon);
                        }
                    }
                }
            }

            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        /// <summary>
        /// Coloque el MessageBox y devuelva el resultado.
        /// </summary>
        public DialogResult Show()
        {
            MSGBOXPARAMS parms = new MSGBOXPARAMS();
            parms.dwStyle = BuildStyle();
            parms.lpszText = Text;
            parms.lpszCaption = Caption;

            if (!Owner.Equals(null))
            {
                parms.hwndOwner = Owner.Handle;
            }

            parms.hInstance = Instance;
            parms.cbSize = (uint)Marshal.SizeOf(typeof(MSGBOXPARAMS));
            parms.lpfnMsgBoxCallback = Callback;
            parms.lpszIcon = UserIcon;
            parms.dwLanguageId = LanguageID;
            parms.dwContextHelpId = new IntPtr(_contextID);

            DialogResult retval = DialogResult.Cancel;

            try
            {
                // Solo se ancla si tenemos una razón para hacerlo, es decir, para configurar el icono personalizado.
                if (!_sysSmallIcon.Equals(IntPtr.Zero))
                {
                    HookProc CbtHookProcedure = new HookProc(CbtHookProc);
                    hHook = SetWindowsHookEx(WH_CBT, CbtHookProcedure, (IntPtr)0, AppDomain.GetCurrentThreadId());
                }

                retval = (DialogResult)_MessageBoxIndirect(ref parms);
            }
            finally
            {
                if (hHook > 0)
                {
                    UnhookWindowsHookEx(hHook);
                    hHook = 0;
                }
            }

            return retval;
        }
        #endregion

        #region Cosntructores
        /// <summary>
        /// Muestra un MessageBox con el texto especificado.
        /// </summary>
		public MessageBoxIndirect(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Muestra un MessageBox delante del objeto especificado y con el texto especificado.
        /// </summary>
        public MessageBoxIndirect(IWin32Window owner, string text)
        {
            Owner = owner;
            Text = text;
        }

        /// <summary>
        /// Muestra un MessageBox con el texto y el título especificados.
        /// </summary>
        public MessageBoxIndirect(string text, string caption)
        {
            Text = text;
            Caption = caption;
        }

        /// <summary>
        /// Muestra un MessageBox delante del objeto especificado y con el texto y el título especificados.
        /// </summary>
        public MessageBoxIndirect(IWin32Window owner, string text, string caption)
        {
            Owner = owner;
            Text = text;
            Caption = caption;
        }

        /// <summary>
        /// Muestra un MessageBox con texto, título y botones especificados.
        /// </summary>
        public MessageBoxIndirect(string text, string caption, MessageBoxButtons buttons)
        {
            Text = text;
            Caption = caption;
            Buttons = buttons;
        }

        /// <summary>
        /// Muestra un MessageBox delante del objeto especificado y con el texto, el título y los botones especificados.
        /// </summary>
        public MessageBoxIndirect(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
        {
            Owner = owner;
            Text = text;
            Caption = caption;
            Buttons = buttons;
        }

        /// <summary>
        /// Muestra un MessageBox con texto, título, botones e íconos especificados.
        /// </summary>
        public MessageBoxIndirect(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            Text = text;
            Caption = caption;
            Buttons = buttons;
            Icon = icon;
        }

        /// <summary>
        /// Muestra un MessageBox delante del objeto especificado y con el texto, el título, los botones y el icono especificados.
        /// </summary>
        public MessageBoxIndirect(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            Owner = owner;
            Text = text;
            Caption = caption;
            Buttons = buttons;
            Icon = icon;
        }

        /// <summary>
        /// Muestra un MessageBox con el texto, el título, los botones, el icono y el botón predeterminados especificados.
        /// </summary>
        public MessageBoxIndirect(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            Text = text;
            Caption = caption;
            Buttons = buttons;
            Icon = icon;
            DefaultButton = defaultButton;
        }

        /// <summary>
        /// Muestra un MessageBox delante del objeto especificado y con el texto, título, botones, icono y botón predeterminado especificados.
        /// </summary>
        public MessageBoxIndirect(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            Owner = owner;
            Text = text;
            Caption = caption;
            Buttons = buttons;
            Icon = icon;
            DefaultButton = defaultButton;
        }

        /// <summary>
        /// Muestra un MessageBox con el texto, el título, los botones, el icono, el botón predeterminado y las opciones especificados.
        /// </summary>
        public MessageBoxIndirect(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            Text = text;
            Caption = caption;
            Buttons = buttons;
            Icon = icon;
            DefaultButton = defaultButton;
            Options = options;
        }

        /// <summary>
        /// Muestra un MessageBox delante del objeto especificado y con el texto, el título, los botones, el icono, 
        /// el botón predeterminado y las opciones especificados.
        /// </summary>
        public MessageBoxIndirect(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            Owner = owner;
            Text = text;
            Caption = caption;
            Buttons = buttons;
            Icon = icon;
            DefaultButton = defaultButton;
            Options = options;
        }
        #endregion
    }
}
