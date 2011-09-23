using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processing
{
    public class PState
    {


        public bool focused { get; set; }
        public int frameCount { get; set; }
        public int frameRate { get; set; }      // Number of frames per second
        public bool online { get; set; }        // Confirms if a Processing program is runing inside a web browser
        public size screen { get; set; }        // System: Size of the screen


        public PState()
        {
        }
    }
}
