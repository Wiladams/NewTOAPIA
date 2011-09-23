
namespace NewTOAPIA.UI
{
    using NewTOAPIA.Graphics;

    public class LayoutManager : ILayoutManager
	{
        // By having an 'Empty' layout manager, there can be a default
        // layout manager, which does nothing special.  That way we don't
        // have to check for null every time.
		public static ILayoutManager Empty = new LayoutManager();

        RectangleI fFrame;
        private IGraphicGroup fContainer;


		public LayoutManager()
			:this(null,RectangleI.Empty)
		{
		}

        public LayoutManager(IGraphicGroup gg, RectangleI frame)
		{
			fFrame = frame;
			fContainer = gg;
		}

        public IGraphicGroup Container
		{
			get {return fContainer;}
		}

		public RectangleI Frame
		{
			get { return fFrame; }
			set { fFrame = value; }
		}

		public virtual void ResetLayout()
		{
			// Reset the layout for incremental adding
		}

		public virtual void AddToLayout(IGraphic trans)
		{
		}

        public virtual void Layout(IGraphicGroup gg)
		{
			// Reset the layout
			ResetLayout();

			fContainer = gg;

			// And add each of the children one by one
			foreach (IGraphic g in gg.GraphicChildren)
			{
				AddToLayout(g);
			}
		}
	}
}