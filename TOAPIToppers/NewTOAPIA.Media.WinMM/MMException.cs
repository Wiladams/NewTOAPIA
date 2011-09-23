using System;

namespace NewTOAPIA.Media.WinMM
{
    using TOAPI.WinMM;

    /// <summary>
    /// Summary description for MmException.
    /// </summary>
    public class MmException : Exception
    {
        private MMSYSERROR result;
        private string function;

        /// <summary>
        /// Creates a new MmException
        /// </summary>
        /// <param name="result">The result returned by the Windows API call</param>
        /// <param name="function">The name of the Windows API that failed</param>
        public MmException(int result, string function)
            : base(MmException.ErrorMessage(result, function))
        {
            this.result = (MMSYSERROR)result;
            this.function = function;
        }

        public MmException(MMSYSERROR result, string function)
            : base(MmException.ErrorMessage(result, function))
        {
            this.result = result;
            this.function = function;
        }

        private static string ErrorMessage(int result, string function)
        {
            return String.Format("{0} calling {1}", result, function);
        }

        private static string ErrorMessage(MMSYSERROR result, string function)
        {
            return String.Format("{0} calling {1}", result.ToString(), function);
        }

        /// <summary>
        /// Helper function to automatically raise an exception on failure
        /// </summary>
        /// <param name="result">The result of the API call</param>
        /// <param name="function">The API function name</param>
        public static void Try(int result, string function)
        {
            if (result != winmm.MMSYSERR_NOERROR)
                throw new MmException(result, function);
        }

        public static void Try(MMSYSERROR result, string function)
        {
            if (result != MMSYSERROR.MMSYSERR_NOERROR)
                throw new MmException(result, function);
        }

        /// <summary>
        /// Returns the Windows API result
        /// </summary>
        public MMSYSERROR Result
        {
            get
            {
                return result;
            }
        }
    }
}
