
namespace NewTOAPIA.Modeling
{
    using System;

    using NewTOAPIA;
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public enum LightType
    {
        Ambient,
        Directional,
        Point,
        Spot
    }

    
    public class Light
    {
        LightType fLightType;

        ColorRGBA fAmbient;
        ColorRGBA fDiffuse;
        ColorRGBA fSpecular;

        // Attenuation is typically specified as a modulator
        //   m = 1/(C + L*d + Q*d*d)
        // where C is the constant coefficient, L is the linear coefficient,
        // Q is the quadratic coefficient, and d is the distance from the light
        // position to the vertex position.  
        // To allow for a linear adjustment of intensity, the following formula
        // is used instead
        //   m = I/(C + L*d + Q*d*d)
        // where I is an "intensity" factor.
        float fConstant;     // default: 1
        float fLinear;       // default: 0
        float fQuadratic;    // default: 0
        float fIntensity;    // default: 1


        // Parameters for spot lights.  
        // The cone angle must be in radians and
        // should satisfy 0 < Angle <= pi.
        float fAngle;        // default: pi
        float fCosAngle;     // default: -1
        float fSinAngle;     // default:  0
        float fExponent;     // default:  1

        // Although the standard directional and spot lights need only a direction
        // vector, to allow for new types of derived-class lights that would use
        // a full coordinate frame, Light provides storage for such a frame.  The
        // light frame is always in world coordinates.
        //   default position  P = (0,0,0)
        //   default direction D = (0,0,-1)
        //   default up        U = (0,1,0)
        //   default right     R = (1,0,0)
        // The set {D,U,R} must be a right-handed orthonormal set.  That is, each
        // vector is unit length, the vectors are mutually perpendicular, and
        // R = Cross(D,U).
        Point3D fPosition;
        Vector3D DVector; 
        //Vector3D UVector, RVector;

        #region Constructor
        public Light(LightType aType)
        {
            fLightType = aType;

            fConstant = 1.0f;
            fLinear = 0.0f;
            fQuadratic = 0.0f;
            fIntensity = 1.0f;
        
            // Setting up spotlight stuff
            fAngle = (float)Math.PI;
            fCosAngle = -1;
            fSinAngle = 0.0f;
            fExponent = 1.0f;

        }
        #endregion

        #region Properties
        public Point3D Position
        {
            get { return fPosition; }
            set { fPosition = value; }
        }

        public ColorRGBA Ambient
        {
            get { return fAmbient; }
            set { fAmbient = value; }
        }

        public ColorRGBA Diffuse
        {
            get { return fDiffuse; }
            set { fDiffuse = value; }
        }

        public ColorRGBA Specular
        {
            get { return fSpecular; }
            set { fSpecular = value; }
        }

        public LightType TypeOfLight
        {
            get { return fLightType; }
        }
        #endregion

        // A helper function that lets you set Angle and have CosAngle and
        // SinAngle computed for you.
        void SetAngle(float angle)
        {
            //assert(0.0f < angle && angle <= Math.PI);
            fAngle = angle;
            fCosAngle = (float)Math.Cos(fAngle);
            fSinAngle = (float)Math.Sin(fAngle);
       }

        // A helper function that lets you set the direction vector and computes
        // the up and right vectors automatically.
        public void SetDirection (Vector3D direction, bool isUnitLength)
        {
            DVector = direction;
            //Vector3f.GenerateOrthonormalBasis(UVector,RVector,DVector,bUnitLength);
        }

        // This is for debug mode to allow you to check if the coordinate frame
        // vectors form a right-handed orthonormal set.
        public bool IsValidFrame ()
        {
            //float fTest = DVector.Dot(UVector);
            //if (Mathf.FAbs(fTest) > Math.ZERO_TOLERANCE)
            //{
            //    return false;
            //}

            //fTest = DVector.Dot(RVector);
            //if (Mathf::FAbs(fTest) > Mathf::ZERO_TOLERANCE)
            //{
            //    return false;
            //}

            //fTest = UVector.Dot(RVector);
            //if (Mathf::FAbs(fTest) > Mathf::ZERO_TOLERANCE)
            //{
            //    return false;
            //}

            //fTest = DVector.Dot(UVector.Cross(RVector));
            //return Mathf::FAbs(1.0f - fTest) <= Mathf::ZERO_TOLERANCE;

            return true;
        }

    }
}
