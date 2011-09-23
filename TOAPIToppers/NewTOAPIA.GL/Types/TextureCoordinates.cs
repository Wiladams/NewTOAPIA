

namespace NewTOAPIA.GL
{
    public struct TextureCoordinates
    {
        public float s;
        public float t;

        public TextureCoordinates(float s, float t)
        {
            this.s = s;
            this.t = t;
        }

        public TextureCoordinates(float[] coords)
        {
            s = coords[0];
            t = coords[1];
        }

        public void Set(float s, float t)
        {
            this.s = s;
            this.t = t;
        }
    }
}
