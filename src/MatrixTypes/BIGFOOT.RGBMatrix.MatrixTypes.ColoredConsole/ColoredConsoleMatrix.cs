using BIGFOOT.RGBMatrix.DriverInterfacing;
using System;

namespace BIGFOOT.RGBMatrix.MatrixTypes.ColoredConsole
{
    public class ColoredConsoleMatrix : Matrix<ColoredConsoleCanvas>
    {
        public ColoredConsoleMatrix(int rows, int chained_not_yet_supported = 1, int parallel_not_yet_supported = 1) 
            : base(rows, chained_not_yet_supported, parallel_not_yet_supported) { }

        public override ColoredConsoleCanvas InterfacedCreateOffscreenCanvas()
        {
            return new ColoredConsoleCanvas(Size);
        }

        public override ColoredConsoleCanvas InterfacedSwapOnVsync(ColoredConsoleCanvas canvas)
        {
            canvas.Display();
            return canvas;
        }

        public override ColoredConsoleCanvas InterfacedGetCanvas()
        {
            return new ColoredConsoleCanvas(Size);
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
