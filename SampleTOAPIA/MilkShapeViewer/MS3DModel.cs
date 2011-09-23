using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

using NewTOAPIA;
using NewTOAPIA.GL;

namespace MS3D
{
    public struct byte3
    {
        public byte a, b, c;

        public byte this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return a;
                    case 1:
                        return b;
                    case 2:
                        return c;
                    default:
                        return 0;
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        a = value;
                        break;
                    case 1:
                        b = value;
                        break;
                    case 2:
                        c = value;
                        break;
                }
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct MS3DHeader
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public char[] ID;
        public int version;

        public void Init()
        {
            ID = new char[10];
        }
    }

    public struct MS3DVertex
    {
        public byte flags;
        public float3 vertex;
        public byte boneId;
        public byte referenceCount;
        public byte3 boneIds;
        public byte3 weights;
        public uint extra;
        public float3 renderColor;
    };

    public class MS3DTriangle
    {
        public ushort flags;
        public ushort[] vertexIndices;
        public float3[] vertexNormals;
        public float3 s;
        public float3 t;
        public float3 normal;
        public byte smoothingGroup;
        public byte groupIndex;

        public MS3DTriangle()
        {
            vertexIndices = new ushort[3];
            vertexNormals = new float3[3];
        }
    };

    public class MS3DGroup
    {
        public byte flags;
        public string name; // 32
        public List<ushort> triangleIndices;
        public byte materialIndex;
        public string comment;

        public MS3DGroup()
        {
            triangleIndices = new List<ushort>();
            comment = string.Empty;
        }
    };

    public class MS3DMaterial
    {
        public string name;   // 32 bytes
        public float4 ambient;
        public float4 diffuse;
        public float4 specular;
        public float4 emissive;
        public float shininess;
        public float transparency;
        public byte mode;
        public string texture;        // [MS3DModel.MAX_TEXTURE_FILENAME_SIZE];
        public string alphamap;       // [MS3DModel.MAX_TEXTURE_FILENAME_SIZE];
        public byte id;
        public string comment;

        public MS3DMaterial()
        {
            name = string.Empty;
            comment = string.Empty;
        }
    };

    //public class M3DKeyframe
    //{
    //    public float time;
    //    public float3 key;
    //};

    public class MS3DRotationFrame
    {
        public float time;
        public Quaternion mRotation;
    };
    public class MS3DTranslationFrame
    {
        public float fTime;
        public float3 fTranslation;
    };

    public struct M3DTangent
    {
        public float3 tangentIn;
        public float3 tangentOut;
    };

    public class MS3DJoint
    {
        public byte flags;
        public string name;       // 32 bytes
        public string parentName;   // 32 bytes

        public Quaternion fInitialRotation;      // Initial Rotation
        public float3 fInitialPosition;      // Initial position

        public List<MS3DRotationFrame> rotationKeys;
        public List<MS3DTranslationFrame> fPositionKeyframes;
        public List<M3DTangent> tangents;

        public string comment;
        public float3 color;

        //used for rendering
        public int parentIndex;
        public Matrix4 matLocalSkeleton;
        public Matrix4 matGlobalSkeleton;

        public Matrix4 matLocal;
        public Matrix4 matGlobal;

        public MS3DJoint()
        {
            rotationKeys = new List<MS3DRotationFrame>();
            fPositionKeyframes = new List<MS3DTranslationFrame>();
            tangents = new List<M3DTangent>();
        }
    }

    public class MS3DModel
    {
        #region Constants
        public const int MAX_VERTICES = 65534;
        public const int MAX_TRIANGLES = 65534;
        public const int MAX_GROUPS = 255;
        public const int MAX_MATERIALS = 128;
        public const int MAX_JOINTS = 128;
        public const int MAX_TEXTURE_FILENAME_SIZE = 128;

        public const int SELECTED = 1;
        public const int HIDDEN = 2;
        public const int SELECTED2 = 4;
        public const int DIRTY = 8;
        public const int ISKEY = 16;
        public const int NEWLYCREATED = 32;
        public const int MARKED = 64;

        public const int SPHEREMAP = 0x80;
        public const int HASALPHA = 0x40;
        public const int COMBINEALPHA = 0x20;

        public const int TRANSPARENCY_MODE_SIMPLE = 0;
        public const int TRANSPARENCY_MODE_DEPTHSORTEDTRIANGLES = 1;
        public const int TRANSPARENCY_MODE_ALPHAREF = 2;
        #endregion

        #region Private Fields
        long fFileSize;

        public MS3DVertex [] m_vertices;
        public List<MS3DTriangle> m_triangles;
        public List<MS3DGroup> m_groups;
        public List<MS3DMaterial> m_materials;

        public float m_animationFps;
        public float m_currentTime;
        public int m_totalFrames;

        public List<MS3DJoint> m_joints;
        public string m_comment;

        public float m_jointSize;
        public int m_transparencyMode;
        public float m_alphaRef;

        // Some size helpers
        float3 dim;
        float3 center;
        float radius;

        #endregion

        public static MS3DModel CreateFromFile(string filename)
        {
            FileStream fs = File.OpenRead(filename);

            if (null == fs)
                return null;

            MS3DModel model = CreateFromStream(fs);

            return model;
        }

        public static MS3DModel CreateFromStream(Stream aStream)
        {
            MS3DModel newModel = new MS3DModel();
            newModel.ReadFromStream(aStream);

            return newModel;
        }


        public MS3DModel()
        {
            //m_vertices = new List<MS3DVertex>();
            m_vertices = null;
            m_triangles = new List<MS3DTriangle>();
            m_groups = new List<MS3DGroup>();
            m_materials = new List<MS3DMaterial>();
            m_joints = new List<MS3DJoint>();
            m_comment = null;

            Clear();
        }

        ~MS3DModel()
        {
            Clear();
        }



        bool ReadFromStream(Stream fs)
        {
            Clear();

            fFileSize = fs.Length;
            BinaryReader fp = new BinaryReader(fs);

            // Header
            if (!ReadHeader(fp))
            {
                fp.Close();
                fs.Close();

                return false;
            }

            // vertices
            ReadVertices(fp);

            // triangles
            ReadTriangles(fp);

            // groups
            ReadGroups(fp);

            // materials
            ReadMaterials(fp);

            // animation
            ReadAnimationInformation(fp);

            // joints
            ReadJoints(fp);

            // comments
            ReadComments(fp);

            // vertex extra
            ReadVertexExtra(fp);

            // joint extra
            ReadJointExtra(fp);

            // model extra
            ReadModelExtra(fp);

            CalculateDimensions();

            fp.Close();
            fs.Close();

            return true;
        }

        bool ReadHeader(BinaryReader fp)
        {
            char[] id = fp.ReadChars(10);

            string readIdentifier = new string(id);
            string Identifier = "MS3D000000";
            if (string.Compare(readIdentifier, Identifier) != 0)
            {
                // "This is not a valid MS3D file format!"
                return false;
            }

            int version = fp.ReadInt32();
            if (version != 4)
            {
                // "This is not a valid MS3D file version!"
                return false;
            }

            return true;
        }

        void ReadVertices(BinaryReader fp)
        {
            ushort numVertices;
            numVertices = fp.ReadUInt16();
            m_vertices = new MS3DVertex[numVertices];
            m_vertices.Initialize();
            for (int i = 0; i < numVertices; i++)
            {
                //m_vertices.Add(new MS3DVertex());
                m_vertices[i].flags = fp.ReadByte();

                m_vertices[i].vertex = ReadFloat3(fp);
                m_vertices[i].boneId = fp.ReadByte();
                m_vertices[i].referenceCount = fp.ReadByte();
            }
        }

        void ReadTriangles(BinaryReader fp)
        {
            ushort numTriangles = fp.ReadUInt16();
            m_triangles.Capacity = numTriangles;
            for (int i = 0; i < numTriangles; i++)
            {
                m_triangles.Add(new MS3DTriangle());
                m_triangles[i].flags = fp.ReadUInt16();
                m_triangles[i].vertexIndices[0] = fp.ReadUInt16();
                m_triangles[i].vertexIndices[1] = fp.ReadUInt16();
                m_triangles[i].vertexIndices[2] = fp.ReadUInt16();

                // Read Vertex normals
                m_triangles[i].vertexNormals[0] = ReadFloat3(fp);
                m_triangles[i].vertexNormals[1] = ReadFloat3(fp);
                m_triangles[i].vertexNormals[2] = ReadFloat3(fp);

                m_triangles[i].s = ReadFloat3(fp);
                m_triangles[i].t = ReadFloat3(fp);

                m_triangles[i].smoothingGroup = fp.ReadByte();
                m_triangles[i].groupIndex = fp.ReadByte();

                // TODO: calculate triangle normal
            }
        }

        void ReadGroups(BinaryReader fp)
        {
            ushort numGroups;
            numGroups = fp.ReadUInt16();
            m_groups.Capacity = numGroups;
            for (int i = 0; i < numGroups; i++)
            {
                m_groups.Add(new MS3DGroup());
                m_groups[i].flags = fp.ReadByte();
                m_groups[i].name = new string(fp.ReadChars(32));
                ushort numGroupTriangles = fp.ReadUInt16();
                m_groups[i].triangleIndices.Capacity = numGroupTriangles;
                if (numGroupTriangles > 0)
                {
                    for (int ntris = 0; ntris < numGroupTriangles; ntris++)
                        m_groups[i].triangleIndices.Add(fp.ReadUInt16());
                }

                m_groups[i].materialIndex = fp.ReadByte();
            }
        }

        void ReadMaterials(BinaryReader fp)
        {
            ushort numMaterials;
            numMaterials = fp.ReadUInt16();
            m_materials.Capacity = numMaterials;
            for (int i = 0; i < numMaterials; i++)
            {
                m_materials.Add(new MS3DMaterial());
                m_materials[i].name = new string(fp.ReadChars(32));
                m_materials[i].ambient = ReadFloat4(fp);
                m_materials[i].diffuse = ReadFloat4(fp);
                m_materials[i].specular = ReadFloat4(fp);
                m_materials[i].emissive = ReadFloat4(fp);
                m_materials[i].shininess = fp.ReadSingle();
                m_materials[i].transparency = fp.ReadSingle();
                m_materials[i].mode = fp.ReadByte();
                m_materials[i].texture = new string(fp.ReadChars(MAX_TEXTURE_FILENAME_SIZE));
                m_materials[i].alphamap = new string(fp.ReadChars(MAX_TEXTURE_FILENAME_SIZE));

                // set alpha
                m_materials[i].ambient[3] = m_materials[i].transparency;
                m_materials[i].diffuse[3] = m_materials[i].transparency;
                m_materials[i].specular[3] = m_materials[i].transparency;
                m_materials[i].emissive[3] = m_materials[i].transparency;
            }
        }

        void ReadAnimationInformation(BinaryReader fp)
        {
            m_animationFps = fp.ReadSingle();
            if (m_animationFps < 1.0f)
                m_animationFps = 1.0f;

            m_currentTime = fp.ReadSingle();
            m_totalFrames = fp.ReadInt32();
        }

        void ReadJoints(BinaryReader fp)
        {
            int i, j;
            ushort numJoints;
            float3 angles = new float3();
            numJoints = fp.ReadUInt16();
            m_joints.Capacity = numJoints;
            for (i = 0; i < numJoints; i++)
            {
                m_joints.Add(new MS3DJoint());
                m_joints[i].flags = fp.ReadByte();
                m_joints[i].name = new string(fp.ReadChars(32));
                m_joints[i].parentName = new string(fp.ReadChars(32));
                angles = ReadFloat3(fp);  // Euler Angles, need to convert to quaternion
                m_joints[i].fInitialRotation = Quaternion.FromEulerAngles(angles);
                m_joints[i].fInitialPosition = ReadFloat3(fp);

                ushort numKeyFramesRot;
                numKeyFramesRot = fp.ReadUInt16();
                m_joints[i].rotationKeys.Capacity = numKeyFramesRot;

                ushort numKeyFramesPos;
                numKeyFramesPos = fp.ReadUInt16();
                m_joints[i].fPositionKeyframes.Capacity = numKeyFramesPos;

                // the frame time is in seconds, so multiply it by the animation fps, to get the frame number
                // rotation channel
                for (j = 0; j < numKeyFramesRot; j++)
                {
                    m_joints[i].rotationKeys.Add(new MS3DRotationFrame());
                    m_joints[i].rotationKeys[j].time = fp.ReadSingle();
                    m_joints[i].rotationKeys[j].time *= m_animationFps;
                    angles = ReadFloat3(fp);
                    m_joints[i].rotationKeys[j].mRotation = Quaternion.FromEulerAngles(angles);
                }

                // translation channel
                for (j = 0; j < numKeyFramesPos; j++)
                {
                    m_joints[i].fPositionKeyframes.Add(new MS3DTranslationFrame());
                    m_joints[i].fPositionKeyframes[j].fTime = fp.ReadSingle();
                    m_joints[i].fPositionKeyframes[j].fTime *= m_animationFps;
                    m_joints[i].fPositionKeyframes[j].fTranslation = ReadFloat3(fp);
                }
            }

            InitializeJoints();
        }

        int FindJointIndex(string name)
        {
            for (int i = 0; i < Joints.Count; i++)
            {
                MS3DJoint joint = Joints[i];
                if (string.Compare(joint.name, name) == 0)
                    return i;
            }

            return -1;
        }
        
        void InitializeJoints()
        {
            foreach (MS3DJoint joint in Joints)
            {
                // Get the parent index so a lookup does not have to occur
                // during rendering.
                joint.parentIndex = FindJointIndex(joint.parentName);
            }
        }

        void ReadComments(BinaryReader fp)
        {
            long filePos = fp.BaseStream.Position;
            int i;

            if (filePos < fFileSize)
            {
                int subVersion = fp.ReadInt32();
                if (subVersion == 1)
                {
                    int numComments = 0;
                    int commentSize = 0;

                    // group comments
                    numComments = fp.ReadInt32();
                    for (i = 0; i < numComments; i++)
                    {
                        int index;
                        index = fp.ReadInt32();
                        commentSize = fp.ReadInt32();
                        char[] comment = null;
                        if (commentSize > 0)
                            comment = fp.ReadChars(commentSize);
                        if (index >= 0 && index < (int)m_groups.Count)
                            m_groups[index].comment = new string(comment);
                    }

                    // material comments
                    numComments = fp.ReadInt32();
                    for (i = 0; i < numComments; i++)
                    {
                        int index;
                        index = fp.ReadInt32();
                        commentSize = fp.ReadInt32();
                        char[] comment = null;
                        if (commentSize > 0)
                            comment = fp.ReadChars(commentSize);
                        if (index >= 0 && index < (int)m_materials.Count)
                            m_materials[index].comment = new string(comment);
                    }

                    // joint comments
                    numComments = fp.ReadInt32();
                    for (i = 0; i < numComments; i++)
                    {
                        int index;
                        index = fp.ReadInt32();
                        commentSize = fp.ReadInt32();
                        char[] comment = null;
                        if (commentSize > 0)
                            comment = fp.ReadChars(commentSize);
                        if (index >= 0 && index < (int)m_materials.Count)
                            m_joints[index].comment = new string(comment);
                    }


                    // model comments
                    numComments = fp.ReadInt32();
                    if (numComments == 1)
                    {
                        commentSize = fp.ReadInt32();
                        char[] comment = null;
                        if (commentSize > 0)
                            comment = fp.ReadChars(commentSize);
                        m_comment = new string(comment);
                    }
                }
                else
                {
                    // "Unknown subversion for comments %d\n", subVersion);
                }
            }
        }

        void ReadVertexExtra(BinaryReader fp)
        {
            long filePos = fp.BaseStream.Position;
            if (filePos < fFileSize)
            {
                int subVersion = 0;
                subVersion = fp.ReadInt32();
                if (subVersion == 2)
                {
                    for (int ctr1 = 0; ctr1 < Vertices.Length; ctr1++)
                    {
                        m_vertices[ctr1].boneIds = ReadByte3(fp);
                        m_vertices[ctr1].weights = ReadByte3(fp);
                        m_vertices[ctr1].extra = fp.ReadUInt32();
                    }
                }
                else if (subVersion == 1)
                {
                    for (int ctr2 = 0; ctr2 < Vertices.Length; ctr2++)
                    {
                        m_vertices[ctr2].boneIds = ReadByte3(fp);
                        m_vertices[ctr2].weights = ReadByte3(fp);
                    }
                }
                else
                {
                    // "Unknown subversion for vertex extra %d\n", subVersion);
                }
            }

        }

        void ReadJointExtra(BinaryReader fp)
        {
            long filePos = fp.BaseStream.Position;
            if (filePos < fFileSize)
            {
                int subVersion = 0;
                subVersion = fp.ReadInt32();
                if (subVersion == 1)
                {
                    for (int ctr3 = 0; ctr3 < m_joints.Count; ctr3++)
                    {
                        m_joints[ctr3].color = ReadFloat3(fp);
                    }
                }
                else
                {
                    // "Unknown subversion for joint extra %d\n", subVersion);
                }
            }
        }

        void ReadModelExtra(BinaryReader fp)
        {
            long filePos = fp.BaseStream.Position;
            if (filePos < fFileSize)
            {
                int subVersion = 0;
                subVersion = fp.ReadInt32();
                if (subVersion == 1)
                {
                    m_jointSize = fp.ReadSingle();
                    m_transparencyMode = fp.ReadInt32();
                    m_alphaRef = fp.ReadSingle();
                }
                else
                {
                    //"Unknown subversion for model extra %d\n", subVersion);
                }
            }
        }

        void CalculateDimensions()
        {
            // Calculate dimensions
            float3 mins = new float3();
            float3 maxs = new float3();
            MS3DMath.ClearBounds(ref mins, ref maxs);
            foreach (MS3DVertex vertex in Vertices)
            {
                MS3DMath.AddPointToBounds(vertex.vertex, ref mins, ref maxs);
            }

            dim[0] = maxs[0] - mins[0];
            dim[1] = maxs[1] - mins[1];
            dim[2] = maxs[2] - mins[2];
            center[0] = mins[0] + dim[0] / 2.0f;
            center[1] = mins[1] + dim[1] / 2.0f;
            center[2] = mins[2] + dim[2] / 2.0f;
            radius = dim[0];
            if (dim[1] > radius)
                radius = dim[1];
            if (dim[2] > radius)
                radius = dim[2];
            radius *= 1.41f;
        }

        void Clear()
        {
            //m_vertices.Clear();
            m_triangles.Clear();
            m_groups.Clear();
            m_materials.Clear();
            m_animationFps = 24.0f;
            m_currentTime = 1.0f;
            m_totalFrames = 30;
            m_joints.Clear();
            m_comment = string.Empty;
            m_jointSize = 1.0f;
            m_transparencyMode = TRANSPARENCY_MODE_SIMPLE;
            m_alphaRef = 0.5f;
        }

        #region Properties
        public float AlphaRef
        {
            get {return m_alphaRef;}
        }

        public float AnimationFps
        {
            get { return m_animationFps; }
        }

        public float3 Center
        {
            get { return center; }
        }

        public float3 Dimension
        {
            get { return dim; }
        }

        public float Radius
        {
            get { return radius; }
        }

        public int TotalFrames
        {
            get { return m_totalFrames; }
        }

        public List<MS3DGroup> Groups
        {
            get { return m_groups; }
        }

        public List<MS3DJoint> Joints
        {
            get { return m_joints; }
        }

        public float JointSize
        {
            get { return m_jointSize; }
        }

        public List<MS3DMaterial> Materials
        {
            get { return m_materials; }
        }

        public int TransparencyMode
        {
            get { return m_transparencyMode; }
        }

        public List<MS3DTriangle> Triangles
        {
            get { return m_triangles; }
        }

        public MS3DVertex[] Vertices
        {
            get { return m_vertices; }
        }
        #endregion

        #region Helper Functions
        byte3 ReadByte3(BinaryReader reader)
        {
            byte3 newByte3 = new byte3();

            newByte3.a = reader.ReadByte();
            newByte3.b = reader.ReadByte();
            newByte3.c = reader.ReadByte();

            return newByte3;
        }

        float3 ReadFloat3(BinaryReader reader)
        {
            float3 newFloat3 = new float3();

            newFloat3.x = reader.ReadSingle();
            newFloat3.y = reader.ReadSingle();
            newFloat3.z = reader.ReadSingle();

            return newFloat3;
        }

        float4 ReadFloat4(BinaryReader reader)
        {
            float4 newFloat4 = new float4();

            newFloat4.x = reader.ReadSingle();
            newFloat4.y = reader.ReadSingle();
            newFloat4.z = reader.ReadSingle();
            newFloat4.w = reader.ReadSingle();

            return newFloat4;
        }
        #endregion
    };
}

