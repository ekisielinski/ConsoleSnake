using Snake.Common.Geometry;
using Snake.Common.Helpers;
using Snake.Core;
using Snake.Hardware.Audio;
using Snake.Hardware.Input;
using Snake.Text;

namespace Snake.Game.Core
{
    public abstract class GameModule
    {
        bool exit = false;

        //====== public virtual methods -- lifecycle

        public virtual void Initialize (GameTime gameTime) { } // TODO: can be called second time when re-entering module, need fix

        //====== public virtual methods -- flow

        // 1)

        public virtual void ProcessKey (IInputDevice inputDevice)
        {
            SetExitFlagIfCancelKeyPressed (inputDevice);
        }

        // 2)

        public virtual void ProcessLogic (IGameModuleContext context)
        {
            HandleExitFlag (context);
        }

        // 3)

        public virtual void ProcessCanvas (TextCanvas canvas)
        {
            var msg = $"Please implement {nameof (ProcessCanvas)} method in {GetType ().Name} class.";

            canvas.WriteText (Point.Zero, msg, Color16.White, Color16.Red);
        }

        // 4)

        public virtual void ProcessAudio (IAudioDevice audioPlayer) { }

        //====== protected methods

        protected bool SetExitFlagIfCancelKeyPressed (IInputDevice inputDevice)
        {
            Verify.NotNull (inputDevice, nameof (inputDevice));

            if (inputDevice.TryReadKey (KeyType.Cancel))
            {
                exit = true;
                inputDevice.Clear ();
            }

            return exit;
        }

        protected bool HandleExitFlag (IGameModuleContext context)
        {
            Verify.NotNull (context, nameof (context));

            if (exit) context.Exit (null);

            return exit;
        }
    }
}
