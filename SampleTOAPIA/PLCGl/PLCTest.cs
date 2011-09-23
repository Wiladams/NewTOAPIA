using Papyri;
using System;
using System.Collections;

namespace PLC
{
	class PLCTest
	{

		public static void Main(string[] args)
		{
			PLCApplication app = new PLCApplication();
			app.Run();
		}

		/*
		public void SaveAsXml(string filename)
		{
			// This code consumes all processor resources in some infinite loop
			
						// Create an ObjectNavigator
						ObjectXPathNavigator nav = new ObjectXPathNavigator(this);

						// Open up an XmlWriter
						XmlWriter writer = XmlWriter.Create(filename);

						// Feed the navigator to the writer
						writer.WriteNode(nav, false);

						// close the writer
						writer.Close();
			
		}
	*/
	}
}