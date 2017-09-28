namespace Snake.Core
{
    public interface IGameOverJustification
    {
        string Reason     { get; }
        bool   IsGameOver { get; }
    }
}
