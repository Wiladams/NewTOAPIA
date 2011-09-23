namespace NewTOAPIA.Drawing
{
    using System;
    using NewTOAPIA.Graphics;

    public class VGPaint
    {
        public VGPaintType PaintType {get; private set;}

        protected VGPaint(VGPaintType typeOfPaint)
        {
            PaintType = typeOfPaint;
        }
    }

    public class VGPaintColor : VGPaint
    {
        //VG_PAINT_COLOR_RAMP_SPREAD_MODE = 0x1A02,
        //VG_PAINT_COLOR_RAMP_STOPS = 0x1A03,
        //VG_PAINT_COLOR_RAMP_PREMULTIPLIED = 0x1A07,

        public ColorRGBA Color { get; set; }

        public VGColorRampSpreadMode SpreadMode {get; set;}
            //public ColorRampStops RampStops {get; set;}

        public VGPaintColor()
            :base(VGPaintType.VG_PAINT_TYPE_COLOR)
        {
        }
    }

    public class VGPaintLinearGradient : VGPaint
    {
        public float4 Gradient {get; set;}

        public VGPaintLinearGradient(float4 gradient)
            :base(VGPaintType.VG_PAINT_TYPE_LINEAR_GRADIENT)
        {
        }
    }

    public class VGPaintRadialGradient : VGPaint
    {
        public float2 Center { get; set; }
        public float Radius { get; set; }
        public float2 FocalPoint { get; set; }

        public VGPaintRadialGradient(float2 center, float radius, float2 focal)
            :base(VGPaintType.VG_PAINT_TYPE_RADIAL_GRADIENT)
        {
            this.Center = center;
            this.Radius = radius;
            this.FocalPoint = focal;
        }
    }

    public class VGPaintPattern : VGPaint
    {
        public int TilingMode {get; set;}

        public VGPaintPattern()
            :base(VGPaintType.VG_PAINT_TYPE_PATTERN)
        {
        }
    }
}