using Snake.Common.Helpers;
using System;

namespace Snake.Common.Logging
{
    public interface ILogger
    {
        void Log (string message, bool isError);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------

    public static class ILoggerExtensions
    {
        public static void Log (this ILogger me, Exception ex)
        {
            Verify.NotNull (me, nameof (me));
            Verify.NotNull (ex, nameof (ex));

            me.Log ($"Exception occurred!\r\n{ex}", true);
        }
    }
}
