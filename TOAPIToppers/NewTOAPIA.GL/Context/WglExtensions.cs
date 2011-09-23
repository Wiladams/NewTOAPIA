using System;

namespace NewTOAPIA.GL
{
    // These are in the import library
    //#region Imports
    //public delegate IntPtr DwglGetProcAddress_S(String functionName);
    //public delegate int DwglUseFontBitmapsA_PIII(IntPtr hdc, int first, int count, int listbase);
    //public delegate int DwglUseFontBitmapsW_PIII(IntPtr hdc, int first, int count, int listbase);
    //public delegate int DwglUseFontOutlinesA_PIIIFFIX(IntPtr hdc, int first, int count, int listBase, float deviation, float extrusion, int format, [Out, MarshalAs(UnmanagedType.LPArray)] GLYPHMETRICSFLOAT[] gmfarray);
    //public delegate int DwglUseFontOutlinesA_PIIIFFIP(IntPtr hdc, int first, int count, int listBase, float deviation, float extrusion, int format, IntPtr gmfarray);
    //public delegate int DwglUseFontOutlinesW_PIIIFFIX(IntPtr hdc, int first, int count, int listBase, float deviation, float extrusion, int format, [Out, MarshalAs(UnmanagedType.LPArray)] GLYPHMETRICSFLOAT[] gmfarray);
    //public delegate int DwglUseFontOutlinesW_PIIIFFIP(IntPtr hdc, int first, int count, int listBase, float deviation, float extrusion, int format, IntPtr gmfarray);
    //#endregion

