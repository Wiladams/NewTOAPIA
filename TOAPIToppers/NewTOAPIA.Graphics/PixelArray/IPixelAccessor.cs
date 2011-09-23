
namespace NewTOAPIA.Graphics
{
    using System;

    public interface IAssignPixel
    {
        void SetPixel(int x, int y, IPixel aPixel);
    }

    public interface IRetrievePixel
    {
        IPixel GetPixel(int x, int y);
    }

    public interface IPixelAccessor : IAssignPixel, IRetrievePixel
    {
        IntPtr Pixels { get; }
    }
}
