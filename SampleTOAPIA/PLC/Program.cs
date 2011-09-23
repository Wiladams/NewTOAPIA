using System;

namespace PLC
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            PLCWindow window = new PLCWindow();
            window.Show();
            window.PlatformWindow.Run();
        }
    }
}
