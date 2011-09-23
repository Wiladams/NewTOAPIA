using System;
using System.Collections.Generic;
using System.Text;

namespace Autometaii
{
    interface IAutometus
    {
        void ReceiveData(byte[] data);
        void ReceiveData(byte[] data, int offset, int length);
        void ReceiveCommand(ICommand aCommand);
    }
}
