using Snake.Core;
using System;

namespace Snake.Game.Core
{
    public interface IGameModuleContext
    {
        GameTime GameTime      { get; }
        TimeSpan ModuleRunTime { get; }

        void Exit (object result);
    }
}
