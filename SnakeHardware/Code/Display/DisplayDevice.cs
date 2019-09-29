using Snake.Common.Geometry;
using System;

namespace Snake.Hardware.Display
{
    public abstract class DisplayDevice : IDisplayDevice, IFPSCounter
    {
        bool isInitialized = false;
        long lastUpdateGeneration;

        readonly FPSCounter fpsCounter = new FPSCounter ();

        //====== ctors

        protected DisplayDevice (Size screenSize)
        {
            ScreenBuffer = new ScreenBuffer (screenSize);

            lastUpdateGeneration = ScreenBuffer.Generation - 1; // dirty
        }

        //====== IFPSCounter

        public int FPS => fpsCounter.FPS;

        //====== IDisplayDevice

        public ScreenBuffer ScreenBuffer { get; private set; }

        public void UpdateDevice ()
        {
            ThrowIfNotInitialized ();

            fpsCounter.Update ();

            if (lastUpdateGeneration != ScreenBuffer.Generation)
            {
                UpdateDeviceImpl ();
                lastUpdateGeneration = ScreenBuffer.Generation;
            }
        }

        //====== protected abstract methods

        protected abstract void InitializeDevice ();
        protected abstract void UpdateDeviceImpl ();

        //====== public methods

        public void Initialize ()
        {
            if (isInitialized) throw new InvalidOperationException ("Device is already initialized.");

            InitializeDevice ();

            isInitialized = true;

            fpsCounter.StartMeasuring ();
        }

        //====== private methods

        private void ThrowIfNotInitialized ()
        {
            if (isInitialized == false) throw new InvalidOperationException ("Device must be initialized first.");
        }
    }
}
