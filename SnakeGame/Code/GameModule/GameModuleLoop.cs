using Snake.Common.Geometry;
using Snake.Common.Helpers;
using Snake.Core;
using Snake.Hardware;
using Snake.Hardware.Audio;
using Snake.Hardware.Input;
using Snake.Text;
using System;
using System.Diagnostics;

namespace Snake.Game.Core
{
    // 1) keys
    // 2) logic
    // 3) canvas
    // 4) audio

    public sealed class GameModuleLoop
    {
        readonly GameModule gameModule;
        readonly ScreenTransitionActivator transitionActivator;
        readonly HardwareAccessLayer hal;
        readonly ISleeper sleeper;

        readonly GameModuleContext context = new GameModuleContext ();

        bool showFpsIndicator = true;

        //====== ctors

        public GameModuleLoop (GameModule gameModule, ScreenTransitionActivator transitionActivator, HardwareAccessLayer hal, ISleeper sleeper)
        {
            this.gameModule          = Verify.NotNull (gameModule,          nameof (gameModule));
            this.transitionActivator = Verify.NotNull (transitionActivator, nameof (transitionActivator));
            this.hal                 = Verify.NotNull (hal,                 nameof (hal));
            this.sleeper             = Verify.NotNull (sleeper,             nameof (sleeper));
        }

        //====== public methods

        public object Start ()
        {
            gameModule.Initialize (context.GameTime);

            hal.InputDevice.Clear ();

            var canvas = new TextCanvas (hal.DisplayDevice.ScreenBuffer);

            var transition = CreateScreenTransition (canvas);

            context.EnterModule ();

            StartLoop (canvas, transition);

            context.ExitModule ();

            return context.Result;
        }

        private void StartLoop (TextCanvas canvas, ScreenTransition screenTransition)
        {
            var sw = new Stopwatch ();

            while (!context.ExitRequested)
            {
                sw.Restart ();

                ProcessInput ();
                ProcessLogic ();
                ProcessCanvas (canvas, screenTransition);
                ProcessAudio ();

                ApplyPauseBetweenFrames (sw.Elapsed);

                context.GameTimeSource.NextTick ();
            }
        }

        private void ApplyPauseBetweenFrames (TimeSpan timeSpentOnProcessing)
        {
            var maxDelay = GameTimeUtils.DelayBetweenTicks (context.GameTime.TicksPerSecond);
            var delay = maxDelay - timeSpentOnProcessing;

            if (delay > TimeSpan.Zero) sleeper.Sleep (delay);
        }

        private ScreenTransition CreateScreenTransition (TextCanvas canvas)
        {
            var canvasSnapshot = canvas.ToTextImage ();

            return transitionActivator.Invoke (canvasSnapshot, canvas, context.GameTime);
        }

        private void ProcessInput ()
        {
            var key = hal.InputDevice.Peek ();

            if (key.IsNull) return;

            gameModule.ProcessKey (hal.InputDevice);

            if (hal.InputDevice.Peek ().KeyChar == 'f') showFpsIndicator = !showFpsIndicator;
            
            hal.InputDevice.Clear (); // TODO: should it be cleared or not?
        }

        private void ProcessLogic ()
        {
            gameModule.ProcessLogic (context);
        }

        private void ProcessCanvas (TextCanvas canvas, ScreenTransition screenTransition)
        {
            gameModule.ProcessCanvas (canvas);

            var canvasSnapshot = canvas.ToTextImage ();

            screenTransition.Paint ();

            DrawFpsIndicator (canvas);

            hal.DisplayDevice.UpdateDevice ();

            canvas.DrawImage (canvasSnapshot, Point.Zero, false);

            if (screenTransition.IsDead == false) screenTransition.Update ();
        }

        private void ProcessAudio ()
        {
            var disableableAudioDevice = new DisableableAudioDevice (hal.AudioDevice);

            try
            {
                gameModule.ProcessAudio (disableableAudioDevice);
            }
            finally
            {
                disableableAudioDevice.IsDisabled = true;
            }
        }

        private void DrawFpsIndicator (TextCanvas canvas)
        {
            if (!showFpsIndicator) return;

            var position = new Point (canvas.Size.Width - 11, 0);

            canvas.WriteText (position, $" FPS: {hal.DisplayDevice.FPS} ", Color16.White, Color16.Black);
        }
    }
}
