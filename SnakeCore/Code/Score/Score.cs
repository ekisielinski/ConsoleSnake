using Snake.Common.Helpers;
using System;

namespace Snake.Core
{
    internal sealed class Score : IScoreStatus
    {
        public const int MaxScore = 99999;

        //====== public properties =========================================================================================================

        public int Value { get; private set; }

        //====== public methods ============================================================================================================

        public void Increase (int value)
        {
            Verify.InRange (value, 0, MaxScore, nameof (value));

            int newValue = Value + value;

            Value = Math.Min (MaxScore, newValue);
        }

        //====== override: Object ==========================================================================================================

        public override string ToString () => $"{nameof (Score)}: {Value}";
    }
}
