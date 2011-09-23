using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sketch1
{
    using Processing;

    public class DocRefSamples : PSketch
    {
        public override void setup()
        {            

            size(200, 200);
            //background(220);

            Doc_arc();
            //Doc_beginShape();
            //Doc_endShape();
            //Doc_bezier();
            //Doc_colorMode();
            //Doc_ellipse();
            //Doc_lerp();
            //Doc_line();
            //Doc_point();
            //Doc_quad();
            //Doc_rect();
            //Doc_rectMode();
            //Doc_sqrt();
            //Doc_strokeCap();
            //Doc_strokeJoin();
            //Doc_strokeWeight();
            //Doc_triangle();
        }

        void Doc_arc()
        {
            arc(50, 55, 50, 50, 0, PI / 2);
            noFill();
            arc(50, 55, 60, 60, PI / 2, PI);
            arc(50, 55, 70, 70, PI, TWO_PI - PI / 2);
            arc(50, 55, 80, 80, TWO_PI - PI / 2, TWO_PI);
        }

        void Doc_beginShape()
        {
            // sample 1
            //beginShape();
            //vertex(30, 20);
            //vertex(85, 20);
            //vertex(85, 75);
            //vertex(30, 75);
            //endShape(CLOSE);

            // sample 2
            //beginShape(POINTS);
            //vertex(30, 20);
            //vertex(85, 20);
            //vertex(85, 75);
            //vertex(30, 75);
            //endShape();

            // sample 3
            //beginShape(LINES);
            //vertex(30, 20);
            //vertex(85, 20);
            //vertex(85, 75);
            //vertex(30, 75);
            //endShape();

            // sample 4
            //noFill();
            //beginShape();
            //vertex(30, 20);
            //vertex(85, 20);
            //vertex(85, 75);
            //vertex(30, 75);
            //endShape();

            // sample 5
            //noFill();
            //beginShape();
            //vertex(30, 20);
            //vertex(85, 20);
            //vertex(85, 75);
            //vertex(30, 75);
            //endShape(CLOSE);

            // sample 6
            //beginShape(TRIANGLES);
            //vertex(30, 75);
            //vertex(40, 20);
            //vertex(50, 75);
            //vertex(60, 20);
            //vertex(70, 75);
            //vertex(80, 20);
            //endShape();

            // sample 7
            //beginShape(TRIANGLE_STRIP);
            //vertex(30, 75);
            //vertex(40, 20);
            //vertex(50, 75);
            //vertex(60, 20);
            //vertex(70, 75);
            //vertex(80, 20);
            //vertex(90, 75);
            //endShape();

            // sample 8
            //beginShape(TRIANGLE_FAN);
            //vertex(57.5, 50);
            //vertex(57.5, 15);
            //vertex(92, 50);
            //vertex(57.5, 85);
            //vertex(22, 50);
            //vertex(57.5, 15);
            //endShape();

            // sample 9
            //beginShape(QUADS);
            //vertex(30, 20);
            //vertex(30, 75);
            //vertex(50, 75);
            //vertex(50, 20);
            //vertex(65, 20);
            //vertex(65, 75);
            //vertex(85, 75);
            //vertex(85, 20);
            //endShape();

            // sample 10
            //beginShape(QUAD_STRIP);
            //vertex(30, 20);
            //vertex(30, 75);
            //vertex(50, 75);
            //vertex(50, 20);
            //vertex(65, 20);
            //vertex(65, 75);
            //vertex(85, 75);
            //vertex(85, 20);
            //endShape();

            // sample 11
            beginShape();
            vertex(20, 20);
            vertex(40, 20);
            vertex(40, 40);
            vertex(60, 40);
            vertex(60, 60);
            vertex(20, 60);
            endShape(CLOSE);
        }

        void Doc_bezier()
        {
            // sample 1
            //noFill();
            //stroke(255, 102, 0);
            //line(85, 20, 10, 10);
            //line(90, 90, 15, 80);
            //stroke(0, 0, 0);
            //bezier(85, 20, 10, 10, 90, 90, 15, 80);

            // sample 2
            noFill();
            stroke(255, 102, 0);
            line(30, 20, 80, 5);
            line(80, 75, 30, 75);
            stroke(0, 0, 0);
            bezier(30, 20, 80, 5, 80, 75, 30, 75);
        }

        void Doc_colorMode()
        {
            noStroke();
            colorMode(RGB, 100);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    stroke(i, j, 0);
                    point(i, j);
                }
            }
        }

        void Doc_ellipse()
        {
            fill(255, 255, 255);
            ellipse(56, 46, 55, 55);
        }

        void Doc_endShape()
        {
            noFill();

            beginShape();
            vertex(20, 20);
            vertex(45, 20);
            vertex(45, 80);
            endShape(CLOSE);

            beginShape();
            vertex(50, 20);
            vertex(75, 20);
            vertex(75, 80);
            endShape();
        }

        void Doc_lerp()
        {
            //float a = 20;
            //float b = 80;
            //float c = lerp(a, b, .2);
            //float d = lerp(a, b, .5);
            //float e = lerp(a, b, .8);

            //beginShape(POINTS);
            //vertex(a, 50);
            //vertex(b, 50);
            //vertex(c, 50);
            //vertex(d, 50);
            //vertex(e, 50);
            //endShape();

            // sample 2
            int x1 = 15;
            int y1 = 10;
            int x2 = 80;
            int y2 = 90;
            line(x1, y1, x2, y2);
            for (int i = 0; i <= 10; i++)
            {
                float x = lerp(x1, x2, i / 10.0) + 10;
                float y = lerp(y1, y2, i / 10.0);
                point(x, y);
            }
        }

        void Doc_line()
        {
            // sample 1
            //line(30, 20, 85, 75);

            // sample 2
            line(30, 20, 85, 20);
            stroke(126);
            line(85, 20, 85, 75);
            stroke(255);
            line(85, 75, 30, 75);
        }

        void Doc_mouseX()
        {
        }

        void Doc_point()
        {
            // sample 1
            point(30, 20);
            point(85, 20);
            point(85, 75);
            point(30, 75);
        }

        void Doc_quad()
        {
            quad(38, 31, 86, 20, 69, 63, 30, 76);
        }

        void Doc_rect()
        {
            rect(30, 20, 55, 55);
        }

        void Doc_rectMode()
        {
            rectMode(CENTER);
            rect(35, 35, 50, 50);
            
            rectMode(CORNER);
            fill(102);
            rect(35, 35, 50, 50);
        }

        void Doc_sqrt()
        {
            noStroke();
            float a = sqrt(6561);  // Sets a to 81
            float b = sqrt(625);   // Sets b to 25
            float c = sqrt(1);     // Sets c to 1
            rect(0, 25, a, 10);
            rect(0, 45, b, 10);
            rect(0, 65, c, 10);
        }

        void Doc_strokeCap()
        {
            smooth();
            strokeWeight(12.0);
            strokeCap(ROUND);
            line(20, 30, 80, 30);
            strokeCap(SQUARE);
            line(20, 50, 80, 50);
            strokeCap(PROJECT);
            line(20, 70, 80, 70);
        }

        void Doc_strokeJoin()
        {
            strokeCap(SQUARE);

            // sample 1
            //noFill();
            //smooth();
            //strokeWeight(10.0);
            //strokeJoin(MITER);
            //beginShape();
            //vertex(35, 20);
            //vertex(65, 50);
            //vertex(35, 80);
            //endShape();

            // sample 2
            noFill();
            smooth();
            strokeWeight(10.0);
            strokeJoin(BEVEL);
            beginShape();
            vertex(35, 20);
            vertex(65, 50);
            vertex(35, 80);
            endShape();

            // sample 3
            //noFill();
            //smooth();
            //strokeWeight(10.0);
            //strokeJoin(ROUND);
            //beginShape();
            //vertex(35, 20);
            //vertex(65, 50);
            //vertex(35, 80);
            //endShape();
        }

        void Doc_strokeWeight()
        {
            smooth();
            strokeWeight(1);   // Default
            line(20, 20, 80, 20);
            strokeWeight(4);   // Thicker
            line(20, 40, 80, 40);
            strokeWeight(10);  // Beastly
            line(20, 70, 80, 70);
        }

        void Doc_triangle()
        {
            triangle(30, 75, 58, 20, 86, 75);
        }
    }
}
