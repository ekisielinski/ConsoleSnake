namespace Snake.Core
{
    public interface IGameTimeSource
    {
        long Tick           { get; }
        int  TicksPerSecond { get; }
    }
}
