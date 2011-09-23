
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.UI;
using NewTOAPIA.Drawing;
using NewTOAPIA.Net;
using NewTOAPIA.Net.Rtp;

using NewTOAPIA.Media;
using NewTOAPIA.Modeling;
using NewTOAPIA.UI.GL;

namespace ShowIt
{
    public class ShowItModel : GLModel, IReceiveConferenceFrames
    {
        #region Instance Variables
        Queue<RtpParticipant> fPendingParticipants;
        Dictionary<int, ConferenceAttendee> fAttendees;
        ConferenceSession fSession;

        // Camera Position
        Vector3f fCameraLocation;
        Vector3f fCameraRotation;
        float fExpansionFactor;
        float fExpansionRatio;
        const float largeCameraMove = 0.10f;
        const float largeCameraRotation = 10.0f;

        // Light and material Data
        float[] fLightPos = { -100.0f, 100.0f, 50.0f, 1.0f };  // Point source
        float[] fLightPosMirror = { -100.0f, -100.0f, 50.0f, 1.0f };
        float[] fNoLight = { 0.0f, 0.0f, 0.0f, 0.0f };
        float[] fLowLight = { 0.25f, 0.25f, 0.25f, 1.0f };
        float[] fBrightLight = { 1.0f, 1.0f, 1.0f, 1.0f };


        // These variables set the dimensions of the rectanglar region we wish to view.
        float fAspect;
        const double Xmin = 0.0, Xmax = 3.0;
        const double Ymin = 0.0, Ymax = 3.0;

        // The base model of the room
        List<IRenderable> fRenderables;
        ConferenceRoom fRoom;
        int nextPosition;
        #endregion

        #region Constructor
        public ShowItModel(ConferenceSession session)
        {
            fSession = session;

            fCameraLocation = new Vector3f(0.0f, -1.0f, -2.3f);
            fCameraRotation = new Vector3f(0.0f, 0.0f, 0.0f);
            fExpansionFactor = 1.0f;
            fExpansionRatio = 1.005f;

            fRenderables = new List<IRenderable>();
            fPendingParticipants = new Queue<RtpParticipant>();
            fAttendees = new Dictionary<int, ConferenceAttendee>();

        }
        #endregion

        #region Properties
        public ConferenceRoom Room
        {
            get { return fRoom; }
        }
        #endregion

        #region Receiving Frame Events
        public virtual void ReceiveDesktopFrame(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            // Find the attendee that matches the participant who sent the frame

            if (!fAttendees.ContainsKey((int)ea.RtpStream.SSRC))
                return;

            ConferenceAttendee attendee;
            attendee = fAttendees[(int)ea.RtpStream.SSRC];

            if (null != attendee)
                attendee.ReceiveDesktopFrame(sender, ea);

        }

        public virtual void ReceiveVideoFrame(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            // Find the attendee that matches the participant who sent the frame

            if (!fAttendees.ContainsKey((int)ea.RtpStream.SSRC))
                return;

            ConferenceAttendee attendee;
            attendee = fAttendees[(int)ea.RtpStream.SSRC];

            if (null != attendee)
                attendee.ReceiveVideoFrame(sender, ea);
        }

        public virtual void ReceiveAudioFrame(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            // Find the attendee that matches the participant who sent the frame

            if (!fAttendees.ContainsKey((int)ea.RtpStream.SSRC))
                return;

            ConferenceAttendee attendee;
            attendee = fAttendees[(int)ea.RtpStream.SSRC];

            if (null != attendee)
                attendee.ReceiveAudioFrame(sender, ea);
        }

        #endregion

        public void AddRenderable(IRenderable renderable)
        {
            fRenderables.Add(renderable);
        }

        public void AddPendingParticipant(RtpParticipant participant)
        {
            fPendingParticipants.Enqueue(participant);
        }

        protected virtual void CreatePendingAttendees()
        {
            foreach (RtpParticipant part in fPendingParticipants)
            {
                ConferenceAttendee attendee = new ConferenceAttendee(fSession, part);

                // The participant may have multiple ssrc values based on how
                // many channels they have open.
                // So, add all of them to the list.
                List<uint> ssrcs = part.SSRCs;

                for (int i = 0; i < ssrcs.Count; i++)
                {
                    uint ssrc = (uint)ssrcs[i];
                    fAttendees.Add((int)ssrc, attendee);
                }
            }

            fPendingParticipants.Clear();
        }

