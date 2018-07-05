using Snake.Core;
using Snake.Game.Core;
using Snake.Text;
using SnakeGame.Properties;
using System;

namespace Snake.Game.Modules
{
    public sealed class CreditsGameModule : GameModule
    {
        Animation<Color16> colorAnimation;

        //====== override: GameModule ======================================================================================================

        public override void Initialize (GameTime gameTime)
        {
            var frames = new AnimationFrames<Color16>
                    (GameAnimations.BlueToWhiteTransition, FramesArrangement.AfterLastFrameTravelBackToSecondFrame);

            colorAnimation = new Animation<Color16> (frames, gameTime, TimeSpan.FromMilliseconds (100));
            colorAnimation.Start ();
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public override void ProcessLogic (IGameModuleContext context)
        {
            base.ProcessLogic (context);

            colorAnimation.Update ();
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public override void ProcessCanvas (TextCanvas canvas)
        {
            canvas.ClearColor (Color16.Black);

            canvas.WriteTextCenter (2, Resources.TextCredits, colorAnimation.CurrentFrame, Color16.Black);

            canvas.WriteTextCenter (10, "Special thanks to:", Color16.LightTeal, Color16.Black);
            canvas.WriteTextCenter (12, "me :)", Color16.Yellow, Color16.Black);

            canvas.WriteTextCenter (15, "2017-2018", Color16.Silver, Color16.Black);

            canvas.WriteTextCenter (18, "Created by: Eryk Kisieliński", Color16.Gray, Color16.Black);

            canvas.WriteTextCenter (canvas.Size.Height - 2, "-- Presc ESC to back --", Color16.Silver, Color16.Black);
        }
    }
}
