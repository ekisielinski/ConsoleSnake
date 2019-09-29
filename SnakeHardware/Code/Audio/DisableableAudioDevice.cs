using Snake.Common.Helpers;
using System;

namespace Snake.Hardware.Audio
{
    public sealed class DisableableAudioDevice : IAudioDevice
    {
        readonly IAudioDevice audioDevice;

        //====== ctors

        public DisableableAudioDevice (IAudioDevice audioDevice)
        {
            this.audioDevice = Verify.NotNull (audioDevice, nameof (audioDevice));
        }

        //====== public properties

        public bool IsDisabled { get; set; } = false;

        //====== IAudioDevice

        public void Beep ()
        {
            ThrowIfDisabled ();

            audioDevice.Beep ();
        }

        public void SpeakAsync (string message)
        {
            ThrowIfDisabled ();

            audioDevice.SpeakAsync (message);
        }

        //====== private methods

        private void ThrowIfDisabled ()
        {
            if (IsDisabled) throw new InvalidOperationException ("Disabled audio device cannot be used.");
        }
    }
}
