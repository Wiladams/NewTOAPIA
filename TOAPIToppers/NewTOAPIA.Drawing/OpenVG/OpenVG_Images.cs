namespace NewTOAPIA.Drawing
{
    using System;

    using VGboolean = System.Boolean;
    using VGubyte = System.Byte;
    using VGshort = System.UInt16;
    using VGint = System.Int32;
    using VGuint = System.UInt32;
    using VGbitfield = System.UInt32;
    using VGfloat = System.Single;

    using VGHandle = System.IntPtr;
    using VGPath = System.Object;
    using VGMaskLayer = System.Object;
    using VGFont = System.Object;

    public partial class OpenVG
    {
        /* Images */
        //abstract public VGImage CreateImage(VGImageFormat format,
        //                                  VGint width, VGint height,
        //                                  VGbitfield allowedQuality);
        //abstract public void DestroyImage(VGImage image);
        //abstract public void ClearImage(VGImage image,
        //                              VGint x, VGint y, VGint width, VGint height);
        abstract public void ImageSubData(VGImage image,
                                         object data, VGint dataStride,
                                        VGImageFormat dataFormat,
                                        VGint x, VGint y, VGint width, VGint height);
        abstract public void GetImageSubData(VGImage image,
                                           object data, VGint dataStride,
                                           VGImageFormat dataFormat,
                                           VGint x, VGint y,
                                           VGint width, VGint height);
        abstract public VGImage ChildImage(VGImage parent,
                                         VGint x, VGint y, VGint width, VGint height);
        abstract public VGImage GetParent(VGImage image);
        abstract public void CopyImage(VGImage dst, VGint dx, VGint dy,
                                     VGImage src, VGint sx, VGint sy,
                                     VGint width, VGint height,
                                     VGboolean dither);
        abstract public void DrawImage(VGImage image);
        abstract public void SetPixels(VGint dx, VGint dy,
                                     VGImage src, VGint sx, VGint sy,
                                     VGint width, VGint height);
        abstract public void WritePixels(object data, VGint dataStride,
                                       VGImageFormat dataFormat,
                                       VGint dx, VGint dy,
                                       VGint width, VGint height);
        abstract public void GetPixels(VGImage dst, VGint dx, VGint dy,
                                     VGint sx, VGint sy,
                                     VGint width, VGint height);
        abstract public void ReadPixels(object data, VGint dataStride,
                                      VGImageFormat dataFormat,
                                      VGint sx, VGint sy,
                                      VGint width, VGint height);
        abstract public void CopyPixels(VGint dx, VGint dy,
                                      VGint sx, VGint sy,
                                      VGint width, VGint height);
    }
}