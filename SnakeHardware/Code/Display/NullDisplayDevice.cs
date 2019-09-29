using Snake.Common.Geometry;

namespace Snake.Hardware.Display
{
    public sealed class NullDisplayDevice : IDisplayDevice, IFPSCounter
    {
        public NullDisplayDevice (Size screenSize)
        {
            ScreenBuffer = new ScreenBuffer (screenSize);
        }

        //====== IFPSCounter

        public int FPS => 0;

        //====== IDisplayDevice

        public ScreenBuffer ScreenBuffer { get; }

        public void UpdateDevice () { /* nothing to do here... */ }
    }
}
