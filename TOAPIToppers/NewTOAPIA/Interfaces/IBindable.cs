
namespace NewTOAPIA
{
    /// <summary>
    /// There are many instances where an object needs to be "bound" in order for
    /// it to perform it's action.  There are many such cases in OpenGL such
    /// as binding a Texture so it will be used for texture mapping, or binding
    /// a GLSL Program so that its shaders will become a part of the rendering pipeline.
    /// </summary>
    public interface IBindable
    {
        /// <summary>
        /// Take whatever action is necessary to bind the object.
        /// This may include allocation of resources, and changes to state
        /// information.
        /// </summary>
        void Bind();

        /// <summary>
        /// Perform whatever action is necessary to unbind the object.  This may include
        /// deallocation of resources and other changes to state.
        /// </summary>
        void Unbind();
    }
}
