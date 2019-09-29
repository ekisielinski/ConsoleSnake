using Snake.Common.Helpers;

namespace Snake.Game
{
    public sealed class HiScoresInMemoryStorage : IHiScoresStorage
    {
        IHiScores hiScoresCopy;

        //====== IHiScoresStorage

        public IHiScores Load () => hiScoresCopy;

        public void Save (IHiScores hiScores)
        {
            Verify.NotNull (hiScores, nameof (hiScores));

            hiScoresCopy = new HiScores (hiScores);
        }
    }
}
