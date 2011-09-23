

namespace NewTOAPIA.Modeling
{
    using System;
    using System.Collections.Generic;

    public abstract class Spacial
    {
        Spacial fParent;
        GlobalStateManager fGlobalState;
        LightManager fLightManager;
        List<Effect> fEffects;

        protected Spacial()
        {
            fParent = null;
        }

        public Spacial Parent
        {
            get { return fParent; }
            set { fParent = value; }
        }

        public List<Effect> Effects
        {
            get
            {
                if (null == fEffects)
                {
                    fEffects = new List<Effect>();
                }
                return fEffects;
            }
        }

        public GlobalStateManager GlobalState
        {
            get {
                if (null == fGlobalState)
                {
                    fGlobalState = new GlobalStateManager();
                }
                return fGlobalState;
            }
        }

        public LightManager Lights
        {
            get {
                if (null == fLightManager)
                {
                    fLightManager = new LightManager();
                }
                return fLightManager;
            }
        }
    }
}
