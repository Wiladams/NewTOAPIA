using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.BDA
{
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    [ComImport, Guid("5FFDC5E6-B83A-4b55-B6E8-C69E765FE9DB")]
    public class TuningSpace
    {
    }

    [ComImport, Guid("8A674B4C-1F63-11d3-B64C-00C04F79498E")]
    public class AnalogRadioTuningSpace
    {
    }

    [ComImport, Guid("F9769A06-7ACA-4e39-9CFB-97BB35F0E77E")]
    public class AuxInTuningSpace
    {
    }

    [ComImport, Guid("8A674B4D-1F63-11d3-B64C-00C04F79498E")]
    public class AnalogTVTuningSpace
    {
    }

    [ComImport, Guid("1BE49F30-0E1B-11d3-9D8E-00C04F72D980")]
    public class LanguageComponentType
    {
    }

    [ComImport, Guid("418008F3-CF67-4668-9628-10DC52BE1D08")]
    public class MPEG2ComponentType
    {
    }

    [ComImport, Guid("A8DCF3D5-0780-4ef4-8A83-2CFFAACB8ACE")]
    public class ATSCComponentType
    {
    }

    [ComImport, Guid("809B6661-94C4-49e6-B6EC-3F0F862215AA")]
    public class Components
    {
    }

    [ComImport, Guid("59DC47A8-116C-11d3-9D8E-00C04F72D980")]
    public class Component
    {
    }

    [ComImport, Guid("055CB2D7-2969-45cd-914B-76890722F112")]
    public class MPEG2Component
    {
    }

    [ComImport, Guid("B46E0D38-AB35-4a06-A137-70576B01B39F")]
    public class TuneRequest
    {
    }

    [ComImport, Guid("0369B4E5-45B6-11d3-B650-00C04F79498E")]
    public class ChannelTuneRequest
    {
    }

    [ComImport, Guid("0369B4E6-45B6-11d3-B650-00C04F79498E")]
    public class ATSCChannelTuneRequest
    {
    }

    [ComImport, Guid("0955AC62-BF2E-4cba-A2B9-A63F772D46CF")]
    public class MPEG2TuneRequest
    {
    }

    [ComImport, Guid("0888C883-AC4F-4943-B516-2C38D9B34562")]
    public class Locator
    {
    }

    [ComImport, Guid("8872FF1B-98FA-4d7a-8D93-C9F1055F85BB")]
    public class ATSCLocator
    {
    }

    [ComImport, Guid("C531D9FD-9685-4028-8B68-6E1232079F1E")]
    public class DVBCLocator
    {
    }

    [ComImport, Guid("15D6504A-5494-499c-886C-973C9E53B9F1")]
    public class DVBTuneRequest
    {
    }

    [ComImport, Guid("A1A2B1C4-0E3A-11d3-9D8E-00C04F72D980")]
    public class ComponentTypes
    {
    }

    [ComImport, Guid("823535A0-0318-11d3-9D8E-00C04F72D980")]
    public class ComponentType
    {
    }

    [ComImport, Guid("A2E30750-6C3D-11d3-B653-00C04F79498E")]
    public class ATSCTuningSpace
    {
    }

    [ComImport, Guid("C6B14B32-76AA-4a86-A7AC-5C79AAF58DA7")]
    public class DVBTuningSpace
    {
    }

    [ComImport, Guid("B64016F3-C9A2-4066-96F0-BD9563314726")]
    public class DVBSTuningSpace
    {
    }

    [ComImport, Guid("9CD64701-BDF3-4d14-8E03-F12983D86664")]
    public class DVBTLocator
    {
    }

    [ComImport, Guid("1DF7D126-4050-47f0-A7CF-4C4CA9241333")]
    public class DVBSLocator
    {
    }

    [ComImport, Guid("8A674B49-1F63-11d3-B64C-00C04F79498E")]
    public class CreatePropBagOnRegKey
    {
    }

    [ComImport, Guid("D02AAC50-027E-11d3-9D8E-00C04F72D980")]
    public class SystemTuningSpaces
    {
    }

    [ComImport, Guid("2C63E4EB-4CEA-41b8-919C-E947EA19A77C")]
    public class MPEG2TuneRequestFactory
    {
    }


    [ComImport, Guid("D9BB4CEE-B87A-47F1-AC92-B08D9C7813FC")]
    public class DigitalCableTuningSpace
    {
    }

    [ComImport, Guid("28AB0005-E845-4FFA-AA9B-F4665236141C")]
    public class AnalogAudioComponentType
    {
    }

    [ComImport, Guid("26EC0B63-AA90-458A-8DF4-5659F2C8A18A")]
    public class DigitalCableTuneRequest
    {
    }

    [ComImport, Guid("6E50CC0D-C19B-4BF6-810B-5BD60761F5CC")]
    public class DigitalLocator
    {
    }

    [ComImport, Guid("49638B91-48AB-48B7-A47A-7D0E75A08EDE")]
    public class AnalogLocator
    {
    }

    [ComImport, Guid("03C06416-D127-407A-AB4C-FDD279ABBE5D")]
    public class DigitalCableLocator
    {
    }

    /// <summary>
    /// From TunerLockType
    /// </summary>
    public enum TunerLockType
    {
        None = 0x00,
        WithinScanSensingRange = 0x01,
        Locked = 0x02
    }

}
