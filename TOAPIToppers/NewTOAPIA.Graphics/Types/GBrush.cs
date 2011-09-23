namespace NewTOAPIA.Graphics
{
    using System;
 
    [Serializable]
    public class GBrush : IBrush
    {
        #region Properties
        public Colorref Color { get; protected set; }
        public BrushStyle BrushStyle { get; protected set; }
        public HatchStyle HatchStyle { get; protected set; }
        public Guid UniqueID { get; protected set; }
        #endregion

        #region Constructors
        public GBrush()
            : this((Colorref)Colorrefs.Black)
        {
        }

        public GBrush(Colorref color)
            :this (new ColorRGBA(color))
        {
        }

        public GBrush(ColorRGBA aColor)
            : this(BrushStyle.Solid, HatchStyle.Vertical, (Colorref)aColor.RGB, Guid.NewGuid())           
        {
        }

        public GBrush(BrushStyle aStyle, HatchStyle hatchStyle, Colorref aColor, Guid uniqueID)
        {
            BrushStyle = aStyle;
            HatchStyle = hatchStyle;
            Color = aColor;
            UniqueID = uniqueID;
        }
        #endregion

    }
}