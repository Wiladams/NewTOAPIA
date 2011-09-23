using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sketch1
{
    using Processing;

    public class Chapter10 :PSketch
    {
        const int Y_AXIS = 1;
        const int X_AXIS = 2;

        public override void setup()
        {
            Fig10_17();
            //Fig10_18();
        }

        #region Figure 10-17
        void Fig10_17()
        {

            size(400, 400);

            // create some gradients
            // background
            color b1 = color(190, 190, 190);
            color b2 = color(20, 20, 20);
            setGradient(0, 0, width, height, b1, b2, Y_AXIS);

            // center squares
            color c1 = color(255, 120, 0);
            color c2 = color(10, 45, 255);
            color c3 = color(10, 255, 15);
            color c4 = color(125, 2, 140);
            color c5 = color(255, 255, 0);
            color c6 = color(25, 255, 200);

            setGradient(50, 50, 150, 150, c1, c2, Y_AXIS);
            setGradient(200, 50, 150, 150, c3, c4, X_AXIS);
            setGradient(50, 200, 150, 150, c2, c5, X_AXIS);
            setGradient(200, 200, 150, 150, c4, c6, Y_AXIS);
        }

        void setGradient(int x, int y, float w, float h, color c1, color c2, int axis)
        {
            float deltaR = red(c2) - red(c1);
            float deltaG = green(c2) - green(c1);
            float deltaB = blue(c2) - blue(c1);

            // choose axis
            if (axis == Y_AXIS)
            {
                for (int i = x; i <= (x + w); i++)
                {
                    // row
                    for (int j = y; j <= (y + h); j++)
                    {
                        color c = color((int)(red(c1) + (j - y) * (deltaR / h)),
                            (int)(green(c1) + (j - y) * (deltaG / h)),
                            (int)(blue(c1) + (j - y) * (deltaB / h)));
                        
                        set(i, j, c);
                    }
                }
            }
            else if (axis == X_AXIS)
            {
                // column
                for (int i = y; i <= (y + h); i++)
                {
                    // row
                    for (int j = x; j <= (x + w); j++)
                    {
                        color c = color((int)(red(c1) + (j - x) * (deltaR / h)),
    (int)(green(c1) + (j - x) * (deltaG / h)),
    (int)(blue(c1) + (j - x) * (deltaB / h)));
                        set(j, i, c);
                    }
                }
            }
        }
        #endregion

        #region Figure 10-18
        void Fig10_18()
        {
            size(400, 400);
            background(0);
            smooth();

            // create a simple table of gradients
            int columns = 4;
            int radius = (width / columns) / 2;

            // create some gradients
            for (int i = radius; i < width; i += radius * 2)
            {
                for (int j = radius; j < height; j += radius * 2)
                {
                    createGradient(i, j, radius,
                        color((int)random(255), (int)random(255), (int)random(255)),
                        color((int)random(255), (int)random(255), (int)random(255)));
                }
            }
        }

        void createGradient(float x, float y, float radius, color c1, color c2)
        {
            float px = 0;
            float py = 0;
            float angle = 0;

            // calculate differences between color components
            float deltaR = red(c2) - red(c1);
            float deltaG = green(c2) - green(c1);
            float deltaB = blue(c2) - blue(c1);

            float gapFiller = 8;

            for (int i = 0; i < radius; i++)
            {
                for (float j = 0; j < 360; j += 1.0f / gapFiller)
                {
                    px = x + cos(radians(angle)) * i;
                    py = y + sin(radians(angle)) * i;
                    angle += 1.0f / gapFiller;

                    color c = color(
                        ((int)red(c1) + (i) * (deltaR / radius)),
                        ((int)green(c1) + (i) * (deltaG / radius)),
                        ((int)blue(c1) + (i) * (deltaB / radius)));

                    set((int)px, (int)py, c);
                }
            }

            // adds smooth edges
            // hack anti-aliasing
            noFill();
            strokeWeight(3);
            ellipse(x, y, radius * 2, radius * 2);
        }
        #endregion
    }
}
