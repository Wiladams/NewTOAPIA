﻿namespace NewTOAPIA.Drawing
{
    using System;

    public static class Path
    {
        [Flags]
        public enum FlagsAndCommand
        {
            CommandStop = 0,
            CommandMoveTo = 1,
            CommandLineTo = 2,
            CommandCurve3 = 3,
            CommandCurve4 = 4,
            //CommandCurveN   = 5, // unused [3/10/2009 lbrubaker]
            //CommandCatRom   = 6, // unused [3/10/2009 lbrubaker]
            //CommandUBSpline = 7, // unused [3/10/2009 lbrubaker]
            CommandEndPoly = 0x0F,
            CommandsMask = 0x0F,

            FlagNone = 0,
            FlagCCW = 0x10,
            FlagCW = 0x20,
            FlagClose = 0x40,
            FlagsMask = 0xF0
        };

        public static bool is_vertex(FlagsAndCommand c)
        {
            return c >= FlagsAndCommand.CommandMoveTo
                && c < FlagsAndCommand.CommandEndPoly;
        }

        public static bool is_drawing(FlagsAndCommand c)
        {
            return c >= FlagsAndCommand.CommandLineTo && c < FlagsAndCommand.CommandEndPoly;
        }

        public static bool is_stop(FlagsAndCommand c)
        {
            return c == FlagsAndCommand.CommandStop;
        }

        public static bool is_move_to(FlagsAndCommand c)
        {
            return c == FlagsAndCommand.CommandMoveTo;
        }

        public static bool is_line_to(FlagsAndCommand c)
        {
            return c == FlagsAndCommand.CommandLineTo;
        }

        public static bool is_curve(FlagsAndCommand c)
        {
            return c == FlagsAndCommand.CommandCurve3
                || c == FlagsAndCommand.CommandCurve4;
        }

        public static bool is_curve3(FlagsAndCommand c)
        {
            return c == FlagsAndCommand.CommandCurve3;
        }

        public static bool is_curve4(FlagsAndCommand c)
        {
            return c == FlagsAndCommand.CommandCurve4;
        }

        public static bool is_end_poly(FlagsAndCommand c)
        {
            return (c & FlagsAndCommand.CommandsMask) == FlagsAndCommand.CommandEndPoly;
        }

        public static bool is_close(FlagsAndCommand c)
        {
            return (c & ~(FlagsAndCommand.FlagCW | FlagsAndCommand.FlagCCW)) ==
                   (FlagsAndCommand.CommandEndPoly | FlagsAndCommand.FlagClose);
        }

        public static bool is_next_poly(FlagsAndCommand c)
        {
            return is_stop(c) || is_move_to(c) || is_end_poly(c);
        }

        public static bool is_cw(FlagsAndCommand c)
        {
            return (c & FlagsAndCommand.FlagCW) != 0;
        }

        public static bool is_ccw(FlagsAndCommand c)
        {
            return (c & FlagsAndCommand.FlagCCW) != 0;
        }

        public static bool is_oriented(FlagsAndCommand c)
        {
            return (c & (FlagsAndCommand.FlagCW | FlagsAndCommand.FlagCCW)) != 0;
        }

        public static bool is_closed(FlagsAndCommand c)
        {
            return (c & FlagsAndCommand.FlagClose) != 0;
        }

        public static FlagsAndCommand get_close_flag(FlagsAndCommand c)
        {
            return (FlagsAndCommand)(c & FlagsAndCommand.FlagClose);
        }

        public static FlagsAndCommand clear_orientation(FlagsAndCommand c)
        {
            return c & ~(FlagsAndCommand.FlagCW | FlagsAndCommand.FlagCCW);
        }

        public static FlagsAndCommand get_orientation(FlagsAndCommand c)
        {
            return c & (FlagsAndCommand.FlagCW | FlagsAndCommand.FlagCCW);
        }

        /*
        //---------------------------------------------------------set_orientation
        public static path_flags_e set_orientation(int c, path_flags_e o)
        {
            return clear_orientation(c) | o;
        }
         */

        static public void shorten_path(VertexSequence vs, double s)
        {
            shorten_path(vs, s, 0);
        }

        static public void shorten_path(VertexSequence vs, double s, int closed)
        {
            if (s > 0.0 && vs.Count > 1)
            {
                double d;
                int n = (int)(vs.Count - 2);
                while (n != 0)
                {
                    d = vs[n].dist;
                    if (d > s) break;
                    vs.RemoveLast();
                    s -= d;
                    --n;
                }
                if (vs.Count < 2)
                {
                    vs.Clear();
                }
                else
                {
                    n = (int)vs.Count - 1;
                    VertexWithDistance prev = vs[n - 1];
                    VertexWithDistance last = vs[n];
                    d = (prev.dist - s) / prev.dist;
                    double x = prev.x + (last.x - prev.x) * d;
                    double y = prev.y + (last.y - prev.y) * d;
                    last.x = x;
                    last.y = y;
                    
                    if (!prev.IsEqual(last)) 
                        vs.RemoveLast();
                    
                    vs.close(closed != 0);
                }
            }
        }
    }
}