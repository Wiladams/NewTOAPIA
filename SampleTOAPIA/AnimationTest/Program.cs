using System;
using System.Media;
using Papyrus;
using Papyrus.Animation;

namespace AnimationTest
{


    class AnimationTestApp : Application
    {
        GDIBitmap fScreenImage;
        GDIBitmap fBlankImage;
        GDIBitmap fPictureImage;
        GDIBitmap fPicture;
        SoundPlayer fPlayer;

        protected override void Initialize()
        {            
            // Setup a couple of bitmaps
            // First make a bitmap to capture the current screen image
            RectangleI rect = DesktopView.MainDesktopView.Frame;
            fScreenImage = new GDIBitmap("image1", 0, 0, rect.Width, rect.Height);
            GDIRenderer.BitBlt(fScreenImage.GraphDevice.DeviceContext, 0, 0, rect.Width, rect.Height,
                DesktopView.MainDesktopView.GraphDevice.DeviceContext,
                0, 0, TernaryRasterOps.SRCCOPY);

            // Then make a bitmap we'll draw transitions onto
            fBlankImage = new GDIBitmap("image2", 0, 0, rect.Width, rect.Height);
            fBlankImage.ClearToWhite();

            // Load a picture
            fPicture = new GDIBitmap("LEAP.jpg",0,0);
            fPictureImage = new GDIBitmap("leap", 0, 0, rect.Width, rect.Height);
            int xoff = (rect.Width - fPicture.Width) / 2;
            int yoff = (rect.Height - fPicture.Height) / 2;
            fPictureImage.GraphDevice.DrawImage(fPicture,xoff,yoff,fPicture.Width,fPicture.Height);

            // Load some sound to play
            fPlayer = new SoundPlayer("c:\\media\\sounds\\BlueSkyMine.wma");
            fPlayer.Play();
            Animate();

            this.Quit();
        }

        public void Animate()
        {

            //PlaySound("soundtrack.wma", IntPtr.Zero, PlaySoundFlags.SND_ASYNC);

            // Run through some animations
            CrossFade animation1 = new CrossFade(fBlankImage, fPictureImage, fBlankImage.Frame, 5, 200);
            animation1.Run(DesktopView.MainDesktopView.GraphDevice);

            // Clear bitmap before next animation
            //fBitmap2.ClearToWhite();

            CoverDown animation2 = new CoverDown(fBlankImage, fScreenImage, fBlankImage.Frame, 200);
            animation2.Run(DesktopView.MainDesktopView.GraphDevice);

            // Clear bitmap before next animation
            fBlankImage.ClearToWhite();

            ExpandVerticalOut animation3 = new ExpandVerticalOut(fPictureImage, fBlankImage.Frame, 250);
            animation3.Run(DesktopView.MainDesktopView.GraphDevice);
        
        }

        [System.Runtime.InteropServices.DllImport("winmm.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        private static extern bool PlaySound(string szSound, IntPtr hMod, PlaySoundFlags flags);

        [System.Flags]
        public enum PlaySoundFlags : int
        {
            SND_SYNC = 0x0000,
            SND_ASYNC = 0x0001,
            SND_NODEFAULT = 0x0002,
            SND_LOOP = 0x0008,
            SND_NOSTOP = 0x0010,
            SND_NOWAIT = 0x00002000,
            SND_FILENAME = 0x00020000,
            SND_RESOURCE = 0x00040004
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AnimationTestApp app = new AnimationTestApp();
            app.Run();
        }
    }
}