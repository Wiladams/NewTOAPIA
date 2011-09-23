
namespace NewTOAPIA.GL
{
    public enum GluStringName
    {
        Version            = 0x0189c0,
        Extensions         = 0x0189c1,
    }

    /* QuadricDrawStyle */
    public enum QuadricDrawStyle
    {
        Point       = 0x0186aa,
        Line        = 0x0186ab,
        Fill        = 0x0186ac,
        Silhouette  = 0x0186ad,
    }

    /* QuadricNormal */
    public enum QuadricNormalType
    {
        Smooth      = 0x0186a0,
        Flat        = 0x0186a1,
        None        = 0x0186a2
    }

    /* QuadricOrientation */
    public enum QuadricOrientation
    {
        Outside     = 0x186b4,
        Inside      = 0x186b5
    }
}
