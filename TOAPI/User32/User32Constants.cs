using System;

using TOAPI.Types;

namespace TOAPI.User32
{
    public partial class User32
    {
        // SetWindowsHook() codes
        public const int WH_MIN             = (-1);
        public const int WH_MSGFILTER       = (-1);
        public const int WH_JOURNALRECORD   = 0;
        public const int WH_JOURNALPLAYBACK = 1;
        public const int WH_KEYBOARD        = 2;
        public const int WH_GETMESSAGE      = 3;
        public const int WH_CALLWNDPROC     = 4;
        public const int WH_CBT             = 5;
        public const int WH_SYSMSGFILTER    = 6;
        public const int WH_MOUSE           = 7;
        public const int WH_HARDWARE        = 8;
        public const int WH_DEBUG           = 9;
        public const int WH_SHELL           =10;
        public const int WH_FOREGROUNDIDLE  =11;
        public const int WH_CALLWNDPROCRET  =12;
        public const int WH_KEYBOARD_LL     =13;
        public const int WH_MOUSE_LL        =14;

        public const int WH_MAX             =14;

        public const int WH_MINHOOK         =WH_MIN;
        public const int WH_MAXHOOK         =WH_MAX;

        // Hook Codes
        public const int HC_ACTION          = 0;
        public const int HC_GETNEXT         = 1;
        public const int HC_SKIP            = 2;
        public const int HC_NOREMOVE        = 3;
        public const int HC_SYSMODALON      = 4;
        public const int HC_SYSMODALOFF     = 5;

        // CBT Hook Codes
        public const int HCBT_MOVESIZE      = 0;
        public const int HCBT_MINMAX        = 1;
        public const int HCBT_QS            = 2;
        public const int HCBT_CREATEWND     = 3;
        public const int HCBT_DESTROYWND    = 4;
        public const int HCBT_ACTIVATE      = 5;
        public const int HCBT_CLICKSKIPPED  = 6;
        public const int HCBT_KEYSKIPPED    = 7;
        public const int HCBT_SYSCOMMAND    = 8;
        public const int HCBT_SETFOCUS      = 9;


        // WH_MSGFILTER Filter Proc Codes
        public const int  MSGF_DIALOGBOX     = 0;
        public const int  MSGF_MESSAGEBOX    = 1;
        public const int  MSGF_MENU          = 2;
        public const int  MSGF_SCROLLBAR     = 5;
        public const int  MSGF_NEXTWINDOW    = 6;
        public const int  MSGF_MAX           = 8;                       // unused
        public const int  MSGF_USER          = 4096;

            // Shell support
            public const int HSHELL_WINDOWCREATED = 1;
            public const int  HSHELL_WINDOWDESTROYED     = 2;
            public const int  HSHELL_ACTIVATESHELLWINDOW = 3;
            public const int  HSHELL_WINDOWACTIVATED     = 4;
            public const int  HSHELL_GETMINRECT          = 5;
            public const int  HSHELL_REDRAW              = 6;
            public const int  HSHELL_TASKMAN             = 7;
            public const int  HSHELL_LANGUAGE            = 8;
            public const int  HSHELL_SYSMENU             = 9;
            public const int  HSHELL_ENDTASK             = 10;
            public const int  HSHELL_ACCESSIBILITYSTATE  = 11;
            public const int  HSHELL_APPCOMMAND          = 12;

            public const int  HSHELL_WINDOWREPLACED      = 13;
            public const int  HSHELL_WINDOWREPLACING     = 14;


            public const int  HSHELL_HIGHBIT             = 0x8000;
            public const int  HSHELL_FLASH               = (HSHELL_REDRAW|HSHELL_HIGHBIT);
            public const int  HSHELL_RUDEAPPACTIVATED    = (HSHELL_WINDOWACTIVATED|HSHELL_HIGHBIT);


            public const int
            DESKTOP_READOBJECTS = 0x00000001,
            DESKTOP_CREATEWINDOW = 0x00000002,
            DESKTOP_CREATEMENU = 0x00000004,
            DESKTOP_HOOKCONTROL = 0x00000008,
            DESKTOP_JOURNALRECORD = 0x00000010,
            DESKTOP_JOURNALPLAYBACK = 0x00000020,
            DESKTOP_ENUMERATE = 0x00000040,
            DESKTOP_WRITEOBJECTS = 0x00000080,
            DESKTOP_SWITCHDESKTOP = 0x00000100,
            DESKTOP_ALL = (DESKTOP_READOBJECTS | DESKTOP_CREATEWINDOW | DESKTOP_CREATEMENU |
                            DESKTOP_HOOKCONTROL | DESKTOP_JOURNALRECORD | DESKTOP_JOURNALPLAYBACK |
                            DESKTOP_ENUMERATE | DESKTOP_WRITEOBJECTS | DESKTOP_SWITCHDESKTOP | ACC.STANDARD_RIGHTS_REQUIRED);

