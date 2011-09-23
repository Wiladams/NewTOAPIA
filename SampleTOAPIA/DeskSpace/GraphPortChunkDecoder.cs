using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using NewTOAPIA;
using NewTOAPIA.Drawing;

using TOAPI.GDI32;
using TOAPI.Types;

public class GraphPortChunkDecoder : GraphPortDelegate
{
    Guid UnpackGuid(BufferChunk chunk)
    {
        int bufferLength = chunk.NextInt32(); // How many bytes did we pack
        byte[] bytes = (byte[])chunk.NextBufferChunk(bufferLength);
        Guid aGuid = new Guid(bytes);

        return aGuid;
    }

    Point[] UnpackPoints(BufferChunk chunk)
    {
        // Need to know how many points so that space can be allocated for them on the receiving end
        int numPoints = chunk.NextInt32();

        // Allocate a points array of the right size
        Point[] points = new Point[numPoints];

        // Encode each of the points
        for (int i = 0; i < numPoints; i++)
        {
            points[i].x = chunk.NextInt32();
            points[i].y = chunk.NextInt32();
        }

        return points;
    }

    TRIVERTEX[] UnpackTRIVERTEX(BufferChunk chunk)
    {
        int nVertices = chunk.NextInt32();

        TRIVERTEX[] pVertex = new TRIVERTEX[nVertices];

        // Pack the vertices, starting with the length
        for (int i = 0; i < nVertices; i++)
        {
            pVertex[i].x = chunk.NextInt32();
            pVertex[i].y = chunk.NextInt32();
            pVertex[i].Alpha = chunk.NextUInt16();
            pVertex[i].Blue = chunk.NextUInt16();
            pVertex[i].Green = chunk.NextUInt16();
            pVertex[i].Red = chunk.NextUInt16();
        }

        return pVertex;
    }

    GRADIENT_RECT[] UnpackGRADIENT_RECT(BufferChunk chunk)
    {
        int nRects = chunk.NextInt32();

        GRADIENT_RECT[] pMesh = new GRADIENT_RECT[nRects];

        for (int i = 0; i < nRects; i++)
        {
            pMesh[i].UpperLeft = chunk.NextUInt32();
            pMesh[i].LowerRight = chunk.NextUInt32();
        }

        return pMesh;
    }

