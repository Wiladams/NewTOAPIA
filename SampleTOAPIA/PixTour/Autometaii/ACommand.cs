using System;
using System.Collections.Generic;
using System.Text;

namespace Autometaii
{
    public enum ABaseCommand : int
    {
        Default = 0,
        Run = 1,
        Stop = 2,
        Pause = 3,
    }

    [Serializable]
    public class ACommand : ICommand
    {
        Guid fCommandID;

        protected ACommand(Guid commandID)
        {
            fCommandID = commandID;
        }

        public Guid CommandID
        {
            get { return fCommandID; }
        }

    }
}
