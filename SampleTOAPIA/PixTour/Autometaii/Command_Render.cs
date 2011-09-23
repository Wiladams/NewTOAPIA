using System;
using System.Collections.Generic;
using System.Text;

namespace Autometaii
{
    public class Command_Render : ICommand
    {
        IRenderGDI fGraphPort;

        public Command_Render(IRenderGDI graphPort)
        {
            fGraphPort = graphPort;
        }

        public Guid CommandID
        {
            get { return Guid.Empty; }
        }

        public IRenderGDI GraphPort
        {
            get { return fGraphPort; }
        }
    }
}
