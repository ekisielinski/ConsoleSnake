using Snake.Common.Geometry;
using Snake.Common.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Core
{
    public interface ISnakeBody
    {
        IReadOnlyList<Point> Parts { get; }
    }

    public static class ISnakeBodyExtensions
    {
        public static Point Head (this ISnakeBody me)
        {
            Verify.NotNull (me, nameof (me));

            return me.Parts[0];
        }

        public static IReadOnlyList<Point> Tail (this ISnakeBody me)
        {
            Verify.NotNull (me, nameof (me));

            return me.Parts.Skip (1).ToArray ();
        }
    }
}
