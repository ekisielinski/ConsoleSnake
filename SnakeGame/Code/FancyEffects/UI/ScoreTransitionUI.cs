using Snake.Common.Helpers;
using Snake.Core;
using System;
using System.Collections.Generic;

namespace Snake.Game
{
    public sealed class ScoreTransitionUI : GameObject, IScoreStatus, IStatus
    {
        const double TransitionDurationInSecs = 0.75;

        //----------------------------------------------------------------------------------------------------------------------------------

        readonly IScoreStatus scoreStatus;

        int fromScore, toScore; // transition
        int displayedScore;

        GameTimeDelay delay;

        //====== ctors =====================================================================================================================

        public ScoreTransitionUI (IScoreStatus scoreStatus, GameTime gameTime) : base (gameTime)
        {
            this.scoreStatus = Verify.NotNull (scoreStatus, nameof (scoreStatus));

            fromScore = toScore = displayedScore = scoreStatus.Value;
        }

        //====== public properties =========================================================================================================

        public int Value => displayedScore;

        //====== override: GameObject ======================================================================================================

        protected override void UpdateImpl ()
        {
            int currentScore = scoreStatus.Value;

            if (displayedScore == currentScore) return;

            if (toScore != currentScore)
            {
                fromScore = displayedScore;
                toScore   = currentScore;

                delay = new GameTimeDelay (gameTime, TimeSpan.FromSeconds (TransitionDurationInSecs));
            }

            if (delay == null) return; // TODO: Range.Map utility could be nice

            int maxScoreToAdd = toScore - fromScore;
            double scoreToAdd = maxScoreToAdd * (delay.PercentageDone / 100d);

            displayedScore = fromScore + MiscUtils.Clamp ((int) scoreToAdd, 0, maxScoreToAdd);
        }

        //====== IStatus ===================================================================================================================

        public IReadOnlyList<StatusItem> GetStatus ()
        {
            return new[] { new StatusItem ("Score", displayedScore.ToString ()) };
        }
    }
}
