using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace PLC
{
	/// <summary>
	/// Summary description for PLCPanel.
	/// </summary>
	public class PLCPanel : GraphicPanel
	{
		GraphicPanel	fClientPanel;
		
        PLCView fPLCView;
		PLCSoftwareView fPLCSoftwareView;
        //PLCHardwareView fPLCHardwareView;
        //PLCITView fPLCITView;

		Point WOrg;
		Point WExt;
		Point VOrg;
		Point VExt;

		public PLCPanel()
			:base("PLCPanel", 0,0,1024,768)
		{
			WOrg = new Point(0, 0);
			WExt = new Point(1000, 750);
			VOrg = new Point(0, 0);
			VExt = new Point(1024, 768);

			//
			// TODO: Add constructor logic here
			//
			fClientPanel = new GraphicPanel("clientarea",128,0,1024,768);
			//fClientPanel.Dock = DockStyle.Left;
			//fClientPanel.Parent = this;

			fPLCView = null;

			
			// Create the primary View			
			//fPLCHardwareView = new PLCHardwareView("hardwareview", 0, 0, WExt.X, WExt.Y);
			//fPLCSoftwareView = new PLCSoftwareView("softwareview", 0, 0, WExt.X, WExt.Y);
			//fPLCITView = new PLCITView("itview", 0, 0, WExt.X, WExt.Y);
			fPLCView = fPLCSoftwareView;

			fClientPanel.AddGraphic(fPLCView);


		}

		public string EditionName
		{
			get {return fPLCView.Edition;}
		}

		public void DisplayView(string viewName)
		{
			switch (viewName)
			{
				case "Software":
					SwitchToView(fPLCSoftwareView);
					break;
                //case "Hardware":
                //    SwitchToView(fPLCHardwareView);
                //    break;
                //case "IT":
                //    SwitchToView(fPLCITView);
                //    break;
			}
		}

		public void SaveAs(string filename)
		{
			//Console.WriteLine("PLCPanel.SaveAs");
			fPLCView.Save(filename);
		}

		public void Copy()
		{
			//Console.WriteLine("Copy");

			// Create a memory stream for the metafile
			MemoryStream memStream = new MemoryStream();

			// We need a Windows Device Context for the Metafile
			// creation, so get it from our Window by creating
			// a Graphics object, and then getting the device context
			// from there.
			Graphics grfx = CreateGraphics();
			grfx.PageUnit = GraphicsUnit.Inch;
			IntPtr hDC = grfx.GetHdc();

			// For metadata copying through the clipboard, there is this
			// KB Article: http://support.microsoft.com/?id=323530
			// Now we have enough to creat the metafile object
			Metafile mf = new Metafile(memStream, hDC, EmfType.EmfPlusDual,"A PLC Image");

			// Release the dc, it's no longer needed
			grfx.ReleaseHdc(hDC);
			grfx.Dispose();

			// Now create a new Graphics object so we can draw
			// into the Metafile image
			grfx = Graphics.FromImage(mf);
			grfx.PageUnit = GraphicsUnit.Inch;

			// Now do our drawing
			fPLCView.DrawInto(grfx);

			// We're done drawing, so dispose of the graphics object
			grfx.Dispose();

			// Now copy the metafile to the clipboard and keep it there
			// after the application exits.
			// Once we do this, the metafile is no longer valid
			ClipboardMetafileHelper.PutEnhMetafileOnClipboard(this.Handle, mf );

			// The ideal way to copy to the clipboard is to just call SetDataObject(), 
			// but there is a bug in metafile copying
			// that prevents us from doing this simple single line of code.
			// KB Article: http://support.microsoft.com/?id=323530
			//Clipboard.SetDataObject(mf,true);
			
			// Dispose of the metafile
			mf.Dispose();

			// This is an alternative way to do it.  Just take the current image 
			// and copy it to the clip board.  This isn't as nice as using the metafile
			// as it's not scalable, and it is just a bitmap
			//Clipboard.SetDataObject(fPLCView.CurrentImage,true);
		}

		public void SwitchToView(PLCView srcView)
		{
			// If there is no current view, then just display
			// immediately
			if (fPLCView == null)
			{
				fPLCView = srcView;
				this.RemoveAllGraphics();
				this.AddGraphic(fPLCView);
				Invalidate();
			}

			// If the view is blank, then don't do any further processing
			if (srcView == null)
				return ;

			// Create the two bitmaps to play with

			PixelBuffer srcPixelBuffer = new PixelBuffer(ClientRectangle.Width, ClientRectangle.Height);
			PixelBuffer dstPixelBuffer = new PixelBuffer(ClientRectangle.Width, ClientRectangle.Height);

			// Create graphics objects for each bitmap
			IGraphPort srcGraphics = srcPixelBuffer.GraphPort;
            IGraphPort dstGraphics = dstPixelBuffer.GraphPort;


			// Fill both bitmaps with the background color of the arrow so there
			// won't be a disconcerting flicker if the backgroud bitmap background
			// starts out as black or white.
            srcPixelBuffer.ClearToWhite();
            dstPixelBuffer.ClearToWhite();

			// Draw the currently displayed thing into the destination bitmap
			fPLCView.DrawInto(dstGraphics);


			// Draw the invisible one into its bitmap
			srcView.DrawInto(srcGraphics);

			// At this point we have two bitmaps that hold the images of the 
			// two graphics we want to swap.


			// Now cross fade from the destination to the source
			Graphics grfx = null;
			Form form = this.FindForm();
			if (form != null)
				grfx = form.CreateGraphics();
			else
				grfx = CreateGraphics();

			ImageTransition transition = srcView.PreferredTransition;
			transition.Frame = ClientRectangle;
			transition.SourcePixelBuffer = srcPixelBuffer;
			transition.DestinationPixelBuffer = dstPixelBuffer;
			
			transition.Run(grfx);

			grfx.Dispose();


			
			// Cleanup
			srcPixelBuffer.Dispose();
			dstPixelBuffer.Dispose();

			// swap the references
			fPLCView = srcView;
			
			if (fClientPanel != null)
			{
				fClientPanel.RemoveAllGraphics();
				fClientPanel.AddGraphic(fPLCView);
				fClientPanel.Invalidate();		// Make sure we indicate we want to draw
			}
		}
	}
}
