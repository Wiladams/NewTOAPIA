using System;
using System.Collections.Generic;
using System.Text;

class DigitalClockMain
{

    public static void Main(string[] args)
    {
        ClockWindow win = new ClockWindow();
        win.Show();
        win.Run();
    }
}

