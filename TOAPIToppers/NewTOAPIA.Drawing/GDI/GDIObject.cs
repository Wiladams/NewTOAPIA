using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using TOAPI.GDI32;

namespace NewTOAPIA.Drawing
{
    public enum GDIObjectType
    {
        DC = 0x01,
        Region = 0x04,
        Bitmap = 0x05,
        Palette = 0x08,
        Font = 0x0a,
        Brush = 0x10,
        EnhMetafile = 0x21,
        Pen = 0x30,
        ExtendedPen = 0x50
    }

    public class GDIObject : SafeHandle, IUniqueObject
    {
        Guid fUniqueID;

        public GDIObject(bool ownsHandle, Guid uniqueID)
            : this(IntPtr.Zero, ownsHandle, uniqueID)
        {
        }

        public GDIObject(IntPtr aHandle, bool ownsHandle, Guid uniqueID)
            : base(IntPtr.Zero, ownsHandle)
        {
            fUniqueID = uniqueID;
            SetHandle(aHandle);
        }

        #region SafeHandle
        public override bool IsInvalid
        {
            get
            {
                return IsClosed || (IntPtr.Zero == handle);
            }
        }

        protected override bool ReleaseHandle()
        {
            bool retValue = GDI32.DeleteObject(base.handle) != 0;
            return retValue;
        }
        #endregion

        public Guid UniqueID
        {
            get { return fUniqueID; }
        }

        public bool IsStockObject
        {
            get
            {
                bool retValue = ((base.handle.ToInt32() & 0x00800000) != 0);

                return retValue;
            }
        }

        /// <summary>
        /// Return the index in the GDI object table
        /// </summary>
        public int HandleTableIndex
        {
            get
            {
                int retValue = (base.handle.ToInt32() & 0x3fff);

                return retValue;
            }
        }

        public GDIObjectType TypeOfGDIObject
        {
            get {
                GDIObjectType retValue = (GDIObjectType)(((base.handle.ToInt32() >> 16) & 0x7f));

                return retValue;
            }
        }
    }
}
