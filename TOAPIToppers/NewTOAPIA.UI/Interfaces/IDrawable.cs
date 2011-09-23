
using NewTOAPIA.Drawing;

namespace NewTOAPIA.UI
{
    public delegate void DrawEventHandler(object sender, DrawEvent devent);

    public interface IDrawable
    {
        // Drawable things
        void Draw(DrawEvent drawEvent);
    }

    public interface IDrawableReaction
    {
        void OnDraw(DrawEvent drawEvent);
    }

}