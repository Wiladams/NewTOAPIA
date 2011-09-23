namespace NewTOAPIA.DirectShow.Core
{
	using System;
	using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

	using NewTOAPIA.DirectShow.Core;
    using NewTOAPIA.DirectShow.DES;

	/// <summary>
	/// Summary description for Filter.
	/// </summary>
	public class Filter : IComparable
	{
        IBaseFilter m_Filter;

		// Device name
		public readonly string FriendlyName;
        public readonly string MonikerString;   // Moniker string

        #region Constructors
        // Create new filter from moniker`s string
        internal Filter(string monikerString)
        {
            MonikerString = monikerString;
            FriendlyName = GetName(monikerString);
        }

		// Create new filter from it's Moniker
		public Filter(IMoniker moniker)
		{
			MonikerString = GetMonikerString(moniker);
            FriendlyName = GetFriendlyName(moniker);
        }
        #endregion

        // Get pin of the filter
        public IPin GetPin(PinDirection dir, int num)
        {
            IPin[] pin = new IPin[1];

            IEnumPins pinsEnum = null;

            // enum filter pins
            if (m_Filter.EnumPins(out pinsEnum) == 0)
            {
                PinDirection pinDir;
                int n;

                // get next pin
                while (pinsEnum.Next(1, pin, out n) == 0)
                {
                    // query pin`s direction
                    pin[0].QueryDirection(out pinDir);

                    if (pinDir == dir)
                    {
                        if (num == 0)
                            return pin[0];
                        num--;
                    }

                    Marshal.ReleaseComObject(pin[0]);
                    pin[0] = null;
                }
            }
            return null;
        }

        // Get input pin of the filter
        public IPin GetInPin(IBaseFilter filter, int num)
        {
            return GetPin(PinDirection.Input, num);
        }

        // Get output pin of the filter
        public IPin GetOutPin(int num)
        {
            return GetPin(PinDirection.Output, num);
        }

        // Get moniker string of the moniker
		private string GetMonikerString(IMoniker moniker)
		{
			string str;
			moniker.GetDisplayName(null, null, out str);

			return str;
		}

		// Get filter name represented by the moniker
		private string GetFriendlyName(IMoniker moniker)
		{
			Object			bagObj = null;
			IPropertyBag	bag = null;

			try
			{
				Guid bagId = typeof(IPropertyBag).GUID;

				// get property bag of the moniker
				moniker.BindToStorage(null, null, ref bagId, out bagObj);
				bag = (IPropertyBag) bagObj;

				// read FriendlyName
				object val = "";
				int hr = bag.Read("FriendlyName", out val, null);
				if (hr != 0)
					Marshal.ThrowExceptionForHR(hr);

				// get it as string
				string ret = val as string;
				if ((ret == null) || (ret.Length < 1))
					throw new ApplicationException();

				return ret;
			}	
			catch (Exception)
			{
				return "";
			}
			finally
			{
				// release all COM objects
				bag = null;
				if (bagObj != null)
				{
					Marshal.ReleaseComObject(bagObj);
					bagObj = null;
				}
			}
		}

		// Get filter name represented by the moniker string
		private string GetName(string monikerString)
		{
			IBindCtx bindCtx = null;
			IMoniker moniker = null;
			String name = "";
			int n = 0;

			// create bind context
			if (TOAPI.Ole.Ole32.CreateBindCtx(0, out bindCtx) == 0)
			{
				// convert moniker`s string to a moniker
				if (TOAPI.Ole.Ole32.MkParseDisplayName(bindCtx, monikerString, ref n, out moniker) == 0)
				{
					// get device name
                    name = GetFriendlyName(moniker);

					Marshal.ReleaseComObject(moniker);
					moniker = null;
				}
				Marshal.ReleaseComObject(bindCtx);
				bindCtx = null;
			}
			return name;
		}

		// Compares objects of the type
		public int CompareTo(object obj)
		{
			if (obj == null)
				return 1;

			Filter f = (Filter) obj;
			return (this.FriendlyName.CompareTo(f.FriendlyName));
		}

        public static Filter CreateFromMonikerString(string monikerString)
        {
            Filter aFilter = new Filter(monikerString);

            return aFilter;
        }
	}
}
