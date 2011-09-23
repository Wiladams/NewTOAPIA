namespace NewTOAPIA.Drawing
{
    public sealed class conv_stroke : conv_adaptor_vcgen, IVertexSource
    {
        public conv_stroke(IVertexSource vs)
            : base(vs, new vcgen_stroke())
        {
        }

        public void line_cap(math_stroke.line_cap_e lc) { base.generator().line_cap(lc); }
        public void line_join(math_stroke.line_join_e lj) { base.generator().line_join(lj); }
        public void inner_join(math_stroke.inner_join_e ij) { base.generator().inner_join(ij); }

        public math_stroke.line_cap_e line_cap() { return base.generator().line_cap(); }
        public math_stroke.line_join_e line_join() { return base.generator().line_join(); }
        public math_stroke.inner_join_e inner_join() { return base.generator().inner_join(); }

        public void width(double w) { base.generator().width(w); }
        public void miter_limit(double ml) { base.generator().miter_limit(ml); }
        public void miter_limit_theta(double t) { base.generator().miter_limit_theta(t); }
        public void inner_miter_limit(double ml) { base.generator().inner_miter_limit(ml); }
        public void approximation_scale(double approxScale) { base.generator().approximation_scale(approxScale); }

        public double width() { return base.generator().width(); }
        public double miter_limit() { return base.generator().miter_limit(); }
        public double inner_miter_limit() { return base.generator().inner_miter_limit(); }
        public double approximation_scale() { return base.generator().approximation_scale(); }

        public void shorten(double s) { base.generator().shorten(s); }
        public double shorten() { return base.generator().shorten(); }
    }
}