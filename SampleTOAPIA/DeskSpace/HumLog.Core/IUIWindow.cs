
using TOAPI.Types;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;


namespace HumLog
{
    public interface IUIWindow : IDrawable, IReactToMouseActivity, IMoveable
    {
        void RefreshDisplay(RECT aRect);
    }
}
