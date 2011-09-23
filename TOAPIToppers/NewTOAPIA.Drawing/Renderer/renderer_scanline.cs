

namespace NewTOAPIA.Drawing
{
    using System;
    using System.Collections.Generic;

    using NewTOAPIA.Graphics;

    public class Renderer : RendererBase
    {
        const int cover_full = 255;
        protected IScanlineCache m_ScanlineCache;

        public Renderer()
        {
        }

        public Renderer(IImage destImage, rasterizer_scanline_aa rasterizer, IScanlineCache scanlineCache)
            : base(destImage, rasterizer)
        {
            m_ScanlineCache = scanlineCache;
        }

        public override IScanlineCache ScanlineCache
        {
            get { return m_ScanlineCache; }
            set { m_ScanlineCache = value; }
        }

        public override void SetClippingRect(RectangleD clippingRect)
        {
            Rasterizer.SetVectorClipBox(clippingRect);
        }

        public override void Render(IVertexSource vertexSource, int pathIndexToRender, RGBA_Bytes colorBytes)
        {
            m_Rasterizer.reset();
            Affine transform = GetTransform();
            if (!transform.IsIdentity())
            {
                vertexSource = new conv_transform(vertexSource, transform);
            }
            m_Rasterizer.add_path(vertexSource, pathIndexToRender);
            Renderer.RenderSolid(m_DestImage, m_Rasterizer, m_ScanlineCache, colorBytes);
        }

        static PathStorage RectPath = new PathStorage();

