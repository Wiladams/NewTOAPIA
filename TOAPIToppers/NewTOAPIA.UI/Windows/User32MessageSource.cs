
using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.User32;
using TOAPI.Kernel32;

namespace NewTOAPIA.UI
{
    public delegate void IdleHandler();

	/// <summary>
	/// A MessageLooper is simply a object that has a message
	/// queue, and it runs a continuous loop trying to pull the
	/// messages off and dispatch them.
	/// 
	/// This base class is intended to be windowless, so it can
	/// be setup to receive messages without incurring the cost
	/// of allocating any real windowing resources.
	/// 
	/// A new thread is created to process the messages, so 
	/// this is a easy way to do multi-threaded programming.
	/// A typical usage would be to subclass a message looper, 
	/// and do message specific processing within the Dispatch()
	/// method.
	/// 
	/// </summary>
    /// <remarks>
    /// It might be nice to add some delegate capability so
    /// that a user can register a delegate to handle specific
    /// types of messages.  That way, no subclassing is required.
    /// </remarks>
    /// 
	public class User32MessageSource : Observable<MSG>, IHandle
	{
        protected static string gWindowClassName = "MSGLooper";

        User32WindowClass fWindowClass;
        WindowProfile fWindowProfile;


        private IntPtr fHandle;
        private Thread fThread;
		private uint fThreadID;
        private WindowProc fWindowProc;
        AutoResetEvent fQueueReady = new AutoResetEvent(false);
        public event IdleHandler IdleEvent;

		public User32MessageSource()
            :this(new User32WindowClass(gWindowClassName, User32.CS_GLOBALCLASS, null), null)
		{
   		}

        public User32MessageSource(User32WindowClass wndClass, WindowProfile wndProfile)
        {
            fWindowProc = new WindowProc(this.Callback);
            
            fWindowClass = wndClass;
            fWindowProfile = wndProfile;

            fWindowClass.CallbackProcedure = fWindowProc;

            fWindowClass.RegisterWindowClass();

        }

        public virtual IntPtr Handle
        {
            get { return fHandle; }
        }

        public virtual void Start()
		{
            // Create the new thread for this message looper.
			fThread = new Thread(ThreadRoutine);
            fThread.IsBackground = true;

            // And the start the thread going.
			fThread.Start();
            
			// Wait for it to be alive before returning so that
			// messages can be posted to it until it's ready.
            // We'll wait at least until the handle to the window
            // has been created.
			fQueueReady.WaitOne();
		}

        public virtual User32WindowWrapper CreateWindow(string title, IntPtr parentWindow, WindowProfile winProfile)
        {
            IntPtr appInstance = Kernel32.GetModuleHandle(null);

            // The following sequence of messages will come in through the callback
            // method before this function returns:
            // WM_GETMINMAXINFO
            // WM_NCCREATE
            // WM_NCCALCSIZE
            // WM_CREATE
            IntPtr titlePtr = Marshal.StringToHGlobalAuto(title);

            fHandle = User32.CreateWindowEx(
                (int)fWindowProfile.ExtendedWindowStyle,
                fWindowClass.ClassNamePointer,
                titlePtr,
                (int)fWindowProfile.WindowStyle,
                fWindowProfile.X,
                fWindowProfile.Y,
                fWindowProfile.Width,
                fWindowProfile.Height,
                parentWindow,
                IntPtr.Zero,
                appInstance,
                IntPtr.Zero);

            User32WindowWrapper wrapper = new User32WindowWrapper(fHandle);

            return wrapper;
        }

        //private void CreateWindow()
        //{
        //    IntPtr appInstance = Kernel32.GetModuleHandle(null);
            
        //    //WindowProfile wProfile = new WindowProfile(WindowStyle.Overlapped, ExtendedWindowStyle.TopMost);
        //    //fWindowClass.CreateWindow("MSGLooper", System.Drawing.Rectangle.Empty, new IntPtr(User32.HWND_MESSAGE), wProfile);

        //    fHandle = User32.CreateWindowEx(
        //        0,
        //        gWindowClassName,
        //        "MSGLooperWindowTitle",
        //        0, 
        //        0, 0, 0, 0,
        //        new IntPtr(User32.HWND_MESSAGE),    // We only want to receive messages
        //        IntPtr.Zero,
        //        appInstance,
        //        null);
        //}

