using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing
{
    public abstract class Matrix
    {
        public Matrix(int rows, int chained = 1, int parallel = 1)
        {

        }

        public abstract Canvas CreateOffscreenCanvas();
        public abstract Canvas GetCanvas();
        public abstract Canvas SwapOnVsync(Canvas canvas);
    }
}
