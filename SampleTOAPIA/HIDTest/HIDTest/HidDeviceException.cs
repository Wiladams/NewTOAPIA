namespace NewTOAPIA
{
    using System;
    using System.Runtime.InteropServices;

    public class HIDDeviceException : ApplicationException
    {
        public HIDDeviceException(string strMessage) 
            : base(strMessage) 
        { }

        public static HIDDeviceException GenerateWithWinError(string strMessage)
        {
            return new HIDDeviceException(string.Format("Msg:{0} WinEr:{1:X8}", strMessage, Marshal.GetLastWin32Error()));
        }

        public static HIDDeviceException GenerateError(string strMessage)
        {
            return new HIDDeviceException(string.Format("Msg:{0}", strMessage));
        }
    }

}