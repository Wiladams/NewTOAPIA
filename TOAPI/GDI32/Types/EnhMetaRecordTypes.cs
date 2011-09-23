using System;
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.GDI32
{
    // Base structure used by all other structures as their first field
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMR
    {
        int iType;
        int nSize;

        public EMR()
            :this(GDI32.EMR_HEADER)
        {
        }

        public EMR(int aType)
        {
            iType = aType;
            nSize = Marshal.SizeOf(this);
        }

        public int Command
        {
            get { return iType; }
        }

        public int Size
        {
            get { return nSize; }
        }
    }


    //EMR_ABORTPATH 
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRABORTPATH : EMR
    {
        public EMRABORTPATH()
            :base(GDI32.EMR_ABORTPATH)
        {}
    }
    
    //EMR_ANGLEARC
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRANGLEARC : EMR
    {
        public POINT ptlCenter;
        public uint nRadius;
        public float eStartAngle;
        public float eSweepAngle;

        public EMRANGLEARC()
            :base(GDI32.EMR_ANGLEARC)
        {}
    }

    //EMR_ARC
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRARC : EMR
    {
        public RECT rclBox;
        public POINT ptlStart;
        public POINT ptlEnd;

        public EMRARC()
            : base(GDI32.EMR_ARC)
        { }
    }

    //EMR_ARCTO
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRARCTO : EMR
    {
        public RECT rclBox;
        public POINT ptlStart;
        public POINT ptlEnd;

        public EMRARCTO()
            : base(GDI32.EMR_ARCTO)
        { }

    }

    //EMR_BEGINPATH
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRBEGINPATH : EMR
    {
        public EMRBEGINPATH()
            : base(GDI32.EMR_BEGINPATH)
        { }
    }

    //EMR_BITBLT
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRBITBLT : EMR
    {
        public RECT rclBounds;
        public int xDest;
        public int yDest;
        public int cxDest;
        public int cyDest;
        public uint dwRop;
        public int xSrc;
        public int ySrc;
        public XFORM xformSrc;
        public uint crBkColorSrc;
        public uint iUsageSrc;
        public uint offBmiSrc;
        public uint cbBmiSrc;
        public uint offBitsSrc;
        public uint cbBitsSrc;

        public EMRBITBLT()
            : base(GDI32.EMR_BITBLT)
        { }
    }

    //EMR_STRETCHBLT
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRSTRETCHBLT : EMR
    {
        public RECT rclBounds;
        public int xDest;
        public int yDest;
        public int cxDest;
        public int cyDest;
        public uint dwRop;
        public int xSrc;
        public int ySrc;
        public XFORM xformSrc;
        public uint crBkColorSrc;
        public uint iUsageSrc;
        public uint offBmiSrc;
        public uint cbBmiSrc;
        public uint offBitsSrc;
        public uint cbBitsSrc;
        public int cxSrc;
        public int cySrc;


        public EMRSTRETCHBLT()
            : base(GDI32.EMR_STRETCHBLT)
        { }
    }

    //EMR_STRETCHDIBITS
    //EMR_ALPHABLEND 
    //EMR_TRANSPARENTBLT
    //EMR_SETDIBITSTODEVICE
    //EMR_PLGBLT

    //EMR_CHORD
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRCHORD : EMR
    {
        public RECT rclBox;
        public POINT ptlStart;
        public POINT ptlEnd;

        public EMRCHORD()
            : base(GDI32.EMR_CHORD)
        { }
    }

    //EMR_CLOSEFIGURE
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRCLOSEFIGURE : EMR
    {
        public EMRCLOSEFIGURE()
            : base(GDI32.EMR_CLOSEFIGURE)
        { }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct LOGBRUSH32
    {
        public int lbStyle;
        public uint lbColor;
        public int lbHatch;
    }

    //EMR_CREATEBRUSHINDIRECT
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRCREATEBRUSHINDIRECT : EMRCREATEUNIQUEOBJECT
    {
        public uint ihBrush;
        public LOGBRUSH32 lb;
    }

    //EMR_CREATEDIBPATTERNBRUSHPT

    //EMR_CREATEMONOBRUSH

    //EMR_CREATEPALETTE

    //EMR_CREATEUNIQUEOBJECT
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRCREATEUNIQUEOBJECT : EMR
    {
        public Guid uniqueID;

        public EMRCREATEUNIQUEOBJECT()
            : base(GDI32.EMR_CREATEUNIQUEOBJECT)
        {}

        public EMRCREATEUNIQUEOBJECT(int subType)
            : base(subType)
        {}
    }

    //EMR_CREATEPEN
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRCREATEPEN : EMRCREATEUNIQUEOBJECT
    {
        public int ihPen;
        public LOGPEN lopn;

        public EMRCREATEPEN()
            : base(GDI32.EMR_CREATEPEN)
        { }
    }


    //EMR_ELLIPSE
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRELLIPSE : EMR
    {        
        public RECT rclBox;

        protected EMRELLIPSE(int subType)
            : base(subType)
        { }

        public EMRELLIPSE()
            : base(GDI32.EMR_ELLIPSE)
        { }

    }

    //EMR_RECT
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRRECT : EMRELLIPSE
    {
        public EMRRECT()
            : base(GDI32.EMR_RECTANGLE)
        { }
    }

    //EMR_ENDPATH
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRENDPATH : EMR
    {
        public EMRENDPATH()
            : base(GDI32.EMR_ENDPATH)
        { }
    }

    //EMR_EOF
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMREOF : EMR
    {
        public uint nPalEntries;
        public uint offPalEntries;
        int nSizeLast;

        public EMREOF()
            : base(GDI32.EMR_EOF)
        {
            nSizeLast = Size;
        }
    }

//EMR_EXCLUDECLIPRECT

//EMR_EXTCREATEFONTINDIRECTW

//EMR_EXTCREATEPEN

//EMR_EXTFLOODFILL

//EMR_EXTSELECTCLIPRGN

    [Serializable]
    [StructLayout(LayoutKind.Sequential,CharSet=CharSet.Auto)]
    public struct EMRTEXT
    {
        public POINT ptlReference;

        public uint nChars;
        public uint offString;
        public uint fOptions;
        public RECT rcl;
        public uint offDx;
        public string text;
    }

    //EMR_EXTTEXTOUTA
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMREXTTEXTOUTA : EMR
    {
        public RECT rclBounds;
        public uint iGraphicsMode;
        public float exScale;
        public float eyScale;
        public EMRTEXT emrtext;

        public EMREXTTEXTOUTA(int subType)
            :base(subType)
        {}

        public EMREXTTEXTOUTA()
            : base(GDI32.EMR_EXTTEXTOUTA)
        {}
    }

//EMR_EXTTEXTOUTW

    //EMR_FILLPATH
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRFILLPATH : EMR
    {
        public RECT rclBounds;

        protected EMRFILLPATH(int subType)
            : base(subType)
        { }

        public EMRFILLPATH()
            : base(GDI32.EMR_FILLPATH)
        { }
    }

    //EMR_STROKEPATH
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRSTROKEPATH : EMRFILLPATH
    {
        public EMRSTROKEPATH()
            : base(GDI32.EMR_STROKEPATH)
        {}
    }

//EMR_FILLRGN

//EMR_FLATTENPATH
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRFLATTENPATH : EMR
    {
        public EMRFLATTENPATH()
            : base(GDI32.EMR_FLATTENPATH)
        { }
    }

//EMR_FRAMERGN

//EMR_GDICOMMENT

//EMR_INTERSECTCLIPRECT

//EMR_INVERTRGN

    //EMR_MOVETOEX
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRMOVETO : EMR
    {
        public POINT ptl;

        protected EMRMOVETO(int subType)
            : base(subType)
        {
        }

        public EMRMOVETO()
            : base(GDI32.EMR_MOVETOEX)
        { }
    }

    //EMR_LINETO
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRLINETO : EMRMOVETO
    {
        public EMRLINETO()
            : base(GDI32.EMR_LINETO)
        { }

    }

//EMR_MASKBLT

//EMR_MODIFYWORLDTRANSFORM


//EMR_OFFSETCLIPRGN

//EMR_PAINTRGN

//EMR_PIE



    //EMR_POLYLINE
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRPOLYLINE : EMR
    {
        public RECT rclBounds;
        public int cptl;

        /// POINT[1]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=1, ArraySubType=UnmanagedType.Struct)]
        public POINT[] aptl;

        protected EMRPOLYLINE(int subType)
            : base (subType)
        {
        }

        public EMRPOLYLINE()
            : base(GDI32.EMR_POLYLINE)
        { }
    }

    //EMR_POLYBEZIER
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRPOLYBEZIER : EMRPOLYLINE
    {
        public EMRPOLYBEZIER()
            : base(GDI32.EMR_POLYBEZIER)
        {
        }
    }


    //EMR_POLYBEZIERTO


    //EMR_POLYDRAW
    [Serializable]
    [StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRPOLYDRAW : EMR
    {
        public RECT rclBounds;
        public int cptl;

        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public POINT[] aptl;

        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] abTypes;

        public EMRPOLYDRAW()
            : base(GDI32.EMR_POLYDRAW) 
        { }
    }



    //EMR_POLYGON
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRPOLYGON : EMRPOLYLINE
    {
        public EMRPOLYGON()
            : base(GDI32.EMR_POLYGON)
        {
        }
    }

//EMR_POLYLINE16 
//EMR_POLYLINETO

//EMR_POLYLINETO16

//EMR_POLYPOLYGON

//EMR_POLYPOLYGON16

//EMR_POLYPOLYLINE

//EMR_POLYPOLYLINE16

//EMR_POLYTEXTOUTA

//EMR_POLYTEXTOUTW

    //EMR_REALIZEPALETTE
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class REALIZEPALETTE : EMR
    {
        public EMR emr;

        public REALIZEPALETTE()
            : base(GDI32.EMR_REALIZEPALETTE)
        { }
    }


    //EMR_RESIZEPALETTE

    //EMR_RESTOREDC
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRRESTOREDC : EMR
    {
        public int iRelative;

        public EMRRESTOREDC()
            : base(GDI32.EMR_RESTOREDC)
        { }
    }

    //EMR_ROUNDRECT
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRROUNDRECT : EMR
    {
        public RECT rclBox;
        public SIZE szlCorner;

        public EMRROUNDRECT()
            : base(GDI32.EMR_ROUNDRECT)
        { }
    }

    //EMR_SAVEDC
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRSAVEDC : EMR
    {
        public EMRSAVEDC()
            : base(GDI32.EMR_SAVEDC)
        { }
    }



    //EMR_SELECTCLIPPATH
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRSELECTCLIPPATH : EMR
    {
        public uint iMode;

        protected EMRSELECTCLIPPATH(int subType)
            : base(subType)
        { }

        public EMRSELECTCLIPPATH()
            : base(GDI32.EMR_SELECTCLIPPATH)
        { }
    }

    //EMR_SELECTOBJECT
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRSELECTOBJECT : EMR
    {
        public int ihObject;

        public EMRSELECTOBJECT()
            : base(GDI32.EMR_SELECTOBJECT)
        { }

        // This one gets used by sub-classes
        protected EMRSELECTOBJECT(int aSubType)
            : base(aSubType)
        {
        }
    }

    //EMR_DELETEOBJECT
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRDELETEOBJECT : EMRSELECTOBJECT
    {
        public EMRDELETEOBJECT()
            : base(GDI32.EMR_DELETEOBJECT)
        {}
    }


    //EMR_SELECTSTOCKOBJECT
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRSELECTSTOCKOBJECT : EMRSELECTOBJECT
    {
        public EMRSELECTSTOCKOBJECT()
            : base(GDI32.EMR_SELECTSTOCKOBJECT)
        { }
    }

    //EMR_SELECTUNIQUEOBJECT
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRSELECTUNIQUEOBJECT : EMR
    {
        public Guid uniqueID;

        public EMRSELECTUNIQUEOBJECT()
            : base(GDI32.EMR_SELECTUNIQUEOBJECT)
        { }
    }

    //EMR_SELECTPALETTE

    //EMR_SETARCDIRECTION

    //EMR_SETBKCOLOR
    [StructLayout(LayoutKind.Sequential)]
    public class EMRSETBKCOLOR : EMRSETTEXTCOLOR
    {
        public EMRSETBKCOLOR()
            : base(GDI32.EMR_SETBKCOLOR)
        {
        }
    }

    //EMR_SETBKMODE
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRSETBKMODE : EMRSELECTCLIPPATH
    {
        public EMRSETBKMODE()
            : base(GDI32.EMR_SETBKMODE)
        { }
    }

    //EMR_SETBRUSHORGEX

    //EMR_SETCOLORADJUSTMENT


    //EMR_SETMAPMODE
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRSETMAPMODE : EMRSELECTCLIPPATH
    {
        public EMRSETMAPMODE()
            : base(GDI32.EMR_SETMAPMODE)
        { }
    }

    //EMR_SETMAPPERFLAGS

    //EMR_SETMETARGN
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRSETMETARGN : EMR
    {
        public EMRSETMETARGN()
            : base(GDI32.EMR_SETMETARGN)
        {}
    }


    //EMR_SETMITERLIMIT

    //EMR_SETPALETTEENTRIES

    //EMR_SETPIXELV
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRSETPIXELV : EMR
    {
        public POINT ptlPixel;
        public uint crColor;

        public EMRSETPIXELV()
            : base(GDI32.EMR_SETPIXELV)
        {
        }
    }

    //EMR_SETPOLYFILLMODE
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRSETPOLYFILLMODE : EMRSELECTCLIPPATH
    {
        public EMRSETPOLYFILLMODE()
            : base(GDI32.EMR_SETPOLYFILLMODE)
        { }
    }

    //EMR_SETROP2
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRSETROP2 : EMRSELECTCLIPPATH
    {
        public EMRSETROP2()
            : base(GDI32.EMR_SETROP2)
        { }
    }

    //EMR_SETSTRETCHBLTMODE
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRSETSTRETCHBLTMODE : EMRSELECTCLIPPATH
    {
        public EMRSETSTRETCHBLTMODE()
            : base(GDI32.EMR_SETSTRETCHBLTMODE)
        { }
    }

    //EMR_SETTEXTALIGN

    //EMR_SETTEXTCOLOR
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRSETTEXTCOLOR : EMR
    {
        public uint crColor;

        public EMRSETTEXTCOLOR(int subType)
            : base(subType)
        { }

        public EMRSETTEXTCOLOR()
            : base(GDI32.EMR_SETTEXTCOLOR)
        {}
    }

    // EMR_SETDCPENCOLOR
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRSETDCPENCOLOR : EMRSETTEXTCOLOR
    {
        public EMRSETDCPENCOLOR()
            : base(GDI32.EMR_SETDCPENCOLOR)
        { }
    }

    // EMR_SETDCBRUSHCOLOR
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRSETDCBRUSHCOLOR : EMRSETTEXTCOLOR
    {
        public EMRSETDCBRUSHCOLOR()
            : base(GDI32.EMR_SETDCBRUSHCOLOR)
        { }
    }




    //EMR_SETVIEWPORTEXTEX
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRSETVIEWPORTEXTEX : EMR
    {
        public SIZE szlExtent;

        protected EMRSETVIEWPORTEXTEX(int subType)
            : base(subType)
        { }

        public EMRSETVIEWPORTEXTEX()
            : base(GDI32.EMR_SETVIEWPORTEXTEX)
        {}
    }

    //EMR_SETVIEWPORTORGEX
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRSETVIEWPORTORGEX : EMR
    {
        public POINT ptlOrigin;


        public EMRSETVIEWPORTORGEX()
            : base(GDI32.EMR_SETVIEWPORTORGEX)
        { }
    
        protected EMRSETVIEWPORTORGEX(int subType)
            : base(subType)
        { }
    }

    //EMR_SCALEVIEWPORTEXTEX
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRSCALEVIEWPORTEXTEX : EMR
    {
        public int xNum;
        public int xDenom;
        public int yNum;
        public int yDenom;
 
        protected EMRSCALEVIEWPORTEXTEX(int subType)
            : base(subType)
        { }

        public EMRSCALEVIEWPORTEXTEX()
                : base(GDI32.EMR_SCALEVIEWPORTEXTEX)
        { }
    }

    //EMR_SETWINDOWEXTEX
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRSETWINDOWEXTEX : EMRSETVIEWPORTEXTEX
    {
        public EMRSETWINDOWEXTEX()
            : base(GDI32.EMR_SETWINDOWEXTEX)
        { }
    }

    //EMR_SETWINDOWORGEX
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRSETWINDOWORGEX : EMRSETVIEWPORTORGEX
    {
        public EMRSETWINDOWORGEX()
            : base(GDI32.EMR_SETWINDOWORGEX)
        { }
    }

    //EMR_SCALEWINDOWEXTEX

    //EMR_SETWORLDTRANSFORM
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRSETWORLDTRANSFORM : EMR
    {
        public XFORM xform;

        public EMRSETWORLDTRANSFORM()
            : base(GDI32.EMR_SETWORLDTRANSFORM)
        { }
    }



    //EMR_STROKEANDFILLPATH

    //EMR_WIDENPATH
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class EMRWIDENPATH : EMR
    {
        public EMRWIDENPATH()
            : base(GDI32.EMR_WIDENPATH)
        { }
    }

    //EMR_CREATECOLORSPACE 

    //EMR_DELETECOLORSPACE

    //EMR_GLSBOUNDEDRECORD

    //EMR_GLSRECORD

    //EMR_PIXELFORMAT 
    //[Serializable]
    //[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    //public class EMRPIXELFORMAT : EMR
    //{
    //    public PIXELFORMATDESCRIPTOR pfd;

    //    public EMRPIXELFORMAT()
    //        : base(GDI32.EMR_PIXELFORMAT)
    //    { }
    //}

    //EMR_SETCOLORSPACE

    //EMR_SETICMMODE

    //EMR_COLORCORRECTPALETTE

    //EMR_COLORMATCHTOTARGETW

    //EMR_CREATECOLORSPACEW

    //EMR_GRADIENTFILL
    [Serializable]
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class EMRGRADIENTFILL : EMR
    {
        public RECT rclBounds;
        public uint nVer;
        public uint nTri;
        public uint ulMode;
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.Struct)]
        public TRIVERTEX[] Ver;

        public EMRGRADIENTFILL()
            : base(GDI32.EMR_GRADIENTFILL)
        {}
    }

    //EMR_SETICMPROFILEA 

    //EMR_SETICMPROFILEW

    //EMR_SETLAYOUT

    // There is no really good reason to implement these
    //EMR_POLYBEZIER16
    //EMR_POLYBEZIERTO16
    //EMR_POLYDRAW16
    //EMR_POLYGON16

}
