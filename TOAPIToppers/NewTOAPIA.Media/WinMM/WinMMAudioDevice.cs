using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.Types;
using TOAPI.WinMM;
using TOAPI.Kernel32;

namespace NewTOAPIA.Media.WinMM
{
    abstract public class WinMMAudioDevice
    {
        public delegate void AudioDeviceNotificationProc(WinMMAudioDevice aDevice);
        public delegate void AudioDeviceBufferReceivedProc(WinMMAudioDevice aDevice, IntPtr bufferPtr, IntPtr param2);


        #region Fields
        protected WaveCallback fCallbackMethod;
        protected int fDeviceID;
        protected IntPtr fDeviceHandle;
        protected WAVEFORMATEX fWaveFormat;
        protected PCMAudioFormat fPCMFormat;
        #endregion

        public WinMMAudioDevice(int deviceID, int channels, int sampleRate, int bitsPerSample)
            :this(deviceID, WAVEFORMATEX.CreatePCMFormat(channels, sampleRate, bitsPerSample))
        {
        }

        public WinMMAudioDevice(int deviceID, WAVEFORMATEX wf)
        {
            fCallbackMethod = new WaveCallback(this.DefaultAudioCallback);

            fDeviceHandle = IntPtr.Zero;
            fDeviceID = deviceID;
            fWaveFormat = wf;
            fPCMFormat = PCMAudioFormat.CreateFromWaveFormat(wf);

            //if (callbackProc != null)
            //    fCallbackProcedure = callbackProc;
            //else
            //    fCallbackProcedure = DefaultAudioCallback;
        }

        //public WaveAudioProc CallbackProcedure
        //{
        //    get { return fCallbackProcedure; }
        //    set { fCallbackProcedure = value; }
        //}

        public IntPtr DeviceHandle
        {
            get { return fDeviceHandle; }
            protected set { fDeviceHandle = value; }
        }

        //public uint DeviceID
        //{
        //    get
        //    {
        //        return fDeviceID;
        //        //uint deviceID = winmm.WAVE_MAPPER;
        //        //uint result = (uint)winmm.waveOutGetID(DeviceHandle, out deviceID);
                
        //        //return result;
        //    }
        //}

        public PCMAudioFormat PCMFormat
        {
            get { return fPCMFormat; }
        }

        public WAVEFORMATEX WaveFormat
        {
            get { return fWaveFormat; }
        }

        abstract public void Open();

        abstract public void Close();

        abstract public void Reset();

        #region Callback routines
        public virtual void DeviceOpened(IntPtr devHandle)
        {
        }

        public virtual void DeviceClosed(IntPtr devHandle)
        {
        }

        public virtual void OnInputData(IntPtr wavhdr, IntPtr param2)
        {

        }

        public virtual void OutputDone(IntPtr wavhdr, IntPtr param2)
        {
        }

        protected virtual void DefaultAudioCallback(IntPtr deviceHandle, int uMsg, IntPtr userData, IntPtr wavhdr, IntPtr reserved)
        {
            switch ((WaveCallbackMsg)uMsg)
            {
                case WaveCallbackMsg.InputOpened:
                    DeviceOpened(deviceHandle);
                    break;

                case WaveCallbackMsg.InputClosed:   // Port has been opened
                    DeviceClosed(deviceHandle);
                    break;

                case WaveCallbackMsg.InputData:
                    OnInputData(wavhdr, reserved);
                    break;

                
                
                
                case WaveCallbackMsg.OutputOpened:
                    DeviceOpened(deviceHandle);
                    break;

                case WaveCallbackMsg.OutputClosed:
                    DeviceClosed(deviceHandle);
                    break;

                case WaveCallbackMsg.OutputDone:
                    OutputDone(wavhdr, reserved);
                    break;
            }
        }
        #endregion
    }
}
