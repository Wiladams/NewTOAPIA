

namespace PixTour
{
    using System;
    using System.Windows.Forms;

    using NewTOAPIA.Drawing.GDI;
    using NewTOAPIA.Graphics;

    using Autometaii;
    
    public partial class Form1 : Form
    {
        GDIRenderer fChannel;
        GDIContext fDeviceContext;

        BezierTest bezierer;
        TextTest aTextTest;

        int fDemoCounter;

        public Form1()
        {
            InitializeComponent();


            // Get the right device context
            fDeviceContext = GDIContext.CreateForWindowClientArea(this.Handle);   // This is fairly fast for the window
            //fDeviceContext = GDIContext.CreateForDesktopBackground();   // This one is the desktop background and is fast
            //fDeviceContext = GDIContext.CreateForDefaultDisplay();    // This one is way slow on Vista

            fChannel = new GDIRenderer(fDeviceContext);

            fDemoCounter = 0;
            //this.Text = "PixTour";

            var clientSize = ClientRectangle.Size;
            bezierer = new BezierTest(new Size2I(clientSize.Width, clientSize.Height));
            aTextTest = new TextTest(new Size2I(clientSize.Width, clientSize.Height));

            PrintDeviceContext(fDeviceContext);
        }

        public virtual void PixBlt(int x, int y, GDIDIBSection pixmap)
        {
            TOAPI.Types.BITMAPINFO bmi = new TOAPI.Types.BITMAPINFO();
            bmi.Init();

            bmi.bmiHeader.biBitCount = (ushort)pixmap.BitsPerPixel;
            bmi.bmiHeader.biWidth = pixmap.Width;
            bmi.bmiHeader.biHeight = pixmap.Height;
            bmi.bmiHeader.biSizeImage = (uint)(Math.Abs(pixmap.BytesPerRow) * Math.Abs(pixmap.Height));
            bmi.bmiHeader.biPlanes = 1;
            bmi.bmiHeader.biClrImportant = 0;
            bmi.bmiHeader.biClrUsed = 0;
            bmi.bmiHeader.biCompression = 0;

            //int result = GDI32.StretchDIBits(fDeviceContext, x, y, pixmap.Width, pixmap.Height, 
            //    0, 0, pixmap.Width, pixmap.Height, 
            //    pixmap.Pixels, ref bmi, 
            //    GDI32.DIB_RGB_COLORS, (uint)TernaryRasterOps.SRCCOPY);

            fDeviceContext.PixelBlt(new RectangleI(0, 0, pixmap.Width, pixmap.Height),
                new RectangleI(x, y, pixmap.Width, pixmap.Height),
                pixmap.Pixels, pixmap.BitCount);
        }

        void PrintDeviceContext(GDIContext dc)
        {

            DeviceTechnology tech = dc.TypeOfDevice;
            GDIRasterCapabilities rastercaps = dc.RasterCapabilities;
            GDITextCapabilities textcaps = dc.TextCapabilities;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fChannel.UseDefaultBrush();
            fChannel.UseDefaultPen();
            fChannel.Flush();
            int width = fDeviceContext.SizeInPixels.Width;
            int height = fDeviceContext.SizeInPixels.Height;

            Size2I cSize = new Size2I(ClientRectangle.Size.Width, ClientRectangle.Size.Height);
            switch (fDemoCounter)
            {
                case 0:
                    LineDemo1 aLineDemo = new LineDemo1(cSize);
                    aLineDemo.ReceiveCommand(new Command_Render(fChannel));
                    break;
                case 1:
                    RectangleTest aRectTest = new RectangleTest(cSize);
                    aRectTest.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 2:
                    aTextTest.Dimension = cSize;
                    aTextTest.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 3:
                    SineWave siner = new SineWave(cSize, 100);
                    siner.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 4:
                    PolygonTest polygoner = new PolygonTest(cSize);
                    polygoner.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 5:
                    bezierer.Dimension = cSize;
                    bezierer.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 6:
                    GraphTest grapher = new GraphTest(cSize);
                    grapher.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 7:
                    RandomRect randrect = new RandomRect(cSize);
                    randrect.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 8:
                    RegionTest regionTest = new RegionTest(cSize);
                    regionTest.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 9:
                    PathTest pathTest = new PathTest(cSize);
                    pathTest.ReceiveCommand(new Command_Render(fChannel));
                    break;
            }

            ((Button)sender).Text = fDemoCounter.ToString();

            fDemoCounter++;
            if (fDemoCounter == 10)
            {
                fDemoCounter = 0;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Just clear the screen
            this.Invalidate();
        }

    }
}
