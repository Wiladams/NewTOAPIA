using System;

namespace NewTOAPIA.GL
{
    public class GLRenderer
    {
        GraphicsInterface fGI;

        public GLRenderer(GraphicsInterface gi)
        {
            fGI = gi;
        }

        #region Properties
        public GraphicsInterface GI
        {
            get { return fGI; }
        }
        #endregion

        #region Public Methods
        public void Render(GLDisplayList displayList)
        {
            GI.CallList(displayList.ListID);
        }
        #endregion
    }
}
