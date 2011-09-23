
namespace NewTOAPIA.Media.WinMM
{
    using System;

    using TOAPI.WinMM;

    /// <summary>
    /// Flags used with the PlaySound and sndPlaySound functions.
    /// </summary>
    [Flags]
    public enum PLAYSOUNDFLAGS
    {
        /// <summary>
        /// The sound is played synchronously and the function does not return until the sound ends. 
        /// </summary>
        SND_SYNC = 0x0000,

        /// <summary>
        /// The sound is played asynchronously and the function returns immediately after beginning the sound. To terminate an asynchronously played sound, call sndPlaySound with lpszSoundName set to NULL.
        /// </summary>
        SND_ASYNC = 0x0001,

        /// <summary>
        /// If the sound cannot be found, the function returns silently without playing the default sound.
        /// </summary>
        SND_NODEFAULT = 0x0002,

        /// <summary>
        /// The parameter specified by lpszSoundName points to an image of a waveform sound in memory.
        /// </summary>
        SND_MEMORY = 0x0004,

        /// <summary>
        /// The sound plays repeatedly until sndPlaySound is called again with the lpszSoundName parameter set to NULL. You must also specify the SND_ASYNC flag to loop sounds.
        /// </summary>
        SND_LOOP = 0x0008,

        /// <summary>
        /// If a sound is currently playing, the function immediately returns FALSE, without playing the requested sound.
        /// </summary>
        SND_NOSTOP = 0x0010,

        /// <summary>
        /// Sounds are to be stopped for the calling task. If pszSound is not NULL, all instances of the specified sound are stopped. If pszSound is NULL, all sounds that are playing on behalf of the calling task are stopped.  You must also specify the instance handle to stop SND_RESOURCE events.
        /// </summary>
        SND_PURGE = 0x0040,

        /// <summary>
        /// The sound is played using an application-specific association.
        /// </summary>
        SND_APPLICATION = 0x0080,

        /// <summary>
        /// If the driver is busy, return immediately without playing the sound.
        /// </summary>
        SND_NOWAIT = 0x00002000,

        /// <summary>
        /// The pszSound parameter is a system-event alias in the registry or the WIN.INI file. Do not use with either SND_FILENAME or SND_RESOURCE.
        /// </summary>
        SND_ALIAS = 0x00010000,

        /// <summary>
        /// The pszSound parameter is a filename.
        /// </summary>
        SND_FILENAME = 0x00020000,

        /// <summary>
        /// The pszSound parameter is a resource identifier; hmod must identify the instance that contains the resource.
        /// </summary>
        SND_RESOURCE = 0x00040004,

        /// <summary>
        /// The pszSound parameter is a predefined sound identifier.
        /// </summary>
        SND_ALIAS_ID = 0x00110000,
    }

    public sealed class PlaySound
    {
        private PlaySound()
        {
        }

        /// <summary>
        /// Plays a well known system sound.
        /// </summary>
        /// <param name="soundName"></param>
        public static void PlaySystemSound(string soundName)
        {
            bool succeeded = winmm.PlaySound(soundName, IntPtr.Zero, (int)(PLAYSOUNDFLAGS.SND_ALIAS | PLAYSOUNDFLAGS.SND_PURGE | PLAYSOUNDFLAGS.SND_NODEFAULT | PLAYSOUNDFLAGS.SND_ASYNC));
            if (!succeeded)
            {
                throw new MmException(0, "PlaySoundFile");
            }
        }

        public static void PlaySoundFile(string soundFile)
        {
            bool res = winmm.PlaySound(soundFile, IntPtr.Zero, (int)(PLAYSOUNDFLAGS.SND_FILENAME | PLAYSOUNDFLAGS.SND_PURGE | PLAYSOUNDFLAGS.SND_NODEFAULT | PLAYSOUNDFLAGS.SND_ASYNC));
            if (res == false)
            {
                throw new MmException(0, "PlaySoundFile");
            }
        }

        public static void StopAllSounds()
        {
            bool res = winmm.PlaySound(null, (IntPtr)0, (int)PLAYSOUNDFLAGS.SND_PURGE);
            if (res == false)
            {
                throw new MmException(0, "StopAllSounds");
            }

        }


    }
}