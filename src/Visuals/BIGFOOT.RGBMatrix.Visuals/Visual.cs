using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using System;
using System.Threading;

namespace BIGFOOT.RGBMatrix.Visuals
{
    public abstract class Visual<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        protected readonly TMatrix Matrix;
        private bool _isEmulating 
        {
            get 
            { 
                return Matrix.GetType().Name != "InterfacedRGBLedMatrix"; 
            } 
        }

        public Visual(TMatrix matrix)
        {
            Matrix = matrix;
        }

        public void Visualize()
        {
            if (_isEmulating)
            {
                VisualizeVirtually();
            }
            else
            {
                VisualizeOnHardware();
            }

            Thread.Sleep(10000);
        }

        public abstract void VisualizeOnHardware();
        public abstract void VisualizeVirtually();
    }
}
