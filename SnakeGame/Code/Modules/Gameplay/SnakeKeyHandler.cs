using Snake.Common.Geometry;
using Snake.Common.Helpers;
using Snake.Core;
using Snake.Hardware.Input;
using System;

namespace SnakeGame.Modules
{
    public sealed class SnakeKeyHandler
    {
        readonly IInputDevice inputDevice;
        readonly ISnakeMoveQueue directionChanger;

        //====== ctors =====================================================================================================================

        public SnakeKeyHandler (IInputDevice inputDevice, ISnakeMoveQueue directionChanger)
        {
            this.inputDevice = Verify.NotNull (inputDevice, nameof (inputDevice));
            this.directionChanger = Verify.NotNull (directionChanger, nameof (directionChanger));
        }

        //====== public methods ============================================================================================================

        public void ReceiveKeys ()
        {
            while (true)
            {
                KeyType? keyType = inputDevice.TryReadKeys (KeyType.Up, KeyType.Down, KeyType.Left, KeyType.Right).KeyType;

                if (keyType == null) return;

                var direction = KeyTypeToDirection (keyType.Value);

                directionChanger.Add (direction);
            }
        }

        //====== private methods ===========================================================================================================

        private Direction KeyTypeToDirection (KeyType keyType)
        {
            switch (keyType)
            {
                case KeyType.Up    : return Direction.Up;
                case KeyType.Down  : return Direction.Down;
                case KeyType.Left  : return Direction.Left;
                case KeyType.Right : return Direction.Right;

                default:
                    throw new ArgumentException ("Invalid key type.");
            }
        }
    }
}
