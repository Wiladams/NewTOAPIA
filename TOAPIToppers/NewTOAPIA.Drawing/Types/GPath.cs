using System;
using System.Collections.Generic;
using System.Drawing;

//using TOAPI.Types;
using TOAPI.GDI32;

namespace NewTOAPIA.Drawing
{
    public class GPath : IBracket, IUniqueGDIObject
    {
        #region Fields
        Guid fUniqueID;

        Point[] fVertices;
        List<Point> fVertexList;
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

            fVertexList = new List<Point>();
            fCommandList = new List<byte>();
        }

        public GPath(Point[] vertices, byte[] commands)
        {
            SetPath(vertices, commands);
        }
        #endregion

        #region Properties
        public bool IsFrozen
        {
            get { return fIsFrozen; }
        }

        public Point[] Vertices
        {
            get { return fVertices; }
        }

        public byte[] Commands
        {
            get { return fCommands; }
        }
        #endregion

        #region Methods
        protected void SetPath(Point[] vertices, byte[] commands)
        {
            fVertices = vertices;
            fCommands = commands;

            fIsFrozen = true;
        }

        public virtual void AddVertex(Point aPoint, PathCommand aCommand, bool closeFigure)
        {
            if (closeFigure)
                aCommand |= PathCommand.CloseFigure;

            fVertexList.Add(aPoint);
            fCommandList.Add((byte)aCommand);
        }

        public virtual void MoveTo(int x, int y, bool closeFigure)
        {
            AddVertex(new Point(x, y), PathCommand.MoveTo, closeFigure);
        }

        public virtual void LineTo(int x, int y, bool closeFigure)
        {
            AddVertex(new Point(x, y), PathCommand.LineTo, closeFigure);
        }

        public virtual void BezierTo(int x, int y, bool closeFigure)
        {
            AddVertex(new Point(x, y), PathCommand.BezierTo, closeFigure);
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
