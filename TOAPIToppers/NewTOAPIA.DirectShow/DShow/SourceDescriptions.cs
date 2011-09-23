
using System;
using System.Xml;
using NewTOAPIA.Media;

namespace NewTOAPIA.DirectShow
{

	/// <summary>
	/// CaptureDeviceDescription
	/// </summary>
	public class CaptureDeviceDescription : IVideoSourceDescription
	{
		// FriendlyName property
		public string Name
		{
			get { return "Connected Capture Device"; }
		}

		// Description property
		public string Description
		{
			get { return "Captures video from any DirectShow VideoCapture device connected to the computer"; }
		}

		// Return settings page
		public IVideoSourcePage GetSettingsPage()
		{
			return new CaptureDeviceSetupPage();
		}

		// Save configuration
		public void SaveConfiguration(XmlTextWriter writer, object config)
		{
			LocalConfiguration cfg = (LocalConfiguration) config;

			if (cfg != null)
			{
				writer.WriteAttributeString("source", cfg.source);
			}
		}

		// Load configuration
		public object LoadConfiguration(XmlTextReader reader)
		{
			LocalConfiguration config = new LocalConfiguration();

			try
			{
				config.source = reader.GetAttribute("source");
			}
			catch (Exception)
			{
			}
			return (object) config;
		}

		// Create video source object
		public IVideoSource CreateVideoSource(object config)
		{
			LocalConfiguration cfg = (LocalConfiguration) config;
			
			if (cfg != null)
			{
				VideoCaptureDevice source = new VideoCaptureDevice();

				source.VideoSource	= cfg.source;
               
				return (IVideoSource) source;
			}
			return null;
		}
	}
}
