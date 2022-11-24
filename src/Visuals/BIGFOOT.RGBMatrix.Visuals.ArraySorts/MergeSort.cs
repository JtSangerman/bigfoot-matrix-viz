using BIGFOOT.RGBMatrix.DriverInterfacing;
using System;

namespace BIGFOOT.RGBMatrix.Visuals.ArraySorts
{
    public class MergeSort<TMatrix, TCanvas> : Visual<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        public MergeSort(TMatrix matrix) : base(matrix) { }

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
