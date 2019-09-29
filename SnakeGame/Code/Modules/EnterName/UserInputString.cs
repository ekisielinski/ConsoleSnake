using Snake.Common.Helpers;
using Snake.Hardware.Input;
using System.Linq;

namespace Snake.Game.Modules
{
    public sealed class UserInputString
    {
        private readonly int maxLength;

        //====== ctors

        public UserInputString (int maxLength = 12)
        {
            this.maxLength = Verify.InRange (maxLength, 1, int.MaxValue, nameof (maxLength));
        }

        //====== public properties

        public string Entered { get; private set; } = string.Empty;

        public bool IsValid => Entered.Length > 0;

        //====== public methods

        public bool HandleKey (IInputDevice inputDevice)
        {
            var key = inputDevice.Peek ();

            if (key.KeyType == KeyType.Delete)
            {
                inputDevice.Read ();

                return TryRemoveLastCharacter ();
            }

            if (key.KeyChar.HasValue == false) return false;

            if (Entered.Length >= maxLength) return false;

            bool canAdd = MiscUtils.IsLatinLetter (key.KeyChar.Value) || MiscUtils.IsDigit09 (key.KeyChar.Value);

            if (canAdd)
            {
                inputDevice.Read ();

                Entered += key.KeyChar;
            }

            return canAdd;
        }

        //====== private methods

        private bool TryRemoveLastCharacter ()
        {
            if (Entered.Length == 0) return false;

            Entered = new string (Entered.ToArray (), 0, Entered.Length - 1);

            return true;
        }
    }
}
