namespace Snake.Hardware.Display
{
    public interface IDisplayDevice : IFPSCounter
    {
        ScreenBuffer ScreenBuffer { get; }

        void UpdateDevice ();
    }
}
