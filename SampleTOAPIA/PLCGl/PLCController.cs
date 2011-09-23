using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLC
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.GL;
    using NewTOAPIA.UI;

    public enum EditionType
    {
        Hardware,
        Software,
        IT
    }

    public class PLCController : GLController
    {
        PLCView fPLCView;
        PLCHardwareView fPLCHardwareView;
        PLCSoftwareView fPLCSoftwareView;
        PLCITView fPLCITView;

        public PLCController()
        {
            CreatePanels();
        }

        void CreatePanels()
        {
            fPLCSoftwareView = new PLCSoftwareView();
            //fPLCSoftwareView.Window = this;

            fPLCHardwareView = new PLCHardwareView();
            //fPLCHardwareView.Window = this;

            fPLCITView = new PLCITView();
            //fPLCITView.Window = this;

            //fPLCView = fPLCITView;
            //fPLCView = fPLCHardwareView;
            fPLCView = fPLCSoftwareView;
            //AddGraphic(fPLCView);
        }

        public override void OnMouseActivity(object sender, NewTOAPIA.UI.MouseActivityArgs me)
        {
            if (me.ActivityType == NewTOAPIA.UI.MouseActivityType.MouseDown)
                OnMouseDown(me);
        }

        public virtual void OnMouseDown(MouseActivityArgs e)
        {
            if (e.Clicks > 1)
            {
                if (fPLCView == fPLCSoftwareView)
                    SwitchToView(fPLCHardwareView);
                else if (fPLCView == fPLCHardwareView)
                {
                    if (null != fPLCITView)
                        SwitchToView(fPLCITView);
                    else
                        SwitchToView(fPLCSoftwareView);
                }
                else
                    SwitchToView(fPLCSoftwareView);
            }
        }

        public void SwitchToView(PLCView srcView)
        {
            // First, if there is an existing graphic, remove
            // it before doing anything else.
            //if (fPLCView != null)
            //    RemoveGraphic(fPLCView);

            // If there is no current view, then just display
            // immediately
            if (fPLCView == null)
            {
                fPLCView = srcView;
                //this.AddGraphic(fPLCView);
            }

            // If the view is blank, then don't do any further processing
            if (srcView == null)
                return;

            // Now cross fade from the destination to the source
            //ImageTransition transition = new UnCoverDown(null, null, System.Drawing.Rectangle.Empty, 0.33);  // Not working
            //ImageTransition transition = new CrossFade(null, null, Rectangle.Empty, 0.5);
            //ImageTransition transition = new PushDown(null, null, Rectangle.Empty, 0.5);
            //ImageTransition transition = new PushUp(null, null, Rectangle.Empty, 0.5);
            //ImageTransition transition = new PushLeft(null, null, Rectangle.Empty, 0.5);
            //ImageTransition transition = new PushRight(null, null, Rectangle.Empty, 0.5);
            //ImageTransition transition = new CoverDown(null, null, Rectangle.Empty, 1.0);
            //ImageTransition transition = new CoverUp(null, null, Rectangle.Empty, 1.0);                       // Not working
            //ImageTransition transition = new CoverLeft(null, null, Rectangle.Empty, 0.33);
            //ImageTransition transition = new CoverRight(null, null, Rectangle.Empty, 0.33);
            //ImageTransition transition = new WipeDown(null, null, Rectangle.Empty, 1.0);
            //ImageTransition transition = new WipeUp(null, null, Rectangle.Empty, 0.5);                    // Not working
            //ImageTransition transition = new WipeLeft(null, null, Rectangle.Empty, 0.33);
            //ImageTransition transition = new WipeRight(null, null, Rectangle.Empty, 0.5);

            ImageTransition transition = srcView.PreferredTransition;

            //AnimateViewTransition(fPLCView, srcView, transition);

            fPLCView = srcView;
            //this.AddGraphic(fPLCView);
        }

        //void AnimateViewTransition(IGraphic dstGraphic, IGraphic srcGraphic, ImageTransition transition)
        //{
        //    // Create the two bitmaps to play with
        //    GDIDIBSection srcPixelBuffer = new GDIDIBSection(ClientRectangle.Width, ClientRectangle.Height);
        //    GDIDIBSection dstPixelBuffer = new GDIDIBSection(ClientRectangle.Width, ClientRectangle.Height);

        //    // Create graphics objects for each bitmap
        //    IGraphPort srcGraphics = srcPixelBuffer.GraphPort;
        //    IGraphPort dstGraphPort = dstPixelBuffer.GraphPort;


        //    // Fill both bitmaps with the background color of the arrow so there
        //    // won't be a disconcerting flicker if the backgroud bitmap background
        //    // starts out as black or white.
        //    srcPixelBuffer.DeviceContext.ClearToWhite();
        //    dstPixelBuffer.DeviceContext.ClearToWhite();

        //    // Draw the currently displayed thing into the destination bitmap
        //    dstGraphic.Draw(new DrawEvent(dstGraphPort, ClientRectangle));


        //    // Draw the invisible one into its bitmap
        //    srcGraphic.Draw(new DrawEvent(srcGraphics, ClientRectangle));

        //    // At this point we have two bitmaps that hold the images of the 
        //    // two graphics we want to swap.




        //    transition.Frame = ClientRectangle;
        //    transition.SourcePixelBuffer = srcPixelBuffer;
        //    transition.DestinationPixelBuffer = dstPixelBuffer;

        //    ClientAreaGraphPort.SaveState();

        //    // Set the graphport scaling appropriately
        //    //DeviceContextClientArea.SetWindowOrigin(WOrg.X, WOrg.Y);
        //    //DeviceContextClientArea.SetWindowExtent(WExt.X, WExt.Y);
        //    //DeviceContextClientArea.SetViewportOrigin(VOrg.X, VOrg.Y);
        //    //DeviceContextClientArea.SetViewportExtent(VExt.X, VExt.Y);

        //    transition.Run(ClientAreaGraphPort);

        //    ClientAreaGraphPort.ResetState();

        //    // Cleanup
        //    srcPixelBuffer.Dispose();
        //    dstPixelBuffer.Dispose();
        //}


        #region Menu Commands
        void MenuSaveAllClick(object obj, EventArgs ea)
        {
            fPLCView.SaveInterface(string.Empty);
        }

        void MenuExitClick(object obj, EventArgs ea)
        {
            Console.WriteLine("MenuExitClick");
        }

        void MenuCutClick(object obj, EventArgs ea)
        {
            Console.WriteLine("MenuCutClick");
        }

        void MenuCopyClick(object obj, EventArgs ea)
        {
            Console.WriteLine("MenuCopyClick");

            //fClientPanel.Copy();
        }

        void MenuPasteClick(object obj, EventArgs ea)
        {
            Console.WriteLine("MenuPasteClick");
        }

        #endregion

        void LoadFile(string filename)
        {
            fPLCView.LoadFile(filename);
            //nmInvalidate();
        }

    }
}
