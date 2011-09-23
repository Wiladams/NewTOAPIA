using System;

namespace Autometaii
{
    [Serializable]
    public class Autometus : IAutometus
    {
        public virtual void ReceiveData(byte[] data)
        {
            ReceiveData(data, 0, data.Length);
        }

        public virtual void ReceiveData(byte[] data, int offset, int count)
        {
        }

        public virtual void ReceiveCommand(ICommand command)
        {
        }
    }
}
