using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

using TOAPI.Kernel32;
using TOAPI.Types;
using TOAPI.User32;

using NewTOAPIA.Drawing;

namespace NewTOAPIA.UI
{
    /// <summary>
    /// The User32WindowClass represents a Window Class.  This class holds information
    /// that is found in the WNDCLASS record.  
    /// 
    /// The usage pattern is to create an instance of this class to represent a class of Window,
    /// in the strict User32 sense of the word.  To create specific instances of windows of this
    /// particular class, you call the CreateWindow() method.
    /// </summary>
    public class User32WindowClass
    {
        #region Fields
        bool fClassRegistered;
        private ushort fClassAtom;		// atom representing class registration

        // This set of fields are used to initialize and register
        // the window class.
        private WNDCLASSEX fWndClass;  // Data structure holding class information

        string fClassName;
        public IntPtr ClassNamePointer { get; set; }
        IntPtr fAppInstance;
        #endregion

        #region Constructors
        public User32WindowClass(string className, uint classStyle, WindowProc wndProc)
        {
            fClassName = className;
            ClassNamePointer = Marshal.StringToHGlobalAuto(className);
            fAppInstance = Marshal.GetHINSTANCE(typeof(User32WindowClass).Module);

            // Create the WNDCLASS structure.
            // We don't register it here because we allow the user to change
            // properties before registering the class.
            fWndClass = new WNDCLASSEX();
            fWndClass.Init();

            // Although the application instance is not passed in the
            // constructor, it is not likely to change.
            fAppInstance = Kernel32.GetModuleHandle(null);
            fWndClass.hInstance = fAppInstance;

            // These can change, but since they're passed into the constructor
            // they should already have the desired values.
            fWndClass.lpszClassName = ClassNamePointer;
            fWndClass.lpfnWndProc = wndProc;
            fWndClass.style = (int)classStyle;

            // The user might want to change any of these as they are not passed
            // into the constructor.
            fWndClass.hbrBackground = (IntPtr)(User32.COLOR_BACKGROUND + 1);
            fWndClass.hCursor = IntPtr.Zero;
            fWndClass.lpszMenuName = IntPtr.Zero;
            fWndClass.hIcon = IntPtr.Zero;
            fWndClass.hIconSm = IntPtr.Zero;

            // These are not likely to be used at all
            fWndClass.cbClsExtra = 0;
            fWndClass.cbWndExtra = 0;

        }
        #endregion

        #region Properties
        public IntPtr BackgroundBrush
        {
            get { return fWndClass.hbrBackground; }
            set
            {
                if (!fClassRegistered)
                    fWndClass.hbrBackground = value;
            }
        }

        public uint ClassStyle
        {
            get { return (uint)fWndClass.style; }
            set
            {
                if (!fClassRegistered)
                    fWndClass.style = (int)value;
            }
        }

        public WindowProc CallbackProcedure
        {
            get { return fWndClass.lpfnWndProc; }
            set { fWndClass.lpfnWndProc = value; }
        }
        #endregion

        /// <summary>
        /// Register the window class so that windows can be created based on this class.
        /// </summary>
        public virtual void RegisterWindowClass()
        {
            if (fClassRegistered)
                return;

            // Register the window class. 
            fClassAtom = User32.RegisterClassEx(ref fWndClass);
            uint error = Kernel32.GetLastError();

            if (fClassAtom == 0)
            {
                throw new Exception("Could not register Window Class");
            }

            fClassRegistered = true;
        }

        public virtual void UnregisterWindowClass()
        {
            if (!fClassRegistered)
                return;

            User32.UnregisterClass(fWndClass.lpszClassName, fAppInstance);
        }


        public virtual User32WindowWrapper CreateWindow(string title, Rectangle frame, IntPtr parentWindow, WindowProfile winProfile)
        {
            // The following sequence of messages will come in through the callback
            // method before this function returns:
            // WM_GETMINMAXINFO
            // WM_NCCREATE
            // WM_NCCALCSIZE
            // WM_CREATE
            IntPtr titlePtr = Marshal.StringToHGlobalAuto(title);

            IntPtr windowHandle = User32.CreateWindowEx(
                (int)winProfile.ExtendedWindowStyle,
                fWndClass.lpszClassName,
                titlePtr,
                (int)winProfile.WindowStyle,
                frame.X,
                frame.Y,
                frame.Width,
                frame.Height,
                parentWindow,
                IntPtr.Zero,
                fAppInstance,
                IntPtr.Zero);

            User32WindowWrapper wrapper = new User32WindowWrapper(windowHandle);

            return wrapper;
        }
    }
}