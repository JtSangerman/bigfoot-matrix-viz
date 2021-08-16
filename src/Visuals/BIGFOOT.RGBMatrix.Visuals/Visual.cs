using System;

namespace BIGFOOT.RGBMatrix.Visuals
{
    public abstract class Visual<TMatrix, TCanvas>
    {
        protected readonly TMatrix _matrix;
        protected readonly TCanvas _canvas;
        public Visual(TMatrix matrix, TCanvas canvas)
        {
            _matrix = matrix;
            _canvas = canvas;
        }
    }
}