    //#region Extensions
    //public delegate IntPtr DwglAllocateMemoryNV_IFFF(int size, float readfreq, float writefreq, float priority);
    //public delegate int DwglAssociateImageBufferEventsI3D_PrPrPrII(IntPtr hDC, ref IntPtr pEvent, ref IntPtr pAddress, ref int pSize, int count);
    //public delegate int DwglBeginFrameTrackingI3D_V();
    //public delegate byte DwglBindDisplayColorTableEXT_H(short id);
    //public delegate int DwglBindTexImageARB_PI(IntPtr hPbuffer, int iBuffer);
    //public delegate int DwglChoosePixelFormatARB_PaIaFIaIrI(IntPtr hdc, int[] piAttribIList, float[] pfAttribFList, int nMaxFormats, int[] piFormats, ref int nNumFormats);
    //public delegate int DwglChoosePixelFormatARB_PPPIPP(IntPtr hdc, IntPtr piAttribIList, IntPtr pfAttribFList, int nMaxFormats, IntPtr piFormats, IntPtr nNumFormats);
    //public delegate int DwglChoosePixelFormatEXT_PaIaFIaIrI(IntPtr hdc, int[] piAttribIList, float[] pfAttribFList, int nMaxFormats, int[] piFormats, ref int nNumFormats);
    //public delegate int DwglChoosePixelFormatEXT_PPPIPP(IntPtr hdc, IntPtr piAttribIList, IntPtr pfAttribFList, int nMaxFormats, IntPtr piFormats, IntPtr nNumFormats);
    //public delegate int DwglCopyContext_PPI(IntPtr hglrcSrc, IntPtr hglrcDst, int mask);
    //public delegate IntPtr DwglCreateBufferRegionARB_PII(IntPtr hDC, int iLayerPlane, int uType);
    //public delegate IntPtr DwglCreateContext_P(IntPtr hdc);
    //public delegate byte DwglCreateDisplayColorTableEXT_H(short id);
    //public delegate IntPtr DwglCreateImageBufferI3D_PII(IntPtr hDC, int dwSize, int uFlags);
    //public delegate IntPtr DwglCreateLayerContext_PI(IntPtr hdc, int iLayerPlane);
    //public delegate IntPtr DwglCreatePbufferARB_PIIIaI(IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, int[] piAttribList);
    //public delegate IntPtr DwglCreatePbufferARB_PIIIP(IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, IntPtr piAttribList);
    //public delegate IntPtr DwglCreatePbufferEXT_PIIIaI(IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, int[] piAttribList);
    //public delegate IntPtr DwglCreatePbufferEXT_PIIIP(IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, IntPtr piAttribList);
    //public delegate void DwglDeleteBufferRegionARB_P(IntPtr hRegion);
    //public delegate int DwglDeleteContext_P(IntPtr hglrc);
    //public delegate int DwglDescribeLayerPlane_PIIIrX(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nBytes, ref LAYERPLANEDESCRIPTOR plpd);
    //public delegate void DwglDestroyDisplayColorTableEXT_H(short id);
    //public delegate int DwglDestroyImageBufferI3D_PaB(IntPtr hDC, byte[] pAddress);
    //public delegate int DwglDestroyImageBufferI3D_PP(IntPtr hDC, IntPtr pAddress);
    //public delegate int DwglDestroyPbufferARB_P(IntPtr hPbuffer);
    //public delegate int DwglDestroyPbufferEXT_P(IntPtr hPbuffer);
    //public delegate int DwglDisableFrameLockI3D_V();
    //public delegate int DwglDisableGenlockI3D_P(IntPtr hDC);
    //public delegate int DwglEnableFrameLockI3D_V();
    //public delegate int DwglEnableGenlockI3D_P(IntPtr hDC);
    //public delegate int DwglEndFrameTrackingI3D_V();
    //public delegate void DwglFreeMemoryNV_P(IntPtr pointer);
    //public delegate int DwglGenlockSampleRateI3D_PI(IntPtr hDC, int uRate);
    //public delegate int DwglGenlockSourceDelayI3D_PI(IntPtr hDC, int uDelay);
    //public delegate int DwglGenlockSourceEdgeI3D_PI(IntPtr hDC, int uEdge);
    //public delegate int DwglGenlockSourceI3D_PI(IntPtr hDC, int uSource);
    //public delegate IntPtr DwglGetCurrentContext_V();
    //public delegate IntPtr DwglGetCurrentDC_V();
    //public delegate IntPtr DwglGetCurrentReadDCARB_V();
    //public delegate IntPtr DwglGetCurrentReadDCEXT_V();
    //public delegate int DwglGetDigitalVideoParametersI3D_PIrI(IntPtr hDC, int iAttribute, ref int piValue);
    //public delegate String DwglGetExtensionsStringARB_P(IntPtr hdc);
    //public delegate String DwglGetExtensionsStringEXT_V();
    //public delegate int DwglGetFrameUsageI3D_rF(ref float pUsage);
    //public delegate int DwglGetGammaTableI3D_PIaHaHaH(IntPtr hDC, int iEntries, short[] puRed, short[] puGreen, short[] puBlue);
    //public delegate int DwglGetGammaTableI3D_PIPPP(IntPtr hDC, int iEntries, IntPtr puRed, IntPtr puGreen, IntPtr puBlue);
    //public delegate int DwglGetGammaTableParametersI3D_PIrI(IntPtr hDC, int iAttribute, ref int piValue);
    //public delegate int DwglGetGenlockSampleRateI3D_PrI(IntPtr hDC, ref int uRate);
    //public delegate int DwglGetGenlockSourceDelayI3D_PrI(IntPtr hDC, ref int uDelay);
    //public delegate int DwglGetGenlockSourceEdgeI3D_PrI(IntPtr hDC, ref int uEdge);
    //public delegate int DwglGetGenlockSourceI3D_PrI(IntPtr hDC, ref int uSource);
    //public delegate int DwglGetLayerPaletteEntries_PIIIaI(IntPtr hdc, int iLayerPlane, int iStart, int cEntries, int[] rixbgr);
    //public delegate int DwglGetLayerPaletteEntries_PIIIP(IntPtr hdc, int iLayerPlane, int iStart, int cEntries, IntPtr rixbgr);
    //public delegate int DwglGetMscRateOML_PrIrI(IntPtr hdc, ref int numerator, ref int denominator);
    //public delegate IntPtr DwglGetPbufferDCARB_P(IntPtr hPbuffer);
    //public delegate IntPtr DwglGetPbufferDCEXT_P(IntPtr hPbuffer);
    //public delegate int DwglGetPixelFormatAttribfvARB_PIIIaIaF(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, int[] piAttributes, float[] pfValues);
    //public delegate int DwglGetPixelFormatAttribfvARB_PIIIPP(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, IntPtr piAttributes, IntPtr pfValues);
    //public delegate int DwglGetPixelFormatAttribfvEXT_PIIIaIaF(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, int[] piAttributes, float[] pfValues);
    //public delegate int DwglGetPixelFormatAttribfvEXT_PIIIPP(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, IntPtr piAttributes, IntPtr pfValues);
    //public delegate int DwglGetPixelFormatAttribivARB_PIIIaIaI(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, int[] piAttributes, int[] piValues);
    //public delegate int DwglGetPixelFormatAttribivARB_PIIIPP(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, IntPtr piAttributes, IntPtr piValues);
    //public delegate int DwglGetPixelFormatAttribivEXT_PIIIaIaI(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, int[] piAttributes, int[] piValues);
    //public delegate int DwglGetPixelFormatAttribivEXT_PIIIPP(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, IntPtr piAttributes, IntPtr piValues);
    //public delegate int DwglGetSwapIntervalEXT_V();
    //public delegate int DwglGetSyncValuesOML_PrLrLrL(IntPtr hdc, ref long ust, ref long msc, ref long sbc);
    //public delegate int DwglIsEnabledFrameLockI3D_rI(ref int pFlag);
    //public delegate int DwglIsEnabledGenlockI3D_PrI(IntPtr hDC, ref int pFlag);
    //public delegate byte DwglLoadDisplayColorTableEXT_aHI(short[] table, int length);
    //public delegate byte DwglLoadDisplayColorTableEXT_PI(IntPtr table, int length);
    //public delegate int DwglMakeContextCurrentARB_PPP(IntPtr hDrawDC, IntPtr hReadDC, IntPtr hglrc);
    //public delegate int DwglMakeContextCurrentEXT_PPP(IntPtr hDrawDC, IntPtr hReadDC, IntPtr hglrc);
    //public delegate int DwglMakeCurrent_PP(IntPtr hdc, IntPtr hglrc);
    //public delegate int DwglQueryFrameLockMasterI3D_rI(ref int pFlag);
    //public delegate int DwglQueryFrameTrackingI3D_rIrIrF(ref int pFrameCount, ref int pMissedFrames, ref float pLastMissedUsage);
    //public delegate int DwglQueryGenlockMaxSourceDelayI3D_PrIrI(IntPtr hDC, ref int uMaxLineDelay, ref int uMaxPixelDelay);
    //public delegate int DwglQueryPbufferARB_PIrI(IntPtr hPbuffer, int iAttribute, ref int piValue);
    //public delegate int DwglQueryPbufferEXT_PIrI(IntPtr hPbuffer, int iAttribute, ref int piValue);
    //public delegate int DwglRealizeLayerPalette_PII(IntPtr hdc, int iLayerPlane, int bRealize);
    //public delegate int DwglReleaseImageBufferEventsI3D_PrPI(IntPtr hDC, ref IntPtr pAddress, int count);
    //public delegate int DwglReleasePbufferDCARB_PP(IntPtr hPbuffer, IntPtr hDC);
    //public delegate int DwglReleasePbufferDCEXT_PP(IntPtr hPbuffer, IntPtr hDC);
    //public delegate int DwglReleaseTexImageARB_PI(IntPtr hPbuffer, int iBuffer);
    //public delegate int DwglRestoreBufferRegionARB_PIIIIII(IntPtr hRegion, int x, int y, int width, int height, int xSrc, int ySrc);
    //public delegate int DwglSaveBufferRegionARB_PIIII(IntPtr hRegion, int x, int y, int width, int height);
    //public delegate int DwglSetDigitalVideoParametersI3D_PIrI(IntPtr hDC, int iAttribute, ref int piValue);
    //public delegate int DwglSetGammaTableI3D_PIaHaHaH(IntPtr hDC, int iEntries, short[] puRed, short[] puGreen, short[] puBlue);
    //public delegate int DwglSetGammaTableI3D_PIPPP(IntPtr hDC, int iEntries, IntPtr puRed, IntPtr puGreen, IntPtr puBlue);
    //public delegate int DwglSetGammaTableParametersI3D_PIrI(IntPtr hDC, int iAttribute, ref int piValue);
    //public delegate int DwglSetLayerPaletteEntries_PIIIaI(IntPtr hdc, int iLayerPlane, int iStart, int cEntries, int[] rixbgr);
    //public delegate int DwglSetLayerPaletteEntries_PIIIP(IntPtr hdc, int iLayerPlane, int iStart, int cEntries, IntPtr rixbgr);
    //public delegate int DwglSetPbufferAttribARB_PaI(IntPtr hPbuffer, int[] piAttribList);
    //public delegate int DwglSetPbufferAttribARB_PP(IntPtr hPbuffer, IntPtr piAttribList);
    //public delegate int DwglShareLists_PP(IntPtr hglrc1, IntPtr hglrc2);
    //public delegate int DwglSwapBuffers_P(IntPtr hdc);
    //public delegate long DwglSwapBuffersMscOML_PLLL(IntPtr hdc, long target_msc, long divisor, long remainder);
    //public delegate int DwglSwapIntervalEXT_I(int interval);
    //public delegate int DwglSwapLayerBuffers_PI(IntPtr hdc, int fuPlanes);
    //public delegate long DwglSwapLayerBuffersMscOML_PILLL(IntPtr hdc, int fuPlanes, long target_msc, long divisor, long remainder);
    ////public delegate int DwglSwapMultipleBuffers_IrX(int p1, ref WGLSWAP pwglswap);
    //public delegate int DwglWaitForMscOML_PLLLrLrLrL(IntPtr hdc, long target_msc, long divisor, long remainder, ref long ust, ref long msc, ref long sbc);
    //public delegate int DwglWaitForMscOML_PLLLPPP(IntPtr hdc, long target_msc, long divisor, long remainder, IntPtr ust, IntPtr msc, IntPtr sbc);
    //public delegate int DwglWaitForSbcOML_PLPPP(IntPtr hdc, long target_sbc, IntPtr ust, IntPtr msc, IntPtr sbc);
    //#endregion

    public partial class Wgl
    {
        #region wgl Extensions
        //public DwglSwapIntervalEXT_I wglSwapIntervalEXT;
        #endregion

    }
}
