namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Collections.Generic;

    public class CHash
    {
        private Dictionary<int, CUDT> fDictionary = new Dictionary<int,CUDT>();

        // Functionality:
        //    Look for a UDT instance from the hash table.
        // Parameters:
        //    1) [in] id: socket ID
        // Returned value:
        //    Pointer to a UDT instance, or null if not found.

        public CUDT Lookup(Int32 id)
        {
            CUDT retValue = null;

            if (fDictionary.TryGetValue(id, out retValue))
                return retValue;

            return null;
        }

        // Functionality:
        //    Insert an entry to the hash table.
        // Parameters:
        //    1) [in] id: socket ID
        //    2) [in] u: pointer to the UDT instance
        // Returned value:
        //    None.

        public void Insert(Int32 id, CUDT u)
        {
            fDictionary.Add(id, u);
        }

        // Functionality:
        //    Remove an entry from the hash table.
        // Parameters:
        //    1) [in] id: socket ID
        // Returned value:
        //    None.

        public void Remove(Int32 id)
        {
            fDictionary.Remove(id);
        }
    }
}

