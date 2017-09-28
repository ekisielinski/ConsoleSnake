using Snake.Common.Helpers;
using Snake.Common.Logging;
using System;
using System.IO;

namespace Snake.Shared
{
    public sealed class FileLogger : ILogger
    {
        bool isDisabled = false;

        //====== ctors =====================================================================================================================

        public FileLogger (string fileName)
        {
            FileName = Verify.NotNull (fileName, nameof (fileName));
        }

        //====== public properties =========================================================================================================

        public string FileName { get; }

        //====== public methods ============================================================================================================

        public void DeleteLogFile ()
        {
            try { File.Delete (FileName); } catch { } // I don't care
        }

        //====== ILogger ===================================================================================================================

        public void Log (string message, bool isError)
        {
            if (isDisabled) return;

            try
            {
                string errorPrefix = isError ? "ERROR :: " : "";

                File.AppendAllText (FileName, $"{DateTime.Now} :: {errorPrefix}{message}\r\n");
            }
            catch // not much to do about it
            {
                isDisabled = true;
            }
        }
    }
}
