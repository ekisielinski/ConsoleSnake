using Snake.Common.Helpers;
using Snake.Core;
using System;

namespace Snake.Game
{
    public sealed class Animation<T> : IUpdateable
    {
        readonly AnimationFrames<T> frames;
        readonly GameTime           gameTime;
        readonly TimeSpan           frameDelay;
        readonly bool               repeat;

        GameTimeDelay gtd;

        int frameIndex = 0;

        //====== ctors

        public Animation (AnimationFrames<T> frames, GameTime gameTime, TimeSpan frameDelay, bool repeat = true)
        {
            this.frames     = Verify.NotNull (frames, nameof (frames));
            this.gameTime   = Verify.NotNull (gameTime, nameof (gameTime));
            this.frameDelay = frameDelay;
            this.repeat     = repeat;

            if (frameDelay <= TimeSpan.Zero) throw new ArgumentOutOfRangeException (nameof (frameDelay));
        }

        //====== public properties

        public T CurrentFrame => frames.Frames[frameIndex];

        //====== public methods

        public void Start ()
        {
            if (gtd != null) throw new InvalidOperationException ("Animation already started.");

            gtd = gameTime.CreateDelay (frameDelay);
        }

        public void Update ()
        {
            if (gtd != null && gtd.IsDone)
            {
                frameIndex = ++frameIndex % frames.Frames.Count;
                gtd = gtd.ReCreate ();
            }
        }
    }
}
