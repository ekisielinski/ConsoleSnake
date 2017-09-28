using Snake.Common.Helpers;
using Snake.Game.Modules;

namespace Snake.Game
{
    public static class Graph
    {
        public static IGraphNode CreateGraph (IHiScoresReader hiScoresReader, IHiScoresUpdater hiScoresUpdater)
        {
            Verify.NotNull (hiScoresReader, nameof (hiScoresReader));
            Verify.NotNull (hiScoresReader, nameof (hiScoresReader));

            var t1 = TransitionT1.Activator;
            var t2 = TransitionT2.Activator;

            var mainMenuModule = new MainMenuGameModule ();

            var nodeMainMenu  = new GraphNode<MainMenuEntry> (() => mainMenuModule);
            var nodeHiScores  = new GraphNode<object> (() => new HiScoreGameModule (hiScoresReader.GetHiScores ()));
            var nodeHowToPlay = new GraphNode<object> (() => new HowToPlayGameModule ());
            var nodeCredits   = new GraphNode<object> (() => new CreditsGameModule ());
            var nodeExit      = new GraphNode<object> (() => new ExitGameModule ());
            
            var nodeEnterName = new GraphNode<string> (() => new EnterNameGameModule ());

            //------

            nodeMainMenu.SetNext (entry =>
            {
                switch (entry)
                {
                    case MainMenuEntry.StartNewGame : return (nodeEnterName, null);
                    case MainMenuEntry.HiScores     : return (nodeHiScores,  t2);
                    case MainMenuEntry.HowToPlay    : return (nodeHowToPlay, t2);
                    case MainMenuEntry.Credits      : return (nodeCredits,   t2);
                    case MainMenuEntry.ExitGame     : return (nodeExit,      null);

                    default: return (null, null);
                }
            });

            //------

            nodeHiScores .SetNext (_ => (nodeMainMenu, t2)); // go back to main menu
            nodeHowToPlay.SetNext (_ => (nodeMainMenu, t2));
            nodeCredits  .SetNext (_ => (nodeMainMenu, t2));

            //------

            nodeEnterName.SetNext (name =>
            {
                if (name == null) return (nodeMainMenu, null); // canceled

                var nodeGameplay = new GraphNode<GameOverInfo> (() => new GameplayGameModule (name));

                nodeGameplay.SetNext (gameOverInfo =>
                {
                    int? placement = hiScoresUpdater.RegisterNewResult (gameOverInfo.PlayerName, gameOverInfo.Score);

                    var nodeGameOver = new GraphNode<object> (() => new GameOverGameModule (gameOverInfo, placement));

                    nodeGameOver.SetNext (_ => (nodeMainMenu, t2));

                    return (nodeGameOver, t1);
                });
                
                return (nodeGameplay, t1);
            });

            return nodeMainMenu; // start game in main menu
        }
    }
}
