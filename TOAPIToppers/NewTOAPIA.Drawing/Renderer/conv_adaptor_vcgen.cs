
namespace NewTOAPIA.Drawing
{
    public struct null_markers : IMarkers
    {
        public void remove_all() { }
        public void add_vertex(double x, double y, Path.FlagsAndCommand unknown) { }
        public void prepare_src() { }

        public void rewind(int unknown) { }
        public Path.FlagsAndCommand vertex(ref double x, ref double y) { return Path.FlagsAndCommand.CommandStop; }
    };

    //------------------------------------------------------conv_adaptor_vcgen
    public class conv_adaptor_vcgen
    {
        private enum status
        {
            initial,
            accumulate,
            generate
        };

        public conv_adaptor_vcgen(IVertexSource source, IGenerator generator)
        {
            m_markers = new null_markers();
            m_source = source;
            m_generator = generator;
            m_status = status.initial;
        }

        public conv_adaptor_vcgen(IVertexSource source, IGenerator generator, IMarkers markers)
            : this(source, generator)
        {
            m_markers = markers;
        }
        void attach(IVertexSource source) { m_source = source; }

        protected IGenerator generator() { return m_generator; }

        IMarkers markers() { return m_markers; }

        public void rewind(int path_id)
        {
            m_source.rewind(path_id);
            m_status = status.initial;
        }

        public Path.FlagsAndCommand vertex(out double x, out double y)
        {
            x = 0;
            y = 0;
            Path.FlagsAndCommand cmd = Path.FlagsAndCommand.CommandStop;
            bool done = false;
            while (!done)
            {
                switch (m_status)
                {
                    case status.initial:
                        m_markers.remove_all();
                        m_last_cmd = m_source.vertex(out m_start_x, out m_start_y);
                        m_status = status.accumulate;
                        goto case status.accumulate;

                    case status.accumulate:
                        if (Path.is_stop(m_last_cmd)) return Path.FlagsAndCommand.CommandStop;

                        m_generator.remove_all();
                        m_generator.add_vertex(m_start_x, m_start_y, Path.FlagsAndCommand.CommandMoveTo);
                        m_markers.add_vertex(m_start_x, m_start_y, Path.FlagsAndCommand.CommandMoveTo);

                        for (; ; )
                        {
                            cmd = m_source.vertex(out x, out y);
                            //DebugFile.Print("x=" + x.ToString() + " y=" + y.ToString() + "\n");
                            if (Path.is_vertex(cmd))
                            {
                                m_last_cmd = cmd;
                                if (Path.is_move_to(cmd))
                                {
                                    m_start_x = x;
                                    m_start_y = y;
                                    break;
                                }
                                m_generator.add_vertex(x, y, cmd);
                                m_markers.add_vertex(x, y, Path.FlagsAndCommand.CommandLineTo);
                            }
                            else
                            {
                                if (Path.is_stop(cmd))
                                {
                                    m_last_cmd = Path.FlagsAndCommand.CommandStop;
                                    break;
                                }
                                if (Path.is_end_poly(cmd))
                                {
                                    m_generator.add_vertex(x, y, cmd);
                                    break;
                                }
                            }
                        }
                        m_generator.rewind(0);
                        m_status = status.generate;
                        goto case status.generate;

                    case status.generate:
                        cmd = m_generator.vertex(ref x, ref y);
                        //DebugFile.Print("x=" + x.ToString() + " y=" + y.ToString() + "\n");
                        if (Path.is_stop(cmd))
                        {
                            m_status = status.accumulate;
                            break;
                        }
                        done = true;
                        break;
                }
            }
            return cmd;
        }

        private IVertexSource m_source;
        private IGenerator m_generator;
        private IMarkers m_markers;
        private status m_status;
        private Path.FlagsAndCommand m_last_cmd;
        private double m_start_x;
        private double m_start_y;
    }
}