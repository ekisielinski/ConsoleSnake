using Snake.Common.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Game
{
    public sealed class HiScores : IHiScores
    {
        List<HiScoresEntry> list = new List<HiScoresEntry> ();

        //====== ctors =====================================================================================================================

        public HiScores (IHiScores hiScores)
        {
            Verify.NotNull (hiScores, nameof (hiScores));

            Capacity = hiScores.Capacity;

            list.AddRange (hiScores.GetList ());
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public HiScores (int capacity = 8)
        {
            Capacity = Verify.InRange (capacity, 2, 100, nameof (capacity));
        }

        //====== IHiScores =================================================================================================================

        public int Capacity { get; }
        
        public IReadOnlyList<HiScoresEntry> GetList () => list.OrderBy (x => x).ToArray ();

        //====== public methods ============================================================================================================

        public int? TryAdd (HiScoresEntry item)
        {
            Verify.NotNull (item, nameof (item));

            list.Add (item);

            list = list.OrderBy (x => x).Take (Capacity).ToList ();

            int placement = list.IndexOf (item);

            return placement == -1 ? (int?) null : placement;
        }
    }
}
