using Snake.Core;
using Snake.Text;

namespace Snake.Game.Drawing
{
    public sealed class AppleLookProvider : ITerrainEntityLookProvider
    {
        public TextCell GetLook (TerrainEntity entity)
        {
            var appleEntity = entity as AppleEntity;

            var color = (appleEntity.Type == AppleEntity.AppleType.Normal) ? Color16.Lime : Color16.Red;

            if (appleEntity.IsDying && appleEntity.DelayBeforeDie.RemainingTicks % 2 == 0) // blinking
            {
                color = Color16.DarkRed;
            }

            return new TextCell ('o', color, Color16.Black);
        }
    }
}
