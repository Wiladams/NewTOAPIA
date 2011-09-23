using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Modeling
{
    public class IndexBuffer
    {
        int fCount;
        int [] fIndices;

        public IndexBuffer(int count)
        {
            fCount = count;
            fIndices = new int[count];
        }

        public int Count
        {
            get { return fCount; }
        }

        public void SetDefaultIndices()
        {
            for (int i = 0; i < fCount; i++)
                fIndices[i] = i;
        }

        public void IncreaseCount()
        {
            fCount += 1;
        }

        public void DecreaseCount()
        {
            fCount -= 1;
        }

        public int this[int i]
        {
            get
            {
                return fIndices[i];
            }

            set
            {
                fIndices[i] = value;
            }
        }

    }
}