        void DrawImage(IImage sourceImage,
            double DestX, double DestY,
            double HotspotOffsetX, double HotspotOffsetY,
            double ScaleX, double ScaleY,
            double AngleRad,
            RGBA_Bytes Color32,
            ref RectangleD pFinalBlitBounds,
            bool doDrawing,
            bool oneMinusSourceAlphaOne)
        {
            Affine destRectTransform = Affine.NewIdentity();

            if (HotspotOffsetX != 0.0f || HotspotOffsetY != 0.0f)
            {
                destRectTransform *= Affine.NewTranslation(-HotspotOffsetX, -HotspotOffsetY);
            }

            if (ScaleX != 1 || ScaleY != 1)
            {
                destRectTransform *= Affine.NewScaling(ScaleX, ScaleY);
            }

            if (AngleRad != 0)
            {
                destRectTransform *= Affine.NewRotation(AngleRad);
            }

            if (DestX != 0 || DestY != 0)
            {
                destRectTransform *= Affine.NewTranslation(DestX, DestY);
            }

            int SourceBufferWidth = (int)sourceImage.Width();
            int SourceBufferHeight = (int)sourceImage.Height();

            RectPath.Clear();

            RectPath.MoveTo(0, 0);
            RectPath.LineTo(SourceBufferWidth, 0);
            RectPath.LineTo(SourceBufferWidth, SourceBufferHeight);
            RectPath.LineTo(0, SourceBufferHeight);
            RectPath.ClosePolygon();


            // Calculate the bounds. LBB [10/5/2004]
            const int ERROR_ADD = 0;
            double BoundXDouble, BoundYDouble;
            BoundXDouble = 0; BoundYDouble = 0;
            destRectTransform.Transform(ref BoundXDouble, ref BoundYDouble);
            double BoundX = (double)BoundXDouble;
            double BoundY = (double)BoundYDouble;

            pFinalBlitBounds.Left = Math.Floor(BoundX - ERROR_ADD);
            pFinalBlitBounds.Right = Math.Ceiling(BoundX + ERROR_ADD);
            pFinalBlitBounds.Top = Math.Floor(BoundY - ERROR_ADD);
            pFinalBlitBounds.Bottom = Math.Ceiling(BoundY + ERROR_ADD);

            BoundXDouble = SourceBufferWidth; BoundYDouble = 0;
            destRectTransform.Transform(ref BoundXDouble, ref BoundYDouble);
            BoundX = (double)BoundXDouble;
            BoundY = (double)BoundYDouble;
            pFinalBlitBounds.Left = Math.Min((long)Math.Floor(BoundX - ERROR_ADD), pFinalBlitBounds.Left);
            pFinalBlitBounds.Right = Math.Max((long)Math.Ceiling(BoundX + ERROR_ADD), pFinalBlitBounds.Right);
            pFinalBlitBounds.Top = Math.Min((long)Math.Floor(BoundY - ERROR_ADD), pFinalBlitBounds.Top);
            pFinalBlitBounds.Bottom = Math.Max((long)Math.Ceiling(BoundY + ERROR_ADD), pFinalBlitBounds.Bottom);

            BoundXDouble = SourceBufferWidth; BoundYDouble = SourceBufferHeight;
            destRectTransform.Transform(ref BoundXDouble, ref BoundYDouble);
            BoundX = (double)BoundXDouble;
            BoundY = (double)BoundYDouble;
            pFinalBlitBounds.Left = Math.Min((long)Math.Floor(BoundX - ERROR_ADD), pFinalBlitBounds.Left);
            pFinalBlitBounds.Right = Math.Max((long)Math.Ceiling(BoundX + ERROR_ADD), pFinalBlitBounds.Right);
            pFinalBlitBounds.Top = Math.Min((long)Math.Floor(BoundY - ERROR_ADD), pFinalBlitBounds.Top);
            pFinalBlitBounds.Bottom = Math.Max((long)Math.Ceiling(BoundY + ERROR_ADD), pFinalBlitBounds.Bottom);

            BoundXDouble = 0; BoundYDouble = SourceBufferHeight;
            destRectTransform.Transform(ref BoundXDouble, ref BoundYDouble);
            BoundX = (double)BoundXDouble;
            BoundY = (double)BoundYDouble;
            pFinalBlitBounds.Left = Math.Min((long)Math.Floor(BoundX - ERROR_ADD), pFinalBlitBounds.Left);
            pFinalBlitBounds.Right = Math.Max((long)Math.Ceiling(BoundX + ERROR_ADD), pFinalBlitBounds.Right);
            pFinalBlitBounds.Top = Math.Min((long)Math.Floor(BoundY - ERROR_ADD), pFinalBlitBounds.Top);
            pFinalBlitBounds.Bottom = Math.Max((long)Math.Ceiling(BoundY + ERROR_ADD), pFinalBlitBounds.Bottom);

            if (!doDrawing)
            {
                return;
            }

            if (m_DestImage.OriginOffset.x != 0 || m_DestImage.OriginOffset.y != 0)
            {
                destRectTransform *= Affine.NewTranslation(-m_DestImage.OriginOffset.x, -m_DestImage.OriginOffset.y);
            }

            Affine sourceRectTransform = new Affine(destRectTransform);
            // We invert it because it is the transform to make the image go to the same position as the polygon. LBB [2/24/2004]
            sourceRectTransform.Invert();

            span_allocator spanAllocator = new span_allocator();

            span_interpolator_linear interpolator = new span_interpolator_linear(sourceRectTransform);

            ImageBuffer sourceImageWithBlender = (ImageBuffer)sourceImage;// new ImageBuffer(sourceImage, new BlenderBGRA());

            span_image_filter_rgba_bilinear_clip spanImageFilter;
            ImageBufferAccessorClip source = new ImageBufferAccessorClip(sourceImageWithBlender, RGBA_Doubles.rgba_pre(0, 0, 0, 0).GetAsRGBA_Bytes());
            spanImageFilter = new span_image_filter_rgba_bilinear_clip(source, RGBA_Doubles.rgba_pre(0, 0, 0, 0), interpolator);

            rasterizer_scanline_aa rasterizer = new rasterizer_scanline_aa();
            rasterizer.SetVectorClipBox(0, 0, m_DestImage.Width(), m_DestImage.Height());
            scanline_packed_8 scanlineCache = new scanline_packed_8();
            //scanline_unpacked_8 scanlineCache = new scanline_unpacked_8();

            conv_transform transfromedRect = new conv_transform(RectPath, destRectTransform);
            rasterizer.add_path(transfromedRect);
#if false
	        bool HighQualityFilter = (BlitXParams.m_OptionalFlags & CBlitXParams::BlitHighQualityFilter) != 0
		        && (BlitXParams.m_OptionalFlags & CBlitXParams::RenderOneMinusScrAlpha_One) == 0;
	        if (HighQualityFilter)
	        {
		        static agg::image_filter_lut filter;
		        static bool BuiltLUT = false;
		        if (!BuiltLUT)
		        {
			        filter.calculate(agg::image_filter_blackman(4), true);
			        BuiltLUT = true;
		        }
		        typedef agg::span_image_filter_rgba<src_accessor_type, interpolator_type> span_gen_type;

		        CFrame PremultFrame;
		        PremultFrame.Initialize(sourceImage->GetWidth(), sourceImage->GetHeight(), 32);
		        PremultFrame.Fill(CColor(255,0,255,255));
		        const byte* pSrcBuffer = sourceImage->GetBuffer();
		        const ulong* pSrcOffsets = sourceImage->GetYTable();
		        byte* pDestBuffer = PremultFrame.GetBuffer();
		        ulong* pDestOffsets = PremultFrame.GetYTable();

		        const Pixel32* pSrcRow = (const Pixel32*)(pSrcBuffer);
		        Pixel32* pDestRow = (Pixel32*)(pDestBuffer);
		        uint NumPixels = sourceImage->GetHeight() * sourceImage->GetWidth();
		        for (uint i=0; i<NumPixels; i++)
		        {
			        if( (*((ulong*)&Color32) & 0xFFFFFFFF) == 0xFFFFFFFF)
			        {
				        pDestRow[i].Alpha = pSrcRow[i].Alpha;
				        pDestRow[i].Red = (pSrcRow[i].Red*pSrcRow[i].Alpha + 255) >> 8;
				        pDestRow[i].Green = (pSrcRow[i].Green*pSrcRow[i].Alpha + 255) >> 8;
				        pDestRow[i].Blue = (pSrcRow[i].Blue*pSrcRow[i].Alpha + 255) >> 8;
			        }
			        else
			        {
				        pDestRow[i].Alpha = (pSrcRow[i].Alpha*Color32.Alpha + 255) >> 8;
				        pDestRow[i].Red = (Color32.Red*pSrcRow[i].Red*pDestRow[i].Alpha + 65535) >> 16;
				        pDestRow[i].Green = (Color32.Green*pSrcRow[i].Green*pDestRow[i].Alpha + 65535) >> 16;
				        pDestRow[i].Blue = (Color32.Blue*pSrcRow[i].Blue*pDestRow[i].Alpha + 65535) >> 16;
			        }
		        }
		        agg::rendering_buffer colored_premultiplied_buffer;
		        colored_premultiplied_buffer.attach(PremultFrame.GetBuffer(), PremultFrame.GetWidth(), PremultFrame.GetHeight(), (int)PremultFrame.GetScanWidth() * (int)PremultFrame.GetBytesPerPixel());
		        agg::pixfmt_bgra32 premult_pixfmt(colored_premultiplied_buffer);
		        src_accessor_type premult_image(premult_pixfmt, agg::rgba(0,0,0,0));
		        if(BlitXParams.m_OptionalFlags & CBlitXParams::RenderCorrectDestAlpha)
		        {
			        CRenderScanlinesFiltered<pixfmt_bgra32_premult_src_respect_dest_alpha, span_gen_type>::Draw(&DestBuffer, &premult_image, &spanAllocator, &interpolator, &rasterizer, &scanlineCache, pClippingRect, &filter);
		        }
		        else
		        {
			        CRenderScanlinesFiltered<pixfmt_bgra32_premult_src_ignore_dest_alpha, span_gen_type>::Draw(&DestBuffer, &premult_image, &spanAllocator, &interpolator, &rasterizer, &scanlineCache, pClippingRect, &filter);
		        }
	        }
	        else 
#endif
            {
                ImageClippingProxy destImageWithClipping = new ImageClippingProxy(m_DestImage);
                Renderer.GenerateAndRender(rasterizer, scanlineCache, destImageWithClipping, spanAllocator, spanImageFilter);
#if false
		        if(oneMinusSourceAlphaOne)
		        {
			        CRenderScanlines<agg::pixfmt_bgra32_add_blend_respect_alpha, span_gen_type>::Draw(&DestBuffer, &src_image, Color32, &spanAllocator, &interpolator, &rasterizer, &scanlineCache, pClippingRect);
		        }
		        else
		        {
			        CRenderScanlines<agg::pixfmt_bgra32_mult_blend_respect_alpha, span_gen_type>::Draw(&DestBuffer, &src_image, Color32, &spanAllocator, &interpolator, &rasterizer, &scanlineCache, pClippingRect);
		        }
#endif
            }
        }

