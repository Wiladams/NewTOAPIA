
namespace NewTOAPIA.Modeling
{
    public class Effect
    {

        public virtual void Draw(ModelRenderer aRenderer, Spacial aSpacial, int min, int max, VisibleObject[] aVisual)
        {
            VisibleObject current = aVisual[0];

            for (int i = min; i < max; i++)
            {
                Geometry geometry = (Geometry)current.Spacial;
                geometry.Effects.Add(this);
                aRenderer.Draw(geometry);
                geometry.Effects.Remove(this);

                current = aVisual[i + 1];
            }
        }

        /// <summary>
        /// Lead any resources needed by an Effect subclass.
        /// This function is called by Renderer.LoadResources()
        /// </summary>
        /// <param name="aRenderer">The Rendering object doing the calling.</param>
        /// <param name="aGeometry">The Geometry object that is to be rendered.</param>
        public virtual void LoadResources(ModelRenderer aRenderer, Geometry aGeometry)
        {
        }

        /// <summary>
        /// Release any resources that were loaded with LoadResources.
        /// This function is called by Renderer.ReleaseResources.
        /// </summary>
        /// <param name="aRenderer"></param>
        /// <param name="aGeometry"></param>
        public virtual void ReleaseResources(ModelRenderer aRenderer, Geometry aGeometry)
        {
        }

    }

}
