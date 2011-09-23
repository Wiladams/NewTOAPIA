using System;
using System.Collections.Generic;

namespace NewTOAPIA.Modeling
{
    public class EffectsManager
    {
        List<Effect> fEffects;

        public EffectsManager()
        {
            fEffects = new List<Effect>();
        }

        public int Count
        {
            get {return fEffects.Count;}
        }

        public void Attach(Effect aEffect)
        {
            fEffects.Add(aEffect);
        }

        public void Detach(Effect aEffect)
        {
            fEffects.Remove(aEffect);
        }

        public void DetachAll()
        {
            fEffects.Clear();
        }

        public Effect this[int index]
        {
            get { return fEffects[index]; }
        }
    }
}
