using System;

using NewTOAPIA;
using NewTOAPIA.Drawing;

namespace NewTOAPIA.UI
{
    using NewTOAPIA.Graphics;

    /// <summary>
    /// A Graphic is a little more than a Drawable in that it
    /// can be moved, and retains positional information.
    /// Additionally, a Graphic has hit detection, so it can tell you whether
    /// or not a particular point is within its boundaries.
    /// </summary>
    public interface IGraphic : IDrawable, ITransformable, IInteractor
    {
        string Name { get; }				// The name of the graphic
        
        Point3D Origin { get; set; }		// The uppper left of the graphic
        Vector3D Dimension { get; set; }		// The size of the graphic

        RectangleI Frame { get; set; }		        // Where is it located relative to its parent
        RectangleI ClientRectangle { get;}     // What is the 'content' area of the graphic

        Transformation WorldTransform { get; set; }
        Transformation LocalTransform { get; set; }

        void UpdateBoundaryState();
        void UpdateGeometryState();

        // Hit detection
        bool Contains(int x, int y);

        // Visibility
        bool IsVisible { get; set; }

        // Parent container
        IGraphicGroup Container { get; set; }
        IWindow Window { get; set; }

        // Unit of drawing
        //GraphicsUnit GraphicsUnit { get; set; }

        // Drawing validation
        void Invalidate();
        void Invalidate(RectangleI partialFrame);
    }
}
