
namespace NewTOAPIA.UI
{
    using System;
    using System.Collections.Generic;

    public interface IGraphicHierarchy
    {
		// Graphic hierarchy
		void	AddGraphic(IGraphic graphic);
		void	AddGraphic(IGraphic graphic, IGraphic before);
		void	AddGraphicAfter(IGraphic graphic, IGraphic after);
		bool	RemoveGraphic(IGraphic graphic);
        void MoveGraphicToFront(IGraphic graphic);
		void	RemoveAllGraphics();
		int		CountGraphics();
		List<IGraphic> GraphicChildren { get;}

		void	OnGraphicAdded(IGraphic aGraphic);

		// Find which graphic is in the group at a particular
		// location.
		IGraphic	GraphicAt(int index);
		//IGraphic	GraphicAt(float x, float y);
		void		GraphicsAt(int x, int y, ref Stack<IGraphic> coll);
		IGraphic	GraphicNamed(string graphicName);
		IGraphic	GraphicNamedRecurse(string graphicName);	// search heirarchy.
	
		// Layout manager for graphics
		ILayoutManager LayoutHandler {get;set;}
	}
}
