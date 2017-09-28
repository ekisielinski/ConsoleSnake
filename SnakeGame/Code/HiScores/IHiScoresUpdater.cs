namespace Snake.Game
{
    public interface IHiScoresUpdater
    {
        int? RegisterNewResult (string playerName, int score);
    }
}
