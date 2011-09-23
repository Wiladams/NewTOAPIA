using System;

using TOAPI.Types;

public struct LineG
{
    public Point StartPoint;
    public Point EndPoint;

    public LineG(Point point1, Point point2)
    {
        StartPoint = point1;
        EndPoint = point2;
    }
}

