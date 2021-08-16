using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing
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
    }
}
