using Snake.Common.Geometry;
using Snake.Common.Helpers;
using Snake.Core;
using Snake.Game.Drawing;
using Snake.Text;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Modules.Gameplay
{
    public sealed class StatsWindow : Window
    {
        readonly StatusItem[] items;

        //====== ctors

        public StatsWindow (string playerName, IEnumerable<StatusItem> items, Size size, WindowAppearance appearance = null) : base (appearance)
        {
            this.items = Verify.NotNull (items, nameof (items)).ToArray ();

            Title = string.IsNullOrEmpty (playerName) ? "???" : playerName;
            Size  = size;
        }

        //====== override: Window
        
        protected override void PaintContent (TextCanvas canvas)
        {
            for (int i = 0; i < items.Length; i++)
            {
                var foreColor = items[i].IsValueCritical ? Color16.Red : Color16.Lime;

                WriteStatsLine (canvas, new Point (2, 1 + (i * 2)), items[i].Name, items[i].Value, foreColor);
            }
        }

        //====== private methods

        private void WriteStatsLine (TextCanvas canvas, Point position, string key, string value, Color16 foreColor)
        {
            canvas.WriteText (position, key + ":", Color16.White, Appearance.BackColor);
            canvas.WriteText (position.AddToX (key.Length + 2), value, foreColor, Appearance.BackColor);
        }
    }
}
