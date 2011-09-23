using System;

using NewTOAPIA.UI;

namespace Snapper
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            SnapperWindow win = new SnapperWindow();

            User32Application app = new User32Application();
            app.Run(win);
        }
    }
}
