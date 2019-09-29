using Snake.Common.Helpers;
using System;

namespace Snake.Core
{
    public abstract class GameObject : IUpdateable
    {
        protected readonly GameTime gameTime;

        long? lastTick = null;

        //====== ctors

        protected GameObject (GameTime gameTime)
        {
            this.gameTime = Verify.NotNull (gameTime, nameof (gameTime));
        }

        //====== public properties

        public bool IsDead { get; protected set; }

        //====== abstract

        protected abstract void UpdateImpl ();

        //====== public methods

        public void Update ()
        {
            if (IsDead) throw new InvalidOperationException ("Cannot update 'dead' game object.");

            long gameTick = gameTime.Tick;

            if (lastTick == null) lastTick = gameTick - 1;

            if (lastTick != gameTick - 1)
            {
                throw new InvalidOperationException ("Game Time must advance to next tick before call to Update() method again.");
            }

            lastTick = gameTick;

            UpdateImpl ();
        }
    }
}