        public const int
            WINSTA_ENUMDESKTOPS = 0x00000001,
            WINSTA_READATTRIBUTES = 0x00000002,
            WINSTA_ACCESSCLIPBOARD = 0x00000004,
            WINSTA_CREATEDESKTOP = 0x00000008,
            WINSTA_WRITEATTRIBUTES = 0x00000010,
            WINSTA_ACCESSGLOBALATOMS = 0x00000020,
            WINSTA_EXITWINDOWS = 0x00000040,
            WINSTA_ENUMERATE = 0x00000100,
            WINSTA_READSCREEN = 0x00000200,

            WINSTA_ALL_ACCESS = 0x0000037f;
        
        public const int
            UOI_FLAGS      = 1,
            UOI_NAME       = 2,
            UOI_TYPE       = 3,
            UOI_USER_SID   = 4,
            UOI_HEAPSIZE   = 5,
            UOI_IO         = 6;


        // Color Types
        public const int
            CTLCOLOR_MSGBOX = 0,
            CTLCOLOR_EDIT = 1,
            CTLCOLOR_LISTBOX = 2,
            CTLCOLOR_BTN = 3,
            CTLCOLOR_DLG = 4,
            CTLCOLOR_SCROLLBAR = 5,
            CTLCOLOR_STATIC = 6,
            CTLCOLOR_MAX = 7;

        public const int
            COLOR_SCROLLBAR = 0,
            COLOR_BACKGROUND = 1,
            COLOR_ACTIVECAPTION = 2,
            COLOR_INACTIVECAPTION = 3,
            COLOR_MENU = 4,
            COLOR_WINDOW = 5,
            COLOR_WINDOWFRAME = 6,
            COLOR_MENUTEXT = 7,
            COLOR_WINDOWTEXT = 8,
            COLOR_CAPTIONTEXT = 9,
            COLOR_ACTIVEBORDER = 10,
            COLOR_INACTIVEBORDER = 11,
            COLOR_APPWORKSPACE = 12,
            COLOR_HIGHLIGHT = 13,
            COLOR_HIGHLIGHTTEXT = 14,
            COLOR_BTNFACE = 15,
            COLOR_BTNSHADOW = 16,
            COLOR_GRAYTEXT = 17,
            COLOR_BTNTEXT = 18,
            COLOR_INACTIVECAPTIONTEXT = 19,
            COLOR_BTNHIGHLIGHT = 20;

        public const int
            COLOR_3DDKSHADOW = 21,
            COLOR_3DLIGHT = 22,
            COLOR_INFOTEXT = 23,
            COLOR_INFOBK = 24;

        public const int
            COLOR_HOTLIGHT = 26,
            COLOR_GRADIENTACTIVECAPTION = 27,
            COLOR_GRADIENTINACTIVECAPTION = 28;

        public const int
            COLOR_MENUHILIGHT = 29,
            COLOR_MENUBAR = 30;

        public const int
            COLOR_DESKTOP = COLOR_BACKGROUND,
            COLOR_3DFACE = COLOR_BTNFACE,
            COLOR_3DSHADOW = COLOR_BTNSHADOW,
            COLOR_3DHIGHLIGHT = COLOR_BTNHIGHLIGHT,
            COLOR_3DHILIGHT = COLOR_BTNHIGHLIGHT,
            COLOR_BTNHILIGHT = COLOR_BTNHIGHLIGHT;

        // ClassStyle flags for window creation
        public const int
            CS_VREDRAW = 0x0001,
            CS_HREDRAW = 0x0002,
            CS_KEYCVTWINDOW = 0x0004,
            CS_DBLCLKS = 0x0008,
            CS_OWNDC = 0x0020,
            CS_CLASSDC = 0x0040,
            CS_PARENTDC = 0x0080,
            CS_NOKEYCVT = 0x0100,
            CS_SAVEBITS = 0x0800,
            CS_NOCLOSE = 0x0200,
            CS_BYTEALIGNCLIENT = 0x00001000,
            CS_BYTEALIGNWINDOW = 0x00002000,
            CS_GLOBALCLASS = 0x00004000,
            CS_IME = 0x00010000;
        
        /* flags for DrawFrameControl */
        // This is the 'type' parameter
        public const int
            DFC_CAPTION          =   1,
            DFC_MENU             =   2,
            DFC_SCROLL           =   3,
            DFC_BUTTON           =   4,
            DFC_POPUPMENU        =   5;

        // These are all the 'state' parameters.  They are
        // specific to the DFC_CAPTION type.
        public const int
            DFCS_CAPTIONCLOSE     =  0x0000,
            DFCS_CAPTIONMIN       =  0x0001,
            DFCS_CAPTIONMAX       =  0x0002,
            DFCS_CAPTIONRESTORE   =  0x0003,
            DFCS_CAPTIONHELP      =  0x0004;

