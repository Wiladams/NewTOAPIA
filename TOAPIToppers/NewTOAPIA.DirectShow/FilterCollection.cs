namespace NewTOAPIA.DirectShow.Core
{
	using System;
	using System.Collections;
	using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

	using NewTOAPIA.DirectShow.Core;

	/// <summary>
	/// Summary description for FilterCollection.
	/// </summary>
	public class FilterCollection : CollectionBase
	{
		public FilterCollection(Guid category)
		{
			CollectFilters(category);
		}

		// collect filters of specified category
		private void CollectFilters(Guid category)
		{
			object				comObj = null;
			ICreateDevEnum		enumDev = null;
			IEnumMoniker	enumMon = null;
			IMoniker[]		devMon = new IMoniker[1];
			int					hr;

			try
			{
				// Get the system device enumerator
				Type srvType = Type.GetTypeFromCLSID(Clsid.SystemDeviceEnum);
				if (srvType == null)
					throw new ApplicationException("Failed creating device enumerator");

				// create device enumerator
				comObj = Activator.CreateInstance(srvType);
				enumDev = (ICreateDevEnum) comObj;

				// Create an enumerator to find filters of specified category
                hr = enumDev.CreateClassEnumerator(category, out enumMon, CDef.None);
                if (hr != 0)
					throw new ApplicationException("No devices of the category");

				// Collect all filters
				IntPtr n = IntPtr.Zero;
				while (true)
				{
					// Get next filter
					hr = enumMon.Next(1, devMon, n);
					if ((hr != 0) || (devMon[0] == null))
						break;

					// Add the filter
					Filter filter = new Filter(devMon[0]);
					InnerList.Add(filter);

					// Release COM object
					Marshal.ReleaseComObject(devMon[0]);
					devMon[0] = null;
				}

				// Sort the collection
				InnerList.Sort();
			}

			finally
			{
				// release all COM objects
				enumDev = null;
				if (comObj != null)
				{
					Marshal.ReleaseComObject(comObj);
					comObj = null;
				}
				if (enumMon != null)
				{
					Marshal.ReleaseComObject(enumMon);
					enumMon = null;
				}
				if (devMon[0] != null)
				{
					Marshal.ReleaseComObject(devMon[0]);
					devMon[0] = null;
				}
			}
		}

		// Get a filter at the specified index
		public Filter this[int index]
		{
			get
			{
				return ((Filter) InnerList[index]);
			}
		}
	}
}
