using Snake.Common.Helpers;
using Snake.Common.Logging;
using System;

namespace Snake.Hardware.Audio
{
    public sealed class SafeAudioDevice : IAudioDevice
    {
        readonly IAudioDevice audioDevice;
        readonly ILogger logger;

        bool exceptionOccurred = false;

        //====== ctors

        public SafeAudioDevice (IAudioDevice audioDevice, ILogger logger)
        {
            this.audioDevice = Verify.NotNull (audioDevice, nameof (audioDevice));
            this.logger      = Verify.NotNull (logger, nameof (logger));
        }

        //====== IAudioDevice

        public void Beep ()
        {
            if (exceptionOccurred) return;

            try
            {
                audioDevice.Beep ();
            }
            catch (Exception ex)
            {
                exceptionOccurred = true; // in case of error, disable this functionality forever

                logger.Log (ex);
            }
        }

        public void SpeakAsync (string message)
        {
            if (exceptionOccurred) return;

            try
            {
                audioDevice.SpeakAsync (message);
            }
            catch (Exception ex)
            {
                exceptionOccurred = true;

                logger.Log (ex);
            }
        }
    }
}