        // State specific to the DFC_MENU type
        public const int
            DFCS_MENUARROW         = 0x0000,
            DFCS_MENUCHECK         = 0x0001,
            DFCS_MENUBULLET        = 0x0002,
            DFCS_MENUARROWRIGHT    = 0x0004;

        // State specific to the DFC_SCROLL type
        public const int
            DFCS_SCROLLUP          = 0x0000,
            DFCS_SCROLLDOWN        = 0x0001,
            DFCS_SCROLLLEFT        = 0x0002,
            DFCS_SCROLLRIGHT       = 0x0003,
            DFCS_SCROLLCOMBOBOX    = 0x0005,
            DFCS_SCROLLSIZEGRIP    = 0x0008,
            DFCS_SCROLLSIZEGRIPRIGHT = 0x0010;

        // State specific to the DFC_BUTTON
        public const int
            DFCS_BUTTONCHECK       = 0x0000,
            DFCS_BUTTONRADIOIMAGE  = 0x0001,
            DFCS_BUTTONRADIOMASK   = 0x0002,
            DFCS_BUTTONRADIO       = 0x0004,
            DFCS_BUTTON3STATE      = 0x0008,
            DFCS_BUTTONPUSH        = 0x0010;

        public const int
        DFCS_ADJUSTRECT = 0x2000;

        // State modifiers
        public const int
            DFCS_INACTIVE          = 0x0100,
            DFCS_PUSHED            = 0x0200,
            DFCS_CHECKED           = 0x0400,
            DFCS_TRANSPARENT       = 0x0800,
            DFCS_HOT               = 0x1000,
            DFCS_FLAT              = 0x4000,
            DFCS_MONO              = 0x8000;




        // Key State Masks for Mouse Messages
        public const int
            MK_LBUTTON       =   0x0001,
            MK_RBUTTON       =   0x0002,
            MK_SHIFT         =   0x0004,
            MK_CONTROL       =   0x0008,
            MK_MBUTTON       =   0x0010,
            MK_XBUTTON1      =   0x0020,
            MK_XBUTTON2      =   0x0040;

        // Constants for selecting system cursors
        public const int
            IDC_ARROW = 32512,
            IDC_IBEAM = 32513,
            IDC_WAIT = 32514,
            IDC_CROSS = 32515,
            IDC_UPARROW = 32516,
            IDC_SIZE = 32640,
            IDC_ICON = 32641,
            IDC_SIZENWSE = 32642,
            IDC_SIZENESW = 32643,
            IDC_SIZEWE = 32644,
            IDC_SIZENS = 32645,
            IDC_SIZEALL = 32646,
            IDC_NO = 32648,
            IDC_APPSTARTING = 32650,
            IDC_HELP = 32651;

        public const int CW_USEDEFAULT = (unchecked((int)0x80000000));

        public const int PM_NOREMOVE = 0x0000,
            PM_REMOVE = 0x0001,
            PM_NOYIELD = 0x0002;


        // GetWindow() Constants
        public const int
            GW_HWNDFIRST       = 0,
            GW_HWNDLAST        = 1,
            GW_HWNDNEXT        = 2,
            GW_HWNDPREV        = 3,
            GW_OWNER           = 4,
            GW_CHILD           = 5,
            GW_ENABLEDPOPUP    = 6,
            GW_MAX             = 6;


        // Constants for window styles
        // These come from WinUser.h
        public const int
            WS_OVERLAPPED = 0x00000000,
            WS_POPUP = unchecked((int)0x80000000),
            WS_CHILD = 0x40000000,
            WS_MINIMIZE = 0x20000000,
            WS_VISIBLE = 0x10000000,
            WS_DISABLED = 0x08000000,
            WS_CLIPSIBLINGS = 0x04000000,
            WS_CLIPCHILDREN = 0x02000000,
            WS_MAXIMIZE = 0x01000000,
            WS_CAPTION = 0x00C00000,
            WS_BORDER = 0x00800000,
            WS_DLGFRAME = 0x00400000,
            WS_VSCROLL = 0x00200000,
            WS_HSCROLL = 0x00100000,
            WS_SYSMENU = 0x00080000,
            WS_THICKFRAME = 0x00040000,
            WS_GROUP = 0x00020000,
            WS_TABSTOP = 0x00010000,
            WS_MINIMIZEBOX = 0x00020000,
            WS_MAXIMIZEBOX = 0x00010000,
            WS_TILED = 0x00000000,
            WS_ICONIC = 0x20000000,
            WS_SIZEBOX = 0x00040000,
            WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_GROUP | WS_TABSTOP),
            WS_POPUPWINDOW = (unchecked(WS_POPUP | WS_BORDER | WS_SYSMENU)),
            WS_CHILDWINDOW = (0x40000000);

