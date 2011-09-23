using System;
using System.IO;
using System.Xml;
using System.Text;

namespace PLC
{
	/// <summary>
	/// This class will read a plc.xml file and turn it into a data model
	/// that can be used to build things.  It basically provides something 
	/// a little better than the plain old DOM interface.
	/// </summary>
	public class ITDataBinder : PLCDataBinder
	{
		public ITDataBinder(PLCView aView)
			: base(aView)
		{ }

		public override string CreateLabel(XmlReader reader)
		{
			// A top level element is inidicated by the absence of the 'ParentContentID'
			// attribute.  So, if we find this attribute, we know the topic is not a top
			// level topic, and the parentID indicates which topic is its parent.
			string parentID = reader.GetAttribute("ParentContentID");
			if (null != parentID)
				return string.Empty;

			string phase = reader.GetAttribute("Phase");
			string stage = reader.GetAttribute("Stage");
			string scope = reader.GetAttribute("Scope");
			string contentType = reader.GetAttribute("ContentType");

			string appendage=string.Empty;
			
			int phaseNumber = 0;
			int stageNumber = 0;

			StringBuilder sb = new StringBuilder();
			
			switch(contentType)
			{
				case "Task":
				case "Deliverable":
					appendage = "dlist";
					break;

				case "Checkpoint":
					appendage = "clist";
					break;
			}

			switch (phase)
			{
				case "Envision":
					phaseNumber = 1;
					break;

				case "Design":
					phaseNumber = 2;
					break;

				case "Build":
					phaseNumber = 3;
					break;
				case "Stabilize":
					phaseNumber = 4;
					break;
				case "Deploy":
					phaseNumber = 5;
					break;
				case "Production":
					phaseNumber = 6;
					break;
			}


			switch(stage)
			{
				default:
					stageNumber = 1;
					break;
			}

			sb.Append("p");
			sb.Append(phaseNumber.ToString());
			sb.Append("s");
			sb.Append(stageNumber.ToString());
			sb.Append(appendage);

			return sb.ToString();
		}
	}
}