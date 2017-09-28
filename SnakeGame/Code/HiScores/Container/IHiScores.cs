using Snake.Common.Helpers;
using System.Collections.Generic;

namespace Snake.Game
{
    public interface IHiScores
    {
        IReadOnlyList<HiScoresEntry> GetList ();

        int Capacity { get; }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------

    public static class IHiScoresExtensions
    {
        public static bool IsEmpty (this IHiScores me)
        {
            Verify.NotNull (me, nameof (me));

            return me.GetList ().Count == 0;
        }
    }
}