        // Constants for extended window styles
        public const int
            WS_EX_DLGMODALFRAME = 0x00000001,
            WS_EX_NOPARENTNOTIFY = 0x00000004,
            WS_EX_TOPMOST = 0x00000008,
            WS_EX_ACCEPTFILES = 0x00000010,
            WS_EX_TRANSPARENT = 0x00000020,
            WS_EX_MDICHILD = 0x00000040,
            WS_EX_TOOLWINDOW = 0x00000080,
            WS_EX_WINDOWEDGE = 0x00000100,
            WS_EX_CLIENTEDGE = 0x00000200,
            WS_EX_CONTEXTHELP = 0x00000400,
            WS_EX_RIGHT = 0x00001000,
            WS_EX_LEFT = 0x00000000,
            WS_EX_RTLREADING = 0x00002000,
            WS_EX_LTRREADING = 0x00000000,
            WS_EX_LEFTSCROLLBAR = 0x00004000,
            WS_EX_RIGHTSCROLLBAR = 0x00000000,
            WS_EX_CONTROLPARENT = 0x00010000,
            WS_EX_STATICEDGE = 0x00020000,
            WS_EX_APPWINDOW = 0x00040000,
            WS_EX_OVERLAPPEDWINDOW = (0x00000100 | 0x00000200),
            WS_EX_PALETTEWINDOW = (0x00000100 | 0x00000080 | 0x00000008),
            WS_EX_LAYERED = 0x00080000,
            WS_EX_NOINHERITLAYOUT = 0x00100000, // Disable inheritence of mirroring by children
            WS_EX_LAYOUTRTL = 0x00400000, // Right to left mirroring
            WS_EX_COMPOSITED = 0x02000000,
            WS_EX_NOACTIVATE = 0x08000000;

        public const int GWL_WNDPROC = (-4),
            GWL_HINSTANCE = (-6),
            GWL_HWNDPARENT = (-8),
            GWL_STYLE = (-16),
            GWL_EXSTYLE = (-20),
            GWL_USERDATA = (-21),
            GWL_ID = (-12);

        public const int
            HWND_DESKTOP    = 0x0000,
            HWND_BROADCAST  = 0xffff,
            HWND_TOP        = (0),
            HWND_BOTTOM     = (1),
            HWND_TOPMOST    = (-1),
            HWND_NOTOPMOST  = (-2),
            HWND_MESSAGE = (-3);


        public const int
        LWA_COLORKEY = 0x00000001,
        LWA_ALPHA = 0x00000002,
        ULW_COLORKEY = 0x00000001,
        ULW_ALPHA = 0x00000002,
        ULW_OPAQUE = 0x00000004,
        ULW_EX_NORESIZE = 0x00000008;



        public const uint TME_HOVER = 0x00000001,
            TME_LEAVE = 0x00000002,
            TME_NONCLIENT = 0x00000010,
            TME_QUERY = 0x40000000,
            TME_CANCEL = 0x80000000,
            HOVER_DEFAULT = 0xFFFFFFFF;

        /// <summary>
        /// These are used to indicate the DeviceState flag from the
        /// EnumDisplayDevices call
        /// </summary>
        public const int
            DISPLAY_DEVICE_ATTACHED_TO_DESKTOP = 0x00000001,
            DISPLAY_DEVICE_MULTI_DRIVER = 0x00000002,
            DISPLAY_DEVICE_PRIMARY_DEVICE = 0x00000004,
            DISPLAY_DEVICE_MIRRORING_DRIVER = 0x00000008,
            DISPLAY_DEVICE_VGA_COMPATIBLE = 0x00000010,
            DISPLAY_DEVICE_REMOVABLE = 0x00000020,
            DISPLAY_DEVICE_TS_COMPATIBLE = 0x00200000,
            DISPLAY_DEVICE_DISCONNECT = 0x02000000,
            DISPLAY_DEVICE_REMOTE = 0x04000000,
            DISPLAY_DEVICE_MODESPRUNED = 0x08000000;

        public const int
            DISPLAY_DEVICE_ACTIVE = 0x00000001,       // Child device state
            DISPLAY_DEVICE_ATTACHED = 0x00000002;      // Child device state

        // Used with EnumDisplaySettings, EnumDisplaySettingsEx
        public const int
            ENUM_CURRENT_SETTINGS = -1,      // Retrieve the current settings for the display device. 
            ENUM_REGISTRY_SETTINGS = -2;     // Retrieve the settings for the display device that are currently stored in the registry. 


        // Flags for ChangeDisplaySettings
        public const int
            CDS_UPDATEREGISTRY = 0x00000001,
            CDS_TEST = 0x00000002,
            CDS_FULLSCREEN = 0x00000004,
            CDS_GLOBAL = 0x00000008,
            CDS_SET_PRIMARY = 0x00000010,
            CDS_VIDEOPARAMETERS = 0x00000020,
            CDS_ENABLE_UNSAFE_MODES = 0x00000100,
            //CDS_DISABLE_UNSAFE_MODES     0x00000200
            CDS_NORESET = 0x10000000,
            CDS_RESET = 0x40000000;