        public override void Render(IImage source,
            double x, double y,
            double angleDegrees,
            double inScaleX, double inScaleY,
            RGBA_Bytes color,
            bool doDrawing,
            bool oneMinusSourceAlphaOne)
        {
            RectangleD m_OutFinalBlitBounds = new RectangleD();
            //const int ALPHA_CHANNEL_BITS_DIVISOR = 5;

            double scaleX = inScaleX;
            double scaleY = inScaleY;

            double HotspotOffsetX = 0;
            double HotspotOffsetY = 0;

#if false
	        MaxAlphaFrameProperty maxAlphaFrameProperty = MaxAlphaFrameProperty::GetMaxAlphaFrameProperty(source);

	        if((maxAlphaFrameProperty.GetMaxAlpha() * color.A_Byte) / 256 <= ALPHA_CHANNEL_BITS_DIVISOR)
	        {
		        m_OutFinalBlitBounds.SetRect(0,0,0,0);
	        }
#endif

            if (source == m_DestImage || scaleX <= 0 || scaleY <= 0)
            {
                m_OutFinalBlitBounds.SetRect(0, 0, 0, 0);
            }

#if false // all our buffers our 32
	        if(source.GetBitDepth() != 32)
	        {
		        int HSX = source.GetUpperLeftOffsetX();
		        int HSY = source.GetUpperLeftOffsetY();
		        CFrame Temp;
		        Temp.Initialize(source.GetWidth(), source.GetHeight(), 32);
		        Temp.CFrameInterface::Blit(source, -HSX, -HSY, &BlitNormal);
		        assert(SafeCast(source, CFrame));
		        ((CFrame*)source).Initialize(&Temp);
		        ((CFrame*)source).SetUpperLeftOffsetX(HSX);
		        ((CFrame*)source).SetUpperLeftOffsetY(HSY);
	        }
#endif

            //*(Pixel32*)&g_BlitTransColorBlend = m_Color.GetColor32();

            bool IsScaled = (scaleX != 1 || scaleY != 1);
            bool IsRotated = (Math.Abs(angleDegrees) > 0.1);

            //bool IsMipped = false;
            double sourceOriginOffsetX = source.OriginOffset.x;
            double sourceOriginOffsetY = source.OriginOffset.y;
            bool CanUseMipMaps = IsScaled;
            if (scaleX > 0.5 || scaleY > 0.5)
            {
                CanUseMipMaps = false;
            }
#if false
	        if(CanUseMipMaps)
	        {
		        CMipMapFrameProperty* pMipMapFrameProperty = CMipMapFrameProperty::GetMipMapFrameProperty(source);
		        double OldScaleX = scaleX;
		        double OldScaleY = scaleY;
		        const CFrameInterface* pMippedFrame = pMipMapFrameProperty.GetMipMapFrame(ref scaleX, ref scaleY);
		        if(pMippedFrame != source)
		        {
			        IsMipped = true;
			        source = pMippedFrame;
			        sourceOriginOffsetX *= (OldScaleX / scaleX);
			        sourceOriginOffsetY *= (OldScaleY / scaleY);
		        }
	        }
#endif
#if false // this is the fast drawing path
	        if(!IsScaled
		        && !IsRotated
		        &&	m_X == FloatToLong(m_X)
		        &&	m_Y == FloatToLong(m_Y)
		        &&  (m_OptionalFlags & RenderCorrectDestAlpha) == 0)
	        {
		        bool SomethingToDraw = false;
		        double X = m_X - HotspotOffsetX;
		        double Y = m_Y - HotspotOffsetY;
		        g_NumPixelsBlitted += source.GetWidth() * source.GetHeight();

		        static CBlitTransBlendMultColor TransBlendMultColor;
		        static CBlitTransBlendAddColor  TransBlendAddColor;

		        CRect SourceRect;
		        source.GetClippingRect(&SourceRect);
		        assert((SourceRect.top < SourceRect.bottom) && (SourceRect.left < SourceRect.right));
		        CRect DestRect;
		        DestRect = SourceRect;
		        DestRect.OffsetRect(FloatToLong(X), FloatToLong(Y));
		        if(m_DoDrawing)
		        {
			        if(m_DoClipping)
			        {
				        if(CFrame::ClipRects(&m_ClippingRect, &SourceRect, &DestRect))
				        {
					        SomethingToDraw = true;
				        }
			        }
			        else
			        {
				        SomethingToDraw = true;
			        }

			        if(SomethingToDraw)
			        {
				        if(m_OptionalFlags & CBlitXParams::RenderOneMinusScrAlpha_One)
				        {
					        SomethingToDraw = pDestFrame.Blit(source, &SourceRect, &DestRect, &TransBlendAddColor);
				        }
				        else
				        {
					        if((g_BlitTransColorBlend & 0xFFFFFF) == 0xFFFFFF)
					        {
						        BlitTrans.SetOpacity((double)Color32.Alpha / 255.f);
						        SomethingToDraw = pDestFrame.Blit(source, &SourceRect, &DestRect, &BlitTrans);
						        BlitTrans.SetOpacity(1.f);
					        }
					        else
					        {
						        SomethingToDraw = pDestFrame.Blit(source, &SourceRect, &DestRect, &TransBlendMultColor);
					        }
				        }
			        }
		        }

		        m_OutFinalBlitBounds = DestRect;

	            return SomethingToDraw;
	        }
	        else
#endif
            {
#if false
		        if(	IsMipped)
		        {
			        HotspotOffsetX *= (inScaleX / scaleX);
			        HotspotOffsetY *= (inScaleY / scaleY);
		        }
#endif
                double FinalHotspotX = sourceOriginOffsetX - HotspotOffsetX;
                double FinalHotspotY = sourceOriginOffsetY - HotspotOffsetY;

                DrawImage(source, x, y,
                    FinalHotspotX, FinalHotspotY,
                    scaleX, scaleY, agg_math.DegToRad(angleDegrees), color, ref m_OutFinalBlitBounds, true, false);

#if false
		        LineFloat(BoundingRect.left, BoundingRect.top, BoundingRect.right, BoundingRect.top, WHITE);
		        LineFloat(BoundingRect.right, BoundingRect.top, BoundingRect.right, BoundingRect.bottom, WHITE);
		        LineFloat(BoundingRect.right, BoundingRect.bottom, BoundingRect.left, BoundingRect.bottom, WHITE);
		        LineFloat(BoundingRect.left, BoundingRect.bottom, BoundingRect.left, BoundingRect.top, WHITE);
#endif
            }
        }

