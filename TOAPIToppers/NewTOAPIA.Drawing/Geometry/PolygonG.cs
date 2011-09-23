using System;

public class PolygonG
{
    System.Drawing.Point[] fPoints;

    public PolygonG(System.Drawing.Point[] points)
    {
        fPoints = points;
    }

    public System.Drawing.Point[] Points
    {
        get { return fPoints; }
    }
}

