using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.Modeling;

namespace ShowIt
{
    public  class ConferenceRoom : AABBSurface
    {
        WebCamTexture localCamProjector;
        Whiteboard whiteboard;
        PedestalTable fTable;
        List<IRenderable> fChildren;

        GLTexture fWallTexture;
        GLTexture fFloorTexture;
        GLTexture fCeilingTexture;

        public ConferenceRoom(GraphicsInterface gi, Vector3D roomSize)
            : base(gi, roomSize, new Vector3D(0, roomSize.Y / 2.0f, 0), new Resolution(10, 10))
        {


            fWallTexture = TextureHelper.CreateTextureFromFile(gi, "Textures\\Wall.tiff", false);
            fCeilingTexture = TextureHelper.CreateTextureFromFile(gi, "Textures\\Ceiling.tiff", false);
            fFloorTexture = TextureHelper.CreateTextureFromFile(gi, "Textures\\Carpet_berber_dirt.jpg", false);

            fChildren = new List<IRenderable>();

            SetWallTexture(AABBFace.Ceiling, fCeilingTexture);
            SetWallTexture(AABBFace.Floor, fFloorTexture);

            SetWallTexture(AABBFace.Front, fWallTexture);
            SetWallTexture(AABBFace.Back, fWallTexture);
            SetWallTexture(AABBFace.Left, fWallTexture);
            SetWallTexture(AABBFace.Right, fWallTexture);

            Vector3D wbSize = new Vector3D(4.0f-(0.305f*2.0f), 3.0f, 0.01f);
            Point3D wbTrans = new Point3D(0, (wbSize.Y/2)+(roomSize.Y-wbSize.Y)/2, -roomSize.Z / 2.0f+.02f);
            AABB wbBB = new AABB(wbSize, wbTrans);
            whiteboard = new Whiteboard(gi, wbBB);
            //whiteboard.ImageSource = localCamProjector;
            
            fTable = new PedestalTable(gi);

            
            
            fChildren.Add(whiteboard);
            fChildren.Add(fTable);
        }

        #region Properties
        public Whiteboard Whiteboard
        {
            get { return whiteboard; }
        }

        #endregion

        public override void Render(GraphicsInterface gi)
        {
            // Render the basic room
            base.Render(gi);

            // Render all the furniture and equipment
            foreach (IRenderable r in fChildren)
            {
                r.Render(gi);
            }
        }
    }
}
