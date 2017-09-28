using Snake.Common.Helpers;

namespace Snake.Game
{
    public sealed class HiScoresManager : IHiScoresReader, IHiScoresUpdater
    {
        readonly IHiScoresStorage hiScoresStorage;

        //====== ctors =====================================================================================================================

        public HiScoresManager (IHiScoresStorage hiScoresStorage)
        {
            this.hiScoresStorage = Verify.NotNull (hiScoresStorage, nameof (hiScoresStorage));
        }

        //====== IHiScoresReader ===========================================================================================================

        public IHiScores GetHiScores ()
        {
            var hiScores = hiScoresStorage.Load ();

            if (hiScores.GetList ().Count == 0)
            {
                hiScores = GetFakeHiScores ();

                hiScoresStorage.Save (hiScores);
            }

            return hiScores;
        }

        //====== IHiScoresUpdater ==========================================================================================================

        public int? RegisterNewResult (string playerName, int score)
        {
            if (score < 1) return null;

            var copy = new HiScores (GetHiScores ());

            int? placement = copy.TryAdd (new HiScoresEntry (playerName, score));

            if (placement.HasValue)
            {
                hiScoresStorage.Save (copy);
            }

            return placement;
        }

        //====== private static methods ====================================================================================================

        private static IHiScores GetFakeHiScores ()
        {
            var result = new HiScores ();

            result.TryAdd (new HiScoresEntry ("Janusz"  , 10));
            result.TryAdd (new HiScoresEntry ("Stefan"  , 20));
            result.TryAdd (new HiScoresEntry ("Barbara" , 30));
            result.TryAdd (new HiScoresEntry ("Dawid"   , 40));

            return result;
        }
    }
}
