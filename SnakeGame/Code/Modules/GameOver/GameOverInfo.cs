using Snake.Common.Helpers;

namespace Snake.Game.Modules
{
    public sealed class GameOverInfo
    {
        public GameOverInfo (string playerName, int score, string reason)
        {
            PlayerName = Verify.NotNullOrWhiteSpace (playerName, nameof (playerName));
            Score      = Verify.InRange (score, 0, int.MaxValue, nameof (score));
            Reason     = Verify.NotNullOrWhiteSpace (reason, nameof (reason));
        }

        //====== public properties =========================================================================================================

        public string PlayerName { get; }
        public int    Score      { get; }
        public string Reason     { get; }
    }
}
