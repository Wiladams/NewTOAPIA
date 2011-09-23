using System;
using System.Collections.Generic;

using TOAPI.Types;
using NewTOAPIA.UI;

namespace HamSketch.Tools
{
    public delegate void OnPointArrayToolFinished(Point[] points);

    public class Digitizer : BaseTool
    {
        public event OnPointArrayToolFinished PointsAvailableEvent;

        List<Point> fPointList;
        int fMaxPoints;

        public Digitizer(int maxPoints)
        {
            fMaxPoints = maxPoints;
            fPointList = new List<Point>();
        }


        public virtual void OnFinished(Point[] points)
        {
            if (null != PointsAvailableEvent)
                PointsAvailableEvent(points);

            fPointList.Clear();
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

        public override void OnMouseUp(MouseEventArgs e)
        {

            switch (e.Button)
            {
                case MouseButtons.Left:
                    fPointList.Add(new Point(e.X, e.Y));
                    break;

            }

            if (fPointList.Count >= fMaxPoints)
            {
                OnFinished(PointListToArray(fPointList));
            }

            base.OnMouseUp(e);
        }
    }
}
