using System;

using TOAPI.Types;

public class Filter_Water
{
    public static bool Filter(PixelBuffer b, short nWave, bool bSmoothing)
    {
        int nWidth = b.Width;
        int nHeight = b.Height;

        PointDouble[,] fp = new PointDouble[nWidth, nHeight];
        Point[,] pt = new Point[nWidth, nHeight];

        Point mid = new Point(nWidth / 2, nHeight / 2);

        double newX, newY;
        double xo, yo;

        for (int x = 0; x < nWidth; ++x)
            for (int y = 0; y < nHeight; ++y)
            {
                xo = ((double)nWave * Math.Sin(2.0 * 3.1415 * (float)y / 128.0));
                yo = ((double)nWave * Math.Cos(2.0 * 3.1415 * (float)x / 128.0));

                newX = (x + xo);
                newY = (y + yo);

                if (newX > 0 && newX < nWidth)
                {
                    fp[x, y].X = newX;
                    pt[x, y].x = (int)newX;
                }
                else
                {
                    fp[x, y].X = 0.0;
                    pt[x, y].x = 0;
                }


                if (newY > 0 && newY < nHeight)
                {
                    fp[x, y].Y = newY;
                    pt[x, y].y = (int)newY;
                }
                else
                {
                    fp[x, y].Y = 0.0;
                    pt[x, y].y = 0;
                }
            }

        if (bSmoothing)
            OffsetFilter.FilterAbs(b, pt);
        else
            OffsetFilter.FilterAbs(b, pt);
            //OffsetFilterAntiAlias(b, fp);

        return true;
    }
}

