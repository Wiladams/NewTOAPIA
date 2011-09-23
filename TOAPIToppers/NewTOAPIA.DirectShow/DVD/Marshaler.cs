using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.DVD
{
    using System.Runtime.InteropServices;

    using NewTOAPIA.DirectShow.Core;
    
    // c# does not correctly create structures that contain ByValArrays of structures (or enums!).  Instead
    // of allocating enough room for the ByValArray of structures, it only reserves room for a ref,
    // even when decorated with ByValArray and SizeConst.  Needless to say, if DirectShow tries to
    // write to this too-short buffer, bad things will happen.
    //
    // To work around this for the DvdTitleAttributes structure, use this custom marshaler
    // by declaring the parameter DvdTitleAttributes as:
    //
    //    [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef=typeof(DTAMarshaler))]
    //    DvdTitleAttributes pTitle
    //
    // See DsMarshaler for more info on custom marshalers
    internal class DTAMarshaler : DsMarshaler
    {
        public DTAMarshaler(string cookie)
            : base(cookie)
        {
        }

        // Called just after invoking the COM method.  The IntPtr is the same one that just got returned
        // from MarshalManagedToNative.  The return value is unused.
        override public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            DvdTitleAttributes dta = m_obj as DvdTitleAttributes;

            // Copy in the value, and advance the pointer
            dta.AppMode = (DvdTitleAppMode)Marshal.ReadInt32(pNativeData);
            pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(int)));

            // Copy in the value, and advance the pointer
            dta.VideoAttributes = (DvdVideoAttributes)Marshal.PtrToStructure(pNativeData, typeof(DvdVideoAttributes));
            pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(DvdVideoAttributes)));

            // Copy in the value, and advance the pointer
            dta.ulNumberOfAudioStreams = (int)Marshal.ReadInt32(pNativeData);
            pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(int)));

            // Allocate a large enough array to hold all the returned structs.
            dta.AudioAttributes = new DvdAudioAttributes[8];
            for (int x = 0; x < 8; x++)
            {
                // Copy in the value, and advance the pointer
                dta.AudioAttributes[x] = (DvdAudioAttributes)Marshal.PtrToStructure(pNativeData, typeof(DvdAudioAttributes));
                pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(DvdAudioAttributes)));
            }

            // Allocate a large enough array to hold all the returned structs.
            dta.MultichannelAudioAttributes = new DvdMultichannelAudioAttributes[8];
            for (int x = 0; x < 8; x++)
            {
                // MultichannelAudioAttributes has nested ByValArrays.  They need to be individually copied.

                dta.MultichannelAudioAttributes[x].Info = new DvdMUAMixingInfo[8];

                for (int y = 0; y < 8; y++)
                {
                    // Copy in the value, and advance the pointer
                    dta.MultichannelAudioAttributes[x].Info[y] = (DvdMUAMixingInfo)Marshal.PtrToStructure(pNativeData, typeof(DvdMUAMixingInfo));
                    pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(DvdMUAMixingInfo)));
                }

                dta.MultichannelAudioAttributes[x].Coeff = new DvdMUACoeff[8];

                for (int y = 0; y < 8; y++)
                {
                    // Copy in the value, and advance the pointer
                    dta.MultichannelAudioAttributes[x].Coeff[y] = (DvdMUACoeff)Marshal.PtrToStructure(pNativeData, typeof(DvdMUACoeff));
                    pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(DvdMUACoeff)));
                }
            }

            // The DvdMultichannelAudioAttributes needs to be 16 byte aligned
            pNativeData = (IntPtr)(pNativeData.ToInt64() + 4);

            // Copy in the value, and advance the pointer
            dta.ulNumberOfSubpictureStreams = (int)Marshal.ReadInt32(pNativeData);
            pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(int)));

            // Allocate a large enough array to hold all the returned structs.
            dta.SubpictureAttributes = new DvdSubpictureAttributes[32];
            for (int x = 0; x < 32; x++)
            {
                // Copy in the value, and advance the pointer
                dta.SubpictureAttributes[x] = (DvdSubpictureAttributes)Marshal.PtrToStructure(pNativeData, typeof(DvdSubpictureAttributes));
                pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(DvdSubpictureAttributes)));
            }

            // Note that 4 bytes (more alignment) are unused at the end

            return null;
        }

        // The number of bytes to marshal out
        override public int GetNativeDataSize()
        {
            // This is the actual size of a DvdTitleAttributes structure
            return 3208;
        }

        // This method is called by interop to create the custom marshaler.  The (optional)
        // cookie is the value specified in MarshalCookie="asdf", or "" is none is specified.
        public static ICustomMarshaler GetInstance(string cookie)
        {
            return new DTAMarshaler(cookie);
        }
    }


    // See DTAMarshaler for an explanation of the problem.  
    // This class is for marshaling
    // a DvdKaraokeAttributes structure.
    internal class DKAMarshaler : DsMarshaler
    {
        // The constructor.  This is called from GetInstance (below)
        public DKAMarshaler(string cookie)
            : base(cookie)
        {
        }

        // Called just after invoking the COM method.  The IntPtr is the same one that just got returned
        // from MarshalManagedToNative.  The return value is unused.
        override public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            DvdKaraokeAttributes dka = m_obj as DvdKaraokeAttributes;

            // Copy in the value, and advance the pointer
            dka.bVersion = (byte)Marshal.ReadByte(pNativeData);
            pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(byte)));

            // DWORD Align
            pNativeData = (IntPtr)(pNativeData.ToInt64() + 3);

            // Copy in the value, and advance the pointer
            dka.fMasterOfCeremoniesInGuideVocal1 = Marshal.ReadInt32(pNativeData) != 0;
            pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(bool)));

            // Copy in the value, and advance the pointer
            dka.fDuet = Marshal.ReadInt32(pNativeData) != 0;
            pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(typeof(bool)));

            // Copy in the value, and advance the pointer
            dka.ChannelAssignment = (DvdKaraokeAssignment)Marshal.ReadInt32(pNativeData);
            pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(DvdKaraokeAssignment.GetUnderlyingType(typeof(DvdKaraokeAssignment))));

            // Allocate a large enough array to hold all the returned structs.
            dka.wChannelContents = new DvdKaraokeContents[8];
            for (int x = 0; x < 8; x++)
            {
                // Copy in the value, and advance the pointer
                dka.wChannelContents[x] = (DvdKaraokeContents)Marshal.ReadInt16(pNativeData);
                pNativeData = (IntPtr)(pNativeData.ToInt64() + Marshal.SizeOf(DvdKaraokeContents.GetUnderlyingType(typeof(DvdKaraokeContents))));
            }

            return null;
        }

        // The number of bytes to marshal out
        override public int GetNativeDataSize()
        {
            // This is the actual size of a DvdKaraokeAttributes structure.
            return 32;
        }

        // This method is called by interop to create the custom marshaler.  The (optional)
        // cookie is the value specified in MarshalCookie="asdf", or "" is none is specified.
        public static ICustomMarshaler GetInstance(string cookie)
        {
            return new DKAMarshaler(cookie);
        }
    }

}
