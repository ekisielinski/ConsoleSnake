using Snake.Common.Helpers;
using System;

namespace Snake.Core
{
    public static class GameTimeUtils
    {
        public const int MaxTicksPerSecond = 1000;

        //----------------------------------------------------------------------------------------------------------------------------------

        public static long DurationToTicks (TimeSpan duration, int ticksPerSecond)
        {
            if (duration < TimeSpan.Zero) throw new ArgumentOutOfRangeException (nameof (duration));

            Verify.InRange (ticksPerSecond, 1, MaxTicksPerSecond, nameof (ticksPerSecond));

            long ticks = (long) Math.Round (duration.TotalSeconds * ticksPerSecond, MidpointRounding.AwayFromZero);

            return ticks;
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public static TimeSpan TicksToDuration (long ticks, int ticksPerSecond)
        {
            Verify.InRange (ticks, 0, long.MaxValue, nameof (ticks));

            Verify.InRange (ticksPerSecond, 1, MaxTicksPerSecond, nameof (ticksPerSecond));

            double seconds = ticks / (double) ticksPerSecond;

            var duration = TimeSpan.FromSeconds (seconds);

            return duration;
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public static TimeSpan DelayBetweenTicks (int ticksPerSecond)
        {
            Verify.InRange (ticksPerSecond, 1, MaxTicksPerSecond, nameof (ticksPerSecond));

            double delayInSeconds = 1d / ticksPerSecond;

            return TimeSpan.FromSeconds (delayInSeconds);
        }
    }
}
