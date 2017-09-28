using Snake.Core;
using Snake.Text;

namespace Snake.Game.Drawing
{
    public interface ITerrainEntityLookProvider
    {
        TextCell GetLook (TerrainEntity entity);
    }
}
