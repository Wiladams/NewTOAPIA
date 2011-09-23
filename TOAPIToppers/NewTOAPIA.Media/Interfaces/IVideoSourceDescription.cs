
using System;
using System.Xml;

namespace NewTOAPIA.Media
{

	/// <summary>
	/// IVideoSourceDescription interface - description of video source
	/// </summary>
	public interface IVideoSourceDescription
	{
		/// <summary>
		/// Name property
		/// </summary>
		string Name{get;}

		/// <summary>
		/// Description property
		/// </summary>
		string Description{get;}

		/// <summary>
		/// Return settings page
		/// </summary>
		IVideoSourcePage GetSettingsPage();

		/// <summary>
		/// Save configuration
		/// </summary>
		void SaveConfiguration(XmlTextWriter writer, object config);

		/// <summary>
		/// Load configuration
		/// </summary>
		object LoadConfiguration(XmlTextReader reader);

		/// <summary>
		/// Create video source object
		/// </summary>
		IVideoSource CreateVideoSource(object config);
	}
}
