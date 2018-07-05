using Snake.Common.Geometry;
using Snake.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Core
{
    public sealed class Terrain : ITerrainEntityReader
    {
        readonly List<TerrainEntity> entities = new List<TerrainEntity> ();

        //====== ctors =====================================================================================================================

        public Terrain (Size size)
        {
            if (size.HasNoArea) throw new ArgumentException ("Requested size is too small.");

            Size = size;
        }

        //====== public methods ============================================================================================================

        public void Add (TerrainEntity entity)
        {
            Verify.NotNull (entity, nameof (entity));

            entities.Add (entity);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public void RemoveAll (Predicate<TerrainEntity> predicate) => entities.RemoveAll (predicate);

        public int Count (Func<TerrainEntity, bool> predicate) => entities.Where (predicate).Count ();

        //====== ITerrainEntityReader ======================================================================================================

        public Size Size { get; }

        public TerrainEntity GetEntityOrNull (Point position) => entities.FirstOrDefault (x => x.Position == position);

        //====== override: Object ==========================================================================================================

        public override string ToString () => $"{nameof (Terrain)}: {Size}, Entities: {entities.Count}";
    }
}
