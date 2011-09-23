using System;

using NewTOAPIA.UI;

/// <summary>
/// A GraphicBox is a graphic group that does not extend its size to match
/// the extents of its children.  Furthermore, it encapsulates many of the properties
/// that can be found in the CSS style sheets for the Box object type.
/// 
/// Margin
/// Padding
/// Border
/// </summary>
public class GraphicBox : ActiveArea
{
	public GraphicBox(string name, int x, int y, int width, int height)
		:base(name,x,y,width,height)
	{
	}
}
