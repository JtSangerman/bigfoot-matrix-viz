using BIGFOOT.RGBMatrix.DriverInterfacing;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.MatrixTypes.ColoredConsole
{
    public class FastConsoleMatrix : Matrix<FastConsoleCanvas>
    {
        public FastConsoleMatrix(int rows, int chained_not_yet_supported = 1, int parallel_not_yet_supported = 1)
            : base(rows, chained_not_yet_supported, parallel_not_yet_supported) { }

        public override FastConsoleCanvas InterfacedCreateOffscreenCanvas()
        {
            return new FastConsoleCanvas(Size);
        }

        public override FastConsoleCanvas InterfacedSwapOnVsync(FastConsoleCanvas canvas)
        {
            canvas.Display();
            return canvas;
        }

        public override FastConsoleCanvas InterfacedGetCanvas()
        {
            return new FastConsoleCanvas(Size);
        }

        /* The methods below should never be executed, but exist for abstraction compilation.       */
        /* An RGB Led visual can't be ran virtually. This class on the hardware exists differently  */
        /* and these functions will be executed as such there                                       */
        private const string _invalidOpError = "Invalid operation. This Method be explicitly implemented for non-hardware interfaced classes.";
        public override RGBLedCanvas CreateOffscreenCanvas()
        {
            throw new InvalidOperationException(_invalidOpError);
        }
        public override RGBLedCanvas GetCanvas()
        {
            throw new InvalidOperationException(_invalidOpError);
        }
        public override RGBLedCanvas SwapOnVsync(RGBLedCanvas canvas)
        {
            throw new InvalidOperationException(_invalidOpError);
        }
    }
}
