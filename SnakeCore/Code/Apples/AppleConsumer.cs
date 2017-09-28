using Snake.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Core
{
    public sealed class AppleConsumer : GameObject, IAppleConsumerStatus, IStatus
    {
        public event Action<AppleEntity> Consumed;

        //----------------------------------------------------------------------------------------------------------------------------------

        readonly Terrain terrain;
        readonly ISnakeBody snakeBody;

        Dictionary<AppleEntity.AppleType, int> consumedApplesCounters;

        //====== ctors =====================================================================================================================

        public AppleConsumer (Terrain terrain, ISnakeBody snakeBody, GameTime gameTime) : base (gameTime)
        {
            this.terrain   = Verify.NotNull (terrain, nameof (terrain));
            this.snakeBody = Verify.NotNull (snakeBody, nameof (snakeBody));

            InitializeCounters ();
        }

        //====== IApplesConsumerStatus =====================================================================================================

        public int CountConsumedApples (AppleEntity.AppleType appleType) => consumedApplesCounters[appleType];

        //====== override: GameObject ======================================================================================================

        protected override void UpdateImpl ()
        {
            if (terrain.GetEntityOrNull (snakeBody.Head ()) is AppleEntity item)
            {
                terrain.RemoveAll (x => x == item);

                consumedApplesCounters[item.Type]++;

                Consumed?.Invoke (item);
            }
        }

        //====== IStatus ===================================================================================================================

        public IReadOnlyList<StatusItem> GetStatus ()
        {
            return new[]
            {
                new StatusItem ("Consumed [N]", $"{consumedApplesCounters[AppleEntity.AppleType.Normal]}"),
                new StatusItem ("Consumed [T]", $"{consumedApplesCounters[AppleEntity.AppleType.Transient]}")
            };
        }

        //====== private methods ===========================================================================================================

        private void InitializeCounters ()
        {
            consumedApplesCounters = new Dictionary<AppleEntity.AppleType, int> ();

            var appleTypes = Enum.GetValues (typeof (AppleEntity.AppleType)).Cast<AppleEntity.AppleType> ().ToList ();

            appleTypes.ForEach (x => consumedApplesCounters.Add (x, 0));
        }
    }
}
