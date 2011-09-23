namespace NewTOAPIA.Drawing
{
    public interface IGenerator
    {
        void remove_all();
        void add_vertex(double x, double y, Path.FlagsAndCommand unknown);
        void rewind(int path_id);
        Path.FlagsAndCommand vertex(ref double x, ref double y);

        math_stroke.line_cap_e line_cap();
        math_stroke.line_join_e line_join();
        math_stroke.inner_join_e inner_join();

        void line_cap(math_stroke.line_cap_e lc);
        void line_join(math_stroke.line_join_e lj);
        void inner_join(math_stroke.inner_join_e ij);

        void width(double w);
        void miter_limit(double ml);
        void miter_limit_theta(double t);
        void inner_miter_limit(double ml);
        void approximation_scale(double approxScale);

        double width();
        double miter_limit();
        double inner_miter_limit();
        double approximation_scale();

        void shorten(double s);
        double shorten();
    }
}