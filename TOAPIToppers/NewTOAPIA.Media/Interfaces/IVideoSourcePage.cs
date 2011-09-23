using System;

namespace NewTOAPIA.Media
{

	/// <summary>
	/// IVideoSourcePage interface
	/// </summary>
	public interface IVideoSourcePage
	{
		/// <summary>
		/// State changed event - notify client if the page is completed
		/// </summary>
		event EventHandler StateChanged;

		/// <summary>
		/// Completed property
		/// true, if the page is completed and wizard can proceed to next page
		/// </summary>
		bool Completed { get; }

		/// <summary>
		/// Display - display the page
		/// Wizard call the method after the page was shown
		/// </summary>
		void Display();

		/// <summary>
		/// Apply - check and update all variables
		/// Return false if something wrong and we want to stay on the page
		/// </summary>
		bool Apply();

		/// <summary>
		/// Get configuration object
		/// </summary>
		object GetConfiguration();

		/// <summary>
		/// Set configuration
		/// </summary>
		void SetConfiguration(object config);
	}
}
