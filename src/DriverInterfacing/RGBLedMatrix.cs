using BIGFOOT.RGBMatrix.DriverInterfacing;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.DriverInterfacing
{
    public class RGBLedMatrix
    {
        public RGBLedMatrix(int x, int y, int z) { }
        public RGBLedCanvas CreateOffscreenCanvas() { return new RGBLedCanvas(); }
        public RGBLedCanvas SwapOnVsync(RGBLedCanvas canvas) { return new RGBLedCanvas(); }
        public RGBLedCanvas GetCanvas() { return new RGBLedCanvas(); }
    }
}
