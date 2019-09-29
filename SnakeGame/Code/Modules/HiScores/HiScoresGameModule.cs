using Snake.Common.Helpers;
using Snake.Core;
using Snake.Game.Core;
using Snake.Game.Drawing;
using Snake.Text;
using SnakeGame.Properties;
using System;

namespace Snake.Game.Modules
{
    public sealed class HiScoreGameModule : GameModule
    {
        readonly IHiScores hiScores;

        Animation<Color16> hiScoresAnimation;

        //====== ctors

        public HiScoreGameModule (IHiScores hiScores)
        {
            this.hiScores = Verify.NotNull (hiScores, nameof (hiScores));
        }

        //====== override: GameModule

        public override void Initialize (GameTime gameTime)
        {
            var frames = new AnimationFrames<Color16>
                    (GameAnimations.BlackToWhiteTransition, FramesArrangement.AfterLastFrameTravelBackToSecondFrame);

            hiScoresAnimation = new Animation<Color16> (frames, gameTime, TimeSpan.FromMilliseconds (100));
            hiScoresAnimation.Start ();
        }

        public override void ProcessLogic (IGameModuleContext context)
        {
            hiScoresAnimation.Update ();

            HandleExitFlag (context);
        }

        public override void ProcessCanvas (TextCanvas canvas)
        {
            canvas.ClearColor (Color16.Black);

            canvas.WriteTextCenter (1, Resources.TextHiScores, hiScoresAnimation.CurrentFrame, Color16.Black);

            var appearance = new WindowAppearance (Color16.Black, Color16.Lime, Color16.Yellow);
            var hiScoresWindow = new HiScoresWindow (hiScores, appearance);

            var winPosition = hiScoresWindow.Size.AsRectangle.CenterAt (canvas.Size.AsRectangle.Center).TopLeft;

            hiScoresWindow.Paint (canvas, winPosition);

            canvas.WriteTextCenter (canvas.Size.Height - 2, "-- Presc ESC to back --", Color16.Silver, Color16.Black);
        }
    }
}
