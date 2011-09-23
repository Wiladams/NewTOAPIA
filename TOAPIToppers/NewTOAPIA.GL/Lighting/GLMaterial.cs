
namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public struct GLMaterial
    {
        ColorRGBA fAmbient;
        ColorRGBA fDiffuse;
        ColorRGBA fSpecular;
        ColorRGBA fEmissive;
        float fShininess;

        /// <summary>
        /// Create a material with some default properties.
        /// </summary>
        /// <returns>GLMaterial</returns>
        public static GLMaterial Create()
        {
            GLMaterial aMaterial = new GLMaterial(new ColorRGBA(0.2f, 0.2f, 0.2f, 1.0f),
                new ColorRGBA(0.8f, 0.8f, 0.8f, 1.0f),
                new ColorRGBA(0.0f, 0.0f, 0.0f, 1.0f),
                new ColorRGBA(0.0f, 0.0f, 0.0f, 1.0f),
                0.0f);

            return aMaterial;
        }

        public GLMaterial(ColorRGBA ambient, ColorRGBA diffuse, ColorRGBA specular, ColorRGBA emissive, float shininess)
        {
            fAmbient = ambient;
            fDiffuse = diffuse;
            fSpecular = specular;
            fEmissive = emissive;

            fShininess = shininess;
        }

        public ColorRGBA Ambient
        {
            get { return fAmbient; }
            set
            {
                fAmbient = value;
            }
        }

        public ColorRGBA Diffuse
        {
            get { return fDiffuse; }
            set
            {
                fDiffuse = value;
            }
        }

        public ColorRGBA Specular
        {
            get { return fSpecular; }
            set
            {
                fSpecular = value;
            }
        }

        public ColorRGBA Emissive
        {
            get { return fEmissive; }
            set
            {
                fEmissive = value;
            }
        }

        public float Shininess
        {
            get {
                return fShininess; 
            }
            set
            {
                fShininess = value;
            }
        }

        public void RealizeFace(GLFace aFace)
        {
            GL.Material(aFace, MaterialParameter.Ambient, (float[])fAmbient);
            GL.Material(aFace, MaterialParameter.Diffuse, (float[])fDiffuse);
            GL.Material(aFace, MaterialParameter.Specular, (float[])fSpecular);
            GL.Material(aFace, MaterialParameter.Emission, (float[])fEmissive);
            GL.Material(aFace, MaterialParameter.Shininess, new float[]{fShininess});
        }
    }
}
