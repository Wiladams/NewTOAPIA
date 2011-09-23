using System.Drawing;

using NewTOAPIA;
using NewTOAPIA.Drawing;

using TOAPI.User32;
using TOAPI.Types;

namespace NewTOAPIA.UI
{
    using NewTOAPIA.Drawing.GDI;

    public class User32ControlRenderer
    {
        public static void Draw(GDIContext dc, ControlRenderingStyle renderStyle, Rectangle rect)
        {
            RECT aRect = new RECT(rect.X, rect.Y, rect.Width, rect.Height);

            User32.DrawFrameControl(dc, 
                ref aRect, 
                (int)renderStyle.ControlCategory, 
                renderStyle.ControlType | (int)renderStyle.StateModifiers);
        }
   }

    public class ControlRenderingStyle
    {
        #region Fields
        ControlCategory fControlCategory;
        int fControlType;
        ControlStateModifier fModifiers;
        #endregion

        #region Constructors
        public ControlRenderingStyle(ControlCategory category, int controlType, ControlStateModifier modifiers)
        {
            fControlCategory = category;
            fControlType = controlType;
            fModifiers = modifiers;
        }
        #endregion

        #region Properties
        public ControlCategory ControlCategory
        {
            get { return fControlCategory; }
            protected set { fControlCategory = value; }
        }

        public int ControlType
        {
            get { return fControlType; }
            protected set { fControlType = value; }
        }

        public ControlStateModifier StateModifiers
        {
            get { return fModifiers; }
            protected set { fModifiers = value; }
        }
        #endregion
    }

    public class ButtonRendering : ControlRenderingStyle
    {
        public ButtonRendering(ButtonElementType buttonType, ControlStateModifier modifiers)
            : base(ControlCategory.Button, (int)buttonType, modifiers)
        {

        }

        public ButtonElementType ButtonElement
        {
            get { return (ButtonElementType)ControlType; }
            set { ControlType = (int)value; }
        }
    }

    public class MenuRendering : ControlRenderingStyle
    {
        public MenuRendering(MenuElementType menuElement, ControlStateModifier modifiers)
            : base(ControlCategory.Menu, (int)menuElement, modifiers)
        {}

        public MenuElementType MenuElement
        {
            get {return (MenuElementType)ControlType;}
            set {ControlType = (int)value;}
        }
    }
}