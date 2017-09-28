using Snake.Common.Helpers;
using System.Linq;

namespace Snake.Hardware.Input
{
    public interface IInputDevice
    {
        KeyData Peek ();
        KeyData Read ();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------

    public static class IInputDeviceExtensions
    {
        public static void Clear (this IInputDevice me)
        {
            Verify.NotNull (me, nameof (me));

            while (me.Read ().IsNull == false) { }
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public static KeyData TryReadKeys (this IInputDevice me, params KeyData[] keys)
        {
            Verify.NotNull (me, nameof (me));
            Verify.NotNull (keys, nameof (keys));

            if (keys.Length == 0) return KeyData.Null;

            var key = me.Peek ();

            if (keys.Any (x => x.Equals (key))) return me.Read ();

            return KeyData.Null;
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public static KeyData TryReadKeys (this IInputDevice me, params KeyType[] keys)
        {
            Verify.NotNull (me, nameof (me));
            Verify.NotNull (keys, nameof (keys));

            var keyDataList = keys.Select (x => KeyData.FromType (x)).ToArray ();

            return me.TryReadKeys (keyDataList);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public static bool TryReadKey (this IInputDevice me, KeyType key)
        {
            Verify.NotNull (me, nameof (me));

            return me.TryReadKeys (key).IsNull == false;
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        public static KeyData TryReadKeys (this IInputDevice me, params char[] keys)
        {
            Verify.NotNull (me, nameof (me));
            Verify.NotNull (keys, nameof (keys));

            var keyDataList = keys.Select (x => KeyData.FromCharacter (x)).ToArray ();

            return me.TryReadKeys (keyDataList);
        }
    }
}
