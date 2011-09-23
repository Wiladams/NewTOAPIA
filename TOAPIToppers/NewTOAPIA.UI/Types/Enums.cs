﻿using System;

//using TOAPI.Kernel32;
using TOAPI.Types;
using TOAPI.User32;

using NewTOAPIA.Drawing;

namespace NewTOAPIA.UI
{
    //
    // These are constants used by the ShowWindow() function
    public enum ShowHide : int
    {
        Hide = 0,
        ShowNormal = 1,
        ShowMinimized = 2,
        ShowMaximized = 3,
        ShowNoActivate = 4,
        Show = 5,
        Minimize = 6,
        ShowMinNoActivate = 7,
        SW_SHOWNA = 8,
        Restore = 9,
        ShowDefault = 10
    }


    /// <summary>
    /// Enum for types of beep messages for MessageBeep()
    /// </summary>
    public enum MB_ICON
    {
        /// <summary>
        /// A simple windows beep
        /// </summary>            
        SimpleBeep = -1,
        /// <summary>
        /// A standard windows OK beep
        /// </summary>
        OK = 0x00,

        Hand = 0x10,

        /// <summary>
        /// A standard windows Question beep
        /// </summary>
        Question = 0x20,
        /// <summary>
        /// A standard windows Exclamation beep
        /// </summary>
        Exclamation = 0x30,
        /// <summary>
        /// A standard windows Asterisk beep
        /// </summary>
        Asterisk = 0x40,
    }

    [Flags]
    public enum MouseButtons
    {

        None = 0x00000000,

        Left = 0x00100000,
        Right = 0x00200000,
        Middle = 0x00400000,

        XButton1 = 0x0080000,
        XButton2 = 0x0100000,
    }


    // Constants for KeyboardType
    public enum TypeOfKeyboard : int
    {
        IBMPCXT = 1,
        Olivetti = 2,
        IBMPCAT = 3,
        IBMEnhanced = 4,
        Nokia1050 = 5,
        Nokia9140 = 6,
        Japanese = 7,
        RemoteDesktop =81   
    }

    public enum KeyboardFuncType : int
    {
        Type = 0,
        SubType = 1,
        FuncKeys = 2
    }



    [Flags]
    public enum KeyMasks : int
    {
        None = 0,

        // A couple of bit masks to get parts of the key message
        // Modifiers are the high word of the key code.  Further, these 
        // masks indicate which bits are used for Shift, Control, and Alt
        Shift = 0x00010000,
        Control = 0x00020000,
        Alt = 0x00040000,
        CapsLock = 0x00100000,
        NumLock = 0x00200000,
        ScrollLock = 0x00400000,
        /// <summary>
        /// The mask used to isolate the keyboard modifier bits
        /// </summary>
        Modifiers = (unchecked((int)0xFFFF0000)),

        /// <summary>
        /// The KeyCode is held in the lower 16 bits of the key value
        /// </summary>
        KeyCode = 0x0000FFFF,
    }

    /// <summary>
    /// Keys
    /// Represents the virtual key codes for keys pressed on the keyboard.
    /// These values are returned from GetMessage and the like before
    /// TranslateMessage() is called.
    /// </summary>
    public enum VirtualKeyCodes : int
    {
        // Junk information related to the mouse
        None = 0x00,
        LButton = 0x01,
        RButton = 0x02,
        Cancel = 0x03,
        MButton = 0x04,
        XButton1 = 0x05,
        XButton2 = 0x06,

        // Constants for interesting keys
        Back = 0x08,
        Tab = 0x09,
        Clear = 0x0C,
        Return = 0x0D,
        Enter = Return,
        ShiftKey = 0x10,
        ControlKey = 0x11,
        Menu = 0x12,
        Pause = 0x13,
        Capital = 0x14,
        CapsLock = 0x14,
        Escape = 0x1B,
        Space = 0x20,
        Prior = 0x21,
        PageUp = Prior,
        Next = 0x22,
        PageDown = Next,
        End = 0x23,
        Home = 0x24,
        Left = 0x25,
        Up = 0x26,
        Right = 0x27,
        Down = 0x28,
        Select = 0x29,
        Print = 0x2A,
        Execute = 0x2B,
        Snapshot = 0x2C,
        PrintScreen = Snapshot,
        Insert = 0x2D,
        Delete = 0x2E,
        Help = 0x2F,

