using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

using TOAPI.GDI32;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;



namespace TextTour
{
    /// <summary>
    /// EditMode determines whether typing will overwrite the existing characters, or
    /// insert new characters into the current position.
    /// </summary>
    public enum EditMode
    {
        Insert,
        Overwrite,
    }

    /// <summary>
    /// SingleLineTextEditor displays, and allows the user to edit a single line
    /// of text in a single font.  The cursor movement keys are operational, as 
    /// well as the 'delete' and 'backspace' keys.  The 'tab' key will have no
    /// effect.
    /// </summary>
    public partial class SingleLineTextEditor : Graphic
    {
        #region Fields
        Point fCaretPosition;       // Where in the window is the caret located
        Point fCaretStartPosition;  // Where is the starting point of the caret
        GDIFont fFont;              // Which font is being used to render the text
        Size fFontSize;             // Width and height of the "M" character in the font.

        int fCharacterPosition;     // An index value indicating the current character position
        EditMode fEditMode;         // Which editing mode are we currently using

        TextRun fCurrentTextRun;    // The TextRun object used to display the text
        #endregion

        #region Constructors
        public SingleLineTextEditor(string name, int x, int y, int width, int height)
            : base(name, x, y, width, height)
        {
            //Debug = true;
            SetEditMode(EditMode.Insert);

            fFont = new GDIFont("Courier", 24, Guid.NewGuid());
            fFontSize = fFont.MeasureString("W");

            fCaretStartPosition = new Point(4, 4);
            fCaretPosition = fCaretStartPosition;
            fCharacterPosition = 0;

            fCurrentTextRun = new TextRun(fFont, fCaretStartPosition.X, fCaretStartPosition.Y, fFontSize.Width, fFontSize.Height);
        }
        #endregion

        protected Point GetLeadingEdgeOfCharacterPosition(int characterPosition)
        {
            Point charPosition = fCurrentTextRun.GetLeadingPositionOfIndexedCharacter(characterPosition);

            float3 tpos = WorldTransform.ApplyForward(new float3(charPosition.X, charPosition.Y, 0));
            charPosition.X = (int)tpos.x;
            charPosition.Y = (int)tpos.y;

            return charPosition;
        }

        protected Point GetTrailingEdgeOfCharacterPosition(int characterPosition)
        {
            Point charPosition = fCurrentTextRun.GetTrailingPositionOfIndexedCharacter(characterPosition);

            float3 tpos = WorldTransform.ApplyForward(new float3(charPosition.X, charPosition.Y, 0));
            charPosition.X = (int)tpos.x;
            charPosition.Y = (int)tpos.y;

            return charPosition;
        }

        public virtual void SelectCharacterPosition(int charPosition)
        {
            fCharacterPosition = charPosition;
            fCaretPosition = GetLeadingEdgeOfCharacterPosition(fCharacterPosition);
                            
            Win32Caret.Current.MoveTo(fCaretPosition.X, fCaretPosition.Y);
        }

        #region IGraphic
        public override void DrawBackground(DrawEvent devent)
        {
            devent.GraphPort.DrawRoundRect(CosmeticPen.Black, new Rectangle(0, 0, Dimension.Width, Dimension.Height), 2, 2);
        }

        public override void DrawSelf(DrawEvent devent)
        {
            devent.GraphPort.SetFont(fFont);
            //devent.GraphPort.SetBkMode((int)BackgroundMixMode.Transparent);
            devent.GraphPort.SetTextColor(RGBColor.Black);
            fCurrentTextRun.Draw(devent);
        }

        public override void OnKeyDown(KeyboardActivityArgs ke)
        {
            //Console.WriteLine("SingleLineTextEditor.OnKeyDown: {0}", ke);

            switch (ke.VirtualKeyCode)
            {
                case VirtualKeyCodes.Up:
                    fCaretPosition.Y -= fFontSize.Height;
                    if (fCaretPosition.Y < fCaretStartPosition.Y)
                        fCaretPosition.Y = fCaretStartPosition.Y;
                    Win32Caret.Current.MoveTo(fCaretPosition.X, fCaretPosition.Y);
                    break;

                case VirtualKeyCodes.Left:
                    fCharacterPosition--;
                    if (fCharacterPosition < fCurrentTextRun.IndexOfFirstCharacter)
                        fCharacterPosition = fCurrentTextRun.IndexOfFirstCharacter;

                    SelectCharacterPosition(fCharacterPosition);
                    break;

                case VirtualKeyCodes.Right:
                    // If there are no characters in the run, 
                    // just return
                    if (0 == fCurrentTextRun.Length)
                        return;

                    // If we're already sitting past the last character
                    // just return
                    if (fCurrentTextRun.IndexOfLastCharacter == fCharacterPosition - 1)
                        return;

                    // Otherwise, increment the character position
                    // and move to that space
                    fCharacterPosition++;
                    if (fCharacterPosition > fCurrentTextRun.IndexOfLastCharacter)
                        fCharacterPosition = fCurrentTextRun.IndexOfLastCharacter + 1;

                    SelectCharacterPosition(fCharacterPosition);
                    break;

                case VirtualKeyCodes.Insert:
                    // Switch editing mode
                    if (EditMode.Insert == fEditMode)
                    {
                        SetEditMode(EditMode.Overwrite);
                    }
                    else
                    {
                        SetEditMode(EditMode.Insert);
                    }
                    break;

                case VirtualKeyCodes.Delete:
                    DeleteNextCharacter();
                    Invalidate();

                    break;
            }
        }

