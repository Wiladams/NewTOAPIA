using System;
using System.Collections.Generic;
using System.Text;

namespace MS3D
{
    class MS3DModelAnimator
    {
        MS3DModel fModel;
        float m_currentTime;

        public MS3DModelAnimator(MS3DModel aModel)
        {
            fModel = aModel;
        }
        
        public float CurrentTime
        {
            get {return m_currentTime;}
        }

        public void SetFrame(float frame)
        {
            if (frame < 0.0f)
            {
                foreach (MS3DJoint joint in fModel.m_joints)
                {
                    //memcpy(joint.matLocal, joint.matLocalSkeleton, sizeof(joint.matLocal));
                    //memcpy(joint.matGlobal, joint.matGlobalSkeleton, sizeof(joint.matGlobal));
                    joint.matLocalSkeleton = joint.matLocal;
                    joint.matGlobalSkeleton = joint.matGlobal;
                }
            }
            else
            {
                foreach (MS3DJoint joint in fModel.m_joints)
                {
                    EvaluateJoint(joint, frame);
                }
            }

            m_currentTime = frame;

        }

        public MS3DJoint FindJoint(string name)
        {
            foreach (MS3DJoint joint in fModel.m_joints)
            {
                if (string.Compare(joint.name, name) == 0)
                    return joint;
            }

            return null;
        }

        public int FindJointIndex(string name)
        {
            for (int i = 0; i < fModel.m_joints.Count; i++ )
            {
                MS3DJoint joint = fModel.m_joints[i];
                if (string.Compare(joint.name, name) == 0)
                    return i;
            }

            return -1;
        }

        public void SetupJoints()
        {
            foreach (MS3DJoint joint in fModel.m_joints)
            {
                joint.parentIndex = FindJointIndex(joint.parentName);
            }

            foreach (MS3DJoint joint in fModel.m_joints)
            {
                //MS3DMath.AngleMatrix(joint.fInitialRotation, ref joint.matLocalSkeleton);
                //joint.matLocalSkeleton[0][3] = joint.fInitialPosition[0];
                //joint.matLocalSkeleton[1][3] = joint.fInitialPosition[1];
                //joint.matLocalSkeleton[2][3] = joint.fInitialPosition[2];

                if (joint.parentIndex == -1)
                {
                    //memcpy(joint.matGlobalSkeleton, joint.matLocalSkeleton, sizeof(joint.matGlobalSkeleton));
                    joint.matLocalSkeleton = joint.matGlobalSkeleton;
                }
                else
                {
                    MS3DJoint parentJoint = fModel.m_joints[joint.parentIndex];
                    //MS3DMath.R_ConcatTransforms(parentJoint.matGlobalSkeleton, joint.matLocalSkeleton, joint.matGlobalSkeleton);
                }

                SetupTangents();
            }
        }


        void SetupTangents()
        {
            //    for (int j = 0; j < m_joints.Count; j++)
            //    {
            //        MS3DJoint joint = m_joints[j];
            //        int numPositionKeys = (int) joint.fPositionKeyframes.size();
            //        joint.tangents.Capacity = numPositionKeys;

            //        // clear all tangents (zero derivatives)
            //        for (int k = 0; k < numPositionKeys; k++)
            //        {
            //            joint.tangents[k].tangentIn[0] = 0.0f;
            //            joint.tangents[k].tangentIn[1] = 0.0f;
            //            joint.tangents[k].tangentIn[2] = 0.0f;
            //            joint.tangents[k].tangentOut[0] = 0.0f;
            //            joint.tangents[k].tangentOut[1] = 0.0f;
            //            joint.tangents[k].tangentOut[2] = 0.0f;
            //        }

            //        // if there are more than 2 keys, we can calculate tangents, otherwise we use zero derivatives
            //        if (numPositionKeys > 2)
            //        {
            //            for (int k = 0; k < numPositionKeys; k++)
            //            {
            //                // make the curve tangents looped
            //                int k0 = k - 1;
            //                if (k0 < 0)
            //                    k0 = numPositionKeys - 1;
            //                int k1 = k;
            //                int k2 = k + 1;
            //                if (k2 >= numPositionKeys)
            //                    k2 = 0;

            //                // calculate the tangent, which is the vector from key[k - 1] to key[k + 1]
            //                float3 tangent;
            //                tangent[0] = (joint.fPositionKeyframes[k2].key[0] - joint.fPositionKeyframes[k0].key[0]);
            //                tangent[1] = (joint.fPositionKeyframes[k2].key[1] - joint.fPositionKeyframes[k0].key[1]);
            //                tangent[2] = (joint.fPositionKeyframes[k2].key[2] - joint.fPositionKeyframes[k0].key[2]);

            //                // weight the incoming and outgoing tangent by their time to avoid changes in speed, if the keys are not within the same interval
            //                float dt1 = joint.fPositionKeyframes[k1].time - joint.fPositionKeyframes[k0].time;
            //                float dt2 = joint.fPositionKeyframes[k2].time - joint.fPositionKeyframes[k1].time;
            //                float dt = dt1 + dt2;
            //                joint.tangents[k1].tangentIn[0] = tangent[0] * dt1 / dt;
            //                joint.tangents[k1].tangentIn[1] = tangent[1] * dt1 / dt;
            //                joint.tangents[k1].tangentIn[2] = tangent[2] * dt1 / dt;

            //                joint.tangents[k1].tangentOut[0] = tangent[0] * dt2 / dt;
            //                joint.tangents[k1].tangentOut[1] = tangent[1] * dt2 / dt;
            //                joint.tangents[k1].tangentOut[2] = tangent[2] * dt2 / dt;
            //            }
            //        }
            //    }
        }

