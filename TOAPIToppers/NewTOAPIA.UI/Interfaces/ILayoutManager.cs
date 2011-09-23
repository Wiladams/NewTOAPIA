namespace NewTOAPIA.UI
{
    public interface ILayoutManager
    {
        //void SuspendLayout();
        //void ResumeLayout();

        void ResetLayout();						// Reset the layout for incremental adding
        void AddToLayout(IGraphic trans);	// Incrementally add a new graphic to the layout
        void Layout(IGraphicGroup gh);		// Layout the entire group in one shot
    }
}
