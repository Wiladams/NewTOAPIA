using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.Drawing;

namespace DistributedDesktop
{
    public class UIPortDelegate : IUIPort
    {
        public event PixBltPixelBuffer24 PixBltPixelBuffer24Handler;
        public event PixBltPixelArray PixBltPixelArrayHandler;
        public event PixBltBGRb PixBltBGRbHandler;
        public event PixBltBGRAb PixBltBGRAbHandler;
        public event PixBltLumb PixBltLumbHandler;


        public virtual void AddPort(IUIPort aPort)
        {
            PixBltPixelBuffer24Handler += aPort.PixBltPixelBuffer24;
            PixBltPixelArrayHandler += aPort.PixBltPixelArray;
            PixBltBGRbHandler += aPort.PixBltBGRb;
            PixBltBGRAbHandler += aPort.PixBltBGRAb;
            PixBltLumbHandler += aPort.PixBltLumb;
        }

        public virtual void PixBltPixelBuffer24(GDIDIBSection pixMap, int x, int y)
        {
            if (null != PixBltPixelBuffer24Handler)
                PixBltPixelBuffer24Handler(pixMap, x, y);
        }

        public virtual void PixBltPixelArray(IPixelArray pixBuff, int x, int y)
        {
            if (null != PixBltPixelArrayHandler)
                PixBltPixelArrayHandler(pixBuff, x, y);
        }

        public virtual void PixBltBGRb(PixelAccessor<BGRb> pixBuff, int x, int y)
        {
            if (null != PixBltBGRbHandler)
                PixBltBGRbHandler(pixBuff, x, y);
        }

        public virtual void PixBltBGRAb(PixelArray<BGRAb> pixBuff, int x, int y)
        {
            if (null != PixBltBGRAbHandler)
                PixBltBGRAbHandler(pixBuff, x, y);
        }

        public virtual void PixBltLumb(PixelArray<Lumb> pixBuff, int x, int y)
        {
            if (null != PixBltLumbHandler)
                PixBltLumbHandler(pixBuff, x, y);
        }

    }
}
