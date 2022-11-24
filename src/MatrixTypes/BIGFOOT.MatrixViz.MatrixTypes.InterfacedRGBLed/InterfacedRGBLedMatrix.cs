using BIGFOOT.MatrixViz.DriverInterfacing;
using System;

namespace BIGFOOT.MatrixViz.MatrixTypes.InterfacedRGBLed
{
    public class InterfacedRGBLedMatrix : Matrix<InterfacedRGBLedCanvas>
    {
        private readonly RGBLedMatrix _matrix;
        public InterfacedRGBLedMatrix(int rows, int chained, int parallel) : base(rows, chained, parallel)
        {
            _matrix = new RGBLedMatrix(rows, chained, parallel);
        }


        public override RGBLedCanvas SwapOnVsync(RGBLedCanvas canvas)
        {
            return _matrix.SwapOnVsync(canvas);
        }

        public override RGBLedCanvas GetCanvas()
        {
            return _matrix.GetCanvas();
        }

        public override RGBLedCanvas CreateOffscreenCanvas()
        {
            return _matrix.CreateOffscreenCanvas();
        }


        /* The methods below should never be executed, but exist for abstraction compilation.       */
        /* An RGB Led visual can't be ran virtually. This class on the hardware exists differently  */
        /* and these functions will be executed as such there                                       */
        private const string _invalidOpError = "Invalid operation. This Method be explicitly implemented for non-hardware interfaced classes.";
        public override InterfacedRGBLedCanvas InterfacedCreateOffscreenCanvas()
        {
            throw new InvalidOperationException(_invalidOpError);
        }
        public override InterfacedRGBLedCanvas InterfacedGetCanvas() 
        {
            throw new InvalidOperationException(_invalidOpError);
        }
        public override InterfacedRGBLedCanvas InterfacedSwapOnVsync(InterfacedRGBLedCanvas canvas)
        {
            throw new InvalidOperationException(_invalidOpError);
        }
    }
}
