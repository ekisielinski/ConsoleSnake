using Snake.Common.Geometry;
using Snake.Common.Helpers;
using Snake.Core;
using Snake.Text;
using System;
using System.Collections.Generic;

namespace Snake.Game.Drawing
{
    public class TerrainRenderer
    {
        Dictionary<Type, ITerrainEntityLookProvider> lookProviders = new Dictionary<Type, ITerrainEntityLookProvider> ();

        //====== public methods

        public void Register<T> (ITerrainEntityLookProvider lookProvider)
        {
            Verify.NotNull (lookProvider, nameof (lookProvider));

            lookProviders.Add (typeof (T), lookProvider);
        }

        public void Paint (ITerrainEntityReader boardReader, TextCanvas canvas, Point offset)
        {
            Verify.NotNull (boardReader, nameof (boardReader));
            Verify.NotNull (canvas, nameof (canvas));

            var rectFrame = new Rectangle (offset, new Size (boardReader.Size.Width + 2, boardReader.Size.Height + 2));

            canvas.Frame (rectFrame, Color16.Gray, Color16.Black, true);

            for (int x = 0; x < boardReader.Size.Width; x++)
            {
                for (int y = 0; y < boardReader.Size.Height; y++)
                {
                    var entity = boardReader.GetEntityOrNull (new Point (x, y));

                    var screenCell = GetLook (entity);

                    canvas.Write (new Point (x, y).Add (offset.Add (Point.One)), screenCell);
                }
            }
        }

        //====== private methods

        private TextCell GetLook (TerrainEntity entity)
        {
            if (entity == null) return new TextCell (' ', Color16.Black, Color16.Black);

            lookProviders.TryGetValue (entity.GetType (), out var lookProvider);

            return (lookProvider != null) ? lookProvider.GetLook (entity)
                                          : new TextCell ('?', Color16.Gray, Color16.Black);
        }
    }
}
