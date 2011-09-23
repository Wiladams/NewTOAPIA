using System;
using System.Collections.Generic;
using System.Text;
using NewTOAPIA.Drawing;

namespace Autometaii
{
    public class Command_Render : ICommand
    {
        IGraphPort fGraphPort;

        public Command_Render(IGraphPort graphPort)
        {
            fGraphPort = graphPort;
        }

        public Guid CommandID
        {
            get { return Guid.Empty; }
        }

        public IGraphPort GraphPort
        {
            get { return fGraphPort; }
        }
    }
}
