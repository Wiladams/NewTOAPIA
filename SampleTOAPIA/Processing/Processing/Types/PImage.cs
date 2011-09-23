using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processing
{
    public class PImage
    {
        public int width { get; set; }
        public int height { get; set; }
        public FORMAT format { get; set; }

        public byte[] pixels { get; set; }

        public PImage()
        {
        }

        public PImage(int width_, int height_)
        {
            width = width;
            height = height;
            format = FORMAT.ARGB;
        }

        public PImage(int width_, int height_, FORMAT format_)
        {
            width = width;
            height = height;
            format = format_;
        }

        public PImage(PImage img)
        {
        }

        public void get()
        {
        }

        public void set()
        {
        }

        public void copy()
        {
        }

        public void mask()
        {
        }

        public void blend()
        {
        }

        public void filter()
        {
        }

        public void save()
        {
        }

        public void resize()
        {
        }
        
        public void loadPixels()
        {
        }

        public void udatePixels()
        {
        }
    }
}
