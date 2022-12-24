using System.Drawing;

namespace BIGFOOT.MatrixViz.DriverInterfacing
{
    public class Color
    {
        public byte R;
        public byte G;
        public byte B;
        public byte Luminance => (byte)((R + G + B) / 3);
        public Color(int r, int g, int b)
        {
            int bound = byte.MaxValue + 1;
            R = (byte) (r % bound);
            G = (byte) (g % bound); 
            B = (byte) (b % bound);
        }

        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static Color FromSystemColor(System.Drawing.Color c)
        {
            return new Color(c.R, c.G, c.B);
        }

        public static Color FromKnownColor(System.Drawing.KnownColor k)
        {
            var c = System.Drawing.Color.FromKnownColor(k);
            return new Color(c.R, c.G, c.B);
        }

        public static Color ShiftLuminance(Color color, int amount)
            => new Color(
                    amount + color.R,
                    amount + color.G,
                    amount + color.B);
    }
}
