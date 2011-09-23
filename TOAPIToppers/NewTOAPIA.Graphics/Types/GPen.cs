namespace NewTOAPIA.Graphics
{
    using System;

    [Serializable]
    public class GPen : IPen
    {
        public PenType TypeOfPen { get; set; }
        public PenStyle Style { get; set; }
        public PenJoinStyle JoinStyle { get; set; }
        public PenEndCap EndCap { get; set; }
        public int Width { get; set; }
        public Colorref Color { get; set; }
        public Guid UniqueID { get; set; }

        #region Constructors
        public GPen(Colorref colorref)
            : this(PenType.Cosmetic, PenStyle.Solid, PenJoinStyle.Round, PenEndCap.Round, colorref, 1, Guid.NewGuid())
        {
        }

        public GPen(PenType aType, PenStyle aStyle, PenJoinStyle aJoinStyle, PenEndCap aEndCap, Colorref colorref, int width, Guid uniqueID)
        {
            TypeOfPen = aType;
            Style = aStyle;
            JoinStyle = aJoinStyle;
            EndCap = aEndCap;
            Width = width;
            Color = colorref;

            this.UniqueID = uniqueID;

            //int combinedStyle = (int)aStyle | (int)aType | (int)aJoinStyle | (int)aEndCap;

            if (PenType.Cosmetic == aType)
            {
                // If it's cosmetic, the width must be 1
                width = 1;
            }
        }
        #endregion
    }
}