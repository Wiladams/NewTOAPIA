using System;
using System.Runtime.InteropServices;
using System.Xml;
using System.Drawing;
using System.Drawing.Imaging;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Graphics;
using NewTOAPIA.UI;

namespace PLC
{
    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class GraphicImage : Graphic
    {
        IPixelArray fPixmap;
        double fOpacity;
        bool fUseOpacity;

        #region Constructor
        public GraphicImage(string name, IPixelArray pbuff)
            : this(name, 0, 0, pbuff.Width, pbuff.Height, pbuff)
        {
        }

        public GraphicImage(string name, int x, int y, int width, int height, IPixelArray pbuff)
            :base(name, x,y,width,height)
        {
            fPixmap = pbuff;
            fOpacity = 1.0;
            fUseOpacity = false;

            Debug = true;
        }
        #endregion

        #region Properties
        public IPixelArray Image
        {
            get { return fPixmap; }
        }

        public double Opacity
        {
            get { return fOpacity; }
            set { fOpacity = value; }
        }
        #endregion

        public override void DrawSelf(DrawEvent devent)
        {
            if (null != Image)
            {
                if (!fUseOpacity)
                    devent.GraphPort.PixBlt(Image, Origin.X, Origin.Y);
                else
                    devent.GraphPort.PixBlt(Image, Origin.X, Origin.Y);
            }
        }

        #region Static Construction Helpers
        public static GraphicImage CreateFromFile(string filename, int x, int y, int width, int height)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(filename);
            IPixelArray pmap = PixelBufferHelper.CreateFromImage(image);

            GraphicImage gImage = new GraphicImage(filename, x, y, width, height, pmap);
            return gImage;
        }

        public static GraphicImage CreateFromResource(string resourceName, int x, int y, int width, int height)
        {
            System.IO.Stream stream = AppDomain.CurrentDomain.GetType().Assembly.GetManifestResourceStream(resourceName);

            GraphicImage gImage;

            if (null != stream)
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                IPixelArray pmap = PixelBufferHelper.CreateFromImage(image);

                gImage = new GraphicImage(resourceName, x, y, width, height, pmap);
            }
            else
            {
                gImage = new GraphicImage(resourceName, x, y, width, height, null);
            }

            return gImage;
        }

        public static GraphicImage CreateFromBitmap(Bitmap bm, string name, int x, int y, int width, int height)
        {
            IPixelArray pbuff = PixelBufferHelper.CreatePixelBufferFromBitmap(bm);
            GraphicImage gimage = new GraphicImage(name, x, y, width, height, pbuff);

            return gimage;
        }
        #endregion



        // XML Serialization

        //public override void WriteXmlAttributes(XmlWriter writer)
        //{
        //    base.WriteXmlAttributes(writer);

        //    writer.WriteAttributeString("source", fFileName);
        //}

    }
}