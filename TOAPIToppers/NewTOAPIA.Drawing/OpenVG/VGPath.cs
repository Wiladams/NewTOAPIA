namespace NewTOAPIA.Drawing
{
    using VGboolean = System.Boolean;
    using VGubyte = System.Byte;
    using VGshort = System.UInt16;
    using VGint = System.Int32;
    using VGuint = System.UInt32;
    using VGbitfield = System.UInt32;
    using VGfloat = System.Single;

    public class VGPath
    {
        public void Clear(VGbitfield capabilities)
        {
        }

        public void RemoveCapabilities(VGbitfield capabilities)
        {
        }

        public VGbitfield GetCapabilities()
        {
            return 0;
        }

        public void Append(VGPath srcPath)
        {
        }

        public void AppendData(VGint numSegments, VGubyte[] pathSegments, object pathData)
        {
        }

        public void ModifyCoords(VGint startIndex, VGint numSegments, object pathData)
        {
        }

        public void Transform(VGPath srcPath)
        {
        }

        public VGboolean Interpolate(VGPath startPath, VGPath endPath, VGfloat amount)
        {
            return false;
        }

        public VGfloat Length(VGint startSegment, VGint numSegments)
        {
            return 0.0f;
        }

        public void PointAlongPath(VGint startSegment, VGint numSegments,
                                      VGfloat distance, VGfloat[] x, VGfloat[] y,
                                      VGfloat[] tangentX, VGfloat[] tangentY)
        {
        }

        public void Bounds(VGfloat[] minX, VGfloat[] minY, VGfloat[] width, VGfloat[] height)
        {
        }

        public void TransformedBounds(VGfloat[] minX, VGfloat[] minY, VGfloat[] width, VGfloat[] height)
        {
        }

        #region Static Helpers
        public static VGPath Create(VGint pathFormat,
                                VGPathDatatype datatype,
                                VGfloat scale, VGfloat bias,
                                VGint segmentCapacityHint,
                                VGint coordCapacityHint,
                                VGbitfield capabilities)
        {
            return null;
        }
        #endregion
    }
}