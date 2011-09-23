using System;
using System.IO;
//using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;

using TOAPI.GDI32;
using TOAPI.Types;

    public class GDICommandRecipient
    {
        public delegate void ReceiveCommandHandler(EMR aRecord);

        public event ReceiveCommandHandler CommandReceived;

        IRenderGDI fRenderer;

        public GDICommandRecipient(GDIGeometryRenderer renderTo)
        {
            fRenderer = renderTo;
        }

        void ReceiveData(byte[] data, int size)
        {
            // Create a memory stream on the byte array
            MemoryStream recordStream = new MemoryStream(data, false);

            // Deserialize the object from the memory stream
            BinaryFormatter formatter = new BinaryFormatter();
            //SoapFormatter sf = new SoapFormatter();
            
            EMR aRecord = (EMR)formatter.Deserialize(recordStream);

            ReceiveCommand(aRecord);
        }

        public virtual void ReceiveCommand(EMR aRecord)
        {
            // Go execute the given command
            switch (aRecord.Command)
            {
                case GDI32.EMR_HEADER:
                    break;

                // Transform related
                case GDI32.EMR_SCALEVIEWPORTEXTEX:
                case GDI32.EMR_SCALEWINDOWEXTEX:
                case GDI32.EMR_SETWINDOWEXTEX:
                case GDI32.EMR_SETWINDOWORGEX:
                case GDI32.EMR_SETVIEWPORTEXTEX:
                    break;

                case GDI32.EMR_SETVIEWPORTORGEX:
                    EMRSETVIEWPORTORGEX viewportorigin = (EMRSETVIEWPORTORGEX)aRecord;
                    fRenderer.SetViewportOrigin(viewportorigin.ptlOrigin.x, viewportorigin.ptlOrigin.y);
                    break;

                case GDI32.EMR_SETWORLDTRANSFORM:
                case GDI32.EMR_MODIFYWORLDTRANSFORM:
                    break;

                case GDI32.EMR_SETBRUSHORGEX:
                    break;

                case GDI32.EMR_EOF:
                    EMREOF eof = (EMREOF)aRecord;
                    fRenderer.Flush();
                    break;

                case GDI32.EMR_SETPIXELV:
                    EMRSETPIXELV aPixel = (EMRSETPIXELV)aRecord;
                    fRenderer.SetPixel(aPixel.ptlPixel.x, aPixel.ptlPixel.y, aPixel.crColor);
                    break;

                case GDI32.EMR_SETMAPPERFLAGS:
                    break;

                case GDI32.EMR_SETPOLYFILLMODE:
                    EMRSETPOLYFILLMODE polyfillmode = (EMRSETPOLYFILLMODE)aRecord;
                    fRenderer.SetPolyFillMode((int)polyfillmode.iMode);
                    break;

                case GDI32.EMR_SETSTRETCHBLTMODE:
                    break;

                case GDI32.EMR_SETMAPMODE:
                    EMRSETMAPMODE mapMode = (EMRSETMAPMODE)aRecord;
                    fRenderer.SetMappingMode((MappingModes)mapMode.iMode);
                    break;


                case GDI32.EMR_SETTEXTCOLOR:
                    EMRSETTEXTCOLOR settextcolor = (EMRSETTEXTCOLOR)aRecord;
                    fRenderer.SetTextColor(settextcolor.crColor);
                    break;

                case GDI32.EMR_SETBKCOLOR:
                    EMRSETBKCOLOR bkColor = (EMRSETBKCOLOR)aRecord;
                    fRenderer.SetBkColor(bkColor.crColor);
                    break;

                case GDI32.EMR_SETDCBRUSHCOLOR:
                    EMRSETDCBRUSHCOLOR dcColor = (EMRSETDCBRUSHCOLOR)aRecord;
                    fRenderer.SetDefaultBrushColor(dcColor.crColor);
                    break;

                case GDI32.EMR_SETDCPENCOLOR:
                    EMRSETDCPENCOLOR dcPenColor = (EMRSETDCPENCOLOR)aRecord;
                    fRenderer.SetDefaultPenColor(dcPenColor.crColor);
                    break;

                case GDI32.EMR_SETBKMODE:
                    EMRSETBKMODE bkMode = (EMRSETBKMODE)aRecord;
                    fRenderer.SetBkMode((int)bkMode.iMode);
                    break;

                case GDI32.EMR_SETROP2:
                    EMRSETROP2 setRop2 = (EMRSETROP2)aRecord;
                    //fRenderer.SetROP2(setRop2.iMode);
                    break;

                case GDI32.EMR_SETTEXTALIGN:
                case GDI32.EMR_SETCOLORADJUSTMENT:
                    break;

                case GDI32.EMR_MOVETOEX:
                    EMRMOVETO aMove = (EMRMOVETO)aRecord;
                    fRenderer.MoveTo(aMove.ptl.x, aMove.ptl.y);
                    break;

                case GDI32.EMR_OFFSETCLIPRGN:
                case GDI32.EMR_SETMETARGN:
                case GDI32.EMR_EXCLUDECLIPRECT:
                case GDI32.EMR_INTERSECTCLIPRECT:
                    break;

                case GDI32.EMR_SAVEDC:
                    EMRSAVEDC saveDC = (EMRSAVEDC)aRecord;
                    fRenderer.SaveState();
                    break;

                case GDI32.EMR_RESTOREDC:
                    EMRRESTOREDC restoreDC = (EMRRESTOREDC)aRecord;
                    fRenderer.RestoreState(restoreDC.iRelative);
                    break;

                case GDI32.EMR_CREATEPEN:
                    EMRCREATEPEN createPen = (EMRCREATEPEN)aRecord;
                    fRenderer.CreatePen(createPen.lopn.lopnStyle,
                        createPen.lopn.lopnWidth,
                        createPen.lopn.lopnColor,
                        createPen.uniqueID);
                    break;

                case GDI32.EMR_CREATEBRUSHINDIRECT:
                    break;

                case GDI32.EMR_SELECTOBJECT:
                    break;

                case GDI32.EMR_DELETEOBJECT:
                    break;

                case GDI32.EMR_SELECTSTOCKOBJECT:
                    EMRSELECTSTOCKOBJECT stockObject = (EMRSELECTSTOCKOBJECT)aRecord;
                    fRenderer.SelectStockObject(stockObject.ihObject);
                    break;

                case GDI32.EMR_SELECTUNIQUEOBJECT:
                    EMRSELECTUNIQUEOBJECT uniqueObject = (EMRSELECTUNIQUEOBJECT)aRecord;
                    fRenderer.SelectUniqueObject(uniqueObject.uniqueID);
                    break;

                case GDI32.EMR_ELLIPSE:
                    EMRELLIPSE ellipse = (EMRELLIPSE)aRecord;
                    fRenderer.Ellipse(ellipse.rclBox.Left, ellipse.rclBox.Top, ellipse.rclBox.Right, ellipse.rclBox.Bottom);
                    break;

                case GDI32.EMR_ROUNDRECT:
                    EMRROUNDRECT round = (EMRROUNDRECT)aRecord;
                    fRenderer.RoundRect(round.rclBox.Left,round.rclBox.Top,round.rclBox.Right,round.rclBox.Bottom,
                        round.szlCorner.Width,round.szlCorner.Height);
                    break;

                case GDI32.EMR_RECTANGLE:
                    EMRRECTANGLE rectangle = (EMRRECTANGLE)aRecord;
                    fRenderer.Rectangle(rectangle.rclBox);
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
                case GDI32.EMR_EXTFLOODFILL:
                    break;

                case GDI32.EMR_LINETO:
                    EMRLINETO aLine = (EMRLINETO)aRecord;
                    fRenderer.LineTo(aLine.ptl);
                    break;

                case GDI32.EMR_POLYDRAW:
                    EMRPOLYDRAW polydraw = (EMRPOLYDRAW)aRecord;
                    fRenderer.PolyDraw(polydraw.aptl, polydraw.abTypes);
                    break;

                case GDI32.EMR_SETARCDIRECTION:
                case GDI32.EMR_SETMITERLIMIT:
                    break;

                case GDI32.EMR_ABORTPATH:
                case GDI32.EMR_BEGINPATH:
                case GDI32.EMR_ENDPATH:
                case GDI32.EMR_CLOSEFIGURE:
                case GDI32.EMR_FILLPATH:
                case GDI32.EMR_STROKEANDFILLPATH:
                case GDI32.EMR_STROKEPATH:
                case GDI32.EMR_FLATTENPATH:
                case GDI32.EMR_WIDENPATH:
                    break;

                case GDI32.EMR_SELECTCLIPPATH:
                    break;

                case GDI32.EMR_BITBLT:
                    EMRBITBLT bitblt = (EMRBITBLT)aRecord;
                    Console.WriteLine("Received: {0}", aRecord.Command);
                    break;

                case GDI32.EMR_POLYBEZIER:
                    EMRPOLYBEZIER polybezier = (EMRPOLYBEZIER)aRecord;
                    fRenderer.PolyBezier(polybezier.aptl);
                    break;

                case GDI32.EMR_POLYGON:
                    EMRPOLYGON polygon = (EMRPOLYGON)aRecord;
                    fRenderer.Polygon(polygon.aptl);
                    break;

                case GDI32.EMR_POLYLINE:
                    EMRPOLYLINE polyLine = (EMRPOLYLINE)aRecord;
                    fRenderer.PolyLine(polyLine.aptl);
                    break;

                case GDI32.EMR_EXTTEXTOUTA:
                case GDI32.EMR_EXTTEXTOUTW:
                    EMREXTTEXTOUTA textout = (EMREXTTEXTOUTA)aRecord;
                    fRenderer.DrawString(textout.emrtext.ptlReference.x, textout.emrtext.ptlReference.y, textout.emrtext.text);
                    break;



                case GDI32.EMR_GDICOMMENT:
                case GDI32.EMR_FILLRGN:
                case GDI32.EMR_FRAMERGN:
                case GDI32.EMR_INVERTRGN:
                case GDI32.EMR_PAINTRGN:
                case GDI32.EMR_EXTSELECTCLIPRGN:
                case GDI32.EMR_ALPHABLEND:
                case GDI32.EMR_STRETCHBLT:
                case GDI32.EMR_MASKBLT:
                case GDI32.EMR_PLGBLT:
                case GDI32.EMR_SETDIBITSTODEVICE:
                case GDI32.EMR_STRETCHDIBITS:
                case GDI32.EMR_TRANSPARENTBLT:
                case GDI32.EMR_POLYBEZIERTO:
                case GDI32.EMR_POLYPOLYGON:
                case GDI32.EMR_POLYLINETO:
                case GDI32.EMR_POLYPOLYLINE:
                case GDI32.EMR_CREATEMONOBRUSH:
                case GDI32.EMR_CREATEDIBPATTERNBRUSHPT:
                case GDI32.EMR_EXTCREATEPEN:
                case GDI32.EMR_EXTCREATEFONTINDIRECTW:
                case GDI32.EMR_POLYTEXTOUTA:
                case GDI32.EMR_POLYTEXTOUTW:

                case GDI32.EMR_SETICMMODE:

                case GDI32.EMR_CREATECOLORSPACE:
                case GDI32.EMR_SETCOLORSPACE:
                case GDI32.EMR_DELETECOLORSPACE:
                case GDI32.EMR_GLSRECORD:
                case GDI32.EMR_GLSBOUNDEDRECORD:
                case GDI32.EMR_PIXELFORMAT:
                case GDI32.EMR_COLORCORRECTPALETTE:
                case GDI32.EMR_SETICMPROFILEA:
                case GDI32.EMR_SETICMPROFILEW:
                case GDI32.EMR_SETLAYOUT:
                case GDI32.EMR_GRADIENTFILL:
                case GDI32.EMR_COLORMATCHTOTARGETW:
                case GDI32.EMR_CREATECOLORSPACEW:

                // These will likely never show up
                case GDI32.EMR_RESERVED_105:
                case GDI32.EMR_RESERVED_106:
                case GDI32.EMR_RESERVED_107:
                case GDI32.EMR_RESERVED_108:
                case GDI32.EMR_RESERVED_109:
                case GDI32.EMR_RESERVED_110:
                case GDI32.EMR_RESERVED_117:
                case GDI32.EMR_RESERVED_119:
                case GDI32.EMR_RESERVED_120:

                // Probably no real need to implement these
                case GDI32.EMR_POLYBEZIER16:
                case GDI32.EMR_POLYBEZIERTO16:
                case GDI32.EMR_POLYGON16:
                case GDI32.EMR_POLYPOLYGON16:
                case GDI32.EMR_POLYLINE16:
                case GDI32.EMR_POLYLINETO16:
                case GDI32.EMR_POLYPOLYLINE16:
                case GDI32.EMR_POLYDRAW16:                
                default:
                    if (CommandReceived != null)
                        CommandReceived(aRecord);
                    break;
            }
        }
    }

