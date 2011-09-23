using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.UI;
using TOAPI.Types;

namespace PLC
{
	/// <summary>
	/// This view is meant to be a view that covers an entire window
	/// It doesn't do much more than exist the size of the window
	/// And has the opportunity to act as the containing view for 
	/// a bunch of drawing.
	/// </summary>
	public class PLCView : ActiveArea
	{
		XmlNode fXmlData;
		ImageTransition fTransitionAnimator;
        GDIDIBSection fBackingPixelBuffer;
		IGraphPort fBackingGraphics;
		string fHeading;
		PLCDataBinder fDataBinder;
		PLCColorScheme fCS;
		IGraphic fLegend;

		string fEditionName;
		IGraphic fTeamLogo;
		GraphicGroup fTeamLogoBox;

		// Displaying content
		//ContentBrowser fContentBrowser;

		public PLCView()
			:this("PLCView", string.Empty,0,0,100,100)
		{
		}

		public PLCView(string name, string heading, int x, int y, int width, int height)
			: base(name, x, y, width, height)
		{
			//this.GraphicsUnit = GraphicsUnit.Inch;

			fTeamLogo = new TextBox("Team Logo","Tahoma",14,GDIFont.FontStyle.Regular,0,0,160,60,RGBColor.Black);
			fLegend = null;
			fCS = new PLCColorScheme();
			//fTransitionAnimator = new CrossFade();
			fDataBinder = null;
			fBackingPixelBuffer = new GDIDIBSection(width, height);
			fBackingGraphics = fBackingPixelBuffer.GraphPort;
			//fBackingGraphics.PageUnit = GraphicsUnit.Inch;

			fHeading = heading;

			// These are the set of virtual calls
			// That any subclasser will implement
			AddMasterHeading();
			AddPhaseArrows();
			AddPhaseHeadings();
			AddStageHeadings();
			AddDeliveryContainers();
			AddImages();
			AddLegend();
			AddTeamLogo();

			//DrawSelf(fBackingGraphics);
		}

        public GDIDIBSection CurrentImage
		{
			get
			{
                fBackingPixelBuffer.DeviceContext.ClearToWhite();
                DrawEvent de = new DrawEvent(fBackingGraphics, Frame);
                Draw(de);

				return fBackingPixelBuffer;
			}
		}

		public GDIDIBSection BackingImage
		{
			get { return fBackingPixelBuffer; }
		}

		public IGraphPort BackingGraphics
		{
			get { return fBackingGraphics; }
		}

		public virtual PLCDataBinder DataBinder
		{
			get
			{
				if (fDataBinder == null)
					fDataBinder = new PLCDataBinder(this);
				return fDataBinder;
			}
		}

        public virtual ImageTransition PreferredTransition
        {
            get { return fTransitionAnimator; }
            set { fTransitionAnimator = value; }
        }

		public virtual PLCColorScheme CS
		{
			get { return fCS; }
		}

		public virtual IGraphic Legend
		{
			get {return fLegend;}
			set 
			{
				fLegend = value;
				AddGraphic(fLegend);
			}
		}

		public virtual IGraphic TeamLogo
		{
			get {return fTeamLogo;}
			set 
			{
				fTeamLogo = value;
				fTeamLogoBox.RemoveAllGraphics();
				fTeamLogoBox.AddGraphic(fTeamLogo);
			}
		}

		public virtual string Edition
		{
			get {return fEditionName;}
			set {
				fEditionName = value;
				TeamLogo = new TextBox(fEditionName,"Tahoma",1400,GDIFont.FontStyle.Regular,0,0,160,60,RGBColor.Black);
			}
		}

		public XmlNode XmlData
		{
			get {return fXmlData;}

			set {
				fXmlData = value;
				System.Xml.XmlNodeReader reader = new XmlNodeReader(fXmlData);
				LoadXML(reader);
			}
		}

		public override void Draw(DrawEvent devent)
		{
            IGraphPort graphPort = devent.GraphPort;

            //Size sizer = graphPort.VisibleClipBounds.Size;
            //graphPort.PageUnit = this.GraphicsUnit;

			// Create page scaling
			//float scale = Math.Min(sizer.Width/graphPort.DpiX / 10.0F, sizer.Height/graphPort.DpiY / 7.5F);
			
            //float scale = Math.Min(sizer.Width / 10.0F, sizer.Height/ 7.5F);
			//Console.WriteLine("Scale: {0}", scale);
			//graphPort.PageScale = scale;

			
			base.Draw(devent);
			
		}

