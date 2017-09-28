using Snake.Common.Helpers;
using System;

namespace Snake.Game.Core
{
    public sealed class GameModuleNavigator  
    {
        bool isDisabled = false;

        //====== public properties =========================================================================================================

        public GameModuleActivator Activator { get; private set; } = null;

        public bool ExitRequested { get; private set; }

        //====== IGameModuleNavigator ======================================================================================================

        public void GoToModule (GameModuleActivator activator)
        {
            ThrowIfDisabled ();

            Activator = Verify.NotNull (activator, nameof (activator));
            ExitRequested = true;
        }

        //====== public methods ============================================================================================================

        public void Disable ()
        {
            ThrowIfDisabled (); // Disable() can be called only once
            isDisabled = true;
        }

        //====== private methods ===========================================================================================================

        private void ThrowIfDisabled ()
        {
            if (isDisabled) throw new InvalidOperationException ("Navigator is disabled.");
        }
    }
}
