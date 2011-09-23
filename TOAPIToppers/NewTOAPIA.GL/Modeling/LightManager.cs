using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Modeling
{
    public class LightManager
    {
        List<Light> fLightList;

        public LightManager()
        {
            fLightList = new List<Light>();
        }

        public void Attach(Light aLight)
        {
            fLightList.Add(aLight);
        }

        public void Detach(Light aLight)
        {
            fLightList.Remove(aLight);
        }

        public void DetachAll()
        {
            fLightList.Clear();
        }

        public int Count
        {
            get { return fLightList.Count; }
        }

        public Light this[int index]
        {
            get { return fLightList[index]; }
        }

    }
}