        void EvaluateJoint(MS3DJoint joint, float frame)
        {
            //    MS3DJoint *joint = &m_joints[index];

            //    //
            //    // calculate joint animation matrix, this matrix will animate matLocalSkeleton
            //    //
            //    vec3_t fInitialPosition = { 0.0f, 0.0f, 0.0f };
            //    int numPositionKeys = (int) joint.fPositionKeyframes.size();
            //    if (numPositionKeys > 0)
            //    {
            //        int i1 = -1;
            //        int i2 = -1;

            //        // find the two keys, where "frame" is in between for the position channel
            //        for (int i = 0; i < (numPositionKeys - 1); i++)
            //        {
            //            if (frame >= joint.fPositionKeyframes[i].time && frame < joint.fPositionKeyframes[i + 1].time)
            //            {
            //                i1 = i;
            //                i2 = i + 1;
            //                break;
            //            }
            //        }

            //        // if there are no such keys
            //        if (i1 == -1 || i2 == -1)
            //        {
            //            // either take the first
            //            if (frame < joint.fPositionKeyframes[0].time)
            //            {
            //                fInitialPosition[0] = joint.fPositionKeyframes[0].key[0];
            //                fInitialPosition[1] = joint.fPositionKeyframes[0].key[1];
            //                fInitialPosition[2] = joint.fPositionKeyframes[0].key[2];
            //            }

            //            // or the last key
            //            else if (frame >= joint.fPositionKeyframes[numPositionKeys - 1].time)
            //            {
            //                fInitialPosition[0] = joint.fPositionKeyframes[numPositionKeys - 1].key[0];
            //                fInitialPosition[1] = joint.fPositionKeyframes[numPositionKeys - 1].key[1];
            //                fInitialPosition[2] = joint.fPositionKeyframes[numPositionKeys - 1].key[2];
            //            }
            //        }

            //        // there are such keys, so interpolate using hermite interpolation
            //        else
            //        {
            //            M3DKeyframe *p0 = &joint.fPositionKeyframes[i1];
            //            M3DKeyframe *p1 = &joint.fPositionKeyframes[i2];
            //            M3DTangent *m0 = &joint.tangents[i1];
            //            M3DTangent *m1 = &joint.tangents[i2];

            //            // normalize the time between the keys into [0..1]
            //            float t = (frame - joint.fPositionKeyframes[i1].time) / (joint.fPositionKeyframes[i2].time - joint.fPositionKeyframes[i1].time);
            //            float t2 = t * t;
            //            float t3 = t2 * t;

            //            // calculate hermite basis
            //            float h1 =  2.0f * t3 - 3.0f * t2 + 1.0f;
            //            float h2 = -2.0f * t3 + 3.0f * t2;
            //            float h3 =         t3 - 2.0f * t2 + t;
            //            float h4 =         t3 -        t2;

            //            // do hermite interpolation
            //            fInitialPosition[0] = h1 * p0.key[0] + h3 * m0.tangentOut[0] + h2 * p1.key[0] + h4 * m1.tangentIn[0];
            //            fInitialPosition[1] = h1 * p0.key[1] + h3 * m0.tangentOut[1] + h2 * p1.key[1] + h4 * m1.tangentIn[1];
            //            fInitialPosition[2] = h1 * p0.key[2] + h3 * m0.tangentOut[2] + h2 * p1.key[2] + h4 * m1.tangentIn[2];
            //        }
            //    }

            //    vec4_t quat = { 0.0f, 0.0f, 0.0f, 1.0f };
            //    int numRotationKeys = (int) joint.rotationKeys.size();
            //    if (numRotationKeys > 0)
            //    {
            //        int i1 = -1;
            //        int i2 = -1;

            //        // find the two keys, where "frame" is in between for the rotation channel
            //        for (int i = 0; i < (numRotationKeys - 1); i++)
            //        {
            //            if (frame >= joint.rotationKeys[i].time && frame < joint.rotationKeys[i + 1].time)
            //            {
            //                i1 = i;
            //                i2 = i + 1;
            //                break;
            //            }
            //        }

            //        // if there are no such keys
            //        if (i1 == -1 || i2 == -1)
            //        {
            //            // either take the first key
            //            if (frame < joint.rotationKeys[0].time)
            //            {
            //                AngleQuaternion(joint.rotationKeys[0].key, quat);
            //            }

            //            // or the last key
            //            else if (frame >= joint.rotationKeys[numRotationKeys - 1].time)
            //            {
            //                AngleQuaternion(joint.rotationKeys[numRotationKeys - 1].key, quat);
            //            }
            //        }

            //        // there are such keys, so do the quaternion slerp interpolation
            //        else
            //        {
            //            float t = (frame - joint.rotationKeys[i1].time) / (joint.rotationKeys[i2].time - joint.rotationKeys[i1].time);
            //            vec4_t q1;
            //            AngleQuaternion(joint.rotationKeys[i1].key, q1);
            //            vec4_t q2;
            //            AngleQuaternion(joint.rotationKeys[i2].key, q2);
            //            QuaternionSlerp(q1, q2, t, quat);
            //        }
            //    }

            //    // make a matrix from fInitialPosition/quat
            //    float matAnimate[3][4];
            //    QuaternionMatrix(quat, matAnimate);
            //    matAnimate[0][3] = fInitialPosition[0];
            //    matAnimate[1][3] = fInitialPosition[1];
            //    matAnimate[2][3] = fInitialPosition[2];

            //    // animate the local joint matrix using: matLocal = matLocalSkeleton * matAnimate
            //    R_ConcatTransforms(joint.matLocalSkeleton, matAnimate, joint.matLocal);

            //    // build up the hierarchy if joints
            //    // matGlobal = matGlobal(parent) * matLocal
            //    if (joint.parentIndex == -1)
            //    {
            //        memcpy(joint.matGlobal, joint.matLocal, sizeof(joint.matGlobal));
            //    }
            //    else
            //    {
            //        MS3DJoint *parentJoint = &m_joints[joint.parentIndex];
            //        R_ConcatTransforms(parentJoint.matGlobal, joint.matLocal, joint.matGlobal);
            //    }
            //}

            //            void TransformVertex(MS3DVertex vertex, float3 vout)
            //    {
            //        int [] jointIndices = new int[4];
            //        int [] jointWeights = new int[4];

            //        FillJointIndicesAndWeights(vertex, jointIndices, jointWeights);

            //        if (jointIndices[0] < 0 || jointIndices[0] >= (int) m_joints.size() || m_currentTime < 0.0f)
            //        {
            //            vout[0] = vertex.vertex[0];
            //            vout[1] = vertex.vertex[1];
            //            vout[2] = vertex.vertex[2];
            //        }
            //    else
            //    {
            //        // count valid weights
            //        int numWeights = 0;
            //        for (int i = 0; i < 4; i++)
            //        {
            //            if (jointWeights[i] > 0 && jointIndices[i] >= 0 && jointIndices[i] < (int) m_joints.size())
            //                ++numWeights;
            //            else
            //                break;
            //        }

            //        // init
            //        vout[0] = 0.0f;
            //        vout[1] = 0.0f;
            //        vout[2] = 0.0f;

            //        float weights[4] = { (float) jointWeights[0] / 100.0f, (float) jointWeights[1] / 100.0f, (float) jointWeights[2] / 100.0f, (float) jointWeights[3] / 100.0f };
            //        if (numWeights == 0)
            //        {
            //            numWeights = 1;
            //            weights[0] = 1.0f;
            //        }
            //        // add weighted vertices
            //        for (int i = 0; i < numWeights; i++)
            //        {
            //            const MS3DJoint *joint = &m_joints[jointIndices[i]];
            //            vec3_t tmp, vert;
            //            VectorITransform(vertex.vertex, joint.matGlobalSkeleton, tmp);
            //            VectorTransform(tmp, joint.matGlobal, vert);

            //            vout[0] += vert[0] * weights[i];
            //            vout[1] += vert[1] * weights[i];
            //            vout[2] += vert[2] * weights[i];
            //        }
            //    }
        }

    }
}