using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Sketch1
{
    using Processing;

    public class Chapter7 : PSketch
    {
        public override void setup()
        {
            //Fig7_6();
            //Fig7_9();
            //Fig7_11();
            //Fig7_21();
            Fig7_26();
        }

        public void Fig7_6()
        {
            size(200, 200);
            background(255);
            int margin = height / 15;
            strokeWeight(5);
            smooth();
            float x = margin;
            float y = margin;

            float xSpeed = 1.1f;
            float ySpeed = 1.02f;

            while (y < height - margin)
            {
                point(x, y);
                x += xSpeed;    // arithmetic progression
                y *= ySpeed;    // geometric progression
            }
        }

        public void Fig7_9()
        {
            int particles = 25;
            float[] x = new float[particles];
            float[] y = new float[particles];
            float[] xSpeed = new float[particles];
            float[] ySpeed = new float[particles];
            float[] accel = new float[particles];
            float gravity = 0.75f;

            // setup
            size(800, 400);
            background(0);
            smooth();
            strokeWeight(1.5f);
            stroke(255);

            // fill speed arrays with initial values
            for (int i = 0; i < particles; i++)
            {
                xSpeed[i] = random(0.75f, 1.2f);
                accel[i] = random(0.00f, 0.2f);
            }

            for (int i = 0; i < particles; i++)
            {
                // stop particle on collision with right edge of display window
                while (x[i] < width)
                {
                    x[i] += xSpeed[i];

                    // double assignment creates y acceleration
                    ySpeed[i] += accel[i];
                    y[i] += ySpeed[i];

                    // draw the point
                    point(x[i], y[i]);

                    // check ground detection only
                    if (y[i] >= height)
                    {
                        // reverse particle direction
                        ySpeed[i] *= -1;

                        // lower particle speed
                        ySpeed[i] *= gravity;

                        // keep particle from sliding out of window
                        y[i] = height;
                    }
                }
            }
        }

        public void Fig7_11()
        {
            int particles = 125;
            int timeLimit = 2000;
            float particleSpan = 2;
            float accelMin = 0.005f;
            float accelMax = 0.2f;
            float strokeWtMin = 1.25f;
            float strokeWtMax = 1.6f;
            float materialMin = 0.25f;
            float materialMax = 0.99f;
            float gravity = 0.9f;

            // not meant to be changed
            int timer = 0;
            float[] x = new float[particles];
            float[] y = new float[particles];
            float[] xSpeed = new float[particles];
            float[] ySpeed = new float[particles];
            float[] accel = new float[particles];
            float[] material = new float[particles];
            float[] strokeWts = new float[particles];

            // setup
            size(800, 400);
            background(0);
            smooth();
            stroke(255);

            // fill speed arrays with initial values
            for (int i = 0; i < particles; i++)
            {
                x[i] = random(width / 2 - 10, width / 2 + 10);
                xSpeed[i] = random(-particleSpan, particleSpan);
                accel[i] = random(accelMin, accelMax);
                material[i] = random(materialMin, materialMax);
                strokeWts[i] = random(strokeWtMin, strokeWtMax);
            }

            for (int i = 0; i < particles; i++)
            {
                // timer controls while loop
                timer = 0;
                strokeWeight(strokeWts[i]);

                while (timer++ < timeLimit)
                {
                    x[i] += xSpeed[i];

                    // double assignment creates y acceleration
                    ySpeed[i] += accel[i];
                    y[i] += ySpeed[i];
                    point(x[i], y[i]);

                    // check ground detection
                    if (y[i] >= height)
                    {
                        // reverse particle direction
                        ySpeed[i] *= -1*material[i];

                        // lower particle speed
                        ySpeed[i] *= gravity;

                        // keep particle from sliding out of window
                        y[i] = height;
                    }

                    // wall detection
                    if (x[i] >= width || x[i] < 0)
                    {
                        // reverse particle horizontal direction
                        xSpeed[i] *= -1;
                    }

                }
            }
        }

        public void Fig7_21()
        {
            size(200, 200);
            background(255);
            int x = width / 2;
            int y = height / 2;
            int w = 100;
            int h = 100;

            strokeWeight(4);
            smooth();
            fill(0);
            arc(x, y - h / 2, w, h, 0, PI);
            //noFill();
            //arc(x, y + h / 2, w, h, PI, PI * 2);
        }

        public void Fig7_26()
        {
            size(400, 400);
            background(255);
            smooth();

            Point pt1 = new Point(150, 300);
            Point pt2 = new Point(100, 100);
            Point pt3 = new Point(300, 100);
            Point pt4 = new Point(250, 300);

            // plot curve
            stroke(0);
            bezier(pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y);

            // draw control points connected to anchor points
            stroke(150);
            line(pt1.X, pt1.Y, pt2.X, pt2.Y);
            line(pt3.X, pt3.Y, pt4.X, pt4.Y);

            // control points
            ellipse(pt2.X, pt2.Y, 10, 10);
            ellipse(pt3.X, pt3.Y, 10, 10);

            // anchor points
            rectMode(CENTER);
            rect(pt1.X, pt1.Y, 10, 10);
            rect(pt4.X, pt4.Y, 10, 10);
        }
    }
}
