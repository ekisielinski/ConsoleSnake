namespace Snake.Hardware.Audio
{
    public sealed class NullAudioDevice : IAudioDevice
    {
        public void Beep () { }

        public void SpeakAsync (string message) { }
    }
}
