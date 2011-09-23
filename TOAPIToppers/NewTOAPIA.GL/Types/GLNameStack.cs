using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    public class GLNameStack : GIObject
    {

        public GLNameStack(GraphicsInterface gi)
            :base(gi)
        {
            InitNames();
        }

        void InitNames()
        {
            GI.InitNames();
        }

        public void LoadName(uint name)
        {
            GI.LoadName(name);
        }

        public void PopName()
        {
            GI.PopName();
        }

        public void PushName(uint name)
        {
            GI.PushName(name);
        }

    }
}
