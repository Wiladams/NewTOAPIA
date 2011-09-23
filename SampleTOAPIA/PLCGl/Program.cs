using System;

namespace PLC
{
    using NewTOAPIA.GL;

    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            PLCModel model = new PLCModel();

            GLApplication<PLCController> app = new GLApplication<PLCController>("PLC GL");
            app.Run(model);
        }
    }
}
