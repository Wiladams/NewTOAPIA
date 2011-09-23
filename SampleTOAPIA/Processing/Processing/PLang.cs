using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processing
{
    using System.Threading;

    using NewTOAPIA.Graphics;


    public class PLang
    {
        #region Contants
        protected const float HALF_PI = 1.57079632679489661923f;
        protected const float PI = 3.14159265358979323846f;
        protected const float QUARTER_PI = 0.7853982f;
        protected const float TWO_PI = 6.28318530717958647693f;


        // colorMode
        protected const int RGB = 0;
        protected const int HSB = 1;

        // stroke Join
        protected const int ROUND = 0;
        protected const int MITER = 2;
        protected const int BEVEL = 1;

        // stroke EndCap
        //protected const int ROUND = 0;
        protected const int SQUARE = 1;
        protected const int PROJECT = 2;

        // rectMode
        protected const int CORNER = 0;
        protected const int CORNERS = 1;
        protected const int CENTER = 0x02;  // used by mouse buttons as well
        protected const int RADIUS = 3;


        // beginShape/endShape
        protected const int NOSHAPE = 0;
        protected const int CLOSE = 0x01;
        protected const int POINTS = 0x02;
        protected const int LINES = 0x04;
        protected const int TRIANGLES = 0x08;
        protected const int TRIANGLE_FAN = 0x10;
        protected const int TRIANGLE_STRIP = 0x20;
        protected const int QUADS = 0x40;
        protected const int QUAD_STRIP = 0x80;

        // mouse buttons
        protected const int LEFT = 0x01;
        protected const int RIGHT = 0x04;
        #endregion Constants

        #region Attributes

        public color backgroundColor { get; set; }   // The background color

        color fFillColor;
        public bool useFill { get; set; }

        color strokeColor;
        public bool useStroke { get; set; }

        int fColorMode;
        vec4 fColorRange;

        int fStrokeWeight;
        int fStrokeJoin;
        int fStrokeEndCap;

        // rectangle attributes
        int fEllipseMode;
        int fRectMode;

        // shape
        int fShapeMode;
        bool inShape;

        // mouse
        public virtual int mouseX { get; set; }
        public virtual int mouseY { get; set; }
        public virtual bool isMousePressed { get; set; }
        public virtual bool isMouseReleased { get; set; }
        public virtual int mouseButton { get; set; }
        #endregion

        Random rng = new Random();

        List<PVertex> fVertices = new List<PVertex>();

        #region Constructors
        public PLang()
            :this(null)
        {
        }

        public PLang(PRenderer rndr)
        {
            colorMode(RGB, 255);

            // Stroke default
            fStrokeWeight = 1;
            fStrokeJoin = ROUND;
            fStrokeEndCap = SQUARE;
            strokeColor = new color(0, 0, 0, 255);
            useStroke = true;

            // Fill Default
            fFillColor = new color(255, 255, 255, 255);
            useFill = true;

            backgroundColor = new color(255, 255, 255, 255);

            fRectMode = CORNER;
            fEllipseMode = CENTER;

            fShapeMode = NOSHAPE;

            SetRenderer(rndr);
        }

        protected PRenderer Renderer { get; private set; }
        
        protected void SetRenderer(PRenderer r)
        {
            Renderer = r;

            if (r == null)
                return;

            fill(fFillColor);
            stroke(strokeColor);
            //ResetBrush();
            //ResetPen();
        }
        #endregion

        #region Structure
        // delay
        public void delay(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }


        // exit
        public void exit()
        {
        }

        // loop
        public void loop()
        {
        }

        // noloop
        public void noLoop()
        {
        }

        // popStyle
        public void popStyle()
        {
        }

        // pushStyle
        public void pushStyle()
        {
        }

        // redraw
        public virtual void redraw()
        {
        }

        // size
        public virtual void size(int width_, int height_)
        {
            width = width_;
            height = height_;

            onsize(width, height);

            ResetBrush();
            ResetPen();
        }

        public virtual void onsize(int width, int height)
        {
        }
        #region To be implemented by Sketches
        // draw
        public virtual void draw()
        {
            exit();
        }

        // setup
        public virtual void setup()
        {
        }
        #endregion
        #endregion

        #region Language Features
        #region Color
        #region Setting
        #region background
        public void background(int gray)
        {
            color c = color(gray);
            background(c);
        }

        public void background(int gray, int alpha)
        {
            color c = color(gray,alpha);
            background(c);
        }

        public void background(int v1, int v2, int v3)
        {
            color c = color(v1, v2, v3);
            background(c);
        }

        public void background(int v1, int v2, int v3, int alpha)
        {
            color c = color(v1, v2, v3, alpha);
            background(c);
        }

        public void background(color c, int alpha)
        {
            color newColor = new color(c.red, c.green, c.blue, (byte)alpha);
            background(newColor);
        }

        public void background(color c)
        {
            backgroundColor = c;
            Renderer.ClearToColor(c);

            ResetPen();
            ResetBrush();
        }

        #endregion background

        #region colorMode
        public void colorMode(int mode)
        {
            // Just change the mode, don't mess
            // with the color ranges
            fColorMode = mode;
        }

        public void colorMode(int mode, int range)
        {
            colorMode(mode,range, range, range, range);
        }

        public void colorMode(int mode, int range1, int range2, int range3)
        {
            colorMode(mode, range1, range2, range3, (int)fColorRange.w);
        }

        public void colorMode(int mode, int range1, int range2, int range3, int range4)
        {
            fColorMode = mode;
            fColorRange = new vec4(range1, range2, range3, range4);
        }
        #endregion colorMode

        #region fill
        public void fill(int gray)
        {
            color c = color(gray);
            fill(c);
        }

        public void fill(int gray, int alpha)
        {
            color c = color(gray, alpha);
            fill(c);
        }

        public void fill(int v1, int v2, int v3)
        {
            color c = color(v1, v2, v3);
            fill(c);
        }

        public void fill(int v1, int v2, int v3, int alpha)
        {
            color c = color(v1, v2, v3, alpha);
            fill(c);
        }


        public void fill(color c, int alpha)
        {
            color newColor = new color(c.red, c.green, c.blue, (byte)alpha);
            fill(c);
        }

        public void fill(color c)
        {
            fFillColor = c;
            useFill = true;
            ResetBrush();
        }

        // noFill
        public void noFill()
        {
            useFill = false;
            ResetBrush();
        }
        #endregion fill

        #region stroke
        public void stroke(int gray)
        {
            color c = color(gray);
            stroke(c);
        }

        public void stroke(int gray, int alpha)
        {
            color c = color(gray, alpha);
            stroke(c);
        }

        public void stroke(int v1, int v2, int v3)
        {
            color c = color(v1, v2, v3);
            stroke(c);
        }

        public void stroke(int v1, int v2, int v3, int alpha)
        {
            color c = color(v1, v2, v3, alpha);
            stroke(c);
        }


        public void stroke(color c, int alpha)
        {
            color newColor = new color(c.red, c.green, c.blue, (byte)alpha);
            stroke(newColor);
        }

        public void stroke(color c)
        {
            byte red = (byte)Math.Round(lerp(0, 255, norm(c.red, 0, fColorRange.r)));
            byte green = (byte)Math.Round(lerp(0, 255, norm(c.green, 0, fColorRange.g)));
            byte blue = (byte)Math.Round(lerp(0, 255, norm(c.blue, 0, fColorRange.b)));
            byte alpha = (byte)Math.Round(lerp(0, 255, norm(c.alpha, 0, fColorRange.a)));

            color newColor = new color(red, green, blue, alpha);

            // Clamp the color to the color range
            strokeColor = newColor;
            useStroke = true;
            ResetPen();
        }

        // noStroke
        public void noStroke()
        {
            useStroke = false;
        }
        #endregion
        #endregion Setting

        #region Creating & Reading
        // alpha
        public float alpha(color c)
        {
            return c.alpha;
        }

        // blendColor
        public color blendColor(color c1, color c2, BLENDMODE mode)
        {
            return new color(c1.red, c1.green, c1.blue);
        }

        // blue
        public float blue(color c)
        {
            return c.blue;
        }

        // brightness
        // color
        public color color(int gray)
        {
            return new color((byte)gray);
        }

        public color color(int gray, int alpha)
        {
            return new color((byte)gray, (byte)alpha);
        }

        public color color(int v1, int v2, int v3)
        {
            // Check the color mode
            // If it is RGB, the construct a RGB color
            return new color((byte)v1, (byte)v2, (byte)v3);
        }

        public color color(double v1, double v2, double v3)
        {
            // Check the color mode
            // If it is RGB, the construct a RGB color
            return new color((byte)v1, (byte)v2, (byte)v3);
        }

        public color color(int v1, int v2, int v3, int alpha)
        {
            // Check the color mode
            // If it is RGB, the construct a RGB color
            return new color((byte)v1, (byte)v2, (byte)v3, (byte)alpha);
        }

        // green
        public float green(color c)
        {
            return c.green;
        }

        // hue
        // lerpColor
        
        // red
        public float red(color c)
        {
            return c.red;
        }

        // saturation
        #endregion Creating & Reading
        #endregion Color

        #region Data
        #region Conversion

        #endregion Conversion

        #region String Functions
        #endregion

        #region Array Functions
        #endregion Array Functions

        #endregion

        #region Environment
        public int width {get; set;}
        public int height { get; set; }

        public void cursor()
        {
        }

        public void cursor(MODE mode)
        {
        }

        public void cursor(PImage image, int x, int y)
        {
        }

        public void frameRate(int fps)
        {
        }

        /// <summary>
        /// Hides the cursor from view.
        /// </summary>
        public void noCursor()
        {
        }
        
        #endregion

        #region Image
        public PImage createImage(int width, int height, FORMAT format)
        {
            return new PImage(width, height, format);
        }

        #region Loading & Displaying
        // image
        // imageMode
        // loadImage
        // noTint
        // requestImage
        // tint
        #endregion Loading & Displaying

        #region Pixels
        // blend
        // copy
        // filter
        // get
        // loadPixels
        // pixels
        // set
        public void set(int x, int y, color c)
        {
            Renderer.SetPixel(x, y, c);
        }

        // updatePixels
        #endregion Pixels
        #endregion

        #region Input
        #region Mouse
        #endregion Mouse

        #region Keyboard
        #endregion Keyboard

        #region Files
        #endregion Files

        #region Web
        #endregion Web

        #region Time & Date
        #endregion Time & Date


        #endregion Input

        #region Lights, Camera
        #region Lights
        #endregion Lights

        #region Camera
        #endregion Camera

        #region Coordinates
        #endregion Coordinates

        #region Material Properties
        #endregion Material Properties
        #endregion Lights, Camera

        #region Math
        #region Calculation
        // abs
        public float abs(float x)
        {
            return Math.Abs(x);
        }

        public int abs(int x)
        {
            return Math.Abs(x);
        }

        // ceil
        public int ceil(float x)
        {
            return (int)Math.Ceiling(x);
        }

        // constrain
        // This is like clamp
        public float constrain(float x, float minVal, float maxVal)
        {
            if (x < minVal)
                return minVal;

            if (x > maxVal)
                return maxVal;

            return x;
        }

        public int constrain(int x, int minVal, int maxVal)
        {
            if (x < minVal)
                return minVal;

            if (x > maxVal)
                return maxVal;

            return x;
        }

        // dist
        // calculate distance between two points
        public float dist(int x1, int y1, int x2, int y2)
        {
            vec2 v1 = new vec2(x1, y1);
            vec2 v2 = new vec2(x2, y2);
            vec2 v3 = v1 - v2;
            
            return (float)v3.Length;
        }

        public float dist(float x1, float y1, float x2, float y2)
        {
            vec2 v1 = new vec2(x1, y1);
            vec2 v2 = new vec2(x2, y2);
            vec2 v3 = v1 - v2;
            return (float)v3.Length;
        }

        // exp
        public float exp(float x)
        {
            return (float)Math.Exp(x);
        }

        public float exp(int x)
        {
            return (float)Math.Exp(x);
        }

        // floor
        public int floor(float x)
        {
            return (int)Math.Floor(x);
        }

        // lerp
        public float lerp(double value1, double value2, double amt)
        {
            double distance = value2 - value1;
            return (float)(value1 + amt * distance);
        }

        public float lerp(int value1, int value2, float amt)
        {
            float distance = value2 - value1;
            return value1 + amt * distance;
        }

        // log
        public float log(float x)
        {
            return (float)Math.Log(x);
        }

        public float log(int x)
        {
            return (float)Math.Log(x);
        }

        // mag
        public float mag(float x, float y)
        {
            vec2 v = new vec2(x, y);
            return (float)v.Length;
        }

        public float mag(float x, float y, float z)
        {
            vec3 v = new vec3(x, y, z);
            return (float)v.Length;
        }

        // map
        public float map(float x, float low1, float high1, float low2, float high2)
        {
            if (x < low1 || x > high1)
                return x;

            float pct1 = (x - low1) / (high1 - low1);
            float result = low2 + pct1 * (high2 - low2);

            return result;
        }

        // max
        public float max(float x, float y)
        {
            return x > y ? x : y;
        }

        public float max(float x, float y, float z)
        {
            return max(max(x, y), z);
        }

        public float max(float[] xv)
        {
            float max = float.MinValue;

            foreach (float f in xv)
                max = f > max ? f : max;

            return max;
        }

        // min
        public float min(float x, float y)
        {
            return x > y ? x : y;
        }

        public float min(float x, float y, float z)
        {
            return min(min(x, y), z);
        }

        public float min(float[] xv)
        {
            float min = float.MaxValue;

            foreach (float f in xv)
                min = f < min ? f : min;

            return min;
        }

        // norm
        public float norm(float x, float low, float high)
        {
            return map(x, low, high, 0f, 1f);
        }

        // pow
        public float pow(float num, float exponent)
        {
            return (float)Math.Pow(num, exponent);
        }

        // round
        public int round(float value)
        {
            return (int)Math.Round(value, MidpointRounding.AwayFromZero);
        }

        // sq
        public float sq(float x)
        {
            return x * x;
        }

        // sqrt
        public float sqrt(float x)
        {
            return (float)Math.Sqrt(x);
        }
        #endregion Calculation

        #region Trigonometry
        const float degToRad = (float)(Math.PI / 180);
        const float radToDeg = (float)(180.0f / Math.PI);

        // acos
        public float acos(float v)
        {
            return (float)Math.Acos(v);
        }

        // asin
        public float asin(float x)
        {
            return (float)Math.Asin(x);
        }

        // atan
        public float atan(float x)
        {
            return (float)Math.Atan(x);
        }

        // atan2
        public float atan2(float y, float x)
        {
            return (float)Math.Atan2(y,x);
        }

        // cos
        public float cos(float x)
        {
            return (float)Math.Cos(x);
        }

        // degrees
        static public float degrees(float x)
        {
            return x * radToDeg;
        }

        // radians
        static public float radians(float x)
        {
            return x * degToRad;
        }

        // sin
        public float sin(float x)
        {
            return (float)Math.Sin(x);
        }

        // tan
        public float tan(float x)
        {
            return (float)Math.Tan(x);
        }

        #endregion Trigonometry

        #region Random
        // noise
        public float noise(float x)
        {
            return Noise.Noise1(x);
        }

        public float noise(float x, float y)
        {
            return Noise.Noise2(x,y);
        }

        public float noise(float x, float y, float z)
        {
            return Noise.Noise3(x,y,z);
        }

        // noiseDetail
        public void noiseDetail(int octaves)
        {
        }

        public void noiseDetail(int octaves, float falloff)
        {
        }

        // noiseSeed
        public float noiseSeed(int x)
        {
            return (float)x;
        }

        // random
        public float random(float high)
        {
            double low = 0;
            double range = high;
            double next = rng.NextDouble();
            float r = (float)(low + next * range);

            return r;
        }

        public float random(float low, float high)
        {
            double range = high - low;
            double next = rng.NextDouble();
            float r = (float)(low + next * range);

            return r;
        }

        // randomSeed
        public void randomSeed(int seed)
        {
            rng = new Random(seed);
        }
        #endregion Random
        #endregion Math

        #region Output
        #region Text Area
        // print
        // println
        #endregion Text Area

        #region Image
        // save
        // saveFrame
        #endregion Image

        #region Files
        // PrintWriter
        // beginRaw
        // beginRecord
        // createOutput
        // createReader
        // createWriter
        // endRaw
        // endRecord
        // saveBytes
        // saveStream
        // saveStrings
        // selectOutput
        #endregion Files
        #endregion Output

        #region Rendering
        // createGraphics
        PImage createGraphics(int width, int heigh, PRenderer renderer)
        {
            return null;
        }

        // hint()
        public void hint(int item)
        {
        }
        #endregion Rendering

        #region Shape
        #region 2D Primitives
        void ResetBrush()
        {
            Colorref bColor = Colorref.FromRGB(fFillColor.red, fFillColor.green, fFillColor.blue);

            GBrush brush = null;

            if (useFill)
                brush = new GBrush(BrushStyle.Solid, HatchStyle.Horizontal, bColor, Guid.NewGuid());
            else
                brush = new GBrush(BrushStyle.Hollow, HatchStyle.Horizontal, bColor, Guid.NewGuid());

            Renderer.SetBrush(brush);
        }

        void ResetPen()
        {
            Renderer.strokeWidth = fStrokeWeight;

            PenType pType = PenType.Geometric;
            PenStyle pStyle = PenStyle.Solid;
            PenJoinStyle pJoin = PenJoinStyle.Round;
            PenEndCap pEndCap = PenEndCap.Round;

            switch (fStrokeJoin)
            {
                case ROUND:
                    pJoin = PenJoinStyle.Round;
                    break;

                case MITER:
                    pJoin = PenJoinStyle.Miter;
                    break;

                case BEVEL:
                    pJoin = PenJoinStyle.Bevel;
                    break;
            }

            switch (fStrokeEndCap)
            {
                case ROUND:
                    pEndCap = PenEndCap.Round;
                    break;

                case PROJECT:
                    pEndCap = PenEndCap.Square;
                    break;

                case SQUARE:
                    pEndCap = PenEndCap.Flat;
                    break;
            }

            GPen pen = new GPen(PenType.Geometric, PenStyle.Solid, pJoin, pEndCap, strokeColor, fStrokeWeight, Guid.NewGuid());
            Renderer.SetPen(pen);
        }

        public RectangleI GetRectForMode(float x, float y, float width, float height, int mode)
        {
            int left = (int)Math.Round(x);
            int top = (int)Math.Round(y);
            int right = (int)Math.Round(x + width);
            int bottom = (int)Math.Round(y + height);

            switch (mode)
            {

                case CORNERS:
                    left = (int)x;
                    top = (int)y;
                    right = (int)width;
                    bottom = (int)height;
                    break;

                case CENTER:
                    left = (int)(x - width / 2);
                    top = (int)(y - height / 2);
                    right = (int)(left + width);
                    bottom = (int)(top + height);
                    break;

                case CORNER:
                    left = (int)x;
                    top = (int)y;
                    right = (int)(left + width);
                    bottom = (int)(top + height);
                    break;

                case RADIUS:
                    break;

                default:
                    left = (int)x;
                    top = (int)y;
                    right = (int)(left + width);
                    bottom = (int)(top + height);
                    break;
            }

            return new RectangleI(left, top, right, bottom);
        }

        public void arc(float x, float y, float width, float height, float start, float stop)
        {
            RectangleI r = GetRectForMode(x, y, width, height, fEllipseMode);

            Renderer.Arc(r.Left, r.Top, width, height, start, stop);
        }


        public void ellipse(float x, float y, float width, float height)
        {
            RectangleI r = GetRectForMode(x, y, width, height, fEllipseMode);

                Renderer.Ellipse(r.Left, r.Top, r.Right, r.Bottom);
        }

        public void line(float x1, float y1, float x2, float y2)
        {
            if (!useStroke)
                return;

            Renderer.Line(x1, y1, x2, y2);
        }

        public void line(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            Renderer.Line(x1, y1, x2, y2);
        }

        #region point
        public void point(float x, float y)
        {
            if (fStrokeWeight <= 1)
                Renderer.SetPixel(x, y, strokeColor);
            else
                line(x, y, x, y);
        }

        public void point(float x, float y, float z)
        {
            point(x, y);
        }
        #endregion

        public void quad(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            Point2I[] pts = {
                                new Point2I((int)Math.Round(x1), (int)Math.Round(y1)),
                                new Point2I((int)Math.Round(x2), (int)Math.Round(y2)),
                                new Point2I((int)Math.Round(x3), (int)Math.Round(y3)),
                                new Point2I((int)Math.Round(x4), (int)Math.Round(y4))};

            Renderer.Polygon(pts);
        }


        public void rect(float x, float y, float width, float height)
        {
            RectangleI r = GetRectForMode(x, y, width, height, fRectMode);

            Renderer.Rectangle(r.Left, r.Top, width, height);

            //if (useStroke && useFill)
            //{
            //    Renderer.StrokeAndFillRectangle(r.Left, r.Top, width, height, fFillColor);
            //    return;
            //}

            //if (useFill)
            //    Renderer.StrokeAndFillRectangle(r.Left, r.Top, width, height, fFillColor);
            //else if (useStroke)
            //    Renderer.StrokeRectangle(r.Left, r.Top, width, height);
        }

        public void triangle(float x1, float y1, float x2, float y2, float x3, float y3)
        {
            Renderer.StrokeAndFillTriangle(x1, y1, x2, y2, x3, y3);
        }
        #endregion

        #region Curves
        // bezier
        public void bezier(int x1, int y1, int cx1, int cy1, int cx2, int cy2, int x2, int y2)
        {
            Renderer.Bezier(x1, y1, cx1, cy1, cx2, cy2, x2, y2);
        }

        // bezierDetail
        // bezierPoint
        // bezierTangent
        // curve
        // cureveDetail
        // curevePoint
        // curveTangent
        // curveTightness
        #endregion

        #region 3D Primitives
        // box
        // sphere
        // sphereDetail
        #endregion 3D Primitives

        #region Attributes
        // ellipseMode
        public void ellipseMode(int mode)
        {
            fEllipseMode = mode;
        }

        // noSmooth
        // rectMode
        public void rectMode(int mode)
        {
            fRectMode = mode;
        }
        // smooth
        #region smooth
        public void smooth()
        {
        }
        #endregion smooth

        #region strokeCap
        public void strokeCap(int cap)
        {
            fStrokeEndCap = cap;
            ResetPen();
        }
        #endregion

        public void strokeJoin(int join)
        {
            fStrokeJoin = join;
            ResetPen();
        }

        public void strokeWeight(int weight_)
        {
            fStrokeWeight = weight_;
            ResetPen();
        }

        public void strokeWeight(double weight_)
        {
            strokeWeight((int)Math.Round(weight_));
        }
        #endregion

        #region Vertex
        // beginShape
        public void beginShape()
        {
            beginShape(NOSHAPE);
        }

        public void beginShape(int mode)
        {
            inShape = true;
            fShapeMode = mode;
            fVertices.Clear();
        }

        // bezierVertex
        // curveVertex

        // endShape
        #region DrawElements
        void DrawPoints(PVertex[] pts)
        {
            foreach (PVertex pt in pts)
            {
                point(pt.x, pt.y);
            }
        }

        void DrawLines(PVertex[] pts)
        {
            // There must be at least two points to draw a line
            if (pts.Length < 2)
                return;

            for (int i=0; i<pts.Length; i+=2)
            {
                line(pts[i].x, pts[i].y, pts[i + 1].x, pts[i + 1].y);
            }
        }

        void DrawTriangles(PVertex[] pts)
        {
            // There must be at least two points to draw a line
            if (pts.Length < 3)
                return;

            for (int i = 0; i < pts.Length; i += 3)
            {
                triangle(pts[i].x, pts[i].y, pts[i + 1].x, pts[i + 1].y, pts[i + 2].x, pts[i + 2].y);
            }
        }

        void DrawTriangleStrip(PVertex[] pts)
        {
            // There must be at least two points to draw a line
            if (pts.Length < 3)
                return;

            for (int i = 0; i < pts.Length-2; i += 1)
            {
                triangle(pts[i].x, pts[i].y, pts[i + 1].x, pts[i + 1].y, pts[i + 2].x, pts[i + 2].y);
            }
        }

        void DrawTriangleFan(PVertex[] pts)
        {
            // There must be at least two points to draw a line
            if (pts.Length < 3)
                return;

            float x0 = pts[0].x;
            float y0 = pts[0].y;

            for (int i = 1; i < pts.Length - 1; i += 1)
            {
                triangle(x0, y0, pts[i].x, pts[i].y, pts[i + 1].x, pts[i + 1].y);
            }
        }

        void DrawQuads(PVertex[] pts)
        {
            for (int i = 0; i < pts.Length; i += 4)
            {
                quad(pts[i].x, pts[i].y, pts[i + 1].x, pts[i + 1].y, pts[i + 2].x, pts[i + 2].y, pts[i + 3].x, pts[i + 3].y);
            }
        }

        void DrawQuadStrip(PVertex[] pts)
        {
            for (int i = 0; i < pts.Length-2; i += 2)
            {
                quad(pts[i].x, pts[i].y, pts[i + 1].x, pts[i + 1].y, pts[i + 2].x, pts[i + 2].y, pts[i + 3].x, pts[i + 3].y);
            }
        }

        Point2I[] GetPoints(PVertex[] pts)
        {
            Point2I[] r = new Point2I[pts.Length];

            for (int i=0; i< pts.Length; i++)
            {
                r[i].x = (int)Math.Round(pts[i].x);
                r[i].y = (int)Math.Round(pts[i].y);
            }

            return r;
        }
        #endregion

        public void endShape()
        {
            endShape(NOSHAPE);
        }


        public void endShape(int mode)
        {
            // Turn the vertices into an array
            PVertex[] pts = fVertices.ToArray();

            // Draw whatever is appropriate for the vertices
            switch (mode)
            {
                case CLOSE:
                    Renderer.Polygon(GetPoints(pts));
                    break;

                case NOSHAPE:
                default:
                    switch (fShapeMode)
                    {
                        case LINES:
                            DrawLines(pts);
                            break;

                        case POINTS:
                            DrawPoints(pts);
                            break;

                        case TRIANGLES:
                            DrawTriangles(pts);
                            break;

                        case TRIANGLE_STRIP:
                            DrawTriangleStrip(pts);
                            break;

                        case TRIANGLE_FAN:
                            DrawTriangleFan(pts);
                            break;

                        case QUADS:
                            DrawQuads(pts);
                            break;

                        case QUAD_STRIP:
                            DrawQuadStrip(pts);
                            break;

                        default:
                            Renderer.PolyLine(GetPoints(pts));
                            break;
                    }
                    break;
            }

            fVertices.Clear();
            inShape = false; 
        }

        // texture
        // textureMode

        // vertex
        public void vertex(double x, double y)
        {
            vertex((int)Math.Round(x), (int)Math.Round(y));
        }

        public void vertex(double x, double y, double z)
        {
            vertex(x, y);
        }

        public void vertex(double x, double y, double u, double v)
        {
            vertex(x, y);
        }

        public void vertex(double x, double y, double z, double u, double v)
        {
            vertex(x, y);
        }


        public void vertex(int x, int y)
        {
            PVertex v = new PVertex();
            v.x = x;
            v.y = y;
            fVertices.Add(v);
        }

        public void vertex(int x, int y, int z)
        {
            vertex(x, y);
        }

        public void vertex(int x, int y, int u, int v)
        {
            vertex(x, y);
        }

        public void vertex(int x, int y, int z, int u, int v)
        {
            vertex(x, y);
        }
        #endregion Vertex

        #region Loading & Displaying
        // loadShape
        // shape
        // shapeMode
        #endregion Loading & Displaying

        #endregion Shape

        #region Transform
        //applyMatrix
        //    popMatrix
        //    printMatrix
        //    pushMatrix
        //    resetMatrix
        //    rotate
        //    rotateX
        //    rotateY
        //    rotateZ
        //    scale
        //    translate
        #endregion Transform

        #region Typography
        #region Loading & Displaying
        #endregion

        #region Attributes
        // textAlign
        // textLeading
        // textMode
        // textSize
        // textWidth
        #endregion Attributes

        #region Metrics
        // textAscent
        // textDescent
        #endregion

        #endregion
        #endregion Language Features
    }
}
