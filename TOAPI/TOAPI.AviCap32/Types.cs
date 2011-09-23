using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.Types;

namespace TOAPI.AviCap32
{
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct CAPDRIVERCAPS
    {
        /// UINT->unsigned int
        public uint wDeviceIndex;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fHasOverlay;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fHasDlgVideoSource;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fHasDlgVideoFormat;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fHasDlgVideoDisplay;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fCaptureInitialized;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fDriverSuppliesPalettes;

        /// HANDLE->void*
        public System.IntPtr hVideoIn;

        /// HANDLE->void*
        public System.IntPtr hVideoOut;

        /// HANDLE->void*
        public System.IntPtr hVideoExtIn;

        /// HANDLE->void*
        public System.IntPtr hVideoExtOut;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct CAPINFOCHUNK
    {
        public uint fccInfoID;
        public System.IntPtr lpData;
        public int cbData;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct CAPSTATUS
    {

        /// UINT->unsigned int
        public uint uiImageWidth;

        /// UINT->unsigned int
        public uint uiImageHeight;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fLiveWindow;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fOverlayWindow;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fScale;

        /// POINT->tagPOINT
        public POINT ptScroll;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fUsingDefaultPalette;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fAudioHardware;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fCapFileExists;

        /// DWORD->unsigned int
        public uint dwCurrentVideoFrame;

        /// DWORD->unsigned int
        public uint dwCurrentVideoFramesDropped;

        /// DWORD->unsigned int
        public uint dwCurrentWaveSamples;

        /// DWORD->unsigned int
        public uint dwCurrentTimeElapsedMS;

        /// HPALETTE->HPALETTE__*
        public System.IntPtr hPalCurrent;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fCapturingNow;

        /// DWORD->unsigned int
        public uint dwReturn;

        /// UINT->unsigned int
        public uint wNumVideoAllocated;

        /// UINT->unsigned int
        public uint wNumAudioAllocated;
    }



    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct CAPTUREPARMS
    {

        /// DWORD->unsigned int
        public uint dwRequestMicroSecPerFrame;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fMakeUserHitOKToCapture;

        /// UINT->unsigned int
        public uint wPercentDropForError;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fYield;

        /// DWORD->unsigned int
        public uint dwIndexSize;

        /// UINT->unsigned int
        public uint wChunkGranularity;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fUsingDOSMemory;

        /// UINT->unsigned int
        public uint wNumVideoRequested;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fCaptureAudio;

        /// UINT->unsigned int
        public uint wNumAudioRequested;

        /// UINT->unsigned int
        public uint vKeyAbort;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fAbortLeftMouse;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fAbortRightMouse;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fLimitEnabled;

        /// UINT->unsigned int
        public uint wTimeLimit;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fMCIControl;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fStepMCIDevice;

        /// DWORD->unsigned int
        public uint dwMCIStartTime;

        /// DWORD->unsigned int
        public uint dwMCIStopTime;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fStepCaptureAt2x;

        /// UINT->unsigned int
        public uint wStepCaptureAverageFrames;

        /// DWORD->unsigned int
        public uint dwAudioBufferSize;

        /// BOOL->int
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fDisableWriteCache;

        /// UINT->unsigned int
        public uint AVStreamMaster;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
    public struct ICINFO
    {
        /// DWORD->unsigned int
        public uint dwSize;

        /// DWORD->unsigned int
        public uint fccType;

        /// DWORD->unsigned int
        public uint fccHandler;

        /// DWORD->unsigned int
        public uint dwFlags;

        /// DWORD->unsigned int
        public uint dwVersion;

        /// DWORD->unsigned int
        public uint dwVersionICM;

        /// WCHAR[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string szName;

        /// WCHAR[128]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szDescription;

        /// WCHAR[128]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szDriver;
    }

}
