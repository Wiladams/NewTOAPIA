

namespace PixTour
{
    using System;
    using System.Windows.Forms;

    using NewTOAPIA.Drawing;
    using NewTOAPIA.Drawing.GDI;

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

            bezierer = new BezierTest(ClientRectangle.Size);
            aTextTest = new TextTest(ClientRectangle.Size);

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

            fDeviceContext.PixelBlt(new System.Drawing.Rectangle(0, 0, pixmap.Width, pixmap.Height),
                new System.Drawing.Rectangle(x, y, pixmap.Width, pixmap.Height),
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

            switch (fDemoCounter)
            {
                case 0:
                    LineDemo1 aLineDemo = new LineDemo1(ClientRectangle.Size);
                    aLineDemo.ReceiveCommand(new Command_Render(fChannel));
                    break;
                case 1:
                    RectangleTest aRectTest = new RectangleTest(ClientRectangle.Size);
                    aRectTest.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 2:
                    aTextTest.Dimension = ClientRectangle.Size;
                    aTextTest.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 3:
                    SineWave siner = new SineWave(ClientRectangle.Size, 100);
                    siner.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 4:
                    PolygonTest polygoner = new PolygonTest(ClientRectangle.Size);
                    polygoner.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 5:
                    bezierer.Dimension = ClientRectangle.Size;
                    bezierer.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 6:
                    GraphTest grapher = new GraphTest(ClientRectangle.Size);
                    grapher.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 7:
                    RandomRect randrect = new RandomRect(ClientRectangle.Size);
                    randrect.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 8:
                    RegionTest regionTest = new RegionTest(ClientRectangle.Size);
                    regionTest.ReceiveCommand(new Command_Render(fChannel));
                    break;

                case 9:
                    PathTest pathTest = new PathTest(ClientRectangle.Size);
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
