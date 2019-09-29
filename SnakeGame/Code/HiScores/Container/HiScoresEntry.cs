using Snake.Common.Helpers;
using System;

namespace Snake.Game
{
    public sealed class HiScoresEntry : IEquatable<HiScoresEntry>, IComparable, IComparable<HiScoresEntry>
    {
        public HiScoresEntry (string name, int score)
        {
            if (string.IsNullOrWhiteSpace (name)) throw new ArgumentException ("Cannot be empty", nameof (name));

            Name  = name;
            Score = Verify.InRange (score, 1, int.MaxValue, nameof (score));
        }

        //====== public properties

        public string Name  { get; }
        public int    Score { get; }

        //====== IEquatable<HiScoresEntry>

        public bool Equals (HiScoresEntry other) => (other != null) ? Score == other.Score && Name == other.Name : false;

        //====== IComparable

        public int CompareTo (object obj)
        {
            if (obj == null) return 1;

            var other = obj as HiScoresEntry;

            if (other == null) throw new ArgumentException ("Invalid type.");

            return CompareTo (other);
        }

        //====== IComparable<HiScoresEntry>

        public int CompareTo (HiScoresEntry other) => other.Score.CompareTo (Score);

        //====== override: Object

        public override bool Equals (object obj) => Equals (obj as HiScoresEntry);

        public override string ToString () => $"{Name}: {Score}";

        public override int GetHashCode ()
        {
            int hash = 17;

            hash = hash * 23 + Name.GetHashCode ();
            hash = hash * 23 + Score.GetHashCode ();

            return hash;
        }
    }
}
