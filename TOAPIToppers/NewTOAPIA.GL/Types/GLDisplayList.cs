using NewTOAPIA.GL;

namespace NewTOAPIA.GL
{
    public class GLDisplayList : GIObject, IBracket
    {
        public uint ListID { get; protected set; }

        GraphicsInterface fGI;

        public GLDisplayList(GraphicsInterface gi)
            :base(gi)
        {
            fGI = gi;
            ListID = gi.GenLists(1);
        }

        public void Begin()
        {
            Begin(ListMode.Compile);
        }

        public void BeginCompile()
        {
            Begin(ListMode.Compile);
        }

        public void BeginCompileAndExecute()
        {
            Begin(ListMode.CompileAndExecute);
        }

        void Begin(ListMode mode)
        {
            fGI.NewList(ListID, mode);
        }

        public void End()
        {
            fGI.EndList();
        }

        public virtual void Render(GraphicsInterface gi)
        {
            gi.CallList(ListID);
        }


        ~GLDisplayList()
        {
            // Need to be careful here.  It is possible that the context
            // has been destroyed before this gets called, in which case
            // an access violation will occur.
            //GL.DeleteLists(fListID, 1);
        }
    }
}
