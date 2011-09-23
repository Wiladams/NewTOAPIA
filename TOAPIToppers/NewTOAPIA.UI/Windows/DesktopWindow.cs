using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.User32;
using NewTOAPIA.Drawing;

namespace NewTOAPIA.UI
{
    using NewTOAPIA.Drawing.GDI;

    public class DesktopWindow : User32WindowWrapper
    {
        GDIContext fShellBackgroundContext;
        GDIContext fDesktopContext;

        public DesktopWindow()
            :base(User32.GetDesktopWindow())
        {
        }

        public GDIContext ShellBackgroundContext
        {
            get
            {
                if (null == fShellBackgroundContext)
                    fShellBackgroundContext = GDIContext.CreateForDesktopBackground();

                return fShellBackgroundContext;
            }
        }

        public override GDIContext DeviceContextClientArea
        {
            get
            {
                if (null == fDesktopContext)
                {
                    fDesktopContext = GDIContext.CreateForDefaultDisplay();
                }

                return fDesktopContext;
            }
        }
    }
}
