using Snake.Common.Geometry;
using Snake.Common.Helpers;
using System;
using System.Collections.Generic;

namespace Snake.Core
{
    public sealed class AppleGenerator : GameObject, IStatus
    {
        readonly Terrain terrain;

        GameTimeDelay delaySpawnNormalApple;
        GameTimeDelay delaySpawnTransientApple;

        readonly Random random = new Random ();

        //====== ctors =====================================================================================================================

        public AppleGenerator (Terrain terrain, GameTime gameTime, int maxApples = 50) : base (gameTime)
        {
            this.terrain = Verify.NotNull (terrain, nameof (terrain));

            MaxApples = Verify.InRange (maxApples, 1, 1000, nameof (maxApples));

            delaySpawnNormalApple    = gameTime.CreateDelay (TimeSpan.Zero);
            delaySpawnTransientApple = gameTime.CreateDelay (TimeSpan.FromSeconds (8));
        }

        //====== public properties =========================================================================================================

        public int MaxApples { get; private set; }

        public bool IsApplesLimitReached => CountApples >= MaxApples;

        //====== override: GameObject ======================================================================================================

        protected override void UpdateImpl ()
        {
            TrySpawnNormalApple ();
            TrySpawnTransientApple ();
        }

        //====== private properties ========================================================================================================

        private int CountApples => terrain.Count (x => !x.IsDead && x is AppleEntity);

        //====== private methods ===========================================================================================================

        private void TrySpawnNormalApple ()
        {
            if (CountApples < MaxApples && delaySpawnNormalApple.IsDone)
            {
                GenerateAppleAtRandomPosition (AppleEntity.AppleType.Normal);

                delaySpawnNormalApple = gameTime.CreateDelay (TimeSpan.FromSeconds (2));
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        private void TrySpawnTransientApple ()
        {
            if (CountApples < MaxApples && delaySpawnTransientApple.IsDone)
            {
                GenerateAppleAtRandomPosition (AppleEntity.AppleType.Transient);

                int randomDelayInSecs = random.Next (10, 30);

                delaySpawnTransientApple = gameTime.CreateDelay (TimeSpan.FromSeconds (randomDelayInSecs));
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        private void GenerateAppleAtRandomPosition (AppleEntity.AppleType type)
        {
            var position = GetFreeRandomPosition ();

            GenerateApple (type, position);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        private void GenerateApple (AppleEntity.AppleType type, Point position)
        {
            var apple = new AppleEntity (position, type, gameTime);

            terrain.Add (apple);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        private Point GetFreeRandomPosition ()
        {
            while (true) // TODO: risky!
            {
                int x = random.Next (terrain.Size.Width);
                int y = random.Next (terrain.Size.Height);


                if (terrain.GetEntityOrNull (new Point (x, y)) == null)
                {
                    return new Point (x, y);
                }
            }
        }

        //====== IStatus ===================================================================================================================

        public IReadOnlyList<StatusItem> GetStatus ()
        {
            bool appleLimitIsAlmostReached = CountApples >= (int) (MaxApples * 0.8);

            return new[] { new StatusItem ("Apples on Board", $"{CountApples}/{MaxApples}", appleLimitIsAlmostReached) };
        }
    }
}
