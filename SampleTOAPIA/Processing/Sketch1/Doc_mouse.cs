using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sketch1
{
    using Processing;

    public class Doc_mouse : PSketch
    {
        public override void setup()
        {
            size(400, 400);
            background(204);
        }

        //public override void draw()
        //{
        //    background(204);

        //    color vColor = color((int)random(255), (int)random(255), (int)random(255));
        //    stroke(vColor);

        //    // vertical lines
        //    line(mouseX, 20, mouseX, 80);
        //    line(mouseX, height - 80, mouseX, height - 20);

        //    // horizontal lines
        //    line(20, mouseY, 80, mouseY);
        //    line(width-80, mouseY, width-20, mouseY);

        //}

        // Click within the image to change 
        // the value of the rectangle
        //public override void draw()
        //{
        //    if (mousePressed == true)
        //    {
        //        fill(0);
        //    }
        //    else
        //    {
        //        fill(255);
        //    }
        //    rect(25, 25, 50, 50);
        //}

        // Click within the image and press
        // the left and right mouse buttons to 
        // change the value of the rectangle
        public override void draw()
        {
            rect(25, 25, 50, 50);
        }

        protected override void mousePressed()
        {
            if (mouseButton == LEFT)
            {
                fill(0);
            }
            else if (mouseButton == RIGHT)
            {
                fill(255);
            }
            else
            {
                fill(126);
            }
        }
    }
}
