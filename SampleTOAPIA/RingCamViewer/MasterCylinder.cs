
namespace RingCamView
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using NewTOAPIA;
    using NewTOAPIA.GL;

    public class MasterCylinder : GLRenderable
    {
        public GraphicsInterface GI { get; protected set; }
        public GLTexture CylinderTexture { get; set; }
        public float ExpansionFactor { get; set; }
        protected float3 Translation;

        int NumberOfSections;
        float Radius;
        float Height;
        float ArcDegrees;
        int Stacks;
        int Slices;

        List<CylinderSection> Sections = new List<CylinderSection>();

        public MasterCylinder(GraphicsInterface gi, GLTexture texture, int numberOfSections, float radius, float height, int stacks, int slices, float expanseFactor)
        {
            GI = gi;
            CylinderTexture = texture;

            NumberOfSections = numberOfSections;
            Radius = radius;
            Height = height;
            Stacks = stacks;
            Slices = slices;
            ArcDegrees = 360 / numberOfSections;
            ExpansionFactor = expanseFactor;

            ConstructSections();
        }

        protected virtual void ConstructSections()
        {
            for (int i = 1; i <= NumberOfSections; i++)
            {
                RectangleF textureRect = new RectangleF((float)(NumberOfSections - i) / NumberOfSections, 1, 1.0f / NumberOfSections, 1);
                CylinderSection newSection = new CylinderSection(GI, Radius, Height, ArcDegrees, Stacks, Slices, textureRect);
                Sections.Add(newSection);
            }
        }

        protected override void BeginRender(GraphicsInterface GI)
        {
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Clamp);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Clamp);
            GI.TexEnv(TextureEnvModeParam.Modulate);
            GI.Features.Texturing2D.Enable();


            // Set drawing color to white
            GI.Drawing.Color = ColorRGBA.White;
            GI.FrontFace(FrontFaceDirection.Cw);
            GI.PolygonMode(GLFace.Front, PolygonMode.Fill);

            CylinderTexture.Bind();
        }

        protected override void RenderContent(GraphicsInterface GI)
        {
            for (int i = 0; i < NumberOfSections; i++)
            {
                float3 trans = Translation + new float3(0, 0, -Radius * ExpansionFactor);
                // Draw Section 0
                GI.PushMatrix();
                GI.Rotate(i*ArcDegrees, 0, 1, 0);
                GI.Translate(trans.x, trans.y, trans.z);
                Sections[i].Render(GI);
                GI.PopMatrix();
            }
        }

        protected override void EndRender(GraphicsInterface GI)
        {
            CylinderTexture.Unbind();
            GI.Features.Texturing2D.Disable();
        }

    }
}
