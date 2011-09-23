using System.Collections.Generic;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.Shapes;

namespace ShowIt
{

    using NewTOAPIA.Modeling;

    /// <summary>
    /// A class that allows us to easily assign different textures
    /// to each surface.  The surfaces are named as well.
    /// </summary>
    public class AABBSurface : IRenderable
    {
        protected GraphicsInterface GI { get; set; }

        GLAxes fAxes;

        Vector3D fRoomSize;
        Vector3D fTranslation;
        Resolution fResolution;

        static GLTexture fDefaultTexture;
        Dictionary<AABBFace, AABBFaceMesh> fWalls;

        public AABBSurface(GraphicsInterface gi, Vector3D roomSize)
            : this(gi, roomSize, new Vector3D(0,roomSize.Y / 2.0f, 0), new Resolution(1,1))
        {
        }

        /// <summary>
        /// Construct a cube of the given size.  Initially, the cube starts as a
        /// unit cube, being 1 unit in all directions, with its center located 
        /// at the origin (0,0,0).  The scaling is applied, then the translation.
        /// This way, you can specify a solid rectangular volume of any dimension
        /// and put it anywhere in the scene.
        /// </summary>
        /// <param name="scale">A vector describing the scale</param>
        /// <param name="translation">A vector describing the translation</param>
        public AABBSurface(GraphicsInterface gi, Vector3D roomSize, Vector3D translation, Resolution res)
        {
            GI = gi;
            fAxes = new GLAxes(roomSize.Y);

            // Setup scale and translation
            fRoomSize = roomSize;
            fTranslation = translation;
            fResolution = res;
            fDefaultTexture = TextureHelper.CreateCheckerboardTexture(gi, 256, 256, 16);

            // Create the six walls of the room
            fWalls = new Dictionary<AABBFace, AABBFaceMesh>(6);
            AddWalls();
        }

        #region Properties
        public AABBFaceMesh GetWall(AABBFace whichWall)
        {
            if (!fWalls.ContainsKey(whichWall))
                return null;

            AABBFaceMesh aWall = fWalls[whichWall];

            return aWall;
        }

        public Vector3D Size
        {
            get { return fRoomSize; }
        }

        #endregion

        public virtual void AddWalls()
        {
            fWalls.Add(AABBFace.Front, AABBFaceMesh.CreateFace(GI, Size, AABBFace.Front, fResolution, fDefaultTexture));
            fWalls.Add(AABBFace.Back, AABBFaceMesh.CreateFace(GI, Size, AABBFace.Back, fResolution, fDefaultTexture));
            fWalls.Add(AABBFace.Left, AABBFaceMesh.CreateFace(GI, Size, AABBFace.Left, fResolution, fDefaultTexture));
            fWalls.Add(AABBFace.Right, AABBFaceMesh.CreateFace(GI, Size, AABBFace.Right, fResolution, fDefaultTexture));
            fWalls.Add(AABBFace.Floor, AABBFaceMesh.CreateFace(GI, Size, AABBFace.Floor, fResolution, fDefaultTexture));
            fWalls.Add(AABBFace.Ceiling, AABBFaceMesh.CreateFace(GI, Size, AABBFace.Ceiling, fResolution, fDefaultTexture));
        }

        public void SetWallTexture(AABBFace whichWall, GLTexture texture)
        {
            AABBFaceMesh wall = GetWall(whichWall);
            if (null == wall)
                return;

            wall.Texture = texture;
        }

        public virtual void Render(GraphicsInterface gi)
        {
            //fAxes.Render(gi);

            // Do a model translation before rendering?
            ///
            gi.PushMatrix();
            gi.Translate(fTranslation.X, fTranslation.Y, fTranslation.Z);

            foreach (AABBFaceMesh rw in fWalls.Values)
            {
                rw.Render(gi);
            }
            gi.PopMatrix();

        }
    }
}
