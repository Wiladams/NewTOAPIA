using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using TOAPI.Winsock;

namespace NewTOAPIA.Net
{

    public class PgmSocket : Socket
    {
        public event OnReceive ReceiveEvent;

        public static readonly SocketOptionLevel PGM_LEVEL = (SocketOptionLevel)Winsock.IPPROTO_PGM;
        private IDictionary<int, uint> _socketOptions = new Dictionary<int, uint>();

        private string _ip;
        private int _port;
        private int _receiveBufferSize;
        private int _readBuffer;

        public PgmSocket()
            : base(AddressFamily.InterNetwork, SocketType.Rdm, (ProtocolType)Winsock.IPPROTO_PGM)
        {
        }


        public string Address
        {
            set { _ip = value; }
        }

        public int Port
        {
            set { _port = value; }
        }

        public int ReceiveBuffer
        {
            set { _receiveBufferSize = (value * 1024); }
        }

        public int ReadBuffer
        {
            set { _readBuffer = (value * 1024); }
        }

        public static void SetPgmOption(Socket socket, int option, byte[] value)
        {
            socket.SetSocketOption(PGM_LEVEL, (SocketOptionName)option, value);
        }

        public void SetPgmOption(int option, byte[] value)
        {
            try
            {
                SetSocketOption(PGM_LEVEL, (SocketOptionName)option, value);
            }
            catch (Exception failed)
            {
                Console.WriteLine("failed: {0}", failed);
            }
        }

        public void AddSocketOption(int opt, uint val)
        {
            _socketOptions[opt] = val;
        }

        public IDictionary<int, uint> SocketOptions
        {
            set { _socketOptions = value; }
        }

        public void ApplySocketOptions()
        {
            foreach (int option in _socketOptions.Keys)
            {
                SetSocketOption(this, option.ToString(), option, _socketOptions[option]);
            }
        }

        public static bool EnableGigabit(Socket socket)
        {
            return SetSocketOption(socket, "Gigabit", 1014, 1);
        }

        public static bool SetSocketOption(Socket socket, string name, int option, uint val)
        {
            try
            {
                byte[] bits = BitConverter.GetBytes(val);
                SetPgmOption(socket, option, bits);
                //log.Info("Set: " + name + " Option : " + option + " value: " + val);
                
                return true;
            }
            catch (Exception failed)
            {
                //log.Debug(name + " Option : " + option + " value: " + val, failed);
                return false;
            }
        }

        //public unsafe _RM_RECEIVER_STATS GetReceiverStats(Socket socket)
        //{
        //    int size = sizeof(_RM_RECEIVER_STATS);
        //    byte[] data = socket.GetSocketOption(PGM_LEVEL, (SocketOptionName)1013, size);
        //    fixed (byte* pBytes = &data[0])
        //    {
        //        return *((_RM_RECEIVER_STATS*)pBytes);
        //    }
        //}

        public static byte[] ConvertStructToBytes(object obj)
        {
            int structSize = Marshal.SizeOf(obj);
            byte[] allData = new byte[structSize];
            GCHandle handle =
            GCHandle.Alloc(allData, GCHandleType.Pinned);
            Marshal.StructureToPtr(obj,
            handle.AddrOfPinnedObject(),
            false);
            handle.Free();

            return allData;
        }


        public void Dispose()
        {
            try
            {
                Disconnect(true);
                Close();
            }
            catch (Exception failed)
            {
                //log.Warn("close failed", failed);
            }
        }
    }


}
