using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Modeling
{
    public class VisibleObject
    {
        Spacial fObject;
        Effect fEffect;

        public VisibleObject(Spacial spacialObject, Effect anEffect)
        {
            fObject = spacialObject;
            fEffect = anEffect;
        }

        #region Properties

        public Spacial Spacial
        {
            get { return fObject; }
        }

        public Effect Effect
        {
            get { return fEffect; }
        }

        public bool IsVisible
        {
            get { return true; }
        }
        #endregion
    }
}
