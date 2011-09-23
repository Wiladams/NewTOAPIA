

namespace NewTOAPIA.Drawing
{

    public interface IColorType
    {
        RGBA_Doubles GetAsRGBA_Doubles();
        RGBA_Bytes GetAsRGBA_Bytes();

        RGBA_Bytes gradient(RGBA_Bytes c, double k);

        int R_Byte { get; set; }
        int G_Byte { get; set; }
        int B_Byte { get; set; }
        int A_Byte { get; set; }
    };
}
