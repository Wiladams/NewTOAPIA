using System;

using TOAPI.Types;

public struct LineG
{
    public System.Drawing.Point StartPoint;
    public System.Drawing.Point EndPoint;

    public LineG(System.Drawing.Point point1, System.Drawing.Point point2)
    {
        StartPoint = point1;
        EndPoint = point2;
    }
}

