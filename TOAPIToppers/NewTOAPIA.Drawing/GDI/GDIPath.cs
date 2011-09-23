using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.GDI32;

namespace NewTOAPIA.Drawing
{
    public class GDIPath : GPath
    {
        #region Fields
        GDIContext fDeviceContext;
        #endregion

        #region Constructor
        public GDIPath(GDIContext dc, Guid uniqueID)
           : base(uniqueID)
        {
            fDeviceContext = dc;
        }
        #endregion

        #region Properties
        public GDIContext DeviceContext
        {
            get { return fDeviceContext; }
        }

        #endregion

        #region Methods
        public void FlattenPath()
        {
            if (!IsFrozen && (null != fDeviceContext))
                fDeviceContext.FlattenPath();
        }

        #region IBracket
        protected override void OnBegin()
        {
            fDeviceContext.BeginPath();
        }

        protected override void OnEnd()
        {
            // 1. Indicate path drawing is finished
            fDeviceContext.EndPath();
            fDeviceContext.Flush();

            // 2. Get the path data
            int numPoints = GDI32.GetPath(fDeviceContext, null, null, 0);

            if (numPoints < 1)
                return;

            // 3. Allocate memory based on number of points indicated
            UnmanagedMemory vertexMemory = new UnmanagedMemory((int)numPoints * Marshal.SizeOf(typeof(TOAPI.Types.POINT)));
            UnmanagedMemory commandMemory = new UnmanagedMemory((int)numPoints);

            // 4. Call again to actually get the data filled in
            int verticesFilled = GDI32.GetPath(fDeviceContext, vertexMemory.MemoryPointer, commandMemory.MemoryPointer, numPoints);

            // 5. Convert data to correct values

            // The bytes for the commands are a straight copy
            byte[] commands = new byte[numPoints];
            Marshal.Copy(commandMemory.MemoryPointer, commands, 0, numPoints);

            // The points are done one structure at a time
            UnmanagedPointer vertexPtr = vertexMemory.MemoryPointer;
            List<Point> pts = new List<Point>();
            int structIncrement = Marshal.SizeOf(typeof(TOAPI.Types.POINT));

            for (int i = 0; i < numPoints; i++)
            {
                TOAPI.Types.POINT pt = (TOAPI.Types.POINT)Marshal.PtrToStructure(vertexPtr, typeof(TOAPI.Types.POINT));
                pts.Add(new Point(pt.X, pt.Y));
                vertexPtr = vertexPtr + structIncrement;
            }

            // convert the list of points into an array
            Point[] vertices = pts.ToArray();

            // 6. Assign to the path variables
            SetPath(vertices, commands);

            // 7. Free the allocated memory
            vertexMemory.Dispose();
            commandMemory.Dispose();
        }
        #endregion
        #endregion


    }
}
