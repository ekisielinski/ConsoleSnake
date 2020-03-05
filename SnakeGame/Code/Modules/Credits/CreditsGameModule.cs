using Snake.Core;
using Snake.Game.Core;
using Snake.Text;
using SnakeGame.Properties;
using System;

using static Snake.Text.Color16;

namespace Snake.Game.Modules
{
    public sealed class CreditsGameModule : GameModule
    {
        Animation<Color16> colorAnimation;

        //====== override: GameModule

        public override void Initialize (GameTime gameTime)
        {
            var frames = new AnimationFrames<Color16>
                    (GameAnimations.BlueToWhiteTransition, FramesArrangement.AfterLastFrameTravelBackToSecondFrame);

            colorAnimation = new Animation<Color16> (frames, gameTime, TimeSpan.FromMilliseconds (100));
            colorAnimation.Start ();
        }

        public override void ProcessLogic (IGameModuleContext context)
        {
            base.ProcessLogic (context);

            colorAnimation.Update ();
        }

        public override void ProcessCanvas (TextCanvas canvas)
        {
            canvas.ClearColor (Black);

            canvas.WriteTextCenter (2, Resources.TextCredits, colorAnimation.CurrentFrame, Black);

            canvas.WriteTextCenter (10, "Special thanks to:", LightTeal, Black);
            canvas.WriteTextCenter (12, "me :)", Yellow, Black);

            canvas.WriteTextCenter (15, "2017-2020", Silver, Black);

            canvas.WriteTextCenter (18, "Created by: Eryk Kisieliński", Gray, Black);

            canvas.WriteTextCenter (canvas.Size.Height - 2, "-- Presc ESC to back --", Silver, Black);
        }
    }
}
