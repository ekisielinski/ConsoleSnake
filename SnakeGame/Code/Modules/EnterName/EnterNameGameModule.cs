using Snake.Game.Core;
using Snake.Hardware.Audio;
using Snake.Hardware.Input;
using Snake.Text;

namespace Snake.Game.Modules
{
    public sealed class EnterNameGameModule : GameModule
    {
        readonly UserInputString userInputString = new UserInputString (13);

        bool showCursor = true;

        ManualSetTrigger trgStartGameplay = ManualSetTrigger.NotReady;
        ManualSetTrigger trgErrorBeep     = ManualSetTrigger.NotReady;

        //====== override: GameModule ======================================================================================================

        public override void ProcessKey (IInputDevice inputDevice)
        {
            base.ProcessKey (inputDevice);

            if (userInputString.HandleKey (inputDevice) == false)
            {
                trgErrorBeep.Set ();
            }

            if (inputDevice.TryReadKey (KeyType.Enter))
            {
                if (userInputString.IsValid) trgStartGameplay.Set (); else trgErrorBeep.Set ();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public override void ProcessLogic (IGameModuleContext context)
        {
            if (HandleExitFlag (context)) return;

            showCursor = context.ModuleRunTime.Milliseconds < 500;

            if (trgStartGameplay.TryHandle ())
            {
                context.Exit (userInputString.Entered);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public override void ProcessCanvas (TextCanvas canvas)
        {
            canvas.Grayscale ();

            canvas.WriteTextCenter (23, "Enter your name: ", Color16.White, Color16.Black);

            var cursor = showCursor ? "_" : "";

            string textField = userInputString.Entered + cursor;

            canvas.WriteTextCenter (25, textField.PadRight (14, ' '), Color16.Lime, Color16.Green);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public override void ProcessAudio (IAudioDevice audioPlayer)
        {
            if (trgErrorBeep.TryHandle ())
            {
                audioPlayer.Beep ();

                trgErrorBeep = ManualSetTrigger.NotReady;
            }
        }
    }
}
