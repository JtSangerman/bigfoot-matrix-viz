using BIGFOOT.MatrixViz.DriverInterfacing;
using System;

namespace BIGFOOT.MatrixViz.Visuals.ArraySorts
{
    public class PseudoBogoSort<TMatrix, TCanvas> : Visual<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        public PseudoBogoSort(TMatrix matrix) : base(matrix) { }

        public override void VisualizeOnHardware()
        {
            throw new NotImplementedException();
        }

        public override void VisualizeVirtually()
        {
            throw new NotImplementedException();
        }
    }
}
