using Snake.Game;

namespace Snake.Shared
{
    public interface IHiScoresSerializer
    {
        IHiScores Deserialize (byte[] data);

        byte[] Serialize (IHiScores hiScores);
    }
}
