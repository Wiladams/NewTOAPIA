using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processing
{
    using TOAPI.Types;
    using TOAPI.User32;

    using NewTOAPIA;
    using NewTOAPIA.Drawing.GDI;
    using NewTOAPIA.UI;

    /// <summary>
    /// Base class for all sketches
    /// </summary>
    abstract public class PSketch : PLang, IObserver<MouseActivityArgs>, IObserver<PositionActivity>
    {
        Window win;

        protected PSketch()
            : base(null)
        {
            // Create a Window
            win = new Window("Sketch", 10, 10, 400, 400);

            // Show the window
            win.Show();

        }

        public virtual void Run()
        {
            win.Subscribe((IObserver<MouseActivityArgs>)this);
            win.Subscribe((IObserver<PositionActivity>)this);



            //PRenderer r = new PGDIRenderer(win.DeviceContextClientArea);
            PRenderer r = new PDirect2DRenderer(win.Handle);

            RECT cRect;
            User32.GetClientRect(win.Handle, out cRect);
            width = cRect.Width;
            height = cRect.Height;

            SetRenderer(r);
            
            // Call setup
            setup();

            win.StartTimer(1000 / 30);
            win.TimerEvent += OnTimer;

            // Run the window loop
            win.Run();
        }

        #region Language Overrides
        public override void redraw()
        {
            win.Invalidate();
            draw();
        }

        public override void onsize(int width_, int height_)
        {
            win.ResizeTo(width_, height_);
        }
        #endregion


        public virtual void OnTimer()
        {
            draw();
        }

        #region Observable
        public virtual void OnCompleted()
        {
        }

        public virtual void OnError(Exception exp)
        {
        }
        #endregion

        #region Observing Mouse Activity
        // Mouse sbuscription
        public virtual void OnNext(MouseActivityArgs e)
        {
            switch (e.ActivityType)
            {
                case MouseActivityType.MouseDown:
                    OnMouseDown(e);
                    break;

                case MouseActivityType.MouseMove:
                    OnMouseMove(e);
                    break;

                case MouseActivityType.MouseUp:
                    OnMouseUp(e);
                    break;

                case MouseActivityType.MouseEnter:
                    OnMouseEnter(e);
                    break;

                case MouseActivityType.MouseHover:
                    OnMouseHover(e);
                    break;

                case MouseActivityType.MouseLeave:
                    OnMouseLeave(e);
                    break;

                case MouseActivityType.MouseWheel:
                    OnMouseWheel(e);
                    break;
            }
        }

        int GetButton(MouseButtonActivity activity)
        {
            switch (activity)
            {
                case MouseButtonActivity.LeftButtonDown:
                case MouseButtonActivity.LeftButtonUp:
                    return LEFT;

                case MouseButtonActivity.MiddleButtonDown:
                case MouseButtonActivity.MiddleButtonUp:
                    return CENTER;

                case MouseButtonActivity.RightButtonDown:
                case MouseButtonActivity.RightButtonUp:
                    return RIGHT;

                default:
                    return 0;
            }
        }

        void OnMouseDown(MouseActivityArgs e)
        {
            isMousePressed = true;
            isMouseReleased = false;
            mouseButton = GetButton(e.ButtonActivity);

            mousePressed();
        }

        protected virtual void mousePressed()
        {
        }

        /// <summary>
        /// OnMouseEnter
        /// 
        /// This gets called whenever the pointing device enters our frame.
        /// We want to do interesting things here like change the cursor shape
        /// to be whatever we require.
        /// </summary>
        /// <param name="e"></param>
        void OnMouseEnter(MouseActivityArgs e)
        {
        }

        void OnMouseHover(MouseActivityArgs e)
        {
        }

        void OnMouseLeave(MouseActivityArgs e)
        {
        }

        void OnMouseMove(MouseActivityArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;

            mouseMoved();
        }

        protected virtual void mouseMoved()
        {
        }

        void OnMouseUp(MouseActivityArgs e)
        {
            isMousePressed = false;
            isMouseReleased = true;

            mouseReleased();
            mouseClicked();
        }

        protected virtual void mouseReleased()
        {
        }

        protected virtual void mouseClicked()
        {
        }

        void OnMouseWheel(MouseActivityArgs e)
        {
        }
        #endregion

        #region Observing Window Position Activity
        public virtual void OnNext(PositionActivity pActivity)
        {
            //Console.WriteLine("Window Activity {0}", pActivity);

            if ((pActivity.Position & PositionReport.Resized) == PositionReport.Resized)
                Renderer.Resize(pActivity.Width, pActivity.Height);

        }
        #endregion
    }
}
