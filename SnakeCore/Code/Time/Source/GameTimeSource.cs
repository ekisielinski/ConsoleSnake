using Snake.Common.Helpers;

namespace Snake.Core
{
    public sealed class GameTimeSource : IGameTimeSource
    {
        public GameTimeSource (int ticksPerSecond)
        {
            TicksPerSecond = Verify.InRange (ticksPerSecond, 1, 1000, nameof (ticksPerSecond));

            GameTime = new GameTime (this);
        }

        //====== IGameTimeSource ===========================================================================================================

        public long Tick { get; private set; } = 0;

        public int TicksPerSecond { get; }

        //====== public properties =========================================================================================================

        public GameTime GameTime { get; }

        //====== public methods ============================================================================================================

        public void NextTick () => Tick++;
    }
}
