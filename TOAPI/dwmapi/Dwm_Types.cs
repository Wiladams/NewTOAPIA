using System;


using DWM_FRAME_COUNT = System.UInt64;
using QPC_TIME = System.UInt64;
using ULONGLONG = System.UInt64;
using DWORD = System.Int32;
using BOOL = System.Int32;
using HRGN = System.IntPtr;
using BYTE = System.Byte;
using UINT32 = System.UInt32;
using UINT = System.UInt32;
using DOUBLE = System.Double;

using TOAPI.Types;

namespace TOAPI.DwmApi
{
    public struct MARGINS 
    {
        int cxLeftWidth;
        int cxRightWidth;
        int cyTopHeight;
        int cyBottomHeight;
    } ;

    public struct MIL_MATRIX3X2D 
    {
        DOUBLE   _11, _12;
        DOUBLE   _21, _22;
        DOUBLE   DX;
        DOUBLE   DY;
    } ;

    public struct DWM_BLURBEHIND
    {
        DWORD dwFlags;
        BOOL fEnable;
        HRGN hRgnBlur;
        BOOL fTransitionOnMaximized;
    } ;




// Thumbnails
//typedef HANDLE HTHUMBNAIL;
//typedef HTHUMBNAIL* PHTHUMBNAIL;


    public struct DWM_THUMBNAIL_PROPERTIES
    {
        DWORD dwFlags;
        RECT rcDestination;
        RECT rcSource;
        BYTE opacity;
        BOOL fVisible;
        BOOL fSourceClientAreaOnly;
    };

    // Video enabling apis
    public struct UNSIGNED_RATIO
    {
        UINT32 uiNumerator;
        UINT32 uiDenominator;
    };

    public  struct DWM_TIMING_INFO
    {
        UINT32          cbSize;

        // Data on DWM composition overall
        
        // Monitor refresh rate
        UNSIGNED_RATIO  rateRefresh;

        // Actual period
        QPC_TIME        qpcRefreshPeriod;

        // composition rate     
        UNSIGNED_RATIO  rateCompose;

        // QPC time at a VSync interupt
        QPC_TIME        qpcVBlank;

        // DWM refresh count of the last vsync
        // DWM refresh count is a 64bit number where zero is
        // the first refresh the DWM woke up to process
        DWM_FRAME_COUNT cRefresh;

        // DX refresh count at the last Vsync Interupt
        // DX refresh count is a 32bit number with zero 
        // being the first refresh after the card was initialized
        // DX increments a counter when ever a VSync ISR is processed
        // It is possible for DX to miss VSyncs
        //
        // There is not a fixed mapping between DX and DWM refresh counts
        // because the DX will rollover and may miss VSync interupts
        UINT cDXRefresh;

        // QPC time at a compose time.  
        QPC_TIME        qpcCompose;

        // Frame number that was composed at qpcCompose
        DWM_FRAME_COUNT cFrame;

        // The present number DX uses to identify renderer frames
        UINT            cDXPresent;

        // Refresh count of the frame that was composed at qpcCompose
        DWM_FRAME_COUNT cRefreshFrame;


        // DWM frame number that was last submitted
        DWM_FRAME_COUNT cFrameSubmitted;

        // DX Present number that was last submitted
        UINT cDXPresentSubmitted;

        // DWM frame number that was last confirmed presented
        DWM_FRAME_COUNT cFrameConfirmed;

        // DX Present number that was last confirmed presented
        UINT cDXPresentConfirmed;

        // The target refresh count of the last
        // frame confirmed completed by the GPU
        DWM_FRAME_COUNT cRefreshConfirmed;

        // DX refresh count when the frame was confirmed presented
        UINT cDXRefreshConfirmed;

        // Number of frames the DWM presented late
        // AKA Glitches
        DWM_FRAME_COUNT          cFramesLate;
        
        // the number of composition frames that 
        // have been issued but not confirmed completed
        UINT          cFramesOutstanding;


        // Following fields are only relavent when an HWND is specified
        // Display frame


        // Last frame displayed
        DWM_FRAME_COUNT cFrameDisplayed;

        // QPC time of the composition pass when the frame was displayed
        QPC_TIME        qpcFrameDisplayed; 

        // Count of the VSync when the frame should have become visible
        DWM_FRAME_COUNT cRefreshFrameDisplayed;

        // Complete frames: DX has notified the DWM that the frame is done rendering

        // ID of the the last frame marked complete (starts at 0)
        DWM_FRAME_COUNT cFrameComplete;

        // QPC time when the last frame was marked complete
        QPC_TIME        qpcFrameComplete;

        // Pending frames:
        // The application has been submitted to DX but not completed by the GPU
     
        // ID of the the last frame marked pending (starts at 0)
        DWM_FRAME_COUNT cFramePending;

        // QPC time when the last frame was marked pending
        QPC_TIME        qpcFramePending;

        // number of unique frames displayed
        DWM_FRAME_COUNT cFramesDisplayed;

        // number of new completed frames that have been received
        DWM_FRAME_COUNT cFramesComplete;

         // number of new frames submitted to DX but not yet complete
        DWM_FRAME_COUNT cFramesPending;

        // number of frames available but not displayed, used or dropped
        DWM_FRAME_COUNT cFramesAvailable;

        // number of rendered frames that were never
        // displayed because composition occured too late
        DWM_FRAME_COUNT cFramesDropped;
        
        // number of times an old frame was composed 
        // when a new frame should have been used
        // but was not available
        DWM_FRAME_COUNT cFramesMissed;
        
        // the refresh at which the next frame is
        // scheduled to be displayed
        DWM_FRAME_COUNT cRefreshNextDisplayed;

        // the refresh at which the next DX present is 
        // scheduled to be displayed
        DWM_FRAME_COUNT cRefreshNextPresented;

        // The total number of refreshes worth of content
        // for this HWND that have been displayed by the DWM
        // since DwmSetPresentParameters was called
        DWM_FRAME_COUNT cRefreshesDisplayed;
    	
        // The total number of refreshes worth of content
        // that have been presented by the application
        // since DwmSetPresentParameters was called
        DWM_FRAME_COUNT cRefreshesPresented;


        // The actual refresh # when content for this
        // window started to be displayed
        // it may be different than that requested
        // DwmSetPresentParameters
        DWM_FRAME_COUNT cRefreshStarted;

        // Total number of pixels DX redirected
        // to the DWM.
        // If Queueing is used the full buffer
        // is transfered on each present.
        // If not queuing it is possible only 
        // a dirty region is updated
        ULONGLONG  cPixelsReceived;

        // Total number of pixels drawn.
        // Does not take into account if
        // if the window is only partial drawn
        // do to clipping or dirty rect management 
        ULONGLONG  cPixelsDrawn;

        // The number of buffers in the flipchain
        // that are empty.   An application can 
        // present that number of times and guarantee 
        // it won't be blocked waiting for a buffer to 
        // become empty to present to
        DWM_FRAME_COUNT      cBuffersEmpty;
    } ;


    public struct DWM_PRESENT_PARAMETERS
    {
        UINT32          cbSize;
        BOOL            fQueue;
        DWM_FRAME_COUNT cRefreshStart;
        UINT            cBuffer;
        BOOL            fUseSourceRate;
        UNSIGNED_RATIO  rateSource;
        UINT            cRefreshesPerFrame;
        DWM_SOURCE_FRAME_SAMPLING  eSampling;
    };

}
