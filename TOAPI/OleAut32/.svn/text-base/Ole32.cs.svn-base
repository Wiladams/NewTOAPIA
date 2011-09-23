using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace TOAPI.Ole
{
    public class Ole32
    {
        // Supplies a pointer to an implementation of IBindCtx
        [DllImport("ole32.dll")]
        public static extern int CreateBindCtx(int reserved, out IBindCtx ppbc);

        // Converts a string into a moniker that identifies
        // the object named by the string
        [DllImport("ole32.dll", CharSet = CharSet.Unicode)]
        public static extern int MkParseDisplayName(IBindCtx pbc, string szUserName, ref int pchEaten, out IMoniker ppmk);

    }
}