        /* Return values for ChangeDisplaySettings */
        public const int
            DISP_CHANGE_SUCCESSFUL = 0,
            DISP_CHANGE_RESTART = 1,
            DISP_CHANGE_FAILED = -1,
            DISP_CHANGE_BADMODE = -2,
            DISP_CHANGE_NOTUPDATED = -3,
            DISP_CHANGE_BADFLAGS = -4,
            DISP_CHANGE_BADPARAM = -5,
            DISP_CHANGE_BADDUALVIEW = -6;

        /* Flags for EnumDisplaySettingsEx */
        public const int
            EDS_RAWMODE = 0x00000002,
            EDS_ROTATEDMODE = 0x00000004;

        public const int
            MONITOR_DEFAULTTONULL = 0x00000000,
            MONITOR_DEFAULTTOPRIMARY = 0x00000001,
            MONITOR_DEFAULTTONEAREST = 0x00000002,
            MONITORINFOF_PRIMARY = 0x00000001;


//        Table 6: Special-Cased Top-Level Collections
//            for HID Input
        //Device Common Name        Usage Page  Usage ID    Additional Hardware ID 
        //Pointer                   0x01        0x01        HID_DEVICE_SYSTEM_MOUSE
        //Mouse                     0x01        0x02        HID_DEVICE_SYSTEM_MOUSE
        //Joystick                  0x01        0x04        HID_DEVICE_SYSTEM_GAME
        //Game pad                  0x01        0x05        HID_DEVICE_SYSTEM_GAME 
        //Keyboard                  0x01        0x06        HID_DEVICE_SYSTEM_KEYBOARD
        //Keypad                    0x01        0x07        HID_DEVICE_SYSTEM_KEYBOARD 
        //System Control            0x01        0x80        HID_DEVICE_SYSTEM_CONTROL
        //Consumer Audio Control    0x0C        0x01        HID_DEVICE_SYSTEM_CONSUMER
 

        /// <summary>
        /// Flags for GetRawInputData
        /// </summary>
        public const int
            RID_INPUT = 0x10000003,
            RID_HEADER = 0x10000005;

        public const int
            RIDEV_REMOVE           = 0x00000001,
            RIDEV_EXCLUDE          = 0x00000010,
            RIDEV_PAGEONLY         = 0x00000020,
            RIDEV_NOLEGACY         = 0x00000030,
            RIDEV_INPUTSINK        = 0x00000100,
            RIDEV_CAPTUREMOUSE     = 0x00000200,  // effective when mouse nolegacy is specified, otherwise it would be an error
            RIDEV_NOHOTKEYS        = 0x00000200,  // effective for keyboard.
            RIDEV_APPKEYS          = 0x00000400, // effective for keyboard.
            RIDEV_EXINPUTSINK      = 0x00001000,
            RIDEV_DEVNOTIFY        = 0x00002000,
            RIDEV_EXMODEMASK       = 0x000000F0;

            //RIDEV_EXMODE(mode)  ((mode) & RIDEV_EXMODEMASK)

        /*
 * Raw Input Device Information
 */
        public const int
            RIDI_PREPARSEDDATA = 0x20000005,
            RIDI_DEVICENAME = 0x20000007,  // the return valus is the character length, not the byte size
            RIDI_DEVICEINFO = 0x2000000b;

        public const int
            RIM_INPUT = 0,
            RIM_INPUTSINK = 1,
            RIM_TYPEMOUSE = 0,
            RIM_TYPEKEYBOARD = 1,
            RIM_TYPEHID = 2;

         /*
         * Define the keyboard overrun MakeCode.
         */
        public const int
            KEYBOARD_OVERRUN_MAKE_CODE  =  0xFF;

        /*
         * Define the keyboard input data Flags.
         * 
         */
        /// <summary>
        /// These constants represent the Flags field of the 
        /// RAWKEYBOARD structure.
        /// </summary>
        public const int
            RI_KEY_MAKE            = 0,
            RI_KEY_BREAK           = 1,
            RI_KEY_E0              = 2,
            RI_KEY_E1              = 4,
            RI_KEY_TERMSRV_SET_LED = 8,
            RI_KEY_TERMSRV_SHADOW  = 0x10;


        // Define the mouse button state indicators.
        public const int
            RI_MOUSE_LEFT_BUTTON_DOWN   = 0x0001,  // Left Button changed to down.
            RI_MOUSE_LEFT_BUTTON_UP     = 0x0002,  // Left Button changed to up.
            RI_MOUSE_RIGHT_BUTTON_DOWN  = 0x0004,  // Right Button changed to down.
            RI_MOUSE_RIGHT_BUTTON_UP    = 0x0008,  // Right Button changed to up.
            RI_MOUSE_MIDDLE_BUTTON_DOWN = 0x0010,  // Middle Button changed to down.
            RI_MOUSE_MIDDLE_BUTTON_UP   = 0x0020;  // Middle Button changed to up.