        public override void Clear(IColorType color)
        {
            ImageClippingProxy clipper = (ImageClippingProxy)m_DestImage;
            if (clipper != null)
            {
                clipper.clear(color);
            }
        }

        #region RenderSolid

#if use_timers
        static CNamedTimer PrepareTimer = new CNamedTimer("Prepare");
#endif

        //========================================================render_scanlines
        public static void RenderSolid(IImage rasterFormat, IRasterizer rasterizer, IScanlineCache scanLine, RGBA_Bytes color)
        {
            if (rasterizer.rewind_scanlines())
            {
                scanLine.reset(rasterizer.min_x(), rasterizer.max_x());
#if use_timers
                PrepareTimer.Start();
#endif
                //renderer.prepare();
#if use_timers
                PrepareTimer.Stop();
#endif
                while (rasterizer.sweep_scanline(scanLine))
                {
                    Renderer.RenderSolidSingleScanLine(rasterFormat, scanLine, color);
                }
            }
        }
        #endregion

        #region RenderSolidSingleScanLine
#if use_timers
        static CNamedTimer render_scanline_aa_solidTimer = new CNamedTimer("render_scanline_aa_solid");
        static CNamedTimer render_scanline_aa_solid_blend_solid_hspan = new CNamedTimer("render_scanline_aa_solid_blend_solid_hspan");
        static CNamedTimer render_scanline_aa_solid_blend_hline = new CNamedTimer("render_scanline_aa_solid_blend_hline");
#endif
        //================================================render_scanline_aa_solid
        private static void RenderSolidSingleScanLine(IImage destImage, IScanlineCache scanLine, RGBA_Bytes color)
        {
#if use_timers
            render_scanline_aa_solidTimer.Start();
#endif
            int y = scanLine.y();
            int num_spans = scanLine.num_spans();
            ScanlineSpan scanlineSpan = scanLine.Begin();

            byte[] ManagedCoversArray = scanLine.GetCovers();
            for (; ; )
            {
                int x = scanlineSpan.x;
                if (scanlineSpan.len > 0)
                {
#if use_timers
                    render_scanline_aa_solid_blend_solid_hspan.Start();
#endif
                    destImage.blend_solid_hspan(x, y, scanlineSpan.len, color, ManagedCoversArray, scanlineSpan.cover_index);
#if use_timers
                    render_scanline_aa_solid_blend_solid_hspan.Stop();
#endif
                }
                else
                {
#if use_timers
                    render_scanline_aa_solid_blend_hline.Start();
#endif
                    int x2 = (x - (int)scanlineSpan.len - 1);
                    destImage.blend_hline(x, y, x2, color, ManagedCoversArray[scanlineSpan.cover_index]);
#if use_timers
                    render_scanline_aa_solid_blend_hline.Stop();
#endif
                }
                if (--num_spans == 0) break;
                scanlineSpan = scanLine.GetNextScanlineSpan();
            }
#if use_timers
            render_scanline_aa_solidTimer.Stop();
#endif
        }
        #endregion

