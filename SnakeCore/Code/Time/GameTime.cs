using Snake.Common.Helpers;
using System;

namespace Snake.Core
{
    public sealed class GameTime
    {
        readonly IGameTimeSource gameTimeSource;

        //====== ctors

        public GameTime (IGameTimeSource gameTimeSource)
        {
            this.gameTimeSource = Verify.NotNull (gameTimeSource, nameof (gameTimeSource));
        }

        //====== public properties

        public long Tick           => gameTimeSource.Tick;
        public int  TicksPerSecond => gameTimeSource.TicksPerSecond;

        public TimeSpan Elapsed => GameTimeUtils.TicksToDuration (Tick, TicksPerSecond);

        //====== public methods

        public GameTimeDelay CreateDelay (TimeSpan duration) => new GameTimeDelay (this, duration);

        //====== override: Object

        public override string ToString () => $"Tick: {Tick}, Elapsed: {Elapsed}";
    }
}