        public const int
            RI_MOUSE_BUTTON_1_DOWN      = RI_MOUSE_LEFT_BUTTON_DOWN,
            RI_MOUSE_BUTTON_1_UP        = RI_MOUSE_LEFT_BUTTON_UP,
            RI_MOUSE_BUTTON_2_DOWN      = RI_MOUSE_RIGHT_BUTTON_DOWN,
            RI_MOUSE_BUTTON_2_UP        = RI_MOUSE_RIGHT_BUTTON_UP,
            RI_MOUSE_BUTTON_3_DOWN      = RI_MOUSE_MIDDLE_BUTTON_DOWN,
            RI_MOUSE_BUTTON_3_UP        = RI_MOUSE_MIDDLE_BUTTON_UP;

        public const int
            RI_MOUSE_BUTTON_4_DOWN      = 0x0040,
            RI_MOUSE_BUTTON_4_UP        = 0x0080,
            RI_MOUSE_BUTTON_5_DOWN      = 0x0100,
            RI_MOUSE_BUTTON_5_UP        = 0x0200;

        /*
         * If usButtonFlags has RI_MOUSE_WHEEL, the wheel delta is stored in usButtonData.
         * Take it as a signed value.
         */
        public const int RI_MOUSE_WHEEL = 0x0400;

        /// <summary>
        /// RAWMOUSE.usFlags
        /// </summary>
        public const int
            MOUSE_MOVE_RELATIVE      = 0x00,
            MOUSE_MOVE_ABSOLUTE      = 0x01,
            MOUSE_VIRTUAL_DESKTOP    = 0x02,  // the coordinates are mapped to the virtual desktop
            MOUSE_ATTRIBUTES_CHANGED = 0x04,  // requery for mouse attributes
            MOUSE_MOVE_NOCOALESCE    = 0x08;  // do not coalesce mouse moves

        // The number of multiples the mouse wheel moves per index value
        public const int
            WHEEL_DELTA = 120;


        // Used with SendInput
        // They look very similar to RAW Input, but they are completely different
        // Input type for INPUT structure, related to SendInput
        public const int INPUT_MOUSE     = 0;
        public const int INPUT_KEYBOARD  = 1;
        public const int INPUT_HARDWARE  = 2;

        public const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        public const int KEYEVENTF_KEYUP       = 0x0002;
        public const int KEYEVENTF_UNICODE     = 0x0004;
        public const int KEYEVENTF_SCANCODE    = 0x0008;

        public const int MOUSEEVENTF_MOVE           = 0x0001; /* mouse move */
        public const int MOUSEEVENTF_LEFTDOWN       = 0x0002; /* left button down */
        public const int MOUSEEVENTF_LEFTUP         = 0x0004; /* left button up */
        public const int MOUSEEVENTF_RIGHTDOWN      = 0x0008; /* right button down */
        public const int MOUSEEVENTF_RIGHTUP        = 0x0010; /* right button up */
        public const int MOUSEEVENTF_MIDDLEDOWN     = 0x0020; /* middle button down */
        public const int MOUSEEVENTF_MIDDLEUP       = 0x0040; /* middle button up */
        public const int MOUSEEVENTF_XDOWN          = 0x0080; /* x button down */
        public const int MOUSEEVENTF_XUP            = 0x0100; /* x button down */
        public const int MOUSEEVENTF_WHEEL          = 0x0800; /* wheel button rolled */
        public const int MOUSEEVENTF_HWHEEL         = 0x01000; /* hwheel button rolled */
        public const int MOUSEEVENTF_MOVE_NOCOALESCE = 0x2000; /* do not coalesce mouse moves */
        public const int MOUSEEVENTF_VIRTUALDESK    = 0x4000; /* map to entire virtual desktop */
        public const int MOUSEEVENTF_ABSOLUTE       = 0x8000; /* absolute move */

        // SetWindowPos Flags
        public const int
            SWP_NOSIZE          = 0x0001,
            SWP_NOMOVE          = 0x0002,
            SWP_NOZORDER        = 0x0004,
            SWP_NOREDRAW        = 0x0008,
            SWP_NOACTIVATE      = 0x0010,
            SWP_FRAMECHANGED    = 0x0020, /* The frame changed: send WM_NCCALCSIZE */
            SWP_SHOWWINDOW      = 0x0040,
            SWP_HIDEWINDOW      = 0x0080,
            SWP_NOCOPYBITS      = 0x0100,
            SWP_NOOWNERZORDER   = 0x0200,  /* Don't do owner Z ordering */
            SWP_NOSENDCHANGING  = 0x0400,  /* Don't send WM_WINDOWPOSCHANGING */

            SWP_DRAWFRAME       = SWP_FRAMECHANGED,
            SWP_NOREPOSITION    = SWP_NOOWNERZORDER,

            SWP_DEFERERASE      = 0x2000,
            SWP_ASYNCWINDOWPOS  = 0x4000;

        /*
        * Virtual Keys, Standard Set
        */
        public const int
            VK_LBUTTON      =  0x01,
            VK_RBUTTON      =  0x02,
            VK_CANCEL       =  0x03,
            VK_MBUTTON      =  0x04,    /* NOT contiguous with L & RBUTTON */

