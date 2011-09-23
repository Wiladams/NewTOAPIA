using System;
using System.Runtime.InteropServices;
using TOAPI.Types;

using BOOL = System.Int32;
using UINT = System.UInt32;
using UUID = System.UInt32;
using DWORD = System.Int32;

namespace TOAPI.DwmApi
{
    public partial class Dwm
    {
        //111    0 00005CAD DwmAttachMilContent
        [DllImport("dwmapi.dll", EntryPoint = "DwmAttachMilContent")]
        public static extern int DwmAttachMilContent(IntPtr hwnd);


        //113    1 0000559B DwmDefWindowProc
        [DllImport("dwmapi.dll", EntryPoint = "DwmDefWindowProc")]
        public static extern int DwmDefWindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, out int plResult);

        //114    2 00005D1B DwmDetachMilContent
        [DllImport("dwmapi.dll", EntryPoint = "DwmDetachMilContent")]
        public static extern int DwmDetachMilContent(IntPtr hwnd);

        //116    3 0000283D DwmEnableBlurBehindWindow
        [DllImport("dwmapi.dll", EntryPoint = "DwmEnableBlurBehindWindow")]
        public static extern int DwmEnableBlurBehindWindow(IntPtr hWnd, ref DWM_BLURBEHIND pBlurBehind);

        //102    4 000039C3 DwmEnableComposition
        [DllImport("dwmapi.dll", EntryPoint = "DwmEnableComposition")]
        public static extern int DwmEnableComposition(uint uCompositionAction);

        //117    5 00005DC2 DwmEnableMMCSS
        [DllImport("dwmapi.dll", EntryPoint = "DwmEnableMMCSS")]
        public static extern int DwmEnableMMCSS(BOOL fEnableMMCSS);

        //122    6 00002507 DwmExtendFrameIntoClientArea
        [DllImport("dwmapi.dll", EntryPoint = "DwmExtendFrameIntoClientArea")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        //123    7 00005549 DwmFlush
        [DllImport("dwmapi.dll", EntryPoint = "DwmFlush")]
        public static extern int DwmFlush();


        //125    8 0000302A DwmGetColorizationColor
        [DllImport("dwmapi.dll", EntryPoint = "DwmGetColorizationColor")]
        public static extern int DwmGetColorizationColor(out int pcrColorization, out BOOL pfOpaqueBlend);

        //126    9 00005E35 DwmGetCompositionTimingInfo
        [DllImport("dwmapi.dll", EntryPoint = "DwmGetCompositionTimingInfo")]
        public static extern int DwmGetCompositionTimingInfo(IntPtr hwnd, ref DWM_TIMING_INFO pTimingInfo);

        //127    A 00005952 DwmGetGraphicsStreamClient
        [DllImport("dwmapi.dll", EntryPoint = "DwmGetGraphicsStreamClient")]
        public static extern int DwmGetGraphicsStreamClient(UINT uIndex, out UUID pClientUuid);

        //128    B 00005BBD DwmGetGraphicsStreamTransformHint
        [DllImport("dwmapi.dll", EntryPoint = "DwmGetGraphicsStreamTransformHint")]
        public static extern int DwmGetGraphicsStreamTransformHint(UINT uIndex, out MIL_MATRIX3X2D pTransform);

        //129    C 00005A48 DwmGetTransportAttributes
        [DllImport("dwmapi.dll", EntryPoint = "DwmGetTransportAttributes")]
        public static extern int DwmGetTransportAttributes(out BOOL pfIsRemoting, out BOOL pfIsConnected, out DWORD pDwGeneration);

        //130    D 0000617C DwmGetWindowAttribute
        [DllImport("dwmapi.dll", EntryPoint = "DwmGetWindowAttribute")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, IntPtr pvAttribute, int cbAttribute);

        //131    E 00001BB6 DwmIsCompositionEnabled
        [DllImport("dwmapi.dll", EntryPoint = "DwmIsCompositionEnabled")]
        public static extern int DwmIsCompositionEnabled(out BOOL pfEnabled);

        //132    F 0000607A DwmModifyPreviousDxFrameDuration
        [DllImport("dwmapi.dll", EntryPoint = "DwmModifyPreviousDxFrameDuration")]
        public static extern int DwmModifyPreviousDxFrameDuration(IntPtr hwnd, int cRefreshes, BOOL fRelative);

        //133   10 00002F85 DwmQueryThumbnailSourceSize
        [DllImport("dwmapi.dll", EntryPoint = "DwmQueryThumbnailSourceSize")]
        public static extern int DwmQueryThumbnailSourceSize(IntPtr hThumbnail, ref Size pSize);

        //134   11 0000192C DwmRegisterThumbnail
        [DllImport("dwmapi.dll", EntryPoint = "DwmRegisterThumbnail")]
        public static extern int DwmRegisterThumbnail(IntPtr hwndDestination, ref IntPtr hwndSource, IntPtr phThumbnailId);

        //135   12 00006028 DwmSetDxFrameDuration
        [DllImport("dwmapi.dll", EntryPoint = "DwmSetDxFrameDuration")]
        public static extern int DwmSetDxFrameDuration(IntPtr hwnd, int cRefreshes);

        //136   13 00005F18 DwmSetPresentParameters
        [DllImport("dwmapi.dll", EntryPoint = "DwmSetPresentParameters")]
        public static extern int DwmSetPresentParameters(IntPtr hwnd, ref DWM_PRESENT_PARAMETERS pPresentParams);

        //137   14 00002A49 DwmSetWindowAttribute
        [DllImport("dwmapi.dll", EntryPoint = "DwmSetWindowAttribute")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, IntPtr pvAttribute, int cbAttribute);

        //138   15 00002993 DwmUnregisterThumbnail
        [DllImport("dwmapi.dll", EntryPoint = "DwmUnregisterThumbnail")]
        public static extern int DwmUnregisterThumbnail(IntPtr hThumbnailId);

        //139   16 00001895 DwmUpdateThumbnailProperties
        [DllImport("dwmapi.dll", EntryPoint = "DwmUpdateThumbnailProperties")]
        public static extern int DwmUpdateThumbnailProperties(IntPtr hThumbnailId, ref DWM_THUMBNAIL_PROPERTIES ptnProperties);
    }
}
