
namespace MouseTest
{
    using System;

    class Test
    {

        public static void Main(string[] args)
        {
            MouseTestWindow win = new MouseTestWindow();
            win.Show();
            win.PlatformWindow.Run();
        }
    }
}
