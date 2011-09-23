using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL.Types
{
    public class ServerAttributeStack : GIObject
    {
        public ServerAttributeStack(GraphicsInterface gi)
            : base(gi)
        {
        }

        public void Pop()
        {
            GI.PopAttrib();
        }

        public void Push(AttribMask mask)
        {
            GI.PushAttrib(mask);
        }

    }
}
