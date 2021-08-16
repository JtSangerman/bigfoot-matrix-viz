using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing
{
    public class Point
    {
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x;
        public int y;
        public bool recycled;
    }
}
