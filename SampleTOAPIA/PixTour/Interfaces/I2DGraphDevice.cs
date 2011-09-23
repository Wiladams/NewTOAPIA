

namespace Papyrus.Types
{
    using System;

	/// <summary>
	/// The IGraphPort interface is intended to represent the
	/// interface to a graphics device.  Some notable things, 
	/// the coordinates are int based
	/// It uses the bare minimum of functionality that can be
	/// implemented by just about any device.
	/// 
	/// Devices are also different in that they don't typically
	/// maintain any graph state information.  That is, they don't
	/// know anything about a 'current position', and they don't 
	/// do fancy line and curve drawing.  That is all left for 
	/// GraphPort implementations to deal with.
	/// </summary>
	public interface IGraphPort : IRender2DGeometry, IGDIDeviceContext
	{
        // Creation of drawing tools: Brush, Pen, Bitmap
        Brush CreateBrush(Brush.Style aStyle, int width, uint color);
        Pen CreatePen(Pen.Style aStyle, int width, uint color);
        Bitmap CreateBitmap(int width, int height);

        void UseDefaultBrush();
        void UseDefaultPen();

        // Simple drawing properties
        //FillMode FillMode {get;set;}
        Brush Brush { get; set; }
        //StringFormat StringFormat {get;set;}

        // Colors used by default pen and brush
        uint BrushColor { get; set; }
		uint PenColor { get; set; }


        // Some drawing attributes
        uint BackgroundColor { get; set; }
        BackgroundMixMode BackgroundMixMode { get; set; }
        uint DrawingColor { get; set; }
		//uint ForegroundColor{get;set;}
		uint FillColor { get; set; }

		// Text related stuff
		Font Font { get; set; }
		uint TextColor { get; set; }
		int FontSize { get; set; }
		string	FontName {get; set; }

		/// Some GraphPort control things
		void Flush();


        void SaveState();
        void RestoreState();

		// Drawing Primitives

		/// Our first pass will be to implement everything as 
		/// taking explicit int parameters.
		/// We'll assume that drawing with a different pen will 
		/// be accomplished by setting the Pen property on the 
		/// port before drawing.  This will keep our API count 
		/// low and alleviate the need to pass the same 
		/// parameter every time.  We can add more later.
		///
		///


		/// DrawLine
		//void DrawLine(Line aLine);
        void DrawLine(int x1, int y1, int x2, int y2);

		/// DrawRectangle
        void FrameRectangle(int x, int y, int width, int height);
        void FillRectangle(int x, int y, int width, int height);


		/// DrawEllipse

		/// Draw a String
		void DrawText(string s, int x, int y, int width, int height, int flags);

		void MeasureString(String s, ref int width, ref int height);
		void MeasureCharacter(char c, ref int width, ref int height);

		// Draw a Polygon
        //void DrawPolygon(Polygon aPolygon);
        void DrawPolygon(PointG[] points, Pen aPen, Brush aBrush);
        void DrawPolygon(PointG[] points);

		/// Draw bitmaps
        /// 
        ////void DrawImage(IPixelBuffer anImage);

        
        //// Specialized image drawing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="srchDC"></param>
        /// <param name="srcX"></param>
        /// <param name="srcY"></param>
        /// <param name="srcWidth"></param>
        /// <param name="srcHeight"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
		bool AlphaBlend(int x, int y, int width, int height,
			IGDIDeviceContext srchDC, int srcX, int srcY, int srcWidth, int srcHeight,
			byte alpha);
		bool BitBlt(int x, int y, int nWidth, int nHeight,
            IGDIDeviceContext hSrcDC, int xSrc, int ySrc, TernaryRasterOps dwRop);
        bool BitBlt(int x, int y, int nWidth, int nHeight,
            IntPtr hSrcDC, int xSrc, int ySrc, TernaryRasterOps dwRop);
        bool StretchBlt(int nXOriginDest, int nYOriginDest,
			int nWidthDest, int nHeightDest,
			IGDIDeviceContext hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
            TernaryRasterOps dwRop);
        bool PlgBlt(PointG[] lpPoint,
			IGDIDeviceContext hdcSrc, int nXSrc, int nYSrc, int nWidth, int nHeight,
			IntPtr hbmMask, int xMask, int yMask);

        // Dealing with a path
        bool BeginPath();
        bool EndPath();
        bool FramePath();
        bool FillPath();
        bool DrawPath();

        // Dealing with clipping region
        bool SetPathAsClipRegion();
        void SetClip(RectangleG clipRect);
        //void SetClip(Region clipRegion);
        void ResetClip();

		// ViewPort/Window origin and scaling
        int SetMappingMode(MappingModes aMode);
        
        PointG SetViewPortOrigin(int x, int y);
        SizeG SetViewPortExtent(int width, int height);

        // Window Origin and Extent
        PointG SetWindowOrigin(int x, int y);
        PointG OffsetWindowOrigin(int x, int y);
        SizeG SetWindowExtent(int width, int height);

        XFORM WorldTransform { get; set; }

        // Some Affine transforms
        bool TranslateTransform(int dx, int dy);
	}
}