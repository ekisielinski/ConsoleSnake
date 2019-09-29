using Snake.Game.Core;
using Snake.Text;
using System;

namespace Snake.Game.Modules
{
    public sealed class ExitGameModule : GameModule
    {
        public override void ProcessLogic (IGameModuleContext context)
        {
            if (context.ModuleRunTime > TimeSpan.FromMilliseconds (2500))
            {
                context.Exit (null);
            }
        }

        public override void ProcessCanvas (TextCanvas canvas)
        {
            canvas.ClearColor (Color16.Black);

            canvas.WriteTextCenter (3, "S N A K E", Color16.White, Color16.Black);
            canvas.WriteTextCenter (5, "Thanks for playing!", Color16.Lime, Color16.Black);
        }
    }
}
