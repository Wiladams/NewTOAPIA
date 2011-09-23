using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Modeling
{
    public struct Particle
    {
        public float3 Position;
        public float3 PreviousPosition;
        public float3 Velocity;
        public float3 Acceleration;

        public float Mass;
        public float MassInverse;
        public float MassDelta;

        public float Energy;    // How long the particle is alive

        public float Size;
        public float SizeDelta;


        public ColorRGBA Color;
        public ColorRGBA ColorDelta;
    }
}
