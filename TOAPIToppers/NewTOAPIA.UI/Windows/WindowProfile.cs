namespace NewTOAPIA.UI
{
    public class WindowProfile
    {
        public string Title { get; private set; }
        public WindowStyle WindowStyle { get; private set; }
        public ExtendedWindowStyle ExtendedWindowStyle { get; private set; }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        #region Constructors
        public WindowProfile(string title, WindowStyle style, ExtendedWindowStyle extStyle)
            : this(title, style, extStyle, 0, 0, 0, 0)
        {
        }

        public WindowProfile(string title, WindowStyle style, ExtendedWindowStyle extStyle, int x, int y, int width, int height)
        {
            Title = title;
            WindowStyle = style;
            ExtendedWindowStyle = extStyle;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        #endregion
    }
}