using Snake.Core;
using System;
using System.Diagnostics;

namespace Snake.Game.Core
{
    public sealed class GameModuleContext : IGameModuleContext
    {
        readonly Stopwatch runTime = new Stopwatch ();

        //====== IGameModuleContext

        public GameTime GameTime      => GameTimeSource.GameTime;
        public TimeSpan ModuleRunTime => runTime.Elapsed;

        public void Exit (object result)
        {
            ExitRequested = true;
            Result = result;
        }

        //====== public properties

        public bool   ExitRequested { get; private set; }
        public object Result        { get; private set; }

        public GameTimeSource GameTimeSource { get; } = new GameTimeSource (25);

        //====== public methods
       
        public void EnterModule () => runTime.Start ();
        public void ExitModule  () => runTime.Reset ();
    }
}
