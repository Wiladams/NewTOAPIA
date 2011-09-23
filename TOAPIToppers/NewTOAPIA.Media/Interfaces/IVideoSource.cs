using System;


namespace NewTOAPIA.Media
{

    public delegate int MediaBufferCallback(double SampleTime, IntPtr pBuffer, int BufferLen);
    
    /// <summary>
	/// IVideoSource interface
	/// </summary>
	public interface IVideoSource
	{
        /// <summary>
		/// New frame event - notify client about the new frame
		/// </summary>
		event CameraEventHandler NewFrame;

		/// <summary>
		/// Video source property
		/// </summary>
		string VideoSource{get; set;}

		/// <summary>
		/// Login property
		/// </summary>
		string Login{get; set;}

		/// <summary>
		/// Password property
		/// </summary>
		string Password{get; set;}

		/// <summary>
		/// FramesReceived property
		/// get number of frames the video source received from the last
		/// access to the property
		/// </summary>
		int FramesReceived{get;}

		/// <summary>
		/// BytesReceived property
		/// get number of bytes the video source received from the last
		/// access to the property
		/// </summary>
		int BytesReceived{get;}

		/// <summary>
		/// UserData property
		/// allows to associate user data with an object
		/// </summary>
		object UserData{get; set;}

		/// <summary>
		/// Get state of video source
		/// </summary>
		bool Running{get;}

		/// <summary>
		/// Start receiving video frames
		/// </summary>
		void Start();

		/// <summary>
		/// Stop receiving video frames
		/// </summary>
		void SignalToStop();

		/// <summary>
		/// Wait for stop
		/// </summary>
		void WaitForStop();

		/// <summary>
		/// Stop work
		/// </summary>
		void Stop();
	}
}
