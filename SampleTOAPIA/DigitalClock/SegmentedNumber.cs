using System;

using TOAPI.Types;
using NewTOAPIA.Drawing;
using NewTOAPIA.Graphics;

public class SegmentedNumber
{
	static int[,] fSevenSegment;
	static Point2I[][] ptSegment;

	static SegmentedNumber()
	{
		fSevenSegment = new int[10, 7]{
			{1,1,1,0,1,1,1},
			{0,0,1,0,0,1,0},
			{1,0,1,1,1,0,1},
			{1,0,1,1,0,1,1},
			{0,1,1,1,0,1,0},
			{1,1,0,1,0,1,1},
			{1,1,0,1,1,1,1},
			{1,0,1,0,0,1,0},
			{1,1,1,1,1,1,1},
			{1,1,1,1,0,1,1}};

        ptSegment = new Point2I[7][];
        ptSegment[0] = new Point2I[] { new Point2I(7, 6), new Point2I(11, 2), new Point2I(31, 2), new Point2I(35, 6), new Point2I(31, 10), new Point2I(11, 10) };
        ptSegment[1] = new Point2I[] { new Point2I(6, 7), new Point2I(10, 11), new Point2I(10, 31), new Point2I(6, 35), new Point2I(2, 31), new Point2I(2, 11) };
        ptSegment[2] = new Point2I[] { new Point2I(36, 7), new Point2I(40, 11), new Point2I(40, 31), new Point2I(36, 35), new Point2I(32, 31), new Point2I(32, 11) };
        ptSegment[3] = new Point2I[] { new Point2I(7, 36), new Point2I(11, 32), new Point2I(31, 32), new Point2I(35, 36), new Point2I(31, 40), new Point2I(11, 40) };
        ptSegment[4] = new Point2I[] { new Point2I(6, 37), new Point2I(10, 41), new Point2I(10, 61), new Point2I(6, 65), new Point2I(2, 61), new Point2I(2, 41) };
        ptSegment[5] = new Point2I[] { new Point2I(36, 37), new Point2I(40, 41), new Point2I(40, 61), new Point2I(36, 65), new Point2I(32, 61), new Point2I(32, 41) };
        ptSegment[6] = new Point2I[] { new Point2I(7, 66), new Point2I(11, 62), new Point2I(31, 62), new Point2I(35, 66), new Point2I(31, 70), new Point2I(11, 70) };

	}

    public static void DisplayDigit(IGraphPort graphPort, int iNumber)
    {
        int iSeg;
        
        for (iSeg = 0; iSeg < 7; iSeg++)
            if (fSevenSegment[iNumber, iSeg] > 0)
            {
                graphPort.Polygon(ptSegment[iSeg]);
            }
    }
}
