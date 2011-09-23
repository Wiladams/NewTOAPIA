
namespace JoyTest
{
    using NewTOAPIA.UI;

    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main()
        {
            JoyWindow win = new JoyWindow(640, 480);
            User32Application app = new User32Application();
            app.Run(win);
        }



    }
}
