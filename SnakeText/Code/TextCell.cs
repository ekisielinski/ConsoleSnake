using Snake.Common.Helpers;
using System;

namespace Snake.Text
{
    public struct TextCell : IEquatable<TextCell>
    {
        public TextCell (char character, Color16 foreColor, Color16 backColor)
        {
            Character = Verify.VisibleAscii (character, nameof (character));

            ForeColor = foreColor;
            BackColor = backColor;
        }

        public static TextCell DefaultEmpty => new TextCell (' ', Color16.Silver, Color16.Black);

        //====== public properties

        public char Character { get; }

        public Color16 ForeColor { get; }
        public Color16 BackColor { get; }

        public bool IsValid => MiscUtils.IsVisibleAscii (Character);

        //====== IEquatable<TextCell>

        public bool Equals (TextCell other)
        {
            return Character == other.Character && ForeColor == other.ForeColor && BackColor == other.BackColor;
        }

        //====== operators

        public static bool operator == (TextCell left, TextCell right) => left.Equals (right);
        public static bool operator != (TextCell left, TextCell right) => left.Equals (right) == false;

        //====== override: Object

        public override bool Equals (object obj)
        {
            return (obj is TextCell) ? Equals ((TextCell) obj) : false;
        }

        public override int GetHashCode ()
        {
            int hash = 17;

            hash = hash * 23 + Character.GetHashCode ();
            hash = hash * 23 + ForeColor.GetHashCode ();
            hash = hash * 23 + BackColor.GetHashCode ();

            return hash;
        }

        public override string ToString () => Character.ToString ();
    }
}
