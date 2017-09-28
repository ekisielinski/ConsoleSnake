using Snake.Common.Helpers;
using Snake.Common.Logging;
using Snake.Game.Core;
using Snake.Hardware;

namespace Snake.Game
{
    public sealed class GameRoot
    {
        readonly HardwareAccessLayer hal;
        readonly ISleeper sleeper;
        readonly ILogger logger;

        //====== ctors =====================================================================================================================

        public GameRoot (HardwareAccessLayer hal, ISleeper sleeper, ILogger logger)
        {
            this.hal     = Verify.NotNull (hal,     nameof (hal));
            this.sleeper = Verify.NotNull (sleeper, nameof (sleeper));
            this.logger  = Verify.NotNull (logger,  nameof (logger));
        }

        //====== public methods ============================================================================================================

        public void StartGame (IGraphNode mainNode)
        {
            logger.Log ($"Starting game.", false);

            IGraphNode node = mainNode;

            ScreenTransitionActivator nextTransition = null;

            do
            {
                var module = node.GetInstance ();
                
                logger.Log ("Going to module: " + module.GetType ().Name, false);

                var loop = new GameModuleLoop (module, nextTransition ?? NullTransition.Activator, hal, sleeper);

                var result = loop.Start ();

                (node, nextTransition) = node.GetNextNode (result);
            }
            while (node != null);

            logger.Log ($"Exiting game.", false);
        }
    }
}
