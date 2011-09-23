using System.Collections.Generic;

namespace NewTOAPIA.Modeling
{
    public class VisibleSet
    {
        const int VS_DEFAULT_MAX_QUANTITY = 32;
        const int VS_DEFAULT_GROWBY = 32;

        int fMaxQuantity;
        int fGrowBy;
        List<VisibleObject> fVisibleObjects;

        public VisibleSet()
            : this(0, 0)
        {
        }

        public VisibleSet(int maxQuantity, int growBy)
        {
            fVisibleObjects = new List<VisibleObject>();

            fMaxQuantity = maxQuantity;
            fGrowBy = growBy;
        }

        public VisibleObject this[int i]
        {
            get { return fVisibleObjects[i]; }
        }

        public VisibleObject Visible
        {
            get { return fVisibleObjects[0]; }
        }

        public void Insert(Spacial anObject, Effect anEffect)
        {
            VisibleObject aVisible = new VisibleObject(anObject, anEffect);

            fVisibleObjects.Add(aVisible);
        }


        public void Clear()
        {
            fVisibleObjects.Clear();
        }

        void Resize(int iMaxQuantity, int iGrowBy)
        {
        }

    }
}
