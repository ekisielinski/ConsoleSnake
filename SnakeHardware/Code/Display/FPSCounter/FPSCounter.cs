using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Snake.Hardware.Display
{
    // TODO: returns slightly incorrect values...

    public sealed class FPSCounter : IFPSCounter
    {
        readonly TimeSpan updateDelay;

        readonly Stopwatch stopwatch = new Stopwatch ();
        readonly Queue<DateTime> timestamps = new Queue<DateTime> ();

        DateTime? lastTimestamp = null;

        //====== ctors =====================================================================================================================

        public FPSCounter (TimeSpan? updateDelay = null)
        {
            this.updateDelay = updateDelay ?? TimeSpan.FromSeconds (0.5);
        }

        //====== IFPSCounter ===============================================================================================================

        public int FPS { get; private set; }

        //====== public methods ============================================================================================================

        public void StartMeasuring () => stopwatch.Start ();

        //----------------------------------------------------------------------------------------------------------------------------------

        public void StopMeasuring ()
        {
            stopwatch.Reset ();

            timestamps.Clear ();
            lastTimestamp = null;

            FPS = 0;
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public void Update ()
        {
            if (stopwatch.IsRunning == false)
            {
                throw new InvalidOperationException ("FPS Counter is disabled.");
            }

            lastTimestamp = DateTime.Now;
            timestamps.Enqueue (lastTimestamp.Value);

            var windowSize = TimeSpan.FromSeconds (updateDelay.TotalSeconds * 2);

            if (GetQueueDuration () >= windowSize)
            {
                FPS = (int) (timestamps.Count / windowSize.TotalSeconds);

                ReduceQueueDuration ();
            }
        }

        //====== private methods ===========================================================================================================

        private TimeSpan GetQueueDuration ()
        {
            if (timestamps.Count <= 1) return TimeSpan.Zero;

            return lastTimestamp.Value - timestamps.Peek ();
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        private void ReduceQueueDuration ()
        {
            while (GetQueueDuration () > updateDelay)
            {
                timestamps.Dequeue ();
            }
        }
    }
}
