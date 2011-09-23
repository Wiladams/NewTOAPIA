

namespace NewTOAPIA.Drawing
{
    using System;
    using System.Collections.Generic;

    using NewTOAPIA.Graphics;

    
    public interface IStyleHandler
    {
        bool is_solid(int style);
        RGBA_Bytes color(int style);
        void generate_span(RGBA_Bytes[] span, int spanIndex, int x, int y, int len, int style);
    };

    public abstract class RendererBase
    {
        const int cover_full = 255;
        protected IImage m_DestImage;
        protected gsv_text TextPath;
        protected conv_stroke StrockedText;
        protected Stack<Affine> m_AffineTransformStack = new Stack<Affine>();
        protected rasterizer_scanline_aa m_Rasterizer;

        public RendererBase()
        {
            TextPath = new gsv_text();
            StrockedText = new conv_stroke(TextPath);
            m_AffineTransformStack.Push(Affine.NewIdentity());
        }

        public RendererBase(IImage DestImage, rasterizer_scanline_aa Rasterizer)
            : this()
        {
            Initialize(DestImage, Rasterizer);
        }

        public void Initialize(IImage DestImage, rasterizer_scanline_aa Rasterizer)
        {
            m_DestImage = DestImage;
            m_Rasterizer = Rasterizer;
        }

        public Affine PopTransform()
        {
            if (m_AffineTransformStack.Count == 1)
            {
                throw new System.Exception("You cannot remove the last transform from the stack.");
            }

            return m_AffineTransformStack.Pop();
        }

        public void PushTransform()
        {
            if (m_AffineTransformStack.Count > 1000)
            {
                throw new System.Exception("You seem to be leaking transforms.  You should be poping some of them at some point.");
            }

            m_AffineTransformStack.Push(m_AffineTransformStack.Peek());
        }

        public Affine GetTransform()
        {
            return m_AffineTransformStack.Peek();
        }

        public void SetTransform(Affine value)
        {
            m_AffineTransformStack.Pop();
            m_AffineTransformStack.Push(value);
        }

        public rasterizer_scanline_aa Rasterizer
        {
            get { return m_Rasterizer; }
        }

        public abstract IScanlineCache ScanlineCache
        {
            get;
            set;
        }

        public IImage DestImage
        {
            get
            {
                return m_DestImage;
            }
        }

        public abstract void Render(IVertexSource vertexSource, int pathIndexToRender, RGBA_Bytes colorBytes);
        public abstract void Render(IImage imageSource,
            double x, double y,
            double angleDegrees,
            double scaleX, double ScaleY,
            RGBA_Bytes color,
            bool doDrawing,
            bool oneMinusSourceAlphaOne);

        public void Render(IVertexSource vertexSource, RGBA_Bytes[] colorArray, int[] pathIdArray, int numPaths)
        {
            for (int i = 0; i < numPaths; i++)
            {
                Render(vertexSource, pathIdArray[i], colorArray[i]);
            }
        }

        public void Render(IVertexSource vertexSource, RGBA_Bytes color)
        {
            Render(vertexSource, 0, color);
        }

        public abstract void Clear(IColorType color);

        public void DrawString(string Text, double x, double y)
        {
            TextPath.SetFontSize(10);
            TextPath.start_point(x, y);
            TextPath.text(Text);
            Render(StrockedText, new RGBA_Bytes(0, 0, 0, 255));
        }

        public void Line(Vector2D Start, Vector2D End, RGBA_Bytes color)
        {
            Line(Start.x, Start.y, End.x, End.y, color);
        }

        public void Line(double x1, double y1, double x2, double y2, RGBA_Bytes color)
        {
            PathStorage m_LinesToDraw = new PathStorage();
            m_LinesToDraw.Clear();
            m_LinesToDraw.MoveTo(x1, y1);
            m_LinesToDraw.LineTo(x2, y2);
            conv_stroke StrockedLineToDraw = new conv_stroke(m_LinesToDraw);
            Render(StrockedLineToDraw, color);
        }

        public abstract void SetClippingRect(RectangleD rect_d);
    }
}
