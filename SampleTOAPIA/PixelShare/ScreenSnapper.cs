using System;
using System.Drawing;

using NewTOAPIA;
using NewTOAPIA.Drawing;

namespace PixelShare.Core
{
    /// <summary>
    /// The ScreenSnapper class takes snapshot images of any given device context that is 
    /// capable of acting as a source for a GDI BitBlt operation.  As it is GDI based, 
    /// the pixel buffer that is used for the capture must be a GDI based pixel buffer that
    /// has an associated GDI Device Context.  The GDIDIBSection object is one such bitmap,
    /// and thus it shows up in the interface.
    /// 
    /// This code can be adapted to work with other pixel buffer objects, as a convenience, 
    /// but that can be handled just as simply outside the context of this code.
    /// 
    /// The primary function here is the BitBlt operation within the two Snapshot routines.
    /// This basic GDI call takes a source context and rectangle, and performs a BitBlt to 
    /// the destination context (the bitmap).  It's a fairly fast operation, depending on the 
    /// context you're taking the snapshot from.
    /// 
    /// Creating a context for a Window is performed easily from the Window Handle.  Any window, 
    /// such as a User32 based control could be used as well, so snapshots can be taken of specific
    /// controls within a window, and not just an entire window or the screen.
    /// </summary>
    public class ScreenSnapper
    {
        GDIContext fContext;

        #region Constructors
        /// <summary>
        /// This default constructor will create a snapper for the default display context.
        /// </summary>
        public ScreenSnapper()
        {
            fContext = GDIContext.CreateForDefaultDisplay();
        }

        /// <summary>
        /// This constructor will create a snapper for a specific GDIContext.
        /// The context can be for any GDI device that is capable of being a 
        /// source of a GDI BitBlt operation.
        /// </summary>
        /// <param name="aContext">The specific GDI context that will be the source of captures.</param>
        public ScreenSnapper(GDIContext aContext)
        {
            fContext = aContext;
        }
        #endregion

        /// <summary>
        /// Allow setting and getting the context.  This is a thread safe operation as the current snapping
        /// will continue with whichever context was present at the time the snap was initiated.
        /// </summary>
        public GDIContext Context
        {
            get { return fContext; }
            set { fContext = value; }
        }

        /// <summary>
        /// Take a snapshot of the currently held context.  Use the rectangle to determine what part of 
        /// the context will be captured.
        /// </summary>
        /// <param name="rect">The Source rectangle of the snapshot.  This is the area of the context that will be captured.</param>
        /// <returns>Return an allocated PixelBuffer24 object that contains the snapshot.</returns>
        public GDIDIBSection SnapAPicture(Rectangle rect)
        {
            GDIDIBSection contextImage = new GDIDIBSection(rect.Width, rect.Height, BitCount.Bits24);

            contextImage.DeviceContext.BitBlt(fContext, new Point(rect.X, rect.Y), new Rectangle(0, 0, rect.Width, rect.Height),
                (TernaryRasterOps.SRCCOPY | TernaryRasterOps.CAPTUREBLT));

            return contextImage;
        }

        /// <summary>
        /// Take a snapshot of the current held context.  Use the rectangle as the area of the source
        /// that is to be captured.
        /// </summary>
        /// <param name="rect">The Source area to be captured.</param>
        /// <param name="pixMap">The PixelBuffer object that will receive the snapshot.</param>
        public void SnapAPicture(Rectangle rect, GDIDIBSection pixMap)
        {
            pixMap.DeviceContext.BitBlt(fContext, new Point(rect.X, rect.Y), new Rectangle(0, 0, rect.Width, rect.Height),
                (TernaryRasterOps.SRCCOPY | TernaryRasterOps.CAPTUREBLT));

        }
    }
}

