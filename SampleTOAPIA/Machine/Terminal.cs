using System;
using System.Text;

using TOAPI.Types;
using TOAPI.Kernel32;

public class Terminal
{
    IntPtr fOutputHandle = IntPtr.Zero;
    IntPtr fInputHandle = IntPtr.Zero;
    IntPtr fErrorHandle = IntPtr.Zero;

    public Terminal()
    {
        fOutputHandle = Kernel32.GetStdHandle(StdHandleEnum.STD_OUTPUT_HANDLE);
        fInputHandle = Kernel32.GetStdHandle(StdHandleEnum.STD_INPUT_HANDLE);
        fErrorHandle = Kernel32.GetStdHandle(StdHandleEnum.STD_ERROR_HANDLE);
    }

    // Properties of Terminal 
    // Title
    // Mouse Cursor Position
    // Display Mode
    // Font
    // Size

    public int DisplayMode
    {
        //get
        //{
        //    //int retValue = Kernel32.GetConsoleDisplayMode();
        //}

        set
        {
            bool retValue = Kernel32.SetConsoleDisplayMode(fOutputHandle, value);
        }
    }

    public COORD FontSize
    {
        get
        {
            CONSOLE_FONT_INFO currentFont=new CONSOLE_FONT_INFO();
            Kernel32.GetCurrentConsoleFont(fOutputHandle, false, ref currentFont);
            return currentFont.dwFontSize;
        }
    }

    public int MouseButtons
    {
        get
        {
            int nButtons = 0;
            bool retValue = Kernel32.GetNumberOfConsoleMouseButtons(ref nButtons);
            
            // If no successful return, then make sure we return
            // zero buttons.
            if (!retValue)
                nButtons = 0;

            return nButtons;
        }
    }

    public string Title
    {
        get
        {
            StringBuilder builder = new StringBuilder(256);
            int returned = Kernel32.GetConsoleTitle(builder, 256);
            return builder.ToString();
        }

        set
        {
            Kernel32.SetConsoleTitle(value);
        }
    }

    public void Write(string aString)
    {
        int charsWritten;
        Kernel32.WriteConsole(fOutputHandle, aString, aString.Length, out charsWritten, IntPtr.Zero);
    }

    public void WriteLine(string aString)
    {
        int charsWritten;
        Kernel32.WriteConsole(fOutputHandle, aString, aString.Length, out charsWritten, IntPtr.Zero);
        Kernel32.WriteConsole(fOutputHandle, "\r\n", 2, out charsWritten, IntPtr.Zero);
    }

}

