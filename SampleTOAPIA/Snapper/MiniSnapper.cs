using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms; // For clipboard object

using NewTOAPIA;
using NewTOAPIA.UI;
using NewTOAPIA.Drawing;

using TOAPI.User32;     // For User32.ClientToScreen
using TOAPI.Types;

public class SnapperWindow : Window
{
    bool fHaveImage;            // Whether we have a current image or not
    GDIDIBSection fScreenImage;   // The last snapped image
    ScreenSnapper fSnapper;     // The snapshot object

    public SnapperWindow()
        : base("Snapper", 10, 10, 320, 110)
    {
        fSnapper = new ScreenSnapper();

        BackgroundColor = RGBColor.Yellow;
        this.Opacity = 0.5;
    }

    // Don't look at the following unless you're trying to snapshot a specific window
    //void fMouse_MouseActivityEvent(object sender, MouseActivityArgs me)
    //{
    //    if (me.EventType == MouseEventType.MouseUp && me.ButtonActivity == MouseButtonActivity.RightButtonUp)
    //    {
    //        // Find out where the cursor is
    //        Point currentPosition = Win32Cursor.Current.Location;

    //        // find the window for that position
    //        IntPtr winHandle = TOAPI.User32.User32.WindowFromPoint(currentPosition.X, currentPosition.Y);

    //        if (IntPtr.Zero != winHandle)
    //        {
    //            // Get the window's frame
    //            TOAPI.Types.RECT rect;
    //            TOAPI.User32.User32.GetWindowRect(winHandle, out rect);

    //            // get the device context for the window
    //            GDIContext winContext = GDIContext.CreateForWholeWindow(winHandle);

    //            // hand the context to the snapper
    //            fSnapper.Context = winContext;

    //            // snap a picture
    //            fScreenImage = fSnapper.SnapAPicture(new Rectangle(0, 0, rect.Width, rect.Height));
    //            fHaveImage = true;

    //            CopyToClipboard();
    //            Invalidate();
    //        }
    //    }
    //}

    void CopyToClipboard()
    {
        if (!fHaveImage)
            return;

        Bitmap bm = PixelBufferHelper.CreateBitmapFromPixelArray(fScreenImage);

        Clipboard.SetImage(bm);
    }

    public override void OnPaint(DrawEvent devent)
    {
        if (fHaveImage && (null != fScreenImage))
        {
            Rectangle cRect = ClientRectangle;

            // If we have an image, just draw it to the screen
            devent.GraphPort.AlphaBlend(0, 0, cRect.Width, cRect.Height, 
                fScreenImage, 0, 0, fScreenImage.Width, fScreenImage.Height, 255);
        }
    }

    public override void OnMouseDown(MouseActivityArgs e)
    {
        if (e.ButtonActivity == MouseButtonActivity.RightButtonDown)
        {
            fHaveImage = false;
            Opacity = 0.5;
        }

        if (e.ButtonActivity == MouseButtonActivity.LeftButtonDown)
        {
            if (e.Clicks > 1)
            {
                this.Hide();
                POINT origin = new POINT(0, 0);
                User32.ClientToScreen(Handle, ref origin);
                fScreenImage = fSnapper.SnapAPicture(new Rectangle(origin.X, origin.Y, ClientRectangle.Width, ClientRectangle.Height));
                fHaveImage = true;
                CopyToClipboard();
                this.Show();
            }
        }
        Invalidate();
    }

    public override void OnSetFocus()
    {
        if (fHaveImage)
            Opacity = 1;
    }

    public override void OnKillFocus()
    {
        Opacity = 0.5;
    }

}
