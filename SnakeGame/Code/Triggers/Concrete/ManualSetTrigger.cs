namespace Snake.Game
{
    public sealed class ManualSetTrigger : Trigger
    {
        bool isReadyToFire;

        //====== ctors

        private ManualSetTrigger (bool isReadyToFire)
        {
            this.isReadyToFire = isReadyToFire;
        }

        //====== public methods

        public void Set () => isReadyToFire = true;

        //====== override: Trigger

        protected override bool Condition => isReadyToFire;

        protected override void AfterHandle () => isReadyToFire = false;

        //====== public static properties

        public static ManualSetTrigger NotReady => new ManualSetTrigger (false);
        public static ManualSetTrigger Ready    => new ManualSetTrigger (true);
    }
}
