namespace NewTOAPIA.DirectShow
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    sealed public class DsError
    {
        private DsError()
        {
            // Prevent people from trying to instantiate this class
        }

        [DllImport("quartz.dll", CharSet = CharSet.Auto)]
        public static extern int AMGetErrorText(int hr, StringBuilder buf, int max);

        /// <summary>
        /// If hr has a "failed" status code (E_*), throw an exception.  Note that status
        /// messages (S_*) are not considered failure codes.  If DirectShow error text
        /// is available, it is used to build the exception, otherwise a generic com error
        /// is thrown.
        /// </summary>
        /// <param name="hr">The HRESULT to check</param>
        public static void ThrowExceptionForHR(int hr)
        {
            // If a severe error has occurred
            if (hr < 0)
            {
                string s = GetErrorText(hr);

                // If a string is returned, build a com error from it
                if (s != null)
                {
                    throw new COMException(s, hr);
                }
                else
                {
                    // No string, just use standard com error
                    Marshal.ThrowExceptionForHR(hr);
                }
            }
        }

        /// <summary>
        /// Returns a string describing a DS error.  Works for both error codes
        /// (values < 0) and Status codes (values >= 0)
        /// </summary>
        /// <param name="hr">HRESULT for which to get description</param>
        /// <returns>The string, or null if no error text can be found</returns>
        public static string GetErrorText(int hr)
        {
            const int MAX_ERROR_TEXT_LEN = 160;

            // Make a buffer to hold the string
            StringBuilder buf = new StringBuilder(MAX_ERROR_TEXT_LEN, MAX_ERROR_TEXT_LEN);

            // If a string is returned, build a com error from it
            if (AMGetErrorText(hr, buf, MAX_ERROR_TEXT_LEN) > 0)
            {
                return buf.ToString();
            }

            return null;
        }
    }
}
