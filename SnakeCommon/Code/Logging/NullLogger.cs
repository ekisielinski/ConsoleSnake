namespace Snake.Common.Logging
{
    public sealed class NullLogger : ILogger
    {
        public void Log (string message, bool isError)
        {
            // nothing to do here...
        }
    }
}
