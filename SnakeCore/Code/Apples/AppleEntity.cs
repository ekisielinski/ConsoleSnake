using Snake.Common.Geometry;
using Snake.Common.Helpers;
using System;

namespace Snake.Core
{
    public sealed class AppleEntity : TerrainEntity
    {
        const int LifetimeInSeconds    = 20;
        const int ScoreForNormal       = 8;
        const int MaxScoreForTransient = 60;

        public enum AppleType
        {
            Normal,
            Transient // gives extra score, but will disappear after few seconds
        }

        readonly GameTime gameTime;

        //====== ctors

        public AppleEntity (Point position, AppleType appleType, GameTime gameTime) : base (position)
        {
            Type = appleType;

            this.gameTime = Verify.NotNull (gameTime, nameof (gameTime));

            DelayBeforeDie = gameTime.CreateDelay (TimeSpan.FromSeconds (LifetimeInSeconds));
        }

        //====== override: TerrainEntity ===================================================================================================

        public override bool IsDead => Type == AppleType.Transient && DelayBeforeDie.IsDone;

        //====== public properties

        public AppleType Type { get; }

        public GameTimeDelay DelayBeforeDie { get; }

        public bool IsDying => Type == AppleType.Transient && DelayBeforeDie.Remaining.TotalSeconds <= 5;

        public int Score
        {
            get
            {
                if (Type == AppleType.Normal) return ScoreForNormal;

                int remainingSecs = (int) DelayBeforeDie.Remaining.TotalSeconds;

                if (remainingSecs > 10) return MaxScoreForTransient;

                int scoreLoss = 4 * (10 - remainingSecs);

                int score = MaxScoreForTransient - scoreLoss;

                return score;
            }
        }
    }
}