        public DeskSet CreateDeskSet(ConferenceAttendee attendee)
        {
            DeskSet aDeskSet = new DeskSet(nextPosition, attendee);
            nextPosition++;
            AddRenderable(aDeskSet);

            return aDeskSet;
        }

        protected override void  OnSetContext()
        {
            GraphicsInterface.gCheckErrors = true;

            
            // Setup the room
            // The coordinate system is based on meters 
            Vector3D roomSize = new Vector3D(4, 4, 5);
            fRoom = new ConferenceRoom(GI, roomSize);



            // Do some general GL Initialization
            GI.Features.DepthTest.Enable();

            // Medium Cyan background
            GI.Buffers.ColorBuffer.Color = ColorRGBA.MediumCyan;

            // Cull backs of polygons
            //GI.CullFace(GLFace.Back);
            GI.FrontFace(FrontFaceDirection.Ccw);
            //GI.Enable(GLOption.CullFace);


            // Setup light parameters
            //GI.LightModel(LightModelParameter.LightModelAmbient, fNoLight);
            GI.LightModel(LightModelParameter.LightModelAmbient, fBrightLight);
            GI.Features.Lighting.Light0.Ambient = new ColorRGBA(fLowLight);
            GI.Features.Lighting.Light0.Diffuse = new ColorRGBA(fBrightLight);
            GI.Features.Lighting.Light0.Specular = new ColorRGBA(fBrightLight);
            GI.Features.Lighting.Enable();
            GI.Features.Lighting.Light0.Enable();

            // Mostly use material tracking
            GI.Features.ColorMaterial.Enable();
            GI.ColorMaterial(GLFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
            GI.Material(GLFace.FrontAndBack, MaterialParameter.Shininess, 128);
        }

        void DrawRoom()
        {
            fRoom.Render(GI);
        }

        protected override void DrawBegin()
        {
            // First see if there are any new participants to create
            CreatePendingAttendees();

            base.DrawBegin();

            // Reset the coordinate system before modifying
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();

            // Set the viewing volume and use a perspective
            // projection.
            GI.Glu.Perspective(55.0f, fAspect, 0.1f, 150.0f);

            // The camera determines the initial viewing position
            // and modifies the model matrix.
            GI.MatrixMode(MatrixMode.Modelview);
            GI.LoadIdentity();
            GI.Translate(fCameraLocation);
            GI.Rotate(fCameraRotation);
        }

        protected override void DrawContent()
        {
            DrawRoom();

            //foreach (IRenderable r in fRenderables)
            //{
            //    r.Render(GI);
            //}
        }

        public override void OnSetViewport(int width, int height)
        {
            // Prevent a divide by zero, when window is too short
            // (you cant make a window of zero width).
            if (height == 0)
                height = 1;

            GI.Viewport(0, 0, width, height);

            fAspect = (float)width / (float)height;
        }

        public override IntPtr OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
        {

            if (kbde.AcitivityType == KeyActivityType.KeyDown)
            {
                switch (kbde.VirtualKeyCode)
                {
                    // Move closer to current direction
                    case VirtualKeyCodes.Up:
                        fCameraLocation.z += largeCameraMove ;
                        fExpansionFactor *= fExpansionRatio;
                        if (fExpansionFactor > 2.0)
                            fExpansionFactor = 2.0f;

                       
                        break;

                    // Move further from center
                    case VirtualKeyCodes.Down:
                        fCameraLocation.z -= largeCameraMove ;
                        fExpansionFactor *= 1 / fExpansionRatio;
                        if (fExpansionFactor < 1.0f)
                            fExpansionFactor = 1.0f;
                        break;

                    // Look to the right 
                    case VirtualKeyCodes.Right:
                        if (!kbde.Shift)
                            fCameraRotation.y += largeCameraRotation;
                        else
                            fCameraLocation.x += 0.5f;
                        break;

                    // Look to the left
                    case VirtualKeyCodes.Left:
                        if (!kbde.Shift)
                            fCameraRotation.y -= largeCameraRotation;
                        else
                            fCameraLocation.x -= 0.5f;
                        break;

                    case VirtualKeyCodes.PageUp:
                        fCameraLocation.y -= largeCameraMove;
                        break;

                    case VirtualKeyCodes.PageDown:
                        fCameraLocation.y += largeCameraMove;
                        break;

                    case VirtualKeyCodes.Space:
                        break;

                    default:
                        break;
                }

            }

            return base.OnKeyboardActivity(sender, kbde);
        }
    }
}