using System;

namespace Snake.Common.Helpers
{
    public static class MiscUtils
    {
        public static bool IsVisibleAscii (char ch)
        {
            return !(ch < 32 || ch == 127 || ch == 255);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public static bool IsLatinLetter (char ch)
        {
            return (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z');
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public static bool IsDigit09 (char ch)
        {
            return (ch >= '0' && ch <= '9');
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public static int Clamp (int value, int min, int max)
        {
            if (min > max) throw new ArgumentException ($"The {min} value cannot be greater tha the {max} value.");

            return Math.Min (Math.Max (value, min), max);
        }
    }
}
