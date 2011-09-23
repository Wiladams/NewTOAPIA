using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.UI;

namespace NewTOAPIA
{
    public class PixmapShard : IDrawable
    {
        IPixelArray fPixmap;
        Rectangle fBoundary;
        Rectangle fFrame;

        #region Constructors
        public PixmapShard(IPixelArray pixmap, Rectangle boundary, Rectangle frame)
        {
            fPixmap = pixmap;
            fBoundary = boundary;
            fFrame = frame;
        }
        #endregion

        #region Properties
        public Rectangle Boundary
        {
            get { return fBoundary; }
            set { fBoundary = value; }
        }

        public Rectangle Frame
        {
            get { return fFrame; }
            set { fFrame = value; }
        }

        #endregion

        #region Public Methods
        #region IDrawable
        public virtual void Draw(DrawEvent devent)
        {
            devent.GraphPort.PixmapShardBlt(fPixmap, fBoundary, fFrame);
        }
        #endregion

        #endregion
    }
}
