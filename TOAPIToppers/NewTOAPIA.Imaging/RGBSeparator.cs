using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Imaging
{
    public class RGBSeparator
    {
        void Separate(PixelArray<BGRb> pixMap, PixelArray<Redb> redPlane, PixelArray<Greenb> greenPlane, PixelArray<Blueb> bluePlane)
        {
            for (int row = 0; row < pixMap.Height; row++)
            {
                for (int column = 0; column < pixMap.Width; column++)
                {
                    BGRb srcPixel = pixMap.RetrievePixel(column, row);

                    redPlane.AssignPixel(column, row, new Redb(srcPixel.Red));
                    greenPlane.AssignPixel(column, row, new Greenb(srcPixel.Green));
                    bluePlane.AssignPixel(column, row, new Blueb(srcPixel.Blue));
                }
            }
        }
    }
}
