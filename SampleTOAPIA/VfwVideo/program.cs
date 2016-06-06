using NewTOAPIA.UI;

namespace VfwVideo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Form1 form = new Form1();
            User32Application app = new User32Application();

            app.Run(form);

        }
    }
}
