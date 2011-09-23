
namespace RingCamView
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using NewTOAPIA;
    using NewTOAPIA.GL;

    public class SpeakerCube : MasterCylinder
    {
        CylinderSection fSpeakerSection0;
        CylinderSection fSpeakerSection90;
        CylinderSection fSpeakerSection180;
        CylinderSection fSpeakerSection270;
        float fSpeakerSeparation;

        public SpeakerCube(GraphicsInterface gi, GLTexture faceTexture)
            :base(gi, faceTexture, 4, 4.0f, 4.0f, 10, 10, 1.0f)
        {
            Translation = new float3(0, 4.0f, 0.0);
            fSpeakerSeparation = 0.30f;

            // Sections displaying current speaker
            fSpeakerSection0 = new CylinderSection(GI, 4.0f, 4.0f, 45, 10, 10);
            fSpeakerSection90 = new CylinderSection(GI, 4.0f, 4.0f, 45, 10, 10);
            fSpeakerSection180 = new CylinderSection(GI, 4.0f, 4.0f, 45, 10, 10);
            fSpeakerSection270 = new CylinderSection(GI, 4.0f, 4.0f, 45, 10, 10);
        }

        protected override void RenderContent(GraphicsInterface GI)
        {
            // Draw Section 0
            GI.PushMatrix();
            GI.Rotate(0, 0, 1, 0);
            GI.Translate(0, 4, fSpeakerSection0.Radius * fSpeakerSeparation);
            fSpeakerSection0.Render(GI);
            GI.PopMatrix();

            // Draw Section 90
            GI.PushMatrix();
            GI.Rotate(90, 0, 1, 0);
            GI.Translate(0, 4, fSpeakerSection90.Radius * fSpeakerSeparation);
            fSpeakerSection90.Render(GI);
            GI.PopMatrix();

            // Draw Section 180
            GI.PushMatrix();
            GI.Rotate(180, 0, 1, 0);
            GI.Translate(0, 4, fSpeakerSection180.Radius * fSpeakerSeparation);
            fSpeakerSection180.Render(GI);
            GI.PopMatrix();

            // Draw Section 270
            GI.PushMatrix();
            GI.Rotate(270, 0, 1, 0);
            GI.Translate(0, 4, fSpeakerSection270.Radius * fSpeakerSeparation);
            fSpeakerSection270.Render(GI);
            GI.PopMatrix();

        }

        //protected override void EndRender(GraphicsInterface gi)
        //{
        //    fCubeTexture.Unbind();
        //}
    }
}
