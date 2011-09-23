

namespace NewTOAPIA.Media.WinMM
{
    using System;

    using TOAPI.Types;
    using TOAPI.WinMM;
    
    public interface WinMMAudioDevice
    {


        //#region Fields
        //protected WaveCallback fCallbackMethod;
        //protected int fDeviceID;
        //protected IntPtr fDeviceHandle = new IntPtr(0);
        //protected WAVEFORMATEX fWaveFormat;
        //protected PCMAudioFormat fPCMFormat;
        //#endregion

        //public WinMMAudioDevice(int deviceID, int channels, int sampleRate, int bitsPerSample)
        //    :this(deviceID, WAVEFORMATEX.CreatePCMFormat(channels, sampleRate, bitsPerSample))
        //{
        //}

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
    }
}
