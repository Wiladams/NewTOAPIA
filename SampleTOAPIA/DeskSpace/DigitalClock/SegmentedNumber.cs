using System;

using TOAPI.Types;
using NewTOAPIA.Drawing;

public class SegmentedNumber
{
	static int[,] fSevenSegment;
	static Point[][] ptSegment;

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

        ptSegment = new Point[7][];
        ptSegment[0] = new Point[] { new Point(7, 6), new Point(11, 2), new Point(31, 2), new Point(35, 6), new Point(31, 10), new Point(11, 10) };
        ptSegment[1] = new Point[] { new Point(6, 7), new Point(10, 11), new Point(10, 31), new Point(6, 35), new Point(2, 31), new Point(2, 11) };
        ptSegment[2] = new Point[] { new Point(36, 7), new Point(40, 11), new Point(40, 31), new Point(36, 35), new Point(32, 31), new Point(32, 11) };
        ptSegment[3] = new Point[] { new Point(7, 36), new Point(11, 32), new Point(31, 32), new Point(35, 36), new Point(31, 40), new Point(11, 40) };
        ptSegment[4] = new Point[] { new Point(6, 37), new Point(10, 41), new Point(10, 61), new Point(6, 65), new Point(2, 61), new Point(2, 41) };
        ptSegment[5] = new Point[] { new Point(36, 37), new Point(40, 41), new Point(40, 61), new Point(36, 65), new Point(32, 61), new Point(32, 41) };
        ptSegment[6] = new Point[] { new Point(7, 66), new Point(11, 62), new Point(31, 62), new Point(35, 66), new Point(31, 70), new Point(11, 70) };

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