    public virtual void ReceiveData(BufferChunk aRecord)
    {
        // First read out the record type
        int recordType = aRecord.NextInt32();

        // Then deserialize the rest from there
        switch (recordType)
        {
            case GDI32.EMR_HEADER:
                break;

            // Transform related
            case GDI32.EMR_SCALEVIEWPORTEXTEX:
            case GDI32.EMR_SCALEWINDOWEXTEX:
                break;

            case GDI32.EMR_SETWINDOWEXTEX:
                {
                    int width = aRecord.NextInt32();
                    int height = aRecord.NextInt32();

                    SetWindowExtent(width, height);
                }
                break;

            case GDI32.EMR_SETWINDOWORGEX:
                {
                    int x = aRecord.NextInt32();
                    int y = aRecord.NextInt32();

                    SetWindowOrigin(x, y);
                }
                break;

            case GDI32.EMR_SETVIEWPORTEXTEX:
                {
                    int width = aRecord.NextInt32();
                    int height = aRecord.NextInt32();

                    SetViewportExtent(width, height);
                }
                break;

            case GDI32.EMR_SETVIEWPORTORGEX:
                {
                    int x = aRecord.NextInt32();
                    int y = aRecord.NextInt32();
                    
                    SetViewportOrigin(x, y);
                }
                break;

            case GDI32.EMR_SETWORLDTRANSFORM:
            case GDI32.EMR_MODIFYWORLDTRANSFORM:
                break;

            case GDI32.EMR_SETBRUSHORGEX:
                break;

            case GDI32.EMR_EOF:
                Flush();
                break;

            case GDI32.EMR_SETPIXELV:
                {
                    int x = aRecord.NextInt32();
                    int y = aRecord.NextInt32();
                    UInt32 colorref = aRecord.NextUInt32();

                    SetPixel(x, y, colorref);
                }
                break;

            case GDI32.EMR_SETMAPPERFLAGS:
                break;

            case GDI32.EMR_SETPOLYFILLMODE:
                SetPolyFillMode(aRecord.NextInt32());
                break;

            case GDI32.EMR_SETSTRETCHBLTMODE:
                break;

            case GDI32.EMR_SETMAPMODE:
                MappingModes aMode = (MappingModes)aRecord.NextInt32();
                SetMappingMode(aMode);
                break;


            case GDI32.EMR_SETTEXTCOLOR:
                SetTextColor(aRecord.NextUInt32());
                break;

            case GDI32.EMR_SETBKCOLOR:
                SetBkColor(aRecord.NextUInt32());
                break;

            case GDI32.EMR_SETDCBRUSHCOLOR:
                SetDefaultBrushColor(aRecord.NextUInt32());
                break;

            case GDI32.EMR_SETDCPENCOLOR:
                SetDefaultPenColor(aRecord.NextUInt32());
                break;

            case GDI32.EMR_SETBKMODE:
                SetBkMode(aRecord.NextInt32());
                break;

            case GDI32.EMR_SETROP2:
                SetROP2((BinaryRasterOps)aRecord.NextInt32());
                break;

            case GDI32.EMR_SETTEXTALIGN:
            case GDI32.EMR_SETCOLORADJUSTMENT:
                break;

            case GDI32.EMR_MOVETOEX:
                MoveTo(aRecord.NextInt32(), aRecord.NextInt32());
                break;

            case GDI32.EMR_OFFSETCLIPRGN:
            case GDI32.EMR_SETMETARGN:
            case GDI32.EMR_EXCLUDECLIPRECT:
            case GDI32.EMR_INTERSECTCLIPRECT:
                break;

            case GDI32.EMR_SAVEDC:
                SaveState();
                break;

            case GDI32.EMR_RESTOREDC:
                RestoreState(aRecord.NextInt32());
                break;

                // Creates a generic pen of any style
            case GDI32.EMR_CREATEPEN:
                {
                    int aStyle = aRecord.NextInt32();
                    int width = aRecord.NextInt32();
                    uint colorref = aRecord.NextUInt32();
                    Guid uniqueID = this.UnpackGuid(aRecord);
                    CreateCosmeticPen((PenStyle)aStyle, colorref, uniqueID);
                }
                break;

            case GDI32.EMR_CREATECOSMETICPEN:
                {
                    int aStyle = aRecord.NextInt32();
                    int width = aRecord.NextInt32();
                    uint colorref = aRecord.NextUInt32();
                    Guid uniqueID = this.UnpackGuid(aRecord);
                    CreateCosmeticPen((PenStyle)aStyle, colorref, uniqueID);
                }
                break;


            case GDI32.EMR_CREATEBRUSHINDIRECT:
                {
                    int aStyle = aRecord.NextInt32();   // chunk += aStyle;
                    int hatch = aRecord.NextInt32(); // chunk += hatch;
                    uint color = aRecord.NextUInt32(); // chunk += color;

                    Guid uniqueID = this.UnpackGuid(aRecord);
                    CreateBrush(aStyle, hatch, color, uniqueID);
                }
                break;

            case GDI32.EMR_SELECTOBJECT:
                break;

            case GDI32.EMR_DELETEOBJECT:
                break;

            case GDI32.EMR_SELECTSTOCKOBJECT:
                SelectStockObject(aRecord.NextInt32());
                break;

            case GDI32.EMR_SELECTUNIQUEOBJECT:
                {
                    Guid uniqueID = this.UnpackGuid(aRecord);
                    SelectUniqueObject(uniqueID);
                }
                break;

            case GDI32.EMR_ELLIPSE:
                {
                    int left = aRecord.NextInt32();
                    int top = aRecord.NextInt32();
                    int right = aRecord.NextInt32();
                    int bottom = aRecord.NextInt32();

                    Ellipse(left, top, right, bottom);
                }
                break;

            case GDI32.EMR_ROUNDRECT:
                {
                    int left = aRecord.NextInt32();
                    int top = aRecord.NextInt32();
                    int right = aRecord.NextInt32();
                    int bottom = aRecord.NextInt32();
                    int width = aRecord.NextInt32();
                    int height = aRecord.NextInt32();

                    RoundRect(left, top, right, bottom, width, height);
                }
                break;

            case GDI32.EMR_RECTANGLE:
                {
                    int left = aRecord.NextInt32();
                    int top = aRecord.NextInt32();
                    int right = aRecord.NextInt32();
                    int bottom = aRecord.NextInt32();

                    Rectangle(left, top, right, bottom);
                }
                break;

            case GDI32.EMR_GRADIENTFILL:    // Only rectangles at the moment
                {
                    // Unpack the vertices
                    TRIVERTEX[] pVertex = this.UnpackTRIVERTEX(aRecord);

                    // unpack the gradient mesh
                    GRADIENT_RECT[] pMesh = UnpackGRADIENT_RECT(aRecord);

                    // pack the mode
                    uint dwMode = aRecord.NextUInt32();

                    this.GradientRectangle(pVertex, pMesh, dwMode);
                }
                break;

            case GDI32.EMR_ARC:
            case GDI32.EMR_ARCTO:
            case GDI32.EMR_ANGLEARC:
            case GDI32.EMR_CHORD:
            case GDI32.EMR_PIE:
                break;

            case GDI32.EMR_SELECTPALETTE:
            case GDI32.EMR_CREATEPALETTE:
            case GDI32.EMR_SETPALETTEENTRIES:
            case GDI32.EMR_RESIZEPALETTE:
            case GDI32.EMR_REALIZEPALETTE:
                break;

            case GDI32.EMR_LINETO:
                LineTo(aRecord.NextInt32(), aRecord.NextInt32());
                break;

            case GDI32.EMR_EXTFLOODFILL:
                break;

            case GDI32.EMR_POLYDRAW:
                //EMRPOLYDRAW polydraw = (EMRPOLYDRAW)aRecord;
                //fRenderer.PolyDraw(polydraw.aptl, polydraw.abTypes);
                break;

            case GDI32.EMR_SETARCDIRECTION:
            case GDI32.EMR_SETMITERLIMIT:
                break;

            case GDI32.EMR_BEGINPATH:
                BeginPath();
                break;

            case GDI32.EMR_ENDPATH:
                EndPath();
                break;

            case GDI32.EMR_FILLPATH:
                FillPath();
                break;

            case GDI32.EMR_STROKEANDFILLPATH:
                DrawPath();
                break;

            case GDI32.EMR_STROKEPATH:
                FramePath();
                break;

            case GDI32.EMR_ABORTPATH:
            case GDI32.EMR_FLATTENPATH:
            case GDI32.EMR_WIDENPATH:
                break;

            case GDI32.EMR_CLOSEFIGURE:

            case GDI32.EMR_SELECTCLIPPATH:
                break;

            case GDI32.EMR_BITBLT:
                {
                    // Get the X, Y
                    int x = aRecord.NextInt32();
                    int y = aRecord.NextInt32();
                    int width = aRecord.NextInt32();
                    int height = aRecord.NextInt32();

                    // Now create a pixbuff on the specified size
                    PixelBuffer pixBuff = new PixelBuffer(width, height);
                    int dataSize = aRecord.NextInt32();

                    // Copy the received data into it right pixel data pointer
                    aRecord.CopyTo(pixBuff.Pixels.Data, dataSize);

                    // And finally, call the BitBlt function
                    BitBlt(x,y,pixBuff);
                }
                break;


            case GDI32.EMR_POLYBEZIERTO:
                break;


            case GDI32.EMR_POLYBEZIER:
                {
                    Point[] points = UnpackPoints(aRecord);

                    // Now we have everything, so call the call
                    PolyBezier(points);
                }
                break;

            case GDI32.EMR_POLYGON:
                {
                    Point[] points = UnpackPoints(aRecord);

                    // Now we have everything, so call the call
                    Polygon(points);
                }
                break;

            case GDI32.EMR_POLYPOLYGON:
                break;


            case GDI32.EMR_POLYLINE:
                {
                    Point[] points = UnpackPoints(aRecord);

                    // Now we have everything, so call the call
                    PolyLine(points);
                }
                break;

            case GDI32.EMR_POLYLINETO:
                {
                    Point[] points = UnpackPoints(aRecord);

                    //PolyLineTo(points);
                }
                break;

            case GDI32.EMR_POLYPOLYLINE:
                break;

            case GDI32.EMR_POLYTEXTOUTA:
            case GDI32.EMR_POLYTEXTOUTW:
                break;


            case GDI32.EMR_EXTTEXTOUTA:
            case GDI32.EMR_EXTTEXTOUTW:
                {
                    int x = aRecord.NextInt32();
                    int y = aRecord.NextInt32();
                    int strLength = aRecord.NextInt32();
                    string text = (string)aRecord;
                    DrawString(x, y, text);
                }
                break;

            case GDI32.EMR_EXTCREATEFONTINDIRECTW:
                {
                    LOGFONT newFont = new LOGFONT();

                    int faceNameLength = aRecord.NextInt32();
                    newFont.lfFaceName = aRecord.NextUtf8String(faceNameLength);
                    newFont.lfHeight = aRecord.NextInt32();
                    newFont.lfCharSet = aRecord.NextByte();
                    newFont.lfClipPrecision = aRecord.NextByte();
                    newFont.lfEscapement = aRecord.NextInt32();
                    newFont.lfItalic = aRecord.NextByte();
                    newFont.lfOrientation = aRecord.NextInt32();
                    newFont.lfOutPrecision = aRecord.NextByte();
                    newFont.lfPitchAndFamily = aRecord.NextByte();
                    newFont.lfQuality = aRecord.NextByte();
                    newFont.lfStrikeOut = aRecord.NextByte();
                    newFont.lfUnderline = aRecord.NextByte();
                    newFont.lfWeight = aRecord.NextInt32();
                    newFont.lfWidth = aRecord.NextInt32();

                    Guid uniqueID = this.UnpackGuid(aRecord);

                    CreateFont(newFont.lfFaceName, newFont.lfHeight, uniqueID);
                }
                break;


            case GDI32.EMR_CREATEMONOBRUSH:
            case GDI32.EMR_EXTCREATEPEN:
            case GDI32.EMR_CREATEDIBPATTERNBRUSHPT:
                break;

            case GDI32.EMR_FILLRGN:
            case GDI32.EMR_FRAMERGN:
            case GDI32.EMR_INVERTRGN:
            case GDI32.EMR_PAINTRGN:
            case GDI32.EMR_EXTSELECTCLIPRGN:
                break;

            case GDI32.EMR_ALPHABLEND:
                {
                    // Get the X, Y
                    int x = aRecord.NextInt32();
                    int y = aRecord.NextInt32();
                    int width = aRecord.NextInt32();
                    int height = aRecord.NextInt32();

                    int srcX = aRecord.NextInt32();
                    int srcY = aRecord.NextInt32();
                    int srcWidth = aRecord.NextInt32();
                    int srcHeight = aRecord.NextInt32();

                    byte alpha = aRecord.NextByte();

                    // Now create a pixbuff on the specified size
                    int buffWidth = aRecord.NextInt32();
                    int buffHeight = aRecord.NextInt32();
                    PixelBuffer pixBuff = new PixelBuffer(buffWidth, buffHeight);
                    int dataSize = aRecord.NextInt32();

                    // Copy the received data into it right pixel data pointer
                    aRecord.CopyTo(pixBuff.Pixels.Data, dataSize);

                    // And finally, call the BitBlt function
                    AlphaBlend(x,y,width,height, pixBuff,srcX,srcY, srcWidth,srcHeight,alpha);
                }
                break;

            case GDI32.EMR_STRETCHBLT:
            case GDI32.EMR_MASKBLT:
            case GDI32.EMR_PLGBLT:
            case GDI32.EMR_SETDIBITSTODEVICE:
            case GDI32.EMR_STRETCHDIBITS:
            case GDI32.EMR_TRANSPARENTBLT:
                break;


            case GDI32.EMR_GDICOMMENT:
            case GDI32.EMR_GLSRECORD:
            case GDI32.EMR_GLSBOUNDEDRECORD:
                break;

            case GDI32.EMR_SETLAYOUT:
                break;

            case GDI32.EMR_PIXELFORMAT:
                break;

            case GDI32.EMR_CREATECOLORSPACE:
            case GDI32.EMR_SETCOLORSPACE:
            case GDI32.EMR_DELETECOLORSPACE:
            case GDI32.EMR_COLORCORRECTPALETTE:
            case GDI32.EMR_SETICMMODE:
            case GDI32.EMR_SETICMPROFILEA:
            case GDI32.EMR_SETICMPROFILEW:
            case GDI32.EMR_COLORMATCHTOTARGETW:
            case GDI32.EMR_CREATECOLORSPACEW:
                break;

            case GDI32.EMR_RESERVED_105:            // These will likely never show up
            case GDI32.EMR_RESERVED_106:
            case GDI32.EMR_RESERVED_107:
            case GDI32.EMR_RESERVED_108:
            case GDI32.EMR_RESERVED_109:
            case GDI32.EMR_RESERVED_110:
            case GDI32.EMR_RESERVED_117:
            case GDI32.EMR_RESERVED_119:
            case GDI32.EMR_RESERVED_120:
            case GDI32.EMR_POLYBEZIER16:    // Probably no real need to implement these
            case GDI32.EMR_POLYBEZIERTO16:
            case GDI32.EMR_POLYGON16:
            case GDI32.EMR_POLYPOLYGON16:
            case GDI32.EMR_POLYLINE16:
            case GDI32.EMR_POLYLINETO16:
            case GDI32.EMR_POLYPOLYLINE16:
            case GDI32.EMR_POLYDRAW16:
            default:
                //if (CommandReceived != null)
                //    CommandReceived(aRecord);
                break;
        }
    }
}

