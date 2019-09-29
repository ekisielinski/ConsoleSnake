using Snake.Common.Geometry;
using Snake.Common.Helpers;
using Snake.Core;
using Snake.Game.Core;
using Snake.Game.Drawing;
using Snake.Hardware.Audio;
using Snake.Hardware.Input;
using Snake.Text;
using SnakeGame.Core;
using SnakeGame.Modules;
using SnakeGame.Modules.Gameplay;

namespace Snake.Game.Modules
{
    public sealed class GameplayGameModule : GameModule
    {
        readonly string playerName;

        GameLogic         gameLogic;
        TerrainRenderer   terrainRenderer;
        SnakeKeyHandler   keyHandler;
        BigDigitsScoreUI  bigDigitsScoreUI;
        ScoreTransitionUI scoreTransitionUI;

        ManualSetTrigger  trgAppleConsumeSound = ManualSetTrigger.NotReady;

        //====== ctors

        public GameplayGameModule (string playerName)
        { 
            this.playerName = Verify.NotNull (playerName, nameof (playerName));
        }

        //====== override: GameModule

        public override void Initialize (GameTime gameTime)
        {
            gameLogic = new GameLogic (new Size (100 - 30 - 3, 30 - 2), gameTime);
            gameLogic.AppleConsumed += () => trgAppleConsumeSound.Set ();

            terrainRenderer = new TerrainRenderer ();
            terrainRenderer.Register<AppleEntity> (new AppleLookProvider ());
            terrainRenderer.Register<SnakeBodyPartEntity> (new SnakeBodyPartLookProvider ());

            scoreTransitionUI = new ScoreTransitionUI (gameLogic.ScoreStatus, gameTime);
            bigDigitsScoreUI  = new BigDigitsScoreUI (scoreTransitionUI);
        }

        public override void ProcessKey (IInputDevice inputDevice)
        {
            if (keyHandler == null)
            {
                keyHandler = new SnakeKeyHandler (inputDevice, gameLogic.DirectionChanger);
            }

            keyHandler.ReceiveKeys ();
        }

        public override void ProcessLogic (IGameModuleContext context)
        {
            if (gameLogic.GameOverJustification.IsGameOver)
            {
                var info = new GameOverInfo (playerName, gameLogic.ScoreStatus.Value, gameLogic.GameOverJustification.Reason);

                context.Exit (info);
                return;
            }

            gameLogic.Update ();
            scoreTransitionUI.Update ();
        }

        public override void ProcessCanvas (TextCanvas canvas)
        {
            canvas.ClearColor (Color16.Black);

            terrainRenderer.Paint (gameLogic.TerrainReader, canvas, new Point (31, 0));

            PaintStatsWindow (canvas);

            var scoreView = canvas.Slice (new Rectangle (new Point (1, 21), new Size (28, 76)));


            scoreView.WriteTextCenter (0, new string ('─', scoreView.Size.Width), Color16.Teal, Color16.Black);
            scoreView.WriteTextCenter (0, " SCORE ", Color16.Yellow, Color16.Black);

            bigDigitsScoreUI.DrawCenter (scoreView, 2);
        }

        public override void ProcessAudio (IAudioDevice audioPlayer)
        {
            if (trgAppleConsumeSound.TryHandle ())
            {
                audioPlayer.Beep ();
                trgAppleConsumeSound = ManualSetTrigger.NotReady;
            }
        }

        //====== private methods

        private void PaintStatsWindow (TextCanvas canvas)
        {
            var statsView = canvas.Slice (canvas.Size.AsRectangle.ChangeWidth (30));

            statsView.ClearColor (Color16.DarkBlue);

            var app = new WindowAppearance (Color16.Black, Color16.Teal, Color16.Lime, true);

            var statsWindow = new StatsWindow (playerName, gameLogic.GetStatus (), statsView.Size,app );

            statsWindow.Paint (statsView, Point.Zero);
        }
    }
}
