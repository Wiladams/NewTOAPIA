
namespace NewTOAPIA.Drawing
{
    using System;
    using System.Collections.Generic;

    public class QuickSort_cell_aa
    {
        public QuickSort_cell_aa()
        {
        }

        public void Sort(cell_aa[] dataToSort)
        {
            Sort(dataToSort, 0, (int)(dataToSort.Length - 1));
        }

        public void Sort(cell_aa[] dataToSort, int beg, int end)
        {
            if (end == beg)
            {
                return;
            }
            else
            {
                int pivot = getPivotPoint(dataToSort, beg, end);
                if (pivot > beg)
                {
                    Sort(dataToSort, beg, pivot - 1);
                }

                if (pivot < end)
                {
                    Sort(dataToSort, pivot + 1, end);
                }
            }
        }

        private int getPivotPoint(cell_aa[] dataToSort, int begPoint, int endPoint)
        {
            int pivot = begPoint;
            int m = begPoint + 1;
            int n = endPoint;
            while ((m < endPoint)
                && dataToSort[pivot].x >= dataToSort[m].x)
            {
                m++;
            }

            while ((n > begPoint) && (dataToSort[pivot].x <= dataToSort[n].x))
            {
                n--;
            }
            while (m < n)
            {
                cell_aa temp = dataToSort[m];
                dataToSort[m] = dataToSort[n];
                dataToSort[n] = temp;

                while ((m < endPoint) && (dataToSort[pivot].x >= dataToSort[m].x))
                {
                    m++;
                }

                while ((n > begPoint) && (dataToSort[pivot].x <= dataToSort[n].x))
                {
                    n--;
                }

            }
            if (pivot != n)
            {
                cell_aa temp2 = dataToSort[n];
                dataToSort[n] = dataToSort[pivot];
                dataToSort[pivot] = temp2;

            }
            return n;
        }
    }

    public class QuickSort_range_adaptor_uint
    {
        public QuickSort_range_adaptor_uint()
        {
        }

        public void Sort(VectorPOD_RangeAdaptor dataToSort)
        {
            Sort(dataToSort, 0, (int)(dataToSort.size() - 1));
        }

        public void Sort(VectorPOD_RangeAdaptor dataToSort, int beg, int end)
        {
            if (end == beg)
            {
                return;
            }
            else
            {
                int pivot = getPivotPoint(dataToSort, beg, end);
                if (pivot > beg)
                {
                    Sort(dataToSort, beg, pivot - 1);
                }

                if (pivot < end)
                {
                    Sort(dataToSort, pivot + 1, end);
                }
            }
        }

        private int getPivotPoint(VectorPOD_RangeAdaptor dataToSort, int begPoint, int endPoint)
        {
            int pivot = begPoint;
            int m = begPoint + 1;
            int n = endPoint;
            while ((m < endPoint)
                && dataToSort[pivot] >= dataToSort[m])
            {
                m++;
            }

            while ((n > begPoint) && (dataToSort[pivot] <= dataToSort[n]))
            {
                n--;
            }
            while (m < n)
            {
                int temp = dataToSort[m];
                dataToSort[m] = dataToSort[n];
                dataToSort[n] = temp;

                while ((m < endPoint) && (dataToSort[pivot] >= dataToSort[m]))
                {
                    m++;
                }

                while ((n > begPoint) && (dataToSort[pivot] <= dataToSort[n]))
                {
                    n--;
                }

            }
            if (pivot != n)
            {
                int temp2 = dataToSort[n];
                dataToSort[n] = dataToSort[pivot];
                dataToSort[pivot] = temp2;

            }
            return n;
        }
    }
}
