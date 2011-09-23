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

    public partial class Form1 : GraphicWindow
    {
        public Form1()
            :base("Text Tour", 10, 10, 800, 600)
        {
            SingleLineTextEditor textEditor = new SingleLineTextEditor("texteditor", 10, 10, 700, 36);
            AddGraphic(textEditor);


            Win32Caret.Current.FocusWindow = this;
            //Win32Caret.Current.SetShape(1, fFontSize.Height);
            //Win32Caret.Current.MoveTo(fCaretStartPosition.X, fCaretStartPosition.Y);
            //Win32Caret.Current.Show();

            BackgroundColor = RGBColor.LtGray;
        }


        //public override void OnKeyDown(KeyboardActivityArgs ke)
        //{
        //    switch (ke.VirtualKeyCode)
        //    {
        //        case VirtualKeyCodes.Up:
        //            fCaretPosition.Y -= fFontSize.Height;
        //            if (fCaretPosition.Y < fCaretStartPosition.Y)
        //                fCaretPosition.Y = fCaretStartPosition.Y;
        //            Win32Caret.Current.MoveTo(fCaretPosition.X, fCaretPosition.Y);
        //            break;

        //        case VirtualKeyCodes.Left:
        //            fCharacterPosition--;
        //            if (fCharacterPosition < fCurrentTextRun.IndexOfFirstCharacter)
        //                fCharacterPosition = fCurrentTextRun.IndexOfFirstCharacter;
        //            fCaretPosition = fCurrentTextRun.GetLeadingPositionOfIndexedCharacter(fCharacterPosition);
        //            Win32Caret.Current.MoveTo(fCaretPosition.X, fCaretPosition.Y);
        //            break;

        //        case VirtualKeyCodes.Right:
        //            // If there are no characters in the run, 
        //            // just return
        //            if (0 == fCurrentTextRun.Length)
        //                return;

        //            // If we're already sitting past the last character
        //            // just return
        //            if (fCurrentTextRun.IndexOfLastCharacter == fCharacterPosition-1)
        //                return;

        //            // Otherwise, increment the character position
        //            // and move to that space
        //            fCharacterPosition++;
        //            if (fCharacterPosition > fCurrentTextRun.IndexOfLastCharacter)
        //                fCharacterPosition = fCurrentTextRun.IndexOfLastCharacter+1;
        //            fCaretPosition = fCurrentTextRun.GetTrailingPositionOfIndexedCharacter(fCharacterPosition - 1);
        //            Win32Caret.Current.MoveTo(fCaretPosition.X, fCaretPosition.Y);
        //            break;

        //        case VirtualKeyCodes.Insert:
        //            // Switch editing mode
        //            if (EditMode.Insert == fEditMode)
        //            {
        //                fEditMode = EditMode.Overwrite;
        //                Win32Caret.Current.SetShape(fFontSize.Width / 2, fFontSize.Height);
        //                Win32Caret.Current.Show();
        //            }
        //            else
        //            {
        //                fEditMode = EditMode.Insert;
        //                Win32Caret.Current.SetShape(1, fFontSize.Height);
        //                Win32Caret.Current.Show();
        //            }
        //            break;

        //        case VirtualKeyCodes.Delete:
        //            DeleteNextCharacter();
        //            Invalidate();

        //            break;
        //    }
        //}

        //// Delete the character that is at
        //// the current character position
        //public void DeleteNextCharacter()
        //{
        //    if (fCharacterPosition == fCurrentTextRun.Length)
        //        return;

        //    fCurrentTextRun.DeleteCharacter(fCharacterPosition);
        //    Invalidate();
        //}

        //public override void OnKeyPress(KeyboardActivityArgs ke)
        //{
        //    Console.Write("{0}", ke.Character);

        //    // Deal with characters that are not mode dependent
        //    switch (ke.Character)
        //    {
        //        case '\b':      // Backspace
        //            // Delete the character that is one position less than 
        //            // the current character position
        //            if (fCharacterPosition == fCurrentTextRun.IndexOfFirstCharacter)
        //                return;

        //            fCharacterPosition--;
        //            fCurrentTextRun.DeleteCharacter(fCharacterPosition);
        //            fCaretPosition = fCurrentTextRun.GetTrailingPositionOfIndexedCharacter(fCharacterPosition - 1);
        //            Win32Caret.Current.MoveTo(fCaretPosition.X, fCaretPosition.Y);
        //            Invalidate();
        //            return;

        //        case '\r':      // Return
        //            fCaretPosition.Y += fFontSize.Height;
        //            fCaretPosition.X = fCaretStartPosition.X;
        //            Win32Caret.Current.MoveTo(fCaretPosition.X, fCaretPosition.Y);
        //            return;
        //    }

        //    if (EditMode.Insert == fEditMode)
        //    {
        //        switch (ke.Character)
        //        {
        //            default:
        //                {
        //                    fCurrentTextRun.InsertCharacter(fCharacterPosition, ke.Character);
        //                    Invalidate();
        //                    fCaretPosition = fCurrentTextRun.GetTrailingPositionOfIndexedCharacter(fCharacterPosition);
        //                    Win32Caret.Current.MoveTo(fCaretPosition.X, fCaretPosition.Y);
        //                    fCharacterPosition++;
        //                    Invalidate();

        //                }
        //                break;
        //        }
        //    }

        //    if (EditMode.Overwrite == fEditMode)
        //    {
        //        switch (ke.Character)
        //        {
        //            default:
        //                {
        //                    // Delete the character at the current position
        //                    DeleteNextCharacter();

        //                    // Then insert the new character at the same position
        //                    // and advance.
        //                    fCurrentTextRun.InsertCharacter(fCharacterPosition, ke.Character);
        //                    fCaretPosition = fCurrentTextRun.GetTrailingPositionOfIndexedCharacter(fCharacterPosition);
        //                    Win32Caret.Current.MoveTo(fCaretPosition.X, fCaretPosition.Y);
        //                    fCharacterPosition++;
        //                    Invalidate();
        //                }
        //                break;
        //        }
        //    }
        //}

        //public override void OnMouseUp(MouseActivityArgs e)
        //{
        //    Console.WriteLine("{0}: {1},{2} ", e.ButtonActivity.ToString(), e.X, e.Y);

        //    if (e.ButtonActivity == MouseButtonActivity.LeftButtonUp)
        //    {
        //        Win32Caret.Current.FocusWindow = this;
        //        fFontSize = fFont.MeasureString("W");
        //        Win32Caret.Current.SetShape(1, fFontSize.Height);
        //        Win32Caret.Current.MoveTo(e.X, e.Y);
        //        Win32Caret.Current.Show();
        //        fCaretPosition.X = e.X;
        //        fCaretPosition.Y = e.Y;
        //    }
        //    else if (e.ButtonActivity == MouseButtonActivity.RightButtonUp)
        //    {
        //        Win32Caret.Current.FocusWindow = null;
        //    }
        //}
    }
}
