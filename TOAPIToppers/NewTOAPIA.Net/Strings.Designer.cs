﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewTOAPIA.Net {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NewTOAPIA.Net.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All integer values (size, index, length) must be &gt;= 0.
        /// </summary>
        public static string AllIntegerValuesMustBePositive {
            get {
                return ResourceManager.GetString("AllIntegerValuesMustBePositive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BufferChunk does not accept or create null objects.
        /// </summary>
        public static string BufferChunkDoesNotAcceptNull {
            get {
                return ResourceManager.GetString("BufferChunkDoesNotAcceptNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BufferChunk does not accept or create zero-length objects.
        /// </summary>
        public static string BufferChunkDoesNotAcceptZeroLength {
            get {
                return ResourceManager.GetString("BufferChunkDoesNotAcceptZeroLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BufferChunk does not have enough buffer or data to fulfill your request. Requested space:{0} Actual space:{1}.
        /// </summary>
        public static string BufferChunkDoesNotHaveEnoughBuffer {
            get {
                return ResourceManager.GetString("BufferChunkDoesNotHaveEnoughBuffer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BufferChunk does not have enough data to fulfill your request. Requested data:{0} Actual data:{1}.
        /// </summary>
        public static string BufferChunkDoesNotHaveEnoughData {
            get {
                return ResourceManager.GetString("BufferChunkDoesNotHaveEnoughData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DelayBetweenPackets must be in the range of 0 to 30.
        /// </summary>
        public static string DelayBetweenPacketsRange {
            get {
                return ResourceManager.GetString("DelayBetweenPacketsRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DropPacketsSentPercent must be between 0 and 100.
        /// </summary>
        public static string DropPacketsSentPercentRange {
            get {
                return ResourceManager.GetString("DropPacketsSentPercentRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Index and Length do not point to valid data index:{0} + length:{1} must be &lt;= dataLength:{2}.
        /// </summary>
        public static string IndexAndLengthInvalidData {
            get {
                return ResourceManager.GetString("IndexAndLengthInvalidData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to MulticastUdpListener already disposed.
        /// </summary>
        public static string MulticastUdpListenerAlreadyDisposed {
            get {
                return ResourceManager.GetString("MulticastUdpListenerAlreadyDisposed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Null pointers are not valid arguments..
        /// </summary>
        public static string NullPointersAreInvalid {
            get {
                return ResourceManager.GetString("NullPointersAreInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sock does not exist as a shared socket..
        /// </summary>
        public static string SockDoesNotExistAsASharedSocket {
            get {
                return ResourceManager.GetString("SockDoesNotExistAsASharedSocket", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UdpSender already disposed.
        /// </summary>
        public static string UdpSenderAlreadyDisposed {
            get {
                return ResourceManager.GetString("UdpSenderAlreadyDisposed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to find a local routing interface, is no network present?.
        /// </summary>
        public static string UnableToFindLocalRoutingInterface {
            get {
                return ResourceManager.GetString("UnableToFindLocalRoutingInterface", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to value {0}  The maximum payload for this packet is: {1}.
        /// </summary>
        public static string ValueMaximumPayload {
            get {
                return ResourceManager.GetString("ValueMaximumPayload", resourceCulture);
            }
        }
    }
}