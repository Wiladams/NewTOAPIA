using System;

using TOAPI.Types;

public class PolygonG
{
    Point[] fPoints;

    public PolygonG(Point[] points)
    {
        fPoints = points;
    }

    public Point[] Points
    {
        get { return fPoints; }
    }
}

