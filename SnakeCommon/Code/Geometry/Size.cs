using Snake.Common.Helpers;
using System;

namespace Snake.Common.Geometry
{
    public struct Size : IEquatable<Size>
    {
        public Size (int width, int height)
        {
            Width  = Verify.InRange (width,  0, int.MaxValue, nameof (width ));
            Height = Verify.InRange (height, 0, int.MaxValue, nameof (height));
        }

        //====== public properties =========================================================================================================

        public int Width  { get; }
        public int Height { get; }

        //----------------------------------------------------------------------------------------------------------------------------------

        public int  Area      => Width * Height;
        public bool HasNoArea => Area == 0;

        //====== public methods ============================================================================================================

        public Rectangle AsRectangle => new Rectangle (Point.Zero, this);

        //----------------------------------------------------------------------------------------------------------------------------------

        public Size Scale (double factor)
        {
            if (factor < 0) throw new ArgumentOutOfRangeException (nameof (factor), "Cannot be less than 0.");

            int w = (int) Math.Round (Width * factor,  MidpointRounding.AwayFromZero);
            int h = (int) Math.Round (Height * factor, MidpointRounding.AwayFromZero);

            return new Size (w, h);
        }
        
        //====== public static properties ==================================================================================================

        public static Size Zero => new Size ();

        //====== IEquatable<Size> ==========================================================================================================

        public bool Equals (Size other) => Width == other.Width && Height == other.Height;

        //====== operators =================================================================================================================

        public static bool operator == (Size left, Size right) => left.Equals (right);
        public static bool operator != (Size left, Size right) => left.Equals (right) == false;

        //====== override: Object ==========================================================================================================

        public override bool Equals (object obj) => (obj is Size) && Equals ((Size) obj);

        public override string ToString () => $"{Width}x{Height}";

        //----------------------------------------------------------------------------------------------------------------------------------

        public override int GetHashCode ()
        {
            int hash = 17;

            hash = hash * 23 + Width;
            hash = hash * 23 + Height;

            return hash;
        }
    }
}