        #region RenderSolidAllPaths
#if use_timers
        static CNamedTimer AddPathTimer = new CNamedTimer("AddPath");
        static CNamedTimer RenderSLTimer = new CNamedTimer("RenderSLs");
#endif
        //========================================================render_all_paths
        public static void RenderSolidAllPaths(IImage destImage,
            IRasterizer ras,
            IScanlineCache sl,
            IVertexSource vs,
            RGBA_Bytes[] color_storage,
            int[] path_id,
            int num_paths)
        {
            for (int i = 0; i < num_paths; i++)
            {
                ras.reset();

#if use_timers
                AddPathTimer.Start();
#endif
                ras.add_path(vs, path_id[i]);
#if use_timers
                AddPathTimer.Stop();
#endif


#if use_timers
                RenderSLTimer.Start();
#endif
                RenderSolid(destImage, ras, sl, color_storage[i]);
#if use_timers
                RenderSLTimer.Stop();
#endif
            }
        }
        #endregion

        #region GenerateAndRenderSingleScanline
        static VectorPOD<RGBA_Bytes> tempSpanColors = new VectorPOD<RGBA_Bytes>();

#if use_timers
        static CNamedTimer blend_color_hspan = new CNamedTimer("blend_color_hspan");
#endif
        //private static void GenerateAndRenderSingleScanline(IScanlineCache scanlineCache, IImage destImage, span_allocator spanAllocator, ISpanGenerator spanGenerator)
        private static void GenerateAndRenderSingleScanline(IScanlineCache sl, IImage ren,
                                span_allocator alloc, ISpanGenerator span_gen)
        {
            int y = sl.y();
            int num_spans = sl.num_spans();
            ScanlineSpan scanlineSpan = sl.Begin();

            byte[] ManagedCoversArray = sl.GetCovers();
            for (; ; )
            {
                int x = scanlineSpan.x;
                int len = scanlineSpan.len;
                if (len < 0) len = -len;

                if (tempSpanColors.capacity() < len)
                {
                    tempSpanColors.capacity(len);
                }

                span_gen.generate(tempSpanColors.Array, 0, x, y, len);
#if use_timers
                            blend_color_hspan.Start();
#endif
                bool useFirstCoverForAll = scanlineSpan.len < 0;
                ren.blend_color_hspan(x, y, len, tempSpanColors.Array, 0, ManagedCoversArray, scanlineSpan.cover_index, useFirstCoverForAll);
#if use_timers
                            blend_color_hspan.Stop();
#endif

                if (--num_spans == 0) break;
                scanlineSpan = sl.GetNextScanlineSpan();
            }
        }
        #endregion

