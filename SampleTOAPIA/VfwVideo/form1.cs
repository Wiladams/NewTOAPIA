using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.GDI32;
using TOAPI.AviCap32;
using TOAPI.User32;

using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Kernel;
using NewTOAPIA.UI;
using NewTOAPIA.Media;




namespace VfwVideo
{
    public partial class Form1 : Window
    {
        VfwCameraDevice fCamera;
        NewTOAPIA.Drawing.GDI.GDIContext fScreenContext;
        BITMAPINFO fBitmapInfo;
        //PixelBuffer24 fPixelBuffer;
        VfwDeCompressor fDecompressor;

        TimedDispatcher fCaptureDispatcher;

        public Form1()
            :base("GDI Video", 10, 10, 1024, 480)
        {
            fScreenContext = GDIContext.CreateForDefaultDisplay();

            fCamera = new VfwCameraDevice(0, 320, 240, 15, User32.WS_CHILD | User32.WS_VISIBLE, this.Handle);
            //fCamera = new VfwCameraDevice(320, 240);
            fCamera.FrameDelegate = FrameReceived;
            fBitmapInfo = fCamera.GetBitmapInfo();

            fDecompressor = VfwDeCompressor.Create(Vfw.ICTYPE_VIDEO);

            // Allocate a pixel buffer to receive decompressed bits
            //fPixelBuffer = new PixelBuffer24(fCamera.Width, fCamera.Height);

            //fCamera.StartStreaming();
            fCamera.PreviewRate = 33;
            //fCamera.ShowPreviewWindow();

            fCaptureDispatcher = new TimedDispatcher(1.0 / 15, CaptureFrame, null);
            fCaptureDispatcher.Start();
        }

        void CaptureFrame(TimedDispatcher dispatcher, double time, Object[] dispatchParams)
        {
            //Console.WriteLine("CaptureFrame: {0}", time);
            fCamera.GrabSingleFrame();
        }

        public override void OnKeyDown(KeyboardActivityArgs ke)
        {
            switch (ke.VirtualKeyCode)
            {
                case VirtualKeyCodes.Space:
                    fCamera.GrabSingleFrame();
                    break;

                case VirtualKeyCodes.C:
                    fCamera.ShowCompressionChoices();
                    break;

                case VirtualKeyCodes.F:
                    fCamera.ShowVideoFormats();
                    break;

                case VirtualKeyCodes.S:
                    fCamera.ShowVideoSourceChoices();
                    break;
            }
        }

        void FrameReceived(IntPtr hwnd, ref VIDEOHDR hdr)
        {
            switch (fBitmapInfo.bmiHeader.biCompression)
            {
                case GDI32.BI_BITFIELDS:
                    Console.WriteLine("Bitfields");
                    break;
                case GDI32.BI_JPEG:
                    Console.WriteLine("JPEG");
                    break;
                case GDI32.BI_PNG:
                    Console.WriteLine("PNG");
                    break;

                case GDI32.BI_RGB:
                    {
                        //Console.WriteLine("RGB");
                        //PixelAccessorBGRb accessor = new PixelAccessorBGRb(fCamera.Width, fCamera.Height, PixmapOrientation.BottomToTop, hdr.lpData);
                        //SourceCopy srcCopy = new SourceCopy();

                        //fPixelBuffer.ApplyBinaryColorOperator(srcCopy, accessor);
                        //Rectangle srcRect = new Rectangle(0, 0, fCamera.Width, fCamera.Height);
                        //bool success = DeviceContextClientArea.AlphaBlend(fPixelBuffer.DeviceContext, srcRect, srcRect, 255);
                    }
                    break;

                case GDI32.BI_RLE4:
                    Console.WriteLine("RLE4");
                    break;
                case GDI32.BI_RLE8:
                    Console.WriteLine("RLE8");
                    break;

                default:
                    {
                        // Construct a stream on the bits
                        byte[] data = new byte[hdr.dwBytesUsed];
                        Marshal.Copy(hdr.lpData, data, 0, (int)hdr.dwBytesUsed);
                        MemoryStream ms = new MemoryStream(data);
                        Bitmap bm = (Bitmap)Bitmap.FromStream(ms);

                        string dStr = FOURCC.FourCCToString(FOURCC.MakeFourCC(data));
                        Console.WriteLine("data: {0}", dStr);

                        ICINFO icinfo = new ICINFO();
                        icinfo.dwSize = (uint)Marshal.SizeOf(icinfo);

                        uint fccHandler = 0;
                        while (Vfw.ICInfo(0, fccHandler, ref icinfo))
                        {
                            Console.WriteLine("Info: {0}  Driver: {1}", icinfo.szName, icinfo.szDriver);
                            string fccTypeStr = FOURCC.FourCCToString(icinfo.fccType);
                            string fccHandlerStr = FOURCC.FourCCToString(icinfo.fccHandler); 
                            Console.WriteLine("Form1.FrameReceived - FCC: {0}  Handler: {1}", fccTypeStr, fccHandlerStr);
                            fccHandler++;
                        }

                        //BITMAPINFO bmiOut = fPixelBuffer.BitmapInfo;
                        UInt32 vcap = FOURCC.MakeFourCC('v', 'c', 'a','p');
                        UInt32 VIDC = FOURCC.MakeFourCC('V', 'I', 'D', 'C');

                        #region temporarily off
                        /*
                        BITMAPINFOHEADER bmihIn = fPixelBuffer.BitmapInfo.bmiHeader;
                        BITMAPINFOHEADER bmihOut = new BITMAPINFOHEADER();
                        bmihOut.Init();
                        IntPtr handle = Vfw.ICLocate(VIDC, 0, ref bmihIn, ref bmihOut, 0);
                        Console.WriteLine("Handle: {0}", handle);
    */                    
    #endregion

                        //// Try to load it using the Image class
                        //Bitmap bm = (Bitmap)Image.FromStream(ms);
                        //Rectangle srcRect = new Rectangle(0, 0, fCamera.Width, fCamera.Height);
                        //bool success = DeviceContextClientArea.AlphaBlend(fPixelBuffer.DeviceContext, srcRect, srcRect, 255);

                    }
                    break;
            }

            
            //BITMAPINFO bmiOut = fPixelBuffer.BitmapInfo;
            //BITMAPINFOHEADER bmihIn = fPixelBuffer.BitmapInfo.bmiHeader;
            //BITMAPINFOHEADER bmihOut = new BITMAPINFOHEADER();
            //bmihOut.Init();

            //StringBuilder sb = new StringBuilder(100);
            //COMPVARS cvars = new COMPVARS();
            //cvars.cbSize = Marshal.SizeOf(cvars);

            //success = Vfw.ICCompressorChoose(this.Handle, 0, IntPtr.Zero, IntPtr.Zero, ref cvars, null);
            //IntPtr handle = Vfw.ICLocate(fBitmapInfo.bmiHeader.biCompression, 0, ref bmihIn, ref bmihOut, 0);
            //IntPtr bmHandle = fDecompressor.DeCompress(ref fBitmapInfo, hdr.lpData, ref bmiOut);

            //Console.WriteLine("Form1.FrameReceived - Compression: {0}", FOURCC.FourCCToString(fBitmapInfo.bmiHeader.biCompression));
            //Console.WriteLine("{0}", handle);
            
        }
    }
}
