using System;

using NewTOAPIA.UI;

namespace GDIVideo
{
    static class Program
    {
        static void Main()
        {
            Form1 form = new Form1();
            User32Application app = new User32Application();

            app.Run(form);
        }
    }
}