        // The digit keys on top of keyboard
        D0 = 0x30,
        D1 = 0x31,
        D2 = 0x32,
        D3 = 0x33,
        D4 = 0x34,
        D5 = 0x35,
        D6 = 0x36,
        D7 = 0x37,
        D8 = 0x38,
        D9 = 0x39,

        // The alpha keys
        A = 0x41,
        B = 0x42,
        C = 0x43,
        D = 0x44,
        E = 0x45,
        F = 0x46,
        G = 0x47,
        H = 0x48,
        I = 0x49,
        J = 0x4A,
        K = 0x4B,
        L = 0x4C,
        M = 0x4D,
        N = 0x4E,
        O = 0x4F,
        P = 0x50,
        Q = 0x51,
        R = 0x52,
        S = 0x53,
        T = 0x54,
        U = 0x55,
        V = 0x56,
        W = 0x57,
        X = 0x58,
        Y = 0x59,
        Z = 0x5A,

        // Special Microsoft Natural keyboard keys
        LWin = 0x5B,
        RWin = 0x5C,
        Apps = 0x5D,

        // Digits on numeric keypad
        NumPad0 = 0x60,
        NumPad1 = 0x61,
        NumPad2 = 0x62,
        NumPad3 = 0x63,
        NumPad4 = 0x64,
        NumPad5 = 0x65,
        NumPad6 = 0x66,
        NumPad7 = 0x67,
        NumPad8 = 0x68,
        NumPad9 = 0x69,

        // And the special keys around the keypad
        Multiply = 0x6A,
        Add = 0x6B,
        Separator = 0x6C,
        Subtract = 0x6D,
        Decimal = 0x6E,
        Divide = 0x6F,

        // Function keys
        F1 = 0x70,
        F2 = 0x71,
        F3 = 0x72,
        F4 = 0x73,
        F5 = 0x74,
        F6 = 0x75,
        F7 = 0x76,
        F8 = 0x77,
        F9 = 0x78,
        F10 = 0x79,
        F11 = 0x7A,
        F12 = 0x7B,
        F13 = 0x7C,
        F14 = 0x7D,
        F15 = 0x7E,
        F16 = 0x7F,
        F17 = 0x80,
        F18 = 0x81,
        F19 = 0x82,
        F20 = 0x83,
        F21 = 0x84,
        F22 = 0x85,
        F23 = 0x86,
        F24 = 0x87,

        // More interesting keys
        NumLock = 0x90,
        Scroll = 0x91,
        LShiftKey = 0xA0,
        RShiftKey = 0xA1,
        LControlKey = 0xA2,
        RControlKey = 0xA3,
        LMenu = 0xA4,
        RMenu = 0xA5,
        ProcessKey = 0xE5,
        Attn = 0xF6,
        Crsel = 0xF7,
        Exsel = 0xF8,
        EraseEof = 0xF9,
        Play = 0xFA,
        Zoom = 0xFB,
        NoName = 0xFC,
        Pa1 = 0xFD,
        OemClear = 0xFE,

    }

    public enum ControlCategory
    {
        Caption = User32.DFC_CAPTION,
        Menu = User32.DFC_MENU,
        Scroll = User32.DFC_SCROLL,
        Button = User32.DFC_BUTTON,
        PopupMenu = User32.DFC_POPUPMENU,
    }

    public enum ButtonElementType
    {
        Check = User32.DFCS_BUTTONCHECK,
        RadioImage = User32.DFCS_BUTTONRADIOIMAGE,
        RadioMask = User32.DFCS_BUTTONRADIOMASK,
        Radio = User32.DFCS_BUTTONRADIO,
        ThreeState = User32.DFCS_BUTTON3STATE,
        PushButton = User32.DFCS_BUTTONPUSH,
    }

    public enum MenuElementType
    {
        Arrow = User32.DFCS_MENUARROW,
        Check = User32.DFCS_MENUCHECK,
        Bullet = User32.DFCS_MENUBULLET,
        ArrowRight = User32.DFCS_MENUARROWRIGHT,
    }

    [Flags]
    public enum ControlStateModifier
    {
        None = 0,
        Inactive =    User32.DFCS_INACTIVE,
        Pushed =    User32.DFCS_PUSHED,
        Checked =    User32.DFCS_CHECKED,
        Transparent =    User32.DFCS_TRANSPARENT,
        Hot =    User32.DFCS_HOT,
        Flat =    User32.DFCS_FLAT,
        Mono =    User32.DFCS_MONO
    }
}