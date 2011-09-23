using System;
using System.Collections.Generic;

namespace NewTOAPIA.Modeling
{
    public class Node : Spacial
    {
        List<Spacial> fChildList;

        public Node()
        {
            fChildList = new List<Spacial>();
        }

        public List<Spacial> Children
        {
            get { return fChildList; }
        }

        public int GetQuantity()
        {
            return fChildList.Count;
        }

        public int AddChild(Spacial child)
        {
            fChildList.Add(child);

            return fChildList.Count - 1;
        }

        public int RemoveChild(Spacial child)
        {
            int index = fChildList.LastIndexOf(child);
            fChildList.RemoveAt(index);

            return index;
        }

        public Spacial RemoveChildAt(int position)
        {
            Spacial aChild = fChildList[position];
            fChildList.RemoveAt(position);

            return aChild;
        }

        public Spacial SetChild(int i, Spacial child)
        {
            Spacial oldChild = fChildList[i];
            fChildList[i] = child;

            return oldChild;
        }

        public Spacial GetChild(int i)
        {
            return fChildList[i];
        }
    }
}
