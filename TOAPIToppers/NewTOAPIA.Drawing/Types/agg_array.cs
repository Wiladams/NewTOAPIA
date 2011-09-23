
namespace NewTOAPIA.Drawing
{
    public interface IDataContainer<T>
    {
        T[] Array { get; }
        void remove_last();
    }

    public class ArrayPOD<T> : IDataContainer<T>
    {
        public ArrayPOD()
            : this(64)
        {
        }

        public ArrayPOD(int size)
        {
            m_array = new T[size];
            m_size = size;
        }

        public void remove_last()
        {
            throw new System.NotImplementedException();
        }

        public ArrayPOD(ArrayPOD<T> v)
        {
            m_array = (T[])v.m_array.Clone();
            m_size = v.m_size;
        }

        public void resize(int size)
        {
            if (size != m_size)
            {
                m_array = new T[size];
            }
        }

        public int size() { return m_size; }

        public T this[int Index]
        {
            get
            {
                return m_array[Index];
            }

            set
            {
                m_array[Index] = value;
            }
        }

        public T[] Array
        {
            get
            {
                return m_array;
            }
        }
        private T[] m_array;
        private int m_size;
    };


    //--------------------------------------------------------------pod_vector
    // A simple class template to store Plain Old Data, a vector
    // of a fixed size. The data is contiguous in memory
    //------------------------------------------------------------------------
    public class VectorPOD<T> : IDataContainer<T>
    {
        protected int m_size;
        private int m_capacity;
        private T[] m_array;

        public VectorPOD()
        {
        }

        public VectorPOD(int cap)
            : this(cap, 0)
        {
        }

        public VectorPOD(int cap, int extra_tail)
        {
            allocate(cap, extra_tail);
        }

        public virtual void remove_last()
        {
            if (m_size != 0)
            {
                m_size--;
            }
        }

        // Copying
        public VectorPOD(VectorPOD<T> v)
        {
            m_size = v.m_size;
            m_capacity = v.m_capacity;
            m_array = (T[])v.m_array.Clone();
        }

        public void CopyFrom(VectorPOD<T> v)
        {
            allocate(v.m_size);
            if (v.m_size != 0)
            {
                v.m_array.CopyTo(m_array, 0);
            }
        }

        // Set new capacity. All data is lost, size is set to zero.
        public void capacity(int cap)
        {
            capacity(cap, 0);
        }

        public void capacity(int cap, int extra_tail)
        {
            m_size = 0;
            if (cap > m_capacity)
            {
                m_array = null;
                m_capacity = cap + extra_tail;
                if (m_capacity != 0)
                {
                    m_array = new T[m_capacity];
                }
            }
        }

        public int capacity() { return m_capacity; }

        // Allocate n elements. All data is lost, 
        // but elements can be accessed in range 0...size-1. 
        public void allocate(int size)
        {
            allocate(size, 0);
        }

        public void allocate(int size, int extra_tail)
        {
            capacity(size, extra_tail);
            m_size = size;
        }

        // Resize keeping the content.
        public void resize(int new_size)
        {
            if (new_size > m_size)
            {
                if (new_size > m_capacity)
                {
                    T[] newArray = new T[new_size];
                    if (m_array != null)
                    {
                        for (int i = 0; i < m_array.Length; i++)
                        {
                            newArray[i] = m_array[i];
                        }
                    }
                    m_array = newArray;
                    m_capacity = new_size;
                }
            }
        }

#pragma warning disable 649
        static T zeroed_object;
#pragma warning restore 649

        public void zero()
        {
            int NumItems = m_array.Length;
            for (int i = 0; i < NumItems; i++)
            {
                m_array[i] = zeroed_object;
            }
        }

        public virtual void add(T v)
        {
            if (m_array == null || m_array.Length < (m_size + 1))
            {
                resize(m_size + (m_size / 2) + 16);
            }
            m_array[m_size++] = v;
        }

        public void push_back(T v) { m_array[m_size++] = v; }
        public void insert_at(int pos, T val)
        {
            if (pos >= m_size)
            {
                m_array[m_size] = val;
            }
            else
            {
                for (int i = 0; i < m_size - pos; i++)
                {
                    m_array[i + pos + 1] = m_array[i + pos];
                }
                m_array[pos] = val;
            }
            ++m_size;
        }

        public void inc_size(int size) { m_size += size; }
        public int size() { return m_size; }

        public T this[int i]
        {
            get
            {
                return m_array[i];
            }
        }

        public T[] Array
        {
            get
            {
                return m_array;
            }
        }

        public T at(int i) { return m_array[i]; }
        public T value_at(int i) { return m_array[i]; }

        public T[] data() { return m_array; }

        public void Clear() { m_size = 0; }
        public void clear() { m_size = 0; }
        public void cut_at(int num) { if (num < m_size) m_size = num; }
    }

    //----------------------------------------------------------range_adaptor
    public class VectorPOD_RangeAdaptor
    {
        VectorPOD<int> m_array;
        int m_start;
        int m_size;

        public VectorPOD_RangeAdaptor(VectorPOD<int> array, int start, int size)
        {
            m_array = (array);
            m_start = (start);
            m_size = (size);
        }

        public int size() { return m_size; }
        public int this[int i]
        {
            get
            {
                return m_array.Array[m_start + i];
            }

            set
            {
                m_array.Array[m_start + i] = value;
            }
        }
        public int at(int i) { return m_array.Array[m_start + i]; }
        public int value_at(int i) { return m_array.Array[m_start + i]; }
    };
}
