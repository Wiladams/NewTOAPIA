using System;
using System.Text;

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


    public virtual void Open(string application)
    {
        PROCESS_INFORMATION processInfo = new PROCESS_INFORMATION();
        STARTUPINFO startupInfo = new STARTUPINFO();
        startupInfo.Init();

        Kernel32.CreateProcess(application, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, false,
            ProcessCreationFlags.CREATE_NEW_CONSOLE, IntPtr.Zero, null, ref startupInfo, out processInfo);
    }

    public void Run()
    {
        // Do some stuff to the terminal

    }

    // Properties of Terminal 
    // Display Mode
    // Font
    public short BufferHeight
    {
        get
        {
            CONSOLE_SCREEN_BUFFER_INFO scrBuffInfo = new CONSOLE_SCREEN_BUFFER_INFO();
            
            bool success = Kernel32.GetConsoleScreenBufferInfo(fOutputHandle, ref scrBuffInfo);
            return scrBuffInfo.dwSize.Y;
        }

        set
        {
            CONSOLE_SCREEN_BUFFER_INFO scrBuffInfo = new CONSOLE_SCREEN_BUFFER_INFO();

            // Get the current position
            bool success = Kernel32.GetConsoleScreenBufferInfo(fOutputHandle, ref scrBuffInfo);
            scrBuffInfo.dwSize.Y = value;
            success = Kernel32.SetConsoleScreenBufferSize(fOutputHandle, scrBuffInfo.dwSize);
        }

    }

    public short BufferWidth
    {
        get
        {
            CONSOLE_SCREEN_BUFFER_INFO scrBuffInfo = new CONSOLE_SCREEN_BUFFER_INFO();

            bool success = Kernel32.GetConsoleScreenBufferInfo(fOutputHandle, ref scrBuffInfo);
            return scrBuffInfo.dwSize.X;
        }

        set
        {
            CONSOLE_SCREEN_BUFFER_INFO scrBuffInfo = new CONSOLE_SCREEN_BUFFER_INFO();

            // Get the current position
            bool success = Kernel32.GetConsoleScreenBufferInfo(fOutputHandle, ref scrBuffInfo);
            scrBuffInfo.dwSize.X = value;
            success = Kernel32.SetConsoleScreenBufferSize(fOutputHandle, scrBuffInfo.dwSize);
        }

    }

    public void Clear()
    {
        CONSOLE_SCREEN_BUFFER_INFO csbi=new CONSOLE_SCREEN_BUFFER_INFO();
        COORD home = new COORD(0,0);
        uint numChars;
        uint length;

        Kernel32.GetConsoleScreenBufferInfo(fOutputHandle, ref csbi);
        length = (uint)(csbi.dwSize.X * csbi.dwSize.Y);

        Kernel32.FillConsoleOutputCharacter(fOutputHandle, ' ',length, home, out numChars);

        Kernel32.SetConsoleCursorPosition(fOutputHandle, home);
    }

    public COORD Size
    {
        get
        {
            CONSOLE_SCREEN_BUFFER_INFO scrBuffInfo = new CONSOLE_SCREEN_BUFFER_INFO();

            bool success = Kernel32.GetConsoleScreenBufferInfo(fOutputHandle, ref scrBuffInfo);
            return scrBuffInfo.dwSize;
        }
    }

    public short CursorLeft
    {
        get
        {
            CONSOLE_SCREEN_BUFFER_INFO scrBuffInfo = new CONSOLE_SCREEN_BUFFER_INFO();

            bool success = Kernel32.GetConsoleScreenBufferInfo(fOutputHandle, ref scrBuffInfo);
            return scrBuffInfo.dwCursorPosition.X;
        }

        set
        {
            CONSOLE_SCREEN_BUFFER_INFO scrBuffInfo = new CONSOLE_SCREEN_BUFFER_INFO();

            // Get the current position
            bool success = Kernel32.GetConsoleScreenBufferInfo(fOutputHandle, ref scrBuffInfo);
            COORD pos = new COORD(value, scrBuffInfo.dwCursorPosition.Y);
            success = Kernel32.SetConsoleCursorPosition(fOutputHandle, pos);
        }

    }

    public short CursorTop
    {
        get
        {
            CONSOLE_SCREEN_BUFFER_INFO scrBuffInfo = new CONSOLE_SCREEN_BUFFER_INFO();

            bool success = Kernel32.GetConsoleScreenBufferInfo(fOutputHandle, ref scrBuffInfo);
            return scrBuffInfo.dwCursorPosition.Y;
        }

        set
        {
            CONSOLE_SCREEN_BUFFER_INFO scrBuffInfo = new CONSOLE_SCREEN_BUFFER_INFO();

            // Get the current position
            bool success = Kernel32.GetConsoleScreenBufferInfo(fOutputHandle, ref scrBuffInfo);
            COORD pos = new COORD(scrBuffInfo.dwCursorPosition.X, value);
            success = Kernel32.SetConsoleCursorPosition(fOutputHandle, pos);
        }
    }

    public void GotoXY(short x, short y)
    {
        COORD newPoint = new COORD(x,y);
        bool success = Kernel32.SetConsoleCursorPosition(fOutputHandle, newPoint);
    }

    public COORD CursorPosition
    {
        get
        {
            CONSOLE_SCREEN_BUFFER_INFO scrBuffInfo = new CONSOLE_SCREEN_BUFFER_INFO();
            
            bool success = Kernel32.GetConsoleScreenBufferInfo(fOutputHandle, ref scrBuffInfo);
            return scrBuffInfo.dwCursorPosition;
        }

        set
        {
            bool success = Kernel32.SetConsoleCursorPosition(fOutputHandle, value);
        }
    }

    public int CursorSize
    {
        get
        {
            CONSOLE_CURSOR_INFO cursorInfo = new CONSOLE_CURSOR_INFO();
            bool success = Kernel32.GetConsoleCursorInfo(fOutputHandle, ref cursorInfo);

            return cursorInfo.dwSize;
        }

        set
        {
            CONSOLE_CURSOR_INFO cursorInfo = new CONSOLE_CURSOR_INFO();
            bool success = Kernel32.GetConsoleCursorInfo(fOutputHandle, ref cursorInfo);
            cursorInfo.dwSize = value;
            success = Kernel32.SetConsoleCursorInfo(fOutputHandle, ref cursorInfo);
        }
    }

    public bool CursorVisible
    {
        get
        {
            CONSOLE_CURSOR_INFO cursorInfo = new CONSOLE_CURSOR_INFO();
            bool success = Kernel32.GetConsoleCursorInfo(fOutputHandle, ref cursorInfo);

            return cursorInfo.bVisible;
        }

        set
        {
            CONSOLE_CURSOR_INFO cursorInfo = new CONSOLE_CURSOR_INFO();
            bool success = Kernel32.GetConsoleCursorInfo(fOutputHandle, ref cursorInfo);
            cursorInfo.bVisible = value;
            success = Kernel32.SetConsoleCursorInfo(fOutputHandle, ref cursorInfo);
        }
    }

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

    public void FlushInputBuffer()
    {
        bool success = Kernel32.FlushConsoleInputBuffer(fInputHandle);
    }

    public COORD FontSize
    {
        get
        {
            CONSOLE_FONT_INFO currentFont = new CONSOLE_FONT_INFO();
            Kernel32.GetCurrentConsoleFont(fOutputHandle, false, ref currentFont);
            return currentFont.dwFontSize;
        }
    }

    public ConsoleTextColor ForegroundColor
    {
        get
        {
            CONSOLE_SCREEN_BUFFER_INFO csbi = new CONSOLE_SCREEN_BUFFER_INFO();
            bool success = Kernel32.GetConsoleScreenBufferInfo(fOutputHandle, ref csbi);

            return (ConsoleTextColor)(csbi.wAttributes&0x000f);
        }

        set
        {
            CONSOLE_SCREEN_BUFFER_INFO csbi = new CONSOLE_SCREEN_BUFFER_INFO();
        
            // First, get the current character attribute settings
            bool success = Kernel32.GetConsoleScreenBufferInfo(fOutputHandle, ref csbi);

            // Add the desired color to the foreground
            ushort background = (ushort)(csbi.wAttributes & 0x00f0);
            csbi.wAttributes = (ushort)(background | (ushort)value) ;

            // Now set the text attribute
            success = Kernel32.SetConsoleTextAttribute(fOutputHandle, csbi.wAttributes);
        }
    }

    public ConsoleTextColor BackgroundColor
    {
        get
        {
            CONSOLE_SCREEN_BUFFER_INFO csbi = new CONSOLE_SCREEN_BUFFER_INFO();
            bool success = Kernel32.GetConsoleScreenBufferInfo(fOutputHandle, ref csbi);

            return (ConsoleTextColor)((csbi.wAttributes & 0x00f0)>>4);
        }

        set
        {
            CONSOLE_SCREEN_BUFFER_INFO csbi = new CONSOLE_SCREEN_BUFFER_INFO();

            // First, get the current character attribute settings
            bool success = Kernel32.GetConsoleScreenBufferInfo(fOutputHandle, ref csbi);

            // Add the desired color to the foreground
            ushort foreground = (ushort)(csbi.wAttributes & 0x000f);
            csbi.wAttributes = (ushort)(((ushort)value)<<4 | (ushort)foreground);

            // Now set the text attribute
            success = Kernel32.SetConsoleTextAttribute(fOutputHandle, csbi.wAttributes);
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

    public IntPtr WindowHandle
    {
        get
        {
            IntPtr handle = Kernel32.GetConsoleWindow();
            return handle;
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

    // Console Properties
    //In Gets the standard input stream. 
    //Error Gets the standard error output stream. 
    //Out Gets the standard output stream. 
    
    //InputEncoding Gets or sets the encoding the console uses to read input.  
    //OutputEncoding Gets or sets the encoding the console uses to write output.  
    //CapsLock Gets a value indicating whether the CAPS LOCK keyboard toggle is turned on or turned off. 
    //NumberLock Gets a value indicating whether the NUM LOCK keyboard toggle is turned on or turned off. 
 
 //KeyAvailable Gets a value indicating whether a key press is available in the input stream. 
 //LargestWindowHeight Gets the largest possible number of console window rows, based on the current font and screen resolution. 
 //LargestWindowWidth Gets the largest possible number of console window columns, based on the current font and screen resolution. 
 //TreatControlCAsInput Gets or sets a value indicating whether the combination of the Control modifier key and C console key (CTRL+C) is treated as ordinary input or as an interruption that is handled by the operating system. 
 //WindowHeight Gets or sets the height of the console window area. 
 //WindowLeft Gets or sets the leftmost position of the console window area relative to the screen buffer. 
 //WindowTop Gets or sets the top position of the console window area relative to the screen buffer. 
 //WindowWidth 


    //Beep Overloaded. Plays the sound of a beep through the console speaker. 
    // MoveBufferArea Overloaded. Copies a specified source area of the screen buffer to a specified destination area. 
    // OpenStandardError Overloaded. Acquires the standard error stream. 
    // OpenStandardInput Overloaded. Acquires the standard input stream. 
    // OpenStandardOutput Overloaded. Acquires the standard output stream. 
    // Read Reads the next character from the standard input stream. 
    // ReadKey Overloaded. Obtains the next character or function key pressed by the user. 
    // ReadLine Reads the next line of characters from the standard input stream. 
    // ResetColor Sets the foreground and background console colors to their defaults. 
    // SetBufferSize Sets the height and width of the screen buffer area to the specified values. 
    // SetCursorPosition Sets the position of the cursor. 
    // SetError Sets the Error property to the specified TextWriter object. 
    // SetIn Sets the In property to the specified TextReader object. 
    // SetOut Sets the Out property to the specified TextWriter object. 
    // SetWindowPosition Sets the position of the console window relative to the screen buffer. 
    // SetWindowSize Sets the height and width of the console window to the specified values. 
    // Write Overloaded. Writes the text representation of the specified value or values to the standard output stream. 
    // WriteLine 

}


