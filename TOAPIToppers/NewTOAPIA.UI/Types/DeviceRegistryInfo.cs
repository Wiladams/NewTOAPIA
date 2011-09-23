using System;
using Microsoft.Win32;

namespace NewTOAPIA.UI
{
    public class DeviceRegistryInfo
    {
        public string FullName; // From GetRawDeviceInfo

        // These are parsed from the Full Name
        public string ClassCode;
        public string SubClass;
        public string Protocol;
        public Guid ClassGUID;

        // These are obtained from Registry Lookups
        int Capabilities;
        public string ClassName;
        public string Description;
        string Driver;
        string [] HardwareID;
        string Manufacturer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceName"></param>
        public DeviceRegistryInfo(string deviceName)
        {
            FullName = deviceName;

            // A typical name is something like:  \\?\ACPI#PNP0303#3&13c0b0c5&0#{884b96c3-56ef-11d1-bc8c-00a0c91405dd}
            // First remove the front part
            // remove the \??\
            FullName = FullName.Substring(4);

            // Now, split the string into component parts based on the hash mark
            string[] split = FullName.Split('#');

            ClassCode = split[0];    // ACPI (Class code)
            SubClass = split[1];    // PNP0303 (SubClass code)
            Protocol = split[2];    // 3&13c0b0c5&0 (Protocol code)
            ClassGUID = new Guid(split[3]); // Class GUID

            // Now go look things up in the registry
            LookupRegistryEntries();
        }

        void LookupRegistryEntries()
        {
            RegistryKey aKey = Registry.LocalMachine;

            // Format the string for the key
            string findkey = string.Format(@"System\CurrentControlSet\Enum\{0}\{1}\{2}", 
                ClassCode, SubClass, Protocol);

            RegistryKey theKey = aKey.OpenSubKey(findkey);

            ClassName = (string)theKey.GetValue("Class");
            if (ClassName != null)
                ClassName = ClassName.ToUpper();

            // For the description, we only want what is after
            // the first ';' in the string
            Description = (string)theKey.GetValue("DeviceDesc");
            string[] descsplit = Description.Split(';');
            if (descsplit.Length > 1)
                Description = descsplit[1];

            Capabilities = (int)theKey.GetValue("Capabilities");
            Driver = (string)theKey.GetValue("Driver");
            HardwareID = (string[])theKey.GetValue("HardwareID");

            Manufacturer = (string)theKey.GetValue("Mfg");
            string[] mfgsplit = Manufacturer.Split(';');
            if (mfgsplit.Length > 1)
                Manufacturer = mfgsplit[1];

            theKey.Close();
        }

        public override string ToString()
        {
            string aString = string.Format("<{0}>{1}</{0}>",
                ClassName, Description);
            return aString;
        }
    }
}
