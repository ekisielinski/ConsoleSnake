using Snake.Common.Geometry;

namespace Snake.Core
{
    public interface ITerrainEntityReader
    {
        Size Size { get; }

        TerrainEntity GetEntityOrNull (Point position);
    }
}
