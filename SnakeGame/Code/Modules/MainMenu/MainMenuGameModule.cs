using Snake.Common.Geometry;
using Snake.Game.Core;
using Snake.Hardware.Audio;
using Snake.Hardware.Input;
using Snake.Text;
using SnakeGame.Properties;

namespace Snake.Game.Modules
{
    public sealed class MainMenuGameModule : GameModule
    {
        OptionSelector<MainMenuEntry> optionSelector;

        bool isOptionAccepted = false;
        bool isOptionChanged  = false;
 
        //====== ctors

        public MainMenuGameModule ()
        {
            var mainMenu = CreateOptionsList ();

            optionSelector = new OptionSelector<MainMenuEntry> (mainMenu);

            optionSelector.IndexChanged += (option, index) => isOptionChanged = true;
        }

        //====== override: GameModule

        public override void ProcessKey (IInputDevice inputDevice)
        {
            var key = inputDevice.TryReadKeys (KeyType.Up, KeyType.Down, KeyType.Enter);

            if (key.KeyType == KeyType.Up)    optionSelector.SelectPrevious ();
            if (key.KeyType == KeyType.Down)  optionSelector.SelectNext ();
            if (key.KeyType == KeyType.Enter) isOptionAccepted = true;
        }

        public override void ProcessLogic (IGameModuleContext context)
        {
            if (isOptionAccepted)
            {
                MainMenuEntry entry = optionSelector.CurrentValue;

                context.Exit (entry);
            }
        }

        public override void ProcessCanvas (TextCanvas canvas)
        {
            canvas.ClearColor (Color16.Black);

            canvas.WriteTextCenter (2, Resources.TextLogo, Color16.Red, Color16.Black);

            var menuWindow = new OptionsListWindow<MainMenuEntry> (optionSelector);

            var rectCenter = menuWindow.Size.AsRectangle.CenterAt (canvas.Size.AsRectangle.Center);

            menuWindow.Paint (canvas, rectCenter.TopLeft.AddToY (2));

            // print version

            var version = Resources.SnakeGameVersion;
            var position = new Point (canvas.Size.Width - version.Length - 1, canvas.Size.Height - 1);

            canvas.WriteText (position, version, Color16.Gray, Color16.Black);
        }

        public override void ProcessAudio (IAudioDevice audioPlayer)
        {
            if (isOptionChanged)
            {
                string optionName = optionSelector.OptionsList.Options[optionSelector.CurrentOptionIndex].Name;
                audioPlayer.SpeakAsync (optionName);

                isOptionChanged = false;
            }

            if (isOptionAccepted)
            {
                audioPlayer.Beep ();

                isOptionAccepted = false;
            }
        }

        //====== private static methods

        private static OptionsList<MainMenuEntry> CreateOptionsList ()
        {
            var options = new Option<MainMenuEntry>[]
            {
                new Option<MainMenuEntry> ("Start New Game", MainMenuEntry.StartNewGame),
                new Option<MainMenuEntry> ("Hi Scores"     , MainMenuEntry.HiScores    ),
                new Option<MainMenuEntry> ("How To Play"   , MainMenuEntry.HowToPlay   ),
                new Option<MainMenuEntry> ("Credits"       , MainMenuEntry.Credits     ),
                new Option<MainMenuEntry> ("Exit Game"     , MainMenuEntry.ExitGame    ),
            };

            return new OptionsList<MainMenuEntry> ("Main Menu", options);
        }
    }
}
