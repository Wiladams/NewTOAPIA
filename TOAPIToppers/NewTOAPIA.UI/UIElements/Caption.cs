using NewTOAPIA.UI;

namespace NewTOAPIA.UI
{
    /// <summary>
    /// A Caption is a graphic group that allows only two graphics.  It uses a BinaryLayout
    /// to place the 'label' graphic in a position relative to the 'primary' graphic.
    /// </summary>
	public class Caption : GraphicGroup
	{
		public Caption(IGraphic label, IGraphic primaryGraphic, Position pos, int gap)
			: base("Caption", 0,0,0,0)
		{
            AutoGrow = true;
			LayoutHandler = new BinaryLayout(pos, gap);

			AddGraphic(label);
            AddGraphic(primaryGraphic);

		}
	}
}
