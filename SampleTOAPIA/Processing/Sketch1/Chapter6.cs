using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sketch1
{
    using Processing;

    public class Chapter6 : PSketch
    {
        public override void setup()
        {
            //Fig6_7();
            //Fig6_16();
            //Fig6_17();
            Fig6_23();
            //Fig6_27();
            //Fig6_40();
            //Fig6_43();
        }

        public void Fig6_7()
        {
            size(300, 300);
            background(0);
            int totalPts = 300;
            float steps = totalPts + 1;
            stroke(255);
            float rand = 0;

            for (int i=1; i<steps; i++)
            {
                point((width / steps) * i, (height / 2)+random(-rand,rand));
                rand += 0.2f;
            }
        }

        public void Fig6_16()
        {
            size(300, 300);
            background(0);
            float cellWidth = width / 30.0f;

            // find ratio of value range(255) to width
            float val = cellWidth * (255.0f / width);

            // vertical lines
            for (float i = cellWidth, v = 255; i<width; i+= cellWidth, v-=val)
            {
                stroke((int)v);
                for (int j=0; j<height; j++)
                {
                    point(i,j);
                }
            }
        }

        public void Fig6_17()
        {
            size(500, 200);
            background(0);
            float cellWidth = width / 50.0f;

            // find ratio of value range(255) to width
            float valw = cellWidth * (255.0f / width);

            // vertical lines
            for (float i = cellWidth, v = 255; i < width; i += cellWidth, v -= valw)
            {
                stroke((int)v);
                for (int j = 0; j < height; j++)
                {
                    point(i, j);
                }
            }

            float cellHeight = height / 50.0f;

            // find ratio of value range(255) to height
            float valh = cellHeight * (255.0f / height);

            // horizontal lines
            for (float i = cellHeight, v = 255; i < height; i += cellHeight, v -= valh)
            {
                stroke((int)v);

                for (int j = 0; j < width; j++)
                {
                    point(j, i);
                }
            }
        }

        public void Fig6_23()
        {
            size(500, 300);
            background(255);
            stroke(0);
            int step = height / 10;
            for (int i = step; i < height; i += step)
            {
                strokeWeight(i * 0.1f);
                line(20, i, width - 20, i);
            }
        }

        public void Fig6_27()
        {
            size(700, 500);
            background(0);
            drawTable_6_27();
        }

        void drawTable_6_27()
        {
            stroke(255);
            int[] caps = { ROUND, PROJECT, SQUARE };
            strokeWeight(0.1f);
            int cols = 20;
            int rows = 20;
            int xPadding = 100;
            int yPadding = 100;
            float w = (width - xPadding) / cols;
            float h = (height - yPadding) / rows;
            float colSpan = (width - cols * w) / (cols + 1);
            float rowSpan = (height - rows * h) / (rows + 1);
            float x;
            float y = rowSpan;

            for (int i = 0; i < rows; i++)
            {
                x = colSpan;
                for (int j = 0, k = 0; j < cols; j++)
                {
                    //strokeCap(caps[k++]);
                    if (k > 2) { k = 0; }

                    line(x + random(-4, 4), y + random(-4, 4), x + w + random(-4, 4), y + random(-4, 4));
                    line(x + w+random(-4, 4), y + random(-4, 4), x + w + random(-4, 4), y + h+random(-4, 4));
                    line(x + random(-4, 4), y + h+random(-4, 4), x + w + random(-4, 4), y + h + random(-4, 4));
                    line(x + random(-4, 4), y + h + random(-4, 4), x + random(-4, 4), y + random(-4, 4));

                    x += w + colSpan;
                }
                y += h + rowSpan;
            }
        }

        #region Figure 6-40
        void Fig6_40()
        {
            size(400, 400);
            background(0);
            smooth();
            noFill();
            makePoly6_40(width / 2, height / 2, 9, 150, 255, 8, MITER);
        }

        void makePoly6_40(int x, int y, int points, float radius, int strokeCol, float strokeWt, int strokeJn)
        {
            float px = 0;
            float py = 0;
            float angle = 0;
            stroke(strokeCol);
            strokeJoin(strokeJn);
            strokeWeight(strokeWt);
            
            beginShape();
            for (int i=0; i<points; i++)
            {
                px = x+cos(radians(angle))*radius;
                py = y+sin(radians(angle))*radius;
                vertex(px, py);
                angle+=360/points;
            }
            endShape(CLOSE);
        }
        #endregion Figure 6-40

        #region Figure 6-43
        void Fig6_43()
        {
            size(400, 400);
            background(0);
            smooth();
            float radius = 0;
            float radius2 = 0;
            float x = 0;
            float y = 0;
            float ang = 0;

            while (x < width * 1.5)
            {
                y = height / 2 + sin(radians(ang)) * radius;
                x = width / 2 + cos(radians(ang)) * radius;
                makePoly6_43(x, y, 8, radius2, (int)(radius2 * 30), 6, BEVEL);

                // you can change these values
                ang += 1.1f;
                radius += 0.059f;
                radius2 += 0.0016f;
            }
        }

        void makePoly6_43(float x, float y, int points, float radius, int strokeCol, float strokeWt, int strokeJn)
        {
            float px = 0;
            float py = 0;
            float angle = 0;
            stroke(strokeCol);
            noFill();
            strokeJoin(strokeJn);
            strokeWeight(strokeWt);
            
            beginShape();
            for (int i = 0; i < points; i++)
            {
                px = x + cos(radians(angle)) * radius;
                py = y + sin(radians(angle)) * radius;
                vertex(px, py);
                angle += 360 / points;
            }
            endShape(CLOSE);
        }
        #endregion Figure 6-43
    }
}
