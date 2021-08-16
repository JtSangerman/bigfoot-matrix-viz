using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.Visuals.Visualizer.ColoredConsole
{
    public class ColoredConsoleMatrix : Matrix
    {
        public ColoredConsoleMatrix(int rows, int chained = 1, int parallel = 1) : base(rows, chained, parallel)
        {

        }

        public override Canvas CreateOffscreenCanvas()
        {
            throw new NotImplementedException();
        }

        public override Canvas GetCanvas()
        {
            throw new NotImplementedException();
        }

        public override Canvas SwapOnVsync(Canvas canvas)
        {
            throw new NotImplementedException();
        }
    }
}
