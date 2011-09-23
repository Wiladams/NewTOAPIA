using System;
using System.Collections.Generic;

using NewTOAPIA;
using NewTOAPIA.GL;

namespace MS3D
{
    public class MS3DModelRenderer
    {
        public enum JointDrawStyle
        {
            eJointLines,
            eJointPoints
        }

        MS3DModel fModel;

        MS3DModelAnimator fModelAnimator;
        GraphicsInterface fGI;

        public MS3DModelRenderer(MS3DModel model)
        {
            fModel = model;
            fModelAnimator = new MS3DModelAnimator(fModel);
            fModelAnimator.SetupJoints();
            fModelAnimator.SetFrame(-1.0f);
        }

        ~MS3DModelRenderer()
        {
        }

        GraphicsInterface GI
        {
            get { return fGI; }
        }

        public void RenderBaseModel(GraphicsInterface gi, bool withMaterial, bool flatShaded)
        {
            fGI = gi;

            foreach (MS3DGroup group in fModel.Groups)
            {
                if (withMaterial)
                    BindMaterial(group.materialIndex);
                else
                    BindMaterial(-1);

                gi.Begin(BeginMode.Triangles);
                foreach (MS3DTriangle triangle in fModel.Triangles)
                {
                    for (int v = 0; v < 3; v++)
                    {
                        MS3DVertex vertex = fModel.Vertices[triangle.vertexIndices[v]];

                        gi.TexCoord(triangle.s[v], triangle.t[v]);

                        float3 normal = new float3();
                        if (flatShaded)
                            normal = triangle.normal;
                        else
                            normal = triangle.vertexNormals[v];

                        gi.Normal(normal);

                        gi.Vertex(vertex.vertex);
                    }
                }

                gi.End();
            }

        }

        public void Render(GraphicsInterface gi, bool withMaterial, bool flatShaded)
        {
            fGI = gi;

            foreach(MS3DGroup group in fModel.Groups)
            {
                if (withMaterial)
                    BindMaterial(group.materialIndex);
                else
                    BindMaterial(-1);

                gi.Begin(BeginMode.Triangles);
                foreach(MS3DTriangle triangle in fModel.Triangles)
                {
                    for (int v = 0; v < 3; v++)
                    {
                        MS3DVertex vertex = fModel.Vertices[triangle.vertexIndices[v]];

                        gi.TexCoord(triangle.s[v], triangle.t[v]);

                        float3 normal = new float3();
                        if (flatShaded)
                            TransformNormal(vertex, triangle.normal, normal);
                        else
                            TransformNormal(vertex, triangle.vertexNormals[v], normal);

                        gi.Normal(normal);

                        float3 pos = new float3();
                        TransformVertex(vertex, ref pos);
                        gi.Vertex(pos);
                    }
                }

                gi.End();
            }
        }

        void RenderJoints(JointDrawStyle what)
        {
            if (JointDrawStyle.eJointLines == what)
            {
                GI.Begin(BeginMode.Lines);
                foreach(MS3DJoint joint in fModel.Joints)
                {
                    if (joint.parentIndex == -1)
                    {
                        //GI.Vertex(joint.matGlobal[0][3], joint.matGlobal[1][3], joint.matGlobal[2][3]);
                        //GI.Vertex(joint.matGlobal[0][3], joint.matGlobal[1][3], joint.matGlobal[2][3]);
                    }
                    else
                    {
                        MS3DJoint parentJoint = fModel.Joints[joint.parentIndex];
                        //GI.Vertex(joint.matGlobal[0][3], joint.matGlobal[1][3], joint.matGlobal[2][3]);
                        //GI.Vertex(parentJoint.matGlobal[0][3], parentJoint.matGlobal[1][3], parentJoint.matGlobal[2][3]);
                    }
                }
                GI.End();
            }
            else if (JointDrawStyle.eJointPoints == what)
            {
                GI.Begin(BeginMode.Points);
                foreach (MS3DJoint joint in fModel.Joints)
                {
                    //GI.Vertex(joint.matGlobal[0][3], joint.matGlobal[1][3], joint.matGlobal[2][3]);
                }
                GI.End();
            }
        }

