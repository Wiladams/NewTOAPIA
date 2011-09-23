using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Modeling
{
    public class AlphaState : GlobalState
    {
        #region Alpha State Enumerations
        public enum AlphaFunction
        {
            Never = 0x0200,
            Less = 0x0201,
            Equal = 0x0202,
            Lequal = 0x0203,
            Greater = 0x0204,
            Notequal = 0x0205,
            Gequal = 0x0206,
            Always = 0x0207,
        }


        public enum SrcBlendFactor
        {
            Zero,               // SBF_ZERO
            One,                // SBF_ONE
            SrcColor,
            SrcAlpha,           // SBF_SRC_ALPHA
            OneMinusSrcAlpha,   // SBF_ONE_MINUS_SRC_ALPHA
            DstAlpha,           // SBF_DST_ALPHA,
            OneMinusDstAlpha,   // SBF_ONE_MINUS_DST_ALPHA
            DstColor,           // SBF_DST_COLOR,
            OneMinusDstColor,   // SBF_ONE_MINUS_DST_COLOR
            SrcAlphaSaturate,   // SBF_SRC_ALPHA_SATURATE
        }
        //SBF_CONSTANT_COLOR,
        //SBF_ONE_MINUS_CONSTANT_COLOR,
        //SBF_CONSTANT_ALPHA,
        //SBF_ONE_MINUS_CONSTANT_ALPHA,


        public enum DstBlendFactor
        {
            Zero,               // DBF_ZERO
            One,                // DBF_ONE
            SrcColor,           // DBF_SRC_COLOR
            OneMinusSrcColor,   // DBF_ONE_MINUS_SRC_COLOR
            SrcAlpha,           // DBF_SRC_ALPHA
            OneMinusSrcAlpha,   // DBF_ONE_MINUS_SRC_ALPHA
            DstAlpha,           // DBF_DST_ALPHA
            OneMinusDstAlpha,   // DBF_ONE_MINUS_DST_ALPHA
        }
        //DBF_CONSTANT_COLOR,
        //DBF_ONE_MINUS_CONSTANT_COLOR,
        //DBF_CONSTANT_ALPHA,
        //DBF_ONE_MINUS_CONSTANT_ALPHA,
        #endregion

        SrcBlendFactor fSrcBlendFactor;
        DstBlendFactor fDstBlendFactor;
        AlphaFunction fAlphaFunction;
        bool fBlendEnabled;
        bool fTestEnabled;
        float fReference;   // clamped between [0,1]


        #region Constructor
        public AlphaState()
            : base(StateType.Alpha)
        {
            fBlendEnabled = false;
            fTestEnabled = false;
            fAlphaFunction = AlphaFunction.Always;
            fSrcBlendFactor = SrcBlendFactor.SrcAlpha;
            fDstBlendFactor = DstBlendFactor.OneMinusSrcAlpha;
            fReference = 0.0f;
        }
        #endregion

        #region Properties
        public AlphaFunction Function
        {
            get { return fAlphaFunction; }
            set { fAlphaFunction = value; }
        }

        public bool BlendEnabled
        {
            get { return fBlendEnabled; }
            set { fBlendEnabled = value; }
        }

        public bool TestEnabled
        {
            get { return fTestEnabled; }
            set { fTestEnabled = value; }
        }

        public SrcBlendFactor SourceBlendMode
        {
            get { return fSrcBlendFactor; }
            set { fSrcBlendFactor = value; }
        }

        public DstBlendFactor DestinationBlendMode
        {
            get { return fDstBlendFactor; }
            set { fDstBlendFactor = value; }
        }

        public float Reference
        {
            get { return fReference; }
            set { fReference = value; }
        }
        #endregion
    }
}
