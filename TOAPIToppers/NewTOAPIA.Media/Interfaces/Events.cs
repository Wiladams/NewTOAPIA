using System;

namespace NewTOAPIA.Media
{
	// NewFrame delegate
	public delegate void CameraEventHandler(object sender, CameraEventArgs e);

	/// <summary>
	/// Camera event arguments
	/// </summary>
	public class CameraEventArgs : EventArgs
	{
        public double fTimeStamp;
        public IntPtr fData;
        public int fSize;
        public int Width;
        public int Height;

        public CameraEventArgs(double timestamp, IntPtr buffer, int size, int width, int height)
        {
            fTimeStamp = timestamp;
            fData = buffer;
            fSize = size;
            Width = width;
            Height = height;
        }
	}
}