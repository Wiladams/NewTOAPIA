using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TOAPI.GDI32;
using TOAPI.Types;

namespace PixTour
{
    class GDIStreamReceiver
    {
        //public delegate int EnhMetaFileProc(IntPtr hdc, IntPtr lpht, [In()] ref ENHMETARECORD lpmr, int hHandles, IntPtr data);
        
        EnhMetaFileProc  fEnumDelegate;

        GDIStreamReceiver(string filename)
        {
            fEnumDelegate = new EnhMetaFileProc(this.MetaRecordReceiver);
        }

        public int MetaRecordReceiver(IntPtr hdc, IntPtr lpht, ref ENHMETARECORD lpmr, int hHandles, IntPtr data)
        {
        }
    }
}
