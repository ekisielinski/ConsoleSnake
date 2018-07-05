using Snake.Text;
using System;
using System.Text;

namespace SnakeConsoleUI.Hardware
{
    /// <summary>
    /// For faster writing to the console window.
    /// Characters with the same fore and back color placed sequentially are written in one Console.Write call.
    /// </summary>

    internal sealed class ConsoleWriter : IDisposable
    {
        readonly StringBuilder sb = new StringBuilder (500);

        ConsoleColor? currentForeColor = null;
        ConsoleColor? currentBackColor = null;

        bool isDisposed = false;

        //====== public methods ============================================================================================================

        public void WriteCell (TextCell cell)
        {
            if (isDisposed) throw new ObjectDisposedException (GetType ().Name);

            bool colorChanged = (currentForeColor != ToConsoleColor (cell.ForeColor)) || (currentBackColor != ToConsoleColor (cell.BackColor));

            if (colorChanged)
            {
                FlushBuffer ();
            }

            currentForeColor = ToConsoleColor (cell.ForeColor);
            currentBackColor = ToConsoleColor (cell.BackColor);

            sb.Append (cell.Character);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public void Close () => Dispose ();

        //====== IDisposable ===============================================================================================================

        public void Dispose ()
        {
            if (isDisposed == false)
            {
                FlushBuffer ();
                isDisposed = true;
            }
        }

        //====== private methods ===========================================================================================================

        private void FlushBuffer ()
        {
            if (sb.Length == 0) return;

            Console.ForegroundColor = currentForeColor.Value;
            Console.BackgroundColor = currentBackColor.Value;

            Console.Write (sb.ToString ());

            sb.Clear ();
        }

        //====== private static methods ====================================================================================================

        private static ConsoleColor ToConsoleColor (Color16 color16) => (ConsoleColor) color16;
    }
}
