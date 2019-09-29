using Snake.Common.Helpers;
using System;
using System.Diagnostics;

namespace Snake.Game.Core
{
    public sealed class Sleeper : ISleeper
    {
        readonly Stopwatch sw = new Stopwatch ();

        //====== public methods

        public void Sleep (TimeSpan duration)
        {
            Verify.InRange (duration.Ticks, 0, long.MaxValue, nameof (duration));

            sw.Restart ();

            while (sw.Elapsed < duration) { }
        }
    }
}
