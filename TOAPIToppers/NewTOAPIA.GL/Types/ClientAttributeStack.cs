using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    public class ClientAttributeStack : GIObject
    {
        public ClientAttributeStack(GraphicsInterface gi)
            : base(gi)
        {
        }

        public void Pop()
        {
            GI.PopClientAttrib();
        }

        public void Push(ClientAttribMask mask)
        {
            GI.PushClientAttrib(mask);
        }

        public void PushPixelStore()
        {
            Push(ClientAttribMask.ClientPixelStoreBit);
        }

        public void PushVertexArray()
        {
            Push(ClientAttribMask.ClientVertexArrayBit);
        }
    }
}
