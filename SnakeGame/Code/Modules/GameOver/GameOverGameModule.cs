using Snake.Common.Geometry;
using Snake.Common.Helpers;
using Snake.Core;
using Snake.Game.Core;
using Snake.Game.Drawing;
using Snake.Text;

namespace Snake.Game.Modules
{
    public sealed class GameOverGameModule : GameModule
    {
        readonly GameOverInfo gameOverInfo;
        readonly int? placement;

        ITextArrayReader placementImage;

        //====== ctors =====================================================================================================================

        public GameOverGameModule (GameOverInfo gameOverInfo, int? placement)
        {
            this.gameOverInfo = Verify.NotNull (gameOverInfo, nameof (gameOverInfo));
            this.placement    = placement;
        }
    
        //====== override: GameModule ======================================================================================================

        public override void Initialize (GameTime gameTime)
        {
            if (placement.HasValue)
            {
                placementImage = new BigDigitsValueRenderer (placement.Value + 1, Color16.Lime).RenderImage ();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public override void ProcessCanvas (TextCanvas canvas)
        {
            canvas.ClearColor (Color16.Black);

            canvas.WriteTextCenter (2, "                       ", Color16.White, Color16.DarkRed);
            canvas.WriteTextCenter (3, "   <<< GAME OVER >>>   ", Color16.White, Color16.DarkRed);
            canvas.WriteTextCenter (4, "                       ", Color16.White, Color16.DarkRed);

            canvas.WriteTextCenter (7, $"{gameOverInfo.Reason}", Color16.Yellow, Color16.Black);

            canvas.WriteTextCenter (10, $"Player: {gameOverInfo.PlayerName}", Color16.Lime, Color16.Black);
            canvas.WriteTextCenter (12, $"SCORE: {gameOverInfo.Score}", Color16.LightTeal, Color16.Black);

            if (placement.HasValue)
            {
                canvas.WriteTextCenter (15, $"Congratulations!", Color16.White, Color16.Black);
                canvas.WriteTextCenter (16, $"You have earned enough points to be ranked!", Color16.Silver, Color16.Black);

                int x = placementImage.Size.AsRectangle.CenterAt (canvas.Size.AsRectangle.Center).TopLeft.X;
                int y = canvas.Size.Height - placementImage.Size.Height - 5;

                canvas.DrawImage (placementImage, new Point (x, y));
            }

            canvas.WriteTextCenter (canvas.Size.Height - 2, "-- Presc ESC to back to main menu --", Color16.Silver, Color16.Black);
        }
    }
}
