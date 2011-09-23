

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace NewTOAPIA.DirectShow.MMStreaming
{
    using NewTOAPIA.DirectShow;


    sealed public class MsResults
    {
        private MsResults()
        {
            // Prevent people from trying to instantiate this class
        }

        public const int S_Pending = unchecked((int)0x00040001);
        public const int S_NoUpdate = unchecked((int)0x00040002);
        public const int S_EndOfStream = unchecked((int)0x00040003);
        public const int E_SampleAlloc = unchecked((int)0x80040401);
        public const int E_PurposeId = unchecked((int)0x80040402);
        public const int E_NoStream = unchecked((int)0x80040403);
        public const int E_NoSeeking = unchecked((int)0x80040404);
        public const int E_Incompatible = unchecked((int)0x80040405);
        public const int E_Busy = unchecked((int)0x80040406);
        public const int E_NotInit = unchecked((int)0x80040407);
        public const int E_SourceAlreadyDefined = unchecked((int)0x80040408);
        public const int E_InvalidStreamType = unchecked((int)0x80040409);
        public const int E_NotRunning = unchecked((int)0x8004040a);
    }

    sealed public class MsError
    {
        private MsError()
        {
            // Prevent people from trying to instantiate this class
        }

        public static string GetErrorText(int hr)
        {
            string sRet = null;

            switch (hr)
            {
                case MsResults.S_Pending:
                    sRet = "Sample update is not yet complete.";
                    break;
                case MsResults.S_NoUpdate:
                    sRet = "Sample was not updated after forced completion.";
                    break;
                case MsResults.S_EndOfStream:
                    sRet = "End of stream. Sample not updated.";
                    break;
                case MsResults.E_SampleAlloc:
                    sRet = "An IMediaStream object could not be removed from an IMultiMediaStream object because it still contains at least one allocated sample.";
                    break;
                case MsResults.E_PurposeId:
                    sRet = "The specified purpose ID can't be used for the call.";
                    break;
                case MsResults.E_NoStream:
                    sRet = "No stream can be found with the specified attributes.";
                    break;
                case MsResults.E_NoSeeking:
                    sRet = "Seeking not supported for this IMultiMediaStream object.";
                    break;
                case MsResults.E_Incompatible:
                    sRet = "The stream formats are not compatible.";
                    break;
                case MsResults.E_Busy:
                    sRet = "The sample is busy.";
                    break;
                case MsResults.E_NotInit:
                    sRet = "The object can't accept the call because its initialize function or equivalent has not been called.";
                    break;
                case MsResults.E_SourceAlreadyDefined:
                    sRet = "Source already defined.";
                    break;
                case MsResults.E_InvalidStreamType:
                    sRet = "The stream type is not valid for this operation.";
                    break;
                case MsResults.E_NotRunning:
                    sRet = "The IMultiMediaStream object is not in running state.";
                    break;
                default:
                    sRet = DsError.GetErrorText(hr);
                    break;
            }

            return sRet;
        }

        /// <summary>
        /// If hr has a "failed" status code (E_*), throw an exception.  Note that status
        /// messages (S_*) are not considered failure codes.  If DES or DShow error text
        /// is available, it is used to build the exception, otherwise a generic com error
        /// is thrown.
        /// </summary>
        /// <param name="hr">The HRESULT to check</param>
        public static void ThrowExceptionForHR(int hr)
        {
            // If an error has occurred
            if (hr < 0)
            {
                // If a string is returned, build a com error from it
                string buf = GetErrorText(hr);

                if (buf != null)
                {
                    throw new COMException(buf, hr);
                }
                else
                {
                    // No string, just use standard com error
                    Marshal.ThrowExceptionForHR(hr);
                }
            }
        }
    }

}
