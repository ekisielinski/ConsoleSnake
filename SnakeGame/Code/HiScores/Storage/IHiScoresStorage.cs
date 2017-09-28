namespace Snake.Game
{
    public interface IHiScoresStorage
    {
        void Save (IHiScores hiScores);

        IHiScores Load ();
    }
}
