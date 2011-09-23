using System;
using System.Drawing;

using NewTOAPIA.UI;
using NewTOAPIA.Drawing;


namespace MouseTest
{
    class MouseTestWindow : GraphicWindow
    {
        ActiveArea fArea;
        bool fDrawing;
        Point fLastDown;
        GDIPen blackPen;

        public MouseTestWindow()
            : base("MouseTestWindow", 20, 20, 320, 600)
        {
            // Use a layout handler so all our graphics line up vertically
            //this.LayoutHandler = new LinearLayout(this, 4, 4, Orientation.Vertical);

            blackPen = new GDIPen(PenType.Cosmetic, PenStyle.Solid, PenJoinStyle.Round, PenEndCap.Round, RGBColor.Black, 1, Guid.NewGuid());

            Rectangle bounds = ClientRectangle;
            fLastDown = new Point();

            fArea = new ActiveArea("area", 50,50, 200, 200);
            AddGraphic(fArea);
            fArea.Debug = true;

            fArea.MouseMoveEvent += new MouseEventHandler(this.MouseMovedActivity);
            fArea.MouseDownEvent += new MouseEventHandler(this.MouseDownActivity);
            fArea.MouseUpEvent += new MouseEventHandler(this.MouseUpActivity);
            
            BackgroundColor = RGBColor.LtGray;
        }

        private void MouseMovedActivity(object sender, MouseActivityArgs e)
        {
            if (fDrawing)
            {
                ClientAreaGraphPort.DrawLine(blackPen, fLastDown, new Point(e.X, e.Y));
            }
        }

        private void MouseDownActivity(object sender, MouseActivityArgs e)
        {
            if (fDrawing)
            {
                fLastDown.X = e.X;
                fLastDown.Y = e.Y;

                GraphPort.SetPixel(e.X, e.Y, Color.White);
            }
            //Console.WriteLine("MouseTestWindow.MouseDownActivity: {0}", e.ToString());
        }

        private void MouseUpActivity(object sender, MouseActivityArgs e)
        {
            if (fDrawing)
            {
                fDrawing = false;

            }
            else
            {
                fDrawing = true;
                //GDI.MoveToEx(GraphPort.DeviceContext, e.X, e.Y, IntPtr.Zero);
            }
            //Console.WriteLine("MouseTestWindow.MouseUpActivity: {0}", e.ToString());
        }

        public override void OnPaint(DrawEvent dea)
        {
            base.OnPaint(dea);

            IGraphPort graphPort = dea.GraphPort;

            //Console.WriteLine("GUIStyleWindow.OnPaint");
        }
    }

}
