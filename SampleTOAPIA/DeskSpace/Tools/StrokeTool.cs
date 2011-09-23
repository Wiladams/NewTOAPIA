using System;
using System.Collections.Generic;

using TOAPI.Types;
using NewTOAPIA.UI;

namespace HamSketch.Tools
{
    public delegate void OnStrokeFinishedDelegate(Point[] points);

    public class StrokeTool : BaseTool
    {
        public event OnStrokeFinishedDelegate StrokeFinishedEvent;

        List<Point> fPointList;
        bool fLeftButtonDown;
        bool fRightButtonDown;
        protected int xMouse;
        protected int yMouse;


        public StrokeTool()
        {
            fLeftButtonDown = false;
            fRightButtonDown = false;

            fPointList = new List<Point>();
        }

        public bool IsTracking
        {
            get { return fLeftButtonDown || fRightButtonDown; }
        }

        public virtual void OnStrokeFinished(Point [] points)
        {
            if (null != StrokeFinishedEvent)
                StrokeFinishedEvent(points);
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            fPointList.Clear(); // Clear out the point list to start over

            switch (e.Button)
            {
                case MouseButtons.Left:
                    //if (!fRightButtonDown)
                    //    this.Capture = true;

                    xMouse = e.X;
                    yMouse = e.Y;
                    fLeftButtonDown = true;
                    fPointList.Add(new Point(xMouse, yMouse));
                    break;
            }

            base.OnMouseDown(e);
        }


        public override void OnMouseMove(MouseEventArgs e)
        {
            if (!fLeftButtonDown && !fRightButtonDown)
                return;

            // Make sure we stay within the bounds of our graph port
            if (e.X < 0)
                xMouse = 0;
            else
                xMouse = e.X;

            if (e.Y < 0)
                yMouse = 0;
            else
                yMouse = e.Y;

            // Add the current point to the list of points
            fPointList.Add(new Point(xMouse, yMouse));

            base.OnMouseMove(e);
        }


        public override void OnMouseUp(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    //if (fLeftButtonDown)
                    //    this.Capture = false;

                    fLeftButtonDown = false;

                    OnStrokeFinished(PointListToArray(fPointList));

                    break;

            }

            base.OnMouseUp(e);
        }

        Point[] PointListToArray(List<Point> pList)
        {
            Point[] points = new Point[pList.Count];
            for (int i = 0; i < pList.Count; i++)
            {
                points[i] = pList[i];
            }
            return points;
        }

    }
}
