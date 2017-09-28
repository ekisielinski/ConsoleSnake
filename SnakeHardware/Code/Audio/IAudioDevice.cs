namespace Snake.Hardware.Audio
{
    public interface IAudioDevice
    {
        void Beep ();

        void SpeakAsync (string message);
    }
}
