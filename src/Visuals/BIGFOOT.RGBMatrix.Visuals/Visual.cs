using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using System;

namespace BIGFOOT.RGBMatrix.Visuals
{
    public abstract class Visual<TMatrix, TCanvas> 
        where TMatrix : Matrix<TCanvas> 
        where TCanvas : Canvas
    {
        protected readonly TMatrix _matrix;
        public Visual(TMatrix matrix)
        {
            _matrix = matrix;
        }

        public abstract void Visualise();
    }
}