		// Do the spinning wheel animation
		public virtual void Animate()
		{
		}

		void diagramHeading_MouseDown(object sender, EventArgs e)
		{
			Animate();
		}

		public virtual void AddMasterHeading()
		{
			HeadingButton diagramHeading = new HeadingButton(fHeading, "Verdana", 24, GDIFont.FontStyle.Bold, 0, 24, 1000, 67, RGBColor.Black);
			diagramHeading.MouseDownEvent += new EventHandler(diagramHeading_MouseDown);

			AddGraphic(diagramHeading);
		}


		public virtual void AddPhaseHeadings()
		{
		}

		public virtual void AddStageHeadings()
		{
		}

		public virtual void AddPhaseArrows()
		{
		}

		public virtual void AddDeliverablesHeadings()
		{
		}

		public virtual void AddDeliveryContainers()
		{
		}

		public virtual void AddImages()
		{
            GraphicImage eeglogo = GraphicImage.CreateFromResource("Resources.eeglogo.png", 797, 688, 169, 28);
			AddGraphic(eeglogo);
		}

		public virtual void AddLegend()
		{
			AddGraphic(Legend);
		}

		public virtual void AddTeamLogo()
		{
			fTeamLogoBox = new GraphicGroup("teamlogo",797, 573,169,88);
            //fTeamLogoBox.LayoutHandler = new LinearLayout(fTeamLogoBox, 1, 1, Orientation.Horizontal);

			AddGraphic(fTeamLogoBox);
			fTeamLogoBox.AddGraphic(TeamLogo);
		}


		public virtual void LoadXML(XmlReader reader)
		{
			// First clear all the deliverables
			ClearDeliverables();

			// Now use the data binder to load the data
			DataBinder.LoadXmlData(reader);
		}

		public virtual void LoadFile(string filename)
		{
			// First clear all the deliverables
			ClearDeliverables();

			// Now use the data binder to load the data
			DataBinder.LoadFile(filename);
		}

		public DeliverablesList FindDeliverablesList(string name)
		{
			// Find the graphic with this label
			DeliverablesList aList = null;
			IDrawable aDrawable = this.GraphicNamedRecurse(name);
			IGraphic aGraphic = (IGraphic)aDrawable;
			if (aGraphic is DeliverablesList)
				aList = (DeliverablesList)aGraphic;

			return aList;
		}

		public void ClearDeliverable(string name)
		{
			DeliverablesList aList = FindDeliverablesList(name);

			if (aList != null)
				aList.Clear();

		}

		public virtual void ClearDeliverables()
		{
			foreach (IDrawable d in GraphicChildren)
			{
				if (d is DeliverablesList)
					((DeliverablesList)d).Clear();
			}
		}


		public virtual void Save(string tofile)
		{
			string extension = Path.GetExtension(tofile);

			switch (extension)
			{
				case ".xml":
					SaveData(tofile);
					break;

				case ".html":
				case ".htm":
					SaveInterface(tofile);
					break;

				default:
					//CurrentImage.Save(tofile);
					break;
			}
		}

		public virtual void SaveData(string tofile)
		{
			// Just testing to see what it looks like
			System.Xml.XmlNodeReader reader = new XmlNodeReader(XmlData);
			XmlTextWriter writer = new XmlTextWriter(tofile,System.Text.Encoding.UTF8);
			//writer.Indentation = 2;
			//writer.IndentChar = ' ';
			writer.WriteNode(reader,false);
			writer.Close();
		}

		public virtual void SaveInterface(string tofile)
		{
			// Save out the interface
/*
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
*/
			string filename;
			if (tofile == string.Empty)
				filename = Name + ".xml";
			else
				filename = tofile;

			//XmlWriter writer = XmlWriter.Create(filename, settings);
			XmlTextWriter writer = new XmlTextWriter(filename,System.Text.Encoding.UTF8);

			//fPLCView.WriteXml(writer);

			// Using XML Serialization
			//XmlSerializer ser = new XmlSerializer(typeof (PLCView));
			XmlSerializer ser = new XmlSerializer(GetType());

			ser.Serialize(writer, this);

			writer.Close();
		}
	}
}