using BIGFOOT.MatrixViz.DriverInterfacing;
using System;

namespace BIGFOOT.MatrixViz.MatrixTypes.Direct2D
{
    public class Direct2DMatrix : Matrix<Direct2DCanvas>
    {
        public Direct2DMatrix(int rows, int chained_not_yet_supported = 1, int parallel_not_yet_supported = 1)
            : base(rows, chained_not_yet_supported, parallel_not_yet_supported) { }


        private Direct2DCanvas _canvas;
        public override Direct2DCanvas InterfacedCreateOffscreenCanvas()
        {
            if (_canvas == null)
            {
                _canvas = new Direct2DCanvas(Size, false);
            }
            return _canvas;
        }

        public override Direct2DCanvas InterfacedGetCanvas()
        {
            return _canvas;
        }

        public override Direct2DCanvas InterfacedSwapOnVsync(Direct2DCanvas canvas)
        {
            //_canvas.Display();
            return canvas;
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
