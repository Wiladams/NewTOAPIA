using System;
using System.Collections.Generic;
using System.Text;

namespace Autometaii
{
    public interface ICommand
    {
        Guid CommandID { get; }
    }
}
