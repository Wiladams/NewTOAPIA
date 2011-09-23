
namespace NewTOAPIA.GL.Imaging
{
    /// <summary>
    /// This is the most basic interface for any class that processes a texture
    /// object.  The general idea is to return the processed texture from the call.
    /// The returned value may be the passed in texture modified, or a new texture
    /// altogether, depending on the nature of the processor.
    /// </summary>
    public interface IUnaryTextureProcessor
    {
        GLTexture2D ProcessTexture(GLTexture2D baseTexture);
    }

    public interface IBinaryTextureProcessor
    {
        GLTexture2D ProcessTwoTextures(GLTexture2D baseTexture, GLTexture2D blendTexture);
    }

    public interface ITernaryTextureProcessor
    {
        GLTexture2D ProcessThreeTextures(GLTexture2D baseTexture, GLTexture2D blendTexture1, GLTexture2D blendTexture2);
    }
}
