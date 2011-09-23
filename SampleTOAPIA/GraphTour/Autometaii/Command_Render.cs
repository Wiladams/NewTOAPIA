using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;

namespace Autometaii
{
    public class Command_Render : ICommand
    {
        GDIRenderer fGraphPort;

        public Command_Render(GDIRenderer graphPort)
        {
            fGraphPort = graphPort;
        }

        public Guid CommandID
        {
            get { return Guid.Empty; }
        }

        public GDIRenderer GraphPort
        {
            get { return fGraphPort; }
        }
    }
}
