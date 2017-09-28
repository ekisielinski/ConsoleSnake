using Snake.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Game
{
    public sealed class AnimationFrames<T>
    {
        readonly FramesArrangement arrangement;

        //====== ctors =====================================================================================================================

        public AnimationFrames (IReadOnlyList<T> frames, FramesArrangement arrangement = FramesArrangement.Original)
        {
            Verify.NotNull (frames, nameof (frames));

            if (frames.Count < 1) throw new ArgumentException ("At least 1 frame is required.");

            this.arrangement = arrangement; // just to remember

            switch (arrangement)
            {
                case FramesArrangement.Original:
                    Frames = frames.ToArray ();
                    return;

                case FramesArrangement.AfterLastFrameTravelBackToFirstFrame:
                    Frames = frames.Concat (frames.Reverse ().Skip (1)).ToArray (); // 1234 will produce 1234_321
                    return;

                case FramesArrangement.AfterLastFrameTravelBackToSecondFrame:
                    Frames = frames.Concat (frames.Skip (1).Reverse ().Skip (1)).ToArray (); // 1234 will produce 1234_32
                    return;
            }
        }

        //====== public properties =========================================================================================================

        public IReadOnlyList<T> Frames { get; }
    }
}
