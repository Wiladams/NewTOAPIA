using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.GDI32;

namespace NewTOAPIA.Drawing.GDI
{
    public class GDIGlyph
    {
        public static ushort GetOutline(UnmanagedPointer ipHeader, int size, out float2[] pnts, out sbyte[] onCurve)
        {
            ushort cTotal = 0;
            UnmanagedPointer endPtr = ipHeader + size;
            UnmanagedPointer ipCurve;

            int MaxPts = size / Marshal.SizeOf(typeof(POINTFX));

            TTPOLYGONHEADER Header;
            TTPOLYCURVE Curve;
            POINTFX pntfx = new POINTFX();

            pnts = new float2[MaxPts];
            onCurve = new sbyte[MaxPts];

            while (ipHeader < endPtr && pnts != null)
            {
                Header = (TTPOLYGONHEADER)Marshal.PtrToStructure(ipHeader, typeof(TTPOLYGONHEADER));

                if (Header.dwType == GDI32.TT_POLYGON_TYPE)
                {
                    pnts[cTotal].x = Header.pfxStart.x.ToSingle();
                    pnts[cTotal].y = Header.pfxStart.y.ToSingle();
                    onCurve[cTotal++] = -1; // new contour

                    ipCurve = ipHeader + Marshal.SizeOf(typeof(TTPOLYGONHEADER));

                    while (ipCurve < ipHeader + Header.cb)
                    {
                        Curve = (TTPOLYCURVE)Marshal.PtrToStructure(ipCurve, typeof(TTPOLYCURVE));
                        ipCurve = ipCurve + Marshal.SizeOf(typeof(TTPOLYCURVE));

                        for (int i = 0; i < Curve.cpfx; i++)
                        {
                            pntfx = (POINTFX)Marshal.PtrToStructure(ipCurve, typeof(POINTFX));

                            pnts[cTotal].x = pntfx.x.ToSingle();
                            pnts[cTotal].y = pntfx.y.ToSingle();

                            onCurve[cTotal++] = (sbyte)((i == Curve.cpfx - 1) ? 1 : (Curve.wType == GDI32.TT_PRIM_LINE) ? 1 : 0);

                            ipCurve = ipCurve + Marshal.SizeOf(typeof(POINTFX));
                        }
                    }
                    // check to see if first contour point is repeated and remove it
                    if (Header.pfxStart == pntfx)
                        cTotal--;
                }

                ipHeader = ipHeader + Header.cb;
            }

            return cTotal;
        }


        public static uint SelectFont(GDIContext hdc, System.Drawing.Font font)
        {
            GDI32.SelectObject(hdc, font.ToHfont());

            uint size = GDI32.GetOutlineTextMetrics(hdc, 0, IntPtr.Zero);

            if (size != uint.MaxValue)
            {
                UnmanagedMemory otm = new UnmanagedMemory((int)size);
                uint err = GDI32.GetOutlineTextMetrics(hdc, size, otm.MemoryPointer);
                uint otmEMSquare = (uint)Marshal.ReadInt32(otm, 92);
                otm.Dispose();

                LOGFONT log = new LOGFONT();
                font.ToLogFont(log);
                log.lfHeight = -(int)otmEMSquare;

                font = System.Drawing.Font.FromLogFont(log);
                GDI32.SelectObject(hdc, font.ToHfont());
            }

            return size;
        }

        public static bool IsTrueType(GDIContext hdc, System.Drawing.Font font)
        {
            GDI32.SelectObject(hdc, font.ToHfont());

            UnmanagedMemory tmPtr = new UnmanagedMemory(Marshal.SizeOf(typeof(TEXTMETRIC)));
            uint err = GDI32.GetTextMetrics(hdc, tmPtr.MemoryPointer);
            TEXTMETRIC tm = (TEXTMETRIC)Marshal.PtrToStructure(tmPtr.MemoryPointer, typeof(TEXTMETRIC));
            tmPtr.Dispose();

            return (tm.tmPitchAndFamily & 4) != 0;
        }


        public static ushort GetGlyph(GDIContext hdc, uint uchar, out float2[] pnts, out sbyte[] onCurve, out short advanceWidth)
        {
            MAT2 mat = new MAT2();

            IntPtr matPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(MAT2)));
            Marshal.StructureToPtr(mat, matPtr, true);

            IntPtr gmPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(GLYPHMETRICS)));

            int sz = GDI32.GetGlyphOutline(hdc, uchar, GDI32.GGO_NATIVE, gmPtr, 0, IntPtr.Zero, matPtr);

            GLYPHMETRICS gm = (GLYPHMETRICS)Marshal.PtrToStructure(gmPtr, typeof(GLYPHMETRICS));

            advanceWidth = gm.gmCellIncX;

            IntPtr gbufPtr = Marshal.AllocCoTaskMem(sz);
            int err0 = GDI32.GetGlyphOutline(hdc, uchar, GDI32.GGO_NATIVE, gmPtr, sz, gbufPtr, matPtr);

            return GetOutline(new UnmanagedPointer(gbufPtr), sz, out pnts, out onCurve);
        }

    }
}
