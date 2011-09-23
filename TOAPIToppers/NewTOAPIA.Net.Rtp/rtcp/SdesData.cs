

namespace NewTOAPIA.Net.Rtp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    
    /// <summary>
    /// SdesData structure, used extensively among Rtcp and Rtp objects to describe the common properties 
    /// associated with the Rtp data / Rtcp Participant.
    /// See RFC 3550 for definitions on the data it contains.
    /// </summary>
    public class SdesData : RtcpData
    {
        #region Statics

        private const int MAX_PROPERTY_LENGTH = 255;
        private const int MAX_PRIV_PROPERTY_LENGTH = 254;

        private static System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();

        #endregion Statics

        #region Members

        /// <summary>
        /// These properties are stored as byte[] instead of string because they will be sent
        /// across the wire in byte[] format more often than they will be updated.  It is also
        /// helpful to know how many bytes of storage (size) a particular instance requires.
        /// And they can be manipulated more efficiently in loops.
        /// 
        /// We use the SDESType as the index.  Unfortunately, the SDESType starts with End = 0.  In
        /// order to avoid a bunch of -1 math, we just allocate the extra storage (4 bytes) at 0 
        /// and never use it.
        /// </summary>
        private byte[][] data = new byte[(int)SDESType.NOTE + 1][];

        /// <summary>
        /// A place to store private SDES extensions
        /// 
        /// According to a close reading of RFC 3550 6.5.8 and emails exchanged with Colin Perkins
        /// Private extensions are to be transmitted as UTF8 strings to remain consistent with the
        /// rest of the SDES items, even though that may not be the most efficient storage.
        /// 
        /// It is also desirable to not risk breaking other implementations by packing the data
        /// efficiently as bytes (1 == True, and 0 == False) and producing an invalid UTF8 string
        /// </summary>
        private SdesPrivateExtensionHashtable privs = new SdesPrivateExtensionHashtable();

        #endregion Members

        #region Constructors

        /// <summary>
        /// Most common constructor, setting the most relevant data
        /// </summary>
        /// <param name="cName">Canonical name - Unique network identifier for this participant</param>
        /// <param name="name">Friendly name for this participant</param>
        public SdesData(string cName, string name)
        {
            SetCName(cName);
            Name = name;
        }


        /// <summary>
        /// Constructs an SdesData instance by reading its properties from another instance 
        /// </summary>
        /// <param name="data"></param>
        public SdesData(SdesData sdes)
        {
            // Skip the first index - see comments for member variable 'data'
            for (int i = 1; i < sdes.data.Length; i++)
            {
                byte[] data = sdes.data[i];

                if (data != null)
                {
                    byte[] copy = new byte[data.Length];
                    data.CopyTo(copy, 0);
                    data = copy;
                }

                this.data[i] = data;
            }

            foreach (DictionaryEntry de in sdes.privs)
            {
                SetPrivateExtension((byte[])de.Key, (byte[])de.Value);
            }

        }


        /// <summary>
        /// Constructs an SdesData instance by reading its properties from a BufferChunk 
        /// </summary>
        /// <param name="buffer"></param>
        public SdesData(BufferChunk buffer)
        {
            ReadDataFromBuffer(buffer);
        }


        #endregion Constructors

        #region Methods

        #region Public

        /// <summary>
        /// Serializes this object into the provided buffer
        /// </summary>
        /// <param name="buffer"></param>
        public override void WriteDataToBuffer(BufferChunk buffer)
        {
            // TODO - we don't scale well, because we write every property, every time
            // Colin's book recommends Name 7/8 times, and alternating the others (1/8)
            // CName is a given of course JVE

            // Well-known properties
            for (int id = (int)SDESType.CNAME; id <= (int)SDESType.NOTE; id++)
            {
                WritePropertyToBuffer((SDESType)id, data[id], buffer);
            }

            // Write private properties
            foreach (DictionaryEntry de in privs)
            {
                WritePrivatePropertyToBuffer((byte[])de.Key, (byte[])de.Value, buffer);
            }

            // Indicate the list is finished
            buffer += (byte)SDESType.END;
        }


        /// <summary>
        /// Deserializes the provided buffer into this object
        /// </summary>
        /// <param name="buffer"></param>
        public override void ReadDataFromBuffer(BufferChunk buffer)
        {
            SDESType type;

            while ((type = (SDESType)buffer.NextByte()) != SDESType.END)
            {
                switch (type)
                {
                    case SDESType.CNAME:
                    case SDESType.EMAIL:
                    case SDESType.LOC:
                    case SDESType.NAME:
                    case SDESType.NOTE:
                    case SDESType.PHONE:
                    case SDESType.TOOL:
                        ReadPropertyFromBuffer((int)type, buffer);
                        break;

                    case SDESType.PRIV:
                        ReadPrivatePropertyFromBuffer(buffer);
                        break;

                    default:
                        throw new ApplicationException(string.Format(CultureInfo.CurrentCulture,
                            Strings.UnexpectedSDESType, type));
                }
            }
        }


        /// <summary>
        /// Return the size in bytes of this SdesData instance
        /// Used to find out how much buffer space it will take to serialize this instance
        /// </summary>
        public override int Size
        {
            get
            {
                int ret = 0;

                // Well-known properties
                for (int id = (int)SDESType.CNAME; id <= (int)SDESType.NOTE; id++)
                {
                    if (data[id] != null) // We don't write null data
                    {
                        ret += data[id].Length + 2; // Type + Length byte
                    }
                }

                // Private properties
                foreach (DictionaryEntry de in privs)
                {
                    ret += 3; // Type + Length byte + Prefix Length byte

                    // Hashtables don't allow a null key, so no need to check for null
                    ret += ((byte[])de.Key).Length;

                    // Value can be null though
                    if (de.Value != null)
                    {
                        ret += ((byte[])de.Value).Length;
                    }
                }

                // +1 for the SDESType.END marker
                return ++ret;
            }
        }


        /// <summary>
        /// Canonical name - Unique network identifier for this participant
        /// Usually an email address, but a domain name domain\user or GUID would work as well
        /// </summary>
        public string CName
        {
            get { return GetProperty(SDESType.CNAME); }
        }


        /// <summary>
        /// Friendly name of the participant, e.g. "Dogbert"
        /// </summary>
        public string Name
        {
            get { return GetProperty(SDESType.NAME); }
            set { SetProperty(value, SDESType.NAME); }
        }


        /// <summary>
        /// Email address
        /// </summary>
        public string Email
        {
            get { return GetProperty(SDESType.EMAIL); }
            set { SetProperty(value, SDESType.EMAIL); }
        }


        /// <summary>
        /// Phone number
        /// </summary>
        public string Phone
        {
            get { return GetProperty(SDESType.PHONE); }
            set { SetProperty(value, SDESType.PHONE); }
        }


        /// <summary>
        /// Physical Location.  AKA Microsoft Campus, 112/2379, Redmond, WA, USA, Third Rock from the Sun
        /// </summary>
        public string Location
        {
            get { return GetProperty(SDESType.LOC); }
            set { SetProperty(value, SDESType.LOC); }
        }


        /// <summary>
        /// Name and Version of tool sending streams, e.g. 'ConferenceXP v1.0'
        /// </summary>
        public string Tool
        {
            get { return GetProperty(SDESType.TOOL); }
        }


        /// <summary>
        /// Whatever the user wants to communicate to others on a periodic basis
        /// Similar to Instant Messenger status
        /// This isn't real time chat  :-)
        /// </summary>
        public string Note
        {
            get { return GetProperty(SDESType.NOTE); }
            set { SetProperty(value, SDESType.NOTE); }
        }


        /// <summary>
        /// Flag indicating whether you want the tool field to be set or not
        /// </summary>
        /// <param name="val"></param>
        public void SetTool(bool wantSet)
        {
            string tool = null;

            if (wantSet)
            {
                AssemblyName exe = Assembly.GetEntryAssembly().GetName();
                AssemblyName rtp = Assembly.GetExecutingAssembly().GetName();

                tool = exe.Name + " v" + exe.Version + ", ";
                tool += rtp.Name + " v" + rtp.Version;
            }

            SetProperty(tool, SDESType.TOOL);
        }


        /// <summary>
        /// Add a private extension to the SDES data
        /// 
        /// This method validates the data will fit in the 255 bytes (length = (1 Byte) = 255 max) 
        /// available when converted to UTF8 for transmission across the wire and then stores it
        /// 
        /// ---------------------------------------------------------------------------------------
        /// Structure:
        /// ---------------------------------------------------------------------------------------
        ///  0                   1                   2                   3
        ///  0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
        /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        /// |     PRIV=8    |     length    | prefix length |prefix string...
        /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        /// ...             |               value string                  ...
        /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        /// </summary>
        /// <param name="prefix">Name of the extension</param>
        /// <param name="data">Value of the extension</param>
        public void SetPrivateExtension(string prefix, string data)
        {
            // Prefix cannot be null, it is the hashtable key
            if (prefix == null)
            {
                throw new ArgumentNullException(Strings.Prefix);
            }

            byte[] prefixBytes;

            lock (utf8)
            {
                prefixBytes = utf8.GetBytes(prefix);
            }

            // Data can be null though, if the prefix communicates enough data
            byte[] dataBytes = null;
            int dataLength = 0;

            if (data != null)
            {
                lock (utf8)
                {
                    dataBytes = utf8.GetBytes(data);
                    dataLength = dataBytes.Length;
                }
            }

            // Check to see if it is too long
            if (prefixBytes.Length + dataLength > MAX_PRIV_PROPERTY_LENGTH)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.PrefixAndDataBytesExceeded,
                    MAX_PRIV_PROPERTY_LENGTH));
            }

            SetPrivateExtension(prefixBytes, dataBytes);
        }

        public string GetPrivateExtension(string prefix)
        {
            string data = null;

            if (privs != null)
            {
                lock (utf8)
                {
                    byte[] prefixBytes = utf8.GetBytes(prefix);

                    if (privs.ContainsKey(prefixBytes))
                    {
                        data = utf8.GetString(privs[prefixBytes]);
                    }
                }
            }

            return data;
        }

        /// <summary>
        /// Gets the private extensions for this stream in a Hashtable with (string, string) key/value pairs.
        /// </summary>
        public Dictionary<string, string> GetPrivateExtensions()
        {
            Dictionary<string, string> priExns = new Dictionary<string, string>();
            ICollection keys = privs.Keys;
            foreach (byte[] key in keys)
            {
                byte[] val = privs[key];
                priExns.Add(utf8.GetString(key), utf8.GetString(val));
            }

            return priExns;
        }


        public override string ToString()
        {
            string str = string.Format(CultureInfo.CurrentCulture, "SdesData [ CName: {0}, Name: {1}, Email: {2}, " +
                "Phone: {3}, Location: {4}, Tool: {5}, Note: {6}", CName, Name, Email, Phone, Location, Tool,
                Note);

            if (privs != null)
            {
                foreach (DictionaryEntry de in privs)
                {
                    string prefix, data = null;

                    lock (utf8)
                    {
                        prefix = utf8.GetString((byte[])de.Key);

                        if (de.Value != null)
                        {
                            data = utf8.GetString((byte[])de.Value);
                        }
                    }
                    str += ", " + prefix + ": " + data;
                }
            }

            str += " ]";
            return str;
        }


        #endregion Public

        #region Internal

        /// <summary>
        /// This method is called to update the local data from another SdesData
        /// </summary>
        /// <param name="data">what we want to update our data to</param>
        /// <returns>true if the local data was updated, otherwise false</returns>
        internal bool UpdateData(SdesData sdes)
        {
            bool ret = false;

            // Well-known properties
            // CName can never be updated, so start with Name
            for (int id = (int)SDESType.NAME; id <= (int)SDESType.NOTE; id++)
            {
                if (!data[id].Compare(sdes.data[id]))
                {
                    data[id] = sdes.data[id].Copy();
                    ret = true;
                }
            }

            // Write private properties
            foreach (DictionaryEntry de in sdes.privs)
            {
                byte[] key = (byte[])de.Key;
                byte[] data = (byte[])de.Value;

                if (!privs.Contains(key) || !data.Compare(privs[key]))
                {
                    privs[key.Copy()] = data.Copy();
                    ret = true;
                }
            }

            return ret;
        }


        #endregion Internal

        #region Private

        private void SetCName(string cName)
        {
            // CName must be something valid
            if (null == cName || string.Empty == cName)
            {
                throw new ApplicationException(Strings.cNameMustBeAValidString);
            }

            SetProperty(cName, SDESType.CNAME);
        }


        /// <summary>
        /// ---------------------------------------------------------------------------------------
        /// Purpose:
        /// ---------------------------------------------------------------------------------------
        /// Make sure the data will fit in the 255 bytes (length == 1 byte == byte.MaxValue) 
        /// available to it when converted to UTF8 for transmission across the wire
        /// 
        /// ---------------------------------------------------------------------------------------
        /// General structure of an SDES property:
        /// ---------------------------------------------------------------------------------------
        ///  0                   1                   2                   3
        ///  0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
        /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        /// |     SDES=N    |     length    |        data                 ...
        /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        /// </summary>
        /// <param name="data"></param>
        private void SetProperty(string data, SDESType type)
        {
            byte[] bytes = null;

            if (data != null)
            {
                lock (utf8)
                {
                    bytes = utf8.GetBytes(data);
                }

                // Check to see if it is too long
                if (bytes.Length > MAX_PROPERTY_LENGTH)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.SDESItemDataBytesExceeded,
                        MAX_PROPERTY_LENGTH, bytes.Length, data));
                }
            }

            this.data[(int)type] = bytes;
        }

        private string GetProperty(SDESType type)
        {
            string ret = null;

            if (data[(int)type] != null)
            {
                lock (utf8)
                {
                    ret = utf8.GetString(data[(int)type]);
                }
            }

            return ret;
        }


        private void SetPrivateExtension(byte[] prefix, byte[] data)
        {
            // If the collection does not exist, create it
            if (privs == null)
            {
                //privs = new Dictionary<byte[], byte[]>();
                privs = new SdesPrivateExtensionHashtable();
            }

            // Set the value (this will add if it does not exist)
            privs[prefix] = data;
        }


        private void WritePropertyToBuffer(SDESType type, byte[] data, BufferChunk buffer)
        {
            if (data != null)
            {
                // Type
                buffer += (byte)type;

                // Length
                buffer += (byte)data.Length;

                // Data
                if (data.Length != 0)
                {
                    buffer += data;
                }
            }
        }

        private void ReadPropertyFromBuffer(int type, BufferChunk buffer)
        {
            int dataLength = buffer.NextByte();

            if (dataLength != 0)
            {
                data[type] = (byte[])buffer.NextBufferChunk(dataLength);
            }
            else // Clear value
            {
                data[type] = null;
            }
        }


        private void WritePrivatePropertyToBuffer(byte[] prefix, byte[] data, BufferChunk buffer)
        {
            int prefixLength = prefix.Length;

            int dataLength = 0;
            if (data != null)
            {
                dataLength = data.Length;
            }

            // This should have already been validated as the property was added/set
            Debug.Assert(prefixLength + dataLength <= SdesData.MAX_PRIV_PROPERTY_LENGTH);

            // Write data
            buffer += (byte)SDESType.PRIV;
            buffer += (byte)(prefixLength + dataLength + 1); // +1 = prefix length
            buffer += (byte)prefixLength;
            buffer += prefix;

            if (data != null)
            {
                buffer += data;
            }
        }

        private void ReadPrivatePropertyFromBuffer(BufferChunk buffer)
        {
            byte totalLength = buffer.NextByte();
            byte prefixLength = buffer.NextByte();
            int dataLength = totalLength - prefixLength - 1;

            // The cast to byte[] does a copy
            byte[] prefix = (byte[])buffer.NextBufferChunk(prefixLength);
            byte[] data = null;

            if (dataLength > 0)
            {
                // The cast to byte[] does a copy
                data = (byte[])buffer.NextBufferChunk(dataLength);
            }

            privs[prefix] = data;
        }


        #endregion Private

        #endregion Methods
    }

    internal class SdesReport
    {
        internal uint ssrc;
        internal SdesData sdes;

        internal SdesReport(uint ssrc, SdesData sdes)
        {
            this.ssrc = ssrc;
            this.sdes = sdes;
        }

        internal int Size
        {
            get
            {
                return Rtp.SSRC_SIZE + sdes.Size;
            }
        }
    }
}
