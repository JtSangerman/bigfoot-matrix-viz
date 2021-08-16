using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing
{
    public abstract class Matrix<TCanvas> where TCanvas : Canvas
    {
        public int Size { get; protected set; }

        public Matrix(int size, int rows_not_yet_supported, int parallel_not_yet_supported)
        {
            Size = size;
        }

        public abstract TCanvas CreateOffscreenCanvas();
        public abstract TCanvas GetCanvas();
        public abstract TCanvas SwapOnVsync(TCanvas canvas);
    }
}
