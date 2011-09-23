using System;
using System.Text;
using TOAPI.Kernel32;

/// <summary>
/// ComputerNameFormat - Enum used with the GetComputerNameEx() kernel32 system call
/// </summary>
enum ComputerNameFormat
{
    NetBIOS,
    DnsHostname,
    DnsDomain,
    DnsFullyQualified,
    NamePhysicalNetBIOS,
    PhysicalDnsHostname,
    PhysicalDnsDomain,
    PhysicalDnsFullyQualified
}

/// <summary>
/// Summary description for Class1
/// </summary>
public class Machine
{
    string fShortName = null;
    string fDomainName = null;

	public Machine()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string ShortName
    {
        get {
            if (null == fShortName)
            {
                StringBuilder builder = new StringBuilder(256);
                int aSize = 0;
                Kernel32.GetComputerNameEx((int)ComputerNameFormat.DnsHostname, IntPtr.Zero, ref aSize);

                if (true == Kernel32.GetComputerNameEx((int)ComputerNameFormat.DnsHostname, builder, ref aSize))
                {
                    fShortName = builder.ToString();
                }
                else
                    fShortName = string.Empty;
            }
        
            return fShortName; 
        }
    }

    public string DomainName
    {
        get
        {
            if (null == fDomainName)
            {
                StringBuilder builder = new StringBuilder(256);
                int aSize = 0;
                Kernel32.GetComputerNameEx((int)ComputerNameFormat.DnsDomain, IntPtr.Zero, ref aSize);

                if (true == Kernel32.GetComputerNameEx((int)ComputerNameFormat.DnsDomain, builder, ref aSize))
                {
                    fDomainName = builder.ToString();
                }
                else
                    fDomainName = string.Empty;
            }

            return fDomainName;
        }
    }
}
