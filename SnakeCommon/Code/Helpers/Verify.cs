using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Snake.Common.Helpers
{
    [DebuggerStepThrough]
    public static class Verify
    {
        [MethodImpl (MethodImplOptions.NoInlining)]
        public static T NotNull<T> (T suspect, string expr = null) where T : class
        {
            if (suspect != null) return suspect; else throw new ArgumentNullException (expr);
        }

        [MethodImpl (MethodImplOptions.NoInlining)]
        public static int InRange (int suspect, int fromInclusive, int toInclusive, string expr = null)
        {
            if (fromInclusive > toInclusive) throw new ArgumentException ("Invalid range.");

            if (suspect >= fromInclusive && suspect <= toInclusive) return suspect;

            throw new ArgumentOutOfRangeException (expr, suspect, $"Value is out of range [{fromInclusive}..{toInclusive}].");
        }

        [MethodImpl (MethodImplOptions.NoInlining)]
        public static long InRange (long suspect, long fromInclusive, long toInclusive, string expr = null)
        {
            if (fromInclusive > toInclusive) throw new ArgumentException ("Invalid range.");

            if (suspect >= fromInclusive && suspect <= toInclusive) return suspect;

            throw new ArgumentOutOfRangeException (expr, suspect, $"Value is out of range [{fromInclusive}..{toInclusive}].");
        }

        [MethodImpl (MethodImplOptions.NoInlining)]
        public static char VisibleAscii (char suspect, string expr = null)
        {
            if (MiscUtils.IsVisibleAscii (suspect)) return suspect;

            throw new ArgumentOutOfRangeException ("Must be a visible ASCII character.");
        }

        [MethodImpl (MethodImplOptions.NoInlining)]
        public static string NotNullOrWhiteSpace (string suspect, string expr = null)
        {
            if (!string.IsNullOrWhiteSpace (suspect)) return suspect;

            throw new ArgumentException ("Cannot be null or whitespace.", expr);
        }
    }
}
