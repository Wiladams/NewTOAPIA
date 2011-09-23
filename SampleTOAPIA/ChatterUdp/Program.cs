using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Chatter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            double freq = readCPUFrequency();
            Console.WriteLine("Freq: {0}", freq);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static double readCPUFrequency()
        {
            long gTickFactor = 1000000;
            long ticks;
            double retValue;

            if (System.Diagnostics.Stopwatch.IsHighResolution)
                ticks = System.Diagnostics.Stopwatch.Frequency;
            else
                ticks = 1*gTickFactor;

            retValue = (double)ticks / gTickFactor;
            return retValue;
        }

    }
}
