

namespace NewTOAPIA.Drawing
{
    using System.ServiceModel;

    
    [ServiceContract]
    public interface IGraphPort : IDraw2D, IRenderPixelBuffer, IGraphState
    {
    }
}