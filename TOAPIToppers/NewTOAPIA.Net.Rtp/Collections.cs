using System;
using System.Collections;



public class SdesPrivateExtensionHashtable : IEnumerable
{
    private Hashtable h;
    private ByteComparer comparer = new ByteComparer();

    public SdesPrivateExtensionHashtable(int length)
    {
        h = new Hashtable(length, comparer);
    }
    public SdesPrivateExtensionHashtable()
        : base()
    {
        h = new Hashtable(comparer);
    }

    public ICollection Keys
    {
        get { return (h.Keys); }
    }

    public ICollection Values
    {
        get { return (h.Values); }
    }

    public object Clone()
    {
        SdesPrivateExtensionHashtable clone = new SdesPrivateExtensionHashtable();

        foreach (DictionaryEntry de in h)
        {
            clone.Add((byte[])de.Key, (byte[])de.Value);
        }

        return clone;
    }

    public byte[] this[byte[] prefix]
    {
        get { return (byte[])h[prefix]; }
        set { h[prefix] = value; }
    }

    public void Add(byte[] prefix, byte[] data)
    {
        h.Add(prefix, data);
    }

    public bool Contains(byte[] prefix)
    {
        return h.Contains(prefix);
    }

    public bool ContainsKey(byte[] prefix)
    {
        return h.Contains(prefix);
    }

    public void Remove(byte[] prefix)
    {
        h.Remove(prefix);
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return h.GetEnumerator();
    }


    private class ByteComparer : IEqualityComparer
    {
        bool IEqualityComparer.Equals(object x, object y)
        {
            byte[] xBytes = (byte[])x;
            byte[] yBytes = (byte[])y;

            // Make sure lengths are the same
            if (xBytes.Length == yBytes.Length)
            {
                // Compare bytes
                for (int i = 0; i < xBytes.Length; i++)
                {
                    if (xBytes[i] != yBytes[i])
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        int IEqualityComparer.GetHashCode(object obj)
        {
            // This is very specific to this collection and not terribly intelligent
            // but it doesn't need to be, because we don't expect many items in the hashtable
            return ((byte[])obj).Length;
        }
    }
}
