using Snake.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Core
{
    public sealed class GameObjectsUpdater : IUpdateable
    {
        private List<GameObject> list = new List<GameObject> ();

        //====== public properties =========================================================================================================

        public bool IsEmpty => list.Count == 0;

        //====== public methods ============================================================================================================

        public void Add (GameObject gameObject)
        {
            Verify.NotNull (gameObject, nameof (gameObject));

            if (list.Any (x => ReferenceEquals (x, gameObject)))
            {
                throw new InvalidOperationException ("The object is already added to the list.");
            }

            list.Add (gameObject);
        }

        //====== IUpdateable ===============================================================================================================

        public void Update ()
        {
            RemoveDead ();

            foreach (var gameObject in list)
            {
                gameObject.Update ();
            }
        }

        //====== private methods ===========================================================================================================

        private void RemoveDead () => list.RemoveAll (x => x.IsDead);

        //====== override: Object ==========================================================================================================

        public override string ToString () => $"{nameof (GameObjectsUpdater)} ({list.Count})";
    }
}
