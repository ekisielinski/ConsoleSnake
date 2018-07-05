using Snake.Common.Geometry;
using Snake.Game;
using Snake.Shared;
using SnakeConsoleUI.Hardware;

namespace SnakeConsoleUI
{
    class Program
    {
        static void Main (string[] args)
        {
            var logger = new FileLogger ("gamelog.txt");
            logger.DeleteLogFile ();

            //------

            logger.Log ("Initializing devices...", false);

            var inputDevice = new ConsoleInputDevice ();

            var displayDevice = new ConsoleDisplayDevice (new Size (100, 30), "(( Snake ))");
            displayDevice.Initialize ();

            var audioDevice = new StandardAudioDevice ();

            //------

            var xmlSerializer   = new HiScoresXmlSerializer ();
            var hiScoresStorage = new HiScoresFileStorage ("hiscores.dat", xmlSerializer, logger);

            //------

            var gameRoot = PureDiBoostrapper.CreateGameRoot (inputDevice, displayDevice, audioDevice, hiScoresStorage, logger);

            var hiScoresManager = new HiScoresManager (hiScoresStorage);
            var startNode = Graph.CreateGraph (hiScoresManager, hiScoresManager);

            gameRoot.StartGame (startNode);

            audioDevice.Dispose ();
        }
    }
}
