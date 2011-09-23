using System;
using System.Drawing;
using System.Collections.Generic;

using NewTOAPIA;
using NewTOAPIA.Drawing;

    public class ScreenSnapper
    {
        GDIContext fContext;

        #region Constructors
        public ScreenSnapper()
        {
            fContext = GDIContext.CreateForDefaultDisplay();
        }

        public ScreenSnapper(GDIContext aContext)
        {
            fContext = aContext;
        }
        #endregion

        public GDIContext Context
        {
            get { return fContext; }
            set { fContext = value; }
        }

        public GDIDIBSection SnapAPicture(Rectangle rect)
        {
            GDIDIBSection contextImage = new GDIDIBSection(rect.Width, rect.Height, BitCount.Bits24);

            contextImage.DeviceContext.BitBlt(fContext, new Point(rect.X, rect.Y), new Rectangle(0, 0, rect.Width, rect.Height),
                (TernaryRasterOps.SRCCOPY | TernaryRasterOps.CAPTUREBLT));


            return contextImage;
        }
    }

