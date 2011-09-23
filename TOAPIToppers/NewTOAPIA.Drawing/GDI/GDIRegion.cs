using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.GDI32;

namespace NewTOAPIA.Drawing
{
    public class GDIRegion : GDIObject
    {

        #region Constructor
        public GDIRegion(IntPtr regionHandle, bool ownsHandle)
            : this(regionHandle, ownsHandle, Guid.NewGuid())
        {
        }

        public GDIRegion(IntPtr regionHandle, bool ownsHandle, Guid uniqueID)
            : base(regionHandle, ownsHandle, uniqueID)
        {
        }
        #endregion


        #region Properties
        public bool IsEmpty
        {
            get
            {
                Rectangle frame = GetFrame();
                return frame.IsEmpty;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Clear the region to a null region.
        /// </summary>
        public void Clear()
        {
            GDI32.SetRectRgn(DangerousGetHandle(), 0, 0, 0, 0);
        }

        #region Some Region Arithmetic
        public GDIRegion Add(GDIRegion aRegion)
        {
            RegionCombineType retValue = (RegionCombineType)GDI32.CombineRgn(DangerousGetHandle(), DangerousGetHandle(), aRegion.DangerousGetHandle(), (int)RegionCombineStyles.OR);

            return this;
        }

        public GDIRegion Subtract(GDIRegion aRegion)
        {
            RegionCombineType retValue = (RegionCombineType)GDI32.CombineRgn(DangerousGetHandle(), DangerousGetHandle(), aRegion.DangerousGetHandle(), (int)RegionCombineStyles.Diff);

            return this;
        }

        public GDIRegion Intersect(GDIRegion aRegion)
        {
            RegionCombineType retValue = (RegionCombineType)GDI32.CombineRgn(DangerousGetHandle(), DangerousGetHandle(), aRegion.DangerousGetHandle(), (int)RegionCombineStyles.AND);

            return this;
        }

        public RegionCombineType Combine(GDIRegion aRegion, GDIRegion bRegion, RegionCombineStyles combineStyle)
        {
            int retValue = GDI32.CombineRgn(DangerousGetHandle(), aRegion.DangerousGetHandle(), bRegion.DangerousGetHandle(), (int)combineStyle);
            
            RegionCombineType result = (RegionCombineType)retValue;
            return result;
        }
        #endregion

        public bool Contains(int x, int y)
        {
            bool contains = GDI32.PtInRegion(DangerousGetHandle(), x, y);
            return contains;
        }

        public bool Equals(GDIRegion aRegion)
        {
            bool isEqual = GDI32.EqualRgn(DangerousGetHandle(), aRegion.DangerousGetHandle());

            return isEqual;
        }

        public Rectangle GetFrame()
        {
            RECT rect = new RECT();
            int result = GDI32.GetRgnBox(DangerousGetHandle(), out rect);

            return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public Rectangle[] GetRectangles()
        {

            // If the region is empty, return a single empty rectangle
            if (IsEmpty)
            {
                return new Rectangle[1];
            }

            // 1. Call GetRgnData with null to get size
            uint dataLengthNeeded = GDI32.GetRegionData(DangerousGetHandle(), 0, IntPtr.Zero);

            // 2. allocate some memory to hold the data
            UnmanagedMemory uMem = new UnmanagedMemory((int)dataLengthNeeded);
            IntPtr memoryPtr = uMem.MemoryPointer;
            UnmanagedPointer structPtr = new UnmanagedPointer(memoryPtr);

            // 3. call again passing in memory
            uint bytesFilled = GDI32.GetRegionData(DangerousGetHandle(), dataLengthNeeded, structPtr);
            
            // If the return value is 0, then the call failed.
            if (0 == bytesFilled)
                return null;

            // 4. convert memory to data structures
            // First get the RGNDATAHEADER
            RGNDATAHEADER dataHeader;
            dataHeader = (RGNDATAHEADER)Marshal.PtrToStructure(structPtr, typeof(RGNDATAHEADER));

            // dataHeader.dwSize    - tells us how big the header is, and thus how many bytes to skip to get
            //                          to the rectangle information
            // dataHeader.nRgnSize  - tells us how many bytes are needed for the rectangle information
            // dataHeader.nCount    - tells us how many rectangles there are


            structPtr = structPtr + dataHeader.dwSize;

            // The nRgnSize / nCount will tell us how many bytes per rectangle structure
            // and therefore how much to advance the pointer as we turn the buffer
            // into a set of rectangles using PtrToStructure.
            int structIncrement = (int)(dataHeader.nRgnSize / dataHeader.nCount);

            // Create a list to add the rectangles to.  This is simply convenient.
            Rectangle[] rects = new Rectangle[dataHeader.nCount];

            // Loop through creating as many rectangles as indicated by the dataheader nCount field
            for (int i = 0; i < dataHeader.nCount; i++)
            {
                RECT rect = (RECT)Marshal.PtrToStructure(structPtr, typeof(RECT));
                rects[i].X = rect.X;
                rects[i].Y = rect.Y;
                rects[i].Width = rect.Width;
                rects[i].Height = rect.Height;

                structPtr = structPtr + structIncrement;
            }


            // 5. free memory
            uMem.Dispose();

            return rects;
        }

        public void MoveBy(int dx, int dy)
        {
            GDI32.OffsetRgn(DangerousGetHandle(), dx, dy);
        }
        #endregion

        #region Static Helpers
        public static GDIRegion CreateFromPath(GPath aPath, Guid uniqueID)
        {
            // 1. Create a simple device context
            GDIContext deviceContext = GDIContext.CreateForDefaultDisplay();

            // 2. Replay the path into the context
            deviceContext.ReplayPath(aPath);

            // 3. Create a region from the replayed path
            IntPtr regionHandle = GDI32.PathToRegion(deviceContext);

            GDIRegion newRegion = new GDIRegion(regionHandle, true, uniqueID);

            return newRegion;
        }

        public static GDIRegion CreateFromPolygon(Point[] points, PolygonFillMode aMode, Guid uniqueID)
        {
            TOAPI.Types.POINT[] pts = new POINT[points.Length];
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i].X = points[i].X;
                pts[i].Y = points[i].Y;
            }

            IntPtr regionHandle = GDI32.CreatePolygonRgn(pts, pts.Length, (int)aMode);

            GDIRegion newRegion = new GDIRegion(regionHandle, true, uniqueID);

            return newRegion;
        }

        public static GDIRegion CreateFromRectangle(int left, int top, int right, int bottom, Guid uniqueID)
        {
            RECT newRect = RECT.FromLTRB(left, top, right, bottom);
            IntPtr regionHandle = GDI32.CreateRectRgnIndirect(ref newRect);

            GDIRegion newRegion = new GDIRegion(regionHandle, true, uniqueID);

            return newRegion;
        }

        public static GDIRegion CreateFromRectangles(Rectangle[] rects, Guid uniqueID)
        {
            // 1.  Create a header
            RGNDATAHEADER header = new RGNDATAHEADER();
            header.dwSize = Marshal.SizeOf(header);
            header.iType = 1;
            header.nCount = (uint)rects.Length;
            header.nRgnSize = (uint)Marshal.SizeOf(typeof(RECT)) * header.nCount;
            //header.rcBound = 

            // 2. Allocate memory to hold the header plus the data for the rectangles
            int dataLengthNeeded = (int)(header.dwSize + header.nRgnSize);
            IntPtr memoryPtr = Marshal.AllocCoTaskMem((int)dataLengthNeeded);
            UnmanagedPointer structPtr = new UnmanagedPointer(memoryPtr);

            // 4. Copy the Header into the memory buffer
            Marshal.StructureToPtr(header, structPtr, false);

            // 5. Increment the memory buffer pointer to write the rectangles
            structPtr = structPtr + header.dwSize;

            // The nRgnSize / nCount will tell us how many bytes per rectangle structure
            // and therefore how much to advance the pointer as we turn the buffer
            // into a set of rectangles using PtrToStructure.
            int structIncrement = (int)(header.nRgnSize / header.nCount);

            // 6. Write the rectangles
            for (int i = 0; i < header.nCount; i++)
            {
                RECT aRect = new RECT(rects[i].X, rects[i].Y, rects[i].Width, rects[i].Height);
                Marshal.StructureToPtr(aRect, structPtr, false);

                // Increment the structure pointer to the next position
                structPtr = structPtr + structIncrement;
            }

            // 7. Create the region
            IntPtr regionHandle = GDI32.ExtCreateRegion(IntPtr.Zero, (uint)rects.Length, memoryPtr);
            GDIRegion newRegion = new GDIRegion(regionHandle, true, uniqueID);

            // 8. Free the memory
            Marshal.FreeCoTaskMem(memoryPtr);
        
            return newRegion;
        }
        #endregion

    }
}
