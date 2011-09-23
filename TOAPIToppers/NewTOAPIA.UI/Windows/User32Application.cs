using System;

using TOAPI.Types;
using TOAPI.User32;

using NewTOAPIA.UI;

namespace NewTOAPIA.UI
{
    public class User32Application
    {
        Window fMainWindow;

        public virtual void Run()
        {
            Run(null);
        }

        public virtual void Run(Window mainWindow)
        {
            fMainWindow = mainWindow;

            if (fMainWindow != null)
            {
                fMainWindow.IsApplicationWindow = true;
                fMainWindow.Show();
                //fMainWindow.Update();
            }

            MSG msg = new MSG();

            int retValue =0;
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