            VK_XBUTTON1     =  0x05,    /* NOT contiguous with L & RBUTTON */
            VK_XBUTTON2     =  0x06;    /* NOT contiguous with L & RBUTTON */

/*
 * 0x07 : unassigned
 */
        public const int
            VK_BACK         =  0x08,
            VK_TAB          =  0x09;

/*
 * 0x0A - 0x0B : reserved
 */
        public const int
            VK_CLEAR        =  0x0C,
            VK_RETURN       =  0x0D,

            VK_SHIFT        =  0x10,
            VK_CONTROL      =  0x11,
            VK_MENU         =  0x12,
            VK_PAUSE        =  0x13,
            VK_CAPITAL      =  0x14,

            VK_KANA         =  0x15,  // VK_HANGEUL        0x15  // old name - should be here for compatibility
            
            VK_HANGUL       =  0x15,
            VK_JUNJA        =  0x17,
            VK_FINAL        =  0x18,
            VK_HANJA        =  0x19,
            VK_KANJI        =  0x19,

            VK_ESCAPE       =  0x1B,

            VK_CONVERT      =  0x1C,
            VK_NONCONVERT   =  0x1D,
            VK_ACCEPT       =  0x1E,
            VK_MODECHANGE   =  0x1F;

        public const int
            VK_SPACE        =  0x20,
            VK_PRIOR        =  0x21,
            VK_NEXT         =  0x22,
            VK_END          =  0x23,
            VK_HOME         =  0x24,
            VK_LEFT         =  0x25,
            VK_UP           =  0x26,
            VK_RIGHT        =  0x27,
            VK_DOWN         =  0x28,
            VK_SELECT       =  0x29,
            VK_PRINT        =  0x2A,
            VK_EXECUTE      =  0x2B,
            VK_SNAPSHOT     =  0x2C,
            VK_INSERT       =  0x2D,
            VK_DELETE       =  0x2E,
            VK_HELP         =  0x2F;

/*
 * VK_0 - VK_9 are the same as ASCII '0' - '9' (0x30 - 0x39)
 * 0x40 : unassigned
 * VK_A - VK_Z are the same as ASCII 'A' - 'Z' (0x41 - 0x5A)
 */
        public const int
            VK_LWIN         =  0x5B,
            VK_RWIN         =  0x5C,
            VK_APPS         =  0x5D;

/*
 * 0x5E : reserved
 */
        public const int
            VK_SLEEP        =  0x5F;

        public const int
            VK_NUMPAD0      =  0x60,
            VK_NUMPAD1      =  0x61,
            VK_NUMPAD2      =  0x62,
            VK_NUMPAD3      =  0x63,
            VK_NUMPAD4      =  0x64,
            VK_NUMPAD5      =  0x65,
            VK_NUMPAD6      =  0x66,
            VK_NUMPAD7      =  0x67,
            VK_NUMPAD8      =  0x68,
            VK_NUMPAD9      =  0x69,
            VK_MULTIPLY     =  0x6A,
            VK_ADD          =  0x6B,
            VK_SEPARATOR    =  0x6C,
            VK_SUBTRACT     =  0x6D,
            VK_DECIMAL      =  0x6E,
            VK_DIVIDE       =  0x6F,
            VK_F1           =  0x70,
            VK_F2           =  0x71,
            VK_F3           =  0x72,
            VK_F4           =  0x73,
            VK_F5           =  0x74,
            VK_F6           =  0x75,
            VK_F7           =  0x76,
            VK_F8           =  0x77,
            VK_F9           =  0x78,
            VK_F10          =  0x79,
            VK_F11          =  0x7A,
            VK_F12          =  0x7B,
            VK_F13          =  0x7C,
            VK_F14          =  0x7D,
            VK_F15          =  0x7E,
            VK_F16          =  0x7F,
            VK_F17          =  0x80,
            VK_F18          =  0x81,
            VK_F19          =  0x82,
            VK_F20          =  0x83,
            VK_F21          =  0x84,
            VK_F22          =  0x85,
            VK_F23          =  0x86,
            VK_F24          =  0x87;

/*
 * 0x88 - 0x8F : unassigned
 */
        public const int
            VK_NUMLOCK      =  0x90,
            VK_SCROLL       =  0x91;

/*
 * NEC PC-9800 kbd definitions
 */
        public const int
            VK_OEM_NEC_EQUAL = 0x92;   // '=' key on numpad

/*
 * Fujitsu/OASYS kbd definitions
 */
        public const int
            VK_OEM_FJ_JISHO   = 0x92,   // 'Dictionary' key
            VK_OEM_FJ_MASSHOU = 0x93,   // 'Unregister word' key
            VK_OEM_FJ_TOUROKU = 0x94,   // 'Register word' key
            VK_OEM_FJ_LOYA    = 0x95,   // 'Left OYAYUBI' key
            VK_OEM_FJ_ROYA    = 0x96;   // 'Right OYAYUBI' key

/*
 * 0x97 - 0x9F : unassigned
 */

/*
 * VK_L* & VK_R* - left and right Alt, Ctrl and Shift virtual keys.
 * Used only as parameters to GetAsyncKeyState() and GetKeyState().
 * No other API or message will distinguish left and right keys in this way.
 */
        public const int
            VK_LSHIFT        = 0xA0,
            VK_RSHIFT        = 0xA1,
            VK_LCONTROL      = 0xA2,
            VK_RCONTROL      = 0xA3,
            VK_LMENU         = 0xA4,
            VK_RMENU         = 0xA5;

