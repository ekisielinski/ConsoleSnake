using Snake.Common.Helpers;
using System;

namespace Snake.Core
{
    public sealed class GameTimeDelay
    {
        readonly GameTime gameTime;

        readonly long startTick;
        readonly long endTick;

        //====== ctors

        public GameTimeDelay (GameTime gameTime, TimeSpan duration)
        {
            this.gameTime = Verify.NotNull (gameTime, nameof (gameTime));

            if (duration < TimeSpan.Zero) throw new ArgumentOutOfRangeException (nameof (duration));

            startTick = gameTime.Tick;
            endTick   = startTick + GameTimeUtils.DurationToTicks (duration, gameTime.TicksPerSecond);
        }

        //====== public properties

        public TimeSpan From           => GameTimeUtils.TicksToDuration (startTick, gameTime.TicksPerSecond);
        public TimeSpan To             => GameTimeUtils.TicksToDuration (endTick,   gameTime.TicksPerSecond);
        public TimeSpan Duration       => To - From;
        public TimeSpan Remaining      => GameTimeUtils.TicksToDuration (RemainingTicks, gameTime.TicksPerSecond);

        public long     RemainingTicks => Math.Max (0, endTick - gameTime.Tick);

        public bool     IsDone         => gameTime.Tick >= endTick;

        public int PercentageDone
        {
            get
            {
                if (IsDone) return 100;

                long durationInTicks = endTick - startTick;
                long elapsedTicks    = gameTime.Tick - startTick;

                double percentageDone = ((double) elapsedTicks / durationInTicks) * 100;

                return MiscUtils.Clamp ((int) percentageDone, 0, 100);
            }
        }

        //====== public methods

        public GameTimeDelay ReCreate () => new GameTimeDelay (gameTime, Duration);
    }
}