        void BindMaterial(int materialIndex)
        {
            if ((materialIndex < 0) || (materialIndex >= fModel.Materials.Count))
            {
                GI.DepthMask(true);
                //GI.Disable(GLOption.AlphaTest);
                //GI.Disable(GLOption.TextureGenS);
                //GI.Disable(GLOption.TextureGenT);
                GI.Features.AlphaTest.Disable();
                GI.Features.Texturing2D.TextureGenS.Disable();
                GI.Features.Texturing2D.TextureGenT.Disable();

                GI.Color4(1, 1, 1, 1);
                GI.Features.Texturing2D.Disable();
                GI.Features.Blend.Disable();

                GI.BindTexture(TextureBindTarget.Texture2d, 0);
                float[] ma = { 0.2f, 0.2f, 0.2f, 1.0f };
                float[] md = { 0.8f, 0.8f, 0.8f, 1.0f };
                float[] ms = { 0.0f, 0.0f, 0.0f, 1.0f };
                float[] me = { 0.0f, 0.0f, 0.0f, 1.0f };
                float mss = 0.0f;
                GI.Material(GLFace.FrontAndBack, MaterialParameter.Ambient, ma);
                GI.Material(GLFace.FrontAndBack, MaterialParameter.Diffuse, md);
                GI.Material(GLFace.FrontAndBack, MaterialParameter.Specular, ms);
                GI.Material(GLFace.FrontAndBack, MaterialParameter.Emission, me);
                GI.Material(GLFace.FrontAndBack, MaterialParameter.Shininess, mss);
            }
            else
            {
                MS3DMaterial material = fModel.Materials[materialIndex];
                GI.Features.Texturing2D.Enable();

                if ((material.transparency < 1.0f) || (0 != (material.mode & MS3DModel.HASALPHA)))
                {
                    GI.Features.Blend.Enable();
                    GI.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                    GI.Color(1.0f, 1.0f, 1.0f, material.transparency);
                    GI.LightModel(LightModelParameter.LightModelTwoSide, 1);

                    if (fModel.TransparencyMode == MS3DModel.TRANSPARENCY_MODE_SIMPLE)
                    {
                        GI.DepthMask(false);
                        GI.Features.AlphaTest.Enable();
                        GI.AlphaFunc(AlphaFunction.Greater, 0.0f);
                    }
                    else if (fModel.TransparencyMode == MS3DModel.TRANSPARENCY_MODE_ALPHAREF)
                    {
                        GI.DepthMask(true);
                        GI.Features.AlphaTest.Enable();
                        GI.AlphaFunc(AlphaFunction.Greater, fModel.AlphaRef);
                    }
                }
                else
                {
                    GI.Features.Blend.Disable();
                    GI.Color(1.0f, 1.0f, 1.0f, 1.0f);
                    GI.LightModel(LightModelParameter.LightModelTwoSide, 0);
                }

                if ((material.mode & MS3DModel.SPHEREMAP) != 0)
                {
                    GI.Features.Texturing2D.TextureGenS.Enable();
                    GI.Features.Texturing2D.TextureGenT.Enable();
                    GI.TexGen(TextureCoordName.S, TextureGenParameter.TextureGenMode, TextureGenMode.SphereMap);
                    GI.TexGen(TextureCoordName.T, TextureGenParameter.TextureGenMode, TextureGenMode.SphereMap);
                }
                else
                {
                    GI.Features.Texturing2D.TextureGenS.Disable();
                    GI.Features.Texturing2D.TextureGenT.Disable();
                }
                GI.BindTexture(TextureBindTarget.Texture2d, material.id);

                GI.Material(GLFace.FrontAndBack, MaterialParameter.Ambient, material.ambient);
                GI.Material(GLFace.FrontAndBack, MaterialParameter.Diffuse, material.diffuse);
                GI.Material(GLFace.FrontAndBack, MaterialParameter.Specular, material.specular);
                GI.Material(GLFace.FrontAndBack, MaterialParameter.Emission, material.emissive);
                GI.Material(GLFace.FrontAndBack, MaterialParameter.Shininess, material.shininess);
            }
        }

