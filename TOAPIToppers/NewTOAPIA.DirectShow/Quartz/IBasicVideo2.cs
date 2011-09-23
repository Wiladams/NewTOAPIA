
namespace NewTOAPIA.DirectShow.Quartz
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("329bb360-f6ea-11d1-9038-00a0c9697298"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IBasicVideo2 : IBasicVideo
    {

        [PreserveSig]
        new int get_AvgTimePerFrame([Out] out double pAvgTimePerFrame);

        [PreserveSig]
        new int get_BitRate([Out] out int pBitRate);

        [PreserveSig]
        new int get_BitErrorRate([Out] out int pBitRate);

        [PreserveSig]
        new int get_VideoWidth([Out] out int pVideoWidth);

        [PreserveSig]
        new int get_VideoHeight([Out] out int pVideoHeight);

        [PreserveSig]
        new int set_SourceLeft([In] int SourceLeft);

        [PreserveSig]
        new int get_SourceLeft([Out] out int pSourceLeft);

        [PreserveSig]
        new int set_SourceWidth([In] int SourceWidth);

        [PreserveSig]
        new int get_SourceWidth([Out] out int pSourceWidth);

        [PreserveSig]
        new int set_SourceTop([In] int SourceTop);

        [PreserveSig]
        new int get_SourceTop([Out] out int pSourceTop);

        [PreserveSig]
        new int set_SourceHeight([In] int SourceHeight);

        [PreserveSig]
        new int get_SourceHeight([Out] out int pSourceHeight);

        [PreserveSig]
        new int set_DestinationLeft([In] int DestinationLeft);

        [PreserveSig]
        new int get_DestinationLeft([Out] out int pDestinationLeft);

        [PreserveSig]
        new int set_DestinationWidth([In] int DestinationWidth);

        [PreserveSig]
        new int get_DestinationWidth([Out] out int pDestinationWidth);

        [PreserveSig]
        new int set_DestinationTop([In] int DestinationTop);

        [PreserveSig]
        new int get_DestinationTop([Out] out int pDestinationTop);

        [PreserveSig]
        new int set_DestinationHeight([In] int DestinationHeight);

        [PreserveSig]
        new int get_DestinationHeight([Out] out int pDestinationHeight);

        [PreserveSig]
        new int SetSourcePosition(
            [In] int left,
            [In] int top,
            [In] int width,
            [In] int height
            );

        [PreserveSig]
        new int GetSourcePosition(
            [Out] out int left,
            [Out] out int top,
            [Out] out int width,
            [Out] out int height
            );

        [PreserveSig]
        new int SetDefaultSourcePosition();

        [PreserveSig]
        new int SetDestinationPosition(
            [In] int left,
            [In] int top,
            [In] int width,
            [In] int height
            );

        [PreserveSig]
        new int GetDestinationPosition(
            [Out] out int left,
            [Out] out int top,
            [Out] out int width,
            [Out] out int height
            );

        [PreserveSig]
        new int SetDefaultDestinationPosition();

        [PreserveSig]
        new int GetVideoSize(
            [Out] out int pWidth,
            [Out] out int pHeight
            );

        [PreserveSig]
        new int GetVideoPaletteEntries(
            [In] int StartIndex,
            [In] int Entries,
            [Out] out int pRetrieved,
            [Out] out int[] pPalette
            );

        [PreserveSig]
        new int GetCurrentImage(
            [In, Out] ref int pBufferSize,
            [Out] IntPtr pDIBImage // int *
            );

        [PreserveSig]
        new int IsUsingDefaultSource();

        [PreserveSig]
        new int IsUsingDefaultDestination();


        [PreserveSig]
        int GetPreferredAspectRatio(
            [Out] out int plAspectX,
            [Out] out int plAspectY
            );
    }
}
