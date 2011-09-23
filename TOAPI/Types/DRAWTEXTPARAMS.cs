using System.Runtime.InteropServices;

namespace TOAPI.Types
{
	[StructLayout(LayoutKind.Sequential)]
	public struct DRAWTEXTPARAMS
	{
        public int cbSize;
		public int iTabLength;
        public int iLeftMargin;
        public int iRightMargin;
		public uint uiLengthDrawn;

        //public DRAWTEXTPARAMS(int tabLength, int leftMargin, int rightMargin, uint lengthDrawn)
        //{
        //    cbSize = (uint)Marshal.SizeOf(typeof(DRAWTEXTPARAMS));
        //    iTabLength = tabLength;
        //    iLeftMargin = leftMargin;
        //    iRightMargin = rightMargin;
        //    uiLengthDrawn = lengthDrawn;
        //}

        public void Init()
        {
            cbSize = Marshal.SizeOf(this);
        }
    }
}