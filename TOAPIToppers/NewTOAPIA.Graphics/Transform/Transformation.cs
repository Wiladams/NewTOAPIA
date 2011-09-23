
namespace NewTOAPIA.Graphics
{
    using System;

    /// <summary>
    /// The Transformation class is meant to represent a general transformation in 3D.
    /// It will typically be attached to UI elements, both 3D and 2D so they can 
    /// carry their various world, and local transforms with them.
    /// </summary>
    public class Transformation
    {
        #region Static Fields
        public static Transformation IDentity = new Transformation();
        #endregion

        #region Private Fields
        float3x3 fMatrix;
        float3 fTranslate;
        float3 fScale;

        bool fIsIdentity;
        bool fIsRSMatrix;
        bool fIsUniformScale;
        #endregion

        #region Constructors
        public Transformation()
        {
            fMatrix = float3x3.Identity;
            fTranslate = float3.Zero;
            fScale = new float3(1, 1, 1);

            fIsIdentity = true;
            fIsRSMatrix = true;
            fIsUniformScale = true;
        }

        public Transformation(Transformation aTrans)
        {
            fMatrix = aTrans.fMatrix;
            fTranslate = aTrans.fTranslate;
            fScale = aTrans.fScale;

            fIsIdentity = aTrans.fIsIdentity;
            fIsRSMatrix = aTrans.fIsRSMatrix;
            fIsUniformScale = aTrans.fIsUniformScale;
        }

        #endregion

        #region Properties
        public bool IsIdentity
        {
            get { return fIsIdentity; }
        }

        public bool IsRSMatrix
        {
            get { return fIsRSMatrix; }
        }

        public bool IsUniformScale
        {
            get { return fIsUniformScale; }
        }

        #region Whole Matrix
        public float3x3 Matrix
        {
            get { return fMatrix; }
            set
            {
                fIsIdentity = false;
                fIsRSMatrix = false;
                fIsUniformScale = false;

                fMatrix = value;
            }
        }

        #endregion

        #region Rotation
        public void SetRotate(float3x3 rotate)
        {
            fIsIdentity = false;
            fIsRSMatrix = true;

            fMatrix = rotate;
        }

        public float3x3 GetRotate()
        {
            return fMatrix;
        }
        #endregion

        #region Translation 
        public float3 Translation
        {
            get { return fTranslate; }
            set
            {
                fIsIdentity = false;
                fTranslate = value;
            }
        }
        
        public void Translate(float3 offset)
        {
            float3 newtrans = fTranslate + offset;
            Translation = newtrans;
        }

        public void Translate(float2 offset)
        {
            float3 newtrans = fTranslate + new float3(offset, 0);
            Translation = newtrans;
        }

        public void Translate(float dx, float dy)
        {
            float3 newtrans = fTranslate + new float3(dx, dy, 0);
            Translation = newtrans;
        }
        #endregion

        #region Scale
        public float3 Scale
        {
            get { return fScale; }
            set
            {
                fIsIdentity = false;
                fIsUniformScale = false;

                fScale = value;
            }
        }
        #endregion

        #region Scale Uniform
        public float UniformScale
        {
            get { return fScale[0]; }
            set
            {
                fIsIdentity = false;
                fIsUniformScale = true;

                fScale = new float3(value, value, value);
            }
        }
        #endregion
        #endregion

        #region Convenience Functions
        float GetNorm()
        {
            // This needs to be filled in
            return 0.0f;
        }

        public void MakeIdentity()
        {
            fMatrix = float3x3.Identity;
            fTranslate = float3.Zero;
            fScale = new float3(1.0f,1.0f,1.0f);

            fIsIdentity = true;
            fIsRSMatrix = true;
            fIsUniformScale = true;
        }

        public void MakeUnitScale()
        {
            fScale = new float3(1.0f, 1.0f, 1.0f);

            fIsUniformScale = true;
        }

        public void Product(Transformation A, Transformation B, out Transformation C)
        {
            C = null;
        }

        public void Inverse(out Transformation inverse)
        {
            inverse = null;
        }

        public float3 InvertVector(float3 vector)
        {
            return float3.Zero;
        }
        #endregion

        #region Applying Transforms
        /// <summary>
        /// Applying the transform takes the input, and multiplies it by 
        /// the transformation matrix.  This operation will return a new
        /// output that has the transform applied.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public float3 ApplyForward(float3 input)
        {
            // If it's the identity transform, just return the input
            // without wasting cycles on multiplying by '1'.
            if (fIsIdentity)
                return input;

            // If this is a Rotation Scale matrix, then apply the scaling
            // first, then the rotation, and finally the translation.
            if (fIsRSMatrix)
            {
                // Y = R*S*X + T
                float3 result = new float3(fScale.x*input.x,fScale.y*input.y,fScale.z*input.z);
                result = fMatrix*result + fTranslate;
                return result;
            }

            float3 retValue = (fMatrix * input) + fTranslate;
            return retValue;
        }

        public void ApplyForward(int quantity, float3[] input, out float3[] output)
        {
            output = new float3[input.Length];
            for (int i=0; i< input.Length; i++)
            {
                output[i] = ApplyForward(input[i]);
            }

        }

        public float3 ApplyInverse(float3 input)
        {
            if (fIsIdentity)
                return input;

            float3 kOutput = input - fTranslate;
            if (fIsRSMatrix)
            {
                // X = S^{-1}*R^t*(Y - T)
                kOutput = kOutput * fMatrix;
                if (fIsUniformScale)
                {
                    kOutput /= UniformScale;
                }
                else
                {
                    // The direct inverse scaling is
                    //   kOutput.X() /= m_kScale.X();
                    //   kOutput.Y() /= m_kScale.Y();
                    //   kOutput.Z() /= m_kScale.Z();
                    // When division is much more expensive than multiplication,
                    // three divisions are replaced by one division and ten
                    // multiplications.
                    float fSXY = fScale.x * fScale.y;
                    float fSXZ = fScale.x * fScale.z;
                    float fSYZ = fScale.y * fScale.z;
                    float fInvDet = 1.0f / (fSXY * fScale.z);
                    kOutput.x *= fInvDet * fSYZ;
                    kOutput.y *= fInvDet * fSXZ;
                    kOutput.z *= fInvDet * fSXY;
                }
            }
            else
            {
                // X = M^{-1}*(Y - T)
                kOutput = fMatrix.Inverse * kOutput;
            }

            return kOutput;
        }

        public void ApplyInverse(int quantity, float3[] input, out float3[] output)
        {
            output = null;
        }
#endregion

    }
}
