using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

using NewTOAPIA.DirectShow.DES;

namespace NewTOAPIA.DirectShow.Core
{
    public class DsDevice : IDisposable
    {
        #region Fields
        IBaseFilter m_BaseFilter;
        private IMoniker m_Mon;
        private string m_FriendlyName;
        private int fIndex;
        #endregion

        #region Constructors
        public DsDevice(IMoniker Mon)
        {
            fIndex = -1;
            m_Mon = Mon;
            m_FriendlyName = null;
        }

        public DsDevice(IMoniker Mon, int index)
        {
            fIndex = index;
            m_Mon = Mon;
            m_FriendlyName = null;
        }
        #endregion

        #region Properties
        public int Index
        {
            get { return fIndex; }
        }

        public IMoniker Mon
        {
            get
            {
                return m_Mon;
            }
        }

        public IBaseFilter BaseFilter
        {
            get
            {
                if (null == m_BaseFilter)
                {
                    m_BaseFilter = GetBaseFilter();
                }

                return m_BaseFilter;
            }
        }

        public string FriendlyName
        {
            get
            {
                if (m_FriendlyName == null)
                {
                    m_FriendlyName = GetFriendlyName();
                }
                return m_FriendlyName;
            }
        }

        /// <summary>
        /// Returns a unique identifier for a device
        /// </summary>
        public string DevicePath
        {
            get
            {
                string s = null;

                try
                {
                    m_Mon.GetDisplayName(null, null, out s);
                }
                catch
                {
                }

                return s;
            }
        }
        #endregion

        private IBaseFilter GetBaseFilter()
        {
            Guid iid = typeof(IBaseFilter).GUID;
            object sourceObj = null;
            m_Mon.BindToObject(null, null, ref iid, out sourceObj);
            IBaseFilter aFilter = (IBaseFilter)sourceObj;

            return aFilter;

        }

        /// <summary>
        /// Get the FriendlyName for a moniker
        /// </summary>
        /// <returns>String or null on error</returns>
        private string GetFriendlyName()
        {
            IPropertyBag bag = null;
            string ret = null;
            object bagObj = null;
            object val = null;

            try
            {
                Guid bagId = typeof(IPropertyBag).GUID;
                m_Mon.BindToStorage(null, null, ref bagId, out bagObj);

                bag = (IPropertyBag)bagObj;

                int hr = bag.Read("FriendlyName", out val, null);
                DsError.ThrowExceptionForHR(hr);

                ret = val as string;
            }
            catch
            {
                ret = null;
            }
            finally
            {
                bag = null;
                if (bagObj != null)
                {
                    Marshal.ReleaseComObject(bagObj);
                    bagObj = null;
                }
            }

            return ret;
        }

        public void Dispose()
        {
            if (Mon != null)
            {
                Marshal.ReleaseComObject(Mon);
                m_Mon = null;
                GC.SuppressFinalize(this);
            }
            m_FriendlyName = null;
        }

        /// <summary>
        /// Returns an array of DsDevices of type devcat.
        /// </summary>
        /// <param name="cat">Any one of FilterCategory</param>
        public static DsDevice[] GetDevicesOfCategory(Guid FilterCategory)
        {
            int hr;

            List<DsDevice> devs = new List<DsDevice>();
            IEnumMoniker enumMon;

            // 1. Get the inteface  ICreateDevEnum
            // throw an exception if this fails.
            ICreateDevEnum enumDev = (ICreateDevEnum)new CreateDevEnum();

            // 2. Obtain a category enumerator by calling ICreateDevEnum::CreateClassEnumerator 
            // with the CLSID of the desired category. This method returns an IEnumMoniker 
            // interface pointer. If the category is empty (or does not exist), the method 
            // returns S_FALSE rather than an error code. If so, the returned IEnumMoniker 
            // pointer is NULL and dereferencing it will cause an exception. Therefore, explicitly 
            // test for S_OK when you call CreateClassEnumerator, instead of calling the 
            // usual SUCCEEDED macro.
            hr = enumDev.CreateClassEnumerator(FilterCategory, out enumMon, 0);
            DsError.ThrowExceptionForHR(hr);

            // 3. Use the IEnumMoniker::Next method to enumerate each moniker. This method 
            // returns an IMoniker interface pointer. When the Next method reaches the end 
            // of the enumeration, it also returns S_FALSE, so again check for S_OK.
            if (hr != 1)
            {
                try
                {
                    try
                    {
                        IMoniker[] mon = new IMoniker[1];

                        while ((enumMon.Next(1, mon, IntPtr.Zero) == 0))
                        {
                            try
                            {
                                // The devs array now owns this object.  Don't
                                // release it if we are going to be successfully
                                // returning the devret array
                                devs.Add(new DsDevice(mon[0]));
                            }
                            catch
                            {
                                Marshal.ReleaseComObject(mon[0]);
                                throw;
                            }
                        }
                    }
                    finally
                    {
                        Marshal.ReleaseComObject(enumMon);
                    }
                }
                catch
                {
                    foreach (DsDevice d in devs)
                    {
                        d.Dispose();
                    }
                    throw;
                }
            }
            else
            {
                //devret = new DsDevice[0];
            }

            return devs.ToArray();
        }

    }
}
