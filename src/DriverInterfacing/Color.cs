using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.DriverInterfacing
{
    public class Color
    {
        public byte R;
        public byte G;
        public byte B;
        public Color(int r, int g, int b)
        {
            R = (byte)r;
            G = (byte)g;
            B = (byte)b;
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
    }
}
