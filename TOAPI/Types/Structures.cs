//////////////////////////////////////////////////////////////////////
//
// structures.cs
//
// DirectShow interop wrappers for .NET applications.
// Transcoded directly from DirectShow C++ header files.
// For more information, contact Ron Logan (ronlo@microsoft.com)
//
// Copyright © 2004 by Microsoft Corporation. All Rights Reserved.
//
//////////////////////////////////////////////////////////////////////

using System;
using System.Runtime.InteropServices;
using System.Text;


    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct DexterParam
    {
        public string Name;
        public long DispID;
        public int nValues;
    }

    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct DexterValue
    {
        public object v;
        public long rt;
        public int dwInterp;
    }

    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public class OptionalLong
    {
        public long Value;

        public OptionalLong( long val )
        {
            Value = val;
        }

        public static implicit operator OptionalLong( long val )
        {
            return new OptionalLong( val );
        }
    }

    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public class OptionalGuid
    {
        public Guid Value;

        public OptionalGuid( Guid guid )
        {
            Value = guid;
        }
 
        public static implicit operator OptionalGuid( Guid guid )
        {
            return new OptionalGuid( guid );
        }
    }
