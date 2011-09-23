using System;
using System.IO;
using System.Xml;
using System.Text;

using NewTOAPIA.UI;

namespace PLC
{
	/// <summary>
	/// This class will read a plc.xml file and turn it into a data model
	/// that can be used to build things.  It basically provides something 
	/// a little better than the plain old DOM interface.
	/// </summary>
	public class PLCDataBinder
	{
		PLCView fView;

		public PLCDataBinder()
			: this(null)
		{ }

		public PLCDataBinder(PLCView aView)
		{
			fView = aView;
		}

		public PLCView BindingView
		{
			get { return fView; }
		}

		public virtual void LoadFile(string filename)
		{
			//XmlReader reader = XmlReader.Create(filename);
			XmlReader reader = new XmlTextReader(filename);
			
			LoadXmlData(reader);

			reader.Close();
		}

		public virtual void LoadXmlData(XmlReader reader)
		{
			bool logoAdded = false;

			// Walk the top level getting a start to each phase
			while (reader.Read())
			{
				if  (reader.NodeType == XmlNodeType.Element)
				{
					switch (reader.Name)
					{
						// Now add deliverable items
						case "topic":
							// Add the name of the edition to the form
							// If we had an actual team logo, it would be 
							// nice to display that.
							if (!logoAdded)
							{
								// get the name of the addition
								// and set that as the edition name for the view
								BindingView.Edition = reader.GetAttribute("Edition");
								logoAdded = true;
							}

							// Now Now just add the activity.
							//Console.WriteLine("{0} - {1}", fReader.Name, fReader.GetAttribute("Title"));
							AddDeliverable(reader);
							
							// Skip may not be necessary given the flat nature of the content
							//reader.Skip();
							break;
					}
				}
			}
		}
		
		public virtual string CreateLabel(XmlReader reader)
		{
			string phase = reader.GetAttribute("Phase");
			string stage = reader.GetAttribute("Stage");

			StringBuilder sb = new StringBuilder();

			sb.Append(phase);
			sb.Append(stage);
			sb.Append("dlist");

			return sb.ToString();
		}


		public virtual DeliverablesList FindDeliverablesList(string name)
		{
			// Find the graphic with this label
			DeliverablesList aList = null;
			IDrawable aDrawable = BindingView.GraphicNamedRecurse(name);
			if (aDrawable is DeliverablesList)
				aList = (DeliverablesList)aDrawable;

			return aList;
		}

		public virtual void AddDeliverable(XmlReader reader)
		{

			string label = CreateLabel(reader);

			// Find the graphic with this label
			DeliverablesList aList = FindDeliverablesList(label);

			// Assuming it's a deliverablelist, add the deliverable to it.
			if (aList != null)
				aList.AddDeliverable(reader.GetAttribute("Title"));
		}
	}
}