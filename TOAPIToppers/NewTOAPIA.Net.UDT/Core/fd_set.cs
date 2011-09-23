namespace NewTOAPIA.Net.Udt
{
    using System;

    using SOCKET = System.Int32;

    public class fd_set
    {
        uint fd_count;
        SOCKET[] fd_array;

        public fd_set(int setSize)
        {
            fd_array = new SOCKET[setSize];
        }
    }
}