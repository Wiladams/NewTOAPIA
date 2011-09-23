using System;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace PLC
{
    using NewTOAPIA;

	/// <summary>
	/// This view is meant to be a view that covers an entire window
	/// It doesn't do much more than exist the size of the window
	/// And has the opportunity to act as the containing view for 
	/// a bunch of drawing.
	/// </summary>
	[XmlRoot("UserInterface")]
	public class PLCITView : PLCView
	{
		ITDataBinder fMyDataBinder;
		ITPhaseControl fActiveControl;
		GraphicArrow fShadowArrow;
		Rectangle fExpansionFrame;

		ITPhaseControl fPhase1Control;
		ITPhaseControl fPhase2Control;
		ITPhaseControl fPhase3Control;
		ITPhaseControl fPhase4Control;
		ITPhaseControl fPhase5Control;
		ITPhaseControl fPhase6Control;

		public PLCITView()
			: this("PLCITView", 0, 0, 1000, 750)
		{ }

		public PLCITView(string name, int x, int y, int width, int height)
			: base(name, "Product Life Cycle - IT", x, y, width, height)
		{
			fMyDataBinder = new ITDataBinder(this);

			Legend = new ITLegend();
			
			fExpansionFrame = new Rectangle(250, 150, 500, 470);
			fShadowArrow = new GraphicArrow("shadowArrow", fExpansionFrame.Left+4, fExpansionFrame.Top+6,
				fExpansionFrame.Width, fExpansionFrame.Height, RGBColor.RGB(128,200, 200), RGBColor.TRANSPARENT);
			fActiveControl = null;

			//PreferredTransition = new StretchRight(0.5);
            PreferredTransition = new StretchUp(0.5);
            //PreferredTransition = new PushRight(null, null, Rectangle.Empty, 0.5);
		}

		public override PLCDataBinder DataBinder
		{
			get
			{
				return fMyDataBinder;
			}
		}


		void DoFlyIn(ITPhaseControl control, int steps)
		{
			// Create a pixmap to represent the control to be animated
            GDIDIBSection srcPixelBuffer = new GDIDIBSection((int)(control.Frame.Width), (int)(control.Frame.Height));
            IGraphPort srcGraphics = srcPixelBuffer.GraphPort;
            srcPixelBuffer.DeviceContext.ClearToWhite();

            // Draw the control into its bitmap
            DrawEvent sde = new DrawEvent(srcGraphics, Frame);
            control.OnDraw(sde);
            srcGraphics.Flush();
            
            // Get the current image of what's being displayed
            GDIDIBSection dstPixelBuffer = this.BackingImage;
            IGraphPort dstGraphics = dstPixelBuffer.GraphPort;
            dstPixelBuffer.DeviceContext.ClearToWhite();

            DrawEvent dde = new DrawEvent(dstGraphics, Frame);
            this.Draw(dde);

			// At this point we have two bitmaps that hold the images of the 
			// background, and the graphic that will fly in.


			// Get a hold of our window's graphics port
            IGraphPort grfx = Window.ClientAreaGraphPort;
            
            // Draw the current destination image into the graphport
            grfx.PixBlt(dstPixelBuffer, 0, 0);


            Rectangle startFrame = new Rectangle(0, (int)(control.Frame.Top), (int)(control.Frame.Width), (int)(control.Frame.Height));
			//Rectangle startFrame = new Rectangle(0, 0, (int)(control.Frame.Width), (int)(control.Frame.Height));
			Rectangle endFrame = new Rectangle((control.Frame.Left), (control.Frame.Top), (control.Frame.Width), (control.Frame.Height));

            FlyIn transition = new FlyIn(startFrame, endFrame, steps);
            transition.SourcePixelBuffer = srcPixelBuffer;
            transition.DestinationPixelBuffer = dstPixelBuffer;

            transition.Run(grfx);
            //FlyAGraphic(grfx, srcPixelBuffer, startFrame, dstPixelBuffer, endFrame, steps);

			// Cleanup
			srcPixelBuffer.Dispose();
		}

        void FlyAGraphic(IGraphPort graphPort, GDIDIBSection srcPixelBuffer, Rectangle startFrame, GDIDIBSection dstPixelBuffer, Rectangle endFrame, int steps)
        {
            double xIncrement = (float)(endFrame.X - startFrame.X) / steps;
            double yIncrement = (float)(endFrame.Y - startFrame.Y) / steps;

            Rectangle lastFrame = startFrame;
            int startX = startFrame.X;
            int startY = startFrame.Y;
            int x;
            int y;

            for (int i = 0; i < steps; i++)
            {
                // Draw a section of the destination pixel buffer to 
                // cover where the graphic was sitting before it draws
                // itself in the new location.
                if (i > 0)
                {
                    graphPort.AlphaBlend(lastFrame.X, lastFrame.Y, srcPixelBuffer.Width, srcPixelBuffer.Height, dstPixelBuffer,
                        lastFrame.X, lastFrame.Y, srcPixelBuffer.Width, srcPixelBuffer.Height, 255);
                }

                x = (int)Math.Floor((double)startX + (i * xIncrement) + 0.5);
                y = (int)Math.Floor((double)startY + (i * yIncrement) + 0.5);


                //aGraphic.MoveTo(x, y);
                
                //DrawEvent devent = new DrawEvent(graphPort, Frame);
                //aGraphic.Draw(devent);
                graphPort.PixBlt(srcPixelBuffer, x, y);
                //graphPort.AlphaBlend(x, y, srcPixelBuffer.Width, srcPixelBuffer.Height, srcPixelBuffer,
                //    0, 0, srcPixelBuffer.Width, srcPixelBuffer.Height, 127);

                lastFrame = new Rectangle(x, y, srcPixelBuffer.Width, srcPixelBuffer.Height);
            }

            //aGraphic.MoveTo(endFrame.X, endFrame.Y);
            graphPort.PixBlt(srcPixelBuffer, endFrame.X, endFrame.Y);

        }

        public override void Animate()
		{
			//Console.WriteLine("PLCITView.Animate()");

			RemoveGraphic(fPhase1Control);
			RemoveGraphic(fPhase2Control);
			RemoveGraphic(fPhase3Control);
			RemoveGraphic(fPhase4Control);
			RemoveGraphic(fPhase5Control);
			RemoveGraphic(fPhase6Control);

            DoFlyIn(fPhase1Control, 300);
            AddGraphic(fPhase1Control);

            DoFlyIn(fPhase2Control, 400);
            AddGraphic(fPhase2Control);

            DoFlyIn(fPhase3Control, 500);
            AddGraphic(fPhase3Control);

            DoFlyIn(fPhase4Control, 600);
            AddGraphic(fPhase4Control);

            DoFlyIn(fPhase5Control, 700);
            AddGraphic(fPhase5Control);

            DoFlyIn(fPhase6Control, 700);
            AddGraphic(fPhase6Control);

            //Window.Invalidate();
		}



		public override void AddPhaseArrows()
		{
			// The trailing arrow
			AddGraphic(new GraphicArrow("trailingArrow", 0, 105, 93, 468));


			// The six phases
			fPhase1Control = new ITPhaseControl("envision", "Envison","p1s1dlist", "p1s1mlist", "p1s1clist",70, 105);
            fPhase2Control = new ITPhaseControl("design", "Design", "p2s1dlist", "p2s1mlist", "p2s1clist", 220, 105);
            fPhase3Control = new ITPhaseControl("build", "Build", "p3s1dlist", "p3s1mlist", "p3s1clist", 370, 105);
            fPhase4Control = new ITPhaseControl("stabilize", "Stabilize", "p4s1dlist", "p4s1mlist", "p4s1clist", 521, 105);
            fPhase5Control = new ITPhaseControl("deploy", "Deploy", "p5s1dlist", "p5s1mlist", "p5s1clist", 670, 105);
            fPhase6Control = new ITPhaseControl("production", "Production", "p6s1dlist", "p6s1mlist", "p6s1clist", 820, 105);
			
			AddGraphic(fPhase1Control);
            AddGraphic(fPhase2Control);
            AddGraphic(fPhase3Control);
            AddGraphic(fPhase4Control);
            AddGraphic(fPhase5Control);
            AddGraphic(fPhase6Control);
		}


		public override void MoveGraphicToFront(IGraphic aDrawable)
		{
			// Shrink the current control back into position
			if (fActiveControl != null)
			{
				RemoveGraphic(fShadowArrow);
				fActiveControl.Shrink();

                IGraphPort graphPort = Window.GraphPort;
                graphPort.PixBlt(CurrentImage, Frame.Left, Frame.Top);

				if (fActiveControl == aDrawable)
				{
					fActiveControl = null;
					return;
				}
			}

			// Move the new graphic to the front in the hierarchy
			if ((aDrawable != null) && (aDrawable is ITPhaseControl))
			{
				AddGraphic(fShadowArrow);
				//base.MoveGraphicToFront(aDrawable);
				fActiveControl = (ITPhaseControl)aDrawable;

				// Expand the new control
				fActiveControl.ExpansionFrame = fExpansionFrame;
				fActiveControl.Expand();

				IGraphPort grfx = Window.GraphPort;
				grfx.PixBlt(this.CurrentImage, Frame.Left,Frame.Top);
			}
		}
	}
}