using Snake.Core;
using Snake.Text;

namespace Snake.Game.Drawing
{
    public sealed class SnakeBodyPartLookProvider : ITerrainEntityLookProvider
    {
        public TextCell GetLook (TerrainEntity entity)
        {
            var color = (entity as SnakeBodyPartEntity).IsHead ? Color16.White : Color16.Red;

            return new TextCell ('X', color, Color16.Black);
        }
    }
}