        public void TransformVertex(MS3DVertex vertex, ref float3 vout)
        {
            int[] jointIndices = new int[4];
            int[] jointWeights = new int[4];

            FillJointIndicesAndWeights(vertex, jointIndices, jointWeights);

            if (jointIndices[0] < 0 || jointIndices[0] >= (int)fModel.Joints.Count || fModel.m_currentTime < 0.0f)
            {
                vout[0] = vertex.vertex[0];
                vout[1] = vertex.vertex[1];
                vout[2] = vertex.vertex[2];
            }
            else
            {
                // count valid weights
                int numWeights = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (jointWeights[i] > 0 && jointIndices[i] >= 0 && jointIndices[i] < (int)fModel.Joints.Count)
                        ++numWeights;
                    else
                        break;
                }

                // init
                vout[0] = 0.0f;
                vout[1] = 0.0f;
                vout[2] = 0.0f;

                float[] weights = { (float)jointWeights[0] / 100.0f, (float)jointWeights[1] / 100.0f, (float)jointWeights[2] / 100.0f, (float)jointWeights[3] / 100.0f };
                if (numWeights == 0)
                {
                    numWeights = 1;
                    weights[0] = 1.0f;
                }
                // add weighted vertices
                for (int i = 0; i < numWeights; i++)
                {
                    MS3DJoint joint = fModel.Joints[jointIndices[i]];
                    float3 tmp = new float3();
                    float3 vert = new float3();
                    //MS3DMath.VectorITransform(vertex.vertex, joint.matGlobalSkeleton, ref tmp);
                    //MS3DMath.VectorTransform(tmp, joint.matGlobal, ref vert);

                    vout[0] += vert[0] * weights[i];
                    vout[1] += vert[1] * weights[i];
                    vout[2] += vert[2] * weights[i];
                }
            }
        }

        public void TransformNormal(MS3DVertex vertex, float3 normal, float3 nout)
        {
            int[] jointIndices = new int[4];
            int[] jointWeights = new int[4];

            FillJointIndicesAndWeights(vertex, jointIndices, jointWeights);

            if (jointIndices[0] < 0 || jointIndices[0] >= (int)fModel.Joints.Count || fModel.m_currentTime < 0.0f)
            {
                nout[0] = normal[0];
                nout[1] = normal[1];
                nout[2] = normal[2];
            }
            else
            {
                // count valid weights
                int numWeights = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (jointWeights[i] > 0 && jointIndices[i] >= 0 && jointIndices[i] < (int)fModel.Joints.Count)
                        ++numWeights;
                    else
                        break;
                }

                // init
                nout[0] = 0.0f;
                nout[1] = 0.0f;
                nout[2] = 0.0f;

                float[] weights = { (float)jointWeights[0] / 100.0f, (float)jointWeights[1] / 100.0f, (float)jointWeights[2] / 100.0f, (float)jointWeights[3] / 100.0f };
                if (numWeights == 0)
                {
                    numWeights = 1;
                    weights[0] = 1.0f;
                }

                // add weighted vertices
                for (int i = 0; i < numWeights; i++)
                {
                    MS3DJoint joint = fModel.Joints[jointIndices[i]];
                    float3 tmp = new float3();
                    float3 norm = new float3();
                    //MS3DMath.VectorIRotate(normal, joint.matGlobalSkeleton, ref tmp);
                    //MS3DMath.VectorRotate(tmp, joint.matGlobal, ref norm);

                    nout[0] += norm[0] * weights[i];
                    nout[1] += norm[1] * weights[i];
                    nout[2] += norm[2] * weights[i];
                }
            }
        }

        void FillJointIndicesAndWeights(MS3DVertex vertex, int[] jointIndices, int[] jointWeights)
        {
            jointIndices[0] = vertex.boneId;
            jointIndices[1] = vertex.boneIds[0];
            jointIndices[2] = vertex.boneIds[1];
            jointIndices[3] = vertex.boneIds[2];

            jointWeights[0] = 100;
            jointWeights[1] = 0;
            jointWeights[2] = 0;
            jointWeights[3] = 0;

            if (vertex.weights[0] != 0 || vertex.weights[1] != 0 || vertex.weights[2] != 0)
            {
                jointWeights[0] = vertex.weights[0];
                jointWeights[1] = vertex.weights[1];
                jointWeights[2] = vertex.weights[2];
                jointWeights[3] = 100 - (vertex.weights[0] + vertex.weights[1] + vertex.weights[2]);
            }
        }

    }
}