        public const int
            VK_BROWSER_BACK       = 0xA6,
            VK_BROWSER_FORWARD    = 0xA7,
            VK_BROWSER_REFRESH    = 0xA8,
            VK_BROWSER_STOP       = 0xA9,
            VK_BROWSER_SEARCH     = 0xAA,
            VK_BROWSER_FAVORITES  = 0xAB,
            VK_BROWSER_HOME       = 0xAC;

        public const int
            VK_VOLUME_MUTE        = 0xAD,
            VK_VOLUME_DOWN        = 0xAE,
            VK_VOLUME_UP          = 0xAF,
            VK_MEDIA_NEXT_TRACK   = 0xB0,
            VK_MEDIA_PREV_TRACK   = 0xB1,
            VK_MEDIA_STOP         = 0xB2,
            VK_MEDIA_PLAY_PAUSE   = 0xB3,
            VK_LAUNCH_MAIL        = 0xB4,
            VK_LAUNCH_MEDIA_SELECT= 0xB5,
            VK_LAUNCH_APP1        = 0xB6,
            VK_LAUNCH_APP2        = 0xB7;


/*
 * 0xB8 - 0xB9 : reserved
 */
        public const int
            VK_OEM_1        =  0xBA,   // ';:' for US
            VK_OEM_PLUS     =  0xBB,   // '+' any country
            VK_OEM_COMMA    =  0xBC,   // ',' any country
            VK_OEM_MINUS    =  0xBD,   // '-' any country
            VK_OEM_PERIOD   =  0xBE,   // '.' any country
            VK_OEM_2        =  0xBF,   // '/?' for US
            VK_OEM_3        =  0xC0;   // '`~' for US

/*
 * 0xC1 - 0xD7 : reserved
 */

/*
 * 0xD8 - 0xDA : unassigned
 */
        public const int
            VK_OEM_4         = 0xDB,  //  '[{' for US
            VK_OEM_5         = 0xDC,  //  '\|' for US
            VK_OEM_6         = 0xDD,  //  ']}' for US
            VK_OEM_7         = 0xDE,  //  ''"' for US
            VK_OEM_8         = 0xDF;

/*
 * 0xE0 : reserved
 */

/*
 * Various extended or enhanced keyboards
 */
        public const int
            VK_OEM_AX       =  0xE1,  //  'AX' key on Japanese AX kbd
            VK_OEM_102      =  0xE2, //  "<>" or "\|" on RT 102-key kbd.
            VK_ICO_HELP     =  0xE3,  //  Help key on ICO
            VK_ICO_00       =  0xE4,  //  00 key on ICO
            VK_PROCESSKEY   =  0xE5,
            VK_ICO_CLEAR    =  0xE6,
            VK_PACKET       = 0xE7;

/*
 * 0xE8 : unassigned
 */

/*
 * Nokia/Ericsson definitions
 */
        public const int
            VK_OEM_RESET    =  0xE9,
            VK_OEM_JUMP     =  0xEA,
            VK_OEM_PA1      =  0xEB,
            VK_OEM_PA2      =  0xEC,
            VK_OEM_PA3      =  0xED,
            VK_OEM_WSCTRL   =  0xEE,
            VK_OEM_CUSEL    =  0xEF,
            VK_OEM_ATTN     =  0xF0,
            VK_OEM_FINISH   =  0xF1,
            VK_OEM_COPY     =  0xF2,
            VK_OEM_AUTO     =  0xF3,
            VK_OEM_ENLW     =  0xF4,
            VK_OEM_BACKTAB  =  0xF5,

            VK_ATTN         =  0xF6,
            VK_CRSEL        =  0xF7,
            VK_EXSEL        =  0xF8,
            VK_EREOF        =  0xF9,
            VK_PLAY         =  0xFA,
            VK_ZOOM         =  0xFB,
            VK_NONAME       =  0xFC,
            VK_PA1          =  0xFD,
            VK_OEM_CLEAR    =  0xFE;

/*
 * 0xFF : reserved
 */
        // For the wParam of WM_DEVICECHANGE
        public const int DEVICE_ARRIVAL         = 0x8000;
        public const int DEVICE_REMOVECOMPLETE  = 0x8004;

    }
}