        #region GenerateAndRender
        //=====================================================render_scanlines_aa
        public static void GenerateAndRender(IRasterizer rasterizer, IScanlineCache scanlineCache, IImage destImage, span_allocator spanAllocator, ISpanGenerator spanGenerator)
        {
            if (rasterizer.rewind_scanlines())
            {
                scanlineCache.reset(rasterizer.min_x(), rasterizer.max_x());
                spanGenerator.prepare();
                while (rasterizer.sweep_scanline(scanlineCache))
                {
                    GenerateAndRenderSingleScanline(scanlineCache, destImage, spanAllocator, spanGenerator);
                }
            }
        }
        #endregion

        #region RenderCompound
        public static void RenderCompound(rasterizer_compound_aa ras,
                                       IScanlineCache sl_aa,
                                       IScanlineCache sl_bin,
                                       IImage imageFormat,
                                       span_allocator alloc,
                                       IStyleHandler sh)
        {
#if false
            unsafe
            {
                if (ras.rewind_scanlines())
                {
                    int min_x = ras.min_x();
                    int len = ras.max_x() - min_x + 2;
                    sl_aa.reset(min_x, ras.max_x());
                    sl_bin.reset(min_x, ras.max_x());

                    //typedef typename BaseRenderer::color_type color_type;
                    ArrayPOD<RGBA_Bytes> color_span = alloc.allocate((int)len * 2);
                    byte[] ManagedCoversArray = sl_aa.GetCovers();
                    fixed (byte* pCovers = ManagedCoversArray)
                    {
                        fixed (RGBA_Bytes* pColorSpan = color_span.Array)
                        {
                            int mix_bufferOffset = len;
                            int num_spans;

                            int num_styles;
                            int style;
                            bool solid;
                            while ((num_styles = ras.sweep_styles()) > 0)
                            {
                                if (num_styles == 1)
                                {
                                    // Optimization for a single style. Happens often
                                    //-------------------------
                                    if (ras.sweep_scanline(sl_aa, 0))
                                    {
                                        style = ras.style(0);
                                        if (sh.is_solid(style))
                                        {
                                            // Just solid fill
                                            //-----------------------
                                            RenderSolidSingleScanLine(imageFormat, sl_aa, sh.color(style));
                                        }
                                        else
                                        {
                                            // Arbitrary span generator
                                            //-----------------------
                                            ScanlineSpan span_aa = sl_aa.Begin();
                                            num_spans = sl_aa.num_spans();
                                            for (; ; )
                                            {
                                                len = span_aa.len;
                                                sh.generate_span(pColorSpan,
                                                                 span_aa.x,
                                                                 sl_aa.y(),
                                                                 (int)len,
                                                                 style);

                                                imageFormat.blend_color_hspan(span_aa.x,
                                                                      sl_aa.y(),
                                                                      (int)span_aa.len,
                                                                      pColorSpan,
                                                                      &pCovers[span_aa.cover_index], 0);
                                                if (--num_spans == 0) break;
                                                span_aa = sl_aa.GetNextScanlineSpan();
                                            }
                                        }
                                    }
                                }
                                else // there are multiple styles
                                {
                                    if (ras.sweep_scanline(sl_bin, -1))
                                    {
                                        // Clear the spans of the mix_buffer
                                        //--------------------
                                        ScanlineSpan span_bin = sl_bin.Begin();
                                        num_spans = sl_bin.num_spans();
                                        for (; ; )
                                        {
                                            agg_basics.MemClear((byte*)&pColorSpan[mix_bufferOffset + span_bin.x - min_x],
                                                   span_bin.len * sizeof(RGBA_Bytes));

                                            if (--num_spans == 0) break;
                                            span_bin = sl_bin.GetNextScanlineSpan();
                                        }

                                        for (int i = 0; i < num_styles; i++)
                                        {
                                            style = ras.style(i);
                                            solid = sh.is_solid(style);

                                            if (ras.sweep_scanline(sl_aa, (int)i))
                                            {
                                                //IColorType* colors;
                                                //IColorType* cspan;
                                                //typename ScanlineAA::cover_type* covers;
                                                ScanlineSpan span_aa = sl_aa.Begin();
                                                num_spans = sl_aa.num_spans();
                                                if (solid)
                                                {
                                                    // Just solid fill
                                                    //-----------------------
                                                    for (; ; )
                                                    {
                                                        RGBA_Bytes c = sh.color(style);
                                                        len = span_aa.len;
                                                        RGBA_Bytes* colors = &pColorSpan[mix_bufferOffset + span_aa.x - min_x];
                                                        byte* covers = &pCovers[span_aa.cover_index];
                                                        do
                                                        {
                                                            if (*covers == cover_full)
                                                            {
                                                                *colors = c;
                                                            }
                                                            else
                                                            {
                                                                colors->add(c, *covers);
                                                            }
                                                            ++colors;
                                                            ++covers;
                                                        }
                                                        while (--len != 0);
                                                        if (--num_spans == 0) break;
                                                        span_aa = sl_aa.GetNextScanlineSpan();
                                                    }
                                                }
                                                else
                                                {
                                                    // Arbitrary span generator
                                                    //-----------------------
                                                    for (; ; )
                                                    {
                                                        len = span_aa.len;
                                                        RGBA_Bytes* colors = &pColorSpan[mix_bufferOffset + span_aa.x - min_x];
                                                        RGBA_Bytes* cspan = pColorSpan;
                                                        sh.generate_span(cspan,
                                                                         span_aa.x,
                                                                         sl_aa.y(),
                                                                         (int)len,
                                                                         style);
                                                        byte* covers = &pCovers[span_aa.cover_index]; 
                                                        do
                                                        {
                                                            if (*covers == cover_full)
                                                            {
                                                                *colors = *cspan;
                                                            }
                                                            else
                                                            {
                                                                colors->add(*cspan, *covers);
                                                            }
                                                            ++cspan;
                                                            ++colors;
                                                            ++covers;
                                                        }
                                                        while (--len != 0);
                                                        if (--num_spans == 0) break;
                                                        span_aa = sl_aa.GetNextScanlineSpan();
                                                    }
                                                }
                                            }
                                        }

                                        // Emit the blended result as a color hspan
                                        //-------------------------
                                        span_bin = sl_bin.Begin();
                                        num_spans = sl_bin.num_spans();
                                        for (; ; )
                                        {
                                            imageFormat.blend_color_hspan(span_bin.x,
                                                                  sl_bin.y(),
                                                                  (int)span_bin.len,
                                                                  &pColorSpan[mix_bufferOffset + span_bin.x - min_x],
                                                                  null,
                                                                  cover_full);
                                            if (--num_spans == 0) break;
                                            span_bin = sl_bin.GetNextScanlineSpan();
                                        }
                                    } // if(ras.sweep_scanline(sl_bin, -1))
                                } // if(num_styles == 1) ... else
                            } // while((num_styles = ras.sweep_styles()) > 0)
                        }
                    }
                } // if(ras.rewind_scanlines())
#endif
        }
        #endregion
    }
}