        /// <summary>
        /// The routines within ThreadRoutine are running in
        /// their own thread.
        /// We will only receive messages sent specifically to the 'window' handle
        /// that we have registered, 
        /// and general thread messages.
        /// </summary>
        private void ThreadRoutine()
        {
            IntPtr parentWindow = IntPtr.Zero;

            CreateWindow(fWindowProfile.Title, parentWindow, fWindowProfile);
            fThreadID = Kernel32.GetCurrentThreadId();
            
            // Post a single message to ensure that the
            // message queue is setup.
            User32.PostMessage(fHandle, (int)WinMsg.WM_NULL, 0, 0);

            // Even though this window is not a visiable one, doing
            // the following two calls will ensure that a couple of messages
            // come through the message pump, so we know we can move on with things.
            //User32.ShowWindow(fHandle, (int)ShowHide.ShowNormal);
            //User32.UpdateWindow(fHandle);

            Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        /// <remarks>
        /// The sequence of messages that we can expect to see are:
        /// WM_GETMINMAXINFO
        /// WM_NCCREATE
        /// WM_NCCALCSIZE
        /// WM_CREATE
        /// In that specific order.
        /// </remarks>
        public virtual IntPtr Callback(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            IntPtr retValue = IntPtr.Zero;

            MSG aMsg = MSG.Create(hWnd, msg, wParam, lParam);

            //Console.WriteLine("User32MessageSource Callback: {0}", aMsg);

            // These are the only standard messages we should see.
            switch (msg)
            {
                    // Mouse
                case (int)WinMsg.WM_MOUSEHOVER:
                case (int)WinMsg.WM_MOUSELEAVE:
                case (int)WinMsg.WM_MOUSEMOVE:
                case (int)WinMsg.WM_MOUSEWHEEL:
                case (int)WinMsg.WM_LBUTTONDBLCLK:
                case (int)WinMsg.WM_LBUTTONDOWN:
                case (int)WinMsg.WM_LBUTTONUP:
                case (int)WinMsg.WM_MBUTTONDBLCLK:
                case (int)WinMsg.WM_MBUTTONDOWN:
                case (int)WinMsg.WM_MBUTTONUP:
                case (int)WinMsg.WM_RBUTTONDBLCLK:
                case (int)WinMsg.WM_RBUTTONDOWN:
                case (int)WinMsg.WM_RBUTTONUP:
                    DispatchData(aMsg);
                    break;

                    // Keyboard
                //case MSG.WM_SYSKEYDOWN:
                //case MSG.WM_SYSKEYUP:
                //case MSG.WM_SYSCHAR:
                case (int)WinMsg.WM_KEYDOWN:
                case (int)WinMsg.WM_KEYUP:
                case (int)WinMsg.WM_CHAR:   // these are generated from TranslateMessage after WM_KEYDOWN
                    DispatchData(aMsg);
                    break;

                case (int)WinMsg.WM_MOVE:
                case (int)WinMsg.WM_MOVING:
                case (int)WinMsg.WM_SIZE:
                case (int)WinMsg.WM_SIZING:
                case (int)WinMsg.WM_EXITSIZEMOVE:
                    DispatchData(aMsg);
                    break;

                case (int)WinMsg.WM_COMMAND:
                case (int)WinMsg.WM_INPUT:
                case (int)WinMsg.WM_PAINT:
                case (int)WinMsg.WM_TIMER:
                case (int)WinMsg.WM_CLOSE:
                case (int)WinMsg.WM_ENABLE:
                case (int)WinMsg.WM_ERASEBKGND:
                case (int)WinMsg.WM_KILLFOCUS:
                case (int)WinMsg.WM_SETFOCUS:
                case (int)WinMsg.WM_DESTROY:
                case (int)WinMsg.WM_NCDESTROY:
                    DispatchData(aMsg);
                    break;

                //case (int)WinMsg.WM_GETMINMAXINFO:
                //case (int)WinMsg.WM_NCCALCSIZE:
                case (int)WinMsg.WM_SETCURSOR:
                case (int)WinMsg.WM_NCHITTEST:
                    retValue = User32.DefWindowProc(hWnd, msg, wParam, lParam);
                    return retValue;

                case (int)WinMsg.WM_CREATE:
                case (int)WinMsg.WM_NCCREATE:
                    retValue = User32.DefWindowProc(hWnd, msg, wParam, lParam);
                    return retValue;


                default:
                    retValue = User32.DefWindowProc(hWnd, msg, wParam, lParam);
                    break;
            }

            return retValue;
        }

		public bool PostMessage(uint aMessage)
		{
			bool result = User32.PostThreadMessage(fThreadID,aMessage, IntPtr.Zero, IntPtr.Zero) != 0;
			
            return result;
		}

		// Two ways to quit.
		/// <summary>
		/// Two ways to quit.
		/// 1) Call the Stop() method directly.
		/// 2) PostMessage(WM_QUIT)
		/// 
		/// The PostMessage method is probably better.
		/// </summary>
		public int Quit()
		{
            bool result = PostMessage((int)WinMsg.WM_QUIT);

			return 0;
		}

	    /// <summary>
	    /// this is the heart of the message loop.  In this routine, we'll basically loop
        /// on a Get/Translate/Dispatch sequence until PostQuiteMessage is received.
        /// At that point, GetMessage will return false, and we'll drop out of the loop.
	    /// </summary>
    	public void Run()
		{
			MSG msg = new MSG();

			// Get the thread specific message queue setup by peeking
			// for a initial message.
            int peeked = User32.PeekMessage(out msg, IntPtr.Zero,
                (int)WinMsg.WM_NULL, (int)WinMsg.WM_NULL,
                User32.PM_REMOVE);

            fQueueReady.Set();

            //// While there are messages in the queue, get them out
            //// and send them onward for processing.
            //// This loop will be terminated by a PostQuitMessage() call
            //// from our Stop() method.
            //int retValue;
            //while ((retValue = User32.GetMessage(out msg, IntPtr.Zero, 0, 0)) != 0)
            //{
            //    DispatchData(msg);
            //}

            int retValue = 0;
            while ((retValue = User32.GetMessage(out msg, IntPtr.Zero, 0, 0)) != 0)
            {
                if (retValue == -1)
                    throw new Exception("GetMessage returned a -1 value");

                User32.TranslateMessage(ref msg);
                User32.DispatchMessage(ref msg);
            }
		}
	}
}