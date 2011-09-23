namespace NewTOAPIA.Graphics.Raytrace
{
    using NewTOAPIA.Graphics;

    abstract public class Material 
    {				
		public abstract Material Clone();


        public virtual ColorRGB shade(ShadeRec sr)
        {
            return new ColorRGB(0, 0, 0);
        }
			
        //Material operator= (const Material& rhs);						
}
}