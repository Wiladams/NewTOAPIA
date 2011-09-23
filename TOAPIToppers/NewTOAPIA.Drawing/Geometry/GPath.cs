
namespace NewTOAPIA.Drawing
{
    using System;
    using System.Collections.Generic;

    using NewTOAPIA.Graphics;

    public class GPath : IBracket
    {
        #region Fields
        Guid fUniqueID;

        Point3D[] fVertices;
        List<Point3D> fVertexList;
        byte[] fCommands;
        List<byte> fCommandList;

        bool fIsFrozen;
        #endregion

        #region Constructor
        public GPath()
            : this(Guid.NewGuid())
        {
        }

        public GPath(Guid uniqueID)
        {
            fUniqueID = uniqueID;

            fIsFrozen = false;

            fVertexList = new List<Point3D>();
            fCommandList = new List<byte>();
        }

        public GPath(Point3D[] vertices, byte[] commands)
        {
            SetPath(vertices, commands);
        }
        #endregion

        #region Properties
        public bool IsFrozen
        {
            get { return fIsFrozen; }
        }

        public Point3D[] Vertices
        {
            get { return fVertices; }
        }

        public byte[] Commands
        {
            get { return fCommands; }
        }
        #endregion

        #region Methods
        protected void SetPath(Point3D[] vertices, byte[] commands)
        {
            fVertices = vertices;
            fCommands = commands;

            fIsFrozen = true;
        }

        public virtual void AddVertex(Point3D aPoint, GDIPathCommand aCommand, bool closeFigure)
        {
            if (closeFigure)
                aCommand |= GDIPathCommand.CloseFigure;

            fVertexList.Add(aPoint);
            fCommandList.Add((byte)aCommand);
        }

        public virtual void MoveTo(double x, double y, bool closeFigure)
        {
            AddVertex(new Point3D(x, y), GDIPathCommand.MoveTo, closeFigure);
        }

        public virtual void LineTo(double x, double y, bool closeFigure)
        {
            AddVertex(new Point3D(x, y), GDIPathCommand.LineTo, closeFigure);
        }

        public virtual void BezierTo(int x, int y, bool closeFigure)
        {
            AddVertex(new Point3D(x, y), GDIPathCommand.BezierTo, closeFigure);
        }
        #endregion

        #region IBracket
        /// <summary>
        /// Begin a Path.  If the path is already frozen, then this will have
        /// no effect.
        /// </summary>
        public void Begin()
        {
            fVertices = null;
            fCommands = null;

            OnBegin();
        }

        protected virtual void OnBegin()
        {
            // Do nothing specific
        }

        /// <summary>
        /// End the path.
        /// </summary>
        public void End()
        {
            OnEnd();
        }

        protected virtual void OnEnd()
        {
            fVertices = fVertexList.ToArray();
            fCommands = fCommandList.ToArray();
        }
        #endregion

        #region IUniqueGDIObject
        public virtual IntPtr Handle
        {
            get { return IntPtr.Zero; }

            // This empty set must exist or subclasses won't
            // be able to implement it.
            set { }
        }

        public virtual Guid UniqueID
        {
            get { return fUniqueID; }
        }
        #endregion
    }
}
