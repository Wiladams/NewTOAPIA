
using System;
using TOAPI.Types;
using TOAPI.WinMM;

using NewTOAPIA;

namespace NewTOAPIA.UI
{

    /// <summary>
    ///     Decodes Windows messages.  This is in a separate class from Message
    ///     so we can avoid loading it in the 99% case where we don't need it.
    /// </summary>
    public sealed class MessageDecoder 
    {
        /// <summary>
        ///     Returns the symbolic name of the msg value, or null if it
        ///     isn't one of the existing constants.
        /// </summary>
        /// <returns>
        ///     The symbolic name of the msg value.
        /// </returns>
        public static string MsgToString(int msg)
        {
            string text;

            if ((msg & (int)WinMsg.WM_REFLECT) == (int)WinMsg.WM_REFLECT)
            {
                string subtext = MsgToString(msg - (int)WinMsg.WM_REFLECT);
                if (subtext == null)
                    subtext = "???";
                text = "WM_REFLECT + " + subtext;
            }
            else
            {
                text = "WM_<UNKNOWN>: " + "0x" + msg.ToString("x") + ", " + msg.ToString("d");
            }

            return text;
        }


        private static string Parenthesize(string input) 
		{
            if (input == null)
                return "";
            else
                return " " + input + "";
                //return " (" + input + ")";
        }

        public static string ToString(IntPtr hWnd, int msg, int wParam, int lParam) 
		{
            string ID = Parenthesize(MsgToString(msg));

			string lDescription = "";
            if (msg == (int)WinMsg.WM_PARENTNOTIFY)
                lDescription = Parenthesize(MsgToString((int)BitUtils.Loword((int)wParam)));

            return "<MSG id='0x" + Convert.ToString(msg, 16) 
			+ "' name='" + ID
            + "' hwnd='0x" + hWnd.ToString()
            + "' wParam='0x" + Convert.ToString((int)wParam, 16)
            + "' lParam='0x" + Convert.ToString((int)lParam, 16) 
			+ lDescription
			+ "' />";
        }

        public static string ToString(MSG message)
        {
            // first, see if it's one of the defined WinMsg enum message
            if (Enum.IsDefined(typeof(WinMsg), message.message))
                return ((WinMsg)message.message).ToString();

            return ToString(message.hWnd, message.message, (int)message.wParam, (int)message.lParam);
        }
    }
}
