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
	public class SoftwareDataBinder : PLCDataBinder
	{
		public SoftwareDataBinder(PLCView aView)
			:base(aView)
		{}

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
					switch (scope)
					{
						case "products":
							appendage = "dlist";
						break;

						case "features":
							appendage = "fdlist";
						break;
					}
				break;

				case "Checkpoint":
					switch (scope)
					{
						case "products":
							appendage = "clist";
						break;

						case "features":
							appendage = "fclist";
						break;

						default:
							appendage = "clist";
							break;
					}
					break;
			}



			switch (phase)
			{
				case "Product Definition":
					phaseNumber = 1;
					break;

				case "Product Development":
					phaseNumber = 2;
					break;

				case "Product Servicing":
					phaseNumber = 3;
					break;
			}


			switch(stage)
			{
				case "Requirements":
					stageNumber = 1;
					break;

				case "Design":
					stageNumber = 2;
					break;

				case "Implementation":
					stageNumber = 3;
					break;

				case "Verification":
					stageNumber = 4;
					break;

				case "Release":
					stageNumber = 5;
					break;

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