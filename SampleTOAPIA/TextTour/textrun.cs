using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace TextTour
{
    public class TextRun : Graphic
    {
        List<Char> fCharacters;
        GDIFont fFont;
        Size fFontSize;
        int fIndexOfFirstCharacter;
        int fIndexOfLastCharacter;
        int fIndexOfNextCharacter;

        #region Constructors
        public TextRun(GDIFont aFont, int x, int y, int width, int height)
            :base("TextRun", x, y, width, height)
        {
            fIndexOfFirstCharacter = 0;
            fIndexOfNextCharacter = 0;
            fIndexOfLastCharacter = -1;
            fCharacters = new List<char>();
            fFont = aFont;
            fFontSize = fFont.MeasureString("W");
            Rectangle frame = Frame;
            frame.Height = fFontSize.Height;
            Frame = frame;
        }
        #endregion

        #region Properties
        public int IndexOfFirstCharacter
        {
            get { return fIndexOfFirstCharacter; }
        }

        public int IndexOfNextCharacter
        {
            get { return fIndexOfNextCharacter; }
        }

        public int IndexOfLastCharacter
        {
            get {
                int index = IndexOfFirstCharacter + fCharacters.Count-1;
                return index;
            }
        }

        public int Length
        {
            get { return fCharacters.Count; }
        }
        #endregion

        #region Edit Commands
        public void AppendCharacter(char aChar)
        {
            fCharacters.Add(aChar);
            fIndexOfNextCharacter++;
            char[] charArray = { aChar };
            Size charSize = fFont.MeasureString(new string(charArray));
            Rectangle frame = Frame;
            frame.Width += charSize.Width;
            Frame = frame;
        }

        public void DeleteCharacter(int index)
        {
            Rectangle currentFrame = Frame;
            Rectangle characterFrame = GetIndexedCharacterRectangle(index);

            fCharacters.RemoveAt(index);
            fIndexOfNextCharacter--;
            currentFrame.Width -= characterFrame.Width;
            Frame = currentFrame;
        }

        public void InsertCharacter(int index, char aChar)
        {
            fCharacters.Insert(index, aChar);
            fIndexOfNextCharacter++;
            char[] charArray = { aChar };
            Size charSize = fFont.MeasureString(new string(charArray));
            Rectangle frame = Frame;
            frame.Width += charSize.Width;
            Frame = frame;
        }
        #endregion

        public override void DrawSelf(DrawEvent devent)
        {
            devent.GraphPort.SetFont(fFont);
            devent.GraphPort.DrawString(Frame.X, Frame.Y, new string(fCharacters.ToArray()));
        }

        public Rectangle GetIndexedCharacterRectangle(int index)
        {
            
            if ((fCharacters.Count == 0) || (index < 0))
                return new Rectangle(Frame.X, Frame.Y, 0,0);

            if ((index >= fCharacters.Count))
                return new Rectangle(Frame.X, Frame.Y, 0, 0);

            int x = Frame.X;
            int y = Frame.Y;
            int width=0;
            int height = Frame.Height;
            int charOffset=0;
        
            while (charOffset <= index)
            {
                x += width;
                char [] charArray = { fCharacters[charOffset] };
                Size charSize = fFont.MeasureString(new string(charArray));
                width = charSize.Width;
                charOffset++;
            }

            Rectangle frame;
            frame = new Rectangle(x, y, width, height);

            return frame;
        }

        public int GetCharacterIndexForPoint(Point aPoint)
        {
            // until the index is found
            for (int i=0; i< fCharacters.Count; i++)
            {
                // get the rectangle of each character
                Rectangle frame = GetIndexedCharacterRectangle(i);

                // see if the point is located within its rectangle
                // if it is, then return that character
                if (frame.Contains(aPoint))
                    return i;
            }            
            
            // Return -1 to indicate no character found
            return -1;
        }

        public Point GetCharacterLeadingPositionForPoint(Point aPoint)
        {
            // Get the index of the character at the point
            int charIndex = GetCharacterIndexForPoint(aPoint);

            if (-1 == charIndex)
                return Point.Empty;

            // if the index != -1, then report the leading position
            // for the specified character.
            Rectangle frame = GetIndexedCharacterRectangle(charIndex);

            Point leading = new Point(frame.X, frame.Y);
            return leading;
        }

        public Point GetLeadingPositionOfLastCharacter()
        {
            Rectangle charFrame = GetIndexedCharacterRectangle(fCharacters.Count - 1);
            return new Point(charFrame.X, charFrame.Y);
        }

        public Point GetTrailingPositionOfLastCharacter()
        {
            Rectangle charFrame = GetIndexedCharacterRectangle(fCharacters.Count - 1);
            return new Point(charFrame.Right, charFrame.Y);
        }

        public Point GetTrailingPositionOfIndexedCharacter(int index)
        {
            Rectangle charFrame = GetIndexedCharacterRectangle(index);
            if (Rectangle.Empty == charFrame)
                return Point.Empty;

            return new Point(charFrame.Right, charFrame.Y);
        }

        public Point GetLeadingPositionOfIndexedCharacter(int index)
        {
            // If it's the last character, then 
            // return the edge of the previous character
            if (index == fCharacters.Count)
                return GetTrailingPositionOfIndexedCharacter(index - 1);

            Rectangle charFrame = GetIndexedCharacterRectangle(index);
            if (Rectangle.Empty == charFrame)
                return new Point(0,0);

            return new Point(charFrame.Left, charFrame.Y);
        }
    }
}
