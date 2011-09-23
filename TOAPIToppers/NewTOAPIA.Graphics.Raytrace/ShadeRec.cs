namespace NewTOAPIA.Graphics.Raytrace
{
    using NewTOAPIA.Graphics;

    public class ShadeRec
    {
        bool hit_an_object;		// did the ray hit an object?
        Material material_ptr;		// pointer to the nearest object's material
        Point3D hit_point;			// world coordinates of intersection
        Point3D local_hit_point;	// world coordinates of hit point on untransformed object (used for texture transformations)
        Normal3D normal;				// normal at hit point
        Ray ray;				// required for specular highlights and area lights
        int depth;				// recursion depth
        ColorRGB color;				// used in the Chapter 3 only
        double t;					// ray parameter
        float u;					// texture coordinate
        float v;					// texture coordinate
        World w;					// world reference

        ShadeRec(World wr)					// constructor
        {
            	hit_an_object = false;
		material_ptr = null;
		hit_point = new Point3D();
		local_hit_point = new Point3D();
		normal = new Normal3D();
		ray = new Ray();
		depth = 0;
		color = new ColorRGB(0,0,0);
		t = (0.0);
		u = (0.0f);
		v = (0.0f);
		w = (wr);

        }

        ShadeRec(ShadeRec sr)
        {
	        hit_an_object = (sr.hit_an_object);
		material_ptr = (sr.material_ptr);
		hit_point = (sr.hit_point);
		local_hit_point = (sr.local_hit_point);
		normal = (sr.normal);
		ray = (sr.ray);
		depth = (sr.depth);
		color = (sr.color);
		t = (sr.t);
		u = (sr.u);
		v = (sr.v);
        w = (sr.w);

        }

        //~ShadeRec(void);						// destructor
    }
}