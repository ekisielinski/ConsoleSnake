using System;
using System.Collections.Generic;

namespace Snake.Core
{
    public sealed class SnakeSpeed : IStatus
    {
        readonly TimeSpan minDelay  = TimeSpan.FromMilliseconds (25);
        readonly TimeSpan deltaStep = TimeSpan.FromMilliseconds (5);

        TimeSpan currentDelay = TimeSpan.FromMilliseconds (200);

        //====== ctors =====================================================================================================================

        public SnakeSpeed () { /* for default values */ }

        //----------------------------------------------------------------------------------------------------------------------------------

        public SnakeSpeed (TimeSpan delayBetweenMoves, TimeSpan minDelayBetweenMoves, TimeSpan deltaStep)
        {
            currentDelay = delayBetweenMoves;
            minDelay     = minDelayBetweenMoves;

            if (currentDelay < minDelay) throw new ArgumentException ("Invalid delays.");

            if (deltaStep <= TimeSpan.Zero) throw new ArgumentOutOfRangeException ($"Invalid {nameof (deltaStep)}.");
        }

        //====== public properties =========================================================================================================

        public TimeSpan CurrentDelay => currentDelay;

        public bool HasMaxSpeed => currentDelay <= minDelay;

        //====== public methods ============================================================================================================

        public void Increase ()
        {
            var newDelay = currentDelay - deltaStep;

            currentDelay = (newDelay < minDelay) ? minDelay : newDelay;
        }

        //====== IStatus ===================================================================================================================

        public IReadOnlyList<StatusItem> GetStatus ()
        {
            return new[] { new StatusItem ("Speed (delay)", $"{currentDelay.TotalMilliseconds} ms") };
        }
    }
}