        public override void OnMouseUp(MouseActivityArgs e)
        {
            Console.WriteLine("{0}: {1},{2} ", e.ButtonActivity.ToString(), e.X, e.Y);

            if (e.ButtonActivity == MouseButtonActivity.LeftButtonUp)
            {
                Win32Caret.Current.FocusWindow = this.Window;
                fFontSize = fFont.MeasureString("W");
                Win32Caret.Current.SetShape(1, fFontSize.Height);
                Win32Caret.Current.MoveTo(e.X, e.Y);
                Win32Caret.Current.Show();
                fCaretPosition.X = e.X;
                fCaretPosition.Y = e.Y;
            }
            else if (e.ButtonActivity == MouseButtonActivity.RightButtonUp)
            {
                Win32Caret.Current.FocusWindow = null;
            }
        }

        public override void OnKeyPress(KeyboardActivityArgs ke)
        {
            //Console.Write("{0}", ke.Character);

            // Deal with characters that are not mode dependent
            switch (ke.Character)
            {
                case '\b':      // Backspace
                    // Delete the character that is one position less than 
                    // the current character position
                    if (fCharacterPosition == fCurrentTextRun.IndexOfFirstCharacter)
                        return;

                    fCharacterPosition--;
                    fCurrentTextRun.DeleteCharacter(fCharacterPosition);
                    SelectCharacterPosition(fCharacterPosition);
                    //fCaretPosition = fCurrentTextRun.GetTrailingPositionOfIndexedCharacter(fCharacterPosition - 1);
                    //Win32Caret.Current.MoveTo(fCaretPosition.X, fCaretPosition.Y);
                    Invalidate();
                    return;

                case '\r':      // Return
                    fCaretPosition.Y += fFontSize.Height;
                    fCaretPosition.X = fCaretStartPosition.X;
                    Win32Caret.Current.MoveTo(fCaretPosition.X, fCaretPosition.Y);
                    return;
            }

            if (EditMode.Insert == fEditMode)
            {
                switch (ke.Character)
                {
                    default:
                        {
                            fCurrentTextRun.InsertCharacter(fCharacterPosition, ke.Character);
                            SelectCharacterPosition(fCharacterPosition + 1);

                            Invalidate();
                        }
                        break;
                }
            }

            if (EditMode.Overwrite == fEditMode)
            {
                switch (ke.Character)
                {
                    default:
                        {
                            // Delete the character at the current position
                            DeleteNextCharacter();

                            // Then insert the new character at the same position
                            // and advance.
                            fCurrentTextRun.InsertCharacter(fCharacterPosition, ke.Character);
                            fCaretPosition = fCurrentTextRun.GetTrailingPositionOfIndexedCharacter(fCharacterPosition);
                            Win32Caret.Current.MoveTo(fCaretPosition.X, fCaretPosition.Y);
                            fCharacterPosition++;
                            Invalidate();
                        }
                        break;
                }
            }
        }

        protected override void OnGainedFocus()
        {
            Win32Caret.Current.FocusWindow = Window;

            SetEditMode(fEditMode);

            fCharacterPosition = 0;

            SelectCharacterPosition(fCharacterPosition);

            Win32Caret.Current.Show();
        }

        protected override void OnLostFocus()
        {
            base.OnLostFocus();
        }
        #endregion

        public virtual void SetEditMode(EditMode newMode)
        {
            fEditMode = newMode;

            if (EditMode.Overwrite == fEditMode)
            {
                Win32Caret.Current.SetShape(fFontSize.Width / 2, fFontSize.Height);
            }
            else
            {
                Win32Caret.Current.SetShape(1, fFontSize.Height);
            }
            Win32Caret.Current.Show();
        }

        // Delete the character that is at
        // the current character position
        public void DeleteNextCharacter()
        {
            if (fCharacterPosition == fCurrentTextRun.Length)
                return;

            fCurrentTextRun.DeleteCharacter(fCharacterPosition);
            Invalidate();
        }


    }
}
