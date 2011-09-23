using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Net
{
    abstract public class MACAddress
    {
        public abstract byte[] GetBytes();

    }

    public class MAC48Address : MACAddress
    {
        public override byte[] GetBytes()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Extended Unique Identifier
    /// </summary>
    public class EUI48Address : MACAddress
    {
        public override byte[] GetBytes()
        {
            throw new NotImplementedException();
        }
    }

    public class EUI64Address : MACAddress
    {
        public override byte[] GetBytes()
        {
            throw new NotImplementedException();
        }
    }
}
