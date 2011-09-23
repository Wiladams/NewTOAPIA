namespace NewTOAPIA.Drawing
{
    using System;

    using VGubyte = System.Byte;
    using VGshort = System.UInt16;
    using VGint = System.Int32;
    using VGuint = System.UInt32;
    using VGbitfield = System.UInt32;
    using VGfloat = System.Single;

    using VGHandle = System.IntPtr;
    using VGPath = System.Object;
    using VGImage = System.Object;
    using VGMaskLayer = System.Object;
    using VGFont = System.Object;
    using VGPaint = System.Object;

    public partial class OpenVG
    {
        /* Getters and Setters */
        abstract public void Set(VGParamType type, VGfloat value);
        abstract public void Set(VGParamType type, VGint value);
        abstract public void Setfv(VGParamType type, VGint count, VGfloat[] values);
        abstract public void Setiv(VGParamType type, VGint count, VGint[] values);

        abstract public VGfloat Getf(VGParamType type);
        abstract public VGint Geti(VGParamType type);
        abstract public VGint GetVectorSize(VGParamType type);
        abstract public void Getfv(VGParamType type, VGint count, VGfloat[] values);
        abstract public void Getiv(VGParamType type, VGint count, VGint[] values);

        abstract public void SetParameter(VGHandle obj, VGint paramType, VGfloat value);
        abstract public void SetParameter(VGHandle obj, VGint paramType, VGint value);
        abstract public void SetParameterfv(VGHandle obj, VGint paramType, VGint count, VGfloat[] values);
        abstract public void SetParameteriv(VGHandle obj, VGint paramType, VGint count, VGint[] values);

        abstract public VGfloat GetParameterf(VGHandle obj, VGint paramType);
        abstract public VGint GetParameteri(VGHandle obj, VGint paramType);
        abstract public VGint GetParameterVectorSize(VGHandle obj, VGint paramType);
        abstract public void GetParameterfv(VGHandle obj, VGint paramType, VGint count, VGfloat[] values);
        abstract public void GetParameteriv(VGHandle obj, VGint paramType, VGint count, VGint[] values);

        /* Matrix Manipulation */
        abstract public void LoadIdentity();
        abstract public void LoadMatrix(VGfloat[] m);
        abstract public void GetMatrix(VGfloat[] m);
        abstract public void MultMatrix(VGfloat[] m);
        abstract public void Translate(VGfloat tx, VGfloat ty);
        abstract public void Scale(VGfloat sx, VGfloat sy);
        abstract public void Shear(VGfloat shx, VGfloat shy);
        abstract public void Rotate(VGfloat angle);
    }
}