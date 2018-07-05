using Snake.Common.Helpers;
using System;

namespace Snake.Hardware.Input
{
    public sealed class KeyData
    {
        private KeyData (char? keyChar, KeyType? keyType)
        {
            if (keyChar.HasValue && keyType.HasValue) throw new ArgumentException ("Requires only one parameter at a time.");

            if (keyChar.HasValue)
            {
                char ch = keyChar.Value;

                bool isValid = MiscUtils.IsLatinLetter (ch) || char.IsDigit (ch) || ch == ' ';

                if (isValid == false)
                {
                    throw new ArgumentException ("Invalid character.", nameof (keyChar));
                }
            }

            KeyChar = keyChar;
            KeyType = keyType;
        }

        //====== public properties =========================================================================================================

        public char?    KeyChar { get; }
        public KeyType? KeyType { get; }

        public bool IsNull => KeyChar == null && KeyType == null;

        //====== public static methods =====================================================================================================

        public static KeyData FromCharacter (char character) => new KeyData (character, null);
        public static KeyData FromType      (KeyType type)   => new KeyData (null, type);

        //====== public static properties ==================================================================================================

        public static KeyData Null { get; } = new KeyData (null, null);

        //====== override: Object ==========================================================================================================

        public override string ToString () => "Key: " + (KeyChar.HasValue ? KeyChar.ToString () : KeyType.ToString ());

        //----------------------------------------------------------------------------------------------------------------------------------

        public override bool Equals (object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;

            var other = obj as KeyData;

            return KeyChar == other.KeyChar && KeyType == other.KeyType;
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public override int GetHashCode () => KeyChar.HasValue ? KeyChar.GetHashCode () : KeyType.GetHashCode ();
    }
}
